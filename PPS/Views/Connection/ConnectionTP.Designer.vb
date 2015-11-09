<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConnectionTP
    Inherits AddinExpress.XL.ADXExcelTaskPane

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ConnectionTP))
        Me.m_userLabel = New System.Windows.Forms.Label()
        Me.userNameTextBox = New System.Windows.Forms.TextBox()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.passwordTextBox = New System.Windows.Forms.TextBox()
        Me.m_passwordLabel = New System.Windows.Forms.Label()
        Me.CPPanel = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ConnectionBT = New VIBlend.WinForms.Controls.vButton()
        Me.CancelBT = New VIBlend.WinForms.Controls.vButton()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'm_userLabel
        '
        Me.m_userLabel.AutoSize = True
        Me.m_userLabel.Location = New System.Drawing.Point(99, 58)
        Me.m_userLabel.Name = "m_userLabel"
        Me.m_userLabel.Size = New System.Drawing.Size(117, 15)
        Me.m_userLabel.TabIndex = 1
        Me.m_userLabel.Text = "[connection.user_id]"
        '
        'userNameTextBox
        '
        Me.userNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.userNameTextBox.Location = New System.Drawing.Point(53, 90)
        Me.userNameTextBox.Name = "userNameTextBox"
        Me.userNameTextBox.Size = New System.Drawing.Size(140, 21)
        Me.userNameTextBox.TabIndex = 2
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "cloud.ico")
        Me.ImageList1.Images.SetKeyName(1, "cloud.ico")
        Me.ImageList1.Images.SetKeyName(2, "upload.ico")
        Me.ImageList1.Images.SetKeyName(3, "delete.ico")
        '
        'passwordTextBox
        '
        Me.passwordTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.passwordTextBox.Location = New System.Drawing.Point(53, 168)
        Me.passwordTextBox.Name = "passwordTextBox"
        Me.passwordTextBox.Size = New System.Drawing.Size(140, 21)
        Me.passwordTextBox.TabIndex = 3
        Me.passwordTextBox.UseSystemPasswordChar = True
        '
        'm_passwordLabel
        '
        Me.m_passwordLabel.AutoSize = True
        Me.m_passwordLabel.Location = New System.Drawing.Point(93, 136)
        Me.m_passwordLabel.Name = "m_passwordLabel"
        Me.m_passwordLabel.Size = New System.Drawing.Size(129, 15)
        Me.m_passwordLabel.TabIndex = 6
        Me.m_passwordLabel.Text = "[connection.password]"
        '
        'CPPanel
        '
        Me.CPPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CPPanel.Location = New System.Drawing.Point(81, 223)
        Me.CPPanel.Name = "CPPanel"
        Me.CPPanel.Size = New System.Drawing.Size(85, 85)
        Me.CPPanel.TabIndex = 7
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ConnectionBT)
        Me.Panel1.Controls.Add(Me.CancelBT)
        Me.Panel1.Controls.Add(Me.CPPanel)
        Me.Panel1.Controls.Add(Me.m_userLabel)
        Me.Panel1.Controls.Add(Me.userNameTextBox)
        Me.Panel1.Controls.Add(Me.m_passwordLabel)
        Me.Panel1.Controls.Add(Me.passwordTextBox)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(256, 673)
        Me.Panel1.TabIndex = 14
        '
        'ConnectionBT
        '
        Me.ConnectionBT.AllowAnimations = True
        Me.ConnectionBT.BackColor = System.Drawing.Color.Transparent
        Me.ConnectionBT.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ConnectionBT.ImageKey = "upload.ico"
        Me.ConnectionBT.ImageList = Me.ImageList1
        Me.ConnectionBT.Location = New System.Drawing.Point(73, 240)
        Me.ConnectionBT.Name = "ConnectionBT"
        Me.ConnectionBT.RoundedCornersMask = CType(15, Byte)
        Me.ConnectionBT.Size = New System.Drawing.Size(100, 47)
        Me.ConnectionBT.TabIndex = 4
        Me.ConnectionBT.Text = "[connection.connection]"
        Me.ConnectionBT.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ConnectionBT.UseVisualStyleBackColor = False
        Me.ConnectionBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'CancelBT
        '
        Me.CancelBT.AllowAnimations = True
        Me.CancelBT.BackColor = System.Drawing.Color.Transparent
        Me.CancelBT.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CancelBT.ImageKey = "delete.ico"
        Me.CancelBT.ImageList = Me.ImageList1
        Me.CancelBT.Location = New System.Drawing.Point(73, 357)
        Me.CancelBT.Name = "CancelBT"
        Me.CancelBT.RoundedCornersMask = CType(15, Byte)
        Me.CancelBT.Size = New System.Drawing.Size(100, 47)
        Me.CancelBT.TabIndex = 5
        Me.CancelBT.Text = "[general.cancel]"
        Me.CancelBT.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CancelBT.UseVisualStyleBackColor = False
        Me.CancelBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'ConnectionTP
        '
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(256, 673)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "ConnectionTP"
        Me.Text = "[connection.connection]"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents m_userLabel As System.Windows.Forms.Label
    Friend WithEvents userNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents passwordTextBox As System.Windows.Forms.TextBox
    Friend WithEvents m_passwordLabel As System.Windows.Forms.Label
    Friend WithEvents CPPanel As System.Windows.Forms.Panel
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ConnectionBT As VIBlend.WinForms.Controls.vButton
    Friend WithEvents CancelBT As VIBlend.WinForms.Controls.vButton

End Class
