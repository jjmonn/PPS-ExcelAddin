' ExchangeRate.vb
'
' CRUD for exchangeRates table - relation with c++ server
'
' To do:
'       - Quid update / listen call backs... priority high
'
'
' Author: Julien Monnereau
' Created: 05/08/2015
' Last modified: 19/09/2015


Imports System.Collections
Imports System.Collections.Generic



Friend Class ExchangeRate


#Region "Instance variables"

    ' Variables
    Friend state_flag As Boolean
    Friend m_exchangeRatesHash As New Dictionary(Of Tuple(Of Int32, Int32, Int32), Double)
    Private request_id As Dictionary(Of UInt32, Boolean)

    ' Events
    Public Event ObjectInitialized(ByRef status As Boolean)
    Public Event Read(ByRef status As Boolean, ByRef attributes As Hashtable)
    Public Event UpdateEvent(ByRef status As Boolean, _
                             ByRef destinationCurrency As Int32, _
                             ByRef ratesVersion As Int32, _
                             ByRef period As Int32)
    Public Event UpdateListEvent(ByRef status As Boolean, ByRef resultList As Dictionary(Of Int32, Boolean))

#End Region


#Region "Init"

    Friend Sub New()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_EXCHANGE_RATE_ANSWER, AddressOf SMSG_READ_EXCHANGE_RATE_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_EXCHANGE_RATE_ANSWER, AddressOf SMSG_LIST_EXCHANGE_RATE_ANSWER)
        state_flag = False
        LoadExchangeRatesTable()

    End Sub

    Friend Sub LoadExchangeRatesTable()

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_LIST_EXCHANGE_RATE, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_LIST_EXCHANGE_RATE_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            m_exchangeRatesHash.Clear()
            For i As Int32 = 1 To packet.ReadInt32()
                Dim tmp_ht As New Hashtable
                GetExchangeRateHTFromPacket(packet, tmp_ht)
                AddRecordToExchangeRatesHash(tmp_ht(EX_RATES_DESTINATION_CURR_VAR), _
                                             tmp_ht(EX_RATES_RATE_VERSION), _
                                             tmp_ht(EX_RATES_PERIOD_VARIABLE), _
                                             tmp_ht(EX_RATES_RATE_VARIABLE))
            Next
            state_flag = True
            RaiseEvent ObjectInitialized(True)
        Else
            state_flag = False
            RaiseEvent ObjectInitialized(True)
        End If

    End Sub

#End Region


#Region "CRUD"

    ' add read => must listen in case somebody is already editing
    ' or block any CRUD editing when someone is already editing ?!
    Friend Sub SMSG_READ_EXCHANGE_RATE_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim tmpHt As New Hashtable
            GetExchangeRateHTFromPacket(packet, tmpHt)
            AddRecordToExchangeRatesHash(tmpHt(EX_RATES_DESTINATION_CURR_VAR), _
                                         tmpHt(EX_RATES_RATE_VERSION), _
                                         tmpHt(EX_RATES_PERIOD_VARIABLE), _
                                         tmpHt(EX_RATES_RATE_VARIABLE))
            RaiseEvent Read(True, tmpHt)
        Else
            RaiseEvent Read(False, Nothing)
        End If

    End Sub

    Friend Sub CMSG_UPDATE_EXCHANGE_RATE(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_EXCHANGE_RATE_ANSWER, AddressOf SMSG_UPDATE_EXCHANGE_RATE_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_EXCHANGE_RATE, UShort))
        WriteExchangeRatePacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_EXCHANGE_RATE_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim destinationCurrency As Int32 = packet.ReadUint32()
            Dim ratesVersion As Int32 = packet.ReadUint32()
            Dim period As Int32 = packet.ReadUint32()
            RaiseEvent UpdateEvent(True, destinationCurrency, ratesVersion, period)
        Else
            RaiseEvent UpdateEvent(False, 0, 0, 0)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_EXCHANGE_RATE_ANSWER, AddressOf SMSG_UPDATE_EXCHANGE_RATE_ANSWER)

    End Sub

    Friend Sub CMSG_UPDATE_EXCHANGE_RATE_LIST(ByRef p_currencies As Hashtable)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_EXCHANGE_RATE_LIST_ANSWER, AddressOf SMSG_UPDATE_EXCHANGE_RATE_LIST_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_EXCHANGE_RATE_LIST, UShort))

        packet.WriteInt32(p_currencies.Count())
        For Each attributes As Hashtable In p_currencies.Values
            packet.WriteUint8(CRUDAction.UPDATE)
            WriteExchangeRatePacket(packet, attributes)
        Next
        packet.Release()
        NetworkManager.GetInstance().Send(packet)
    End Sub

    Private Sub SMSG_UPDATE_EXCHANGE_RATE_LIST_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim resultList As New Dictionary(Of Int32, Boolean)
            Dim nbResult As Int32 = packet.ReadInt32()

            For i As Int32 = 1 To nbResult
                Dim id As Int32 = packet.ReadInt32()
                If (resultList.ContainsKey(id)) Then
                    resultList(id) = packet.ReadBool()
                Else
                    resultList.Add(id, packet.ReadBool)
                End If
                packet.ReadString()
            Next

            RaiseEvent UpdateListEvent(False, resultList)
        Else
            RaiseEvent UpdateListEvent(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_EXCHANGE_RATE_LIST_ANSWER, AddressOf SMSG_UPDATE_EXCHANGE_RATE_LIST_ANSWER)

    End Sub

#End Region


#Region "Utilities"

    Private Shared Sub GetExchangeRateHTFromPacket(ByRef packet As ByteBuffer, ByRef exchangeRate_ht As Hashtable)

        exchangeRate_ht(EX_RATES_DESTINATION_CURR_VAR) = packet.ReadUint32()
        exchangeRate_ht(EX_RATES_RATE_VERSION) = packet.ReadUint32()
        exchangeRate_ht(EX_RATES_PERIOD_VARIABLE) = packet.ReadUint32()
        exchangeRate_ht(EX_RATES_RATE_VARIABLE) = packet.ReadDouble()

    End Sub

    Private Sub WriteExchangeRatePacket(ByRef packet As ByteBuffer, ByRef attributes As Hashtable)

        packet.WriteUint32(attributes(EX_RATES_DESTINATION_CURR_VAR))
        packet.WriteUint32(attributes(EX_RATES_RATE_VERSION))
        packet.WriteUint32(attributes(EX_RATES_PERIOD_VARIABLE))
        packet.WriteDouble(attributes(EX_RATES_RATE_VARIABLE))

    End Sub

    Private Sub AddRecordToExchangeRatesHash(ByRef p_destinationCurrency As Int32, _
                                             ByRef p_versionId As Int32, _
                                             ByRef p_period As Int32, _
                                             ByRef p_rateValue As Double)

        Dim key As Tuple(Of Int32, Int32, Int32) = Tuple.Create(p_destinationCurrency, p_versionId, p_period)
        If m_exchangeRatesHash.ContainsKey(key) Then
            m_exchangeRatesHash(key) = p_rateValue
        Else
            m_exchangeRatesHash.Add(key, p_rateValue)
        End If

    End Sub

#End Region


    Protected Overrides Sub finalize()

        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_EXCHANGE_RATE_ANSWER, AddressOf SMSG_LIST_EXCHANGE_RATE_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_READ_EXCHANGE_RATE_ANSWER, AddressOf SMSG_READ_EXCHANGE_RATE_ANSWER)

    End Sub




End Class
