using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  public class Filter : NamedHierarchyCRUDEntity, AxedCRUDEntity, IComparable
  {
    public enum Rules
    {
      NONE,
      UNIQUE_ON_PERIOD
    };

    public UInt32 Id { get; private set; }
    public UInt32 ParentId { get; set; }
    public AxisType Axis { get; set; }
    public bool IsParent { get; set; }
    public string Name { get; set; }
    public Int32 ItemPosition { get; set; }
    public UInt32 Image { get; set; }
    public Rules Rule { get; set; }

    public Filter() { }
    private Filter(UInt32 p_id)
    {
      Id = p_id;
    }

    public static Filter BuildFilter(ByteBuffer p_packet)
    {
      Filter l_filter = new Filter(p_packet.ReadUint32());

      l_filter.ParentId = p_packet.ReadUint32();
      l_filter.Axis = (AxisType)p_packet.ReadUint32();
      l_filter.IsParent = p_packet.ReadBool();
      l_filter.Name = p_packet.ReadString();
      l_filter.ItemPosition = p_packet.ReadInt32();
      l_filter.Rule = (Rules)p_packet.ReadUint8();

      return (l_filter);
    }

    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
      if (p_includeId)
        p_packet.WriteUint32(Id);
      p_packet.WriteUint32(ParentId);
      p_packet.WriteUint32((UInt32)Axis);
      p_packet.WriteBool(IsParent);
      p_packet.WriteString(Name);
      p_packet.WriteInt32(ItemPosition);
      p_packet.WriteUint8((byte)Rule);
    }

    public void CopyFrom(Filter p_model)
    {
      ParentId = p_model.ParentId;
      Axis = p_model.Axis;
      IsParent = p_model.IsParent;
      Name = p_model.Name;
      ItemPosition = p_model.ItemPosition;
      Rule = p_model.Rule;
    }

    public Filter Clone()
    {
      Filter l_clone = new Filter(Id);

      l_clone.CopyFrom(this);
      return (l_clone);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      Filter l_cmpFilter = p_obj as Filter;

      if (l_cmpFilter == null)
        return 0;
      if (l_cmpFilter.ItemPosition > ItemPosition)
        return -1;
      else
        return 1;
    }

  }
}
