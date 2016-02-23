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

  class GlobalFactModel : NamedCRUDModel<GlobalFact>
  {
    static GlobalFactModel s_instance = new GlobalFactModel();
    public static GlobalFactModel Instance { get { return (s_instance); } }
    GlobalFactModel()
    {
      CreateCMSG = ClientMessage.CMSG_CREATE_GLOBAL_FACT;
      ReadCMSG = ClientMessage.CMSG_READ_GLOBAL_FACT;
      UpdateCMSG = ClientMessage.CMSG_UPDATE_GLOBAL_FACT;
      UpdateListCMSG = ClientMessage.CMSG_CRUD_GLOBAL_FACT_LIST;
      DeleteCMSG = ClientMessage.CMSG_DELETE_GLOBAL_FACT;
      ListCMSG = ClientMessage.CMSG_LIST_GLOBAL_FACT;

      CreateSMSG = ServerMessage.SMSG_CREATE_GLOBAL_FACT_ANSWER;
      ReadSMSG = ServerMessage.SMSG_READ_GLOBAL_FACT_ANSWER;
      UpdateSMSG = ServerMessage.SMSG_UPDATE_GLOBAL_FACT_ANSWER;
      UpdateListSMSG = ServerMessage.SMSG_CRUD_GLOBAL_FACT_LIST_ANSWER;
      DeleteSMSG = ServerMessage.SMSG_DELETE_GLOBAL_FACT_ANSWER;
      ListSMSG = ServerMessage.SMSG_LIST_GLOBAL_FACT_ANSWER;

      Build = GlobalFact.BuildGlobalFact;

      InitCallbacks();
    }
  }
}
