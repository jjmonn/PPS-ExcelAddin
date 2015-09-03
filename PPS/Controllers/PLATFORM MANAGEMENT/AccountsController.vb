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
' Last Modified: 02/09/2015
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
    Friend positionsDictionary As New Dictionary(Of Int32, Double)
    Private dependant_account_id As String
    Friend FTypesToBeTested As New List(Of Int32)
    Private isClosing As Boolean = False

#End Region


#Region "Initialization and Closing"

    Friend Sub New()

        GlobalVariables.Accounts.LoadAccountsTV(AccountsTV)
        View = New AccountsControl(Me, AccountsTV)
        InstanceVariablesLoading()
        positionsDictionary = TreeViewsUtilities.GeneratePositionsDictionary(AccountsTV)
        FTypesToBeTested.Add(GlobalEnums.FormulaTypes.FIRST_PERIOD_INPUT)
        FTypesToBeTested.Add(GlobalEnums.FormulaTypes.FORMULA)

        AddHandler GlobalVariables.Accounts.Read, AddressOf AccountUpdateFromServer
        AddHandler GlobalVariables.Accounts.DeleteEvent, AddressOf AccountDeleteFromServer
        AddHandler GlobalVariables.Accounts.CreationEvent, AddressOf AccountCreateConfirmation
        AddHandler GlobalVariables.Accounts.UpdateEvent, AddressOf AccountUpdateConfirmation

    End Sub

    Private Sub InstanceVariablesLoading()

        accountsNameKeysDictionary = GlobalVariables.Accounts.GetAccountsDictionary(NAME_VARIABLE, ID_VARIABLE)
        NewAccountView = New NewAccountUI(View, Me)
        FormulasTranslator = New FormulasTranslations(accountsNameKeysDictionary)
        formulasMGT = New ModelFormulasMGT(accountsNameKeysDictionary, AccountsTV)

    End Sub

    Public Sub addControlToPanel(ByRef dest_panel As Panel, _
                                 ByRef PlatformMGTUI As PlatformMGTGeneralUI)

        Me.PlatformMGTUI = PlatformMGTUI
        dest_panel.Controls.Add(View)
        View.Dock = Windows.Forms.DockStyle.Fill

    End Sub

    Public Sub close()

        isClosing = True
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

    Friend Sub CreateAccount(ByRef account_parent_id As Int32, _
                             ByRef account_name As String, _
                             ByRef account_formula_type As Int32, _
                             ByRef account_formula As String, _
                             ByRef account_type As Int32, _
                             ByRef account_consolidation_option As Int32, _
                             ByRef account_conversion_option As Int32, _
                             ByRef account_format As String, _
                             ByRef account_image As Int32, _
                             ByRef account_position As Int32, _
                             ByRef account_tab As Int32)

        Dim TempHT As New Hashtable
        TempHT.Add(PARENT_ID_VARIABLE, account_parent_id)
        TempHT.Add(NAME_VARIABLE, account_name)
        TempHT.Add(ACCOUNT_FORMULA_TYPE_VARIABLE, account_formula_type)
        TempHT.Add(ACCOUNT_FORMULA_VARIABLE, account_formula)
        TempHT.Add(ACCOUNT_TYPE_VARIABLE, account_type)
        TempHT.Add(ACCOUNT_CONSOLIDATION_OPTION_VARIABLE, account_consolidation_option)
        TempHT.Add(ACCOUNT_CONVERSION_OPTION_VARIABLE, account_conversion_option)
        TempHT.Add(ACCOUNT_FORMAT_VARIABLE, account_format)
        TempHT.Add(ACCOUNT_IMAGE_VARIABLE, TempHT(ACCOUNT_FORMULA_TYPE_VARIABLE))                   ' dumb to be takenoff priotiy normal
        TempHT.Add(ITEMS_POSITIONS, account_position)                                                              ' Dumb
        TempHT.Add(ACCOUNT_TAB_VARIABLE, account_tab)

        GlobalVariables.Accounts.CMSG_CREATE_ACCOUNT(TempHT)
        View.LaunchCP()

    End Sub

    Friend Sub UpdateAccount(ByRef id As Int32, ByRef variable As String, ByVal value As Object)

        Dim ht As Hashtable = GlobalVariables.Accounts.accounts_hash(id).clone
        ht(variable) = value
        GlobalVariables.Accounts.CMSG_UPDATE_ACCOUNT(ht)

    End Sub

    Friend Sub UpdateAccount(ByRef id As String, ByRef account_attributes As Hashtable)

        Dim ht As Hashtable = GlobalVariables.Accounts.accounts_hash(id).clone
        For Each attribute As String In account_attributes
            ht(attribute) = account_attributes(attribute)
        Next
        GlobalVariables.Accounts.CMSG_UPDATE_ACCOUNT(ht)
        View.LaunchCP()

    End Sub

    Friend Sub UpdateBatch(ByRef accountsUpdates As List(Of Tuple(Of Int32, String, Int32)))

        Dim accountsHTUpdates As New List(Of Hashtable)
        For Each tuple_ As Tuple(Of Int32, String, Int32) In accountsUpdates
            Dim ht As Hashtable = GlobalVariables.Accounts.accounts_hash(tuple_.Item1).clone
            ht(tuple_.Item2) = tuple_.Item3
            accountsHTUpdates.Add(ht)
        Next
        GlobalVariables.Accounts.CMSG_UPDATE_LIST_ACCOUNT(accountsHTUpdates)

    End Sub

    Friend Sub UpdateName(ByRef account_id As Int32, _
                           ByRef new_name As String)

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

    Friend Sub DeleteAccount(ByRef account_id As Int32)

        GlobalVariables.Accounts.CMSG_DELETE_ACCOUNT(account_id)

    End Sub

