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
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CreateCopyBT = New System.Windows.Forms.CheckBox()
        Me.BigIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ReferenceTB = New System.Windows.Forms.TextBox()
        Me.NameTB = New System.Windows.Forms.TextBox()
        Me.TimeConfigCB = New System.Windows.Forms.ComboBox()
        Me.NbPeriodsNUD = New System.Windows.Forms.NumericUpDown()
        Me.StartingPeriodNUD = New System.Windows.Forms.NumericUpDown()
        Me.RatesVersionCB = New System.Windows.Forms.ComboBox()
        Me.FactVersionCB = New System.Windows.Forms.ComboBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.CancelBT = New System.Windows.Forms.Button()
        Me.CreateVersionBT = New System.Windows.Forms.Button()
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
        Me.TableLayoutPanel2.Controls.Add(Me.Label3, 0, 6)
        Me.TableLayoutPanel2.Controls.Add(Me.Label2, 0, 5)
        Me.TableLayoutPanel2.Controls.Add(Me.Label1, 0, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.CreateCopyBT, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Label11, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.Label10, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Label4, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.ReferenceTB, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.NameTB, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TimeConfigCB, 1, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.NbPeriodsNUD, 1, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.StartingPeriodNUD, 1, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.RatesVersionCB, 1, 5)
        Me.TableLayoutPanel2.Controls.Add(Me.FactVersionCB, 1, 6)
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
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 223)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 13)
        Me.Label3.TabIndex = 30
        Me.Label3.Text = "Fact Version"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 187)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(124, 13)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "Exchange Rates Version"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 151)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "Number of Years"
        '
        'CreateCopyBT
        '
        Me.CreateCopyBT.Appearance = System.Windows.Forms.Appearance.Button
        Me.CreateCopyBT.AutoSize = True
        Me.CreateCopyBT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CreateCopyBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CreateCopyBT.ImageKey = "favicon(230).ico"
        Me.CreateCopyBT.ImageList = Me.BigIcons
        Me.CreateCopyBT.Location = New System.Drawing.Point(3, 39)
        Me.CreateCopyBT.Name = "CreateCopyBT"
        Me.CreateCopyBT.Size = New System.Drawing.Size(218, 30)
        Me.CreateCopyBT.TabIndex = 24
        Me.CreateCopyBT.Text = "Create Copy of"
        Me.CreateCopyBT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CreateCopyBT.UseVisualStyleBackColor = True
        '
        'BigIcons
        '
        Me.BigIcons.ImageStream = CType(resources.GetObject("BigIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.BigIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.BigIcons.Images.SetKeyName(0, "favicon(230).ico")
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(3, 115)
        Me.Label11.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(68, 13)
        Me.Label11.TabIndex = 17
        Me.Label11.Text = "Starting Year"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(3, 79)
        Me.Label10.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(192, 13)
        Me.Label10.TabIndex = 15
        Me.Label10.Text = "Periods configuration (Yearly / Monthly)"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 7)
        Me.Label4.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Version Name"
        '
        'ReferenceTB
        '
        Me.ReferenceTB.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ReferenceTB.Location = New System.Drawing.Point(227, 41)
        Me.ReferenceTB.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.ReferenceTB.MaximumSize = New System.Drawing.Size(400, 4)
        Me.ReferenceTB.MinimumSize = New System.Drawing.Size(280, 20)
        Me.ReferenceTB.Name = "ReferenceTB"
        Me.ReferenceTB.Size = New System.Drawing.Size(356, 20)
        Me.ReferenceTB.TabIndex = 0
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
        'RatesVersionCB
        '
        Me.RatesVersionCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.RatesVersionCB.FormattingEnabled = True
        Me.RatesVersionCB.Location = New System.Drawing.Point(227, 183)
        Me.RatesVersionCB.Name = "RatesVersionCB"
        Me.RatesVersionCB.Size = New System.Drawing.Size(143, 21)
        Me.RatesVersionCB.TabIndex = 29
        '
        'FactVersionCB
        '
        Me.FactVersionCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.FactVersionCB.FormattingEnabled = True
        Me.FactVersionCB.Location = New System.Drawing.Point(227, 219)
        Me.FactVersionCB.Name = "FactVersionCB"
        Me.FactVersionCB.Size = New System.Drawing.Size(143, 21)
        Me.FactVersionCB.TabIndex = 31
        '
        'Panel1
        '
        Me.Panel1.Location = New System.Drawing.Point(672, 27)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(393, 485)
        Me.Panel1.TabIndex = 2
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
        'CancelBT
        '
        Me.CancelBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CancelBT.ImageKey = "imageres_89.ico"
        Me.CancelBT.ImageList = Me.ButtonIcons
        Me.CancelBT.Location = New System.Drawing.Point(306, 296)
        Me.CancelBT.Name = "CancelBT"
        Me.CancelBT.Size = New System.Drawing.Size(92, 30)
        Me.CancelBT.TabIndex = 23
        Me.CancelBT.Text = "Cancel"
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
        Me.CreateVersionBT.Text = "Create"
        Me.CreateVersionBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CreateVersionBT.UseVisualStyleBackColor = True
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
        Me.Text = "New Version"
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.NbPeriodsNUD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StartingPeriodNUD, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ReferenceTB As System.Windows.Forms.TextBox
    Friend WithEvents NameTB As System.Windows.Forms.TextBox
    Friend WithEvents TimeConfigCB As System.Windows.Forms.ComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents CancelBT As System.Windows.Forms.Button
    Friend WithEvents CreateVersionBT As System.Windows.Forms.Button
    Friend WithEvents ButtonIcons As System.Windows.Forms.ImageList
    Friend WithEvents BigIcons As System.Windows.Forms.ImageList
    Friend WithEvents CreateCopyBT As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents NbPeriodsNUD As System.Windows.Forms.NumericUpDown
    Friend WithEvents StartingPeriodNUD As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents RatesVersionCB As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents FactVersionCB As System.Windows.Forms.ComboBox
End Class
