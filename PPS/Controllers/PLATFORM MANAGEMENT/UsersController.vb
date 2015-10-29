Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports System.Collections
Imports System.Windows.Forms
Imports CRUD

Friend Class UsersController


#Region "Instance variables"

    ' Objects
    Private m_view As UsersControl
    Private m_userDGV As New vDataGridView
    Private m_platformMGTUI As PlatformMGTGeneralUI
    Private m_userList As UserManager
    Private m_groupList As GroupManager
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

    Friend Function GetUserList() As MultiIndexDictionary(Of UInt32, String, NamedCRUDEntity)
        If m_isFiltered = False Then Return m_userList.GetDictionary()
        Dim filteredDic As New MultiIndexDictionary(Of UInt32, String, NamedCRUDEntity)
        For Each user As User In m_userList.GetDictionary().Values
            If user.GroupId = m_filterGroup Then
                filteredDic.Set(user.Id, user.Name, user)
            End If
        Next
        Return filteredDic
    End Function

    Friend Function GetGroupList() As MultiIndexDictionary(Of UInt32, String, NamedCRUDEntity)
        Return (m_groupList.GetDictionary())
    End Function

    Friend Function GetGroupName(ByRef p_id As UInt32) As String
        Return m_groupList.GetValueName(p_id)
    End Function

    Friend Sub SetUserGroup(ByRef p_userId As UInt32, ByRef p_groupId As UInt32)
        Dim tmpUser As User = m_userList.GetValue(p_userId)
        If tmpUser Is Nothing Then Exit Sub

        tmpUser = tmpUser.Clone()
        tmpUser.GroupId = p_groupId
        m_userList.Update(tmpUser)
    End Sub

#End Region


#Region "Utilities"

    Friend Function IsUserNameAlreadyInUse(ByRef userName As String) As Boolean

        If m_userList.GetValueId(userName) <> 0 Then Return True Else Return False

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
