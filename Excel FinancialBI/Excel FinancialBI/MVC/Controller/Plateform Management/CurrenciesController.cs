using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FBI.MVC.Controller
{
  using View;
  using Model;
  using Model.CRUD;
  using Utils;

  class CurrenciesController : NameController<CurrenciesView>
  {
    public override IView View { get { return (m_view); } }

    public CurrenciesController()
    {
      m_view = new CurrenciesView();
      m_view.SetController(this);
      LoadView();
    }

    public override void LoadView()
    {
      m_view.LoadView();
    }

    public bool UpdateCurrency(Currency l_currency, bool p_inUse, string p_name, string p_symbol)
    {
      if (l_currency == null)
        return (false);
      l_currency = l_currency.Clone();
      l_currency.InUse = p_inUse;
      l_currency.Name = p_name;
      l_currency.Symbol = p_symbol;
      if (CurrencyModel.Instance.Update(l_currency))
        return (true);
      Error = Local.GetValue("general.error.system");
      return (false);
    }
  }
}
