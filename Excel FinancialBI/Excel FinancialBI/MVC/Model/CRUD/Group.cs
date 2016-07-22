using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  using Network;

  public class Group : NamedHierarchyCRUDEntity, IComparable
  {
    #region Enum
    public enum Permission : uint
    {
      NONE = 0x00000000,
      EDIT_FACTS = 0x00000001,
      EDIT_USERS = 0x00000002,
      CREATE_USERS = 0x00000004,
      DELETE_USERS = 0x00000008,
      EDIT_GROUPS = 0x00000010,
      CREATE_GROUPS = 0x00000020,
      DELETE_GROUPS = 0x00000040,
      EDIT_AXIS = 0x00000080,
      CREATE_AXIS = 0x00000100,
      DELETE_AXIS = 0x00000200,
      EDIT_ACCOUNT = 0x00002000,
      CREATE_ACCOUNT = 0x00004000,
      DELETE_ACCOUNT = 0x00008000,
      EDIT_CURRENCIES = 0x00010000,
      READ = 0x00020000,
      EDIT_GFACTS = 0x00040000,
      CREATE_GFACTS = 0x00080000,
      DELETE_GFACTS = 0x00100000,
      EDIT_BASE = 0x00200000,
      EDIT_RATES = 0x00400000,
      COMMIT = 0X00800000,
      VIEW_ALL_ENTITIES = 0x01000000,
      SAVE_CHARTS = 0x02000000,
      SUPER_ADMIN = 0x80000000
    };
  #endregion

    public UInt32 Id { get; private set; }
    public UInt32 ParentId { get; set; }
    public string Name { get; set; }
    public UInt64 Rights { get; set; }
    public UInt32 Image { get; set; }
    
    public Group() { }
    private Group(UInt32 p_id)
    {
      Id = p_id;
    }

    public static Group BuildGroup(ByteBuffer p_packet)
    {
      Group l_group = new Group(p_packet.ReadUint32());

      l_group.ParentId = p_packet.ReadUint32();
      l_group.Name = p_packet.ReadString();
      l_group.Rights = p_packet.ReadUint64();

      return (l_group);
    }

    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
      if (p_includeId)
      p_packet.WriteUint32(Id);
      p_packet.WriteUint32(ParentId);
      p_packet.WriteString(Name);
      p_packet.WriteUint64(Rights);
    }

    public void CopyFrom(Group p_model)
    {
      ParentId = p_model.ParentId;
      Name = p_model.Name;
      Rights = p_model.Rights;
    }

    public Group Clone()
    {
      Group l_clone = new Group(Id);

      l_clone.CopyFrom(this);
      return (l_clone);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      Group l_cmpGroup = p_obj as Group;

      if (l_cmpGroup == null)
        return 0;
      if (l_cmpGroup.Id > Id)
        return -1;
      else
        return 1;
    }
  }
}
