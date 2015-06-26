' OperationalUnitConversionMapping.vb
'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 16/01/2015


Imports System.Collections
Imports System.Collections.Generic


Friend Class OperationalUnitConversionMapping


    Protected Friend Shared Sub GetConversionRate(ByRef conversion_rate_id As String, _
                                                  ByRef conversion_rate As Double)

        Dim srv As New ModelServer
        srv.OpenRst(GlobalVariables.database & "." & OPERATIONAL_UNITS_CONVERSION_TABLE, ModelServer.FWD_CURSOR)
        srv.rst.Filter = OPERATIONAL_UNITS_CONVERSION_ID_VAR & "='" & conversion_rate_id & "'"
        If srv.rst.EOF = False Then
            conversion_rate = srv.rst.Fields(OPERATIONAL_UNITS_CONVERSION_RATE_VAR).Value
        Else
            ReverseToken(conversion_rate_id)
            srv.rst.Filter = OPERATIONAL_UNITS_CONVERSION_ID_VAR & "='" & conversion_rate_id & "'"
            If srv.rst.EOF = False Then
                conversion_rate = srv.rst.Fields(OPERATIONAL_UNITS_CONVERSION_RATE_VAR).Value
            Else
                MsgBox(conversion_rate_id & " not in data base. The volumes will not be converted.")
            End If
        End If

    End Sub


#Region "Utilities"

    Public Shared Sub ReverseToken(ByRef token As String)

        Dim left_length As Int32 = token.IndexOf(CURRENCIES_SEPARATOR)
        Dim left_side = Left(token, left_length) - 1
        Dim right_length As Int32 = token.Length - left_length - 1
        Dim right_side = Right(token, right_length)
        token = right_side + CURRENCIES_SEPARATOR + left_side

    End Sub


#End Region


End Class
