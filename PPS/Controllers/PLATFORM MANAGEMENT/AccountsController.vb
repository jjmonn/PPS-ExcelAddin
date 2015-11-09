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
Imports CRUD
Imports VIBlend.WinForms.Controls


Friend Class AccountsController

#Region "Instance Variables"

    ' Objects
    Private m_formulasTranslator As FormulasTranslations
    Private m_view As AccountsView
    Private m_newAccountView As NewAccountUI
    Private m_accountsTV As New vTreeView
    Private m_globalFactsTV As New vTreeView
    Private m_platformMGTUI As PlatformMGTGeneralUI

    ' Variables
    Friend m_positionsDictionary As New Dictionary(Of Int32, Int32)
    Private m_dependant_account_id As String
    Friend m_formulaTypesToBeTested As New List(Of Int32)
    Private m_isClosing As Boolean = False
    Private m_CRUDOperations As New Dictionary(Of Int32, CRUDAction)
    Private Delegate Sub UpdateVarDelegate(ByRef p_account As Account, ByRef p_value As Object)
    ' Constants
    Friend Const ACCOUNTS_FORBIDDEN_CHARACTERS As String = "+-*=<>^?:;![]" ' to be reviewed - 


#End Region

#Region "Initialization and Closing"

    Friend Sub New()

        GlobalVariables.Accounts.LoadAccountsTV(m_accountsTV)
        GlobalVariables.GlobalFacts.LoadGlobalFactsTV(m_globalFactsTV)
        m_view = New AccountsView(Me, m_accountsTV, m_globalFactsTV)
        InstanceVariablesLoading()
        m_positionsDictionary = VTreeViewUtil.GeneratePositionsDictionary(m_accountsTV)
        m_formulaTypesToBeTested.Add(Account.FormulaTypes.FIRST_PERIOD_INPUT)
        m_formulaTypesToBeTested.Add(Account.FormulaTypes.FORMULA)

        AddHandler GlobalVariables.Accounts.Read, AddressOf AccountReadFromServer
        AddHandler GlobalVariables.Accounts.DeleteEvent, AddressOf AccountDeleteFromServer
        AddHandler GlobalVariables.Accounts.CreationEvent, AddressOf AccountCreateConfirmation
        AddHandler GlobalVariables.Accounts.UpdateEvent, AddressOf AccountUpdateConfirmation

    End Sub

    Private Sub InstanceVariablesLoading()

        m_newAccountView = New NewAccountUI(m_view, Me)
        m_formulasTranslator = New FormulasTranslations()

    End Sub

    Public Sub addControlToPanel(ByRef dest_panel As Panel, _
                                 ByRef PlatformMGTUI As PlatformMGTGeneralUI)

        Me.m_platformMGTUI = PlatformMGTUI
        dest_panel.Controls.Add(m_view)
        m_view.Dock = Windows.Forms.DockStyle.Fill

    End Sub

    Public Sub close()

        m_isClosing = True
        SendNewPositionsToModel()
        m_view.Dispose()

    End Sub

#End Region

