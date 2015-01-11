Imports Microsoft.Office.Interop

Public Class DataViewUI


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub Drop_Cmd_Click(sender As Object, e As EventArgs) Handles Drop_Cmd.Click

        '-------------------------------------------------------------------------------------------
        ' Drop the DGV content into the worksheet
        '-------------------------------------------------------------------------------------------
        ' Headers
        Dim destination As Excel.Range = apps.Cells(apps.ActiveCell.Row, apps.ActiveCell.Column + 1)
        destination.Resize(0, UBound(GLOBALMC.PeriodList) + 1).Value = GLOBALMC.PeriodList
        ' Values
        GLOBALMC.WS.Range(GLOBALMC.startCell, GLOBALMC.endCell).Copy()
        destination = apps.Cells(apps.ActiveCell.Row + 1, apps.ActiveCell.Column)
        destination.PasteSpecial()

    End Sub

    '############### Events################

    Private Sub DataViewUI_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize

        '-------------------------------------------------------------------------
        ' Resize the dataGridView1 according to the new size of the UI
        '-------------------------------------------------------------------------
        DataGridView1.Width = Me.Width - 35
        DataGridView1.Height = Me.Height - DataGridView1.Top - 40

    End Sub



End Class