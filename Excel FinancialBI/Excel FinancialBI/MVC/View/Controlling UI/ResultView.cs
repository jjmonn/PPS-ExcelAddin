using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using VIBlend.WinForms.DataGridView;
using VIBlend.WinForms.Controls;
using Microsoft.Office.Interop.Excel;

namespace FBI.MVC.View
{
  using Controller;
  using Forms;
  using Model;
  using Model.CRUD;
  using Utils;

  using DGVDimension = FBI.Forms.BaseFbiDataGridView<Model.CRUD.ResultKey>.Dimension;
  using DGV = FBI.Forms.BaseFbiDataGridView<Model.CRUD.ResultKey>;

  public partial class ResultView : UserControl, IView
  {
    vTabControl m_tabCtrl = new vTabControl();
    private delegate void DGVBuilder(DGV p_dgv, UInt32 p_tabId, CUIDimensionConf p_conf, DGVDimension p_dimension,
    HierarchyItemsCollection p_parent, ResultKey p_parentKey);
    SafeDictionary<Type, DGVBuilder> m_builderList = new SafeDictionary<Type, DGVBuilder>();
    ComputeConfig m_computeConfig;
    ResultController m_controller;

    #region Initialize

    public ResultView()
    {
      InitializeComponent();
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as ResultController;
    }

    public void LoadView()
    {
      m_tabCtrl.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      m_tabCtrl.TitleHeight = 25;
      m_tabCtrl.BorderStyle = System.Windows.Forms.BorderStyle.None;
      m_tabCtrl.TabsAreaBackColor = System.Drawing.SystemColors.Control;
      m_tabCtrl.BackColor = System.Drawing.SystemColors.Control;
      Controls.Add(m_tabCtrl);
      m_tabCtrl.Dock = DockStyle.Fill;
 
      LogRightClick.Visible = (Addin.Process == Account.AccountProcess.FINANCIAL);
      m_builderList.Add(typeof(PeriodModel), PeriodBuilderSelector);
      m_builderList.Add(typeof(VersionModel), VersionBuilder);
      m_builderList.Add(typeof(AxisElemModel), AxisElemBuilder);
      m_builderList.Add(typeof(FilterModel), FilterValueBuilder);
      m_builderList.Add(typeof(AccountModel), AccountBuilder);
      MultilanguageSetup();
      SuscribeEvents();
    }

    void MultilanguageSetup()
    {
      ExpandAllRightClick.Text = Local.GetValue("CUI.expand_all");
      CollapseAllRightClick.Text = Local.GetValue("CUI.collapse_all");
      LogRightClick.Text = Local.GetValue("CUI.log");
      DGVFormatsButton.Text = Local.GetValue("CUI.display_options");
      ColumnsAutoSize.Text = Local.GetValue("CUI.adjust_columns_size");
      ColumnsAutoFitBT.Text = Local.GetValue("CUI.automatic_columns_adjustment");
    }

    void SuscribeEvents()
    {
      ExpandAllRightClick.Click += OnExpandAllClick;
      CollapseAllRightClick.Click += OnCollapseAllClick;
      ColumnsAutoSize.Click += OnColumnAutoSizeClick;
      ColumnsAutoFitBT.Click += OnAutoFitClick;
      LogRightClick.Click += OnLogClick;
    }

    #endregion

    #region User Callbacks

    void OnLogClick(object p_sender, EventArgs p_e)
    {
      if (m_tabCtrl.SelectedTab == null || m_tabCtrl.SelectedTab.Controls.Count <= 0)
        return;
      DGV l_dgv = m_tabCtrl.SelectedTab.Controls[0] as DGV;
      HierarchyItem l_row = l_dgv.HoveredRow;
      HierarchyItem l_column = l_dgv.HoveredColumn;

      if (l_row == null || l_column == null)
        return;
      ResultKey l_key = l_dgv.HierarchyItems[l_row] + l_dgv.HierarchyItems[l_column];

      if (m_controller.ShowLog((l_key.EntityId == 0)
        ? m_computeConfig.Request.EntityId : l_key.EntityId, l_key.VersionId, l_key.AccountId, (UInt32)l_key.Period) == false)
        MessageBox.Show(m_controller.Error);
    }

