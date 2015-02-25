' ChangePasswordUI.vb
'
' Allows to reinit current user's password
'
'
' to do: 
'       - limit to 3 trials max
'
'
' known bugs: 
'
'
' Last modified: 07/07/2014
' Author: Julien Monnereau





Public Class ChangePasswordUI


    Private PRIVMGT As New SQLPrivileges



    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        AddHandler Panel1.Paint, AddressOf Panel1_Paint
        AddHandler Panel1.MouseMove, AddressOf panel1_MouseMove
        AddHandler Panel1.MouseDown, AddressOf form_MouseDown
        AddHandler Panel1.MouseUp, AddressOf form_MouseUp

    End Sub



#Region "Call Backs"

    ' Reset password call back
    Private Sub ResetPWDBT_Click(sender As Object, e As EventArgs) Handles ResetPWDBT.Click

        Dim testConnection As ADODB.Connection = OpenConnection(GlobalVariables.Current_User_ID, CurrentPwdTB.Text)
        If Not testConnection Is Nothing Then
            testConnection.Close()
            testConnection = Nothing
        Else
            MsgBox("The current password is not valid")
            Exit Sub
        End If

        If NewPwd1.Text = NewPwd2.Text Then
            Dim pwd As String = NewPwd1.Text
            pwd = pwd + SNOW_KEY
            PRIVMGT.ChangeCurrentUserPwd(pwd)
            MsgBox("The password has been changed")
        Else
            MsgBox("The two values for the new password do not correspond")
        End If


    End Sub



    ' Exit
    Private Sub CloseBT_Click(sender As Object, e As EventArgs) Handles CloseBT.Click
        Me.Dispose()
        Me.Close()
    End Sub





#End Region




    
End Class