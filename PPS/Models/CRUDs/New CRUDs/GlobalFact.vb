Imports System.Collections
Imports System.Collections.Generic



Friend Class GlobalFact

#Region "Instance variables"


    ' Variables
    Friend state_flag As Boolean
    Friend globalFact_hash As New Hashtable

    ' Events
    Public Event ObjectInitialized()
    Public Event Read(ByRef status As Boolean, ByRef attributes As Hashtable)
    Public Event CreationEvent(ByRef status As Boolean, ByRef id As Int32)
    Public Event UpdateEvent(ByRef status As Boolean, ByRef id As Int32)
    Public Event DeleteEvent(ByRef status As Boolean, ByRef id As UInt32)


#End Region


#Region "Init"

    Friend Sub New()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_GLOBAL_FACT_ANSWER, AddressOf SMSG_LIST_GLOBAL_FACT_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_GLOBAL_FACT_ANSWER, AddressOf SMSG_DELETE_GLOBAL_FACT_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_GLOBAL_FACT_ANSWER, AddressOf SMSG_READ_GLOBAL_FACT_ANSWER)

        state_flag = False

    End Sub

    Private Sub SMSG_LIST_GLOBAL_FACT_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim nb_globalFacts = packet.ReadInt32()
            For i As Int32 = 1 To nb_globalFacts
                Dim tmp_ht As New Hashtable
                GetGlobalFactHTFromPacket(packet, tmp_ht)
                globalFact_hash(CInt(tmp_ht(ID_VARIABLE))) = tmp_ht
            Next
            state_flag = True
            RaiseEvent ObjectInitialized()
        Else
            state_flag = False
        End If

    End Sub

#End Region

#Region "CRUD"

    Friend Sub CMSG_CREATE_GLOBAL_FACT(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_GLOBAL_FACT_ANSWER, AddressOf SMSG_CREATE_GLOBAL_FACT_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_GLOBAL_FACT, UShort))
        WriteGlobalFactPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_GLOBAL_FACT_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            RaiseEvent CreationEvent(True, CInt(packet.ReadUint32()))
        Else
            RaiseEvent CreationEvent(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_GLOBAL_FACT_ANSWER, AddressOf SMSG_CREATE_GLOBAL_FACT_ANSWER)

    End Sub

    Friend Sub CMSG_READ_GLOBAL_FACT(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_READ_GLOBAL_FACT, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_READ_GLOBAL_FACT_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim ht As New Hashtable
            GetGlobalFactHTFromPacket(packet, ht)
            globalFact_hash(CInt(ht(ID_VARIABLE))) = ht
            RaiseEvent Read(True, ht)
        Else
            RaiseEvent Read(False, Nothing)
        End If

    End Sub

    Friend Sub CMSG_UPDATE_GLOBAL_FACT(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_GLOBAL_FACT_ANSWER, AddressOf SMSG_UPDATE_GLOBAL_FACT_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_GLOBAL_FACT, UShort))
        WriteGlobalFactPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_GLOBAL_FACT_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            RaiseEvent UpdateEvent(True, CInt(packet.ReadUint32()))
        Else
            RaiseEvent UpdateEvent(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_GLOBAL_FACT_ANSWER, AddressOf SMSG_UPDATE_GLOBAL_FACT_ANSWER)

    End Sub

    Friend Sub CMSG_DELETE_GLOBAL_FACT(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_GLOBAL_FACT, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_GLOBAL_FACT_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim id As UInt32 = packet.ReadInt32
            globalFact_hash.Remove(id)
            RaiseEvent DeleteEvent(True, id)
        Else
            RaiseEvent DeleteEvent(False, 0)
        End If

    End Sub

#End Region


#Region "Utilities"

    Friend Shared Sub GetGlobalFactHTFromPacket(ByRef packet As ByteBuffer, ByRef globalFact_ht As Hashtable)

        globalFact_ht(ID_VARIABLE) = packet.ReadInt32()
        globalFact_ht(NAME_VARIABLE) = packet.ReadString()

    End Sub

    Private Sub WriteGlobalFactPacket(ByRef packet As ByteBuffer, ByRef attributes As Hashtable)

        If attributes.ContainsKey(ID_VARIABLE) Then packet.WriteInt32(attributes(ID_VARIABLE))
        packet.WriteString(attributes(NAME_VARIABLE))

    End Sub

#End Region


    Protected Overrides Sub finalize()

        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_GLOBAL_FACT_ANSWER, AddressOf SMSG_LIST_GLOBAL_FACT_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_GLOBAL_FACT_ANSWER, AddressOf SMSG_DELETE_GLOBAL_FACT_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_READ_GLOBAL_FACT_ANSWER, AddressOf SMSG_READ_GLOBAL_FACT_ANSWER)
        MyBase.Finalize()

    End Sub


End Class
