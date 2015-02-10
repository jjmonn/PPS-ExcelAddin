' GDFSUEZEntitiesAttribute.vb
'
' gdf_as_entities_attributes table CRUD model
'
' To do: 
'
'
'
'
' Author: Julien Monnereau
' Last modified: 10/02/2015


Imports ADODB
Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic


Friend Class GDFSUEZEntitiesAttribute


#Region "Instance Variables"

    ' Objects
    Private SRV As New ModelServer
    Private RST As Recordset

    ' Constants
    Friend object_is_alive As Boolean
  
#End Region


#Region "Initialize"

    Protected Friend Sub New()

        Dim i As Int32 = 0
        object_is_alive = SRV.OpenRst(CONFIG_DATABASE & "." & GDF_ENTITIES_AS_ATTRIBUTES_TABLE, ModelServer.FWD_CURSOR)
        RST = SRV.rst

    End Sub

#End Region


#Region "CRUD Interface"

    Protected Friend Function ReadEntity(ByRef entity_id As String, ByRef field As String) As Object

        RST.Filter = GDF_ENTITIES_AS_ENTITY_ID_VAR + "='" + entity_id + "'"
        If RST.EOF Then Return Nothing
        Return RST.Fields(field).Value

    End Function

    Protected Friend Function GetRecord(ByRef entity_id As String) As Hashtable

        RST.Filter = GDF_ENTITIES_AS_ENTITY_ID_VAR + "='" + entity_id + "'"
        If RST.EOF Then Return Nothing
        Dim hash As New Hashtable
        hash.Add(GDF_ENTITIES_AS_GAS_FORMULA_VAR, RST.Fields(GDF_ENTITIES_AS_GAS_FORMULA_VAR).Value)
        hash.Add(GDF_ENTITIES_AS_LIQUID_FORMULA_VAR, RST.Fields(GDF_ENTITIES_AS_LIQUID_FORMULA_VAR).Value)
        hash.Add(GDF_ENTITIES_AS_TAX_RATE_VAR, RST.Fields(GDF_ENTITIES_AS_TAX_RATE_VAR).Value)
        Return hash

    End Function

    Protected Friend Sub UpdateEntity(ByRef entity_id As String, _
                                     ByRef field As String, _
                                     ByVal value As Object)

        RST.Filter = GDF_ENTITIES_AS_ENTITY_ID_VAR + "='" + entity_id + "'"
        If RST.EOF = False AndAlso RST.BOF = False Then
            If RST.Fields(field).Value <> value Then
                RST.Fields(field).Value = value
                RST.Update()
            End If
        End If

    End Sub

#End Region


#Region "Utilities"

    Protected Overrides Sub finalize()

        Try
            RST.Close()
        Catch ex As Exception
        End Try
        MyBase.Finalize()

    End Sub

#End Region


End Class
