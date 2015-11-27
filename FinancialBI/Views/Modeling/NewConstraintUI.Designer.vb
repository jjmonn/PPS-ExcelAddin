<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewConstraintUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NewConstraintUI))
        Me.ConstraintsComboBox = New System.Windows.Forms.ComboBox()
        Me.ValidateButton = New System.Windows.Forms.Button()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.DefaultValueTB = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'ConstraintsComboBox
        '
        Me.ConstraintsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ConstraintsComboBox.FormattingEnabled = True
        Me.ConstraintsComboBox.Location = New System.Drawing.Point(39, 43)
        Me.ConstraintsComboBox.Name = "ConstraintsComboBox"
        Me.ConstraintsComboBox.Size = New System.Drawing.Size(230, 21)
        Me.ConstraintsComboBox.TabIndex = 0
        '
        'ValidateButton
        '
        Me.ValidateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ValidateButton.ImageKey = "1420498403_340208.ico"
        Me.ValidateButton.ImageList = Me.ImageList1
        Me.ValidateButton.Location = New System.Drawing.Point(189, 128)
        Me.ValidateButton.Name = "ValidateButton"
        Me.ValidateButton.Size = New System.Drawing.Size(80, 26)
        Me.ValidateButton.TabIndex = 3
        Me.ValidateButton.Text = "Validate"
        Me.ValidateButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ValidateButton.UseVisualStyleBackColor = True
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "1420498403_340208.ico")
        '
        'DefaultValueTB
        '
        Me.DefaultValueTB.Location = New System.Drawing.Point(112, 88)
        Me.DefaultValueTB.Name = "DefaultValueTB"
        Me.DefaultValueTB.Size = New System.Drawing.Size(156, 20)
        Me.DefaultValueTB.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(36, 90)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Default value"
        '
        'NewConstraintUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(304, 178)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DefaultValueTB)
        Me.Controls.Add(Me.ValidateButton)
        Me.Controls.Add(Me.ConstraintsComboBox)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "NewConstraintUI"
        Me.Text = "New Constraint"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ConstraintsComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents ValidateButton As System.Windows.Forms.Button
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents DefaultValueTB As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
