<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ClientsControl
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
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewClientMBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteClientMBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelDropMBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.RCM_TGV = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CopyDownRCMBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.DropToExcelRCMBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.NewClientRCMBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteClientRCMBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CopyValueDownRCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.DropToExcelRCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.NewClientRCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteClientRCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.RCM_TGV.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
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
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(484, 394)
        Me.TableLayoutPanel1.TabIndex = 9
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditToolStripMenuItem, Me.ExcelToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(484, 24)
        Me.MenuStrip1.TabIndex = 7
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DoubleClickEnabled = True
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewClientMBT, Me.DeleteClientMBT, Me.ToolStripSeparator2})
        Me.EditToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.users_relation21
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(71, 20)
        Me.EditToolStripMenuItem.Text = "Clients"
        '
        'NewClientMBT
        '
        Me.NewClientMBT.Image = Global.PPS.My.Resources.Resources.users_relation2_add
        Me.NewClientMBT.Name = "NewClientMBT"
        Me.NewClientMBT.Size = New System.Drawing.Size(141, 22)
        Me.NewClientMBT.Text = "New Client"
        '
        'DeleteClientMBT
        '
        Me.DeleteClientMBT.Image = Global.PPS.My.Resources.Resources.users_relation2_delete
        Me.DeleteClientMBT.Name = "DeleteClientMBT"
        Me.DeleteClientMBT.Size = New System.Drawing.Size(141, 22)
        Me.DeleteClientMBT.Text = "Delete Client"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(138, 6)
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcelDropMBT})
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(45, 20)
        Me.ExcelToolStripMenuItem.Text = "Excel"
        '
        'ExcelDropMBT
        '
        Me.ExcelDropMBT.Image = Global.PPS.My.Resources.Resources.favicon_17_1
        Me.ExcelDropMBT.Name = "ExcelDropMBT"
        Me.ExcelDropMBT.Size = New System.Drawing.Size(143, 22)
        Me.ExcelDropMBT.Text = "Send to Excel"
        '
        'RCM_TGV
        '
        Me.RCM_TGV.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopyDownRCMBT, Me.ToolStripSeparator5, Me.DropToExcelRCMBT, Me.ToolStripSeparator4, Me.NewClientRCMBT, Me.DeleteClientRCMBT})
        Me.RCM_TGV.Name = "ContextMenuStripTGV"
        Me.RCM_TGV.Size = New System.Drawing.Size(169, 104)
        '
        'CopyDownRCMBT
        '
        Me.CopyDownRCMBT.Image = Global.PPS.My.Resources.Resources.arrow_down
        Me.CopyDownRCMBT.Name = "CopyDownRCMBT"
        Me.CopyDownRCMBT.Size = New System.Drawing.Size(168, 22)
        Me.CopyDownRCMBT.Text = "Copy Value Down"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(165, 6)
        '
        'DropToExcelRCMBT
        '
        Me.DropToExcelRCMBT.Image = Global.PPS.My.Resources.Resources.Excel_Blue_32x32
        Me.DropToExcelRCMBT.Name = "DropToExcelRCMBT"
        Me.DropToExcelRCMBT.Size = New System.Drawing.Size(168, 22)
        Me.DropToExcelRCMBT.Text = "Drop To Excel"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(165, 6)
        '
        'NewClientRCMBT
        '
        Me.NewClientRCMBT.Image = Global.PPS.My.Resources.Resources.users_relation2_add
        Me.NewClientRCMBT.Name = "NewClientRCMBT"
        Me.NewClientRCMBT.Size = New System.Drawing.Size(168, 22)
        Me.NewClientRCMBT.Text = "New Client"
        '
        'DeleteClientRCMBT
        '
        Me.DeleteClientRCMBT.Image = Global.PPS.My.Resources.Resources.users_relation2_delete
        Me.DeleteClientRCMBT.Name = "DeleteClientRCMBT"
        Me.DeleteClientRCMBT.Size = New System.Drawing.Size(168, 22)
        Me.DeleteClientRCMBT.Text = "Delete Client"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopyValueDownRCM, Me.ToolStripSeparator1, Me.DropToExcelRCM, Me.ToolStripSeparator3, Me.NewClientRCM, Me.DeleteClientRCM})
        Me.ContextMenuStrip1.Name = "ContextMenuStripTGV"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(169, 104)
        '
        'CopyValueDownRCM
        '
        Me.CopyValueDownRCM.Image = Global.PPS.My.Resources.Resources.arrow_down
        Me.CopyValueDownRCM.Name = "CopyValueDownRCM"
        Me.CopyValueDownRCM.Size = New System.Drawing.Size(168, 22)
        Me.CopyValueDownRCM.Text = "Copy Value Down"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(165, 6)
        '
        'DropToExcelRCM
        '
        Me.DropToExcelRCM.Image = Global.PPS.My.Resources.Resources.Excel_Blue_32x32
        Me.DropToExcelRCM.Name = "DropToExcelRCM"
        Me.DropToExcelRCM.Size = New System.Drawing.Size(168, 22)
        Me.DropToExcelRCM.Text = "Drop To Excel"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(165, 6)
        '
        'NewClientRCM
        '
        Me.NewClientRCM.Image = Global.PPS.My.Resources.Resources.barcode_add
        Me.NewClientRCM.Name = "NewClientRCM"
        Me.NewClientRCM.Size = New System.Drawing.Size(168, 22)
        Me.NewClientRCM.Text = "New Client"
        '
        'DeleteClientRCM
        '
        Me.DeleteClientRCM.Image = Global.PPS.My.Resources.Resources.barcode_delete
        Me.DeleteClientRCM.Name = "DeleteClientRCM"
        Me.DeleteClientRCM.Size = New System.Drawing.Size(168, 22)
        Me.DeleteClientRCM.Text = "Delete Client"
        '
        'ClientsControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "ClientsControl"
        Me.Size = New System.Drawing.Size(484, 394)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.RCM_TGV.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewClientMBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteClientMBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExcelDropMBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RCM_TGV As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents CopyDownRCMBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DropToExcelRCMBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents NewClientRCMBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteClientRCMBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents CopyValueDownRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DropToExcelRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents NewClientRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteClientRCM As System.Windows.Forms.ToolStripMenuItem

End Class
