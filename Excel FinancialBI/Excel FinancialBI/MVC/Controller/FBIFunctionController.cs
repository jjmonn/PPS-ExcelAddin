using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using View;
  using Model.CRUD;

  class FBIFunctionController : IController
  {
    FBIFunctionView m_view;
    public string Error { get; set; }
    public IView View { get { return (m_view); } }

    public FBIFunctionController()
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

    public string Function
    {
      get
      {
        string l_func = "PPSBI(";

        l_func += "\"" + m_view.SelectedEntity + "\";";
        l_func += "\"" + m_view.SelectedAccount + "\";";
        l_func += "\"" + m_view.SelectedPeriod + "\";";
        l_func += "\"" + m_view.SelectedCurrency + "\";";
        l_func += "\"" + m_view.SelectedVersion + "\";";

        l_func += "\"" + GetListParameter(m_view.GetSelectedAxisElem(AxisType.Entities)) + "\";";
        l_func += "\"" + GetListParameter(m_view.GetSelectedAxisElem(AxisType.Client)) + "\";";
        l_func += "\"" + GetListParameter(m_view.GetSelectedAxisElem(AxisType.Product)) + "\";";
        l_func += "\"" + GetListParameter(m_view.GetSelectedAxisElem(AxisType.Adjustment)) + "\";";
        l_func += "\"" + GetListParameter(m_view.SelectedFilterValues) + "\";";

        l_func += ")";
        return (l_func);
      }
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
