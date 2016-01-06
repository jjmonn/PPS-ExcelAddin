Imports System.Collections
Imports System.Collections.Generic
Imports CRUD

Class AxisElemLogManager

#Region "Instance variables"

    Public Shared Event Read(Status As Boolean, axisElemlog_list As List(Of AxisElemLog))

#End Region

#Region "CRUD"

    Friend Shared Sub CMSG_GET_AXIS_ELEM_LOG(ByRef p_axisType As AxisType, ByRef p_startTimestamp As UInt64, ByRef p_endTimestamp As UInt64)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_GET_AXIS_ELEM_LOG_ANSWER, AddressOf SMSG_GET_AXIS_ELEM_LOG_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_GET_AXIS_ELEM_LOG, UShort))
        Dim requestId As UInt32 = packet.AssignRequestId()

        WriteAxisElemLogPacket(packet, p_axisType, p_startTimestamp, p_endTimestamp)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)
    End Sub

    Private Shared Sub SMSG_GET_AXIS_ELEM_LOG_ANSWER(packet As ByteBuffer)
        Dim axisElemlog_list As New List(Of AxisElemLog)

        If packet.GetError() = ErrorMessage.SUCCESS Then

            Dim requestId As UInt32 = packet.ReadUint32()
            Dim nbResult As UInt32 = packet.ReadUint32()

            For i As UInt32 = 0 To nbResult
                Dim ht As AxisElemLog = AxisElemLog.BuildAxisElemLog(packet)
                axisElemlog_list.Add(ht)
                RaiseEvent Read(True, axisElemlog_list)
            Next
        Else
            RaiseEvent Read(False, Nothing)
        End If

    End Sub

#End Region

#Region "Utilities"

    Private Shared Sub WriteAxisElemLogPacket(ByRef packet As ByteBuffer, ByRef p_axisType As AxisType, ByRef p_startTimestamp As UInt64, ByRef p_endTimestamp As UInt64)

        packet.WriteUint32(CUInt(p_axisType))
        packet.WriteUint64(p_startTimestamp)
        packet.WriteUint64(p_endTimestamp)

    End Sub

#End Region

End Class
