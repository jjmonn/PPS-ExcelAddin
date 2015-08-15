﻿Imports VIBlend.WinForms.Controls
Imports System.Drawing
Imports VIBlend.Utilities
Imports System.Collections.Generic


Public Class VTreeViewUtil

    Public Shared Function AddNode(ByVal value As String, _
                                   ByVal text As String, _
                                   Optional ByRef parent As Object = Nothing, _
                                   Optional ByRef imageIndex As Int16 = -1) As vTreeNode

        Dim newNode As New vTreeNode()
        newNode.Text = text
        newNode.Value = value
        parent.Nodes.Add(newNode)
        If imageIndex > -1 Then newNode.ImageIndex = imageIndex
        Return newNode

    End Function

    Public Shared Sub InitTVFormat(ByRef TV As vTreeView)

        TV.VIBlendScrollBarsTheme = VIBLEND_THEME.OFFICESILVER

        ' Change the Expand/Collapse arrow color.
        TV.UseThemeIndicatorsColor = False
        TV.IndicatorsColor = Color.FromArgb(128, 128, 128)
        TV.IndicatorsHighlightColor = Color.FromArgb(128, 128, 128)
        TV.EnableIndicatorsAnimation = False
        TV.PaintNodesDefaultBorder = False
        TV.PaintNodesDefaultFill = False

    End Sub

    Public Shared Function FindNode(ByRef TV As vTreeView, _
                                    ByRef value As String) As vTreeNode

        For Each node As vTreeNode In TV.GetNodes
            If node.Value = value Then Return node
        Next
        Return Nothing

    End Function

    Public Shared Function FindNode(ByRef node As vTreeNode, _
                                    ByRef value As String, _
                                    Optional ByRef includeSelf As Boolean = False) As vTreeNode

        For Each subNode As vTreeNode In GetAllChildrenNodesList(node)
            If subNode.Value = value Then Return subNode
        Next
        Return Nothing

    End Function

    Public Shared Function GetNodesIds(ByRef TV As vTreeView) As List(Of Int32)

        Dim tmpList As New List(Of Int32)
        For Each node As vTreeNode In TV.GetNodes
            tmpList.Add(CInt(node.Value))
        Next
        Return tmpList

    End Function

    Public Shared Function GetNodesIds(ByRef node As vTreeNode) As List(Of Int32)

        Dim tmpList As New List(Of Int32)
        For Each subNode As vTreeNode In GetAllChildrenNodesList(node)
            tmpList.Add(CInt(subNode.Value))
        Next
        Return tmpList

    End Function

    Public Shared Function GetAllChildrenNodesList(ByRef node As vTreeNode) As List(Of vTreeNode)

        Dim tmpList As New List(Of vTreeNode)
        For Each subNode As vTreeNode In node.Nodes
            FillChildrenNodesList(subNode, tmpList)
        Next
        Return tmpList

    End Function

    Private Shared Sub FillChildrenNodesList(ByRef node As vTreeNode, _
                                             ByRef list As List(Of vTreeNode))

        list.Add(node)
        For Each subNode As vTreeNode In node.Nodes
            list.Add(subNode)
        Next

    End Sub

 
    Public Shared Sub CopySubNodes(ByRef or_node As vTreeNode, _
                         ByRef des_node As vTreeNode)

        Dim subNode As vTreeNode = VTreeViewUtil.AddNode(or_node.Value, or_node.Text, des_node)
        For Each node In or_node.Nodes
            CopySubNodes(node, subNode)
        Next

    End Sub

    Public Shared Sub SeEntitiesTVImageIndexes(ByRef TV As vTreeView)

        For Each node As vTreeNode In TV.GetNodes
            If node.Nodes.Count > 0 Then
                node.ImageIndex = 0
            Else
                node.StateImageIndex = 1
            End If
        Next

    End Sub

    Public Shared Function GetCheckedNodesIds(ByRef TV As vTreeView) As List(Of Int32)

        Dim tmpList As New List(Of Int32)
        For Each node As vTreeNode In TV.GetNodes
            If node.Checked = True Then tmpList.Add(CInt(node.Value))
        Next
        Return tmpList

    End Function

