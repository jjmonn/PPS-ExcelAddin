﻿'PlatformMGTGeneralUI.vb 





'Author: Julien Monnereau
'Last modified: 21/12/2015


Imports System.ComponentModel
Imports System.Threading.Tasks
Imports CRUD

Friend Class PlatformMGTGeneralUI


#Region "Instance Variables"

    'Objects
    Private current_controller As Object


#End Region

    Friend Sub New()

        '  This call is required by the designer.
        InitializeComponent()
        MultilanguageSetup()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub MultilanguageSetup()

        Me.AccountsBT.Text = Local.GetValue("GeneralEditionUI.accounts")
        Me.AccountsBT.ToolTipText = Local.GetValue("GeneralEditionUI.tool_tip_account")
        Me.m_entitiesBT.Text = Local.GetValue("GeneralEditionUI.entities")
        Me.m_entitiesBT.ToolTipText = Local.GetValue("GeneralEditionUI.tool_tip_entities")
        Me.CategoriesBT.Text = Local.GetValue("GeneralEditionUI.categories")
        Me.CategoriesBT.ToolTipText = Local.GetValue("GeneralEditionUI.tool_tip_categories")
        Me.m_entitiesFiltersBT.Text = Local.GetValue("general.entities_filters")
        Me.m_clientsFiltersBT.Text = Local.GetValue("general.clients_filters")
        Me.m_productsFiltersBT.Text = Local.GetValue("general.products_filters")
        Me.m_adjustmentsFiltersBT.Text = Local.GetValue("general.adjustments_filters")
        Me.m_employeesFiltersBT.Text = Local.GetValue("general.employees_filters")
        Me.ClientsBT.Text = Local.GetValue("general.clients")
        Me.ClientsBT.ToolTipText = Local.GetValue("GeneralEditionUI.tool_tip_clients")
        Me.ProductsBT.Text = Local.GetValue("general.products")
        Me.ProductsBT.ToolTipText = Local.GetValue("GeneralEditionUI.tool_tip_products")
        Me.AdjustmentsBT.Text = Local.GetValue("general.adjustments")
        Me.AdjustmentsBT.ToolTipText = Local.GetValue("GeneralEditionUI.tool_tip_adjustments")
        Me.VersionsBT.Text = Local.GetValue("general.versions")
        Me.VersionsBT.ToolTipText = Local.GetValue("GeneralEditionUI.tool_tip_versions")
        Me.CurrenciesBT.Text = Local.GetValue("general.currencies")
        Me.CurrenciesBT.ToolTipText = Local.GetValue("GeneralEditionUI.tool_tip_currencies")
        Me.GlobalFact_BT.Text = Local.GetValue("general.economic_indicators")
        Me.GlobalFact_BT.ToolTipText = Local.GetValue("GeneralEditionUI.tool_tip_economic_indicators")
        Me.ExchangeRatesButton.Text = Local.GetValue("GeneralEditionUI.exchange_rates")
        Me.ExchangeRatesButton.ToolTipText = Local.GetValue("GeneralEditionUI.tool_tip_exchange_rates")
        Me.GroupsBT.Text = Local.GetValue("GeneralEditionUI.users_groups")
        Me.GroupsBT.ToolTipText = Local.GetValue("GeneralEditionUI.tool_tip_users_groups")
        Me.Text = Local.GetValue("GeneralEditionUI.platform_config")


    End Sub

    Private Sub closeCurrentControl()

        If Not current_controller Is Nothing Then
            current_controller.close()
        End If

    End Sub

    Private Sub PlatformMGTGeneralUI_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        If Not current_controller Is Nothing Then
            current_controller.close()
        End If

    End Sub

    Private Sub PlatformMGTGeneralUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.WindowState = Windows.Forms.FormWindowState.Maximized

    End Sub

