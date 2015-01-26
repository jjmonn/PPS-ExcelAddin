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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TVTableLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.TabControl1 = New System.Windows.Forms.CustomTabControl()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.categoriesIL = New System.Windows.Forms.ImageList(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.EntitiesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CategoriesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CurrenciesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PeriodsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VersionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DropCurrentEntToExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DropDrillDownToExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ControllingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddVersionsComparisonToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SwitchVersionsOrderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoveVersionsComparisonToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.entitiesRightClickMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.compute_complete = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.SelectAllSubEntitiesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UnselectAllSubEntitiesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.CurrencyTB = New System.Windows.Forms.TextBox()
        Me.VersionTB = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.entityTB = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.periodsRightClickMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SelectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UnselectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VersionsIL = New System.Windows.Forms.ImageList(Me.components)
        Me.EntitiesTVImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.DGVsRCM = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DisplayAdjustmensRCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.DisplayDataTrackingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.entitiesRightClickMenu.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.periodsRightClickMenu.SuspendLayout()
        Me.DGVsRCM.SuspendLayout()
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
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.BackColor = System.Drawing.SystemColors.Control
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 47)
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
        Me.SplitContainer1.Size = New System.Drawing.Size(1010, 563)
        Me.SplitContainer1.SplitterDistance = 230
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
        Me.TVTableLayout.Size = New System.Drawing.Size(230, 563)
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
        Me.TabControl1.Size = New System.Drawing.Size(777, 563)
        Me.TabControl1.TabIndex = 7
        '
        'categoriesIL
        '
        Me.categoriesIL.ImageStream = CType(resources.GetObject("categoriesIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.categoriesIL.TransparentColor = System.Drawing.Color.Transparent
        Me.categoriesIL.Images.SetKeyName(0, "favicon(195).ico")
        Me.categoriesIL.Images.SetKeyName(1, "folder-blue.png")
        Me.categoriesIL.Images.SetKeyName(2, "DB Grey.png")
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EntitiesToolStripMenuItem, Me.CategoriesToolStripMenuItem, Me.CurrenciesToolStripMenuItem, Me.PeriodsToolStripMenuItem, Me.VersionsToolStripMenuItem, Me.ExcelToolStripMenuItem, Me.RefreshToolStripMenuItem, Me.ControllingToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1010, 24)
        Me.MenuStrip1.TabIndex = 12
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'EntitiesToolStripMenuItem
        '
        Me.EntitiesToolStripMenuItem.CheckOnClick = True
        Me.EntitiesToolStripMenuItem.Name = "EntitiesToolStripMenuItem"
        Me.EntitiesToolStripMenuItem.Size = New System.Drawing.Size(57, 20)
        Me.EntitiesToolStripMenuItem.Text = "Entities"
        '
        'CategoriesToolStripMenuItem
        '
        Me.CategoriesToolStripMenuItem.CheckOnClick = True
        Me.CategoriesToolStripMenuItem.Name = "CategoriesToolStripMenuItem"
        Me.CategoriesToolStripMenuItem.Size = New System.Drawing.Size(75, 20)
        Me.CategoriesToolStripMenuItem.Text = "Categories"
        '
        'CurrenciesToolStripMenuItem
        '
        Me.CurrenciesToolStripMenuItem.CheckOnClick = True
        Me.CurrenciesToolStripMenuItem.Name = "CurrenciesToolStripMenuItem"
        Me.CurrenciesToolStripMenuItem.Size = New System.Drawing.Size(75, 20)
        Me.CurrenciesToolStripMenuItem.Text = "Currencies"
        '
        'PeriodsToolStripMenuItem
        '
        Me.PeriodsToolStripMenuItem.CheckOnClick = True
        Me.PeriodsToolStripMenuItem.Name = "PeriodsToolStripMenuItem"
        Me.PeriodsToolStripMenuItem.Size = New System.Drawing.Size(58, 20)
        Me.PeriodsToolStripMenuItem.Text = "Periods"
        '
        'VersionsToolStripMenuItem
        '
        Me.VersionsToolStripMenuItem.CheckOnClick = True
        Me.VersionsToolStripMenuItem.Name = "VersionsToolStripMenuItem"
        Me.VersionsToolStripMenuItem.Size = New System.Drawing.Size(63, 20)
        Me.VersionsToolStripMenuItem.Text = "Versions"
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DropCurrentEntToExcelToolStripMenuItem, Me.DropDrillDownToExcelToolStripMenuItem})
        Me.ExcelToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.Benjigarner_Softdimension_Excel
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
        Me.ExcelToolStripMenuItem.Text = "Excel"
        '
        'DropCurrentEntToExcelToolStripMenuItem
        '
        Me.DropCurrentEntToExcelToolStripMenuItem.Name = "DropCurrentEntToExcelToolStripMenuItem"
        Me.DropCurrentEntToExcelToolStripMenuItem.Size = New System.Drawing.Size(217, 22)
        Me.DropCurrentEntToExcelToolStripMenuItem.Text = "Drop current Entity to Excel"
        '
        'DropDrillDownToExcelToolStripMenuItem
        '
        Me.DropDrillDownToExcelToolStripMenuItem.Name = "DropDrillDownToExcelToolStripMenuItem"
        Me.DropDrillDownToExcelToolStripMenuItem.Size = New System.Drawing.Size(217, 22)
        Me.DropDrillDownToExcelToolStripMenuItem.Text = "Drop drill down to Excel"
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.refresh_greay_bcgd
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(74, 20)
        Me.RefreshToolStripMenuItem.Text = "Refresh"
        '
        'ControllingToolStripMenuItem
        '
        Me.ControllingToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddVersionsComparisonToolStripMenuItem, Me.SwitchVersionsOrderToolStripMenuItem, Me.RemoveVersionsComparisonToolStripMenuItem})
        Me.ControllingToolStripMenuItem.Name = "ControllingToolStripMenuItem"
        Me.ControllingToolStripMenuItem.Size = New System.Drawing.Size(79, 20)
        Me.ControllingToolStripMenuItem.Text = "Controlling"
        '
        'AddVersionsComparisonToolStripMenuItem
        '
        Me.AddVersionsComparisonToolStripMenuItem.Name = "AddVersionsComparisonToolStripMenuItem"
        Me.AddVersionsComparisonToolStripMenuItem.Size = New System.Drawing.Size(232, 22)
        Me.AddVersionsComparisonToolStripMenuItem.Text = "Add Versions Comparison"
        '
        'SwitchVersionsOrderToolStripMenuItem
        '
        Me.SwitchVersionsOrderToolStripMenuItem.Name = "SwitchVersionsOrderToolStripMenuItem"
        Me.SwitchVersionsOrderToolStripMenuItem.Size = New System.Drawing.Size(232, 22)
        Me.SwitchVersionsOrderToolStripMenuItem.Text = "Switch Versions Order"
        '
        'RemoveVersionsComparisonToolStripMenuItem
        '
        Me.RemoveVersionsComparisonToolStripMenuItem.Name = "RemoveVersionsComparisonToolStripMenuItem"
        Me.RemoveVersionsComparisonToolStripMenuItem.Size = New System.Drawing.Size(232, 22)
        Me.RemoveVersionsComparisonToolStripMenuItem.Text = "Remove Versions Comparison"
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
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control
        Me.TableLayoutPanel1.ColumnCount = 6
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 176.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 58.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 148.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 57.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 71.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.CurrencyTB, 5, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.VersionTB, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.entityTB, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 2, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(457, 24)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(553, 25)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'CurrencyTB
        '
        Me.CurrencyTB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CurrencyTB.Enabled = False
        Me.CurrencyTB.Location = New System.Drawing.Point(485, 3)
        Me.CurrencyTB.Name = "CurrencyTB"
        Me.CurrencyTB.Size = New System.Drawing.Size(65, 20)
        Me.CurrencyTB.TabIndex = 7
        '
        'VersionTB
        '
        Me.VersionTB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VersionTB.Enabled = False
        Me.VersionTB.Location = New System.Drawing.Point(280, 3)
        Me.VersionTB.Name = "VersionTB"
        Me.VersionTB.Size = New System.Drawing.Size(142, 20)
        Me.VersionTB.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 7)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Entity"
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(430, 7)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Currency"
        '
        'entityTB
        '
        Me.entityTB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.entityTB.Enabled = False
        Me.entityTB.Location = New System.Drawing.Point(46, 3)
        Me.entityTB.Name = "entityTB"
        Me.entityTB.Size = New System.Drawing.Size(170, 20)
        Me.entityTB.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(232, 7)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Version"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        Me.DGVsRCM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DisplayAdjustmensRCM, Me.ToolStripSeparator2, Me.DisplayDataTrackingToolStripMenuItem})
        Me.DGVsRCM.Name = "DGVsRCM"
        Me.DGVsRCM.Size = New System.Drawing.Size(189, 54)
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
        'ControllingUI_2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1010, 610)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "ControllingUI_2"
        Me.Text = "Business Control"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.entitiesRightClickMenu.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.periodsRightClickMenu.ResumeLayout(False)
        Me.DGVsRCM.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuIcons As System.Windows.Forms.ImageList
    Friend WithEvents TVsImageList As System.Windows.Forms.ImageList
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents TabControl1 As System.Windows.Forms.CustomTabControl
    Friend WithEvents categoriesIL As System.Windows.Forms.ImageList
    Friend WithEvents TVTableLayout As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents EntitiesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CategoriesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CurrenciesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PeriodsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VersionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DropCurrentEntToExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ControllingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DropDrillDownToExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents entitiesRightClickMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SelectAllSubEntitiesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UnselectAllSubEntitiesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents CurrencyTB As System.Windows.Forms.TextBox
    Friend WithEvents VersionTB As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents entityTB As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents periodsRightClickMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SelectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UnselectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VersionsIL As System.Windows.Forms.ImageList
    Friend WithEvents AddVersionsComparisonToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SwitchVersionsOrderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemoveVersionsComparisonToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents compute_complete As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents EntitiesTVImageList As System.Windows.Forms.ImageList
    Friend WithEvents DGVsRCM As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DisplayDataTrackingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DisplayAdjustmensRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
End Class
