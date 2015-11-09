<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AccountsView
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AccountsView))
        Me.SplitContainer1 = New VIBlend.WinForms.Controls.vSplitContainer()
        Me.AccountsTVPanel = New System.Windows.Forms.Panel()
        Me.SplitContainer2 = New VIBlend.WinForms.Controls.vSplitContainer()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.m_accountDescriptionGroupBox = New VIBlend.WinForms.Controls.vGroupBox()
        Me.m_descriptionTextBox = New VIBlend.WinForms.Controls.vTextBox()
        Me.SaveDescriptionBT = New VIBlend.WinForms.Controls.vButton()
        Me.EditButtonsImagelist = New System.Windows.Forms.ImageList(Me.components)
        Me.m_accountFormulaGroupBox = New VIBlend.WinForms.Controls.vGroupBox()
        Me.m_formulaTextBox = New VIBlend.WinForms.Controls.vTextBox()
        Me.formulaEdit = New System.Windows.Forms.CheckBox()
        Me.submit_cmd = New VIBlend.WinForms.Controls.vButton()
        Me.m_accountInformation2GroupBox = New VIBlend.WinForms.Controls.vGroupBox()
        Me.ConsolidationOptionComboBox = New VIBlend.WinForms.Controls.vComboBox()
        Me.CurrencyConversionComboBox = New VIBlend.WinForms.Controls.vComboBox()
        Me.m_accountNameLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_accountformuleTypeLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_accountAccountTypeLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_accountConsolidationOptionLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_accountcurrenciesConversionLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.FormulaTypeComboBox = New VIBlend.WinForms.Controls.vComboBox()
        Me.TypeComboBox = New VIBlend.WinForms.Controls.vComboBox()
        Me.Name_TB = New VIBlend.WinForms.Controls.vTextBox()
        Me.GlobalFactsPanel = New System.Windows.Forms.Panel()
        Me.m_globalFactsLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.accountsIL = New System.Windows.Forms.ImageList(Me.components)
        Me.TVRCM = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddSubAccountToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddCategoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteAccountToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DropHierarchyToExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.MainMenu = New System.Windows.Forms.MenuStrip()
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreateANewAccountToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreateANewCategoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteAccountToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.DropHierarchyToExcelToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.DropAllAccountsHierarchyToExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DropSelectedAccountHierarchyToExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.m_globalFactsImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.m_accountDescriptionGroupBox.SuspendLayout()
        Me.m_accountFormulaGroupBox.SuspendLayout()
        Me.m_accountInformation2GroupBox.SuspendLayout()
        Me.TVRCM.SuspendLayout()
        Me.MainMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.AllowAnimations = True
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 30)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.SplitContainer1.Panel1.BorderColor = System.Drawing.Color.Transparent
        Me.SplitContainer1.Panel1.Controls.Add(Me.AccountsTVPanel)
        Me.SplitContainer1.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.SplitContainer1.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Panel1.Name = "Panel1"
        Me.SplitContainer1.Panel1.Size = New System.Drawing.Size(306, 736)
        Me.SplitContainer1.Panel1.TabIndex = 1
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.BackColor = System.Drawing.Color.White
        Me.SplitContainer1.Panel2.BorderColor = System.Drawing.Color.Transparent
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Panel2.Location = New System.Drawing.Point(311, 0)
        Me.SplitContainer1.Panel2.Name = "Panel2"
        Me.SplitContainer1.Panel2.Size = New System.Drawing.Size(824, 736)
        Me.SplitContainer1.Panel2.TabIndex = 2
        Me.SplitContainer1.Size = New System.Drawing.Size(1135, 736)
        Me.SplitContainer1.SplitterDistance = 246
        Me.SplitContainer1.SplitterSize = 5
        Me.SplitContainer1.StyleKey = "Splitter"
        Me.SplitContainer1.TabIndex = 23
        Me.SplitContainer1.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER
        '
        'AccountsTVPanel
        '
        Me.AccountsTVPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AccountsTVPanel.Location = New System.Drawing.Point(0, 0)
        Me.AccountsTVPanel.Name = "AccountsTVPanel"
        Me.AccountsTVPanel.Size = New System.Drawing.Size(306, 736)
        Me.AccountsTVPanel.TabIndex = 3
        '
        'SplitContainer2
        '
        Me.SplitContainer2.AllowAnimations = True
        Me.SplitContainer2.BackColor = System.Drawing.SystemColors.Control
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.BackColor = System.Drawing.Color.White
        Me.SplitContainer2.Panel1.BorderColor = System.Drawing.Color.Silver
        Me.SplitContainer2.Panel1.Controls.Add(Me.TableLayoutPanel2)
        Me.SplitContainer2.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Panel1.Name = "Panel1"
        Me.SplitContainer2.Panel1.Size = New System.Drawing.Size(445, 736)
        Me.SplitContainer2.Panel1.TabIndex = 1
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.SplitContainer2.Panel2.BorderColor = System.Drawing.Color.Transparent
        Me.SplitContainer2.Panel2.Controls.Add(Me.GlobalFactsPanel)
        Me.SplitContainer2.Panel2.Controls.Add(Me.m_globalFactsLabel)
        Me.SplitContainer2.Panel2.Location = New System.Drawing.Point(450, 0)
        Me.SplitContainer2.Panel2.Name = "Panel2"
        Me.SplitContainer2.Panel2.Size = New System.Drawing.Size(374, 736)
        Me.SplitContainer2.Panel2.TabIndex = 2
        Me.SplitContainer2.Size = New System.Drawing.Size(824, 736)
        Me.SplitContainer2.SplitterDistance = 482
        Me.SplitContainer2.SplitterSize = 5
        Me.SplitContainer2.StyleKey = "Splitter"
        Me.SplitContainer2.TabIndex = 2
        Me.SplitContainer2.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.BackColor = System.Drawing.SystemColors.Control
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.m_accountDescriptionGroupBox, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.m_accountFormulaGroupBox, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.m_accountInformation2GroupBox, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 3
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 302.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.77032!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.22968!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(445, 736)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'm_accountDescriptionGroupBox
        '
        Me.m_accountDescriptionGroupBox.BackColor = System.Drawing.SystemColors.Control
        Me.m_accountDescriptionGroupBox.Controls.Add(Me.m_descriptionTextBox)
        Me.m_accountDescriptionGroupBox.Controls.Add(Me.SaveDescriptionBT)
        Me.m_accountDescriptionGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_accountDescriptionGroupBox.Location = New System.Drawing.Point(3, 542)
        Me.m_accountDescriptionGroupBox.Name = "m_accountDescriptionGroupBox"
        Me.m_accountDescriptionGroupBox.Size = New System.Drawing.Size(439, 191)
        Me.m_accountDescriptionGroupBox.TabIndex = 20
        Me.m_accountDescriptionGroupBox.TabStop = False
        Me.m_accountDescriptionGroupBox.Text = "Account description"
        Me.m_accountDescriptionGroupBox.TitleBackColor = System.Drawing.SystemColors.Control
        Me.m_accountDescriptionGroupBox.UseThemeBorderColor = True
        Me.m_accountDescriptionGroupBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER
        '
        'm_descriptionTextBox
        '
        Me.m_descriptionTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_descriptionTextBox.BackColor = System.Drawing.Color.White
        Me.m_descriptionTextBox.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.m_descriptionTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.m_descriptionTextBox.DefaultText = "Empty..."
        Me.m_descriptionTextBox.Location = New System.Drawing.Point(6, 21)
        Me.m_descriptionTextBox.MaxLength = 32767
        Me.m_descriptionTextBox.Multiline = True
        Me.m_descriptionTextBox.Name = "m_descriptionTextBox"
        Me.m_descriptionTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.m_descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.m_descriptionTextBox.SelectionLength = 0
        Me.m_descriptionTextBox.SelectionStart = 0
        Me.m_descriptionTextBox.Size = New System.Drawing.Size(411, 125)
        Me.m_descriptionTextBox.TabIndex = 8
        Me.m_descriptionTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.m_descriptionTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'SaveDescriptionBT
        '
        Me.SaveDescriptionBT.AllowAnimations = True
        Me.SaveDescriptionBT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SaveDescriptionBT.BackColor = System.Drawing.Color.Transparent
        Me.SaveDescriptionBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.SaveDescriptionBT.ImageKey = "1420498403_340208.ico"
        Me.SaveDescriptionBT.ImageList = Me.EditButtonsImagelist
        Me.SaveDescriptionBT.Location = New System.Drawing.Point(230, 151)
        Me.SaveDescriptionBT.Margin = New System.Windows.Forms.Padding(2)
        Me.SaveDescriptionBT.Name = "SaveDescriptionBT"
        Me.SaveDescriptionBT.RoundedCornersMask = CType(15, Byte)
        Me.SaveDescriptionBT.Size = New System.Drawing.Size(187, 28)
        Me.SaveDescriptionBT.TabIndex = 7
        Me.SaveDescriptionBT.Text = "Save Description"
        Me.SaveDescriptionBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SaveDescriptionBT.UseVisualStyleBackColor = True
        Me.SaveDescriptionBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'EditButtonsImagelist
        '
        Me.EditButtonsImagelist.ImageStream = CType(resources.GetObject("EditButtonsImagelist.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.EditButtonsImagelist.TransparentColor = System.Drawing.Color.Transparent
        Me.EditButtonsImagelist.Images.SetKeyName(0, "1420498403_340208.ico")
        Me.EditButtonsImagelist.Images.SetKeyName(1, "config circle purple.ico")
        '
        'm_accountFormulaGroupBox
        '
        Me.m_accountFormulaGroupBox.BackColor = System.Drawing.SystemColors.Control
        Me.m_accountFormulaGroupBox.Controls.Add(Me.m_formulaTextBox)
        Me.m_accountFormulaGroupBox.Controls.Add(Me.formulaEdit)
        Me.m_accountFormulaGroupBox.Controls.Add(Me.submit_cmd)
        Me.m_accountFormulaGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_accountFormulaGroupBox.Location = New System.Drawing.Point(3, 305)
        Me.m_accountFormulaGroupBox.Name = "m_accountFormulaGroupBox"
        Me.m_accountFormulaGroupBox.Size = New System.Drawing.Size(439, 231)
        Me.m_accountFormulaGroupBox.TabIndex = 19
        Me.m_accountFormulaGroupBox.TabStop = False
        Me.m_accountFormulaGroupBox.Text = "Account's formula"
        Me.m_accountFormulaGroupBox.TitleBackColor = System.Drawing.SystemColors.Control
        Me.m_accountFormulaGroupBox.UseThemeBorderColor = True
        Me.m_accountFormulaGroupBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER
        '
        'm_formulaTextBox
        '
        Me.m_formulaTextBox.AllowDrop = True
        Me.m_formulaTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_formulaTextBox.BackColor = System.Drawing.Color.White
        Me.m_formulaTextBox.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.m_formulaTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.m_formulaTextBox.DefaultText = "Empty..."
        Me.m_formulaTextBox.Location = New System.Drawing.Point(6, 52)
        Me.m_formulaTextBox.MaxLength = 32767
        Me.m_formulaTextBox.Multiline = True
        Me.m_formulaTextBox.Name = "m_formulaTextBox"
        Me.m_formulaTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.m_formulaTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.m_formulaTextBox.SelectionLength = 0
        Me.m_formulaTextBox.SelectionStart = 0
        Me.m_formulaTextBox.Size = New System.Drawing.Size(411, 133)
        Me.m_formulaTextBox.TabIndex = 0
        Me.m_formulaTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.m_formulaTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'formulaEdit
        '
        Me.formulaEdit.Appearance = System.Windows.Forms.Appearance.Button
        Me.formulaEdit.AutoSize = True
        Me.formulaEdit.BackColor = System.Drawing.Color.AliceBlue
        Me.formulaEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.formulaEdit.ImageKey = "config circle purple.ico"
        Me.formulaEdit.ImageList = Me.EditButtonsImagelist
        Me.formulaEdit.Location = New System.Drawing.Point(6, 21)
        Me.formulaEdit.Name = "formulaEdit"
        Me.formulaEdit.Size = New System.Drawing.Size(87, 25)
        Me.formulaEdit.TabIndex = 5
        Me.formulaEdit.Text = "Edit Formula"
        Me.formulaEdit.UseVisualStyleBackColor = True
        '
        'submit_cmd
        '
        Me.submit_cmd.AllowAnimations = True
        Me.submit_cmd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.submit_cmd.BackColor = System.Drawing.Color.Transparent
        Me.submit_cmd.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.submit_cmd.ImageKey = "1420498403_340208.ico"
        Me.submit_cmd.ImageList = Me.EditButtonsImagelist
        Me.submit_cmd.Location = New System.Drawing.Point(232, 190)
        Me.submit_cmd.Margin = New System.Windows.Forms.Padding(2)
        Me.submit_cmd.Name = "submit_cmd"
        Me.submit_cmd.RoundedCornersMask = CType(15, Byte)
        Me.submit_cmd.Size = New System.Drawing.Size(187, 28)
        Me.submit_cmd.TabIndex = 7
        Me.submit_cmd.Text = "Validate formula"
        Me.submit_cmd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.submit_cmd.UseVisualStyleBackColor = True
        Me.submit_cmd.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_accountInformation2GroupBox
        '
        Me.m_accountInformation2GroupBox.BackColor = System.Drawing.SystemColors.Control
        Me.m_accountInformation2GroupBox.Controls.Add(Me.ConsolidationOptionComboBox)
        Me.m_accountInformation2GroupBox.Controls.Add(Me.CurrencyConversionComboBox)
        Me.m_accountInformation2GroupBox.Controls.Add(Me.m_accountNameLabel)
        Me.m_accountInformation2GroupBox.Controls.Add(Me.m_accountformuleTypeLabel)
        Me.m_accountInformation2GroupBox.Controls.Add(Me.m_accountAccountTypeLabel)
        Me.m_accountInformation2GroupBox.Controls.Add(Me.m_accountConsolidationOptionLabel)
        Me.m_accountInformation2GroupBox.Controls.Add(Me.m_accountcurrenciesConversionLabel)
        Me.m_accountInformation2GroupBox.Controls.Add(Me.FormulaTypeComboBox)
        Me.m_accountInformation2GroupBox.Controls.Add(Me.TypeComboBox)
        Me.m_accountInformation2GroupBox.Controls.Add(Me.Name_TB)
        Me.m_accountInformation2GroupBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_accountInformation2GroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.m_accountInformation2GroupBox.Location = New System.Drawing.Point(2, 2)
        Me.m_accountInformation2GroupBox.Margin = New System.Windows.Forms.Padding(2)
        Me.m_accountInformation2GroupBox.Name = "m_accountInformation2GroupBox"
        Me.m_accountInformation2GroupBox.Padding = New System.Windows.Forms.Padding(2)
        Me.m_accountInformation2GroupBox.Size = New System.Drawing.Size(441, 298)
        Me.m_accountInformation2GroupBox.TabIndex = 17
        Me.m_accountInformation2GroupBox.TabStop = False
        Me.m_accountInformation2GroupBox.Text = "Account Information"
        Me.m_accountInformation2GroupBox.TitleBackColor = System.Drawing.SystemColors.Control
        Me.m_accountInformation2GroupBox.UseThemeBorderColor = True
        Me.m_accountInformation2GroupBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER
        '
        'ConsolidationOptionComboBox
        '
        Me.ConsolidationOptionComboBox.BackColor = System.Drawing.Color.White
        Me.ConsolidationOptionComboBox.DisplayMember = ""
        Me.ConsolidationOptionComboBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.ConsolidationOptionComboBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.ConsolidationOptionComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.ConsolidationOptionComboBox.DropDownWidth = 259
        Me.ConsolidationOptionComboBox.Location = New System.Drawing.Point(160, 198)
        Me.ConsolidationOptionComboBox.Name = "ConsolidationOptionComboBox"
        Me.ConsolidationOptionComboBox.RoundedCornersMaskListItem = CType(15, Byte)
        Me.ConsolidationOptionComboBox.Size = New System.Drawing.Size(259, 22)
        Me.ConsolidationOptionComboBox.TabIndex = 32
        Me.ConsolidationOptionComboBox.UseThemeBackColor = False
        Me.ConsolidationOptionComboBox.UseThemeDropDownArrowColor = True
        Me.ConsolidationOptionComboBox.ValueMember = ""
        Me.ConsolidationOptionComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        Me.ConsolidationOptionComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'CurrencyConversionComboBox
        '
        Me.CurrencyConversionComboBox.BackColor = System.Drawing.Color.White
        Me.CurrencyConversionComboBox.DisplayMember = ""
        Me.CurrencyConversionComboBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.CurrencyConversionComboBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.CurrencyConversionComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.CurrencyConversionComboBox.DropDownWidth = 259
        Me.CurrencyConversionComboBox.Location = New System.Drawing.Point(161, 158)
        Me.CurrencyConversionComboBox.Name = "CurrencyConversionComboBox"
        Me.CurrencyConversionComboBox.RoundedCornersMaskListItem = CType(15, Byte)
        Me.CurrencyConversionComboBox.Size = New System.Drawing.Size(259, 22)
        Me.CurrencyConversionComboBox.TabIndex = 46
        Me.CurrencyConversionComboBox.UseThemeBackColor = False
        Me.CurrencyConversionComboBox.UseThemeDropDownArrowColor = True
        Me.CurrencyConversionComboBox.ValueMember = ""
        Me.CurrencyConversionComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        Me.CurrencyConversionComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_accountNameLabel
        '
        Me.m_accountNameLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_accountNameLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_accountNameLabel.Ellipsis = False
        Me.m_accountNameLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_accountNameLabel.Location = New System.Drawing.Point(20, 41)
        Me.m_accountNameLabel.Multiline = True
        Me.m_accountNameLabel.Name = "m_accountNameLabel"
        Me.m_accountNameLabel.Size = New System.Drawing.Size(129, 25)
        Me.m_accountNameLabel.TabIndex = 45
        Me.m_accountNameLabel.Text = "Account's name"
        Me.m_accountNameLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_accountNameLabel.UseMnemonics = True
        Me.m_accountNameLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_accountformuleTypeLabel
        '
        Me.m_accountformuleTypeLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_accountformuleTypeLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_accountformuleTypeLabel.Ellipsis = False
        Me.m_accountformuleTypeLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_accountformuleTypeLabel.Location = New System.Drawing.Point(20, 78)
        Me.m_accountformuleTypeLabel.Multiline = True
        Me.m_accountformuleTypeLabel.Name = "m_accountformuleTypeLabel"
        Me.m_accountformuleTypeLabel.Size = New System.Drawing.Size(129, 25)
        Me.m_accountformuleTypeLabel.TabIndex = 44
        Me.m_accountformuleTypeLabel.Text = "Formula type"
        Me.m_accountformuleTypeLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_accountformuleTypeLabel.UseMnemonics = True
        Me.m_accountformuleTypeLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_accountAccountTypeLabel
        '
        Me.m_accountAccountTypeLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_accountAccountTypeLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_accountAccountTypeLabel.Ellipsis = False
        Me.m_accountAccountTypeLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_accountAccountTypeLabel.Location = New System.Drawing.Point(20, 117)
        Me.m_accountAccountTypeLabel.Multiline = True
        Me.m_accountAccountTypeLabel.Name = "m_accountAccountTypeLabel"
        Me.m_accountAccountTypeLabel.Size = New System.Drawing.Size(129, 25)
        Me.m_accountAccountTypeLabel.TabIndex = 43
        Me.m_accountAccountTypeLabel.Text = "Account type"
        Me.m_accountAccountTypeLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_accountAccountTypeLabel.UseMnemonics = True
        Me.m_accountAccountTypeLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_accountConsolidationOptionLabel
        '
        Me.m_accountConsolidationOptionLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_accountConsolidationOptionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_accountConsolidationOptionLabel.Ellipsis = False
        Me.m_accountConsolidationOptionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_accountConsolidationOptionLabel.Location = New System.Drawing.Point(17, 198)
        Me.m_accountConsolidationOptionLabel.Multiline = True
        Me.m_accountConsolidationOptionLabel.Name = "m_accountConsolidationOptionLabel"
        Me.m_accountConsolidationOptionLabel.Size = New System.Drawing.Size(129, 25)
        Me.m_accountConsolidationOptionLabel.TabIndex = 42
        Me.m_accountConsolidationOptionLabel.Text = "Consolidation option"
        Me.m_accountConsolidationOptionLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_accountConsolidationOptionLabel.UseMnemonics = True
        Me.m_accountConsolidationOptionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_accountcurrenciesConversionLabel
        '
        Me.m_accountcurrenciesConversionLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_accountcurrenciesConversionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_accountcurrenciesConversionLabel.Ellipsis = False
        Me.m_accountcurrenciesConversionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_accountcurrenciesConversionLabel.Location = New System.Drawing.Point(17, 158)
        Me.m_accountcurrenciesConversionLabel.Multiline = True
        Me.m_accountcurrenciesConversionLabel.Name = "m_accountcurrenciesConversionLabel"
        Me.m_accountcurrenciesConversionLabel.Size = New System.Drawing.Size(129, 25)
        Me.m_accountcurrenciesConversionLabel.TabIndex = 41
        Me.m_accountcurrenciesConversionLabel.Text = "Currencies conversion"
        Me.m_accountcurrenciesConversionLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_accountcurrenciesConversionLabel.UseMnemonics = True
        Me.m_accountcurrenciesConversionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'FormulaTypeComboBox
        '
        Me.FormulaTypeComboBox.BackColor = System.Drawing.Color.White
        Me.FormulaTypeComboBox.DisplayMember = ""
        Me.FormulaTypeComboBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.FormulaTypeComboBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.FormulaTypeComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.FormulaTypeComboBox.DropDownWidth = 259
        Me.FormulaTypeComboBox.Location = New System.Drawing.Point(161, 78)
        Me.FormulaTypeComboBox.Name = "FormulaTypeComboBox"
        Me.FormulaTypeComboBox.RoundedCornersMaskListItem = CType(15, Byte)
        Me.FormulaTypeComboBox.Size = New System.Drawing.Size(259, 22)
        Me.FormulaTypeComboBox.TabIndex = 32
        Me.FormulaTypeComboBox.UseThemeBackColor = False
        Me.FormulaTypeComboBox.UseThemeDropDownArrowColor = True
        Me.FormulaTypeComboBox.ValueMember = ""
        Me.FormulaTypeComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        Me.FormulaTypeComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'TypeComboBox
        '
        Me.TypeComboBox.BackColor = System.Drawing.Color.White
        Me.TypeComboBox.DisplayMember = ""
        Me.TypeComboBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.TypeComboBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.TypeComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.TypeComboBox.DropDownWidth = 259
        Me.TypeComboBox.Location = New System.Drawing.Point(161, 117)
        Me.TypeComboBox.Name = "TypeComboBox"
        Me.TypeComboBox.RoundedCornersMaskListItem = CType(15, Byte)
        Me.TypeComboBox.Size = New System.Drawing.Size(259, 22)
        Me.TypeComboBox.TabIndex = 31
        Me.TypeComboBox.UseThemeBackColor = False
        Me.TypeComboBox.UseThemeDropDownArrowColor = True
        Me.TypeComboBox.ValueMember = ""
        Me.TypeComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        Me.TypeComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'Name_TB
        '
        Me.Name_TB.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Name_TB.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.Name_TB.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.Name_TB.DefaultText = "Empty..."
        Me.Name_TB.Location = New System.Drawing.Point(161, 41)
        Me.Name_TB.Margin = New System.Windows.Forms.Padding(0)
        Me.Name_TB.MaxLength = 32767
        Me.Name_TB.Name = "Name_TB"
        Me.Name_TB.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.Name_TB.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.Name_TB.SelectionLength = 0
        Me.Name_TB.SelectionStart = 0
        Me.Name_TB.Size = New System.Drawing.Size(258, 22)
        Me.Name_TB.TabIndex = 1
        Me.Name_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.Name_TB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'GlobalFactsPanel
        '
        Me.GlobalFactsPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GlobalFactsPanel.Location = New System.Drawing.Point(12, 23)
        Me.GlobalFactsPanel.Name = "GlobalFactsPanel"
        Me.GlobalFactsPanel.Size = New System.Drawing.Size(302, 698)
        Me.GlobalFactsPanel.TabIndex = 3
        '
        'm_globalFactsLabel
        '
        Me.m_globalFactsLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_globalFactsLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_globalFactsLabel.Ellipsis = False
        Me.m_globalFactsLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_globalFactsLabel.Location = New System.Drawing.Point(12, 0)
        Me.m_globalFactsLabel.Multiline = True
        Me.m_globalFactsLabel.Name = "m_globalFactsLabel"
        Me.m_globalFactsLabel.Size = New System.Drawing.Size(187, 24)
        Me.m_globalFactsLabel.TabIndex = 0
        Me.m_globalFactsLabel.Text = "Macro economic indicators"
        Me.m_globalFactsLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_globalFactsLabel.UseMnemonics = True
        Me.m_globalFactsLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'accountsIL
        '
        Me.accountsIL.ImageStream = CType(resources.GetObject("accountsIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.accountsIL.TransparentColor = System.Drawing.Color.Transparent
        Me.accountsIL.Images.SetKeyName(0, "WC blue.png")
        Me.accountsIL.Images.SetKeyName(1, "config blue circle.png")
        Me.accountsIL.Images.SetKeyName(2, "func.png")
        Me.accountsIL.Images.SetKeyName(3, "sum purple.png")
        Me.accountsIL.Images.SetKeyName(4, "BS Blue.png")
        Me.accountsIL.Images.SetKeyName(5, "favicon(81).ico")
        '
        'TVRCM
        '
        Me.TVRCM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddSubAccountToolStripMenuItem, Me.AddCategoryToolStripMenuItem, Me.DeleteAccountToolStripMenuItem, Me.DropHierarchyToExcelToolStripMenuItem, Me.ToolStripSeparator1})
        Me.TVRCM.Name = "ContextMenuStripTV"
        Me.TVRCM.Size = New System.Drawing.Size(170, 106)
        '
        'AddSubAccountToolStripMenuItem
        '
        Me.AddSubAccountToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.registry_add
        Me.AddSubAccountToolStripMenuItem.Name = "AddSubAccountToolStripMenuItem"
        Me.AddSubAccountToolStripMenuItem.Size = New System.Drawing.Size(169, 24)
        Me.AddSubAccountToolStripMenuItem.Text = "New account"
        '
        'AddCategoryToolStripMenuItem
        '
        Me.AddCategoryToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.favicon_81_
        Me.AddCategoryToolStripMenuItem.Name = "AddCategoryToolStripMenuItem"
        Me.AddCategoryToolStripMenuItem.Size = New System.Drawing.Size(169, 24)
        Me.AddCategoryToolStripMenuItem.Text = "New tab"
        '
        'DeleteAccountToolStripMenuItem
        '
        Me.DeleteAccountToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.registry_delete
        Me.DeleteAccountToolStripMenuItem.Name = "DeleteAccountToolStripMenuItem"
        Me.DeleteAccountToolStripMenuItem.Size = New System.Drawing.Size(169, 24)
        Me.DeleteAccountToolStripMenuItem.Text = "Delete account"
        '
        'DropHierarchyToExcelToolStripMenuItem
        '
        Me.DropHierarchyToExcelToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.Excel_Blue_32x32
        Me.DropHierarchyToExcelToolStripMenuItem.Name = "DropHierarchyToExcelToolStripMenuItem"
        Me.DropHierarchyToExcelToolStripMenuItem.Size = New System.Drawing.Size(169, 24)
        Me.DropHierarchyToExcelToolStripMenuItem.Text = "Drop to excel"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(166, 6)
        '
        'MainMenu
        '
        Me.MainMenu.BackColor = System.Drawing.SystemColors.Control
        Me.MainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.DropHierarchyToExcelToolStripMenuItem1, Me.HelpToolStripMenuItem})
        Me.MainMenu.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu.Name = "MainMenu"
        Me.MainMenu.Size = New System.Drawing.Size(1135, 27)
        Me.MainMenu.TabIndex = 25
        Me.MainMenu.Text = "MenuStrip1"
        '
        'NewToolStripMenuItem
        '
        Me.NewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CreateANewAccountToolStripMenuItem, Me.CreateANewCategoryToolStripMenuItem, Me.DeleteAccountToolStripMenuItem1, Me.ToolStripSeparator2})
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        Me.NewToolStripMenuItem.Size = New System.Drawing.Size(71, 23)
        Me.NewToolStripMenuItem.Text = "Account"
        '
        'CreateANewAccountToolStripMenuItem
        '
        Me.CreateANewAccountToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.registry_add
        Me.CreateANewAccountToolStripMenuItem.Name = "CreateANewAccountToolStripMenuItem"
        Me.CreateANewAccountToolStripMenuItem.Size = New System.Drawing.Size(181, 24)
        Me.CreateANewAccountToolStripMenuItem.Text = "New account]"
        '
        'CreateANewCategoryToolStripMenuItem
        '
        Me.CreateANewCategoryToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.favicon_81_
        Me.CreateANewCategoryToolStripMenuItem.Name = "CreateANewCategoryToolStripMenuItem"
        Me.CreateANewCategoryToolStripMenuItem.Size = New System.Drawing.Size(181, 24)
        Me.CreateANewCategoryToolStripMenuItem.Text = "New tab account"
        '
        'DeleteAccountToolStripMenuItem1
        '
        Me.DeleteAccountToolStripMenuItem1.Image = Global.FinancialBI.My.Resources.Resources.registry_delete
        Me.DeleteAccountToolStripMenuItem1.Name = "DeleteAccountToolStripMenuItem1"
        Me.DeleteAccountToolStripMenuItem1.Size = New System.Drawing.Size(181, 24)
        Me.DeleteAccountToolStripMenuItem1.Text = "Delete account"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(178, 6)
        '
        'DropHierarchyToExcelToolStripMenuItem1
        '
        Me.DropHierarchyToExcelToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DropAllAccountsHierarchyToExcelToolStripMenuItem, Me.DropSelectedAccountHierarchyToExcelToolStripMenuItem})
        Me.DropHierarchyToExcelToolStripMenuItem1.Name = "DropHierarchyToExcelToolStripMenuItem1"
        Me.DropHierarchyToExcelToolStripMenuItem1.Size = New System.Drawing.Size(50, 23)
        Me.DropHierarchyToExcelToolStripMenuItem1.Text = "Excel"
        '
        'DropAllAccountsHierarchyToExcelToolStripMenuItem
        '
        Me.DropAllAccountsHierarchyToExcelToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.Excel_Blue_32x32
        Me.DropAllAccountsHierarchyToExcelToolStripMenuItem.Name = "DropAllAccountsHierarchyToExcelToolStripMenuItem"
        Me.DropAllAccountsHierarchyToExcelToolStripMenuItem.Size = New System.Drawing.Size(280, 24)
        Me.DropAllAccountsHierarchyToExcelToolStripMenuItem.Text = "Drop to excel]"
        '
        'DropSelectedAccountHierarchyToExcelToolStripMenuItem
        '
        Me.DropSelectedAccountHierarchyToExcelToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.Excel_Green_32x32
        Me.DropSelectedAccountHierarchyToExcelToolStripMenuItem.Name = "DropSelectedAccountHierarchyToExcelToolStripMenuItem"
        Me.DropSelectedAccountHierarchyToExcelToolStripMenuItem.Size = New System.Drawing.Size(280, 24)
        Me.DropSelectedAccountHierarchyToExcelToolStripMenuItem.Text = "Drop_selected_hierarchy_to_excel"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(49, 23)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'm_globalFactsImageList
        '
        Me.m_globalFactsImageList.ImageStream = CType(resources.GetObject("m_globalFactsImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.m_globalFactsImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.m_globalFactsImageList.Images.SetKeyName(0, "currency_euro.ico")
        Me.m_globalFactsImageList.Images.SetKeyName(1, "money_interest.ico")
        '
        'AccountsView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.MainMenu)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "AccountsView"
        Me.Size = New System.Drawing.Size(1135, 766)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.m_accountDescriptionGroupBox.ResumeLayout(False)
        Me.m_accountFormulaGroupBox.ResumeLayout(False)
        Me.m_accountFormulaGroupBox.PerformLayout()
        Me.m_accountInformation2GroupBox.ResumeLayout(False)
        Me.TVRCM.ResumeLayout(False)
        Me.MainMenu.ResumeLayout(False)
        Me.MainMenu.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As VIBlend.WinForms.Controls.vSplitContainer
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents m_accountInformation2GroupBox As VIBlend.WinForms.Controls.vGroupBox
    Friend WithEvents Name_TB As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents m_accountFormulaGroupBox As VIBlend.WinForms.Controls.vGroupBox
    Friend WithEvents formulaEdit As System.Windows.Forms.CheckBox
    Friend WithEvents submit_cmd As VIBlend.WinForms.Controls.vButton
    Friend WithEvents accountsIL As System.Windows.Forms.ImageList
    Friend WithEvents EditButtonsImagelist As System.Windows.Forms.ImageList
    Friend WithEvents TVRCM As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AddSubAccountToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddCategoryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteAccountToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DropHierarchyToExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MainMenu As System.Windows.Forms.MenuStrip
    Friend WithEvents NewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CreateANewAccountToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CreateANewCategoryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteAccountToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DropHierarchyToExcelToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DropAllAccountsHierarchyToExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DropSelectedAccountHierarchyToExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents m_accountDescriptionGroupBox As VIBlend.WinForms.Controls.vGroupBox
    Friend WithEvents SaveDescriptionBT As VIBlend.WinForms.Controls.vButton
    Friend WithEvents FormulaTypeComboBox As VIBlend.WinForms.Controls.vComboBox
    Friend WithEvents TypeComboBox As VIBlend.WinForms.Controls.vComboBox
    Friend WithEvents m_accountConsolidationOptionLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_accountcurrenciesConversionLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_accountNameLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_accountformuleTypeLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_accountAccountTypeLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents ConsolidationOptionComboBox As VIBlend.WinForms.Controls.vComboBox
    Friend WithEvents CurrencyConversionComboBox As VIBlend.WinForms.Controls.vComboBox
    Friend WithEvents GlobalFactsPanel As System.Windows.Forms.Panel
    Friend WithEvents m_globalFactsLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents SplitContainer2 As VIBlend.WinForms.Controls.vSplitContainer
    Friend WithEvents m_descriptionTextBox As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents m_formulaTextBox As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents AccountsTVPanel As System.Windows.Forms.Panel
    Friend WithEvents m_globalFactsImageList As System.Windows.Forms.ImageList

End Class
