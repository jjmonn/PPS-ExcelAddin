using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD
{
  public enum AxisType : byte
  {
    Entities = 1,
    Client,
    Product,
    Adjustment,
  }

  public class AxisElem : NamedCRUDEntity, AxedCRUDEntity, IComparable
  {
    public UInt32 Id { get; private set; }
    public string Name { get; set; }
    public Int32 ItemPosition { get; set; }
    public AxisType Axis { get; set; }

    public AxisElem() { }
    private AxisElem(UInt32 p_id)
    {
      Id = p_id;
    }

    public static AxisElem BuildAxis(ByteBuffer p_packet)
    {
      AxisElem l_account = new AxisElem(p_packet.ReadUint32());

      l_account.Name = p_packet.ReadString();
      l_account.Axis = (AxisType)p_packet.ReadUint32();
      l_account.ItemPosition = p_packet.ReadInt32();

      return (l_account);
    }

    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
      if (p_includeId)
        p_packet.WriteUint32(Id);
      p_packet.WriteString(Name);
      p_packet.WriteUint32((UInt32)Axis);
      p_packet.WriteInt32(ItemPosition);
    }

    public void CopyFrom(AxisElem p_model)
    {
      Name = p_model.Name;
      ItemPosition = p_model.ItemPosition;
    }

    public AxisElem Clone()
    {
      AxisElem l_clone = new AxisElem(Id);

      l_clone.CopyFrom(this);
      return (l_clone);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      AxisElem l_cmpAxis = p_obj as AxisElem;

      if (l_cmpAxis == null)
        return 0;
      if (l_cmpAxis.ItemPosition > ItemPosition)
        return -1;
      else
        return 1;
    }
  }
}