    void OnExpandAllClick(object p_sender, EventArgs p_e)
    {
      if (m_tabCtrl.SelectedTab == null || m_tabCtrl.SelectedTab.Controls.Count <= 0)
        return;
      DGV l_dgv = m_tabCtrl.SelectedTab.Controls[0] as DGV;

      l_dgv.ColumnsHierarchy.ExpandAllItems();
      l_dgv.RowsHierarchy.ExpandAllItems();
    }

    void OnCollapseAllClick(object p_sender, EventArgs p_e)
    {
      if (m_tabCtrl.SelectedTab == null || m_tabCtrl.SelectedTab.Controls.Count <= 0)
        return;
      DGV l_dgv = m_tabCtrl.SelectedTab.Controls[0] as DGV;

      l_dgv.ColumnsHierarchy.CollapseAllItems();
      l_dgv.RowsHierarchy.CollapseAllItems();
    }

    void OnColumnAutoSizeClick(object p_sender, EventArgs p_e)
    {
      if (m_tabCtrl.SelectedTab == null || m_tabCtrl.SelectedTab.Controls.Count <= 0)
        return;
      DGV l_dgv = m_tabCtrl.SelectedTab.Controls[0] as DGV;

      foreach (HierarchyItem l_column in l_dgv.ColumnsHierarchy.Items)
        l_column.Width = 1;
      l_dgv.Refresh();
      l_dgv.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_CELL_CONTENT);
      l_dgv.Refresh();
      l_dgv.Select();
    }

    void OnAutoFitClick(object p_sender, EventArgs p_e)
    {
      Properties.Settings.Default.controllingUIResizeTofitGrid = ColumnsAutoFitBT.Checked;
      Properties.Settings.Default.Save();
    }

    #endregion

    #region Load DGV

    public void PrepareDgv(ComputeConfig p_config)
    {
      m_tabCtrl.TabPages.Clear();
      MultiIndexDictionary<UInt32, string, Account> l_accountList = AccountModel.Instance.GetDictionary();
      m_computeConfig = p_config;

      foreach (Account l_account in l_accountList.Values)
        if (l_account.FormulaType == Account.FormulaTypes.TITLE && l_account.ParentId == 0)
        {
          if (l_account.Process != (Account.AccountProcess)Properties.Settings.Default.processId)
            continue;
          vTabPage l_tab = new vTabPage(l_account.Name);
          DGV l_dgv = new DGV();
          l_dgv.ContextMenuStrip = m_dgvMenu;
          l_tab.Controls.Add(l_dgv);
          m_tabCtrl.TabPages.Add(l_tab);
          l_dgv.Dock = DockStyle.Fill;
          l_tab.Dock = DockStyle.Fill;
          InitDimension(l_dgv, l_account.Id, p_config.Rows, DGVDimension.ROW, l_dgv.RowsHierarchy.Items, new ResultKey(0, "", "", 0, 0, 0, false, l_account.Id));
          InitDimension(l_dgv, l_account.Id, p_config.Columns, DGVDimension.COLUMN, l_dgv.ColumnsHierarchy.Items,
            new ResultKey(0, "", "", 0, 0, (m_computeConfig.Request.Versions.Count <= 1) ? m_computeConfig.Request.Versions[0] : 0, false, l_account.Id));
          DGVFormatUtils.FormatDGVs(l_dgv, m_computeConfig.Request.CurrencyId);
          l_dgv.Refresh();
        }
    }