#Region "CRUD Interface"

    Friend Sub CreateAccount(ByRef p_parentId As Int32, _
                             ByRef p_name As String, _
                             ByRef p_formulaType As Int32, _
                             ByRef p_formula As String, _
                             ByRef p_type As Int32, _
                             ByRef p_consolidationOption As Int32, _
                             ByRef p_conversionOption As Int32, _
                             ByRef p_format As String, _
                             ByRef p_image As Int32, _
                             ByRef p_position As Int32, _
                             ByRef p_tab As Int32)

        Dim l_account As New Account()
        l_account.ParentId = p_parentId
        l_account.Name = p_name
        l_account.FormulaType = p_formulaType
        l_account.Formula = p_formula
        l_account.Type = p_type
        l_account.ConsolidationOptionId = p_consolidationOption
        l_account.ConversionOptionId = p_conversionOption
        l_account.FormatId = p_format
        l_account.Image = p_image
        l_account.ItemPosition = p_position
        l_account.AccountTab = p_tab

        GlobalVariables.Accounts.Create(l_account)
    End Sub

    Friend Sub UpdateAccount(ByRef p_account As Account)

        GlobalVariables.Accounts.Update(p_account)

    End Sub

    Friend Sub UpdateName(ByRef account_id As Int32, _
                           ByRef new_name As String)

        Dim l_account = GetAccountCopy(account_id)

        l_account.Name = new_name
        UpdateAccount(l_account)

    End Sub

    Friend Sub DeleteAccount(ByRef id As Int32)

        GlobalVariables.Accounts.Delete(id)

    End Sub

    Friend Sub UpdateAccountFormulaType(ByVal p_id As UInt32, ByVal p_value As Account.FormulaTypes)
        UpdateVar(p_id, p_value, New UpdateVarDelegate(Sub(ByRef p_account As Account, ByRef p_destValue As Object) p_account.FormulaType = p_destValue))
    End Sub

    Friend Sub UpdateAccountType(ByVal p_id As UInt32, ByVal p_value As Account.AccountType)
        UpdateVar(p_id, p_value, New UpdateVarDelegate(Sub(ByRef p_account As Account, ByRef p_destValue As Object) p_account.Type = p_destValue))
    End Sub

    Friend Sub UpdateAccountImage(ByVal p_id As UInt32, ByVal p_value As UInt32)
        UpdateVar(p_id, p_value, New UpdateVarDelegate(Sub(ByRef p_account As Account, ByRef p_destValue As Object) p_account.Image = p_destValue))
    End Sub


    Friend Sub UpdateAccountConversionOption(ByVal p_id As UInt32, ByVal p_value As Account.ConversionOptions)
        UpdateVar(p_id, p_value, New UpdateVarDelegate(Sub(ByRef p_account As Account, ByRef p_destValue As Object) p_account.ConversionOptionId = p_destValue))
    End Sub

    Friend Sub UpdateAccountConsolidationOption(ByVal p_id As UInt32, ByVal p_value As Account.ConversionOptions)
        UpdateVar(p_id, p_value, New UpdateVarDelegate(Sub(ByRef p_account As Account, ByRef p_destValue As Object) p_account.ConsolidationOptionId = p_destValue))
    End Sub

    Friend Sub UpdateAccountFormula(ByVal p_id As UInt32, ByVal p_value As String)
        UpdateVar(p_id, p_value, New UpdateVarDelegate(Sub(ByRef p_account As Account, ByRef p_destValue As Object) p_account.Formula = p_destValue))
    End Sub

    Friend Sub UpdateAccountDescription(ByVal p_id As UInt32, ByVal p_value As String)
        UpdateVar(p_id, p_value, New UpdateVarDelegate(Sub(ByRef p_account As Account, ByRef p_destValue As Object) p_account.Description = p_destValue))
    End Sub

    Private Sub UpdateVar(ByVal p_id As UInt32, ByVal p_value As Object, ByRef p_action As UpdateVarDelegate)
        Dim l_account = GetAccountCopy(p_id)

        If l_account Is Nothing Then Exit Sub
        p_action(l_account, p_value)
        UpdateAccount(l_account)
    End Sub

#End Region

