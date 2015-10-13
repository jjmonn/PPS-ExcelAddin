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
' Last Modified: 03/09/2015
' Author: Julien Monnereau


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections
Imports System.Linq


Friend Class AccountsController


#Region "Instance Variables"

    ' Objects
    Private FormulasTranslator As FormulasTranslations
    Private m_view As AccountsView
    Private NewAccountView As NewAccountUI
    Private AccountsTV As New TreeView
    Private m_globalFactsTV As New TreeView
    Private PlatformMGTUI As PlatformMGTGeneralUI


    ' Variables
    Friend accountsNameKeysDictionary As Hashtable
    Friend factsNameKeysDictionary As Hashtable
    Friend positionsDictionary As New Dictionary(Of Int32, Double)
    Private dependant_account_id As String
    Friend FTypesToBeTested As New List(Of Int32)
    Private isClosing As Boolean = False
    Private m_CRUDOperations As New Dictionary(Of Int32, CRUDAction)

    ' Constants
    Friend Const ACCOUNTS_FORBIDDEN_CHARACTERS As String = "+-*=<>^?:;![]" ' to be reviewed - 


#End Region


#Region "Initialization and Closing"

    Friend Sub New()

        GlobalVariables.Accounts.LoadAccountsTV(AccountsTV)
        GlobalVariables.GlobalFacts.LoadGlobalFactsTV(m_globalFactsTV)
        m_view = New AccountsView(Me, AccountsTV, m_globalFactsTV)
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
        factsNameKeysDictionary = GlobalVariables.GlobalFacts.GetFactsHashtable()
        NewAccountView = New NewAccountUI(m_view, Me)
        FormulasTranslator = New FormulasTranslations()

    End Sub

    Public Sub addControlToPanel(ByRef dest_panel As Panel, _
                                 ByRef PlatformMGTUI As PlatformMGTGeneralUI)

        Me.PlatformMGTUI = PlatformMGTUI
        dest_panel.Controls.Add(m_view)
        m_view.Dock = Windows.Forms.DockStyle.Fill

    End Sub

    Public Sub close()

        isClosing = True
        SendNewPositionsToModel()
        m_view.Dispose()

    End Sub

#End Region


#Region "CRUD Interface"

    Friend Function ReadAccount(ByRef accountKey As Int32, ByRef field As String) As Object

        Return GlobalVariables.Accounts.m_accountsHash(accountKey)(field)

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
        TempHT.Add(ACCOUNT_DESCRIPTION_VARIABLE, "")

        'Dim tmpId As Int32 = GlobalVariables.Accounts.CreateAccount(TempHT)
        'If m_CRUDOperations.ContainsKey(tmpId) Then m_CRUDOperations(tmpId) = CRUDAction.CREATE Else m_CRUDOperations.Add(tmpId, CRUDAction.CREATE)
        'm_view.TVUpdate(tmpId, account_parent_id, account_name, account_image)
        'accountsNameKeysDictionary.Add(account_name, tmpId)
        GlobalVariables.Accounts.CMSG_CREATE_ACCOUNT(TempHT)
    End Sub

    Friend Sub UpdateAccount(ByRef id As Int32, ByRef variable As String, ByVal value As Object, Optional send As Boolean = False)

        Dim ht As Hashtable = GlobalVariables.Accounts.m_accountsHash(id)

        ht(variable) = value
        If Not ht(IS_TMP_ID) And Not send Then
            If m_CRUDOperations.ContainsKey(id) Then m_CRUDOperations(id) = CRUDAction.UPDATE Else m_CRUDOperations.Add(id, CRUDAction.UPDATE)
        End If
        If send = True Then GlobalVariables.Accounts.CMSG_UPDATE_ACCOUNT(ht)
    End Sub

    Friend Sub UpdateAccount(ByRef id As Int32, ByRef account_attributes As Hashtable, Optional send As Boolean = False)

        Dim ht As Hashtable = GlobalVariables.Accounts.m_accountsHash(id)

        For Each attribute As String In account_attributes.Keys
            ht(attribute) = account_attributes(attribute)
        Next

        If Not ht(IS_TMP_ID) And Not send Then
            If m_CRUDOperations.ContainsKey(id) Then m_CRUDOperations(id) = CRUDAction.UPDATE Else m_CRUDOperations.Add(id, CRUDAction.UPDATE)
        End If
        If send = True Then GlobalVariables.Accounts.CMSG_UPDATE_ACCOUNT(ht)

    End Sub

    Friend Sub Save()

        If (m_CRUDOperations.Count = 0) Then Exit Sub
        GlobalVariables.Accounts.CMSG_CRUD_ACCOUNT_LIST(m_CRUDOperations)
        GlobalVariables.Accounts.CMSG_LIST_ACCOUNT()

    End Sub

    Friend Sub UpdateName(ByRef account_id As Int32, _
                           ByRef new_name As String)

        UpdateAccount(account_id, NAME_VARIABLE, new_name, True)
        For Each name As String In accountsNameKeysDictionary.Keys
            If accountsNameKeysDictionary(name) = account_id Then
                accountsNameKeysDictionary.Remove(name)
                Exit For
            End If
        Next
        accountsNameKeysDictionary.Add(new_name, account_id)

    End Sub

    Friend Sub DeleteAccount(ByRef id As Int32)

        GlobalVariables.Accounts.CMSG_DELETE_ACCOUNT(id)
        'Dim account As Hashtable = GlobalVariables.Accounts.m_accountsHash(id)

        'If Not account(IS_TMP_ID) Then
        '    If m_CRUDOperations.ContainsKey(id) Then m_CRUDOperations(id) = CRUDAction.DELETE Else m_CRUDOperations.Add(id, CRUDAction.DELETE)
        'Else
        '    m_CRUDOperations.Remove(id)
        'End If
        'accountsNameKeysDictionary.Remove(account(NAME_VARIABLE))
        'GlobalVariables.Accounts.DeleteAccount(id)
    End Sub

