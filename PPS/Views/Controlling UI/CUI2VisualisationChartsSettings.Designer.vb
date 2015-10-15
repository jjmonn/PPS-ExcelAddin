<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CUI2VisualisationChartsSettings
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CUI2VisualisationChartsSettings))
        Me.VLabel1 = New VIBlend.WinForms.Controls.vLabel()
        Me.m_chartTitleTextBox = New VIBlend.WinForms.Controls.vTextBox()
        Me.VLabel2 = New VIBlend.WinForms.Controls.vLabel()
        Me.VLabel3 = New VIBlend.WinForms.Controls.vLabel()
        Me.m_serie1ColorPicker = New VIBlend.WinForms.Controls.vColorPicker()
        Me.m_serie2ColorPicker = New VIBlend.WinForms.Controls.vColorPicker()
        Me.VLabel4 = New VIBlend.WinForms.Controls.vLabel()
        Me.VLabel5 = New VIBlend.WinForms.Controls.vLabel()
        Me.m_serie1TypeComboBox = New VIBlend.WinForms.Controls.vComboBox()
        Me.m_serie2TypeComboBox = New VIBlend.WinForms.Controls.vComboBox()
        Me.VLabel6 = New VIBlend.WinForms.Controls.vLabel()
        Me.m_saveButton = New VIBlend.WinForms.Controls.vButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.m_serie1AccountTreeviewBox = New VIBlend.WinForms.Controls.vTreeViewBox()
        Me.m_serie2AccountTreeviewBox = New VIBlend.WinForms.Controls.vTreeViewBox()
        Me.SuspendLayout()
        '
        'VLabel1
        '
        Me.VLabel1.BackColor = System.Drawing.Color.Transparent
        Me.VLabel1.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel1.Ellipsis = False
        Me.VLabel1.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel1.Location = New System.Drawing.Point(17, 24)
        Me.VLabel1.Multiline = True
        Me.VLabel1.Name = "VLabel1"
        Me.VLabel1.Size = New System.Drawing.Size(41, 25)
        Me.VLabel1.TabIndex = 0
        Me.VLabel1.Text = "Title"
        Me.VLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.VLabel1.UseMnemonics = True
        Me.VLabel1.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_chartTitleTextBox
        '
        Me.m_chartTitleTextBox.BackColor = System.Drawing.Color.White
        Me.m_chartTitleTextBox.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.m_chartTitleTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.m_chartTitleTextBox.DefaultText = "Empty..."
        Me.m_chartTitleTextBox.Location = New System.Drawing.Point(86, 26)
        Me.m_chartTitleTextBox.MaxLength = 32767
        Me.m_chartTitleTextBox.Name = "m_chartTitleTextBox"
        Me.m_chartTitleTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.m_chartTitleTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.m_chartTitleTextBox.SelectionLength = 0
        Me.m_chartTitleTextBox.SelectionStart = 0
        Me.m_chartTitleTextBox.Size = New System.Drawing.Size(246, 23)
        Me.m_chartTitleTextBox.TabIndex = 1
        Me.m_chartTitleTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.m_chartTitleTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VLabel2
        '
        Me.VLabel2.BackColor = System.Drawing.Color.Transparent
        Me.VLabel2.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel2.Ellipsis = False
        Me.VLabel2.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel2.Location = New System.Drawing.Point(17, 119)
        Me.VLabel2.Multiline = True
        Me.VLabel2.Name = "VLabel2"
        Me.VLabel2.Size = New System.Drawing.Size(41, 25)
        Me.VLabel2.TabIndex = 2
        Me.VLabel2.Text = "Serie 2"
        Me.VLabel2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.VLabel2.UseMnemonics = True
        Me.VLabel2.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VLabel3
        '
        Me.VLabel3.BackColor = System.Drawing.Color.Transparent
        Me.VLabel3.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel3.Ellipsis = False
        Me.VLabel3.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel3.Location = New System.Drawing.Point(17, 88)
        Me.VLabel3.Multiline = True
        Me.VLabel3.Name = "VLabel3"
        Me.VLabel3.Size = New System.Drawing.Size(41, 25)
        Me.VLabel3.TabIndex = 2
        Me.VLabel3.Text = "Serie 1"
        Me.VLabel3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.VLabel3.UseMnemonics = True
        Me.VLabel3.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_serie1ColorPicker
        '
        Me.m_serie1ColorPicker.BackColor = System.Drawing.Color.White
        Me.m_serie1ColorPicker.BorderColor = System.Drawing.Color.Black
        Me.m_serie1ColorPicker.DropDownHeight = 250
        Me.m_serie1ColorPicker.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.m_serie1ColorPicker.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.m_serie1ColorPicker.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.None
        Me.m_serie1ColorPicker.DropDownWidth = 202
        Me.m_serie1ColorPicker.Location = New System.Drawing.Point(347, 90)
        Me.m_serie1ColorPicker.Name = "m_serie1ColorPicker"
        Me.m_serie1ColorPicker.SelectedColor = System.Drawing.Color.White
        Me.m_serie1ColorPicker.ShowGrip = False
        Me.m_serie1ColorPicker.Size = New System.Drawing.Size(143, 23)
        Me.m_serie1ColorPicker.TabIndex = 4
        Me.m_serie1ColorPicker.Text = "255, 255, 255, 255"
        Me.m_serie1ColorPicker.UseThemeBackColor = False
        Me.m_serie1ColorPicker.UseThemeDropDownArrowColor = True
        Me.m_serie1ColorPicker.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_serie2ColorPicker
        '
        Me.m_serie2ColorPicker.BackColor = System.Drawing.Color.White
        Me.m_serie2ColorPicker.BorderColor = System.Drawing.Color.Black
        Me.m_serie2ColorPicker.DropDownHeight = 250
        Me.m_serie2ColorPicker.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.m_serie2ColorPicker.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.m_serie2ColorPicker.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.None
        Me.m_serie2ColorPicker.DropDownWidth = 202
        Me.m_serie2ColorPicker.Location = New System.Drawing.Point(347, 121)
        Me.m_serie2ColorPicker.Name = "m_serie2ColorPicker"
        Me.m_serie2ColorPicker.SelectedColor = System.Drawing.Color.White
        Me.m_serie2ColorPicker.ShowGrip = False
        Me.m_serie2ColorPicker.Size = New System.Drawing.Size(143, 23)
        Me.m_serie2ColorPicker.TabIndex = 5
        Me.m_serie2ColorPicker.Text = "255, 255, 255, 255"
        Me.m_serie2ColorPicker.UseThemeBackColor = False
        Me.m_serie2ColorPicker.UseThemeDropDownArrowColor = True
        Me.m_serie2ColorPicker.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VLabel4
        '
        Me.VLabel4.BackColor = System.Drawing.Color.Transparent
        Me.VLabel4.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel4.Ellipsis = False
        Me.VLabel4.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel4.Location = New System.Drawing.Point(86, 59)
        Me.VLabel4.Multiline = True
        Me.VLabel4.Name = "VLabel4"
        Me.VLabel4.Size = New System.Drawing.Size(246, 25)
        Me.VLabel4.TabIndex = 6
        Me.VLabel4.Text = "Account"
        Me.VLabel4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.VLabel4.UseMnemonics = True
        Me.VLabel4.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VLabel5
        '
        Me.VLabel5.BackColor = System.Drawing.Color.Transparent
        Me.VLabel5.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel5.Ellipsis = False
        Me.VLabel5.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel5.Location = New System.Drawing.Point(347, 62)
        Me.VLabel5.Multiline = True
        Me.VLabel5.Name = "VLabel5"
        Me.VLabel5.Size = New System.Drawing.Size(143, 25)
        Me.VLabel5.TabIndex = 7
        Me.VLabel5.Text = "Color"
        Me.VLabel5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.VLabel5.UseMnemonics = True
        Me.VLabel5.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_serie1TypeComboBox
        '
        Me.m_serie1TypeComboBox.BackColor = System.Drawing.Color.White
        Me.m_serie1TypeComboBox.DisplayMember = ""
        Me.m_serie1TypeComboBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.m_serie1TypeComboBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.m_serie1TypeComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.m_serie1TypeComboBox.DropDownWidth = 226
        Me.m_serie1TypeComboBox.Location = New System.Drawing.Point(503, 90)
        Me.m_serie1TypeComboBox.Name = "m_serie1TypeComboBox"
        Me.m_serie1TypeComboBox.RoundedCornersMaskListItem = CType(15, Byte)
        Me.m_serie1TypeComboBox.Size = New System.Drawing.Size(226, 23)
        Me.m_serie1TypeComboBox.TabIndex = 8
        Me.m_serie1TypeComboBox.UseThemeBackColor = False
        Me.m_serie1TypeComboBox.UseThemeDropDownArrowColor = True
        Me.m_serie1TypeComboBox.ValueMember = ""
        Me.m_serie1TypeComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        Me.m_serie1TypeComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_serie2TypeComboBox
        '
        Me.m_serie2TypeComboBox.BackColor = System.Drawing.Color.White
        Me.m_serie2TypeComboBox.DisplayMember = ""
        Me.m_serie2TypeComboBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.m_serie2TypeComboBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.m_serie2TypeComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.m_serie2TypeComboBox.DropDownWidth = 226
        Me.m_serie2TypeComboBox.Location = New System.Drawing.Point(503, 121)
        Me.m_serie2TypeComboBox.Name = "m_serie2TypeComboBox"
        Me.m_serie2TypeComboBox.RoundedCornersMaskListItem = CType(15, Byte)
        Me.m_serie2TypeComboBox.Size = New System.Drawing.Size(226, 23)
        Me.m_serie2TypeComboBox.TabIndex = 9
        Me.m_serie2TypeComboBox.UseThemeBackColor = False
        Me.m_serie2TypeComboBox.UseThemeDropDownArrowColor = True
        Me.m_serie2TypeComboBox.ValueMember = ""
        Me.m_serie2TypeComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        Me.m_serie2TypeComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VLabel6
        '
        Me.VLabel6.BackColor = System.Drawing.Color.Transparent
        Me.VLabel6.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel6.Ellipsis = False
        Me.VLabel6.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel6.Location = New System.Drawing.Point(503, 62)
        Me.VLabel6.Multiline = True
        Me.VLabel6.Name = "VLabel6"
        Me.VLabel6.Size = New System.Drawing.Size(226, 25)
        Me.VLabel6.TabIndex = 10
        Me.VLabel6.Text = "Type"
        Me.VLabel6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.VLabel6.UseMnemonics = True
        Me.VLabel6.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_saveButton
        '
        Me.m_saveButton.AllowAnimations = True
        Me.m_saveButton.BackColor = System.Drawing.Color.Transparent
        Me.m_saveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.m_saveButton.ImageKey = "1420498403_340208.ico"
        Me.m_saveButton.ImageList = Me.ImageList1
        Me.m_saveButton.Location = New System.Drawing.Point(649, 165)
        Me.m_saveButton.Name = "m_saveButton"
        Me.m_saveButton.RoundedCornersMask = CType(15, Byte)
        Me.m_saveButton.Size = New System.Drawing.Size(80, 22)
        Me.m_saveButton.TabIndex = 13
        Me.m_saveButton.Text = "Save"
        Me.m_saveButton.UseVisualStyleBackColor = False
        Me.m_saveButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "1420498403_340208.ico")
        '
        'm_serie1AccountTreeviewBox
        '
        Me.m_serie1AccountTreeviewBox.BackColor = System.Drawing.Color.White
        Me.m_serie1AccountTreeviewBox.BorderColor = System.Drawing.Color.Black
        Me.m_serie1AccountTreeviewBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.m_serie1AccountTreeviewBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.m_serie1AccountTreeviewBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.m_serie1AccountTreeviewBox.Location = New System.Drawing.Point(86, 90)
        Me.m_serie1AccountTreeviewBox.Name = "m_serie1AccountTreeviewBox"
        Me.m_serie1AccountTreeviewBox.Size = New System.Drawing.Size(246, 23)
        Me.m_serie1AccountTreeviewBox.TabIndex = 14
        Me.m_serie1AccountTreeviewBox.UseThemeBackColor = False
        Me.m_serie1AccountTreeviewBox.UseThemeDropDownArrowColor = True
        Me.m_serie1AccountTreeviewBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_serie2AccountTreeviewBox
        '
        Me.m_serie2AccountTreeviewBox.BackColor = System.Drawing.Color.White
        Me.m_serie2AccountTreeviewBox.BorderColor = System.Drawing.Color.Black
        Me.m_serie2AccountTreeviewBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.m_serie2AccountTreeviewBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.m_serie2AccountTreeviewBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.m_serie2AccountTreeviewBox.Location = New System.Drawing.Point(86, 121)
        Me.m_serie2AccountTreeviewBox.Name = "m_serie2AccountTreeviewBox"
        Me.m_serie2AccountTreeviewBox.Size = New System.Drawing.Size(246, 23)
        Me.m_serie2AccountTreeviewBox.TabIndex = 15
        Me.m_serie2AccountTreeviewBox.UseThemeBackColor = False
        Me.m_serie2AccountTreeviewBox.UseThemeDropDownArrowColor = True
        Me.m_serie2AccountTreeviewBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'CUI2VisualisationChartsSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(744, 207)
        Me.Controls.Add(Me.m_serie2AccountTreeviewBox)
        Me.Controls.Add(Me.m_serie1AccountTreeviewBox)
        Me.Controls.Add(Me.m_saveButton)
        Me.Controls.Add(Me.VLabel6)
        Me.Controls.Add(Me.m_serie2TypeComboBox)
        Me.Controls.Add(Me.m_serie1TypeComboBox)
        Me.Controls.Add(Me.VLabel5)
        Me.Controls.Add(Me.VLabel4)
        Me.Controls.Add(Me.m_serie2ColorPicker)
        Me.Controls.Add(Me.m_serie1ColorPicker)
        Me.Controls.Add(Me.VLabel3)
        Me.Controls.Add(Me.VLabel2)
        Me.Controls.Add(Me.m_chartTitleTextBox)
        Me.Controls.Add(Me.VLabel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "CUI2VisualisationChartsSettings"
        Me.Text = "Chart Settings"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents VLabel1 As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_chartTitleTextBox As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents VLabel2 As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents VLabel3 As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_serie1ColorPicker As VIBlend.WinForms.Controls.vColorPicker
    Friend WithEvents m_serie2ColorPicker As VIBlend.WinForms.Controls.vColorPicker
    Friend WithEvents VLabel4 As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents VLabel5 As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_serie1TypeComboBox As VIBlend.WinForms.Controls.vComboBox
    Friend WithEvents m_serie2TypeComboBox As VIBlend.WinForms.Controls.vComboBox
    Friend WithEvents VLabel6 As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_saveButton As VIBlend.WinForms.Controls.vButton
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents m_serie1AccountTreeviewBox As VIBlend.WinForms.Controls.vTreeViewBox
    Friend WithEvents m_serie2AccountTreeviewBox As VIBlend.WinForms.Controls.vTreeViewBox
End Class
