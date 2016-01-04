Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports System.Windows.Forms
Imports AddinExpress.MSO
Imports Excel = Microsoft.Office.Interop.Excel
Imports CRUD
Imports System.Collections.Generic
Imports System.Linq
Imports System.IO
Imports System.Reflection

'Add-in Express Add-in Module
<GuidAttribute("28A6804B-8A59-4465-86D0-3372EDC34E55"), ProgIdAttribute("FinancialBI.AddinModule")> _
Public Class AddinModule
    Inherits AddinExpress.MSO.ADXAddinModule


#Region "instance Variables"

    Friend WithEvents ComputeGroup As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents DataUploadGroup As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents m_financialBIMainRibbon As AddinExpress.MSO.ADXRibbonTab
    Friend WithEvents WSUplaodBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents ControlingUI2BT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents ConfigurationGroup As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents adxExcelEvents As AddinExpress.MSO.ADXExcelAppEvents
    Friend WithEvents UploadBT As AddinExpress.MSO.ADXRibbonSplitButton
    Friend WithEvents UplodBT1 As AddinExpress.MSO.ADXRibbonMenu
    Friend WithEvents WBUplaodBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents SettingsBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents BreakLinksBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents ConnectionGroup As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents LightsImageList As System.Windows.Forms.ImageList
    Friend WithEvents m_MainTabversionButton As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents Addin_Version_label As AddinExpress.MSO.ADXRibbonLabel
    Friend WithEvents ConnectionIcons As System.Windows.Forms.ImageList
    Friend WithEvents ConnectionBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonSeparator1 As AddinExpress.MSO.ADXRibbonSeparator
    Friend WithEvents SubmissionModeRibbon As AddinExpress.MSO.ADXRibbonTab
    Friend WithEvents EditSelectionGroup As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents ShowReportBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents SubmissionnGroup As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents m_financialSubmissionSubmitButton As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents StateSelectionGroup As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents CurrentEntityTB As AddinExpress.MSO.ADXRibbonEditBox
    Friend WithEvents AdxRibbonGroup9 As AddinExpress.MSO.ADXRibbonGroup
    Friend WithEvents CloseBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents VersionTBSubRibbon As AddinExpress.MSO.ADXRibbonEditBox
    Friend WithEvents m_financialSubmissionCancelButton As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents SubmissionRibbonIL As System.Windows.Forms.ImageList
    Friend WithEvents m_financialSubmissionSatusButton As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents CurrentLinkedWSBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonSeparator6 As AddinExpress.MSO.ADXRibbonSeparator
    Friend WithEvents m_financialSubmissionAutoCommitButton As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents EntCurrTB As AddinExpress.MSO.ADXRibbonEditBox
    Friend WithEvents RefreshInputsBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents entityEditBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents VersionBT2 As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents EditRangesMenuBT As AddinExpress.MSO.ADXRibbonSplitButton
    Friend WithEvents EditRangesMenu As AddinExpress.MSO.ADXRibbonMenu
    Friend WithEvents SelectAccRangeBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents SelectEntitiesRangeBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents SelectPeriodsRangeBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxExcelTaskPanesManager1 As AddinExpress.XL.ADXExcelTaskPanesManager
    Friend WithEvents InputSelectionTaskPaneItem As AddinExpress.XL.ADXExcelTaskPanesCollectionItem
    Friend WithEvents AdxRibbonLabel1 As AddinExpress.MSO.ADXRibbonLabel
    Friend WithEvents VersionSelectionTaskPaneItem As AddinExpress.XL.ADXExcelTaskPanesCollectionItem
    Friend WithEvents EntitySelectionTaskPaneItem As AddinExpress.XL.ADXExcelTaskPanesCollectionItem
    Friend WithEvents Addin_rates_version_label As AddinExpress.MSO.ADXRibbonLabel
    Friend WithEvents FunctionDesigner As AddinExpress.MSO.ADXRibbonSplitButton
    Friend WithEvents AdxRibbonMenu1 As AddinExpress.MSO.ADXRibbonMenu
    Friend WithEvents AdjustmentDropDown As AddinExpress.MSO.ADXRibbonDropDown
    Friend WithEvents MainTabImageList As System.Windows.Forms.ImageList
    Friend WithEvents SubmissionOptionsBT As AddinExpress.MSO.ADXRibbonSplitButton
    Friend WithEvents AdxRibbonMenu5 As AddinExpress.MSO.ADXRibbonMenu
    Friend WithEvents AdxRibbonSeparator4 As AddinExpress.MSO.ADXRibbonSeparator
    Friend WithEvents NewICOs As System.Windows.Forms.ImageList
    Friend WithEvents NewIcosSmall As System.Windows.Forms.ImageList
    Friend WithEvents ConfigurationRibbonBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonButton1 As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonButton2 As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonButton3 As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonButton4 As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents ClientsDropDown As AddinExpress.MSO.ADXRibbonDropDown
    Friend WithEvents ProductsDropDown As AddinExpress.MSO.ADXRibbonDropDown
    Friend WithEvents m_submissionWorksheetCombobox As AddinExpress.MSO.ADXRibbonDropDown
    Friend WithEvents RefreshBT As AddinExpress.MSO.ADXRibbonSplitButton
    Friend WithEvents RefreshMenu As AddinExpress.MSO.ADXRibbonMenu
    Friend WithEvents RefreshSelectionBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents RefreshWorksheetBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents RefreshWorkbookBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonMenuSeparator1 As AddinExpress.MSO.ADXRibbonMenuSeparator
    Friend WithEvents AutoRefreshBT As AddinExpress.MSO.ADXRibbonCheckBox
    Friend WithEvents financialModelingBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents ConnectionTaskPaneItem As AddinExpress.XL.ADXExcelTaskPanesCollectionItem
    Friend WithEvents Menu3 As System.Windows.Forms.ImageList
    Friend WithEvents AdxRibbonMenu3 As AddinExpress.MSO.ADXRibbonMenu
    Friend WithEvents SubmissionControlBT As AddinExpress.MSO.ADXRibbonSplitButton
    Friend WithEvents EditionMainRibbonBT As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents FormatButton As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents ReportUploadTaskPaneItem As AddinExpress.XL.ADXExcelTaskPanesCollectionItem
    Friend WithEvents m_reportUploadAccountInfoButton As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents AdxRibbonQuickAccessToolbar1 As AddinExpress.MSO.ADXRibbonQuickAccessToolbar
    Friend WithEvents AdxRibbonButton5 As AddinExpress.MSO.ADXRibbonButton
    Friend WithEvents BindingNavigator1 As System.Windows.Forms.BindingNavigator
    Friend WithEvents BindingNavigatorAddNewItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorCountItem As System.Windows.Forms.ToolStripLabel
    Friend WithEvents BindingNavigatorDeleteItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveFirstItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As System.Windows.Forms.ToolStripSeparator


