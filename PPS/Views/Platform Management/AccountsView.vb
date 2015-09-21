﻿' AccountsMGT_UI
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
    Friend Controller As AccountsController
    Friend AccountsTV As TreeView
    Friend current_node As TreeNode

    ' Variables
    Private formulasTypesIdItemDict As New Dictionary(Of Int32, ListItem)
    Private formatsIdItemDict As New Dictionary(Of Int32, ListItem)
    Private currenciesConversionIdItemDict As New Dictionary(Of Int32, ListItem)
    Private consoOptionIdItemDict As New Dictionary(Of Int32, ListItem)
    Private isRevertingFType As Boolean = False
    Private isDisplayingAttributes As Boolean
    Private drag_and_drop As Boolean = False

    ' Constants
    Private Const MARGIN_SIZE As Integer = 15
    Private Const ACCCOUNTS_TV_MAX_WIDTH As Integer = 600
    Private Const MARGIN1 As Integer = 30


#End Region


#Region "Initialization"

    Friend Sub New(ByRef input_controller As AccountsController, _
                   ByRef input_accountTV As TreeView)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Controller = input_controller
        AccountsTV = input_accountTV
        AccountsTVInit()
        ComboBoxesInit()

    End Sub

    Private Sub AccountsTVInit()


        AccountsTVPanel.Controls.Add(AccountsTV)
        AccountsTV.ContextMenuStrip = TVRCM
        AccountsTV.Dock = DockStyle.Fill
        AccountsTV.ImageList = accountsIL
        AccountsTV.AllowDrop = True
        AccountsTV.LabelEdit = False
        AccountsTV.CollapseAll()

        AddHandler AccountsTV.AfterSelect, AddressOf AccountsTV_AfterSelect
        AddHandler AccountsTV.KeyDown, AddressOf AccountsTV_KeyDown
        AddHandler AccountsTV.NodeMouseClick, AddressOf AccountsTV_NodeMouseClick
        AddHandler AccountsTV.NodeMouseDoubleClick, AddressOf AccountsTV_NodeMouseDoubleClick
        AddHandler AccountsTV.DragDrop, AddressOf AccountsTV_DragDrop
        AddHandler AccountsTV.DragOver, AddressOf AccountsTV_DragOver
        AddHandler AccountsTV.ItemDrag, AddressOf AccountsTV_ItemDrag
        AddHandler AccountsTV.DragEnter, AddressOf AccountsTV_DragEnter

    End Sub

    Private Sub ComboBoxesInit()

        ' Formulas 
        Dim InputListItem As New ListItem
        InputListItem.Text = "Input"
        InputListItem.Value = GlobalEnums.FormulaTypes.HARD_VALUE_INPUT
        FormulaTypeComboBox.Items.Add(InputListItem)
        formulasTypesIdItemDict.Add(InputListItem.Value, InputListItem)

        Dim FormulaListItem As New ListItem
        FormulaListItem.Text = "Formula"
        FormulaListItem.Value = GlobalEnums.FormulaTypes.FORMULA
        FormulaTypeComboBox.Items.Add(FormulaListItem)
        formulasTypesIdItemDict.Add(FormulaListItem.Value, FormulaListItem)

        Dim AggregationListItem As New ListItem
        AggregationListItem.Text = "Aggregation of Sub Accounts"
        AggregationListItem.Value = GlobalEnums.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS
        FormulaTypeComboBox.Items.Add(AggregationListItem)
        formulasTypesIdItemDict.Add(AggregationListItem.Value, AggregationListItem)

        Dim FirstPeriodInputListItem As New ListItem
        FirstPeriodInputListItem.Text = "First Period Input"
        FirstPeriodInputListItem.Value = GlobalEnums.FormulaTypes.FIRST_PERIOD_INPUT
        FormulaTypeComboBox.Items.Add(FirstPeriodInputListItem)
        formulasTypesIdItemDict.Add(FirstPeriodInputListItem.Value, FirstPeriodInputListItem)

        Dim TitleListItem As New ListItem
        TitleListItem.Text = "Title"
        TitleListItem.Value = GlobalEnums.FormulaTypes.TITLE
        FormulaTypeComboBox.Items.Add(TitleListItem)
        formulasTypesIdItemDict.Add(TitleListItem.Value, TitleListItem)

        ' Type
        Dim MonetaryFormatLI As New ListItem
        MonetaryFormatLI.Text = "Monetary"
        MonetaryFormatLI.Value = GlobalEnums.AccountType.MONETARY
        TypeComboBox.Items.Add(MonetaryFormatLI)
        formatsIdItemDict.Add(MonetaryFormatLI.Value, MonetaryFormatLI)

        Dim NormalFormatLI As New ListItem
        NormalFormatLI.Text = "Number"
        NormalFormatLI.Value = GlobalEnums.AccountType.NUMBER
        TypeComboBox.Items.Add(NormalFormatLI)
        formatsIdItemDict.Add(NormalFormatLI.Value, NormalFormatLI)

        Dim percentageFormatLI As New ListItem
        percentageFormatLI.Text = "Percentage"
        percentageFormatLI.Value = GlobalEnums.AccountType.PERCENTAGE
        TypeComboBox.Items.Add(percentageFormatLI)
        formatsIdItemDict.Add(percentageFormatLI.Value, percentageFormatLI)

        Dim DateFormatLI As New ListItem
        DateFormatLI.Text = "Date"
        DateFormatLI.Value = GlobalEnums.AccountType.DATE_
        TypeComboBox.Items.Add(DateFormatLI)
        formatsIdItemDict.Add(DateFormatLI.Value, DateFormatLI)


        ' Currencies Conversion
        Dim NoConversionLI As New ListItem
        NoConversionLI.Text = "Non Converted"
        NoConversionLI.Value = GlobalEnums.ConversionOptions.NO_CONVERSION
        CurrencyConversionComboBox.Items.Add(NoConversionLI)
        currenciesConversionIdItemDict.Add(NoConversionLI.Value, NoConversionLI)

        Dim AverageRateLI As New ListItem
        AverageRateLI.Text = "Average Exchange Rate"
        AverageRateLI.Value = GlobalEnums.ConversionOptions.AVERAGE_RATE
        CurrencyConversionComboBox.Items.Add(AverageRateLI)
        currenciesConversionIdItemDict.Add(AverageRateLI.Value, AverageRateLI)

        Dim EndOfPeriodRateLI As New ListItem
        EndOfPeriodRateLI.Text = "End of Period Exchange Rate"
        EndOfPeriodRateLI.Value = GlobalEnums.ConversionOptions.END_OF_PERIOD_RATE
        CurrencyConversionComboBox.Items.Add(EndOfPeriodRateLI)
        currenciesConversionIdItemDict.Add(EndOfPeriodRateLI.Value, EndOfPeriodRateLI)


        ' Recomputation Option
        Dim AggregatedLI As New ListItem
        AggregatedLI.Text = "Aggregated"
        AggregatedLI.Value = GlobalEnums.ConsolidationOptions.AGGREGATION
        ConsolidationOptionComboBox.Items.Add(AggregatedLI)
        consoOptionIdItemDict.Add(AggregatedLI.Value, AggregatedLI)

        Dim RecomputedLI As New ListItem
        RecomputedLI.Text = "Recomputed"
        RecomputedLI.Value = GlobalEnums.ConsolidationOptions.RECOMPUTATION
        ConsolidationOptionComboBox.Items.Add(RecomputedLI)
        consoOptionIdItemDict.Add(RecomputedLI.Value, RecomputedLI)

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
                Dim new_node As TreeNode = AccountsTV.Nodes.Add(CStr(account_id), account_name, account_image, account_image)
                new_node.EnsureVisible()
            Else
                Dim accounts() As TreeNode = AccountsTV.Nodes.Find(account_parent_id, True)
                If accounts.Length = 1 Then
                    Dim new_node As TreeNode = accounts(0).Nodes.Add(CStr(account_id), account_name, account_image, account_image)
                    new_node.EnsureVisible()
                End If
            End If
            AccountsTV.Refresh()
        End If

    End Sub

    Delegate Sub TVNodeDelete_Delegate(ByRef account_id As Int32)
    Friend Sub TVNodeDelete(ByRef account_id As Int32)

        If InvokeRequired Then
            Dim MyDelegate As New TVNodeDelete_Delegate(AddressOf TVNodeDelete)
            Me.Invoke(MyDelegate, New Object() {account_id})
        Else
            Dim accountsNodes() As TreeNode = AccountsTV.Nodes.Find(account_id, True)
            If accountsNodes.Length = 1 Then
                accountsNodes(0).Remove()
                AccountsTV.Refresh()
            End If
        End If

    End Sub