#Region "Checks"

    Friend Function IsUsedName(ByRef p_name As String) As Boolean
        For Each fact In GlobalVariables.GlobalFacts.GetDictionary().Values
            If fact.Name = p_name Then Return True
        Next
        For Each account As Account In GlobalVariables.Accounts.GetDictionary().Values
            If account.Name = p_name Then Return True
        Next
        Return False
    End Function

    Friend Function ExistingDependantAccounts(ByRef node As VIBlend.WinForms.Controls.vTreeNode) As String()

        Dim dependantAccountsNames As New List(Of String)
        Dim accountsKeyList As List(Of UInt32) = VTreeViewUtil.GetNodesIdsUint(node)
        accountsKeyList.Reverse()

        Dim dependantAccountsId() As UInt32 = DependenciesLoopCheck(accountsKeyList).ToArray
        For Each dependantAccountId As UInt32 In dependantAccountsId
            dependantAccountsNames.Add(GetAccount(dependantAccountId).Name)
        Next

        Return dependantAccountsNames.ToArray

    End Function

    Private Function DependenciesLoopCheck(ByRef accountsKeyList As List(Of UInt32)) As List(Of UInt32)

        Dim dependenciesList As New List(Of UInt32)
        For Each key In accountsKeyList
            CheckForDependencies(key, dependenciesList)
        Next
        Dim uniqueDependenciesList As List(Of UInt32) = dependenciesList.Distinct().ToList
        For Each accountId In accountsKeyList
            If uniqueDependenciesList.Contains(accountId) Then uniqueDependenciesList.Remove(accountId)
        Next
        Return uniqueDependenciesList

    End Function

    ' Looks for the param accountKey in Accounts formulas
    Private Sub CheckForDependencies(ByRef accountKey As String, dependenciesList As List(Of UInt32))

        For Each accountId As UInt32 In GlobalVariables.Accounts.GetDictionary().Keys
            Dim formula As String = GetAccount(accountId).Formula
            If Not formula Is Nothing AndAlso formula.Contains(accountKey) Then _
                dependenciesList.Add(accountId)
        Next

    End Sub

    Friend Function AccountNameCheck(ByRef nameStr As String) As Boolean

        If nameStr = "" Then
            MsgBox("The Name of the Account cannot be empty. Please enter a Name")
            Return False
        Else
            If Not GetAccount(nameStr) Is Nothing Then
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

        Return m_formulasTranslator.currentDBFormula

    End Function

    ' 1. Tokens check
    Friend Function CheckFormulaForUnkonwnTokens(ByRef str As String) As List(Of String)

        Return m_formulasTranslator.GetDBFormulaFromHumanFormula(str)

    End Function

    ' 2. Formula syntax validity
    Friend Function CheckFormulaValidity(ByRef str As String)

        Return m_formulasTranslator.testFormula()

    End Function

    ' 3. interdependancies 
    Friend Function InterdependancyTest() As Boolean

        Dim dependancies_dict As New Dictionary(Of UInt32, List(Of UInt32))
        Dim accounts_list = VTreeViewUtil.GetNodesIds(m_accountsTV)
        For Each account_id In accounts_list
            Dim l_formulaType As Int32 = GetAccount(CUInt(account_id)).FormulaType
            If l_formulaType <> Account.FormulaTypes.HARD_VALUE_INPUT _
            AndAlso l_formulaType <> Account.FormulaTypes.TITLE Then
                If dependancies_dict.ContainsKey(account_id) = False Then AddDependantToDependanciesDict(account_id, dependancies_dict)
                For Each dependant_id In dependancies_dict(account_id)
                    If CheckDependantsInterdependancy(account_id, dependant_id, dependancies_dict) = False Then
                        MsgBox("An interdependancy has been introduced inot accounts formula: " & Chr(13) & Chr(13) & _
                               GetAccount(CUInt(m_dependant_account_id)).Name & " depends on " _
                               & GetAccount(CUInt(account_id)).Name & Chr(13) & Chr(13) & _
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
                                                    ByRef dependancies_dict As Dictionary(Of UInt32, List(Of UInt32))) _
                                                    As Boolean

        If dependant_id = account_id Then Return False
        If dependancies_dict.ContainsKey(dependant_id) = False Then AddDependantToDependanciesDict(dependant_id, dependancies_dict)
        For Each dependant_child_id In dependancies_dict(dependant_id)
            If CheckDependantsInterdependancy(account_id, dependant_child_id, dependancies_dict) = False Then
                m_dependant_account_id = dependant_child_id
                Return False
            End If
        Next
        Return True

    End Function

    Private Sub AddDependantToDependanciesDict(ByRef dependant_id As UInt32, _
                                               ByRef dependancies_dict As Dictionary(Of UInt32, List(Of UInt32)))

        If GetAccount(dependant_id).FormulaType = Account.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS Then
            Dim l_dependantNode As vTreeNode = VTreeViewUtil.FindNode(m_accountsTV, dependant_id)
            If l_dependantNode IsNot Nothing Then
                dependancies_dict.Add(dependant_id, VTreeViewUtil.GetNodesIdsUint(l_dependantNode))
            End If
        Else
            dependancies_dict.Add(dependant_id, m_formulasTranslator.GetFormulaDependantsLIst(dependant_id))
        End If

    End Sub

