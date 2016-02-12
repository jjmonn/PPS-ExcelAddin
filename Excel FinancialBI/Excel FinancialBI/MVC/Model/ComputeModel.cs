using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using CRUD;
  using Network;

  class ComputeModel
  {
    public event ComputeCompleteEventHandler ComputeCompleteEvent;
    public delegate void ComputeCompleteEventHandler(ErrorMessage p_status, ComputeRequest p_request, ComputeResult p_result);

    static ComputeModel s_instance = new ComputeModel();
    public static ComputeModel Instance { get { return (s_instance); } }
    SafeDictionary<Int32, ComputeRequest> m_computeRequestDic;
    SafeDictionary<Int32, ComputeResult> m_computeResultDic;
    SafeDictionary<Int32, Tuple<bool, Int32>> m_toDiffList;

    ComputeModel() 
    {
      m_computeRequestDic = new SafeDictionary<Int32, ComputeRequest>();
      m_computeResultDic = new SafeDictionary<Int32, ComputeResult>();
      m_toDiffList = new SafeDictionary<Int32, Tuple<bool, Int32>>();
      NetworkManager.SetCallback((UInt16)ServerMessage.SMSG_COMPUTE_RESULT, OnComputeResult);
    }

    public void ComputeDiff(ComputeRequest p_request1, ComputeRequest p_request2)
    {
      Int32 l_first = Compute(p_request1);
      Int32 l_second = Compute(p_request2);

      m_toDiffList[l_first] = new Tuple<bool, Int32>(false, l_second);
      m_toDiffList[l_second] = new Tuple<bool, Int32>(true, l_first);
    }

    public Int32 Compute(ComputeRequest p_request)
    {
      ByteBuffer l_packet = new ByteBuffer((UInt16)ClientMessage.CMSG_COMPUTE_REQUEST);
      Int32 l_requestId = l_packet.AssignRequestId();

      m_computeRequestDic[l_requestId] = p_request;
      p_request.Dump(l_packet);

      l_packet.Release();
      NetworkManager.Send(l_packet);
      return (l_requestId);
    }

    public void OnComputeResult(ByteBuffer p_packet)
    {
      Int32 l_requestId = p_packet.GetRequestId();
      ComputeRequest l_request = m_computeRequestDic[l_requestId];
      ComputeResult l_result = null;

      if (l_request != null)
      {
        l_result = ComputeResult.BuildComputeResult(l_request, p_packet);
        if (m_toDiffList.ContainsKey(l_requestId))
        {
          Tuple<bool, Int32> l_diffId = m_toDiffList[l_requestId];

          if (m_computeResultDic[l_diffId.Item2] != null)
          {
            if (l_diffId.Item1)
              l_result = l_result - m_computeResultDic[l_diffId.Item2];
            else
              l_result = m_computeResultDic[l_diffId.Item2] - l_result;
            m_computeResultDic.Remove(l_diffId.Item2);
            m_computeRequestDic.Remove(l_requestId);
            m_computeRequestDic.Remove(l_diffId.Item2);
          }
          else
          {
            m_computeResultDic[l_requestId] = l_result;
            return;
          }
        }
        else
          m_computeRequestDic.Remove(l_requestId);
      }
      if (ComputeCompleteEvent != null)
        ComputeCompleteEvent(p_packet.GetError(), l_request, l_result);
    }
  }
}
