<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SimpleDisplaySnapshotUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SimpleDisplaySnapshotUI))
        Me.DataSet1 = New System.Data.DataSet()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Flag1_TB = New System.Windows.Forms.TextBox()
        Me.BT_ChangeSelection = New System.Windows.Forms.Button()
        Me.BT_Edit = New System.Windows.Forms.Button()
        Me.validate_cmd = New System.Windows.Forms.Button()
        CType(Me.DataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataSet1
        '
        Me.DataSet1.DataSetName = "NewDataSet"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(2, 41)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(2)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.Size = New System.Drawing.Size(1073, 594)
        Me.DataGridView1.TabIndex = 0
        Me.DataGridView1.Tag = "XC"
        '
        'Flag1_TB
        '
        Me.Flag1_TB.Location = New System.Drawing.Point(9, 7)
        Me.Flag1_TB.Margin = New System.Windows.Forms.Padding(2)
        Me.Flag1_TB.Name = "Flag1_TB"
        Me.Flag1_TB.Size = New System.Drawing.Size(324, 20)
        Me.Flag1_TB.TabIndex = 1
        '
        'BT_ChangeSelection
        '
        Me.BT_ChangeSelection.Location = New System.Drawing.Point(457, 5)
        Me.BT_ChangeSelection.Margin = New System.Windows.Forms.Padding(2)
        Me.BT_ChangeSelection.Name = "BT_ChangeSelection"
        Me.BT_ChangeSelection.Size = New System.Drawing.Size(108, 20)
        Me.BT_ChangeSelection.TabIndex = 5
        Me.BT_ChangeSelection.Text = "Select Columns"
        Me.BT_ChangeSelection.UseVisualStyleBackColor = True
        '
        'BT_Edit
        '
        Me.BT_Edit.Location = New System.Drawing.Point(349, 5)
        Me.BT_Edit.Margin = New System.Windows.Forms.Padding(2)
        Me.BT_Edit.Name = "BT_Edit"
        Me.BT_Edit.Size = New System.Drawing.Size(86, 20)
        Me.BT_Edit.TabIndex = 6
        Me.BT_Edit.Text = "Edit Template"
        Me.BT_Edit.UseVisualStyleBackColor = True
        '
        'validate_cmd
        '
        Me.validate_cmd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.validate_cmd.FlatAppearance.BorderSize = 0
        Me.validate_cmd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.validate_cmd.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.validate_cmd.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.validate_cmd.Location = New System.Drawing.Point(945, 7)
        Me.validate_cmd.Margin = New System.Windows.Forms.Padding(2)
        Me.validate_cmd.Name = "validate_cmd"
        Me.validate_cmd.Size = New System.Drawing.Size(107, 30)
        Me.validate_cmd.TabIndex = 7
        Me.validate_cmd.Text = "Submit"
        Me.validate_cmd.UseVisualStyleBackColor = True
        '
        'SimpleDisplaySnapshotUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(1077, 635)
        Me.Controls.Add(Me.validate_cmd)
        Me.Controls.Add(Me.BT_Edit)
        Me.Controls.Add(Me.BT_ChangeSelection)
        Me.Controls.Add(Me.Flag1_TB)
        Me.Controls.Add(Me.DataGridView1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "SimpleDisplaySnapshotUI"
        Me.Tag = "XC"
        Me.Text = "Worksheet upload structure set up"
        CType(Me.DataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataSet1 As System.Data.DataSet
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Flag1_TB As System.Windows.Forms.TextBox
    Friend WithEvents BT_ChangeSelection As System.Windows.Forms.Button
    Friend WithEvents BT_Edit As System.Windows.Forms.Button
    Friend WithEvents validate_cmd As System.Windows.Forms.Button
End Class
