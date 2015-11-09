﻿' NewAccountUI.vb
'
' Form for filling new account details. Also manages actions
' 
' To do:
'       - 
'       -
'
'
' Known bugs
'       - 
'       - 
'
'
' Last modified: 09/09/2015
' Author: Julien Monnereau


Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic
Imports CRUD


Friend Class NewAccountUI


#Region "Instance Variables"

    ' Objects
    Private AccountsView As AccountsView
    Private Controller As AccountsController
    Private ParentAccountsTreeviewBox As VIBlend.WinForms.Controls.vTreeViewBox

    ' Variables
    Private isFormExpanded As Boolean
    Friend parentNodeId As Int32

    ' Constants
    Private Const COLLAPSED_WIDTH As Int32 = 720
    Private Const EXPANDED_WIDTH As Int32 = 1100
    Private Const COLLAPSED_HEIGHT As Int32 = 430
    Private Const EXPANDED_HEIGHT As Int32 = 480


#End Region


#Region "Initialize"

    Friend Sub New(ByRef input_accountsView As AccountsView, _
                   ByRef input_controller As AccountsController)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AccountsView = input_accountsView
        Controller = input_controller
        ComboBoxesInitialize()
        MultilanguageSetup()

    End Sub

    Private Sub ComboBoxesInitialize()

        For Each item In AccountsView.TypeComboBox.Items
            TypeComboBox.Items.Add(item)
        Next

        For Each item In AccountsView.FormulaTypeComboBox.Items
            FormulaComboBox.Items.Add(item)
        Next


    End Sub

    Private Sub NewAccountUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ParentAccountsTreeviewBox = New VIBlend.WinForms.Controls.vTreeViewBox
        ParentTVPanel.Controls.Add(ParentAccountsTreeviewBox)
        ParentAccountsTreeviewBox.Dock = DockStyle.Fill
        GlobalVariables.Accounts.LoadAccountsTV(ParentAccountsTreeviewBox.TreeView)
        '   VTreeViewUtil.LoadParentsTreeviewBox(ParentAccountsTreeviewBox, accountsTV)
        On Error Resume Next
        Dim parentNode = VTreeViewUtil.FindNode(ParentAccountsTreeviewBox.TreeView, parentNodeId)
        If Not parentNode Is Nothing Then
            ParentAccountsTreeviewBox.TreeView.SelectedNode = parentNode
        End If

    End Sub

    Private Sub MultilanguageSetup()

        Me.CancelBT.Text = Local.GetValue("general.cancel")
        Me.CreateAccountBT.Text = Local.GetValue("general.create")
        Me.m_accountNameLabel.Text = Local.GetValue("accounts_edition.account_name")
        Me.m_accountParentLabel.Text = Local.GetValue("accounts_edition.account_parent")
        Me.m_formulaTypeLabel.Text = Local.GetValue("accounts_edition.formula_type")
        Me.m_formatLabel.Text = Local.GetValue("accounts_edition.account_format")
        Me.recompute_RB.Text = Local.GetValue("accounts_edition.recomputation")
        Me.aggregation_RB.Text = Local.GetValue("accounts_edition.aggregation")
        Me.m_consolidationOptionLabel.Text = Local.GetValue("accounts_edition.consolidation_option")
        Me.bs_item_RB.Text = Local.GetValue("accounts_edition.end_of_period_rate")
        Me.flux_RB.Text = Local.GetValue("accounts_edition.average_rate")
        Me.m_conversionOptionLabel.Text = Local.GetValue("accounts_edition.currencies_conversion")
        Me.Text = Local.GetValue("accounts_edition.title_new_account")

    End Sub


#End Region


#Region "Call backs"

    Private Sub CreateAccountBT_Click(sender As Object, e As EventArgs) Handles CreateAccountBT.Click

        If IsFormValid() = True Then
            Dim parent_id As Int32 = 0
            Dim conso_option As Int32 = 0
            Dim conversion_option As Int32 = 0
            Dim account_tab As Int32

            If Not ParentAccountsTreeviewBox.TreeView.SelectedNode Is Nothing Then
                parent_id = CInt(ParentAccountsTreeviewBox.TreeView.SelectedNode.Value)

                Dim l_account As Account = GlobalVariables.Accounts.GetValue(parent_id)
                account_tab = If(l_account Is Nothing, 0, l_account.AccountTab)
            Else
                parent_id = 0
                account_tab = ParentAccountsTreeviewBox.TreeView.Nodes.Count
            End If
            If aggregation_RB.Checked = True Then conso_option = Account.ConsolidationOptions.AGGREGATION
            If recompute_RB.Checked = True Then conso_option = Account.ConsolidationOptions.RECOMPUTATION
            If flux_RB.Checked = True Then conversion_option = Account.ConversionOptions.AVERAGE_RATE
            If bs_item_RB.Checked = True Then conversion_option = Account.ConversionOptions.END_OF_PERIOD_RATE

            Controller.CreateAccount(parent_id, _
                                     NameTextBox.Text, _
                                     FormulaComboBox.SelectedItem.Value, _
                                     "", _
                                     TypeComboBox.SelectedItem.Value, _
                                     conso_option, _
                                     conversion_option, _
                                     "n", _
                                     FormulaComboBox.SelectedItem.Value, _
                                     1, _
                                     account_tab)
            Controller.DisplayAccountsView()
        End If

    End Sub

    Private Sub CancelBT_Click(sender As Object, e As EventArgs) Handles CancelBT.Click

        Controller.DisplayAccountsView()

    End Sub

#End Region


#Region "Checks"

    Private Function IsFormValid() As Boolean

        If Controller.AccountNameCheck(NameTextBox.Text) = False Then
            MsgBox(Local.GetValue("accounts_edition.msg_name_empty"))
            Return False
        End If

        If ComboBoxesSelectionCheck() = False Then
            MsgBox(Local.GetValue("accounts_edition.msg_fill_values"))
            Return False
        End If

        If Controller.IsUsedName(NameTextBox.Text) Then
            MsgBox(Local.GetValue("accounts_edition.msg_name_already_used"))
            Return False
        End If
        Return True

    End Function

    Private Function ComboBoxesSelectionCheck() As Boolean

        If TypeComboBox.SelectedItem Is Nothing Then Return False
        If FormulaComboBox.SelectedItem Is Nothing Then Return False
        Return True

    End Function

#End Region


    Private Sub NewAccountUI_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        e.Cancel = True
        Controller.DisplayAccountsView()

    End Sub




End Class