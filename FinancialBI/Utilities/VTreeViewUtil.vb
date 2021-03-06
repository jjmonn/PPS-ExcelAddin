﻿Imports VIBlend.WinForms.Controls
Imports System.Drawing
Imports VIBlend.Utilities
Imports System.Collections.Generic
Imports CRUD

Public Class VTreeViewUtil

    Public Shared Function AddNode(ByVal value As String, _
                                   ByVal text As String, _
                                   Optional ByRef parent As Object = Nothing, _
                                   Optional ByRef imageIndex As Int16 = -1) As vTreeNode

        Dim newNode As New vTreeNode()
        newNode.Text = text
        newNode.Value = value
        parent.Nodes.add(newNode)
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
        TV.UseThemeBackColor = False
        TV.BackColor = Color.White

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

        Dim childrenNodesList As List(Of vTreeNode) = GetAllChildrenNodesList(node)
        If includeSelf = True Then
            childrenNodesList.Add(node)
        End If
        For Each subNode As vTreeNode In childrenNodesList
            If subNode.Value = value Then Return subNode
        Next
        Return Nothing

    End Function

    Public Shared Function GetNodesIds(ByRef TV As vTreeView) As List(Of Int32)

        Dim tmpList As New List(Of Int32)
        For Each node As vTreeNode In TV.Nodes
            If tmpList.Contains(CInt(node.Value)) = False Then tmpList.Add(CInt(node.Value))
            For Each l_accountId As Int32 In GetNodesIds(node)
                If tmpList.Contains(l_accountId) = False Then tmpList.Add(l_accountId)
            Next
        Next
        Return tmpList

    End Function

    Public Shared Function GetNodesIds(ByRef node As vTreeNode) As List(Of Int32)

        Dim tmpList As New List(Of Int32)
        For Each subNode As vTreeNode In GetAllChildrenNodesList(node)
            If tmpList.Contains(CInt(subNode.Value)) = False Then tmpList.Add(CInt(subNode.Value))
        Next
        Return tmpList

    End Function

    Public Shared Function GetNodesIdsUint(ByRef node As vTreeNode) As List(Of UInt32)

        Dim tmpList As New List(Of UInt32)
        For Each subNode As vTreeNode In GetAllChildrenNodesList(node)
            If tmpList.Contains(CInt(subNode.Value)) = False Then tmpList.Add(CUInt(subNode.Value))
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
            FillChildrenNodesList(subNode, list)
        Next

    End Sub

    Public Shared Sub CopySubNodes(ByRef or_node As vTreeNode, _
                                   ByRef des_node As Object)

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
            If node.Checked = Windows.Forms.CheckState.Checked Then tmpList.Add(CInt(node.Value))
        Next
        Return tmpList

    End Function

    Public Shared Sub CheckStateAllNodes(ByRef TV As vTreeView, ByRef state As Boolean)

        For Each node As vTreeNode In TV.GetNodes
            If state = True Then
                node.Checked = Windows.Forms.CheckState.Checked
            Else
                node.Checked = Windows.Forms.CheckState.Unchecked
            End If
        Next

    End Sub

    Public Shared Function ReturnRootNodeFromNode(ByRef inputNode As vTreeNode) As vTreeNode

        Dim currentNode As vTreeNode = inputNode
        Do While Not currentNode.Parent Is Nothing
            currentNode = currentNode.Parent
        Loop
        Return currentNode

    End Function

