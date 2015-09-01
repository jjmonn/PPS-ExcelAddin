<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CurrenciesControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CurrenciesControl))
        Me.MenuButtonIL = New System.Windows.Forms.ImageList(Me.components)
        Me.ratesVersionsIL = New System.Windows.Forms.ImageList(Me.components)
        Me.currenciesRCMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddCurrencyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteCurrencyToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.VersionsRCMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.select_version = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.AddRatesVersionRCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddFolderRCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.DeleteVersionRCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.dgvRCM = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.expand_periods = New System.Windows.Forms.ToolStripMenuItem()
        Me.collapse_periods = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.CopyRateDownToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.DisplayRatesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.CreateFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreateVersionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CurrenciesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddNewCurrencyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.chart_button = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportFromExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rates_version_TB = New System.Windows.Forms.TextBox()
        Me.VersionLabel = New System.Windows.Forms.Label()
        Me.currenciesRCMenu.SuspendLayout()
        Me.VersionsRCMenu.SuspendLayout()
        Me.dgvRCM.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuButtonIL
        '
        Me.MenuButtonIL.ImageStream = CType(resources.GetObject("MenuButtonIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.MenuButtonIL.TransparentColor = System.Drawing.Color.Transparent
        Me.MenuButtonIL.Images.SetKeyName(0, "expand right.png")
        Me.MenuButtonIL.Images.SetKeyName(1, "expandleft.png")
        Me.MenuButtonIL.Images.SetKeyName(2, "favicon(120).ico")
        Me.MenuButtonIL.Images.SetKeyName(3, "favicon(125).ico")
        Me.MenuButtonIL.Images.SetKeyName(4, "favicon(217).ico")
        Me.MenuButtonIL.Images.SetKeyName(5, "favicon(126).ico")
        '
        'ratesVersionsIL
        '
        Me.ratesVersionsIL.ImageStream = CType(resources.GetObject("ratesVersionsIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ratesVersionsIL.TransparentColor = System.Drawing.Color.Transparent
        Me.ratesVersionsIL.Images.SetKeyName(0, "favicon(149).ico")
        Me.ratesVersionsIL.Images.SetKeyName(1, "favicon(81).ico")
        Me.ratesVersionsIL.Images.SetKeyName(2, "imageres_157.ico")
        '
        'currenciesRCMenu
        '
        Me.currenciesRCMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddCurrencyToolStripMenuItem, Me.DeleteCurrencyToolStripMenuItem1})
        Me.currenciesRCMenu.Name = "currenciesRCMenu"
        Me.currenciesRCMenu.Size = New System.Drawing.Size(159, 48)
        '
        'AddCurrencyToolStripMenuItem
        '
        Me.AddCurrencyToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.symbol_dollar_euro_add
        Me.AddCurrencyToolStripMenuItem.Name = "AddCurrencyToolStripMenuItem"
        Me.AddCurrencyToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.AddCurrencyToolStripMenuItem.Text = "Add Currency"
        '
        'DeleteCurrencyToolStripMenuItem1
        '
        Me.DeleteCurrencyToolStripMenuItem1.Image = Global.PPS.My.Resources.Resources.symbol_dollar_euro_delete
        Me.DeleteCurrencyToolStripMenuItem1.Name = "DeleteCurrencyToolStripMenuItem1"
        Me.DeleteCurrencyToolStripMenuItem1.Size = New System.Drawing.Size(158, 22)
        Me.DeleteCurrencyToolStripMenuItem1.Text = "Delete Currency"
        '
        'VersionsRCMenu
        '
        Me.VersionsRCMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.select_version, Me.ToolStripSeparator2, Me.AddRatesVersionRCM, Me.AddFolderRCM, Me.ToolStripSeparator6, Me.DeleteVersionRCM})
        Me.VersionsRCMenu.Name = "VersionsRCMenu"
        Me.VersionsRCMenu.Size = New System.Drawing.Size(148, 104)
        '
        'select_version
        '
        Me.select_version.Image = Global.PPS.My.Resources.Resources.config_circle_green
        Me.select_version.Name = "select_version"
        Me.select_version.Size = New System.Drawing.Size(147, 22)
        Me.select_version.Text = "Select Version"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(144, 6)
        '
        'AddRatesVersionRCM
        '
        Me.AddRatesVersionRCM.Image = Global.PPS.My.Resources.Resources.elements3_add
        Me.AddRatesVersionRCM.Name = "AddRatesVersionRCM"
        Me.AddRatesVersionRCM.Size = New System.Drawing.Size(147, 22)
        Me.AddRatesVersionRCM.Text = "Add Version"
        '
        'AddFolderRCM
        '
        Me.AddFolderRCM.Image = Global.PPS.My.Resources.Resources.folder_open_add
        Me.AddFolderRCM.Name = "AddFolderRCM"
        Me.AddFolderRCM.Size = New System.Drawing.Size(147, 22)
        Me.AddFolderRCM.Text = "Add Folder"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(144, 6)
        '
        'DeleteVersionRCM
        '
        Me.DeleteVersionRCM.Image = Global.PPS.My.Resources.Resources.elements3_delete
        Me.DeleteVersionRCM.Name = "DeleteVersionRCM"
        Me.DeleteVersionRCM.Size = New System.Drawing.Size(147, 22)
        Me.DeleteVersionRCM.Text = "Delete"
        '
        'dgvRCM
        '
        Me.dgvRCM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.expand_periods, Me.collapse_periods, Me.ToolStripSeparator3, Me.CopyRateDownToolStripMenuItem})
        Me.dgvRCM.Name = "dgvRCM"
        Me.dgvRCM.Size = New System.Drawing.Size(177, 76)
        '
        'expand_periods
        '
        Me.expand_periods.Image = Global.PPS.My.Resources.Resources.images
        Me.expand_periods.Name = "expand_periods"
        Me.expand_periods.Size = New System.Drawing.Size(176, 22)
        Me.expand_periods.Text = "Expand all Periods"
        '
        'collapse_periods
        '
        Me.collapse_periods.Name = "collapse_periods"
        Me.collapse_periods.Size = New System.Drawing.Size(176, 22)
        Me.collapse_periods.Text = "Collapse all Periods"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(173, 6)
        '
        'CopyRateDownToolStripMenuItem
        '
        Me.CopyRateDownToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.Download_
        Me.CopyRateDownToolStripMenuItem.Name = "CopyRateDownToolStripMenuItem"
        Me.CopyRateDownToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.CopyRateDownToolStripMenuItem.Text = "Copy Rate Down"
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 1
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.SplitContainer2, 0, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.TableLayoutPanel5, 0, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 2
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(580, 476)
        Me.TableLayoutPanel4.TabIndex = 5
        '
        'SplitContainer2
        '
        Me.SplitContainer2.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(3, 27)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.SplitContainer1)
        Me.SplitContainer2.Size = New System.Drawing.Size(574, 446)
        Me.SplitContainer2.SplitterDistance = 328
        Me.SplitContainer2.SplitterWidth = 1
        Me.SplitContainer2.TabIndex = 2
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BackColor = System.Drawing.SystemColors.Control
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.SplitContainer1.Size = New System.Drawing.Size(574, 328)
        Me.SplitContainer1.SplitterDistance = 87
        Me.SplitContainer1.SplitterWidth = 2
        Me.SplitContainer1.TabIndex = 5
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 3
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 97.02381!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.976191!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 205.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.MenuStrip1, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.Panel1, 2, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel5.Margin = New System.Windows.Forms.Padding(0, 0, 4, 0)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(576, 24)
        Me.TableLayoutPanel5.TabIndex = 2
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CurrenciesToolStripMenuItem, Me.ToolStripMenuItem2, Me.chart_button, Me.ImportFromExcelToolStripMenuItem, Me.ToolStripMenuItem1})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(359, 24)
        Me.MenuStrip1.TabIndex = 5
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DisplayRatesToolStripMenuItem, Me.ToolStripSeparator5, Me.CreateFolderToolStripMenuItem, Me.CreateVersionToolStripMenuItem, Me.ToolStripSeparator4, Me.DeleteToolStripMenuItem})
        Me.ToolStripMenuItem2.Image = Global.PPS.My.Resources.Resources.breakpoints
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(79, 20)
        Me.ToolStripMenuItem2.Text = "Versions"
        '
        'DisplayRatesToolStripMenuItem
        '
        Me.DisplayRatesToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.config_circle_green
        Me.DisplayRatesToolStripMenuItem.Name = "DisplayRatesToolStripMenuItem"
        Me.DisplayRatesToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.DisplayRatesToolStripMenuItem.Text = "Display Rates"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(149, 6)
        '
        'CreateFolderToolStripMenuItem
        '
        Me.CreateFolderToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.folder_open_add
        Me.CreateFolderToolStripMenuItem.Name = "CreateFolderToolStripMenuItem"
        Me.CreateFolderToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.CreateFolderToolStripMenuItem.Text = "Create Folder"
        '
        'CreateVersionToolStripMenuItem
        '
        Me.CreateVersionToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.elements3_add
        Me.CreateVersionToolStripMenuItem.Name = "CreateVersionToolStripMenuItem"
        Me.CreateVersionToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.CreateVersionToolStripMenuItem.Text = "Create Version"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(149, 6)
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.elements3_delete
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'CurrenciesToolStripMenuItem
        '
        Me.CurrenciesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddNewCurrencyToolStripMenuItem, Me.ToolStripSeparator1})
        Me.CurrenciesToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.symbol_dollar_euro
        Me.CurrenciesToolStripMenuItem.Name = "CurrenciesToolStripMenuItem"
        Me.CurrenciesToolStripMenuItem.Size = New System.Drawing.Size(91, 20)
        Me.CurrenciesToolStripMenuItem.Text = "Currencies"
        '
        'AddNewCurrencyToolStripMenuItem
        '
        Me.AddNewCurrencyToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.symbol_dollar_euro_add
        Me.AddNewCurrencyToolStripMenuItem.Name = "AddNewCurrencyToolStripMenuItem"
        Me.AddNewCurrencyToolStripMenuItem.Size = New System.Drawing.Size(174, 22)
        Me.AddNewCurrencyToolStripMenuItem.Text = "Add New Currency"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(171, 6)
        '
        'chart_button
        '
        Me.chart_button.Image = Global.PPS.My.Resources.Resources.chart2
        Me.chart_button.Name = "chart_button"
        Me.chart_button.Size = New System.Drawing.Size(64, 20)
        Me.chart_button.Text = "Chart"
        '
        'ImportFromExcelToolStripMenuItem
        '
        Me.ImportFromExcelToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.excel_blue2
        Me.ImportFromExcelToolStripMenuItem.Name = "ImportFromExcelToolStripMenuItem"
        Me.ImportFromExcelToolStripMenuItem.Size = New System.Drawing.Size(129, 20)
        Me.ImportFromExcelToolStripMenuItem.Text = "Import from Excel"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(12, 20)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.VersionLabel)
        Me.Panel1.Controls.Add(Me.rates_version_TB)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(370, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(206, 24)
        Me.Panel1.TabIndex = 6
        '
        'rates_version_TB
        '
        Me.rates_version_TB.Enabled = False
        Me.rates_version_TB.Location = New System.Drawing.Point(54, 3)
        Me.rates_version_TB.Margin = New System.Windows.Forms.Padding(1)
        Me.rates_version_TB.Name = "rates_version_TB"
        Me.rates_version_TB.Size = New System.Drawing.Size(130, 20)
        Me.rates_version_TB.TabIndex = 2
        '
        'VersionLabel
        '
        Me.VersionLabel.AutoSize = True
        Me.VersionLabel.Location = New System.Drawing.Point(8, 7)
        Me.VersionLabel.Name = "VersionLabel"
        Me.VersionLabel.Size = New System.Drawing.Size(42, 13)
        Me.VersionLabel.TabIndex = 3
        Me.VersionLabel.Text = "Version"
        '
        'CurrenciesControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Controls.Add(Me.TableLayoutPanel4)
        Me.Name = "CurrenciesControl"
        Me.Size = New System.Drawing.Size(580, 476)
        Me.currenciesRCMenu.ResumeLayout(False)
        Me.VersionsRCMenu.ResumeLayout(False)
        Me.dgvRCM.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MenuButtonIL As System.Windows.Forms.ImageList
    Friend WithEvents ratesVersionsIL As System.Windows.Forms.ImageList
    Friend WithEvents currenciesRCMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AddCurrencyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteCurrencyToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VersionsRCMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents select_version As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AddRatesVersionRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddFolderRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteVersionRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents dgvRCM As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents expand_periods As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents collapse_periods As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CopyRateDownToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents TableLayoutPanel5 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents CurrenciesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddNewCurrencyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents chart_button As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImportFromExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DisplayRatesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CreateFolderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CreateVersionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents VersionLabel As System.Windows.Forms.Label
    Friend WithEvents rates_version_TB As System.Windows.Forms.TextBox

End Class
