<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ControlsControl
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ControlsControl))
        Me.TabControl1 = New System.Windows.Forms.CustomTabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ControlsDGVPanel = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.AddControlBT = New System.Windows.Forms.Button()
        Me.ButtonsImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.DeleteControlBT = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.ChartsTVPanel = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.NewSerieBT = New System.Windows.Forms.Button()
        Me.NewChartBT = New System.Windows.Forms.Button()
        Me.DeleteChartsBT = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PalettesCB = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ColorBT = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.SerieAccountIDCB = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SerieNameTB = New System.Windows.Forms.TextBox()
        Me.SerieTypeCB = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ChartNameLabel = New System.Windows.Forms.Label()
        Me.ChartNameTB = New System.Windows.Forms.TextBox()
        Me.ChartsTVImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.ChartsTVRCM = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.NewChartRCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewSerieRCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.RenameRCBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.DeleteRCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.ChartsTVRCM.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.DisplayStyle = System.Windows.Forms.TabStyle.Rounded
        '
        '
        '
        Me.TabControl1.DisplayStyleProvider.BorderColor = System.Drawing.SystemColors.ControlDark
        Me.TabControl1.DisplayStyleProvider.BorderColorHot = System.Drawing.SystemColors.ControlDark
        Me.TabControl1.DisplayStyleProvider.BorderColorSelected = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.TabControl1.DisplayStyleProvider.CloserColor = System.Drawing.Color.DarkGray
        Me.TabControl1.DisplayStyleProvider.FocusTrack = False
        Me.TabControl1.DisplayStyleProvider.HotTrack = True
        Me.TabControl1.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.TabControl1.DisplayStyleProvider.Opacity = 1.0!
        Me.TabControl1.DisplayStyleProvider.Overlap = 0
        Me.TabControl1.DisplayStyleProvider.Padding = New System.Drawing.Point(6, 3)
        Me.TabControl1.DisplayStyleProvider.Radius = 10
        Me.TabControl1.DisplayStyleProvider.ShowTabCloser = False
        Me.TabControl1.DisplayStyleProvider.TextColor = System.Drawing.SystemColors.ControlText
        Me.TabControl1.DisplayStyleProvider.TextColorDisabled = System.Drawing.SystemColors.ControlDark
        Me.TabControl1.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.HotTrack = True
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(952, 552)
        Me.TabControl1.TabIndex = 9
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage1.Controls.Add(Me.TableLayoutPanel1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 23)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(944, 525)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Numeric Controls"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.ControlsDGVPanel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(938, 519)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'ControlsDGVPanel
        '
        Me.ControlsDGVPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ControlsDGVPanel.Location = New System.Drawing.Point(3, 28)
        Me.ControlsDGVPanel.Name = "ControlsDGVPanel"
        Me.ControlsDGVPanel.Size = New System.Drawing.Size(932, 488)
        Me.ControlsDGVPanel.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.Controls.Add(Me.AddControlBT)
        Me.Panel1.Controls.Add(Me.DeleteControlBT)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(938, 25)
        Me.Panel1.TabIndex = 1
        '
        'AddControlBT
        '
        Me.AddControlBT.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.AddControlBT.FlatAppearance.BorderSize = 0
        Me.AddControlBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.AddControlBT.ImageKey = "add blue.jpg"
        Me.AddControlBT.ImageList = Me.ButtonsImageList
        Me.AddControlBT.Location = New System.Drawing.Point(11, 1)
        Me.AddControlBT.Name = "AddControlBT"
        Me.AddControlBT.Size = New System.Drawing.Size(22, 22)
        Me.AddControlBT.TabIndex = 12
        Me.AddControlBT.UseVisualStyleBackColor = True
        '
        'ButtonsImageList
        '
        Me.ButtonsImageList.ImageStream = CType(resources.GetObject("ButtonsImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonsImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonsImageList.Images.SetKeyName(0, "favicon(236).ico")
        Me.ButtonsImageList.Images.SetKeyName(1, "Refresh2.png")
        Me.ButtonsImageList.Images.SetKeyName(2, "Target zoomed.png")
        Me.ButtonsImageList.Images.SetKeyName(3, "Report.png")
        Me.ButtonsImageList.Images.SetKeyName(4, "favicon(187).ico")
        Me.ButtonsImageList.Images.SetKeyName(5, "folder 2 ctrl bgd.png")
        Me.ButtonsImageList.Images.SetKeyName(6, "favicon(239).ico")
        Me.ButtonsImageList.Images.SetKeyName(7, "favicon(188).ico")
        Me.ButtonsImageList.Images.SetKeyName(8, "add blue.jpg")
        Me.ButtonsImageList.Images.SetKeyName(9, "imageres_89.ico")
        '
        'DeleteControlBT
        '
        Me.DeleteControlBT.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.DeleteControlBT.FlatAppearance.BorderSize = 0
        Me.DeleteControlBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DeleteControlBT.ImageKey = "imageres_89.ico"
        Me.DeleteControlBT.ImageList = Me.ButtonsImageList
        Me.DeleteControlBT.Location = New System.Drawing.Point(42, 2)
        Me.DeleteControlBT.Name = "DeleteControlBT"
        Me.DeleteControlBT.Size = New System.Drawing.Size(22, 22)
        Me.DeleteControlBT.TabIndex = 11
        Me.DeleteControlBT.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage2.Controls.Add(Me.SplitContainer1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 23)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(944, 525)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Visual Controls"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TableLayoutPanel2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label5)
        Me.SplitContainer1.Panel2.Controls.Add(Me.PalettesCB)
        Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBox1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ChartNameLabel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ChartNameTB)
        Me.SplitContainer1.Size = New System.Drawing.Size(938, 519)
        Me.SplitContainer1.SplitterDistance = 232
        Me.SplitContainer1.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.ChartsTVPanel, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Panel2, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(232, 519)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'ChartsTVPanel
        '
        Me.ChartsTVPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ChartsTVPanel.Location = New System.Drawing.Point(3, 28)
        Me.ChartsTVPanel.Name = "ChartsTVPanel"
        Me.ChartsTVPanel.Size = New System.Drawing.Size(226, 488)
        Me.ChartsTVPanel.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.NewSerieBT)
        Me.Panel2.Controls.Add(Me.NewChartBT)
        Me.Panel2.Controls.Add(Me.DeleteChartsBT)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(1, 1)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(1)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(230, 23)
        Me.Panel2.TabIndex = 1
        '
        'NewSerieBT
        '
        Me.NewSerieBT.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.NewSerieBT.FlatAppearance.BorderSize = 0
        Me.NewSerieBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.NewSerieBT.ImageKey = "favicon(188).ico"
        Me.NewSerieBT.ImageList = Me.ButtonsImageList
        Me.NewSerieBT.Location = New System.Drawing.Point(39, 1)
        Me.NewSerieBT.Name = "NewSerieBT"
        Me.NewSerieBT.Size = New System.Drawing.Size(22, 22)
        Me.NewSerieBT.TabIndex = 14
        Me.NewSerieBT.UseVisualStyleBackColor = True
        '
        'NewChartBT
        '
        Me.NewChartBT.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.NewChartBT.FlatAppearance.BorderSize = 0
        Me.NewChartBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.NewChartBT.ImageKey = "favicon(239).ico"
        Me.NewChartBT.ImageList = Me.ButtonsImageList
        Me.NewChartBT.Location = New System.Drawing.Point(11, 1)
        Me.NewChartBT.Name = "NewChartBT"
        Me.NewChartBT.Size = New System.Drawing.Size(22, 22)
        Me.NewChartBT.TabIndex = 13
        Me.NewChartBT.UseVisualStyleBackColor = True
        '
        'DeleteChartsBT
        '
        Me.DeleteChartsBT.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.DeleteChartsBT.FlatAppearance.BorderSize = 0
        Me.DeleteChartsBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DeleteChartsBT.ImageKey = "imageres_89.ico"
        Me.DeleteChartsBT.ImageList = Me.ButtonsImageList
        Me.DeleteChartsBT.Location = New System.Drawing.Point(69, 2)
        Me.DeleteChartsBT.Name = "DeleteChartsBT"
        Me.DeleteChartsBT.Size = New System.Drawing.Size(22, 22)
        Me.DeleteChartsBT.TabIndex = 12
        Me.DeleteChartsBT.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(340, 50)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(107, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Chart's Colors Palette"
        '
        'PalettesCB
        '
        Me.PalettesCB.FormattingEnabled = True
        Me.PalettesCB.Location = New System.Drawing.Point(335, 75)
        Me.PalettesCB.Name = "PalettesCB"
        Me.PalettesCB.Size = New System.Drawing.Size(242, 21)
        Me.PalettesCB.TabIndex = 11
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ColorBT)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.SerieAccountIDCB)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.SerieNameTB)
        Me.GroupBox1.Controls.Add(Me.SerieTypeCB)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(59, 169)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(607, 310)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Serie"
        '
        'ColorBT
        '
        Me.ColorBT.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ColorBT.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.ColorBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ColorBT.Location = New System.Drawing.Point(370, 118)
        Me.ColorBT.Name = "ColorBT"
        Me.ColorBT.Size = New System.Drawing.Size(27, 19)
        Me.ColorBT.TabIndex = 19
        Me.ColorBT.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(25, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Serie's Name"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(296, 156)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Serie's Type"
        '
        'SerieAccountIDCB
        '
        Me.SerieAccountIDCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SerieAccountIDCB.FormattingEnabled = True
        Me.SerieAccountIDCB.Location = New System.Drawing.Point(293, 72)
        Me.SerieAccountIDCB.Name = "SerieAccountIDCB"
        Me.SerieAccountIDCB.Size = New System.Drawing.Size(225, 21)
        Me.SerieAccountIDCB.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(296, 121)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Serie's Color"
        '
        'SerieNameTB
        '
        Me.SerieNameTB.Enabled = False
        Me.SerieNameTB.Location = New System.Drawing.Point(22, 72)
        Me.SerieNameTB.Name = "SerieNameTB"
        Me.SerieNameTB.Size = New System.Drawing.Size(223, 20)
        Me.SerieNameTB.TabIndex = 3
        '
        'SerieTypeCB
        '
        Me.SerieTypeCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SerieTypeCB.FormattingEnabled = True
        Me.SerieTypeCB.Location = New System.Drawing.Point(293, 182)
        Me.SerieTypeCB.Name = "SerieTypeCB"
        Me.SerieTypeCB.Size = New System.Drawing.Size(225, 21)
        Me.SerieTypeCB.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(296, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(175, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Serie's Financial or Operational Item"
        '
        'ChartNameLabel
        '
        Me.ChartNameLabel.AutoSize = True
        Me.ChartNameLabel.Location = New System.Drawing.Point(62, 50)
        Me.ChartNameLabel.Name = "ChartNameLabel"
        Me.ChartNameLabel.Size = New System.Drawing.Size(70, 13)
        Me.ChartNameLabel.TabIndex = 2
        Me.ChartNameLabel.Text = "Chart's Name"
        '
        'ChartNameTB
        '
        Me.ChartNameTB.Enabled = False
        Me.ChartNameTB.Location = New System.Drawing.Point(59, 76)
        Me.ChartNameTB.Name = "ChartNameTB"
        Me.ChartNameTB.Size = New System.Drawing.Size(223, 20)
        Me.ChartNameTB.TabIndex = 1
        '
        'ChartsTVImageList
        '
        Me.ChartsTVImageList.ImageStream = CType(resources.GetObject("ChartsTVImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ChartsTVImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ChartsTVImageList.Images.SetKeyName(0, "favicon(239).ico")
        Me.ChartsTVImageList.Images.SetKeyName(1, "favicon(176).ico")
        '
        'ChartsTVRCM
        '
        Me.ChartsTVRCM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewChartRCM, Me.NewSerieRCM, Me.ToolStripSeparator2, Me.RenameRCBT, Me.ToolStripSeparator1, Me.DeleteRCM})
        Me.ChartsTVRCM.Name = "ContextMenuStrip1"
        Me.ChartsTVRCM.Size = New System.Drawing.Size(131, 104)
        '
        'NewChartRCM
        '
        Me.NewChartRCM.Image = Global.PPS.My.Resources.Resources.favicon_239_
        Me.NewChartRCM.Name = "NewChartRCM"
        Me.NewChartRCM.Size = New System.Drawing.Size(130, 22)
        Me.NewChartRCM.Text = "New Chart"
        '
        'NewSerieRCM
        '
        Me.NewSerieRCM.Image = Global.PPS.My.Resources.Resources.favicon_188_
        Me.NewSerieRCM.Name = "NewSerieRCM"
        Me.NewSerieRCM.Size = New System.Drawing.Size(130, 22)
        Me.NewSerieRCM.Text = "New Serie"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(127, 6)
        '
        'RenameRCBT
        '
        Me.RenameRCBT.Image = Global.PPS.My.Resources.Resources.down_arrow
        Me.RenameRCBT.Name = "RenameRCBT"
        Me.RenameRCBT.Size = New System.Drawing.Size(130, 22)
        Me.RenameRCBT.Text = "Rename"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(127, 6)
        '
        'DeleteRCM
        '
        Me.DeleteRCM.Image = Global.PPS.My.Resources.Resources.imageres_89
        Me.DeleteRCM.Name = "DeleteRCM"
        Me.DeleteRCM.Size = New System.Drawing.Size(130, 22)
        Me.DeleteRCM.Text = "Delete"
        '
        'ColorDialog1
        '
        Me.ColorDialog1.AnyColor = True
        Me.ColorDialog1.Color = System.Drawing.Color.White
        '
        'ControlsControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "ControlsControl"
        Me.Size = New System.Drawing.Size(952, 552)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ChartsTVRCM.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.CustomTabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ControlsDGVPanel As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents AddControlBT As System.Windows.Forms.Button
    Friend WithEvents DeleteControlBT As System.Windows.Forms.Button
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ChartsTVPanel As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents NewSerieBT As System.Windows.Forms.Button
    Friend WithEvents NewChartBT As System.Windows.Forms.Button
    Friend WithEvents DeleteChartsBT As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents PalettesCB As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ColorBT As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents SerieAccountIDCB As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents SerieNameTB As System.Windows.Forms.TextBox
    Friend WithEvents SerieTypeCB As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ChartNameLabel As System.Windows.Forms.Label
    Friend WithEvents ChartNameTB As System.Windows.Forms.TextBox
    Friend WithEvents ButtonsImageList As System.Windows.Forms.ImageList
    Friend WithEvents ChartsTVImageList As System.Windows.Forms.ImageList
    Friend WithEvents ChartsTVRCM As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents NewChartRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewSerieRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents RenameRCBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DeleteRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog

End Class
