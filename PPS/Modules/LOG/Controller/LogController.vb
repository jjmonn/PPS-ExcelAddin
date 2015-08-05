' LogModel.vb
'
' reimplement with all analysis axis !!
'
'
'
' Author: Julien Monnereau
' Last modified: 24/07/2015


Imports System.Windows.Forms
Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports System.Collections
Imports System.Drawing


Friend Class LogController


#Region "Instance Variables"

    ' Objects
    Private Model As LogModel
    Private View As LogUI
    Private EntitiesTV As New TreeView

    ' Variables
    Private current_version_id As String
    Private period_list As UInt32()
    Friend current_entity_id As String
    Private entities_id_currency_dic As Hashtable
    Private client_id_name_dict As Hashtable = GlobalVariables.Clients.GetClientsDictionary(ID_VARIABLE, NAME_VARIABLE)
    Private product_id_name_dict As Hashtable = GlobalVariables.Products.GetProductsDictionary(ID_VARIABLE, NAME_VARIABLE)
    Private adjustment_id_name_dict As Hashtable = GlobalVariables.Adjustments.GetAdjustmentsDictionary(ID_VARIABLE, NAME_VARIABLE)

#End Region


#Region "Initialize"

    Protected Friend Sub New()

        Globalvariables.Entities.LoadEntitiesTV(EntitiesTV)
        View = New LogUI(Me, EntitiesTV)
        Model = New LogModel()
        entities_id_currency_dic = GlobalVariables.Entities.GetEntitiesDictionary(ID_VARIABLE, ENTITIES_CURRENCY_VARIABLE)
        ChangeVersionID()
        View.Show()

    End Sub

#End Region


#Region "Interface"

    Protected Friend Sub ChangeVersionID()

        current_version_id = GlobalVariables.GLOBALCurrentVersionCode
        period_list = Model.GetPeriodList(current_version_id)
        View.InitializeDGVColumns(current_version_id, period_list, Model.GetTimeConfig(current_version_id))

    End Sub

    Protected Friend Sub LaunchComputation(ByRef entity_node As TreeNode)

        Model.ComputeEntity(entity_node, current_version_id)
        DisplayResults()
        View.DisplayCurrentAttributes(entities_id_currency_dic(entity_node.Name))
        current_entity_id = entity_node.Name

    End Sub

    Protected Friend Sub DisplayDataHistory(ByRef account_id As String, _
                                            ByRef period As Int32, _
                                            ByRef cell As GridCell)

        Dim DataHistory As New DataHistoryUI(SQLLog.GetValueHistory(current_entity_id, _
                                                                    current_version_id, _
                                                                    account_id, _
                                                                    period), _
                                            client_id_name_dict, _
                                            product_id_name_dict, _
                                            adjustment_id_name_dict)

        Dim p As New Point(cell.Bounds.Left + View.DGVTabControl.Left, cell.Bounds.Top + View.DGVTabControl.Top)
        DataHistory.Location = p
        DataHistory.Show()

    End Sub

#End Region


#Region "Utilities"

    Protected Friend Function IsAccountInput(ByRef account_id As String, _
                                             ByRef period_index As Int32) As Boolean

        Dim ftype As String = GlobalVariables.Accounts.accounts_hash(account_id)(ACCOUNT_FORMULA_TYPE_VARIABLE)
        If ftype = GlobalEnums.FormulaTypes.HARD_VALUE_INPUT Then
            Return True
        Else
            If ftype = GlobalEnums.FormulaTypes.FIRST_PERIOD_INPUT Then
                If period_index = 0 Then Return True
            End If
        End If
        Return False

    End Function

#Region "Display Results"

    Private Sub DisplayResults()

        For Each tab_ As TabPage In View.DGVTabControl.TabPages
            Dim node_index As Int32 = 0
            Dim DGV As vDataGridView = tab_.Controls(0)
            For Each row As HierarchyItem In DGV.RowsHierarchy.Items
                FillInViewRow(row, DGV)
            Next
        Next

    End Sub

    Private Sub FillInViewRow(ByRef row As HierarchyItem, _
                              ByRef DGV As vDataGridView)

        For Each column As HierarchyItem In DGV.ColumnsHierarchy.Items
            Dim account_id = View.rows_item_account_id_dic(row)
            If GlobalVariables.Accounts.accounts_hash(account_id)(ACCOUNT_FORMULA_TYPE_VARIABLE) _
               <> GlobalEnums.FormulaTypes.TITLE Then _
                 DGV.CellsArea.SetCellValue(row, column, Model.GetData(account_id, period_list(column.ItemIndex)))
        Next
        For Each sub_row As HierarchyItem In row.Items
            FillInViewRow(sub_row, DGV)
        Next

    End Sub


#End Region


#End Region


End Class
