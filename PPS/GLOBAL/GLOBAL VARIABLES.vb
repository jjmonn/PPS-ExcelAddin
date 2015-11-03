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
    Friend Shared Filters As FilterManager
    Friend Shared FiltersValues As FilterValueManager
    Friend Shared Versions As VersionManager
    Friend Shared Currencies As CurrencyManager
    Friend Shared RatesVersions As RatesVersionManager
    Friend Shared GlobalFacts As GlobalFactManager
    Friend Shared GlobalFactsDatas As GlobalFactDataManager
    Friend Shared GlobalFactsVersions As GlobalFactVersionManager
    Friend Shared Users As UserManager
    Friend Shared Groups As GroupManager
    Friend Shared GroupAllowedEntities As GroupAllowedEntityManager
    Friend Shared FModelingsAccounts As FModelingAccountManager
    Friend Shared AxisElems As AxisElemManager
    Friend Shared AxisFilters As AxisFilterManager
    Friend Shared EntityCurrencies As EntityCurrencyManager

#End Region



End Class
