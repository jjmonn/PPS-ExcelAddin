' DragAndDrop.vb
'
' Drag and Drop Utilities functions
'
'
'
'
' Author: Julien Monnereau
' Last modified: 10/12/2014


Imports System.Windows.Forms
Imports VIBlend.WinForms.DataGridView
Imports System.Drawing



Friend Class DragAndDrop


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