#Region "TV loading"

    Public Shared Sub LoadTreeview(ByRef TV As vTreeView, _
                                ByRef items_attributes As Collections.Hashtable)

        Dim currentNode, ParentNode As vTreeNode
        Dim orphans_ids_list As New List(Of Int32)
        Dim image_index As UInt16 = 0
        TV.Nodes.Clear()

        For Each id As Int32 In items_attributes.Keys
            If items_attributes(id).ContainsKey(IMAGE_VARIABLE) Then image_index = items_attributes(id)(IMAGE_VARIABLE)
            If items_attributes(id)(PARENT_ID_VARIABLE) = 0 Then
                currentNode = AddNode(items_attributes(id)(ID_VARIABLE), _
                                      items_attributes(id)(NAME_VARIABLE),
                                      TV, _
                                      image_index)
            Else
                ParentNode = FindNode(TV, items_attributes(id)(ID_VARIABLE))
                If Not ParentNode Is Nothing Then
                    currentNode = AddNode(items_attributes(id)(ID_VARIABLE), _
                                        items_attributes(id)(NAME_VARIABLE),
                                        ParentNode, _
                                        image_index)
                Else
                    orphans_ids_list.Add(items_attributes(id)(ID_VARIABLE))
                End If
            End If
        Next
        If orphans_ids_list.Count > 0 Then SolveOrphanNodesList(TV, items_attributes, orphans_ids_list)

    End Sub

    Private Shared Sub SolveOrphanNodesList(ByRef TV As vTreeView, _
                                          ByRef items_attributes As Collections.Hashtable, _
                                          ByRef orphans_id_list As List(Of Int32), _
                                          Optional ByRef solved_orphans_list As List(Of Int32) = Nothing)

        Dim current_node, parent_node As vTreeNode
        Dim image_index As UInt16 = 0
        If solved_orphans_list Is Nothing Then solved_orphans_list = New List(Of Int32)
        For Each orphan_id As Int32 In orphans_id_list
            If items_attributes(orphan_id).ContainsKey(IMAGE_VARIABLE) = True Then image_index = items_attributes(orphan_id)(IMAGE_VARIABLE)
            If solved_orphans_list.Contains(orphan_id) = False Then
                parent_node = FindNode(TV, items_attributes(orphan_id)(PARENT_ID_VARIABLE))

                If Not parent_node Is Nothing Then
                    current_node = AddNode(items_attributes(orphan_id)(ID_VARIABLE), _
                                           items_attributes(orphan_id)(NAME_VARIABLE),
                                           parent_node, _
                                           image_index)

                    solved_orphans_list.Add(orphan_id)
                End If
            End If
        Next
        If solved_orphans_list.Count <> orphans_id_list.Count Then SolveOrphanNodesList(TV, _
                                                                                        items_attributes, _
                                                                                        orphans_id_list, _
                                                                                        solved_orphans_list)

    End Sub

    Public Shared Sub LoadTreenode(ByRef node As vTreeNode, _
                                   ByRef items_attributes As Collections.Hashtable)

        Dim currentNode, ParentNode As vTreeNode
        Dim orphans_ids_list As New List(Of Int32)
        Dim image_index As UInt16 = 0
        node.Nodes.Clear()

        For Each id As Int32 In items_attributes.Keys
            If items_attributes(id).ContainsKey(IMAGE_VARIABLE) Then image_index = items_attributes(id)(IMAGE_VARIABLE)
            If items_attributes(id)(PARENT_ID_VARIABLE) = 0 Then
                currentNode = AddNode(items_attributes(id)(ID_VARIABLE), _
                                       items_attributes(id)(NAME_VARIABLE), _
                                       node, _
                                       image_index)
            Else
                ParentNode = FindNode(node, items_attributes(id)(ID_VARIABLE))
                If Not ParentNode Is Nothing Then
                    currentNode = AddNode(items_attributes(id)(ID_VARIABLE), _
                                        items_attributes(id)(NAME_VARIABLE),
                                        ParentNode, _
                                        image_index)
                Else
                    orphans_ids_list.Add(items_attributes(id)(ID_VARIABLE))
                End If
            End If
        Next
        If orphans_ids_list.Count > 0 Then SolveOrphanNodesList(node, items_attributes, orphans_ids_list)

    End Sub

    Private Shared Sub SolveOrphanNodesList(ByRef node As vTreeNode, _
                                          ByRef items_attributes As Collections.Hashtable, _
                                          ByRef orphans_id_list As List(Of Int32), _
                                          Optional ByRef solved_orphans_list As List(Of Int32) = Nothing)

        Dim current_node, parent_node As vTreeNode
        Dim image_index As UInt16 = 0
        If solved_orphans_list Is Nothing Then solved_orphans_list = New List(Of Int32)
        For Each orphan_id As Int32 In orphans_id_list
            If items_attributes(orphan_id).ContainsKey(IMAGE_VARIABLE) = True Then image_index = items_attributes(orphan_id)(IMAGE_VARIABLE)
            If solved_orphans_list.Contains(orphan_id) = False Then
                parent_node = FindNode(node, items_attributes(orphan_id)(PARENT_ID_VARIABLE))

                If Not parent_node Is Nothing Then
                    current_node = AddNode(items_attributes(orphan_id)(ID_VARIABLE), _
                                           items_attributes(orphan_id)(NAME_VARIABLE),
                                           parent_node, _
                                           image_index)

                    solved_orphans_list.Add(orphan_id)
                End If
            End If
        Next
        If solved_orphans_list.Count <> orphans_id_list.Count Then SolveOrphanNodesList(node, _
                                                                                        items_attributes, _
                                                                                        orphans_id_list, _
                                                                                        solved_orphans_list)

    End Sub


#End Region


End Class
