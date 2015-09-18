Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports System.Collections
Imports System.Windows.Forms
Imports VIBlend.WinForms.Controls

Friend Class GroupController


#Region "Instance variables"

    ' Objects
    Private m_view As GroupsControl
    Private m_platformMGTUI As PlatformMGTGeneralUI
    Private m_groupList As Group
    Private PlatformMGTUI As PlatformMGTGeneralUI
    Private m_userController As UsersController

#End Region


#Region "Initialize"

    Friend Sub New()

        m_groupList = GlobalVariables.Groups
        m_view = New GroupsControl(Me)
        m_userController = New UsersController()
        m_view.AddUserControl(m_userController)

    End Sub


    Public Sub close()

        m_view.Dispose()

    End Sub

    Public Sub addControlToPanel(ByRef dest_panel As Panel, _
                             ByRef PlatformMGTUI As PlatformMGTGeneralUI)

        Me.PlatformMGTUI = PlatformMGTUI
        dest_panel.Controls.Add(m_view)
        m_view.Dock = Windows.Forms.DockStyle.Fill

    End Sub

#End Region


#Region "Interface"

    Friend Function GetGroupList() As Dictionary(Of Int32, Hashtable)
        Return (m_groupList.groupDic)
    End Function

    Friend Function GetGroupName(ByRef p_id As Int32) As String
        If Not m_groupList.groupDic.ContainsKey(p_id) Then Return ""
        Return m_groupList.groupDic(p_id)(NAME_VARIABLE)
    End Function

    Friend Function GetGroupTV() As vTreeView
        Dim treeView As New vTreeView
        m_groupList.LoadGroupTV(treeView)
        Return treeView
    End Function

    Friend Function GetGroupAllowedEntities(ByRef p_groupId As Int32) As List(Of Int32)
        Return GlobalVariables.GroupAllowedEntities.groupAllowedEntityDic(p_groupId)
    End Function

    Friend Function IsAllowedEntity(ByRef p_groupId As Int32, ByRef p_entityId As Int32) As Boolean
        Dim list = GetGroupAllowedEntities(p_groupId)

        If list Is Nothing Then Return False
        Return list.Contains(p_entityId)
    End Function

    Friend Sub AddAllowedEntity(ByRef p_groupId As Int32, ByRef p_entityId As Int32)
        GlobalVariables.GroupAllowedEntities.CMSG_ADD_GROUP_ENTITY(p_groupId, p_entityId)
    End Sub

    Friend Sub RemoveAllowedEntity(ByRef p_groupId As Int32, ByRef p_entityId As Int32)
        GlobalVariables.GroupAllowedEntities.CMSG_DEL_GROUP_ENTITY(p_groupId, p_entityId)
    End Sub

#End Region

End Class
