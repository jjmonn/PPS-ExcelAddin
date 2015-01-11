Module DataGridViewsUtil

    Public Sub DGVsetUpDisplay(DGV As Windows.Forms.DataGridView)

        DGV.BackgroundColor = Drawing.Color.White
        DGV.Columns(0).AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        DGV.DefaultCellStyle.Format = "n2"

    End Sub


End Module
