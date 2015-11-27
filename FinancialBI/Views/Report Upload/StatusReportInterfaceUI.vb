Imports System.Collections.Generic

Class StatusReportInterfaceUI

    Private Sub StatusReportInterfaceUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub StatusReportInterfaceUI_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        Me.Visible = False
    End Sub

    Private Function GetErrorMessage(ByRef p_error As ErrorMessage) As String
        Select Case p_error
            Case ErrorMessage.PERMISSION_DENIED
                Return "You are not allowed to edit this version."
            Case Else
                Return "Error system: please contact a FinancialBI administrator if this problem persist."
        End Select
    End Function

    Public Sub AddError(ByRef p_error As String)
        m_errorsListBox.Items.Add(p_error)
    End Sub

    Public Sub ClearBox()
        m_errorsListBox.Items.Clear()
    End Sub

    Public Delegate Sub AfterCommit_Delegate(ByRef p_dataSet As ModelDataSet, ByRef commitResults As Dictionary(Of String, ErrorMessage))
    Public Sub AfterCommit(ByRef p_dataSet As ModelDataSet, ByRef p_commitResults As Dictionary(Of String, ErrorMessage))
        If InvokeRequired Then
            Dim MyDelegate As New AfterCommit_Delegate(AddressOf AfterCommit)
            Me.Invoke(MyDelegate, New Object() {p_dataSet, p_commitResults})
        Else
            For Each result In p_commitResults
                If p_commitResults(result.Key) <> ErrorMessage.SUCCESS Then
                    m_errorsListBox.Items.Add(DateTime.Now().Hour & ":" & DateTime.Now().Minute & ": " & GetErrorMessage(result.Value))
                End If
            Next
        End If
    End Sub
End Class