#End Region


#Region "Call backs"

    Private Sub newAccountBT_Click(sender As Object, e As EventArgs) Handles CreateANewAccountToolStripMenuItem.Click, _
                                                                             AddSubAccountToolStripMenuItem.Click

        formulaEdit.Checked = False
        If Not current_node Is Nothing Then
            Controller.DisplayNewAccountView(current_node)
        Else
            MsgBox("Please Select Parent Account first.")
        End If

    End Sub

    Private Sub newCategoryBT_Click(sender As Object, e As EventArgs) Handles CreateANewCategoryToolStripMenuItem.Click, _
                                                                              AddCategoryToolStripMenuItem.Click

        formulaEdit.Checked = False
        Dim newCategoryName As String = InputBox("Name of the New Tab: ", "Account Tab Creation", "")
        If newCategoryName <> "" Then

            If Controller.AccountNameCheck(newCategoryName) = True Then
                Controller.CreateAccount(0, _
                                         newCategoryName, _
                                         GlobalEnums.FormulaTypes.TITLE, _
                                         "", _
                                         GlobalEnums.AccountType.DATE_, _
                                         1, _
                                         1, _
                                         TITLE_FORMAT_CODE, _
                                         GlobalEnums.FormulaTypes.TITLE, _
                                         1, _
                                         AccountsTV.Nodes.Count)
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
        Dim errorsList = Controller.CheckFormulaForUnkonwnTokens(formulastr)
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
        If Controller.InterdependancyTest = True Then
            GoTo SubmitFormula
        Else
            Exit Sub
        End If

