Public Class Test

    Public Sub New()
        InitializeComponent()
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_TEST_ANSWER, AddressOf SMSG_TEST_ANSWER)
    End Sub

    Private Sub SMSG_TEST_ANSWER(packet As ByteBuffer)
        MsgBox(packet.ReadString())
    End Sub

    Private Sub CMSG_TEST()
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_TEST, UShort))
        Dim test_array() As Int32 = {2, 6, 5, 3}

        packet.WriteInt32(test_array.Length)
        For Each value In test_array
            packet.WriteInt32(value)
        Next
        packet.Release()

        NetworkManager.GetInstance().Send(packet)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub Test_Leave(sender As Object, e As EventArgs) Handles MyBase.Leave
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_TEST_ANSWER, AddressOf CMSG_TEST)
    End Sub

End Class