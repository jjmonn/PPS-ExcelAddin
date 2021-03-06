﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VIBlend.WinForms.DataGridView;
using System.Threading;
using Microsoft.VisualBasic;
using Microsoft.Office.Interop.Excel;

namespace FBI.MVC.View
{
  using Controller;
  using Model;
  using Model.CRUD;
  using Utils;
  using FBI.Forms;
  using Network;

  public partial class AxisBaseView<TControllerType> : UserControl, IPlatformMgtView where TControllerType : class, IAxisController
  {
    protected FbiDataGridView m_dgv = new FbiDataGridView();
    protected TControllerType m_controller;
    bool m_cellModif = false;
    RightManager m_rightMgr = new RightManager();
    HierarchyItem m_hoveredRow = null;
    static SafeDictionary<AxisType, string> m_axisNames = null;

    public AxisBaseView()
    {
      InitializeComponent();
      if (m_axisNames == null)
      {
        m_axisNames = new SafeDictionary<AxisType, string>();
        m_axisNames[AxisType.Adjustment] = Local.GetValue("general.adjustments");
        m_axisNames[AxisType.Client] = Local.GetValue("general.clients");
        m_axisNames[AxisType.Employee] = Local.GetValue("general.employees");
        m_axisNames[AxisType.Entities] = Local.GetValue("general.entities");
        m_axisNames[AxisType.Product] = Local.GetValue("general.products");
      }
    }

    public virtual void SetController(IController p_controller)
    {
      m_controller = p_controller as TControllerType;
    }

    #region "Load"

    public virtual void LoadView()
    {
      Dock = DockStyle.Fill;
      MultiIndexDictionary<UInt32, string, AxisElem> l_axisElemDic = AxisElemModel.Instance.GetDictionary(m_controller.AxisType);

      TableLayoutPanel1.Controls.Add(m_dgv);

      DefineUIPermissions();
      DesactivateUnallowed();
      SuscribeEvents();
      MultiLangueSetup();
      if (m_controller.AxisType == AxisType.Client || m_controller.AxisType == AxisType.Entities)
      {
        m_dgv.AllowDrop = true;
        m_dgv.AllowDragAndDrop = true;
        m_dgv.AllowDragDropIndication = true;
      }
    }

    private void MultiLangueSetup()
    {
      this.m_renameAxisElemMenu.Text = Local.GetValue("general.rename");
      this.m_createAxisElemMenu.Text = Local.GetValue("general.create");
      this.m_deleteAxisElemMenu.Text = Local.GetValue("general.delete");
      this.m_copyDownMenu.Text = Local.GetValue("general.copy_down");
      this.m_dropToExcelMenu.Text = Local.GetValue("general.drop_on_excel");
      this.m_autoResizeMenu.Text = Local.GetValue("general.auto_resize_columns");
      this.m_expandAllMenu.Text = Local.GetValue("general.expand_all");
      this.m_collapseAllMenu.Text = Local.GetValue("general.collapse_all");
      this.m_createNewAxisElemMenuTop.Text = Local.GetValue("general.create");
      this.m_deleteAxisElemMenuTop.Text = Local.GetValue("general.delete");
      this.m_dropToExcelMenuTop.Text = Local.GetValue("general.drop_on_excel");
      this.m_axisEditionButton.Text = Local.GetValue("general.edition");
    }

    protected void DesactivateUnallowed()
    {
      this.m_rightMgr.Enable(UserModel.Instance.GetCurrentUserRights());
    }

    private void DefineUIPermissions()
    {
      m_rightMgr[m_renameAxisElemMenu] = Group.Permission.EDIT_AXIS;
      m_rightMgr[m_createAxisElemMenu] = Group.Permission.CREATE_AXIS;
      m_rightMgr[m_deleteAxisElemMenuTop] = Group.Permission.DELETE_AXIS;
      m_rightMgr[m_deleteAxisElemMenu] = Group.Permission.DELETE_AXIS;
      m_rightMgr[m_createNewAxisElemMenuTop] = Group.Permission.CREATE_AXIS;
      m_rightMgr[m_copyDownMenu] = Group.Permission.EDIT_AXIS;
    }

