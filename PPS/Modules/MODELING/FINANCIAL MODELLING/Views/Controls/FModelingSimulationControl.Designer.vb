<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FModelingSimulationControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FModelingSimulationControl))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.rightnavBT = New System.Windows.Forms.Button()
        Me.ButtonsIL = New System.Windows.Forms.ImageList(Me.components)
        Me.leftnavBT = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.simulationResultTB = New VIBlend.WinForms.Controls.vTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CurrencyTB = New VIBlend.WinForms.Controls.vTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.InputsVersionTB = New VIBlend.WinForms.Controls.vTextBox()
        Me.refreshBT = New System.Windows.Forms.Button()
        Me.DGVsSplitContainer = New System.Windows.Forms.SplitContainer()
        Me.inputsDGVsRightClickMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddConstraintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteConstraintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshScenarioToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.SerieMGTRCMBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.CopyValueRightBT = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.DGVsSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DGVsSplitContainer.SuspendLayout()
        Me.inputsDGVsRightClickMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TableLayoutPanel1)
        Me.SplitContainer1.Size = New System.Drawing.Size(862, 501)
        Me.SplitContainer1.SplitterDistance = 348
        Me.SplitContainer1.TabIndex = 0
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DGVsSplitContainer, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 335.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(862, 348)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.rightnavBT)
        Me.Panel1.Controls.Add(Me.leftnavBT)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.simulationResultTB)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.CurrencyTB)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.InputsVersionTB)
        Me.Panel1.Controls.Add(Me.refreshBT)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(1, 1)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(860, 45)
        Me.Panel1.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button1.Location = New System.Drawing.Point(79, 5)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(35, 35)
        Me.Button1.TabIndex = 9
        Me.Button1.UseVisualStyleBackColor = True
        '
        'rightnavBT
        '
        Me.rightnavBT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.rightnavBT.ImageKey = "nav_right.ico"
        Me.rightnavBT.ImageList = Me.ButtonsIL
        Me.rightnavBT.Location = New System.Drawing.Point(42, 5)
        Me.rightnavBT.Name = "rightnavBT"
        Me.rightnavBT.Size = New System.Drawing.Size(35, 35)
        Me.rightnavBT.TabIndex = 8
        Me.rightnavBT.UseVisualStyleBackColor = True
        '
        'ButtonsIL
        '
        Me.ButtonsIL.ImageStream = CType(resources.GetObject("ButtonsIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonsIL.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonsIL.Images.SetKeyName(0, "nav_left.ico")
        Me.ButtonsIL.Images.SetKeyName(1, "nav_right.ico")
        '
        'leftnavBT
        '
        Me.leftnavBT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.leftnavBT.ImageKey = "nav_left.ico"
        Me.leftnavBT.ImageList = Me.ButtonsIL
        Me.leftnavBT.Location = New System.Drawing.Point(6, 5)
        Me.leftnavBT.Name = "leftnavBT"
        Me.leftnavBT.Size = New System.Drawing.Size(35, 35)
        Me.leftnavBT.TabIndex = 7
        Me.leftnavBT.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(235, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Simulation Status"
        '
        'simulationResultTB
        '
        Me.simulationResultTB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.simulationResultTB.BackColor = System.Drawing.Color.White
        Me.simulationResultTB.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.simulationResultTB.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.simulationResultTB.DefaultText = "Empty..."
        Me.simulationResultTB.Enabled = False
        Me.simulationResultTB.Location = New System.Drawing.Point(327, 5)
        Me.simulationResultTB.MaxLength = 32767
        Me.simulationResultTB.Name = "simulationResultTB"
        Me.simulationResultTB.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.simulationResultTB.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.simulationResultTB.SelectionLength = 0
        Me.simulationResultTB.SelectionStart = 0
        Me.simulationResultTB.Size = New System.Drawing.Size(108, 23)
        Me.simulationResultTB.TabIndex = 5
        Me.simulationResultTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.simulationResultTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010BLUE
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(452, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Currency"
        '
        'CurrencyTB
        '
        Me.CurrencyTB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CurrencyTB.BackColor = System.Drawing.Color.White
        Me.CurrencyTB.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.CurrencyTB.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.CurrencyTB.DefaultText = "Empty..."
        Me.CurrencyTB.Enabled = False
        Me.CurrencyTB.Location = New System.Drawing.Point(505, 5)
        Me.CurrencyTB.MaxLength = 32767
        Me.CurrencyTB.Name = "CurrencyTB"
        Me.CurrencyTB.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.CurrencyTB.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.CurrencyTB.SelectionLength = 0
        Me.CurrencyTB.SelectionStart = 0
        Me.CurrencyTB.Size = New System.Drawing.Size(84, 23)
        Me.CurrencyTB.TabIndex = 3
        Me.CurrencyTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.CurrencyTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010BLUE
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(602, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Inputs Version"
        '
        'InputsVersionTB
        '
        Me.InputsVersionTB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.InputsVersionTB.BackColor = System.Drawing.Color.White
        Me.InputsVersionTB.BoundsOffset = New System.Drawing.Size(1, 1)
        Me.InputsVersionTB.ControlBorderColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.InputsVersionTB.DefaultText = "Empty..."
        Me.InputsVersionTB.Enabled = False
        Me.InputsVersionTB.Location = New System.Drawing.Point(680, 5)
        Me.InputsVersionTB.MaxLength = 32767
        Me.InputsVersionTB.Name = "InputsVersionTB"
        Me.InputsVersionTB.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.InputsVersionTB.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.InputsVersionTB.SelectionLength = 0
        Me.InputsVersionTB.SelectionStart = 0
        Me.InputsVersionTB.Size = New System.Drawing.Size(177, 23)
        Me.InputsVersionTB.TabIndex = 1
        Me.InputsVersionTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.InputsVersionTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010BLUE
        '
        'refreshBT
        '
        Me.refreshBT.BackgroundImage = Global.PPS.My.Resources.Resources.Refresh2
        Me.refreshBT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.refreshBT.Location = New System.Drawing.Point(116, 5)
        Me.refreshBT.Name = "refreshBT"
        Me.refreshBT.Size = New System.Drawing.Size(35, 35)
        Me.refreshBT.TabIndex = 0
        Me.refreshBT.UseVisualStyleBackColor = True
        '
        'DGVsSplitContainer
        '
        Me.DGVsSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVsSplitContainer.Location = New System.Drawing.Point(4, 50)
        Me.DGVsSplitContainer.Name = "DGVsSplitContainer"
        Me.DGVsSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.DGVsSplitContainer.Size = New System.Drawing.Size(854, 329)
        Me.DGVsSplitContainer.SplitterDistance = 82
        Me.DGVsSplitContainer.TabIndex = 1
        '
        'inputsDGVsRightClickMenu
        '
        Me.inputsDGVsRightClickMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddConstraintToolStripMenuItem, Me.DeleteConstraintToolStripMenuItem, Me.RefreshScenarioToolStripMenuItem, Me.ToolStripSeparator4, Me.SerieMGTRCMBT, Me.ToolStripSeparator5, Me.CopyValueRightBT})
        Me.inputsDGVsRightClickMenu.Name = "inputsDGVsRightClickMenu"
        Me.inputsDGVsRightClickMenu.Size = New System.Drawing.Size(166, 126)
        '
        'AddConstraintToolStripMenuItem
        '
        Me.AddConstraintToolStripMenuItem.Name = "AddConstraintToolStripMenuItem"
        Me.AddConstraintToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.AddConstraintToolStripMenuItem.Text = "Add Target"
        '
        'DeleteConstraintToolStripMenuItem
        '
        Me.DeleteConstraintToolStripMenuItem.Name = "DeleteConstraintToolStripMenuItem"
        Me.DeleteConstraintToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.DeleteConstraintToolStripMenuItem.Text = "Delete Target"
        '
        'RefreshScenarioToolStripMenuItem
        '
        Me.RefreshScenarioToolStripMenuItem.Name = "RefreshScenarioToolStripMenuItem"
        Me.RefreshScenarioToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.RefreshScenarioToolStripMenuItem.Text = "Refresh Scenario"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(162, 6)
        '
        'SerieMGTRCMBT
        '
        Me.SerieMGTRCMBT.Name = "SerieMGTRCMBT"
        Me.SerieMGTRCMBT.Size = New System.Drawing.Size(165, 22)
        Me.SerieMGTRCMBT.Text = "Display Options"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(162, 6)
        '
        'CopyValueRightBT
        '
        Me.CopyValueRightBT.Name = "CopyValueRightBT"
        Me.CopyValueRightBT.Size = New System.Drawing.Size(165, 22)
        Me.CopyValueRightBT.Text = "Copy Value Right"
        '
        'FModelingSimulationControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FModelingSimulationControl"
        Me.Size = New System.Drawing.Size(862, 501)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DGVsSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DGVsSplitContainer.ResumeLayout(False)
        Me.inputsDGVsRightClickMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents refreshBT As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents InputsVersionTB As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents simulationResultTB As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CurrencyTB As VIBlend.WinForms.Controls.vTextBox
    Friend WithEvents inputsDGVsRightClickMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AddConstraintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteConstraintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshScenarioToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SerieMGTRCMBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CopyValueRightBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents DGVsSplitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents rightnavBT As System.Windows.Forms.Button
    Friend WithEvents leftnavBT As System.Windows.Forms.Button
    Friend WithEvents ButtonsIL As System.Windows.Forms.ImageList
    Friend WithEvents Button1 As System.Windows.Forms.Button

End Class
