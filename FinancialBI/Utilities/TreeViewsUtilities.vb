﻿' TreeViewsUtilities.vb
'
' Common treeviews functions
'
'
' Last modified: 24/07/2015
' Author: Julien Monnereau


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports VIBlend.WinForms.DataGridView
Imports System.Drawing
Imports VIBlend.WinForms.Controls
Imports CRUD

Friend Class TreeViewsUtilities


#Region "Treeviews Loading"

    Private Shared Function GenerateTreeNode(Of T As {NamedHierarchyCRUDEntity}) _
    (ByRef p_node As TreeNode, ByRef p_topItemId As UInt32, ByRef items_attributes As MultiIndexDictionary(Of UInt32, String, T)) As Boolean
        Dim currentItem As NamedHierarchyCRUDEntity = items_attributes(p_topItemId)
        If currentItem Is Nothing Then Return False

        p_node.Name = CStr(currentItem.Id)
        p_node.Text = currentItem.Name
        For Each item As NamedHierarchyCRUDEntity In items_attributes.SortedValues
            If item.ParentId = currentItem.Id Then
                Dim childNode As New TreeNode
                If GenerateTreeNode(childNode, item.Id, items_attributes) = False Then Return False
                p_node.Nodes.Add(childNode)
            End If
        Next
        Return True

    End Function

    Friend Shared Sub LoadTreeview(Of T As {NamedHierarchyCRUDEntity})(ByRef TV As TreeView, ByRef items_attributes As MultiIndexDictionary(Of UInt32, String, T))

        For Each item As NamedHierarchyCRUDEntity In items_attributes.SortedValues
            If item.ParentId = 0 Then
                Dim currentNode As New TreeNode

                If (GenerateTreeNode(currentNode, item.Id, items_attributes) = True) Then TV.Nodes.Add(currentNode)
            End If
        Next

    End Sub

    Friend Shared Sub LoadTreeview(Of T As {NamedHierarchyCRUDEntity})(ByRef node As TreeNode, ByRef items_attributes As MultiIndexDictionary(Of UInt32, String, T))

        For Each item As NamedHierarchyCRUDEntity In items_attributes.SortedValues
            If item.ParentId = 0 Then
                Dim currentNode As New TreeNode

                If (GenerateTreeNode(currentNode, item.Id, items_attributes) = True) Then node.Nodes.Add(currentNode)
            End If
        Next

    End Sub

    Public Shared Sub LoadTreeviewIcons(Of T As {CRUDEntity})(ByRef p_treeview As TreeView, _
                                       ByRef p_itemsAttributes As MultiIndexDictionary(Of UInt32, String, T))

        For Each l_node As TreeNode In GetNodesList(p_treeview)
            Dim value As T = p_itemsAttributes(CUInt(l_node.Name))
            If value IsNot Nothing Then
                l_node.ImageIndex = value.Image
                l_node.SelectedImageIndex = value.Image
            End If
        Next

    End Sub

#End Region


#Region "Check all nodes"

    Public Shared Sub CheckAllNodes(ByRef TV As TreeView)

        For Each TreeNode As Windows.Forms.TreeNode In TV.Nodes
            TreeNode.Checked = True
            TreeNode.Tag = True
            If TreeNode.Nodes.Count > 0 Then CheckAllChildNodes(TV, TreeNode, True)
        Next

    End Sub

    Public Shared Sub CheckAllChildNodes(ByRef TV As TreeView, _
                                         ByRef nodeSelected As TreeNode, _
                                         ByRef NodeChecked As Boolean)

        For Each childNode As Windows.Forms.TreeNode In nodeSelected.Nodes
            childNode.Checked = NodeChecked
            childNode.Tag = NodeChecked
            If childNode.Nodes.Count > 0 Then CheckAllChildNodes(TV, childNode, NodeChecked)
        Next

    End Sub

    Public Shared Sub UnselectAll(ByRef TV As TreeView)

        For Each TreeNode As TreeNode In TV.Nodes
            TreeNode.Checked = False
            TreeNode.Tag = False
            If TreeNode.Nodes.Count > 0 Then CheckAllChildNodes(TV, TreeNode, False)
        Next

    End Sub

    Public Shared Sub UpdateNodesCheckedStatus(ByRef TV As TreeView)

        For Each TreeNode As TreeNode In TV.Nodes
            TreeNode.Checked = TreeNode.Tag
        Next TreeNode

    End Sub

