using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  using Network;

  public class AxisFilter : CRUDEntity, AxedCRUDEntity, IComparable
  {

    public UInt32 Id { get; private set; }
    public AxisType Axis { get; set; }
    public UInt32 AxisElemId { get; set; }
    public UInt32 FilterId { get; set; }
    public UInt32 FilterValueId { get; set; }
    public UInt32 Image { get; set; }

    public AxisFilter() { }
    private AxisFilter(UInt32 p_id)
    {
      Id = p_id;
    }

    public static AxisFilter BuildAxisFilter(ByteBuffer p_packet)
    {
      AxisFilter l_axisFilter = new AxisFilter(p_packet.ReadUint32());

      l_axisFilter.Axis = (AxisType)p_packet.ReadUint32();
      l_axisFilter.AxisElemId = p_packet.ReadUint32();
      l_axisFilter.FilterId = p_packet.ReadUint32();
      l_axisFilter.FilterValueId = p_packet.ReadUint32();

      return (l_axisFilter);
    }

    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
      if (p_includeId)
        p_packet.WriteUint32(Id);
      p_packet.WriteUint32((UInt32)Axis);
      p_packet.WriteUint32(AxisElemId);
      p_packet.WriteUint32(FilterId);
      p_packet.WriteUint32(FilterValueId);
    }

    public void CopyFrom(AxisFilter p_model)
    {
      Axis = p_model.Axis;
      AxisElemId = p_model.AxisElemId;
      FilterId = p_model.FilterId;
      FilterValueId = p_model.FilterValueId;
    }

    public AxisFilter Clone()
    {
      AxisFilter l_clone = new AxisFilter(Id);

      l_clone.CopyFrom(this);
      return (l_clone);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      AxisFilter l_cmpAxisFilter = p_obj as AxisFilter;

      if (l_cmpAxisFilter == null)
        return 0;
      if (l_cmpAxisFilter.Id > Id)
        return -1;
      else
        return 1;
    }
  }
}