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

  class AxedCRUDModel<T> : ICRUDModel<T> where T : class, AxedCRUDEntity, NamedCRUDEntity
  {
    protected SortedDictionary<AxisType, MultiIndexDictionary<UInt32, string, T>> m_CRUDDic = new SortedDictionary<AxisType,MultiIndexDictionary<uint,string,T>>();

    public AxedCRUDModel()
    {
    }

    private void Clear()
    {
      m_CRUDDic.Clear();
      foreach (AxisType l_axis in System.Enum.GetValues(typeof(AxisType)))
        m_CRUDDic[l_axis] = new MultiIndexDictionary<UInt32, string, T>();
    }

    protected override void ListAnswer(ByteBuffer p_packet)
    {
      if (p_packet.GetError() == ErrorMessage.SUCCESS)
      {
        Clear();
        UInt32 count = p_packet.ReadUint32();
        for (UInt32 i = 1; i <= count; i++)
        {
          T tmp_filter = Build(p_packet) as T;

          m_CRUDDic[tmp_filter.Axis].Set(tmp_filter.Id, tmp_filter.Name, tmp_filter);
        }
        IsInit = true;
        RaiseObjectInitializedEvent(p_packet.GetError(), typeof(T));
      }
      else
      {
        IsInit = false;
        RaiseObjectInitializedEvent(p_packet.GetError(), typeof(T));
      }
    }

    protected override void ReadAnswer(ByteBuffer p_packet)
    {
      if (p_packet.GetError() == ErrorMessage.SUCCESS)
      {
        T tmp_filter = Build(p_packet) as T;

        m_CRUDDic[tmp_filter.Axis].Set(tmp_filter.Id, tmp_filter.Name, tmp_filter);
        RaiseReadEvent(p_packet.GetError(), tmp_filter);
      }
      else
      {
        RaiseReadEvent(p_packet.GetError(), null);
      }

    }


    protected override void DeleteAnswer(ByteBuffer packet)
    {
      UInt32 id = packet.ReadUint32();

      if (packet.GetError() == ErrorMessage.SUCCESS)
      {
        foreach (AxisType axis in m_CRUDDic.Keys)
          m_CRUDDic[axis].Remove(id);
        RaiseDeleteEvent(packet.GetError(), id);
      }
      else
      {
        RaiseDeleteEvent(packet.GetError(), id);
      }

    }

    public T GetValue(AxisType p_axis, string p_name)
    {
      if (m_CRUDDic.ContainsKey(p_axis) == false)
        return null;
      return (m_CRUDDic[p_axis][p_name]);
    }

    public T GetValue(AxisType p_axis, UInt32 p_id)
    {
      if (m_CRUDDic.ContainsKey(p_axis) == false)
        return (null);
      return (m_CRUDDic[p_axis][p_id]);
    }

    public override T GetValue(UInt32 p_id)
    {
      foreach (MultiIndexDictionary<UInt32, string, T> axis in m_CRUDDic.Values)
      {
        if (axis.ContainsKey(p_id) == false)
          continue;

        return (axis[p_id]);
      }
      return (null);
    }

    public MultiIndexDictionary<UInt32, string, T> GetDictionary(AxisType p_axis)
    {
      if (m_CRUDDic.ContainsKey(p_axis) == false)
        return (null);
      return (m_CRUDDic[p_axis]);
    }

    /* MultiIndexDictionary<UInt32, string, T> GetDictionary(AxisType p_axis, UInt32 p_AxisOwner)
     {
       if (m_CRUDDic.ContainsKey(p_axis) == false)
         return null;
       MultiIndexDictionary<UInt32, string, T> l_multiIndexDict = new MultiIndexDictionary<UInt32, string, T>();

       foreach (T l_axisElem in m_CRUDDic[p_axis].Values)
       {
         AxisOwner l_AxisOwner = GlobalVariables.AxisOwners.GetValue(l_axisElem.Id);
         if (l_AxisOwner == null)
           continue;
         if (l_AxisOwner.OwnerId == p_AxisOwner)
         {
           l_multiIndexDict.Set(l_axisElem.Id, l_axisElem.Name, l_axisElem);
         }
       }
       return l_multiIndexDict;

     }*/

    SortedDictionary<AxisType, MultiIndexDictionary<UInt32, string, T>> GetDictionary()
    {
      return m_CRUDDic;
    }

    UInt32 GetValueId(AxisType p_axis, string name)
    {
      T axisValue = GetValue(p_axis, name);

      if (axisValue == null)
        return 0;
      return axisValue.Id;
    }

    string GetValueName(AxisType p_axis, UInt32 p_id)
    {
      T axisValue = GetValue(p_axis, p_id);

      if (axisValue == null)
        return "";
      return axisValue.Name;
    }
  }
}