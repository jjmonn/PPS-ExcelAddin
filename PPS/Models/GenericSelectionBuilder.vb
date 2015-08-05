' GenericSelectionBuilder.vb
'
'  Generates Filtered lists from categories selection treeviews for analysis axis
' 
'
'
' Author: Julien Monnereau
' Last modified: 03/08/2015


Imports System.Collections.Generic
Imports System.Windows.Forms


Friend Class GenericSelectionBuilder

    ' Returns the list of the axis filter values corresponding to the filters applied on its
    ' filters tree
    Friend Shared Function GetAxisFilteredValuesList(ByRef FvTv As TreeView, _
                                                     ByRef axis_id As UInt32) As List(Of UInt32)

        ' get axis values filters dict
        ' get axis values from axis CRUD and filters dict => no !! managed on the server !!
        ' here we just have to build a filterId -> {filtersValuesIds}
        ' No -> needed to update the axis TVs ? option 
        ' to be decided priority normal

        ' get active filterIds list
        ' loop through all nodes
        '   -> if filterId of node is in the list and node = checked => add to filter values list


    End Function

    ' Returns a dictionary of active filter with checked values
    Friend Shared Function GetAxisFiltersValuesDict(ByRef FvTv As TreeView, _
                                                    ByRef axis_id As UInt32) As Dictionary(Of UInt32, List(Of UInt32))

        Dim selectionDictionary As New Dictionary(Of UInt32, List(Of UInt32))
        Dim activeFiltersList As List(Of UInt32) = GetActiveFiltersList(FvTv)

        For Each node As TreeNode In FvTv.Nodes
            For Each valueNode As TreeNode In node.Nodes
                FillSubLevelFiltersValues(valueNode, activeFiltersList, selectionDictionary)
            Next
        Next
        Return selectionDictionary

    End Function


#Region "Active Filters Identification"

    ' Returns the list of filters that must be taken into account (one value uncheck at least)
    Friend Shared Function GetActiveFiltersList(ByRef FvTv As TreeView) As List(Of UInt32)

        Dim activeFiltersList As New List(Of UInt32)

        For Each FvTvNode As TreeNode In FvTv.Nodes

            Dim tmpList1 As New List(Of UInt32)
            For Each valueNode As TreeNode In FvTvNode.Nodes
                If valueNode.Checked = False Then tmpList1.Add(valueNode.Name)
            Next
            If tmpList1.Count > 0 Then
                ' Add filterId directly (1st level in node)
                activeFiltersList.Add(FvTvNode.Name)
            End If

            For Each node In FvTvNode.Nodes
                AddActiveFiltersList(FvTvNode, activeFiltersList)
            Next
        Next
        Return activeFiltersList

    End Function

    ' Recursive filtersId addition (treeview hierarchy exploration)
    Friend Shared Sub AddActiveFiltersList(ByRef FvTvNode As TreeNode, _
                                           ByRef activeFiltersList As List(Of UInt32))

        For Each node As TreeNode In FvTvNode.Nodes
            Dim tmpList1 As New List(Of String)
            For Each valueNode As TreeNode In node.Nodes
                If valueNode.Checked = False Then
                    tmpList1.Add(valueNode.Name)
                End If
            Next
            If tmpList1.Count > 0 Then
                ' We first request the filterId => node id corresponds to the filterValueId
                Dim filter_id As UInt32 = GlobalVariables.FiltersValues.filtervalues_hash(node.Name)(FILTER_ID_VARIABLE)
                If activeFiltersList.Contains(filter_id) = False Then
                    activeFiltersList.Add(filter_id)
                End If
            End If
        Next

    End Sub

#End Region


#Region "Filters Values Filling"

    ' Recursive
    Friend Shared Sub FillSubLevelFiltersValues(ByRef valueNode As TreeNode, _
                                                ByRef filterIdsList As List(Of UInt32), _
                                                ByRef selectionDictionary As Dictionary(Of UInt32, List(Of UInt32)))

        Dim filterId As UInt32 = GlobalVariables.FiltersValues.filtervalues_hash(valueNode.Name)(FILTER_ID_VARIABLE)
        If filterIdsList.Contains(filterId) _
         AndAlso valueNode.Checked = True Then
            selectionDictionary(filterId).Add(valueNode.Name)
        End If

        ' Loop through children nodes to fill up selection dictionary
        For Each subValueNode As TreeNode In valueNode.Nodes
            FillSubLevelFiltersValues(subValueNode, filterIdsList, selectionDictionary)
        Next

    End Sub

#End Region


End Class
