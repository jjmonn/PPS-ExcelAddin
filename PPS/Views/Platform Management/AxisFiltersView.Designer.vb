﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AxisFiltersView
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AxisFiltersView))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.CategoriesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewCategoryMenuBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddValueMenuBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteMenuBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.RenameMenuBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.RCM_TV = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CreateCategoryRCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddValueRCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.DeleteRCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.RenameRCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExpandAllBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.CollapseAllBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.RCM_TV.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(525, 430)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "filter_and_sort.ico")
        Me.ImageList1.Images.SetKeyName(1, "config circle orangev small.png")
        Me.ImageList1.Images.SetKeyName(2, "favicon(81).ico")
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CategoriesToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(525, 24)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'CategoriesToolStripMenuItem
        '
        Me.CategoriesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewCategoryMenuBT, Me.AddValueMenuBT, Me.DeleteMenuBT, Me.RenameMenuBT, Me.ToolStripSeparator3})
        Me.CategoriesToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.elements
        Me.CategoriesToolStripMenuItem.Name = "CategoriesToolStripMenuItem"
        Me.CategoriesToolStripMenuItem.Size = New System.Drawing.Size(91, 20)
        Me.CategoriesToolStripMenuItem.Text = "Categories"
        '
        'NewCategoryMenuBT
        '
        Me.NewCategoryMenuBT.Image = Global.FinancialBI.My.Resources.Resources.elements_add
        Me.NewCategoryMenuBT.Name = "NewCategoryMenuBT"
        Me.NewCategoryMenuBT.Size = New System.Drawing.Size(149, 22)
        Me.NewCategoryMenuBT.Text = "New Category"
        '
        'AddValueMenuBT
        '
        Me.AddValueMenuBT.Name = "AddValueMenuBT"
        Me.AddValueMenuBT.Size = New System.Drawing.Size(149, 22)
        Me.AddValueMenuBT.Text = "New Value"
        '
        'DeleteMenuBT
        '
        Me.DeleteMenuBT.Image = Global.FinancialBI.My.Resources.Resources.elements_delete
        Me.DeleteMenuBT.Name = "DeleteMenuBT"
        Me.DeleteMenuBT.Size = New System.Drawing.Size(149, 22)
        Me.DeleteMenuBT.Text = "Delete"
        '
        'RenameMenuBT
        '
        Me.RenameMenuBT.Name = "RenameMenuBT"
        Me.RenameMenuBT.Size = New System.Drawing.Size(149, 22)
        Me.RenameMenuBT.Text = "Rename"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(146, 6)
        '
        'RCM_TV
        '
        Me.RCM_TV.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CreateCategoryRCM, Me.AddValueRCM, Me.ToolStripSeparator2, Me.DeleteRCM, Me.ToolStripSeparator1, Me.RenameRCM, Me.ExpandAllBT, Me.CollapseAllBT})
        Me.RCM_TV.Name = "RCM_TV"
        Me.RCM_TV.Size = New System.Drawing.Size(160, 170)
        '
        'CreateCategoryRCM
        '
        Me.CreateCategoryRCM.Image = Global.FinancialBI.My.Resources.Resources.folder_open_add
        Me.CreateCategoryRCM.Name = "CreateCategoryRCM"
        Me.CreateCategoryRCM.Size = New System.Drawing.Size(159, 22)
        Me.CreateCategoryRCM.Text = "Create Category"
        '
        'AddValueRCM
        '
        Me.AddValueRCM.Image = Global.FinancialBI.My.Resources.Resources.elements_add
        Me.AddValueRCM.Name = "AddValueRCM"
        Me.AddValueRCM.Size = New System.Drawing.Size(159, 22)
        Me.AddValueRCM.Text = "Create Value"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(156, 6)
        '
        'DeleteRCM
        '
        Me.DeleteRCM.Image = Global.FinancialBI.My.Resources.Resources.elements_delete
        Me.DeleteRCM.Name = "DeleteRCM"
        Me.DeleteRCM.Size = New System.Drawing.Size(159, 22)
        Me.DeleteRCM.Text = "Delete"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(156, 6)
        '
        'RenameRCM
        '
        Me.RenameRCM.Name = "RenameRCM"
        Me.RenameRCM.Size = New System.Drawing.Size(159, 22)
        Me.RenameRCM.Text = "Rename"
        '
        'ExpandAllBT
        '
        Me.ExpandAllBT.Name = "ExpandAllBT"
        Me.ExpandAllBT.Size = New System.Drawing.Size(159, 22)
        Me.ExpandAllBT.Text = "Expand all"
        '
        'CollapseAllBT
        '
        Me.CollapseAllBT.Name = "CollapseAllBT"
        Me.CollapseAllBT.Size = New System.Drawing.Size(159, 22)
        Me.CollapseAllBT.Text = "Collapse all"
        '
        'AxisFiltersView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "AxisFiltersView"
        Me.Size = New System.Drawing.Size(525, 430)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.RCM_TV.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents CategoriesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewCategoryMenuBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddValueMenuBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RenameMenuBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteMenuBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RCM_TV As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents CreateCategoryRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddValueRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents RenameRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DeleteRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExpandAllBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CollapseAllBT As System.Windows.Forms.ToolStripMenuItem

End Class
