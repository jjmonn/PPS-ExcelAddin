' NewMarketPricesVersionUI.vb
'
'
'
'
' Author: Julien Monnereau
' Last modified: 05/02/2015


Imports System.Windows.Forms


Friend Class NewMarketPricesVersionUI


#Region "Instance Variables"

    Private Controller As MarketPricesController
    Protected Friend parent_node As TreeNode = Nothing

#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef input_controller As MarketPricesController)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Controller = input_controller
        StartPeriodNUD.Value = Year(Now)

    End Sub

#End Region


#Region "Call Backs"

    Private Sub ValidateBT_Click(sender As Object, e As EventArgs) Handles ValidateBT.Click

        Dim name As String = NameTB.Text
        If Len(name) < MARKET_INDEXES_VERSIONS_TOKEN_SIZE Then
            Controller.CreateVersion(name, 0, _
                                     StartPeriodNUD.Value, _
                                     NBPeriodsNUD.Value, _
                                     parent_node)
            Me.Hide()
        Else
            MsgBox("The Name cannot exceed " & MARKET_INDEXES_VERSIONS_TOKEN_SIZE & " characters")
        End If

    End Sub

    Private Sub CancelBT_Click(sender As Object, e As EventArgs) Handles CancelBT.Click

        Me.Hide()

    End Sub


#End Region


#Region "Events"


    Private Sub NewPricesVersionUI_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        e.Cancel = True
        Me.Hide()

    End Sub


#End Region




End Class