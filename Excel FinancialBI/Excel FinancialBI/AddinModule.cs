﻿using System;
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

  [GuidAttribute("D046D807-38A0-47AF-AB7B-71AA24A67FB9"), ProgId("ExcelFinancialBI.AddinModule")]
  public partial class AddinModule : AddinExpress.MSO.ADXAddinModule
  {
    public AddinModule()
    {
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
      Addin.Main();
      SubmissionModeRibbon.Visible = false;
      fbiRibbonChangeState(false);
      SuscribeEvents();
      MultilanguageSetup();
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
    }

    void SuscribeEvents()
    {
      Addin.InitializationEvent += OnAddinInitializationEvent;
      Addin.ConnectionStateEvent += OnConnectionEvent;
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

    public ReportUploadEntitySelectionSidePane ReportUploadEntitySelectionSidePane
    {
      get
      {
        if (ReportUploadEntitySelectionSidePaneItem.TaskPaneInstance != null)
          return (ReportUploadEntitySelectionSidePaneItem.TaskPaneInstance as ReportUploadEntitySelectionSidePane);
        return (ReportUploadEntitySelectionSidePaneItem.CreateTaskPaneInstance() as ReportUploadEntitySelectionSidePane);
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
      Addin.SetCurrentProcessId((int)Account.AccountProcess.FINANCIAL);
    }

    private void m_RHProcessRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {
      Addin.SetCurrentProcessId((int)Account.AccountProcess.RH);
    }

    private void m_snapshotRibbonSplitButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {
        Version l_version = VersionModel.Instance.GetValue(FBI.Properties.Settings.Default.version_id);
        if (l_version == null)
        {
            MessageBox.Show("versions.select_version");
            return;
        }
        Excel.Worksheet l_worksheet = this.ExcelApp.ActiveSheet as Excel.Worksheet;   
        Account.AccountProcess l_process = (Account.AccountProcess)FBI.Properties.Settings.Default.processId;
        if (l_process == Account.AccountProcess.FINANCIAL)
        {
            FactsEditionController l_factsEditionController = new FactsEditionController(l_process, l_version, l_worksheet);
        }
        else
        {
            // TO DO : launch snapshot RH selection Controller (to be implemented)
        }
    }

    private void m_directoryRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {

    }

    private void m_reportUploadRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {
      Account.AccountProcess l_process = (Account.AccountProcess)FBI.Properties.Settings.Default.processId;
      ReportUploadEntitySelectionSidePane.LoadView(l_process);
      ReportUploadEntitySelectionSidePane.Show();
    }

    private void m_CUIRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {
      new CUIController();
    }

    private void m_submissionsTrackingRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {
      new CommitFollowUpController();
    }

    private void m_fbiRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {

    }

    private void m_fbiBreakLinksRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {

    }

    private void m_refreshRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {

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
      // if connected
      new PlatformMgtController();
    }

    private void m_settingsRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {
      
    }

    #endregion

    #region Financial facts edition

    private void m_financialSubmissionSubmitButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {
        // TO DO raise event or call method in owned facts edition controller

    }

    private void m_financialSubmissionAutoCommitButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {

    }

    private void CloseBT_OnClick(object sender, IRibbonControl control, bool pressed)
    {

    }

    #endregion

    void OnAddinInitializationEvent()
    {
      fbiRibbonChangeState(true);
    }

    private void OnConnectionEvent(bool p_connected)
    {
      if (p_connected == false)
        fbiRibbonChangeState(false);
    }

    #endregion

    public void SetProcessCaption(string p_process)
    {
      m_processRibbonButton.Caption = p_process;
    }


  }
}

