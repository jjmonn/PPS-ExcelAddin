<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VersionSelectionUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VersionSelectionUI))
        Me.VersioningTVIL = New System.Windows.Forms.ImageList(Me.components)
        Me.ImageList2 = New System.Windows.Forms.ImageList(Me.components)
        Me.ValidateButton = New System.Windows.Forms.Button()
        Me.VersionsTreeComboBox = New VIBlend.WinForms.Controls.vTreeViewBox()
        Me.SuspendLayout()
        '
        'VersioningTVIL
        '
        Me.VersioningTVIL.ImageStream = CType(resources.GetObject("VersioningTVIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.VersioningTVIL.TransparentColor = System.Drawing.Color.Transparent
        Me.VersioningTVIL.Images.SetKeyName(0, "icons-blue.png")
        Me.VersioningTVIL.Images.SetKeyName(1, "DB Grey.png")
        '
        'ImageList2
        '
        Me.ImageList2.ImageStream = CType(resources.GetObject("ImageList2.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList2.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList2.Images.SetKeyName(0, "favicon(16).ico")
        Me.ImageList2.Images.SetKeyName(1, "favicon(25).ico")
        Me.ImageList2.Images.SetKeyName(2, "favicon(28).ico")
        Me.ImageList2.Images.SetKeyName(3, "favicon(76).ico")
        '
        'ValidateButton
        '
        Me.ValidateButton.FlatAppearance.BorderSize = 0
        Me.ValidateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ValidateButton.ImageKey = "favicon(76).ico"
        Me.ValidateButton.ImageList = Me.ImageList2
        Me.ValidateButton.Location = New System.Drawing.Point(239, 72)
        Me.ValidateButton.Name = "ValidateButton"
        Me.ValidateButton.Size = New System.Drawing.Size(91, 30)
        Me.ValidateButton.TabIndex = 17
        Me.ValidateButton.Text = "Validate"
        Me.ValidateButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ValidateButton.UseVisualStyleBackColor = True
        '
        'VersionsTreeComboBox
        '
        Me.VersionsTreeComboBox.BackColor = System.Drawing.Color.White
        Me.VersionsTreeComboBox.BorderColor = System.Drawing.Color.Black
        Me.VersionsTreeComboBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.VersionsTreeComboBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.VersionsTreeComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.VersionsTreeComboBox.Location = New System.Drawing.Point(21, 24)
        Me.VersionsTreeComboBox.Name = "VersionsTreeComboBox"
        Me.VersionsTreeComboBox.Size = New System.Drawing.Size(307, 25)
        Me.VersionsTreeComboBox.TabIndex = 18
        Me.VersionsTreeComboBox.Text = "Select version"
        Me.VersionsTreeComboBox.UseThemeBackColor = False
        Me.VersionsTreeComboBox.UseThemeDropDownArrowColor = True
        Me.VersionsTreeComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VersionSelectionUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(344, 156)
        Me.Controls.Add(Me.VersionsTreeComboBox)
        Me.Controls.Add(Me.ValidateButton)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "VersionSelectionUI"
        Me.Text = "Select a version"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents VersioningTVIL As System.Windows.Forms.ImageList
    Friend WithEvents ValidateButton As System.Windows.Forms.Button
    Friend WithEvents ImageList2 As System.Windows.Forms.ImageList
    Friend WithEvents VersionsTreeComboBox As VIBlend.WinForms.Controls.vTreeViewBox
End Class
