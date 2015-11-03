Imports System.Collections
Imports System.Collections.Generic
Imports CRUD

Public Class EntityCurrencyManager : Inherits CRUDManager

#Region "Instance variables"

    ' Variables
    Private m_entityCurrencyDic As New SortedDictionary(Of UInt32, EntityCurrency)
    Private request_id As Dictionary(Of UInt32, Boolean)

#End Region

#Region "Init"

    Friend Sub New()

        ReadCMSG = ClientMessage.CMSG_READ_ENTITY_CURRENCY
        UpdateCMSG = ClientMessage.CMSG_UPDATE_ENTITY_CURRENCY
        UpdateListCMSG = ClientMessage.CMSG_CRUD_ENTITY_CURRENCY
        ListCMSG = ClientMessage.CMSG_LIST_ENTITY_CURRENCY

        ReadSMSG = ServerMessage.SMSG_READ_ENTITY_CURRENCY_ANSWER
        UpdateSMSG = ServerMessage.SMSG_UPDATE_ENTITY_CURRENCY_ANSWER
        UpdateListSMSG = ServerMessage.SMSG_CRUD_ENTITY_CURRENCY_LIST_ANSWER
        DeleteSMSG = ServerMessage.SMSG_DELETE_ENTITY_CURRENCY_ANSWER
        ListSMSG = ServerMessage.SMSG_LIST_ENTITY_CURRENCY_ANSWER

        Build = AddressOf EntityCurrency.BuildEntityCurrency

        InitCallbacks()

    End Sub

#End Region

#Region "CRUD"

#Region "Disabled"

    <Obsolete("Not implemented", True)> _
<System.ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)>
    Friend Overrides Sub Delete(ByRef p_id As UInt32)
    End Sub

    <Obsolete("Not implemented", True)> _
    <System.ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)>
    Friend Overrides Sub Create(ByRef p_crud As CRUDEntity)
    End Sub

#End Region

    Protected Overrides Sub ListAnswer(packet As ByteBuffer)

        If packet.GetError() = ErrorMessage.SUCCESS Then
            m_entityCurrencyDic.Clear()
            For i As Int32 = 1 To packet.ReadInt32()
                Dim tmp_entity = Build(packet)

                m_entityCurrencyDic.Add(tmp_entity.Id, tmp_entity)
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
            Dim tmp_entity = Build(packet)

            If m_entityCurrencyDic.ContainsKey(tmp_entity.Id) Then
                m_entityCurrencyDic(tmp_entity.Id) = tmp_entity
            Else
                m_entityCurrencyDic.Add(tmp_entity.Id, tmp_entity)
            End If
            RaiseReadEvent(packet.GetError(), tmp_entity)
        Else
            RaiseReadEvent(packet.GetError(), Nothing)
        End If

    End Sub

    Protected Overrides Sub DeleteAnswer(packet As ByteBuffer)
        Dim id As UInt32 = packet.ReadUint32()
        If packet.GetError() = ErrorMessage.SUCCESS Then
            m_entityCurrencyDic.Remove(id)
        End If
        RaiseDeleteEvent(packet.GetError(), id)
    End Sub

#End Region

#Region "Mapping"

    Public Overrides Function GetValue(ByVal p_id As UInt32) As CRUDEntity
        If m_entityCurrencyDic.ContainsKey(p_id) = False Then Return Nothing
        Return m_entityCurrencyDic(p_id)
    End Function

    Public Overrides Function GetValue(ByVal p_id As Int32) As CRUDEntity
        Return GetValue(CUInt(p_id))
    End Function

    Public Overloads Function GetValue(ByVal p_name As String) As CRUDEntity
        Dim entity As AxisElem = GlobalVariables.AxisElems.GetValue(AxisType.Entities, p_name)

        If entity Is Nothing Then Return Nothing
        Return GetValue(entity.Id)
    End Function

#End Region

End Class
