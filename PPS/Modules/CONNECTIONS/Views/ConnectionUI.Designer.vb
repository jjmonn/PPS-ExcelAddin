<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConnectionUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ConnectionUI))
        Me.mainPanel = New System.Windows.Forms.Panel()
        Me.IDsPanel = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.IDTB = New System.Windows.Forms.TextBox()
        Me.ConnectionBT = New System.Windows.Forms.Button()
        Me.PWDTB = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CloseBT = New System.Windows.Forms.Button()
        Me.ButtonIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.mainPanel.SuspendLayout()
        Me.IDsPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'mainPanel
        '
        Me.mainPanel.Controls.Add(Me.IDsPanel)
        Me.mainPanel.Controls.Add(Me.CloseBT)
        Me.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.mainPanel.Location = New System.Drawing.Point(0, 0)
        Me.mainPanel.Name = "mainPanel"
        Me.mainPanel.Size = New System.Drawing.Size(444, 259)
        Me.mainPanel.TabIndex = 0
        '
        'IDsPanel
        '
        Me.IDsPanel.Controls.Add(Me.Label1)
        Me.IDsPanel.Controls.Add(Me.IDTB)
        Me.IDsPanel.Controls.Add(Me.ConnectionBT)
        Me.IDsPanel.Controls.Add(Me.PWDTB)
        Me.IDsPanel.Controls.Add(Me.Label2)
        Me.IDsPanel.Location = New System.Drawing.Point(35, 53)
        Me.IDsPanel.Name = "IDsPanel"
        Me.IDsPanel.Size = New System.Drawing.Size(374, 194)
        Me.IDsPanel.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(25, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "User ID"
        '
        'IDTB
        '
        Me.IDTB.Location = New System.Drawing.Point(109, 34)
        Me.IDTB.Name = "IDTB"
        Me.IDTB.Size = New System.Drawing.Size(196, 20)
        Me.IDTB.TabIndex = 1
        '
        'ConnectionBT
        '
        Me.ConnectionBT.Location = New System.Drawing.Point(267, 151)
        Me.ConnectionBT.Name = "ConnectionBT"
        Me.ConnectionBT.Size = New System.Drawing.Size(89, 21)
        Me.ConnectionBT.TabIndex = 3
        Me.ConnectionBT.Text = "Connect"
        Me.ConnectionBT.UseVisualStyleBackColor = True
        '
        'PWDTB
        '
        Me.PWDTB.Location = New System.Drawing.Point(109, 90)
        Me.PWDTB.Name = "PWDTB"
        Me.PWDTB.Size = New System.Drawing.Size(196, 20)
        Me.PWDTB.TabIndex = 2
        Me.PWDTB.UseSystemPasswordChar = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(25, 93)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Password"
        '
        'CloseBT
        '
        Me.CloseBT.FlatAppearance.BorderColor = System.Drawing.Color.Purple
        Me.CloseBT.FlatAppearance.BorderSize = 0
        Me.CloseBT.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control
        Me.CloseBT.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.CloseBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CloseBT.ImageIndex = 6
        Me.CloseBT.ImageList = Me.ButtonIcons
        Me.CloseBT.Location = New System.Drawing.Point(413, 4)
        Me.CloseBT.Name = "CloseBT"
        Me.CloseBT.Size = New System.Drawing.Size(27, 27)
        Me.CloseBT.TabIndex = 9
        Me.CloseBT.UseVisualStyleBackColor = True
        '
        'ButtonIcons
        '
        Me.ButtonIcons.ImageStream = CType(resources.GetObject("ButtonIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonIcons.Images.SetKeyName(0, "imageres_89.ico")
        Me.ButtonIcons.Images.SetKeyName(1, "close.ico")
        Me.ButtonIcons.Images.SetKeyName(2, "favicon(95).ico")
        Me.ButtonIcons.Images.SetKeyName(3, "submit 1 ok.ico")
        Me.ButtonIcons.Images.SetKeyName(4, "favicon(97).ico")
        Me.ButtonIcons.Images.SetKeyName(5, "imageres_99.ico")
        Me.ButtonIcons.Images.SetKeyName(6, "Close_Box_Red.ico")
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        '
        'ConnectionUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(444, 259)
        Me.Controls.Add(Me.mainPanel)
        Me.Name = "ConnectionUI"
        Me.Text = "ConnectionUI"
        Me.mainPanel.ResumeLayout(False)
        Me.IDsPanel.ResumeLayout(False)
        Me.IDsPanel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents mainPanel As System.Windows.Forms.Panel
    Friend WithEvents ConnectionBT As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PWDTB As System.Windows.Forms.TextBox
    Friend WithEvents IDTB As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CloseBT As System.Windows.Forms.Button
    Friend WithEvents ButtonIcons As System.Windows.Forms.ImageList
    Friend WithEvents IDsPanel As System.Windows.Forms.Panel
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
End Class
