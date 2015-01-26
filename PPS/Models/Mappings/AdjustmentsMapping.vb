'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 19/01/2014


Imports System.Collections
Imports System.Collections.Generic


Friend Class AdjustmentsMapping


    Protected Friend Shared Function GetAdjustmentsDictionary(ByRef Key As String, ByRef Value As String) As Dictionary(Of String, String)

        Dim srv As New ModelServer
        Dim tmpHT As New Dictionary(Of String, String)
        srv.OpenRst(CONFIG_DATABASE + "." + ADJUSTMENTS_TABLE, ModelServer.FWD_CURSOR)

        If srv.rst.EOF = False And srv.rst.BOF = False Then
            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False
                tmpHT.Add(srv.rst.Fields(Key).Value, srv.rst.Fields(Value).Value)
                srv.rst.MoveNext()
            Loop

        End If
        srv.rst.Close()
        srv = Nothing
        Return tmpHT

    End Function

    Protected Friend Shared Sub LoadAdjustmentsIDDD()

        IsLoadingAdjusmtentsIDs = True
        AdjustmentIDDropDown.Items.Clear()
 
        Dim srv As New ModelServer
        Dim tmpHT As New Dictionary(Of String, String)
        srv.OpenRst(CONFIG_DATABASE + "." + ADJUSTMENTS_TABLE, ModelServer.FWD_CURSOR)

        If srv.rst.EOF = False And srv.rst.BOF = False Then
            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False
                AddItemToAdjustmentsIDDD(srv.rst.Fields(ADJUSTMENTS_ID_VAR).Value, _
                                         srv.rst.Fields(ADJUSTMENTS_NAME_VAR).Value)
                srv.rst.MoveNext()
            Loop
        End If
        srv.rst.Close()
        AdjustmentIDDropDown.SelectedItemId = DEFAULT_ADJUSTMENT_ID
        IsLoadingAdjusmtentsIDs = False

    End Sub

    Protected Friend Shared Sub AddItemToAdjustmentsIDDD(ByVal id As String, _
                                                         ByVal caption As String)

        Dim adxRibbonItem As AddinExpress.MSO.ADXRibbonItem = New AddinExpress.MSO.ADXRibbonItem()
        adxRibbonItem.Caption = caption
        adxRibbonItem.Id = id
        adxRibbonItem.ImageTransparentColor = System.Drawing.Color.Transparent
        AdjustmentIDDropDown.Items.Add(adxRibbonItem)

    End Sub


End Class
