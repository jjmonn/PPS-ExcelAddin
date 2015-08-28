' cAccountsController.vb
'
' Utilities functions for accounts controler
'
' To do: 
'       - Verification du statut accounts manager avant toute modification de la chart of accounts
'
'
'
' Known bugs:
'       - 
'
'
'
' Last Modified: 28/08/2015
' Author: Julien Monnereau


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections
Imports System.Linq


Friend Class AccountsController


#Region "Instance Variables"

    ' Objects
    Private formulasMGT As ModelFormulasMGT
    Private FormulasTranslator As FormulasTranslations
    Private View As AccountsControl
    Private NewAccountView As NewAccountUI
    Private AccountsTV As New TreeView
    Private PlatformMGTUI As PlatformMGTGeneralUI


    ' Variables
    Friend accountsNameKeysDictionary As Hashtable
    Friend positionsDictionary As New Dictionary(Of String, Double)
    Private dependant_account_id As String
    Friend FTypesToBeTested As New List(Of Int32)
      
#End Region


#Region "Initialization and Closing"

    Friend Sub New()

        accountsNameKeysDictionary = GlobalVariables.Accounts.GetAccountsDictionary(NAME_VARIABLE, ID_VARIABLE)
        GlobalVariables.Accounts.LoadAccountsTV(AccountsTV)
        View = New AccountsControl(Me, AccountsTV)
        NewAccountView = New NewAccountUI(View, Me)
        FormulasTranslator = New FormulasTranslations(accountsNameKeysDictionary)
        formulasMGT = New ModelFormulasMGT(accountsNameKeysDictionary, AccountsTV)
        positionsDictionary = TreeViewsUtilities.GeneratePositionsDictionary(AccountsTV)
        FTypesToBeTested.Add(GlobalEnums.FormulaTypes.FIRST_PERIOD_INPUT)
        FTypesToBeTested.Add(GlobalEnums.FormulaTypes.FORMULA)

        AddHandler globalvariables.accounts.AccountCreationEvent, AddressOf AccountCreateConfirmation
        AddHandler globalvariables.accounts.AccountUpdateEvent, AddressOf AccountUpdateConfirmation
        AddHandler globalvariables.accounts.AccountDeleteEvent, AddressOf AccountDeleteConfirmation

    End Sub

    Public Sub addControlToPanel(ByRef dest_panel As Panel, _
                                 ByRef PlatformMGTUI As PlatformMGTGeneralUI)

        Me.PlatformMGTUI = PlatformMGTUI
        dest_panel.Controls.Add(View)
        View.Dock = Windows.Forms.DockStyle.Fill

    End Sub

    Public Sub close()

        View.closeControl()

    End Sub

    Friend Sub sendCloseOrder()

        View.Dispose()
        PlatformMGTUI.displayControl()

    End Sub

#End Region


