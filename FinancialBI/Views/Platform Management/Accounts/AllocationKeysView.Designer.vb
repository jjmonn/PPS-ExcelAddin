<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AllocationKeysView
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AllocationKeysView))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.m_allocationsKeysDGV = New VIBlend.WinForms.DataGridView.vDataGridView()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.m_allocationsKeysDGV, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(594, 529)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'm_allocationsKeysDGV
        '
        Me.m_allocationsKeysDGV.AllowAnimations = True
        Me.m_allocationsKeysDGV.AllowCellMerge = True
        Me.m_allocationsKeysDGV.AllowClipDrawing = True
        Me.m_allocationsKeysDGV.AllowContextMenuColumnChooser = True
        Me.m_allocationsKeysDGV.AllowContextMenuFiltering = True
        Me.m_allocationsKeysDGV.AllowContextMenuGrouping = True
        Me.m_allocationsKeysDGV.AllowContextMenuSorting = True
        Me.m_allocationsKeysDGV.AllowCopyPaste = False
        Me.m_allocationsKeysDGV.AllowDefaultContextMenu = True
        Me.m_allocationsKeysDGV.AllowDragDropIndication = True
        Me.m_allocationsKeysDGV.AllowHeaderItemHighlightOnCellSelection = True
        Me.m_allocationsKeysDGV.AutoUpdateOnListChanged = False
        Me.m_allocationsKeysDGV.BackColor = System.Drawing.Color.White
        Me.m_allocationsKeysDGV.BindingProgressEnabled = False
        Me.m_allocationsKeysDGV.BindingProgressSampleRate = 20000
        Me.m_allocationsKeysDGV.BorderColor = System.Drawing.Color.Empty
        Me.m_allocationsKeysDGV.CellsArea.AllowCellMerge = True
        Me.m_allocationsKeysDGV.CellsArea.ConditionalFormattingEnabled = False
        Me.m_allocationsKeysDGV.ColumnsHierarchy.AllowDragDrop = False
        Me.m_allocationsKeysDGV.ColumnsHierarchy.AllowResize = True
        Me.m_allocationsKeysDGV.ColumnsHierarchy.AutoStretchColumns = False
        Me.m_allocationsKeysDGV.ColumnsHierarchy.Fixed = False
        Me.m_allocationsKeysDGV.ColumnsHierarchy.ShowExpandCollapseButtons = True
        Me.m_allocationsKeysDGV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_allocationsKeysDGV.EnableColumnChooser = False
        Me.m_allocationsKeysDGV.EnableResizeToolTip = True
        Me.m_allocationsKeysDGV.EnableToolTips = True
        Me.m_allocationsKeysDGV.FilterDisplayMode = VIBlend.WinForms.DataGridView.FilterDisplayMode.[Default]
        Me.m_allocationsKeysDGV.GridLinesDashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.m_allocationsKeysDGV.GridLinesDisplayMode = VIBlend.WinForms.DataGridView.GridLinesDisplayMode.DISPLAY_ALL
        Me.m_allocationsKeysDGV.GroupingEnabled = False
        Me.m_allocationsKeysDGV.HorizontalScroll = 0
        Me.m_allocationsKeysDGV.HorizontalScrollBarLargeChange = 20
        Me.m_allocationsKeysDGV.HorizontalScrollBarSmallChange = 5
        Me.m_allocationsKeysDGV.ImageList = Nothing
        Me.m_allocationsKeysDGV.Localization = DataGridLocalization1
        Me.m_allocationsKeysDGV.Location = New System.Drawing.Point(3, 28)
        Me.m_allocationsKeysDGV.MultipleSelectionEnabled = True
        Me.m_allocationsKeysDGV.Name = "m_allocationsKeysDGV"
        Me.m_allocationsKeysDGV.PivotColumnsTotalsEnabled = False
        Me.m_allocationsKeysDGV.PivotColumnsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH
        Me.m_allocationsKeysDGV.PivotRowsTotalsEnabled = False
        Me.m_allocationsKeysDGV.PivotRowsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH
        Me.m_allocationsKeysDGV.RowsHierarchy.AllowDragDrop = False
        Me.m_allocationsKeysDGV.RowsHierarchy.AllowResize = True
        Me.m_allocationsKeysDGV.RowsHierarchy.CompactStyleRenderingEnabled = False
        Me.m_allocationsKeysDGV.RowsHierarchy.CompactStyleRenderingItemsIndent = 15
        Me.m_allocationsKeysDGV.RowsHierarchy.Fixed = False
        Me.m_allocationsKeysDGV.RowsHierarchy.ShowExpandCollapseButtons = True
        Me.m_allocationsKeysDGV.ScrollBarsEnabled = True
        Me.m_allocationsKeysDGV.SelectionBorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.m_allocationsKeysDGV.SelectionBorderEnabled = True
        Me.m_allocationsKeysDGV.SelectionBorderWidth = 2
        Me.m_allocationsKeysDGV.SelectionMode = VIBlend.WinForms.DataGridView.vDataGridView.SELECTION_MODE.CELL_SELECT
        Me.m_allocationsKeysDGV.Size = New System.Drawing.Size(588, 498)
        Me.m_allocationsKeysDGV.TabIndex = 0
        Me.m_allocationsKeysDGV.Text = "VDataGridView1"
        Me.m_allocationsKeysDGV.ToolTipDuration = 5000
        Me.m_allocationsKeysDGV.ToolTipShowDelay = 1500
        Me.m_allocationsKeysDGV.VerticalScroll = 0
        Me.m_allocationsKeysDGV.VerticalScrollBarLargeChange = 20
        Me.m_allocationsKeysDGV.VerticalScrollBarSmallChange = 5
        Me.m_allocationsKeysDGV.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        Me.m_allocationsKeysDGV.VirtualModeCellDefault = False
        '
        'AllocationKeysView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(594, 529)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "AllocationKeysView"
        Me.Text = "Allocation Keys"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents m_allocationsKeysDGV As VIBlend.WinForms.DataGridView.vDataGridView
End Class
