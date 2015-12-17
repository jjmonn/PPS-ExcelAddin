﻿' AxisController.vb
'
' Controller for Clients, Products, Adjustments
' To do:
'     
'       - 
'   
'
' Known bugs:
'       -
'
'
' Author: Julien Monnereau
' Created: 04/09/2015
' Last modified: 04/09/2015


Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic
Imports VIBlend.WinForms.DataGridView
Imports VIBlend.WinForms.Controls
Imports CRUD

Friend Class AxisController


#Region "Instance Variables"

    ' Objects
    Private View As AxisView
    Private CrudModel As AxisElemManager
    Private CrudModelFilters As AxisFilterManager

    Private AxisTV As New vTreeView
    Private AxisFilterTV As New vTreeView
    Private AxisFilterValuesTV As New vTreeView
    Private PlatformMGTUI As PlatformMGTGeneralUI

    ' Variables
    Private m_axisType As AxisType
    Private positionsDictionary As New SafeDictionary(Of Int32, Double)

    Public Event AxisCreated(ByRef p_status As Boolean, ByRef p_axisId As Int32)

#End Region


#Region "Initialize"

    ' p_axisId => GlobalEnums.AnalysisAxis (client, product, adjustment)
    Friend Sub New(ByRef p_CrudModel As AxisElemManager, _
                   ByRef p_CrudFilterModel As AxisFilterManager, _
                   ByRef p_axisType As AxisType)

        m_axisType = p_axisType
        CrudModel = p_CrudModel
        CrudModelFilters = p_CrudFilterModel
        LoadInstanceVariables()
        View = New AxisView(Me, AxisTV, AxisFilterValuesTV, AxisFilterTV)

        ' Axis CRUD Events
        AddHandler CrudModel.CreationEvent, AddressOf AfterAxisCreation
        AddHandler CrudModel.DeleteEvent, AddressOf AfterAxisDeletion
        AddHandler CrudModel.UpdateEvent, AddressOf AfterAxisUpdate
        AddHandler CrudModel.Read, AddressOf AfterAxisRead

        ' Axis Filters CRUD Events
        AddHandler CrudModelFilters.Read, AddressOf AfterAxisFilterRead
        AddHandler CrudModelFilters.UpdateEvent, AddressOf AfterAxisFilterUpdate

    End Sub

    Protected Overrides Sub Finalize()
        RemoveHandler CrudModel.CreationEvent, AddressOf AfterAxisCreation
        RemoveHandler CrudModel.DeleteEvent, AddressOf AfterAxisDeletion
        RemoveHandler CrudModel.UpdateEvent, AddressOf AfterAxisUpdate
        RemoveHandler CrudModel.Read, AddressOf AfterAxisRead

        ' Axis Filters CRUD Events
        RemoveHandler CrudModelFilters.Read, AddressOf AfterAxisFilterRead
        RemoveHandler CrudModelFilters.UpdateEvent, AddressOf AfterAxisFilterUpdate
    End Sub

    Friend Sub LoadInstanceVariables()

        CrudModel.LoadAxisTree(m_axisType, AxisTV)
        GlobalVariables.Filters.LoadFiltersTV(AxisFilterTV, m_axisType)
        AxisFilterManager.LoadFvTv(AxisFilterValuesTV, CInt(m_axisType))

    End Sub

    Public Sub AddControlToPanel(ByRef dest_panel As Panel, _
                                 ByRef PlatformMGTUI As PlatformMGTGeneralUI)

        Me.PlatformMGTUI = PlatformMGTUI
        dest_panel.Controls.Add(View)
        View.Dock = Windows.Forms.DockStyle.Fill

    End Sub

    Public Sub close()

        SendNewPositionsToModel()
        View.Dispose()

    End Sub

    Friend Function GetAxisElem(ByRef p_axisType As AxisType, ByVal p_axisId As UInt32) As AxisElem
        Return CrudModel.GetValue(p_axisType, p_axisId)
    End Function

    Friend Function GetAxisElemCopy(ByRef p_axisType As AxisType, ByVal p_axisId As UInt32) As AxisElem
        Dim l_axis = GetAxisElem(p_axisType, p_axisId)

        If l_axis Is Nothing Then Return Nothing
        Return l_axis.Clone()
    End Function

    Friend Function GetAxisDictionary() As MultiIndexDictionary(Of UInt32, String, AxisElem)
        Return CrudModel.GetDictionary(CType(m_axisType, AxisType))
    End Function

    Friend Function GetAxisFilter(ByRef p_axisType As AxisType, ByVal p_axisFilterId As UInt32) As AxisFilter
        Return CrudModelFilters.GetValue(p_axisType, p_axisFilterId)
    End Function

    Friend Function GetAxisFilterCopy(ByRef p_axisType As AxisType, ByVal p_axisId As UInt32) As AxisFilter
        Dim l_axis = GetAxisFilter(p_axisType, p_axisId)

        If l_axis Is Nothing Then Return Nothing
        Return l_axis.Clone()
    End Function

    Friend Function GetAxisFilterDictionary() As MultiIndexDictionary(Of UInt32, Tuple(Of UInt32, UInt32), AxisFilter)
        Return CrudModelFilters.GetDictionary(CType(m_axisType, AxisType))
    End Function

