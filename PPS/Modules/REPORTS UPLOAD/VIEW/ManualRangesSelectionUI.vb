' ManualRangesSelectionUI.vb
'   Allow the user to select the ranges to be uploaded
'
'
' to do : 
'        - this module contains CONTROLLER code...!!
'        - At the opening put current ranges values ?
'        - Replace/ Append ranges ?
'        - so far selection is check for accounts, entities and periods belonging to set up data
'        - evolution -> allow other input and propose mapping to validate
'
' Known Bugs:
'       - 
'
'
' Author: Julien Monnereau
' Last modified: 23/10/2014


Imports Microsoft.Office.Interop


Public Class ManualRangesSelectionUI

#Region "Instance Variables"

    ' Objects
    Private GRS As CGeneralReportSubmissionControler
    Private DATASET As CModelDataSet

    ' Variables
    Private rangesModifiedFlag As Boolean

#End Region

#Region "Initialize"

    Friend Sub New(ByRef inputGRS As CGeneralReportSubmissionControler, _
                   ByRef inputDATASET As CModelDataSet)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        GRS = inputGRS
        DATASET = inputDATASET

    End Sub

#End Region
    
#Region "Call Backs"

    ' Launch Accounts Range Selection
    Private Sub AccountsEditBT_Click(sender As Object, e As EventArgs) Handles AccountsEditBT.Click

        Me.TopMost = False
        Dim tmpRng As Excel.Range = GlobalVariables.apps.InputBox("Select Account(s) Range(s)", System.Type.Missing, System.Type.Missing, _
                          System.Type.Missing, System.Type.Missing, System.Type.Missing, _
                          System.Type.Missing, 8)

        AccountsRefEdit.Text = tmpRng.Address

        ' Check if it is a valid address !!
        If AccountsRefEdit.Text <> "" Then
            DATASET.AccountsAddressValuesDictionary.Clear()
            For Each cell As Excel.Range In tmpRng
                If DATASET.inputsAccountsList.Contains(cell.Value2) Then
                    DATASET.AccountsAddressValuesDictionary.Add(CStr(cell.Address), CStr(cell.Value2))
                End If
            Next
        End If
        rangesModifiedFlag = True
        Me.TopMost = True

    End Sub

    ' Launch Entities Range Selection
    Private Sub EntitiesEditBT_Click(sender As Object, e As EventArgs) Handles EntitiesEditBT.Click

        Me.TopMost = False
        Dim tmpRng As Excel.Range = GlobalVariables.apps.InputBox("Select Entity(s) Range(s)", System.Type.Missing, System.Type.Missing, _
                      System.Type.Missing, System.Type.Missing, System.Type.Missing, _
                      System.Type.Missing, 8)

        EntitiesRefEdit.Text = tmpRng.Address

        ' Check if it is a valid address !!
        If EntitiesRefEdit.Text <> "" Then
            DATASET.EntitiesAddressValuesDictionary.Clear()
            For Each cell As Excel.Range In tmpRng
                If DATASET.assetsList.Contains(cell.Value2) Then
                    DATASET.EntitiesAddressValuesDictionary.Add(CStr(cell.Address), CStr(cell.Value2))
                End If
            Next
        End If
        rangesModifiedFlag = True
        Me.TopMost = True

    End Sub

    ' Launch Periods Range Selection
    Private Sub PeriodsRef_Click(sender As Object, e As EventArgs) Handles PeriodsEditBT.Click

        Me.TopMost = False
        Dim tmpRng As Excel.Range = GlobalVariables.apps.InputBox("Select Period(s) Range(s)", System.Type.Missing, System.Type.Missing, _
                            System.Type.Missing, System.Type.Missing, System.Type.Missing, _
                            System.Type.Missing, 8)

        PeriodsRefEdit.Text = tmpRng.Address

        ' Check if it is a valid address !!
        If PeriodsRefEdit.Text <> "" Then
            DATASET.periodsAddressValuesDictionary.Clear()
            For Each cell As Excel.Range In tmpRng
                ' Here control Int/ Double / Date
                If DATASET.periodsDatesList.Contains(cell.Value2) Then
                    DATASET.periodsAddressValuesDictionary.Add(CStr(cell.Address), CDate(cell.Value2).ToOADate())
                End If
            Next
        End If
        rangesModifiedFlag = True
        Me.TopMost = True

    End Sub

    ' Validate Click
    Private Sub Validate_Cmd_Click(sender As Object, e As EventArgs) Handles Validate_Cmd.Click

        If rangesModifiedFlag = True Then
            GRS.UpdateDataSet()
        End If
        Me.Dispose()
        Me.Close()

    End Sub

    '' Closing Event
    'Private Sub ManualRangesSelectionUI_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

    '    If rangesModifiedFlag = True Then
    '        GRSCONTROLER.UpdateDataSet()
    '    End If

    'End Sub

#End Region


   

  
End Class