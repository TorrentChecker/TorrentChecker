<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmKeyWord
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtKeyWord = New System.Windows.Forms.TextBox()
        Me.grpTrackersBox = New System.Windows.Forms.GroupBox()
        Me.lblKinozalForumsCount = New System.Windows.Forms.Label()
        Me.lblRutrackerForumsCount = New System.Windows.Forms.Label()
        Me.lblForumsKinozal = New System.Windows.Forms.Label()
        Me.lblForumsRutracker = New System.Windows.Forms.Label()
        Me.chkKinozal = New System.Windows.Forms.CheckBox()
        Me.chkRuTracker = New System.Windows.Forms.CheckBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.grpFiltersBox = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtTorrentMaxSize = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtTorrentMinSize = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtTopicID = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtAdditionalFilter = New System.Windows.Forms.TextBox()
        Me.chkAdditionalFilter = New System.Windows.Forms.CheckBox()
        Me.txtKeyWordLabel = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tlpToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.grpNotificationBox = New System.Windows.Forms.GroupBox()
        Me.lblResetToQB = New System.Windows.Forms.Label()
        Me.lblResetToUT = New System.Windows.Forms.Label()
        Me.lblTCCommandLineHelp = New System.Windows.Forms.Label()
        Me.txtTCCommandLine = New System.Windows.Forms.TextBox()
        Me.chkSendToClient = New System.Windows.Forms.CheckBox()
        Me.grpKWOptionsBox = New System.Windows.Forms.GroupBox()
        Me.chkPause = New System.Windows.Forms.CheckBox()
        Me.chkUniqueness = New System.Windows.Forms.CheckBox()
        Me.grpTrackersBox.SuspendLayout()
        Me.grpFiltersBox.SuspendLayout()
        Me.grpNotificationBox.SuspendLayout()
        Me.grpKWOptionsBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(149, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Ключевое слово или фраза:"
        '
        'txtKeyWord
        '
        Me.txtKeyWord.Location = New System.Drawing.Point(186, 48)
        Me.txtKeyWord.MaxLength = 100
        Me.txtKeyWord.Name = "txtKeyWord"
        Me.txtKeyWord.Size = New System.Drawing.Size(302, 20)
        Me.txtKeyWord.TabIndex = 1
        '
        'grpTrackersBox
        '
        Me.grpTrackersBox.AutoSize = True
        Me.grpTrackersBox.Controls.Add(Me.lblKinozalForumsCount)
        Me.grpTrackersBox.Controls.Add(Me.lblRutrackerForumsCount)
        Me.grpTrackersBox.Controls.Add(Me.lblForumsKinozal)
        Me.grpTrackersBox.Controls.Add(Me.lblForumsRutracker)
        Me.grpTrackersBox.Controls.Add(Me.chkKinozal)
        Me.grpTrackersBox.Controls.Add(Me.chkRuTracker)
        Me.grpTrackersBox.Location = New System.Drawing.Point(12, 179)
        Me.grpTrackersBox.Name = "grpTrackersBox"
        Me.grpTrackersBox.Size = New System.Drawing.Size(499, 85)
        Me.grpTrackersBox.TabIndex = 2
        Me.grpTrackersBox.TabStop = False
        Me.grpTrackersBox.Text = "Трекеры"
        '
        'lblKinozalForumsCount
        '
        Me.lblKinozalForumsCount.AutoSize = True
        Me.lblKinozalForumsCount.Location = New System.Drawing.Point(244, 50)
        Me.lblKinozalForumsCount.Name = "lblKinozalForumsCount"
        Me.lblKinozalForumsCount.Size = New System.Drawing.Size(112, 13)
        Me.lblKinozalForumsCount.TabIndex = 5
        Me.lblKinozalForumsCount.Text = "Выбрано форумов: 0"
        '
        'lblRutrackerForumsCount
        '
        Me.lblRutrackerForumsCount.AutoSize = True
        Me.lblRutrackerForumsCount.Location = New System.Drawing.Point(244, 27)
        Me.lblRutrackerForumsCount.Name = "lblRutrackerForumsCount"
        Me.lblRutrackerForumsCount.Size = New System.Drawing.Size(112, 13)
        Me.lblRutrackerForumsCount.TabIndex = 4
        Me.lblRutrackerForumsCount.Text = "Выбрано форумов: 0"
        '
        'lblForumsKinozal
        '
        Me.lblForumsKinozal.AutoSize = True
        Me.lblForumsKinozal.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblForumsKinozal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblForumsKinozal.Location = New System.Drawing.Point(116, 50)
        Me.lblForumsKinozal.Name = "lblForumsKinozal"
        Me.lblForumsKinozal.Size = New System.Drawing.Size(95, 13)
        Me.lblForumsKinozal.TabIndex = 9
        Me.lblForumsKinozal.Text = "Выбрать форумы"
        '
        'lblForumsRutracker
        '
        Me.lblForumsRutracker.AutoSize = True
        Me.lblForumsRutracker.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblForumsRutracker.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblForumsRutracker.Location = New System.Drawing.Point(116, 27)
        Me.lblForumsRutracker.Name = "lblForumsRutracker"
        Me.lblForumsRutracker.Size = New System.Drawing.Size(95, 13)
        Me.lblForumsRutracker.TabIndex = 7
        Me.lblForumsRutracker.Text = "Выбрать форумы"
        '
        'chkKinozal
        '
        Me.chkKinozal.AutoSize = True
        Me.chkKinozal.Location = New System.Drawing.Point(9, 49)
        Me.chkKinozal.Name = "chkKinozal"
        Me.chkKinozal.Size = New System.Drawing.Size(59, 17)
        Me.chkKinozal.TabIndex = 8
        Me.chkKinozal.Text = "kinozal"
        Me.chkKinozal.UseVisualStyleBackColor = True
        '
        'chkRuTracker
        '
        Me.chkRuTracker.AutoSize = True
        Me.chkRuTracker.Location = New System.Drawing.Point(9, 26)
        Me.chkRuTracker.Name = "chkRuTracker"
        Me.chkRuTracker.Size = New System.Drawing.Size(68, 17)
        Me.chkRuTracker.TabIndex = 6
        Me.chkRuTracker.Text = "rutracker"
        Me.chkRuTracker.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(309, 533)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(98, 26)
        Me.btnSave.TabIndex = 14
        Me.btnSave.Text = "Сохранить"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(413, 533)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(98, 26)
        Me.btnCancel.TabIndex = 15
        Me.btnCancel.Text = "Отмена"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'grpFiltersBox
        '
        Me.grpFiltersBox.AutoSize = True
        Me.grpFiltersBox.Controls.Add(Me.Label6)
        Me.grpFiltersBox.Controls.Add(Me.txtTorrentMaxSize)
        Me.grpFiltersBox.Controls.Add(Me.Label5)
        Me.grpFiltersBox.Controls.Add(Me.txtTorrentMinSize)
        Me.grpFiltersBox.Controls.Add(Me.Label4)
        Me.grpFiltersBox.Controls.Add(Me.txtTopicID)
        Me.grpFiltersBox.Controls.Add(Me.Label3)
        Me.grpFiltersBox.Controls.Add(Me.txtAdditionalFilter)
        Me.grpFiltersBox.Controls.Add(Me.chkAdditionalFilter)
        Me.grpFiltersBox.Controls.Add(Me.txtKeyWordLabel)
        Me.grpFiltersBox.Controls.Add(Me.Label2)
        Me.grpFiltersBox.Controls.Add(Me.txtKeyWord)
        Me.grpFiltersBox.Controls.Add(Me.Label1)
        Me.grpFiltersBox.Location = New System.Drawing.Point(12, 12)
        Me.grpFiltersBox.Name = "grpFiltersBox"
        Me.grpFiltersBox.Size = New System.Drawing.Size(499, 162)
        Me.grpFiltersBox.TabIndex = 5
        Me.grpFiltersBox.TabStop = False
        Me.grpFiltersBox.Text = "Фильтр"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(422, 126)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(20, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "ГБ"
        '
        'txtTorrentMaxSize
        '
        Me.txtTorrentMaxSize.Location = New System.Drawing.Point(364, 123)
        Me.txtTorrentMaxSize.MaxLength = 20
        Me.txtTorrentMaxSize.Name = "txtTorrentMaxSize"
        Me.txtTorrentMaxSize.Size = New System.Drawing.Size(52, 20)
        Me.txtTorrentMaxSize.TabIndex = 5
        Me.txtTorrentMaxSize.Text = "0"
        Me.txtTorrentMaxSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(244, 126)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(103, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "ГБ и/или не более:"
        '
        'txtTorrentMinSize
        '
        Me.txtTorrentMinSize.Location = New System.Drawing.Point(186, 123)
        Me.txtTorrentMinSize.MaxLength = 20
        Me.txtTorrentMinSize.Name = "txtTorrentMinSize"
        Me.txtTorrentMinSize.Size = New System.Drawing.Size(52, 20)
        Me.txtTorrentMinSize.TabIndex = 4
        Me.txtTorrentMinSize.Text = "0"
        Me.txtTorrentMinSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 126)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(148, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Размер торрента не менее:"
        '
        'txtTopicID
        '
        Me.txtTopicID.Location = New System.Drawing.Point(186, 98)
        Me.txtTopicID.MaxLength = 15
        Me.txtTopicID.Name = "txtTopicID"
        Me.txtTopicID.Size = New System.Drawing.Size(143, 20)
        Me.txtTopicID.TabIndex = 3
        Me.txtTopicID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTopicID.WordWrap = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 101)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(120, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Идентификатор темы:"
        '
        'txtAdditionalFilter
        '
        Me.txtAdditionalFilter.Enabled = False
        Me.txtAdditionalFilter.Location = New System.Drawing.Point(186, 73)
        Me.txtAdditionalFilter.MaxLength = 100
        Me.txtAdditionalFilter.Name = "txtAdditionalFilter"
        Me.txtAdditionalFilter.Size = New System.Drawing.Size(302, 20)
        Me.txtAdditionalFilter.TabIndex = 2
        '
        'chkAdditionalFilter
        '
        Me.chkAdditionalFilter.AutoSize = True
        Me.chkAdditionalFilter.Location = New System.Drawing.Point(9, 75)
        Me.chkAdditionalFilter.Name = "chkAdditionalFilter"
        Me.chkAdditionalFilter.Size = New System.Drawing.Size(157, 17)
        Me.chkAdditionalFilter.TabIndex = 4
        Me.chkAdditionalFilter.Text = "Дополнительный фильтр:"
        Me.chkAdditionalFilter.UseVisualStyleBackColor = True
        '
        'txtKeyWordLabel
        '
        Me.txtKeyWordLabel.Location = New System.Drawing.Point(186, 23)
        Me.txtKeyWordLabel.MaxLength = 500
        Me.txtKeyWordLabel.Name = "txtKeyWordLabel"
        Me.txtKeyWordLabel.Size = New System.Drawing.Size(302, 20)
        Me.txtKeyWordLabel.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(133, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Метка для отображения:"
        '
        'grpNotificationBox
        '
        Me.grpNotificationBox.AutoSize = True
        Me.grpNotificationBox.Controls.Add(Me.lblResetToQB)
        Me.grpNotificationBox.Controls.Add(Me.lblResetToUT)
        Me.grpNotificationBox.Controls.Add(Me.lblTCCommandLineHelp)
        Me.grpNotificationBox.Controls.Add(Me.txtTCCommandLine)
        Me.grpNotificationBox.Controls.Add(Me.chkSendToClient)
        Me.grpNotificationBox.Location = New System.Drawing.Point(12, 355)
        Me.grpNotificationBox.Name = "grpNotificationBox"
        Me.grpNotificationBox.Size = New System.Drawing.Size(499, 172)
        Me.grpNotificationBox.TabIndex = 6
        Me.grpNotificationBox.TabStop = False
        Me.grpNotificationBox.Text = "Поведение"
        '
        'lblResetToQB
        '
        Me.lblResetToQB.AutoSize = True
        Me.lblResetToQB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblResetToQB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblResetToQB.Location = New System.Drawing.Point(116, 143)
        Me.lblResetToQB.Name = "lblResetToQB"
        Me.lblResetToQB.Size = New System.Drawing.Size(95, 13)
        Me.lblResetToQB.TabIndex = 4
        Me.lblResetToQB.Text = "Сброс (qBittorrent)"
        '
        'lblResetToUT
        '
        Me.lblResetToUT.AutoSize = True
        Me.lblResetToUT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblResetToUT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.lblResetToUT.Location = New System.Drawing.Point(6, 143)
        Me.lblResetToUT.Name = "lblResetToUT"
        Me.lblResetToUT.Size = New System.Drawing.Size(87, 13)
        Me.lblResetToUT.TabIndex = 3
        Me.lblResetToUT.Text = "Сброс (uTorrent)"
        '
        'lblTCCommandLineHelp
        '
        Me.lblTCCommandLineHelp.AutoSize = True
        Me.lblTCCommandLineHelp.Location = New System.Drawing.Point(7, 77)
        Me.lblTCCommandLineHelp.Name = "lblTCCommandLineHelp"
        Me.lblTCCommandLineHelp.Size = New System.Drawing.Size(232, 13)
        Me.lblTCCommandLineHelp.TabIndex = 2
        Me.lblTCCommandLineHelp.Text = "%ClientPath% - торрент-клиент по умолчанию"
        '
        'txtTCCommandLine
        '
        Me.txtTCCommandLine.Location = New System.Drawing.Point(9, 52)
        Me.txtTCCommandLine.MaxLength = 1000
        Me.txtTCCommandLine.Name = "txtTCCommandLine"
        Me.txtTCCommandLine.Size = New System.Drawing.Size(479, 20)
        Me.txtTCCommandLine.TabIndex = 13
        Me.txtTCCommandLine.Text = "%ClientPath% /directory ""C:\PathToSave\%TorrentName%"" %TorrentPath%"
        '
        'chkSendToClient
        '
        Me.chkSendToClient.AutoSize = True
        Me.chkSendToClient.Location = New System.Drawing.Point(9, 29)
        Me.chkSendToClient.Name = "chkSendToClient"
        Me.chkSendToClient.Size = New System.Drawing.Size(404, 17)
        Me.chkSendToClient.TabIndex = 12
        Me.chkSendToClient.Text = "Автоматически скачать найденный торрент и переслать в торрент-клиент"
        Me.chkSendToClient.UseVisualStyleBackColor = True
        '
        'grpKWOptionsBox
        '
        Me.grpKWOptionsBox.Controls.Add(Me.chkPause)
        Me.grpKWOptionsBox.Controls.Add(Me.chkUniqueness)
        Me.grpKWOptionsBox.Location = New System.Drawing.Point(12, 270)
        Me.grpKWOptionsBox.Name = "grpKWOptionsBox"
        Me.grpKWOptionsBox.Size = New System.Drawing.Size(499, 79)
        Me.grpKWOptionsBox.TabIndex = 7
        Me.grpKWOptionsBox.TabStop = False
        Me.grpKWOptionsBox.Text = "Свойства"
        '
        'chkPause
        '
        Me.chkPause.AutoSize = True
        Me.chkPause.Location = New System.Drawing.Point(9, 49)
        Me.chkPause.Name = "chkPause"
        Me.chkPause.Size = New System.Drawing.Size(57, 17)
        Me.chkPause.TabIndex = 11
        Me.chkPause.Text = "Пауза"
        Me.chkPause.UseVisualStyleBackColor = True
        '
        'chkUniqueness
        '
        Me.chkUniqueness.AutoSize = True
        Me.chkUniqueness.Location = New System.Drawing.Point(9, 26)
        Me.chkUniqueness.Name = "chkUniqueness"
        Me.chkUniqueness.Size = New System.Drawing.Size(99, 17)
        Me.chkUniqueness.TabIndex = 10
        Me.chkUniqueness.Text = "Уникальность"
        Me.chkUniqueness.UseVisualStyleBackColor = True
        '
        'frmKeyWord
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(522, 563)
        Me.Controls.Add(Me.grpKWOptionsBox)
        Me.Controls.Add(Me.grpNotificationBox)
        Me.Controls.Add(Me.grpFiltersBox)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.grpTrackersBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmKeyWord"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Добавление ключевого слова"
        Me.grpTrackersBox.ResumeLayout(False)
        Me.grpTrackersBox.PerformLayout()
        Me.grpFiltersBox.ResumeLayout(False)
        Me.grpFiltersBox.PerformLayout()
        Me.grpNotificationBox.ResumeLayout(False)
        Me.grpNotificationBox.PerformLayout()
        Me.grpKWOptionsBox.ResumeLayout(False)
        Me.grpKWOptionsBox.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtKeyWord As System.Windows.Forms.TextBox
    Friend WithEvents grpTrackersBox As System.Windows.Forms.GroupBox
    Friend WithEvents chkKinozal As System.Windows.Forms.CheckBox
    Friend WithEvents chkRuTracker As System.Windows.Forms.CheckBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents grpFiltersBox As System.Windows.Forms.GroupBox
    Friend WithEvents txtKeyWordLabel As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtAdditionalFilter As System.Windows.Forms.TextBox
    Friend WithEvents chkAdditionalFilter As System.Windows.Forms.CheckBox
    Friend WithEvents tlpToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents lblForumsKinozal As System.Windows.Forms.Label
    Friend WithEvents lblForumsRutracker As System.Windows.Forms.Label
    Friend WithEvents lblKinozalForumsCount As System.Windows.Forms.Label
    Friend WithEvents lblRutrackerForumsCount As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtTopicID As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtTorrentMaxSize As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtTorrentMinSize As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents grpNotificationBox As System.Windows.Forms.GroupBox
    Friend WithEvents txtTCCommandLine As System.Windows.Forms.TextBox
    Friend WithEvents chkSendToClient As System.Windows.Forms.CheckBox
    Friend WithEvents lblTCCommandLineHelp As System.Windows.Forms.Label
    Friend WithEvents lblResetToUT As System.Windows.Forms.Label
    Friend WithEvents lblResetToQB As System.Windows.Forms.Label
    Friend WithEvents grpKWOptionsBox As System.Windows.Forms.GroupBox
    Friend WithEvents chkPause As System.Windows.Forms.CheckBox
    Friend WithEvents chkUniqueness As System.Windows.Forms.CheckBox
End Class