#Region "Main Menu Call Backs"


    Private Sub AccountsBT_Click(sender As Object, e As EventArgs) Handles AccountsBT.Click

        closeCurrentControl()
        current_controller = New AccountsController()
        current_controller.addControlToPanel(Panel1, Me)

    End Sub

    Private Sub EntitiesBT_Click(sender As Object, e As EventArgs) Handles m_entitiesBT.Click

        closeCurrentControl()
        current_controller = New AxisController(AxisType.Entities)
        current_controller.addControlToPanel(Panel1, Me)

    End Sub

    Private Sub m_employeesButton_Click(sender As Object, e As EventArgs) Handles m_employeesButton.Click

        closeCurrentControl()
        current_controller = New AxisController(AxisType.Employee)
        current_controller.addControlToPanel(Panel1, Me)

    End Sub

    Private Sub ConsultantsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles m_employeesFiltersBT.Click


        closeCurrentControl()
        current_controller = New AxisFiltersController(AxisType.Employee)
        current_controller.addControlToPanel(Panel1, Me)


    End Sub

    Private Sub ClientsBT_Click(sender As Object, e As EventArgs) Handles ClientsBT.Click

        closeCurrentControl()
        current_controller = New AxisController(AxisType.Client)
        current_controller.addControlToPanel(Panel1, Me)

    End Sub

    Private Sub ProductsBT_Click(sender As Object, e As EventArgs) Handles ProductsBT.Click

        closeCurrentControl()
        current_controller = New AxisController(AxisType.Product)
        current_controller.addControlToPanel(Panel1, Me)

    End Sub

    Private Sub ClientsFiltersBT_Click(sender As Object, e As EventArgs) Handles m_clientsFiltersBT.Click

        closeCurrentControl()
        current_controller = New AxisFiltersController(AxisType.Client)
        current_controller.addControlToPanel(Panel1, Me)

    End Sub

    Private Sub EntitiesFiltersBT_Click(sender As Object, e As EventArgs) Handles m_entitiesFiltersBT.Click

        closeCurrentControl()
        current_controller = New AxisFiltersController(AxisType.Entities)
        current_controller.addControlToPanel(Panel1, Me)

    End Sub

    Private Sub ProductsFiltersBT_Click(sender As Object, e As EventArgs) Handles m_productsFiltersBT.Click

        closeCurrentControl()
        current_controller = New AxisFiltersController(AxisType.Product)
        current_controller.addControlToPanel(Panel1, Me)

    End Sub

    Private Sub AdjustmentsFiltersBT_Click_1(sender As Object, e As EventArgs) Handles m_adjustmentsFiltersBT.Click

        closeCurrentControl()
        current_controller = New AxisFiltersController(AxisType.Adjustment)
        current_controller.addControlToPanel(Panel1, Me)

    End Sub

    Private Sub AdjustmentsBT_Click(sender As Object, e As EventArgs) Handles AdjustmentsBT.Click

        closeCurrentControl()
        current_controller = New AxisController(AxisType.Adjustment)
        current_controller.addControlToPanel(Panel1, Me)

    End Sub

    Private Sub VersionsBT_Click(sender As Object, e As EventArgs) Handles VersionsBT.Click

        closeCurrentControl()
        current_controller = New DataVersionsController()
        current_controller.addControlToPanel(Panel1, Me)

    End Sub

    Private Sub CurrenciesBT_Click(sender As Object, e As EventArgs) Handles CurrenciesBT.Click

        closeCurrentControl()
        current_controller = New CurrenciesController()
        current_controller.addControlToPanel(Panel1, Me)

    End Sub

    Private Sub ExchangeRatesButton_Click(sender As Object, e As EventArgs) Handles ExchangeRatesButton.Click

        closeCurrentControl()
        current_controller = New ExchangeRatesController()
        current_controller.addControlToPanel(Panel1, Me)

    End Sub

    Private Sub UsersBT_Click(sender As Object, e As EventArgs)

        closeCurrentControl()
        current_controller = New UsersController()
        current_controller.addControlToPanel(Panel1, Me)

    End Sub

    Private Sub ControlsBT_Click(sender As Object, e As EventArgs)

        'closeCurrentControl()
        'current_controller = New ControlsController()
        'current_controller.addControlToPanel(Panel1, Me)

    End Sub

    Private Sub GroupsBT_Click(sender As Object, e As EventArgs) Handles GroupsBT.Click
        closeCurrentControl()
        current_controller = New UsersController()
        current_controller.addControlToPanel(Panel1, Me)
    End Sub

    Private Sub GlobalFactBT_Click(sender As Object, e As EventArgs) Handles GlobalFact_BT.Click
        closeCurrentControl()
        current_controller = New GlobalFactController()
        current_controller.AddControlToPanel(Panel1, Me)
    End Sub

#End Region

   
   
End Class