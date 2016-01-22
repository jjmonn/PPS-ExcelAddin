Imports System.Collections
Imports System.Collections.Generic
Imports CRUD

Class FactTagManager : Inherits SimpleCRUDManager

#Region "Init"

    Friend Sub New()

        CreateCMSG = ClientMessage.CMSG_CREATE_FACT_TAG
        ReadCMSG = ClientMessage.CMSG_READ_FACT_TAG
        UpdateCMSG = ClientMessage.CMSG_UPDATE_FACT_TAG
        UpdateListCMSG = ClientMessage.CMSG_CRUD_FACT_TAG
        ListCMSG = ClientMessage.CMSG_LIST_FACT_TAG

        CreateSMSG = ServerMessage.SMSG_CREATE_FACT_TAG_ANSWER
        ReadSMSG = ServerMessage.SMSG_READ_FACT_TAG_ANSWER
        UpdateSMSG = ServerMessage.SMSG_UPDATE_FACT_TAG_ANSWER
        UpdateListSMSG = ServerMessage.SMSG_CRUD_FACT_TAG_LIST_ANSWER
        DeleteSMSG = ServerMessage.SMSG_DELETE_FACT_TAG_ANSWER
        ListSMSG = ServerMessage.SMSG_LIST_FACT_TAG_ANSWER

        Build = AddressOf FactTag.BuildFactTag

        InitCallbacks()

    End Sub

#End Region

End Class
