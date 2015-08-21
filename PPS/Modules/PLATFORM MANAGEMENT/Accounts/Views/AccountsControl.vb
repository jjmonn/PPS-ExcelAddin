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
' Last modified: 17/07/2015
' Author: Julien Monnereau


Imports Microsoft.Office.Interop
Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel



Friend Class AccountsControl


#Region "Instance Variables"

    ' Objects
    Friend Controller As AccountsController
    Private CP As CircularProgressUI
    Private AccountsTV As TreeView
    Friend current_node As TreeNode

    ' Variables
    Friend accountsTypesKeyNameDict As Dictionary(Of String, String)
    Friend accountsTypeNameKeyDictionary As Dictionary(Of String, String)
    Friend formatsNameKeyDictionary As Hashtable
    Friend formatKeyNameDictionary As Hashtable
    Friend fTypeCodeNameDictionary As Dictionary(Of String, String)
    Friend fTypeNameCodeDictionary As Dictionary(Of String, String)
    Private fTypesCodesRequiringFormulas As List(Of String)
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

        ' priority normal => implement format CRUD
        '   formatsNameKeyDictionary = FormatsMapping.GetFormatsDictionary(FORMAT_NAME_VARIABLE, FORMAT_CODE_VARIABLE, INPUT_FORMAT_CODE)
        '   formatKeyNameDictionary = FormatsMapping.GetFormatsDictionary(FORMAT_CODE_VARIABLE, FORMAT_NAME_VARIABLE, INPUT_FORMAT_CODE)
        fTypesCodesRequiringFormulas = FormulaTypesMapping.GetFTypesKeysNeedingFormula

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

        accountsTypeNameKeyDictionary = AccountTypesMapping.GetAccountTypesDic(ACCOUNT_TYPE_NAME_VARIABLE, ACCOUNT_TYPE_CODE_VARIABLE)
        accountsTypesKeyNameDict = AccountTypesMapping.GetAccountTypesDic(ACCOUNT_TYPE_CODE_VARIABLE, ACCOUNT_TYPE_NAME_VARIABLE)

        For Each format_name In formatsNameKeyDictionary.Keys
            formatsCB.Items.Add(format_name)
        Next

        For Each key In accountsTypesKeyNameDict.Keys
            TypeCB.Items.Add(accountsTypesKeyNameDict.Item(key))
        Next

        fTypeNameCodeDictionary = FormulaTypesMapping.GetFormulaTypesDictionary(FORMULA_TYPES_NAME_VARIABLE, FORMULA_TYPES_CODE_VARIABLE)
        fTypeCodeNameDictionary = FormulaTypesMapping.GetFormulaTypesDictionary(FORMULA_TYPES_CODE_VARIABLE, FORMULA_TYPES_NAME_VARIABLE)
        For Each item In fTypeNameCodeDictionary.Keys
            formulaTypeCB.Items.Add(item)
        Next

    End Sub

    Friend Sub closeControl()

        LaunchCP()
        BackgroundWorker1.RunWorkerAsync()

    End Sub

#End Region


