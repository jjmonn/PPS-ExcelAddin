' AlternativeScenariosUI.vb
'
' 
'
'
'
' To do: 
'       -
'
'
'
' Author:Julien Monnereau
' Last modified:28/01/2015


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections
Imports VIBlend.WinForms.DataGridView



Friend Class ASInputsController



#Region "Instance Variables"

    ' Object 
    Private ASController As AlternativeScenariosController
    Private View As AlternativeScenariosUI
    Private VersionsTV As New TreeView
    Protected Friend EntitiesTV As New TreeView
    Private MarketPricesTV As New TreeView

    ' Variables
    Private versions_id_list As List(Of String)
    Private market_index_versions_id_list As List(Of String)

    ' Current Inputs Selection
    Protected Friend current_version_id As String
    Protected Friend current_entity_node As TreeNode
    Protected Friend current_market_prices_version_id As String

    ' Display


#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef input_AScontroller As AlternativeScenariosController)

        ASController = input_AScontroller

        Version.LoadVersionsTree(VersionsTV)
        Globalvariables.Entities.LoadEntitiesTV(EntitiesTV)
        TreeViewsUtilities.CheckAllNodes(EntitiesTV)
        MarketIndexVersion.load_market_index_version_tv(MarketPricesTV)

        versions_id_list = VersionsMapping.GetVersionsList(ID_VARIABLE)
        market_index_versions_id_list = MarketIndexVersion.GetMarketIndexesVersionsList(MARKET_INDEXES_VERSIONS_ID_VAR)

        AddHandler VersionsTV.AfterSelect, AddressOf VersionsTV_AfterSelect
        AddHandler EntitiesTV.AfterSelect, AddressOf EntitiesTV_AfterSelect
        AddHandler MarketPricesTV.AfterSelect, AddressOf MarketPricesTV_AfterSelect
        AddHandler VersionsTV.KeyDown, AddressOf versionsTV_KeyDown
        AddHandler EntitiesTV.KeyDown, AddressOf entitiesTV_KeyDown
        AddHandler MarketPricesTV.KeyDown, AddressOf MarketPricesTV_KeyDown

    End Sub


#End Region


#Region "Interface"

    Protected Friend Sub InitializeView(ByRef input_view As AlternativeScenariosUI)

        View = input_view
        EntitiesTV.ImageList = View.EntitiesTVImageList
        VersionsTV.ImageList = View.VersionsTVIcons

        EntitiesTV.CollapseAll()
        VersionsTV.CollapseAll()
        MarketPricesTV.CollapseAll()

        View.AddInputsTabElement(EntitiesTV, VersionsTV, MarketPricesTV)

        View.NumericUpDown1.Value = 0
        View.NumericUpDown1.Increment = 1


    End Sub

    Protected Friend Function ValidateInputsSelection() As Boolean

        If current_version_id <> "" Then
            If Not current_entity_node Is Nothing Then
                If current_market_prices_version_id <> "" Then
                    Return True
                Else
                    MsgBox("A Market Prices Version must be selected")
                End If
            Else
                MsgBox("A Scope must be selected")
                Return False
            End If
        Else
            MsgBox("A Version must be selected")
            Return False
        End If

    End Function

#End Region


#Region "Events"

    Private Sub VersionsTV_AfterSelect(sender As Object, e As TreeViewEventArgs)

        If versions_id_list.Contains(e.Node.Name) Then
            current_version_id = e.Node.Name
            View.VersionTB.Text = e.Node.Text
        End If

    End Sub

    Private Sub EntitiesTV_AfterSelect(sender As Object, e As TreeViewEventArgs)

        View.EntityTB.Text = e.Node.Text
        current_entity_node = e.Node

    End Sub

    Private Sub MarketPricesTV_AfterSelect(sender As Object, e As TreeViewEventArgs)

        If market_index_versions_id_list.Contains(e.Node.Name) Then
            current_market_prices_version_id = e.Node.Name
            View.MarketPricesTB.Text = e.Node.Text
        End If

    End Sub

    Private Sub versionsTV_KeyDown(sender As Object, e As KeyEventArgs)

        Select Case e.KeyCode
            Case Keys.Enter
                EntitiesTV.Select()
                If Not EntitiesTV.Nodes(0) Is Nothing Then EntitiesTV.SelectedNode = EntitiesTV.Nodes(0)
        End Select

    End Sub

    Private Sub entitiesTV_KeyDown(sender As Object, e As KeyEventArgs)

        Select Case e.KeyCode
            Case Keys.Enter
                MarketPricesTV.Select()
                If Not MarketPricesTV.Nodes(0) Is Nothing Then MarketPricesTV.SelectedNode = MarketPricesTV.Nodes(0)
        End Select

    End Sub

    Private Sub MarketPricesTV_KeyDown(sender As Object, e As KeyEventArgs)

        Select Case e.KeyCode
            Case Keys.Enter : View.ComputeScenarioBT.Select()
        End Select

    End Sub


#End Region


#Region "Utilities"




#End Region



End Class