#Region "CRUD Interface"

    Friend Function ReadAccount(ByRef accountKey As Int32, ByRef field As String) As Object

        Return GlobalVariables.Accounts.accounts_hash(accountKey)(field)

    End Function

    Friend Sub CreateAccount()

        Dim TempHT As New Hashtable
        TempHT.Add(NAME_VARIABLE, NewAccountView.nameTB.Text)
        TempHT.Add(ACCOUNT_FORMULA_TYPE_VARIABLE, NewAccountView.formulaCB.SelectedItem.value)
        TempHT.Add(ACCOUNT_FORMULA_VARIABLE, "")
        TempHT.Add(ACCOUNT_TYPE_VARIABLE, NewAccountView.formatCB.SelectedItem.value)
        TempHT.Add(ACCOUNT_IMAGE_VARIABLE, TempHT(ACCOUNT_FORMULA_TYPE_VARIABLE))   ' dumb to be takenoff priotiy normal
        If NewAccountView.aggregation_RB.Checked = True Then TempHT.Add(ACCOUNT_CONSOLIDATION_OPTION_VARIABLE, GlobalEnums.ConsolidationOptions.AGGREGATION)
        If NewAccountView.recompute_RB.Checked = True Then TempHT.Add(ACCOUNT_CONSOLIDATION_OPTION_VARIABLE, GlobalEnums.ConsolidationOptions.RECOMPUTATION)
        If NewAccountView.flux_RB.Checked = True Then TempHT.Add(ACCOUNT_CONVERSION_OPTION_VARIABLE, GlobalEnums.ConversionOptions.AVERAGE_RATE)
        If NewAccountView.bs_item_RB.Checked = True Then TempHT.Add(ACCOUNT_CONVERSION_OPTION_VARIABLE, GlobalEnums.ConversionOptions.END_OF_PERIOD_RATE)
        If Not NewAccountView.parent_node Is Nothing Then
            TempHT.Add(PARENT_ID_VARIABLE, NewAccountView.parent_node.Name)
        Else
            TempHT.Add(PARENT_ID_VARIABLE, 0)
        End If
        TempHT.Add(ITEMS_POSITIONS, 1)
        TempHT.Add(ACCOUNT_TAB_VARIABLE, GlobalVariables.Accounts.accounts_hash(CInt(NewAccountView.parent_node.Name))(ACCOUNT_TAB_VARIABLE))
        TempHT.Add(ACCOUNT_FORMAT_VARIABLE, "n") ' dumb to be takenoff priotiy normal

        GlobalVariables.Accounts.CMSG_CREATE_ACCOUNT(TempHT)
        View.LaunchCP()

    End Sub

    Friend Sub CreateAccountTab()

        ' to be reimplemented priority high !! 


    End Sub


    Friend Sub UpdateAccount(ByRef id As Int32, ByRef variable As String, ByVal value As Object)

        Dim ht As Hashtable = GlobalVariables.Accounts.accounts_hash(id)
        ht(variable) = value
        GlobalVariables.Accounts.CMSG_UPDATE_ACCOUNT(ht)
        View.LaunchCP()

    End Sub

    Friend Sub UpdateAccount(ByRef id As String, ByRef account_attributes As Hashtable)

        Dim ht As Hashtable = globalvariables.accounts.accounts_hash(id)
        For Each attribute As String In account_attributes
            ht(attribute) = account_attributes(attribute)
        Next
        globalvariables.accounts.CMSG_UPDATE_ACCOUNT(ht)
        View.LaunchCP()

    End Sub

    Friend Function DeleteAccount(ByRef node As TreeNode) As Boolean

        Dim accountsKeyList As List(Of Int32) = TreeViewsUtilities.GetNodesKeysList(node)
        accountsKeyList.Reverse()
        If AccountsDependenciesCheck(accountsKeyList) = False Then Return False

        Dim accountsToBeDeleted = AccountsVersionsCheck(accountsKeyList)
        If accountsToBeDeleted.Count > 0 Then
            Dim confirm As Integer = MessageBox.Show("The data corresponding to the accounts will be deleted permanetly, do you confirm?", _
                                                     "Accounts deletion validation", _
                                                       MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            If confirm = DialogResult.Yes Then
                RemoveAccount(accountsKeyList, node)
            Else
                Return False
            End If
        Else
            RemoveAccount(accountsKeyList, node)
        End If
        Return True

    End Function

    Friend Sub UpdateName(ByRef account_id As Int32, _
                          ByRef new_name As String)

        ' below -> may raise issue if pb on update!(priority: low)
        Dim old_name = GlobalVariables.Accounts.accounts_hash(account_id)(NAME_VARIABLE)
        accountsNameKeysDictionary.Remove(old_name)
        accountsNameKeysDictionary.Add(new_name, account_id)
        UpdateAccount(account_id, NAME_VARIABLE, new_name)

    End Sub

    Friend Sub UpdateFormula(ByRef id As Int32, ByRef formulaStr As String)

        If formulaStr <> "" Then

            FormulasTranslator.GetDBFormulaFromHumanFormula(formulaStr)


            '  to be reviewed/ remimplemented priority high

            ' -> priority => periods 


            formulasMGT.convertFormulaFromNamesToKeys(formulaStr)
            If formulasMGT.errorList.Count = 0 Then
                If formulasMGT.testFormula() = True _
                AndAlso InterdependancyTest() = True Then
                    Dim tmp_ht As New Hashtable
                    tmp_ht(ACCOUNT_FORMULA_TYPE_VARIABLE) = GlobalEnums.FormulaTypes.FORMULA
                    tmp_ht(ACCOUNT_FORMULA_VARIABLE) = formulasMGT.keysFormulaString.Replace(",", ".")
                    UpdateAccount(id, tmp_ht)
                Else
                    MsgBox("The formula is not valid and could not be saved.")
                End If
            Else
                Dim errors As String = ""
                For Each errorItem In formulasMGT.errorList
                    errors = errors + errorItem + Chr(13)
                Next
                MsgBox("The following items are not mapped accounts or invalid items in a formula: " + Chr(13) _
                    + errors)
            End If
        Else
            Dim confirm2 As Integer = MessageBox.Show("The formula is empty, do you want to save it?", _
                                                      "Formula validation", _
                                                       MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            If confirm2 = DialogResult.Yes Then
                Dim tmp_ht As New Hashtable
                tmp_ht(ACCOUNT_FORMULA_TYPE_VARIABLE) = GlobalEnums.FormulaTypes.FORMULA
                tmp_ht(ACCOUNT_FORMULA_VARIABLE) = ""
                UpdateAccount(id, tmp_ht)
            End If

        End If

    End Sub

    Friend Sub UpdateFormulaType(ByRef id As Int32, ByRef ftype As String)

        ' A revoir !! priority high
        'Dim tmp_ht As New Hashtable
        'tmp_ht(ACCOUNT_FORMULA_TYPE_VARIABLE) = ftype
        'tmp_ht(ACCOUNT_IMAGE_VARIABLE) = View.ftype_icon_dic(ftype)
        'tmp_ht(ACCOUNT_SELECTED_IMAGE_VARIABLE) = View.ftype_icon_dic(ftype)
        'tmp_ht(ACCOUNT_FORMULA_VARIABLE) = "" ' ?????!!!!!!!
        'UpdateAccount(id, tmp_ht)

        'View.current_node.ImageIndex = View.ftype_icon_dic(ftype)
        'View.current_node.SelectedImageIndex = View.ftype_icon_dic(ftype)
        'View.formula_TB.Text = ""
        'AccountsTV.Invalidate()
        'AccountsTV.Update()
        'AccountsTV.Refresh()

    End Sub

    Friend Function GetFormulaText(ByRef accountId As Int32) As String

        Return formulasMGT.convertFormulaFromKeysToNames(ReadAccount(accountId, ACCOUNT_FORMULA_VARIABLE))

    End Function

    Friend Sub SendNewPositionsToModel()

        ' dans l'idéal trouver une autre solution !! priority high
        positionsDictionary = TreeViewsUtilities.GeneratePositionsDictionary(AccountsTV)
        For Each account In positionsDictionary.Keys
            UpdateAccount(account, ITEMS_POSITIONS, positionsDictionary(account))
        Next

    End Sub


#End Region


#Region "Accounts Deletion"

    Private Function AccountsDependenciesCheck(ByRef accountsKeyList As List(Of Int32)) As Boolean

        Dim dependenciesList As List(Of Int32) = DependenciesLoopCheck(accountsKeyList)
        If dependenciesList.Count > 0 Then
            Dim listStr As String = ""
            For Each accountName In dependenciesList
                listStr = listStr & " - " & accountName & Chr(13)
            Next
            MsgBox("The following Accounts formula are dependant on some accounts you want to delete: " & Chr(13) & _
                   listStr & Chr(13) & _
                   "Those formulas must first be changed before the Selected Accounts can be deleted.")
            Return False
        Else
            Return True
        End If

    End Function

    Private Function DependenciesLoopCheck(ByRef accountsKeyList As List(Of Int32)) As List(Of Int32)

        Dim dependenciesList As New List(Of Int32)
        For Each key In accountsKeyList
            CheckForDependencies(key, dependenciesList)
        Next
        Dim uniqueDependenciesList As List(Of Int32) = dependenciesList.Distinct().ToList
        For Each accountName In accountsKeyList
            If uniqueDependenciesList.Contains(accountsNameKeysDictionary(accountName)) Then uniqueDependenciesList.Remove(accountName)
        Next
        Return uniqueDependenciesList

    End Function

    Private Function AccountsVersionsCheck(ByRef accountsKeyList As List(Of Int32)) As List(Of Int32)

        ' display accounts ids to users !!
        ' priority normal
        ' a priori implémenté au niveau server on account deletion ?
        ' to be validated/ reviewed

        ' ------------------------------------
        ' 
        ' ask server for the list of accounts !
        '
        '-------------------------------------

        'If accountsDictionaries.Count > 0 Then
        '    Dim resultStr As String = "The following Accounts were found in the databases: " + Chr(13)
        '    For Each account In accountsDictionaries.Keys
        '        resultStr = resultStr & "- " & account & " found in: " + Chr(13)
        '        For Each versionName In accountsDictionaries(account)
        '            resultStr = resultStr + "  - " + versionName + Chr(13)
        '        Next
        '    Next
        '    MsgBox(resultStr)
        'End If



    End Function

    

    Private Sub RemoveAccount(ByRef accountsList As List(Of Int32), ByRef node As TreeNode)

        For Each id In accountsList
            GlobalVariables.Accounts.CMSG_DELETE_ACCOUNT(id)
            ' -> must wait for confirmation !!
            accountsNameKeysDictionary.Remove(GlobalVariables.Accounts.accounts_hash(id)(NAME_VARIABLE))
        Next

    End Sub

#End Region


#Region "Checks"

#Region "Accounts checks"

    Friend Function AccountNameCheck(ByRef nameStr As String) As Boolean

        If nameStr = "" Then
            MsgBox("The Name of the Account cannot be empty. Please enter a Name")
            Return False
        Else
            If accountsNameKeysDictionary.ContainsKey(nameStr) Then
                MsgBox("This Name is already an used. Please enter another Account Name")
                Return False
            Else
                If CheckAccountNameForForbiddenCharacters(nameStr) = False Then
                    MsgBox("This Name contains special characters (e.g. '+', ','). Please enter another Account Name")
                    Return False
                Else
                    If nameStr.Length > NAMES_MAX_LENGTH Then
                        MsgBox("The Account Names cannot exceed " & NAMES_MAX_LENGTH & " characters.")
                        Return False
                    Else
                        Return True
                    End If
                End If
            End If
        End If

    End Function

    Private Function CheckAccountNameForForbiddenCharacters(ByRef NameStr As String) As Boolean

        For Each specialCharacter As String In ModelFormulasMGT.FORMULAS_TOKEN_CHARACTERS
            If NameStr.Contains(specialCharacter) Then Return False
        Next
        Return True

    End Function

    Private Function InterdependancyTest() As Boolean

        Dim dependancies_dict As New Dictionary(Of String, List(Of String))
        Dim accounts_list = TreeViewsUtilities.GetNodesKeysList(AccountsTV)
        For Each account_id In accounts_list
            Dim ftype As String = globalvariables.accounts.accounts_hash(account_id)(ACCOUNT_FORMULA_TYPE_VARIABLE)
            If ftype <> GlobalEnums.FormulaTypes.HARD_VALUE_INPUT _
            AndAlso ftype <> GlobalEnums.FormulaTypes.TITLE Then
                If dependancies_dict.ContainsKey(account_id) = False Then AddDependantToDependanciesDict(account_id, dependancies_dict)
                For Each dependant_id In dependancies_dict(account_id)
                    If CheckDependantsInterdependancy(account_id, dependant_id, dependancies_dict) = False Then
                        MsgBox("An interdependancy has been introduced inot accounts formula: " & Chr(13) & Chr(13) & _
                               GlobalVariables.Accounts.accounts_hash(dependant_account_id)(NAME_VARIABLE) & " depends on " & GlobalVariables.Accounts.accounts_hash(account_id)(NAME_VARIABLE) & Chr(13) & Chr(13) & _
                               "The formula cannot therefore be saved.")
                        Return False
                    End If
                Next
            End If
        Next
        Return True

    End Function

    Private Function CheckDependantsInterdependancy(ByRef account_id As String, _
                                                    ByRef dependant_id As String, _
                                                    ByRef dependancies_dict As Dictionary(Of String, List(Of String))) _
                                                    As Boolean

        If dependant_id = account_id Then Return False
        If dependancies_dict.ContainsKey(dependant_id) = False Then AddDependantToDependanciesDict(dependant_id, dependancies_dict)
        For Each dependant_child_id In dependancies_dict(dependant_id)
            If CheckDependantsInterdependancy(account_id, dependant_child_id, dependancies_dict) = False Then
                dependant_account_id = dependant_child_id
                Return False
            End If
        Next
        Return True

    End Function

    Private Sub AddDependantToDependanciesDict(ByRef dependant_id As String, _
                                               ByRef dependancies_dict As Dictionary(Of String, List(Of String)))

        If GlobalVariables.Accounts.accounts_hash(dependant_id)(ACCOUNT_FORMULA_TYPE_VARIABLE) = GlobalEnums.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS Then
            dependancies_dict.Add(dependant_id, TreeViewsUtilities.GetChildrenIDList(AccountsTV.Nodes.Find(dependant_id, True)(0)))
        Else
            dependancies_dict.Add(dependant_id, formulasMGT.GetFormulaDependantsLIst(dependant_id))
        End If

    End Sub


#End Region

#Region "Formulas Checks"

    Friend Function FormulasTypesChecks() As Boolean

        Dim accountsNamesFormulaErrorList As New List(Of String)
      
        For Each accountKey In positionsDictionary.Keys
            If FTypesToBeTested.Contains(globalvariables.accounts.accounts_hash(accountKey)(ACCOUNT_FORMULA_TYPE_VARIABLE)) Then
                If globalvariables.accounts.accounts_hash(accountKey)(ACCOUNT_FORMULA_VARIABLE) = "" Then _
                    accountsNamesFormulaErrorList.Add(globalvariables.accounts.accounts_hash(accountKey)(NAME_VARIABLE))
            End If
        Next

        If accountsNamesFormulaErrorList.Count > 0 Then
            Dim errorStr As String = "-"
            For Each account In accountsNamesFormulaErrorList
                errorStr = errorStr + account + Chr(13) + "-"
            Next
            errorStr = Strings.Left(errorStr, errorStr.Length - 1)
            MsgBox("The following accounts need to have a valid formula: " + Chr(13) + errorStr + Chr(13) + _
                   "In order for these accounts to be calculated a valid formula must be given.")
            Return False
        Else
            Return True
        End If

    End Function

    ' Looks for the param accountKey in Accounts formulas
    Private Sub CheckForDependencies(ByRef accountKey As String, dependenciesList As List(Of Int32))

        For Each currentKey In positionsDictionary.Keys
            Dim formula As String = GlobalVariables.Accounts.accounts_hash(currentKey)(ACCOUNT_FORMULA_VARIABLE)
            If Not formula Is Nothing AndAlso formula.Contains(accountKey) Then _
                dependenciesList.Add(GlobalVariables.Accounts.accounts_hash(currentKey)(NAME_VARIABLE))
        Next

    End Sub

#End Region

#End Region


#Region "Utilities"

    Friend Sub DisplayAcountsView()

        NewAccountView.Hide()
        View.Show()

    End Sub

    Friend Sub DisplayNewAccountView(ByRef parent_node As TreeNode)

        View.Hide()
        NewAccountView.PrePopulateForm(parent_node)
        NewAccountView.Show()

    End Sub


#End Region


#Region "Events"

    Private Sub AccountCreateConfirmation(ByRef status As Boolean, ByRef attributes As Hashtable)

        accountsNameKeysDictionary.Add(attributes(NAME_VARIABLE), attributes(ID_VARIABLE))
        If IsNumeric(attributes(PARENT_ID_VARIABLE)) Then
            Dim parent_node As TreeNode = AccountsTV.Nodes.Find(attributes(ID_VARIABLE), True)(0)
            parent_node.Nodes.Add(attributes(ID_VARIABLE), _
                                  attributes(NAME_VARIABLE), _
                                  attributes(IMAGE_VARIABLE), _
                                  attributes(IMAGE_VARIABLE))
        Else
            AccountsTV.Nodes.Add(attributes(ID_VARIABLE), _
                                attributes(NAME_VARIABLE), _
                                attributes(IMAGE_VARIABLE), _
                                attributes(IMAGE_VARIABLE))
        End If
        View.StopCP()

    End Sub

    Private Sub AccountUpdateConfirmation()

        View.StopCP()

    End Sub

    Private Sub AccountDeleteConfirmation()

        View.StopCP()

    End Sub


#End Region


End Class
