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
' Last Modified: 28/04/2014
' Author: Julien Monnereau


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections
Imports System.Linq


Friend Class AccountsController


#Region "Instance Variables"

    ' Objects
    Private Accounts As Account
    Private formulasMGT As ModelFormulasMGT
    Private DATAMODEL As DataModel
    Private View As AccountsControl
    Private NewAccountView As NewAccountUI
    Private AccountsTV As New TreeView
    Private PlatformMGTUI As PlatformMGTGeneralUI

    ' Variables
    Protected Friend accountsNameKeysDictionary As Hashtable
    Protected Friend accountsKeyNamesDictionary As Hashtable
    Protected Friend positionsDictionary As New Dictionary(Of String, Double)
    Protected Friend needToUpdateModel As Boolean
    Private dependant_account_id As String
    
#End Region


#Region "Initialization and Closing"

    Protected Friend Sub New()

        Accounts = New Account
        accountsNameKeysDictionary = AccountsMapping.GetAccountsDictionary(ACCOUNT_NAME_VARIABLE, ACCOUNT_ID_VARIABLE)
        accountsKeyNamesDictionary = AccountsMapping.GetAccountsDictionary(ACCOUNT_ID_VARIABLE, ACCOUNT_NAME_VARIABLE)
        Account.LoadAccountsTree(AccountsTV)
        View = New AccountsControl(Me, AccountsTV)
        NewAccountView = New NewAccountUI(View, Me)
        formulasMGT = New ModelFormulasMGT(accountsNameKeysDictionary, AccountsTV)
        positionsDictionary = TreeViewsUtilities.GeneratePositionsDictionary(AccountsTV)

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

    Protected Friend Sub sendCloseOrder()

        View.Dispose()
        Accounts.Close()
        PlatformMGTUI.displayControl()

    End Sub

#End Region


