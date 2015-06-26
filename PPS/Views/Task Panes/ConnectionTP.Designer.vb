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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.IDTB = New System.Windows.Forms.TextBox()
        Me.ConnectionBT = New System.Windows.Forms.Button()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.PWDTB = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CPPanel = New System.Windows.Forms.Panel()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 54)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 15)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "User ID"
        '
        'IDTB
        '
        Me.IDTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.IDTB.Location = New System.Drawing.Point(90, 54)
        Me.IDTB.Name = "IDTB"
        Me.IDTB.Size = New System.Drawing.Size(140, 21)
        Me.IDTB.TabIndex = 1
        '
        'ConnectionBT
        '
        Me.ConnectionBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ConnectionBT.ImageIndex = 1
        Me.ConnectionBT.ImageList = Me.ImageList1
        Me.ConnectionBT.Location = New System.Drawing.Point(139, 137)
        Me.ConnectionBT.Name = "ConnectionBT"
        Me.ConnectionBT.Size = New System.Drawing.Size(91, 31)
        Me.ConnectionBT.TabIndex = 3
        Me.ConnectionBT.Text = "Connect"
        Me.ConnectionBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ConnectionBT.UseVisualStyleBackColor = True
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "cloud.ico")
        Me.ImageList1.Images.SetKeyName(1, "cloud.ico")
        '
        'PWDTB
        '
        Me.PWDTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PWDTB.Location = New System.Drawing.Point(90, 94)
        Me.PWDTB.Name = "PWDTB"
        Me.PWDTB.Size = New System.Drawing.Size(140, 21)
        Me.PWDTB.TabIndex = 2
        Me.PWDTB.UseSystemPasswordChar = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 96)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 15)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Password"
        '
        'CPPanel
        '
        Me.CPPanel.Location = New System.Drawing.Point(90, 236)
        Me.CPPanel.Name = "CPPanel"
        Me.CPPanel.Size = New System.Drawing.Size(85, 85)
        Me.CPPanel.TabIndex = 13
        '
        'BackgroundWorker1
        '
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.CPPanel)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.IDTB)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.ConnectionBT)
        Me.Panel1.Controls.Add(Me.PWDTB)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(246, 673)
        Me.Panel1.TabIndex = 14
        '
        'ConnectionTP
        '
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(246, 673)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "ConnectionTP"
        Me.Text = "Connection"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents IDTB As System.Windows.Forms.TextBox
    Friend WithEvents ConnectionBT As System.Windows.Forms.Button
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents PWDTB As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CPPanel As System.Windows.Forms.Panel
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Panel1 As System.Windows.Forms.Panel

End Class
