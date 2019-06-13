Imports System.Text.RegularExpressions
Imports System.Reflection
Imports System.Net
Imports System.Text
Imports System.IO

Public Module mdlMain

    Public Enum Columns
        torrent_status
        forum_name
        topic_id
        topic_name
        topic_author
        torrent_dl
        torrent_size
        torrent_size_dimension
        torrent_unique_identifier
        forum_text
        tracker_id
        tracker_name
        found_datetime
        found_unix_datetime
        keyword_id
        marked_as_new
        torrent_size_bytes
    End Enum

    Public dgvTorrentsColumns As New Dictionary(Of String, Object) From
    {
        {Columns.tracker_name.ToString, New Dictionary(Of String, Object) From {{"name", "Трекер"}, {"index", 0}, {"width", 100}, {"visibility", True}, {"type", GetType(String)}, {"technical", False}}},
        {Columns.torrent_status.ToString, New Dictionary(Of String, Object) From {{"name", "Статус"}, {"index", 1}, {"width", 100}, {"visibility", True}, {"type", GetType(String)}, {"technical", False}}},
        {Columns.forum_name.ToString, New Dictionary(Of String, Object) From {{"name", "Форум"}, {"index", 2}, {"width", 100}, {"visibility", True}, {"type", GetType(String)}, {"technical", False}}},
        {Columns.topic_name.ToString, New Dictionary(Of String, Object) From {{"name", "Тема"}, {"index", 3}, {"width", 100}, {"visibility", True}, {"type", GetType(String)}, {"technical", False}}},
        {Columns.topic_author.ToString, New Dictionary(Of String, Object) From {{"name", "Автор"}, {"index", 4}, {"width", 100}, {"visibility", True}, {"type", GetType(String)}, {"technical", False}}},
        {Columns.torrent_size_dimension.ToString, New Dictionary(Of String, Object) From {{"name", "Размер"}, {"index", 5}, {"width", 100}, {"visibility", True}, {"type", GetType(String)}, {"technical", False}}},
        {Columns.found_datetime.ToString, New Dictionary(Of String, Object) From {{"name", "Найдено"}, {"index", 6}, {"width", 100}, {"visibility", True}, {"type", GetType(String)}, {"technical", False}}},
        {Columns.torrent_dl.ToString, New Dictionary(Of String, Object) From {{"name", Columns.torrent_dl.ToString}, {"index", 7}, {"width", 0}, {"visibility", False}, {"type", GetType(String)}, {"technical", True}}},
        {Columns.topic_id.ToString, New Dictionary(Of String, Object) From {{"name", Columns.topic_id.ToString}, {"index", 8}, {"width", 0}, {"visibility", False}, {"type", GetType(String)}, {"technical", True}}},
        {Columns.tracker_id.ToString, New Dictionary(Of String, Object) From {{"name", Columns.tracker_id.ToString}, {"index", 9}, {"width", 0}, {"visibility", False}, {"type", GetType(Trackers)}, {"technical", True}}},
        {Columns.found_unix_datetime.ToString, New Dictionary(Of String, Object) From {{"name", Columns.found_unix_datetime.ToString}, {"index", 10}, {"width", 0}, {"visibility", False}, {"type", GetType(Long)}, {"technical", True}}},
        {Columns.keyword_id.ToString, New Dictionary(Of String, Object) From {{"name", Columns.keyword_id.ToString}, {"index", 11}, {"width", 0}, {"visibility", False}, {"type", GetType(Integer)}, {"technical", True}}},
        {Columns.marked_as_new.ToString, New Dictionary(Of String, Object) From {{"name", Columns.marked_as_new.ToString}, {"index", 12}, {"width", 0}, {"visibility", False}, {"type", GetType(Boolean)}, {"technical", True}}},
        {Columns.torrent_size_bytes.ToString, New Dictionary(Of String, Object) From {{"name", Columns.torrent_size_bytes.ToString}, {"index", 13}, {"width", 0}, {"visibility", False}, {"type", GetType(Double)}, {"technical", True}}}
    }

    Public dgvTorrentsSorting As New Dictionary(Of String, Object) From
    {
        {"column_name", Columns.found_unix_datetime.ToString},
        {"sort_order", SortOrder.Descending}
    }

    Public MainFormSettings As New Dictionary(Of String, Object) From
        {
            {"height", 600},
            {"width", 1000},
            {"top", Screen.PrimaryScreen.Bounds.Height / 2 - 300},
            {"left", Screen.PrimaryScreen.Bounds.Width / 2 - 500},
            {"windowstate", FormWindowState.Normal},
            {"splitter_distance", 170}
        }

    Public Enum Trackers
        rutracker
        kinozal
    End Enum
    Class LoggedResult
        Public Enum LoggedStatuses
            logged
            no_logged
            need_captcha
        End Enum
        Public Property Status As LoggedStatuses
        Public Property captcha_url As String
        Public Property captcha_sid As String
        Public Property captcha_code As String
    End Class

    Public dtFoundTorrents As New DataTable 'torrents found during the current session
    Public dtStoredTorrents As New DataTable 'all found torrents

    Public TrackerBase As New Dictionary(Of Trackers, String) From
        {
            {Trackers.rutracker, "https://rutracker.org"},
            {Trackers.kinozal, "https://kinozal.guru"}
        }

    Private rutracker As New Dictionary(Of Object, Object) From
        {
            {"login_url", TrackerBase(Trackers.rutracker) & "/forum/login.php"},
            {"search_url", TrackerBase(Trackers.rutracker) & "/forum/tracker.php"},
            {"download_url", TrackerBase(Trackers.rutracker) & "/forum/dl.php?t="},
            {"registration_url", TrackerBase(Trackers.rutracker) & "/forum/profile.php?mode=register"},
            {"topic_url", TrackerBase(Trackers.rutracker) & "/forum/viewtopic.php?t="},
            {"tracker_name", Regex.Match(TrackerBase(Trackers.rutracker), "://(.*)").Groups(1).Value},
            {"is_logged", False},
            {"is_tracker_failure", False},
            {"forum_token", ""},
            {"credentials", New Dictionary(Of String, String) From
                {
                    {"login_username", ""},
                    {"login_password", ""},
                    {"login", "%C2%F5%EE%E4"}
                }
            },
            {"forums", New DataTable}
        }

    Private kinozal As New Dictionary(Of Object, Object) From
        {
            {"login_url", TrackerBase(Trackers.kinozal) & "/takelogin.php"},
            {"search_url", TrackerBase(Trackers.kinozal) & "/browse.php"},
            {"download_url", Regex.Replace(TrackerBase(Trackers.kinozal), "(https?://)(.*)", "$1dl.$2") & "/download.php?id="},
            {"registration_url", TrackerBase(Trackers.kinozal) & "/signup.php"},
            {"topic_url", TrackerBase(Trackers.kinozal) & "/details.php?id="},
            {"tracker_name", Regex.Match(TrackerBase(Trackers.kinozal), "://(.*)").Groups(1).Value},
            {"details_url", TrackerBase(Trackers.kinozal) & "/get_srv_details.php?action=2&id="},
            {"tz_offset", New TimeSpan},
            {"server_datetime", New DateTime},
            {"is_logged", False},
            {"is_tracker_failure", False},
            {"credentials", New Dictionary(Of String, String) From
                {
                    {"touser", "1"},
                    {"wact", "takerecover"},
                    {"username", ""},
                    {"password", ""}
                }
            },
            {"forums", New DataTable},
            {"formats", New DataTable}
        }

    Public TrackerParams As New Dictionary(Of Trackers, Object) From
        {
            {Trackers.rutracker, rutracker},
            {Trackers.kinozal, kinozal}
        }

    Public KeyWordsParams As New Dictionary(Of Integer, Dictionary(Of String, String)) From
        {
            {0, New Dictionary(Of String, String) From
                {
                    {"label", "Все"},
                    {"count_of_new", "0"}
                }
            }
        }

    Public KwListToParamsMapping(0) As Integer  'mapping between KeyWordsBox's index to KeyWordsParams's index
    Public KeyWordsSelectedItem As Integer 'currently selected item in the KeyWordsBox
    Public TrackerSelectedID As Trackers  'currently selected tracker in the frmKeyWord
    Public ForumsSelected As New Dictionary(Of Trackers, String) From
        {
            {Trackers.rutracker, ""},
            {Trackers.kinozal, ""}
        } 'currently selected forums in the frmForums

    Public FormatsSelected As New Dictionary(Of Trackers, String) From
        {
            {Trackers.kinozal, ""}
        } 'currently selected formats in the frmForums (kinozal only)

    Public ProxySettings As New Dictionary(Of String, Object) From
    {
        {"use_proxy", False},
        {"proxy_address", ""},
        {"proxy_port", ""}
    }
    Public AppOptions As New Dictionary(Of String, Object) From
    {
        {"hide_to_tray", False},
        {"close_to_tray", False},
        {"run_hidden", False},
        {"show_journal_tab", False},
        {"auto_cheking", False},
        {"write_fatal_errors", True},
        {"check_for_updates", True},
        {"confirm_exit", True}
    }

    Public AppRepository As New Dictionary(Of String, String) From
    {
        {"repository_url", "https://github.com/TorrentChecker/TorrentChecker/releases/latest"},
        {"repository_api", "https://api.github.com/repos/TorrentChecker/TorrentChecker/releases/latest"}
    }

    'constants
    Public APP_VERSION As String = "3.0.2"
    Public APP_NAME As String = "TorrentChecker v" & APP_VERSION
    Public HOME_PAGE_URL As String = TrackerBase(Trackers.rutracker) & "/forum/viewtopic.php?t=992695"
    Public REGEX_TIMEOUT As TimeSpan = TimeSpan.FromMilliseconds(1000)
    Public FONT_BOLD As Font = New Font("Verdana", 8.0!, FontStyle.Bold)
    Public FONT_NORMAL As Font = New Font("Verdana", 8.0!, FontStyle.Regular)
    Public FONT_ITALIC As Font = New Font("Verdana", 8.0!, FontStyle.Italic)
    Public FONT_UNDERLINE As Font = New Font("Verdana", 8.0!, FontStyle.Underline)
    Public UNIX_TIME_ORIGIN As DateTime = New DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
    Public DGV_BOLD As New DataGridViewCellStyle With {.Font = FONT_BOLD}
    Public DGV_NORMAL As New DataGridViewCellStyle With {.Font = FONT_NORMAL}
    Public ITEM_PADDING As New System.Windows.Forms.Padding(5, 0, 0, 0)
    Public DATETIME_FORMAT As String = "dd.MM.yyyy HH:mm:ss"
    Public CredentialsControl_SELINDEX As Integer = 0
    Public EMPTY_ICON As Icon = My.Resources.ico_empty
    Public TRAY_ICON As Icon = My.Resources.main_ico
    Public TRAY_ICON_PAUSE As Icon = My.Resources.main_ico_pause
    Public IMAGE_ALL_KW As Bitmap = My.Resources.img_all_kw
    Public IMAGE_CLOCK As Bitmap = My.Resources.img_clock
    Public IMAGE_PAUSE As Bitmap = My.Resources.img_pause
    Public LAST_CHECK_TIME As DateTime = UNIX_TIME_ORIGIN
    Public APP_PAUSED As Boolean = False
    Public APP_HIDDEN As Boolean = False
    Public TORRENTS_MAX_STORETIME As Long = 24 * 60 * 60 * 30 '30 days
    Public TORRENTS_MAX_RESULTS As Short = 100
    Public CHECK_INTERVAL As Integer = 15 * 60 * 1000 '15 minutes

    Public Function CheckLogged(ByVal str As String, ByVal tracker_id As Trackers) As LoggedResult
        Dim ret As Boolean, forum_token As String = "", ret_res As New LoggedResult
        Select Case tracker_id
            Case Trackers.rutracker
                ret = Regex.IsMatch(str, "<a id=""logged-in-username""", RegexOptions.IgnoreCase, REGEX_TIMEOUT)
                forum_token = Regex.Match(str, "form_token:\s'(.*?)'", RegexOptions.IgnoreCase, REGEX_TIMEOUT).Groups(1).Value
            Case Trackers.kinozal
                ret = Regex.IsMatch(str, "logout.php\?hash4u=", RegexOptions.IgnoreCase, REGEX_TIMEOUT)
        End Select
        TrackerParams(tracker_id)("is_logged") = ret
        If tracker_id = Trackers.rutracker Then
            TrackerParams(tracker_id)("forum_token") = forum_token
        End If

        If ret Then
            ret_res.Status = LoggedResult.LoggedStatuses.logged
        Else
            'check for captcha
            ret = Regex.IsMatch(str, "cap_sid", RegexOptions.IgnoreCase, REGEX_TIMEOUT)
            If ret Then
                ret_res.Status = LoggedResult.LoggedStatuses.need_captcha
                ret_res.captcha_url = Regex.Match(str, "<div><img src=""(.+?)"".+?alt=""pic""></div>", RegexOptions.IgnoreCase, REGEX_TIMEOUT).Groups(1).Value
                ret_res.captcha_sid = Regex.Match(str, "name=""cap_sid"" value=""(.+?)""", RegexOptions.IgnoreCase, REGEX_TIMEOUT).Groups(1).Value
                ret_res.captcha_code = Regex.Match(str, "name=""(cap_code_[\w]+)""", RegexOptions.IgnoreCase, REGEX_TIMEOUT).Groups(1).Value
            Else
                ret_res.Status = LoggedResult.LoggedStatuses.no_logged
            End If
        End If

        Return ret_res
    End Function

    Public Function GetUniformDimension(ByVal torrent_dimension As String) As String
        Dim d As String
        Select Case UCase(torrent_dimension)
            Case "B", "Б"
                d = "B"
            Case "KB", "КБ"
                d = "KB"
            Case "MB", "МБ"
                d = "MB"
            Case "GB", "ГБ"
                d = "GB"
            Case "TB", "ТБ"
                d = "TB"
            Case "PB", "ПБ"
                d = "PB"
            Case "EB", "ЭБ"
                d = "EB"
            Case Else
                d = "B"
        End Select

        Return d
    End Function
    Public Function GetTorrentSizeInBytes(ByVal torrent_size As Double, ByVal torrent_dimension As String) As Double
        Dim bytes As Double
        Select Case UCase(torrent_dimension)
            Case "B"
                bytes = 1
            Case "KB"
                bytes = 1024
            Case "MB"
                bytes = 1024 ^ 2
            Case "GB"
                bytes = 1024 ^ 3
            Case "TB"
                bytes = 1024 ^ 4
            Case "PB"
                bytes = 1024 ^ 5
            Case "EB"
                bytes = 1024 ^ 6
            Case Else
                bytes = 0
        End Select

        Return torrent_size * bytes
    End Function

    Public Function GetHumanTime(ByVal total_seconds As Integer) As String
        Try
            Dim ret As String
            Dim minutes As Integer
            Dim seconds As Integer

            minutes = Math.DivRem(total_seconds, 60, seconds)

            If minutes = 0 Then
                ret = String.Format("{0} {1} назад", seconds, GetWordDeclension(CStr(seconds), "секунд"))
            Else
                ret = String.Format("{0} {1}, {2} {3} назад", minutes, GetWordDeclension(CStr(minutes), "минут"), seconds, GetWordDeclension(CStr(seconds), "секунд"))
            End If

            Return ret
        Catch ex As Exception
            Return "(ошибка)"
        End Try
    End Function

    Private Function GetWordDeclension(number As String, str As String, Optional one As String = "у", Optional two As String = "ы", Optional more As String = "") As String
        Try
            Dim ret As String
            Dim l As Byte = CByte(number.Substring(number.Length - 1, 1))
            Dim l2 As Byte = If(number.Length <= 2, CByte(number), CByte(number.Substring(number.Length - 2, 2)))

            If (l2 >= 5) And (l2 <= 20) Then
                ret = str & more
            Else
                Select Case l
                    Case 1
                        ret = str & one
                    Case 2, 3, 4
                        ret = str & two
                    Case Else
                        ret = str & more
                End Select
            End If

            Return ret
        Catch ex As Exception
            Return str
        End Try
    End Function

    Public Function ClearText(ByVal str As String) As String
        str = Regex.Replace(str, "(&nbsp;|&#8194;|&#8195;|&ensp;|&emsp;)", " ") 'remove special space characters
        str = Trim(WebUtility.HtmlDecode(str))
        '&#127954; --???

        Return str
    End Function
    Public Function EscapeLikeValue(ByVal str As String) As String
        Dim ret As String = ""

        For Each c As Char In str
            If c = "*" Or c = "%" Or c = "[" Or c = "]" Then
                ret &= "[" & c & "]"
            ElseIf c = "'" Then
                ret &= "''"
            Else
                ret &= c
            End If
        Next

        Return ret
    End Function
    Public Sub EnableDoubleBuffer(ByVal obj As Object)
        Dim objType As Type = obj.[GetType]()
        Dim pi As PropertyInfo = objType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        pi.SetValue(obj, True, Nothing)
    End Sub

    Public Function GetAssociatedProgram(ByVal file_extension As String) As String
        'returns the application associated with the specified extension
        Dim strExtValue As String
        Dim SplitArray() As String
        Try
            'Open registry areas containing launching app details
            Using objExtReg As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(file_extension)
                strExtValue = objExtReg.GetValue("")
                Using objAppReg As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(strExtValue & "\shell\open\command")
                    'Parse out, tidy up and return result
                    SplitArray = Split(objAppReg.GetValue(Nothing), """")

                    If Trim(SplitArray(0)).Length > 0 Then
                        Return SplitArray(0).Replace("%1", "")
                    Else
                        Return SplitArray(1).Replace("%1", "")
                    End If
                End Using
            End Using
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function GetTorrentName(ByVal torrent_body As Byte()) As String
        Try
            Dim torrent_body_string As String = Encoding.UTF8.GetString(torrent_body, 0, torrent_body.Length)
            Dim matches As Match = Regex.Match(torrent_body_string, ":name([\d]+):(.+)", RegexOptions.IgnoreCase, REGEX_TIMEOUT)
            Dim name_start_pos As Integer = matches.Groups(2).Index
            Dim name_length As Integer = CInt(matches.Groups(1).Value)
            Dim torrent_name As String

            torrent_name = torrent_body_string.Substring(name_start_pos, name_length)

            'cleaning, if any
            Dim pattern As String = New String(Path.GetInvalidFileNameChars())
            torrent_name = Regex.Replace(torrent_name, String.Format("[{0}]", Regex.Escape(pattern)), "_", RegexOptions.IgnoreCase, REGEX_TIMEOUT)

            Return torrent_name
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function GetProperExceptionText(ex As Exception) As String
        Dim ret As String
        If ex.InnerException IsNot Nothing Then
            ret = ex.Message & " " & ex.GetBaseException.Message '.ToString
        Else
            ret = ex.Message
        End If

        Return ret
    End Function

    Public Function GetKinozalInfoHash(ByVal str As String) As String
        Dim ret As String
        ret = Regex.Match(str, "<li>Инфо хеш: (.+?)</li>", RegexOptions.IgnoreCase, REGEX_TIMEOUT).Groups(1).Value

        If ret.Length = 0 Then
            Throw New Exception("Empty infohash.")
        End If

        Return ret
    End Function
End Module
