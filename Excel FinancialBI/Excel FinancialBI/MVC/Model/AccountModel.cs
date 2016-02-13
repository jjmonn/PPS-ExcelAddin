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

  class AccountModel : NamedCRUDModel<Account>
  {
    static AccountModel s_instance = new AccountModel();
    public static AccountModel Instance { get { return (s_instance); } }

    AccountModel()
    {
      CreateCMSG = ClientMessage.CMSG_CREATE_ACCOUNT;
      ReadCMSG = ClientMessage.CMSG_READ_ACCOUNT;
      UpdateCMSG = ClientMessage.CMSG_UPDATE_ACCOUNT;
      UpdateListCMSG = ClientMessage.CMSG_CRUD_ACCOUNT_LIST;
      DeleteCMSG = ClientMessage.CMSG_DELETE_ACCOUNT;
      ListCMSG = ClientMessage.CMSG_LIST_ACCOUNT;

      CreateSMSG = ServerMessage.SMSG_CREATE_ACCOUNT_ANSWER;
      ReadSMSG = ServerMessage.SMSG_READ_ACCOUNT_ANSWER;
      UpdateSMSG = ServerMessage.SMSG_UPDATE_ACCOUNT_ANSWER;
      UpdateListSMSG = ServerMessage.SMSG_CRUD_ACCOUNT_LIST_ANSWER;
      DeleteSMSG = ServerMessage.SMSG_DELETE_ACCOUNT_ANSWER;
      ListSMSG = ServerMessage.SMSG_LIST_ACCOUNT_ANSWER;

      Build = Account.BuildAccount;

      InitCallbacks();
    }

  }
}
