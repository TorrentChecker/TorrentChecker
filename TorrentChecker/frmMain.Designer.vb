<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.dgvTorrents = New System.Windows.Forms.DataGridView()
        Me.cmsTorrents = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmsiCopyLink = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmsiMarkSelectedAsRead = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmsiDeleteSelectedTorrents = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmsiMarkAllAsRead_t = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmsiClearAll_t = New System.Windows.Forms.ToolStripMenuItem()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.scMain = New System.Windows.Forms.SplitContainer()
        Me.grpKWBox = New System.Windows.Forms.GroupBox()
        Me.dgvKeyWords = New System.Windows.Forms.DataGridView()
        Me.cmsKeyWord = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmsiAddKeyWord = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmsiEditKeyWord = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmsiDeleteSelectedKW = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmsiMarkAllAsRead = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmsiClearAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmsiPause = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmsiResume = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnAddKeyWord = New System.Windows.Forms.Button()
        Me.tbControl = New System.Windows.Forms.TabControl()
        Me.TabPage_torrents = New System.Windows.Forms.TabPage()
        Me.TabPage_journal = New System.Windows.Forms.TabPage()
        Me.rtbLogTextBox = New System.Windows.Forms.RichTextBox()
        Me.cmsLog = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmsiLogSelectAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem11 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmsiLogCopyText = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem10 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmsiLogClear = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabPage_captcha = New System.Windows.Forms.TabPage()
        Me.txtCaptcha = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnSendCaptcha = New System.Windows.Forms.Button()
        Me.picCaptcha = New System.Windows.Forms.PictureBox()
        Me.tlpToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.tmrStartChecking = New System.Windows.Forms.Timer(Me.components)
        Me.msMain = New System.Windows.Forms.MenuStrip()
        Me.msiFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.msiExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.msiOptions = New System.Windows.Forms.ToolStripMenuItem()
        Me.msiCredentials = New System.Windows.Forms.ToolStripMenuItem()
        Me.msiAppOptions = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripSeparator()
        Me.msiHide = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem8 = New System.Windows.Forms.ToolStripSeparator()
        Me.msiPause = New System.Windows.Forms.ToolStripMenuItem()
        Me.msiHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.msiHomePage = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem12 = New System.Windows.Forms.ToolStripSeparator()
        Me.msiAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.msiNewPM = New System.Windows.Forms.ToolStripMenuItem()
        Me.msiPM_ru = New System.Windows.Forms.ToolStripMenuItem()
        Me.msiPM_kz = New System.Windows.Forms.ToolStripMenuItem()
        Me.trayIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.cmsTray = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.cmsiHide = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem9 = New System.Windows.Forms.ToolStripSeparator()
        Me.сmsiPause = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripSeparator()
        Me.сmsiExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmrTray = New System.Windows.Forms.Timer(Me.components)
        Me.stStrip = New System.Windows.Forms.StatusStrip()
        Me.tssLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tmrLastCheck = New System.Windows.Forms.Timer(Me.components)
        Me.cmsTorrentColumns = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tmrCheckUpdate = New System.Windows.Forms.Timer(Me.components)
        Me.lblShowNewVersion = New System.Windows.Forms.Label()
        Me.trayIconPM = New System.Windows.Forms.NotifyIcon(Me.components)
        CType(Me.dgvTorrents, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsTorrents.SuspendLayout()
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.scMain.Panel1.SuspendLayout()
        Me.scMain.Panel2.SuspendLayout()
        Me.scMain.SuspendLayout()
        Me.grpKWBox.SuspendLayout()
        CType(Me.dgvKeyWords, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsKeyWord.SuspendLayout()
        Me.tbControl.SuspendLayout()
        Me.TabPage_torrents.SuspendLayout()
        Me.TabPage_journal.SuspendLayout()
        Me.cmsLog.SuspendLayout()
        Me.TabPage_captcha.SuspendLayout()
        CType(Me.picCaptcha, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.msMain.SuspendLayout()
        Me.cmsTray.SuspendLayout()
        Me.stStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnConnect
        '
        Me.btnConnect.AutoSize = True
        Me.btnConnect.Location = New System.Drawing.Point(5, 27)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(98, 26)
        Me.btnConnect.TabIndex = 0
        Me.btnConnect.Text = "Подключиться"
        Me.btnConnect.UseVisualStyleBackColor = True
        '
        'dgvTorrents
        '
        Me.dgvTorrents.AllowUserToAddRows = False
        Me.dgvTorrents.AllowUserToDeleteRows = False
        Me.dgvTorrents.AllowUserToOrderColumns = True
        Me.dgvTorrents.AllowUserToResizeRows = False
        Me.dgvTorrents.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgvTorrents.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvTorrents.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvTorrents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvTorrents.ContextMenuStrip = Me.cmsTorrents
        Me.dgvTorrents.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTorrents.Location = New System.Drawing.Point(3, 3)
        Me.dgvTorrents.Name = "dgvTorrents"
        Me.dgvTorrents.ReadOnly = True
        Me.dgvTorrents.RowHeadersVisible = False
        Me.dgvTorrents.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvTorrents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTorrents.Size = New System.Drawing.Size(752, 428)
        Me.dgvTorrents.TabIndex = 2
        '
        'cmsTorrents
        '
        Me.cmsTorrents.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmsiCopyLink, Me.ToolStripMenuItem1, Me.cmsiMarkSelectedAsRead, Me.cmsiDeleteSelectedTorrents, Me.ToolStripMenuItem2, Me.cmsiMarkAllAsRead_t, Me.cmsiClearAll_t})
        Me.cmsTorrents.Name = "cmsTorrents"
        Me.cmsTorrents.Size = New System.Drawing.Size(349, 126)
        '
        'cmsiCopyLink
        '
        Me.cmsiCopyLink.Name = "cmsiCopyLink"
        Me.cmsiCopyLink.Size = New System.Drawing.Size(348, 22)
        Me.cmsiCopyLink.Text = "Скопировать ссылку в буфер обмена"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(345, 6)
        '
        'cmsiMarkSelectedAsRead
        '
        Me.cmsiMarkSelectedAsRead.Name = "cmsiMarkSelectedAsRead"
        Me.cmsiMarkSelectedAsRead.Size = New System.Drawing.Size(348, 22)
        Me.cmsiMarkSelectedAsRead.Text = "Отметить выбранные торренты как прочитанные"
        '
        'cmsiDeleteSelectedTorrents
        '
        Me.cmsiDeleteSelectedTorrents.Name = "cmsiDeleteSelectedTorrents"
        Me.cmsiDeleteSelectedTorrents.Size = New System.Drawing.Size(348, 22)
        Me.cmsiDeleteSelectedTorrents.Text = "Удалить выбранные торренты из списка"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(345, 6)
        '
        'cmsiMarkAllAsRead_t
        '
        Me.cmsiMarkAllAsRead_t.Name = "cmsiMarkAllAsRead_t"
        Me.cmsiMarkAllAsRead_t.Size = New System.Drawing.Size(348, 22)
        Me.cmsiMarkAllAsRead_t.Text = "Отметить все торренты как прочитанные"
        '
        'cmsiClearAll_t
        '
        Me.cmsiClearAll_t.Name = "cmsiClearAll_t"
        Me.cmsiClearAll_t.Size = New System.Drawing.Size(348, 22)
        Me.cmsiClearAll_t.Text = "Очистить все найденные торренты"
        '
        'txtSearch
        '
        Me.txtSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearch.Location = New System.Drawing.Point(681, 33)
        Me.txtSearch.MaxLength = 100
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(265, 20)
        Me.txtSearch.TabIndex = 4
        '
        'scMain
        '
        Me.scMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.scMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.scMain.Location = New System.Drawing.Point(5, 69)
        Me.scMain.Name = "scMain"
        '
        'scMain.Panel1
        '
        Me.scMain.Panel1.Controls.Add(Me.grpKWBox)
        Me.scMain.Panel1.Controls.Add(Me.btnAddKeyWord)
        Me.scMain.Panel1MinSize = 0
        '
        'scMain.Panel2
        '
        Me.scMain.Panel2.Controls.Add(Me.tbControl)
        Me.scMain.Panel2MinSize = 500
        Me.scMain.Size = New System.Drawing.Size(942, 462)
        Me.scMain.SplitterDistance = 170
        Me.scMain.TabIndex = 0
        '
        'grpKWBox
        '
        Me.grpKWBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpKWBox.Controls.Add(Me.dgvKeyWords)
        Me.grpKWBox.Location = New System.Drawing.Point(0, 3)
        Me.grpKWBox.Name = "grpKWBox"
        Me.grpKWBox.Size = New System.Drawing.Size(167, 417)
        Me.grpKWBox.TabIndex = 4
        Me.grpKWBox.TabStop = False
        Me.grpKWBox.Text = "Ключевые фразы: 0"
        '
        'dgvKeyWords
        '
        Me.dgvKeyWords.AllowUserToAddRows = False
        Me.dgvKeyWords.AllowUserToDeleteRows = False
        Me.dgvKeyWords.AllowUserToResizeColumns = False
        Me.dgvKeyWords.AllowUserToResizeRows = False
        Me.dgvKeyWords.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvKeyWords.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgvKeyWords.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvKeyWords.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvKeyWords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvKeyWords.ColumnHeadersVisible = False
        Me.dgvKeyWords.ContextMenuStrip = Me.cmsKeyWord
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvKeyWords.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvKeyWords.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvKeyWords.Location = New System.Drawing.Point(3, 16)
        Me.dgvKeyWords.Name = "dgvKeyWords"
        Me.dgvKeyWords.ReadOnly = True
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvKeyWords.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvKeyWords.RowHeadersVisible = False
        Me.dgvKeyWords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvKeyWords.Size = New System.Drawing.Size(161, 398)
        Me.dgvKeyWords.TabIndex = 1
        '
        'cmsKeyWord
        '
        Me.cmsKeyWord.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmsiAddKeyWord, Me.ToolStripMenuItem3, Me.cmsiEditKeyWord, Me.cmsiDeleteSelectedKW, Me.ToolStripMenuItem4, Me.cmsiMarkAllAsRead, Me.cmsiClearAll, Me.ToolStripMenuItem5, Me.cmsiPause, Me.cmsiResume})
        Me.cmsKeyWord.Name = "cmsKeyWord"
        Me.cmsKeyWord.Size = New System.Drawing.Size(337, 176)
        '
        'cmsiAddKeyWord
        '
        Me.cmsiAddKeyWord.Name = "cmsiAddKeyWord"
        Me.cmsiAddKeyWord.Size = New System.Drawing.Size(336, 22)
        Me.cmsiAddKeyWord.Text = "Добавить ключевое слово"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(333, 6)
        '
        'cmsiEditKeyWord
        '
        Me.cmsiEditKeyWord.Name = "cmsiEditKeyWord"
        Me.cmsiEditKeyWord.Size = New System.Drawing.Size(336, 22)
        Me.cmsiEditKeyWord.Text = "Редактировать выбранное ключевое слово"
        '
        'cmsiDeleteSelectedKW
        '
        Me.cmsiDeleteSelectedKW.Name = "cmsiDeleteSelectedKW"
        Me.cmsiDeleteSelectedKW.Size = New System.Drawing.Size(336, 22)
        Me.cmsiDeleteSelectedKW.Text = "Удалить выбранные ключевые слова из списка"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(333, 6)
        '
        'cmsiMarkAllAsRead
        '
        Me.cmsiMarkAllAsRead.Name = "cmsiMarkAllAsRead"
        Me.cmsiMarkAllAsRead.Size = New System.Drawing.Size(336, 22)
        Me.cmsiMarkAllAsRead.Text = "Отметить все торренты как прочитанные"
        '
        'cmsiClearAll
        '
        Me.cmsiClearAll.Name = "cmsiClearAll"
        Me.cmsiClearAll.Size = New System.Drawing.Size(336, 22)
        Me.cmsiClearAll.Text = "Очистить все найденные торренты"
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(333, 6)
        '
        'cmsiPause
        '
        Me.cmsiPause.Name = "cmsiPause"
        Me.cmsiPause.Size = New System.Drawing.Size(336, 22)
        Me.cmsiPause.Text = "Приостановить"
        '
        'cmsiResume
        '
        Me.cmsiResume.Name = "cmsiResume"
        Me.cmsiResume.Size = New System.Drawing.Size(336, 22)
        Me.cmsiResume.Text = "Возобновить"
        '
        'btnAddKeyWord
        '
        Me.btnAddKeyWord.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddKeyWord.Location = New System.Drawing.Point(66, 426)
        Me.btnAddKeyWord.Name = "btnAddKeyWord"
        Me.btnAddKeyWord.Size = New System.Drawing.Size(98, 26)
        Me.btnAddKeyWord.TabIndex = 3
        Me.btnAddKeyWord.Text = "Добавить"
        Me.btnAddKeyWord.UseVisualStyleBackColor = True
        '
        'tbControl
        '
        Me.tbControl.Controls.Add(Me.TabPage_torrents)
        Me.tbControl.Controls.Add(Me.TabPage_journal)
        Me.tbControl.Controls.Add(Me.TabPage_captcha)
        Me.tbControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbControl.Location = New System.Drawing.Point(0, 0)
        Me.tbControl.Name = "tbControl"
        Me.tbControl.SelectedIndex = 0
        Me.tbControl.Size = New System.Drawing.Size(766, 460)
        Me.tbControl.TabIndex = 4
        '
        'TabPage_torrents
        '
        Me.TabPage_torrents.Controls.Add(Me.dgvTorrents)
        Me.TabPage_torrents.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_torrents.Name = "TabPage_torrents"
        Me.TabPage_torrents.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_torrents.Size = New System.Drawing.Size(758, 434)
        Me.TabPage_torrents.TabIndex = 0
        Me.TabPage_torrents.Text = "Найденные торренты: 0"
        Me.TabPage_torrents.UseVisualStyleBackColor = True
        '
        'TabPage_journal
        '
        Me.TabPage_journal.Controls.Add(Me.rtbLogTextBox)
        Me.TabPage_journal.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_journal.Name = "TabPage_journal"
        Me.TabPage_journal.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_journal.Size = New System.Drawing.Size(758, 434)
        Me.TabPage_journal.TabIndex = 1
        Me.TabPage_journal.Text = "Журнал ошибок"
        Me.TabPage_journal.UseVisualStyleBackColor = True
        '
        'rtbLogTextBox
        '
        Me.rtbLogTextBox.BackColor = System.Drawing.SystemColors.Control
        Me.rtbLogTextBox.ContextMenuStrip = Me.cmsLog
        Me.rtbLogTextBox.DetectUrls = False
        Me.rtbLogTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbLogTextBox.Location = New System.Drawing.Point(3, 3)
        Me.rtbLogTextBox.Name = "rtbLogTextBox"
        Me.rtbLogTextBox.ReadOnly = True
        Me.rtbLogTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.rtbLogTextBox.Size = New System.Drawing.Size(752, 428)
        Me.rtbLogTextBox.TabIndex = 4
        Me.rtbLogTextBox.Text = ""
        '
        'cmsLog
        '
        Me.cmsLog.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmsiLogSelectAll, Me.ToolStripMenuItem11, Me.cmsiLogCopyText, Me.ToolStripMenuItem10, Me.cmsiLogClear})
        Me.cmsLog.Name = "cmsLog"
        Me.cmsLog.Size = New System.Drawing.Size(186, 82)
        '
        'cmsiLogSelectAll
        '
        Me.cmsiLogSelectAll.Name = "cmsiLogSelectAll"
        Me.cmsiLogSelectAll.Size = New System.Drawing.Size(185, 22)
        Me.cmsiLogSelectAll.Text = "Выделить весь текст"
        '
        'ToolStripMenuItem11
        '
        Me.ToolStripMenuItem11.Name = "ToolStripMenuItem11"
        Me.ToolStripMenuItem11.Size = New System.Drawing.Size(182, 6)
        '
        'cmsiLogCopyText
        '
        Me.cmsiLogCopyText.Name = "cmsiLogCopyText"
        Me.cmsiLogCopyText.Size = New System.Drawing.Size(185, 22)
        Me.cmsiLogCopyText.Text = "Копировать текст"
        '
        'ToolStripMenuItem10
        '
        Me.ToolStripMenuItem10.Name = "ToolStripMenuItem10"
        Me.ToolStripMenuItem10.Size = New System.Drawing.Size(182, 6)
        '
        'cmsiLogClear
        '
        Me.cmsiLogClear.Name = "cmsiLogClear"
        Me.cmsiLogClear.Size = New System.Drawing.Size(185, 22)
        Me.cmsiLogClear.Text = "Очистить журнал"
        '
        'TabPage_captcha
        '
        Me.TabPage_captcha.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage_captcha.Controls.Add(Me.txtCaptcha)
        Me.TabPage_captcha.Controls.Add(Me.Label1)
        Me.TabPage_captcha.Controls.Add(Me.btnSendCaptcha)
        Me.TabPage_captcha.Controls.Add(Me.picCaptcha)
        Me.TabPage_captcha.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_captcha.Name = "TabPage_captcha"
        Me.TabPage_captcha.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_captcha.Size = New System.Drawing.Size(758, 434)
        Me.TabPage_captcha.TabIndex = 2
        Me.TabPage_captcha.Text = "xyz запрашивает капчу"
        '
        'txtCaptcha
        '
        Me.txtCaptcha.Location = New System.Drawing.Point(166, 87)
        Me.txtCaptcha.MaxLength = 25
        Me.txtCaptcha.Name = "txtCaptcha"
        Me.txtCaptcha.Size = New System.Drawing.Size(129, 20)
        Me.txtCaptcha.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 90)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(124, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Введите текст с капчи:"
        '
        'btnSendCaptcha
        '
        Me.btnSendCaptcha.Location = New System.Drawing.Point(166, 113)
        Me.btnSendCaptcha.Name = "btnSendCaptcha"
        Me.btnSendCaptcha.Size = New System.Drawing.Size(129, 26)
        Me.btnSendCaptcha.TabIndex = 0
        Me.btnSendCaptcha.Text = "Отправить капчу"
        Me.btnSendCaptcha.UseVisualStyleBackColor = True
        '
        'picCaptcha
        '
        Me.picCaptcha.Image = CType(resources.GetObject("picCaptcha.Image"), System.Drawing.Image)
        Me.picCaptcha.InitialImage = CType(resources.GetObject("picCaptcha.InitialImage"), System.Drawing.Image)
        Me.picCaptcha.Location = New System.Drawing.Point(166, 6)
        Me.picCaptcha.Name = "picCaptcha"
        Me.picCaptcha.Size = New System.Drawing.Size(129, 75)
        Me.picCaptcha.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.picCaptcha.TabIndex = 1
        Me.picCaptcha.TabStop = False
        '
        'tmrStartChecking
        '
        Me.tmrStartChecking.Interval = 60000
        '
        'msMain
        '
        Me.msMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.msiFile, Me.msiOptions, Me.msiHelp, Me.msiNewPM})
        Me.msMain.Location = New System.Drawing.Point(0, 0)
        Me.msMain.Name = "msMain"
        Me.msMain.Size = New System.Drawing.Size(952, 24)
        Me.msMain.TabIndex = 6
        Me.msMain.Text = "MenuStrip1"
        '
        'msiFile
        '
        Me.msiFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.msiExit})
        Me.msiFile.Name = "msiFile"
        Me.msiFile.Size = New System.Drawing.Size(48, 20)
        Me.msiFile.Text = "Файл"
        '
        'msiExit
        '
        Me.msiExit.Name = "msiExit"
        Me.msiExit.Size = New System.Drawing.Size(108, 22)
        Me.msiExit.Text = "Выход"
        '
        'msiOptions
        '
        Me.msiOptions.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.msiCredentials, Me.msiAppOptions, Me.ToolStripMenuItem7, Me.msiHide, Me.ToolStripMenuItem8, Me.msiPause})
        Me.msiOptions.Name = "msiOptions"
        Me.msiOptions.Size = New System.Drawing.Size(56, 20)
        Me.msiOptions.Text = "Опции"
        '
        'msiCredentials
        '
        Me.msiCredentials.Name = "msiCredentials"
        Me.msiCredentials.Size = New System.Drawing.Size(214, 22)
        Me.msiCredentials.Text = "Настройки подключения"
        '
        'msiAppOptions
        '
        Me.msiAppOptions.Name = "msiAppOptions"
        Me.msiAppOptions.Size = New System.Drawing.Size(214, 22)
        Me.msiAppOptions.Text = "Настройки программы"
        '
        'ToolStripMenuItem7
        '
        Me.ToolStripMenuItem7.Name = "ToolStripMenuItem7"
        Me.ToolStripMenuItem7.Size = New System.Drawing.Size(211, 6)
        '
        'msiHide
        '
        Me.msiHide.Name = "msiHide"
        Me.msiHide.Size = New System.Drawing.Size(214, 22)
        Me.msiHide.Text = "Скрыть в трей"
        '
        'ToolStripMenuItem8
        '
        Me.ToolStripMenuItem8.Name = "ToolStripMenuItem8"
        Me.ToolStripMenuItem8.Size = New System.Drawing.Size(211, 6)
        '
        'msiPause
        '
        Me.msiPause.Name = "msiPause"
        Me.msiPause.Size = New System.Drawing.Size(214, 22)
        Me.msiPause.Text = "Приостановить проверку"
        '
        'msiHelp
        '
        Me.msiHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.msiHomePage, Me.ToolStripMenuItem12, Me.msiAbout})
        Me.msiHelp.Name = "msiHelp"
        Me.msiHelp.Size = New System.Drawing.Size(68, 20)
        Me.msiHelp.Text = "Помощь"
        '
        'msiHomePage
        '
        Me.msiHomePage.Name = "msiHomePage"
        Me.msiHomePage.Size = New System.Drawing.Size(273, 22)
        Me.msiHomePage.Text = "Домашняя страница TorrentChecker"
        '
        'ToolStripMenuItem12
        '
        Me.ToolStripMenuItem12.Name = "ToolStripMenuItem12"
        Me.ToolStripMenuItem12.Size = New System.Drawing.Size(270, 6)
        '
        'msiAbout
        '
        Me.msiAbout.Name = "msiAbout"
        Me.msiAbout.Size = New System.Drawing.Size(273, 22)
        Me.msiAbout.Text = "О программе"
        '
        'msiNewPM
        '
        Me.msiNewPM.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.msiNewPM.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.msiPM_ru, Me.msiPM_kz})
        Me.msiNewPM.Name = "msiNewPM"
        Me.msiNewPM.Size = New System.Drawing.Size(137, 20)
        Me.msiNewPM.Text = "У вас есть новые ПМ!"
        Me.msiNewPM.Visible = False
        '
        'msiPM_ru
        '
        Me.msiPM_ru.Name = "msiPM_ru"
        Me.msiPM_ru.Size = New System.Drawing.Size(124, 22)
        Me.msiPM_ru.Text = "Рутрекер"
        Me.msiPM_ru.Visible = False
        '
        'msiPM_kz
        '
        Me.msiPM_kz.Name = "msiPM_kz"
        Me.msiPM_kz.Size = New System.Drawing.Size(124, 22)
        Me.msiPM_kz.Text = "Кинозал"
        Me.msiPM_kz.Visible = False
        '
        'trayIcon
        '
        Me.trayIcon.ContextMenuStrip = Me.cmsTray
        Me.trayIcon.Icon = CType(resources.GetObject("trayIcon.Icon"), System.Drawing.Icon)
        Me.trayIcon.Text = "NotifyIcon1"
        Me.trayIcon.Visible = True
        '
        'cmsTray
        '
        Me.cmsTray.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmsiHide, Me.ToolStripMenuItem9, Me.сmsiPause, Me.ToolStripMenuItem6, Me.сmsiExit})
        Me.cmsTray.Name = "cmsTray"
        Me.cmsTray.Size = New System.Drawing.Size(215, 82)
        '
        'cmsiHide
        '
        Me.cmsiHide.Name = "cmsiHide"
        Me.cmsiHide.Size = New System.Drawing.Size(214, 22)
        Me.cmsiHide.Text = "Скрыть в трей"
        '
        'ToolStripMenuItem9
        '
        Me.ToolStripMenuItem9.Name = "ToolStripMenuItem9"
        Me.ToolStripMenuItem9.Size = New System.Drawing.Size(211, 6)
        '
        'сmsiPause
        '
        Me.сmsiPause.Name = "сmsiPause"
        Me.сmsiPause.Size = New System.Drawing.Size(214, 22)
        Me.сmsiPause.Text = "Приостановить проверку"
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(211, 6)
        '
        'сmsiExit
        '
        Me.сmsiExit.Name = "сmsiExit"
        Me.сmsiExit.Size = New System.Drawing.Size(214, 22)
        Me.сmsiExit.Text = "Выход"
        '
        'tmrTray
        '
        Me.tmrTray.Interval = 400
        '
        'stStrip
        '
        Me.stStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tssLabel})
        Me.stStrip.Location = New System.Drawing.Point(0, 534)
        Me.stStrip.Name = "stStrip"
        Me.stStrip.Size = New System.Drawing.Size(952, 22)
        Me.stStrip.TabIndex = 8
        Me.stStrip.Text = "StatusStrip1"
        '
        'tssLabel
        '
        Me.tssLabel.Name = "tssLabel"
        Me.tssLabel.Size = New System.Drawing.Size(121, 17)
        Me.tssLabel.Text = "ToolStripStatusLabel1"
        '
        'tmrLastCheck
        '
        Me.tmrLastCheck.Enabled = True
        Me.tmrLastCheck.Interval = 1000
        '
        'cmsTorrentColumns
        '
        Me.cmsTorrentColumns.Name = "cmsTorrentColumns"
        Me.cmsTorrentColumns.Size = New System.Drawing.Size(61, 4)
        '
        'tmrCheckUpdate
        '
        Me.tmrCheckUpdate.Enabled = True
        Me.tmrCheckUpdate.Interval = 18000000
        '
        'lblShowNewVersion
        '
        Me.lblShowNewVersion.AutoSize = True
        Me.lblShowNewVersion.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblShowNewVersion.Location = New System.Drawing.Point(184, 36)
        Me.lblShowNewVersion.Name = "lblShowNewVersion"
        Me.lblShowNewVersion.Size = New System.Drawing.Size(89, 13)
        Me.lblShowNewVersion.TabIndex = 9
        Me.lblShowNewVersion.Text = "Обновлений нет"
        Me.lblShowNewVersion.Visible = False
        '
        'trayIconPM
        '
        Me.trayIconPM.Text = "NotifyIcon1"
        Me.trayIconPM.Visible = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(952, 556)
        Me.Controls.Add(Me.lblShowNewVersion)
        Me.Controls.Add(Me.stStrip)
        Me.Controls.Add(Me.msMain)
        Me.Controls.Add(Me.scMain)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.btnConnect)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.msMain
        Me.MinimumSize = New System.Drawing.Size(600, 300)
        Me.Name = "frmMain"
        Me.Text = "TorrentChecker"
        CType(Me.dgvTorrents, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsTorrents.ResumeLayout(False)
        Me.scMain.Panel1.ResumeLayout(False)
        Me.scMain.Panel2.ResumeLayout(False)
        CType(Me.scMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.scMain.ResumeLayout(False)
        Me.grpKWBox.ResumeLayout(False)
        CType(Me.dgvKeyWords, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsKeyWord.ResumeLayout(False)
        Me.tbControl.ResumeLayout(False)
        Me.TabPage_torrents.ResumeLayout(False)
        Me.TabPage_journal.ResumeLayout(False)
        Me.cmsLog.ResumeLayout(False)
        Me.TabPage_captcha.ResumeLayout(False)
        Me.TabPage_captcha.PerformLayout()
        CType(Me.picCaptcha, System.ComponentModel.ISupportInitialize).EndInit()
        Me.msMain.ResumeLayout(False)
        Me.msMain.PerformLayout()
        Me.cmsTray.ResumeLayout(False)
        Me.stStrip.ResumeLayout(False)
        Me.stStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnConnect As System.Windows.Forms.Button
    Friend WithEvents dgvTorrents As System.Windows.Forms.DataGridView
    Friend WithEvents scMain As System.Windows.Forms.SplitContainer
    Friend WithEvents btnAddKeyWord As System.Windows.Forms.Button
    Friend WithEvents grpKWBox As System.Windows.Forms.GroupBox
    Friend WithEvents tlpToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents tbControl As System.Windows.Forms.TabControl
    Friend WithEvents TabPage_torrents As System.Windows.Forms.TabPage
    Friend WithEvents TabPage_journal As System.Windows.Forms.TabPage
    Friend WithEvents dgvKeyWords As System.Windows.Forms.DataGridView
    Friend WithEvents cmsTorrents As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmsiMarkSelectedAsRead As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsiDeleteSelectedTorrents As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsiCopyLink As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents tmrStartChecking As System.Windows.Forms.Timer
    Friend WithEvents cmsKeyWord As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmsiAddKeyWord As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmsiEditKeyWord As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsiDeleteSelectedKW As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmsiMarkAllAsRead As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsiClearAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msMain As System.Windows.Forms.MenuStrip
    Friend WithEvents msiOptions As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msiCredentials As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TabPage_captcha As System.Windows.Forms.TabPage
    Friend WithEvents btnSendCaptcha As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents picCaptcha As System.Windows.Forms.PictureBox
    Friend WithEvents txtCaptcha As System.Windows.Forms.TextBox
    Friend WithEvents trayIcon As System.Windows.Forms.NotifyIcon
    Friend WithEvents tmrTray As System.Windows.Forms.Timer
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmsiMarkAllAsRead_t As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsiClearAll_t As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msiFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msiExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmsiPause As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsiResume As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsTray As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents сmsiExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents stStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents tssLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tmrLastCheck As System.Windows.Forms.Timer
    Friend WithEvents сmsiPause As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents msiPause As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msiHide As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmsiHide As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents rtbLogTextBox As System.Windows.Forms.RichTextBox
    Friend WithEvents cmsLog As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents cmsiLogSelectAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmsiLogCopyText As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmsiLogClear As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msiAppOptions As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsTorrentColumns As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents msiHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msiHomePage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents msiAbout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem12 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tmrCheckUpdate As Timer
    Friend WithEvents lblShowNewVersion As Label
    Friend WithEvents msiNewPM As ToolStripMenuItem
    Friend WithEvents msiPM_ru As ToolStripMenuItem
    Friend WithEvents msiPM_kz As ToolStripMenuItem
    Friend WithEvents trayIconPM As NotifyIcon
End Class
