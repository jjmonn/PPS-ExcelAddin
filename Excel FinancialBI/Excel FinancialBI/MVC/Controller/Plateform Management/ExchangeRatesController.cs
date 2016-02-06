using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using View;
  using Model;
  using Model.CRUD;
  using Utils;

  class ExchangeRatesController : NameController, IPlatformManagementController
  {
    ExchangeRatesView m_view;
    public override IView View { get { return (m_view); } }

    public ExchangeRatesController()
    {
      m_view = new ExchangeRatesView();
      m_view.SetController(this);
      LoadView();
    }

    public override void LoadView()
    {

    }

    public override void Close()
    {
      // Add any dispose action here !
      if (m_view != null)
      {
        m_view.Hide();
        m_view.Dispose();
        m_view = null;
      }
    }
  }
}
