﻿' EntitiesController.vb
'
' Controller for axis 
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
' Last modified: 07/01/2016


Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic
Imports VIBlend.WinForms.DataGridView
Imports VIBlend.WinForms.Controls
Imports CRUD

Friend Class AxisController

#Region "Instance Variables"

    ' Objects
    Private m_view As AxisView
    Private m_axisTV As New vTreeView
    Private m_axisFilterTV As New vTreeView
    Private m_axisFilterValuesTV As New vTreeView
    Private m_newAxisView As NewAxisUI
    Private m_platformMGTUI As PlatformMGTGeneralUI
    Private m_newAxisNameAxisOwnerDict As New SafeDictionary(Of String, UInt32)

    Private m_axisType As AxisType

    ' Variables
    Private m_positionsDictionary As New SafeDictionary(Of Int32, Double)
    Public Event AxisCreated(ByRef p_status As Boolean, ByRef p_axisId As Int32)

#End Region

#Region "Initialize"

    Friend Sub New(ByRef p_axisType As AxisType)

        m_axisType = p_axisType
        Select Case p_axisType
            Case AxisType.Employee : m_view = New EmployeeView(Me, m_axisFilterValuesTV, m_axisFilterTV)
            Case Else : m_view = New AxisView(Me, m_axisFilterValuesTV, m_axisFilterTV)
        End Select
        m_newAxisView = New NewAxisUI(Me, p_axisType)

        ' Entities CRUD Events
        AddHandler GlobalVariables.AxisElems.CreationEvent, AddressOf AfterAxisElemCreation
        AddHandler GlobalVariables.AxisElems.DeleteEvent, AddressOf AfterAxisElemDeletion
        AddHandler GlobalVariables.AxisElems.UpdateEvent, AddressOf AfterAxisElemUpdate
        AddHandler GlobalVariables.AxisElems.Read, AddressOf AfterAxisElemRead

        ' Entities Filters CRUD Events
        AddHandler GlobalVariables.AxisFilters.Read, AddressOf AfterAxisElemFilterRead
        AddHandler GlobalVariables.AxisFilters.UpdateEvent, AddressOf AfterAxisElemFilterUpdate

        AddHandler GlobalVariables.EntityCurrencies.Read, AddressOf AfterEntityCurrencyRead

    End Sub

    Public Sub LoadInstanceVariables(Optional ByRef p_AxisOwnerId As UInt32 = 0)

        m_axisFilterTV.Nodes.Clear()
        m_axisFilterValuesTV.Nodes.Clear()
        If p_AxisOwnerId <> 0 Then
            GlobalVariables.AxisElems.LoadAxisTree(m_axisType, m_axisTV, p_AxisOwnerId)
        Else
            If m_axisType <> AxisType.Client Then
                GlobalVariables.AxisElems.LoadHierarchyAxisTree(m_axisType, m_axisTV)
            End If
            'GlobalVariables.AxisElems.LoadAxisTree(m_axisType, m_axisTV)
        End If
        GlobalVariables.Filters.LoadFiltersTV(m_axisFilterTV, m_axisType)
        AxisFilterManager.LoadFvTv(m_axisFilterValuesTV, CInt(m_axisType))

    End Sub

    Public Sub addControlToPanel(ByRef dest_panel As Panel, _
                                 ByRef PlatformMGTUI As PlatformMGTGeneralUI)

        Me.m_platformMGTUI = PlatformMGTUI
        dest_panel.Controls.Add(m_view)
        m_view.Dock = Windows.Forms.DockStyle.Fill

    End Sub

    Public Sub close()

        RemoveHandler GlobalVariables.AxisElems.CreationEvent, AddressOf AfterAxisElemCreation
        RemoveHandler GlobalVariables.AxisElems.DeleteEvent, AddressOf AfterAxisElemDeletion
        RemoveHandler GlobalVariables.AxisElems.UpdateEvent, AddressOf AfterAxisElemUpdate
        RemoveHandler GlobalVariables.AxisElems.Read, AddressOf AfterAxisElemRead

        ' Entities Filters CRUD Events
        RemoveHandler GlobalVariables.AxisFilters.Read, AddressOf AfterAxisElemFilterRead
        RemoveHandler GlobalVariables.AxisFilters.UpdateEvent, AddressOf AfterAxisElemFilterUpdate

        RemoveHandler GlobalVariables.EntityCurrencies.Read, AddressOf AfterEntityCurrencyRead
        SendNewPositionsToModel()
        m_view.Dispose()

    End Sub

