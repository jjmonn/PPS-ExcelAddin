Public Class ServerTest

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_TEST_ANSWER, AddressOf onSMSG_TEST_ANSWER)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_TEST, UShort))

        packet.WriteString("Salut c'est moi")
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Sub onSMSG_TEST_ANSWER(p_data As ByteBuffer)
        Dim answer As String = p_data.ReadString()
        MsgBox(answer)
    End Sub
End Class