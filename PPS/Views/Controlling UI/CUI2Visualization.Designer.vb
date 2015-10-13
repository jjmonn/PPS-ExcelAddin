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
        Dim ChartArea5 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend5 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim ChartArea6 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend6 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim ChartArea7 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend7 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim ChartArea8 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend8 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
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
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.m_chartsRightClickMenu = New VIBlend.WinForms.Controls.vContextMenu()
        Me.m_addSerieButton = New System.Windows.Forms.MenuItem()
        Me.m_removeSerieButton = New System.Windows.Forms.MenuItem()
        Me.m_editSerieButton = New System.Windows.Forms.MenuItem()
        Me.m_editChartButton = New System.Windows.Forms.MenuItem()
        Me.m_exportOnExcel = New System.Windows.Forms.MenuItem()
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
        Me.VSplitContainer2.Panel1.Size = New System.Drawing.Size(347, 190)
        Me.VSplitContainer2.Panel1.TabIndex = 1
        '
        'VSplitContainer2.Panel2
        '
        Me.VSplitContainer2.Panel2.BackColor = System.Drawing.Color.White
        Me.VSplitContainer2.Panel2.BorderColor = System.Drawing.Color.Silver
        Me.VSplitContainer2.Panel2.Controls.Add(Me.Chart4)
        Me.VSplitContainer2.Panel2.Location = New System.Drawing.Point(0, 195)
        Me.VSplitContainer2.Panel2.Name = "Panel2"
        Me.VSplitContainer2.Panel2.Size = New System.Drawing.Size(347, 193)
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
        Me.Chart2.Size = New System.Drawing.Size(347, 190)
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
        Me.Chart4.Size = New System.Drawing.Size(347, 193)
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
        Me.Panel1.Controls.Add(Me.VersionTB)
        Me.Panel1.Controls.Add(Me.CurrencyTB)
        Me.Panel1.Controls.Add(Me.EntityTB)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
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
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(356, 5)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 15)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Currency"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(492, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 15)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Version"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(172, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 15)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Entity"
        '
        'm_chartsRightClickMenu
        '
        Me.m_chartsRightClickMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.m_addSerieButton, Me.m_removeSerieButton, Me.m_editSerieButton, Me.m_editChartButton, Me.m_exportOnExcel})
        '
        'm_addSerieButton
        '
        Me.m_addSerieButton.Index = 0
        Me.m_addSerieButton.Text = "Add Serie"
        '
        'm_removeSerieButton
        '
        Me.m_removeSerieButton.Index = 1
        Me.m_removeSerieButton.Text = "Remove Serie"
        '
        'm_editSerieButton
        '
        Me.m_editSerieButton.Index = 2
        Me.m_editSerieButton.Text = "Edit Serie"
        '
        'm_editChartButton
        '
        Me.m_editChartButton.Index = 3
        Me.m_editChartButton.Text = "Edit Chart"
        '
        'm_exportOnExcel
        '
        Me.m_exportOnExcel.Index = 4
        Me.m_exportOnExcel.Text = "Export on Excel"
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
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents m_chartsRightClickMenu As VIBlend.WinForms.Controls.vContextMenu
    Friend WithEvents m_removeSerieButton As System.Windows.Forms.MenuItem
    Friend WithEvents m_addSerieButton As System.Windows.Forms.MenuItem
    Friend WithEvents m_editSerieButton As System.Windows.Forms.MenuItem
    Friend WithEvents m_editChartButton As System.Windows.Forms.MenuItem
    Friend WithEvents m_exportOnExcel As System.Windows.Forms.MenuItem

End Class
