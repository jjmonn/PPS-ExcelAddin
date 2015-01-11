Imports Microsoft.Office.Interop

Public Class ManualRangeSel_UI


    Private snapshot As SnapShotUI

    Private Sub ManualRangeSel_UI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '-----------------------------------------------------------------------------------
        ' Initialize the UI
        '-----------------------------------------------------------------------------------
        snapshot = Me.Owner                                    ' Set reference to snapshotUI


    End Sub


    Private Sub Validate_Cmd_Click(sender As Object, e As EventArgs) Handles Validate_Cmd.Click

        '----------------------------------------------------------------------------------
        ' Update snapshotUI.DataSet with current ranges ot accounts, periods and assets
        '----------------------------------------------------------------------------------
        ' pAssetAddressList update  
        If EntRefEdit.get_Value <> "" Then                                                  ' Case new Entities range
            snapshot.DataSet.pAddressAssetsList.Clear()
            For Each cell As Excel.Range In apps.Range(EntRefEdit.get_Value)
                snapshot.DataSet.pAddressAssetsList.Add(CStr(cell.Address), CStr(cell.Value2))
            Next
        ElseIf Entities_Input.Text <> "" Then                                               ' Case new 1 Asset 
            snapshot.DataSet.pAddressAssetsList.Clear()
            snapshot.DataSet.pAddressAssetsList.Add("Value", Entities_Input.Text)
            snapshot.DataSet.pAssetAddressValueFlag = VALUE_FLAG
        End If

        'pAccountAddressList update
        If AccRefEdit.get_Value <> "" Then                                                  ' Case new Account range
            snapshot.DataSet.pAddressAccountsList.Clear()
            For Each cell As Excel.Range In apps.Range(AccRefEdit.get_Value)
                snapshot.DataSet.pAddressAccountsList.Add(CStr(cell.get_Value), CStr(cell.Value2))
            Next
        ElseIf AccountsInput.Text <> "" Then
            snapshot.DataSet.pAddressAccountsList.Clear()
            snapshot.DataSet.pAddressAccountsList.Add("Value", AccountsInput.Text)
            snapshot.DataSet.pAccountAddressValueFlag = VALUE_FLAG
        End If

        'pDatesAddressList update
        If PeriodRefEdit.get_Value <> "" Then
            snapshot.DataSet.pAddressDatesList.Clear()
            For Each cell As Excel.Range In apps.Range(PeriodRefEdit.get_Value)
                snapshot.DataSet.pAddressDatesList.Add(CStr(cell.Address), CStr(cell.Value2))
            Next
        ElseIf PeriodInput.Text <> "" Then
            snapshot.DataSet.pAddressDatesList.Clear()
            snapshot.DataSet.pAddressDatesList.Add("Value", PeriodInput.Text)
            snapshot.DataSet.pDateAddressValueFlag = VALUE_FLAG
        End If

        Me.Close()
        snapshot.Show()

    End Sub


End Class