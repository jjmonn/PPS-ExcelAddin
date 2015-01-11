'
'
'
'
' -> test if we can input false range!
'
'
'
' Author: Julien Monnereau
' Last modified: 28/11/2014

Imports Microsoft.Office.Interop



Public Class InputRatesExcel


#Region "Instance Variables"

    ' objects
    Private CONTROLLER As CExchangeRatesCONTROLER

    ' variables
    Private periods_range As Excel.Range
    Private rates_range As Excel.Range


#End Region


#Region "Initialize"

    Friend Sub New(ByRef inputController As CExchangeRatesCONTROLER)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        CONTROLLER = inputController
        For Each curr In CONTROLLER.currencies_list
            If curr <> MAIN_CURRENCY Then currency_CB.Items.Add(curr)
        Next

    End Sub

#End Region


#Region "Call Backs"

    Private Sub PeriodsEditBT_Click(sender As Object, e As EventArgs) Handles periods_edit_BT.Click

        Me.TopMost = False
        periods_range = APPS.InputBox("Select Account(s) Range(s)", System.Type.Missing, System.Type.Missing, _
                          System.Type.Missing, System.Type.Missing, System.Type.Missing, _
                          System.Type.Missing, 8)

        periods_RefEdit.Text = periods_range.Address

        ' Check if it is a valid address !!
        Me.TopMost = True

    End Sub

    Private Sub RatesEditBT_Click(sender As Object, e As EventArgs) Handles rates_edit_BT.Click

        Me.TopMost = False
        rates_range = APPS.InputBox("Select Account(s) Range(s)", System.Type.Missing, System.Type.Missing, _
                          System.Type.Missing, System.Type.Missing, System.Type.Missing, _
                          System.Type.Missing, 8)

        rates_RefEdit.Text = rates_range.Address
        ' Check if it is a valid address !!
        Me.TopMost = True

    End Sub

    Private Sub import_BT_Click(sender As Object, e As EventArgs) Handles import_BT.Click

        If periods_range.Count = rates_range.Count _
        AndAlso currency_CB.Text <> "" _
        AndAlso CheckRangeDimension(periods_range) = True Then
            Dim periods_array(periods_range.Count - 1) As Integer
            Dim rates_array(rates_range.Count - 1) As Double

            Dim i As Int32 = 0
            For Each period_cell As Excel.Range In periods_range.Cells
                Dim rate_cell As Excel.Range = rates_range.Cells(i + 1)
                If IsNumeric(period_cell.Value2) And IsNumeric(rate_cell.Value2) Then
                    periods_array(i) = CInt(period_cell.Value2)
                    rates_array(i) = CDbl(rate_cell.Value2)
                    i = i + 1
                End If
            Next
            CONTROLLER.InputRangesCallBack(periods_array, rates_array, currency_CB.Text)
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