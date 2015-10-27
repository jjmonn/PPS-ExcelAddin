using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD
{
  class FilterValue : HierarchyCRUDEntity, IComparable
  {
    public UInt32 Id { get; private set; }
    public UInt32 FilterId { get; set; }
    public UInt32 ParentId { get; set; }
    public string Name { get; set; }
    public Int32 ItemPosition { get; set; }

    public FilterValue() { }
    private FilterValue(UInt32 p_id)
    {
      Id = p_id;
    }

    public static FilterValue BuildFilterValue(ByteBuffer p_packet)
    {
      FilterValue l_filter = new FilterValue(p_packet.ReadUint32());

      l_filter.FilterId = p_packet.ReadUint32();
      l_filter.ParentId = p_packet.ReadUint32();
      l_filter.Name = p_packet.ReadString();
      l_filter.ItemPosition = p_packet.ReadInt32();

      return (l_filter);
    }

    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
      if (p_includeId)
        p_packet.WriteUint32(Id);
      p_packet.WriteUint32(FilterId);
      p_packet.WriteUint32(ParentId);
      p_packet.WriteString(Name);
      p_packet.WriteInt32(ItemPosition);
    }

    public void CopyFrom(FilterValue p_model)
    {
      FilterId = p_model.FilterId;
      ParentId = p_model.ParentId;
      Name = p_model.Name;
      ItemPosition = p_model.ItemPosition;
    }

    public FilterValue Clone()
    {
      FilterValue l_clone = new FilterValue(Id);

      l_clone.CopyFrom(this);
      return (l_clone);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      FilterValue l_cmpFilterValue = p_obj as FilterValue;

      if (l_cmpFilterValue == null)
        return 0;
      if (l_cmpFilterValue.ItemPosition > ItemPosition)
        return -1;
      else
        return 1;
    }
  }
}
