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
    Friend Shared NetworkConnect As NetworkLauncher
    Friend Shared ConnectionState As Boolean = False
    Friend Shared AuthenticationFlag As Boolean = False
    
#End Region


#Region "Task Panes"

    Friend Shared VersionSelectionTaskPane As VersionSelectionPane
    Friend Shared ProcessSelectionTaskPane As ProcessSelectionTaskPane
    Friend Shared InputReportTaskPane As ReportUploadEntitySelectionPane
    Friend Shared VersionButton As ADXRibbonButton
    Friend Shared VersionlabelSubRibbon As ADXRibbonEditBox
    Friend Shared ProcessButton As ADXRibbonButton

#End Region


#Region "Menu Display"

    Friend Shared ConnectionToggleButton As ADXRibbonButton

    Friend Shared InputSelectionTaskPaneVisible As Boolean
    Friend Shared VersionsSelectionTaskPaneVisible As Boolean
    Friend Shared ProcessSelectionTaskPaneVisible As Boolean
    Friend Shared EntitySelectionTaskPaneVisible As Boolean
    Friend Shared ConnectionTaskPaneVisible As Boolean

#End Region


#Region "Computing"

    Friend Shared APPS As Application
    Friend Shared GlobalPPSBIController As FBIFunctionController
    Friend Shared g_mustResetCache As Boolean

#End Region


#Region "Submission Process Global Variables"

    Friend Shared s_reportUploadSidePane As New ReportUploadAccountInfoSidePane
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
    Friend Shared UserAllowedEntities As UserAllowedEntityManager
    Friend Shared FModelingsAccounts As FModelingAccountManager
    Friend Shared AxisElems As AxisElemManager
    Friend Shared AxisFilters As AxisFilterManager
    Friend Shared EntityCurrencies As EntityCurrencyManager
    Friend Shared EntitiesDistributions As EntityDistributionManager
    Friend Shared AxisParents As AxisParentManager

#End Region



End Class
