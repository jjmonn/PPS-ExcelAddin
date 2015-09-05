' AxisFiltersController.vb
'
' Generic
'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 05/09/2015


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections


Friend Class AxisFiltersController

    ' implementation to be finished !!!!
    ' priority normal 
    '   -> manage actions triggered by tv events -> filters / filters values creation !
    '
    '

#Region "Instance Variables"

    ' Objects
    Private View As AxisFiltersControl
    Private filtersNodes As New TreeNode
    Private filtersFilterValuesTv As New TreeView
  
    ' Variables
    Private axisId As Int32


#End Region


#Region "Initialization"

    Friend Sub New(ByRef p_axis_id As UInt32)

        axisId = p_axis_id
        LoadinstancesVariables()
        View = New AxisFiltersControl(Me, filtersFilterValuesTv)
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

    Private Sub LoadinstancesVariables()

        AxisFilter.LoadFvTv(filtersFilterValuesTv, filtersNodes, axisId)

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
    Friend Function CreateFilter(ByRef filter_name As String) As Boolean

    End Function

    Friend Sub UpdateFilter(ByRef filter_id As String, ByRef field As String, ByRef value As Object)

        ' value !!!!!!!!!!!! != filter
        '    GlobalVariables.Filters.CMSG_UPDATE_FILTER(filter_id, field, value)

    End Sub

    Friend Sub DeleteFilter(ByRef node As TreeNode)


    End Sub


    ' Filters Values
    Friend Function CreateFilterValue(ByRef filter_value_name As String, _
                                      ByRef parent_node As TreeNode, _
                                      Optional ByRef new_filter_value_id As String = "") As Boolean

        ' to be reimplemented
        If GlobalVariables.Filters.IsNameValid(filter_value_name) = False Then Return False
        Dim hash As New Hashtable
        hash.Add(PARENT_ID_VARIABLE, CInt(parent_node.Name))
        hash.Add(NAME_VARIABLE, filter_value_name)
        hash.Add(FILTER_IS_PARENT_VARIABLE, 0)
        hash.Add(ITEMS_POSITIONS, 1)
        GlobalVariables.Filters.CMSG_CREATE_FILTER(hash)
        Return True

    End Function

    Friend Sub UpdateFilterValue()


    End Sub

    Friend Sub DeleteFilterValue(ByRef node As TreeNode)


    End Sub


#End Region


#Region "Events"

    Private Sub AfterFilterRead(ByRef status As Boolean, ByRef ht As Hashtable)

    End Sub

    Private Sub AfterFilterDelete(ByRef status As Boolean, ByRef id As Int32)


    End Sub

    Private Sub AfterFilterCreation(ByRef status As Boolean, ByRef id As Int32)


    End Sub

    Private Sub AfterFilterUpdate(ByRef status As Boolean, ByRef id As Int32)


    End Sub

    Private Sub AfterFilterValueRead(ByRef status As Boolean, ByRef ht As Hashtable)

    End Sub

    Private Sub AfterFilterValueDelete(ByRef status As Boolean, ByRef id As Int32)


    End Sub

    Private Sub AfterFilterValueCreation(ByRef status As Boolean, ByRef id As Int32)


    End Sub

    Private Sub AfterFilterValueUpdate(ByRef status As Boolean, ByRef id As Int32)


    End Sub


#End Region


#Region "Utilities"

    Friend Function IsFilter(ByRef id As UInt32) As Boolean

        Dim result As TreeNode() = filtersNodes.Nodes.Find(CStr(id), True)
        If result.Length > 0 Then Return True
        Return False

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
