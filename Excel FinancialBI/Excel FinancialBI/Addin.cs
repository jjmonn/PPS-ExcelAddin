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
  using MVC.Model.CRUD;
  using MVC.Model;
  using VIBlend.Utilities;

  static class Addin
  {
    static public event ConnectionStateEventHandler ConnectionStateEvent;
    public delegate void ConnectionStateEventHandler(bool p_connected);
    static HashSet<Type> m_initTypeSet = new HashSet<Type>();
    public static event InitializationEventHandler InitializationEvent;
    public delegate void InitializationEventHandler();
    public static VIBLEND_THEME VIBLEND_THEME = VIBLEND_THEME.VISTABLUE;
    static NetworkLauncher m_networkLauncher = new NetworkLauncher();
    public static string UserName { get; private set; }
    public static string Password { get; private set; }

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
      SuscribeModel<Account>(AccountModel.Instance);
      SuscribeModel<AxisElem>(AxisElemModel.Instance);
      SuscribeModel<AxisFilter>(AxisFilterModel.Instance);
      SuscribeModel<AxisOwner>(AxisOwnerModel.Instance);
      SuscribeModel<Commit>(CommitModel.Instance);
      SuscribeModel<Currency>(CurrencyModel.Instance);
      SuscribeModel<EntityCurrency>(EntityCurrencyModel.Instance);
      SuscribeModel<EntityDistribution>(EntityDistributionModel.Instance);
      SuscribeModel<FactTag>(FactTagModel.Instance);
      SuscribeModel<Filter>(FilterModel.Instance);
      SuscribeModel<FilterValue>(FilterValueModel.Instance);
      SuscribeModel<FModelingAccount>(FModelingAccountModel.Instance);
      SuscribeModel<GlobalFactData>(GlobalFactDataModel.Instance);
      SuscribeModel<GlobalFact>(GlobalFactModel.Instance);
      SuscribeModel<GlobalFactVersion>(GlobalFactVersionModel.Instance);
      SuscribeModel<Group>(GroupModel.Instance);
      SuscribeModel<ExchangeRateVersion>(RatesVersionModel.Instance);
      SuscribeModel<UserAllowedEntity>(UserAllowedEntityModel.Instance);
      SuscribeModel<User>(UserModel.Instance);
      SuscribeModel<Version>(VersionModel.Instance);
    }

    static void SuscribeModel<T>(ICRUDModel<T> p_model) where T : class, CRUDEntity
    {
      p_model.ObjectInitialized += OnModelInit;
      m_initTypeSet.Add(typeof(T));
    }

    static void OnModelInit(ErrorMessage p_success, Type p_type)
    {
      if (p_success == ErrorMessage.SUCCESS && m_initTypeSet.Contains(p_type))
      {
        System.Threading.Thread.Sleep(100);
        m_initTypeSet.Remove(p_type);
        if (m_initTypeSet.Count == 0 && InitializationEvent != null)
          InitializationEvent();
      }
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
      bool result;

      UserName = p_userName;
      Password = p_password;
      UserModel.Instance.CurrentUserName = UserName;
      if ((result = m_networkLauncher.Launch("192.168.0.41", 4242, OnDisconnect)) == true)
        Authenticator.Instance.AskAuthentication(UserName, Password);
      if (ConnectionStateEvent != null)
        ConnectionStateEvent(result);
      return (result);
    }

    static void OnDisconnect()
    {
      bool failed = true;
      Int32 nbTry = 10;

      while (failed && nbTry > 0)
      {
        System.Threading.Thread.Sleep(3000);
        Connect(UserName, Password);
      }
      if (ConnectionStateEvent != null)
        ConnectionStateEvent(false);
    }
  }
}
