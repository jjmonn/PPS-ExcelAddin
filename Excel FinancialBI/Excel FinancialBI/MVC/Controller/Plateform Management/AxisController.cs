using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using View;
  using Model;
  using Model.CRUD;
  using Utils;

  class AxisController : AxisBaseController<AxisView, AxisController>
  {
    public AxisController(AxisType p_axisType) : base(p_axisType)
    {
      m_view = new AxisView();
      m_view.SetController(this);
      LoadView();
    }

    public override void LoadView()
    {
      base.LoadView();
      m_view.LoadView();
    }

  }
}
