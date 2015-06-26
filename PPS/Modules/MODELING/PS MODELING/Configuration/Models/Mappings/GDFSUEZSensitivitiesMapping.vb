' GDFSUEZSensitivitiesMapping.vb
'
' Provides information on GDF SUEZ Sensitivities Table
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
Imports System.Collections


Friend Class GDFSUEZSensitivitiesMapping

    Protected Friend Shared Function GetSensitivitiesDictionary() As Dictionary(Of String, Hashtable)

        Dim sensitivities_dict As New Dictionary(Of String, Hashtable)
        Dim srv As New ModelServer
        srv.OpenRst(GlobalVariables.database + "." + GDF_SENSITIVITIES_LIST_TABLE, ModelServer.FWD_CURSOR)

        If srv.rst.EOF = False And srv.rst.BOF = False Then
            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False
                Dim ht As New Hashtable
                ht.Add(GDF_SENSITIVITIES_VOLUMES_ACCOUNT_ID_VAR, srv.rst.Fields(GDF_SENSITIVITIES_VOLUMES_ACCOUNT_ID_VAR).Value)
                ht.Add(GDF_SENSITIVITIES_REVENUES_ACCOUNT_ID_VAR, srv.rst.Fields(GDF_SENSITIVITIES_REVENUES_ACCOUNT_ID_VAR).Value)
                ht.Add(GDF_SENSITIVITIES_INITIAL_UNIT_VAR, srv.rst.Fields(GDF_SENSITIVITIES_INITIAL_UNIT_VAR).Value)
                ht.Add(GDF_SENSITIVITIES_DEST_UNIT_VAR, srv.rst.Fields(GDF_SENSITIVITIES_DEST_UNIT_VAR).Value)
                ht.Add(GDF_SENSITIVITIES_FORMULA_NAME_VAR, srv.rst.Fields(GDF_SENSITIVITIES_FORMULA_NAME_VAR).Value)

                sensitivities_dict.Add(srv.rst.Fields(GDF_SENSITIVITIES_ITEM_VAR).Value, ht)
                srv.rst.MoveNext()
            Loop
        End If
        srv.rst.Close()
        Return sensitivities_dict

    End Function



End Class
