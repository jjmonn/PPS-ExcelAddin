' CInputSelectionPane.vb
' 
' Task pane for Entity Input Report Selection
'
' To do:
'       - simplification actuelle -> le choix des periods est enlevé
'
'
'
' Known bugs:
'
'
'
'
' Author: Julien Monnereau
' Last modified: 16/12/2015


Imports System.Runtime.InteropServices
Imports AddinExpress.XL
Imports System.Collections
Imports System.Windows.Forms



Public Class ReportUploadEntitySelectionPane


#Region "Instance Variables"

    Private ADDIN As AddinModule

#End Region


#Region "Initialize"

    Public Sub New()
        MyBase.New()

        InitializeComponent()
        EntitiesTV.ImageList = EntitiesTVImageList

    End Sub

    ' Init TV and combo boxes
    Friend Sub InitializeSelectionChoices(ByRef AddinInstance As AddinModule)

        If EntitiesTV.Nodes.Count > 0 Then EntitiesTV.Nodes.Clear()
        ADDIN = AddinInstance
        GlobalVariables.AxisElems.LoadEntitiesTV(EntitiesTV)
        EntitiesTV.CollapseAll()

    End Sub

#End Region


#Region "Interface"

    ' Validate
    Private Sub ValidateInputSelection()
        If Not EntitiesTV.SelectedNode Is Nothing AndAlso EntitiesTV.SelectedNode.Nodes.Count = 0 Then ADDIN.InputReportPaneCallBack_ReportCreation()
    End Sub


#End Region


#Region "Events"

    Private Sub EntitiesTV_NodeMouseDoubleClick(sender As Object, e As Windows.Forms.TreeNodeMouseClickEventArgs) Handles EntitiesTV.NodeMouseDoubleClick
        ValidateInputSelection()
    End Sub

    Private Sub EntitiesTV_KeyPress(sender As Object, e As Windows.Forms.KeyPressEventArgs) Handles EntitiesTV.KeyPress
        If e.KeyChar = ChrW(Keys.Return) Then
            ValidateInputSelection()
        End If
    End Sub

    Private Sub ValidateBT_Click(sender As Object, e As EventArgs)
        ValidateInputSelection()
    End Sub

#Region "Form show and close events"

    Private Sub ADXExcelTaskPane1_ADXBeforeTaskPaneShow(sender As Object, e As ADXBeforeTaskPaneShowEventArgs) Handles MyBase.ADXBeforeTaskPaneShow
        Me.Visible = GlobalVariables.InputSelectionTaskPaneVisible
    End Sub

    Private Sub CInputSelectionPane_ADXAfterTaskPaneShow(sender As Object, e As ADXAfterTaskPaneShowEventArgs) Handles MyBase.ADXAfterTaskPaneShow

        EntitiesTV.Select()
        If EntitiesTV.Nodes.Count > 0 Then EntitiesTV.SelectedNode = EntitiesTV.Nodes(0)

    End Sub

    Private Sub CInputSelectionPane_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
    End Sub

#End Region

#End Region




End Class