#End Region


#Region " Add-in Express automatic code "

    'Required by Add-in Express - do not modify
    'the methods within this region

    Public Overrides Function GetContainer() As System.ComponentModel.IContainer
        If components Is Nothing Then
            components = New System.ComponentModel.Container
        End If
        GetContainer = components
    End Function

    <ComRegisterFunctionAttribute()> _
    Public Shared Sub AddinRegister(ByVal t As Type)
        AddinExpress.MSO.ADXAddinModule.ADXRegister(t)
    End Sub

    <ComUnregisterFunctionAttribute()> _
    Public Shared Sub AddinUnregister(ByVal t As Type)
        AddinExpress.MSO.ADXAddinModule.ADXUnregister(t)
    End Sub

    Public Overrides Sub UninstallControls()
        MyBase.UninstallControls()
    End Sub

#End Region


#Region "My Instance Variables"

    Friend m_ribbon As IRibbonUI
    Friend m_reportUploadControlersDictionary As New SafeDictionary(Of Excel.Worksheet, ReportUploadControler)
    Private m_worksheetNamesObjectDict As New SafeDictionary(Of String, Excel.Worksheet)
    Public setUpFlag As Boolean
    Public ppsbi_refresh_flag As Boolean = True
    Public m_ppsbiController As FBIFunctionController
    Private m_currentReportUploadControler As ReportUploadControler
    Private Const EXCEL_MIN_VERSION As Double = 9

#End Region


#Region "Task Panes"

    Private ReadOnly Property InputReportTaskPane() As ReportUploadEntitySelectionPane

        Get
            Dim taskPaneInstance As AddinExpress.XL.ADXExcelTaskPane = Nothing
            taskPaneInstance = InputSelectionTaskPaneItem.TaskPaneInstance

            If taskPaneInstance Is Nothing Then
                taskPaneInstance = InputSelectionTaskPaneItem.CreateTaskPaneInstance()
            End If

            Return TryCast(taskPaneInstance, ReportUploadEntitySelectionPane)
        End Get
    End Property

    Private ReadOnly Property VersionSelectionTaskPane() As VersionSelectionPane
        Get
            Dim taskPaneInstance As AddinExpress.XL.ADXExcelTaskPane = Nothing
            taskPaneInstance = VersionSelectionTaskPaneItem.TaskPaneInstance

            If taskPaneInstance Is Nothing Then
                taskPaneInstance = VersionSelectionTaskPaneItem.CreateTaskPaneInstance()
            End If
            Return TryCast(taskPaneInstance, VersionSelectionPane)
        End Get
    End Property

    Private ReadOnly Property ProcessSelectionTaskPane() As ProcessSelectionTaskPane
        Get
            Dim taskPaneInstance As AddinExpress.XL.ADXExcelTaskPane = Nothing
            taskPaneInstance = ProcessSelectionTaskPaneItem.TaskPaneInstance

            If taskPaneInstance Is Nothing Then
                taskPaneInstance = ProcessSelectionTaskPaneItem.CreateTaskPaneInstance()
            End If
            Return TryCast(taskPaneInstance, ProcessSelectionTaskPane)
        End Get
    End Property

    Friend ReadOnly Property EntitySelectionTaskPane As EntitySelectionTP
        Get
            Dim taskPaneInstance As AddinExpress.XL.ADXExcelTaskPane = Nothing
            taskPaneInstance = EntitySelectionTaskPaneItem.TaskPaneInstance

            If taskPaneInstance Is Nothing Then
                taskPaneInstance = EntitySelectionTaskPaneItem.CreateTaskPaneInstance()
            End If
            Return TryCast(taskPaneInstance, EntitySelectionTP)
        End Get
    End Property

    Friend ReadOnly Property ReportUploadTaskPane As ReportUploadAccountInfoSidePane
        Get
            Dim taskPaneInstance As AddinExpress.XL.ADXExcelTaskPane = Nothing
            taskPaneInstance = ReportUploadTaskPaneItem.TaskPaneInstance

            If taskPaneInstance Is Nothing Then
                taskPaneInstance = ReportUploadTaskPaneItem.CreateTaskPaneInstance()
            End If
            Return TryCast(taskPaneInstance, ReportUploadAccountInfoSidePane)
        End Get
    End Property

    Friend ReadOnly Property ConnectionTaskPane As ConnectionTP

        Get
            Dim taskPaneInstance As AddinExpress.XL.ADXExcelTaskPane = Nothing
            taskPaneInstance = ConnectionTaskPaneItem.TaskPaneInstance

            If taskPaneInstance Is Nothing Then
                taskPaneInstance = ConnectionTaskPaneItem.CreateTaskPaneInstance()
            End If
            Return TryCast(taskPaneInstance, ConnectionTP)
        End Get

    End Property

#End Region


