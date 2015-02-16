' FModellingSimulationsControler.vb
'
'
' 
' To do:
'       - Link the automated addition of the three constraints to the DB
'       - Formatting considerations regarding percentages
'
'
'
' Author: Julien Monnereau
' Last modified: 28/12/2014


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections
Imports VIBlend.WinForms.DataGridView
Imports System.Windows.Forms.DataVisualization.Charting



Friend Class FModellingSimulationsControler


#Region "Instance Variables"

    ' Objects
    Private Model As New FDLL_Interface
    Private View As FModellingUI
    Private InputsController As FModellingInputsController
    Private ExportsController As FModellingExportController
    Private FModellingAccount As New FModellingAccount

    ' Variables
    Private scenariosTV As New TreeView
    Private scenarios_dic As New Dictionary(Of String, Scenario)
    Private periods_list As Int32()
    Protected Friend version_id As String
    Private inputs_list As New List(Of String)
    Private inputs_periods As New List(Of Int32)
    Private inputs_values As New List(Of Double)
    Private constraints_list As New List(Of String)
    Private constraints_periods As New List(Of Int32)
    Private constraints_targets As New List(Of Double)
    Private Outputs_name_id_dic As Hashtable
    Private FModelling_name_id_dic As Hashtable
    Private FModelling_accounts_all_id_list As List(Of String)
    Private inputs_loaded_flag As Boolean

    ' Const
    Private Const SCENARII_TOKENS_SIZE As Int32 = 3


#End Region


#Region "Initialize"

    Protected Friend Sub New()

        Outputs_name_id_dic = FModellingAccountsMapping.GetFModellingAccountsDict(FINANCIAL_MODELLING_NAME_VARIABLE, _
                                                                                  FINANCIAL_MODELLING_ID_VARIABLE, FINANCIAL_MODELLING_OUTPUT_TYPE)

        FModelling_name_id_dic = FModellingAccountsMapping.GetFModellingAccountsDict(FINANCIAL_MODELLING_NAME_VARIABLE, _
                                                                                     FINANCIAL_MODELLING_ID_VARIABLE, FINANCIAL_MODELLING_OUTPUT_TYPE)

        FModelling_accounts_all_id_list = FModellingAccountsMapping.GetFModellingAccountsList(FINANCIAL_MODELLING_ID_VARIABLE)
        FModelling_name_id_dic.Add("Interest Rate (%)", "icod")
        FModelling_name_id_dic.Add("Incremental Tax Rate (%)", "itrate")
        FModelling_name_id_dic.Add("Cash Capitalization (%)", "iccap")

        InputsController = New FModellingInputsController(Me, FModellingAccount)
        ExportsController = New FModellingExportController(Me, FModellingAccount, InputsController.CBEditor, InputsController.accounts_id_names_dic, InputsController.accounts_names_id_dic)
        View = New FModellingUI(Me, InputsController, ExportsController, scenariosTV)
        InputsController.InitializeView(View)
        ExportsController.InitializeView(View)
        View.Show()

    End Sub

#End Region


#Region "Inputs Interface"

    Protected Friend Sub SetPeriodList(ByRef input_period_list As List(Of Int32))

        periods_list = input_period_list.ToArray
        ExportsController.UpdatePeriodList(periods_list)

    End Sub

    Protected Friend Sub InitializeInputs()

        LoadInputs()
        View.TabControl1.SelectedTab = View.TabControl1.TabPages(1)

    End Sub

    Private Sub LoadInputs()

        inputs_list.Clear()
        inputs_periods.Clear()
        inputs_values.Clear()

        'Dim nb_inputs = 6
        'Dim inputs_names As String() = {"orn",
        '                                "odiv",
        '                                "ordebt",
        '                                "oequ",
        '                                "ocapex",
        '                                "ocash"}

        'Dim inputs_array = _
        '   {{1000, 1050, 1100, 1200, 1200, 1200, 1200, 1200, 1200},
        '    {0, 0, 0, 0, 0, 0, 0, 0, 0},
        '    {100, 101, 102, 103, 104, 104, 104, 104, 104},
        '    {1000, 2050, 3150, 4350, 5550, 5550, 5550, 5550, 5550},
        '    {0, 0, 0, 0, 0, 0, 0, 0, 0},
        '    {800, 800, 800, 800, 800, 800, 800, 800, 800}}

        'For i = 0 To nb_inputs - 1
        '    Dim account = inputs_names(i)
        '    For j = 0 To periods_list.Length - 1
        '        inputs_list.Add(account)
        '        inputs_periods.Add(j)
        '        inputs_values.Add(inputs_array(i, j))
        '    Next
        'Next

        For Each row In InputsController.InputsDGV.RowsHierarchy.Items
            Dim fmodelling_account_id = row.Caption
            For j As Int32 = 1 To InputsController.InputsDGV.ColumnsHierarchy.Items.Count - 1
                Dim column = InputsController.InputsDGV.ColumnsHierarchy.Items(j)
                Dim value = InputsController.InputsDGV.CellsArea.GetCellValue(row, column)
                If Not IsNumeric(value) Then value = 0
                inputs_list.Add(fmodelling_account_id)
                inputs_periods.Add(j - 1)
                inputs_values.Add(value)
            Next
        Next
        inputs_loaded_flag = True

    End Sub

#End Region


