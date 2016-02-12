using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VIBlend.WinForms.Controls;

namespace FBI.MVC.View
{
  using Controller;
  using Utils;
  using Model.CRUD;
  using Forms;
  using Model;

  public partial class CUI2RightPane : UserControl, IView
  {
    #region Variables

    private CUIRightPaneController m_controller = null;
    SafeDictionary<UInt32, Tuple<string, CuiDgvConf>> m_dimensions;
    UInt32 m_dimensionId = 0;
    FbiTreeView<NamedCRUDEntity> m_dimensionTV;

    #endregion

    #region Initialize

    public CUI2RightPane()
    {
      InitializeComponent();
      m_dimensions = new SafeDictionary<uint, Tuple<string, CuiDgvConf>>();
      MultilangueSetup();
    }

    public void LoadView()
    {
      SetToDimensionTV(GenerateDimension(Local.GetValue("CUI.dimension.adjustment"), new AxisElemConf(AxisType.Adjustment)));
      LoadFilters(AxisType.Adjustment);
      SetToDimensionTV(GenerateDimension(Local.GetValue("CUI.dimension.client"), new AxisElemConf(AxisType.Client)));
      LoadFilters(AxisType.Client);
      SetToDimensionTV(GenerateDimension(Local.GetValue("CUI.dimension.entity"), new AxisElemConf(AxisType.Entities)));
      LoadFilters(AxisType.Entities);
      SetToDimensionTV(GenerateDimension(Local.GetValue("CUI.dimension.product"), new AxisElemConf(AxisType.Product)));
      LoadFilters(AxisType.Product);
      SetToDimensionTV(GenerateDimension(Local.GetValue("CUI.dimension.employee"), new AxisElemConf(AxisType.Employee)));
      LoadFilters(AxisType.Employee);
      SetToDimensionTV(GenerateDimension(Local.GetValue("CUI.dimension.day"), new PeriodConf(TimeConfig.DAYS)));
      SetToDimensionTV(GenerateDimension(Local.GetValue("CUI.dimension.week"), new PeriodConf(TimeConfig.WEEK)));
      SetToDimensionTV(GenerateDimension(Local.GetValue("CUI.dimension.month"), new PeriodConf(TimeConfig.MONTHS)));
      SetToDimensionTV(GenerateDimension(Local.GetValue("CUI.dimension.year"), new PeriodConf(TimeConfig.YEARS)));
      SetToDimensionTV(GenerateDimension(Local.GetValue("CUI.dimension.version"), new CuiDgvConf(typeof(Version))));
    }

    void LoadFilters(AxisType p_type)
    {
      MultiIndexDictionary<UInt32, string, Filter> l_filterDic = FilterModel.Instance.GetDictionary(p_type);

      foreach (Filter l_filter in l_filterDic.Values)
        SetToDimensionTV(GenerateDimension(l_filter.Name, new CuiDgvConf(typeof(Filter))));
    }

    void SetToDimensionTV(UInt32 p_dimensionId)
    {
      vTreeNode l_node = new vTreeNode();
      Tuple<string, CuiDgvConf> l_dimension = m_dimensions[p_dimensionId];

      if (l_dimension == null)
        return;
      l_node.Value = p_dimensionId;
      l_node.Text = l_dimension.Item1;
      m_dimensionTV.Add(l_node);
    }

    UInt32 GenerateDimension(string p_name, CuiDgvConf p_conf)
    {
      UInt32 l_id = m_dimensionId++;

      m_dimensions[l_id] = new Tuple<string, CuiDgvConf>(p_name, p_conf);
      return (l_id);
    }

    private void MultilangueSetup()
    {
      this.m_columnsLabel.Text = Local.GetValue("CUI.columns_label");
      this.m_rowsLabel.Text = Local.GetValue("CUI.rows_label");
      this.UpdateBT.Text = Local.GetValue("CUI.refresh");
      this.m_fieldChoiceLabel.Text = Local.GetValue("CUI.fields_choice");
    }

    public void SetController(IController p_controller)
    {
      this.m_controller = p_controller as CUIRightPaneController;
    }

    #endregion

  }
}
