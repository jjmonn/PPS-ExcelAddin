using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  public class CuiDgvConf
  {
    public CuiDgvConf(Type p_type) { ModelType = p_type; }
    public Type ModelType { get; set; }
    public UInt32 Id { get; set; }
    public CuiDgvConf child { get; set; }
  }

  class AxisElemConf : CuiDgvConf
  {
    public AxisElemConf(AxisType p_axisType) : base(typeof(AxisElem)) { AxisTypeId = p_axisType; }
    public AxisType AxisTypeId { get; set; }
  }

  class PeriodConf : CuiDgvConf
  {
    public PeriodConf(TimeConfig p_config) : base(typeof(PeriodModel)) { PeriodRange = p_config; }
    public int StartPeriod { get; set; }
    public int NbPeriods { get; set; }
    public TimeConfig PeriodRange { get; set; }
  }
}
