'
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


Friend Class NewControlUI


#Region "Instance Variables"

    ' Objects
    Private Controller As ControlsController

    ' Variables
    Private accounts_name_id_dic As Hashtable
    Private operators_symbol_id_dic As Dictionary(Of String, String)
    Private period_options_name_id_dic As Dictionary(Of String, String)



#End Region


#Region "Initialization"

    Friend Sub New(ByRef input_controller As ControlsController, _
                   ByRef input_accounts_name_id_dic As Hashtable, _
                   ByRef input_operators_symbol_id_dic As Dictionary(Of String, String), _
                   ByRef input_period_options_name_id_dic As Dictionary(Of String, String))

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Controller = input_controller
        accounts_name_id_dic = input_accounts_name_id_dic
        operators_symbol_id_dic = input_operators_symbol_id_dic
        period_options_name_id_dic = input_period_options_name_id_dic

        InitializeComboBoxes()

    End Sub

    Private Sub InitializeComboBoxes()

        For Each account_name In accounts_name_id_dic.Keys
            Item1CB.Items.Add(account_name)
            Item2CB.Items.Add(account_name)
        Next

        For Each operator_symbol In operators_symbol_id_dic.Keys
            OperatorCB.Items.Add(operator_symbol)
        Next

        For Each period_option_name In period_options_name_id_dic.Keys
            PeriodOptionCB.Items.Add(period_option_name)
        Next

    End Sub


#End Region


#Region "Call Backs"

    Private Sub ValidateBT_Click(sender As Object, e As EventArgs) Handles ValidateBT.Click

        If IsFormValid() Then
            Dim HT As New Hashtable
            HT.Add(CONTROL_NAME_VARIABLE, NameTB.Text)

            ' Item 1
            If IsNumeric(Item1CB.Text) Then
                HT.Add(CONTROL_ITEM1_VARIABLE, Item1CB.Text)
            Else
                HT.Add(CONTROL_ITEM1_VARIABLE, accounts_name_id_dic(Item1CB.SelectedItem))
            End If

            ' Item 2
            If IsNumeric(Item2CB.Text) Then
                HT.Add(CONTROL_ITEM2_VARIABLE, Item2CB.Text)
            Else
                HT.Add(CONTROL_ITEM2_VARIABLE, accounts_name_id_dic(Item2CB.SelectedItem))
            End If

            HT.Add(CONTROL_OPERATOR_ID_VARIABLE, operators_symbol_id_dic(OperatorCB.SelectedItem))
            HT.Add(CONTROL_PERIOD_OPTION_VARIABLE, period_options_name_id_dic(PeriodOptionCB.SelectedItem))
            Controller.CreateControl(HT)
            Controller.DisplayMGTUI()
        End If

    End Sub

    Private Sub CancelBT_Click(sender As Object, e As EventArgs) Handles CancelBT.Click

        Controller.DisplayMGTUI()

    End Sub

#End Region


#Region "Utilities"

    Private Function IsFormValid() As Boolean

        If Controller.IsNameValid(Name) Then
            If Not IsNumeric(Item1CB.Text) AndAlso Item1CB.SelectedItem = "" Then
                MsgBox("Item 1 is empty.")
                Return False
            End If
            If Not IsNumeric(Item2CB.Text) AndAlso Item2CB.SelectedItem = "" Then
                MsgBox("Item 2 is empty.")
                Return False
            End If
            If OperatorCB.SelectedItem = "" Then
                MsgBox("Operator is empty.")
                Return False
            End If
            If PeriodOptionCB.SelectedItem = "" Then
                MsgBox("Option is empty.")
                Return False
            End If
            Return True
        Else
            MsgBox("This Control name is already in use. Please choose another name.")
            Return False
        End If

    End Function

#End Region


#Region "Events"

    Private Sub NewControlUI_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        e.Cancel = True
        Controller.DisplayMGTUI()

    End Sub

#End Region



End Class