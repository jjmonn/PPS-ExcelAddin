﻿using System;
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
    ComputeConfig m_config = null;
    UInt32 m_displayedVersionCompare = 0;

    public ResultController(CUIController p_controller)
    {
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
      m_config = p_config;
      m_view.PrepareDgv(p_config);
    }

    public void DisplayResult(SafeDictionary<UInt32, ComputeResult> p_result)
    {
      m_view.FillDGV(p_result);
    }

    public bool DisplayVersionComparaison()
    {
      if (m_config == null || m_config.Request.Versions == null || m_config.Request.Versions.Count != 2)
      {
        Error = Local.GetValue("CUI.error.invalid_version");
        return (false);
      }
      UInt32 versionDiff1 = ComputeResult.GetDiffId(m_config.Request.Versions[0], m_config.Request.Versions[1]);
      UInt32 versionDiff2 = ComputeResult.GetDiffId(m_config.Request.Versions[1], m_config.Request.Versions[0]);


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
      UInt32 versionDiff1 = ComputeResult.GetDiffId(m_config.Request.Versions[0], m_config.Request.Versions[1]);
      UInt32 versionDiff2 = ComputeResult.GetDiffId(m_config.Request.Versions[1], m_config.Request.Versions[0]);

      m_view.SetVersionVisible(versionDiff1, false);
      m_view.SetVersionVisible(versionDiff2, false);
    }
  }
}
