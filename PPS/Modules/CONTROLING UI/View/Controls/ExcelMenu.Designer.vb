<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExcelMenu
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.ConsoExportBT = New System.Windows.Forms.Button()
        Me.DrillDownExportBT = New System.Windows.Forms.Button()
        Me.DrillDownExportLabel = New System.Windows.Forms.Label()
        Me.ConsoExportLabel = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.8!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.2!))
        Me.TableLayoutPanel1.Controls.Add(Me.ConsoExportBT, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DrillDownExportBT, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DrillDownExportLabel, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.ConsoExportLabel, 0, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(250, 52)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'ConsoExportBT
        '
        Me.ConsoExportBT.BackgroundImage = Global.PPS.My.Resources.Resources.excel_2
        Me.ConsoExportBT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ConsoExportBT.FlatAppearance.BorderSize = 0
        Me.ConsoExportBT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ConsoExportBT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray
        Me.ConsoExportBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ConsoExportBT.Location = New System.Drawing.Point(55, 2)
        Me.ConsoExportBT.Margin = New System.Windows.Forms.Padding(55, 2, 0, 2)
        Me.ConsoExportBT.Name = "ConsoExportBT"
        Me.ConsoExportBT.Size = New System.Drawing.Size(32, 30)
        Me.ConsoExportBT.TabIndex = 8
        Me.ConsoExportBT.UseVisualStyleBackColor = True
        '
        'DrillDownExportBT
        '
        Me.DrillDownExportBT.BackgroundImage = Global.PPS.My.Resources.Resources.favicon_17_1
        Me.DrillDownExportBT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.DrillDownExportBT.FlatAppearance.BorderSize = 0
        Me.DrillDownExportBT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DrillDownExportBT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray
        Me.DrillDownExportBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DrillDownExportBT.Location = New System.Drawing.Point(176, 2)
        Me.DrillDownExportBT.Margin = New System.Windows.Forms.Padding(35, 2, 0, 2)
        Me.DrillDownExportBT.Name = "DrillDownExportBT"
        Me.DrillDownExportBT.Size = New System.Drawing.Size(32, 30)
        Me.DrillDownExportBT.TabIndex = 7
        Me.DrillDownExportBT.UseVisualStyleBackColor = True
        '
        'DrillDownExportLabel
        '
        Me.DrillDownExportLabel.AutoSize = True
        Me.DrillDownExportLabel.Location = New System.Drawing.Point(156, 34)
        Me.DrillDownExportLabel.Margin = New System.Windows.Forms.Padding(15, 0, 0, 0)
        Me.DrillDownExportLabel.Name = "DrillDownExportLabel"
        Me.DrillDownExportLabel.Size = New System.Drawing.Size(83, 13)
        Me.DrillDownExportLabel.TabIndex = 10
        Me.DrillDownExportLabel.Text = "Send Drill Down"
        '
        'ConsoExportLabel
        '
        Me.ConsoExportLabel.AutoSize = True
        Me.ConsoExportLabel.Location = New System.Drawing.Point(10, 34)
        Me.ConsoExportLabel.Margin = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.ConsoExportLabel.Name = "ConsoExportLabel"
        Me.ConsoExportLabel.Size = New System.Drawing.Size(122, 13)
        Me.ConsoExportLabel.TabIndex = 9
        Me.ConsoExportLabel.Text = "Send Consolidated View"
        '
        'ExcelMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "ExcelMenu"
        Me.Size = New System.Drawing.Size(250, 52)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents DrillDownExportBT As System.Windows.Forms.Button
    Friend WithEvents ConsoExportBT As System.Windows.Forms.Button
    Friend WithEvents ConsoExportLabel As System.Windows.Forms.Label
    Friend WithEvents DrillDownExportLabel As System.Windows.Forms.Label

End Class
