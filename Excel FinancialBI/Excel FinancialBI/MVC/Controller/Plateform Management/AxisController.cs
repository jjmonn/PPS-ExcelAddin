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

  class AxisController : NameController<AxisView>
  {
    public override IView View { get { return (m_view); } }

    public AxisController(AxisType p_axisType)
    {
      m_view = new AxisView(p_axisType);
      m_view.SetController(this);
      m_view.LoadView();
    }

    public override void LoadView()
    {
      throw new NotImplementedException();
    }

    public bool Add(AxisFilter p_axisFilter, FilterValue p_filterValue)
    {
      if (p_axisFilter == null || p_filterValue == null)
        return (false);
      p_axisFilter = p_axisFilter.Clone();

      p_axisFilter.FilterValueId = p_filterValue.Id;
      AxisFilterModel.Instance.Update(p_axisFilter);
      return (true);
    }
  }
}
