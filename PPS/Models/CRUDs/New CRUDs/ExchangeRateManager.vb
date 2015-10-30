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
Imports CRUD

Friend Class ExchangeRateManager : Inherits CRUDManager

#Region "Instance variables"

    ' Variables
    Private m_exchangeRatesDic As New MultiIndexDictionary(Of UInt32, Tuple(Of UInt32, UInt32, UInt32), ExchangeRate)
    Private request_id As Dictionary(Of UInt32, Boolean)

#End Region

#Region "Init"

    Friend Sub New()

        CreateCMSG = ClientMessage.CMSG_CREATE_EXCHANGE_RATE
        ReadCMSG = ClientMessage.CMSG_READ_EXCHANGE_RATE
        UpdateCMSG = ClientMessage.CMSG_UPDATE_EXCHANGE_RATE
        UpdateListCMSG = ClientMessage.CMSG_UPDATE_EXCHANGE_RATE_LIST
        DeleteCMSG = ClientMessage.CMSG_DELETE_EXCHANGE_RATE
        ListCMSG = ClientMessage.CMSG_LIST_EXCHANGE_RATE

        CreateSMSG = ServerMessage.SMSG_CREATE_EXCHANGE_RATE_ANSWER
        ReadSMSG = ServerMessage.SMSG_READ_EXCHANGE_RATE_ANSWER
        UpdateSMSG = ServerMessage.SMSG_UPDATE_EXCHANGE_RATE_ANSWER
        UpdateListSMSG = ServerMessage.SMSG_CRUD_EXCHANGE_RATE_LIST_ANSWER
        DeleteSMSG = ServerMessage.SMSG_DELETE_EXCHANGE_RATE_ANSWER
        ListSMSG = ServerMessage.SMSG_LIST_EXCHANGE_RATE_ANSWER

        Build = AddressOf ExchangeRate.BuildExchangeRate

        InitCallbacks()

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_LIST_EXCHANGE_RATE, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

#End Region

#Region "CRUD"

    Protected Overrides Sub ListAnswer(packet As ByteBuffer)

        If packet.GetError() = ErrorMessage.SUCCESS Then
            m_exchangeRatesDic.Clear()
            For i As Int32 = 1 To packet.ReadInt32()
                Dim tmp_rate = ExchangeRate.BuildExchangeRate(packet)

                m_exchangeRatesDic.Set(tmp_rate.Id, New Tuple(Of UInt32, UInt32, UInt32)(tmp_rate.DestCurrencyId, tmp_rate.RateVersionId, tmp_rate.Period), tmp_rate)
            Next
            state_flag = True
            RaiseObjectInitializedEvent()
        Else
            state_flag = False
            RaiseObjectInitializedEvent()
        End If

    End Sub

    ' add read => must listen in case somebody is already editing
    ' or block any CRUD editing when someone is already editing ?!
    Protected Overrides Sub ReadAnswer(packet As ByteBuffer)

        If packet.GetError() = ErrorMessage.SUCCESS Then
            Dim tmp_rate = ExchangeRate.BuildExchangeRate(packet)

            m_exchangeRatesDic.Set(tmp_rate.Id, New Tuple(Of UInt32, UInt32, UInt32)(tmp_rate.DestCurrencyId, tmp_rate.RateVersionId, tmp_rate.Period), tmp_rate)
            RaiseReadEvent(packet.GetError(), tmp_rate)
        Else
            RaiseReadEvent(packet.GetError(), Nothing)
        End If

    End Sub

    Protected Overrides Sub DeleteAnswer(packet As ByteBuffer)
        Dim id As UInt32 = packet.ReadUint32()
        If packet.GetError() = ErrorMessage.SUCCESS Then
            m_exchangeRatesDic.Remove(id)
        End If
        RaiseDeleteEvent(packet.GetError(), id)
    End Sub

#End Region

#Region "Mapping"

    Public Overloads Function GetValue(ByVal p_currencyId As UInt32, ByVal p_rateVersionId As UInt32, ByVal p_period As UInt32) As ExchangeRate
        Return m_exchangeRatesDic(New Tuple(Of UInt32, UInt32, UInt32)(p_currencyId, p_rateVersionId, p_period))
    End Function

    Public Overrides Function GetValue(ByVal p_id As UInt32) As CRUDEntity
        Return m_exchangeRatesDic(p_id)
    End Function

    Public Overrides Function GetValue(ByVal p_id As Int32) As CRUDEntity
        Return m_exchangeRatesDic(p_id)
    End Function

#End Region

End Class
