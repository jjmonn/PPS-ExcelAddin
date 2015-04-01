' TreeViewsUtilities.vb
'
' Common treeviews functions
'
'
' Last modified: 26/02/2015
' Author: Julien Monnereau


Imports System.Windows.Forms
Imports System.Collections.Generic


Friend Class TreeViewsUtilities


#Region "Add nodes to Treeview"

    Protected Friend Shared Function AddNode(ByRef TV As TreeView, _
                                   ByRef token_size As Int32, _
                                   Optional ByRef nodeText As String = "", _
                                   Optional ByRef imageIndex As Int32 = 0,
                                   Optional ByRef selectedImageIndex As Int32 = 0) As String

        Dim sKey As String = GetNewNodeKey(TV, token_size)

        If Not IsNothing(TV.SelectedNode.Parent) Then
            Dim TreeNode1() As TreeNode = TV.Nodes.Find(TV.SelectedNode.Parent.Name, True)
            TreeNode1(0).Nodes.Add(sKey, nodeText, imageIndex, selectedImageIndex)
            With TreeNode1(0).Nodes(sKey)
                .Tag = True
                .Checked = True
                .EnsureVisible()
            End With
            TV.SelectedNode = TreeNode1(0).Nodes(sKey)
        Else
            TV.Nodes.Add(sKey, nodeText, imageIndex, selectedImageIndex)       'Add a node at the end if nothing was selected
            With TV.Nodes(sKey)
                .Tag = True
                .Checked = True
                .EnsureVisible()
            End With
            TV.SelectedNode = TV.Nodes(sKey)
        End If

        Return sKey

    End Function

    Protected Friend Shared Function AddChildNode(ByRef TV As TreeView, _
                                        ByRef token_size As Int32, _
                                        Optional ByRef str As String = "", _
                                        Optional ByRef imageIndex As Int32 = 0, _
                                        Optional ByRef selectedImageIndex As Int32 = 0) As String

        Dim sKey As String = GetNewNodeKey(TV, token_size)

        If Not IsNothing(TV.SelectedNode) Then
            Dim TreeNode1() As TreeNode = TV.Nodes.Find(TV.SelectedNode.Name, True)
            TreeNode1(0).Nodes.Add(sKey, str, imageIndex, selectedImageIndex)
            With TreeNode1(0).Nodes(sKey)
                .Tag = True
                .Checked = True
                .EnsureVisible()
            End With
            TV.SelectedNode = TreeNode1(0).Nodes(sKey)
        Else
            MsgBox("A node must be selected in order to add a child")
        End If

        Return sKey

    End Function

    Protected Friend Shared Function GetNewNodeKey(ByRef TV As TreeView, ByRef token_size As Int32) As String

        Dim key As String
        key = IssueNewToken(token_size)
        Do While TV.Nodes.Find(key, True).Length > 0
            key = IssueNewToken(token_size)
        Loop
        Return key

    End Function

    Protected Friend Shared Function IssueNewToken(ByRef NbCharacters As Int32) As String

        Dim token As String = ""

        For i = 0 To NbCharacters - 1
            Randomize()
            Dim randomValue As Integer = CInt(Math.Floor((ANSII_CEILING_TOKEN_CHAR - ANSII_FLOOR_TOKEN_CHAR + 1) * Rnd())) + ANSII_FLOOR_TOKEN_CHAR
            token = token + Chr(randomValue)
        Next i

        Return token

    End Function

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

    Protected Friend Shared Sub FilterSelectedNodes(ByRef entity_node As TreeNode, _
                                                    ByRef entities_list As List(Of String))

        Dim tmp_array(entities_list.Count - 1) As String
        entities_list.CopyTo(tmp_array)
        For Each entity_id As String In tmp_array
            If entity_node.TreeView.Nodes.Find(entity_id, True)(0).Checked = False Then entities_list.Remove(entity_id)
        Next

    End Sub

    Protected Friend Shared Function GetCheckedNodeCollection(inputTV As TreeView) As List(Of TreeNode)

        Dim tmpList As New List(Of TreeNode)
        For Each node As TreeNode In inputTV.Nodes
            GetSelectedNodes(node, tmpList)
        Next
        Return tmpList

    End Function

    Protected Friend Shared Sub GetSelectedNodes(ByRef inputNode As TreeNode, ByRef tmpList As List(Of TreeNode))

        If inputNode.Checked = True Then tmpList.Add(inputNode)
        For Each childNode As TreeNode In inputNode.Nodes
            GetSelectedNodes(childNode, tmpList)
        Next

    End Sub

    Protected Friend Shared Function GetCheckedNodesID(ByRef TV As TreeView) As List(Of String)

        Dim tmpList As New List(Of String)
        For Each childNode As TreeNode In TV.Nodes
            If childNode.Checked = True Then tmpList.Add(childNode.Name)
        Next
        Return tmpList

    End Function

    Protected Friend Shared Function GetCheckedNodesID(ByRef node As TreeNode) As List(Of String)

        Dim tmpList As New List(Of String)
        For Each childNode As TreeNode In node.Nodes
            If childNode.Checked = True Then tmpList.Add(childNode.Name)
        Next
        Return tmpList

    End Function

