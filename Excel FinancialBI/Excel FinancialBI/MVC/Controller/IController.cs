using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  public interface IController
  {
    void LoadView();
    string Error { get; private set; }
  }
}