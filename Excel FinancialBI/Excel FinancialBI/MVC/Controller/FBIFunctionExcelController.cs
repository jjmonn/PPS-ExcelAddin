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

  public class FBIFunctionExcelController : AFBIFunctionController
  {
    public override IView View { get { return (null); } }

    string GetValue(object p_param)
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

    List<string> GetValueList(object p_param)
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

    public object FBI(object p_entity, object p_account, object p_period, object p_currency, object p_version,
          object p_clientsFilters, object p_productsFilters, object p_adjustmentsFilters, object p_categoriesFilters)
    {
      return (null);
    }
  }
}