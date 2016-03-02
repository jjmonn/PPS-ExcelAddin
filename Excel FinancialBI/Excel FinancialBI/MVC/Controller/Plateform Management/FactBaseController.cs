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

  public interface IFactBaseController<TVersion> : IController where TVersion : BaseVersion, NamedCRUDEntity, new()
  {
    UInt32 SelectedVersion { get; set; }
    bool CreateVersion(TVersion p_version);
    bool UpdateVersion(TVersion p_version);
    bool DeleteVersion(UInt32 p_versionId);
    void ShowNewVersionUI();
  };

  public abstract class FactBaseController<TView, TVersion> : NameController<TView>, IFactBaseController<TVersion>
    where TView : ContainerControl, IPlatformMgtView
    where TVersion : BaseVersion, NamedCRUDEntity, new()
  {
    public override IView View { get { return (m_view); } }
    public UInt32 SelectedVersion { get; set; }
    NewFactBaseVersionView<FactBaseController<TView, TVersion>, TVersion> m_newVersionUI;

    public FactBaseController()
    {
      m_newVersionUI = new NewFactBaseVersionView<FactBaseController<TView,TVersion>,TVersion>();
    }

    public override void LoadView()
    {
      m_newVersionUI.LoadView();
    }

    public void SetNewVersionUIController(IController p_controller)
    {
      m_newVersionUI.SetController(p_controller);
    }

    public void ShowNewVersionUI()
    {
      m_newVersionUI.SelectedParent = SelectedVersion;
      m_newVersionUI.ShowDialog();
    }

    #region Checks

    protected bool IsVersionNameAvailable(string p_name, NamedCRUDModel<TVersion> p_model)
    {
      if (p_model.GetValue(p_name) != null)
      {
        Error = Local.GetValue("general.error.name_already_used");
        return (false);
      }
      return (true);
    }

    protected bool IsVersionValid(TVersion p_version, NamedCRUDModel<TVersion> p_model)
    {
      if (!IsNameValid(p_version.Name))
        return (false);
      if (p_version.IsFolder == false && p_version.NbPeriod == 0)
      {
        Error = Local.GetValue("version.error.nb_period");
        return (false);
      }
      if (p_version.ParentId != 0)
      {
        TVersion l_parentVersion = p_model.GetValue(p_version.ParentId);

        if (l_parentVersion == null || l_parentVersion.IsFolder == false)
        {
          Error = Local.GetValue("version.error.parent_is_not_folder");
          return (false);
        }
      }
      return (true);
    }

    public abstract bool CreateVersion(TVersion p_version);
    public abstract bool UpdateVersion(TVersion p_version);
    public abstract bool DeleteVersion(UInt32 p_versionId);

    #endregion
  }
}
