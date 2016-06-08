using System;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows.Forms;
using AddinExpress.MSO;
using Excel = Microsoft.Office.Interop.Excel;
using AddinExpress.XL;

namespace FBI
{
  using MVC.View;
  using MVC.Model;
  using MVC.Model.CRUD;
  using MVC.Controller;
  using Utils;

  [GuidAttribute("D046D807-38A0-47AF-AB7B-71AA24A67FB9"), ProgId("FBI.AddinModule")]
  public partial class AddinModule : AddinExpress.MSO.ADXAddinModule
  {
    private AddinModuleController m_controller;
    public FBIFunctionExcelController FBIFunctionController { private set; get; }
    public ExcelWorksheetEvents WorksheetEvents { private set; get; }

    public AddinModule()
    {
      VIBlend.Utilities.Licensing.LicenseContent = "0g0g635756920818516203geeae134609ccc047560baf100768868d8abc|P20sP8MuxE14HfXpiO3X3PueOBIpfMynKjd9/ifkWaAwYrH/u0NNk7rqh77rOQs3OmrTem7ghz4hnkVM+Bdu9Fzt6u6rne35u/o5JPTC00BzjTUNUXP7f/xplMNliHELbbHfixl1O/3/E6uDNHDVKJc6sRHHTOYPBnao09omX4s=";
      Application.EnableVisualStyles();
      InitializeComponent();
      // Please add any initialization code to the AddinInitialize event handler
      if (IsNetworkDeployed())
      {
        CheckForUpdates();
        Properties.Settings.Default.Upgrade();
      }
    }

    private void AddinModule_AddinInitialize(object sender, EventArgs e)
    {
      FBIFunctionController = new FBIFunctionExcelController();
      Addin.HostApplication = HostApplication;
      Addin.AddinModule = this;
      Addin.Main();
      m_controller = new AddinModuleController(this);

      m_financialSubmissionRibbon.Visible = false;
      m_RHSubmissionRibbon.Visible = false;
      fbiRibbonChangeState(false);
      SuscribeEvents();
      MultilanguageSetup();
      Addin.Process = (Account.AccountProcess)Properties.Settings.Default.processId;
    }

    private void AddinModule_AddinStartupComplete(object sender, EventArgs e)
    {
      WorksheetEvents = new ExcelWorksheetEvents(this);
    }

    private void MultilanguageSetup()
    {
      m_connectionGroup.Caption = Local.GetValue("connection.connection");
      m_connectionButton.Caption = Local.GetValue("connection.connection");
      m_versionGroup.Caption = Local.GetValue("general.version");
      m_versionRibbonButton.Caption = Local.GetValue("general.select_version");
      m_processRibbonButton.Caption = Local.GetValue("general.select_process");
      m_uploadGroup.Caption = Local.GetValue("general.upload");
      m_snapshotRibbonSplitButton.Caption = Local.GetValue("general.snapshot");
      m_reportUploadRibbonButton.Caption = Local.GetValue("general.edition");
      m_submissionsTrackingRibbonButton.Caption = Local.GetValue("submissionsFollowUp.submissions_tracking");
      m_CUIRibbonButton.Caption = Local.GetValue("general.data_visualization");
      m_refreshRibbonButton.Caption = Local.GetValue("general.refresh");
      m_platformManagementButton.Caption = Local.GetValue("general.platform_management");
      m_visualizationGroup.Caption = Local.GetValue("general.visualization");
      m_settingsRibbonButton.Caption = Local.GetValue("general.settings");
      m_configurationGroup.Caption = Local.GetValue("general.configuration");
      m_accountSnapshotBT.Caption = Local.GetValue("general.account_snapshot");
      m_reportAccount.Caption = Local.GetValue("general.report_account");
    }

