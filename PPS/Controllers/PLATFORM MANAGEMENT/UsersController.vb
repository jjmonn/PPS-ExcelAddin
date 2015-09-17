' UsersController.vb
'
' 
' To do: 
'       - if change pwd = success then send pwd else try again ?
'   
'
' Known bugs: 
'       - issues with cmd. -> execute sql queries -> find a way more secure or manage potential errors...
'
'
' Author: Julien Monnereau
' Last modified: 25/06/2015


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

#End Region


#Region "Initialize"

    Friend Sub New()

        m_userList = GlobalVariables.Users
        m_groupList = GlobalVariables.Groups
        m_view = New UsersControl(Me)

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

    Friend Function GetUserList() As Dictionary(Of Int32, Hashtable)
        Return m_userList.userDic
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
