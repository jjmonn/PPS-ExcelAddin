using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using CRUD;

  class UserModel : NamedCRUDModel<User>
  {
    static UserModel s_instance = new UserModel();
    public static UserModel Instance { get { return (s_instance); } }
    public string CurrentUserName;

    UserModel()
    {
      CreateCMSG = ClientMessage.CMSG_CREATE_USER;
      ReadCMSG = ClientMessage.CMSG_READ_USER;
      UpdateCMSG = ClientMessage.CMSG_UPDATE_USER;
      DeleteCMSG = ClientMessage.CMSG_DELETE_USER;
      ListCMSG = ClientMessage.CMSG_LIST_USERS;

      CreateSMSG = ServerMessage.SMSG_CREATE_USER_ANSWER;
      ReadSMSG = ServerMessage.SMSG_READ_USER_ANSWER;
      UpdateSMSG = ServerMessage.SMSG_UPDATE_USER_ANSWER;
      DeleteSMSG = ServerMessage.SMSG_DELETE_USER_ANSWER;
      ListSMSG = ServerMessage.SMSG_LIST_USERS;

      Build = User.BuildUser;

      InitCallbacks();
    }

    #region Utilities

    public User GetCurrentUser()
    {
      return (m_CRUDDic[CurrentUserName]);
    }

    public UInt64 GetCurrentUserRights()
    {
      User user = GetCurrentUser();

      if (user == null)
        return ((UInt64)Group.Permission.NONE);
      return (GroupModel.Instance.GetInheritedRight(user.GroupId));
    }

    public bool CurrentUserHasRight(Group.Permission p_right)
    {
      User user = GetCurrentUser();

      if (user == null)
        return (false);
      return (GroupModel.Instance.HasRight(user.GroupId, p_right));
    }

    public bool CurrentUserHasProcess(Account.AccountProcess p_process)
    {
      User user = GetCurrentUser();

      if (user == null)
        return (false);
      return ((user.ProcessFlag & p_process) != 0);
    }

    #endregion
  }
}