#End Region


#Region "Treeviews Checked Nodes List"

    Friend Shared Sub FilterSelectedNodes(ByRef entity_node As TreeNode, _
                                                   ByRef entities_list As List(Of String))

        Dim tmp_array(entities_list.Count - 1) As String
        entities_list.CopyTo(tmp_array)
        For Each entity_id As String In tmp_array
            If entity_node.TreeView.Nodes.Find(entity_id, True)(0).Checked = False Then entities_list.Remove(entity_id)
        Next

    End Sub

    Friend Shared Function GetCheckedNodeCollection(inputTV As TreeView) As List(Of TreeNode)

        Dim tmpList As New List(Of TreeNode)
        For Each node As TreeNode In inputTV.Nodes
            GetSelectedNodes(node, tmpList)
        Next
        Return tmpList

    End Function

    Friend Shared Sub GetSelectedNodes(ByRef inputNode As TreeNode, ByRef tmpList As List(Of TreeNode))

        If inputNode.Checked = True Then tmpList.Add(inputNode)
        For Each childNode As TreeNode In inputNode.Nodes
            GetSelectedNodes(childNode, tmpList)
        Next

    End Sub

    Friend Shared Function GetCheckedNodesID(ByRef TV As TreeView) As List(Of Int32)

        Dim tmpList As New List(Of Int32)
        For Each node As TreeNode In TV.Nodes
            AddCheckedNodesToList(node, tmpList)
        Next
        If tmpList.Count > 0 Then Return tmpList
        Return Nothing

    End Function

    Friend Shared Sub AddCheckedNodesToList(ByRef node As TreeNode, ByRef list As List(Of Int32))

        If node.Checked = True Then list.Add(CInt(node.Name))
        For Each child_node As TreeNode In node.Nodes
            AddCheckedNodesToList(child_node, list)
        Next

    End Sub

    Friend Shared Function GetCheckedNodesID(ByRef node As TreeNode) As List(Of Int32)

        Dim tmpList As New List(Of Int32)
        If node Is Nothing Then Return tmpList
        For Each childNode As TreeNode In node.Nodes
            AddCheckedNodesToList(childNode, tmpList)
        Next
        Return tmpList

    End Function

#End Region


