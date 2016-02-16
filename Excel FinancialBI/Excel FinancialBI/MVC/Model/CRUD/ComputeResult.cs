using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  using Network;
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

  public class ComputeResult
  {
    public SafeDictionary<ResultKey, double> Values { get; private set; }
    public UInt32 VersionId { get; set; }
    ComputeRequest m_request;
    Version m_version;
    List<Int32> m_periodList;
    List<Int32> m_aggregationPeriodList;
    public bool IsDiff { get; private set; }

    ComputeResult()
    {
      Values = new SafeDictionary<ResultKey, double>();
      IsDiff = false;
    }

    public static ComputeResult BuildComputeResult(ComputeRequest p_request, ByteBuffer p_packet, UInt32 p_versionId)
    {
      ComputeResult l_result = new ComputeResult();

      l_result.VersionId = p_versionId;
      l_result.m_request = p_request;
      l_result.m_version = VersionModel.Instance.GetValue(l_result.VersionId);
      l_result.m_periodList = PeriodModel.GetPeriodList((Int32)p_request.StartPeriod, (Int32)p_request.NbPeriods, l_result.m_version.TimeConfiguration);

      TimeConfig l_aggregationTimeConfig = (TimeConfig)((byte)l_result.m_version.TimeConfiguration + 1);
      if (Enum.IsDefined(typeof(TimeConfig), l_aggregationTimeConfig))
        l_result.m_aggregationPeriodList = PeriodModel.GetPeriodList((Int32)p_request.StartPeriod, (Int32)p_request.NbPeriods, l_aggregationTimeConfig);
      else
        l_result.m_aggregationPeriodList = new List<Int32>();
      l_result.FillResultData(p_packet);
      return (l_result);
    }

    void FillResultData(ByteBuffer p_packet, SortKey p_sortKey = "")
    {
      bool l_isFiltered;
      SortKey l_currentLevelKey = "";

      if ((l_isFiltered = p_packet.ReadBool()))
      {
        AxisType l_axis = (AxisType)p_packet.ReadUint8();
        bool l_isAxis = p_packet.ReadBool();
        UInt32 l_value;

        if (l_isAxis)
          l_value = p_packet.ReadUint32();
        else
        {
          p_packet.ReadUint32();
          l_value = p_packet.ReadUint32();
        }
        l_currentLevelKey = ResultKey.GetSortKey(l_isAxis, l_axis, l_value);
        p_sortKey += l_currentLevelKey;
      }
      FillEntityData(p_packet, p_sortKey, l_currentLevelKey);

      UInt32 l_nbChildResult = p_packet.ReadUint32();
      for (UInt32 i = 0; i < l_nbChildResult; ++i)
        FillResultData(p_packet, p_sortKey);
    }

    void FillEntityData(ByteBuffer p_packet, SortKey p_sortKey, SortKey p_currentLevelKey, SortKey p_entityKey = "")
    {
      UInt32 l_entityId = p_packet.ReadUint32();
      UInt32 l_nbAccount = p_packet.ReadUint32();
      bool firstLevel = (p_entityKey == "");

      p_entityKey += ResultKey.GetSortKey(true, AxisType.Entities, l_entityId);

      for (UInt32 i = 0; i < l_nbAccount; ++i)
      {
        UInt32 l_accountId = p_packet.ReadUint32();
        UInt16 l_nbPeriod = p_packet.ReadUint16();
        UInt32 l_nbAggregation;

        for (UInt16 j = 0; j < l_nbPeriod && j < m_periodList.Count; ++j)
        {
          double l_value = p_packet.ReadDouble();

          if (firstLevel)
            Values[new ResultKey(l_accountId, p_sortKey, "", m_version.TimeConfiguration, m_periodList[j], VersionId)] = l_value;
          Values[new ResultKey(l_accountId, p_sortKey, p_entityKey, m_version.TimeConfiguration, m_periodList[j], VersionId)] = l_value;
        }

        l_nbAggregation = p_packet.ReadUint32();
        TimeConfig l_aggregationTimeConfig = (TimeConfig)((byte)m_version.TimeConfiguration + 1);
        for (UInt16 j = 0; j < l_nbAggregation && j < m_aggregationPeriodList.Count; ++j)
        {
          double l_value = p_packet.ReadDouble();

          if (firstLevel)
            Values[new ResultKey(l_accountId, p_sortKey, "", l_aggregationTimeConfig, m_aggregationPeriodList[j], VersionId)] = l_value;
          Values[new ResultKey(l_accountId, p_sortKey, p_entityKey, l_aggregationTimeConfig, m_aggregationPeriodList[j], VersionId)] = l_value;
        }

        UInt32 l_nbChildEntity = p_packet.ReadUint32();
        for (UInt32 j = 0; j < l_nbChildEntity; ++j)
          FillEntityData(p_packet, p_sortKey, p_currentLevelKey, p_entityKey);
      }
    }

    public static ComputeResult operator-(ComputeResult p_a, ComputeResult p_b)
    {
      ComputeResult l_result = new ComputeResult();

      l_result.IsDiff = true;
      foreach (KeyValuePair<ResultKey, double> l_pair in p_a.Values)
      {
        double l_bValue = p_b.Values[l_pair.Key];

        l_result.Values[l_pair.Key] = l_pair.Value - l_bValue;
      }
      l_result.VersionId = p_a.VersionId ^ p_b.VersionId;
      return (l_result);
    }
  }
}
