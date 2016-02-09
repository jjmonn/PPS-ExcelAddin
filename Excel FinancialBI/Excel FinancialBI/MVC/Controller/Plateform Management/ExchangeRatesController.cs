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

  class ExchangeRatesController : FactBaseController<ExchangeRatesView>
  {
    public ExchangeRatesController() : base(new NewRateVersionView())
    {
      m_view = new ExchangeRatesView();
      m_view.SetController(this);
      SetNewVersionUIController(this);
      LoadView();
    }

    public override void LoadView()
    {
      base.LoadView();
      m_view.LoadView();
    }

    public bool Update(ExchangeRate p_rate)
    {
      ExchangeRateModel.Instance.Update(p_rate);
      return (true);
    }

    public bool Create(ExchangeRate p_rate)
    {
      ExchangeRateModel.Instance.Create(p_rate);
      return (true);
    }

    bool IsVersionNameAvailable(string p_name)
    {
      if (RatesVersionModel.Instance.GetValue(p_name) != null)
      {
        Error = Local.GetValue("general.error.name_already_used");
        return (false);
      }
      return (true);
    }

    public bool CreateVersion(ExchangeRateVersion p_rateVersion)
    {
      if (!IsNameValid(p_rateVersion.Name) || !IsVersionNameAvailable(p_rateVersion.Name))
        return (false);
      if (p_rateVersion.NbPeriod == 0)
      {
        Error = Local.GetValue("version.error.nb_period");
        return (false);
      }
      RatesVersionModel.Instance.Create(p_rateVersion);
      return (true);
    }
  }
}
