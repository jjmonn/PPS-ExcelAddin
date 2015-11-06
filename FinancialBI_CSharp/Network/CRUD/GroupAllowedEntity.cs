using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD
{
  public class GroupAllowedEntity : CRUDEntity, IComparable
  {
    public UInt32 Id { get; private set; }
    public UInt32 GroupId { get; set; }
    public UInt32 EntityId { get; set; }
    public UInt32 Image { get; set; }

    public GroupAllowedEntity() { }
    private GroupAllowedEntity(UInt32 p_id)
    {
      Id = p_id;
    }

    public static GroupAllowedEntity BuildGroupAllowedEntity(ByteBuffer p_packet)
    {
      GroupAllowedEntity l_entity = new GroupAllowedEntity(p_packet.ReadUint32());

      l_entity.GroupId = p_packet.ReadUint32();
      l_entity.EntityId = p_packet.ReadUint32();

      return (l_entity);
    }

    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
      if (p_includeId)
        p_packet.WriteUint32(Id);
      p_packet.WriteUint32(GroupId);
      p_packet.WriteUint32(EntityId);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      GroupAllowedEntity l_cmpEntity = p_obj as GroupAllowedEntity;

      if (l_cmpEntity == null)
        return 0;
      if (l_cmpEntity.GroupId > GroupId)
        return -1;
      else
        return 1;
    }
  }
}
