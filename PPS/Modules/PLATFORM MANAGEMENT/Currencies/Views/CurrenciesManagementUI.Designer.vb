<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CurrenciesManagementUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CurrenciesManagementUI))
        Dim DataGridLocalization1 As VIBlend.WinForms.DataGridView.DataGridLocalization = New VIBlend.WinForms.DataGridView.DataGridLocalization()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.versionsTV = New System.Windows.Forms.TreeView()
        Me.VersionsRCMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.select_version = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.AddRatesVersionRCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddFolderRCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteVersionRCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.ratesVersionsIL = New System.Windows.Forms.ImageList(Me.components)
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.rates_DGV = New VIBlend.WinForms.DataGridView.vDataGridView()
        Me.dgvRCM = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.expand_periods = New System.Windows.Forms.ToolStripMenuItem()
        Me.collapse_periods = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.rates_version_TB = New System.Windows.Forms.TextBox()
        Me.rates_chart = New ZedGraph.ZedGraphControl()
        Me.currenciesRCMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddCurrencyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteCurrencyToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuButtonIL = New System.Windows.Forms.ImageList(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.rates_versions_button = New System.Windows.Forms.ToolStripMenuItem()
        Me.CurrenciesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddNewCurrencyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.chart_button = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportFromExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.VersionsRCMenu.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.dgvRCM.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.currenciesRCMenu.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BackColor = System.Drawing.SystemColors.Control
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 24)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TableLayoutPanel2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(991, 578)
        Me.SplitContainer1.SplitterDistance = 153
        Me.SplitContainer1.SplitterWidth = 2
        Me.SplitContainer1.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.versionsTV, 0, 0)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 24)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(147, 551)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'versionsTV
        '
        Me.versionsTV.ContextMenuStrip = Me.VersionsRCMenu
        Me.versionsTV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.versionsTV.ImageIndex = 0
        Me.versionsTV.ImageList = Me.ratesVersionsIL
        Me.versionsTV.Location = New System.Drawing.Point(3, 3)
        Me.versionsTV.Margin = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.versionsTV.Name = "versionsTV"
        Me.versionsTV.SelectedImageIndex = 0
        Me.versionsTV.Size = New System.Drawing.Size(144, 520)
        Me.versionsTV.TabIndex = 0
        '
        'VersionsRCMenu
        '
        Me.VersionsRCMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.select_version, Me.ToolStripSeparator2, Me.AddRatesVersionRCM, Me.AddFolderRCM, Me.DeleteVersionRCM})
        Me.VersionsRCMenu.Name = "VersionsRCMenu"
        Me.VersionsRCMenu.Size = New System.Drawing.Size(148, 98)
        '
        'select_version
        '
        Me.select_version.Image = Global.PPS.My.Resources.Resources.checked
        Me.select_version.Name = "select_version"
        Me.select_version.Size = New System.Drawing.Size(147, 22)
        Me.select_version.Text = "Select Version"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(144, 6)
        '
        'AddRatesVersionRCM
        '
        Me.AddRatesVersionRCM.Image = Global.PPS.My.Resources.Resources.add21
        Me.AddRatesVersionRCM.Name = "AddRatesVersionRCM"
        Me.AddRatesVersionRCM.Size = New System.Drawing.Size(147, 22)
        Me.AddRatesVersionRCM.Text = "Add Version"
        '
        'AddFolderRCM
        '
        Me.AddFolderRCM.Image = Global.PPS.My.Resources.Resources.folder2
        Me.AddFolderRCM.Name = "AddFolderRCM"
        Me.AddFolderRCM.Size = New System.Drawing.Size(147, 22)
        Me.AddFolderRCM.Text = "Add Folder"
        '
        'DeleteVersionRCM
        '
        Me.DeleteVersionRCM.Image = Global.PPS.My.Resources.Resources.imageres_891
        Me.DeleteVersionRCM.Name = "DeleteVersionRCM"
        Me.DeleteVersionRCM.Size = New System.Drawing.Size(147, 22)
        Me.DeleteVersionRCM.Text = "Delete"
        '
        'ratesVersionsIL
        '
        Me.ratesVersionsIL.ImageStream = CType(resources.GetObject("ratesVersionsIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ratesVersionsIL.TransparentColor = System.Drawing.Color.Transparent
        Me.ratesVersionsIL.Images.SetKeyName(0, "favicon(149).ico")
        Me.ratesVersionsIL.Images.SetKeyName(1, "favicon(81).ico")
        Me.ratesVersionsIL.Images.SetKeyName(2, "imageres_157.ico")
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.TableLayoutPanel1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.rates_chart)
        Me.SplitContainer2.Size = New System.Drawing.Size(836, 578)
        Me.SplitContainer2.SplitterDistance = 353
        Me.SplitContainer2.SplitterWidth = 1
        Me.SplitContainer2.TabIndex = 2
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.rates_DGV, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(836, 353)
        Me.TableLayoutPanel1.TabIndex = 3
        '
        'rates_DGV
        '
        Me.rates_DGV.AllowAnimations = True
        Me.rates_DGV.AllowCellMerge = True
        Me.rates_DGV.AllowClipDrawing = True
        Me.rates_DGV.AllowContextMenuColumnChooser = True
        Me.rates_DGV.AllowContextMenuFiltering = True
        Me.rates_DGV.AllowContextMenuGrouping = True
        Me.rates_DGV.AllowContextMenuSorting = True
        Me.rates_DGV.AllowCopyPaste = False
        Me.rates_DGV.AllowDefaultContextMenu = True
        Me.rates_DGV.AllowDragDropIndication = True
        Me.rates_DGV.AllowHeaderItemHighlightOnCellSelection = True
        Me.rates_DGV.AutoUpdateOnListChanged = False
        Me.rates_DGV.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.rates_DGV.BindingProgressEnabled = False
        Me.rates_DGV.BindingProgressSampleRate = 20000
        Me.rates_DGV.BorderColor = System.Drawing.Color.Empty
        Me.rates_DGV.CellsArea.AllowCellMerge = True
        Me.rates_DGV.CellsArea.ConditionalFormattingEnabled = False
        Me.rates_DGV.ColumnsHierarchy.AllowDragDrop = False
        Me.rates_DGV.ColumnsHierarchy.AllowResize = True
        Me.rates_DGV.ColumnsHierarchy.AutoStretchColumns = False
        Me.rates_DGV.ColumnsHierarchy.Fixed = False
        Me.rates_DGV.ColumnsHierarchy.ShowExpandCollapseButtons = True
        Me.rates_DGV.ContextMenuStrip = Me.dgvRCM
        Me.rates_DGV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rates_DGV.EnableColumnChooser = False
        Me.rates_DGV.EnableResizeToolTip = True
        Me.rates_DGV.EnableToolTips = True
        Me.rates_DGV.FilterDisplayMode = VIBlend.WinForms.DataGridView.FilterDisplayMode.[Default]
        Me.rates_DGV.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rates_DGV.GridLinesDashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.rates_DGV.GridLinesDisplayMode = VIBlend.WinForms.DataGridView.GridLinesDisplayMode.DISPLAY_ALL
        Me.rates_DGV.GroupingEnabled = False
        Me.rates_DGV.HorizontalScroll = 0
        Me.rates_DGV.HorizontalScrollBarLargeChange = 20
        Me.rates_DGV.HorizontalScrollBarSmallChange = 5
        Me.rates_DGV.ImageList = Nothing
        Me.rates_DGV.Localization = DataGridLocalization1
        Me.rates_DGV.Location = New System.Drawing.Point(3, 28)
        Me.rates_DGV.MultipleSelectionEnabled = True
        Me.rates_DGV.Name = "rates_DGV"
        Me.rates_DGV.PivotColumnsTotalsEnabled = False
        Me.rates_DGV.PivotColumnsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH
        Me.rates_DGV.PivotRowsTotalsEnabled = False
        Me.rates_DGV.PivotRowsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH
        Me.rates_DGV.RowsHierarchy.AllowDragDrop = False
        Me.rates_DGV.RowsHierarchy.AllowResize = True
        Me.rates_DGV.RowsHierarchy.CompactStyleRenderingEnabled = False
        Me.rates_DGV.RowsHierarchy.CompactStyleRenderingItemsIndent = 15
        Me.rates_DGV.RowsHierarchy.Fixed = False
        Me.rates_DGV.RowsHierarchy.ShowExpandCollapseButtons = True
        Me.rates_DGV.ScrollBarsEnabled = True
        Me.rates_DGV.SelectionBorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.rates_DGV.SelectionBorderEnabled = True
        Me.rates_DGV.SelectionBorderWidth = 2
        Me.rates_DGV.SelectionMode = VIBlend.WinForms.DataGridView.vDataGridView.SELECTION_MODE.CELL_SELECT
        Me.rates_DGV.ShowBorder = True
        Me.rates_DGV.Size = New System.Drawing.Size(830, 323)
        Me.rates_DGV.TabIndex = 1
        Me.rates_DGV.Text = "VDataGridView1"
        Me.rates_DGV.ToolTipDuration = 5000
        Me.rates_DGV.ToolTipShowDelay = 1500
        Me.rates_DGV.VerticalScroll = 0
        Me.rates_DGV.VerticalScrollBarLargeChange = 20
        Me.rates_DGV.VerticalScrollBarSmallChange = 5
        Me.rates_DGV.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        Me.rates_DGV.VirtualModeCellDefault = False
        '
        'dgvRCM
        '
        Me.dgvRCM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.expand_periods, Me.collapse_periods})
        Me.dgvRCM.Name = "dgvRCM"
        Me.dgvRCM.Size = New System.Drawing.Size(177, 48)
        '
        'expand_periods
        '
        Me.expand_periods.Image = Global.PPS.My.Resources.Resources.images
        Me.expand_periods.Name = "expand_periods"
        Me.expand_periods.Size = New System.Drawing.Size(176, 22)
        Me.expand_periods.Text = "Expand all Periods"
        '
        'collapse_periods
        '
        Me.collapse_periods.Name = "collapse_periods"
        Me.collapse_periods.Size = New System.Drawing.Size(176, 22)
        Me.collapse_periods.Text = "Collapse all Periods"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 3
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 143.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 589.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.Label2, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.rates_version_TB, 1, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(1, 1)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(834, 23)
        Me.TableLayoutPanel3.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 5)
        Me.Label2.Margin = New System.Windows.Forms.Padding(5, 5, 3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Current Version"
        '
        'rates_version_TB
        '
        Me.rates_version_TB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rates_version_TB.Enabled = False
        Me.rates_version_TB.Location = New System.Drawing.Point(103, 1)
        Me.rates_version_TB.Margin = New System.Windows.Forms.Padding(1)
        Me.rates_version_TB.Name = "rates_version_TB"
        Me.rates_version_TB.Size = New System.Drawing.Size(141, 20)
        Me.rates_version_TB.TabIndex = 1
        '
        'rates_chart
        '
        Me.rates_chart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rates_chart.Location = New System.Drawing.Point(0, 0)
        Me.rates_chart.Margin = New System.Windows.Forms.Padding(0)
        Me.rates_chart.Name = "rates_chart"
        Me.rates_chart.ScrollGrace = 0.0R
        Me.rates_chart.ScrollMaxX = 0.0R
        Me.rates_chart.ScrollMaxY = 0.0R
        Me.rates_chart.ScrollMaxY2 = 0.0R
        Me.rates_chart.ScrollMinX = 0.0R
        Me.rates_chart.ScrollMinY = 0.0R
        Me.rates_chart.ScrollMinY2 = 0.0R
        Me.rates_chart.Size = New System.Drawing.Size(836, 224)
        Me.rates_chart.TabIndex = 4
        '
        'currenciesRCMenu
        '
        Me.currenciesRCMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddCurrencyToolStripMenuItem, Me.DeleteCurrencyToolStripMenuItem1})
        Me.currenciesRCMenu.Name = "currenciesRCMenu"
        Me.currenciesRCMenu.Size = New System.Drawing.Size(159, 48)
        '
        'AddCurrencyToolStripMenuItem
        '
        Me.AddCurrencyToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.checked
        Me.AddCurrencyToolStripMenuItem.Name = "AddCurrencyToolStripMenuItem"
        Me.AddCurrencyToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.AddCurrencyToolStripMenuItem.Text = "Add Currency"
        '
        'DeleteCurrencyToolStripMenuItem1
        '
        Me.DeleteCurrencyToolStripMenuItem1.Image = Global.PPS.My.Resources.Resources.imageres_891
        Me.DeleteCurrencyToolStripMenuItem1.Name = "DeleteCurrencyToolStripMenuItem1"
        Me.DeleteCurrencyToolStripMenuItem1.Size = New System.Drawing.Size(158, 22)
        Me.DeleteCurrencyToolStripMenuItem1.Text = "Delete Currency"
        '
        'MenuButtonIL
        '
        Me.MenuButtonIL.ImageStream = CType(resources.GetObject("MenuButtonIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.MenuButtonIL.TransparentColor = System.Drawing.Color.Transparent
        Me.MenuButtonIL.Images.SetKeyName(0, "expand right.png")
        Me.MenuButtonIL.Images.SetKeyName(1, "expandleft.png")
        Me.MenuButtonIL.Images.SetKeyName(2, "favicon(120).ico")
        Me.MenuButtonIL.Images.SetKeyName(3, "favicon(125).ico")
        Me.MenuButtonIL.Images.SetKeyName(4, "favicon(217).ico")
        Me.MenuButtonIL.Images.SetKeyName(5, "favicon(126).ico")
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.rates_versions_button, Me.CurrenciesToolStripMenuItem, Me.chart_button, Me.ImportFromExcelToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(991, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'rates_versions_button
        '
        Me.rates_versions_button.Name = "rates_versions_button"
        Me.rates_versions_button.Size = New System.Drawing.Size(94, 20)
        Me.rates_versions_button.Text = "Rates Versions"
        '
        'CurrenciesToolStripMenuItem
        '
        Me.CurrenciesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddNewCurrencyToolStripMenuItem, Me.ToolStripSeparator1})
        Me.CurrenciesToolStripMenuItem.Name = "CurrenciesToolStripMenuItem"
        Me.CurrenciesToolStripMenuItem.Size = New System.Drawing.Size(75, 20)
        Me.CurrenciesToolStripMenuItem.Text = "Currencies"
        '
        'AddNewCurrencyToolStripMenuItem
        '
        Me.AddNewCurrencyToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.add
        Me.AddNewCurrencyToolStripMenuItem.Name = "AddNewCurrencyToolStripMenuItem"
        Me.AddNewCurrencyToolStripMenuItem.Size = New System.Drawing.Size(174, 22)
        Me.AddNewCurrencyToolStripMenuItem.Text = "Add New Currency"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(171, 6)
        '
        'chart_button
        '
        Me.chart_button.Name = "chart_button"
        Me.chart_button.Size = New System.Drawing.Size(48, 20)
        Me.chart_button.Text = "Chart"
        '
        'ImportFromExcelToolStripMenuItem
        '
        Me.ImportFromExcelToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.Benjigarner_Softdimension_Excel
        Me.ImportFromExcelToolStripMenuItem.Name = "ImportFromExcelToolStripMenuItem"
        Me.ImportFromExcelToolStripMenuItem.Size = New System.Drawing.Size(129, 20)
        Me.ImportFromExcelToolStripMenuItem.Text = "Import from Excel"
        '
        'CurrenciesManagementUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(991, 602)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "CurrenciesManagementUI"
        Me.Text = "Currencies Management"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.VersionsRCMenu.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.dgvRCM.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.currenciesRCMenu.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents rates_DGV As VIBlend.WinForms.DataGridView.vDataGridView
    Friend WithEvents MenuButtonIL As System.Windows.Forms.ImageList
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents rates_chart As ZedGraph.ZedGraphControl
    Friend WithEvents ratesVersionsIL As System.Windows.Forms.ImageList
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents CurrenciesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddNewCurrencyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents rates_versions_button As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents currenciesRCMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AddCurrencyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteCurrencyToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VersionsRCMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AddRatesVersionRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddFolderRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteVersionRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents chart_button As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents rates_version_TB As System.Windows.Forms.TextBox
    Friend WithEvents versionsTV As System.Windows.Forms.TreeView
    Friend WithEvents ImportFromExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents dgvRCM As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents expand_periods As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents collapse_periods As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents select_version As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
End Class
