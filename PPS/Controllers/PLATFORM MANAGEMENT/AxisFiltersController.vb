' AxisFiltersController.vb
'
' Generic
'
' To do: 
'
'
'
' Author: Julien Monnereau
' Last modified: 15/10/2015


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections
Imports VIBlend.WinForms.Controls


Friend Class AxisFiltersController


#Region "Instance Variables"

    ' Objects
    Private m_view As AxisFiltersView
    Private m_filtersNode As New vTreeNode
    '    Private m_filtersFilterValuesTv As vTreeView
    Private m_filterTV As New vTreeView

    ' Variables
    Private m_axisId As Int32
    Friend Const m_FilterTag As String = "filterId"
    Private m_editFilterStructUI As AxisFilterStructView
    Private m_isEditingFiltersStructure As Boolean

#End Region


#Region "Initialization"

    Friend Sub New(ByRef p_axis_id As UInt32)

        m_axisId = p_axis_id
        m_view = New AxisFiltersView(Me, m_filtersNode, m_axisId)

        GlobalVariables.Filters.LoadFiltersTV(m_filterTV, m_axisId)
        m_editFilterStructUI = New AxisFilterStructView(m_filterTV, m_axisId, Me, m_filtersNode)

        ' Event Handlers
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

        dest_panel.Controls.Add(m_view)
        m_view.Dock = Windows.Forms.DockStyle.Fill

    End Sub

    Public Sub Close()

        m_view.closeControl()
        ' m_view.Dispose()
        m_view.Hide()

    End Sub

#End Region


#Region "Interface"

    Friend Sub ShowEditStructure()
        Try
            m_editFilterStructUI.Show()
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine(ex.Message)
        End Try
    End Sub

    ' Filters
    Friend Function CreateFilter(ByRef p_filterName As String, _
                                 ByRef p_parentFilterid As Int32, _
                                 ByRef p_isParent As Int32) As Boolean

        Dim ht As New Hashtable
        ht.Add(NAME_VARIABLE, p_filterName)
        ht.Add(PARENT_ID_VARIABLE, p_parentFilterid)
        ht.Add(AXIS_ID_VARIABLE, m_axisId)
        ht.Add(ITEMS_POSITIONS, 1)
        ht.Add(FILTER_IS_PARENT_VARIABLE, p_isParent)
        m_isEditingFiltersStructure = True
        GlobalVariables.Filters.CMSG_CREATE_FILTER(ht)

    End Function

    Friend Sub UpdateFilter(ByRef filterId As Int32, ByRef field As String, ByRef value As Object)

        Dim ht As Hashtable = GlobalVariables.Filters.filters_hash(filterId).clone
        ht(field) = value
        GlobalVariables.Filters.CMSG_UPDATE_FILTER(ht)

    End Sub

    Friend Sub UpdateFiltersBatch()


    End Sub

    Friend Sub DeleteFilter(ByRef filterId As Int32)

        m_isEditingFiltersStructure = True
        GlobalVariables.Filters.CMSG_DELETE_FILTER(filterId)

    End Sub

    Friend Function IsAllowedFilterName(ByRef p_name As String)
        For Each filter In GlobalVariables.Filters.filters_hash.Values
            If filter(NAME_VARIABLE) = p_name Then Return False
        Next
        For Each filterValue In GlobalVariables.FiltersValues.filtervalues_hash.Values
            If filterValue(NAME_VARIABLE) = p_name Then Return False
        Next
        Return True
    End Function

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

    Friend Sub UpdateFilterValuesBatch(ByRef p_filtersValuesUpdates As List(Of Tuple(Of Int32, String, Int32)))

        Dim filterValuesHTUpdates As New Hashtable
        For Each tuple_ As Tuple(Of Int32, String, Int32) In p_filtersValuesUpdates
            Dim ht As Hashtable = GlobalVariables.FiltersValues.filtervalues_hash(tuple_.Item1).clone
            ht(tuple_.Item2) = tuple_.Item3
            filterValuesHTUpdates(CInt(ht(ID_VARIABLE))) = ht
        Next
        GlobalVariables.FiltersValues.CMSG_UPDATE_FILTERS_VALUE_LIST(filterValuesHTUpdates)

    End Sub

    Friend Sub DeleteFilterValue(ByRef filterValueId As Int32)

        GlobalVariables.FiltersValues.CMSG_DELETE_FILTER_VALUE(filterValueId)

    End Sub

#End Region


#Region "Events"

    ' Filters
    Private Sub AfterFilterRead(ByRef status As Boolean, ByRef ht As Hashtable)

        If status = True Then
            m_view.UpdateFiltersValuesTV()
            m_editFilterStructUI.SetFilter(ht)
        End If
        m_isEditingFiltersStructure = False

    End Sub

    Private Sub AfterFilterDelete(ByRef status As Boolean, ByRef id As Int32)

        If status = True Then
            m_view.UpdateFiltersValuesTV()
            m_editFilterStructUI.DeleteFilter(id)
        End If
        m_isEditingFiltersStructure = False

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

    ' Filters values
    Private Sub AfterFilterValueRead(ByRef status As Boolean, ByRef ht As Hashtable)

        If status = True AndAlso m_isEditingFiltersStructure = False Then
            m_view.UpdateFiltersValuesTV()
        End If

    End Sub

    Private Sub AfterFilterValueDelete(ByRef status As Boolean, ByRef id As Int32)

        If status = True AndAlso m_isEditingFiltersStructure = False Then
            m_view.UpdateFiltersValuesTV()
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

    Friend Sub SendNewPositionsToModel()

        Dim position As Int32
        Dim accountsUpdates As New List(Of Tuple(Of Int32, String, Int32))

        Dim positionsDictionary As Dictionary(Of Int32, Int32) = GetFiltersValuesPositionsDictionary()
        Dim updateList As New List(Of Tuple(Of Int32, String, Int32))

        For Each filterValueId As Int32 In positionsDictionary.Keys
            position = positionsDictionary(filterValueId)
            If position <> GlobalVariables.FiltersValues.filtervalues_hash(filterValueId)(ITEMS_POSITIONS) Then
                Dim tuple_ As New Tuple(Of Int32, String, Int32)(filterValueId, ITEMS_POSITIONS, position)
                updateList.Add(tuple_)
            End If
        Next
        UpdateFilterValuesBatch(updateList)

    End Sub

    Private Function GetFiltersValuesPositionsDictionary() As Dictionary(Of Int32, Int32)

        Dim positionsDictionary As New Dictionary(Of Int32, Int32)
        For Each node As vTreeNode In m_view.m_filtersFiltersValuesTV.Nodes


        Next
        Return positionsDictionary

    End Function



#End Region



End Class
