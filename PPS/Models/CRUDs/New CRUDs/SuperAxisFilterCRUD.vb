Imports System.Collections
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports VIBlend.WinForms.Controls
Imports CRUD


Class AxisFilterManager

#Region "Instance variables"


    ' Events
    Public Event ObjectInitialized(ByRef status As Boolean)
    Public Event CreationEvent(ByRef status As ErrorMessage, ByRef p_axisType As AxisType, ByRef AXIS_id As Int32, ByRef filter_id As Int32, filter_value_id As Int32)
    Public Event DeleteEvent(ByRef status As ErrorMessage, ByRef AXIS_id As Int32, filter_id As Int32)
    Public Event Read(ByRef status As ErrorMessage, ByRef attributes As AxisFilter)
    Public Event UpdateEvent(ByRef status As ErrorMessage, ByRef p_axisType As AxisType, ByRef p_id As UInt32)
    Public Event UpdateListEvent(ByRef status As ErrorMessage, ByRef resultList As Dictionary(Of Int32, Boolean))

    Friend state_flag As Boolean
    Protected m_axisFilterDictionary As New SortedDictionary(Of AxisType, SortedDictionary(Of Int32, AxisFilter))

#End Region

#Region "Init"

    Friend Sub New()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_AXIS_FILTER_ANSWER, AddressOf SMSG_READ_AXIS_FILTER_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_AXIS_FILTER_ANSWER, AddressOf SMSG_DELETE_AXIS_FILTER_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_AXIS_FILTER_ANSWER, AddressOf SMSG_LIST_AXIS_FILTER_ANSWER)
        state_flag = False

    End Sub

    Protected Overrides Sub finalize()

        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_READ_AXIS_FILTER_ANSWER, AddressOf SMSG_READ_AXIS_FILTER_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_AXIS_FILTER_ANSWER, AddressOf SMSG_DELETE_AXIS_FILTER_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_AXIS_FILTER_ANSWER, AddressOf SMSG_LIST_AXIS_FILTER_ANSWER)

    End Sub

    Private Sub SMSG_LIST_AXIS_FILTER_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            For i As Int32 = 1 To packet.ReadInt32()
                Dim tmp_ht = AxisFilter.BuildAxisFilter(packet)
                m_axisFilterDictionary(tmp_ht.Axis)(tmp_ht.Id) = tmp_ht
            Next
            state_flag = True
            RaiseEvent ObjectInitialized(True)
        Else
            state_flag = False
        End If

    End Sub

#End Region

