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
        Me.CreateCopyBT = New System.Windows.Forms.CheckBox()
        Me.BigIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ReferenceTB = New System.Windows.Forms.TextBox()
        Me.NameTB = New System.Windows.Forms.TextBox()
        Me.TimeConfigCB = New System.Windows.Forms.ComboBox()
        Me.RefPeriodCB = New System.Windows.Forms.ComboBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CancelBT = New System.Windows.Forms.Button()
        Me.ButtonIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.CreateEntityBT = New System.Windows.Forms.Button()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.35376!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 61.64624!))
        Me.TableLayoutPanel2.Controls.Add(Me.CreateCopyBT, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Label11, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.Label10, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Label4, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.ReferenceTB, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.NameTB, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TimeConfigCB, 1, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.RefPeriodCB, 1, 3)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(32, 27)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 4
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(586, 170)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'CreateCopyBT
        '
        Me.CreateCopyBT.Appearance = System.Windows.Forms.Appearance.Button
        Me.CreateCopyBT.AutoSize = True
        Me.CreateCopyBT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CreateCopyBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CreateCopyBT.ImageKey = "favicon(230).ico"
        Me.CreateCopyBT.ImageList = Me.BigIcons
        Me.CreateCopyBT.Location = New System.Drawing.Point(3, 45)
        Me.CreateCopyBT.Name = "CreateCopyBT"
        Me.CreateCopyBT.Size = New System.Drawing.Size(218, 36)
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
        Me.Label11.Location = New System.Drawing.Point(3, 133)
        Me.Label11.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(205, 13)
        Me.Label11.TabIndex = 17
        Me.Label11.Text = "Refrence year (monthly time configuration)"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(3, 91)
        Me.Label10.Margin = New System.Windows.Forms.Padding(3, 7, 3, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(106, 13)
        Me.Label10.TabIndex = 15
        Me.Label10.Text = "Periods configuration"
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
        Me.ReferenceTB.Location = New System.Drawing.Point(227, 47)
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
        Me.TimeConfigCB.Location = New System.Drawing.Point(227, 87)
        Me.TimeConfigCB.Name = "TimeConfigCB"
        Me.TimeConfigCB.Size = New System.Drawing.Size(143, 21)
        Me.TimeConfigCB.TabIndex = 16
        '
        'RefPeriodCB
        '
        Me.RefPeriodCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.RefPeriodCB.FormattingEnabled = True
        Me.RefPeriodCB.Location = New System.Drawing.Point(227, 129)
        Me.RefPeriodCB.Name = "RefPeriodCB"
        Me.RefPeriodCB.Size = New System.Drawing.Size(143, 21)
        Me.RefPeriodCB.TabIndex = 18
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
        Me.CancelBT.Location = New System.Drawing.Point(306, 250)
        Me.CancelBT.Name = "CancelBT"
        Me.CancelBT.Size = New System.Drawing.Size(92, 30)
        Me.CancelBT.TabIndex = 23
        Me.CancelBT.Text = "Cancel"
        Me.CancelBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CancelBT.UseVisualStyleBackColor = True
        '
        'ButtonIcons
        '
        Me.ButtonIcons.ImageStream = CType(resources.GetObject("ButtonIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonIcons.Images.SetKeyName(0, "imageres_89.ico")
        Me.ButtonIcons.Images.SetKeyName(1, "favicon(95).ico")
        Me.ButtonIcons.Images.SetKeyName(2, "submit 1 ok.ico")
        Me.ButtonIcons.Images.SetKeyName(3, "favicon(97).ico")
        Me.ButtonIcons.Images.SetKeyName(4, "imageres_99.ico")
        Me.ButtonIcons.Images.SetKeyName(5, "folder 1.ico")
        '
        'CreateEntityBT
        '
        Me.CreateEntityBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CreateEntityBT.ImageKey = "submit 1 ok.ico"
        Me.CreateEntityBT.ImageList = Me.ButtonIcons
        Me.CreateEntityBT.Location = New System.Drawing.Point(526, 250)
        Me.CreateEntityBT.Name = "CreateEntityBT"
        Me.CreateEntityBT.Size = New System.Drawing.Size(92, 30)
        Me.CreateEntityBT.TabIndex = 22
        Me.CreateEntityBT.Text = "Create"
        Me.CreateEntityBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CreateEntityBT.UseVisualStyleBackColor = True
        '
        'NewDataVersionUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(641, 322)
        Me.Controls.Add(Me.CancelBT)
        Me.Controls.Add(Me.CreateEntityBT)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "NewDataVersionUI"
        Me.Text = "NewDataVersionUI"
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ReferenceTB As System.Windows.Forms.TextBox
    Friend WithEvents NameTB As System.Windows.Forms.TextBox
    Friend WithEvents TimeConfigCB As System.Windows.Forms.ComboBox
    Friend WithEvents RefPeriodCB As System.Windows.Forms.ComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents CancelBT As System.Windows.Forms.Button
    Friend WithEvents CreateEntityBT As System.Windows.Forms.Button
    Friend WithEvents ButtonIcons As System.Windows.Forms.ImageList
    Friend WithEvents BigIcons As System.Windows.Forms.ImageList
    Friend WithEvents CreateCopyBT As System.Windows.Forms.CheckBox
End Class
