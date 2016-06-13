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
' Last modified: 16/12/2015


Imports System.Runtime.InteropServices
Imports AddinExpress.XL
Imports System.Windows.Forms


Public Class VersionSelectionPane

#Region "Instance Variables"

    Private m_versionSelectionController As VersionSelection
    Private mode As Int32

#End Region

#Region "Initialize"

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        InitializeMultiLanguage()
    End Sub

    Private Sub InitializeMultiLanguage()

        m_versionSelectionLabel.Text = Local.GetValue("general.select_version")
        m_validateButton.Text = Local.GetValue("general.validate")

    End Sub

    Friend Function Init() As Boolean

        m_versionSelectionController = New VersionSelection(m_versionsTreeviewImageList, Me)
        InsertDataVersionSelection()
        AddHandler m_validateButton.Click, AddressOf m_versionSelectionController.SetSelectedVersion
        Return False

    End Function

    Private Sub InsertDataVersionSelection()

        TableLayoutPanel1.Controls.Clear()
        TableLayoutPanel1.Controls.Add(m_versionSelectionController.versionsTV, 0, 1)
        TableLayoutPanel1.GetControlFromPosition(0, 1).Dock = DockStyle.Fill

    End Sub

#End Region

#Region "Interface"

    Public Sub ClearAndClose()

        If Not m_versionSelectionController Is Nothing Then
            m_versionSelectionController.versionsTV.Nodes.Clear()
            TableLayoutPanel1.Controls.Remove(TableLayoutPanel1.GetControlFromPosition(0, 1))
            m_versionSelectionController = Nothing
            Me.Hide()
        End If

    End Sub

    Friend Sub SetVersion(ByRef version_id As String)

        If m_versionSelectionController Is Nothing Then m_versionSelectionController = New VersionSelection(m_versionsTreeviewImageList, Me)
        m_versionSelectionController.versionsTV.SelectedNode = m_versionSelectionController.versionsTV.Nodes.Find(version_id, True)(0)
        m_versionSelectionController.SetSelectedVersion()

    End Sub

#End Region

#Region "Events"

    Private Sub ADXExcelTaskPane1_ADXBeforeTaskPaneShow(sender As Object, e As ADXBeforeTaskPaneShowEventArgs) Handles MyBase.ADXBeforeTaskPaneShow
        Me.Visible = GlobalVariables.VersionsSelectionTaskPaneVisible
    End Sub

    Private Sub CVersionSelectionPane_Deactivate(sender As Object, e As EventArgs) Handles MyBase.Deactivate
        ClearAndClose()
    End Sub


#End Region


End Class
