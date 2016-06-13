<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SubmissionsFollowUpView
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SubmissionsFollowUpView))
        Me.m_submissionsDGV = New VIBlend.WinForms.DataGridView.vDataGridView()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.m_startDateLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_endDateLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_startDate = New VIBlend.WinForms.Controls.vDatePicker()
        Me.m_endDate = New VIBlend.WinForms.Controls.vDatePicker()
        Me.m_cellsRightClickMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.m_hierarchyRightClickMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CopyDownToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExpandAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CollapseAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.m_cellsRightClickMenu.SuspendLayout()
        Me.m_hierarchyRightClickMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'm_submissionsDGV
        '
        Me.m_submissionsDGV.AllowAnimations = True
        Me.m_submissionsDGV.AllowCellMerge = True
        Me.m_submissionsDGV.AllowClipDrawing = True
        Me.m_submissionsDGV.AllowContextMenuColumnChooser = True
        Me.m_submissionsDGV.AllowContextMenuFiltering = True
        Me.m_submissionsDGV.AllowContextMenuGrouping = True
        Me.m_submissionsDGV.AllowContextMenuSorting = True
        Me.m_submissionsDGV.AllowCopyPaste = False
        Me.m_submissionsDGV.AllowDefaultContextMenu = True
        Me.m_submissionsDGV.AllowDragDropIndication = True
        Me.m_submissionsDGV.AllowHeaderItemHighlightOnCellSelection = True
        Me.m_submissionsDGV.AutoUpdateOnListChanged = False
        Me.m_submissionsDGV.BackColor = System.Drawing.SystemColors.Control
        Me.m_submissionsDGV.BindingProgressEnabled = False
        Me.m_submissionsDGV.BindingProgressSampleRate = 20000
        Me.m_submissionsDGV.BorderColor = System.Drawing.Color.Empty
        Me.m_submissionsDGV.CellsArea.AllowCellMerge = True
        Me.m_submissionsDGV.CellsArea.ConditionalFormattingEnabled = False
        Me.m_submissionsDGV.ColumnsHierarchy.AllowDragDrop = False
        Me.m_submissionsDGV.ColumnsHierarchy.AllowResize = True
        Me.m_submissionsDGV.ColumnsHierarchy.AutoStretchColumns = False
        Me.m_submissionsDGV.ColumnsHierarchy.Fixed = False
        Me.m_submissionsDGV.ColumnsHierarchy.ShowExpandCollapseButtons = True
        Me.m_submissionsDGV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_submissionsDGV.EnableColumnChooser = False
        Me.m_submissionsDGV.EnableResizeToolTip = True
        Me.m_submissionsDGV.EnableToolTips = True
        Me.m_submissionsDGV.FilterDisplayMode = VIBlend.WinForms.DataGridView.FilterDisplayMode.[Default]
        Me.m_submissionsDGV.GridLinesDashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.m_submissionsDGV.GridLinesDisplayMode = VIBlend.WinForms.DataGridView.GridLinesDisplayMode.DISPLAY_ALL
        Me.m_submissionsDGV.GroupingEnabled = False
        Me.m_submissionsDGV.HorizontalScroll = 0
        Me.m_submissionsDGV.HorizontalScrollBarLargeChange = 20
        Me.m_submissionsDGV.HorizontalScrollBarSmallChange = 5
        Me.m_submissionsDGV.ImageList = Nothing
        Me.m_submissionsDGV.Localization = DataGridLocalization1
        Me.m_submissionsDGV.Location = New System.Drawing.Point(3, 35)
        Me.m_submissionsDGV.MultipleSelectionEnabled = True
        Me.m_submissionsDGV.Name = "m_submissionsDGV"
        Me.m_submissionsDGV.PivotColumnsTotalsEnabled = False
        Me.m_submissionsDGV.PivotColumnsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH
        Me.m_submissionsDGV.PivotRowsTotalsEnabled = False
        Me.m_submissionsDGV.PivotRowsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH
        Me.m_submissionsDGV.RowsHierarchy.AllowDragDrop = False
        Me.m_submissionsDGV.RowsHierarchy.AllowResize = True
        Me.m_submissionsDGV.RowsHierarchy.CompactStyleRenderingEnabled = False
        Me.m_submissionsDGV.RowsHierarchy.CompactStyleRenderingItemsIndent = 15
        Me.m_submissionsDGV.RowsHierarchy.Fixed = False
        Me.m_submissionsDGV.RowsHierarchy.ShowExpandCollapseButtons = True
        Me.m_submissionsDGV.ScrollBarsEnabled = True
        Me.m_submissionsDGV.SelectionBorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.m_submissionsDGV.SelectionBorderEnabled = True
        Me.m_submissionsDGV.SelectionBorderWidth = 2
        Me.m_submissionsDGV.SelectionMode = VIBlend.WinForms.DataGridView.vDataGridView.SELECTION_MODE.CELL_SELECT
        Me.m_submissionsDGV.Size = New System.Drawing.Size(890, 646)
        Me.m_submissionsDGV.TabIndex = 0
        Me.m_submissionsDGV.Text = "VDataGridView1"
        Me.m_submissionsDGV.ToolTipDuration = 5000
        Me.m_submissionsDGV.ToolTipShowDelay = 1500
        Me.m_submissionsDGV.VerticalScroll = 0
        Me.m_submissionsDGV.VerticalScrollBarLargeChange = 20
        Me.m_submissionsDGV.VerticalScrollBarSmallChange = 5
        Me.m_submissionsDGV.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        Me.m_submissionsDGV.VirtualModeCellDefault = False
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.m_submissionsDGV, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(896, 684)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.m_startDateLabel)
        Me.Panel1.Controls.Add(Me.m_endDateLabel)
        Me.Panel1.Controls.Add(Me.m_startDate)
        Me.Panel1.Controls.Add(Me.m_endDate)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(896, 32)
        Me.Panel1.TabIndex = 1
        '
        'm_startDateLabel
        '
        Me.m_startDateLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_startDateLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_startDateLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_startDateLabel.Ellipsis = False
        Me.m_startDateLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_startDateLabel.Location = New System.Drawing.Point(458, 3)
        Me.m_startDateLabel.Multiline = True
        Me.m_startDateLabel.Name = "m_startDateLabel"
        Me.m_startDateLabel.Size = New System.Drawing.Size(105, 26)
        Me.m_startDateLabel.TabIndex = 3
        Me.m_startDateLabel.Text = "Start date"
        Me.m_startDateLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.m_startDateLabel.UseMnemonics = True
        Me.m_startDateLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_endDateLabel
        '
        Me.m_endDateLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_endDateLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_endDateLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_endDateLabel.Ellipsis = False
        Me.m_endDateLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_endDateLabel.Location = New System.Drawing.Point(676, 3)
        Me.m_endDateLabel.Multiline = True
        Me.m_endDateLabel.Name = "m_endDateLabel"
        Me.m_endDateLabel.Size = New System.Drawing.Size(114, 26)
        Me.m_endDateLabel.TabIndex = 2
        Me.m_endDateLabel.Text = "End date"
        Me.m_endDateLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Me.m_endDateLabel.UseMnemonics = True
        Me.m_endDateLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_startDate
        '
        Me.m_startDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_startDate.BackColor = System.Drawing.Color.White
        Me.m_startDate.BorderColor = System.Drawing.Color.Black
        Me.m_startDate.Culture = New System.Globalization.CultureInfo("")
        Me.m_startDate.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.m_startDate.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.m_startDate.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.None
        Me.m_startDate.FormatValue = ""
        Me.m_startDate.Location = New System.Drawing.Point(567, 3)
        Me.m_startDate.MaxDate = New Date(2100, 1, 1, 0, 0, 0, 0)
        Me.m_startDate.MinDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.m_startDate.Name = "m_startDate"
        Me.m_startDate.ShowGrip = False
        Me.m_startDate.Size = New System.Drawing.Size(100, 26)
        Me.m_startDate.TabIndex = 1
        Me.m_startDate.Text = "VDatePicker1"
        Me.m_startDate.UseThemeBackColor = False
        Me.m_startDate.UseThemeDropDownArrowColor = True
        Me.m_startDate.Value = New Date(2016, 1, 4, 9, 28, 7, 919)
        Me.m_startDate.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_endDate
        '
        Me.m_endDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_endDate.BackColor = System.Drawing.Color.White
        Me.m_endDate.BorderColor = System.Drawing.Color.Black
        Me.m_endDate.Culture = New System.Globalization.CultureInfo("")
        Me.m_endDate.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.m_endDate.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.m_endDate.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.None
        Me.m_endDate.FormatValue = ""
        Me.m_endDate.Location = New System.Drawing.Point(793, 3)
        Me.m_endDate.MaxDate = New Date(2100, 1, 1, 0, 0, 0, 0)
        Me.m_endDate.MinDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.m_endDate.Name = "m_endDate"
        Me.m_endDate.ShowGrip = False
        Me.m_endDate.Size = New System.Drawing.Size(100, 26)
        Me.m_endDate.TabIndex = 0
        Me.m_endDate.Text = "VDatePicker1"
        Me.m_endDate.UseThemeBackColor = False
        Me.m_endDate.UseThemeDropDownArrowColor = True
        Me.m_endDate.Value = New Date(2016, 1, 4, 9, 27, 20, 177)
        Me.m_endDate.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_cellsRightClickMenu
        '
        Me.m_cellsRightClickMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopyDownToolStripMenuItem})
        Me.m_cellsRightClickMenu.Name = "m_cellsRightClickMenu"
        Me.m_cellsRightClickMenu.Size = New System.Drawing.Size(149, 28)
        '
        'm_hierarchyRightClickMenu
        '
        Me.m_hierarchyRightClickMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExpandAllToolStripMenuItem, Me.CollapseAllToolStripMenuItem})
        Me.m_hierarchyRightClickMenu.Name = "m_hierarchyRightClickMenu"
        Me.m_hierarchyRightClickMenu.Size = New System.Drawing.Size(153, 74)
        '
        'CopyDownToolStripMenuItem
        '
        Me.CopyDownToolStripMenuItem.Name = "CopyDownToolStripMenuItem"
        Me.CopyDownToolStripMenuItem.Size = New System.Drawing.Size(148, 24)
        Me.CopyDownToolStripMenuItem.Text = "Copy down"
        '
        'ExpandAllToolStripMenuItem
        '
        Me.ExpandAllToolStripMenuItem.Name = "ExpandAllToolStripMenuItem"
        Me.ExpandAllToolStripMenuItem.Size = New System.Drawing.Size(152, 24)
        Me.ExpandAllToolStripMenuItem.Text = "Expand all"
        '
        'CollapseAllToolStripMenuItem
        '
        Me.CollapseAllToolStripMenuItem.Name = "CollapseAllToolStripMenuItem"
        Me.CollapseAllToolStripMenuItem.Size = New System.Drawing.Size(152, 24)
        Me.CollapseAllToolStripMenuItem.Text = "Collapse all"
        '
        'SubmissionsFollowUpView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(896, 684)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "SubmissionsFollowUpView"
        Me.Text = "Submissions Tracking"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.m_cellsRightClickMenu.ResumeLayout(False)
        Me.m_hierarchyRightClickMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents m_submissionsDGV As VIBlend.WinForms.DataGridView.vDataGridView
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents m_startDateLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_endDateLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_startDate As VIBlend.WinForms.Controls.vDatePicker
    Friend WithEvents m_endDate As VIBlend.WinForms.Controls.vDatePicker
    Friend WithEvents m_cellsRightClickMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents m_hierarchyRightClickMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents CopyDownToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExpandAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CollapseAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
