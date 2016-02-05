using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using Model;
  using Model.CRUD;

  public class AxisFiltersStructController : NameController
  {
    private AxisType m_axisId;

    public AxisFiltersStructController(AxisType p_axisId)
    {
      m_axisId = p_axisId;
    }

    public bool Add(string p_filter, UInt32 p_parentId)
    {
      Filter l_filter = new Filter();

      if (this.IsNameValidAndNotAlreadyUsed(p_filter))
      {
        l_filter.Name = p_filter;
        l_filter.ParentId = p_parentId;
        l_filter.Axis = m_axisId;
        l_filter.IsParent = false;
        FilterModel.Instance.Create(l_filter);
        return (true);
      }
      return (false);
    }

    public bool Remove(UInt32 p_filterId)
    {
      FilterModel.Instance.Delete(p_filterId);
      return (true);
    }
  }
}
