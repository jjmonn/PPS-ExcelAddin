<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MarketPricesUI
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
        Dim DataGridLocalization1 As VIBlend.WinForms.DataGridView.DataGridLocalization = New VIBlend.WinForms.DataGridView.DataGridLocalization()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MarketPricesUI))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.indexesRCMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddIndexToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteIndexToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.versionsTV = New System.Windows.Forms.TreeView()
        Me.VersionsRCMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.select_version = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.AddVersionRCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddFolderRCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteVersionRCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.prices_DGV = New VIBlend.WinForms.DataGridView.vDataGridView()
        Me.dgvRCM = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.expand_periods = New System.Windows.Forms.ToolStripMenuItem()
        Me.collapse_periods = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.CopyPriceDown = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.index_version_TB = New System.Windows.Forms.TextBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.versions_button = New System.Windows.Forms.ToolStripMenuItem()
        Me.indexesBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddNewIndexToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.chart_button = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportFromExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.indexesRCMenu.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.VersionsRCMenu.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.dgvRCM.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.BackColor = System.Drawing.SystemColors.Control
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 27)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.ContextMenuStrip = Me.indexesRCMenu
        Me.SplitContainer1.Panel1.Controls.Add(Me.TableLayoutPanel2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Panel2.Margin = New System.Windows.Forms.Padding(3)
        Me.SplitContainer1.Size = New System.Drawing.Size(823, 492)
        Me.SplitContainer1.SplitterDistance = 125
        Me.SplitContainer1.SplitterWidth = 2
        Me.SplitContainer1.TabIndex = 1
        '
        'indexesRCMenu
        '
        Me.indexesRCMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddIndexToolStripMenuItem, Me.DeleteIndexToolStripMenuItem1})
        Me.indexesRCMenu.Name = "currenciesRCMenu"
        Me.indexesRCMenu.Size = New System.Drawing.Size(139, 48)
        '
        'AddIndexToolStripMenuItem
        '
        Me.AddIndexToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.checked
        Me.AddIndexToolStripMenuItem.Name = "AddIndexToolStripMenuItem"
        Me.AddIndexToolStripMenuItem.Size = New System.Drawing.Size(138, 22)
        Me.AddIndexToolStripMenuItem.Text = "Add Index"
        '
        'DeleteIndexToolStripMenuItem1
        '
        Me.DeleteIndexToolStripMenuItem1.Image = Global.PPS.My.Resources.Resources.imageres_891
        Me.DeleteIndexToolStripMenuItem1.Name = "DeleteIndexToolStripMenuItem1"
        Me.DeleteIndexToolStripMenuItem1.Size = New System.Drawing.Size(138, 22)
        Me.DeleteIndexToolStripMenuItem1.Text = "Delete Index"
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
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 465.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(119, 465)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'versionsTV
        '
        Me.versionsTV.ContextMenuStrip = Me.VersionsRCMenu
        Me.versionsTV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.versionsTV.Location = New System.Drawing.Point(3, 3)
        Me.versionsTV.Margin = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.versionsTV.Name = "versionsTV"
        Me.versionsTV.Size = New System.Drawing.Size(116, 459)
        Me.versionsTV.TabIndex = 0
        '
        'VersionsRCMenu
        '
        Me.VersionsRCMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.select_version, Me.ToolStripSeparator2, Me.AddVersionRCM, Me.AddFolderRCM, Me.DeleteVersionRCM})
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
        'AddVersionRCM
        '
        Me.AddVersionRCM.Image = Global.PPS.My.Resources.Resources.add21
        Me.AddVersionRCM.Name = "AddVersionRCM"
        Me.AddVersionRCM.Size = New System.Drawing.Size(147, 22)
        Me.AddVersionRCM.Text = "Add Version"
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
        'SplitContainer2
        '
        Me.SplitContainer2.BackColor = System.Drawing.SystemColors.Control
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
        Me.SplitContainer2.Panel2.Margin = New System.Windows.Forms.Padding(3)
        Me.SplitContainer2.Size = New System.Drawing.Size(696, 492)
        Me.SplitContainer2.SplitterDistance = 299
        Me.SplitContainer2.SplitterWidth = 1
        Me.SplitContainer2.TabIndex = 2
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.prices_DGV, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(696, 299)
        Me.TableLayoutPanel1.TabIndex = 3
        '
        'prices_DGV
        '
        Me.prices_DGV.AllowAnimations = True
        Me.prices_DGV.AllowCellMerge = True
        Me.prices_DGV.AllowClipDrawing = True
        Me.prices_DGV.AllowContextMenuColumnChooser = True
        Me.prices_DGV.AllowContextMenuFiltering = True
        Me.prices_DGV.AllowContextMenuGrouping = True
        Me.prices_DGV.AllowContextMenuSorting = True
        Me.prices_DGV.AllowCopyPaste = False
        Me.prices_DGV.AllowDefaultContextMenu = True
        Me.prices_DGV.AllowDragDropIndication = True
        Me.prices_DGV.AllowHeaderItemHighlightOnCellSelection = True
        Me.prices_DGV.AutoUpdateOnListChanged = False
        Me.prices_DGV.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.prices_DGV.BindingProgressEnabled = False
        Me.prices_DGV.BindingProgressSampleRate = 20000
        Me.prices_DGV.BorderColor = System.Drawing.Color.Empty
        Me.prices_DGV.CellsArea.AllowCellMerge = True
        Me.prices_DGV.CellsArea.ConditionalFormattingEnabled = False
        Me.prices_DGV.ColumnsHierarchy.AllowDragDrop = False
        Me.prices_DGV.ColumnsHierarchy.AllowResize = True
        Me.prices_DGV.ColumnsHierarchy.AutoStretchColumns = False
        Me.prices_DGV.ColumnsHierarchy.Fixed = False
        Me.prices_DGV.ColumnsHierarchy.ShowExpandCollapseButtons = True
        Me.prices_DGV.ContextMenuStrip = Me.dgvRCM
        Me.prices_DGV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.prices_DGV.EnableColumnChooser = False
        Me.prices_DGV.EnableResizeToolTip = True
        Me.prices_DGV.EnableToolTips = True
        Me.prices_DGV.FilterDisplayMode = VIBlend.WinForms.DataGridView.FilterDisplayMode.[Default]
        Me.prices_DGV.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.prices_DGV.GridLinesDashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.prices_DGV.GridLinesDisplayMode = VIBlend.WinForms.DataGridView.GridLinesDisplayMode.DISPLAY_ALL
        Me.prices_DGV.GroupingEnabled = False
        Me.prices_DGV.HorizontalScroll = 0
        Me.prices_DGV.HorizontalScrollBarLargeChange = 20
        Me.prices_DGV.HorizontalScrollBarSmallChange = 5
        Me.prices_DGV.ImageList = Nothing
        Me.prices_DGV.Localization = DataGridLocalization1
        Me.prices_DGV.Location = New System.Drawing.Point(3, 28)
        Me.prices_DGV.MultipleSelectionEnabled = True
        Me.prices_DGV.Name = "prices_DGV"
        Me.prices_DGV.PivotColumnsTotalsEnabled = False
        Me.prices_DGV.PivotColumnsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH
        Me.prices_DGV.PivotRowsTotalsEnabled = False
        Me.prices_DGV.PivotRowsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH
        Me.prices_DGV.RowsHierarchy.AllowDragDrop = False
        Me.prices_DGV.RowsHierarchy.AllowResize = True
        Me.prices_DGV.RowsHierarchy.CompactStyleRenderingEnabled = False
        Me.prices_DGV.RowsHierarchy.CompactStyleRenderingItemsIndent = 15
        Me.prices_DGV.RowsHierarchy.Fixed = False
        Me.prices_DGV.RowsHierarchy.ShowExpandCollapseButtons = True
        Me.prices_DGV.ScrollBarsEnabled = True
        Me.prices_DGV.SelectionBorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.prices_DGV.SelectionBorderEnabled = True
        Me.prices_DGV.SelectionBorderWidth = 2
        Me.prices_DGV.SelectionMode = VIBlend.WinForms.DataGridView.vDataGridView.SELECTION_MODE.CELL_SELECT
        Me.prices_DGV.ShowBorder = True
        Me.prices_DGV.Size = New System.Drawing.Size(690, 323)
        Me.prices_DGV.TabIndex = 1
        Me.prices_DGV.Text = "VDataGridView1"
        Me.prices_DGV.ToolTipDuration = 5000
        Me.prices_DGV.ToolTipShowDelay = 1500
        Me.prices_DGV.VerticalScroll = 0
        Me.prices_DGV.VerticalScrollBarLargeChange = 20
        Me.prices_DGV.VerticalScrollBarSmallChange = 5
        Me.prices_DGV.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        Me.prices_DGV.VirtualModeCellDefault = False
        '
        'dgvRCM
        '
        Me.dgvRCM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.expand_periods, Me.collapse_periods, Me.ToolStripSeparator3, Me.CopyPriceDown})
        Me.dgvRCM.Name = "dgvRCM"
        Me.dgvRCM.Size = New System.Drawing.Size(177, 76)
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
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(173, 6)
        '
        'CopyPriceDown
        '
        Me.CopyPriceDown.Image = Global.PPS.My.Resources.Resources.Download_
        Me.CopyPriceDown.Name = "CopyPriceDown"
        Me.CopyPriceDown.Size = New System.Drawing.Size(176, 22)
        Me.CopyPriceDown.Text = "Copy Value Down"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.BackColor = System.Drawing.SystemColors.Control
        Me.TableLayoutPanel3.ColumnCount = 3
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 143.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 589.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.Label2, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.index_version_TB, 1, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(1, 1)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(694, 23)
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
        'index_version_TB
        '
        Me.index_version_TB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.index_version_TB.Enabled = False
        Me.index_version_TB.Location = New System.Drawing.Point(103, 1)
        Me.index_version_TB.Margin = New System.Windows.Forms.Padding(1)
        Me.index_version_TB.Name = "index_version_TB"
        Me.index_version_TB.Size = New System.Drawing.Size(141, 20)
        Me.index_version_TB.TabIndex = 1
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.versions_button, Me.indexesBT, Me.chart_button, Me.ImportFromExcelToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(823, 24)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'versions_button
        '
        Me.versions_button.Name = "versions_button"
        Me.versions_button.Size = New System.Drawing.Size(63, 20)
        Me.versions_button.Text = "Versions"
        '
        'indexesBT
        '
        Me.indexesBT.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddNewIndexToolStripMenuItem, Me.ToolStripSeparator1})
        Me.indexesBT.Name = "indexesBT"
        Me.indexesBT.Size = New System.Drawing.Size(98, 20)
        Me.indexesBT.Text = "Market Indexes"
        '
        'AddNewIndexToolStripMenuItem
        '
        Me.AddNewIndexToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.add
        Me.AddNewIndexToolStripMenuItem.Name = "AddNewIndexToolStripMenuItem"
        Me.AddNewIndexToolStripMenuItem.Size = New System.Drawing.Size(154, 22)
        Me.AddNewIndexToolStripMenuItem.Text = "Add New Index"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(151, 6)
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
        'MarketPricesUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(823, 518)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "MarketPricesUI"
        Me.Text = "Market Prices"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.indexesRCMenu.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.VersionsRCMenu.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.dgvRCM.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents versionsTV As System.Windows.Forms.TreeView
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents prices_DGV As VIBlend.WinForms.DataGridView.vDataGridView
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents index_version_TB As System.Windows.Forms.TextBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents versions_button As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents indexesBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddNewIndexToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents chart_button As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImportFromExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents dgvRCM As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents expand_periods As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents collapse_periods As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CopyPriceDown As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents indexesRCMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AddIndexToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteIndexToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VersionsRCMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents select_version As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AddVersionRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddFolderRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteVersionRCM As System.Windows.Forms.ToolStripMenuItem
End Class
