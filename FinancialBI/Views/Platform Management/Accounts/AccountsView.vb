' AccountsMGT_UI
' 
' Accounts management User Interface class
'
' To do:
'       - Formulas WC and BS  -> must be explained in the manual !!
'       -
'              
'
'    Known bugs:
'       -
'        
'
' Last modified: 05/11/2015
' Author: Julien Monnereau


Imports Microsoft.Office.Interop
Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports VIBlend.WinForms.Controls
Imports CRUD

Friend Class AccountsView


#Region "Instance Variables"

    ' Objects
    Friend m_controller As AccountsController
    Friend m_accountTV As vTreeView
    Friend m_globalFactsTV As vTreeView
    Friend m_currentNode As vTreeNode

    ' Variables
    Private m_formulasTypesIdItemDict As New Dictionary(Of Int32, ListItem)
    Private m_formatsIdItemDict As New Dictionary(Of Int32, ListItem)
    Private m_currenciesConversionIdItemDict As New Dictionary(Of Int32, ListItem)
    Private m_consoOptionIdItemDict As New Dictionary(Of Int32, ListItem)
    Private m_isRevertingFType As Boolean = False
    Private m_isDisplayingAttributes As Boolean
    Private m_dragAndDrop As Boolean = False

    ' Constants
    Private Const MARGIN_SIZE As Integer = 15
    Private Const ACCCOUNTS_TV_MAX_WIDTH As Integer = 600
    Private Const MARGIN1 As Integer = 30


#End Region


