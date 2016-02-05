using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FBI.MVC.Controller
{
  using View;
  using Model;
  using Model.CRUD;

  class AxisFiltersStructController : NameController
  {
    private AxisType m_axisType;

    public AxisFiltersStructController(AxisType p_axisType)
    {
      m_axisType = p_axisType;
    }
    public override void Close()
    {
      //TODO
    }

    public override void LoadView()
    {
      //TODO
    }

    public bool Add(string p_filterName, UInt32 p_parentId)
    {
      Filter l_filter = new Filter();

      if (this.IsNameValidAndNotAlreadyUsed(p_filterName))
      {
        l_filter.Name = p_filterName;
        l_filter.ParentId = p_parentId;
        l_filter.Axis = m_axisType;
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

    public bool Update(UInt32 p_filterId, string p_filterNewName)
    {
      Filter l_filter;

      l_filter = FilterModel.Instance.GetValue(p_filterId).Clone();
      if (l_filter != null && this.IsNameValidAndNotAlreadyUsed(p_filterNewName))
      {
        l_filter.Name = p_filterNewName;
        FilterModel.Instance.Update(l_filter);
        return (true);
      }
      return (false);
    }
  }
}
