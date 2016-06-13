using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialBI_ServerTest.Test
{
  class DownloadTest
  {
    static public void Download(NetworkManager p_networkMgr)
    {
      ByteBuffer packet = new ByteBuffer((UInt16)ClientMessage.CMSG_TEST);

      packet.Release();
      p_networkMgr.Send(packet);
    }
  }
}