#End Region

    Friend Function FormulaTypeChangeImpliesFactsDeletion(ByRef p_accountId As UInt32, _
                                                          ByRef p_newFormulaType As UInt32) As Boolean

        Dim l_currentFType As Int32 = GetAccount(p_accountId).FormulaType

        Select Case l_currentFType
            Case Account.FormulaTypes.HARD_VALUE_INPUT, _
                 Account.FormulaTypes.FIRST_PERIOD_INPUT
                If p_newFormulaType = Account.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS _
                Or p_newFormulaType = Account.FormulaTypes.FORMULA _
                Or p_newFormulaType = Account.FormulaTypes.TITLE Then
                    Return True
                End If
        End Select
        Return False

    End Function

    Friend Function FormulaTypeChangeImpliesFormulaDeletion(ByRef p_accountId As UInt32, _
                                                            ByRef p_newFormulaType As UInt32) As Boolean


        Dim l_currentFType As Int32 = GetAccount(p_accountId).FormulaType

        Select Case l_currentFType
            Case Account.FormulaTypes.FORMULA, _
                 Account.FormulaTypes.FIRST_PERIOD_INPUT
                If p_newFormulaType = Account.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS _
           Or p_newFormulaType = Account.FormulaTypes.HARD_VALUE_INPUT _
           Or p_newFormulaType = Account.FormulaTypes.TITLE Then
                    Return True
                End If
        End Select
        Return False

    End Function

#End Region

#Region "Events"

    Private Sub AccountReadFromServer(ByRef status As ErrorMessage, ByRef p_account As Account)

        Dim l_node As vTreeNode = VTreeViewUtil.FindNode(m_accountsTV, p_account.Id)
        If status = ErrorMessage.SUCCESS _
        AndAlso m_isClosing = False Then
            InstanceVariablesLoading()
            If l_node Is Nothing Then
                m_view.AccountNodeAddition(p_account.Id, _
                                           p_account.ParentId, _
                                           p_account.Name, _
                                           p_account.Image)
            Else
                m_view.TVUpdate(l_node, _
                                p_account.Name, _
                                p_account.Image)
            End If
        End If

    End Sub

    Private Sub AccountDeleteFromServer(ByRef status As ErrorMessage, ByRef id As UInt32)

        If status = ErrorMessage.SUCCESS _
         AndAlso m_isClosing = False Then
            m_view.TVNodeDelete(id)
            InstanceVariablesLoading()
        End If

    End Sub

    Private Sub AccountCreateConfirmation(ByRef status As ErrorMessage, ByRef p_accountId As Int32)

        If status <> ErrorMessage.SUCCESS Then
            MsgBox("The account could not be created." & Chr(13) & "Error" & "")
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

    Friend Sub DisplayAccountsView()

        m_newAccountView.Hide()

    End Sub

    Friend Sub DisplayNewAccountView(ByRef p_parentNode As VIBlend.WinForms.Controls.vTreeNode)

        m_newAccountView.parentNodeId = p_parentNode.Value
        m_newAccountView.Show()

    End Sub

    Friend Sub SendNewPositionsToModel()

        Dim position As Int32
        Dim listAccounts As New List(Of CRUDEntity)

        m_positionsDictionary = VTreeViewUtil.GeneratePositionsDictionary(m_accountsTV)
        For Each account_id As UInt32 In m_positionsDictionary.Keys
            position = m_positionsDictionary(account_id)
            Dim l_account1 As Account = GetAccount(account_id)
            If l_account1 Is Nothing Then Continue For
            If position <> l_account1.ItemPosition Then
                Dim l_account = GetAccountCopy(account_id)
                If l_account IsNot Nothing Then
                    l_account.ItemPosition = position
                    listAccounts.Add(l_account)
                End If
            End If
        Next
        GlobalVariables.Accounts.UpdateList(listAccounts)

    End Sub

    Friend Function GetFormulaText(ByRef p_accountId As UInt32) As String

        If GetAccount(p_accountId) Is Nothing Then Return ""
        Return m_formulasTranslator.GetHumanFormulaFromDB(GetAccount(p_accountId).Formula)

    End Function

    Friend Function GetAccount(ByVal p_name As String) As Account
        Return GlobalVariables.Accounts.GetValue(p_name)
    End Function

    Friend Function GetAccount(ByVal p_accountId As UInt32) As Account
        Return GlobalVariables.Accounts.GetValue(p_accountId)
    End Function

    Friend Function GetAccountCopy(ByVal p_accountId As UInt32) As Account
        Dim l_account = GetAccount(p_accountId)

        If l_account Is Nothing Then Return Nothing
        Return l_account.Clone()
    End Function

#End Region

End Class
