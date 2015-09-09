<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewFilterUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NewFilterUI))
        Me.ParentFilterTreeBox = New VIBlend.WinForms.Controls.vTreeViewBox()
        Me.VLabel1 = New VIBlend.WinForms.Controls.vLabel()
        Me.VLabel2 = New VIBlend.WinForms.Controls.vLabel()
        Me.NameTextBox = New VIBlend.WinForms.Controls.vTextBox()
        Me.CreateButton = New System.Windows.Forms.Button()
        Me.ButtonsIL = New System.Windows.Forms.ImageList(Me.components)
        Me.CancelButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ParentFilterTreeBox
        '
        Me.ParentFilterTreeBox.BackColor = System.Drawing.Color.White
        Me.ParentFilterTreeBox.BorderColor = System.Drawing.Color.Black
        Me.ParentFilterTreeBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.ParentFilterTreeBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.ParentFilterTreeBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.ParentFilterTreeBox.Location = New System.Drawing.Point(109, 61)
        Me.ParentFilterTreeBox.Name = "ParentFilterTreeBox"
        Me.ParentFilterTreeBox.Size = New System.Drawing.Size(246, 26)
        Me.ParentFilterTreeBox.TabIndex = 15
        Me.ParentFilterTreeBox.Text = " "
        Me.ParentFilterTreeBox.UseThemeBackColor = False
        Me.ParentFilterTreeBox.UseThemeDropDownArrowColor = True
        Me.ParentFilterTreeBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VLabel1
        '
        Me.VLabel1.BackColor = System.Drawing.Color.Transparent
        Me.VLabel1.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel1.Ellipsis = False
        Me.VLabel1.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel1.Location = New System.Drawing.Point(23, 65)
        Me.VLabel1.Multiline = True
        Me.VLabel1.Name = "VLabel1"
        Me.VLabel1.Size = New System.Drawing.Size(80, 18)
        Me.VLabel1.TabIndex = 16
        Me.VLabel1.Text = "Parent Filter"
        Me.VLabel1.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel1.UseMnemonics = True
        Me.VLabel1.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VLabel2
        '
        Me.VLabel2.BackColor = System.Drawing.Color.Transparent
        Me.VLabel2.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel2.Ellipsis = False
        Me.VLabel2.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel2.Location = New System.Drawing.Point(23, 29)
        Me.VLabel2.Multiline = True
        Me.VLabel2.Name = "VLabel2"
        Me.VLabel2.Size = New System.Drawing.Size(80, 18)
        Me.VLabel2.TabIndex = 17
        Me.VLabel2.Text = "Filter's Name"
        Me.VLabel2.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel2.UseMnemonics = True
        Me.VLabel2.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'NameTextBox
        '
        Me.NameTextBox.BackColor = System.Drawing.Color.White
        Me.NameTextBox.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.NameTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.NameTextBox.DefaultText = "Empty..."
        Me.NameTextBox.Location = New System.Drawing.Point(109, 25)
        Me.NameTextBox.MaxLength = 32767
        Me.NameTextBox.Name = "NameTextBox"
        Me.NameTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.NameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.NameTextBox.SelectionLength = 0
        Me.NameTextBox.SelectionStart = 0
        Me.NameTextBox.Size = New System.Drawing.Size(246, 23)
        Me.NameTextBox.TabIndex = 18
        Me.NameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.NameTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'CreateButton
        '
        Me.CreateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CreateButton.ImageKey = "favicon(76).ico"
        Me.CreateButton.ImageList = Me.ButtonsIL
        Me.CreateButton.Location = New System.Drawing.Point(263, 120)
        Me.CreateButton.Name = "CreateButton"
        Me.CreateButton.Size = New System.Drawing.Size(92, 25)
        Me.CreateButton.TabIndex = 21
        Me.CreateButton.Text = "Create"
        Me.CreateButton.UseVisualStyleBackColor = True
        '
        'ButtonsIL
        '
        Me.ButtonsIL.ImageStream = CType(resources.GetObject("ButtonsIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonsIL.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonsIL.Images.SetKeyName(0, "delete-2-xxl.png")
        Me.ButtonsIL.Images.SetKeyName(1, "favicon(76).ico")
        '
        'CancelButton
        '
        Me.CancelButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CancelButton.ImageKey = "delete-2-xxl.png"
        Me.CancelButton.ImageList = Me.ButtonsIL
        Me.CancelButton.Location = New System.Drawing.Point(145, 120)
        Me.CancelButton.Name = "CancelButton"
        Me.CancelButton.Size = New System.Drawing.Size(92, 25)
        Me.CancelButton.TabIndex = 22
        Me.CancelButton.Text = "Cancel"
        Me.CancelButton.UseVisualStyleBackColor = True
        '
        'NewFilter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(380, 168)
        Me.Controls.Add(Me.CancelButton)
        Me.Controls.Add(Me.CreateButton)
        Me.Controls.Add(Me.NameTextBox)
        Me.Controls.Add(Me.VLabel2)
        Me.Controls.Add(Me.VLabel1)
        Me.Controls.Add(Me.ParentFilterTreeBox)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "NewFilter"
        Me.Text = "Filter Creation"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ParentFilterTreeBox As VIBlend.WinForms.Controls.vTreeViewBox
    Friend WithEvents VLabel1 As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents VLabel2 As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents NameTextBox As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents CreateButton As System.Windows.Forms.Button
    Friend WithEvents ButtonsIL As System.Windows.Forms.ImageList
    Friend WithEvents CancelButton As System.Windows.Forms.Button
End Class
