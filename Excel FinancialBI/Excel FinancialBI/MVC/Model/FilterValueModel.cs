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

  class FilterValueModel : ICRUDModel<FilterValue>
  {
    static FilterValueModel s_instance = new FilterValueModel();
    public static FilterValueModel Instance { get { return (s_instance); } }
    private SortedDictionary<UInt32, MultiIndexDictionary<UInt32, string, FilterValue>> m_filterValuesDic = new SortedDictionary<uint, MultiIndexDictionary<uint, string, FilterValue>>();

    FilterValueModel()
    {
      CreateCMSG = ClientMessage.CMSG_CREATE_FILTER_VALUE;
      ReadCMSG = ClientMessage.CMSG_READ_FILTER_VALUE;
      UpdateCMSG = ClientMessage.CMSG_UPDATE_FILTER_VALUE;
      UpdateListCMSG = ClientMessage.CMSG_CRUD_FILTER_VALUE_LIST;
      DeleteCMSG = ClientMessage.CMSG_DELETE_FILTER_VALUE;
      ListCMSG = ClientMessage.CMSG_LIST_FILTER_VALUE;

      CreateSMSG = ServerMessage.SMSG_CREATE_FILTERS_VALUE_ANSWER;
      ReadSMSG = ServerMessage.SMSG_READ_FILTERS_VALUE_ANSWER;
      UpdateSMSG = ServerMessage.SMSG_UPDATE_FILTERS_VALUE_ANSWER;
      UpdateListSMSG = ServerMessage.SMSG_CRUD_FILTER_VALUE_LIST_ANSWER;
      DeleteSMSG = ServerMessage.SMSG_DELETE_FILTERS_VALUE_ANSWER;
      ListSMSG = ServerMessage.SMSG_LIST_FILTER_VALUE_ANSWER;

      Build = FilterValue.BuildFilterValue;

      InitCallbacks();
    }

    #region CRUD

    protected override void ListAnswer(ByteBuffer p_packet)
    {
      if (p_packet.GetError() == ErrorMessage.SUCCESS)
      {
        m_filterValuesDic.Clear();
        UInt32 count = p_packet.ReadUint32();
        for (UInt32 i = 1; i <= count; i++)
        {
          FilterValue filterValue = Build(p_packet) as FilterValue;

          if (m_filterValuesDic.ContainsKey(filterValue.FilterId) == false)
            m_filterValuesDic[filterValue.FilterId] = new MultiIndexDictionary<UInt32, string, FilterValue>();
          m_filterValuesDic[filterValue.FilterId].Set(filterValue.Id, filterValue.Name, filterValue);
        }
        IsInit = true;
        RaiseObjectInitializedEvent(p_packet.GetError(), typeof(FilterValue));
      }
      else
        IsInit = false;
    }


    protected override void ReadAnswer(ByteBuffer p_packet)
    {
      if (p_packet.GetError() == ErrorMessage.SUCCESS)
      {
        FilterValue filterValue = Build(p_packet) as FilterValue;

        if (m_filterValuesDic.ContainsKey(filterValue.FilterId) == false)
          m_filterValuesDic[filterValue.FilterId] = new MultiIndexDictionary<uint, string, FilterValue>();
        m_filterValuesDic[filterValue.FilterId].Set(filterValue.Id, filterValue.Name, filterValue);
        RaiseReadEvent(p_packet.GetError(), filterValue);
      }
      else
        RaiseReadEvent(p_packet.GetError(), null);
    }

    protected override void DeleteAnswer(ByteBuffer p_packet)
    {
      UInt32 id = p_packet.ReadUint32();

      if (p_packet.GetError() == ErrorMessage.SUCCESS)
        foreach (MultiIndexDictionary<UInt32, string, FilterValue> filterValueDic in m_filterValuesDic.Values)
          if (filterValueDic.ContainsKey(id))
          {
            filterValueDic.Remove(id);
            break;
          }
      RaiseDeleteEvent(p_packet.GetError(), id);
    }

    #endregion

    #region Mapping

    public MultiIndexDictionary<UInt32, string, FilterValue> GetDictionary(UInt32 p_filterId)
    {
      if (m_filterValuesDic.ContainsKey(p_filterId) == false)
        return null;
      return (m_filterValuesDic[p_filterId]);
    }

    public MultiIndexDictionary<UInt32, string, FilterValue> GetChildrenDictionary(UInt32 p_filterValueId)
    {
      MultiIndexDictionary<UInt32, string, FilterValue> l_resultDic = new MultiIndexDictionary<uint,string,FilterValue>();

      foreach (MultiIndexDictionary<UInt32, string, FilterValue> l_dic in m_filterValuesDic.Values)
        foreach (FilterValue l_fv in l_dic.Values)
          if (l_fv.ParentId == p_filterValueId)
            l_resultDic.Set(l_fv.Id, l_fv.Name, l_fv);
      return (l_resultDic);
    }

    public void GetChildrenDictionary(UInt32 p_filterValueId, MultiIndexDictionary<UInt32, string, FilterValue> p_childrenDic)
    {
      MultiIndexDictionary<UInt32, string, Filter> l_resultDic = new MultiIndexDictionary<uint, string, Filter>();

      foreach (dynamic l_fvDic in m_filterValuesDic.Values)
        foreach (FilterValue l_fv in l_fvDic.Values)
          if (l_fv.ParentId == p_filterValueId)
          {
            p_childrenDic.Set(l_fv.Id, l_fv.Name, l_fv);
            GetChildrenDictionary(l_fv.Id, p_childrenDic);
          }
    }

    public SortedDictionary<UInt32, MultiIndexDictionary<UInt32, string, FilterValue>> GetDictionary()
    {
      return (m_filterValuesDic);
    }

    public UInt32 GetValueId(string p_name)
    {
      FilterValue filterValue = GetValue(p_name);

      if (filterValue == null)
        return (0);
      return (filterValue.Id);

    }

    public string GetValueName(UInt32 p_id)
    {

      FilterValue filterValue = GetValue(p_id);

      if (filterValue == null)
        return ("");
      return (filterValue.Name);

    }

    public FilterValue GetValue(string name)
    {
      foreach (MultiIndexDictionary<UInt32, string, FilterValue> filterValueSet in m_filterValuesDic.Values)
      {
        FilterValue filterValue = filterValueSet[name];

        if ((filterValue != null))
          return (filterValue);
      }
      return (null);
    }

    public override FilterValue GetValue(UInt32 p_id)
    {
      foreach (MultiIndexDictionary<UInt32, string, FilterValue> filterValueSet in m_filterValuesDic.Values)
      {
        FilterValue filterValue = filterValueSet[p_id];

        if ((filterValue != null))
          return (filterValue);
      }
      return (null);
    }

    public FilterValue GetValue(UInt32 p_filterId, UInt32 p_id)
    {
      if (m_filterValuesDic.ContainsKey(p_filterId) == false)
        return (null);
      return (m_filterValuesDic[p_filterId][p_id]);
    }

    #endregion
  }
}