#Region "Nodes Lists"

    Friend Shared Function GetNodeAllChildrenCount(inputNode As TreeNode) As Integer

        Dim nbChildren As Integer = 0
        If inputNode.Nodes.Count > 0 Then
            nbChildren = inputNode.Nodes.Count
            For Each child As TreeNode In inputNode.Nodes
                nbChildren = nbChildren + GetNodeAllChildrenCount(child)
            Next
            Return nbChildren
        Else
            Return 0
        End If

    End Function

    Friend Shared Function GetNodesKeysList(ByRef TV As TreeView) As List(Of Int32)

        Dim tmpList As New List(Of Int32)
        For Each node In TV.Nodes
            AddChildrenKeysToList(node, tmpList)
        Next
        Return tmpList

    End Function

    Friend Shared Function GetNodesTextsList(ByRef TV As TreeView) As List(Of String)

        Dim tmpList As New List(Of String)
        For Each node In TV.Nodes
            AddChildrenTextsToList(node, tmpList)
        Next
        Return tmpList

    End Function

    Friend Shared Function GetNodesKeysList(ByRef node As TreeNode) As List(Of Int32)

        Dim tmpList As New List(Of Int32)
        AddChildrenKeysToList(node, tmpList)
        Return tmpList

    End Function

    Friend Shared Function GetNodesTextsList(ByRef node As TreeNode) As List(Of String)

        Dim tmpList As New List(Of String)
        AddChildrenTextsToList(node, tmpList)
        Return tmpList

    End Function

    Friend Shared Function GetNodesList(ByRef node As TreeNode) As List(Of TreeNode)

        Dim tmp_list As New List(Of TreeNode)
        AddChildrenNodesToList(node, tmp_list)
        tmp_list.Remove(node)
        Return tmp_list

    End Function

    Friend Shared Function GetNodesList(ByRef tv As TreeView) As List(Of TreeNode)

        Dim tmp_list As New List(Of TreeNode)
        For Each node In tv.Nodes
            AddChildrenNodesToList(node, tmp_list)
        Next
        Return tmp_list

    End Function

    Friend Shared Sub AddChildrenKeysToList(ByRef node As TreeNode, tmpList As List(Of Int32))

        tmpList.Add(CInt(node.Name))
        For Each subNode In node.Nodes
            AddChildrenKeysToList(subNode, tmpList)
        Next

    End Sub

    Friend Shared Sub AddChildrenNodesToList(ByRef node As TreeNode, tmpList As List(Of TreeNode))

        tmpList.Add(node)
        For Each subNode In node.Nodes
            AddChildrenNodesToList(subNode, tmpList)
        Next

    End Sub

    Friend Shared Sub AddChildrenTextsToList(ByRef node As TreeNode, tmpList As List(Of String))

        tmpList.Add(node.Text)
        For Each subNode In node.Nodes
            AddChildrenTextsToList(subNode, tmpList)
        Next

    End Sub

    Friend Shared Function GetNodeChildrenIDsArray(ByRef input_node As TreeNode, _
                                                  Optional ByRef filter As Boolean = False)

        Dim children_array(input_node.Nodes.Count - 1) As String
        Dim i As Int32 = 0
        For Each child As TreeNode In input_node.Nodes
            children_array(i) = child.Name
            i = i + 1
        Next

        If filter = True Then
            Dim tmp_list As New List(Of String)
            For Each child As String In children_array
                If input_node.Nodes.Find(child, True)(0).Checked = True Then tmp_list.Add(child)
            Next
            ReDim children_array(tmp_list.Count - 1)
            children_array = tmp_list.ToArray
        End If
        Return children_array

    End Function

    Friend Shared Function GetNoChildrenNodesList(ByRef allIDsList As List(Of Int32),
                                                  ByRef tv As TreeView) As List(Of Int32)

        Dim tmpList As New List(Of Int32)
        For Each id In allIDsList
            Dim tmpNode As TreeNode = tv.Nodes.Find(id, True)(0)
            If tmpNode.Nodes.Count = 0 Then tmpList.Add(tmpNode.Name)
        Next
        Return tmpList

    End Function

    Friend Shared Function getNoChildrenNodesList(ByRef node As TreeNode) As List(Of Int32)

        Dim tmpList As New List(Of Int32)
        Dim allIdList As List(Of Int32) = GetNodesKeysList(node)
        allIdList.Remove(node.Name)
        For Each id In allIdList
            Dim tmpNode As TreeNode = node.Nodes.Find(id, True)(0)
            If tmpNode.Nodes.Count = 0 Then tmpList.Add(tmpNode.Name)
        Next
        Return tmpList

    End Function

    Friend Shared Function GetChildrenIDList(ByRef node As TreeNode) As List(Of Int32)

        Dim children_list As New List(Of Int32)
        For Each child In node.Nodes
            children_list.Add(child.name)
        Next
        Return children_list

    End Function

    Friend Shared Function GetUniqueList(ByRef input_list As List(Of Int32)) As List(Of Int32)

        Dim tmp_list As New List(Of Int32)
        For Each item In input_list
            If tmp_list.Contains(item) = False Then tmp_list.Add(item)
        Next
        Return tmp_list

    End Function

