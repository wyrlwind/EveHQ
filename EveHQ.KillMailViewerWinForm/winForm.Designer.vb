<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class winForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.gb_APIInformation = New System.Windows.Forms.GroupBox()
        Me.bt_GetCharacters = New System.Windows.Forms.Button()
        Me.lb_APIStatus = New System.Windows.Forms.Label()
        Me.tb_APIKey = New System.Windows.Forms.TextBox()
        Me.tb_UserID = New System.Windows.Forms.TextBox()
        Me.lb_APIKey = New System.Windows.Forms.Label()
        Me.lb_UserID = New System.Windows.Forms.Label()
        Me.cb_UseExistingAccount = New System.Windows.Forms.ComboBox()
        Me.rb_UseSpecificAPI = New System.Windows.Forms.RadioButton()
        Me.rb_UseExistingAccount = New System.Windows.Forms.RadioButton()
        Me.gb_AvaibleCharacters = New System.Windows.Forms.GroupBox()
        Me.lv_AvaibleCharacters = New System.Windows.Forms.ListView()
        Me.bt_FetchKillmails = New System.Windows.Forms.Button()
        Me.lb_KillmailSummary = New System.Windows.Forms.Label()
        Me.bt_UploadToBattleClinic = New System.Windows.Forms.Button()
        Me.lb_KillmailDetails = New System.Windows.Forms.Label()
        Me.bt_CopyKillmail = New System.Windows.Forms.Button()
        Me.bt_ExportToHQF = New System.Windows.Forms.Button()
        Me.lv_KillmailSummary = New System.Windows.Forms.ListView()
        Me.lv_cl_Victim = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lv_cl_KillType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lv_cl_KillDate = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.rtb_KillmailDetails = New System.Windows.Forms.RichTextBox()
        Me.lv_cl_KillID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.gb_APIInformation.SuspendLayout()
        Me.gb_AvaibleCharacters.SuspendLayout()
        Me.SuspendLayout()
        '
        'gb_APIInformation
        '
        Me.gb_APIInformation.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.gb_APIInformation.Controls.Add(Me.bt_GetCharacters)
        Me.gb_APIInformation.Controls.Add(Me.lb_APIStatus)
        Me.gb_APIInformation.Controls.Add(Me.tb_APIKey)
        Me.gb_APIInformation.Controls.Add(Me.tb_UserID)
        Me.gb_APIInformation.Controls.Add(Me.lb_APIKey)
        Me.gb_APIInformation.Controls.Add(Me.lb_UserID)
        Me.gb_APIInformation.Controls.Add(Me.cb_UseExistingAccount)
        Me.gb_APIInformation.Controls.Add(Me.rb_UseSpecificAPI)
        Me.gb_APIInformation.Controls.Add(Me.rb_UseExistingAccount)
        Me.gb_APIInformation.ForeColor = System.Drawing.SystemColors.ControlText
        Me.gb_APIInformation.Location = New System.Drawing.Point(12, 12)
        Me.gb_APIInformation.Name = "gb_APIInformation"
        Me.gb_APIInformation.Size = New System.Drawing.Size(395, 149)
        Me.gb_APIInformation.TabIndex = 0
        Me.gb_APIInformation.TabStop = False
        Me.gb_APIInformation.Text = "API Information"
        '
        'bt_GetCharacters
        '
        Me.bt_GetCharacters.Location = New System.Drawing.Point(288, 117)
        Me.bt_GetCharacters.Name = "bt_GetCharacters"
        Me.bt_GetCharacters.Size = New System.Drawing.Size(87, 23)
        Me.bt_GetCharacters.TabIndex = 8
        Me.bt_GetCharacters.Text = "Get Characters"
        Me.bt_GetCharacters.UseVisualStyleBackColor = True
        '
        'lb_APIStatus
        '
        Me.lb_APIStatus.AutoSize = True
        Me.lb_APIStatus.Location = New System.Drawing.Point(109, 122)
        Me.lb_APIStatus.Name = "lb_APIStatus"
        Me.lb_APIStatus.Size = New System.Drawing.Size(154, 13)
        Me.lb_APIStatus.TabIndex = 7
        Me.lb_APIStatus.Text = "API Status : Not yet connected"
        '
        'tb_APIKey
        '
        Me.tb_APIKey.Location = New System.Drawing.Point(105, 94)
        Me.tb_APIKey.Name = "tb_APIKey"
        Me.tb_APIKey.Size = New System.Drawing.Size(270, 20)
        Me.tb_APIKey.TabIndex = 6
        '
        'tb_UserID
        '
        Me.tb_UserID.Location = New System.Drawing.Point(105, 68)
        Me.tb_UserID.Name = "tb_UserID"
        Me.tb_UserID.Size = New System.Drawing.Size(100, 20)
        Me.tb_UserID.TabIndex = 5
        '
        'lb_APIKey
        '
        Me.lb_APIKey.AutoSize = True
        Me.lb_APIKey.Location = New System.Drawing.Point(50, 97)
        Me.lb_APIKey.Name = "lb_APIKey"
        Me.lb_APIKey.Size = New System.Drawing.Size(51, 13)
        Me.lb_APIKey.TabIndex = 4
        Me.lb_APIKey.Text = "API Key :"
        '
        'lb_UserID
        '
        Me.lb_UserID.AutoSize = True
        Me.lb_UserID.Location = New System.Drawing.Point(55, 71)
        Me.lb_UserID.Name = "lb_UserID"
        Me.lb_UserID.Size = New System.Drawing.Size(46, 13)
        Me.lb_UserID.TabIndex = 3
        Me.lb_UserID.Text = "UserID :"
        '
        'cb_UseExistingAccount
        '
        Me.cb_UseExistingAccount.FormattingEnabled = True
        Me.cb_UseExistingAccount.Location = New System.Drawing.Point(155, 20)
        Me.cb_UseExistingAccount.Name = "cb_UseExistingAccount"
        Me.cb_UseExistingAccount.Size = New System.Drawing.Size(121, 21)
        Me.cb_UseExistingAccount.TabIndex = 2
        '
        'rb_UseSpecificAPI
        '
        Me.rb_UseSpecificAPI.AutoSize = True
        Me.rb_UseSpecificAPI.Location = New System.Drawing.Point(16, 43)
        Me.rb_UseSpecificAPI.Name = "rb_UseSpecificAPI"
        Me.rb_UseSpecificAPI.Size = New System.Drawing.Size(111, 17)
        Me.rb_UseSpecificAPI.TabIndex = 1
        Me.rb_UseSpecificAPI.Text = "Use Specific API :"
        Me.rb_UseSpecificAPI.UseVisualStyleBackColor = True
        '
        'rb_UseExistingAccount
        '
        Me.rb_UseExistingAccount.AutoSize = True
        Me.rb_UseExistingAccount.Checked = True
        Me.rb_UseExistingAccount.Location = New System.Drawing.Point(16, 20)
        Me.rb_UseExistingAccount.Name = "rb_UseExistingAccount"
        Me.rb_UseExistingAccount.Size = New System.Drawing.Size(132, 17)
        Me.rb_UseExistingAccount.TabIndex = 0
        Me.rb_UseExistingAccount.TabStop = True
        Me.rb_UseExistingAccount.Text = "Use Existing Account :"
        Me.rb_UseExistingAccount.UseVisualStyleBackColor = True
        '
        'gb_AvaibleCharacters
        '
        Me.gb_AvaibleCharacters.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.gb_AvaibleCharacters.Controls.Add(Me.lv_AvaibleCharacters)
        Me.gb_AvaibleCharacters.Controls.Add(Me.bt_FetchKillmails)
        Me.gb_AvaibleCharacters.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gb_AvaibleCharacters.ForeColor = System.Drawing.SystemColors.ControlText
        Me.gb_AvaibleCharacters.Location = New System.Drawing.Point(413, 12)
        Me.gb_AvaibleCharacters.Name = "gb_AvaibleCharacters"
        Me.gb_AvaibleCharacters.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.gb_AvaibleCharacters.Size = New System.Drawing.Size(332, 149)
        Me.gb_AvaibleCharacters.TabIndex = 1
        Me.gb_AvaibleCharacters.TabStop = False
        Me.gb_AvaibleCharacters.Text = "Available Characters"
        '
        'lv_AvaibleCharacters
        '
        Me.lv_AvaibleCharacters.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.lv_AvaibleCharacters.HoverSelection = True
        Me.lv_AvaibleCharacters.Location = New System.Drawing.Point(9, 19)
        Me.lv_AvaibleCharacters.Name = "lv_AvaibleCharacters"
        Me.lv_AvaibleCharacters.Size = New System.Drawing.Size(317, 97)
        Me.lv_AvaibleCharacters.TabIndex = 12
        Me.lv_AvaibleCharacters.UseCompatibleStateImageBehavior = False
        Me.lv_AvaibleCharacters.View = System.Windows.Forms.View.List
        '
        'bt_FetchKillmails
        '
        Me.bt_FetchKillmails.Enabled = False
        Me.bt_FetchKillmails.Location = New System.Drawing.Point(230, 117)
        Me.bt_FetchKillmails.Name = "bt_FetchKillmails"
        Me.bt_FetchKillmails.Size = New System.Drawing.Size(87, 23)
        Me.bt_FetchKillmails.TabIndex = 10
        Me.bt_FetchKillmails.Text = "Fetch Killmails"
        Me.bt_FetchKillmails.UseVisualStyleBackColor = True
        '
        'lb_KillmailSummary
        '
        Me.lb_KillmailSummary.AutoSize = True
        Me.lb_KillmailSummary.BackColor = System.Drawing.Color.Transparent
        Me.lb_KillmailSummary.Location = New System.Drawing.Point(25, 177)
        Me.lb_KillmailSummary.Name = "lb_KillmailSummary"
        Me.lb_KillmailSummary.Size = New System.Drawing.Size(84, 13)
        Me.lb_KillmailSummary.TabIndex = 8
        Me.lb_KillmailSummary.Text = "Killmail Summary"
        '
        'bt_UploadToBattleClinic
        '
        Me.bt_UploadToBattleClinic.Location = New System.Drawing.Point(276, 167)
        Me.bt_UploadToBattleClinic.Name = "bt_UploadToBattleClinic"
        Me.bt_UploadToBattleClinic.Size = New System.Drawing.Size(118, 23)
        Me.bt_UploadToBattleClinic.TabIndex = 10
        Me.bt_UploadToBattleClinic.Text = "Upload to BattleClinic"
        Me.bt_UploadToBattleClinic.UseVisualStyleBackColor = True
        '
        'lb_KillmailDetails
        '
        Me.lb_KillmailDetails.AutoSize = True
        Me.lb_KillmailDetails.BackColor = System.Drawing.Color.Transparent
        Me.lb_KillmailDetails.Location = New System.Drawing.Point(419, 177)
        Me.lb_KillmailDetails.Name = "lb_KillmailDetails"
        Me.lb_KillmailDetails.Size = New System.Drawing.Size(73, 13)
        Me.lb_KillmailDetails.TabIndex = 11
        Me.lb_KillmailDetails.Text = "Killmail Details"
        '
        'bt_CopyKillmail
        '
        Me.bt_CopyKillmail.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.bt_CopyKillmail.Enabled = False
        Me.bt_CopyKillmail.Location = New System.Drawing.Point(413, 386)
        Me.bt_CopyKillmail.Name = "bt_CopyKillmail"
        Me.bt_CopyKillmail.Size = New System.Drawing.Size(107, 22)
        Me.bt_CopyKillmail.TabIndex = 12
        Me.bt_CopyKillmail.Text = "Copy Killmail"
        Me.bt_CopyKillmail.UseVisualStyleBackColor = True
        '
        'bt_ExportToHQF
        '
        Me.bt_ExportToHQF.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.bt_ExportToHQF.Enabled = False
        Me.bt_ExportToHQF.Location = New System.Drawing.Point(526, 387)
        Me.bt_ExportToHQF.Name = "bt_ExportToHQF"
        Me.bt_ExportToHQF.Size = New System.Drawing.Size(107, 21)
        Me.bt_ExportToHQF.TabIndex = 13
        Me.bt_ExportToHQF.Text = "Export to HQF"
        Me.bt_ExportToHQF.UseVisualStyleBackColor = True
        '
        'lv_KillmailSummary
        '
        Me.lv_KillmailSummary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lv_KillmailSummary.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.lv_cl_KillID, Me.lv_cl_Victim, Me.lv_cl_KillType, Me.lv_cl_KillDate})
        Me.lv_KillmailSummary.FullRowSelect = True
        Me.lv_KillmailSummary.Location = New System.Drawing.Point(12, 196)
        Me.lv_KillmailSummary.Name = "lv_KillmailSummary"
        Me.lv_KillmailSummary.Size = New System.Drawing.Size(395, 212)
        Me.lv_KillmailSummary.TabIndex = 13
        Me.lv_KillmailSummary.UseCompatibleStateImageBehavior = False
        Me.lv_KillmailSummary.View = System.Windows.Forms.View.Details
        '
        'lv_cl_Victim
        '
        Me.lv_cl_Victim.Text = "Victim"
        Me.lv_cl_Victim.Width = 128
        '
        'lv_cl_KillType
        '
        Me.lv_cl_KillType.Text = "Kill Type"
        Me.lv_cl_KillType.Width = 101
        '
        'lv_cl_KillDate
        '
        Me.lv_cl_KillDate.Text = "Kill Date"
        Me.lv_cl_KillDate.Width = 105
        '
        'rtb_KillmailDetails
        '
        Me.rtb_KillmailDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rtb_KillmailDetails.Location = New System.Drawing.Point(414, 196)
        Me.rtb_KillmailDetails.Name = "rtb_KillmailDetails"
        Me.rtb_KillmailDetails.Size = New System.Drawing.Size(325, 183)
        Me.rtb_KillmailDetails.TabIndex = 14
        Me.rtb_KillmailDetails.Text = ""
        '
        'lv_cl_KillID
        '
        Me.lv_cl_KillID.Text = "Kill ID"
        '
        'winForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(757, 423)
        Me.Controls.Add(Me.rtb_KillmailDetails)
        Me.Controls.Add(Me.lv_KillmailSummary)
        Me.Controls.Add(Me.bt_ExportToHQF)
        Me.Controls.Add(Me.bt_CopyKillmail)
        Me.Controls.Add(Me.lb_KillmailDetails)
        Me.Controls.Add(Me.bt_UploadToBattleClinic)
        Me.Controls.Add(Me.lb_KillmailSummary)
        Me.Controls.Add(Me.gb_AvaibleCharacters)
        Me.Controls.Add(Me.gb_APIInformation)
        Me.Name = "winForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "EveHQ Killmail Viewer winForm"
        Me.gb_APIInformation.ResumeLayout(False)
        Me.gb_APIInformation.PerformLayout()
        Me.gb_AvaibleCharacters.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents gb_APIInformation As Windows.Forms.GroupBox
    Friend WithEvents gb_AvaibleCharacters As Windows.Forms.GroupBox
    Friend WithEvents bt_GetCharacters As Windows.Forms.Button
    Friend WithEvents lb_APIStatus As Windows.Forms.Label
    Friend WithEvents tb_APIKey As Windows.Forms.TextBox
    Friend WithEvents tb_UserID As Windows.Forms.TextBox
    Friend WithEvents lb_APIKey As Windows.Forms.Label
    Friend WithEvents lb_UserID As Windows.Forms.Label
    Friend WithEvents cb_UseExistingAccount As Windows.Forms.ComboBox
    Friend WithEvents rb_UseSpecificAPI As Windows.Forms.RadioButton
    Friend WithEvents rb_UseExistingAccount As Windows.Forms.RadioButton
    Friend WithEvents lb_KillmailSummary As Windows.Forms.Label
    Friend WithEvents bt_UploadToBattleClinic As Windows.Forms.Button
    Friend WithEvents lb_KillmailDetails As Windows.Forms.Label
    Friend WithEvents bt_FetchKillmails As Windows.Forms.Button
    Friend WithEvents bt_CopyKillmail As Windows.Forms.Button
    Friend WithEvents bt_ExportToHQF As Windows.Forms.Button
    Friend WithEvents lv_AvaibleCharacters As Windows.Forms.ListView
    Friend WithEvents lv_KillmailSummary As Windows.Forms.ListView
    Friend WithEvents lv_cl_Victim As Windows.Forms.ColumnHeader
    Friend WithEvents lv_cl_KillType As Windows.Forms.ColumnHeader
    Friend WithEvents lv_cl_KillDate As Windows.Forms.ColumnHeader
    Friend WithEvents rtb_KillmailDetails As Windows.Forms.RichTextBox
    Friend WithEvents lv_cl_KillID As Windows.Forms.ColumnHeader
End Class
