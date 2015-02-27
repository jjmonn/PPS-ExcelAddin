<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CInputSelectionPane
    Inherits AddinExpress.XL.ADXExcelTaskPane

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CInputSelectionPane))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.EntitiesTV = New System.Windows.Forms.TreeView()
        Me.ValidateBT = New System.Windows.Forms.Button()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.PeriodsLB = New System.Windows.Forms.CheckedListBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SelectCB = New System.Windows.Forms.CheckBox()
        Me.EntitiesTVImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label1.Location = New System.Drawing.Point(3, 4)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Select an Entity"
        '
        'EntitiesTV
        '
        Me.EntitiesTV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.EntitiesTV.Location = New System.Drawing.Point(3, 25)
        Me.EntitiesTV.Name = "EntitiesTV"
        Me.EntitiesTV.Size = New System.Drawing.Size(253, 439)
        Me.EntitiesTV.TabIndex = 1
        '
        'ValidateBT
        '
        Me.ValidateBT.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.ValidateBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ValidateBT.ImageKey = "1420498403_340208.ico"
        Me.ValidateBT.ImageList = Me.ImageList1
        Me.ValidateBT.Location = New System.Drawing.Point(157, 656)
        Me.ValidateBT.Name = "ValidateBT"
        Me.ValidateBT.Size = New System.Drawing.Size(99, 25)
        Me.ValidateBT.TabIndex = 2
        Me.ValidateBT.Text = "Open Edition"
        Me.ValidateBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ValidateBT.UseVisualStyleBackColor = True
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "1420498403_340208.ico")
        '
        'PeriodsLB
        '
        Me.PeriodsLB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PeriodsLB.FormattingEnabled = True
        Me.PeriodsLB.Location = New System.Drawing.Point(3, 527)
        Me.PeriodsLB.Name = "PeriodsLB"
        Me.PeriodsLB.Size = New System.Drawing.Size(253, 122)
        Me.PeriodsLB.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label2.Location = New System.Drawing.Point(3, 471)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Select Periods"
        '
        'SelectCB
        '
        Me.SelectCB.AutoSize = True
        Me.SelectCB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.SelectCB.Location = New System.Drawing.Point(3, 499)
        Me.SelectCB.Name = "SelectCB"
        Me.SelectCB.Size = New System.Drawing.Size(104, 17)
        Me.SelectCB.TabIndex = 5
        Me.SelectCB.Text = "Select All/ None"
        Me.SelectCB.UseVisualStyleBackColor = True
        '
        'EntitiesTVImageList
        '
        Me.EntitiesTVImageList.ImageStream = CType(resources.GetObject("EntitiesTVImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.EntitiesTVImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.EntitiesTVImageList.Images.SetKeyName(0, "favicon(81).ico")
        Me.EntitiesTVImageList.Images.SetKeyName(1, "config purple circle small.png")
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ValidateBT, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.PeriodsLB, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.SelectCB, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.EntitiesTV, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 6
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.781705!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.21829!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 128.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(259, 685)
        Me.TableLayoutPanel1.TabIndex = 6
        '
        'CInputSelectionPane
        '
        Me.BackColor = System.Drawing.SystemColors.GrayText
        Me.ClientSize = New System.Drawing.Size(259, 685)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "CInputSelectionPane"
        Me.Text = "Data Edition"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents EntitiesTV As System.Windows.Forms.TreeView
    Friend WithEvents ValidateBT As System.Windows.Forms.Button
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents PeriodsLB As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents SelectCB As System.Windows.Forms.CheckBox
    Friend WithEvents EntitiesTVImageList As System.Windows.Forms.ImageList
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel

End Class