#Region "Initialize"

    Public Shared Shadows ReadOnly Property CurrentInstance() As AddinModule
        Get
            Return CType(AddinExpress.MSO.ADXAddinModule.CurrentInstance, AddinModule)
        End Get
    End Property

    Public ReadOnly Property ExcelApp() As Excel._Application
        Get
            Return CType(HostApplication, Excel._Application)
        End Get
    End Property

    Public ReadOnly Property GetAuthenticationFlag() As Boolean
        Get
            Return GlobalVariables.AuthenticationFlag
        End Get
    End Property

    Private Sub AddinModule_AddinInitialize(sender As Object, e As EventArgs) Handles MyBase.AddinInitialize

        GlobalVariables.APPS = Me.HostApplication

        ' Main Ribbon Initialize
        GlobalVariables.VersionSelectionTaskPane = Me.VersionSelectionTaskPane
        GlobalVariables.ProcessSelectionTaskPane = Me.ProcessSelectionTaskPane
        GlobalVariables.InputReportTaskPane = Me.InputReportTaskPane
        GlobalVariables.SubmissionStatusButton = m_financialSubmissionSatusButton
        GlobalVariables.ConnectionToggleButton = Me.ConnectionBT
        GlobalVariables.VersionButton = Me.m_MainTabversionButton
        GlobalVariables.ProcessButton = Me.m_mainTabProcessButton
        ConnectionBT.Image = 0

        ' Submission Ribbon Initialize
        m_submissionWorksheetCombobox.Items.RemoveAt(0)
        SubmissionModeRibbon.Visible = False
        m_PDCSubmissionRibbon.Visible = False
        GlobalVariables.s_reportUploadSidePane = Me.ReportUploadTaskPane
        GlobalVariables.VersionlabelSubRibbon = VersionTBSubRibbon
        GlobalVariables.ClientsIDDropDown = ClientsDropDown
        GlobalVariables.ProductsIDDropDown = ProductsDropDown
        GlobalVariables.AdjustmentIDDropDown = AdjustmentDropDown

        ' CRUDs
        GlobalVariables.Accounts = New AccountManager
        GlobalVariables.Filters = New FilterManager
        GlobalVariables.FiltersValues = New FilterValueManager
        GlobalVariables.Versions = New VersionManager
        GlobalVariables.Currencies = New CurrencyManager
        GlobalVariables.RatesVersions = New RatesVersionManager
        GlobalVariables.GlobalFacts = New GlobalFactManager
        GlobalVariables.GlobalFactsDatas = New GlobalFactDataManager
        GlobalVariables.GlobalFactsVersions = New GlobalFactVersionManager
        GlobalVariables.Users = New UserManager
        GlobalVariables.Groups = New GroupManager
        GlobalVariables.UserAllowedEntities = New UserAllowedEntityManager
        GlobalVariables.FModelingsAccounts = New FModelingAccountManager
        GlobalVariables.AxisElems = New AxisElemManager
        GlobalVariables.AxisFilters = New AxisFilterManager
        GlobalVariables.EntityCurrencies = New EntityCurrencyManager
        GlobalVariables.EntitiesDistributions = New EntityDistributionManager
        GlobalVariables.AxisParents = New AxisParentManager

        ' Financial Bi User Defined Function
        GlobalVariables.GlobalPPSBIController = New FBIFunctionController
        GlobalVariables.Addin = Me
        SetMainMenuButtonState(False)

        Select Case My.Settings.language
            Case 0 : Local.LoadLocalFile(My.Resources.english)
            Case 1 : Local.LoadLocalFile(My.Resources.french)
            Case Else : Local.LoadLocalFile(My.Resources.english)
        End Select


        If (Me.IsNetworkDeployed().ToString()) Then
            Me.CheckForUpdates()
            My.Settings.Upgrade()
        End If

    End Sub

    Friend Shared Sub SetMainMenuButtonState(ByRef p_state As Boolean)
        Dim Addin As AddinModule = GlobalVariables.Addin

        Addin.m_MainTabversionButton.Enabled = p_state
        Addin.m_mainTabProcessButton.Enabled = p_state
        Addin.UploadBT.Enabled = p_state
        Addin.EditionMainRibbonBT.Enabled = p_state
        Addin.RefreshBT.Enabled = p_state
        Addin.ControlingUI2BT.Enabled = p_state
        Addin.FunctionDesigner.Enabled = p_state
        Addin.financialModelingBT.Enabled = p_state
        Addin.FormatButton.Enabled = p_state
        Addin.ConfigurationRibbonBT.Enabled = p_state
        Addin.m_submissionFolloupButton.Enabled = p_state

    End Sub

#Region "Properties Getters"

    ' Returns the connection instance variable
    'Public Function GetAddinConnection() As ADODB.Connection

    '    Return GlobalVariables.Connection

    'End Function

    ' Returns the entities View variable
    Public Function GetVersionButton() As ADXRibbonButton
        Return GlobalVariables.VersionButton
    End Function


#End Region

#End Region


#Region "Call Backs"

#Region "Main Ribbon"

#Region "Connection"

    Private Sub ConnectionBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles ConnectionBT.OnClick

        If CDbl(GlobalVariables.APPS.Version.Replace(".", ",")) > EXCEL_MIN_VERSION Then

            GlobalVariables.ConnectionTaskPaneVisible = True
            Me.ConnectionTaskPane.Init(Me)
            Me.ConnectionTaskPane.Show()

        Else
            Dim CONNECTIONUI As New ConnectionUI(Me)
            CONNECTIONUI.Show()
        End If

        ' setupflag to be set !!

    End Sub

    Private Sub VersionBT_OnClick_1(sender As Object, control As IRibbonControl, pressed As Boolean) Handles m_MainTabversionButton.OnClick

        If GlobalVariables.AuthenticationFlag = False Then
            ConnectionBT_OnClick(sender, control, pressed)
        Else
            LaunchVersionSelection()
        End If

    End Sub

    Private Sub m_mainTabProcessButton_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles m_mainTabProcessButton.OnClick

        If GlobalVariables.AuthenticationFlag = False Then
            ConnectionBT_OnClick(sender, control, pressed)
        Else
            LaunchProcessSelection()
        End If

    End Sub

#End Region

