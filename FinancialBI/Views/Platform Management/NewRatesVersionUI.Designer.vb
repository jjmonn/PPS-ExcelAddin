<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewRatesVersionUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NewRatesVersionUI))
        Me.m_nameLabel = New System.Windows.Forms.Label()
        Me.m_startingYearLabel = New System.Windows.Forms.Label()
        Me.m_numberOfYearsLabel = New System.Windows.Forms.Label()
        Me.NameTB = New System.Windows.Forms.TextBox()
        Me.StartPeriodNUD = New System.Windows.Forms.NumericUpDown()
        Me.NBPeriodsNUD = New System.Windows.Forms.NumericUpDown()
        Me.CancelBT = New System.Windows.Forms.Button()
        Me.ButtonIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.ValidateBT = New System.Windows.Forms.Button()
        Me.m_versionsTreeviewImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.m_creationBackgroundWorker = New System.ComponentModel.BackgroundWorker()
        Me.m_circularProgress = New VIBlend.WinForms.Controls.vCircularProgressBar()
        CType(Me.StartPeriodNUD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NBPeriodsNUD, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'm_nameLabel
        '
        Me.m_nameLabel.AutoSize = True
        Me.m_nameLabel.Location = New System.Drawing.Point(46, 49)
        Me.m_nameLabel.Name = "m_nameLabel"
        Me.m_nameLabel.Size = New System.Drawing.Size(35, 13)
        Me.m_nameLabel.TabIndex = 0
        Me.m_nameLabel.Text = "Name"
        '
        'm_startingYearLabel
        '
        Me.m_startingYearLabel.AutoSize = True
        Me.m_startingYearLabel.Location = New System.Drawing.Point(46, 97)
        Me.m_startingYearLabel.Name = "m_startingYearLabel"
        Me.m_startingYearLabel.Size = New System.Drawing.Size(68, 13)
        Me.m_startingYearLabel.TabIndex = 1
        Me.m_startingYearLabel.Text = "Starting Year"
        '
        'm_numberOfYearsLabel
        '
        Me.m_numberOfYearsLabel.AutoSize = True
        Me.m_numberOfYearsLabel.Location = New System.Drawing.Point(46, 139)
        Me.m_numberOfYearsLabel.Name = "m_numberOfYearsLabel"
        Me.m_numberOfYearsLabel.Size = New System.Drawing.Size(86, 13)
        Me.m_numberOfYearsLabel.TabIndex = 2
        Me.m_numberOfYearsLabel.Text = "Number of Years"
        '
        'NameTB
        '
        Me.NameTB.Location = New System.Drawing.Point(171, 49)
        Me.NameTB.Name = "NameTB"
        Me.NameTB.Size = New System.Drawing.Size(160, 20)
        Me.NameTB.TabIndex = 3
        '
        'StartPeriodNUD
        '
        Me.StartPeriodNUD.Location = New System.Drawing.Point(171, 95)
        Me.StartPeriodNUD.Maximum = New Decimal(New Integer() {3000, 0, 0, 0})
        Me.StartPeriodNUD.Name = "StartPeriodNUD"
        Me.StartPeriodNUD.Size = New System.Drawing.Size(100, 20)
        Me.StartPeriodNUD.TabIndex = 4
        '
        'NBPeriodsNUD
        '
        Me.NBPeriodsNUD.Location = New System.Drawing.Point(171, 137)
        Me.NBPeriodsNUD.Maximum = New Decimal(New Integer() {70, 0, 0, 0})
        Me.NBPeriodsNUD.Name = "NBPeriodsNUD"
        Me.NBPeriodsNUD.Size = New System.Drawing.Size(100, 20)
        Me.NBPeriodsNUD.TabIndex = 5
        '
        'CancelBT
        '
        Me.CancelBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CancelBT.ImageKey = "imageres_89.ico"
        Me.CancelBT.ImageList = Me.ButtonIcons
        Me.CancelBT.Location = New System.Drawing.Point(171, 192)
        Me.CancelBT.Name = "CancelBT"
        Me.CancelBT.Size = New System.Drawing.Size(86, 26)
        Me.CancelBT.TabIndex = 25
        Me.CancelBT.Text = "Cancel"
        Me.CancelBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CancelBT.UseVisualStyleBackColor = True
        '
        'ButtonIcons
        '
        Me.ButtonIcons.ImageStream = CType(resources.GetObject("ButtonIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonIcons.Images.SetKeyName(0, "favicon(97).ico")
        Me.ButtonIcons.Images.SetKeyName(1, "imageres_99.ico")
        Me.ButtonIcons.Images.SetKeyName(2, "folder 1.ico")
        Me.ButtonIcons.Images.SetKeyName(3, "imageres_89.ico")
        Me.ButtonIcons.Images.SetKeyName(4, "1420498403_340208.ico")
        '
        'ValidateBT
        '
        Me.ValidateBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ValidateBT.ImageKey = "1420498403_340208.ico"
        Me.ValidateBT.ImageList = Me.ButtonIcons
        Me.ValidateBT.Location = New System.Drawing.Point(291, 192)
        Me.ValidateBT.Name = "ValidateBT"
        Me.ValidateBT.Size = New System.Drawing.Size(86, 26)
        Me.ValidateBT.TabIndex = 24
        Me.ValidateBT.Text = "Create"
        Me.ValidateBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ValidateBT.UseVisualStyleBackColor = True
        '
        'm_versionsTreeviewImageList
        '
        Me.m_versionsTreeviewImageList.ImageStream = CType(resources.GetObject("m_versionsTreeviewImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.m_versionsTreeviewImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.m_versionsTreeviewImageList.Images.SetKeyName(0, "cloud_dark.ico")
        Me.m_versionsTreeviewImageList.Images.SetKeyName(1, "favicon(81).ico")
        '
        'm_circularProgress
        '
        Me.m_circularProgress.AllowAnimations = True
        Me.m_circularProgress.BackColor = System.Drawing.Color.Transparent
        Me.m_circularProgress.IndicatorsCount = 8
        Me.m_circularProgress.Location = New System.Drawing.Point(171, 82)
        Me.m_circularProgress.Maximum = 100
        Me.m_circularProgress.Minimum = 0
        Me.m_circularProgress.Name = "m_circularProgress"
        Me.m_circularProgress.Size = New System.Drawing.Size(75, 75)
        Me.m_circularProgress.TabIndex = 26
        Me.m_circularProgress.Text = "VCircularProgressBar1"
        Me.m_circularProgress.UseThemeBackground = False
        Me.m_circularProgress.Value = 0
        Me.m_circularProgress.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICEBLUE
        '
        'NewRatesVersionUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(402, 248)
        Me.Controls.Add(Me.m_circularProgress)
        Me.Controls.Add(Me.CancelBT)
        Me.Controls.Add(Me.ValidateBT)
        Me.Controls.Add(Me.NBPeriodsNUD)
        Me.Controls.Add(Me.StartPeriodNUD)
        Me.Controls.Add(Me.NameTB)
        Me.Controls.Add(Me.m_numberOfYearsLabel)
        Me.Controls.Add(Me.m_startingYearLabel)
        Me.Controls.Add(Me.m_nameLabel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "NewRatesVersionUI"
        Me.Text = "New Exchange Rates Version"
        CType(Me.StartPeriodNUD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NBPeriodsNUD, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents m_nameLabel As System.Windows.Forms.Label
    Friend WithEvents m_startingYearLabel As System.Windows.Forms.Label
    Friend WithEvents m_numberOfYearsLabel As System.Windows.Forms.Label
    Friend WithEvents NameTB As System.Windows.Forms.TextBox
    Friend WithEvents StartPeriodNUD As System.Windows.Forms.NumericUpDown
    Friend WithEvents NBPeriodsNUD As System.Windows.Forms.NumericUpDown
    Friend WithEvents CancelBT As System.Windows.Forms.Button
    Friend WithEvents ValidateBT As System.Windows.Forms.Button
    Friend WithEvents ButtonIcons As System.Windows.Forms.ImageList
    Friend WithEvents m_versionsTreeviewImageList As System.Windows.Forms.ImageList
    Friend WithEvents m_creationBackgroundWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents m_circularProgress As VIBlend.WinForms.Controls.vCircularProgressBar
End Class
