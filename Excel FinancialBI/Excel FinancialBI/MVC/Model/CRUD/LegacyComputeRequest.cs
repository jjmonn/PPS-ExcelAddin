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

    public LegacyComputeRequest()
    {
      Versions = new List<uint>();
      AccountList = new List<uint>();
      FilterList = new List<Tuple<AxisType, uint, uint>>();
      AxisElemList = new List<Tuple<AxisType, uint>>();
      SortList = new List<Tuple<bool, AxisType, uint>>();
      IsDiff = false;
    }

    public void Dump(ByteBuffer p_packet, UInt32 p_versionId)
    {
      base.Dump(p_packet, p_versionId, EntityId);

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