#Region "Financial BI Data Acquisition"

    Private Sub snapshotBT_onclick(sender As System.Object,
                                 control As AddinExpress.MSO.IRibbonControl,
                                 pressed As System.Boolean) Handles UploadBT.OnClick

        If GlobalVariables.AuthenticationFlag = False Then
            ConnectionBT_OnClick(sender, control, pressed)
        Else
            If m_reportUploadControlersDictionary.ContainsKey(GlobalVariables.APPS.ActiveSheet) Then
                m_currentReportUploadControler = m_reportUploadControlersDictionary(GlobalVariables.APPS.ActiveSheet)
                m_currentReportUploadControler.UpdateRibbon()
                SubmissionModeRibbon.Visible = True
            Else
                ' -> choix adjustment, client, product
                AssociateReportUploadControler(False)
            End If
        End If

    End Sub

    Private Sub WSUploadBT_onclick(sender As System.Object,
                                  control As AddinExpress.MSO.IRibbonControl,
                                  pressed As System.Boolean) Handles WSUplaodBT.OnClick

        If GlobalVariables.AuthenticationFlag = False Then
            ConnectionBT_OnClick(sender, control, pressed)
        Else
            '    Dim DSDUI As New SubmissionControlUI
            '    DSDUI.Show()
        End If

    End Sub

    Private Sub CurrentLinkedWSBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles CurrentLinkedWSBT.OnClick

    End Sub

    Private Sub InputReportLaunchBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles EditionMainRibbonBT.OnClick

        If GlobalVariables.AuthenticationFlag = False Then
            ConnectionBT_OnClick(sender, control, pressed)
        Else
            If CDbl(GlobalVariables.APPS.Version.Replace(".", ",")) > EXCEL_MIN_VERSION Then
                GlobalVariables.InputSelectionTaskPaneVisible = True
                Me.InputReportTaskPane.InitializeSelectionChoices(Me)
                Me.InputReportTaskPane.Show()
            Else
                ' display UI selection
            End If
        End If

    End Sub

#End Region

#Region "Download Data"

    Private Sub m_submissionFolloupButton_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles m_submissionFolloupButton.OnClick

        If GlobalVariables.AuthenticationFlag = False Then
            ConnectionBT_OnClick(sender, control, pressed)
        Else
            Dim l_submissionsFollowUpUI As New SubmissionsFollowUpView
            l_submissionsFollowUpUI.Show()
        End If

    End Sub

    Private Sub ControllingUI2BT_onclick(sender As System.Object,
                                        control As AddinExpress.MSO.IRibbonControl,
                                        pressed As System.Boolean) Handles ControlingUI2BT.OnClick

        If GlobalVariables.AuthenticationFlag = False Then
            ConnectionBT_OnClick(sender, control, pressed)
        Else
            Dim CONTROLLING As New ControllingUI_2()
            CONTROLLING.Show()
        End If

    End Sub

#Region "Refresh"

    Private Sub AdxRibbonSplitButton1_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles RefreshBT.OnClick, RefreshWorksheetBT.OnClick

        If GlobalVariables.AuthenticationFlag = False Then
            ConnectionBT_OnClick(sender, control, pressed)
        Else
            Dim cREFRESH As New WorksheetRefreshController
            cREFRESH.RefreshWorksheet(Me)
        End If

    End Sub

    Private Sub RefreshSelectionBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles RefreshSelectionBT.OnClick

        If GlobalVariables.AuthenticationFlag = False Then
            ConnectionBT_OnClick(sender, control, pressed)
        Else
            If Not m_reportUploadControlersDictionary.ContainsKey(GlobalVariables.APPS.ActiveSheet) Then
                Dim l_refreshWorksheetModule As New WorksheetRefreshController
                Dim ws As Excel.Worksheet = GlobalVariables.APPS.ActiveSheet
                l_refreshWorksheetModule.RefreshWorksheet(GlobalVariables.APPS.Selection, Me)
            Else
                MsgBox("Cannot Refresh while the worksheet is being edited. " + Chr(13) + _
                       "The submission must be closed for this worksheet in order to refresh it.")
            End If
        End If

    End Sub

    Private Sub RefreshWorkbookBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles RefreshWorkbookBT.OnClick

        ' to be implemented !

    End Sub

    Private Sub AutoRefreshBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles AutoRefreshBT.OnClick

        If ppsbi_refresh_flag = True Then

            ppsbi_refresh_flag = False
        Else
            ppsbi_refresh_flag = True
        End If


    End Sub

#End Region

    Private Sub PPSBIFuncBT_onclick(sender As System.Object,
                                    control As AddinExpress.MSO.IRibbonControl,
                                    pressed As System.Boolean) Handles FunctionDesigner.OnClick

        If GlobalVariables.AuthenticationFlag = False Then
            ConnectionBT_OnClick(sender, control, pressed)
        Else
            Dim PPSBI As New FBIFunctionUI
            PPSBI.Show()
        End If

    End Sub

    Private Sub BreakLinksBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles BreakLinksBT.OnClick

        WorksheetRefreshController.BreakLinks()

    End Sub

    Private Sub SubmissionsControlBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles SubmissionControlBT.OnClick

        If GlobalVariables.AuthenticationFlag = False Then
            ConnectionBT_OnClick(sender, control, pressed)
        Else
            Dim SubmissionControl As New SubmissionsControlsController
        End If

    End Sub


#End Region

#Region "Modeling"

    Private Sub FModelingBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles financialModelingBT.OnClick

        If GlobalVariables.AuthenticationFlag = False Then
            ConnectionBT_OnClick(sender, control, pressed)
        Else
            Dim FModellingui2 As New FModelingUI2
            FModellingui2.Show()
        End If

    End Sub

#End Region

#Region "Platform Management"

    Private Sub ConfigurationRibbonBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles ConfigurationRibbonBT.OnClick

        If GlobalVariables.AuthenticationFlag = False Then
            ConnectionBT_OnClick(sender, control, pressed)
        Else
            Dim MGTUI As New PlatformMGTGeneralUI
            MGTUI.Show()
        End If


    End Sub


