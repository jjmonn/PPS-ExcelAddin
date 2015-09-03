﻿' EntitiesController.vb
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
' Last modified: 01/09/2015


Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic
Imports VIBlend.WinForms.DataGridView



Friend Class EntitiesController


#Region "Instance Variables"

    ' Objects
    Private View As EntitiesControl
    Private entitiesTV As New TreeView
    Private entitiesFilterTV As New TreeView
    Private entitiesFilterValuesTV As New TreeView
    Private NewEntityView As NewEntityUI
    Private PlatformMGTUI As PlatformMGTGeneralUI

    ' Variables
    Friend entitiesFilterValuesNameIdDict As Hashtable
    Private entitiesNameKeyDic As Hashtable
    Private positionsDictionary As New Dictionary(Of Int32, Double)

#End Region


#Region "Initialize"

    Friend Sub New()

        Globalvariables.Entities.LoadEntitiesTV(entitiesTV)   ' can be replaced by a treenode instead !
        GlobalVariables.Filters.LoadFiltersTV(entitiesFilterTV, GlobalEnums.AnalysisAxis.ENTITIES)
        AxisFilter.LoadFvTv(entitiesFilterValuesTV, GlobalEnums.AnalysisAxis.ENTITIES)
        entitiesNameKeyDic = GlobalVariables.Entities.GetEntitiesDictionary(NAME_VARIABLE, ID_VARIABLE)
        entitiesFilterValuesNameIdDict = GlobalVariables.Filters.GetFiltersDictionary(GlobalEnums.AnalysisAxis.ENTITIES, NAME_VARIABLE, ID_VARIABLE)

        View = New EntitiesControl(Me, entitiesTV, entitiesNameKeyDic, entitiesFilterValuesNameIdDict, entitiesFilterTV)
        NewEntityView = New NewEntityUI(Me, entitiesTV, entitiesFilterTV, entitiesFilterValuesNameIdDict)

        AddHandler GlobalVariables.Entities.CreationEvent, AddressOf AfterEntityCreation
        AddHandler GlobalVariables.Entities.DeleteEvent, AddressOf AfterEntityDeletion
        AddHandler GlobalVariables.Entities.UpdateEvent, AddressOf AfterEntityUpdate
        AddHandler GlobalVariables.Entities.Read, AddressOf AfterEntityRead
        ' handler after update !! priority normal

    End Sub

    Private Sub AssignFilterValue(ByRef fullEntitiesHash As Hashtable, _
                                  ByRef filter_id As UInt32, _
                                  ByRef entity_id As UInt32)

        'fullEntitiesHash(entity_id)(filter_id) = GlobalVariables.EntitiesFilters.GetFilter




    End Sub

    Public Sub addControlToPanel(ByRef dest_panel As Panel, _
                                 ByRef PlatformMGTUI As PlatformMGTGeneralUI)

        Me.PlatformMGTUI = PlatformMGTUI
        dest_panel.Controls.Add(View)
        View.Dock = Windows.Forms.DockStyle.Fill

    End Sub

    Public Sub close()

        View.closeControl()

    End Sub

    Friend Sub sendCloseOrder()

        View.Dispose()
        PlatformMGTUI.displayControl()

    End Sub

#End Region


#Region "Interface"

    Friend Sub CreateEntity(ByRef hash As Hashtable, ByRef parent_node As TreeNode)

        hash(PARENT_ID_VARIABLE) = CInt(parent_node.Name)
        hash.Add(ITEMS_POSITIONS, 1)
        GlobalVariables.Entities.CMSG_CREATE_ENTITY(hash)

    End Sub

    Friend Sub UpdateEntity(ByRef id As Int32, ByRef variable As String, ByVal value As Object)

        Dim ht As Hashtable = GlobalVariables.Entities.entities_hash(id).Clone()
        ht(variable) = value
        GlobalVariables.Entities.CMSG_UPDATE_ENTITY(ht)
     
    End Sub

    Friend Sub UpdateEntity(ByRef id As String, ByRef entity_attributes As Hashtable)

        Dim ht As Hashtable = GlobalVariables.Entities.entities_hash(id).Clone()
        For Each attribute As String In entity_attributes
            ht(attribute) = entity_attributes(attribute)
        Next
        GlobalVariables.Entities.CMSG_UPDATE_ENTITY(ht)

    End Sub

    Friend Sub DeleteEntities(ByRef input_node As TreeNode)

        ' see with nath if this loop goes on the server !
        ' priority normal

        Dim entities_to_delete = TreeViewsUtilities.GetNodesKeysList(input_node)
        entities_to_delete.Reverse()
        For Each entity_id In entities_to_delete
            DeleteEntity(entity_id)
        Next
        View.getDGVRowsIDItemsDict()(input_node.Name).Delete()
        input_node.Remove()

    End Sub

    Private Sub DeleteEntity(ByRef entity_id As Int32)

        '  EntitiesDeletion.DeleteEntityFromPlatform(entity_id)
        GlobalVariables.Entities.CMSG_DELETE_ENTITY(entity_id)

    End Sub

