﻿using System;
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
    private NewDataVersionUI m_newVersionView;

    public VersionsController()
    {
      LoadView();
    }

    public override void LoadView()
    {
      m_view = new VersionsView();
      m_view.SetController(this);
      m_newVersionView = new NewDataVersionUI();
      m_newVersionView.SetController(this);
    }

    #region Validity checks
  
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

    public void ShowNewVersionView(uint p_parentId)
    {
      m_newVersionView.m_parentId = p_parentId;
      m_newVersionView.Show();
    }

    public void ShowVersionCopyView()
    {
      // TO DO
    }


  }
}
