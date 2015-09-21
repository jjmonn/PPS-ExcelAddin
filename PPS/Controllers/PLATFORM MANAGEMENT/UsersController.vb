Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports System.Collections
Imports System.Windows.Forms

Friend Class UsersController


#Region "Instance variables"

    ' Objects
    Private m_view As UsersControl
    Private m_userDGV As New vDataGridView
    Private m_platformMGTUI As PlatformMGTGeneralUI
    Private m_userList As User
    Private m_groupList As Group
    Private PlatformMGTUI As PlatformMGTGeneralUI
    Private m_filterGroup As UInt32
    Private m_isFiltered As Boolean

#End Region


#Region "Initialize"

    Friend Sub New()

        m_filterGroup = 0
        m_isFiltered = False
        m_userList = GlobalVariables.Users
        m_groupList = GlobalVariables.Groups
        m_view = New UsersControl(Me)

    End Sub

    Public Sub close()

        m_view.Dispose()

    End Sub

    Friend Sub ApplyFilter(ByRef p_filterGroup As Int32)
        m_filterGroup = p_filterGroup
        m_isFiltered = True
        m_view.RowsInitialize()
        m_view.Refresh()
    End Sub

    Friend Sub RemoveFilter()
        m_isFiltered = False
        m_view.RowsInitialize()
    End Sub

    Friend Function GetView() As UserControl
        Return m_view
    End Function

    Public Sub addControlToPanel(ByRef dest_panel As Panel, _
                             ByRef PlatformMGTUI As PlatformMGTGeneralUI)

        Me.PlatformMGTUI = PlatformMGTUI
        dest_panel.Controls.Add(m_view)
        m_view.Dock = Windows.Forms.DockStyle.Fill

    End Sub

#End Region


#Region "Interface"

    Friend Function GetUserList() As Dictionary(Of Int32, Hashtable)
        If m_isFiltered = False Then Return m_userList.userDic
        Dim filteredDic As New Dictionary(Of Int32, Hashtable)
        For Each user In m_userList.userDic
            If user.Value(GROUP_ID_VARIABLE) = m_filterGroup Then
                filteredDic.Add(user.Value(ID_VARIABLE), user.Value)
            End If
        Next
        Return filteredDic
    End Function

    Friend Function GetGroupList() As Dictionary(Of Int32, Hashtable)
        Return (m_groupList.groupDic)
    End Function

    Friend Function GetGroupName(ByRef p_id As Int32) As String
        If Not m_groupList.groupDic.ContainsKey(p_id) Then Return ""
        Return m_groupList.groupDic(p_id)(NAME_VARIABLE)
    End Function

    Friend Sub SetUserGroup(ByRef p_userId As Int32, ByRef p_groupId As Int32)
        Dim tmpUser As Hashtable = m_userList.userDic(p_userId).Clone()
        tmpUser(GROUP_ID_VARIABLE) = p_groupId
        m_userList.CMSG_UPDATE_USER(tmpUser)
    End Sub

#End Region


#Region "Utilities"

    Friend Function IsUserNameAlreadyInUse(ByRef userName As String) As Boolean

        If m_userList.GetIdFromName(userName) <> 0 Then Return True Else Return False

    End Function

    Private Sub ReCreateAllViewsFromCurrentUser()

        ' class to be deleted => on server !!!!! priority normal

        MsgBox("users mgt to be implemented on server")
        Dim entities_credentials_tv As New TreeView
        '  Entity.LoadEntitiesCredentialTree(entities_credentials_tv)
        'ViewsController.CreateAllViews(entities_credentials_tv)

    End Sub

#End Region

End Class
