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
using VIBlend.Utilities;

namespace FBI.MVC.View
{
  using Controller;
  using Model;
  using Network;
  using Model.CRUD;
  using Forms;
  using Utils;

  public partial class AllocationKeysView : Form, IPlatformMgtView
  {

    #region Variables

    private AllocationKeysController m_controller = null;

    private FbiDataGridView m_dgv = new FbiDataGridView();
    private TextBoxEditor m_allocatedTextBoxEditor = new TextBoxEditor();
    private TextBoxEditor m_allocatedTextBoxEditorDisabled = new TextBoxEditor();
    private Account m_account = null;
    private bool m_isFillingPercentage = false;

    #endregion

    #region Initialize

    public AllocationKeysView()
    {
      InitializeComponent();
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as AllocationKeysController;
    }

    public void LoadView()
    {
      MultilangueSetup();
      SuscribeEvents();
    }

    public void ShowView(Account p_account)
    {
      m_account = p_account;
      if (m_account == null)
        return;

      m_accountTextBox.Text = m_account.Name;
      m_accountTextBox.Enabled = false;

      DGVInit();
      ShowDialog();
    }

    void SuscribeEvents()
    {
      EntityDistributionModel.Instance.ReadEvent += OnModelRead;
      EntityDistributionModel.Instance.UpdateEvent += OnModelUpdate;
      m_dgv.CellChangedAndValidated += OnDGVCellChanged;
      m_dgv.CellBeginEdit += OnCellBeginEdit;
    }

    public void CloseView()
    {
      EntityDistributionModel.Instance.ReadEvent -= OnModelRead;
      EntityDistributionModel.Instance.UpdateEvent -= OnModelUpdate;
    }

    private void DGVInit()
    {
      MultiIndexDictionary<UInt32, string, AxisElem> l_axisElemMID = AxisElemModel.Instance.GetDictionary(AxisType.Entities);

      m_dgv.ClearColumns();
      m_dgv.ClearRows();
      m_dgv.InitializeRows<AxisElem>(AxisElemModel.Instance, l_axisElemMID);
      m_dgv.SetDimension(FbiDataGridView.Dimension.COLUMN, 42, Local.GetValue("allocationKeys.repartition_column_name"));
      m_dgv.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL);
      m_dgv.RowsHierarchy.ExpandAllItems();
      m_dgv.Refresh();

      GridCellStyle l_nonEditionCellStyle = GridTheme.GetDefaultTheme(m_dgv.VIBlendTheme).GridCellStyle;
      l_nonEditionCellStyle.FillStyle = new FillStyleSolid(Color.DarkGray);
      l_nonEditionCellStyle.TextColor = Color.White;

      GridCellStyle l_editableCellStyle = VIBlend.Utilities.GridTheme.GetDefaultTheme(m_dgv.VIBlendTheme).GridCellStyle;
      l_editableCellStyle.TextColor = Color.DarkBlue;

      InitRows(m_dgv.RowsHierarchy.Items, l_nonEditionCellStyle, l_editableCellStyle);

      foreach (AxisElem l_entity in l_axisElemMID.SortedValues)
        FillPercentage(l_entity);

      m_dgv.Dock = DockStyle.Fill;
      m_DGVPanel.Controls.Add(m_dgv);
    }

    void InitRows(HierarchyItemsCollection p_items, GridCellStyle p_nonEditionCellStyle, GridCellStyle p_editableCellStyle)
    {
      foreach (HierarchyItem l_row in p_items)
      {
        SetEditionEnabling(l_row, p_nonEditionCellStyle, p_editableCellStyle);
        l_row.CellsFormatString = "{0:P}";
        if (l_row.Items.Count > 0)
          InitRows(l_row.Items, p_nonEditionCellStyle, p_editableCellStyle);
      }
    }

    private void SetEditionEnabling(HierarchyItem p_row, GridCellStyle p_nonEditionCellStyle, GridCellStyle p_editableCellStyle)
    {
      p_row.CellsTextAlignment = ContentAlignment.MiddleRight;

      AxisElem l_entity = AxisElemModel.Instance.GetValue(AxisType.Entities, (UInt32)p_row.ItemValue);
      if (l_entity == null)
        return;
      if (l_entity.AllowEdition == false)
      {
        p_row.Enabled = false;
        if (m_dgv.ColumnsHierarchy.Items != null)
          m_dgv.CellsArea.SetCellDrawStyle(p_row, m_dgv.ColumnsHierarchy.Items[0], p_nonEditionCellStyle);
      }
      else
      {
        p_row.CellsEditor = m_allocatedTextBoxEditor;
        if (m_dgv.ColumnsHierarchy.Items != null)
          m_dgv.CellsArea.SetCellDrawStyle(p_row, m_dgv.ColumnsHierarchy.Items[0], p_editableCellStyle);
      }

      foreach (HierarchyItem l_childrenRow in p_row.Items)
        SetEditionEnabling(l_childrenRow, p_nonEditionCellStyle, p_editableCellStyle);
    }

