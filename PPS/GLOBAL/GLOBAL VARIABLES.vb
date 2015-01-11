' GLOBAL VARIABLES.vb
'
' List and explains the global variables
'
'
'
' To do:
'       - Change syntax : Xxxx_Xxxx for Global_Variables
'
'
'
' Known Bugs
'
'
' Author: Julien Monnereau
' Last modified: 29/08/2014


Imports AddinExpress.MSO
Imports Microsoft.Office.Interop.Excel



Module GLOBAL_VARIABLES


    ' General
    Friend APPS As Application
  
    '  Friend Addin_Instance As AddinModule
    Friend Version_Label As ADXRibbonLabel
    Friend Rates_Version_Label As ADXRibbonLabel
    Friend Version_label_Sub_Ribbon As ADXRibbonEditBox
    Friend GLOBALCurrentVersionCode As String
    Friend GLOBALCurrentRatesVersionCode As String

    Friend Connection_Toggle_Button As ADXRibbonButton
    Friend InputSelectionPaneVisible As Boolean
    Friend VersionsSelectionPaneVisible As Boolean
    Friend EntitySelectionPaneVisible As Boolean

    'friend VersionsDictionary As hashtable

    ' Computation and refresh
    Friend GENERICDCGLobalInstance As GenericSingleEntityComputer        ' change the name of this global instance
    Friend UDFCALLBACKINSTANCE As cPPSBIControl
  

    ' Credential
    Friend User_Credential As Int32             ' User credential for the current session -> gives a VIEW combined with a table name 
    Friend Current_User_ID As String            ' Current user ID
    Friend Entities_View As String              ' Default ACF_Lentities.entities table VIEW used to provide info on entities

    ' Constants
    Public Const MONTHLY_TIME_PERIOD_FORMAT As String = ""
    Public Const YEARLY_TIME_PERIOD_FORMAT As String = ""

#Region "Submission Process Global Variables"

    Friend SubmissionStatusButton As ADXRibbonButton
    Friend WSHasChangedSinceLastSubmission As Boolean

#End Region






End Module
