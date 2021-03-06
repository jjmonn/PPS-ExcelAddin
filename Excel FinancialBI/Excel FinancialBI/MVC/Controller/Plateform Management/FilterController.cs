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

  class FilterController : NameController<FilterView>
  {
    public AxisType AxisType { get; private set; }
    public override IView View { get { return (m_view); } }
    FilterStructView m_filterStructView;

    public FilterController(AxisType p_axisType)
    {
      AxisType = p_axisType;
      m_view = new FilterView();
      m_view.SetController(this);
      m_filterStructView = new FilterStructView();
      m_filterStructView.SetController(this);
      LoadView();
    }

    public override void LoadView()
    {
      m_view.LoadView();
      m_filterStructView.LoadView();
    }

    public void ShowFilterStructView()
    {
      m_filterStructView.ShowDialog();
      m_view.Reload();
    }

    #region Add

    public bool Add(string p_filterName, UInt32 p_parentId, UInt32 p_filterId, Type p_type)
    {
      if (p_filterName == "") //If the user has entered nothing, or if he has canceled, don't show an error, and do nothing.
      {
        return (true);
      }
      if (p_type == typeof(Filter))
      {
        return (this.AddCategory(p_filterName, p_parentId));
      }
      if (p_type == typeof(FilterValue))
      {
        return (this.AddValue(p_filterName, p_filterId, p_parentId));
      }
      return (false);
    }

    public bool AddCategory(string p_filterName, UInt32 p_parentId)
    {
      Filter l_filter = new Filter();

      if (this.IsNameValid(p_filterName) && FilterModel.Instance.GetValue(p_filterName) == null)
      {
        l_filter.Name = p_filterName;
        l_filter.ParentId = p_parentId;
        l_filter.Axis = AxisType;
        l_filter.IsParent = false;
        if (FilterModel.Instance.Create(l_filter))
          return (true);
        Error = Local.GetValue("general.error.system");
      }
      else
        Error = Local.GetValue("general.error.name_already_used");
      return (false);
    }

    public bool AddValue(string p_filterValueName, UInt32 p_filterId, UInt32 p_parentId)
    {
      FilterValue l_filter = new FilterValue();

      if (this.IsNameValid(p_filterValueName) && FilterValueModel.Instance.GetValue(p_filterValueName) == null)
      {
        l_filter.Name = p_filterValueName;
        l_filter.FilterId = p_filterId;
        l_filter.ParentId = p_parentId;
        if (FilterValueModel.Instance.Create(l_filter))
          return (true);
        Error = Local.GetValue("general.error.system");
      }
      else
        Error = Local.GetValue("general.error.name_already_used");
      return (false);
    }

    #endregion

    #region Remove

    public bool Remove(UInt32 p_filterId, Type p_type)
    {
      if (p_type == typeof(Filter))
        return (this.RemoveCategory(p_filterId));
      if (p_type == typeof(FilterValue))
        return (this.RemoveValue(p_filterId));
      return (false);
    }

    public bool RemoveCategory(UInt32 p_filterId)
    {
      if (FilterModel.Instance.Delete(p_filterId))
        return (true);
      Error = Local.GetValue("general.error.system");
      return (false);
    }

    public bool RemoveValue(UInt32 p_filterValue)
    {
      if (FilterValueModel.Instance.Delete(p_filterValue))
        return (true);
      Error = Local.GetValue("general.error.system");
      return (false);
    }

    #endregion

    #region Update

    public bool Update(UInt32 p_filterId, string p_filterName, Type p_type)
    {
      if (p_filterName == "")
        return (true);
      if (p_type == typeof(Filter))
        return (this.UpdateCategory(p_filterId, p_filterName));
      if (p_type == typeof(FilterValue))
        return (this.UpdateValue(p_filterId, p_filterName));
      return (false);
    }

    public bool UpdateValue(UInt32 p_filterId, UInt32 p_newParentId, UInt32 p_newFilterId)
    {
      FilterValue l_tmp, l_filterVal;

      if ((l_tmp = FilterValueModel.Instance.GetValue(p_filterId)) == null)
        return (false);
      l_filterVal = l_tmp.Clone();
      if (l_filterVal != null)
      {
        l_filterVal.ParentId = p_newParentId;
        l_filterVal.FilterId = p_newFilterId;
        if (FilterValueModel.Instance.Update(l_filterVal))
          return (true);
        Error = Local.GetValue("general.error.system");
      }
      return (false);
    }

    public bool UpdateCategory(UInt32 p_filterId, string p_filterNewName)
    {
      Filter l_tmp, l_filter;

      if ((l_tmp = FilterModel.Instance.GetValue(p_filterId)) == null)
        return (false);
      l_filter = l_tmp.Clone();
      if (l_filter != null && this.IsNameValid(p_filterNewName) && FilterModel.Instance.GetValue(AxisType, p_filterNewName) == null)
      {
        l_filter.Name = p_filterNewName;
        if (FilterModel.Instance.Update(l_filter))
          return (true);
        Error = Local.GetValue("general.error.system");
      }
      return (false);
    }

    public bool UpdateValue(UInt32 p_filterValue, string p_filterValueNewName)
    {
      FilterValue l_tmp, l_filterVal;

      if ((l_tmp = FilterValueModel.Instance.GetValue(p_filterValue)) == null)
        return (false);
      l_filterVal = l_tmp.Clone();
      if (l_filterVal != null && this.IsNameValid(p_filterValueNewName) && FilterValueModel.Instance.GetValue(p_filterValueNewName) == null)
      {
        l_filterVal.Name = p_filterValueNewName;
        if (FilterValueModel.Instance.Update(l_filterVal))
          return (true);
        Error = Local.GetValue("general.error.system");
      }
      return (false);
    }

    #endregion

  }
}
