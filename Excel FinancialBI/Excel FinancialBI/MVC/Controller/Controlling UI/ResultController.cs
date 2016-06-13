using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using View;
  using Model.CRUD;
  using Utils;

  class ResultController : IController
  {
    CUIController m_parentController;
    ResultView m_view;
    public string Error { get; set; }
    public IView View { get { return (m_view); } }
    public ComputeConfig Config { get; set; }
    UInt32 m_displayedVersionCompare = 0;
    LogController m_logController;

    public ResultController(CUIController p_controller)
    {
      Config = null;
      m_logController = new LogController();
      m_parentController = p_controller;
      m_view = new ResultView();
      m_view.SetController(this);
      LoadView();
    }

    void LoadView()
    {
      m_view.LoadView();
    }

    public void LoadDGV(ComputeConfig p_config)
    {
      m_displayedVersionCompare = 0;
      Config = p_config;
      m_view.PrepareDgv(p_config);
    }

    public void DisplayResult(SafeDictionary<UInt32, ComputeResult> p_result)
    {
      m_view.FillDGV(p_result);
    }

    public bool DisplayVersionComparaison()
    {
      if (Config == null || Config.Request.Versions == null || Config.Request.Versions.Count != 2)
      {
        Error = Local.GetValue("CUI.error.invalid_version");
        return (false);
      }
      UInt32 versionDiff1 = ComputeResult.GetDiffId(Config.Request.Versions[0], Config.Request.Versions[1]);
      UInt32 versionDiff2 = ComputeResult.GetDiffId(Config.Request.Versions[1], Config.Request.Versions[0]);


      if (m_displayedVersionCompare != versionDiff1)
        m_displayedVersionCompare = versionDiff1;
      else
        m_displayedVersionCompare = versionDiff2;

      m_view.SetVersionVisible(versionDiff1, m_displayedVersionCompare == versionDiff1);
      m_view.SetVersionVisible(versionDiff2, m_displayedVersionCompare == versionDiff2);
      return (true);
    }

    public void SwitchVersionComparaison()
    {
      if (m_displayedVersionCompare == 0)
        return;
      DisplayVersionComparaison();
    }

    public void HideVersionComparaison()
    {
      if (m_displayedVersionCompare == 0)
        return;
      UInt32 versionDiff1 = ComputeResult.GetDiffId(Config.Request.Versions[0], Config.Request.Versions[1]);
      UInt32 versionDiff2 = ComputeResult.GetDiffId(Config.Request.Versions[1], Config.Request.Versions[0]);

      m_view.SetVersionVisible(versionDiff1, false);
      m_view.SetVersionVisible(versionDiff2, false);
    }

    public bool ShowLog(UInt32 p_entityId, UInt32 p_versionId, UInt32 p_accountId, UInt32 p_period)
    {
      if (m_logController.ShowView(p_entityId, p_versionId, p_accountId, p_period) == false)
      {
        Error = m_logController.Error;
        return (false);
      }
      return (true);
    }

    public void DropOnExcel(bool p_copyOnlyExpanded)
    {
      m_view.DropOnExcel(p_copyOnlyExpanded);
    }
  }
}
