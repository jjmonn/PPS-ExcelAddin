' Facts.vb
' 
'
'
'
' Author: Julien Monnereau
' Created: 01/09/2015
' Last modified: 14/12/2015


Imports System.Collections
Imports System.Collections.Generic
Imports CRUD

Class FactsManager

#Region "Intance Variables"

    Private requestIdFactsCommitDict As New SafeDictionary(Of UInt32, List(Of String))
    Public Event AfterUpdate(ByRef p_status As Boolean, ByRef p_resultsDict As Dictionary(Of String, ErrorMessage))
    Public Shared Event Read(p_status As Boolean, p_requestId As UInt32, p_fact_list As List(Of Fact))

#End Region

#Region "Interface"

    Public Sub New()
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_FACT_LIST_ANSWER, AddressOf SMSG_UPDATE_FACT_LIST_ANSWER)
    End Sub

    Friend Shared Function CMSG_GET_FACT(ByRef p_accountId As UInt32, _
                                        ByRef p_employeeId As UInt32, _
                                        ByRef p_versionId As UInt32, _
                                        ByRef p_startPeriod As UInt32, _
                                        ByRef p_endPeriod As UInt32) As Int32

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_GET_FACT_ANSWER, AddressOf SMSG_GET_FACT_ANSWER)
        Dim l_packet As New ByteBuffer(CType(ClientMessage.CMSG_GET_FACT, UShort))
        Dim l_requestId As Int32 = l_packet.AssignRequestId()

        l_packet.WriteUint32(p_accountId)
        l_packet.WriteUint32(p_employeeId)
        l_packet.WriteUint32(p_versionId)
        l_packet.WriteUint32(p_startPeriod)
        l_packet.WriteUint32(p_endPeriod)

        l_packet.Release()
        NetworkManager.GetInstance().Send(l_packet)
        Return l_requestId

    End Function

    Friend Sub CMSG_UPDATE_FACT_LIST(ByRef factsValues As List(Of Fact), _
                                     ByRef cellsAddresses As List(Of String))

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_FACT_LIST, UShort))
        Dim requestId As UInt32 = packet.AssignRequestId()
        requestIdFactsCommitDict.Add(requestId, cellsAddresses)
        packet.WriteUint32(factsValues.Count)
        For Each fact_value As Fact In factsValues
            fact_value.Dump(packet, False)
        Next
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_FACT_LIST_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim requestId As UInt32 = packet.GetRequestId()
            Dim resultsDict As New SafeDictionary(Of String, ErrorMessage)
            packet.ReadUint32()
            If requestIdFactsCommitDict.ContainsKey(requestId) Then
                For Each cell_address As String In requestIdFactsCommitDict(requestId)
                    packet.ReadUint32()
                    resultsDict.Add(cell_address, packet.ReadUint8())
                Next
                requestIdFactsCommitDict.Remove(requestId)
            Else
                System.Diagnostics.Debug.WriteLine("FACTS UDPATE LIST request id not in dictionary")
            End If
            RaiseEvent AfterUpdate(True, resultsDict)
        Else
            RaiseEvent AfterUpdate(False, Nothing)
        End If

    End Sub

    Private Shared Sub SMSG_GET_FACT_ANSWER(p_packet As ByteBuffer)

        Dim l_factList As New List(Of Fact)
        If p_packet.GetError() = ErrorMessage.SUCCESS Then
            Dim l_requestId As UInt32 = p_packet.GetRequestId()
            Dim nbResult As UInt32 = p_packet.ReadUint32()
            For i As UInt32 = 1 To nbResult
                Dim hl_fact As Fact = Fact.BuildFact(p_packet)
                l_factList.Add(hl_fact)
            Next
            RaiseEvent Read(True, l_requestId, l_factList)
        Else
            RaiseEvent Read(False, 0, Nothing)
        End If

    End Sub

#End Region

    Protected Overrides Sub finalize()

        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_FACT_LIST_ANSWER, AddressOf SMSG_UPDATE_FACT_LIST_ANSWER)

    End Sub


End Class
