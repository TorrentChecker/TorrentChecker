'HTTPClient known bug:
'timeout does not affect DNS resolution

Imports System.Net
Imports System.Net.Http
Imports System.Text
Imports System.Threading
Imports System.Web

Public Class clsHTTP
    Class HTTPResult
        Public Property Result As String
        Public Property ResultB As Byte()
        Public Property Success As Boolean
        Public Property Headers As Headers.HttpContentHeaders
    End Class

    Private Const http_timeout_sec As Double = 30
    Shared cookie_container As New CookieContainer
    Private http_handler As New HttpClientHandler With {.CookieContainer = cookie_container, .UseProxy = False}
    Private http_request As New HttpClient(http_handler) With {.Timeout = TimeSpan.FromSeconds(http_timeout_sec)}
    Private cts As New CancellationTokenSource()
    Private web_proxy As New WebProxy
    Private proxy_builder As New UriBuilder
    Private proxy_credentials As New NetworkCredential
    Private ret As New HTTPResult

    Public Function Cancel() As CancellationToken
        'Cancel all requests
        cts.Cancel()
        cts.Dispose()
        cts = New CancellationTokenSource()
        Return cts.Token
    End Function

    Public Sub ClearCookies(url As Uri)
        For Each c As Cookie In cookie_container.GetCookies(url)
            c.Expired = True
        Next
    End Sub

    Public Sub SetProxy(ByVal ProxyParams As Dictionary(Of String, Object), Optional ByVal username As String = "", Optional ByVal password As String = "")
        proxy_builder.Host = CStr(ProxyParams("proxy_address"))
        proxy_builder.Port = CInt(ProxyParams("proxy_port"))
        proxy_credentials.UserName = username
        proxy_credentials.Password = password
        web_proxy.Address = proxy_builder.Uri
        web_proxy.Credentials = proxy_credentials
    End Sub

    Public WriteOnly Property UseProxy As Boolean
        Set(value As Boolean)
            If http_handler.UseProxy = value Then Exit Property
            Cancel()
            http_request.Dispose()
            http_handler.Dispose()
            http_handler = New HttpClientHandler With {.CookieContainer = cookie_container, .UseProxy = value}
            http_request = New HttpClient(http_handler) With {.Timeout = TimeSpan.FromSeconds(http_timeout_sec)}
            If value Then
                http_handler.Proxy = web_proxy
                http_handler.PreAuthenticate = True
            End If
        End Set
    End Property

    Public Async Function HTTPGet(ByVal Url As String, Optional ByVal GetData As Dictionary(Of String, String) = Nothing, Optional ByVal asBinary As Boolean = False) As Task(Of HTTPResult)
        Dim token As CancellationToken = cts.Token

        Try
            If GetData IsNot Nothing Then
                Url &= "?"
                Url &= String.Join("&", GetData.Select(Function(kvp) String.Format("{0}={1}", kvp.Key, Uri.EscapeDataString(kvp.Value))))
            End If
            With http_request
                .DefaultRequestHeaders.Clear()
                .DefaultRequestHeaders.ConnectionClose = True
                '.DefaultRequestHeaders.Accept.Add(New Headers.MediaTypeWithQualityHeaderValue("text/html"))
                .DefaultRequestHeaders.Add("User-Agent", APP_NAME)
                Using Response As HttpResponseMessage = Await http_request.GetAsync(Url, token)
                    If asBinary Then
                        ret.ResultB = Await Response.Content.ReadAsByteArrayAsync
                    Else
                        If LCase(Response.Content.Headers.ContentType.CharSet) = "cp1251" Then
                            Response.Content.Headers.ContentType.CharSet = "windows-1251"
                        End If
                        ret.Result = Await Response.Content.ReadAsStringAsync
                    End If
                    ret.Headers = Response.Content.Headers
                    ret.Success = True
                End Using
                Return ret
            End With
        Catch ex As TaskCanceledException
            If Not token.IsCancellationRequested Then
                'timed out
                Throw New TimeoutException("The operation has timed out.", New Exception(Url))
            End If

            ret.Success = False
            Return ret
        End Try
    End Function

    Public Async Function HTTPPost(ByVal Url As String, ByVal PostData As Dictionary(Of String, String), Optional ByVal asBinary As Boolean = False) As Task(Of HTTPResult)
        Dim token As CancellationToken = cts.Token
        Dim post_encoding As String = "windows-1251"
        Dim postData_encoded As String = String.Join("&", PostData.Select(Function(kvp) String.Format("{0}={1}", kvp.Key, HttpUtility.UrlEncode(kvp.Value, Encoding.GetEncoding(post_encoding)))))
        Dim postData_bytes As Byte() = Encoding.GetEncoding(post_encoding).GetBytes(postData_encoded)

        Try
            'Using content As FormUrlEncodedContent = New FormUrlEncodedContent(PostData)
            Using content As ByteArrayContent = New ByteArrayContent(postData_bytes)
                content.Headers.Add("Content-Type", "application/x-www-form-urlencoded")
                With http_request
                    .DefaultRequestHeaders.Clear()
                    .DefaultRequestHeaders.ConnectionClose = True
                    .DefaultRequestHeaders.ExpectContinue = False
                    .DefaultRequestHeaders.Accept.Add(New Headers.MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"))
                    .DefaultRequestHeaders.Add("User-Agent", APP_NAME)
                    Using Response As HttpResponseMessage = Await .PostAsync(Url, content, token)
                        With Response
                            If asBinary Then
                                ret.ResultB = Await Response.Content.ReadAsByteArrayAsync
                            Else
                                If LCase(Response.Content.Headers.ContentType.CharSet) = "cp1251" Then
                                    Response.Content.Headers.ContentType.CharSet = "windows-1251"
                                End If
                                ret.Result = Await Response.Content.ReadAsStringAsync()
                            End If
                            ret.Headers = Response.Content.Headers
                            ret.Success = True
                        End With
                    End Using
                    Return ret
                End With
            End Using
        Catch ex As TaskCanceledException
            If Not token.IsCancellationRequested Then
                'timed out
                Throw New TimeoutException("The operation has timed out.", New Exception(Url))
            End If

            ret.Success = False
            Return ret
        End Try
    End Function

    Public Sub New()
        ServicePointManager.DefaultConnectionLimit = 5
    End Sub
End Class