﻿' InputValuesExcel.vb
'
' Used in market prices and rates MGT UIs
'
' -> test if we can input false range!
'
'
'
' Author: Julien Monnereau
' Last modified: 05/08/2015


Imports Microsoft.Office.Interop
Imports System.Collections.Generic
Imports VIBlend.WinForms.Controls



Friend Class ExcelExchangeRatesImportUI


#Region "Instance Variables"

    ' objects
    Private m_controller As ExchangeRatesController

    ' variables
    Private m_periodsRange As Excel.Range
    Private m_valuesRange As Excel.Range
    Friend m_destinationCurrency As Int32 = -1

#End Region


#Region "Initialize"

    Friend Sub New(ByRef p_controller As ExchangeRatesController)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_controller = p_controller
        Dim mainCurrencyId As Int32 = GlobalVariables.Currencies.mainCurrency
        Dim mainCurrencyName As String = GlobalVariables.Currencies.currencies_hash(mainCurrencyId)(NAME_VARIABLE)
        For Each currencyId As Int32 In GlobalVariables.Currencies.currencies_hash.Keys
            If currencyId <> GlobalVariables.Currencies.mainCurrency Then
                Dim li As New ListItem
                li.Value = currencyId
                li.Text = mainCurrencyName & "/" & GlobalVariables.Currencies.currencies_hash(currencyId)(NAME_VARIABLE)
                m_currencyComboBox.Items.Add(li)
                If currencyId = m_destinationCurrency Then
                    m_currencyComboBox.SelectedItem = li
                End If
            End If
        Next

    End Sub

#End Region


#Region "Call Backs"

    Private Sub PeriodsEditBT_Click(sender As Object, e As EventArgs) Handles periods_edit_BT.Click

        Me.TopMost = False
        m_periodsRange = GlobalVariables.APPS.InputBox("Select periods(s) range(s)", System.Type.Missing, System.Type.Missing, _
                          System.Type.Missing, System.Type.Missing, System.Type.Missing, _
                          System.Type.Missing, 8)

        m_periodsRangeTextBox.Text = m_periodsRange.Address

        ' Check if it is a valid address !!
        Me.TopMost = True

    End Sub

    Private Sub RatesEditBT_Click(sender As Object, e As EventArgs) Handles rates_edit_BT.Click

        Me.TopMost = False
        m_valuesRange = GlobalVariables.APPS.InputBox("Select rates(s) range(s)", System.Type.Missing, System.Type.Missing, _
                          System.Type.Missing, System.Type.Missing, System.Type.Missing, _
                          System.Type.Missing, 8)

        m_ratesRangeTextBox.Text = m_valuesRange.Address
        ' Check if it is a valid address !!
        Me.TopMost = True

    End Sub

    Private Sub import_BT_Click(sender As Object, e As EventArgs) Handles import_BT.Click

        If m_periodsRange.Count = m_valuesRange.Count _
        AndAlso Not m_currencyComboBox.SelectedItem Is Nothing _
        AndAlso CheckRangeDimension(m_periodsRange) = True Then
            Dim periods_array(m_periodsRange.Count - 1) As Integer
            Dim rates_array(m_valuesRange.Count - 1) As Double

            Dim i As Int32 = 0
            For Each period_cell As Excel.Range In m_periodsRange.Cells
                Dim rate_cell As Excel.Range = m_valuesRange.Cells(i + 1)
                If IsNumeric(period_cell.Value2) And IsNumeric(rate_cell.Value2) Then
                    periods_array(i) = CInt(period_cell.Value2)
                    rates_array(i) = CDbl(rate_cell.Value2)
                    i = i + 1
                End If
            Next
            m_controller.InputRangesCallBack(periods_array, rates_array, m_currencyComboBox.SelectedItem.Value)
        Else
            MsgBox("The Periods range and Rates range do not have the same size, or the dimensions of the ranges are not valid.")
        End If
    End Sub

#End Region


#Region "Utilities"

    ' Check if a range dimensions are either 1-X or X-1
    Private Function CheckRangeDimension(ByRef input_range As Excel.Range)

        If input_range.Columns.Count > 1 Then
            If input_range.Rows.Count > 1 Then Return False Else Return True
        Else
            Return True
        End If

    End Function


#End Region


End Class