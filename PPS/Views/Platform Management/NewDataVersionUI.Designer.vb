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
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.m_factsVersionLabel = New System.Windows.Forms.Label()
        Me.m_ratesVersionLabel = New System.Windows.Forms.Label()
        Me.m_numberOfYearsLabel = New System.Windows.Forms.Label()
        Me.m_startingYearLabel = New System.Windows.Forms.Label()
        Me.m_periodConfigLabel = New System.Windows.Forms.Label()
        Me.m_nameLabel = New System.Windows.Forms.Label()
        Me.NameTB = New System.Windows.Forms.TextBox()
        Me.TimeConfigCB = New System.Windows.Forms.ComboBox()
        Me.NbPeriodsNUD = New System.Windows.Forms.NumericUpDown()
        Me.StartingPeriodNUD = New System.Windows.Forms.NumericUpDown()
        Me.m_exchangeRatesVersionVTreeviewbox = New VIBlend.WinForms.Controls.vTreeViewBox()
        Me.m_factsVersionVTreeviewbox = New VIBlend.WinForms.Controls.vTreeViewBox()
        Me.m_versionsTreeviewBox = New VIBlend.WinForms.Controls.vTreeViewBox()
        Me.ButtonIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.BigIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CancelBT = New System.Windows.Forms.Button()
        Me.CreateVersionBT = New System.Windows.Forms.Button()
        Me.m_copyCheckBox = New VIBlend.WinForms.Controls.vCheckBox()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.NbPeriodsNUD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StartingPeriodNUD, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.35376!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 61.64624!))
        Me.TableLayoutPanel2.Controls.Add(Me.m_factsVersionLabel, 0, 6)
        Me.TableLayoutPanel2.Controls.Add(Me.m_ratesVersionLabel, 0, 5)
        Me.TableLayoutPanel2.Controls.Add(Me.m_numberOfYearsLabel, 0, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.m_startingYearLabel, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.m_periodConfigLabel, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.m_nameLabel, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.NameTB, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TimeConfigCB, 1, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.NbPeriodsNUD, 1, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.StartingPeriodNUD, 1, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.m_exchangeRatesVersionVTreeviewbox, 1, 5)
        Me.TableLayoutPanel2.Controls.Add(Me.m_factsVersionVTreeviewbox, 1, 6)
        Me.TableLayoutPanel2.Controls.Add(Me.m_versionsTreeviewBox, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.m_copyCheckBox, 0, 1)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(32, 27)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 7
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(586, 251)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'm_factsVersionLabel
        '
        Me.m_factsVersionLabel.AutoSize = True
        Me.m_factsVersionLabel.Location = New System.Drawing.Point(3, 223)
        Me.m_factsVersionLabel.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.m_factsVersionLabel.Name = "m_factsVersionLabel"
        Me.m_factsVersionLabel.Size = New System.Drawing.Size(158, 15)
        Me.m_factsVersionLabel.TabIndex = 30
        Me.m_factsVersionLabel.Text = "fact_version"
        '
        'm_ratesVersionLabel
        '
        Me.m_ratesVersionLabel.AutoSize = True
        Me.m_ratesVersionLabel.Location = New System.Drawing.Point(3, 187)
        Me.m_ratesVersionLabel.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.m_ratesVersionLabel.Name = "m_ratesVersionLabel"
        Me.m_ratesVersionLabel.Size = New System.Drawing.Size(217, 29)
        Me.m_ratesVersionLabel.TabIndex = 28
        Me.m_ratesVersionLabel.Text = "exchange_rates_version"
        '
        'Label1
        '
        Me.m_numberOfYearsLabel.AutoSize = True
        Me.m_numberOfYearsLabel.Location = New System.Drawing.Point(3, 151)
        Me.m_numberOfYearsLabel.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.m_numberOfYearsLabel.Name = "Label1"
        Me.m_numberOfYearsLabel.Size = New System.Drawing.Size(143, 15)
        Me.m_numberOfYearsLabel.TabIndex = 25
        Me.m_numberOfYearsLabel.Text = "nb_years"
        '
        'm_startingYearLabel
        '
        Me.m_startingYearLabel.AutoSize = True
        Me.m_startingYearLabel.Location = New System.Drawing.Point(3, 115)
        Me.m_startingYearLabel.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.m_startingYearLabel.Name = "m_startingYearLabel"
        Me.m_startingYearLabel.Size = New System.Drawing.Size(175, 15)
        Me.m_startingYearLabel.TabIndex = 17
        Me.m_startingYearLabel.Text = "starting_period"
        '
        'm_periodConfigLabel
        '
        Me.m_periodConfigLabel.AutoSize = True
        Me.m_periodConfigLabel.Location = New System.Drawing.Point(3, 79)
        Me.m_periodConfigLabel.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.m_periodConfigLabel.Name = "m_periodConfigLabel"
        Me.m_periodConfigLabel.Size = New System.Drawing.Size(168, 15)
        Me.m_periodConfigLabel.TabIndex = 15
        Me.m_periodConfigLabel.Text = "period_config"
        '
        'm_nameLabel
        '
        Me.m_nameLabel.AutoSize = True
        Me.m_nameLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.m_nameLabel.Location = New System.Drawing.Point(3, 7)
        Me.m_nameLabel.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.m_nameLabel.Name = "m_nameLabel"
        Me.m_nameLabel.Size = New System.Drawing.Size(200, 15)
        Me.m_nameLabel.TabIndex = 7
        Me.m_nameLabel.Text = "version_name"
        '
        'NameTB
        '
        Me.NameTB.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NameTB.Location = New System.Drawing.Point(227, 5)
        Me.NameTB.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.NameTB.MaximumSize = New System.Drawing.Size(400, 4)
        Me.NameTB.MinimumSize = New System.Drawing.Size(280, 20)
        Me.NameTB.Name = "NameTB"
        Me.NameTB.Size = New System.Drawing.Size(356, 20)
        Me.NameTB.TabIndex = 13
        '
        'TimeConfigCB
        '
        Me.TimeConfigCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TimeConfigCB.FormattingEnabled = True
        Me.TimeConfigCB.Location = New System.Drawing.Point(227, 75)
        Me.TimeConfigCB.Name = "TimeConfigCB"
        Me.TimeConfigCB.Size = New System.Drawing.Size(143, 21)
        Me.TimeConfigCB.TabIndex = 16
        '
        'NbPeriodsNUD
        '
        Me.NbPeriodsNUD.Location = New System.Drawing.Point(227, 147)
        Me.NbPeriodsNUD.Maximum = New Decimal(New Integer() {70, 0, 0, 0})
        Me.NbPeriodsNUD.Name = "NbPeriodsNUD"
        Me.NbPeriodsNUD.Size = New System.Drawing.Size(139, 20)
        Me.NbPeriodsNUD.TabIndex = 26
        '
        'StartingPeriodNUD
        '
        Me.StartingPeriodNUD.Location = New System.Drawing.Point(227, 111)
        Me.StartingPeriodNUD.Maximum = New Decimal(New Integer() {3000, 0, 0, 0})
        Me.StartingPeriodNUD.Name = "StartingPeriodNUD"
        Me.StartingPeriodNUD.Size = New System.Drawing.Size(139, 20)
        Me.StartingPeriodNUD.TabIndex = 27
        '
        'm_exchangeRatesVersionVTreeviewbox
        '
        Me.m_exchangeRatesVersionVTreeviewbox.BackColor = System.Drawing.Color.White
        Me.m_exchangeRatesVersionVTreeviewbox.BorderColor = System.Drawing.Color.Black
        Me.m_exchangeRatesVersionVTreeviewbox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.m_exchangeRatesVersionVTreeviewbox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.m_exchangeRatesVersionVTreeviewbox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.m_exchangeRatesVersionVTreeviewbox.Location = New System.Drawing.Point(227, 183)
        Me.m_exchangeRatesVersionVTreeviewbox.Name = "m_exchangeRatesVersionVTreeviewbox"
        Me.m_exchangeRatesVersionVTreeviewbox.Size = New System.Drawing.Size(227, 23)
        Me.m_exchangeRatesVersionVTreeviewbox.TabIndex = 31
        Me.m_exchangeRatesVersionVTreeviewbox.UseThemeBackColor = False
        Me.m_exchangeRatesVersionVTreeviewbox.UseThemeDropDownArrowColor = True
        Me.m_exchangeRatesVersionVTreeviewbox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_factsVersionVTreeviewbox
        '
        Me.m_factsVersionVTreeviewbox.BackColor = System.Drawing.Color.White
        Me.m_factsVersionVTreeviewbox.BorderColor = System.Drawing.Color.Black
        Me.m_factsVersionVTreeviewbox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.m_factsVersionVTreeviewbox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.m_factsVersionVTreeviewbox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.m_factsVersionVTreeviewbox.Location = New System.Drawing.Point(227, 219)
        Me.m_factsVersionVTreeviewbox.Name = "m_factsVersionVTreeviewbox"
        Me.m_factsVersionVTreeviewbox.Size = New System.Drawing.Size(227, 23)
        Me.m_factsVersionVTreeviewbox.TabIndex = 32
        Me.m_factsVersionVTreeviewbox.UseThemeBackColor = False
        Me.m_factsVersionVTreeviewbox.UseThemeDropDownArrowColor = True
        Me.m_factsVersionVTreeviewbox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_versionsTreeviewBox
        '
        Me.m_versionsTreeviewBox.BackColor = System.Drawing.Color.White
        Me.m_versionsTreeviewBox.BorderColor = System.Drawing.Color.Black
        Me.m_versionsTreeviewBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.m_versionsTreeviewBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.m_versionsTreeviewBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.m_versionsTreeviewBox.Location = New System.Drawing.Point(227, 39)
        Me.m_versionsTreeviewBox.Name = "m_versionsTreeviewBox"
        Me.m_versionsTreeviewBox.Size = New System.Drawing.Size(356, 23)
        Me.m_versionsTreeviewBox.TabIndex = 33
        Me.m_versionsTreeviewBox.Text = "VTreeViewBox1"
        Me.m_versionsTreeviewBox.UseThemeBackColor = False
        Me.m_versionsTreeviewBox.UseThemeDropDownArrowColor = True
        Me.m_versionsTreeviewBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
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
        'Panel1
        '
        Me.Panel1.Location = New System.Drawing.Point(672, 27)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(393, 485)
        Me.Panel1.TabIndex = 2
        '
        'CancelBT
        '
        Me.CancelBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CancelBT.ImageKey = "imageres_89.ico"
        Me.CancelBT.ImageList = Me.ButtonIcons
        Me.CancelBT.Location = New System.Drawing.Point(306, 296)
        Me.CancelBT.Name = "CancelBT"
        Me.CancelBT.Size = New System.Drawing.Size(92, 30)
        Me.CancelBT.TabIndex = 23
        Me.CancelBT.Text = "cancel"
        Me.CancelBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CancelBT.UseVisualStyleBackColor = True
        '
        'CreateVersionBT
        '
        Me.CreateVersionBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CreateVersionBT.ImageKey = "1420498403_340208.ico"
        Me.CreateVersionBT.ImageList = Me.ButtonIcons
        Me.CreateVersionBT.Location = New System.Drawing.Point(526, 296)
        Me.CreateVersionBT.Name = "CreateVersionBT"
        Me.CreateVersionBT.Size = New System.Drawing.Size(92, 30)
        Me.CreateVersionBT.TabIndex = 22
        Me.CreateVersionBT.Text = "create"
        Me.CreateVersionBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CreateVersionBT.UseVisualStyleBackColor = True
        '
        'm_copyCheckBox
        '
        Me.m_copyCheckBox.BackColor = System.Drawing.Color.Transparent
        Me.m_copyCheckBox.Location = New System.Drawing.Point(3, 39)
        Me.m_copyCheckBox.Name = "m_copyCheckBox"
        Me.m_copyCheckBox.Size = New System.Drawing.Size(175, 24)
        Me.m_copyCheckBox.TabIndex = 34
        Me.m_copyCheckBox.Text = "VCheckBox1"
        Me.m_copyCheckBox.UseVisualStyleBackColor = False
        Me.m_copyCheckBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'NewDataVersionUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(641, 352)
        Me.Controls.Add(Me.CancelBT)
        Me.Controls.Add(Me.CreateVersionBT)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "NewDataVersionUI"
        Me.Text = "version_new"
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.NbPeriodsNUD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StartingPeriodNUD, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents m_startingYearLabel As System.Windows.Forms.Label
    Friend WithEvents m_periodConfigLabel As System.Windows.Forms.Label
    Friend WithEvents m_nameLabel As System.Windows.Forms.Label
    Friend WithEvents NameTB As System.Windows.Forms.TextBox
    Friend WithEvents TimeConfigCB As System.Windows.Forms.ComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents CancelBT As System.Windows.Forms.Button
    Friend WithEvents CreateVersionBT As System.Windows.Forms.Button
    Friend WithEvents ButtonIcons As System.Windows.Forms.ImageList
    Friend WithEvents BigIcons As System.Windows.Forms.ImageList
    Friend WithEvents m_numberOfYearsLabel As System.Windows.Forms.Label
    Friend WithEvents NbPeriodsNUD As System.Windows.Forms.NumericUpDown
    Friend WithEvents StartingPeriodNUD As System.Windows.Forms.NumericUpDown
    Friend WithEvents m_ratesVersionLabel As System.Windows.Forms.Label
    Friend WithEvents m_factsVersionLabel As System.Windows.Forms.Label
    Friend WithEvents m_exchangeRatesVersionVTreeviewbox As VIBlend.WinForms.Controls.vTreeViewBox
    Friend WithEvents m_factsVersionVTreeviewbox As VIBlend.WinForms.Controls.vTreeViewBox
    Friend WithEvents m_versionsTreeviewBox As VIBlend.WinForms.Controls.vTreeViewBox
    Friend WithEvents m_copyCheckBox As VIBlend.WinForms.Controls.vCheckBox
End Class
