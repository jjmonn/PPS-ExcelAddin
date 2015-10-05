Public Class CUI2VisualizationContainer

    Private Sub CUI2VisualizationContainer_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        e.Cancel = True
        Me.Hide()

    End Sub
End Class