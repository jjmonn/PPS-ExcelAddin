''
''
''
''
''
'' Author: Julien Monnereau
'' Last Modified: 11/05/2015


'Imports System.Windows.Forms


'Friend Class FModelingExportControl


'#Region "Instance Variables"

'    ' Objects
'    Private Controller As FModelingExportController
'    Private exportTV As TreeView
'    Private entitiesTV As TreeView
'    Friend PBar As New ProgressBarControl


'#End Region


'#Region "Initialize"

'    Friend Sub New(ByRef controller As FModelingExportController)

'        ' This call is required by the designer.
'        InitializeComponent()

'        ' Add any initialization after the InitializeComponent() call.
'        Me.Controller = controller
'        Me.Controls.Add(PBar)                           ' Progress Bar
'        PBar.Left = (Me.Width - PBar.Width) / 2         ' Progress Bar
'        PBar.Top = (Me.Height - PBar.Height) / 2        ' Progress Bar

'    End Sub

'    Protected Friend Sub AddExportTabElements(ByRef exportTV As TreeView, _
'                                              ByVal entitiesTV As TreeView, _
'                                              ByRef export_mapping As VIBlend.WinForms.DataGridView.vDataGridView)

'        Me.exportTV = exportTV
'        Me.entitiesTV = entitiesTV

'        ExportsTVPanel.Controls.Add(exportTV)
'        EntitiesTV2Panel.Controls.Add(entitiesTV)
'        ExportMappingPanel.Controls.Add(export_mapping)
'        exportTV.Dock = DockStyle.Fill
'        entitiesTV.Dock = DockStyle.Fill
'        export_mapping.Dock = DockStyle.Fill

'        entitiesTV.CollapseAll()
'        exportTV.ExpandAll()
'        entitiesTV.ImageList = EntitiesTVImageList
'        exportTV.ImageList = ExportTVImageList

'        AddHandler entitiesTV.ItemDrag, AddressOf TV_ItemDrag
'        AddHandler entitiesTV.DragEnter, AddressOf TV_DragEnter
'        AddHandler exportTV.DragOver, AddressOf TV_DragOver
'        AddHandler exportTV.DragDrop, AddressOf TV_DragDrop

'    End Sub

'#End Region


'#Region "Call backs"

'    Private Sub ReinjectionBT_Click(sender As Object, e As EventArgs) Handles ReinjectionBT.Click

'        Select Case Controller.AreMappingsComplete
'            Case -1 : MsgBox("All Exports must be assigned to an Account")
'            Case -2 : MsgBox("All Exports must be assigned to an Entity")
'            Case 0 : Controller.Export()
'        End Select

'    End Sub

'#Region "TVs Drag and Drop"

'    Private Sub TV_ItemDrag(sender As Object, e As ItemDragEventArgs)

'        DoDragDrop(e.Item, Windows.Forms.DragDropEffects.Move)

'    End Sub

'    Private Sub TV_DragEnter(sender As Object, e As DragEventArgs)

'        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) Then
'            e.Effect = DragDropEffects.Move
'        Else
'            e.Effect = DragDropEffects.None
'        End If
'    End Sub

'    Private Sub TV_DragOver(sender As Object, e As DragEventArgs)

'        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub
'        Dim selectedTreeview As Windows.Forms.TreeView = CType(sender, Windows.Forms.TreeView)
'        Dim pt As Drawing.Point = CType(sender, TreeView).PointToClient(New Drawing.Point(e.X, e.Y))
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

'    Private Sub TV_DragDrop(sender As Object, e As DragEventArgs)

'        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub
'        Dim selectedTreeview As TreeView = CType(sender, Windows.Forms.TreeView)
'        Dim dropNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

'        Dim targetNode As TreeNode = selectedTreeview.SelectedNode
'        If targetNode.Parent Is Nothing Then
'            If targetNode.Nodes.Count > 0 Then targetNode.Nodes(0).Remove()
'        Else
'            targetNode = targetNode.Parent
'            targetNode.Nodes(0).Remove()
'        End If
'        ' dropNode.Remove()             'Remove the drop node from its current location
'        dropNode = dropNode.Clone()

'        If targetNode Is Nothing Then
'            selectedTreeview.Nodes.Add(dropNode)
'        Else
'            targetNode.Nodes.Add(dropNode)
'        End If
'        Controller.UpdateMappedEntity(targetNode.Name, dropNode.Name)
'        dropNode.EnsureVisible()                                        ' Ensure the newley created node is visible to the user and 
'        selectedTreeview.SelectedNode = dropNode                        ' Select it
'        dropNode.StateImageIndex = 1
'        dropNode.SelectedImageIndex = 1

'    End Sub

'#End Region

'#End Region



'End Class
