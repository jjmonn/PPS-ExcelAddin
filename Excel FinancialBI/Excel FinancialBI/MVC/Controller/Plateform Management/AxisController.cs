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
  }
}
