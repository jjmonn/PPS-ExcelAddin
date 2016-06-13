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

  public class ExchangeRatesController : FactBaseController<ExchangeRatesView, ExchangeRateVersion>
  {
    public ExchangeRatesController() : base()
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

    bool IsRateValid(ExchangeRate p_rate)
    {
      Currency l_currency = CurrencyModel.Instance.GetValue(p_rate.DestCurrencyId);
      ExchangeRateVersion l_version = RatesVersionModel.Instance.GetValue(p_rate.RateVersionId);

      if (l_version == null)
      {
        Error = Local.GetValue("exchange_rate.error.version_not_found");
        return (false);
      }
      if (l_currency == null)
      {
        Error = Local.GetValue("exchange_rate.error.currency_not_found");
        return (false);
      }
      return (true);
    }

    public bool UpdateRate(ExchangeRate p_rate)
    {
      if (IsRateValid(p_rate) == false)
        return (false);
      if (ExchangeRateModel.Instance.Update(p_rate))
        return (true);
      Error = Local.GetValue("general.error.system");
      return (false);
    }

    public bool CreateRate(ExchangeRate p_rate)
    {
      if (IsRateValid(p_rate) == false)
        return (false);
      if (ExchangeRateModel.Instance.GetValue(p_rate.DestCurrencyId, p_rate.RateVersionId, p_rate.Period) != null)
      {
        Error = Local.GetValue("general.error.name_already_used");
        return (false);
      }
      if (ExchangeRateModel.Instance.Create(p_rate))
        return (true);
      Error = Local.GetValue("general.error.system");
      return (false);
    }

    public override bool CreateVersion(ExchangeRateVersion p_rateVersion)
    {
      if (!IsVersionValid(p_rateVersion, RatesVersionModel.Instance) || !IsVersionNameAvailable(p_rateVersion.Name, RatesVersionModel.Instance))
        return (false);
      if (RatesVersionModel.Instance.Create(p_rateVersion))
        return (true);
      Error = Local.GetValue("general.error.system");
      return (false);
    }

    public override bool UpdateVersion(ExchangeRateVersion p_rateVersion)
    {
      if (!IsVersionValid(p_rateVersion, RatesVersionModel.Instance))
        return (false);
      if (RatesVersionModel.Instance.Update(p_rateVersion))
        return (true);
      Error = Local.GetValue("general.error.system");
      return (false);
    }

    public override bool DeleteVersion(UInt32 p_versionId)
    {
      if (RatesVersionModel.Instance.GetValue(p_versionId) == null)
      {
        Error = Local.GetValue("general.error.not_found");
        return (false);
      }
      if (RatesVersionModel.Instance.Delete(p_versionId))
        return (true);
      Error = Local.GetValue("general.error.system");
      return (false);
    }
  }
}
