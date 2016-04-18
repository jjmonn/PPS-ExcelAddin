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

  class ChartModel : ICRUDModel<ChartConf>
  {
    static ChartModel s_instance = new ChartModel();
    public static ChartModel Instance { get { return (s_instance); } }
    SortedDictionary<UInt32, SafeDictionary<UInt32, ChartConf>> m_chartDic =
      new SortedDictionary<UInt32, SafeDictionary<UInt32, ChartConf>>();

    ChartModel()
    {
      CreateCMSG = ClientMessage.CMSG_CREATE_CHART;
      DeleteCMSG = ClientMessage.CMSG_DELETE_CHART;
      ListCMSG = ClientMessage.CMSG_LIST_CHART;
      UpdateCMSG = ClientMessage.CMSG_UPDATE_CHART;
      UpdateListCMSG = ClientMessage.CMSG_CRUD_CHART_LIST;

      CreateSMSG = ServerMessage.SMSG_CREATE_CHART_ANSWER;
      ReadSMSG = ServerMessage.SMSG_READ_CHART_ANSWER;
      DeleteSMSG = ServerMessage.SMSG_DELETE_CHART_ANSWER;
      ListSMSG = ServerMessage.SMSG_LIST_CHART_ANSWER;
      UpdateSMSG = ServerMessage.SMSG_UPDATE_CHART_ANSWER;
      UpdateListSMSG = ServerMessage.SMSG_CRUD_CHART_LIST_ANSWER;

      Build = ChartConf.BuildChart;

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
          ChartConf l_chart = Build(p_packet) as ChartConf;

          if (m_chartDic.ContainsKey(l_chart.PanelId) == false)
            m_chartDic[l_chart.PanelId] = new SafeDictionary<UInt32, ChartConf>();
          m_chartDic[l_chart.PanelId][l_chart.Id] = l_chart;
        }
        RaiseObjectInitializedEvent(p_packet.GetError(), typeof(ChartConf));
        IsInit = true;
      }
      else
      {
        RaiseObjectInitializedEvent(p_packet.GetError(), typeof(ChartConf));
        IsInit = false;
      }
    }

    protected override void ReadAnswer(ByteBuffer packet)
    {
      ChartConf l_chart = Build(packet) as ChartConf;

      if (m_chartDic.ContainsKey(l_chart.PanelId) == false)
        m_chartDic[l_chart.PanelId] = new SafeDictionary<UInt32, ChartConf>();
      m_chartDic[l_chart.PanelId][l_chart.Id] = l_chart;
      RaiseReadEvent(packet.GetError(), l_chart);
    }

    protected override void DeleteAnswer(ByteBuffer packet)
    {
      UInt32 Id = packet.ReadUint32();

      foreach (SafeDictionary<UInt32, ChartConf> l_panelDic in m_chartDic.Values)
      {
        if (l_panelDic.ContainsKey(Id))
        {
          l_panelDic.Remove(Id);
          break;
        }
      }
      RaiseDeleteEvent(packet.GetError(), Id);
    }
    #endregion

    #region Mapping

    public override ChartConf GetValue(UInt32 p_id)
    {
      foreach (SafeDictionary<UInt32, ChartConf> l_panelDic in m_chartDic.Values)
        if (l_panelDic.ContainsKey(p_id))
          return (l_panelDic[p_id]);
      return (null);
    }

    public SortedDictionary<UInt32, SafeDictionary<UInt32, ChartConf>> GetDictionary()
    {
      return (m_chartDic);
    }

    public SafeDictionary<UInt32, ChartConf> GetDictionary(UInt32 p_panelId)
    {
      if (m_chartDic.ContainsKey(p_panelId) == false)
        return (null);
      return (m_chartDic[p_panelId]);
    }

    #endregion
  }
}
