using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using Model.CRUD;
  using View;
  using GraphSortedDic = SafeDictionary<UInt32, SafeDictionary<Int32, SafeDictionary<Tuple<bool, Model.CRUD.AxisType, UInt32>, double>>>;
  using GraphUnSortedDic = SafeDictionary<UInt32, SafeDictionary<Int32, List<double>>>;

  class CUIVisualizationController : IController
  {
    CUIVisualization m_view;

    public string Error { get; set; }
    public IView View { get { return (m_view); } }

    SafeDictionary<ResultKey, double> m_values;
    SafeDictionary<ResultKey, double> m_accountValues;

    public CUIVisualizationController()
    {
      m_view = new CUIVisualization();
      m_view.SetController(this);
      LoadView();
    }

    void LoadView()
    {
      m_view.LoadView();
      m_view.Show();
    }


    void LoadData(SafeDictionary<ResultKey, double> p_values)
    {
      m_values = p_values;
      SelectAccount(1);
    }

    public void SelectAccount(UInt32 p_accountId)
    {
      m_accountValues = new SafeDictionary<ResultKey, double>();

      foreach (KeyValuePair<ResultKey, double> l_pair in m_values)
        if (l_pair.Key.AccountId == p_accountId)
          m_accountValues[l_pair.Key] = l_pair.Value;
    }

    GraphSortedDic BuildSortedValues(bool p_isAxis, AxisType p_axis, UInt32 p_id = 0)
    {
      GraphSortedDic l_sortedValues = new GraphSortedDic();

      foreach (KeyValuePair<ResultKey, double> l_pair in m_accountValues)
        if (l_pair.Key.IsSort(p_isAxis, p_axis, p_id))
        {
          Tuple<bool, AxisType, UInt32> l_sort = l_pair.Key.LastSort;

          if (l_sort != null)
            l_sortedValues[l_pair.Key.VersionId][l_pair.Key.Period][l_sort] = l_pair.Value;
        }
      return (l_sortedValues);
    }

    GraphUnSortedDic BuildUnsortedValues()
    {
      GraphUnSortedDic l_values = new GraphUnSortedDic();

      foreach (KeyValuePair<ResultKey, double> l_pair in m_accountValues)
        if (l_pair.Key.SortHash == "")
          l_values[l_pair.Key.VersionId][l_pair.Key.Period].Add(l_pair.Value);
      return (l_values);
    }
  }
}
