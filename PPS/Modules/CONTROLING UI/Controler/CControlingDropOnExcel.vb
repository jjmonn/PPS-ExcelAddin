' CControlingDropOnExcel.vb
'
' Manages the transfert from controlingUI DGVs to Excel
'
' To do:
'       - Reimplement send drill down and send tab !
'
'
' Known Bugs:
'       - 
'
'
' Author: Julien Monnereau
' Last Modified: 23/03/2015


Imports System.Windows.Forms
Imports VIBlend.WinForms.DataGridView
Imports Microsoft.Office.Interop


Friend Class CControlingDropOnExcel


#Region "Instance Variables"

    Private View As ControllingUI_2
    Private Controller As ControllingUI2Controller


#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef inputView As ControllingUI_2, _
                             ByRef inputControler As ControllingUI2Controller)

        View = inputView
        Controller = inputControler

    End Sub

#End Region


#Region "Interface"

    Protected Friend Sub SendCurrentEntityToExcel(ByRef version_name As String, _
                                                  ByRef currency As String)

        If Not Controller.Entity_node.Text Is Nothing Then

            Dim destination = CWorksheetWrittingFunctions.CreateReceptionWS(Controller.Entity_node.Text, _
                                                                            {"Entity", "Version", "Currency"}, _
                                                                            {Controller.Entity_node.Text, version_name, currency})

            ' To be reimplemented -> copy send drill down method BUT without rows levels recursive loops

        End If

    End Sub

    Protected Friend Sub SendTabToExcel(ByRef currentDGV As vDataGridView, _
                                        ByRef version_name As String, _
                                        ByRef currency As String)

        
        ' To be reimplemented (like above BUT for the current tab only)

    End Sub

    Protected Friend Sub SendDrillDownToExcel(ByRef version_name As String, _
                                              ByRef currency As String)

        If Not Controller.Entity_node.Text Is Nothing Then
            Dim destination As Excel.Range = CWorksheetWrittingFunctions.CreateReceptionWS(Controller.Entity_node.Text, _
                                                                                           {"Entity", "Version", "Currency"}, _
                                                                                           {Controller.Entity_node.Text, version_name, currency})
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
