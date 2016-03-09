using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using View;
  class SettingsController : IController
  {
    SettingsView m_view;
    public string Error { get; set; }
    public IView View { get { return (m_view); } }

    public SettingsController()
    {
      m_view = new SettingsView();
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
