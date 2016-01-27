using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialBI_ServerTest.Test
{
  class AuthenticationTest
  {
    NetworkManager[] networkMgr;
    NetworkLauncher[] networkLauncher;
    Authenticator[] auth;

    public void MultipleConnection(int p_count, Action<NetworkManager> p_callback, double p_cooldown)
    {
     /*networkMgr = new NetworkManager[p_count];
      networkLauncher = new NetworkLauncher[p_count];
      auth = new Authenticator[p_count];

      for (int i = 0; i < p_count; ++i)
      {
        networkMgr[i] = new NetworkManager();
        networkLauncher[i] = new NetworkLauncher("192.168.0.41", 4242, null, networkMgr[i]);
        auth[i] = new Authenticator(networkMgr[i]);

        auth[i].RequestAuthentication("1.0.1", "root", "root", p_callback);
        System.Threading.Thread.Sleep((int)(p_cooldown * 1000));
      }*/
    }
  }
}
