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
' Last modified: 09/09/2015
' Author: Julien Monnereau


Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic
Imports CRUD
Imports VIBlend.WinForms.Controls


Friend Class NewAccountUI


#Region "Instance Variables"

    ' Objects
    Private m_accountsView As AccountsView
    Private m_controller As AccountsController
    Private m_parentAccountsTreeviewBox As VIBlend.WinForms.Controls.vTreeViewBox

    ' Variables
    Friend m_parentNodeId As Int32


#End Region


#Region "Initialize"

    Friend Sub New(ByRef input_accountsView As AccountsView, _
                   ByRef input_controller As AccountsController)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_accountsView = input_accountsView
        m_controller = input_controller

        ' Parents accounts treeview update
        m_parentAccountsTreeviewBox = New vTreeViewBox
        ParentTVPanel.Controls.Add(m_parentAccountsTreeviewBox)
        m_parentAccountsTreeviewBox.Dock = DockStyle.Fill
        GlobalVariables.Accounts.LoadAccountsTV(m_parentAccountsTreeviewBox.TreeView)

        ComboBoxesInitialize()
        MultilanguageSetup()

    End Sub

    Private Sub ComboBoxesInitialize()

        For Each item In m_accountsView.TypeComboBox.Items
            TypeComboBox.Items.Add(item)
        Next

        For Each item In m_accountsView.FormulaTypeComboBox.Items
            FormulaComboBox.Items.Add(item)
        Next


    End Sub

    Private Sub NewAccountUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '   VTreeViewUtil.LoadParentsTreeviewBox(ParentAccountsTreeviewBox, accountsTV)
        On Error Resume Next
        Dim parentNode = VTreeViewUtil.FindNode(m_parentAccountsTreeviewBox.TreeView, m_parentNodeId)
        If Not parentNode Is Nothing Then
            m_parentAccountsTreeviewBox.TreeView.SelectedNode = parentNode
            m_parentAccountsTreeviewBox.Text = parentNode.Text
        End If

    End Sub

    Private Sub MultilanguageSetup()

        Me.CancelBT.Text = Local.GetValue("general.cancel")
        Me.CreateAccountBT.Text = Local.GetValue("general.create")
        Me.m_accountNameLabel.Text = Local.GetValue("accounts_edition.account_name")
        Me.m_accountParentLabel.Text = Local.GetValue("accounts_edition.account_parent")
        Me.m_formulaTypeLabel.Text = Local.GetValue("accounts_edition.formula_type")
        Me.m_formatLabel.Text = Local.GetValue("accounts_edition.account_format")
        Me.m_recomputeRadioButton.Text = Local.GetValue("accounts_edition.recomputation")
        Me.m_aggregationRadioButton.Text = Local.GetValue("accounts_edition.aggregation")
        Me.m_consolidationOptionLabel.Text = Local.GetValue("accounts_edition.consolidation_option")
        Me.m_endOfPeriodRadioButton.Text = Local.GetValue("accounts_edition.end_of_period_rate")
        Me.m_averageRateRadioButton.Text = Local.GetValue("accounts_edition.average_rate")
        Me.m_conversionOptionLabel.Text = Local.GetValue("accounts_edition.currencies_conversion")
        Me.Text = Local.GetValue("accounts_edition.title_new_account")

    End Sub

#End Region


