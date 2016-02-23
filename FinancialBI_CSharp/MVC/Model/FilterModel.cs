using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using CRUD;

  class FilterModel : AxedCRUDModel<Filter>
  {
    static FilterModel s_instance = new FilterModel();
    public static FilterModel Instance { get { return (s_instance); } }

    FilterModel()
    {
      CreateCMSG = ClientMessage.CMSG_CREATE_FILTER;
      ReadCMSG = ClientMessage.CMSG_READ_FILTER;
      UpdateCMSG = ClientMessage.CMSG_UPDATE_FILTER;
      UpdateListCMSG = ClientMessage.CMSG_CRUD_FILTER_LIST;
      DeleteCMSG = ClientMessage.CMSG_DELETE_FILTER;
      ListCMSG = ClientMessage.CMSG_LIST_FILTER;

      CreateSMSG = ServerMessage.SMSG_CREATE_FILTER_ANSWER;
      ReadSMSG = ServerMessage.SMSG_READ_FILTER_ANSWER;
      UpdateSMSG = ServerMessage.SMSG_UPDATE_FILTER_ANSWER;
      UpdateListSMSG = ServerMessage.SMSG_CRUD_FILTER_LIST_ANSWER;
      DeleteSMSG = ServerMessage.SMSG_DELETE_FILTER_ANSWER;
      ListSMSG = ServerMessage.SMSG_LIST_FILTER_ANSWER;

      Build = Filter.BuildFilter;

      InitCallbacks();
    }
  }
}
