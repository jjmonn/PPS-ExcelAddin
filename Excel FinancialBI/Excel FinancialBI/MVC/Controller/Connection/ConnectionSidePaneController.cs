using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using View;
  using Network;
  using Model;
  using Model.CRUD;

  class ConnectionSidePaneController : IController
  {
    public string Error { get; set; }
    ConnectionSidePane m_view;

    public ConnectionSidePaneController(ConnectionSidePane p_view)
    {
      m_view = p_view;
    }
  }
}
