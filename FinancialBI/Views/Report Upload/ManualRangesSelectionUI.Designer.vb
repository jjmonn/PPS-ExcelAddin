<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ManualRangesSelectionUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ManualRangesSelectionUI))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PeriodsEditBT = New System.Windows.Forms.Button()
        Me.ButtonsImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.PeriodsRefEdit = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.AccountsEditBT = New System.Windows.Forms.Button()
        Me.AccountsRefEdit = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.EntitiesEditBT = New System.Windows.Forms.Button()
        Me.EntitiesRefEdit = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Validate_Cmd = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.GrayText
        Me.Label1.Location = New System.Drawing.Point(11, 26)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(453, 15)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Select the ranges corresponding to the Entities, Accounts and Periods"
        '
        'PeriodsEditBT
        '
        Me.PeriodsEditBT.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.PeriodsEditBT.ImageIndex = 0
        Me.PeriodsEditBT.ImageList = Me.ButtonsImageList
        Me.PeriodsEditBT.Location = New System.Drawing.Point(525, 112)
        Me.PeriodsEditBT.Margin = New System.Windows.Forms.Padding(2)
        Me.PeriodsEditBT.Name = "PeriodsEditBT"
        Me.PeriodsEditBT.Size = New System.Drawing.Size(27, 27)
        Me.PeriodsEditBT.TabIndex = 8
        Me.PeriodsEditBT.UseVisualStyleBackColor = True
        '
        'ButtonsImageList
        '
        Me.ButtonsImageList.ImageStream = CType(resources.GetObject("ButtonsImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonsImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonsImageList.Images.SetKeyName(0, "favicon(161).ico")
        Me.ButtonsImageList.Images.SetKeyName(1, "favicon(132).ico")
        Me.ButtonsImageList.Images.SetKeyName(2, "favicon(76).ico")
        '
        'PeriodsRefEdit
        '
        Me.PeriodsRefEdit.Location = New System.Drawing.Point(182, 112)
        Me.PeriodsRefEdit.Margin = New System.Windows.Forms.Padding(2)
        Me.PeriodsRefEdit.Name = "PeriodsRefEdit"
        Me.PeriodsRefEdit.Size = New System.Drawing.Size(261, 20)
        Me.PeriodsRefEdit.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(2, 110)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(116, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Select Period(s) Range"
        '
        'AccountsEditBT
        '
        Me.AccountsEditBT.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.AccountsEditBT.ImageIndex = 0
        Me.AccountsEditBT.ImageList = Me.ButtonsImageList
        Me.AccountsEditBT.Location = New System.Drawing.Point(525, 2)
        Me.AccountsEditBT.Margin = New System.Windows.Forms.Padding(2)
        Me.AccountsEditBT.Name = "AccountsEditBT"
        Me.AccountsEditBT.Size = New System.Drawing.Size(27, 27)
        Me.AccountsEditBT.TabIndex = 6
        Me.AccountsEditBT.UseVisualStyleBackColor = True
        '
        'AccountsRefEdit
        '
        Me.AccountsRefEdit.Location = New System.Drawing.Point(182, 2)
        Me.AccountsRefEdit.Margin = New System.Windows.Forms.Padding(2)
        Me.AccountsRefEdit.Name = "AccountsRefEdit"
        Me.AccountsRefEdit.Size = New System.Drawing.Size(261, 20)
        Me.AccountsRefEdit.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(2, 0)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(126, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Select Account(s) Range"
        '
        'EntitiesEditBT
        '
        Me.EntitiesEditBT.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.EntitiesEditBT.ImageIndex = 0
        Me.EntitiesEditBT.ImageList = Me.ButtonsImageList
        Me.EntitiesEditBT.Location = New System.Drawing.Point(525, 57)
        Me.EntitiesEditBT.Margin = New System.Windows.Forms.Padding(2)
        Me.EntitiesEditBT.Name = "EntitiesEditBT"
        Me.EntitiesEditBT.Size = New System.Drawing.Size(27, 27)
        Me.EntitiesEditBT.TabIndex = 8
        Me.EntitiesEditBT.UseVisualStyleBackColor = True
        '
        'EntitiesRefEdit
        '
        Me.EntitiesRefEdit.Location = New System.Drawing.Point(182, 57)
        Me.EntitiesRefEdit.Margin = New System.Windows.Forms.Padding(2)
        Me.EntitiesRefEdit.Name = "EntitiesRefEdit"
        Me.EntitiesRefEdit.Size = New System.Drawing.Size(261, 20)
        Me.EntitiesRefEdit.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(2, 55)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(114, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Select Entitiy(s) Range"
        '
        'Validate_Cmd
        '
        Me.Validate_Cmd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Validate_Cmd.BackColor = System.Drawing.Color.SkyBlue
        Me.Validate_Cmd.FlatAppearance.BorderColor = System.Drawing.Color.SkyBlue
        Me.Validate_Cmd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.PaleTurquoise
        Me.Validate_Cmd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue
        Me.Validate_Cmd.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Validate_Cmd.ImageKey = "favicon(76).ico"
        Me.Validate_Cmd.ImageList = Me.ButtonsImageList
        Me.Validate_Cmd.Location = New System.Drawing.Point(491, 266)
        Me.Validate_Cmd.Margin = New System.Windows.Forms.Padding(2)
        Me.Validate_Cmd.Name = "Validate_Cmd"
        Me.Validate_Cmd.Size = New System.Drawing.Size(100, 25)
        Me.Validate_Cmd.TabIndex = 3
        Me.Validate_Cmd.Text = "Validate"
        Me.Validate_Cmd.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.45122!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.54878!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.AccountsRefEdit, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.AccountsEditBT, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label4, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.PeriodsRefEdit, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.PeriodsEditBT, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.EntitiesEditBT, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.EntitiesRefEdit, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(35, 75)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(562, 166)
        Me.TableLayoutPanel1.TabIndex = 7
        '
        'ManualRangesSelectionUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(629, 316)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.Validate_Cmd)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "ManualRangesSelectionUI"
        Me.Text = "Input Ranges Edition"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Validate_Cmd As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PeriodsEditBT As System.Windows.Forms.Button
    Friend WithEvents PeriodsRefEdit As System.Windows.Forms.TextBox
    Friend WithEvents AccountsEditBT As System.Windows.Forms.Button
    Friend WithEvents AccountsRefEdit As System.Windows.Forms.TextBox
    Friend WithEvents EntitiesEditBT As System.Windows.Forms.Button
    Friend WithEvents EntitiesRefEdit As System.Windows.Forms.TextBox
    Friend WithEvents ButtonsImageList As System.Windows.Forms.ImageList
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
End Class
