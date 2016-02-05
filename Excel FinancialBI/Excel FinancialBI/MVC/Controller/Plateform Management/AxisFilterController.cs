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

  class AxisFilterController : NameController
  {
    /*public override void AddControlToPanel(Panel p_panel, PlatformMGTGeneralUI p_platformMgtUI)
    {
      //
    }*/

    public override void Close()
    {
      // Add any dispose action here !
      /* if (m_view != null)
       {
         m_view.Hide();
         m_view.Dispose();
         m_view = null;
       }*/
    }

    public override void LoadView()
    {
      //
    }

    public bool Add(string p_filterValueName, UInt32 p_filterId, UInt32 p_parentId)
    {
      FilterValue l_filter = new FilterValue();

      if (this.IsNameValidAndNotAlreadyUsed(p_filterValueName))
      {
        l_filter.Name = p_filterValueName;
        l_filter.FilterId = p_filterId;
        l_filter.ParentId = p_parentId;
        FilterValueModel.Instance.Create(l_filter);
        return (true);
      }
      return (false);
    }

    public bool Remove(UInt32 p_filterValue)
    {
      FilterValueModel.Instance.Delete(p_filterValue);
      return (true);
    }

    public bool Update(UInt32 p_filterValue, string p_filterValueNewName)
    {
      FilterValue l_filter;

      l_filter = FilterValueModel.Instance.GetValue(p_filterValue).Clone();
      if (l_filter != null && this.IsNameValidAndNotAlreadyUsed(p_filterValueNewName))
      {
        l_filter.Name = p_filterValueNewName;
        FilterValueModel.Instance.Update(l_filter);
        return (true);
      }
      return (false);
    }
  }
}
