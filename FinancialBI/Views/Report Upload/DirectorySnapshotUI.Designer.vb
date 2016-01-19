<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DirectorySnapshotUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DirectorySnapshotUI))
        Me.m_directoryLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.m_directoryTextBox = New VIBlend.WinForms.Controls.vTextBox()
        Me.m_accountSelectionLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_accountSelectionComboBox = New VIBlend.WinForms.Controls.vComboBox()
        Me.m_periodSelectionPanel = New System.Windows.Forms.Panel()
        Me.m_validateButton = New VIBlend.WinForms.Controls.vButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.m_worksheetTargetName = New VIBlend.WinForms.Controls.vTextBox()
        Me.m_worksheetNameLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.SuspendLayout()
        '
        'm_directoryLabel
        '
        Me.m_directoryLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_directoryLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_directoryLabel.Ellipsis = False
        Me.m_directoryLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_directoryLabel.Location = New System.Drawing.Point(20, 19)
        Me.m_directoryLabel.Multiline = True
        Me.m_directoryLabel.Name = "m_directoryLabel"
        Me.m_directoryLabel.Size = New System.Drawing.Size(110, 18)
        Me.m_directoryLabel.TabIndex = 0
        Me.m_directoryLabel.Text = "Directory"
        Me.m_directoryLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_directoryLabel.UseMnemonics = True
        Me.m_directoryLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_directoryTextBox
        '
        Me.m_directoryTextBox.BackColor = System.Drawing.Color.White
        Me.m_directoryTextBox.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.m_directoryTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.m_directoryTextBox.DefaultText = "Empty..."
        Me.m_directoryTextBox.Location = New System.Drawing.Point(136, 14)
        Me.m_directoryTextBox.MaxLength = 32767
        Me.m_directoryTextBox.Name = "m_directoryTextBox"
        Me.m_directoryTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.m_directoryTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.m_directoryTextBox.SelectionLength = 0
        Me.m_directoryTextBox.SelectionStart = 0
        Me.m_directoryTextBox.Size = New System.Drawing.Size(299, 23)
        Me.m_directoryTextBox.TabIndex = 1
        Me.m_directoryTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.m_directoryTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_accountSelectionLabel
        '
        Me.m_accountSelectionLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_accountSelectionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_accountSelectionLabel.Ellipsis = False
        Me.m_accountSelectionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_accountSelectionLabel.Location = New System.Drawing.Point(20, 262)
        Me.m_accountSelectionLabel.Multiline = True
        Me.m_accountSelectionLabel.Name = "m_accountSelectionLabel"
        Me.m_accountSelectionLabel.Size = New System.Drawing.Size(134, 23)
        Me.m_accountSelectionLabel.TabIndex = 10
        Me.m_accountSelectionLabel.Text = "Account selection"
        Me.m_accountSelectionLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.m_accountSelectionLabel.UseMnemonics = True
        Me.m_accountSelectionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_accountSelectionComboBox
        '
        Me.m_accountSelectionComboBox.BackColor = System.Drawing.Color.White
        Me.m_accountSelectionComboBox.DisplayMember = ""
        Me.m_accountSelectionComboBox.DropDownList = True
        Me.m_accountSelectionComboBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.m_accountSelectionComboBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.m_accountSelectionComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.m_accountSelectionComboBox.DropDownWidth = 275
        Me.m_accountSelectionComboBox.Location = New System.Drawing.Point(160, 262)
        Me.m_accountSelectionComboBox.Name = "m_accountSelectionComboBox"
        Me.m_accountSelectionComboBox.RoundedCornersMaskListItem = CType(15, Byte)
        Me.m_accountSelectionComboBox.Size = New System.Drawing.Size(275, 23)
        Me.m_accountSelectionComboBox.TabIndex = 9
        Me.m_accountSelectionComboBox.UseThemeBackColor = False
        Me.m_accountSelectionComboBox.UseThemeDropDownArrowColor = True
        Me.m_accountSelectionComboBox.ValueMember = ""
        Me.m_accountSelectionComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        Me.m_accountSelectionComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_periodSelectionPanel
        '
        Me.m_periodSelectionPanel.Location = New System.Drawing.Point(20, 105)
        Me.m_periodSelectionPanel.Name = "m_periodSelectionPanel"
        Me.m_periodSelectionPanel.Size = New System.Drawing.Size(415, 129)
        Me.m_periodSelectionPanel.TabIndex = 8
        '
        'm_validateButton
        '
        Me.m_validateButton.AllowAnimations = True
        Me.m_validateButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.m_validateButton.BackColor = System.Drawing.Color.Transparent
        Me.m_validateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.m_validateButton.ImageKey = "1420498403_340208.ico"
        Me.m_validateButton.ImageList = Me.ImageList1
        Me.m_validateButton.Location = New System.Drawing.Point(344, 305)
        Me.m_validateButton.Name = "m_validateButton"
        Me.m_validateButton.RoundedCornersMask = CType(15, Byte)
        Me.m_validateButton.Size = New System.Drawing.Size(91, 24)
        Me.m_validateButton.TabIndex = 7
        Me.m_validateButton.Text = "Validate"
        Me.m_validateButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.m_validateButton.UseVisualStyleBackColor = False
        Me.m_validateButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "1420498403_340208.ico")
        '
        'm_worksheetTargetName
        '
        Me.m_worksheetTargetName.BackColor = System.Drawing.Color.White
        Me.m_worksheetTargetName.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.m_worksheetTargetName.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.m_worksheetTargetName.DefaultText = "Empty..."
        Me.m_worksheetTargetName.Location = New System.Drawing.Point(136, 54)
        Me.m_worksheetTargetName.MaxLength = 32767
        Me.m_worksheetTargetName.Name = "m_worksheetTargetName"
        Me.m_worksheetTargetName.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.m_worksheetTargetName.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.m_worksheetTargetName.SelectionLength = 0
        Me.m_worksheetTargetName.SelectionStart = 0
        Me.m_worksheetTargetName.Size = New System.Drawing.Size(299, 23)
        Me.m_worksheetTargetName.TabIndex = 12
        Me.m_worksheetTargetName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.m_worksheetTargetName.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_worksheetNameLabel
        '
        Me.m_worksheetNameLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_worksheetNameLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_worksheetNameLabel.Ellipsis = False
        Me.m_worksheetNameLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_worksheetNameLabel.Location = New System.Drawing.Point(20, 59)
        Me.m_worksheetNameLabel.Multiline = True
        Me.m_worksheetNameLabel.Name = "m_worksheetNameLabel"
        Me.m_worksheetNameLabel.Size = New System.Drawing.Size(110, 18)
        Me.m_worksheetNameLabel.TabIndex = 11
        Me.m_worksheetNameLabel.Text = "Worksheet Name"
        Me.m_worksheetNameLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_worksheetNameLabel.UseMnemonics = True
        Me.m_worksheetNameLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'DirectorySnapshotUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(447, 349)
        Me.Controls.Add(Me.m_worksheetTargetName)
        Me.Controls.Add(Me.m_worksheetNameLabel)
        Me.Controls.Add(Me.m_accountSelectionLabel)
        Me.Controls.Add(Me.m_accountSelectionComboBox)
        Me.Controls.Add(Me.m_periodSelectionPanel)
        Me.Controls.Add(Me.m_validateButton)
        Me.Controls.Add(Me.m_directoryTextBox)
        Me.Controls.Add(Me.m_directoryLabel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "DirectorySnapshotUI"
        Me.Text = "Directory Snapshot"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents m_directoryLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents m_directoryTextBox As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents m_accountSelectionLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_accountSelectionComboBox As VIBlend.WinForms.Controls.vComboBox
    Friend WithEvents m_periodSelectionPanel As System.Windows.Forms.Panel
    Friend WithEvents m_validateButton As VIBlend.WinForms.Controls.vButton
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents m_worksheetTargetName As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents m_worksheetNameLabel As VIBlend.WinForms.Controls.vLabel
End Class
