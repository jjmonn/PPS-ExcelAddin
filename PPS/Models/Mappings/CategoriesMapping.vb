' CategoriesMappingClass.vb
' 
' Provides dictionaries and lists for the categories Tables
'
'
'
' To do:
'
'
'
'
' Known bugs:
'
'
' Last modified: 02/12/2014
' Author: Julien Monnereau


Imports System.Collections


Public Class CategoriesMapping


#Region "DICTIONARIES"

    ' CategoriesDictionary. Param 1: key. Param 2: value
    Public Shared Function GetCategoriesDictionary(ByRef Key As String, ByRef Value As String) As Hashtable

        Dim tmpHT As New Hashtable
        Dim srv As New ModelServer

        srv.openRst(CONFIG_DATABASE + "." + CATEGORIES_TABLE_NAME, ModelServer.FWD_CURSOR)
        If srv.rst.EOF = False And srv.rst.BOF = False Then

            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False
                tmpHT.Add(srv.rst.Fields(Key).Value, srv.rst.Fields(Value).Value)
                srv.rst.MoveNext()
            Loop

        End If
        srv.rst.Close()
        srv = Nothing
        Return tmpHT

    End Function

#End Region










End Class
