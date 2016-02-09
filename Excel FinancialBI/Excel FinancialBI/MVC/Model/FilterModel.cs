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
    public uint FindChildId(AxisType p_axisType, uint p_id)
    {
      MultiIndexDictionary<uint, string, Filter> l_filterDic = this.GetDictionary(p_axisType);
      foreach (Filter l_filter in l_filterDic.Values)
      {
        if (l_filter.ParentId == p_id)
          return (l_filter.Id);
      }
      return (0);
    }

    public List<Filter> GetSortedByParentsDictionary(AxisType p_axisType)
    {
      MultiIndexDictionary<uint, string, Filter> l_filterDic = this.GetDictionary(p_axisType);
      List<Filter> l_newFilterDic = new List<Filter>();
      
      foreach (Filter l_filter in l_filterDic.Values)
      {
        SortedByParentsDictionaryAddElem(l_filter, l_newFilterDic, l_filterDic);
      }
      return (l_newFilterDic);
    }

    private void SortedByParentsDictionaryAddElem(Filter p_filter, List<Filter> p_newFilterDic, MultiIndexDictionary<uint, string, Filter> p_filterDic)
    {
      if (p_newFilterDic.Contains(p_filter) == false)
      {
        p_newFilterDic.Add(p_filter);
      }
      if (p_filter.IsParent == true)
        foreach (Filter l_toAdd in p_filterDic.Values)
        {
          if (l_toAdd.ParentId == p_filter.Id)
            SortedByParentsDictionaryAddElem(l_toAdd, p_newFilterDic, p_filterDic);
        }
    }

    public Filter GetChild(UInt32 p_filterId, AxisType p_axisType)
    {
      foreach (Filter l_filter in this.GetDictionary(p_axisType).Values)
      {
        if (l_filter.ParentId == p_filterId)
          return (l_filter);
      }
      return (null);
    }

    public void GetChildrenDictionary(UInt32 p_filterId, MultiIndexDictionary<UInt32, string, Filter> p_childrenDic)
    {
      MultiIndexDictionary<UInt32, string, Filter> l_resultDic = new MultiIndexDictionary<uint, string, Filter>();

      foreach (MultiIndexDictionary<UInt32, string, Filter> l_dic in this.m_CRUDDic.Values)
        foreach (Filter l_fv in l_dic.Values)
          if (l_fv.ParentId == p_filterId)
          {
            p_childrenDic.Set(l_fv.Id, l_fv.Name, l_fv);
            GetChildrenDictionary(l_fv.Id, p_childrenDic);
          }
    }
  }
}
