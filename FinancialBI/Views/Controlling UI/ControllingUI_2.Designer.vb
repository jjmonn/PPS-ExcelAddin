﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        Me.EntitiesRCMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RefreshRightClick = New System.Windows.Forms.ToolStripMenuItem()
        Me.PeriodsRCMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SelectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UnselectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataGridViewsRCMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ExpandAllRightClick = New System.Windows.Forms.ToolStripMenuItem()
        Me.CollapseAllRightClick = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.LogRightClick = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.DGVFormatsButton = New System.Windows.Forms.ToolStripMenuItem()
        Me.ColumnsAutoSize = New System.Windows.Forms.ToolStripMenuItem()
        Me.ColumnsAutoFitBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.AdjustmentsRCMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SelectAllToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.UnselectAllToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.m_progressBar = New VIBlend.WinForms.Controls.vProgressBar()
        Me.DGVsControlTab = New VIBlend.WinForms.Controls.vTabControl()
        Me.ButtonsImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.MenuImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.VersionTB = New VIBlend.WinForms.Controls.vTextBox()
        Me.CurrencyTB = New VIBlend.WinForms.Controls.vTextBox()
        Me.EntityTB = New VIBlend.WinForms.Controls.vTextBox()
        Me.m_currencyLabel = New System.Windows.Forms.Label()
        Me.m_versionLabel = New System.Windows.Forms.Label()
        Me.m_entityLabel = New System.Windows.Forms.Label()
        Me.MainMenu = New System.Windows.Forms.MenuStrip()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DropOnExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DropOnlyTheVisibleItemsOnExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BusinessControlToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VersionsComparisonToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SwitchVersionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HideVersionsComparisonToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChartBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExpansionImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.EntitiesRCMenu.SuspendLayout()
        Me.PeriodsRCMenu.SuspendLayout()
        Me.DataGridViewsRCMenu.SuspendLayout()
        Me.AdjustmentsRCMenu.SuspendLayout()
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
        'EntitiesRCMenu
        '
        Me.EntitiesRCMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefreshRightClick})
        Me.EntitiesRCMenu.Name = "ContextMenuStripEntitiesNodes"
        Me.EntitiesRCMenu.Size = New System.Drawing.Size(155, 28)
        '
        'RefreshRightClick
        '
        Me.RefreshRightClick.Image = CType(resources.GetObject("RefreshRightClick.Image"), System.Drawing.Image)
        Me.RefreshRightClick.Name = "RefreshRightClick"
        Me.RefreshRightClick.Size = New System.Drawing.Size(154, 24)
        Me.RefreshRightClick.Text = "[CUI.refresh]"
        '
        'PeriodsRCMenu
        '
        Me.PeriodsRCMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectAllToolStripMenuItem, Me.UnselectAllToolStripMenuItem})
        Me.PeriodsRCMenu.Name = "periodsRightClickMenu"
        Me.PeriodsRCMenu.Size = New System.Drawing.Size(182, 52)
        '
        'SelectAllToolStripMenuItem
        '
        Me.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem"
        Me.SelectAllToolStripMenuItem.Size = New System.Drawing.Size(181, 24)
        Me.SelectAllToolStripMenuItem.Text = "[CUI.select_all]"
        '
        'UnselectAllToolStripMenuItem
        '
        Me.UnselectAllToolStripMenuItem.Name = "UnselectAllToolStripMenuItem"
        Me.UnselectAllToolStripMenuItem.Size = New System.Drawing.Size(181, 24)
        Me.UnselectAllToolStripMenuItem.Text = "[CUI.unselect_all]"
        '
        'DataGridViewsRCMenu
        '
        Me.DataGridViewsRCMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExpandAllRightClick, Me.CollapseAllRightClick, Me.ToolStripSeparator2, Me.LogRightClick, Me.ToolStripSeparator4, Me.DGVFormatsButton, Me.ColumnsAutoSize, Me.ColumnsAutoFitBT})
        Me.DataGridViewsRCMenu.Name = "DGVsRCM"
        Me.DataGridViewsRCMenu.Size = New System.Drawing.Size(306, 160)
        '
        'ExpandAllRightClick
        '
        Me.ExpandAllRightClick.Name = "ExpandAllRightClick"
        Me.ExpandAllRightClick.Size = New System.Drawing.Size(305, 24)
        Me.ExpandAllRightClick.Text = "[CUI.expand_all]"
        '
        'CollapseAllRightClick
        '
        Me.CollapseAllRightClick.Name = "CollapseAllRightClick"
        Me.CollapseAllRightClick.Size = New System.Drawing.Size(305, 24)
        Me.CollapseAllRightClick.Text = "[CUI.collapse_all]"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(302, 6)
        '
        'LogRightClick
        '
        Me.LogRightClick.Name = "LogRightClick"
        Me.LogRightClick.Size = New System.Drawing.Size(305, 24)
        Me.LogRightClick.Text = "[CUI.log]"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(302, 6)
        '
        'DGVFormatsButton
        '
        Me.DGVFormatsButton.Image = CType(resources.GetObject("DGVFormatsButton.Image"), System.Drawing.Image)
        Me.DGVFormatsButton.Name = "DGVFormatsButton"
        Me.DGVFormatsButton.Size = New System.Drawing.Size(305, 24)
        Me.DGVFormatsButton.Text = "[CUI.display_options]"
        '
        'ColumnsAutoSize
        '
        Me.ColumnsAutoSize.Name = "ColumnsAutoSize"
        Me.ColumnsAutoSize.Size = New System.Drawing.Size(305, 24)
        Me.ColumnsAutoSize.Text = "[CUI.adjust_columns_size]"
        '
        'ColumnsAutoFitBT
        '
        Me.ColumnsAutoFitBT.Checked = True
        Me.ColumnsAutoFitBT.CheckOnClick = True
        Me.ColumnsAutoFitBT.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ColumnsAutoFitBT.Name = "ColumnsAutoFitBT"
        Me.ColumnsAutoFitBT.Size = New System.Drawing.Size(305, 24)
        Me.ColumnsAutoFitBT.Text = "[CUI.automatic_columns_adjustment]"
        '
        'AdjustmentsRCMenu
        '
        Me.AdjustmentsRCMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectAllToolStripMenuItem1, Me.UnselectAllToolStripMenuItem1})
        Me.AdjustmentsRCMenu.Name = "AdjustmentsRCM"
        Me.AdjustmentsRCMenu.Size = New System.Drawing.Size(182, 52)
        '
        'SelectAllToolStripMenuItem1
        '
        Me.SelectAllToolStripMenuItem1.Name = "SelectAllToolStripMenuItem1"
        Me.SelectAllToolStripMenuItem1.Size = New System.Drawing.Size(181, 24)
        Me.SelectAllToolStripMenuItem1.Text = "[CUI.select_all]"
        '
        'UnselectAllToolStripMenuItem1
        '
        Me.UnselectAllToolStripMenuItem1.Name = "UnselectAllToolStripMenuItem1"
        Me.UnselectAllToolStripMenuItem1.Size = New System.Drawing.Size(181, 24)
        Me.UnselectAllToolStripMenuItem1.Text = "[CUI.unselect_all]"
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.m_progressBar)
        Me.SplitContainer1.Panel2.Controls.Add(Me.DGVsControlTab)
        Me.SplitContainer1.Size = New System.Drawing.Size(861, 407)
        Me.SplitContainer1.SplitterDistance = 159
        Me.SplitContainer1.TabIndex = 8
        '
        'm_progressBar
        '
        Me.m_progressBar.BackColor = System.Drawing.Color.Transparent
        Me.m_progressBar.Location = New System.Drawing.Point(272, 310)
        Me.m_progressBar.Name = "m_progressBar"
        Me.m_progressBar.RoundedCornersMask = CType(15, Byte)
        Me.m_progressBar.Size = New System.Drawing.Size(272, 18)
        Me.m_progressBar.TabIndex = 3
        Me.m_progressBar.Text = "VProgressBar1"
        Me.m_progressBar.Value = 0
        Me.m_progressBar.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'DGVsControlTab
        '
        Me.DGVsControlTab.AllowAnimations = True
        Me.DGVsControlTab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVsControlTab.Location = New System.Drawing.Point(0, 0)
        Me.DGVsControlTab.Name = "DGVsControlTab"
        Me.DGVsControlTab.Padding = New System.Windows.Forms.Padding(0, 25, 0, 0)
        Me.DGVsControlTab.Size = New System.Drawing.Size(698, 407)
        Me.DGVsControlTab.TabAlignment = VIBlend.WinForms.Controls.vTabPageAlignment.Top
        Me.DGVsControlTab.TabIndex = 0
        Me.DGVsControlTab.TabsAreaBackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.DGVsControlTab.TabsInitialOffset = 5
        Me.DGVsControlTab.TitleHeight = 25
        Me.DGVsControlTab.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        '
        'ButtonsImageList
        '
        Me.ButtonsImageList.ImageStream = CType(resources.GetObject("ButtonsImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonsImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonsImageList.Images.SetKeyName(0, "tablet_computer.ico")
        '
        'MenuImageList
        '
        Me.MenuImageList.ImageStream = CType(resources.GetObject("MenuImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.MenuImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.MenuImageList.Images.SetKeyName(0, "elements.ico")
        Me.MenuImageList.Images.SetKeyName(1, "favicon(2).ico")
        Me.MenuImageList.Images.SetKeyName(2, "element_branch2.ico")
        Me.MenuImageList.Images.SetKeyName(3, "tablet_computer.ico")
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
        Me.SplitContainer2.Size = New System.Drawing.Size(1086, 413)
        Me.SplitContainer2.SplitterDistance = 864
        Me.SplitContainer2.TabIndex = 8
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Panel1.Controls.Add(Me.VersionTB)
        Me.Panel1.Controls.Add(Me.CurrencyTB)
        Me.Panel1.Controls.Add(Me.EntityTB)
        Me.Panel1.Controls.Add(Me.m_currencyLabel)
        Me.Panel1.Controls.Add(Me.m_versionLabel)
        Me.Panel1.Controls.Add(Me.m_entityLabel)
        Me.Panel1.Controls.Add(Me.MainMenu)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1086, 61)
        Me.Panel1.TabIndex = 9
        '
        'VersionTB
        '
        Me.VersionTB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VersionTB.BackColor = System.Drawing.Color.White
        Me.VersionTB.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.VersionTB.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.VersionTB.DefaultText = ""
        Me.VersionTB.Enabled = False
        Me.VersionTB.Location = New System.Drawing.Point(929, 5)
        Me.VersionTB.MaxLength = 32767
        Me.VersionTB.Name = "VersionTB"
        Me.VersionTB.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.VersionTB.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.VersionTB.SelectionLength = 0
        Me.VersionTB.SelectionStart = 0
        Me.VersionTB.Size = New System.Drawing.Size(154, 23)
        Me.VersionTB.TabIndex = 6
        Me.VersionTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.VersionTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'CurrencyTB
        '
        Me.CurrencyTB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CurrencyTB.BackColor = System.Drawing.Color.White
        Me.CurrencyTB.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.CurrencyTB.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.CurrencyTB.DefaultText = ""
        Me.CurrencyTB.Enabled = False
        Me.CurrencyTB.Location = New System.Drawing.Point(800, 5)
        Me.CurrencyTB.MaxLength = 32767
        Me.CurrencyTB.Name = "CurrencyTB"
        Me.CurrencyTB.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.CurrencyTB.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.CurrencyTB.SelectionLength = 0
        Me.CurrencyTB.SelectionStart = 0
        Me.CurrencyTB.Size = New System.Drawing.Size(69, 23)
        Me.CurrencyTB.TabIndex = 5
        Me.CurrencyTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.CurrencyTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'EntityTB
        '
        Me.EntityTB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.EntityTB.BackColor = System.Drawing.Color.White
        Me.EntityTB.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.EntityTB.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.EntityTB.DefaultText = ""
        Me.EntityTB.Enabled = False
        Me.EntityTB.Location = New System.Drawing.Point(597, 5)
        Me.EntityTB.MaxLength = 32767
        Me.EntityTB.Name = "EntityTB"
        Me.EntityTB.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.EntityTB.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.EntityTB.SelectionLength = 0
        Me.EntityTB.SelectionStart = 0
        Me.EntityTB.Size = New System.Drawing.Size(136, 23)
        Me.EntityTB.TabIndex = 4
        Me.EntityTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.EntityTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_currencyLabel
        '
        Me.m_currencyLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_currencyLabel.AutoSize = True
        Me.m_currencyLabel.Location = New System.Drawing.Point(739, 9)
        Me.m_currencyLabel.Name = "m_currencyLabel"
        Me.m_currencyLabel.Size = New System.Drawing.Size(82, 15)
        Me.m_currencyLabel.TabIndex = 3
        Me.m_currencyLabel.Text = "[CUI.currency]"
        '
        'm_versionLabel
        '
        Me.m_versionLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_versionLabel.AutoSize = True
        Me.m_versionLabel.Location = New System.Drawing.Point(875, 9)
        Me.m_versionLabel.Name = "m_versionLabel"
        Me.m_versionLabel.Size = New System.Drawing.Size(75, 15)
        Me.m_versionLabel.TabIndex = 2
        Me.m_versionLabel.Text = "[CUI.version]"
        '
        'm_entityLabel
        '
        Me.m_entityLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_entityLabel.AutoSize = True
        Me.m_entityLabel.Location = New System.Drawing.Point(555, 9)
        Me.m_entityLabel.Name = "m_entityLabel"
        Me.m_entityLabel.Size = New System.Drawing.Size(64, 15)
        Me.m_entityLabel.TabIndex = 1
        Me.m_entityLabel.Text = "[CUI.entity]"
        '
        'MainMenu
        '
        Me.MainMenu.Dock = System.Windows.Forms.DockStyle.None
        Me.MainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExcelToolStripMenuItem, Me.BusinessControlToolStripMenuItem, Me.RefreshToolStripMenuItem, Me.ChartBT})
        Me.MainMenu.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu.Name = "MainMenu"
        Me.MainMenu.ShowItemToolTips = True
        Me.MainMenu.Size = New System.Drawing.Size(515, 59)
        Me.MainMenu.TabIndex = 0
        Me.MainMenu.Text = "[CUI.main_menu]"
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DropOnExcelToolStripMenuItem, Me.DropOnlyTheVisibleItemsOnExcelToolStripMenuItem})
        Me.ExcelToolStripMenuItem.Image = CType(resources.GetObject("ExcelToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ExcelToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(141, 55)
        Me.ExcelToolStripMenuItem.Text = "[CUI.drop_on_excel]"
        Me.ExcelToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ExcelToolStripMenuItem.ToolTipText = "[CUI.drop_on_excel_tooltip]"
        '
        'DropOnExcelToolStripMenuItem
        '
        Me.DropOnExcelToolStripMenuItem.Name = "DropOnExcelToolStripMenuItem"
        Me.DropOnExcelToolStripMenuItem.Size = New System.Drawing.Size(294, 24)
        Me.DropOnExcelToolStripMenuItem.Text = "[CUI.drop_on_excel]"
        '
        'DropOnlyTheVisibleItemsOnExcelToolStripMenuItem
        '
        Me.DropOnlyTheVisibleItemsOnExcelToolStripMenuItem.Name = "DropOnlyTheVisibleItemsOnExcelToolStripMenuItem"
        Me.DropOnlyTheVisibleItemsOnExcelToolStripMenuItem.Size = New System.Drawing.Size(294, 24)
        Me.DropOnlyTheVisibleItemsOnExcelToolStripMenuItem.Text = "Drop only the visible items on Excel"
        '
        'BusinessControlToolStripMenuItem
        '
        Me.BusinessControlToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.VersionsComparisonToolStripMenuItem, Me.SwitchVersionsToolStripMenuItem, Me.HideVersionsComparisonToolStripMenuItem})
        Me.BusinessControlToolStripMenuItem.Image = CType(resources.GetObject("BusinessControlToolStripMenuItem.Image"), System.Drawing.Image)
        Me.BusinessControlToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.BusinessControlToolStripMenuItem.Name = "BusinessControlToolStripMenuItem"
        Me.BusinessControlToolStripMenuItem.Size = New System.Drawing.Size(177, 55)
        Me.BusinessControlToolStripMenuItem.Text = "[CUI.performance_review]"
        Me.BusinessControlToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.BusinessControlToolStripMenuItem.ToolTipText = "[CUI.performance_review_tooltip]"
        '
        'VersionsComparisonToolStripMenuItem
        '
        Me.VersionsComparisonToolStripMenuItem.Name = "VersionsComparisonToolStripMenuItem"
        Me.VersionsComparisonToolStripMenuItem.Size = New System.Drawing.Size(287, 24)
        Me.VersionsComparisonToolStripMenuItem.Text = "[CUI.display_versions_comparison]"
        '
        'SwitchVersionsToolStripMenuItem
        '
        Me.SwitchVersionsToolStripMenuItem.Name = "SwitchVersionsToolStripMenuItem"
        Me.SwitchVersionsToolStripMenuItem.Size = New System.Drawing.Size(287, 24)
        Me.SwitchVersionsToolStripMenuItem.Text = "[CUI.switch_versions]"
        '
        'HideVersionsComparisonToolStripMenuItem
        '
        Me.HideVersionsComparisonToolStripMenuItem.Name = "HideVersionsComparisonToolStripMenuItem"
        Me.HideVersionsComparisonToolStripMenuItem.Size = New System.Drawing.Size(287, 24)
        Me.HideVersionsComparisonToolStripMenuItem.Text = "[CUI.take_off_comparison]"
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Image = CType(resources.GetObject("RefreshToolStripMenuItem.Image"), System.Drawing.Image)
        Me.RefreshToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Space), System.Windows.Forms.Keys)
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(97, 55)
        Me.RefreshToolStripMenuItem.Text = "[CUI.refresh]"
        Me.RefreshToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.RefreshToolStripMenuItem.ToolTipText = "[CUI.refresh_tooltip]"
        '
        'ChartBT
        '
        Me.ChartBT.Image = Global.FinancialBI.My.Resources.Resources.chart_pie
        Me.ChartBT.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ChartBT.Name = "ChartBT"
        Me.ChartBT.Size = New System.Drawing.Size(92, 55)
        Me.ChartBT.Text = "[CUI.charts]"
        Me.ChartBT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ExpansionImageList
        '
        Me.ExpansionImageList.ImageStream = CType(resources.GetObject("ExpansionImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ExpansionImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ExpansionImageList.Images.SetKeyName(0, "add.ico")
        Me.ExpansionImageList.Images.SetKeyName(1, "minus")
        '
        'ControllingUI_2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1086, 475)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.SplitContainer2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "ControllingUI_2"
        Me.Text = "[CUI.financials]"
        Me.EntitiesRCMenu.ResumeLayout(False)
        Me.PeriodsRCMenu.ResumeLayout(False)
        Me.DataGridViewsRCMenu.ResumeLayout(False)
        Me.AdjustmentsRCMenu.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.MainMenu.ResumeLayout(False)
        Me.MainMenu.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents EntitiesRCMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents PeriodsRCMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SelectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UnselectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshRightClick As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DataGridViewsRCMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents LogRightClick As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AdjustmentsRCMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SelectAllToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UnselectAllToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DGVFormatsButton As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents MenuImageList As System.Windows.Forms.ImageList
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents ButtonsImageList As System.Windows.Forms.ImageList
    Friend WithEvents DGVsControlTab As VIBlend.WinForms.Controls.vTabControl
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents MainMenu As System.Windows.Forms.MenuStrip
    Friend WithEvents RefreshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BusinessControlToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VersionsComparisonToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SwitchVersionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HideVersionsComparisonToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DropOnExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents m_currencyLabel As System.Windows.Forms.Label
    Friend WithEvents m_versionLabel As System.Windows.Forms.Label
    Friend WithEvents m_entityLabel As System.Windows.Forms.Label
    Friend WithEvents VersionTB As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents CurrencyTB As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents EntityTB As VIBlend.WinForms.Controls.vTextBox
    Public WithEvents ExpansionImageList As System.Windows.Forms.ImageList
    Friend WithEvents ColumnsAutoFitBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ColumnsAutoSize As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExpandAllRightClick As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CollapseAllRightClick As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChartBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DropOnlyTheVisibleItemsOnExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents m_progressBar As VIBlend.WinForms.Controls.vProgressBar
End Class
