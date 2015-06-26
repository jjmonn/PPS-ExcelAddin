'
'
'
' Author: Julien Monnereau
' Last modified: 03/05/2015


Friend Class CurrenciesSymbolsMapping

    Protected Friend Shared Function getCurrenciesDict(ByRef key As String, ByRef value As String) As Collections.Generic.Dictionary(Of String, String)

        Dim tmpDict As New Collections.Generic.Dictionary(Of String, String)
        Dim srv As New ModelServer
        srv.OpenRst(GlobalVariables.database + "." + CURRENCIES_SYMBOLS_TABLE_NAME, ModelServer.FWD_CURSOR)

        Do While srv.rst.EOF = False
            tmpDict.Add(srv.rst.Fields(key).Value, srv.rst.Fields(value).Value)
            srv.rst.MoveNext()
        Loop
        srv.rst.Close()
        Return tmpDict


    End Function

    Protected Friend Shared Function getCurrenciesList(ByRef item As String) As Collections.Generic.List(Of String)

        Dim tmpList As New Collections.Generic.List(Of String)
        Dim srv As New ModelServer
        srv.OpenRst(GlobalVariables.database + "." + CURRENCIES_SYMBOLS_TABLE_NAME, ModelServer.FWD_CURSOR)

        Do While srv.rst.EOF = False
            tmpList.Add(srv.rst.Fields(item).Value)
            srv.rst.MoveNext()
        Loop
        srv.rst.Close()
        Return tmpList

    End Function

End Class
