Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports System.Collections
Imports System.Windows.Forms
Imports VIBlend.WinForms.Controls
Imports CRUD

Friend Class GroupController


#Region "Instance variables"

    ' Objects
    Private m_view As GroupsControl
    Private m_platformMGTUI As PlatformMGTGeneralUI
    Private m_groupList As GroupManager
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

    Friend Function GetGroupList() As MultiIndexDictionary(Of UInt32, String, NamedHierarchyCRUDEntity)
        Return (m_groupList.GetDictionary())
    End Function

    Friend Function GetGroupName(ByRef p_id As UInt32) As String
        Return m_groupList.GetValueName(p_id)
    End Function

    Friend Function GetGroupTV() As vTreeView
        Dim treeView As New vTreeView
        m_groupList.LoadGroupTV(treeView)
        Return treeView
    End Function

    Friend Function GetGroupAllowedEntities(ByRef p_groupId As Int32) As MultiIndexDictionary(Of UInt32, UInt32, GroupAllowedEntity)
        Return GlobalVariables.GroupAllowedEntities.GetDictionary(p_groupId)
    End Function

    Friend Function IsAllowedEntity(ByRef p_groupId As Int32, ByRef p_entityId As Int32) As Boolean
        Dim list = GetGroupAllowedEntities(p_groupId)

        If list Is Nothing Then Return False
        Return list.ContainsSecondaryKey(p_entityId)
    End Function

    Friend Sub AddAllowedEntity(ByRef p_groupId As UInt32, ByRef p_entityId As UInt32)
        Dim allowedEntity As New GroupAllowedEntity

        allowedEntity.GroupId = p_groupId
        allowedEntity.EntityId = p_entityId
        GlobalVariables.GroupAllowedEntities.Create(allowedEntity)
    End Sub

    Friend Sub RemoveAllowedEntity(ByRef p_groupId As Int32, ByRef p_entityId As UInt32)
        Dim allowedEntity As GroupAllowedEntity = GlobalVariables.GroupAllowedEntities.GetValue(p_groupId, p_entityId)

        If allowedEntity Is Nothing Then Exit Sub
        GlobalVariables.GroupAllowedEntities.Delete(allowedEntity.Id)
    End Sub

#End Region

End Class
