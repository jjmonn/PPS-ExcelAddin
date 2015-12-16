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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SettingUI))
        Dim DataGridLocalization2 As VIBlend.WinForms.DataGridView.DataGridLocalization = New VIBlend.WinForms.DataGridView.DataGridLocalization()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.m_connectionTab = New System.Windows.Forms.TabPage()
        Me.m_saveConnectionButton = New VIBlend.WinForms.Controls.vButton()
        Me.ButtonIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.PortTB = New System.Windows.Forms.TextBox()
        Me.m_portNumberLabel = New System.Windows.Forms.Label()
        Me.ServerAddressTB = New System.Windows.Forms.TextBox()
        Me.m_serverAddressLabel = New System.Windows.Forms.Label()
        Me.IDTB = New System.Windows.Forms.TextBox()
        Me.m_userIdLabel = New System.Windows.Forms.Label()
        Me.m_formatsTab = New System.Windows.Forms.TabPage()
        Me.m_formatsGroup = New VIBlend.WinForms.Controls.vGroupBox()
        Me.FormatsDGV = New VIBlend.WinForms.DataGridView.vDataGridView()
        Me.m_otherTab = New System.Windows.Forms.TabPage()
        Me.m_otherValidateButton = New VIBlend.WinForms.Controls.vButton()
        Me.m_languageComboBox = New VIBlend.WinForms.Controls.vComboBox()
        Me.m_languageLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_currenciesCombobox = New VIBlend.WinForms.Controls.vComboBox()
        Me.m_consolidationCurrencyLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.ControlImages = New System.Windows.Forms.ImageList(Me.components)
        Me.ACFIcon = New System.Windows.Forms.ImageList(Me.components)
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.m_connectionTab.SuspendLayout()
        Me.m_formatsTab.SuspendLayout()
        Me.m_formatsGroup.SuspendLayout()
        Me.m_otherTab.SuspendLayout()
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
        Me.TabControl1.Controls.Add(Me.m_connectionTab)
        Me.TabControl1.Controls.Add(Me.m_formatsTab)
        Me.TabControl1.Controls.Add(Me.m_otherTab)
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
        'm_connectionTab
        '
        Me.m_connectionTab.Controls.Add(Me.m_saveConnectionButton)
        Me.m_connectionTab.Controls.Add(Me.PortTB)
        Me.m_connectionTab.Controls.Add(Me.m_portNumberLabel)
        Me.m_connectionTab.Controls.Add(Me.ServerAddressTB)
        Me.m_connectionTab.Controls.Add(Me.m_serverAddressLabel)
        Me.m_connectionTab.Controls.Add(Me.IDTB)
        Me.m_connectionTab.Controls.Add(Me.m_userIdLabel)
        Me.m_connectionTab.Location = New System.Drawing.Point(124, 4)
        Me.m_connectionTab.Name = "m_connectionTab"
        Me.m_connectionTab.Padding = New System.Windows.Forms.Padding(3)
        Me.m_connectionTab.Size = New System.Drawing.Size(700, 464)
        Me.m_connectionTab.TabIndex = 0
        Me.m_connectionTab.Text = "Connection"
        Me.m_connectionTab.UseVisualStyleBackColor = True
        '
        'm_saveConnectionButton
        '
        Me.m_saveConnectionButton.AllowAnimations = True
        Me.m_saveConnectionButton.BackColor = System.Drawing.Color.Transparent
        Me.m_saveConnectionButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.m_saveConnectionButton.ImageKey = "1420498403_340208.ico"
        Me.m_saveConnectionButton.ImageList = Me.ButtonIcons
        Me.m_saveConnectionButton.Location = New System.Drawing.Point(319, 206)
        Me.m_saveConnectionButton.Name = "m_saveConnectionButton"
        Me.m_saveConnectionButton.RoundedCornersMask = CType(15, Byte)
        Me.m_saveConnectionButton.Size = New System.Drawing.Size(102, 30)
        Me.m_saveConnectionButton.TabIndex = 20
        Me.m_saveConnectionButton.Text = "Save"
        Me.m_saveConnectionButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.m_saveConnectionButton.UseVisualStyleBackColor = False
        Me.m_saveConnectionButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
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
        'PortTB
        '
        Me.PortTB.AcceptsReturn = True
        Me.PortTB.Location = New System.Drawing.Point(201, 104)
        Me.PortTB.Name = "PortTB"
        Me.PortTB.Size = New System.Drawing.Size(220, 20)
        Me.PortTB.TabIndex = 16
        '
        'm_portNumberLabel
        '
        Me.m_portNumberLabel.AutoSize = True
        Me.m_portNumberLabel.Location = New System.Drawing.Point(27, 107)
        Me.m_portNumberLabel.Name = "m_portNumberLabel"
        Me.m_portNumberLabel.Size = New System.Drawing.Size(75, 15)
        Me.m_portNumberLabel.TabIndex = 15
        Me.m_portNumberLabel.Text = "Port number"
        '
        'ServerAddressTB
        '
        Me.ServerAddressTB.Location = New System.Drawing.Point(201, 52)
        Me.ServerAddressTB.Name = "ServerAddressTB"
        Me.ServerAddressTB.Size = New System.Drawing.Size(220, 20)
        Me.ServerAddressTB.TabIndex = 5
        '
        'm_serverAddressLabel
        '
        Me.m_serverAddressLabel.AutoSize = True
        Me.m_serverAddressLabel.Location = New System.Drawing.Point(27, 55)
        Me.m_serverAddressLabel.Name = "m_serverAddressLabel"
        Me.m_serverAddressLabel.Size = New System.Drawing.Size(89, 15)
        Me.m_serverAddressLabel.TabIndex = 4
        Me.m_serverAddressLabel.Text = "Server address"
        '
        'IDTB
        '
        Me.IDTB.Location = New System.Drawing.Point(201, 153)
        Me.IDTB.Name = "IDTB"
        Me.IDTB.Size = New System.Drawing.Size(220, 20)
        Me.IDTB.TabIndex = 1
        '
        'm_userIdLabel
        '
        Me.m_userIdLabel.AutoSize = True
        Me.m_userIdLabel.Location = New System.Drawing.Point(27, 156)
        Me.m_userIdLabel.Name = "m_userIdLabel"
        Me.m_userIdLabel.Size = New System.Drawing.Size(46, 15)
        Me.m_userIdLabel.TabIndex = 0
        Me.m_userIdLabel.Text = "User Id"
        '
        'm_formatsTab
        '
        Me.m_formatsTab.Controls.Add(Me.m_formatsGroup)
        Me.m_formatsTab.Location = New System.Drawing.Point(124, 4)
        Me.m_formatsTab.Name = "m_formatsTab"
        Me.m_formatsTab.Padding = New System.Windows.Forms.Padding(3)
        Me.m_formatsTab.Size = New System.Drawing.Size(700, 464)
        Me.m_formatsTab.TabIndex = 1
        Me.m_formatsTab.Text = "Formats options"
        Me.m_formatsTab.UseVisualStyleBackColor = True
        '
        'm_formatsGroup
        '
        Me.m_formatsGroup.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_formatsGroup.BackColor = System.Drawing.Color.Transparent
        Me.m_formatsGroup.Controls.Add(Me.FormatsDGV)
        Me.m_formatsGroup.Location = New System.Drawing.Point(17, 19)
        Me.m_formatsGroup.Name = "m_formatsGroup"
        Me.m_formatsGroup.Size = New System.Drawing.Size(670, 218)
        Me.m_formatsGroup.TabIndex = 1
        Me.m_formatsGroup.TabStop = False
        Me.m_formatsGroup.Text = "Reports formats"
        Me.m_formatsGroup.UseThemeBorderColor = True
        Me.m_formatsGroup.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
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
        Me.FormatsDGV.Localization = DataGridLocalization2
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
        'm_otherTab
        '
        Me.m_otherTab.Controls.Add(Me.m_otherValidateButton)
        Me.m_otherTab.Controls.Add(Me.m_languageComboBox)
        Me.m_otherTab.Controls.Add(Me.m_languageLabel)
        Me.m_otherTab.Controls.Add(Me.m_currenciesCombobox)
        Me.m_otherTab.Controls.Add(Me.m_consolidationCurrencyLabel)
        Me.m_otherTab.Location = New System.Drawing.Point(124, 4)
        Me.m_otherTab.Name = "m_otherTab"
        Me.m_otherTab.Padding = New System.Windows.Forms.Padding(3)
        Me.m_otherTab.Size = New System.Drawing.Size(700, 464)
        Me.m_otherTab.TabIndex = 2
        Me.m_otherTab.Text = "Other"
        Me.m_otherTab.UseVisualStyleBackColor = True
        '
        'm_otherValidateButton
        '
        Me.m_otherValidateButton.AllowAnimations = True
        Me.m_otherValidateButton.BackColor = System.Drawing.Color.Transparent
        Me.m_otherValidateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.m_otherValidateButton.ImageKey = "1420498403_340208.ico"
        Me.m_otherValidateButton.ImageList = Me.ButtonIcons
        Me.m_otherValidateButton.Location = New System.Drawing.Point(264, 146)
        Me.m_otherValidateButton.Name = "m_otherValidateButton"
        Me.m_otherValidateButton.RoundedCornersMask = CType(15, Byte)
        Me.m_otherValidateButton.Size = New System.Drawing.Size(102, 30)
        Me.m_otherValidateButton.TabIndex = 21
        Me.m_otherValidateButton.Text = "Save"
        Me.m_otherValidateButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.m_otherValidateButton.UseVisualStyleBackColor = False
        Me.m_otherValidateButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_languageComboBox
        '
        Me.m_languageComboBox.BackColor = System.Drawing.Color.White
        Me.m_languageComboBox.DisplayMember = ""
        Me.m_languageComboBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.m_languageComboBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.m_languageComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.m_languageComboBox.DropDownWidth = 166
        Me.m_languageComboBox.Location = New System.Drawing.Point(200, 79)
        Me.m_languageComboBox.Name = "m_languageComboBox"
        Me.m_languageComboBox.RoundedCornersMaskListItem = CType(15, Byte)
        Me.m_languageComboBox.Size = New System.Drawing.Size(166, 23)
        Me.m_languageComboBox.TabIndex = 1
        Me.m_languageComboBox.UseThemeBackColor = False
        Me.m_languageComboBox.UseThemeDropDownArrowColor = True
        Me.m_languageComboBox.ValueMember = ""
        Me.m_languageComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        Me.m_languageComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_languageLabel
        '
        Me.m_languageLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_languageLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_languageLabel.Ellipsis = False
        Me.m_languageLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_languageLabel.Location = New System.Drawing.Point(22, 82)
        Me.m_languageLabel.Multiline = True
        Me.m_languageLabel.Name = "m_languageLabel"
        Me.m_languageLabel.Size = New System.Drawing.Size(179, 25)
        Me.m_languageLabel.TabIndex = 2
        Me.m_languageLabel.Text = "Language"
        Me.m_languageLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_languageLabel.UseMnemonics = True
        Me.m_languageLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_currenciesCombobox
        '
        Me.m_currenciesCombobox.BackColor = System.Drawing.Color.White
        Me.m_currenciesCombobox.DisplayMember = ""
        Me.m_currenciesCombobox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.m_currenciesCombobox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.m_currenciesCombobox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.m_currenciesCombobox.DropDownWidth = 166
        Me.m_currenciesCombobox.Location = New System.Drawing.Point(200, 35)
        Me.m_currenciesCombobox.Name = "m_currenciesCombobox"
        Me.m_currenciesCombobox.RoundedCornersMaskListItem = CType(15, Byte)
        Me.m_currenciesCombobox.Size = New System.Drawing.Size(166, 23)
        Me.m_currenciesCombobox.TabIndex = 0
        Me.m_currenciesCombobox.UseThemeBackColor = False
        Me.m_currenciesCombobox.UseThemeDropDownArrowColor = True
        Me.m_currenciesCombobox.ValueMember = ""
        Me.m_currenciesCombobox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        Me.m_currenciesCombobox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_consolidationCurrencyLabel
        '
        Me.m_consolidationCurrencyLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_consolidationCurrencyLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_consolidationCurrencyLabel.Ellipsis = False
        Me.m_consolidationCurrencyLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_consolidationCurrencyLabel.Location = New System.Drawing.Point(22, 38)
        Me.m_consolidationCurrencyLabel.Multiline = True
        Me.m_consolidationCurrencyLabel.Name = "m_consolidationCurrencyLabel"
        Me.m_consolidationCurrencyLabel.Size = New System.Drawing.Size(179, 25)
        Me.m_consolidationCurrencyLabel.TabIndex = 0
        Me.m_consolidationCurrencyLabel.Text = "Devise de consolidation"
        Me.m_consolidationCurrencyLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_consolidationCurrencyLabel.UseMnemonics = True
        Me.m_consolidationCurrencyLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
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
        Me.m_connectionTab.ResumeLayout(False)
        Me.m_connectionTab.PerformLayout()
        Me.m_formatsTab.ResumeLayout(False)
        Me.m_formatsGroup.ResumeLayout(False)
        Me.m_otherTab.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents m_connectionTab As System.Windows.Forms.TabPage
    Friend WithEvents ControlImages As System.Windows.Forms.ImageList
    Friend WithEvents ButtonIcons As System.Windows.Forms.ImageList
    Friend WithEvents ACFIcon As System.Windows.Forms.ImageList
    Friend WithEvents IDTB As System.Windows.Forms.TextBox
    Friend WithEvents m_userIdLabel As System.Windows.Forms.Label
    Friend WithEvents ServerAddressTB As System.Windows.Forms.TextBox
    Friend WithEvents m_serverAddressLabel As System.Windows.Forms.Label
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents PortTB As System.Windows.Forms.TextBox
    Friend WithEvents m_portNumberLabel As System.Windows.Forms.Label
    Friend WithEvents m_formatsTab As System.Windows.Forms.TabPage
    Friend WithEvents m_formatsGroup As VIBlend.WinForms.Controls.vGroupBox
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents FormatsDGV As VIBlend.WinForms.DataGridView.vDataGridView
    Friend WithEvents m_otherTab As System.Windows.Forms.TabPage
    Friend WithEvents m_currenciesCombobox As VIBlend.WinForms.Controls.vComboBox
    Friend WithEvents m_consolidationCurrencyLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_saveConnectionButton As VIBlend.WinForms.Controls.vButton
    Friend WithEvents m_languageComboBox As VIBlend.WinForms.Controls.vComboBox
    Friend WithEvents m_languageLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_otherValidateButton As VIBlend.WinForms.Controls.vButton
End Class
