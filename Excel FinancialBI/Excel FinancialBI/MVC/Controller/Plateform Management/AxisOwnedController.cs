using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using Model;
  using Model.CRUD;
  using View;
  using Utils;

  public class AxisOwnedController : AxisBaseController<AxisOwnedView, AxisOwnedController>
  {
    public UInt32 SelectedOwner { get; set; }
    public AxisType OwnerType { get; private set; }

    public AxisOwnedController(AxisType p_axisOwnerType, AxisType p_axisType) : base(p_axisType)
    {
      OwnerType = p_axisOwnerType;
      m_view = new AxisOwnedView();
      m_view.SetController(this);
      LoadView();
    }

    public override void LoadView()
    {
      base.LoadView();
      m_view.LoadView();
    }

    public bool IsAxisOwnerValid(AxisOwner p_axisOwner)
    {
      AxisElem l_axisElem = AxisElemModel.Instance.GetValue(p_axisOwner.Id);
      AxisElem l_owner = AxisElemModel.Instance.GetValue(OwnerType, p_axisOwner.OwnerId);

      if (l_axisElem == null)
      {
        Error = Local.GetValue("axis_owner.error.axis_elem_not_found");
        return (false);
      }
      if (l_owner == null)
      {
        Error = Local.GetValue("axis_owner.error.owner_not_found");
        return (false);
      }
      return (true);
    }

    public bool CreateAxisOwner(AxisOwner p_axisOwner)
    {
      if (IsAxisOwnerValid(p_axisOwner) == false)
        return (false);
      if (AxisOwnerModel.Instance.Create(p_axisOwner))
        return (true);
      Error = Local.GetValue("general.error.system");
      return (false);
    }

    public bool UpdateAxisOwner(AxisOwner p_axisOwner)
    {
      if (IsAxisOwnerValid(p_axisOwner) == false)
        return (false);
      if (AxisOwnerModel.Instance.Update(p_axisOwner))
        return (true);
      Error = Local.GetValue("general.error.system");
      return (false);
    }
  }
}
