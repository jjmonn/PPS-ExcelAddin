' MarketIndexesMapping.vb
'
' Provides information on market indexes Table
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
' Last modified: 16/01/2015 


Imports System.Collections.Generic



Friend Class MarketIndexesMapping


    ' Returns a list of param
    Protected Friend Shared Function GetMarketIndexesList() As List(Of String)

        Dim tmpList As New List(Of String)
        Dim srv As New ModelServer
        srv.OpenRst(GlobalVariables.database + "." + MARKET_INDEXES_TABLE, ModelServer.FWD_CURSOR)

        If srv.rst.EOF = False And srv.rst.BOF = False Then
            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False
                tmpList.Add(srv.rst.Fields(MARKET_INDEXES_ID_VARIABLE).Value)
                srv.rst.MoveNext()
            Loop
        End If
        srv.rst.Close()
        Return tmpList

    End Function


End Class
