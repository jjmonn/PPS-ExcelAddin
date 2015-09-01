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
' Last modified: 28/08/2015
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
    Friend parent_node As TreeNode
    Private isFormExpanded As Boolean

    ' Constants
    Private Const COLLAPSED_WIDTH As Int32 = 720
    Private Const EXPANDED_WIDTH As Int32 = 1100
    Private Const COLLAPSED_HEIGHT As Int32 = 430
    Private Const EXPANDED_HEIGHT As Int32 = 480


#End Region


#Region "Initialize"

    Friend Sub New(ByRef input_accountsView As AccountsControl, _
                   ByRef input_controller As AccountsController)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AccountsView = input_accountsView
        Controller = input_controller
        ComboBoxesInitialize()

        accountsTVPanel.Controls.Add(accountsTV)
        accountsTV.Dock = DockStyle.Fill
        AddHandler accountsTV.NodeMouseClick, AddressOf AccountsTV_NodeMouseClick
        AddHandler accountsTV.KeyDown, AddressOf AccountsTV_KeyDown

    End Sub

    Private Sub ComboBoxesInitialize()

        For Each item In AccountsView.FormatComboBox.Items
            formatCB.Items.Add(item)
        Next

        For Each item In AccountsView.FormulaTypeComboBox.Items
            formulaCB.Items.Add(item)
        Next


    End Sub

#End Region


#Region "Interface"

    Protected Friend Sub PrePopulateForm(ByRef inputNode As TreeNode)

        parentTB.Text = inputNode.Text
        parent_node = inputNode
        TreeViewsUtilities.TreeviewCopy(inputNode.TreeView, accountsTV)
        'Dim parentFormatCode = Controller.ReadAccount(parent_node.Name, ACCOUNT_FORMAT_VARIABLE)
        'formatCB.SelectedItem = AccountsView.formatKeyNameDictionary(parentFormatCode)
        'typeCB.SelectedItem = AccountsView.accountsTypesKeyNameDict(DEFAULT_ACCOUNT_TYPE_VALUE)
        'Dim parentFType = Controller.ReadAccount(parent_node.Name, ACCOUNT_FORMULA_TYPE_VARIABLE)
        'formulaCB.SelectedItem = AccountsView.fTypeCodeNameDictionary(parentFType)

    End Sub

  
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

        If IsFormValid() = True Then
            Controller.CreateAccount()
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

        If formatCB.SelectedItem Is Nothing Then Return False
        If formulaCB.SelectedItem Is Nothing Then Return False
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