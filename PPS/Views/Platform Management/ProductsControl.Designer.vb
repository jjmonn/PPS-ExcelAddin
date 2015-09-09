<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProductsControl
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RCM_TGV = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewProductMBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteProductMBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExcelDropMBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyDownRCMBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.DropToExcelRCMBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewProductRCMBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteProductRCMBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.RCM_TGV.SuspendLayout()
        Me.SuspendLayout()
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
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(556, 454)
        Me.TableLayoutPanel1.TabIndex = 8
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditToolStripMenuItem, Me.ExcelToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(556, 24)
        Me.MenuStrip1.TabIndex = 7
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcelDropMBT})
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(45, 20)
        Me.ExcelToolStripMenuItem.Text = "Excel"
        '
        'RCM_TGV
        '
        Me.RCM_TGV.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopyDownRCMBT, Me.ToolStripSeparator5, Me.DropToExcelRCMBT, Me.ToolStripSeparator4, Me.NewProductRCMBT, Me.DeleteProductRCMBT})
        Me.RCM_TGV.Name = "ContextMenuStripTGV"
        Me.RCM_TGV.Size = New System.Drawing.Size(169, 104)
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(165, 6)
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(165, 6)
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DoubleClickEnabled = True
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewProductMBT, Me.DeleteProductMBT, Me.ToolStripSeparator2})
        Me.EditToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.barcode
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(82, 20)
        Me.EditToolStripMenuItem.Text = "Products"
        '
        'NewProductMBT
        '
        Me.NewProductMBT.Image = Global.FinancialBI.My.Resources.Resources.element_branch2_add
        Me.NewProductMBT.Name = "NewProductMBT"
        Me.NewProductMBT.Size = New System.Drawing.Size(152, 22)
        Me.NewProductMBT.Text = "New Product"
        '
        'DeleteProductMBT
        '
        Me.DeleteProductMBT.Image = Global.FinancialBI.My.Resources.Resources.element_branch2_delete
        Me.DeleteProductMBT.Name = "DeleteProductMBT"
        Me.DeleteProductMBT.Size = New System.Drawing.Size(152, 22)
        Me.DeleteProductMBT.Text = "Delete Product"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(149, 6)
        '
        'ExcelDropMBT
        '
        Me.ExcelDropMBT.Image = Global.FinancialBI.My.Resources.Resources.favicon_17_1
        Me.ExcelDropMBT.Name = "ExcelDropMBT"
        Me.ExcelDropMBT.Size = New System.Drawing.Size(143, 22)
        Me.ExcelDropMBT.Text = "Send to Excel"
        '
        'CopyDownRCMBT
        '
        Me.CopyDownRCMBT.Image = Global.FinancialBI.My.Resources.Resources.arrow_down
        Me.CopyDownRCMBT.Name = "CopyDownRCMBT"
        Me.CopyDownRCMBT.Size = New System.Drawing.Size(168, 22)
        Me.CopyDownRCMBT.Text = "Copy Value Down"
        '
        'DropToExcelRCMBT
        '
        Me.DropToExcelRCMBT.Image = Global.FinancialBI.My.Resources.Resources.Excel_Blue_32x32
        Me.DropToExcelRCMBT.Name = "DropToExcelRCMBT"
        Me.DropToExcelRCMBT.Size = New System.Drawing.Size(168, 22)
        Me.DropToExcelRCMBT.Text = "Drop To Excel"
        '
        'NewProductRCMBT
        '
        Me.NewProductRCMBT.Image = Global.FinancialBI.My.Resources.Resources.barcode_add
        Me.NewProductRCMBT.Name = "NewProductRCMBT"
        Me.NewProductRCMBT.Size = New System.Drawing.Size(168, 22)
        Me.NewProductRCMBT.Text = "New Product"
        '
        'DeleteProductRCMBT
        '
        Me.DeleteProductRCMBT.Image = Global.FinancialBI.My.Resources.Resources.barcode_delete
        Me.DeleteProductRCMBT.Name = "DeleteProductRCMBT"
        Me.DeleteProductRCMBT.Size = New System.Drawing.Size(168, 22)
        Me.DeleteProductRCMBT.Text = "Delete Product"
        '
        'ProductsControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "ProductsControl"
        Me.Size = New System.Drawing.Size(556, 454)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.RCM_TGV.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewProductMBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteProductMBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExcelDropMBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RCM_TGV As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents CopyDownRCMBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DropToExcelRCMBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents NewProductRCMBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteProductRCMBT As System.Windows.Forms.ToolStripMenuItem

End Class
