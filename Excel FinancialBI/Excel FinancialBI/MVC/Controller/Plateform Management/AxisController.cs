using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FBI.MVC.Controller
{
  using View;

  class AxisController : IController, IPlatformManagementController
  {
    AxisView m_view;
    public string Error { get; set; }

    public AxisController(AxisView p_view)
    {
      m_view = p_view;
      m_view.SetController(this);
      m_view.LoadView();
    }

    public void Close()
    {

    }

    public void AddControlToPanel(Panel p_panel)
    {
      p_panel.Controls.Add(m_view);
    }
  }
}
