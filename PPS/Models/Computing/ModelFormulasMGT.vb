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


Friend Class ModelFormulasMGT


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
    Public Shared ReadOnly FORMULAS_TOKEN_CHARACTERS As String() = {"(", ")", "+", "-", "*", "/", "=", "<", ">", "^", "?", ":", ";", "!"}


#End Region


#Region "Initialize"

    Public Sub New(ByRef inputAccountsNameKeysDictionary As Hashtable, _
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

    ' Convert a formula stored with accounts keys to accounts names
    Public Function convertFormulaFromKeysToNames(formulaKeys As String) As String

        Dim formulaArray() As String
        Dim i As Integer

        formulaArray = Split(Utilities_Functions.DivideFormula(formulaKeys), FORMULA_SEPARATOR)         ' Generate array from formula
        For Each item In formulaArray
            If accountsTV.Nodes.Find(item, True).Length > 0 Then formulaArray(i) = accountsTV.Nodes.Find(item, True)(0).Text
            i = i + 1
        Next
        Return Join(formulaArray, "")

    End Function

    ' Convert a formula stored with accounts names to accounts keys
    Public Sub convertFormulaFromNamesToKeys(formulaNames As String)

        errorList.Clear()
        Dim accountKey As String
        Dim i As Integer

        formula_array = Split(Utilities_Functions.DivideFormula(formulaNames), FORMULA_SEPARATOR)         ' Generate array from formula

        For Each item In formula_array
            If IsNumeric(Replace(item, ".", ",")) Then
                item = Replace(item, ".", ",")
                formula_array(i) = item
                i = i + 1
            End If
            If Not tokensList.Contains(item) Then
                If Not IsNumeric(item) Then
                    item = LTrim(item)
                    item = RTrim(item)
                    If Not item = "" Then
                        accountKey = AccountsNameKeyDictionary.Item(item)
                        If accountKey = "" Then
                            errorList.Add(item)
                        Else
                            formula_array(i) = accountKey
                            i = i + 1
                        End If
                    End If
                End If
            Else
                formula_array(i) = item
                i = i + 1
            End If
        Next
        ReDim Preserve formula_array(i - 1)

        keysFormulaString = ""
        For Each item In formula_array
            If Not item = " " Then keysFormulaString = keysFormulaString & item
        Next

    End Sub

    ' Check the validity of a formula
    Public Function testFormula() As Boolean

        buildTestFormulaString()
        Return GENERICDCGLobalInstance.CheckParserFormula(formulaStringTest)

    End Function

    Friend Function GetFormulaDependantsLIst(ByRef formula_str As String) As List(Of String)

        Dim dependants_list As New List(Of String)
        Dim formulaArray() As String
        Dim accountKey As String
        Dim i As Integer

        formulaArray = Split(Utilities_Functions.DivideFormula(formula_str), FORMULA_SEPARATOR)         ' Generate array from formula
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

    '  Uses the evalauate function of the application to see whether we can compute or not
    Private Function Eval(Ref As String) As Object

        Ref = Replace(Ref, ",", ".")
        Eval = APPS.Evaluate(Ref)

    End Function


#End Region


#Region "Utilities"

    ' Replace accounts by random values in order to build a test expression
    Public Sub buildTestFormulaString()

        Dim i As Int32 = 0
        For Each item In formula_array
            If Not tokensList.Contains(item) And Not IsNumeric(item) Then
                formula_array(i) = Rnd(5)
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