    private void MultilangueSetup()
    {
      m_accountLabel.Text = Local.GetValue("general.account");
    }

    #endregion

    #region Event

    #region Server

    private void OnModelUpdate(ErrorMessage p_status, uint p_id)
    {
      if (p_status != ErrorMessage.SUCCESS)
        MsgBox.Show(Local.GetValue("allocationKeys.msg_error_update") + ": " + Error.GetMessage(p_status));
    }

    delegate void OnModelRead_delegate(ErrorMessage p_status, EntityDistribution p_attributes);
    private void OnModelRead(ErrorMessage p_status, EntityDistribution p_attributes)
    {
      if (m_dgv.InvokeRequired)
      {
        OnModelRead_delegate func = new OnModelRead_delegate(OnModelRead);
        Invoke(func, p_status, p_attributes);
      }
      else
      {
        if (p_status == ErrorMessage.SUCCESS)
        {
          AxisElem l_entity = AxisElemModel.Instance.GetValue(p_attributes.EntityId);
          FillPercentage(AxisElemModel.Instance.GetValue(AxisType.Entities, p_attributes.EntityId));
          m_dgv.Refresh();
        }
      }
    }

    #endregion

    #region DataGridView

    void OnCellBeginEdit(object sender, CellCancelEventArgs p_args)
    {
      double l_value = (double)p_args.Cell.Value;

      p_args.Cell.Value = l_value * 100;
    }

    private void OnDGVCellChanged(object p_sender, CellEventArgs p_args)
    {
      if (!m_isFillingPercentage)
      {
        double l_value;

        if (!double.TryParse((string)p_args.Cell.Value, out l_value))
          l_value = 0;
        FillPercentage(AxisElemModel.Instance.GetValue((UInt32)p_args.Cell.RowItem.ItemValue));
        UpdateEntityDistribution((UInt32)p_args.Cell.RowItem.ItemValue, l_value);
      }
    }

    #endregion

    #endregion

    #region Utils

    public void UpdateEntityDistribution(UInt32 p_entityId, double p_value)
    {
      if (m_controller.UpdateEntityDistribution(p_entityId, p_value) == false)
        MsgBox.Show(m_controller.Error);
    }

    private void FillPercentage(AxisElem p_entity)
    {
      if (p_entity == null)
        return;
      m_isFillingPercentage = true;

      EntityDistribution l_entityDistrib = EntityDistributionModel.Instance.GetValue(p_entity.Id, m_account.Id);

      if (l_entityDistrib == null)
        m_dgv.FillField<double, TextBoxEditor>(p_entity.Id, 42, 0, (p_entity.AllowEdition) ? m_allocatedTextBoxEditor : null);
      else
        m_dgv.FillField<double, TextBoxEditor>(p_entity.Id, 42, 
          l_entityDistrib.Percentage / 100.0, (p_entity.AllowEdition) ? m_allocatedTextBoxEditor : null);

      FillParents(p_entity.Id, 42);
      m_isFillingPercentage = false;
    }

    private void FillParents(UInt32 p_rowId, UInt32 p_colId)
    {
      if (!m_dgv.Rows.ContainsKey(p_rowId) || m_dgv.Rows[p_rowId].ParentItem == null)
        return;
      HierarchyItem l_parent = m_dgv.Rows[p_rowId].ParentItem;

      double l_value = 0;

      foreach (HierarchyItem l_item in l_parent.Items)
      {
        object l_tmp = m_dgv.GetCellValue((UInt32)l_item.ItemValue, p_colId);

        if (l_tmp != null)
          l_value += (double)l_tmp;
      }
      m_dgv.FillField((UInt32)l_parent.ItemValue, p_colId, l_value);
      FillParents((UInt32)l_parent.ItemValue, p_colId);
    }

    #endregion

  }
}
