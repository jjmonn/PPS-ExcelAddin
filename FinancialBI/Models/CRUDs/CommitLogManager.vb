Imports System.Collections
Imports System.Collections.Generic
Imports CRUD

Class CommitLogManager

#Region "Instance variables"

    Public Shared Event Read(Status As Boolean, commitlog_list As List(Of CommitLog))

#End Region

#Region "CRUD"

    Friend Shared Sub CMSG_GET_COMMIT_LOG(ByRef p_entityId As UInt32, ByRef p_period As UInt32)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_GET_COMMIT_LOG_ANSWER, AddressOf SMSG_GET_COMMIT_LOG_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_GET_COMMIT_LOG, UShort))
        Dim requestId As UInt32 = packet.AssignRequestId()

        WriteCommitLogPacket(packet, p_entityId, p_period)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)
    End Sub

    Private Shared Sub SMSG_GET_COMMIT_LOG_ANSWER(packet As ByteBuffer)
        Dim commitlog_list As New List(Of CommitLog)

        If packet.GetError() = ErrorMessage.SUCCESS Then

            Dim requestId As UInt32 = packet.ReadUint32()
            Dim nbResult As UInt32 = packet.ReadUint32()

            For i As UInt32 = 0 To nbResult
                Dim ht As CommitLog = CommitLog.BuildCommitLog(packet)
                commitlog_list.Add(ht)
                RaiseEvent Read(True, commitlog_list)
            Next
        Else
            RaiseEvent Read(False, Nothing)
        End If

    End Sub

#End Region

#Region "Utilities"

    Private Shared Sub WriteCommitLogPacket(ByRef packet As ByteBuffer, ByRef p_entityId As UInt32, ByRef p_period As UInt32)

        packet.WriteUint32(p_entityId)
        packet.WriteUint32(p_period)

    End Sub

#End Region

End Class
