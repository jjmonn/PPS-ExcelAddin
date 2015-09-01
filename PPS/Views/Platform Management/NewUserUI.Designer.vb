<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewUserUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NewUserUI))
        Me.ButtonIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.UserIDTB = New System.Windows.Forms.TextBox()
        Me.emailLabel = New System.Windows.Forms.Label()
        Me.CancelBT = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.emailTB = New System.Windows.Forms.TextBox()
        Me.CreateBT = New System.Windows.Forms.Button()
        Me.ChooseEntityBT = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ConsoEntityTB = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CredentialTypeCB = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'ButtonIcons
        '
        Me.ButtonIcons.ImageStream = CType(resources.GetObject("ButtonIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonIcons.Images.SetKeyName(0, "imageres_89.ico")
        Me.ButtonIcons.Images.SetKeyName(1, "favicon(95).ico")
        Me.ButtonIcons.Images.SetKeyName(2, "imageres_99.ico")
        Me.ButtonIcons.Images.SetKeyName(3, "favicon(194).ico")
        Me.ButtonIcons.Images.SetKeyName(4, "favicon(76).ico")
        '
        'UserIDTB
        '
        Me.UserIDTB.Location = New System.Drawing.Point(184, 38)
        Me.UserIDTB.Name = "UserIDTB"
        Me.UserIDTB.Size = New System.Drawing.Size(231, 20)
        Me.UserIDTB.TabIndex = 15
        '
        'emailLabel
        '
        Me.emailLabel.AutoSize = True
        Me.emailLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.emailLabel.ImageKey = "imageres_99.ico"
        Me.emailLabel.ImageList = Me.ButtonIcons
        Me.emailLabel.Location = New System.Drawing.Point(441, 87)
        Me.emailLabel.Name = "emailLabel"
        Me.emailLabel.Size = New System.Drawing.Size(33, 20)
        Me.emailLabel.TabIndex = 26
        Me.emailLabel.Text = "      "
        Me.emailLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CancelBT
        '
        Me.CancelBT.FlatAppearance.BorderSize = 0
        Me.CancelBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CancelBT.ImageKey = "imageres_89.ico"
        Me.CancelBT.ImageList = Me.ButtonIcons
        Me.CancelBT.Location = New System.Drawing.Point(120, 264)
        Me.CancelBT.Name = "CancelBT"
        Me.CancelBT.Size = New System.Drawing.Size(91, 29)
        Me.CancelBT.TabIndex = 22
        Me.CancelBT.Text = "Cancel"
        Me.CancelBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CancelBT.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(48, 90)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(32, 13)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "Email"
        '
        'emailTB
        '
        Me.emailTB.Location = New System.Drawing.Point(184, 87)
        Me.emailTB.Name = "emailTB"
        Me.emailTB.Size = New System.Drawing.Size(231, 20)
        Me.emailTB.TabIndex = 16
        '
        'CreateBT
        '
        Me.CreateBT.FlatAppearance.BorderSize = 0
        Me.CreateBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CreateBT.ImageKey = "favicon(76).ico"
        Me.CreateBT.ImageList = Me.ButtonIcons
        Me.CreateBT.Location = New System.Drawing.Point(324, 264)
        Me.CreateBT.Name = "CreateBT"
        Me.CreateBT.Size = New System.Drawing.Size(91, 29)
        Me.CreateBT.TabIndex = 20
        Me.CreateBT.Text = "Create"
        Me.CreateBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CreateBT.UseVisualStyleBackColor = True
        '
        'ChooseEntityBT
        '
        Me.ChooseEntityBT.FlatAppearance.BorderSize = 0
        Me.ChooseEntityBT.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control
        Me.ChooseEntityBT.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight
        Me.ChooseEntityBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ChooseEntityBT.ImageKey = "favicon(194).ico"
        Me.ChooseEntityBT.ImageList = Me.ButtonIcons
        Me.ChooseEntityBT.Location = New System.Drawing.Point(181, 193)
        Me.ChooseEntityBT.Name = "ChooseEntityBT"
        Me.ChooseEntityBT.Size = New System.Drawing.Size(20, 20)
        Me.ChooseEntityBT.TabIndex = 24
        Me.ChooseEntityBT.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(48, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "User's ID"
        '
        'ConsoEntityTB
        '
        Me.ConsoEntityTB.Location = New System.Drawing.Point(211, 196)
        Me.ConsoEntityTB.Name = "ConsoEntityTB"
        Me.ConsoEntityTB.Size = New System.Drawing.Size(204, 20)
        Me.ConsoEntityTB.TabIndex = 19
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(48, 142)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 13)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Credential Type"
        '
        'CredentialTypeCB
        '
        Me.CredentialTypeCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CredentialTypeCB.FormattingEnabled = True
        Me.CredentialTypeCB.Location = New System.Drawing.Point(184, 139)
        Me.CredentialTypeCB.Name = "CredentialTypeCB"
        Me.CredentialTypeCB.Size = New System.Drawing.Size(231, 21)
        Me.CredentialTypeCB.TabIndex = 17
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(48, 199)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(83, 13)
        Me.Label3.TabIndex = 23
        Me.Label3.Text = "Credential Level"
        '
        'NewUserUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(528, 331)
        Me.Controls.Add(Me.UserIDTB)
        Me.Controls.Add(Me.emailLabel)
        Me.Controls.Add(Me.CancelBT)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.emailTB)
        Me.Controls.Add(Me.CreateBT)
        Me.Controls.Add(Me.ChooseEntityBT)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ConsoEntityTB)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.CredentialTypeCB)
        Me.Controls.Add(Me.Label3)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "NewUserUI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "New User"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonIcons As System.Windows.Forms.ImageList
    Friend WithEvents UserIDTB As System.Windows.Forms.TextBox
    Friend WithEvents emailLabel As System.Windows.Forms.Label
    Friend WithEvents CancelBT As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents emailTB As System.Windows.Forms.TextBox
    Friend WithEvents CreateBT As System.Windows.Forms.Button
    Friend WithEvents ChooseEntityBT As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ConsoEntityTB As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CredentialTypeCB As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
