<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AxisView
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AxisView))
        Me.EntitiesIL = New System.Windows.Forms.ImageList(Me.components)
        Me.ButtonsIL = New System.Windows.Forms.ImageList(Me.components)
        Me.m_axisRightClickMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CreateAxisElemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteAxisElemToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.RenameAxisElemButton = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.CreateANewAxisElemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteAxisElemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SendEntitiesHierarchyToExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.m_axisRightClickMenu.SuspendLayout()
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
        'm_axisRightClickMenu
        '
        Me.m_axisRightClickMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CreateAxisElemToolStripMenuItem, Me.DeleteAxisElemToolStripMenuItem2, Me.ToolStripSeparator1, Me.RenameAxisElemButton, Me.ToolStripSeparator5, Me.copy_down_bt, Me.ToolStripSeparator4, Me.drop_to_excel_bt, Me.AutoResizeColumnsButton, Me.ExpandAllBT, Me.CollapseAllBT})
        Me.m_axisRightClickMenu.Name = "ContextMenuStripTGV"
        Me.m_axisRightClickMenu.Size = New System.Drawing.Size(182, 198)
        '
        'CreateAxisElemToolStripMenuItem
        '
        Me.CreateAxisElemToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.element_branch2_add
        Me.CreateAxisElemToolStripMenuItem.Name = "CreateAxisElemToolStripMenuItem"
        Me.CreateAxisElemToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.CreateAxisElemToolStripMenuItem.Text = "Create"
        '
        'DeleteAxisElemToolStripMenuItem2
        '
        Me.DeleteAxisElemToolStripMenuItem2.Image = Global.FinancialBI.My.Resources.Resources.element_branch2_delete
        Me.DeleteAxisElemToolStripMenuItem2.Name = "DeleteAxisElemToolStripMenuItem2"
        Me.DeleteAxisElemToolStripMenuItem2.Size = New System.Drawing.Size(181, 22)
        Me.DeleteAxisElemToolStripMenuItem2.Text = "Delete"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(178, 6)
        '
        'RenameAxisElemButton
        '
        Me.RenameAxisElemButton.Name = "RenameAxisElemButton"
        Me.RenameAxisElemButton.Size = New System.Drawing.Size(181, 22)
        Me.RenameAxisElemButton.Text = "Rename"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(178, 6)
        '
        'copy_down_bt
        '
        Me.copy_down_bt.Image = Global.FinancialBI.My.Resources.Resources.Download
        Me.copy_down_bt.Name = "copy_down_bt"
        Me.copy_down_bt.Size = New System.Drawing.Size(181, 22)
        Me.copy_down_bt.Text = "Copy down"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(178, 6)
        '
        'drop_to_excel_bt
        '
        Me.drop_to_excel_bt.Image = Global.FinancialBI.My.Resources.Resources.excel_blue2
        Me.drop_to_excel_bt.Name = "drop_to_excel_bt"
        Me.drop_to_excel_bt.Size = New System.Drawing.Size(181, 22)
        Me.drop_to_excel_bt.Text = "Drop on excel"
        '
        'AutoResizeColumnsButton
        '
        Me.AutoResizeColumnsButton.Name = "AutoResizeColumnsButton"
        Me.AutoResizeColumnsButton.Size = New System.Drawing.Size(181, 22)
        Me.AutoResizeColumnsButton.Text = "Auto resize columns"
        '
        'ExpandAllBT
        '
        Me.ExpandAllBT.Name = "ExpandAllBT"
        Me.ExpandAllBT.Size = New System.Drawing.Size(181, 22)
        Me.ExpandAllBT.Text = "Expand all"
        '
        'CollapseAllBT
        '
        Me.CollapseAllBT.Name = "CollapseAllBT"
        Me.CollapseAllBT.Size = New System.Drawing.Size(181, 22)
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
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CreateANewAxisElemToolStripMenuItem, Me.DeleteAxisElemToolStripMenuItem, Me.ToolStripSeparator2})
        Me.EditToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.element_branch23
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(73, 20)
        Me.EditToolStripMenuItem.Text = "Entities"
        '
        'CreateANewAxisElemToolStripMenuItem
        '
        Me.CreateANewAxisElemToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.plus
        Me.CreateANewAxisElemToolStripMenuItem.Name = "CreateANewAxisElemToolStripMenuItem"
        Me.CreateANewAxisElemToolStripMenuItem.Size = New System.Drawing.Size(108, 22)
        Me.CreateANewAxisElemToolStripMenuItem.Text = "Create"
        '
        'DeleteAxisElemToolStripMenuItem
        '
        Me.DeleteAxisElemToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.imageres_89
        Me.DeleteAxisElemToolStripMenuItem.Name = "DeleteAxisElemToolStripMenuItem"
        Me.DeleteAxisElemToolStripMenuItem.Size = New System.Drawing.Size(108, 22)
        Me.DeleteAxisElemToolStripMenuItem.Text = "Delete"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(105, 6)
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SendEntitiesHierarchyToExcelToolStripMenuItem})
        Me.ExcelToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.Excel_dark_24_24
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
        Me.ExcelToolStripMenuItem.Text = "Excel"
        '
        'SendEntitiesHierarchyToExcelToolStripMenuItem
        '
        Me.SendEntitiesHierarchyToExcelToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.excel_blue2
        Me.SendEntitiesHierarchyToExcelToolStripMenuItem.Name = "SendEntitiesHierarchyToExcelToolStripMenuItem"
        Me.SendEntitiesHierarchyToExcelToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.SendEntitiesHierarchyToExcelToolStripMenuItem.Text = "Drop on excel"
        '
        'AxisView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "AxisView"
        Me.Size = New System.Drawing.Size(516, 420)
        Me.m_axisRightClickMenu.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents EntitiesIL As System.Windows.Forms.ImageList
    Friend WithEvents ButtonsIL As System.Windows.Forms.ImageList
    Friend WithEvents m_axisRightClickMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents copy_down_bt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents drop_to_excel_bt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CreateAxisElemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteAxisElemToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CreateANewAxisElemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteAxisElemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SendEntitiesHierarchyToExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RenameAxisElemButton As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AutoResizeColumnsButton As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExpandAllBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CollapseAllBT As System.Windows.Forms.ToolStripMenuItem
    Protected WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel

End Class
