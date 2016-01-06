<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PeriodRangeSelectionControl
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
        Me.m_endWeekTB = New VIBlend.WinForms.Controls.vTextBox()
        Me.m_startWeekTB = New VIBlend.WinForms.Controls.vTextBox()
        Me.m_startDateLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_endDateLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_startDate = New VIBlend.WinForms.Controls.vDatePicker()
        Me.m_endDate = New VIBlend.WinForms.Controls.vDatePicker()
        Me.SuspendLayout()
        '
        'm_endWeekTB
        '
        Me.m_endWeekTB.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_endWeekTB.BackColor = System.Drawing.Color.White
        Me.m_endWeekTB.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.m_endWeekTB.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.m_endWeekTB.DefaultText = "Empty..."
        Me.m_endWeekTB.Enabled = False
        Me.m_endWeekTB.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.764706!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.m_endWeekTB.Location = New System.Drawing.Point(107, 108)
        Me.m_endWeekTB.MaxLength = 32767
        Me.m_endWeekTB.Name = "m_endWeekTB"
        Me.m_endWeekTB.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.m_endWeekTB.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.m_endWeekTB.SelectionLength = 0
        Me.m_endWeekTB.SelectionStart = 0
        Me.m_endWeekTB.Size = New System.Drawing.Size(122, 23)
        Me.m_endWeekTB.TabIndex = 15
        Me.m_endWeekTB.Text = " "
        Me.m_endWeekTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.m_endWeekTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_startWeekTB
        '
        Me.m_startWeekTB.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_startWeekTB.BackColor = System.Drawing.Color.White
        Me.m_startWeekTB.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.m_startWeekTB.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.m_startWeekTB.DefaultText = "Empty..."
        Me.m_startWeekTB.Enabled = False
        Me.m_startWeekTB.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.764706!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.m_startWeekTB.Location = New System.Drawing.Point(107, 40)
        Me.m_startWeekTB.MaxLength = 32767
        Me.m_startWeekTB.Name = "m_startWeekTB"
        Me.m_startWeekTB.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.m_startWeekTB.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.m_startWeekTB.SelectionLength = 0
        Me.m_startWeekTB.SelectionStart = 0
        Me.m_startWeekTB.Size = New System.Drawing.Size(122, 23)
        Me.m_startWeekTB.TabIndex = 10
        Me.m_startWeekTB.Text = " "
        Me.m_startWeekTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.m_startWeekTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_startDateLabel
        '
        Me.m_startDateLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_startDateLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_startDateLabel.Ellipsis = False
        Me.m_startDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.764706!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.m_startDateLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_startDateLabel.Location = New System.Drawing.Point(6, 8)
        Me.m_startDateLabel.Multiline = True
        Me.m_startDateLabel.Name = "m_startDateLabel"
        Me.m_startDateLabel.Size = New System.Drawing.Size(100, 26)
        Me.m_startDateLabel.TabIndex = 14
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
        Me.m_endDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.764706!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.m_endDateLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_endDateLabel.Location = New System.Drawing.Point(6, 76)
        Me.m_endDateLabel.Multiline = True
        Me.m_endDateLabel.Name = "m_endDateLabel"
        Me.m_endDateLabel.Size = New System.Drawing.Size(100, 26)
        Me.m_endDateLabel.TabIndex = 13
        Me.m_endDateLabel.Text = "End date"
        Me.m_endDateLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.m_endDateLabel.UseMnemonics = True
        Me.m_endDateLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_startDate
        '
        Me.m_startDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_startDate.BackColor = System.Drawing.Color.White
        Me.m_startDate.BorderColor = System.Drawing.Color.Black
        Me.m_startDate.Culture = New System.Globalization.CultureInfo("")
        Me.m_startDate.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.m_startDate.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.m_startDate.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.None
        Me.m_startDate.FormatValue = ""
        Me.m_startDate.Location = New System.Drawing.Point(107, 8)
        Me.m_startDate.MaxDate = New Date(2100, 1, 1, 0, 0, 0, 0)
        Me.m_startDate.MinDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.m_startDate.Name = "m_startDate"
        Me.m_startDate.ShowGrip = False
        Me.m_startDate.Size = New System.Drawing.Size(122, 26)
        Me.m_startDate.TabIndex = 12
        Me.m_startDate.Text = "VDatePicker1"
        Me.m_startDate.UseThemeBackColor = False
        Me.m_startDate.UseThemeDropDownArrowColor = True
        Me.m_startDate.Value = New Date(2016, 1, 4, 9, 28, 7, 919)
        Me.m_startDate.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_endDate
        '
        Me.m_endDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_endDate.BackColor = System.Drawing.Color.White
        Me.m_endDate.BorderColor = System.Drawing.Color.Black
        Me.m_endDate.Culture = New System.Globalization.CultureInfo("")
        Me.m_endDate.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.m_endDate.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.m_endDate.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.None
        Me.m_endDate.FormatValue = ""
        Me.m_endDate.Location = New System.Drawing.Point(107, 76)
        Me.m_endDate.MaxDate = New Date(2100, 1, 1, 0, 0, 0, 0)
        Me.m_endDate.MinDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.m_endDate.Name = "m_endDate"
        Me.m_endDate.ShowGrip = False
        Me.m_endDate.Size = New System.Drawing.Size(122, 26)
        Me.m_endDate.TabIndex = 11
        Me.m_endDate.Text = "VDatePicker1"
        Me.m_endDate.UseThemeBackColor = False
        Me.m_endDate.UseThemeDropDownArrowColor = True
        Me.m_endDate.Value = New Date(2016, 1, 4, 9, 27, 20, 177)
        Me.m_endDate.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'PeriodRangeSelectionControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.m_endWeekTB)
        Me.Controls.Add(Me.m_startWeekTB)
        Me.Controls.Add(Me.m_startDateLabel)
        Me.Controls.Add(Me.m_endDateLabel)
        Me.Controls.Add(Me.m_startDate)
        Me.Controls.Add(Me.m_endDate)
        Me.Name = "PeriodRangeSelectionControl"
        Me.Size = New System.Drawing.Size(239, 137)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents m_endWeekTB As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents m_startWeekTB As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents m_startDateLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_endDateLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_startDate As VIBlend.WinForms.Controls.vDatePicker
    Friend WithEvents m_endDate As VIBlend.WinForms.Controls.vDatePicker

End Class
