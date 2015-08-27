'FormulasTranslation.vb
'
'Translates human readable formulas into machine language formulas (financialbi storage format , eg:"account_id#p1")
'
'
'To do:
'   - Replace "<>" by "!="
'
'
'Author: Julien Monnereau
'Last modified: 06/07/2015


Imports System.Collections
Imports System.Collections.Generic
Imports System.Text.RegularExpressions
Imports System.Linq


Friend Class FormulasTranslations


#Region "Instance Variables and constants"

    ' Objects
    Private if_reg As Regex = New Regex("if\(([^\(\)]*);([^\(\)]*);([^\(\)]*)\)", RegexOptions.IgnoreCase)
    Private inner_str As String = "((?'Open'\()[^\(\)]*)+(?'Close-Open'\)[^\(\)]*)+)*(?(Open)(?!)"
    Private outer_str As String = "if\(" & _
                                        "(" & inner_str & "|[^\(\)]*)" & _
                                        "(" & inner_str & "|[^\(\)]*)" & _
                                        "(" & inner_str & "|[^\(\)]*)" & _
                                      "\)"
    Private outer_reg As Regex = New Regex(outer_str, RegexOptions.IgnoreCase)
    Private period_str_regex As String = "[^\[\]]*(((?'Open'\[)[^\[\]]*)+((?'Close-Open'\])[^\[\]]*)+)*(?(Open)(?!))"
    Private Const NESTED_IF_GROUP_INDEX As UInt16 = 8

    Dim periods_regex As Regex = New Regex(period_str_regex, RegexOptions.IgnoreCase)

    ' Variables
    Private AccountsNameKeyDictionary As Hashtable
    Friend operators As New List(Of Char)
    Friend parser_functions As New List(Of String)
    Friend error_tokens As List(Of String)

    ' Constants
    Friend Const FORMULA_SEPARATOR As String = "|" ' NOT OK !!!!!! -> "or" operator !!!!!!!!!!
    Friend Const ACCOUNT_IDENTIFIER As String = "acc"
    Private Const TERNARY_SEPRATOR_1 As String = "?"
    Private Const TERNARY_SEPRATOR_2 As String = ":"
    Friend Const FORMULAS_TOKEN_CHARACTERS As String = "()+-*/=<>^?:;!"
    Friend Const PARSER_FORMULAS As String = "if,sin,cos,tan,log2,log10,log,ln,exp,sqrt,sign,rint,abs,min,max,sum,avg"

    ' Periods
    Friend Const PERIODS_SEPARATOR_START As String = "["
    Friend Const PERIODS_SEPARATOR_END As String = "]"
    Friend Const PERIODS_DB_SEPARATOR As String = "#"
    Friend Const RELATIVE_PERIODS_IDENTIFIER As String = "n"
    Friend Const PERIODS_DB_PLUS As String = "p"
    Friend Const PERIODS_DB_MINUS As String = "m"
    Private Const PERIOD_REGEX_GROUP As UInt16 = 5


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


    Friend Function GetDBFormulaFromHumanFormula(ByRef h_formula As String) As Boolean

        error_tokens = New List(Of String)
        If h_formula.Count(Function(c As Char) c = "(") <> h_formula.Count(Function(c As Char) c = "(") Then
            MsgBox("There is an error with parentheses in your function.")
            Return False
        End If
        If h_formula.Count(Function(c As Char) c = "[") <> h_formula.Count(Function(c As Char) c = "]") Then
            MsgBox("There is an error with periods braces in your function.")
            Return False
        End If

        ParsePeriods(h_formula)
        TokensIdentifier(h_formula)
        If error_tokens.Count > 0 Then Return False
        TransformTernaryOperator(h_formula)
        Return True

    End Function


#Region "Ternary operator Parsing"

    Private Sub TransformTernaryOperator(ByRef global_str As String)

        ' attention only for nested if()s?

        Dim previous_cap_values As String() = {"", ""}
        Dim m As Match = outer_reg.Match(global_str)
        If (m.Success) Then
            While m.Success
                Dim cap_index As UInt16 = 0
                Dim grp As Group = m.Groups(NESTED_IF_GROUP_INDEX)
                For Each cap As Capture In grp.Captures

                    Dim capture_value_copy As String = cap.Value        ' is overwritten if parsed

                    Dim parsing_result As Boolean = ParseInnerFormula(capture_value_copy, _
                                              global_str, _
                                              previous_cap_values)
                    If cap_index + 1 < grp.Captures.Count _
                    AndAlso grp.Captures(cap_index + 1).Value.Contains(cap.Value) _
                    AndAlso parsing_result = True Then
                        ' Current capture is nested in next capture
                        previous_cap_values = {"if(" & cap.Value & ")", capture_value_copy}
                    Else
                        ' Current capture is stand alone
                        previous_cap_values = {"", ""}
                    End If

                    cap_index += 1
                Next
                m = m.NextMatch
            End While
        End If
        global_str = ParseIfFormula(global_str)

    End Sub

    ' Input: if() function
    Private Function ParseInnerFormula(ByRef str As String, _
                                       ByRef global_str As String, _
                                       ByRef previous_cap_values As String()) As Boolean

        Dim base_str As String = "if(" & str & ")"
        If previous_cap_values(0) <> "" Then
            base_str = Replace(base_str, previous_cap_values(0), previous_cap_values(1))
        End If
        Dim new_str As String = ParseIfFormula(base_str)
        If base_str <> new_str _
        AndAlso base_str.Contains(base_str) Then
            global_str = Replace(global_str, base_str, new_str)
            str = new_str
            Return True
        Else
            Return False
        End If

    End Function

    ' Transform a formula from 'if(;;)' to '?:' 
    ' Non recursive, input must be the most nested
    Private Function ParseIfFormula(str) As String

        Dim m As Match = if_reg.Match(str)
        Do While m.Success
            str = Replace(str, m.Value, m.Groups(1).Value & "?" & _
                                        m.Groups(2).Value & ":" & _
                                        m.Groups(3).Value)
            m = m.NextMatch
        Loop
        Return str

    End Function

#End Region


#Region "Tokens Creation"

    Private Sub TokensIdentifier(ByRef human_formula As String)

        Dim i As Integer
        Dim parsed_account As String
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

    '' Input:    "" , "[n+1]" , "[n-1]" , "[0]" , "[n:n+11]" 
    '' Output: "#n" , "#np1"  , "#nm1"  , "#0"  , "#n#np11"
    'Private Sub ParsePeriodReference(ByRef period_str As String)

    '        Replace(period_str, "+", PERIODS_DB_PLUS)
    '        Replace(period_str, "-", PERIODS_DB_MINUS)
    '        Replace(period_str, ":", PERIODS_DB_SEPARATOR)

    'End Sub


#End Region


End Class
