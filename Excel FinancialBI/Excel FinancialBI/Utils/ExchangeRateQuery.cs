using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.Utils
{
  using MVC.Model;
  using MVC.Model.CRUD;

    class ExchangeRateQuery
    {
      static double GetExchangeRate(UInt32 p_entityId, UInt32 p_destCurrency, UInt32 p_period, UInt32 p_rateVersionId)
      {
        EntityCurrency l_entity = EntityCurrencyModel.Instance.GetValue(p_entityId);

        if (l_entity == null)
          return (0);
        if (l_entity.CurrencyId == p_destCurrency)
          return (1);

        UInt32 mainCurrencyId = CurrencyModel.Instance.GetMainCurrency();
        ExchangeRate l_originRate = ExchangeRateModel.Instance.GetValue(l_entity.CurrencyId, p_rateVersionId, p_period);
        ExchangeRate l_destRate = ExchangeRateModel.Instance.GetValue(p_destCurrency, p_rateVersionId, p_period);

        if (l_originRate == null || (p_destCurrency != mainCurrencyId && l_destRate == null))
          return (0);
        if (l_destRate == null)
          return (l_originRate.Value);
        return (l_destRate.Value / l_originRate.Value);
      }

      public static double GetExchangeRateValue(UInt32 p_entityId, UInt32 p_destCurrency, Int32 p_period,
                                         UInt32 p_rateVersionId,
                                         TimeConfig p_timeConfig,
                                         Account.ConversionOptions p_conversionOption)
      {
        EntityCurrency entity = EntityCurrencyModel.Instance.GetValue(p_entityId);

        p_period = (p_timeConfig == TimeConfig.DAYS) ? PeriodModel.GetMonthIdFromPeriodId(p_period) : p_period;
        if (entity == null)
          return (0);
        if (entity.CurrencyId == p_destCurrency)
          return (1);
        if (p_timeConfig == TimeConfig.MONTHS || p_conversionOption == Account.ConversionOptions.END_OF_PERIOD_RATE)
          return (GetExchangeRate(p_entityId, p_destCurrency, (UInt32)p_period, p_rateVersionId));
        else
        {
          List<Int32> periodList = PeriodModel.GetYearPastMonths(p_period);
          double value = 0;
          int count = 0;

          for (int i = 0; i < periodList.Count; ++i)
          {
            value += GetExchangeRate(p_entityId, p_destCurrency, (UInt32)periodList[i], p_rateVersionId);
            count++;
          }
          return (value / count);
        }
      }
    };
}