#Region "CRUD"

    Friend Sub CMSG_CREATE_AXIS_FILTER(ByRef attributes As AxisFilter)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_AXIS_FILTER_ANSWER, AddressOf SMSG_CREATE_AXIS_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_AXIS_FILTER, UShort))
        attributes.Dump(packet, False)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_AXIS_FILTER_ANSWER(packet As ByteBuffer)

        Dim axisType As AxisType = CType(packet.ReadUint32(), AxisType)
        Dim AXIS_id As Int32 = packet.ReadUint32()
        Dim filter_id As Int32 = packet.ReadUint32()
        Dim filter_value_id As Int32 = packet.ReadUint32()

        RaiseEvent CreationEvent(packet.GetError(), axisType, AXIS_id, filter_id, filter_value_id)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_AXIS_FILTER_ANSWER, AddressOf SMSG_CREATE_AXIS_FILTER_ANSWER)

    End Sub

    Friend Sub CMSG_READ_AXIS_FILTER(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_READ_AXIS_FILTER, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_READ_AXIS_FILTER_ANSWER(packet As ByteBuffer)

        If packet.GetError() = ErrorMessage.SUCCESS Then
            Dim tmp_ht = AxisFilter.BuildAxisFilter(packet)
            m_axisFilterDictionary(tmp_ht.Axis)(tmp_ht.Id) = tmp_ht
            RaiseEvent Read(packet.GetError(), tmp_ht)
        Else
            RaiseEvent Read(packet.GetError(), Nothing)
        End If

    End Sub

    Friend Sub CMSG_UPDATE_AXIS_FILTER(ByRef p_axisFilter As AxisFilter)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_AXIS_FILTER_ANSWER, AddressOf SMSG_UPDATE_AXIS_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_AXIS_FILTER, UShort))
        p_axisFilter.Dump(packet, True)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_AXIS_FILTER_ANSWER(packet As ByteBuffer)

        RaiseEvent UpdateEvent(packet.GetError(), CType(packet.ReadUint32(), AxisType), packet.ReadUint32())
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_AXIS_ELEM_ANSWER, AddressOf SMSG_UPDATE_AXIS_FILTER_ANSWER)

    End Sub

    Friend Sub CMSG_UPDATE_AXIS_FILTER_LIST(ByRef p_axisFilterList As List(Of AxisFilter))
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CRUD_AXIS_FILTER_LIST_ANSWER, AddressOf SMSG_CRUD_AXIS_FILTER_LIST_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CRUD_AXIS_ELEM_LIST, UShort))

        packet.WriteInt32(p_axisFilterList.Count())
        For Each attributes As AxisFilter In p_axisFilterList
            packet.WriteUint8(CRUDAction.UPDATE)
            attributes.Dump(packet, True)
        Next
        packet.Release()
        NetworkManager.GetInstance().Send(packet)
    End Sub

    Private Sub SMSG_CRUD_AXIS_FILTER_LIST_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim resultList As New Dictionary(Of Int32, Boolean)
            Dim nbResult As Int32 = packet.ReadInt32()

            For i As Int32 = 1 To nbResult
                Dim id As Int32 = packet.ReadInt32()
                If (resultList.ContainsKey(id)) Then
                    resultList(id) = packet.ReadBool()
                Else
                    resultList.Add(id, packet.ReadBool)
                End If
                packet.ReadString()
            Next

            RaiseEvent UpdateListEvent(True, resultList)
        Else
            RaiseEvent UpdateListEvent(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CRUD_AXIS_FILTER_LIST_ANSWER, AddressOf SMSG_CRUD_AXIS_FILTER_LIST_ANSWER)


    End Sub

    Friend Sub CMSG_DELETE_AXIS_FILTER(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_AXIS_FILTER, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_AXIS_FILTER_ANSWER(packet As ByteBuffer)

        If packet.GetError() = ErrorMessage.SUCCESS Then
            Dim type As AxisType = CType(packet.ReadUint32(), AxisType)
            Dim id As UInt32 = packet.ReadInt32()
            m_axisFilterDictionary(type).Remove(id)
            RaiseEvent DeleteEvent(packet.GetError(), type, id)
        Else
            RaiseEvent DeleteEvent(packet.GetError(), 0, 0)
        End If

    End Sub

#End Region

#Region "Mapping"
    Public Function GetAxisFilter(ByVal p_axis As AxisType, ByVal p_id As UInt32) As AxisFilter
        Return m_axisFilterDictionary(p_axis)(p_id)
    End Function

    Public Function GetAxisFilter(ByVal p_axis As AxisType, ByVal p_id As Int32) As AxisFilter
        Return m_axisFilterDictionary(p_axis)(p_id)
    End Function

    Friend Function GetAxisFilterDictionary(ByVal p_axis As AxisType) As SortedDictionary(Of Int32, AxisFilter)
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

        Dim filtersValuesIdDict = GlobalVariables.FiltersValues.GetFiltervaluesDictionary(CInt(filterNode.Name), _
                                                                                          ID_VARIABLE, NAME_VARIABLE)
        For Each filterValueId As Int32 In filtersValuesIdDict.Keys
            If firstLevelFlag = True Then

                Dim newFvTVNode As TreeNode = FvTvNode.Nodes.Add(filterValueId, filtersValuesIdDict(filterValueId))
                If filterNode.Nodes.Count > 0 Then
                    LoadFiltersValues(filterNode.Nodes(0), newFvTVNode, False)
                End If


            ElseIf GlobalVariables.FiltersValues.filtervalues_hash(filterValueId)(PARENT_FILTER_VALUE_ID_VARIABLE) = FvTvNode.Name Then

                Dim newFvTVNode As TreeNode = FvTvNode.Nodes.Add(filterValueId, filtersValuesIdDict(filterValueId))
                If filterNode.Nodes.Count > 0 Then
                    LoadFiltersValues(filterNode.Nodes(0), newFvTVNode, False)
                End If

            End If
        Next

    End Sub

    Friend Shared Sub LoadFiltersValues(ByRef filterNode As vTreeNode, _
                                        ByRef FvTvNode As vTreeNode, _
                                        Optional ByVal firstLevelFlag As Boolean = True)

        Dim filtersValuesIdDict = GlobalVariables.FiltersValues.GetFiltervaluesDictionary(CInt(filterNode.Value), _
                                                                                          ID_VARIABLE, NAME_VARIABLE)
        For Each filterValueId As Int32 In filtersValuesIdDict.Keys
            If firstLevelFlag = True Then

                Dim valueNode As vTreeNode = VTreeViewUtil.AddNode(filterValueId, filtersValuesIdDict(filterValueId), FvTvNode)
                If filterNode.Nodes.Count > 0 Then
                    LoadFiltersValues(filterNode.Nodes(0), valueNode, False)
                End If

            ElseIf GlobalVariables.FiltersValues.filtervalues_hash(filterValueId)(PARENT_FILTER_VALUE_ID_VARIABLE) = FvTvNode.Value Then
                Dim valueNode As vTreeNode = VTreeViewUtil.AddNode(filterValueId, filtersValuesIdDict(filterValueId), FvTvNode)
                If filterNode.Nodes.Count > 0 Then
                    LoadFiltersValues(filterNode.Nodes(0), valueNode, False)
                End If
            End If
        Next

    End Sub
#End Region

End Class
