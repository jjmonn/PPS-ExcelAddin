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

  class ExchangeRatesController : NameController<ExchangeRatesView>
  {
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
  }
}
