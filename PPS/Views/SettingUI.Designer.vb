<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SettingUI
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
        Dim DataGridLocalization1 As VIBlend.WinForms.DataGridView.DataGridLocalization = New VIBlend.WinForms.DataGridView.DataGridLocalization()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SettingUI))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.databasesCB = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PortTB = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ServerAddressTB = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.IDTB = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.VGroupBox1 = New VIBlend.WinForms.Controls.vGroupBox()
        Me.FormatsDGV = New VIBlend.WinForms.DataGridView.vDataGridView()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.CurrenciesCombobox = New VIBlend.WinForms.Controls.vComboBox()
        Me.VLabel1 = New VIBlend.WinForms.Controls.vLabel()
        Me.ButtonIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.ControlImages = New System.Windows.Forms.ImageList(Me.components)
        Me.ACFIcon = New System.Windows.Forms.ImageList(Me.components)
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.m_saveButton = New VIBlend.WinForms.Controls.vButton()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.VGroupBox1.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.TabControl1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(852, 496)
        Me.Panel1.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Alignment = System.Windows.Forms.TabAlignment.Left
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed
        Me.TabControl1.ItemSize = New System.Drawing.Size(30, 120)
        Me.TabControl1.Location = New System.Drawing.Point(11, 11)
        Me.TabControl1.Multiline = True
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(828, 472)
        Me.TabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.m_saveButton)
        Me.TabPage1.Controls.Add(Me.databasesCB)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.PortTB)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.ServerAddressTB)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.IDTB)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Location = New System.Drawing.Point(124, 4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(700, 464)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Connection"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'databasesCB
        '
        Me.databasesCB.FormattingEnabled = True
        Me.databasesCB.Location = New System.Drawing.Point(151, 157)
        Me.databasesCB.Name = "databasesCB"
        Me.databasesCB.Size = New System.Drawing.Size(197, 21)
        Me.databasesCB.TabIndex = 19
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(27, 157)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 15)
        Me.Label5.TabIndex = 17
        Me.Label5.Text = "Database"
        '
        'PortTB
        '
        Me.PortTB.AcceptsReturn = True
        Me.PortTB.Location = New System.Drawing.Point(151, 104)
        Me.PortTB.Name = "PortTB"
        Me.PortTB.Size = New System.Drawing.Size(196, 20)
        Me.PortTB.TabIndex = 16
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(27, 107)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 15)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Port"
        '
        'ServerAddressTB
        '
        Me.ServerAddressTB.Location = New System.Drawing.Point(151, 52)
        Me.ServerAddressTB.Name = "ServerAddressTB"
        Me.ServerAddressTB.Size = New System.Drawing.Size(196, 20)
        Me.ServerAddressTB.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(27, 55)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 15)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Server"
        '
        'IDTB
        '
        Me.IDTB.Location = New System.Drawing.Point(151, 210)
        Me.IDTB.Name = "IDTB"
        Me.IDTB.Size = New System.Drawing.Size(196, 20)
        Me.IDTB.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 213)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "User ID"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.VGroupBox1)
        Me.TabPage2.Location = New System.Drawing.Point(124, 4)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(700, 464)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Display"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'VGroupBox1
        '
        Me.VGroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VGroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.VGroupBox1.Controls.Add(Me.FormatsDGV)
        Me.VGroupBox1.Location = New System.Drawing.Point(17, 19)
        Me.VGroupBox1.Name = "VGroupBox1"
        Me.VGroupBox1.Size = New System.Drawing.Size(670, 218)
        Me.VGroupBox1.TabIndex = 1
        Me.VGroupBox1.TabStop = False
        Me.VGroupBox1.Text = "Reports Formats"
        Me.VGroupBox1.UseThemeBorderColor = True
        Me.VGroupBox1.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'FormatsDGV
        '
        Me.FormatsDGV.AllowAnimations = True
        Me.FormatsDGV.AllowCellMerge = True
        Me.FormatsDGV.AllowClipDrawing = True
        Me.FormatsDGV.AllowContextMenuColumnChooser = True
        Me.FormatsDGV.AllowContextMenuFiltering = True
        Me.FormatsDGV.AllowContextMenuGrouping = True
        Me.FormatsDGV.AllowContextMenuSorting = True
        Me.FormatsDGV.AllowCopyPaste = False
        Me.FormatsDGV.AllowDefaultContextMenu = True
        Me.FormatsDGV.AllowDragDropIndication = True
        Me.FormatsDGV.AllowHeaderItemHighlightOnCellSelection = True
        Me.FormatsDGV.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FormatsDGV.AutoUpdateOnListChanged = False
        Me.FormatsDGV.BackColor = System.Drawing.Color.White
        Me.FormatsDGV.BindingProgressEnabled = False
        Me.FormatsDGV.BindingProgressSampleRate = 20000
        Me.FormatsDGV.BorderColor = System.Drawing.Color.Empty
        Me.FormatsDGV.CellsArea.AllowCellMerge = True
        Me.FormatsDGV.CellsArea.ConditionalFormattingEnabled = False
        Me.FormatsDGV.ColumnsHierarchy.AllowDragDrop = False
        Me.FormatsDGV.ColumnsHierarchy.AllowResize = True
        Me.FormatsDGV.ColumnsHierarchy.AutoStretchColumns = False
        Me.FormatsDGV.ColumnsHierarchy.Fixed = False
        Me.FormatsDGV.ColumnsHierarchy.ShowExpandCollapseButtons = True
        Me.FormatsDGV.EnableColumnChooser = False
        Me.FormatsDGV.EnableResizeToolTip = True
        Me.FormatsDGV.EnableToolTips = True
        Me.FormatsDGV.FilterDisplayMode = VIBlend.WinForms.DataGridView.FilterDisplayMode.[Default]
        Me.FormatsDGV.GridLinesDashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.FormatsDGV.GridLinesDisplayMode = VIBlend.WinForms.DataGridView.GridLinesDisplayMode.DISPLAY_ALL
        Me.FormatsDGV.GroupingEnabled = False
        Me.FormatsDGV.HorizontalScroll = 0
        Me.FormatsDGV.HorizontalScrollBarLargeChange = 20
        Me.FormatsDGV.HorizontalScrollBarSmallChange = 5
        Me.FormatsDGV.ImageList = Nothing
        Me.FormatsDGV.Localization = DataGridLocalization1
        Me.FormatsDGV.Location = New System.Drawing.Point(6, 16)
        Me.FormatsDGV.MultipleSelectionEnabled = True
        Me.FormatsDGV.Name = "FormatsDGV"
        Me.FormatsDGV.PivotColumnsTotalsEnabled = False
        Me.FormatsDGV.PivotColumnsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH
        Me.FormatsDGV.PivotRowsTotalsEnabled = False
        Me.FormatsDGV.PivotRowsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH
        Me.FormatsDGV.RowsHierarchy.AllowDragDrop = False
        Me.FormatsDGV.RowsHierarchy.AllowResize = True
        Me.FormatsDGV.RowsHierarchy.CompactStyleRenderingEnabled = False
        Me.FormatsDGV.RowsHierarchy.CompactStyleRenderingItemsIndent = 15
        Me.FormatsDGV.RowsHierarchy.Fixed = False
        Me.FormatsDGV.RowsHierarchy.ShowExpandCollapseButtons = True
        Me.FormatsDGV.ScrollBarsEnabled = True
        Me.FormatsDGV.SelectionBorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        Me.FormatsDGV.SelectionBorderEnabled = True
        Me.FormatsDGV.SelectionBorderWidth = 2
        Me.FormatsDGV.SelectionMode = VIBlend.WinForms.DataGridView.vDataGridView.SELECTION_MODE.CELL_SELECT
        Me.FormatsDGV.Size = New System.Drawing.Size(658, 185)
        Me.FormatsDGV.TabIndex = 0
        Me.FormatsDGV.Text = "VDataGridView1"
        Me.FormatsDGV.ToolTipDuration = 5000
        Me.FormatsDGV.ToolTipShowDelay = 1500
        Me.FormatsDGV.VerticalScroll = 0
        Me.FormatsDGV.VerticalScrollBarLargeChange = 20
        Me.FormatsDGV.VerticalScrollBarSmallChange = 5
        Me.FormatsDGV.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER
        Me.FormatsDGV.VirtualModeCellDefault = False
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.CurrenciesCombobox)
        Me.TabPage3.Controls.Add(Me.VLabel1)
        Me.TabPage3.Location = New System.Drawing.Point(124, 4)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(700, 464)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Preferences"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'CurrenciesCombobox
        '
        Me.CurrenciesCombobox.BackColor = System.Drawing.Color.White
        Me.CurrenciesCombobox.DisplayMember = ""
        Me.CurrenciesCombobox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.CurrenciesCombobox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.CurrenciesCombobox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.CurrenciesCombobox.DropDownWidth = 166
        Me.CurrenciesCombobox.Location = New System.Drawing.Point(165, 35)
        Me.CurrenciesCombobox.Name = "CurrenciesCombobox"
        Me.CurrenciesCombobox.RoundedCornersMaskListItem = CType(15, Byte)
        Me.CurrenciesCombobox.Size = New System.Drawing.Size(166, 23)
        Me.CurrenciesCombobox.TabIndex = 0
        Me.CurrenciesCombobox.UseThemeBackColor = False
        Me.CurrenciesCombobox.UseThemeDropDownArrowColor = True
        Me.CurrenciesCombobox.ValueMember = ""
        Me.CurrenciesCombobox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        Me.CurrenciesCombobox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VLabel1
        '
        Me.VLabel1.BackColor = System.Drawing.Color.Transparent
        Me.VLabel1.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel1.Ellipsis = False
        Me.VLabel1.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel1.Location = New System.Drawing.Point(22, 38)
        Me.VLabel1.Multiline = True
        Me.VLabel1.Name = "VLabel1"
        Me.VLabel1.Size = New System.Drawing.Size(149, 25)
        Me.VLabel1.TabIndex = 0
        Me.VLabel1.Text = "Consolidation Currency"
        Me.VLabel1.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel1.UseMnemonics = True
        Me.VLabel1.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'ButtonIcons
        '
        Me.ButtonIcons.ImageStream = CType(resources.GetObject("ButtonIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonIcons.Images.SetKeyName(0, "imageres_89.ico")
        Me.ButtonIcons.Images.SetKeyName(1, "favicon(95).ico")
        Me.ButtonIcons.Images.SetKeyName(2, "1420498403_340208.ico")
        Me.ButtonIcons.Images.SetKeyName(3, "favicon(97).ico")
        Me.ButtonIcons.Images.SetKeyName(4, "imageres_99.ico")
        Me.ButtonIcons.Images.SetKeyName(5, "favicon(70).ico")
        Me.ButtonIcons.Images.SetKeyName(6, "imageres_82.ico")
        Me.ButtonIcons.Images.SetKeyName(7, "refresh greay bcgd.bmp")
        '
        'ControlImages
        '
        Me.ControlImages.ImageStream = CType(resources.GetObject("ControlImages.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ControlImages.TransparentColor = System.Drawing.Color.Transparent
        Me.ControlImages.Images.SetKeyName(0, "close blue light.ico")
        Me.ControlImages.Images.SetKeyName(1, "favicon(99).ico")
        '
        'ACFIcon
        '
        Me.ACFIcon.ImageStream = CType(resources.GetObject("ACFIcon.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ACFIcon.TransparentColor = System.Drawing.Color.Transparent
        Me.ACFIcon.Images.SetKeyName(0, "ACF Square 2 .1Control bgd.png")
        '
        'm_saveButton
        '
        Me.m_saveButton.AllowAnimations = True
        Me.m_saveButton.BackColor = System.Drawing.Color.Transparent
        Me.m_saveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.m_saveButton.ImageKey = "1420498403_340208.ico"
        Me.m_saveButton.ImageList = Me.ButtonIcons
        Me.m_saveButton.Location = New System.Drawing.Point(248, 258)
        Me.m_saveButton.Name = "m_saveButton"
        Me.m_saveButton.RoundedCornersMask = CType(15, Byte)
        Me.m_saveButton.Size = New System.Drawing.Size(100, 30)
        Me.m_saveButton.TabIndex = 20
        Me.m_saveButton.Text = "Save"
        Me.m_saveButton.UseVisualStyleBackColor = False
        Me.m_saveButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'SettingUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(852, 496)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "SettingUI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Settings"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.VGroupBox1.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents ControlImages As System.Windows.Forms.ImageList
    Friend WithEvents ButtonIcons As System.Windows.Forms.ImageList
    Friend WithEvents ACFIcon As System.Windows.Forms.ImageList
    Friend WithEvents IDTB As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ServerAddressTB As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents PortTB As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents databasesCB As System.Windows.Forms.ComboBox
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents VGroupBox1 As VIBlend.WinForms.Controls.vGroupBox
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents FormatsDGV As VIBlend.WinForms.DataGridView.vDataGridView
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents CurrenciesCombobox As VIBlend.WinForms.Controls.vComboBox
    Friend WithEvents VLabel1 As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_saveButton As VIBlend.WinForms.Controls.vButton
End Class
