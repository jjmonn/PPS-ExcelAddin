<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Snapshot_SelectionUI
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
        Me.Cmd_Close = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Cmd_SelectAll = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Cmd_Close
        '
        Me.Cmd_Close.Location = New System.Drawing.Point(514, 380)
        Me.Cmd_Close.Name = "Cmd_Close"
        Me.Cmd_Close.Size = New System.Drawing.Size(78, 27)
        Me.Cmd_Close.TabIndex = 1
        Me.Cmd_Close.Text = "Close"
        Me.Cmd_Close.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Panel1.Location = New System.Drawing.Point(2, 10)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(575, 310)
        Me.Panel1.TabIndex = 2
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 54)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(580, 320)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(460, 18)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Select the columns that you wish to upload in the Data Base"
        '
        'Cmd_SelectAll
        '
        Me.Cmd_SelectAll.Location = New System.Drawing.Point(427, 380)
        Me.Cmd_SelectAll.Name = "Cmd_SelectAll"
        Me.Cmd_SelectAll.Size = New System.Drawing.Size(81, 27)
        Me.Cmd_SelectAll.TabIndex = 3
        Me.Cmd_SelectAll.Text = "Select all"
        Me.Cmd_SelectAll.UseVisualStyleBackColor = True
        '
        'Snapshot_SelectionUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(604, 419)
        Me.Controls.Add(Me.Cmd_SelectAll)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Cmd_Close)
        Me.Location = New System.Drawing.Point(200, 50)
        Me.Name = "Snapshot_SelectionUI"
        Me.Text = "Snapshot_SelectionUI"
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Cmd_Close As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Cmd_SelectAll As System.Windows.Forms.Button
End Class
