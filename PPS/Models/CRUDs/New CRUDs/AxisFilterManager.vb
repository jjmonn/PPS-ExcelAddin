Imports System.Collections
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports VIBlend.WinForms.Controls
Imports CRUD


Class AxisFilterManager : Inherits CRUDManager

#Region "Instance variables"

    Protected m_axisFilterDictionary As New SortedDictionary(Of AxisType, SortedDictionary(Of Int32, AxisFilter))

#End Region

#Region "Init"

    Friend Sub New()

        Clear()

        CreateCMSG = ClientMessage.CMSG_CREATE_AXIS_FILTER
        ReadCMSG = ClientMessage.CMSG_READ_AXIS_FILTER
        UpdateCMSG = ClientMessage.CMSG_UPDATE_AXIS_FILTER
        UpdateListCMSG = ClientMessage.CMSG_CRUD_AXIS_FILTER_LIST
        DeleteCMSG = ClientMessage.CMSG_DELETE_AXIS_FILTER
        ListCMSG = ClientMessage.CMSG_LIST_AXIS_FILTER

        CreateSMSG = ServerMessage.SMSG_CREATE_AXIS_FILTER_ANSWER
        ReadSMSG = ServerMessage.SMSG_READ_AXIS_FILTER_ANSWER
        UpdateSMSG = ServerMessage.SMSG_UPDATE_AXIS_FILTER_ANSWER
        UpdateListSMSG = ServerMessage.SMSG_CRUD_AXIS_FILTER_LIST_ANSWER
        DeleteSMSG = ServerMessage.SMSG_DELETE_AXIS_FILTER_ANSWER
        ListSMSG = ServerMessage.SMSG_LIST_AXIS_FILTER_ANSWER

        Build = AddressOf AxisFilter.BuildAxisFilter

        InitCallbacks()

    End Sub

    Private Sub Clear()
        For Each l_axis In System.Enum.GetValues(GetType(AxisType))
            m_axisFilterDictionary(l_axis) = New SortedDictionary(Of Int32, AxisFilter)
        Next
    End Sub

#End Region

#Region "CRUD"

    Protected Overrides Sub ListAnswer(packet As ByteBuffer)

        If packet.GetError() = ErrorMessage.SUCCESS Then
            Clear()
            For i As Int32 = 1 To packet.ReadInt32()
                Dim tmp_ht = AxisFilter.BuildAxisFilter(packet)
                m_axisFilterDictionary(tmp_ht.Axis)(tmp_ht.Id) = tmp_ht
            Next
            state_flag = True
            RaiseObjectInitializedEvent()
        Else
            RaiseObjectInitializedEvent()
            state_flag = False
        End If

    End Sub

    Protected Overrides Sub ReadAnswer(packet As ByteBuffer)

        If packet.GetError() = ErrorMessage.SUCCESS Then
            Dim tmp_ht = AxisFilter.BuildAxisFilter(packet)
            m_axisFilterDictionary(tmp_ht.Axis)(tmp_ht.Id) = tmp_ht
            RaiseReadEvent(packet.GetError(), tmp_ht)
        Else
            RaiseReadEvent(packet.GetError(), Nothing)
        End If

    End Sub

    Protected Overrides Sub DeleteAnswer(packet As ByteBuffer)

        If packet.GetError() = ErrorMessage.SUCCESS Then
            Dim type As AxisType = CType(packet.ReadUint32(), AxisType)
            Dim id As UInt32 = packet.ReadInt32()
            m_axisFilterDictionary(type).Remove(id)
            RaiseDeleteEvent(packet.GetError(), id)
        Else
            RaiseDeleteEvent(packet.GetError(), 0)
        End If

    End Sub

#End Region

#Region "Mapping"

    Public Overloads Function GetValue(ByVal p_axis As AxisType, ByVal p_id As UInt32) As AxisFilter
        Return m_axisFilterDictionary(p_axis)(p_id)
    End Function

    Public Overloads Function GetValue(ByVal p_axis As AxisType, ByVal p_id As Int32) As AxisFilter
        Return m_axisFilterDictionary(p_axis)(p_id)
    End Function

    Public Overrides Function GetValue(ByVal p_id As UInt32) As CRUDEntity
        For Each axis In m_axisFilterDictionary.Values
            Dim l_value As CRUDEntity = axis(p_id)

            If Not l_value Is Nothing Then Return l_value
        Next
        Return Nothing
    End Function

    Public Overrides Function GetValue(ByVal p_id As Int32) As CRUDEntity
        Return GetValue(CUInt(p_id))
    End Function

    Friend Function GetDictionary(ByVal p_axis As AxisType) As SortedDictionary(Of Int32, AxisFilter)
        Return m_axisFilterDictionary(p_axis)
    End Function

    ' Mapping Methods
    Friend Function GetFilteredAxisIDs(ByRef p_axisType As AxisType, ByRef filter_id As UInt32, _
                                       ByRef filter_value_id As UInt32) As List(Of UInt32)

        Dim axis_list As New List(Of UInt32)
        For Each axisFilter As AxisFilter In m_axisFilterDictionary(p_axisType).Values
            If axisFilter.FilterValueId = filter_value_id Then axis_list.Add(axisFilter.AxisElemId)
        Next
        Return axis_list

    End Function

    Friend Function GetFilterValueId(ByRef p_axisType As AxisType, ByRef filterId As Int32, _
                                     ByRef axisValueId As Int32) As Int32

        Dim mostNestedFilterId = GlobalVariables.Filters.GetMostNestedFilterId(filterId)
        Dim mostNestedFilterValueId = m_axisFilterDictionary(p_axisType)(mostNestedFilterId)

        If mostNestedFilterValueId Is Nothing Then Return 0
        Return GlobalVariables.FiltersValues.GetFilterValueId(mostNestedFilterValueId.FilterValueId, filterId)

    End Function
