<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CircularProgressUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CircularProgressUI))
        Me.CP = New ProgressControls.ProgressIndicator()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'CP
        '
        Me.CP.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CP.CircleColor = System.Drawing.Color.Purple
        Me.CP.CircleSize = 0.7!
        Me.CP.Location = New System.Drawing.Point(56, 28)
        Me.CP.Name = "CP"
        Me.CP.NumberOfCircles = 12
        Me.CP.Percentage = 0.0!
        Me.CP.Size = New System.Drawing.Size(79, 79)
        Me.CP.TabIndex = 0
        Me.CP.Text = "ProgressIndicator1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(67, 128)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Initializing"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CircularProgressUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(190, 164)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CP)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "CircularProgressUI"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CP As ProgressControls.ProgressIndicator
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
