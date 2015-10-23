' AxisController.vb
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



Friend Class AxisController


#Region "Instance Variables"

    ' Objects
    Private View As AxisView
    Private CrudModel As AxisElemManager
    Private CrudModelFilters As SuperAxisFilterCRUD

    Private AxisTV As New vTreeView
    Private AxisFilterTV As New vTreeView
    Private AxisFilterValuesTV As New vTreeView
    Private PlatformMGTUI As PlatformMGTGeneralUI

    ' Variables
    Private axisId As Int32
    Private positionsDictionary As New Dictionary(Of Int32, Double)

#End Region


#Region "Initialize"

    Friend Sub New(ByRef p_CrudModel As AxisElemManager, _
                   ByRef p_CrudFilterModel As SuperAxisFilterCRUD, _
                   ByRef p_axisId As Int32)

        axisId = p_axisId
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

        CrudModel.LoadAxisTV(AxisTV)
        GlobalVariables.Filters.LoadFiltersTV(AxisFilterTV, axisId)
        AxisFilter.LoadFvTv(AxisFilterValuesTV, axisId)

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

    Friend Function GetAxis(ByVal p_axisId As UInt32) As AxisElem
        Return CrudModel.GetAxis(p_axisId)
    End Function

    Friend Function GetAxisCopy(ByVal p_axisId As UInt32) As AxisElem
        Dim l_axis = GetAxis(p_axisId)

        If l_axis Is Nothing Then Return Nothing
        Return l_axis.Clone()
    End Function

    Friend Function GetAxisDictionary() As SortedDictionary(Of Int32, AxisElem)
        Return CrudModel.GetAxisDictionary()
    End Function

#End Region


#Region "Interface"

    Friend Sub CreateAxis(ByRef AxisName As String)

        Dim ht As New AxisElem
        ht.Name = AxisName
        ht.ItemPosition = 0
        CrudModel.CMSG_CREATE_AXIS(ht)

    End Sub

    Friend Sub UpdateAxis(ByRef Axis_attributes As AxisElem)

        CrudModel.CMSG_UPDATE_AXIS(Axis_attributes)

    End Sub

    Friend Sub DeleteAxis(ByRef AxisValueId As Int32)

        CrudModel.CMSG_DELETE_AXIS(AxisValueId)

    End Sub

    Friend Sub UpdateFilterValue(ByRef AxisValueId As Int32, _
                                 ByRef filterId As Int32, _
                                 ByRef filterValueId As Int32)

        If filterId = GlobalVariables.Filters.GetMostNestedFilterId(filterId) Then
            CrudModelFilters.CMSG_UPDATE_AXIS_FILTER(AxisValueId, filterId, filterValueId)
        End If

    End Sub

#End Region


#Region "Events"

    Private Sub AfterAxisRead(ByRef status As Boolean, ByRef ht As AxisElem)

        If (status = True) Then
            View.LoadInstanceVariables_Safe()
            View.UpdateAxis(ht)
        End If

    End Sub

    Private Sub AfterAxisDeletion(ByRef status As Boolean, ByRef id As Int32)

        If status = True Then
            View.LoadInstanceVariables_Safe()
            View.DeleteAxis(id)
        End If

    End Sub

    Private Sub AfterAxisUpdate(ByRef status As Boolean, ByRef id As Int32)

        If (status = False) Then
            View.UpdateAxis(CrudModel.GetAxis(id))
            MsgBox("Invalid parameter")
        End If

    End Sub

    Private Sub AfterAxisCreation(ByRef status As Boolean, ByRef id As Int32)

        If status = False Then
            MsgBox("The Axis Could not be created.")
            ' catch and display error as well V2 priority normal
        End If

    End Sub

    Private Sub AfterAxisFilterRead(ByRef status As Boolean, ByRef AxisFilterHT As Hashtable)

        If (status = True) Then
            View.LoadInstanceVariables_Safe()
            View.UpdateAxis(CrudModel.GetAxis(CInt(AxisFilterHT(AXIS_ID_VARIABLE))))
        End If

    End Sub

    Private Sub AfterAxisFilterUpdate(ByRef status As Boolean, _
                                        ByRef AxisValueId As Int32, _
                                        ByRef filterId As Int32, _
                                        ByRef filterValueId As Int32)
        If status = False Then
            View.UpdateAxis(CrudModel.GetAxis(AxisValueId))
            MsgBox("The Axis could be updated.")
            ' catch and display message
        End If

    End Sub

#End Region


#Region "Utilities"

    Friend Function GetAxisValueId(ByRef name As String) As Int32

        Return CrudModel.GetAxisValueId(name)

    End Function

    Friend Function GetFilterValueId(ByRef filterId As Int32, _
                                     ByRef axisValueId As Int32) As Int32

        Return CrudModelFilters.GetFilterValueId(filterId, axisValueId)

    End Function

    Friend Sub SendNewPositionsToModel()

        Dim position As Int32
        Dim axisUpdates As New List(Of AxisElem)
        positionsDictionary = DataGridViewsUtil.GeneratePositionsDictionary(View.DGV)

        For Each axisId As Int32 In positionsDictionary.Keys
            position = positionsDictionary(axisId)

            If CrudModel.GetAxis(axisId) Is Nothing Then Continue For
            If position <> CrudModel.GetAxis(axisId).ItemPosition Then
                axisUpdates.Add(CrudModel.GetAxis(axisId))
            End If
        Next
        CrudModel.CMSG_UPDATE_AXIS_LIST(axisUpdates)

    End Sub

#End Region



End Class