#End Region

    Private Sub FormatButton_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles FormatButton.OnClick

        If GlobalVariables.AuthenticationFlag = False Then
            ConnectionBT_OnClick(sender, control, pressed)
        Else
            Dim startDate As Date = Nothing
            Dim version As CRUD.Version = GlobalVariables.Versions.GetValue(My.Settings.version_id)

            If Not version Is Nothing Then
                startDate = Date.FromOADate(version.StartPeriod)
            End If
            Dim l_currencyId As String = ""
            Dim l_currency As Currency = GlobalVariables.Currencies.GetValue(My.Settings.currentCurrency)
            If Not l_currency Is Nothing Then
                l_currencyId = l_currency.Id
            End If
            ExcelFormatting.FormatFinancialExcelRange(GlobalVariables.APPS.ActiveSheet.cells(1, 1), l_currencyId, startDate)
        End If

    End Sub

#Region "Settings"

    Private Sub SettingsBT_onclick(sender As System.Object,
                                    control As AddinExpress.MSO.IRibbonControl,
                                    pressed As System.Boolean) Handles SettingsBT.OnClick

        Dim SETTINGSUI As New SettingUI
        SETTINGSUI.Show()


    End Sub


#End Region

#End Region

#Region "Submission Ribbon"

#Region "Edit Selection"

    Private Sub HighlightBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean)

        m_currentReportUploadControler.HighlightItemsAndDataRegions()

    End Sub

    Private Sub RefreshInputsBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles RefreshInputsBT.OnClick

        ' Relaunch dataset.snapshot, orientation and get data if ok !!

        ' if successful and GRS state was false -> enable buttons - state = true

    End Sub

    Private Sub EditRangesMenuBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles EditRangesMenuBT.OnClick

        m_currentReportUploadControler.RangeEdition()

    End Sub

    Private Sub SelectAccRangeBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles SelectAccRangeBT.OnClick

    End Sub

    Private Sub SelectEntitiesRangeBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles SelectEntitiesRangeBT.OnClick

    End Sub

    Private Sub SelectPeriodsRangeBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles SelectPeriodsRangeBT.OnClick

    End Sub

#End Region

#Region "Submission"

    Private Sub SubmitBT2_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles m_financialSubmissionSubmitButton.OnClick
        m_currentReportUploadControler.DataSubmission()
    End Sub

    Private Sub SubmissionStatus_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles m_financialSubmissionSatusButton.OnClick
        m_currentReportUploadControler.DisplayUploadStatusAndErrorsUI()
    End Sub

    Private Sub CancelBT2_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles m_financialSubmissionCancelButton.OnClick
        ' allow to come back a few steps before (cf log ?)
    End Sub

    Private Sub AutoCommitBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles m_financialSubmissionAutoCommitButton.OnClick
        If m_currentReportUploadControler.m_autoCommitFlag = False Then
            m_currentReportUploadControler.m_autoCommitFlag = True
        Else
            m_currentReportUploadControler.m_autoCommitFlag = False
        End If
    End Sub

#End Region

#Region "Submission items"

    Private Sub entityEditBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles entityEditBT.OnClick

        LaunchEntitySelectionTP(Me, True)

    End Sub

    Private Sub VersionBT2_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles VersionBT2.OnClick

        If GlobalVariables.AuthenticationFlag = False Then
            ConnectionBT_OnClick(sender, control, pressed)
        Else
            LaunchVersionSelection()
        End If

    End Sub

#End Region

#Region "Display"

    Private Sub ReportOptionsBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean)
        ' to implement !!
    End Sub

    Private Sub CloseBT_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles CloseBT.OnClick
        ClearSubmissionMode(m_currentReportUploadControler)
    End Sub

    Private Sub m_reportUploadAccountInfoButton_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles m_reportUploadAccountInfoButton.OnClick
        DisplayReportUploadSidePane()
    End Sub

#End Region

#End Region

#Region "PDC Submission Ribbon"

    Private Sub m_PDCSubmissionButton_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles m_PDCSubmissionButton.OnClick
        m_currentReportUploadControler.DataSubmission()
    End Sub

    Private Sub m_PDCAutocommitButton_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles m_PDCAutocommitButton.OnClick
        If m_currentReportUploadControler.m_autoCommitFlag = False Then
            m_currentReportUploadControler.m_autoCommitFlag = True
        Else
            m_currentReportUploadControler.m_autoCommitFlag = False
        End If
    End Sub

    Private Sub m_PDCSUbmissionStatusButton_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles m_PDCSUbmissionStatusButton.OnClick
        m_currentReportUploadControler.DisplayUploadStatusAndErrorsUI()
    End Sub

    Private Sub m_PDCSubmissionCancelButton_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles m_PDCSubmissionCancelButton.OnClick
        ' to be implementd (ticket sur redmine)
    End Sub

    Private Sub m_PDCRefreshSnapthshotButton_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles m_PDCRefreshSnapthshotButton.OnClick
        RefreshSelectionBT_OnClick(sender, control, pressed)
    End Sub

    Private Sub m_PDCConsultantRangeEditButton_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles m_PDCConsultantRangeEditButton.OnClick
        ' to be implemented
    End Sub

    Private Sub m_PDCPeriodsRangeEditButton_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles m_PDCPeriodsRangeEditButton.OnClick
        ' to be implemented
    End Sub

    Private Sub m_PDCSumbissionExitButton_OnClick(sender As Object, control As IRibbonControl, pressed As Boolean) Handles m_PDCSumbissionExitButton.OnClick
        ClearSubmissionMode(m_currentReportUploadControler)
    End Sub

#End Region

#End Region


#Region "Events"

#Region "Main Ribbon"

    Private Sub AddinModule_OnRibbonLoaded(sender As Object, ribbon As IRibbonUI) Handles Me.OnRibbonLoaded
        Me.m_ribbon = ribbon
    End Sub

#End Region

