Imports System.Collections.Generic
Imports System.Collections
Imports CRUD

' Computer.vb
'
' Computing interface with c++ server
'
'
' To do:
'       - 
'
'
' Author: Julien Monnereau Julien
' Created: 20/07/2015
' Last modified: 21/08/2015


Public Class ComputerInputEntity


#Region "Instance Variables"

    ' Events
    Public Event ComputationAnswered(ByRef entityId As String, ByRef status As Boolean)

    ' Variables
    Private requestIdEntityIdDict As New Dictionary(Of UInt32, UInt32)
    Private dataMap As Dictionary(Of Int32, Dictionary(Of String, Double))
    Private entityId As Int32
    Private m_versionId As Int32
    Private accountId As Int32
    Private periodIdentifier As String
    Private periodsTokenDict As Dictionary(Of String, String)


#End Region


    ' dataMap: [account_id][period_token] => value
    Friend Function GetDataMap() As Dictionary(Of Int32, Dictionary(Of String, Double))
        Return dataMap
    End Function


    Friend Sub CMSG_SOURCED_COMPUTE(ByRef p_versionId As Int32, _
                                    ByRef p_entityId As Int32, _
                                    ByRef p_currencyId As Int32, _
                                    ByRef accKeysArray() As Int32, _
                                    ByRef periodsArray() As Int32, _
                                    ByRef valuesArray() As Double)


        m_versionId = p_versionId
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_SOURCED_COMPUTE_RESULT, AddressOf SMSG_SOURCED_COMPUTE_RESULT)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_SOURCED_COMPUTE, UShort))
        Dim requestId As Int32 = packet.AssignRequestId()
        requestIdEntityIdDict.Add(requestId, p_entityId)

        packet.WriteUint32(p_versionId)                          ' version_id
        packet.WriteUint32(p_entityId)                           ' entity_id
        packet.WriteUint32(p_currencyId)                         ' currency_id

        packet.WriteUint32(valuesArray.Length)
        For i = 0 To valuesArray.Length - 1
            packet.WriteUint32(accKeysArray(i))
            packet.WriteUint32(periodsArray(i))
            packet.WriteDouble(valuesArray(i))
        Next

        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub


    ' Server Response
    Private Sub SMSG_SOURCED_COMPUTE_RESULT(packet As ByteBuffer)

        If packet.GetError() = ErrorMessage.SUCCESS Then
            Dim request_id = packet.GetRequestId()
            dataMap = New Dictionary(Of Int32, Dictionary(Of String, Double))

            Dim version As Version = GlobalVariables.Versions.GetValue(m_versionId)
            If version Is Nothing Then
                MsgBox("Compute returned a result for an invalid version.")
                Exit Sub
            End If

            periodsTokenDict = GlobalVariables.Versions.GetPeriodTokensDict(m_versionId)
            Select Case version.TimeConfiguration
                Case CRUD.TimeConfig.YEARS : periodIdentifier = Computer.YEAR_PERIOD_IDENTIFIER
                Case CRUD.TimeConfig.MONTHS : periodIdentifier = Computer.MONTH_PERIOD_IDENTIFIER
            End Select
            FillEntityData(packet)
            RaiseEvent ComputationAnswered(requestIdEntityIdDict(request_id), True)
            requestIdEntityIdDict.Remove(request_id)
        Else
            RaiseEvent ComputationAnswered("", False)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_SOURCED_COMPUTE_RESULT, AddressOf SMSG_SOURCED_COMPUTE_RESULT)
        ' here ?!! riority high

    End Sub

    ' DataMap Filling
    Private Sub FillEntityData(ByRef packet As ByteBuffer)

        entityId = packet.ReadUint32()
        For account_index As Int32 = 1 To packet.ReadUint32()
            accountId = packet.ReadUint32()

            Dim periodDict As New Dictionary(Of String, Double)
            ' Non aggregated data
            For period_index As Int16 = 0 To packet.ReadUint16() - 1
                periodDict.Add(periodsTokenDict(periodIdentifier & period_index), packet.ReadDouble())
            Next

            ' Aggreagted data
            For aggregationIndex As Int32 = 0 To packet.ReadUint32() - 1
                periodDict.Add(periodsTokenDict(Computer.YEAR_PERIOD_IDENTIFIER & aggregationIndex), packet.ReadDouble())
            Next
            dataMap.Add(accountId, periodDict)
        Next

        For children_index As Int32 = 1 To packet.ReadUint32()
            FillEntityData(packet)
        Next

    End Sub


End Class
