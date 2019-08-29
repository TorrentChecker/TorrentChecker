<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAppOptions
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.chkHideToTray = New System.Windows.Forms.CheckBox()
        Me.chkCloseToTray = New System.Windows.Forms.CheckBox()
        Me.chkRunWithWindows = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCheckInterval = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.chkRunHidden = New System.Windows.Forms.CheckBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTorrentsStoreTime = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.grpTrayBox = New System.Windows.Forms.GroupBox()
        Me.grpRunBox = New System.Windows.Forms.GroupBox()
        Me.chkAutoChecking = New System.Windows.Forms.CheckBox()
        Me.gprOtherBox = New System.Windows.Forms.GroupBox()
        Me.chkCheckUpdates = New System.Windows.Forms.CheckBox()
        Me.chkConfirmExit = New System.Windows.Forms.CheckBox()
        Me.lblClearCache = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtMaxVisibleResults = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.chkWriteFatalErrors = New System.Windows.Forms.CheckBox()
        Me.chkShowJournalTab = New System.Windows.Forms.CheckBox()
        Me.tlpToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.lblChangeMainFont = New System.Windows.Forms.Label()
        Me.fntdMain = New System.Windows.Forms.FontDialog()
        Me.grpTrayBox.SuspendLayout()
        Me.grpRunBox.SuspendLayout()
        Me.gprOtherBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkHideToTray
        '
        Me.chkHideToTray.AutoSize = True
        Me.chkHideToTray.Location = New System.Drawing.Point(9, 25)
        Me.chkHideToTray.Name = "chkHideToTray"
        Me.chkHideToTray.Size = New System.Drawing.Size(126, 17)
        Me.chkHideToTray.TabIndex = 1
        Me.chkHideToTray.Text = "Сворачивать в трей"
        Me.chkHideToTray.UseVisualStyleBackColor = True
        '
        'chkCloseToTray
        '
        Me.chkCloseToTray.AutoSize = True
        Me.chkCloseToTray.Location = New System.Drawing.Point(9, 48)
        Me.chkCloseToTray.Name = "chkCloseToTray"
        Me.chkCloseToTray.Size = New System.Drawing.Size(117, 17)
        Me.chkCloseToTray.TabIndex = 2
        Me.chkCloseToTray.Text = "Закрывать в трей"
        Me.chkCloseToTray.UseVisualStyleBackColor = True
        '
        'chkRunWithWindows
        '
        Me.chkRunWithWindows.AutoSize = True
        Me.chkRunWithWindows.Location = New System.Drawing.Point(9, 25)
        Me.chkRunWithWindows.Name = "chkRunWithWindows"
        Me.chkRunWithWindows.Size = New System.Drawing.Size(175, 17)
        Me.chkRunWithWindows.TabIndex = 3
        Me.chkRunWithWindows.Text = "Запускать вместе с Windows"
        Me.chkRunWithWindows.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 119)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(110, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Интервал проверки:"
        '
        'txtCheckInterval
        '
        Me.txtCheckInterval.Location = New System.Drawing.Point(246, 116)
        Me.txtCheckInterval.MaxLength = 3
        Me.txtCheckInterval.Name = "txtCheckInterval"
        Me.txtCheckInterval.Size = New System.Drawing.Size(41, 20)
        Me.txtCheckInterval.TabIndex = 10
        Me.txtCheckInterval.Text = "5"
        Me.txtCheckInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(293, 119)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "минут"
        '
        'chkRunHidden
        '
        Me.chkRunHidden.AutoSize = True
        Me.chkRunHidden.Location = New System.Drawing.Point(9, 48)
        Me.chkRunHidden.Name = "chkRunHidden"
        Me.chkRunHidden.Size = New System.Drawing.Size(138, 17)
        Me.chkRunHidden.TabIndex = 4
        Me.chkRunHidden.Text = "Запускать свернутым"
        Me.chkRunHidden.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(245, 441)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(98, 26)
        Me.btnSave.TabIndex = 13
        Me.btnSave.Text = "Сохранить"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(349, 441)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(98, 26)
        Me.btnCancel.TabIndex = 14
        Me.btnCancel.Text = "Отмена"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 142)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(198, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Срок хранения найденных торрентов:"
        '
        'txtTorrentsStoreTime
        '
        Me.txtTorrentsStoreTime.Location = New System.Drawing.Point(246, 139)
        Me.txtTorrentsStoreTime.MaxLength = 2
        Me.txtTorrentsStoreTime.Name = "txtTorrentsStoreTime"
        Me.txtTorrentsStoreTime.Size = New System.Drawing.Size(41, 20)
        Me.txtTorrentsStoreTime.TabIndex = 11
        Me.txtTorrentsStoreTime.Text = "30"
        Me.txtTorrentsStoreTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(293, 142)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(31, 13)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "дней"
        '
        'grpTrayBox
        '
        Me.grpTrayBox.Controls.Add(Me.chkCloseToTray)
        Me.grpTrayBox.Controls.Add(Me.chkHideToTray)
        Me.grpTrayBox.Location = New System.Drawing.Point(12, 12)
        Me.grpTrayBox.Name = "grpTrayBox"
        Me.grpTrayBox.Size = New System.Drawing.Size(435, 82)
        Me.grpTrayBox.TabIndex = 13
        Me.grpTrayBox.TabStop = False
        Me.grpTrayBox.Text = "Трей"
        '
        'grpRunBox
        '
        Me.grpRunBox.Controls.Add(Me.chkAutoChecking)
        Me.grpRunBox.Controls.Add(Me.chkRunWithWindows)
        Me.grpRunBox.Controls.Add(Me.chkRunHidden)
        Me.grpRunBox.Location = New System.Drawing.Point(12, 100)
        Me.grpRunBox.Name = "grpRunBox"
        Me.grpRunBox.Size = New System.Drawing.Size(435, 105)
        Me.grpRunBox.TabIndex = 14
        Me.grpRunBox.TabStop = False
        Me.grpRunBox.Text = "Запуск"
        '
        'chkAutoChecking
        '
        Me.chkAutoChecking.AutoSize = True
        Me.chkAutoChecking.Location = New System.Drawing.Point(9, 71)
        Me.chkAutoChecking.Name = "chkAutoChecking"
        Me.chkAutoChecking.Size = New System.Drawing.Size(155, 17)
        Me.chkAutoChecking.TabIndex = 5
        Me.chkAutoChecking.Text = "Сразу начинать проверку"
        Me.chkAutoChecking.UseVisualStyleBackColor = True
        '
        'gprOtherBox
        '
        Me.gprOtherBox.Controls.Add(Me.lblChangeMainFont)
        Me.gprOtherBox.Controls.Add(Me.chkCheckUpdates)
        Me.gprOtherBox.Controls.Add(Me.chkConfirmExit)
        Me.gprOtherBox.Controls.Add(Me.lblClearCache)
        Me.gprOtherBox.Controls.Add(Me.Label6)
        Me.gprOtherBox.Controls.Add(Me.txtMaxVisibleResults)
        Me.gprOtherBox.Controls.Add(Me.Label5)
        Me.gprOtherBox.Controls.Add(Me.chkWriteFatalErrors)
        Me.gprOtherBox.Controls.Add(Me.chkShowJournalTab)
        Me.gprOtherBox.Controls.Add(Me.Label1)
        Me.gprOtherBox.Controls.Add(Me.txtCheckInterval)
        Me.gprOtherBox.Controls.Add(Me.Label2)
        Me.gprOtherBox.Controls.Add(Me.Label4)
        Me.gprOtherBox.Controls.Add(Me.Label3)
        Me.gprOtherBox.Controls.Add(Me.txtTorrentsStoreTime)
        Me.gprOtherBox.Location = New System.Drawing.Point(12, 211)
        Me.gprOtherBox.Name = "gprOtherBox"
        Me.gprOtherBox.Size = New System.Drawing.Size(435, 220)
        Me.gprOtherBox.TabIndex = 15
        Me.gprOtherBox.TabStop = False
        Me.gprOtherBox.Text = "Разное"
        '
        'chkCheckUpdates
        '
        Me.chkCheckUpdates.AutoSize = True
        Me.chkCheckUpdates.Location = New System.Drawing.Point(9, 71)
        Me.chkCheckUpdates.Name = "chkCheckUpdates"
        Me.chkCheckUpdates.Size = New System.Drawing.Size(144, 17)
        Me.chkCheckUpdates.TabIndex = 8
        Me.chkCheckUpdates.Text = "Проверять обновления"
        Me.chkCheckUpdates.UseVisualStyleBackColor = True
        '
        'chkConfirmExit
        '
        Me.chkConfirmExit.AutoSize = True
        Me.chkConfirmExit.Location = New System.Drawing.Point(9, 94)
        Me.chkConfirmExit.Name = "chkConfirmExit"
        Me.chkConfirmExit.Size = New System.Drawing.Size(237, 17)
        Me.chkConfirmExit.TabIndex = 9
        Me.chkConfirmExit.Text = "Запрашивать подтверждение при выходе"
        Me.chkConfirmExit.UseVisualStyleBackColor = True
        '
        'lblClearCache
        '
        Me.lblClearCache.AutoSize = True
        Me.lblClearCache.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblClearCache.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblClearCache.Location = New System.Drawing.Point(338, 142)
        Me.lblClearCache.Name = "lblClearCache"
        Me.lblClearCache.Size = New System.Drawing.Size(77, 13)
        Me.lblClearCache.TabIndex = 18
        Me.lblClearCache.Text = "Очистить кэш"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(293, 166)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 13)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "результатов"
        '
        'txtMaxVisibleResults
        '
        Me.txtMaxVisibleResults.Location = New System.Drawing.Point(246, 163)
        Me.txtMaxVisibleResults.MaxLength = 3
        Me.txtMaxVisibleResults.Name = "txtMaxVisibleResults"
        Me.txtMaxVisibleResults.Size = New System.Drawing.Size(41, 20)
        Me.txtMaxVisibleResults.TabIndex = 12
        Me.txtMaxVisibleResults.Text = "100"
        Me.txtMaxVisibleResults.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 166)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(126, 13)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Отображать последние"
        '
        'chkWriteFatalErrors
        '
        Me.chkWriteFatalErrors.AutoSize = True
        Me.chkWriteFatalErrors.Location = New System.Drawing.Point(9, 48)
        Me.chkWriteFatalErrors.Name = "chkWriteFatalErrors"
        Me.chkWriteFatalErrors.Size = New System.Drawing.Size(202, 17)
        Me.chkWriteFatalErrors.TabIndex = 7
        Me.chkWriteFatalErrors.Text = "Писать в файл фатальные ошибки"
        Me.chkWriteFatalErrors.UseVisualStyleBackColor = True
        '
        'chkShowJournalTab
        '
        Me.chkShowJournalTab.AutoSize = True
        Me.chkShowJournalTab.Location = New System.Drawing.Point(9, 25)
        Me.chkShowJournalTab.Name = "chkShowJournalTab"
        Me.chkShowJournalTab.Size = New System.Drawing.Size(170, 17)
        Me.chkShowJournalTab.TabIndex = 6
        Me.chkShowJournalTab.Text = "Показывать журнал ошибок"
        Me.chkShowJournalTab.UseVisualStyleBackColor = True
        '
        'lblChangeMainFont
        '
        Me.lblChangeMainFont.AutoSize = True
        Me.lblChangeMainFont.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblChangeMainFont.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblChangeMainFont.Location = New System.Drawing.Point(6, 189)
        Me.lblChangeMainFont.Name = "lblChangeMainFont"
        Me.lblChangeMainFont.Size = New System.Drawing.Size(170, 13)
        Me.lblChangeMainFont.TabIndex = 19
        Me.lblChangeMainFont.Text = "Изменить шрифт главного окна"
        '
        'frmAppOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(458, 475)
        Me.Controls.Add(Me.gprOtherBox)
        Me.Controls.Add(Me.grpRunBox)
        Me.Controls.Add(Me.grpTrayBox)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAppOptions"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Настройки"
        Me.grpTrayBox.ResumeLayout(False)
        Me.grpTrayBox.PerformLayout()
        Me.grpRunBox.ResumeLayout(False)
        Me.grpRunBox.PerformLayout()
        Me.gprOtherBox.ResumeLayout(False)
        Me.gprOtherBox.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents chkHideToTray As System.Windows.Forms.CheckBox
    Friend WithEvents chkCloseToTray As System.Windows.Forms.CheckBox
    Friend WithEvents chkRunWithWindows As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCheckInterval As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkRunHidden As System.Windows.Forms.CheckBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtTorrentsStoreTime As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents grpTrayBox As System.Windows.Forms.GroupBox
    Friend WithEvents grpRunBox As System.Windows.Forms.GroupBox
    Friend WithEvents gprOtherBox As System.Windows.Forms.GroupBox
    Friend WithEvents chkShowJournalTab As System.Windows.Forms.CheckBox
    Friend WithEvents chkAutoChecking As System.Windows.Forms.CheckBox
    Friend WithEvents chkWriteFatalErrors As System.Windows.Forms.CheckBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtMaxVisibleResults As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents tlpToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents lblClearCache As System.Windows.Forms.Label
    Friend WithEvents chkConfirmExit As System.Windows.Forms.CheckBox
    Friend WithEvents chkCheckUpdates As CheckBox
    Friend WithEvents lblChangeMainFont As Label
    Friend WithEvents fntdMain As FontDialog
End Class