#End Region


#Region "Check"

    Friend Function ExistingDependantAccounts(ByRef node As TreeNode) As String()

        Dim dependantAccountsNames As New List(Of String)
        Dim accountsKeyList As List(Of Int32) = TreeViewsUtilities.GetNodesKeysList(node)
        accountsKeyList.Reverse()

        Dim dependantAccountsId() As Int32 = DependenciesLoopCheck(accountsKeyList).ToArray
        For Each dependantAccountId As Int32 In dependantAccountsId
            dependantAccountsNames.Add(GlobalVariables.Accounts.accounts_hash(dependantAccountId)(NAME_VARIABLE))
        Next

        Return dependantAccountsNames.ToArray

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

    ' Looks for the param accountKey in Accounts formulas
    Private Sub CheckForDependencies(ByRef accountKey As String, dependenciesList As List(Of Int32))

        For Each accountId As Int32 In GlobalVariables.Accounts.accounts_hash.Keys
            Dim formula As String = GlobalVariables.Accounts.accounts_hash(accountId)(ACCOUNT_FORMULA_VARIABLE)
            If Not formula Is Nothing AndAlso formula.Contains(accountKey) Then _
                dependenciesList.Add(accountId)
        Next

    End Sub

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


#Region "Formulas Interdependancies checks"

    Private Function InterdependancyTest() As Boolean

        Dim dependancies_dict As New Dictionary(Of String, List(Of String))
        Dim accounts_list = TreeViewsUtilities.GetNodesKeysList(AccountsTV)
        For Each account_id In accounts_list
            Dim ftype As String = GlobalVariables.Accounts.accounts_hash(account_id)(ACCOUNT_FORMULA_TYPE_VARIABLE)
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

#End Region


#Region "Events"

    Private Sub AccountUpdateFromServer(ByRef status As Boolean, ByRef accountsAttributes As Hashtable)

        If status = True _
        AndAlso AccountsTV.Nodes.Find(accountsAttributes(ID_VARIABLE), True).Length = 0 _
        AndAlso isClosing = False Then
            View.TVUpdate(accountsAttributes(ID_VARIABLE), _
                          accountsAttributes(PARENT_ID_VARIABLE), _
                          accountsAttributes(NAME_VARIABLE), _
                          accountsAttributes(IMAGE_VARIABLE))
            InstanceVariablesLoading()
        End If

    End Sub

    Private Sub AccountDeleteFromServer(ByRef status As Boolean, ByRef id As UInt32)

        If status = True _
        AndAlso isClosing = False Then
            View.TVNodeDelete(id)
            InstanceVariablesLoading()
        End If

    End Sub

    Private Sub AccountCreateConfirmation(ByRef status As Boolean, ByRef id As Int32)

        View.CircularProgressStop()
        If status = False Then
            MsgBox("The account could not be created." & Chr(13) & _
                   "Error " & "")
            ' display error from error (to be catched in account) priority normal 
        End If

    End Sub

    Private Sub AccountUpdateConfirmation(ByRef status As Boolean, ByRef id As Int32)

        If status = False Then
            MsgBox("The account could not be created." & Chr(13) & _
                   "Error " & "")
            ' display error from error (to be catched in account) priority normal 
        End If


    End Sub

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

    Friend Sub SendNewPositionsToModel()

        Dim position As Int32
        Dim accountsUpdates As New List(Of Tuple(Of Int32, String, Int32))
        positionsDictionary = TreeViewsUtilities.GeneratePositionsDictionary(AccountsTV)
        For Each account_id As Int32 In positionsDictionary.Keys
            position = positionsDictionary(account_id)
            If position <> GlobalVariables.Accounts.accounts_hash(account_id)(ITEMS_POSITIONS) Then
                Dim tuple_ As New Tuple(Of Int32, String, Int32)(account_id, ITEMS_POSITIONS, position)
                accountsUpdates.Add(tuple_)
            End If
        Next
        UpdateBatch(accountsUpdates)

    End Sub

    Friend Function GetFormulaText(ByRef accountId As Int32) As String

        Return formulasMGT.convertFormulaFromKeysToNames(ReadAccount(accountId, ACCOUNT_FORMULA_VARIABLE))

    End Function

#End Region



End Class
