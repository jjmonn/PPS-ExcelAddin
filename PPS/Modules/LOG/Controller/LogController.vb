' LogModel.vb
'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 07/01/2015


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
    Private period_list As List(Of Int32)
    Protected Friend current_entity_id As String
    Private accounts_id_ftype_dic As Hashtable
    Private entities_id_currency_dic As Hashtable

#End Region


#Region "Initialize"

    Protected Friend Sub New()

        Entity.LoadEntitiesTree(EntitiesTV)
        View = New LogUI(Me, EntitiesTV)
        Model = New LogModel()
        accounts_id_ftype_dic = AccountsMapping.GetAccountsDictionary(ACCOUNT_ID_VARIABLE, ACCOUNT_FORMULA_TYPE_VARIABLE)

        entities_id_currency_dic = EntitiesMapping.GetEntitiesDictionary(ASSETS_TREE_ID_VARIABLE, ASSETS_CURRENCY_VARIABLE)
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

    Protected Friend Sub LaunchComputation(ByRef entity_id As String)

        Model.ComputeEntity(entity_id, current_version_id)
        DisplayResults()
        View.DisplayCurrentAttributes(entities_id_currency_dic(entity_id))
        current_entity_id = entity_id

    End Sub

    Protected Friend Sub DisplayDataHistory(ByRef account_id As String, _
                                            ByRef period As Int32, _
                                            ByRef cell As GridCell)

        Dim DataHistory As New DataHistoryUI(SQLLog.GetValueHistory(current_entity_id, _
                                                                    current_version_id, _
                                                                    account_id, _
                                                                    period))
        Dim p As New Point(cell.Bounds.Left + View.DGVTabControl.Left, cell.Bounds.Top + View.DGVTabControl.Top)
        DataHistory.Location = p
        DataHistory.Show()

    End Sub

#End Region


#Region "Utilities"

    Protected Friend Function IsAccountInput(ByRef account_id As String, _
                                           ByRef period_index As Int32) As Boolean

        Dim ftype As String = accounts_id_ftype_dic(account_id)
        If ftype = FORMULA_TYPE_HARD_VALUE Then
            Return True
        Else
            If ftype = BALANCE_SHEET_ACCOUNT_FORMULA_TYPE Or ftype = WORKING_CAPITAL_ACCOUNT_FORMULA_TYPE Then
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
            If accounts_id_ftype_dic(account_id) <> FORMULA_TYPE_TITLE Then DGV.CellsArea.SetCellValue(row, column, Model.GetData(account_id, period_list(column.ItemIndex)))
        Next
        For Each sub_row As HierarchyItem In row.Items
            FillInViewRow(sub_row, DGV)
        Next

    End Sub


#End Region


#End Region


End Class