#Region "Scenarios Interface"

    Protected Friend Sub NewScenario(ByRef name As String)

        ' Create a new Scenario Object
        Dim new_id As String = cTreeViews_Functions.GetNewNodeKey(scenariosTV, SCENARII_TOKENS_SIZE)

        Dim new_scenario As New Scenario(new_id, scenariosTV, periods_list, Outputs_name_id_dic, View.inputsDGVsRightClickMenu, FModellingAccount)
        Dim new_node = scenariosTV.Nodes.Add(new_id, name, 0, 0)
        scenarios_dic.Add(new_id, new_scenario)

        ' below should loop through type "contraint" available in DB
        AddConstraint(new_id, "Interest Rate (%)", True, -2)
        AddConstraint(new_id, "Incremental Tax Rate (%)", True, -33)
        AddConstraint(new_id, "Cash Capitalization (%)", True, 0)

        new_node.Expand()
        View.AddScenario(new_scenario, new_node.Index + 1)

    End Sub

    Protected Friend Sub AddConstraint(ByRef scenario_id As String, _
                                       Optional ByRef name As String = "", _
                                       Optional ByRef set_default_value As Boolean = False, _
                                       Optional ByRef default_value As Double = Nothing)

        Dim constraint_id As String = cTreeViews_Functions.GetNewNodeKey(scenariosTV, SCENARII_TOKENS_SIZE)
        Dim new_node = scenariosTV.Nodes.Find(scenario_id, True)(0).Nodes.Add(constraint_id, name, 1, 1)
        If set_default_value = True Then
            scenarios_dic(scenario_id).AddConstraint(constraint_id, name, True, default_value)
        Else
            scenarios_dic(scenario_id).AddConstraint(constraint_id, name)
        End If


    End Sub

    Protected Friend Function GetScenario(ByRef scenario_id As String) As Scenario

        Return scenarios_dic(scenario_id)

    End Function

    Protected Friend Sub ComputeAllScenarios()

        For Each node In scenariosTV.Nodes
            ComputeScenario(node.name)
        Next

    End Sub

    Protected Friend Sub ComputeScenario(ByRef scenario_id As String)

        Dim scenario = scenarios_dic(scenario_id)
        BuildConstraintsArrays(scenario)

        Dim comp_result = Model.Compute(inputs_list.ToArray, _
                                        inputs_periods.ToArray, _
                                        inputs_values.ToArray, _
                                        constraints_list.ToArray, _
                                        constraints_periods.ToArray, _
                                        constraints_targets.ToArray, _
                                        periods_list.Length, 0)

        Select Case comp_result
            Case 0 : MsgBox("The solver could not find any solution with the current inputs and targets." & Chr(13) & _
                            "Possible issues may be a negative Net Result or Debt/ Equity unbalance." & Chr(13) & _
                           "Remember that the Financial Solver bears constraints as Dividend <=0, Debt >=0, etc..." & Chr(13) & _
                           "Please contact the PPS Team if you need further help.")
            Case 1
                Dim data_dic = Model.GetOutputMatrix(FModelling_accounts_all_id_list)
                scenario.FillOutputs(scenario.OutputDGV, scenario.Outputchart, data_dic)
                View.Refresh()
            Case 2 : MsgBox("An occurred in the Financial Solver. You should contact the PPS Team and report this issue.")
        End Select

    End Sub

    Protected Friend Sub DeleteDGVActiveConstraint(ByRef scenario_id As String)

        Dim constraint_id = scenarios_dic(scenario_id).DeleteDGVActiveConstraint
        If constraint_id <> "" Then scenariosTV.Nodes.Find(constraint_id, True)(0).Remove()

    End Sub

    Protected Friend Sub DeleteScenario(ByRef scenario_node As TreeNode)

        scenarios_dic(scenario_node.Name).Delete()
        scenarios_dic.Remove(scenario_node.Name)
        scenario_node.Remove()
        View.RedrawOutputs()

    End Sub

    Protected Friend Sub DeleteConstraint(ByRef scenario_id As String, _
                                          ByRef constraint_node As TreeNode)

        If scenarios_dic(scenario_id).DeleteConstraint(constraint_node.Name) = True Then constraint_node.Remove()

    End Sub

    Protected Friend Sub ExportScenarioToUI(ByRef scenario_id As String)

        Dim scenario As Scenario = scenarios_dic(scenario_id)
        Dim inputDGV As New vDataGridView
        Dim outputDGV As New vDataGridView
        Dim chart As New Chart

        scenario.BuildInputsDGV(inputDGV)
        scenario.BuildOutputsDGV(outputDGV)
        scenario.BuildOutputsChart(chart)
        scenario.FillOutputs(outputDGV, chart)
        scenario.CopyInputsDGVLines(inputdgv)

        View.ExportScenarioToSeparateUI(scenario_id, _
                                        inputDGV, outputDGV, chart)

    End Sub

#End Region


#Region "Utilities"

    Private Sub BuildConstraintsArrays(ByRef scenario As Scenario)

        constraints_list.Clear()
        constraints_periods.Clear()
        constraints_targets.Clear()

        For Each row In scenario.InputsDGV.RowsHierarchy.Items
            For j As Int32 = 1 To periods_list.Length
                Dim value = scenario.InputsDGV.CellsArea.GetCellValue(row, scenario.InputsDGV.ColumnsHierarchy.Items(j))
                If IsNumeric(value) Then
                    constraints_list.Add(FModelling_name_id_dic(scenario.InputsDGV.CellsArea.GetCellValue(row, scenario.InputsDGV.ColumnsHierarchy.Items(0))))
                    constraints_periods.Add(j - 1)
                    constraints_targets.Add(value)
                End If
            Next
        Next

    End Sub

#End Region



End Class
