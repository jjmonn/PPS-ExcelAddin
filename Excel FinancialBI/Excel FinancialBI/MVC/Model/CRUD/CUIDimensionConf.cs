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
    public virtual CUIDimensionConf Clone()
    {
      CUIDimensionConf l_new = new CUIDimensionConf(ModelType);

      return (Copy(l_new));
    }

    protected CUIDimensionConf Copy(CUIDimensionConf p_dest)
    {
      p_dest.Child = Child;
      p_dest.ModelType = ModelType;
      return (p_dest);
    }
  }

  class FilterConf : CUIDimensionConf
  {
    public FilterConf(UInt32 p_filterId) : base(typeof(FilterModel)) { FilterId = p_filterId; }
    public UInt32 FilterId { get; set; }
    
    public override CUIDimensionConf Clone()
    {
      FilterConf l_new = new FilterConf(FilterId);

      return (Copy(l_new));
    }
  }

  class AxisElemConf : CUIDimensionConf
  {
    public AxisElemConf(AxisType p_axisType) : base(typeof(AxisElemModel)) { AxisTypeId = p_axisType; }
    public AxisType AxisTypeId { get; set; }

    public override CUIDimensionConf Clone()
    {
      AxisElemConf l_new = new AxisElemConf(AxisTypeId);

      return (Copy(l_new));
    }
  }

  class PeriodConf : CUIDimensionConf
  {
    public PeriodConf(TimeConfig p_config) : base(typeof(PeriodModel)) { PeriodType = p_config; }
    public TimeConfig PeriodType { get; set; }

    public override CUIDimensionConf Clone()
    {
      PeriodConf l_new = new PeriodConf(PeriodType);

      return (Copy(l_new));
    }
  }

  class VersionConf : CUIDimensionConf
  {
    public VersionConf(UInt32 p_version1, UInt32 p_version2 = 0) : base(typeof(VersionModel))
    {
      Version1 = p_version1;
      Version2 = p_version2;
    }
    public UInt32 Version1 { get; set; }
    public UInt32 Version2 { get; set; }

    public override CUIDimensionConf Clone()
    {
      VersionConf l_new = new VersionConf(Version1, Version2);

      return (Copy(l_new));
    }
  }
}
