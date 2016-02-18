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

  public class ExcelRateController : IExcelImportController
  {
    UInt32 m_versionId;
    ExcelImportView m_view;

    public string Error { get; set; }
    public IView View { get { return (m_view); } }

    public ExcelRateController()
    {
      m_view = new ExcelImportView(ExcelImportView.ViewType.EXCHANGE_RATE);
      m_view.SetController(this);
      m_view.LoadView();
    }

    public void LoadView(UInt32 p_version)
    {
      m_versionId = p_version;
      if (p_version > 0)
      {
        m_view.Reload();
      }
    }

    private bool SetError(string p_localName)
    {
      Error = Local.GetValue(p_localName);
      return (false);
    }

    private bool IsRateValid(ExchangeRate p_rate)
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

    public bool Create(UInt32 p_id, Int32 p_period, double p_value)
    {
      ExchangeRate l_rate = ExchangeRateModel.Instance.GetValue(p_id, m_versionId, (UInt32)p_period);

      if (l_rate == null)
      {
        l_rate = new ExchangeRate();
        l_rate.Period = (UInt32)p_period;
        l_rate.DestCurrencyId = p_id;
        l_rate.RateVersionId = m_versionId;
        l_rate.Value = p_value;
        if (IsRateValid(l_rate) == false)
          return (false);
        ExchangeRateModel.Instance.Create(l_rate);
      }
      else
      {
        l_rate = l_rate.Clone();
        l_rate.Value = p_value;
        if (IsRateValid(l_rate) == false)
          return (false);
        ExchangeRateModel.Instance.Update(l_rate);
      }
      return (true);
    }

    public bool Create(UInt32 p_id, List<Int32> p_periods, List<double> p_values)
    {
      Int32 i = 0;

      if (p_values == null || p_periods == null)
        return (this.SetError("upload.not_complete"));
      if (p_periods.Count != p_values.Count)
        return (this.SetError("upload.range_mismatch"));
      for (i = 0; i < p_periods.Count; ++i)
      {
        if (!this.Create(p_id, p_periods[i], p_values[i]))
          return (false);
      }
      return (true);
    }
  }
}
