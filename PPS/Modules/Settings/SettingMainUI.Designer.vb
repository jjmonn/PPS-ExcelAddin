<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SettingMainUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SettingMainUI))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.databasesCB = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PortTB = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CertificatesBT = New System.Windows.Forms.Button()
        Me.CertificatesPathTB = New System.Windows.Forms.TextBox()
        Me.DiconnectBT = New System.Windows.Forms.Button()
        Me.ButtonIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.ReinitPwdBT = New System.Windows.Forms.Button()
        Me.ConnectionBT = New System.Windows.Forms.Button()
        Me.ServerAddressTB = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PWDTB = New System.Windows.Forms.TextBox()
        Me.IDTB = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.ControlImages = New System.Windows.Forms.ImageList(Me.components)
        Me.ACFIcon = New System.Windows.Forms.ImageList(Me.components)
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.TabControl1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(745, 496)
        Me.Panel1.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Alignment = System.Windows.Forms.TabAlignment.Left
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed
        Me.TabControl1.ItemSize = New System.Drawing.Size(30, 120)
        Me.TabControl1.Location = New System.Drawing.Point(11, 45)
        Me.TabControl1.Multiline = True
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(720, 438)
        Me.TabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.databasesCB)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.PortTB)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.CertificatesBT)
        Me.TabPage1.Controls.Add(Me.CertificatesPathTB)
        Me.TabPage1.Controls.Add(Me.DiconnectBT)
        Me.TabPage1.Controls.Add(Me.ReinitPwdBT)
        Me.TabPage1.Controls.Add(Me.ConnectionBT)
        Me.TabPage1.Controls.Add(Me.ServerAddressTB)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.PWDTB)
        Me.TabPage1.Controls.Add(Me.IDTB)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Location = New System.Drawing.Point(124, 4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(592, 430)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Connection"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'databasesCB
        '
        Me.databasesCB.FormattingEnabled = True
        Me.databasesCB.Location = New System.Drawing.Point(151, 157)
        Me.databasesCB.Name = "databasesCB"
        Me.databasesCB.Size = New System.Drawing.Size(197, 21)
        Me.databasesCB.TabIndex = 19
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(27, 157)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 15)
        Me.Label5.TabIndex = 17
        Me.Label5.Text = "Database"
        '
        'PortTB
        '
        Me.PortTB.AcceptsReturn = True
        Me.PortTB.Location = New System.Drawing.Point(151, 104)
        Me.PortTB.Name = "PortTB"
        Me.PortTB.Size = New System.Drawing.Size(196, 20)
        Me.PortTB.TabIndex = 16
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(27, 107)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 15)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Port"
        '
        'CertificatesBT
        '
        Me.CertificatesBT.Location = New System.Drawing.Point(30, 349)
        Me.CertificatesBT.Name = "CertificatesBT"
        Me.CertificatesBT.Size = New System.Drawing.Size(96, 25)
        Me.CertificatesBT.TabIndex = 14
        Me.CertificatesBT.Text = "Certificates Path"
        Me.CertificatesBT.UseVisualStyleBackColor = True
        '
        'CertificatesPathTB
        '
        Me.CertificatesPathTB.Location = New System.Drawing.Point(151, 352)
        Me.CertificatesPathTB.Name = "CertificatesPathTB"
        Me.CertificatesPathTB.Size = New System.Drawing.Size(398, 20)
        Me.CertificatesPathTB.TabIndex = 12
        '
        'DiconnectBT
        '
        Me.DiconnectBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.DiconnectBT.ImageKey = "imageres_89.ico"
        Me.DiconnectBT.ImageList = Me.ButtonIcons
        Me.DiconnectBT.Location = New System.Drawing.Point(398, 93)
        Me.DiconnectBT.Name = "DiconnectBT"
        Me.DiconnectBT.Size = New System.Drawing.Size(151, 29)
        Me.DiconnectBT.TabIndex = 11
        Me.DiconnectBT.Text = "Disconnect"
        Me.DiconnectBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DiconnectBT.UseVisualStyleBackColor = True
        '
        'ButtonIcons
        '
        Me.ButtonIcons.ImageStream = CType(resources.GetObject("ButtonIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonIcons.Images.SetKeyName(0, "imageres_89.ico")
        Me.ButtonIcons.Images.SetKeyName(1, "favicon(95).ico")
        Me.ButtonIcons.Images.SetKeyName(2, "submit 1 ok.ico")
        Me.ButtonIcons.Images.SetKeyName(3, "favicon(97).ico")
        Me.ButtonIcons.Images.SetKeyName(4, "imageres_99.ico")
        Me.ButtonIcons.Images.SetKeyName(5, "favicon(70).ico")
        Me.ButtonIcons.Images.SetKeyName(6, "imageres_82.ico")
        Me.ButtonIcons.Images.SetKeyName(7, "refresh greay bcgd.bmp")
        '
        'ReinitPwdBT
        '
        Me.ReinitPwdBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ReinitPwdBT.ImageKey = "imageres_82.ico"
        Me.ReinitPwdBT.ImageList = Me.ButtonIcons
        Me.ReinitPwdBT.Location = New System.Drawing.Point(398, 293)
        Me.ReinitPwdBT.Name = "ReinitPwdBT"
        Me.ReinitPwdBT.Size = New System.Drawing.Size(151, 29)
        Me.ReinitPwdBT.TabIndex = 10
        Me.ReinitPwdBT.Text = "Reinitialize Password"
        Me.ReinitPwdBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ReinitPwdBT.UseVisualStyleBackColor = True
        '
        'ConnectionBT
        '
        Me.ConnectionBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ConnectionBT.ImageKey = "refresh greay bcgd.bmp"
        Me.ConnectionBT.ImageList = Me.ButtonIcons
        Me.ConnectionBT.Location = New System.Drawing.Point(398, 47)
        Me.ConnectionBT.Name = "ConnectionBT"
        Me.ConnectionBT.Size = New System.Drawing.Size(151, 29)
        Me.ConnectionBT.TabIndex = 9
        Me.ConnectionBT.Text = "Refresh Connection"
        Me.ConnectionBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ConnectionBT.UseVisualStyleBackColor = True
        '
        'ServerAddressTB
        '
        Me.ServerAddressTB.Location = New System.Drawing.Point(151, 52)
        Me.ServerAddressTB.Name = "ServerAddressTB"
        Me.ServerAddressTB.Size = New System.Drawing.Size(196, 20)
        Me.ServerAddressTB.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(27, 55)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 15)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Server"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 305)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 15)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Password"
        '
        'PWDTB
        '
        Me.PWDTB.Location = New System.Drawing.Point(151, 302)
        Me.PWDTB.Name = "PWDTB"
        Me.PWDTB.Size = New System.Drawing.Size(196, 20)
        Me.PWDTB.TabIndex = 2
        Me.PWDTB.UseSystemPasswordChar = True
        '
        'IDTB
        '
        Me.IDTB.Location = New System.Drawing.Point(151, 249)
        Me.IDTB.Name = "IDTB"
        Me.IDTB.Size = New System.Drawing.Size(196, 20)
        Me.IDTB.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 252)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "User ID"
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(124, 4)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(592, 430)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'ControlImages
        '
        Me.ControlImages.ImageStream = CType(resources.GetObject("ControlImages.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ControlImages.TransparentColor = System.Drawing.Color.Transparent
        Me.ControlImages.Images.SetKeyName(0, "close blue light.ico")
        Me.ControlImages.Images.SetKeyName(1, "favicon(99).ico")
        '
        'ACFIcon
        '
        Me.ACFIcon.ImageStream = CType(resources.GetObject("ACFIcon.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ACFIcon.TransparentColor = System.Drawing.Color.Transparent
        Me.ACFIcon.Images.SetKeyName(0, "ACF Square 2 .1Control bgd.png")
        '
        'SettingMainUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(745, 496)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "SettingMainUI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SettingMainUI"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents ControlImages As System.Windows.Forms.ImageList
    Friend WithEvents ButtonIcons As System.Windows.Forms.ImageList
    Friend WithEvents ACFIcon As System.Windows.Forms.ImageList
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PWDTB As System.Windows.Forms.TextBox
    Friend WithEvents IDTB As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ServerAddressTB As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ReinitPwdBT As System.Windows.Forms.Button
    Friend WithEvents ConnectionBT As System.Windows.Forms.Button
    Friend WithEvents DiconnectBT As System.Windows.Forms.Button
    Friend WithEvents CertificatesPathTB As System.Windows.Forms.TextBox
    Friend WithEvents CertificatesBT As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents PortTB As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents databasesCB As System.Windows.Forms.ComboBox
End Class
