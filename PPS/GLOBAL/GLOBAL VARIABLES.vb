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



Friend Class GlobalVariables

#Region "Connections and Server"

    Friend Shared Connection As ADODB.Connection
    Friend Shared database As String = My.Settings.database

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
    Friend Shared GenericGlobalSingleEntityComputer As GenericSingleEntityDLL3Computer        ' change the name of this global instance
    Friend Shared GenericGlobalAggregationComputer As GenericAggregationDLL3Computing
    Friend Shared GlobalDll3Interface As DLL3_Interface
    Friend Shared GlobalDBDownloader As DataBaseDataDownloader
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



End Class
