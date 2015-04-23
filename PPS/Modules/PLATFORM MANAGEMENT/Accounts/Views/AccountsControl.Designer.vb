<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AccountsControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AccountsControl))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.AccountsTVPanel = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.flux_RB = New System.Windows.Forms.RadioButton()
        Me.bs_item_RB = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.recompute_RB = New System.Windows.Forms.RadioButton()
        Me.aggregation_RB = New System.Windows.Forms.RadioButton()
        Me.formulaTypeCB = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TypeCB = New System.Windows.Forms.ComboBox()
        Me.formatsCB = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Name_TB = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.formulaEdit = New System.Windows.Forms.CheckBox()
        Me.submit_cmd = New System.Windows.Forms.Button()
        Me.formula_TB = New System.Windows.Forms.TextBox()
        Me.accountsIL = New System.Windows.Forms.ImageList(Me.components)
        Me.EditButtonsImagelist = New System.Windows.Forms.ImageList(Me.components)
        Me.TVRCM = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.PositionsBCDGW = New System.ComponentModel.BackgroundWorker()
        Me.MainMenu = New System.Windows.Forms.MenuStrip()
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreateANewCategoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.DropHierarchyToExcelToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.DropAllAccountsHierarchyToExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DropSelectedAccountHierarchyToExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreateANewAccountToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteAccountToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddSubAccountToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddCategoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteAccountToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DropHierarchyToExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.TableLayoutPanel2)
        Me.SplitContainer1.Size = New System.Drawing.Size(772, 544)
        Me.SplitContainer1.SplitterDistance = 192
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
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(192, 544)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(1, 1)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(190, 23)
        Me.Panel1.TabIndex = 1
        '
        'AccountsTVPanel
        '
        Me.AccountsTVPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AccountsTVPanel.Location = New System.Drawing.Point(1, 26)
        Me.AccountsTVPanel.Margin = New System.Windows.Forms.Padding(1)
        Me.AccountsTVPanel.Name = "AccountsTVPanel"
        Me.AccountsTVPanel.Size = New System.Drawing.Size(190, 517)
        Me.AccountsTVPanel.TabIndex = 2
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.GroupBox1, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.GroupBox3, 0, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 308.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(576, 544)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.formulaTypeCB)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.TypeCB)
        Me.GroupBox1.Controls.Add(Me.formatsCB)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Name_TB)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.GroupBox1.Location = New System.Drawing.Point(2, 2)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Size = New System.Drawing.Size(572, 304)
        Me.GroupBox1.TabIndex = 17
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Account's information"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.flux_RB)
        Me.GroupBox4.Controls.Add(Me.bs_item_RB)
        Me.GroupBox4.Location = New System.Drawing.Point(39, 226)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(479, 45)
        Me.GroupBox4.TabIndex = 27
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Conversion Option"
        '
        'flux_RB
        '
        Me.flux_RB.AutoSize = True
        Me.flux_RB.Location = New System.Drawing.Point(140, 19)
        Me.flux_RB.Name = "flux_RB"
        Me.flux_RB.Size = New System.Drawing.Size(44, 17)
        Me.flux_RB.TabIndex = 24
        Me.flux_RB.TabStop = True
        Me.flux_RB.Text = "Flux"
        Me.flux_RB.UseVisualStyleBackColor = True
        '
        'bs_item_RB
        '
        Me.bs_item_RB.AutoSize = True
        Me.bs_item_RB.Location = New System.Drawing.Point(275, 19)
        Me.bs_item_RB.Name = "bs_item_RB"
        Me.bs_item_RB.Size = New System.Drawing.Size(118, 17)
        Me.bs_item_RB.TabIndex = 25
        Me.bs_item_RB.TabStop = True
        Me.bs_item_RB.Text = "Balance Sheet Item"
        Me.bs_item_RB.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.recompute_RB)
        Me.GroupBox2.Controls.Add(Me.aggregation_RB)
        Me.GroupBox2.Location = New System.Drawing.Point(39, 174)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(479, 45)
        Me.GroupBox2.TabIndex = 26
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Computation Option"
        '
        'recompute_RB
        '
        Me.recompute_RB.AutoSize = True
        Me.recompute_RB.Location = New System.Drawing.Point(275, 20)
        Me.recompute_RB.Name = "recompute_RB"
        Me.recompute_RB.Size = New System.Drawing.Size(97, 17)
        Me.recompute_RB.TabIndex = 21
        Me.recompute_RB.TabStop = True
        Me.recompute_RB.Text = "Recomputation"
        Me.recompute_RB.UseVisualStyleBackColor = True
        '
        'aggregation_RB
        '
        Me.aggregation_RB.AutoSize = True
        Me.aggregation_RB.Location = New System.Drawing.Point(140, 17)
        Me.aggregation_RB.Name = "aggregation_RB"
        Me.aggregation_RB.Size = New System.Drawing.Size(82, 17)
        Me.aggregation_RB.TabIndex = 20
        Me.aggregation_RB.TabStop = True
        Me.aggregation_RB.Text = "Aggregation"
        Me.aggregation_RB.UseVisualStyleBackColor = True
        '
        'formulaTypeCB
        '
        Me.formulaTypeCB.FormattingEnabled = True
        Me.formulaTypeCB.Location = New System.Drawing.Point(179, 67)
        Me.formulaTypeCB.Margin = New System.Windows.Forms.Padding(2)
        Me.formulaTypeCB.Name = "formulaTypeCB"
        Me.formulaTypeCB.Size = New System.Drawing.Size(339, 21)
        Me.formulaTypeCB.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(36, 71)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 13)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "Formula/ Input"
        '
        'TypeCB
        '
        Me.TypeCB.FormattingEnabled = True
        Me.TypeCB.Location = New System.Drawing.Point(179, 138)
        Me.TypeCB.Margin = New System.Windows.Forms.Padding(2)
        Me.TypeCB.Name = "TypeCB"
        Me.TypeCB.Size = New System.Drawing.Size(339, 21)
        Me.TypeCB.TabIndex = 4
        '
        'formatsCB
        '
        Me.formatsCB.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.formatsCB.FormattingEnabled = True
        Me.formatsCB.Location = New System.Drawing.Point(179, 102)
        Me.formatsCB.Margin = New System.Windows.Forms.Padding(2)
        Me.formatsCB.Name = "formatsCB"
        Me.formatsCB.Size = New System.Drawing.Size(339, 21)
        Me.formatsCB.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(36, 105)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Format"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(36, 142)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(113, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Monetary/ Operational"
        '
        'Name_TB
        '
        Me.Name_TB.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Name_TB.Location = New System.Drawing.Point(179, 33)
        Me.Name_TB.Margin = New System.Windows.Forms.Padding(2)
        Me.Name_TB.Name = "Name_TB"
        Me.Name_TB.Size = New System.Drawing.Size(339, 20)
        Me.Name_TB.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(36, 37)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Account's name"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.formulaEdit)
        Me.GroupBox3.Controls.Add(Me.submit_cmd)
        Me.GroupBox3.Controls.Add(Me.formula_TB)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Location = New System.Drawing.Point(3, 311)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(570, 230)
        Me.GroupBox3.TabIndex = 19
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Formula"
        '
        'formulaEdit
        '
        Me.formulaEdit.Appearance = System.Windows.Forms.Appearance.Button
        Me.formulaEdit.AutoSize = True
        Me.formulaEdit.BackColor = System.Drawing.Color.AliceBlue
        Me.formulaEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.formulaEdit.ImageKey = "config circle purple.ico"
        Me.formulaEdit.ImageList = Me.EditButtonsImagelist
        Me.formulaEdit.Location = New System.Drawing.Point(20, 23)
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
        Me.submit_cmd.Location = New System.Drawing.Point(421, 196)
        Me.submit_cmd.Margin = New System.Windows.Forms.Padding(2)
        Me.submit_cmd.Name = "submit_cmd"
        Me.submit_cmd.Size = New System.Drawing.Size(145, 28)
        Me.submit_cmd.TabIndex = 7
        Me.submit_cmd.Text = "Validate Formula"
        Me.submit_cmd.UseVisualStyleBackColor = True
        '
        'formula_TB
        '
        Me.formula_TB.AllowDrop = True
        Me.formula_TB.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.formula_TB.BackColor = System.Drawing.SystemColors.HighlightText
        Me.formula_TB.Location = New System.Drawing.Point(20, 52)
        Me.formula_TB.Margin = New System.Windows.Forms.Padding(2)
        Me.formula_TB.Multiline = True
        Me.formula_TB.Name = "formula_TB"
        Me.formula_TB.Size = New System.Drawing.Size(545, 134)
        Me.formula_TB.TabIndex = 6
        '
        'accountsIL
        '
        Me.accountsIL.ImageStream = CType(resources.GetObject("accountsIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.accountsIL.TransparentColor = System.Drawing.Color.Transparent
        Me.accountsIL.Images.SetKeyName(0, "favicon(81).ico")
        Me.accountsIL.Images.SetKeyName(1, "sum purple.png")
        Me.accountsIL.Images.SetKeyName(2, "config blue circle.png")
        Me.accountsIL.Images.SetKeyName(3, "func.png")
        Me.accountsIL.Images.SetKeyName(4, "BS Blue.png")
        Me.accountsIL.Images.SetKeyName(5, "WC blue.png")
        '
        'EditButtonsImagelist
        '
        Me.EditButtonsImagelist.ImageStream = CType(resources.GetObject("EditButtonsImagelist.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.EditButtonsImagelist.TransparentColor = System.Drawing.Color.Transparent
        Me.EditButtonsImagelist.Images.SetKeyName(0, "1420498403_340208.ico")
        Me.EditButtonsImagelist.Images.SetKeyName(1, "config circle purple.ico")
        '
        'TVRCM
        '
        Me.TVRCM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddSubAccountToolStripMenuItem, Me.AddCategoryToolStripMenuItem, Me.DeleteAccountToolStripMenuItem, Me.DropHierarchyToExcelToolStripMenuItem, Me.ToolStripSeparator1})
        Me.TVRCM.Name = "ContextMenuStripTV"
        Me.TVRCM.Size = New System.Drawing.Size(198, 98)
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(194, 6)
        '
        'PositionsBCDGW
        '
        '
        'MainMenu
        '
        Me.MainMenu.BackColor = System.Drawing.SystemColors.Control
        Me.MainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.DropHierarchyToExcelToolStripMenuItem1, Me.HelpToolStripMenuItem})
        Me.MainMenu.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu.Name = "MainMenu"
        Me.MainMenu.Size = New System.Drawing.Size(772, 24)
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
        'CreateANewCategoryToolStripMenuItem
        '
        Me.CreateANewCategoryToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.favicon_81_
        Me.CreateANewCategoryToolStripMenuItem.Name = "CreateANewCategoryToolStripMenuItem"
        Me.CreateANewCategoryToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.CreateANewCategoryToolStripMenuItem.Text = "Create a new Category"
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
        Me.DropAllAccountsHierarchyToExcelToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.Excel_Blue_32x32
        Me.DropAllAccountsHierarchyToExcelToolStripMenuItem.Name = "DropAllAccountsHierarchyToExcelToolStripMenuItem"
        Me.DropAllAccountsHierarchyToExcelToolStripMenuItem.Size = New System.Drawing.Size(291, 22)
        Me.DropAllAccountsHierarchyToExcelToolStripMenuItem.Text = "Drop all Accounts Hierarchy to Excel"
        '
        'DropSelectedAccountHierarchyToExcelToolStripMenuItem
        '
        Me.DropSelectedAccountHierarchyToExcelToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.Excel_Green_32x32
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
        'CreateANewAccountToolStripMenuItem
        '
        Me.CreateANewAccountToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.registry_add
        Me.CreateANewAccountToolStripMenuItem.Name = "CreateANewAccountToolStripMenuItem"
        Me.CreateANewAccountToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.CreateANewAccountToolStripMenuItem.Text = "Create a new Account"
        '
        'DeleteAccountToolStripMenuItem1
        '
        Me.DeleteAccountToolStripMenuItem1.Image = Global.PPS.My.Resources.Resources.registry_delete
        Me.DeleteAccountToolStripMenuItem1.Name = "DeleteAccountToolStripMenuItem1"
        Me.DeleteAccountToolStripMenuItem1.Size = New System.Drawing.Size(193, 22)
        Me.DeleteAccountToolStripMenuItem1.Text = "Delete Account"
        '
        'AddSubAccountToolStripMenuItem
        '
        Me.AddSubAccountToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.registry_add
        Me.AddSubAccountToolStripMenuItem.Name = "AddSubAccountToolStripMenuItem"
        Me.AddSubAccountToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.AddSubAccountToolStripMenuItem.Text = "Add Sub Account"
        '
        'AddCategoryToolStripMenuItem
        '
        Me.AddCategoryToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.favicon_81_
        Me.AddCategoryToolStripMenuItem.Name = "AddCategoryToolStripMenuItem"
        Me.AddCategoryToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.AddCategoryToolStripMenuItem.Text = "Add Category"
        '
        'DeleteAccountToolStripMenuItem
        '
        Me.DeleteAccountToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.registry_delete
        Me.DeleteAccountToolStripMenuItem.Name = "DeleteAccountToolStripMenuItem"
        Me.DeleteAccountToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.DeleteAccountToolStripMenuItem.Text = "Delete Account"
        '
        'DropHierarchyToExcelToolStripMenuItem
        '
        Me.DropHierarchyToExcelToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.Excel_Blue_32x32
        Me.DropHierarchyToExcelToolStripMenuItem.Name = "DropHierarchyToExcelToolStripMenuItem"
        Me.DropHierarchyToExcelToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.DropHierarchyToExcelToolStripMenuItem.Text = "Drop Hierarchy to Excel"
        '
        'AccountsControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.MainMenu)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "AccountsControl"
        Me.Size = New System.Drawing.Size(772, 544)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.TVRCM.ResumeLayout(False)
        Me.MainMenu.ResumeLayout(False)
        Me.MainMenu.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents AccountsTVPanel As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents flux_RB As System.Windows.Forms.RadioButton
    Friend WithEvents bs_item_RB As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents recompute_RB As System.Windows.Forms.RadioButton
    Friend WithEvents aggregation_RB As System.Windows.Forms.RadioButton
    Friend WithEvents formulaTypeCB As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TypeCB As System.Windows.Forms.ComboBox
    Friend WithEvents formatsCB As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Name_TB As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
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
    Friend WithEvents PositionsBCDGW As System.ComponentModel.BackgroundWorker
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

End Class
