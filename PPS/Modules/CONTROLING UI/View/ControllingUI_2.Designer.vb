<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ControllingUI_2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ControllingUI_2))
        Me.MenuIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.TVsImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.categoriesIL = New System.Windows.Forms.ImageList(Me.components)
        Me.entitiesRightClickMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.compute_complete = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.SelectAllSubEntitiesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UnselectAllSubEntitiesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.periodsRightClickMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SelectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UnselectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VersionsIL = New System.Windows.Forms.ImageList(Me.components)
        Me.EntitiesTVImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.DGVsRCM = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DisplayAdjustmensRCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.DisplayDataTrackingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.FormatsRCMBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.CustomTabControl2 = New System.Windows.Forms.CustomTabControl()
        Me.MainTab = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TVTableLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.TabControl1 = New System.Windows.Forms.CustomTabControl()
        Me.ChartsTab = New System.Windows.Forms.TabPage()
        Me.MenuTableLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.entityTB = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.VersionTB = New System.Windows.Forms.TextBox()
        Me.CurrencyTB = New System.Windows.Forms.TextBox()
        Me.RibbonsPanel = New System.Windows.Forms.Panel()
        Me.RefreshBT = New System.Windows.Forms.Button()
        Me.MainMenu = New System.Windows.Forms.MenuStrip()
        Me.SelectionMBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.BusinessControlMBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelMBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.AdjustmentsRCM = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SelectAllToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.UnselectAllToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.entitiesRightClickMenu.SuspendLayout()
        Me.periodsRightClickMenu.SuspendLayout()
        Me.DGVsRCM.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.CustomTabControl2.SuspendLayout()
        Me.MainTab.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.MenuTableLayout.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.MainMenu.SuspendLayout()
        Me.AdjustmentsRCM.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuIcons
        '
        Me.MenuIcons.ImageStream = CType(resources.GetObject("MenuIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.MenuIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.MenuIcons.Images.SetKeyName(0, "chart.ico")
        Me.MenuIcons.Images.SetKeyName(1, "Bars 16-16.ico")
        Me.MenuIcons.Images.SetKeyName(2, "favicon(57).ico")
        Me.MenuIcons.Images.SetKeyName(3, "favicon(54).ico")
        Me.MenuIcons.Images.SetKeyName(4, "favicon(30).ico")
        Me.MenuIcons.Images.SetKeyName(5, "favicon(15).ico")
        Me.MenuIcons.Images.SetKeyName(6, "favicon(31).ico")
        Me.MenuIcons.Images.SetKeyName(7, "Benjigarner-Softdimension-Excel.ico")
        Me.MenuIcons.Images.SetKeyName(8, "favicon(20).ico")
        '
        'TVsImageList
        '
        Me.TVsImageList.ImageStream = CType(resources.GetObject("TVsImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.TVsImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.TVsImageList.Images.SetKeyName(0, "imageres_148.ico")
        Me.TVsImageList.Images.SetKeyName(1, "file icon blue small.png")
        '
        'categoriesIL
        '
        Me.categoriesIL.ImageStream = CType(resources.GetObject("categoriesIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.categoriesIL.TransparentColor = System.Drawing.Color.Transparent
        Me.categoriesIL.Images.SetKeyName(0, "favicon(195).ico")
        Me.categoriesIL.Images.SetKeyName(1, "folder-blue.png")
        Me.categoriesIL.Images.SetKeyName(2, "DB Grey.png")
        '
        'entitiesRightClickMenu
        '
        Me.entitiesRightClickMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.compute_complete, Me.ToolStripSeparator1, Me.SelectAllSubEntitiesToolStripMenuItem, Me.UnselectAllSubEntitiesToolStripMenuItem})
        Me.entitiesRightClickMenu.Name = "ContextMenuStripEntitiesNodes"
        Me.entitiesRightClickMenu.Size = New System.Drawing.Size(201, 76)
        '
        'compute_complete
        '
        Me.compute_complete.Image = Global.PPS.My.Resources.Resources.Report
        Me.compute_complete.Name = "compute_complete"
        Me.compute_complete.Size = New System.Drawing.Size(200, 22)
        Me.compute_complete.Text = "Display Complete Entity"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(197, 6)
        '
        'SelectAllSubEntitiesToolStripMenuItem
        '
        Me.SelectAllSubEntitiesToolStripMenuItem.Name = "SelectAllSubEntitiesToolStripMenuItem"
        Me.SelectAllSubEntitiesToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.SelectAllSubEntitiesToolStripMenuItem.Text = "Select All Sub Entities"
        '
        'UnselectAllSubEntitiesToolStripMenuItem
        '
        Me.UnselectAllSubEntitiesToolStripMenuItem.Name = "UnselectAllSubEntitiesToolStripMenuItem"
        Me.UnselectAllSubEntitiesToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.UnselectAllSubEntitiesToolStripMenuItem.Text = "Unselect All Sub Entities"
        '
        'periodsRightClickMenu
        '
        Me.periodsRightClickMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectAllToolStripMenuItem, Me.UnselectAllToolStripMenuItem})
        Me.periodsRightClickMenu.Name = "periodsRightClickMenu"
        Me.periodsRightClickMenu.Size = New System.Drawing.Size(137, 48)
        '
        'SelectAllToolStripMenuItem
        '
        Me.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem"
        Me.SelectAllToolStripMenuItem.Size = New System.Drawing.Size(136, 22)
        Me.SelectAllToolStripMenuItem.Text = "Select All"
        '
        'UnselectAllToolStripMenuItem
        '
        Me.UnselectAllToolStripMenuItem.Name = "UnselectAllToolStripMenuItem"
        Me.UnselectAllToolStripMenuItem.Size = New System.Drawing.Size(136, 22)
        Me.UnselectAllToolStripMenuItem.Text = "Unselect All"
        '
        'VersionsIL
        '
        Me.VersionsIL.ImageStream = CType(resources.GetObject("VersionsIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.VersionsIL.TransparentColor = System.Drawing.Color.Transparent
        Me.VersionsIL.Images.SetKeyName(0, "favicon(217).ico")
        Me.VersionsIL.Images.SetKeyName(1, "favicon(97).ico")
        '
        'EntitiesTVImageList
        '
        Me.EntitiesTVImageList.ImageStream = CType(resources.GetObject("EntitiesTVImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.EntitiesTVImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.EntitiesTVImageList.Images.SetKeyName(0, "favicon(81).ico")
        Me.EntitiesTVImageList.Images.SetKeyName(1, "favicon(110).ico")
        '
        'DGVsRCM
        '
        Me.DGVsRCM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DisplayAdjustmensRCM, Me.ToolStripSeparator2, Me.DisplayDataTrackingToolStripMenuItem, Me.ToolStripSeparator4, Me.FormatsRCMBT})
        Me.DGVsRCM.Name = "DGVsRCM"
        Me.DGVsRCM.Size = New System.Drawing.Size(189, 82)
        '
        'DisplayAdjustmensRCM
        '
        Me.DisplayAdjustmensRCM.Name = "DisplayAdjustmensRCM"
        Me.DisplayAdjustmensRCM.Size = New System.Drawing.Size(188, 22)
        Me.DisplayAdjustmensRCM.Text = "Display Adjustments"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(185, 6)
        '
        'DisplayDataTrackingToolStripMenuItem
        '
        Me.DisplayDataTrackingToolStripMenuItem.Name = "DisplayDataTrackingToolStripMenuItem"
        Me.DisplayDataTrackingToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.DisplayDataTrackingToolStripMenuItem.Text = "Display Data Tracking"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(185, 6)
        '
        'FormatsRCMBT
        '
        Me.FormatsRCMBT.Image = Global.PPS.My.Resources.Resources.Actions_format_stroke_color_icon
        Me.FormatsRCMBT.Name = "FormatsRCMBT"
        Me.FormatsRCMBT.Size = New System.Drawing.Size(188, 22)
        Me.FormatsRCMBT.Text = "Display Options"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.CustomTabControl2, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.MenuTableLayout, 0, 0)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 27)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 61.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(911, 622)
        Me.TableLayoutPanel2.TabIndex = 13
        '
        'CustomTabControl2
        '
        Me.CustomTabControl2.Controls.Add(Me.MainTab)
        Me.CustomTabControl2.Controls.Add(Me.ChartsTab)
        '
        '
        '
        Me.CustomTabControl2.DisplayStyleProvider.BorderColor = System.Drawing.SystemColors.ControlDark
        Me.CustomTabControl2.DisplayStyleProvider.BorderColorHot = System.Drawing.SystemColors.ControlDark
        Me.CustomTabControl2.DisplayStyleProvider.BorderColorSelected = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.CustomTabControl2.DisplayStyleProvider.CloserColor = System.Drawing.Color.DarkGray
        Me.CustomTabControl2.DisplayStyleProvider.FocusTrack = True
        Me.CustomTabControl2.DisplayStyleProvider.HotTrack = True
        Me.CustomTabControl2.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CustomTabControl2.DisplayStyleProvider.Opacity = 1.0!
        Me.CustomTabControl2.DisplayStyleProvider.Overlap = 0
        Me.CustomTabControl2.DisplayStyleProvider.Padding = New System.Drawing.Point(6, 3)
        Me.CustomTabControl2.DisplayStyleProvider.Radius = 2
        Me.CustomTabControl2.DisplayStyleProvider.ShowTabCloser = False
        Me.CustomTabControl2.DisplayStyleProvider.TextColor = System.Drawing.SystemColors.ControlText
        Me.CustomTabControl2.DisplayStyleProvider.TextColorDisabled = System.Drawing.SystemColors.ControlDark
        Me.CustomTabControl2.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText
        Me.CustomTabControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CustomTabControl2.HotTrack = True
        Me.CustomTabControl2.Location = New System.Drawing.Point(0, 61)
        Me.CustomTabControl2.Margin = New System.Windows.Forms.Padding(0)
        Me.CustomTabControl2.Name = "CustomTabControl2"
        Me.CustomTabControl2.SelectedIndex = 0
        Me.CustomTabControl2.Size = New System.Drawing.Size(911, 561)
        Me.CustomTabControl2.TabIndex = 15
        '
        'MainTab
        '
        Me.MainTab.BackColor = System.Drawing.SystemColors.Control
        Me.MainTab.Controls.Add(Me.SplitContainer1)
        Me.MainTab.Location = New System.Drawing.Point(4, 23)
        Me.MainTab.Margin = New System.Windows.Forms.Padding(0)
        Me.MainTab.Name = "MainTab"
        Me.MainTab.Padding = New System.Windows.Forms.Padding(3)
        Me.MainTab.Size = New System.Drawing.Size(903, 534)
        Me.MainTab.TabIndex = 0
        Me.MainTab.Text = "Data"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BackColor = System.Drawing.SystemColors.Control
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TVTableLayout)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.SplitContainer1.Panel2.Controls.Add(Me.TabControl1)
        Me.SplitContainer1.Size = New System.Drawing.Size(897, 528)
        Me.SplitContainer1.SplitterDistance = 201
        Me.SplitContainer1.SplitterWidth = 3
        Me.SplitContainer1.TabIndex = 11
        '
        'TVTableLayout
        '
        Me.TVTableLayout.BackColor = System.Drawing.SystemColors.Control
        Me.TVTableLayout.ColumnCount = 1
        Me.TVTableLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TVTableLayout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TVTableLayout.Location = New System.Drawing.Point(0, 0)
        Me.TVTableLayout.Name = "TVTableLayout"
        Me.TVTableLayout.RowCount = 2
        Me.TVTableLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TVTableLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 276.0!))
        Me.TVTableLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TVTableLayout.Size = New System.Drawing.Size(201, 528)
        Me.TVTableLayout.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.DisplayStyle = System.Windows.Forms.TabStyle.Rounded
        '
        '
        '
        Me.TabControl1.DisplayStyleProvider.BorderColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.TabControl1.DisplayStyleProvider.BorderColorHot = System.Drawing.SystemColors.GradientActiveCaption
        Me.TabControl1.DisplayStyleProvider.BorderColorSelected = System.Drawing.SystemColors.ActiveCaption
        Me.TabControl1.DisplayStyleProvider.CloserColor = System.Drawing.Color.Crimson
        Me.TabControl1.DisplayStyleProvider.CloserColorActive = System.Drawing.Color.Crimson
        Me.TabControl1.DisplayStyleProvider.FocusColor = System.Drawing.Color.Black
        Me.TabControl1.DisplayStyleProvider.FocusTrack = False
        Me.TabControl1.DisplayStyleProvider.HotTrack = True
        Me.TabControl1.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.TabControl1.DisplayStyleProvider.Opacity = 1.0!
        Me.TabControl1.DisplayStyleProvider.Overlap = 0
        Me.TabControl1.DisplayStyleProvider.Padding = New System.Drawing.Point(6, 3)
        Me.TabControl1.DisplayStyleProvider.Radius = 10
        Me.TabControl1.DisplayStyleProvider.ShowTabCloser = True
        Me.TabControl1.DisplayStyleProvider.TextColor = System.Drawing.SystemColors.ControlText
        Me.TabControl1.DisplayStyleProvider.TextColorDisabled = System.Drawing.SystemColors.ControlDark
        Me.TabControl1.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.HotTrack = True
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(693, 528)
        Me.TabControl1.TabIndex = 7
        '
        'ChartsTab
        '
        Me.ChartsTab.Location = New System.Drawing.Point(4, 23)
        Me.ChartsTab.Margin = New System.Windows.Forms.Padding(0)
        Me.ChartsTab.Name = "ChartsTab"
        Me.ChartsTab.Padding = New System.Windows.Forms.Padding(3)
        Me.ChartsTab.Size = New System.Drawing.Size(903, 534)
        Me.ChartsTab.TabIndex = 1
        Me.ChartsTab.Text = "Charts"
        Me.ChartsTab.UseVisualStyleBackColor = True
        '
        'MenuTableLayout
        '
        Me.MenuTableLayout.ColumnCount = 3
        Me.MenuTableLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.5!))
        Me.MenuTableLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.5!))
        Me.MenuTableLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 271.0!))
        Me.MenuTableLayout.Controls.Add(Me.TableLayoutPanel1, 2, 0)
        Me.MenuTableLayout.Controls.Add(Me.RibbonsPanel, 0, 0)
        Me.MenuTableLayout.Controls.Add(Me.RefreshBT, 1, 0)
        Me.MenuTableLayout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MenuTableLayout.Location = New System.Drawing.Point(0, 0)
        Me.MenuTableLayout.Margin = New System.Windows.Forms.Padding(0)
        Me.MenuTableLayout.Name = "MenuTableLayout"
        Me.MenuTableLayout.RowCount = 1
        Me.MenuTableLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 78.0!))
        Me.MenuTableLayout.Size = New System.Drawing.Size(911, 61)
        Me.MenuTableLayout.TabIndex = 12
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 218.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.entityTB, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.VersionTB, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.CurrencyTB, 1, 2)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(641, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(270, 60)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 5)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Entity"
        '
        'entityTB
        '
        Me.entityTB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.entityTB.Location = New System.Drawing.Point(52, 0)
        Me.entityTB.Margin = New System.Windows.Forms.Padding(0)
        Me.entityTB.Name = "entityTB"
        Me.entityTB.Size = New System.Drawing.Size(218, 20)
        Me.entityTB.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 25)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Version"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 43)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 17)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Currency"
        '
        'VersionTB
        '
        Me.VersionTB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VersionTB.Location = New System.Drawing.Point(52, 20)
        Me.VersionTB.Margin = New System.Windows.Forms.Padding(0)
        Me.VersionTB.Name = "VersionTB"
        Me.VersionTB.Size = New System.Drawing.Size(218, 20)
        Me.VersionTB.TabIndex = 6
        '
        'CurrencyTB
        '
        Me.CurrencyTB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CurrencyTB.Location = New System.Drawing.Point(52, 40)
        Me.CurrencyTB.Margin = New System.Windows.Forms.Padding(0)
        Me.CurrencyTB.Name = "CurrencyTB"
        Me.CurrencyTB.Size = New System.Drawing.Size(218, 20)
        Me.CurrencyTB.TabIndex = 7
        '
        'RibbonsPanel
        '
        Me.RibbonsPanel.BackColor = System.Drawing.SystemColors.Control
        Me.RibbonsPanel.Location = New System.Drawing.Point(15, 0)
        Me.RibbonsPanel.Margin = New System.Windows.Forms.Padding(15, 0, 0, 0)
        Me.RibbonsPanel.Name = "RibbonsPanel"
        Me.RibbonsPanel.Size = New System.Drawing.Size(471, 50)
        Me.RibbonsPanel.TabIndex = 7
        '
        'RefreshBT
        '
        Me.RefreshBT.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RefreshBT.BackgroundImage = Global.PPS.My.Resources.Resources.Refresh2
        Me.RefreshBT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.RefreshBT.FlatAppearance.BorderSize = 0
        Me.RefreshBT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.RefreshBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RefreshBT.Location = New System.Drawing.Point(591, 5)
        Me.RefreshBT.Margin = New System.Windows.Forms.Padding(0, 5, 10, 0)
        Me.RefreshBT.Name = "RefreshBT"
        Me.RefreshBT.Size = New System.Drawing.Size(38, 35)
        Me.RefreshBT.TabIndex = 6
        Me.RefreshBT.UseVisualStyleBackColor = True
        '
        'MainMenu
        '
        Me.MainMenu.BackColor = System.Drawing.SystemColors.Control
        Me.MainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectionMBT, Me.BusinessControlMBT, Me.ExcelMBT})
        Me.MainMenu.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu.Name = "MainMenu"
        Me.MainMenu.Size = New System.Drawing.Size(911, 24)
        Me.MainMenu.TabIndex = 1
        Me.MainMenu.Text = "MenuStrip2"
        '
        'SelectionMBT
        '
        Me.SelectionMBT.CheckOnClick = True
        Me.SelectionMBT.Name = "SelectionMBT"
        Me.SelectionMBT.Size = New System.Drawing.Size(67, 20)
        Me.SelectionMBT.Text = "Selection"
        '
        'BusinessControlMBT
        '
        Me.BusinessControlMBT.CheckOnClick = True
        Me.BusinessControlMBT.Name = "BusinessControlMBT"
        Me.BusinessControlMBT.Size = New System.Drawing.Size(107, 20)
        Me.BusinessControlMBT.Text = "Business Control"
        '
        'ExcelMBT
        '
        Me.ExcelMBT.CheckOnClick = True
        Me.ExcelMBT.Name = "ExcelMBT"
        Me.ExcelMBT.Size = New System.Drawing.Size(45, 20)
        Me.ExcelMBT.Text = "Excel"
        '
        'AdjustmentsRCM
        '
        Me.AdjustmentsRCM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectAllToolStripMenuItem1, Me.UnselectAllToolStripMenuItem1})
        Me.AdjustmentsRCM.Name = "AdjustmentsRCM"
        Me.AdjustmentsRCM.Size = New System.Drawing.Size(137, 48)
        '
        'SelectAllToolStripMenuItem1
        '
        Me.SelectAllToolStripMenuItem1.Name = "SelectAllToolStripMenuItem1"
        Me.SelectAllToolStripMenuItem1.Size = New System.Drawing.Size(136, 22)
        Me.SelectAllToolStripMenuItem1.Text = "Select All"
        '
        'UnselectAllToolStripMenuItem1
        '
        Me.UnselectAllToolStripMenuItem1.Name = "UnselectAllToolStripMenuItem1"
        Me.UnselectAllToolStripMenuItem1.Size = New System.Drawing.Size(136, 22)
        Me.UnselectAllToolStripMenuItem1.Text = "Unselect All"
        '
        'ControllingUI_2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(911, 649)
        Me.Controls.Add(Me.MainMenu)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "ControllingUI_2"
        Me.Text = "Financials"
        Me.entitiesRightClickMenu.ResumeLayout(False)
        Me.periodsRightClickMenu.ResumeLayout(False)
        Me.DGVsRCM.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.CustomTabControl2.ResumeLayout(False)
        Me.MainTab.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.MenuTableLayout.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.MainMenu.ResumeLayout(False)
        Me.MainMenu.PerformLayout()
        Me.AdjustmentsRCM.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuIcons As System.Windows.Forms.ImageList
    Friend WithEvents TVsImageList As System.Windows.Forms.ImageList
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents categoriesIL As System.Windows.Forms.ImageList
    Friend WithEvents entitiesRightClickMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SelectAllSubEntitiesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UnselectAllSubEntitiesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents periodsRightClickMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SelectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UnselectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VersionsIL As System.Windows.Forms.ImageList
    Friend WithEvents compute_complete As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents EntitiesTVImageList As System.Windows.Forms.ImageList
    Friend WithEvents DGVsRCM As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DisplayDataTrackingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DisplayAdjustmensRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents MenuTableLayout As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents entityTB As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents VersionTB As System.Windows.Forms.TextBox
    Friend WithEvents CurrencyTB As System.Windows.Forms.TextBox
    Friend WithEvents MainMenu As System.Windows.Forms.MenuStrip
    Friend WithEvents SelectionMBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CustomTabControl2 As System.Windows.Forms.CustomTabControl
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TVTableLayout As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TabControl1 As System.Windows.Forms.CustomTabControl
    Friend WithEvents MainTab As System.Windows.Forms.TabPage
    Friend WithEvents ChartsTab As System.Windows.Forms.TabPage
    Friend WithEvents AdjustmentsRCM As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SelectAllToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UnselectAllToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FormatsRCMBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BusinessControlMBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExcelMBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshBT As System.Windows.Forms.Button
    Friend WithEvents RibbonsPanel As System.Windows.Forms.Panel
End Class