#Region "Submission module"

    ' Associated Worksheet combobox on change event
    Private Sub m_submissionWorksheetCombobox_OnAction(sender As Object, _
                                                       Control As IRibbonControl, _
                                                       selectedId As String, _
                                                       selectedIndex As Integer) Handles m_submissionWorksheetCombobox.OnAction, _
                                                                                         m_PDCWorksheetDropDown.OnAction

        Try
            If Not m_currentReportUploadControler.m_associatedWorksheet.Name = selectedId Then
                ActivateReportUploadController(m_reportUploadControlersDictionary(m_worksheetNamesObjectDict(selectedId)))
                m_currentReportUploadControler.UpdateRibbon()
                m_worksheetNamesObjectDict(selectedId).Activate()
            End If
        Catch ex As Exception
            '    m_GRSControlersDictionary.Remove(m_ctrlsTextWSDictionary(selectedId))
            m_worksheetNamesObjectDict.Remove(selectedId)
            MsgBox("The worksheet has been deleted.")
        End Try

    End Sub

    ' Change in Clients DropDown
    Private Sub ClientsDropDown_OnAction(sender As Object, Control As IRibbonControl, selectedId As String, selectedIndex As Integer) Handles ClientsDropDown.OnAction

        GlobalVariables.APPS.Interactive = False
        m_currentReportUploadControler.UpdateAfterAnalysisAxisChanged(selectedId, _
                                                           ProductsDropDown.SelectedItemId, _
                                                           AdjustmentDropDown.SelectedItemId,
                                                           True)

    End Sub

    ' Change in Products DropDown
    Private Sub ProductsDropDown_OnAction(sender As Object, Control As IRibbonControl, selectedId As String, selectedIndex As Integer) Handles ProductsDropDown.OnAction

        GlobalVariables.APPS.Interactive = False
        m_currentReportUploadControler.UpdateAfterAnalysisAxisChanged(ClientsDropDown.SelectedItemId, _
                                                           selectedId, _
                                                           AdjustmentDropDown.SelectedItemId, _
                                                           True)

    End Sub

    ' Change in Adjustments DropDown
    Private Sub AdjustmentDD_OnAction(sender As Object, Control As IRibbonControl, selectedId As String, selectedIndex As Integer) Handles AdjustmentDropDown.OnAction

        GlobalVariables.APPS.Interactive = False
        m_currentReportUploadControler.UpdateAfterAnalysisAxisChanged(ClientsDropDown.SelectedItemId, _
                                                            ProductsDropDown.SelectedItemId, _
                                                            selectedId, _
                                                            True)

    End Sub

#End Region

#Region "Addin Events"

    Private Sub AddinModule_Finalize(sender As Object, e As EventArgs) Handles MyBase.AddinFinalize
        If m_reportUploadControlersDictionary.Count > 0 Then
            On Error Resume Next
            Dim l_reportUploadList = m_reportUploadControlersDictionary.Values
            For Each l_generalSubmissionController As ReportUploadControler In l_reportUploadList
                ClearSubmissionMode(l_generalSubmissionController)
            Next
        End If
        m_reportUploadControlersDictionary.Clear()
    End Sub


#End Region

#End Region


#Region "Task Panes Launches and call backs"

    Private Sub DisplayReportUploadSidePane()

        If CDbl(GlobalVariables.APPS.Version.Replace(".", ",")) > EXCEL_MIN_VERSION Then  ' 
            GlobalVariables.s_reportUploadSidePaneVisible = True
            GlobalVariables.s_reportUploadSidePane.Show()
        End If

    End Sub

    Private Shared Sub LaunchProcessSelection()
        If CDbl(GlobalVariables.APPS.Version.Replace(".", ",")) > EXCEL_MIN_VERSION Then
            GlobalVariables.ProcessSelectionTaskPaneVisible = True
            GlobalVariables.ProcessSelectionTaskPane.Show()
        Else
            MsgBox("This version of Excel is below Excel 2003 and therefore cannot display TaskPanes. Please contact Financial BI team.")
        End If
    End Sub

#Region "Version Selection"

    Public Sub SetVersion(ByRef version_id As String)
        Me.VersionSelectionTaskPane.SetVersion(version_id)
    End Sub

    Public Shared Sub LaunchVersionSelection()
        If CDbl(GlobalVariables.APPS.Version.Replace(".", ",")) > EXCEL_MIN_VERSION Then  ' 
            GlobalVariables.VersionsSelectionTaskPaneVisible = True
            GlobalVariables.VersionSelectionTaskPane.Init()
            GlobalVariables.VersionSelectionTaskPane.Show()
        Else
            ' Implement settings versions for version selection without panes
            Dim VERSELUI As New VersionSelectionUI
            VERSELUI.Show()
        End If
    End Sub

#End Region

#Region "Entity Selection"

    Friend Sub LaunchEntitySelectionTP(ByRef parentObject As Object, Optional ByRef restrictionToInputsEntities As Boolean = False)

        If CDbl(GlobalVariables.APPS.Version.Replace(".", ",")) > EXCEL_MIN_VERSION Then
            GlobalVariables.EntitySelectionTaskPaneVisible = True
            Me.EntitySelectionTaskPane.Init(parentObject, True)
            Me.EntitySelectionTaskPane.Show()
        Else

        End If

    End Sub

    ' call back from entitySelectionTP for setting up new entity in submission control mode
    Public Sub ValidateEntitySelection(ByRef entityName As String)

        m_currentReportUploadControler.ChangeCurrentEntity(entityName)
        Me.EntitySelectionTaskPane.ClearAndClose()

    End Sub

#End Region

#End Region


