' GenericSelectionBuilder.vb
'
'  Generates Filtered lists from categories selection treeviews for analysis axis
' 
'
'
' Author: Julien Monnereau
' Last modified: 19/08/2015


Imports System.Collections.Generic
Imports System.Windows.Forms
Imports VIBlend.WinForms.Controls
Imports CRUD

Friend Class FiltersReader


#Region "Axis Filters Build"

    Friend Shared Function IsAxisFiltered(ByRef TV As vTreeView) As Boolean

        For Each node As vTreeNode In TV.GetNodes
            If node.Checked = CheckState.Unchecked Then Return True
        Next
        Return False

    End Function

  

#End Region


#Region "Filters Dictionary Build"

        'Returns a dictionary of active filter with checked values
    Friend Shared Function GetFiltersValuesDict(ByRef FvTv As vTreeView, _
                                                ByRef axis_id As Int32) As Dictionary(Of Int32, List(Of Int32))

        Dim selectionDictionary As New SafeDictionary(Of Int32, List(Of Int32))
        Dim activeFiltersList As List(Of Int32) = GetActiveFiltersList(FvTv)

        For Each node As vTreeNode In FvTv.Nodes
            For Each valueNode As vTreeNode In node.Nodes
                FillSubLevelFiltersValues(valueNode, activeFiltersList, selectionDictionary)
            Next
        Next
        Return selectionDictionary

    End Function

    ' Returns the list of filters that must be taken into account (one value uncheck at least)
    Private Shared Function GetActiveFiltersList(ByRef FvTv As vTreeView) As List(Of Int32)

        Dim activeFiltersList As New List(Of Int32)

        For Each FvTvNode As vTreeNode In FvTv.Nodes

            Dim tmpList1 As New List(Of Int32)
            For Each valueNode As vTreeNode In FvTvNode.Nodes
                If valueNode.Checked = CheckState.Unchecked Then tmpList1.Add(valueNode.Value)
            Next
            If tmpList1.Count > 0 Then
                activeFiltersList.Add(FvTvNode.Value)   ' Add filterId directly (1st level in node)
            End If

            For Each node In FvTvNode.Nodes
                AddActiveFiltersList(FvTvNode, activeFiltersList)
            Next
        Next
        Return activeFiltersList

    End Function

    ' Recursive filtersId addition (treeview hierarchy exploration)
    Private Shared Sub AddActiveFiltersList(ByRef FvTvNode As vTreeNode, _
                                            ByRef activeFiltersList As List(Of Int32))

        For Each node As vTreeNode In FvTvNode.Nodes
            Dim tmpList1 As New List(Of String)
            For Each valueNode As vTreeNode In node.Nodes
                If valueNode.Checked = CheckState.Unchecked Then
                    tmpList1.Add(valueNode.Value)
                End If
            Next
            If tmpList1.Count > 0 Then
                ' We first request the filterId => node id corresponds to the filterValueId
                Dim filterValue As FilterValue = GlobalVariables.FiltersValues.GetValue(CUInt(node.Value))

                If filterValue Is Nothing Then Continue For
                If activeFiltersList.Contains(filterValue.FilterId) = False Then
                    activeFiltersList.Add(filterValue.FilterId)
                End If
            End If
        Next

    End Sub

    ' Fills filter_values_id recursively
    Private Shared Sub FillSubLevelFiltersValues(ByRef valueNode As vTreeNode, _
                                                ByRef filterIdsList As List(Of Int32), _
                                                ByRef selectionDictionary As Dictionary(Of Int32, List(Of Int32)))

        Dim filterValue As FilterValue = GlobalVariables.FiltersValues.GetValue(CUInt(valueNode.Value))

        If filterValue Is Nothing Then Exit Sub
        If filterIdsList.Contains(filterValue.FilterId) _
         AndAlso valueNode.Checked = CheckState.Checked Then
            If selectionDictionary.ContainsKey(filterValue.FilterId) = False Then
                selectionDictionary.Add(filterValue.FilterId, New List(Of Int32))
            End If
            selectionDictionary(filterValue.FilterId).Add(valueNode.Value)
        End If

        ' Loop through children nodes to fill up selection dictionary
        For Each subValueNode As vTreeNode In valueNode.Nodes
            FillSubLevelFiltersValues(subValueNode, filterIdsList, selectionDictionary)
        Next

    End Sub

#End Region


    ' Returns the list of the axis filter values corresponding to the filters applied on its
    ' filters tree
    '  Friend Shared Function GetAxisFilteredValuesList(ByRef FvTv As TreeView, _
    '                                                ByRef axis_id As UInt32) As List(Of UInt32)

    ' get axis values filters dict
    ' get axis values from axis CRUD and filters dict => no !! managed on the server !!
    ' here we just have to build a filterId -> {filtersValuesIds}
    ' No -> needed to update the axis TVs ? option 
    ' to be decided priority normal

    ' get active filterIds list
    ' loop through all nodes
    '   -> if filterId of node is in the list and node = checked => add to filter values list


    ' End Function



End Class