#Region "TV loading"

    Private Shared Function GenerateTreeNode(Of T As {NamedHierarchyCRUDEntity}) _
        (ByRef p_node As vTreeNode, ByRef p_topItemId As UInt32, ByRef items_attributes As MultiIndexDictionary(Of UInt32, String, T)) As Boolean
        Dim currentItem As NamedHierarchyCRUDEntity = items_attributes(p_topItemId)
        If currentItem Is Nothing Then Return False

        p_node.Value = CStr(currentItem.Id)
        p_node.Text = currentItem.Name
        For Each item As NamedHierarchyCRUDEntity In items_attributes.SortedValues
            If item.ParentId = currentItem.Id Then
                Dim childNode As New vTreeNode
                If GenerateTreeNode(childNode, item.Id, items_attributes) = False Then Return False
                p_node.Nodes.Add(childNode)
            End If
        Next
        Return True
    End Function

    Public Shared Function LoadTreeview(Of T As {NamedHierarchyCRUDEntity})(ByRef TV As vTreeView, _
                                        ByRef items_attributes As MultiIndexDictionary(Of UInt32, String, T)) As Boolean
        For Each item As NamedHierarchyCRUDEntity In items_attributes.SortedValues
            If item.ParentId = 0 Then
                Dim currentNode As New vTreeNode

                If (GenerateTreeNode(currentNode, item.Id, items_attributes) = True) Then TV.Nodes.Add(currentNode)
            End If
        Next
        Return True

    End Function

    Public Shared Function LoadTreenode(Of T As {NamedHierarchyCRUDEntity})(ByRef node As vTreeNode, _
                                        ByRef items_attributes As MultiIndexDictionary(Of UInt32, String, T)) As Boolean

        For Each item As NamedHierarchyCRUDEntity In items_attributes.SortedValues
            If item.ParentId = 0 Then
                Dim currentNode As New vTreeNode

                If (GenerateTreeNode(currentNode, item.Id, items_attributes) = True) Then node.Nodes.Add(currentNode)
            End If
        Next
        Return True
 
    End Function

    Public Shared Sub LoadFlatTreeview(Of T As {NamedCRUDEntity})(ByRef p_treeview As vTreeView, _
                                      ByRef p_itemsAttributes As MultiIndexDictionary(Of UInt32, String, T))

        p_treeview.Nodes.Clear()
        For Each value As T In p_itemsAttributes.SortedValues
            Dim l_node As vTreeNode = AddNode(value.Id, value.Name, p_treeview)
            l_node.Checked = Windows.Forms.CheckState.Checked
        Next

    End Sub

    Public Shared Sub LoadTreeviewIcons(Of T As {CRUDEntity})(ByRef p_treeview As vTreeView, _
                                        ByRef p_itemsAttributes As MultiIndexDictionary(Of UInt32, String, T))

        For Each l_node In p_treeview.GetNodes
            Dim l_attributes As T = p_itemsAttributes(CUInt(l_node.Value))
            If l_attributes IsNot Nothing Then
                l_node.ImageIndex = l_attributes.Image
            End If
        Next

    End Sub

    Public Shared Sub SetTreeviewIconsHierarchy(ByRef p_treeview As vTreeView)

        For Each l_node As vTreeNode In p_treeview.GetNodes
            If l_node.Nodes.Count > 0 Then
                l_node.ImageIndex = 0
            Else
                l_node.ImageIndex = 1
            End If
        Next

    End Sub

    Public Shared Function GetEntitiesImageList() As Windows.Forms.ImageList

        Dim l_imageList As New Windows.Forms.ImageList
        l_imageList.ColorDepth = Windows.Forms.ColorDepth.Depth32Bit
        l_imageList.ImageSize = New Size(16, 16)
        l_imageList.Images.Add(My.Resources.favicon_81_)
        l_imageList.Images.Add(My.Resources.element_branch25)

        Return l_imageList

    End Function

#End Region


    Public Shared Function GeneratePositionsDictionary(ByRef p_treeview As vTreeView) As Dictionary(Of Int32, Int32)

        Dim positionsDictionary As New SafeDictionary(Of Int32, Int32)
        Dim currentPosition As Int32 = 0
        For Each l_value As Int32 In GetNodesIds(p_treeview)
            positionsDictionary.Add(l_value, currentPosition)
            currentPosition += 1
        Next
        Return positionsDictionary

    End Function


#Region "Move nodes up and down into hierarchy Procedure"

    Friend Shared Sub MoveNodeUp(ByRef p_node As vTreeNode)

        SwapNode(p_node, p_node.PrevSiblingNode)
        p_node.TreeView.Refresh()

    End Sub

    Friend Shared Sub MoveNodeDown(ByRef p_node As vTreeNode)

        SwapNode(p_node, p_node.NextSiblingNode)
        p_node.TreeView.Refresh()

    End Sub

    Friend Shared Sub SwapNode(ByRef p_first As vTreeNode, ByRef p_second As vTreeNode)
        If p_second Is Nothing OrElse p_first Is Nothing Then Exit Sub

        Dim parentNode = If(p_first.Parent Is Nothing, p_first.TreeView, p_first.Parent)

        Dim indexOrigin As Int32 = parentNode.Nodes.IndexOf(p_first)
        Dim indexNext As Int32 = parentNode.Nodes.IndexOf(p_second)

        parentNode.Nodes.RemoveAt(indexOrigin)
        parentNode.Nodes.Insert(indexOrigin, p_second)
        parentNode.Nodes.RemoveAt(indexNext)
        parentNode.Nodes.Insert(indexNext, p_first)
    End Sub

    Private Shared Function GetNodesNamesValueDict(ByRef p_treeview As vTreeView) As SafeDictionary(Of String, String)

        Dim l_namesValuesDict As New SafeDictionary(Of String, String)
        For Each l_node As vTreeNode In p_treeview.GetNodes
            If l_namesValuesDict.ContainsKey(l_node.Text) = False Then
                l_namesValuesDict.Add(l_node.Text, l_node.Value)
            End If
        Next
        Return l_namesValuesDict

    End Function

    Private Shared Sub PutBackNodesValues(ByRef p_treeview As vTreeView, _
                                   ByRef p_namesValuesDict As SafeDictionary(Of String, String))

        For Each l_node As vTreeNode In p_treeview.GetNodes
            l_node.Value = p_namesValuesDict(l_node.Text)
        Next

    End Sub

