Imports System.Windows.Forms
Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports System.Collections

' FinancialsDataDisplay.vb
'
' Manages the display of a data_map (fomr computer.vb) on datagrid views or excel
'
'
' To do:
'   - 
'
' Known bugs:
'   -
'
'
' Created: 20/07/2015
' Author: Julien Monnereau
' Last modified: 20/07/2015


Friend Class FinancialsDataDisplay


#Region "Instance variables"

    ' Objects



    ' Variables
    Private accounts_id_shortlist As List(Of UInt32)
    Private rows_hierarchy_node As TreeNode
    Private columns_hierarchy_node As TreeNode
    Private display_axis_ht As New Hashtable
    Private node_id_display_axis_dict As New Dictionary(Of String, UInt32)
    Private data_map As Hashtable
    Private filters_dict As Dictionary(Of String, UInt32)
    Private TVsDict As New Dictionary(Of UInt16, TreeView)

    ' Constants
 



#End Region


#Region "Initialize"


    Friend Sub New()

        Dim filters_id_values_id_lists As New Dictionary(Of UInt32, List(Of UInt32))

        node_id_display_axis_dict.Add(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ENTITIES, GlobalEnums.DataMapAxis.ENTITIES)
        node_id_display_axis_dict.Add(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ACCOUNTS, GlobalEnums.DataMapAxis.ACCOUNTS)
        node_id_display_axis_dict.Add(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.VERSIONS, GlobalEnums.DataMapAxis.VERSIONS)
        node_id_display_axis_dict.Add(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YEARS, GlobalEnums.DataMapAxis.YEARS)
        node_id_display_axis_dict.Add(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.MONTHS, GlobalEnums.DataMapAxis.MONTHS)
        node_id_display_axis_dict.Add(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.CLIENTS, GlobalEnums.DataMapAxis.FILTERS)
        node_id_display_axis_dict.Add(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.PRODUCTS, GlobalEnums.DataMapAxis.FILTERS)
        node_id_display_axis_dict.Add(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ADJUSTMENTS, GlobalEnums.DataMapAxis.FILTERS)
        For Each filter_id In filters_id_values_id_lists.Keys
            node_id_display_axis_dict.Add(Computer.FILTERS_DECOMPOSITION_IDENTIFIER & filter_id, GlobalEnums.DataMapAxis.FILTERS)
        Next

        ' init TVsDict
        ' TVs as param ?
        '+ init  accountsTV

    End Sub


#End Region

#Region "Interface"


    Friend Sub RefreshData()

        filters_dict = New Dictionary(Of String, UInt32)
        ' fillHierarchy -> rows_display_node
        ' fillHierarchy -> columns_display_node 



    End Sub

#End Region


#Region "Hierarchies Initialization"

    Private Sub FillHierarchy(ByRef node As TreeNode, _
                              ByRef list As List(Of TreeNode))

        node.Nodes.Clear()
        For Each item_node In list
            Select Case item_node.Name
                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ENTITIES
                    Dim axis_node As TreeNode = node.Nodes.Add(item_node.Name, item_node.Text)
                    For Each entity_node In TVsDict(item_node.Name).Nodes
                        Dim sub_node As TreeNode = axis_node.Nodes.Add(entity_node.name, entity_node.text)
                        CopySubNodes(entity_node, sub_node)
                    Next

                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ACCOUNTS
                    Dim axis_node As TreeNode = node.Nodes.Add(item_node.Name, item_node.Text)
                    For Each account_node In TreeViewsUtilities.GetNodesList(TVsDict(item_node.Name))
                        axis_node.Nodes.Add(account_node.Name, account_node.Text)
                    Next

                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YEARS
                    ' dependant on version

                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.MONTHS
                    ' dependant on version


                Case Else
                    Dim axis_node As TreeNode = node.Nodes.Add(item_node.Name, item_node.Text)
                    For Each value_node In TVsDict(item_node.Name).Nodes
                        axis_node.Nodes.Add(value_node.name, value_node.text)
                    Next

            End Select
        Next

    End Sub


#End Region


#Region "Columns Creation"

    Friend Sub CreateColumns(ByRef DGV As vDataGridView)

        ' to be implemented
        ' use node.text
        ' loop through columns hierarchy



    End Sub

#End Region


