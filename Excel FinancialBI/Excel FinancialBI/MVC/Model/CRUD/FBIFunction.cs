using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  using Utils;
  using Model;

  public class FBIFunction
  {
    public DateTime Period { get; set; }
    public UInt32 EntityId { get; set; }
    public UInt32 AccountId { get; set; }
    public UInt32 CurrencyId { get; set; }
    public UInt32 VersionId { get; set; }
    public SafeDictionary<AxisType, List<string>> AxisElems { get; set; }
    public List<string> Filters { get; set; }

    public string PeriodString 
    { 
      get { return (Period.ToString("MM/dd/yyyy")); }
      set { Period = DateTime.Parse(value); }
    }

    public string EntityName
    {
      get { return (AxisElemModel.Instance.GetValueName(EntityId)); }
      set
      {
        AxisElem l_elem = AxisElemModel.Instance.GetValue(AxisType.Entities, value);

        EntityId = (l_elem == null) ? 0 : l_elem.Id;
      }
    }

    public string AccountName 
    {
      get { return (AccountModel.Instance.GetValueName(AccountId)); }
      set { AccountId = AccountModel.Instance.GetValueId(value); }
    }

    public string CurrencyName 
    {
      get { return (CurrencyModel.Instance.GetValueName(CurrencyId)); }
      set { CurrencyId = CurrencyModel.Instance.GetValueId(value); }
    }

    public string VersionName 
    {
      get { return (VersionModel.Instance.GetValueName(VersionId)); }
      set { VersionId = VersionModel.Instance.GetValueId(value); }
    }

    public FBIFunction()
    {
      AxisElems = new SafeDictionary<AxisType, List<string>>();
      Filters = new List<string>();
    }
  }
}
