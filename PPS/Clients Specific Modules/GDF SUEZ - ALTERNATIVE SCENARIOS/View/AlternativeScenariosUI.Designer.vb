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
        Me.ConfTabControl = New System.Windows.Forms.CustomTabControl()
        Me.InputsTab = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.MarketPricesTB = New System.Windows.Forms.TextBox()
        Me.EntitiesTVPanel = New System.Windows.Forms.Panel()
        Me.VersionsTVpanel = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.EntityTB = New System.Windows.Forms.TextBox()
        Me.VersionTB = New System.Windows.Forms.TextBox()
        Me.MarketPricesPanel = New System.Windows.Forms.Panel()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.MarketPricesTVPanel = New System.Windows.Forms.Panel()
        Me.VarianceTB = New System.Windows.Forms.TextBox()
        Me.ComputeScenarioBT = New System.Windows.Forms.Button()
        Me.ButtonsImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.MainTab = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.CommitTab = New System.Windows.Forms.TabPage()
        Me.TVImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.EntitiesTVImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.VersionsTVIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.MenuStrip1.SuspendLayout()
        Me.ConfTabControl.SuspendLayout()
        Me.InputsTab.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.MarketPricesPanel.SuspendLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MainTab.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.SuspendLayout()
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
        'ConfTabControl
        '
        Me.ConfTabControl.Controls.Add(Me.InputsTab)
        Me.ConfTabControl.Controls.Add(Me.MainTab)
        Me.ConfTabControl.Controls.Add(Me.CommitTab)
        Me.ConfTabControl.DisplayStyle = System.Windows.Forms.TabStyle.Rounded
        '
        '
        '
        Me.ConfTabControl.DisplayStyleProvider.BorderColor = System.Drawing.SystemColors.ControlDark
        Me.ConfTabControl.DisplayStyleProvider.BorderColorHot = System.Drawing.SystemColors.ControlDark
        Me.ConfTabControl.DisplayStyleProvider.BorderColorSelected = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.ConfTabControl.DisplayStyleProvider.CloserColor = System.Drawing.Color.DarkGray
        Me.ConfTabControl.DisplayStyleProvider.FocusTrack = False
        Me.ConfTabControl.DisplayStyleProvider.HotTrack = True
        Me.ConfTabControl.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ConfTabControl.DisplayStyleProvider.Opacity = 1.0!
        Me.ConfTabControl.DisplayStyleProvider.Overlap = 0
        Me.ConfTabControl.DisplayStyleProvider.Padding = New System.Drawing.Point(6, 3)
        Me.ConfTabControl.DisplayStyleProvider.Radius = 10
        Me.ConfTabControl.DisplayStyleProvider.ShowTabCloser = False
        Me.ConfTabControl.DisplayStyleProvider.TextColor = System.Drawing.SystemColors.ControlText
        Me.ConfTabControl.DisplayStyleProvider.TextColorDisabled = System.Drawing.SystemColors.ControlDark
        Me.ConfTabControl.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText
        Me.ConfTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ConfTabControl.HotTrack = True
        Me.ConfTabControl.Location = New System.Drawing.Point(0, 24)
        Me.ConfTabControl.Name = "ConfTabControl"
        Me.ConfTabControl.SelectedIndex = 0
        Me.ConfTabControl.Size = New System.Drawing.Size(734, 511)
        Me.ConfTabControl.TabIndex = 9
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
        Me.TableLayoutPanel1.ColumnCount = 6
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.608034!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.9266!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.9266!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.21494!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.323833!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel6, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel5, 3, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.EntitiesTVPanel, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.VersionsTVpanel, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label6, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.EntityTB, 2, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.VersionTB, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.MarketPricesPanel, 3, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.ComputeScenarioBT, 4, 1)
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
        Me.Panel6.Location = New System.Drawing.Point(357, 20)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(273, 28)
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
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.MarketPricesTB)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(357, 418)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(273, 40)
        Me.Panel5.TabIndex = 12
        '
        'MarketPricesTB
        '
        Me.MarketPricesTB.Enabled = False
        Me.MarketPricesTB.Location = New System.Drawing.Point(4, 3)
        Me.MarketPricesTB.Name = "MarketPricesTB"
        Me.MarketPricesTB.Size = New System.Drawing.Size(156, 20)
        Me.MarketPricesTB.TabIndex = 16
        '
        'EntitiesTVPanel
        '
        Me.EntitiesTVPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.EntitiesTVPanel.Location = New System.Drawing.Point(194, 51)
        Me.EntitiesTVPanel.Name = "EntitiesTVPanel"
        Me.EntitiesTVPanel.Size = New System.Drawing.Size(158, 364)
        Me.EntitiesTVPanel.TabIndex = 9
        '
        'VersionsTVpanel
        '
        Me.VersionsTVpanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.VersionsTVpanel.Location = New System.Drawing.Point(28, 51)
        Me.VersionsTVpanel.Name = "VersionsTVpanel"
        Me.VersionsTVpanel.Size = New System.Drawing.Size(152, 364)
        Me.VersionsTVpanel.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(194, 26)
        Me.Label6.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Scope"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(28, 26)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Base Scenario"
        '
        'EntityTB
        '
        Me.EntityTB.Enabled = False
        Me.EntityTB.Location = New System.Drawing.Point(194, 421)
        Me.EntityTB.Name = "EntityTB"
        Me.EntityTB.Size = New System.Drawing.Size(158, 20)
        Me.EntityTB.TabIndex = 15
        '
        'VersionTB
        '
        Me.VersionTB.Enabled = False
        Me.VersionTB.Location = New System.Drawing.Point(28, 421)
        Me.VersionTB.Name = "VersionTB"
        Me.VersionTB.Size = New System.Drawing.Size(152, 20)
        Me.VersionTB.TabIndex = 16
        '
        'MarketPricesPanel
        '
        Me.MarketPricesPanel.Controls.Add(Me.NumericUpDown1)
        Me.MarketPricesPanel.Controls.Add(Me.Label1)
        Me.MarketPricesPanel.Controls.Add(Me.MarketPricesTVPanel)
        Me.MarketPricesPanel.Controls.Add(Me.VarianceTB)
        Me.MarketPricesPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MarketPricesPanel.Location = New System.Drawing.Point(360, 51)
        Me.MarketPricesPanel.Name = "MarketPricesPanel"
        Me.MarketPricesPanel.Size = New System.Drawing.Size(267, 364)
        Me.MarketPricesPanel.TabIndex = 17
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Location = New System.Drawing.Point(229, 44)
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(16, 20)
        Me.NumericUpDown1.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(182, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Variance"
        '
        'MarketPricesTVPanel
        '
        Me.MarketPricesTVPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.MarketPricesTVPanel.Location = New System.Drawing.Point(1, 3)
        Me.MarketPricesTVPanel.Name = "MarketPricesTVPanel"
        Me.MarketPricesTVPanel.Size = New System.Drawing.Size(156, 364)
        Me.MarketPricesTVPanel.TabIndex = 10
        '
        'VarianceTB
        '
        Me.VarianceTB.Location = New System.Drawing.Point(183, 44)
        Me.VarianceTB.Name = "VarianceTB"
        Me.VarianceTB.Size = New System.Drawing.Size(44, 20)
        Me.VarianceTB.TabIndex = 0
        '
        'ComputeScenarioBT
        '
        Me.ComputeScenarioBT.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.ComputeScenarioBT.FlatAppearance.BorderSize = 0
        Me.ComputeScenarioBT.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonFace
        Me.ComputeScenarioBT.ImageKey = "Report.png"
        Me.ComputeScenarioBT.ImageList = Me.ButtonsImageList
        Me.ComputeScenarioBT.Location = New System.Drawing.Point(633, 23)
        Me.ComputeScenarioBT.Name = "ComputeScenarioBT"
        Me.ComputeScenarioBT.Size = New System.Drawing.Size(56, 22)
        Me.ComputeScenarioBT.TabIndex = 3
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
        'MainTab
        '
        Me.MainTab.BackColor = System.Drawing.SystemColors.Control
        Me.MainTab.Controls.Add(Me.SplitContainer1)
        Me.MainTab.Location = New System.Drawing.Point(4, 23)
        Me.MainTab.Name = "MainTab"
        Me.MainTab.Padding = New System.Windows.Forms.Padding(3)
        Me.MainTab.Size = New System.Drawing.Size(726, 484)
        Me.MainTab.TabIndex = 1
        Me.MainTab.Text = "Alternative Scenario"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Size = New System.Drawing.Size(720, 478)
        Me.SplitContainer1.SplitterDistance = 356
        Me.SplitContainer1.TabIndex = 0
        '
        'CommitTab
        '
        Me.CommitTab.BackColor = System.Drawing.SystemColors.Control
        Me.CommitTab.Location = New System.Drawing.Point(4, 23)
        Me.CommitTab.Name = "CommitTab"
        Me.CommitTab.Padding = New System.Windows.Forms.Padding(3)
        Me.CommitTab.Size = New System.Drawing.Size(726, 484)
        Me.CommitTab.TabIndex = 2
        Me.CommitTab.Text = "Commit Scenario"
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
        'AlternativeScenariosUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(734, 535)
        Me.Controls.Add(Me.ConfTabControl)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "AlternativeScenariosUI"
        Me.Text = "Market Prices Scenarios Modeling"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ConfTabControl.ResumeLayout(False)
        Me.InputsTab.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.MarketPricesPanel.ResumeLayout(False)
        Me.MarketPricesPanel.PerformLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MainTab.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents NewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConfigurationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConfTabControl As System.Windows.Forms.CustomTabControl
    Friend WithEvents InputsTab As System.Windows.Forms.TabPage
    Friend WithEvents MainTab As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents CommitTab As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
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
    Friend WithEvents VarianceTB As System.Windows.Forms.TextBox
    Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents MarketPricesTVPanel As System.Windows.Forms.Panel
    Friend WithEvents TVImageList As System.Windows.Forms.ImageList
    Public WithEvents EntitiesTVImageList As System.Windows.Forms.ImageList
    Friend WithEvents VersionsTVIcons As System.Windows.Forms.ImageList
End Class
