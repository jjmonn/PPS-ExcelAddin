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
Imports CRUD

Friend Class CurrencyManager : Inherits NamedCRUDManager

#Region "Instance variables"

    ' Variables
    Private m_usedCurrencies As SortedSet(Of UInt32)
    Private m_mainCurrency As UInt32

    Public Event GetMainCurrencyEvent(ByRef status As ErrorMessage, ByRef id As UInt32)

#End Region

#Region "Init"

    Friend Sub New()

        CreateCMSG = ClientMessage.CMSG_CREATE_CURRENCY
        ReadCMSG = ClientMessage.CMSG_READ_CURRENCY
        UpdateCMSG = ClientMessage.CMSG_UPDATE_CURRENCY
        UpdateListCMSG = ClientMessage.CMSG_UPDATE_CURRENCY_LIST
        DeleteCMSG = ClientMessage.CMSG_DELETE_CURRENCY
        ListCMSG = ClientMessage.CMSG_LIST_CURRENCY

        CreateSMSG = ServerMessage.SMSG_CREATE_CURRENCY_ANSWER
        ReadSMSG = ServerMessage.SMSG_READ_CURRENCY_ANSWER
        UpdateSMSG = ServerMessage.SMSG_UPDATE_CURRENCY_ANSWER
        UpdateListSMSG = ServerMessage.SMSG_UPDATE_CURRENCY_LIST_ANSWER
        DeleteSMSG = ServerMessage.SMSG_DELETE_CURRENCY_ANSWER
        ListSMSG = ServerMessage.SMSG_LIST_CURRENCY_ANSWER

        Build = AddressOf Currency.BuildCurrency

        InitCallbacks()

        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_CURRENCY_ANSWER, AddressOf MyBase.ListAnswer)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_READ_CURRENCY_ANSWER, AddressOf MyBase.ReadAnswer)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_CURRENCY_ANSWER, AddressOf MyBase.DeleteAnswer)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_CURRENCY_ANSWER, AddressOf ListAnswer)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_CURRENCY_ANSWER, AddressOf ReadAnswer)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_CURRENCY_ANSWER, AddressOf DeleteAnswer)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_GET_MAIN_CURRENCY_ANSWER, AddressOf SMSG_GET_MAIN_CURRENCY_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_SET_MAIN_CURRENCY_ANSWER, AddressOf SMSG_SET_MAIN_CURRENCY_ANSWER)

    End Sub

    Protected Overrides Sub finalize()

        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_CURRENCY_ANSWER, AddressOf ListAnswer)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_READ_CURRENCY_ANSWER, AddressOf ReadAnswer)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_CURRENCY_ANSWER, AddressOf DeleteAnswer)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_GET_MAIN_CURRENCY_ANSWER, AddressOf SMSG_GET_MAIN_CURRENCY_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_SET_MAIN_CURRENCY_ANSWER, AddressOf SMSG_SET_MAIN_CURRENCY_ANSWER)

        MyBase.Finalize()

    End Sub

#End Region

#Region "CRUD"

    Protected Overrides Sub ListAnswer(packet As ByteBuffer)

        If packet.GetError() = ErrorMessage.SUCCESS Then
            MyBase.ListAnswer(packet)
            m_usedCurrencies.Clear()

            For Each l_currency As Currency In m_CRUDDic.Values
                If l_currency.InUse = True Then m_usedCurrencies.Add(l_currency.Id)
            Next
        End If

    End Sub

    Protected Overrides Sub ReadAnswer(packet As ByteBuffer)

        If packet.GetError() = ErrorMessage.SUCCESS Then
            Dim l_currency = Currency.BuildCurrency(packet)

            If (l_currency.InUse = True) Then
                If m_usedCurrencies.Contains(l_currency.Id) = False Then m_usedCurrencies.Add(l_currency.Id)
            Else
                If m_usedCurrencies.Contains(l_currency.Id) Then m_usedCurrencies.Remove(l_currency.Id)
            End If

            m_CRUDDic.Set(l_currency.Id, l_currency.Name, l_currency)
            RaiseReadEvent(packet.GetError(), l_currency)
        Else
            RaiseReadEvent(packet.GetError(), Nothing)
        End If

    End Sub

    Protected Overrides Sub DeleteAnswer(packet As ByteBuffer)

        If packet.GetError() = ErrorMessage.SUCCESS Then
            Dim id As UInt32 = packet.ReadInt32
            m_usedCurrencies.Remove(id)
            m_CRUDDic.Remove(id)
            RaiseDeleteEvent(packet.GetError(), id)
        Else
            RaiseDeleteEvent(packet.GetError(), 0)
        End If

    End Sub

    Private Sub SMSG_GET_MAIN_CURRENCY_ANSWER(packet As ByteBuffer)

        m_mainCurrency = packet.ReadUint32()
        RaiseEvent GetMainCurrencyEvent(packet.GetError(), m_mainCurrency)

    End Sub

    Private Sub SMSG_SET_MAIN_CURRENCY_ANSWER(packet As ByteBuffer)

        If packet.GetError() = ErrorMessage.SUCCESS Then
            RaiseEvent GetMainCurrencyEvent(packet.GetError(), 0)
        Else
            RaiseEvent GetMainCurrencyEvent(packet.GetError(), packet.ReadUint32())
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

    Friend Function GetMainCurrency() As UInt32
        Return m_mainCurrency
    End Function

    Friend Function GetCurrencyNameList() As List(Of String)

        Dim tmp_list As New List(Of String)
        For Each currency In m_CRUDDic.Values
            tmp_list.Add(currency.Name)
        Next
        Return tmp_list

    End Function

    Friend Function GetInUseCurrenciesIdList() As SortedSet(Of UInt32)

        Return m_usedCurrencies

    End Function

#End Region

End Class
