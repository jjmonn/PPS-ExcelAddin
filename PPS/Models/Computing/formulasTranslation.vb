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


Friend Class FormulasTranslations


#Region "Instance Variables and constants"


#Region "Common"

    ' Variables
    Friend operators As New List(Of Char)
    Friend parser_functions As New List(Of String)
    Friend error_tokens As List(Of String)

    ' Constants
    Friend Const FORMULA_SEPARATOR As Char = "#" ' to be checkek if ok
    Friend Const ACCOUNT_IDENTIFIER As String = "acc"
    Friend Const FORMULAS_TOKEN_CHARACTERS As String = "()+-*/=<>^?:;!" ' to be reviewed - 
    Friend Const PARSER_FORMULAS As String = "if,sin,cos,tan,log2,log10,log,ln,exp,sqrt,sign,rint,abs,min,max,sum,avg" ' to be reviewed + case insensitive !! priority high

    ' Periods
    Friend Const PERIODS_SEPARATOR_START As Char = "["
    Friend Const PERIODS_SEPARATOR_END As Char = "]"
    Friend Const PERIODS_DB_SEPARATOR As Char = "T"
    Friend Const RELATIVE_PERIODS_IDENTIFIER As Char = "n"
    Friend Const PERIODS_DB_PLUS As Char = "p"
    Friend Const PERIODS_DB_MINUS As Char = "m"
    Friend Const PERIODS_AGGREGATION_IDENTIFIER As Char = "I"
    Private Const PERIOD_REGEX_GROUP As UInt16 = 5

    Friend Const PERIODS_HUMAN_AGG_IDENTIFIER As Char = ":"


#End Region


#Region "From Human to DataBase"

    Private period_str_regex As String = "[^\[\]]*(((?'Open'\[)[^\[\]]*)+((?'Close-Open'\])[^\[\]]*)+)*(?(Open)(?!))"
    Private periods_regex As Regex = New Regex(period_str_regex, RegexOptions.IgnoreCase)
    Private AccountsNameKeyDictionary As Hashtable

#End Region


#Region "From DB to Human"

    ' Accounts parsing
    Private accStr As String = ACCOUNT_IDENTIFIER & "([0-9]+)\" & PERIODS_SEPARATOR_START
    Private accountsTokenRegex As Regex = New Regex(accStr, RegexOptions.IgnoreCase)

    ' Periods parsing
    Private periodsStr As String = PERIODS_DB_SEPARATOR & _
                                   "([" & _
                                   RELATIVE_PERIODS_IDENTIFIER & _
                                   PERIODS_DB_PLUS & _
                                   PERIODS_DB_MINUS & _
                                   PERIODS_AGGREGATION_IDENTIFIER & _
                                   "0-9]+)"
    Private periodsTokenRegex As Regex = New Regex(periodsStr, RegexOptions.IgnoreCase)

#End Region


#End Region


#Region "Initialize"

    Friend Sub New(ByRef inputAccountsNameKeysDictionary As Hashtable)

        AccountsNameKeyDictionary = inputAccountsNameKeysDictionary
        For Each item As Char In FORMULAS_TOKEN_CHARACTERS
            operators.Add(item)
        Next

        Dim parsers_functions_array As String() = Split(PARSER_FORMULAS, ",")
        For Each item As String In parsers_functions_array
            parser_functions.Add(item)
        Next

    End Sub

#End Region


#Region "Parsing From Human to DB"

    Friend Function GetDBFormulaFromHumanFormula(ByRef h_formula As String) As List(Of String)

        error_tokens = New List(Of String)
        If h_formula.Count(Function(c As Char) c = "(") <> h_formula.Count(Function(c As Char) c = "(") Then
            MsgBox("There is an error with parentheses in your function.")
            Return error_tokens
        End If
        If h_formula.Count(Function(c As Char) c = "[") <> h_formula.Count(Function(c As Char) c = "]") Then
            MsgBox("There is an error with periods braces in your function.")
            Return error_tokens
        End If

        ParsePeriods(h_formula)
        TokensIdentifier(h_formula)
        Return error_tokens

    End Function

