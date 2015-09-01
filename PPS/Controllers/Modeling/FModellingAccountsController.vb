' FModellingAccountsController.vb
'
' Outputs accounts format management
'
'
'
'
' Author: Julien Monnereau
' Last modified: 17/02/2015


Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports System.Collections


Friend Class FModellingAccountsController

#Region "Instance Variables"

    ' Objects
    Private FModellingAccounts As FModellingAccount
    Private View As FModelingAccountsMGTUI
 

    ' Variables
    Friend f_accounts_name_id_dic As Hashtable


#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef input_fmodeling_accounts_name_id_dict As Hashtable, _
                             Optional ByRef input_FModelingAccounts As FModellingAccount = Nothing)

        f_accounts_name_id_dic = input_fmodeling_accounts_name_id_dict
        If Not input_FModelingAccounts Is Nothing Then FModellingAccounts = input_FModelingAccounts Else FModellingAccounts = New FModellingAccount
        View = New FModelingAccountsMGTUI(Me, f_accounts_name_id_dic)
        InitializeDGVValues()
       
    End Sub

    Private Sub InitializeDGVValues()

        Dim f_accounts_dic As New Dictionary(Of String, Hashtable)
        For Each f_account_id As String In f_accounts_name_id_dic.Values
            f_accounts_dic.Add(f_account_id, FModellingAccounts.GetSeriHT(f_account_id))
        Next
        View.FillDGV(f_accounts_dic)

    End Sub

#End Region


#Region "Interface"

    Protected Friend Sub DisplayView()

        View.Visible = True
        View.Show()

    End Sub

    Protected Friend Sub UpdateSerieColor(ByRef f_account_id As String, _
                                          ByVal color_int As Int32)

        FModellingAccounts.UpdateFModellingAccount(f_account_id, FINANCIAL_MODELLING_SERIE_COLOR_VARIABLE, color_int)

    End Sub

    Protected Friend Sub UpdateSerieType(ByRef f_account_id As String, _
                                          ByVal serie_type As String)

        FModellingAccounts.UpdateFModellingAccount(f_account_id, FINANCIAL_MODELLING_SERIE_TYPE_VARIABLE, serie_type)

    End Sub

    Protected Friend Sub UpdateSerieChart(ByRef f_account_id As String, _
                                          ByVal serie_chart As String)

        Select Case serie_chart
            Case "Left Chart" : serie_chart = "ChartArea1"
            Case "Right Chart" : serie_chart = "ChartArea2"
        End Select

    End Sub


#End Region




End Class
