using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  using AccountKey = UInt32;
  using SortKey = String; // is fv, axis, value
  using PeriodTypeKey = TimeConfig;
  using PeriodKey = Int32;
  using VersionKey = UInt32;
  using TabKey = UInt32;

  public class ResultKey
  {
    public UInt32 AccountId { get { return (m_key.Item1); } }
    public SortKey SortHash { get { return (m_key.Item2); } }
    public SortKey EntityHash { get { return (m_key.Item3); } }
    public PeriodTypeKey PeriodType { get { return (m_key.Item4); } }
    public Int32 Period { get { return (m_key.Item5); } }
    public UInt32 VersionId { get { return (m_key.Item6); } }
    public UInt32 Tab { get { return (m_key.Item7); } }
    Tuple<AccountKey, SortKey, SortKey, PeriodTypeKey, PeriodKey, VersionKey, TabKey> m_key;
    public bool StrongVersion { get; private set; }

    public void RemoveTab()
    {
      m_key = new Tuple<AccountKey, SortKey, SortKey, PeriodTypeKey, PeriodKey, VersionKey, TabKey>
        (m_key.Item1, m_key.Item2, m_key.Item3, m_key.Item4, m_key.Item5, m_key.Item6, 0);
    }

    public Tuple<ResultKey, bool> GetTuple()
    {
      return (new Tuple<ResultKey, bool>(this, StrongVersion));
    }

    public ResultKey(Tuple<AccountKey, SortKey, SortKey, PeriodTypeKey, PeriodKey, VersionKey, TabKey> p_key)
    {
      m_key = p_key;
      StrongVersion = false;
    }

    public ResultKey()
    {
      m_key = new Tuple<AccountKey, SortKey, SortKey, PeriodTypeKey, PeriodKey, VersionKey, TabKey>(0, "", "", (PeriodTypeKey)0, 0, 0, 0);
      StrongVersion = false;
    }

    public ResultKey(AccountKey p_account, SortKey p_sort, SortKey p_entitySort, 
      PeriodTypeKey p_periodType, PeriodKey p_period, VersionKey p_version, bool p_strongVersion = false, TabKey p_tab = 0)
    {
      m_key = new Tuple<AccountKey, SortKey, SortKey, PeriodTypeKey, PeriodKey, VersionKey, TabKey>(
        p_account, p_sort, p_entitySort, p_periodType, p_period, p_version, p_tab);
      StrongVersion = p_strongVersion;
    }

    public override bool Equals(object p_obj)
    {
      lock (m_key)
      {
        ResultKey l_obj = (ResultKey)p_obj;

        if (l_obj.GetHashCode() == GetHashCode())
          if (l_obj.m_key.Item1 == m_key.Item1)
            if (l_obj.m_key.Item2 == m_key.Item2)
              if (l_obj.m_key.Item3 == m_key.Item3)
                if (l_obj.m_key.Item4 == m_key.Item4)
                  if (l_obj.m_key.Item5 == m_key.Item5)
                    if (l_obj.m_key.Item6 == m_key.Item6)
                      return (l_obj.m_key.Item7 == m_key.Item7);
        return (false);
      }
    }

    public override int GetHashCode()
    {
      lock (m_key)
      {
        return m_key.GetHashCode();
      }
    }

    public static ResultKey operator +(ResultKey p_a, ResultKey p_b)
    {
      PeriodKey l_periodKey;
      PeriodTypeKey l_periodTypeKey;
      AccountKey l_accountKey;
      VersionKey l_versionkey;
      SortKey l_sortKey = p_a.m_key.Item2 + p_b.m_key.Item2;
      SortKey l_entitySortKey = p_a.m_key.Item3 + p_b.m_key.Item3;
      TabKey l_tab;

      l_periodTypeKey = ((byte)p_b.PeriodType == 0) ? p_a.PeriodType : p_b.PeriodType;
      l_periodKey = (p_b.Period != 0) ? p_b.Period : p_a.Period;
      l_accountKey = (p_b.AccountId != 0) ? p_b.AccountId : p_a.AccountId;
      l_tab = (p_b.Tab != 0) ? p_b.Tab : p_a.Tab;
      l_versionkey = (p_b.VersionId != 0 && (p_b.StrongVersion || !p_a.StrongVersion)) ? p_b.VersionId : p_a.VersionId;

      ResultKey l_newKey = 
        new ResultKey(l_accountKey, l_sortKey, l_entitySortKey, l_periodTypeKey, l_periodKey,
          l_versionkey, (p_b.StrongVersion || p_a.StrongVersion), l_tab);

      return (l_newKey);
    }

    public static String GetSortKey(bool p_isAxis, AxisType p_axisType, UInt32 p_value)
    {
      String key = "#";

      key += (p_isAxis) ? "t" : "f";
      key += ((byte)p_axisType).ToString();
      key += "v";
      key += p_value.ToString();
      return (key);
    }

    public bool IsClientSort()
    {
      string l_findStr = "#t" + ((byte)AxisType.Client).ToString();

      if (SortHash.Contains(l_findStr) == false)
        return (false);
      if (SortHash.LastIndexOf("#") != SortHash.LastIndexOf(l_findStr))
        return (false);
      return (true);
    }

    public bool IsSort(bool p_isAxis, AxisType p_axis, UInt32 p_id = 0)
    {
      string l_findStr = ((p_isAxis) ? "#t" : "#f") + ((byte)p_axis).ToString();

      if (p_id != 0)
        l_findStr += "v" + p_id.ToString();
      if (SortHash.Contains(l_findStr) == false)
        return (false);
      if (SortHash.LastIndexOf("#") != SortHash.LastIndexOf(l_findStr))
        return (false);
      return (true);
    }

    public Tuple<bool, AxisType, UInt32> LastSort
    {
      get
      {
        bool l_isAxis;
        Int32 l_axisType;
        UInt32 l_id;

        Int32 l_beginPos = SortHash.LastIndexOf('#');

        if (l_beginPos < 0 || l_beginPos == SortHash.Length)
          return (null);
        string l_str = SortHash.Substring(l_beginPos + 1);
        l_isAxis = (l_str[0] == 't');
        
        Int32 l_endPos = l_str.IndexOf('v');
        if (l_endPos < 0 || l_endPos == SortHash.Length)
          return (null);
        l_str = l_str.Substring(1, l_endPos - 1);
        if (Int32.TryParse(l_str, out l_axisType) == false)
          return (null);
        l_str = l_str.Substring(l_endPos + 1);
        if (UInt32.TryParse(l_str, out l_id) == false)
          return (null);
        return (new Tuple<bool, AxisType, TabKey>(l_isAxis, (AxisType)l_axisType, l_id));
      }
    }

    public UInt32 EntityId
    {
      get
      {
        string l_findStr = "#t" + ((byte)AxisType.Entities).ToString() + "v";
        Int32 l_beginPos = EntityHash.LastIndexOf(l_findStr);
        Int32 l_endPos = EntityHash.LastIndexOf('#');
        UInt32 l_entityId = 0;

        if (l_endPos <= l_beginPos)
          l_endPos = EntityHash.Length;
        if (l_endPos < 0 || l_beginPos < 0)
          return (0);
        l_beginPos += l_findStr.Length;
        string l_entityStr = EntityHash.Substring(l_beginPos, l_endPos - l_beginPos);
        if (UInt32.TryParse(l_entityStr, out l_entityId))
          return (l_entityId);
        return (0);
      }
    }
  }
}
