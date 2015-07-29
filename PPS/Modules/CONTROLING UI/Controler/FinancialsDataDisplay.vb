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
' Last modified: 29/07/2015


Friend Class FinancialsDataDisplay


#Region "Instance variables"

    ' Objects
    Private View As ControllingUI_2

    ' Variables
    Private accounts_id_shortlist As List(Of UInt32)
    Private rows_hierarchy_node As TreeNode
    Private columns_hierarchy_node As TreeNode
    Private display_axis_ht As New Hashtable
    Private node_id_display_axis_dict As New Dictionary(Of String, UInt32)
    Private data_map As Hashtable
    Private filters_dict As Dictionary(Of String, UInt32)
    Private TVsDict As New Dictionary(Of String, TreeView)
    Private VersionsTV As New TreeView
    Private YearsTV As New TreeView
    Private MonthsTV As New TreeView
    Friend versionsDict As Dictionary(Of UInt32, String)

    ' Constants
 

#End Region


#Region "Initialize"


    Friend Sub New(ByRef View As ControllingUI_2)

        Me.View = View
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

         TVsDict.Add(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ENTITIES, View.entitiesTV)
        TVsDict.Add(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ACCOUNTS, View.accountsTV)
        TVsDict.Add(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.CLIENTS, View.clientsTV)
        TVsDict.Add(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.PRODUCTS, View.productsTV)
        TVsDict.Add(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ADJUSTMENTS, View.adjustmentsTV)
        TVsDict.Add(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.VERSIONS, VersionsTV)
        TVsDict.Add(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YEARS, YearsTV)
        TVsDict.Add(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.MONTHS, MonthsTV)
        Dim FullFvTV As New TreeView
        LoadSpecialFiltersValuesNode(FullFvTV)
        TVsDict.Add(Computer.FILTERS_DECOMPOSITION_IDENTIFIER, FullFvTV)
     
    End Sub


#End Region


#Region "Interface"


    Friend Sub InitDisplay(ByRef rowsHierarchyNodeList As List(Of TreeNode), _
                           ByRef columnsHierarchyNodeList As List(Of TreeNode))

        For Each versionNode As TreeNode In View.versionsTV.Nodes
            If versionNode.Checked = True Then versionsDict.Add(versionNode.Name, versionNode.Text)
        Next

        ' while computation on server
        ' (before while loop waiting for background worker to end)

        '   -> reload years/ months trees
        '   -> reload versions TV

        filters_dict = New Dictionary(Of String, UInt32)


        FillHierarchy(rows_hierarchy_node, rowsHierarchyNodeList)
        FillHierarchy(columns_hierarchy_node, columnsHierarchyNodeList)


    End Sub

    Friend Sub FillDGVs(ByRef DGV As vDataGridView, _
                        ByRef tab_account_id As UInt32)

        accounts_id_shortlist = TreeViewsUtilities.GetNodesKeysList(View.accountsTV.Nodes.Find(tab_account_id, True)(0))
        accounts_id_shortlist.Remove(tab_account_id)

        ' display_axis_values should be initialized !!! 
        '  -> entities = head entity
        '  -> filters = 0
        '  -> months = 0

        ' Bien vérifier le display des filters values = TOTAL ("0")

        For Each node As TreeNode In rows_hierarchy_node.Nodes
            RowsDisplayLoop(DGV, rows_hierarchy_node.Nodes(0))
        Next

        ' format ?

    End Sub

#End Region


