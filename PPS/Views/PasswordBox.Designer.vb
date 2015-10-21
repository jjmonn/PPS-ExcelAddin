<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PasswordBox
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
        Me.inputBox = New VIBlend.WinForms.Controls.vTextBox()
        Me.DescTB = New VIBlend.WinForms.Controls.vLabel()
        Me.AcceptBT = New VIBlend.WinForms.Controls.vButton()
        Me.CancelBT = New VIBlend.WinForms.Controls.vButton()
        Me.SuspendLayout()
        '
        'inputBox
        '
        Me.inputBox.BackColor = System.Drawing.Color.White
        Me.inputBox.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.inputBox.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.inputBox.DefaultText = ""
        Me.inputBox.Location = New System.Drawing.Point(12, 92)
        Me.inputBox.MaxLength = 32767
        Me.inputBox.Name = "inputBox"
        Me.inputBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.inputBox.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.inputBox.SelectionLength = 0
        Me.inputBox.SelectionStart = 0
        Me.inputBox.ShowDefaultText = False
        Me.inputBox.Size = New System.Drawing.Size(314, 23)
        Me.inputBox.TabIndex = 0
        Me.inputBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.inputBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        '
        'DescTB
        '
        Me.DescTB.BackColor = System.Drawing.Color.Transparent
        Me.DescTB.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.DescTB.Ellipsis = False
        Me.DescTB.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.DescTB.Location = New System.Drawing.Point(12, 13)
        Me.DescTB.Multiline = True
        Me.DescTB.Name = "DescTB"
        Me.DescTB.Size = New System.Drawing.Size(220, 63)
        Me.DescTB.TabIndex = 1
        Me.DescTB.Text = "Label"
        Me.DescTB.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.DescTB.UseMnemonics = True
        Me.DescTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        '
        'AcceptBT
        '
        Me.AcceptBT.AllowAnimations = True
        Me.AcceptBT.BackColor = System.Drawing.Color.Transparent
        Me.AcceptBT.Location = New System.Drawing.Point(252, 13)
        Me.AcceptBT.Name = "AcceptBT"
        Me.AcceptBT.RoundedCornersMask = CType(15, Byte)
        Me.AcceptBT.Size = New System.Drawing.Size(74, 22)
        Me.AcceptBT.TabIndex = 2
        Me.AcceptBT.Text = "OK"
        Me.AcceptBT.UseVisualStyleBackColor = False
        Me.AcceptBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        '
        'CancelBT
        '
        Me.CancelBT.AllowAnimations = True
        Me.CancelBT.BackColor = System.Drawing.Color.Transparent
        Me.CancelBT.Location = New System.Drawing.Point(252, 41)
        Me.CancelBT.Name = "CancelBT"
        Me.CancelBT.RoundedCornersMask = CType(15, Byte)
        Me.CancelBT.Size = New System.Drawing.Size(74, 22)
        Me.CancelBT.TabIndex = 3
        Me.CancelBT.Text = "Cancel"
        Me.CancelBT.UseVisualStyleBackColor = False
        Me.CancelBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        '
        'PasswordBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(338, 127)
        Me.Controls.Add(Me.CancelBT)
        Me.Controls.Add(Me.AcceptBT)
        Me.Controls.Add(Me.DescTB)
        Me.Controls.Add(Me.inputBox)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PasswordBox"
        Me.ShowIcon = False
        Me.Text = "PasswordBox"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents inputBox As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents DescTB As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents AcceptBT As VIBlend.WinForms.Controls.vButton
    Friend WithEvents CancelBT As VIBlend.WinForms.Controls.vButton
End Class
