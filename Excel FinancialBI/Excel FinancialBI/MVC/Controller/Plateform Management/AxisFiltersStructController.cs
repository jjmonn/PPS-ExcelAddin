using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FBI.MVC.Controller
{
  using View;
  using Model.CRUD;

  class AxisFiltersStructController : NameController
  {
    AxisType m_axisId;

    public AxisFiltersStructController(AxisType p_axisId)
    {
      m_axisId = p_axisId;
    }

    public void AddControlToPanel(Panel p_panel, PlatformMGTGeneralUI p_platformMgtUI)
    {
      //YEY
    }

    public bool Add(string p_filterName, UInt32 p_parentId)
    {
      Filter l_filter = new Filter();

      if (this.IsNameValidAndNotAlreadyUsed(p_filterName))
      {
        l_filter.Name = p_filterName;
        l_filter.ParentId = p_parentId;
        l_filter.Axis = m_axisId;
        l_filter.IsParent = false;
        return (true);
      }
      return (false);
    }
  }
}