#Region "Interface"

    Delegate Sub TVUpdate_Delegate(ByRef p_node As vTreeNode, _
                                ByRef p_accountName As String, _
                                ByRef p_accountImage As Int32)
    Friend Sub TVUpdate(ByRef p_node As vTreeNode, _
                        ByRef p_accountName As String, _
                        ByRef p_accountImage As Int32)

        If Me.m_parentAccountsTreeviewBox.TreeView.InvokeRequired Then
            Dim MyDelegate As New TVUpdate_Delegate(AddressOf TVUpdate)
            Me.m_parentAccountsTreeviewBox.TreeView.Invoke(MyDelegate, New Object() {p_node, p_accountName, p_accountImage})
        Else
            p_node.Text = p_accountName
            p_node.ImageIndex = p_accountImage
            m_parentAccountsTreeviewBox.TreeView.Refresh()
        End If

    End Sub

    Delegate Sub AccountNodeAddition_Delegate(ByRef p_accountId As Int32, _
                               ByRef p_accountParentId As Int32, _
                               ByRef p_accountName As String, _
                               ByRef p_accountImage As Int32)
    Friend Sub AccountNodeAddition(ByRef p_accountId As Int32, _
                                   ByRef p_accountParentId As Int32, _
                                   ByRef p_accountName As String, _
                                   ByRef p_accountImage As Int32)

        If Me.m_parentAccountsTreeviewBox.TreeView.InvokeRequired Then
            Dim MyDelegate As New AccountNodeAddition_Delegate(AddressOf AccountNodeAddition)
            Me.m_parentAccountsTreeviewBox.TreeView.Invoke(MyDelegate, New Object() {p_accountId, p_accountParentId, p_accountName, p_accountImage})
        Else
            If p_accountParentId = 0 Then
                Dim new_node As vTreeNode = VTreeViewUtil.AddNode(CStr(p_accountId), p_accountName, m_parentAccountsTreeviewBox.TreeView, p_accountImage)
                new_node.IsVisible = True
            Else
                Dim l_parentNode As vTreeNode = VTreeViewUtil.FindNode(m_parentAccountsTreeviewBox.TreeView, p_accountParentId)
                If l_parentNode IsNot Nothing Then
                    Dim new_node As vTreeNode = VTreeViewUtil.AddNode(CStr(p_accountId), p_accountName, l_parentNode, p_accountImage)
                    new_node.IsVisible = True
                End If
            End If
            m_parentAccountsTreeviewBox.TreeView.Refresh()
        End If

    End Sub

    Delegate Sub TVNodeDelete_Delegate(ByRef p_account_id As Int32)
    Friend Sub TVNodeDelete(ByRef p_account_id As Int32)

        If Me.m_parentAccountsTreeviewBox.TreeView.InvokeRequired Then
            Dim MyDelegate As New TVNodeDelete_Delegate(AddressOf TVNodeDelete)
            Me.m_parentAccountsTreeviewBox.TreeView.Invoke(MyDelegate, New Object() {p_account_id})
        Else
            Dim l_accountNode As vTreeNode = VTreeViewUtil.FindNode(m_parentAccountsTreeviewBox.TreeView, p_account_id)
            If l_accountNode IsNot Nothing Then
                l_accountNode.Remove()
                m_parentAccountsTreeviewBox.TreeView.Refresh()
            End If
        End If

    End Sub


#End Region


#Region "Call backs"

    Private Sub CreateAccountBT_Click(sender As Object, e As EventArgs) Handles CreateAccountBT.Click

        If IsFormValid() = True Then
            Dim parent_id As Int32 = 0
            Dim conso_option As Int32 = 0
            Dim conversion_option As Int32 = 0
            Dim account_tab As Int32

            If Not m_parentAccountsTreeviewBox.TreeView.SelectedNode Is Nothing Then
                parent_id = CInt(m_parentAccountsTreeviewBox.TreeView.SelectedNode.Value)

                Dim l_account As Account = GlobalVariables.Accounts.GetValue(parent_id)
                account_tab = If(l_account Is Nothing, 0, l_account.AccountTab)
            Else
                parent_id = 0
                account_tab = m_parentAccountsTreeviewBox.TreeView.Nodes.Count
            End If
            If m_aggregationRadioButton.Checked = True Then conso_option = Account.ConsolidationOptions.AGGREGATION
            If m_recomputeRadioButton.Checked = True Then conso_option = Account.ConsolidationOptions.RECOMPUTATION
            If m_averageRateRadioButton.Checked = True Then conversion_option = Account.ConversionOptions.AVERAGE_RATE
            If m_endOfPeriodRadioButton.Checked = True Then conversion_option = Account.ConversionOptions.END_OF_PERIOD_RATE

            m_controller.CreateAccount(parent_id, _
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
            m_controller.DisplayAccountsView()
        End If

    End Sub

    Private Sub CancelBT_Click(sender As Object, e As EventArgs) Handles CancelBT.Click

        m_controller.DisplayAccountsView()

    End Sub

#End Region


#Region "Events"

    Private Sub NewAccountUI_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        m_controller.DisplayAccountsView()
    End Sub

#End Region


#Region "Checks"

    Private Function IsFormValid() As Boolean

        If m_controller.AccountNameCheck(NameTextBox.Text) = False Then
            MsgBox(Local.GetValue("accounts_edition.msg_name_empty"))
            Return False
        End If

        If ComboBoxesSelectionCheck() = False Then
            MsgBox(Local.GetValue("accounts_edition.msg_fill_values"))
            Return False
        End If

        If m_controller.IsUsedName(NameTextBox.Text) Then
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







End Class