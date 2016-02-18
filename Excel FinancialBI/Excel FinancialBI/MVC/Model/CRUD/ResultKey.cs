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

  public class ResultKey
  {
    public UInt32 VersionId { get { return (m_key.Item6); } }
    public UInt32 AccountId { get { return (m_key.Item1); } }
    public SortKey SortHash { get { return (m_key.Item2); } }
    public SortKey EntityHash { get { return (m_key.Item3); } }
    public PeriodTypeKey PeriodType { get { return (m_key.Item4); } }
    public DateTime Date { get { return (DateTime.FromOADate(m_key.Item5)); } }
    Tuple<AccountKey, SortKey, SortKey, PeriodTypeKey, PeriodKey, VersionKey> m_key;
    public bool StrongVersion { get; private set; }

    public Tuple<ResultKey, bool> GetTuple()
    {
      return (new Tuple<ResultKey, bool>(this, StrongVersion));
    }

    public ResultKey(Tuple<AccountKey, SortKey, SortKey, PeriodTypeKey, PeriodKey, VersionKey> p_key)
    {
      m_key = p_key;
      StrongVersion = false;
    }

    public ResultKey()
    {
      m_key = new Tuple<AccountKey, SortKey, SortKey, PeriodTypeKey, PeriodKey, VersionKey>(0, "", "", (PeriodTypeKey)0, 0, 0);
      StrongVersion = false;
    }

    public ResultKey(AccountKey p_account, SortKey p_sort, SortKey p_entitySort, PeriodTypeKey p_periodType, PeriodKey p_period, VersionKey p_version, bool p_strongVersion = false)
    {
      m_key = new Tuple<AccountKey, SortKey, SortKey, PeriodTypeKey, PeriodKey, VersionKey>(p_account, p_sort, p_entitySort, p_periodType, p_period, p_version);
      StrongVersion = p_strongVersion;
    }

    public override bool Equals(object p_obj)
    {
      ResultKey l_obj = (ResultKey)p_obj;

      if (l_obj.GetHashCode() == GetHashCode())
        if (l_obj.m_key.Item1 == m_key.Item1)
          if (l_obj.m_key.Item2 == m_key.Item2)
            if (l_obj.m_key.Item3 == m_key.Item3)
              if (l_obj.m_key.Item4 == m_key.Item4)
                if (l_obj.m_key.Item5 == m_key.Item5)
                  if (l_obj.m_key.Item6 == m_key.Item6)
                    return (true);
      return (false);
    }

    public override int GetHashCode()
    {
      return m_key.GetHashCode();
    }

    public static ResultKey operator +(ResultKey p_a, ResultKey p_b)
    {
      PeriodKey l_periodKey;
      PeriodTypeKey l_periodTypeKey;
      AccountKey l_accountKey;
      VersionKey l_versionkey;
      SortKey l_sortKey = p_a.m_key.Item2 + p_b.m_key.Item2;
      SortKey l_entitySortKey = p_a.m_key.Item3 + p_b.m_key.Item3;

      l_periodTypeKey = ((byte)p_b.m_key.Item4 == 0) ? p_a.m_key.Item4 : p_b.m_key.Item4;
      l_periodKey = (p_b.m_key.Item4 != 0) ? p_b.m_key.Item5 : p_a.m_key.Item5;
      l_accountKey = (p_b.m_key.Item1 != 0) ? p_b.m_key.Item1 : p_a.m_key.Item1;
      l_versionkey = (p_b.m_key.Item6 != 0 && (p_b.StrongVersion || !p_a.StrongVersion)) ? p_b.m_key.Item6 : p_a.m_key.Item6;

      ResultKey l_newKey = new ResultKey(l_accountKey, l_sortKey, l_entitySortKey, l_periodTypeKey, l_periodKey, l_versionkey, (p_b.StrongVersion || p_a.StrongVersion));

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
  }
}
