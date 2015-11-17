using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD
{
  public class EntityDistribution : CRUDEntity, IComparable
  {
    public UInt32 Id { get; private set; }
    public UInt32 EntityId { get; set; }
    public UInt32 AccountId { get; set; }
    public double Percentage { get; set; }
    public UInt32 Image { get; set; }

    public EntityDistribution() { }
    private EntityDistribution(UInt32 p_id)
    {
      Id = p_id;
    }

    public static EntityDistribution BuildEntityDistribution(ByteBuffer p_packet)
    {
      EntityDistribution l_entity = new EntityDistribution(p_packet.ReadUint32());

      l_entity.EntityId = p_packet.ReadUint32();
      l_entity.AccountId = p_packet.ReadUint32();
      l_entity.Percentage = p_packet.ReadDouble();

      return (l_entity);
    }

    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
      if (p_includeId)
        p_packet.WriteUint32(Id);
      p_packet.WriteUint32(EntityId);
      p_packet.WriteUint32(AccountId);
      p_packet.WriteDouble(Percentage);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      EntityDistribution l_cmpEntity = p_obj as EntityDistribution;

      if (l_cmpEntity == null)
        return 0;
      if (l_cmpEntity.EntityId > EntityId)
        return -1;
      else
        return 1;
    }
  }
}
