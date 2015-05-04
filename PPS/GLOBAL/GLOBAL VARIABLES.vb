' GlobalVariables.vb
'
'
' To do:
'       - Change syntax : Xxxx_Xxxx for Global_Variables ?
'
'
' Author: Julien Monnereau
' Last modified: 04/05/2015


Imports AddinExpress.MSO
Imports Microsoft.Office.Interop.Excel



Friend Class GlobalVariables

#Region "Versioning"

    Protected Friend Shared Version_Label As ADXRibbonLabel
    Protected Friend Shared Version_label_Sub_Ribbon As ADXRibbonEditBox
    Protected Friend Shared Rates_Version_Label As ADXRibbonLabel
    Protected Friend Shared GLOBALCurrentVersionCode As String
    Protected Friend Shared GLOBALCurrentRatesVersionCode As String

#End Region

#Region "Menu Display"

    Protected Friend Shared Connection_Toggle_Button As ADXRibbonButton
    Protected Friend Shared InputSelectionPaneVisible As Boolean
    Protected Friend Shared VersionsSelectionPaneVisible As Boolean
    Protected Friend Shared EntitySelectionPaneVisible As Boolean

#End Region

#Region "Computing"

    Protected Friend Shared APPS As Application
    Protected Friend Shared GenericGlobalSingleEntityComputer As GenericSingleEntityDLL3Computer        ' change the name of this global instance
    Protected Friend Shared GenericGlobalAggregationComputer As GenericAggregationDLL3Computing
    Protected Friend Shared GlobalDll3Interface As DLL3_Interface
    Protected Friend Shared GlobalDBDownloader As DataBaseDataDownloader
    Protected Friend Shared GlobalPPSBIController As PPSBIController
  
#End Region

#Region "Credentials"

    Protected Friend Shared User_Credential As Int32             ' User credential for the current session -> gives a VIEW combined with a table name 
    Protected Friend Shared Current_User_ID As String            ' Current user ID
    Protected Friend Shared Entities_View As String              ' Default ACF_Lentities.entities table VIEW used to provide info on entities

#End Region

#Region "Submission Process Global Variables"

    Protected Friend Shared SubmissionStatusButton As ADXRibbonButton
    Protected Friend Shared WSHasChangedSinceLastSubmission As Boolean
    Protected Friend Shared ClientsIDDropDown As ADXRibbonDropDown
    Protected Friend Shared ProductsIDDropDown As ADXRibbonDropDown
    Protected Friend Shared AdjustmentIDDropDown As ADXRibbonDropDown
  
#End Region



End Class
