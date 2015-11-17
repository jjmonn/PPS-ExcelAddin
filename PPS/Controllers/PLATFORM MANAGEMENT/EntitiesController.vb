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
    Private m_view As EntitiesView
    Private m_entitiesTV As New vTreeView
    Private m_entitiesFilterTV As New vTreeView
    Private m_entitiesFilterValuesTV As New vTreeView
    Private m_newEntityView As NewEntityUI
    Private m_platformMGTUI As PlatformMGTGeneralUI

    ' Variables
    Private m_positionsDictionary As New Dictionary(Of Int32, Double)

#End Region

#Region "Initialize"

    Friend Sub New()

        LoadInstanceVariables()
        m_view = New EntitiesView(Me, m_entitiesTV, m_entitiesFilterValuesTV, m_entitiesFilterTV)
        m_newEntityView = New NewEntityUI(Me, GlobalVariables.Currencies.GetDictionary())

        ' Entities CRUD Events
        AddHandler GlobalVariables.AxisElems.CreationEvent, AddressOf AfterEntityCreation
        AddHandler GlobalVariables.AxisElems.DeleteEvent, AddressOf AfterEntityDeletion
        AddHandler GlobalVariables.AxisElems.UpdateEvent, AddressOf AfterEntityUpdate
        AddHandler GlobalVariables.AxisElems.Read, AddressOf AfterEntityRead

        ' Entities Filters CRUD Events
        AddHandler GlobalVariables.AxisFilters.Read, AddressOf AfterEntityFilterRead
        AddHandler GlobalVariables.AxisFilters.UpdateEvent, AddressOf AfterEntityFilterUpdate

        AddHandler GlobalVariables.EntityCurrencies.Read, AddressOf AfterEntityCurrencyRead

    End Sub

    Public Sub LoadInstanceVariables()

        GlobalVariables.AxisElems.LoadEntitiesTV(m_entitiesTV)
        GlobalVariables.Filters.LoadFiltersTV(m_entitiesFilterTV, GlobalEnums.AnalysisAxis.ENTITIES)
        AxisFilterManager.LoadFvTv(m_entitiesFilterValuesTV, GlobalEnums.AnalysisAxis.ENTITIES)

    End Sub

    Public Sub addControlToPanel(ByRef dest_panel As Panel, _
                                 ByRef PlatformMGTUI As PlatformMGTGeneralUI)

        Me.m_platformMGTUI = PlatformMGTUI
        dest_panel.Controls.Add(m_view)
        m_view.Dock = Windows.Forms.DockStyle.Fill

    End Sub

    Public Sub close()

        RemoveHandler GlobalVariables.AxisFilters.Read, AddressOf AfterEntityFilterRead
        RemoveHandler GlobalVariables.AxisFilters.UpdateEvent, AddressOf AfterEntityFilterUpdate
        SendNewPositionsToModel()
        m_view.Dispose()

    End Sub

#End Region

#Region "Interface"

    Friend Sub CreateEntity(ByRef entityName As String, _
                            ByRef parentEntityId As Int32, _
                            ByRef position As Int32, _
                            ByRef allowEdition As Int32)

        Dim l_entity As New AxisElem

        l_entity.Name = entityName
        l_entity.Axis = AxisType.Entities
        l_entity.ParentId = parentEntityId
        l_entity.ItemPosition = position
        l_entity.AllowEdition = allowEdition
        GlobalVariables.AxisElems.Create(l_entity)

    End Sub

    Friend Sub UpdateEntity(ByRef entity_attributes As AxisElem)

        GlobalVariables.AxisElems.Update(entity_attributes)

    End Sub

    Friend Sub DeleteEntity(ByRef entity_id As Int32)

        GlobalVariables.AxisElems.Delete(entity_id)

    End Sub

    Friend Sub UpdateAxisFilter(ByRef p_axisElemId As Int32, _
                             ByRef filterId As Int32, _
                             ByRef filterValueId As Int32)
        For Each axisFilter As AxisFilter In GetAxisFilterDictionary().Values
            If axisFilter.FilterId = filterId AndAlso axisFilter.AxisElemId = p_axisElemId Then
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

    Friend Function GetAxisFilterDictionary() As MultiIndexDictionary(Of UInt32, Tuple(Of UInt32, UInt32), AxisFilter)
        Return GlobalVariables.AxisFilters.GetDictionary(AxisType.Entities)
    End Function

    Friend Sub UpdateEntityName(ByVal p_id As UInt32, ByVal p_value As String)
        Dim l_entity = GetEntityCopy(p_id)

        If l_entity Is Nothing Then Exit Sub
        l_entity.Name = p_value
        UpdateEntity(l_entity)
    End Sub

    Friend Sub UpdateEntityCurrency(ByVal p_id As UInt32, ByVal p_value As UInt32)
        Dim l_entityCurrency As EntityCurrency = GlobalVariables.EntityCurrencies.GetValue(p_id)

        If l_entityCurrency Is Nothing Then Exit Sub
        l_entityCurrency = l_entityCurrency.Clone()

        l_entityCurrency.CurrencyId = p_value
        GlobalVariables.EntityCurrencies.Update(l_entityCurrency)
    End Sub

#End Region