#Region "Initialization"

    Friend Sub New(ByRef p_inputController As AccountsController, _
                   ByRef p_inputAccountTV As vTreeView, _
                   ByRef p_inputGlobalFactsTv As vTreeView)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_controller = p_inputController
        m_accountTV = p_inputAccountTV
        m_globalFactsTV = p_inputGlobalFactsTv
        AccountsTVInit()
        GlobalFactsTVInit()
        ComboBoxesInit()
        SetAccountUIState(False, GlobalVariables.Users.CurrentUserIsAdmin())
        MultilangueSetup()

    End Sub

    Private Sub MultilangueSetup()

        Me.m_accountDescriptionGroupBox.Text = Local.GetValue("accounts_edition.account_description")
        Me.SaveDescriptionBT.Text = Local.GetValue("accounts_edition.save_description")
        Me.m_accountFormulaGroupBox.Text = Local.GetValue("accounts_edition.account_formula")
        Me.m_formulaEditionButton.Text = Local.GetValue("accounts_edition.edit_formula")
        Me.submit_cmd.Text = Local.GetValue("accounts_edition.validate_formula")
        Me.m_accountInformationGroupbox.Text = Local.GetValue("accounts_edition.account_information")
        Me.m_accountNameLabel.Text = Local.GetValue("accounts_edition.account_name")
        Me.m_accountFormulaTypeLabel.Text = Local.GetValue("accounts_edition.formula_type")
        Me.m_accountTypeLabel.Text = Local.GetValue("accounts_edition.account_type")
        Me.m_accountConsolidationOptionLabel.Text = Local.GetValue("accounts_edition.consolidation_option")
        Me.m_accountcurrenciesConversionLabel.Text = Local.GetValue("accounts_edition.currencies_conversion")
        Me.m_globalFactsLabel.Text = Local.GetValue("accounts_edition.macro_economic_indicators")
        Me.AddSubAccountToolStripMenuItem.Text = Local.GetValue("accounts_edition.new_account")
        Me.AddCategoryToolStripMenuItem.Text = Local.GetValue("accounts_edition.add_tab_account")
        Me.DeleteAccountToolStripMenuItem.Text = Local.GetValue("accounts_edition.delete_account")
        Me.DropHierarchyToExcelToolStripMenuItem.Text = Local.GetValue("accounts_edition.drop_to_excel")
        Me.NewToolStripMenuItem.Text = Local.GetValue("general.account")
        Me.CreateANewAccountToolStripMenuItem.Text = Local.GetValue("accounts_edition.new_account")
        Me.CreateANewCategoryToolStripMenuItem.Text = Local.GetValue("accounts_edition.add_tab_account")
        Me.DeleteAccountToolStripMenuItem1.Text = Local.GetValue("accounts_edition.delete_account")
        Me.DropAllAccountsHierarchyToExcelToolStripMenuItem.Text = Local.GetValue("accounts_edition.drop_to_excel")
        Me.DropSelectedAccountHierarchyToExcelToolStripMenuItem.Text = Local.GetValue("accounts_edition.drop_selected_hierarchy_to_excel")
        Me.HelpToolStripMenuItem.Text = Local.GetValue("general.help")


    End Sub

    Private Sub SetAccountUIState(ByRef p_uiState As Boolean, ByRef p_rightClickState As Boolean)
        SaveDescriptionBT.Enabled = p_uiState
        Name_TB.Enabled = p_uiState
        m_formulaTextBox.Enabled = p_uiState
        FormulaTypeComboBox.Enabled = p_uiState
        TypeComboBox.Enabled = p_uiState
        CurrencyConversionComboBox.Enabled = p_uiState
        m_descriptionTextBox.Enabled = p_uiState
        ConsolidationOptionComboBox.Enabled = p_uiState
        AddSubAccountToolStripMenuItem.Enabled = p_rightClickState
        AddCategoryToolStripMenuItem.Enabled = p_rightClickState
        DeleteAccountToolStripMenuItem.Enabled = p_rightClickState
        DeleteAccountToolStripMenuItem1.Enabled = p_rightClickState
        submit_cmd.Enabled = p_uiState
        m_formulaEditionButton.Enabled = p_uiState
        CreateANewAccountToolStripMenuItem.Enabled = p_rightClickState
        CreateANewCategoryToolStripMenuItem.Enabled = p_rightClickState
    End Sub

    Private Sub DesactivateUnallowed()
        SetAccountUIState(GlobalVariables.Users.CurrentUserIsAdmin(), GlobalVariables.Users.CurrentUserIsAdmin())
    End Sub

    Private Sub AccountsTVInit()

        m_accountTV.ContextMenuStrip = TVRCM
        m_accountTV.Dock = DockStyle.Fill
        m_accountTV.AllowDrop = True
        m_accountTV.LabelEdit = False
        m_accountTV.CollapseAll()
        m_accountTV.ImageList = accountsIL
        VTreeViewUtil.InitTVFormat(m_accountTV)
        m_accountTV.BorderColor = Drawing.Color.Transparent
        AccountsTVPanel.Controls.Add(m_accountTV)

        AddHandler m_accountTV.AfterSelect, AddressOf AccountsTV_AfterSelect
        AddHandler m_accountTV.KeyDown, AddressOf AccountsTV_KeyDown
        AddHandler m_accountTV.DoubleClick, AddressOf AccountsTV_MouseDoubleClick

        ' Drag and drop events
        AddHandler m_accountTV.MouseDown, AddressOf AccountsTV_MouseDown
        AddHandler m_accountTV.DragDrop, AddressOf AccountsTV_DragDrop
        AddHandler m_accountTV.DragOver, AddressOf AccountsTV_DragOver
        AddHandler m_accountTV.AfterSelect, AddressOf AccountsTV_AfterSelect

        AddHandler m_globalFactsTV.MouseDoubleClick, AddressOf GlobalFactTV_NodeMouseDoubleClick
        AddHandler m_formulaTextBox.KeyDown, AddressOf FormulaTextBox_KeyDown

    End Sub

    Private Sub GlobalFactsTVInit()

        m_globalFactsTV.Dock = DockStyle.Fill
        m_globalFactsTV.LabelEdit = False
        m_globalFactsTV.CollapseAll()
        m_globalFactsTV.ImageList = m_globalFactsImageList
        VTreeViewUtil.InitTVFormat(m_globalFactsTV)

        GlobalFactsPanel.Controls.Add(m_globalFactsTV)
        AddHandler m_globalFactsTV.DragEnter, AddressOf GlobalFactsTV_DragEnter
        AddHandler m_globalFactsTV.MouseDown, AddressOf GlobalFactsTV_MouseDown

    End Sub

    Private Sub ComboBoxesInit()

        ' Formulas 
        Dim InputListItem As New ListItem
        InputListItem.Text = "Input"
        InputListItem.Value = Account.FormulaTypes.HARD_VALUE_INPUT
        FormulaTypeComboBox.Items.Add(InputListItem)
        m_formulasTypesIdItemDict.Add(InputListItem.Value, InputListItem)

        Dim FormulaListItem As New ListItem
        FormulaListItem.Text = "Formula"
        FormulaListItem.Value = Account.FormulaTypes.FORMULA
        FormulaTypeComboBox.Items.Add(FormulaListItem)
        m_formulasTypesIdItemDict.Add(FormulaListItem.Value, FormulaListItem)

        Dim AggregationListItem As New ListItem
        AggregationListItem.Text = "Aggregation of Sub Accounts"
        AggregationListItem.Value = Account.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS
        FormulaTypeComboBox.Items.Add(AggregationListItem)
        m_formulasTypesIdItemDict.Add(AggregationListItem.Value, AggregationListItem)

        Dim FirstPeriodInputListItem As New ListItem
        FirstPeriodInputListItem.Text = "First Period Input"
        FirstPeriodInputListItem.Value = Account.FormulaTypes.FIRST_PERIOD_INPUT
        FormulaTypeComboBox.Items.Add(FirstPeriodInputListItem)
        m_formulasTypesIdItemDict.Add(FirstPeriodInputListItem.Value, FirstPeriodInputListItem)

        Dim TitleListItem As New ListItem
        TitleListItem.Text = "Title"
        TitleListItem.Value = Account.FormulaTypes.TITLE
        FormulaTypeComboBox.Items.Add(TitleListItem)
        m_formulasTypesIdItemDict.Add(TitleListItem.Value, TitleListItem)

        ' Type
        Dim MonetaryFormatLI As New ListItem
        MonetaryFormatLI.Text = "Monetary"
        MonetaryFormatLI.Value = Account.AccountType.MONETARY
        TypeComboBox.Items.Add(MonetaryFormatLI)
        m_formatsIdItemDict.Add(MonetaryFormatLI.Value, MonetaryFormatLI)

        Dim NormalFormatLI As New ListItem
        NormalFormatLI.Text = "Number"
        NormalFormatLI.Value = Account.AccountType.NUMBER
        TypeComboBox.Items.Add(NormalFormatLI)
        m_formatsIdItemDict.Add(NormalFormatLI.Value, NormalFormatLI)

        Dim percentageFormatLI As New ListItem
        percentageFormatLI.Text = "Percentage"
        percentageFormatLI.Value = Account.AccountType.PERCENTAGE
        TypeComboBox.Items.Add(percentageFormatLI)
        m_formatsIdItemDict.Add(percentageFormatLI.Value, percentageFormatLI)

        Dim DateFormatLI As New ListItem
        DateFormatLI.Text = "Date"
        DateFormatLI.Value = Account.AccountType.DATE_
        TypeComboBox.Items.Add(DateFormatLI)
        m_formatsIdItemDict.Add(DateFormatLI.Value, DateFormatLI)


        ' Currencies Conversion
        Dim NoConversionLI As New ListItem
        NoConversionLI.Text = "Non Converted"
        NoConversionLI.Value = Account.ConversionOptions.NO_CONVERSION
        CurrencyConversionComboBox.Items.Add(NoConversionLI)
        m_currenciesConversionIdItemDict.Add(NoConversionLI.Value, NoConversionLI)

        Dim AverageRateLI As New ListItem
        AverageRateLI.Text = "Average Exchange Rate"
        AverageRateLI.Value = Account.ConversionOptions.AVERAGE_RATE
        CurrencyConversionComboBox.Items.Add(AverageRateLI)
        m_currenciesConversionIdItemDict.Add(AverageRateLI.Value, AverageRateLI)

        Dim EndOfPeriodRateLI As New ListItem
        EndOfPeriodRateLI.Text = "End of Period Exchange Rate"
        EndOfPeriodRateLI.Value = Account.ConversionOptions.END_OF_PERIOD_RATE
        CurrencyConversionComboBox.Items.Add(EndOfPeriodRateLI)
        m_currenciesConversionIdItemDict.Add(EndOfPeriodRateLI.Value, EndOfPeriodRateLI)


        ' Recomputation Option
        Dim AggregatedLI As New ListItem
        AggregatedLI.Text = "Aggregated"
        AggregatedLI.Value = Account.ConsolidationOptions.AGGREGATION
        ConsolidationOptionComboBox.Items.Add(AggregatedLI)
        m_consoOptionIdItemDict.Add(AggregatedLI.Value, AggregatedLI)

        Dim RecomputedLI As New ListItem
        RecomputedLI.Text = "Recomputed"
        RecomputedLI.Value = Account.ConsolidationOptions.RECOMPUTATION
        ConsolidationOptionComboBox.Items.Add(RecomputedLI)
        m_consoOptionIdItemDict.Add(RecomputedLI.Value, RecomputedLI)

        Dim NoneLI As New ListItem
        NoneLI.Text = "None"
        NoneLI.Value = Account.ConsolidationOptions.NONE
        ConsolidationOptionComboBox.Items.Add(NoneLI)
        m_consoOptionIdItemDict.Add(NoneLI.Value, NoneLI)

    End Sub

