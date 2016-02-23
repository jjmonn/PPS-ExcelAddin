using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using CRUD;

  class SimpleCRUDModel<T> : ICRUDModel<T>
  {
    private SortedDictionary<UInt32, CRUDEntity> m_dic = new SortedDictionary<UInt32, CRUDEntity>();

    #region "CRUD"

    protected override void ListAnswer(ByteBuffer packet)
    {
      if (packet.GetError() == ErrorMessage.SUCCESS)
      {
        m_dic.Clear();
        for (Int32 i = 1; i <= packet.ReadInt32(); i++)
        {
          CRUDEntity tmpValue = Build(packet);

          m_dic.Add(tmpValue.Id, tmpValue);
        }
        IsInit = true;
        RaiseObjectInitializedEvent();
      }
      else
      {
        IsInit = false;
        RaiseObjectInitializedEvent();
      }

    }

    protected override void ReadAnswer(ByteBuffer packet)
    {
      if (packet.GetError() == ErrorMessage.SUCCESS)
      {
        CRUDEntity value = Build(packet);

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

    public override CRUDEntity GetValue(UInt32 p_id)
    {
      if (m_dic.ContainsKey(p_id) == false)
        return null;
      return m_dic[p_id];
    }

    #endregion
  }
}
