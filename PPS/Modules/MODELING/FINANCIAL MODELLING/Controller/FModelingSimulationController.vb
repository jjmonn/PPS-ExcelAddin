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
' Last modified: 27/05/2015


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections
Imports VIBlend.WinForms.DataGridView
Imports System.Windows.Forms.DataVisualization.Charting



Friend Class FModelingSimulationController


#Region "Instance Variables"

    ' Objects
    Private Model As New FDLL_Interface
    Private MainView As FModelingUI2
    Friend View As FModelingSimulationControl
    Friend FModellingAccount As New FModellingAccount
    Friend scenario As Scenario
    Private fAccountsNodes As TreeNode

    ' Variables
    Friend periods_list As Int32()
    Friend version_id As String
    Private inputs_list As New List(Of String)
    Private inputs_periods As New List(Of Int32)
    Private inputs_values As New List(Of Int32)
    Private constraints_list As New List(Of String)
    Private constraints_periods As New List(Of Int32)
    Private constraints_targets As New List(Of Int32)
    Friend possible_targets_name_id_dict As Hashtable
    Private initial_constraints_id_list As List(Of String)
    Private inputs_loaded_flag As Boolean

    ' Const
    Private Const SCENARII_TOKENS_SIZE As Int32 = 3

#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef MainView As FModelingUI2)

        Me.MainView = MainView
        View = New FModelingSimulationControl(Me)
        fAccountsNodes = FModellingAccount.getFAccountsNode()
        possible_targets_name_id_dict = FModelingAccountsMapping.GetFModellingAccountsDict(FINANCIAL_MODELLING_NAME_VARIABLE, _
                                                                                            FINANCIAL_MODELLING_ID_VARIABLE, _
                                                                                            FINANCIAL_MODELLING_OUTPUT_TYPE)
        initial_constraints_id_list = FModelingAccountsMapping.GetFModellingAccountsList(FINANCIAL_MODELLING_ID_VARIABLE, _
                                                                                         FINANCIAL_MODELLING_CONSTRAINT_TYPE)

    End Sub

#End Region


#Region "Scenarios Interface"

    Friend Sub InitializeInputs(ByRef inputsDGV As vDataGridView)

        inputs_list.Clear()
        inputs_periods.Clear()
        inputs_values.Clear()

        For Each row In InputsDGV.RowsHierarchy.Items
            Dim fmodelling_account_id = row.Caption
            For j As Int32 = 1 To inputsDGV.ColumnsHierarchy.Items.Count - 1
                Dim column = inputsDGV.ColumnsHierarchy.Items(j)
                Dim value = inputsDGV.CellsArea.GetCellValue(row, column)
                If Not IsNumeric(value) Then value = 0
                inputs_list.Add(fmodelling_account_id)
                inputs_periods.Add(j - 1)
                inputs_values.Add(CInt(value))
            Next
        Next
        inputs_loaded_flag = True

    End Sub

    Friend Sub NewScenario()

        scenario = New Scenario(FModellingAccount, _
                                periods_list,
                                fAccountsNodes, _
                                FModelingAccountsMapping.GetFModellingAccountsList(FINANCIAL_MODELLING_ID_VARIABLE, _
                                                                                   FINANCIAL_MODELLING_CONSTRAINT_TYPE))
        scenario.constraintsDGV.ContextMenuStrip = View.inputsDGVsRightClickMenu
        View.DGVsSplitContainer.Panel1.Controls.Add(scenario.constraintsDGV)
        View.DGVsSplitContainer.Panel2.Controls.Add(scenario.generalDGV)
        View.SplitContainer1.Panel2.Controls.Add(scenario.Outputchart)
        scenario.constraintsDGV.Dock = DockStyle.Fill
        scenario.generalDGV.Dock = DockStyle.Fill
        scenario.Outputchart.Dock = DockStyle.Fill

        ComputeScenario()

    End Sub

    Friend Sub AddConstraint(ByRef f_account_name As String, _
                             ByRef default_value As Double)

        scenario.addConstraintRow(fAccountsNodes.Nodes.Find(possible_targets_name_id_dict(f_account_name), True)(0).Name, default_value)

    End Sub

    Friend Sub ComputeScenario()

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
                View.simulationResultTB.Text = "No Solution"
                View.simulationResultTB.ForeColor = Drawing.Color.Orange
            Case 1
                Dim fAccountsIdsList As List(Of UInt32) = TreeViewsUtilities.GetNoChildrenNodesList(fAccountsNodes)
                Dim data_dic = Model.GetOutputMatrix(fAccountsIdsList)
                scenario.fillGeneralDGV(data_dic, fAccountsIdsList)
                View.Refresh()
                View.simulationResultTB.Text = "Success"
                View.simulationResultTB.ForeColor = Drawing.Color.ForestGreen
            Case 2 : MsgBox("An occurred in the Financial Solver. You should contact the PPS Team and report this issue.")
                View.simulationResultTB.Text = "Error"
                View.simulationResultTB.ForeColor = Drawing.Color.Red
        End Select

    End Sub

    Friend Sub DeleteDGVActiveConstraint()

        Select Case scenario.DeleteDGVActiveConstraint
            Case "" : MsgBox("This is a necessary constraint that cannot be removed, but can be set to 0.")
            Case "na" : MsgBox("Please select a constraint to remove.")
        End Select

    End Sub

    Friend Sub CopyValueRight()

        scenario.CopyValueRight()

    End Sub

#End Region


#Region "Utilities"

    Private Sub BuildConstraintsArrays(ByRef scenario As Scenario)

        constraints_list.Clear()
        constraints_periods.Clear()
        constraints_targets.Clear()

        For Each constraint_row As HierarchyItem In scenario.constraintsDGV.RowsHierarchy.Items
            Dim percentage_coef As Int32

            ' instance variable -> fModelingAccounts Attributes Hash !!! 
            If FModellingAccount.ReadFModellingAccount(constraint_row.Caption, FINANCIAL_MODELLING_FORMAT_VARIABLE) = scenario.PERCENT_FORMAT Then percentage_coef = 100 Else percentage_coef = 1
            For j As Int32 = 1 To periods_list.Length
                Dim value = scenario.constraintsDGV.CellsArea.GetCellValue(constraint_row, scenario.constraintsDGV.ColumnsHierarchy.Items(j))
                If IsNumeric(value) Then

                    value = value * percentage_coef
                    If initial_constraints_id_list.Contains(constraint_row.Caption) = False AndAlso j - 1 = 0 Then
                    Else
                        constraints_list.Add(constraint_row.Caption)
                        constraints_periods.Add(j - 1)
                        constraints_targets.Add(CInt(value))
                    End If
                End If
            Next
        Next

    End Sub
    
    Friend Sub Close()

        Model.DestroyDll()
        MyBase.Finalize()

    End Sub

    Friend Function getPossiblesConstraintsNameList() As List(Of String)

        Dim tmp_list As New List(Of String)
        For Each constraint_name As String In possible_targets_name_id_dict.Keys
            If scenario.constraints_DGV_rows_id_item_dict.ContainsKey(possible_targets_name_id_dict(constraint_name)) = False Then
                tmp_list.Add(constraint_name)
            End If
        Next
        Return tmp_list

    End Function

    Friend Sub setCartoucheValues(ByRef version_name As String, _
                                  ByRef currency As String)

        View.InputsVersionTB.Text = version_name
        View.CurrencyTB.Text = currency

    End Sub

#End Region



End Class
