<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewAxisUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NewAxisUI))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.m_parentEntityLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.NameTextBox = New VIBlend.WinForms.Controls.vTextBox()
        Me.m_nameLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_parentAxisElemTreeviewBox = New VIBlend.WinForms.Controls.vTreeViewBox()
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
        Me.TableLayoutPanel1.Controls.Add(Me.m_parentEntityLabel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.NameTextBox, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.m_nameLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.m_parentAxisElemTreeviewBox, 1, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(22, 25)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(482, 156)
        Me.TableLayoutPanel1.TabIndex = 18
        '
        'm_parentEntityLabel
        '
        Me.m_parentEntityLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_parentEntityLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_parentEntityLabel.Ellipsis = False
        Me.m_parentEntityLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_parentEntityLabel.Location = New System.Drawing.Point(3, 33)
        Me.m_parentEntityLabel.Multiline = True
        Me.m_parentEntityLabel.Name = "m_parentEntityLabel"
        Me.m_parentEntityLabel.Size = New System.Drawing.Size(169, 24)
        Me.m_parentEntityLabel.TabIndex = 5
        Me.m_parentEntityLabel.Text = "parent_entity"
        Me.m_parentEntityLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.m_parentEntityLabel.UseMnemonics = True
        Me.m_parentEntityLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
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
        'm_nameLabel
        '
        Me.m_nameLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_nameLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_nameLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_nameLabel.Ellipsis = False
        Me.m_nameLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_nameLabel.Location = New System.Drawing.Point(3, 3)
        Me.m_nameLabel.Multiline = True
        Me.m_nameLabel.Name = "m_nameLabel"
        Me.m_nameLabel.Size = New System.Drawing.Size(169, 24)
        Me.m_nameLabel.TabIndex = 1
        Me.m_nameLabel.Text = "Name"
        Me.m_nameLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.m_nameLabel.UseMnemonics = True
        Me.m_nameLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_parentEntitiesTreeviewBox
        '
        Me.m_parentAxisElemTreeviewBox.BackColor = System.Drawing.Color.White
        Me.m_parentAxisElemTreeviewBox.BorderColor = System.Drawing.Color.Black
        Me.m_parentAxisElemTreeviewBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.m_parentAxisElemTreeviewBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.m_parentAxisElemTreeviewBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.m_parentAxisElemTreeviewBox.Location = New System.Drawing.Point(178, 33)
        Me.m_parentAxisElemTreeviewBox.Name = "m_parentEntitiesTreeviewBox"
        Me.m_parentAxisElemTreeviewBox.Size = New System.Drawing.Size(301, 23)
        Me.m_parentAxisElemTreeviewBox.TabIndex = 4
        Me.m_parentAxisElemTreeviewBox.Text = "parent_entity_selection"
        Me.m_parentAxisElemTreeviewBox.UseThemeBackColor = False
        Me.m_parentAxisElemTreeviewBox.UseThemeDropDownArrowColor = True
        Me.m_parentAxisElemTreeviewBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
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
        Me.CancelBT.Text = "Cancel"
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
        Me.CreateEntityBT.Text = "Create"
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
        Me.Text = "New_entity"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents CancelBT As System.Windows.Forms.Button
    Friend WithEvents CreateEntityBT As System.Windows.Forms.Button
    Friend WithEvents ButtonsIL As System.Windows.Forms.ImageList
    Friend WithEvents ButtonIcons As System.Windows.Forms.ImageList
    Friend WithEvents NameTextBox As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents m_nameLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_parentEntityLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_parentAxisElemTreeviewBox As VIBlend.WinForms.Controls.vTreeViewBox
End Class
