' GlobalVariables.vb
'
'
'     
'
' Author: Julien Monnereau
' Last modified: 24/08/2015


Imports AddinExpress.MSO
Imports Microsoft.Office.Interop.Excel


Friend Class GlobalVariables

#Region "Connections and Server"

    Friend Shared Addin As AddinModule
    'Friend Shared Connection As ADODB.Connection
  Friend Shared NetworkConnect As NetworkLauncher
    Friend Shared ConnectionState As Boolean = False
    Friend Shared AuthenticationFlag As Boolean = False
    Friend Shared database As String = My.Settings.database
    Friend Shared timeOut As UInt16 = 5

#End Region


#Region "Versioning"

    Friend Shared VersionSelectionTaskPane As VersionSelectionPane
    Friend Shared Version_Button As ADXRibbonButton
    Friend Shared Version_label_Sub_Ribbon As ADXRibbonEditBox
  
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
    Friend Shared g_mustResetCache As Boolean

#End Region


#Region "Submission Process Global Variables"

    Friend Shared s_reportUploadSidePane As New ReportUploadSidePane
    Friend Shared s_reportUploadSidePaneVisible As Boolean = False
    Friend Shared SubmissionStatusButton As ADXRibbonButton
    Friend Shared WSHasChangedSinceLastSubmission As Boolean
    Friend Shared ClientsIDDropDown As ADXRibbonDropDown
    Friend Shared ProductsIDDropDown As ADXRibbonDropDown
    Friend Shared AdjustmentIDDropDown As ADXRibbonDropDown

#End Region


#Region "Models"

    Friend Shared Accounts As AccountManager
    Friend Shared Entities As Entity
    Friend Shared Filters As Filter
    Friend Shared FiltersValues As FilterValue
    Friend Shared Versions As Version
    Friend Shared Currencies As Currency
    Friend Shared RatesVersions As RatesVersion
    Friend Shared GlobalFacts As GlobalFact
    Friend Shared GlobalFactsDatas As GlobalFactData
    Friend Shared GlobalFactsVersions As GlobalFactVersion
    Friend Shared Users As User
    Friend Shared Groups As Group
    Friend Shared GroupAllowedEntities As GroupAllowedEntity
    Friend Shared FModelingsAccounts As FModelingAccount
    Friend Shared AxisElems As AxisElemManager
    Friend Shared AxisFilters As AxisFilterManager

#End Region



End Class
