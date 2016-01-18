<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SnapshotPeriodRangeSelectionUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SnapshotPeriodRangeSelectionUI))
        Me.m_validateButton = New VIBlend.WinForms.Controls.vButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.m_periodSelectionPanel = New System.Windows.Forms.Panel()
        Me.m_accountSelectionComboBox = New VIBlend.WinForms.Controls.vComboBox()
        Me.m_accountSelectionLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.SuspendLayout()
        '
        'm_validateButton
        '
        Me.m_validateButton.AllowAnimations = True
        Me.m_validateButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.m_validateButton.BackColor = System.Drawing.Color.Transparent
        Me.m_validateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.m_validateButton.ImageKey = "1420498403_340208.ico"
        Me.m_validateButton.ImageList = Me.ImageList1
        Me.m_validateButton.Location = New System.Drawing.Point(304, 187)
        Me.m_validateButton.Name = "m_validateButton"
        Me.m_validateButton.RoundedCornersMask = CType(15, Byte)
        Me.m_validateButton.Size = New System.Drawing.Size(91, 24)
        Me.m_validateButton.TabIndex = 3
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
        'm_periodSelectionPanel
        '
        Me.m_periodSelectionPanel.Location = New System.Drawing.Point(12, 13)
        Me.m_periodSelectionPanel.Name = "m_periodSelectionPanel"
        Me.m_periodSelectionPanel.Size = New System.Drawing.Size(383, 129)
        Me.m_periodSelectionPanel.TabIndex = 4
        '
        'm_accountSelectionComboBox
        '
        Me.m_accountSelectionComboBox.BackColor = System.Drawing.Color.White
        Me.m_accountSelectionComboBox.DisplayMember = ""
        Me.m_accountSelectionComboBox.DropDownList = True
        Me.m_accountSelectionComboBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.m_accountSelectionComboBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.m_accountSelectionComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.m_accountSelectionComboBox.DropDownWidth = 256
        Me.m_accountSelectionComboBox.Location = New System.Drawing.Point(139, 148)
        Me.m_accountSelectionComboBox.Name = "m_accountSelectionComboBox"
        Me.m_accountSelectionComboBox.RoundedCornersMaskListItem = CType(15, Byte)
        Me.m_accountSelectionComboBox.Size = New System.Drawing.Size(256, 23)
        Me.m_accountSelectionComboBox.TabIndex = 5
        Me.m_accountSelectionComboBox.UseThemeBackColor = False
        Me.m_accountSelectionComboBox.UseThemeDropDownArrowColor = True
        Me.m_accountSelectionComboBox.ValueMember = ""
        Me.m_accountSelectionComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        Me.m_accountSelectionComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_accountSelectionLabel
        '
        Me.m_accountSelectionLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_accountSelectionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_accountSelectionLabel.Ellipsis = False
        Me.m_accountSelectionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_accountSelectionLabel.Location = New System.Drawing.Point(12, 148)
        Me.m_accountSelectionLabel.Multiline = True
        Me.m_accountSelectionLabel.Name = "m_accountSelectionLabel"
        Me.m_accountSelectionLabel.Size = New System.Drawing.Size(121, 23)
        Me.m_accountSelectionLabel.TabIndex = 6
        Me.m_accountSelectionLabel.Text = "Account selection"
        Me.m_accountSelectionLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.m_accountSelectionLabel.UseMnemonics = True
        Me.m_accountSelectionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'SnapshotPeriodRangeSelectionUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(407, 223)
        Me.Controls.Add(Me.m_accountSelectionLabel)
        Me.Controls.Add(Me.m_accountSelectionComboBox)
        Me.Controls.Add(Me.m_periodSelectionPanel)
        Me.Controls.Add(Me.m_validateButton)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "SnapshotPeriodRangeSelectionUI"
        Me.Text = "Period Range"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents m_validateButton As VIBlend.WinForms.Controls.vButton
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents m_periodSelectionPanel As System.Windows.Forms.Panel
    Friend WithEvents m_accountSelectionComboBox As VIBlend.WinForms.Controls.vComboBox
    Friend WithEvents m_accountSelectionLabel As VIBlend.WinForms.Controls.vLabel
End Class
