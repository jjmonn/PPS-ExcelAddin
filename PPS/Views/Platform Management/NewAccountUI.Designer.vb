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
        Me.CancelBT = New System.Windows.Forms.Button()
        Me.ButtonsIL = New System.Windows.Forms.ImageList(Me.components)
        Me.CreateAccountBT = New System.Windows.Forms.Button()
        Me.ButtonIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.accountsIL = New System.Windows.Forms.ImageList(Me.components)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
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
        Me.ParentTVPanel = New System.Windows.Forms.Panel()
        Me.NameTextBox = New VIBlend.WinForms.Controls.vTextBox()
        Me.FormulaComboBox = New VIBlend.WinForms.Controls.vComboBox()
        Me.TypeComboBox = New VIBlend.WinForms.Controls.vComboBox()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'CancelBT
        '
        Me.CancelBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CancelBT.ImageKey = "delete-2-xxl.png"
        Me.CancelBT.ImageList = Me.ButtonsIL
        Me.CancelBT.Location = New System.Drawing.Point(406, 281)
        Me.CancelBT.Name = "CancelBT"
        Me.CancelBT.Size = New System.Drawing.Size(92, 25)
        Me.CancelBT.TabIndex = 10
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
        Me.CreateAccountBT.Location = New System.Drawing.Point(543, 281)
        Me.CreateAccountBT.Name = "CreateAccountBT"
        Me.CreateAccountBT.Size = New System.Drawing.Size(92, 25)
        Me.CreateAccountBT.TabIndex = 9
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
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.75513!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.24487!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label4, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label6, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox1, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Label7, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox2, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.Label8, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.ParentTVPanel, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.NameTextBox, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.FormulaComboBox, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TypeComboBox, 1, 3)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(43, 29)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 6
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(633, 209)
        Me.TableLayoutPanel1.TabIndex = 36
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 32)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 13)
        Me.Label4.TabIndex = 31
        Me.Label4.Text = "Account's Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 13)
        Me.Label2.TabIndex = 30
        Me.Label2.Text = "Parent Account"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 64)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(76, 13)
        Me.Label6.TabIndex = 32
        Me.Label6.Text = "Formula/ Input"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 96)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 13)
        Me.Label3.TabIndex = 33
        Me.Label3.Text = "Account's Format"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.recompute_RB)
        Me.GroupBox1.Controls.Add(Me.aggregation_RB)
        Me.GroupBox1.Location = New System.Drawing.Point(219, 128)
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
        Me.recompute_RB.Size = New System.Drawing.Size(97, 17)
        Me.recompute_RB.TabIndex = 5
        Me.recompute_RB.Text = "Recomputation"
        Me.recompute_RB.UseVisualStyleBackColor = True
        '
        'aggregation_RB
        '
        Me.aggregation_RB.AutoSize = True
        Me.aggregation_RB.Checked = True
        Me.aggregation_RB.Location = New System.Drawing.Point(39, 11)
        Me.aggregation_RB.Name = "aggregation_RB"
        Me.aggregation_RB.Size = New System.Drawing.Size(82, 17)
        Me.aggregation_RB.TabIndex = 4
        Me.aggregation_RB.TabStop = True
        Me.aggregation_RB.Text = "Aggregation"
        Me.aggregation_RB.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(3, 128)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(104, 13)
        Me.Label7.TabIndex = 34
        Me.Label7.Text = "Consolidation Option"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.bs_item_RB)
        Me.GroupBox2.Controls.Add(Me.flux_RB)
        Me.GroupBox2.Location = New System.Drawing.Point(219, 160)
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
        Me.bs_item_RB.Size = New System.Drawing.Size(131, 17)
        Me.bs_item_RB.TabIndex = 8
        Me.bs_item_RB.Text = "End of Period FX Rate"
        Me.bs_item_RB.UseVisualStyleBackColor = True
        '
        'flux_RB
        '
        Me.flux_RB.AutoSize = True
        Me.flux_RB.Checked = True
        Me.flux_RB.Location = New System.Drawing.Point(39, 12)
        Me.flux_RB.Name = "flux_RB"
        Me.flux_RB.Size = New System.Drawing.Size(107, 17)
        Me.flux_RB.TabIndex = 6
        Me.flux_RB.TabStop = True
        Me.flux_RB.Text = "Average FX Rate"
        Me.flux_RB.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(3, 160)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(105, 13)
        Me.Label8.TabIndex = 35
        Me.Label8.Text = "Currency Conversion"
        '
        'ParentTVPanel
        '
        Me.ParentTVPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ParentTVPanel.Location = New System.Drawing.Point(222, 3)
        Me.ParentTVPanel.Name = "ParentTVPanel"
        Me.ParentTVPanel.Size = New System.Drawing.Size(408, 26)
        Me.ParentTVPanel.TabIndex = 14
        '
        'NameTextBox
        '
        Me.NameTextBox.BackColor = System.Drawing.Color.White
        Me.NameTextBox.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.NameTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.NameTextBox.DefaultText = "Empty..."
        Me.NameTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NameTextBox.Location = New System.Drawing.Point(222, 35)
        Me.NameTextBox.MaxLength = 32767
        Me.NameTextBox.Name = "NameTextBox"
        Me.NameTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.NameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.NameTextBox.SelectionLength = 0
        Me.NameTextBox.SelectionStart = 0
        Me.NameTextBox.Size = New System.Drawing.Size(408, 26)
        Me.NameTextBox.TabIndex = 1
        Me.NameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.NameTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'FormulaComboBox
        '
        Me.FormulaComboBox.BackColor = System.Drawing.Color.White
        Me.FormulaComboBox.DisplayMember = ""
        Me.FormulaComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FormulaComboBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.FormulaComboBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.FormulaComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.FormulaComboBox.DropDownWidth = 408
        Me.FormulaComboBox.Location = New System.Drawing.Point(222, 67)
        Me.FormulaComboBox.Name = "FormulaComboBox"
        Me.FormulaComboBox.RoundedCornersMaskListItem = CType(15, Byte)
        Me.FormulaComboBox.Size = New System.Drawing.Size(408, 26)
        Me.FormulaComboBox.TabIndex = 2
        Me.FormulaComboBox.Text = " "
        Me.FormulaComboBox.UseThemeBackColor = False
        Me.FormulaComboBox.UseThemeDropDownArrowColor = True
        Me.FormulaComboBox.ValueMember = ""
        Me.FormulaComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        Me.FormulaComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'TypeComboBox
        '
        Me.TypeComboBox.BackColor = System.Drawing.Color.White
        Me.TypeComboBox.DisplayMember = ""
        Me.TypeComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TypeComboBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.TypeComboBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.TypeComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.TypeComboBox.DropDownWidth = 408
        Me.TypeComboBox.Location = New System.Drawing.Point(222, 99)
        Me.TypeComboBox.Name = "TypeComboBox"
        Me.TypeComboBox.RoundedCornersMaskListItem = CType(15, Byte)
        Me.TypeComboBox.Size = New System.Drawing.Size(408, 26)
        Me.TypeComboBox.TabIndex = 3
        Me.TypeComboBox.Text = " "
        Me.TypeComboBox.UseThemeBackColor = False
        Me.TypeComboBox.UseThemeDropDownArrowColor = True
        Me.TypeComboBox.ValueMember = ""
        Me.TypeComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        Me.TypeComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'NewAccountUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(690, 313)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.CancelBT)
        Me.Controls.Add(Me.CreateAccountBT)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "NewAccountUI"
        Me.Text = "New Account"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CancelBT As System.Windows.Forms.Button
    Friend WithEvents CreateAccountBT As System.Windows.Forms.Button
    Friend WithEvents ButtonIcons As System.Windows.Forms.ImageList
    Friend WithEvents ButtonsIL As System.Windows.Forms.ImageList
    Friend WithEvents accountsIL As System.Windows.Forms.ImageList
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents recompute_RB As System.Windows.Forms.RadioButton
    Friend WithEvents aggregation_RB As System.Windows.Forms.RadioButton
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents bs_item_RB As System.Windows.Forms.RadioButton
    Friend WithEvents flux_RB As System.Windows.Forms.RadioButton
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ParentTVPanel As System.Windows.Forms.Panel
    Friend WithEvents NameTextBox As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents FormulaComboBox As VIBlend.WinForms.Controls.vComboBox
    Friend WithEvents TypeComboBox As VIBlend.WinForms.Controls.vComboBox
End Class
