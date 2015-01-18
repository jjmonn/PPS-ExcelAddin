<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AlternativeScenariosUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AlternativeScenariosUI))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConfigurationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MainTabControl = New System.Windows.Forms.CustomTabControl()
        Me.InputsTab = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.EntitiesTVPanel = New System.Windows.Forms.Panel()
        Me.VersionsTVpanel = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.EntityTB = New System.Windows.Forms.TextBox()
        Me.VersionTB = New System.Windows.Forms.TextBox()
        Me.MarketPricesPanel = New System.Windows.Forms.Panel()
        Me.MarketPricesTVPanel = New System.Windows.Forms.Panel()
        Me.ComputeScenarioBT = New System.Windows.Forms.Button()
        Me.ButtonsImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.MarketPricesTB = New System.Windows.Forms.TextBox()
        Me.MainTab = New System.Windows.Forms.TabPage()
        Me.MainPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SensitivitiesTab = New System.Windows.Forms.TabPage()
        Me.SensitivitiesTabControl = New System.Windows.Forms.CustomTabControl()
        Me.CommitTabPage = New System.Windows.Forms.TabPage()
        Me.TVImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.EntitiesTVImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.VersionsTVIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.ReportsRCM = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SendToExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.MainTabControl.SuspendLayout()
        Me.InputsTab.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.MarketPricesPanel.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MainTab.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SensitivitiesTab.SuspendLayout()
        Me.ReportsRCM.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.ConfigurationToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(734, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'NewToolStripMenuItem
        '
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        Me.NewToolStripMenuItem.Size = New System.Drawing.Size(43, 20)
        Me.NewToolStripMenuItem.Text = "New"
        '
        'ConfigurationToolStripMenuItem
        '
        Me.ConfigurationToolStripMenuItem.Name = "ConfigurationToolStripMenuItem"
        Me.ConfigurationToolStripMenuItem.Size = New System.Drawing.Size(93, 20)
        Me.ConfigurationToolStripMenuItem.Text = "Configuration"
        '
        'MainTabControl
        '
        Me.MainTabControl.Controls.Add(Me.InputsTab)
        Me.MainTabControl.Controls.Add(Me.MainTab)
        Me.MainTabControl.Controls.Add(Me.SensitivitiesTab)
        Me.MainTabControl.Controls.Add(Me.CommitTabPage)
        Me.MainTabControl.DisplayStyle = System.Windows.Forms.TabStyle.Rounded
        '
        '
        '
        Me.MainTabControl.DisplayStyleProvider.BorderColor = System.Drawing.SystemColors.ControlDark
        Me.MainTabControl.DisplayStyleProvider.BorderColorHot = System.Drawing.SystemColors.ControlDark
        Me.MainTabControl.DisplayStyleProvider.BorderColorSelected = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.MainTabControl.DisplayStyleProvider.CloserColor = System.Drawing.Color.DarkGray
        Me.MainTabControl.DisplayStyleProvider.FocusTrack = False
        Me.MainTabControl.DisplayStyleProvider.HotTrack = True
        Me.MainTabControl.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.MainTabControl.DisplayStyleProvider.Opacity = 1.0!
        Me.MainTabControl.DisplayStyleProvider.Overlap = 0
        Me.MainTabControl.DisplayStyleProvider.Padding = New System.Drawing.Point(6, 3)
        Me.MainTabControl.DisplayStyleProvider.Radius = 10
        Me.MainTabControl.DisplayStyleProvider.ShowTabCloser = False
        Me.MainTabControl.DisplayStyleProvider.TextColor = System.Drawing.SystemColors.ControlText
        Me.MainTabControl.DisplayStyleProvider.TextColorDisabled = System.Drawing.SystemColors.ControlDark
        Me.MainTabControl.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText
        Me.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainTabControl.HotTrack = True
        Me.MainTabControl.Location = New System.Drawing.Point(0, 24)
        Me.MainTabControl.Name = "MainTabControl"
        Me.MainTabControl.SelectedIndex = 0
        Me.MainTabControl.Size = New System.Drawing.Size(734, 511)
        Me.MainTabControl.TabIndex = 9
        '
        'InputsTab
        '
        Me.InputsTab.BackColor = System.Drawing.SystemColors.Control
        Me.InputsTab.Controls.Add(Me.TableLayoutPanel1)
        Me.InputsTab.Location = New System.Drawing.Point(4, 23)
        Me.InputsTab.Name = "InputsTab"
        Me.InputsTab.Padding = New System.Windows.Forms.Padding(3)
        Me.InputsTab.Size = New System.Drawing.Size(726, 484)
        Me.InputsTab.TabIndex = 0
        Me.InputsTab.Text = "Inputs"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 4
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel6, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.EntitiesTVPanel, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.VersionsTVpanel, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label6, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.EntityTB, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.VersionTB, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.MarketPricesPanel, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.ComputeScenarioBT, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 3, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.MarketPricesTB, 2, 3)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 5
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(720, 478)
        Me.TableLayoutPanel1.TabIndex = 12
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Label4)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(360, 20)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(180, 28)
        Me.Panel6.TabIndex = 13
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 6)
        Me.Label4.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(110, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Market Prices Version"
        '
        'EntitiesTVPanel
        '
        Me.EntitiesTVPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.EntitiesTVPanel.Location = New System.Drawing.Point(183, 51)
        Me.EntitiesTVPanel.Name = "EntitiesTVPanel"
        Me.EntitiesTVPanel.Size = New System.Drawing.Size(174, 364)
        Me.EntitiesTVPanel.TabIndex = 9
        '
        'VersionsTVpanel
        '
        Me.VersionsTVpanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VersionsTVpanel.Location = New System.Drawing.Point(3, 51)
        Me.VersionsTVpanel.Name = "VersionsTVpanel"
        Me.VersionsTVpanel.Size = New System.Drawing.Size(174, 364)
        Me.VersionsTVpanel.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(183, 26)
        Me.Label6.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Scope"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 26)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Base Scenario"
        '
        'EntityTB
        '
        Me.EntityTB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.EntityTB.Enabled = False
        Me.EntityTB.Location = New System.Drawing.Point(183, 421)
        Me.EntityTB.Name = "EntityTB"
        Me.EntityTB.Size = New System.Drawing.Size(174, 20)
        Me.EntityTB.TabIndex = 15
        '
        'VersionTB
        '
        Me.VersionTB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VersionTB.Enabled = False
        Me.VersionTB.Location = New System.Drawing.Point(3, 421)
        Me.VersionTB.Name = "VersionTB"
        Me.VersionTB.Size = New System.Drawing.Size(174, 20)
        Me.VersionTB.TabIndex = 16
        '
        'MarketPricesPanel
        '
        Me.MarketPricesPanel.Controls.Add(Me.MarketPricesTVPanel)
        Me.MarketPricesPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MarketPricesPanel.Location = New System.Drawing.Point(363, 51)
        Me.MarketPricesPanel.Name = "MarketPricesPanel"
        Me.MarketPricesPanel.Size = New System.Drawing.Size(174, 364)
        Me.MarketPricesPanel.TabIndex = 17
        '
        'MarketPricesTVPanel
        '
        Me.MarketPricesTVPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MarketPricesTVPanel.Location = New System.Drawing.Point(0, 0)
        Me.MarketPricesTVPanel.Name = "MarketPricesTVPanel"
        Me.MarketPricesTVPanel.Size = New System.Drawing.Size(174, 364)
        Me.MarketPricesTVPanel.TabIndex = 10
        '
        'ComputeScenarioBT
        '
        Me.ComputeScenarioBT.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.ComputeScenarioBT.FlatAppearance.BorderSize = 0
        Me.ComputeScenarioBT.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonFace
        Me.ComputeScenarioBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ComputeScenarioBT.ImageKey = "Report.png"
        Me.ComputeScenarioBT.ImageList = Me.ButtonsImageList
        Me.ComputeScenarioBT.Location = New System.Drawing.Point(543, 23)
        Me.ComputeScenarioBT.Name = "ComputeScenarioBT"
        Me.ComputeScenarioBT.Size = New System.Drawing.Size(152, 22)
        Me.ComputeScenarioBT.TabIndex = 3
        Me.ComputeScenarioBT.Text = "Compute Scenario"
        Me.ComputeScenarioBT.UseVisualStyleBackColor = True
        '
        'ButtonsImageList
        '
        Me.ButtonsImageList.ImageStream = CType(resources.GetObject("ButtonsImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonsImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonsImageList.Images.SetKeyName(0, "imageres_89.ico")
        Me.ButtonsImageList.Images.SetKeyName(1, "favicon(236).ico")
        Me.ButtonsImageList.Images.SetKeyName(2, "Refresh2.png")
        Me.ButtonsImageList.Images.SetKeyName(3, "Target zoomed.png")
        Me.ButtonsImageList.Images.SetKeyName(4, "Report.png")
        Me.ButtonsImageList.Images.SetKeyName(5, "favicon(187).ico")
        Me.ButtonsImageList.Images.SetKeyName(6, "favicon(196).ico")
        Me.ButtonsImageList.Images.SetKeyName(7, "add blue.jpg")
        Me.ButtonsImageList.Images.SetKeyName(8, "folder 2 ctrl bgd.png")
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.NumericUpDown1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(543, 51)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(174, 364)
        Me.Panel1.TabIndex = 18
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Location = New System.Drawing.Point(21, 41)
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(56, 20)
        Me.NumericUpDown1.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Variance"
        '
        'MarketPricesTB
        '
        Me.MarketPricesTB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MarketPricesTB.Enabled = False
        Me.MarketPricesTB.Location = New System.Drawing.Point(363, 421)
        Me.MarketPricesTB.Name = "MarketPricesTB"
        Me.MarketPricesTB.Size = New System.Drawing.Size(174, 20)
        Me.MarketPricesTB.TabIndex = 16
        '
        'MainTab
        '
        Me.MainTab.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.MainTab.Controls.Add(Me.MainPanel)
        Me.MainTab.Controls.Add(Me.Panel2)
        Me.MainTab.Location = New System.Drawing.Point(4, 23)
        Me.MainTab.Name = "MainTab"
        Me.MainTab.Padding = New System.Windows.Forms.Padding(3)
        Me.MainTab.Size = New System.Drawing.Size(726, 484)
        Me.MainTab.TabIndex = 1
        Me.MainTab.Text = "Alternative Scenario"
        '
        'MainPanel
        '
        Me.MainPanel.ColumnCount = 2
        Me.MainPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.MainPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainPanel.Location = New System.Drawing.Point(3, 41)
        Me.MainPanel.Name = "MainPanel"
        Me.MainPanel.RowCount = 1
        Me.MainPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 440.0!))
        Me.MainPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 440.0!))
        Me.MainPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 440.0!))
        Me.MainPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 440.0!))
        Me.MainPanel.Size = New System.Drawing.Size(720, 440)
        Me.MainPanel.TabIndex = 2
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.Panel2.Controls.Add(Me.TableLayoutPanel2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(720, 38)
        Me.Panel2.TabIndex = 1
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Label5, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Label3, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(720, 38)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(363, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(354, 38)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Alternative Scenario"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(354, 38)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Base Scenario"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SensitivitiesTab
        '
        Me.SensitivitiesTab.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.SensitivitiesTab.Controls.Add(Me.SensitivitiesTabControl)
        Me.SensitivitiesTab.Location = New System.Drawing.Point(4, 23)
        Me.SensitivitiesTab.Name = "SensitivitiesTab"
        Me.SensitivitiesTab.Padding = New System.Windows.Forms.Padding(3)
        Me.SensitivitiesTab.Size = New System.Drawing.Size(726, 484)
        Me.SensitivitiesTab.TabIndex = 2
        Me.SensitivitiesTab.Text = "Sensitivities"
        '
        'SensitivitiesTabControl
        '
        Me.SensitivitiesTabControl.DisplayStyle = System.Windows.Forms.TabStyle.Rounded
        '
        '
        '
        Me.SensitivitiesTabControl.DisplayStyleProvider.BorderColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.SensitivitiesTabControl.DisplayStyleProvider.BorderColorHot = System.Drawing.SystemColors.GradientActiveCaption
        Me.SensitivitiesTabControl.DisplayStyleProvider.BorderColorSelected = System.Drawing.SystemColors.ActiveCaption
        Me.SensitivitiesTabControl.DisplayStyleProvider.CloserColor = System.Drawing.Color.Crimson
        Me.SensitivitiesTabControl.DisplayStyleProvider.CloserColorActive = System.Drawing.Color.Crimson
        Me.SensitivitiesTabControl.DisplayStyleProvider.FocusColor = System.Drawing.Color.Black
        Me.SensitivitiesTabControl.DisplayStyleProvider.FocusTrack = False
        Me.SensitivitiesTabControl.DisplayStyleProvider.HotTrack = True
        Me.SensitivitiesTabControl.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SensitivitiesTabControl.DisplayStyleProvider.Opacity = 1.0!
        Me.SensitivitiesTabControl.DisplayStyleProvider.Overlap = 0
        Me.SensitivitiesTabControl.DisplayStyleProvider.Padding = New System.Drawing.Point(6, 3)
        Me.SensitivitiesTabControl.DisplayStyleProvider.Radius = 10
        Me.SensitivitiesTabControl.DisplayStyleProvider.ShowTabCloser = True
        Me.SensitivitiesTabControl.DisplayStyleProvider.TextColor = System.Drawing.SystemColors.ControlText
        Me.SensitivitiesTabControl.DisplayStyleProvider.TextColorDisabled = System.Drawing.SystemColors.ControlDark
        Me.SensitivitiesTabControl.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText
        Me.SensitivitiesTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SensitivitiesTabControl.HotTrack = True
        Me.SensitivitiesTabControl.Location = New System.Drawing.Point(3, 3)
        Me.SensitivitiesTabControl.Name = "SensitivitiesTabControl"
        Me.SensitivitiesTabControl.SelectedIndex = 0
        Me.SensitivitiesTabControl.Size = New System.Drawing.Size(720, 478)
        Me.SensitivitiesTabControl.TabIndex = 8
        '
        'CommitTabPage
        '
        Me.CommitTabPage.BackColor = System.Drawing.SystemColors.Control
        Me.CommitTabPage.Location = New System.Drawing.Point(4, 23)
        Me.CommitTabPage.Name = "CommitTabPage"
        Me.CommitTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.CommitTabPage.Size = New System.Drawing.Size(726, 484)
        Me.CommitTabPage.TabIndex = 3
        Me.CommitTabPage.Text = "Commit Scenario"
        '
        'TVImageList
        '
        Me.TVImageList.ImageStream = CType(resources.GetObject("TVImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.TVImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.TVImageList.Images.SetKeyName(0, "folder 2.png")
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
        'ReportsRCM
        '
        Me.ReportsRCM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SendToExcelToolStripMenuItem})
        Me.ReportsRCM.Name = "ContextMenuStrip1"
        Me.ReportsRCM.Size = New System.Drawing.Size(144, 26)
        '
        'SendToExcelToolStripMenuItem
        '
        Me.SendToExcelToolStripMenuItem.Name = "SendToExcelToolStripMenuItem"
        Me.SendToExcelToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.SendToExcelToolStripMenuItem.Text = "Send to Excel"
        '
        'AlternativeScenariosUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(734, 535)
        Me.Controls.Add(Me.MainTabControl)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "AlternativeScenariosUI"
        Me.Text = "Market Prices Scenarios Modeling"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.MainTabControl.ResumeLayout(False)
        Me.InputsTab.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.MarketPricesPanel.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MainTab.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.SensitivitiesTab.ResumeLayout(False)
        Me.ReportsRCM.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents NewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConfigurationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MainTabControl As System.Windows.Forms.CustomTabControl
    Friend WithEvents InputsTab As System.Windows.Forms.TabPage
    Friend WithEvents MainTab As System.Windows.Forms.TabPage
    Friend WithEvents SensitivitiesTab As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ComputeScenarioBT As System.Windows.Forms.Button
    Friend WithEvents EntitiesTVPanel As System.Windows.Forms.Panel
    Friend WithEvents VersionsTVpanel As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents EntityTB As System.Windows.Forms.TextBox
    Friend WithEvents VersionTB As System.Windows.Forms.TextBox
    Friend WithEvents MarketPricesTB As System.Windows.Forms.TextBox
    Friend WithEvents MarketPricesPanel As System.Windows.Forms.Panel
    Friend WithEvents ButtonsImageList As System.Windows.Forms.ImageList
    Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents MarketPricesTVPanel As System.Windows.Forms.Panel
    Friend WithEvents TVImageList As System.Windows.Forms.ImageList
    Public WithEvents EntitiesTVImageList As System.Windows.Forms.ImageList
    Friend WithEvents VersionsTVIcons As System.Windows.Forms.ImageList
    Friend WithEvents CommitTabPage As System.Windows.Forms.TabPage
    Friend WithEvents SensitivitiesTabControl As System.Windows.Forms.CustomTabControl
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents MainPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ReportsRCM As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SendToExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
