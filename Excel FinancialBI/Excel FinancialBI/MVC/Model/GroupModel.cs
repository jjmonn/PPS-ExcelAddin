using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using CRUD;
  using Network;
  using Utils;

  class GroupModel : NamedCRUDModel<Group>
  {
    static GroupModel s_instance = new GroupModel();
    public static GroupModel Instance { get { return (s_instance); } }
    GroupModel()
    {
      CreateCMSG = ClientMessage.CMSG_CREATE_GROUP;
      ReadCMSG = ClientMessage.CMSG_READ_GROUP;
      UpdateCMSG = ClientMessage.CMSG_UPDATE_GROUP;
      DeleteCMSG = ClientMessage.CMSG_DELETE_GROUP;
      ListCMSG = ClientMessage.CMSG_LIST_GROUPS;

      CreateSMSG = ServerMessage.SMSG_CREATE_GROUP_ANSWER;
      ReadSMSG = ServerMessage.SMSG_READ_GROUP_ANSWER;
      UpdateSMSG = ServerMessage.SMSG_UPDATE_GROUP_ANSWER;
      DeleteSMSG = ServerMessage.SMSG_DELETE_GROUP_ANSWER;
      ListSMSG = ServerMessage.SMSG_LIST_GROUPS;

      Build = Group.BuildGroup;

      InitCallbacks();
    }

    #region Utilities

    public UInt64 GetRight(UInt32 p_groupId)
    {
      Group group = GetValue(p_groupId);

      if (group == null)
        return (0);
      return (group.Rights);
    }

    public UInt64 GetInheritedRight(UInt32 p_groupId)
    {
      Group group = GetValue(p_groupId);

      if (group == null)
        return (0);
      return (group.Rights | GetInheritedRight(group.ParentId));
    }

    public bool HasRight(UInt32 p_groupId, Group.Permission p_permission)
    {
      Group group = GetValue(p_groupId);

      if (group == null)
        return (false);
      if ((group.Rights & (UInt64)p_permission) != 0)
        return (true);
      return (HasRight(group.ParentId, p_permission));
    }

    #endregion
  }
}
