' AxisFiltersController.vb
'
' Generic
'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 08/09/2015


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections


Friend Class AxisFiltersController


#Region "Instance Variables"

    ' Objects
    Private View As AxisFiltersControl
    Private filtersNode As New TreeNode
    Private filtersFilterValuesTv As New TreeView
  
    ' Variables
    Private axisId As Int32


#End Region


#Region "Initialization"

    Friend Sub New(ByRef p_axis_id As UInt32)

        axisId = p_axis_id
        AxisFilter.LoadFvTv(filtersFilterValuesTv, filtersNode, axisId)
        If filtersFilterValuesTv.Nodes.Count > 0 Then
            filtersFilterValuesTv.Nodes(0).Name = "filterId" & filtersFilterValuesTv.Nodes(0).Name
        End If

        View = New AxisFiltersControl(Me, filtersNode, axisId, filtersFilterValuesTv)
        View.Show()

        AddHandler GlobalVariables.Filters.CreationEvent, AddressOf AfterFilterCreation
        AddHandler GlobalVariables.Filters.Read, AddressOf AfterFilterRead
        AddHandler GlobalVariables.Filters.UpdateEvent, AddressOf AfterFilterUpdate
        AddHandler GlobalVariables.Filters.DeleteEvent, AddressOf AfterFilterDelete

        AddHandler GlobalVariables.FiltersValues.CreationEvent, AddressOf AfterFilterValueCreation
        AddHandler GlobalVariables.FiltersValues.Read, AddressOf AfterFilterValueRead
        AddHandler GlobalVariables.FiltersValues.UpdateEvent, AddressOf AfterFilterValueUpdate
        AddHandler GlobalVariables.FiltersValues.DeleteEvent, AddressOf AfterFilterValueDelete

    End Sub

    Public Sub addControlToPanel(ByRef dest_panel As Panel, ByRef PlatformMgtUI As PlatformMGTGeneralUI)

        dest_panel.Controls.Add(View)
        View.Dock = Windows.Forms.DockStyle.Fill

    End Sub

    Public Sub close()

        View.closeControl()
        View.Dispose()

    End Sub

#End Region


#Region "Interface"

    ' Filters
    Friend Function CreateFilter(ByRef p_filterName As String, _
                                 ByRef p_parentFilterid As Int32, _
                                 ByRef p_isParent As Int32) As Boolean

        Dim ht As New Hashtable
        ht.Add(NAME_VARIABLE, p_filterName)
        ht.Add(PARENT_ID_VARIABLE, p_parentFilterid)
        ht.Add(AXIS_ID_VARIABLE, axisId)
        ht.Add(ITEMS_POSITIONS, 1)
        ht.Add(FILTER_IS_PARENT_VARIABLE, p_isParent)
        GlobalVariables.Filters.CMSG_CREATE_FILTER(ht)

    End Function

    Friend Sub UpdateFilter(ByRef filterId As Int32, ByRef field As String, ByRef value As Object)

        Dim ht As Hashtable = GlobalVariables.Filters.filters_hash(filterid).clone
        ht(field) = value
        GlobalVariables.Filters.CMSG_UPDATE_FILTER(ht)

    End Sub

    Friend Sub UpdateFiltersBatch()


    End Sub

    Friend Sub DeleteFilter(ByRef filterId As Int32)

        GlobalVariables.Filters.CMSG_DELETE_FILTER(filterId)

    End Sub


    ' Filters Values
    Friend Sub CreateFilterValue(ByRef filterValueName As String, _
                                ByRef filterId As Int32, _
                                ByRef parentFilterValueId As Int32)

        Dim ht As New Hashtable
        ht.Add(NAME_VARIABLE, filterValueName)
        ht.Add(FILTER_ID_VARIABLE, filterId)
        ht.Add(PARENT_FILTER_VALUE_ID_VARIABLE, parentFilterValueId)
        ht.Add(ITEMS_POSITIONS, 1)
        GlobalVariables.FiltersValues.CMSG_CREATE_FILTER_VALUE(ht)

    End Sub

    Friend Sub UpdateFilterValue(ByRef filterId As Int32, _
                                 ByRef variable As String, _
                                 ByRef value As Object)

        Dim ht As Hashtable = GlobalVariables.FiltersValues.filtervalues_hash(filterId).clone
        ht(variable) = value
        GlobalVariables.FiltersValues.CMSG_UPDATE_FILTER_VALUE(ht)

    End Sub

    Friend Sub UpdateFilterValuesBatch(ByRef filtersValuesUpdates As List(Of Tuple(Of Int32, String, Int32)))

        'Dim filterValuesHTUpdates As New List(Of Hashtable)
        'For Each tuple_ As Tuple(Of Int32, String, Int32) In filtersValuesUpdates
        '    Dim ht As Hashtable = GlobalVariables.FiltersValues.filtervalues_hash(tuple_.Item1).clone
        '    ht(tuple_.Item2) = tuple_.Item3
        '    filterValuesHTUpdates.Add(ht)
        'Next
        '    GlobalVariables.filtervalues.(filterValuesHTUpdates)
        ' bacth not implemented on server ?

    End Sub

    Friend Sub DeleteFilterValue(ByRef filterValueId As Int32)

        GlobalVariables.FiltersValues.CMSG_DELETE_FILTER_VALUE(filterValueId)

    End Sub

