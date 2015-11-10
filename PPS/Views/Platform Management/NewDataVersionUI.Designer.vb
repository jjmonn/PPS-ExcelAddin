<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewDataVersionUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NewDataVersionUI))
        Me.m_factVersionLabel = New System.Windows.Forms.Label()
        Me.m_rateVersionLabel = New System.Windows.Forms.Label()
        Me.m_nbYearLabel = New System.Windows.Forms.Label()
        Me.m_startingPeriodLabel = New System.Windows.Forms.Label()
        Me.m_periodConfigLabel = New System.Windows.Forms.Label()
        Me.m_versionNameLabel = New System.Windows.Forms.Label()
        Me.NameTB = New VIBlend.WinForms.Controls.vTextBox()
        Me.TimeConfigCB = New VIBlend.WinForms.Controls.vComboBox()
        Me.m_exchangeRatesVersionVTreeviewbox = New VIBlend.WinForms.Controls.vTreeViewBox()
        Me.m_factsVersionVTreeviewbox = New VIBlend.WinForms.Controls.vTreeViewBox()
        Me.m_versionsTreeviewBox = New VIBlend.WinForms.Controls.vTreeViewBox()
        Me.m_copyCheckBox = New VIBlend.WinForms.Controls.vCheckBox()
        Me.ButtonIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.BigIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.CancelBT = New VIBlend.WinForms.Controls.vButton()
        Me.CreateVersionBT = New VIBlend.WinForms.Controls.vButton()
        Me.m_startingMonthLabel = New System.Windows.Forms.Label()
        Me.m_startingMonthCombobox = New VIBlend.WinForms.Controls.vComboBox()
        Me.NbPeriodsNUD = New VIBlend.WinForms.Controls.vNumericUpDown()
        Me.StartingPeriodNUD = New VIBlend.WinForms.Controls.vNumericUpDown()
        Me.SuspendLayout()
        '
        'm_factVersionLabel
        '
        Me.m_factVersionLabel.AutoSize = True
        Me.m_factVersionLabel.Location = New System.Drawing.Point(34, 327)
        Me.m_factVersionLabel.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.m_factVersionLabel.Name = "m_factVersionLabel"
        Me.m_factVersionLabel.Size = New System.Drawing.Size(142, 13)
        Me.m_factVersionLabel.TabIndex = 30
        Me.m_factVersionLabel.Text = "[facts_versions.fact_version]"
        '
        'm_rateVersionLabel
        '
        Me.m_rateVersionLabel.AutoSize = True
        Me.m_rateVersionLabel.Location = New System.Drawing.Point(34, 291)
        Me.m_rateVersionLabel.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.m_rateVersionLabel.Name = "m_rateVersionLabel"
        Me.m_rateVersionLabel.Size = New System.Drawing.Size(200, 13)
        Me.m_rateVersionLabel.TabIndex = 28
        Me.m_rateVersionLabel.Text = "[facts_versions.exchange_rates_version]"
        '
        'm_nbYearLabel
        '
        Me.m_nbYearLabel.AutoSize = True
        Me.m_nbYearLabel.Location = New System.Drawing.Point(34, 255)
        Me.m_nbYearLabel.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.m_nbYearLabel.Name = "m_nbYearLabel"
        Me.m_nbYearLabel.Size = New System.Drawing.Size(127, 13)
        Me.m_nbYearLabel.TabIndex = 25
        Me.m_nbYearLabel.Text = "[facts_versions.nb_years]"
        '
        'm_startingPeriodLabel
        '
        Me.m_startingPeriodLabel.AutoSize = True
        Me.m_startingPeriodLabel.Location = New System.Drawing.Point(34, 170)
        Me.m_startingPeriodLabel.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.m_startingPeriodLabel.Name = "m_startingPeriodLabel"
        Me.m_startingPeriodLabel.Size = New System.Drawing.Size(153, 13)
        Me.m_startingPeriodLabel.TabIndex = 17
        Me.m_startingPeriodLabel.Text = "[facts_versions.starting_period]"
        '
        'm_periodConfigLabel
        '
        Me.m_periodConfigLabel.AutoSize = True
        Me.m_periodConfigLabel.Location = New System.Drawing.Point(34, 125)
        Me.m_periodConfigLabel.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.m_periodConfigLabel.Name = "m_periodConfigLabel"
        Me.m_periodConfigLabel.Size = New System.Drawing.Size(148, 13)
        Me.m_periodConfigLabel.TabIndex = 15
        Me.m_periodConfigLabel.Text = "[facts_versions.period_config]"
        '
        'm_versionNameLabel
        '
        Me.m_versionNameLabel.AutoSize = True
        Me.m_versionNameLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.m_versionNameLabel.Location = New System.Drawing.Point(34, 36)
        Me.m_versionNameLabel.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.m_versionNameLabel.Name = "m_versionNameLabel"
        Me.m_versionNameLabel.Size = New System.Drawing.Size(179, 13)
        Me.m_versionNameLabel.TabIndex = 7
        Me.m_versionNameLabel.Text = "[facts_versions.version_name]"
        '
        'NameTB
        '
        Me.NameTB.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NameTB.BackColor = System.Drawing.Color.White
        Me.NameTB.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.NameTB.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.NameTB.DefaultText = "Empty..."
        Me.NameTB.Location = New System.Drawing.Point(347, 36)
        Me.NameTB.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.NameTB.MaximumSize = New System.Drawing.Size(400, 4)
        Me.NameTB.MaxLength = 32767
        Me.NameTB.MinimumSize = New System.Drawing.Size(280, 20)
        Me.NameTB.Name = "NameTB"
        Me.NameTB.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.NameTB.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.NameTB.SelectionLength = 0
        Me.NameTB.SelectionStart = 0
        Me.NameTB.Size = New System.Drawing.Size(280, 20)
        Me.NameTB.TabIndex = 13
        Me.NameTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.NameTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        '
        'TimeConfigCB
        '
        Me.TimeConfigCB.BackColor = System.Drawing.Color.White
        Me.TimeConfigCB.DisplayMember = ""
        Me.TimeConfigCB.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.TimeConfigCB.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.TimeConfigCB.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.TimeConfigCB.DropDownWidth = 280
        Me.TimeConfigCB.Location = New System.Drawing.Point(347, 123)
        Me.TimeConfigCB.Name = "TimeConfigCB"
        Me.TimeConfigCB.RoundedCornersMaskListItem = CType(15, Byte)
        Me.TimeConfigCB.Size = New System.Drawing.Size(280, 21)
        Me.TimeConfigCB.TabIndex = 16
        Me.TimeConfigCB.UseThemeBackColor = False
        Me.TimeConfigCB.UseThemeDropDownArrowColor = True
        Me.TimeConfigCB.ValueMember = ""
        Me.TimeConfigCB.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        Me.TimeConfigCB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        '
        'm_exchangeRatesVersionVTreeviewbox
        '
        Me.m_exchangeRatesVersionVTreeviewbox.BackColor = System.Drawing.Color.White
        Me.m_exchangeRatesVersionVTreeviewbox.BorderColor = System.Drawing.Color.Black
        Me.m_exchangeRatesVersionVTreeviewbox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.m_exchangeRatesVersionVTreeviewbox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.m_exchangeRatesVersionVTreeviewbox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.m_exchangeRatesVersionVTreeviewbox.Location = New System.Drawing.Point(347, 289)
        Me.m_exchangeRatesVersionVTreeviewbox.Name = "m_exchangeRatesVersionVTreeviewbox"
        Me.m_exchangeRatesVersionVTreeviewbox.Size = New System.Drawing.Size(280, 23)
        Me.m_exchangeRatesVersionVTreeviewbox.TabIndex = 31
        Me.m_exchangeRatesVersionVTreeviewbox.UseThemeBackColor = False
        Me.m_exchangeRatesVersionVTreeviewbox.UseThemeDropDownArrowColor = True
        Me.m_exchangeRatesVersionVTreeviewbox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        '
        'm_factsVersionVTreeviewbox
        '
        Me.m_factsVersionVTreeviewbox.BackColor = System.Drawing.Color.White
        Me.m_factsVersionVTreeviewbox.BorderColor = System.Drawing.Color.Black
        Me.m_factsVersionVTreeviewbox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.m_factsVersionVTreeviewbox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.m_factsVersionVTreeviewbox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.m_factsVersionVTreeviewbox.Location = New System.Drawing.Point(347, 325)
        Me.m_factsVersionVTreeviewbox.Name = "m_factsVersionVTreeviewbox"
        Me.m_factsVersionVTreeviewbox.Size = New System.Drawing.Size(280, 23)
        Me.m_factsVersionVTreeviewbox.TabIndex = 32
        Me.m_factsVersionVTreeviewbox.UseThemeBackColor = False
        Me.m_factsVersionVTreeviewbox.UseThemeDropDownArrowColor = True
        Me.m_factsVersionVTreeviewbox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        '
        'm_versionsTreeviewBox
        '
        Me.m_versionsTreeviewBox.BackColor = System.Drawing.Color.White
        Me.m_versionsTreeviewBox.BorderColor = System.Drawing.Color.Black
        Me.m_versionsTreeviewBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.m_versionsTreeviewBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.m_versionsTreeviewBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.m_versionsTreeviewBox.Location = New System.Drawing.Point(347, 78)
        Me.m_versionsTreeviewBox.Name = "m_versionsTreeviewBox"
        Me.m_versionsTreeviewBox.Size = New System.Drawing.Size(280, 23)
        Me.m_versionsTreeviewBox.TabIndex = 33
        Me.m_versionsTreeviewBox.Text = "VTreeViewBox1"
        Me.m_versionsTreeviewBox.UseThemeBackColor = False
        Me.m_versionsTreeviewBox.UseThemeDropDownArrowColor = True
        Me.m_versionsTreeviewBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        '
        'm_copyCheckBox
        '
        Me.m_copyCheckBox.BackColor = System.Drawing.Color.Transparent
        Me.m_copyCheckBox.Location = New System.Drawing.Point(37, 76)
        Me.m_copyCheckBox.Name = "m_copyCheckBox"
        Me.m_copyCheckBox.Size = New System.Drawing.Size(175, 24)
        Me.m_copyCheckBox.TabIndex = 34
        Me.m_copyCheckBox.Text = "Create copy from"
        Me.m_copyCheckBox.UseVisualStyleBackColor = False
        Me.m_copyCheckBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        '
        'ButtonIcons
        '
        Me.ButtonIcons.ImageStream = CType(resources.GetObject("ButtonIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonIcons.Images.SetKeyName(0, "favicon(81) (1).ico")
        Me.ButtonIcons.Images.SetKeyName(1, "imageres_89.ico")
        Me.ButtonIcons.Images.SetKeyName(2, "1420498403_340208.ico")
        '
        'BigIcons
        '
        Me.BigIcons.ImageStream = CType(resources.GetObject("BigIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.BigIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.BigIcons.Images.SetKeyName(0, "favicon(230).ico")
        '
        'CancelBT
        '
        Me.CancelBT.AllowAnimations = True
        Me.CancelBT.BackColor = System.Drawing.Color.Transparent
        Me.CancelBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CancelBT.ImageKey = "imageres_89.ico"
        Me.CancelBT.ImageList = Me.ButtonIcons
        Me.CancelBT.Location = New System.Drawing.Point(348, 379)
        Me.CancelBT.Name = "CancelBT"
        Me.CancelBT.RoundedCornersMask = CType(15, Byte)
        Me.CancelBT.Size = New System.Drawing.Size(119, 30)
        Me.CancelBT.TabIndex = 23
        Me.CancelBT.Text = "[general.cancel]"
        Me.CancelBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CancelBT.UseVisualStyleBackColor = True
        Me.CancelBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        '
        'CreateVersionBT
        '
        Me.CreateVersionBT.AllowAnimations = True
        Me.CreateVersionBT.BackColor = System.Drawing.Color.Transparent
        Me.CreateVersionBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CreateVersionBT.ImageKey = "1420498403_340208.ico"
        Me.CreateVersionBT.ImageList = Me.ButtonIcons
        Me.CreateVersionBT.Location = New System.Drawing.Point(508, 379)
        Me.CreateVersionBT.Name = "CreateVersionBT"
        Me.CreateVersionBT.RoundedCornersMask = CType(15, Byte)
        Me.CreateVersionBT.Size = New System.Drawing.Size(119, 30)
        Me.CreateVersionBT.TabIndex = 22
        Me.CreateVersionBT.Text = "[general.create]"
        Me.CreateVersionBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CreateVersionBT.UseVisualStyleBackColor = True
        Me.CreateVersionBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        '
        'm_startingMonthLabel
        '
        Me.m_startingMonthLabel.AutoSize = True
        Me.m_startingMonthLabel.Location = New System.Drawing.Point(34, 209)
        Me.m_startingMonthLabel.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.m_startingMonthLabel.Name = "m_startingMonthLabel"
        Me.m_startingMonthLabel.Size = New System.Drawing.Size(153, 13)
        Me.m_startingMonthLabel.TabIndex = 35
        Me.m_startingMonthLabel.Text = "[facts_versions.starting_month]"
        '
        'm_startingMonthCombobox
        '
        Me.m_startingMonthCombobox.BackColor = System.Drawing.Color.White
        Me.m_startingMonthCombobox.DisplayMember = ""
        Me.m_startingMonthCombobox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.m_startingMonthCombobox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.m_startingMonthCombobox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.m_startingMonthCombobox.DropDownWidth = 280
        Me.m_startingMonthCombobox.Location = New System.Drawing.Point(347, 208)
        Me.m_startingMonthCombobox.Name = "m_startingMonthCombobox"
        Me.m_startingMonthCombobox.RoundedCornersMaskListItem = CType(15, Byte)
        Me.m_startingMonthCombobox.Size = New System.Drawing.Size(280, 21)
        Me.m_startingMonthCombobox.TabIndex = 36
        Me.m_startingMonthCombobox.UseThemeBackColor = False
        Me.m_startingMonthCombobox.UseThemeDropDownArrowColor = True
        Me.m_startingMonthCombobox.ValueMember = ""
        Me.m_startingMonthCombobox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        Me.m_startingMonthCombobox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        '
        'NbPeriodsNUD
        '
        Me.NbPeriodsNUD.BackColor = System.Drawing.Color.White
        Me.NbPeriodsNUD.DropDownArrowBackgroundEnabled = True
        Me.NbPeriodsNUD.EnableBorderHighlight = False
        Me.NbPeriodsNUD.Location = New System.Drawing.Point(348, 247)
        Me.NbPeriodsNUD.MaxLength = 32767
        Me.NbPeriodsNUD.Name = "NbPeriodsNUD"
        Me.NbPeriodsNUD.OverrideBackColor = System.Drawing.Color.White
        Me.NbPeriodsNUD.OverrideBorderColor = System.Drawing.Color.Gray
        Me.NbPeriodsNUD.OverrideFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.NbPeriodsNUD.OverrideForeColor = System.Drawing.Color.Black
        Me.NbPeriodsNUD.Size = New System.Drawing.Size(279, 22)
        Me.NbPeriodsNUD.TabIndex = 37
        Me.NbPeriodsNUD.UseThemeForeColor = True
        Me.NbPeriodsNUD.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        '
        'StartingPeriodNUD
        '
        Me.StartingPeriodNUD.BackColor = System.Drawing.Color.White
        Me.StartingPeriodNUD.DropDownArrowBackgroundEnabled = True
        Me.StartingPeriodNUD.EnableBorderHighlight = False
        Me.StartingPeriodNUD.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.StartingPeriodNUD.Location = New System.Drawing.Point(347, 172)
        Me.StartingPeriodNUD.Maximum = 2100
        Me.StartingPeriodNUD.MaxLength = 32767
        Me.StartingPeriodNUD.Name = "StartingPeriodNUD"
        Me.StartingPeriodNUD.OverrideBackColor = System.Drawing.Color.White
        Me.StartingPeriodNUD.OverrideBorderColor = System.Drawing.Color.Gray
        Me.StartingPeriodNUD.OverrideFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.StartingPeriodNUD.OverrideForeColor = System.Drawing.Color.Black
        Me.StartingPeriodNUD.Size = New System.Drawing.Size(280, 22)
        Me.StartingPeriodNUD.TabIndex = 38
        Me.StartingPeriodNUD.UseThemeForeColor = True
        Me.StartingPeriodNUD.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        '
        'NewDataVersionUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(672, 439)
        Me.Controls.Add(Me.StartingPeriodNUD)
        Me.Controls.Add(Me.NbPeriodsNUD)
        Me.Controls.Add(Me.m_startingMonthCombobox)
        Me.Controls.Add(Me.m_startingMonthLabel)
        Me.Controls.Add(Me.m_versionNameLabel)
        Me.Controls.Add(Me.NameTB)
        Me.Controls.Add(Me.m_factVersionLabel)
        Me.Controls.Add(Me.m_copyCheckBox)
        Me.Controls.Add(Me.CancelBT)
        Me.Controls.Add(Me.m_versionsTreeviewBox)
        Me.Controls.Add(Me.m_rateVersionLabel)
        Me.Controls.Add(Me.m_periodConfigLabel)
        Me.Controls.Add(Me.CreateVersionBT)
        Me.Controls.Add(Me.TimeConfigCB)
        Me.Controls.Add(Me.m_nbYearLabel)
        Me.Controls.Add(Me.m_startingPeriodLabel)
        Me.Controls.Add(Me.m_factsVersionVTreeviewbox)
        Me.Controls.Add(Me.m_exchangeRatesVersionVTreeviewbox)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "NewDataVersionUI"
        Me.Text = "[facts_versions.version_new]"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents m_startingPeriodLabel As System.Windows.Forms.Label
    Friend WithEvents m_periodConfigLabel As System.Windows.Forms.Label
    Friend WithEvents m_versionNameLabel As System.Windows.Forms.Label
    Friend WithEvents NameTB As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents TimeConfigCB As VIBlend.WinForms.Controls.vComboBox
    Friend WithEvents CancelBT As VIBlend.WinForms.Controls.vButton
    Friend WithEvents CreateVersionBT As VIBlend.WinForms.Controls.vButton
    Friend WithEvents ButtonIcons As System.Windows.Forms.ImageList
    Friend WithEvents BigIcons As System.Windows.Forms.ImageList
    Friend WithEvents m_nbYearLabel As System.Windows.Forms.Label
    Friend WithEvents m_rateVersionLabel As System.Windows.Forms.Label
    Friend WithEvents m_factVersionLabel As System.Windows.Forms.Label
    Friend WithEvents m_exchangeRatesVersionVTreeviewbox As VIBlend.WinForms.Controls.vTreeViewBox
    Friend WithEvents m_factsVersionVTreeviewbox As VIBlend.WinForms.Controls.vTreeViewBox
    Friend WithEvents m_versionsTreeviewBox As VIBlend.WinForms.Controls.vTreeViewBox
    Friend WithEvents m_copyCheckBox As VIBlend.WinForms.Controls.vCheckBox
    Friend WithEvents m_startingMonthLabel As System.Windows.Forms.Label
    Friend WithEvents m_startingMonthCombobox As VIBlend.WinForms.Controls.vComboBox
    Friend WithEvents NbPeriodsNUD As VIBlend.WinForms.Controls.vNumericUpDown
    Friend WithEvents StartingPeriodNUD As VIBlend.WinForms.Controls.vNumericUpDown
End Class
