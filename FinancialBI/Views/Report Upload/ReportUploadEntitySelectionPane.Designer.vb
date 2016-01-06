<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportUploadEntitySelectionPane
    Inherits AddinExpress.XL.ADXExcelTaskPane

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReportUploadEntitySelectionPane))
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.EntitiesTVImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.m_entitySelectionLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_periodsSelectionLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_accountSelectionLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_accountSelectionComboBox = New VIBlend.WinForms.Controls.vComboBox()
        Me.m_entitiesTV = New VIBlend.WinForms.Controls.vTreeView()
        Me.m_validateButton = New VIBlend.WinForms.Controls.vButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.m_endWeekTB = New VIBlend.WinForms.Controls.vTextBox()
        Me.m_startWeekTB = New VIBlend.WinForms.Controls.vTextBox()
        Me.m_startDateLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_endDateLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_startDate = New VIBlend.WinForms.Controls.vDatePicker()
        Me.m_endDate = New VIBlend.WinForms.Controls.vDatePicker()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "1420498403_340208.ico")
        '
        'EntitiesTVImageList
        '
        Me.EntitiesTVImageList.ImageStream = CType(resources.GetObject("EntitiesTVImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.EntitiesTVImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.EntitiesTVImageList.Images.SetKeyName(0, "favicon(81).ico")
        Me.EntitiesTVImageList.Images.SetKeyName(1, "cloud_dark.ico")
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.m_entitySelectionLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.m_periodsSelectionLabel, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.m_accountSelectionLabel, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.m_accountSelectionComboBox, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.m_entitiesTV, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.m_validateButton, 0, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 3)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 7
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 203.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 74.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(259, 685)
        Me.TableLayoutPanel1.TabIndex = 6
        '
        'm_entitySelectionLabel
        '
        Me.m_entitySelectionLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_entitySelectionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_entitySelectionLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_entitySelectionLabel.Ellipsis = False
        Me.m_entitySelectionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_entitySelectionLabel.Location = New System.Drawing.Point(3, 3)
        Me.m_entitySelectionLabel.Multiline = True
        Me.m_entitySelectionLabel.Name = "m_entitySelectionLabel"
        Me.m_entitySelectionLabel.Size = New System.Drawing.Size(253, 18)
        Me.m_entitySelectionLabel.TabIndex = 4
        Me.m_entitySelectionLabel.Text = "Entity selection"
        Me.m_entitySelectionLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_entitySelectionLabel.UseMnemonics = True
        Me.m_entitySelectionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_periodsSelectionLabel
        '
        Me.m_periodsSelectionLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_periodsSelectionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_periodsSelectionLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_periodsSelectionLabel.Ellipsis = False
        Me.m_periodsSelectionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_periodsSelectionLabel.Location = New System.Drawing.Point(3, 331)
        Me.m_periodsSelectionLabel.Multiline = True
        Me.m_periodsSelectionLabel.Name = "m_periodsSelectionLabel"
        Me.m_periodsSelectionLabel.Size = New System.Drawing.Size(253, 18)
        Me.m_periodsSelectionLabel.TabIndex = 3
        Me.m_periodsSelectionLabel.Text = "Periods selection"
        Me.m_periodsSelectionLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_periodsSelectionLabel.UseMnemonics = True
        Me.m_periodsSelectionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_accountSelectionLabel
        '
        Me.m_accountSelectionLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_accountSelectionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_accountSelectionLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_accountSelectionLabel.Ellipsis = False
        Me.m_accountSelectionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_accountSelectionLabel.Location = New System.Drawing.Point(3, 558)
        Me.m_accountSelectionLabel.Multiline = True
        Me.m_accountSelectionLabel.Name = "m_accountSelectionLabel"
        Me.m_accountSelectionLabel.Size = New System.Drawing.Size(253, 21)
        Me.m_accountSelectionLabel.TabIndex = 5
        Me.m_accountSelectionLabel.Text = "Account selection"
        Me.m_accountSelectionLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_accountSelectionLabel.UseMnemonics = True
        Me.m_accountSelectionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_accountSelectionComboBox
        '
        Me.m_accountSelectionComboBox.BackColor = System.Drawing.Color.White
        Me.m_accountSelectionComboBox.DisplayMember = ""
        Me.m_accountSelectionComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_accountSelectionComboBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.m_accountSelectionComboBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.m_accountSelectionComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.m_accountSelectionComboBox.DropDownWidth = 253
        Me.m_accountSelectionComboBox.Location = New System.Drawing.Point(3, 585)
        Me.m_accountSelectionComboBox.Name = "m_accountSelectionComboBox"
        Me.m_accountSelectionComboBox.RoundedCornersMaskListItem = CType(15, Byte)
        Me.m_accountSelectionComboBox.Size = New System.Drawing.Size(253, 23)
        Me.m_accountSelectionComboBox.TabIndex = 2
        Me.m_accountSelectionComboBox.UseThemeBackColor = False
        Me.m_accountSelectionComboBox.UseThemeDropDownArrowColor = True
        Me.m_accountSelectionComboBox.ValueMember = ""
        Me.m_accountSelectionComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        Me.m_accountSelectionComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_entitiesTV
        '
        Me.m_entitiesTV.AccessibleName = "TreeView"
        Me.m_entitiesTV.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.m_entitiesTV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_entitiesTV.ItemHeight = 17
        Me.m_entitiesTV.Location = New System.Drawing.Point(3, 27)
        Me.m_entitiesTV.Name = "m_entitiesTV"
        Me.m_entitiesTV.ScrollPosition = New System.Drawing.Point(0, 0)
        Me.m_entitiesTV.SelectedNode = Nothing
        Me.m_entitiesTV.Size = New System.Drawing.Size(253, 298)
        Me.m_entitiesTV.TabIndex = 6
        Me.m_entitiesTV.Text = "VTreeView1"
        Me.m_entitiesTV.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICEBLACK
        Me.m_entitiesTV.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICEBLACK
        '
        'm_validateButton
        '
        Me.m_validateButton.AllowAnimations = True
        Me.m_validateButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.m_validateButton.BackColor = System.Drawing.Color.Transparent
        Me.m_validateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.m_validateButton.ImageKey = "1420498403_340208.ico"
        Me.m_validateButton.ImageList = Me.ImageList1
        Me.m_validateButton.Location = New System.Drawing.Point(3, 658)
        Me.m_validateButton.Name = "m_validateButton"
        Me.m_validateButton.RoundedCornersMask = CType(15, Byte)
        Me.m_validateButton.Size = New System.Drawing.Size(98, 24)
        Me.m_validateButton.TabIndex = 7
        Me.m_validateButton.Text = "Validate"
        Me.m_validateButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.m_validateButton.UseVisualStyleBackColor = False
        Me.m_validateButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.m_endWeekTB)
        Me.Panel1.Controls.Add(Me.m_startWeekTB)
        Me.Panel1.Controls.Add(Me.m_startDateLabel)
        Me.Panel1.Controls.Add(Me.m_endDateLabel)
        Me.Panel1.Controls.Add(Me.m_startDate)
        Me.Panel1.Controls.Add(Me.m_endDate)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 355)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(253, 197)
        Me.Panel1.TabIndex = 8
        '
        'm_endWeekTB
        '
        Me.m_endWeekTB.BackColor = System.Drawing.Color.White
        Me.m_endWeekTB.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.m_endWeekTB.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.m_endWeekTB.DefaultText = "Empty..."
        Me.m_endWeekTB.Enabled = False
        Me.m_endWeekTB.Location = New System.Drawing.Point(124, 128)
        Me.m_endWeekTB.MaxLength = 32767
        Me.m_endWeekTB.Name = "m_endWeekTB"
        Me.m_endWeekTB.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.m_endWeekTB.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.m_endWeekTB.SelectionLength = 0
        Me.m_endWeekTB.SelectionStart = 0
        Me.m_endWeekTB.Size = New System.Drawing.Size(120, 23)
        Me.m_endWeekTB.TabIndex = 9
        Me.m_endWeekTB.Text = "w"
        Me.m_endWeekTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.m_endWeekTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_startWeekTB
        '
        Me.m_startWeekTB.BackColor = System.Drawing.Color.White
        Me.m_startWeekTB.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.m_startWeekTB.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.m_startWeekTB.DefaultText = "Empty..."
        Me.m_startWeekTB.Enabled = False
        Me.m_startWeekTB.Location = New System.Drawing.Point(124, 50)
        Me.m_startWeekTB.MaxLength = 32767
        Me.m_startWeekTB.Name = "m_startWeekTB"
        Me.m_startWeekTB.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.m_startWeekTB.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.m_startWeekTB.SelectionLength = 0
        Me.m_startWeekTB.SelectionStart = 0
        Me.m_startWeekTB.Size = New System.Drawing.Size(120, 23)
        Me.m_startWeekTB.TabIndex = 0
        Me.m_startWeekTB.Text = "w"
        Me.m_startWeekTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.m_startWeekTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_startDateLabel
        '
        Me.m_startDateLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_startDateLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_startDateLabel.Ellipsis = False
        Me.m_startDateLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_startDateLabel.Location = New System.Drawing.Point(7, 18)
        Me.m_startDateLabel.Multiline = True
        Me.m_startDateLabel.Name = "m_startDateLabel"
        Me.m_startDateLabel.Size = New System.Drawing.Size(105, 26)
        Me.m_startDateLabel.TabIndex = 7
        Me.m_startDateLabel.Text = "Start date"
        Me.m_startDateLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.m_startDateLabel.UseMnemonics = True
        Me.m_startDateLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_endDateLabel
        '
        Me.m_endDateLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_endDateLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_endDateLabel.Ellipsis = False
        Me.m_endDateLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_endDateLabel.Location = New System.Drawing.Point(7, 96)
        Me.m_endDateLabel.Multiline = True
        Me.m_endDateLabel.Name = "m_endDateLabel"
        Me.m_endDateLabel.Size = New System.Drawing.Size(105, 26)
        Me.m_endDateLabel.TabIndex = 6
        Me.m_endDateLabel.Text = "End date"
        Me.m_endDateLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.m_endDateLabel.UseMnemonics = True
        Me.m_endDateLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_startDate
        '
        Me.m_startDate.BackColor = System.Drawing.Color.White
        Me.m_startDate.BorderColor = System.Drawing.Color.Black
        Me.m_startDate.Culture = New System.Globalization.CultureInfo("")
        Me.m_startDate.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.m_startDate.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.m_startDate.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.None
        Me.m_startDate.FormatValue = ""
        Me.m_startDate.Location = New System.Drawing.Point(124, 18)
        Me.m_startDate.MaxDate = New Date(2100, 1, 1, 0, 0, 0, 0)
        Me.m_startDate.MinDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.m_startDate.Name = "m_startDate"
        Me.m_startDate.ShowGrip = False
        Me.m_startDate.Size = New System.Drawing.Size(120, 26)
        Me.m_startDate.TabIndex = 5
        Me.m_startDate.Text = "VDatePicker1"
        Me.m_startDate.UseThemeBackColor = False
        Me.m_startDate.UseThemeDropDownArrowColor = True
        Me.m_startDate.Value = New Date(2016, 1, 4, 9, 28, 7, 919)
        Me.m_startDate.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_endDate
        '
        Me.m_endDate.BackColor = System.Drawing.Color.White
        Me.m_endDate.BorderColor = System.Drawing.Color.Black
        Me.m_endDate.Culture = New System.Globalization.CultureInfo("")
        Me.m_endDate.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.m_endDate.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.m_endDate.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.None
        Me.m_endDate.FormatValue = ""
        Me.m_endDate.Location = New System.Drawing.Point(124, 96)
        Me.m_endDate.MaxDate = New Date(2100, 1, 1, 0, 0, 0, 0)
        Me.m_endDate.MinDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.m_endDate.Name = "m_endDate"
        Me.m_endDate.ShowGrip = False
        Me.m_endDate.Size = New System.Drawing.Size(120, 26)
        Me.m_endDate.TabIndex = 4
        Me.m_endDate.Text = "VDatePicker1"
        Me.m_endDate.UseThemeBackColor = False
        Me.m_endDate.UseThemeDropDownArrowColor = True
        Me.m_endDate.Value = New Date(2016, 1, 4, 9, 27, 20, 177)
        Me.m_endDate.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'ReportUploadEntitySelectionPane
        '
        Me.BackColor = System.Drawing.SystemColors.GrayText
        Me.ClientSize = New System.Drawing.Size(259, 685)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "ReportUploadEntitySelectionPane"
        Me.Text = "Data Edition"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents EntitiesTVImageList As System.Windows.Forms.ImageList
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents m_entitySelectionLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_periodsSelectionLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_accountSelectionLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_accountSelectionComboBox As VIBlend.WinForms.Controls.vComboBox
    Friend WithEvents m_entitiesTV As VIBlend.WinForms.Controls.vTreeView
    Friend WithEvents m_validateButton As VIBlend.WinForms.Controls.vButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents m_startDateLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_endDateLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_startDate As VIBlend.WinForms.Controls.vDatePicker
    Friend WithEvents m_endDate As VIBlend.WinForms.Controls.vDatePicker
    Friend WithEvents m_endWeekTB As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents m_startWeekTB As VIBlend.WinForms.Controls.vTextBox

End Class
