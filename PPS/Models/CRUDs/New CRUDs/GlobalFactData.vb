Imports System.Collections
Imports System.Collections.Generic
Imports System.Tuple

Friend Class GlobalFactData


#Region "Instance variables"

    ' Variables
    Friend state_flag As Boolean
    Friend globalFactDataHash As New Dictionary(Of Tuple(Of Int32, Int32, Int32), Double)

    ' Events
    Public Event ObjectInitialized(ByRef status As Boolean)
    Public Event Read(ByRef status As Boolean, ByRef attributes As Hashtable)
    Public Event CreationEvent(ByRef status As Boolean, ByRef factId As Int32, ByRef period As Int32, versionId As Int32, ByRef value As Double)
    Public Event UpdateEvent(ByRef status As Boolean, ByRef factId As Int32, ByRef period As Int32, versionId As Int32, ByRef value As Double)
    Public Event DeleteEvent(ByRef status As Boolean, ByRef factId As Int32, ByRef period As Int32, versionId As Int32)


#End Region


#Region "Init"

    Friend Sub New()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_GLOBAL_FACT_DATA_ANSWER, AddressOf SMSG_READ_GLOBAL_FACT_DATA_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_GLOBAL_FACT_DATA_ANSWER, AddressOf SMSG_DELETE_GLOBAL_FACT_DATA_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_GLOBAL_FACT_DATA_ANSWER, AddressOf SMSG_LIST_GLOBAL_FACT_DATA_ANSWER)
        state_flag = False

    End Sub

    Private Sub SMSG_LIST_GLOBAL_FACT_DATA_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            For i As Int32 = 1 To packet.ReadInt32()
                Dim tmp_ht As New Hashtable
                GetGlobalFactDataHTFromPacket(packet, tmp_ht)

                AddRecordToGlobalFactDataHash(tmp_ht(GLOBAL_FACT_ID_VARIABLE), _
                                                tmp_ht(PERIOD_VARIABLE), _
                                                tmp_ht(VERSION_ID_VARIABLE), tmp_ht(VALUE_VARIABLE))
            Next
            state_flag = True
            RaiseEvent ObjectInitialized(True)
        Else
            state_flag = False
        End If

    End Sub

#End Region