    public delegate void FillDGV_delegate(SafeDictionary<uint, ComputeResult> p_data);
    public void FillDGV(SafeDictionary<uint, ComputeResult> p_data)
    {
      if (m_tabCtrl.InvokeRequired)
      {
        FillDGV_delegate func = new FillDGV_delegate(FillDGV);
        Invoke(func, p_data);
      }
      else
      {
        foreach (vTabPage l_tab in m_tabCtrl.TabPages)
        {
          if (l_tab.Controls.Count > 0)
          {
            DGV l_dgv = l_tab.Controls[0] as DGV;
            foreach (ResultKey l_rowKey in l_dgv.Rows.Keys)
            {
              foreach (ResultKey l_columnKey in l_dgv.Columns.Keys)
              {
                ResultKey l_key = l_rowKey + l_columnKey;

                l_key.RemoveTab();
                if (p_data[l_key.VersionId] == null)
                  continue;

                if (p_data[l_key.VersionId].Values.ContainsKey(l_key) == false)
                  continue;
                double l_value = p_data[l_key.VersionId].Values[l_key];

                l_dgv.FillField(l_rowKey, l_columnKey, l_value);
                if (ComputeResult.IsDiffId(l_key.VersionId))
                  DGVFormatUtils.FormatValue(l_dgv, l_rowKey, l_columnKey);
              }
            }
          }
        }
        if (m_computeConfig != null && m_computeConfig.Request.Process == Account.AccountProcess.RH)
          RemoveOrphanDimensions();
        foreach (vTabPage l_tab in m_tabCtrl.TabPages)
        {
          if (l_tab.Controls.Count > 0)
          {
            DGV l_dgv = l_tab.Controls[0] as DGV;
            l_dgv.Select();
            l_dgv.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_CELL_CONTENT);
            l_dgv.RowsHierarchy.AutoResize(AutoResizeMode.FIT_ALL);
            l_dgv.Refresh();
            l_tab.Refresh();
          }
        }
      }
    }

    #endregion

    #region Utils

    public void SetVersionVisible(UInt32 p_versionId, bool p_visible)
    {
      foreach (vTabPage l_tab in m_tabCtrl.TabPages)
      {
        if (l_tab.Controls.Count > 0)
        {
          DGV l_dgv = l_tab.Controls[0] as DGV;
          SetHierachyItemVisible(p_versionId, p_visible, l_dgv.Rows);
          SetHierachyItemVisible(p_versionId, p_visible, l_dgv.Columns);
          l_dgv.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_CELL_CONTENT);
        }
      }
    }

    void SetHierachyItemVisible(UInt32 p_versionId, bool p_visible, SafeDictionary<ResultKey, HierarchyItem> p_items)
    {
      foreach (KeyValuePair<ResultKey, HierarchyItem> l_item in p_items)
        if (l_item.Key.VersionId == p_versionId)
          l_item.Value.Hidden = !p_visible;
    }

    private void InitDimension(DGV p_dgv, UInt32 p_tabId, CUIDimensionConf p_conf,
      DGVDimension p_dimension, HierarchyItemsCollection p_parent, ResultKey p_parentKey)
    {
      if (p_conf == null)
        return;
      if (m_builderList[p_conf.ModelType] != null)
        m_builderList[p_conf.ModelType](p_dgv, p_tabId, p_conf, p_dimension, p_parent, p_parentKey);
    }

    Int32 GetNbPeriod(Int32 p_nbPeriod, TimeConfig p_config, TimeConfig p_baseConfig)
    {
      if ((int)p_config == (int)p_baseConfig)
        return (p_nbPeriod);
      switch (p_config)
      {
        case TimeConfig.WEEK:
          return ((Int32)Math.Ceiling(p_nbPeriod / 7.0));
        case TimeConfig.YEARS:
          return ((Int32)Math.Ceiling(p_nbPeriod / 12.0));
      }
      return (p_nbPeriod);
    }

    HierarchyItem SetDimension(DGV p_dgv, DGVDimension p_dimension, HierarchyItemsCollection p_parent, ResultKey p_key, string p_value)
    {
      return p_dgv.SetDimension(p_dimension, p_parent, p_key, p_value);
    }

    #endregion

    #region Builders

    private void PeriodBuilderSelector(DGV p_dgv, UInt32 p_tabId, CUIDimensionConf p_conf,
        DGVDimension p_dimension, HierarchyItemsCollection p_parent, ResultKey p_parentKey)
    {
      if (m_computeConfig.Request.IsPeriodDiff)
        PeriodCompareBuilder(p_dgv, p_tabId, p_conf, p_dimension, p_parent, p_parentKey);
      else
        PeriodBuilder(p_dgv, p_tabId, p_conf, p_dimension, p_parent, p_parentKey);
    }

    private void PeriodBuilder(DGV p_dgv, UInt32 p_tabId, CUIDimensionConf p_conf,
      DGVDimension p_dimension, HierarchyItemsCollection p_parent, ResultKey p_parentKey)
    {
      PeriodConf l_conf = p_conf as PeriodConf;
      List<int> l_periodList;
      string l_formatedDate;
      Int32 l_startPeriod = m_computeConfig.Request.StartPeriod;

      l_periodList = (l_conf.IsSubPeriod) ? PeriodModel.GetSubPeriods(l_conf.ParentType, l_conf.ParentPeriod) :
        PeriodModel.GetPeriodList(l_startPeriod,
        GetNbPeriod(m_computeConfig.Request.NbPeriods, l_conf.PeriodType, m_computeConfig.BaseTimeConfig), l_conf.PeriodType);
      bool l_includeWeekEnds = l_conf.PeriodType != TimeConfig.DAYS || Properties.Settings.Default.includeWeekEnds;

      foreach (int l_date in l_periodList)
      {
        if (m_computeConfig.Periods != null && m_computeConfig.Periods.Contains(l_date) == false)
          continue;
        if (l_includeWeekEnds == false && PeriodModel.IsWeekEnd(l_date))
          continue;
        l_formatedDate = PeriodModel.GetFormatedDate(l_date, l_conf.PeriodType);

        ResultKey l_key = p_parentKey + new ResultKey(0, "", "", l_conf.PeriodType, l_date, 0);
        HierarchyItem l_newItem = SetDimension(p_dgv, p_dimension, p_parent, l_key, l_formatedDate);

        if (l_newItem != null)
        {
          if (p_conf.Child != null && p_conf.Child.ModelType == typeof(PeriodModel))
          {
            PeriodConf l_childConf = p_conf.Child as PeriodConf;

            l_childConf.ParentPeriod = (l_periodList[0] == l_date) ? l_startPeriod : l_date;
          }
          InitDimension(p_dgv, p_tabId, p_conf.Child, p_dimension, l_newItem.Items, l_key);
        }
      }
    }

    private void PeriodCompareBuilder(DGV p_dgv, UInt32 p_tabId, CUIDimensionConf p_conf,
      DGVDimension p_dimension, HierarchyItemsCollection p_parent, ResultKey p_parentKey)
    {
      PeriodConf l_conf = p_conf as PeriodConf;
      string l_formatedDate;
      int l_count = 0;
      TimeConfig l_lowestConfig = TimeUtils.GetLowestTimeConfig(m_computeConfig.Request.PeriodDiffAssociations.Keys.ToList());

      if (m_computeConfig.Request.PeriodDiffAssociations[l_conf.PeriodType] != null)
        foreach (KeyValuePair<Int32, Int32> l_date in m_computeConfig.Request.PeriodDiffAssociations[l_conf.PeriodType])
        {
          if (p_parentKey.VersionId == m_computeConfig.Request.Versions[0])
            l_formatedDate = PeriodModel.GetFormatedDate(l_date.Key, l_conf.PeriodType);
          else if (p_parentKey.VersionId == m_computeConfig.Request.Versions[1])
            l_formatedDate = PeriodModel.GetFormatedDate(l_date.Value, l_conf.PeriodType);
          else
            l_formatedDate = PeriodModel.GetFormatedDate(l_date.Key, l_conf.PeriodType) + " / " +
              PeriodModel.GetFormatedDate(l_date.Value, l_conf.PeriodType);


          ResultKey l_key = p_parentKey + new ResultKey(0, "", "", l_conf.PeriodType, l_count++, 0);
          if (l_lowestConfig == l_conf.PeriodType && m_computeConfig.Periods != null && m_computeConfig.Periods.Contains(l_count - 1) == false)
            continue;
          HierarchyItem l_newItem = SetDimension(p_dgv, p_dimension, p_parent, l_key, l_formatedDate);

          if (l_newItem != null)
            InitDimension(p_dgv, p_tabId, p_conf.Child, p_dimension, l_newItem.Items, l_key);
        }
    }

    private void VersionBuilder(DGV p_dgv, UInt32 p_tabId, CUIDimensionConf p_conf,
      DGVDimension p_dimension, HierarchyItemsCollection p_parent, ResultKey p_parentKey)
    {
      if (m_computeConfig.Request.Versions.Count <= 1)
        return;
      foreach (UInt32 l_versionId in m_computeConfig.Request.Versions)
      {
        Version l_version = VersionModel.Instance.GetValue(l_versionId);
        if (l_version == null)
          continue;

        VersionBuilderSingle(p_dgv, p_tabId, p_conf, p_dimension, p_parent, p_parentKey, l_version.Id, l_version.Name);
      }
      if (m_computeConfig.Request.IsDiff)
      {
        Version l_versionA = VersionModel.Instance.GetValue(m_computeConfig.Request.Versions[0]);
        Version l_versionB = VersionModel.Instance.GetValue(m_computeConfig.Request.Versions[1]);

        VersionBuilderSingle(p_dgv, p_tabId, p_conf, p_dimension, p_parent, p_parentKey,
          ComputeResult.GetDiffId(l_versionA.Id, l_versionB.Id), l_versionA.Name + " vs. " + l_versionB.Name, true);
        VersionBuilderSingle(p_dgv, p_tabId, p_conf, p_dimension, p_parent, p_parentKey,
          ComputeResult.GetDiffId(l_versionB.Id, l_versionA.Id), l_versionB.Name + " vs. " + l_versionA.Name, true);
      }
    }

    private void VersionBuilderSingle(DGV p_dgv, UInt32 p_tabId, CUIDimensionConf p_conf,
      DGVDimension p_dimension, HierarchyItemsCollection p_parent, ResultKey p_parentKey, UInt32 p_id, string p_name, bool p_hidden = false)
    {
      ResultKey l_key = p_parentKey + new ResultKey(0, "", "", 0, 0, p_id, true);
      HierarchyItem l_newItem = SetDimension(p_dgv, p_dimension, p_parent, l_key, p_name);

      if (l_newItem != null)
      {
        l_newItem.Hidden = p_hidden;
        InitDimension(p_dgv, p_tabId, p_conf.Child, p_dimension, l_newItem.Items, l_key);
      }
    }

    private void AxisElemBuilder(DGV p_dgv, UInt32 p_tabId, CUIDimensionConf p_conf,
      DGVDimension p_dimension, HierarchyItemsCollection p_parent, ResultKey p_parentKey)
    {
      AxisElemConf l_conf = p_conf as AxisElemConf;

      List<AxisElem> l_axisList = new List<AxisElem>();

      if (l_conf.ParentId == 0 && l_conf.AxisTypeId == AxisType.Entities)
        l_axisList.Add(AxisElemModel.Instance.GetValue(m_computeConfig.Request.EntityId));
      else
        l_axisList = AxisElemModel.Instance.GetChildren(l_conf.AxisTypeId, l_conf.ParentId);

      l_axisList.Sort();
      foreach (AxisElem l_elem in l_axisList)
      {
        if (l_elem == null)
          continue;

        ResultKey l_key;

        if (l_conf.AxisTypeId == AxisType.Entities)
          l_key = p_parentKey + new ResultKey(0, "", ResultKey.GetSortKey(true, l_conf.AxisTypeId, l_elem.Id), 0, 0, 0);
        else
          l_key = p_parentKey + new ResultKey(0, ResultKey.GetSortKey(true, l_conf.AxisTypeId, l_elem.Id), "", 0, 0, 0);

        AxisElemConf l_childAxisConf = new AxisElemConf(l_conf.AxisTypeId);
        HierarchyItem l_newItem = SetDimension(p_dgv, p_dimension, p_parent, l_key, l_elem.Name);

        l_childAxisConf.ParentId = l_elem.Id;
        l_childAxisConf.Child = l_conf.Child;
        AxisElemBuilder(p_dgv, p_tabId, l_childAxisConf, p_dimension,
          (l_newItem != null) ? l_newItem.Items : p_parent, (l_conf.AxisTypeId == AxisType.Entities) ? l_key : p_parentKey);

        if (l_newItem != null)
          InitDimension(p_dgv, p_tabId, p_conf.Child, p_dimension, l_newItem.Items, l_key);
      }
    }

    private void FilterValueBuilder(DGV p_dgv, UInt32 p_tabId, CUIDimensionConf p_conf,
      DGVDimension p_dimension, HierarchyItemsCollection p_parent, ResultKey p_parentKey)
    {
      FilterConf l_conf = p_conf as FilterConf;
      Filter l_filter = FilterModel.Instance.GetValue(l_conf.FilterId);
      MultiIndexDictionary<UInt32, string, FilterValue> l_filterValueDic = FilterValueModel.Instance.GetDictionary(l_conf.FilterId);

      if (l_filter == null && l_filterValueDic != null)
        return;
      foreach (FilterValue l_filterValue in l_filterValueDic.SortedValues)
      {
        ResultKey l_key = p_parentKey + new ResultKey(0, ResultKey.GetSortKey(false, l_filter.Axis, l_filterValue.Id), "", 0, 0, 0);
        HierarchyItem l_newItem = SetDimension(p_dgv, p_dimension, p_parent, l_key, l_filterValue.Name);

        if (l_newItem != null)
          InitDimension(p_dgv, p_tabId, p_conf.Child, p_dimension, l_newItem.Items, l_key);
      }
    }

    private void AccountBuilder(DGV p_dgv, UInt32 p_tabId, CUIDimensionConf p_conf,
      DGVDimension p_dimension, HierarchyItemsCollection p_parent, ResultKey p_parentKey)
    {
      List<Account> l_accountList = AccountModel.Instance.GetChildren(p_tabId);

      if (l_accountList == null)
        return;
      l_accountList.Sort();
      foreach (Account l_account in l_accountList)
      {
        if (l_account.ParentId != p_tabId)
          continue;
        if (l_account.Process != m_computeConfig.Request.Process || l_account.FormulaType == Account.FormulaTypes.TITLE)
          continue;
        ResultKey l_key = p_parentKey + new ResultKey(l_account.Id, "", "", 0, 0, 0);
        HierarchyItem l_newItem = SetDimension(p_dgv, p_dimension, p_parent, l_key, l_account.Name);
        CUIDimensionConf l_childConf = new CUIDimensionConf(typeof(Account));

        l_childConf.Child = p_conf.Child;
        AccountBuilder(p_dgv, l_account.Id, l_childConf, p_dimension, l_newItem.Items, l_key);
        if (l_newItem != null)
          InitDimension(p_dgv, p_tabId, p_conf.Child, p_dimension, l_newItem.Items, l_key);
      }
    }

    #endregion

    #region Clean

    void RemoveOrphanDimensions()
    {
      foreach (vTabPage l_tab in m_tabCtrl.TabPages)
      {
        if (l_tab.Controls.Count > 0)
        {
          DGV l_dgv = l_tab.Controls[0] as DGV;

          RemoveOrphanColumns(l_dgv);
          RemoveOrphanRows(l_dgv);
        }
      }
    }

    void RemoveOrphanRows(DGV p_dgv)
    {
      foreach (ResultKey l_rowKey in p_dgv.Rows.Keys)
      {
        if (l_rowKey.IsClientSort() == false)
          continue;
        bool hasData = false;

        foreach (ResultKey l_columnKey in p_dgv.Columns.Keys)
        {
          object l_value = p_dgv.GetCellValue(l_rowKey, l_columnKey);

          if ((l_value != null && (double)l_value != 0.0))
          {
            hasData = true;
            break;
          }
        }
        if (!hasData)
          p_dgv.DeleteRow(l_rowKey);
      }
    }

    void RemoveOrphanColumns(DGV p_dgv)
    {
      foreach (ResultKey l_columnKey in p_dgv.Columns.Keys)
      {
        if (l_columnKey.IsClientSort() == false)
          continue;
        bool hasData = false;

        foreach (ResultKey l_rowKey in p_dgv.Rows.Keys)
        {
          object l_value = p_dgv.GetCellValue(l_rowKey, l_columnKey);

          if ((l_value != null && (double)l_value != 0.0))
          {
            hasData = true;
            break;
          }
        }
        if (!hasData)
          p_dgv.DeleteColumn(l_columnKey);
      }
    }

    #endregion

    internal void DropOnExcel(bool p_copyOnlyExpanded)
    {
      if (m_controller.Config != null)
      {
        string l_entityName = AxisElemModel.Instance.GetValueName(m_controller.Config.Request.EntityId);
        string l_versionName = VersionModel.Instance.GetValueName(m_controller.Config.Request.Versions[0]);
        Currency l_currency = CurrencyModel.Instance.GetValue(m_controller.Config.Request.CurrencyId);

        if (l_currency == null)
          return;
        Range destination = WorksheetWriter.CreateReceptionWS(l_entityName,
        new string[] { Local.GetValue("CUI.entity"), Local.GetValue("CUI.version"), Local.GetValue("CUI.currency") },
        new string[] { l_entityName, l_versionName, l_currency.Name });

        Int32 i = 1;
        foreach (vTabPage l_tab in m_tabCtrl.TabPages)
        {
          vDataGridView DGV = l_tab.Controls[0] as vDataGridView;
          WorksheetWriter.CopyDGVToExcelGeneric(DGV, destination, l_currency.Symbol, ref i, ref p_copyOnlyExpanded);
        }
        destination.Worksheet.Columns.AutoFit();
        destination.Worksheet.Outline.ShowLevels(RowLevels: 1);
        AddinModule.CurrentInstance.ExcelApp.ActiveWindow.Activate();
      }
    }
  }
}
