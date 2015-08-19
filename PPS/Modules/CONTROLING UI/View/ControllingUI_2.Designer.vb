<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ControllingUI_2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ControllingUI_2))
        Me.entitiesRightClickMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.SelectAllSubEntitiesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UnselectAllSubEntitiesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.periodsRightClickMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SelectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UnselectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DGVsRCM = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DisplayAdjustmensRCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.DisplayDataTrackingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.AdjustmentsRCM = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SelectAllToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.UnselectAllToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.tabControl1 = New VIBlend.WinForms.Controls.vTabControl()
        Me.BTIL = New System.Windows.Forms.ImageList(Me.components)
        Me.MenuIL = New System.Windows.Forms.ImageList(Me.components)
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.VersionTB = New VIBlend.WinForms.Controls.vTextBox()
        Me.CurrencyTB = New VIBlend.WinForms.Controls.vTextBox()
        Me.EntityTB = New VIBlend.WinForms.Controls.vTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.MainMenu = New System.Windows.Forms.MenuStrip()
        Me.expansionIL = New System.Windows.Forms.ImageList(Me.components)
        Me.BusinessControlToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VersionsComparisonToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SwitchVersionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HideVersionsComparisonToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DropOnExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.compute_complete = New System.Windows.Forms.ToolStripMenuItem()
        Me.FormatsRCMBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.entitiesRightClickMenu.SuspendLayout()
        Me.periodsRightClickMenu.SuspendLayout()
        Me.DGVsRCM.SuspendLayout()
        Me.AdjustmentsRCM.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.MainMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'entitiesRightClickMenu
        '
        Me.entitiesRightClickMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.compute_complete, Me.ToolStripSeparator1, Me.SelectAllSubEntitiesToolStripMenuItem, Me.UnselectAllSubEntitiesToolStripMenuItem})
        Me.entitiesRightClickMenu.Name = "ContextMenuStripEntitiesNodes"
        Me.entitiesRightClickMenu.Size = New System.Drawing.Size(225, 82)
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(221, 6)
        '
        'SelectAllSubEntitiesToolStripMenuItem
        '
        Me.SelectAllSubEntitiesToolStripMenuItem.Name = "SelectAllSubEntitiesToolStripMenuItem"
        Me.SelectAllSubEntitiesToolStripMenuItem.Size = New System.Drawing.Size(224, 24)
        Me.SelectAllSubEntitiesToolStripMenuItem.Text = "Select All Sub Entities"
        '
        'UnselectAllSubEntitiesToolStripMenuItem
        '
        Me.UnselectAllSubEntitiesToolStripMenuItem.Name = "UnselectAllSubEntitiesToolStripMenuItem"
        Me.UnselectAllSubEntitiesToolStripMenuItem.Size = New System.Drawing.Size(224, 24)
        Me.UnselectAllSubEntitiesToolStripMenuItem.Text = "Unselect All Sub Entities"
        '
        'periodsRightClickMenu
        '
        Me.periodsRightClickMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectAllToolStripMenuItem, Me.UnselectAllToolStripMenuItem})
        Me.periodsRightClickMenu.Name = "periodsRightClickMenu"
        Me.periodsRightClickMenu.Size = New System.Drawing.Size(150, 52)
        '
        'SelectAllToolStripMenuItem
        '
        Me.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem"
        Me.SelectAllToolStripMenuItem.Size = New System.Drawing.Size(149, 24)
        Me.SelectAllToolStripMenuItem.Text = "Select All"
        '
        'UnselectAllToolStripMenuItem
        '
        Me.UnselectAllToolStripMenuItem.Name = "UnselectAllToolStripMenuItem"
        Me.UnselectAllToolStripMenuItem.Size = New System.Drawing.Size(149, 24)
        Me.UnselectAllToolStripMenuItem.Text = "Unselect All"
        '
        'DGVsRCM
        '
        Me.DGVsRCM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DisplayAdjustmensRCM, Me.ToolStripSeparator2, Me.DisplayDataTrackingToolStripMenuItem, Me.ToolStripSeparator4, Me.FormatsRCMBT})
        Me.DGVsRCM.Name = "DGVsRCM"
        Me.DGVsRCM.Size = New System.Drawing.Size(210, 88)
        '
        'DisplayAdjustmensRCM
        '
        Me.DisplayAdjustmensRCM.Name = "DisplayAdjustmensRCM"
        Me.DisplayAdjustmensRCM.Size = New System.Drawing.Size(209, 24)
        Me.DisplayAdjustmensRCM.Text = "Display Adjustments"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(206, 6)
        '
        'DisplayDataTrackingToolStripMenuItem
        '
        Me.DisplayDataTrackingToolStripMenuItem.Name = "DisplayDataTrackingToolStripMenuItem"
        Me.DisplayDataTrackingToolStripMenuItem.Size = New System.Drawing.Size(209, 24)
        Me.DisplayDataTrackingToolStripMenuItem.Text = "Display Data Tracking"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(206, 6)
        '
        'AdjustmentsRCM
        '
        Me.AdjustmentsRCM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectAllToolStripMenuItem1, Me.UnselectAllToolStripMenuItem1})
        Me.AdjustmentsRCM.Name = "AdjustmentsRCM"
        Me.AdjustmentsRCM.Size = New System.Drawing.Size(150, 52)
        '
        'SelectAllToolStripMenuItem1
        '
        Me.SelectAllToolStripMenuItem1.Name = "SelectAllToolStripMenuItem1"
        Me.SelectAllToolStripMenuItem1.Size = New System.Drawing.Size(149, 24)
        Me.SelectAllToolStripMenuItem1.Text = "Select All"
        '
        'UnselectAllToolStripMenuItem1
        '
        Me.UnselectAllToolStripMenuItem1.Name = "UnselectAllToolStripMenuItem1"
        Me.UnselectAllToolStripMenuItem1.Size = New System.Drawing.Size(149, 24)
        Me.UnselectAllToolStripMenuItem1.Text = "Unselect All"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.tabControl1)
        Me.SplitContainer1.Size = New System.Drawing.Size(698, 369)
        Me.SplitContainer1.SplitterDistance = 129
        Me.SplitContainer1.TabIndex = 8
        '
        'tabControl1
        '
        Me.tabControl1.AllowAnimations = True
        Me.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabControl1.Location = New System.Drawing.Point(0, 0)
        Me.tabControl1.Name = "tabControl1"
        Me.tabControl1.Padding = New System.Windows.Forms.Padding(0, 25, 0, 0)
        Me.tabControl1.Size = New System.Drawing.Size(565, 369)
        Me.tabControl1.TabAlignment = VIBlend.WinForms.Controls.vTabPageAlignment.Top
        Me.tabControl1.TabIndex = 0
        Me.tabControl1.TabsAreaBackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.tabControl1.TabsInitialOffset = 5
        Me.tabControl1.TitleHeight = 25
        Me.tabControl1.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        '
        'BTIL
        '
        Me.BTIL.ImageStream = CType(resources.GetObject("BTIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.BTIL.TransparentColor = System.Drawing.Color.Transparent
        Me.BTIL.Images.SetKeyName(0, "tablet_computer.ico")
        '
        'MenuIL
        '
        Me.MenuIL.ImageStream = CType(resources.GetObject("MenuIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.MenuIL.TransparentColor = System.Drawing.Color.Transparent
        Me.MenuIL.Images.SetKeyName(0, "elements.ico")
        Me.MenuIL.Images.SetKeyName(1, "favicon(2).ico")
        Me.MenuIL.Images.SetKeyName(2, "element_branch2.ico")
        Me.MenuIL.Images.SetKeyName(3, "tablet_computer.ico")
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer2.BackColor = System.Drawing.Color.Transparent
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 62)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.SplitContainer1)
        Me.SplitContainer2.Size = New System.Drawing.Size(881, 375)
        Me.SplitContainer2.SplitterDistance = 701
        Me.SplitContainer2.TabIndex = 8
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Panel1.Controls.Add(Me.VersionTB)
        Me.Panel1.Controls.Add(Me.CurrencyTB)
        Me.Panel1.Controls.Add(Me.EntityTB)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.MainMenu)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(881, 61)
        Me.Panel1.TabIndex = 9
        '
        'VersionTB
        '
        Me.VersionTB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VersionTB.BackColor = System.Drawing.Color.White
        Me.VersionTB.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.VersionTB.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.VersionTB.DefaultText = "Empty..."
        Me.VersionTB.Enabled = False
        Me.VersionTB.Location = New System.Drawing.Point(724, 5)
        Me.VersionTB.MaxLength = 32767
        Me.VersionTB.Name = "VersionTB"
        Me.VersionTB.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.VersionTB.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.VersionTB.SelectionLength = 0
        Me.VersionTB.SelectionStart = 0
        Me.VersionTB.Size = New System.Drawing.Size(154, 23)
        Me.VersionTB.TabIndex = 6
        Me.VersionTB.Text = " "
        Me.VersionTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.VersionTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'CurrencyTB
        '
        Me.CurrencyTB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CurrencyTB.BackColor = System.Drawing.Color.White
        Me.CurrencyTB.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.CurrencyTB.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.CurrencyTB.DefaultText = "Empty..."
        Me.CurrencyTB.Enabled = False
        Me.CurrencyTB.Location = New System.Drawing.Point(595, 5)
        Me.CurrencyTB.MaxLength = 32767
        Me.CurrencyTB.Name = "CurrencyTB"
        Me.CurrencyTB.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.CurrencyTB.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.CurrencyTB.SelectionLength = 0
        Me.CurrencyTB.SelectionStart = 0
        Me.CurrencyTB.Size = New System.Drawing.Size(69, 23)
        Me.CurrencyTB.TabIndex = 5
        Me.CurrencyTB.Text = " "
        Me.CurrencyTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.CurrencyTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'EntityTB
        '
        Me.EntityTB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.EntityTB.BackColor = System.Drawing.Color.White
        Me.EntityTB.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.EntityTB.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.EntityTB.DefaultText = "Empty..."
        Me.EntityTB.Enabled = False
        Me.EntityTB.Location = New System.Drawing.Point(392, 5)
        Me.EntityTB.MaxLength = 32767
        Me.EntityTB.Name = "EntityTB"
        Me.EntityTB.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.EntityTB.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.EntityTB.SelectionLength = 0
        Me.EntityTB.SelectionStart = 0
        Me.EntityTB.Size = New System.Drawing.Size(136, 23)
        Me.EntityTB.TabIndex = 4
        Me.EntityTB.Text = " "
        Me.EntityTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.EntityTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(534, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 15)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Currency"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(670, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 15)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Version"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(350, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Entity"
        '
        'MainMenu
        '
        Me.MainMenu.Dock = System.Windows.Forms.DockStyle.None
        Me.MainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcelToolStripMenuItem, Me.BusinessControlToolStripMenuItem, Me.RefreshToolStripMenuItem})
        Me.MainMenu.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu.Name = "MainMenu"
        Me.MainMenu.Size = New System.Drawing.Size(247, 59)
        Me.MainMenu.TabIndex = 0
        Me.MainMenu.Text = "MenuStrip1"
        '
        'expansionIL
        '
        Me.expansionIL.ImageStream = CType(resources.GetObject("expansionIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.expansionIL.TransparentColor = System.Drawing.Color.Transparent
        Me.expansionIL.Images.SetKeyName(0, "expand_down")
        Me.expansionIL.Images.SetKeyName(1, "expand_up")
        Me.expansionIL.Images.SetKeyName(2, "expand_right")
        Me.expansionIL.Images.SetKeyName(3, "expand_left")
        '
        'BusinessControlToolStripMenuItem
        '
        Me.BusinessControlToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.VersionsComparisonToolStripMenuItem, Me.SwitchVersionsToolStripMenuItem, Me.HideVersionsComparisonToolStripMenuItem})
        Me.BusinessControlToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.tablet_computer3
        Me.BusinessControlToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.BusinessControlToolStripMenuItem.Name = "BusinessControlToolStripMenuItem"
        Me.BusinessControlToolStripMenuItem.Size = New System.Drawing.Size(123, 55)
        Me.BusinessControlToolStripMenuItem.Text = "Business Control"
        Me.BusinessControlToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'VersionsComparisonToolStripMenuItem
        '
        Me.VersionsComparisonToolStripMenuItem.Name = "VersionsComparisonToolStripMenuItem"
        Me.VersionsComparisonToolStripMenuItem.Size = New System.Drawing.Size(239, 24)
        Me.VersionsComparisonToolStripMenuItem.Text = "Versions Comparison"
        '
        'SwitchVersionsToolStripMenuItem
        '
        Me.SwitchVersionsToolStripMenuItem.Name = "SwitchVersionsToolStripMenuItem"
        Me.SwitchVersionsToolStripMenuItem.Size = New System.Drawing.Size(239, 24)
        Me.SwitchVersionsToolStripMenuItem.Text = "Switch Versions"
        '
        'HideVersionsComparisonToolStripMenuItem
        '
        Me.HideVersionsComparisonToolStripMenuItem.Name = "HideVersionsComparisonToolStripMenuItem"
        Me.HideVersionsComparisonToolStripMenuItem.Size = New System.Drawing.Size(239, 24)
        Me.HideVersionsComparisonToolStripMenuItem.Text = "Hide Versions Comparison"
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DropOnExcelToolStripMenuItem})
        Me.ExcelToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.Excel_dark_24_24
        Me.ExcelToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(50, 55)
        Me.ExcelToolStripMenuItem.Text = "Excel"
        Me.ExcelToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'DropOnExcelToolStripMenuItem
        '
        Me.DropOnExcelToolStripMenuItem.Name = "DropOnExcelToolStripMenuItem"
        Me.DropOnExcelToolStripMenuItem.Size = New System.Drawing.Size(162, 24)
        Me.DropOnExcelToolStripMenuItem.Text = "Drop on Excel"
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.refresh_db_32
        Me.RefreshToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(66, 55)
        Me.RefreshToolStripMenuItem.Text = "Refresh"
        Me.RefreshToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'compute_complete
        '
        Me.compute_complete.Image = Global.PPS.My.Resources.Resources.refresh_db_32
        Me.compute_complete.Name = "compute_complete"
        Me.compute_complete.Size = New System.Drawing.Size(224, 24)
        Me.compute_complete.Text = "Refresh"
        '
        'FormatsRCMBT
        '
        Me.FormatsRCMBT.Image = Global.PPS.My.Resources.Resources.favicon_13_
        Me.FormatsRCMBT.Name = "FormatsRCMBT"
        Me.FormatsRCMBT.Size = New System.Drawing.Size(209, 24)
        Me.FormatsRCMBT.Text = "Display Options"
        '
        'ControllingUI_2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(212,Byte),Integer), CType(CType(221,Byte),Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(881, 437)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.SplitContainer2)
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "ControllingUI_2"
        Me.Text = "Financials"
        Me.entitiesRightClickMenu.ResumeLayout(false)
        Me.periodsRightClickMenu.ResumeLayout(false)
        Me.DGVsRCM.ResumeLayout(false)
        Me.AdjustmentsRCM.ResumeLayout(false)
        Me.SplitContainer1.Panel2.ResumeLayout(false)
        CType(Me.SplitContainer1,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer1.ResumeLayout(false)
        Me.SplitContainer2.Panel1.ResumeLayout(false)
        CType(Me.SplitContainer2,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer2.ResumeLayout(false)
        Me.Panel1.ResumeLayout(false)
        Me.Panel1.PerformLayout
        Me.MainMenu.ResumeLayout(false)
        Me.MainMenu.PerformLayout
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents entitiesRightClickMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SelectAllSubEntitiesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UnselectAllSubEntitiesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents periodsRightClickMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SelectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UnselectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents compute_complete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DGVsRCM As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DisplayDataTrackingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DisplayAdjustmensRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AdjustmentsRCM As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SelectAllToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UnselectAllToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents FormatsRCMBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents MenuIL As System.Windows.Forms.ImageList
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents BTIL As System.Windows.Forms.ImageList
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents tabControl1 As VIBlend.WinForms.Controls.vTabControl
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents MainMenu As System.Windows.Forms.MenuStrip
    Friend WithEvents RefreshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BusinessControlToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VersionsComparisonToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SwitchVersionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HideVersionsComparisonToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DropOnExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents VersionTB As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents CurrencyTB As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents EntityTB As VIBlend.WinForms.Controls.vTextBox
    Public WithEvents expansionIL As System.Windows.Forms.ImageList
End Class
