<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SnapShotUI
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
        Me.DataSet1 = New System.Data.DataSet()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Flag1_TB = New System.Windows.Forms.TextBox()
        Me.Upload_BT = New System.Windows.Forms.Button()
        Me.BT_ChangeSelection = New System.Windows.Forms.Button()
        Me.BT_Edit = New System.Windows.Forms.Button()
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
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.Control
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(2, 51)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.Size = New System.Drawing.Size(958, 570)
        Me.DataGridView1.TabIndex = 0
        Me.DataGridView1.Tag = "XC"
        '
        'Flag1_TB
        '
        Me.Flag1_TB.Location = New System.Drawing.Point(12, 9)
        Me.Flag1_TB.Name = "Flag1_TB"
        Me.Flag1_TB.Size = New System.Drawing.Size(431, 22)
        Me.Flag1_TB.TabIndex = 1
        '
        'Upload_BT
        '
        Me.Upload_BT.BackColor = System.Drawing.Color.PowderBlue
        Me.Upload_BT.FlatAppearance.BorderColor = System.Drawing.Color.LightBlue
        Me.Upload_BT.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Upload_BT.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Upload_BT.Location = New System.Drawing.Point(835, 5)
        Me.Upload_BT.Name = "Upload_BT"
        Me.Upload_BT.Size = New System.Drawing.Size(113, 26)
        Me.Upload_BT.TabIndex = 3
        Me.Upload_BT.Text = "Upload Data"
        Me.Upload_BT.UseVisualStyleBackColor = False
        '
        'BT_ChangeSelection
        '
        Me.BT_ChangeSelection.Location = New System.Drawing.Point(716, 5)
        Me.BT_ChangeSelection.Name = "BT_ChangeSelection"
        Me.BT_ChangeSelection.Size = New System.Drawing.Size(113, 26)
        Me.BT_ChangeSelection.TabIndex = 5
        Me.BT_ChangeSelection.Text = "Select Columns"
        Me.BT_ChangeSelection.UseVisualStyleBackColor = True
        '
        'BT_Edit
        '
        Me.BT_Edit.Location = New System.Drawing.Point(465, 6)
        Me.BT_Edit.Name = "BT_Edit"
        Me.BT_Edit.Size = New System.Drawing.Size(115, 25)
        Me.BT_Edit.TabIndex = 6
        Me.BT_Edit.Text = "Edit Template"
        Me.BT_Edit.UseVisualStyleBackColor = True
        '
        'SnapShotUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(960, 622)
        Me.Controls.Add(Me.BT_Edit)
        Me.Controls.Add(Me.BT_ChangeSelection)
        Me.Controls.Add(Me.Upload_BT)
        Me.Controls.Add(Me.Flag1_TB)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "SnapShotUI"
        Me.Tag = "XC"
        Me.Text = "SnapShotUI"
        CType(Me.DataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataSet1 As System.Data.DataSet
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Flag1_TB As System.Windows.Forms.TextBox
    Friend WithEvents Upload_BT As System.Windows.Forms.Button
    Friend WithEvents BT_ChangeSelection As System.Windows.Forms.Button
    Friend WithEvents BT_Edit As System.Windows.Forms.Button
End Class
