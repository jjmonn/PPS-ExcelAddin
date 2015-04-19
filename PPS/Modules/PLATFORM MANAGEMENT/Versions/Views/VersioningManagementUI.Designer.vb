<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VersioningManagementUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VersioningManagementUI))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.VersionsTVPanel = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.AddVersionBT = New System.Windows.Forms.Button()
        Me.ButtonsImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.AddFolderBT = New System.Windows.Forms.Button()
        Me.DeleteVersionBT = New System.Windows.Forms.Button()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CreationTB = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.LockedDateT = New System.Windows.Forms.TextBox()
        Me.NameTB = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lockedCB = New System.Windows.Forms.CheckBox()
        Me.TimeConfigTB = New System.Windows.Forms.TextBox()
        Me.StartPeriodTB = New System.Windows.Forms.TextBox()
        Me.NBPeriodsTB = New System.Windows.Forms.TextBox()
        Me.RatesVersionCB = New System.Windows.Forms.ComboBox()
        Me.VersionsTVIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.RCM_TV = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.new_version_bt = New System.Windows.Forms.ToolStripMenuItem()
        Me.new_folder_bt = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.rename_bt = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.delete_bt = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.RCM_TV.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TableLayoutPanel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TableLayoutPanel2)
        Me.SplitContainer1.Size = New System.Drawing.Size(1036, 587)
        Me.SplitContainer1.SplitterDistance = 289
        Me.SplitContainer1.TabIndex = 0
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.VersionsTVPanel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(289, 587)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'VersionsTVPanel
        '
        Me.VersionsTVPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VersionsTVPanel.Location = New System.Drawing.Point(3, 28)
        Me.VersionsTVPanel.Name = "VersionsTVPanel"
        Me.VersionsTVPanel.Size = New System.Drawing.Size(283, 556)
        Me.VersionsTVPanel.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.AddVersionBT)
        Me.Panel1.Controls.Add(Me.AddFolderBT)
        Me.Panel1.Controls.Add(Me.DeleteVersionBT)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(289, 25)
        Me.Panel1.TabIndex = 1
        '
        'AddVersionBT
        '
        Me.AddVersionBT.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.AddVersionBT.FlatAppearance.BorderSize = 0
        Me.AddVersionBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AddVersionBT.ImageKey = "favicon(2).ico"
        Me.AddVersionBT.ImageList = Me.ButtonsImageList
        Me.AddVersionBT.Location = New System.Drawing.Point(49, 1)
        Me.AddVersionBT.Name = "AddVersionBT"
        Me.AddVersionBT.Size = New System.Drawing.Size(22, 22)
        Me.AddVersionBT.TabIndex = 14
        Me.AddVersionBT.UseVisualStyleBackColor = True
        '
        'ButtonsImageList
        '
        Me.ButtonsImageList.ImageStream = CType(resources.GetObject("ButtonsImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonsImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonsImageList.Images.SetKeyName(0, "add blue.jpg")
        Me.ButtonsImageList.Images.SetKeyName(1, "favicon(188).ico")
        Me.ButtonsImageList.Images.SetKeyName(2, "favicon(81).ico")
        Me.ButtonsImageList.Images.SetKeyName(3, "imageres_89.ico")
        Me.ButtonsImageList.Images.SetKeyName(4, "favicon(2).ico")
        '
        'AddFolderBT
        '
        Me.AddFolderBT.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.AddFolderBT.FlatAppearance.BorderSize = 0
        Me.AddFolderBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AddFolderBT.ImageKey = "favicon(81).ico"
        Me.AddFolderBT.ImageList = Me.ButtonsImageList
        Me.AddFolderBT.Location = New System.Drawing.Point(12, 1)
        Me.AddFolderBT.Name = "AddFolderBT"
        Me.AddFolderBT.Size = New System.Drawing.Size(22, 22)
        Me.AddFolderBT.TabIndex = 13
        Me.AddFolderBT.UseVisualStyleBackColor = True
        '
        'DeleteVersionBT
        '
        Me.DeleteVersionBT.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.DeleteVersionBT.FlatAppearance.BorderSize = 0
        Me.DeleteVersionBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DeleteVersionBT.ImageKey = "imageres_89.ico"
        Me.DeleteVersionBT.ImageList = Me.ButtonsImageList
        Me.DeleteVersionBT.Location = New System.Drawing.Point(87, 2)
        Me.DeleteVersionBT.Name = "DeleteVersionBT"
        Me.DeleteVersionBT.Size = New System.Drawing.Size(22, 22)
        Me.DeleteVersionBT.TabIndex = 12
        Me.DeleteVersionBT.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.00139!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.99861!))
        Me.TableLayoutPanel2.Controls.Add(Me.Label2, 0, 7)
        Me.TableLayoutPanel2.Controls.Add(Me.Label1, 0, 6)
        Me.TableLayoutPanel2.Controls.Add(Me.Label11, 0, 5)
        Me.TableLayoutPanel2.Controls.Add(Me.Label10, 0, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.Label4, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.CreationTB, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Label7, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Label8, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.LockedDateT, 1, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.NameTB, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Label3, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.lockedCB, 1, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.TimeConfigTB, 1, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.StartPeriodTB, 1, 5)
        Me.TableLayoutPanel2.Controls.Add(Me.NBPeriodsTB, 1, 6)
        Me.TableLayoutPanel2.Controls.Add(Me.RatesVersionCB, 1, 7)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(50, 28)
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
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(681, 368)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 329)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(124, 13)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "Exchange Rates Version"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 283)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Number of Years"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(3, 237)
        Me.Label11.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(68, 13)
        Me.Label11.TabIndex = 17
        Me.Label11.Text = "Starting Year"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(3, 191)
        Me.Label10.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(106, 13)
        Me.Label10.TabIndex = 15
        Me.Label10.Text = "Periods configuration"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 7)
        Me.Label4.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Version Name"
        '
        'CreationTB
        '
        Me.CreationTB.Enabled = False
        Me.CreationTB.Location = New System.Drawing.Point(146, 51)
        Me.CreationTB.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.CreationTB.MaximumSize = New System.Drawing.Size(400, 4)
        Me.CreationTB.MinimumSize = New System.Drawing.Size(280, 20)
        Me.CreationTB.Name = "CreationTB"
        Me.CreationTB.Size = New System.Drawing.Size(280, 20)
        Me.CreationTB.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(3, 99)
        Me.Label7.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(77, 13)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "Version locked"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(3, 145)
        Me.Label8.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(67, 13)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "Locked date"
        '
        'LockedDateT
        '
        Me.LockedDateT.Enabled = False
        Me.LockedDateT.Location = New System.Drawing.Point(146, 143)
        Me.LockedDateT.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.LockedDateT.MaximumSize = New System.Drawing.Size(400, 4)
        Me.LockedDateT.MinimumSize = New System.Drawing.Size(280, 20)
        Me.LockedDateT.Name = "LockedDateT"
        Me.LockedDateT.Size = New System.Drawing.Size(280, 20)
        Me.LockedDateT.TabIndex = 12
        '
        'NameTB
        '
        Me.NameTB.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NameTB.Enabled = False
        Me.NameTB.Location = New System.Drawing.Point(146, 5)
        Me.NameTB.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.NameTB.MaximumSize = New System.Drawing.Size(400, 4)
        Me.NameTB.MinimumSize = New System.Drawing.Size(280, 20)
        Me.NameTB.Name = "NameTB"
        Me.NameTB.Size = New System.Drawing.Size(400, 20)
        Me.NameTB.TabIndex = 13
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 53)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Creation date"
        '
        'lockedCB
        '
        Me.lockedCB.AutoSize = True
        Me.lockedCB.Location = New System.Drawing.Point(146, 102)
        Me.lockedCB.Margin = New System.Windows.Forms.Padding(3, 10, 3, 3)
        Me.lockedCB.Name = "lockedCB"
        Me.lockedCB.Size = New System.Drawing.Size(15, 14)
        Me.lockedCB.TabIndex = 14
        Me.lockedCB.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.lockedCB.UseVisualStyleBackColor = True
        '
        'TimeConfigTB
        '
        Me.TimeConfigTB.Enabled = False
        Me.TimeConfigTB.Location = New System.Drawing.Point(146, 187)
        Me.TimeConfigTB.Name = "TimeConfigTB"
        Me.TimeConfigTB.Size = New System.Drawing.Size(188, 20)
        Me.TimeConfigTB.TabIndex = 18
        '
        'StartPeriodTB
        '
        Me.StartPeriodTB.Enabled = False
        Me.StartPeriodTB.Location = New System.Drawing.Point(146, 233)
        Me.StartPeriodTB.Name = "StartPeriodTB"
        Me.StartPeriodTB.Size = New System.Drawing.Size(188, 20)
        Me.StartPeriodTB.TabIndex = 19
        '
        'NBPeriodsTB
        '
        Me.NBPeriodsTB.Enabled = False
        Me.NBPeriodsTB.Location = New System.Drawing.Point(146, 279)
        Me.NBPeriodsTB.Name = "NBPeriodsTB"
        Me.NBPeriodsTB.Size = New System.Drawing.Size(188, 20)
        Me.NBPeriodsTB.TabIndex = 21
        '
        'RatesVersionCB
        '
        Me.RatesVersionCB.FormattingEnabled = True
        Me.RatesVersionCB.Location = New System.Drawing.Point(146, 325)
        Me.RatesVersionCB.Name = "RatesVersionCB"
        Me.RatesVersionCB.Size = New System.Drawing.Size(188, 21)
        Me.RatesVersionCB.TabIndex = 23
        '
        'VersionsTVIcons
        '
        Me.VersionsTVIcons.ImageStream = CType(resources.GetObject("VersionsTVIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.VersionsTVIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.VersionsTVIcons.Images.SetKeyName(0, "favicon.ico")
        Me.VersionsTVIcons.Images.SetKeyName(1, "favicon(81).ico")
        '
        'RCM_TV
        '
        Me.RCM_TV.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.new_version_bt, Me.new_folder_bt, Me.ToolStripSeparator2, Me.rename_bt, Me.ToolStripSeparator1, Me.delete_bt})
        Me.RCM_TV.Name = "RCM_TV"
        Me.RCM_TV.Size = New System.Drawing.Size(151, 104)
        '
        'new_version_bt
        '
        Me.new_version_bt.Image = Global.PPS.My.Resources.Resources.checked
        Me.new_version_bt.Name = "new_version_bt"
        Me.new_version_bt.Size = New System.Drawing.Size(150, 22)
        Me.new_version_bt.Text = "Create Version"
        '
        'new_folder_bt
        '
        Me.new_folder_bt.Image = Global.PPS.My.Resources.Resources.folder2
        Me.new_folder_bt.Name = "new_folder_bt"
        Me.new_folder_bt.Size = New System.Drawing.Size(150, 22)
        Me.new_folder_bt.Text = "Create Folder"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(147, 6)
        '
        'rename_bt
        '
        Me.rename_bt.Name = "rename_bt"
        Me.rename_bt.Size = New System.Drawing.Size(150, 22)
        Me.rename_bt.Text = "Rename"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(147, 6)
        '
        'delete_bt
        '
        Me.delete_bt.Image = Global.PPS.My.Resources.Resources.imageres_89
        Me.delete_bt.Name = "delete_bt"
        Me.delete_bt.Size = New System.Drawing.Size(150, 22)
        Me.delete_bt.Text = "Delete"
        '
        'VersioningManagementUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1036, 587)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "VersioningManagementUI"
        Me.Text = "Versions management"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.RCM_TV.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents VersionsTVIcons As System.Windows.Forms.ImageList
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents CreationTB As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents LockedDateT As System.Windows.Forms.TextBox
    Friend WithEvents NameTB As System.Windows.Forms.TextBox
    Friend WithEvents lockedCB As System.Windows.Forms.CheckBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents RCM_TV As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents new_version_bt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents new_folder_bt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents rename_bt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents delete_bt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TimeConfigTB As System.Windows.Forms.TextBox
    Friend WithEvents StartPeriodTB As System.Windows.Forms.TextBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents VersionsTVPanel As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents DeleteVersionBT As System.Windows.Forms.Button
    Friend WithEvents AddVersionBT As System.Windows.Forms.Button
    Friend WithEvents ButtonsImageList As System.Windows.Forms.ImageList
    Friend WithEvents AddFolderBT As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents NBPeriodsTB As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents RatesVersionCB As System.Windows.Forms.ComboBox
End Class
