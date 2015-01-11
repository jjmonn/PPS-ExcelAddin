<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChangePasswordUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ChangePasswordUI))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CloseBT = New System.Windows.Forms.Button()
        Me.ButtonIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.ResetPWDBT = New System.Windows.Forms.Button()
        Me.NewPwd2 = New System.Windows.Forms.TextBox()
        Me.NewPwd1 = New System.Windows.Forms.TextBox()
        Me.CurrentPwdTB = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.CloseBT)
        Me.Panel1.Controls.Add(Me.ResetPWDBT)
        Me.Panel1.Controls.Add(Me.NewPwd2)
        Me.Panel1.Controls.Add(Me.NewPwd1)
        Me.Panel1.Controls.Add(Me.CurrentPwdTB)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(447, 270)
        Me.Panel1.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(35, 176)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(119, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Validate New Password"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(35, 124)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "New Password"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(35, 75)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Current Password"
        '
        'CloseBT
        '
        Me.CloseBT.FlatAppearance.BorderColor = System.Drawing.Color.Purple
        Me.CloseBT.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control
        Me.CloseBT.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.CloseBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CloseBT.ImageIndex = 1
        Me.CloseBT.ImageList = Me.ButtonIcons
        Me.CloseBT.Location = New System.Drawing.Point(416, 12)
        Me.CloseBT.Name = "CloseBT"
        Me.CloseBT.Size = New System.Drawing.Size(18, 18)
        Me.CloseBT.TabIndex = 10
        Me.CloseBT.UseVisualStyleBackColor = True
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
        '
        'ResetPWDBT
        '
        Me.ResetPWDBT.Location = New System.Drawing.Point(297, 223)
        Me.ResetPWDBT.Name = "ResetPWDBT"
        Me.ResetPWDBT.Size = New System.Drawing.Size(103, 24)
        Me.ResetPWDBT.TabIndex = 3
        Me.ResetPWDBT.Text = "Reset Password"
        Me.ResetPWDBT.UseVisualStyleBackColor = True
        '
        'NewPwd2
        '
        Me.NewPwd2.Location = New System.Drawing.Point(185, 173)
        Me.NewPwd2.Name = "NewPwd2"
        Me.NewPwd2.Size = New System.Drawing.Size(216, 20)
        Me.NewPwd2.TabIndex = 2
        Me.NewPwd2.UseSystemPasswordChar = True
        '
        'NewPwd1
        '
        Me.NewPwd1.Location = New System.Drawing.Point(185, 121)
        Me.NewPwd1.Name = "NewPwd1"
        Me.NewPwd1.Size = New System.Drawing.Size(216, 20)
        Me.NewPwd1.TabIndex = 1
        Me.NewPwd1.UseSystemPasswordChar = True
        '
        'CurrentPwdTB
        '
        Me.CurrentPwdTB.Location = New System.Drawing.Point(185, 72)
        Me.CurrentPwdTB.Name = "CurrentPwdTB"
        Me.CurrentPwdTB.Size = New System.Drawing.Size(216, 20)
        Me.CurrentPwdTB.TabIndex = 0
        Me.CurrentPwdTB.UseSystemPasswordChar = True
        '
        'ChangePasswordUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(447, 270)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "ChangePasswordUI"
        Me.Text = "ChangePasswordUI"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ResetPWDBT As System.Windows.Forms.Button
    Friend WithEvents NewPwd2 As System.Windows.Forms.TextBox
    Friend WithEvents NewPwd1 As System.Windows.Forms.TextBox
    Friend WithEvents CurrentPwdTB As System.Windows.Forms.TextBox
    Friend WithEvents CloseBT As System.Windows.Forms.Button
    Friend WithEvents ButtonIcons As System.Windows.Forms.ImageList
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
