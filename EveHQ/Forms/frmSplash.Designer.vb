Imports System.IO
Imports EveHQ.Common.Logging

Namespace Forms

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FrmSplash
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If

            If disposing Then

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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSplash))
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.lblDate = New DevComponents.DotNetBar.LabelX()
            Me.lblStatus = New DevComponents.DotNetBar.Controls.ReflectionLabel()
            Me.lblVersion = New DevComponents.DotNetBar.Controls.ReflectionLabel()
            Me.Panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'Panel1
            '
            Me.Panel1.BackgroundImage = Global.EveHQ.My.Resources.Resources.EveHQSplash9
            Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
            Me.Panel1.Controls.Add(Me.lblDate)
            Me.Panel1.Controls.Add(Me.lblStatus)
            Me.Panel1.Controls.Add(Me.lblVersion)
            Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Panel1.Location = New System.Drawing.Point(0, 0)
            Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(600, 400)
            Me.Panel1.TabIndex = 0
            '
            'lblDate
            '
            Me.lblDate.BackColor = System.Drawing.Color.Transparent
            '
            '
            '
            Me.lblDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
            Me.lblDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, CType(0, Byte))
            Me.lblDate.ForeColor = System.Drawing.Color.White
            Me.lblDate.Location = New System.Drawing.Point(4, 6)
            Me.lblDate.Margin = New System.Windows.Forms.Padding(4)
            Me.lblDate.Name = "lblDate"
            Me.lblDate.Size = New System.Drawing.Size(297, 14)
            Me.lblDate.TabIndex = 9
            Me.lblDate.Text = "Date"
            '
            'lblStatus
            '
            Me.lblStatus.BackColor = System.Drawing.Color.Transparent
            '
            '
            '
            Me.lblStatus.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
            Me.lblStatus.Font = New System.Drawing.Font("Tahoma", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, CType(0, Byte))
            Me.lblStatus.ForeColor = System.Drawing.Color.White
            Me.lblStatus.Location = New System.Drawing.Point(126, 365)
            Me.lblStatus.Margin = New System.Windows.Forms.Padding(4)
            Me.lblStatus.Name = "lblStatus"
            Me.lblStatus.Size = New System.Drawing.Size(470, 33)
            Me.lblStatus.TabIndex = 5
            Me.lblStatus.Text = "> Status:"
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
            Me.lblVersion.Location = New System.Drawing.Point(251, 292)
            Me.lblVersion.Margin = New System.Windows.Forms.Padding(4)
            Me.lblVersion.Name = "lblVersion"
            Me.lblVersion.Size = New System.Drawing.Size(253, 38)
            Me.lblVersion.TabIndex = 4
            Me.lblVersion.Text = "Version"
            '
            'FrmSplash
            '
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
            Me.ClientSize = New System.Drawing.Size(600, 400)
            Me.Controls.Add(Me.Panel1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Margin = New System.Windows.Forms.Padding(4)
            Me.Name = "FrmSplash"
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Loading EveHQ"
            Me.Panel1.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents Panel1 As System.Windows.Forms.Panel



        Public Sub New()

            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.


        End Sub
        Private WithEvents lblVersion As DevComponents.DotNetBar.Controls.ReflectionLabel
        Private WithEvents lblStatus As DevComponents.DotNetBar.Controls.ReflectionLabel
        Private WithEvents lblDate As DevComponents.DotNetBar.LabelX
    End Class
End NameSpace