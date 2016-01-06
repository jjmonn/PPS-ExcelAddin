Imports System.Collections
Imports System.Collections.Generic
Imports CRUD

Class AxisParentManager : Inherits CRUDManager

#Region "Instance variables"

    ' Variables
    Private m_axisParentDic As New SortedDictionary(Of UInt32, AxisParent)
    Private request_id As Dictionary(Of UInt32, Boolean)

#End Region

#Region "Init"

    Friend Sub New()

        CreateCMSG = ClientMessage.CMSG_CREATE_AXIS_PARENT
        ReadCMSG = ClientMessage.CMSG_READ_AXIS_PARENT
        UpdateCMSG = ClientMessage.CMSG_UPDATE_AXIS_PARENT
        UpdateListCMSG = ClientMessage.CMSG_CRUD_AXIS_PARENT
        ListCMSG = ClientMessage.CMSG_LIST_AXIS_PARENT

        CreateSMSG = ServerMessage.SMSG_CREATE_AXIS_PARENT_ANSWER
        ReadSMSG = ServerMessage.SMSG_READ_AXIS_PARENT_ANSWER
        UpdateSMSG = ServerMessage.SMSG_UPDATE_AXIS_PARENT_ANSWER
        UpdateListSMSG = ServerMessage.SMSG_CRUD_AXIS_PARENT_LIST_ANSWER
        DeleteSMSG = ServerMessage.SMSG_DELETE_AXIS_PARENT_ANSWER
        ListSMSG = ServerMessage.SMSG_LIST_AXIS_PARENT_ANSWER

        Build = AddressOf AxisParent.BuildAxisParent

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
            m_axisParentDic.Clear()
            For i As Int32 = 1 To packet.ReadInt32()
                Dim tmp_axis = Build(packet)

                m_axisParentDic.Add(tmp_axis.Id, tmp_axis)
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

            If m_axisParentDic.ContainsKey(tmp_axis.Id) Then
                m_axisParentDic(tmp_axis.Id) = tmp_axis
            Else
                m_axisParentDic.Add(tmp_axis.Id, tmp_axis)
            End If
            RaiseReadEvent(packet.GetError(), tmp_axis)
        Else
            RaiseReadEvent(packet.GetError(), Nothing)
        End If

    End Sub

    Protected Overrides Sub DeleteAnswer(packet As ByteBuffer)
        Dim id As UInt32 = packet.ReadUint32()
        If packet.GetError() = ErrorMessage.SUCCESS Then
            m_axisParentDic.Remove(id)
        End If
        RaiseDeleteEvent(packet.GetError(), id)
    End Sub

#End Region

#Region "Mapping"

    Public Overrides Function GetValue(ByVal p_id As UInt32) As CRUDEntity
        If m_axisParentDic.ContainsKey(p_id) = False Then Return Nothing
        Return m_axisParentDic(p_id)
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