#Region "CRUD"

    Friend Sub CMSG_CREATE_GLOBAL_FACT_DATA(ByRef globalFactId As Int32, _
                                             ByRef period As Int32, _
                                             ByRef VersionId As Int32, _
                                             ByRef value As Double)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_GLOBAL_FACT_DATA_ANSWER, AddressOf SMSG_CREATE_GLOBAL_FACT_DATA_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_GLOBAL_FACT_DATA, UShort))

        packet.WriteUint32(globalFactId)
        packet.WriteUint32(period)
        packet.WriteUint32(VersionId)
        packet.WriteDouble(value)

        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_GLOBAL_FACT_DATA_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim factId As Int32 = packet.ReadInt32()
            Dim period As Int32 = packet.ReadInt32()
            Dim versionId As Int32 = packet.ReadInt32()
            Dim value As Double = packet.ReadDouble()
            RaiseEvent CreationEvent(True, factId, period, versionId, value)
        Else
            RaiseEvent CreationEvent(False, 0, 0, 0, 0.0)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_GLOBAL_FACT_DATA_ANSWER, AddressOf SMSG_CREATE_GLOBAL_FACT_DATA_ANSWER)

    End Sub

    Friend Sub CMSG_READ_GLOBAL_FACT_DATA(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_READ_GLOBAL_FACT_DATA, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_READ_GLOBAL_FACT_DATA_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim ht As New Hashtable
            GetGlobalFactDataHTFromPacket(packet, ht)
            AddRecordToGlobalFactDataHash(ht(GLOBAL_FACT_ID_VARIABLE), _
                                              ht(PERIOD_VARIABLE), _
                                              ht(VERSION_ID_VARIABLE), _
                                              ht(VALUE_VARIABLE))
            RaiseEvent Read(True, ht)
        Else
            RaiseEvent Read(False, Nothing)
        End If

    End Sub

    Friend Sub CMSG_UPDATE_GLOBAL_FACT_DATA(ByRef globalFactId As Int32, _
                                             ByRef period As Int32, _
                                             ByRef VersionId As Int32, _
                                             ByRef value As Double)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_GLOBAL_FACT_DATA_ANSWER, AddressOf SMSG_UPDATE_GLOBAL_FACT_DATA_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_GLOBAL_FACT_DATA, UShort))
        packet.WriteUint32(globalFactId)
        packet.WriteUint32(period)
        packet.WriteUint32(VersionId)
        packet.WriteDouble(value)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_GLOBAL_FACT_DATA_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim globalFactId As Int32 = packet.ReadInt32()
            Dim period As Int32 = packet.ReadInt32()
            Dim versionId As Int32 = packet.ReadInt32()
            Dim value As Double = packet.ReadDouble()

            RaiseEvent UpdateEvent(True, globalFactId, period, versionId, value)
        Else
            RaiseEvent UpdateEvent(False, 0, 0, 0, 0.0)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_GLOBAL_FACT_DATA_ANSWER, AddressOf SMSG_UPDATE_GLOBAL_FACT_DATA_ANSWER)

    End Sub

    Friend Sub CMSG_DELETE_GLOBAL_FACT_DATA(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_GLOBAL_FACT_DATA, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_GLOBAL_FACT_DATA_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim globalFactId As Int32 = packet.ReadInt32()
            Dim period As Int32 = packet.ReadInt32()
            Dim versionId As Int32 = packet.ReadInt32()

            If globalFactDataHash.ContainsKey(Tuple.Create(globalFactId, period, versionId)) Then
                globalFactDataHash.Remove(Tuple.Create(globalFactId, period, versionId))
            End If
            RaiseEvent DeleteEvent(True, globalFactId, period, versionId)
        Else
            RaiseEvent DeleteEvent(False, 0, 0, 0)
        End If

    End Sub

#End Region

#Region "Utilities"

    Private Sub AddRecordToGlobalFactDataHash(ByRef factId As Int32, _
                                              ByRef period As Int32, _
                                              ByRef versionId As Int32, _
                                              ByRef value As Double)

        Dim key As Tuple(Of Int32, Int32, Int32) = Tuple.Create(factId, period, versionId)
        If globalFactDataHash.ContainsKey(key) Then
            globalFactDataHash(key) = value
        Else
            globalFactDataHash.Add(key, value)
        End If

    End Sub

    Friend Shared Sub GetGlobalFactDataHTFromPacket(ByRef packet As ByteBuffer, _
                                                  ByRef globalFactData_ht As Hashtable)

        globalFactData_ht(GLOBAL_FACT_ID_VARIABLE) = packet.ReadUint32()
        globalFactData_ht(PERIOD_VARIABLE) = packet.ReadUint32()
        globalFactData_ht(VERSION_ID_VARIABLE) = packet.ReadUint32()
        globalFactData_ht(VALUE_VARIABLE) = packet.ReadDouble()

    End Sub

    Private Sub WriteGlobalFactDataPacket(ByRef packet As ByteBuffer, _
                                            ByRef attributes As Hashtable)

        packet.WriteUint32(attributes(GLOBAL_FACT_ID_VARIABLE))
        packet.WriteUint32(attributes(PERIOD_VARIABLE))
        packet.WriteUint32(attributes(VERSION_ID_VARIABLE))
        packet.WriteDouble(attributes(VALUE_VARIABLE))

    End Sub

#End Region

    Protected Overrides Sub finalize()

        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_GLOBAL_FACT_DATA_ANSWER, AddressOf SMSG_LIST_GLOBAL_FACT_DATA_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_READ_GLOBAL_FACT_DATA_ANSWER, AddressOf SMSG_READ_GLOBAL_FACT_DATA_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_GLOBAL_FACT_DATA_ANSWER, AddressOf SMSG_DELETE_GLOBAL_FACT_DATA_ANSWER)
        MyBase.Finalize()

    End Sub

End Class
