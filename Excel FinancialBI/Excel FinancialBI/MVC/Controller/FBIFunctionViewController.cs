using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace FBI.MVC.Controller
{
  using View;
  using Model.CRUD;
  using Utils;
  using Model;

  class FBIFunctionViewController : AFBIFunctionController
  {
    FBIFunctionView m_view;
    public override IView View { get { return (m_view); } }

    public FBIFunctionViewController()
    {
      m_view = new FBIFunctionView();
      m_view.SetController(this);
      LoadView();
    }

    void LoadView()
    {
      m_view.LoadView();
      m_view.Show();
    }

    string Formula
    {
      get
      {
        string l_func = "=FBI(";

        l_func += "\"" + m_view.SelectedEntity + "\",";
        l_func += "\"" + m_view.SelectedAccount + "\",";
        l_func += "\"" + m_view.SelectedPeriod + "\",";
        l_func += "\"" + m_view.SelectedCurrency + "\",";
        l_func += "\"" + m_view.SelectedVersion + "\",";

        l_func += "\"" + GetListParameter(m_view.GetSelectedAxisElem(AxisType.Client)) + "\",";
        l_func += "\"" + GetListParameter(m_view.GetSelectedAxisElem(AxisType.Product)) + "\",";
        l_func += "\"" + GetListParameter(m_view.GetSelectedAxisElem(AxisType.Adjustment)) + "\",";
        l_func += "\"" + GetListParameter(m_view.SelectedFilterValues) + "\"";

        l_func += ")";
        return (l_func);
      }
    }

    bool CheckFunctionValidity()
    {
      return (
        IsValidEntity(m_view.SelectedEntity) &&
        IsValidAccount(m_view.SelectedAccount) &&
        IsValidCurrency(m_view.SelectedCurrency) &&
        IsValidVersion(m_view.SelectedVersion) &&
        IsValidAxisElemList(AxisType.Client, m_view.GetSelectedAxisElem(AxisType.Client)) &&
        IsValidAxisElemList(AxisType.Product, m_view.GetSelectedAxisElem(AxisType.Product)) &&
        IsValidAxisElemList(AxisType.Adjustment, m_view.GetSelectedAxisElem(AxisType.Adjustment)) &&
        IsValidFilterValueList(m_view.SelectedFilterValues));
    }

    public bool SaveFunction()
    {
      if (CheckFunctionValidity() == false)
        return (false);
      Range l_cell = AddinModule.CurrentInstance.ExcelApp.ActiveCell;

      if (l_cell == null)
      {
        Error = Local.GetValue("ppsbi.no_cell_selected");
        return (false);
      }
      try
      {
        l_cell.Formula = Formula;
      }
      catch (Exception)
      {
        Error = Local.GetValue("general.error.system");
        return (false);
      }
      return (true);
    }

    string GetListParameter(List<string> p_list)
    {
      string l_parameter = "";

      foreach (string l_elem in p_list)
        l_parameter += l_elem + ((p_list.Last() == l_elem) ? "" : ",");
      return (l_parameter);
    }
  }
}
