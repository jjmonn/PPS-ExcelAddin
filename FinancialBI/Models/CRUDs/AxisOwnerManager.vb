Imports System.Collections
Imports System.Collections.Generic
Imports CRUD

Class AxisOwnerManager : Inherits SimpleCRUDManager

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

#End Region

#Region "Mapping"

    Public Overloads Function GetValue(ByVal p_name As String) As CRUDEntity
        Dim axis As AxisElem = GlobalVariables.AxisElems.GetValue(AxisType.Entities, p_name)

        If axis Is Nothing Then Return Nothing
        Return GetValue(axis.Id)
    End Function

#End Region

End Class