#End Region


#Region "Utilities"

    Friend Shared Function ReturnRootNodeFromNode(ByRef inputNode As TreeNode) As TreeNode

        Dim currentNode As TreeNode = inputNode
        Do While Not currentNode.Parent Is Nothing
            currentNode = currentNode.Parent
        Loop
        Return currentNode

    End Function

    Friend Shared Function IsNameAlreadyInTree(ByRef TV As TreeView, _
                                              ByRef str As String) As Boolean

        For Each node As TreeNode In TV.Nodes
            If IsNameIncludedInHierarchy(node, str) Then Return True
        Next
        Return False

    End Function

    Friend Shared Function IsNameIncludedInHierarchy(ByRef node As TreeNode, _
                                                    ByRef str As String) As Boolean

        If node.Text = str Then
            Return True
        Else
            For Each subNode As TreeNode In node.Nodes
                IsNameIncludedInHierarchy(subNode, str)
            Next
        End If
        Return False

    End Function

    Friend Shared Function GetNodePath(ByRef node As TreeNode, _
                                                Optional ByRef max_hierarchy_id As String = "") As List(Of Int32)

        Dim path_list As New List(Of Int32)
        While Not node.Parent Is Nothing And node.Name <> max_hierarchy_id
            path_list.Add(node.Index)
            node = node.Parent
        End While
        path_list.Reverse()
        Return path_list

    End Function

    Friend Shared Function GetHighestHierarchyLevelFromList(ByRef ids_list As List(Of String), _
                                                                     ByRef TV As TreeView) As String

        Dim all_nodes_list As List(Of Int32) = GetNodesKeysList(TV)
        Dim highest_level_id As String = ""
        Dim highest_index As Int32 = all_nodes_list.Count
        For Each id As String In ids_list
            If all_nodes_list.IndexOf(id) < highest_index Then
                highest_level_id = id
                highest_index = all_nodes_list.IndexOf(id)
            End If
        Next
        Return highest_level_id

    End Function

    Friend Shared Sub TreeviewCopy(ByRef input_tv As TreeView, ByRef copy_tv As TreeView)

        copy_tv.Nodes.Clear()
        For Each node As TreeNode In input_tv.Nodes
            Dim copy_node As TreeNode = copy_tv.Nodes.Add(node.Name, _
                                                          node.Text, _
                                                          node.StateImageIndex, _
                                                          node.SelectedImageIndex)

            For Each sub_node As TreeNode In node.Nodes
                CopyNode(sub_node, copy_node)
            Next
        Next

    End Sub

    Friend Shared Sub CopyNode(ByRef input_node As TreeNode,
                               ByRef copy_parent_node As TreeNode)

        Dim copy_node As TreeNode = copy_parent_node.Nodes.Add(input_node.Name, _
                                                               input_node.Text, _
                                                               input_node.StateImageIndex, _
                                                               input_node.SelectedImageIndex)
        For Each node As TreeNode In input_node.Nodes
            CopyNode(node, copy_node)
        Next

    End Sub

#End Region


#Region "Nodes Icons"

    Public Shared Sub SetTreeviewIconsHiearachy(ByRef p_treeview As TreeView)

        For Each l_node As TreeNode In GetNodesList(p_treeview)
            If l_node.Nodes.Count > 0 Then
                l_node.ImageIndex = 0
                l_node.SelectedImageIndex = 0
            Else
                l_node.ImageIndex = 1
                l_node.SelectedImageIndex = 1
            End If
        Next

    End Sub

    'Public Shared Sub set_TV_basics_icon_index(ByRef TV As TreeView)

    '    Dim nodes_keys_list As List(Of Int32) = GetNodesKeysList(TV)
    '    For Each key In nodes_keys_list
    '        Dim tmp_node = TV.Nodes.Find(key, True)(0)
    '        If tmp_node.Nodes.Count > 0 Then
    '            tmp_node.StateImageIndex = 0
    '            '      tmp_node.SelectedImageIndex = 0
    '        Else
    '            tmp_node.StateImageIndex = 1
    '            '     tmp_node.SelectedImageIndex = 1
    '        End If
    '    Next

    'End Sub