#Region "Interface"

    Friend Sub LaunchCP()

        CP = New CircularProgressUI(Drawing.Color.Blue, "Saving")
        CP.Show()

    End Sub

    Friend Sub StopCP()

        CP.Dispose()

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
        Dim newCategoryName As String = InputBox("Name of the New Category: ", "New Category Creation", "")
        If newCategoryName <> "" Then

            If Controller.AccountNameCheck(newCategoryName) = True Then
                Dim TempHT As New Hashtable
                TempHT.Add(NAME_VARIABLE, newCategoryName)
                TempHT.Add(PARENT_ID_VARIABLE, DBNull.Value)
                TempHT.Add(ACCOUNT_FORMULA_TYPE_VARIABLE, GlobalEnums.FormulaTypes.TITLE)
                TempHT.Add(ACCOUNT_FORMULA_VARIABLE, "")
                TempHT.Add(ACCOUNT_FORMAT_VARIABLE, TITLE_FORMAT_CODE)
                TempHT.Add(ACCOUNT_TYPE_VARIABLE, NORMAL_ACCOUNT_TYPE)
                TempHT.Add(ACCOUNT_TAB_VARIABLE, AccountsTV.Nodes.Count)
                TempHT.Add(ACCOUNT_IMAGE_VARIABLE, 0)
                TempHT.Add(ACCOUNT_SELECTED_IMAGE_VARIABLE, 0)
                TempHT.Add(ITEMS_POSITIONS, 1)
                Controller.CreateAccount(TempHT)
            End If
        End If

    End Sub

    Private Sub Submit_Formula_Click(sender As Object, e As EventArgs) Handles submit_cmd.Click

        Dim confirm As Integer = MessageBox.Show("Careful, you are about to update the formula for Account: " + Chr(13) + Name_TB.Text + Chr(13) + "Do you confirm?", _
                                                 "DataBase submission confirmation", _
                                                  MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

        If confirm = DialogResult.Yes Then
            If Controller.accountsNameKeysDictionary.ContainsKey(Name_TB.Text) Then

                formulaEdit.Checked = False
                Dim accountKey As String = Controller.accountsNameKeysDictionary.Item(Name_TB.Text)
                Controller.UpdateFormula(accountKey, formula_TB.Text)

            End If
        End If

    End Sub

    Private Sub B_DeleteAccount_Click(sender As Object, e As EventArgs) Handles DeleteAccountToolStripMenuItem.Click, _
                                                                                DeleteAccountToolStripMenuItem1.Click

        If Not current_node Is Nothing Then

            formulaEdit.Checked = False
            Dim confirm As Integer = MessageBox.Show("Careful, you are about to delete account " + Chr(13) + Chr(13) + Name_TB.Text + Chr(13) + "Do you confirm?" + Chr(13) + Chr(13) + _
                                                 "This Account and all its Sub Accounts will be deleted.", _
                                                 "Account deletion confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm = DialogResult.Yes Then
                If Controller.DeleteAccount(current_node) = True Then
                    current_node.Remove()
                    current_node = Nothing
                End If
            End If

        End If

    End Sub

    Private Sub BDropToWS_Click(sender As Object, e As EventArgs) Handles DropAllAccountsHierarchyToExcelToolStripMenuItem.Click, _
                                                                          DropHierarchyToExcelToolStripMenuItem.Click

        ' !! to be reimplemented ? !!
        formulaEdit.Checked = False
        Dim ActiveWS As Excel.Worksheet = GlobalVariables.apps.ActiveSheet
        Dim RNG As Excel.Range = GlobalVariables.apps.Application.ActiveCell
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
        drag_and_drop = True

    End Sub

    Private Sub AccountsTV_DragEnter(sender As Object, e As Windows.Forms.DragEventArgs)

        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) Then
            e.Effect = DragDropEffects.Move
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
                If fTypesCodesRequiringFormulas.Contains(Controller.ReadAccount(current_node.Name, ACCOUNT_FORMULA_TYPE_VARIABLE)) Then
                    formulaEdit.Checked = True
                Else
                    MsgBox("This Account has no editable Formula. Please change the Formula Type or select another accounts " _
                           + "in order to edit the formula.")
                    formulaTypeCB.Focus()
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
            formula_TB.Text = formula_TB.Text + AccountsTV.SelectedNode.Text
            ' formula_TB.Focus()
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
        formula_TB.Text = formula_TB.Text + dropNode.Text
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

        If e.KeyCode = Keys.Enter Then formulaTypeCB.Focus()

    End Sub

    Private Sub formatsCB_SelectedValueChanged(sender As Object, e As EventArgs) Handles formatsCB.SelectedValueChanged

        If Not IsNothing(current_node) _
        AndAlso isDisplayingAttributes = False Then
            Dim tmpHT As New Hashtable
            tmpHT.Add(ACCOUNT_FORMAT_VARIABLE, formatsNameKeyDictionary(formatsCB.Text))
            Controller.UpdateAccount(current_node.Name, tmpHT)
        End If

    End Sub

    Private Sub TypeCB_SelectedValueChanged(sender As Object, e As EventArgs) Handles TypeCB.SelectedValueChanged

        If Not IsNothing(current_node) _
        AndAlso isDisplayingAttributes = False Then
            Dim type As String = accountsTypeNameKeyDictionary(TypeCB.Text)
            Controller.UpdateAccount(current_node.Name, ACCOUNT_TYPE_VARIABLE, type)
            If type = MONETARY_ACCOUNT_TYPE Then
                EnableConversionOptions()
            Else
                DisableConversionOptions()
            End If
        End If

    End Sub

    Private Sub formulaTypeCB_SelectedValueChanged(sender As Object, e As EventArgs) Handles formulaTypeCB.SelectedValueChanged

        If Not IsNothing(current_node) _
        AndAlso isDisplayingAttributes = False Then
            Dim f_type = fTypeNameCodeDictionary(formulaTypeCB.Text)
            Controller.UpdateFormulaType(current_node.Name, f_type)
            If f_type = TITLE_FORMAT_CODE Then DisableRecomputationOPtions() Else EnableRecomputationOPtions()
        End If

    End Sub

    Private Sub aggregation_RB_CheckedChanged(sender As Object, e As EventArgs) Handles aggregation_RB.CheckedChanged

        If Not IsNothing(current_node) _
        AndAlso isDisplayingAttributes = False Then
            If aggregation_RB.Checked = True Then
                Controller.UpdateAccount(current_node.Name, ACCOUNT_CONSOLIDATION_OPTION_VARIABLE, GlobalEnums.ConsolidationOptions.AGGREGATION)
            Else
                Controller.UpdateAccount(current_node.Name, ACCOUNT_CONSOLIDATION_OPTION_VARIABLE, GlobalEnums.ConsolidationOptions.RECOMPUTATION)
            End If
        End If

    End Sub

    Private Sub flux_RB_CheckedChanged(sender As Object, e As EventArgs) Handles flux_RB.CheckedChanged

        If Not IsNothing(current_node) _
        AndAlso isDisplayingAttributes = False Then
            If flux_RB.Checked = True Then
                Controller.UpdateAccount(current_node.Name, ACCOUNT_CONVERSION_OPTION_VARIABLE, GlobalEnums.ConversionOptions.AVERAGE_PERIOD_RATE)
            Else
                Controller.UpdateAccount(current_node.Name, ACCOUNT_CONVERSION_OPTION_VARIABLE, GlobalEnums.ConversionOptions.END_OF_PERIOD_RATE)
            End If

        End If

    End Sub

    'Private Sub TypeCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TypeCB.SelectedIndexChanged

    '    If TypeCB.SelectedItem = fTypeCodeNameDictionary(MONETARY_ACCOUNT_TYPE) _
    '    AndAlso isDisplayingAttributes = False Then
    '        EnableConversionOptions()
    '    Else
    '        DisableConversionOptions()
    '    End If

    'End Sub

#End Region


#Region "Background Worker 1"

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        Controller.SendNewPositionsToModel()

    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted

        AfterClosingAttemp_ThreadSafe()

    End Sub

    Delegate Sub AfterClosing_Delegate()

    Private Sub AfterClosingAttemp_ThreadSafe()

        If InvokeRequired Then
            Dim MyDelegate As New AfterClosing_Delegate(AddressOf AfterClosingAttemp_ThreadSafe)
            Me.Invoke(MyDelegate, New Object() {})
        Else
            CP.Dispose()
            Controller.sendCloseOrder()
        End If

    End Sub

#End Region


#Region "Utilities"

    Private Sub DisplayAttributes()

        If Not IsNothing(current_node) _
        AndAlso formulaEdit.Checked = False Then

            isDisplayingAttributes = True
            Dim key As String = current_node.Name
            Dim f_type As String = Controller.ReadAccount(key, ACCOUNT_FORMULA_TYPE_VARIABLE)
            Dim type As String = Controller.ReadAccount(key, ACCOUNT_TYPE_VARIABLE)
            Name_TB.Text = current_node.Text
            formatsCB.Text = formatKeyNameDictionary(Controller.ReadAccount(key, ACCOUNT_FORMAT_VARIABLE))
            TypeCB.Text = accountsTypesKeyNameDict(type)
            formulaTypeCB.Text = fTypeCodeNameDictionary(f_type)

            If f_type = GlobalEnums.FormulaTypes.TITLE Then DisableRecomputationOPtions() Else EnableRecomputationOPtions()
            If Controller.ReadAccount(key, ACCOUNT_CONSOLIDATION_OPTION_VARIABLE) = GlobalEnums.ConsolidationOptions.RECOMPUTATION Then recompute_RB.Checked = True
            If Controller.ReadAccount(key, ACCOUNT_CONSOLIDATION_OPTION_VARIABLE) = GlobalEnums.ConsolidationOptions.AGGREGATION Then aggregation_RB.Checked = True
            If type = MONETARY_ACCOUNT_TYPE Then
                EnableConversionOptions()
                If Controller.ReadAccount(key, ACCOUNT_CONVERSION_OPTION_VARIABLE) = GlobalEnums.ConversionOptions.AVERAGE_PERIOD_RATE Then flux_RB.Checked = True
                If Controller.ReadAccount(key, ACCOUNT_CONVERSION_OPTION_VARIABLE) = GlobalEnums.ConversionOptions.END_OF_PERIOD_RATE Then bs_item_RB.Checked = True
            Else
                DisableConversionOptions()
            End If

            If fTypesCodesRequiringFormulas.Contains(Controller.ReadAccount(key, ACCOUNT_FORMULA_TYPE_VARIABLE)) Then
                formula_TB.Text = Controller.GetFormulaText(key)
            Else
                formula_TB.Text = ""
            End If
            isDisplayingAttributes = False

        End If

    End Sub

    Private Sub ReloadAccountsTree(ByRef expansionDic As Dictionary(Of String, Boolean))

        GlobalVariables.Accounts.LoadAccountsTV(AccountsTV)
        TreeViewsUtilities.ResumeExpansionsLevel(AccountsTV, expansionDic)

    End Sub

#Region "Radio Groups Utilities"

    Private Sub EnableRecomputationOPtions()

        recompute_RB.Enabled = True
        aggregation_RB.Enabled = True

    End Sub

    Private Sub DisableRecomputationOPtions()

        recompute_RB.Enabled = False
        aggregation_RB.Enabled = False

    End Sub

    Private Sub EnableConversionOptions()

        bs_item_RB.Enabled = True
        flux_RB.Enabled = True

    End Sub

    Private Sub DisableConversionOptions()

        bs_item_RB.Enabled = False
        flux_RB.Enabled = False

    End Sub

#End Region

#End Region



End Class
