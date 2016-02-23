using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using CRUD;

  class CurrencyModel : NamedCRUDModel<Currency>
  {
    static CurrencyModel s_instance = new CurrencyModel();
    public static CurrencyModel Instance { get { return (s_instance); } }

    SortedSet<UInt32> m_usedCurrencies = new SortedSet<UInt32>();
    UInt32 m_mainCurrency;

    public event GetMainCurrencyEventHandler GetMainCurrencyEvent;
    public delegate void GetMainCurrencyEventHandler(ErrorMessage p_status, UInt32 p_id);

    CurrencyModel()
    {
      CreateCMSG = ClientMessage.CMSG_CREATE_CURRENCY;
      ReadCMSG = ClientMessage.CMSG_READ_CURRENCY;
      UpdateCMSG = ClientMessage.CMSG_UPDATE_CURRENCY;
      UpdateListCMSG = ClientMessage.CMSG_CRUD_CURRENCY_LIST;
      DeleteCMSG = ClientMessage.CMSG_DELETE_CURRENCY;
      ListCMSG = ClientMessage.CMSG_LIST_CURRENCY;

      CreateSMSG = ServerMessage.SMSG_CREATE_CURRENCY_ANSWER;
      ReadSMSG = ServerMessage.SMSG_READ_CURRENCY_ANSWER;
      UpdateSMSG = ServerMessage.SMSG_UPDATE_CURRENCY_ANSWER;
      UpdateListSMSG = ServerMessage.SMSG_CRUD_CURRENCY_LIST_ANSWER;
      DeleteSMSG = ServerMessage.SMSG_DELETE_CURRENCY_ANSWER;
      ListSMSG = ServerMessage.SMSG_LIST_CURRENCY_ANSWER;

      Build = Currency.BuildCurrency;

      InitCallbacks();

      NetworkManager.GetInstance().SetCallback((ushort)ServerMessage.SMSG_GET_MAIN_CURRENCY_ANSWER, SMSG_GET_MAIN_CURRENCY_ANSWER);
      NetworkManager.GetInstance().SetCallback((ushort)ServerMessage.SMSG_SET_MAIN_CURRENCY_ANSWER, SMSG_SET_MAIN_CURRENCY_ANSWER);
    }

    ~CurrencyModel()
    {
      NetworkManager.GetInstance().RemoveCallback((ushort)ServerMessage.SMSG_GET_MAIN_CURRENCY_ANSWER, SMSG_GET_MAIN_CURRENCY_ANSWER);
      NetworkManager.GetInstance().RemoveCallback((ushort)ServerMessage.SMSG_SET_MAIN_CURRENCY_ANSWER, SMSG_SET_MAIN_CURRENCY_ANSWER);
    }

    #region "CRUD"

    protected override void ListAnswer(ByteBuffer packet)
    {
      if (packet.GetError() == ErrorMessage.SUCCESS)
      {
        base.ListAnswer(packet);
        m_usedCurrencies.Clear();

        foreach (Currency l_currency in m_CRUDDic.Values)
          if (l_currency.InUse == true)
            m_usedCurrencies.Add(l_currency.Id);
      }
    }


    protected override void ReadAnswer(ByteBuffer packet)
    {
      if (packet.GetError() == ErrorMessage.SUCCESS)
      {
        dynamic l_currency = Currency.BuildCurrency(packet);

        if ((l_currency.InUse == true))
        {
          if (m_usedCurrencies.Contains(l_currency.Id) == false)
            m_usedCurrencies.Add(l_currency.Id);
        }
        else
        {
          if (m_usedCurrencies.Contains(l_currency.Id))
            m_usedCurrencies.Remove(l_currency.Id);
        }

        m_CRUDDic.Set(l_currency.Id, l_currency.Name, l_currency);
        RaiseReadEvent(packet.GetError(), l_currency);
      }
      else
      {
        RaiseReadEvent(packet.GetError(), null);
      }
    }


    protected override void DeleteAnswer(ByteBuffer packet)
    {
      if (packet.GetError() == ErrorMessage.SUCCESS)
      {
        UInt32 id = packet.ReadUint32();
        m_usedCurrencies.Remove(id);
        m_CRUDDic.Remove(id);
        RaiseDeleteEvent(packet.GetError(), id);
      }
      else
      {
        RaiseDeleteEvent(packet.GetError(), 0);
      }
    }


    void SMSG_GET_MAIN_CURRENCY_ANSWER(ByteBuffer packet)
    {
      m_mainCurrency = packet.ReadUint32();
      if (GetMainCurrencyEvent != null)
        GetMainCurrencyEvent(packet.GetError(), m_mainCurrency);
    }


    void SMSG_SET_MAIN_CURRENCY_ANSWER(ByteBuffer packet)
    {
      if (packet.GetError() == ErrorMessage.SUCCESS)
      {
        if (GetMainCurrencyEvent != null)
          GetMainCurrencyEvent(packet.GetError(), 0);
      }
      else
      {
        if (GetMainCurrencyEvent != null)
          GetMainCurrencyEvent(packet.GetError(), packet.ReadUint32());
      }
    }

    public void CMSG_SET_MAIN_CURRENCY(ref UInt32 id)
    {
      ByteBuffer packet = new ByteBuffer(Convert.ToUInt16(ClientMessage.CMSG_GET_MAIN_CURRENCY));
      packet.WriteUint32(id);
      packet.Release();
      NetworkManager.GetInstance().Send(packet);
    }

    #endregion

    #region Mappings

    public UInt32 GetMainCurrency()
    {
      return (m_mainCurrency);
    }

    public List<string> GetCurrencyNameList()
    {
      List<string> list = new List<string>();

      foreach (Currency elem in m_CRUDDic.Values)
        list.Add(elem.Name);
      return (list);
    }

    public SortedSet<UInt32> GetUsedCurrencies()
    {
      return (m_usedCurrencies);
    }

    #endregion
  }
}
