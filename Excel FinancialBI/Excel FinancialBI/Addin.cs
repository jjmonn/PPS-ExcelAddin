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

    static void InitModels(bool p_suscribeEvent = true)
    {
      SuscribeModel<Account>(AccountModel.Instance, p_suscribeEvent);
      SuscribeModel<AxisElem>(AxisElemModel.Instance, p_suscribeEvent);
      SuscribeModel<AxisFilter>(AxisFilterModel.Instance, p_suscribeEvent);
      SuscribeModel<AxisOwner>(AxisOwnerModel.Instance, p_suscribeEvent);
      SuscribeModel<Commit>(CommitModel.Instance, p_suscribeEvent);
      SuscribeModel<Currency>(CurrencyModel.Instance, p_suscribeEvent);
      SuscribeModel<EntityCurrency>(EntityCurrencyModel.Instance, p_suscribeEvent);
      SuscribeModel<EntityDistribution>(EntityDistributionModel.Instance, p_suscribeEvent);
      SuscribeModel<FactTag>(FactTagModel.Instance, p_suscribeEvent);
      SuscribeModel<Filter>(FilterModel.Instance, p_suscribeEvent);
      SuscribeModel<FilterValue>(FilterValueModel.Instance, p_suscribeEvent);
      SuscribeModel<FModelingAccount>(FModelingAccountModel.Instance, p_suscribeEvent);
      SuscribeModel<GlobalFactData>(GlobalFactDataModel.Instance, p_suscribeEvent);
      SuscribeModel<GlobalFact>(GlobalFactModel.Instance, p_suscribeEvent);
      SuscribeModel<GlobalFactVersion>(GlobalFactVersionModel.Instance, p_suscribeEvent);
      SuscribeModel<Group>(GroupModel.Instance, p_suscribeEvent);
      SuscribeModel<ExchangeRateVersion>(RatesVersionModel.Instance, p_suscribeEvent);
      SuscribeModel<UserAllowedEntity>(UserAllowedEntityModel.Instance, p_suscribeEvent);
      SuscribeModel<User>(UserModel.Instance, p_suscribeEvent);
      SuscribeModel<Version>(VersionModel.Instance, p_suscribeEvent);
      SuscribeModel<ExchangeRate>(ExchangeRateModel.Instance, p_suscribeEvent);
    }

    static void SuscribeModel<T>(ICRUDModel<T> p_model, bool p_suscribeEvent) where T : class, CRUDEntity
    {
      if (p_suscribeEvent)
        p_model.ObjectInitialized += OnModelInit;
      m_initTypeSet.Add(typeof(T));
    }

    static void OnModelInit(ErrorMessage p_success, Type p_type)
    {
      if (p_success == ErrorMessage.SUCCESS && m_initTypeSet.Contains(p_type))
      {
        m_initTypeSet.Remove(p_type);
        if (m_initTypeSet.Count == 0 && InitializationEvent != null)
        {
          InitializationEvent();
          InitModels(false);
        }
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
      if ((result = m_networkLauncher.Launch("82.125.98.175", 4242, OnDisconnect)) == true)
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
        failed = !Connect(UserName, Password);
        nbTry--;
      }
      if (ConnectionStateEvent != null)
        ConnectionStateEvent(!failed);
    }

  }
}
