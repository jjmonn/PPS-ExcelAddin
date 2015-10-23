Imports System.Collections
Imports System.Collections.Generic
Imports VIBlend.WinForms.Controls
Imports System.Linq


Public MustInherit Class AxisElemManager

#Region "Instance variables"


    Public Shadows Event ObjectInitialized(ByRef status As Boolean)
    Public Event Read(ByRef status As Boolean, ByRef attributes As AxisElem)
    Public Event CreationEvent(ByRef status As Boolean, ByRef id As Int32)
    Public Event UpdateEvent(ByRef status As Boolean, ByRef id As Int32)
    Public Event UpdateListEvent(ByRef status As Boolean, ByRef resultList As Dictionary(Of Int32, Boolean))
    Public Event DeleteEvent(ByRef status As Boolean, ByRef id As UInt32)

    Protected m_axisDictionary As New SortedDictionary(Of Int32, AxisElem)
    Friend state_flag As Boolean

#End Region


#Region "Init"

    Friend Sub New()
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_ADJUSTMENT_ANSWER, AddressOf SMSG_LIST_ADJUSTMENT_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_ADJUSTMENT_ANSWER, AddressOf SMSG_READ_ADJUSTMENT_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_ADJUSTMENT_ANSWER, AddressOf SMSG_DELETE_ADJUSTMENT_ANSWER)
        state_flag = False

    End Sub

    Private Sub SMSG_LIST_ADJUSTMENT_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            For i As Int32 = 1 To packet.ReadInt32()
                Dim tmpAxis = AxisElem.BuildAxis(packet)
                m_axisDictionary(tmpAxis.Id) = tmpAxis
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

    Private Sub SMSG_READ_ADJUSTMENT_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim tmpAxis = AxisElem.BuildAxis(packet)
            m_axisDictionary(tmpAxis.Id) = tmpAxis
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

    Friend Sub CMSG_UPDATE_AXIS_ELEM_LIST(ByRef p_adjustments As List(Of AxisElem))
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CRUD_AXIS_ELEM_LIST_ANSWER, AddressOf SMSG_CRUD_AXIS_ELEM_LIST_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CRUD_AXIS_ELEM_LIST, UShort))

        packet.WriteInt32(p_adjustments.Count())
        For Each attributes As AxisElem In p_adjustments
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

        If packet.GetError() = 0 Then
            RaiseEvent UpdateEvent(True, packet.ReadUint32())
        Else
            RaiseEvent UpdateEvent(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_ADJUSTMENT_ANSWER, AddressOf SMSG_UPDATE_ADJUSTMENT_ANSWER)

    End Sub

    Friend Overrides Sub CMSG_DELETE_AXIS(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_ADJUSTMENT, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_ADJUSTMENT_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim id As UInt32 = packet.ReadInt32
            m_axisDictionary.Remove(id)
            RaiseEvent DeleteEvent(True, id)
        Else
            RaiseEvent DeleteEvent(False, 0)
        End If

    End Sub

#End Region

    ' Mappings

    Friend Function GetDefaultAxis() As Int32
        Return 1
        '' If Axis_hash.Keys.Count <= 0 Then Return 1
        '' Return Axis_hash.Keys.GetEnumerator().Current
    End Function

    Friend Function GetAxisNameList() As List(Of String)

        Dim tmp_list As New List(Of String)
        For Each id In m_axisDictionary.Keys
            tmp_list.Add(m_axisDictionary(id).Name)
        Next
        Return tmp_list

    End Function

    Public Function GetAxis(ByVal p_id As UInt32) As AxisElem
        Return m_axisDictionary(p_id)
    End Function

    Public Function GetAxis(ByVal p_id As Int32) As AxisElem
        Return m_axisDictionary(CUInt(p_id))
    End Function

    Friend Function GetAxisDictionary() As SortedDictionary(Of Int32, AxisElem)
        Return m_axisDictionary
    End Function


    ' Utilities Methods
    Friend Function GetAxisValueId(ByRef name As String) As Int32

        For Each id As Int32 In m_axisDictionary.Keys
            If name = m_axisDictionary(id).Name Then Return id
        Next
        Return 0

    End Function

    Friend Function IsNameValid(ByRef name As String) As Boolean

        If name.Length > NAMES_MAX_LENGTH Then Return False
        For Each char_ As Char In AXIS_NAME_FORBIDEN_CHARS
            If name.Contains(char_) Then Return False
        Next
        If name = "" Then Return False
        If GetAxisNameList().Contains(name) Then Return False
        Return True

    End Function


    ' Treeeviews Loading
    Friend Sub LoadAxisTV(ByRef TV As vTreeView)

        TV.Nodes.Clear()
        VTreeViewUtil.LoadTreeview(TV, m_axisDictionary)

    End Sub

    Friend Sub LoadAxisTree(ByRef TV As VIBlend.WinForms.Controls.vTreeView)

        TV.Nodes.Clear()
        For Each id As Int32 In m_axisDictionary.Keys
            Dim node As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.AddNode(id, m_axisDictionary(id).Name, TV, 0)
            node.Checked = Windows.Forms.CheckState.Checked
        Next

    End Sub

    Friend Sub LoadAxisTree(ByRef DestNode As VIBlend.WinForms.Controls.vTreeNode)

        DestNode.Nodes.Clear()
        For Each id As Int32 In m_axisDictionary.Keys
            Dim newNode As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.AddNode(id, m_axisDictionary(id).Name, DestNode, 0)
            newNode.Checked = Windows.Forms.CheckState.Checked
        Next

    End Sub


    Friend Sub LoadAxisTree(ByRef TV As VIBlend.WinForms.Controls.vTreeView, _
                               ByRef filter_list As List(Of UInt32))

        TV.Nodes.Clear()
        For Each id As Int32 In m_axisDictionary.Keys
            Dim node As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.AddNode(id, m_axisDictionary(id).Name, TV, 0)
            node.Checked = Windows.Forms.CheckState.Checked
        Next

    End Sub

End Class
