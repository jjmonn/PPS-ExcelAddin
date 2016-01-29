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

  class AxisElemModel : AxedCRUDModel<AxisElem>
  {
    static AxisElemModel s_instance = new AxisElemModel();
    public static AxisElemModel Instance { get { return (s_instance); } }

    AxisElemModel()
    {
      CreateCMSG = ClientMessage.CMSG_CREATE_AXIS_ELEM;
      ReadCMSG = ClientMessage.CMSG_READ_AXIS_ELEM;
      UpdateCMSG = ClientMessage.CMSG_UPDATE_AXIS_ELEM;
      UpdateListCMSG = ClientMessage.CMSG_CRUD_AXIS_ELEM_LIST;
      DeleteCMSG = ClientMessage.CMSG_DELETE_AXIS_ELEM;
      ListCMSG = ClientMessage.CMSG_LIST_AXIS_ELEM;

      CreateSMSG = ServerMessage.SMSG_CREATE_AXIS_ELEM_ANSWER;
      ReadSMSG = ServerMessage.SMSG_READ_AXIS_ELEM_ANSWER;
      UpdateSMSG = ServerMessage.SMSG_UPDATE_AXIS_ELEM_ANSWER;
      UpdateListSMSG = ServerMessage.SMSG_CRUD_AXIS_ELEM_LIST_ANSWER;
      DeleteSMSG = ServerMessage.SMSG_DELETE_AXIS_ELEM_ANSWER;
      ListSMSG = ServerMessage.SMSG_LIST_AXIS_ELEM_ANSWER;

      Build = AxisElem.BuildAxis;

      InitCallbacks();
    }

    #region Utilities

    public UInt32 CountChildren(AxisType p_axis, UInt32 p_id)
    {
      UInt32 count = 1;
      AxisElem parent = GetValue(p_axis, p_id);
      MultiIndexDictionary<uint, string, AxisElem> dictionary = GetDictionary(p_axis);

      if (parent == null || dictionary == null)
        return (count);
      foreach (AxisElem elem in dictionary.Values)
        if (elem.ParentId == parent.Id)
          count += CountChildren(p_axis, elem.Id);
      return (count);
    }

    public bool IsNameValid(ref string name)
    {
/*
      if (name.Length > NAMES_MAX_LENGTH)
        return false;
      foreach (char char_ in AXIS_NAME_FORBIDEN_CHARS)
      {
        if (name.Contains(char_))
          return false;
      }
      if (string.IsNullOrEmpty(name))
        return false;
      foreach (AxisType axis in m_CRUDDic.Keys)
      {
        if ((m_CRUDDic[axis][name] != null))
          return false;
      }*/
      return true;
    }

    #endregion
  }
}
