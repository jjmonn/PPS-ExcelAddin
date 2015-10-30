Imports VIBlend.WinForms.Controls
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


#Region "TV loading"

    Public Shared Sub LoadTreeview(Of T As {NamedHierarchyCRUDEntity})(ByRef TV As vTreeView, _
                                ByRef items_attributes As MultiIndexDictionary(Of UInt32, String, T))

        Dim currentNode, ParentNode As vTreeNode
        Dim orphans_ids_list As New List(Of Int32)
        Dim image_index As UInt16 = 0
        TV.Nodes.Clear()

        For Each id As UInt32 In items_attributes.Keys
            If items_attributes(id).ParentId = 0 Then
                currentNode = AddNode(items_attributes(id).Id, _
                                      items_attributes(id).Name,
                                      TV, _
                                      image_index)
            Else
                ParentNode = FindNode(TV, items_attributes(id).Id)
                If Not ParentNode Is Nothing Then
                    currentNode = AddNode(items_attributes(id).Id, _
                                        items_attributes(id).Name,
                                        ParentNode, _
                                        image_index)
                Else
                    orphans_ids_list.Add(items_attributes(id).Id)
                End If
            End If
        Next
        If orphans_ids_list.Count > 0 Then SolveOrphanNodesList(TV, items_attributes, orphans_ids_list)

    End Sub

    Private Shared Sub SolveOrphanNodesList(Of T As {NamedHierarchyCRUDEntity})(ByRef TV As vTreeView, _
                                          ByRef items_attributes As MultiIndexDictionary(Of UInt32, String, T), _
                                          ByRef orphans_id_list As List(Of Int32), _
                                          Optional ByRef solved_orphans_list As List(Of Int32) = Nothing)

        Dim current_node, parent_node As vTreeNode
        Dim image_index As UInt16 = 0
        If solved_orphans_list Is Nothing Then solved_orphans_list = New List(Of Int32)
        For Each orphan_id As UInt32 In orphans_id_list
            If solved_orphans_list.Contains(orphan_id) = False Then
                parent_node = FindNode(TV, items_attributes(orphan_id).ParentId)

                If Not parent_node Is Nothing Then
                    current_node = AddNode(items_attributes(orphan_id).Id, _
                                           items_attributes(orphan_id).Name,
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

    Public Shared Sub LoadTreenode(Of T As {NamedHierarchyCRUDEntity})(ByRef node As vTreeNode, _
                                   ByRef items_attributes As MultiIndexDictionary(Of UInt32, String, T))

        Dim currentNode, ParentNode As vTreeNode
        Dim orphans_ids_list As New List(Of Int32)
        Dim image_index As UInt16 = 0
        node.Nodes.Clear()

        For Each id As UInt32 In items_attributes.Keys
            If items_attributes(id).ParentId = 0 Then
                currentNode = AddNode(items_attributes(id).Id, _
                                       items_attributes(id).Name, _
                                       node, _
                                       image_index)
            Else
                ParentNode = FindNode(node, items_attributes(id).Id)
                If Not ParentNode Is Nothing Then
                    currentNode = AddNode(items_attributes(id).Id, _
                                        items_attributes(id).Name,
                                        ParentNode, _
                                        image_index)
                Else
                    orphans_ids_list.Add(items_attributes(id).Id)
                End If
            End If
        Next
        If orphans_ids_list.Count > 0 Then SolveOrphanNodesList(node, items_attributes, orphans_ids_list)

    End Sub

    Private Shared Sub SolveOrphanNodesList(Of T As {NamedHierarchyCRUDEntity})(ByRef node As vTreeNode, _
                                          ByRef items_attributes As MultiIndexDictionary(Of UInt32, String, T), _
                                          ByRef orphans_id_list As List(Of Int32), _
                                          Optional ByRef solved_orphans_list As List(Of Int32) = Nothing)

        Dim current_node, parent_node As vTreeNode
        Dim image_index As UInt16 = 0
        If solved_orphans_list Is Nothing Then solved_orphans_list = New List(Of Int32)
        For Each orphan_id As UInt32 In orphans_id_list
            If solved_orphans_list.Contains(orphan_id) = False Then
                parent_node = FindNode(node, items_attributes(orphan_id).ParentId)

                If Not parent_node Is Nothing Then
                    current_node = AddNode(items_attributes(orphan_id).Id, _
                                           items_attributes(orphan_id).Name,
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


    Public Shared Function GeneratePositionsDictionary(ByRef TV As vTreeView) As Dictionary(Of Int32, Int32)

        Dim positionsDictionary As New Dictionary(Of Int32, Int32)
        Dim currentPosition As Int32 = 0
        For Each node As vTreeNode In TV.GetNodes
            positionsDictionary.Add(node.Value, currentPosition)
            currentPosition += 1
        Next
        Return positionsDictionary

    End Function


#Region "Move nodes up and down into hierarchy Procedure"

    Friend Shared Sub MoveNodeUp(ByRef inputNode As vTreeNode)

        ' below to be checked priority normal
        Dim index = inputNode.TreeView.Nodes.IndexOf(inputNode)
        Try
            If Not (inputNode.PrevNode Is Nothing) Then
                Dim prev_node = inputNode.PrevNode
                If inputNode.Parent Is Nothing Then
                    inputNode.TreeView.Nodes.Insert(index - 1, CType(inputNode.Clone, vTreeNode))
                Else
                    inputNode.Parent.Nodes.Insert(index - 1, CType(inputNode.Clone, vTreeNode))
                End If
                inputNode.Remove()
                prev_node.TreeView.SelectedNode = prev_node.PrevNode
            End If
        Catch ex As Exception
        End Try

    End Sub

    Friend Shared Sub MoveNodeDown(ByRef inputNode As vTreeNode)

        ' below to be checked priority normal
        Dim index = inputNode.TreeView.Nodes.IndexOf(inputNode)
        Try
            If Not (inputNode.NextNode Is Nothing) Then
                Dim nextnode = inputNode.NextNode
                If inputNode.Parent Is Nothing Then
                    inputNode.TreeView.Nodes.Insert(index + 2, CType(inputNode.Clone, vTreeNode))
                Else
                    inputNode.Parent.Nodes.Insert(index + 2, CType(inputNode.Clone, vTreeNode))
                End If
                inputNode.Remove()
                nextnode.TreeView.SelectedNode = nextnode.NextNode
            End If
        Catch ex As Exception
        End Try

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


End Class
