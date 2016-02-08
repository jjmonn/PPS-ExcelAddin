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
    FbiDataGridView m_dgv = new FbiDataGridView();
    AxisType m_axisType;
    AxisController m_controller = null;
    bool cellModif = false;

    public AxisView(AxisType p_axisType)
    {
      InitializeComponent();
      m_axisType = p_axisType;
      AxisFilterModel.Instance.ReadEvent += Instance_ReadEvent;
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
      UInt32 l_axisElemId = (UInt32)args.Cell.RowItem.ItemValue;
      UInt32 l_filterId = (UInt32)args.Cell.ColumnItem.ItemValue;
      AxisFilter l_axisFilter = AxisFilterModel.Instance.GetValue(m_axisType, l_axisElemId, l_filterId);
      FilterValue l_filterValue = FilterValueModel.Instance.GetValue(args.Cell.Value.ToString());
      this.m_controller.Add(l_axisFilter, l_filterValue);
    }

    void changeParentCellValue(uint p_axisElemId, uint p_filterValueId)
    {
      FilterValue l_filterValue = FilterValueModel.Instance.GetValue(p_filterValueId);
      if (l_filterValue == null)
        return;
      m_dgv.FillField(p_axisElemId, p_filterValueId, l_filterValue.Name);
    }

    void l_cbEditor_SelectedIndexChanged(object sender, EventArgs e)
    {
      cellModif = true;
    }

    void Instance_ReadEvent(Network.ErrorMessage status, AxisFilter attributes)
    {
      if (status == Network.ErrorMessage.SUCCESS)
      {
        AxisFilterModel.Instance.Update(attributes);
        AxisElem l_filterValue = AxisElemModel.Instance.GetValue(attributes.Axis, attributes.AxisElemId);
        changeParentCellValue(attributes.AxisElemId, l_filterValue.ParentId);
      }
      throw new NotImplementedException();
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as AxisController;
    }
  }
}