#End Region


#Region "Interface"

    Delegate Sub TVUpdate_Delegate(ByRef p_node As vTreeNode, _
                                   ByRef p_accountName As String, _
                                   ByRef p_accountImage As Int32)
    Friend Sub TVUpdate(ByRef p_node As vTreeNode, _
                        ByRef p_accountName As String, _
                        ByRef p_accountImage As Int32)

        If m_accountTV.InvokeRequired Then
            Dim MyDelegate As New TVUpdate_Delegate(AddressOf TVUpdate)
            m_accountTV.Invoke(MyDelegate, New Object() {p_node, p_accountName, p_accountImage})
        Else
            p_node.Text = p_accountName
            p_node.ImageIndex = p_accountImage
            m_accountTV.Refresh()
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

        If Me.m_accountTV.InvokeRequired Then
            Dim MyDelegate As New AccountNodeAddition_Delegate(AddressOf AccountNodeAddition)
            m_accountTV.Invoke(MyDelegate, New Object() {p_accountId, p_accountParentId, p_accountName, p_accountImage})
        Else
            If p_accountParentId = 0 Then
                Dim new_node As vTreeNode = VTreeViewUtil.AddNode(CStr(p_accountId), p_accountName, m_accountTV, p_accountImage)
                new_node.IsVisible = True
            Else
                Dim l_parentNode As vTreeNode = VTreeViewUtil.FindNode(m_accountTV, p_accountParentId)
                If l_parentNode IsNot Nothing Then
                    Dim new_node As vTreeNode = VTreeViewUtil.AddNode(CStr(p_accountId), p_accountName, l_parentNode, p_accountImage)
                    new_node.IsVisible = True
                End If
            End If
            m_accountTV.Refresh()
        End If

    End Sub

    Delegate Sub TVNodeDelete_Delegate(ByRef p_account_id As Int32)
    Friend Sub TVNodeDelete(ByRef p_account_id As Int32)

        If Me.m_accountTV.InvokeRequired Then
            Dim MyDelegate As New TVNodeDelete_Delegate(AddressOf TVNodeDelete)
            Me.m_accountTV.Invoke(MyDelegate, New Object() {p_account_id})
        Else
            Dim l_accountNode As vTreeNode = VTreeViewUtil.FindNode(m_accountTV, p_account_id)
            If l_accountNode IsNot Nothing Then
                l_accountNode.Remove()
                m_accountTV.Refresh()
            End If
        End If

    End Sub


#End Region


