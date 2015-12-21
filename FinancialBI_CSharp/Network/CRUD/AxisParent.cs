using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD
{
  public class AxisParent : CRUDEntity, IComparable
  {
    public UInt32 Id { get; set; }
    public UInt32 ParentId { get; set; }
    public UInt32 Image { get; set; }

    public AxisParent() { }
    private AxisParent(UInt32 p_axisId, UInt32 p_parentId)
    {
      Id = p_axisId;
      ParentId = p_parentId;
    }

    public static AxisParent BuildAxisParent(ByteBuffer p_packet)
    {
      AxisParent l_axis = new AxisParent(p_packet.ReadUint32(), p_packet.ReadUint32());

      return (l_axis);
    }

    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
      p_packet.WriteUint32(Id);
      p_packet.WriteUint32(ParentId);
    }

    public AxisParent Clone()
    {
      AxisParent l_clone = new AxisParent(Id, ParentId);

      return (l_clone);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      AxisParent l_cmpaxis = p_obj as AxisParent;

      if (l_cmpaxis == null)
        return 0;
      if (l_cmpaxis.Id > Id)
        return -1;
      else
        return 1;
    }
  }
}
