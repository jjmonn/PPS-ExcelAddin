<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PDCPlanningUI
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
        Dim DataGridLocalization1 As VIBlend.WinForms.DataGridView.DataGridLocalization = New VIBlend.WinForms.DataGridView.DataGridLocalization()
        Dim PivotDesignPanelLocalization1 As VIBlend.WinForms.DataGridView.PivotDesignPanelLocalization = New VIBlend.WinForms.DataGridView.PivotDesignPanelLocalization()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PDCPlanningUI))
        Me.m_PDCDataGridView = New VIBlend.WinForms.DataGridView.vDataGridView()
        Me.m_TabPages = New VIBlend.WinForms.Controls.vTabControl()
        Me.m_PDCTab = New VIBlend.WinForms.Controls.vTabPage()
        Me.m_PDCDataGridViewPivotDesign = New VIBlend.WinForms.DataGridView.vDataGridViewPivotDesign()
        Me.m_PDCSplitContainer = New VIBlend.WinForms.Controls.vSplitContainer()
        Me.m_TabPages.SuspendLayout()
        Me.m_PDCTab.SuspendLayout()
        Me.m_PDCSplitContainer.Panel1.SuspendLayout()
        Me.m_PDCSplitContainer.Panel2.SuspendLayout()
        Me.m_PDCSplitContainer.SuspendLayout()
        Me.SuspendLayout()
        '
        'm_PDCDataGridView
        '
        Me.m_PDCDataGridView.AllowAnimations = True
        Me.m_PDCDataGridView.AllowCellMerge = True
        Me.m_PDCDataGridView.AllowClipDrawing = True
        Me.m_PDCDataGridView.AllowContextMenuColumnChooser = True
        Me.m_PDCDataGridView.AllowContextMenuFiltering = True
        Me.m_PDCDataGridView.AllowContextMenuGrouping = True
        Me.m_PDCDataGridView.AllowContextMenuSorting = True
        Me.m_PDCDataGridView.AllowCopyPaste = False
        Me.m_PDCDataGridView.AllowDefaultContextMenu = True
        Me.m_PDCDataGridView.AllowDragDropIndication = True
        Me.m_PDCDataGridView.AllowHeaderItemHighlightOnCellSelection = True
        Me.m_PDCDataGridView.AutoUpdateOnListChanged = False
        Me.m_PDCDataGridView.BackColor = System.Drawing.SystemColors.Control
        Me.m_PDCDataGridView.BindingProgressEnabled = False
        Me.m_PDCDataGridView.BindingProgressSampleRate = 20000
        Me.m_PDCDataGridView.BorderColor = System.Drawing.Color.Empty
        Me.m_PDCDataGridView.CellsArea.AllowCellMerge = True
        Me.m_PDCDataGridView.CellsArea.ConditionalFormattingEnabled = False
        Me.m_PDCDataGridView.ColumnsHierarchy.AllowDragDrop = False
        Me.m_PDCDataGridView.ColumnsHierarchy.AllowResize = True
        Me.m_PDCDataGridView.ColumnsHierarchy.AutoStretchColumns = False
        Me.m_PDCDataGridView.ColumnsHierarchy.Fixed = False
        Me.m_PDCDataGridView.ColumnsHierarchy.ShowExpandCollapseButtons = True
        Me.m_PDCDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_PDCDataGridView.EnableColumnChooser = False
        Me.m_PDCDataGridView.EnableResizeToolTip = True
        Me.m_PDCDataGridView.EnableToolTips = True
        Me.m_PDCDataGridView.FilterDisplayMode = VIBlend.WinForms.DataGridView.FilterDisplayMode.[Default]
        Me.m_PDCDataGridView.GridLinesDashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.m_PDCDataGridView.GridLinesDisplayMode = VIBlend.WinForms.DataGridView.GridLinesDisplayMode.DISPLAY_ALL
        Me.m_PDCDataGridView.GroupingEnabled = False
        Me.m_PDCDataGridView.HorizontalScroll = 0
        Me.m_PDCDataGridView.HorizontalScrollBarLargeChange = 20
        Me.m_PDCDataGridView.HorizontalScrollBarSmallChange = 5
        Me.m_PDCDataGridView.ImageList = Nothing
        Me.m_PDCDataGridView.Localization = DataGridLocalization1
        Me.m_PDCDataGridView.Location = New System.Drawing.Point(0, 0)
        Me.m_PDCDataGridView.MultipleSelectionEnabled = True
        Me.m_PDCDataGridView.Name = "m_PDCDataGridView"
        Me.m_PDCDataGridView.PivotColumnsTotalsEnabled = False
        Me.m_PDCDataGridView.PivotColumnsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH
        Me.m_PDCDataGridView.PivotRowsTotalsEnabled = False
        Me.m_PDCDataGridView.PivotRowsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH
        Me.m_PDCDataGridView.RowsHierarchy.AllowDragDrop = False
        Me.m_PDCDataGridView.RowsHierarchy.AllowResize = True
        Me.m_PDCDataGridView.RowsHierarchy.CompactStyleRenderingEnabled = False
        Me.m_PDCDataGridView.RowsHierarchy.CompactStyleRenderingItemsIndent = 15
        Me.m_PDCDataGridView.RowsHierarchy.Fixed = False
        Me.m_PDCDataGridView.RowsHierarchy.ShowExpandCollapseButtons = True
        Me.m_PDCDataGridView.ScrollBarsEnabled = True
        Me.m_PDCDataGridView.SelectionBorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.m_PDCDataGridView.SelectionBorderEnabled = True
        Me.m_PDCDataGridView.SelectionBorderWidth = 2
        Me.m_PDCDataGridView.SelectionMode = VIBlend.WinForms.DataGridView.vDataGridView.SELECTION_MODE.CELL_SELECT
        Me.m_PDCDataGridView.Size = New System.Drawing.Size(343, 476)
        Me.m_PDCDataGridView.TabIndex = 0
        Me.m_PDCDataGridView.Text = "VDataGridView1"
        Me.m_PDCDataGridView.ToolTipDuration = 5000
        Me.m_PDCDataGridView.ToolTipShowDelay = 1500
        Me.m_PDCDataGridView.VerticalScroll = 0
        Me.m_PDCDataGridView.VerticalScrollBarLargeChange = 20
        Me.m_PDCDataGridView.VerticalScrollBarSmallChange = 5
        Me.m_PDCDataGridView.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        Me.m_PDCDataGridView.VirtualModeCellDefault = False
        '
        'm_TabPages
        '
        Me.m_TabPages.AllowAnimations = True
        Me.m_TabPages.Controls.Add(Me.m_PDCTab)
        Me.m_TabPages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_TabPages.Location = New System.Drawing.Point(0, 0)
        Me.m_TabPages.Name = "m_TabPages"
        Me.m_TabPages.Padding = New System.Windows.Forms.Padding(0, 30, 0, 0)
        Me.m_TabPages.Size = New System.Drawing.Size(699, 514)
        Me.m_TabPages.TabAlignment = VIBlend.WinForms.Controls.vTabPageAlignment.Top
        Me.m_TabPages.TabIndex = 1
        Me.m_TabPages.TabPages.Add(Me.m_PDCTab)
        Me.m_TabPages.TabsAreaBackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.m_TabPages.TabsInitialOffset = 15
        Me.m_TabPages.TitleHeight = 30
        Me.m_TabPages.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_PDCTab
        '
        Me.m_PDCTab.Controls.Add(Me.m_PDCSplitContainer)
        Me.m_PDCTab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_PDCTab.Location = New System.Drawing.Point(0, 30)
        Me.m_PDCTab.Name = "m_PDCTab"
        Me.m_PDCTab.Padding = New System.Windows.Forms.Padding(0)
        Me.m_PDCTab.Size = New System.Drawing.Size(699, 484)
        Me.m_PDCTab.TabIndex = 3
        Me.m_PDCTab.Text = "PDC"
        Me.m_PDCTab.TooltipText = "TabPage"
        Me.m_PDCTab.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        Me.m_PDCTab.Visible = False
        '
        'm_PDCDataGridViewPivotDesign
        '
        Me.m_PDCDataGridViewPivotDesign.AreasOrientation = System.Windows.Forms.Orientation.Horizontal
        Me.m_PDCDataGridViewPivotDesign.DataGridView = Nothing
        Me.m_PDCDataGridViewPivotDesign.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_PDCDataGridViewPivotDesign.Localization = PivotDesignPanelLocalization1
        Me.m_PDCDataGridViewPivotDesign.Location = New System.Drawing.Point(0, 0)
        Me.m_PDCDataGridViewPivotDesign.Name = "m_PDCDataGridViewPivotDesign"
        Me.m_PDCDataGridViewPivotDesign.Size = New System.Drawing.Size(344, 476)
        Me.m_PDCDataGridViewPivotDesign.TabIndex = 1
        Me.m_PDCDataGridViewPivotDesign.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_PDCSplitContainer
        '
        Me.m_PDCSplitContainer.AllowAnimations = True
        Me.m_PDCSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_PDCSplitContainer.Location = New System.Drawing.Point(4, 4)
        Me.m_PDCSplitContainer.Name = "m_PDCSplitContainer"
        Me.m_PDCSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'm_PDCSplitContainer.Panel1
        '
        Me.m_PDCSplitContainer.Panel1.BackColor = System.Drawing.Color.White
        Me.m_PDCSplitContainer.Panel1.BorderColor = System.Drawing.Color.Silver
        Me.m_PDCSplitContainer.Panel1.Controls.Add(Me.m_PDCDataGridView)
        Me.m_PDCSplitContainer.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.m_PDCSplitContainer.Panel1.Name = "Panel1"
        Me.m_PDCSplitContainer.Panel1.Size = New System.Drawing.Size(343, 476)
        Me.m_PDCSplitContainer.Panel1.TabIndex = 1
        '
        'm_PDCSplitContainer.Panel2
        '
        Me.m_PDCSplitContainer.Panel2.BackColor = System.Drawing.Color.White
        Me.m_PDCSplitContainer.Panel2.BorderColor = System.Drawing.Color.Silver
        Me.m_PDCSplitContainer.Panel2.Controls.Add(Me.m_PDCDataGridViewPivotDesign)
        Me.m_PDCSplitContainer.Panel2.Location = New System.Drawing.Point(347, 0)
        Me.m_PDCSplitContainer.Panel2.Name = "Panel2"
        Me.m_PDCSplitContainer.Panel2.Size = New System.Drawing.Size(344, 476)
        Me.m_PDCSplitContainer.Panel2.TabIndex = 2
        Me.m_PDCSplitContainer.Size = New System.Drawing.Size(691, 476)
        Me.m_PDCSplitContainer.SplitterSize = 4
        Me.m_PDCSplitContainer.StyleKey = "Splitter"
        Me.m_PDCSplitContainer.TabIndex = 2
        Me.m_PDCSplitContainer.Text = "VSplitContainer1"
        Me.m_PDCSplitContainer.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        '
        'PDCPlanningUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(699, 514)
        Me.Controls.Add(Me.m_TabPages)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "PDCPlanningUI"
        Me.Text = "Consolidation Plan de charge"
        Me.m_TabPages.ResumeLayout(False)
        Me.m_PDCTab.ResumeLayout(False)
        Me.m_PDCSplitContainer.Panel1.ResumeLayout(False)
        Me.m_PDCSplitContainer.Panel2.ResumeLayout(False)
        Me.m_PDCSplitContainer.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents m_PDCDataGridView As VIBlend.WinForms.DataGridView.vDataGridView
    Friend WithEvents m_TabPages As VIBlend.WinForms.Controls.vTabControl
    Friend WithEvents m_PDCTab As VIBlend.WinForms.Controls.vTabPage
    Friend WithEvents m_PDCSplitContainer As VIBlend.WinForms.Controls.vSplitContainer
    Friend WithEvents m_PDCDataGridViewPivotDesign As VIBlend.WinForms.DataGridView.vDataGridViewPivotDesign
End Class
