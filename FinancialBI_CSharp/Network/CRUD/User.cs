using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD
{
  public class User : NamedCRUDEntity, IComparable
  {
    public UInt32 Id { get; private set; }
    public UInt32 GroupId { get; set; }
    public string Name { get; set; }
    public string Password { private get; set; }

    public User() { }
    private User(UInt32 p_id)
    {
      Id = p_id;
    }

    public static User BuildUser(ByteBuffer p_packet)
    {
      User l_user = new User(p_packet.ReadUint32());

      l_user.Name = p_packet.ReadString();
      l_user.GroupId = p_packet.ReadUint32();

      return (l_user);
    }

    public void Dump(ByteBuffer p_packet, bool p_includeId)
    {
      if (p_includeId)
        p_packet.WriteUint32(Id);
      p_packet.WriteUint32(GroupId);
      p_packet.WriteString(Name);
    }

    public void DumpWithPassword(ByteBuffer p_packet, bool p_includeId)
    {
      if (p_includeId)
        p_packet.WriteUint32(Id);
      p_packet.WriteUint32(GroupId);
      p_packet.WriteString(Name);
      p_packet.WriteString(Password);
    }

    public void CopyFrom(User p_model)
    {
      GroupId = p_model.GroupId;
      Name = p_model.Name;
      Password = p_model.Password;
    }

    public User Clone()
    {
      User l_clone = new User(Id);

      l_clone.CopyFrom(this);
      return (l_clone);
    }

    public int CompareTo(object p_obj)
    {
      if (p_obj == null)
        return 0;
      User l_cmpUser = p_obj as User;

      if (l_cmpUser == null)
        return 0;
      if (l_cmpUser.GroupId > GroupId)
        return -1;
      else
        return 1;
    }
  }
}
