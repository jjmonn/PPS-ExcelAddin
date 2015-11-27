<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExcelExchangeRatesImportUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ExcelExchangeRatesImportUI))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.m_ratesRangeTextBox = New VIBlend.WinForms.Controls.vTextBox()
        Me.periods_edit_BT = New System.Windows.Forms.Button()
        Me.ButtonsImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.rates_edit_BT = New System.Windows.Forms.Button()
        Me.m_periodsRangeTextBox = New VIBlend.WinForms.Controls.vTextBox()
        Me.m_periodsLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_ratesLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.import_BT = New System.Windows.Forms.Button()
        Me.m_currencyComboBox = New VIBlend.WinForms.Controls.vComboBox()
        Me.m_currencyLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.45122!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.54878!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 46.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.m_ratesRangeTextBox, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.periods_edit_BT, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.rates_edit_BT, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.m_periodsRangeTextBox, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.m_periodsLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.m_ratesLabel, 0, 1)
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
        'm_ratesRangeTextBox
        '
        Me.m_ratesRangeTextBox.BackColor = System.Drawing.Color.White
        Me.m_ratesRangeTextBox.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.m_ratesRangeTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.m_ratesRangeTextBox.DefaultText = "Empty..."
        Me.m_ratesRangeTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_ratesRangeTextBox.Location = New System.Drawing.Point(124, 35)
        Me.m_ratesRangeTextBox.MaxLength = 32767
        Me.m_ratesRangeTextBox.Name = "m_ratesRangeTextBox"
        Me.m_ratesRangeTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.m_ratesRangeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.m_ratesRangeTextBox.SelectionLength = 0
        Me.m_ratesRangeTextBox.SelectionStart = 0
        Me.m_ratesRangeTextBox.Size = New System.Drawing.Size(225, 27)
        Me.m_ratesRangeTextBox.TabIndex = 10
        Me.m_ratesRangeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.m_ratesRangeTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'periods_edit_BT
        '
        Me.periods_edit_BT.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.periods_edit_BT.ImageIndex = 0
        Me.periods_edit_BT.ImageList = Me.ButtonsImageList
        Me.periods_edit_BT.Location = New System.Drawing.Point(354, 2)
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
        Me.rates_edit_BT.Location = New System.Drawing.Point(354, 34)
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
        Me.m_periodsRangeTextBox.Size = New System.Drawing.Size(225, 26)
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
        'm_ratesLabel
        '
        Me.m_ratesLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_ratesLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_ratesLabel.Ellipsis = False
        Me.m_ratesLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_ratesLabel.Location = New System.Drawing.Point(3, 35)
        Me.m_ratesLabel.Multiline = True
        Me.m_ratesLabel.Name = "m_ratesLabel"
        Me.m_ratesLabel.Size = New System.Drawing.Size(80, 25)
        Me.m_ratesLabel.TabIndex = 12
        Me.m_ratesLabel.Text = "Rates"
        Me.m_ratesLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_ratesLabel.UseMnemonics = True
        Me.m_ratesLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'import_BT
        '
        Me.import_BT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.import_BT.ImageIndex = 3
        Me.import_BT.ImageList = Me.ButtonsImageList
        Me.import_BT.Location = New System.Drawing.Point(319, 148)
        Me.import_BT.Name = "import_BT"
        Me.import_BT.Size = New System.Drawing.Size(81, 28)
        Me.import_BT.TabIndex = 6
        Me.import_BT.Text = "Upload"
        Me.import_BT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.import_BT.UseVisualStyleBackColor = True
        '
        'm_currencyComboBox
        '
        Me.m_currencyComboBox.BackColor = System.Drawing.Color.White
        Me.m_currencyComboBox.DisplayMember = ""
        Me.m_currencyComboBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.m_currencyComboBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.m_currencyComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.m_currencyComboBox.DropDownWidth = 108
        Me.m_currencyComboBox.Location = New System.Drawing.Point(137, 25)
        Me.m_currencyComboBox.Name = "m_currencyComboBox"
        Me.m_currencyComboBox.RoundedCornersMaskListItem = CType(15, Byte)
        Me.m_currencyComboBox.Size = New System.Drawing.Size(108, 24)
        Me.m_currencyComboBox.TabIndex = 9
        Me.m_currencyComboBox.UseThemeBackColor = False
        Me.m_currencyComboBox.UseThemeDropDownArrowColor = True
        Me.m_currencyComboBox.ValueMember = ""
        Me.m_currencyComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        Me.m_currencyComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_currencyLabel
        '
        Me.m_currencyLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_currencyLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_currencyLabel.Ellipsis = False
        Me.m_currencyLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_currencyLabel.Location = New System.Drawing.Point(76, 28)
        Me.m_currencyLabel.Multiline = True
        Me.m_currencyLabel.Name = "m_currencyLabel"
        Me.m_currencyLabel.Size = New System.Drawing.Size(53, 25)
        Me.m_currencyLabel.TabIndex = 12
        Me.m_currencyLabel.Text = "Currency"
        Me.m_currencyLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_currencyLabel.UseMnemonics = True
        Me.m_currencyLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'ExcelExchangeRatesImportUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(423, 190)
        Me.Controls.Add(Me.m_currencyLabel)
        Me.Controls.Add(Me.m_currencyComboBox)
        Me.Controls.Add(Me.import_BT)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ExcelExchangeRatesImportUI"
        Me.Text = "Exchange rates upload"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents periods_edit_BT As System.Windows.Forms.Button
    Friend WithEvents rates_edit_BT As System.Windows.Forms.Button
    Friend WithEvents ButtonsImageList As System.Windows.Forms.ImageList
    Friend WithEvents import_BT As System.Windows.Forms.Button
    Friend WithEvents m_ratesRangeTextBox As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents m_periodsRangeTextBox As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents m_periodsLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_ratesLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_currencyComboBox As VIBlend.WinForms.Controls.vComboBox
    Friend WithEvents m_currencyLabel As VIBlend.WinForms.Controls.vLabel
End Class