#End Region

#Region "Checks"

    Friend Function IsUsedName(ByRef p_name As String) As Boolean
        For Each fact In GlobalVariables.GlobalFacts.m_globalFactHash
            If fact.Value(NAME_VARIABLE) = p_name Then Return True
        Next
        For Each account In GlobalVariables.Accounts.m_accountsHash
            If account.Value(NAME_VARIABLE) = p_name Then Return True
        Next
        Return False
    End Function

    Friend Function ExistingDependantAccounts(ByRef node As TreeNode) As String()

        Dim dependantAccountsNames As New List(Of String)
        Dim accountsKeyList As List(Of Int32) = TreeViewsUtilities.GetNodesKeysList(node)
        accountsKeyList.Reverse()

        Dim dependantAccountsId() As Int32 = DependenciesLoopCheck(accountsKeyList).ToArray
        For Each dependantAccountId As Int32 In dependantAccountsId
            dependantAccountsNames.Add(GlobalVariables.Accounts.m_accountsHash(dependantAccountId)(NAME_VARIABLE))
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

        For Each accountId As Int32 In GlobalVariables.Accounts.m_accountsHash.Keys
            Dim formula As String = GlobalVariables.Accounts.m_accountsHash(accountId)(ACCOUNT_FORMULA_VARIABLE)
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

        For Each specialCharacter As Char In ACCOUNTS_FORBIDDEN_CHARACTERS
            If NameStr.Contains(specialCharacter) Then Return False
        Next
        Return True

    End Function

    Friend Function GetCurrentParsedFormula() As String

        Return FormulasTranslator.currentDBFormula

    End Function

    ' 1. Tokens check
    Friend Function CheckFormulaForUnkonwnTokens(ByRef str As String) As List(Of String)

        Return FormulasTranslator.GetDBFormulaFromHumanFormula(str)

    End Function

    ' 2. Formula syntax validity
    Friend Function CheckFormulaValidity(ByRef str As String)

        Return FormulasTranslator.testFormula()

    End Function

    ' 3. interdependancies 
    Friend Function InterdependancyTest() As Boolean

        Dim dependancies_dict As New Dictionary(Of Int32, List(Of Int32))
        Dim accounts_list = TreeViewsUtilities.GetNodesKeysList(AccountsTV)
        For Each account_id In accounts_list
            Dim ftype As String = GlobalVariables.Accounts.m_accountsHash(account_id)(ACCOUNT_FORMULA_TYPE_VARIABLE)
            If ftype <> GlobalEnums.FormulaTypes.HARD_VALUE_INPUT _
            AndAlso ftype <> GlobalEnums.FormulaTypes.TITLE Then
                If dependancies_dict.ContainsKey(account_id) = False Then AddDependantToDependanciesDict(account_id, dependancies_dict)
                For Each dependant_id In dependancies_dict(account_id)
                    If CheckDependantsInterdependancy(account_id, dependant_id, dependancies_dict) = False Then
                        MsgBox("An interdependancy has been introduced inot accounts formula: " & Chr(13) & Chr(13) & _
                               GlobalVariables.Accounts.m_accountsHash(dependant_account_id)(NAME_VARIABLE) & " depends on " & GlobalVariables.Accounts.m_accountsHash(account_id)(NAME_VARIABLE) & Chr(13) & Chr(13) & _
                               "The formula cannot therefore be saved.")
                        Return False
                    End If
                Next
            End If
        Next
        Return True

    End Function

