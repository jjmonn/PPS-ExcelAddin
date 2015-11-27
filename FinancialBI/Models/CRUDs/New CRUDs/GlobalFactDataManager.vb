Imports System.Collections
Imports System.Collections.Generic
Imports System.Tuple
Imports CRUD

Friend Class GlobalFactDataManager : Inherits CRUDManager

#Region "Instance Variable"
    Private m_globalFactDic As New MultiIndexDictionary(Of UInt32, Tuple(Of UInt32, UInt32, UInt32), GlobalFactData)
#End Region

#Region "Init"

    Friend Sub New()

        CreateCMSG = ClientMessage.CMSG_CREATE_GLOBAL_FACT_DATA
        ReadCMSG = ClientMessage.CMSG_READ_GLOBAL_FACT_DATA
        UpdateCMSG = ClientMessage.CMSG_UPDATE_GLOBAL_FACT_DATA
        UpdateListCMSG = ClientMessage.CMSG_CRUD_GLOBAL_FACT_DATA_LIST
        DeleteCMSG = ClientMessage.CMSG_DELETE_GLOBAL_FACT_DATA
        ListCMSG = ClientMessage.CMSG_LIST_GLOBAL_FACT_DATA

        CreateSMSG = ServerMessage.SMSG_CREATE_GLOBAL_FACT_DATA_ANSWER
        ReadSMSG = ServerMessage.SMSG_READ_GLOBAL_FACT_DATA_ANSWER
        UpdateSMSG = ServerMessage.SMSG_UPDATE_GLOBAL_FACT_DATA_ANSWER
        UpdateListSMSG = ServerMessage.SMSG_CRUD_GLOBAL_FACT_DATA_LIST_ANSWER
        DeleteSMSG = ServerMessage.SMSG_DELETE_GLOBAL_FACT_DATA_ANSWER
        ListSMSG = ServerMessage.SMSG_LIST_GLOBAL_FACT_DATA_ANSWER

        Build = AddressOf GlobalFactData.BuildGlobalFact

        InitCallbacks()

    End Sub


#End Region

#Region "CRUD"

    Protected Overrides Sub ListAnswer(packet As ByteBuffer)

        If packet.GetError() = ErrorMessage.SUCCESS Then
            m_globalFactDic.Clear()
            For i As Int32 = 1 To packet.ReadInt32()
                Dim tmp_ht As GlobalFactData = Build(packet)

                m_globalFactDic.Set(tmp_ht.Id, New Tuple(Of UInt32, UInt32, UInt32)(tmp_ht.GlobalFactId, tmp_ht.Period, tmp_ht.VersionId), tmp_ht)
            Next
            state_flag = True
            RaiseObjectInitializedEvent()
        Else
            RaiseObjectInitializedEvent()
            state_flag = False
        End If

    End Sub

    Protected Overrides Sub ReadAnswer(packet As ByteBuffer)

        If packet.GetError() = ErrorMessage.SUCCESS Then
            Dim tmp_ht As GlobalFactData = Build(packet)

            m_globalFactDic.Set(tmp_ht.Id, New Tuple(Of UInt32, UInt32, UInt32)(tmp_ht.GlobalFactId, tmp_ht.Period, tmp_ht.VersionId), tmp_ht)
            RaiseReadEvent(packet.GetError(), tmp_ht)
        Else
            RaiseReadEvent(packet.GetError(), Nothing)
        End If

    End Sub

    Protected Overrides Sub DeleteAnswer(packet As ByteBuffer)

        Dim Id As UInt32 = packet.ReadUint32()
        If packet.GetError() = ErrorMessage.SUCCESS Then
            m_globalFactDic.Remove(Id)
        End If
        RaiseDeleteEvent(packet.GetError(), Id)
    End Sub

#End Region

#Region "Mapping"

    Public Overrides Function GetValue(ByVal p_id As UInt32) As CRUDEntity
        Return m_globalFactDic(p_id)
    End Function

    Public Overrides Function GetValue(ByVal p_id As Int32) As CRUDEntity
        Return m_globalFactDic(CUInt(p_id))
    End Function

    Public Overloads Function GetValue(ByVal p_globalFactId As UInt32, ByVal p_period As UInt32, ByVal p_rateVersionId As UInt32) As GlobalFactData
        Return m_globalFactDic(New Tuple(Of UInt32, UInt32, UInt32)(p_globalFactId, p_period, p_rateVersionId))
    End Function

    Public Function GetDictionary() As MultiIndexDictionary(Of UInt32, Tuple(Of UInt32, UInt32, UInt32), GlobalFactData)
        Return m_globalFactDic
    End Function

#End Region

End Class
