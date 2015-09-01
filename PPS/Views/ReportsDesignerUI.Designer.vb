<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportsDesignerUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReportsDesignerUI))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.ReportsTVPanel = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.NewSerieBT = New System.Windows.Forms.Button()
        Me.ButtonsImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.NewReportBT = New System.Windows.Forms.Button()
        Me.DeleteReportBT = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ChartNameLabel = New System.Windows.Forms.Label()
        Me.ReportTypeCB = New System.Windows.Forms.ComboBox()
        Me.ReportNameTB = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ReportPaletteCB = New System.Windows.Forms.ComboBox()
        Me.Axis1TB = New System.Windows.Forms.TextBox()
        Me.Axis2TB = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ItemCB = New System.Windows.Forms.ComboBox()
        Me.NameTB = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.ChartPanel = New System.Windows.Forms.Panel()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TypeCB = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.AxisCB = New System.Windows.Forms.ComboBox()
        Me.UnitTB = New System.Windows.Forms.TextBox()
        Me.ColorBT = New System.Windows.Forms.Button()
        Me.WidthNumericUpDown = New System.Windows.Forms.NumericUpDown()
        Me.ValuesDisplayRB = New System.Windows.Forms.CheckBox()
        Me.ReportsTVImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.TVRCM = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.NewReportRCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewSerieRCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.RenameRCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.DeleteRCM = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.WidthNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TVRCM.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TableLayoutPanel2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TableLayoutPanel1)
        Me.SplitContainer1.Size = New System.Drawing.Size(1006, 558)
        Me.SplitContainer1.SplitterDistance = 240
        Me.SplitContainer1.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.ReportsTVPanel, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Panel2, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(240, 558)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'ReportsTVPanel
        '
        Me.ReportsTVPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ReportsTVPanel.Location = New System.Drawing.Point(3, 28)
        Me.ReportsTVPanel.Name = "ReportsTVPanel"
        Me.ReportsTVPanel.Size = New System.Drawing.Size(234, 527)
        Me.ReportsTVPanel.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.NewSerieBT)
        Me.Panel2.Controls.Add(Me.NewReportBT)
        Me.Panel2.Controls.Add(Me.DeleteReportBT)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(1, 1)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(1)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(238, 23)
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
        'NewReportBT
        '
        Me.NewReportBT.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.NewReportBT.FlatAppearance.BorderSize = 0
        Me.NewReportBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.NewReportBT.ImageKey = "favicon(239).ico"
        Me.NewReportBT.ImageList = Me.ButtonsImageList
        Me.NewReportBT.Location = New System.Drawing.Point(11, 1)
        Me.NewReportBT.Name = "NewReportBT"
        Me.NewReportBT.Size = New System.Drawing.Size(22, 22)
        Me.NewReportBT.TabIndex = 13
        Me.NewReportBT.UseVisualStyleBackColor = True
        '
        'DeleteReportBT
        '
        Me.DeleteReportBT.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.DeleteReportBT.FlatAppearance.BorderSize = 0
        Me.DeleteReportBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DeleteReportBT.ImageKey = "imageres_89.ico"
        Me.DeleteReportBT.ImageList = Me.ButtonsImageList
        Me.DeleteReportBT.Location = New System.Drawing.Point(69, 2)
        Me.DeleteReportBT.Name = "DeleteReportBT"
        Me.DeleteReportBT.Size = New System.Drawing.Size(22, 22)
        Me.DeleteReportBT.TabIndex = 12
        Me.DeleteReportBT.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox3, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox4, 1, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.18868!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 59.81132!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(762, 558)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TableLayoutPanel5)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(375, 218)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Report"
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 2
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.59789!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.40211!))
        Me.TableLayoutPanel5.Controls.Add(Me.Label6, 0, 4)
        Me.TableLayoutPanel5.Controls.Add(Me.Label4, 0, 3)
        Me.TableLayoutPanel5.Controls.Add(Me.ChartNameLabel, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.ReportTypeCB, 1, 2)
        Me.TableLayoutPanel5.Controls.Add(Me.ReportNameTB, 1, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.Label3, 0, 2)
        Me.TableLayoutPanel5.Controls.Add(Me.Label5, 0, 1)
        Me.TableLayoutPanel5.Controls.Add(Me.ReportPaletteCB, 1, 1)
        Me.TableLayoutPanel5.Controls.Add(Me.Axis1TB, 1, 3)
        Me.TableLayoutPanel5.Controls.Add(Me.Axis2TB, 1, 4)
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(6, 53)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 5
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(378, 123)
        Me.TableLayoutPanel5.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 96)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(45, 13)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "Axis Y 2"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 72)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 13)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Axis Y 1"
        '
        'ChartNameLabel
        '
        Me.ChartNameLabel.AutoSize = True
        Me.ChartNameLabel.Location = New System.Drawing.Point(3, 0)
        Me.ChartNameLabel.Name = "ChartNameLabel"
        Me.ChartNameLabel.Size = New System.Drawing.Size(35, 13)
        Me.ChartNameLabel.TabIndex = 14
        Me.ChartNameLabel.Text = "Name"
        '
        'ReportTypeCB
        '
        Me.ReportTypeCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ReportTypeCB.FormattingEnabled = True
        Me.ReportTypeCB.Items.AddRange(New Object() {"Table", "Chart"})
        Me.ReportTypeCB.Location = New System.Drawing.Point(130, 51)
        Me.ReportTypeCB.Name = "ReportTypeCB"
        Me.ReportTypeCB.Size = New System.Drawing.Size(187, 21)
        Me.ReportTypeCB.TabIndex = 18
        '
        'ReportNameTB
        '
        Me.ReportNameTB.Enabled = False
        Me.ReportNameTB.Location = New System.Drawing.Point(130, 3)
        Me.ReportNameTB.Name = "ReportNameTB"
        Me.ReportNameTB.Size = New System.Drawing.Size(187, 20)
        Me.ReportNameTB.TabIndex = 13
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 13)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "Chart / Table"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(3, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 13)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Palette"
        '
        'ReportPaletteCB
        '
        Me.ReportPaletteCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ReportPaletteCB.FormattingEnabled = True
        Me.ReportPaletteCB.Location = New System.Drawing.Point(130, 27)
        Me.ReportPaletteCB.Name = "ReportPaletteCB"
        Me.ReportPaletteCB.Size = New System.Drawing.Size(187, 21)
        Me.ReportPaletteCB.TabIndex = 15
        '
        'Axis1TB
        '
        Me.Axis1TB.Location = New System.Drawing.Point(130, 75)
        Me.Axis1TB.Name = "Axis1TB"
        Me.Axis1TB.Size = New System.Drawing.Size(187, 20)
        Me.Axis1TB.TabIndex = 22
        '
        'Axis2TB
        '
        Me.Axis2TB.Location = New System.Drawing.Point(130, 99)
        Me.Axis2TB.Name = "Axis2TB"
        Me.Axis2TB.Size = New System.Drawing.Size(187, 20)
        Me.Axis2TB.TabIndex = 23
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TableLayoutPanel3)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(3, 227)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(375, 328)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Serie"
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 186.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.ItemCB, 1, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.NameTB, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Label2, 0, 1)
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(6, 35)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 4
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(431, 284)
        Me.TableLayoutPanel3.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Name"
        '
        'ItemCB
        '
        Me.ItemCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ItemCB.FormattingEnabled = True
        Me.ItemCB.Location = New System.Drawing.Point(189, 28)
        Me.ItemCB.Name = "ItemCB"
        Me.ItemCB.Size = New System.Drawing.Size(239, 21)
        Me.ItemCB.TabIndex = 0
        '
        'NameTB
        '
        Me.NameTB.Enabled = False
        Me.NameTB.Location = New System.Drawing.Point(189, 3)
        Me.NameTB.Name = "NameTB"
        Me.NameTB.Size = New System.Drawing.Size(239, 20)
        Me.NameTB.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(141, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Financial or Operational Item"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.ChartPanel)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Location = New System.Drawing.Point(384, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(375, 218)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Report Preview"
        '
        'ChartPanel
        '
        Me.ChartPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ChartPanel.Location = New System.Drawing.Point(24, 29)
        Me.ChartPanel.Margin = New System.Windows.Forms.Padding(5)
        Me.ChartPanel.Name = "ChartPanel"
        Me.ChartPanel.Size = New System.Drawing.Size(328, 172)
        Me.ChartPanel.TabIndex = 0
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.TableLayoutPanel4)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox4.Location = New System.Drawing.Point(384, 227)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(375, 328)
        Me.GroupBox4.TabIndex = 3
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Serie Display"
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 2
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 116.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.Label10, 0, 2)
        Me.TableLayoutPanel4.Controls.Add(Me.TypeCB, 1, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.Label11, 0, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.Label13, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.Label15, 0, 4)
        Me.TableLayoutPanel4.Controls.Add(Me.Label16, 0, 3)
        Me.TableLayoutPanel4.Controls.Add(Me.Label17, 0, 5)
        Me.TableLayoutPanel4.Controls.Add(Me.AxisCB, 1, 2)
        Me.TableLayoutPanel4.Controls.Add(Me.UnitTB, 1, 4)
        Me.TableLayoutPanel4.Controls.Add(Me.ColorBT, 1, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.WidthNumericUpDown, 1, 3)
        Me.TableLayoutPanel4.Controls.Add(Me.ValuesDisplayRB, 1, 5)
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(24, 35)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 8
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(431, 284)
        Me.TableLayoutPanel4.TabIndex = 4
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(3, 50)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(26, 13)
        Me.Label10.TabIndex = 10
        Me.Label10.Text = "Axis"
        '
        'TypeCB
        '
        Me.TypeCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TypeCB.FormattingEnabled = True
        Me.TypeCB.Location = New System.Drawing.Point(119, 28)
        Me.TypeCB.Name = "TypeCB"
        Me.TypeCB.Size = New System.Drawing.Size(163, 21)
        Me.TypeCB.TabIndex = 7
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(3, 25)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(31, 13)
        Me.Label11.TabIndex = 9
        Me.Label11.Text = "Type"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(3, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(31, 13)
        Me.Label13.TabIndex = 8
        Me.Label13.Text = "Color"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(3, 100)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(26, 13)
        Me.Label15.TabIndex = 11
        Me.Label15.Text = "Unit"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(3, 75)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(35, 13)
        Me.Label16.TabIndex = 12
        Me.Label16.Text = "Width"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(3, 125)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(68, 13)
        Me.Label17.TabIndex = 13
        Me.Label17.Text = "Values Label"
        '
        'AxisCB
        '
        Me.AxisCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.AxisCB.FormattingEnabled = True
        Me.AxisCB.Items.AddRange(New Object() {"Primary", "Secondary"})
        Me.AxisCB.Location = New System.Drawing.Point(119, 53)
        Me.AxisCB.Name = "AxisCB"
        Me.AxisCB.Size = New System.Drawing.Size(163, 21)
        Me.AxisCB.TabIndex = 14
        '
        'UnitTB
        '
        Me.UnitTB.Location = New System.Drawing.Point(119, 103)
        Me.UnitTB.Name = "UnitTB"
        Me.UnitTB.Size = New System.Drawing.Size(163, 20)
        Me.UnitTB.TabIndex = 16
        '
        'ColorBT
        '
        Me.ColorBT.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ColorBT.FlatAppearance.BorderSize = 0
        Me.ColorBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ColorBT.Location = New System.Drawing.Point(119, 3)
        Me.ColorBT.Name = "ColorBT"
        Me.ColorBT.Size = New System.Drawing.Size(27, 19)
        Me.ColorBT.TabIndex = 18
        Me.ColorBT.UseVisualStyleBackColor = False
        '
        'WidthNumericUpDown
        '
        Me.WidthNumericUpDown.Location = New System.Drawing.Point(119, 78)
        Me.WidthNumericUpDown.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.WidthNumericUpDown.Name = "WidthNumericUpDown"
        Me.WidthNumericUpDown.Size = New System.Drawing.Size(161, 20)
        Me.WidthNumericUpDown.TabIndex = 19
        Me.WidthNumericUpDown.Value = New Decimal(New Integer() {20, 0, 0, 0})
        '
        'ValuesDisplayRB
        '
        Me.ValuesDisplayRB.AutoSize = True
        Me.ValuesDisplayRB.Location = New System.Drawing.Point(119, 128)
        Me.ValuesDisplayRB.Name = "ValuesDisplayRB"
        Me.ValuesDisplayRB.Size = New System.Drawing.Size(15, 14)
        Me.ValuesDisplayRB.TabIndex = 20
        Me.ValuesDisplayRB.UseVisualStyleBackColor = True
        '
        'ReportsTVImageList
        '
        Me.ReportsTVImageList.ImageStream = CType(resources.GetObject("ReportsTVImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ReportsTVImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ReportsTVImageList.Images.SetKeyName(0, "favicon(239).ico")
        Me.ReportsTVImageList.Images.SetKeyName(1, "favicon(176).ico")
        Me.ReportsTVImageList.Images.SetKeyName(2, "favicon(2).ico")
        Me.ReportsTVImageList.Images.SetKeyName(3, "PPS black and white small.ico")
        '
        'ColorDialog1
        '
        Me.ColorDialog1.AnyColor = True
        '
        'TVRCM
        '
        Me.TVRCM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewReportRCM, Me.NewSerieRCM, Me.ToolStripSeparator1, Me.RenameRCM, Me.ToolStripSeparator2, Me.DeleteRCM})
        Me.TVRCM.Name = "TVRCM"
        Me.TVRCM.Size = New System.Drawing.Size(137, 104)
        '
        'NewReportRCM
        '
        Me.NewReportRCM.Image = Global.PPS.My.Resources.Resources.checked
        Me.NewReportRCM.Name = "NewReportRCM"
        Me.NewReportRCM.Size = New System.Drawing.Size(136, 22)
        Me.NewReportRCM.Text = "New Report"
        '
        'NewSerieRCM
        '
        Me.NewSerieRCM.Image = Global.PPS.My.Resources.Resources.favicon_233_
        Me.NewSerieRCM.Name = "NewSerieRCM"
        Me.NewSerieRCM.Size = New System.Drawing.Size(136, 22)
        Me.NewSerieRCM.Text = "New Serie"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(133, 6)
        '
        'RenameRCM
        '
        Me.RenameRCM.Name = "RenameRCM"
        Me.RenameRCM.Size = New System.Drawing.Size(136, 22)
        Me.RenameRCM.Text = "Rename"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(133, 6)
        '
        'DeleteRCM
        '
        Me.DeleteRCM.Image = Global.PPS.My.Resources.Resources.imageres_89
        Me.DeleteRCM.Name = "DeleteRCM"
        Me.DeleteRCM.Size = New System.Drawing.Size(136, 22)
        Me.DeleteRCM.Text = "Delete"
        '
        'ReportsDesignerUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1006, 558)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ReportsDesignerUI"
        Me.Text = "Reports Designer"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        CType(Me.WidthNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TVRCM.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ReportsTVImageList As System.Windows.Forms.ImageList
    Friend WithEvents ButtonsImageList As System.Windows.Forms.ImageList
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ReportsTVPanel As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents NewSerieBT As System.Windows.Forms.Button
    Friend WithEvents NewReportBT As System.Windows.Forms.Button
    Friend WithEvents DeleteReportBT As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ReportPaletteCB As System.Windows.Forms.ComboBox
    Friend WithEvents ChartNameLabel As System.Windows.Forms.Label
    Friend WithEvents ReportNameTB As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents NameTB As System.Windows.Forms.TextBox
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ItemCB As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ChartPanel As System.Windows.Forms.Panel
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TypeCB As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents AxisCB As System.Windows.Forms.ComboBox
    Friend WithEvents UnitTB As System.Windows.Forms.TextBox
    Friend WithEvents ColorBT As System.Windows.Forms.Button
    Friend WithEvents WidthNumericUpDown As System.Windows.Forms.NumericUpDown
    Friend WithEvents ReportTypeCB As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel5 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Axis1TB As System.Windows.Forms.TextBox
    Friend WithEvents Axis2TB As System.Windows.Forms.TextBox
    Friend WithEvents TVRCM As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents NewReportRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewSerieRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DeleteRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RenameRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ValuesDisplayRB As System.Windows.Forms.CheckBox
End Class
