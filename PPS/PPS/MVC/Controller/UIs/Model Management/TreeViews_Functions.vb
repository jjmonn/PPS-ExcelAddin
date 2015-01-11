Module TreeViews_Functions


    Public Sub UFInit(uf As Windows.Forms.Form)

        '---------------------------------------------------------------------
        ' Sub: UFInit
        ' Form Initialize common procedures
        '---------------------------------------------------------------------

        'uf.frmImageBox.Visible = False                                                      ' Hide the Image container
        'uf.frmImageBox.Enabled = False
        'uf.BorderStyle = fmBorderStyleNone
    End Sub


    Public Sub loadImages(uf As Windows.Forms.Form)
        '---------------------------------------------------------------------
        ' Sub: LoadImages
        ' Load images into Image list
        '---------------------------------------------------------------------

        ' To be done if possible !!
    End Sub


    '*****************************************************************************
    ' TREEVIEWS COMMON PROCEDURES
    '*****************************************************************************

    Public Sub TVInit(TV As Windows.Forms.TreeView)

        '-----------------------------------------------------------------------
        ' Initialize a treeview
        '-----------------------------------------------------------------------

        TV.CheckBoxes = True
        TV.AllowDrop = True
        TV.LabelEdit = True

    End Sub


    Public Function GetNextChildKey(TV As Windows.Forms.TreeView, mapping As String) As String
        '-----------------------------------------------------------------------
        ' Function: GetNextChildKey
        ' Issues a key for a new child
        '-----------------------------------------------------------------------

        ' Not needed for now
        ' Keys handled manually - generated in another way, espacially for childs creation
        GetNextChildKey = InputBox("Enter the key for the new child")
    End Function


    Public Function GetNextKey(TV As Windows.Forms.TreeView, mapping As String) As String

        '-----------------------------------------------------------------------
        ' Function: GetNextKey
        ' Generates the next key for the current treeview
        '-----------------------------------------------------------------------

        GetNextKey = InputBox("Enter the new item key")

        '        Dim sNewKey As String
        '        Dim iHold As Integer               'Will hold the index of the new node
        '        Dim mapp As ModelMapping
        '        Dim parentKey As String
        '        Dim i As Integer

        '        On Error GoTo errorHandler1
        '        iHold = Val(TV.Nodes(1).key)                      'Generates an error if no node in the tree

        '        On Error GoTo errorHandler2
        '        iHold = TV.SelectedItem.parent.Children + 1         'Count number of brothers
        '        parentKey = TV.SelectedItem.parent.key
        '        sNewKey = parentKey & iHold & "."

        '        mapp = New ModelMapping
        '        mapp.loadMappingRST()
        '        ' check whether the key is already existing
        '        While mapp.isKeyInMapping(mapping, sNewKey)
        '            iHold = iHold + 1
        '            sNewKey = parentKey & iHold + 1 & "."
        '        End While

        '        GetNextKey = sNewKey
        '        mapp = Nothing
        '        Exit Function

        'errorHandler1:
        '        GetNextKey = "1."     ' Case no nodes
        '        Exit Function

        'errorHandler2:  ' Case root nodes
        '        If Not TV.DropHighlight Is Nothing Then
        '            iHold = Val(TV.DropHighlight.LastSibling.key) + 1
        '        Else
        '            iHold = Val(TV.SelectedItem.LastSibling.key) + 1
        '        End If
        '        sNewKey = iHold & "."
        '        mapp = New ModelMapping
        '        mapp.loadMappingRST()
        '        While mapp.isKeyInMapping(mapping, sNewKey)
        '            iHold = iHold + 1
        '            sNewKey = parentKey & iHold + 1 & "."
        '        End While
        '        GetNextKey = sNewKey
        '        mapp = Nothing
        '        Exit Function

    End Function


    Public Sub addNode(TV As Windows.Forms.TreeView, mapping As String, Optional str As String = "")

        '------------------------------------------------------------------------------------
        ' Sub: Add a Node to a treeview
        '------------------------------------------------------------------------------------

        Dim sKey As String
        sKey = GetNextKey(TV, mapping)

        On Error GoTo ErrorHandler
        Dim TreeNode1() As Windows.Forms.TreeNode = TV.Nodes.Find(TV.SelectedNode.Parent.Name, True)
        TreeNode1(0).Nodes.Add(sKey, str)
        With TreeNode1(0).Nodes(sKey)
            .Tag = True
            .Checked = True
            .EnsureVisible()
        End With
        TV.SelectedNode = TreeNode1(0).Nodes(sKey)
        Exit Sub

