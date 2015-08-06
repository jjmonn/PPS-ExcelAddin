Imports System.Collections
Imports System.Collections.Generic


' ExchangeRate2.vb
'
' CRUD for exchangeRates table - relation with c++ server
'
' To do:
'       - CRUD event put server response success as param !! priority high !!
'
'
'
' Author: Julien Monnereau
' Created: 05/08/2015
' Last modified: 05/08/2015



Friend Class ExchangeRate2

#Region "Instance variables"

    ' Variables
    Friend server_response_flag As Boolean
    Friend exchangeRates_hash As New Hashtable
    Private request_id As Dictionary(Of UInt32, Boolean)

    ' Events
    Public Event ObjectInitialized()
    Public Event ExchangeRateCreationEvent(ByRef attributes As Hashtable)
    Public Shared Event ExchangeRateRead(ByRef attributes As Hashtable)
    Public Event ExchangeRateUpdateEvent(ByRef ht As Hashtable)
    Public Event ExchangeRateDeleteEvent(ByRef id As UInt32)

    ' Constants
    Friend Const RATES_CURRENCIES_TOKEN_SEPARATOR As Char = "#"

#End Region


#Region "Init"

    Friend Sub New()

        LoadExchangeRatesTable()

    End Sub

    Friend Sub LoadExchangeRatesTable()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_EXCHANGE_RATE_ANSWER, AddressOf SMSG_LIST_EXCHANGE_RATE_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_LIST_EXCHANGE_RATE, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    ' exchangeRates_hash: [rateVersionId][token][period] => rateValue
    Private Sub SMSG_LIST_EXCHANGE_RATE_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            exchangeRates_hash.Clear()

            For i As Int32 = 1 To packet.ReadInt32()

                Dim tmp_ht As New Hashtable
                GetExchangeRateHTFromPacket(packet, tmp_ht)
                Dim rateCurrenciesToken As String = TokenizeRateCurrencies(tmp_ht(EX_RATES_ORIGIN_CURR_VAR), _
                                                                           tmp_ht(EX_RATES_DEST_CURR_VAR))
                exchangeRates_hash(tmp_ht(EX_RATES_RATE_VARIABLE)) _
                                  (rateCurrenciesToken) _
                                  (tmp_ht(EX_RATES_PERIOD_VARIABLE)) = tmp_ht(EX_RATES_RATE_VARIABLE)

            Next
            NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_EXCHANGE_RATE_ANSWER, AddressOf SMSG_LIST_EXCHANGE_RATE_ANSWER)
            server_response_flag = True
            RaiseEvent ObjectInitialized()
        Else
            server_response_flag = False
        End If

    End Sub

#End Region


#Region "CRUD"

    Friend Sub CMSG_CREATE_EXCHANGE_RATE(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_EXCHANGE_RATE_ANSWER, AddressOf SMSG_CREATE_EXCHANGE_RATE_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_EXCHANGE_RATE, UShort))
        WriteExchangeRatePacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_EXCHANGE_RATE_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            MsgBox(packet.ReadString())
            Dim tmp_ht As New Hashtable
            GetExchangeRateHTFromPacket(packet, tmp_ht)
            exchangeRates_hash(tmp_ht(ID_VARIABLE)) = tmp_ht
            NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_EXCHANGE_RATE_ANSWER, AddressOf SMSG_CREATE_EXCHANGE_RATE_ANSWER)
            RaiseEvent ExchangeRateCreationEvent(tmp_ht)
        Else

        End If

    End Sub

    Friend Shared Sub CMSG_READ_EXCHANGE_RATE(ByRef id As UInt32)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_EXCHANGE_RATE_ANSWER, AddressOf SMSG_READ_EXCHANGE_RATE_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_EXCHANGE_RATE, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Shared Sub SMSG_READ_EXCHANGE_RATE_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim ht As New Hashtable
            GetExchangeRateHTFromPacket(packet, ht)
            NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_EXCHANGE_RATE_ANSWER, AddressOf SMSG_READ_EXCHANGE_RATE_ANSWER)
            RaiseEvent ExchangeRateRead(ht)
        Else

        End If

    End Sub

    Friend Sub UpdateBatch(ByRef updates As List(Of Object()))

        ' to be implemented !!!! priority normal

        request_id.Clear()
        For Each update As Object() In updates


        Next


    End Sub

    Friend Sub CMSG_UPDATE_EXCHANGE_RATE(ByRef id As UInt32, _
                                  ByRef updated_var As String, _
                                  ByRef new_value As String)

        Dim tmp_ht As Hashtable = exchangeRates_hash(id).clone ' check clone !!!!
        tmp_ht(updated_var) = new_value

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_EXCHANGE_RATE_ANSWER, AddressOf SMSG_UPDATE_EXCHANGE_RATE_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_EXCHANGE_RATE, UShort))
        WriteExchangeRatePacket(packet, tmp_ht)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

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
            exchangeRates_hash(ht(ID_VARIABLE)) = ht
            NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_EXCHANGE_RATE_ANSWER, AddressOf SMSG_UPDATE_EXCHANGE_RATE_ANSWER)
            RaiseEvent ExchangeRateUpdateEvent(ht)
        Else

        End If

    End Sub

    Friend Sub DeleteAllRatesForVersion(ByRef fxVerionsId As UInt32)

        ' to be implemented priority normal

    End Sub

    Friend Sub CMSG_DELETE_EXCHANGE_RATE(ByRef id As UInt32)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_EXCHANGE_RATE_ANSWER, AddressOf SMSG_DELETE_EXCHANGE_RATE_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_EXCHANGE_RATE, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_EXCHANGE_RATE_ANSWER()

        '   If packet.ReadInt32() = 0 Then
        Dim id As UInt32
        ' get id from request_id ?
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_EXCHANGE_RATE_ANSWER, AddressOf SMSG_DELETE_EXCHANGE_RATE_ANSWER)
        RaiseEvent ExchangeRateDeleteEvent(id)

    End Sub

