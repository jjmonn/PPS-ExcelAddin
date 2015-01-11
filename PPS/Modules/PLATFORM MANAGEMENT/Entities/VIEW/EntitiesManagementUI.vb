' AssetManagementUI.vb
'
' entities view
'
'
' To do: 
'       - Drag and drop -> Not implemented because the credentials levels must be updated when drag and drop
'       - Open as full window !!
'     
'
' Known bugs:
'
'
'
' Last modified: 09/12/2014
' Author: Julien Monnereau


Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic
Imports VIBlend.WinForms.DataGridView
Imports VIBlend.WinForms.DataGridView.Filters
Imports VIBlend.Utilities
Imports System.Drawing


Friend Class EntitiesManagementUI


#Region "Instance Variables"

    ' Objects
    Private Controller As EntitiesController
    Private entitiesTV As TreeView
    Friend EntitiesDGVMGT As EntitiesDGV

    ' Variables
    Friend entitiesNameKeyDic As Hashtable
    Friend currentRowItem As HierarchyItem
    Friend current_node As TreeNode
    Private tmpSplitterDistance As Double
    Private menuFlag As Boolean
    Private selectionFlag As Boolean

    ' Constants
    'Private Const POSITION_STEP As Double = 0.0000001

#End Region


#Region "Initialization"

    Protected Friend Sub New(ByRef input_controller As EntitiesController, _
                   ByRef input_entitites_name_key_dic As Hashtable, _
                   ByRef input_entities_TV As TreeView)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Controller = input_controller
        entitiesTV = input_entities_TV
        entitiesNameKeyDic = input_entitites_name_key_dic

        EntitiesDGVMGT = New EntitiesDGV(Me, entitiesDGV, Controller)
        Me.WindowState = FormWindowState.Maximized
        entitiesDGV.ImageList = EntitiesTVImageList
        entitiesTV.ImageList = EntitiesTVImageList
        entitiesTV.ContextMenuStrip = RCM_TV
        'entitiesTV.AllowDrop = True

        AddHandler entitiesDGV.CellMouseClick, AddressOf dataGridView_CellMouseClick
        AddHandler entitiesDGV.HierarchyItemMouseClick, AddressOf dataGridView_HierarchyItemMouseClick
        ' AddHandler entitiesDGV.HierarchyItemDrag, AddressOf DragAndDrop.TGV_HierarchyItemDrag
        ' AddHandler entitiesDGV.DragEnter, AddressOf DragAndDrop.TGV_DragEnter
        ' AddHandler entitiesDGV.DragOver, AddressOf DragAndDrop.TGV_DragOver
        ' AddHandler entitiesDGV.DragDrop, AddressOf DragAndDrop.TGV_DragDrop
        AddHandler entitiesDGV.HierarchyItemCollapsed, AddressOf entitiesTGV_HierarchyItemCollapsed
        AddHandler entitiesDGV.HierarchyItemExpanded, AddressOf entitiesTGV_HierarchyItemCollapsed

        AddHandler entitiesTV.AfterCollapse, AddressOf entitiesTV_AfterCollapse
        AddHandler entitiesTV.AfterExpand, AddressOf entitiesTV_AfterExpand
        AddHandler entitiesTV.NodeMouseClick, AddressOf entities_tv_node_mouse_click
        'AddHandler entitiesTV.ItemDrag, AddressOf EntitiesTV_ItemDrag
        'AddHandler entitiesTV.DragEnter, AddressOf EntitiesTV_DragEnter
        'AddHandler entitiesTV.DragOver, AddressOf EntitiesTV_DragOver
        'AddHandler entitiesTV.DragDrop, AddressOf EntitiesTV_DragDrop

    End Sub

    Private Sub EntitiesManagementUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        AddEntitiesTreeview()
        entitiesTV.CollapseAll()
        HideTreeviewToolStripMenuItem_Click(Me, EventArgs.Empty)
        entitiesDGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)


    End Sub

#End Region


