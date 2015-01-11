' SMTPConnection.vb
'
' Establish an SMTP connection 
'
'
'
' to do:
'       - Connection Test DOESN'T WORK ! implement a real test
'       - MAIL_USER_ID: should be modifiable in advanced settings (only for db managers)  !!!
'
'
' knwo bugs:
'       -
'
'
' Last modified: 05/12/2014
' Author: Julien Monnereau


Imports System.Net.Mail


Public Class SMTPConnection


#Region "Instance Variables"

    ' Objects
    Friend SMTPSERVER As SmtpClient
 
    ' Variables
    Private pwd As String
    Private admin_mail As String

    ' Constants
    Private Const EXTRA_DATA_MAIL_ADMIN As String = "ppsAdmin"
 

#End Region


#Region "Initialize"

    Public Sub New()

        SMTPSERVER = New SmtpClient
        admin_mail = ExtraDataMapping.GetValue(EXTRA_DATA_MAIL_ADMIN)
        pwd = InputBox("Please enter the admin password: ")
        If pwd <> "" Then SetUpSMTPConnectionAttributes()

    End Sub

#End Region


#Region "Connections functions"

    ' Set up connection 
    Private Sub SetUpSMTPConnectionAttributes()

        SMTPSERVER.UseDefaultCredentials = False
        SMTPSERVER.Credentials = New Net.NetworkCredential(admin_mail, pwd)
        SMTPSERVER.Port = 587
        SMTPSERVER.EnableSsl = True
        SMTPSERVER.Host = "send.one.com"

    End Sub

    ' Test the connection
    Protected Friend Function testConnection() As Boolean

        Try
            Dim eMail As New MailMessage()
            eMail = New MailMessage()
            eMail.From = New MailAddress(admin_mail)
            eMail.To.Add("test@purplesunsolutions.com")
            'eMail.Subject = "test"
            eMail.IsBodyHtml = False
            eMail.Body = "Test"
            SMTPSERVER.Send(eMail)
            Return True
        Catch ex As Exception
            Return False
        End Try


    End Function

    ' Send an email to the param recipient with param password
    Protected Friend Sub SendPassword(ByRef password As String, ByRef recipient As String, ByRef userID As String)

        If pwd <> "" Then
            Try
                Dim eMail As New MailMessage()
                eMail = New MailMessage()
                eMail.From = New MailAddress(admin_mail)
                eMail.To.Add(recipient)
                eMail.Subject = "PPS Connection"
                eMail.IsBodyHtml = False
                eMail.Body = "Hi, your password has been reinitialized by the Data Base Manager" + Chr(13) + _
                             "Your new password is: '" + password + "'." + Chr(13) + _
                             "User ID: '" + userID + "'" + Chr(13) + Chr(13) + _
                             "Regards" + Chr(13) + _
                             "PPS Team."
                SMTPSERVER.Send(eMail)
                MsgBox("Password Sent")

            Catch error_t As Exception
                MsgBox(error_t.ToString)
            End Try
        Else
            MsgBox("The password could not be sent to the user beacause the admin password has not been entered. " & _
                   Chr(13) & "You need to contact the PPS team to resolve this problem or you can reinitialize the user's password.")
        End If

    End Sub


#End Region




End Class
