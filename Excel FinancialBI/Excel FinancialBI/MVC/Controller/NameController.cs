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
  using Model.CRUD;

  public abstract class NameController<T> : BasePlatformMgtComponent<T>, IController where T : ContainerControl, IView
  {
    public string Error { get; set; }
    public abstract IView View { get; }

    abstract public void LoadView();

    public bool IsNameValid(string p_name)
    {
      if (p_name.Trim() == "")
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
