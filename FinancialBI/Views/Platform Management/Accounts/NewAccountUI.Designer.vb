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
        Me.CancelBT = New VIBlend.WinForms.Controls.vButton()
        Me.ButtonsIL = New System.Windows.Forms.ImageList(Me.components)
        Me.CreateAccountBT = New VIBlend.WinForms.Controls.vButton()
        Me.ButtonIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.accountsIL = New System.Windows.Forms.ImageList(Me.components)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.m_accountNameLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_accountParentLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_formulaTypeLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_formatLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_consolidationOptionLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_conversionOptionLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.ParentTVPanel = New System.Windows.Forms.Panel()
        Me.NameTextBox = New VIBlend.WinForms.Controls.vTextBox()
        Me.FormulaComboBox = New VIBlend.WinForms.Controls.vComboBox()
        Me.TypeComboBox = New VIBlend.WinForms.Controls.vComboBox()
        Me.GroupBox1 = New VIBlend.WinForms.Controls.vGroupBox()
        Me.m_nonRadioButton = New VIBlend.WinForms.Controls.vRadioButton()
        Me.m_recomputeRadioButton = New VIBlend.WinForms.Controls.vRadioButton()
        Me.m_aggregationRadioButton = New VIBlend.WinForms.Controls.vRadioButton()
        Me.GroupBox2 = New VIBlend.WinForms.Controls.vGroupBox()
        Me.m_endOfPeriodRadioButton = New VIBlend.WinForms.Controls.vRadioButton()
        Me.m_averageRateRadioButton = New VIBlend.WinForms.Controls.vRadioButton()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'CancelBT
        '
        Me.CancelBT.AllowAnimations = True
        Me.CancelBT.BackColor = System.Drawing.Color.Transparent
        Me.CancelBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CancelBT.ImageKey = "delete-2-xxl.png"
        Me.CancelBT.ImageList = Me.ButtonsIL
        Me.CancelBT.Location = New System.Drawing.Point(573, 278)
        Me.CancelBT.Name = "CancelBT"
        Me.CancelBT.RoundedCornersMask = CType(15, Byte)
        Me.CancelBT.Size = New System.Drawing.Size(100, 33)
        Me.CancelBT.TabIndex = 10
        Me.CancelBT.Text = "Cancel"
        Me.CancelBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CancelBT.UseVisualStyleBackColor = True
        Me.CancelBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
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
        Me.CreateAccountBT.AllowAnimations = True
        Me.CreateAccountBT.BackColor = System.Drawing.Color.Transparent
        Me.CreateAccountBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CreateAccountBT.ImageKey = "1420498403_340208.ico"
        Me.CreateAccountBT.ImageList = Me.ButtonIcons
        Me.CreateAccountBT.Location = New System.Drawing.Point(455, 278)
        Me.CreateAccountBT.Name = "CreateAccountBT"
        Me.CreateAccountBT.RoundedCornersMask = CType(15, Byte)
        Me.CreateAccountBT.Size = New System.Drawing.Size(100, 33)
        Me.CreateAccountBT.TabIndex = 9
        Me.CreateAccountBT.Text = "Create"
        Me.CreateAccountBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CreateAccountBT.UseVisualStyleBackColor = True
        Me.CreateAccountBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'ButtonIcons
        '
        Me.ButtonIcons.ImageStream = CType(resources.GetObject("ButtonIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonIcons.Images.SetKeyName(0, "imageres_89.ico")
        Me.ButtonIcons.Images.SetKeyName(1, "1420498403_340208.ico")
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
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.48015!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.51984!))
        Me.TableLayoutPanel1.Controls.Add(Me.m_accountNameLabel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.m_accountParentLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.m_formulaTypeLabel, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.m_formatLabel, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.m_consolidationOptionLabel, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.m_conversionOptionLabel, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.ParentTVPanel, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.NameTextBox, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.FormulaComboBox, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TypeComboBox, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox1, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox2, 1, 5)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(13, 19)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 6
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(660, 241)
        Me.TableLayoutPanel1.TabIndex = 36
        '
        'm_accountNameLabel
        '
        Me.m_accountNameLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_accountNameLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_accountNameLabel.Ellipsis = False
        Me.m_accountNameLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_accountNameLabel.Location = New System.Drawing.Point(3, 43)
        Me.m_accountNameLabel.Multiline = True
        Me.m_accountNameLabel.Name = "m_accountNameLabel"
        Me.m_accountNameLabel.Size = New System.Drawing.Size(85, 15)
        Me.m_accountNameLabel.TabIndex = 31
        Me.m_accountNameLabel.Text = "Account name"
        Me.m_accountNameLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_accountNameLabel.UseMnemonics = True
        Me.m_accountNameLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_accountParentLabel
        '
        Me.m_accountParentLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_accountParentLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_accountParentLabel.Ellipsis = False
        Me.m_accountParentLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_accountParentLabel.Location = New System.Drawing.Point(3, 3)
        Me.m_accountParentLabel.Multiline = True
        Me.m_accountParentLabel.Name = "m_accountParentLabel"
        Me.m_accountParentLabel.Size = New System.Drawing.Size(88, 15)
        Me.m_accountParentLabel.TabIndex = 30
        Me.m_accountParentLabel.Text = "Account parent"
        Me.m_accountParentLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_accountParentLabel.UseMnemonics = True
        Me.m_accountParentLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_formulaTypeLabel
        '
        Me.m_formulaTypeLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_formulaTypeLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_formulaTypeLabel.Ellipsis = False
        Me.m_formulaTypeLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_formulaTypeLabel.Location = New System.Drawing.Point(3, 83)
        Me.m_formulaTypeLabel.Multiline = True
        Me.m_formulaTypeLabel.Name = "m_formulaTypeLabel"
        Me.m_formulaTypeLabel.Size = New System.Drawing.Size(78, 15)
        Me.m_formulaTypeLabel.TabIndex = 32
        Me.m_formulaTypeLabel.Text = "Formula type"
        Me.m_formulaTypeLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_formulaTypeLabel.UseMnemonics = True
        Me.m_formulaTypeLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_formatLabel
        '
        Me.m_formatLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_formatLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_formatLabel.Ellipsis = False
        Me.m_formatLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_formatLabel.Location = New System.Drawing.Point(3, 123)
        Me.m_formatLabel.Multiline = True
        Me.m_formatLabel.Name = "m_formatLabel"
        Me.m_formatLabel.Size = New System.Drawing.Size(88, 15)
        Me.m_formatLabel.TabIndex = 33
        Me.m_formatLabel.Text = "Account format"
        Me.m_formatLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_formatLabel.UseMnemonics = True
        Me.m_formatLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_consolidationOptionLabel
        '
        Me.m_consolidationOptionLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_consolidationOptionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_consolidationOptionLabel.Ellipsis = False
        Me.m_consolidationOptionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_consolidationOptionLabel.Location = New System.Drawing.Point(3, 163)
        Me.m_consolidationOptionLabel.Multiline = True
        Me.m_consolidationOptionLabel.Name = "m_consolidationOptionLabel"
        Me.m_consolidationOptionLabel.Size = New System.Drawing.Size(119, 15)
        Me.m_consolidationOptionLabel.TabIndex = 34
        Me.m_consolidationOptionLabel.Text = "Consolidation option"
        Me.m_consolidationOptionLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_consolidationOptionLabel.UseMnemonics = True
        Me.m_consolidationOptionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_conversionOptionLabel
        '
        Me.m_conversionOptionLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_conversionOptionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_conversionOptionLabel.Ellipsis = False
        Me.m_conversionOptionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_conversionOptionLabel.Location = New System.Drawing.Point(3, 203)
        Me.m_conversionOptionLabel.Multiline = True
        Me.m_conversionOptionLabel.Name = "m_conversionOptionLabel"
        Me.m_conversionOptionLabel.Size = New System.Drawing.Size(128, 15)
        Me.m_conversionOptionLabel.TabIndex = 35
        Me.m_conversionOptionLabel.Text = "Currencies conversion"
        Me.m_conversionOptionLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_conversionOptionLabel.UseMnemonics = True
        Me.m_conversionOptionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'ParentTVPanel
        '
        Me.ParentTVPanel.Location = New System.Drawing.Point(171, 3)
        Me.ParentTVPanel.Name = "ParentTVPanel"
        Me.ParentTVPanel.Size = New System.Drawing.Size(486, 26)
        Me.ParentTVPanel.TabIndex = 14
        '
        'NameTextBox
        '
        Me.NameTextBox.BackColor = System.Drawing.Color.White
        Me.NameTextBox.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.NameTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.NameTextBox.DefaultText = "Empty..."
        Me.NameTextBox.Location = New System.Drawing.Point(171, 43)
        Me.NameTextBox.MaxLength = 32767
        Me.NameTextBox.Name = "NameTextBox"
        Me.NameTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.NameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.NameTextBox.SelectionLength = 0
        Me.NameTextBox.SelectionStart = 0
        Me.NameTextBox.Size = New System.Drawing.Size(486, 26)
        Me.NameTextBox.TabIndex = 1
        Me.NameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.NameTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'FormulaComboBox
        '
        Me.FormulaComboBox.BackColor = System.Drawing.Color.White
        Me.FormulaComboBox.DisplayMember = ""
        Me.FormulaComboBox.DropDownList = True
        Me.FormulaComboBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.FormulaComboBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.FormulaComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.FormulaComboBox.DropDownWidth = 486
        Me.FormulaComboBox.Location = New System.Drawing.Point(171, 83)
        Me.FormulaComboBox.Name = "FormulaComboBox"
        Me.FormulaComboBox.RoundedCornersMaskListItem = CType(15, Byte)
        Me.FormulaComboBox.Size = New System.Drawing.Size(486, 26)
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
        Me.TypeComboBox.DropDownList = True
        Me.TypeComboBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.TypeComboBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.TypeComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.TypeComboBox.DropDownWidth = 486
        Me.TypeComboBox.Location = New System.Drawing.Point(171, 123)
        Me.TypeComboBox.Name = "TypeComboBox"
        Me.TypeComboBox.RoundedCornersMaskListItem = CType(15, Byte)
        Me.TypeComboBox.Size = New System.Drawing.Size(486, 26)
        Me.TypeComboBox.TabIndex = 3
        Me.TypeComboBox.Text = " "
        Me.TypeComboBox.UseThemeBackColor = False
        Me.TypeComboBox.UseThemeDropDownArrowColor = True
        Me.TypeComboBox.ValueMember = ""
        Me.TypeComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        Me.TypeComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.m_nonRadioButton)
        Me.GroupBox1.Controls.Add(Me.m_recomputeRadioButton)
        Me.GroupBox1.Controls.Add(Me.m_aggregationRadioButton)
        Me.GroupBox1.Location = New System.Drawing.Point(168, 160)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(489, 40)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.UseThemeBorderColor = True
        Me.GroupBox1.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_nonRadioButton
        '
        Me.m_nonRadioButton.BackColor = System.Drawing.Color.Transparent
        Me.m_nonRadioButton.Flat = True
        Me.m_nonRadioButton.Image = Nothing
        Me.m_nonRadioButton.Location = New System.Drawing.Point(230, 2)
        Me.m_nonRadioButton.Name = "m_nonRadioButton"
        Me.m_nonRadioButton.Size = New System.Drawing.Size(104, 24)
        Me.m_nonRadioButton.TabIndex = 6
        Me.m_nonRadioButton.Text = "None"
        Me.m_nonRadioButton.UseVisualStyleBackColor = False
        Me.m_nonRadioButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_recomputeRadioButton
        '
        Me.m_recomputeRadioButton.AutoSize = True
        Me.m_recomputeRadioButton.BackColor = System.Drawing.Color.Transparent
        Me.m_recomputeRadioButton.Flat = True
        Me.m_recomputeRadioButton.Image = Nothing
        Me.m_recomputeRadioButton.Location = New System.Drawing.Point(115, 3)
        Me.m_recomputeRadioButton.Name = "m_recomputeRadioButton"
        Me.m_recomputeRadioButton.Size = New System.Drawing.Size(109, 19)
        Me.m_recomputeRadioButton.TabIndex = 5
        Me.m_recomputeRadioButton.Text = "Recomputation"
        Me.m_recomputeRadioButton.UseVisualStyleBackColor = True
        Me.m_recomputeRadioButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_aggregationRadioButton
        '
        Me.m_aggregationRadioButton.AutoSize = True
        Me.m_aggregationRadioButton.BackColor = System.Drawing.Color.Transparent
        Me.m_aggregationRadioButton.Checked = True
        Me.m_aggregationRadioButton.Flat = True
        Me.m_aggregationRadioButton.Image = Nothing
        Me.m_aggregationRadioButton.Location = New System.Drawing.Point(18, 3)
        Me.m_aggregationRadioButton.Name = "m_aggregationRadioButton"
        Me.m_aggregationRadioButton.Size = New System.Drawing.Size(91, 19)
        Me.m_aggregationRadioButton.TabIndex = 4
        Me.m_aggregationRadioButton.TabStop = True
        Me.m_aggregationRadioButton.Text = "Aggregation"
        Me.m_aggregationRadioButton.UseVisualStyleBackColor = True
        Me.m_aggregationRadioButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.m_endOfPeriodRadioButton)
        Me.GroupBox2.Controls.Add(Me.m_averageRateRadioButton)
        Me.GroupBox2.Location = New System.Drawing.Point(168, 200)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(489, 40)
        Me.GroupBox2.TabIndex = 13
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.UseThemeBorderColor = True
        Me.GroupBox2.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_endOfPeriodRadioButton
        '
        Me.m_endOfPeriodRadioButton.AutoSize = True
        Me.m_endOfPeriodRadioButton.BackColor = System.Drawing.Color.Transparent
        Me.m_endOfPeriodRadioButton.Flat = True
        Me.m_endOfPeriodRadioButton.Image = Nothing
        Me.m_endOfPeriodRadioButton.Location = New System.Drawing.Point(115, 4)
        Me.m_endOfPeriodRadioButton.Name = "m_endOfPeriodRadioButton"
        Me.m_endOfPeriodRadioButton.Size = New System.Drawing.Size(122, 19)
        Me.m_endOfPeriodRadioButton.TabIndex = 8
        Me.m_endOfPeriodRadioButton.Text = "End of period rate"
        Me.m_endOfPeriodRadioButton.UseVisualStyleBackColor = True
        Me.m_endOfPeriodRadioButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_averageRateRadioButton
        '
        Me.m_averageRateRadioButton.AutoSize = True
        Me.m_averageRateRadioButton.BackColor = System.Drawing.Color.Transparent
        Me.m_averageRateRadioButton.Checked = True
        Me.m_averageRateRadioButton.Flat = True
        Me.m_averageRateRadioButton.Image = Nothing
        Me.m_averageRateRadioButton.Location = New System.Drawing.Point(18, 4)
        Me.m_averageRateRadioButton.Name = "m_averageRateRadioButton"
        Me.m_averageRateRadioButton.Size = New System.Drawing.Size(93, 19)
        Me.m_averageRateRadioButton.TabIndex = 6
        Me.m_averageRateRadioButton.TabStop = True
        Me.m_averageRateRadioButton.Text = "Average rate"
        Me.m_averageRateRadioButton.UseVisualStyleBackColor = True
        Me.m_averageRateRadioButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'NewAccountUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(684, 327)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.CancelBT)
        Me.Controls.Add(Me.CreateAccountBT)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "NewAccountUI"
        Me.Text = "New account"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CancelBT As VIBlend.WinForms.Controls.vButton
    Friend WithEvents CreateAccountBT As VIBlend.WinForms.Controls.vButton
    Friend WithEvents ButtonIcons As System.Windows.Forms.ImageList
    Friend WithEvents ButtonsIL As System.Windows.Forms.ImageList
    Friend WithEvents accountsIL As System.Windows.Forms.ImageList
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents m_accountNameLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_accountParentLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_formulaTypeLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_formatLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents GroupBox1 As VIBlend.WinForms.Controls.vGroupBox
    Friend WithEvents m_recomputeRadioButton As VIBlend.WinForms.Controls.vRadioButton
    Friend WithEvents m_aggregationRadioButton As VIBlend.WinForms.Controls.vRadioButton
    Friend WithEvents m_consolidationOptionLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents GroupBox2 As VIBlend.WinForms.Controls.vGroupBox
    Friend WithEvents m_endOfPeriodRadioButton As VIBlend.WinForms.Controls.vRadioButton
    Friend WithEvents m_averageRateRadioButton As VIBlend.WinForms.Controls.vRadioButton
    Friend WithEvents m_conversionOptionLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents ParentTVPanel As System.Windows.Forms.Panel
    Friend WithEvents NameTextBox As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents FormulaComboBox As VIBlend.WinForms.Controls.vComboBox
    Friend WithEvents TypeComboBox As VIBlend.WinForms.Controls.vComboBox
    Friend WithEvents m_nonRadioButton As VIBlend.WinForms.Controls.vRadioButton
End Class
