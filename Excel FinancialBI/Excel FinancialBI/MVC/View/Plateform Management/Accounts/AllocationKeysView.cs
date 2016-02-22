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

  public partial class AllocationKeysView : Form, IView
  {

    #region Variables

    private AllocationKeysController m_controller = null;

    private FbiDataGridView m_allocationsKeysDGV = new FbiDataGridView();
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
      this.m_controller = p_controller as AllocationKeysController;
    }

    public void LoadView(Account p_account)
    {
      this.m_account = p_account;
      if (this.m_account == null)
        return;

      this.m_accountTextBox.Text = this.m_account.Name;
      this.m_accountTextBox.Enabled = false;

      this.DGVInit();
      this.MultilangueSetup();

      SuscribeEvents();
    }

    void SuscribeEvents()
    {
      EntityDistributionModel.Instance.ReadEvent += OnModelRead;
      EntityDistributionModel.Instance.UpdateEvent += OnModelUpdate;
      Addin.SuscribeAutoLock(this);
    }

    private void DGVInit()
    {
      MultiIndexDictionary<UInt32, string, AxisElem> l_axisElemMID = AxisElemModel.Instance.GetDictionary(AxisType.Entities);

      this.m_allocationsKeysDGV.InitializeRows<AxisElem>(AxisElemModel.Instance, l_axisElemMID);
      this.m_allocationsKeysDGV.SetDimension(FbiDataGridView.Dimension.COLUMN, 42, Local.GetValue("allocationKeys.repartition_column_name"));
      this.m_allocationsKeysDGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL);
      this.m_allocationsKeysDGV.RowsHierarchy.ExpandAllItems();
      this.m_allocationsKeysDGV.Refresh();

      GridCellStyle l_nonEditionCellStyle = GridTheme.GetDefaultTheme(this.m_allocationsKeysDGV.VIBlendTheme).GridCellStyle;
      l_nonEditionCellStyle.FillStyle = new FillStyleSolid(Color.DarkGray);
      l_nonEditionCellStyle.TextColor = Color.White;

      GridCellStyle l_editableCellStyle = VIBlend.Utilities.GridTheme.GetDefaultTheme(this.m_allocationsKeysDGV.VIBlendTheme).GridCellStyle;
      l_editableCellStyle.TextColor = Color.DarkBlue;

      foreach (HierarchyItem l_row in this.m_allocationsKeysDGV.RowsHierarchy.Items)
        this.SpecificyAllocationKeysEditionEnabling(l_row, l_nonEditionCellStyle, l_editableCellStyle);

      foreach (AxisElem l_entity in l_axisElemMID.SortedValues)
        FillPercentage(l_entity);

      this.ChangeAllParentsPercentages();

      this.m_allocationsKeysDGV.Dock = DockStyle.Fill;
      this.m_DGVPanel.Controls.Add(this.m_allocationsKeysDGV);

      this.m_allocationsKeysDGV.CellChangedAndValidated += OnAllocationsKeysDGVCellChangedAndValidated;
      this.m_allocationsKeysDGV.CellEditorActivate += OnAllocationsKeysDGVCellEditorActivate;
      this.m_allocationsKeysDGV.CellEditorDeActivate += OnAllocationsKeysDGVCellEditorDeActivate;
    }

    private void SpecificyAllocationKeysEditionEnabling(HierarchyItem p_row, GridCellStyle p_nonEditionCellStyle, GridCellStyle p_editableCellStyle)
    {
      p_row.CellsTextAlignment = ContentAlignment.MiddleRight;

      AxisElem l_entity = AxisElemModel.Instance.GetValue(AxisType.Entities, (UInt32)p_row.ItemValue);
      if (l_entity == null)
        return;
      if (l_entity.AllowEdition == false)
      {
        p_row.Enabled = false;
        if (this.m_allocationsKeysDGV.ColumnsHierarchy.Items != null)
          m_allocationsKeysDGV.CellsArea.SetCellDrawStyle(p_row, m_allocationsKeysDGV.ColumnsHierarchy.Items[0], p_nonEditionCellStyle);
      }
      else
      {
        p_row.CellsEditor = m_allocatedTextBoxEditor;
        if (this.m_allocationsKeysDGV.ColumnsHierarchy.Items != null)
          m_allocationsKeysDGV.CellsArea.SetCellDrawStyle(p_row, m_allocationsKeysDGV.ColumnsHierarchy.Items[0], p_editableCellStyle);
      }

      foreach (HierarchyItem l_childrenRow in p_row.Items)
        this.SpecificyAllocationKeysEditionEnabling(l_childrenRow, p_nonEditionCellStyle, p_editableCellStyle);
    }

    private void MultilangueSetup()
    {
      this.m_accountLabel.Text = Local.GetValue("general.account");
    }

    #endregion

    #region Event

    #region Server

    delegate void OnModelUpdate_delegate(ErrorMessage p_status, uint p_id);
    private void OnModelUpdate(ErrorMessage p_status, uint p_id)
    {
      if (InvokeRequired)
      {
        OnModelUpdate_delegate func = new OnModelUpdate_delegate(OnModelUpdate);
        Invoke(func, p_status, p_id);
      }
      else
      {
        if (p_status != ErrorMessage.SUCCESS)
        {
          MessageBox.Show(Local.GetValue("allocationKeys.msg_error_update"));
        }
      }
    }

    delegate void OnModelRead_delegate(ErrorMessage p_status, EntityDistribution p_attributes);
    private void OnModelRead(ErrorMessage p_status, EntityDistribution p_attributes)
    {
      if (InvokeRequired)
      {
        OnModelRead_delegate func = new OnModelRead_delegate(OnModelRead);
        Invoke(func, p_status, p_attributes);
      }
      else
      {
        if (p_status == ErrorMessage.SUCCESS)
        {
          AxisElem l_entity = AxisElemModel.Instance.GetValue(p_attributes.EntityId);
          if (l_entity != null)
            this.ChangeParentPercentage(l_entity.ParentId);
          this.FillPercentage(AxisElemModel.Instance.GetValue(AxisType.Entities, p_attributes.EntityId));
        }
      }
    }

    #endregion

    #region DataGridView

    private void OnAllocationsKeysDGVCellChangedAndValidated(object p_sender, CellEventArgs p_args)
    {
      if (!this.m_isFillingPercentage)
        this.CheckUpdateAllocationKey(p_args);
    }

    private void OnAllocationsKeysDGVCellEditorActivate(object p_sender, EditorActivationCancelEventArgs p_args)
    {
      p_args.Cell.Value = ((string)p_args.Cell.Value).Replace(" %", "");
    }

    private void OnAllocationsKeysDGVCellEditorDeActivate(object p_sender, EditorActivationCancelEventArgs p_args)
    {
      this.FillPercentage(AxisElemModel.Instance.GetValue(AxisType.Entities, (UInt32)p_args.Cell.RowItem.ItemValue));
    }

    #endregion

    #endregion

    #region Utils

    public void CheckUpdateAllocationKey(CellEventArgs p_args)
    {
      double l_value = 0.0;

      if (Double.TryParse((string)p_args.Cell.Value, out l_value))
      {
        if (l_value < 0 || l_value > 100)
        {
          MessageBox.Show(Local.GetValue("allocationKeys.msg_invalid_percentage"));
          this.FillPercentage(AxisElemModel.Instance.GetValue(AxisType.Entities, (UInt32)p_args.Cell.RowItem.ItemValue));
        }
        else
        {
          if (this.m_controller.TotalPercentageValid((UInt32)p_args.Cell.RowItem.ItemValue) + l_value <= 100)
          {
            this.m_controller.UpdateAllocationKey((UInt32)p_args.Cell.RowItem.ItemValue, l_value);
            this.FillPercentage(AxisElemModel.Instance.GetValue(AxisType.Entities, (UInt32)p_args.Cell.RowItem.ItemValue));
          }
          else
          {
            MessageBox.Show(Local.GetValue("allocationKeys.msg_percentageOver100"));
            this.FillPercentage(AxisElemModel.Instance.GetValue(AxisType.Entities, (UInt32)p_args.Cell.RowItem.ItemValue));
          }
        }
      }
      else
        this.FillPercentage(AxisElemModel.Instance.GetValue(AxisType.Entities, (UInt32)p_args.Cell.RowItem.ItemValue));
    }

    private void FillPercentage(AxisElem p_entity)
    {
      if (p_entity == null)
        return;
      this.m_isFillingPercentage = true;

      EntityDistribution l_entityDistrib = EntityDistributionModel.Instance.GetValue(p_entity.Id, this.m_account.Id);

      if (l_entityDistrib == null)
      {
        if (p_entity.AllowEdition)
          this.m_allocationsKeysDGV.FillField<string, TextBoxEditor>(p_entity.Id, 42, "0 %", m_allocatedTextBoxEditor);
        else
          this.m_allocationsKeysDGV.FillField<string, TextBoxEditor>(p_entity.Id, 42, "0 %", null);
      }
      else
      {
        if (p_entity.AllowEdition)
          this.m_allocationsKeysDGV.FillField<string, TextBoxEditor>(p_entity.Id, 42, l_entityDistrib.Percentage.ToString() + " %", m_allocatedTextBoxEditor);
        else
          this.m_allocationsKeysDGV.FillField<string, TextBoxEditor>(p_entity.Id, 42, l_entityDistrib.Percentage.ToString() + " %", null);
      }
      this.m_isFillingPercentage = false;
    }

    private void ChangeAllParentsPercentages()
    {
      MultiIndexDictionary<UInt32, string, AxisElem> l_axisElemMID = AxisElemModel.Instance.GetDictionary(AxisType.Entities);

      foreach (AxisElem l_entity in l_axisElemMID.SortedValues)
      {
        if (l_entity.ParentId != 0)
          this.ChangeParentPercentage(l_entity.ParentId);
      }
    }

    private void ChangeParentPercentage(UInt32 p_parentId)
    {
      MultiIndexDictionary<UInt32, string, AxisElem> l_axisElemMID = AxisElemModel.Instance.GetDictionary(AxisType.Entities);
      double l_percentage = 0.0;

      foreach (AxisElem l_entity in l_axisElemMID.SortedValues)
      {
        if (l_entity.ParentId == p_parentId)
        {
          EntityDistribution l_entityDistrib = EntityDistributionModel.Instance.GetValue(l_entity.Id, this.m_account.Id);

          if (l_entityDistrib != null)
            l_percentage += l_entityDistrib.Percentage;
        }
      }
      TextBoxEditor l_block = new TextBoxEditor();

      this.FillPercentage(AxisElemModel.Instance.GetValue(AxisType.Entities, p_parentId));
      this.m_controller.UpdateAllocationKey(p_parentId, l_percentage);
    }

    #endregion

  }
}
