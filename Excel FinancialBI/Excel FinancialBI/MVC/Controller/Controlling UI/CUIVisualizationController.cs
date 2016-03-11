using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using View;

  class CUIVisualizationController : IController
  {
    CUIVisualization m_view;

    public string Error { get; set; }
    public IView View { get { return (m_view); } }

    public CUIVisualizationController()
    {
      m_view = new CUIVisualization();
      m_view.SetController(this);
      LoadView();
    }

    void LoadView()
    {
      m_view.LoadView();
      m_view.Show();
    }
  }
}
