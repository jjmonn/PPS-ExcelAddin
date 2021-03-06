﻿Imports System.Collections.Generic
Imports System.Collections
Imports System.Linq
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
' Last modified: 10/01/2016


Public Class ComputerInputEntity


#Region "Instance Variables"

    ' Events
    Public Event ComputationAnswered(ByRef status As Boolean)

    ' Variables
    '    Private m_requestIdEntityIdDict As New SafeDictionary(Of UInt32, UInt32)
    Private m_entitiesIdComputationQueue As List(Of Int32)
    Private m_dataMap As New SafeDictionary(Of Int32, Dictionary(Of Int32, Dictionary(Of String, Double)))
    '   Private m_entityId As Int32
    Private m_versionId As Int32
    Private m_accountId As Int32
    Private m_periodIdentifier As String
    Private m_periodsTokenDict As Dictionary(Of String, String)


#End Region

    ' dataMap: [account_id][period_token] => value
    Friend Function GetDataMap(ByRef p_entityId As Int32) As Dictionary(Of Int32, Dictionary(Of String, Double))

        If m_dataMap.ContainsKey(p_entityId) Then
            Return m_dataMap(p_entityId)
        Else
            Return Nothing
        End If

    End Function

    Friend Function RemoveEntityDataFromDataMap(ByRef p_entityId As Int32) As Boolean

        If m_dataMap.ContainsKey(p_entityId) Then
            Return m_dataMap.Remove(p_entityId)
        End If
        Return False

    End Function

    ' Compute request
    Friend Function CMSG_SOURCED_COMPUTE(ByRef p_versionId As Int32, _
                                         ByRef p_entitiesId As List(Of Int32), _
                                         ByRef p_entitiesIdInputsAccounts As Dictionary(Of Int32, Int32()), _
                                         ByRef p_entitiesIdInputsPeriods As Dictionary(Of Int32, Int32()), _
                                         ByRef p_entitiesIdInputsValues As Dictionary(Of Int32, Double())) As Boolean

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_SOURCED_COMPUTE_RESULT, AddressOf SMSG_SOURCED_COMPUTE_RESULT)
        m_versionId = p_versionId
        Dim l_version As Version = GlobalVariables.Versions.GetValue(p_versionId)
        If l_version Is Nothing Then Return False

        m_entitiesIdComputationQueue = p_entitiesId.ToList
        For Each l_entityId As Int32 In p_entitiesId

            Dim l_entityCurrency As EntityCurrency = GlobalVariables.EntityCurrencies.GetValue(l_entityId)
            If Not l_entityCurrency Is Nothing Then

                Dim l_packet As New ByteBuffer(CType(ClientMessage.CMSG_SOURCED_COMPUTE, UShort))
                Dim requestId As Int32 = l_packet.AssignRequestId()

                l_packet.WriteUint32(Account.AccountProcess.FINANCIAL)
                l_packet.WriteUint32(p_versionId)                                               ' version_id
                l_packet.WriteUint32(l_version.GlobalFactVersionId)                             ' global facts version id
                l_packet.WriteUint32(l_version.RateVersionId)                                   ' rates version id
                l_packet.WriteUint32(l_entityId)                                                ' entity_id
                l_packet.WriteUint32(l_entityCurrency.CurrencyId)                               ' currency_id
                l_packet.WriteUint32(l_version.StartPeriod)
                l_packet.WriteUint32(l_version.NbPeriod)
                l_packet.WriteUint32(0) ' nb accounts

                l_packet.WriteUint32(p_entitiesIdInputsValues(l_entityId).Length)
                For i = 0 To p_entitiesIdInputsValues(l_entityId).Length - 1
                    l_packet.WriteUint32(p_entitiesIdInputsAccounts(l_entityId)(i))
                    l_packet.WriteUint32(p_entitiesIdInputsPeriods(l_entityId)(i))
                    l_packet.WriteDouble(p_entitiesIdInputsValues(l_entityId)(i))
                Next
                l_packet.Release()
                NetworkManager.GetInstance().Send(l_packet)

            End If
        Next
        Return True
    End Function

    ' Server Response
    Private Sub SMSG_SOURCED_COMPUTE_RESULT(packet As ByteBuffer)

        If packet.GetError() = ErrorMessage.SUCCESS Then
            Dim request_id = packet.GetRequestId()

            Dim version As Version = GlobalVariables.Versions.GetValue(m_versionId)
            If version Is Nothing Then
                MsgBox(Local.GetValue("CUI.msg_return_invalid_version"))
                RaiseEvent ComputationAnswered(False)
                Exit Sub
            End If

            m_periodsTokenDict = GlobalVariables.Versions.GetPeriodTokensDict(m_versionId, Nothing)
            Select Case version.TimeConfiguration
                Case CRUD.TimeConfig.YEARS : m_periodIdentifier = Computer.YEAR_PERIOD_IDENTIFIER
                Case CRUD.TimeConfig.MONTHS : m_periodIdentifier = Computer.MONTH_PERIOD_IDENTIFIER
            End Select

            Dim l_entityId As Int32 = packet.ReadUint32()
            Dim l_entityDataMap As New SafeDictionary(Of Int32, Dictionary(Of String, Double))

            FillEntityData(packet, l_entityId, l_entityDataMap)

            If m_dataMap.ContainsKey(l_entityId) Then
                System.Diagnostics.Debug.WriteLine("Compute single entity : m_dataMap already contained l_entityId = " & l_entityId)
                m_dataMap(l_entityId) = l_entityDataMap
            Else
                m_dataMap.Add(l_entityId, l_entityDataMap)
            End If

            m_entitiesIdComputationQueue.Remove(l_entityId)
            If m_entitiesIdComputationQueue.Count = 0 Then
                RaiseEvent ComputationAnswered(True)
                NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_SOURCED_COMPUTE_RESULT, AddressOf SMSG_SOURCED_COMPUTE_RESULT)
            End If
        Else
            Select Case (CType(packet.GetError(), ErrorMessage))
                Case ErrorMessage.SYSTEM
                    MsgBox(Local.GetValue("CUI.msg_error_system"))
                Case ErrorMessage.PERMISSION_DENIED
                    MsgBox(Local.GetValue("CUI.msg_permission_denied"))
            End Select
            RaiseEvent ComputationAnswered(False)
            NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_SOURCED_COMPUTE_RESULT, AddressOf SMSG_SOURCED_COMPUTE_RESULT)
        End If
        '    NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_SOURCED_COMPUTE_RESULT, AddressOf SMSG_SOURCED_COMPUTE_RESULT)
        ' here ?!! riority high

    End Sub

    ' DataMap Filling
    Private Sub FillEntityData(ByRef packet As ByteBuffer, _
                               ByRef p_entityId As Int32, _
                               ByRef p_entityDataMap As Dictionary(Of Int32, Dictionary(Of String, Double)))


        For account_index As Int32 = 1 To packet.ReadUint32()
            m_accountId = packet.ReadUint32()

            Dim periodDict As New SafeDictionary(Of String, Double)
            ' Non aggregated data
            For period_index As Int16 = 0 To packet.ReadUint16() - 1
                periodDict.Add(m_periodsTokenDict(m_periodIdentifier & period_index), packet.ReadDouble())
            Next

            ' Aggreagted data
            For aggregationIndex As Int32 = 0 To packet.ReadUint32() - 1
                periodDict.Add(m_periodsTokenDict(Computer.YEAR_PERIOD_IDENTIFIER & aggregationIndex), packet.ReadDouble())
            Next
            p_entityDataMap.Add(m_accountId, periodDict)
        Next

        For children_index As Int32 = 1 To packet.ReadUint32()
            FillEntityData(packet, p_entityId, p_entityDataMap)
        Next

    End Sub

    Friend Sub Flush()

        If m_dataMap IsNot Nothing Then m_dataMap.Clear()
        If m_entitiesIdComputationQueue IsNot Nothing Then m_entitiesIdComputationQueue.Clear()
        If m_periodsTokenDict IsNot Nothing Then m_periodsTokenDict.Clear()

    End Sub

End Class
