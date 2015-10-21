'PlatformMGTGeneralUI.vb 





'Author: Julien Monnereau
'Last modified: 18/09/2015


Imports System.ComponentModel
Imports System.Threading.Tasks


Friend Class PlatformMGTGeneralUI


#Region "Instance Variables"

    'Objects
    Private current_controller As Object


#End Region

    Friend Sub New()

        '  This call is required by the designer.
        InitializeComponent()
        If Not GlobalVariables.Users.CurrentUserIsAdmin() Then GroupsBT.Enabled = False

        ' Add any initialization after the InitializeComponent() call.

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

    Private Sub EntitiesBT_Click(sender As Object, e As EventArgs) Handles EntitiesBT.Click

        closeCurrentControl()
        current_controller = New EntitiesController()
        current_controller.addControlToPanel(Panel1, Me)

    End Sub

    Private Sub ClientsBT_Click(sender As Object, e As EventArgs) Handles ClientsBT.Click

        closeCurrentControl()
        current_controller = New AxisController(GlobalVariables.Clients, GlobalVariables.ClientsFilters, GlobalEnums.AnalysisAxis.CLIENTS)
        current_controller.addControlToPanel(Panel1, Me)

    End Sub

    Private Sub ProductsBT_Click(sender As Object, e As EventArgs) Handles ProductsBT.Click

        closeCurrentControl()
        current_controller = New AxisController(GlobalVariables.Products, GlobalVariables.ProductsFilters, GlobalEnums.AnalysisAxis.PRODUCTS)
        current_controller.addControlToPanel(Panel1, Me)

    End Sub

    Private Sub ClientsFiltersBT_Click(sender As Object, e As EventArgs) Handles ClientsFiltersBT.Click

        closeCurrentControl()
        current_controller = New AxisFiltersController(GlobalEnums.AnalysisAxis.CLIENTS)
        current_controller.addControlToPanel(Panel1, Me)

    End Sub

    Private Sub EntitiesFiltersBT_Click(sender As Object, e As EventArgs) Handles EntitiesFiltersBT.Click

        closeCurrentControl()
        current_controller = New AxisFiltersController(GlobalEnums.AnalysisAxis.ENTITIES)
        current_controller.addControlToPanel(Panel1, Me)

    End Sub

    Private Sub ProductsFiltersBT_Click(sender As Object, e As EventArgs) Handles ProductsFiltersBT.Click

        closeCurrentControl()
        current_controller = New AxisFiltersController(GlobalEnums.AnalysisAxis.PRODUCTS)
        current_controller.addControlToPanel(Panel1, Me)

    End Sub

    Private Sub AdjustmentsFiltersBT_Click_1(sender As Object, e As EventArgs) Handles AdjustmentsFiltersBT.Click

        closeCurrentControl()
        current_controller = New AxisFiltersController(GlobalEnums.AnalysisAxis.ADJUSTMENTS)
        current_controller.addControlToPanel(Panel1, Me)

    End Sub

    Private Sub AdjustmentsBT_Click(sender As Object, e As EventArgs) Handles AdjustmentsBT.Click

        closeCurrentControl()
        current_controller = New AxisController(GlobalVariables.Adjustments, GlobalVariables.AdjustmentsFilters, GlobalEnums.AnalysisAxis.ADJUSTMENTS)
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

        closeCurrentControl()
        current_controller = New ControlsController()
        current_controller.addControlToPanel(Panel1, Me)

    End Sub

    Private Sub GroupsBT_Click(sender As Object, e As EventArgs) Handles GroupsBT.Click
        closeCurrentControl()
        current_controller = New GroupController()
        current_controller.addControlToPanel(Panel1, Me)
    End Sub

    Private Sub GlobalFactBT_Click(sender As Object, e As EventArgs) Handles GlobalFact_BT.Click
        closeCurrentControl()
        current_controller = New GlobalFactController()
        current_controller.AddControlToPanel(Panel1, Me)
    End Sub

#End Region

End Class