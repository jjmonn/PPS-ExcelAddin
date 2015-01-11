Imports Microsoft.Office.Interop


Module View_Functions

    Public Sub WriteArray(arrayToWrite(,) As Object, Destination As Excel.Range)

        '--------------------------------------------------------------------------
        ' Write the input array in the specified destination range
        '--------------------------------------------------------------------------
        Destination.Resize(UBound(arrayToWrite, 1) + 1, UBound(arrayToWrite, 2) + 1).Value = arrayToWrite

    End Sub



End Module
