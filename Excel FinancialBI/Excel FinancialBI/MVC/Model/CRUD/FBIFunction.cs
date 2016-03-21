using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  using Utils;

  class FBIFunction
  {
    public string Entity { get; set; }
    public string Account { get; set; }
    public string Period { get; set; }
    public string Currency { get; set; }
    public string Version { get; set; }
    public SafeDictionary<AxisType, List<string>> AxisElems { get; set; }
    public List<string> Filters { get; set; }

    public FBIFunction()
    {
      AxisElems = new SafeDictionary<AxisType, List<string>>();
      Filters = new List<string>();
    }
  }
}
