Imports System.Collections.Generic

' UploadHistoryUI.vb
'
' Display Last Upload Status and errors. Triggerred by clicking on light status in Submission Ribbon
'
' To do:
'       -
'       -
'
' Known bugs:
'       - 
'
'
' Authors: Julien Monnereau
' Last modified: 18/10/2014


Public Class UploadingHistoryUI


    Public Sub New(ByRef uploadState As Boolean, _
                   ByRef timeStamp As Date, _
                   ByRef errorsList As List(Of String))

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        TimeStampTB.Text = timeStamp
        If uploadState = True Then
            UploadStateTB.Text = "Successful"
            UploadStateTB.ForeColor = Drawing.Color.Green
            UploadStateBT.ImageIndex = 1
        Else
            UploadStateTB.Text = "Unsuccessful"
            UploadStateTB.ForeColor = Drawing.Color.Red
            UploadStateBT.ImageIndex = 0
            Dim errorStr As String = ""
            For Each error_ In errorsList
                errorStr = errorStr + error_ + Chr(13)
            Next
            ErrorMessageTB.Text = errorStr
        End If

    End Sub




End Class