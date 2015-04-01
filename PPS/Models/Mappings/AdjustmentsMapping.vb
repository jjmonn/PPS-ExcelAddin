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

    Protected Friend Shared Function GetAdjustmentsIDsList(ByRef field As String) As List(Of String)

        Dim tmp_list As New List(Of String)
        Dim srv As New ModelServer
        srv.OpenRst(CONFIG_DATABASE + "." + ADJUSTMENTS_TABLE, ModelServer.FWD_CURSOR)
        Do While srv.rst.EOF = False
            tmp_list.Add(srv.rst.Fields(field).Value)
            srv.rst.MoveNext()
        Loop
        srv.rst.Close()
        Return tmp_list

    End Function

    Protected Friend Shared Sub LoadAdjustmentsIDDD()

        GlobalVariables.IsLoadingAdjusmtentsIDs = True
        GlobalVariables.AdjustmentIDDropDown.Items.Clear()
 
        Dim srv As New ModelServer
        srv.OpenRst(CONFIG_DATABASE + "." + ADJUSTMENTS_TABLE, ModelServer.FWD_CURSOR)

        If srv.rst.EOF = False And srv.rst.BOF = False Then
            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False
                AddItemToAdjustmentsIDDD(srv.rst.Fields(ANALYSIS_AXIS_ID_VAR).Value, _
                                         srv.rst.Fields(ANALYSIS_AXIS_NAME_VAR).Value)
                srv.rst.MoveNext()
            Loop
        End If
        srv.rst.Close()
        GlobalVariables.AdjustmentIDDropDown.SelectedItemId = DEFAULT_ADJUSTMENT_ID
        GlobalVariables.IsLoadingAdjusmtentsIDs = False

    End Sub

    Protected Friend Shared Sub AddItemToAdjustmentsIDDD(ByVal id As String, _
                                                         ByVal caption As String)

        Dim adxRibbonItem As AddinExpress.MSO.ADXRibbonItem = New AddinExpress.MSO.ADXRibbonItem()
        adxRibbonItem.Caption = caption
        adxRibbonItem.Id = id
        adxRibbonItem.ImageTransparentColor = System.Drawing.Color.Transparent
        GlobalVariables.AdjustmentIDDropDown.Items.Add(adxRibbonItem)

    End Sub


End Class