#Region "Report Upload Methods"

    ' Create report upload Conctroler and display
    Friend Sub AssociateReportUploadControler(ByRef p_mustUpdateInputs As Boolean)

        GlobalVariables.APPS.Interactive = False
        LoadFinancialDropDownsSubmissionButtons()
        Dim l_reportUploadController As New ReportUploadControler(Me)
        Dim l_excelWorksheet As Excel.Worksheet = GlobalVariables.APPS.ActiveSheet

        If l_reportUploadController.RefreshSnapshot(p_mustUpdateInputs) = True Then
            m_currentReportUploadControler = l_reportUploadController
            m_reportUploadControlersDictionary.Add(l_excelWorksheet, l_reportUploadController)
            m_worksheetNamesObjectDict.Add(l_excelWorksheet.Name, GlobalVariables.APPS.ActiveSheet)
            Select Case l_reportUploadController.GetProcess
                Case Account.AccountProcess.FINANCIAL : DisplayFinancialSubmissionRibbon(l_excelWorksheet)
                Case Account.AccountProcess.RH : DisplayPDCSubmissionRibbon(l_excelWorksheet)
            End Select
        Else
            GlobalVariables.APPS.Interactive = True
            GlobalVariables.APPS.ScreenUpdating = True
            m_currentReportUploadControler = Nothing
        End If

    End Sub

    Private Sub DisplayFinancialSubmissionRibbon(ByRef p_ws As Excel.Worksheet)
        Dim l_item = AddButtonToDropDown(m_submissionWorksheetCombobox, _
                                         p_ws.Name, _
                                         p_ws.Name)
        m_submissionWorksheetCombobox.SelectedItemId = p_ws.Name
        SubmissionModeRibbon.Visible = True
        SubmissionModeRibbon.Activate()
        DisplayReportUploadSidePane()
    End Sub

    Private Sub DisplayPDCSubmissionRibbon(ByRef p_ws As Excel.Worksheet)

        m_PDCSubmissionRibbon.Activate()
        m_PDCSubmissionRibbon.Visible = True
        Dim l_item = AddButtonToDropDown(m_PDCWorksheetDropDown, _
                                         p_ws.Name, _
                                         p_ws.Name)
        m_PDCWorksheetDropDown.SelectedItemId = p_ws.Name

    End Sub

    Friend Shared Sub LoadFinancialDropDownsSubmissionButtons()

        GlobalVariables.ClientsIDDropDown.Items.Clear()
        For Each client_id As Int32 In GlobalVariables.AxisElems.GetDictionary(AxisType.Client).Keys
            If GlobalVariables.AxisElems.GetValue(AxisType.Client, client_id) Is Nothing Then Continue For
            AddButtonToDropDown(GlobalVariables.ClientsIDDropDown, _
                                client_id, _
                                GlobalVariables.AxisElems.GetValueName(AxisType.Client, client_id))
        Next
        GlobalVariables.ClientsIDDropDown.SelectedItemId = AxisType.Client
        GlobalVariables.ClientsIDDropDown.Invalidate()
        GlobalVariables.ClientsIDDropDown.ScreenTip = "screentip"
        '  GlobalVariables.ClientsIDDropDown. = GlobalVariables.Clients.axis_hash(DEFAULT_ANALYSIS_AXIS_ID)(NAME_VARIABLE)

        GlobalVariables.ProductsIDDropDown.Items.Clear()
        For Each product_id As Int32 In GlobalVariables.AxisElems.GetDictionary(AxisType.Product).Keys
            If GlobalVariables.AxisElems.GetValue(AxisType.Product, product_id) Is Nothing Then Continue For
            AddButtonToDropDown(GlobalVariables.ProductsIDDropDown, _
                                product_id, _
                                GlobalVariables.AxisElems.GetValueName(AxisType.Product, product_id))
        Next
        GlobalVariables.ProductsIDDropDown.SelectedItemId = AxisType.Product
        '    GlobalVariables.ProductsIDDropDown.Caption = GlobalVariables.Products.axis_hash(DEFAULT_ANALYSIS_AXIS_ID)(NAME_VARIABLE)

        GlobalVariables.AdjustmentIDDropDown.Items.Clear()
        For Each adjustment_id As Int32 In GlobalVariables.AxisElems.GetDictionary(AxisType.Adjustment).Keys
            If GlobalVariables.AxisElems.GetValue(AxisType.Adjustment, adjustment_id) Is Nothing Then Continue For
            AddButtonToDropDown(GlobalVariables.AdjustmentIDDropDown, _
                                adjustment_id, _
                                GlobalVariables.AxisElems.GetValueName(AxisType.Adjustment, adjustment_id))
        Next
        GlobalVariables.AdjustmentIDDropDown.SelectedItemId = AxisType.Adjustment
        '  GlobalVariables.AdjustmentIDDropDown.Caption = GlobalVariables.Adjustments.axis_hash(DEFAULT_ANALYSIS_AXIS_ID)(NAME_VARIABLE)

    End Sub

    Friend Sub SelectFinancialDropDownSubmissionButtons(ByRef p_clientId As Int32, _
                                                        ByRef p_productId As Int32, _
                                                        ByRef p_adjustmentId As Int32)

        If p_clientId <> 0 Then GlobalVariables.ClientsIDDropDown.SelectedItemId = p_clientId
        If p_productId <> 0 Then GlobalVariables.ProductsIDDropDown.SelectedItemId = p_productId
        If p_adjustmentId <> 0 Then GlobalVariables.AdjustmentIDDropDown.SelectedItemId = p_adjustmentId

    End Sub

    ' Disable Submission Buttons (Call back from Report upload Controller)
    Friend Sub ModifySubmissionControlsStatus(ByRef buttonsStatus As Boolean)

        RefreshInputsBT.Enabled = buttonsStatus
        m_financialSubmissionSubmitButton.Enabled = buttonsStatus
        m_financialSubmissionSatusButton.Enabled = buttonsStatus
        m_financialSubmissionCancelButton.Enabled = buttonsStatus
        m_financialSubmissionAutoCommitButton.Enabled = buttonsStatus
        ShowReportBT.Enabled = buttonsStatus

    End Sub

    Friend Sub ClearSubmissionMode(ByRef p_generalSubmissionController As ReportUploadControler)

        On Error Resume Next
        m_worksheetNamesObjectDict.Remove(p_generalSubmissionController.m_associatedWorksheet.Name)
        For Each l_item In m_submissionWorksheetCombobox.Items
            If l_item.id = p_generalSubmissionController.m_associatedWorksheet.Name Then
                m_submissionWorksheetCombobox.Items.Remove(l_item)
                Exit For
            End If
        Next
        p_generalSubmissionController.CloseInstance()
        m_reportUploadControlersDictionary.Remove(p_generalSubmissionController.m_associatedWorksheet)

        If m_reportUploadControlersDictionary.Count = 0 Then
            SubmissionModeRibbon.Visible = False
            m_PDCSubmissionRibbon.Visible = False
            m_reportUploadControlersDictionary.Clear()
            GlobalVariables.APPS.CellDragAndDrop = True
        Else
            ActivateReportUploadController(m_reportUploadControlersDictionary.ElementAt(0).Value)
            m_submissionWorksheetCombobox.SelectedItemId = m_currentReportUploadControler.m_associatedWorksheet.Name
            m_currentReportUploadControler.m_associatedWorksheet.Activate()
        End If
        GlobalVariables.APPS.Interactive = True
        GlobalVariables.APPS.ScreenUpdating = True

    End Sub

    Friend Sub SetPDCSubmissionRibbonEntityAndAccountName(ByRef p_entityName As String, _
                                                ByRef p_accountName As String)
        m_PDCEntityEditBox.Text = p_entityName
        m_PDCaccountNameEditBox.Text = p_accountName
    End Sub

