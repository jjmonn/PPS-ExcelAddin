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
Imports CRUD

Friend Class AxisFiltersController


#Region "Instance Variables"

    ' Objects
    Private m_view As AxisFiltersView
    Private m_editFilterStructUI As AxisFilterStructView

    Private m_filtersNode As New vTreeNode
    Private m_filterTV As New vTreeView
    Private m_filtersFiltersValuesTV As New vTreeView

    ' Variables
    Private m_axisId As Int32
    Private m_isEditingFiltersStructure As Boolean
    Private m_isClosing As Boolean = False

#End Region


#Region "Initialization"

    Friend Sub New(ByRef p_axisType As AxisType)

        m_axisId = p_axisType
        AxisFilterManager.LoadFvTv(m_filtersFiltersValuesTV, m_filtersNode, m_axisId)
        m_view = New AxisFiltersView(Me, m_filtersFiltersValuesTV, m_filtersNode, m_axisId)

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

    Public Sub close()

        m_isClosing = True
        SendNewPositionsToModel()
        m_view.Dispose()
        m_editFilterStructUI.Dispose()
        RemoveHandler GlobalVariables.Filters.CreationEvent, AddressOf AfterFilterCreation
        RemoveHandler GlobalVariables.Filters.Read, AddressOf AfterFilterRead
        RemoveHandler GlobalVariables.Filters.UpdateEvent, AddressOf AfterFilterUpdate
        RemoveHandler GlobalVariables.Filters.DeleteEvent, AddressOf AfterFilterDelete

        RemoveHandler GlobalVariables.FiltersValues.CreationEvent, AddressOf AfterFilterValueCreation
        RemoveHandler GlobalVariables.FiltersValues.Read, AddressOf AfterFilterValueRead
        RemoveHandler GlobalVariables.FiltersValues.UpdateEvent, AddressOf AfterFilterValueUpdate
        RemoveHandler GlobalVariables.FiltersValues.DeleteEvent, AddressOf AfterFilterValueDelete

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
                                 ByRef p_parentFilterid As UInt32, _
                                 ByRef p_isParent As Boolean) As Boolean

        Dim l_filter As New Filter

        l_filter.Name = p_filterName
        l_filter.ParentId = p_parentFilterid
        l_filter.IsParent = p_isParent
        l_filter.Axis = m_axisId
        l_filter.ItemPosition = 1

        m_isEditingFiltersStructure = True
        GlobalVariables.Filters.Create(l_filter)

        Return True

    End Function

    Friend Sub DeleteFilter(ByRef filterId As Int32)

        m_isEditingFiltersStructure = True
        GlobalVariables.Filters.Delete(filterId)

    End Sub

    Friend Function IsAllowedFilterName(ByRef p_name As String)
        If GlobalVariables.Filters.IsNameValid(p_name) = False Then Return False
        If GlobalVariables.FiltersValues.IsNameAvailable(p_name) = False Then Return False
        Return True
    End Function

    ' Filters Values
    Friend Sub CreateFilterValue(ByRef filterValueName As String, _
                                ByRef filterId As Int32, _
                                ByRef parentFilterValueId As Int32)

        Dim ht As New FilterValue

        ht.Name = filterValueName
        ht.FilterId = filterId
        ht.ParentId = parentFilterValueId
        ht.ItemPosition = 1
        GlobalVariables.FiltersValues.Create(ht)

    End Sub

    Friend Sub UpdateFilterValuesBatch(ByRef p_filtersValuesUpdates As List(Of CRUDEntity))

        GlobalVariables.FiltersValues.UpdateList(p_filtersValuesUpdates)

    End Sub

    Friend Sub DeleteFilterValue(ByRef filterValueId As Int32)

        GlobalVariables.FiltersValues.Delete(filterValueId)

    End Sub

    Friend Sub UpdateFilter(ByRef p_filter As Filter)
        GlobalVariables.Filters.Update(p_filter)
    End Sub

    Friend Sub UpdateFilterName(ByVal p_id As UInt32, ByVal p_value As String)
        Dim l_filter As Filter = GetFilterCopy(p_id)

        If l_filter Is Nothing Then Exit Sub
        l_filter.Name = p_value
        UpdateFilter(l_filter)
    End Sub

    Friend Sub UpdateFilterValueName(ByVal p_id As UInt32, ByVal p_value As String)
        Dim l_filterValue As FilterValue = GetFilterValueCopy(p_id)

        If l_filterValue Is Nothing Then Exit Sub
        l_filterValue.Name = p_value
        GlobalVariables.FiltersValues.Update(l_filterValue)
    End Sub

#End Region


