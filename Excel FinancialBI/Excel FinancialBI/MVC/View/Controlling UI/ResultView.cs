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

namespace FBI.MVC.View
{
  using Controller;
  using Forms;
  using Model;
  using Model.CRUD;
  using Utils;

  using DGVDimension = FBI.Forms.BaseFbiDataGridView<Model.CRUD.ResultKey>.Dimension;

  public partial class ResultView : UserControl, IView
  {
    BaseFbiDataGridView<ResultKey> m_dgv = new BaseFbiDataGridView<ResultKey>();
    private delegate void DGVBuilder(CUIDimensionConf p_conf, DGVDimension p_dimension, HierarchyItemsCollection p_parent, ResultKey p_parentKey);
    SafeDictionary<Type, DGVBuilder> m_builderList = new SafeDictionary<Type,DGVBuilder>();
    ComputeConfig m_computeConfig;
    ResultController m_controller;
 
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
      Controls.Add(m_dgv);
      m_dgv.Dock = DockStyle.Fill;
      m_builderList.Add(typeof(PeriodModel), PeriodBuilder);
      m_builderList.Add(typeof(VersionModel), VersionBuilder);
      m_builderList.Add(typeof(AxisElemModel), AxisElemBuilder);
      m_builderList.Add(typeof(FilterModel), FilterValueBuilder);
      m_builderList.Add(typeof(AccountModel), AccountBuilder);
    }

    public void PrepareDgv(ComputeConfig p_config)
    {
      m_dgv.ClearColumns();
      m_dgv.ClearRows();
      m_computeConfig = p_config;
      InitDimension(p_config.Rows, DGVDimension.ROW, m_dgv.RowsHierarchy.Items, new ResultKey());
      InitDimension(p_config.Columns, DGVDimension.COLUMN, m_dgv.ColumnsHierarchy.Items, 
        new ResultKey(0, "", "", 0, 0, (m_computeConfig.Request.Versions.Count <= 1) ? m_computeConfig.Request.Versions[0] : 0, false));
      m_dgv.Refresh();
    }

    public delegate void FillDGV_delegate(SafeDictionary<uint, ComputeResult> p_data);
    public void FillDGV(SafeDictionary<uint, ComputeResult> p_data)
    {
      if (m_dgv.InvokeRequired)
      {
        FillDGV_delegate func = new FillDGV_delegate(FillDGV);
        Invoke(func, p_data);
      }
      else
      {
        foreach (ResultKey l_rowKey in m_dgv.Rows.Keys)
        {
          foreach (ResultKey l_columnKey in m_dgv.Columns.Keys)
          {
            ResultKey l_key = l_rowKey + l_columnKey;
            if (p_data[l_key.VersionId] == null)
              continue;

            if (p_data[l_key.VersionId].Values.ContainsKey(l_key) == false)
              continue;
            double l_value = p_data[l_key.VersionId].Values[l_key];

            m_dgv.FillField(l_rowKey, l_columnKey, l_value);
          }
        }
      }
    }

    private void InitDimension(CUIDimensionConf p_conf, DGVDimension p_dimension, HierarchyItemsCollection p_parent, ResultKey p_parentKey)
    {
      if (p_conf == null)
        return;
      if (m_builderList[p_conf.ModelType] != null)
        m_builderList[p_conf.ModelType](p_conf, p_dimension, p_parent, p_parentKey);
    }

    Int32 GetNbPeriod(Int32 p_nbPeriod, TimeConfig p_config)
    {
      switch (p_config)
      {
        case TimeConfig.WEEK:
          return (p_nbPeriod / 7);
        case TimeConfig.YEARS:
          return (p_nbPeriod / 12);
      }
      return (p_nbPeriod);
    }

    private void PeriodBuilder(CUIDimensionConf p_conf, DGVDimension p_dimension, HierarchyItemsCollection p_parent, ResultKey p_parentKey)
    {
      PeriodConf l_conf = p_conf as PeriodConf;
      List<int> l_periodList;
      string l_formatedDate;
      Int32 l_startPeriod = m_computeConfig.Request.StartPeriod;
      l_periodList = (l_conf.IsSubPeriod) ? PeriodModel.GetSubPeriods(l_conf.ParentType, l_conf.ParentPeriod) :
        PeriodModel.GetPeriodList(l_startPeriod, GetNbPeriod(m_computeConfig.Request.NbPeriods, l_conf.PeriodType), l_conf.PeriodType);

      foreach (int l_date in l_periodList)
      {
        l_formatedDate = PeriodModel.GetFormatedDate(l_date, l_conf.PeriodType);

        ResultKey l_key = p_parentKey + new ResultKey(0, "", "", l_conf.PeriodType, l_date, 0);
        HierarchyItem l_newItem = m_dgv.SetDimension(p_dimension, p_parent, l_key, l_formatedDate);
        
        if (l_newItem != null)
        {
          if (p_conf.Child != null && p_conf.Child.ModelType == typeof(PeriodModel))
          {
            PeriodConf l_childConf = p_conf.Child as PeriodConf;

            l_childConf.ParentPeriod = (l_periodList[0] == l_date) ? l_startPeriod : l_date;
          }
          InitDimension(p_conf.Child, p_dimension, l_newItem.Items, l_key);
        }
      }
    }