#End Region

#Region "Interface"

    Friend Sub CreateAxisElem(ByRef p_axisName As String, _
                              Optional ByRef p_AxisOwnerId As Int32 = 0, _
                              Optional ByRef p_allowEdition As Int32 = 1, _
                              Optional ByRef p_AxisOwnerParentId As Int32 = 0)
        If (GlobalVariables.AxisElems.IsNameValid(p_axisName) = False) Then
            MsgBox(Local.GetValue("axis.msg_invalid_name") & "(" & p_axisName & ")")
            Exit Sub
        End If
        If m_axisType <> AxisType.Entities AndAlso p_AxisOwnerParentId <> 0 Then
            m_newAxisNameAxisOwnerDict.Add(p_axisName, p_AxisOwnerParentId)
        End If

        Dim dict As MultiIndexDictionary(Of UInt32, String, AxisElem) = GetAxisDictionary()

        Dim l_axisElem As New AxisElem
        l_axisElem.Name = p_axisName
        l_axisElem.Axis = m_axisType
        l_axisElem.ParentId = p_AxisOwnerId
        If dict Is Nothing OrElse dict.SortedValues.Count() = 0 Then
            l_axisElem.ItemPosition = 0
        Else
            l_axisElem.ItemPosition = dict.SortedValues(dict.SortedValues.Count - 1).ItemPosition + 1
        End If
        l_axisElem.AllowEdition = p_allowEdition
        GlobalVariables.AxisElems.Create(l_axisElem)

    End Sub

    Friend Sub UpdateAxisElem(ByRef entity_attributes As AxisElem)

        GlobalVariables.AxisElems.Update(entity_attributes)

    End Sub

    Friend Sub DeleteAxisElem(ByRef entity_id As Int32)

        GlobalVariables.AxisElems.Delete(entity_id)

    End Sub

    Friend Sub UpdateAxisFilter(ByRef p_axisElemId As Int32, _
                                ByRef p_filterId As Int32, _
                                ByRef p_filterValueId As Int32)

        Dim l_filter As AxisFilter = GlobalVariables.AxisFilters.GetValue(m_axisType, p_axisElemId, p_filterId)
        If l_filter Is Nothing Then Exit Sub
        Dim l_copy = l_filter.Clone
        l_copy.FilterValueId = p_filterValueId
        UpdateAxisFilter(l_copy)

    End Sub

    Friend Sub UpdateAxisFilter(ByRef p_axisFilter As AxisFilter)

        If p_axisFilter.FilterId = GlobalVariables.Filters.GetMostNestedFilterId(p_axisFilter.FilterId) Then
            GlobalVariables.AxisFilters.Update(p_axisFilter)
        End If

    End Sub

    Friend Function GetAxisFilterCopy(ByVal p_axisId As UInt32) As AxisFilter
        Dim l_axis = GetAxisFilter(p_axisId)
        If l_axis Is Nothing Then Return Nothing
        Return l_axis.Clone()
    End Function

    Friend Function GetAxisFilter(ByVal p_axisFilterId As UInt32) As AxisFilter
        Return GlobalVariables.AxisFilters.GetValue(m_axisType, p_axisFilterId)
    End Function

    'Friend Function GetAxisFilterDictionary() As MultiIndexDictionary(Of UInt32, Tuple(Of UInt32, UInt32), AxisFilter)
    '    Return GlobalVariables.AxisFilters.GetDictionary(m_axisType)
    'End Function

    Friend Sub UpdateAxisElemName(ByVal p_id As UInt32, ByVal p_value As String)
        Dim l_entity = GetAxisElemCopy(p_id)

        If l_entity Is Nothing Then Exit Sub
        l_entity.Name = p_value
        UpdateAxisElem(l_entity)
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

    Private Sub AfterEntityCurrencyRead(ByRef status As ErrorMessage, ByRef p_entityCurrency As CRUDEntity)

        If m_axisType = AxisType.Entities _
        AndAlso (status = ErrorMessage.SUCCESS) Then
            Dim l_entity As AxisElem = GetAxisElem(p_entityCurrency.Id)
            If l_entity Is Nothing Then Exit Sub

            m_view.UpdateAxisElem(l_entity)
        End If

    End Sub

    Private Sub AfterAxisElemRead(ByRef status As ErrorMessage, ByRef p_axisElem As CRUDEntity)

        Dim l_axisElem As AxisElem = p_axisElem
        If l_axisElem.Axis = m_axisType _
        AndAlso (status = ErrorMessage.SUCCESS) Then
            m_view.LoadInstanceVariables()
            m_view.UpdateAxisElem(l_axisElem)
            Dim l_node As vTreeNode = VTreeViewUtil.FindNode(m_newAxisView.m_parentAxisElemTreeviewBox.TreeView, l_axisElem.Id)
            If l_node Is Nothing Then
                m_newAxisView.AxisNodeAddition(l_axisElem.Id, _
                                                   l_axisElem.ParentId, _
                                                   l_axisElem.Name, _
                                                   l_axisElem.Image)
            Else
                m_newAxisView.TVUpdate(l_node, _
                                         l_axisElem.Name, _
                                         l_axisElem.Image)
            End If
        End If

    End Sub

    Private Sub AfterAxisElemDeletion(ByRef status As ErrorMessage, ByRef id As UInt32)

        If status = ErrorMessage.SUCCESS Then
            If GlobalVariables.AxisElems.GetValue(m_axisType, id) Is Nothing Then Exit Sub
            m_view.LoadInstanceVariables()
            m_view.DeleteAxisElem(id)
            m_newAxisView.TVNodeDelete(id)
        End If

    End Sub

    Private Sub AfterAxisElemUpdate(ByRef status As ErrorMessage, ByRef id As UInt32)

        If (status <> ErrorMessage.SUCCESS) Then
            If GlobalVariables.AxisElems.GetValue(m_axisType, id) Is Nothing Then Exit Sub
            m_view.UpdateAxisElem(GlobalVariables.AxisElems.GetValue(m_axisType, id))
            MsgBox(Local.GetValue("axis.msg_unable_update"))
        End If

    End Sub

    Private Sub AfterAxisElemCreation(ByRef p_status As ErrorMessage, ByRef p_id As UInt32)

        If p_status <> ErrorMessage.SUCCESS Then
            MsgBox(Local.GetValue("axis.msg_unable_create"))
        Else
            Dim l_axisElem As AxisElem = GlobalVariables.AxisElems.GetValue(m_axisType, p_id)
            If l_axisElem IsNot Nothing _
            AndAlso l_axisElem.Axis = m_axisType _
            AndAlso m_newAxisNameAxisOwnerDict.ContainsKey(l_axisElem.Name) Then
                Dim l_AxisOwner As New AxisOwner()
                l_AxisOwner.Id = l_axisElem.Id
                l_AxisOwner.OwnerId = m_newAxisNameAxisOwnerDict(l_axisElem.Name)
                GlobalVariables.AxisOwners.Create(l_AxisOwner)
                m_newAxisNameAxisOwnerDict.Remove(l_axisElem.Name)
            End If

            RaiseEvent AxisCreated(p_status, p_id)

        End If

    End Sub

    Private Sub AfterAxisElemFilterRead(ByRef status As ErrorMessage, ByRef p_axisFilter As CRUDEntity)

        If (status = ErrorMessage.SUCCESS) Then
            Dim axisFilter As AxisFilter = CType(p_axisFilter, AxisFilter)
            If axisFilter.Axis <> m_axisType Then Exit Sub
            Dim entityId As Int32 = p_axisFilter.Id
            If Not GlobalVariables.AxisElems.GetValue(m_axisType, entityId) Is Nothing Then
                m_view.LoadInstanceVariables()
                m_view.UpdateAxisElem(GlobalVariables.AxisElems.GetValue(m_axisType, entityId))
            End If
        End If

    End Sub

    Private Sub AfterAxisElemFilterUpdate(ByRef status As ErrorMessage, _
                                        ByRef axisFilterId As UInt32)
        If status <> ErrorMessage.SUCCESS Then
            Dim l_axisFilter As AxisFilter = GlobalVariables.AxisFilters.GetValue(axisFilterId)
            If l_axisFilter.Axis <> m_axisType Then Exit Sub
            If GlobalVariables.AxisElems.GetValue(m_axisType, l_axisFilter.AxisElemId) Is Nothing Then Exit Sub
            m_view.UpdateAxisElem(GlobalVariables.AxisElems.GetValue(m_axisType, l_axisFilter.AxisElemId))
            ' catch and display message
        End If

    End Sub

