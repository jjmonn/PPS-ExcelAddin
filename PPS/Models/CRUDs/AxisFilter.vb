Imports System.Windows.Forms
Imports VIBlend.WinForms.Controls

' AxisFilter
' 
' Loads Filter values treeviews
'
'
' Author: Julien Monnereau
' Created: 27/07/2015
' Modified: 18/08/2015


Friend Class AxisFilter

    Friend Shared Sub LoadFvTv(ByRef FvTv As TreeView, _
                               ByRef axis_id As UInt32)

        Dim filtersNode As New TreeNode
        LoadFvTv(FvTv, filtersNode, axis_id)

    End Sub

    Friend Shared Sub LoadFvTv(ByRef FvTv As vTreeView, _
                               ByRef axis_id As Int32)

        Dim filtersNode As New VIBlend.WinForms.Controls.vTreeNode
        LoadFvTv(FvTv, filtersNode, axis_id)

    End Sub

    Friend Shared Sub LoadFvTv(ByRef FvTv As TreeView, _
                               ByRef filtersNode As TreeNode, _
                               ByRef axis_id As Int32)

        GlobalVariables.Filters.LoadFiltersNode(filtersNode, axis_id)
        For Each filterNode As TreeNode In filtersNode.Nodes
            Dim NewFvTvNode As TreeNode = FvTv.Nodes.Add(filterNode.Name, filterNode.Text, 0, 0)
            LoadFiltersValues(filterNode, NewFvTvNode)
        Next

    End Sub

    Friend Shared Sub LoadFvTv(ByRef FvTv As vTreeView, _
                              ByRef filtersNode As vTreeNode, _
                              ByRef axis_id As UInt32)

        GlobalVariables.Filters.LoadFiltersNode(filtersNode, axis_id)
        For Each filterNode As vTreeNode In filtersNode.Nodes
            Dim NewFvTvNode As vTreeNode = VTreeViewUtil.AddNode(filterNode.Value, filterNode.Text, FvTv, 0)
            LoadFiltersValues(filterNode, NewFvTvNode)
        Next

    End Sub

    Friend Shared Sub LoadFiltersValues(ByRef filterNode As TreeNode, _
                                        ByRef FvTvNode As TreeNode, _
                                        Optional ByVal firstLevelFlag As Boolean = True)

        Dim filtersValuesIdDict = GlobalVariables.FiltersValues.GetFiltervaluesDictionary(CInt(filterNode.Name), _
                                                                                          ID_VARIABLE, NAME_VARIABLE)
        For Each filterValueId As Int32 In filtersValuesIdDict.Keys
            If firstLevelFlag = True Then

                FvTvNode.Nodes.Add(filterValueId, filtersValuesIdDict(filterValueId))
                If filterNode.Nodes.Count > 0 Then
                    LoadFiltersValues(filterNode.Nodes(0), FvTvNode, False)
                End If


            ElseIf GlobalVariables.FiltersValues.filtervalues_hash(filterValueId)(PARENT_FILTER_VALUE_ID_VARIABLE) = FvTvNode.Name Then

                FvTvNode.Nodes.Add(filterValueId, filtersValuesIdDict(filterValueId))
                If filterNode.Nodes.Count > 0 Then
                    LoadFiltersValues(filterNode.Nodes(0), FvTvNode, False)
                End If

            End If
        Next

    End Sub

    Friend Shared Sub LoadFiltersValues(ByRef filterNode As vTreeNode, _
                                        ByRef FvTvNode As vTreeNode, _
                                        Optional ByVal firstLevelFlag As Boolean = True)

        Dim filtersValuesIdDict = GlobalVariables.FiltersValues.GetFiltervaluesDictionary(CInt(filterNode.Value), _
                                                                                          ID_VARIABLE, NAME_VARIABLE)
        For Each filterValueId As Int32 In filtersValuesIdDict.Keys
            If firstLevelFlag = True Then

                Dim valueNode As vTreeNode = VTreeViewUtil.AddNode(filterValueId, filtersValuesIdDict(filterValueId), FvTvNode)
                If filterNode.Nodes.Count > 0 Then
                    LoadFiltersValues(filterNode.Nodes(0), valueNode, False)
                End If

            ElseIf GlobalVariables.FiltersValues.filtervalues_hash(filterValueId)(PARENT_FILTER_VALUE_ID_VARIABLE) = FvTvNode.Value Then
                Dim valueNode As vTreeNode = VTreeViewUtil.AddNode(filterValueId, filtersValuesIdDict(filterValueId), FvTvNode)
                If filterNode.Nodes.Count > 0 Then
                    LoadFiltersValues(filterNode.Nodes(0), valueNode, False)
                End If
            End If
        Next

    End Sub

    ' load a tv with all filters ids and values as children !
    '
    '


End Class
