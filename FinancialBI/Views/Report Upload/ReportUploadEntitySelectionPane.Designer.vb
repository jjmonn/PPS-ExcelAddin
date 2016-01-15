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
        Me.m_periodsSelectionPanel = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1.SuspendLayout()
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
        Me.TableLayoutPanel1.Controls.Add(Me.m_periodsSelectionPanel, 0, 3)
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
        'm_periodsSelectionPanel
        '
        Me.m_periodsSelectionPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_periodsSelectionPanel.Location = New System.Drawing.Point(3, 355)
        Me.m_periodsSelectionPanel.Name = "m_periodsSelectionPanel"
        Me.m_periodsSelectionPanel.Size = New System.Drawing.Size(253, 197)
        Me.m_periodsSelectionPanel.TabIndex = 8
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
    Friend WithEvents m_periodsSelectionPanel As System.Windows.Forms.Panel

End Class
