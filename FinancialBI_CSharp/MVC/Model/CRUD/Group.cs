using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  public class Group : NamedHierarchyCRUDEntity, IComparable
  {
    #region Enum
    public enum Permission
    {
      NONE = 0x0000000000000000,
      EDIT_FACTS = 0x0000000000000001,
      EDIT_USERS = 0x0000000000000002,
      CREATE_USERS = 0x0000000000000004,
      DELETE_USERS = 0x0000000000000008,
      EDIT_GROUPS = 0x0000000000000010,
      CREATE_GROUPS = 0x0000000000000020,
      DELETE_GROUPS = 0x0000000000000040,
      EDIT_AXIS = 0x0000000000000080,
      CREATE_AXIS = 0x0000000000000100,
      DELETE_AXIS = 0x0000000000000200,
      EDIT_ACCOUNT = 0x0000000000002000,
      CREATE_ACCOUNT = 0x0000000000004000,
      DELETE_ACCOUNT = 0x0000000000008000,
      EDIT_CURRENCIES = 0x0000000000010000,
      READ = 0x0000000000020000,
      EDIT_GFACTS = 0x0000000000040000,
      CREATE_GFACTS = 0x0000000000080000,
      DELETE_GFACTS = 0x0000000000100000,
      EDIT_BASE = 0x0000000000200000,
      EDIT_RATES = 0x0000000000400000,
      COMMIT = 0X0000000000800000,
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
