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
  using FBI.Utils;
  using MVC.Model.CRUD;
  using MVC.Model;

  static class Addin
  {
    static public event DisconnectEventHandler DisconnectEvent;
    public delegate void DisconnectEventHandler();
    static public event ConnectEventHandler ConnectEvent;
    public delegate void ConnectEventHandler();

    static NetworkLauncher m_networkLauncher = new NetworkLauncher();

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

    static void InitModels()
    {
      ICRUDModel<AxisElem> l_axisElem = AxisElemModel.Instance;
      ICRUDModel<Filter> l_filter = FilterModel.Instance;
      ICRUDModel<FilterValue> l_filterValue = FilterValueModel.Instance;
      ICRUDModel<AxisFilter> l_axisFilter = AxisFilterModel.Instance;
      ICRUDModel<Version> l_version = VersionModel.Instance;
    }

    public static void Main()
    {
      InitModels();
      SelectLanguage();
      SetCurrentProcessId(Settings.Default.processId);
    }

    public static void SetCurrentProcessId(int p_processId)
    {
      Settings.Default.processId = (int)p_processId;
      Settings.Default.Save();
      if (p_processId == (int)Account.AccountProcess.FINANCIAL)
      {
        AddinModule.CurrentInstance.SetProcessCaption(Local.GetValue("process.process_financial"));
      }
      else
      {
        AddinModule.CurrentInstance.SetProcessCaption(Local.GetValue("process.process_rh"));
      }

    }

    public static bool Connect(string p_userName, string p_password)
    {
      return (Connect_Intern(p_userName, p_password));
    }

    static bool Connect_Intern(string p_userName = "", string p_password = "")
    {
      if (m_networkLauncher.Launch("192.168.0.41", 4242, OnDisconnect) == true)
      {
        if (ConnectEvent != null)
          ConnectEvent();
        Authenticator.Instance.AskAuthentication(p_userName, p_password);
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
        Connect_Intern();
      }
      if (DisconnectEvent != null)
        DisconnectEvent();
    }




  }
}
