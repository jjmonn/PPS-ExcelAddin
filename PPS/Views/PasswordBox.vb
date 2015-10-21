Public Class PasswordBox

    Public Shared m_returnValue As String = ""

    Public Shared Function Open(ByRef p_message As String, Optional ByRef p_title As String = "") As String
        Dim window = New PasswordBox()

        window.Text = p_title
        window.DescTB.Text = p_message
        window.ShowDialog()
        Dim returnValue = m_returnValue
        m_returnValue = ""
        Return (returnValue)

    End Function

    Private Sub AcceptBT_Click(sender As Object, e As EventArgs) Handles AcceptBT.Click
        m_returnValue = inputBox.Text
        Close()
    End Sub

    Private Sub CancelBT_Click(sender As Object, e As EventArgs) Handles CancelBT.Click
        m_returnValue = ""
        Close()
    End Sub
End Class