#End Region


#Region "Interface"

    Friend Sub CreateAxis(ByRef p_axisName As String)

        Dim ht As New AxisElem
        ht.Name = p_axisName
        ht.ItemPosition = 0
        ht.Axis = m_axisType
        CrudModel.Create(ht)

    End Sub

    Friend Sub UpdateAxis(ByRef p_axisElem As AxisElem)

        CrudModel.Update(p_axisElem)

    End Sub

    Friend Sub UpdateAxisName(ByVal p_id As UInt32, ByVal p_value As String)
        Dim l_axisElem = GetAxisElemCopy(m_axisType, p_id)

        If l_axisElem Is Nothing Then Exit Sub
        l_axisElem.Name = p_value
        UpdateAxis(l_axisElem)
    End Sub

    Friend Sub DeleteAxis(ByRef p_axisElemId As Int32)

        CrudModel.Delete(p_axisElemId)

    End Sub

    Friend Sub UpdateFilterValue(ByRef p_axisElemId As Int32, _
                                 ByRef filterId As Int32, _
                                 ByRef filterValueId As Int32)
        For Each axisFilter As AxisFilter In GetAxisFilterDictionary().Values
            If axisFilter.FilterId = filterId AndAlso p_axisElemId = axisFilter.AxisElemId Then
                Dim l_copy = GetAxisFilterCopy(m_axisType, axisFilter.Id)
                l_copy.FilterValueId = filterValueId
                UpdateFilterValue(l_copy)
            End If
        Next
    End Sub

    Friend Sub UpdateFilterValue(ByRef p_axisElem As AxisFilter)

        If p_axisElem.FilterId = GlobalVariables.Filters.GetMostNestedFilterId(p_axisElem.FilterId) Then
            CrudModelFilters.Update(p_axisElem)
        End If

    End Sub

#End Region


#Region "Events"

    Private Sub AfterAxisRead(ByRef status As ErrorMessage, ByRef ht As CRUDEntity)

        If (status = ErrorMessage.SUCCESS) Then
            View.LoadInstanceVariables_Safe()
            View.UpdateAxis(ht)
        End If

    End Sub

    Private Sub AfterAxisDeletion(ByRef status As ErrorMessage, ByRef id As UInt32)

        If status = ErrorMessage.SUCCESS Then
            View.LoadInstanceVariables_Safe()
            View.DeleteAxis(id)
        End If

    End Sub

    Private Sub AfterAxisUpdate(ByRef status As ErrorMessage, ByRef id As UInt32)

        If (status <> ErrorMessage.SUCCESS) Then
            '     View.UpdateAxis(CrudModel.GetValue(id))
            MsgBox("Invalid parameter")
        End If

    End Sub

    Private Sub AfterAxisCreation(ByRef p_status As ErrorMessage, ByRef p_id As UInt32)

        If p_status <> ErrorMessage.SUCCESS Then
            MsgBox("The Axis Could not be created.")
            ' catch and display error as well V2 priority normal
        End If
        RaiseEvent AxisCreated(p_status, p_id)

    End Sub

    Private Sub AfterAxisFilterRead(ByRef status As ErrorMessage, ByRef p_axisFilter As CRUDEntity)

        If (status = ErrorMessage.SUCCESS) Then
            Dim axisFilter As AxisFilter = CType(p_axisFilter, AxisFilter)
            View.LoadInstanceVariables_Safe()
            View.UpdateAxis(CrudModel.GetValue(axisFilter.Axis, axisFilter.Id))
        End If

    End Sub

    Private Sub AfterAxisFilterUpdate(ByRef status As ErrorMessage, _
                                        ByRef p_axisFilterId As UInt32)
        If status = ErrorMessage.SUCCESS Then
            View.UpdateAxis(CrudModel.GetValue(p_axisFilterId))
            ' catch and display message
        End If

    End Sub

#End Region


#Region "Utilities"

    Friend Function GetAxisValueId(ByRef name As String) As Int32

        Return CrudModel.GetValueId(m_axisType, name)

    End Function

    Friend Function GetFilterValueId(ByRef filterId As Int32, _
                                     ByRef p_axisElemId As Int32) As Int32

        Return CrudModelFilters.GetFilterValueId(m_axisType, filterId, p_axisElemId)

    End Function

    Friend Sub SendNewPositionsToModel()

        Dim position As Int32
        Dim axisUpdates As New List(Of CRUDEntity)
        positionsDictionary = DataGridViewsUtil.GeneratePositionsDictionary(View.m_axisDataGridView)

        For Each axisId As Int32 In positionsDictionary.Keys
            position = positionsDictionary(axisId)
            Dim axisElem As AxisElem = CrudModel.GetValue(m_axisType, axisId)

            If axisElem Is Nothing Then Continue For
            If position <> axisElem.ItemPosition Then
                axisElem = axisElem.Clone()
                axisElem.ItemPosition = position
                axisUpdates.Add(axisElem)
            End If
        Next
        If axisUpdates.Count > 0 Then CrudModel.UpdateList(axisUpdates)

    End Sub

#End Region



End Class
