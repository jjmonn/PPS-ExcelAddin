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
    public AxisType AxisType { get; set; }

    public AxisController(AxisType p_axisType)
    {
      AxisType = p_axisType;
      m_view = new AxisView();
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

    public bool Create(AxisElem p_axisElem)
    {
      if (IsNameValidAndNotAlreadyUsed(p_axisElem.Name) == false)
        return (false);
      if (Enum.IsDefined(typeof(AxisType), p_axisElem.Axis) == false)
      {
        Error = Local.GetValue("axis.error.axis_type_invalid");
        return (false);
      }
      AxisElemModel.Instance.Create(p_axisElem);
      return (true);
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
