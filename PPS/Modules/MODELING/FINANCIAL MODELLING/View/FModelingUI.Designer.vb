<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FModelingUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FModelingUI))
        Me.TabControl1 = New System.Windows.Forms.CustomTabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.VersionsTVpanel = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.VersionTB = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.EntitiesTVPanel = New System.Windows.Forms.Panel()
        Me.EntityTB = New System.Windows.Forms.TextBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditMappingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DisplayConsolidatedInputsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LaunchConsoBT = New System.Windows.Forms.Button()
        Me.ButtonsImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.LeftPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Refresh2BT = New System.Windows.Forms.Button()
        Me.NewConstraintBT = New System.Windows.Forms.Button()
        Me.NewScenarioBT = New System.Windows.Forms.Button()
        Me.scenarioTVPanel = New System.Windows.Forms.Panel()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.ExportsTVPanel = New System.Windows.Forms.Panel()
        Me.EntitiesTV2Panel = New System.Windows.Forms.Panel()
        Me.ExportMappingPanel = New System.Windows.Forms.Panel()
        Me.ScenariosCBPanel = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.ReinjectionBT = New System.Windows.Forms.Button()
        Me.ButtonsBigImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.TreeviewRightClick = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EditConstraintRCTV = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ResfreshScenarioToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.NewScenarioTVRCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewConstraintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.inputsDGVsRightClickMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddConstraintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteConstraintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshScenarioToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.SerieMGTRCMBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.CopyValueRightBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.TVImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.EntitiesTVImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.VersionsTVIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.ExportTVImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.BigArrow = New System.Windows.Forms.ImageList(Me.components)
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.LeftPanel.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.TreeviewRightClick.SuspendLayout()
        Me.inputsDGVsRightClickMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.DisplayStyle = System.Windows.Forms.TabStyle.Rounded
        '
        '
        '
        Me.TabControl1.DisplayStyleProvider.BorderColor = System.Drawing.SystemColors.ControlDark
        Me.TabControl1.DisplayStyleProvider.BorderColorHot = System.Drawing.SystemColors.ControlDark
        Me.TabControl1.DisplayStyleProvider.BorderColorSelected = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.TabControl1.DisplayStyleProvider.CloserColor = System.Drawing.Color.DarkGray
        Me.TabControl1.DisplayStyleProvider.FocusTrack = False
        Me.TabControl1.DisplayStyleProvider.HotTrack = True
        Me.TabControl1.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.TabControl1.DisplayStyleProvider.Opacity = 1.0!
        Me.TabControl1.DisplayStyleProvider.Overlap = 0
        Me.TabControl1.DisplayStyleProvider.Padding = New System.Drawing.Point(6, 3)
        Me.TabControl1.DisplayStyleProvider.Radius = 10
        Me.TabControl1.DisplayStyleProvider.ShowTabCloser = False
        Me.TabControl1.DisplayStyleProvider.TextColor = System.Drawing.SystemColors.ControlText
        Me.TabControl1.DisplayStyleProvider.TextColorDisabled = System.Drawing.SystemColors.ControlDark
        Me.TabControl1.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.HotTrack = True
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(1)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(664, 350)
        Me.TabControl1.TabIndex = 8
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage1.Controls.Add(Me.TableLayoutPanel1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 23)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(656, 323)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Inputs"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 6
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.47462!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.43484!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.09054!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.VersionsTVpanel, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.VersionTB, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Label6, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.EntitiesTVPanel, 3, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.EntityTB, 3, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.MenuStrip1, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LaunchConsoBT, 5, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 5
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.90172!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.098282!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(650, 317)
        Me.TableLayoutPanel1.TabIndex = 11
        '
        'VersionsTVpanel
        '
        Me.VersionsTVpanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VersionsTVpanel.Location = New System.Drawing.Point(8, 59)
        Me.VersionsTVpanel.Name = "VersionsTVpanel"
        Me.VersionsTVpanel.Size = New System.Drawing.Size(154, 200)
        Me.VersionsTVpanel.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 36)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(132, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "1. Select the Input Version"
        '
        'VersionTB
        '
        Me.VersionTB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VersionTB.Location = New System.Drawing.Point(8, 265)
        Me.VersionTB.Name = "VersionTB"
        Me.VersionTB.Size = New System.Drawing.Size(154, 20)
        Me.VersionTB.TabIndex = 16
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(188, 36)
        Me.Label6.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(140, 20)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "2. Select the Entity Level to work on"
        '
        'EntitiesTVPanel
        '
        Me.EntitiesTVPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.EntitiesTVPanel.Location = New System.Drawing.Point(188, 59)
        Me.EntitiesTVPanel.Name = "EntitiesTVPanel"
        Me.EntitiesTVPanel.Size = New System.Drawing.Size(159, 200)
        Me.EntitiesTVPanel.TabIndex = 9
        '
        'EntityTB
        '
        Me.EntityTB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.EntityTB.Location = New System.Drawing.Point(188, 265)
        Me.EntityTB.Name = "EntityTB"
        Me.EntityTB.Size = New System.Drawing.Size(159, 20)
        Me.EntityTB.TabIndex = 15
        '
        'MenuStrip1
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.MenuStrip1, 5)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(5, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(645, 24)
        Me.MenuStrip1.TabIndex = 17
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditMappingToolStripMenuItem, Me.DisplayConsolidatedInputsToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(93, 20)
        Me.EditToolStripMenuItem.Text = "Configuration"
        '
        'EditMappingToolStripMenuItem
        '
        Me.EditMappingToolStripMenuItem.Name = "EditMappingToolStripMenuItem"
        Me.EditMappingToolStripMenuItem.Size = New System.Drawing.Size(221, 22)
        Me.EditMappingToolStripMenuItem.Text = "Edit Mapping"
        '
        'DisplayConsolidatedInputsToolStripMenuItem
        '
        Me.DisplayConsolidatedInputsToolStripMenuItem.Name = "DisplayConsolidatedInputsToolStripMenuItem"
        Me.DisplayConsolidatedInputsToolStripMenuItem.Size = New System.Drawing.Size(221, 22)
        Me.DisplayConsolidatedInputsToolStripMenuItem.Text = "Display Consolidated Inputs"
        '
        'LaunchConsoBT
        '
        Me.LaunchConsoBT.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.LaunchConsoBT.FlatAppearance.BorderSize = 0
        Me.LaunchConsoBT.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonFace
        Me.LaunchConsoBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LaunchConsoBT.ImageKey = "1420498403_340208.ico"
        Me.LaunchConsoBT.ImageList = Me.ButtonsImageList
        Me.LaunchConsoBT.Location = New System.Drawing.Point(370, 56)
        Me.LaunchConsoBT.Margin = New System.Windows.Forms.Padding(0)
        Me.LaunchConsoBT.Name = "LaunchConsoBT"
        Me.LaunchConsoBT.Size = New System.Drawing.Size(82, 26)
        Me.LaunchConsoBT.TabIndex = 3
        Me.LaunchConsoBT.Text = "Validate"
        Me.LaunchConsoBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LaunchConsoBT.UseVisualStyleBackColor = True
        '
        'ButtonsImageList
        '
        Me.ButtonsImageList.ImageStream = CType(resources.GetObject("ButtonsImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonsImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonsImageList.Images.SetKeyName(0, "imageres_89.ico")
        Me.ButtonsImageList.Images.SetKeyName(1, "refresh.ico")
        Me.ButtonsImageList.Images.SetKeyName(2, "Report.png")
        Me.ButtonsImageList.Images.SetKeyName(3, "favicon(196).ico")
        Me.ButtonsImageList.Images.SetKeyName(4, "add blue.jpg")
        Me.ButtonsImageList.Images.SetKeyName(5, "blue.jpg")
        Me.ButtonsImageList.Images.SetKeyName(6, "1420498403_340208.ico")
        Me.ButtonsImageList.Images.SetKeyName(7, "favicon(5).ico")
        Me.ButtonsImageList.Images.SetKeyName(8, "Scenario.ico")
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage2.Controls.Add(Me.SplitContainer1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 23)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(656, 323)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Simulations"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.LeftPanel)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.SplitContainer1.Size = New System.Drawing.Size(650, 317)
        Me.SplitContainer1.SplitterDistance = 92
        Me.SplitContainer1.TabIndex = 0
        '
        'LeftPanel
        '
        Me.LeftPanel.ColumnCount = 1
        Me.LeftPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.LeftPanel.Controls.Add(Me.Panel3, 0, 0)
        Me.LeftPanel.Controls.Add(Me.scenarioTVPanel, 0, 1)
        Me.LeftPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LeftPanel.Location = New System.Drawing.Point(0, 0)
        Me.LeftPanel.Margin = New System.Windows.Forms.Padding(1)
        Me.LeftPanel.Name = "LeftPanel"
        Me.LeftPanel.RowCount = 2
        Me.LeftPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.LeftPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.LeftPanel.Size = New System.Drawing.Size(92, 317)
        Me.LeftPanel.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Refresh2BT)
        Me.Panel3.Controls.Add(Me.NewConstraintBT)
        Me.Panel3.Controls.Add(Me.NewScenarioBT)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(1, 1)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(1)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(90, 24)
        Me.Panel3.TabIndex = 0
        '
        'Refresh2BT
        '
        Me.Refresh2BT.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.Refresh2BT.FlatAppearance.BorderSize = 0
        Me.Refresh2BT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Refresh2BT.ImageKey = "refresh.ico"
        Me.Refresh2BT.ImageList = Me.ButtonsImageList
        Me.Refresh2BT.Location = New System.Drawing.Point(63, 0)
        Me.Refresh2BT.Name = "Refresh2BT"
        Me.Refresh2BT.Size = New System.Drawing.Size(22, 22)
        Me.Refresh2BT.TabIndex = 2
        Me.Refresh2BT.UseVisualStyleBackColor = True
        '
        'NewConstraintBT
        '
        Me.NewConstraintBT.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.NewConstraintBT.FlatAppearance.BorderSize = 0
        Me.NewConstraintBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.NewConstraintBT.ImageKey = "favicon(5).ico"
        Me.NewConstraintBT.ImageList = Me.ButtonsImageList
        Me.NewConstraintBT.Location = New System.Drawing.Point(33, 0)
        Me.NewConstraintBT.Name = "NewConstraintBT"
        Me.NewConstraintBT.Size = New System.Drawing.Size(22, 22)
        Me.NewConstraintBT.TabIndex = 1
        Me.NewConstraintBT.UseVisualStyleBackColor = True
        '
        'NewScenarioBT
        '
        Me.NewScenarioBT.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.NewScenarioBT.FlatAppearance.BorderSize = 0
        Me.NewScenarioBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.NewScenarioBT.ImageKey = "Scenario.ico"
        Me.NewScenarioBT.ImageList = Me.ButtonsImageList
        Me.NewScenarioBT.Location = New System.Drawing.Point(4, 0)
        Me.NewScenarioBT.Name = "NewScenarioBT"
        Me.NewScenarioBT.Size = New System.Drawing.Size(22, 22)
        Me.NewScenarioBT.TabIndex = 0
        Me.NewScenarioBT.UseVisualStyleBackColor = True
        '
        'scenarioTVPanel
        '
        Me.scenarioTVPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.scenarioTVPanel.Location = New System.Drawing.Point(0, 26)
        Me.scenarioTVPanel.Margin = New System.Windows.Forms.Padding(0)
        Me.scenarioTVPanel.Name = "scenarioTVPanel"
        Me.scenarioTVPanel.Size = New System.Drawing.Size(92, 291)
        Me.scenarioTVPanel.TabIndex = 1
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage3.Controls.Add(Me.TableLayoutPanel3)
        Me.TabPage3.Location = New System.Drawing.Point(4, 23)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(656, 323)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Output"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 7
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 66.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.31002!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 147.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.31002!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.29604!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.08392!))
        Me.TableLayoutPanel3.Controls.Add(Me.ExportsTVPanel, 1, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.EntitiesTV2Panel, 3, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.ExportMappingPanel, 5, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.ScenariosCBPanel, 1, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Panel7, 2, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.Label7, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Label9, 5, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Label10, 5, 3)
        Me.TableLayoutPanel3.Controls.Add(Me.ReinjectionBT, 5, 4)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 5
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.787557!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 68.28528!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.92716!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(650, 317)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'ExportsTVPanel
        '
        Me.ExportsTVPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExportsTVPanel.Location = New System.Drawing.Point(67, 56)
        Me.ExportsTVPanel.Margin = New System.Windows.Forms.Padding(1, 5, 1, 1)
        Me.ExportsTVPanel.Name = "ExportsTVPanel"
        Me.ExportsTVPanel.Size = New System.Drawing.Size(67, 176)
        Me.ExportsTVPanel.TabIndex = 1
        '
        'EntitiesTV2Panel
        '
        Me.EntitiesTV2Panel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.EntitiesTV2Panel.Location = New System.Drawing.Point(283, 56)
        Me.EntitiesTV2Panel.Margin = New System.Windows.Forms.Padding(1, 5, 1, 1)
        Me.EntitiesTV2Panel.Name = "EntitiesTV2Panel"
        Me.EntitiesTV2Panel.Size = New System.Drawing.Size(67, 176)
        Me.EntitiesTV2Panel.TabIndex = 2
        '
        'ExportMappingPanel
        '
        Me.ExportMappingPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExportMappingPanel.Location = New System.Drawing.Point(492, 56)
        Me.ExportMappingPanel.Margin = New System.Windows.Forms.Padding(1, 5, 1, 1)
        Me.ExportMappingPanel.Name = "ExportMappingPanel"
        Me.ExportMappingPanel.Size = New System.Drawing.Size(108, 176)
        Me.ExportMappingPanel.TabIndex = 3
        '
        'ScenariosCBPanel
        '
        Me.ScenariosCBPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ScenariosCBPanel.Location = New System.Drawing.Point(66, 26)
        Me.ScenariosCBPanel.Margin = New System.Windows.Forms.Padding(0)
        Me.ScenariosCBPanel.Name = "ScenariosCBPanel"
        Me.ScenariosCBPanel.Size = New System.Drawing.Size(69, 25)
        Me.ScenariosCBPanel.TabIndex = 4
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.Button1)
        Me.Panel7.Controls.Add(Me.Label8)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Location = New System.Drawing.Point(138, 54)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(141, 176)
        Me.Panel7.TabIndex = 5
        '
        'Button1
        '
        Me.Button1.BackgroundImage = Global.PPS.My.Resources.Resources.arrow_1
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(19, 59)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(95, 119)
        Me.Button1.TabIndex = 8
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(3, 15)
        Me.Label8.Margin = New System.Windows.Forms.Padding(3, 0, 3, 5)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(138, 26)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "2. Adjust the Entities where " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "the Exports will be injected"
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(69, 0)
        Me.Label7.Margin = New System.Windows.Forms.Padding(3, 0, 3, 5)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(52, 21)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "1. Select the Scenario"
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(494, 26)
        Me.Label9.Margin = New System.Windows.Forms.Padding(3, 0, 3, 5)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(102, 20)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "3. Adjust the Export Mapping"
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(494, 233)
        Me.Label10.Margin = New System.Windows.Forms.Padding(3, 0, 3, 5)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(97, 20)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "4. Inject Financing Assumptions"
        '
        'ReinjectionBT
        '
        Me.ReinjectionBT.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.ReinjectionBT.FlatAppearance.BorderSize = 0
        Me.ReinjectionBT.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonFace
        Me.ReinjectionBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ReinjectionBT.ImageKey = "favicon(187).ico"
        Me.ReinjectionBT.ImageList = Me.ButtonsImageList
        Me.ReinjectionBT.Location = New System.Drawing.Point(494, 261)
        Me.ReinjectionBT.Name = "ReinjectionBT"
        Me.ReinjectionBT.Size = New System.Drawing.Size(88, 30)
        Me.ReinjectionBT.TabIndex = 9
        Me.ReinjectionBT.Text = "Export"
        Me.ReinjectionBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ReinjectionBT.UseVisualStyleBackColor = True
        '
        'ButtonsBigImageList
        '
        Me.ButtonsBigImageList.ImageStream = CType(resources.GetObject("ButtonsBigImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonsBigImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonsBigImageList.Images.SetKeyName(0, "Report.png")
        Me.ButtonsBigImageList.Images.SetKeyName(1, "checked ctrl bck.png")
        '
        'TreeviewRightClick
        '
        Me.TreeviewRightClick.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditConstraintRCTV, Me.ToolStripSeparator1, Me.ResfreshScenarioToolStripMenuItem, Me.RefreshAllToolStripMenuItem, Me.ToolStripSeparator3, Me.NewScenarioTVRCM, Me.NewConstraintToolStripMenuItem, Me.ToolStripSeparator2, Me.DeleteToolStripMenuItem})
        Me.TreeviewRightClick.Name = "ContextMenuStrip1"
        Me.TreeviewRightClick.Size = New System.Drawing.Size(167, 154)
        '
        'EditConstraintRCTV
        '
        Me.EditConstraintRCTV.Image = Global.PPS.My.Resources.Resources.favicon_65_
        Me.EditConstraintRCTV.Name = "EditConstraintRCTV"
        Me.EditConstraintRCTV.Size = New System.Drawing.Size(166, 22)
        Me.EditConstraintRCTV.Text = "Edit Targets"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(163, 6)
        '
        'ResfreshScenarioToolStripMenuItem
        '
        Me.ResfreshScenarioToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.Refresh2
        Me.ResfreshScenarioToolStripMenuItem.Name = "ResfreshScenarioToolStripMenuItem"
        Me.ResfreshScenarioToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.ResfreshScenarioToolStripMenuItem.Text = "Resfresh Scenario"
        '
        'RefreshAllToolStripMenuItem
        '
        Me.RefreshAllToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.favicon__47_
        Me.RefreshAllToolStripMenuItem.Name = "RefreshAllToolStripMenuItem"
        Me.RefreshAllToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.RefreshAllToolStripMenuItem.Text = "Refresh All"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(163, 6)
        '
        'NewScenarioTVRCM
        '
        Me.NewScenarioTVRCM.Image = Global.PPS.My.Resources.Resources.checked
        Me.NewScenarioTVRCM.Name = "NewScenarioTVRCM"
        Me.NewScenarioTVRCM.Size = New System.Drawing.Size(166, 22)
        Me.NewScenarioTVRCM.Text = "New Scenario"
        '
        'NewConstraintToolStripMenuItem
        '
        Me.NewConstraintToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.favicon_232_
        Me.NewConstraintToolStripMenuItem.Name = "NewConstraintToolStripMenuItem"
        Me.NewConstraintToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.NewConstraintToolStripMenuItem.Text = "New Target"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(163, 6)
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.imageres_89
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'inputsDGVsRightClickMenu
        '
        Me.inputsDGVsRightClickMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddConstraintToolStripMenuItem, Me.DeleteConstraintToolStripMenuItem, Me.RefreshScenarioToolStripMenuItem, Me.ToolStripSeparator4, Me.SerieMGTRCMBT, Me.ToolStripSeparator5, Me.CopyValueRightBT})
        Me.inputsDGVsRightClickMenu.Name = "inputsDGVsRightClickMenu"
        Me.inputsDGVsRightClickMenu.Size = New System.Drawing.Size(166, 126)
        '
        'AddConstraintToolStripMenuItem
        '
        Me.AddConstraintToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.favicon_232_
        Me.AddConstraintToolStripMenuItem.Name = "AddConstraintToolStripMenuItem"
        Me.AddConstraintToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.AddConstraintToolStripMenuItem.Text = "Add Target"
        '
        'DeleteConstraintToolStripMenuItem
        '
        Me.DeleteConstraintToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.imageres_89
        Me.DeleteConstraintToolStripMenuItem.Name = "DeleteConstraintToolStripMenuItem"
        Me.DeleteConstraintToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.DeleteConstraintToolStripMenuItem.Text = "Delete Target"
        '
        'RefreshScenarioToolStripMenuItem
        '
        Me.RefreshScenarioToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.Refresh2
        Me.RefreshScenarioToolStripMenuItem.Name = "RefreshScenarioToolStripMenuItem"
        Me.RefreshScenarioToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.RefreshScenarioToolStripMenuItem.Text = "Refresh Scenario"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(162, 6)
        '
        'SerieMGTRCMBT
        '
        Me.SerieMGTRCMBT.Image = Global.PPS.My.Resources.Resources.favicon_239_
        Me.SerieMGTRCMBT.Name = "SerieMGTRCMBT"
        Me.SerieMGTRCMBT.Size = New System.Drawing.Size(165, 22)
        Me.SerieMGTRCMBT.Text = "Display Options"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(162, 6)
        '
        'CopyValueRightBT
        '
        Me.CopyValueRightBT.Name = "CopyValueRightBT"
        Me.CopyValueRightBT.Size = New System.Drawing.Size(165, 22)
        Me.CopyValueRightBT.Text = "Copy Value Right"
        '
        'TVImageList
        '
        Me.TVImageList.ImageStream = CType(resources.GetObject("TVImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.TVImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.TVImageList.Images.SetKeyName(0, "Scenario.ico")
        Me.TVImageList.Images.SetKeyName(1, "favicon(232).ico")
        '
        'EntitiesTVImageList
        '
        Me.EntitiesTVImageList.ImageStream = CType(resources.GetObject("EntitiesTVImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.EntitiesTVImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.EntitiesTVImageList.Images.SetKeyName(0, "favicon(81).ico")
        Me.EntitiesTVImageList.Images.SetKeyName(1, "favicon(110).ico")
        '
        'VersionsTVIcons
        '
        Me.VersionsTVIcons.ImageStream = CType(resources.GetObject("VersionsTVIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.VersionsTVIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.VersionsTVIcons.Images.SetKeyName(0, "DB Grey.png")
        Me.VersionsTVIcons.Images.SetKeyName(1, "favicon(81).ico")
        Me.VersionsTVIcons.Images.SetKeyName(2, "icons-blue.png")
        '
        'ExportTVImageList
        '
        Me.ExportTVImageList.ImageStream = CType(resources.GetObject("ExportTVImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ExportTVImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ExportTVImageList.Images.SetKeyName(0, "favicon(196).ico")
        Me.ExportTVImageList.Images.SetKeyName(1, "favicon(111).ico")
        '
        'BigArrow
        '
        Me.BigArrow.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        Me.BigArrow.ImageSize = New System.Drawing.Size(16, 16)
        Me.BigArrow.TransparentColor = System.Drawing.Color.Transparent
        '
        'FModelingUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(664, 350)
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "FModelingUI"
        Me.Text = "Financial Modeling"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.LeftPanel.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.TreeviewRightClick.ResumeLayout(False)
        Me.inputsDGVsRightClickMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.CustomTabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents ButtonsImageList As System.Windows.Forms.ImageList
    Friend WithEvents LeftPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Refresh2BT As System.Windows.Forms.Button
    Friend WithEvents NewConstraintBT As System.Windows.Forms.Button
    Friend WithEvents NewScenarioBT As System.Windows.Forms.Button
    Friend WithEvents TreeviewRightClick As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents NewScenarioTVRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewConstraintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ResfreshScenarioToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents inputsDGVsRightClickMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AddConstraintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteConstraintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshScenarioToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditConstraintRCTV As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TVImageList As System.Windows.Forms.ImageList
    Friend WithEvents scenarioTVPanel As System.Windows.Forms.Panel
    Friend WithEvents ButtonsBigImageList As System.Windows.Forms.ImageList
    Friend WithEvents LaunchConsoBT As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents VersionsTVpanel As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents EntitiesTVPanel As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents EntityTB As System.Windows.Forms.TextBox
    Friend WithEvents VersionTB As System.Windows.Forms.TextBox
    Public WithEvents EntitiesTVImageList As System.Windows.Forms.ImageList
    Friend WithEvents VersionsTVIcons As System.Windows.Forms.ImageList
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ExportsTVPanel As System.Windows.Forms.Panel
    Friend WithEvents EntitiesTV2Panel As System.Windows.Forms.Panel
    Friend WithEvents ExportMappingPanel As System.Windows.Forms.Panel
    Friend WithEvents ScenariosCBPanel As System.Windows.Forms.Panel
    Friend WithEvents ExportTVImageList As System.Windows.Forms.ImageList
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents BigArrow As System.Windows.Forms.ImageList
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents ReinjectionBT As System.Windows.Forms.Button
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditMappingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DisplayConsolidatedInputsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CopyValueRightBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SerieMGTRCMBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
End Class
