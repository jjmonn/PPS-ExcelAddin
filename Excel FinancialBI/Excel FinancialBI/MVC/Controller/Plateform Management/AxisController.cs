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

  class AxisController : NameController<AxisView>
  {
    public override IView View { get { return (m_view); } }

    public AxisController(AxisType p_axisType)
    {
      m_view = new AxisView(p_axisType);
      m_view.SetController(this);
      LoadView();
    }

    public override void LoadView()
    {
      m_view.LoadView();
    }

    public bool Delete(AxisElem p_elem)
    {
      if (p_elem == null)
      {
        Error = Local.GetValue("axis.error.not_found");
        return (false);
      }
      AxisElemModel.Instance.Delete(p_elem.Id);
      return (true);
    }
  }
}
