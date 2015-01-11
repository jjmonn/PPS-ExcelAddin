<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MultipleSheetsAcquisition
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MultipleSheetsAcquisition))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RB_Structure = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.upload_cmd = New System.Windows.Forms.Button()
        Me.SelectNone_cmd = New System.Windows.Forms.Button()
        Me.selectAll_cmd = New System.Windows.Forms.Button()
        Me.CLB_worksheets = New System.Windows.Forms.CheckedListBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(19, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(491, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "This interface allows you to import automatically multiple worksheets to the data" + _
    " base"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RB_Structure)
        Me.GroupBox1.Location = New System.Drawing.Point(32, 53)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(607, 88)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Worksheets structure"
        '
        'RB_Structure
        '
        Me.RB_Structure.AutoSize = True
        Me.RB_Structure.Location = New System.Drawing.Point(22, 29)
        Me.RB_Structure.Name = "RB_Structure"
        Me.RB_Structure.Size = New System.Drawing.Size(228, 17)
        Me.RB_Structure.TabIndex = 2
        Me.RB_Structure.TabStop = True
        Me.RB_Structure.Text = "All the worksheets have the same structure"
        Me.RB_Structure.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.upload_cmd)
        Me.GroupBox2.Controls.Add(Me.SelectNone_cmd)
        Me.GroupBox2.Controls.Add(Me.selectAll_cmd)
        Me.GroupBox2.Controls.Add(Me.CLB_worksheets)
        Me.GroupBox2.Location = New System.Drawing.Point(34, 159)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(605, 392)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Worksheets selection"
        '
        'upload_cmd
        '
        Me.upload_cmd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.upload_cmd.FlatAppearance.BorderSize = 0
        Me.upload_cmd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.upload_cmd.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.upload_cmd.Location = New System.Drawing.Point(486, 30)
        Me.upload_cmd.Margin = New System.Windows.Forms.Padding(2)
        Me.upload_cmd.Name = "upload_cmd"
        Me.upload_cmd.Size = New System.Drawing.Size(98, 21)
        Me.upload_cmd.TabIndex = 4
        Me.upload_cmd.Text = "Upload"
        Me.upload_cmd.UseVisualStyleBackColor = True
        '
        'SelectNone_cmd
        '
        Me.SelectNone_cmd.Location = New System.Drawing.Point(488, 355)
        Me.SelectNone_cmd.Name = "SelectNone_cmd"
        Me.SelectNone_cmd.Size = New System.Drawing.Size(96, 24)
        Me.SelectNone_cmd.TabIndex = 2
        Me.SelectNone_cmd.Text = "Select none"
        Me.SelectNone_cmd.UseVisualStyleBackColor = True
        '
        'selectAll_cmd
        '
        Me.selectAll_cmd.Location = New System.Drawing.Point(488, 316)
        Me.selectAll_cmd.Name = "selectAll_cmd"
        Me.selectAll_cmd.Size = New System.Drawing.Size(96, 24)
        Me.selectAll_cmd.TabIndex = 1
        Me.selectAll_cmd.Text = "Select all"
        Me.selectAll_cmd.UseVisualStyleBackColor = True
        '
        'CLB_worksheets
        '
        Me.CLB_worksheets.FormattingEnabled = True
        Me.CLB_worksheets.Location = New System.Drawing.Point(20, 30)
        Me.CLB_worksheets.Name = "CLB_worksheets"
        Me.CLB_worksheets.Size = New System.Drawing.Size(444, 349)
        Me.CLB_worksheets.TabIndex = 0
        '
        'MultipleSheetsAcquisition
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(676, 607)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "MultipleSheetsAcquisition"
        Me.Text = "Multiple Worksheets Data Acquisition"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RB_Structure As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents SelectNone_cmd As System.Windows.Forms.Button
    Friend WithEvents selectAll_cmd As System.Windows.Forms.Button
    Friend WithEvents CLB_worksheets As System.Windows.Forms.CheckedListBox
    Friend WithEvents upload_cmd As System.Windows.Forms.Button
End Class
