using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD
{
  public class EntityCurrency : CRUDEntity, IComparable
  {
    public UInt32 Id { get; private set; }
    public UInt32 CurrencyId { get; set; }

    public EntityCurrency() { }
    private EntityCurrency(UInt32 p_entityId, UInt32 p_currencyId)
    {
      Id = p_entityId;
      CurrencyId = p_currencyId;
    }

    public static EntityCurrency BuildEntityCurrency(ByteBuffer p_packet)
    {
      EntityCurrency l_entity = new EntityCurrency(p_packet.ReadUint32(), p_packet.ReadUint32());

      return (l_entity);
    }

    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
      p_packet.WriteUint32(Id);
      p_packet.WriteUint32(CurrencyId);
    }

    public EntityCurrency Clone()
    {
      EntityCurrency l_clone = new EntityCurrency(Id, CurrencyId);

      return (l_clone);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      EntityCurrency l_cmpEntity = p_obj as EntityCurrency;

      if (l_cmpEntity == null)
        return 0;
      if (l_cmpEntity.Id > Id)
        return -1;
      else
        return 1;
    }
  }
}
