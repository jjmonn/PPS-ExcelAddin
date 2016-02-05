using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI
{
  using Network;
  using Properties;
  using Utils;

  static class Addin
  {
    static public event DisconnectEventHandler DisconnectEvent;
    public delegate void DisconnectEventHandler();
    static public event ConnectEventHandler ConnectEvent;
    public delegate void ConnectEventHandler();

    static NetworkLauncher m_networkLauncher = new NetworkLauncher();
    public static bool ConnectionTaskPaneVisible { get; set; }

    static void SelectLanguage()
    {
      switch (Settings.Default.language)
      {
        case 0:
          Local.LoadLocalFile(Properties.Resources.english);
          break;
        case 1:
          Local.LoadLocalFile(Properties.Resources.french);
          break;
        default:
          Local.LoadLocalFile(Properties.Resources.english);
          break;
      }
    }

    public static void Main()
    {
      //SelectLanguage();
    }

    static bool Connect()
    {
      if (m_networkLauncher.Launch("192.168.0.41", 4242, OnDisconnect) == true)
      {
        if (ConnectEvent != null)
          ConnectEvent();
        Authenticator.Instance.AskAuthentication("root", "root");
        return (true);
      }
      return (false);
    }

    static void OnDisconnect()
    {
      bool failed = true;
      Int32 nbTry = 10;

      while (failed && nbTry > 0)
      {
        System.Threading.Thread.Sleep(3000);
        Connect();
      }
      if (DisconnectEvent != null)
        DisconnectEvent();
    }

  }
}
