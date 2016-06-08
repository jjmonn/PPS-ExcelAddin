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

  class ResultController : AResultController<ResultView>
  {
    CUIController m_parentController;
    UInt32 m_displayedVersionCompare = 0;

    public ResultController(CUIController p_controller)
    {
      m_parentController = p_controller;
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

    public override void LoadDGV(ComputeConfig p_config)
    {
      m_displayedVersionCompare = 0;
      base.LoadDGV(p_config);
    }
  }
}
