using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddinExpress.MSO;
using Microsoft.Office.Interop.Excel;

namespace FBI.MVC.Controller
{
  using View;
  using Model.CRUD;

  public class FBIFunctionExcelController : AFBIFunctionController
  {
    public override IView View { get { return (null); } }

    static string GetValue(object p_param)
    {
      if (p_param.GetType() == typeof(string))
        return (p_param as string);
      if (p_param.GetType() == typeof(ADXExcelRef))
      {
        ADXExcelRef l_ref = p_param as ADXExcelRef;
        string l_address = l_ref.ConvertToA1Style();

        Range l_range = AddinModule.CurrentInstance.ExcelApp.Range[l_address];

        if (l_range != null)
          return (GetValue(l_range.Value));
      }
      return ("");
    }

    static List<string> GetValueList(object p_param)
    {
      List<string> l_list = new List<string>();
      if (p_param.GetType().IsArray == false)
        l_list.Add(GetValue(p_param));
      else
      {
        Type l_type = p_param.GetType();
        object[,] l_paramList = p_param as object[,];

        foreach (object l_param in l_paramList)
          l_list.Add(GetValue(l_param));
      }
      return (l_list);
    }

    bool IsValidFunction(FBIFunction p_function)
    {
      return (
              IsValidEntity(p_function.Entity) &&
              IsValidAccount(p_function.Account) &&
              IsValidCurrency(p_function.Currency) &&
              IsValidVersion(p_function.Version) &&
              IsValidAxisElemList(AxisType.Client, p_function.AxisElems[AxisType.Client]) &&
              IsValidAxisElemList(AxisType.Product, p_function.AxisElems[AxisType.Product]) &&
              IsValidAxisElemList(AxisType.Adjustment, p_function.AxisElems[AxisType.Adjustment]) &&
              IsValidFilterValueList(p_function.Filters));
    }

    public object FBI(object p_entity, object p_account, object p_period, object p_currency, object p_version,
          object p_clientsFilters, object p_productsFilters, object p_adjustmentsFilters, object p_categoriesFilters)
    {
      FBIFunction l_function = new FBIFunction();

      l_function.Entity = GetValue(p_entity);
      l_function.Account = GetValue(p_account);
      l_function.Period = GetValue(p_period);
      l_function.Currency = GetValue(p_currency);
      l_function.Version = GetValue(p_version);
      l_function.AxisElems[AxisType.Client] = GetValueList(p_clientsFilters);
      l_function.AxisElems[AxisType.Product] = GetValueList(p_productsFilters);
      l_function.AxisElems[AxisType.Adjustment] = GetValueList(p_adjustmentsFilters);
      l_function.Filters = GetValueList(p_categoriesFilters);

      if (IsValidFunction(l_function) == false)
        return (Error);
      return (null);
    }
  }
}