'' FModelingUI2.vb
''
'' General FModelling Controller
''
''
''
'' Author: Julien Monnereau
'' Last modified: 27/05/2015


'Imports System.Collections.Generic



'Friend Class FModelingUI2


'#Region "Instance Variables"

'    ' Objects
'    Private InputsController As FModelingInputsController
'    Private SimulationsController As FModelingSimulationController
'    Private ExportsController As FModelingExportController
'    Private FAccountsController As FModellingAccountsController

'#End Region


'#Region "Initialize"

'    Friend Sub New()

'        ' This call is required by the designer.
'        InitializeComponent()

'        ' Add any initialization after the InitializeComponent() call.
'        SimulationsController = New FModelingSimulationController(Me)
'        InputsController = New FModelingInputsController(Me, SimulationsController.FModellingAccount)
'        ExportsController = New FModelingExportController(Me, SimulationsController.FModellingAccount, _
'                                                          InputsController.accounts_names_id_dic, _
'                                                          InputsController.CBEditor)

'        FAccountsController = New FModellingAccountsController(SimulationsController.possible_targets_name_id_dict, _
'                                                               SimulationsController.FModellingAccount)


'        InputsController.InitializeView()
'        ExportsController.InitializeView()
'        Me.WindowState = Windows.Forms.FormWindowState.Maximized

'    End Sub

'#End Region


'#Region "Menu"

'#Region "Scenario"

'    Private Sub NewScenarioBT_Click(sender As Object, e As EventArgs) Handles NewScenarioBT.Click

'        displayInputs()

'    End Sub

'    Private Sub NewTargetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewTargetToolStripMenuItem.Click

'        SimulationsController.View.AddConstraintToolStripMenuItem_Click(sender, e)

'    End Sub

'    Private Sub RefreshScenarioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshScenarioToolStripMenuItem.Click

'        SimulationsController.ComputeScenario()

'    End Sub

'#End Region

'#Region "Mappings"

'    Private Sub InputsMappingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InputsMappingToolStripMenuItem.Click

'        ' put a generic control instead !
'        '
'        Dim genericUI As New GenericView("Inputs Mapping")
'        genericUI.Controls.Add(InputsController.MappingDGV)
'        InputsController.MappingDGV.Dock = Windows.Forms.DockStyle.Fill
'        genericUI.Show()

'    End Sub

'    Private Sub FinancialAccountsMappingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FinancialAccountsMappingToolStripMenuItem.Click

'        ' Display must be in this form !
'        ' view = user control
'        FAccountsController.DisplayView()

'    End Sub

'    ' outputs mappings

'#End Region

'#Region "View"

'    Private Sub VersionAndScopeSelectionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VersionAndScopeSelectionToolStripMenuItem.Click

'        displayInputs()

'    End Sub

'    Private Sub SimutlationsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SimutlationsToolStripMenuItem.Click

'        displaySimulation()

'    End Sub

'    Private Sub OutputToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OutputToolStripMenuItem.Click

'        displayOutput()

'    End Sub


'#End Region

'    '   InputsController.DisplayInputsDGV() -> in case we want to separate the conso inputs DGV

'#End Region


'#Region "Controllers Interface"

'    Friend Sub setVersionAndPeriods(ByRef input_period_list As List(Of Int32), _
'                                    ByRef version_id As String, _
'                                    ByRef version_name As String)

'        SimulationsController.periods_list = input_period_list.ToArray
'        SimulationsController.version_id = version_id
'        ExportsController.UpdatePeriodList(input_period_list.ToArray)
'        SimulationsController.setCartoucheValues(version_name, My.Settings.currentCurrency)

'    End Sub

'    Friend Sub addConstraint(ByRef f_account_name As String, _
'                             ByRef default_value As Double)

'        SimulationsController.AddConstraint(f_account_name, default_value)

'    End Sub

'    Friend Sub sendInputsToSimulationController(ByRef inputsDGV As VIBlend.WinForms.DataGridView.vDataGridView)

'        SimulationsController.InitializeInputs(inputsDGV)
'        SimulationsController.NewScenario()

'    End Sub

'#End Region


'#Region "Utilities"

'    Friend Function getVersion_id() As String

'        Return SimulationsController.version_id

'    End Function

'    Friend Function getDataDictionary() As Dictionary(Of String, Double())

'        Return SimulationsController.scenario.data_dic

'    End Function

'#End Region


'#Region "Display"

'    Friend Sub displayInputs()

'        On Error Resume Next
'        TableLayoutPanel1.Controls.RemoveAt(1)
'        TableLayoutPanel1.Controls.Add(InputsController.View, 0, 1)
'        InputsController.View.Dock = Windows.Forms.DockStyle.Fill

'    End Sub

'    Friend Sub displaySimulation()

'        On Error Resume Next
'        TableLayoutPanel1.Controls.RemoveAt(1)
'        TableLayoutPanel1.Controls.Add(SimulationsController.View, 0, 1)
'        SimulationsController.View.Dock = Windows.Forms.DockStyle.Fill

'    End Sub

'    Friend Sub displayOutput()

'        On Error Resume Next
'        TableLayoutPanel1.Controls.RemoveAt(1)
'        TableLayoutPanel1.Controls.Add(ExportsController.View, 0, 1)
'        ExportsController.View.Dock = Windows.Forms.DockStyle.Fill

'    End Sub

'#End Region


'End Class