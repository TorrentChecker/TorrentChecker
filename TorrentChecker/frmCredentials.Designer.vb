<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCredentials
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
        Me.grpProxyBox = New System.Windows.Forms.GroupBox()
        Me.txtProxyPort = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtProxyAddress = New System.Windows.Forms.TextBox()
        Me.chkUseProxy = New System.Windows.Forms.CheckBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtLogin_rt = New System.Windows.Forms.TextBox()
        Me.txtPassword_rt = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbCredentialsControl = New System.Windows.Forms.TabControl()
        Me.TabPage_rutracker = New System.Windows.Forms.TabPage()
        Me.lblRegistration_rt = New System.Windows.Forms.Label()
        Me.TabPage_kinozal = New System.Windows.Forms.TabPage()
        Me.lblRegistration_kz = New System.Windows.Forms.Label()
        Me.txtPassword_kz = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtLogin_kz = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.grpProxyBox.SuspendLayout()
        Me.tbCredentialsControl.SuspendLayout()
        Me.TabPage_rutracker.SuspendLayout()
        Me.TabPage_kinozal.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpProxyBox
        '
        Me.grpProxyBox.Controls.Add(Me.txtProxyPort)
        Me.grpProxyBox.Controls.Add(Me.Label6)
        Me.grpProxyBox.Controls.Add(Me.Label5)
        Me.grpProxyBox.Controls.Add(Me.txtProxyAddress)
        Me.grpProxyBox.Controls.Add(Me.chkUseProxy)
        Me.grpProxyBox.Location = New System.Drawing.Point(12, 122)
        Me.grpProxyBox.Name = "grpProxyBox"
        Me.grpProxyBox.Size = New System.Drawing.Size(533, 86)
        Me.grpProxyBox.TabIndex = 2
        Me.grpProxyBox.TabStop = False
        Me.grpProxyBox.Text = "Прокси-сервер"
        '
        'txtProxyPort
        '
        Me.txtProxyPort.Enabled = False
        Me.txtProxyPort.Location = New System.Drawing.Point(385, 48)
        Me.txtProxyPort.MaxLength = 5
        Me.txtProxyPort.Name = "txtProxyPort"
        Me.txtProxyPort.Size = New System.Drawing.Size(44, 20)
        Me.txtProxyPort.TabIndex = 4
        Me.txtProxyPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(328, 51)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 13)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "Порт:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(11, 51)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Адрес:"
        '
        'txtProxyAddress
        '
        Me.txtProxyAddress.Enabled = False
        Me.txtProxyAddress.Location = New System.Drawing.Point(78, 48)
        Me.txtProxyAddress.MaxLength = 100
        Me.txtProxyAddress.Name = "txtProxyAddress"
        Me.txtProxyAddress.Size = New System.Drawing.Size(235, 20)
        Me.txtProxyAddress.TabIndex = 1
        '
        'chkUseProxy
        '
        Me.chkUseProxy.AutoSize = True
        Me.chkUseProxy.Location = New System.Drawing.Point(14, 25)
        Me.chkUseProxy.Name = "chkUseProxy"
        Me.chkUseProxy.Size = New System.Drawing.Size(177, 17)
        Me.chkUseProxy.TabIndex = 0
        Me.chkUseProxy.Text = "Использовать прокси-сервер"
        Me.chkUseProxy.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(343, 219)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(98, 26)
        Me.btnSave.TabIndex = 4
        Me.btnSave.Text = "Сохранить"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(447, 219)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(98, 26)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "Отмена"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Логин:"
        '
        'txtLogin_rt
        '
        Me.txtLogin_rt.Location = New System.Drawing.Point(73, 15)
        Me.txtLogin_rt.MaxLength = 100
        Me.txtLogin_rt.Name = "txtLogin_rt"
        Me.txtLogin_rt.Size = New System.Drawing.Size(235, 20)
        Me.txtLogin_rt.TabIndex = 1
        '
        'txtPassword_rt
        '
        Me.txtPassword_rt.Location = New System.Drawing.Point(73, 41)
        Me.txtPassword_rt.MaxLength = 100
        Me.txtPassword_rt.Name = "txtPassword_rt"
        Me.txtPassword_rt.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword_rt.Size = New System.Drawing.Size(235, 20)
        Me.txtPassword_rt.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Пароль:"
        '
        'tbCredentialsControl
        '
        Me.tbCredentialsControl.Controls.Add(Me.TabPage_rutracker)
        Me.tbCredentialsControl.Controls.Add(Me.TabPage_kinozal)
        Me.tbCredentialsControl.Location = New System.Drawing.Point(12, 12)
        Me.tbCredentialsControl.Name = "tbCredentialsControl"
        Me.tbCredentialsControl.SelectedIndex = 0
        Me.tbCredentialsControl.Size = New System.Drawing.Size(533, 104)
        Me.tbCredentialsControl.TabIndex = 0
        '
        'TabPage_rutracker
        '
        Me.TabPage_rutracker.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage_rutracker.Controls.Add(Me.lblRegistration_rt)
        Me.TabPage_rutracker.Controls.Add(Me.txtPassword_rt)
        Me.TabPage_rutracker.Controls.Add(Me.Label2)
        Me.TabPage_rutracker.Controls.Add(Me.txtLogin_rt)
        Me.TabPage_rutracker.Controls.Add(Me.Label1)
        Me.TabPage_rutracker.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_rutracker.Name = "TabPage_rutracker"
        Me.TabPage_rutracker.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_rutracker.Size = New System.Drawing.Size(525, 78)
        Me.TabPage_rutracker.TabIndex = 0
        Me.TabPage_rutracker.Text = "rutracker"
        '
        'lblRegistration_rt
        '
        Me.lblRegistration_rt.AutoSize = True
        Me.lblRegistration_rt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblRegistration_rt.Location = New System.Drawing.Point(428, 18)
        Me.lblRegistration_rt.Name = "lblRegistration_rt"
        Me.lblRegistration_rt.Size = New System.Drawing.Size(72, 13)
        Me.lblRegistration_rt.TabIndex = 4
        Me.lblRegistration_rt.Text = "Регистрация"
        '
        'TabPage_kinozal
        '
        Me.TabPage_kinozal.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage_kinozal.Controls.Add(Me.lblRegistration_kz)
        Me.TabPage_kinozal.Controls.Add(Me.txtPassword_kz)
        Me.TabPage_kinozal.Controls.Add(Me.Label3)
        Me.TabPage_kinozal.Controls.Add(Me.txtLogin_kz)
        Me.TabPage_kinozal.Controls.Add(Me.Label4)
        Me.TabPage_kinozal.Location = New System.Drawing.Point(4, 22)
        Me.TabPage_kinozal.Name = "TabPage_kinozal"
        Me.TabPage_kinozal.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_kinozal.Size = New System.Drawing.Size(525, 78)
        Me.TabPage_kinozal.TabIndex = 1
        Me.TabPage_kinozal.Text = "kinozal"
        '
        'lblRegistration_kz
        '
        Me.lblRegistration_kz.AutoSize = True
        Me.lblRegistration_kz.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblRegistration_kz.Location = New System.Drawing.Point(428, 18)
        Me.lblRegistration_kz.Name = "lblRegistration_kz"
        Me.lblRegistration_kz.Size = New System.Drawing.Size(72, 13)
        Me.lblRegistration_kz.TabIndex = 8
        Me.lblRegistration_kz.Text = "Регистрация"
        '
        'txtPassword_kz
        '
        Me.txtPassword_kz.Location = New System.Drawing.Point(73, 41)
        Me.txtPassword_kz.MaxLength = 100
        Me.txtPassword_kz.Name = "txtPassword_kz"
        Me.txtPassword_kz.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword_kz.Size = New System.Drawing.Size(235, 20)
        Me.txtPassword_kz.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 44)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Пароль:"
        '
        'txtLogin_kz
        '
        Me.txtLogin_kz.Location = New System.Drawing.Point(73, 15)
        Me.txtLogin_kz.MaxLength = 100
        Me.txtLogin_kz.Name = "txtLogin_kz"
        Me.txtLogin_kz.Size = New System.Drawing.Size(235, 20)
        Me.txtLogin_kz.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Логин:"
        '
        'frmCredentials
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(557, 250)
        Me.Controls.Add(Me.tbCredentialsControl)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.grpProxyBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCredentials"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Авторизация"
        Me.grpProxyBox.ResumeLayout(False)
        Me.grpProxyBox.PerformLayout()
        Me.tbCredentialsControl.ResumeLayout(False)
        Me.TabPage_rutracker.ResumeLayout(False)
        Me.TabPage_rutracker.PerformLayout()
        Me.TabPage_kinozal.ResumeLayout(False)
        Me.TabPage_kinozal.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpProxyBox As System.Windows.Forms.GroupBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents txtLogin_rt As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtPassword_rt As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tbCredentialsControl As System.Windows.Forms.TabControl
    Friend WithEvents TabPage_rutracker As System.Windows.Forms.TabPage
    Friend WithEvents TabPage_kinozal As System.Windows.Forms.TabPage
    Friend WithEvents txtPassword_kz As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtLogin_kz As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkUseProxy As System.Windows.Forms.CheckBox
    Friend WithEvents txtProxyPort As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtProxyAddress As System.Windows.Forms.TextBox
    Friend WithEvents lblRegistration_rt As System.Windows.Forms.Label
    Friend WithEvents lblRegistration_kz As System.Windows.Forms.Label
End Class
