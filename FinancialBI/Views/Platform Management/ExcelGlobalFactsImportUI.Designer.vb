<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExcelFactsValuesImportUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ExcelFactsValuesImportUI))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.m_factsRangeTextBox = New VIBlend.WinForms.Controls.vTextBox()
        Me.periods_edit_BT = New System.Windows.Forms.Button()
        Me.ButtonsImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.rates_edit_BT = New System.Windows.Forms.Button()
        Me.m_periodsRangeTextBox = New VIBlend.WinForms.Controls.vTextBox()
        Me.m_periodsLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_valuesLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.import_BT = New System.Windows.Forms.Button()
        Me.m_factsComboBox = New VIBlend.WinForms.Controls.vComboBox()
        Me.m_globalFactLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.45122!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.54878!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 47.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.m_factsRangeTextBox, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.periods_edit_BT, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.rates_edit_BT, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.m_periodsRangeTextBox, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.m_periodsLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.m_valuesLabel, 0, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(12, 67)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(399, 65)
        Me.TableLayoutPanel1.TabIndex = 8
        '
        'm_factsRangeTextBox
        '
        Me.m_factsRangeTextBox.BackColor = System.Drawing.Color.White
        Me.m_factsRangeTextBox.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.m_factsRangeTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.m_factsRangeTextBox.DefaultText = "Empty..."
        Me.m_factsRangeTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_factsRangeTextBox.Location = New System.Drawing.Point(124, 35)
        Me.m_factsRangeTextBox.MaxLength = 32767
        Me.m_factsRangeTextBox.Name = "m_factsRangeTextBox"
        Me.m_factsRangeTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.m_factsRangeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.m_factsRangeTextBox.SelectionLength = 0
        Me.m_factsRangeTextBox.SelectionStart = 0
        Me.m_factsRangeTextBox.Size = New System.Drawing.Size(224, 27)
        Me.m_factsRangeTextBox.TabIndex = 10
        Me.m_factsRangeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.m_factsRangeTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'periods_edit_BT
        '
        Me.periods_edit_BT.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.periods_edit_BT.ImageIndex = 0
        Me.periods_edit_BT.ImageList = Me.ButtonsImageList
        Me.periods_edit_BT.Location = New System.Drawing.Point(353, 2)
        Me.periods_edit_BT.Margin = New System.Windows.Forms.Padding(2)
        Me.periods_edit_BT.Name = "periods_edit_BT"
        Me.periods_edit_BT.Size = New System.Drawing.Size(27, 27)
        Me.periods_edit_BT.TabIndex = 2
        Me.periods_edit_BT.UseVisualStyleBackColor = True
        '
        'ButtonsImageList
        '
        Me.ButtonsImageList.ImageStream = CType(resources.GetObject("ButtonsImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonsImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonsImageList.Images.SetKeyName(0, "favicon(161).ico")
        Me.ButtonsImageList.Images.SetKeyName(1, "favicon(132).ico")
        Me.ButtonsImageList.Images.SetKeyName(2, "favicon(76).ico")
        Me.ButtonsImageList.Images.SetKeyName(3, "1420498403_340208.ico")
        '
        'rates_edit_BT
        '
        Me.rates_edit_BT.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.rates_edit_BT.ImageIndex = 0
        Me.rates_edit_BT.ImageList = Me.ButtonsImageList
        Me.rates_edit_BT.Location = New System.Drawing.Point(353, 34)
        Me.rates_edit_BT.Margin = New System.Windows.Forms.Padding(2)
        Me.rates_edit_BT.Name = "rates_edit_BT"
        Me.rates_edit_BT.Size = New System.Drawing.Size(27, 27)
        Me.rates_edit_BT.TabIndex = 4
        Me.rates_edit_BT.UseVisualStyleBackColor = True
        '
        'm_periodsRangeTextBox
        '
        Me.m_periodsRangeTextBox.BackColor = System.Drawing.Color.White
        Me.m_periodsRangeTextBox.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.m_periodsRangeTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.m_periodsRangeTextBox.DefaultText = "Empty..."
        Me.m_periodsRangeTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_periodsRangeTextBox.Location = New System.Drawing.Point(124, 3)
        Me.m_periodsRangeTextBox.MaxLength = 32767
        Me.m_periodsRangeTextBox.Name = "m_periodsRangeTextBox"
        Me.m_periodsRangeTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.m_periodsRangeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.m_periodsRangeTextBox.SelectionLength = 0
        Me.m_periodsRangeTextBox.SelectionStart = 0
        Me.m_periodsRangeTextBox.Size = New System.Drawing.Size(224, 26)
        Me.m_periodsRangeTextBox.TabIndex = 9
        Me.m_periodsRangeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.m_periodsRangeTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_periodsLabel
        '
        Me.m_periodsLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_periodsLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_periodsLabel.Ellipsis = False
        Me.m_periodsLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_periodsLabel.Location = New System.Drawing.Point(3, 3)
        Me.m_periodsLabel.Multiline = True
        Me.m_periodsLabel.Name = "m_periodsLabel"
        Me.m_periodsLabel.Size = New System.Drawing.Size(80, 25)
        Me.m_periodsLabel.TabIndex = 11
        Me.m_periodsLabel.Text = "Periods"
        Me.m_periodsLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_periodsLabel.UseMnemonics = True
        Me.m_periodsLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_valuesLabel
        '
        Me.m_valuesLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_valuesLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_valuesLabel.Ellipsis = False
        Me.m_valuesLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_valuesLabel.Location = New System.Drawing.Point(3, 35)
        Me.m_valuesLabel.Multiline = True
        Me.m_valuesLabel.Name = "m_valuesLabel"
        Me.m_valuesLabel.Size = New System.Drawing.Size(80, 25)
        Me.m_valuesLabel.TabIndex = 12
        Me.m_valuesLabel.Text = "Values"
        Me.m_valuesLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_valuesLabel.UseMnemonics = True
        Me.m_valuesLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'import_BT
        '
        Me.import_BT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.import_BT.ImageIndex = 3
        Me.import_BT.ImageList = Me.ButtonsImageList
        Me.import_BT.Location = New System.Drawing.Point(302, 148)
        Me.import_BT.Name = "import_BT"
        Me.import_BT.Size = New System.Drawing.Size(91, 28)
        Me.import_BT.TabIndex = 6
        Me.import_BT.Text = "Upload"
        Me.import_BT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.import_BT.UseVisualStyleBackColor = True
        '
        'm_factsComboBox
        '
        Me.m_factsComboBox.BackColor = System.Drawing.Color.White
        Me.m_factsComboBox.DisplayMember = ""
        Me.m_factsComboBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.m_factsComboBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.m_factsComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.m_factsComboBox.DropDownWidth = 202
        Me.m_factsComboBox.Location = New System.Drawing.Point(160, 24)
        Me.m_factsComboBox.Name = "m_factsComboBox"
        Me.m_factsComboBox.RoundedCornersMaskListItem = CType(15, Byte)
        Me.m_factsComboBox.Size = New System.Drawing.Size(202, 24)
        Me.m_factsComboBox.TabIndex = 9
        Me.m_factsComboBox.UseThemeBackColor = False
        Me.m_factsComboBox.UseThemeDropDownArrowColor = True
        Me.m_factsComboBox.ValueMember = ""
        Me.m_factsComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        Me.m_factsComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_globalFactLabel
        '
        Me.m_globalFactLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_globalFactLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_globalFactLabel.Ellipsis = False
        Me.m_globalFactLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_globalFactLabel.Location = New System.Drawing.Point(15, 28)
        Me.m_globalFactLabel.Multiline = True
        Me.m_globalFactLabel.Name = "m_globalFactLabel"
        Me.m_globalFactLabel.Size = New System.Drawing.Size(135, 25)
        Me.m_globalFactLabel.TabIndex = 12
        Me.m_globalFactLabel.Text = "Macro economic indicator"
        Me.m_globalFactLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_globalFactLabel.UseMnemonics = True
        Me.m_globalFactLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'ExcelFactsValuesImportUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(423, 190)
        Me.Controls.Add(Me.m_globalFactLabel)
        Me.Controls.Add(Me.m_factsComboBox)
        Me.Controls.Add(Me.import_BT)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ExcelFactsValuesImportUI"
        Me.Text = "Global facts upload"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents periods_edit_BT As System.Windows.Forms.Button
    Friend WithEvents rates_edit_BT As System.Windows.Forms.Button
    Friend WithEvents ButtonsImageList As System.Windows.Forms.ImageList
    Friend WithEvents import_BT As System.Windows.Forms.Button
    Friend WithEvents m_factsRangeTextBox As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents m_periodsRangeTextBox As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents m_periodsLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_valuesLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_factsComboBox As VIBlend.WinForms.Controls.vComboBox
    Friend WithEvents m_globalFactLabel As VIBlend.WinForms.Controls.vLabel
End Class