#Region "Call backs"

    Private Sub newAccountBT_Click(sender As Object, e As EventArgs) Handles CreateANewAccountToolStripMenuItem.Click, _
                                                                             AddSubAccountToolStripMenuItem.Click

        m_formulaEditionButton.Toggle = CheckState.Unchecked
        If Not m_currentNode Is Nothing Then
            m_controller.DisplayNewAccountView(m_currentNode)
        Else
            MsgBox(Local.GetValue("accounts_edition.msg_select_account"))
        End If

    End Sub

    Private Sub newCategoryBT_Click(sender As Object, e As EventArgs) Handles CreateANewCategoryToolStripMenuItem.Click, _
                                                                              AddCategoryToolStripMenuItem.Click

        m_formulaEditionButton.Toggle = CheckState.Unchecked
        Dim newCategoryName As String = InputBox(Local.GetValue("accounts_edition.msg_new_tab_name"), _
                                                 Local.GetValue("accounts_edition.title_new_tab_name"), "")
        If newCategoryName <> "" Then

            If m_controller.AccountNameCheck(newCategoryName) = True Then
                m_controller.CreateAccount(0, _
                                         newCategoryName, _
                                         Account.FormulaTypes.TITLE, _
                                         "", _
                                         Account.AccountType.DATE_, _
                                         1, _
                                         1, _
                                         TITLE_FORMAT_CODE, _
                                         Account.FormulaTypes.TITLE, _
                                         1, _
                                         m_accountTV.Nodes.Count)
            End If
        End If

    End Sub

    Private Sub Submit_Formula_Click(sender As Object, e As EventArgs) Handles submit_cmd.Click

        Dim formulastr As String = m_formulaTextBox.Text
        If formulastr = "" Then
            Dim confirm2 As Integer = MessageBox.Show(Local.GetValue("accounts_edition.msg_formula_empty"), _
                                                      Local.GetValue("accounts_edition.title_formula_validation_confirmation"), _
                                                      MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            If confirm2 = DialogResult.Yes Then
                GoTo TokensCheck
            Else
                Exit Sub
            End If
        Else
            GoTo TokensCheck
        End If

TokensCheck:
        Dim errorsList = m_controller.CheckFormulaForUnkonwnTokens(formulastr)
        If errorsList.Count > 0 Then
            Dim errorsStr As String = ""
            For Each errorItem As String In errorsList
                errorsStr = errorsStr & errorItem & Chr(13)
            Next
            MsgBox(Local.GetValue("accounts_edition.msg_items_not_mapped") + Chr(13) & errorsStr)
            Exit Sub
        Else
            Dim confirm As Integer = MessageBox.Show(Local.GetValue("accounts_edition.msg_formula_edition_for_account") + Chr(13) + Name_TB.Text + Chr(13) + Local.GetValue("accounts_edition.msg_account_deletion2"), _
                                               Local.GetValue("accounts_edition.title_formula_validation_confirmation"), _
                                                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            If confirm = DialogResult.Yes Then
                If Not m_controller.GetAccount(Name_TB.Text) Is Nothing Then
                    m_formulaEditionButton.Toggle = CheckState.Unchecked
                    Dim accountId As Int32 = m_controller.GetAccount(Name_TB.Text).Id
                    m_controller.UpdateAccountFormula(accountId, m_controller.GetCurrentParsedFormula)
                End If
            Else
                m_formulaTextBox.Text = m_controller.GetFormulaText(m_accountTV.SelectedNode.Value)
            End If
        End If

        'DependanciesCheck:
        '        If m_controller.InterdependancyTest = True Then
        '            GoTo SubmitFormula
        '        Else
        '            Exit Sub
        '        End If


    End Sub

    Private Sub B_DeleteAccount_Click(sender As Object, e As EventArgs) Handles DeleteAccountToolStripMenuItem.Click, _
                                                                                DeleteAccountToolStripMenuItem1.Click

        If Not m_currentNode Is Nothing Then
            m_formulaEditionButton.Toggle = CheckState.Unchecked
            Dim dependantAccountslist() As String = m_controller.ExistingDependantAccounts(m_currentNode)
            If dependantAccountslist.Length > 0 Then

                Dim listStr As String = ""
                For Each accountName In dependantAccountslist
                    listStr = listStr & " - " & accountName & Chr(13)
                Next
                MsgBox(Local.GetValue("accounts_edition.msg_dependant_accounts") & Chr(13) & _
                       listStr & Chr(13) & _
                       Local.GetValue("accounts_edition.msg_formula_to_be_changed"))
                Exit Sub
            End If

            Dim confirm As GeneralUtilities.CheckResult = GeneralUtilities.AskPasswordConfirmation(Local.GetValue("accounts_edition.msg_account_deletion1") + Chr(13) + Chr(10) + Name_TB.Text + Chr(13) + Chr(10) + Chr(13) + Chr(10) + _
                                                                                                   Local.GetValue("accounts_edition.msg_account_deletion2") + Chr(13) + Chr(10) + _
                                                                                                   Local.GetValue("accounts_edition.msg_account_deletion3"), _
                                                                                                   Local.GetValue("accounts_edition.msg_account_deletion_confirmation"))
            If confirm = GeneralUtilities.CheckResult.Success Then
                m_controller.DeleteAccount(CInt(m_currentNode.value))
                m_currentNode.Remove()
                m_currentNode = Nothing
            ElseIf confirm = GeneralUtilities.CheckResult.Fail Then
                MessageBox.Show(Local.GetValue("accounts_edition.msg_incorrect_password"))
            End If
        End If

    End Sub

    Private Sub BDropToWS_Click(sender As Object, e As EventArgs) Handles DropAllAccountsHierarchyToExcelToolStripMenuItem.Click, _
                                                                          DropHierarchyToExcelToolStripMenuItem.Click

        m_formulaEditionButton.Toggle = CheckState.Unchecked
        Dim ActiveWS As Excel.Worksheet = GlobalVariables.APPS.ActiveSheet
        Dim RNG As Excel.Range = GlobalVariables.APPS.Application.ActiveCell
        Dim Response As MsgBoxResult

        If IsNothing(RNG) Then
            MsgBox(Local.GetValue("accounts_edition.msg_destination_cell_not_valid"))
            Exit Sub
        Else
            Response = MsgBox(Local.GetValue("accounts_edition.msg_accounts_drop") + RNG.Address, MsgBoxStyle.OkCancel)
            If Response = MsgBoxResult.Ok Then
                ' Launch Accounts Drop
                WorksheetWrittingFunctions.WriteAccountsFromTreeView(m_accountTV, RNG)
            ElseIf Response = MsgBoxResult.Cancel Then
                Exit Sub
            End If
        End If
    End Sub

    Private Sub SaveDescriptionBT_Click(sender As Object, e As EventArgs) Handles SaveDescriptionBT.Click

        Dim l_account As Account = m_controller.GetAccount(Name_TB.Text)

        If l_account Is Nothing Then Exit Sub
        m_controller.UpdateAccountDescription(l_account.Id, m_descriptionTextBox.Text)

    End Sub

    Private Sub m_allocationKeyButton_Click(sender As Object, e As EventArgs) Handles m_allocationKeyButton.Click

        If m_currentNode IsNot Nothing Then
            Dim l_allocationKeysController As New AllocationKeysController(m_currentNode.Value)
        End If

    End Sub

#End Region


#Region "Treeview Events"

    Private Sub AccountsTV_AfterSelect(sender As Object, e As vTreeViewEventArgs)

        If m_dragAndDrop = False _
        AndAlso m_formulaEditionButton.Toggle = CheckState.Unchecked _
        AndAlso m_isDisplayingAttributes = False Then
            m_currentNode = e.Node
            If m_currentNode IsNot Nothing Then
                DesactivateUnallowed()
                DisplayAttributes()
            End If
        Else
            m_accountTV.Capture = False
        End If

    End Sub

    Private Sub AccountsTV_KeyDown(sender As Object, e As KeyEventArgs)

        If m_dragAndDrop = False Then
            Select Case e.KeyCode
                Case Keys.Delete : B_DeleteAccount_Click(sender, e)

                Case Keys.Up
                    If e.Control Then
                        VTreeViewUtil.MoveNodeUp(m_accountTV.SelectedNode)
                    End If
                Case Keys.Down
                    If e.Control Then
                        VTreeViewUtil.MoveNodeDown(m_accountTV.SelectedNode)
                    End If
            End Select
        End If

    End Sub

    Private Sub FormulaTextBox_KeyDown(sender As Object, e As KeyEventArgs)
        If (e.KeyCode = Keys.A AndAlso e.Control) Then
            m_formulaTextBox.SelectAll()
        End If
    End Sub

#Region "Nodes Drag and Drop Procedure"

    ' Accounts TV

    'Private Sub AccountsTV_ItemDrag(sender As Object, e As ItemDragEventArgs)

    '    If m_currentNode Is Nothing Then Exit Sub
    '    If m_currentNode.Parent IsNot Nothing Then
    '        DoDragDrop(e.Item, Windows.Forms.DragDropEffects.Move)
    '    End If

    'End Sub

    Private Sub AccountsTV_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)

        If e.Button = MouseButtons.Right Then
            TVRCM.Visible = False
            TVRCM.Show(e.Location)
            TVRCM.Visible = True
        Else
            Dim l_node As vTreeNode = VTreeViewUtil.GetNodeAtPosition(m_accountTV, e.Location)
            If l_node IsNot Nothing Then
                Me.m_accountTV.DoDragDrop(l_node, DragDropEffects.Move)
            End If
        End If

    End Sub

    Private Sub AccountsTV_DragOver(sender As Object, e As DragEventArgs)

        If m_formulaEditionButton.Toggle = CheckState.Unchecked Then

            If e.Data.GetDataPresent("VIBlend.WinForms.Controls.vTreeNode", True) = False Then Exit Sub
            Dim pt As Drawing.Point = CType(sender, vTreeView).PointToClient(New Drawing.Point(e.X, e.Y))
            Dim targetNode As vTreeNode = VTreeViewUtil.GetNodeAtPosition(m_accountTV, pt)

            'See if the targetNode is currently selected, if so no need to validate again
            If Not (m_accountTV.SelectedNode Is targetNode) Then       'Select the node currently under the cursor
                m_accountTV.SelectedNode = targetNode

                'Check that the selected node is not the dropNode and also that it is not a child of the dropNode and therefore an invalid target
                Dim dropNode As vTreeNode = CType(e.Data.GetData("VIBlend.WinForms.Controls.vTreeNode"), vTreeNode)

                Do Until targetNode Is Nothing
                    If targetNode Is dropNode Then
                        e.Effect = DragDropEffects.None
                        Exit Sub
                    End If
                    targetNode = targetNode.Parent
                Loop
            End If
            'Currently selected node is a suitable target
            e.Effect = DragDropEffects.Move
            m_dragAndDrop = True
        End If

    End Sub

    Private Sub AccountsTV_DragDrop(sender As Object, e As DragEventArgs)

        If m_formulaEditionButton.Toggle = CheckState.Unchecked Then
            'Check that there is a TreeNode being dragged
            If e.Data.GetDataPresent("VIBlend.WinForms.Controls.vTreeNode", True) = False Then Exit Sub

            Dim selectedTreeview As vTreeView = CType(sender, vTreeView)

            'Get the TreeNode being dragged
            Dim dropNode As vTreeNode = CType(e.Data.GetData("VIBlend.WinForms.Controls.vTreeNode"), vTreeNode)

            'The target node should be selected from the DragOver event
            Dim targetNode As vTreeNode = selectedTreeview.SelectedNode

            If targetNode Is Nothing Then
                Dim l_dropAccount As Account = m_controller.GetAccount(CUInt(dropNode.Value))

                If (l_dropAccount IsNot Nothing AndAlso l_dropAccount.ParentId <> 0 AndAlso l_dropAccount.FormulaType = Account.FormulaTypes.TITLE) Then
                    selectedTreeview.Nodes.Add(dropNode)
                Else
                    Exit Sub
                End If
            Else
                If Not targetNode Is dropNode _
                AndAlso VTreeViewUtil.GetAllChildrenNodesList(dropNode).Contains(targetNode) = False Then

                    dropNode.Remove()                   'Remove the drop node from its current location
                    targetNode.Nodes.Add(dropNode)

                Else
                    e.Effect = DragDropEffects.None
                    m_dragAndDrop = False
                    Exit Sub
                End If
            End If

                m_dragAndDrop = False
                dropNode.IsVisible = True                     ' Ensure the newley created node is visible to the user and 
                selectedTreeview.SelectedNode = dropNode     ' Select it
            Dim l_account = m_controller.GetAccountCopy(CUInt(dropNode.Value))

                l_account.AccountTab = m_accountTV.Nodes.IndexOf(VTreeViewUtil.ReturnRootNodeFromNode(dropNode))
                l_account.ParentId = If(targetNode Is Nothing, 0, targetNode.Value)
                m_controller.UpdateAccount(l_account)

            End If


    End Sub

    ' Facts TV
    Private Sub GlobalFactsTV_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)

        Dim l_node As vTreeNode = VTreeViewUtil.GetNodeAtPosition(m_globalFactsTV, e.Location)
        If l_node IsNot Nothing Then
            Me.m_globalFactsTV.DoDragDrop(l_node, DragDropEffects.Move)
        End If

    End Sub

    Private Sub GlobalFactsTV_DragEnter(sender As Object, e As DragEventArgs)

        If e.Data.GetDataPresent("VIBlend.WinForms.Controls.vTreeNode", True) Then
            e.Effect = DragDropEffects.Move
            m_dragAndDrop = True
        Else
            e.Effect = DragDropEffects.None
        End If

    End Sub

