Imports System.Collections
Imports System.Collections.Generic
Imports VIBlend.WinForms.Controls
Imports System.Linq
Imports CRUD


Public Class AxisElemManager

#Region "Instance variables"

    Public Shadows Event ObjectInitialized(ByRef status As ErrorMessage)
    Public Event Read(ByRef status As ErrorMessage, ByRef attributes As AxisElem)
    Public Event CreationEvent(ByRef status As ErrorMessage, ByRef id As Int32)
    Public Event UpdateEvent(ByRef status As ErrorMessage, ByRef p_axisType As AxisType, ByRef id As Int32)
    Public Event UpdateListEvent(ByRef status As ErrorMessage, ByRef resultList As Dictionary(Of Int32, Boolean))
    Public Event DeleteEvent(ByRef status As ErrorMessage, ByRef p_axisType As AxisType, ByRef id As UInt32)

    Protected m_axisDictionary As New SortedDictionary(Of AxisType, SortedDictionary(Of Int32, AxisElem))
    Friend state_flag As Boolean

#End Region


#Region "Init"

    Friend Sub New()
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_AXIS_ELEM_ANSWER, AddressOf SMSG_LIST_AXIS_ELEM_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_AXIS_ELEM_ANSWER, AddressOf SMSG_READ_AXIS_ELEM_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_AXIS_ELEM_ANSWER, AddressOf SMSG_DELETE_AXIS_ELEM_ANSWER)

        m_axisDictionary(AxisType.Adjustment) = New SortedDictionary(Of Int32, AxisElem)
        m_axisDictionary(AxisType.Client) = New SortedDictionary(Of Int32, AxisElem)
        m_axisDictionary(AxisType.Product) = New SortedDictionary(Of Int32, AxisElem)
        m_axisDictionary(AxisType.Entities) = New SortedDictionary(Of Int32, AxisElem)

        state_flag = False

    End Sub

    Protected Overrides Sub finalize()

        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_AXIS_ELEM_ANSWER, AddressOf SMSG_LIST_AXIS_ELEM_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_READ_AXIS_ELEM_ANSWER, AddressOf SMSG_READ_AXIS_ELEM_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_AXIS_ELEM_ANSWER, AddressOf SMSG_DELETE_AXIS_ELEM_ANSWER)
        MyBase.Finalize()

    End Sub

    Private Sub SMSG_LIST_AXIS_ELEM_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            For i As Int32 = 1 To packet.ReadInt32()
                Dim tmpAxis = AxisElem.BuildAxis(packet)
                m_axisDictionary(tmpAxis.Axis)(tmpAxis.Id) = tmpAxis
            Next
            RaiseEvent ObjectInitialized(True)
            state_flag = True
        Else
            RaiseEvent ObjectInitialized(False)
            state_flag = False
        End If

    End Sub

#End Region

    ' CRUD Methods
