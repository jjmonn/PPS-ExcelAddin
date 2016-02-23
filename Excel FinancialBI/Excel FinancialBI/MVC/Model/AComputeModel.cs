using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using CRUD;
  using Network;

  abstract class AComputeModel
  {
    protected SafeDictionary<AComputeRequest, SafeDictionary<UInt32, ComputeResult>> m_resultDic;
    protected List<Tuple<AComputeRequest, List<Int32>>> m_requestList;
    protected SafeDictionary<Int32, UInt32> m_requestAxisList;

    protected AComputeModel()
    {
      m_resultDic = new SafeDictionary<AComputeRequest, SafeDictionary<uint, ComputeResult>>();
      m_requestList = new List<Tuple<AComputeRequest, List<int>>>();
      m_requestAxisList = new SafeDictionary<int, uint>();
    }

    public Tuple<AComputeRequest, List<Int32>> FindComputeRequest(Int32 p_requestId)
    {
      foreach (Tuple<AComputeRequest, List<Int32>> l_elem in m_requestList)
        foreach (Int32 l_requestId in l_elem.Item2)
          if (l_requestId == p_requestId)
            return (l_elem);
      return (null);
    }
  }
}
