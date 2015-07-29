Public Class Test

    Public Sub New()

        InitializeComponent()
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_TEST_ANSWER, AddressOf SMSG_TEST_ANSWER)
        '    NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_ACCOUNT_ANSWER, AddressOf SMSG_LIST_ACCOUNT_ANSWER)

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim Accounts As New Account

    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        '  CMSG_TEST()
        LoadAccountsTable()

    End Sub

    Private Sub SMSG_TEST_ANSWER(packet As ByteBuffer)
        MsgBox(packet.ReadString())

    End Sub

    Private Sub CMSG_TEST()
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_TEST, UShort))

        packet.WriteString("Hell Nath")
        packet.Release()

        NetworkManager.GetInstance().Send(packet)
    End Sub

    Private Sub Test_Leave(sender As Object, e As EventArgs) Handles MyBase.Leave

        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_TEST_ANSWER, AddressOf CMSG_TEST)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_ACCOUNT_ANSWER, AddressOf SMSG_LIST_ACCOUNT_ANSWER)

    End Sub


    Friend Sub LoadAccountsTable()

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_LIST_ACCOUNT, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_LIST_ACCOUNT_ANSWER(packet As ByteBuffer)

        For i As Int32 = 0 To packet.ReadInt32()
            '    Dim tmp_ht As New Hashtable
            '   GetAccountHTFromPacket(packet, tmp_ht)
            '  accounts_hash(tmp_ht(ID_VARIABLE)) = tmp_ht
        Next
  
    End Sub


End Class