Imports System.Collections
Imports System.Collections.Generic


' ExchangeRate2.vb
'
' CRUD for exchangeRates table - relation with c++ server
'
' To do:
'       - Quid update / listen call backs... priority high
'
'
' Author: Julien Monnereau
' Created: 05/08/2015
' Last modified: 26/08/2015



Friend Class ExchangeRate


#Region "Instance variables"

    ' Variables
    Friend state_flag As Boolean
    Friend exchangeRatesDict As New Dictionary(Of String, Double)
    Private request_id As Dictionary(Of UInt32, Boolean)

    ' Events
    Public Event ObjectInitialized(ByRef status As Boolean)
    Public Event ExchangeRateUpdateEvent(ByRef status As Boolean, ByRef ht As Hashtable)
   
    ' Constants
    Friend Const RATES_CURRENCIES_TOKEN_SEPARATOR As Char = "#"

#End Region


#Region "Init"

    Friend Sub New()

        state_flag = False
        LoadExchangeRatesTable()

    End Sub

    Friend Sub LoadExchangeRatesTable()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_EXCHANGE_RATE_ANSWER, AddressOf SMSG_LIST_EXCHANGE_RATE_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_LIST_EXCHANGE_RATE, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    ' ExchangeRates_hash: [currency#ratesVersion#period] => rateValue
    Private Sub SMSG_LIST_EXCHANGE_RATE_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            exchangeRatesDict.Clear()
            For i As Int32 = 1 To packet.ReadInt32()
                Dim tmp_ht As New Hashtable
                GetExchangeRateHTFromPacket(packet, tmp_ht)
                exchangeRatesDict(tmp_ht(EX_RATES_LOCAL_CURR_VAR) & Computer.TOKEN_SEPARATOR & _
                                  tmp_ht(EX_RATES_RATE_VARIABLE) & Computer.TOKEN_SEPARATOR & _
                                  tmp_ht(EX_RATES_PERIOD_VARIABLE)) = _
                                  tmp_ht(EX_RATES_RATE_VARIABLE)
            Next
            RaiseEvent ObjectInitialized(True)
        Else
            state_flag = False
            RaiseEvent ObjectInitialized(True)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_EXCHANGE_RATE_ANSWER, AddressOf SMSG_LIST_EXCHANGE_RATE_ANSWER)

    End Sub

#End Region


#Region "CRUD"

    ' add read => must listen in case somebody is already editing
    ' or block any CRUD editing when someone is already editing ?!


    Friend Sub CMSG_UPDATE_EXCHANGE_RATE(ByRef id As UInt32, _
                                  ByRef updated_var As String, _
                                  ByRef new_value As String)

        ' to be reviewed priority high
        '     Dim tmp_ht As Hashtable = exchangeRatesDict(id).clone ' check clone !!!!
        'tmp_ht(updated_var) = new_value

        'NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_EXCHANGE_RATE_ANSWER, AddressOf SMSG_UPDATE_EXCHANGE_RATE_ANSWER)
        'Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_EXCHANGE_RATE, UShort))
        'WriteExchangeRatePacket(packet, tmp_ht)
        'packet.Release()
        'NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Sub CMSG_UPDATE_EXCHANGE_RATE(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_EXCHANGE_RATE_ANSWER, AddressOf SMSG_UPDATE_EXCHANGE_RATE_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_EXCHANGE_RATE, UShort))
        WriteExchangeRatePacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_EXCHANGE_RATE_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim ht As New Hashtable
            GetExchangeRateHTFromPacket(packet, ht)
            NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_EXCHANGE_RATE_ANSWER, AddressOf SMSG_UPDATE_EXCHANGE_RATE_ANSWER)

            exchangeRatesDict(ht(EX_RATES_LOCAL_CURR_VAR) & Computer.TOKEN_SEPARATOR & _
                              ht(EX_RATES_RATE_VARIABLE) & Computer.TOKEN_SEPARATOR & _
                              ht(EX_RATES_PERIOD_VARIABLE)) = _
                              ht(EX_RATES_RATE_VARIABLE)

            RaiseEvent ExchangeRateUpdateEvent(True, ht)
        Else
            RaiseEvent ExchangeRateUpdateEvent(False, Nothing)
        End If

    End Sub

#End Region


#Region "Utilities"

    Friend Shared Sub GetExchangeRateHTFromPacket(ByRef packet As ByteBuffer, ByRef exchangeRate_ht As Hashtable)

        exchangeRate_ht(EX_RATES_LOCAL_CURR_VAR) = packet.ReadUint32()
        exchangeRate_ht(EX_RATES_RATE_VERSION) = packet.ReadUint32()
        exchangeRate_ht(EX_RATES_PERIOD_VARIABLE) = packet.ReadUint32()
        exchangeRate_ht(EX_RATES_RATE_VARIABLE) = packet.ReadDouble()

    End Sub

    Private Sub WriteExchangeRatePacket(ByRef packet As ByteBuffer, ByRef attributes As Hashtable)

        packet.WriteUint32(attributes(EX_RATES_LOCAL_CURR_VAR))
        packet.WriteUint32(attributes(EX_RATES_RATE_VERSION))
        packet.WriteUint32(attributes(EX_RATES_PERIOD_VARIABLE))
        packet.WriteDouble(attributes(EX_RATES_RATE_VARIABLE))

    End Sub


#End Region


End Class
