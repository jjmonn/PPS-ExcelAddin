using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  public class CUIDimensionConf
  {
    public CUIDimensionConf(Type p_type) { ModelType = p_type; }
    public Type ModelType { get; set; }
    public CUIDimensionConf Child { get; set; }
  }

  class FilterConf : CUIDimensionConf
  {
    public FilterConf(UInt32 p_filterId) : base(typeof(Filter)) { FilterId = p_filterId; }
    public UInt32 FilterId { get; set; }
  }

  class AxisElemConf : CUIDimensionConf
  {
    public AxisElemConf(AxisType p_axisType) : base(typeof(AxisElem)) { AxisTypeId = p_axisType; }
    public AxisType AxisTypeId { get; set; }
  }

  class PeriodConf : CUIDimensionConf
  {
    public PeriodConf(TimeConfig p_config) : base(typeof(PeriodModel)) { PeriodType = p_config; }
    public TimeConfig PeriodType { get; set; }
  }

  class VersionConf : CUIDimensionConf
  {
    public VersionConf(UInt32 p_version1, UInt32 p_version2 = 0) : base(typeof(Version))
    {
      Version1 = p_version1;
      Version2 = p_version2;
    }
    public UInt32 Version1 { get; set; }
    public UInt32 Version2 { get; set; }
  }
}
