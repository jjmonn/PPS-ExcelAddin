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
Imports VIBlend.WinForms.Controls
Imports CRUD

Friend Class EntitiesController


#Region "Instance Variables"

    ' Objects
    Private View As EntitiesView
    Private entitiesTV As New vTreeView
    Private entitiesFilterTV As New vTreeView
    Private entitiesFilterValuesTV As New vTreeView
    Private NewEntityView As NewEntityUI
    Private PlatformMGTUI As PlatformMGTGeneralUI

    ' Variables
    Private positionsDictionary As New Dictionary(Of Int32, Double)

#End Region


#Region "Initialize"

    Friend Sub New()

        LoadInstanceVariables()
        View = New EntitiesView(Me, entitiesTV, entitiesFilterValuesTV, entitiesFilterTV)
        NewEntityView = New NewEntityUI(Me, entitiesTV, GlobalVariables.Currencies.currencies_hash)

        ' Entities CRUD Events
        AddHandler GlobalVariables.Entities.CreationEvent, AddressOf AfterEntityCreation
        AddHandler GlobalVariables.Entities.DeleteEvent, AddressOf AfterEntityDeletion
        AddHandler GlobalVariables.Entities.UpdateEvent, AddressOf AfterEntityUpdate
        AddHandler GlobalVariables.Entities.Read, AddressOf AfterEntityRead

        ' Entities Filters CRUD Events
        AddHandler GlobalVariables.AxisFilters.Read, AddressOf AfterEntityFilterRead
        AddHandler GlobalVariables.AxisFilters.UpdateEvent, AddressOf AfterEntityFilterUpdate

    End Sub

    Public Sub LoadInstanceVariables()

        GlobalVariables.Entities.LoadEntitiesTV(entitiesTV)
        GlobalVariables.Filters.LoadFiltersTV(entitiesFilterTV, GlobalEnums.AnalysisAxis.ENTITIES)
        AxisFilterManager.LoadFvTv(entitiesFilterValuesTV, GlobalEnums.AnalysisAxis.ENTITIES)

    End Sub

    Public Sub addControlToPanel(ByRef dest_panel As Panel, _
                                 ByRef PlatformMGTUI As PlatformMGTGeneralUI)

        Me.PlatformMGTUI = PlatformMGTUI
        dest_panel.Controls.Add(View)
        View.Dock = Windows.Forms.DockStyle.Fill

    End Sub

    Public Sub close()

        RemoveHandler GlobalVariables.AxisFilters.Read, AddressOf AfterEntityFilterRead
        RemoveHandler GlobalVariables.AxisFilters.UpdateEvent, AddressOf AfterEntityFilterUpdate
        SendNewPositionsToModel()
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

    Friend Sub UpdateBatch(ByRef p_entitiesUpdates As List(Of Tuple(Of Int32, String, Int32)))

        Dim entitiesHTUpdates As New Hashtable
        For Each tuple_ As Tuple(Of Int32, String, Int32) In p_entitiesUpdates
            Dim ht As Hashtable = GlobalVariables.Entities.entities_hash(tuple_.Item1).Clone
            ht(tuple_.Item2) = tuple_.Item3
            entitiesHTUpdates(CInt(ht(ID_VARIABLE))) = ht
        Next
        GlobalVariables.Entities.CMSG_UPDATE_ENTITY_LIST(entitiesHTUpdates)

    End Sub

    Friend Sub DeleteEntity(ByRef entity_id As Int32)

        GlobalVariables.Entities.CMSG_DELETE_ENTITY(entity_id)

    End Sub

    Friend Sub UpdateFilterValue(ByRef p_axisElemId As Int32, _
                             ByRef filterId As Int32, _
                             ByRef filterValueId As Int32)
        For Each axisFilter As AxisFilter In GetAxisFilterDictionary().Values
            If axisFilter.FilterId = filterId Then
                Dim l_copy = GetAxisFilterCopy(axisFilter.Id)
                l_copy.FilterValueId = filterValueId
                UpdateFilterValue(l_copy)
            End If
        Next
    End Sub

    Friend Sub UpdateFilterValue(ByRef p_axisFilter As AxisFilter)

        If p_axisFilter.FilterId = GlobalVariables.Filters.GetMostNestedFilterId(p_axisFilter.FilterId) Then
            GlobalVariables.AxisFilters.CMSG_UPDATE_AXIS_FILTER(p_axisFilter)
        End If

    End Sub

    Friend Function GetAxisFilter(ByVal p_axisFilterId As UInt32) As AxisFilter
        Return GlobalVariables.AxisFilters.GetAxisFilter(AxisType.Entities, p_axisFilterId)
    End Function

    Friend Function GetAxisFilterCopy(ByVal p_axisId As UInt32) As AxisFilter
        Dim l_axis = GetAxisFilter(p_axisId)

        If l_axis Is Nothing Then Return Nothing
        Return l_axis.Clone()
    End Function

    Friend Function GetAxisFilterDictionary() As SortedDictionary(Of Int32, AxisFilter)
        Return GlobalVariables.AxisFilters.GetAxisFilterDictionary(AxisType.Entities)
    End Function



#End Region


#Region "Events"

    Private Sub AfterEntityRead(ByRef status As Boolean, ByRef ht As Hashtable)

        If (status = True) Then
            View.LoadInstanceVariables_Safe()
            View.UpdateEntity(ht)
        End If

    End Sub

    Private Sub AfterEntityDeletion(ByRef status As Boolean, ByRef id As Int32)

        If status = True Then
            View.LoadInstanceVariables_Safe()
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

    Private Sub AfterEntityFilterRead(ByRef status As ErrorMessage, ByRef p_axisFilter As AxisFilter)

        If (status = ErrorMessage.SUCCESS) Then
            If p_axisFilter.Axis <> AxisType.Entities Then Exit Sub
            Dim entityId As Int32 = p_axisFilter.Id
            If GlobalVariables.Entities.entities_hash.ContainsKey(entityId) Then
                View.LoadInstanceVariables()
                View.UpdateEntity(GlobalVariables.Entities.entities_hash(entityId))
            End If
        End If

    End Sub

    Private Sub AfterEntityFilterUpdate(ByRef status As ErrorMessage, _
                                        ByRef axisType As AxisType, _
                                        ByRef axisFilterId As Int32)
        If status <> ErrorMessage.SUCCESS Then
            If axisType <> CRUD.AxisType.Entities Then Exit Sub
            Dim l_axisFilter As AxisFilter = GlobalVariables.AxisFilters.GetAxisFilter(axisType, axisFilterId)
            View.UpdateEntity(GlobalVariables.Entities.entities_hash(l_axisFilter.AxisElemId))
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
        If Not current_row Is Nothing Then parentEntityID = VTreeViewUtil.FindNode(entitiesTV, current_row.ItemValue).Value
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

        Dim position As Int32
        Dim entitiesUpdates As New List(Of Tuple(Of Int32, String, Int32))
        positionsDictionary = DataGridViewsUtil.GeneratePositionsDictionary(View.m_entitiesDataGridView)

        For Each entity_id As Int32 In positionsDictionary.Keys
            position = positionsDictionary(entity_id)
            If GlobalVariables.Entities.entities_hash.ContainsKey(entity_id) = False Then Continue For
            If position <> GlobalVariables.Entities.entities_hash(entity_id)(ITEMS_POSITIONS) Then
                Dim tuple_ As New Tuple(Of Int32, String, Int32)(entity_id, ITEMS_POSITIONS, position)
                entitiesUpdates.Add(tuple_)
            End If
        Next
        UpdateBatch(entitiesUpdates)

    End Sub


#End Region


End Class
