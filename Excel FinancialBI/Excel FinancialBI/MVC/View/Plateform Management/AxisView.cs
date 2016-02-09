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
    AxisController m_controller;
    bool m_cellModif = false;
    NewAxisUI m_newAxisUI;

    public AxisView()
    {
      InitializeComponent();
      m_dgv.ContextMenuStrip = m_axisRightClickMenu;
      m_newAxisUI = new NewAxisUI();
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as AxisController;
      m_newAxisUI.SetController(m_controller);
    }

    #region "Load"

    public void LoadView()
    {
      Dock = DockStyle.Fill;
      MultiIndexDictionary<UInt32, string, AxisElem> l_axisElemDic = AxisElemModel.Instance.GetDictionary(m_controller.AxisType);
      List<Filter> l_filterDic = FilterModel.Instance.GetSortedByParentsDictionary(m_controller.AxisType);

      TableLayoutPanel1.Controls.Add(m_dgv);
      if (l_axisElemDic == null)
        return;
      m_dgv.InitializeRows(AxisElemModel.Instance, l_axisElemDic);
      if (l_filterDic == null)
        return;
      foreach (Filter l_filter in l_filterDic)
        m_dgv.SetDimension(FbiDataGridView.Dimension.COLUMN, l_filter.Id, l_filter.Name);
      FillDGV();
      SuscribeEvents();
      m_newAxisUI.LoadView();
    }

    void SuscribeEvents()
    {
      AxisElemModel.Instance.ReadEvent   += OnModelRead;
      AxisElemModel.Instance.DeleteEvent += OnModelDelete;
      AxisFilterModel.Instance.ReadEvent += OnModelReadAxisFilter;
    }

    #endregion

    #region Initialize DGV

    void FillDGV()
    {
      MultiIndexDictionary<UInt32, Tuple<UInt32, UInt32>, AxisFilter> l_axisFilterDic = AxisFilterModel.Instance.GetDictionary(m_controller.AxisType);

      if (l_axisFilterDic == null)
        return;
      foreach (AxisFilter l_axisFilter in l_axisFilterDic.Values)
      {
        FilterValue l_filterValue = FilterValueModel.Instance.GetValue(l_axisFilter.FilterValueId);
        MultiIndexDictionary<UInt32, string, FilterValue> l_filterValueDic = null;
        ComboBoxEditor l_cbEditor = null;

        if (l_filterValue.ParentId == 0)
          l_filterValueDic = FilterValueModel.Instance.GetDictionary(l_axisFilter.FilterId);
        else
          l_filterValueDic = FilterValueModel.Instance.GetChildrenDictionary(l_filterValue.ParentId);
        if (AxisElemModel.Instance.IsParent(l_axisFilter.AxisElemId))
          continue;
        if (l_filterValueDic != null)
          l_cbEditor = BuildComboBoxEditor(l_filterValueDic);
        if (l_filterValue != null && l_cbEditor != null)
        {
          m_dgv.FillField(l_axisFilter.AxisElemId, l_axisFilter.FilterId, l_filterValue.Name, l_cbEditor);
          this.FillParentsColumn(l_filterValue.Id, l_filterValue.ParentId, l_axisFilter);
        }
      }
      m_dgv.CellValueChanged += OnDGVCellValueChanged;
    }

    void FillParentsColumn(uint p_filterValueId, uint p_parentId, AxisFilter p_axisFilter)
    {
      if (p_filterValueId != 0 && p_parentId != 0)
      {
        FilterValue l_parent = FilterValueModel.Instance.GetValue(p_parentId);
        MultiIndexDictionary<uint, string, FilterValue> l_filterValueDic = null;
        if (l_parent == null)
          return;
        if (l_parent.ParentId != 0)
          l_filterValueDic = FilterValueModel.Instance.GetChildrenDictionary(l_parent.ParentId);
        else
          l_filterValueDic = FilterValueModel.Instance.GetDictionary(l_parent.FilterId);
        if (l_filterValueDic == null)
          return;
        ComboBoxEditor l_cbEditor = BuildComboBoxEditor(l_filterValueDic);
        if (l_cbEditor != null)
          m_dgv.FillField(p_axisFilter.AxisElemId, l_parent.FilterId, l_parent.Name, l_cbEditor);
        if (l_parent.ParentId != 0)
          FillParentsColumn(l_parent.Id, l_parent.ParentId, p_axisFilter);
      }
    }

    private ComboBoxEditor BuildComboBoxEditor(MultiIndexDictionary<UInt32, string, FilterValue> p_filterValueDic)
    {
      ComboBoxEditor l_cbEditor = new ComboBoxEditor();
      if (p_filterValueDic != null && l_cbEditor != null)
      {
        l_cbEditor.SelectedIndexChanged += OnCBEditorSelectedIndexChanged;
        foreach (FilterValue l_fv in p_filterValueDic.SortedValues)
          l_cbEditor.Items.Add(l_fv.Name);
      }
      return (l_cbEditor);
    }

    #endregion

    #region User Callback

    private void OnClickDelete(object p_sender, EventArgs p_e)
    {
      HierarchyItem l_row = m_dgv.HoveredRow;

      if (l_row == null && l_row.ItemValue == null)
        return;
      AxisElem l_axisItem = AxisElemModel.Instance.GetValue((UInt32)l_row.ItemValue);

      if (l_axisItem == null)
      {
        MessageBox.Show(Local.GetValue("axis.error.not_found"));
        return;
      }
      string l_result = PasswordBox.Open(Local.GetValue("axis.creation_confirm"));

      if (l_result != PasswordBox.Canceled && l_result != Addin.Password)
      {
        MessageBox.Show(Local.GetValue("general.invalid_password"));
        return;
      }
      if (m_controller.Delete(l_axisItem) == false)
        MessageBox.Show(m_controller.Error);
    }

    private void OnClickCreate(object sender, EventArgs e)
    {
      HierarchyItem row = m_dgv.HoveredRow;

      if (row != null)
        m_newAxisUI.ParentAxisElemId = (UInt32)row.ItemValue;
      m_newAxisUI.ShowDialog();
    }

    private void OnDGVCellValueChanged(object sender, CellEventArgs args)
    {
      if (m_cellModif == false || args == null || args.Cell.Value == null)
        return;
      m_cellModif = false;
      UInt32 l_axisElemId = (UInt32)args.Cell.RowItem.ItemValue;
      UInt32 l_filterId = (UInt32)args.Cell.ColumnItem.ItemValue;
      ChangeChildrenCells(l_axisElemId, l_filterId, (string)args.Cell.Value);
    }

    private void ChangeChildrenCells(UInt32 p_axisElemId, UInt32 p_filterId, string p_name)
    {
      AxisFilter l_axisFilter = AxisFilterModel.Instance.GetValue(m_controller.AxisType, p_axisElemId, p_filterId);
      Filter l_filter = FilterModel.Instance.GetValue(m_controller.AxisType, p_filterId);
      AxisElem l_axisElem = AxisElemModel.Instance.GetValue(p_axisElemId);
      FilterValue l_filterValue = FilterValueModel.Instance.GetValue(p_name);

      if (l_filter.IsParent)
      {
        MultiIndexDictionary<UInt32, string, FilterValue> l_filterValueChildDic = FilterValueModel.Instance.GetChildrenDictionary(l_filterValue.Id);
        if (l_filterValueChildDic.Count > 0)
        {
          ComboBoxEditor l_cbEditor = BuildComboBoxEditor(l_filterValueChildDic);
          m_dgv.FillField(p_axisElemId, FilterModel.Instance.FindChildId(m_controller.AxisType, l_filter.Id), "", l_cbEditor);
        }
        else
        {
          ComboBoxEditor l_cbEditor = new ComboBoxEditor();
          m_dgv.FillField(p_axisElemId, FilterModel.Instance.FindChildId(m_controller.AxisType, l_filter.Id), "", l_cbEditor);
        }
      }
      else
        m_controller.UpdateAxisFilter(l_axisFilter, l_filterValue);
    }

    void OnCBEditorSelectedIndexChanged(object sender, EventArgs e)
    {
      m_cellModif = true;
    }

    #endregion

    #region Model Callback

    void OnModelRead(ErrorMessage p_status, AxisElem p_attributes)
    {
      m_dgv.SetDimension(FbiDataGridView.Dimension.ROW, p_attributes.Id, p_attributes.Name, p_attributes.ParentId, AxisElemModel.Instance);
      m_dgv.Refresh();
    }

    void OnModelDelete(ErrorMessage p_status, UInt32 p_id)
    {
      switch (p_status)
      {
        case ErrorMessage.SUCCESS:
          m_dgv.DeleteRow(p_id);
          m_dgv.Refresh();
          break;
        case ErrorMessage.PERMISSION_DENIED:
          MessageBox.Show(Local.GetValue("general.error.permission_denied"));
          break;
        case ErrorMessage.NOT_FOUND:
          MessageBox.Show(Local.GetValue("general.error.not_found"));
          break;
        default:
          MessageBox.Show(Local.GetValue("general.error.system"));
          break;
      }
    }

    void setParentsCells(uint p_axisElemId, uint p_filterId, FilterValue p_filterValue)
    {
      if (p_filterValue.ParentId != 0)
      {
        FilterValue l_filterValueParent = FilterValueModel.Instance.GetValue(p_filterValue.ParentId);
        MultiIndexDictionary<UInt32, string, FilterValue> l_filterValuesDic = FilterValueModel.Instance.GetChildrenDictionary(l_filterValueParent.Id);
        ComboBoxEditor l_cbEditor = BuildComboBoxEditor(l_filterValuesDic);
        if (l_cbEditor != null && l_filterValuesDic != null && l_filterValueParent != null)
        {
          m_dgv.FillField(p_axisElemId, p_filterId, p_filterValue.Name, l_cbEditor);
          Filter l_filter = FilterModel.Instance.GetValue(m_controller.AxisType, p_filterId);
          if (l_filter != null)
            setParentsCells(p_axisElemId, l_filter.ParentId, l_filterValueParent);
        }
      }
      else
      {
        MultiIndexDictionary<UInt32, string, FilterValue> l_filterValuesDic = FilterValueModel.Instance.GetDictionary(p_filterId);
        ComboBoxEditor l_cbEditor = BuildComboBoxEditor(l_filterValuesDic);
        if (l_cbEditor != null && l_filterValuesDic != null)
          m_dgv.FillField(p_axisElemId, p_filterId, p_filterValue.Name, l_cbEditor);
      }
    }

    delegate void OnModelReadAxisFilter_delegate(ErrorMessage p_status, AxisFilter p_attributes);
    void OnModelReadAxisFilter(ErrorMessage p_status, AxisFilter p_attributes)
    {
      if (InvokeRequired)
      {
        OnModelReadAxisFilter_delegate func = new OnModelReadAxisFilter_delegate(OnModelReadAxisFilter);
        Invoke(func, p_status, p_attributes);
      }
      else
      {
        if (p_status == ErrorMessage.SUCCESS && p_attributes != null)
        {
          FilterValue l_filterValue = FilterValueModel.Instance.GetValue(p_attributes.FilterValueId);
          setParentsCells(p_attributes.AxisElemId, p_attributes.FilterId, l_filterValue);
        }
        else
          MessageBox.Show(Local.GetValue("general.error.system"));
      }
    }

    #endregion
  }
}
