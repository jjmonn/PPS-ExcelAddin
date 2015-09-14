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



Friend Class AxisController


#Region "Instance Variables"

    ' Objects
    Private View As AxisControl
    Private CrudModel As SuperAxisCRUD
    Private CrudModelFilters As SuperAxisFilterCRUD

    Private AxisTV As New TreeView
    Private AxisFilterTV As New TreeView
    Private AxisFilterValuesTV As New TreeView
    Private PlatformMGTUI As PlatformMGTGeneralUI

    ' Variables
    Private axisId As Int32
    Private positionsDictionary As New Dictionary(Of Int32, Double)

#End Region


#Region "Initialize"

    Friend Sub New(ByRef p_CrudModel As SuperAxisCRUD, _
                   ByRef p_CrudFilterModel As SuperAxisFilterCRUD, _
                   ByRef p_axisId As Int32)

        axisId = p_axisId
        CrudModel = p_CrudModel
        CrudModelFilters = p_CrudFilterModel
        LoadInstanceVariables()
        View = New AxisControl(Me, CrudModel.Axis_hash, AxisTV, AxisFilterValuesTV, AxisFilterTV)

        ' Axis CRUD Events
        AddHandler CrudModel.CreationEvent, AddressOf AfterAxisCreation
        AddHandler CrudModel.DeleteEvent, AddressOf AfterAxisDeletion
        AddHandler CrudModel.UpdateEvent, AddressOf AfterAxisUpdate
        AddHandler CrudModel.Read, AddressOf AfterAxisRead

        ' Axis Filters CRUD Events
        AddHandler CrudModelFilters.Read, AddressOf AfterAxisFilterRead
        AddHandler CrudModelFilters.UpdateEvent, AddressOf AfterAxisFilterUpdate

    End Sub

    Private Sub LoadInstanceVariables()

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

    Friend Sub DeleteAxis(ByRef Axis_id As Int32)

        CrudModel.CMSG_DELETE_AXIS(Axis_id)

    End Sub

    Friend Sub UpdateFilterValue(ByRef AxisId As Int32, _
                                 ByRef filterId As Int32, _
                                 ByRef filterValueId As Int32)

        If filterId = GlobalVariables.Filters.GetMostNestedFilterId(filterId) Then
            CrudModelFilters.CMSG_UPDATE_AXIS_FILTER(AxisId, filterId, filterValueId)
        End If

    End Sub

#End Region


#Region "Events"

    Private Sub AfterAxisRead(ByRef status As Boolean, ByRef ht As Hashtable)

        If (status = True) Then
            LoadInstanceVariables()
            View.UpdateAxis(ht)
        End If

    End Sub

    Private Sub AfterAxisDeletion(ByRef status As Boolean, ByRef id As Int32)

        If status = True Then
            LoadInstanceVariables()
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
            LoadInstanceVariables()
            View.UpdateAxis(CrudModel.Axis_hash(CInt(AxisFilterHT(AXIS_ID_VARIABLE))))
        End If

    End Sub

    Private Sub AfterAxisFilterUpdate(ByRef status As Boolean, _
                                        ByRef AxisId As Int32, _
                                        ByRef filterId As Int32, _
                                        ByRef filterValueId As Int32)
        If status = False Then
            View.UpdateAxis(CrudModel.Axis_hash(AxisId))
            MsgBox("The Axis could be updated.")
            ' catch and display message
        End If

    End Sub

#End Region


#Region "Utilities"

    Friend Function GetAxisId(ByRef name As String) As Int32

        Return CrudModel.GetAxisId(name)

    End Function

    Friend Function GetFilterValueId(ByRef filterId As Int32, _
                                     ByRef axisValueId As Int32) As Int32

        Return CrudModelFilters.GetFilterValueId(filterId, axisValueId)

    End Function

#End Region



End Class