#Region "Calls Back"

    Private Sub AddEntity_cmd_Click(sender As Object, e As EventArgs) Handles AddSubEntityToolStripMenuItem.Click, _
                                                                              CreateANewEntityToolStripMenuItem.Click
        Me.Hide()
        Controller.ShowNewEntityUI()

    End Sub

    Private Sub DeleteEntity_cmd_Click(sender As Object, e As EventArgs) Handles DeleteEntityToolStripMenuItem.Click, _
                                                                                 DeleteEntityToolStripMenuItem1.Click

        If Not current_node Is Nothing Then
            Dim confirm As Integer = MessageBox.Show("Careful, you are about to delete the entity " + Chr(13) + Chr(13) + _
                                                     entitiesDGV.CellsArea.GetCellValue(currentRowItem, entitiesDGV.ColumnsHierarchy.Items(0)) + Chr(13) + Chr(13) + _
                                                     "This entity and all sub entities will be deleted, do you confirm?" + Chr(13) + Chr(13), _
                                                     "Entity deletion confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm = DialogResult.Yes Then
                Controller.DeleteEntities(current_node)
            End If
        Else
            MsgBox("An Entity must be selected in order to be deleted")
        End If
        current_node = Nothing
        currentRowItem = Nothing

    End Sub

    Private Sub CopyDownBT_Click(sender As Object, e As EventArgs)

        EntitiesDGVMGT.CopyValueDown()
        entitiesDGV.Refresh()

    End Sub

    Private Sub dropInExcel_cmd_Click(sender As Object, e As EventArgs) Handles DropHierarchyToExcelToolStripMenuItem.Click, _
                                                                                SendEntitiesHierarchyToExcelToolStripMenuItem.Click
        EntitiesDGVMGT.DropInExcel()

    End Sub

    Private Sub ExitBT_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click

        Me.Dispose()
        Me.Close()

    End Sub


#End Region


#Region "DGV Right Click Menu"

    Private Sub cop_down_bt_Click(sender As Object, e As EventArgs) Handles copy_down_bt.Click

        EntitiesDGVMGT.CopyValueDown()

    End Sub

    Private Sub drop_to_excel_bt_Click(sender As Object, e As EventArgs) Handles drop_to_excel_bt.Click

        EntitiesDGVMGT.DropInExcel()

    End Sub

    Private Sub CreateEntityToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateEntityToolStripMenuItem.Click

        AddEntity_cmd_Click(sender, e)

    End Sub

    Private Sub DeleteEntityToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles DeleteEntityToolStripMenuItem2.Click

        DeleteEntity_cmd_Click(sender, e)

    End Sub

#End Region


#Region "DGV Events"

    Private Sub dataGridView_CellMouseClick(ByVal sender As Object, ByVal args As CellMouseEventArgs)

        EntitiesDGVMGT.current_cell = args.Cell
        currentRowItem = args.Cell.RowItem
        current_node = entitiesTV.Nodes.Find(entitiesNameKeyDic(entitiesDGV.CellsArea.GetCellValue(currentRowItem, entitiesDGV.ColumnsHierarchy.Items(0))), True)(0)

    End Sub

    Private Sub dataGridView_HierarchyItemMouseClick(sender As Object, args As HierarchyItemMouseEventArgs)

        currentRowItem = args.HierarchyItem
        Dim tmpKey = entitiesNameKeyDic(entitiesDGV.CellsArea.GetCellValue(args.HierarchyItem, entitiesDGV.ColumnsHierarchy.Items(0)))
        entitiesTV.SelectedNode = entitiesTV.Nodes.Find(tmpKey, True)(0)
        current_node = entitiesTV.SelectedNode

    End Sub

    Private Sub entitiesTGV_KeyDown(sender As Object, e As KeyEventArgs) Handles entitiesDGV.KeyDown

        Select Case e.KeyCode
            Case Keys.Delete : DeleteEntity_cmd_Click(sender, e)
        End Select

    End Sub

    Private Sub entitiesTGV_HierarchyItemCollapsed(sender As Object, args As HierarchyItemEventArgs)

        Dim tmpKey = entitiesNameKeyDic(args.HierarchyItem.GetUniqueID)
        If entitiesTV.Nodes.Find(tmpKey, True).Length > 0 Then
            If args.HierarchyItem.Expanded = True Then
                entitiesTV.Nodes.Find(tmpKey, True)(0).Expand()
            Else
                entitiesTV.Nodes.Find(tmpKey, True)(0).Collapse()
            End If
            Me.Refresh()
        End If

    End Sub

#End Region


#Region "TV Events"

    Private Sub entitiesTV_AfterSelect(sender As Object, e As TreeViewEventArgs)

        Dim tmpKey = e.Node.Name
        If EntitiesDGVMGT.rows_id_item_dic.ContainsKey(tmpKey) Then
            Dim item = EntitiesDGVMGT.rows_id_item_dic(tmpKey)
            entitiesDGV.RowsHierarchy.ClearSelection()
            entitiesDGV.RowsHierarchy.SelectItem(item)
            entitiesDGV.Refresh()
        End If

    End Sub

    Private Sub entitiesTV_AfterCollapse(sender As Object, e As TreeViewEventArgs)

        Dim tmpKey = e.Node.Name
        If EntitiesDGVMGT.rows_id_item_dic.ContainsKey(tmpKey) Then
            EntitiesDGVMGT.rows_id_item_dic(tmpKey).Collapse()
        End If

    End Sub

    Private Sub entitiesTV_AfterExpand(sender As Object, e As TreeViewEventArgs)

        Dim tmpKey = e.Node.Name
        If EntitiesDGVMGT.rows_id_item_dic.ContainsKey(tmpKey) Then
            EntitiesDGVMGT.rows_id_item_dic(tmpKey).Expand()
        End If

    End Sub

    Private Sub entities_tv_node_mouse_click(sender As Object, e As TreeNodeMouseClickEventArgs)

        current_node = e.Node
        currentRowItem = EntitiesDGVMGT.rows_id_item_dic(e.Node.Name)

    End Sub

    Private Sub CategoriesTV_KeyDown(sender As Object, e As KeyEventArgs)

        Select Case e.KeyCode
            Case Keys.Delete : DeleteEntity_cmd_Click(sender, e)
        End Select

    End Sub

    '#Region "Nodes Drag and Drop Procedure"

    '    Private Sub EntitiesTV_ItemDrag(sender As Object, e As ItemDragEventArgs)

    '        DoDragDrop(e.Item, DragDropEffects.Move)

    '    End Sub

    '    Private Sub EntitiesTV_DragEnter(sender As Object, e As DragEventArgs)

    '        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) Then
    '            e.Effect = DragDropEffects.Move
    '        Else
    '            e.Effect = DragDropEffects.None
    '        End If
    '    End Sub

    '    Private Sub EntitiesTV_DragOver(sender As Object, e As Windows.Forms.DragEventArgs)

    '        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub
    '        Dim selectedTreeview As Windows.Forms.TreeView = CType(sender, TreeView)
    '        Dim pt As Drawing.Point = CType(sender, TreeView).PointToClient(New Point(e.X, e.Y))
    '        Dim targetNode As TreeNode = selectedTreeview.GetNodeAt(pt)

    '        'See if the targetNode is currently selected, if so no need to validate again
    '        If Not (selectedTreeview.SelectedNode Is targetNode) Then       'Select the node currently under the cursor
    '            selectedTreeview.SelectedNode = targetNode

    '            'Check that the selected node is not the dropNode and also that it is not a child of the dropNode and therefore an invalid target
    '            Dim dropNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

    '            Do Until targetNode Is Nothing
    '                If targetNode Is dropNode Then
    '                    e.Effect = DragDropEffects.None
    '                    Exit Sub
    '                End If
    '                targetNode = targetNode.Parent
    '            Loop
    '        End If
    '        'Currently selected node is a suitable target
    '        e.Effect = DragDropEffects.Move

    '    End Sub

    '    Private Sub EntitiesTV_DragDrop(sender As Object, e As DragEventArgs)

    '        'Check that there is a TreeNode being dragged
    '        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub
    '        Dim selectedTreeview As TreeView = CType(sender, TreeView)

    '        'Get the TreeNode being dragged
    '        Dim dropNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

    '        'The target node should be selected from the DragOver event
    '        Dim targetNode As TreeNode = selectedTreeview.SelectedNode

    '        dropNode.Remove()                                               'Remove the drop node from its current location

    '        If targetNode Is Nothing Then
    '            selectedTreeview.Nodes.Add(dropNode)
    '        Else
    '            targetNode.Nodes.Add(dropNode)
    '        End If

    '        dropNode.EnsureVisible()                                        ' Ensure the newley created node is visible to the user and 
    '        selectedTreeview.SelectedNode = dropNode
    '        Controller.UpdateValue(dropNode.Name, ASSETS_PARENT_ID_VARIABLE, targetNode.Name)

    '        Dim currentPosition = Controller.positionsDictionary(targetNode.Name) + targetNode.Nodes.Count + POSITION_STEP
    '        cTreeViews_Functions.UpdateChildrenPosition(dropNode, currentPosition, Controller.positionsDictionary)
    '        Controller.SendNewPositionsToModel()

    '    End Sub

    '#End Region

#End Region


#Region "Form Events"

    Private Sub ShowTreeviewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowTreeviewToolStripMenuItem.Click

        SplitContainer1.Panel1.Show()
        SplitContainer1.SplitterDistance = tmpSplitterDistance

    End Sub

    Private Sub HideTreeviewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HideTreeviewToolStripMenuItem.Click

        SplitContainer1.Panel1.Hide()
        tmpSplitterDistance = SplitContainer1.SplitterDistance
        SplitContainer1.SplitterDistance = 0

    End Sub

    Private Sub AssetsManagementUI_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        Controller.SendNewPositionsToModel()

    End Sub

    Protected Friend Sub AddEntitiesTreeview()

        Panel1.Controls.Add(entitiesTV)
        entitiesTV.Dock = DockStyle.Fill

    End Sub

#End Region




End Class