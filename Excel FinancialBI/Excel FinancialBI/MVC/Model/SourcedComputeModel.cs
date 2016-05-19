using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using Network;
  using CRUD;

  class SourcedComputeModel : AComputeModel
  {
    public event ComputeCompleteEventHandler ComputeCompleteEvent;
    public delegate void ComputeCompleteEventHandler(ErrorMessage p_status, SourcedComputeRequest p_request, SafeDictionary<UInt32, ComputeResult> p_result);

    static SourcedComputeModel s_instance = new SourcedComputeModel();
    public static SourcedComputeModel Instance { get { return (s_instance); } }

    SourcedComputeModel()
    {
      NetworkManager.SetCallback((UInt16)ServerMessage.SMSG_SOURCED_COMPUTE_RESULT, OnSourcedComputeResult);
    }

    public bool Compute(SourcedComputeRequest p_request)
    {
      ByteBuffer[] l_packetList = new ByteBuffer[p_request.EntityList.Count];
      List<Int32> l_requestIdList = new List<int>();

      for (Int32 i = 0; i < l_packetList.Length; ++i)
      {
        l_packetList[i] = new ByteBuffer((UInt16)ClientMessage.CMSG_SOURCED_COMPUTE);
        l_requestIdList.Add(l_packetList[i].AssignRequestId());
        m_requestAxisList[l_requestIdList[i]] = p_request.EntityList[i];
      }
      m_requestList.Add(new Tuple<AComputeRequest, List<Int32>>(p_request, l_requestIdList));
      m_resultDic[p_request] = new SafeDictionary<uint, ComputeResult>();
      for (Int32 i = 0; i < l_packetList.Length; ++i)
      {
        p_request.Dump(l_packetList[i], p_request.EntityList[i]);
        l_packetList[i].Release();
        if (NetworkManager.Send(l_packetList[i]) == false)
          return (false);
      }
      return (true);
    }

    void OnSourcedComputeResult(ByteBuffer p_packet)
    {
      Int32 l_requestId = p_packet.GetRequestId();
      Tuple<AComputeRequest, List<Int32>> l_requestTuple = FindComputeRequest(l_requestId);
      SourcedComputeRequest l_request = l_requestTuple.Item1 as SourcedComputeRequest;
      List<Int32> l_requestIdList = l_requestTuple.Item2;
      ComputeResult l_result = null;

      if (l_request != null)
      {
        l_requestIdList.Remove(l_requestId);
        l_result = ComputeResult.BuildSourcedComputeResult(l_request, p_packet, m_requestAxisList[l_requestId]);
        m_requestAxisList.Remove(l_requestId);
        m_resultDic[l_request][l_result.EntityId] = l_result;
        if (l_requestIdList.Count == 0)
        {
          SafeDictionary<UInt32, ComputeResult> l_resultDic = m_resultDic[l_request];

          m_requestList.Remove(l_requestTuple);
          m_resultDic.Remove(l_request);
          if (ComputeCompleteEvent != null)
            ComputeCompleteEvent(p_packet.GetError(), l_request, l_resultDic);
        }
      }
    }
  }
}
