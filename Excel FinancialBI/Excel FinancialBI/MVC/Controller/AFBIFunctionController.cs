using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using Model.CRUD;
  using Utils;
  using Model;
  using View;

  abstract class AFBIFunctionController : IController
  {
    public string Error { get; set; }
    public abstract IView View { get; }

    #region Checks

    protected bool IsValidEntity(string p_entity)
    {
      if (AxisElemModel.Instance.GetValue(AxisType.Entities, p_entity) == null)
      {
        Error = Local.GetValue("ppsbi.error.invalid_entity");
        return (false);
      }
      return (true);
    }

    protected bool IsValidAccount(string p_account)
    {
      Account l_account = AccountModel.Instance.GetValue(p_account);

      if (l_account == null || l_account.FormulaType == Account.FormulaTypes.TITLE)
      {
        Error = Local.GetValue("ppsbi.error.invalid_account");
        return (false);
      }
      return (true);
    }

    protected bool IsValidCurrency(string p_currency)
    {
      Currency l_currency = CurrencyModel.Instance.GetValue(p_currency);

      if (l_currency == null || CurrencyModel.Instance.GetUsedCurrencies().Contains(l_currency.Id) == false)
      {
        Error = Local.GetValue("ppsbi.error.invalid_currency");
        return (false);
      }
      return (true);
    }

    protected bool IsValidVersion(string p_version)
    {
      Version l_version = VersionModel.Instance.GetValue(p_version);

      if (l_version == null || l_version.IsFolder)
      {
        Error = Local.GetValue("ppsbi.error.invalid_version");
        return (false);
      }
      return (true);
    }

    protected bool IsValidAxisElemList(AxisType p_axis, List<string> p_axisElemList)
    {
      foreach (string l_name in p_axisElemList)
      {
        AxisElem l_axisElem = AxisElemModel.Instance.GetValue(p_axis, l_name);

        if (l_axisElem == null)
        {
          Error = Local.GetValue("ppsbi.error.invalid_axis_elem");
          return (false);
        }
      }
      return (true);
    }

    protected bool IsValidFilterValueList(List<string> p_filterValueList)
    {
      foreach (string l_fvName in p_filterValueList)
      {
        FilterValue l_fv = FilterValueModel.Instance.GetValue(l_fvName);

        if (l_fv == null)
        {
          Error = Local.GetValue("ppsbi.error.invalid_filter_value");
          return (false);
        }
      }
      return (true);
    }

    #endregion

  }
}
