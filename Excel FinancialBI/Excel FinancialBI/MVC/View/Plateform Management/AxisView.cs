using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VIBlend.WinForms.DataGridView;

namespace FBI.MVC.View
{
  using Controller;
  using Model;
  using Model.CRUD;
  using Utils;
  using FBI.Forms;

  public partial class AxisView : UserControl, IView
  {
    FbiDataGridView<AxisElem, Filter, string> m_dgv = new FbiDataGridView<AxisElem,Filter,string>();
    AxisType m_axisType;

    public AxisView(AxisType p_axisType)
    {
      InitializeComponent();
      m_axisType = p_axisType;
    }
    
    public void LoadView()
    {
      MultiIndexDictionary<UInt32, string, AxisElem> l_axisElemDic = AxisElemModel.Instance.GetDictionary(m_axisType);
      MultiIndexDictionary<UInt32, string, Filter> l_filterDic = FilterModel.Instance.GetDictionary(m_axisType);

      m_dgv.InitializeRows(AxisElemModel.Instance, l_axisElemDic);
      m_dgv.InitializeColumns(FilterModel.Instance, l_filterDic);
      FillDGV();
      Controls.Add(m_dgv);
    }

    void FillDGV()
    {
      MultiIndexDictionary<UInt32, Tuple<UInt32, UInt32>, AxisFilter> l_axisFilterDic = AxisFilterModel.Instance.GetDictionary(m_axisType);

      foreach (AxisFilter l_axisFilter in l_axisFilterDic.Values)
      {
        FilterValue l_filterValue = FilterValueModel.Instance.GetValue(l_axisFilter.FilterValueId);

        if (l_filterValue != null)
        {
          ComboBoxEditor l_cbEditor = new ComboBoxEditor();
          MultiIndexDictionary<UInt32, string, FilterValue> l_filterValueDic = FilterValueModel.Instance.GetDictionary(l_axisFilter.FilterId);

          if (l_filterValueDic != null)
            foreach (FilterValue l_fv in l_filterValueDic.Values)
            {
              l_cbEditor.Items.Add(l_fv.Name);
            }
          m_dgv.FillField(l_axisFilter.AxisElemId, l_axisFilter.FilterValueId, l_filterValue.Name, l_cbEditor);
        }
      }
    }

    public void SetController(IController p_controller)
    {

    }
  }
}
