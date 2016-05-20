using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using View;
  using Model.CRUD;
  using Model;
  using Network;
  using Properties;
  using Utils;

  class CUIController : IController
  {
    #region Variables

    private ControllingUI_2 m_view;
    PeriodsComparisonSelectionView m_periodDiffView;

    public CUILeftPaneController LeftPaneController { get; set; }
    public CUIRightPaneController RightPaneController { get; set; }
    public ResultController ResultController { get; set; }
    public CUIVisualizationController VisualizationController { get; set; }
    public SafeDictionary<UInt32, ComputeResult> LastResult { get; set; }
    public ComputeConfig LastConfig { get; set; }
    public SafeDictionary<TimeConfig, SafeDictionary<Int32, Int32>> PeriodDiffAssociations { get; set; }
    public bool PeriodDiff { get; set; }

    public IView View { get { return (m_view); } }
    public string Error { get; set; }

    private List<UInt32> m_openedPanels = new List<UInt32>();

    #endregion

    #region Initialize

    public CUIController()
    {
      PeriodDiff = false;
      this.m_view = new ControllingUI_2();
      m_periodDiffView = new PeriodsComparisonSelectionView();
      m_periodDiffView.SetController(this);
      this.m_view.SetController(this);
      this.LoadView();
    }

    private void LoadView()
    {
      this.m_view.LoadView();
    }

    #endregion

    #region Pane

    public void CreatePane()
    {
      LeftPaneController = new CUILeftPaneController(this);
      RightPaneController = new CUIRightPaneController(this);
      ResultController = new ResultController(this);
    }

    #endregion

    public bool AddPanel(UInt32 p_panelId)
    {
      if (m_openedPanels.Contains(p_panelId))
        return (false);
      m_openedPanels.Add(p_panelId);
      return (true);
    }

    public void RemovePanel(UInt32 p_panelid)
    {
      m_openedPanels.Remove(p_panelid);
    }

    public void DisplayVersionComparaison()
    {
      if (m_view.VersionComparaison)
        ResultController.DisplayVersionComparaison();
      else
        ResultController.HideVersionComparaison();
    }

    Version SelectVersion(List<UInt32> p_versionList)
    {
      Version l_selectedVersion = null;

      foreach (UInt32 l_versionId in p_versionList)
      {
        Version l_version = VersionModel.Instance.GetValue(l_versionId);

        if (l_selectedVersion == null || l_version.StartPeriod < l_selectedVersion.StartPeriod)
          l_selectedVersion = l_version;
      }
      return (l_selectedVersion);
    }

    public bool Compute()
    {
      if (LeftPaneController.GetVersions() == null)
      {
        Error = Local.GetValue("CUI.error.no_version_selected");
        return (false);
      }

      ComputeConfig l_config = new ComputeConfig();
      LegacyComputeRequest l_request = new LegacyComputeRequest();
      Version l_version = SelectVersion(LeftPaneController.GetVersions());

      l_config.BaseTimeConfig = l_version.TimeConfiguration;
      if (Addin.Process == Account.AccountProcess.FINANCIAL)
       l_config.Periods = LeftPaneController.GetPeriods();
      l_request.Process = (Account.AccountProcess)Addin.Process;
      if (l_request.Process == Account.AccountProcess.RH)
      {
        l_request.StartPeriod = LeftPaneController.GetStartPeriod();
        l_request.NbPeriods = LeftPaneController.GetNbPeriod(); 
      }
      else
      {
        l_request.StartPeriod = (Int32)l_version.StartPeriod;
        l_request.NbPeriods = l_version.NbPeriod;
      }
      l_request.Periods = LeftPaneController.GetPeriods();
      l_request.PeriodFilter = l_request.Periods != null;
      l_request.Versions = LeftPaneController.GetVersions();
      l_request.CurrencyId = LeftPaneController.GetCurrency();
      l_request.SortList = RightPaneController.GetSort();
      l_request.FilterList = LeftPaneController.GetFilters();
      l_request.AxisElemList = LeftPaneController.GetAxisElems();
      l_request.EntityId = LeftPaneController.GetEntity();
      l_request.GlobalFactVersionId = l_version.GlobalFactVersionId;
      l_request.RateVersionId = l_version.RateVersionId;
      l_request.AxisHierarchy = true;
      l_request.IsDiff = (l_request.Versions.Count == 2);
      l_request.IsPeriodDiff = PeriodDiff;
      l_request.PeriodDiffAssociations = PeriodDiffAssociations;
      l_config.Rows = RightPaneController.GetRows();
      l_config.Columns = RightPaneController.GetColumns();
      l_config.Request = l_request;
      
      if (CheckConfig(l_config) == false)
        return (false);
      LastConfig = l_config;
      ResultController.LoadDGV(l_config);
      DisplayVersionComparaison();
      if (l_request.IsDiff)
      {
        if (LegacyComputeModel.Instance.ComputeDiff(l_request))
          return (true);
      }
      else
        if (LegacyComputeModel.Instance.Compute(l_request))
          return (true);
      Error = Local.GetValue("general.error.system");
      return (false);
    }

    bool CheckRequest(AComputeRequest p_request)
    {
      if (p_request.NbPeriods <= 1)
      {
        Error = Local.GetValue("CUI.error.period_range");
        return (false);
      }
      return (true);
    }

    bool CheckModelType(CUIDimensionConf p_conf, Type p_type)
    {
      if (p_conf == null)
        return (false);
      if (p_conf.ModelType == p_type)
        return (true);
      if (p_conf.Child != null)
        return (CheckModelType(p_conf.Child, p_type));
      return (false);
    }

    bool CheckValidVersion(ComputeConfig p_config)
    {
      if (p_config.Request.Versions.Count > 1 && 
        !CheckModelType(p_config.Columns, typeof(VersionModel)) &&
        !CheckModelType(p_config.Rows, typeof(VersionModel)))
      {
        Error = Local.GetValue("CUI.error.multiversion_no_sort");
        return (false);
      }
      foreach (UInt32 l_versionId in p_config.Request.Versions)
      {
        Version l_version = VersionModel.Instance.GetValue(l_versionId);

        if (l_version == null)
        {
          Error = Local.GetValue("CUI.error.unknown_version");
          return (false);
        }
        if (p_config.Request.Process != l_version.Process)
        {
          Error = Local.GetValue("CUI.error.version_process_invalid");
          return (false);
        }
      }
      return (true);
    }

    bool CheckConfig(ComputeConfig p_config)
    {
      if (CheckRequest(p_config.Request) == false)
        return (false);
      if (CheckValidVersion(p_config) == false)
        return (false);
      if (!CheckModelType(p_config.Rows, typeof(PeriodModel)) && !CheckModelType(p_config.Columns, typeof(PeriodModel)))
      {
        Error = Local.GetValue("CUI.error.no_period_sort");
        return (false);
      }
      return (true);
    }

    public void ShowCharts()
    {
      VisualizationController = new CUIVisualizationController(this);
    }

    public void ShowPeriodDiff()
    {
      List<UInt32> l_versionList = LeftPaneController.GetVersions();
      Version l_versionA;
      Version l_versionB;

      if (l_versionList == null || l_versionList.Count < 2)
        return;
      l_versionA = VersionModel.Instance.GetValue(l_versionList[0]);
      l_versionB = VersionModel.Instance.GetValue(l_versionList[1]);
      if (l_versionA == null || l_versionB == null)
        return;
      m_periodDiffView.LoadView(l_versionA, l_versionB);
      m_periodDiffView.ShowDialog();
    }
  }
}
