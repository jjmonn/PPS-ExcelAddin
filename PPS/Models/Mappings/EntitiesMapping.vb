' EntitiesMappingTransactions.vb
'
' Manages the transaction with ENTITIES table in DB. Create the entities related dictionaries
'
'
' To do : 
'      
'
' Know bugs :
' 
'
'Last modified: 25/06/2015
' Author: Julien Monnereau


Imports System.Collections
Imports System.Collections.Generic


Friend Class EntitiesMapping

    Friend Shared Function GetEntitiesDictionary(ByRef Key As String, ByRef Value As String) As Hashtable

        Dim srv As New ModelServer
        Dim tmpHT As New Hashtable
        srv.OpenRst(GlobalVariables.database + "." + ENTITIES_TABLE, ModelServer.FWD_CURSOR)

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

    Friend Shared Function GetEntitiesNamesList() As List(Of String)

        Dim namesList As New List(Of String)
        Dim srv As New ModelServer
        srv.OpenRst(GlobalVariables.database + "." + ENTITIES_TABLE, ModelServer.FWD_CURSOR)
        If srv.rst.EOF = False And srv.rst.BOF = False Then

            Do While srv.rst.EOF = False
                namesList.Add(srv.rst.Fields(ENTITIES_NAME_VARIABLE).Value)
                srv.rst.MoveNext()
            Loop

        End If
        srv.rst.Close()
        Return namesList

    End Function

    Friend Shared Function GetEntitiesKeysList() As List(Of String)

        Dim keysList As New List(Of String)
        Dim srv As New ModelServer
        srv.OpenRst(GlobalVariables.database + "." + ENTITIES_TABLE, ModelServer.FWD_CURSOR)
        If srv.rst.EOF = False And srv.rst.BOF = False Then

            Do While srv.rst.EOF = False
                keysList.Add(srv.rst.Fields(ENTITIES_ID_VARIABLE).Value)
                srv.rst.MoveNext()
            Loop

        End If

        srv.rst.Close()
        Return keysList

    End Function

    Protected Friend Function GetInputsIDList() As List(Of String)

        Dim tmp_list As New List(Of String)
        Dim srv As New ModelServer
        srv.OpenRst(GlobalVariables.database + "." + ENTITIES_TABLE, ModelServer.FWD_CURSOR)
        srv.rst.Filter = ENTITIES_ALLOW_EDITION_VARIABLE & "= 1"
        If srv.rst.EOF = False AndAlso srv.rst.BOF = False Then
            Do While srv.rst.EOF = False
                tmp_list.Add(srv.rst.Fields(ENTITIES_ID_VARIABLE).Value)
                srv.rst.MoveNext()
            Loop
        End If
        srv.rst.Close()
        Return tmp_list

    End Function

    Friend Shared Function GetCurrenciesInUseList() As List(Of String)

        Dim tmpList As New List(Of String)
        Dim srv As New ModelServer
        srv.openRst(GlobalVariables.database + "." + ENTITIES_TABLE, ModelServer.FWD_CURSOR)
        If srv.rst.EOF = False And srv.rst.BOF = False Then

            Do While srv.rst.EOF = False
                If Not tmpList.Contains(srv.rst.Fields(ENTITIES_CURRENCY_VARIABLE).Value) Then tmpList.Add(srv.rst.Fields(ENTITIES_CURRENCY_VARIABLE).Value)
                srv.rst.MoveNext()
            Loop

        End If

        srv.rst.Close()
        Return tmpList

    End Function

    Friend Shared Function GetEntityCurrency(ByRef entityKey As String) As String

        Dim srv As New ModelServer
        srv.openRst(GlobalVariables.database + "." + ENTITIES_TABLE, ModelServer.FWD_CURSOR)
        Dim criteria As String = ENTITIES_ID_VARIABLE + "='" + entityKey + "'"
        srv.rst.Find(criteria, , , 1)
        If srv.rst.EOF = False AndAlso srv.rst.BOF = False Then
            Return srv.rst.Fields(ENTITIES_CURRENCY_VARIABLE).Value
        End If
        Return Nothing

    End Function



End Class
