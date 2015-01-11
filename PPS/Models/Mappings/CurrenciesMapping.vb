' CurrenciesMapping.vb
'
' Provides information on currencies Table
'
'
'
' To do :
'       -  
'
'
' Known bugs
'
' Author: Julien Monnereau
' Last modified: 02/12/2014 


Imports System.Collections.Generic



Friend Class CurrenciesMapping


    ' Returns a list of param
    Protected Friend Shared Function getCurrenciesList(ByRef item As String) As List(Of String)

        Dim tmpList As New List(Of String)
        Dim srv As New ModelServer
        srv.openRst(CONFIG_DATABASE + "." + CURRENCIES_TABLE_NAME, ModelServer.FWD_CURSOR)

        If srv.rst.EOF = False And srv.rst.BOF = False Then
            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False
                tmpList.Add(srv.rst.Fields(item).Value)
                srv.rst.MoveNext()
            Loop
        End If

        srv.rst.Close()
        Return tmpList

    End Function




End Class
