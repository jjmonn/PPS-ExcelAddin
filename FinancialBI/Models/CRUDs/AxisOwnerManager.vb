Imports System.Collections
Imports System.Collections.Generic
Imports CRUD

Class AxisOwnerManager : Inherits CRUDManager

#Region "Instance variables"

    ' Variables
    Private m_AxisOwnerDic As New SortedDictionary(Of UInt32, AxisOwner)
    Private request_id As Dictionary(Of UInt32, Boolean)

#End Region

#Region "Init"

    Friend Sub New()

        CreateCMSG = ClientMessage.CMSG_CREATE_AXIS_OWNER
        ReadCMSG = ClientMessage.CMSG_READ_AXIS_OWNER
        UpdateCMSG = ClientMessage.CMSG_UPDATE_AXIS_OWNER
        UpdateListCMSG = ClientMessage.CMSG_CRUD_AXIS_OWNER
        ListCMSG = ClientMessage.CMSG_LIST_AXIS_OWNER

        CreateSMSG = ServerMessage.SMSG_CREATE_AXIS_OWNER_ANSWER
        ReadSMSG = ServerMessage.SMSG_READ_AXIS_OWNER_ANSWER
        UpdateSMSG = ServerMessage.SMSG_UPDATE_AXIS_OWNER_ANSWER
        UpdateListSMSG = ServerMessage.SMSG_CRUD_AXIS_OWNER_LIST_ANSWER
        DeleteSMSG = ServerMessage.SMSG_DELETE_AXIS_OWNER_ANSWER
        ListSMSG = ServerMessage.SMSG_LIST_AXIS_OWNER_ANSWER

        Build = AddressOf AxisOwner.BuildAxisOwner

        InitCallbacks()

    End Sub

#End Region

#Region "CRUD"

#Region "Disabled"

    <Obsolete("Not implemented", True)> _
<System.ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)>
    Friend Overrides Sub Delete(ByRef p_id As UInt32)
    End Sub

#End Region

    Protected Overrides Sub ListAnswer(packet As ByteBuffer)

        If packet.GetError() = ErrorMessage.SUCCESS Then
            m_AxisOwnerDic.Clear()
            For i As Int32 = 1 To packet.ReadInt32()
                Dim tmp_axis = Build(packet)

                m_AxisOwnerDic.Add(tmp_axis.Id, tmp_axis)
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
            Dim tmp_axis = Build(packet)

            If m_AxisOwnerDic.ContainsKey(tmp_axis.Id) Then
                m_AxisOwnerDic(tmp_axis.Id) = tmp_axis
            Else
                m_AxisOwnerDic.Add(tmp_axis.Id, tmp_axis)
            End If
            RaiseReadEvent(packet.GetError(), tmp_axis)
        Else
            RaiseReadEvent(packet.GetError(), Nothing)
        End If

    End Sub

    Protected Overrides Sub DeleteAnswer(packet As ByteBuffer)
        Dim id As UInt32 = packet.ReadUint32()
        If packet.GetError() = ErrorMessage.SUCCESS Then
            m_AxisOwnerDic.Remove(id)
        End If
        RaiseDeleteEvent(packet.GetError(), id)
    End Sub

#End Region

#Region "Mapping"

    Public Overrides Function GetValue(ByVal p_id As UInt32) As CRUDEntity
        If m_AxisOwnerDic.ContainsKey(p_id) = False Then Return Nothing
        Return m_AxisOwnerDic(p_id)
    End Function

    Public Overrides Function GetValue(ByVal p_id As Int32) As CRUDEntity
        Return GetValue(CUInt(p_id))
    End Function

    Public Overloads Function GetValue(ByVal p_name As String) As CRUDEntity
        Dim axis As AxisElem = GlobalVariables.AxisElems.GetValue(AxisType.Entities, p_name)

        If axis Is Nothing Then Return Nothing
        Return GetValue(axis.Id)
    End Function

#End Region

End Class
