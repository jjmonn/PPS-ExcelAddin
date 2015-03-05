<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DisplayMenu
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
        Me.EntitiesBT = New System.Windows.Forms.Button()
        Me.CategoriesBT = New System.Windows.Forms.Button()
        Me.AdjustmentBT = New System.Windows.Forms.Button()
        Me.CurrenciesBT = New System.Windows.Forms.Button()
        Me.PeriodsBT = New System.Windows.Forms.Button()
        Me.VersionsBT = New System.Windows.Forms.Button()
        Me.VersionsLabel = New System.Windows.Forms.Label()
        Me.PeriodsLabel = New System.Windows.Forms.Label()
        Me.CurrenciesLabel = New System.Windows.Forms.Label()
        Me.AdjustmentLabel = New System.Windows.Forms.Label()
        Me.CategoriesLabel = New System.Windows.Forms.Label()
        Me.EntitiesLabel = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 6
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel1.Controls.Add(Me.EntitiesBT, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.CategoriesBT, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.AdjustmentBT, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.CurrenciesBT, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.PeriodsBT, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.VersionsBT, 5, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.VersionsLabel, 5, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.PeriodsLabel, 4, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.CurrenciesLabel, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.AdjustmentLabel, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.CategoriesLabel, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.EntitiesLabel, 0, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(418, 52)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'EntitiesBT
        '
        Me.EntitiesBT.BackgroundImage = Global.PPS.My.Resources.Resources.config_circle_blue
        Me.EntitiesBT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.EntitiesBT.FlatAppearance.BorderSize = 0
        Me.EntitiesBT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.EntitiesBT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray
        Me.EntitiesBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.EntitiesBT.Location = New System.Drawing.Point(12, 2)
        Me.EntitiesBT.Margin = New System.Windows.Forms.Padding(12, 2, 0, 2)
        Me.EntitiesBT.Name = "EntitiesBT"
        Me.EntitiesBT.Size = New System.Drawing.Size(32, 30)
        Me.EntitiesBT.TabIndex = 8
        Me.EntitiesBT.UseVisualStyleBackColor = True
        '
        'CategoriesBT
        '
        Me.CategoriesBT.BackgroundImage = Global.PPS.My.Resources.Resources.config_circle_green
        Me.CategoriesBT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CategoriesBT.FlatAppearance.BorderSize = 0
        Me.CategoriesBT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.CategoriesBT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray
        Me.CategoriesBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CategoriesBT.Location = New System.Drawing.Point(81, 2)
        Me.CategoriesBT.Margin = New System.Windows.Forms.Padding(12, 2, 0, 2)
        Me.CategoriesBT.Name = "CategoriesBT"
        Me.CategoriesBT.Size = New System.Drawing.Size(32, 30)
        Me.CategoriesBT.TabIndex = 7
        Me.CategoriesBT.UseVisualStyleBackColor = True
        '
        'AdjustmentBT
        '
        Me.AdjustmentBT.BackgroundImage = Global.PPS.My.Resources.Resources.favicon_13_
        Me.AdjustmentBT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.AdjustmentBT.FlatAppearance.BorderSize = 0
        Me.AdjustmentBT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.AdjustmentBT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray
        Me.AdjustmentBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AdjustmentBT.Location = New System.Drawing.Point(150, 2)
        Me.AdjustmentBT.Margin = New System.Windows.Forms.Padding(12, 2, 0, 2)
        Me.AdjustmentBT.Name = "AdjustmentBT"
        Me.AdjustmentBT.Size = New System.Drawing.Size(32, 30)
        Me.AdjustmentBT.TabIndex = 8
        Me.AdjustmentBT.UseVisualStyleBackColor = True
        '
        'CurrenciesBT
        '
        Me.CurrenciesBT.BackgroundImage = Global.PPS.My.Resources.Resources.favicon_249_
        Me.CurrenciesBT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CurrenciesBT.FlatAppearance.BorderSize = 0
        Me.CurrenciesBT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.CurrenciesBT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray
        Me.CurrenciesBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CurrenciesBT.Location = New System.Drawing.Point(219, 2)
        Me.CurrenciesBT.Margin = New System.Windows.Forms.Padding(12, 2, 0, 2)
        Me.CurrenciesBT.Name = "CurrenciesBT"
        Me.CurrenciesBT.Size = New System.Drawing.Size(32, 30)
        Me.CurrenciesBT.TabIndex = 8
        Me.CurrenciesBT.UseVisualStyleBackColor = True
        '
        'PeriodsBT
        '
        Me.PeriodsBT.BackgroundImage = Global.PPS.My.Resources.Resources.purple_table
        Me.PeriodsBT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PeriodsBT.FlatAppearance.BorderSize = 0
        Me.PeriodsBT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.PeriodsBT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray
        Me.PeriodsBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.PeriodsBT.Location = New System.Drawing.Point(288, 2)
        Me.PeriodsBT.Margin = New System.Windows.Forms.Padding(12, 2, 0, 2)
        Me.PeriodsBT.Name = "PeriodsBT"
        Me.PeriodsBT.Size = New System.Drawing.Size(32, 30)
        Me.PeriodsBT.TabIndex = 8
        Me.PeriodsBT.UseVisualStyleBackColor = True
        '
        'VersionsBT
        '
        Me.VersionsBT.BackgroundImage = Global.PPS.My.Resources.Resources.db_Purple_big
        Me.VersionsBT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.VersionsBT.FlatAppearance.BorderSize = 0
        Me.VersionsBT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.VersionsBT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray
        Me.VersionsBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.VersionsBT.Location = New System.Drawing.Point(357, 2)
        Me.VersionsBT.Margin = New System.Windows.Forms.Padding(12, 2, 0, 2)
        Me.VersionsBT.Name = "VersionsBT"
        Me.VersionsBT.Size = New System.Drawing.Size(32, 30)
        Me.VersionsBT.TabIndex = 8
        Me.VersionsBT.UseVisualStyleBackColor = True
        '
        'VersionsLabel
        '
        Me.VersionsLabel.AutoSize = True
        Me.VersionsLabel.Location = New System.Drawing.Point(348, 34)
        Me.VersionsLabel.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.VersionsLabel.Name = "VersionsLabel"
        Me.VersionsLabel.Size = New System.Drawing.Size(47, 13)
        Me.VersionsLabel.TabIndex = 10
        Me.VersionsLabel.Text = "Versions"
        '
        'PeriodsLabel
        '
        Me.PeriodsLabel.AutoSize = True
        Me.PeriodsLabel.Location = New System.Drawing.Point(282, 34)
        Me.PeriodsLabel.Margin = New System.Windows.Forms.Padding(6, 0, 0, 0)
        Me.PeriodsLabel.Name = "PeriodsLabel"
        Me.PeriodsLabel.Size = New System.Drawing.Size(42, 13)
        Me.PeriodsLabel.TabIndex = 10
        Me.PeriodsLabel.Text = "Periods"
        '
        'CurrenciesLabel
        '
        Me.CurrenciesLabel.AutoSize = True
        Me.CurrenciesLabel.Location = New System.Drawing.Point(210, 34)
        Me.CurrenciesLabel.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.CurrenciesLabel.Name = "CurrenciesLabel"
        Me.CurrenciesLabel.Size = New System.Drawing.Size(57, 13)
        Me.CurrenciesLabel.TabIndex = 10
        Me.CurrenciesLabel.Text = "Currencies"
        '
        'AdjustmentLabel
        '
        Me.AdjustmentLabel.AutoSize = True
        Me.AdjustmentLabel.Location = New System.Drawing.Point(140, 34)
        Me.AdjustmentLabel.Margin = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.AdjustmentLabel.Name = "AdjustmentLabel"
        Me.AdjustmentLabel.Size = New System.Drawing.Size(64, 13)
        Me.AdjustmentLabel.TabIndex = 10
        Me.AdjustmentLabel.Text = "Adjustments"
        '
        'CategoriesLabel
        '
        Me.CategoriesLabel.AutoSize = True
        Me.CategoriesLabel.Location = New System.Drawing.Point(72, 34)
        Me.CategoriesLabel.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.CategoriesLabel.Name = "CategoriesLabel"
        Me.CategoriesLabel.Size = New System.Drawing.Size(57, 13)
        Me.CategoriesLabel.TabIndex = 10
        Me.CategoriesLabel.Text = "Categories"
        '
        'EntitiesLabel
        '
        Me.EntitiesLabel.AutoSize = True
        Me.EntitiesLabel.Location = New System.Drawing.Point(10, 34)
        Me.EntitiesLabel.Margin = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.EntitiesLabel.Name = "EntitiesLabel"
        Me.EntitiesLabel.Size = New System.Drawing.Size(41, 13)
        Me.EntitiesLabel.TabIndex = 9
        Me.EntitiesLabel.Text = "Entities"
        '
        'DisplayMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "DisplayMenu"
        Me.Size = New System.Drawing.Size(416, 52)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents CategoriesBT As System.Windows.Forms.Button
    Friend WithEvents EntitiesBT As System.Windows.Forms.Button
    Friend WithEvents VersionsBT As System.Windows.Forms.Button
    Friend WithEvents PeriodsBT As System.Windows.Forms.Button
    Friend WithEvents CurrenciesBT As System.Windows.Forms.Button
    Friend WithEvents AdjustmentBT As System.Windows.Forms.Button
    Friend WithEvents EntitiesLabel As System.Windows.Forms.Label
    Friend WithEvents AdjustmentLabel As System.Windows.Forms.Label
    Friend WithEvents CategoriesLabel As System.Windows.Forms.Label
    Friend WithEvents VersionsLabel As System.Windows.Forms.Label
    Friend WithEvents PeriodsLabel As System.Windows.Forms.Label
    Friend WithEvents CurrenciesLabel As System.Windows.Forms.Label

End Class
