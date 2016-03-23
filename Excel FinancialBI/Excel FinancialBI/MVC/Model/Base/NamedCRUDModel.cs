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

  public class NamedCRUDModel<T> : ICRUDModel<T> where T : class, NamedCRUDEntity
  {
    protected MultiIndexDictionary<UInt32, string, T> m_CRUDDic = new MultiIndexDictionary<UInt32, string, T>();

    #region "CRUD"

    protected override void ListAnswer(ByteBuffer p_packet)
    {
      if (p_packet.GetError() == ErrorMessage.SUCCESS)
      {
        m_CRUDDic.Clear();
        Int32 nb_accounts = p_packet.ReadInt32();
        for (Int32 i = 1; i <= nb_accounts; i++)
        {
          T tmp_crud = Build(p_packet) as T;

          m_CRUDDic.Set(tmp_crud.Id, StringUtils.RemoveDiacritics(tmp_crud.Name), tmp_crud);
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
        T tmp_crud = Build(p_packet) as T;

        m_CRUDDic.Set(tmp_crud.Id, StringUtils.RemoveDiacritics(tmp_crud.Name), tmp_crud);
        RaiseReadEvent(p_packet.GetError(), tmp_crud);
      }
      else
      {
        RaiseReadEvent(p_packet.GetError(), null);
      }
    }

    protected override void DeleteAnswer(ByteBuffer p_packet)
    {
      UInt32 id = p_packet.ReadUint32();
      if (p_packet.GetError() == ErrorMessage.SUCCESS)
        m_CRUDDic.Remove(id);
      RaiseDeleteEvent(p_packet.GetError(), id);
    }

    #endregion

    #region "Mapping"

    public UInt32 GetValueId(string p_name)
    {

      if (m_CRUDDic[StringUtils.RemoveDiacritics(p_name)] == null)
        return 0;
      return m_CRUDDic[StringUtils.RemoveDiacritics(p_name)].Id;
    }

    public string GetValueName(UInt32 p_id)
    {
      if (m_CRUDDic[p_id] == null)
        return "";
      return m_CRUDDic[p_id].Name;
    }

    public T GetValue(string p_name)
    {
      if (p_name == null)
        return null;
      return m_CRUDDic[StringUtils.RemoveDiacritics(p_name)];
    }

    public override T GetValue(UInt32 p_id)
    {
      return m_CRUDDic[p_id];
    }

    public MultiIndexDictionary<UInt32, string, T> GetDictionary()
    {
      return m_CRUDDic;
    }
    #endregion
  }
}
