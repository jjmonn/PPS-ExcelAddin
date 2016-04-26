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
  using Model;
  using Utils;
  using Network;

  public class FBIFunctionExcelController : AFBIFunctionController
  {
    static System.Threading.ManualResetEvent m_waitResult;
    public override IView View { get { return (null); } }
    SafeDictionary<Int32, ComputeResult> m_results;
    static string m_lastParameter ="";
    static List<Tuple<LegacyComputeRequest, ComputeResult>> m_computeCache = new List<Tuple<LegacyComputeRequest, ComputeResult>>();
    public static FBIFunction LastExecutedFunction { get; set; }

    public FBIFunctionExcelController()
    {
      m_results = new SafeDictionary<int, ComputeResult>();
      m_waitResult = new System.Threading.ManualResetEvent(false);
      LegacyComputeModel.Instance.ComputeCompleteEvent += OnComputeResult;
    }

    void ResetCache()
    {
      m_computeCache.Clear();
    }

    public void Refresh()
    {
      ResetCache();
      Worksheet l_worksheet = AddinModule.CurrentInstance.ExcelApp.ActiveSheet as Worksheet;

      if (l_worksheet != null)
      {
        Range l_range = l_worksheet.Range[l_worksheet.Cells[1, 1], WorksheetWriter.GetRealLastCell(l_worksheet)];
        foreach (Range l_cell in l_range)
          l_cell.Formula = l_cell.Formula;
      }
    }

    static dynamic GetValue(object p_param, string p_name)
    {
      m_lastParameter = p_name;
      if (p_param.GetType() == typeof(ADXExcelRef))
      {
        ADXExcelRef l_ref = p_param as ADXExcelRef;
        string l_address = l_ref.ConvertToA1Style();

        Range l_range = AddinModule.CurrentInstance.ExcelApp.Range[l_address];

        if (l_range != null)
          return (GetValue(l_range.Value, p_name));
        else
          return ("");
      }
      else
        return (p_param);
    }

    static List<string> GetValueList(object p_param, string p_name)
    {
      List<string> l_list = new List<string>();
      if (p_param.GetType().IsArray == false)
      {
        if ((string)p_param != "")
          l_list.Add((string)GetValue(p_param, p_name));
      }
      else
      {
        Type l_type = p_param.GetType();
        object[,] l_paramList = p_param as object[,];

        foreach (object l_param in l_paramList)
        {
          if ((string)l_param != "")
            l_list.Add((string)GetValue(l_param, p_name));
        }
      }
      return (l_list);
    }

    bool IsValidFunction(FBIFunction p_function)
    {
      return (
              IsValidEntity(p_function.EntityName) &&
              IsValidAccount(p_function.AccountName) &&
              IsValidCurrency(p_function.CurrencyName) &&
              IsValidVersion(p_function.VersionName) &&
              IsValidAxisElemList(AxisType.Client, p_function.AxisElems[AxisType.Client]) &&
              IsValidAxisElemList(AxisType.Product, p_function.AxisElems[AxisType.Product]) &&
              IsValidAxisElemList(AxisType.Adjustment, p_function.AxisElems[AxisType.Adjustment]) &&
              IsValidFilterValueList(p_function.Filters));
    }

    public object FBI(object p_entity, object p_account, object p_period, object p_currency, object p_version,
          object p_clientsFilters, object p_productsFilters, object p_adjustmentsFilters, object p_categoriesFilters)
    {
      try
      {
        Range l_cell = AddinModule.CurrentInstance.ExcelApp.ActiveCell;
        FBIFunction l_function = new FBIFunction();

        l_function.EntityName = GetValue(p_entity, Local.GetValue("ppsbi.entity"));
        l_function.AccountName = GetValue(p_account, Local.GetValue("ppsbi.account"));
        dynamic l_period = GetValue(p_period, Local.GetValue("ppsbi.period"));
        if (l_period.GetType() == typeof(string))
          l_function.PeriodString = l_period;
        else
          l_function.Period = l_period;
        l_function.CurrencyName = GetValue(p_currency, Local.GetValue("ppsbi.currency"));
        l_function.VersionName = GetValue(p_version, Local.GetValue("ppsbi.version"));
        l_function.AxisElems[AxisType.Client] = GetValueList(p_clientsFilters, Local.GetValue("ppsbi.clients_filter"));
        l_function.AxisElems[AxisType.Product] = GetValueList(p_productsFilters, Local.GetValue("ppsbi.products_filter"));
        l_function.AxisElems[AxisType.Adjustment] = GetValueList(p_adjustmentsFilters, Local.GetValue("ppsbi.adjustments_filter"));
        l_function.Filters = GetValueList(p_categoriesFilters, Local.GetValue("ppsbi.categories_filter"));

        Version l_version = VersionModel.Instance.GetValue(l_function.VersionId);

        if (l_version == null)
          return ("general.error.system");
        if (l_version.TimeConfiguration <= TimeConfig.MONTHS)
          l_function.Period = l_function.Period.AddDays(DateTime.DaysInMonth(l_function.Period.Year, l_function.Period.Month) - l_function.Period.Day);
        if (l_version.TimeConfiguration <= TimeConfig.YEARS)
          l_function.Period = l_function.Period.AddMonths(12 - l_function.Period.Month);
        if (IsValidFunction(l_function) == false)
          return (Error);
        Int32 l_requestId;
        m_results.Clear();

        LastExecutedFunction = l_function;
        if (Compute(l_function, out l_requestId) == false)
          return (Local.GetValue("ppsbi.error.unable_to_compute"));

        ResultKey l_key = new ResultKey(l_function.AccountId, "", "", l_version.TimeConfiguration,
          (int)l_function.Period.ToOADate(), l_version.Id, true);

        if (m_results[l_requestId] == null)
          return (Error = Local.GetValue("general.error.system"));
        return (m_results[l_requestId].Values[l_key]);
      }
      catch
      {
        return (m_lastParameter + " " + Local.GetValue("ppsbi.error.invalid_parameter"));
      }
    }

    bool Compute(FBIFunction p_function, out Int32 p_requestId)
    {
      LegacyComputeRequest l_request = new LegacyComputeRequest();
      Version l_version = VersionModel.Instance.GetValue(p_function.VersionId);

      p_requestId = 0;
      if (l_version == null)
        return (false);
      l_request.AccountList.Add(p_function.AccountId);

      foreach (string l_client in p_function.AxisElems[AxisType.Client])
        l_request.AxisElemList.Add(new Tuple<AxisType, uint>(AxisType.Client, AxisElemModel.Instance.GetValueId(l_client)));
      foreach (string l_product in p_function.AxisElems[AxisType.Product])
        l_request.AxisElemList.Add(new Tuple<AxisType, uint>(AxisType.Product, AxisElemModel.Instance.GetValueId(l_product)));
      foreach (string l_adjust in p_function.AxisElems[AxisType.Adjustment])
        l_request.AxisElemList.Add(new Tuple<AxisType, uint>(AxisType.Adjustment, AxisElemModel.Instance.GetValueId(l_adjust)));

      foreach (string l_filterName in p_function.Filters)
      {
        FilterValue l_fv = FilterValueModel.Instance.GetValue(l_filterName);
        Filter l_filter = FilterModel.Instance.GetValue(l_fv.FilterId);

        l_request.FilterList.Add(new Tuple<AxisType, uint, uint>(l_filter.Axis, l_fv.FilterId, l_fv.Id));
      }

      l_request.Versions.Add(p_function.VersionId);
      l_request.Process = Account.AccountProcess.FINANCIAL;
      l_request.StartPeriod = (int)p_function.Period.ToOADate();
      l_request.NbPeriods = 1;
      l_request.EntityId = p_function.EntityId;
      l_request.CurrencyId = p_function.CurrencyId;
      l_request.GlobalFactVersionId = l_version.GlobalFactVersionId;
      l_request.RateVersionId = l_version.RateVersionId;

      ComputeResult l_result = FindInCache(l_request);
      if (l_result != null)
      {
        SafeDictionary<UInt32, ComputeResult> l_dic = new SafeDictionary<UInt32, ComputeResult>();
        l_dic[(UInt32)l_result.RequestId] = l_result;
        p_requestId = l_result.RequestId;
        OnComputeResult(ErrorMessage.SUCCESS, l_request, l_dic);
      }
      else
      {
        List<Int32> l_requestIdList = new List<int>();
        m_waitResult = new System.Threading.ManualResetEvent(false);
        bool l_success = LegacyComputeModel.Instance.Compute(l_request, l_requestIdList);

        if (l_success == false)
          return (false);
        p_requestId = l_requestIdList.FirstOrDefault();
        if (m_waitResult.WaitOne(2000) == false)
          return (false);
      }
      return (true);
    }

    void OnComputeResult(ErrorMessage p_status, LegacyComputeRequest p_request, SafeDictionary<UInt32, ComputeResult> p_result)
    {
      if (p_status != ErrorMessage.SUCCESS)
      {
        Error = Local.GetValue("ppsbi.error.compute_failed") + ": " + Network.Error.GetMessage(p_status);
        return;
      }
      foreach (ComputeResult l_result in p_result.Values)
      {
        m_computeCache.Add(new Tuple<LegacyComputeRequest, ComputeResult>(p_request, l_result));
        m_results[l_result.RequestId] = l_result;
      }
      m_waitResult.Set();
    }

    ComputeResult FindInCache(LegacyComputeRequest p_request)
    {
      foreach (Tuple<LegacyComputeRequest, ComputeResult> l_pair in m_computeCache)
      {
        LegacyComputeRequest l_request = l_pair.Item1;

        if (l_request.EntityId == p_request.EntityId &&
          l_request.CurrencyId == p_request.CurrencyId &&
          l_request.RateVersionId == p_request.RateVersionId &&
          l_request.GlobalFactVersionId == p_request.GlobalFactVersionId &&
          !l_request.AccountList.Except(p_request.AccountList).Any() && l_request.AccountList.Count == p_request.AccountList.Count &&
          l_request.AxisHierarchy == p_request.AxisHierarchy &&
          !l_request.FilterList.Except(p_request.FilterList).Any() && l_request.FilterList.Count == p_request.FilterList.Count &&
          !l_request.AxisElemList.Except(p_request.AxisElemList).Any() && l_request.AxisElemList.Count == p_request.AxisElemList.Count &&
          !l_request.SortList.Except(p_request.SortList).Any() && l_request.SortList.Count == p_request.SortList.Count &&
          !l_request.Versions.Except(p_request.Versions).Any() && l_request.Versions.Count == p_request.Versions.Count)
          return (l_pair.Item2);
      }
      return (null);
    }
  }
}