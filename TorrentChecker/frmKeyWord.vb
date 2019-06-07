Imports System.Text.RegularExpressions
Public Class frmKeyWord
    Private HTTP As New clsHTTP
    Private HTTPResult As clsHTTP.HTTPResult
    Private TrackerParse As New clsParse
    Private ParseForumResult As clsParse.ParseForumResult

    Public Sub New()
        InitializeComponent()
        chkRuTracker.Text = TrackerParams(Trackers.rutracker)("tracker_name")
        chkKinozal.Text = TrackerParams(Trackers.kinozal)("tracker_name")

        lblTCCommandLineHelp.Text = "Следующие переменные подставятся автоматически:"
        lblTCCommandLineHelp.Text &= vbCrLf & "%ClientPath% - торрент-клиент по умолчанию"
        lblTCCommandLineHelp.Text &= vbCrLf & "%TorrentName% - имя торрент-файла"
        lblTCCommandLineHelp.Text &= vbCrLf & "%TorrentPath% - путь к скачанному торрент-файлу"

        For Each ctrl As Control In Me.Controls
            ctrl.Font = FONT_NORMAL
        Next
        lblRutrackerForumsCount.Font = FONT_ITALIC
        lblKinozalForumsCount.Font = FONT_ITALIC

        lblForumsRutracker.Font = FONT_UNDERLINE
        lblForumsKinozal.Font = FONT_UNDERLINE
        lblResetToUT.Font = FONT_UNDERLINE
        lblResetToQB.Font = FONT_UNDERLINE

        tlpToolTip.SetToolTip(chkAdditionalFilter, "Можно задать дополнительный фильтр для результирующего набора списка торрентов в Regex-формате (регистр букв не учитывается)")
        tlpToolTip.SetToolTip(txtAdditionalFilter, tlpToolTip.GetToolTip(chkAdditionalFilter))
        tlpToolTip.SetToolTip(txtKeyWord, "Введите фразу для поиска. Пустое поле означает ""Все торренты""")
        tlpToolTip.SetToolTip(chkUniqueness, "Режим поиска до первого совпадения. Все последующие найденные торренты для этого задания будут игнорироваться." & vbCrLf & _
                              "Если список найденных торрентов для этого ключевого слова будет очищен, задание переактивируется")
        tlpToolTip.SetToolTip(chkPause, "Включает или отключает пропуск задания при проверках")

    End Sub

    Private Async Function GetForumsList(tracker_id As Trackers) As Task
        Dim logged_result As LoggedResult

        Try
            HTTP.Cancel()
            If ProxySettings("use_proxy") Then
                HTTP.UseProxy = True
                HTTP.SetProxy(ProxySettings)
            Else
                HTTP.UseProxy = False
            End If
            HTTPResult = Await HTTP.HTTPPost(TrackerParams(tracker_id)("login_url"), TrackerParams(tracker_id)("credentials"))
            If HTTPResult.Success Then
                logged_result = CheckLogged(HTTPResult.Result, tracker_id)
                If logged_result.Status = LoggedResult.LoggedStatuses.logged Then
                    HTTPResult = Await HTTP.HTTPGet(TrackerParams(tracker_id)("search_url"))
                    If HTTPResult.Success Then
                        ParseForumResult = TrackerParse.ParseForums(HTTPResult.Result, tracker_id)
                        If ParseForumResult.Status = clsParse.ParseForumResult.ParseStatuses.parse_ok Then
                            TrackerParams(tracker_id)("forums") = ParseForumResult.Result.Copy
                            If tracker_id = Trackers.kinozal Then
                                ParseForumResult = TrackerParse.ParseFormats(HTTPResult.Result, tracker_id)
                                If ParseForumResult.Status = clsParse.ParseForumResult.ParseStatuses.parse_ok Then
                                    TrackerParams(tracker_id)("formats") = ParseForumResult.Result.Copy
                                Else
                                    MsgBox("Ошибка получения форматов", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                                End If
                            End If
                        Else
                            MsgBox("Ошибка получения форумов", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                        End If
                    End If
                Else
                    MsgBox("Ошибка авторизации", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
        End Try
    End Function

    Private Sub ResetFields()
        'reset all fields to defaults
        Me.Text = "Добавление ключевого слова"
        txtKeyWordLabel.Text = ""
        txtKeyWord.Text = ""
        chkAdditionalFilter.Checked = False
        txtAdditionalFilter.Text = ""
        txtAdditionalFilter.Enabled = chkAdditionalFilter.Checked
        txtTopicID.Text = ""
        txtTorrentMinSize.Text = "0"
        txtTorrentMaxSize.Text = "0"
        chkRuTracker.Checked = False
        chkKinozal.Checked = False
        lblForumsRutracker.Text = "Выбрать форумы"
        lblForumsKinozal.Text = "Выбрать форумы"
        lblForumsRutracker.Enabled = chkRuTracker.Checked
        lblRutrackerForumsCount.Enabled = chkRuTracker.Checked
        lblForumsKinozal.Enabled = chkKinozal.Checked
        lblKinozalForumsCount.Enabled = chkKinozal.Checked
        ForumsSelected(Trackers.rutracker) = "-1"
        ForumsSelected(Trackers.kinozal) = "0"
        FormatsSelected(Trackers.kinozal) = "0"
        lblRutrackerForumsCount.Visible = False
        lblKinozalForumsCount.Visible = False
        chkUniqueness.Checked = False
        chkPause.Checked = False
        chkSendToClient.Checked = False
        txtTCCommandLine.Enabled = chkSendToClient.Checked
        txtTCCommandLine.Text = "%ClientPath% /directory ""C:\PathToSave\%TorrentName%"" %TorrentPath%"
    End Sub
    Private Sub frmKeyWord_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Hide()
    End Sub

    Private Sub frmAddKeyWord_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim idx As Integer = KwListToParamsMapping(KeyWordsSelectedItem)

        Me.Icon = TRAY_ICON

        ResetFields()

        If KeyWordsSelectedItem > 0 Then
            'edit keyword
            Me.Text = "Редактирование ключевого слова"

            'grpFiltersBox
            txtKeyWordLabel.Text = KeyWordsParams(idx)("label")
            txtKeyWord.Text = KeyWordsParams(idx)("search")
            chkAdditionalFilter.Checked = CBool(KeyWordsParams(idx)("additional_filter"))
            txtAdditionalFilter.Text = KeyWordsParams(idx)("additional_filter_text")
            txtTopicID.Text = KeyWordsParams(idx)("topic_id")
            txtTorrentMinSize.Text = KeyWordsParams(idx)("torrent_min_size")
            txtTorrentMaxSize.Text = KeyWordsParams(idx)("torrent_max_size")
            'grpTrackersBox
            chkRuTracker.Checked = CBool(KeyWordsParams(idx)(Trackers.rutracker.ToString))
            chkKinozal.Checked = CBool(KeyWordsParams(idx)(Trackers.kinozal.ToString))
            'forums
            ForumsSelected(Trackers.rutracker) = KeyWordsParams(idx)("forums_rutracker")
            ForumsSelected(Trackers.kinozal) = KeyWordsParams(idx)("forums_kinozal")
            'formats
            FormatsSelected(Trackers.kinozal) = KeyWordsParams(idx)("formats_kinozal")
            'options
            chkUniqueness.Checked = CBool(KeyWordsParams(idx)("uniqueness"))
            chkPause.Checked = CBool(KeyWordsParams(idx)("paused"))
            'behavior
            chkSendToClient.Checked = CBool(KeyWordsParams(idx)("send_to_client"))
            txtTCCommandLine.Text = KeyWordsParams(idx)("client_command_line")

            ShowSelectedForumsCount()
        End If

        Me.Show()
        txtKeyWordLabel.Focus()
        txtKeyWordLabel.SelectAll()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        HTTP.Cancel()
        Me.Hide()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim idx As Integer = KwListToParamsMapping(KeyWordsSelectedItem)
        Dim new_idx As Integer = KeyWordsParams.Keys.Max + 1
        Dim kw_prop As New Dictionary(Of String, String)

        If Not MakeChecks() Then Exit Sub

        If KeyWordsSelectedItem = 0 Then
            'add new keyword

            'grpFiltersBox
            kw_prop.Add("label", txtKeyWordLabel.Text)
            kw_prop.Add("search", txtKeyWord.Text)
            kw_prop.Add("count_of_new", "0")
            kw_prop.Add("additional_filter", chkAdditionalFilter.Checked.ToString)
            kw_prop.Add("additional_filter_text", txtAdditionalFilter.Text)
            kw_prop.Add("topic_id", txtTopicID.Text)
            kw_prop.Add("torrent_min_size", txtTorrentMinSize.Text)
            kw_prop.Add("torrent_max_size", txtTorrentMaxSize.Text)
            'grpTrackersBox
            kw_prop.Add(Trackers.rutracker.ToString, chkRuTracker.Checked.ToString)
            kw_prop.Add(Trackers.kinozal.ToString, chkKinozal.Checked.ToString)
            'forums
            kw_prop.Add("forums_rutracker", ForumsSelected(Trackers.rutracker))
            kw_prop.Add("forums_kinozal", ForumsSelected(Trackers.kinozal))
            'formats
            kw_prop.Add("formats_kinozal", FormatsSelected(Trackers.kinozal))
            'options
            kw_prop.Add("uniqueness", chkUniqueness.Checked.ToString)
            kw_prop.Add("paused", chkPause.Checked.ToString)
            'behavior
            kw_prop.Add("send_to_client", chkSendToClient.Checked.ToString)
            kw_prop.Add("client_command_line", txtTCCommandLine.Text)

            KeyWordsParams.Add(new_idx, kw_prop)
        Else
            'save edited keyword

            'grpFiltersBox
            KeyWordsParams(idx)("label") = txtKeyWordLabel.Text
            KeyWordsParams(idx)("search") = txtKeyWord.Text
            KeyWordsParams(idx)("additional_filter") = chkAdditionalFilter.Checked.ToString
            KeyWordsParams(idx)("additional_filter_text") = txtAdditionalFilter.Text
            KeyWordsParams(idx)("topic_id") = txtTopicID.Text
            KeyWordsParams(idx)("torrent_min_size") = txtTorrentMinSize.Text
            KeyWordsParams(idx)("torrent_max_size") = txtTorrentMaxSize.Text
            'grpTrackersBox
            KeyWordsParams(idx)(Trackers.rutracker.ToString) = chkRuTracker.Checked.ToString
            KeyWordsParams(idx)(Trackers.kinozal.ToString) = chkKinozal.Checked.ToString
            'forums
            KeyWordsParams(idx)("forums_rutracker") = ForumsSelected(Trackers.rutracker)
            KeyWordsParams(idx)("forums_kinozal") = ForumsSelected(Trackers.kinozal)
            'formats
            KeyWordsParams(idx)("formats_kinozal") = FormatsSelected(Trackers.kinozal)
            'options
            KeyWordsParams(idx)("uniqueness") = chkUniqueness.Checked.ToString
            KeyWordsParams(idx)("paused") = chkPause.Checked.ToString
            'behavior
            KeyWordsParams(idx)("send_to_client") = chkSendToClient.Checked.ToString
            KeyWordsParams(idx)("client_command_line") = txtTCCommandLine.Text
        End If

        Me.Hide()
    End Sub

    Private Sub chkAdditionalFilter_CheckedChanged(sender As Object, e As EventArgs) Handles chkAdditionalFilter.CheckedChanged
        txtAdditionalFilter.Enabled = chkAdditionalFilter.Checked
        If txtAdditionalFilter.Enabled Then
            txtAdditionalFilter.SelectAll()
            txtAdditionalFilter.Focus()
        End If
    End Sub

    Private Sub chkRuTracker_CheckedChanged(sender As Object, e As EventArgs) Handles chkRuTracker.CheckedChanged
        If chkRuTracker.Checked And TrackerParams(Trackers.rutracker)("credentials")("login_username") = "" Then
            CredentialsControl_SELINDEX = 0
            frmCredentials.ShowDialog()
            Me.BringToFront()
            'check again
            If TrackerParams(Trackers.rutracker)("credentials")("login_username") = "" Then
                chkRuTracker.Checked = False
            End If
        End If
        lblForumsRutracker.Enabled = chkRuTracker.Checked
        lblRutrackerForumsCount.Enabled = chkRuTracker.Checked
    End Sub

    Private Sub chkKinozal_CheckedChanged(sender As Object, e As EventArgs) Handles chkKinozal.CheckedChanged
        If chkKinozal.Checked And TrackerParams(Trackers.kinozal)("credentials")("username") = "" Then
            CredentialsControl_SELINDEX = 1
            frmCredentials.ShowDialog()
            Me.BringToFront()
            'check again
            If TrackerParams(Trackers.kinozal)("credentials")("username") = "" Then
                chkKinozal.Checked = False
            End If
        End If
        lblForumsKinozal.Enabled = chkKinozal.Checked
        lblKinozalForumsCount.Enabled = chkKinozal.Checked
    End Sub

    Private Async Sub lblForumsRutracker_Click(sender As Object, e As EventArgs) Handles lblForumsRutracker.Click
        TrackerSelectedID = Trackers.rutracker

        If TrackerParams(TrackerSelectedID)("forums").Rows.Count = 0 Then
            grpTrackersBox.Enabled = False
            lblForumsRutracker.Font = FONT_NORMAL
            lblForumsRutracker.Text = "загружается список..."

            Await GetForumsList(TrackerSelectedID)

            lblForumsRutracker.Text = "Выбрать форумы"
            lblForumsRutracker.Font = FONT_UNDERLINE
            grpTrackersBox.Enabled = True
        End If

        If TrackerParams(TrackerSelectedID)("forums").Rows.Count <> 0 Then
            frmForums.ShowDialog()
            Me.BringToFront()
            ShowSelectedForumsCount()
        End If
    End Sub

    Private Async Sub lblForumsKinozal_Click(sender As Object, e As EventArgs) Handles lblForumsKinozal.Click
        TrackerSelectedID = Trackers.kinozal

        If TrackerParams(TrackerSelectedID)("forums").Rows.Count = 0 Then
            grpTrackersBox.Enabled = False
            lblForumsKinozal.Font = FONT_NORMAL
            lblForumsKinozal.Text = "загружается список..."

            Await GetForumsList(TrackerSelectedID)

            lblForumsKinozal.Text = "Выбрать форумы"
            lblForumsKinozal.Font = FONT_UNDERLINE
            grpTrackersBox.Enabled = True
        End If

        If TrackerParams(TrackerSelectedID)("forums").Rows.Count <> 0 Then
            frmForums.ShowDialog()
            Me.BringToFront()
            ShowSelectedForumsCount()
        End If
    End Sub

    Private Sub ShowSelectedForumsCount()
        Dim cnt As Integer 'forums count (for kinozal always 0 or 1 atm)
        Dim cnt_f As Integer 'formats count (always 0 or 1 atm)

        cnt = Split(ForumsSelected(Trackers.rutracker), ",").Count()
        If Split(ForumsSelected(Trackers.rutracker), ",").Contains("-1") Then cnt = 0

        If cnt > 0 Then
            lblRutrackerForumsCount.Text = "Выбрано форумов: " & cnt
            lblRutrackerForumsCount.Visible = True
        Else
            lblRutrackerForumsCount.Visible = False
        End If

        cnt = Split(ForumsSelected(Trackers.kinozal), ",").Count()
        cnt_f = Split(FormatsSelected(Trackers.kinozal), ",").Count()
        If Split(ForumsSelected(Trackers.kinozal), ",").Contains("0") Then cnt = 0
        If Split(FormatsSelected(Trackers.kinozal), ",").Contains("0") Then cnt_f = 0

        If cnt > 0 Or cnt_f > 0 Then
            If cnt > 0 And cnt_f > 0 Then
                lblKinozalForumsCount.Text = "Выбрано форумов: " & cnt & ", форматов: " & cnt_f
            ElseIf cnt > 0 And cnt_f = 0 Then
                lblKinozalForumsCount.Text = "Выбрано форумов: " & cnt
            Else
                lblKinozalForumsCount.Text = "Выбрано форматов: " & cnt_f
            End If
            lblKinozalForumsCount.Visible = True
        Else
            lblKinozalForumsCount.Visible = False
        End If
    End Sub

    Private Function MakeChecks() As Boolean
        Dim x As Integer 'dummy variable for Integer.TryParse

        If Trim(txtKeyWordLabel.Text) = "" Then
            MsgBox("Метка не может быть пустой!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            txtKeyWordLabel.Focus()
            Return False
        End If

        If chkRuTracker.Checked = False And chkKinozal.Checked = False Then
            MsgBox("Нужно выбрать хотя бы один трекер!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            Return False
        End If

        If chkAdditionalFilter.Checked Then
            Try
                Dim user_rg As New Regex(txtAdditionalFilter.Text, RegexOptions.IgnoreCase, REGEX_TIMEOUT)
            Catch ex As Exception
                MsgBox("Ошибка ручного фильтра: " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                txtAdditionalFilter.Focus()
                Return False
            End Try
        End If

        If txtTopicID.TextLength <> 0 AndAlso Not Integer.TryParse(txtTopicID.Text, x) Then
            MsgBox("Неправильный идентификатор темы. Он должен состоять только из цифр", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            txtTopicID.SelectAll()
            txtTopicID.Focus()
            Return False
        End If

        If Not Integer.TryParse(txtTorrentMinSize.Text, x) Then
            MsgBox("Неправильный минимальный размер торрента. Размер должен содержать только цифры", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            txtTorrentMinSize.SelectAll()
            txtTorrentMinSize.Focus()
            Return False
        End If

        If Not Integer.TryParse(txtTorrentMaxSize.Text, x) Then
            MsgBox("Неправильный максимальный размер торрента. Размер должен содержать только цифры", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            txtTorrentMaxSize.SelectAll()
            txtTorrentMaxSize.Focus()
            Return False
        End If

        If chkSendToClient.Checked AndAlso txtTCCommandLine.TextLength < 40 Then
            MsgBox("Командная строка для торрент-клиента слишком короткая! Пожалуйста, проверьте корректность написания", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            txtTCCommandLine.SelectAll()
            txtTCCommandLine.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub txtTopicID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTopicID.KeyPress
        If Not Char.IsDigit(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtTorrentMinSize_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTorrentMinSize.KeyPress
        If Not Char.IsDigit(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtTorrentMaxSize_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTorrentMaxSize.KeyPress
        If Not Char.IsDigit(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtTorrentMinSize_LostFocus(sender As Object, e As EventArgs) Handles txtTorrentMinSize.LostFocus
        If txtTorrentMinSize.TextLength = 0 Then txtTorrentMinSize.Text = "0"
    End Sub

    Private Sub txtTorrentMaxSize_LostFocus(sender As Object, e As EventArgs) Handles txtTorrentMaxSize.LostFocus
        If txtTorrentMaxSize.TextLength = 0 Then txtTorrentMaxSize.Text = "0"
    End Sub

    Private Sub chkSendToClient_CheckedChanged(sender As Object, e As EventArgs) Handles chkSendToClient.CheckedChanged
        txtTCCommandLine.Enabled = chkSendToClient.Checked
        If txtTCCommandLine.Enabled Then
            txtTCCommandLine.SelectAll()
            txtTCCommandLine.Focus()
        End If
    End Sub

    Private Sub lblResetToUT_Click(sender As Object, e As EventArgs) Handles lblResetToUT.Click
        Dim ret As MsgBoxResult
        ret = MsgBox("Восстановить исходную командную строку для uTorrent?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2)
        If ret = MsgBoxResult.No Then Exit Sub
        txtTCCommandLine.Text = "%ClientPath% /directory ""C:\PathToSave\%TorrentName%"" %TorrentPath%"
    End Sub

    Private Sub lblResetToQB_Click(sender As Object, e As EventArgs) Handles lblResetToQB.Click
        Dim ret As MsgBoxResult
        ret = MsgBox("Восстановить исходную командную строку для qBittorrent?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2)
        If ret = MsgBoxResult.No Then Exit Sub
        txtTCCommandLine.Text = "%ClientPath% --save-path=""C:\PathToSave"" --skip-dialog=true %TorrentPath%"
    End Sub

    Private Sub txtTCCommandLine_LostFocus(sender As Object, e As EventArgs) Handles txtTCCommandLine.LostFocus
        txtTCCommandLine.Text.Trim()
    End Sub

    Private Sub txtKeyWordLabel_LostFocus(sender As Object, e As EventArgs) Handles txtKeyWordLabel.LostFocus
        txtKeyWordLabel.Text.Trim()
    End Sub

    Private Sub txtKeyWord_LostFocus(sender As Object, e As EventArgs) Handles txtKeyWord.LostFocus
        txtKeyWord.Text.Trim()
    End Sub
End Class