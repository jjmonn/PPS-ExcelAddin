using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using Utils;
  using MVC.Model;

  public abstract class NameController : IController
  {
    public string Error { get; set; }

    abstract public void LoadView();

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