#End Region


#Region "Position Management"

    Friend Shared Function GeneratePositionsDictionary(ByRef TV As TreeView) As Dictionary(Of Int32, Double)

        Dim positionsDictionary As New SafeDictionary(Of Int32, Double)
        Dim currentPosition As Int32 = 0
        For Each node As TreeNode In TV.Nodes
            AddNodeToPositionDictionary(node, currentPosition, positionsDictionary)
        Next
        Return positionsDictionary

    End Function

    Friend Shared Sub AddNodeToPositionDictionary(ByRef inputNode As TreeNode, _
                                                 ByRef currentPosition As Int32, _
                                                 ByRef positionsDictionary As Dictionary(Of Int32, Double))

        If (positionsDictionary.ContainsKey(inputNode.Name)) Then
            positionsDictionary(inputNode.Name) = currentPosition
        Else
            positionsDictionary.Add(inputNode.Name, currentPosition)
        End If

        currentPosition = currentPosition + 1
        For Each subNode As TreeNode In inputNode.Nodes
            AddNodeToPositionDictionary(subNode, currentPosition, positionsDictionary)
        Next

    End Sub


#Region "Move nodes up and down into hierarchy Procedure"

    Friend Shared Sub MoveNodeUp(ByRef inputNode As TreeNode)

        Try
            If Not (inputNode.PrevNode Is Nothing) Then
                Dim prev_node = inputNode.PrevNode
                If inputNode.Parent Is Nothing Then
                    inputNode.TreeView.Nodes.Insert(inputNode.Index - 1, CType(inputNode.Clone, TreeNode))
                Else
                    inputNode.Parent.Nodes.Insert(inputNode.Index - 1, CType(inputNode.Clone, TreeNode))
                End If
                inputNode.Remove()
                prev_node.TreeView.SelectedNode = prev_node.PrevNode
            End If
        Catch ex As Exception
        End Try

    End Sub

    Friend Shared Sub MoveNodeDown(ByRef inputNode As TreeNode)

        Try
            If Not (inputNode.NextNode Is Nothing) Then
                Dim nextnode = inputNode.NextNode
                If inputNode.Parent Is Nothing Then
                    inputNode.TreeView.Nodes.Insert(inputNode.Index + 2, CType(inputNode.Clone, TreeNode))
                Else
                    inputNode.Parent.Nodes.Insert(inputNode.Index + 2, CType(inputNode.Clone, TreeNode))
                End If
                inputNode.Remove()
                nextnode.TreeView.SelectedNode = nextnode.NextNode
            End If
        Catch ex As Exception
        End Try

    End Sub

#End Region


#End Region


End Class



Friend Class TVDragAndDrop