    private void VersionBuilder(CUIDimensionConf p_conf, DGVDimension p_dimension, HierarchyItemsCollection p_parent, ResultKey p_parentKey)
    {
      VersionConf l_conf = p_conf as VersionConf;

      foreach (UInt32 l_versionId in m_computeConfig.Request.Versions)
      {
        Version l_version = VersionModel.Instance.GetValue(l_versionId);
        if (l_version == null)
          continue;

        ResultKey l_key = p_parentKey + new ResultKey(0, "", "", 0, 0, l_version.Id,  true);
        HierarchyItem l_newItem = m_dgv.SetDimension(p_dimension, p_parent, l_key, l_version.Name);

        if (l_newItem != null)
          InitDimension(p_conf.Child, p_dimension, l_newItem.Items, l_key);
      }
    }

    private void AxisElemBuilder(CUIDimensionConf p_conf, DGVDimension p_dimension, HierarchyItemsCollection p_parent, ResultKey p_parentKey)
    {
      AxisElemConf l_conf = p_conf as AxisElemConf;

      List<AxisElem> l_axisList = new List<AxisElem>();

      if (l_conf.ParentId == 0 && l_conf.AxisTypeId == AxisType.Entities)
        l_axisList.Add(AxisElemModel.Instance.GetValue(m_computeConfig.Request.EntityId));
      else
        l_axisList = AxisElemModel.Instance.GetChildren(l_conf.AxisTypeId, l_conf.ParentId);

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
        HierarchyItem l_newItem = m_dgv.SetDimension(p_dimension, p_parent, l_key, l_elem.Name);

        l_childAxisConf.ParentId = l_elem.Id;
        AxisElemBuilder(l_childAxisConf, p_dimension, (l_newItem != null) ? l_newItem.Items : p_parent, (l_conf.AxisTypeId == AxisType.Entities) ? l_key : p_parentKey);

        if (l_newItem != null)
          InitDimension(p_conf.Child, p_dimension, l_newItem.Items, l_key);
      }
    }

    private void FilterValueBuilder(CUIDimensionConf p_conf, DGVDimension p_dimension, HierarchyItemsCollection p_parent, ResultKey p_parentKey)
    {
      FilterConf l_conf = p_conf as FilterConf;
      Filter l_filter = FilterModel.Instance.GetValue(l_conf.FilterId);
      MultiIndexDictionary<UInt32, string, FilterValue> l_filterValueDic = FilterValueModel.Instance.GetDictionary(l_conf.FilterId);

      if (l_filter == null && l_filterValueDic != null)
        return;
      foreach (FilterValue l_filterValue in l_filterValueDic.SortedValues)
      {
        ResultKey l_key = p_parentKey + new ResultKey(0, ResultKey.GetSortKey(false, l_filter.Axis, l_filterValue.Id), "", 0, 0, 0);
        HierarchyItem l_newItem = m_dgv.SetDimension(p_dimension, p_parent, l_key, l_filterValue.Name);

        if (l_newItem != null)
          InitDimension(p_conf.Child, p_dimension, l_newItem.Items, l_key);
      }
    }

    private void AccountBuilder(CUIDimensionConf p_conf, DGVDimension p_dimension, HierarchyItemsCollection p_parent, ResultKey p_parentKey)
    {
      foreach (Account l_account in AccountModel.Instance.GetDictionary().SortedValues)
      {
        if (l_account.Process != m_computeConfig.Request.Process || l_account.FormulaType == Account.FormulaTypes.TITLE)
          continue;
        ResultKey l_key = p_parentKey + new ResultKey(l_account.Id, "", "", 0, 0, 0);
        HierarchyItem l_newItem = m_dgv.SetDimension(p_dimension, p_parent, l_key, l_account.Name);

        if (l_newItem != null)
          InitDimension(p_conf.Child, p_dimension, l_newItem.Items, l_key);
      }
    }
  }
}