#Region "Events"

    Private Sub AfterEntityCurrencyRead(ByRef status As ErrorMessage, ByRef ht As EntityCurrency)

        If (status = ErrorMessage.SUCCESS) Then
            Dim l_entity As AxisElem = GetEntity(ht.Id)
            If l_entity Is Nothing Then Exit Sub

            m_view.UpdateEntity(l_entity)
        End If

    End Sub

    Private Sub AfterEntityRead(ByRef status As ErrorMessage, ByRef p_axisElem As AxisElem)

        If (status = ErrorMessage.SUCCESS) Then
            m_view.LoadInstanceVariables_Safe()
            m_view.UpdateEntity(p_axisElem)
            Dim l_node As vTreeNode = VTreeViewUtil.FindNode(m_newEntityView.m_parentEntitiesTreeviewBox.TreeView, p_axisElem.Id)
            If l_node Is Nothing Then
                m_newEntityView.entityNodeAddition(p_axisElem.Id, _
                                                   p_axisElem.ParentId, _
                                                   p_axisElem.Name, _
                                                   p_axisElem.Image)
            Else
                m_newEntityView.TVUpdate(l_node, _
                                         p_axisElem.Name, _
                                         p_axisElem.Image)
            End If
        End If

    End Sub

    Private Sub AfterEntityDeletion(ByRef status As ErrorMessage, ByRef id As Int32)

        If status = ErrorMessage.SUCCESS Then
            m_view.LoadInstanceVariables_Safe()
            m_view.DeleteEntity(id)
            m_newEntityView.TVNodeDelete(id)
        End If

    End Sub

    Private Sub AfterEntityUpdate(ByRef status As ErrorMessage, ByRef id As Int32)

        If (status <> ErrorMessage.SUCCESS) Then
            If GlobalVariables.AxisElems.GetValue(AxisType.Entities, id) Is Nothing Then Exit Sub
            m_view.UpdateEntity(GlobalVariables.AxisElems.GetValue(AxisType.Entities, id))
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
            Dim axisFilter As AxisFilter = CType(p_axisFilter, AxisFilter)
            If axisFilter.Axis <> AxisType.Entities Then Exit Sub
            Dim entityId As Int32 = p_axisFilter.Id
            If Not GlobalVariables.AxisElems.GetValue(AxisType.Entities, entityId) Is Nothing Then
                m_view.LoadInstanceVariables()
                m_view.UpdateEntity(GlobalVariables.AxisElems.GetValue(AxisType.Entities, entityId))
            End If
        End If

    End Sub

    Private Sub AfterEntityFilterUpdate(ByRef status As ErrorMessage, _
                                        ByRef axisFilterId As UInt32)
        If status <> ErrorMessage.SUCCESS Then
            Dim l_axisFilter As AxisFilter = GlobalVariables.AxisFilters.GetValue(axisFilterId)
            If l_axisFilter.Axis <> CRUD.AxisType.Entities Then Exit Sub
            If GlobalVariables.AxisElems.GetValue(AxisType.Entities, l_axisFilter.AxisElemId) Is Nothing Then Exit Sub
            m_view.UpdateEntity(GlobalVariables.AxisElems.GetValue(AxisType.Entities, l_axisFilter.AxisElemId))
            ' catch and display message
        End If

    End Sub

#End Region

#Region "Utilities"

    Friend Function GetEntity(ByVal p_id As UInt32) As AxisElem
        Return GlobalVariables.AxisElems.GetValue(AxisType.Entities, p_id)
    End Function

    Friend Function GetEntityCopy(ByVal p_id As UInt32) As AxisElem
        Dim l_entity As AxisElem = GetEntity(p_id)

        If l_entity Is Nothing Then Return Nothing
        Return l_entity.Clone()
    End Function

    Friend Sub ShowNewEntityUI()

        Dim current_row As HierarchyItem = m_view.getCurrentRowItem
        If Not current_row Is Nothing Then
            Dim node As vTreeNode = VTreeViewUtil.FindNode(m_entitiesTV, current_row.ItemValue)
            If node IsNot Nothing Then
                m_newEntityView.SetParentEntityId(node.Value)
            End If
        End If

        m_newEntityView.Show()

    End Sub

    Friend Sub ShowEntitiesMGT()

        m_view.Show()

    End Sub

    Friend Function GetEntityId(ByRef name As String) As Int32

        Return GlobalVariables.AxisElems.GetValueId(AxisType.Entities, name)

    End Function

    Friend Sub SendNewPositionsToModel()

        Dim position As Int32
        Dim listEntities As New List(Of CRUDEntity)

        m_positionsDictionary = DataGridViewsUtil.GeneratePositionsDictionary(m_view.m_entitiesDataGridView)
        For Each entity_id As Int32 In m_positionsDictionary.Keys
            position = m_positionsDictionary(entity_id)
            If GetEntity(entity_id) Is Nothing Then Continue For
            If position <> GetEntity(entity_id).ItemPosition Then
                Dim l_entity = GetEntityCopy(entity_id)

                If Not l_entity Is Nothing Then
                    l_entity.ItemPosition = position
                    listEntities.Add(l_entity)
                End If
            End If
        Next
        If listEntities.Count > 0 Then GlobalVariables.AxisElems.UpdateList(listEntities)
    End Sub


#End Region

End Class
