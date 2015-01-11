Imports Microsoft.Office.Interop

Public Class RefreshGetDataFs

    Private WS As Excel.Worksheet
    'Private MGD As ModelGetData


    Public Sub New()

        'MGD = New ModelGetData
        WS = apps.ActiveSheet
        findGetDataFormulaCells()

    End Sub

    Private Sub findGetDataFormulaCells()

        '--------------------------------------------------------------------------------
        ' Loop throught worksheet and launch formula update if "getData()" formula found
        '--------------------------------------------------------------------------------
        Dim c As Excel.Range
        Dim firstAddress As String
        With WS.Cells
            c = .Find("GetData", WS.Cells(1, 1), , )
           
            If Not c Is Nothing Then
                firstAddress = c.Address
                Do
                    apps.Evaluate(c.Formula)
                    c = .FindNext(c)
                    If IsNothing(c) Then Exit Do
                Loop While Not c Is Nothing And c.Address <> firstAddress
            End If
        End With

    End Sub

     



End Class
