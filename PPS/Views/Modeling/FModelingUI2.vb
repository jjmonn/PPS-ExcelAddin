' FModelingUI2.vb
'
' General FModelling Controller
'
'
'
' Author: Julien Monnereau
' Last modified: 27/05/2015


Imports System.Collections.Generic



Friend Class FModelingUI2


#Region "Instance Variables"

    ' Objects
    Private m_inputsController As FModelingInputsController
    Private m_simulationsController As FModelingSimulationController
    Private m_exportsController As FModelingExportController
    Private m_FAccountsController As FModellingAccountsController

#End Region


#Region "Initialize"

    Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_simulationsController = New FModelingSimulationController(Me)
        m_inputsController = New FModelingInputsController(Me, m_simulationsController.FModellingAccount)
        m_exportsController = New FModelingExportController(Me, m_simulationsController.FModellingAccount, _
                                                          m_inputsController.CBEditor)

        m_FAccountsController = New FModellingAccountsController(m_simulationsController.possible_targets_name_id_dict, _
                                                               m_simulationsController.FModellingAccount)


        m_inputsController.InitializeView()
        m_exportsController.InitializeView()
        Me.WindowState = Windows.Forms.FormWindowState.Maximized

    End Sub

#End Region


#Region "Menu"

#Region "Scenario"

    Private Sub NewScenarioBT_Click(sender As Object, e As EventArgs) Handles NewScenarioBT.Click

        displayInputs()

    End Sub

    Private Sub NewTargetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewTargetToolStripMenuItem.Click

        m_simulationsController.View.AddConstraintToolStripMenuItem_Click(sender, e)

    End Sub

    Private Sub RefreshScenarioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshScenarioToolStripMenuItem.Click

        m_simulationsController.ComputeScenario()

    End Sub

#End Region

#Region "Mappings"

    Private Sub InputsMappingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InputsMappingToolStripMenuItem.Click

        ' put a generic control instead !
        '
        Dim genericUI As New GenericView("Inputs Mapping")
        genericUI.Controls.Add(m_inputsController.m_mappingDGV)
        m_inputsController.m_mappingDGV.Dock = Windows.Forms.DockStyle.Fill
        genericUI.Show()

    End Sub

    Private Sub FinancialAccountsMappingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FinancialAccountsMappingToolStripMenuItem.Click

        ' Display must be in this form !
        ' view = user control
        m_FAccountsController.DisplayView()

    End Sub

    ' outputs mappings

#End Region

#Region "View"

    Private Sub VersionAndScopeSelectionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VersionAndScopeSelectionToolStripMenuItem.Click

        displayInputs()

    End Sub

    Private Sub SimutlationsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SimutlationsToolStripMenuItem.Click

        displaySimulation()

    End Sub

    Private Sub OutputToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OutputToolStripMenuItem.Click

        displayOutput()

    End Sub


#End Region

    '   InputsController.DisplayInputsDGV() -> in case we want to separate the conso inputs DGV

#End Region


#Region "Controllers Interface"

    Friend Sub setVersionAndPeriods(ByRef input_period_list As List(Of Int32), _
                                    ByRef version_id As String, _
                                    ByRef version_name As String)

        m_simulationsController.periods_list = input_period_list.ToArray
        m_simulationsController.version_id = version_id
        m_exportsController.UpdatePeriodList(input_period_list.ToArray)
        m_simulationsController.setCartoucheValues(version_name, My.Settings.currentCurrency)

    End Sub

    Friend Sub addConstraint(ByRef f_account_name As String, _
                             ByRef default_value As Double)

        m_simulationsController.AddConstraint(f_account_name, default_value)

    End Sub

    Friend Sub sendInputsToSimulationController(ByRef inputsDGV As VIBlend.WinForms.DataGridView.vDataGridView)

        m_simulationsController.InitializeInputs(inputsDGV)
        m_simulationsController.NewScenario()

    End Sub

#End Region


#Region "Utilities"

    Friend Function getVersion_id() As String

        Return m_simulationsController.version_id

    End Function

    Friend Function getDataDictionary() As Dictionary(Of String, Double())

        Return m_simulationsController.scenario.data_dic

    End Function

#End Region


#Region "Display"

    Friend Sub displayInputs()

        On Error Resume Next
        TableLayoutPanel1.Controls.RemoveAt(1)
        TableLayoutPanel1.Controls.Add(m_inputsController.View, 0, 1)
        m_inputsController.View.Dock = Windows.Forms.DockStyle.Fill

    End Sub

    Friend Sub displaySimulation()

        On Error Resume Next
        TableLayoutPanel1.Controls.RemoveAt(1)
        TableLayoutPanel1.Controls.Add(m_simulationsController.View, 0, 1)
        m_simulationsController.View.Dock = Windows.Forms.DockStyle.Fill

    End Sub

    Friend Sub displayOutput()

        On Error Resume Next
        TableLayoutPanel1.Controls.RemoveAt(1)
        TableLayoutPanel1.Controls.Add(m_exportsController.m_exportView, 0, 1)
        m_exportsController.m_exportView.Dock = Windows.Forms.DockStyle.Fill

    End Sub

#End Region


End Class