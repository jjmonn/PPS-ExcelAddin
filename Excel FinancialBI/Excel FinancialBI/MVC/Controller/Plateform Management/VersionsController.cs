using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

    public VersionsController()
    {
      LoadView();
    }

    public override void LoadView()
    {
      m_view = new VersionsView();
      m_view.SetController(this);
      m_newVersionView.SetController(this);
      m_copyVersionView.SetController(this);
    }

    #region Validity checks

    bool IsCompatibleVersion(Version p_version, BaseVersion p_cmpVersion)
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
      uint l_baseVersionEndPeriod = (uint)DateTime.FromOADate(p_cmpVersion.StartPeriod).AddMonths(p_cmpVersion.NbPeriod).ToOADate();
      uint l_versionEndPeriod = 0;
      switch (p_version.TimeConfiguration)
      {
        case TimeConfig.YEARS :
          l_versionEndPeriod = (uint)DateTime.FromOADate(p_version.StartPeriod).AddYears(p_version.NbPeriod).ToOADate(); 
          break;
        case TimeConfig.MONTHS:
          l_versionEndPeriod = (uint)DateTime.FromOADate(p_version.StartPeriod).AddMonths(p_version.NbPeriod).ToOADate();
          break;
        case TimeConfig.DAYS :
          l_versionEndPeriod = (uint)DateTime.FromOADate(p_version.StartPeriod).AddDays(p_version.NbPeriod).ToOADate();
          break;
        default :
          Error = Local.GetValue("versions.error.time_config_not_supported");
          System.Diagnostics.Debug.WriteLine("Rates or gfacts versions compatibility check : tiem config not supported");          
          return (false);
      }

      if (p_version.StartPeriod < p_cmpVersion.StartPeriod || l_versionEndPeriod > l_baseVersionEndPeriod)
      {
        Error = Local.GetValue("versions.error.rate_or_gfact_version_not_compatible");
        return (false);
      }
      return (true);
    }

    bool IsVersionValid(Version p_version)
    {
      if (IsNameValid(p_version.Name) == false)
      {
        Error = Local.GetValue("general.error.name_in_use");
        return (false);
      }
      if (Enum.IsDefined(typeof(TimeConfig), p_version.TimeConfiguration) == false)
      {
        Error = Local.GetValue("version.error.invalid_time_config");
        return (false);
      }
      if (p_version.NbPeriod <= 0)
      {
        Error = Local.GetValue("version.error.invalid_nb_period");
        return (false);
      }
      if (IsCompatibleVersion(p_version, RatesVersionModel.Instance.GetValue(p_version.RateVersionId)) == false)
        return (false);
      if (IsCompatibleVersion(p_version, GlobalFactVersionModel.Instance.GetValue(p_version.GlobalFactVersionId)) == false)
        return (false);
      return (true);
    }

    #endregion

    public bool Create(Version p_version)
    {
      SetStartPeriod(p_version);
      if (!IsVersionValid(p_version))
        return (false);
      if (VersionModel.Instance.GetValue(p_version.Name) != null)
      {
        Error = Local.GetValue("general.error.name_in_use");
        return false;
      }
      VersionModel.Instance.Create(p_version);
      return (true);
    }

    public bool Update(Version p_version)
    {
      if (VersionModel.Instance.GetValue(p_version.Id) == null)
      {
        Error = Local.GetValue("general.error.system");
        System.Diagnostics.Debug.WriteLine("Refered version id does not exist, cannot be updated");
        return (false);
      }
      if (IsVersionValid(p_version) == false)
        return (false);
      VersionModel.Instance.Update(p_version);
      return (true);
    }

    public bool Delete(Version p_version)
    {
      if (VersionModel.Instance.GetValue(p_version.Id) == null)
      {
        Error = Local.GetValue("general.version.unknown_version");
        return (false);
      }
      VersionModel.Instance.Delete(p_version.Id);
      return (true);
    }

    public void ShowNewVersionView(uint p_parentVersionId)
    {
      m_newVersionView.m_parentId = p_parentVersionId;
      m_newVersionView.ShowDialog();
    }

    public void ShowVersionCopyView(uint p_id)
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

    private void SetStartPeriod(Version p_version)
    {
      switch (p_version.TimeConfiguration)
      {
        case TimeConfig.YEARS:
          p_version.StartPeriod = (uint)Period.GetYearIdFromPeriodId(Convert.ToInt32(p_version.StartPeriod));
          break;

        case TimeConfig.MONTHS:
          p_version.StartPeriod = (uint)Period.GetMonthIdFromPeriodId(Convert.ToInt32(p_version.StartPeriod));
          break;

        case TimeConfig.DAYS :
          // Nothing to do
          break;

        default :
          System.Diagnostics.Debug.WriteLine("Verison creation : starting period setup. Time configuration not handled.");
          break;
      }
    }

  }
}
