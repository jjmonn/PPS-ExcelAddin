' UsersControl.vb
'
'
'
'
' Author: Julien Monnereau
' Last modified: 25/06/2015


Imports System.Windows.Forms


Friend Class UsersControl


#Region "Instance variables"

    ' Objects
    Friend Controller As UsersController
    Friend UsersTGVMGT As UsersTGV
    Friend EntitySelectionUI As EntitySelectionForUsersMGT


#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef Controller As UsersController)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Controller = Controller
        usersTGV.ImageList = TGVIcons
        UsersTGVMGT = New UsersTGV(usersTGV, Me)
        UsersTGVMGT.SetController(Controller)
        EntitySelectionUI = New EntitySelectionForUsersMGT(UsersTGVMGT)

    End Sub

#End Region


#Region "Call backs"

    'Private Sub AddFolderBT_Click(sender As Object, e As EventArgs) Handles addFolderBT.Click

    '    If Not UsersTGVMGT.currentRowItem Is Nothing Then
    '        If UsersTGVMGT.currentRowItem.ImageIndex = 1 Then
    '            Dim new_user_id = PromptUser_id()
    '            Controller.CreateFolder(new_user_id, UsersTGVMGT.currentRowItem)
    '        Else
    '            MsgBox("A Folder cannot be created under a user.")
    '        End If
    '    Else
    '        Dim new_user_id = PromptUser_id()
    '        Controller.CreateFolder(new_user_id)
    '    End If

    'End Sub

    'Private Sub AddUser_Click(sender As Object, e As EventArgs) Handles addUserBT.Click

    '    Me.Hide()
    '    Controller.ShowNewUserUI()

    'End Sub

    'Private Sub DeleteBT_Click(sender As Object, e As EventArgs) Handles DeleteBT.Click

    '    If Not UsersTGVMGT.currentRowItem Is Nothing Then

    '        If UsersTGVMGT.currentRowItem.ImageIndex = 0 Then
    '            Dim confirm As Integer = MessageBox.Show("Careful, you are about to delete user " + Chr(13) + Chr(13) + _
    '                                                     UsersTGVMGT.currentRowItem.Caption + Chr(13) + Chr(13) + _
    '                                                     "Do you confirm?" + Chr(13) + Chr(13), _
    '                                                     "User deletion confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    '            If confirm = DialogResult.Yes Then
    '                Controller.DeleteUser(UsersTGVMGT.currentRowItem.Caption)
    '            End If
    '        Else
    '            Dim confirm As Integer = MessageBox.Show("Careful, you are about to delete folder " + Chr(13) + Chr(13) + _
    '                                                      UsersTGVMGT.currentRowItem.Caption + Chr(13) + Chr(13) + _
    '                                                      "Deleting this folder will delete all sub folders and users" + Chr(13) + Chr(13) + _
    '                                                      "Do you confirm?" + Chr(13) + Chr(13), _
    '                                                      "Folder deletion confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    '            If confirm = DialogResult.Yes Then
    '                Controller.DeleteFolder(UsersTGVMGT.currentRowItem.Caption)
    '                UsersTGVMGT.currentRowItem = Nothing
    '            End If
    '        End If
    '    End If

    'End Sub

    'Private Sub ReinitPwdBT_Click(sender As Object, e As EventArgs) Handles ReinitPwdBT.Click

    '    If Not UsersTGVMGT.currentRowItem Is Nothing AndAlso UsersTGVMGT.currentRowItem.ImageIndex = 0 Then
    '        Controller.ReiniatilizePassword(UsersTGVMGT.currentRowItem.Caption)
    '    Else
    '        MsgBox("A user must be selected in order to reinitialize the password")
    '    End If

    'End Sub

    'Private Sub ExitBT_Click(sender As Object, e As EventArgs) Handles ExitBT.Click

    '    Me.Dispose()

    'End Sub


#End Region


#Region "Right Click Menu"

    'Private Sub CreateUserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateUserToolStripMenuItem.Click

    '    AddUser_Click(sender, e)

    'End Sub

    'Private Sub CreateFolderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateFolderToolStripMenuItem.Click

    '    AddFolderBT_Click(sender, e)

    'End Sub

    'Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click

    '    DeleteBT_Click(sender, e)

    'End Sub

    'Private Sub ReinitializePasswordToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReinitializePasswordToolStripMenuItem.Click

    '    ReinitPwdBT_Click(sender, e)

    'End Sub


#End Region


#Region "Utilities"

    Friend Sub ShowEntitiesSelection()

        EntitySelectionUI.Show()
        Me.Cursor = New Cursor(Cursor.Current.Handle)
        Dim temp As Drawing.Point = MousePosition
        temp.X = temp.X
        temp.Y = temp.Y
        EntitySelectionUI.Location = temp

    End Sub

    Friend Sub HideEntitySelection()

        EntitySelectionUI.Hide()

    End Sub

    Private Function PromptUser_id() As String

        Dim new_user_id As String = InputBox("Enter the new folder name")
        If Controller.IsUSerIDAlreadyInUse(new_user_id) = True Then
            MsgBox("This name already exists, please enter another name")
            Return ""
        ElseIf Len(new_user_id) > USERS_ID_MAX_SIZE Then
            MsgBox("The User cannot exceed " & USERS_ID_MAX_SIZE & " characters.")
            Return ""
        End If
        Return new_user_id

    End Function

#End Region




End Class