#End Region


#Region "Events"

    Private Sub AfterFilterRead(ByRef status As Boolean, ByRef ht As Hashtable)

        If status = True Then
            View.UpdateFiltersValuesTV()
        End If

    End Sub

    Private Sub AfterFilterDelete(ByRef status As Boolean, ByRef id As Int32)

        If status = True Then
            View.UpdateFiltersValuesTV()
        End If

    End Sub

    Private Sub AfterFilterCreation(ByRef status As Boolean, ByRef id As Int32)

        If status = False Then
            MsgBox("The Filter could not be created.")
        End If

    End Sub

    Private Sub AfterFilterUpdate(ByRef status As Boolean, ByRef id As Int32)

        If status = False Then
            MsgBox("The Filter could not be updated.")
        End If

    End Sub

    Private Sub AfterFilterValueRead(ByRef status As Boolean, ByRef ht As Hashtable)

        If status = True Then
            View.UpdateFiltersValuesTV()
        End If

    End Sub

    Private Sub AfterFilterValueDelete(ByRef status As Boolean, ByRef id As Int32)

        If status = True Then
            View.UpdateFiltersValuesTV()
        End If

    End Sub

    Private Sub AfterFilterValueCreation(ByRef status As Boolean, ByRef id As Int32)

        If status = False Then
            MsgBox("The Filter Value could not be created.")
        End If

    End Sub

    Private Sub AfterFilterValueUpdate(ByRef status As Boolean, ByRef id As Int32)

        If status = False Then
            MsgBox("The Filter Value could not be updated.")
        End If

    End Sub


#End Region


#Region "Utilities"

    Friend Function IsFilter(ByRef id As Int32) As Boolean

        If GlobalVariables.Filters.filters_hash.ContainsKey(id) = True Then
            Return True
        Else
            Return False
        End If

    End Function

    Friend Sub SendNewPositionsToModel()

        ' positions update 
        '       -> filters
        '       -> filters values
        'priority normal
        ' !!!

        'positions_dictionary = TreeViewsUtilities.GeneratePositionsDictionary(filtersTV)
        'Dim batch_update As New List(Of Object())
        'For Each filter_id In positions_dictionary.Keys
        '    batch_update.Add({filter_id, ITEMS_POSITIONS, positions_dictionary(filter_id)})
        'Next
        'GlobalVariables.Filters.UpdateBatch(batch_update)
        ' CP while updating
        ' review ! priority normal

    End Sub

#End Region



End Class