#Region "TGV Drag and drop"

    Protected Friend Shared Sub TGV_HierarchyItemDrag(sender As Object, args As HierarchyItemDragEventArgs)

        ' args.DragSourceHierarchyItem
        Dim TGV As vDataGridView = sender
        TGV.DoDragDrop(TGV.RowsHierarchy.SelectedItems(0), DragDropEffects.Move)

    End Sub

    Friend Shared Sub TGV_DragEnter(sender As Object, e As DragEventArgs)

        If e.Data.GetDataPresent("VIBlend.WinForms.DataGridView.HierarchyItem", True) Then
            e.Effect = DragDropEffects.Move                           ' Hierarchy item found allow move effect
        Else
            e.Effect = DragDropEffects.None                           ' No Hierarchy item found, prevent move
        End If
    End Sub

    Friend Shared Sub TGV_DragOver(sender As Object, e As DragEventArgs)

        If e.Data.GetDataPresent("VIBlend.WinForms.DataGridView.HierarchyItem", True) = False Then
            Exit Sub
        End If

        Dim TGV As vDataGridView = CType(sender, vDataGridView)
        Dim pt As Point = CType(sender, vDataGridView).PointToClient(New Drawing.Point(e.X, e.Y))
        Dim targetItem As HierarchyItem = TGV.RowsHierarchy.HitTest(pt)

        ' See if the targetNode is currently selected, if so no need to validate again
        If Not (TGV.RowsHierarchy.SelectedItems(0) Is targetItem) Then

            'Select the item currently under the cursor
            TGV.RowsHierarchy.SelectItem(targetItem)

            ' Check that the selected node is not the dropNode and also that it is not a child of _
            ' the dropNode and therefore an invalid target
            Dim dropItem As HierarchyItem = CType(e.Data.GetData("VIBlend.WinForms.DataGridView.HierarchyItem"), HierarchyItem)

            Do Until targetItem Is Nothing
                If targetItem Is dropItem Then
                    e.Effect = Windows.Forms.DragDropEffects.None
                    Exit Sub
                End If
                targetItem = targetItem.ParentItem
            Loop
        End If

        'Currently selected node is a suitable target
        e.Effect = DragDropEffects.Move

    End Sub

    Friend Shared Sub TGV_DragDrop(sender As Object, e As DragEventArgs)

        'Check that there is an item being dragged
        If e.Data.GetDataPresent("VIBlend.WinForms.DataGridView.HierarchyItem", True) = False Then Exit Sub

        'Get the TGV raising the event (incase multiple on form)
        Dim TGV As vDataGridView = CType(sender, vDataGridView)

        'Get the TreeNode being dragged
        Dim dropItem As HierarchyItem = CType(e.Data.GetData("VIBlend.WinForms.DataGridView.HierarchyItem"), HierarchyItem)

        'The target node should be selected from the DragOver event
        Dim targetItem As HierarchyItem = TGV.RowsHierarchy.SelectedItems(0)

        dropItem.Delete()                                               ' Remove the item node from its current location

        If targetItem Is Nothing Then
            TGV.RowsHierarchy.Items.Add(dropItem)
        Else
            targetItem.Items.Add(dropItem)
        End If

        TGV.RowsHierarchy.SelectItem(dropItem)                          ' Select it

    End Sub

#End Region


#Region "TV Drag and Drop"

    Protected Friend Shared Sub TV_DragEnter(sender As Object, e As Windows.Forms.DragEventArgs)

        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) Then
            e.Effect = DragDropEffects.Move
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Protected Friend Shared Sub TV_DragOver(sender As Object, e As Windows.Forms.DragEventArgs)

        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub
        Dim selectedTreeview As Windows.Forms.TreeView = CType(sender, Windows.Forms.TreeView)
        Dim pt As Drawing.Point = CType(sender, TreeView).PointToClient(New Drawing.Point(e.X, e.Y))
        Dim targetNode As TreeNode = selectedTreeview.GetNodeAt(pt)

        'See if the targetNode is currently selected, if so no need to validate again
        If Not (selectedTreeview.SelectedNode Is targetNode) Then       'Select the node currently under the cursor
            selectedTreeview.SelectedNode = targetNode

            'Check that the selected node is not the dropNode and also that it is not a child of the dropNode and therefore an invalid target
            Dim dropNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

            Do Until targetNode Is Nothing
                If targetNode Is dropNode Then
                    e.Effect = DragDropEffects.None
                    Exit Sub
                End If
                targetNode = targetNode.Parent
            Loop
        End If
        'Currently selected node is a suitable target
        e.Effect = DragDropEffects.Move

    End Sub

#End Region


End Class