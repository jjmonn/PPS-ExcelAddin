' CControlingDropOnExcel.vb
'
' Manages the transfert from controlingUI DGVs to Excel
'
' To do:
'       - Implement drop drill down to excel
'
'
' Known Bugs:
'       - 
'
'
' Author: Julien Monnereau
' Last Modified: 04/02/2015


Imports System.Windows.Forms
Imports VIBlend.WinForms.DataGridView
Imports Microsoft.Office.Interop


Friend Class CControlingDropOnExcel


#Region "Instance Variables"

    Private View As ControllingUI_2
    Private Controller As ControlingUI2Controller


#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef inputView As ControllingUI_2, _
                             ByRef inputControler As ControlingUI2Controller)

        View = inputView
        Controller = inputControler

    End Sub

#End Region


#Region "Interface"

    Protected Friend Sub SendCurrentEntityToExcel(ByRef version_name As String, _
                                                  ByRef currency As String)

        If Controller.currentEntity <> "" Then

            Dim destination = CWorksheetWrittingFunctions.CreateReceptionWS(Controller.currentEntity, _
                                                                            {"Entity", "Version", "Currency"}, _
                                                                            {Controller.currentEntity, version_name, Currency})
            Dim offset As Int32
            destination = destination.Offset(1, 0)
            For Each tab_ As TabPage In View.TabControl1.TabPages
                If Controller.versions_id_array.Length > 1 Then
                    offset = View.DGVUTIL.writeControllingCurrentEntityToExcel(destination, tab_.Controls(0))
                Else
                    offset = View.DGVUTIL.WriteCurrentEntityToExcel(destination, tab_.Controls(0))
                End If
                destination = destination.Offset(offset, 0)
            Next
            GlobalVariables.apps.ActiveSheet.Columns(1).autofit()

        End If

    End Sub

    Protected Friend Sub SendTabToExcel(ByRef currentDGV As vDataGridView, _
                                        ByRef version_name As String, _
                                        ByRef currency As String)

        If Controller.currentEntity <> "" Then
            Dim destination = CWorksheetWrittingFunctions.CreateReceptionWS(Controller.currentEntity, _
                                                                            {"Entity", "Version", "Currency"}, _
                                                                            {Controller.currentEntity, version_name, currency})
            destination = destination.Offset(1, 0)
            If Controller.versions_id_array.GetLength(0) > 1 Then
                View.DGVUTIL.writeControllingCurrentEntityToExcel(destination, currentDGV)
            Else
                View.DGVUTIL.WriteCurrentEntityToExcel(destination, currentDGV)
            End If
        End If

    End Sub

    Protected Friend Sub SendDrillDownToExcel(ByRef version_name As String, _
                                              ByRef currency As String)

        If Controller.currentEntity <> "" Then
            Dim destination As Excel.Range = CWorksheetWrittingFunctions.CreateReceptionWS(Controller.currentEntity, _
                                                                                           {"Entity", "Version", "Currency"}, _
                                                                                           {Controller.currentEntity, version_name, currency})
            Dim i As Int32 = 1
            For Each tab_ As TabPage In View.TabControl1.TabPages
                Dim DGV As vDataGridView = tab_.Controls(0)
                DataGridViewsUtil.CopyDGVToExcelGeneric(DGV, destination, i)
            Next
            destination.Worksheet.Columns.AutoFit()
            destination.Worksheet.Outline.ShowLevels(RowLevels:=1)
        End If

    End Sub

#End Region




End Class
