using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  using Network;

  public class LegacyComputeRequest : AComputeRequest
  {
    public UInt32 EntityId { get; set; }
    public bool AxisHierarchy { get; set; }
    public List<Tuple<AxisType, UInt32, UInt32>> FilterList { get; set; }
    public List<Tuple<AxisType, UInt32>> AxisElemList { get; set; }
    public List<Tuple<bool, AxisType, UInt32>> SortList { get; set; }
    public List<UInt32> Versions { get; set; }
    public bool IsDiff { get; set; }
    public bool IsPeriodDiff { get; set; }
    public SafeDictionary<TimeConfig, SafeDictionary<Int32, Int32>> PeriodDiffAssociations { get; set; }
    public UInt32 MainDiffVersion { get; set; }
    public bool PeriodFilter { get; set; }
    public List<Int32> Periods;

    public LegacyComputeRequest()
    {
      Versions = new List<uint>();
      AccountList = new List<uint>();
      FilterList = new List<Tuple<AxisType, uint, uint>>();
      AxisElemList = new List<Tuple<AxisType, uint>>();
      SortList = new List<Tuple<bool, AxisType, uint>>();
      IsDiff = false;
      IsPeriodDiff = false;
      PeriodFilter = false;
      Periods = new List<int>();
      PeriodDiffAssociations = new SafeDictionary<TimeConfig, SafeDictionary<int, int>>();
      foreach (TimeConfig config in Enum.GetValues(typeof(TimeConfig)))
        PeriodDiffAssociations[config] = new SafeDictionary<int, int>();
    }

    List<int> GetPeriods(Version p_version)
    {
      TimeConfig l_parentConfig = VersionModel.Instance.GetHightestTimeConfig(Versions);
      List<int> l_periods;

      if (TimeUtils.IsParentConfig(l_parentConfig, p_version.TimeConfiguration))
      {
        l_periods = new List<int>();
        foreach (int l_period in Periods)
        {
          List<int> l_tmp = PeriodModel.GetPeriodList(l_period, TimeUtils.GetChildConfigNbPeriods(l_parentConfig, 1), p_version.TimeConfiguration);

          l_periods = l_periods.Concat(l_tmp).ToList();
        }
      }
      else
        l_periods = Periods;
      return (l_periods);
    }

    public void Dump(ByteBuffer p_packet, UInt32 p_versionId)
    {
      base.Dump(p_packet, p_versionId, EntityId);

      Version l_version = VersionModel.Instance.GetValue(p_versionId);
      List<int> l_periods = (PeriodFilter) ? GetPeriods(l_version) : null;

      p_packet.WriteBool(PeriodFilter && l_periods.Count != l_version.NbPeriod);
      if (PeriodFilter && l_periods.Count != l_version.NbPeriod)
      {
       
        p_packet.WriteInt32(l_periods.Count);
        foreach (Int32 l_period in l_periods)
        {
          Int32 l_value;

          if (IsPeriodDiff)
            l_value = (MainDiffVersion != p_versionId) ?
               PeriodDiffAssociations[l_version.TimeConfiguration].ElementAt(l_period).Key :
               PeriodDiffAssociations[l_version.TimeConfiguration].ElementAt(l_period).Value;
          else
            l_value = l_period;
          p_packet.WriteInt32(l_value);
        }
      }
      bool l_entityDecomposition = SortList.Contains(new Tuple<bool, AxisType, UInt32>(true, AxisType.Entities, 0));

      if (l_entityDecomposition)
        SortList.Remove(new Tuple<bool, AxisType, UInt32>(true, AxisType.Entities, 0));
      p_packet.WriteBool(AxisHierarchy);
      p_packet.WriteBool(l_entityDecomposition);
      p_packet.WriteUint32((UInt32)FilterList.Count);
      foreach (Tuple<AxisType, UInt32, UInt32> l_filter in FilterList)
      {
        p_packet.WriteInt32((Int32)l_filter.Item1);
        p_packet.WriteUint32((UInt32)l_filter.Item2);
        p_packet.WriteUint32((UInt32)l_filter.Item3);
      }
      p_packet.WriteUint32((UInt32)AxisElemList.Count);
      foreach (Tuple<AxisType, UInt32> l_axisElem in AxisElemList)
      {
        p_packet.WriteInt32((Int32)l_axisElem.Item1);
        p_packet.WriteUint32((UInt32)l_axisElem.Item2);
      }
      p_packet.WriteUint32((UInt32)SortList.Count);
      foreach (Tuple<bool, AxisType, UInt32> l_sort in SortList)
      {
        p_packet.WriteInt32((Int32)l_sort.Item2);
        p_packet.WriteBool(l_sort.Item1);
        if (!l_sort.Item1)
          p_packet.WriteUint32(l_sort.Item3);
      }
      if (l_entityDecomposition)
        SortList.Add(new Tuple<bool, AxisType, UInt32>(true, AxisType.Entities, 0));
    }
  }
}
