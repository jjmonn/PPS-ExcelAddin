<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BusinessControlMenu
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
        Me.VersionsComparisonBT = New System.Windows.Forms.Button()
        Me.DeleteComparisonBT = New System.Windows.Forms.Button()
        Me.DeleteComparisonLabel = New System.Windows.Forms.Label()
        Me.SwitchVersionsLabel = New System.Windows.Forms.Label()
        Me.SwitchVersionsBT = New System.Windows.Forms.Button()
        Me.VersionsComparisonLabel = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57.71144!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.28856!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.VersionsComparisonLabel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.SwitchVersionsLabel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.VersionsComparisonBT, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DeleteComparisonLabel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.DeleteComparisonBT, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.SwitchVersionsBT, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(320, 52)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'VersionsComparisonBT
        '
        Me.VersionsComparisonBT.BackgroundImage = Global.PPS.My.Resources.Resources.favicon_239_
        Me.VersionsComparisonBT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.VersionsComparisonBT.FlatAppearance.BorderSize = 0
        Me.VersionsComparisonBT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.VersionsComparisonBT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray
        Me.VersionsComparisonBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.VersionsComparisonBT.Location = New System.Drawing.Point(45, 2)
        Me.VersionsComparisonBT.Margin = New System.Windows.Forms.Padding(45, 2, 0, 2)
        Me.VersionsComparisonBT.Name = "VersionsComparisonBT"
        Me.VersionsComparisonBT.Size = New System.Drawing.Size(32, 30)
        Me.VersionsComparisonBT.TabIndex = 8
        Me.VersionsComparisonBT.UseVisualStyleBackColor = True
        '
        'DeleteComparisonBT
        '
        Me.DeleteComparisonBT.BackgroundImage = Global.PPS.My.Resources.Resources.imageres_89
        Me.DeleteComparisonBT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.DeleteComparisonBT.FlatAppearance.BorderSize = 0
        Me.DeleteComparisonBT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DeleteComparisonBT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray
        Me.DeleteComparisonBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DeleteComparisonBT.Location = New System.Drawing.Point(248, 2)
        Me.DeleteComparisonBT.Margin = New System.Windows.Forms.Padding(35, 2, 0, 2)
        Me.DeleteComparisonBT.Name = "DeleteComparisonBT"
        Me.DeleteComparisonBT.Size = New System.Drawing.Size(32, 30)
        Me.DeleteComparisonBT.TabIndex = 7
        Me.DeleteComparisonBT.UseVisualStyleBackColor = True
        '
        'DeleteComparisonLabel
        '
        Me.DeleteComparisonLabel.AutoSize = True
        Me.DeleteComparisonLabel.Location = New System.Drawing.Point(216, 34)
        Me.DeleteComparisonLabel.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.DeleteComparisonLabel.Name = "DeleteComparisonLabel"
        Me.DeleteComparisonLabel.Size = New System.Drawing.Size(96, 13)
        Me.DeleteComparisonLabel.TabIndex = 9
        Me.DeleteComparisonLabel.Text = "Delete Comparison"
        '
        'SwitchVersionsLabel
        '
        Me.SwitchVersionsLabel.AutoSize = True
        Me.SwitchVersionsLabel.Location = New System.Drawing.Point(126, 34)
        Me.SwitchVersionsLabel.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.SwitchVersionsLabel.Name = "SwitchVersionsLabel"
        Me.SwitchVersionsLabel.Size = New System.Drawing.Size(82, 13)
        Me.SwitchVersionsLabel.TabIndex = 11
        Me.SwitchVersionsLabel.Text = "Switch Versions"
        '
        'SwitchVersionsBT
        '
        Me.SwitchVersionsBT.BackgroundImage = Global.PPS.My.Resources.Resources.favicon_226_
        Me.SwitchVersionsBT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SwitchVersionsBT.FlatAppearance.BorderSize = 0
        Me.SwitchVersionsBT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.SwitchVersionsBT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray
        Me.SwitchVersionsBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SwitchVersionsBT.Location = New System.Drawing.Point(155, 2)
        Me.SwitchVersionsBT.Margin = New System.Windows.Forms.Padding(32, 2, 0, 2)
        Me.SwitchVersionsBT.Name = "SwitchVersionsBT"
        Me.SwitchVersionsBT.Size = New System.Drawing.Size(32, 30)
        Me.SwitchVersionsBT.TabIndex = 12
        Me.SwitchVersionsBT.UseVisualStyleBackColor = True
        '
        'VersionsComparisonLabel
        '
        Me.VersionsComparisonLabel.AutoSize = True
        Me.VersionsComparisonLabel.Location = New System.Drawing.Point(3, 34)
        Me.VersionsComparisonLabel.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.VersionsComparisonLabel.Name = "VersionsComparisonLabel"
        Me.VersionsComparisonLabel.Size = New System.Drawing.Size(110, 13)
        Me.VersionsComparisonLabel.TabIndex = 13
        Me.VersionsComparisonLabel.Text = "Versions Comparisons"
        '
        'BusinessControlMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "BusinessControlMenu"
        Me.Size = New System.Drawing.Size(360, 52)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents DeleteComparisonBT As System.Windows.Forms.Button
    Friend WithEvents VersionsComparisonBT As System.Windows.Forms.Button
    Friend WithEvents DeleteComparisonLabel As System.Windows.Forms.Label
    Friend WithEvents VersionsComparisonLabel As System.Windows.Forms.Label
    Friend WithEvents SwitchVersionsLabel As System.Windows.Forms.Label
    Friend WithEvents SwitchVersionsBT As System.Windows.Forms.Button

End Class
