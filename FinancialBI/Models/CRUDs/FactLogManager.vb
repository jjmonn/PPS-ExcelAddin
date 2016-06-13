Imports System.Collections
Imports System.Collections.Generic
Imports CRUD

Class FactLogManager

#Region "Instance variables"

    Public Shared Event Read(Status As Boolean, factlog_list As List(Of FactLog))

#End Region

#Region "CRUD"

    Friend Shared Sub CMSG_GET_FACT_LOG()
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_GET_FACT_LOG_ANSWER, AddressOf SMSG_GET_FACT_LOG_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_GET_FACT_LOG, UShort))
        WriteFactLogPacket(packet)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)
    End Sub

    Private Shared Sub SMSG_GET_FACT_LOG_ANSWER(packet As ByteBuffer)
        Dim factlog_list As New List(Of FactLog)

        If packet.GetError() = ErrorMessage.SUCCESS Then

            Dim requestId As UInt32 = packet.ReadUint32()
            Dim nbResult As UInt32 = packet.ReadUint32()

            For i As UInt32 = 0 To nbResult
                Dim ht As FactLog = FactLog.BuildFactLog(packet)
                factlog_list.Add(ht)
                RaiseEvent Read(True, factlog_list)
            Next
        Else
            RaiseEvent Read(False, Nothing)
        End If

    End Sub

#End Region

#Region "Utilities"

    Private Shared Sub WriteFactLogPacket(ByRef packet As ByteBuffer)

        Dim requestId As UInt32 = packet.AssignRequestId()

        'packet.WriteInt32(attributes(ACCOUNT_ID_VARIABLE))
        'packet.WriteInt32(attributes(ENTITY_ID_VARIABLE))
        'packet.WriteInt32(attributes(PERIOD_VARIABLE))
        'packet.WriteInt32(attributes(VERSION_ID_VARIABLE))

    End Sub

#End Region

End Class
