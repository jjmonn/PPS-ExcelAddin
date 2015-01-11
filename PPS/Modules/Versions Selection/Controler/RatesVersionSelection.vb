' cRatesVersionSelection.vb
' 
' 
'
'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 01/12/2014


Imports System.Windows.Forms


Public Class RatesVersionSelection
    Inherits VersionSelection


    Friend Sub New(ByRef input_image_list As ImageList, input_view_object As Object)
        MyBase.New(input_image_list, input_view_object)

    End Sub

    Friend Overrides Sub SetSelectedVersion()

        If Not rates_versionTV.SelectedNode Is Nothing _
        AndAlso rates_versions_list.Contains(rates_versionTV.SelectedNode.Name) Then
            GLOBALCurrentRatesVersionCode = rates_versionTV.SelectedNode.Name
            Rates_Version_Label.Caption = rates_versionTV.SelectedNode.Text
            VIEWOBJECT.ClearAndClose()
        End If

    End Sub

    Friend Overrides Sub Rates_VersionsTV_DoubleClick(sender As Object, e As Windows.Forms.TreeNodeMouseClickEventArgs)

        SetSelectedVersion()

    End Sub

    Friend Overrides Sub Rates_VersionsTV_KeyPress(sender As Object, e As Windows.Forms.KeyPressEventArgs)

        If e.KeyChar = ChrW(Keys.Return) Then
            SetSelectedVersion()
        End If

    End Sub

End Class
