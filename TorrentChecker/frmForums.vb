Public Class frmForums
    Private ITEM_INDENT As String = "   "
    Private EnsureVisible_current As Integer = 0

    Private Sub frmForums_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = TRAY_ICON

        Try
            Dim cnt_formats As Integer = 0
            Dim formats_selected() As String = Nothing
            Dim cnt_forums As Integer = TrackerParams(TrackerSelectedID)("forums").Rows.Count()
            Dim forums_selected() As String = Split(ForumsSelected(TrackerSelectedID), ",")
            Dim item As ListViewItem

            txtFilter.Text = ""
            lsvForums.MultiSelect = (TrackerSelectedID = Trackers.rutracker)
            chkAutoLabel.Checked = False

            If TrackerSelectedID = Trackers.rutracker Then
                grpFormatsBox.Visible = False
                grpForumsBox.Height = 435
            Else
                grpFormatsBox.Visible = True
                grpForumsBox.Height = 244
                cnt_formats = TrackerParams(TrackerSelectedID)("formats").Rows.Count()
                formats_selected = Split(FormatsSelected(TrackerSelectedID), ",")
            End If

            'fill forums
            lsvForums.Clear()
            If cnt_forums > 0 Then
                lsvForums.Columns.Add("value", lsvForums.Width - 30)
                lsvForums.Columns.Add("key", 0)
                lsvForums.Columns.Add("root_forum", 0)
                lsvForums.Columns.Add("optgroup", 0)

                For Each dr As DataRow In TrackerParams(TrackerSelectedID)("forums").Select("", "id ASC")
                    If dr("optgroup").ToString <> "" Then
                        item = New ListViewItem(dr("optgroup").ToString) With {.Font = FONT_BOLD}
                    ElseIf dr("root_forum").ToString <> "" Then
                        item = New ListViewItem(ITEM_INDENT & dr("value").ToString) With {.Font = FONT_BOLD}
                        item.ForeColor = Color.Brown
                    Else
                        If dr("id") = 0 Then
                            item = New ListViewItem(dr("value").ToString) With {.Font = FONT_BOLD, .ForeColor = Color.Gray}
                        Else
                            item = New ListViewItem(ITEM_INDENT & dr("value").ToString) With {.Font = FONT_NORMAL}
                        End If
                    End If
                    item.SubItems.Add(dr("key").ToString)
                    item.SubItems.Add(dr("root_forum").ToString)
                    item.SubItems.Add(dr("optgroup").ToString)
                    lsvForums.Items.Add(item)
                    item.Selected = forums_selected.Contains(dr("key").ToString)
                Next
            End If

            'fill formats
            lsvFormats.Clear()
            If cnt_formats > 0 Then
                lsvFormats.Columns.Add("value", lsvFormats.Width - 30)
                lsvFormats.Columns.Add("key", 0)
                lsvFormats.Columns.Add("optgroup", 0)

                For Each dr As DataRow In TrackerParams(TrackerSelectedID)("formats").Select("", "id ASC")
                    If dr("optgroup").ToString <> "" Then
                        item = New ListViewItem(dr("value").ToString) With {.Font = FONT_BOLD}
                    Else
                        If dr("id") = 0 Then
                            item = New ListViewItem(dr("value").ToString) With {.Font = FONT_BOLD, .ForeColor = Color.Gray}
                        Else
                            item = New ListViewItem(ITEM_INDENT & dr("value").ToString) With {.Font = FONT_NORMAL}
                        End If
                    End If
                    item.SubItems.Add(dr("key").ToString)
                    item.SubItems.Add(dr("optgroup").ToString)
                    lsvFormats.Items.Add(item)
                    item.Selected = formats_selected.Contains(dr("key").ToString)
                Next
            End If

            If frmKeyWord.Visible = False Then
                'exit if user closed the frmKeyWord form when parsing is completed
                Me.Close()
            End If

            Me.Show()
            grpForumsBox.Text = "Выбранные форумы: " & lsvForums.SelectedItems.Count
            grpFormatsBox.Text = "Выбранные форматы: " & lsvFormats.SelectedItems.Count
            If cnt_forums > 0 Then lsvForums.EnsureVisible(lsvForums.SelectedIndices(0))
            If cnt_formats > 0 Then lsvFormats.EnsureVisible(lsvFormats.SelectedIndices(0))

            If lsvFormats.CanFocus Then lsvFormats.Focus()
            lsvForums.Focus()

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
        End Try
    End Sub

    Private Sub frmForums_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Hide()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Hide()
    End Sub

    Private Sub lsvForums_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lsvForums.ItemSelectionChanged
        'set optgroup non-clickable
        If (e.Item.SubItems(3).Text <> "") Then
            e.Item.Selected = False
        End If
        grpForumsBox.Text = "Выбранные форумы: " & lsvForums.SelectedItems.Count
    End Sub

    Private Sub btnAccept_Click(sender As Object, e As EventArgs) Handles btnAccept.Click
        If lsvForums.SelectedItems.Count > 200 Then
            'rutracker's limit
            MsgBox("Вы можете выбрать максимум 200 разделов!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            Exit Sub
        End If

        AutoLabel("enabled") = chkAutoLabel.Checked

        'save forums
        If lsvForums.SelectedItems.Count = 0 OrElse lsvForums.Items(0).Selected Then 'nothing selected, empty list or first item selected
            ForumsSelected(TrackerSelectedID) = If(TrackerSelectedID = Trackers.rutracker, "-1", "0")
            AutoLabel("value") = "Все форумы"
        Else
            ForumsSelected(TrackerSelectedID) = String.Join(",", From item As ListViewItem In lsvForums.SelectedItems Select item.SubItems(1).Text)
            AutoLabel("value") = String.Join(" / ", From item As ListViewItem In lsvForums.SelectedItems Select item.SubItems(0).Text.Trim.TrimStart("|", "-", " "))
        End If

        'save formats
        If TrackerSelectedID = Trackers.kinozal Then
            If lsvFormats.SelectedItems.Count = 0 OrElse lsvFormats.Items(0).Selected Then 'nothing selected, empty list or first item selected
                FormatsSelected(TrackerSelectedID) = "0"
                AutoLabel("value") &= " (Все форматы)"
            Else
                FormatsSelected(TrackerSelectedID) = String.Join(",", From item As ListViewItem In lsvFormats.SelectedItems Select item.SubItems(1).Text)
                AutoLabel("value") &= " (" & String.Join(" / ", From item As ListViewItem In lsvFormats.SelectedItems Select item.SubItems(0).Text.Trim.TrimStart("|", "-", " ")) & ")"
            End If
        End If

        Me.Hide()
    End Sub

    Private Sub lsvFormats_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lsvFormats.ItemSelectionChanged
        'set optgroup non-clickable
        If (e.Item.SubItems(2).Text <> "") Then
            e.Item.Selected = False
        End If
        grpFormatsBox.Text = "Выбранные форматы: " & lsvFormats.SelectedItems.Count
    End Sub

    Private Sub lsvForums_MouseMove(sender As Object, e As MouseEventArgs) Handles lsvForums.MouseMove
        If Not lsvForums.Focused AndAlso lsvForums.CanFocus Then lsvForums.Focus()
    End Sub

    Private Sub lsvFormats_MouseMove(sender As Object, e As MouseEventArgs) Handles lsvFormats.MouseMove
        If Not lsvFormats.Focused AndAlso lsvFormats.CanFocus Then lsvFormats.Focus()
    End Sub

    Private Sub txtFilter_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFilter.KeyDown
        'scroll to next selected item
        If Not e.KeyCode = Keys.Enter Then Exit Sub
        e.SuppressKeyPress = True

        Dim idx As Integer

        If lsvForums.SelectedIndices.Count > 0 Then
            If EnsureVisible_current = lsvForums.SelectedIndices(lsvForums.SelectedIndices.Count - 1) Then
                idx = lsvForums.SelectedIndices(0)
                lsvForums.EnsureVisible(idx)
                EnsureVisible_current = idx
                Exit Sub
            End If

            For Each idx In lsvForums.SelectedIndices
                If idx <= EnsureVisible_current Then Continue For
                lsvForums.EnsureVisible(idx)
                EnsureVisible_current = idx
                Exit For
            Next
        End If
    End Sub

    Private Sub txtFilter_MouseMove(sender As Object, e As MouseEventArgs) Handles txtFilter.MouseMove
        'If Not txtFilter.Focused AndAlso txtFilter.CanFocus Then txtFilter.Focus()
    End Sub

    Private Sub txtFilter_TextChanged(sender As Object, e As EventArgs) Handles txtFilter.TextChanged
        lsvForums.SelectedItems.Clear()
        If txtFilter.TextLength < 3 Or TrackerParams(TrackerSelectedID)("forums").Rows.Count() = 0 Then Exit Sub

        For Each dr As DataRow In TrackerParams(TrackerSelectedID)("forums").Select("value LIKE '*" & EscapeLikeValue(txtFilter.Text) & "*'", "id ASC")
            lsvForums.Items(dr("id")).Selected = True
            'kinozal does not support multiselect, so just exit after first match
            If TrackerSelectedID = Trackers.kinozal Then Exit For
        Next

        'scroll to first selected item
        If lsvForums.SelectedIndices.Count > 0 Then
            lsvForums.EnsureVisible(lsvForums.SelectedIndices(0))
            EnsureVisible_current = lsvForums.SelectedIndices(0)
        End If
    End Sub

    Public Sub New()
        InitializeComponent()
        For Each ctrl As Control In Me.Controls
            ctrl.Font = FONT_NORMAL
        Next

        tlpToolTip.SetToolTip(chkAutoLabel, "Позволяет автоматически сгенерировать ""Метку для отображения"" в окне ключевых слов (она будет переименована согласно выбранным форумам)")
    End Sub
End Class