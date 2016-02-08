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
  using Network;

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
      MultiIndexDictionary<UInt32, string, Filter> l_filterDic = FilterModel.Instance.GetSortedByParentsDictionary(m_axisType);

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
        MultiIndexDictionary<UInt32, string, FilterValue> l_filterValueDic = FilterValueModel.Instance.GetDictionary(l_axisFilter.FilterId);
        ComboBoxEditor l_cbEditor = null;

        if (l_filterValueDic != null)
        {
          l_cbEditor = BuildComboBoxEditor(l_filterValueDic);
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
        if (l_parent == null)
          return;
        MultiIndexDictionary<UInt32, string, FilterValue> l_filterValueDic = FilterValueModel.Instance.GetDictionary(l_parent.FilterId);
        if (l_filterValueDic == null)
          return;
        ComboBoxEditor l_cbEditor = BuildComboBoxEditor(l_filterValueDic);
        if (l_cbEditor == null)
          m_dgv.FillField(p_axisFilter.AxisElemId, l_parent.FilterId, l_parent.Name, l_cbEditor);
      }
    }

    private ComboBoxEditor BuildComboBoxEditor(MultiIndexDictionary<UInt32, string, FilterValue> p_filterValueDic)
    {
      ComboBoxEditor l_cbEditor = new ComboBoxEditor();
      if (p_filterValueDic != null && l_cbEditor != null)
      {
        l_cbEditor.SelectedIndexChanged += l_cbEditor_SelectedIndexChanged;
        foreach (FilterValue l_fv in p_filterValueDic.SortedValues)
          l_cbEditor.Items.Add(l_fv.Name);
      }
      return (l_cbEditor);
    }

    private void m_dgv_CellValueChanged(object sender, CellEventArgs args)
    {
      if (cellModif == false || args == null)
        return;
      cellModif = false;
      UInt32 l_axisElemId = (UInt32)args.Cell.RowItem.ItemValue;
      UInt32 l_filterId = (UInt32)args.Cell.ColumnItem.ItemValue;
      AxisFilter l_axisFilter = AxisFilterModel.Instance.GetValue(m_axisType, l_axisElemId, l_filterId);
      FilterValue l_filterValue = FilterValueModel.Instance.GetValue(args.Cell.Value.ToString());
      Filter l_filter = FilterModel.Instance.GetValue(l_filterId);

      if (l_filter.IsParent == true)
      {
        Filter l_filterChild = FilterModel.Instance.GetValue(FilterModel.Instance.FindChildId(m_axisType, l_filterId));
        if (l_filterChild != null)
        {
          AxisFilter l_axisFilterChild = AxisFilterModel.Instance.GetValue(m_axisType, l_axisFilter.AxisElemId, l_filterChild.Id);
          if (l_axisFilterChild != null)
          {
            MultiIndexDictionary<UInt32, string, FilterValue> l_filterValueDic = FilterValueModel.Instance.GetChildrenDictionary(l_filterValue.Id);
            ComboBoxEditor l_cbEditor = BuildComboBoxEditor(l_filterValueDic);
            if (l_cbEditor != null && l_filterValueDic.SortedValues.Count > 0)
              m_dgv.FillField(l_axisElemId, l_filterChild.Id, l_filterValueDic.SortedValues[0].Name, l_cbEditor);
          }
        }
      }
      if (l_axisFilter != null && l_filterValue != null)
        this.m_controller.UpdateAxisFilter(l_axisFilter, l_filterValue);
    }

    void changeParentCellValue(uint p_axisElemId, uint p_filterValueId)
    {
      FilterValue l_filterValue = FilterValueModel.Instance.GetValue(p_filterValueId);
      if (l_filterValue != null)
        m_dgv.FillField(p_axisElemId, l_filterValue.FilterId, l_filterValue.Name);
    }

    void l_cbEditor_SelectedIndexChanged(object sender, EventArgs e)
    {
      cellModif = true;
    }

    void Instance_ReadEvent(Network.ErrorMessage status, AxisFilter attributes)
    {
      if (status == ErrorMessage.SUCCESS && attributes != null)
      {
        AxisFilter l_test = AxisFilterModel.Instance.GetValue(attributes.Id);
        AxisElem l_axisElem = AxisElemModel.Instance.GetValue(attributes.Axis, attributes.AxisElemId);
        FilterValue l_filterValue = FilterValueModel.Instance.GetValue(attributes.FilterValueId);
        if (l_filterValue != null && l_filterValue != null)
          changeParentCellValue(l_axisElem.Id, l_filterValue.ParentId);
      }
      else
        MessageBox.Show(Local.GetValue("general.error.system"));
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as AxisController;
    }
  }
}
