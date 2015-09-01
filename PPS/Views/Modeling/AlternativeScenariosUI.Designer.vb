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
        Me.Label8 = New System.Windows.Forms.Label()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.MarketPricesTB = New System.Windows.Forms.TextBox()
        Me.SensitivitiesTab = New System.Windows.Forms.TabPage()
        Me.SensitivitiesTabControl = New System.Windows.Forms.CustomTabControl()
        Me.SensiTab = New System.Windows.Forms.TabPage()
        Me.VolumesTabControl = New System.Windows.Forms.CustomTabControl()
        Me.IncrRevTab = New System.Windows.Forms.TabPage()
        Me.RevenuesTabControl = New System.Windows.Forms.CustomTabControl()
        Me.NetResultTab = New System.Windows.Forms.TabPage()
        Me.NetResultTabControl = New System.Windows.Forms.CustomTabControl()
        Me.AlternativeScenarioTabPage = New System.Windows.Forms.TabPage()
        Me.MainPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.CommitTabPage = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ExportMappingPanel = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.ReinjectionBT = New System.Windows.Forms.Button()
        Me.AdjustmentsTVPanel = New System.Windows.Forms.Panel()
        Me.TVImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.VersionsTVIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.ReportsRCM = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SendToExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EntitiesTVImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.ChartsRCM2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SendToExcelToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.SetMaximumY1ValueToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SetMaximumY2ValueToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.MainTabControl.SuspendLayout()
        Me.InputsTab.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.MarketPricesPanel.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SensitivitiesTab.SuspendLayout()
        Me.SensitivitiesTabControl.SuspendLayout()
        Me.SensiTab.SuspendLayout()
        Me.IncrRevTab.SuspendLayout()
        Me.NetResultTab.SuspendLayout()
        Me.AlternativeScenarioTabPage.SuspendLayout()
        Me.CommitTabPage.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.ReportsRCM.SuspendLayout()
        Me.ChartsRCM2.SuspendLayout()
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
        Me.MainTabControl.Controls.Add(Me.SensitivitiesTab)
        Me.MainTabControl.Controls.Add(Me.AlternativeScenarioTabPage)
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
        Me.ButtonsImageList.Images.SetKeyName(5, "favicon(196).ico")
        Me.ButtonsImageList.Images.SetKeyName(6, "add blue.jpg")
        Me.ButtonsImageList.Images.SetKeyName(7, "folder 2 ctrl bgd.png")
        Me.ButtonsImageList.Images.SetKeyName(8, "1420498403_340208.ico")
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.NumericUpDown1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(543, 51)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(174, 364)
        Me.Panel1.TabIndex = 18
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(19, 45)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(15, 13)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "%"
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Location = New System.Drawing.Point(37, 42)
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
        'SensitivitiesTab
        '
        Me.SensitivitiesTab.BackColor = System.Drawing.SystemColors.Control
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
        Me.SensitivitiesTabControl.Controls.Add(Me.SensiTab)
        Me.SensitivitiesTabControl.Controls.Add(Me.IncrRevTab)
        Me.SensitivitiesTabControl.Controls.Add(Me.NetResultTab)
        '
        '
        '
        Me.SensitivitiesTabControl.DisplayStyleProvider.BorderColor = System.Drawing.SystemColors.ControlDark
        Me.SensitivitiesTabControl.DisplayStyleProvider.BorderColorHot = System.Drawing.SystemColors.ControlDark
        Me.SensitivitiesTabControl.DisplayStyleProvider.BorderColorSelected = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.SensitivitiesTabControl.DisplayStyleProvider.CloserColor = System.Drawing.Color.DarkGray
        Me.SensitivitiesTabControl.DisplayStyleProvider.FocusTrack = True
        Me.SensitivitiesTabControl.DisplayStyleProvider.HotTrack = True
        Me.SensitivitiesTabControl.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SensitivitiesTabControl.DisplayStyleProvider.Opacity = 1.0!
        Me.SensitivitiesTabControl.DisplayStyleProvider.Overlap = 0
        Me.SensitivitiesTabControl.DisplayStyleProvider.Padding = New System.Drawing.Point(6, 3)
        Me.SensitivitiesTabControl.DisplayStyleProvider.Radius = 2
        Me.SensitivitiesTabControl.DisplayStyleProvider.ShowTabCloser = False
        Me.SensitivitiesTabControl.DisplayStyleProvider.TextColor = System.Drawing.SystemColors.ControlText
        Me.SensitivitiesTabControl.DisplayStyleProvider.TextColorDisabled = System.Drawing.SystemColors.ControlDark
        Me.SensitivitiesTabControl.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText
        Me.SensitivitiesTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SensitivitiesTabControl.HotTrack = True
        Me.SensitivitiesTabControl.ItemSize = New System.Drawing.Size(80, 18)
        Me.SensitivitiesTabControl.Location = New System.Drawing.Point(3, 3)
        Me.SensitivitiesTabControl.Name = "SensitivitiesTabControl"
        Me.SensitivitiesTabControl.SelectedIndex = 0
        Me.SensitivitiesTabControl.Size = New System.Drawing.Size(720, 478)
        Me.SensitivitiesTabControl.TabIndex = 8
        '
        'SensiTab
        '
        Me.SensiTab.Controls.Add(Me.VolumesTabControl)
        Me.SensiTab.Location = New System.Drawing.Point(4, 23)
        Me.SensiTab.Name = "SensiTab"
        Me.SensiTab.Padding = New System.Windows.Forms.Padding(3)
        Me.SensiTab.Size = New System.Drawing.Size(712, 451)
        Me.SensiTab.TabIndex = 0
        Me.SensiTab.Text = "Volumes"
        Me.SensiTab.UseVisualStyleBackColor = True
        '
        'VolumesTabControl
        '
        Me.VolumesTabControl.DisplayStyle = System.Windows.Forms.TabStyle.Rounded
        '
        '
        '
        Me.VolumesTabControl.DisplayStyleProvider.BorderColor = System.Drawing.SystemColors.ControlDark
        Me.VolumesTabControl.DisplayStyleProvider.BorderColorHot = System.Drawing.SystemColors.ControlDark
        Me.VolumesTabControl.DisplayStyleProvider.BorderColorSelected = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.VolumesTabControl.DisplayStyleProvider.CloserColor = System.Drawing.Color.DarkGray
        Me.VolumesTabControl.DisplayStyleProvider.FocusTrack = False
        Me.VolumesTabControl.DisplayStyleProvider.HotTrack = True
        Me.VolumesTabControl.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.VolumesTabControl.DisplayStyleProvider.Opacity = 1.0!
        Me.VolumesTabControl.DisplayStyleProvider.Overlap = 0
        Me.VolumesTabControl.DisplayStyleProvider.Padding = New System.Drawing.Point(6, 3)
        Me.VolumesTabControl.DisplayStyleProvider.Radius = 10
        Me.VolumesTabControl.DisplayStyleProvider.ShowTabCloser = False
        Me.VolumesTabControl.DisplayStyleProvider.TextColor = System.Drawing.SystemColors.ControlText
        Me.VolumesTabControl.DisplayStyleProvider.TextColorDisabled = System.Drawing.SystemColors.ControlDark
        Me.VolumesTabControl.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText
        Me.VolumesTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VolumesTabControl.HotTrack = True
        Me.VolumesTabControl.Location = New System.Drawing.Point(3, 3)
        Me.VolumesTabControl.Name = "VolumesTabControl"
        Me.VolumesTabControl.SelectedIndex = 0
        Me.VolumesTabControl.Size = New System.Drawing.Size(706, 445)
        Me.VolumesTabControl.TabIndex = 8
        '
        'IncrRevTab
        '
        Me.IncrRevTab.Controls.Add(Me.RevenuesTabControl)
        Me.IncrRevTab.Location = New System.Drawing.Point(4, 23)
        Me.IncrRevTab.Name = "IncrRevTab"
        Me.IncrRevTab.Padding = New System.Windows.Forms.Padding(3)
        Me.IncrRevTab.Size = New System.Drawing.Size(712, 451)
        Me.IncrRevTab.TabIndex = 1
        Me.IncrRevTab.Text = "Revenues Impact"
        Me.IncrRevTab.UseVisualStyleBackColor = True
        '
        'RevenuesTabControl
        '
        Me.RevenuesTabControl.DisplayStyle = System.Windows.Forms.TabStyle.Rounded
        '
        '
        '
        Me.RevenuesTabControl.DisplayStyleProvider.BorderColor = System.Drawing.SystemColors.ControlDark
        Me.RevenuesTabControl.DisplayStyleProvider.BorderColorHot = System.Drawing.SystemColors.ControlDark
        Me.RevenuesTabControl.DisplayStyleProvider.BorderColorSelected = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.RevenuesTabControl.DisplayStyleProvider.CloserColor = System.Drawing.Color.DarkGray
        Me.RevenuesTabControl.DisplayStyleProvider.FocusTrack = False
        Me.RevenuesTabControl.DisplayStyleProvider.HotTrack = True
        Me.RevenuesTabControl.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RevenuesTabControl.DisplayStyleProvider.Opacity = 1.0!
        Me.RevenuesTabControl.DisplayStyleProvider.Overlap = 0
        Me.RevenuesTabControl.DisplayStyleProvider.Padding = New System.Drawing.Point(6, 3)
        Me.RevenuesTabControl.DisplayStyleProvider.Radius = 10
        Me.RevenuesTabControl.DisplayStyleProvider.ShowTabCloser = False
        Me.RevenuesTabControl.DisplayStyleProvider.TextColor = System.Drawing.SystemColors.ControlText
        Me.RevenuesTabControl.DisplayStyleProvider.TextColorDisabled = System.Drawing.SystemColors.ControlDark
        Me.RevenuesTabControl.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText
        Me.RevenuesTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RevenuesTabControl.HotTrack = True
        Me.RevenuesTabControl.Location = New System.Drawing.Point(3, 3)
        Me.RevenuesTabControl.Name = "RevenuesTabControl"
        Me.RevenuesTabControl.SelectedIndex = 0
        Me.RevenuesTabControl.Size = New System.Drawing.Size(706, 445)
        Me.RevenuesTabControl.TabIndex = 8
        '
        'NetResultTab
        '
        Me.NetResultTab.Controls.Add(Me.NetResultTabControl)
        Me.NetResultTab.Location = New System.Drawing.Point(4, 23)
        Me.NetResultTab.Name = "NetResultTab"
        Me.NetResultTab.Padding = New System.Windows.Forms.Padding(3)
        Me.NetResultTab.Size = New System.Drawing.Size(712, 451)
        Me.NetResultTab.TabIndex = 2
        Me.NetResultTab.Text = "Net Result Impact"
        Me.NetResultTab.UseVisualStyleBackColor = True
        '
        'NetResultTabControl
        '
        Me.NetResultTabControl.DisplayStyle = System.Windows.Forms.TabStyle.Rounded
        '
        '
        '
        Me.NetResultTabControl.DisplayStyleProvider.BorderColor = System.Drawing.SystemColors.ControlDark
        Me.NetResultTabControl.DisplayStyleProvider.BorderColorHot = System.Drawing.SystemColors.ControlDark
        Me.NetResultTabControl.DisplayStyleProvider.BorderColorSelected = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.NetResultTabControl.DisplayStyleProvider.CloserColor = System.Drawing.Color.DarkGray
        Me.NetResultTabControl.DisplayStyleProvider.FocusTrack = False
        Me.NetResultTabControl.DisplayStyleProvider.HotTrack = True
        Me.NetResultTabControl.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.NetResultTabControl.DisplayStyleProvider.Opacity = 1.0!
        Me.NetResultTabControl.DisplayStyleProvider.Overlap = 0
        Me.NetResultTabControl.DisplayStyleProvider.Padding = New System.Drawing.Point(6, 3)
        Me.NetResultTabControl.DisplayStyleProvider.Radius = 10
        Me.NetResultTabControl.DisplayStyleProvider.ShowTabCloser = False
        Me.NetResultTabControl.DisplayStyleProvider.TextColor = System.Drawing.SystemColors.ControlText
        Me.NetResultTabControl.DisplayStyleProvider.TextColorDisabled = System.Drawing.SystemColors.ControlDark
        Me.NetResultTabControl.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText
        Me.NetResultTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NetResultTabControl.HotTrack = True
        Me.NetResultTabControl.Location = New System.Drawing.Point(3, 3)
        Me.NetResultTabControl.Name = "NetResultTabControl"
        Me.NetResultTabControl.SelectedIndex = 0
        Me.NetResultTabControl.Size = New System.Drawing.Size(706, 445)
        Me.NetResultTabControl.TabIndex = 8
        '
        'AlternativeScenarioTabPage
        '
        Me.AlternativeScenarioTabPage.BackColor = System.Drawing.SystemColors.Control
        Me.AlternativeScenarioTabPage.Controls.Add(Me.MainPanel)
        Me.AlternativeScenarioTabPage.Location = New System.Drawing.Point(4, 23)
        Me.AlternativeScenarioTabPage.Name = "AlternativeScenarioTabPage"
        Me.AlternativeScenarioTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.AlternativeScenarioTabPage.Size = New System.Drawing.Size(726, 484)
        Me.AlternativeScenarioTabPage.TabIndex = 3
        Me.AlternativeScenarioTabPage.Text = "Alternative Scenario"
        '
        'MainPanel
        '
        Me.MainPanel.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.MainPanel.ColumnCount = 2
        Me.MainPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.MainPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainPanel.Location = New System.Drawing.Point(3, 3)
        Me.MainPanel.Name = "MainPanel"
        Me.MainPanel.RowCount = 4
        Me.MainPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 310.0!))
        Me.MainPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 310.0!))
        Me.MainPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 310.0!))
        Me.MainPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 310.0!))
        Me.MainPanel.Size = New System.Drawing.Size(720, 478)
        Me.MainPanel.TabIndex = 3
        '
        'CommitTabPage
        '
        Me.CommitTabPage.BackColor = System.Drawing.SystemColors.Control
        Me.CommitTabPage.Controls.Add(Me.TableLayoutPanel3)
        Me.CommitTabPage.Location = New System.Drawing.Point(4, 23)
        Me.CommitTabPage.Name = "CommitTabPage"
        Me.CommitTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.CommitTabPage.Size = New System.Drawing.Size(726, 484)
        Me.CommitTabPage.TabIndex = 4
        Me.CommitTabPage.Text = "Commit"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 7
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 66.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.31002!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.31002!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 56.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.29604!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.08392!))
        Me.TableLayoutPanel3.Controls.Add(Me.Label7, 1, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Label9, 3, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.ExportMappingPanel, 3, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.Label10, 5, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.ReinjectionBT, 5, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.AdjustmentsTVPanel, 1, 2)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 4
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(720, 478)
        Me.TableLayoutPanel3.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(69, 25)
        Me.Label7.Margin = New System.Windows.Forms.Padding(3, 0, 3, 5)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(73, 25)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "1. Choose an Adjustment"
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(236, 25)
        Me.Label9.Margin = New System.Windows.Forms.Padding(3, 0, 3, 5)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(102, 25)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "2. Adjust the Export Mapping"
        '
        'ExportMappingPanel
        '
        Me.ExportMappingPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExportMappingPanel.Location = New System.Drawing.Point(234, 60)
        Me.ExportMappingPanel.Margin = New System.Windows.Forms.Padding(1, 5, 1, 1)
        Me.ExportMappingPanel.Name = "ExportMappingPanel"
        Me.ExportMappingPanel.Size = New System.Drawing.Size(128, 392)
        Me.ExportMappingPanel.TabIndex = 3
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(422, 37)
        Me.Label10.Margin = New System.Windows.Forms.Padding(3, 0, 3, 5)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(191, 13)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "3. Commit Alternative Scenario Impacts"
        '
        'ReinjectionBT
        '
        Me.ReinjectionBT.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.ReinjectionBT.FlatAppearance.BorderSize = 0
        Me.ReinjectionBT.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonFace
        Me.ReinjectionBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ReinjectionBT.ImageKey = "1420498403_340208.ico"
        Me.ReinjectionBT.ImageList = Me.ButtonsImageList
        Me.ReinjectionBT.Location = New System.Drawing.Point(422, 58)
        Me.ReinjectionBT.Name = "ReinjectionBT"
        Me.ReinjectionBT.Size = New System.Drawing.Size(88, 30)
        Me.ReinjectionBT.TabIndex = 9
        Me.ReinjectionBT.Text = "Export"
        Me.ReinjectionBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ReinjectionBT.UseVisualStyleBackColor = True
        '
        'AdjustmentsTVPanel
        '
        Me.AdjustmentsTVPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AdjustmentsTVPanel.Location = New System.Drawing.Point(67, 60)
        Me.AdjustmentsTVPanel.Margin = New System.Windows.Forms.Padding(1, 5, 1, 1)
        Me.AdjustmentsTVPanel.Name = "AdjustmentsTVPanel"
        Me.AdjustmentsTVPanel.Size = New System.Drawing.Size(128, 392)
        Me.AdjustmentsTVPanel.TabIndex = 10
        '
        'TVImageList
        '
        Me.TVImageList.ImageStream = CType(resources.GetObject("TVImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.TVImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.TVImageList.Images.SetKeyName(0, "folder 2.png")
        Me.TVImageList.Images.SetKeyName(1, "favicon(232).ico")
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
        Me.SendToExcelToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.excel_2
        Me.SendToExcelToolStripMenuItem.Name = "SendToExcelToolStripMenuItem"
        Me.SendToExcelToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.SendToExcelToolStripMenuItem.Text = "Send to Excel"
        '
        'EntitiesTVImageList
        '
        Me.EntitiesTVImageList.ImageStream = CType(resources.GetObject("EntitiesTVImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.EntitiesTVImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.EntitiesTVImageList.Images.SetKeyName(0, "favicon(81).ico")
        Me.EntitiesTVImageList.Images.SetKeyName(1, "favicon(110).ico")
        '
        'ChartsRCM2
        '
        Me.ChartsRCM2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SendToExcelToolStripMenuItem1, Me.ToolStripSeparator1, Me.SetMaximumY1ValueToolStripMenuItem, Me.SetMaximumY2ValueToolStripMenuItem})
        Me.ChartsRCM2.Name = "ContextMenuStrip1"
        Me.ChartsRCM2.Size = New System.Drawing.Size(196, 76)
        '
        'SendToExcelToolStripMenuItem1
        '
        Me.SendToExcelToolStripMenuItem1.Image = Global.PPS.My.Resources.Resources.excel_2
        Me.SendToExcelToolStripMenuItem1.Name = "SendToExcelToolStripMenuItem1"
        Me.SendToExcelToolStripMenuItem1.Size = New System.Drawing.Size(195, 22)
        Me.SendToExcelToolStripMenuItem1.Text = "Send to Excel"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(192, 6)
        '
        'SetMaximumY1ValueToolStripMenuItem
        '
        Me.SetMaximumY1ValueToolStripMenuItem.Name = "SetMaximumY1ValueToolStripMenuItem"
        Me.SetMaximumY1ValueToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.SetMaximumY1ValueToolStripMenuItem.Text = "Set Maximum Y1 Value"
        '
        'SetMaximumY2ValueToolStripMenuItem
        '
        Me.SetMaximumY2ValueToolStripMenuItem.Name = "SetMaximumY2ValueToolStripMenuItem"
        Me.SetMaximumY2ValueToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.SetMaximumY2ValueToolStripMenuItem.Text = "Set Maximum Y2 Value"
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
        Me.Text = "Alternative Scenarios Modeling"
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
        Me.SensitivitiesTab.ResumeLayout(False)
        Me.SensitivitiesTabControl.ResumeLayout(False)
        Me.SensiTab.ResumeLayout(False)
        Me.IncrRevTab.ResumeLayout(False)
        Me.NetResultTab.ResumeLayout(False)
        Me.AlternativeScenarioTabPage.ResumeLayout(False)
        Me.CommitTabPage.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.ReportsRCM.ResumeLayout(False)
        Me.ChartsRCM2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents NewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConfigurationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MainTabControl As System.Windows.Forms.CustomTabControl
    Friend WithEvents InputsTab As System.Windows.Forms.TabPage
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
    Friend WithEvents VersionsTVIcons As System.Windows.Forms.ImageList
    Friend WithEvents SensitivitiesTabControl As System.Windows.Forms.CustomTabControl
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ReportsRCM As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SendToExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AlternativeScenarioTabPage As System.Windows.Forms.TabPage
    Friend WithEvents MainPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents CommitTabPage As System.Windows.Forms.TabPage
    Friend WithEvents SensiTab As System.Windows.Forms.TabPage
    Friend WithEvents IncrRevTab As System.Windows.Forms.TabPage
    Friend WithEvents NetResultTab As System.Windows.Forms.TabPage
    Friend WithEvents VolumesTabControl As System.Windows.Forms.CustomTabControl
    Friend WithEvents RevenuesTabControl As System.Windows.Forms.CustomTabControl
    Friend WithEvents NetResultTabControl As System.Windows.Forms.CustomTabControl
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ExportMappingPanel As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents ReinjectionBT As System.Windows.Forms.Button
    Friend WithEvents AdjustmentsTVPanel As System.Windows.Forms.Panel
    Public WithEvents EntitiesTVImageList As System.Windows.Forms.ImageList
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ChartsRCM2 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SendToExcelToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SetMaximumY1ValueToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SetMaximumY2ValueToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
End Class
