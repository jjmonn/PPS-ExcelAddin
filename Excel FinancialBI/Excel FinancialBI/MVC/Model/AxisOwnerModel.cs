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

  class AxisOwnerModel : SimpleCRUDModel<AxisOwner>
  {
    static AxisOwnerModel s_instance = new AxisOwnerModel();
    public static AxisOwnerModel Instance { get { return (s_instance); } }

    AxisOwnerModel()
    {
      CreateCMSG = ClientMessage.CMSG_CREATE_AXIS_OWNER;
      ReadCMSG = ClientMessage.CMSG_READ_AXIS_OWNER;
      UpdateCMSG = ClientMessage.CMSG_UPDATE_AXIS_OWNER;
      UpdateListCMSG = ClientMessage.CMSG_CRUD_AXIS_OWNER;
      ListCMSG = ClientMessage.CMSG_LIST_AXIS_OWNER;

      CreateSMSG = ServerMessage.SMSG_CREATE_AXIS_OWNER_ANSWER;
      ReadSMSG = ServerMessage.SMSG_READ_AXIS_OWNER_ANSWER;
      UpdateSMSG = ServerMessage.SMSG_UPDATE_AXIS_OWNER_ANSWER;
      UpdateListSMSG = ServerMessage.SMSG_CRUD_AXIS_OWNER_LIST_ANSWER;
      DeleteSMSG = ServerMessage.SMSG_DELETE_AXIS_OWNER_ANSWER;
      ListSMSG = ServerMessage.SMSG_LIST_AXIS_OWNER_ANSWER;

      Build = AxisOwner.BuildAxisOwner;

      InitCallbacks();
    }
  }
}
