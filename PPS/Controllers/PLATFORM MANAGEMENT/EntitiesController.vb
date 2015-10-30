﻿' EntitiesController.vb
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
        NewEntityView = New NewEntityUI(Me, entitiesTV, GlobalVariables.Currencies.GetDictionary())

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

        Dim l_entity As New Entity

        l_entity.Name = entityName
        l_entity.CurrencyId = entityCurrency
        l_entity.ParentId = parentEntityId
        l_entity.ItemPosition = position
        l_entity.AllowEdition = allowEdition
        GlobalVariables.Entities.Create(l_entity)

    End Sub

    Friend Sub UpdateEntity(ByRef entity_attributes As Entity)

        GlobalVariables.Entities.Update(entity_attributes)

    End Sub

    Friend Sub DeleteEntity(ByRef entity_id As Int32)

        GlobalVariables.Entities.Delete(entity_id)

    End Sub

    Friend Sub UpdateAxisFilter(ByRef p_axisElemId As Int32, _
                             ByRef filterId As Int32, _
                             ByRef filterValueId As Int32)
        For Each axisFilter As AxisFilter In GetAxisFilterDictionary().Values
            If axisFilter.FilterId = filterId Then
                Dim l_copy = GetAxisFilterCopy(axisFilter.Id)
                l_copy.FilterValueId = filterValueId
                UpdateAxisFilter(l_copy)
            End If
        Next
    End Sub

    Friend Sub UpdateAxisFilter(ByRef p_axisFilter As AxisFilter)

        If p_axisFilter.FilterId = GlobalVariables.Filters.GetMostNestedFilterId(p_axisFilter.FilterId) Then
            GlobalVariables.AxisFilters.Update(p_axisFilter)
        End If

    End Sub

    Friend Function GetAxisFilter(ByVal p_axisFilterId As UInt32) As AxisFilter
        Return GlobalVariables.AxisFilters.GetValue(AxisType.Entities, p_axisFilterId)
    End Function

    Friend Function GetAxisFilterCopy(ByVal p_axisId As UInt32) As AxisFilter
        Dim l_axis = GetAxisFilter(p_axisId)

        If l_axis Is Nothing Then Return Nothing
        Return l_axis.Clone()
    End Function

    Friend Function GetAxisFilterDictionary() As SortedDictionary(Of Int32, AxisFilter)
        Return GlobalVariables.AxisFilters.GetDictionary(AxisType.Entities)
    End Function

    Friend Sub UpdateEntityName(ByVal p_id As UInt32, ByVal p_value As String)
        UpdateVar(p_id, p_value, Function(p_entity As Entity, p_destValue As Object) p_entity.Name = p_destValue)
    End Sub

    Friend Sub UpdateEntityCurrency(ByVal p_id As UInt32, ByVal p_value As UInt32)
        UpdateVar(p_id, p_value, Function(p_entity As Entity, p_destValue As Object) p_entity.CurrencyId = p_destValue)
    End Sub

    Private Sub UpdateVar(ByVal p_id As UInt32, ByVal p_value As Object, ByRef p_action As Action(Of Entity, Object))
        Dim l_entity = GetEntityCopy(p_id)

        If l_entity Is Nothing Then Exit Sub
        p_action(l_entity, p_value)
        UpdateEntity(l_entity)
    End Sub

#End Region

#Region "Events"

    Private Sub AfterEntityRead(ByRef status As ErrorMessage, ByRef ht As Entity)

        If (status = ErrorMessage.SUCCESS) Then
            View.LoadInstanceVariables_Safe()
            View.UpdateEntity(ht)
        End If

    End Sub

    Private Sub AfterEntityDeletion(ByRef status As ErrorMessage, ByRef id As Int32)

        If status = ErrorMessage.SUCCESS Then
            View.LoadInstanceVariables_Safe()
            View.DeleteEntity(id)
        End If

    End Sub

    Private Sub AfterEntityUpdate(ByRef status As ErrorMessage, ByRef id As Int32)

        If (status <> ErrorMessage.SUCCESS) Then
            If GlobalVariables.Entities.GetValue(id) Is Nothing Then Exit Sub
            View.UpdateEntity(GlobalVariables.Entities.GetValue(id))
            MsgBox("Invalid parameter")
        End If

    End Sub

    Private Sub AfterEntityCreation(ByRef status As ErrorMessage, ByRef id As Int32)

        If status <> ErrorMessage.SUCCESS Then
            MsgBox("The Entity Could not be created.")
            ' catch and display error as well V2 priority normal
        End If

    End Sub

    Private Sub AfterEntityFilterRead(ByRef status As ErrorMessage, ByRef p_axisFilter As CRUDEntity)

        If (status = ErrorMessage.SUCCESS) Then
            Dim axisFilter As AxisElem = CType(p_axisFilter, AxisElem)
            If axisFilter.Axis <> AxisType.Entities Then Exit Sub
            Dim entityId As Int32 = p_axisFilter.Id
            If Not GlobalVariables.Entities.GetValue(entityId) Is Nothing Then
                View.LoadInstanceVariables()
                View.UpdateEntity(GlobalVariables.Entities.GetValue(entityId))
            End If
        End If

    End Sub

    Private Sub AfterEntityFilterUpdate(ByRef status As ErrorMessage, _
                                        ByRef axisFilterId As UInt32)
        If status <> ErrorMessage.SUCCESS Then
            Dim l_axisFilter As AxisFilter = GlobalVariables.AxisFilters.GetValue(axisFilterId)
            If l_axisFilter.Axis <> CRUD.AxisType.Entities Then Exit Sub
            If GlobalVariables.Entities.GetValue(l_axisFilter.AxisElemId) Is Nothing Then Exit Sub
            View.UpdateEntity(GlobalVariables.Entities.GetValue(l_axisFilter.AxisElemId))
            MsgBox("The Entity could be updated.")
            ' catch and display message
        End If

    End Sub

#End Region

#Region "Utilities"

    Friend Function GetEntity(ByVal p_id As UInt32) As Entity
        Return GlobalVariables.Entities.GetValue(p_id)
    End Function

    Friend Function GetEntityCopy(ByVal p_id As UInt32) As Entity
        Dim l_entity As Entity = GlobalVariables.Entities.GetValue(p_id)

        If l_entity Is Nothing Then Return Nothing
        Return l_entity.Clone()
    End Function

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

        Return GlobalVariables.Entities.GetValueId(name)

    End Function

    Friend Sub SendNewPositionsToModel()

        Dim position As Int32
        Dim listEntities As New List(Of CRUDEntity)

        positionsDictionary = DataGridViewsUtil.GeneratePositionsDictionary(View.m_entitiesDataGridView)
        For Each entity_id As Int32 In positionsDictionary.Keys
            position = positionsDictionary(entity_id)
            If GetEntity(entity_id) Is Nothing Then Continue For
            If position <> GetEntity(entity_id).ItemPosition Then
                Dim l_entity = GetEntityCopy(entity_id)

                If Not l_entity Is Nothing Then
                    l_entity.ItemPosition = position
                    listEntities.Add(l_entity)
                End If
            End If
        Next
        GlobalVariables.Entities.UpdateList(listEntities)
    End Sub


#End Region

End Class
