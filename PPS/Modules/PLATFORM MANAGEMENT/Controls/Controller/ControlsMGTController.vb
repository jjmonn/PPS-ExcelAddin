'
'
'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 06/01/2015


Imports System.Collections
Imports System.Collections.Generic


Friend Class ControlsMGTController

#Region "Instance Variables"

    ' Objects
    Private Model As New Control
    Private View As ControlsMGTUI
    Private NewControlUI As NewControlUI
    Private ChartsController As New ChartsControlsMGTController

    ' Variables
    Private accounts_name_id_dic As Hashtable
    Private operators_symbol_id_dic As Dictionary(Of String, String)
    Private period_options_name_id_dic As Dictionary(Of String, String)
    Private controls_names_list As New List(Of String)
   
    ' Const

#End Region


#Region "Initialize"

    Protected Friend Sub New()

        accounts_name_id_dic = AccountsMapping.GetAccountsDictionary(ACCOUNT_NAME_VARIABLE, ACCOUNT_ID_VARIABLE)
        operators_symbol_id_dic = OperatorsMapping.GetOperatorsDictionary(OPERATOR_SYMBOL_VARIABLE, OPERATOR_ID_VARIABLE)
        period_options_name_id_dic = ControlOptionsMapping.GetControlOptionsDictionary(CONTROL_OPTION_NAME_VARIABLE, CONTROL_OPTION_ID_VARIABLE)

        View = New ControlsMGTUI(Me, ChartsController, accounts_name_id_dic, operators_symbol_id_dic, period_options_name_id_dic)
        NewControlUI = New NewControlUI(Me, accounts_name_id_dic, operators_symbol_id_dic, period_options_name_id_dic)

        InitializeView()
        ChartsController.InitializeDisplay(View)
        View.Show()

    End Sub

    Private Sub InitializeView()

        Dim controls_dic = Model.ReadAll
        For Each control_id In controls_dic.Keys
            View.AddRow(controls_dic(control_id))
            controls_names_list.Add(controls_dic(control_id)(CONTROL_NAME_VARIABLE))
        Next

    End Sub

#End Region


#Region "Interface"

    Protected Friend Sub CreateControl(ByRef HT As Hashtable)

        Dim id = GetNewID()
        HT.Add(CONTROL_ID_VARIABLE, id)
        Model.CreateControl(HT)
        controls_names_list.Add(HT(CONTROL_NAME_VARIABLE))
        View.AddRow(HT)
        View.DGV.Refresh()
        View.DGV.Select()

    End Sub

    Protected Friend Sub UpdateName(ByRef control_id As String, ByRef name As String)

        Model.UpdateControl(control_id, CONTROL_NAME_VARIABLE, name)

    End Sub

    Protected Friend Sub UpdateItem1(ByRef control_id As String, ByRef account_id As String)

        Model.UpdateControl(control_id, CONTROL_ITEM1_VARIABLE, account_id)

    End Sub

    Protected Friend Sub UpdateItem2(ByRef control_id As String, ByRef account_id As String)

        Model.UpdateControl(control_id, CONTROL_ITEM2_VARIABLE, account_id)

    End Sub

    Protected Friend Sub UpdateOperator(ByRef control_id As String, ByRef operator_id As String)

        Model.UpdateControl(control_id, CONTROL_OPERATOR_ID_VARIABLE, operator_id)

    End Sub

    Protected Friend Sub UpdateOption(ByRef control_id As String, ByRef option_id As String)

        Model.UpdateControl(control_id, CONTROL_PERIOD_OPTION_VARIABLE, option_id)

    End Sub

    Protected Friend Sub DeleteControl(ByRef control_id)

        Model.DeleteControl(control_id)

    End Sub

#End Region


#Region "Utilities"

    Protected Friend Function IsNameValid(ByRef name As String) As Boolean

        If controls_names_list.Contains(name) Then Return False
        Return True

    End Function

    Private Function GetNewID() As String

        Dim new_id As String = cTreeViews_Functions.IssueNewToken(CONTROLS_TOKEN_SIZE)
        While Not Model.ReadControl(new_id, CONTROL_NAME_VARIABLE) Is Nothing
            new_id = cTreeViews_Functions.IssueNewToken(CONTROLS_TOKEN_SIZE)
        End While
        Return new_id

    End Function

#Region "Display Utilities"

    Protected Friend Sub DisplayMGTUI()

        NewControlUI.Hide()
        View.Show()

    End Sub

    Protected Friend Sub DisplayNewUI()

        View.Hide()
        NewControlUI.Show()

    End Sub

#End Region

#End Region


End Class
