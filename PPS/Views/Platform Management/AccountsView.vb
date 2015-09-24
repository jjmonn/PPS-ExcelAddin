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
' Last modified: 02/09/2015
' Author: Julien Monnereau


Imports Microsoft.Office.Interop
Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports VIBlend.WinForms.Controls



Friend Class AccountsView


#Region "Instance Variables"

    ' Objects
    Friend m_controller As AccountsController
    Friend m_accountTV As TreeView
    Friend m_globalFactsTV As TreeView
    Friend m_currentNode As TreeNode

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
                   ByRef p_inputAccountTV As TreeView, ByRef p_inputGlobalFactsTv As TreeView)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_controller = p_inputController
        m_accountTV = p_inputAccountTV
        m_globalFactsTV = p_inputGlobalFactsTv
        AccountsTVInit()
        GlobalFactsTVInit()
        ComboBoxesInit()

    End Sub

    Private Sub TVInit(ByRef p_tv As TreeView)
        p_tv.ContextMenuStrip = TVRCM
        p_tv.Dock = DockStyle.Fill
        p_tv.ImageList = accountsIL
        p_tv.AllowDrop = True
        p_tv.LabelEdit = False
        p_tv.CollapseAll()
    End Sub

    Private Sub AccountsTVInit()

        TVInit(m_accountTV)
        AccountsTVPanel.Controls.Add(m_accountTV)

        AddHandler m_accountTV.AfterSelect, AddressOf AccountsTV_AfterSelect
        AddHandler m_accountTV.KeyDown, AddressOf AccountsTV_KeyDown
        AddHandler m_accountTV.NodeMouseClick, AddressOf AccountsTV_NodeMouseClick
        AddHandler m_accountTV.NodeMouseDoubleClick, AddressOf AccountsTV_NodeMouseDoubleClick
        AddHandler m_accountTV.DragDrop, AddressOf AccountsTV_DragDrop
        AddHandler m_accountTV.DragOver, AddressOf AccountsTV_DragOver
        AddHandler m_accountTV.ItemDrag, AddressOf AccountsTV_ItemDrag
        AddHandler m_accountTV.DragEnter, AddressOf AccountsTV_DragEnter
        AddHandler m_globalFactsTV.NodeMouseDoubleClick, AddressOf GlobalFactTV_NodeMouseDoubleClick

    End Sub

    Private Sub GlobalFactsTVInit()

        TVInit(m_globalFactsTV)
        GlobalFactsPanel.Controls.Add(m_globalFactsTV)

    End Sub

    Private Sub ComboBoxesInit()

        ' Formulas 
        Dim InputListItem As New ListItem
        InputListItem.Text = "Input"
        InputListItem.Value = GlobalEnums.FormulaTypes.HARD_VALUE_INPUT
        FormulaTypeComboBox.Items.Add(InputListItem)
        m_formulasTypesIdItemDict.Add(InputListItem.Value, InputListItem)

        Dim FormulaListItem As New ListItem
        FormulaListItem.Text = "Formula"
        FormulaListItem.Value = GlobalEnums.FormulaTypes.FORMULA
        FormulaTypeComboBox.Items.Add(FormulaListItem)
        m_formulasTypesIdItemDict.Add(FormulaListItem.Value, FormulaListItem)

        Dim AggregationListItem As New ListItem
        AggregationListItem.Text = "Aggregation of Sub Accounts"
        AggregationListItem.Value = GlobalEnums.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS
        FormulaTypeComboBox.Items.Add(AggregationListItem)
        m_formulasTypesIdItemDict.Add(AggregationListItem.Value, AggregationListItem)

        Dim FirstPeriodInputListItem As New ListItem
        FirstPeriodInputListItem.Text = "First Period Input"
        FirstPeriodInputListItem.Value = GlobalEnums.FormulaTypes.FIRST_PERIOD_INPUT
        FormulaTypeComboBox.Items.Add(FirstPeriodInputListItem)
        m_formulasTypesIdItemDict.Add(FirstPeriodInputListItem.Value, FirstPeriodInputListItem)

        Dim TitleListItem As New ListItem
        TitleListItem.Text = "Title"
        TitleListItem.Value = GlobalEnums.FormulaTypes.TITLE
        FormulaTypeComboBox.Items.Add(TitleListItem)
        m_formulasTypesIdItemDict.Add(TitleListItem.Value, TitleListItem)

        ' Type
        Dim MonetaryFormatLI As New ListItem
        MonetaryFormatLI.Text = "Monetary"
        MonetaryFormatLI.Value = GlobalEnums.AccountType.MONETARY
        TypeComboBox.Items.Add(MonetaryFormatLI)
        m_formatsIdItemDict.Add(MonetaryFormatLI.Value, MonetaryFormatLI)

        Dim NormalFormatLI As New ListItem
        NormalFormatLI.Text = "Number"
        NormalFormatLI.Value = GlobalEnums.AccountType.NUMBER
        TypeComboBox.Items.Add(NormalFormatLI)
        m_formatsIdItemDict.Add(NormalFormatLI.Value, NormalFormatLI)

        Dim percentageFormatLI As New ListItem
        percentageFormatLI.Text = "Percentage"
        percentageFormatLI.Value = GlobalEnums.AccountType.PERCENTAGE
        TypeComboBox.Items.Add(percentageFormatLI)
        m_formatsIdItemDict.Add(percentageFormatLI.Value, percentageFormatLI)

        Dim DateFormatLI As New ListItem
        DateFormatLI.Text = "Date"
        DateFormatLI.Value = GlobalEnums.AccountType.DATE_
        TypeComboBox.Items.Add(DateFormatLI)
        m_formatsIdItemDict.Add(DateFormatLI.Value, DateFormatLI)


        ' Currencies Conversion
        Dim NoConversionLI As New ListItem
        NoConversionLI.Text = "Non Converted"
        NoConversionLI.Value = GlobalEnums.ConversionOptions.NO_CONVERSION
        CurrencyConversionComboBox.Items.Add(NoConversionLI)
        m_currenciesConversionIdItemDict.Add(NoConversionLI.Value, NoConversionLI)

        Dim AverageRateLI As New ListItem
        AverageRateLI.Text = "Average Exchange Rate"
        AverageRateLI.Value = GlobalEnums.ConversionOptions.AVERAGE_RATE
        CurrencyConversionComboBox.Items.Add(AverageRateLI)
        m_currenciesConversionIdItemDict.Add(AverageRateLI.Value, AverageRateLI)

        Dim EndOfPeriodRateLI As New ListItem
        EndOfPeriodRateLI.Text = "End of Period Exchange Rate"
        EndOfPeriodRateLI.Value = GlobalEnums.ConversionOptions.END_OF_PERIOD_RATE
        CurrencyConversionComboBox.Items.Add(EndOfPeriodRateLI)
        m_currenciesConversionIdItemDict.Add(EndOfPeriodRateLI.Value, EndOfPeriodRateLI)


        ' Recomputation Option
        Dim AggregatedLI As New ListItem
        AggregatedLI.Text = "Aggregated"
        AggregatedLI.Value = GlobalEnums.ConsolidationOptions.AGGREGATION
        ConsolidationOptionComboBox.Items.Add(AggregatedLI)
        m_consoOptionIdItemDict.Add(AggregatedLI.Value, AggregatedLI)

        Dim RecomputedLI As New ListItem
        RecomputedLI.Text = "Recomputed"
        RecomputedLI.Value = GlobalEnums.ConsolidationOptions.RECOMPUTATION
        ConsolidationOptionComboBox.Items.Add(RecomputedLI)
        m_consoOptionIdItemDict.Add(RecomputedLI.Value, RecomputedLI)

    End Sub



