' GDFSUEZEntitiesAttributes.vb
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


Friend Class GDFSUEZEntitiesAttributesMapping


    Protected Friend Shared Function GetEntitiesAttributes() As Dictionary(Of String, Hashtable)

        Dim entities_attributes_dict As New Dictionary(Of String, Hashtable)
        Dim srv As New ModelServer
        srv.OpenRst(GlobalVariables.database + "." + GDF_ENTITIES_AS_ATTRIBUTES_TABLE, ModelServer.FWD_CURSOR)

        If srv.rst.EOF = False And srv.rst.BOF = False Then
            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False
                Dim ht As New Hashtable
                ht.Add(GDF_ENTITIES_AS_GAS_FORMULA_VAR, srv.rst.Fields(GDF_ENTITIES_AS_GAS_FORMULA_VAR).Value)
                ht.Add(GDF_ENTITIES_AS_LIQUID_FORMULA_VAR, srv.rst.Fields(GDF_ENTITIES_AS_LIQUID_FORMULA_VAR).Value)
                ht.Add(GDF_ENTITIES_AS_TAX_RATE_VAR, srv.rst.Fields(GDF_ENTITIES_AS_TAX_RATE_VAR).Value)

                entities_attributes_dict.Add(srv.rst.Fields(GDF_ENTITIES_AS_ENTITY_ID_VAR).Value, ht)
                srv.rst.MoveNext()
            Loop
        End If
        srv.rst.Close()
        Return entities_attributes_dict

    End Function

End Class