ErrorHandler:
        TV.Nodes.Add(sKey, str)       'Add a node at the end if nothing was selected
        With TV.Nodes(sKey)
            .Tag = True
            .Checked = True
            .EnsureVisible()
        End With
        TV.SelectedNode = TV.Nodes(sKey)
        Exit Sub
    End Sub


    Public Sub addChild(TV As Windows.Forms.TreeView, mapping As String, Optional str As String = "")

        '------------------------------------------------------------------------------------
        ' Sub: Add a Node to a treeview
        '------------------------------------------------------------------------------------

        Dim sKey As String
        sKey = GetNextKey(TV, mapping)

        On Error GoTo ErrorHandler
        Dim TreeNode1() As Windows.Forms.TreeNode = TV.Nodes.Find(TV.SelectedNode.Name, True)
        TreeNode1(0).Nodes.Add(sKey, Str)
        With TreeNode1(0).Nodes(sKey)
            .Tag = True
            .Checked = True
            .EnsureVisible()
        End With
        TV.SelectedNode = TreeNode1(0).Nodes(sKey)
        Exit Sub

ErrorHandler:
        Exit Sub
    End Sub


    Public Sub CheckAllChildNodes(TV As Windows.Forms.TreeView, nodeSelected As Windows.Forms.TreeNode, NodeChecked As Boolean)

        '--------------------------------------------------------------------------------------------
        ' CheckAllChildNodes
        ' Updates all child tree nodes recursively
        '--------------------------------------------------------------------------------------------

        For Each childNode As Windows.Forms.TreeNode In nodeSelected.Nodes
            childNode.Checked = NodeChecked
            childNode.Tag = NodeChecked
            If childNode.Nodes.Count > 0 Then CheckAllChildNodes(TV, childNode, NodeChecked)
        Next

    End Sub


    Public Sub selectAll(TV As Windows.Forms.TreeView)

        '---------------------------------------------------------------------------------
        ' Sub: Check all nodes
        ' set Checked = true and tag = true for all nodes
        '---------------------------------------------------------------------------------

        For Each TreeNode As Windows.Forms.TreeNode In TV.Nodes
            TreeNode.Checked = True
            TreeNode.Tag = True
            If TreeNode.Nodes.Count > 0 Then CheckAllChildNodes(TV, TreeNode, True)
        Next

    End Sub


    Public Sub unselectAll(TV As Windows.Forms.TreeView)

        '---------------------------------------------------------------------------------
        ' Sub: UnCheck all nodes
        ' Set Checked = false and tag = flase for all nodes
        '---------------------------------------------------------------------------------

        For Each TreeNode As Windows.Forms.TreeNode In TV.Nodes
            TreeNode.Checked = False
            TreeNode.Tag = False
            If TreeNode.Nodes.Count > 0 Then CheckAllChildNodes(TV, TreeNode, False)
        Next
    End Sub


    Public Sub UpdateNodesCheckedStatus(TV As Windows.Forms.TreeView)

        '--------------------------------------------------------------------------------------
        ' Sub: Update Nodes Checked Status (called when focus back on TV)
        '--------------------------------------------------------------------------------------

        For Each TreeNode As Windows.Forms.TreeNode In TV.Nodes
            TreeNode.Checked = TreeNode.Tag
        Next TreeNode
    End Sub


    Public Function checkSelection(TV As Windows.Forms.TreeView) As Boolean

        '-----------------------------------------------------------------------------------
        ' Function: checkSelection
        ' Returns true if at least one node is selected, false if no node selected
        '-----------------------------------------------------------------------------------

        For Each Node In TV.Nodes
            If Node.Tag = True Then
                checkSelection = True
                Exit Function
            End If
        Next Node
        checkSelection = False
    End Function


    Public Function NbSelected(TV As Windows.Forms.TreeView) As Integer

        '-----------------------------------------------------------------------------------
        ' Function: NbSelected
        ' Returns the number of selected nodes
        '-----------------------------------------------------------------------------------

        Dim counter As Integer
        counter = 0
        For Each Node In TV.Nodes
            If Node.Tag = True Then counter = counter + 1
        Next Node
        NbSelected = counter
    End Function



End Module
