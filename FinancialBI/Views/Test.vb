Imports System.Linq

Public Class Test


    Private Computer As New Computer


    Public Sub New()

        InitializeComponent()
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_TEST_ANSWER, AddressOf SMSG_TEST_ANSWER)
        AddHandler Computer.ComputationAnswered, AddressOf AfterComputation

    End Sub


    Private Sub ComputeBT_Click(sender As Object, e As EventArgs) Handles ComputeBT.Click

        Dim decompList As New Collections.Generic.List(Of String)
        decompList.Add("A2")
        Computer.CMSG_COMPUTE_REQUEST({5}, _
                                      {2}.ToList, _
                                      CRUD.Account.AccountProcess.FINANCIAL, _
                                      3, _
                                      , _
                                      , _
                                     decompList)

    End Sub


    Private Sub AfterComputation()

        Dim dataMap = Computer.GetData

    End Sub



#Region "Test"

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_TEST, UShort))
        packet.WriteString("Hello Nath !! I am a test string")
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_TEST_ANSWER(packet As ByteBuffer)

        MsgBox(452.8)
        MsgBox(packet.ReadDouble())

    End Sub


#End Region







    Private Sub VTreeView1_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles VTreeView1.KeyDown
        MsgBox("key down worked")
    End Sub


  
   
End Class