#End Region


#Region "Nodes Lists"

    Protected Friend Shared Function GetNodeAllChildrenCount(inputNode As TreeNode) As Integer

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

    Protected Friend Shared Function GetNodesKeysList(ByRef TV As TreeView) As List(Of String)

        Dim tmpList As New List(Of String)
        For Each node In TV.Nodes
            AddChildrenKeysToList(node, tmpList)
        Next
        Return tmpList

    End Function

    Protected Friend Shared Function GetNodesTextsList(ByRef TV As TreeView) As List(Of String)

        Dim tmpList As New List(Of String)
        For Each node In TV.Nodes
            AddChildrenTextsToList(node, tmpList)
        Next
        Return tmpList

    End Function

    Protected Friend Shared Function GetNodesKeysList(ByRef node As TreeNode) As List(Of String)

        Dim tmpList As New List(Of String)
        AddChildrenKeysToList(node, tmpList)
        Return tmpList

    End Function

    Protected Friend Shared Function GetNodesTextsList(ByRef node As TreeNode) As List(Of String)

        Dim tmpList As New List(Of String)
        AddChildrenTextsToList(node, tmpList)
        Return tmpList

    End Function

    Protected Friend Shared Function GetNodesList(ByRef node As TreeNode) As List(Of TreeNode)

        Dim tmp_list As New List(Of TreeNode)
        AddChildrenNodesToList(node, tmp_list)
        tmp_list.Remove(node)
        Return tmp_list

    End Function

    Protected Friend Shared Sub AddChildrenKeysToList(ByRef node As TreeNode, tmpList As List(Of String))

        tmpList.Add(node.Name)
        For Each subNode In node.Nodes
            AddChildrenKeysToList(subNode, tmpList)
        Next

    End Sub

    Protected Friend Shared Sub AddChildrenNodesToList(ByRef node As TreeNode, tmpList As List(Of TreeNode))

        tmpList.Add(node)
        For Each subNode In node.Nodes
            AddChildrenNodesToList(subNode, tmpList)
        Next

    End Sub

    Protected Friend Shared Sub AddChildrenTextsToList(ByRef node As TreeNode, tmpList As List(Of String))

        tmpList.Add(node.Text)
        For Each subNode In node.Nodes
            AddChildrenTextsToList(subNode, tmpList)
        Next

    End Sub

    Protected Friend Shared Function GetNodeChildrenIDsStringArray(ByRef input_node As TreeNode, _
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

    Protected Friend Shared Function GetNoChildrenNodesList(ByRef allIDsList As List(Of String), ByRef tv As TreeView) As List(Of String)

        Dim tmpList As New List(Of String)
        For Each id In allIDsList
            Dim tmpNode As TreeNode = tv.Nodes.Find(id, True)(0)
            If tmpNode.Nodes.Count = 0 Then tmpList.Add(tmpNode.Name)
        Next
        Return tmpList

    End Function

    Protected Friend Shared Function GetChildrenIDList(ByRef node As TreeNode) As List(Of String)

        Dim children_list As New List(Of String)
        For Each child In node.Nodes
            children_list.Add(child.name)
        Next
        Return children_list

    End Function

    Protected Friend Shared Function GetUniqueList(ByRef input_list As List(Of String)) As List(Of String)

        Dim tmp_list As New List(Of String)
        For Each item In input_list
            If tmp_list.Contains(item) = False Then tmp_list.Add(item)
        Next
        Return tmp_list

    End Function

#End Region


#Region "Utilities"

    Protected Friend Shared Function ReturnRootNodeFromNode(ByRef inputNode As TreeNode) As TreeNode

        Dim currentNode As TreeNode = inputNode
        Do While Not currentNode.Parent Is Nothing
            currentNode = currentNode.Parent
        Loop
        Return currentNode

    End Function

    Protected Friend Shared Function IsNameAlreadyInTree(ByRef TV As TreeView, _
                                               ByRef str As String) As Boolean

        For Each node As TreeNode In TV.Nodes
            If IsNameIncludedInHierarchy(node, str) Then Return True
        Next
        Return False

    End Function

    Protected Friend Shared Function IsNameIncludedInHierarchy(ByRef node As TreeNode, _
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

    Protected Friend Shared Function GetNodePath(ByRef node As TreeNode, _
                                                 Optional ByRef max_hierarchy_id As String = "") As List(Of Int32)

        Dim path_list As New List(Of Int32)
        While Not node.Parent Is Nothing And node.Name <> max_hierarchy_id
            path_list.Add(node.Index)
            node = node.Parent
        End While
        path_list.Reverse()
        Return path_list

    End Function

    Protected Friend Shared Function GetHighestHierarchyLevelFromList(ByRef ids_list As List(Of String), _
                                                                      ByRef TV As TreeView) As String

        Dim all_nodes_list As List(Of String) = GetNodesKeysList(TV)
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

        Dim nodes_keys_list As List(Of String) = GetNodesKeysList(TV)
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

    Protected Friend Shared Function GeneratePositionsDictionary(ByRef TV As TreeView) As Dictionary(Of String, Double)

        Dim positionsDictionary As New Dictionary(Of String, Double)
        Dim currentPosition As Int32 = 0
        For Each node As TreeNode In TV.Nodes
            AddNodeToPositionDictionary(node, currentPosition, positionsDictionary)
        Next
        Return positionsDictionary

    End Function

    Protected Friend Shared Sub AddNodeToPositionDictionary(ByRef inputNode As TreeNode, _
                                                  ByRef currentPosition As Int32, _
                                                  ByRef positionsDictionary As Dictionary(Of String, Double))

        positionsDictionary.Add(inputNode.Name, currentPosition)
        currentPosition = currentPosition + 1
        For Each subNode As TreeNode In inputNode.Nodes
            AddNodeToPositionDictionary(subNode, currentPosition, positionsDictionary)
        Next

    End Sub



#Region "Move nodes up and down into hierarchy Procedure"

    Protected Friend Shared Sub MoveNodeUp(ByRef inputNode As TreeNode)

        Try
            If Not (inputNode.PrevNode Is Nothing) Then
                Dim prev_node = inputNode.PrevNode
                inputNode.Parent.Nodes.Insert(inputNode.Index - 1, CType(inputNode.Clone, TreeNode))
                inputNode.Remove()
                inputNode.TreeView.SelectedNode = prev_node.PrevNode
            End If
        Catch ex As Exception
        End Try

    End Sub

    Protected Friend Shared Sub MoveNodeDown(ByRef inputNode As TreeNode)

        Try
            If Not (inputNode.NextNode Is Nothing) Then
                Dim nextnode = inputNode.NextNode
                inputNode.Parent.Nodes.Insert(inputNode.Index + 2, CType(inputNode.Clone, TreeNode))
                inputNode.Remove()
                inputNode.TreeView.SelectedNode = nextnode.NextNode
            End If
        Catch ex As Exception
        End Try

    End Sub

#End Region


#End Region





End Class
