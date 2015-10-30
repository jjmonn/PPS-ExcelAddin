'FormulasTranslation.vb
'
'Translates human readable formulas into machine language formulas (financialbi storage format , eg:"account_id#p1")
'
'
'To do:
'
'
'Author: Julien Monnereau
'Last modified: 03/09/2015


Imports System.Collections
Imports System.Collections.Generic
Imports System.Text.RegularExpressions
Imports System.Linq
Imports CRUD

Friend Class FormulasTranslations


#Region "Instance Variables and constants"

#Region "Common"

    ' Variables
    Friend parser_functions As New List(Of String)
    Friend error_tokens As List(Of String)
 
    ' Constants
    Friend Const FORMULA_SEPARATOR As Char = "#" ' to be checkek if ok
    Friend Const ACCOUNT_IDENTIFIER As String = "acc"
    Friend Const PARSER_FORMULAS As String = "if,sin,cos,tan,log2,log10,log,ln,exp,sqrt,sign,rint,abs,min,max,sum,avg" ' to be reviewed + case insensitive !! priority high
    Friend Const ACCOUNTS_HUMAN_IDENTIFIER As Char = Chr(34) '"'"
    Friend Const FACTS_HUMAN_IDENTIFIER As Char = Chr(34)
    Friend Const FACTS_IDENTIFIER As String = "gfact"

    ' Periods
    Friend Const PERIODS_SEPARATOR_START As Char = "["
    Friend Const PERIODS_SEPARATOR_END As Char = "]"
    Friend Const PERIODS_DB_SEPARATOR As Char = "T"
    Friend Const RELATIVE_PERIODS_IDENTIFIER As Char = "n"
    Friend Const PERIODS_DB_PLUS As Char = "p"
    Friend Const PERIODS_DB_MINUS As Char = "m"
    Friend Const PERIODS_DB_AGGREGATION_IDENTIFIER As Char = "I"
    Private Const PERIOD_REGEX_GROUP As UInt16 = 5
    Friend Const PERIODS_HUMAN_AGG_IDENTIFIER As Char = ":"

#End Region

#Region "From Human to DataBase"

    Friend currentDBFormula As String

    ' Accounts Regex
    Private regexOperatorsStr As String = "/\(\)\+\-\*\=\<\>\^\?\:\;\!"
    Private accRegexStr As String = "([^\[" & regexOperatorsStr & "][\w\s]+)\[?[^\]" & regexOperatorsStr & "]"  ' & OPERATORS & "\w]"
    Private accountsHumanToDBRegex As Regex = New Regex(accRegexStr, RegexOptions.IgnoreCase)

    Private accGuillemetStr As String = Chr(34) & "([\w\s\(\)'’]+)" & Chr(34) & "\[?"  'ACCOUNTS_HUMAN_IDENTIFIYER & "([\w\s]+)" & ACCOUNTS_HUMAN_IDENTIFIYER & "\[?"
    Private guillemetsHumanToDBRegex As Regex = New Regex(accGuillemetStr, RegexOptions.IgnoreCase)

    ' facts Regex
    Private factHashtagStr As String = FACTS_HUMAN_IDENTIFIER & "([\w\s\(\)'’]+)" & FACTS_HUMAN_IDENTIFIER & "\[?"
    Private factHashtagToDBRegex As Regex = New Regex(factHashtagStr, RegexOptions.IgnoreCase)

    ' Periods Regex
    Private period_str_regex As String = "[^\[\]]*(((?'Open'\[)[^\[\]]*)+((?'Close-Open'\])[^\[\]]*)+)*(?(Open)(?!))"
    Private periodsHumanToDBregex As Regex = New Regex(period_str_regex, RegexOptions.IgnoreCase)

#End Region

#Region "From DB to Human"

    ' Accounts parsing
    Private accStr As String = ACCOUNT_IDENTIFIER & "([0-9]+)\" & PERIODS_SEPARATOR_START
    Private accountsDBToHumanRegex As Regex = New Regex(accStr)

    Private factStr As String = FACTS_IDENTIFIER & "([0-9]+)\" & PERIODS_SEPARATOR_START
    Private factsDBToHumanRegex As Regex = New Regex(factStr)

    Private dependanciesCheckStr As String = ACCOUNT_IDENTIFIER & "([0-9]+)" & PERIODS_DB_SEPARATOR
    Private dependanciesCheckRegex As Regex = New Regex(dependanciesCheckStr)

    ' Periods parsing
    Private periodsStr As String = PERIODS_DB_SEPARATOR & _
                                   "([" & _
                                   RELATIVE_PERIODS_IDENTIFIER & _
                                   PERIODS_DB_PLUS & _
                                   PERIODS_DB_MINUS & _
                                   PERIODS_DB_AGGREGATION_IDENTIFIER & _
                                   "0-9]+)"
    Private periodsDBToHumanRegex As Regex = New Regex(periodsStr)

