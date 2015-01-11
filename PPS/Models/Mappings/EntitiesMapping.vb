' EntitiesMappingTransactions.vb
'
' Manages the transaction with ENTITIES table in DB. Create the entities related dictionaries
'
'
' To do : 
'       - ADAPT TO VIEWS !!!!
'
'
' Know bugs :
' 
'
'Last modified: 05/12/2014
' Author: Julien Monnereau


Imports System.Collections
Imports System.Collections.Generic


Friend Class EntitiesMapping

    ' Returns a dictionary for Entities. Param 1: Key, Param 2: Values
    Friend Shared Function GetEntitiesDictionary(ByRef Key As String, ByRef Value As String) As Hashtable

        Dim srv As New ModelServer
        Dim tmpHT As New Hashtable
        srv.openRst(VIEWS_DATABASE + "." + Entities_View, ModelServer.FWD_CURSOR)

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

    ' Return the list of the assets' name
    Friend Shared Function GetEntitiesNamesList() As List(Of String)

        Dim namesList As New List(Of String)
        Dim srv As New ModelServer
        srv.openRst(VIEWS_DATABASE + "." + Entities_View, ModelServer.FWD_CURSOR)
        If srv.rst.EOF = False And srv.rst.BOF = False Then

            Do While srv.rst.EOF = False
                namesList.Add(srv.rst.Fields(ASSETS_NAME_VARIABLE).Value)
                srv.rst.MoveNext()
            Loop

        End If
        srv.rst.Close()
        Return namesList

    End Function

    ' Return the list of the assets' keys
    Friend Shared Function GetEntitiesKeysList() As List(Of String)

        Dim keysList As New List(Of String)
        Dim srv As New ModelServer
        srv.openRst(VIEWS_DATABASE + "." + Entities_View, ModelServer.FWD_CURSOR)
        If srv.rst.EOF = False And srv.rst.BOF = False Then

            Do While srv.rst.EOF = False
                keysList.Add(srv.rst.Fields(ASSETS_TREE_ID_VARIABLE).Value)
                srv.rst.MoveNext()
            Loop

        End If

        srv.rst.Close()
        Return keysList

    End Function


    Friend Shared Function GetCurrenciesInUseList() As List(Of String)

        Dim tmpList As New List(Of String)
        Dim srv As New ModelServer
        srv.openRst(VIEWS_DATABASE + "." + Entities_View, ModelServer.FWD_CURSOR)
        If srv.rst.EOF = False And srv.rst.BOF = False Then

            Do While srv.rst.EOF = False
                If Not tmpList.Contains(srv.rst.Fields(ASSETS_CURRENCY_VARIABLE).Value) Then tmpList.Add(srv.rst.Fields(ASSETS_CURRENCY_VARIABLE).Value)
                srv.rst.MoveNext()
            Loop

        End If

        srv.rst.Close()
        Return tmpList

    End Function

    Friend Shared Function GetEntityCurrency(ByRef entityKey As String) As String

        Dim srv As New ModelServer
        srv.openRst(VIEWS_DATABASE + "." + Entities_View, ModelServer.FWD_CURSOR)
        Dim criteria As String = ASSETS_TREE_ID_VARIABLE + "='" + entityKey + "'"
        srv.rst.Find(criteria, , , 1)
        If srv.rst.EOF = False AndAlso srv.rst.BOF = False Then
            Return srv.rst.Fields(ASSETS_CURRENCY_VARIABLE).Value
        End If
        Return Nothing

    End Function



End Class
