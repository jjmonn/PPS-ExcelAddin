using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using Utils;
  using View;
  using Model;
  using Model.CRUD;

  interface IResultController : IController
  {
    void LoadView();
    void LoadDGV(ComputeConfig p_config);
    void DisplayResult(SafeDictionary<UInt32, ComputeResult> p_result);
    void DropOnExcel(bool p_copyOnlyExpanded);
    bool ShowLog(UInt32 p_entityId, UInt32 p_versionId, UInt32 p_accountId, UInt32 p_period);
    ComputeConfig Config { get; set; }
  };

  class AResultController<TView> : IResultController where TView : class, IResultView, new()
  {
    protected TView m_view;
    public string Error { get; set; }
    public IView View { get { return (m_view); } }
    public ComputeConfig Config { get; set; }
    LogController m_logController;

    public AResultController()
    {
      Config = null;
      m_view = new TView();
      m_view.SetController(this);
      LoadView();
      m_logController = new LogController();
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

    public void LoadView()
    {
      m_view.LoadView();
    }

    public virtual void LoadDGV(ComputeConfig p_config)
    {
      Config = p_config;
      m_view.PrepareDgv(p_config);
    }

    public void DisplayResult(SafeDictionary<UInt32, ComputeResult> p_result)
    {
      m_view.FillDGV(p_result);
    }

    public void DropOnExcel(bool p_copyOnlyExpanded)
    {
      m_view.DropOnExcel(p_copyOnlyExpanded);
    }
  }
}