#End Region

#End Region


#Region "Initialize"

    Friend Sub New()

        ' caution => accounts name/ parser functions !!! priority high
        Dim parsers_functions_array As String() = Split(PARSER_FORMULAS, ",")
        For Each item As String In parsers_functions_array
            parser_functions.Add(item)
        Next

    End Sub

#End Region


#Region "Parsing From Human to DB"

    Friend Function GetDBFormulaFromHumanFormula(ByVal h_formula As String) As List(Of String)

        currentDBFormula = ""
        error_tokens = New List(Of String)
        If h_formula.Count(Function(c As Char) c = "(") <> h_formula.Count(Function(c As Char) c = "(") Then
            MsgBox("There is an error with parentheses in your function.")
            Return error_tokens
        End If
        If h_formula.Count(Function(c As Char) c = "[") <> h_formula.Count(Function(c As Char) c = "]") Then
            MsgBox("There is an error with periods braces in your function.")
            Return error_tokens
        End If

        TokensAccountIdentifier(h_formula)
        TokensFactIdentifier(h_formula)
        ParsePeriods(h_formula)
        currentDBFormula = h_formula
        Return error_tokens

    End Function

#Region "Tokens Creation"

    Private Sub TokensAccountIdentifier(ByRef human_formula As String)

        Dim tmpStr As String = human_formula
        Dim matchStr As String
        Dim accountName, accountId As String
        Dim m As Match = guillemetsHumanToDBRegex.Match(tmpStr)
        If (m.Success) Then
            While m.Success

                matchStr = m.Groups(0).Value
                accountName = m.Groups(1).Value

                accountId = GlobalVariables.Accounts.GetValueId(accountName)
                If accountId <> 0 Then

                    If matchStr.IndexOf(PERIODS_SEPARATOR_START, 0) > 0 Then
                        ' human_formula = human_formula.Replace(accountName, ACCOUNT_IDENTIFIER & accountId)
                        human_formula = Replace(human_formula, matchStr, ACCOUNT_IDENTIFIER & accountId & PERIODS_SEPARATOR_START, 1, 1)
                    Else
                        human_formula = Replace(human_formula, _
                                                matchStr, _
                                                ACCOUNT_IDENTIFIER & accountId & _
                                                PERIODS_SEPARATOR_START & _
                                                RELATIVE_PERIODS_IDENTIFIER & _
                                                PERIODS_SEPARATOR_END, _
                                                1, 1)
                        'human_formula = human_formula.Replace(matchStr, _
                        '                                      ACCOUNT_IDENTIFIER & accountId & _
                        '                                      PERIODS_SEPARATOR_START & _
                        '                                      RELATIVE_PERIODS_IDENTIFIER & _
                        '                                      PERIODS_SEPARATOR_END)
                    End If
                ElseIf GlobalVariables.GlobalFacts.GetValueId(accountName) = 0 Then
                    error_tokens.Add(accountName)
                End If
                m = m.NextMatch
            End While
        End If

    End Sub

    Private Sub TokensFactIdentifier(ByRef human_formula As String)

        Dim tmpStr As String = human_formula
        Dim matchStr As String
        Dim factName, factId As String
        Dim m As Match = factHashtagToDBRegex.Match(tmpStr)
        If (m.Success) Then
            While m.Success

                matchStr = m.Groups(0).Value
                factName = m.Groups(1).Value

                factId = GlobalVariables.GlobalFacts.GetValueId(factName)
                If factId <> 0 Then

                    If matchStr.IndexOf(PERIODS_SEPARATOR_START, 0) > 0 Then
                        human_formula = Replace(human_formula, matchStr, FACTS_IDENTIFIER & factId & PERIODS_SEPARATOR_START, 1, 1)
                    Else
                        human_formula = Replace(human_formula, _
                                                matchStr, _
                                                FACTS_IDENTIFIER & factId & _
                                                PERIODS_SEPARATOR_START & _
                                                RELATIVE_PERIODS_IDENTIFIER & _
                                                PERIODS_SEPARATOR_END, _
                                                1, 1)
                    End If
                ElseIf GlobalVariables.Accounts.GetValueId(factName) = 0 Then
                    error_tokens.Add(factName)
                End If
                m = m.NextMatch
            End While
        End If

    End Sub

    Private Sub ParsePeriods(ByRef str As String)

        Dim m As Match = periodsHumanToDBregex.Match(str)
        If (m.Success) Then
            For Each cap As Capture In m.Groups(PERIOD_REGEX_GROUP).Captures
                Dim tmp_val As String = cap.Value.Replace("+", PERIODS_DB_PLUS)
                tmp_val = tmp_val.Replace("-", PERIODS_DB_MINUS)
                tmp_val = tmp_val.Replace(PERIODS_HUMAN_AGG_IDENTIFIER, PERIODS_DB_AGGREGATION_IDENTIFIER)
                str = Replace(str, _
                              PERIODS_SEPARATOR_START & cap.Value & PERIODS_SEPARATOR_END, _
                              PERIODS_SEPARATOR_START & tmp_val & PERIODS_SEPARATOR_END, _
                              1, 1)
            Next
        End If
        str = Replace(str, PERIODS_SEPARATOR_START, PERIODS_DB_SEPARATOR)
        str = Replace(str, PERIODS_SEPARATOR_END, "")

    End Sub


