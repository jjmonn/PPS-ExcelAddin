using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD
{
  public class Entity : NamedHierarchyCRUDEntity, IComparable
  {
    public UInt32 Id { get; private set; }
    public UInt32 ParentId { get; set; }
    public UInt32 CurrencyId { get; set; }
    public string Name { get; set; }
    public Int32 ItemPosition { get; set; }
    public bool AllowEdition { get; set; }
    public Int32 Image { get; set; }

    public Entity() { }
    private Entity(UInt32 p_id)
    {
      Id = p_id;
    }

    public static Entity BuildEntity(ByteBuffer p_packet)
    {
      Entity l_entity = new Entity(p_packet.ReadUint32());

      l_entity.ParentId = p_packet.ReadUint32();
      l_entity.CurrencyId = p_packet.ReadUint32();
      l_entity.Name = p_packet.ReadString();
      l_entity.ItemPosition = p_packet.ReadInt32();
      l_entity.AllowEdition = p_packet.ReadBool();

      return (l_entity);
    }

    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
      if (p_includeId)
        p_packet.WriteUint32(Id);
      p_packet.WriteUint32(ParentId);
      p_packet.WriteUint32(CurrencyId);
      p_packet.WriteString(Name);
      p_packet.WriteInt32(ItemPosition);
      p_packet.WriteBool(AllowEdition);
    }

    public void CopyFrom(Entity p_model)
    {
      ParentId = p_model.ParentId;
      CurrencyId = p_model.CurrencyId;
      Name = p_model.Name;
      ItemPosition = p_model.ItemPosition;
      AllowEdition = p_model.AllowEdition;
      Image = p_model.Image;
    }

    public Entity Clone()
    {
      Entity l_clone = new Entity(Id);

      l_clone.CopyFrom(this);
      return (l_clone);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      Entity l_cmpEntity = p_obj as Entity;

      if (l_cmpEntity == null)
        return 0;
      if (l_cmpEntity.ItemPosition > ItemPosition)
        return -1;
      else
        return 1;
    }
  }
}
