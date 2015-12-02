﻿' Facts.vb
' 
'
'
'
' Author: Julien Monnereau
' Created: 01/09/2015
' Last modified: 01/09/2015


Imports System.Collections
Imports System.Collections.Generic


Public Class Facts


#Region "Intance Variables"

    Private requestIdFactsCommitDict As New SafeDictionary(Of UInt32, List(Of String))
    Public Event AfterUpdate(ByRef status As Boolean, ByRef resultsDict As Dictionary(Of String, ErrorMessage))


#End Region


#Region "Interface"


    Public Sub New()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_FACT_LIST_ANSWER, AddressOf SMSG_UPDATE_FACT_LIST_ANSWER)

    End Sub

    Friend Sub CMSG_UPDATE_FACT_LIST(ByRef factsValues As List(Of Hashtable), _
                                     ByRef cellsAddresses As List(Of String))

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_FACT_LIST, UShort))
        Dim requestId As UInt32 = packet.AssignRequestId()
        requestIdFactsCommitDict.Add(requestId, cellsAddresses)
        packet.WriteUint32(factsValues.Count)
        For Each fact_value As Hashtable In factsValues
            packet.WriteUint32(fact_value(ENTITY_ID_VARIABLE))
            packet.WriteUint32(fact_value(ACCOUNT_ID_VARIABLE))
            packet.WriteUint32(fact_value(PERIOD_VARIABLE))
            packet.WriteUint32(fact_value(VERSION_ID_VARIABLE))
            packet.WriteUint32(fact_value(CLIENT_ID_VARIABLE))
            packet.WriteUint32(fact_value(PRODUCT_ID_VARIABLE))
            packet.WriteUint32(fact_value(ADJUSTMENT_ID_VARIABLE))
            packet.WriteDouble(fact_value(VALUE_VARIABLE))
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


#End Region


    Protected Overrides Sub finalize()

        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_FACT_LIST_ANSWER, AddressOf SMSG_UPDATE_FACT_LIST_ANSWER)

    End Sub


End Class
