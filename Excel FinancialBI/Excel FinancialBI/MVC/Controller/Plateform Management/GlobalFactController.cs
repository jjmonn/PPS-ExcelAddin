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

  public class GlobalFactController : FactBaseController<GlobalFactView, GlobalFactVersion>
  {
    public GlobalFactController() : base()
    {
      m_view = new GlobalFactView();
      m_view.SetController(this);
      SetNewVersionUIController(this);
      LoadView();
    }

    public override void LoadView()
    {
      base.LoadView();
      m_view.LoadView();
    }

    public bool IsGFactDataValid(GlobalFactData p_gfactData)
    {
      if (GlobalFactModel.Instance.GetValue(p_gfactData.GlobalFactId) == null)
      {
        Error = Local.GetValue("gfactdata.error.gfact_not_found");
        return (false);
      }
      if (GlobalFactVersionModel.Instance.GetValue(p_gfactData.VersionId) == null)
      {
        Error = Local.GetValue("gfactdata.error.version_not_found");
        return (false);
      }
      return (true);
    }

    public override bool CreateVersion(GlobalFactVersion p_version)
    {
      if (!IsVersionValid(p_version, GlobalFactVersionModel.Instance) || !IsVersionNameAvailable(p_version.Name, GlobalFactVersionModel.Instance))
        return (false);
      GlobalFactVersionModel.Instance.Create(p_version);
      return (true);
    }

    public override bool UpdateVersion(GlobalFactVersion p_version)
    {
      if (!IsVersionValid(p_version, GlobalFactVersionModel.Instance))
        return (false);
      GlobalFactVersionModel.Instance.Update(p_version);
      return (true);
    }

    public override bool DeleteVersion(UInt32 p_versionId)
    {
      if (GlobalFactVersionModel.Instance.GetValue(p_versionId) == null)
      {
        Error = Local.GetValue("general.error.already_exist");
        return (false);
      }
      GlobalFactVersionModel.Instance.Delete(p_versionId);
      return (true);
    }

    public bool CreateGFactData(GlobalFactData p_gfactData)
    {
      if (IsGFactDataValid(p_gfactData) == false)
        return (false);
      if (GlobalFactDataModel.Instance.GetValue(p_gfactData.GlobalFactId, p_gfactData.Period, p_gfactData.VersionId) != null)
      {
        Error = Local.GetValue("general.error.already_exist");
        return (false);
      }
      GlobalFactDataModel.Instance.Create(p_gfactData);
      return (true);
    }

    public bool UpdateGFactData(GlobalFactData p_gfactData)
    {
      if (IsGFactDataValid(p_gfactData) == false)
        return (false);
      GlobalFactDataModel.Instance.Update(p_gfactData);
      return (true);
    }

    public bool UpdateGFact(GlobalFact p_gfact)
    {
      if (IsNameValid(p_gfact.Name) == false)
        return (false);
      GlobalFactModel.Instance.Update(p_gfact);
      return (true);
    }

    public bool CreateGFact(GlobalFact p_gfact)
    {
      if (IsNameValid(p_gfact.Name) == false)
        return (false);
      if (GlobalFactModel.Instance.GetValue(p_gfact.Name) != null)
      {
        Error = Local.GetValue("general.error.name_already_used");
        return (false);
      }
      GlobalFactModel.Instance.Create(p_gfact);
      return (true);
    }

    public bool DeleteGFact(UInt32 p_gfactId)
    {
      if (GlobalFactModel.Instance.GetValue(p_gfactId) == null)
      {
        Error = Local.GetValue("general.error.not_found");
        return (false);
      }
      GlobalFactModel.Instance.Delete(p_gfactId);
      return (true);
    }
  }
}
