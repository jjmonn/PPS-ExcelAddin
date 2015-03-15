<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExcelRatesImportUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ExcelRatesImportUI))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.periods_RefEdit = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.periods_edit_BT = New System.Windows.Forms.Button()
        Me.ButtonsImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.Label4 = New System.Windows.Forms.Label()
        Me.rates_edit_BT = New System.Windows.Forms.Button()
        Me.rates_RefEdit = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.items_CB = New System.Windows.Forms.ComboBox()
        Me.import_BT = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.45122!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.54878!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.periods_RefEdit, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.periods_edit_BT, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label4, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.rates_edit_BT, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.rates_RefEdit, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.items_CB, 1, 2)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(12, 29)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(399, 110)
        Me.TableLayoutPanel1.TabIndex = 8
        '
        'periods_RefEdit
        '
        Me.periods_RefEdit.Location = New System.Drawing.Point(124, 2)
        Me.periods_RefEdit.Margin = New System.Windows.Forms.Padding(2)
        Me.periods_RefEdit.Name = "periods_RefEdit"
        Me.periods_RefEdit.Size = New System.Drawing.Size(230, 20)
        Me.periods_RefEdit.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(2, 0)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Periods"
        '
        'periods_edit_BT
        '
        Me.periods_edit_BT.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.periods_edit_BT.ImageIndex = 0
        Me.periods_edit_BT.ImageList = Me.ButtonsImageList
        Me.periods_edit_BT.Location = New System.Drawing.Point(358, 2)
        Me.periods_edit_BT.Margin = New System.Windows.Forms.Padding(2)
        Me.periods_edit_BT.Name = "periods_edit_BT"
        Me.periods_edit_BT.Size = New System.Drawing.Size(27, 27)
        Me.periods_edit_BT.TabIndex = 2
        Me.periods_edit_BT.UseVisualStyleBackColor = True
        '
        'ButtonsImageList
        '
        Me.ButtonsImageList.ImageStream = CType(resources.GetObject("ButtonsImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonsImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonsImageList.Images.SetKeyName(0, "favicon(161).ico")
        Me.ButtonsImageList.Images.SetKeyName(1, "favicon(132).ico")
        Me.ButtonsImageList.Images.SetKeyName(2, "favicon(76).ico")
        Me.ButtonsImageList.Images.SetKeyName(3, "favicon(187).ico")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(2, 72)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Currency"
        '
        'rates_edit_BT
        '
        Me.rates_edit_BT.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.rates_edit_BT.ImageIndex = 0
        Me.rates_edit_BT.ImageList = Me.ButtonsImageList
        Me.rates_edit_BT.Location = New System.Drawing.Point(358, 38)
        Me.rates_edit_BT.Margin = New System.Windows.Forms.Padding(2)
        Me.rates_edit_BT.Name = "rates_edit_BT"
        Me.rates_edit_BT.Size = New System.Drawing.Size(27, 27)
        Me.rates_edit_BT.TabIndex = 4
        Me.rates_edit_BT.UseVisualStyleBackColor = True
        '
        'rates_RefEdit
        '
        Me.rates_RefEdit.Location = New System.Drawing.Point(124, 38)
        Me.rates_RefEdit.Margin = New System.Windows.Forms.Padding(2)
        Me.rates_RefEdit.Name = "rates_RefEdit"
        Me.rates_RefEdit.Size = New System.Drawing.Size(230, 20)
        Me.rates_RefEdit.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(2, 36)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Rates"
        '
        'items_CB
        '
        Me.items_CB.FormattingEnabled = True
        Me.items_CB.Location = New System.Drawing.Point(125, 75)
        Me.items_CB.Name = "items_CB"
        Me.items_CB.Size = New System.Drawing.Size(228, 21)
        Me.items_CB.TabIndex = 5
        '
        'import_BT
        '
        Me.import_BT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.import_BT.ImageIndex = 3
        Me.import_BT.ImageList = Me.ButtonsImageList
        Me.import_BT.Location = New System.Drawing.Point(299, 160)
        Me.import_BT.Name = "import_BT"
        Me.import_BT.Size = New System.Drawing.Size(101, 28)
        Me.import_BT.TabIndex = 6
        Me.import_BT.Text = "Import"
        Me.import_BT.UseVisualStyleBackColor = True
        '
        'InputValuesExcel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(423, 201)
        Me.Controls.Add(Me.import_BT)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "InputValuesExcel"
        Me.Text = "InputRatesExcel"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents periods_RefEdit As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents periods_edit_BT As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents rates_edit_BT As System.Windows.Forms.Button
    Friend WithEvents rates_RefEdit As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ButtonsImageList As System.Windows.Forms.ImageList
    Friend WithEvents items_CB As System.Windows.Forms.ComboBox
    Friend WithEvents import_BT As System.Windows.Forms.Button
End Class
