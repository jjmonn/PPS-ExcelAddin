<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ASEntitiesAttributesUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ASEntitiesAttributesUI))
        Dim DataGridLocalization1 As VIBlend.WinForms.DataGridView.DataGridLocalization = New VIBlend.WinForms.DataGridView.DataGridLocalization()
        Me.ButtonsIL = New System.Windows.Forms.ImageList(Me.components)
        Me.EditButtonsImagelist = New System.Windows.Forms.ImageList(Me.components)
        Me.RCM_TGV = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.copy_down_bt = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.drop_to_excel_bt = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.CreateEntityToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteEntityToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuButtonIL = New System.Windows.Forms.ImageList(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SendEntitiesHierarchyToExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.entitiesDGV = New VIBlend.WinForms.DataGridView.vDataGridView()
        Me.RCM_TGV.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonsIL
        '
        Me.ButtonsIL.ImageStream = CType(resources.GetObject("ButtonsIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonsIL.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonsIL.Images.SetKeyName(0, "submit 1 ok.ico")
        '
        'EditButtonsImagelist
        '
        Me.EditButtonsImagelist.ImageStream = CType(resources.GetObject("EditButtonsImagelist.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.EditButtonsImagelist.TransparentColor = System.Drawing.Color.Transparent
        Me.EditButtonsImagelist.Images.SetKeyName(0, "page_add.ico")
        Me.EditButtonsImagelist.Images.SetKeyName(1, "favicon(18).ico")
        Me.EditButtonsImagelist.Images.SetKeyName(2, "imageres_89.ico")
        Me.EditButtonsImagelist.Images.SetKeyName(3, "expand right.png")
        Me.EditButtonsImagelist.Images.SetKeyName(4, "expandleft.png")
        '
        'RCM_TGV
        '
        Me.RCM_TGV.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.copy_down_bt, Me.ToolStripSeparator5, Me.drop_to_excel_bt, Me.ToolStripSeparator4, Me.CreateEntityToolStripMenuItem, Me.DeleteEntityToolStripMenuItem2})
        Me.RCM_TGV.Name = "ContextMenuStripTGV"
        Me.RCM_TGV.Size = New System.Drawing.Size(169, 104)
        '
        'copy_down_bt
        '
        Me.copy_down_bt.Image = Global.PPS.My.Resources.Resources.Download_
        Me.copy_down_bt.Name = "copy_down_bt"
        Me.copy_down_bt.Size = New System.Drawing.Size(168, 22)
        Me.copy_down_bt.Text = "Copy Value Down"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(165, 6)
        '
        'drop_to_excel_bt
        '
        Me.drop_to_excel_bt.Image = Global.PPS.My.Resources.Resources.Benjigarner_Softdimension_Excel
        Me.drop_to_excel_bt.Name = "drop_to_excel_bt"
        Me.drop_to_excel_bt.Size = New System.Drawing.Size(168, 22)
        Me.drop_to_excel_bt.Text = "Drop To Excel"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(165, 6)
        '
        'CreateEntityToolStripMenuItem
        '
        Me.CreateEntityToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.checked
        Me.CreateEntityToolStripMenuItem.Name = "CreateEntityToolStripMenuItem"
        Me.CreateEntityToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.CreateEntityToolStripMenuItem.Text = "Create Entity"
        '
        'DeleteEntityToolStripMenuItem2
        '
        Me.DeleteEntityToolStripMenuItem2.Image = Global.PPS.My.Resources.Resources.imageres_89
        Me.DeleteEntityToolStripMenuItem2.Name = "DeleteEntityToolStripMenuItem2"
        Me.DeleteEntityToolStripMenuItem2.Size = New System.Drawing.Size(168, 22)
        Me.DeleteEntityToolStripMenuItem2.Text = "Delete Entity"
        '
        'MenuButtonIL
        '
        Me.MenuButtonIL.ImageStream = CType(resources.GetObject("MenuButtonIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.MenuButtonIL.TransparentColor = System.Drawing.Color.Transparent
        Me.MenuButtonIL.Images.SetKeyName(0, "expand right.png")
        Me.MenuButtonIL.Images.SetKeyName(1, "expandleft.png")
        Me.MenuButtonIL.Images.SetKeyName(2, "favicon(114).ico")
        Me.MenuButtonIL.Images.SetKeyName(3, "favicon(115).ico")
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcelToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(587, 24)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SendEntitiesHierarchyToExcelToolStripMenuItem})
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(45, 20)
        Me.ExcelToolStripMenuItem.Text = "Excel"
        '
        'SendEntitiesHierarchyToExcelToolStripMenuItem
        '
        Me.SendEntitiesHierarchyToExcelToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.Benjigarner_Softdimension_Excel
        Me.SendEntitiesHierarchyToExcelToolStripMenuItem.Name = "SendEntitiesHierarchyToExcelToolStripMenuItem"
        Me.SendEntitiesHierarchyToExcelToolStripMenuItem.Size = New System.Drawing.Size(238, 22)
        Me.SendEntitiesHierarchyToExcelToolStripMenuItem.Text = "Send Entities Hierarchy to Excel"
        '
        'entitiesDGV
        '
        Me.entitiesDGV.AllowAnimations = True
        Me.entitiesDGV.AllowCellMerge = True
        Me.entitiesDGV.AllowClipDrawing = True
        Me.entitiesDGV.AllowContextMenuColumnChooser = True
        Me.entitiesDGV.AllowContextMenuFiltering = True
        Me.entitiesDGV.AllowContextMenuGrouping = True
        Me.entitiesDGV.AllowContextMenuSorting = True
        Me.entitiesDGV.AllowCopyPaste = False
        Me.entitiesDGV.AllowDefaultContextMenu = True
        Me.entitiesDGV.AllowDragDropIndication = True
        Me.entitiesDGV.AllowDrop = True
        Me.entitiesDGV.AllowHeaderItemHighlightOnCellSelection = True
        Me.entitiesDGV.AutoUpdateOnListChanged = False
        Me.entitiesDGV.BackColor = System.Drawing.SystemColors.Control
        Me.entitiesDGV.BindingProgressEnabled = False
        Me.entitiesDGV.BindingProgressSampleRate = 20000
        Me.entitiesDGV.BorderColor = System.Drawing.SystemColors.Control
        Me.entitiesDGV.CellsArea.AllowCellMerge = True
        Me.entitiesDGV.CellsArea.ConditionalFormattingEnabled = False
        Me.entitiesDGV.ColumnsHierarchy.AllowDragDrop = False
        Me.entitiesDGV.ColumnsHierarchy.AllowResize = True
        Me.entitiesDGV.ColumnsHierarchy.AutoStretchColumns = False
        Me.entitiesDGV.ColumnsHierarchy.Fixed = False
        Me.entitiesDGV.ColumnsHierarchy.ShowExpandCollapseButtons = True
        Me.entitiesDGV.ContextMenuStrip = Me.RCM_TGV
        Me.entitiesDGV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.entitiesDGV.EnableColumnChooser = False
        Me.entitiesDGV.EnableResizeToolTip = True
        Me.entitiesDGV.EnableToolTips = True
        Me.entitiesDGV.FilterDisplayMode = VIBlend.WinForms.DataGridView.FilterDisplayMode.[Default]
        Me.entitiesDGV.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.entitiesDGV.GridLinesDashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.entitiesDGV.GridLinesDisplayMode = VIBlend.WinForms.DataGridView.GridLinesDisplayMode.DISPLAY_ALL
        Me.entitiesDGV.GroupingEnabled = False
        Me.entitiesDGV.HorizontalScroll = 0
        Me.entitiesDGV.HorizontalScrollBarLargeChange = 20
        Me.entitiesDGV.HorizontalScrollBarSmallChange = 5
        Me.entitiesDGV.ImageList = Nothing
        Me.entitiesDGV.Localization = DataGridLocalization1
        Me.entitiesDGV.Location = New System.Drawing.Point(0, 24)
        Me.entitiesDGV.MultipleSelectionEnabled = True
        Me.entitiesDGV.Name = "entitiesDGV"
        Me.entitiesDGV.PivotColumnsTotalsEnabled = False
        Me.entitiesDGV.PivotColumnsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH
        Me.entitiesDGV.PivotRowsTotalsEnabled = False
        Me.entitiesDGV.PivotRowsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH
        Me.entitiesDGV.RowsHierarchy.AllowDragDrop = False
        Me.entitiesDGV.RowsHierarchy.AllowResize = True
        Me.entitiesDGV.RowsHierarchy.CompactStyleRenderingEnabled = False
        Me.entitiesDGV.RowsHierarchy.CompactStyleRenderingItemsIndent = 15
        Me.entitiesDGV.RowsHierarchy.Fixed = False
        Me.entitiesDGV.RowsHierarchy.ShowExpandCollapseButtons = True
        Me.entitiesDGV.ScrollBarsEnabled = True
        Me.entitiesDGV.SelectionBorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.entitiesDGV.SelectionBorderEnabled = True
        Me.entitiesDGV.SelectionBorderWidth = 2
        Me.entitiesDGV.SelectionMode = VIBlend.WinForms.DataGridView.vDataGridView.SELECTION_MODE.CELL_SELECT
        Me.entitiesDGV.Size = New System.Drawing.Size(587, 492)
        Me.entitiesDGV.TabIndex = 4
        Me.entitiesDGV.Text = "VDataGridView1"
        Me.entitiesDGV.ToolTipDuration = 5000
        Me.entitiesDGV.ToolTipShowDelay = 1500
        Me.entitiesDGV.VerticalScroll = 0
        Me.entitiesDGV.VerticalScrollBarLargeChange = 20
        Me.entitiesDGV.VerticalScrollBarSmallChange = 5
        Me.entitiesDGV.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        Me.entitiesDGV.VirtualModeCellDefault = False
        '
        'ASEntitiesAttributesUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(587, 516)
        Me.Controls.Add(Me.entitiesDGV)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "ASEntitiesAttributesUI"
        Me.Text = "Entities Management"
        Me.RCM_TGV.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents EditButtonsImagelist As System.Windows.Forms.ImageList
    Friend WithEvents ButtonsIL As System.Windows.Forms.ImageList
    Friend WithEvents MenuButtonIL As System.Windows.Forms.ImageList
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents RCM_TGV As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SendEntitiesHierarchyToExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CreateEntityToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteEntityToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents copy_down_bt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents drop_to_excel_bt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents entitiesDGV As VIBlend.WinForms.DataGridView.vDataGridView
End Class