    void SuscribeEvents()
    {
      Addin.InitializationEvent += OnAddinInitializationEvent;
      Addin.ConnectionStateEvent += OnConnectionEvent;

      m_connectionButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(m_connectionButton_OnClick);
      m_versionRibbonButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(m_versionRibbonButton_OnClick);
      m_RHProcessRibbonButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(m_RHProcessRibbonButton_OnClick);
      m_snapshotRibbonSplitButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(m_snapshotRibbonSplitButton_OnClick);
      m_reportUploadRibbonButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(m_reportUploadRibbonButton_OnClick);
      m_CUIRibbonButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(m_CUIRibbonButton_OnClick);
      m_fbiRibbonButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(OnFBIFunctionButtonClick);
      m_refreshRibbonButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(m_refreshRibbonButton_OnClick);
      m_resfreshSelectionRibbonButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(m_resfreshSelectionRibbonButton_OnClick);
      m_refreshWorksheetRibbonButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(m_refreshWorksheetRibbonButton_OnClick);
      m_refreshWorkbookRibbonButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(m_refreshWorkbookRibbonButton_OnClick);
      m_platformManagementButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(m_platformManagementButton_OnClick);
      m_settingsRibbonButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(m_settingsRibbonButton_OnClick);
      m_financialSubmissionSubmitButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(m_financialSubmissionSubmitButton_OnClick);
      m_financialSubmissionAutoCommitButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(m_financialSubmissionAutoCommitButton_OnClick);
      CloseBT.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(CloseBT_OnClick);
      m_PDCSubmissionButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(m_PDCSubmissionButton_OnClick);
      m_PDCAutocommitButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(m_PDCAutocommitButton_OnClick);
      m_PDCSUbmissionStatusButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(m_PDCSUbmissionStatusButton_OnClick);
      m_PDCRefreshSnapthshotButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(m_PDCRefreshSnapthshotButton_OnClick);
      m_PDCSumbissionExitButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(m_PDCSumbissionExitButton_OnClick);
      m_directoryRibbonButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(m_directoryRibbonButton_OnClick);
      m_submissionsTrackingRibbonButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(m_submissionsTrackingRibbonButton_OnClick);
      m_fbiBreakLinksRibbonButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(m_fbiBreakLinksRibbonButton_OnClick);
      m_autoRefreshRibbonChackBox.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(m_autoRefreshRibbonChackBox_OnClick);
      m_financialProcessRibbonButton.OnClick += new AddinExpress.MSO.ADXRibbonOnAction_EventHandler(m_financialProcessRibbonButton_OnClick);
    }

    private void fbiRibbonChangeState(bool p_enabled)
    {
      m_versionRibbonButton.Enabled = p_enabled;
      m_processRibbonButton.Enabled = p_enabled;
      m_snapshotRibbonSplitButton.Enabled = p_enabled;
      m_reportUploadRibbonButton.Enabled = p_enabled;
      m_CUIRibbonButton.Enabled = p_enabled;
      m_submissionsTrackingRibbonButton.Enabled = p_enabled;
      m_fbiRibbonButton.Enabled = p_enabled;
      m_refreshRibbonButton.Enabled = p_enabled;
      m_platformManagementButton.Enabled = p_enabled;

      if (p_enabled == false)
          m_connectionButton.Image = 0;
      else
          m_connectionButton.Image = 1;
    }

    #region Add-in Express automatic code

    // Required by Add-in Express - do not modify
    // the methods within this region

    public override System.ComponentModel.IContainer GetContainer()
    {
      if (components == null)
        components = new System.ComponentModel.Container();
      return components;
    }

    [ComRegisterFunctionAttribute]
    public static void AddinRegister(Type t)
    {
      AddinExpress.MSO.ADXAddinModule.ADXRegister(t);
    }

    [ComUnregisterFunctionAttribute]
    public static void AddinUnregister(Type t)
    {
      AddinExpress.MSO.ADXAddinModule.ADXUnregister(t);
    }

