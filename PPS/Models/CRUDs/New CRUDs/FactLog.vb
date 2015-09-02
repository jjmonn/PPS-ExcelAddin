Imports System.Collections
Imports System.Collections.Generic

Public Class FactLog

#Region "Instance variables"

    Friend state_flag As Boolean = False
    Public Event Read(Status As Boolean, factlog_list As List(Of Hashtable))
#End Region

#Region "CRUD"

    Friend Sub CMSG_GET_FACT_LOG(ByRef attributes As Hashtable)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_GET_FACT_LOG_ANSWER, AddressOf SMSG_GET_FACT_LOG_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_GET_FACT_LOG, UShort))
        WriteFactLogPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)
    End Sub

    Private Sub SMSG_GET_FACT_LOG_ANSWER(packet As ByteBuffer)
        Dim factlog_list As New List(Of Hashtable)

        If packet.ReadInt32() = 0 Then

            Dim requestId As UInt32 = packet.ReadUint32()
            Dim nbResult As UInt32 = packet.ReadUint32()

            For i As UInt32 = 0 To nbResult
                Dim ht As New Hashtable

                GetFactLogHTFromPacket(packet, ht)
                factlog_list.Add(ht)
                RaiseEvent Read(True, factlog_list)
            Next
        Else
            RaiseEvent Read(False, Nothing)
        End If

    End Sub

#End Region

#Region "Utilities"

    Friend Shared Sub GetFactLogHTFromPacket(ByRef packet As ByteBuffer, ByRef factlog_ht As Hashtable)

        factlog_ht(FACTLOG_USER_VARIABLE) = packet.ReadString()
        factlog_ht(FACTLOG_DATE_VARIABLE) = packet.ReadUint64()
        factlog_ht(FACTLOG_CLIENT_ID_VARIABLE) = packet.ReadUint32()
        factlog_ht(FACTLOG_PRODUCT_ID_VARIABLE) = packet.ReadUint32()
        factlog_ht(FACTLOG_ADJUSTMENT_ID_VARIABLE) = packet.ReadUint32()
        factlog_ht(FACTLOG_VALUE_VARIABLE) = packet.ReadDouble()

    End Sub

    Private Sub WriteFactLogPacket(ByRef packet As ByteBuffer, ByRef attributes As Hashtable)

        Dim requestId As UInt32 = packet.AssignRequestId()

        packet.WriteInt32(attributes(ACCOUNT_ID_VARIABLE))
        packet.WriteInt32(attributes(ENTITY_ID_VARIABLE))
        packet.WriteInt32(attributes(PERIOD_VARIABLE))
        packet.WriteInt32(attributes(VERSION_ID_VARIABLE))

    End Sub

#End Region

End Class
