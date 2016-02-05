using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FBI.MVC.Controller
{
  using Utils;
  using View;
  using Model;

  public abstract class NameController : IController, IPlatformManagementController
  {
    public string Error { get; set; }

    abstract public void LoadView();
    abstract public void Close();
    abstract public void AddControlToPanel(Panel p_panel, PlatformMGTGeneralUI p_platformMgtUI);

    public bool IsNameValidAndNotAlreadyUsed(string p_name)
    {
      return (this.IsNameValid(p_name) && !this.IsNameAlreadyUsed(p_name));
    }

    public bool IsNameAlreadyUsed(string p_name)
    {
      if (VersionModel.Instance.GetValue(p_name) != null)
      {
        Error = Local.GetValue("general.error.name_already_used");
        return (true);
      }
      return (false);
    }

    public bool IsNameValid(string p_name)
    {
      if (p_name == "")
      {
        Error = Local.GetValue("general.error.name_empty");
        return (false);
      }
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

  }
}
