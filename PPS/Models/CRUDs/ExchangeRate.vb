' ExchangeRate.vb : CRUD model for exchange_rates table
'
'
' - Modify fx rates building when destination currency != main currency -> (origin_curr/main_curr)*(main_curr/dest_curr)
'
'
'
'
' Author: Julien Monnereau
' Last modified: 24/01/2015


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections
Imports ADODB


Friend Class ExchangeRate


#Region "Instance Variables"

    ' Objects
    Private SRV As ModelServer
    Friend RST As Recordset

    ' Variables
    Friend current_version As String
    Friend modified_flag As Boolean
    Friend object_is_alive As Boolean

    ' Constants
    Friend Const AVERAGE_RATE As String = "average"
    Friend Const CLOSING_RATE As String = "closing"


#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef rates_version_id As String)

        SRV = New ModelServer
        Dim str_sql = "SELECT * FROM " & CONFIG_DATABASE & "." & EXCHANGE_RATES_TABLE_NAME & _
                              " WHERE " & EX_RATES_RATE_VERSION & "='" & rates_version_id & "'"
        Dim i As Int32 = 0
        Dim q_result = SRV.openRstSQL(str_sql, ModelServer.FWD_CURSOR)  '
        object_is_alive = q_result
        RST = SRV.rst
        current_version = rates_version_id

    End Sub


#End Region


#Region "CRUD Interface"

    Protected Friend Sub CreateExchangeRate(ByRef curr As String, _
                                  ByRef period As Integer, _
                                  ByRef version As String, _
                                  ByRef value As Double)

        RST.AddNew()
        Dim curr_token As String = curr & "/" & MAIN_CURRENCY
        RST(EX_RATES_CURRENCY_VARIABLE).Value = curr_token
        RST(EX_RATES_PERIOD_VARIABLE).Value = period
        RST(EX_RATES_RATE_VARIABLE).Value = 1
        RST(EX_RATES_RATE_ID_VARIABLE).Value = curr_token & period
        RST(EX_RATES_RATE_VERSION).Value = version
        RST.Update()

    End Sub

    Protected Friend Function ReadRate(ByRef rate_id As String, ByRef field As String) As Object

        RST.Filter = EX_RATES_RATE_ID_VARIABLE + "='" + rate_id + "'"
        If RST.EOF Then Return Nothing
        Return RST.Fields(field).Value

    End Function

    Protected Friend Sub UpdateRate(ByRef rate_id As String, ByRef rateAttributes As Hashtable)

        RST.Filter = EX_RATES_RATE_ID_VARIABLE + "='" + rate_id + "'"
        If RST.EOF = False AndAlso RST.BOF = False Then
            For Each Attribute In rateAttributes.Keys
                If RST.Fields(Attribute).Value <> rateAttributes(Attribute) Then RST.Fields(Attribute).Value = rateAttributes(Attribute)
            Next
            modified_flag = True
        End If

    End Sub

    Protected Friend Sub UpdateRate(ByRef rate_id As String, _
                          ByRef field As String, _
                          ByVal value As Object)

        RST.Filter = EX_RATES_RATE_ID_VARIABLE + "='" + rate_id + "'"
        If RST.EOF = False AndAlso RST.BOF = False Then
            If RST.Fields(field).Value <> value Then
                RST.Fields(field).Value = value
                modified_flag = True
            End If
        End If

    End Sub

    Protected Friend Sub DeleteRate(ByRef rate_id As String)

        RST.Filter = EX_RATES_RATE_ID_VARIABLE + "='" + rate_id + "'"
        If RST.EOF = False Then
            RST.Delete()
            modified_flag = True
        End If

    End Sub

    Protected Friend Sub UpdateModel()

        RST.UpdateBatch()
        modified_flag = False

    End Sub

#End Region


#Region "Utilities"

    Protected Friend Shared Function DeleteAllRates(ByRef version_id As String) As Boolean

        Dim srv As New ModelServer
        Dim str_sql As String = "DELETE FROM " & CONFIG_DATABASE & "." & EXCHANGE_RATES_TABLE_NAME & _
                                " WHERE " & EX_RATES_RATE_VERSION & "='" & version_id & "'"
        Dim i As Int32 = 0
        Dim q_result = srv.sqlQuery(str_sql)
        Return q_result
        srv = Nothing

    End Function

    ' Return (period)(average/closing) -> rate
    Protected Friend Function BuildExchangeRatesDictionary(ByRef time_config As String, _
                                                           ByRef start_period As Int32, _
                                                           ByRef nb_periods As Int32, _
                                                           ByVal currency_token As String, _
                                                           ByRef reverse_flag As Boolean) As Dictionary(Of Int32, Dictionary(Of String, Double))

        If reverse_flag = True Then ExchangeRatesMapping.reverse_token(currency_token)
        Dim rates_dict As New Dictionary(Of Int32, Dictionary(Of String, Double))
        Select Case time_config
            Case YEARLY_TIME_CONFIGURATION
                Dim periods_list As List(Of Int32) = Period.GetYearlyPeriodList(start_period, nb_periods)
                Dim global_periods_list As Dictionary(Of Integer, Integer()) = Period.GetGlobalPeriodsDictionary(periods_list)

                For Each year_int In periods_list
                    Dim tmp_dic As New Dictionary(Of String, Double)
                    Dim rates_sum As Double = 0
                    For Each month_int In global_periods_list(year_int)
                        rates_sum = rates_sum + ReadRate(currency_token & month_int, EX_RATES_RATE_VARIABLE)
                    Next
                    tmp_dic.Add(AVERAGE_RATE, rates_sum / NB_MONTHS)
                    tmp_dic.Add(CLOSING_RATE, ReadRate(currency_token & year_int, EX_RATES_RATE_VARIABLE))
                    rates_dict.Add(year_int, tmp_dic)
                Next

            Case MONTHLY_TIME_CONFIGURATION
                Dim periods_list As List(Of Int32) = Period.GetYearlyPeriodList(start_period, nb_periods)
                Dim global_periods_list As Dictionary(Of Integer, Integer()) = Period.GetGlobalPeriodsDictionary(periods_list)
                For Each year_int In periods_list
                    Dim tmp_dic As New Dictionary(Of String, Double)
                    For Each month_int In global_periods_list(year_int)
                        tmp_dic.Add(CLOSING_RATE, ReadRate(currency_token & month_int, EX_RATES_RATE_VARIABLE))
                        rates_dict.Add(month_int, tmp_dic)
                    Next
                Next

        End Select
        Return rates_dict

    End Function


    Protected Overrides Sub finalize()

        RST.Close()
        MyBase.Finalize()

    End Sub


#End Region



End Class