#Region "CRUD"

    Friend Sub CMSG_CREATE_AXIS_ELEM(ByRef attributes As AxisElem)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_AXIS_ELEM_ANSWER, AddressOf SMSG_CREATE_AXIS_ELEM_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_AXIS_ELEM, UShort))
        attributes.Dump(packet, False)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_AXIS_ELEM_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            RaiseEvent CreationEvent(True, packet.ReadUint32())
        Else
            RaiseEvent CreationEvent(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_AXIS_ELEM_ANSWER, AddressOf SMSG_CREATE_AXIS_ELEM_ANSWER)

    End Sub

    Friend Shared Sub CMSG_READ_AXIS_ELEM(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_READ_AXIS_ELEM, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_READ_AXIS_ELEM_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim tmpAxis = AxisElem.BuildAxis(packet)
            m_axisDictionary(tmpAxis.Axis)(tmpAxis.Id) = tmpAxis
            RaiseEvent Read(True, tmpAxis)
        Else
            RaiseEvent Read(False, Nothing)
        End If

    End Sub

    Friend Sub CMSG_UPDATE_AXIS_ELEM(ByRef attributes As AxisElem)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_AXIS_ELEM_ANSWER, AddressOf SMSG_UPDATE_AXIS_ELEM_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_AXIS_ELEM, UShort))
        attributes.Dump(packet, True)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Sub CMSG_UPDATE_AXIS_ELEM_LIST(ByRef p_axisElemList As List(Of AxisElem))
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CRUD_AXIS_ELEM_LIST_ANSWER, AddressOf SMSG_CRUD_AXIS_ELEM_LIST_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CRUD_AXIS_ELEM_LIST, UShort))

        packet.WriteInt32(p_axisElemList.Count())
        For Each attributes As AxisElem In p_axisElemList
            packet.WriteUint8(CRUDAction.UPDATE)
            attributes.Dump(packet, True)
        Next
        packet.Release()
        NetworkManager.GetInstance().Send(packet)
    End Sub

    Private Sub SMSG_CRUD_AXIS_ELEM_LIST_ANSWER(packet As ByteBuffer)

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
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CRUD_AXIS_ELEM_LIST_ANSWER, AddressOf SMSG_CRUD_AXIS_ELEM_LIST_ANSWER)

    End Sub

    Private Sub SMSG_UPDATE_AXIS_ELEM_ANSWER(packet As ByteBuffer)

        RaiseEvent UpdateEvent(packet.GetError(), CType(packet.ReadUint32(), AxisType), packet.ReadUint32())
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_AXIS_ELEM_ANSWER, AddressOf SMSG_UPDATE_AXIS_ELEM_ANSWER)

    End Sub

    Friend Sub CMSG_DELETE_AXIS_ELEM(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_AXIS_ELEM, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_AXIS_ELEM_ANSWER(packet As ByteBuffer)

        If packet.GetError() = ErrorMessage.SUCCESS Then
            Dim l_type As AxisType = CType(packet.ReadUint32(), AxisType)
            Dim id As UInt32 = packet.ReadInt32()
            m_axisDictionary(l_type).Remove(id)
            RaiseEvent DeleteEvent(packet.GetError(), l_type, id)
        Else
            RaiseEvent DeleteEvent(packet.GetError(), 0, 0)
        End If

    End Sub

#End Region

    ' Mappings

    Friend Function GetDefaultAxis() As Int32
        Return 1
        '' If Axis_hash.Keys.Count <= 0 Then Return 1
        '' Return Axis_hash.Keys.GetEnumerator().Current
    End Function

    Friend Function GetAxisNameList(ByVal p_axis As AxisType) As List(Of String)

        Dim tmp_list As New List(Of String)
        For Each id In m_axisDictionary(p_axis).Keys
            tmp_list.Add(m_axisDictionary(p_axis)(id).Name)
        Next
        Return tmp_list

    End Function

    Public Function GetAxisElem(ByVal p_axis As AxisType, ByVal p_id As UInt32) As AxisElem
        Return m_axisDictionary(p_axis)(p_id)
    End Function

    Public Function GetAxisElem(ByVal p_axis As AxisType, ByVal p_id As Int32) As AxisElem
        Return m_axisDictionary(p_axis)(p_id)
    End Function

    Friend Function GetAxisElemDictionary(ByVal p_axis As AxisType) As SortedDictionary(Of Int32, AxisElem)
        Return m_axisDictionary(p_axis)
    End Function

    ' Utilities Methods
    Friend Function GetAxisValueId(ByVal p_axis As AxisType, ByRef name As String) As Int32

        For Each id As Int32 In m_axisDictionary(p_axis).Keys
            If name = m_axisDictionary(p_axis)(id).Name Then Return id
        Next
        Return 0

    End Function

    Friend Function IsNameValid(ByRef name As String) As Boolean

        If name.Length > NAMES_MAX_LENGTH Then Return False
        For Each char_ As Char In AXIS_NAME_FORBIDEN_CHARS
            If name.Contains(char_) Then Return False
        Next
        If name = "" Then Return False
        For Each axis As AxisType In [Enum].GetValues(GetType(AxisType))
            If GetAxisNameList(axis).Contains(name) Then Return False
        Next
        Return True

    End Function


    ' Treeeviews Loading
    Friend Sub LoadAxisTV(ByVal p_axis As AxisType, ByRef TV As vTreeView)

        TV.Nodes.Clear()
        VTreeViewUtil.LoadTreeview(TV, m_axisDictionary(p_axis))

    End Sub

    Friend Sub LoadAxisTree(ByVal p_axis As AxisType, ByRef TV As VIBlend.WinForms.Controls.vTreeView)

        TV.Nodes.Clear()
        For Each id As Int32 In m_axisDictionary.Keys
            Dim node As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.AddNode(id, m_axisDictionary(p_axis)(id).Name, TV, 0)
            node.Checked = Windows.Forms.CheckState.Checked
        Next

    End Sub

    Friend Sub LoadAxisTree(ByVal p_axis As AxisType, ByRef DestNode As VIBlend.WinForms.Controls.vTreeNode)

        DestNode.Nodes.Clear()
        For Each id As Int32 In m_axisDictionary.Keys
            Dim newNode As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.AddNode(id, m_axisDictionary(p_axis)(id).Name, DestNode, 0)
            newNode.Checked = Windows.Forms.CheckState.Checked
        Next

    End Sub


    Friend Sub LoadAxisTree(ByVal p_axis As AxisType, ByRef TV As VIBlend.WinForms.Controls.vTreeView, _
                               ByRef filter_list As List(Of UInt32))

        TV.Nodes.Clear()
        For Each id As Int32 In m_axisDictionary.Keys
            Dim node As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.AddNode(id, m_axisDictionary(p_axis)(id).Name, TV, 0)
            node.Checked = Windows.Forms.CheckState.Checked
        Next

    End Sub

End Class