#End Region


#Region "VtreeviewBox Loading"

    Friend Shared Sub LoadParentsTreeviewBox(ByRef TVBox As vTreeViewBox, _
                                                    ByRef originalTV As vTreeView)

        TVBox.TreeView.Nodes.Clear()
        For Each node As vTreeNode In originalTV.Nodes
            AddNodeToParentEntityTreeviewBox(TVBox, node, Nothing)
        Next

    End Sub

    Friend Shared Sub LoadParentsTreeviewBox(ByRef TVBox As vTreeViewBox, _
                                                    ByRef originalNode As vTreeNode)

        TVBox.TreeView.Nodes.Clear()
        For Each node As vTreeNode In originalNode.Nodes
            AddNodeToParentEntityTreeviewBox(TVBox, node, Nothing)
        Next

    End Sub

    Private Shared Sub AddNodeToParentEntityTreeviewBox(ByRef TVBox As vTreeViewBox, _
                                                        ByRef originNode As vTreeNode, _
                                                        ByRef destinationNode As VIBlend.WinForms.Controls.vTreeNode)

        Dim newNode As New VIBlend.WinForms.Controls.vTreeNode
        newNode.Value = originNode.Value
        newNode.Text = originNode.Text
        If destinationNode Is Nothing Then
            TVBox.TreeView.Nodes.Add(newNode)
        Else
            destinationNode.Nodes.Add(newNode)
        End If
        For Each subNode As vTreeNode In originNode.Nodes
            AddNodeToParentEntityTreeviewBox(TVBox, subNode, newNode)
        Next

    End Sub

#End Region


#Region "CheckStates"

    Public Shared Function SaveCheckedStates(ByRef TV As vTreeView) As List(Of String)

        Dim checkedList As New List(Of String)
        For Each currNode As vTreeNode In TV.Nodes
            SaveChildrenCheckedState(currNode, checkedList)
        Next
        Return checkedList

    End Function

    Public Shared Sub SaveChildrenCheckedState(ByRef currNode As vTreeNode, _
                                               ByRef checkedList As List(Of String))

        If currNode.Checked = True Then checkedList.Add(currNode.Value)
        For Each child As vTreeNode In currNode.Nodes
            SaveChildrenCheckedState(child, checkedList)
        Next

    End Sub

    Public Shared Function ResumeCheckedStates(ByRef TV As vTreeView, ByRef checkedList As List(Of String)) As List(Of String)

        Dim tmpList As New List(Of String)
        For Each key In checkedList
            Dim tmpNode = VTreeViewUtil.FindNode(TV, key)
            If Not tmpNode Is Nothing Then
                tmpNode.Checked = True
            Else
                tmpList.Add(key)
            End If
        Next
        Return tmpList

    End Function


#End Region


#Region "Expansions Levels"

    Public Shared Function SaveNodesExpansionsLevel(ByVal TV As vTreeView) As Dictionary(Of String, Boolean)

        Dim expansionDic As New Collections.Generic.Dictionary(Of String, Boolean)
        For Each currNode As vTreeNode In TV.Nodes
            SaveChildrenExpansionsLevel(currNode, expansionDic)
        Next
        Return expansionDic

    End Function

    Public Shared Sub SaveChildrenExpansionsLevel(ByRef currNode As vTreeNode, _
                                                  ByRef expansionDic As Dictionary(Of String, Boolean))

        If expansionDic.ContainsKey(currNode.Value) Then expansionDic(currNode.Value) = currNode.IsExpanded Else expansionDic.Add(currNode.Value, currNode.IsExpanded)
        If currNode.Nodes.Count > 0 Then
            For Each child As vTreeNode In currNode.Nodes
                SaveChildrenExpansionsLevel(child, expansionDic)
            Next
        End If

    End Sub

    Public Shared Sub ResumeExpansionsLevel(ByRef TV As vTreeView, _
                                            ByRef expansionDictionary As Dictionary(Of String, Boolean))

        TV.CollapseAll()
        For Each key In expansionDictionary.Keys
            If expansionDictionary(key) = True Then
                Dim tmpNode = VTreeViewUtil.FindNode(TV, key)
                If Not tmpNode Is Nothing Then tmpNode.Expand()
            End If
        Next

    End Sub

#End Region

    Public Shared Function GetNodeAtPosition(ByRef p_treeview As vTreeView, p_position As Drawing.Point) As vTreeNode

        p_position.Y -= p_treeview.ScrollPosition.Y
        p_position.X -= p_treeview.ScrollPosition.X
        Dim l_node As vTreeNode = p_treeview.HitTest(p_position)
        If l_node IsNot Nothing Then
            Return l_node
        Else
            Return Nothing
        End If

    End Function

End Class
