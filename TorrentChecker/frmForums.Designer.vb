<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmForums
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
        Me.btnAccept = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.grpFormatsBox = New System.Windows.Forms.GroupBox()
        Me.grpForumsBox = New System.Windows.Forms.GroupBox()
        Me.lblFilter = New System.Windows.Forms.Label()
        Me.txtFilter = New System.Windows.Forms.TextBox()
        Me.lsvForums = New TorrentChecker.clsListViewExt()
        Me.lsvFormats = New TorrentChecker.clsListViewExt()
        Me.grpFormatsBox.SuspendLayout()
        Me.grpForumsBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnAccept
        '
        Me.btnAccept.Location = New System.Drawing.Point(350, 443)
        Me.btnAccept.Name = "btnAccept"
        Me.btnAccept.Size = New System.Drawing.Size(98, 26)
        Me.btnAccept.TabIndex = 4
        Me.btnAccept.Text = "Применить"
        Me.btnAccept.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(454, 443)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(98, 26)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "Отмена"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'grpFormatsBox
        '
        Me.grpFormatsBox.Controls.Add(Me.lsvFormats)
        Me.grpFormatsBox.Location = New System.Drawing.Point(6, 252)
        Me.grpFormatsBox.Name = "grpFormatsBox"
        Me.grpFormatsBox.Size = New System.Drawing.Size(546, 185)
        Me.grpFormatsBox.TabIndex = 6
        Me.grpFormatsBox.TabStop = False
        Me.grpFormatsBox.Text = "Выбранные форматы"
        '
        'grpForumsBox
        '
        Me.grpForumsBox.Controls.Add(Me.lsvForums)
        Me.grpForumsBox.Controls.Add(Me.lblFilter)
        Me.grpForumsBox.Controls.Add(Me.txtFilter)
        Me.grpForumsBox.Location = New System.Drawing.Point(6, 2)
        Me.grpForumsBox.Name = "grpForumsBox"
        Me.grpForumsBox.Size = New System.Drawing.Size(546, 244)
        Me.grpForumsBox.TabIndex = 0
        Me.grpForumsBox.TabStop = False
        Me.grpForumsBox.Text = "Выбранные форумы"
        '
        'lblFilter
        '
        Me.lblFilter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFilter.AutoSize = True
        Me.lblFilter.Location = New System.Drawing.Point(3, 221)
        Me.lblFilter.Name = "lblFilter"
        Me.lblFilter.Size = New System.Drawing.Size(118, 13)
        Me.lblFilter.TabIndex = 3
        Me.lblFilter.Text = "Фильтр по названию:"
        '
        'txtFilter
        '
        Me.txtFilter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFilter.Location = New System.Drawing.Point(145, 218)
        Me.txtFilter.Name = "txtFilter"
        Me.txtFilter.Size = New System.Drawing.Size(398, 20)
        Me.txtFilter.TabIndex = 2
        '
        'lsvForums
        '
        Me.lsvForums.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lsvForums.FullRowSelect = True
        Me.lsvForums.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lsvForums.HideSelection = False
        Me.lsvForums.LabelWrap = False
        Me.lsvForums.Location = New System.Drawing.Point(3, 16)
        Me.lsvForums.Name = "lsvForums"
        Me.lsvForums.Size = New System.Drawing.Size(540, 196)
        Me.lsvForums.TabIndex = 1
        Me.lsvForums.UseCompatibleStateImageBehavior = False
        Me.lsvForums.View = System.Windows.Forms.View.Details
        '
        'lsvFormats
        '
        Me.lsvFormats.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lsvFormats.FullRowSelect = True
        Me.lsvFormats.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        Me.lsvFormats.HideSelection = False
        Me.lsvFormats.LabelWrap = False
        Me.lsvFormats.Location = New System.Drawing.Point(3, 16)
        Me.lsvFormats.MultiSelect = False
        Me.lsvFormats.Name = "lsvFormats"
        Me.lsvFormats.Size = New System.Drawing.Size(540, 166)
        Me.lsvFormats.TabIndex = 3
        Me.lsvFormats.UseCompatibleStateImageBehavior = False
        Me.lsvFormats.View = System.Windows.Forms.View.Details
        '
        'frmForums
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(557, 475)
        Me.Controls.Add(Me.grpForumsBox)
        Me.Controls.Add(Me.grpFormatsBox)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnAccept)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmForums"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Выбор форума"
        Me.grpFormatsBox.ResumeLayout(False)
        Me.grpForumsBox.ResumeLayout(False)
        Me.grpForumsBox.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnAccept As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents grpFormatsBox As System.Windows.Forms.GroupBox
    Friend WithEvents grpForumsBox As System.Windows.Forms.GroupBox
    Friend WithEvents txtFilter As System.Windows.Forms.TextBox
    Friend WithEvents lblFilter As System.Windows.Forms.Label
    Friend WithEvents lsvForums As TorrentChecker.clsListViewExt
    Friend WithEvents lsvFormats As TorrentChecker.clsListViewExt
End Class
