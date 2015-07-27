' GenericSelectionBuilder.vb
'
'  Generates Filtered lists from categories selection treeviews for analysis axis
' 
'
'
' Author: Julien Monnereau
' Last modified: 27/07/2015


Imports System.Collections.Generic
Imports System.Windows.Forms


Friend Class GenericSelectionBuilder


    Friend Shared Function GetAxisFilteredValuesList(ByRef FvTv As TreeView, _
                                                     ByRef axis_id As UInt32) As List(Of UInt32)

        ' get axis values filters dict
        ' get axis values from axis CRUD and filters dict
        Select Case axis_id



        End Select

    End Function

    Friend Shared Function GetAxisFiltersValuesDict(ByRef FvTv As TreeView, _
                                                    ByRef axis_id As UInt32) As Dictionary(Of UInt32, List(Of UInt32))

        Dim selectionDictionary As New Dictionary(Of UInt32, List(Of UInt32))

        For Each Category As String In getActiveFiltersList(FvTv)
            Dim tmpList As New List(Of UInt32)
            For Each valueNode As TreeNode In FvTv.Nodes.Find(Category, False)(0).Nodes
                If valueNode.Checked = False Then tmpList.Add(valueNode.Name)
            Next
            If tmpList.Count > 0 Then selectionDictionary.Add(Category, tmpList)
        Next
        Return selectionDictionary

    End Function

    Friend Shared Function getActiveFiltersList(ByRef FvTv As TreeView) As List(Of UInt32)

        Dim activeFiltersList As New List(Of UInt32)

        For Each FvTvNode As TreeNode In FvTv.Nodes

            Dim tmpList1 As New List(Of String)
            For Each valueNode As TreeNode In FvTvNode.Nodes
                If valueNode.Checked = False Then tmpList1.Add(valueNode.Name)
            Next
            If tmpList1.Count > 0 Then
                activeFiltersList.Add(FvTvNode.Name)
            End If

            For Each node In FvTvNode.Nodes
                AddActiveFiltersList(FvTvNode, activeFiltersList)
            Next
        Next
        Return activeFiltersList

    End Function

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
                Dim filter_id As UInt32 = GlobalVariables.FiltersValues.filtervalues_hash(node.Name)(FILTER_ID_VARIABLE)
                activeFiltersList.Add(filter_id)
            End If
        Next

    End Sub




End Class