#End Region

#Region "Utils"

    ' Base Method
    Friend Shared Sub LoadFvTv(ByRef FvTv As vTreeView, _
                               ByRef axis_id As UInt32)

        Dim filtersNode As New vTreeNode
        LoadFvTv(FvTv, filtersNode, axis_id)

    End Sub

    Friend Shared Sub LoadFvTv(ByRef FvNode As vTreeNode, _
                              ByRef axis_id As UInt32)

        Dim filtersNode As New vTreeNode
        LoadFvTv(FvNode, filtersNode, axis_id)

    End Sub

    Friend Shared Sub LoadFvTv(ByRef FvTv As vTreeView, _
                               ByRef axis_id As Int32)

        Dim filtersNode As New VIBlend.WinForms.Controls.vTreeNode
        LoadFvTv(FvTv, filtersNode, axis_id)

    End Sub

    ' Method with Filters Nodes given as param to be filled as well
    Friend Shared Sub LoadFvTv(ByRef FvTv As vTreeView, _
                               ByRef filtersNode As vTreeNode, _
                               ByRef axis_id As Int32)

        FvTv.Nodes.Clear()
        GlobalVariables.Filters.LoadFiltersNode(filtersNode, axis_id)
        For Each filterNode As vTreeNode In filtersNode.Nodes
            Dim NewFvTvNode As New vTreeNode
            NewFvTvNode.Value = filterNode.Value
            NewFvTvNode.Text = filterNode.Text
            FvTv.Nodes.Add(NewFvTvNode)
            LoadFiltersValues(filterNode, NewFvTvNode)
        Next

    End Sub

    Friend Shared Sub LoadFvTv(ByRef FvTv As vTreeView, _
                              ByRef filtersNode As vTreeNode, _
                              ByRef axis_id As UInt32)

        FvTv.Nodes.Clear()
        GlobalVariables.Filters.LoadFiltersNode(filtersNode, axis_id)
        For Each filterNode As vTreeNode In filtersNode.Nodes
            Dim NewFvTvNode As vTreeNode = VTreeViewUtil.AddNode(filterNode.Value, filterNode.Text, FvTv, 0)
            LoadFiltersValues(filterNode, NewFvTvNode)
        Next

    End Sub

    Friend Shared Sub LoadFvTv(ByRef FvNode As vTreeNode, _
                             ByRef filtersNode As vTreeNode, _
                             ByRef axis_id As UInt32)

        FvNode.Nodes.Clear()
        GlobalVariables.Filters.LoadFiltersNode(filtersNode, axis_id)
        For Each filterNode As vTreeNode In filtersNode.Nodes
            Dim NewFvTvNode As vTreeNode = VTreeViewUtil.AddNode(filterNode.Value, filterNode.Text, FvNode, 0)
            LoadFiltersValues(filterNode, NewFvTvNode)
        Next

    End Sub


    ' Load Filters Values Methods
    Friend Shared Sub LoadFiltersValues(ByRef filterNode As TreeNode, _
                                        ByRef FvTvNode As TreeNode, _
                                        Optional ByVal firstLevelFlag As Boolean = True)

        For Each filterValueSet As MultiIndexDictionary(Of UInt32, String, FilterValue) In GlobalVariables.FiltersValues.GetDictionary().Values
            For Each filterValue As FilterValue In filterValueSet.Values
                If firstLevelFlag = True Then

                    Dim newFvTVNode As TreeNode = FvTvNode.Nodes.Add(filterValue.Id, filterValue.Name)
                    If filterNode.Nodes.Count > 0 Then
                        LoadFiltersValues(filterNode.Nodes(0), newFvTVNode, False)
                    End If


                ElseIf filterValue.ParentId = FvTvNode.Name Then

                    Dim newFvTVNode As TreeNode = FvTvNode.Nodes.Add(filterValue.Id, filterValue.Name)
                    If filterNode.Nodes.Count > 0 Then
                        LoadFiltersValues(filterNode.Nodes(0), newFvTVNode, False)
                    End If

                End If
            Next
        Next

    End Sub

    Friend Shared Sub LoadFiltersValues(ByRef filterNode As vTreeNode, _
                                        ByRef FvTvNode As vTreeNode, _
                                        Optional ByVal firstLevelFlag As Boolean = True)

        Dim filtersValuesDict = GlobalVariables.FiltersValues.GetDictionary(CUInt(filterNode.Value))
        For Each filterValue As FilterValue In filtersValuesDict.Values
            If firstLevelFlag = True Then

                Dim valueNode As vTreeNode = VTreeViewUtil.AddNode(filterValue.Id, filterValue.Name, FvTvNode)
                If filterNode.Nodes.Count > 0 Then
                    LoadFiltersValues(filterNode.Nodes(0), valueNode, False)
                End If

            ElseIf filterValue.Id = FvTvNode.Value Then
                Dim valueNode As vTreeNode = VTreeViewUtil.AddNode(filterValue.Id, filterValue.Name, FvTvNode)
                If filterNode.Nodes.Count > 0 Then
                    LoadFiltersValues(filterNode.Nodes(0), valueNode, False)
                End If
            End If
        Next

    End Sub
#End Region

End Class
