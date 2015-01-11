Imports Microsoft.Office.Interop

Module DataTables_Functions



    Public Function GetDataTable(HeadersArray As Collections.Generic.Dictionary(Of String, String), _
                                 RowsArray As Collections.Generic.Dictionary(Of String, String), _
                                  DataArray(,) As Double) As Data.DataTable

        '---------------------------------------------------------------------------------
        ' Return a DataTable (DataBase format) from Headers, 1st column and data arrays
        ' Specific to the Snapshot UI ?
        '---------------------------------------------------------------------------------

        ' Need for a check nbRows(RowsArray) = nbRows(DataArray) 
        Dim WS As Excel.Worksheet = apps.ActiveSheet
        Dim i As Integer = 0
        Dim table As New Data.DataTable

        ' -> Set up check boxes for the first column
        table.Columns.Add(SELECTION_COLUMN_TITLE, GetType(Boolean))
        table.Columns.Add(" ", GetType(String))

        For Each HeaderAddress As String In HeadersArray.Keys
            table.Columns.Add(HeadersArray.Item(HeaderAddress), GetType(Object))
        Next

        For Each rowKey As String In RowsArray.Keys                  ' Add tuples to the DataTable
            Dim Tuple(HeadersArray.Count + 2 - 1) As Object
            Tuple(0) = True
            Tuple(1) = RowsArray.Item(rowKey)
            For j As Integer = 0 To HeadersArray.Count - 1
                Tuple(j + 2) = CDbl(DataArray(i, j))
            Next j
            i = i + 1
            table.Rows.Add(Tuple)
        Next
        Return table

    End Function

    Public Function BuildDownloadDataTable() As Data.DataTable

        Dim table As New Data.DataTable
        table.Columns.Add(DATA_ASSET_ID_VARIABLE, GetType(String))        ' Asset
        table.Columns.Add(DATA_ACCOUNT_ID_TABLE, GetType(String))         ' Account
        table.Columns.Add(DATA_PERIOD_VARIABLE, GetType(Integer))         ' Period
        table.Columns.Add(DATA_VALUE_VARIABLE, GetType(Double))           ' Value
        Return table

    End Function




End Module
