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
        Me.AxisIL = New System.Windows.Forms.ImageList(Me.components)
        Me.ButtonsIL = New System.Windows.Forms.ImageList(Me.components)
        Me.RCM_TGV = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.copy_down_bt = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.drop_to_excel_bt = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.CreateAxisToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteAxisToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreateNewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteAxisToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SendEntitiesHierarchyToExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RCM_TGV.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'AxisIL
        '
        Me.AxisIL.ImageStream = CType(resources.GetObject("AxisIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.AxisIL.TransparentColor = System.Drawing.Color.Transparent
        Me.AxisIL.Images.SetKeyName(0, "element_branch2.ico")
        Me.AxisIL.Images.SetKeyName(1, "breakpoint.ico")
        '
        'ButtonsIL
        '
        Me.ButtonsIL.ImageStream = CType(resources.GetObject("ButtonsIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonsIL.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonsIL.Images.SetKeyName(0, "submit 1 ok.ico")
        '
        'RCM_TGV
        '
        Me.RCM_TGV.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CreateAxisToolStripMenuItem, Me.DeleteAxisToolStripMenuItem2, Me.ToolStripSeparator5, Me.copy_down_bt, Me.ToolStripSeparator4, Me.drop_to_excel_bt})
        Me.RCM_TGV.Name = "ContextMenuStripTGV"
        Me.RCM_TGV.Size = New System.Drawing.Size(188, 134)
        '
        'copy_down_bt
        '
        Me.copy_down_bt.Image = Global.FinancialBI.My.Resources.Resources.Download_
        Me.copy_down_bt.Name = "copy_down_bt"
        Me.copy_down_bt.Size = New System.Drawing.Size(187, 24)
        Me.copy_down_bt.Text = "Copy down"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(184, 6)
        '
        'drop_to_excel_bt
        '
        Me.drop_to_excel_bt.Image = Global.FinancialBI.My.Resources.Resources.Excel_Blue_32x32
        Me.drop_to_excel_bt.Name = "drop_to_excel_bt"
        Me.drop_to_excel_bt.Size = New System.Drawing.Size(187, 24)
        Me.drop_to_excel_bt.Text = "Drop_on_excel"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(184, 6)
        '
        'CreateAxisToolStripMenuItem
        '
        Me.CreateAxisToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.element_branch2_add
        Me.CreateAxisToolStripMenuItem.Name = "CreateAxisToolStripMenuItem"
        Me.CreateAxisToolStripMenuItem.Size = New System.Drawing.Size(187, 24)
        Me.CreateAxisToolStripMenuItem.Text = "General.create"
        '
        'DeleteAxisToolStripMenuItem2
        '
        Me.DeleteAxisToolStripMenuItem2.Image = Global.FinancialBI.My.Resources.Resources.element_branch2_delete
        Me.DeleteAxisToolStripMenuItem2.Name = "DeleteAxisToolStripMenuItem2"
        Me.DeleteAxisToolStripMenuItem2.Size = New System.Drawing.Size(187, 24)
        Me.DeleteAxisToolStripMenuItem2.Text = "Delete"
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
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CreateNewToolStripMenuItem, Me.DeleteAxisToolStripMenuItem, Me.ToolStripSeparator2})
        Me.EditToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.element_branch21
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(73, 20)
        Me.EditToolStripMenuItem.Text = "Menu"
        '
        'CreateNewToolStripMenuItem
        '
        Me.CreateNewToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.element_branch2_add
        Me.CreateNewToolStripMenuItem.Name = "CreateNewToolStripMenuItem"
        Me.CreateNewToolStripMenuItem.Size = New System.Drawing.Size(117, 24)
        Me.CreateNewToolStripMenuItem.Text = "Create"
        '
        'DeleteAxisToolStripMenuItem
        '
        Me.DeleteAxisToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.element_branch2_delete
        Me.DeleteAxisToolStripMenuItem.Name = "DeleteAxisToolStripMenuItem"
        Me.DeleteAxisToolStripMenuItem.Size = New System.Drawing.Size(117, 24)
        Me.DeleteAxisToolStripMenuItem.Text = "Delete"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(114, 6)
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SendEntitiesHierarchyToExcelToolStripMenuItem})
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(50, 20)
        Me.ExcelToolStripMenuItem.Text = "Excel"
        '
        'SendEntitiesHierarchyToExcelToolStripMenuItem
        '
        Me.SendEntitiesHierarchyToExcelToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.Excel_Blue_32x32
        Me.SendEntitiesHierarchyToExcelToolStripMenuItem.Name = "SendEntitiesHierarchyToExcelToolStripMenuItem"
        Me.SendEntitiesHierarchyToExcelToolStripMenuItem.Size = New System.Drawing.Size(268, 24)
        Me.SendEntitiesHierarchyToExcelToolStripMenuItem.Text = "Drop on excel"
        '
        'AxisControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "AxisControl"
        Me.Size = New System.Drawing.Size(516, 420)
        Me.RCM_TGV.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents AxisIL As System.Windows.Forms.ImageList
    Friend WithEvents ButtonsIL As System.Windows.Forms.ImageList
    Friend WithEvents RCM_TGV As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents copy_down_bt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents drop_to_excel_bt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CreateAxisToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteAxisToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CreateNewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteAxisToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SendEntitiesHierarchyToExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
