' Format.vb
'
' gdfsuez_as_formats table CRUD model
'
' To do: 
'
'
'
'
' Author: Julien Monnereau
' Last modified: 03/02/2015


Imports ADODB
Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic



Friend Class FormatM


#Region "Instance Variables"

    ' Objects
    Private SRV As New ModelServer
    Private RST As Recordset


#End Region


#Region "Initialize"

    Protected Friend Sub New()

        Dim i As Int32 = 0
        Dim q_result = SRV.OpenRst(GlobalVariables.database & "." & FORMATS_TABLE_NAME, ModelServer.DYNAMIC_CURSOR)
        RST = SRV.rst

    End Sub

#End Region


#Region "CRUD Interface"

    Protected Friend Sub Createformat(ByRef hash As Hashtable)

        Dim fieldsArray(hash.Count - 1) As Object
        Dim valuesArray(hash.Count - 1) As Object
        hash.Keys.CopyTo(fieldsArray, 0)
        hash.Values.CopyTo(valuesArray, 0)
        RST.AddNew(fieldsArray, valuesArray)

    End Sub

    Protected Friend Function Readformat(ByRef format_code As String, _
                                         ByRef format_destination As String, _
                                         ByRef field As String) As Object

        RST.Filter = FORMAT_CODE_VARIABLE & "='" & format_code & "' AND " _
                   & FORMAT_DESTINATION_VARIABLE & "='" & format_destination & "'"
        If RST.EOF Then Return Nothing
        Return RST.Fields(field).Value

    End Function

    Protected Friend Function GetFormatHT(ByRef format_code As String, _
                                          ByRef format_destination As String) As Hashtable

        Dim ht As New Hashtable
        RST.Filter = FORMAT_CODE_VARIABLE & "='" & format_code & "' AND " _
                   & FORMAT_DESTINATION_VARIABLE & "='" & format_destination & "'"
        If RST.EOF Then Return Nothing
        ht.Add(FORMAT_TEXT_COLOR_VARIABLE, SRV.rst.Fields(FORMAT_TEXT_COLOR_VARIABLE).Value)
        ht.Add(FORMAT_BOLD_VARIABLE, SRV.rst.Fields(FORMAT_BOLD_VARIABLE).Value)
        ht.Add(FORMAT_ITALIC_VARIABLE, SRV.rst.Fields(FORMAT_ITALIC_VARIABLE).Value)
        ht.Add(FORMAT_BORDER_VARIABLE, SRV.rst.Fields(FORMAT_BORDER_VARIABLE).Value)
        ht.Add(FORMAT_BCKGD_VARIABLE, SRV.rst.Fields(FORMAT_BCKGD_VARIABLE).Value)
        ht.Add(FORMAT_NAME_VARIABLE, SRV.rst.Fields(FORMAT_NAME_VARIABLE).Value)
        ht.Add(FORMAT_ICON_VARIABLE, SRV.rst.Fields(FORMAT_ICON_VARIABLE).Value)
        ht.Add(FORMAT_INDENT_VARIABLE, SRV.rst.Fields(FORMAT_INDENT_VARIABLE).Value)
        ht.Add(FORMAT_UP_BORDER_VARIABLE, SRV.rst.Fields(FORMAT_UP_BORDER_VARIABLE).Value)
        ht.Add(FORMAT_BOTTOM_BORDER_VARIABLE, SRV.rst.Fields(FORMAT_BOTTOM_BORDER_VARIABLE).Value)

        Return ht

    End Function

    Protected Friend Sub Updateformat(ByRef format_code As String, _
                                      ByRef format_destination As String, _
                                      ByRef field As String, _
                                      ByVal value As Object)

        RST.Filter = FORMAT_CODE_VARIABLE & "='" & format_code & "' AND " _
                   & FORMAT_DESTINATION_VARIABLE & "='" & format_destination & "'"
        If RST.EOF = False AndAlso RST.BOF = False Then
            If IsDBNull(RST.Fields(field).Value) Then
                If Not IsNumeric(value) AndAlso value <> "" Then
                    RST.Fields(field).Value = value
                    RST.Update()
                ElseIf IsNumeric(value) Then
                    RST.Fields(field).Value = value
                    RST.Update()
                End If
            ElseIf RST.Fields(field).Value <> value Then
                If Not IsNumeric(value) AndAlso value = "" Then
                    RST.Fields(field).Value = DBNull.Value
                Else
                    RST.Fields(field).Value = value
                End If
                RST.Update()
            End If
        End If

    End Sub

    Protected Friend Sub Deleteformat(ByRef format_code As String, _
                                      ByRef format_destination As String)

        RST.Filter = FORMAT_CODE_VARIABLE & "='" & format_code & "' AND " _
                   & FORMAT_DESTINATION_VARIABLE & "='" & format_destination & "'"
        If RST.EOF = False Then
            RST.Delete()
            RST.Update()
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