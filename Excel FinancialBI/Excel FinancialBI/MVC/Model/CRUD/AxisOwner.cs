using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  using Network;

  public class AxisOwner : CRUDEntity, IComparable
  {
    public UInt32 Id { get; set; }
    public UInt32 OwnerId { get; set; }
    public UInt32 Image { get; set; }

    public AxisOwner() { }
    private AxisOwner(UInt32 p_axisId, UInt32 p_ownerId)
    {
      Id = p_axisId;
      OwnerId = p_ownerId;
    }

    public static AxisOwner BuildAxisOwner(ByteBuffer p_packet)
    {
      AxisOwner l_axis = new AxisOwner(p_packet.ReadUint32(), p_packet.ReadUint32());

      return (l_axis);
    }

    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
      p_packet.WriteUint32(Id);
      p_packet.WriteUint32(OwnerId);
    }

    public AxisOwner Clone()
    {
      AxisOwner l_clone = new AxisOwner(Id, OwnerId);

      return (l_clone);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      AxisOwner l_cmpaxis = p_obj as AxisOwner;

      if (l_cmpaxis == null)
        return 0;
      if (l_cmpaxis.Id > Id)
        return -1;
      else
        return 1;
    }
  }
}
