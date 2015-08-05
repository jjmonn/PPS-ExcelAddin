' GlobalVariables.vb
'
'
' To do:
'       - Change syntax : Xxxx_Xxxx for Global_Variables ?
'
'
' Author: Julien Monnereau
' Last modified: 26/06/2015


Imports AddinExpress.MSO
Imports Microsoft.Office.Interop.Excel

'Imports Microsoft.Office.Interop.Excel



Friend Class GlobalVariables

#Region "Connections and Server"

    Friend Shared Connection As ADODB.Connection
    Friend Shared NetworkConnect As NetworkLauncher
    Friend Shared database As String = My.Settings.database
    Friend Shared timeOut As UInt16 = 5

#End Region

#Region "Versioning"

    Friend Shared Version_Label As ADXRibbonLabel
    Friend Shared Version_label_Sub_Ribbon As ADXRibbonEditBox
    Friend Shared Rates_Version_Label As ADXRibbonLabel
    Friend Shared GLOBALCurrentVersionCode As String
    Friend Shared GLOBALCurrentRatesVersionCode As String

#End Region

#Region "Menu Display"

    Friend Shared Connection_Toggle_Button As ADXRibbonButton
    Friend Shared InputSelectionPaneVisible As Boolean
    Friend Shared VersionsSelectionPaneVisible As Boolean
    Friend Shared EntitySelectionPaneVisible As Boolean
    Friend Shared ConnectionPaneVisible As Boolean

#End Region

#Region "Computing"

    Friend Shared APPS As Application
    Friend Shared GlobalPPSBIController As PPSBIController

#End Region

#Region "Credentials"

    Friend Shared Current_User_ID As String            ' Current user ID

#End Region

#Region "Submission Process Global Variables"

    Friend Shared SubmissionStatusButton As ADXRibbonButton
    Friend Shared WSHasChangedSinceLastSubmission As Boolean
    Friend Shared ClientsIDDropDown As ADXRibbonDropDown
    Friend Shared ProductsIDDropDown As ADXRibbonDropDown
    Friend Shared AdjustmentIDDropDown As ADXRibbonDropDown

#End Region

#Region "Models"

    Friend Shared Accounts As Account
    Friend Shared Entities As Entity
    Friend Shared Filters As Filter
    Friend Shared FiltersValues As FilterValue
    Friend Shared Clients As Client
    Friend Shared Products As Product
    Friend Shared Adjustments As Adjustment
    Friend Shared EntitiesFilters As EntitiesFilter
    Friend Shared ClientsFilters As ClientsFilter
    Friend Shared ProductsFilters As ProductsFilter
    Friend Shared AdjustmentsFilters As AdjustmentFilter
    Friend Shared Versions As FactsVersion
    Friend Shared Currencies As Currency2

#End Region

End Class
