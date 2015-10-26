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
    Private CrudModel As SuperAxisCRUD
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

    ' p_axisId => GlobalEnums.AnalysisAxis (client, product, adjustment)
    Friend Sub New(ByRef p_CrudModel As SuperAxisCRUD, _
                   ByRef p_CrudFilterModel As SuperAxisFilterCRUD, _
                   ByRef p_axisId As Int32)

        axisId = p_axisId
        CrudModel = p_CrudModel
        CrudModelFilters = p_CrudFilterModel
        LoadInstanceVariables()
        View = New AxisView(Me, CrudModel.Axis_hash, AxisTV, AxisFilterValuesTV, AxisFilterTV)

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

#End Region


#Region "Interface"

    Friend Sub CreateAxis(ByRef AxisName As String)

        Dim ht As New Hashtable
        ht.Add(NAME_VARIABLE, AxisName)
        CrudModel.CMSG_CREATE_AXIS(ht)

    End Sub

    Friend Sub UpdateAxis(ByRef id As Int32, ByRef variable As String, ByVal value As Object)

        Dim ht As Hashtable = CrudModel.Axis_hash(id).Clone()
        ht(variable) = value
        CrudModel.CMSG_UPDATE_AXIS(ht)

    End Sub

    Friend Sub UpdateAxis(ByRef id As String, ByRef Axis_attributes As Hashtable)

        Dim ht As Hashtable = CrudModel.Axis_hash(id).Clone()
        For Each attribute As String In Axis_attributes
            ht(attribute) = Axis_attributes(attribute)
        Next
        CrudModel.CMSG_UPDATE_AXIS(ht)

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

    Friend Sub UpdateBatch(ByRef p_axisUpdates As List(Of Tuple(Of Int32, String, Int32)))

        Dim axisHTUpdates As New Hashtable
        For Each tuple_ As Tuple(Of Int32, String, Int32) In p_axisUpdates
            Dim ht As Hashtable = CrudModel.Axis_hash(tuple_.Item1).Clone
            ht(tuple_.Item2) = tuple_.Item3
            axisHTUpdates(CInt(ht(ID_VARIABLE))) = ht
        Next
        CrudModel.CMSG_UPDATE_AXIS_LIST(axisHTUpdates)

    End Sub

#End Region


#Region "Events"

    Private Sub AfterAxisRead(ByRef status As Boolean, ByRef ht As Hashtable)

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
            View.UpdateAxis(CrudModel.Axis_hash(id))
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
            View.UpdateAxis(CrudModel.Axis_hash(CInt(AxisFilterHT(AXIS_ID_VARIABLE))))
        End If

    End Sub

    Private Sub AfterAxisFilterUpdate(ByRef status As Boolean, _
                                        ByRef AxisValueId As Int32, _
                                        ByRef filterId As Int32, _
                                        ByRef filterValueId As Int32)
        If status = False Then
            View.UpdateAxis(CrudModel.Axis_hash(AxisValueId))
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
        Dim axisUpdates As New List(Of Tuple(Of Int32, String, Int32))
        positionsDictionary = DataGridViewsUtil.GeneratePositionsDictionary(View.DGV)

        For Each axisId As Int32 In positionsDictionary.Keys
            position = positionsDictionary(axisId)

            If CrudModel.Axis_hash.ContainsKey(axisId) = False Then Continue For
            If position <> CrudModel.Axis_hash(axisId)(ITEMS_POSITIONS) Then
                Dim tuple_ As New Tuple(Of Int32, String, Int32)(axisId, ITEMS_POSITIONS, position)
                axisUpdates.Add(tuple_)
            End If
        Next
        UpdateBatch(axisUpdates)

    End Sub

#End Region



End Class