    void SuscribeEvents()
    {
      AxisElemModel.Instance.ReadEvent   += OnModelRead;
      AxisElemModel.Instance.DeleteEvent += OnModelDelete;
      AxisElemModel.Instance.CreationEvent += OnModelCreate;
      AxisFilterModel.Instance.ReadEvent += OnModelReadAxisFilter;
      m_dgv.CellMouseClick += OnClickCell;
      m_expandAllMenu.Click += OnClickExpand;
      m_collapseAllMenu.Click += OnClickCollapse;
      m_copyDownMenu.Click += OnClickCopyValueDown;
      m_renameAxisElemMenu.Click += OnClickRename;
      m_createNewAxisElemMenuTop.Click += OnClickCreate;
      m_deleteAxisElemMenuTop.Click += OnClickDelete;
      m_dgv.MouseClick += OnDGVMouseClick;
      Addin.SuscribeAutoLock(this);
      if (m_controller.AxisType == AxisType.Client || m_controller.AxisType == AxisType.Entities)
        m_dgv.Dropped += OnDGVDropItem;
    }

    public virtual void CloseView()
    {
      AxisElemModel.Instance.ReadEvent -= OnModelRead;
      AxisElemModel.Instance.DeleteEvent -= OnModelDelete;
      AxisFilterModel.Instance.ReadEvent -= OnModelReadAxisFilter;
      AxisElemModel.Instance.CreationEvent -= OnModelCreate;
    }

    #endregion

    #region Initialize DGV

    protected void LoadDGV(MultiIndexDictionary<UInt32, string, AxisElem> p_axisElemList, List<AxisFilter> p_axisFilterlist)
    {
      List<Filter> l_filterDic = FilterModel.Instance.GetSortedByParentsDictionary(m_controller.AxisType);

      if (p_axisElemList == null)
        return;
      m_dgv.ClearColumns();
      m_dgv.ClearRows();
      m_dgv.InitializeRows(AxisElemModel.Instance, p_axisElemList);
      if (l_filterDic == null)
        return;
      foreach (Filter l_filter in l_filterDic)
        m_dgv.SetDimension(FbiDataGridView.Dimension.COLUMN, l_filter.Id, l_filter.Name);
      FillDGV(p_axisFilterlist);
    }

    void FillDGV(List<AxisFilter> p_axisFilterlist)
    {
      if (p_axisFilterlist == null)
        return;
      foreach (AxisFilter l_axisFilter in p_axisFilterlist)
        LoadAxisFilter(l_axisFilter);
      m_dgv.CellValueChanged += OnDGVCellValueChanged;
      m_dgv.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL);
      m_dgv.RowsHierarchy.AutoResize(AutoResizeMode.FIT_ALL);
      m_dgv.Refresh();
    }

