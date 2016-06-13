using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FinancialBI_ServerTest
{
  class Program
  {
    static int i = 0;
    static void Main(string[] args)
    {
      NetworkLauncher netlaunch = new NetworkLauncher();

      netlaunch.Launch("52.49.116.246", 4242);
      Authenticator auth = new Authenticator(NetworkManager.GetInstance());

      auth.RequestAuthentication("1.0.1", "root", "root", Compute);
      /*
     Test.AuthenticationTest authTest = new Test.AuthenticationTest();

     authTest.MultipleConnection(200, BeginTest, 0);*/
    }

    static void Compute(NetworkManager p_netMgr)
    {
      //Test.ComputeTest.Compute(p_netMgr, 100);
    }

    static void BeginTest(NetworkManager p_netMgr)
    {
     /* p_netMgr.SetCallback((UInt16)ServerMessage.SMSG_READ_GROUP_ANSWER, CreateSuccess);
      ByteBuffer packet = new ByteBuffer((UInt16)ClientMessage.CMSG_CREATE_GROUP);
      CRUD.Group group = new CRUD.Group();

      group.Name = i.ToString();
      group.Dump(packet, false);
      packet.Release();
      p_netMgr.Send(packet);
      ++i;*/
     // Test.DownloadTest.Download(p_netMgr);
    }

    static void CreateSuccess(ByteBuffer packet)
    {
      Debug.WriteLine("Group " + i);
    }
  }
}