#End Region

#End Region


#Region "Parsing From DB to Human"

    Friend Function GetHumanFormulaFromDB(ByVal formulaStr As String) As String

        If formulaStr = "" Then Return ""
        ParsePeriodsTokenFromDB(formulaStr)
        ParseFactsTokenFromDB(formulaStr)
        ParseAccountsTokenFromDB(formulaStr)
        Return formulaStr

    End Function

    Private Sub ParseAccountsTokenFromDB(ByRef formulaStr As String)

        Dim accountId As Int32
        Dim accountName As String
        Dim l_account As Account
        Dim str_copy As String = formulaStr
        Dim m As Match = accountsDBToHumanRegex.Match(str_copy)
        If (m.Success) Then
            While m.Success
                If m.Groups(1).Captures.Count > 1 Then
                    System.Diagnostics.Debug.Write("Error in regex parsing from DB to Human: Identified several Accounts IDs.")
                End If
                accountId = m.Groups(1).Captures(0).Value
                l_account = GlobalVariables.Accounts.GetValue(accountId)

                If l_account Is Nothing Then Exit While
                accountName = l_account.Name
                formulaStr = Replace(formulaStr, m.Groups(0).Value, ACCOUNTS_HUMAN_IDENTIFIER & _
                                                                    accountName & _
                                                                    ACCOUNTS_HUMAN_IDENTIFIER & _
                                                                    PERIODS_SEPARATOR_START)
                m = m.NextMatch
            End While
        End If

    End Sub

    Private Sub ParseFactsTokenFromDB(ByRef formulaStr As String)

        Dim factId As UInt32
        Dim str_copy As String = formulaStr
        Dim m As Match = factsDBToHumanRegex.Match(str_copy)
        If (m.Success) Then
            While m.Success
                If m.Groups(1).Captures.Count > 1 Then
                    System.Diagnostics.Debug.Write("Error in regex parsing from DB to Human: Identified several Accounts IDs.")
                End If
                factId = m.Groups(1).Captures(0).Value
                Dim fact As GlobalFact = GlobalVariables.GlobalFacts.GetValue(factId)
                If fact Is Nothing Then Exit Sub

                formulaStr = Replace(formulaStr, m.Groups(0).Value, FACTS_HUMAN_IDENTIFIER & _
                                                                    fact.Name & _
                                                                    FACTS_HUMAN_IDENTIFIER & _
                                                                    PERIODS_SEPARATOR_START)
                m = m.NextMatch
            End While
        End If

    End Sub

    Private Sub ParsePeriodsTokenFromDB(ByRef formulaStr As String)

        Dim str_copy As String = formulaStr
        Dim tmpStr As String
        Dim m As Match = periodsDBToHumanRegex.Match(str_copy)
        If (m.Success) Then
            While m.Success
                tmpStr = m.Groups(1).Value
                tmpStr = Replace(tmpStr, PERIODS_DB_PLUS, "+")
                tmpStr = Replace(tmpStr, PERIODS_DB_MINUS, "-")
                tmpStr = Replace(tmpStr, PERIODS_DB_AGGREGATION_IDENTIFIER, PERIODS_HUMAN_AGG_IDENTIFIER)
                formulaStr = Replace(formulaStr, m.Groups(0).Value, PERIODS_SEPARATOR_START & tmpStr & PERIODS_SEPARATOR_END, 1, 1)
                m = m.NextMatch
            End While
        End If

    End Sub

#End Region


#Region "Utilities"

    Friend Function GetFormulaDependantsLIst(ByRef accountid As UInt32) As List(Of UInt32)

        Dim dependantId As UInt32
        Dim dependants_list As New List(Of UInt32)
        Dim l_account As Account = GlobalVariables.Accounts.GetValue(accountid)

        If l_account Is Nothing Then Return dependants_list
        Dim formula_str As String = l_account.Formula
        Dim m As Match = dependanciesCheckRegex.Match(formula_str)
        If (m.Success) Then
            While m.Success
                dependantId = m.Groups(1).Captures(0).Value
                If dependantId <> accountid Then dependants_list.Add(dependantId)
                m = m.NextMatch
            End While
        End If
        Return dependants_list

    End Function

    Friend Function testFormula() As Boolean

        ' to be reimplemented
        Return True

    End Function


#End Region


End Class
