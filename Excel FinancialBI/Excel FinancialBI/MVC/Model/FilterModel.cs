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

    public MultiIndexDictionary<uint, string, Filter> GetSortedByParentsDictionary(AxisType p_axisType)
    {
      MultiIndexDictionary<uint, string, Filter> l_filterDic = this.GetDictionary(p_axisType);
      MultiIndexDictionary<uint, string, Filter> l_newFilterDic = new MultiIndexDictionary<uint, string, Filter>();
      
      foreach (Filter l_filter in l_filterDic.Values)
      {
        SortedByParentsDictionaryAddElem(l_filter, l_newFilterDic, l_filterDic);
      }
      return (l_filterDic);
    }

    private void SortedByParentsDictionaryAddElem(Filter p_filter, MultiIndexDictionary<uint, string, Filter> p_newFilterDic, MultiIndexDictionary<uint, string, Filter> p_filterDic)
    {
      if (p_newFilterDic.ContainsKey(p_filter.Id) == false)
      {
        p_newFilterDic.Set(p_filter.Id, p_filter.Name, p_filter);
      }
      if (p_filter.IsParent == true)
        foreach (Filter l_toAdd in p_filterDic.Values)
        {
          if (l_toAdd.ParentId == p_filter.Id)
            SortedByParentsDictionaryAddElem(l_toAdd, p_newFilterDic, p_filterDic);
        }
    }
  }
}
