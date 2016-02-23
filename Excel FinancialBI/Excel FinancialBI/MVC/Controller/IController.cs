using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using View;

  public interface IController
  {
    string Error { get; set; }
    IView View { get; }
  }
}