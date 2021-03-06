﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VersionsControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VersionsControl))
        Me.ButtonsImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.RCM_TV = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.new_version_bt = New System.Windows.Forms.ToolStripMenuItem()
        Me.new_folder_bt = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.rename_bt = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.delete_bt = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.VersionsTVPanel = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.m_globalFactsVersionLabel = New System.Windows.Forms.Label()
        Me.m_exchangeRatesVersionLabel = New System.Windows.Forms.Label()
        Me.m_numberOfYearsLabel = New System.Windows.Forms.Label()
        Me.m_startingPeriodLabel = New System.Windows.Forms.Label()
        Me.m_periodConfigLabel = New System.Windows.Forms.Label()
        Me.m_nameLabel = New System.Windows.Forms.Label()
        Me.CreationTB = New System.Windows.Forms.TextBox()
        Me.m_lockedLabel = New System.Windows.Forms.Label()
        Me.m_lockedDateLabel = New System.Windows.Forms.Label()
        Me.LockedDateT = New System.Windows.Forms.TextBox()
        Me.NameTB = New System.Windows.Forms.TextBox()
        Me.m_creationDateLabel = New System.Windows.Forms.Label()
        Me.lockedCB = New System.Windows.Forms.CheckBox()
        Me.m_timeConfigTB = New System.Windows.Forms.TextBox()
        Me.StartPeriodTB = New System.Windows.Forms.ComboBox()
        Me.NBPeriodsTB = New System.Windows.Forms.TextBox()
        Me.m_exchangeRatesVersionVTreeviewbox = New VIBlend.WinForms.Controls.vTreeViewBox()
        Me.m_factsVersionVTreeviewbox = New VIBlend.WinForms.Controls.vTreeViewBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.VersionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewVersionMenuBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewFolderMenuBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteVersionMenuBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.RenameMenuBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.m_versionsTreeviewImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.RCM_TV.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
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
        'RCM_TV
        '
        Me.RCM_TV.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.new_version_bt, Me.new_folder_bt, Me.ToolStripSeparator2, Me.rename_bt, Me.ToolStripSeparator1, Me.delete_bt})
        Me.RCM_TV.Name = "RCM_TV"
        Me.RCM_TV.Size = New System.Drawing.Size(142, 104)
        '
        'new_version_bt
        '
        Me.new_version_bt.Image = Global.FinancialBI.My.Resources.Resources.elements3_add
        Me.new_version_bt.Name = "new_version_bt"
        Me.new_version_bt.Size = New System.Drawing.Size(141, 22)
        Me.new_version_bt.Text = "New_version"
        '
        'new_folder_bt
        '
        Me.new_folder_bt.Image = Global.FinancialBI.My.Resources.Resources.folder2
        Me.new_folder_bt.Name = "new_folder_bt"
        Me.new_folder_bt.Size = New System.Drawing.Size(141, 22)
        Me.new_folder_bt.Text = "New_folder"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(138, 6)
        '
        'rename_bt
        '
        Me.rename_bt.Name = "rename_bt"
        Me.rename_bt.Size = New System.Drawing.Size(141, 22)
        Me.rename_bt.Text = "Rename"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(138, 6)
        '
        'delete_bt
        '
        Me.delete_bt.Image = Global.FinancialBI.My.Resources.Resources.elements3_delete
        Me.delete_bt.Name = "delete_bt"
        Me.delete_bt.Size = New System.Drawing.Size(141, 22)
        Me.delete_bt.Text = "Delete"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.SplitContainer1, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.MenuStrip1, 0, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.863813!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.13618!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(856, 514)
        Me.TableLayoutPanel3.TabIndex = 2
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 28)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.VersionsTVPanel)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TableLayoutPanel2)
        Me.SplitContainer1.Size = New System.Drawing.Size(850, 483)
        Me.SplitContainer1.SplitterDistance = 232
        Me.SplitContainer1.TabIndex = 2
        '
        'VersionsTVPanel
        '
        Me.VersionsTVPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VersionsTVPanel.Location = New System.Drawing.Point(0, 0)
        Me.VersionsTVPanel.Name = "VersionsTVPanel"
        Me.VersionsTVPanel.Size = New System.Drawing.Size(232, 483)
        Me.VersionsTVPanel.TabIndex = 1
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.07971!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.92029!))
        Me.TableLayoutPanel2.Controls.Add(Me.m_globalFactsVersionLabel, 0, 8)
        Me.TableLayoutPanel2.Controls.Add(Me.m_exchangeRatesVersionLabel, 0, 7)
        Me.TableLayoutPanel2.Controls.Add(Me.m_numberOfYearsLabel, 0, 6)
        Me.TableLayoutPanel2.Controls.Add(Me.m_startingPeriodLabel, 0, 5)
        Me.TableLayoutPanel2.Controls.Add(Me.m_periodConfigLabel, 0, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.m_nameLabel, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.CreationTB, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.m_lockedLabel, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.m_lockedDateLabel, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.LockedDateT, 1, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.NameTB, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.m_creationDateLabel, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.lockedCB, 1, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.m_timeConfigTB, 1, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.StartPeriodTB, 1, 5)
        Me.TableLayoutPanel2.Controls.Add(Me.NBPeriodsTB, 1, 6)
        Me.TableLayoutPanel2.Controls.Add(Me.m_exchangeRatesVersionVTreeviewbox, 1, 7)
        Me.TableLayoutPanel2.Controls.Add(Me.m_factsVersionVTreeviewbox, 1, 8)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(14, 24)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 9
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(552, 392)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'm_globalFactsVersionLabel
        '
        Me.m_globalFactsVersionLabel.AutoSize = True
        Me.m_globalFactsVersionLabel.Location = New System.Drawing.Point(3, 359)
        Me.m_globalFactsVersionLabel.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.m_globalFactsVersionLabel.Name = "m_globalFactsVersionLabel"
        Me.m_globalFactsVersionLabel.Size = New System.Drawing.Size(106, 13)
        Me.m_globalFactsVersionLabel.TabIndex = 24
        Me.m_globalFactsVersionLabel.Text = "Global_facts_version"
        '
        'm_exchangeRatesVersionLabel
        '
        Me.m_exchangeRatesVersionLabel.AutoSize = True
        Me.m_exchangeRatesVersionLabel.Location = New System.Drawing.Point(3, 315)
        Me.m_exchangeRatesVersionLabel.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.m_exchangeRatesVersionLabel.Name = "m_exchangeRatesVersionLabel"
        Me.m_exchangeRatesVersionLabel.Size = New System.Drawing.Size(124, 13)
        Me.m_exchangeRatesVersionLabel.TabIndex = 22
        Me.m_exchangeRatesVersionLabel.Text = "Exchange_rates_version"
        '
        'm_numberOfYearsLabel
        '
        Me.m_numberOfYearsLabel.AutoSize = True
        Me.m_numberOfYearsLabel.Location = New System.Drawing.Point(3, 271)
        Me.m_numberOfYearsLabel.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.m_numberOfYearsLabel.Name = "m_numberOfYearsLabel"
        Me.m_numberOfYearsLabel.Size = New System.Drawing.Size(50, 13)
        Me.m_numberOfYearsLabel.TabIndex = 20
        Me.m_numberOfYearsLabel.Text = "nb_years"
        '
        'm_startingPeriodLabel
        '
        Me.m_startingPeriodLabel.AutoSize = True
        Me.m_startingPeriodLabel.Location = New System.Drawing.Point(3, 227)
        Me.m_startingPeriodLabel.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.m_startingPeriodLabel.Name = "m_startingPeriodLabel"
        Me.m_startingPeriodLabel.Size = New System.Drawing.Size(76, 13)
        Me.m_startingPeriodLabel.TabIndex = 17
        Me.m_startingPeriodLabel.Text = "starting_period"
        '
        'm_periodConfigLabel
        '
        Me.m_periodConfigLabel.AutoSize = True
        Me.m_periodConfigLabel.Location = New System.Drawing.Point(3, 183)
        Me.m_periodConfigLabel.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.m_periodConfigLabel.Name = "m_periodConfigLabel"
        Me.m_periodConfigLabel.Size = New System.Drawing.Size(71, 13)
        Me.m_periodConfigLabel.TabIndex = 15
        Me.m_periodConfigLabel.Text = "period_config"
        '
        'm_nameLabel
        '
        Me.m_nameLabel.AutoSize = True
        Me.m_nameLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.m_nameLabel.Location = New System.Drawing.Point(3, 7)
        Me.m_nameLabel.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.m_nameLabel.Name = "m_nameLabel"
        Me.m_nameLabel.Size = New System.Drawing.Size(86, 13)
        Me.m_nameLabel.TabIndex = 7
        Me.m_nameLabel.Text = "Version_name"
        '
        'CreationTB
        '
        Me.CreationTB.Enabled = False
        Me.CreationTB.Location = New System.Drawing.Point(157, 49)
        Me.CreationTB.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.CreationTB.MaximumSize = New System.Drawing.Size(400, 4)
        Me.CreationTB.MinimumSize = New System.Drawing.Size(280, 20)
        Me.CreationTB.Name = "CreationTB"
        Me.CreationTB.Size = New System.Drawing.Size(280, 20)
        Me.CreationTB.TabIndex = 3
        '
        'm_lockedLabel
        '
        Me.m_lockedLabel.AutoSize = True
        Me.m_lockedLabel.Location = New System.Drawing.Point(3, 95)
        Me.m_lockedLabel.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.m_lockedLabel.Name = "m_lockedLabel"
        Me.m_lockedLabel.Size = New System.Drawing.Size(80, 13)
        Me.m_lockedLabel.TabIndex = 10
        Me.m_lockedLabel.Text = "Version_locked"
        '
        'm_lockedDateLabel
        '
        Me.m_lockedDateLabel.AutoSize = True
        Me.m_lockedDateLabel.Location = New System.Drawing.Point(3, 139)
        Me.m_lockedDateLabel.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.m_lockedDateLabel.Name = "m_lockedDateLabel"
        Me.m_lockedDateLabel.Size = New System.Drawing.Size(70, 13)
        Me.m_lockedDateLabel.TabIndex = 11
        Me.m_lockedDateLabel.Text = "Locked_date"
        '
        'LockedDateT
        '
        Me.LockedDateT.Enabled = False
        Me.LockedDateT.Location = New System.Drawing.Point(157, 137)
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
        Me.NameTB.Location = New System.Drawing.Point(157, 5)
        Me.NameTB.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.NameTB.MaximumSize = New System.Drawing.Size(400, 4)
        Me.NameTB.MinimumSize = New System.Drawing.Size(280, 20)
        Me.NameTB.Name = "NameTB"
        Me.NameTB.Size = New System.Drawing.Size(392, 20)
        Me.NameTB.TabIndex = 13
        '
        'm_creationDateLabel
        '
        Me.m_creationDateLabel.AutoSize = True
        Me.m_creationDateLabel.Location = New System.Drawing.Point(3, 51)
        Me.m_creationDateLabel.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.m_creationDateLabel.Name = "m_creationDateLabel"
        Me.m_creationDateLabel.Size = New System.Drawing.Size(73, 13)
        Me.m_creationDateLabel.TabIndex = 6
        Me.m_creationDateLabel.Text = "Creation_date"
        '
        'lockedCB
        '
        Me.lockedCB.AutoSize = True
        Me.lockedCB.Location = New System.Drawing.Point(157, 98)
        Me.lockedCB.Margin = New System.Windows.Forms.Padding(3, 10, 3, 3)
        Me.lockedCB.Name = "lockedCB"
        Me.lockedCB.Size = New System.Drawing.Size(15, 14)
        Me.lockedCB.TabIndex = 14
        Me.lockedCB.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.lockedCB.UseVisualStyleBackColor = True
        '
        'm_timeConfigTB
        '
        Me.m_timeConfigTB.Enabled = False
        Me.m_timeConfigTB.Location = New System.Drawing.Point(157, 179)
        Me.m_timeConfigTB.Name = "m_timeConfigTB"
        Me.m_timeConfigTB.Size = New System.Drawing.Size(227, 20)
        Me.m_timeConfigTB.TabIndex = 18
        '
        'StartPeriodTB
        '
        Me.StartPeriodTB.Enabled = False
        Me.StartPeriodTB.Location = New System.Drawing.Point(157, 223)
        Me.StartPeriodTB.Name = "StartPeriodTB"
        Me.StartPeriodTB.Size = New System.Drawing.Size(227, 21)
        Me.StartPeriodTB.TabIndex = 19
        '
        'NBPeriodsTB
        '
        Me.NBPeriodsTB.Enabled = False
        Me.NBPeriodsTB.Location = New System.Drawing.Point(157, 267)
        Me.NBPeriodsTB.Name = "NBPeriodsTB"
        Me.NBPeriodsTB.Size = New System.Drawing.Size(227, 20)
        Me.NBPeriodsTB.TabIndex = 21
        '
        'm_exchangeRatesVersionVTreeviewbox
        '
        Me.m_exchangeRatesVersionVTreeviewbox.BackColor = System.Drawing.Color.White
        Me.m_exchangeRatesVersionVTreeviewbox.BorderColor = System.Drawing.Color.Black
        Me.m_exchangeRatesVersionVTreeviewbox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.m_exchangeRatesVersionVTreeviewbox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.m_exchangeRatesVersionVTreeviewbox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.m_exchangeRatesVersionVTreeviewbox.Location = New System.Drawing.Point(157, 311)
        Me.m_exchangeRatesVersionVTreeviewbox.Name = "m_exchangeRatesVersionVTreeviewbox"
        Me.m_exchangeRatesVersionVTreeviewbox.Size = New System.Drawing.Size(227, 23)
        Me.m_exchangeRatesVersionVTreeviewbox.TabIndex = 25
        Me.m_exchangeRatesVersionVTreeviewbox.UseThemeBackColor = False
        Me.m_exchangeRatesVersionVTreeviewbox.UseThemeDropDownArrowColor = True
        Me.m_exchangeRatesVersionVTreeviewbox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_factsVersionVTreeviewbox
        '
        Me.m_factsVersionVTreeviewbox.BackColor = System.Drawing.Color.White
        Me.m_factsVersionVTreeviewbox.BorderColor = System.Drawing.Color.Black
        Me.m_factsVersionVTreeviewbox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.m_factsVersionVTreeviewbox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.m_factsVersionVTreeviewbox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.m_factsVersionVTreeviewbox.Location = New System.Drawing.Point(157, 355)
        Me.m_factsVersionVTreeviewbox.Name = "m_factsVersionVTreeviewbox"
        Me.m_factsVersionVTreeviewbox.Size = New System.Drawing.Size(227, 23)
        Me.m_factsVersionVTreeviewbox.TabIndex = 26
        Me.m_factsVersionVTreeviewbox.UseThemeBackColor = False
        Me.m_factsVersionVTreeviewbox.UseThemeDropDownArrowColor = True
        Me.m_factsVersionVTreeviewbox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.VersionsToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(856, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'VersionsToolStripMenuItem
        '
        Me.VersionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewVersionMenuBT, Me.NewFolderMenuBT, Me.DeleteVersionMenuBT, Me.RenameMenuBT})
        Me.VersionsToolStripMenuItem.Name = "VersionsToolStripMenuItem"
        Me.VersionsToolStripMenuItem.Size = New System.Drawing.Size(62, 20)
        Me.VersionsToolStripMenuItem.Text = "Versions"
        '
        'NewVersionMenuBT
        '
        Me.NewVersionMenuBT.Image = Global.FinancialBI.My.Resources.Resources.elements3_add
        Me.NewVersionMenuBT.Name = "NewVersionMenuBT"
        Me.NewVersionMenuBT.Size = New System.Drawing.Size(191, 22)
        Me.NewVersionMenuBT.Text = "[versions.add_version]"
        '
        'NewFolderMenuBT
        '
        Me.NewFolderMenuBT.Image = Global.FinancialBI.My.Resources.Resources.favicon_81_
        Me.NewFolderMenuBT.Name = "NewFolderMenuBT"
        Me.NewFolderMenuBT.Size = New System.Drawing.Size(191, 22)
        Me.NewFolderMenuBT.Text = "Add_folder"
        '
        'DeleteVersionMenuBT
        '
        Me.DeleteVersionMenuBT.Image = Global.FinancialBI.My.Resources.Resources.elements3_delete
        Me.DeleteVersionMenuBT.Name = "DeleteVersionMenuBT"
        Me.DeleteVersionMenuBT.Size = New System.Drawing.Size(191, 22)
        Me.DeleteVersionMenuBT.Text = "Delete"
        '
        'RenameMenuBT
        '
        Me.RenameMenuBT.Name = "RenameMenuBT"
        Me.RenameMenuBT.Size = New System.Drawing.Size(191, 22)
        Me.RenameMenuBT.Text = "Rename"
        '
        'm_versionsTreeviewImageList
        '
        Me.m_versionsTreeviewImageList.ImageStream = CType(resources.GetObject("m_versionsTreeviewImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.m_versionsTreeviewImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.m_versionsTreeviewImageList.Images.SetKeyName(0, "cloud_dark.ico")
        Me.m_versionsTreeviewImageList.Images.SetKeyName(1, "favicon(81).ico")
        '
        'VersionsControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel3)
        Me.Name = "VersionsControl"
        Me.Size = New System.Drawing.Size(856, 514)
        Me.RCM_TV.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ButtonsImageList As System.Windows.Forms.ImageList
    Friend WithEvents RCM_TV As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents new_version_bt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents new_folder_bt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents rename_bt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents delete_bt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents VersionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewVersionMenuBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewFolderMenuBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteVersionMenuBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents VersionsTVPanel As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents m_exchangeRatesVersionLabel As System.Windows.Forms.Label
    Friend WithEvents m_numberOfYearsLabel As System.Windows.Forms.Label
    Friend WithEvents m_startingPeriodLabel As System.Windows.Forms.Label
    Friend WithEvents m_periodConfigLabel As System.Windows.Forms.Label
    Friend WithEvents m_nameLabel As System.Windows.Forms.Label
    Friend WithEvents CreationTB As System.Windows.Forms.TextBox
    Friend WithEvents m_lockedLabel As System.Windows.Forms.Label
    Friend WithEvents m_lockedDateLabel As System.Windows.Forms.Label
    Friend WithEvents LockedDateT As System.Windows.Forms.TextBox
    Friend WithEvents NameTB As System.Windows.Forms.TextBox
    Friend WithEvents m_creationDateLabel As System.Windows.Forms.Label
    Friend WithEvents lockedCB As System.Windows.Forms.CheckBox
    Friend WithEvents m_timeConfigTB As System.Windows.Forms.TextBox
    Friend WithEvents StartPeriodTB As System.Windows.Forms.ComboBox
    Friend WithEvents NBPeriodsTB As System.Windows.Forms.TextBox
    Friend WithEvents RenameMenuBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents m_globalFactsVersionLabel As System.Windows.Forms.Label
    Friend WithEvents m_exchangeRatesVersionVTreeviewbox As VIBlend.WinForms.Controls.vTreeViewBox
    Friend WithEvents m_factsVersionVTreeviewbox As VIBlend.WinForms.Controls.vTreeViewBox
    Friend WithEvents m_versionsTreeviewImageList As System.Windows.Forms.ImageList

End Class
