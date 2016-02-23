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
    public delegate void ComputeCompleteEventHandler(ErrorMessage p_status, ComputeRequest p_request, SafeDictionary<UInt32, ComputeResult> p_result);

    static ComputeModel s_instance = new ComputeModel();
    public static ComputeModel Instance { get { return (s_instance); } }
    SafeDictionary<ComputeRequest, SafeDictionary<UInt32, ComputeResult>> m_computeResultDic;
    SafeDictionary<Int32, Tuple<bool, Int32>> m_toDiffList;
    List<Tuple<ComputeRequest, List<Int32>>> m_computeRequestList;
    SafeDictionary<Int32, UInt32> m_computeRequestVersionList;

    ComputeModel() 
    {
      m_computeResultDic = new SafeDictionary<ComputeRequest, SafeDictionary<uint, ComputeResult>>();
      m_computeRequestList = new List<Tuple<ComputeRequest, List<int>>>();
      m_computeRequestVersionList = new SafeDictionary<int, uint>();
      m_toDiffList = new SafeDictionary<Int32, Tuple<bool, Int32>>();
      NetworkManager.SetCallback((UInt16)ServerMessage.SMSG_COMPUTE_RESULT, OnComputeResult);
    }

    public bool ComputeDiff(ComputeRequest p_request)
    {
      if (p_request.Versions.Count > 2)
        return (false);
      p_request.IsDiff = true;
      Compute(p_request);
      return (true);
    }

    public void Compute(ComputeRequest p_request)
    {
      ByteBuffer[] l_packetList = new ByteBuffer[p_request.Versions.Count];
      List<Int32> l_requestIdList = new List<int>();

      for (Int32 i = 0; i < l_packetList.Length; ++i)
      {
        l_packetList[i] = new ByteBuffer((UInt16)ClientMessage.CMSG_COMPUTE_REQUEST);
        l_requestIdList.Add(l_packetList[i].AssignRequestId());
        m_computeRequestVersionList[l_requestIdList[i]] = p_request.Versions[i];
      }
      m_computeRequestList.Add(new Tuple<ComputeRequest, List<Int32>>(p_request, l_requestIdList));
      m_computeResultDic[p_request] = new SafeDictionary<uint, ComputeResult>();
      for (Int32 i = 0; i < l_packetList.Length; ++i)
      {
        p_request.Dump(l_packetList[i], p_request.Versions[i]);
        l_packetList[i].Release();
        NetworkManager.Send(l_packetList[i]);
      }
    }

    public Tuple<ComputeRequest, List<Int32>> FindComputeRequest(Int32 p_requestId)
    {
      foreach (Tuple<ComputeRequest, List<Int32>> l_elem in m_computeRequestList)
        foreach (Int32 l_requestId in l_elem.Item2)
          if (l_requestId == p_requestId)
            return (l_elem);
      return (null);
    }

    public void OnComputeResult(ByteBuffer p_packet)
    {
      Int32 l_requestId = p_packet.GetRequestId();
      Tuple<ComputeRequest, List<Int32>> l_requestTuple = FindComputeRequest(l_requestId);
      ComputeRequest l_request = l_requestTuple.Item1;
      List<Int32> l_requestIdList = l_requestTuple.Item2;
      ComputeResult l_result = null;

      if (l_request != null)
      {
        l_requestIdList.Remove(l_requestId);
        l_result = ComputeResult.BuildComputeResult(l_request, p_packet, m_computeRequestVersionList[l_requestId]);
        m_computeRequestVersionList.Remove(l_requestId);
        m_computeResultDic[l_request][l_result.VersionId] = l_result;
        if (l_requestIdList.Count == 0)
        {
          SafeDictionary<UInt32, ComputeResult> l_resultDic = m_computeResultDic[l_request];
          List<ComputeResult> l_resultList = l_resultDic.Values.ToList();

          if (l_request.IsDiff && l_resultDic.Count == 2)
          {
            ComputeResult l_diffA = l_resultList[0] - l_resultList[1];
            ComputeResult l_diffB = l_resultList[1] - l_resultList[0];

            l_resultDic[l_diffA.VersionId] = l_diffA;
            l_resultDic[l_diffB.VersionId] = l_diffB;
          }
          m_computeRequestList.Remove(l_requestTuple);
          m_computeResultDic.Remove(l_request);
          if (ComputeCompleteEvent != null)
            ComputeCompleteEvent(p_packet.GetError(), l_request, l_resultDic);
        }
      }
    }
  }
}
