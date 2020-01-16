Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading

Public Class frmMain
    Private HTTP As New clsHTTP
    Private HTTPResult As clsHTTP.HTTPResult
    Private HTTPUpdate As New clsHTTP
    Private HTTPUpdateResult As clsHTTP.HTTPResult
    Private TrackerParse As New clsParse
    Private TrackerParseResult As clsParse.ParseResult
    Private bsrcFoundTorrents As New BindingSource
    Private unread_errors_count As Integer = 0
    Private CaptchaParams As New Dictionary(Of String, Object) From
        {
            {"tracker_id", Trackers.rutracker},
            {"captcha_url", ""},
            {"captcha_sid", ""},
            {"captcha_code", ""}
        }
    Private isFormLoaded As Boolean = False
    Private isFormQueryToUnload As Boolean = False

    Public Sub New()
        InitializeComponent()
        For Each ctrl As Control In Me.Controls
            ctrl.Font = FONT_NORMAL
        Next

        lblShowNewVersion.Font = FONT_UNDERLINE
        msiNewPM.Font = FONT_BOLD
        msiPM_ru.Font = FONT_NORMAL
        msiPM_kz.Font = FONT_NORMAL
    End Sub

    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = CloseReason.UserClosing And AppOptions("close_to_tray") And Not isFormQueryToUnload Then
            AppHide(True)
            e.Cancel = True
            Exit Sub
        ElseIf e.CloseReason = CloseReason.UserClosing Then
            If AppOptions("confirm_exit") AndAlso MsgBox("Действительно хотите выйти из программы?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + vbDefaultButton2) = MsgBoxResult.No Then
                e.Cancel = True
                isFormQueryToUnload = False
                Exit Sub
            End If
        End If

        isFormQueryToUnload = True
        ApplicationExit()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            trayIcon.Text = APP_NAME
            trayIconPM.Text = APP_NAME & vbCrLf & "Eсть новые персональные сообщения!"

            HideCaptchaTab()
            AddHandler bsrcFoundTorrents.CurrentChanged, AddressOf bsrcFoundTorrents_CurrentChanged
            KwListToParamsMapping(0) = 0
            InitializeKeyWordsView()

            EnableDoubleBuffer(dgvTorrents)
            EnableDoubleBuffer(dgvKeyWords)

            'initialize dgvTorrents
            dgvTorrents.AutoGenerateColumns = False

            ReadSettings()

            'apply fonts
            dgvTorrents.DefaultCellStyle = DGV_NORMAL
            dgvKeyWords.DefaultCellStyle = DGV_NORMAL
            dgvKeyWords.Rows(0).DefaultCellStyle = DGV_BOLD

            'manual sorting
            dgvTorrents.Columns(Columns.found_datetime.ToString).SortMode = DataGridViewColumnSortMode.Programmatic
            dgvTorrents.Columns(Columns.torrent_size_dimension.ToString).SortMode = DataGridViewColumnSortMode.Programmatic

            bsrcFoundTorrents.DataSource = dtFoundTorrents
            RemoveHandler dgvTorrents.DataBindingComplete, AddressOf dgvTorrents_DataBindingComplete
            dgvTorrents.DataSource = bsrcFoundTorrents
            AddHandler dgvTorrents.DataBindingComplete, AddressOf dgvTorrents_DataBindingComplete

            For Each kw_id As KeyValuePair(Of Integer, Dictionary(Of String, String)) In KeyWordsParams
                If kw_id.Key = 0 Then
                    ShowCountOfNewTorrents(0)
                    Continue For
                End If
                Dim cnt As Integer = CInt(kw_id.Value("count_of_new"))
                RenameKWItem(kw_id.Key, kw_id.Value("label"), cnt)
            Next

            'restore default torrents sorting
            Dim virtual_sort_column As DataGridViewColumn = dgvTorrents.Columns(dgvTorrentsSorting("column_name"))
            Dim sort_direction_dgv As System.ComponentModel.ListSortDirection = If(dgvTorrentsSorting("sort_order") = SortOrder.Ascending, System.ComponentModel.ListSortDirection.Ascending, System.ComponentModel.ListSortDirection.Descending)
            dgvTorrents.Sort(virtual_sort_column, sort_direction_dgv)

            Me.Text = APP_NAME
            tssLabel.Text = ""

            'main form settings
            SetFormSettings()

            'check interval
            tmrStartChecking.Interval = CHECK_INTERVAL

            'show/hide journal
            If Not AppOptions("show_journal_tab") Then
                tbControl.TabPages.Remove(TabPage_journal)
            End If

            If AppOptions("auto_cheking") Then
                StartChecking()
            End If

            CheckLatestUpdate()
        Catch ex As Exception
            MsgBox("Ошибка загрузки программы!" & vbCrLf & GetProperExceptionText(ex), MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
        Finally
            isFormLoaded = True
        End Try
    End Sub

    Private Sub InitializeKeyWordsView()
        Dim img_col As New DataGridViewImageColumn
        With img_col
            .Name = "kw_img"
            .DataPropertyName = "kw_img"
            .ImageLayout = DataGridViewImageCellLayout.Zoom
            .DefaultCellStyle.NullValue = Nothing
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 25
        End With

        Dim kw_col As New DataGridViewTextBoxColumn
        With kw_col
            .DataPropertyName = "kw_item"
            .Name = "kw_item"
        End With

        dgvKeyWords.Columns.Add(img_col)
        dgvKeyWords.Columns.Add(kw_col)

        dgvKeyWords.Rows.Add(IMAGE_ALL_KW, KeyWordsParams(0)("label"))
        'dgvKeyWords.Rows(0).Cells("kw_item").Style.Font = FONT_BOLD
        dgvKeyWords.Rows(0).Cells("kw_item").Style.ForeColor = Color.Gray
    End Sub

    Private Sub SetFormSettings()
        With Me
            .Height = MainFormSettings("height")
            .Width = MainFormSettings("width")
            .Top = MainFormSettings("top")
            .Left = MainFormSettings("left")
            .WindowState = MainFormSettings("windowstate")
            scMain.SplitterDistance = MainFormSettings("splitter_distance")
        End With
    End Sub

    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        'check for credentials
        If TrackerParams(Trackers.rutracker)("credentials")("login_username") = "" AndAlso TrackerParams(Trackers.kinozal)("credentials")("username") = "" Then
            frmCredentials.ShowDialog()
            Me.BringToFront()
            'check again
            If TrackerParams(Trackers.rutracker)("credentials")("login_username") = "" AndAlso TrackerParams(Trackers.kinozal)("credentials")("username") = "" Then
                Exit Sub
            End If
        End If

        'check for keywords
        If UBound(KwListToParamsMapping) < 1 Then
            MsgBox("Добавьте как минимум одно ключевое слово!", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
            btnAddKeyWord_Click(sender, New EventArgs)
            'check again
            If UBound(KwListToParamsMapping) < 1 Then Exit Sub
        End If

        StartChecking()
    End Sub

    Private Sub ClearWorkingStatus()
        'clear all working status, except paused
        For Each dr As DataGridViewRow In dgvKeyWords.Rows
            If dr.Index = 0 Then Continue For
            If CBool(KeyWordsParams(KwListToParamsMapping(dr.Index))("paused")) Then Continue For
            dr.Cells("kw_img").Value = Nothing
        Next
    End Sub

    Private Async Sub StartChecking()
        Dim new_torrents_dt As DataTable
        Dim SearchParams As New Dictionary(Of String, String)
        Dim tracker_name As String
        Dim params_idx As Integer
        Dim local_KeyWordsParams As Dictionary(Of String, String)
        Dim operation_canceled As Boolean = False
        Dim kw_idx As Integer = 1
        Dim logged_result As LoggedResult

        'cancel all connections (if any)
        Dim cts As CancellationToken = HTTP.Cancel()

        Try
            'reset time counters
            tmrStartChecking.Enabled = False
            LAST_CHECK_TIME = UNIX_TIME_ORIGIN
            tssLabel.Text = "Идет проверка..."

            If APP_PAUSED Then
                'cancel pause
                AppPause(False, False)
            End If

            'reset failure flag
            TrackerParams(Trackers.rutracker)("is_tracker_failure") = False
            TrackerParams(Trackers.kinozal)("is_tracker_failure") = False

            ClearWorkingStatus()

            new_torrents_dt = dtFoundTorrents.Clone

            While kw_idx <= UBound(KwListToParamsMapping)

                If APP_PAUSED Then Exit While

                If CBool(KeyWordsParams(KwListToParamsMapping(kw_idx))("paused")) Then
                    kw_idx += 1
                    Continue While
                End If

                params_idx = KwListToParamsMapping(kw_idx)

                If ProxySettings("use_proxy") Then
                    HTTP.UseProxy = True
                    HTTP.SetProxy(ProxySettings)
                Else
                    HTTP.UseProxy = False
                End If

                'show working status image
                dgvKeyWords.Rows(kw_idx).Cells("kw_img").Value = IMAGE_CLOCK

                'store all variables (if user deletes the keyword item during global check)
                local_KeyWordsParams = New Dictionary(Of String, String)(KeyWordsParams(params_idx))

                new_torrents_dt.Clear()

                For Each tracker_id As Trackers In System.Enum.GetValues(GetType(Trackers))
                    tracker_name = TrackerParams(tracker_id)("tracker_name")
                    Try
                        If CBool(local_KeyWordsParams(tracker_id.ToString)) AndAlso Not TrackerParams(tracker_id)("is_tracker_failure") Then
                            If Not TrackerParams(tracker_id)("is_logged") Then
                                HTTPResult = Await HTTP.HTTPPost(TrackerParams(tracker_id)("login_url"), TrackerParams(tracker_id)("credentials"))
                                If HTTPResult.Success Then
                                    logged_result = CheckLogged(HTTPResult.Result, tracker_id)
                                    If logged_result.Status = LoggedResult.LoggedStatuses.no_logged Then
                                        TrackerParams(tracker_id)("is_tracker_failure") = True
                                        LogOutput(String.Format("{0} ({1})", "Ошибка авторизации", tracker_name), True, HTTPResult.Result)
                                    ElseIf logged_result.Status = LoggedResult.LoggedStatuses.need_captcha Then
                                        TrackerParams(tracker_id)("is_tracker_failure") = True
                                        LogOutput(String.Format("{0} ({1}): {2}", "Ошибка авторизации", tracker_name, "нужно ввести капчу"))

                                        ShowCaptchaTab(tracker_id, logged_result)
                                    Else
                                        'if the authorization was successful, check if the captcha is open
                                        HideCaptchaTab(tracker_id)
                                    End If
                                Else
                                    Exit For
                                End If
                            End If

                            If TrackerParams(tracker_id)("is_logged") Then

                                SearchParams.Clear()

                                If tracker_id = Trackers.rutracker Then
                                    SearchParams.Add("f", local_KeyWordsParams("forums_rutracker"))
                                    SearchParams.Add("tst", "-1")
                                    SearchParams.Add("o", "1")
                                    SearchParams.Add("s", "2")
                                    SearchParams.Add("tm", "-1")
                                    SearchParams.Add("pn", "")
                                    SearchParams.Add("nm", local_KeyWordsParams("search"))

                                    HTTPResult = Await HTTP.HTTPPost(TrackerParams(tracker_id)("search_url"), SearchParams)
                                ElseIf tracker_id = Trackers.kinozal Then
                                    SearchParams.Add("g", "0")
                                    SearchParams.Add("c", local_KeyWordsParams("forums_kinozal"))
                                    SearchParams.Add("v", local_KeyWordsParams("formats_kinozal"))
                                    SearchParams.Add("d", "0")
                                    SearchParams.Add("w", "0")
                                    SearchParams.Add("t", "0")
                                    SearchParams.Add("f", "0")
                                    SearchParams.Add("s", local_KeyWordsParams("search"))

                                    HTTPResult = Await HTTP.HTTPGet(TrackerParams(tracker_id)("search_url"), SearchParams)
                                End If

                                If HTTPResult.Success Then
                                    TrackerParseResult = TrackerParse.ParseResults(HTTPResult.Result, tracker_id)
                                    If TrackerParseResult.Status = clsParse.ParseResult.ParseStatuses.parse_ok Then
                                        If tracker_id = Trackers.kinozal Then
                                            'get correct torrent_unique_identifier. It is not stored in browse.php, so need send an HTTP request for each torrent.. :(
                                            For Each dr As DataRow In TrackerParseResult.Result.Select("")
                                                Try
                                                    HTTPResult = Await HTTP.HTTPGet(TrackerParams(tracker_id)("details_url") & dr(Columns.topic_id.ToString))
                                                    If HTTPResult.Success Then
                                                        dr(Columns.torrent_unique_identifier.ToString) = GetKinozalInfoHash(HTTPResult.Result)
                                                    Else
                                                        operation_canceled = True
                                                        Exit For
                                                    End If
                                                Catch ex As Exception
                                                    LogOutput(String.Format("{0} '{1}' ({2}): {3}", "Ошибка получения уникального идентификатора для", dr(Columns.topic_name.ToString), tracker_name, GetProperExceptionText(ex)))
                                                    TrackerParseResult.Result.Rows.Remove(dr)
                                                End Try
                                            Next
                                        End If

                                        If operation_canceled Then
                                            Exit For
                                        End If

                                        ShowPM(tracker_id, TrackerParseResult.GotPM)

                                        new_torrents_dt.Merge(TrackerParseResult.Result)
                                    ElseIf TrackerParseResult.Status = clsParse.ParseResult.ParseStatuses.forum_off Then
                                        TrackerParams(tracker_id)("is_tracker_failure") = True
                                        LogOutput(String.Format("{0} ({1}): {2}", "Ошибка парсинга", tracker_name, TrackerParseResult.Result.Rows(0)(Columns.forum_text.ToString)))
                                    Else
                                        CheckLogged(HTTPResult.Result, tracker_id)
                                        TrackerParams(tracker_id)("is_tracker_failure") = True
                                        LogOutput(String.Format("{0} ({1})", "Ошибка парсинга", tracker_name), True, HTTPResult.Result)
                                    End If
                                Else
                                    Exit For
                                End If
                            End If
                        End If
                    Catch ex As Exception
                        LogOutput(String.Format("{0} ({1}): {2}", "Ошибка проверки", tracker_name, GetProperExceptionText(ex)))
                    End Try
                Next

                If cts.IsCancellationRequested Then ClearWorkingStatus() : Exit While

                'remove outdated stored torrents
                Dim unix_date_time As Long = (DateTime.UtcNow - UNIX_TIME_ORIGIN).TotalSeconds
                For Each dr As DataRow In dtStoredTorrents.Select(unix_date_time & "-" & Columns.found_unix_datetime.ToString & ">" & TORRENTS_MAX_STORETIME)
                    dtStoredTorrents.Rows.Remove(dr)
                Next

                'join new and stored torrents for updating and deleting
                Dim matched_rows As IEnumerable(Of Object) = From new_t In new_torrents_dt.AsEnumerable()
                                                             Join old_t In dtStoredTorrents.AsEnumerable() On
                                                             new_t.Field(Of Trackers)(Columns.tracker_id.ToString) Equals old_t.Field(Of Trackers)(Columns.tracker_id.ToString) _
                                                             And new_t.Field(Of String)(Columns.topic_id.ToString) Equals old_t.Field(Of String)(Columns.topic_id.ToString) _
                                                             And new_t.Field(Of String)(Columns.torrent_unique_identifier.ToString) Equals old_t.Field(Of String)(Columns.torrent_unique_identifier.ToString)
                                                             Select new_t, old_t

                For Each dr As Object In matched_rows.ToList()
                    'update time
                    dr.old_t(Columns.found_unix_datetime.ToString) = dr.new_t(Columns.found_unix_datetime.ToString)
                    'remove already found torrents
                    new_torrents_dt.Rows.Remove(dr.new_t)
                Next

                'filters

                'filter non-dl torrents [disabled, show all torrents even if they cannot be downloaded]
                'For Each dr As DataRow In new_torrents_dt.Select(Columns.torrent_dl.ToString & "=''")
                'new_torrents_dt.Rows.Remove(dr)
                'Next

                'user-defined regex filter
                If CBool(local_KeyWordsParams("additional_filter")) Then
                    Try
                        Dim user_rg As New Regex(local_KeyWordsParams("additional_filter_text"), RegexOptions.Compiled + RegexOptions.IgnoreCase, REGEX_TIMEOUT)
                        For Each dr As DataRow In new_torrents_dt.Select("")
                            If Not user_rg.IsMatch(dr(Columns.topic_name.ToString).ToString) Then
                                new_torrents_dt.Rows.Remove(dr)
                            End If
                        Next
                    Catch ex As Exception
                        LogOutput(String.Format("{0} ({1}): {2}", "Ошибка выражения для ручного фильтра", local_KeyWordsParams("additional_filter_text"), GetProperExceptionText(ex)))
                    End Try
                End If

                'topic_id filter
                If local_KeyWordsParams("topic_id") <> "" Then
                    For Each dr As DataRow In new_torrents_dt.Select(Columns.topic_id.ToString & "<>" & local_KeyWordsParams("topic_id"))
                        new_torrents_dt.Rows.Remove(dr)
                    Next
                End If

                'correcting rows
                For Each dr As DataRow In new_torrents_dt.Rows
                    'calculate correct torrent size in bytes
                    dr(Columns.torrent_size_bytes.ToString) = GetTorrentSizeInBytes(dr(Columns.torrent_size.ToString), GetUniformDimension(dr(Columns.torrent_size_dimension.ToString)))
                    'combine torrent size and dimension
                    dr(Columns.torrent_size_dimension.ToString) = dr(Columns.torrent_size.ToString) & " " & GetUniformDimension(dr(Columns.torrent_size_dimension.ToString))
                    'set current datetime according to timezone
                    dr(Columns.found_datetime.ToString) = (UNIX_TIME_ORIGIN.AddSeconds(dr(Columns.found_unix_datetime.ToString)) + DateTimeOffset.Now.Offset).ToString(DATETIME_FORMAT)
                    'set keyword_id
                    dr(Columns.keyword_id.ToString) = params_idx
                    dr(Columns.marked_as_new.ToString) = True
                Next

                'torrent size filter (min)
                If local_KeyWordsParams("torrent_min_size") <> "0" Then
                    For Each dr As DataRow In new_torrents_dt.Select(Columns.torrent_size_bytes.ToString & "<" & CDbl(local_KeyWordsParams("torrent_min_size")) * 1024 ^ 3)
                        new_torrents_dt.Rows.Remove(dr)
                    Next
                End If
                'torrent size filter (max)
                If local_KeyWordsParams("torrent_max_size") <> "0" Then
                    For Each dr As DataRow In new_torrents_dt.Select(Columns.torrent_size_bytes.ToString & ">" & CDbl(local_KeyWordsParams("torrent_max_size")) * 1024 ^ 3)
                        new_torrents_dt.Rows.Remove(dr)
                    Next
                End If

                'remove more unnecessary column
                If new_torrents_dt.Columns.Contains(Columns.torrent_size.ToString) Then
                    new_torrents_dt.Columns.Remove(Columns.torrent_size.ToString)
                End If

                If KeyWordsParams.ContainsKey(params_idx) Then 'key still exists?
                    Dim kw_found_rows As Integer = 0
                    For Each dr As DataRow In dtFoundTorrents.Select(Columns.keyword_id.ToString & "=" & params_idx)
                        'set current datetime according to timezone for existing torrents
                        Dim current_datetime As String = (UNIX_TIME_ORIGIN.AddSeconds(dr(Columns.found_unix_datetime.ToString)) + DateTimeOffset.Now.Offset).ToString(DATETIME_FORMAT)
                        If dr(Columns.found_datetime.ToString) <> current_datetime Then
                            dr(Columns.found_datetime.ToString) = current_datetime
                        End If
                        kw_found_rows += 1
                    Next

                    If new_torrents_dt.Rows.Count > 0 Then
                        'new torrents found, check uniqueness
                        If kw_found_rows = 0 AndAlso CBool(KeyWordsParams(params_idx)("uniqueness")) Then
                            Dim dr_first As DataRow = new_torrents_dt.Copy.AsEnumerable.First
                            new_torrents_dt.Clear()
                            new_torrents_dt.ImportRow(dr_first)
                        ElseIf kw_found_rows <> 0 AndAlso CBool(KeyWordsParams(params_idx)("uniqueness")) Then
                            new_torrents_dt.Clear()
                        End If
                    End If

                    If new_torrents_dt.Rows.Count > 0 Then
                        'dtFoundTorrents.Merge(new_torrents_dt)
                        For Each dr As DataRow In new_torrents_dt.Rows
                            'using ImportRow instead of Merge saves row selection
                            dtFoundTorrents.ImportRow(dr)
                            kw_found_rows += 1
                        Next

                        'delete last N results
                        If kw_found_rows - TORRENTS_MAX_RESULTS > 0 Then
                            For Each dr As DataRow In dtFoundTorrents.Select(Columns.keyword_id.ToString & "=" & params_idx, Columns.found_unix_datetime.ToString).Take(kw_found_rows - TORRENTS_MAX_RESULTS)
                                dtFoundTorrents.Rows.Remove(dr)
                            Next
                        End If

                        dtStoredTorrents.Merge(new_torrents_dt, True, MissingSchemaAction.Ignore)

                        Dim cnt_new As Integer = dtFoundTorrents.Compute("COUNT(keyword_id)", String.Format("{0}={1} AND {2}=TRUE", Columns.keyword_id.ToString, params_idx, Columns.marked_as_new.ToString))
                        KeyWordsParams(params_idx)("count_of_new") = CStr(cnt_new)

                        ShowCountOfNewTorrents(params_idx)
                        Dim tray_message As String = String.Format("Метка: {0}{1}Новых: {2}{1}Всего непрочитанных: {3}{1}Последний найденный: {4}",
                                                                   KeyWordsParams(params_idx)("label"),
                                                                   vbCrLf,
                                                                   Math.Min(new_torrents_dt.Rows.Count, TORRENTS_MAX_RESULTS),
                                                                   KeyWordsParams(params_idx)("count_of_new"),
                                                                   (new_torrents_dt.Copy.AsEnumerable.First)(Columns.topic_name.ToString))
                        EnableTrayBlinking("Найдены новые торренты!", tray_message)
                    End If
                End If

                'download the torrent-file
                If KeyWordsParams.ContainsKey(params_idx) AndAlso CBool(KeyWordsParams(params_idx)("send_to_client")) Then
                    For Each dr As DataRow In new_torrents_dt.Select(Columns.torrent_dl.ToString & "<>''")
                        Dim tracker_id As Trackers = dr(Columns.tracker_id.ToString)
                        tracker_name = TrackerParams(tracker_id)("tracker_name")
                        Try
                            If KeyWordsParams.ContainsKey(params_idx) AndAlso CBool(KeyWordsParams(params_idx)("send_to_client")) Then 'user can change/delete the option/keyword at any time
                                Dim command_line As String = KeyWordsParams(params_idx)("client_command_line")
                                SearchParams.Clear()

                                If tracker_id = Trackers.rutracker Then
                                    SearchParams.Add("t", dr(Columns.torrent_dl.ToString))
                                    SearchParams.Add("form_token", TrackerParams(tracker_id)("forum_token"))

                                    HTTPResult = Await HTTP.HTTPPost(TrackerParams(tracker_id)("download_url") & dr(Columns.torrent_dl.ToString), SearchParams, True)
                                ElseIf tracker_id = Trackers.kinozal Then
                                    HTTPResult = Await HTTP.HTTPGet(TrackerParams(tracker_id)("download_url") & dr(Columns.torrent_dl.ToString), Nothing, True)
                                End If

                                If HTTPResult.Success Then
                                    If HTTPResult.Headers.ContentType.MediaType = "application/x-bittorrent" Then
                                        SaveTorrent(HTTPResult.ResultB, command_line, tracker_name)
                                    Else
                                        LogOutput(String.Format("{0} ({1}): {2}", "Ошибка скачивания торрент-файла", tracker_id.ToString,
                                                    "ожидался заголовок application/x-bittorrent, но получен " & HTTPResult.Headers.ContentType.MediaType),
                                                    True, Encoding.UTF8.GetString(HTTPResult.ResultB, 0, HTTPResult.ResultB.Length))
                                    End If
                                Else
                                    Exit For
                                End If
                            End If
                        Catch ex As Exception
                            LogOutput(String.Format("{0} ({1}): {2}", "Ошибка скачивания торрент-файла", tracker_name, GetProperExceptionText(ex)))
                        End Try
                    Next
                End If

                'clear status image and do correct increment method
                If KeyWordsParams.ContainsKey(params_idx) Then
                    For _i As Integer = 1 To UBound(KwListToParamsMapping)
                        If params_idx = KwListToParamsMapping(_i) Then
                            If Not CBool(KeyWordsParams(params_idx)("paused")) Then
                                dgvKeyWords.Rows(_i).Cells("kw_img").Value = Nothing
                            End If
                            kw_idx = _i + 1
                            Exit For
                        End If
                    Next
                End If

                local_KeyWordsParams.Clear()
                If cts.IsCancellationRequested Then Exit While
            End While
        Catch ex As Exception
            LogOutput(String.Format("{0}: {1}", "Ошибка проверки", GetProperExceptionText(ex)), True, ex.ToString)
            ClearWorkingStatus()
        Finally
            LAST_CHECK_TIME = DateTime.UtcNow

            If Not APP_PAUSED Then
                tmrStartChecking.Interval = CHECK_INTERVAL
                tmrStartChecking.Enabled = True
            End If

            If Not cts.IsCancellationRequested Then SaveSettings()
        End Try
    End Sub

    Private Sub SaveTorrent(ByVal torrent_body As Byte(), ByVal command_line As String, ByVal tracker_name As String)
        Dim default_torrent_client As String
        Dim tmp_filename As String = Path.GetTempFileName()
        Dim torrent_name As String = GetTorrentName(torrent_body)

        If torrent_name = "" Then
            LogOutput(String.Format("{0} с {1}: {2}", "Ошибка торрент-файла", tracker_name, "неверное имя"))
            Exit Sub
        End If

        Try
            'write torrent-file
            Using stream As Stream = File.Open(tmp_filename, FileMode.OpenOrCreate)
                Using writer As BinaryWriter = New BinaryWriter(stream)
                    writer.Write(torrent_body)
                End Using
            End Using
        Catch ex As Exception
            LogOutput(String.Format("{0} с {1}: {2}", "Ошибка записи при сохранении торрент-файла", tracker_name, GetProperExceptionText(ex)))
        End Try

        'check command line
        Try
            command_line = Replace(command_line, "%TorrentPath%", Chr(34) & tmp_filename & Chr(34))
            command_line = Replace(command_line, "%TorrentName%", torrent_name)
            If InStr(command_line, "%ClientPath%", CompareMethod.Text) <> 0 Then
                'send to default torrent-client
                default_torrent_client = GetAssociatedProgram(".torrent")
                If default_torrent_client <> "" Then
                    command_line = Replace(command_line, "%ClientPath%", Chr(34) & default_torrent_client & Chr(34))

                    Shell(command_line, AppWinStyle.NormalNoFocus)
                Else
                    LogOutput(String.Format("{0} с {1}: {2}", "Ошибка передачи торрент-файла", tracker_name, "не могу найти программу, ассоциированную с расширением .torrent"))
                End If

            Else
                'execute command line 'as is'
                Shell(command_line, AppWinStyle.NormalNoFocus)
            End If
        Catch ex As Exception
            LogOutput(String.Format("{0} с {1}: {2}", "Ошибка передачи торрент-файла в торрент-клиент", tracker_name, GetProperExceptionText(ex)))
        End Try
    End Sub

    Private Sub btnAddKeyWord_Click(sender As Object, e As EventArgs) Handles btnAddKeyWord.Click
        'add new keyword

        Dim kw_params_count As Integer = KeyWordsParams.Count
        Dim KeyWordsNewItemID As Integer

        KeyWordsSelectedItem = 0
        KeyWordsNewItemID = dgvKeyWords.Rows.Count
        frmKeyWord.ShowDialog()
        Me.BringToFront()

        If kw_params_count <> KeyWordsParams.Count Then 'new keyword was added
            If CBool(KeyWordsParams(KeyWordsParams.Keys.Max)("paused")) Then
                dgvKeyWords.Rows.Add(IMAGE_PAUSE, KeyWordsParams(KeyWordsParams.Keys.Max)("label"))
            Else
                dgvKeyWords.Rows.Add(Nothing, KeyWordsParams(KeyWordsParams.Keys.Max)("label"))
            End If
            dgvKeyWords.Rows(KeyWordsNewItemID).Cells("kw_item").Style.Padding = ITEM_PADDING
            grpKWBox.Text = "Ключевые фразы: " & KeyWordsNewItemID
            ReDim Preserve KwListToParamsMapping(KeyWordsNewItemID)
            KwListToParamsMapping(KeyWordsNewItemID) = KeyWordsParams.Keys.Max

            SaveSettings()
        End If
    End Sub

    Private Sub dgvKeyWords_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvKeyWords.KeyDown
        'delete selected keywords

        Dim idx As Integer
        If (Not e.KeyCode = Keys.Delete) Or (dgvKeyWords.SelectedRows.Count = 1 And dgvKeyWords.Rows(0).Selected) Then Exit Sub

        If dgvKeyWords.Rows(0).Selected Then dgvKeyWords.Rows(0).Selected = False

        If Not MsgBox("Хотите удалить выбранные ключевые слова из списка?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + vbDefaultButton2) = MsgBoxResult.Yes Then
            Exit Sub
        End If

        For n As Integer = dgvKeyWords.SelectedRows.Count - 1 To 0 Step -1
            idx = dgvKeyWords.SelectedRows(n).Index
            If idx = 0 Then Continue For 'do not delete the rool keyword

            KeyWordsParams.Remove(KwListToParamsMapping(idx))
            For Each dr As DataRow In dtFoundTorrents.Select(Columns.keyword_id.ToString & "=" & KwListToParamsMapping(idx))
                dtFoundTorrents.Rows.Remove(dr)
            Next

            If idx <> UBound(KwListToParamsMapping) Then 'not the last element
                For i As Integer = idx To UBound(KwListToParamsMapping) - 1
                    KwListToParamsMapping(i) = KwListToParamsMapping(i + 1) 'shift up
                Next
            End If

            ReDim Preserve KwListToParamsMapping(UBound(KwListToParamsMapping) - 1) 'delete the last element in the KwListToParamsMapping
            dgvKeyWords.Rows.RemoveAt(idx) 'delete element in the dgvKeyWords
        Next

        'recalculate
        KeyWordsParams(0)("count_of_new") = "0"
        ShowCountOfNewTorrents(0)
        grpKWBox.Text = "Ключевые фразы: " & dgvKeyWords.Rows.Count - 1

        'check for the auto_cheking option
        If AppOptions("auto_cheking") AndAlso UBound(KwListToParamsMapping) < 1 Then
            MsgBox("При отсутствии ключевых слов опция автопроверки на старте будет отключена", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
            AppOptions("auto_cheking") = False
        End If

        SaveSettings()
    End Sub

    Private Sub dgvKeyWords_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles dgvKeyWords.MouseDoubleClick
        Dim idx As Integer = dgvKeyWords.HitTest(e.X, e.Y).RowIndex

        If idx < 1 Then Exit Sub

        KeyWordsSelectedItem = idx
        Dim old_kw_paused_status As Boolean = CBool(KeyWordsParams(KwListToParamsMapping(KeyWordsSelectedItem))("paused"))
        frmKeyWord.ShowDialog()
        Me.BringToFront()

        Dim kw_id As Integer = KwListToParamsMapping(KeyWordsSelectedItem)
        Dim cnt As Integer = CInt(KeyWordsParams(kw_id)("count_of_new"))
        RenameKWItem(KeyWordsSelectedItem, KeyWordsParams(kw_id)("label"), cnt)

        If old_kw_paused_status <> CBool(KeyWordsParams(kw_id)("paused")) Then
            'change icon only if pause status has changed
            If CBool(KeyWordsParams(kw_id)("paused")) Then
                dgvKeyWords.Rows(idx).Cells("kw_img").Value = IMAGE_PAUSE
            Else
                dgvKeyWords.Rows(idx).Cells("kw_img").Value = Nothing
            End If
        End If

        SaveSettings()
    End Sub

    Private Sub dgvKeyWords_MouseDown(sender As Object, e As MouseEventArgs) Handles dgvKeyWords.MouseDown
        'select row under mouse cursor (right click)

        Dim idx As Integer = dgvKeyWords.HitTest(e.X, e.Y).RowIndex
        If idx = -1 Then Exit Sub

        If e.Button = Windows.Forms.MouseButtons.Right And dgvKeyWords.Rows(idx).Selected = False Then
            'prevent filter from firing
            RemoveHandler dgvKeyWords.SelectionChanged, AddressOf dgvKeyWords_SelectionChanged
            dgvKeyWords.ClearSelection()
            AddHandler dgvKeyWords.SelectionChanged, AddressOf dgvKeyWords_SelectionChanged
            dgvKeyWords.Rows(idx).Selected = True
        End If
    End Sub

    Private Sub dgvKeyWords_SelectionChanged(sender As Object, e As EventArgs) Handles dgvKeyWords.SelectionChanged
        'filter the torrent list
        If dgvKeyWords.SelectedRows.Count = 0 Then
            dgvKeyWords.Rows(0).Selected = True
            Exit Sub
        End If

        SetDefaultSearchText()

        Dim idx As Integer
        Try
            If dgvKeyWords.SelectedRows.Contains(dgvKeyWords.Rows(0)) Then
                'show full dataset
                bsrcFoundTorrents.RemoveFilter()
            Else
                'show selected torrents
                Dim idx_arr(dgvKeyWords.SelectedRows.Count - 1) As Integer
                For n As Integer = 0 To dgvKeyWords.SelectedRows.Count - 1
                    idx = dgvKeyWords.SelectedRows(n).Index
                    idx_arr(n) = KwListToParamsMapping(idx)
                Next

                bsrcFoundTorrents.Filter = String.Format("{0} IN ({1})", Columns.keyword_id.ToString, String.Join(",", idx_arr))
            End If

            If bsrcFoundTorrents.Count > 0 Then
                dgvTorrents.FirstDisplayedScrollingRowIndex = 0
            End If
        Catch ex As Exception
            MsgBox("Ошибка списка ключевых слов" & vbCrLf & GetProperExceptionText(ex), vbOKOnly + MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub dgvTorrents_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvTorrents.ColumnHeaderMouseClick
        If e.Button = Windows.Forms.MouseButtons.Left Then

            With dgvTorrents
                'make proper sorting for the found_datetime column
                If .Columns(Columns.found_datetime.ToString).Index = e.ColumnIndex Then
                    Dim virtual_sort_column As DataGridViewColumn = .Columns(Columns.found_unix_datetime.ToString)
                    Dim sort_direction As SortOrder = .Columns(e.ColumnIndex).HeaderCell.SortGlyphDirection
                    sort_direction = If(sort_direction = SortOrder.Ascending, SortOrder.Descending, SortOrder.Ascending)
                    Dim sort_direction_dgv As System.ComponentModel.ListSortDirection = If(sort_direction = SortOrder.Ascending, System.ComponentModel.ListSortDirection.Ascending, System.ComponentModel.ListSortDirection.Descending)
                    .Sort(virtual_sort_column, sort_direction_dgv)
                    'virtual_sort_column.HeaderCell.SortGlyphDirection = sort_direction
                    .Columns(e.ColumnIndex).HeaderCell.SortGlyphDirection = sort_direction
                End If

                'make proper sorting for the torrent_dimension column
                If .Columns(Columns.torrent_size_dimension.ToString).Index = e.ColumnIndex Then
                    Dim virtual_sort_column As DataGridViewColumn = .Columns(Columns.torrent_size_bytes.ToString)
                    Dim sort_direction As SortOrder = .Columns(e.ColumnIndex).HeaderCell.SortGlyphDirection
                    sort_direction = If(sort_direction = SortOrder.Ascending, SortOrder.Descending, SortOrder.Ascending)
                    Dim sort_direction_dgv As System.ComponentModel.ListSortDirection = If(sort_direction = SortOrder.Ascending, System.ComponentModel.ListSortDirection.Ascending, System.ComponentModel.ListSortDirection.Descending)
                    .Sort(virtual_sort_column, sort_direction_dgv)
                    'virtual_sort_column.HeaderCell.SortGlyphDirection = sort_direction
                    .Columns(e.ColumnIndex).HeaderCell.SortGlyphDirection = sort_direction
                End If
            End With
        ElseIf e.Button = Windows.Forms.MouseButtons.Right Then
            If cmsTorrentColumns.Items.Count = 0 Then
                For Each col As KeyValuePair(Of String, Object) In dgvTorrentsColumns
                    If col.Value("technical") Then Continue For
                    Dim menu As New ToolStripMenuItem() With {.Text = col.Value("name"), .Name = col.Key, .Checked = col.Value("visibility")}
                    cmsTorrentColumns.Items.Add(menu)
                Next
            End If
            cmsTorrentColumns.Show(Cursor.Position)
        End If
    End Sub

    Private Sub dgvTorrents_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvTorrents.DataBindingComplete
        If e.ListChangedType = System.ComponentModel.ListChangedType.ItemDeleted _
            Or isFormQueryToUnload Then
            'Or e.ListChangedType = System.ComponentModel.ListChangedType.ItemAdded _
            Exit Sub
        End If

        For Each dgvr As DataGridViewRow In dgvTorrents.Rows
            Dim is_row_marked_as_new As Boolean = dgvr.Cells(Columns.marked_as_new.ToString).Value
            Dim is_row_bold As Boolean = (dgvr.DefaultCellStyle Is DGV_BOLD)
            If is_row_marked_as_new AndAlso Not is_row_bold Then
                dgvr.DefaultCellStyle = DGV_BOLD
            End If

            If Not is_row_marked_as_new AndAlso is_row_bold Then
                dgvr.DefaultCellStyle = DGV_NORMAL
            End If
        Next

        'restore glyph for the found_datetime column or torrent_size_dimension (if it was selected)
        dgvTorrents.Columns(Columns.found_datetime.ToString).HeaderCell.SortGlyphDirection = dgvTorrents.Columns(Columns.found_unix_datetime.ToString).HeaderCell.SortGlyphDirection
        dgvTorrents.Columns(Columns.torrent_size_dimension.ToString).HeaderCell.SortGlyphDirection = dgvTorrents.Columns(Columns.torrent_size_bytes.ToString).HeaderCell.SortGlyphDirection
    End Sub

    Private Sub dgvTorrents_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvTorrents.KeyDown
        'delete selected torrents
        'recalculate new torrents

        Dim selected_rows As DataGridViewSelectedRowCollection = dgvTorrents.SelectedRows

        If (Not e.KeyCode = Keys.Delete) Or selected_rows.Count = 0 Then Exit Sub

        If Not MsgBox("Хотите удалить выбранные торренты из списка?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + vbDefaultButton2) = MsgBoxResult.Yes Then
            Exit Sub
        End If

        Dim cnt_new As String

        'physical delete
        For Each obj As Object In selected_rows.Cast(Of DataGridViewRow)().Select(Function(dgvr) dgvr.DataBoundItem)
            Dim drw As DataRowView = CType(obj, DataRowView)
            dtFoundTorrents.Rows.Remove(drw.Row)
        Next

        'recalculate
        For Each kw_id As Integer In KeyWordsParams.Keys
            If kw_id = 0 Then Continue For
            cnt_new = CStr(dtFoundTorrents.Compute("COUNT(keyword_id)", String.Format("{0}={1} AND {2}=TRUE", Columns.keyword_id.ToString, kw_id, Columns.marked_as_new.ToString)))
            KeyWordsParams(kw_id)("count_of_new") = cnt_new
            ShowCountOfNewTorrents(kw_id)
        Next

        SaveSettings()
    End Sub

    Private Sub dgvTorrents_MouseDown(sender As Object, e As MouseEventArgs) Handles dgvTorrents.MouseDown
        'select row under mouse cursor (right click)
        'mark the row as read
        'count new torrents

        Dim row_idx As Integer = dgvTorrents.HitTest(e.X, e.Y).RowIndex
        Dim col_idx As Integer = dgvTorrents.HitTest(e.X, e.Y).ColumnIndex

        If row_idx = -1 Then
            dgvTorrents.ClearSelection()
            Exit Sub
        End If

        If e.Button = Windows.Forms.MouseButtons.Right AndAlso dgvTorrents.Rows(row_idx).Selected = False Then
            dgvTorrents.ClearSelection()
            dgvTorrents.Rows(row_idx).Selected = True
            dgvTorrents.CurrentCell = dgvTorrents.Rows(row_idx).Cells(col_idx)
        End If

        Dim drw As DataRowView = CType(dgvTorrents.Rows(row_idx).DataBoundItem, DataRowView)
        Dim kw_id As Integer = drw.Row(Columns.keyword_id.ToString)

        If e.Button = Windows.Forms.MouseButtons.Left AndAlso drw.Row(Columns.marked_as_new.ToString) Then
            drw.Row(Columns.marked_as_new.ToString) = False
            KeyWordsParams(kw_id)("count_of_new") = CStr(CInt(KeyWordsParams(kw_id)("count_of_new")) - 1)
            ShowCountOfNewTorrents(kw_id)
        End If
    End Sub
    Private Sub dgvTorrents_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles dgvTorrents.MouseDoubleClick
        'open browser with the topic

        Try
            If dgvTorrents.Rows.Count = 0 Or dgvTorrents.HitTest(e.X, e.Y).RowIndex = -1 Then Exit Sub

            dgvTorrents.ClearSelection()
            dgvTorrents.CurrentRow.Selected = True
            Dim url As String = TrackerParams(dgvTorrents.CurrentRow.Cells(Columns.tracker_id.ToString).Value)("topic_url") & dgvTorrents.CurrentRow.Cells(Columns.topic_id.ToString).Value
            'open browser
            System.Diagnostics.Process.Start(url)
        Catch ex As Exception
            MsgBox("Не могу открыть браузер" & vbCrLf & GetProperExceptionText(ex), MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
        End Try
    End Sub

    Private Sub LogOutput(ByVal str As String, Optional ByVal isFatal As Boolean = False, Optional ByVal base_str As String = "")
        Dim current_length As Integer = rtbLogTextBox.TextLength
        Dim current_datetime As String = DateTime.Now.ToString(DATETIME_FORMAT)
        Dim formatted_str As String = String.Format("[{0}] {1}", current_datetime, str)

        formatted_str = If(current_length = 0, formatted_str, vbCrLf & formatted_str)
        rtbLogTextBox.AppendText(formatted_str)
        rtbLogTextBox.ScrollToCaret()

        Try
            If isFatal AndAlso AppOptions("write_fatal_errors") Then
                Using stream As StreamWriter = File.AppendText(LogPath)
                    stream.WriteLine(String.Format("=====LOG START [{0}] [v.{1}]=====", current_datetime, APP_VERSION))
                    stream.WriteLine(str)
                    stream.WriteLine("-----")
                    stream.WriteLine(base_str)
                    stream.WriteLine("-----")
                    stream.WriteLine("=====LOG END=====")
                End Using
            End If
        Catch ex As Exception
            LogOutput(String.Format("{0}: {1}", "Ошибка записи лог-файла", GetProperExceptionText(ex)))
        End Try

        If tbControl.SelectedIndex = 1 Then Exit Sub
        unread_errors_count += 1
        TabPage_journal.Text = "Журнал ошибок: новых " & CStr(unread_errors_count)
    End Sub

    Private Sub bsrcFoundTorrents_CurrentChanged(sender As Object, e As EventArgs)
        tbControl.TabPages(0).Text = "Найденные торренты: " & bsrcFoundTorrents.Count
    End Sub

    Private Sub ShowCountOfNewTorrents(ByVal kw_id As Integer)
        Dim all_unread_rows As Integer = 0
        Dim cnt As Integer = CInt(KeyWordsParams(kw_id)("count_of_new"))

        For i As Integer = 1 To UBound(KwListToParamsMapping)
            If kw_id = KwListToParamsMapping(i) Then
                RenameKWItem(i, KeyWordsParams(kw_id)("label"), cnt)
            End If
            'count all unread rows
            all_unread_rows += CInt(KeyWordsParams(KwListToParamsMapping(i))("count_of_new"))
        Next

        RenameKWItem(0, KeyWordsParams(0)("label"), all_unread_rows)
        KeyWordsParams(0)("count_of_new") = CStr(all_unread_rows)

        If all_unread_rows = 0 Then
            DisableTrayBlinking()
        End If
    End Sub

    Private Sub RenameKWItem(ByVal row_id As Integer, ByVal label As String, ByVal cnt As Integer)
        dgvKeyWords.Rows(row_id).Cells("kw_item").Value = If(cnt = 0, label, String.Format("{0} ({1})", label, cnt))

        If row_id <> 0 Then
            dgvKeyWords.Rows(row_id).DefaultCellStyle = If(cnt = 0, DGV_NORMAL, DGV_BOLD)
        End If
    End Sub

    Private Sub dgvTorrents_MouseMove(sender As Object, e As MouseEventArgs) Handles dgvTorrents.MouseMove
        If Not dgvTorrents.Focused AndAlso dgvTorrents.CanFocus Then dgvTorrents.Focus()
    End Sub

    Private Sub cmsiDeleteSelectedTorrents_Click(sender As Object, e As EventArgs) Handles cmsiDeleteSelectedTorrents.Click
        'delete selected torrents

        dgvTorrents_KeyDown(sender, New KeyEventArgs(Keys.Delete))
    End Sub

    Private Sub cmsiMarkSelectedAsRead_Click(sender As Object, e As EventArgs) Handles cmsiMarkSelectedAsRead.Click
        'mark selected torrents as read

        Dim selected_rows As DataGridViewSelectedRowCollection = dgvTorrents.SelectedRows
        Dim cnt_new As String

        For Each obj As Object In selected_rows.Cast(Of DataGridViewRow)().Select(Function(dgvr) dgvr.DataBoundItem)
            Dim drw As DataRowView = CType(obj, DataRowView)
            drw.Row(Columns.marked_as_new.ToString) = False
        Next

        'recalculate
        For Each kw_id As Integer In KeyWordsParams.Keys
            If kw_id = 0 Then Continue For
            cnt_new = CStr(dtFoundTorrents.Compute("COUNT(keyword_id)", String.Format("{0}={1} AND {2}=TRUE", Columns.keyword_id.ToString, kw_id, Columns.marked_as_new.ToString)))
            KeyWordsParams(kw_id)("count_of_new") = cnt_new
            ShowCountOfNewTorrents(kw_id)
        Next

        SaveSettings()
    End Sub

    Private Sub cmsTorrents_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmsTorrents.Opening
        Dim p As Point = dgvTorrents.PointToClient(Cursor.Position)
        Dim row_idx As Integer = dgvTorrents.HitTest(p.X, p.Y).RowIndex
        Dim col_idx As Integer = dgvTorrents.HitTest(p.X, p.Y).ColumnIndex

        If row_idx = -1 And col_idx <> -1 Then
            'do not show the menu if the user has clicked on the column header
            e.Cancel = True
            Exit Sub
        End If

        If dgvTorrents.SelectedRows.Count = 0 Then
            If dgvTorrents.Rows.Count <> 0 Then
                'hide unnecessary menu items
                For Each mnu As ToolStripItem In cmsTorrents.Items
                    mnu.Enabled = False
                Next
                cmsiMarkAllAsRead_t.Enabled = True
                cmsiClearAll_t.Enabled = True
            Else
                e.Cancel = True
            End If
        Else
            'restore menu state
            For Each mnu As ToolStripItem In cmsTorrents.Items
                mnu.Enabled = True
            Next
        End If
    End Sub

    Private Sub cmsiCopyLink_Click(sender As Object, e As EventArgs) Handles cmsiCopyLink.Click
        'copy links to clipboard

        Try
            Dim url As String
            url = String.Join(vbCrLf, From row As DataGridViewRow In dgvTorrents.SelectedRows Select TrackerParams(row.Cells(Columns.tracker_id.ToString).Value)("topic_url") & row.Cells(Columns.topic_id.ToString).Value)
            Clipboard.SetText(url)
        Catch ex As Exception
            MsgBox("Ошибка буфера обмена" & vbCrLf & GetProperExceptionText(ex), MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
        End Try
    End Sub

    Private Sub cmsiAddKeyWord_Click(sender As Object, e As EventArgs) Handles cmsiAddKeyWord.Click
        'add new keyword

        btnAddKeyWord_Click(sender, New EventArgs)
    End Sub

    Private Sub cmsiDeleteSelectedKW_Click(sender As Object, e As EventArgs) Handles cmsiDeleteSelectedKW.Click
        'delete selected keywords

        dgvKeyWords_KeyDown(sender, New KeyEventArgs(Keys.Delete))
    End Sub

    Private Sub cmsKeyWord_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmsKeyWord.Opening
        'prevent column header or 'empty' click

        Dim p As Point = dgvKeyWords.PointToClient(Cursor.Position)
        Dim idx As Integer = dgvKeyWords.HitTest(p.X, p.Y).RowIndex
        Dim cnt As Integer = dgvKeyWords.SelectedRows.Count
        Dim paused_cnt As Integer = 0
        Dim resumed_cnt As Integer = 0

        For Each dr_kw As DataGridViewRow In dgvKeyWords.SelectedRows
            If dr_kw.Index = 0 Then Continue For
            If CBool(KeyWordsParams(KwListToParamsMapping(dr_kw.Index))("paused")) Then
                paused_cnt += 1
            Else
                resumed_cnt += 1
            End If
        Next

        If idx = 0 And cnt > 1 Then
            cmsiDeleteSelectedKW.Enabled = True
        ElseIf idx > 0 Then
            cmsiDeleteSelectedKW.Enabled = True
        Else
            cmsiDeleteSelectedKW.Enabled = False
        End If

        If idx < 1 Or cnt > 1 Then
            cmsiEditKeyWord.Enabled = False
        Else
            cmsiEditKeyWord.Enabled = True
        End If

        If idx < 1 Then
            cmsiPause.Enabled = False
            cmsiResume.Enabled = False
        ElseIf paused_cnt > 0 And resumed_cnt = 0 Then
            cmsiPause.Enabled = False
            cmsiResume.Enabled = True
        ElseIf paused_cnt = 0 And resumed_cnt > 0 Then
            cmsiPause.Enabled = True
            cmsiResume.Enabled = False
        Else
            cmsiPause.Enabled = False
            cmsiResume.Enabled = False
        End If
    End Sub

    Private Sub cmsiEditKeyWord_Click(sender As Object, e As EventArgs) Handles cmsiEditKeyWord.Click
        'edit selected keyword

        Dim p As Point = dgvKeyWords.PointToClient(Me.cmsKeyWord.Bounds.Location)
        dgvKeyWords_MouseDoubleClick(sender, New MouseEventArgs(Windows.Forms.MouseButtons.Left, 2, p.X, p.Y, 0))
    End Sub

    Private Sub cmsiMarkAllAsRead_Click(sender As Object, e As EventArgs) Handles cmsiMarkAllAsRead.Click
        MarkAllAsRead()
    End Sub

    Private Sub MarkAllAsRead()
        'mark all torrents as read

        Dim kw_id As Integer

        SetDefaultSearchText()

        If bsrcFoundTorrents.Count > 0 Then

            For Each dr_kw As DataGridViewRow In dgvKeyWords.SelectedRows
                If dr_kw.Index = 0 Then 'root element selected
                    For Each dr As DataRow In dtFoundTorrents.Select(String.Format("{0}=TRUE", Columns.marked_as_new.ToString))
                        dr(Columns.marked_as_new.ToString) = False
                    Next

                    For i As Integer = 1 To UBound(KwListToParamsMapping)
                        kw_id = KwListToParamsMapping(i)
                        'recalculate
                        KeyWordsParams(kw_id)("count_of_new") = "0"
                        ShowCountOfNewTorrents(kw_id)
                    Next

                    Exit For
                End If

                kw_id = KwListToParamsMapping(dr_kw.Index)

                For Each dr As DataRow In dtFoundTorrents.Select(String.Format("{0}={1} AND {2}=TRUE", Columns.keyword_id.ToString, kw_id, Columns.marked_as_new.ToString))
                    dr(Columns.marked_as_new.ToString) = False
                Next

                'recalculate
                KeyWordsParams(kw_id)("count_of_new") = "0"
                ShowCountOfNewTorrents(kw_id)
            Next

            SaveSettings()
        End If
    End Sub

    Private Sub MarkAllAsUnRead()
        'mark all torrents as unread

        Dim kw_id As Integer
        Dim cnt_new As String

        SetDefaultSearchText()

        If bsrcFoundTorrents.Count > 0 Then

            For Each dr_kw As DataGridViewRow In dgvKeyWords.SelectedRows
                If dr_kw.Index = 0 Then 'root element selected
                    For Each dr As DataRow In dtFoundTorrents.Select(String.Format("{0}=FALSE", Columns.marked_as_new.ToString))
                        dr(Columns.marked_as_new.ToString) = True
                    Next

                    For i As Integer = 1 To UBound(KwListToParamsMapping)
                        kw_id = KwListToParamsMapping(i)
                        'recalculate
                        cnt_new = CStr(dtFoundTorrents.Compute("COUNT(keyword_id)", String.Format("{0}={1} AND {2}=TRUE", Columns.keyword_id.ToString, kw_id, Columns.marked_as_new.ToString)))
                        KeyWordsParams(kw_id)("count_of_new") = cnt_new
                        ShowCountOfNewTorrents(kw_id)
                    Next

                    Exit For
                End If

                kw_id = KwListToParamsMapping(dr_kw.Index)

                For Each dr As DataRow In dtFoundTorrents.Select(String.Format("{0}={1} AND {2}=FALSE", Columns.keyword_id.ToString, kw_id, Columns.marked_as_new.ToString))
                    dr(Columns.marked_as_new.ToString) = True
                Next

                'recalculate
                cnt_new = CStr(dtFoundTorrents.Compute("COUNT(keyword_id)", String.Format("{0}={1} AND {2}=TRUE", Columns.keyword_id.ToString, kw_id, Columns.marked_as_new.ToString)))
                KeyWordsParams(kw_id)("count_of_new") = cnt_new
                ShowCountOfNewTorrents(kw_id)

            Next

            SaveSettings()
        End If
    End Sub

    Private Sub cmsiClearAll_Click(sender As Object, e As EventArgs) Handles cmsiClearAll.Click
        ClearAll()
    End Sub

    Private Sub ClearAll()
        'delete all found torrents for selected keywords

        Dim kw_id As Integer

        SetDefaultSearchText()

        If Not MsgBox("Хотите удалить все торренты из списка?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + vbDefaultButton2) = MsgBoxResult.Yes Then
            Exit Sub
        End If

        If bsrcFoundTorrents.Count > 0 Then

            For Each dr_kw As DataGridViewRow In dgvKeyWords.SelectedRows
                If dr_kw.Index = 0 Then 'root element selected
                    dtFoundTorrents.Clear()

                    For i As Integer = 1 To UBound(KwListToParamsMapping)
                        kw_id = KwListToParamsMapping(i)
                        'recalculate
                        KeyWordsParams(kw_id)("count_of_new") = "0"
                        ShowCountOfNewTorrents(kw_id)
                    Next

                    Exit For
                End If

                kw_id = KwListToParamsMapping(dr_kw.Index)

                For Each dr As DataRow In dtFoundTorrents.Select(Columns.keyword_id.ToString & "=" & kw_id)
                    dtFoundTorrents.Rows.Remove(dr)
                Next

                'recalculate
                KeyWordsParams(kw_id)("count_of_new") = "0"
                ShowCountOfNewTorrents(kw_id)
            Next

            SaveSettings()
        End If
    End Sub

    Private Sub txtSearch_GotFocus(sender As Object, e As EventArgs) Handles txtSearch.GotFocus
        If txtSearch.Tag = "default_text_is_set" Then
            txtSearch.Text = ""
            txtSearch.Font = FONT_NORMAL
            txtSearch.Tag = ""
        End If
    End Sub

    Private Sub txtSearch_LostFocus(sender As Object, e As EventArgs) Handles txtSearch.LostFocus
        If txtSearch.TextLength = 0 Then
            SetDefaultSearchText()
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        'remove default text and set the filter

        If txtSearch.Tag = "default_text_is_set" Then Exit Sub

        Dim current_filter As String = Columns.topic_name.ToString & " LIKE '*" & EscapeLikeValue(txtSearch.Text) & "*'"
        If bsrcFoundTorrents.Filter <> "" Then
            Dim match As Match = Regex.Match(bsrcFoundTorrents.Filter, "^" & Columns.keyword_id.ToString & " IN \(.+?\)", RegexOptions.Singleline, REGEX_TIMEOUT)
            If match.Success Then
                current_filter = match.Groups(0).Value & " AND " & current_filter
            End If
        End If
        bsrcFoundTorrents.Filter = current_filter
    End Sub

    Private Sub SetDefaultSearchText()
        txtSearch.Text = ""
        txtSearch.Font = FONT_ITALIC
        txtSearch.Tag = "default_text_is_set"
        txtSearch.Text = "Фильтр по названию темы"
    End Sub

    Private Sub tmrStartChecking_Tick(sender As Object, e As EventArgs) Handles tmrStartChecking.Tick
        StartChecking()
    End Sub

    Private Sub msiCredentials_Click(sender As Object, e As EventArgs) Handles msiCredentials.Click
        frmCredentials.ShowDialog()
        Me.BringToFront()
    End Sub

    Private Sub tbControl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tbControl.SelectedIndexChanged
        If tbControl.SelectedTab Is TabPage_journal Then
            unread_errors_count = 0
            TabPage_journal.Text = "Журнал ошибок"
            txtSearch.Visible = False
        ElseIf tbControl.SelectedTab Is TabPage_captcha Then
            txtCaptcha.Text = ""
            txtCaptcha.Focus()
            txtSearch.Visible = False
        Else
            txtSearch.Visible = True
        End If
    End Sub

    Private Sub ShowCaptchaTab(ByVal tracker_id As Trackers, ByVal logged_result As LoggedResult)
        Dim tracker_name As String = TrackerParams(tracker_id)("tracker_name")

        If Not tbControl.TabPages.Contains(TabPage_captcha) Then
            tbControl.TabPages.Insert(tbControl.TabPages.Count, TabPage_captcha)
            CaptchaParams("tracker_id") = tracker_id
        End If

        If CaptchaParams("tracker_id") <> tracker_id Then
            'if the tracker asks for a captcha when it is already open by another tracker - exit
            Exit Sub
        End If

        CaptchaParams("captcha_url") = logged_result.captcha_url
        CaptchaParams("captcha_sid") = logged_result.captcha_sid
        CaptchaParams("captcha_code") = logged_result.captcha_code

        TabPage_captcha.Text = tracker_name & " запрашивает капчу"
        DrawCaptcha(tracker_name)
    End Sub

    Private Sub HideCaptchaTab(Optional ByVal tracker_id As Trackers = Nothing)
        If (CaptchaParams("tracker_id") = tracker_id Or tracker_id = Nothing) AndAlso tbControl.TabPages.Contains(TabPage_captcha) Then
            picCaptcha.Image.Dispose()
            tbControl.TabPages.Remove(TabPage_captcha)
        End If
    End Sub

    Private Async Sub DrawCaptcha(tracker_name As String)
        Dim CapHTTPResult As clsHTTP.HTTPResult

        If ProxySettings("use_proxy") Then
            HTTP.UseProxy = True
            HTTP.SetProxy(ProxySettings)
        Else
            HTTP.UseProxy = False
        End If

        Try
            picCaptcha.Image.Dispose()
            picCaptcha.Image = picCaptcha.InitialImage
            CapHTTPResult = Await HTTP.HTTPGet(CaptchaParams("captcha_url"), Nothing, True)
            If CapHTTPResult.Success Then
                Using memstr As MemoryStream = New MemoryStream(CapHTTPResult.ResultB)
                    picCaptcha.Image = Bitmap.FromStream(memstr)
                End Using
            End If
        Catch ex As Exception
            LogOutput(String.Format("{0} ({1}): {2}", "Ошибка загрузки капчи", tracker_name, GetProperExceptionText(ex)))
            picCaptcha.Image.Dispose()
        End Try
    End Sub

    Private Sub txtCaptcha_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCaptcha.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnSendCaptcha_Click(sender, New EventArgs)
        End If
    End Sub

    Private Sub txtCaptcha_LostFocus(sender As Object, e As EventArgs) Handles txtCaptcha.LostFocus
        txtCaptcha.Text.Trim()
    End Sub

    Private Async Sub btnSendCaptcha_Click(sender As Object, e As EventArgs) Handles btnSendCaptcha.Click
        Dim tracker_id As Trackers = CaptchaParams("tracker_id")
        Dim CapHTTPResult As clsHTTP.HTTPResult
        Dim CapPostParams As New Dictionary(Of String, String)(DirectCast(TrackerParams(tracker_id)("credentials"), Dictionary(Of String, String)))
        Dim logged_result As LoggedResult
        Dim tracker_name As String = TrackerParams(tracker_id)("tracker_name")

        If txtCaptcha.TextLength = 0 Then
            MsgBox("Введите капчу!", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
            txtCaptcha.Focus()
            Exit Sub
        End If

        If ProxySettings("use_proxy") Then
            HTTP.UseProxy = True
            HTTP.SetProxy(ProxySettings)
        Else
            HTTP.UseProxy = False
        End If

        Try
            Select Case tracker_id
                Case Trackers.rutracker
                    CapPostParams.Add("cap_sid", CaptchaParams("captcha_sid"))
                    CapPostParams.Add(CaptchaParams("captcha_code"), txtCaptcha.Text)
                    txtCaptcha.Text = ""
                    CapHTTPResult = Await HTTP.HTTPPost(TrackerParams(tracker_id)("login_url"), CapPostParams)
                    If CapHTTPResult.Success Then
                        logged_result = CheckLogged(CapHTTPResult.Result, tracker_id)
                        If logged_result.Status = LoggedResult.LoggedStatuses.no_logged Then
                            TrackerParams(tracker_id)("is_tracker_failure") = True
                            LogOutput(String.Format("{0} ({1})", "Ошибка авторизации", tracker_name), True, CapHTTPResult.Result)
                            HideCaptchaTab(tracker_id)
                        ElseIf logged_result.Status = LoggedResult.LoggedStatuses.need_captcha Then
                            TrackerParams(tracker_id)("is_tracker_failure") = True
                            LogOutput(String.Format("{0} ({1}): {2}", "Ошибка авторизации", tracker_name, "нужно ввести капчу"))
                            CaptchaParams("tracker_id") = tracker_id
                            CaptchaParams("captcha_url") = logged_result.captcha_url
                            CaptchaParams("captcha_sid") = logged_result.captcha_sid
                            CaptchaParams("captcha_code") = logged_result.captcha_code
                            DrawCaptcha(tracker_name)
                            MsgBox("Неверная капча! Проверьте правильность ввода и убедитесь, что установлены корректные логин и/или пароль", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
                            txtCaptcha.Focus()
                        Else
                            'authorization successful
                            HideCaptchaTab(tracker_id)
                            StartChecking()
                        End If
                    End If
            End Select
        Catch ex As Exception
            LogOutput(String.Format("{0} ({1}): {2}", "Ошибка загрузки капчи", tracker_name, GetProperExceptionText(ex)))
        End Try
    End Sub

    Private Sub tmrTray_Tick(sender As Object, e As EventArgs) Handles tmrTray.Tick
        If trayIcon.Icon Is EMPTY_ICON Then
            trayIcon.Icon = TRAY_ICON
        Else
            trayIcon.Icon = EMPTY_ICON
        End If
    End Sub

    Private Sub cmsiMarkAllAsRead_t_Click(sender As Object, e As EventArgs) Handles cmsiMarkAllAsRead_t.Click
        MarkAllAsRead()
    End Sub

    Private Sub cmsiClearAll_t_Click(sender As Object, e As EventArgs) Handles cmsiClearAll_t.Click
        ClearAll()
    End Sub

    Private Sub EnableTrayBlinking(ByVal title As String, ByVal text As String)
        'start flashing tray icon
        If APP_PAUSED Then Exit Sub

        title = title.Trim.Substring(0, If(title.Length > 100, 100, title.Length))
        text = text.Trim.Substring(0, If(text.Length > 1000, 1000, text.Length))
        trayIcon.ShowBalloonTip(5000, title, text, ToolTipIcon.Info)
        trayIcon.Text = APP_NAME & vbCrLf & title
        tmrTray.Enabled = True

        If AppOptions("play_sound") Then
            Media.SystemSounds.Exclamation.Play()
        End If
    End Sub

    Private Sub DisableTrayBlinking()
        'stop flashing tray icon
        tmrTray.Enabled = False
        trayIcon.Icon = TRAY_ICON
        trayIcon.Text = APP_NAME
    End Sub

    Private Sub ApplicationExit()
        HTTP.Cancel()
        HTTPUpdate.Cancel()
        picCaptcha.Image.Dispose()
        SaveSettings()
    End Sub

    Private Sub msiExit_Click(sender As Object, e As EventArgs) Handles msiExit.Click
        isFormQueryToUnload = True
        Me.Close()
    End Sub

    Private Sub cmsiPause_Click(sender As Object, e As EventArgs) Handles cmsiPause.Click
        For Each dr_kw As DataGridViewRow In dgvKeyWords.SelectedRows
            If dr_kw.Index = 0 Then Continue For
            KeyWordsParams(KwListToParamsMapping(dr_kw.Index))("paused") = CStr(True)
            dgvKeyWords.Rows(dr_kw.Index).Cells("kw_img").Value = IMAGE_PAUSE
        Next

        SaveSettings()
    End Sub

    Private Sub cmsiResume_Click(sender As Object, e As EventArgs) Handles cmsiResume.Click
        For Each dr_kw As DataGridViewRow In dgvKeyWords.SelectedRows
            If dr_kw.Index = 0 Then Continue For
            KeyWordsParams(KwListToParamsMapping(dr_kw.Index))("paused") = CStr(False)
            dgvKeyWords.Rows(dr_kw.Index).Cells("kw_img").Value = Nothing
        Next

        SaveSettings()
    End Sub

    Private Sub сmsiExit_Click(sender As Object, e As EventArgs) Handles сmsiExit.Click
        isFormQueryToUnload = True
        Me.Close()
    End Sub

    Private Sub tmrLastCheck_Tick(sender As Object, e As EventArgs) Handles tmrLastCheck.Tick
        If LAST_CHECK_TIME = UNIX_TIME_ORIGIN Then Exit Sub

        tssLabel.Text = "Последняя проверка была " & GetHumanTime((DateTime.UtcNow - LAST_CHECK_TIME).TotalSeconds)

        If APP_PAUSED Then
            tssLabel.Text &= " [Пауза]"
        End If
    End Sub

    Private Sub сmsiPause_Click(sender As Object, e As EventArgs) Handles сmsiPause.Click
        AppPause(Not APP_PAUSED, APP_PAUSED)
    End Sub

    Private Sub msiPause_Click(sender As Object, e As EventArgs) Handles msiPause.Click
        AppPause(Not APP_PAUSED, APP_PAUSED)
    End Sub

    Private Sub AppPause(value As Boolean, start_checking As Boolean)
        APP_PAUSED = value

        сmsiPause.Checked = value
        msiPause.Checked = value

        If APP_PAUSED Then
            tmrStartChecking.Enabled = False
            DisableTrayBlinking()
            trayIcon.Icon = TRAY_ICON_PAUSE
        Else
            trayIcon.Icon = TRAY_ICON
            If dgvKeyWords.Rows.Count > 1 Then
                If start_checking Then
                    StartChecking()
                End If
            End If
        End If
    End Sub

    Private Sub AppHide(value As Boolean)
        APP_HIDDEN = value

        If value Then
            msiHide.Text = "Показать"
            cmsiHide.Text = "Показать"
            Me.Hide()
        Else
            msiHide.Text = "Скрыть в трей"
            cmsiHide.Text = "Скрыть в трей"
            Me.Show()
            SetFormSettings()
            Me.Activate()
        End If
    End Sub

    Private Sub cmsiHide_Click(sender As Object, e As EventArgs) Handles cmsiHide.Click
        AppHide(Not APP_HIDDEN)
    End Sub

    Private Sub msiHide_Click(sender As Object, e As EventArgs) Handles msiHide.Click
        AppHide(Not APP_HIDDEN)
    End Sub

    Private Sub TrayIcon_MouseClick(sender As Object, e As MouseEventArgs) Handles trayIcon.MouseClick
        If Not e.Button = Windows.Forms.MouseButtons.Left Then Exit Sub

        AppHide(Not APP_HIDDEN)
    End Sub

    Private Sub cmsiLogSelectAll_Click(sender As Object, e As EventArgs) Handles cmsiLogSelectAll.Click
        rtbLogTextBox.Focus()
        rtbLogTextBox.SelectAll()
    End Sub

    Private Sub cmsiLogCopyText_Click(sender As Object, e As EventArgs) Handles cmsiLogCopyText.Click
        rtbLogTextBox.Focus()
        If rtbLogTextBox.SelectedText.Length = 0 Then
            rtbLogTextBox.SelectAll()
        End If
        rtbLogTextBox.Copy()
    End Sub

    Private Sub cmsiLogClear_Click(sender As Object, e As EventArgs) Handles cmsiLogClear.Click
        rtbLogTextBox.Clear()
    End Sub

    Private Sub frmMain_Move(sender As Object, e As EventArgs) Handles Me.Move
        SaveFormSettings()
    End Sub

    Private Sub frmMain_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If AppOptions("hide_to_tray") AndAlso Me.WindowState = FormWindowState.Minimized Then
            AppHide(True)
        End If

        SaveFormSettings()
    End Sub

    Private Sub SaveFormSettings()
        With Me
            If .WindowState = FormWindowState.Minimized Or isFormLoaded = False Then Exit Sub
            MainFormSettings("height") = .Height
            MainFormSettings("width") = .Width
            MainFormSettings("top") = .Top
            MainFormSettings("left") = .Left
            MainFormSettings("windowstate") = .WindowState
            MainFormSettings("splitter_distance") = scMain.SplitterDistance
        End With
    End Sub

    Private Sub msiAppOptions_Click(sender As Object, e As EventArgs) Handles msiAppOptions.Click
        frmAppOptions.ShowDialog()
        Me.BringToFront()

        If AppOptions("show_journal_tab") Then
            If Not tbControl.TabPages.Contains(TabPage_journal) Then
                tbControl.TabPages.Insert(tbControl.TabPages.Count, TabPage_journal)
            End If
        Else
            If tbControl.TabPages.Contains(TabPage_journal) Then
                tbControl.TabPages.Remove(TabPage_journal)
            End If
        End If
    End Sub

    Private Sub frmMain_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If AppOptions("run_hidden") Then Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub cmsTorrentColumns_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles cmsTorrentColumns.ItemClicked
        Dim item As ToolStripMenuItem = e.ClickedItem
        Dim cnt_unchecked As Byte = 0

        item.Checked = Not item.Checked

        For Each i As ToolStripMenuItem In cmsTorrentColumns.Items
            If Not i.Checked Then cnt_unchecked += 1
        Next

        If cnt_unchecked = cmsTorrentColumns.Items.Count Then
            item.Checked = Not item.Checked
            MsgBox("Нужно оставить хотя бы один видимый столбец!", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
            Exit Sub
        End If

        dgvTorrents.Columns(item.Name).Visible = item.Checked

        SaveSettings()
    End Sub

    Private Sub scMain_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles scMain.SplitterMoved
        SaveFormSettings()
    End Sub

    Private Sub msiHomePage_Click(sender As Object, e As EventArgs) Handles msiHomePage.Click
        'open browser with the forum

        Try
            System.Diagnostics.Process.Start(HOME_PAGE_URL)
        Catch ex As Exception
            MsgBox("Не могу открыть браузер" & vbCrLf & GetProperExceptionText(ex), MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
        End Try
    End Sub

    Private Sub msiAbout_Click(sender As Object, e As EventArgs) Handles msiAbout.Click
        MsgBox(APP_NAME & vbCrLf & "2019, hardhouse", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
    End Sub

    Private Sub dgvKeyWords_MouseMove(sender As Object, e As MouseEventArgs) Handles dgvKeyWords.MouseMove
        If Not dgvKeyWords.Focused AndAlso dgvKeyWords.CanFocus Then dgvKeyWords.Focus()
    End Sub

    Private Async Sub CheckLatestUpdate()
        Try
            If Not AppOptions("check_for_updates") Then Exit Sub

            HTTPUpdate.Cancel()

            If ProxySettings("use_proxy") Then
                HTTPUpdate.UseProxy = True
                HTTPUpdate.SetProxy(ProxySettings)
            Else
                HTTPUpdate.UseProxy = False
            End If

            HTTPUpdateResult = Await HTTPUpdate.HTTPGet(AppRepository("repository_api"))
            If HTTPUpdateResult.Success Then
                If HTTPUpdateResult.Result.Length <> 0 Then
                    Dim tag_name As String = Regex.Match(HTTPUpdateResult.Result, """tag_name"":""(.+?)""", RegexOptions.IgnoreCase, REGEX_TIMEOUT).Groups(1).Value
                    If tag_name.Length <> 0 AndAlso tag_name <> APP_VERSION Then
                        lblShowNewVersion.Visible = True
                        lblShowNewVersion.Text = String.Format("{0}: {1}. {2}: {3}", "Доступна новая версия", tag_name, "Текущая версия", APP_VERSION)
                    Else
                        lblShowNewVersion.Visible = False
                    End If
                End If
            End If
        Catch ex As Exception
            LogOutput(String.Format("{0}: {1}", "Ошибка проверки обновлений", GetProperExceptionText(ex)))
        End Try
    End Sub

    Private Sub tmrCheckUpdate_Tick(sender As Object, e As EventArgs) Handles tmrCheckUpdate.Tick
        CheckLatestUpdate()
    End Sub

    Private Sub lblShowNewVersion_Click(sender As Object, e As EventArgs) Handles lblShowNewVersion.Click
        'open browser with the repository

        Try
            System.Diagnostics.Process.Start(AppRepository("repository_url"))
        Catch ex As Exception
            MsgBox("Не могу открыть браузер" & vbCrLf & GetProperExceptionText(ex), MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
        End Try
    End Sub

    Private Sub ShowPM(ByVal tracker_id As Trackers, ByVal show As Boolean)
        'show/hide the 'new PM' indicator

        Dim tracker_name As String = TrackerParams(tracker_id)("tracker_name")
        Dim menu_text As String = "Новое персональное сообщение на " & tracker_name

        Select Case tracker_id
            Case Trackers.rutracker
                msiPM_ru.Text = menu_text
                msiPM_ru.Visible = show
            Case Trackers.kinozal
                msiPM_kz.Text = menu_text
                msiPM_kz.Visible = show
        End Select

        NewPMMenuVisible(tracker_id) = show
        ChangeVisibilityPM(NewPMMenuVisible(Trackers.rutracker) OrElse NewPMMenuVisible(Trackers.kinozal), menu_text)
    End Sub

    Private Sub MsiPM_ru_Click(sender As Object, e As EventArgs) Handles msiPM_ru.Click
        'hide menu item after click
        msiPM_ru.Visible = False
        NewPMMenuVisible(Trackers.rutracker) = False
        ChangeVisibilityPM(NewPMMenuVisible(Trackers.rutracker) OrElse NewPMMenuVisible(Trackers.kinozal))

        'open browser with the PM
        Try
            System.Diagnostics.Process.Start(TrackerParams(Trackers.rutracker)("pm_url"))
        Catch ex As Exception
            MsgBox("Не могу открыть браузер" & vbCrLf & GetProperExceptionText(ex), MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
        End Try
    End Sub

    Private Sub MsiPM_kz_Click(sender As Object, e As EventArgs) Handles msiPM_kz.Click
        'hide menu item after click
        msiPM_kz.Visible = False
        NewPMMenuVisible(Trackers.kinozal) = False
        ChangeVisibilityPM(NewPMMenuVisible(Trackers.rutracker) OrElse NewPMMenuVisible(Trackers.kinozal))

        'open browser with the PM
        Try
            System.Diagnostics.Process.Start(TrackerParams(Trackers.kinozal)("pm_url"))
        Catch ex As Exception
            MsgBox("Не могу открыть браузер" & vbCrLf & GetProperExceptionText(ex), MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
        End Try
    End Sub

    Private Sub ChangeVisibilityPM(ByVal show As Boolean, Optional ByVal text As String = "")
        'snow/hide PM menu
        msiNewPM.Visible = show

        'snow/hide tray PM icon
        If show Then
            If trayIconPM.Icon Is Nothing Then
                trayIconPM.Icon = TRAY_ICON_PM
                trayIconPM.ShowBalloonTip(5000, APP_NAME, text, ToolTipIcon.Info)
            End If
        Else
            trayIconPM.Icon = Nothing
        End If
    End Sub

    Private Sub TrayIconPM_MouseClick(sender As Object, e As MouseEventArgs) Handles trayIconPM.MouseClick
        If Not e.Button = Windows.Forms.MouseButtons.Left Then Exit Sub

        AppHide(Not APP_HIDDEN)
    End Sub

    Private Sub CmsiMarkSelectedAsUnRead_Click(sender As Object, e As EventArgs) Handles cmsiMarkSelectedAsUnRead.Click
        'mark selected torrents as unread

        Dim selected_rows As DataGridViewSelectedRowCollection = dgvTorrents.SelectedRows
        Dim cnt_new As String

        For Each obj As Object In selected_rows.Cast(Of DataGridViewRow)().Select(Function(dgvr) dgvr.DataBoundItem)
            Dim drw As DataRowView = CType(obj, DataRowView)
            drw.Row(Columns.marked_as_new.ToString) = True
        Next

        'recalculate
        For Each kw_id As Integer In KeyWordsParams.Keys
            If kw_id = 0 Then Continue For
            cnt_new = CStr(dtFoundTorrents.Compute("COUNT(keyword_id)", String.Format("{0}={1} AND {2}=TRUE", Columns.keyword_id.ToString, kw_id, Columns.marked_as_new.ToString)))
            KeyWordsParams(kw_id)("count_of_new") = cnt_new
            ShowCountOfNewTorrents(kw_id)
        Next

        SaveSettings()
    End Sub

    Private Sub CmsiMarkAllAsUnRead_t_Click(sender As Object, e As EventArgs) Handles cmsiMarkAllAsUnRead_t.Click
        MarkAllAsUnRead()
    End Sub

    Private Sub CmsiMarkAllAsUnRead_Click(sender As Object, e As EventArgs) Handles cmsiMarkAllAsUnRead.Click
        MarkAllAsUnRead()
    End Sub
End Class
