using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  public interface IExcelImportController : IController
  {
    void LoadView(UInt32 p_version);

    bool Create(UInt32 p_id, List<Int32> p_periods, List<double> p_values);
  }
}
