using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using View;
  using Model.CRUD;

  class ResultController : IController
  {
    CUIController m_parentController;
    ResultView m_view;
    public string Error { get; set; }
    public IView View { get { return (m_view); } }

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
      m_view.PrepareDgv(p_config);
    }
  }
}
