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

    public CUILeftPaneController LeftPaneController { get; set; }
    public CUIRightPaneController RightPaneController { get; set; }
    public ResultController ResultController { get; set; }
    public CUIVisualizationController VisualizationController { get; set; }

    public IView View { get { return (m_view); } }
    public string Error { get; set; }

    #endregion

    #region Initialize

    public CUIController()
    {
      this.m_view = new ControllingUI_2();
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

    public bool Compute()
    {
      if (LeftPaneController.GetVersions() == null)
      {
        Error = Local.GetValue("CUI.error.no_version_selected");
        return (false);
      }

      ComputeConfig l_config = new ComputeConfig();
      LegacyComputeRequest l_request = new LegacyComputeRequest();
      Version l_version = VersionModel.Instance.GetValue(LeftPaneController.GetVersions()[0]);

      l_config.BaseTimeConfig = l_version.TimeConfiguration;
      l_request.Process = (Account.AccountProcess)Settings.Default.processId;
      l_request.StartPeriod = (l_request.Process == Account.AccountProcess.RH) ? 
        LeftPaneController.GetStartPeriod() : (int)l_version.StartPeriod;
      l_request.NbPeriods = (l_request.Process == Account.AccountProcess.RH) ? 
        LeftPaneController.GetNbPeriod() : (int)l_version.NbPeriod;
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
      l_config.Rows = RightPaneController.GetRows();
      l_config.Columns = RightPaneController.GetColumns();
      l_config.Request = l_request;

      if (CheckConfig(l_config) == false)
        return (false);
      ResultController.LoadDGV(l_config);
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
      VisualizationController = new CUIVisualizationController();
    }
  }
}
