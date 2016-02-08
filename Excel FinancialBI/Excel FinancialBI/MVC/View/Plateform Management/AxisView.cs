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
    AxisController m_controller;

    public AxisView(AxisType p_axisType)
    {
      InitializeComponent();
      m_axisType = p_axisType;
      m_dgv.ContextMenuStrip = m_axisRightClickMenu;
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as AxisController;
    }

    #region "Load"

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
      SuscribeEvents();
    }

    void SuscribeEvents()
    {
      AxisElemModel.Instance.DeleteEvent += OnModelDelete;
    }

    #endregion

    #region Initialize DGV

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
          foreach (FilterValue l_fv in l_filterValueDic.Values)
            l_cbEditor.Items.Add(l_fv.Name);
        if (l_filterValue != null && l_cbEditor != null)
        {
          m_dgv.FillField(l_axisFilter.AxisElemId, l_axisFilter.FilterId, l_filterValue.Name, l_cbEditor);
          this.FillParentsColumn(l_filterValue.Id, l_filterValue.ParentId, l_axisFilter);
        }
      }
    }

    void FillParentsColumn(uint p_filterValueId, uint p_parentId, AxisFilter p_axisFilter)
    {
      if (p_filterValueId != 0 && p_parentId != 0)
      {
        FilterValue l_parent = FilterValueModel.Instance.GetValue(p_parentId);
        MultiIndexDictionary<UInt32, string, FilterValue> l_filterValueDic = FilterValueModel.Instance.GetDictionary(l_parent.FilterId);
        ComboBoxEditor l_cbEditor = new ComboBoxEditor();
        if (l_filterValueDic != null && l_cbEditor != null)
          foreach (FilterValue l_fv in l_filterValueDic.Values)
            l_cbEditor.Items.Add(l_fv.Name);
        m_dgv.FillField(p_axisFilter.AxisElemId, l_parent.FilterId, l_parent.Name, l_cbEditor);
      }
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

    private void OnClickCreate(object p_sender, EventArgs p_e)
    {

    }

    #endregion

    #region Model Callback

    private void OnModelDelete(ErrorMessage p_status, UInt32 p_id)
    {
      switch (p_status)
      {
        case ErrorMessage.SUCCESS:
          m_dgv.DeleteRow(p_id);
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

    #endregion
  }
}
