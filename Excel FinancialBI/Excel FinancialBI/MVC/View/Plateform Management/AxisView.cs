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
    AxisController m_controller = null;
    bool cellModif = false;

    public AxisView(AxisType p_axisType)
    {
      InitializeComponent();
      m_axisType = p_axisType;
    }
    
    public void LoadView()
    {
      Dock = DockStyle.Fill;
      MultiIndexDictionary<UInt32, string, AxisElem> l_axisElemDic = AxisElemModel.Instance.GetDictionary(m_axisType);
      MultiIndexDictionary<UInt32, string, Filter> l_filterDic = FilterModel.Instance.GetDictionary(m_axisType);

      TableLayoutPanel1.Controls.Add(m_dgv);
      if (l_axisElemDic == null)
        return;
      m_dgv.InitializeRows(AxisElemModel.Instance, l_axisElemDic);
      if (l_filterDic == null)
        return;
      m_dgv.InitializeColumns(FilterModel.Instance, l_filterDic);
      FillDGV();
    }

    void FillDGV()
    {
      MultiIndexDictionary<UInt32, Tuple<UInt32, UInt32>, AxisFilter> l_axisFilterDic = AxisFilterModel.Instance.GetDictionary(m_axisType);

      if (l_axisFilterDic == null)
        return;
      foreach (AxisFilter l_axisFilter in l_axisFilterDic.Values)
      {
        FilterValue l_filterValue = FilterValueModel.Instance.GetValue(l_axisFilter.FilterValueId);
        ComboBoxEditor l_cbEditor = new ComboBoxEditor();
        MultiIndexDictionary<UInt32, string, FilterValue> l_filterValueDic = FilterValueModel.Instance.GetDictionary(l_axisFilter.FilterId);

        if (l_filterValueDic != null && l_cbEditor != null)
        {
          foreach (FilterValue l_fv in l_filterValueDic.Values)
            l_cbEditor.Items.Add(l_fv.Name);
          l_cbEditor.SelectedIndexChanged += l_cbEditor_SelectedIndexChanged;
        }
        if (l_filterValue != null && l_cbEditor != null)
        {
          m_dgv.FillField(l_axisFilter.AxisElemId, l_axisFilter.FilterId, l_filterValue.Name, l_cbEditor);
          this.fillParentsColumn(l_filterValue.Id, l_filterValue.ParentId, l_axisFilter);
        }
      }
      m_dgv.CellValueChanged += m_dgv_CellValueChanged;
    }

    void fillParentsColumn(uint p_filterValueId, uint p_parentId, AxisFilter p_axisFilter)
    {
      if (p_filterValueId != 0 && p_parentId != 0)
      {
        FilterValue l_parent = FilterValueModel.Instance.GetValue(p_parentId);
        MultiIndexDictionary<UInt32, string, FilterValue> l_filterValueDic = FilterValueModel.Instance.GetDictionary(l_parent.FilterId);
        ComboBoxEditor l_cbEditor = new ComboBoxEditor();
        l_cbEditor.SelectedIndexChanged += l_cbEditor_SelectedIndexChanged;
        if (l_filterValueDic != null && l_cbEditor != null)
          foreach (FilterValue l_fv in l_filterValueDic.Values)
            l_cbEditor.Items.Add(l_fv.Name);
        m_dgv.FillField(p_axisFilter.AxisElemId, l_parent.FilterId, l_parent.Name, l_cbEditor);
      }
    }

    private void m_dgv_CellValueChanged(object sender, CellEventArgs args)
    {
      if (cellModif == false)
        return;
      cellModif = false;
      
      changeParentCellValue(FilterValueModel.Instance.GetValue(args.Cell.Value.ToString()), args.Cell.RowItem);
    }

    void changeParentCellValue(FilterValue p_filterValue, HierarchyItem p_row)
    {
      if (p_filterValue == null)
        return;
      if (p_filterValue.ParentId == 0)
        return;
      FilterValue l_parent = FilterValueModel.Instance.GetValue(p_filterValue.ParentId);
      if (l_parent == null)
        return;
      m_dgv.FillField(p_row, l_parent.FilterId, l_parent.Name);
    }

    void l_cbEditor_SelectedIndexChanged(object sender, EventArgs e)
    {
      cellModif = true;
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as AxisController;
    }
  }
}