#Region "CRUD Interface"

    Friend Function ReadAccount(ByRef accountKey As String, ByRef field As String) As Object

        Return Accounts.ReadAccount(accountKey, field)

    End Function

    Protected Friend Sub CreateAccount(ByRef accountsAttributes As Hashtable, _
                                       ByRef parent_node As TreeNode)

        Dim id As String = TreeViewsUtilities.GetNewNodeKey(AccountsTV, ACCOUNTS_TOKEN_SIZE)
        accountsAttributes.Add(ACCOUNT_ID_VARIABLE, id)
        accountsAttributes.Add(ITEMS_POSITIONS, 1)
        accountsAttributes.Add(ACCOUNT_TAB_VARIABLE, Accounts.ReadAccount(parent_node.Name, ACCOUNT_TAB_VARIABLE))
        Accounts.CreateAccount(accountsAttributes)

        Dim new_node As TreeNode = parent_node.Nodes.Add(id, accountsAttributes(ACCOUNT_NAME_VARIABLE), accountsAttributes(ACCOUNT_IMAGE_VARIABLE), accountsAttributes(ACCOUNT_SELECTED_IMAGE_VARIABLE))
        accountsNameKeysDictionary.Add(accountsAttributes(ACCOUNT_NAME_VARIABLE), accountsAttributes(ACCOUNT_ID_VARIABLE))
        accountsKeyNamesDictionary.Add(accountsAttributes(ACCOUNT_ID_VARIABLE), accountsAttributes(ACCOUNT_NAME_VARIABLE))
        needToUpdateModel = True

    End Sub

    Protected Friend Sub CreateCategory(ByRef HT As Hashtable)

        Dim id As String = TreeViewsUtilities.GetNewNodeKey(AccountsTV, ACCOUNTS_TOKEN_SIZE)
        HT.Add(ACCOUNT_ID_VARIABLE, id)
        Accounts.CreateAccount(HT)

        Dim newNode As TreeNode = AccountsTV.Nodes.Add(id, HT(ACCOUNT_NAME_VARIABLE))
        accountsNameKeysDictionary.Add(HT(ACCOUNT_NAME_VARIABLE), HT(ACCOUNT_ID_VARIABLE))
        accountsKeyNamesDictionary.Add(HT(ACCOUNT_ID_VARIABLE), HT(ACCOUNT_NAME_VARIABLE))
        needToUpdateModel = True

    End Sub

    Friend Sub UpdateAccount(ByRef accountKey As String, ByRef variable As String, ByVal value As Object)

        Accounts.UpdateAccount(accountKey, variable, value)
        needToUpdateModel = True

    End Sub

    Friend Sub UpdateAccount(ByRef accountKey As String, ByRef HT As Hashtable)

        Accounts.UpdateAccount(accountKey, HT)
        needToUpdateModel = True

    End Sub

    Friend Sub UpdateFormula(ByRef accountKey As String, ByRef formulaStr As String)

        If formulaStr <> "" Then
            formulasMGT.convertFormulaFromNamesToKeys(formulaStr)
            If formulasMGT.errorList.Count = 0 Then
                If formulasMGT.testFormula() = True _
                AndAlso InterdependancyTest() = True Then
                    Accounts.UpdateAccount(accountKey, ACCOUNT_FORMULA_TYPE_VARIABLE, FORMULA_TYPE_FORMULA)
                    Accounts.UpdateAccount(accountKey, ACCOUNT_FORMULA_VARIABLE, formulasMGT.keysFormulaString.Replace(",", "."))
                    MsgBox("Formula successfully saved")
                    needToUpdateModel = True
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
                Accounts.UpdateAccount(accountKey, ACCOUNT_FORMULA_TYPE_VARIABLE, FORMULA_TYPE_FORMULA)
                Accounts.UpdateAccount(accountKey, ACCOUNT_FORMULA_VARIABLE, "")
                MsgBox("Formula successfully saved")
                needToUpdateModel = True
            End If
        End If

    End Sub

    Protected Friend Sub UpdateFormulaType(ByRef account_id As String, ByRef ftype As String)

        Accounts.UpdateAccount(account_id, ACCOUNT_FORMULA_TYPE_VARIABLE, ftype)
        Accounts.UpdateAccount(account_id, ACCOUNT_IMAGE_VARIABLE, View.ftype_icon_dic(ftype))
        Accounts.UpdateAccount(account_id, ACCOUNT_FORMULA_VARIABLE, "")
        Accounts.UpdateAccount(account_id, ACCOUNT_SELECTED_IMAGE_VARIABLE, View.ftype_icon_dic(ftype))
        View.current_node.ImageIndex = View.ftype_icon_dic(ftype)
        View.current_node.SelectedImageIndex = View.ftype_icon_dic(ftype)
        View.formula_TB.Text = ""
        AccountsTV.Invalidate()
        AccountsTV.Update()
        AccountsTV.Refresh()
        needToUpdateModel = True

    End Sub

    Friend Function GetFormulaText(ByRef accountKey As String) As String

        Return formulasMGT.convertFormulaFromKeysToNames(ReadAccount(accountKey, ACCOUNT_FORMULA_VARIABLE))

    End Function

    Friend Function DeleteAccount(ByRef node As TreeNode) As Boolean

        Dim accountsKeyList As List(Of String) = TreeViewsUtilities.GetNodesKeysList(node)
        accountsKeyList.Reverse()
        If AccountsDependenciesCheck(accountsKeyList) = False Then Return False

        DATAMODEL = New DataModel
        Dim accountsToBeDeleted = AccountsVersionsCheck(accountsKeyList)
        If accountsToBeDeleted.Count > 0 Then
            Dim confirm As Integer = MessageBox.Show("The data corresponding to the accounts will be deleted permanetly, do you confirm?", _
                                                     "Accounts deletion validation", _
                                                       MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            If confirm = DialogResult.Yes Then
                RemoveAccountsFromDataTables(accountsToBeDeleted)
                RemoveAccount(accountsKeyList, node)
            Else
                Return False
            End If
        Else
            RemoveAccount(accountsKeyList, node)
        End If
        needToUpdateModel = True
        Return True

    End Function

#End Region


#Region "Computer and Positions Interface"

    Friend Sub SendNewPositionsToModel()

        positionsDictionary = TreeViewsUtilities.GeneratePositionsDictionary(AccountsTV)
        For Each account In positionsDictionary.Keys
            Accounts.UpdateAccount(account, ITEMS_POSITIONS, positionsDictionary(account))
        Next

    End Sub

    Friend Sub UpdateModel()

        If Not GlobalVariables.GlobalDll3Interface Is Nothing Then
            Accounts.Close()
            GlobalVariables.GlobalDll3Interface.destroy_dll()
            GlobalVariables.GlobalDll3Interface = New DLL3_Interface
        End If
        needToUpdateModel = False

    End Sub

#End Region


#Region "Accounts Deletion"

    Private Function AccountsDependenciesCheck(ByRef accountsKeyList As List(Of String)) As Boolean

        Dim dependenciesList As List(Of String) = DependenciesLoopCheck(accountsKeyList)
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

    Private Function DependenciesLoopCheck(ByRef accountsKeyList As List(Of String)) As List(Of String)

        Dim dependenciesList As New List(Of String)
        For Each key In accountsKeyList
            CheckForDependencies(key, dependenciesList)
        Next
        Dim uniqueDependenciesList As List(Of String) = dependenciesList.Distinct().ToList
        For Each accountName In accountsKeyList
            If uniqueDependenciesList.Contains(accountsNameKeysDictionary(accountName)) Then uniqueDependenciesList.Remove(accountName)
        Next
        Return uniqueDependenciesList

    End Function

    Private Function AccountsVersionsCheck(ByRef accountsKeyList As List(Of String)) As List(Of String)

        Dim accountsToBeDeleted As New List(Of String)
        Dim accountsDictionaries As New Dictionary(Of String, List(Of String))
        For Each accountKey In accountsKeyList
            Dim tmpList = DATAMODEL.checkForItemInDataTables(accountKey, DATA_ACCOUNT_ID_VARIABLE)
            If tmpList.Count > 0 Then
                accountsToBeDeleted.Add(accountKey)
                accountsDictionaries.Add(accountsKeyNamesDictionary(accountKey), tmpList)
            End If
        Next

        If accountsDictionaries.Count > 0 Then
            Dim resultStr As String = "The following Accounts were found in the databases: " + Chr(13)
            For Each account In accountsDictionaries.Keys
                resultStr = resultStr + "- " + account + " found in: " + Chr(13)
                For Each versionName In accountsDictionaries(account)
                    resultStr = resultStr + "  - " + versionName + Chr(13)
                Next
            Next
            MsgBox(resultStr)
        End If

        Return accountsToBeDeleted

    End Function

    Private Sub RemoveAccountsFromDataTables(ByRef AccountsToBeDeleted As List(Of String))

        For Each accountKey In AccountsToBeDeleted
            DATAMODEL.deleteRowsWithItemKeys(accountKey, DATA_ACCOUNT_ID_VARIABLE)
        Next

    End Sub

    Private Sub RemoveAccount(ByRef accountsList As List(Of String), ByRef node As TreeNode)

        For Each accountKey In accountsList
            Accounts.DeleteAccount(accountKey)
            accountsNameKeysDictionary.Remove(accountsKeyNamesDictionary(accountKey))
            accountsKeyNamesDictionary.Remove(accountKey)
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
                    Return True
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
            Dim ftype As String = Accounts.ReadAccount(account_id, ACCOUNT_FORMULA_TYPE_VARIABLE)
            If ftype <> FORMULA_TYPE_HARD_VALUE _
            AndAlso ftype <> FORMULA_TYPE_TITLE Then
                If dependancies_dict.ContainsKey(account_id) = False Then AddDependantToDependanciesDict(account_id, dependancies_dict)
                For Each dependant_id In dependancies_dict(account_id)
                    If CheckDependantsInterdependancy(account_id, dependant_id, dependancies_dict) = False Then
                        MsgBox("An interdependancy has been introduced inot accounts formula: " & Chr(13) & Chr(13) & _
                               accountsKeyNamesDictionary(dependant_account_id) & " depends on " & accountsKeyNamesDictionary(account_id) & Chr(13) & Chr(13) & _
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

        If Accounts.ReadAccount(dependant_id, ACCOUNT_FORMULA_TYPE_VARIABLE) = FORMULA_TYPE_SUM_OF_CHILDREN Then
            dependancies_dict.Add(dependant_id, TreeViewsUtilities.GetChildrenIDList(AccountsTV.Nodes.Find(dependant_id, True)(0)))
        Else
            dependancies_dict.Add(dependant_id, formulasMGT.GetFormulaDependantsLIst(dependant_id))
        End If

    End Sub


#End Region

#Region "Formulas Checks"

    Friend Function FormulasTypesChecks() As Boolean

        Dim accountsNamesFormulaErrorList As New List(Of String)
        Dim FTypesToBeTested As List(Of String) = FormulaTypesMapping.GetFTypesKeysNeedingFormula
        For Each accountKey In positionsDictionary.Keys
            If FTypesToBeTested.Contains(Accounts.ReadAccount(accountKey, ACCOUNT_FORMULA_TYPE_VARIABLE)) Then
                If Accounts.ReadAccount(accountKey, ACCOUNT_FORMULA_VARIABLE) = "" Then _
                    accountsNamesFormulaErrorList.Add(Accounts.ReadAccount(accountKey, ACCOUNT_NAME_VARIABLE))
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
    Private Sub CheckForDependencies(ByRef accountKey As String, dependenciesList As List(Of String))

        For Each currentKey In positionsDictionary.Keys
            Dim formula As String = Accounts.ReadAccount(currentKey, ACCOUNT_FORMULA_VARIABLE)
            If Not formula Is Nothing AndAlso formula.Contains(accountKey) Then _
                dependenciesList.Add(Accounts.ReadAccount(currentKey, ACCOUNT_NAME_VARIABLE))
        Next

    End Sub

#End Region

#End Region


#Region "Utilities"

    Protected Friend Sub DisplayAcountsView()

        NewAccountView.Hide()
        View.Show()

    End Sub

    Protected Friend Sub DisplayNewAccountView(ByRef parent_node As TreeNode)

        View.Hide()
        NewAccountView.PrePopulateForm(parent_node)
        NewAccountView.Show()

    End Sub


#End Region


End Class
