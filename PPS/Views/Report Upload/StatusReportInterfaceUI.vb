Public Class StatusReportInterfaceUI

    Private Sub StatusReportInterfaceUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub StatusReportInterfaceUI_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        Me.Visible = False
    End Sub
End Class