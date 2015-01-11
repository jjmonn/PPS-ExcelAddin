﻿' cTreeViews_Functions.vb
'
' Common treeviews functions
'
' To do:
'       -
'       -
'
'
' Known bugs:
'       -
'
'
'
' Last modified: 05/12/2014
' Author: Julien Monnereau


Imports System.Windows.Forms
Imports System.Collections.Generic


Class cTreeViews_Functions

    Public Shared POSITION_STEP_CHILDREN As Double = 0.00000001


#Region "Add nodes to Treeview"

    Public Shared Function AddNode(ByRef TV As TreeView, _
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

    Public Shared Function AddChildNode(ByRef TV As TreeView, _
                                        ByRef token_size As Int32, _
                                        Optional ByRef str As String = "", _
                                        Optional ByRef imageIndex As Int32 = 0, _
                                        Optional ByRef selectedImageIndex As Int32 = 0) As String

        Dim sKey As String = GetNewNodeKey(TV, token_size)

        If Not IsNothing(TV.SelectedNode) Then
            Dim TreeNode1() As TreeNode = TV.Nodes.Find(TV.SelectedNode.Name, True)
            TreeNode1(0).Nodes.Add(sKey, Str, imageIndex, selectedImageIndex)
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

    Public Shared Function GetNewNodeKey(ByRef TV As TreeView, ByRef token_size As Int32) As String

        Dim key As String
        key = IssueNewToken(token_size)
        Do While TV.Nodes.Find(key, True).Length > 0
            key = IssueNewToken(token_size)
        Loop
        Return key

    End Function

    Public Shared Function IssueNewToken(ByRef NbCharacters As Int32) As String

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


#Region "Get Treeview Checked Nodes List"

    Public Shared Function GetCheckedNodeCollection(inputTV As TreeView) As List(Of TreeNode)

        Dim tmpList As New List(Of TreeNode)
        For Each node As TreeNode In inputTV.Nodes
            GetSelectedNodes(node, tmpList)
        Next
        Return tmpList

    End Function

    Public Shared Sub GetSelectedNodes(ByRef inputNode As TreeNode, ByRef tmpList As List(Of TreeNode))

        If inputNode.Checked = True Then tmpList.Add(inputNode)
        For Each childNode As TreeNode In inputNode.Nodes
            GetSelectedNodes(childNode, tmpList)
        Next

    End Sub

#End Region


#Region "Utilities"

    Public Shared Function ReturnRootNodeFromNode(ByRef inputNode As TreeNode) As TreeNode

        Dim currentNode As TreeNode = inputNode
        Do While Not currentNode.Parent Is Nothing
            currentNode = currentNode.Parent
        Loop
        Return currentNode

    End Function

    Public Shared Function IsNameAlreadyInTree(ByRef TV As TreeView, _
                                               ByRef str As String) As Boolean

        For Each node As TreeNode In TV.Nodes
            If IsNameIncludedInHierarchy(node, str) Then Return True
        Next
        Return False

    End Function

    Public Shared Function IsNameIncludedInHierarchy(ByRef node As TreeNode, _
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

    Public Shared Function GetNodeAllChildrenCount(inputNode As TreeNode) As Integer

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

    Public Shared Function GetNodesKeysList(ByRef TV As TreeView) As List(Of String)

        Dim tmpList As New List(Of String)
        For Each node In TV.Nodes
            AddChildrenKeysToList(node, tmpList)
        Next
        Return tmpList

    End Function

    Public Shared Function GetNodesTextsList(ByRef TV As TreeView) As List(Of String)

        Dim tmpList As New List(Of String)
        For Each node In TV.Nodes
            AddChildrenTextsToList(node, tmpList)
        Next
        Return tmpList

    End Function

    Public Shared Function GetNodesKeysList(ByRef node As TreeNode) As List(Of String)

        Dim tmpList As New List(Of String)
        AddChildrenKeysToList(node, tmpList)
        Return tmpList

    End Function

    Public Shared Function GetNodesTextsList(ByRef node As TreeNode) As List(Of String)

        Dim tmpList As New List(Of String)
        AddChildrenTextsToList(node, tmpList)
        Return tmpList

    End Function

    Public Shared Sub AddChildrenKeysToList(ByRef node As TreeNode, tmpList As List(Of String))

        tmpList.Add(node.Name)
        For Each subNode In node.Nodes
            AddChildrenKeysToList(subNode, tmpList)
        Next

    End Sub

    Public Shared Sub AddChildrenTextsToList(ByRef node As TreeNode, tmpList As List(Of String))

        tmpList.Add(node.Text)
        For Each subNode In node.Nodes
            AddChildrenTextsToList(subNode, tmpList)
        Next

    End Sub

    Public Shared Function GetNodeChildrenIDsStringArray(input_node As TreeNode)

        Dim children(input_node.Nodes.Count - 1) As String
        Dim i = 0
        For Each child In input_node.Nodes
            children(i) = child.name
            i = i + 1
        Next
        Return children

    End Function

    Public Shared Function GetNoChildrenNodesList(ByRef allIDsList As List(Of String), ByRef tv As TreeView) As List(Of String)

        Dim tmpList As New List(Of String)
        For Each id In allIDsList
            Dim tmpNode As TreeNode = tv.Nodes.Find(id, True)(0)
            If tmpNode.Nodes.Count = 0 Then tmpList.Add(tmpNode.Name)
        Next
        Return tmpList

    End Function

    Public Shared Function GetChildrenIDList(ByRef node As TreeNode) As List(Of String)

        Dim children_list As New List(Of String)
        For Each child In node.Nodes
            children_list.Add(child.name)
        Next
        Return children_list

    End Function

    Public Shared Function GetUniqueList(ByRef input_list As List(Of String)) As List(Of String)

        Dim tmp_list As New List(Of String)
        For Each item In input_list
            If tmp_list.Contains(item) = False Then tmp_list.Add(item)
        Next
        Return tmp_list

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

    ' Update the position of the children in the accountsAttributes Dictionary
    Protected Friend Shared Sub UpdateChildrenPosition(ByRef inputNode As TreeNode, _
                                             ByRef currentPosition As Double, _
                                             ByRef positionsDictionary As Dictionary(Of String, Double))

        positionsDictionary(inputNode.Name) = currentPosition
        If inputNode.Nodes.Count > 0 Then
            For Each child As TreeNode In inputNode.Nodes
                currentPosition = currentPosition + POSITION_STEP_CHILDREN
                UpdateChildrenPosition(child, currentPosition, positionsDictionary)
            Next
        End If

    End Sub

#End Region



End Class
