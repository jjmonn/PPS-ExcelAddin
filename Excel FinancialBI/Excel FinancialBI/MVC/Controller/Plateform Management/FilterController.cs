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
  using Utils;

  class FilterController : NameController<FiltersView>
  {
    public AxisType AxisType { get; private set; }
    public override IView View { get { return (m_view); } }

    public FilterController(AxisType p_axisType)
    {
      AxisType = p_axisType;
      m_view = new FiltersView();
      m_view.SetController(this);
      LoadView();
    }

    public override void LoadView()
    {
      m_view.LoadView();
    }

    public void ShowFilterStructView()
    {
      
    }

    public bool Add(string p_filterValueName, UInt32 p_filterId, UInt32 p_parentId)
    {
      FilterValue l_filter = new FilterValue();


      if (!FilterModel.Instance.HasChild(p_filterId, this.AxisType))
      {
        Error = Local.GetValue("filters.error.no_child");
        return (false);
      }
      if (this.IsNameValid(p_filterValueName) && FilterValueModel.Instance.GetValue(p_filterValueName) != null)
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
      if (l_filter != null && this.IsNameValid(p_filterValueNewName) && FilterValueModel.Instance.GetValue(p_filterValueNewName) != null)
      {
        l_filter.Name = p_filterValueNewName;
        FilterValueModel.Instance.Update(l_filter);
        return (true);
      }
      return (false);
    }
  }
}
