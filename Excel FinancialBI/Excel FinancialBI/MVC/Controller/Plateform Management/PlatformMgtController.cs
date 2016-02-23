using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FBI.MVC.View;
using System.Windows.Forms;

namespace FBI.MVC.Controller
{
  using Controller;
  using Utils;

  class PlatformMgtController : IController
  {
    PlatformMGTGeneralUI m_view;
    public string Error { get; set; }
    public IView View { get { return (m_view); } }
    private IBasePlatformMgtComponent m_currentController;

    public PlatformMgtController()
    {
      m_view = new PlatformMGTGeneralUI();
      m_view.SetController(this);
      m_view.Show();
    }

    void CloseCurrentControl()
    {
      if ((m_currentController != null))
      {
        m_currentController.Close();
        m_currentController = null;
      }
    }

    public void SwitchView<TView, TController>(TController p_controller)
      where TView : UserControl, IView
      where TController : IBasePlatformMgtComponent, IController
    {
      CloseCurrentControl();
      m_currentController = p_controller;
      UserControl l_view = p_controller.View as UserControl;
      m_view.Panel1.Controls.Add(l_view);
      l_view.Dock = DockStyle.Fill;
    }

    void Close()
    {
      CloseCurrentControl();
    }
  }
}
