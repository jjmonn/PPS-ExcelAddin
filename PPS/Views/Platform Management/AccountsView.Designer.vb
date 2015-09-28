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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.AccountsTVPanel = New System.Windows.Forms.Panel()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.SaveDescriptionBT = New System.Windows.Forms.Button()
        Me.EditButtonsImagelist = New System.Windows.Forms.ImageList(Me.components)
        Me.DescriptionTB = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.formulaEdit = New System.Windows.Forms.CheckBox()
        Me.submit_cmd = New System.Windows.Forms.Button()
        Me.formula_TB = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ConsolidationOptionComboBox = New VIBlend.WinForms.Controls.vComboBox()
        Me.CurrencyConversionComboBox = New VIBlend.WinForms.Controls.vComboBox()
        Me.VLabel5 = New VIBlend.WinForms.Controls.vLabel()
        Me.VLabel4 = New VIBlend.WinForms.Controls.vLabel()
        Me.VLabel2 = New VIBlend.WinForms.Controls.vLabel()
        Me.VLabel3 = New VIBlend.WinForms.Controls.vLabel()
        Me.VLabel1 = New VIBlend.WinForms.Controls.vLabel()
        Me.FormulaTypeComboBox = New VIBlend.WinForms.Controls.vComboBox()
        Me.TypeComboBox = New VIBlend.WinForms.Controls.vComboBox()
        Me.Name_TB = New System.Windows.Forms.TextBox()
        Me.GlobalFactsPanel = New System.Windows.Forms.Panel()
        Me.VLabel6 = New VIBlend.WinForms.Controls.vLabel()
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
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.TableLayoutPanel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(844, 652)
        Me.SplitContainer1.SplitterDistance = 246
        Me.SplitContainer1.TabIndex = 23
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.AccountsTVPanel, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(246, 652)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(1, 1)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(244, 23)
        Me.Panel1.TabIndex = 1
        '
        'AccountsTVPanel
        '
        Me.AccountsTVPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AccountsTVPanel.Location = New System.Drawing.Point(1, 26)
        Me.AccountsTVPanel.Margin = New System.Windows.Forms.Padding(1)
        Me.AccountsTVPanel.Name = "AccountsTVPanel"
        Me.AccountsTVPanel.Size = New System.Drawing.Size(244, 625)
        Me.AccountsTVPanel.TabIndex = 2
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
        Me.SplitContainer2.Panel2.Controls.Add(Me.VLabel6)
        Me.SplitContainer2.Size = New System.Drawing.Size(594, 652)
        Me.SplitContainer2.SplitterDistance = 482
        Me.SplitContainer2.SplitterWidth = 3
        Me.SplitContainer2.TabIndex = 2
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.GroupBox5, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.GroupBox3, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.GroupBox1, 0, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 4
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 284.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.77032!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.22968!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(482, 652)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.SaveDescriptionBT)
        Me.GroupBox5.Controls.Add(Me.DescriptionTB)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox5.Location = New System.Drawing.Point(3, 500)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(476, 149)
        Me.GroupBox5.TabIndex = 20
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Account's Description"
        '
        'SaveDescriptionBT
        '
        Me.SaveDescriptionBT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SaveDescriptionBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.SaveDescriptionBT.ImageKey = "1420498403_340208.ico"
        Me.SaveDescriptionBT.ImageList = Me.EditButtonsImagelist
        Me.SaveDescriptionBT.Location = New System.Drawing.Point(269, 109)
        Me.SaveDescriptionBT.Margin = New System.Windows.Forms.Padding(2)
        Me.SaveDescriptionBT.Name = "SaveDescriptionBT"
        Me.SaveDescriptionBT.Size = New System.Drawing.Size(187, 28)
        Me.SaveDescriptionBT.TabIndex = 7
        Me.SaveDescriptionBT.Text = "Save Description"
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
        'DescriptionTB
        '
        Me.DescriptionTB.AllowDrop = True
        Me.DescriptionTB.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DescriptionTB.BackColor = System.Drawing.SystemColors.HighlightText
        Me.DescriptionTB.Location = New System.Drawing.Point(6, 30)
        Me.DescriptionTB.Margin = New System.Windows.Forms.Padding(2)
        Me.DescriptionTB.Multiline = True
        Me.DescriptionTB.Name = "DescriptionTB"
        Me.DescriptionTB.Size = New System.Drawing.Size(450, 75)
        Me.DescriptionTB.TabIndex = 6
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.formulaEdit)
        Me.GroupBox3.Controls.Add(Me.submit_cmd)
        Me.GroupBox3.Controls.Add(Me.formula_TB)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Location = New System.Drawing.Point(3, 313)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(476, 181)
        Me.GroupBox3.TabIndex = 19
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Account's Formula"
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
        Me.formulaEdit.Size = New System.Drawing.Size(93, 23)
        Me.formulaEdit.TabIndex = 5
        Me.formulaEdit.Text = "Edit Formula      "
        Me.formulaEdit.UseVisualStyleBackColor = True
        '
        'submit_cmd
        '
        Me.submit_cmd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.submit_cmd.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.submit_cmd.ImageKey = "1420498403_340208.ico"
        Me.submit_cmd.ImageList = Me.EditButtonsImagelist
        Me.submit_cmd.Location = New System.Drawing.Point(269, 140)
        Me.submit_cmd.Margin = New System.Windows.Forms.Padding(2)
        Me.submit_cmd.Name = "submit_cmd"
        Me.submit_cmd.Size = New System.Drawing.Size(187, 28)
        Me.submit_cmd.TabIndex = 7
        Me.submit_cmd.Text = "Validate Formula"
        Me.submit_cmd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.submit_cmd.UseVisualStyleBackColor = True
        '
        'formula_TB
        '
        Me.formula_TB.AllowDrop = True
        Me.formula_TB.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.formula_TB.BackColor = System.Drawing.SystemColors.HighlightText
        Me.formula_TB.Location = New System.Drawing.Point(5, 51)
        Me.formula_TB.Margin = New System.Windows.Forms.Padding(2)
        Me.formula_TB.Multiline = True
        Me.formula_TB.Name = "formula_TB"
        Me.formula_TB.Size = New System.Drawing.Size(451, 85)
        Me.formula_TB.TabIndex = 6
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ConsolidationOptionComboBox)
        Me.GroupBox1.Controls.Add(Me.CurrencyConversionComboBox)
        Me.GroupBox1.Controls.Add(Me.VLabel5)
        Me.GroupBox1.Controls.Add(Me.VLabel4)
        Me.GroupBox1.Controls.Add(Me.VLabel2)
        Me.GroupBox1.Controls.Add(Me.VLabel3)
        Me.GroupBox1.Controls.Add(Me.VLabel1)
        Me.GroupBox1.Controls.Add(Me.FormulaTypeComboBox)
        Me.GroupBox1.Controls.Add(Me.TypeComboBox)
        Me.GroupBox1.Controls.Add(Me.Name_TB)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.GroupBox1.Location = New System.Drawing.Point(2, 28)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Size = New System.Drawing.Size(478, 280)
        Me.GroupBox1.TabIndex = 17
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Account's information"
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
        'VLabel5
        '
        Me.VLabel5.BackColor = System.Drawing.Color.Transparent
        Me.VLabel5.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel5.Ellipsis = False
        Me.VLabel5.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel5.Location = New System.Drawing.Point(20, 41)
        Me.VLabel5.Multiline = True
        Me.VLabel5.Name = "VLabel5"
        Me.VLabel5.Size = New System.Drawing.Size(129, 25)
        Me.VLabel5.TabIndex = 45
        Me.VLabel5.Text = "Account's Name"
        Me.VLabel5.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel5.UseMnemonics = True
        Me.VLabel5.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VLabel4
        '
        Me.VLabel4.BackColor = System.Drawing.Color.Transparent
        Me.VLabel4.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel4.Ellipsis = False
        Me.VLabel4.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel4.Location = New System.Drawing.Point(20, 78)
        Me.VLabel4.Multiline = True
        Me.VLabel4.Name = "VLabel4"
        Me.VLabel4.Size = New System.Drawing.Size(129, 25)
        Me.VLabel4.TabIndex = 44
        Me.VLabel4.Text = "Formula Type"
        Me.VLabel4.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel4.UseMnemonics = True
        Me.VLabel4.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VLabel2
        '
        Me.VLabel2.BackColor = System.Drawing.Color.Transparent
        Me.VLabel2.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel2.Ellipsis = False
        Me.VLabel2.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel2.Location = New System.Drawing.Point(20, 117)
        Me.VLabel2.Multiline = True
        Me.VLabel2.Name = "VLabel2"
        Me.VLabel2.Size = New System.Drawing.Size(129, 25)
        Me.VLabel2.TabIndex = 43
        Me.VLabel2.Text = "Account Type"
        Me.VLabel2.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel2.UseMnemonics = True
        Me.VLabel2.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VLabel3
        '
        Me.VLabel3.BackColor = System.Drawing.Color.Transparent
        Me.VLabel3.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel3.Ellipsis = False
        Me.VLabel3.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel3.Location = New System.Drawing.Point(17, 198)
        Me.VLabel3.Multiline = True
        Me.VLabel3.Name = "VLabel3"
        Me.VLabel3.Size = New System.Drawing.Size(129, 25)
        Me.VLabel3.TabIndex = 42
        Me.VLabel3.Text = "Consolidation Option"
        Me.VLabel3.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel3.UseMnemonics = True
        Me.VLabel3.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VLabel1
        '
        Me.VLabel1.BackColor = System.Drawing.Color.Transparent
        Me.VLabel1.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel1.Ellipsis = False
        Me.VLabel1.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel1.Location = New System.Drawing.Point(17, 158)
        Me.VLabel1.Multiline = True
        Me.VLabel1.Name = "VLabel1"
        Me.VLabel1.Size = New System.Drawing.Size(129, 25)
        Me.VLabel1.TabIndex = 41
        Me.VLabel1.Text = "Currencies Conversion"
        Me.VLabel1.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel1.UseMnemonics = True
        Me.VLabel1.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
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
        Me.GlobalFactsPanel.Size = New System.Drawing.Size(85, 579)
        Me.GlobalFactsPanel.TabIndex = 3
        '
        'VLabel6
        '
        Me.VLabel6.BackColor = System.Drawing.Color.Transparent
        Me.VLabel6.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel6.Ellipsis = False
        Me.VLabel6.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel6.Location = New System.Drawing.Point(12, 28)
        Me.VLabel6.Multiline = True
        Me.VLabel6.Name = "VLabel6"
        Me.VLabel6.Size = New System.Drawing.Size(110, 16)
        Me.VLabel6.TabIndex = 0
        Me.VLabel6.Text = "Market Indexes"
        Me.VLabel6.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel6.UseMnemonics = True
        Me.VLabel6.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
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
        Me.TVRCM.Size = New System.Drawing.Size(198, 98)
        '
        'AddSubAccountToolStripMenuItem
        '
        Me.AddSubAccountToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.registry_add
        Me.AddSubAccountToolStripMenuItem.Name = "AddSubAccountToolStripMenuItem"
        Me.AddSubAccountToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.AddSubAccountToolStripMenuItem.Text = "Add Sub Account"
        '
        'AddCategoryToolStripMenuItem
        '
        Me.AddCategoryToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.favicon_81_
        Me.AddCategoryToolStripMenuItem.Name = "AddCategoryToolStripMenuItem"
        Me.AddCategoryToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.AddCategoryToolStripMenuItem.Text = "Add Category"
        '
        'DeleteAccountToolStripMenuItem
        '
        Me.DeleteAccountToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.registry_delete
        Me.DeleteAccountToolStripMenuItem.Name = "DeleteAccountToolStripMenuItem"
        Me.DeleteAccountToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.DeleteAccountToolStripMenuItem.Text = "Delete Account"
        '
        'DropHierarchyToExcelToolStripMenuItem
        '
        Me.DropHierarchyToExcelToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.Excel_Blue_32x32
        Me.DropHierarchyToExcelToolStripMenuItem.Name = "DropHierarchyToExcelToolStripMenuItem"
        Me.DropHierarchyToExcelToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.DropHierarchyToExcelToolStripMenuItem.Text = "Drop Hierarchy to Excel"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(194, 6)
        '
        'MainMenu
        '
        Me.MainMenu.BackColor = System.Drawing.SystemColors.Control
        Me.MainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.DropHierarchyToExcelToolStripMenuItem1, Me.HelpToolStripMenuItem})
        Me.MainMenu.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu.Name = "MainMenu"
        Me.MainMenu.Size = New System.Drawing.Size(844, 24)
        Me.MainMenu.TabIndex = 25
        Me.MainMenu.Text = "MenuStrip1"
        '
        'NewToolStripMenuItem
        '
        Me.NewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CreateANewAccountToolStripMenuItem, Me.CreateANewCategoryToolStripMenuItem, Me.DeleteAccountToolStripMenuItem1, Me.ToolStripSeparator2})
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        Me.NewToolStripMenuItem.Size = New System.Drawing.Size(64, 20)
        Me.NewToolStripMenuItem.Text = "Account"
        '
        'CreateANewAccountToolStripMenuItem
        '
        Me.CreateANewAccountToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.registry_add
        Me.CreateANewAccountToolStripMenuItem.Name = "CreateANewAccountToolStripMenuItem"
        Me.CreateANewAccountToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.CreateANewAccountToolStripMenuItem.Text = "Create a new Account"
        '
        'CreateANewCategoryToolStripMenuItem
        '
        Me.CreateANewCategoryToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.favicon_81_
        Me.CreateANewCategoryToolStripMenuItem.Name = "CreateANewCategoryToolStripMenuItem"
        Me.CreateANewCategoryToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.CreateANewCategoryToolStripMenuItem.Text = "Create a new Category"
        '
        'DeleteAccountToolStripMenuItem1
        '
        Me.DeleteAccountToolStripMenuItem1.Image = Global.FinancialBI.My.Resources.Resources.registry_delete
        Me.DeleteAccountToolStripMenuItem1.Name = "DeleteAccountToolStripMenuItem1"
        Me.DeleteAccountToolStripMenuItem1.Size = New System.Drawing.Size(193, 22)
        Me.DeleteAccountToolStripMenuItem1.Text = "Delete Account"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(190, 6)
        '
        'DropHierarchyToExcelToolStripMenuItem1
        '
        Me.DropHierarchyToExcelToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DropAllAccountsHierarchyToExcelToolStripMenuItem, Me.DropSelectedAccountHierarchyToExcelToolStripMenuItem})
        Me.DropHierarchyToExcelToolStripMenuItem1.Name = "DropHierarchyToExcelToolStripMenuItem1"
        Me.DropHierarchyToExcelToolStripMenuItem1.Size = New System.Drawing.Size(45, 20)
        Me.DropHierarchyToExcelToolStripMenuItem1.Text = "Excel"
        '
        'DropAllAccountsHierarchyToExcelToolStripMenuItem
        '
        Me.DropAllAccountsHierarchyToExcelToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.Excel_Blue_32x32
        Me.DropAllAccountsHierarchyToExcelToolStripMenuItem.Name = "DropAllAccountsHierarchyToExcelToolStripMenuItem"
        Me.DropAllAccountsHierarchyToExcelToolStripMenuItem.Size = New System.Drawing.Size(291, 22)
        Me.DropAllAccountsHierarchyToExcelToolStripMenuItem.Text = "Drop all Accounts Hierarchy to Excel"
        '
        'DropSelectedAccountHierarchyToExcelToolStripMenuItem
        '
        Me.DropSelectedAccountHierarchyToExcelToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.Excel_Green_32x32
        Me.DropSelectedAccountHierarchyToExcelToolStripMenuItem.Name = "DropSelectedAccountHierarchyToExcelToolStripMenuItem"
        Me.DropSelectedAccountHierarchyToExcelToolStripMenuItem.Size = New System.Drawing.Size(291, 22)
        Me.DropSelectedAccountHierarchyToExcelToolStripMenuItem.Text = "Drop selected Account Hierarchy to Excel"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'AccountsView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.MainMenu)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "AccountsView"
        Me.Size = New System.Drawing.Size(844, 652)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TVRCM.ResumeLayout(False)
        Me.MainMenu.ResumeLayout(False)
        Me.MainMenu.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Name_TB As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents formulaEdit As System.Windows.Forms.CheckBox
    Friend WithEvents submit_cmd As System.Windows.Forms.Button
    Friend WithEvents formula_TB As System.Windows.Forms.TextBox
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
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents SaveDescriptionBT As System.Windows.Forms.Button
    Friend WithEvents DescriptionTB As System.Windows.Forms.TextBox
    Friend WithEvents FormulaTypeComboBox As VIBlend.WinForms.Controls.vComboBox
    Friend WithEvents TypeComboBox As VIBlend.WinForms.Controls.vComboBox
    Friend WithEvents VLabel3 As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents VLabel1 As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents VLabel5 As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents VLabel4 As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents VLabel2 As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents ConsolidationOptionComboBox As VIBlend.WinForms.Controls.vComboBox
    Friend WithEvents CurrencyConversionComboBox As VIBlend.WinForms.Controls.vComboBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents AccountsTVPanel As System.Windows.Forms.Panel
    Friend WithEvents GlobalFactsPanel As System.Windows.Forms.Panel
    Friend WithEvents VLabel6 As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer

End Class
