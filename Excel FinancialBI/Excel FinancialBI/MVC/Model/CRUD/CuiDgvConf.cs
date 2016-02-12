using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  class CuiDgvConf
  {
    public object Type { get; set; }
    public UInt32 FilterId { get; set; }
    public AxisType AxisTypeId { get; set; }
    public UInt32 StartPeriod { get; set; }
    public UInt32 NbPeriods { get; set; }
    public TimeConfig PeriodRange { get; set; }
  }
}
