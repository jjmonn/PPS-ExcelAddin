<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        Me.m_tableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.CategoriesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddValueMenuBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteMenuBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.RenameMenuBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.EditStructureToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RCM_TV = New System.Windows.Forms.ContextMenuStrip(Me.components)
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
        'm_tableLayoutPanel
        '
        Me.m_tableLayoutPanel.ColumnCount = 1
        Me.m_tableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.m_tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_tableLayoutPanel.Location = New System.Drawing.Point(0, 0)
        Me.m_tableLayoutPanel.Name = "m_tableLayoutPanel"
        Me.m_tableLayoutPanel.RowCount = 2
        Me.m_tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.m_tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.m_tableLayoutPanel.Size = New System.Drawing.Size(525, 430)
        Me.m_tableLayoutPanel.TabIndex = 0
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
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CategoriesToolStripMenuItem, Me.EditStructureToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(525, 24)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'CategoriesToolStripMenuItem
        '
        Me.CategoriesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddValueMenuBT, Me.DeleteMenuBT, Me.RenameMenuBT, Me.ToolStripSeparator3})
        Me.CategoriesToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Resources.element_branch25
        Me.CategoriesToolStripMenuItem.Name = "CategoriesToolStripMenuItem"
        Me.CategoriesToolStripMenuItem.Size = New System.Drawing.Size(129, 20)
        Me.CategoriesToolStripMenuItem.Text = "[filters.categories]"
        '
        'AddValueMenuBT
        '
        Me.AddValueMenuBT.Name = "AddValueMenuBT"
        Me.AddValueMenuBT.Size = New System.Drawing.Size(169, 22)
        Me.AddValueMenuBT.Text = "[filters.new_value]"
        '
        'DeleteMenuBT
        '
        Me.DeleteMenuBT.Image = Global.FinancialBI.My.Resources.Resources.imageres_89
        Me.DeleteMenuBT.Name = "DeleteMenuBT"
        Me.DeleteMenuBT.Size = New System.Drawing.Size(169, 22)
        Me.DeleteMenuBT.Text = "[general.delete]"
        '
        'RenameMenuBT
        '
        Me.RenameMenuBT.Name = "RenameMenuBT"
        Me.RenameMenuBT.Size = New System.Drawing.Size(169, 22)
        Me.RenameMenuBT.Text = "[general.rename]"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(166, 6)
        '
        'EditStructureToolStripMenuItem
        '
        Me.EditStructureToolStripMenuItem.Name = "EditStructureToolStripMenuItem"
        Me.EditStructureToolStripMenuItem.Size = New System.Drawing.Size(131, 20)
        Me.EditStructureToolStripMenuItem.Text = "[filters.edit_structure]"
        '
        'RCM_TV
        '
        Me.RCM_TV.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddValueRCM, Me.ToolStripSeparator2, Me.DeleteRCM, Me.ToolStripSeparator1, Me.RenameRCM, Me.ExpandAllBT, Me.CollapseAllBT})
        Me.RCM_TV.Name = "RCM_TV"
        Me.RCM_TV.Size = New System.Drawing.Size(185, 126)
        '
        'AddValueRCM
        '
        Me.AddValueRCM.Image = Global.FinancialBI.My.Resources.Resources.add
        Me.AddValueRCM.Name = "AddValueRCM"
        Me.AddValueRCM.Size = New System.Drawing.Size(184, 22)
        Me.AddValueRCM.Text = "[filters.new_value]"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(181, 6)
        '
        'DeleteRCM
        '
        Me.DeleteRCM.Image = Global.FinancialBI.My.Resources.Resources.imageres_89
        Me.DeleteRCM.Name = "DeleteRCM"
        Me.DeleteRCM.Size = New System.Drawing.Size(184, 22)
        Me.DeleteRCM.Text = "[general.delete]"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(181, 6)
        '
        'RenameRCM
        '
        Me.RenameRCM.Name = "RenameRCM"
        Me.RenameRCM.Size = New System.Drawing.Size(184, 22)
        Me.RenameRCM.Text = "[general.rename]"
        '
        'ExpandAllBT
        '
        Me.ExpandAllBT.Name = "ExpandAllBT"
        Me.ExpandAllBT.Size = New System.Drawing.Size(184, 22)
        Me.ExpandAllBT.Text = "[general.expand_all]"
        '
        'CollapseAllBT
        '
        Me.CollapseAllBT.Name = "CollapseAllBT"
        Me.CollapseAllBT.Size = New System.Drawing.Size(184, 22)
        Me.CollapseAllBT.Text = "[general.collapse_all]"
        '
        'AxisFiltersView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.m_tableLayoutPanel)
        Me.Name = "AxisFiltersView"
        Me.Size = New System.Drawing.Size(525, 430)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.RCM_TV.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents m_tableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents CategoriesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddValueMenuBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RenameMenuBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteMenuBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RCM_TV As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AddValueRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents RenameRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DeleteRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExpandAllBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CollapseAllBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditStructureToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
