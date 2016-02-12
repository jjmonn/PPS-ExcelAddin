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
    public CuiDgvConf child { get; set; }
  }

  class FilterConf : CuiDgvConf
  {
    public FilterConf(UInt32 p_filterId) : base(typeof(Filter)) { FilterId = p_filterId; }
    public UInt32 FilterId { get; set; }
  }

  class AxisElemConf : CuiDgvConf
  {
    public AxisElemConf(AxisType p_axisType) : base(typeof(AxisElem)) { AxisTypeId = p_axisType; }
    public AxisType AxisTypeId { get; set; }
  }

  class PeriodConf : CuiDgvConf
  {
    public PeriodConf(TimeConfig p_config) : base(typeof(PeriodModel)) { PeriodType = p_config; }
    public TimeConfig PeriodType { get; set; }
  }
}