#End Region


#Region "Interface"


    Delegate Sub TVUpdate_Delegate(ByRef account_id As Int32, _
                                   ByRef account_parent_id As Int32, _
                                   ByRef account_name As String, _
                                   ByRef account_image As Int32)
    Friend Sub TVUpdate(ByRef account_id As Int32, _
                        ByRef account_parent_id As Int32, _
                        ByRef account_name As String, _
                        ByRef account_image As Int32)

        If InvokeRequired Then
            Dim MyDelegate As New TVUpdate_Delegate(AddressOf TVUpdate)
            Me.Invoke(MyDelegate, New Object() {account_id, account_parent_id, account_name, account_image})
        Else
            If account_parent_id = 0 Then
                Dim new_node As TreeNode = m_accountTV.Nodes.Add(CStr(account_id), account_name, account_image, account_image)
                new_node.EnsureVisible()
            Else
                Dim accounts() As TreeNode = m_accountTV.Nodes.Find(account_parent_id, True)
                If accounts.Length = 1 Then
                    Dim new_node As TreeNode = accounts(0).Nodes.Add(CStr(account_id), account_name, account_image, account_image)
                    new_node.EnsureVisible()
                End If
            End If
            m_accountTV.Refresh()
        End If

    End Sub

    Delegate Sub TVNodeDelete_Delegate(ByRef account_id As Int32)
    Friend Sub TVNodeDelete(ByRef account_id As Int32)

        If InvokeRequired Then
            Dim MyDelegate As New TVNodeDelete_Delegate(AddressOf TVNodeDelete)
            Me.Invoke(MyDelegate, New Object() {account_id})
        Else
            Dim accountsNodes() As TreeNode = m_accountTV.Nodes.Find(account_id, True)
            If accountsNodes.Length = 1 Then
                accountsNodes(0).Remove()
                m_accountTV.Refresh()
            End If
        End If

    End Sub