#Region "Events"

    ' Filters
    Private Sub AfterFilterRead(ByRef status As ErrorMessage, ByRef ht As CRUDEntity)

        If status = ErrorMessage.SUCCESS Then
            m_view.UpdateFiltersValuesTV()
            m_editFilterStructUI.SetFilter(ht)
        End If
        m_isEditingFiltersStructure = False

    End Sub

    Private Sub AfterFilterDelete(ByRef status As ErrorMessage, ByRef id As UInt32)

        If status = ErrorMessage.SUCCESS Then
            m_view.UpdateFiltersValuesTV()
            m_editFilterStructUI.DeleteFilter(id)
        End If
        m_isEditingFiltersStructure = False

    End Sub

    Private Sub AfterFilterCreation(ByRef status As ErrorMessage, ByRef id As UInt32)

        If status <> ErrorMessage.SUCCESS Then
            MsgBox("The Filter could not be created.")
        End If

    End Sub

    Private Sub AfterFilterUpdate(ByRef status As ErrorMessage, ByRef id As UInt32)

        If status <> ErrorMessage.SUCCESS Then
            MsgBox("The Filter could not be updated.")
        End If

    End Sub

    ' Filters values
    Private Sub AfterFilterValueRead(ByRef status As ErrorMessage, ByRef ht As CRUDEntity)

        If status = ErrorMessage.SUCCESS _
        AndAlso m_isEditingFiltersStructure = False _
        AndAlso m_isClosing = False Then
            m_view.UpdateFiltersValuesTV()
        End If

    End Sub

    Private Sub AfterFilterValueDelete(ByRef status As ErrorMessage, ByRef id As UInt32)

        If status = ErrorMessage.SUCCESS AndAlso m_isEditingFiltersStructure = False Then
            m_view.UpdateFiltersValuesTV()
        End If

    End Sub

    Private Sub AfterFilterValueCreation(ByRef status As ErrorMessage, ByRef id As UInt32)

        If status <> ErrorMessage.SUCCESS Then
            MsgBox("The Filter Value could not be created.")
        End If

    End Sub

    Private Sub AfterFilterValueUpdate(ByRef status As ErrorMessage, ByRef id As UInt32)

        If status <> ErrorMessage.SUCCESS Then
            MsgBox("The Filter Value could not be updated.")
        End If

    End Sub


#End Region


#Region "Utilities"

    Friend Sub SendNewPositionsToModel()

        Dim position As Int32
        Dim accountsUpdates As New List(Of Tuple(Of Int32, String, Int32))

        Dim positionsDictionary As Dictionary(Of Int32, Int32) = VTreeViewUtil.GeneratePositionsDictionary(m_filtersFiltersValuesTV)
        Dim updateList As New List(Of CRUDEntity)

        For Each filterValueId As Int32 In positionsDictionary.Keys
            position = positionsDictionary(filterValueId)

            If GetFilterValue(filterValueId) Is Nothing Then Continue For
            If position <> GetFilterValue(filterValueId).ItemPosition Then
                Dim filterValue As FilterValue = GetFilterValueCopy(filterValueId)

                If filterValue Is Nothing Then Continue For
                filterValue.ItemPosition = position
                updateList.Add(filterValue)
            End If
        Next

        If updateList.Count > 0 Then UpdateFilterValuesBatch(updateList)

    End Sub

    Private Function GetFiltersValuesPositionsDictionary() As Dictionary(Of Int32, Int32)

        Dim positionsDictionary As New SafeDictionary(Of Int32, Int32)
        For Each node As vTreeNode In m_view.m_filtersFiltersValuesTV.Nodes


        Next
        Return positionsDictionary

    End Function

    Public Function GetFilter(ByRef p_id As UInt32) As Filter
        For Each axis In GlobalVariables.Filters.GetDictionary().Keys
            Dim filter = GlobalVariables.Filters.GetValue(axis, p_id)

            If Not filter Is Nothing Then Return filter
        Next
        Return Nothing
    End Function

    Public Function GetFilterCopy(ByRef p_id As UInt32) As Filter
        Dim filter = GetFilter(p_id)

        If filter Is Nothing Then Return Nothing
        Return filter.Clone()
    End Function

    Public Function GetFilterValue(ByRef p_id As UInt32) As FilterValue
        Return GlobalVariables.FiltersValues.GetValue(p_id)
    End Function

    Public Function GetFilterValueCopy(ByRef p_id As UInt32) As FilterValue
        Dim filterValue = GetFilterValue(p_id)

        If filterValue Is Nothing Then Return Nothing
        Return filterValue.Clone()
    End Function

#End Region



End Class
