' currency.vb
'
' CRUD for currencies table - relation with c++ server
'
'   change list method => be able to take all currencies or only in_use !! priority high
'
'
' Author: Julien Monnereau
' Created: 03/08/2015
' Last modified: 02/09/2015


Imports System.Collections
Imports System.Collections.Generic



Friend Class Currency

#Region "Instance variables"


    ' Variables
    Friend state_flag As Boolean
    Friend currencies_hash As New Hashtable
    Friend mainCurrency As UInt32

    ' Events
    Public Event ObjectInitialized()
    Public Event Read(ByRef status As Boolean, ByRef attributes As Hashtable)
    Public Event CreationEvent(ByRef status As Boolean, ByRef id As Int32)
    Public Event UpdateEvent(ByRef status As Boolean, ByRef id As Int32)
    Public Event DeleteEvent(ByRef status As Boolean, ByRef id As UInt32)
    Public Event GetMainCurrency(ByRef status As Boolean, ByRef id As UInt32)


#End Region


#Region "Init"

    Friend Sub New()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_CURRENCY_ANSWER, AddressOf SMSG_LIST_CURRENCY_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_CURRENCY_ANSWER, AddressOf SMSG_READ_CURRENCY_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_CURRENCY_ANSWER, AddressOf SMSG_DELETE_CURRENCY_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_GET_MAIN_CURRENCY_ANSWER, AddressOf SMSG_GET_MAIN_CURRENCY_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_SET_MAIN_CURRENCY_ANSWER, AddressOf SMSG_SET_MAIN_CURRENCY_ANSWER)

        state_flag = False

    End Sub

#End Region


#Region "CRUD"


    Private Sub SMSG_LIST_CURRENCY_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim nb_currencies = packet.ReadInt32()
            For i As Int32 = 1 To nb_currencies
                Dim tmp_ht As New Hashtable
                GetcurrencyHTFromPacket(packet, tmp_ht)
                If tmp_ht(CURRENCY_IN_USE_VARIABLE) = True Then currencies_hash(CInt(tmp_ht(ID_VARIABLE))) = tmp_ht
            Next
            state_flag = True
            RaiseEvent ObjectInitialized()
        Else
            state_flag = False
        End If

    End Sub

    Friend Sub CMSG_CREATE_CURRENCY(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_CURRENCY_ANSWER, AddressOf SMSG_CREATE_CURRENCY_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_CURRENCY, UShort))
        WritecurrencyPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_CURRENCY_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            RaiseEvent CreationEvent(True, packet.ReadUint32())
        Else
            RaiseEvent CreationEvent(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_CURRENCY_ANSWER, AddressOf SMSG_CREATE_CURRENCY_ANSWER)

    End Sub

    Friend Sub CMSG_READ_CURRENCY(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_READ_CURRENCY, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_READ_CURRENCY_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim ht As New Hashtable
            GetcurrencyHTFromPacket(packet, ht)
            currencies_hash(CInt(ht(ID_VARIABLE))) = ht
            RaiseEvent Read(True, ht)
        Else
            RaiseEvent Read(False, Nothing)
        End If

    End Sub

    Friend Sub CMSG_UPDATE_CURRENCY(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_CURRENCY_ANSWER, AddressOf SMSG_UPDATE_CURRENCY_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_CURRENCY, UShort))
        WritecurrencyPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_CURRENCY_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            RaiseEvent UpdateEvent(True, packet.ReadUint32())
        Else
            RaiseEvent UpdateEvent(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_CURRENCY_ANSWER, AddressOf SMSG_UPDATE_CURRENCY_ANSWER)

    End Sub

    Friend Sub CMSG_DELETE_CURRENCY(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_CURRENCY, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_CURRENCY_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim id As UInt32 = packet.ReadInt32
            currencies_hash.Remove(CInt(id))
            RaiseEvent DeleteEvent(True, id)
        Else
            RaiseEvent DeleteEvent(False, 0)
        End If

    End Sub

    Private Sub SMSG_GET_MAIN_CURRENCY_ANSWER(packet As ByteBuffer)
        packet.ReadInt32()

        mainCurrency = packet.ReadUint32()
        RaiseEvent GetMainCurrency(True, mainCurrency)
    End Sub

    Private Sub SMSG_SET_MAIN_CURRENCY_ANSWER(packet As ByteBuffer)
        If packet.GetError() = 0 Then
            RaiseEvent GetMainCurrency(False, 0)
        Else
            RaiseEvent GetMainCurrency(True, packet.ReadUint32())
        End If
    End Sub

    Friend Sub CMSG_SET_MAIN_CURRENCY(ByRef id As UInt32)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_GET_MAIN_CURRENCY, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)
    End Sub
#End Region


#Region "Mappings"

    Friend Function GetCurrencyNameList() As List(Of String)

        Dim tmp_list As New List(Of String)
        For Each id In currencies_hash.Keys
            tmp_list.Add(currencies_hash(id)(NAME_VARIABLE))
        Next
        Return tmp_list

    End Function

    Friend Function GetCurrencyId(ByVal name As String) As Int32
        For Each id In currencies_hash.Keys
            If (currencies_hash(id)(NAME_VARIABLE) = name) Then
                Return (id)
            End If
        Next
        Return 0
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

        currency_ht(ID_VARIABLE) = packet.ReadUint32()
        currency_ht(NAME_VARIABLE) = packet.ReadString()
        currency_ht(CURRENCY_SYMBOL_VARIABLE) = packet.ReadString()
        currency_ht(CURRENCY_IN_USE_VARIABLE) = packet.ReadBool()

    End Sub

    Private Sub WritecurrencyPacket(ByRef packet As ByteBuffer, ByRef attributes As Hashtable)

        If attributes.ContainsKey(ID_VARIABLE) Then packet.WriteInt32(attributes(ID_VARIABLE))
        packet.WriteString(attributes(NAME_VARIABLE))
        packet.WriteString(attributes(CURRENCY_SYMBOL_VARIABLE))
        packet.WriteUint8(attributes(CURRENCY_IN_USE_VARIABLE))

    End Sub



#End Region


    Protected Overrides Sub finalize()

        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_CURRENCY_ANSWER, AddressOf SMSG_LIST_CURRENCY_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_READ_CURRENCY_ANSWER, AddressOf SMSG_READ_CURRENCY_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_CURRENCY_ANSWER, AddressOf SMSG_DELETE_CURRENCY_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_GET_MAIN_CURRENCY_ANSWER, AddressOf SMSG_GET_MAIN_CURRENCY_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_SET_MAIN_CURRENCY_ANSWER, AddressOf SMSG_SET_MAIN_CURRENCY_ANSWER)

        MyBase.Finalize()

    End Sub



End Class
