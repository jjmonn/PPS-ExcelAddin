using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FBI.MVC.Controller
{
  using View;

  public abstract class BasePlatformMgtComponent<T> : IBasePlatformMgtComponent where T : ContainerControl, IPlatformMgtView
  {
    protected T m_view;

    virtual public void Close()
    {
      if (m_view != null)
      {
        m_view.CloseView();
        m_view.Hide();
        m_view.Dispose();
        m_view = null;
      }
    }
  }
}
