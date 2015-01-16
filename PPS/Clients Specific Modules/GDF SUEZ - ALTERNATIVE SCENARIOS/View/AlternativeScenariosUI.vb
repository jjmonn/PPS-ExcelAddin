' AlternativeScenariosUI.vb
'
'
' To do: 
'       -
'
'
'
' Author:Julien Monnereau
' Last modified:16/01/2015


Imports System.Windows.Forms
Imports System.Windows.Forms.DataVisualization.Charting
Imports VIBlend.WinForms.DataGridView
Imports System.Collections



Friend Class AlternativeScenariosUI


#Region "Instance Variables"

    Private Controller As AlternativeScenariosController
    Private InputsController As ASInputsController

    Private VersionsTV As TreeView
    Private EntitiesTV As TreeView
    Private MarketPricesTV As treeview

    Friend PBar As New ProgressBarControl

    ' Variables


    ' Constants



#End Region


#Region "Initialization"

    Protected Friend Sub New(ByRef input_controller As AlternativeScenariosController, _
                             ByRef input_as_inputs_controller As ASInputsController)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Controller = input_controller
        InputsController = input_as_inputs_controller


    End Sub


    Private Sub FModellingUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.WindowState = FormWindowState.Maximized
        Me.Controls.Add(PBar)                           ' Progress Bar
        PBar.Left = (Me.Width - PBar.Width) / 2         ' Progress Bar
        PBar.Top = (Me.Height - PBar.Height) / 2        ' Progress Bar

    End Sub



#End Region

#Region "Interface"


    Protected Friend Sub AddInputsTabElement(ByRef input_entitiesTV As TreeView, _
                                             ByRef input_versionsTV As TreeView, _
                                             ByRef input_marketpricesTV As treeview)

        EntitiesTVPanel.Controls.Add(input_entitiesTV)
        VersionsTVpanel.Controls.Add(input_versionsTV)
        MarketPricesTVPanel.Controls.Add(input_marketpricesTV)

        input_entitiesTV.Dock = DockStyle.Fill
        input_versionsTV.Dock = DockStyle.Fill
        input_marketpricesTV.Dock = DockStyle.Fill
      
        VersionsTV = input_versionsTV
        EntitiesTV = input_entitiesTV
        MarketPricesTV = input_marketpricesTV

    End Sub

#End Region




#Region "Call Backs"


    Private Sub ComputeScenarioBT_Click(sender As Object, e As EventArgs) Handles ComputeScenarioBT.Click

        Controller.ComputeAlternativeScenario()

    End Sub


#End Region




End Class