#Region "Formulas Interdependancies checks"

    Private Function CheckDependantsInterdependancy(ByRef account_id As String, _
                                                    ByRef dependant_id As String, _
                                                    ByRef dependancies_dict As Dictionary(Of Int32, List(Of Int32))) _
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

    Private Sub AddDependantToDependanciesDict(ByRef dependant_id As Int32, _
                                               ByRef dependancies_dict As Dictionary(Of Int32, List(Of Int32)))

        If GlobalVariables.Accounts.m_accountsHash(dependant_id)(ACCOUNT_FORMULA_TYPE_VARIABLE) = GlobalEnums.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS Then
            dependancies_dict.Add(dependant_id, TreeViewsUtilities.GetChildrenIDList(AccountsTV.Nodes.Find(dependant_id, True)(0)))
        Else
            dependancies_dict.Add(dependant_id, FormulasTranslator.GetFormulaDependantsLIst(dependant_id))
        End If

    End Sub

#End Region

    Friend Function FormulaTypeChangeImpliesFactsDeletion(ByRef account_id As Int32, ByRef new_formula_type As Int32) As Boolean

        Dim currentFType As Int32 = GlobalVariables.Accounts.m_accountsHash(account_id)(ACCOUNT_FORMULA_TYPE_VARIABLE)
        If currentFType = GlobalEnums.FormulaTypes.HARD_VALUE_INPUT _
        Or currentFType = GlobalEnums.FormulaTypes.FIRST_PERIOD_INPUT Then
            If new_formula_type = GlobalEnums.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS _
            Or new_formula_type = GlobalEnums.FormulaTypes.FORMULA Then
                Return True
            End If
        End If
        Return False

    End Function

#End Region


#Region "Events"

    Private Sub AccountUpdateFromServer(ByRef status As Boolean, ByRef accountsAttributes As Hashtable)

        If status = True _
        AndAlso AccountsTV.Nodes.Find(accountsAttributes(ID_VARIABLE), True).Length = 0 _
        AndAlso isClosing = False Then
            m_view.TVUpdate(accountsAttributes(ID_VARIABLE), _
                          accountsAttributes(PARENT_ID_VARIABLE), _
                          accountsAttributes(NAME_VARIABLE), _
                          accountsAttributes(IMAGE_VARIABLE))

            ' -> view update attributes view -> 

            InstanceVariablesLoading()
        End If

    End Sub

    Private Sub AccountDeleteFromServer(ByRef status As Boolean, ByRef id As UInt32)

        If status = True _
        AndAlso isClosing = False Then
            m_view.TVNodeDelete(id)
            InstanceVariablesLoading()
        End If

    End Sub

    Private Sub AccountCreateConfirmation(ByRef status As Boolean, ByRef id As Int32)

        If status = False Then
            MsgBox("The account could not be created." & Chr(13) & _
                   "Error " & "")
            ' display error from error (to be catched in account) priority normal 
        End If

    End Sub

    Private Sub AccountUpdateConfirmation(ByRef status As ErrorMessage, ByRef id As Int32)

        If status <> ErrorMessage.SUCCESS Then
            Dim errorMsg As String

            Select Case status
                Case ErrorMessage.SYNTAX
                    errorMsg = "Syntax incorrect."
                Case ErrorMessage.INVALID_ATTRIBUTE
                    errorMsg = "Invalid attribute."
                Case ErrorMessage.NOT_FOUND
                    errorMsg = "One or more of formula's defined elements does not exist."
                Case Else
                    errorMsg = "Unknown error."
            End Select

            MsgBox("The account could not be created." & Chr(13) & _
                   "Error: " & errorMsg)
        End If


    End Sub

#End Region


#Region "Utilities"

    Friend Function GetFormatedAccountFormula(ByRef p_accountId As Int32) As String
        If GlobalVariables.Accounts.m_accountsHash.ContainsKey(p_accountId) = False Then Return ""
        Return FormulasTranslator.GetHumanFormulaFromDB(GlobalVariables.Accounts.m_accountsHash(p_accountId)(ACCOUNT_FORMULA_VARIABLE))
    End Function

    Friend Sub DisplayAccountsView()

        NewAccountView.Hide()

    End Sub

    Friend Sub DisplayNewAccountView(ByRef parent_node As TreeNode)

        NewAccountView.parentNodeId = parent_node.Name
        NewAccountView.Show()

    End Sub

    Friend Sub SendNewPositionsToModel()

        Dim position As Int32
        Dim accountsUpdates As New List(Of Tuple(Of Int32, String, Int32))
        positionsDictionary = TreeViewsUtilities.GeneratePositionsDictionary(AccountsTV)
        For Each account_id As Int32 In positionsDictionary.Keys
            position = positionsDictionary(account_id)
            If position <> GlobalVariables.Accounts.m_accountsHash(account_id)(ITEMS_POSITIONS) Then
                UpdateAccount(account_id, ITEMS_POSITIONS, position)
            End If
        Next
        Save()

    End Sub

    Friend Function GetFormulaText(ByRef accountId As Int32) As String

        Return FormulasTranslator.GetHumanFormulaFromDB(ReadAccount(accountId, ACCOUNT_FORMULA_VARIABLE))

    End Function

#End Region



End Class
