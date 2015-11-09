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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.AccountsTVPanel = New System.Windows.Forms.Panel()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.m_accountDescriptionGroupbox = New System.Windows.Forms.GroupBox()
        Me.m_descriptionTextBox = New VIBlend.WinForms.Controls.vTextBox()
        Me.SaveDescriptionBT = New VIBlend.WinForms.Controls.vButton()
        Me.EditButtonsImagelist = New System.Windows.Forms.ImageList(Me.components)
        Me.m_accountFormulaGroupbox = New System.Windows.Forms.GroupBox()
        Me.m_formulaTextBox = New VIBlend.WinForms.Controls.vTextBox()
        Me.formulaEdit = New System.Windows.Forms.CheckBox()
        Me.submit_cmd = New VIBlend.WinForms.Controls.vButton()
        Me.m_accountInformationGroupbox = New System.Windows.Forms.GroupBox()
        Me.ConsolidationOptionComboBox = New VIBlend.WinForms.Controls.vComboBox()
        Me.CurrencyConversionComboBox = New VIBlend.WinForms.Controls.vComboBox()
        Me.m_accountNameLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_accountFormulaTypeLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_accountTypeLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_accountConsolidationOptionLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_accountCurrenciesConversionLabel = New VIBlend.WinForms.Controls.vLabel()
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
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.m_accountDescriptionGroupbox.SuspendLayout()
        Me.m_accountFormulaGroupbox.SuspendLayout()
        Me.m_accountInformationGroupbox.SuspendLayout()
        Me.TVRCM.SuspendLayout()
        Me.MainMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.AccountsTVPanel)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(981, 692)
        Me.SplitContainer1.SplitterDistance = 285
        Me.SplitContainer1.TabIndex = 23
        '
        'AccountsTVPanel
        '
        Me.AccountsTVPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AccountsTVPanel.Location = New System.Drawing.Point(1, 35)
        Me.AccountsTVPanel.Margin = New System.Windows.Forms.Padding(1)
        Me.AccountsTVPanel.Name = "AccountsTVPanel"
        Me.AccountsTVPanel.Size = New System.Drawing.Size(283, 642)
        Me.AccountsTVPanel.TabIndex = 3
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.TableLayoutPanel2)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.GlobalFactsPanel)
        Me.SplitContainer2.Panel2.Controls.Add(Me.m_globalFactsLabel)
        Me.SplitContainer2.Size = New System.Drawing.Size(692, 692)
        Me.SplitContainer2.SplitterDistance = 561
        Me.SplitContainer2.SplitterWidth = 3
        Me.SplitContainer2.TabIndex = 2
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.m_accountDescriptionGroupbox, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.m_accountFormulaGroupbox, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.m_accountInformationGroupbox, 0, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 4
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 284.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.77032!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.22968!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(561, 692)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'm_accountDescriptionGroupbox
        '
        Me.m_accountDescriptionGroupbox.Controls.Add(Me.m_descriptionTextBox)
        Me.m_accountDescriptionGroupbox.Controls.Add(Me.SaveDescriptionBT)
        Me.m_accountDescriptionGroupbox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_accountDescriptionGroupbox.Location = New System.Drawing.Point(3, 522)
        Me.m_accountDescriptionGroupbox.Name = "m_accountDescriptionGroupbox"
        Me.m_accountDescriptionGroupbox.Size = New System.Drawing.Size(555, 167)
        Me.m_accountDescriptionGroupbox.TabIndex = 20
        Me.m_accountDescriptionGroupbox.TabStop = False
        Me.m_accountDescriptionGroupbox.Text = "Account description"
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
        Me.m_descriptionTextBox.Size = New System.Drawing.Size(529, 101)
        Me.m_descriptionTextBox.TabIndex = 8
        Me.m_descriptionTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.m_descriptionTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'SaveDescriptionBT
        '
        Me.SaveDescriptionBT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SaveDescriptionBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.SaveDescriptionBT.ImageKey = "1420498403_340208.ico"
        Me.SaveDescriptionBT.ImageList = Me.EditButtonsImagelist
        Me.SaveDescriptionBT.Location = New System.Drawing.Point(348, 127)
        Me.SaveDescriptionBT.Margin = New System.Windows.Forms.Padding(2)
        Me.SaveDescriptionBT.Name = "SaveDescriptionBT"
        Me.SaveDescriptionBT.Size = New System.Drawing.Size(187, 28)
        Me.SaveDescriptionBT.TabIndex = 7
        Me.SaveDescriptionBT.Text = "[accounts_edition.save_description]"
        Me.SaveDescriptionBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SaveDescriptionBT.UseVisualStyleBackColor = True
        '
        'EditButtonsImagelist
        '
        Me.EditButtonsImagelist.ImageStream = CType(resources.GetObject("EditButtonsImagelist.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.EditButtonsImagelist.TransparentColor = System.Drawing.Color.Transparent
        Me.EditButtonsImagelist.Images.SetKeyName(0, "1420498403_340208.ico")
        Me.EditButtonsImagelist.Images.SetKeyName(1, "config circle purple.ico")
        '
        'm_accountFormulaGroupbox
        '
        Me.m_accountFormulaGroupbox.Controls.Add(Me.m_formulaTextBox)
        Me.m_accountFormulaGroupbox.Controls.Add(Me.formulaEdit)
        Me.m_accountFormulaGroupbox.Controls.Add(Me.submit_cmd)
        Me.m_accountFormulaGroupbox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_accountFormulaGroupbox.Location = New System.Drawing.Point(3, 313)
        Me.m_accountFormulaGroupbox.Name = "m_accountFormulaGroupbox"
        Me.m_accountFormulaGroupbox.Size = New System.Drawing.Size(555, 203)
        Me.m_accountFormulaGroupbox.TabIndex = 19
        Me.m_accountFormulaGroupbox.TabStop = False
        Me.m_accountFormulaGroupbox.Text = "Account formula"
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
        Me.m_formulaTextBox.Size = New System.Drawing.Size(529, 105)
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
        Me.formulaEdit.Size = New System.Drawing.Size(188, 25)
        Me.formulaEdit.TabIndex = 5
        Me.formulaEdit.Text = "[accounts_edition.edit_formula]"
        Me.formulaEdit.UseVisualStyleBackColor = True
        '
        'submit_cmd
        '
        Me.submit_cmd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.submit_cmd.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.submit_cmd.ImageKey = "1420498403_340208.ico"
        Me.submit_cmd.ImageList = Me.EditButtonsImagelist
        Me.submit_cmd.Location = New System.Drawing.Point(348, 162)
        Me.submit_cmd.Margin = New System.Windows.Forms.Padding(2)
        Me.submit_cmd.Name = "submit_cmd"
        Me.submit_cmd.Size = New System.Drawing.Size(187, 28)
        Me.submit_cmd.TabIndex = 7
        Me.submit_cmd.Text = "[accounts_edition.validate_formula]"
        Me.submit_cmd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.submit_cmd.UseVisualStyleBackColor = True
        '
        'm_accountInformationGroupbox
        '
        Me.m_accountInformationGroupbox.Controls.Add(Me.ConsolidationOptionComboBox)
        Me.m_accountInformationGroupbox.Controls.Add(Me.CurrencyConversionComboBox)
        Me.m_accountInformationGroupbox.Controls.Add(Me.m_accountNameLabel)
        Me.m_accountInformationGroupbox.Controls.Add(Me.m_accountFormulaTypeLabel)
        Me.m_accountInformationGroupbox.Controls.Add(Me.m_accountTypeLabel)
        Me.m_accountInformationGroupbox.Controls.Add(Me.m_accountConsolidationOptionLabel)
        Me.m_accountInformationGroupbox.Controls.Add(Me.m_accountCurrenciesConversionLabel)
        Me.m_accountInformationGroupbox.Controls.Add(Me.FormulaTypeComboBox)
        Me.m_accountInformationGroupbox.Controls.Add(Me.TypeComboBox)
        Me.m_accountInformationGroupbox.Controls.Add(Me.Name_TB)
        Me.m_accountInformationGroupbox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_accountInformationGroupbox.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.m_accountInformationGroupbox.Location = New System.Drawing.Point(2, 28)
        Me.m_accountInformationGroupbox.Margin = New System.Windows.Forms.Padding(2)
        Me.m_accountInformationGroupbox.Name = "m_accountInformationGroupbox"
        Me.m_accountInformationGroupbox.Padding = New System.Windows.Forms.Padding(2)
        Me.m_accountInformationGroupbox.Size = New System.Drawing.Size(557, 280)
        Me.m_accountInformationGroupbox.TabIndex = 17
        Me.m_accountInformationGroupbox.TabStop = False
        Me.m_accountInformationGroupbox.Text = "Account information"
        '
        'ConsolidationOptionComboBox
        '
        Me.ConsolidationOptionComboBox.BackColor = System.Drawing.Color.White
        Me.ConsolidationOptionComboBox.DisplayMember = ""
        Me.ConsolidationOptionComboBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.ConsolidationOptionComboBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.ConsolidationOptionComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.ConsolidationOptionComboBox.DropDownWidth = 216
        Me.ConsolidationOptionComboBox.Location = New System.Drawing.Point(160, 198)
        Me.ConsolidationOptionComboBox.Name = "ConsolidationOptionComboBox"
        Me.ConsolidationOptionComboBox.RoundedCornersMaskListItem = CType(15, Byte)
        Me.ConsolidationOptionComboBox.Size = New System.Drawing.Size(216, 20)
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
        Me.CurrencyConversionComboBox.DropDownWidth = 216
        Me.CurrencyConversionComboBox.Location = New System.Drawing.Point(161, 158)
        Me.CurrencyConversionComboBox.Name = "CurrencyConversionComboBox"
        Me.CurrencyConversionComboBox.RoundedCornersMaskListItem = CType(15, Byte)
        Me.CurrencyConversionComboBox.Size = New System.Drawing.Size(216, 20)
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
        Me.m_accountNameLabel.Text = "Name"
        Me.m_accountNameLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_accountNameLabel.UseMnemonics = True
        Me.m_accountNameLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_accountFormulaTypeLabel
        '
        Me.m_accountFormulaTypeLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_accountFormulaTypeLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_accountFormulaTypeLabel.Ellipsis = False
        Me.m_accountFormulaTypeLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_accountFormulaTypeLabel.Location = New System.Drawing.Point(20, 78)
        Me.m_accountFormulaTypeLabel.Multiline = True
        Me.m_accountFormulaTypeLabel.Name = "m_accountFormulaTypeLabel"
        Me.m_accountFormulaTypeLabel.Size = New System.Drawing.Size(129, 25)
        Me.m_accountFormulaTypeLabel.TabIndex = 44
        Me.m_accountFormulaTypeLabel.Text = "Formula type"
        Me.m_accountFormulaTypeLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_accountFormulaTypeLabel.UseMnemonics = True
        Me.m_accountFormulaTypeLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_accountTypeLabel
        '
        Me.m_accountTypeLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_accountTypeLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_accountTypeLabel.Ellipsis = False
        Me.m_accountTypeLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_accountTypeLabel.Location = New System.Drawing.Point(20, 117)
        Me.m_accountTypeLabel.Multiline = True
        Me.m_accountTypeLabel.Name = "m_accountTypeLabel"
        Me.m_accountTypeLabel.Size = New System.Drawing.Size(129, 25)
        Me.m_accountTypeLabel.TabIndex = 43
        Me.m_accountTypeLabel.Text = "Account type"
        Me.m_accountTypeLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_accountTypeLabel.UseMnemonics = True
        Me.m_accountTypeLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
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
        'm_accountCurrenciesConversionLabel
        '
        Me.m_accountCurrenciesConversionLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_accountCurrenciesConversionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_accountCurrenciesConversionLabel.Ellipsis = False
        Me.m_accountCurrenciesConversionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_accountCurrenciesConversionLabel.Location = New System.Drawing.Point(17, 158)
        Me.m_accountCurrenciesConversionLabel.Multiline = True
        Me.m_accountCurrenciesConversionLabel.Name = "m_accountCurrenciesConversionLabel"
        Me.m_accountCurrenciesConversionLabel.Size = New System.Drawing.Size(129, 25)
        Me.m_accountCurrenciesConversionLabel.TabIndex = 41
        Me.m_accountCurrenciesConversionLabel.Text = "Currencies conversion"
        Me.m_accountCurrenciesConversionLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_accountCurrenciesConversionLabel.UseMnemonics = True
        Me.m_accountCurrenciesConversionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'FormulaTypeComboBox
        '
        Me.FormulaTypeComboBox.BackColor = System.Drawing.Color.White
        Me.FormulaTypeComboBox.DisplayMember = ""
        Me.FormulaTypeComboBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.FormulaTypeComboBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.FormulaTypeComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.FormulaTypeComboBox.DropDownWidth = 216
        Me.FormulaTypeComboBox.Location = New System.Drawing.Point(161, 78)
        Me.FormulaTypeComboBox.Name = "FormulaTypeComboBox"
        Me.FormulaTypeComboBox.RoundedCornersMaskListItem = CType(15, Byte)
        Me.FormulaTypeComboBox.Size = New System.Drawing.Size(216, 20)
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
        Me.TypeComboBox.DropDownWidth = 216
        Me.TypeComboBox.Location = New System.Drawing.Point(161, 117)
        Me.TypeComboBox.Name = "TypeComboBox"
        Me.TypeComboBox.RoundedCornersMaskListItem = CType(15, Byte)
        Me.TypeComboBox.Size = New System.Drawing.Size(216, 20)
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
        Me.Name_TB.Location = New System.Drawing.Point(161, 41)
        Me.Name_TB.Margin = New System.Windows.Forms.Padding(2)
        Me.Name_TB.Name = "Name_TB"
        Me.Name_TB.Size = New System.Drawing.Size(258, 20)
        Me.Name_TB.TabIndex = 1
        '
        'GlobalFactsPanel
        '
        Me.GlobalFactsPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GlobalFactsPanel.Location = New System.Drawing.Point(12, 58)
        Me.GlobalFactsPanel.Name = "GlobalFactsPanel"
        Me.GlobalFactsPanel.Size = New System.Drawing.Size(114, 619)
        Me.GlobalFactsPanel.TabIndex = 3
        '
        'm_globalFactsLabel
        '
        Me.m_globalFactsLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_globalFactsLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_globalFactsLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_globalFactsLabel.Ellipsis = False
        Me.m_globalFactsLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_globalFactsLabel.Location = New System.Drawing.Point(12, 28)
        Me.m_globalFactsLabel.Multiline = True
        Me.m_globalFactsLabel.Name = "m_globalFactsLabel"
        Me.m_globalFactsLabel.Size = New System.Drawing.Size(136, 16)
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
        Me.TVRCM.Size = New System.Drawing.Size(295, 106)
        '
        'AddSubAccountToolStripMenuItem
        '
        Me.AddSubAccountToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.registry_add
        Me.AddSubAccountToolStripMenuItem.Name = "AddSubAccountToolStripMenuItem"
        Me.AddSubAccountToolStripMenuItem.Size = New System.Drawing.Size(294, 24)
        Me.AddSubAccountToolStripMenuItem.Text = "[accounts_edition.new_account]"
        '
        'AddCategoryToolStripMenuItem
        '
        Me.AddCategoryToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.favicon_81_
        Me.AddCategoryToolStripMenuItem.Name = "AddCategoryToolStripMenuItem"
        Me.AddCategoryToolStripMenuItem.Size = New System.Drawing.Size(294, 24)
        Me.AddCategoryToolStripMenuItem.Text = "[accounts_edition.add_tab_account]"
        '
        'DeleteAccountToolStripMenuItem
        '
        Me.DeleteAccountToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.registry_delete
        Me.DeleteAccountToolStripMenuItem.Name = "DeleteAccountToolStripMenuItem"
        Me.DeleteAccountToolStripMenuItem.Size = New System.Drawing.Size(294, 24)
        Me.DeleteAccountToolStripMenuItem.Text = "[accounts_edition.delete_account]"
        '
        'DropHierarchyToExcelToolStripMenuItem
        '
        Me.DropHierarchyToExcelToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.Excel_Blue_32x32
        Me.DropHierarchyToExcelToolStripMenuItem.Name = "DropHierarchyToExcelToolStripMenuItem"
        Me.DropHierarchyToExcelToolStripMenuItem.Size = New System.Drawing.Size(294, 24)
        Me.DropHierarchyToExcelToolStripMenuItem.Text = "[accounts_edition.drop_to_excel]"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(291, 6)
        '
        'MainMenu
        '
        Me.MainMenu.BackColor = System.Drawing.SystemColors.Control
        Me.MainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.DropHierarchyToExcelToolStripMenuItem1, Me.HelpToolStripMenuItem})
        Me.MainMenu.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu.Name = "MainMenu"
        Me.MainMenu.Size = New System.Drawing.Size(981, 27)
        Me.MainMenu.TabIndex = 25
        Me.MainMenu.Text = "MenuStrip1"
        '
        'NewToolStripMenuItem
        '
        Me.NewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CreateANewAccountToolStripMenuItem, Me.CreateANewCategoryToolStripMenuItem, Me.DeleteAccountToolStripMenuItem1, Me.ToolStripSeparator2})
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        Me.NewToolStripMenuItem.Size = New System.Drawing.Size(125, 23)
        Me.NewToolStripMenuItem.Text = "[general.account]"
        '
        'CreateANewAccountToolStripMenuItem
        '
        Me.CreateANewAccountToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.registry_add
        Me.CreateANewAccountToolStripMenuItem.Name = "CreateANewAccountToolStripMenuItem"
        Me.CreateANewAccountToolStripMenuItem.Size = New System.Drawing.Size(296, 24)
        Me.CreateANewAccountToolStripMenuItem.Text = "[accounts_edition.new_account]"
        '
        'CreateANewCategoryToolStripMenuItem
        '
        Me.CreateANewCategoryToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.favicon_81_
        Me.CreateANewCategoryToolStripMenuItem.Name = "CreateANewCategoryToolStripMenuItem"
        Me.CreateANewCategoryToolStripMenuItem.Size = New System.Drawing.Size(296, 24)
        Me.CreateANewCategoryToolStripMenuItem.Text = "[accounts_edition.new_tab_account]"
        '
        'DeleteAccountToolStripMenuItem1
        '
        Me.DeleteAccountToolStripMenuItem1.Image = Global.FinancialBI.My.Resources.Resources.registry_delete
        Me.DeleteAccountToolStripMenuItem1.Name = "DeleteAccountToolStripMenuItem1"
        Me.DeleteAccountToolStripMenuItem1.Size = New System.Drawing.Size(296, 24)
        Me.DeleteAccountToolStripMenuItem1.Text = "[accounts_edition.delete_account]"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(293, 6)
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
        Me.DropAllAccountsHierarchyToExcelToolStripMenuItem.Size = New System.Drawing.Size(391, 24)
        Me.DropAllAccountsHierarchyToExcelToolStripMenuItem.Text = "[accounts_edition.drop_to_excel]"
        '
        'DropSelectedAccountHierarchyToExcelToolStripMenuItem
        '
        Me.DropSelectedAccountHierarchyToExcelToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.Excel_Green_32x32
        Me.DropSelectedAccountHierarchyToExcelToolStripMenuItem.Name = "DropSelectedAccountHierarchyToExcelToolStripMenuItem"
        Me.DropSelectedAccountHierarchyToExcelToolStripMenuItem.Size = New System.Drawing.Size(391, 24)
        Me.DropSelectedAccountHierarchyToExcelToolStripMenuItem.Text = "[accounts_edition.drop_selected_hierarchy_to_excel]"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(103, 23)
        Me.HelpToolStripMenuItem.Text = "[general.help]"
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
        Me.Size = New System.Drawing.Size(981, 692)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.m_accountDescriptionGroupbox.ResumeLayout(False)
        Me.m_accountFormulaGroupbox.ResumeLayout(False)
        Me.m_accountFormulaGroupbox.PerformLayout()
        Me.m_accountInformationGroupbox.ResumeLayout(False)
        Me.m_accountInformationGroupbox.PerformLayout()
        Me.TVRCM.ResumeLayout(False)
        Me.MainMenu.ResumeLayout(False)
        Me.MainMenu.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents m_accountInformationGroupbox As System.Windows.Forms.GroupBox
    Friend WithEvents Name_TB As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents m_accountFormulaGroupbox As System.Windows.Forms.GroupBox
    Friend WithEvents formulaEdit As System.Windows.Forms.CheckBox
    Friend WithEvents submit_cmd As System.Windows.Forms.Button
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
    Friend WithEvents m_accountDescriptionGroupbox As System.Windows.Forms.GroupBox
    Friend WithEvents SaveDescriptionBT As System.Windows.Forms.Button
    Friend WithEvents FormulaTypeComboBox As VIBlend.WinForms.Controls.vComboBox
    Friend WithEvents TypeComboBox As VIBlend.WinForms.Controls.vComboBox
    Friend WithEvents m_accountConsolidationOptionLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_accountCurrenciesConversionLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_accountNameLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_accountFormulaTypeLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_accountTypeLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents ConsolidationOptionComboBox As VIBlend.WinForms.Controls.vComboBox
    Friend WithEvents CurrencyConversionComboBox As VIBlend.WinForms.Controls.vComboBox
    Friend WithEvents GlobalFactsPanel As System.Windows.Forms.Panel
    Friend WithEvents m_globalFactsLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents m_descriptionTextBox As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents m_formulaTextBox As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents AccountsTVPanel As System.Windows.Forms.Panel
    Friend WithEvents m_globalFactsImageList As System.Windows.Forms.ImageList

End Class
