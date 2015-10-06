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
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim ChartArea3 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend3 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim ChartArea4 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend4 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Me.VSplitContainer1 = New VIBlend.WinForms.Controls.vSplitContainer()
        Me.VSplitContainer3 = New VIBlend.WinForms.Controls.vSplitContainer()
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Chart3 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.VSplitContainer2 = New VIBlend.WinForms.Controls.vSplitContainer()
        Me.Chart2 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Chart4 = New System.Windows.Forms.DataVisualization.Charting.Chart()
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
        Me.SuspendLayout()
        '
        'VSplitContainer1
        '
        Me.VSplitContainer1.AllowAnimations = True
        Me.VSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VSplitContainer1.Location = New System.Drawing.Point(0, 0)
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
        Me.VSplitContainer1.Panel1.Size = New System.Drawing.Size(348, 419)
        Me.VSplitContainer1.Panel1.TabIndex = 1
        '
        'VSplitContainer1.Panel2
        '
        Me.VSplitContainer1.Panel2.BackColor = System.Drawing.Color.White
        Me.VSplitContainer1.Panel2.BorderColor = System.Drawing.Color.Silver
        Me.VSplitContainer1.Panel2.Controls.Add(Me.VSplitContainer2)
        Me.VSplitContainer1.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VSplitContainer1.Panel2.Location = New System.Drawing.Point(355, 0)
        Me.VSplitContainer1.Panel2.Name = "Panel2"
        Me.VSplitContainer1.Panel2.Size = New System.Drawing.Size(349, 419)
        Me.VSplitContainer1.Panel2.TabIndex = 2
        Me.VSplitContainer1.Size = New System.Drawing.Size(704, 419)
        Me.VSplitContainer1.StyleKey = "Splitter"
        Me.VSplitContainer1.TabIndex = 0
        Me.VSplitContainer1.Text = "VSplitContainer1"
        Me.VSplitContainer1.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
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
        Me.VSplitContainer3.Panel1.Size = New System.Drawing.Size(348, 206)
        Me.VSplitContainer3.Panel1.TabIndex = 1
        '
        'VSplitContainer3.Panel2
        '
        Me.VSplitContainer3.Panel2.BackColor = System.Drawing.Color.White
        Me.VSplitContainer3.Panel2.BorderColor = System.Drawing.Color.Silver
        Me.VSplitContainer3.Panel2.Controls.Add(Me.Chart3)
        Me.VSplitContainer3.Panel2.Location = New System.Drawing.Point(0, 213)
        Me.VSplitContainer3.Panel2.Name = "Panel2"
        Me.VSplitContainer3.Panel2.Size = New System.Drawing.Size(348, 206)
        Me.VSplitContainer3.Panel2.TabIndex = 2
        Me.VSplitContainer3.Size = New System.Drawing.Size(348, 419)
        Me.VSplitContainer3.StyleKey = "Splitter"
        Me.VSplitContainer3.TabIndex = 0
        Me.VSplitContainer3.Text = "VSplitContainer3"
        Me.VSplitContainer3.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'Chart1
        '
        ChartArea1.Name = "ChartArea1"
        Me.Chart1.ChartAreas.Add(ChartArea1)
        Me.Chart1.Dock = System.Windows.Forms.DockStyle.Fill
        Legend1.Name = "Legend1"
        Me.Chart1.Legends.Add(Legend1)
        Me.Chart1.Location = New System.Drawing.Point(0, 0)
        Me.Chart1.Name = "Chart1"
        Me.Chart1.Size = New System.Drawing.Size(348, 206)
        Me.Chart1.TabIndex = 0
        Me.Chart1.Text = "Chart1"
        '
        'Chart3
        '
        ChartArea2.Name = "ChartArea1"
        Me.Chart3.ChartAreas.Add(ChartArea2)
        Me.Chart3.Dock = System.Windows.Forms.DockStyle.Fill
        Legend2.Name = "Legend1"
        Me.Chart3.Legends.Add(Legend2)
        Me.Chart3.Location = New System.Drawing.Point(0, 0)
        Me.Chart3.Name = "Chart3"
        Me.Chart3.Size = New System.Drawing.Size(348, 206)
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
        Me.VSplitContainer2.Panel1.Size = New System.Drawing.Size(349, 206)
        Me.VSplitContainer2.Panel1.TabIndex = 1
        '
        'VSplitContainer2.Panel2
        '
        Me.VSplitContainer2.Panel2.BackColor = System.Drawing.Color.White
        Me.VSplitContainer2.Panel2.BorderColor = System.Drawing.Color.Silver
        Me.VSplitContainer2.Panel2.Controls.Add(Me.Chart4)
        Me.VSplitContainer2.Panel2.Location = New System.Drawing.Point(0, 213)
        Me.VSplitContainer2.Panel2.Name = "Panel2"
        Me.VSplitContainer2.Panel2.Size = New System.Drawing.Size(349, 206)
        Me.VSplitContainer2.Panel2.TabIndex = 2
        Me.VSplitContainer2.Size = New System.Drawing.Size(349, 419)
        Me.VSplitContainer2.StyleKey = "Splitter"
        Me.VSplitContainer2.TabIndex = 0
        Me.VSplitContainer2.Text = "VSplitContainer2"
        Me.VSplitContainer2.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'Chart2
        '
        ChartArea3.Name = "ChartArea1"
        Me.Chart2.ChartAreas.Add(ChartArea3)
        Me.Chart2.Dock = System.Windows.Forms.DockStyle.Fill
        Legend3.Name = "Legend1"
        Me.Chart2.Legends.Add(Legend3)
        Me.Chart2.Location = New System.Drawing.Point(0, 0)
        Me.Chart2.Name = "Chart2"
        Me.Chart2.Size = New System.Drawing.Size(349, 206)
        Me.Chart2.TabIndex = 1
        Me.Chart2.Text = "Chart2"
        '
        'Chart4
        '
        ChartArea4.Name = "ChartArea1"
        Me.Chart4.ChartAreas.Add(ChartArea4)
        Me.Chart4.Dock = System.Windows.Forms.DockStyle.Fill
        Legend4.Name = "Legend1"
        Me.Chart4.Legends.Add(Legend4)
        Me.Chart4.Location = New System.Drawing.Point(0, 0)
        Me.Chart4.Name = "Chart4"
        Me.Chart4.Size = New System.Drawing.Size(349, 206)
        Me.Chart4.TabIndex = 1
        Me.Chart4.Text = "Chart4"
        '
        'CUI2Visualization
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.VSplitContainer1)
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
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents VSplitContainer1 As VIBlend.WinForms.Controls.vSplitContainer
    Friend WithEvents VSplitContainer3 As VIBlend.WinForms.Controls.vSplitContainer
    Friend WithEvents VSplitContainer2 As VIBlend.WinForms.Controls.vSplitContainer
    Friend WithEvents Chart1 As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents Chart3 As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents Chart2 As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents Chart4 As System.Windows.Forms.DataVisualization.Charting.Chart

End Class
