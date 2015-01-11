<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UsersManagementUI
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
        Dim DataGridLocalization1 As VIBlend.WinForms.DataGridView.DataGridLocalization = New VIBlend.WinForms.DataGridView.DataGridLocalization()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UsersManagementUI))
        Me.usersTGV = New VIBlend.WinForms.DataGridView.vDataGridView()
        Me.RCM1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CreateUserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreateFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ReinitializePasswordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ButtonIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.TGVIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.ExitBT = New System.Windows.Forms.Button()
        Me.DeleteBT = New System.Windows.Forms.Button()
        Me.addFolderBT = New System.Windows.Forms.Button()
        Me.addUserBT = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ReinitPwdBT = New System.Windows.Forms.Button()
        Me.RCM1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'usersTGV
        '
        Me.usersTGV.AllowAnimations = True
        Me.usersTGV.AllowCellMerge = True
        Me.usersTGV.AllowClipDrawing = True
        Me.usersTGV.AllowContextMenuColumnChooser = True
        Me.usersTGV.AllowContextMenuFiltering = True
        Me.usersTGV.AllowContextMenuGrouping = True
        Me.usersTGV.AllowContextMenuSorting = True
        Me.usersTGV.AllowCopyPaste = False
        Me.usersTGV.AllowDefaultContextMenu = True
        Me.usersTGV.AllowDragDropIndication = True
        Me.usersTGV.AllowHeaderItemHighlightOnCellSelection = True
        Me.usersTGV.AutoUpdateOnListChanged = False
        Me.usersTGV.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.usersTGV.BindingProgressEnabled = False
        Me.usersTGV.BindingProgressSampleRate = 20000
        Me.usersTGV.BorderColor = System.Drawing.Color.Empty
        Me.usersTGV.CellsArea.AllowCellMerge = True
        Me.usersTGV.CellsArea.ConditionalFormattingEnabled = False
        Me.usersTGV.ColumnsHierarchy.AllowDragDrop = False
        Me.usersTGV.ColumnsHierarchy.AllowResize = True
        Me.usersTGV.ColumnsHierarchy.AutoStretchColumns = False
        Me.usersTGV.ColumnsHierarchy.Fixed = False
        Me.usersTGV.ColumnsHierarchy.ShowExpandCollapseButtons = True
        Me.usersTGV.ContextMenuStrip = Me.RCM1
        Me.usersTGV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.usersTGV.EnableColumnChooser = False
        Me.usersTGV.EnableResizeToolTip = True
        Me.usersTGV.EnableToolTips = True
        Me.usersTGV.FilterDisplayMode = VIBlend.WinForms.DataGridView.FilterDisplayMode.[Default]
        Me.usersTGV.GridLinesDashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.usersTGV.GridLinesDisplayMode = VIBlend.WinForms.DataGridView.GridLinesDisplayMode.DISPLAY_ALL
        Me.usersTGV.GroupingEnabled = False
        Me.usersTGV.HorizontalScroll = 0
        Me.usersTGV.HorizontalScrollBarLargeChange = 20
        Me.usersTGV.HorizontalScrollBarSmallChange = 5
        Me.usersTGV.ImageList = Nothing
        Me.usersTGV.Localization = DataGridLocalization1
        Me.usersTGV.Location = New System.Drawing.Point(3, 3)
        Me.usersTGV.MultipleSelectionEnabled = True
        Me.usersTGV.Name = "usersTGV"
        Me.usersTGV.PivotColumnsTotalsEnabled = False
        Me.usersTGV.PivotColumnsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH
        Me.usersTGV.PivotRowsTotalsEnabled = False
        Me.usersTGV.PivotRowsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH
        Me.usersTGV.RowsHierarchy.AllowDragDrop = False
        Me.usersTGV.RowsHierarchy.AllowResize = True
        Me.usersTGV.RowsHierarchy.CompactStyleRenderingEnabled = False
        Me.usersTGV.RowsHierarchy.CompactStyleRenderingItemsIndent = 15
        Me.usersTGV.RowsHierarchy.Fixed = False
        Me.usersTGV.RowsHierarchy.ShowExpandCollapseButtons = True
        Me.usersTGV.ScrollBarsEnabled = True
        Me.usersTGV.SelectionBorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.usersTGV.SelectionBorderEnabled = True
        Me.usersTGV.SelectionBorderWidth = 2
        Me.usersTGV.SelectionMode = VIBlend.WinForms.DataGridView.vDataGridView.SELECTION_MODE.CELL_SELECT
        Me.usersTGV.Size = New System.Drawing.Size(789, 556)
        Me.usersTGV.TabIndex = 0
        Me.usersTGV.Text = "VDataGridView1"
        Me.usersTGV.ToolTipDuration = 5000
        Me.usersTGV.ToolTipShowDelay = 1500
        Me.usersTGV.VerticalScroll = 0
        Me.usersTGV.VerticalScrollBarLargeChange = 20
        Me.usersTGV.VerticalScrollBarSmallChange = 5
        Me.usersTGV.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        Me.usersTGV.VirtualModeCellDefault = False
        '
        'RCM1
        '
        Me.RCM1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CreateUserToolStripMenuItem, Me.CreateFolderToolStripMenuItem, Me.ToolStripSeparator1, Me.DeleteToolStripMenuItem, Me.ToolStripSeparator2, Me.ReinitializePasswordToolStripMenuItem})
        Me.RCM1.Name = "RCM1"
        Me.RCM1.Size = New System.Drawing.Size(184, 104)
        '
        'CreateUserToolStripMenuItem
        '
        Me.CreateUserToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.checked
        Me.CreateUserToolStripMenuItem.Name = "CreateUserToolStripMenuItem"
        Me.CreateUserToolStripMenuItem.Size = New System.Drawing.Size(183, 22)
        Me.CreateUserToolStripMenuItem.Text = "Create User"
        '
        'CreateFolderToolStripMenuItem
        '
        Me.CreateFolderToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.folder2
        Me.CreateFolderToolStripMenuItem.Name = "CreateFolderToolStripMenuItem"
        Me.CreateFolderToolStripMenuItem.Size = New System.Drawing.Size(183, 22)
        Me.CreateFolderToolStripMenuItem.Text = "Create Folder"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(180, 6)
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.imageres_89
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(183, 22)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(180, 6)
        '
        'ReinitializePasswordToolStripMenuItem
        '
        Me.ReinitializePasswordToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.imageres_59
        Me.ReinitializePasswordToolStripMenuItem.Name = "ReinitializePasswordToolStripMenuItem"
        Me.ReinitializePasswordToolStripMenuItem.Size = New System.Drawing.Size(183, 22)
        Me.ReinitializePasswordToolStripMenuItem.Text = "Reinitialize Password"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.usersTGV, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 562.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(795, 562)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'ButtonIcons
        '
        Me.ButtonIcons.ImageStream = CType(resources.GetObject("ButtonIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonIcons.Images.SetKeyName(0, "submit 1 ok.ico")
        Me.ButtonIcons.Images.SetKeyName(1, "imageres_89.ico")
        Me.ButtonIcons.Images.SetKeyName(2, "imageres_59.ico")
        Me.ButtonIcons.Images.SetKeyName(3, "users.png")
        Me.ButtonIcons.Images.SetKeyName(4, "favicon(81).ico")
        '
        'TGVIcons
        '
        Me.TGVIcons.ImageStream = CType(resources.GetObject("TGVIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.TGVIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.TGVIcons.Images.SetKeyName(0, "favicon(91).ico")
        Me.TGVIcons.Images.SetKeyName(1, "favicon(81).ico")
        Me.TGVIcons.Images.SetKeyName(2, "favicon(88).ico")
        Me.TGVIcons.Images.SetKeyName(3, "favicon(87).ico")
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TableLayoutPanel2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TableLayoutPanel1)
        Me.SplitContainer1.Size = New System.Drawing.Size(1006, 562)
        Me.SplitContainer1.SplitterDistance = 207
        Me.SplitContainer1.TabIndex = 2
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.BackColor = System.Drawing.SystemColors.GrayText
        Me.TableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.ExitBT, 0, 7)
        Me.TableLayoutPanel2.Controls.Add(Me.DeleteBT, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.addFolderBT, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.addUserBT, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Button1, 0, 5)
        Me.TableLayoutPanel2.Controls.Add(Me.ReinitPwdBT, 0, 3)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 8
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(207, 562)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'ExitBT
        '
        Me.ExitBT.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExitBT.BackColor = System.Drawing.SystemColors.GrayText
        Me.ExitBT.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.ExitBT.FlatAppearance.BorderSize = 0
        Me.ExitBT.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ScrollBar
        Me.ExitBT.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.AppWorkspace
        Me.ExitBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ExitBT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExitBT.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.ExitBT.Location = New System.Drawing.Point(1, 491)
        Me.ExitBT.Margin = New System.Windows.Forms.Padding(0)
        Me.ExitBT.Name = "ExitBT"
        Me.ExitBT.Size = New System.Drawing.Size(205, 70)
        Me.ExitBT.TabIndex = 7
        Me.ExitBT.Text = "Exit"
        Me.ExitBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExitBT.UseVisualStyleBackColor = False
        '
        'DeleteBT
        '
        Me.DeleteBT.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DeleteBT.BackColor = System.Drawing.SystemColors.GrayText
        Me.DeleteBT.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.DeleteBT.FlatAppearance.BorderSize = 0
        Me.DeleteBT.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ScrollBar
        Me.DeleteBT.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.AppWorkspace
        Me.DeleteBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DeleteBT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DeleteBT.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.DeleteBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.DeleteBT.ImageKey = "imageres_89.ico"
        Me.DeleteBT.ImageList = Me.ButtonIcons
        Me.DeleteBT.Location = New System.Drawing.Point(1, 141)
        Me.DeleteBT.Margin = New System.Windows.Forms.Padding(0)
        Me.DeleteBT.Name = "DeleteBT"
        Me.DeleteBT.Size = New System.Drawing.Size(205, 69)
        Me.DeleteBT.TabIndex = 3
        Me.DeleteBT.Text = "Delete User"
        Me.DeleteBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DeleteBT.UseVisualStyleBackColor = False
        '
        'addFolderBT
        '
        Me.addFolderBT.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.addFolderBT.BackColor = System.Drawing.SystemColors.GrayText
        Me.addFolderBT.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.addFolderBT.FlatAppearance.BorderSize = 0
        Me.addFolderBT.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ScrollBar
        Me.addFolderBT.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.AppWorkspace
        Me.addFolderBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.addFolderBT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.addFolderBT.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.addFolderBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.addFolderBT.ImageKey = "favicon(81).ico"
        Me.addFolderBT.ImageList = Me.ButtonIcons
        Me.addFolderBT.Location = New System.Drawing.Point(1, 71)
        Me.addFolderBT.Margin = New System.Windows.Forms.Padding(0)
        Me.addFolderBT.Name = "addFolderBT"
        Me.addFolderBT.Size = New System.Drawing.Size(205, 69)
        Me.addFolderBT.TabIndex = 2
        Me.addFolderBT.Text = "Create New Folder"
        Me.addFolderBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.addFolderBT.UseVisualStyleBackColor = False
        '
        'addUserBT
        '
        Me.addUserBT.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.addUserBT.BackColor = System.Drawing.SystemColors.GrayText
        Me.addUserBT.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.addUserBT.FlatAppearance.BorderSize = 0
        Me.addUserBT.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ScrollBar
        Me.addUserBT.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.AppWorkspace
        Me.addUserBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.addUserBT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.addUserBT.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.addUserBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.addUserBT.ImageKey = "users.png"
        Me.addUserBT.ImageList = Me.ButtonIcons
        Me.addUserBT.Location = New System.Drawing.Point(1, 1)
        Me.addUserBT.Margin = New System.Windows.Forms.Padding(0)
        Me.addUserBT.Name = "addUserBT"
        Me.addUserBT.Size = New System.Drawing.Size(205, 69)
        Me.addUserBT.TabIndex = 1
        Me.addUserBT.Text = "Create New User"
        Me.addUserBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.addUserBT.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.SystemColors.GrayText
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ScrollBar
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.AppWorkspace
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Button1.Location = New System.Drawing.Point(1, 351)
        Me.Button1.Margin = New System.Windows.Forms.Padding(0)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(205, 69)
        Me.Button1.TabIndex = 5
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.UseVisualStyleBackColor = False
        '
        'ReinitPwdBT
        '
        Me.ReinitPwdBT.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ReinitPwdBT.BackColor = System.Drawing.SystemColors.GrayText
        Me.ReinitPwdBT.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.ReinitPwdBT.FlatAppearance.BorderSize = 0
        Me.ReinitPwdBT.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ScrollBar
        Me.ReinitPwdBT.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.AppWorkspace
        Me.ReinitPwdBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ReinitPwdBT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ReinitPwdBT.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.ReinitPwdBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ReinitPwdBT.ImageKey = "imageres_59.ico"
        Me.ReinitPwdBT.ImageList = Me.ButtonIcons
        Me.ReinitPwdBT.Location = New System.Drawing.Point(1, 211)
        Me.ReinitPwdBT.Margin = New System.Windows.Forms.Padding(0)
        Me.ReinitPwdBT.Name = "ReinitPwdBT"
        Me.ReinitPwdBT.Size = New System.Drawing.Size(205, 69)
        Me.ReinitPwdBT.TabIndex = 4
        Me.ReinitPwdBT.Text = "Reinitialize the Password"
        Me.ReinitPwdBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ReinitPwdBT.UseVisualStyleBackColor = False
        '
        'UsersManagementUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(1006, 562)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "UsersManagementUI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Users management"
        Me.RCM1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents usersTGV As VIBlend.WinForms.DataGridView.vDataGridView
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ButtonIcons As System.Windows.Forms.ImageList
    Friend WithEvents TGVIcons As System.Windows.Forms.ImageList
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ExitBT As System.Windows.Forms.Button
    Friend WithEvents ReinitPwdBT As System.Windows.Forms.Button
    Friend WithEvents DeleteBT As System.Windows.Forms.Button
    Friend WithEvents addFolderBT As System.Windows.Forms.Button
    Friend WithEvents addUserBT As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents RCM1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents CreateUserToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CreateFolderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ReinitializePasswordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
