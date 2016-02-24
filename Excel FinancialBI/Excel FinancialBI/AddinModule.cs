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
    private AddinModuleController m_controller;

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
      Addin.Main();
      m_controller = new AddinModuleController(this);
      m_financialSubmissionRibbon.Visible = false;
      m_RHSubmissionRibbon.Visible = false;
      fbiRibbonChangeState(false);
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

    public ReportUploadSidePane ReportUploadEntitySelectionSidePane
    {
      get
      {
        if (ReportUploadEntitySelectionSidePaneItem.TaskPaneInstance != null)
          return (ReportUploadEntitySelectionSidePaneItem.TaskPaneInstance as ReportUploadSidePane);
        return (ReportUploadEntitySelectionSidePaneItem.CreateTaskPaneInstance() as ReportUploadSidePane);
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
      Account.AccountProcess l_process = (Account.AccountProcess)FBI.Properties.Settings.Default.processId;
      if (l_process == Account.AccountProcess.FINANCIAL)
      {
        if (m_controller.LaunchSnapshot(l_process, false) == false)
          MessageBox.Show(m_controller.Error);
      }
      else
      {
        if (m_controller.LaunchRHSnapshotView() == false)
          MessageBox.Show(m_controller.Error);
      }
    }

    private void m_directoryRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {

    }

    private void m_reportUploadRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {
      // Passer par le controller !!!!!!!!! TO DO 

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

    private void m_PDCSubmissionButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {

    }

    private void m_PDCAutocommitButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {

    }

    private void m_PDCSUbmissionStatusButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {

    }

    private void m_PDCSubmissionCancelButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {

    }

    private void m_PDCSumbissionExitButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {

    }

    private void m_PDCRefreshSnapthshotButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {

    }

    private void m_PDCConsultantRangeEditButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {

    }

    private void m_PDCPeriodsRangeEditButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {

    }


  }
}

