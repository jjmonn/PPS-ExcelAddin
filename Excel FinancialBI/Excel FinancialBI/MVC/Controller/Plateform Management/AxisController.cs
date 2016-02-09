﻿using System;
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
    NewAxisUI m_newAxisUI;

    public AxisController(AxisType p_axisType)
    {
      AxisType = p_axisType;
      m_view = new AxisView();
      m_view.SetController(this);
      m_newAxisUI = new NewAxisUI();
      m_newAxisUI.SetController(this);
      LoadView();
    }

    public override void LoadView()
    {
      m_view.LoadView();
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
      AxisElemModel.Instance.Delete(p_elem.Id);
      return (true);
    }

    public bool Create(AxisElem p_axisElem)
    {
      if (IsNameValid(p_axisElem.Name) == false || AxisElemModel.Instance.GetValue(p_axisElem.Id) == null)
        return (false);
      if (Enum.IsDefined(typeof(AxisType), p_axisElem.Axis) == false)
      {
        Error = Local.GetValue("axis.error.axis_type_invalid");
        return (false);
      }
      AxisElemModel.Instance.Create(p_axisElem);
      return (true);
    }

    public bool UpdateAxisFilter(AxisFilter p_axisFilter, FilterValue p_filterValue)
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