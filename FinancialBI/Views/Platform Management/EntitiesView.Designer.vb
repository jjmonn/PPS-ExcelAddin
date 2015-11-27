<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EntitiesView
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EntitiesView))
        Me.EntitiesIL = New System.Windows.Forms.ImageList(Me.components)
        Me.ButtonsIL = New System.Windows.Forms.ImageList(Me.components)
        Me.m_entitiesRightClickMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RenameEntityButton = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.CreateEntityToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteEntityToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.copy_down_bt = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.drop_to_excel_bt = New System.Windows.Forms.ToolStripMenuItem()
        Me.AutoResizeColumnsButton = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExpandAllBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.CollapseAllBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreateANewEntityToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteEntityToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SendEntitiesHierarchyToExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.m_entitiesRightClickMenu.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'EntitiesIL
        '
        Me.EntitiesIL.ImageStream = CType(resources.GetObject("EntitiesIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.EntitiesIL.TransparentColor = System.Drawing.Color.Transparent
        Me.EntitiesIL.Images.SetKeyName(0, "elements_branch.ico")
        Me.EntitiesIL.Images.SetKeyName(1, "favicon(81).ico")
        '
        'ButtonsIL
        '
        Me.ButtonsIL.ImageStream = CType(resources.GetObject("ButtonsIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonsIL.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonsIL.Images.SetKeyName(0, "submit 1 ok.ico")
        '
        'm_entitiesRightClickMenu
        '
        Me.m_entitiesRightClickMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CreateEntityToolStripMenuItem, Me.DeleteEntityToolStripMenuItem2, Me.ToolStripSeparator1, Me.RenameEntityButton, Me.ToolStripSeparator5, Me.copy_down_bt, Me.ToolStripSeparator4, Me.drop_to_excel_bt, Me.AutoResizeColumnsButton, Me.ExpandAllBT, Me.CollapseAllBT})
        Me.m_entitiesRightClickMenu.Name = "ContextMenuStripTGV"
        Me.m_entitiesRightClickMenu.Size = New System.Drawing.Size(202, 236)
        '
        'RenameEntityButton
        '
        Me.RenameEntityButton.Name = "RenameEntityButton"
        Me.RenameEntityButton.Size = New System.Drawing.Size(201, 24)
        Me.RenameEntityButton.Text = "Rename"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(198, 6)
        '
        'CreateEntityToolStripMenuItem
        '
        Me.CreateEntityToolStripMenuItem.Image = Global.FinancialBI.My.resources.element_branch2_add
        Me.CreateEntityToolStripMenuItem.Name = "CreateEntityToolStripMenuItem"
        Me.CreateEntityToolStripMenuItem.Size = New System.Drawing.Size(201, 24)
        Me.CreateEntityToolStripMenuItem.Text = "Create"
        '
        'DeleteEntityToolStripMenuItem2
        '
        Me.DeleteEntityToolStripMenuItem2.Image = Global.FinancialBI.My.resources.element_branch2_delete
        Me.DeleteEntityToolStripMenuItem2.Name = "DeleteEntityToolStripMenuItem2"
        Me.DeleteEntityToolStripMenuItem2.Size = New System.Drawing.Size(201, 24)
        Me.DeleteEntityToolStripMenuItem2.Text = "Delete"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(198, 6)
        '
        'copy_down_bt
        '
        Me.copy_down_bt.Image = Global.FinancialBI.My.Resources.Download
        Me.copy_down_bt.Name = "copy_down_bt"
        Me.copy_down_bt.Size = New System.Drawing.Size(201, 24)
        Me.copy_down_bt.Text = "Copy down"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(198, 6)
        '
        'drop_to_excel_bt
        '
        Me.drop_to_excel_bt.Image = Global.FinancialBI.My.Resources.excel_blue2
        Me.drop_to_excel_bt.Name = "drop_to_excel_bt"
        Me.drop_to_excel_bt.Size = New System.Drawing.Size(201, 24)
        Me.drop_to_excel_bt.Text = "Drop on excel"
        '
        'AutoResizeColumnsButton
        '
        Me.AutoResizeColumnsButton.Name = "AutoResizeColumnsButton"
        Me.AutoResizeColumnsButton.Size = New System.Drawing.Size(201, 24)
        Me.AutoResizeColumnsButton.Text = "Auto resize columns"
        '
        'ExpandAllBT
        '
        Me.ExpandAllBT.Name = "ExpandAllBT"
        Me.ExpandAllBT.Size = New System.Drawing.Size(201, 24)
        Me.ExpandAllBT.Text = "Expand all"
        '
        'CollapseAllBT
        '
        Me.CollapseAllBT.Name = "CollapseAllBT"
        Me.CollapseAllBT.Size = New System.Drawing.Size(201, 24)
        Me.CollapseAllBT.Text = "Collapse all"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.MenuStrip1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(516, 420)
        Me.TableLayoutPanel1.TabIndex = 7
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditToolStripMenuItem, Me.ExcelToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(516, 24)
        Me.MenuStrip1.TabIndex = 7
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CreateANewEntityToolStripMenuItem, Me.DeleteEntityToolStripMenuItem, Me.ToolStripSeparator2})
        Me.EditToolStripMenuItem.Image = Global.FinancialBI.My.Resources.element_branch23
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(81, 20)
        Me.EditToolStripMenuItem.Text = "Entities"
        '
        'CreateANewEntityToolStripMenuItem
        '
        Me.CreateANewEntityToolStripMenuItem.Image = Global.FinancialBI.My.Resources.plus
        Me.CreateANewEntityToolStripMenuItem.Name = "CreateANewEntityToolStripMenuItem"
        Me.CreateANewEntityToolStripMenuItem.Size = New System.Drawing.Size(118, 24)
        Me.CreateANewEntityToolStripMenuItem.Text = "Create"
        '
        'DeleteEntityToolStripMenuItem
        '
        Me.DeleteEntityToolStripMenuItem.Image = Global.FinancialBI.My.Resources.imageres_89
        Me.DeleteEntityToolStripMenuItem.Name = "DeleteEntityToolStripMenuItem"
        Me.DeleteEntityToolStripMenuItem.Size = New System.Drawing.Size(118, 24)
        Me.DeleteEntityToolStripMenuItem.Text = "Delete"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(115, 6)
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SendEntitiesHierarchyToExcelToolStripMenuItem})
        Me.ExcelToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Excel_dark_24_24
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(66, 20)
        Me.ExcelToolStripMenuItem.Text = "Excel"
        '
        'SendEntitiesHierarchyToExcelToolStripMenuItem
        '
        Me.SendEntitiesHierarchyToExcelToolStripMenuItem.Image = Global.FinancialBI.My.Resources.excel_blue2
        Me.SendEntitiesHierarchyToExcelToolStripMenuItem.Name = "SendEntitiesHierarchyToExcelToolStripMenuItem"
        Me.SendEntitiesHierarchyToExcelToolStripMenuItem.Size = New System.Drawing.Size(162, 24)
        Me.SendEntitiesHierarchyToExcelToolStripMenuItem.Text = "Drop on excel"
        '
        'EntitiesView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "EntitiesView"
        Me.Size = New System.Drawing.Size(516, 420)
        Me.m_entitiesRightClickMenu.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents EntitiesIL As System.Windows.Forms.ImageList
    Friend WithEvents ButtonsIL As System.Windows.Forms.ImageList
    Friend WithEvents m_entitiesRightClickMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents copy_down_bt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents drop_to_excel_bt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CreateEntityToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteEntityToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CreateANewEntityToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteEntityToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SendEntitiesHierarchyToExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RenameEntityButton As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AutoResizeColumnsButton As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExpandAllBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CollapseAllBT As System.Windows.Forms.ToolStripMenuItem

End Class
