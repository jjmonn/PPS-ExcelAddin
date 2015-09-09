' NewAccountUI.vb
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
' Last modified: 02/09/2015
' Author: Julien Monnereau


Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic


Friend Class NewAccountUI


#Region "Instance Variables"

    ' Objects
    Private AccountsView As AccountsControl
    Private Controller As AccountsController
    Private accountsTV As New TreeView

    ' Variables
    Private isFormExpanded As Boolean

    ' Constants
    Private Const COLLAPSED_WIDTH As Int32 = 720
    Private Const EXPANDED_WIDTH As Int32 = 1100
    Private Const COLLAPSED_HEIGHT As Int32 = 430
    Private Const EXPANDED_HEIGHT As Int32 = 480


#End Region


#Region "Initialize"

    Friend Sub New(ByRef input_accountsView As AccountsControl, _
                   ByRef input_controller As AccountsController, _
                   ByRef p_accountsTV As TreeView)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AccountsView = input_accountsView
        Controller = input_controller
        accountsTV = p_accountsTV
        ComboBoxesInitialize()

    End Sub

    Private Sub ComboBoxesInitialize()

        For Each item In AccountsView.FormatComboBox.Items
            TypeCB.Items.Add(item)
        Next

        For Each item In AccountsView.FormulaTypeComboBox.Items
            formulaCB.Items.Add(item)
        Next


    End Sub

    Private Sub NewEntityUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        VTreeViewUtil.LoadParentEntitiesTreeviewBox(ParentAccountTreeComboBox, accountsTV)

    End Sub

#End Region


#Region "Interface"

    Friend Sub PrePopulateForm(ByRef inputNode As TreeNode)

        Dim parentNode As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.FindNode(ParentAccountTreeComboBox.TreeView, inputNode.Name)
        If Not parentNode Is Nothing Then
            ParentAccountTreeComboBox.TreeView.SelectedNode = parentNode
        End If
        'Dim parentFormatCode = Controller.ReadAccount(parent_node.Name, ACCOUNT_FORMAT_VARIABLE)
        'formatCB.SelectedItem = AccountsView.formatKeyNameDictionary(parentFormatCode)
        'typeCB.SelectedItem = AccountsView.accountsTypesKeyNameDict(DEFAULT_ACCOUNT_TYPE_VALUE)
        'Dim parentFType = Controller.ReadAccount(parent_node.Name, ACCOUNT_FORMULA_TYPE_VARIABLE)
        'formulaCB.SelectedItem = AccountsView.fTypeCodeNameDictionary(parentFType)

    End Sub


#End Region


#Region "Call backs"

  
    Private Sub CreateAccountBT_Click(sender As Object, e As EventArgs) Handles CreateAccountBT.Click

        If IsFormValid() = True Then
            Dim parent_id As Int32 = 0
            Dim conso_option As Int32 = 0
            Dim conversion_option As Int32 = 0
            Dim account_tab As Int32

            If Not ParentAccountTreeComboBox.TreeView.SelectedNode Is Nothing Then
                parent_id = CInt(ParentAccountTreeComboBox.TreeView.SelectedNode.Value)
                account_tab = GlobalVariables.Accounts.accounts_hash(parent_id)(ACCOUNT_TAB_VARIABLE)
            Else
                parent_id = 0
                account_tab = accountsTV.Nodes.Count
            End If
            If aggregation_RB.Checked = True Then conso_option = GlobalEnums.ConsolidationOptions.AGGREGATION
            If recompute_RB.Checked = True Then conso_option = GlobalEnums.ConsolidationOptions.RECOMPUTATION
            If flux_RB.Checked = True Then conversion_option = GlobalEnums.ConversionOptions.AVERAGE_RATE
            If bs_item_RB.Checked = True Then conversion_option = GlobalEnums.ConversionOptions.END_OF_PERIOD_RATE

            Controller.CreateAccount(parent_id, _
                                     nameTB.Text, _
                                     formulaCB.SelectedItem.value, _
                                     "", _
                                     TypeCB.SelectedItem.value, _
                                     conso_option, _
                                     conversion_option, _
                                     "n", _
                                     formulaCB.SelectedItem.value, _
                                     1, _
                                     account_tab)   
            Controller.DisplayAcountsView()
        End If

    End Sub

    Private Sub CancelBT_Click(sender As Object, e As EventArgs) Handles CancelBT.Click

        Controller.DisplayAcountsView()

    End Sub

#End Region


#Region "Checks"

    Private Function IsFormValid() As Boolean

        If Controller.AccountNameCheck(nameTB.Text) = False Then
            MsgBox("The Account Name must not be empty.")
            Return False
        End If

        If ComboBoxesSelectionCheck() = False Then
            MsgBox("Account's Format and Formula type must be populated.")
            Return False
        End If

        Return True

    End Function

    Private Function ComboBoxesSelectionCheck() As Boolean

        If TypeCB.SelectedItem Is Nothing Then Return False
        If formulaCB.SelectedItem Is Nothing Then Return False
        Return True

    End Function

#End Region


    Private Sub NewAccountUI_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        e.Cancel = True
        Controller.DisplayAcountsView()

    End Sub




End Class