#Region "Display Initialization"

    ' Generic Rows/ Columns hierarchy node initialization
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

                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.VERSIONS
                    For Each version_id In versionsDict.Keys
                        node.Nodes.Add(version_id, versionsDict(version_id))
                    Next

                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YEARS
                    ' dependant on version priority high !!!

                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.MONTHS
                    ' dependant on version priority high !!!

                Case Else
                    Dim axis_node As TreeNode = node.Nodes.Add(item_node.Name, item_node.Text)
                    For Each value_node In TVsDict(Computer.FILTERS_DECOMPOSITION_IDENTIFIER).Nodes
                        axis_node.Nodes.Add(value_node.name, value_node.text)
                    Next

            End Select
        Next

    End Sub

    Friend Sub CreateRowsAndColumns(ByRef DGV As vDataGridView)

        ' keep track of dimension id / column ? priority normal !!!
        ' keep track of version hierarchy item !!! priority high !!!!
        For Each node In columns_hierarchy_node.Nodes
            CreateColumn(DGV, node)
        Next

        For Each node In rows_hierarchy_node.Nodes
            CreateRow(DGV, node)
        Next

    End Sub

    Private Sub CreateColumn(ByRef dgv As vDataGridView, _
                             ByRef dimensionNode As TreeNode, _
                             Optional ByRef valueNode As TreeNode = Nothing, _
                             Optional ByRef column As HierarchyItem = Nothing)

        Dim subColumn As HierarchyItem
        If valueNode Is Nothing Then
            subColumn = column
            ' Loop through values
            For Each subNode In dimensionNode.Nodes
                CreateColumn(dgv, dimensionNode, subNode, subColumn)
            Next
        Else
            If column Is Nothing Then
                subColumn = dgv.ColumnsHierarchy.Items.Add(valueNode.Text)
            Else
                subColumn = column.Items.Add(valueNode.Text)
            End If

            ' Dig one level deeper if any
            If Not dimensionNode.NextNode Is Nothing Then
                CreateColumn(dgv, dimensionNode.NextNode, , subColumn)
            End If

            ' Loop through children if any
            For Each subNode In valueNode.Nodes
                CreateColumn(dgv, dimensionNode, subNode, subColumn)
            Next
        End If

    End Sub

    Private Sub CreateRow(ByRef dgv As vDataGridView, _
                          ByRef dimensionNode As TreeNode, _
                          Optional ByRef valueNode As TreeNode = Nothing, _
                          Optional ByRef row As HierarchyItem = Nothing)

        Dim subRow As HierarchyItem
        If valueNode Is Nothing Then
            ' Loop through values
            For Each subNode In dimensionNode.Nodes
                CreateRow(dgv, dimensionNode, subNode, row)
            Next
        Else
            If row Is Nothing Then
                subRow = dgv.RowsHierarchy.Items.Add(valueNode.Text)
            Else
                subRow = row.Items.Add(valueNode.Text)
            End If

            ' Dig one level deeper if any
            If Not dimensionNode.NextNode Is Nothing Then
                CreateRow(dgv, dimensionNode.NextNode, , subRow)
            End If

            ' Loop through children if any
            For Each subNode In valueNode.Nodes
                CreateRow(dgv, dimensionNode, subNode, subRow)
            Next
        End If

    End Sub

#End Region


