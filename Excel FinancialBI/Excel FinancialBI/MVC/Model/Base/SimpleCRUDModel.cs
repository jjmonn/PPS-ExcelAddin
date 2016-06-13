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

  class SimpleCRUDModel<T> : ICRUDModel<T> where T : class, CRUDEntity
  {
    private SortedDictionary<UInt32, T> m_dic = new SortedDictionary<UInt32, T>();

    #region "CRUD"

    protected override void ListAnswer(ByteBuffer packet)
    {
      if (packet.GetError() == ErrorMessage.SUCCESS)
      {
        m_dic.Clear();
        UInt32 count = packet.ReadUint32();
        for (UInt32 i = 1; i <= count; i++)
        {
          T tmpValue = Build(packet) as T;

          m_dic.Add(tmpValue.Id, tmpValue);
        }
        IsInit = true;
        RaiseObjectInitializedEvent(packet.GetError(), typeof(T));
      }
      else
      {
        IsInit = false;
        RaiseObjectInitializedEvent(packet.GetError(), typeof(T));
      }

    }

    protected override void ReadAnswer(ByteBuffer packet)
    {
      if (packet.GetError() == ErrorMessage.SUCCESS)
      {
        T value = Build(packet) as T;

        if (m_dic.ContainsKey(value.Id))
          m_dic[value.Id] = value;
        else
          m_dic.Add(value.Id, value);
        RaiseReadEvent(packet.GetError(), value);
      }
      else
      {
        RaiseReadEvent(packet.GetError(), null);
      }

    }

    protected override void DeleteAnswer(ByteBuffer packet)
    {
      UInt32 id = packet.ReadUint32();
      if (packet.GetError() == ErrorMessage.SUCCESS)
      {
        m_dic.Remove(id);
      }
      RaiseDeleteEvent(packet.GetError(), id);
    }

    #endregion

    #region "Mapping"

    public override T GetValue(UInt32 p_id)
    {
      if (m_dic.ContainsKey(p_id) == false)
        return (null);
      return (m_dic[p_id]);
    }

    public SortedDictionary<UInt32, T> GetDictionary()
    {
      return (m_dic);
    }

    #endregion
  }
}
