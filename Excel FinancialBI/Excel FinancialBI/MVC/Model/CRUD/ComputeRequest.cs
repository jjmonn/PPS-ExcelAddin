using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  using Network;

  class ComputeRequest
  {
    public UInt32 VersionId { get; set; }
    public UInt32 GlobalFactVersionId { get; set; }
    public UInt32 RateVersionId { get; set; }
    public UInt32 EntityId { get; set; }
    public UInt32 CurrencyId { get; set; }
    public UInt32 StartPeriod { get; set; }
    public UInt32 NbPeriods { get; set; }
    public bool AxisHierarchy { get; set; }
    public List<UInt32> AccountList { get; set; }
    public List<Tuple<AxisType, UInt32, UInt32>> FilterList { get; set; }
    public List<Tuple<AxisType, UInt32>> AxisElemList { get; set; }
    public List<Tuple<AxisType, UInt32>> FilterSortList { get; set; }
    public List<AxisType> AxisSortList { get; set; }

    public void Dump(ByteBuffer p_packet)
    {
      p_packet.WriteUint32(VersionId);
      p_packet.WriteUint32(GlobalFactVersionId);
      p_packet.WriteUint32(RateVersionId);
      p_packet.WriteUint32(EntityId);
      p_packet.WriteUint32(CurrencyId);
      p_packet.WriteUint32(StartPeriod);
      p_packet.WriteUint32(NbPeriods);
      p_packet.WriteBool(AxisHierarchy);
      p_packet.WriteBool(AxisSortList.Contains(AxisType.Entities));
      AxisSortList.Remove(AxisType.Entities);
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
      p_packet.WriteUint32((UInt32)FilterSortList.Count + (UInt32)AxisSortList.Count);
      foreach (Tuple<AxisType, UInt32> l_filterSort in FilterSortList)
      {
        p_packet.WriteInt32((Int32)l_filterSort.Item1);
        p_packet.WriteUint32(l_filterSort.Item2);
        p_packet.WriteBool(false);
      }
      foreach (AxisType l_axis in AxisSortList)
      {
        p_packet.WriteInt32((Int32)l_axis);
        p_packet.WriteUint32(0);
        p_packet.WriteBool(true);
      }
      p_packet.WriteUint32((UInt32)AccountList.Count);
      foreach (UInt32 l_account in AccountList)
        p_packet.WriteUint32(l_account);
    }
  }
}
