<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewGlobalFactVersionUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NewGlobalFactVersionUI))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.NameTB = New System.Windows.Forms.TextBox()
        Me.StartPeriodNUD = New System.Windows.Forms.NumericUpDown()
        Me.m_nb_years = New System.Windows.Forms.NumericUpDown()
        Me.CancelBT = New System.Windows.Forms.Button()
        Me.ButtonIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.ValidateBT = New System.Windows.Forms.Button()
        CType(Me.StartPeriodNUD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.m_nb_years, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(28, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = Local.GetValue("general.name")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(28, 79)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(153, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = Local.GetValue("facts_versions.starting_period")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(28, 121)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(127, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = Local.GetValue("facts_versions.nb_years")
        '
        'NameTB
        '
        Me.NameTB.Location = New System.Drawing.Point(153, 31)
        Me.NameTB.Name = "NameTB"
        Me.NameTB.Size = New System.Drawing.Size(160, 20)
        Me.NameTB.TabIndex = 3
        '
        'StartPeriodNUD
        '
        Me.StartPeriodNUD.Location = New System.Drawing.Point(153, 77)
        Me.StartPeriodNUD.Maximum = New Decimal(New Integer() {3000, 0, 0, 0})
        Me.StartPeriodNUD.Name = "StartPeriodNUD"
        Me.StartPeriodNUD.Size = New System.Drawing.Size(100, 20)
        Me.StartPeriodNUD.TabIndex = 4
        '
        'm_nb_years
        '
        Me.m_nb_years.Location = New System.Drawing.Point(153, 119)
        Me.m_nb_years.Maximum = New Decimal(New Integer() {70, 0, 0, 0})
        Me.m_nb_years.Name = "m_nb_years"
        Me.m_nb_years.Size = New System.Drawing.Size(100, 20)
        Me.m_nb_years.TabIndex = 5
        '
        'CancelBT
        '
        Me.CancelBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CancelBT.ImageKey = "imageres_89.ico"
        Me.CancelBT.ImageList = Me.ButtonIcons
        Me.CancelBT.Location = New System.Drawing.Point(153, 168)
        Me.CancelBT.Name = "CancelBT"
        Me.CancelBT.Size = New System.Drawing.Size(86, 26)
        Me.CancelBT.TabIndex = 25
        Me.CancelBT.Text = Local.GetValue("general.cancel")
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
        Me.ValidateBT.Location = New System.Drawing.Point(273, 168)
        Me.ValidateBT.Name = "ValidateBT"
        Me.ValidateBT.Size = New System.Drawing.Size(86, 26)
        Me.ValidateBT.TabIndex = 24
        Me.ValidateBT.Text = Local.GetValue("general.create")
        Me.ValidateBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ValidateBT.UseVisualStyleBackColor = True
        '
        'NewGlobalFactVersionUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(392, 211)
        Me.Controls.Add(Me.CancelBT)
        Me.Controls.Add(Me.ValidateBT)
        Me.Controls.Add(Me.m_nb_years)
        Me.Controls.Add(Me.StartPeriodNUD)
        Me.Controls.Add(Me.NameTB)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "NewGlobalFactVersionUI"
        Me.Text = Local.GetValue("global_facts.new_version")
        CType(Me.StartPeriodNUD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.m_nb_years, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents NameTB As System.Windows.Forms.TextBox
    Friend WithEvents StartPeriodNUD As System.Windows.Forms.NumericUpDown
    Friend WithEvents m_nb_years As System.Windows.Forms.NumericUpDown
    Friend WithEvents CancelBT As System.Windows.Forms.Button
    Friend WithEvents ValidateBT As System.Windows.Forms.Button
    Friend WithEvents ButtonIcons As System.Windows.Forms.ImageList
End Class