SubmitFormula:
        Dim confirm As Integer = MessageBox.Show("You are about to update the formula for Account: " + Chr(13) + Name_TB.Text + Chr(13) + "Do you confirm?", _
                                                 "DataBase submission confirmation", _
                                                  MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
        If confirm = DialogResult.Yes Then
            If Controller.accountsNameKeysDictionary.ContainsKey(Name_TB.Text) Then
                formulaEdit.Checked = False
                Dim accountId As Int32 = Controller.accountsNameKeysDictionary.Item(Name_TB.Text)
                Controller.UpdateAccount(accountId, ACCOUNT_FORMULA_VARIABLE, Controller.GetCurrentParsedFormula)
            End If
        End If

    End Sub

    Private Sub B_DeleteAccount_Click(sender As Object, e As EventArgs) Handles DeleteAccountToolStripMenuItem.Click, _
                                                                                DeleteAccountToolStripMenuItem1.Click

        If Not current_node Is Nothing Then
            formulaEdit.Checked = False
            Dim dependantAccountslist() As String = Controller.ExistingDependantAccounts(current_node)
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
                Controller.DeleteAccount(CInt(current_node.Name))
                current_node = Nothing
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
                WorksheetWrittingFunctions.WriteAccountsFromTreeView(AccountsTV, RNG)
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

        If drag_and_drop = False Then
            If formulaEdit.Checked = False Then current_node = e.Node
        End If

    End Sub

    Private Sub AccountsTV_AfterSelect(sender As Object, e As TreeViewEventArgs)

        If drag_and_drop = False Then
            If formulaEdit.Checked = False Then
                current_node = e.Node
                DisplayAttributes()
            End If
        End If

    End Sub

    Private Sub AccountsTV_KeyDown(sender As Object, e As KeyEventArgs)

        If drag_and_drop = False Then
            Select Case e.KeyCode
                Case Keys.Delete : B_DeleteAccount_Click(sender, e)

                Case Keys.Up
                    If e.Control Then
                        TreeViewsUtilities.MoveNodeUp(AccountsTV.SelectedNode)
                    End If
                Case Keys.Down
                    If e.Control Then
                        TreeViewsUtilities.MoveNodeDown(AccountsTV.SelectedNode)
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
            drag_and_drop = True
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
                    drag_and_drop = False
                    Exit Sub
                End If
            End If

            drag_and_drop = False
            dropNode.EnsureVisible()                                        ' Ensure the newley created node is visible to the user and 
            selectedTreeview.SelectedNode = dropNode                        ' Select it
            Dim tmpHT As New Hashtable
            tmpHT.Add(ACCOUNT_TAB_VARIABLE, TreeViewsUtilities.ReturnRootNodeFromNode(dropNode).Index)
            tmpHT.Add(PARENT_ID_VARIABLE, targetNode.Name)
            Controller.UpdateAccount(dropNode.Name, tmpHT)

        End If


    End Sub

#End Region

#End Region


#Region "Formula Events"

    Private Sub formula_TB_Enter(sender As Object, e As EventArgs) Handles formula_TB.Enter

        If formulaEdit.Checked = False Then
            If Not current_node Is Nothing Then
                If Controller.FTypesToBeTested.Contains(Controller.ReadAccount(current_node.Name, ACCOUNT_FORMULA_TYPE_VARIABLE)) Then
                    formulaEdit.Checked = True
                Else
                    MsgBox("This Account has no editable Formula. Please change the Formula Type or select another accounts " _
                           + "in order to edit the formula.")
                    FormulaTypeComboBox.Focus()
                End If
            Else
                MsgBox("An Accounts must be selected in order to edit its formula.")
                formulaEdit.Checked = False
                AccountsTV.Focus()
                AccountsTV.SelectedNode = AccountsTV.Nodes(0)
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
            formula_TB.Text = formula_TB.Text & FormulasTranslations.ACCOUNTS_HUMAN_IDENTIFIYER & _
                              AccountsTV.SelectedNode.Text & _
                              FormulasTranslations.ACCOUNTS_HUMAN_IDENTIFIYER
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
        formula_TB.Text = formula_TB.Text & FormulasTranslations.ACCOUNTS_HUMAN_IDENTIFIYER & _
                          AccountsTV.SelectedNode.Text & _
                          FormulasTranslations.ACCOUNTS_HUMAN_IDENTIFIYER
        formula_TB.Focus()

    End Sub

#End Region


#End Region


#Region "Accounts Attributes Events"

    Private Sub Name_TB_Validated(sender As Object, e As EventArgs) Handles Name_TB.Validated

        If Not IsNothing(current_node) _
        AndAlso isDisplayingAttributes = False Then
            Dim newNameStr = Name_TB.Text

            If Controller.AccountNameCheck(newNameStr) = True Then
                current_node.Text = Name_TB.Text
                Controller.UpdateName(current_node.Name, current_node.Text)
            Else
                Name_TB.Text = current_node.Text
            End If

        End If
    End Sub

    Private Sub Name_TB_KeyDown(sender As Object, e As KeyEventArgs) Handles Name_TB.KeyDown

        If e.KeyCode = Keys.Enter Then FormulaTypeComboBox.Focus()

    End Sub

    Private Sub formulaTypeCB_SelectedValueChanged(sender As Object, e As EventArgs) Handles FormulaTypeComboBox.SelectedItemChanged

        Dim li = FormulaTypeComboBox.SelectedItem
        If Not IsNothing(current_node) _
        AndAlso isDisplayingAttributes = False _
        AndAlso isRevertingFType = False Then
            If Controller.FormulaTypeChangeImpliesFactsDeletion(CInt(current_node.Name), li.Value) = True Then
                Dim confirm As Integer = MessageBox.Show("Changing the Formula Type of this account may imply the loss of inputs, do you confirm you want to convert this account into a formula?", _
                                                         "Formula Type Upadte Confirmation", _
                                                         MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If confirm = DialogResult.Yes Then
                    GoTo UdpateFormulaType
                Else
                    isRevertingFType = True
                    FormulaTypeComboBox.SelectedValue = Controller.ReadAccount(current_node.Name, ACCOUNT_FORMULA_TYPE_VARIABLE)
                    isRevertingFType = False
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
        Controller.UpdateAccount(current_node.Name, ACCOUNT_FORMULA_TYPE_VARIABLE, li.Value)

    End Sub

    Private Sub TypeCB_SelectedItemChanged(sender As Object, e As EventArgs) Handles TypeComboBox.SelectedItemChanged

        Dim li = TypeComboBox.SelectedItem
        If Not IsNothing(current_node) _
        AndAlso isDisplayingAttributes = False Then
            Controller.UpdateAccount(current_node.Name, ACCOUNT_TYPE_VARIABLE, li.Value)
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
        If Not IsNothing(current_node) _
        AndAlso isDisplayingAttributes = False Then
            Select Case li.Value
                Case GlobalEnums.ConversionOptions.AVERAGE_RATE, _
                     GlobalEnums.ConversionOptions.END_OF_PERIOD_RATE
                    Controller.UpdateAccount(current_node.Name, ACCOUNT_CONVERSION_OPTION_VARIABLE, li.Value)
            End Select
        End If

    End Sub

    Private Sub ConsolidationOptionComboBox_SelectedItemChanged(sender As Object, e As EventArgs) Handles ConsolidationOptionComboBox.SelectedItemChanged

        Dim li = ConsolidationOptionComboBox.SelectedItem
        If Not IsNothing(current_node) _
        AndAlso isDisplayingAttributes = False Then
            Controller.UpdateAccount(current_node.Name, ACCOUNT_CONSOLIDATION_OPTION_VARIABLE, li.Value)
        End If

    End Sub

#End Region


#Region "Utilities"

    Private Sub DisplayAttributes()

        If Not IsNothing(current_node) _
        AndAlso formulaEdit.Checked = False Then

            isDisplayingAttributes = True
            Dim account_id As Int32 = current_node.Name
            Name_TB.Text = current_node.Text

            ' Formula Type ComboBox
            Dim formulaTypeLI = formulasTypesIdItemDict(Controller.ReadAccount(account_id, ACCOUNT_FORMULA_TYPE_VARIABLE))
            FormulaTypeComboBox.SelectedItem = formulaTypeLI

            If formulaTypeLI.Value = GlobalEnums.FormulaTypes.TITLE Then
                SetEnableStatusEdition(False)
            Else
                SetEnableStatusEdition(True)
            End If

            ' Format ComboBox
            Dim formatLI = formatsIdItemDict(Controller.ReadAccount(account_id, ACCOUNT_TYPE_VARIABLE))
            TypeComboBox.SelectedItem = formatLI

            If formatLI.Value = GlobalEnums.AccountType.MONETARY Then

                ' Currency Conversion
                Dim conversionLI = currenciesConversionIdItemDict(Controller.ReadAccount(account_id, ACCOUNT_CONVERSION_OPTION_VARIABLE))
                CurrencyConversionComboBox.SelectedItem = conversionLI

                ' Consolidation Option
                Dim consolidationLI = consoOptionIdItemDict(Controller.ReadAccount(account_id, ACCOUNT_CONSOLIDATION_OPTION_VARIABLE))
                ConsolidationOptionComboBox.SelectedItem = consolidationLI

            End If


            ' Formula TB
            If Controller.FTypesToBeTested.Contains(Controller.ReadAccount(account_id, ACCOUNT_FORMULA_TYPE_VARIABLE)) Then
                formula_TB.Text = Controller.GetFormulaText(account_id)
            Else
                formula_TB.Text = ""
            End If
            isDisplayingAttributes = False

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