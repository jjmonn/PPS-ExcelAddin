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

  class GlobalFactDataModel : ICRUDModel<GlobalFactData>
  {
    static GlobalFactDataModel s_instance = new GlobalFactDataModel();
    public static GlobalFactDataModel Instance { get { return (s_instance); } }
    private MultiIndexDictionary<UInt32, Tuple<UInt32, UInt32, UInt32>, GlobalFactData> m_globalFactDic = new MultiIndexDictionary<UInt32, Tuple<UInt32, UInt32, UInt32>, GlobalFactData>();
    GlobalFactDataModel()
    {
      CreateCMSG = ClientMessage.CMSG_CREATE_GLOBAL_FACT_DATA;
      ReadCMSG = ClientMessage.CMSG_READ_GLOBAL_FACT_DATA;
      UpdateCMSG = ClientMessage.CMSG_UPDATE_GLOBAL_FACT_DATA;
      UpdateListCMSG = ClientMessage.CMSG_CRUD_GLOBAL_FACT_DATA_LIST;
      DeleteCMSG = ClientMessage.CMSG_DELETE_GLOBAL_FACT_DATA;
      ListCMSG = ClientMessage.CMSG_LIST_GLOBAL_FACT_DATA;

      CreateSMSG = ServerMessage.SMSG_CREATE_GLOBAL_FACT_DATA_ANSWER;
      ReadSMSG = ServerMessage.SMSG_READ_GLOBAL_FACT_DATA_ANSWER;
      UpdateSMSG = ServerMessage.SMSG_UPDATE_GLOBAL_FACT_DATA_ANSWER;
      UpdateListSMSG = ServerMessage.SMSG_CRUD_GLOBAL_FACT_DATA_LIST_ANSWER;
      DeleteSMSG = ServerMessage.SMSG_DELETE_GLOBAL_FACT_DATA_ANSWER;
      ListSMSG = ServerMessage.SMSG_LIST_GLOBAL_FACT_DATA_ANSWER;

      Build = GlobalFactData.BuildGlobalFact;

      InitCallbacks();
    }

    #region "CRUD"

    protected override void ListAnswer(ByteBuffer packet)
    {
      if (packet.GetError() == ErrorMessage.SUCCESS)
      {
        m_globalFactDic.Clear();
        UInt32 count = packet.ReadUint32();
        for (UInt32 i = 1; i <= count; i++)
        {
          GlobalFactData tmp_ht = Build(packet) as GlobalFactData;

          m_globalFactDic.Set(tmp_ht.Id, new Tuple<UInt32, UInt32, UInt32>(tmp_ht.GlobalFactId, tmp_ht.Period, tmp_ht.VersionId), tmp_ht);
        }
        IsInit = true;
        RaiseObjectInitializedEvent();
      }
      else
        IsInit = false;
    }


    protected override void ReadAnswer(ByteBuffer packet)
    {
      if (packet.GetError() == ErrorMessage.SUCCESS)
      {
        GlobalFactData tmp_ht = Build(packet) as GlobalFactData;

        m_globalFactDic.Set(tmp_ht.Id, new Tuple<UInt32, UInt32, UInt32>(tmp_ht.GlobalFactId, tmp_ht.Period, tmp_ht.VersionId), tmp_ht);
        RaiseReadEvent(packet.GetError(), tmp_ht);
      }
      else
        RaiseReadEvent(packet.GetError(), null);
    }


    protected override void DeleteAnswer(ByteBuffer packet)
    {
      UInt32 Id = packet.ReadUint32();
      if (packet.GetError() == ErrorMessage.SUCCESS)
      {
        m_globalFactDic.Remove(Id);
      }
      RaiseDeleteEvent(packet.GetError(), Id);
    }

    #endregion

    #region Mapping

    public override GlobalFactData GetValue(UInt32 p_id)
    {
      return (m_globalFactDic[p_id]);
    }

    public GlobalFactData GetValue(UInt32 p_globalFactId, UInt32 p_period, UInt32 p_rateVersionId)
    {
      return (m_globalFactDic[new Tuple<UInt32, UInt32, UInt32>(p_globalFactId, p_period, p_rateVersionId)]);
    }

    public MultiIndexDictionary<UInt32, Tuple<UInt32, UInt32, UInt32>, GlobalFactData> GetDictionary()
    {
      return (m_globalFactDic);
    }

    #endregion
  }
}
