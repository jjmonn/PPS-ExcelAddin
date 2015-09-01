' TreeViewsUtilities.vb
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


Friend Class TreeViewsUtilities


#Region "Treeviews Loading"

    'Friend Shared Sub LoadTreeview(ByRef TV As TreeView, _
    '                               ByRef RST As ADODB.Recordset)

    '    Dim currentNode, ParentNode() As TreeNode
    '    Dim orphans_ids_list As New List(Of String)
    '    TV.Nodes.Clear()
    '    RST.MoveFirst()
    '    Do While RST.EOF = False
    '        Dim image_index As Int32 = RST.Fields(IMAGE_VARIABLE).Value

    '        If IsDBNull(RST.Fields(PARENT_ID_VARIABLE).Value) Then
    '            currentNode = TV.Nodes.Add(Trim(RST.Fields(ID_VARIABLE).Value), _
    '                                       Trim(RST.Fields(NAME_VARIABLE).Value), _
    '                                       image_index, image_index)
    '        Else
    '            ParentNode = TV.Nodes.Find(Trim(RST.Fields(PARENT_ID_VARIABLE).Value), True)
    '            If ParentNode.Length > 0 Then
    '                currentNode = ParentNode(0).Nodes.Add(Trim(RST.Fields(ID_VARIABLE).Value), _
    '                                                      Trim(RST.Fields(NAME_VARIABLE).Value), _
    '                                                      image_index, image_index)
    '            Else
    '                orphans_ids_list.Add(RST.Fields(ID_VARIABLE).Value)
    '            End If
    '        End If
    '        RST.MoveNext()
    '    Loop
    '    If orphans_ids_list.Count > 0 Then SolveOrphanNodesList(TV, RST, orphans_ids_list)

    'End Sub

    'Friend Shared Sub SolveOrphanNodesList(ByRef TV As TreeView, _
    '                                       ByRef RST As ADODB.Recordset, _
    '                                       ByRef orphans_id_list As List(Of String), _
    '                                       Optional ByRef solved_orphans_list As List(Of String) = Nothing)

    '    Dim current_node, parent_nodes() As TreeNode
    '    Dim image_index As Int32
    '    If solved_orphans_list Is Nothing Then solved_orphans_list = New List(Of String)
    '    For Each orphan_id In orphans_id_list
    '        If solved_orphans_list.Contains(orphan_id) = False Then
    '            RST.Filter = ID_VARIABLE & "='" & orphan_id & "'"
    '            parent_nodes = TV.Nodes.Find(RST.Fields(PARENT_ID_VARIABLE).Value, True)

    '            If parent_nodes.Length > 0 Then
    '                image_index = RST.Fields(IMAGE_VARIABLE).Value
    '                current_node = parent_nodes(0).Nodes.Add(Trim(RST.Fields(ID_VARIABLE).Value), _
    '                                                         Trim(RST.Fields(NAME_VARIABLE).Value), _
    '                                                         image_index, _
    '                                                         image_index)
    '                solved_orphans_list.Add(orphan_id)
    '            End If
    '        End If
    '    Next
    '    If solved_orphans_list.Count <> orphans_id_list.Count Then SolveOrphanNodesList(TV, _
    '                                                                                   RST, _
    '                                                                                   orphans_id_list, _
    '                                                                                   solved_orphans_list)

    'End Sub

    Friend Shared Sub LoadTreeview(ByRef TV As TreeView, ByRef items_attributes As Collections.Hashtable)

        Dim currentNode, ParentNode() As TreeNode
        Dim orphans_ids_list As New List(Of int32)
        Dim image_index As UInt16 = 0
        TV.Nodes.Clear()

        For Each id As Int32 In items_attributes.Keys
            If items_attributes(id).ContainsKey(IMAGE_VARIABLE) Then image_index = items_attributes(id)(IMAGE_VARIABLE)
            If items_attributes(id)(PARENT_ID_VARIABLE) = 0 Then
                currentNode = TV.Nodes.Add(items_attributes(id)(ID_VARIABLE), _
                                           items_attributes(id)(NAME_VARIABLE), _
                                           image_index, image_index)
            Else
                ParentNode = TV.Nodes.Find((items_attributes(id)(PARENT_ID_VARIABLE)), True)
                If ParentNode.Length > 0 Then
                    currentNode = ParentNode(0).Nodes.Add(items_attributes(id)(ID_VARIABLE), _
                                                          items_attributes(id)(NAME_VARIABLE), _
                                                          image_index, image_index)
                Else
                    orphans_ids_list.Add(items_attributes(id)(ID_VARIABLE))
                End If
            End If
        Next
        If orphans_ids_list.Count > 0 Then SolveOrphanNodesList(TV, items_attributes, orphans_ids_list)

    End Sub

    Friend Shared Sub SolveOrphanNodesList(ByRef TV As TreeView, _
                                          ByRef items_attributes As Collections.Hashtable, _
                                          ByRef orphans_id_list As List(Of Int32), _
                                          Optional ByRef solved_orphans_list As List(Of Int32) = Nothing)

        Dim current_node, parent_nodes() As TreeNode
        Dim image_index As UInt16 = 0
        If solved_orphans_list Is Nothing Then solved_orphans_list = New List(Of int32)
        For Each orphan_id As Int32 In orphans_id_list
            If items_attributes(orphan_id).ContainsKey(IMAGE_VARIABLE) = True Then image_index = items_attributes(orphan_id)(IMAGE_VARIABLE)
            If solved_orphans_list.Contains(orphan_id) = False Then
                parent_nodes = TV.Nodes.Find(items_attributes(orphan_id)(PARENT_ID_VARIABLE), True)

                If parent_nodes.Length > 0 Then
                    current_node = parent_nodes(0).Nodes.Add(items_attributes(orphan_id)(ID_VARIABLE), _
                                                             items_attributes(orphan_id)(NAME_VARIABLE), _
                                                             image_index, _
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

    Friend Shared Sub LoadTreeview(ByRef node As TreeNode, ByRef items_attributes As Collections.Hashtable)

        Dim currentNode, ParentNode() As TreeNode
        Dim orphans_ids_list As New List(Of int32)
        Dim image_index As UInt16 = 0
        node.Nodes.Clear()

        For Each id As Int32 In items_attributes.Keys
            If items_attributes(id).ContainsKey(IMAGE_VARIABLE) Then image_index = items_attributes(id)(IMAGE_VARIABLE)
            If items_attributes(id)(PARENT_ID_VARIABLE) = 0 Then
                currentNode = node.Nodes.Add(items_attributes(id)(ID_VARIABLE), _
                                           items_attributes(id)(NAME_VARIABLE), _
                                           image_index, image_index)
            Else
                ParentNode = node.Nodes.Find((items_attributes(id)(PARENT_ID_VARIABLE)), True)
                If ParentNode.Length > 0 Then
                    currentNode = ParentNode(0).Nodes.Add(items_attributes(id)(ID_VARIABLE), _
                                                          items_attributes(id)(NAME_VARIABLE), _
                                                          image_index, image_index)
                Else
                    orphans_ids_list.Add(items_attributes(id)(ID_VARIABLE))
                End If
            End If
        Next
        If orphans_ids_list.Count > 0 Then SolveOrphanNodesList(node, items_attributes, orphans_ids_list)

    End Sub

    Friend Shared Sub SolveOrphanNodesList(ByRef node As TreeNode, _
                                          ByRef items_attributes As Collections.Hashtable, _
                                          ByRef orphans_id_list As List(Of Int32), _
                                          Optional ByRef solved_orphans_list As List(Of Int32) = Nothing)

        Dim current_node, parent_nodes() As TreeNode
        Dim image_index As UInt16 = 0
        If solved_orphans_list Is Nothing Then solved_orphans_list = New List(Of int32)
        For Each orphan_id As Int32 In orphans_id_list
            If items_attributes(orphan_id).ContainsKey(IMAGE_VARIABLE) Then image_index = items_attributes(orphan_id)(IMAGE_VARIABLE)
            If solved_orphans_list.Contains(orphan_id) = False Then
                parent_nodes = node.Nodes.Find(items_attributes(orphan_id)(PARENT_ID_VARIABLE), True)

                If parent_nodes.Length > 0 Then
                    current_node = parent_nodes(0).Nodes.Add(items_attributes(orphan_id)(ID_VARIABLE), _
                                                             items_attributes(orphan_id)(NAME_VARIABLE), _
                                                             image_index, _
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

    Friend Shared Function GetChildrenIDList(ByRef node As TreeNode) As List(Of String)

        Dim children_list As New List(Of String)
        For Each child In node.Nodes
            children_list.Add(child.name)
        Next
        Return children_list

    End Function

    Friend Shared Function GetUniqueList(ByRef input_list As List(Of Int32)) As List(Of Int32)

        Dim tmp_list As New List(Of int32)
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

        Dim all_nodes_list As List(Of int32) = GetNodesKeysList(TV)
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


#Region "Nodes Expansion/ Checked States Level Utility"

    Public Shared Function SaveNodesExpansionsLevel(TV As TreeView) As Dictionary(Of String, Boolean)

        Dim expansionDic As New Collections.Generic.Dictionary(Of String, Boolean)
        For Each currNode As TreeNode In TV.Nodes
            SaveChildrenExpansionsLevel(currNode, expansionDic)
        Next
        Return expansionDic

    End Function

    Public Shared Sub SaveChildrenExpansionsLevel(ByRef currNode As TreeNode, _
                                                  ByRef expansionDic As Dictionary(Of String, Boolean))

        expansionDic.Add(currNode.Name, currNode.IsExpanded)
        If currNode.Nodes.Count > 0 Then
            For Each child As TreeNode In currNode.Nodes
                SaveChildrenExpansionsLevel(child, expansionDic)
            Next
        End If

    End Sub

    Public Shared Sub ResumeExpansionsLevel(ByRef TV As TreeView, _
                                            ByRef expansionDictionary As Dictionary(Of String, Boolean))

        TV.CollapseAll()
        For Each key In expansionDictionary.Keys
            If expansionDictionary(key) = True Then
                Dim tmpNode() = TV.Nodes.Find(key, True)
                If tmpNode.Length > 0 Then tmpNode(0).Expand()
            End If
        Next

    End Sub

    Public Shared Function SaveCheckedStates(ByRef TV As TreeView) As List(Of String)

        Dim checkedList As New List(Of String)
        For Each currNode As TreeNode In TV.Nodes
            SaveChildrenCheckedState(currNode, checkedList)
        Next
        Return checkedList

    End Function

    Public Shared Sub SaveChildrenCheckedState(ByRef currNode As TreeNode, _
                                               ByRef checkedList As List(Of String))

        If currNode.Checked = True Then checkedList.Add(currNode.Name)
        For Each child As TreeNode In currNode.Nodes
            SaveChildrenCheckedState(child, checkedList)
        Next

    End Sub

    Public Shared Function ResumeCheckedStates(ByRef TV As TreeView, ByRef checkedList As List(Of String)) As List(Of String)

        Dim tmpList As New List(Of String)
        For Each key In checkedList
            Dim tmpNode() = TV.Nodes.Find(key, True)
            If tmpNode.Length > 0 Then
                tmpNode(0).Checked = True
            Else
                tmpList.Add(key)
            End If
        Next
        Return tmpList

    End Function


#End Region


#Region "Nodes Icons"

    Public Shared Sub set_TV_basics_icon_index(ByRef TV As TreeView)

        Dim nodes_keys_list As List(Of int32) = GetNodesKeysList(TV)
        For Each key In nodes_keys_list
            Dim tmp_node = TV.Nodes.Find(key, True)(0)
            If tmp_node.Nodes.Count > 0 Then
                tmp_node.StateImageIndex = 0
                '      tmp_node.SelectedImageIndex = 0
            Else
                tmp_node.StateImageIndex = 1
                '     tmp_node.SelectedImageIndex = 1
            End If
        Next

    End Sub

#End Region


#Region "Position Management"

    Friend Shared Function GeneratePositionsDictionary(ByRef TV As TreeView) As Dictionary(Of Int32, Double)

        Dim positionsDictionary As New Dictionary(Of Int32, Double)
        Dim currentPosition As Int32 = 0
        For Each node As TreeNode In TV.Nodes
            AddNodeToPositionDictionary(node, currentPosition, positionsDictionary)
        Next
        Return positionsDictionary

    End Function

    Friend Shared Sub AddNodeToPositionDictionary(ByRef inputNode As TreeNode, _
                                                 ByRef currentPosition As Int32, _
                                                 ByRef positionsDictionary As Dictionary(Of Int32, Double))

        positionsDictionary.Add(inputNode.Name, currentPosition)
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

    Protected Friend Shared Sub TGV_DragEnter(sender As Object, e As DragEventArgs)

        If e.Data.GetDataPresent("VIBlend.WinForms.DataGridView.HierarchyItem", True) Then
            e.Effect = DragDropEffects.Move                           ' Hierarchy item found allow move effect
        Else
            e.Effect = DragDropEffects.None                           ' No Hierarchy item found, prevent move
        End If
    End Sub

    Protected Friend Shared Sub TGV_DragOver(sender As Object, e As DragEventArgs)

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

    Protected Friend Shared Sub TGV_DragDrop(sender As Object, e As DragEventArgs)

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