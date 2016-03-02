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

  public interface IAxisController : IController
  {
    AxisType AxisType { get; set; }

    void ShowNewAxisUI(UInt32 p_parentId = 0);
    bool CreateAxisElem(AxisElem p_axisElem);
    bool UpdateAxisFilter(AxisFilter p_axisFilter, UInt32 p_filterValueId);
    bool UpdateAxisElem(AxisElem p_axisElem);
    bool Delete(AxisElem p_elem);
  }

  public abstract class AxisBaseController<TView, TController> : NameController<TView>, IAxisController 
    where TView : ContainerControl, IPlatformMgtView
    where TController : class, IAxisController
  {
    public override IView View { get { return (m_view); } }
    public AxisType AxisType { get; set; }
    NewAxisUI<TController> m_newAxisUI;

    public AxisBaseController(AxisType p_axisType)
    {
      AxisType = p_axisType;

      m_newAxisUI = new NewAxisUI<TController>();
      m_newAxisUI.SetController(this);
    }

    public override void LoadView()
    {
      m_newAxisUI.LoadView();
    }

    public void ShowNewAxisUI(UInt32 p_parentId = 0)
    {
      if (p_parentId != 0)
        m_newAxisUI.ParentAxisElemId = p_parentId;
      m_newAxisUI.ShowDialog();
    }

    public bool Delete(AxisElem p_elem)
    {
      if (p_elem == null)
      {
        Error = Local.GetValue("axis.error.not_found");
        return (false);
      }
      if (AxisElemModel.Instance.Delete(p_elem.Id))
        return (true);
      Error = Local.GetValue("general.error.system");
      return (false);
    }

    bool IsAxisElemValid(AxisElem p_axisElem)
    {
      if (IsNameValid(p_axisElem.Name) == false)
        return (false);
      if (Enum.IsDefined(typeof(AxisType), p_axisElem.Axis) == false)
      {
        Error = Local.GetValue("axis.error.axis_type_invalid");
        return (false);
      }
      return (true);
    }

    public bool CreateAxisElem(AxisElem p_axisElem)
    {
      if (AxisElemModel.Instance.GetValue(p_axisElem.Id) != null)
      {
        Error = Local.GetValue("general.error.name_already_used");
        return (false);
      }
      if (!IsAxisElemValid(p_axisElem))
        return (false);
      if (AxisElemModel.Instance.Create(p_axisElem))
        return (true);
      Error = Local.GetValue("general.error.system");
      return (false);
    }

    public bool UpdateAxisFilter(AxisFilter p_axisFilter, UInt32 p_filterValueId)
    {
      if (p_axisFilter == null)
        return (false);
      p_axisFilter = p_axisFilter.Clone();

      p_axisFilter.FilterValueId = p_filterValueId;
      if (AxisFilterModel.Instance.Update(p_axisFilter))
        return (true);
      Error = Local.GetValue("general.error.system");
      return (false);
    }

    public bool UpdateAxisElem(AxisElem p_axisElem)
    {
      if (IsAxisElemValid(p_axisElem) == false)
        return (false);
      if (AxisElemModel.Instance.Update(p_axisElem))
        return (true);
      Error = Local.GetValue("general.error.system");
      return (false);
    }
  }
}
