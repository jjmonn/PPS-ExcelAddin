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

  class ComputeResult
  {
    public SafeDictionary<Tuple<AccountKey, SortKey, PeriodTypeKey, PeriodKey, VersionKey>, double> Values { get; private set; }
    ComputeRequest m_request;
    Version m_version;
    List<Int32> m_periodList;
    List<Int32> m_aggregationPeriodList; 

    ComputeResult()
    {
      Values = new SafeDictionary<Tuple<AccountKey, SortKey, PeriodTypeKey, PeriodKey, VersionKey>, double>();
    }

    public static ComputeResult BuildComputeResult(ComputeRequest p_request, ByteBuffer p_packet) // store request by request id
    {
      ComputeResult l_result = new ComputeResult();

      l_result.m_request = p_request;
      l_result.m_version = VersionModel.Instance.GetValue(p_request.VersionId);
      l_result.m_periodList = PeriodModel.GetPeriodList((Int32)p_request.StartPeriod, (Int32)p_request.NbPeriods, l_result.m_version.TimeConfiguration);

      TimeConfig l_aggregationTimeConfig = (TimeConfig)((byte)l_result.m_version.TimeConfiguration + 1);
      if (Enum.IsDefined(typeof(TimeConfig), l_aggregationTimeConfig))
        l_result.m_aggregationPeriodList = PeriodModel.GetPeriodList((Int32)p_request.StartPeriod, (Int32)p_request.NbPeriods, l_result.m_version.TimeConfiguration);
      else
        l_result.m_aggregationPeriodList = new List<Int32>();
      l_result.FillResultData(p_packet);
      return (l_result);
    }

    void FillResultData(ByteBuffer p_packet)
    {
      SortKey l_sortKey = "";
      Version l_version = VersionModel.Instance.GetValue(m_request.VersionId);
      bool l_isFiltered;

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
        l_sortKey += GetSortKey(l_isAxis, l_axis, l_value);
      }
      FillEntityData(p_packet, l_sortKey);

      UInt32 l_nbChildResult = p_packet.ReadUint32();
      for (UInt32 i = 0; i < l_nbChildResult; ++i)
        FillResultData(p_packet);
    }

    void FillEntityData(ByteBuffer p_packet, SortKey p_sortKey)
    {
      UInt32 l_entityId = p_packet.ReadUint32();
      UInt32 l_nbAccount = p_packet.ReadUint32();

      p_sortKey += GetSortKey(true, AxisType.Entities, l_entityId);

      for (UInt32 i = 0; i < l_nbAccount; ++i)
      {
        UInt32 l_accountId = p_packet.ReadUint32();
        UInt16 l_nbPeriod = p_packet.ReadUint16();
        UInt32 l_nbAggregation;

        for (UInt16 j = 0; j < l_nbPeriod && j < m_periodList.Count; ++j)
        {
          SortKey l_sortKey = p_sortKey;
          Tuple<AccountKey, SortKey, PeriodTypeKey, PeriodKey, VersionKey> l_valueKey = 
            new Tuple<AccountKey, SortKey, PeriodTypeKey, PeriodKey, VersionKey>(
              l_accountId, l_sortKey, m_version.TimeConfiguration, m_periodList[j], m_version.Id);
          Values.Add(l_valueKey, p_packet.ReadDouble());
        }

        l_nbAggregation = p_packet.ReadUint32();
        TimeConfig l_aggregationTimeConfig = (TimeConfig)((byte)m_version.TimeConfiguration + 1);
        for (UInt16 j = 0; j < l_nbAggregation && j < m_aggregationPeriodList.Count; ++j)
        {
          SortKey l_sortKey = p_sortKey;
          Tuple<AccountKey, SortKey, PeriodTypeKey, PeriodKey, VersionKey> l_valueKey =
            new Tuple<AccountKey, SortKey, PeriodTypeKey, PeriodKey, VersionKey>(
              l_accountId, l_sortKey, l_aggregationTimeConfig, m_aggregationPeriodList[j], m_version.Id);
          Values.Add(l_valueKey, p_packet.ReadDouble());
        }

        UInt32 l_nbChildEntity = p_packet.ReadUint32();
        for (UInt32 j = 0; j < l_nbChildEntity; ++j)
          FillEntityData(p_packet, p_sortKey);
      }
    }

    String GetSortKey(bool p_isAxis, AxisType p_axisType, UInt32 p_value)
    {
      String key = "#"; 
        
      key += (p_isAxis) ? "t" : "f";
      key += ((byte)p_axisType).ToString();
      key += "v";
      key += p_value.ToString();
      return (key);
    }

    public static ComputeResult operator-(ComputeResult p_a, ComputeResult p_b)
    {
      ComputeResult l_result = new ComputeResult();

      foreach (KeyValuePair<Tuple<AccountKey, SortKey, PeriodTypeKey, PeriodKey, VersionKey>, double> l_pair in p_a.Values)
      {
        double l_bValue = p_b.Values[l_pair.Key];

        if (l_bValue != null)
          l_result.Values[l_pair.Key] = l_pair.Value - l_bValue;
      }
      return (l_result);
    }
  }
}