#End Region

#End Region


#Region "Formula Events"

    Private Sub formula_TB_Enter(sender As Object, e As EventArgs) Handles m_formulaTextBox.Enter

        If m_formulaEditionButton.Toggle = CheckState.Unchecked Then
            If Not m_currentNode Is Nothing Then
                Dim l_account As Account = GlobalVariables.Accounts.GetValue(CInt(m_currentNode.value))

                If l_account Is Nothing Then Exit Sub
                If m_controller.m_formulaTypesToBeTested.Contains(l_account.FormulaType) Then
                    m_formulaEditionButton.Toggle = CheckState.Checked
                Else
                    MsgBox(Local.GetValue("accounts_edition.msg_formula_not_editable"))
                    FormulaTypeComboBox.Focus()
                End If
            Else
                MsgBox(Local.GetValue("accounts_edition.msg_select_account"))
                m_formulaEditionButton.Toggle = CheckState.Unchecked
                m_accountTV.Focus()
                m_accountTV.SelectedNode = m_accountTV.Nodes(0)
            End If
        End If

    End Sub

    Private Sub formulaEdit_Click(sender As Object, e As EventArgs)

        If m_formulaEditionButton.Toggle = CheckState.Unchecked Then
            Dim confirm As Integer = MessageBox.Show(Local.GetValue("accounts_edition.msg_formula_validation_confirmation"), _
                                                     Local.GetValue("accounts_edition.title_formula_validation_confirmation"), MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm = DialogResult.Yes Then
                Submit_Formula_Click(sender, e)
            Else
                m_formulaTextBox.Text = m_controller.GetFormulaText(m_accountTV.SelectedNode.Value)
            End If
        Else
            m_formulaEditionButton.Toggle = CheckState.Unchecked
            formula_TB_Enter(sender, e)
        End If

    End Sub

    Private Sub formula_TB_Keydown(sender As Object, e As KeyEventArgs) Handles m_formulaTextBox.KeyDown

        Select Case e.KeyCode
            Case Keys.Escape
                Dim confirm As Integer = MessageBox.Show(Local.GetValue("accounts_edition.msg_formula_validation_confirmation"), _
                                                         Local.GetValue("accounts_edition.title_formula_validation_confirmation"), MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If confirm = DialogResult.Yes Then
                    Submit_Formula_Click(sender, e)
                Else
                    m_formulaTextBox.Text = m_controller.GetFormulaText(m_accountTV.SelectedNode.value)
                End If
                m_formulaEditionButton.Toggle = CheckState.Unchecked
            Case Keys.Enter
                Submit_Formula_Click(sender, e)
        End Select

    End Sub

    Private Sub AccountsTV_MouseDoubleClick(sender As Object, e As EventArgs)

        Dim node As vTreeNode = Me.m_accountTV.SelectedNode
        If node IsNot Nothing Then
            If m_formulaEditionButton.Toggle = CheckState.Checked Then
                m_formulaTextBox.Text = m_formulaTextBox.Text & FormulasTranslations.ACCOUNTS_HUMAN_IDENTIFIER & _
                                        node.Text & _
                                        FormulasTranslations.ACCOUNTS_HUMAN_IDENTIFIER
                m_formulaTextBox.Focus()
            End If
        End If

    End Sub

    Private Sub GlobalFactTV_NodeMouseDoubleClick(sender As Object, e As Windows.Forms.TreeNodeMouseClickEventArgs)

        Dim node As vTreeNode = VTreeViewUtil.GetNodeAtPosition(m_globalFactsTV, e.Location)
        If node IsNot Nothing Then
            If m_formulaEditionButton.Toggle = CheckState.Checked Then
                m_formulaTextBox.Text = m_formulaTextBox.Text & FormulasTranslations.FACTS_HUMAN_IDENTIFIER & _
                                  m_globalFactsTV.SelectedNode.Text & _
                                  FormulasTranslations.FACTS_HUMAN_IDENTIFIER
                m_formulaTextBox.Focus()
            End If
        End If

    End Sub


#Region "Drag and drop into formula TB"

    Private Sub formula_DragOver(sender As Object, e As DragEventArgs) Handles m_formulaTextBox.DragOver

        'Check that there is a TreeNode being dragged 
        If e.Data.GetDataPresent("VIBlend.WinForms.Controls.vTreeNode", True) = False Then Exit Sub

        'Get the TreeView raising the event (incase multiple on form)
        Dim selectedListBox As vTextBox = CType(sender, vTextBox)

        'Currently selected node is a suitable target
        e.Effect = DragDropEffects.Move

    End Sub

    Private Sub formula_DragDrop(sender As Object, e As DragEventArgs) Handles m_formulaTextBox.DragDrop

        'Check that there is a TreeNode being dragged
        If e.Data.GetDataPresent("VIBlend.WinForms.Controls.vTreeNode", True) = False Then Exit Sub

        'Get the TreeView raising the event (incase multiple on form)
        Dim selectedListBox As vTextBox = CType(sender, vTextBox)

        'Get the TreeNode being dragged
        Dim dropNode As vTreeNode = CType(e.Data.GetData("VIBlend.WinForms.Controls.vTreeNode"), vTreeNode)

        ' Add the node and childs node to the selected list view
        m_formulaTextBox.Text = m_formulaTextBox.Text & FormulasTranslations.ACCOUNTS_HUMAN_IDENTIFIER & _
                                dropNode.Text & _
                                FormulasTranslations.ACCOUNTS_HUMAN_IDENTIFIER
        m_formulaTextBox.Focus()

    End Sub

#End Region


#End Region


#Region "Accounts Attributes Events"

    Private Sub Name_TB_Validated(sender As Object, e As EventArgs) Handles Name_TB.Validated

        If Not IsNothing(m_currentNode) _
        AndAlso m_isDisplayingAttributes = False Then
            Dim newNameStr = Name_TB.Text

            If m_controller.AccountNameCheck(newNameStr) = True Then
                m_currentNode.Text = Name_TB.Text
                m_controller.UpdateName(m_currentNode.value, m_currentNode.Text)
            Else
                Name_TB.Text = m_currentNode.Text
            End If

        End If
    End Sub

    Private Sub Name_TB_KeyDown(sender As Object, e As KeyEventArgs) Handles Name_TB.KeyDown

        If e.KeyCode = Keys.Enter Then FormulaTypeComboBox.Focus()

    End Sub

    Private Sub formulaTypeCB_SelectedValueChanged(sender As Object, e As EventArgs) Handles FormulaTypeComboBox.SelectedItemChanged

        Dim li = FormulaTypeComboBox.SelectedItem
        If m_currentNode IsNot Nothing _
        AndAlso m_isDisplayingAttributes = False _
        AndAlso m_isRevertingFType = False Then

            If m_controller.FormulaTypeChangeImpliesFactsDeletion(CInt(m_currentNode.Value), li.Value) = True Then
                Dim confirm As GeneralUtilities.CheckResult = GeneralUtilities.AskPasswordConfirmation(Local.GetValue("accounts_edition.msg_password_required"), _
                                                                                                       Local.GetValue("accounts_edition.title_formula_type_validation_confirmation"))
                If confirm = GeneralUtilities.CheckResult.Success Then
                    UpdateFormulaType(m_currentNode.Value, li)
                Else
                    ' reverting formula type
                    m_isRevertingFType = True
                    Dim l_account As Account = GlobalVariables.Accounts.GetValue(CInt(m_currentNode.Value))
                    If Not l_account Is Nothing Then
                        FormulaTypeComboBox.SelectedValue = l_account.FormulaType
                    End If
                    m_isRevertingFType = False
                    If confirm = GeneralUtilities.CheckResult.Fail Then
                        MsgBox(Local.GetValue("accounts_edition.msg_password_confirmation_failed"))
                        Exit Sub
                    End If
                End If
            ElseIf m_controller.FormulaTypeChangeImpliesFormulaDeletion(CInt(m_currentNode.Value), li.Value) = True Then
                Dim confirm As Integer = MessageBox.Show(Local.GetValue("This implies deleting the account formula, do you confirm ?"), _
                                                         Local.GetValue("accounts_edition.title_formula_validation_confirmation"), _
                                                         MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If confirm = DialogResult.Yes Then
                    UpdateFormulaType(m_currentNode.Value, li)
                End If
            Else
                UpdateFormulaType(m_currentNode.Value, li)
            End If
        Else
            If li.Value = Account.FormulaTypes.TITLE Then
                CurrencyConversionComboBox.SelectedValue = Account.ConversionOptions.NO_CONVERSION
                TypeComboBox.Enabled = False
                CurrencyConversionComboBox.Enabled = False
                ConsolidationOptionComboBox.Enabled = False
            Else
                TypeComboBox.Enabled = True
                CurrencyConversionComboBox.Enabled = True
                ConsolidationOptionComboBox.Enabled = True
            End If
        End If

    End Sub

    Private Sub UpdateFormulaType(ByRef p_accountId As Int32, _
                                  ByRef p_li As ListItem)

        If p_li.Value = Account.FormulaTypes.TITLE Then
            TypeComboBox.Enabled = False
            CurrencyConversionComboBox.Enabled = False
            m_controller.UpdateAccountType(p_accountId, Account.AccountType.DATE_)
        End If
        m_controller.UpdateAccountFormulaType(p_accountId, p_li.Value)
        m_currentNode.ImageIndex = p_li.Value

    End Sub

    Private Sub TypeCB_SelectedItemChanged(sender As Object, e As EventArgs) Handles TypeComboBox.SelectedItemChanged

        Dim li = TypeComboBox.SelectedItem
        If Not IsNothing(m_currentNode) _
        AndAlso m_isDisplayingAttributes = False Then
            m_controller.UpdateAccountType(m_currentNode.value, li.Value)
        End If
        If li.Value = Account.AccountType.MONETARY Then
            CurrencyConversionComboBox.Enabled = True
            CurrencyConversionComboBox.SelectedValue = Account.ConversionOptions.AVERAGE_RATE
        Else
            CurrencyConversionComboBox.Enabled = False ' check if selected value <=> selected item
            CurrencyConversionComboBox.SelectedValue = Account.ConversionOptions.NO_CONVERSION
        End If

    End Sub

    Private Sub CurrencyConversionComboBox_SelectedItemChanged(sender As Object, e As EventArgs) Handles CurrencyConversionComboBox.SelectedItemChanged

        Dim li = CurrencyConversionComboBox.SelectedItem
        If Not IsNothing(m_currentNode) _
        AndAlso m_isDisplayingAttributes = False Then
            Select Case li.Value
                Case Account.ConversionOptions.AVERAGE_RATE, _
                     Account.ConversionOptions.END_OF_PERIOD_RATE
                    m_controller.UpdateAccountConversionOption(m_currentNode.value, li.Value)
            End Select
        End If

    End Sub

    Private Sub ConsolidationOptionComboBox_SelectedItemChanged(sender As Object, e As EventArgs) Handles ConsolidationOptionComboBox.SelectedItemChanged

        Dim li = ConsolidationOptionComboBox.SelectedItem
        If Not IsNothing(m_currentNode) _
        AndAlso m_isDisplayingAttributes = False Then
            m_controller.UpdateAccountConsolidationOption(m_currentNode.value, li.Value)
        End If

    End Sub

#End Region


#Region "Utilities"

    Private Sub DisplayAttributes()

        If Not IsNothing(m_currentNode) _
        AndAlso m_formulaEditionButton.Toggle = CheckState.Unchecked Then

            m_isDisplayingAttributes = True
            Dim account_id As Int32 = m_currentNode.value
            Dim l_account As Account = GlobalVariables.Accounts.GetValue(account_id)

            If l_account Is Nothing Then Exit Sub
            Name_TB.Text = m_currentNode.Text

            ' Formula Type ComboBox
            Dim formulaTypeLI = m_formulasTypesIdItemDict(l_account.FormulaType)
            FormulaTypeComboBox.SelectedItem = formulaTypeLI

            ' Format ComboBox
            If m_formatsIdItemDict.ContainsKey(l_account.Type) Then
                Dim formatLI = m_formatsIdItemDict(l_account.Type)
                TypeComboBox.SelectedItem = formatLI
                If formatLI.Value = Account.AccountType.MONETARY Then
                    ' Currency Conversion
                    Dim conversionLI = m_currenciesConversionIdItemDict(l_account.ConversionOptionId)
                    CurrencyConversionComboBox.SelectedItem = conversionLI
                End If
            Else
                TypeComboBox.SelectedItem = Nothing
                TypeComboBox.Text = ""
            End If

            ' Consolidation Option
            Dim consolidationLI = m_consoOptionIdItemDict(l_account.ConsolidationOptionId)
            ConsolidationOptionComboBox.SelectedItem = consolidationLI


            ' Formula TB
            If m_controller.m_formulaTypesToBeTested.Contains(l_account.FormulaType) Then
                m_formulaTextBox.Text = m_controller.GetFormulaText(account_id)
            Else
                m_formulaTextBox.Text = ""
            End If

            m_descriptionTextBox.Text = l_account.Description

            Dim l_isRootAccount As Boolean = False
            If m_currentNode.Parent Is Nothing Then l_isRootAccount = True
            If formulaTypeLI.Value = Account.FormulaTypes.TITLE Then
                SetEnableStatusEdition(False, l_isRootAccount)
            Else
                SetEnableStatusEdition(True, l_isRootAccount)
            End If
            m_isDisplayingAttributes = False
        End If

    End Sub

    Private Sub SetEnableStatusEdition(ByRef p_status As Boolean, _
                                       ByRef p_isRootAccount As Boolean)

        If p_isRootAccount = True Then
            FormulaTypeComboBox.Enabled = p_status
        End If
        TypeComboBox.Enabled = p_status
        CurrencyConversionComboBox.Enabled = p_status
        ConsolidationOptionComboBox.Enabled = p_status
        m_formulaTextBox.Enabled = p_status

    End Sub

#End Region


  
End Class
