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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DisplayControl))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.columnsDisplayList = New VIBlend.WinForms.Controls.vListBox()
        Me.rowsDisplayList = New VIBlend.WinForms.Controls.vListBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DimensionsTVPanel = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DimensionsTVPanel, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(10)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43.37742!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 56.62257!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(277, 616)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Label2, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Label1, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.columnsDisplayList, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.rowsDisplayList, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Button1, 1, 3)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(10, 297)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(10)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 4
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(257, 309)
        Me.TableLayoutPanel2.TabIndex = 5
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.ImageKey = "refresh blue.ico"
        Me.Button1.ImageList = Me.ImageList1
        Me.Button1.Location = New System.Drawing.Point(179, 281)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 25)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Update"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "refresh blue.ico")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(3, 134)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(122, 15)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Rows Labels"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(131, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(123, 15)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Columns Labels"
        '
        'columnsDisplayList
        '
        Me.columnsDisplayList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.columnsDisplayList.Location = New System.Drawing.Point(131, 23)
        Me.columnsDisplayList.Name = "columnsDisplayList"
        Me.columnsDisplayList.RoundedCornersMaskListItem = CType(15, Byte)
        Me.columnsDisplayList.Size = New System.Drawing.Size(123, 123)
        Me.columnsDisplayList.TabIndex = 5
        Me.columnsDisplayList.Text = "VListBox1"
        Me.columnsDisplayList.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        Me.columnsDisplayList.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        '
        'rowsDisplayList
        '
        Me.rowsDisplayList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rowsDisplayList.Location = New System.Drawing.Point(3, 152)
        Me.rowsDisplayList.Name = "rowsDisplayList"
        Me.rowsDisplayList.RoundedCornersMaskListItem = CType(15, Byte)
        Me.rowsDisplayList.Size = New System.Drawing.Size(122, 123)
        Me.rowsDisplayList.TabIndex = 6
        Me.rowsDisplayList.Text = "VListBox1"
        Me.rowsDisplayList.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        Me.rowsDisplayList.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Location = New System.Drawing.Point(10, 20)
        Me.Label3.Margin = New System.Windows.Forms.Padding(10, 3, 3, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(264, 15)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Choose fields to add to report:"
        '
        'DimensionsTVPanel
        '
        Me.DimensionsTVPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DimensionsTVPanel.Location = New System.Drawing.Point(10, 45)
        Me.DimensionsTVPanel.Margin = New System.Windows.Forms.Padding(10)
        Me.DimensionsTVPanel.Name = "DimensionsTVPanel"
        Me.DimensionsTVPanel.Size = New System.Drawing.Size(257, 232)
        Me.DimensionsTVPanel.TabIndex = 7
        '
        'DisplayControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "DisplayControl"
        Me.Size = New System.Drawing.Size(277, 616)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DimensionsTVPanel As System.Windows.Forms.Panel
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Public WithEvents columnsDisplayList As VIBlend.WinForms.Controls.vListBox
    Public WithEvents rowsDisplayList As VIBlend.WinForms.Controls.vListBox

End Class
