using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  using Network;
  using AccountKey = UInt32;
  using SortKey = String;
  using PeriodTypeKey = TimeConfig;
  using PeriodKey = Int32;
  using VersionKey = UInt32;

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
