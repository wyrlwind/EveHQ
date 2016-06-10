Namespace Controls.DBControls
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class DBCCorpInfo
        Inherits Widget

        'UserControl overrides dispose to clean up the component list.
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
            Me.components = New System.ComponentModel.Container()
            Me.pbCorp = New System.Windows.Forms.PictureBox()
            Me.lblCorp = New System.Windows.Forms.LinkLabel()
            Me.cboCorp = New System.Windows.Forms.ComboBox()
            Me.lblTicker = New System.Windows.Forms.Label()
            Me.lblCeo = New System.Windows.Forms.Label()
            Me.lblStation = New System.Windows.Forms.Label()
            Me.lblAlliance = New System.Windows.Forms.Label()
            Me.lblTaxRate = New System.Windows.Forms.Label()
            Me.lblMemberCount = New System.Windows.Forms.Label()
            Me.lblIsk = New System.Windows.Forms.Label()
            Me.AGPContent.SuspendLayout()
            CType(Me.pbConfig, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.pbCorp, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'lblHeader
            '
            Me.lblHeader.BackgroundStyle.Class = ""
            Me.lblHeader.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
            Me.lblHeader.Image = Global.EveHQ.My.Resources.Resources.corporation
            Me.lblHeader.Text = "Corporation Information"
            '
            'AGPContent
            '
            Me.AGPContent.CanvasColor = System.Drawing.SystemColors.Control
            Me.AGPContent.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
            Me.AGPContent.Controls.Add(Me.pbCorp)
            Me.AGPContent.Controls.Add(Me.lblCorp)
            Me.AGPContent.Controls.Add(Me.cboCorp)
            Me.AGPContent.Controls.Add(Me.lblTicker)
            Me.AGPContent.Controls.Add(Me.lblCeo)
            Me.AGPContent.Controls.Add(Me.lblStation)
            Me.AGPContent.Controls.Add(Me.lblAlliance)
            Me.AGPContent.Controls.Add(Me.lblTaxRate)
            Me.AGPContent.Controls.Add(Me.lblMemberCount)
            Me.AGPContent.Controls.Add(Me.lblIsk)
            Me.AGPContent.DisabledBackColor = System.Drawing.Color.Empty
            Me.AGPContent.Dock = System.Windows.Forms.DockStyle.Fill
            Me.AGPContent.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.AGPContent.Location = New System.Drawing.Point(0, 0)
            Me.AGPContent.Name = "AGPContent"
            Me.AGPContent.Size = New System.Drawing.Size(300, 220)
            Me.AGPContent.Style.Alignment = System.Drawing.StringAlignment.Center
            Me.AGPContent.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
            Me.AGPContent.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
            Me.AGPContent.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
            Me.AGPContent.Style.BorderColor.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.AGPContent.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
            Me.AGPContent.Style.GradientAngle = 90
            Me.AGPContent.TabIndex = 1
            Me.AGPContent.Controls.SetChildIndex(Me.lblIsk, 0)
            Me.AGPContent.Controls.SetChildIndex(Me.lblMemberCount, 0)
            Me.AGPContent.Controls.SetChildIndex(Me.lblTaxRate, 0)
            Me.AGPContent.Controls.SetChildIndex(Me.lblAlliance, 0)
            Me.AGPContent.Controls.SetChildIndex(Me.lblStation, 0)
            Me.AGPContent.Controls.SetChildIndex(Me.lblCeo, 0)
            Me.AGPContent.Controls.SetChildIndex(Me.lblTicker, 0)
            Me.AGPContent.Controls.SetChildIndex(Me.pbConfig, 0)
            Me.AGPContent.Controls.SetChildIndex(Me.lblHeader, 0)
            Me.AGPContent.Controls.SetChildIndex(Me.cboCorp, 0)
            Me.AGPContent.Controls.SetChildIndex(Me.lblCorp, 0)
            Me.AGPContent.Controls.SetChildIndex(Me.pbCorp, 0)
            '
            'pbCorp
            '
            Me.pbCorp.Location = New System.Drawing.Point(13, 64)
            Me.pbCorp.Name = "pbCorp"
            Me.pbCorp.Size = New System.Drawing.Size(54, 54)
            Me.pbCorp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.pbCorp.TabIndex = 34
            Me.pbCorp.TabStop = False
            '
            'lblCorp
            '
            Me.lblCorp.AutoSize = True
            Me.lblCorp.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
            Me.lblCorp.Location = New System.Drawing.Point(10, 40)
            Me.lblCorp.Name = "lblCorp"
            Me.lblCorp.Size = New System.Drawing.Size(68, 13)
            Me.lblCorp.TabIndex = 33
            Me.lblCorp.TabStop = True
            Me.lblCorp.Text = "Corporation:"
            '
            'cboCorp
            '
            Me.cboCorp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cboCorp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboCorp.FormattingEnabled = True
            Me.cboCorp.Location = New System.Drawing.Point(84, 37)
            Me.cboCorp.Name = "cboCorp"
            Me.cboCorp.Size = New System.Drawing.Size(198, 21)
            Me.cboCorp.Sorted = True
            Me.cboCorp.TabIndex = 24
            '
            'lblTicker
            '
            Me.lblTicker.AutoSize = True
            Me.lblTicker.Location = New System.Drawing.Point(74, 65)
            Me.lblTicker.Name = "lblTicker"
            Me.lblTicker.Size = New System.Drawing.Size(35, 13)
            Me.lblTicker.TabIndex = 2
            Me.lblTicker.Text = "Ticker"
            '
            'lblCeo
            '
            Me.lblCeo.AutoSize = True
            Me.lblCeo.Location = New System.Drawing.Point(74, 84)
            Me.lblCeo.Name = "lblCeo"
            Me.lblCeo.Size = New System.Drawing.Size(28, 13)
            Me.lblCeo.TabIndex = 2
            Me.lblCeo.Text = "CEO"
            '
            'lblStation
            '
            Me.lblStation.AutoSize = True
            Me.lblStation.Location = New System.Drawing.Point(73, 103)
            Me.lblStation.Name = "lblStation"
            Me.lblStation.Size = New System.Drawing.Size(41, 13)
            Me.lblStation.TabIndex = 2
            Me.lblStation.Text = "Station"
            '
            'lblAlliance
            '
            Me.lblAlliance.AutoSize = True
            Me.lblAlliance.Location = New System.Drawing.Point(13, 125)
            Me.lblAlliance.Name = "lblAlliance"
            Me.lblAlliance.Size = New System.Drawing.Size(43, 13)
            Me.lblAlliance.TabIndex = 2
            Me.lblAlliance.Text = "Alliance"
            '
            'lblTaxRate
            '
            Me.lblTaxRate.AutoSize = True
            Me.lblTaxRate.Location = New System.Drawing.Point(13, 146)
            Me.lblTaxRate.Name = "lblTaxRate"
            Me.lblTaxRate.Size = New System.Drawing.Size(51, 13)
            Me.lblTaxRate.TabIndex = 2
            Me.lblTaxRate.Text = "Tax Rate"
            '
            'lblMemberCount
            '
            Me.lblMemberCount.AutoSize = True
            Me.lblMemberCount.Location = New System.Drawing.Point(13, 169)
            Me.lblMemberCount.Name = "lblMemberCount"
            Me.lblMemberCount.Size = New System.Drawing.Size(77, 13)
            Me.lblMemberCount.TabIndex = 2
            Me.lblMemberCount.Text = "Member Count"
            '
            'lblIsk
            '
            Me.lblIsk.AutoSize = True
            Me.lblIsk.Location = New System.Drawing.Point(13, 192)
            Me.lblIsk.Name = "lblIsk"
            Me.lblIsk.Size = New System.Drawing.Size(21, 13)
            Me.lblIsk.TabIndex = 2
            Me.lblIsk.Text = "Isk"
            '
            'DBCCorpInfo
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Name = "DBCCorpInfo"
            Me.AGPContent.ResumeLayout(False)
            Me.AGPContent.PerformLayout()
            CType(Me.pbConfig, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.pbCorp, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents pbCorp As System.Windows.Forms.PictureBox
        Friend WithEvents lblCorp As System.Windows.Forms.LinkLabel
        Friend WithEvents cboCorp As System.Windows.Forms.ComboBox
        Friend WithEvents lblTicker As Label
        Friend WithEvents lblCeo As Label
        Friend WithEvents lblStation As Label
        Friend WithEvents lblAlliance As Label
        Friend WithEvents lblTaxRate As Label
        Friend WithEvents lblMemberCount As Label
        Friend WithEvents lblIsk As Label
    End Class
End Namespace
