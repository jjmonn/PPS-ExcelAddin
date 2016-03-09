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
  using System.Windows.Forms;

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
    public static dynamic HostApplication { get; set; }
    public static AddinModule AddinModule { get; set; }

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
      SuscribeModel<LegalHoliday>(LegalHolidayModel.Instance, p_suscribeEvent);
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
    }

    public static void Disconnect()
    {
      m_networkLauncher.Stop();
    }

    public static bool Connect(string p_userName, string p_password)
    {
      bool result;

      UserName = p_userName;
      Password = p_password;
      UserModel.Instance.CurrentUserName = UserName;
      m_networkLauncher = new NetworkLauncher();
      if ((result = m_networkLauncher.Launch(Properties.Settings.Default.serverIp, Properties.Settings.Default.port_number, OnDisconnect)) == true)
        Authenticator.Instance.AskAuthentication(UserName, Password);
      if (ConnectionStateEvent != null)
        ConnectionStateEvent(result);
      return (result);
    }

    static void OnDisconnect()
    {
      bool failed = true;
      Int32 nbTry = 10;

      ConnectionStateEvent(false);
      while (failed && nbTry > 0)
      {
        System.Threading.Thread.Sleep(3000);
        failed = !Connect(UserName, Password);
        nbTry--;
      }
      if (ConnectionStateEvent != null)
        ConnectionStateEvent(!failed);
    }

    public static void SuscribeAutoLock(ContainerControl p_control)
    {
      p_control.Enabled = m_networkLauncher.GetState() == ClientState.running;
      Addin.ConnectionStateEvent += delegate(bool p_connected) { LockView(p_connected, p_control); };
    }

    delegate void LockView_delegate(bool p_connected, ContainerControl p_control);
    public static void LockView(bool p_connected, ContainerControl p_control)
    {
      if (p_control.InvokeRequired)
      {
        LockView_delegate func = new LockView_delegate(LockView);
        p_control.Invoke(func, p_connected, p_control);
      }
      else
      {
        p_control.Enabled = p_connected;
      }
    }

    public static UInt32 VersionId
    {
      get { return (Properties.Settings.Default.version_id); }
      set
      {
        string l_name = VersionModel.Instance.GetValueName(value);

        if (l_name == "")
          l_name = Local.GetValue("general.select_version");
        AddinModule.m_versionRibbonButton.Caption = l_name;
        Properties.Settings.Default.version_id = value;
        Properties.Settings.Default.Save();
      }
    }

    public static Account.AccountProcess Process
    {
      get { return ((Account.AccountProcess)Properties.Settings.Default.processId); }
      set
      {
        string l_name;

        switch (value)
        {
          case Account.AccountProcess.RH:
            l_name = Local.GetValue("process.process_rh");
            break;
          case Account.AccountProcess.FINANCIAL:
            l_name = Local.GetValue("process.process_financial");
            break;
          default:
            l_name = Local.GetValue("process.select_process");
            break;
        }
        AddinModule.m_processRibbonButton.Caption = l_name;
        Properties.Settings.Default.processId = (Int32)value;
        Properties.Settings.Default.Save();
      }
    }
  }
}
