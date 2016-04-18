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

  class ChartAccountModel : ICRUDModel<ChartAccount>
  {
    static ChartAccountModel s_instance = new ChartAccountModel();
    public static ChartAccountModel Instance { get { return (s_instance); } }
    SortedDictionary<UInt32, SafeDictionary<UInt32, ChartAccount>> m_chartAccountDic =
      new SortedDictionary<UInt32, SafeDictionary<UInt32, ChartAccount>>();

    ChartAccountModel()
    {
      CreateCMSG = ClientMessage.CMSG_CREATE_CHART_ACCOUNT;
      DeleteCMSG = ClientMessage.CMSG_DELETE_CHART_ACCOUNT;
      ListCMSG = ClientMessage.CMSG_LIST_CHART_ACCOUNT;
      UpdateCMSG = ClientMessage.CMSG_UPDATE_CHART_ACCOUNT;
      UpdateListCMSG = ClientMessage.CMSG_CRUD_CHART_ACCOUNT_LIST;

      CreateSMSG = ServerMessage.SMSG_CREATE_CHART_ACCOUNT_ANSWER;
      ReadSMSG = ServerMessage.SMSG_READ_CHART_ACCOUNT_ANSWER;
      DeleteSMSG = ServerMessage.SMSG_DELETE_CHART_ACCOUNT_ANSWER;
      ListSMSG = ServerMessage.SMSG_LIST_CHART_ACCOUNT_ANSWER;
      UpdateSMSG = ServerMessage.SMSG_UPDATE_CHART_ACCOUNT_ANSWER;
      UpdateListSMSG = ServerMessage.SMSG_CRUD_CHART_ACCOUNT_LIST_ANSWER;

      Build = ChartAccount.BuildChartAccount;

      InitCallbacks();
    }

    #region CRUD

    protected override void ListAnswer(ByteBuffer p_packet)
    {
      if (p_packet.GetError() == ErrorMessage.SUCCESS)
      {
        UInt32 count = p_packet.ReadUint32();
        for (Int32 i = 1; i <= count; i++)
        {
          ChartAccount l_account = Build(p_packet) as ChartAccount;

          if (m_chartAccountDic.ContainsKey(l_account.ChartId) == false)
            m_chartAccountDic[l_account.ChartId] = new SafeDictionary<UInt32, ChartAccount>();
          m_chartAccountDic[l_account.ChartId][l_account.Id] = l_account;
        }
        RaiseObjectInitializedEvent(p_packet.GetError(), typeof(ChartAccount));
        IsInit = true;
      }
      else
      {
        RaiseObjectInitializedEvent(p_packet.GetError(), typeof(ChartAccount));
        IsInit = false;
      }
    }

    protected override void ReadAnswer(ByteBuffer packet)
    {
      ChartAccount l_chartAccount = Build(packet) as ChartAccount;

      if (m_chartAccountDic.ContainsKey(l_chartAccount.ChartId) == false)
        m_chartAccountDic[l_chartAccount.ChartId] = new SafeDictionary<UInt32, ChartAccount>();
      m_chartAccountDic[l_chartAccount.ChartId][l_chartAccount.Id] = l_chartAccount;
      RaiseReadEvent(packet.GetError(), l_chartAccount);
    }

    protected override void DeleteAnswer(ByteBuffer packet)
    {
      UInt32 Id = packet.ReadUint32();

      foreach (SafeDictionary<UInt32, ChartAccount> l_chartDic in m_chartAccountDic.Values)
      {
        if (l_chartDic.ContainsKey(Id))
        {
          l_chartDic.Remove(Id);
          break;
        }
      }
      RaiseDeleteEvent(packet.GetError(), Id);
    }
    #endregion

    #region Mapping

    public override ChartAccount GetValue(UInt32 p_id)
    {
      foreach (SafeDictionary<UInt32, ChartAccount> l_chartDic in m_chartAccountDic.Values)
        if (l_chartDic.ContainsKey(p_id))
          return (l_chartDic[p_id]);
      return (null);
    }

    public SortedDictionary<UInt32, SafeDictionary<UInt32, ChartAccount>> GetDictionary()
    {
      return (m_chartAccountDic);
    }

    public SafeDictionary<UInt32, ChartAccount> GetDictionary(UInt32 p_chartId)
    {
      if (m_chartAccountDic.ContainsKey(p_chartId) == false)
        return (null);
      return (m_chartAccountDic[p_chartId]);
    }
    #endregion
  }
}
