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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.nameTB = New System.Windows.Forms.TextBox()
        Me.formulaCB = New System.Windows.Forms.ComboBox()
        Me.TypeCB = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.recompute_RB = New System.Windows.Forms.RadioButton()
        Me.aggregation_RB = New System.Windows.Forms.RadioButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.bs_item_RB = New System.Windows.Forms.RadioButton()
        Me.flux_RB = New System.Windows.Forms.RadioButton()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ParentAccountTreeComboBox = New VIBlend.WinForms.Controls.vTreeViewBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CancelBT = New System.Windows.Forms.Button()
        Me.ButtonsIL = New System.Windows.Forms.ImageList(Me.components)
        Me.CreateAccountBT = New System.Windows.Forms.Button()
        Me.ButtonIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.accountsIL = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.Controls.Add(Me.TableLayoutPanel1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.CancelBT)
        Me.Panel1.Controls.Add(Me.CreateAccountBT)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(688, 376)
        Me.Panel1.TabIndex = 20
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.75513!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.24487!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label4, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.nameTB, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.formulaCB, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TypeCB, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Label6, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox1, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Label7, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox2, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.Label8, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.ParentAccountTreeComboBox, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(28, 61)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 7
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(633, 239)
        Me.TableLayoutPanel1.TabIndex = 23
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 30)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(96, 15)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Account's Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 15)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Parent Account"
        '
        'nameTB
        '
        Me.nameTB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.nameTB.Location = New System.Drawing.Point(222, 33)
        Me.nameTB.Name = "nameTB"
        Me.nameTB.Size = New System.Drawing.Size(408, 20)
        Me.nameTB.TabIndex = 6
        '
        'formulaCB
        '
        Me.formulaCB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.formulaCB.FormattingEnabled = True
        Me.formulaCB.Location = New System.Drawing.Point(222, 63)
        Me.formulaCB.Name = "formulaCB"
        Me.formulaCB.Size = New System.Drawing.Size(408, 21)
        Me.formulaCB.TabIndex = 7
        '
        'TypeCB
        '
        Me.TypeCB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TypeCB.FormattingEnabled = True
        Me.TypeCB.Location = New System.Drawing.Point(222, 93)
        Me.TypeCB.Name = "TypeCB"
        Me.TypeCB.Size = New System.Drawing.Size(408, 21)
        Me.TypeCB.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 60)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(86, 15)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Formula/ Input"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 90)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(101, 15)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Account's Format"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.recompute_RB)
        Me.GroupBox1.Controls.Add(Me.aggregation_RB)
        Me.GroupBox1.Location = New System.Drawing.Point(219, 120)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(414, 30)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        '
        'recompute_RB
        '
        Me.recompute_RB.AutoSize = True
        Me.recompute_RB.Location = New System.Drawing.Point(167, 11)
        Me.recompute_RB.Name = "recompute_RB"
        Me.recompute_RB.Size = New System.Drawing.Size(109, 19)
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
        Me.aggregation_RB.Size = New System.Drawing.Size(91, 19)
        Me.aggregation_RB.TabIndex = 22
        Me.aggregation_RB.TabStop = True
        Me.aggregation_RB.Text = "Aggregation"
        Me.aggregation_RB.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(3, 120)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(121, 15)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "Consolidation Option"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.bs_item_RB)
        Me.GroupBox2.Controls.Add(Me.flux_RB)
        Me.GroupBox2.Location = New System.Drawing.Point(219, 150)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(414, 30)
        Me.GroupBox2.TabIndex = 13
        Me.GroupBox2.TabStop = False
        '
        'bs_item_RB
        '
        Me.bs_item_RB.AutoSize = True
        Me.bs_item_RB.Location = New System.Drawing.Point(167, 12)
        Me.bs_item_RB.Name = "bs_item_RB"
        Me.bs_item_RB.Size = New System.Drawing.Size(146, 19)
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
        Me.flux_RB.Size = New System.Drawing.Size(116, 19)
        Me.flux_RB.TabIndex = 26
        Me.flux_RB.TabStop = True
        Me.flux_RB.Text = "Average FX Rate"
        Me.flux_RB.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(3, 150)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(119, 15)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "Currency Conversion"
        '
        'ParentAccountTreeComboBox
        '
        Me.ParentAccountTreeComboBox.BackColor = System.Drawing.Color.White
        Me.ParentAccountTreeComboBox.BorderColor = System.Drawing.Color.Black
        Me.ParentAccountTreeComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ParentAccountTreeComboBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.ParentAccountTreeComboBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.ParentAccountTreeComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.ParentAccountTreeComboBox.Location = New System.Drawing.Point(222, 3)
        Me.ParentAccountTreeComboBox.Name = "ParentAccountTreeComboBox"
        Me.ParentAccountTreeComboBox.Size = New System.Drawing.Size(408, 24)
        Me.ParentAccountTreeComboBox.TabIndex = 14
        Me.ParentAccountTreeComboBox.Text = "Select Parent Account"
        Me.ParentAccountTreeComboBox.UseThemeBackColor = False
        Me.ParentAccountTreeComboBox.UseThemeDropDownArrowColor = True
        Me.ParentAccountTreeComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(27, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(227, 15)
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
        'ButtonsIL
        '
        Me.ButtonsIL.ImageStream = CType(resources.GetObject("ButtonsIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonsIL.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonsIL.Images.SetKeyName(0, "delete-2-xxl.png")
        Me.ButtonsIL.Images.SetKeyName(1, "favicon(97).ico")
        Me.ButtonsIL.Images.SetKeyName(2, "favicon(76).ico")
        Me.ButtonsIL.Images.SetKeyName(3, "favicon(7).ico")
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
        Me.ClientSize = New System.Drawing.Size(688, 376)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "NewAccountUI"
        Me.Text = "New Account"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
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
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents nameTB As System.Windows.Forms.TextBox
    Friend WithEvents formulaCB As System.Windows.Forms.ComboBox
    Friend WithEvents TypeCB As System.Windows.Forms.ComboBox
    Friend WithEvents accountsIL As System.Windows.Forms.ImageList
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents recompute_RB As System.Windows.Forms.RadioButton
    Friend WithEvents aggregation_RB As System.Windows.Forms.RadioButton
    Friend WithEvents bs_item_RB As System.Windows.Forms.RadioButton
    Friend WithEvents flux_RB As System.Windows.Forms.RadioButton
    Friend WithEvents ParentAccountTreeComboBox As VIBlend.WinForms.Controls.vTreeViewBox
End Class