    void LoadAxisFilter(AxisFilter p_axisFilter)
    {
      FilterValue l_filterValue = FilterValueModel.Instance.GetValue(p_axisFilter.FilterValueId);
      MultiIndexDictionary<UInt32, string, FilterValue> l_filterValueDic = null;
      ComboBoxEditor l_cbEditor = null;

      if (l_filterValue == null)
        return;
      if (l_filterValue.ParentId == 0)
        l_filterValueDic = FilterValueModel.Instance.GetDictionary(p_axisFilter.FilterId);
      else
        l_filterValueDic = FilterValueModel.Instance.GetChildrenDictionary(l_filterValue.ParentId);
      if (AxisElemModel.Instance.IsParent(p_axisFilter.AxisElemId))
        return;
      if (l_filterValue != null)
      {
        m_dgv.FillField(p_axisFilter.AxisElemId, p_axisFilter.FilterId, l_filterValue.Name, l_cbEditor);
        this.FillParentsColumn(l_filterValue.Id, l_filterValue.ParentId, p_axisFilter);
      }
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
          m_dgv.FillField(p_axisFilter.AxisElemId, l_parent.FilterId, l_parent.Name);
        if (l_parent.ParentId != 0)
          FillParentsColumn(l_parent.Id, l_parent.ParentId, p_axisFilter);
      }
    }

    private ComboBoxEditor BuildComboBoxEditor(MultiIndexDictionary<UInt32, string, FilterValue> p_filterValueDic, UInt32 p_parentId)
    {
      ComboBoxEditor l_cbEditor = new ComboBoxEditor();
      if (p_filterValueDic != null && l_cbEditor != null)
      {
        l_cbEditor.SelectedIndexChanged += OnCBEditorSelectedIndexChanged;
        foreach (FilterValue l_fv in p_filterValueDic.SortedValues)
          if (l_fv.ParentId == p_parentId)
            l_cbEditor.Items.Add(l_fv.Name);
      }
      return (l_cbEditor);
    }

    #endregion

    #region User Callback

    void OnDGVDropItem(HierarchyItem p_origin, HierarchyItem p_dest, DragEventArgs p_args)
    {
      if (p_origin == null || p_dest == null)
        return;
      AxisElem l_originAxis = AxisElemModel.Instance.GetValue((UInt32)p_origin.ItemValue);
      AxisElem l_destAxis = AxisElemModel.Instance.GetValue((UInt32)p_dest.ItemValue);

      if (l_originAxis == null || l_destAxis == null)
        return;
      l_originAxis = l_originAxis.Clone();
      l_originAxis.ParentId = l_destAxis.Id;
      if (m_controller.UpdateAxisElem(l_originAxis) == false)
        Forms.MsgBox.Show(m_controller.Error);
    }

    void OnDGVMouseClick(object p_sender, MouseEventArgs p_args)
    {
      if (p_args.Button == MouseButtons.Right)
      {
        System.Drawing.Point l_location = PointToScreen(p_args.Location);

        l_location.Y += 30;
        m_hoveredRow = m_dgv.HitTestRow(p_args.Location);
        m_axisRightClickMenu.Show(l_location);
      }
    }

    private void OnClickCell(object p_sender, CellMouseEventArgs p_e)
    {
      if (UserModel.Instance.CurrentUserHasRight(Group.Permission.EDIT_AXIS) == false)
        return;
      UInt32 l_filterId = (UInt32)p_e.Cell.ColumnItem.ItemValue;
      Filter l_filter = FilterModel.Instance.GetValue(l_filterId);
      UInt32 l_filterValueParent = 0;

      if (l_filter == null)
        return;
      if (l_filter.ParentId != 0 && p_e.Cell.ColumnItem.ItemIndex > 0)
      {
        HierarchyItem l_previousCol = m_dgv.ColumnsHierarchy.Items[p_e.Cell.ColumnItem.ItemIndex - 1];

        if (l_previousCol != null && (UInt32)l_previousCol.ItemValue == l_filter.ParentId)
        {
          string filterValueName = (string)m_dgv.CellsArea.GetCellValue(p_e.Cell.RowItem, l_previousCol);
          FilterValue l_filterValue = FilterValueModel.Instance.GetValue(filterValueName);

          if (l_filterValue != null)
            l_filterValueParent = l_filterValue.Id;
        }
      }

      ComboBoxEditor l_cb = BuildComboBoxEditor(FilterValueModel.Instance.GetDictionary(l_filterId), l_filterValueParent);
      m_dgv.CellsArea.SetCellEditor(p_e.Cell.RowItem, p_e.Cell.ColumnItem, l_cb);
    }

    protected void OnClickDelete(object p_sender, EventArgs p_e)
    {
      HierarchyItem l_row = m_hoveredRow;

      if (l_row == null || l_row.ItemValue == null)
        return;
      AxisElem l_axisItem = AxisElemModel.Instance.GetValue((UInt32)l_row.ItemValue);

      if (l_axisItem == null)
      {
        Forms.MsgBox.Show(Local.GetValue("axis.error.not_found"));
        return;
      }
      string l_result = PasswordBox.Open(Local.GetValue("axis.delete_confirm"));

      if (l_result != PasswordBox.Canceled && l_result != Addin.Password)
      {
        Forms.MsgBox.Show(Local.GetValue("general.invalid_password"));
        return;
      }
      if (l_result != PasswordBox.Canceled && m_controller.Delete(l_axisItem) == false)
        Forms.MsgBox.Show(m_controller.Error);
    }

    protected void OnClickCreate(object sender, EventArgs e)
    {
      HierarchyItem l_row = m_hoveredRow;

      if (l_row != null && l_row.ItemValue != null)
        m_controller.ShowNewAxisUI((UInt32)l_row.ItemValue);
      else
        m_controller.ShowNewAxisUI();
    }

    private void OnDGVCellValueChanged(object sender, CellEventArgs args)
    {
      if (m_cellModif == false || args == null || args.Cell.Value == null)
        return;
      if (args.Cell.RowItem.ItemValue == null || args.Cell.ColumnItem.ItemValue == null)
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
        UInt32 l_childFilterId = FilterModel.Instance.FindChildId(m_controller.AxisType, l_filter.Id);
        m_dgv.FillField(p_axisElemId, l_childFilterId, "");
        ChangeChildrenCells(p_axisElemId, l_childFilterId, "");
      }
      else if (l_filterValue != null)
        m_controller.UpdateAxisFilter(l_axisFilter, l_filterValue.Id);
    }

    void OnCBEditorSelectedIndexChanged(object sender, EventArgs e)
    {
      m_cellModif = true;
    }

    void OnClickExpand(object sender, EventArgs e)
    {
      m_dgv.RowsHierarchy.ExpandAllItems();
    }

    void OnClickCollapse(object sender, EventArgs e)
    {
      m_dgv.RowsHierarchy.CollapseAllItems();
    }

    void OnClickCopyValueDown(object sender, EventArgs e)
    {
      GridCell l_cell = m_dgv.HoveredCell;

      if (l_cell == null)
        return;

      HierarchyItemsCollection l_collection = (m_dgv.HoveredRow.ParentItem == null) ? m_dgv.RowsHierarchy.Items : m_dgv.HoveredRow.ParentItem.Items;
      for (int i = m_dgv.HoveredRow.ItemIndex; i < l_collection.Count; ++i)
        OnCopyDown(l_cell.Value, (UInt32)l_collection[i].ItemValue, (UInt32)m_dgv.HoveredColumn.ItemValue);
    }

    protected virtual void OnCopyDown(object p_cellValue, UInt32 p_rowValue, UInt32 p_columnValue)
    {
      UInt32 l_filterId = p_columnValue;
      AxisFilter l_axisFilter = AxisFilterModel.Instance.GetValue(m_controller.AxisType, p_rowValue, l_filterId);
      FilterValue l_filterValue = FilterValueModel.Instance.GetValue((string)p_cellValue);

      if (l_axisFilter == null || l_filterValue == null)
        return;
      m_controller.UpdateAxisFilter(l_axisFilter, l_filterValue.Id);
    }

    void OnClickRename(object sender, EventArgs e)
    {
      if (m_hoveredRow == null || m_hoveredRow.ItemValue == null)
        return;
      UInt32 l_axisElemId = (UInt32)m_hoveredRow.ItemValue;
      AxisElem l_axisElem = AxisElemModel.Instance.GetValue(l_axisElemId);

      if (l_axisElem == null)
        return;
      string l_result = Interaction.InputBox(Local.GetValue("axis.rename"));
      if (l_result == "")
        return;
      l_axisElem = l_axisElem.Clone();
      l_axisElem.Name = l_result;
      m_controller.UpdateAxisElem(l_axisElem);
    }

    #endregion

    #region Model Callback


    delegate void OnModelCreate_delegate(ErrorMessage p_status, UInt32 p_id);
    void OnModelCreate(ErrorMessage p_status, UInt32 p_id)
    {
      if (m_dgv.InvokeRequired)
      {
        OnModelCreate_delegate func = new OnModelCreate_delegate(OnModelCreate);
        Invoke(func, p_status, p_id);
      }
      else
      {
        if (p_status != ErrorMessage.SUCCESS)
          MessageBox.Show(Error.GetMessage(p_status));
      }
    }

    delegate void OnModelRead_delegate(ErrorMessage p_status, AxisElem p_attributes);
    void OnModelRead(ErrorMessage p_status, AxisElem p_attributes)
    {
      if (m_dgv.InvokeRequired)
      {
        OnModelRead_delegate func = new OnModelRead_delegate(OnModelRead);
        Invoke(func, p_status, p_attributes);
      }
      else
      {
        m_dgv.SetDimension(FbiDataGridView.Dimension.ROW, p_attributes.Id, p_attributes.Name, p_attributes.ParentId, AxisElemModel.Instance);
        m_dgv.Refresh();
        DesactivateUnallowed();
      }
    }

    delegate void OnModelDelete_delegate(ErrorMessage p_status, UInt32 p_id);
    void OnModelDelete(ErrorMessage p_status, UInt32 p_id)
    {
      if (m_dgv.InvokeRequired)
      {
        OnModelDelete_delegate func = new OnModelDelete_delegate(OnModelDelete);
        Invoke(func, p_status, p_id);
      }
      else
      {
        switch (p_status)
        {
          case ErrorMessage.SUCCESS:
            m_dgv.DeleteRow(p_id);
            m_dgv.Refresh();
            break;
          default:
            Forms.MsgBox.Show(Error.GetMessage(p_status));
            break;
        }
      }
    }

    void SetParentsCells(uint p_axisElemId, uint p_filterId, FilterValue p_filterValue)
    {
      if (p_filterValue.ParentId != 0)
      {
        FilterValue l_filterValueParent = FilterValueModel.Instance.GetValue(p_filterValue.ParentId);
        MultiIndexDictionary<UInt32, string, FilterValue> l_filterValuesDic = FilterValueModel.Instance.GetChildrenDictionary(l_filterValueParent.Id);
        if (l_filterValuesDic != null && l_filterValueParent != null)
        {
          m_dgv.FillField(p_axisElemId, p_filterId, p_filterValue.Name);
          Filter l_filter = FilterModel.Instance.GetValue(m_controller.AxisType, p_filterId);
          if (l_filter != null)
            SetParentsCells(p_axisElemId, l_filter.ParentId, l_filterValueParent);
        }
      }
      else
      {
        MultiIndexDictionary<UInt32, string, FilterValue> l_filterValuesDic = FilterValueModel.Instance.GetDictionary(p_filterId);
        if (l_filterValuesDic != null)
          m_dgv.FillField(p_axisElemId, p_filterId, p_filterValue.Name);
      }
    }

    delegate void OnModelReadAxisFilter_delegate(ErrorMessage p_status, AxisFilter p_attributes);
    void OnModelReadAxisFilter(ErrorMessage p_status, AxisFilter p_attributes)
    {
      if (m_dgv.InvokeRequired)
      {
        OnModelReadAxisFilter_delegate func = new OnModelReadAxisFilter_delegate(OnModelReadAxisFilter);
        Invoke(func, p_status, p_attributes);
      }
      else
      {
        if (p_status == ErrorMessage.SUCCESS && p_attributes != null)
        {
          FilterValue l_filterValue = FilterValueModel.Instance.GetValue(p_attributes.FilterValueId);
          SetParentsCells(p_attributes.AxisElemId, p_attributes.FilterId, l_filterValue);
          m_dgv.Refresh();
          DesactivateUnallowed();
        }
        else
          Forms.MsgBox.Show(Local.GetValue("general.error.system"));
      }
    }

    #endregion

    private void OnDropToExcelClick(object sender, EventArgs e)
    {
      Range l_destination = WorksheetWriter.CreateReceptionWS(m_axisNames[m_controller.AxisType], new string[]{ "" }, new string[]{ "" });
      int i = 0;
      bool l_expanded = false;

      if (l_destination != null)
        WorksheetWriter.CopyDGVToExcelGeneric(m_dgv, l_destination, "", ref i, ref l_expanded);
    }
  }
}