#End Region

#Region "Utilities"

    Friend Function GetAxisDictionary(Optional ByRef p_AxisOwnerId As UInt32 = 0) As MultiIndexDictionary(Of UInt32, String, AxisElem)

        If p_AxisOwnerId <> 0 Then
            Return GlobalVariables.AxisElems.GetDictionary(CType(m_axisType, AxisType), p_AxisOwnerId)
        Else
            Return GlobalVariables.AxisElems.GetDictionary(CType(m_axisType, AxisType))
        End If

    End Function

    Friend Function GetAxisElem(ByVal p_id As UInt32) As AxisElem
        Return GlobalVariables.AxisElems.GetValue(m_axisType, p_id)
    End Function

    Friend Function GetAxisElemCopy(ByVal p_id As UInt32) As AxisElem
        Dim l_entity As AxisElem = GetAxisElem(p_id)

        If l_entity Is Nothing Then Return Nothing
        Return l_entity.Clone()
    End Function

    Friend Sub ShowNewAxisElemUI(Optional ByRef p_AxisOwnerParentId As Int32 = 0)

        Dim current_row As HierarchyItem = m_view.getCurrentRowItem
        If Not current_row Is Nothing Then
            Dim node As vTreeNode = VTreeViewUtil.FindNode(m_axisTV, current_row.ItemValue)
            If node IsNot Nothing Then
                m_newAxisView.SetAxisOwnerParentId(p_AxisOwnerParentId)
                m_newAxisView.SetParentAxisId(node.Value)
            End If
        End If

        m_newAxisView.Show()

    End Sub

    Friend Sub ShowEntitiesMGT()

        m_view.Show()

    End Sub

    Friend Function GetAxisElemId(ByRef name As String) As Int32

        Return GlobalVariables.AxisElems.GetValueId(m_axisType, name)

    End Function

    Friend Function GetAxisType() As AxisType
        Return m_axisType
    End Function

    Friend Sub SendNewPositionsToModel()

        Dim position As Int32
        Dim listEntities As New List(Of CRUDEntity)

        m_positionsDictionary = DataGridViewsUtil.GeneratePositionsDictionary(m_view.m_axisDataGridView)
        For Each entity_id As Int32 In m_positionsDictionary.Keys
            position = m_positionsDictionary(entity_id)
            If GetAxisElem(entity_id) Is Nothing Then Continue For
            If position <> GetAxisElem(entity_id).ItemPosition Then
                Dim l_entity = GetAxisElemCopy(entity_id)

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
