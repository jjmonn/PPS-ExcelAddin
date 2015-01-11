﻿' CControlingDropOnExcel.vb
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
' Last Modified: 06/01/2015


Imports System.Windows.Forms
Imports VIBlend.WinForms.DataGridView


Friend Class CControlingDropOnExcel


#Region "Instance Variables"

    Private View As ControllingUI_2
    Private Controller As CControlingCONTROLER


#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef inputView As ControllingUI_2, _
                             ByRef inputControler As CControlingCONTROLER)

        View = inputView
        Controller = inputControler

    End Sub

#End Region


#Region "Interface"

    Protected Friend Sub SendCurrentEntityToExcel()

        If Controller.currentEntity <> "" Then

            Dim destination = CWorksheetWrittingFunctions.CreateReceptionWS(Controller.currentEntity)
            Dim offset As Int32
            destination = destination.Offset(1, 0)
            For Each tab_ As TabPage In View.TabControl1.TabPages
                If Controller.VERSIONSMGT.VersionsCodeArray.Length > 1 Then
                    offset = View.DGVUTIL.writeControllingCurrentEntityToExcel(destination, tab_.Controls(0))
                Else
                    offset = View.DGVUTIL.WriteCurrentEntityToExcel(destination, tab_.Controls(0))
                End If
                destination = destination.Offset(offset, 0)
            Next
            APPS.ActiveSheet.Columns(1).autofit()

        End If

    End Sub

    Protected Friend Sub SendTabToExcel(ByRef currentDGV As vDataGridView)

        If Controller.currentEntity <> "" Then
            Dim destination = CWorksheetWrittingFunctions.CreateReceptionWS(Controller.currentEntity)
            destination = destination.Offset(1, 0)
            If Controller.VERSIONSMGT.VersionsCodeArray.GetLength(0) > 1 Then
                View.DGVUTIL.writeControllingCurrentEntityToExcel(destination, currentDGV)
            Else
                View.DGVUTIL.WriteCurrentEntityToExcel(destination, currentDGV)
            End If
        End If

    End Sub

    Protected Friend Sub SendDropDownToExcel()

        ' TBI
        ' Reflexion so far: send the dropdown to excel will be too heavy unmanageable 
        ' solutions : 
        '       - Use excel rows grouping - nb levels = 
        '       - Send data to pivot table


    End Sub

#End Region




End Class
