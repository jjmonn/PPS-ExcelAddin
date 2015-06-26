Imports System.Collections.Generic

' cFormulaTypesMapping.vb
'
' Provides lists and dictionaries of the content of the formulaTypes Table in DB
'
' To do: 
'       - 
'       - 
'
'
' Known bugs:
'       -
'
'
'
' Last modified: 02/12/2014
' Author: Julien MONNEREAU



Public Class FormulaTypesMapping

    
    Public Shared Function GetFormulaTypesDictionary(ByRef key As String, ByRef value As String) As Dictionary(Of String, String)

        Dim tmpDict As New Dictionary(Of String, String)
        Dim srv As New ModelServer
        srv.openRst(GlobalVariables.database + "." + FORMULA_TYPES_TABLE, ModelServer.FWD_CURSOR)

        If srv.rst.EOF = False And srv.rst.BOF = False Then
            Do While srv.rst.EOF = False
                tmpDict.Add(srv.rst.Fields(key).Value, srv.rst.Fields(value).Value)
                srv.rst.MoveNext()
            Loop
        End If

        srv.rst.Close()
        Return tmpDict

    End Function

    Public Shared Function GetFTypesKeysNeedingFormula() As List(Of String)

        Dim tmpList As New List(Of String)
        Dim srv As New ModelServer
        srv.openRst(GlobalVariables.database + "." + FORMULA_TYPES_TABLE, ModelServer.FWD_CURSOR)
        srv.rst.Filter = FORMULA_TYPES_MUST_HAVE_F + "=1"

        If srv.rst.EOF = False And srv.rst.BOF = False Then
            Do While srv.rst.EOF = False
                tmpList.Add(srv.rst.Fields(FORMULA_TYPES_CODE_VARIABLE).Value)
                srv.rst.MoveNext()
            Loop
        End If

        srv.rst.Filter = ""
        srv.rst.Close()
        Return tmpList


    End Function


#Region "DLL purpose Functions"

    Public Shared Function GetModelFormulaTypesIntCodes() As List(Of Int32)

        Dim codesList As New List(Of Int32)
        Dim srv As New ModelServer
        srv.openRst(GlobalVariables.database + "." + FORMULA_TYPES_TABLE, ModelServer.FWD_CURSOR)
        
        If srv.rst.EOF = False And srv.rst.BOF = False Then
            Do While srv.rst.EOF = False And srv.rst.BOF = False
                If srv.rst.Fields(FORMULA_TYPES_MODEL_INCLUSION).Value = 1 Then
                    codesList.Add(srv.rst.Fields(FORMULA_TYPES_MODEL_CODE).Value)
                End If
                srv.rst.MoveNext()
            Loop
        End If

        srv.rst.Close()
        Return codesList

    End Function

    Public Shared Function GetModelFormulaTypesKeys() As List(Of String)

        Dim namesList As New List(Of String)
        Dim srv As New ModelServer
        srv.openRst(GlobalVariables.database + "." + FORMULA_TYPES_TABLE, ModelServer.FWD_CURSOR)

        If srv.rst.EOF = False And srv.rst.BOF = False Then
            Do While srv.rst.EOF = False
                If srv.rst.Fields(FORMULA_TYPES_MODEL_INCLUSION).Value = 1 Then
                    namesList.Add(srv.rst.Fields(FORMULA_TYPES_CODE_VARIABLE).Value)
                End If
                srv.rst.MoveNext()
            Loop
        End If

        srv.rst.Close()
        Return namesList

    End Function

#End Region


End Class
