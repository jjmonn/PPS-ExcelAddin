'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 19/01/2014


Imports System.Collections
Imports System.Collections.Generic


Friend Class AdjustmentsMapping


    Protected Friend Shared Function GetAdjustmentsDictionary(ByRef Key As String, ByRef Value As String) As Dictionary(Of String, String)

        Dim srv As New ModelServer
        Dim tmpHT As New Dictionary(Of String, String)
        srv.OpenRst(GlobalVariables.database + "." + ADJUSTMENTS_TABLE, ModelServer.FWD_CURSOR)

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

    Protected Friend Shared Function GetAdjustmentsIDsList(ByRef field As String) As List(Of String)

        Dim tmp_list As New List(Of String)
        Dim srv As New ModelServer
        srv.OpenRst(GlobalVariables.database + "." + ADJUSTMENTS_TABLE, ModelServer.FWD_CURSOR)
        Do While srv.rst.EOF = False
            tmp_list.Add(srv.rst.Fields(field).Value)
            srv.rst.MoveNext()
        Loop
        srv.rst.Close()
        Return tmp_list

    End Function

    
    
End Class
