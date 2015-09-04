' EntitiesController.vb
'
' Controller for entities 
'
' To do:
'       - implement rows up down ?
'       - 
'   
'
' Known bugs:
'       -
'
'
' Author: Julien Monnereau
' Last modified: 03/09/2015


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
    Private positionsDictionary As New Dictionary(Of Int32, Double)

#End Region


#Region "Initialize"

    Friend Sub New()

        LoadInstanceVariables()
        View = New EntitiesControl(Me, entitiesTV, entitiesFilterValuesTV, entitiesFilterTV)
        NewEntityView = New NewEntityUI(Me, entitiesTV, GlobalVariables.Currencies.currencies_hash)

        ' Entities CRUD Events
        AddHandler GlobalVariables.Entities.CreationEvent, AddressOf AfterEntityCreation
        AddHandler GlobalVariables.Entities.DeleteEvent, AddressOf AfterEntityDeletion
        AddHandler GlobalVariables.Entities.UpdateEvent, AddressOf AfterEntityUpdate
        AddHandler GlobalVariables.Entities.Read, AddressOf AfterEntityRead

        ' Entities Filters CRUD Events
        AddHandler GlobalVariables.EntitiesFilters.Read, AddressOf AfterEntityFilterRead
        AddHandler GlobalVariables.EntitiesFilters.UpdateEvent, AddressOf AfterEntityFilterUpdate

    End Sub

    Private Sub LoadInstanceVariables()

        GlobalVariables.Entities.LoadEntitiesTV(entitiesTV)
        GlobalVariables.Filters.LoadFiltersTV(entitiesFilterTV, GlobalEnums.AnalysisAxis.ENTITIES)
        AxisFilter.LoadFvTv(entitiesFilterValuesTV, GlobalEnums.AnalysisAxis.ENTITIES)

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

        View.Dispose()

    End Sub

#End Region


#Region "Interface"

    Friend Sub CreateEntity(ByRef entityName As String, _
                            ByRef entityCurrency As Int32, _
                            ByRef parentEntityId As Int32, _
                            ByRef position As Int32, _
                            ByRef allowEdition As Int32)

        Dim ht As New Hashtable
        ht.Add(NAME_VARIABLE, entityName)
        ht.Add(ENTITIES_CURRENCY_VARIABLE, entityCurrency)
        ht.Add(PARENT_ID_VARIABLE, parentEntityId)
        ht.Add(ITEMS_POSITIONS, position)
        ht.Add(ENTITIES_ALLOW_EDITION_VARIABLE, allowEdition)
        GlobalVariables.Entities.CMSG_CREATE_ENTITY(ht)

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

    Friend Sub DeleteEntity(ByRef entity_id As Int32)

        GlobalVariables.Entities.CMSG_DELETE_ENTITY(entity_id)

    End Sub

    Friend Sub UpdateFilterValue(ByRef entityId As Int32, _
                                 ByRef filterId As Int32, _
                                 ByRef filterValueId As Int32)

        If filterId = GlobalVariables.Filters.GetMostNestedFilterId(filterId) Then
            GlobalVariables.EntitiesFilters.CMSG_UPDATE_entity_FILTER(entityId, filterId, filterValueId)
        End If
     
    End Sub


#End Region


#Region "Events"

    Private Sub AfterEntityRead(ByRef status As Boolean, ByRef ht As Hashtable)

        If (status = True) Then
            LoadInstanceVariables()
            View.UpdateEntity(ht)
        End If

    End Sub

    Private Sub AfterEntityDeletion(ByRef status As Boolean, ByRef id As Int32)

        If status = True Then
            LoadInstanceVariables()
            View.DeleteEntity(id)
        End If

    End Sub

    Private Sub AfterEntityUpdate(ByRef status As Boolean, ByRef id As Int32)

        If (status = False) Then
            View.UpdateEntity(GlobalVariables.Entities.entities_hash(id))
            MsgBox("Invalid parameter")
        End If

    End Sub

    Private Sub AfterEntityCreation(ByRef status As Boolean, ByRef id As Int32)

        If status = False Then
            MsgBox("The Entity Could not be created.")
            ' catch and display error as well V2 priority normal
        End If

    End Sub

    Private Sub AfterEntityFilterRead(ByRef status As Boolean, ByRef entityFilterHT As Hashtable)

        If (status = True) Then
            LoadInstanceVariables()
            View.UpdateEntity(GlobalVariables.Entities.entities_hash(CInt(entityFilterHT(ENTITY_ID_VARIABLE))))
        End If

    End Sub

    Private Sub AfterEntityFilterUpdate(ByRef status As Boolean, _
                                        ByRef entityId As Int32, _
                                        ByRef filterId As Int32, _
                                        ByRef filterValueId As Int32)
        If status = False Then
            View.UpdateEntity(GlobalVariables.Entities.entities_hash(entityId))
            MsgBox("The Entity could be updated.")
            ' catch and display message
        End If

    End Sub


#End Region


#Region "Utilities"

    Friend Sub ShowNewEntityUI()

        Dim parentEntityID As Int32 = 0
        Dim current_row As HierarchyItem = View.getCurrentRowItem
        On Error GoTo ShowNewEntity
        If Not current_row Is Nothing Then parentEntityID = entitiesTV.Nodes.Find(current_row.ItemValue, True)(0).Name
        GoTo ShowNewEntity

ShowNewEntity:
        NewEntityView.SetParentEntityId(parentEntityID)
        NewEntityView.Show()

    End Sub

    Friend Sub ShowEntitiesMGT()

        View.Show()

    End Sub

    Friend Function GetEntityId(ByRef name As String) As Int32

        Return GlobalVariables.Entities.GetEntityId(name)

    End Function

    Friend Sub SendNewPositionsToModel()

        ' to be reviewed priority normal
        'positionsDictionary = TreeViewsUtilities.GeneratePositionsDictionary(entitiesTV)
        'Dim batch_update As New List(Of Object())
        'For Each entity_id In positionsDictionary.Keys
        '    batch_update.Add({entity_id, ITEMS_POSITIONS, positionsDictionary(entity_id)})
        'Next

    End Sub


#End Region


End Class
