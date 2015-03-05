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
' Last modified: 05/03/2015



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

    Friend Function Init() As Boolean

        VERSEL = New VersionSelection(VersioningTVIL, Me)
        InsertDataVersionSelection()
        AddHandler ValidateBT.Click, AddressOf VERSEL.SetSelectedVersion
        Return False

    End Function

    Private Sub InsertDataVersionSelection()

        TableLayoutPanel1.Controls.Add(VERSEL.versionsTV, 0, 1)
        TableLayoutPanel1.GetControlFromPosition(0, 1).Dock = DockStyle.Fill

    End Sub


#End Region


#Region "Interface"

    Public Sub ClearAndClose()

        If Not VERSEL Is Nothing Then
            VERSEL.versionsTV.Nodes.Clear()
            TableLayoutPanel1.Controls.Remove(TableLayoutPanel1.GetControlFromPosition(0, 1))
            VERSEL = Nothing
            Me.Hide()
        End If

    End Sub

    Protected Friend Sub SetVersion(ByRef version_id As String)

        If VERSEL Is Nothing Then VERSEL = New VersionSelection(VersioningTVIL, Me)
        VERSEL.versionsTV.SelectedNode = VERSEL.versionsTV.Nodes.Find(version_id, True)(0)
        VERSEL.SetSelectedVersion()

    End Sub

#End Region


#Region "Events"

    Private Sub ADXExcelTaskPane1_ADXBeforeTaskPaneShow(sender As Object, e As ADXBeforeTaskPaneShowEventArgs) Handles MyBase.ADXBeforeTaskPaneShow

        Me.Visible = GlobalVariables.VersionsSelectionPaneVisible

    End Sub

    Private Sub CVersionSelectionPane_Deactivate(sender As Object, e As EventArgs) Handles MyBase.Deactivate

        ClearAndClose()

    End Sub


#End Region



End Class
