using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace FBI.MVC.Controller
{
  using View;
  using Model;
  using Model.CRUD;
  using Utils;

  public class VersionsController : NameController<VersionsView>
  {
    public override IView View { get { return (m_view); } }
    private NewDataVersionUI m_newVersionView = new NewDataVersionUI();
    private CopyVersionView m_copyVersionView = new CopyVersionView();
    public UInt32 SelectedVersion { get; set; }
    ManualResetEvent m_wait;
    UInt32 m_waitId;

    public VersionsController()
    {
      LoadView();
      GlobalFactVersionModel.Instance.UpdateEvent += OnUpdateFactVersion;
      RatesVersionModel.Instance.UpdateEvent += OnUpdateFactVersion;
    }

    public override void LoadView()
    {
      m_view = new VersionsView();
      m_view.SetController(this);
      m_newVersionView.SetController(this);
      m_copyVersionView.SetController(this);
    }

    public override void Close()
    {
      GlobalFactVersionModel.Instance.UpdateEvent -= OnUpdateFactVersion;
      RatesVersionModel.Instance.UpdateEvent -= OnUpdateFactVersion;
      base.Close();
    }

    public bool ResizeFactVersion<TVersion>(Version p_version, TVersion p_cmpVersion, NamedCRUDModel<TVersion> p_model) where TVersion : BaseVersion, NamedCRUDEntity
    {
      UInt32 l_versionEndPeriod = GetVersionEndPeriod(p_version, p_version.TimeConfiguration);
      UInt32 l_cmpVersionEndPeriod = GetVersionEndPeriod(p_cmpVersion, TimeConfig.MONTHS);

      p_cmpVersion.StartPeriod = (p_version.StartPeriod < p_cmpVersion.StartPeriod) ? p_version.StartPeriod : p_cmpVersion.StartPeriod;
      p_cmpVersion.NbPeriod = (ushort)(
        PeriodModel.GetMonthPeriodListFromPeriodsRange(
        DateTime.FromOADate(p_cmpVersion.StartPeriod),
        DateTime.FromOADate((l_versionEndPeriod > l_cmpVersionEndPeriod) ? l_versionEndPeriod : l_cmpVersionEndPeriod)).Count);

      m_wait = new ManualResetEvent(false);
      if (p_model.Update(p_cmpVersion) == false)
        return (false);
      m_waitId = p_cmpVersion.Id;
      if (m_wait.WaitOne(2000) == false)
        return (false); 
      return (true);
    }

    void OnUpdateFactVersion(Network.ErrorMessage p_status, UInt32 p_id)
    {
      if (p_id != m_waitId || p_status != Network.ErrorMessage.SUCCESS)
        return;
      m_wait.Set();
    }

    #region Validity checks

    UInt32 GetVersionEndPeriod(BaseVersion p_version, TimeConfig p_config)
    {
      UInt32 l_versionEndPeriod = 0;
      switch (p_config)
      {
        case TimeConfig.YEARS:
          l_versionEndPeriod = (UInt32)DateTime.FromOADate(p_version.StartPeriod).AddYears(p_version.NbPeriod).ToOADate();
          break;
        case TimeConfig.MONTHS:
          l_versionEndPeriod = (UInt32)DateTime.FromOADate(p_version.StartPeriod).AddMonths(p_version.NbPeriod).ToOADate();
          break;
        case TimeConfig.DAYS:
          l_versionEndPeriod = (UInt32)DateTime.FromOADate(p_version.StartPeriod).AddDays(p_version.NbPeriod).ToOADate();
          break;
        default:
          Error = Local.GetValue("versions.error.time_config_not_supported");
          System.Diagnostics.Debug.WriteLine("Rates or gfacts versions compatibility check : time config not supported");
          return (0);
      }
      return (l_versionEndPeriod);
    }

    public bool IsCompatibleVersion(Version p_version, BaseVersion p_cmpVersion)
    {
      if (p_cmpVersion == null)
        return (true);

      UInt32 l_baseVersionEndPeriod = GetVersionEndPeriod(p_cmpVersion, TimeConfig.MONTHS);
      UInt32 l_versionEndPeriod = GetVersionEndPeriod(p_version, p_version.TimeConfiguration);

      if (l_versionEndPeriod == 0)
        return (false);

      if (p_version.StartPeriod < p_cmpVersion.StartPeriod || l_versionEndPeriod > l_baseVersionEndPeriod)
      {
        Error = Local.GetValue("versions.error.rate_or_gfact_version_not_compatible");
        return (false);
      }
      return (true);
    }

    bool IsValidVersion(Version p_version, BaseVersion p_cmpVersion)
    {
      if (p_cmpVersion == null)
      {
        Error = Local.GetValue("versions.error.rate_or_gfact_version_undefined");
        return (false);
      }
      if (p_cmpVersion.IsFolder)
      {
        Error = Local.GetValue("versions.error.rate_or_gfact_version_is_folder");
        return (false);
      }
      return (IsCompatibleVersion(p_version, p_cmpVersion));
    }

    bool IsVersionValid(Version p_version)
    {
      if (IsNameValid(p_version.Name) == false)
      {
        Error = Local.GetValue("general.error.name_in_use");
        return (false);
      }
      if (p_version.ParentId != 0)
      {
        Version l_parent = VersionModel.Instance.GetValue(p_version.ParentId);

        if (l_parent == null || l_parent.IsFolder == false)
        {
          Error = Local.GetValue("versions.error.invalid_parent");
          return (false);
        }
      }
      if (p_version.IsFolder == false)
      {
        if (Enum.IsDefined(typeof(TimeConfig), p_version.TimeConfiguration) == false)
        {
          Error = Local.GetValue("versions.error.invalid_time_config");
          return (false);
        }
        if (p_version.NbPeriod <= 0)
        {
          Error = Local.GetValue("versions.error.invalid_nb_period");
          return (false);
        }
        if (IsValidVersion(p_version, RatesVersionModel.Instance.GetValue(p_version.RateVersionId)) == false)
          return (false);
        if (IsValidVersion(p_version, GlobalFactVersionModel.Instance.GetValue(p_version.GlobalFactVersionId)) == false)
          return (false);
      }
      if (p_version.FormulaPeriodIndex > p_version.NbPeriod || p_version.FormulaNbPeriod > p_version.NbPeriod || 
        p_version.FormulaNbPeriod + p_version.FormulaPeriodIndex > p_version.NbPeriod || p_version.FormulaNbPeriod == 0)
      {
        Error = Local.GetValue("versions.error.formula_period_range_invalid");
        return (false);
      }
      return (true);
    }

    #endregion

    public bool Create(Version p_version)
    {
      if (!IsVersionValid(p_version))
        return (false);
      if (VersionModel.Instance.GetValue(p_version.Name) != null)
      {
        Error = Local.GetValue("general.error.name_in_use");
        return false;
      }
      if (VersionModel.Instance.Create(p_version))
        return (true);
      Error = Local.GetValue("general.error.system");
      return (false);
    }

    public bool Update(Version p_version)
    {
      if (VersionModel.Instance.GetValue(p_version.Id) == null)
      {
        Error = Local.GetValue("general.error.system");
        return (false);
      }
      if (IsVersionValid(p_version) == false)
        return (false);
      if (VersionModel.Instance.Update(p_version))
        return (true);
      Error = Local.GetValue("general.error.system");
      return (false);
    }

    public bool Delete(UInt32 p_versionId)
    {
      if (VersionModel.Instance.GetValue(p_versionId) == null)
      {
        Error = Local.GetValue("facts_versions.unknown_version");
        return (false);
      }
      if (VersionModel.Instance.Delete(p_versionId))
        return (true);
      Error = Local.GetValue("general.error.system");
      return (false);
    }

    public void ShowNewVersionView(UInt32 p_parentVersionId)
    {
      Version l_parentVersion = VersionModel.Instance.GetValue(p_parentVersionId);
      if (l_parentVersion == null || l_parentVersion.IsFolder == false)
      {
        p_parentVersionId = 0;
      }
      m_newVersionView.m_parentId = p_parentVersionId;
      m_newVersionView.ShowDialog();
    }

    public void ShowVersionCopyView(UInt32 p_id)
    {
      Version l_version = VersionModel.Instance.GetValue(p_id);
      if (l_version == null)
      {
        Error= Local.GetValue("versions.error.no_selected_version");
        return;
      }
      if (l_version.IsFolder == true)
      {
        Error = Local.GetValue("versions.error.cannot_copy_folder");
        return;
      }
      m_copyVersionView.SetCopiedVersion(l_version);
      m_copyVersionView.Show();
    }

    public void SetStartPeriod(Version p_version)
    {
      switch (p_version.TimeConfiguration)
      {
        case TimeConfig.YEARS:
          p_version.StartPeriod = (UInt32)PeriodModel.GetYearIdFromPeriodId(Convert.ToInt32(p_version.StartPeriod));
          break;

        case TimeConfig.MONTHS:
          p_version.StartPeriod = (UInt32)PeriodModel.GetMonthIdFromPeriodId(Convert.ToInt32(p_version.StartPeriod));
          break;

        case TimeConfig.DAYS :
          // Nothing to do
          break;

        default :
          System.Diagnostics.Debug.WriteLine("Version creation : starting period setup. Time configuration not handled.");
          break;
      }
    }

  }
}
