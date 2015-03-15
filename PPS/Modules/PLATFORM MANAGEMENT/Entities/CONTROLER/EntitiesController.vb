' EntitiesController.vb
'
' Controller for entities 
'
' To do:
'       - new entity -> put the parent entity's credential else 0 -> check !!       
'       - implement rows up down ?
'       - 
'   
'
' Known bugs:
'       -
'
'
' Author: Julien Monnereau
' Last modified: 12/03/2015


Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic
Imports VIBlend.WinForms.DataGridView



Friend Class EntitiesController


#Region "Instance Variables"

    ' Objects
    Private Entities As Entity
    Private ViewObject As EntitiesManagementUI
    Friend NewEntityView As NewEntityUI
    Private EntitiesDeletion As New SQLEntities

    ' Variables
    Private entitiesTV As New TreeView
    Friend categoriesTV As New TreeView
    Private entitiesNameKeyDic As Hashtable
    Friend positionsDictionary As New Dictionary(Of String, Double)


#End Region


#Region "Initialize"

    Friend Sub New()

        Entity.LoadEntitiesTree(entitiesTV)
        Category.LoadCategoriesTree(categoriesTV)
        entitiesNameKeyDic = EntitiesMapping.GetEntitiesDictionary(ASSETS_NAME_VARIABLE, ASSETS_TREE_ID_VARIABLE)
        Entities = New Entity

        If Entities.object_is_alive = False Then
            MsgBox("An error occured with network connection. Please check the connection and try again." & Chr(13) & _
                   "If the error persist please contact the PPS team.")
            Exit Sub
        End If

        ViewObject = New EntitiesManagementUI(Me, entitiesNameKeyDic, entitiesTV)
        NewEntityView = New NewEntityUI(Me, entitiesTV, categoriesTV, ViewObject.EntitiesDGVMGT.categoriesNameKeyDic, ViewObject.EntitiesDGVMGT.categoriesKeyNameDic)

        DisplayDGVData()
        ViewObject.Show()

    End Sub

#End Region


#Region "Interface"

    Private Sub DisplayDGVData()

        ViewObject.EntitiesDGVMGT.InitializeTGVRows(entitiesTV)

        Dim entities_dic As New Dictionary(Of String, Hashtable)
        Dim entities_list = TreeViewsUtilities.GetNodesKeysList(entitiesTV)
        For Each entity_id In entities_list
            entities_dic.Add(entity_id, Entities.GetRecord(entity_id, categoriesTV))
        Next
        ViewObject.EntitiesDGVMGT.FillDGV(entities_dic)

    End Sub

    Protected Friend Sub CreateEntity(ByRef hash As Hashtable, ByRef parent_node As TreeNode)

        Dim entity_id As String = TreeViewsUtilities.GetNewNodeKey(entitiesTV, ENTITIES_TOKEN_SIZE)
        hash.Add(ASSETS_TREE_ID_VARIABLE, entity_id)
        Dim credential_level As Int32
        If Not parent_node Is Nothing Then
            credential_level = Entities.ReadEntity(hash(ASSETS_PARENT_ID_VARIABLE), ASSETS_CREDENTIAL_ID_VARIABLE)
        Else
            credential_level = 0
        End If
        hash.Add(ASSETS_CREDENTIAL_ID_VARIABLE, credential_level)
        hash.Add(ITEMS_POSITIONS, 1)

        Entities.CreateEntity(hash)
        AddNodeAndRow(entity_id, hash(ASSETS_NAME_VARIABLE), parent_node)
        If Entities.ReadEntity(parent_node.Name, ASSETS_ALLOW_EDITION_VARIABLE) = 1 Then
            Entities.UpdateEntity(parent_node.Name, ASSETS_ALLOW_EDITION_VARIABLE, 0)
            parent_node.StateImageIndex = 0
        End If
        DisplayDGVData()

    End Sub

    Protected Friend Sub UpdateValue(ByRef entity_id As String, ByRef field As String, ByRef value As Object)

        Entities.UpdateEntity(entity_id, field, value)

    End Sub

    Friend Sub DeleteEntities(ByRef input_node As TreeNode)

        Dim entities_to_delete = TreeViewsUtilities.GetNodesKeysList(input_node)
        entities_to_delete.Reverse()
        For Each entity_id In entities_to_delete
            DeleteEntity(entity_id)
        Next
        ViewObject.EntitiesDGVMGT.rows_id_item_dic(input_node.Name).Delete()
        input_node.Remove()

    End Sub

    Private Sub DeleteEntity(ByRef entity_id As String)

        EntitiesDeletion.DeleteEntityFromPlatform(entity_id)
        Entities.DeleteEntity(entity_id)

    End Sub

#End Region


#Region "Utilities"

    Protected Friend Sub ShowNewEntityUI()

        Dim current_node As TreeNode = Nothing
        If Not ViewObject.currentRowItem Is Nothing Then
            Dim entity_id As String = entitiesNameKeyDic(ViewObject.entitiesDGV.CellsArea.GetCellValue(ViewObject.currentRowItem, ViewObject.entitiesDGV.ColumnsHierarchy.Items(0)))
            current_node = entitiesTV.Nodes.Find(entity_id, True)(0)
            entitiesTV.SelectedNode = current_node
            NewEntityView.FillIn(current_node.Text, Entities.GetRecord(current_node.Name, categoriesTV))
        End If
        NewEntityView.AddEntitiesTV()
        NewEntityView.Show()

    End Sub

    Protected Friend Sub ShowEntitiesMGT()

        ViewObject.Show()

    End Sub

    Private Sub AddNodeAndRow(ByRef entity_id As String, _
                              ByRef entity_name As String, _
                              ByRef parent_node As TreeNode)

        If Not parent_node Is Nothing Then
            parent_node.Nodes.Add(entity_id, entity_name, 1, 1)
        Else
            entitiesTV.Nodes.Add(entity_id, entity_name, 1, 1)
        End If
        entitiesNameKeyDic.Add(entity_name, entity_id)
        positionsDictionary = TreeViewsUtilities.GeneratePositionsDictionary(entitiesTV)

    End Sub

    Protected Friend Sub SendNewPositionsToModel()

        For Each entity_id In positionsDictionary.Keys
            Entities.UpdateEntity(entity_id, ITEMS_POSITIONS, positionsDictionary(entity_id))
        Next

    End Sub


#End Region


End Class
