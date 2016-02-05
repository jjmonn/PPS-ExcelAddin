using System;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows.Forms;
using AddinExpress.MSO;
using Excel = Microsoft.Office.Interop.Excel;

namespace FBI
{
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

    public MVC.View.ConnectionSidePane ConnectionSidePane
    {
      get
      {
        AddinExpress.XL.ADXExcelTaskPane l_taskPaneInstance = null;
        l_taskPaneInstance = (AddinExpress.XL.ADXExcelTaskPane)ConnectionSidePaneItem.TaskPaneInstance;
        if (l_taskPaneInstance == null)
        {
          l_taskPaneInstance = (MVC.View.ConnectionSidePane)ConnectionSidePaneItem.CreateTaskPaneInstance();
        }

        return l_taskPaneInstance as MVC.View.ConnectionSidePane;
      }
    }

    public MVC.View.VersionSelectionPane VersionSelectionSidePane
    {
      get
      {
        AddinExpress.XL.ADXExcelTaskPane l_taskPaneInstance = null;
        l_taskPaneInstance = (AddinExpress.XL.ADXExcelTaskPane)VersionSelectionSidePaneItem.TaskPaneInstance;
        if (l_taskPaneInstance == null)
        {
          l_taskPaneInstance = (MVC.View.VersionSelectionPane)VersionSelectionSidePaneItem.CreateTaskPaneInstance();
        }

        return l_taskPaneInstance as MVC.View.VersionSelectionPane;
      }
    }

    public MVC.View.ReportUploadEntitySelectionSidePane ReportUploadEntitySelectionSidePane
    {
      get
      {
        AddinExpress.XL.ADXExcelTaskPane l_taskPaneInstance = null;
        l_taskPaneInstance = (AddinExpress.XL.ADXExcelTaskPane)ReportUploadEntitySelectionSidePaneItem.TaskPaneInstance;
        if (l_taskPaneInstance == null)
        {
          l_taskPaneInstance = (MVC.View.ReportUploadEntitySelectionSidePane)ReportUploadEntitySelectionSidePaneItem.CreateTaskPaneInstance();
        }
        return l_taskPaneInstance as MVC.View.ReportUploadEntitySelectionSidePane;
      }
    }

    public MVC.View.ReportUploadAccountInfoSidePane ReportUploadAccountInfoSidePane
    {
      get
      {
        AddinExpress.XL.ADXExcelTaskPane l_taskPaneInstance = null;
        l_taskPaneInstance = (AddinExpress.XL.ADXExcelTaskPane)ReportUploadAccountInfoSidePaneItem.TaskPaneInstance;
        if (l_taskPaneInstance == null)
        {
          l_taskPaneInstance = (MVC.View.ReportUploadAccountInfoSidePane)ReportUploadAccountInfoSidePaneItem.CreateTaskPaneInstance();
        }
        return l_taskPaneInstance as MVC.View.ReportUploadAccountInfoSidePane;
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
        ConnectionSidePane.m_showned = true;
        ConnectionSidePane.Show();
      }

    }

    private void m_versionRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {
      VersionSelectionSidePane.m_showned = true;
      VersionSelectionSidePane.Show();
    }

    private void m_financialProcessRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {
      m_processRibbonButton.Caption = FBI.Utils.Local.GetValue("process.process_financial");
      Addin.SetCurrentProcessId(FBI.MVC.Model.CRUD.Account.AccountProcess.FINANCIAL);
    }

    private void m_RHProcessRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {
      m_processRibbonButton.Caption = FBI.Utils.Local.GetValue("process.process_rh");
      Addin.SetCurrentProcessId(FBI.MVC.Model.CRUD.Account.AccountProcess.RH);
    }

    private void m_snapshotRibbonSplitButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {

    }

    private void m_directoryRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {

    }

    private void m_reportUploadRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {

    }

    private void m_CUIRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {

    }

    private void m_submissionsTrackingRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {

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
      FBI.MVC.View.PlatformMGTGeneralUI l_platformMgtUI = new FBI.MVC.View.PlatformMGTGeneralUI();
      l_platformMgtUI.Show();

    }

    private void m_settingsRibbonButton_OnClick(object sender, IRibbonControl control, bool pressed)
    {

    }

    #endregion



    #endregion

  }
}

