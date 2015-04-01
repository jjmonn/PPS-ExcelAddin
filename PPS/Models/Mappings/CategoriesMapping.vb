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
' Last modified: 24/03/2015
' Author: Julien Monnereau


Imports System.Collections
Imports System.Collections.Generic


Friend Class CategoriesMapping


    Protected Friend Shared Function GetCategoryDictionary(ByRef code As String, _
                                                           ByRef Key As String, _
                                                           ByRef Value As String) As Hashtable

        Dim tmpHT As New Hashtable
        Dim srv As New ModelServer
        srv.OpenRst(CONFIG_DATABASE + "." + CATEGORIES_TABLE_NAME, ModelServer.FWD_CURSOR)
        srv.rst.Filter = CATEGORY_CODE_VARIABLE & "='" & code & "'"

        Do While srv.rst.EOF = False
            tmpHT.Add(srv.rst.Fields(Key).Value, srv.rst.Fields(Value).Value)
            srv.rst.MoveNext()
        Loop

        srv.rst.Close()
        srv = Nothing
        Return tmpHT

    End Function

    Protected Friend Shared Function GetCategoriesIDCodeMapping() As Dictionary(Of String, String)

        Dim tmp_dict As New Dictionary(Of String, String)
        Dim srv As New ModelServer
        srv.OpenRst(CONFIG_DATABASE + "." + CATEGORIES_TABLE_NAME, ModelServer.FWD_CURSOR)
        srv.rst.Filter = CATEGORY_PARENT_ID_VARIABLE & "=NULL"

        Do While srv.rst.EOF = False
            tmp_dict.Add(srv.rst.Fields(CATEGORY_ID_VARIABLE).Value, srv.rst.Fields(CATEGORY_CODE_VARIABLE).Value)
            srv.rst.MoveNext()
        Loop

        srv.rst.Close()
        srv = Nothing
        Return tmp_dict

    End Function



End Class
