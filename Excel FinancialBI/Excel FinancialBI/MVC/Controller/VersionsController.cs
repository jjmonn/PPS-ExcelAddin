using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using View;
  using Model;
  using Model.CRUD;
  using Utils;

  public class VersionsController : IController
  {
    VersionsView m_view;
    public string Error { get; set; }

    public VersionsController(VersionsView p_view)
    {
      m_view = p_view;
      m_view.SetController(this);
      LoadView();
    }

    public void LoadView()
    {

    }

    #region Validity Check

    bool IsNameAlreadyUsed(string p_name)
    {
      if (VersionModel.Instance.GetValue(p_name) != null)
      {
        Error = Local.GetValue("general.error.name_already_used");
        return (true);
      }
      return (false);
    }

    bool IsNameValid(string p_name)
    {
      if (p_name.Length > Constants.NAMES_MAX_LENGTH)
      {
        Error = Local.GetValue("general.error.name_too_long");
        return (false);
      }
      if (StringUtils.ContainChars(p_name, Constants.FORBIDEN_CHARS))
      {
        Error = Local.GetValue("general.error.invalid_name");
        return (false);
      }
      return (true);
    }

    bool IsCompatibleVersion(Version p_version, BaseVersion p_cmpVersion)
    {
      if (p_cmpVersion == null)
      {
        Error = Local.GetValue("version.error.rate_or_gfact_version_undefined");
        return (false);
      }
      if (p_cmpVersion.IsFolder)
      {
        Error = Local.GetValue("version.error.rate_or_gfact_version_is_folder");
        return (false);
      }
      if (p_version.StartPeriod < p_cmpVersion.StartPeriod ||
        (p_version.NbPeriod + p_version.StartPeriod) > (p_cmpVersion.NbPeriod + p_cmpVersion.StartPeriod))
      {
        Error = Local.GetValue("version.error.rate_or_gfact_version_not_compatible");
        return (false);
      }
      return (true);
    }

    bool IsVersionValid(Version p_version)
    {
      if (IsNameValid(p_version.Name) == false)
        return (false);
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
      if (!IsVersionValid(p_version) || IsNameAlreadyUsed(p_version.Name))
        return (false);
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
  }
}