#End Region


#Region "Mappings"

    Friend Function GetExchangeRatesDictionary(ByRef Key As String, ByRef Value As String) As Hashtable

        Dim tmpHT As New Hashtable
        For Each id In exchangeRates_hash.Keys
            tmpHT(exchangeRates_hash(id)(Key)) = exchangeRates_hash(id)(Value)
        Next
        Return tmpHT

    End Function

#End Region


#Region "Utilities"

    Friend Shared Sub GetExchangeRateHTFromPacket(ByRef packet As ByteBuffer, ByRef exchangeRate_ht As Hashtable)

        exchangeRate_ht(EX_RATES_ORIGIN_CURR_VAR) = packet.ReadUint32()
        exchangeRate_ht(EX_RATES_DEST_CURR_VAR) = packet.ReadUint32()
        exchangeRate_ht(EX_RATES_RATE_VERSION) = packet.ReadUint32()
        exchangeRate_ht(EX_RATES_PERIOD_VARIABLE) = packet.ReadUint32()
        exchangeRate_ht(EX_RATES_RATE_VARIABLE) = packet.ReadDouble()

    End Sub

    Private Sub WriteExchangeRatePacket(ByRef packet As ByteBuffer, ByRef attributes As Hashtable)

        packet.WriteUint32(attributes(EX_RATES_ORIGIN_CURR_VAR))
        packet.WriteUint32(attributes(EX_RATES_DEST_CURR_VAR))
        packet.WriteUint32(attributes(EX_RATES_RATE_VERSION))
        packet.WriteUint32(attributes(EX_RATES_PERIOD_VARIABLE))
        packet.WriteDouble(attributes(EX_RATES_RATE_VARIABLE))
      
    End Sub

    Public Shared Function TokenizeRateCurrencies(ByRef originCurrencyId As UInt32, _
                                                  ByRef destinationCurrencyId As UInt32) As String

        Return originCurrencyId & RATES_CURRENCIES_TOKEN_SEPARATOR & destinationCurrencyId

    End Function

    Public Shared Function GetOriginCurrencyIdFromToken(ByRef token As String) As UInt32

        Dim sepPosition As UInt16 = token.IndexOf(RATES_CURRENCIES_TOKEN_SEPARATOR)
        Return Left(token, sepPosition)

    End Function

    Public Shared Function GetDestinationCurrencyIdFromToken(ByRef token As String) As UInt32

        Dim sepPosition As UInt16 = token.IndexOf(RATES_CURRENCIES_TOKEN_SEPARATOR)
        Return Right(token, Len(token) - sepPosition)

    End Function

#End Region


    Public Shared Function ReverseToken(ByRef token As String) As String

        Return GetDestinationCurrencyIdFromToken(token) _
               & RATES_CURRENCIES_TOKEN_SEPARATOR _
               & GetOriginCurrencyIdFromToken(token)

    End Function


End Class
