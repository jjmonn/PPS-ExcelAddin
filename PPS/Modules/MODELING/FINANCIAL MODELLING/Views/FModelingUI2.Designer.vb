<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FModelingUI2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FModelingUI2))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ScenarioToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewScenarioBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshScenarioToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewTargetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteTargetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportSimulationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AssumptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FinancialAccountsMappingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportMappingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InputsMappingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VersionAndScopeSelectionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SimutlationsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OutputToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.MenuStrip1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ScenarioToolStripMenuItem, Me.AssumptionsToolStripMenuItem, Me.ViewToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(221, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ScenarioToolStripMenuItem
        '
        Me.ScenarioToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewScenarioBT, Me.RefreshScenarioToolStripMenuItem, Me.NewTargetToolStripMenuItem, Me.DeleteTargetToolStripMenuItem, Me.ExportSimulationToolStripMenuItem})
        Me.ScenarioToolStripMenuItem.Name = "ScenarioToolStripMenuItem"
        Me.ScenarioToolStripMenuItem.Size = New System.Drawing.Size(76, 20)
        Me.ScenarioToolStripMenuItem.Text = "Simulation"
        '
        'NewScenarioBT
        '
        Me.NewScenarioBT.Name = "NewScenarioBT"
        Me.NewScenarioBT.Size = New System.Drawing.Size(167, 22)
        Me.NewScenarioBT.Text = "New Simulation"
        '
        'RefreshScenarioToolStripMenuItem
        '
        Me.RefreshScenarioToolStripMenuItem.Name = "RefreshScenarioToolStripMenuItem"
        Me.RefreshScenarioToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.RefreshScenarioToolStripMenuItem.Text = "Refresh"
        '
        'NewTargetToolStripMenuItem
        '
        Me.NewTargetToolStripMenuItem.Name = "NewTargetToolStripMenuItem"
        Me.NewTargetToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.NewTargetToolStripMenuItem.Text = "New Target"
        '
        'DeleteTargetToolStripMenuItem
        '
        Me.DeleteTargetToolStripMenuItem.Name = "DeleteTargetToolStripMenuItem"
        Me.DeleteTargetToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.DeleteTargetToolStripMenuItem.Text = "Delete Target"
        '
        'ExportSimulationToolStripMenuItem
        '
        Me.ExportSimulationToolStripMenuItem.Name = "ExportSimulationToolStripMenuItem"
        Me.ExportSimulationToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.ExportSimulationToolStripMenuItem.Text = "Export Simulation"
        '
        'AssumptionsToolStripMenuItem
        '
        Me.AssumptionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FinancialAccountsMappingToolStripMenuItem, Me.ExportMappingToolStripMenuItem, Me.InputsMappingToolStripMenuItem})
        Me.AssumptionsToolStripMenuItem.Name = "AssumptionsToolStripMenuItem"
        Me.AssumptionsToolStripMenuItem.Size = New System.Drawing.Size(93, 20)
        Me.AssumptionsToolStripMenuItem.Text = "Configuration"
        '
        'FinancialAccountsMappingToolStripMenuItem
        '
        Me.FinancialAccountsMappingToolStripMenuItem.Name = "FinancialAccountsMappingToolStripMenuItem"
        Me.FinancialAccountsMappingToolStripMenuItem.Size = New System.Drawing.Size(251, 22)
        Me.FinancialAccountsMappingToolStripMenuItem.Text = "Financial Accounts Configuration"
        '
        'ExportMappingToolStripMenuItem
        '
        Me.ExportMappingToolStripMenuItem.Name = "ExportMappingToolStripMenuItem"
        Me.ExportMappingToolStripMenuItem.Size = New System.Drawing.Size(251, 22)
        Me.ExportMappingToolStripMenuItem.Text = "Export Mapping"
        '
        'InputsMappingToolStripMenuItem
        '
        Me.InputsMappingToolStripMenuItem.Name = "InputsMappingToolStripMenuItem"
        Me.InputsMappingToolStripMenuItem.Size = New System.Drawing.Size(251, 22)
        Me.InputsMappingToolStripMenuItem.Text = "Inputs Mapping"
        '
        'ViewToolStripMenuItem
        '
        Me.ViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.VersionAndScopeSelectionToolStripMenuItem, Me.SimutlationsToolStripMenuItem, Me.OutputToolStripMenuItem})
        Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        Me.ViewToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.ViewToolStripMenuItem.Text = "View"
        '
        'VersionAndScopeSelectionToolStripMenuItem
        '
        Me.VersionAndScopeSelectionToolStripMenuItem.Name = "VersionAndScopeSelectionToolStripMenuItem"
        Me.VersionAndScopeSelectionToolStripMenuItem.Size = New System.Drawing.Size(222, 22)
        Me.VersionAndScopeSelectionToolStripMenuItem.Text = "Version and Scope Selection"
        '
        'SimutlationsToolStripMenuItem
        '
        Me.SimutlationsToolStripMenuItem.Name = "SimutlationsToolStripMenuItem"
        Me.SimutlationsToolStripMenuItem.Size = New System.Drawing.Size(222, 22)
        Me.SimutlationsToolStripMenuItem.Text = "Simulations"
        '
        'OutputToolStripMenuItem
        '
        Me.OutputToolStripMenuItem.Name = "OutputToolStripMenuItem"
        Me.OutputToolStripMenuItem.Size = New System.Drawing.Size(222, 22)
        Me.OutputToolStripMenuItem.Text = "Output"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.MenuStrip1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(591, 441)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'FModelingUI2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(591, 441)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "FModelingUI2"
        Me.Text = "Financial Modeling"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ScenarioToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AssumptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents FinancialAccountsMappingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExportMappingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VersionAndScopeSelectionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SimutlationsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OutputToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshScenarioToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewTargetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteTargetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExportSimulationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InputsMappingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewScenarioBT As System.Windows.Forms.ToolStripMenuItem
End Class