    public override void UninstallControls()
    {
      base.UninstallControls();
    }

    #endregion

    #region Accessors

    public static new AddinModule CurrentInstance
    {
      get { return AddinExpress.MSO.ADXAddinModule.CurrentInstance as AddinModule; }
    }

    public Excel._Application ExcelApp
    {
      get { return (HostApplication as Excel._Application); }
    }

    #region Side panes accessors

    public ConnectionSidePane ConnectionSidePane
    {
      get
      {
        if (ConnectionSidePaneItem.TaskPaneInstance != null)
          return (ConnectionSidePaneItem.TaskPaneInstance as ConnectionSidePane);
        return (ConnectionSidePaneItem.CreateTaskPaneInstance() as ConnectionSidePane);
      }
    }

    public VersionSelectionPane VersionSelectionSidePane
    {
      get
      {
        if (VersionSelectionSidePaneItem.TaskPaneInstance != null)
          return (VersionSelectionSidePaneItem.TaskPaneInstance as VersionSelectionPane);
        return (VersionSelectionSidePaneItem.CreateTaskPaneInstance() as VersionSelectionPane);
      }
    }

    public ReportEditionSidePane ReportUploadEntitySelectionSidePane
    {
      get
      {
        if (ReportUploadEntitySelectionSidePaneItem.TaskPaneInstance != null)
          return (ReportUploadEntitySelectionSidePaneItem.TaskPaneInstance as ReportEditionSidePane);
        return (ReportUploadEntitySelectionSidePaneItem.CreateTaskPaneInstance() as ReportEditionSidePane);
      }
    }

    public ReportUploadAccountInfoSidePane ReportUploadAccountInfoSidePane
    {
      get
      {
        if (ReportUploadAccountInfoSidePaneItem.TaskPaneInstance != null)
          return (ReportUploadAccountInfoSidePaneItem.TaskPaneInstance as ReportUploadAccountInfoSidePane);
        return (ReportUploadAccountInfoSidePaneItem.CreateTaskPaneInstance() as ReportUploadAccountInfoSidePane);
      }
    }

    #endregion

    #endregion

    #region Instance variables

    private const Double EXCEL_MIN_VERSION = 9;

    #endregion

    #region Call backs

    #region Main Ribbon

    private void m_connectionButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {
      if (Convert.ToDouble(ExcelApp.Version.Replace(".", ",")) > EXCEL_MIN_VERSION)
      {
        ConnectionSidePane.m_shown = true;
        ConnectionSidePane.Show();
      }
    }

    private void m_versionRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {
      VersionSelectionSidePane.m_shown = true;
      VersionSelectionSidePane.Show();
    }

    private void m_financialProcessRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {
      Addin.Process = Account.AccountProcess.FINANCIAL;
    }

    private void m_RHProcessRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {
      Addin.Process = Account.AccountProcess.RH;
    }

    private void m_snapshotRibbonSplitButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {
      Account.AccountProcess l_process = Addin.Process;
      Version l_version = m_controller.GetCurrentVersion();

      if (l_version != null)
      {
        SubmissionVersionName = l_version.Name;
        if (l_process == Account.AccountProcess.FINANCIAL)
        {
          if (m_controller.LaunchFinancialSnapshot(true, false, l_version.Id) == false)
            MessageBox.Show(m_controller.Error);
        }
        else
        {
          if (m_controller.LaunchRHSnapshotView() == false)
            MessageBox.Show(m_controller.Error);
        }
      }
    }

    private void m_directoryRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {

    }

    private void m_reportUploadRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {
      m_controller.LaunchReportEdition();
    }

    private void m_CUIRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {
      new CUIController();
    }

    private void m_submissionsTrackingRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {
      new CommitFollowUpController();
    }

    private void OnFBIFunctionButtonClick(object sender, IRibbonControl control, bool pressed)
    {
      new FBIFunctionViewController();
    }

    private void m_fbiBreakLinksRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {

    }

