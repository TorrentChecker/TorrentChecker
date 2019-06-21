Imports System.Text.RegularExpressions

Public Class clsParse
    Class ParseResult
        Public Enum ParseStatuses
            parse_ok
            forum_off
            parse_error
        End Enum
        Public Property Result As DataTable
        Public Property Status As ParseStatuses
        Public Property GotPM As Boolean
    End Class
    Class ParseForumResult
        Public Enum ParseStatuses
            parse_ok
            parse_error
        End Enum
        Public Property Result As DataTable
        Public Property Status As ParseStatuses
    End Class

    Private ret_dt As New DataTable
    Private ret_result As New ParseResult
    Private ret_forums_dt As New DataTable
    Private ret_formats_dt As New DataTable
    Private ret_forums_result As New ParseForumResult
    Private ret_formats_result As New ParseForumResult

    Public Function ParseResults(ByVal str As String, ByVal tracker_id As Trackers) As ParseResult
        Dim match As Match, matches As MatchCollection, dr As DataRow
        Dim pattern, match_value As String
        Dim decimal_separator As String = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
        Dim column_idx As Integer
        Dim unix_date_time As Long = (DateTime.UtcNow - UNIX_TIME_ORIGIN).TotalSeconds

        Try
            ret_dt.Reset()
            ret_dt.Columns.Add(Columns.tracker_name.ToString)
            ret_dt.Columns.Add(Columns.tracker_id.ToString, GetType(Trackers))
            ret_dt.Columns.Add(Columns.found_unix_datetime.ToString, GetType(Long))
            ret_dt.Columns.Add(Columns.torrent_status.ToString)
            ret_dt.Columns.Add(Columns.forum_name.ToString)
            ret_dt.Columns.Add(Columns.topic_id.ToString)
            ret_dt.Columns.Add(Columns.topic_name.ToString)
            ret_dt.Columns.Add(Columns.topic_author.ToString)
            ret_dt.Columns.Add(Columns.torrent_dl.ToString)
            ret_dt.Columns.Add(Columns.torrent_size.ToString, GetType(Single))
            ret_dt.Columns.Add(Columns.torrent_size_dimension.ToString)
            ret_dt.Columns.Add(Columns.torrent_unique_identifier.ToString)

            Select Case tracker_id
                Case Trackers.rutracker
                    'check for PM
                    match = Regex.Match(str, "<a href=""privmsg.php\?folder=inbox"" class=""new-pm-link"">", RegexOptions.Singleline, REGEX_TIMEOUT)
                    ret_result.GotPM = match.Success

                    'get results
                    match = Regex.Match(str, "<table class=""forumline tablesorter"" id=""tor-tbl"">.*?</table>", RegexOptions.Singleline, REGEX_TIMEOUT)
                    If match.Success Then
                        ret_result.Status = ParseResult.ParseStatuses.parse_ok
                        'fill the forums datatable
                        If TrackerParams(tracker_id)("forums").Rows.Count() = 0 Then
                            Dim _retpf As ParseForumResult
                            _retpf = ParseForums(str, tracker_id)
                            If _retpf.Status = ParseForumResult.ParseStatuses.parse_ok Then
                                TrackerParams(tracker_id)("forums") = _retpf.Result.Copy
                            End If
                        End If
                        str = match.Value
                        'parse each line
                        pattern = "<tr.*?"
                        'torrent_status
                        pattern &= "<td.*?title=""(.*?)"""
                        'forum_name
                        pattern &= ".*?<td.*?<a.*?>(.*?)</a>"
                        'topic_id and topic_name
                        pattern &= ".*?<a.*?""viewtopic.php\?t=(.*?)"">(.*?)</a>"
                        'topic_author
                        pattern &= ".*?<a.*?>(.*?)</a>"
                        'torrent_dl
                        pattern &= ".*?<td.*?>\s+<u>\d+</u>\s+(?:<a.*?dl.php\?t=(.*?)"">)?"
                        'torrent_size
                        pattern &= "([\d.]+)"
                        'torrent_dimension
                        pattern &= "&nbsp;(.*?)(?:\s&#8595;)?(?:</a>)?\s+</td>"
                        'torrent_timestamp
                        pattern &= "\s+(?:<td.*?</td>\s+){3}\s+<td.*?>\s+<u>(\d+)</u>.*?</td>"
                        'end parsing
                        pattern &= ".*?</tr>"

                        matches = Regex.Matches(str, pattern, RegexOptions.Singleline, REGEX_TIMEOUT)
                        For Each m As Match In matches
                            dr = ret_dt.NewRow()
                            'some technical fields
                            dr(Columns.tracker_name.ToString) = TrackerParams(tracker_id)("tracker_name")
                            dr(Columns.tracker_id.ToString) = tracker_id
                            dr(Columns.found_unix_datetime.ToString) = unix_date_time

                            For i As Integer = 1 To m.Groups.Count - 1
                                match_value = ClearText(m.Groups(i).Value)
                                column_idx = i + 2 'skip technical fields
                                If ret_dt.Columns(column_idx).DataType = GetType(Single) Then
                                    'respect regional settings
                                    If decimal_separator = "," Then
                                        dr(column_idx) = CSng(Replace(match_value, ".", decimal_separator))
                                    Else
                                        dr(column_idx) = CSng(match_value)
                                    End If
                                ElseIf ret_dt.Columns(i - 1).DataType = GetType(Integer) Then
                                    dr(column_idx) = CInt(match_value)
                                Else
                                    dr(column_idx) = match_value
                                End If
                            Next
                            ret_dt.Rows.Add(dr)
                        Next
                    Else
                        'the forum may be temporary off
                        match = Regex.Match(str, "<div class=""msg-main"">(.*?)</div>", RegexOptions.Singleline, REGEX_TIMEOUT)
                        If match.Success Then
                            ret_result.Status = ParseResult.ParseStatuses.forum_off
                            ret_dt.Columns.Add(Columns.forum_text.ToString)
                            dr = ret_dt.NewRow()
                            'store the forum text
                            dr(Columns.forum_text.ToString) = ClearText(match.Groups(1).Value)
                            ret_dt.Rows.Add(dr)
                        Else
                            ret_result.Status = ParseResult.ParseStatuses.parse_error
                        End If
                    End If
                Case Trackers.kinozal
                    'check for PM
                    match = Regex.Match(str, "<a href='/inbox.php'>ЛС: <span class=""green"">", RegexOptions.Singleline, REGEX_TIMEOUT)
                    ret_result.GotPM = match.Success

                    'get results
                    match = Regex.Match(str, "<table class=""t_peer w100p"" cellspacing=0 cellpadding=0>.*?</table>", RegexOptions.Singleline, REGEX_TIMEOUT)
                    If match.Success Then
                        ret_result.Status = ParseResult.ParseStatuses.parse_ok
                        'fill the forums datatable
                        If TrackerParams(tracker_id)("forums").Rows.Count() = 0 Then
                            Dim _retpf As ParseForumResult
                            _retpf = ParseForums(str, tracker_id)
                            If _retpf.Status = ParseForumResult.ParseStatuses.parse_ok Then
                                TrackerParams(tracker_id)("forums") = _retpf.Result.Copy
                            End If
                        End If
                        'fill the formats datatable
                        If TrackerParams(tracker_id)("formats").Rows.Count() = 0 Then
                            Dim _retpfr As ParseForumResult
                            _retpfr = ParseFormats(str, tracker_id)
                            If _retpfr.Status = ParseForumResult.ParseStatuses.parse_ok Then
                                TrackerParams(tracker_id)("formats") = _retpfr.Result.Copy
                            End If
                        End If
                        str = match.Value
                        'parse each line
                        pattern = "<tr.*?"
                        'forum_name
                        pattern &= "<td.*?onclick=""cat\((.*?)\);"".*?</td>"
                        'topic_id, torrent_status and topic_name
                        pattern &= ".*?<a.*?""/details.php\?id=(.*?)""\sclass=""(.*?)"">(.*?)</a>"
                        'torrent_size and torrent_dimension
                        pattern &= "<td.*?</td>.*?<td.*?>([\d.]+)\s(.*?)</td>"
                        'torrent time
                        pattern &= ".*?<td.*?</td>.*?<td.*?</td>.*?<td.*?>(.*?)</td>"
                        'topic_author
                        pattern &= ".*?<a.*?>(.*?)</a>"
                        'end parsing
                        pattern &= ".*?</tr>"

                        matches = Regex.Matches(str, pattern, RegexOptions.Singleline, REGEX_TIMEOUT)
                        For Each m As Match In matches
                            dr = ret_dt.NewRow()
                            dr(Columns.tracker_name.ToString) = TrackerParams(tracker_id)("tracker_name")
                            dr(Columns.tracker_id.ToString) = tracker_id
                            dr(Columns.found_unix_datetime.ToString) = unix_date_time
                            If TrackerParams(tracker_id)("forums").Rows.Count() > 0 Then
                                Dim _retf() As DataRow = TrackerParams(tracker_id)("forums").Select("key='" & ClearText(m.Groups(1).Value) & "'")
                                If _retf.Count > 0 Then
                                    dr(Columns.forum_name.ToString) = _retf(0)("value")
                                End If
                            End If
                            dr(Columns.topic_id.ToString) = ClearText(m.Groups(2).Value)
                            dr(Columns.torrent_status.ToString) = GetTorrentStatus(ClearText(m.Groups(3).Value))
                            dr(Columns.topic_name.ToString) = ClearText(m.Groups(4).Value)
                            dr(Columns.torrent_unique_identifier.ToString) = ""
                            dr(Columns.topic_author.ToString) = ClearText(m.Groups(8).Value)
                            dr(Columns.torrent_dl.ToString) = dr(Columns.topic_id.ToString)
                            'respect regional settings
                            match_value = ClearText(m.Groups(5).Value)
                            If decimal_separator = "," Then
                                dr(Columns.torrent_size.ToString) = CDbl(Replace(match_value, ".", decimal_separator))
                            Else
                                dr(Columns.torrent_size.ToString) = CDbl(match_value)
                            End If
                            dr(Columns.torrent_size_dimension.ToString) = ClearText(m.Groups(6).Value)
                            ret_dt.Rows.Add(dr)
                        Next
                    Else
                        match = Regex.Match(str, "<div class='bx1 b center'><div class=pad10x10>", RegexOptions.Singleline, REGEX_TIMEOUT) 'nothing found
                        If match.Success Then
                            ret_result.Status = ParseResult.ParseStatuses.parse_ok
                        Else
                            ret_result.Status = ParseResult.ParseStatuses.parse_error
                        End If
                    End If
            End Select
            ret_result.Result = ret_dt

            Return ret_result
        Catch ex As RegexMatchTimeoutException
            ret_result.Status = ParseResult.ParseStatuses.parse_error
            Return ret_result
        End Try
    End Function

    Private Function GetTorrentStatus(ByVal status As String) As String
        Dim s As String
        Select Case UCase(status)
            Case "R2"
                s = "серебро"
            Case "R1"
                s = "золото"
            Case "R0"
                s = "стандарт"
            Case Else
                s = "стандарт"
        End Select

        Return s
    End Function

    Public Function ParseForums(ByVal str As String, ByVal tracker_id As Trackers) As ParseForumResult
        Dim match As Match, matches As MatchCollection
        Dim pattern As String, dr As DataRow, dc As New DataColumn

        Try
            ret_forums_dt.Reset()
            dc.AutoIncrement = True
            dc.ReadOnly = True
            dc.AutoIncrementSeed = 0
            dc.AutoIncrementStep = 1
            dc.ColumnName = "id"
            dc.DataType = GetType(Short)
            ret_forums_dt.Columns.Add(dc)
            ret_forums_dt.Columns.Add("key")
            ret_forums_dt.Columns.Add("value")
            ret_forums_dt.Columns.Add("root_forum")
            ret_forums_dt.Columns.Add("optgroup")

            Select Case tracker_id
                Case Trackers.rutracker
                    match = Regex.Match(str, "<select id=""fs-main"" name=""f\[\]"".*?>.*?</select>", RegexOptions.Singleline, REGEX_TIMEOUT)
                    If match.Success Then
                        ret_forums_result.Status = ParseForumResult.ParseStatuses.parse_ok
                        str = match.Value
                        'parse each line
                        pattern = "(?:<optgroup label=""(.*?)"">)?\s<option.*?"
                        'value
                        pattern &= "value=""(.*?)""(?:\sclass=""(root_forum)(?:\shas_sf)?"")?(?:\stitle=""(.*?)"")?>"
                        'forum_name
                        pattern &= "(.*?)"
                        'end parsing
                        pattern &= "</option>"
                        matches = Regex.Matches(str, pattern, RegexOptions.Singleline, REGEX_TIMEOUT)
                        For Each m As Match In matches
                            If ClearText(m.Groups(1).Value) <> "" Then
                                dr = ret_forums_dt.NewRow()
                                dr("optgroup") = ClearText(m.Groups(1).Value)
                                ret_forums_dt.Rows.Add(dr)
                            End If
                            dr = ret_forums_dt.NewRow()
                            dr("key") = ClearText(m.Groups(2).Value)
                            'get full-length string
                            dr("value") = If(ClearText(m.Groups(4).Value) <> "", "|- " & ClearText(m.Groups(4).Value), ClearText(m.Groups(5).Value))
                            dr("root_forum") = ClearText(m.Groups(3).Value)
                            ret_forums_dt.Rows.Add(dr)
                        Next
                    Else
                        ret_forums_result.Status = ParseForumResult.ParseStatuses.parse_error
                    End If
                Case Trackers.kinozal
                    match = Regex.Match(str, "<select name=""c"".*?>.*?</select>", RegexOptions.Singleline, REGEX_TIMEOUT)
                    If match.Success Then
                        ret_forums_result.Status = ParseForumResult.ParseStatuses.parse_ok
                        str = match.Value
                        'parse each line
                        pattern = "<option.*?"
                        'value
                        pattern &= "value=""?(.*?)""?(?:\sclass=.*?)?>"
                        'forum_name
                        pattern &= "(.*?)"
                        'end parsing
                        pattern &= "</option>"
                        matches = Regex.Matches(str, pattern, RegexOptions.Singleline, REGEX_TIMEOUT)
                        For Each m As Match In matches
                            dr = ret_forums_dt.NewRow()
                            dr("key") = ClearText(m.Groups(1).Value)
                            dr("value") = ClearText(m.Groups(2).Value)
                            ret_forums_dt.Rows.Add(dr)
                        Next
                    Else
                        ret_forums_result.Status = ParseForumResult.ParseStatuses.parse_error
                    End If
            End Select
            ret_forums_result.Result = ret_forums_dt

            Return ret_forums_result
        Catch ex As RegexMatchTimeoutException
            ret_forums_result.Status = ParseForumResult.ParseStatuses.parse_error
            Return ret_forums_result
        End Try
    End Function

    Public Function ParseFormats(ByVal str As String, ByVal tracker_id As Trackers) As ParseForumResult
        Dim match As Match, matches As MatchCollection
        Dim pattern As String, dr As DataRow, dc As New DataColumn

        Try
            ret_formats_dt.Reset()
            dc.AutoIncrement = True
            dc.ReadOnly = True
            dc.AutoIncrementSeed = 0
            dc.AutoIncrementStep = 1
            dc.ColumnName = "id"
            dc.DataType = GetType(Short)
            ret_formats_dt.Columns.Add(dc)
            ret_formats_dt.Columns.Add("key")
            ret_formats_dt.Columns.Add("value")
            ret_formats_dt.Columns.Add("optgroup")

            Select Case tracker_id
                Case Trackers.rutracker
                    'none
                Case Trackers.kinozal
                    match = Regex.Match(str, "<select name=""v"".*?>.*?</select>", RegexOptions.Singleline, REGEX_TIMEOUT)
                    If match.Success Then
                        ret_formats_result.Status = ParseForumResult.ParseStatuses.parse_ok
                        str = match.Value
                        'parse each line
                        pattern = "<option.*?"
                        'value
                        pattern &= "value=([\d]+)\s+(disabled)?.*?>"
                        'format_name
                        pattern &= "(.*?)"
                        'end parsing
                        pattern &= "</option>"
                        matches = Regex.Matches(str, pattern, RegexOptions.Singleline, REGEX_TIMEOUT)
                        For Each m As Match In matches
                            dr = ret_formats_dt.NewRow()
                            dr("key") = ClearText(m.Groups(1).Value)
                            dr("value") = ClearText(m.Groups(3).Value)
                            dr("optgroup") = ClearText(m.Groups(2).Value)
                            ret_formats_dt.Rows.Add(dr)
                        Next
                    Else
                        ret_formats_result.Status = ParseForumResult.ParseStatuses.parse_error
                    End If
            End Select
            ret_formats_result.Result = ret_formats_dt

            Return ret_formats_result
        Catch ex As RegexMatchTimeoutException
            ret_formats_result.Status = ParseForumResult.ParseStatuses.parse_error
            Return ret_formats_result
        End Try
    End Function
End Class
