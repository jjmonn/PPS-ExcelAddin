<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewControlUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NewControlUI))
        Me.Item1CB = New System.Windows.Forms.ComboBox()
        Me.OperatorCB = New System.Windows.Forms.ComboBox()
        Me.Item2CB = New System.Windows.Forms.ComboBox()
        Me.PeriodOptionCB = New System.Windows.Forms.ComboBox()
        Me.NameTB = New System.Windows.Forms.TextBox()
        Me.ValidateBT = New System.Windows.Forms.Button()
        Me.ButtonsIL = New System.Windows.Forms.ImageList(Me.components)
        Me.CancelBT = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Item1CB
        '
        Me.Item1CB.FormattingEnabled = True
        Me.Item1CB.Location = New System.Drawing.Point(29, 109)
        Me.Item1CB.Name = "Item1CB"
        Me.Item1CB.Size = New System.Drawing.Size(241, 21)
        Me.Item1CB.TabIndex = 2
        '
        'OperatorCB
        '
        Me.OperatorCB.FormattingEnabled = True
        Me.OperatorCB.Location = New System.Drawing.Point(290, 109)
        Me.OperatorCB.Name = "OperatorCB"
        Me.OperatorCB.Size = New System.Drawing.Size(80, 21)
        Me.OperatorCB.TabIndex = 3
        '
        'Item2CB
        '
        Me.Item2CB.FormattingEnabled = True
        Me.Item2CB.Location = New System.Drawing.Point(388, 109)
        Me.Item2CB.Name = "Item2CB"
        Me.Item2CB.Size = New System.Drawing.Size(241, 21)
        Me.Item2CB.TabIndex = 4
        '
        'PeriodOptionCB
        '
        Me.PeriodOptionCB.FormattingEnabled = True
        Me.PeriodOptionCB.Location = New System.Drawing.Point(657, 109)
        Me.PeriodOptionCB.Name = "PeriodOptionCB"
        Me.PeriodOptionCB.Size = New System.Drawing.Size(164, 21)
        Me.PeriodOptionCB.TabIndex = 5
        '
        'NameTB
        '
        Me.NameTB.Location = New System.Drawing.Point(29, 48)
        Me.NameTB.Name = "NameTB"
        Me.NameTB.Size = New System.Drawing.Size(238, 20)
        Me.NameTB.TabIndex = 1
        '
        'ValidateBT
        '
        Me.ValidateBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ValidateBT.ImageKey = "submit 1 ok.ico"
        Me.ValidateBT.ImageList = Me.ButtonsIL
        Me.ValidateBT.Location = New System.Drawing.Point(735, 153)
        Me.ValidateBT.Name = "ValidateBT"
        Me.ValidateBT.Size = New System.Drawing.Size(86, 25)
        Me.ValidateBT.TabIndex = 6
        Me.ValidateBT.Text = "Validate"
        Me.ValidateBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ValidateBT.UseVisualStyleBackColor = True
        '
        'ButtonsIL
        '
        Me.ButtonsIL.ImageStream = CType(resources.GetObject("ButtonsIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonsIL.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonsIL.Images.SetKeyName(0, "submit 1 ok.ico")
        Me.ButtonsIL.Images.SetKeyName(1, "delete-2-xxl.png")
        Me.ButtonsIL.Images.SetKeyName(2, "favicon(97).ico")
        '
        'CancelBT
        '
        Me.CancelBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CancelBT.ImageKey = "delete-2-xxl.png"
        Me.CancelBT.ImageList = Me.ButtonsIL
        Me.CancelBT.Location = New System.Drawing.Point(622, 153)
        Me.CancelBT.Name = "CancelBT"
        Me.CancelBT.Size = New System.Drawing.Size(86, 25)
        Me.CancelBT.TabIndex = 7
        Me.CancelBT.Text = "Cancel"
        Me.CancelBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CancelBT.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(33, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Control Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(33, 91)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Item 1"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(392, 91)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Item 2"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(661, 91)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(38, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Option"
        '
        'NewControlUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(847, 201)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CancelBT)
        Me.Controls.Add(Me.ValidateBT)
        Me.Controls.Add(Me.NameTB)
        Me.Controls.Add(Me.PeriodOptionCB)
        Me.Controls.Add(Me.Item2CB)
        Me.Controls.Add(Me.OperatorCB)
        Me.Controls.Add(Me.Item1CB)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "NewControlUI"
        Me.Text = "New Submission Control"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Item1CB As System.Windows.Forms.ComboBox
    Friend WithEvents OperatorCB As System.Windows.Forms.ComboBox
    Friend WithEvents Item2CB As System.Windows.Forms.ComboBox
    Friend WithEvents PeriodOptionCB As System.Windows.Forms.ComboBox
    Friend WithEvents NameTB As System.Windows.Forms.TextBox
    Friend WithEvents ValidateBT As System.Windows.Forms.Button
    Friend WithEvents CancelBT As System.Windows.Forms.Button
    Friend WithEvents ButtonsIL As System.Windows.Forms.ImageList
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