#End Region


#Region "Utilities"

    ' Add an item to a DropDown Control
    Friend Shared Function AddButtonToDropDown(ByRef DropDownMenu As ADXRibbonDropDown, _
                                                ByVal id As String, _
                                                ByVal name As String) As ADXRibbonItem

        Dim adxRibbonItem As ADXRibbonItem = New AddinExpress.MSO.ADXRibbonItem()
        adxRibbonItem.Caption = name
        adxRibbonItem.Id = id
        adxRibbonItem.ImageTransparentColor = System.Drawing.Color.Transparent
        DropDownMenu.Items.Add(adxRibbonItem)
        Return adxRibbonItem

    End Function

    Friend Function IsCurrentReportUploadController(ByRef p_reportUploadController As ReportUploadControler) As Boolean
        Return p_reportUploadController Is m_currentReportUploadControler
    End Function

    Friend Sub ActivateReportUploadController(ByRef p_reportUploadController As ReportUploadControler)
        m_currentReportUploadControler = p_reportUploadController
        m_submissionWorksheetCombobox.SelectedItemId = p_reportUploadController.m_associatedWorksheet.Name
    End Sub

#End Region


#Region "Interface"

    Public Shared Function DisplayConnectionStatus(ByRef connected As Boolean) As Int32

        On Error Resume Next
        SetMainMenuButtonState(connected)
        If connected = True Then
            GlobalVariables.ConnectionToggleButton.Image = 1
            GlobalVariables.ConnectionToggleButton.Caption = "Connected"
            SetCurrentVersionAfterConnection()
            SetCurrentProcessAfterConnection()
        Else
            GlobalVariables.ConnectionToggleButton.Image = 0
            GlobalVariables.ConnectionToggleButton.Caption = "Not connected"
        End If
        Return 0

    End Function

    Private Shared Sub SetCurrentVersionAfterConnection()

        Dim l_currentVersionId As Int32 = My.Settings.version_id
        If GlobalVariables.Versions.IsVersionValid(l_currentVersionId) = True Then
            SetCurrentVersionId(l_currentVersionId)
        Else
            LaunchVersionSelection()
        End If

    End Sub

    Private Shared Sub SetCurrentProcessAfterConnection()

        Dim l_currentProcessId As CRUD.Account.AccountProcess = My.Settings.processId
        Select Case l_currentProcessId
            Case Account.AccountProcess.FINANCIAL, Account.AccountProcess.RH
                SetCurrentProcessId(l_currentProcessId)
            Case Else
                LaunchProcessSelection
        End Select

    End Sub

    Public Shared Sub SetCurrentVersionId(ByRef p_versionId As UInt32)

        Dim l_version As CRUD.Version = GlobalVariables.Versions.GetValue(p_versionId)
        If l_version Is Nothing Then Exit Sub

        GlobalVariables.VersionButton.Caption = l_version.Name
        GlobalVariables.VersionlabelSubRibbon.Text = l_version.Name
        My.Settings.version_id = p_versionId
        My.Settings.Save()

    End Sub

    Public Shared Sub SetCurrentProcessId(ByRef p_processId As CRUD.Account.AccountProcess)

        Select Case p_processId
            Case Account.AccountProcess.FINANCIAL : GlobalVariables.ProcessButton.Caption = Local.GetValue("process.process_financial")
            Case Account.AccountProcess.RH : GlobalVariables.ProcessButton.Caption = Local.GetValue("process.process_rh")
        End Select
        My.Settings.processId = p_processId
        My.Settings.Save()

    End Sub

    Public Sub InitializePPSBIController()
        m_ppsbiController = New FBIFunctionController
    End Sub

    Public Function IsCurrentGeneralSubmissionController() As Boolean
        If m_currentReportUploadControler Is Nothing Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Function RefreshGeneralSubmissionControllerSnapshot(ByRef p_refreshFromDataBaseFlag As Boolean) As Boolean
        If m_currentReportUploadControler IsNot Nothing Then
            If m_currentReportUploadControler.RefreshSnapshot(p_refreshFromDataBaseFlag) = False Then
                Return False
            Else
                Return True
            End If
        End If
        Return False
    End Function

    Public Function GetPPSBIResult(ByRef p_entity As Object, _
                                    ByRef p_account As Object, _
                                    ByRef p_period As Object, _
                                    ByRef p_currency As Object, _
                                    ByRef p_version As Object, _
                                    ByRef p_clientsFilters As Object, _
                                    ByRef p_productsFilters As Object, _
                                    ByRef p_adjustmentsFilters As Object, _
                                    ByRef p_filtersArray As Object) As Object

        Return GlobalVariables.GlobalPPSBIController.GetDataCallBack(p_entity, _
                                                                    p_account, _
                                                                    p_period, _
                                                                    p_currency,
                                                                    p_version, _
                                                                    p_clientsFilters, _
                                                                    p_productsFilters, _
                                                                    p_adjustmentsFilters, _
                                                                    p_filtersArray)

    End Function

#End Region


   
End Class

