<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewAccountUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NewAccountUI))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.accountsTVPanel = New System.Windows.Forms.Panel()
        Me.selectParentBT = New System.Windows.Forms.Button()
        Me.ButtonsIL = New System.Windows.Forms.ImageList(Me.components)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.bs_item_RB = New System.Windows.Forms.RadioButton()
        Me.flux_RB = New System.Windows.Forms.RadioButton()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.parentTB = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.nameTB = New System.Windows.Forms.TextBox()
        Me.formulaCB = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.formatCB = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.recompute_RB = New System.Windows.Forms.RadioButton()
        Me.aggregation_RB = New System.Windows.Forms.RadioButton()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.typeCB = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CancelBT = New System.Windows.Forms.Button()
        Me.CreateAccountBT = New System.Windows.Forms.Button()
        Me.ButtonIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.accountsIL = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.accountsTVPanel)
        Me.Panel1.Controls.Add(Me.selectParentBT)
        Me.Panel1.Controls.Add(Me.TableLayoutPanel1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.CancelBT)
        Me.Panel1.Controls.Add(Me.CreateAccountBT)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1084, 442)
        Me.Panel1.TabIndex = 20
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.SystemColors.MenuText
        Me.Label9.Location = New System.Drawing.Point(731, 42)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(81, 13)
        Me.Label9.TabIndex = 26
        Me.Label9.Text = "Parent Account"
        '
        'accountsTVPanel
        '
        Me.accountsTVPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.accountsTVPanel.Location = New System.Drawing.Point(733, 61)
        Me.accountsTVPanel.Name = "accountsTVPanel"
        Me.accountsTVPanel.Size = New System.Drawing.Size(339, 369)
        Me.accountsTVPanel.TabIndex = 25
        '
        'selectParentBT
        '
        Me.selectParentBT.FlatAppearance.BorderColor = System.Drawing.Color.Purple
        Me.selectParentBT.FlatAppearance.BorderSize = 0
        Me.selectParentBT.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control
        Me.selectParentBT.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control
        Me.selectParentBT.ImageKey = "favicon(7).ico"
        Me.selectParentBT.ImageList = Me.ButtonsIL
        Me.selectParentBT.Location = New System.Drawing.Point(668, 62)
        Me.selectParentBT.Name = "selectParentBT"
        Me.selectParentBT.Size = New System.Drawing.Size(22, 22)
        Me.selectParentBT.TabIndex = 24
        Me.selectParentBT.UseVisualStyleBackColor = True
        '
        'ButtonsIL
        '
        Me.ButtonsIL.ImageStream = CType(resources.GetObject("ButtonsIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonsIL.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonsIL.Images.SetKeyName(0, "delete-2-xxl.png")
        Me.ButtonsIL.Images.SetKeyName(1, "favicon(97).ico")
        Me.ButtonsIL.Images.SetKeyName(2, "favicon(76).ico")
        Me.ButtonsIL.Images.SetKeyName(3, "favicon(7).ico")
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.75513!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.24487!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox2, 1, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.Label8, 0, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.Label4, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.parentTB, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label5, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.nameTB, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.formulaCB, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label7, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.formatCB, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox1, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.Label6, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.typeCB, 1, 4)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(28, 61)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 7
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28572!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(633, 239)
        Me.TableLayoutPanel1.TabIndex = 23
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.bs_item_RB)
        Me.GroupBox2.Controls.Add(Me.flux_RB)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(219, 204)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(414, 35)
        Me.GroupBox2.TabIndex = 13
        Me.GroupBox2.TabStop = False
        '
        'bs_item_RB
        '
        Me.bs_item_RB.AutoSize = True
        Me.bs_item_RB.Location = New System.Drawing.Point(167, 12)
        Me.bs_item_RB.Name = "bs_item_RB"
        Me.bs_item_RB.Size = New System.Drawing.Size(131, 17)
        Me.bs_item_RB.TabIndex = 27
        Me.bs_item_RB.TabStop = True
        Me.bs_item_RB.Text = "End of Period FX Rate"
        Me.bs_item_RB.UseVisualStyleBackColor = True
        '
        'flux_RB
        '
        Me.flux_RB.AutoSize = True
        Me.flux_RB.Location = New System.Drawing.Point(39, 12)
        Me.flux_RB.Name = "flux_RB"
        Me.flux_RB.Size = New System.Drawing.Size(107, 17)
        Me.flux_RB.TabIndex = 26
        Me.flux_RB.TabStop = True
        Me.flux_RB.Text = "Average FX Rate"
        Me.flux_RB.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(3, 204)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(105, 13)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "Currency Conversion"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 34)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Account's Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Parent Account"
        '
        'parentTB
        '
        Me.parentTB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.parentTB.Enabled = False
        Me.parentTB.Location = New System.Drawing.Point(222, 3)
        Me.parentTB.Name = "parentTB"
        Me.parentTB.Size = New System.Drawing.Size(408, 20)
        Me.parentTB.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(3, 136)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(113, 13)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Monetary/ Operational"
        '
        'nameTB
        '
        Me.nameTB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.nameTB.Location = New System.Drawing.Point(222, 37)
        Me.nameTB.Name = "nameTB"
        Me.nameTB.Size = New System.Drawing.Size(408, 20)
        Me.nameTB.TabIndex = 6
        '
        'formulaCB
        '
        Me.formulaCB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.formulaCB.FormattingEnabled = True
        Me.formulaCB.Location = New System.Drawing.Point(222, 71)
        Me.formulaCB.Name = "formulaCB"
        Me.formulaCB.Size = New System.Drawing.Size(408, 21)
        Me.formulaCB.TabIndex = 7
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(3, 170)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(104, 13)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "Consolidation Option"
        '
        'formatCB
        '
        Me.formatCB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.formatCB.FormattingEnabled = True
        Me.formatCB.Location = New System.Drawing.Point(222, 105)
        Me.formatCB.Name = "formatCB"
        Me.formatCB.Size = New System.Drawing.Size(408, 21)
        Me.formatCB.TabIndex = 9
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.recompute_RB)
        Me.GroupBox1.Controls.Add(Me.aggregation_RB)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(219, 170)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(414, 34)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        '
        'recompute_RB
        '
        Me.recompute_RB.AutoSize = True
        Me.recompute_RB.Location = New System.Drawing.Point(167, 11)
        Me.recompute_RB.Name = "recompute_RB"
        Me.recompute_RB.Size = New System.Drawing.Size(97, 17)
        Me.recompute_RB.TabIndex = 23
        Me.recompute_RB.TabStop = True
        Me.recompute_RB.Text = "Recomputation"
        Me.recompute_RB.UseVisualStyleBackColor = True
        '
        'aggregation_RB
        '
        Me.aggregation_RB.AutoSize = True
        Me.aggregation_RB.Location = New System.Drawing.Point(39, 11)
        Me.aggregation_RB.Name = "aggregation_RB"
        Me.aggregation_RB.Size = New System.Drawing.Size(82, 17)
        Me.aggregation_RB.TabIndex = 22
        Me.aggregation_RB.TabStop = True
        Me.aggregation_RB.Text = "Aggregation"
        Me.aggregation_RB.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 68)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(76, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Formula/ Input"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 102)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Account's Format"
        '
        'typeCB
        '
        Me.typeCB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.typeCB.FormattingEnabled = True
        Me.typeCB.Location = New System.Drawing.Point(222, 139)
        Me.typeCB.Name = "typeCB"
        Me.typeCB.Size = New System.Drawing.Size(408, 21)
        Me.typeCB.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(27, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(204, 13)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Populate New Account Information"
        '
        'CancelBT
        '
        Me.CancelBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CancelBT.ImageKey = "delete-2-xxl.png"
        Me.CancelBT.ImageList = Me.ButtonsIL
        Me.CancelBT.Location = New System.Drawing.Point(432, 334)
        Me.CancelBT.Name = "CancelBT"
        Me.CancelBT.Size = New System.Drawing.Size(92, 25)
        Me.CancelBT.TabIndex = 21
        Me.CancelBT.Text = "Cancel"
        Me.CancelBT.UseVisualStyleBackColor = True
        '
        'CreateAccountBT
        '
        Me.CreateAccountBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CreateAccountBT.ImageKey = "favicon(76).ico"
        Me.CreateAccountBT.ImageList = Me.ButtonsIL
        Me.CreateAccountBT.Location = New System.Drawing.Point(569, 334)
        Me.CreateAccountBT.Name = "CreateAccountBT"
        Me.CreateAccountBT.Size = New System.Drawing.Size(92, 25)
        Me.CreateAccountBT.TabIndex = 20
        Me.CreateAccountBT.Text = "Create"
        Me.CreateAccountBT.UseVisualStyleBackColor = True
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
        'accountsIL
        '
        Me.accountsIL.ImageStream = CType(resources.GetObject("accountsIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.accountsIL.TransparentColor = System.Drawing.Color.Transparent
        Me.accountsIL.Images.SetKeyName(0, "favicon(217).ico")
        Me.accountsIL.Images.SetKeyName(1, "entity icon 3.jpg")
        Me.accountsIL.Images.SetKeyName(2, "imageres_9.ico")
        Me.accountsIL.Images.SetKeyName(3, "imageres_148.ico")
        Me.accountsIL.Images.SetKeyName(4, "imageres_10.ico")
        Me.accountsIL.Images.SetKeyName(5, "imageres_1013.ico")
        Me.accountsIL.Images.SetKeyName(6, "imageres_100.ico")
        Me.accountsIL.Images.SetKeyName(7, "star1.jpg")
        Me.accountsIL.Images.SetKeyName(8, "imageres_190.ico")
        Me.accountsIL.Images.SetKeyName(9, "imageres_81.ico")
        '
        'NewAccountUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1084, 442)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "NewAccountUI"
        Me.Text = "New Account"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CancelBT As System.Windows.Forms.Button
    Friend WithEvents CreateAccountBT As System.Windows.Forms.Button
    Friend WithEvents ButtonIcons As System.Windows.Forms.ImageList
    Friend WithEvents ButtonsIL As System.Windows.Forms.ImageList
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents parentTB As System.Windows.Forms.TextBox
    Friend WithEvents nameTB As System.Windows.Forms.TextBox
    Friend WithEvents formulaCB As System.Windows.Forms.ComboBox
    Friend WithEvents typeCB As System.Windows.Forms.ComboBox
    Friend WithEvents formatCB As System.Windows.Forms.ComboBox
    Friend WithEvents selectParentBT As System.Windows.Forms.Button
    Friend WithEvents accountsIL As System.Windows.Forms.ImageList
    Friend WithEvents accountsTVPanel As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents recompute_RB As System.Windows.Forms.RadioButton
    Friend WithEvents aggregation_RB As System.Windows.Forms.RadioButton
    Friend WithEvents bs_item_RB As System.Windows.Forms.RadioButton
    Friend WithEvents flux_RB As System.Windows.Forms.RadioButton
    Friend WithEvents Label9 As System.Windows.Forms.Label
End Class
