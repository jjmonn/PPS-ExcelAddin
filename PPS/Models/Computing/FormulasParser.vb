'
'
'
' To do: 
'       - tokens to be placed in DB
'
'
'
'
' Last modified: 29/12/2014
'


Imports System.Collections
Imports System.Windows.Forms
Imports System.Collections.Generic


Friend Class FormulasParser


#Region "Instance Variables"

    ' Objects
    Private accountsTV As TreeView

    ' Variables
    Private AccountsNameKeyDictionary As Hashtable
    Private formulaStringTest As String
    Public Property keysFormulaString As String
    Public Property errorList As New List(Of String)
    Private tokensList As List(Of String)
    Private formula_array() As String

    ' Constants
    Friend Shared ReadOnly FORMULAS_TOKEN_CHARACTERS As String() = {"(", ")", "+", "-", "*", "/", "=", "<", ">", "^", "?", ":", ";", "!"}


#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef inputAccountsNameKeysDictionary As Hashtable, _
                   ByRef inputAccountsTV As TreeView)

        AccountsNameKeyDictionary = inputAccountsNameKeysDictionary
        accountsTV = inputAccountsTV
        tokensList = New List(Of String)
        For Each item In FORMULAS_TOKEN_CHARACTERS
            tokensList.Add(item)
        Next

    End Sub

#End Region


#Region "Interface"

 
    Friend Function testFormula() As Boolean

        'buildTestFormulaString()
        'Return GlobalVariables.GenericGlobalSingleEntityComputer.CheckParserFormula(formulaStringTest)
        Return True

    End Function

    Friend Function GetFormulaDependantsLIst(ByRef formula_str As String) As List(Of String)

        Dim dependants_list As New List(Of String)
        Dim formulaArray() As String
        Dim accountKey As String
        Dim i As Integer

        formulaArray = Split(Utilities_Functions.DivideFormula(formula_str), FormulasTranslations.FORMULA_SEPARATOR)         ' Generate array from formula
        For Each item In formulaArray
            If Not tokensList.Contains(item) AndAlso Not IsNumeric(item) AndAlso item <> "" Then
                item = LTrim(item)
                item = RTrim(item)
                accountKey = AccountsNameKeyDictionary.Item(item)
                If Not accountKey = "" Then
                    dependants_list.Add(accountKey)
                End If
            End If
            i = i + 1
        Next
        Return dependants_list

    End Function

    ' Uses the evalauate function of the application to see whether we can compute or not
    Private Function Eval(Ref As String) As Object

        Ref = Replace(Ref, ",", ".")
        Eval = GlobalVariables.apps.Evaluate(Ref)

    End Function


#End Region


#Region "Utilities"

    ' Replace accounts by random values in order to build a test expression
    Friend Sub buildTestFormulaString()

        Dim i As Int32 = 0
        For Each item In formula_array
            If Not tokensList.Contains(item) And Not IsNumeric(item) Then
                formula_array(i) = 2
            End If
            i = i + 1
        Next

        formulaStringTest = ""
        For Each item In formula_array
            If Not item = " " Then formulaStringTest = formulaStringTest & item
        Next
        formulaStringTest.Replace(" ", "")

    End Sub


#End Region




End Class
