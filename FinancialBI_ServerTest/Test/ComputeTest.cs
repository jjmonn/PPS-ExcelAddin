using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialBI_ServerTest.Test
{
  class ComputeTest
  {
    static public void Compute(NetworkManager p_networkMgr, UInt32 p_nbCompute)
    {

      ByteBuffer l_packet = new ByteBuffer((ushort)ClientMessage.CMSG_COMPUTE_REQUEST);

      int l_requestId = l_packet.AssignRequestId();
      l_packet.WriteUint32(1);  // process
      l_packet.WriteUint32(1);  // version
      l_packet.WriteUint32(1);  // globalFactVersion
      l_packet.WriteUint32(1);  // rateVersion
      l_packet.WriteUint32(8);  // entityId
      l_packet.WriteUint32(34); // currency
      l_packet.WriteUint32(42356); //startPeriod
      l_packet.WriteUint32(61); // nbPeriods
      l_packet.WriteBool(true); // axis hierarchy decomposition
      l_packet.WriteBool(true); // entity decomposition

      l_packet.WriteUint32(0);  // nbFilter
      l_packet.WriteUint32(0);  // nbAxisElem
      l_packet.WriteUint32(1);  // nbSort
      l_packet.WriteUint32(2);  // sort axis
      l_packet.WriteBool(true); // is axis sort

      l_packet.Release();

      for (int i = 0; i < p_nbCompute; ++i)
        p_networkMgr.Send(l_packet);
    }
  }
}