#Region "Fill Data"

    Private Sub RowsDisplayLoop(ByRef DGV As vDataGridView, _
                                ByRef dimensionNode As TreeNode, _
                                Optional ByRef valueNode As TreeNode = Nothing, _
                                Optional ByRef row_index As UInt32 = 0, _
                                Optional ByRef parent_row As HierarchyItem = Nothing)

        Dim sub_row_index As UInt32 = 0
        Dim row As HierarchyItem

        If valueNode Is Nothing Then
            For Each currentValueNode In dimensionNode.Nodes
                RowsDisplayLoop(DGV, dimensionNode, currentValueNode, row_index, parent_row)
            Next
        Else
            'Set current value for current display axis
            If SetDisplayAxisValue(dimensionNode, valueNode) = True Then

                ' Set current DGV row
                If parent_row Is Nothing Then
                    row = DGV.RowsHierarchy.Items(row_index)
                Else
                    row = parent_row.Items(row_index)
                End If

                '  Launch columns loop
                ColumnsDisplayLoop(row, columns_hierarchy_node.Nodes(0))

                ' Dig one level deeper if any
                If Not dimensionNode.NextNode Is Nothing Then
                    RowsDisplayLoop(DGV, dimensionNode.NextNode, , sub_row_index, row)
                End If

                '  Loop through children if any
                For Each subValueNode As TreeNode In valueNode.Nodes
                    RowsDisplayLoop(DGV, dimensionNode, subValueNode, sub_row_index, row)
                Next

                ' Take off filter at the end of the loop if needed !!
                If node_id_display_axis_dict(CInt(dimensionNode.Name)) = GlobalEnums.DataMapAxis.FILTERS Then _
                    filters_dict.Remove(dimensionNode.Name)

            End If
            row_index += 1
        End If

    End Sub

    Private Sub ColumnsDisplayLoop(ByRef row As HierarchyItem, _
                                   ByRef dimensionNode As TreeNode, _
                                   Optional ByRef valueNode As TreeNode = Nothing, _
                                   Optional ByRef column_index As UInt32 = 0, _
                                   Optional ByRef parent_column As HierarchyItem = Nothing)

        Dim sub_column_index As UInt32 = 0
        Dim column As HierarchyItem


        If valueNode Is Nothing Then
            For Each currentValueNode In dimensionNode.Nodes
                ColumnsDisplayLoop(row, dimensionNode, currentValueNode, column_index, parent_column)
            Next
        Else
            ' Set current value for current display axis
            If SetDisplayAxisValue(dimensionNode, valueNode) = True Then

                ' Set current DGV column
                If parent_column Is Nothing Then
                    column = row.DataGridView.ColumnsHierarchy.Items(column_index)
                Else
                    column = parent_column.Items(column_index)
                End If

                ' Set value
                ' virtual binding -> priority normal !!!
                row.DataGridView.CellsArea.SetCellValue(row, column, GetData())

                ' Dig one level deeper if needed
                If Not dimensionNode.NextNode Is Nothing Then
                    ColumnsDisplayLoop(row, dimensionNode.NextNode, , sub_column_index, column)
                End If

                ' Loop through children if any
                For Each subValueNode As TreeNode In valueNode.Nodes
                    RowsDisplayLoop(row.DataGridView, dimensionNode, subValueNode, sub_column_index, row)
                Next
            End If

            ' Take off filter at the end of the loop if needed
            If node_id_display_axis_dict(CInt(dimensionNode.Name)) = GlobalEnums.DataMapAxis.FILTERS Then filters_dict.Remove(dimensionNode.Name)
            column_index += 1
        End If
       
    End Sub

    Private Function SetDisplayAxisValue(ByRef dimensionNode As TreeNode, _
                                         ByRef valueNode As TreeNode) As Boolean

        Select Case node_id_display_axis_dict(dimensionNode.Name)
            Case GlobalEnums.DataMapAxis.ACCOUNTS
                ' In case display_axis is accounts we should only display the accounts belonging to the current accounts tab
                If accounts_id_shortlist.Contains(CInt(valueNode.Name)) = True Then
                    display_axis_ht(node_id_display_axis_dict(dimensionNode.Name)) = CInt(valueNode.Name)
                Else
                    Return False
                End If

            Case GlobalEnums.DataMapAxis.FILTERS
                ' In case display_axis is filters we just add the filter_value_id to the filters_values_id_list
                ' Possible values are analysis_axis except entities
                filters_dict.Add(dimensionNode.Name, CInt(valueNode.Name))

            Case Else : display_axis_ht(node_id_display_axis_dict(dimensionNode.Name)) = CInt(valueNode.Name)
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

    Private Sub LoadSpecialFiltersValuesNode(ByRef FullFvNode As TreeView)

        For Each filterId As UInt32 In GlobalVariables.Filters.filters_hash.Keys
            Dim filterNodeName As String = Computer.FILTERS_DECOMPOSITION_IDENTIFIER & filterId
            Dim filterNodeText As String = GlobalVariables.Filters.filters_hash(filterId)(NAME_VARIABLE)
            Dim filterNode As TreeNode = FullFvNode.Nodes.Add(filterNodeName, filterNodeText)

            Dim filterValuesDict As Hashtable = GlobalVariables.FiltersValues.GetFiltervaluesDictionary(filterId, _
                                                                                                        ID_VARIABLE, _
                                                                                                        NAME_VARIABLE)
            For Each filterValueId As UInt32 In filterValuesDict.Keys
                filterNode.Nodes.Add(filterValueId, filterValuesDict(filterValueId))
            Next
        Next

    End Sub



#End Region


End Class
