' FormulasGenericCheck.vb
'
' Class testing if a formula is valid or not
'
' To do
'       - 
'
'
'
' Author: Julien Monnereau
' Last modified: 10/02/2015


Imports System.Collections.Generic
Imports System.Collections


Friend Class FormulasGenericCheck


#Region "Instance Variables"



    ' Constants

#End Region


#Region "Interface"

    Protected Friend Shared Function CheckFormula(ByVal formula As String, _
                                                  ByRef token_list As List(Of String)) As Int32

        Dim operators As List(Of String) = New List(Of String)
        For Each item In ModelFormulasMGT.FORMULAS_TOKEN_CHARACTERS
            operators.Add(item)
        Next
        formula = convertTokensToNumericValues(formula, _
                                               token_list, _
                                               operators)
        If formula Is Nothing Then Return 0
        If GlobalVariables.GenericGlobalSingleEntityComputer.CheckParserFormula(formula) = True Then Return 2 Else Return 1

    End Function

#End Region


#Region "Utilities"

    Protected Friend Shared Function convertTokensToNumericValues(ByVal formula As String, _
                                                           ByRef token_list As List(Of String), _
                                                           ByRef operators As List(Of String)) As String

        Dim i As Integer
        Dim formula_array As String() = Split(Utilities_Functions.DivideFormula(formula), FORMULA_SEPARATOR)         ' Generate array from formula

        For Each item In formula_array
            If IsNumeric(Replace(item, ".", ",")) Then
                item = Replace(item, ".", ",")
                formula_array(i) = item
                i = i + 1
            End If
            If Not operators.Contains(item) Then
                If Not IsNumeric(item) Then
                    item = LTrim(item)
                    item = RTrim(item)
                    If Not item = "" Then
                        If token_list.Contains(item) = False Then
                            Return Nothing
                        Else
                            formula_array(i) = 1
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

        formula = ""
        For Each item In formula_array
            If Not item = " " Then formula = formula & item
        Next
        Return formula

    End Function


#End Region


End Class
