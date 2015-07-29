<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DisplayControl
    Inherits System.Windows.Forms.UserControl

    'UserControl1 overrides dispose to clean up the component list.
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.rows_display_tv = New System.Windows.Forms.TreeView()
        Me.columns_display_tv = New System.Windows.Forms.TreeView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.columns_display_tv, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.rows_display_tv, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 1, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.89869!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.55066!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.55066!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(214, 530)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'rows_display_tv
        '
        Me.rows_display_tv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rows_display_tv.Location = New System.Drawing.Point(3, 397)
        Me.rows_display_tv.Name = "rows_display_tv"
        Me.rows_display_tv.Size = New System.Drawing.Size(101, 130)
        Me.rows_display_tv.TabIndex = 1
        '
        'columns_display_tv
        '
        Me.columns_display_tv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.columns_display_tv.Location = New System.Drawing.Point(110, 263)
        Me.columns_display_tv.Name = "columns_display_tv"
        Me.columns_display_tv.Size = New System.Drawing.Size(101, 128)
        Me.columns_display_tv.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(110, 245)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 15)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Columns"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(3, 379)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 15)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Rows"
        '
        'DisplayControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "DisplayControl"
        Me.Size = New System.Drawing.Size(214, 530)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Public WithEvents rows_display_tv As System.Windows.Forms.TreeView
    Public WithEvents columns_display_tv As System.Windows.Forms.TreeView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label

End Class
