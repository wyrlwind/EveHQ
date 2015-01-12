Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FrmAbout
        Inherits DevComponents.DotNetBar.Office2007Form

        'Form overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.lblVersion = New DevComponents.DotNetBar.Controls.ReflectionLabel()
            Me.lblEveHQLink = New System.Windows.Forms.LinkLabel()
            Me.wbCredits = New System.Windows.Forms.WebBrowser()
            Me.Panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'Panel1
            '
            Me.Panel1.BackColor = System.Drawing.Color.Black
            Me.Panel1.BackgroundImage = Global.EveHQ.My.Resources.Resources.EveHQSplash9
            Me.Panel1.Controls.Add(Me.lblVersion)
            Me.Panel1.Controls.Add(Me.lblEveHQLink)
            Me.Panel1.Location = New System.Drawing.Point(0, 0)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(600, 400)
            Me.Panel1.TabIndex = 0
            '
            'lblVersion
            '
            Me.lblVersion.BackColor = System.Drawing.Color.Transparent
            '
            '
            '
            Me.lblVersion.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
            Me.lblVersion.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, CType(0, Byte))
            Me.lblVersion.ForeColor = System.Drawing.Color.White
            Me.lblVersion.Location = New System.Drawing.Point(256, 292)
            Me.lblVersion.Name = "lblVersion"
            Me.lblVersion.Size = New System.Drawing.Size(190, 31)
            Me.lblVersion.TabIndex = 8
            Me.lblVersion.Text = "Version"
            '
            'lblEveHQLink
            '
            Me.lblEveHQLink.ActiveLinkColor = System.Drawing.Color.DarkTurquoise
            Me.lblEveHQLink.AutoSize = True
            Me.lblEveHQLink.BackColor = System.Drawing.Color.Transparent
            Me.lblEveHQLink.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblEveHQLink.ForeColor = System.Drawing.Color.PaleTurquoise
            Me.lblEveHQLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
            Me.lblEveHQLink.LinkColor = System.Drawing.Color.PaleTurquoise
            Me.lblEveHQLink.Location = New System.Drawing.Point(191, 377)
            Me.lblEveHQLink.Name = "lblEveHQLink"
            Me.lblEveHQLink.Size = New System.Drawing.Size(303, 13)
            Me.lblEveHQLink.TabIndex = 4
            Me.lblEveHQLink.TabStop = True
            Me.lblEveHQLink.Text = "Visit the forums at evehq.co for bug reporting and comments."
            Me.lblEveHQLink.VisitedLinkColor = System.Drawing.Color.PaleTurquoise
            '
            'wbCredits
            '
            Me.wbCredits.AllowNavigation = False
            Me.wbCredits.AllowWebBrowserDrop = False
            Me.wbCredits.Dock = System.Windows.Forms.DockStyle.Right
            Me.wbCredits.Location = New System.Drawing.Point(599, 0)
            Me.wbCredits.MinimumSize = New System.Drawing.Size(20, 20)
            Me.wbCredits.Name = "wbCredits"
            Me.wbCredits.ScriptErrorsSuppressed = True
            Me.wbCredits.ScrollBarsEnabled = False
            Me.wbCredits.Size = New System.Drawing.Size(195, 399)
            Me.wbCredits.TabIndex = 1
            '
            'FrmAbout
            '
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
            Me.ClientSize = New System.Drawing.Size(794, 399)
            Me.Controls.Add(Me.wbCredits)
            Me.Controls.Add(Me.Panel1)
            Me.EnableGlass = False
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "FrmAbout"
            Me.ShowIcon = False
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "About EveHQ"
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents lblEveHQLink As System.Windows.Forms.LinkLabel
        Friend WithEvents wbCredits As System.Windows.Forms.WebBrowser
        Private WithEvents lblVersion As DevComponents.DotNetBar.Controls.ReflectionLabel
    End Class
End NameSpace