#Region "Fill Data"

    ' 
    Friend Sub FillDGVs(ByRef DGV As vDataGridView, _
                        ByRef tab_account_id As UInt32, _
                        ByRef accountsTV As TreeView, _
                        ByRef accounts_short_list As List(Of UInt32))

        accounts_short_list = TreeViewsUtilities.GetNodesKeysList(accountsTV.Nodes.Find(tab_account_id, True)(0))
        accounts_short_list.Remove(tab_account_id)

        ' display_axis_values should be initialized !!! 
        '  -> entities = head entity
        '  -> filters = 0
        '  -> months = 0

        ' Bien vérifier le display des filters values = TOTAL ("0")


        RowsDisplayLoop(DGV, rows_hierarchy_node.Nodes(0))
        ' format ?

    End Sub

    Private Sub RowsDisplayLoop(ByRef DGV As vDataGridView, _
                                ByRef row_node As TreeNode, _
                                Optional ByRef row_index As UInt32 = 0, _
                                Optional ByRef parent_row As HierarchyItem = Nothing)

        Dim sub_row_index As UInt32 = 0
        Dim row As HierarchyItem

        For Each value_node In row_node.Nodes

            ' Set current value for current display axis
            If SetDisplayAxisValue(value_node) = True Then

                ' Set current DGV row
                If parent_row Is Nothing Then
                    row = DGV.RowsHierarchy.Items(row_index)
                Else
                    row = parent_row.Items(row_index)
                End If

                ' Launch columns loop
                ColumnsDisplayLoop(row, columns_hierarchy_node.Nodes(0))

                ' Dig one level deeper if any
                If Not row_node.NextNode Is Nothing Then
                    RowsDisplayLoop(DGV, row_node.NextNode, sub_row_index, row)
                End If

                ' Loop through children if any
                If row_node.Nodes.Count > 0 Then
                    RowsDisplayLoop(DGV, value_node, sub_row_index, row)
                End If

                ' take off filter at the end of the loop if needed !!
                If node_id_display_axis_dict(CInt(row_node.Name)) = GlobalEnums.DataMapAxis.FILTERS Then filters_dict.Remove(row_node.Name)

            End If
            row_index += 1
        Next

    End Sub

    Private Sub ColumnsDisplayLoop(ByRef row As HierarchyItem, _
                                   ByRef column_node As TreeNode, _
                                   Optional ByRef column_index As UInt32 = 0, _
                                   Optional ByRef parent_column As HierarchyItem = Nothing)

        Dim sub_column_index As UInt32 = 0
        Dim column As HierarchyItem

       For Each value_node As TreeNode In column_node.Nodes

            ' Set current value for current display axis
            If SetDisplayAxisValue(value_node) = True Then

                ' Set current DGV column
                If parent_column Is Nothing Then
                    column = row.DataGridView.ColumnsHierarchy.Items(column_index)
                Else
                    column = parent_column.Items(column_index)
                End If

                ' Set value
                row.DataGridView.CellsArea.SetCellValue(row, column, GetData())

                ' Dig one level deeper if needed
                If Not column_node.NextNode Is Nothing Then
                    ColumnsDisplayLoop(row, column_node.NextNode, sub_column_index, column)
                End If

                ' Loop through children if any
                If column_node.Nodes.Count > 0 Then
                    RowsDisplayLoop(row.DataGridView, value_node, sub_column_index, row)
                End If

                ' Take off filter at the end of the loop if needed
                If node_id_display_axis_dict(CInt(column_node.Name)) = GlobalEnums.DataMapAxis.FILTERS Then filters_dict.Remove(column_node.Name)

            End If
            column_index += 1
        Next

    End Sub

    Private Function SetDisplayAxisValue(ByRef node As TreeNode) As Boolean

        Select Case node_id_display_axis_dict(node.Parent.Name)
            Case GlobalEnums.DataMapAxis.ACCOUNTS
                ' In case display_axis is accounts we should only display the accounts belonging to the current accounts tab
                If accounts_id_shortlist.Contains(CInt(node.Name)) = True Then
                    display_axis_ht(node_id_display_axis_dict(node.Parent.Name)) = CInt(node.Name)
                Else
                    Return False
                End If

            Case GlobalEnums.DataMapAxis.FILTERS
                ' In case display_axis is filters we just add the filter_value_id to the filters_values_id_list
                ' Possible values are analysis_axis except entities
                filters_dict.Add(node.Parent.Name, node.Name)

            Case Else : display_axis_ht(node_id_display_axis_dict(node.Parent.Name)) = CInt(node.Name)
        End Select
        Return True

    End Function

    Private Function GetData() As Object

        ' careful: May be a difference of treatment of total "filters" => "filter_code#0" ou pas de référence du tout
        '          A regarder au moment des essais et fixer avec Nath
        '          S'assurer que la boucle total a bien lieu au niveau du serveur

        Try
            Return data_map(Computer.GetFiltersToken(filters_dict)) _
                           (display_axis_ht(GlobalEnums.DataMapAxis.VERSIONS)) _
                           (display_axis_ht(GlobalEnums.DataMapAxis.ENTITIES)) _
                           (display_axis_ht(GlobalEnums.DataMapAxis.ACCOUNTS)) _
                           (display_axis_ht(GlobalEnums.DataMapAxis.YEARS)) _
                           (display_axis_ht(GlobalEnums.DataMapAxis.MONTHS))
        Catch ex As Exception
            Return "Error"
        End Try

    End Function

#End Region


#Region "Util"

  

    Private Sub CopySubNodes(ByRef or_node As TreeNode, _
                             ByRef des_node As TreeNode)

        For Each node In or_node.Nodes
            Dim new_node As TreeNode = des_node.Nodes.Add(node.name, node.text)
            CopySubNodes(node, new_node)
        Next

    End Sub

#End Region


End Class