#End Region

#Region "Events"

    Private Sub AfterEntityRead(ByRef status As Boolean, ByRef ht As Hashtable)

        If (status = True) Then
            View.UpdateEntity(ht)
        End If

    End Sub


    Private Sub AfterEntityUpdate(ByRef status As Boolean, ByRef id As Int32)

        If (status = False) Then
            View.UpdateEntity(GlobalVariables.Entities.entities_hash(id))
            MsgBox("Invalid parameter")
        End If

    End Sub

    Private Sub AfterEntityDeletion(ByRef status As Boolean, ByRef id As Int32)

        ' remove node/ row

    End Sub

    ' Triggered by CRUD model creation confirmation 
    Private Sub AfterEntityCreation(ByRef status As Boolean, ByRef id As Int32)

        'Dim parent_node As TreeNode = entitiesTV.Nodes.Find(entity_ht(PARENT_ID_VARIABLE), True)(0)
        'AddNodeAndRow(entity_ht, parent_node)

        'If GlobalVariables.Entities.entities_hash(CInt(parent_node.Name))(ENTITIES_ALLOW_EDITION_VARIABLE) = 1 Then

        '    ' priority normal

        '    'Entities.UpdateEntity(parent_node.Name, ENTITIES_ALLOW_EDITION_VARIABLE, 0)

        '    ' attention dans ce cas les données précédemment soumises sur cette entités sont orphelines !!!
        '    '  -> Triggers creation entity_name & "CORP"
        '    '  -> Transfert des facts
        '    ' ceci peut être codé au niveau du serveur ?

        '    MsgBox(entity_ht(NAME_VARIABLE) & " was previously an editable entity. Adding children entities change its status to not editable. " & Chr(13) _
        '           & "A CORP level has been created and the data previsoulsy submitted on this entity has been transfered to the CORP entity")

        '    parent_node.StateImageIndex = 0
        'End If

    End Sub
#End Region


#Region "Utilities"

    Friend Sub ShowNewEntityUI()

        Dim current_node As TreeNode = Nothing
        If Not View.getCurrentRowItem Is Nothing Then
            Dim entity_id As Int32 = entitiesNameKeyDic(View.getCurrentEntityName())
            current_node = entitiesTV.Nodes.Find(entity_id, True)(0)
            entitiesTV.SelectedNode = current_node

            ' call to entities_filter (axis_filter object) 
            ' to be implemented
            ' priority normal

            '        NewEntityView.FillIn(current_node.Text, Entities.GetRecord(current_node.Name, categoriesTV))
        End If
        NewEntityView.AddEntitiesTV()
        NewEntityView.Show()

    End Sub

    Friend Sub ShowEntitiesMGT()

        View.Show()

    End Sub

    Private Sub AddNodeAndRow(ByRef ht As Hashtable, _
                              ByRef parent_node As TreeNode)

        If Not parent_node Is Nothing Then
            Dim node As TreeNode = parent_node.Nodes.Add(ht(ID_VARIABLE), ht(NAME_VARIABLE), 1, 1)
            View.EntitiesDGV.addRow(node, View.EntitiesDGV.rows_id_item_dic(parent_node.Name))
        Else
            Dim node As TreeNode = entitiesTV.Nodes.Add(ht(ID_VARIABLE), ht(NAME_VARIABLE), 1, 1)
            View.EntitiesDGV.addRow(node)
        End If
        View.EntitiesDGV.fillRow(ht(ID_VARIABLE), ht)
        entitiesNameKeyDic(ht(NAME_VARIABLE)) = ht(ID_VARIABLE)

    End Sub

    Friend Sub SendNewPositionsToModel()

        positionsDictionary = TreeViewsUtilities.GeneratePositionsDictionary(entitiesTV)
        Dim batch_update As New List(Of Object())
        For Each entity_id In positionsDictionary.Keys
            batch_update.Add({entity_id, ITEMS_POSITIONS, positionsDictionary(entity_id)})
        Next

        ' CP while updating
        ' review ! priority normal

    End Sub


#End Region


End Class
