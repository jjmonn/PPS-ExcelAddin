Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq
Imports CRUD

' Entity2.vb
'
' CRUD for entities table - relation with c++ server
'
'
'
'
' Author: Julien Monnereau
' Created: 21/07/2015
' Last modified: 23/09/2015

Friend Class EntityManager : Inherits NamedCRUDManager

#Region "Instance variables"

    Private request_id As Dictionary(Of UInt32, Boolean)

#End Region

#Region "Init"

    Friend Sub New()


        CreateCMSG = ClientMessage.CMSG_CREATE_ENTITY
        ReadCMSG = ClientMessage.CMSG_READ_ENTITY
        UpdateCMSG = ClientMessage.CMSG_UPDATE_ENTITY
        UpdateListCMSG = ClientMessage.CMSG_UPDATE_ENTITY_LIST
        DeleteCMSG = ClientMessage.CMSG_DELETE_ENTITY
        ListCMSG = ClientMessage.CMSG_LIST_ENTITY

        CreateSMSG = ServerMessage.SMSG_CREATE_ENTITY_ANSWER
        ReadSMSG = ServerMessage.SMSG_READ_ENTITY_ANSWER
        UpdateSMSG = ServerMessage.SMSG_UPDATE_ENTITY_ANSWER
        UpdateListSMSG = ServerMessage.SMSG_UPDATE_ENTITY_LIST_ANSWER
        DeleteSMSG = ServerMessage.SMSG_DELETE_ENTITY_ANSWER
        ListSMSG = ServerMessage.SMSG_LIST_ENTITY_ANSWER

        Build = AddressOf Entity.BuildEntity

        InitCallbacks()

    End Sub

#End Region

#Region "Utilities"

    Friend Sub LoadEntitiesTV(ByRef TV As Windows.Forms.TreeView)

        TreeViewsUtilities.LoadTreeview(TV, m_CRUDDic)

    End Sub

    Friend Sub LoadEntitiesTV(ByRef TV As VIBlend.WinForms.Controls.vTreeView)

        VTreeViewUtil.LoadTreeview(TV, m_CRUDDic)

    End Sub

    Friend Sub LoadEntitiesTV(ByRef TV As Windows.Forms.TreeView, _
                            ByRef nodes_icon_dic As Dictionary(Of UInt32, Int32))

        Dim tmp_ht As New Dictionary(Of Int32, Entity)
        For Each id As UInt32 In nodes_icon_dic.Keys
            Dim l_entity As Entity = GetValue(id)
            If l_entity Is Nothing Then Continue For
            l_entity = l_entity.Clone()

            l_entity.Image = nodes_icon_dic(id)
            tmp_ht(l_entity.Id) = l_entity
        Next
        TreeViewsUtilities.LoadTreeview(TV, tmp_ht)

    End Sub

    Friend Sub LoadEntitiesTVWithFilters(ByRef TV As Windows.Forms.TreeView, _
                                         ByRef axisFilteredValues As List(Of UInt32))


        Dim tmp_ht As New Hashtable
        For Each id As UInt32 In m_CRUDDic.Keys
            If axisFilteredValues.Contains(id) Then
                tmp_ht(id) = m_CRUDDic(id)
            End If
        Next
        TreeViewsUtilities.LoadTreeview(TV, tmp_ht)

    End Sub


#End Region

End Class
