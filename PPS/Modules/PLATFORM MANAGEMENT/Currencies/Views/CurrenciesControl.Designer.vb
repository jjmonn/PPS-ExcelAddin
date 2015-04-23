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
        Me.DeleteVersionRCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.dgvRCM = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.expand_periods = New System.Windows.Forms.ToolStripMenuItem()
        Me.collapse_periods = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.CopyRateDownToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.rates_version_TB = New System.Windows.Forms.TextBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.CurrenciesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddNewCurrencyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.chart_button = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportFromExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.currenciesRCMenu.SuspendLayout()
        Me.VersionsRCMenu.SuspendLayout()
        Me.dgvRCM.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.SuspendLayout()
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
        Me.VersionsRCMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.select_version, Me.ToolStripSeparator2, Me.AddRatesVersionRCM, Me.AddFolderRCM, Me.DeleteVersionRCM})
        Me.VersionsRCMenu.Name = "VersionsRCMenu"
        Me.VersionsRCMenu.Size = New System.Drawing.Size(148, 98)
        '
        'select_version
        '
        Me.select_version.Image = Global.PPS.My.Resources.Resources.checked
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
        Me.AddRatesVersionRCM.Image = Global.PPS.My.Resources.Resources.add21
        Me.AddRatesVersionRCM.Name = "AddRatesVersionRCM"
        Me.AddRatesVersionRCM.Size = New System.Drawing.Size(147, 22)
        Me.AddRatesVersionRCM.Text = "Add Version"
        '
        'AddFolderRCM
        '
        Me.AddFolderRCM.Image = Global.PPS.My.Resources.Resources.folder2
        Me.AddFolderRCM.Name = "AddFolderRCM"
        Me.AddFolderRCM.Size = New System.Drawing.Size(147, 22)
        Me.AddFolderRCM.Text = "Add Folder"
        '
        'DeleteVersionRCM
        '
        Me.DeleteVersionRCM.Image = Global.PPS.My.Resources.Resources.imageres_89
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
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.TableLayoutPanel5, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.SplitContainer1, 0, 1)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 2
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.831933!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.16807!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(580, 476)
        Me.TableLayoutPanel4.TabIndex = 5
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 2
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 382.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 198.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.TableLayoutPanel3, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.MenuStrip1, 0, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel5.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(580, 23)
        Me.TableLayoutPanel5.TabIndex = 2
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 92.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 336.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.Label2, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.rates_version_TB, 1, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(383, 1)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(196, 21)
        Me.TableLayoutPanel3.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 5)
        Me.Label2.Margin = New System.Windows.Forms.Padding(5, 5, 3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Current Version"
        '
        'rates_version_TB
        '
        Me.rates_version_TB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rates_version_TB.Enabled = False
        Me.rates_version_TB.Location = New System.Drawing.Point(93, 1)
        Me.rates_version_TB.Margin = New System.Windows.Forms.Padding(1)
        Me.rates_version_TB.Name = "rates_version_TB"
        Me.rates_version_TB.Size = New System.Drawing.Size(334, 20)
        Me.rates_version_TB.TabIndex = 1
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CurrenciesToolStripMenuItem, Me.chart_button, Me.ImportFromExcelToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(382, 23)
        Me.MenuStrip1.TabIndex = 5
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'CurrenciesToolStripMenuItem
        '
        Me.CurrenciesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddNewCurrencyToolStripMenuItem, Me.ToolStripSeparator1})
        Me.CurrenciesToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.symbol_dollar_euro
        Me.CurrenciesToolStripMenuItem.Name = "CurrenciesToolStripMenuItem"
        Me.CurrenciesToolStripMenuItem.Size = New System.Drawing.Size(91, 19)
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
        Me.chart_button.Name = "chart_button"
        Me.chart_button.Size = New System.Drawing.Size(48, 19)
        Me.chart_button.Text = "Chart"
        '
        'ImportFromExcelToolStripMenuItem
        '
        Me.ImportFromExcelToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.excel_blue2
        Me.ImportFromExcelToolStripMenuItem.Name = "ImportFromExcelToolStripMenuItem"
        Me.ImportFromExcelToolStripMenuItem.Size = New System.Drawing.Size(129, 19)
        Me.ImportFromExcelToolStripMenuItem.Text = "Import from Excel"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BackColor = System.Drawing.SystemColors.Control
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 23)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TableLayoutPanel2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(580, 453)
        Me.SplitContainer1.SplitterDistance = 88
        Me.SplitContainer1.SplitterWidth = 2
        Me.SplitContainer1.TabIndex = 5
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(88, 453)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.SplitContainer2.Size = New System.Drawing.Size(490, 453)
        Me.SplitContainer2.SplitterDistance = 274
        Me.SplitContainer2.SplitterWidth = 1
        Me.SplitContainer2.TabIndex = 2
        '
        'CurrenciesControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel4)
        Me.Name = "CurrenciesControl"
        Me.Size = New System.Drawing.Size(580, 476)
        Me.currenciesRCMenu.ResumeLayout(False)
        Me.VersionsRCMenu.ResumeLayout(False)
        Me.dgvRCM.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
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
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents TableLayoutPanel5 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents rates_version_TB As System.Windows.Forms.TextBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents CurrenciesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddNewCurrencyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents chart_button As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImportFromExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
