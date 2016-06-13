' CEntitySelection.vb
'
' Display entity selection
'
'
' To do:
'       -  
'       -
'
'
' Known Bugs:
'       - 
'
'
'
' Author: Julien Monnereau
' Last modified: 19/08/2014


Imports System.Runtime.InteropServices
Imports AddinExpress.XL
Imports System.Windows.Forms


Public Class EntitySelectionTP


#Region "Instance Variables"

    Private entitiesTV As TreeView
    Private selectionRestrictedToInput As Boolean
    Private PARENTOBJECT As Object

#End Region


#Region "Initialize"

    Public Sub New()
        MyBase.New()

        InitializeComponent()

    End Sub

#End Region


#Region "Interface"

    Friend Sub Init(ByRef inputObject As Object, _
                   Optional ByRef RestrictionToInputsEntities As Boolean = False)

        PARENTOBJECT = inputObject
        selectionRestrictedToInput = RestrictionToInputsEntities
        entitiesTV = New TreeView
        entitiesTV.ImageList = EntitiesTVImageList
        GlobalVariables.AxisElems.LoadEntitiesTV(entitiesTV)
        TableLayoutPanel1.Controls.Add(entitiesTV, 0, 1)
        TableLayoutPanel1.GetControlFromPosition(0, 1).Dock = DockStyle.Fill
        entitiesTV.CollapseAll()
        AddHandler entitiesTV.NodeMouseDoubleClick, AddressOf EntitiesTV_NodeMouseDoubleClick
        AddHandler entitiesTV.KeyPress, AddressOf EntitiesTV_KeyPress

    End Sub

    Private Sub ValidateSelection()

        Select Case selectionRestrictedToInput
            Case True
                If entitiesTV.SelectedNode.Nodes.Count = 0 Then
                    PARENTOBJECT.ValidateEntitySelection(entitiesTV.SelectedNode.Text)
                Else
                    MsgBox("This entity is a Consolidation Level, please select an input Entity")
                End If
            Case False : PARENTOBJECT.ValidateEntitySelection(entitiesTV.SelectedNode.Text)
        End Select

    End Sub

    Friend Sub ClearAndClose()
        entitiesTV.Nodes.Clear()
        entitiesTV.Dispose()
        entitiesTV = Nothing
        TableLayoutPanel1.Controls.Remove(TableLayoutPanel1.GetControlFromPosition(0, 1))
        Me.Hide()
    End Sub

#End Region


#Region "Events and Calls back"

    Private Sub ValidateBT_Click(sender As Object, e As EventArgs) Handles ValidateBT.Click
        ValidateSelection()
    End Sub

    Private Sub EntitiesTV_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs)
        ValidateSelection()
    End Sub

    Private Sub EntitiesTV_KeyPress(sender As Object, e As KeyPressEventArgs)
        ValidateSelection()
    End Sub

    Private Sub EntitySelectionTP_ADXBeforeTaskPaneShow(sender As Object, e As ADXBeforeTaskPaneShowEventArgs) Handles MyBase.ADXBeforeTaskPaneShow
        Me.Visible = GlobalVariables.EntitySelectionTaskPaneVisible
    End Sub

#End Region





End Class
