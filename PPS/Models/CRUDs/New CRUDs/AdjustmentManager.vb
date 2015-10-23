Imports System.Collections
Imports System.Collections.Generic


' Adjustment2.vb
'
' CRUD for adjustments table - relation with c++ server
'
'
'
'
' Author: Julien Monnereau
' Created: 24/07/2015


Friend Class AdjustmentManager : Inherits AxisElemManager


#Region "Instance variables"

    ' Variables
    Friend state_flag As Boolean

    ' Events
    Public Shadows Event ObjectInitialized(ByRef status As Boolean)


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
                Dim tmpAxis = Axis.BuildAxis(packet)
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


#Region "CRUD"

    Friend Overrides Sub CMSG_CREATE_AXIS(ByRef attributes As AxisElem)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_ADJUSTMENT_ANSWER, AddressOf SMSG_CREATE_ADJUSTMENT_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_ADJUSTMENT, UShort))
        attributes.Dump(packet, False)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_ADJUSTMENT_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            MyBase.OnCreate(True, packet.ReadUint32())
        Else
            MyBase.OnCreate(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_ADJUSTMENT_ANSWER, AddressOf SMSG_CREATE_ADJUSTMENT_ANSWER)

    End Sub

    Friend Shared Sub CMSG_READ_ADJUSTMENT(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_READ_ADJUSTMENT, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_READ_ADJUSTMENT_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim tmpAxis = Axis.BuildAxis(packet)
            m_axisDictionary(tmpAxis.Id) = tmpAxis
            MyBase.OnRead(True, tmpAxis)
        Else
            MyBase.OnRead(False, Nothing)
        End If

    End Sub

    Friend Overrides Sub CMSG_UPDATE_AXIS(ByRef attributes As AxisElem)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_ADJUSTMENT_ANSWER, AddressOf SMSG_UPDATE_ADJUSTMENT_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_ADJUSTMENT, UShort))
        attributes.Dump(packet, True)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Overrides Sub CMSG_UPDATE_AXIS_LIST(ByRef p_adjustments As List(Of AxisElem))
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_ADJUSTMENT_LIST_ANSWER, AddressOf SMSG_UPDATE_ADJUSTMENT_LIST_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_ADJUSTMENT_LIST, UShort))

        packet.WriteInt32(p_adjustments.Count())
        For Each attributes As AxisElem In p_adjustments
            packet.WriteUint8(CRUDAction.UPDATE)
            attributes.Dump(packet, True)
        Next
        packet.Release()
        NetworkManager.GetInstance().Send(packet)
    End Sub

    Private Sub SMSG_UPDATE_ADJUSTMENT_LIST_ANSWER(packet As ByteBuffer)

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

            MyBase.OnUpdateList(True, resultList)
        Else
            MyBase.OnUpdateList(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_ADJUSTMENT_LIST_ANSWER, AddressOf SMSG_UPDATE_ADJUSTMENT_LIST_ANSWER)

    End Sub

    Private Sub SMSG_UPDATE_ADJUSTMENT_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            MyBase.OnUpdate(True, packet.ReadUint32())
        Else
            MyBase.OnUpdate(False, Nothing)
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
            MyBase.OnDelete(True, id)
        Else
            MyBase.OnDelete(False, 0)
        End If

    End Sub

#End Region

    Protected Overrides Sub finalize()

        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_ADJUSTMENT_ANSWER, AddressOf SMSG_LIST_ADJUSTMENT_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_READ_ADJUSTMENT_ANSWER, AddressOf SMSG_READ_ADJUSTMENT_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_ADJUSTMENT_ANSWER, AddressOf SMSG_DELETE_ADJUSTMENT_ANSWER)
        MyBase.Finalize()

    End Sub

End Class