    private void m_refreshRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {
      FBIFunctionController.Refresh();
    }

    private void m_resfreshSelectionRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {

    }

    private void m_refreshWorksheetRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {

    }

    private void m_refreshWorkbookRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {

    }

    private void m_autoRefreshRibbonChackBox_OnClick(object sender, IRibbonControl control, bool pressed)
    {

    }

    private void m_platformManagementButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {
      new PlatformMgtController();
    }

    private void m_settingsRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {
      new SettingsController();
    }

    private void m_duplicatesFinderButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {
      DuplicatesUIDemo l_duplicatesUI = new DuplicatesUIDemo();
      l_duplicatesUI.Show();
    }

    #endregion

    #region Financial facts edition

    private void m_financialSubmissionSubmitButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {
        // TO DO raise event or call method in owned facts edition controller
      m_controller.FactsSubmission();
    }

    private void m_financialSubmissionAutoCommitButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {
      m_controller.SetUploadAutoCommit(pressed);
    }

    private void CloseBT_OnClick(object sender, IRibbonControl control, bool pressed)
    {
      m_controller.CloseEditionMode();
    }

    private void RefreshInputsBT_OnClick(object sender, IRibbonControl control, bool pressed)
    {
      m_controller.RefreshInputs();
    }

    #endregion

    #region RH facts edition

    private void m_PDCSubmissionButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {
      m_controller.FactsSubmission();
    }

    private void m_PDCAutocommitButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {
      m_controller.SetUploadAutoCommit(pressed);
    }

    private void m_PDCSUbmissionStatusButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {

    }

    private void m_PDCSubmissionCancelButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {

    }

    private void m_PDCSumbissionExitButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {
      m_controller.CloseEditionMode();
    }

    private void m_PDCRefreshSnapthshotButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {
      m_controller.RefreshInputs();
    }

    private void m_PDCConsultantRangeEditButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {

    }

    private void m_PDCPeriodsRangeEditButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {

    }

    #endregion

    void OnAddinInitializationEvent()
    {
      ClientsDropDown.OnAction += OnAxisSelectionChanged;
      ProductsDropDown.OnAction += OnAxisSelectionChanged;
      AdjustmentDropDown.OnAction += OnAxisSelectionChanged;
      fbiRibbonChangeState(true);
      SetFinancialRibbonState(true);
      SetRHRibbonState(true);
      Addin.VersionId = Properties.Settings.Default.version_id;
    }

    void OnAxisSelectionChanged(object p_sender, IRibbonControl p_control, string p_selectedId, int p_selectedIndex)
    {
      DialogResult l_result = MessageBox.Show(Local.GetValue("upload.change_axis_elem"), "", MessageBoxButtons.YesNo);

      m_controller.ReloadReportUpload(l_result == DialogResult.No);
    }

    private void OnConnectionEvent(bool p_connected)
    {
      if (p_connected == false)
      {
        fbiRibbonChangeState(p_connected);
        SetFinancialRibbonState(p_connected);
        SetRHRibbonState(p_connected);
      }
    }

    #endregion

    #region Financial Submission Ribon

    public ADXRibbonItem AddButtonToDropDown(ADXRibbonDropDown p_menu, UInt32 p_id, string p_name)
    {
      ADXRibbonItem l_item = new ADXRibbonItem();

      l_item.Caption = p_name;
      l_item.Id = p_id.ToString();
      l_item.ImageTransparentColor = System.Drawing.Color.Transparent;
      p_menu.Items.Add(l_item);
      return (l_item);
    }

    public void SetFinancialRibbonState(bool p_state)
    {
      foreach (AddinExpress.MSO.ADXRibbonGroup l_control in m_financialSubmissionRibbon.Controls)
        foreach (AddinExpress.MSO.ADXRibbonCustomControl l_button in l_control.Controls)
          if (l_button.AsRibbonButton != null)
            l_button.AsRibbonButton.Enabled = p_state;
      CloseBT.Enabled = true;
    }

