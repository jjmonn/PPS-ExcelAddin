' CVersionSelectionPane.vb
'
' VIEW for versions selection
'
'
' To do:
'       -
'       -
'
'
' Known bugs:   
'       -
' 
'
' Author: Julien Monnereau
' Last modified: 01/12/2014



Imports System.Runtime.InteropServices
Imports AddinExpress.XL
Imports System.Windows.Forms

Public Class CVersionSelectionPane


#Region "Instance Variables"

    Private VERSEL As VersionSelection
    Private mode As Int32

#End Region


#Region "Initialize"


    Public Sub New()
        MyBase.New()

        InitializeComponent()

    End Sub

    ' mode 0: both rates and data version, 1: data version, 2: rates version
    Friend Sub Init(ByRef input_mode As Int32)

        Select Case input_mode
            Case 0
                VERSEL = New VersionSelection(VersioningTVIL, Me)
                InsertDataVersionSelection()
                InsertRatesVersionSelection()
            Case 1
                VERSEL = New VersionSelection(VersioningTVIL, Me)
                InsertDataVersionSelection()
            Case 2
                VERSEL = New RatesVersionSelection(VersioningTVIL, Me)
                InsertRatesVersionSelection()
        End Select
        mode = input_mode
        AddHandler ValidateButton.Click, AddressOf VERSEL.SetSelectedVersion

    End Sub

    Private Sub InsertDataVersionSelection()

        TableLayoutPanel1.Controls.Add(VERSEL.versionsTV, 0, 1)
        TableLayoutPanel1.GetControlFromPosition(0, 1).Dock = DockStyle.Fill

    End Sub

    Private Sub InsertRatesVersionSelection()

        TableLayoutPanel1.Controls.Add(VERSEL.rates_versionTV, 0, 3)
        TableLayoutPanel1.GetControlFromPosition(0, 3).Dock = DockStyle.Fill

    End Sub

#End Region


    Public Sub ClearAndClose()

        If Not VERSEL Is Nothing Then
            VERSEL.versionsTV.Nodes.Clear()
            Select Case mode
                Case 0
                    TableLayoutPanel1.Controls.Remove(TableLayoutPanel1.GetControlFromPosition(0, 1))
                    TableLayoutPanel1.Controls.Remove(TableLayoutPanel1.GetControlFromPosition(0, 3))
                Case 1 : TableLayoutPanel1.Controls.Remove(TableLayoutPanel1.GetControlFromPosition(0, 1))
                Case 2 : TableLayoutPanel1.Controls.Remove(TableLayoutPanel1.GetControlFromPosition(0, 3))
            End Select
            VERSEL = Nothing
            Me.Hide()
        End If

    End Sub


#Region "Events"

    Private Sub ADXExcelTaskPane1_ADXBeforeTaskPaneShow(sender As Object, e As ADXBeforeTaskPaneShowEventArgs) Handles MyBase.ADXBeforeTaskPaneShow

        Me.Visible = VersionsSelectionPaneVisible

    End Sub

    Private Sub CVersionSelectionPane_Deactivate(sender As Object, e As EventArgs) Handles MyBase.Deactivate

        ClearAndClose()

    End Sub


#End Region





    
   
End Class
