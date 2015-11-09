<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CUI2Visualization
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
        Dim ChartArea5 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend5 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim ChartArea6 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend6 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim ChartArea7 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend7 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim ChartArea8 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend8 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CUI2Visualization))
        Me.VSplitContainer1 = New VIBlend.WinForms.Controls.vSplitContainer()
        Me.VSplitContainer3 = New VIBlend.WinForms.Controls.vSplitContainer()
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Chart3 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.VSplitContainer2 = New VIBlend.WinForms.Controls.vSplitContainer()
        Me.Chart2 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Chart4 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.VersionTB = New VIBlend.WinForms.Controls.vTextBox()
        Me.CurrencyTB = New VIBlend.WinForms.Controls.vTextBox()
        Me.EntityTB = New VIBlend.WinForms.Controls.vTextBox()
        Me.m_currencyLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_versionLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_entityLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_chartsRightClickMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.m_editChartButton = New System.Windows.Forms.ToolStripMenuItem()
        Me.m_dropChartOnExcelButton = New System.Windows.Forms.ToolStripMenuItem()
        Me.m_refreshButton = New VIBlend.WinForms.Controls.vButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.VSplitContainer1.Panel1.SuspendLayout()
        Me.VSplitContainer1.Panel2.SuspendLayout()
        Me.VSplitContainer1.SuspendLayout()
        Me.VSplitContainer3.Panel1.SuspendLayout()
        Me.VSplitContainer3.Panel2.SuspendLayout()
        Me.VSplitContainer3.SuspendLayout()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Chart3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.VSplitContainer2.Panel1.SuspendLayout()
        Me.VSplitContainer2.Panel2.SuspendLayout()
        Me.VSplitContainer2.SuspendLayout()
        CType(Me.Chart2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Chart4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.m_chartsRightClickMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'VSplitContainer1
        '
        Me.VSplitContainer1.AllowAnimations = True
        Me.VSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VSplitContainer1.Location = New System.Drawing.Point(3, 28)
        Me.VSplitContainer1.Name = "VSplitContainer1"
        Me.VSplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'VSplitContainer1.Panel1
        '
        Me.VSplitContainer1.Panel1.BackColor = System.Drawing.Color.White
        Me.VSplitContainer1.Panel1.BorderColor = System.Drawing.Color.Silver
        Me.VSplitContainer1.Panel1.Controls.Add(Me.VSplitContainer3)
        Me.VSplitContainer1.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.VSplitContainer1.Panel1.Name = "Panel1"
        Me.VSplitContainer1.Panel1.Size = New System.Drawing.Size(346, 388)
        Me.VSplitContainer1.Panel1.TabIndex = 1
        '
        'VSplitContainer1.Panel2
        '
        Me.VSplitContainer1.Panel2.BackColor = System.Drawing.Color.White
        Me.VSplitContainer1.Panel2.BorderColor = System.Drawing.Color.Silver
        Me.VSplitContainer1.Panel2.Controls.Add(Me.VSplitContainer2)
        Me.VSplitContainer1.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VSplitContainer1.Panel2.Location = New System.Drawing.Point(351, 0)
        Me.VSplitContainer1.Panel2.Name = "Panel2"
        Me.VSplitContainer1.Panel2.Size = New System.Drawing.Size(347, 388)
        Me.VSplitContainer1.Panel2.TabIndex = 2
        Me.VSplitContainer1.Size = New System.Drawing.Size(698, 388)
        Me.VSplitContainer1.SplitterSize = 5
        Me.VSplitContainer1.StyleKey = "Splitter"
        Me.VSplitContainer1.TabIndex = 0
        Me.VSplitContainer1.Text = "VSplitContainer1"
        Me.VSplitContainer1.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER
        '
        'VSplitContainer3
        '
        Me.VSplitContainer3.AllowAnimations = True
        Me.VSplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VSplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.VSplitContainer3.Name = "VSplitContainer3"
        Me.VSplitContainer3.Orientation = System.Windows.Forms.Orientation.Vertical
        '
        'VSplitContainer3.Panel1
        '
        Me.VSplitContainer3.Panel1.BackColor = System.Drawing.Color.White
        Me.VSplitContainer3.Panel1.BorderColor = System.Drawing.Color.Silver
        Me.VSplitContainer3.Panel1.Controls.Add(Me.Chart1)
        Me.VSplitContainer3.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.VSplitContainer3.Panel1.Name = "Panel1"
        Me.VSplitContainer3.Panel1.Size = New System.Drawing.Size(346, 191)
        Me.VSplitContainer3.Panel1.TabIndex = 1
        '
        'VSplitContainer3.Panel2
        '
        Me.VSplitContainer3.Panel2.BackColor = System.Drawing.Color.White
        Me.VSplitContainer3.Panel2.BorderColor = System.Drawing.Color.Silver
        Me.VSplitContainer3.Panel2.Controls.Add(Me.Chart3)
        Me.VSplitContainer3.Panel2.Location = New System.Drawing.Point(0, 196)
        Me.VSplitContainer3.Panel2.Name = "Panel2"
        Me.VSplitContainer3.Panel2.Size = New System.Drawing.Size(346, 192)
        Me.VSplitContainer3.Panel2.TabIndex = 2
        Me.VSplitContainer3.Size = New System.Drawing.Size(346, 388)
        Me.VSplitContainer3.SplitterSize = 5
        Me.VSplitContainer3.StyleKey = "Splitter"
        Me.VSplitContainer3.TabIndex = 0
        Me.VSplitContainer3.Text = "VSplitContainer3"
        Me.VSplitContainer3.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER
        '
        'Chart1
        '
        ChartArea5.Name = "ChartArea1"
        Me.Chart1.ChartAreas.Add(ChartArea5)
        Me.Chart1.Dock = System.Windows.Forms.DockStyle.Fill
        Legend5.Name = "Legend1"
        Me.Chart1.Legends.Add(Legend5)
        Me.Chart1.Location = New System.Drawing.Point(0, 0)
        Me.Chart1.Name = "Chart1"
        Me.Chart1.Size = New System.Drawing.Size(346, 191)
        Me.Chart1.TabIndex = 0
        Me.Chart1.Text = "Chart1"
        '
        'Chart3
        '
        ChartArea6.Name = "ChartArea1"
        Me.Chart3.ChartAreas.Add(ChartArea6)
        Me.Chart3.Dock = System.Windows.Forms.DockStyle.Fill
        Legend6.Name = "Legend1"
        Me.Chart3.Legends.Add(Legend6)
        Me.Chart3.Location = New System.Drawing.Point(0, 0)
        Me.Chart3.Name = "Chart3"
        Me.Chart3.Size = New System.Drawing.Size(346, 192)
        Me.Chart3.TabIndex = 1
        Me.Chart3.Text = "Chart3"
        '
        'VSplitContainer2
        '
        Me.VSplitContainer2.AllowAnimations = True
        Me.VSplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VSplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.VSplitContainer2.Name = "VSplitContainer2"
        Me.VSplitContainer2.Orientation = System.Windows.Forms.Orientation.Vertical
        '
        'VSplitContainer2.Panel1
        '
        Me.VSplitContainer2.Panel1.BackColor = System.Drawing.Color.White
        Me.VSplitContainer2.Panel1.BorderColor = System.Drawing.Color.Silver
        Me.VSplitContainer2.Panel1.Controls.Add(Me.Chart2)
        Me.VSplitContainer2.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.VSplitContainer2.Panel1.Name = "Panel1"
        Me.VSplitContainer2.Panel1.Size = New System.Drawing.Size(347, 191)
        Me.VSplitContainer2.Panel1.TabIndex = 1
        '
        'VSplitContainer2.Panel2
        '
        Me.VSplitContainer2.Panel2.BackColor = System.Drawing.Color.White
        Me.VSplitContainer2.Panel2.BorderColor = System.Drawing.Color.Silver
        Me.VSplitContainer2.Panel2.Controls.Add(Me.Chart4)
        Me.VSplitContainer2.Panel2.Location = New System.Drawing.Point(0, 196)
        Me.VSplitContainer2.Panel2.Name = "Panel2"
        Me.VSplitContainer2.Panel2.Size = New System.Drawing.Size(347, 192)
        Me.VSplitContainer2.Panel2.TabIndex = 2
        Me.VSplitContainer2.Size = New System.Drawing.Size(347, 388)
        Me.VSplitContainer2.SplitterSize = 5
        Me.VSplitContainer2.StyleKey = "Splitter"
        Me.VSplitContainer2.TabIndex = 0
        Me.VSplitContainer2.Text = "VSplitContainer2"
        Me.VSplitContainer2.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER
        '
        'Chart2
        '
        ChartArea7.Name = "ChartArea1"
        Me.Chart2.ChartAreas.Add(ChartArea7)
        Me.Chart2.Dock = System.Windows.Forms.DockStyle.Fill
        Legend7.Name = "Legend1"
        Me.Chart2.Legends.Add(Legend7)
        Me.Chart2.Location = New System.Drawing.Point(0, 0)
        Me.Chart2.Name = "Chart2"
        Me.Chart2.Size = New System.Drawing.Size(347, 191)
        Me.Chart2.TabIndex = 1
        Me.Chart2.Text = "Chart2"
        '
        'Chart4
        '
        ChartArea8.Name = "ChartArea1"
        Me.Chart4.ChartAreas.Add(ChartArea8)
        Me.Chart4.Dock = System.Windows.Forms.DockStyle.Fill
        Legend8.Name = "Legend1"
        Me.Chart4.Legends.Add(Legend8)
        Me.Chart4.Location = New System.Drawing.Point(0, 0)
        Me.Chart4.Name = "Chart4"
        Me.Chart4.Size = New System.Drawing.Size(347, 192)
        Me.Chart4.TabIndex = 1
        Me.Chart4.Text = "Chart4"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.VSplitContainer1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(704, 419)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.m_refreshButton)
        Me.Panel1.Controls.Add(Me.VersionTB)
        Me.Panel1.Controls.Add(Me.CurrencyTB)
        Me.Panel1.Controls.Add(Me.EntityTB)
        Me.Panel1.Controls.Add(Me.m_currencyLabel)
        Me.Panel1.Controls.Add(Me.m_versionLabel)
        Me.Panel1.Controls.Add(Me.m_entityLabel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(704, 25)
        Me.Panel1.TabIndex = 1
        '
        'VersionTB
        '
        Me.VersionTB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VersionTB.BackColor = System.Drawing.Color.White
        Me.VersionTB.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.VersionTB.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.VersionTB.DefaultText = "Empty..."
        Me.VersionTB.Enabled = False
        Me.VersionTB.Location = New System.Drawing.Point(546, 1)
        Me.VersionTB.MaxLength = 32767
        Me.VersionTB.Name = "VersionTB"
        Me.VersionTB.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.VersionTB.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.VersionTB.SelectionLength = 0
        Me.VersionTB.SelectionStart = 0
        Me.VersionTB.Size = New System.Drawing.Size(154, 23)
        Me.VersionTB.TabIndex = 12
        Me.VersionTB.Text = " "
        Me.VersionTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.VersionTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'CurrencyTB
        '
        Me.CurrencyTB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CurrencyTB.BackColor = System.Drawing.Color.White
        Me.CurrencyTB.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.CurrencyTB.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.CurrencyTB.DefaultText = "Empty..."
        Me.CurrencyTB.Enabled = False
        Me.CurrencyTB.Location = New System.Drawing.Point(417, 1)
        Me.CurrencyTB.MaxLength = 32767
        Me.CurrencyTB.Name = "CurrencyTB"
        Me.CurrencyTB.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.CurrencyTB.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.CurrencyTB.SelectionLength = 0
        Me.CurrencyTB.SelectionStart = 0
        Me.CurrencyTB.Size = New System.Drawing.Size(69, 23)
        Me.CurrencyTB.TabIndex = 11
        Me.CurrencyTB.Text = " "
        Me.CurrencyTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.CurrencyTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'EntityTB
        '
        Me.EntityTB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.EntityTB.BackColor = System.Drawing.Color.White
        Me.EntityTB.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.EntityTB.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.EntityTB.DefaultText = "Empty..."
        Me.EntityTB.Enabled = False
        Me.EntityTB.Location = New System.Drawing.Point(214, 1)
        Me.EntityTB.MaxLength = 32767
        Me.EntityTB.Name = "EntityTB"
        Me.EntityTB.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.EntityTB.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.EntityTB.SelectionLength = 0
        Me.EntityTB.SelectionStart = 0
        Me.EntityTB.Size = New System.Drawing.Size(136, 23)
        Me.EntityTB.TabIndex = 10
        Me.EntityTB.Text = " "
        Me.EntityTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.EntityTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_currencyLabel
        '
        Me.m_currencyLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_currencyLabel.AutoSize = True
        Me.m_currencyLabel.Location = New System.Drawing.Point(356, 5)
        Me.m_currencyLabel.Name = "m_currencyLabel"
        Me.m_currencyLabel.Size = New System.Drawing.Size(49, 13)
        Me.m_currencyLabel.TabIndex = 9
        Me.m_currencyLabel.Text = "Currency"
        '
        'm_versionLabel
        '
        Me.m_versionLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_versionLabel.AutoSize = True
        Me.m_versionLabel.Location = New System.Drawing.Point(492, 5)
        Me.m_versionLabel.Name = "m_versionLabel"
        Me.m_versionLabel.Size = New System.Drawing.Size(42, 13)
        Me.m_versionLabel.TabIndex = 8
        Me.m_versionLabel.Text = "Version"
        '
        'm_entityLabel
        '
        Me.m_entityLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_entityLabel.AutoSize = True
        Me.m_entityLabel.Location = New System.Drawing.Point(172, 5)
        Me.m_entityLabel.Name = "m_entityLabel"
        Me.m_entityLabel.Size = New System.Drawing.Size(33, 13)
        Me.m_entityLabel.TabIndex = 7
        Me.m_entityLabel.Text = "Entity"
        '
        'm_chartsRightClickMenu
        '
        Me.m_chartsRightClickMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.m_editChartButton, Me.m_dropChartOnExcelButton})
        Me.m_chartsRightClickMenu.Name = "m_chartsRightClickMenu"
        Me.m_chartsRightClickMenu.Size = New System.Drawing.Size(147, 48)
        '
        'm_editChartButton
        '
        Me.m_editChartButton.Image = Global.FinancialBI.My.Resources.Resources.chart_line
        Me.m_editChartButton.Name = "m_editChartButton"
        Me.m_editChartButton.Size = New System.Drawing.Size(146, 22)
        Me.m_editChartButton.Text = "Edit Chart"
        '
        'm_dropChartOnExcelButton
        '
        Me.m_dropChartOnExcelButton.Image = Global.FinancialBI.My.Resources.Resources.Excel_Blue_32x32
        Me.m_dropChartOnExcelButton.Name = "m_dropChartOnExcelButton"
        Me.m_dropChartOnExcelButton.Size = New System.Drawing.Size(146, 22)
        Me.m_dropChartOnExcelButton.Text = "Drop on Excel"
        '
        'm_refreshButton
        '
        Me.m_refreshButton.AllowAnimations = True
        Me.m_refreshButton.BackColor = System.Drawing.Color.Transparent
        Me.m_refreshButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.m_refreshButton.ImageKey = "refresh classic green.ico"
        Me.m_refreshButton.ImageList = Me.ImageList1
        Me.m_refreshButton.Location = New System.Drawing.Point(3, 0)
        Me.m_refreshButton.Name = "m_refreshButton"
        Me.m_refreshButton.RoundedCornersMask = CType(15, Byte)
        Me.m_refreshButton.Size = New System.Drawing.Size(73, 25)
        Me.m_refreshButton.TabIndex = 13
        Me.m_refreshButton.Text = "Refresh"
        Me.m_refreshButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.m_refreshButton.UseVisualStyleBackColor = False
        Me.m_refreshButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Export classic green bigger.ico")
        Me.ImageList1.Images.SetKeyName(1, "refresh classic green.ico")
        '
        'CUI2Visualization
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "CUI2Visualization"
        Me.Size = New System.Drawing.Size(704, 419)
        Me.VSplitContainer1.Panel1.ResumeLayout(False)
        Me.VSplitContainer1.Panel2.ResumeLayout(False)
        Me.VSplitContainer1.ResumeLayout(False)
        Me.VSplitContainer3.Panel1.ResumeLayout(False)
        Me.VSplitContainer3.Panel2.ResumeLayout(False)
        Me.VSplitContainer3.ResumeLayout(False)
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Chart3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.VSplitContainer2.Panel1.ResumeLayout(False)
        Me.VSplitContainer2.Panel2.ResumeLayout(False)
        Me.VSplitContainer2.ResumeLayout(False)
        CType(Me.Chart2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Chart4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.m_chartsRightClickMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents VSplitContainer1 As VIBlend.WinForms.Controls.vSplitContainer
    Friend WithEvents VSplitContainer3 As VIBlend.WinForms.Controls.vSplitContainer
    Friend WithEvents VSplitContainer2 As VIBlend.WinForms.Controls.vSplitContainer
    Friend WithEvents Chart1 As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents Chart3 As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents Chart2 As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents Chart4 As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents VersionTB As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents CurrencyTB As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents EntityTB As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents m_currencyLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_versionLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_entityLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_chartsRightClickMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents m_editChartButton As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents m_dropChartOnExcelButton As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents m_refreshButton As VIBlend.WinForms.Controls.vButton
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList

End Class