    public void SetRHRibbonState(bool p_state)
    {
      foreach (AddinExpress.MSO.ADXRibbonGroup l_control in m_RHSubmissionRibbon.Controls)
        foreach (AddinExpress.MSO.ADXRibbonCustomControl l_button in l_control.Controls)
          if (l_button.AsRibbonButton != null)
            l_button.AsRibbonButton.Enabled = p_state;
      m_PDCSumbissionExitButton.Enabled = true;
    }

    public void InitFinancialSubmissionRibon()
    {
      ClientsDropDown.Items.Clear();
      ProductsDropDown.Items.Clear();
      AdjustmentDropDown.Items.Clear();
      foreach (AxisElem l_client in AxisElemModel.Instance.GetDictionary(AxisType.Client).Values)
        AddButtonToDropDown(ClientsDropDown, l_client.Id, l_client.Name);
      foreach (AxisElem l_product in AxisElemModel.Instance.GetDictionary(AxisType.Product).Values)
        AddButtonToDropDown(ProductsDropDown, l_product.Id, l_product.Name);
      foreach (AxisElem l_adjustment in AxisElemModel.Instance.GetDictionary(AxisType.Adjustment).Values)
        AddButtonToDropDown(AdjustmentDropDown, l_adjustment.Id, l_adjustment.Name);
      SubmissionAdjustmentId = (UInt32)AxisType.Adjustment;
      SubmissionClientId = (UInt32)AxisType.Client;
      SubmissionProductId = (UInt32)AxisType.Product;
    }

    public string SubmissionVersionName
    {
      set
      {
        if (Addin.Process == Account.AccountProcess.RH)
          m_PDCVersionEditBox.Text = value;
        else
          VersionTBSubRibbon.Text = value;
      }
    }

    public string SubmissionEntityName
    {
      set
      {
        EntityTB.Text = value;
      }
    }

    public string SubmissionCurrencyName
    {
      set
      {
        EntCurrTB.Text = value;
      }
    }

    public string SubmissionCurrency
    {
      set
      {
        EntCurrTB.Text = value;
      }
    }

    public UInt32 SubmissionClientId
    {
      set
      {
        ClientsDropDown.SelectedItemId = value.ToString();
      }
      get
      {
        return (UInt32.Parse(ClientsDropDown.SelectedItemId));
      }
    }

    public UInt32 SubmissionProductId
    {
      set
      {
        ProductsDropDown.SelectedItemId = value.ToString();
      }
      get
      {
        return (UInt32.Parse(ProductsDropDown.SelectedItemId));
      }
    }

    public UInt32 SubmissionAdjustmentId
    {
      set
      {
        AdjustmentDropDown.SelectedItemId = value.ToString();
      }
      get
      {
        return (UInt32.Parse(AdjustmentDropDown.SelectedItemId));
      }
    }

    #endregion

    private void m_financialSubmissionSatusButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {
      m_controller.ShowStatusView();
    }

    private void m_accountSnapshot_OnClick(object sender, IRibbonControl control, bool pressed)
    {
      AccountSnapshotController l_snapshot = new AccountSnapshotController(ExcelApp.ActiveSheet as Microsoft.Office.Interop.Excel.Worksheet);

      if (l_snapshot.LaunchSnapshot() == false)
        MessageBox.Show(l_snapshot.Error);
      l_snapshot.Close();
    }

    private void m_reportAccount_OnClick(object sender, IRibbonControl control, bool pressed)
    {
      AccountSnapshotController l_snapshot = new AccountSnapshotController(ExcelApp.ActiveSheet as Microsoft.Office.Interop.Excel.Worksheet);

      if (l_snapshot.CreateReport() == false)
        MessageBox.Show(l_snapshot.Error);
      l_snapshot.Close();
    }
  }
}

