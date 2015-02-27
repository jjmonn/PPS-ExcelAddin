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
' Last modified: 29/12/2014
' Author: Julien Monnereau


Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic


Friend Class NewAccountUI


#Region "Instance Variables"

    ' Objects
    Private AccountsView As AccountsMGT_UI
    Private Controller As AccountsController
    Private accountsTV As New TreeView

    ' Variables
    Private parent_node As TreeNode
    Private isFormExpanded As Boolean

    ' Constants
    Private Const COLLAPSED_WIDTH As Int32 = 720
    Private Const EXPANDED_WIDTH As Int32 = 1100
    Private Const COLLAPSED_HEIGHT As Int32 = 430
    Private Const EXPANDED_HEIGHT As Int32 = 480


#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef input_accountsView As AccountsMGT_UI, _
                             ByRef input_controller As AccountsController)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AccountsView = input_accountsView
        Controller = input_controller
        Account.LoadAccountsTree(accountsTV)
        ComboBoxesInitialize()
       
        accountsTVPanel.Controls.Add(accountsTV)
        accountsTV.Dock = DockStyle.Fill
        AddHandler accountsTV.NodeMouseClick, AddressOf AccountsTV_NodeMouseClick
        AddHandler accountsTV.KeyDown, AddressOf AccountsTV_KeyDown

    End Sub

    Private Sub ComboBoxesInitialize()

        For Each item In AccountsView.formatsCB.Items
            formatCB.Items.Add(item)
        Next

        For Each item In AccountsView.formulaTypeCB.Items
            formulaCB.Items.Add(item)
        Next

        For Each item In AccountsView.TypeCB.Items
            typeCB.Items.Add(item)
        Next

    End Sub


#End Region


#Region "Interface"

    Protected Friend Sub PrePopulateForm(ByRef inputNode As TreeNode)

        parentTB.Text = inputNode.Text
        parent_node = inputNode
        Dim parentFormatCode = Controller.ReadAccount(parent_node.Name, ACCOUNT_FORMAT_VARIABLE)
        formatCB.SelectedItem = AccountsView.formatKeyNameDictionary(parentFormatCode)
        typeCB.SelectedItem = AccountsView.accountsTypesKeyNameDict(DEFAULT_ACCOUNT_TYPE_VALUE)
        Dim parentFType = Controller.ReadAccount(parent_node.Name, ACCOUNT_FORMULA_TYPE_VARIABLE)
        formulaCB.SelectedItem = AccountsView.fTypeCodeNameDictionary(parentFType)

    End Sub

    Private Function CreateNewAccount() As Boolean

        If IsFormValid() = True Then
            Dim TempHT As New Hashtable
            TempHT.Add(ACCOUNT_PARENT_ID_VARIABLE, parent_node.Name)
            TempHT.Add(ACCOUNT_NAME_VARIABLE, nameTB.Text)
            TempHT.Add(ACCOUNT_FORMULA_TYPE_VARIABLE, AccountsView.fTypeNameCodeDictionary(formulaCB.Text))
            If TempHT(ACCOUNT_FORMULA_TYPE_VARIABLE) = AGGREGATION_F_TYPE_CODE Then
                TempHT.Add(ACCOUNT_FORMULA_VARIABLE, AGGREGATION_F_TYPE_CODE)
            Else
                TempHT.Add(ACCOUNT_FORMULA_VARIABLE, "")
            End If
            TempHT.Add(ACCOUNT_FORMAT_VARIABLE, AccountsView.formatsNameKeyDictionary(formatCB.Text))
            TempHT.Add(ACCOUNT_TYPE_VARIABLE, AccountsView.accountsTypeNameKeyDictionary(typeCB.Text))
            TempHT.Add(ACCOUNT_IMAGE_VARIABLE, AccountsView.ftype_icon_dic(TempHT(ACCOUNT_FORMULA_TYPE_VARIABLE)))
            TempHT.Add(ACCOUNT_SELECTED_IMAGE_VARIABLE, AccountsView.ftype_icon_dic(TempHT(ACCOUNT_FORMULA_TYPE_VARIABLE)))
            If aggregation_RB.Checked = True Then TempHT.Add(ACCOUNT_RECOMPUTATION_OPTION_VARIABLE, AGGREGATION_CODE)
            If recompute_RB.Checked = True Then TempHT.Add(ACCOUNT_RECOMPUTATION_OPTION_VARIABLE, RECOMPUTATION_CODE)
            If flux_RB.Checked = True Then TempHT.Add(ACCOUNT_CONVERSION_FLAG_VARIABLE, FLUX_CONVERSION)
            If bs_item_RB.Checked = True Then TempHT.Add(ACCOUNT_CONVERSION_FLAG_VARIABLE, BS_ITEM_CONVERSION)

            Controller.CreateAccount(TempHT, parent_node)
            Return True
        Else
            Return False
        End If

    End Function

#End Region


#Region "Call backs"

    Private Sub selectParentBT_Click(sender As Object, e As EventArgs) Handles selectParentBT.Click

        If isFormExpanded = False Then
            DisplayParentAccountsTV()
        Else
            HideParentAccountsTV()
        End If

    End Sub

    Private Sub CreateAccountBT_Click(sender As Object, e As EventArgs) Handles CreateAccountBT.Click

        If CreateNewAccount() = True Then
            Controller.DisplayAcountsView()
        End If

    End Sub

    Private Sub CancelBT_Click(sender As Object, e As EventArgs) Handles CancelBT.Click

        Controller.DisplayAcountsView()

    End Sub

#End Region


#Region "Event"

    Private Sub AccountsTV_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs)

        If Not accountsTV.SelectedNode Is Nothing Then
            Select Case accountsTV.SelectedNode.Nodes.Count
                Case 0
                    parent_node = e.Node
                    parentTB.Text = accountsTV.SelectedNode.Text
                    HideParentAccountsTV()
            End Select
        End If

    End Sub

    Private Sub AccountsTV_KeyDown(sender As Object, e As KeyEventArgs)

        If e.KeyCode = Keys.Enter _
        AndAlso Not accountsTV.SelectedNode Is Nothing Then
            parent_node = accountsTV.SelectedNode
            parentTB.Text = accountsTV.SelectedNode.Text
            HideParentAccountsTV()
        End If


    End Sub

#End Region


#Region "Checks"

    Private Function IsFormValid() As Boolean

        If controller.AccountNameCheck(nameTB.Text) = False Then Return False
        If ComboBoxesSelectionCheck() = False Then Return False
        Return True

    End Function

    Private Function ComboBoxesSelectionCheck() As Boolean

        If formatCB.SelectedItem Is Nothing Then Return False
        If formulaCB.SelectedItem Is Nothing Then Return False
        If typeCB.SelectedItem Is Nothing Then Return False
        Return True

    End Function

#End Region


#Region "Utilities"

    Private Sub DisplayParentAccountsTV()

        Me.Width = EXPANDED_WIDTH
        Me.Height = EXPANDED_HEIGHT
        isFormExpanded = True

    End Sub

    Private Sub HideParentAccountsTV()

        Me.Width = COLLAPSED_WIDTH
        Me.Height = COLLAPSED_HEIGHT
        isFormExpanded = False

    End Sub

    Private Sub NewAccountUI_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        e.Cancel = True
        Controller.DisplayAcountsView()

    End Sub
  
#End Region


End Class