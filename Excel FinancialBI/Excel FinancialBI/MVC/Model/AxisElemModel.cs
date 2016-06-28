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

    public string GetValueName(UInt32 p_id)
    {
      AxisElem l_axisElem = GetValue(p_id);

      if (l_axisElem == null)
        return ("");
      return (l_axisElem.Name);
    }

    public UInt32 GetValueId(string p_name)
    {
      AxisElem l_axisElem = GetValue(p_name);

      if (l_axisElem == null)
        return (0);
      return (l_axisElem.Id);
    }

    #region Utilities

    public UInt32 CountChildren(AxisType p_axis, UInt32 p_id)
    {
      UInt32 count = 1;
      AxisElem parent = GetValue(p_axis, p_id);
      MultiIndexDictionary<UInt32, string, AxisElem> dictionary = GetDictionary(p_axis);

      if (parent == null || dictionary == null)
        return (count);
      foreach (AxisElem elem in dictionary.Values)
        if (elem.ParentId == parent.Id)
          count += CountChildren(p_axis, elem.Id);
      return (count);
    }

    public List<AxisElem> GetChildren(AxisType p_axisType, UInt32 p_parentId, bool p_includeParent = false)
    {
      List<AxisElem> l_list = new List<AxisElem>();

      if (p_includeParent)
      {
        AxisElem l_parent = GetValue(p_parentId);

        if (l_parent != null)
          l_list.Add(l_parent);
      }
      foreach (AxisElem l_elem in this.GetDictionary(p_axisType).Values)
      {
        if (l_elem.ParentId == p_parentId)
          l_list.Add(l_elem);
      }
      return (l_list);
    }

    public List<AxisElem> GetChildrenRecurse(AxisType p_axisType, UInt32 p_parentId, bool p_includeParent = false)
    {
      List<AxisElem> l_list = GetChildren(p_axisType, p_parentId, false);
      List<AxisElem> l_resultList = new List<AxisElem>();

      foreach (AxisElem l_elem in l_list)
        l_resultList.AddRange(GetChildrenRecurse(p_axisType, l_elem.Id));
      l_resultList.AddRange(l_list);
      if (p_includeParent)
      {
        AxisElem l_parent = GetValue(p_parentId);

        if (l_parent != null)
          l_resultList.Add(l_parent);
      }
      return (l_resultList);
    }

    public bool IsParent(AxisType p_axisType, UInt32 p_id)
    {
      foreach (AxisElem l_elem in this.GetDictionary(p_axisType).Values)
      {
        if (l_elem.ParentId == p_id)
          return (true);
      }
      return (false);
    }


    public bool IsParent(UInt32 p_axisElemId)
    {
      foreach (MultiIndexDictionary<UInt32, string, AxisElem> l_axisElemDic in m_CRUDDic.Values)
        foreach (AxisElem l_axisElem in l_axisElemDic.Values)
          if (l_axisElem.ParentId == p_axisElemId)
            return (true);
      return (false);
    }

    public AxisElem GetTopEntity()
    {
      foreach (AxisElem l_entity in GetDictionary(AxisType.Entities).Values)
        if (l_entity.ParentId == 0)
          return (l_entity);
      return (null);
    }

    #endregion
  }
}
