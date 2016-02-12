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

  using ResultViewDgvKey = Tuple<Type, UInt32>;
  using DGVDimension = FBI.Forms.BaseFbiDataGridView<Tuple<Type, UInt32>>.Dimension;

  public partial class ResultView : UserControl, IView
  {
    BaseFbiDataGridView<ResultViewDgvKey> m_dgv = new BaseFbiDataGridView<ResultViewDgvKey>();
    private delegate void DGVBuilder(CUIDimensionConf p_conf, DGVDimension p_dimension, HierarchyItemsCollection p_parent);
    SafeDictionary<Type, DGVBuilder> m_builderList = new SafeDictionary<Type,DGVBuilder>();
    ComputeConfig m_computeConfig;
 
    public ResultView()
    {
      InitializeComponent();
    }

    public void SetController(IController p_controller)
    {

    }

    public void LoadView()
    {
      m_builderList.Add(typeof(PeriodModel), PeriodBuilder);
     /* m_builderList.Add(typeof(VersionModel), VersionBuilder);
      m_builderList.Add(typeof(AxisElemModel), AxisElemBuilder);
      m_builderList.Add(typeof(FilterValueModel), FilterValueBuilder);
      m_builderList.Add(typeof(AccountModel), AccountBuilder);*/
    }

    public void PrepareDgv(ComputeConfig p_config)
    {
      m_computeConfig = p_config;
      InitDimension(p_config.Rows, DGVDimension.ROW, m_dgv.RowsHierarchy.Items);
      InitDimension(p_config.Columns, DGVDimension.COLUMN, m_dgv.ColumnsHierarchy.Items);
    }

    private void InitDimension(CUIDimensionConf p_conf, DGVDimension p_dimension, HierarchyItemsCollection p_parent)
    {
      if (p_conf == null)
        return;
      if (m_builderList[p_conf.ModelType] != null)
        m_builderList[p_conf.ModelType](p_conf, p_dimension, p_parent);
    }

    private void PeriodBuilder(CUIDimensionConf p_conf, DGVDimension p_dimension, HierarchyItemsCollection p_parent)
    {
      PeriodConf l_conf = p_conf as PeriodConf;
      List<int> l_periodList = PeriodModel.GetPeriodList(m_computeConfig.Request.StartPeriod, m_computeConfig.Request.NbPeriods, l_conf.PeriodType);
      string l_formatedDate;

      foreach (int l_date in l_periodList)
      {
        l_formatedDate = DateTime.FromOADate(l_date).ToString("MMM d yyyy", DateTimeFormatInfo.InvariantInfo);

        HierarchyItem l_newItem = m_dgv.SetDimension(p_dimension, p_parent, new ResultViewDgvKey(p_conf.ModelType, (UInt32)l_date), l_formatedDate);

        if (l_newItem != null)
         InitDimension(p_conf.Child, p_dimension, l_newItem.Items);
      }
    }

    private void VersionBuilder(CUIDimensionConf p_conf, DGVDimension p_dimension, HierarchyItemsCollection p_parent)
    {

    }

    private void AxisElemBuilder(CUIDimensionConf p_conf, DGVDimension p_dimension, HierarchyItemsCollection p_parent)
    {
      AxisElemConf l_conf = p_conf as AxisElemConf;

      foreach (AxisElem l_elem in AxisElemModel.Instance.GetDictionary(l_conf.AxisTypeId).SortedValues)
      {
        HierarchyItem l_newItem = m_dgv.SetDimension(p_dimension, p_parent, new ResultViewDgvKey(p_conf.ModelType, (UInt32)l_elem.Id), l_elem.Name);

        if (l_newItem != null)
          InitDimension(p_conf, p_dimension, l_newItem.Items);
      }
    }

    private void FilterValueBuilder(CUIDimensionConf p_conf, DGVDimension p_dimension, HierarchyItemsCollection p_parent)
    {
      FilterConf l_conf = p_conf as FilterConf;

      foreach (FilterValue l_filterValue in FilterValueModel.Instance.GetDictionary(l_conf.FilterId).SortedValues)
      {
        HierarchyItem l_newItem = m_dgv.SetDimension(p_dimension, p_parent, new ResultViewDgvKey(p_conf.ModelType, (UInt32)l_filterValue.Id), l_filterValue.Name);

        if (l_newItem != null)
          InitDimension(p_conf, p_dimension, l_newItem.Items);
      }
    }

    private void AccountBuilder(CUIDimensionConf p_conf, DGVDimension p_dimension, HierarchyItemsCollection p_parent)
    {
      foreach (Account l_account in AccountModel.Instance.GetDictionary().SortedValues)
      {
        HierarchyItem l_newItem = m_dgv.SetDimension(p_dimension, p_parent, new ResultViewDgvKey(p_conf.ModelType, (UInt32)l_account.Id), l_account.Name);

        if (l_newItem != null)
          InitDimension(p_conf, p_dimension, l_newItem.Items);
      }
    }
  }
}
