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
            Me.Label1 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.linkTeam = New System.Windows.Forms.LinkLabel()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.Label7 = New System.Windows.Forms.Label()
            Me.linkArtwork = New System.Windows.Forms.LinkLabel()
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
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(607, 19)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(110, 16)
            Me.Label1.TabIndex = 1
            Me.Label1.Text = "EveHQ Credits"
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Location = New System.Drawing.Point(607, 52)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(105, 13)
            Me.Label2.TabIndex = 2
            Me.Label2.Text = "Current contributors :"
            '
            'linkTeam
            '
            Me.linkTeam.AutoSize = True
            Me.linkTeam.Location = New System.Drawing.Point(607, 67)
            Me.linkTeam.Name = "linkTeam"
            Me.linkTeam.Size = New System.Drawing.Size(72, 13)
            Me.linkTeam.TabIndex = 3
            Me.linkTeam.TabStop = True
            Me.linkTeam.Text = "EveHQ Team"
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Location = New System.Drawing.Point(607, 99)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(103, 13)
            Me.Label3.TabIndex = 4
            Me.Label3.Text = "Former contributors :"
            '
            'Label4
            '
            Me.Label4.AutoEllipsis = True
            Me.Label4.Location = New System.Drawing.Point(607, 118)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(176, 98)
            Me.Label4.TabIndex = 5
            Me.Label4.Text = "Drailen, Warlof Tutsimo, Darkwolf, Darmed Khan, Eowarian, farlin, geniusfreak, Md" & _
        "ram, Modescond, MoWe79, MrCue, Nauvus3x7, Quantix Blackstar, Rob Crowley, Saulvi" & _
        "n, Taedrin, Thorien"
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Location = New System.Drawing.Point(607, 222)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(132, 13)
            Me.Label5.TabIndex = 6
            Me.Label5.Text = "EveHQ Created by Drailen"
            '
            'Label6
            '
            Me.Label6.Location = New System.Drawing.Point(607, 260)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(155, 35)
            Me.Label6.TabIndex = 7
            Me.Label6.Text = "EVECacheParser Library By: Desmont McCallock"
            '
            'Label7
            '
            Me.Label7.AutoSize = True
            Me.Label7.Location = New System.Drawing.Point(607, 318)
            Me.Label7.Name = "Label7"
            Me.Label7.Size = New System.Drawing.Size(49, 13)
            Me.Label7.TabIndex = 8
            Me.Label7.Text = "Artwork :"
            '
            'linkArtwork
            '
            Me.linkArtwork.AutoSize = True
            Me.linkArtwork.Location = New System.Drawing.Point(691, 318)
            Me.linkArtwork.Name = "linkArtwork"
            Me.linkArtwork.Size = New System.Drawing.Size(71, 13)
            Me.linkArtwork.TabIndex = 9
            Me.linkArtwork.TabStop = True
            Me.linkArtwork.Text = "Foxgguy2001"
            '
            'FrmAbout
            '
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
            Me.BackColor = System.Drawing.Color.White
            Me.ClientSize = New System.Drawing.Size(794, 399)
            Me.Controls.Add(Me.linkArtwork)
            Me.Controls.Add(Me.Label7)
            Me.Controls.Add(Me.Label6)
            Me.Controls.Add(Me.Label5)
            Me.Controls.Add(Me.Label4)
            Me.Controls.Add(Me.Label3)
            Me.Controls.Add(Me.linkTeam)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.Panel1)
            Me.DoubleBuffered = True
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
            Me.PerformLayout()

        End Sub
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents lblEveHQLink As System.Windows.Forms.LinkLabel
        Private WithEvents lblVersion As DevComponents.DotNetBar.Controls.ReflectionLabel
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents linkTeam As System.Windows.Forms.LinkLabel
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents Label4 As System.Windows.Forms.Label
        Friend WithEvents Label5 As System.Windows.Forms.Label
        Friend WithEvents Label6 As System.Windows.Forms.Label
        Friend WithEvents Label7 As System.Windows.Forms.Label
        Friend WithEvents linkArtwork As System.Windows.Forms.LinkLabel
    End Class
End NameSpace