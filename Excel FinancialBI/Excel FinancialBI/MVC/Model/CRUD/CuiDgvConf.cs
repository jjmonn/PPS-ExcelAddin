using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  public class CuiDgvConf
  {
    public Type ModelType { get; set; }
    public UInt32 FilterId { get; set; }
    public CuiDgvConf Child { get; set; }
  }

  class AxisElemConf : CuiDgvConf
  {
    public AxisType AxisTypeId { get; set; }
  }

  class PeriodConf : CuiDgvConf
  {
    public int StartPeriod { get; set; }
    public int NbPeriods { get; set; }
    public TimeConfig PeriodRange { get; set; }
  }
}