#End Region


#Region "Call backs"

    Private Sub newAccountBT_Click(sender As Object, e As EventArgs) Handles CreateANewAccountToolStripMenuItem.Click, _
                                                                             AddSubAccountToolStripMenuItem.Click

        formulaEdit.Checked = False
        If Not m_currentNode Is Nothing Then
            m_controller.DisplayNewAccountView(m_currentNode)
        Else
            MsgBox("Please Select Parent Account first.")
        End If

    End Sub

    Private Sub newCategoryBT_Click(sender As Object, e As EventArgs) Handles CreateANewCategoryToolStripMenuItem.Click, _
                                                                              AddCategoryToolStripMenuItem.Click

        formulaEdit.Checked = False
        Dim newCategoryName As String = InputBox("Name of the New Tab: ", "Account Tab Creation", "")
        If newCategoryName <> "" Then

            If m_controller.AccountNameCheck(newCategoryName) = True Then
                m_controller.CreateAccount(0, _
                                         newCategoryName, _
                                         GlobalEnums.FormulaTypes.TITLE, _
                                         "", _
                                         GlobalEnums.AccountType.DATE_, _
                                         1, _
                                         1, _
                                         TITLE_FORMAT_CODE, _
                                         GlobalEnums.FormulaTypes.TITLE, _
                                         1, _
                                         m_accountTV.Nodes.Count)
            End If
        End If

    End Sub

    Private Sub Submit_Formula_Click(sender As Object, e As EventArgs) Handles submit_cmd.Click

        Dim formulastr As String = formula_TB.Text
        If formulastr = "" Then
            Dim confirm2 As Integer = MessageBox.Show("The formula is empty, do you want to save it?", _
                                                      "Formula validation", _
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
            MsgBox("The following items are not mapped accounts or invalid items in a formula: " + Chr(13) & errorsStr)
            Exit Sub
        Else
            GoTo DependanciesCheck
        End If

DependanciesCheck:
        If m_controller.InterdependancyTest = True Then
            GoTo SubmitFormula
        Else
            Exit Sub
        End If

SubmitFormula:
        Dim confirm As Integer = MessageBox.Show("You are about to update the formula for Account: " + Chr(13) + Name_TB.Text + Chr(13) + "Do you confirm?", _
                                                 "DataBase submission confirmation", _
                                                  MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
        If confirm = DialogResult.Yes Then
            If m_controller.accountsNameKeysDictionary.ContainsKey(Name_TB.Text) Then
                formulaEdit.Checked = False
                Dim accountId As Int32 = m_controller.accountsNameKeysDictionary.Item(Name_TB.Text)
                m_controller.UpdateAccount(accountId, ACCOUNT_FORMULA_VARIABLE, m_controller.GetCurrentParsedFormula)
            End If
        End If

    End Sub

    Private Sub B_DeleteAccount_Click(sender As Object, e As EventArgs) Handles DeleteAccountToolStripMenuItem.Click, _
                                                                                DeleteAccountToolStripMenuItem1.Click

        If Not m_currentNode Is Nothing Then
            formulaEdit.Checked = False
            Dim dependantAccountslist() As String = m_controller.ExistingDependantAccounts(m_currentNode)
            If dependantAccountslist.Length > 0 Then

                Dim listStr As String = ""
                For Each accountName In dependantAccountslist
                    listStr = listStr & " - " & accountName & Chr(13)
                Next
                MsgBox("The following Accounts formula are dependant on some accounts you want to delete: " & Chr(13) & _
                       listStr & Chr(13) & _
                       "Those formulas must first be changed before the Selected Accounts can be deleted.")
                Exit Sub
            End If

            Dim confirm As Integer = MessageBox.Show("Careful, you are about to delete account " + Chr(13) + Chr(13) + Name_TB.Text + Chr(13) + "Do you confirm?" + Chr(13) + Chr(13) + _
                                                     "This Account and all its Sub Accounts will be deleted.", _
                                                     "Account deletion confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm = DialogResult.Yes Then
                m_controller.DeleteAccount(CInt(m_currentNode.Name))
                m_currentNode = Nothing
            End If
        End If

    End Sub

    Private Sub BDropToWS_Click(sender As Object, e As EventArgs) Handles DropAllAccountsHierarchyToExcelToolStripMenuItem.Click, _
                                                                          DropHierarchyToExcelToolStripMenuItem.Click

        ' !! to be reimplemented ? !!
        formulaEdit.Checked = False
        Dim ActiveWS As Excel.Worksheet = GlobalVariables.APPS.ActiveSheet
        Dim RNG As Excel.Range = GlobalVariables.APPS.Application.ActiveCell
        Dim Response As MsgBoxResult

        If IsNothing(RNG) Then
            MsgBox("A destination cell must be selected in order to drop the Accounts on the Worksheet")
            Exit Sub
        Else
            Response = MsgBox("The Accounts will be dropped into cell" + RNG.Address, MsgBoxStyle.OkCancel)
            If Response = MsgBoxResult.Ok Then
                ' Launch Accounts Drop
                WorksheetWrittingFunctions.WriteAccountsFromTreeView(m_accountTV, RNG)
            ElseIf Response = MsgBoxResult.Cancel Then
                Exit Sub
            End If
        End If
    End Sub

    Private Sub SaveDescriptionBT_Click(sender As Object, e As EventArgs) Handles SaveDescriptionBT.Click



    End Sub

#End Region


#Region "Treeview Events"

    Private Sub AccountsTV_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs)

        If m_dragAndDrop = False Then
            If formulaEdit.Checked = False Then m_currentNode = e.Node
        End If

    End Sub

    Private Sub AccountsTV_AfterSelect(sender As Object, e As TreeViewEventArgs)

        If m_dragAndDrop = False Then
            If formulaEdit.Checked = False Then
                m_currentNode = e.Node
                DisplayAttributes()
            End If
        End If

    End Sub

    Private Sub AccountsTV_KeyDown(sender As Object, e As KeyEventArgs)

        If m_dragAndDrop = False Then
            Select Case e.KeyCode
                Case Keys.Delete : B_DeleteAccount_Click(sender, e)

                Case Keys.Up
                    If e.Control Then
                        TreeViewsUtilities.MoveNodeUp(m_accountTV.SelectedNode)
                    End If
                Case Keys.Down
                    If e.Control Then
                        TreeViewsUtilities.MoveNodeDown(m_accountTV.SelectedNode)
                    End If
            End Select
        End If

    End Sub

#Region "Nodes Drag and Drop Procedure"

    Private Sub AccountsTV_ItemDrag(sender As Object, e As ItemDragEventArgs)

        DoDragDrop(e.Item, Windows.Forms.DragDropEffects.Move)


    End Sub

    Private Sub AccountsTV_DragEnter(sender As Object, e As Windows.Forms.DragEventArgs)

        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) Then
            e.Effect = DragDropEffects.Move
            m_dragAndDrop = True
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub AccountsTV_DragOver(sender As Object, e As Windows.Forms.DragEventArgs)

        If formulaEdit.Checked = False Then

            If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub
            Dim selectedTreeview As Windows.Forms.TreeView = CType(sender, Windows.Forms.TreeView)
            Dim pt As Drawing.Point = CType(sender, TreeView).PointToClient(New Drawing.Point(e.X, e.Y))
            Dim targetNode As TreeNode = selectedTreeview.GetNodeAt(pt)

            'See if the targetNode is currently selected, if so no need to validate again
            If Not (selectedTreeview.SelectedNode Is targetNode) Then       'Select the node currently under the cursor
                selectedTreeview.SelectedNode = targetNode

                'Check that the selected node is not the dropNode and also that it is not a child of the dropNode and therefore an invalid target
                Dim dropNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

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
        End If

    End Sub

    Private Sub AccountsTV_DragDrop(sender As Object, e As Windows.Forms.DragEventArgs)

        If formulaEdit.Checked = False Then
            'Check that there is a TreeNode being dragged
            If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub

            Dim selectedTreeview As TreeView = CType(sender, Windows.Forms.TreeView)

            'Get the TreeNode being dragged
            Dim dropNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

            'The target node should be selected from the DragOver event
            Dim targetNode As TreeNode = selectedTreeview.SelectedNode

            If targetNode Is Nothing Then
                selectedTreeview.Nodes.Add(dropNode)
            Else
                If Not targetNode Is dropNode _
                AndAlso TreeViewsUtilities.GetNodesKeysList(dropNode).Contains(targetNode.Name) = False Then

                    dropNode.Remove()                                               'Remove the drop node from its current location
                    targetNode.Nodes.Add(dropNode)

                Else
                    e.Effect = DragDropEffects.None
                    m_dragAndDrop = False
                    Exit Sub
                End If
            End If

            m_dragAndDrop = False
            dropNode.EnsureVisible()                                        ' Ensure the newley created node is visible to the user and 
            selectedTreeview.SelectedNode = dropNode                        ' Select it
            Dim tmpHT As New Hashtable
            tmpHT.Add(ACCOUNT_TAB_VARIABLE, TreeViewsUtilities.ReturnRootNodeFromNode(dropNode).Index)
            tmpHT.Add(PARENT_ID_VARIABLE, targetNode.Name)
            m_controller.UpdateAccount(dropNode.Name, tmpHT)

        End If


    End Sub

#End Region

#End Region


#Region "Formula Events"

    Private Sub formula_TB_Enter(sender As Object, e As EventArgs) Handles formula_TB.Enter

        If formulaEdit.Checked = False Then
            If Not m_currentNode Is Nothing Then
                If m_controller.FTypesToBeTested.Contains(m_controller.ReadAccount(m_currentNode.Name, ACCOUNT_FORMULA_TYPE_VARIABLE)) Then
                    formulaEdit.Checked = True
                Else
                    MsgBox("This Account has no editable Formula. Please change the Formula Type or select another accounts " _
                           + "in order to edit the formula.")
                    FormulaTypeComboBox.Focus()
                End If
            Else
                MsgBox("An Accounts must be selected in order to edit its formula.")
                formulaEdit.Checked = False
                m_accountTV.Focus()
                m_accountTV.SelectedNode = m_accountTV.Nodes(0)
            End If
        End If

    End Sub

    Private Sub formulaEdit_Click(sender As Object, e As EventArgs) Handles formulaEdit.Click

        If formulaEdit.Checked = False Then
            Dim confirm As Integer = MessageBox.Show("Do you want to save the formula?", _
                                                     "Formula Edition Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm = DialogResult.Yes Then
                Submit_Formula_Click(sender, e)
            End If
        Else
            formulaEdit.Checked = False
            formula_TB_Enter(sender, e)
        End If

    End Sub

    Private Sub formula_TB_Keydown(sender As Object, e As KeyEventArgs) Handles formula_TB.KeyDown

        Select Case e.KeyCode
            Case Keys.Escape
                Dim confirm As Integer = MessageBox.Show("You are about to ext the Formula Editor. Do you want to save the formula?", _
                                         "Formula Edition Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If confirm = DialogResult.Yes Then
                    Submit_Formula_Click(sender, e)
                End If
                formulaEdit.Checked = False
            Case Keys.Enter
                Submit_Formula_Click(sender, e)
        End Select

    End Sub

    Private Sub AccountsTV_NodeMouseDoubleClick(sender As Object, e As Windows.Forms.TreeNodeMouseClickEventArgs)

        If formulaEdit.Checked = True Then
            formula_TB.Text = formula_TB.Text & FormulasTranslations.ACCOUNTS_HUMAN_IDENTIFIER & _
                              m_accountTV.SelectedNode.Text & _
                              FormulasTranslations.ACCOUNTS_HUMAN_IDENTIFIER
            formula_TB.Focus()
        End If

    End Sub

    Private Sub GlobalFactTV_NodeMouseDoubleClick(sender As Object, e As Windows.Forms.TreeNodeMouseClickEventArgs)
        If formulaEdit.Checked = True Then
            formula_TB.Text = formula_TB.Text & FormulasTranslations.FACTS_HUMAN_IDENTIFIER & _
                              m_globalFactsTV.SelectedNode.Text & _
                              FormulasTranslations.FACTS_HUMAN_IDENTIFIER
            formula_TB.Focus()
        End If
    End Sub


#Region "Drag and drop into formula TB"

    Private Sub formula_DragOver(sender As Object, e As DragEventArgs) Handles formula_TB.DragOver

        'Check that there is a TreeNode being dragged 
        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub

        'Get the TreeView raising the event (incase multiple on form)
        Dim selectedListBox As TextBox = CType(sender, TextBox)

        'Currently selected node is a suitable target
        e.Effect = DragDropEffects.Move

    End Sub

    Private Sub formula_DragDrop(sender As Object, e As DragEventArgs) Handles formula_TB.DragDrop

        'Check that there is a TreeNode being dragged
        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub

        'Get the TreeView raising the event (incase multiple on form)
        Dim selectedListBox As TextBox = CType(sender, TextBox)

        'Get the TreeNode being dragged
        Dim dropNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

        ' Add the node and childs node to the selected list view
        formula_TB.Text = formula_TB.Text & FormulasTranslations.ACCOUNTS_HUMAN_IDENTIFIER & _
                          m_accountTV.SelectedNode.Text & _
                          FormulasTranslations.ACCOUNTS_HUMAN_IDENTIFIER
        formula_TB.Focus()

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
                m_controller.UpdateName(m_currentNode.Name, m_currentNode.Text)
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
        If Not IsNothing(m_currentNode) _
        AndAlso m_isDisplayingAttributes = False _
        AndAlso m_isRevertingFType = False Then
            If m_controller.FormulaTypeChangeImpliesFactsDeletion(CInt(m_currentNode.Name), li.Value) = True Then
                Dim confirm As Integer = MessageBox.Show("Changing the Formula Type of this account may imply the loss of inputs, do you confirm you want to convert this account into a formula?", _
                                                         "Formula Type Upadte Confirmation", _
                                                         MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If confirm = DialogResult.Yes Then
                    GoTo UdpateFormulaType
                Else
                    m_isRevertingFType = True
                    FormulaTypeComboBox.SelectedValue = m_controller.ReadAccount(m_currentNode.Name, ACCOUNT_FORMULA_TYPE_VARIABLE)
                    m_isRevertingFType = False
                    Exit Sub
                End If
            Else
                GoTo UdpateFormulaType
            End If
        End If

        ' below -> not clean -> must happen after update'
        ' at display time priority high

        If li.Value = GlobalEnums.FormulaTypes.TITLE Then
            CurrencyConversionComboBox.SelectedValue = GlobalEnums.ConversionOptions.NO_CONVERSION
            TypeComboBox.Enabled = False
            CurrencyConversionComboBox.Enabled = False
            ConsolidationOptionComboBox.Enabled = False
        Else
            TypeComboBox.Enabled = True
            CurrencyConversionComboBox.Enabled = True
            ConsolidationOptionComboBox.Enabled = True
        End If
        Exit Sub

UdpateFormulaType:
        m_controller.UpdateAccount(m_currentNode.Name, ACCOUNT_FORMULA_TYPE_VARIABLE, li.Value)

    End Sub

    Private Sub TypeCB_SelectedItemChanged(sender As Object, e As EventArgs) Handles TypeComboBox.SelectedItemChanged

        Dim li = TypeComboBox.SelectedItem
        If Not IsNothing(m_currentNode) _
        AndAlso m_isDisplayingAttributes = False Then
            m_controller.UpdateAccount(m_currentNode.Name, ACCOUNT_TYPE_VARIABLE, li.Value)
        End If
        If li.Value = GlobalEnums.AccountType.MONETARY Then
            CurrencyConversionComboBox.Enabled = True
            CurrencyConversionComboBox.SelectedValue = GlobalEnums.ConversionOptions.AVERAGE_RATE
        Else
            CurrencyConversionComboBox.Enabled = False ' check if selected value <=> selected item
            CurrencyConversionComboBox.SelectedValue = GlobalEnums.ConversionOptions.NO_CONVERSION
        End If

    End Sub

    Private Sub CurrencyConversionComboBox_SelectedItemChanged(sender As Object, e As EventArgs) Handles CurrencyConversionComboBox.SelectedItemChanged

        Dim li = CurrencyConversionComboBox.SelectedItem
        If Not IsNothing(m_currentNode) _
        AndAlso m_isDisplayingAttributes = False Then
            Select Case li.Value
                Case GlobalEnums.ConversionOptions.AVERAGE_RATE, _
                     GlobalEnums.ConversionOptions.END_OF_PERIOD_RATE
                    m_controller.UpdateAccount(m_currentNode.Name, ACCOUNT_CONVERSION_OPTION_VARIABLE, li.Value)
            End Select
        End If

    End Sub

    Private Sub ConsolidationOptionComboBox_SelectedItemChanged(sender As Object, e As EventArgs) Handles ConsolidationOptionComboBox.SelectedItemChanged

        Dim li = ConsolidationOptionComboBox.SelectedItem
        If Not IsNothing(m_currentNode) _
        AndAlso m_isDisplayingAttributes = False Then
            m_controller.UpdateAccount(m_currentNode.Name, ACCOUNT_CONSOLIDATION_OPTION_VARIABLE, li.Value)
        End If

    End Sub

#End Region


#Region "Utilities"

    Private Sub DisplayAttributes()

        If Not IsNothing(m_currentNode) _
        AndAlso formulaEdit.Checked = False Then

            m_isDisplayingAttributes = True
            Dim account_id As Int32 = m_currentNode.Name
            Name_TB.Text = m_currentNode.Text

            ' Formula Type ComboBox
            Dim formulaTypeLI = m_formulasTypesIdItemDict(m_controller.ReadAccount(account_id, ACCOUNT_FORMULA_TYPE_VARIABLE))
            FormulaTypeComboBox.SelectedItem = formulaTypeLI

            If formulaTypeLI.Value = GlobalEnums.FormulaTypes.TITLE Then
                SetEnableStatusEdition(False)
            Else
                SetEnableStatusEdition(True)
            End If

            ' Format ComboBox
            Dim formatLI = m_formatsIdItemDict(m_controller.ReadAccount(account_id, ACCOUNT_TYPE_VARIABLE))
            TypeComboBox.SelectedItem = formatLI

            If formatLI.Value = GlobalEnums.AccountType.MONETARY Then
                ' Currency Conversion
                Dim conversionLI = m_currenciesConversionIdItemDict(m_controller.ReadAccount(account_id, ACCOUNT_CONVERSION_OPTION_VARIABLE))
                CurrencyConversionComboBox.SelectedItem = conversionLI

            End If

            ' Consolidation Option
            Dim consolidationLI = m_consoOptionIdItemDict(m_controller.ReadAccount(account_id, ACCOUNT_CONSOLIDATION_OPTION_VARIABLE))
            ConsolidationOptionComboBox.SelectedItem = consolidationLI


            ' Formula TB
            If m_controller.FTypesToBeTested.Contains(m_controller.ReadAccount(account_id, ACCOUNT_FORMULA_TYPE_VARIABLE)) Then
                formula_TB.Text = m_controller.GetFormulaText(account_id)
            Else
                formula_TB.Text = ""
            End If
            m_isDisplayingAttributes = False

        End If

    End Sub

    Private Sub SetEnableStatusEdition(ByRef status As Boolean)

        FormulaTypeComboBox.Enabled = status
        TypeComboBox.Enabled = status
        CurrencyConversionComboBox.Enabled = status
        ConsolidationOptionComboBox.Enabled = status
        formula_TB.Enabled = status

    End Sub


#End Region

End Class
