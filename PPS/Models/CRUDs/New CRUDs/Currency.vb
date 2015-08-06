' currency2.vb
'
' CRUD for currencies table - relation with c++ server
'
'
'
'
' Author: Julien Monnereau
' Created: 03/08/2015
' Last modified: 03/08/2015


Imports System.Collections
Imports System.Collections.Generic



Friend Class Currency2

#Region "Instance variables"


    ' Variables
    Friend state_flag As Boolean
    Friend server_response_flag As Boolean
    Friend currencies_hash As New Hashtable

    ' Events
    Public Event ObjectInitialized()
    Public Event currencyCreationEvent(ByRef attributes As Hashtable)
    Public Event currencyUpdateEvent(ByRef attributes As Hashtable)
    Public Event currencyDeleteEvent(ByRef id As UInt32)


#End Region


#Region "Init"

    Friend Sub New()

        state_flag = False
        ' LoadCurrencyTable()

    End Sub

    'Friend Sub LoadCurrencyTable()

    '    NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_CURRENCY_ANSWER, AddressOf SMSG_LIST_CURRENCY_ANSWER)
    '    Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_LIST_CURRENCY, UShort))
    '    packet.Release()
    '    NetworkManager.GetInstance().Send(packet)

    'End Sub

    'Private Sub SMSG_LIST_CURRENCY_ANSWER(packet As ByteBuffer)

    '    If packet.ReadInt32() = 0 Then
    '        Dim nb_currencies = packet.ReadInt32()
    '        For i As Int32 = 1 To nb_currencies
    '            Dim tmp_ht As New Hashtable
    '            GetcurrencyHTFromPacket(packet, tmp_ht)
    '            currencies_hash(CInt(tmp_ht(ID_VARIABLE))) = tmp_ht
    '        Next
    '        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_CURRENCY_ANSWER, AddressOf SMSG_LIST_CURRENCY_ANSWER)
    '        server_response_flag = True
    '        RaiseEvent ObjectInitialized()
    '    Else
    '        server_response_flag = False
    '    End If

    'End Sub

#End Region


#Region "CRUD"

    Friend Sub CMSG_CREATE_CURRENCY(ByRef attributes As Hashtable)

        '   NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_CURRENCY_ANSWER, AddressOf SMSG_CREATE_CURRENCY_ANSWER)
        '   Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_CURRENCY, UShort))
        'WritecurrencyPacket(packet, attributes)
        'packet.Release()
        'NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_CURRENCY_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim tmp_ht As New Hashtable
            GetcurrencyHTFromPacket(packet, tmp_ht)
            currencies_hash(tmp_ht(ID_VARIABLE)) = tmp_ht
            'NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_CURRENCY_ANSWER, AddressOf SMSG_CREATE_CURRENCY_ANSWER)
            RaiseEvent currencyCreationEvent(tmp_ht)
        Else
            '
            '
        End If

    End Sub

    Friend Shared Sub CMSG_READ_CURRENCY(ByRef id As UInt32)

        '        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_CURRENCY_ANSWER, AddressOf SMSG_READ_CURRENCY_ANSWER)
        '       Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_CURRENCY, UShort))
        '   packet.Release()
        '  NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Shared Sub SMSG_READ_CURRENCY_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim ht As New Hashtable
            GetcurrencyHTFromPacket(packet, ht)
            '       NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_CURRENCY_ANSWER, AddressOf SMSG_READ_CURRENCY_ANSWER)
            ' Send the ht to the object which demanded it
        Else

        End If

    End Sub

    Friend Sub CMSG_UPDATE_CURRENCY(ByRef attributes As Hashtable)

        'NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_CURRENCY_ANSWER, AddressOf SMSG_UPDATE_CURRENCY_ANSWER)
        'Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_CURRENCY, UShort))
        'WritecurrencyPacket(packet, attributes)
        'packet.Release()
        'NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_CURRENCY_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim ht As New Hashtable
            GetcurrencyHTFromPacket(packet, ht)
            currencies_hash(ht(ID_VARIABLE)) = ht
            '      NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_CURRENCY_ANSWER, AddressOf SMSG_UPDATE_CURRENCY_ANSWER)
            RaiseEvent currencyUpdateEvent(ht)
        Else

        End If

    End Sub

    Friend Sub CMSG_DELETE_CURRENCY(ByRef id As UInt32)

        'NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_CURRENCY_ANSWER, AddressOf SMSG_DELETE_CURRENCY_ANSWER)
        'Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_CURRENCY, UShort))
        'packet.WriteUint32(id)
        'packet.Release()
        'NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_CURRENCY_ANSWER()

        ' add packet param in network manager !
        ' priority high !!!

        'If packet.ReadInt32() = 0 Then

        'Else
        'End If

        Dim id As UInt32  ' packet.ReadInt32
        '   NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_CURRENCY_ANSWER, AddressOf SMSG_DELETE_CURRENCY_ANSWER)
        RaiseEvent currencyDeleteEvent(id)

    End Sub

#End Region


#Region "Mappings"

    Friend Function GetCurrencyNameList() As List(Of String)

        Dim tmp_list As New List(Of String)

        For Each id In currencies_hash.Keys
            tmp_list.Add(currencies_hash(id)(NAME_VARIABLE))
        Next

        ' STUB !!!
        ' implement priority high !!!!!!!!!!!!!!!!!!!!
        tmp_list.Add("EUR")
        tmp_list.Add("USD")
        tmp_list.Add("GBP")
        Return tmp_list

    End Function

    Friend Function GetCurrenciesDict(ByRef Key As String, ByRef Value As String) As Hashtable

        Dim tmpHT As New Hashtable
        For Each id In currencies_hash.Keys
            tmpHT(currencies_hash(id)(Key)) = currencies_hash(id)(Value)
        Next
        Return tmpHT

    End Function


#End Region


#Region "Utilities"

    Friend Shared Sub GetcurrencyHTFromPacket(ByRef packet As ByteBuffer, ByRef currency_ht As Hashtable)

        currency_ht(ID_VARIABLE) = packet.ReadInt32()
        currency_ht(NAME_VARIABLE) = packet.ReadString()


    End Sub

    Private Sub WritecurrencyPacket(ByRef packet As ByteBuffer, ByRef attributes As Hashtable)

        If attributes.ContainsKey(ID_VARIABLE) Then packet.WriteInt32(attributes(ID_VARIABLE))
        packet.WriteString(attributes(NAME_VARIABLE))

    End Sub



#End Region



End Class