#Region "Tokens Creation"

    Private Sub TokensIdentifier(ByRef human_formula As String)

        Dim i As Integer
        Dim parsed_account As String = ""
        Dim human_formula_array As String() = Split(Utilities_Functions.DivideFormula(human_formula), FORMULA_SEPARATOR)         ' Generate array from formula
        For Each item In human_formula_array
            If operators.Contains(item) = False _
            AndAlso parser_functions.Contains(item) = False _
            AndAlso Not IsNumeric(item) _
            AndAlso item <> "" Then
                human_formula_array(i) = BuildAccountPeriodToken(item)
            End If
            i = i + 1
        Next

        human_formula = ""
        For Each item In human_formula_array
            If Not item = " " Then human_formula = human_formula & item
        Next

    End Sub

    Private Function BuildAccountPeriodToken(ByVal token As String) As String

        Dim account_token As String
        Dim period_token As String = ""
        Dim period_start_position As Int16 = token.IndexOf(PERIODS_DB_SEPARATOR)

        If period_start_position <> -1 Then
            account_token = Left(token, period_start_position)
            period_token = Right(token, Len(token) - period_start_position)
        Else
            account_token = token
            period_token = PERIODS_DB_SEPARATOR & RELATIVE_PERIODS_IDENTIFIER
        End If
        If AccountsNameKeyDictionary.ContainsKey(account_token) Then

            Return AccountsNameKeyDictionary(account_token) & period_token
        Else
            error_tokens.Add(account_token)
            Return ""
        End If

    End Function

    Private Sub ParsePeriods(ByRef str As String)

        Dim m As Match = periods_regex.Match(str)
        If (m.Success) Then
            Dim j As UInt16
            For Each cap As Capture In m.Groups(PERIOD_REGEX_GROUP).Captures
                Dim tmp_val As String = cap.Value.Replace("+", PERIODS_DB_PLUS)
                tmp_val = tmp_val.Replace("-", PERIODS_DB_MINUS)
                str = Replace(str, _
                              PERIODS_SEPARATOR_START & cap.Value & PERIODS_SEPARATOR_END, _
                              PERIODS_SEPARATOR_START & tmp_val & PERIODS_SEPARATOR_END)
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
        ParseAccountsTokenFromDB(formulaStr)
        Return formulaStr

    End Function

    Private Sub ParseAccountsTokenFromDB(ByRef formulaStr As String)

        Dim accountId As Int32
        Dim accountName As String
        Dim str_copy As String = formulaStr
        Dim m As Match = accountsTokenRegex.Match(str_copy)
        If (m.Success) Then
            While m.Success
                If m.Groups(1).Captures.Count > 1 Then
                    System.Diagnostics.Debug.Write("Error in regex parsing from DB to Human: Identified several Accounts IDs.")
                End If
                accountId = m.Groups(1).Captures(0).Value
                accountName = GlobalVariables.Accounts.accounts_hash(accountId)(NAME_VARIABLE)
                formulaStr = Replace(formulaStr, m.Groups(0).Value, accountName & PERIODS_SEPARATOR_START)
                m = m.NextMatch
            End While
        End If

    End Sub

    Private Sub ParsePeriodsTokenFromDB(ByRef formulaStr As String)

        Dim str_copy As String = formulaStr
        Dim tmpStr As String
        Dim m As Match = periodsTokenRegex.Match(str_copy)
        If (m.Success) Then
            While m.Success
                tmpStr = m.Groups(1).Value
                tmpStr = Replace(tmpStr, PERIODS_DB_PLUS, "+")
                tmpStr = Replace(tmpStr, PERIODS_DB_MINUS, "-")
                tmpStr = Replace(tmpStr, PERIODS_AGGREGATION_IDENTIFIER, PERIODS_HUMAN_AGG_IDENTIFIER)
                formulaStr = Replace(formulaStr, m.Groups(0).Value, PERIODS_SEPARATOR_START & tmpStr & PERIODS_SEPARATOR_END)
                m = m.NextMatch
            End While
        End If

    End Sub

#End Region



End Class
