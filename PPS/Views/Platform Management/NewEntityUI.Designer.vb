<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewEntityUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NewEntityUI))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.VLabel3 = New VIBlend.WinForms.Controls.vLabel()
        Me.NameTextBox = New VIBlend.WinForms.Controls.vTextBox()
        Me.VLabel1 = New VIBlend.WinForms.Controls.vLabel()
        Me.ParentEntityTreeViewBox = New VIBlend.WinForms.Controls.vTreeViewBox()
        Me.ButtonIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.CancelBT = New System.Windows.Forms.Button()
        Me.ButtonsIL = New System.Windows.Forms.ImageList(Me.components)
        Me.CreateEntityBT = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.51376!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.48624!))
        Me.TableLayoutPanel1.Controls.Add(Me.VLabel3, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.NameTextBox, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.VLabel1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ParentEntityTreeViewBox, 1, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(22, 25)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(482, 156)
        Me.TableLayoutPanel1.TabIndex = 18
        '
        'VLabel3
        '
        Me.VLabel3.BackColor = System.Drawing.Color.Transparent
        Me.VLabel3.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel3.Ellipsis = False
        Me.VLabel3.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel3.Location = New System.Drawing.Point(3, 33)
        Me.VLabel3.Multiline = True
        Me.VLabel3.Name = "VLabel3"
        Me.VLabel3.Size = New System.Drawing.Size(169, 24)
        Me.VLabel3.TabIndex = 5
        Me.VLabel3.Text = "[entities_edition.parent_entity]"
        Me.VLabel3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.VLabel3.UseMnemonics = True
        Me.VLabel3.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'NameTextBox
        '
        Me.NameTextBox.BackColor = System.Drawing.Color.White
        Me.NameTextBox.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.NameTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.NameTextBox.DefaultText = "Empty..."
        Me.NameTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NameTextBox.Location = New System.Drawing.Point(178, 3)
        Me.NameTextBox.MaxLength = 32767
        Me.NameTextBox.Name = "NameTextBox"
        Me.NameTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.NameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.NameTextBox.SelectionLength = 0
        Me.NameTextBox.SelectionStart = 0
        Me.NameTextBox.Size = New System.Drawing.Size(301, 24)
        Me.NameTextBox.TabIndex = 0
        Me.NameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.NameTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VLabel1
        '
        Me.VLabel1.BackColor = System.Drawing.Color.Transparent
        Me.VLabel1.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VLabel1.Ellipsis = False
        Me.VLabel1.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel1.Location = New System.Drawing.Point(3, 3)
        Me.VLabel1.Multiline = True
        Me.VLabel1.Name = "VLabel1"
        Me.VLabel1.Size = New System.Drawing.Size(169, 24)
        Me.VLabel1.TabIndex = 1
        Me.VLabel1.Text = "[general.name]"
        Me.VLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.VLabel1.UseMnemonics = True
        Me.VLabel1.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'ParentEntityTreeViewBox
        '
        Me.ParentEntityTreeViewBox.BackColor = System.Drawing.Color.White
        Me.ParentEntityTreeViewBox.BorderColor = System.Drawing.Color.Black
        Me.ParentEntityTreeViewBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.ParentEntityTreeViewBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.ParentEntityTreeViewBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.ParentEntityTreeViewBox.Location = New System.Drawing.Point(178, 33)
        Me.ParentEntityTreeViewBox.Name = "ParentEntityTreeViewBox"
        Me.ParentEntityTreeViewBox.Size = New System.Drawing.Size(301, 23)
        Me.ParentEntityTreeViewBox.TabIndex = 4
        Me.ParentEntityTreeViewBox.Text = "[entities_edition.parent_entity_selection]"
        Me.ParentEntityTreeViewBox.UseThemeBackColor = False
        Me.ParentEntityTreeViewBox.UseThemeDropDownArrowColor = True
        Me.ParentEntityTreeViewBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
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
        '
        'CancelBT
        '
        Me.CancelBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CancelBT.ImageKey = "delete-2-xxl.png"
        Me.CancelBT.ImageList = Me.ButtonsIL
        Me.CancelBT.Location = New System.Drawing.Point(314, 187)
        Me.CancelBT.Name = "CancelBT"
        Me.CancelBT.Size = New System.Drawing.Size(92, 30)
        Me.CancelBT.TabIndex = 21
        Me.CancelBT.Text = "[general.cancel]"
        Me.CancelBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CancelBT.UseVisualStyleBackColor = True
        '
        'ButtonsIL
        '
        Me.ButtonsIL.ImageStream = CType(resources.GetObject("ButtonsIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonsIL.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonsIL.Images.SetKeyName(0, "submit 1 ok.ico")
        Me.ButtonsIL.Images.SetKeyName(1, "delete-2-xxl.png")
        Me.ButtonsIL.Images.SetKeyName(2, "favicon(97).ico")
        '
        'CreateEntityBT
        '
        Me.CreateEntityBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CreateEntityBT.ImageKey = "submit 1 ok.ico"
        Me.CreateEntityBT.ImageList = Me.ButtonsIL
        Me.CreateEntityBT.Location = New System.Drawing.Point(412, 187)
        Me.CreateEntityBT.Name = "CreateEntityBT"
        Me.CreateEntityBT.Size = New System.Drawing.Size(92, 30)
        Me.CreateEntityBT.TabIndex = 20
        Me.CreateEntityBT.Text = "[general.create]"
        Me.CreateEntityBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CreateEntityBT.UseVisualStyleBackColor = True
        '
        'NewEntityUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(523, 237)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.CancelBT)
        Me.Controls.Add(Me.CreateEntityBT)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "NewEntityUI"
        Me.Text = "[entities_edition.new_entity]"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents CancelBT As System.Windows.Forms.Button
    Friend WithEvents CreateEntityBT As System.Windows.Forms.Button
    Friend WithEvents ButtonsIL As System.Windows.Forms.ImageList
    Friend WithEvents ButtonIcons As System.Windows.Forms.ImageList
    Friend WithEvents NameTextBox As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents VLabel1 As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents VLabel3 As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents ParentEntityTreeViewBox As VIBlend.WinForms.Controls.vTreeViewBox
End Class
