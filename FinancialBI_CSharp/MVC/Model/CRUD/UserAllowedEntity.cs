using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  public class UserAllowedEntity : CRUDEntity, IComparable
  {
    public UInt32 Id { get; private set; }
    public UInt32 UserId { get; set; }
    public UInt32 EntityId { get; set; }
    public UInt32 Image { get; set; }

    public UserAllowedEntity() { }
    private UserAllowedEntity(UInt32 p_id)
    {
      Id = p_id;
    }

    public static UserAllowedEntity BuildUserAllowedEntity(ByteBuffer p_packet)
    {
      UserAllowedEntity l_entity = new UserAllowedEntity(p_packet.ReadUint32());

      l_entity.UserId = p_packet.ReadUint32();
      l_entity.EntityId = p_packet.ReadUint32();

      return (l_entity);
    }

    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
      if (p_includeId)
        p_packet.WriteUint32(Id);
      p_packet.WriteUint32(UserId);
      p_packet.WriteUint32(EntityId);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      UserAllowedEntity l_cmpEntity = p_obj as UserAllowedEntity;

      if (l_cmpEntity == null)
        return 0;
      if (l_cmpEntity.UserId > UserId)
        return -1;
      else
        return 1;
    }
  }
}
