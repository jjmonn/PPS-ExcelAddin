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
' Last modified: 04/05/2015


Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic
Imports VIBlend.WinForms.DataGridView



Friend Class EntitiesController


#Region "Instance Variables"

    ' Objects
    Private Entities As Entity
    Private View As EntitiesControl
    Private entitiesTV As New TreeView
    Private categoriesTV As New TreeView
    Private NewEntityView As NewEntityUI
    Private EntitiesDeletion As New SQLEntities
    Private PlatformMGTUI As PlatformMGTGeneralUI

    ' Variables
    Friend categoriesNameKeyDic As Hashtable
    Private entitiesNameKeyDic As Hashtable
    Private positionsDictionary As New Dictionary(Of String, Double)

#End Region


#Region "Initialize"

    Friend Sub New()

        Entities = New Entity
        Entity.LoadEntitiesTree(entitiesTV)   ' can be replaced by a treenode instead !
        AnalysisAxisCategory.LoadCategoryCodeTV(categoriesTV, ControllingUI2Controller.ENTITY_CATEGORY_CODE)
        entitiesNameKeyDic = EntitiesMapping.GetEntitiesDictionary(ENTITIES_NAME_VARIABLE, ENTITIES_ID_VARIABLE)
        categoriesNameKeyDic = CategoriesMapping.GetCategoryDictionary(ControllingUI2Controller.ENTITY_CATEGORY_CODE, CATEGORY_NAME_VARIABLE, CATEGORY_ID_VARIABLE)

        If Entities.object_is_alive = False Then
            MsgBox("An error occured with network connection. Please check the connection and try again." & Chr(13) & _
                   "If the error persist please contact the PPS team.")
            Exit Sub
        End If

        View = New EntitiesControl(Me, getEntitiesDict, entitiesTV, entitiesNameKeyDic, categoriesNameKeyDic, categoriesTV)
        NewEntityView = New NewEntityUI(Me, entitiesTV, categoriesTV, categoriesNameKeyDic)

    End Sub

    Private Function getEntitiesDict() As Dictionary(Of String, Hashtable)

        Dim entities_dic As New Dictionary(Of String, Hashtable)
        Dim entities_list = TreeViewsUtilities.GetNodesKeysList(entitiesTV)
        For Each entity_id In entities_list
            entities_dic.Add(entity_id, Entities.GetRecord(entity_id, categoriesTV))
        Next
        Return entities_dic

    End Function

    Public Sub addControlToPanel(ByRef dest_panel As Panel, _
                                 ByRef PlatformMGTUI As PlatformMGTGeneralUI)

        Me.PlatformMGTUI = PlatformMGTUI
        dest_panel.Controls.Add(View)
        View.Dock = Windows.Forms.DockStyle.Fill

    End Sub

    Public Sub close()

        Entities.close()
        View.closeControl()

    End Sub

    Protected Friend Sub sendCloseOrder()

        View.Dispose()
        PlatformMGTUI.displayControl()

    End Sub

#End Region


#Region "Interface"

    Protected Friend Sub CreateEntity(ByRef hash As Hashtable, ByRef parent_node As TreeNode)

        Dim entity_id As String = TreeViewsUtilities.GetNewNodeKey(entitiesTV, ENTITIES_TOKEN_SIZE)
        hash.Add(ENTITIES_ID_VARIABLE, entity_id)
        Dim credential_level As Int32
        If Not parent_node Is Nothing Then
            credential_level = Entities.ReadEntity(hash(ENTITIES_PARENT_ID_VARIABLE), ENTITIES_CREDENTIAL_ID_VARIABLE)
        Else
            credential_level = 0
        End If
        hash.Add(ENTITIES_CREDENTIAL_ID_VARIABLE, credential_level)
        hash.Add(ITEMS_POSITIONS, 1)

        Entities.CreateEntity(hash)
        AddNodeAndRow(entity_id, hash(ENTITIES_NAME_VARIABLE), parent_node)
        If Entities.ReadEntity(parent_node.Name, ENTITIES_ALLOW_EDITION_VARIABLE) = 1 Then
            Entities.UpdateEntity(parent_node.Name, ENTITIES_ALLOW_EDITION_VARIABLE, 0)
            parent_node.StateImageIndex = 0
        End If
       
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
        View.getDGVRowsIDItemsDict()(input_node.Name).Delete()
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
        If Not View.getCurrentRowItem Is Nothing Then
            Dim entity_id As String = entitiesNameKeyDic(View.getCurrentEntityName())
            current_node = entitiesTV.Nodes.Find(entity_id, True)(0)
            entitiesTV.SelectedNode = current_node
            NewEntityView.FillIn(current_node.Text, Entities.GetRecord(current_node.Name, categoriesTV))
        End If
        NewEntityView.AddEntitiesTV()
        NewEntityView.Show()

    End Sub

    Protected Friend Sub ShowEntitiesMGT()

        View.Show()

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
