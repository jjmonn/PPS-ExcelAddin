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

  class GlobalFactVersionModel : NamedCRUDModel<GlobalFactVersion>
  {
    static GlobalFactVersionModel s_instance = new GlobalFactVersionModel();
    public static GlobalFactVersionModel Instance { get { return (s_instance); } }
    GlobalFactVersionModel()
    {
      CreateCMSG = ClientMessage.CMSG_CREATE_GLOBAL_FACT_VERSION;
      ReadCMSG = ClientMessage.CMSG_READ_GLOBAL_FACT_VERSION;
      UpdateCMSG = ClientMessage.CMSG_UPDATE_GLOBAL_FACT_VERSION;
      UpdateListCMSG = ClientMessage.CMSG_CRUD_GLOBAL_FACT_VERSION_LIST;
      DeleteCMSG = ClientMessage.CMSG_DELETE_GLOBAL_FACT_VERSION;
      ListCMSG = ClientMessage.CMSG_LIST_GLOBAL_FACT_VERSION;

      CreateSMSG = ServerMessage.SMSG_CREATE_GLOBAL_FACT_VERSION_ANSWER;
      ReadSMSG = ServerMessage.SMSG_READ_GLOBAL_FACT_VERSION_ANSWER;
      UpdateSMSG = ServerMessage.SMSG_UPDATE_GLOBAL_FACT_VERSION_ANSWER;
      UpdateListSMSG = ServerMessage.SMSG_CRUD_GLOBAL_FACT_VERSION_LIST_ANSWER;
      DeleteSMSG = ServerMessage.SMSG_DELETE_GLOBAL_FACT_VERSION_ANSWER;
      ListSMSG = ServerMessage.SMSG_LIST_GLOBAL_FACT_VERSION_ANSWER;

      Build = GlobalFactVersion.BuildGlobalFactVersion;

      InitCallbacks();
    }
  }
}
