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
        Me.m_endWeekTB = New VIBlend.WinForms.Controls.vTextBox()
        Me.m_startWeekTB = New VIBlend.WinForms.Controls.vTextBox()
        Me.m_startDateLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_endDateLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_startDate = New VIBlend.WinForms.Controls.vDatePicker()
        Me.m_endDate = New VIBlend.WinForms.Controls.vDatePicker()
        Me.m_validateButton = New VIBlend.WinForms.Controls.vButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.SuspendLayout()
        '
        'm_endWeekTB
        '
        Me.m_endWeekTB.BackColor = System.Drawing.Color.White
        Me.m_endWeekTB.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.m_endWeekTB.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.m_endWeekTB.DefaultText = "Empty..."
        Me.m_endWeekTB.Enabled = False
        Me.m_endWeekTB.Location = New System.Drawing.Point(254, 70)
        Me.m_endWeekTB.MaxLength = 32767
        Me.m_endWeekTB.Name = "m_endWeekTB"
        Me.m_endWeekTB.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.m_endWeekTB.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.m_endWeekTB.SelectionLength = 0
        Me.m_endWeekTB.SelectionStart = 0
        Me.m_endWeekTB.Size = New System.Drawing.Size(120, 26)
        Me.m_endWeekTB.TabIndex = 13
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
        Me.m_startWeekTB.Location = New System.Drawing.Point(254, 26)
        Me.m_startWeekTB.MaxLength = 32767
        Me.m_startWeekTB.Name = "m_startWeekTB"
        Me.m_startWeekTB.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.m_startWeekTB.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.m_startWeekTB.SelectionLength = 0
        Me.m_startWeekTB.SelectionStart = 0
        Me.m_startWeekTB.Size = New System.Drawing.Size(120, 26)
        Me.m_startWeekTB.TabIndex = 12
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
        Me.m_startDateLabel.Location = New System.Drawing.Point(20, 26)
        Me.m_startDateLabel.Multiline = True
        Me.m_startDateLabel.Name = "m_startDateLabel"
        Me.m_startDateLabel.Size = New System.Drawing.Size(88, 26)
        Me.m_startDateLabel.TabIndex = 10
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
        Me.m_endDateLabel.Location = New System.Drawing.Point(20, 70)
        Me.m_endDateLabel.Multiline = True
        Me.m_endDateLabel.Name = "m_endDateLabel"
        Me.m_endDateLabel.Size = New System.Drawing.Size(88, 26)
        Me.m_endDateLabel.TabIndex = 11
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
        Me.m_startDate.Location = New System.Drawing.Point(114, 26)
        Me.m_startDate.MaxDate = New Date(2100, 1, 1, 0, 0, 0, 0)
        Me.m_startDate.MinDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.m_startDate.Name = "m_startDate"
        Me.m_startDate.ShowGrip = False
        Me.m_startDate.Size = New System.Drawing.Size(120, 26)
        Me.m_startDate.TabIndex = 1
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
        Me.m_endDate.Location = New System.Drawing.Point(114, 70)
        Me.m_endDate.MaxDate = New Date(2100, 1, 1, 0, 0, 0, 0)
        Me.m_endDate.MinDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.m_endDate.Name = "m_endDate"
        Me.m_endDate.ShowGrip = False
        Me.m_endDate.Size = New System.Drawing.Size(120, 26)
        Me.m_endDate.TabIndex = 2
        Me.m_endDate.Text = "VDatePicker1"
        Me.m_endDate.UseThemeBackColor = False
        Me.m_endDate.UseThemeDropDownArrowColor = True
        Me.m_endDate.Value = New Date(2016, 1, 4, 9, 27, 20, 177)
        Me.m_endDate.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_validateButton
        '
        Me.m_validateButton.AllowAnimations = True
        Me.m_validateButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.m_validateButton.BackColor = System.Drawing.Color.Transparent
        Me.m_validateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.m_validateButton.ImageKey = "1420498403_340208.ico"
        Me.m_validateButton.ImageList = Me.ImageList1
        Me.m_validateButton.Location = New System.Drawing.Point(276, 120)
        Me.m_validateButton.Name = "m_validateButton"
        Me.m_validateButton.RoundedCornersMask = CType(15, Byte)
        Me.m_validateButton.Size = New System.Drawing.Size(98, 24)
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
        'SnapshotPeriodRangeSelectionUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(400, 156)
        Me.Controls.Add(Me.m_validateButton)
        Me.Controls.Add(Me.m_endWeekTB)
        Me.Controls.Add(Me.m_startWeekTB)
        Me.Controls.Add(Me.m_startDateLabel)
        Me.Controls.Add(Me.m_endDateLabel)
        Me.Controls.Add(Me.m_startDate)
        Me.Controls.Add(Me.m_endDate)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "SnapshotPeriodRangeSelectionUI"
        Me.Text = "Period Range"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents m_endWeekTB As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents m_startWeekTB As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents m_startDateLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_endDateLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_startDate As VIBlend.WinForms.Controls.vDatePicker
    Friend WithEvents m_endDate As VIBlend.WinForms.Controls.vDatePicker
    Friend WithEvents m_validateButton As VIBlend.WinForms.Controls.vButton
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
End Class
