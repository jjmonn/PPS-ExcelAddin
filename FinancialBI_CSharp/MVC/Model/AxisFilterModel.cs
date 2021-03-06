﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using CRUD;

  class AxisFilterModel : ICRUDModel<AxisFilter>
  {
    protected SortedDictionary<AxisType, MultiIndexDictionary<UInt32, Tuple<UInt32, UInt32>, AxisFilter>> m_axisFilterDictionary =
      new SortedDictionary<AxisType, MultiIndexDictionary<UInt32, Tuple<UInt32, UInt32>, AxisFilter>>();

    static AxisFilterModel s_instance = new AxisFilterModel();
    public static AxisFilterModel Instance { get { return (s_instance); } }

    AxisFilterModel()
    {
      Clear();

      CreateCMSG = ClientMessage.CMSG_CREATE_AXIS_FILTER;
      ReadCMSG = ClientMessage.CMSG_READ_AXIS_FILTER;
      UpdateCMSG = ClientMessage.CMSG_UPDATE_AXIS_FILTER;
      UpdateListCMSG = ClientMessage.CMSG_CRUD_AXIS_FILTER_LIST;
      DeleteCMSG = ClientMessage.CMSG_DELETE_AXIS_FILTER;
      ListCMSG = ClientMessage.CMSG_LIST_AXIS_FILTER;

      CreateSMSG = ServerMessage.SMSG_CREATE_AXIS_FILTER_ANSWER;
      ReadSMSG = ServerMessage.SMSG_READ_AXIS_FILTER_ANSWER;
      UpdateSMSG = ServerMessage.SMSG_UPDATE_AXIS_FILTER_ANSWER;
      UpdateListSMSG = ServerMessage.SMSG_CRUD_AXIS_FILTER_LIST_ANSWER;
      DeleteSMSG = ServerMessage.SMSG_DELETE_AXIS_FILTER_ANSWER;
      ListSMSG = ServerMessage.SMSG_LIST_AXIS_FILTER_ANSWER;

      Build = AxisFilter.BuildAxisFilter;

      InitCallbacks();
    }

    private void Clear()
    {
      foreach (AxisType l_axis in System.Enum.GetValues(typeof(AxisType)))
      {
        m_axisFilterDictionary[l_axis] = new MultiIndexDictionary<UInt32, Tuple<UInt32, UInt32>, AxisFilter>();
      }
    }

    #region "CRUD"

    protected override void ListAnswer(ByteBuffer p_packet)
    {
      if (p_packet.GetError() == ErrorMessage.SUCCESS)
      {
        Clear();
        for (Int32 i = 1; i <= p_packet.ReadInt32(); i++)
        {
          AxisFilter tmp_ht = AxisFilter.BuildAxisFilter(p_packet);
          m_axisFilterDictionary[tmp_ht.Axis].Set(tmp_ht.Id, new Tuple<UInt32, UInt32>(tmp_ht.AxisElemId, tmp_ht.FilterId), tmp_ht);
        }
        IsInit = true;
        RaiseObjectInitializedEvent();
      }
      else
      {
        RaiseObjectInitializedEvent();
        IsInit = false;
      }
    }

    protected override void ReadAnswer(ByteBuffer p_packet)
    {
      if (p_packet.GetError() == ErrorMessage.SUCCESS)
      {
        AxisFilter tmp_ht = AxisFilter.BuildAxisFilter(p_packet);

        m_axisFilterDictionary[tmp_ht.Axis].Set(tmp_ht.Id, new Tuple<UInt32, UInt32>(tmp_ht.AxisElemId, tmp_ht.FilterId), tmp_ht);
        RaiseReadEvent(p_packet.GetError(), tmp_ht);
      }
      else
      {
        RaiseReadEvent(p_packet.GetError(), null);
      }
    }

    protected override void DeleteAnswer(ByteBuffer p_packet)
    {
      if (p_packet.GetError() == ErrorMessage.SUCCESS)
      {
        AxisType type = (AxisType)p_packet.ReadUint32();
        UInt32 id = p_packet.ReadUint32();
        m_axisFilterDictionary[type].Remove(id);
        RaiseDeleteEvent(p_packet.GetError(), id);
      }
      else
      {
        RaiseDeleteEvent(p_packet.GetError(), 0);
      }
    }

    #endregion

    #region "Mapping"

    public AxisFilter GetValue(AxisType p_axis, UInt32 p_axisElemId, UInt32 p_filterId)
    {
      if (m_axisFilterDictionary.ContainsKey(p_axis) == false)
        return null;
      return m_axisFilterDictionary[p_axis][new Tuple<UInt32, UInt32>(p_axisElemId, p_filterId)];
    }

    public AxisFilter GetValue(AxisType p_axis, Int32 p_axisFilterId)
    {
      return GetValue(p_axis, p_axisFilterId);
    }

    public override AxisFilter GetValue(UInt32 p_axisFilterId)
    {
      foreach (MultiIndexDictionary<uint, Tuple<uint, uint>, AxisFilter> axis in m_axisFilterDictionary.Values)
      {
        if (axis.ContainsKey(p_axisFilterId) == false)
          continue;
        return axis[p_axisFilterId];
      }
      return null;
    }

    MultiIndexDictionary<UInt32, Tuple<UInt32, UInt32>, AxisFilter> GetDictionary(AxisType p_axis)
    {
      if (m_axisFilterDictionary.ContainsKey(p_axis) == false)
        return null;
      return m_axisFilterDictionary[p_axis];
    }
    #endregion
  }
}
