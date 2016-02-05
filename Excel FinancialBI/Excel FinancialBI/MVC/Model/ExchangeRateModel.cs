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

  class ExchangeRateModel : ICRUDModel<ExchangeRate>
  {
    static ExchangeRateModel s_instance = new ExchangeRateModel();
    public static ExchangeRateModel Instance { get { return (s_instance); } }

    MultiIndexDictionary<UInt32, Tuple<UInt32, UInt32, UInt32>, ExchangeRate> m_CRUDDic = new MultiIndexDictionary<UInt32,Tuple<UInt32,UInt32,UInt32>,ExchangeRate>();

    ExchangeRateModel()
    {
      CreateCMSG = ClientMessage.CMSG_CREATE_EXCHANGE_RATE;
      ReadCMSG = ClientMessage.CMSG_READ_EXCHANGE_RATE;
      UpdateCMSG = ClientMessage.CMSG_UPDATE_EXCHANGE_RATE;
      UpdateListCMSG = ClientMessage.CMSG_CRUD_EXCHANGE_RATE_LIST;
      DeleteCMSG = ClientMessage.CMSG_DELETE_EXCHANGE_RATE;
      ListCMSG = ClientMessage.CMSG_LIST_EXCHANGE_RATE;

      CreateSMSG = ServerMessage.SMSG_CREATE_EXCHANGE_RATE_ANSWER;
      ReadSMSG = ServerMessage.SMSG_READ_EXCHANGE_RATE_ANSWER;
      UpdateSMSG = ServerMessage.SMSG_UPDATE_EXCHANGE_RATE_ANSWER;
      UpdateListSMSG = ServerMessage.SMSG_CRUD_EXCHANGE_RATE_LIST_ANSWER;
      DeleteSMSG = ServerMessage.SMSG_DELETE_EXCHANGE_RATE_ANSWER;
      ListSMSG = ServerMessage.SMSG_LIST_EXCHANGE_RATE_ANSWER;

      Build = ExchangeRate.BuildExchangeRate;

      InitCallbacks();
    }

    #region CRUD

    protected override void ListAnswer(ByteBuffer p_packet)
    {
      if (p_packet.GetError() == ErrorMessage.SUCCESS)
      {
        m_CRUDDic.Clear();
        UInt32 count = p_packet.ReadUint32();
        for (UInt32 i = 1; i <= count; i++)
        {
          ExchangeRate tmp_rate = Build(p_packet) as ExchangeRate;

          m_CRUDDic.Set(tmp_rate.Id, new Tuple<UInt32, UInt32, UInt32>(tmp_rate.DestCurrencyId, tmp_rate.RateVersionId, tmp_rate.Period), tmp_rate);
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
        ExchangeRate tmp_rate = Build(packet) as ExchangeRate;

        m_CRUDDic.Set(tmp_rate.Id, new Tuple<UInt32, UInt32, UInt32>(tmp_rate.DestCurrencyId, tmp_rate.RateVersionId, tmp_rate.Period), tmp_rate);
        RaiseReadEvent(packet.GetError(), tmp_rate);
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
        m_CRUDDic.Remove(id);
      RaiseDeleteEvent(packet.GetError(), id);
    }

    #endregion

    #region Mapping

    public ExchangeRate GetValue(UInt32 p_currencyId, UInt32 p_rateVersionId, UInt32 p_period)
    {
      return (m_CRUDDic[new Tuple<UInt32, UInt32, UInt32>(p_currencyId, p_rateVersionId, p_period)]);
    }

    public override ExchangeRate GetValue(UInt32 p_id)
    {
      return (m_CRUDDic[p_id]);
    }

    #endregion

  }
}
