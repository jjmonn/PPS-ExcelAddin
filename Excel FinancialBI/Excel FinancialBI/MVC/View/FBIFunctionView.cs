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
  using Forms;
  using Model;
  using Model.CRUD;

  public partial class FBIFunctionView : UserControl, IView
  {
    SafeDictionary<AxisType, vTreeViewBox> m_axisElemTV;
    SafeDictionary<UInt32, ListItem> m_currencyListItems = new SafeDictionary<uint, ListItem>();
    SafeDictionary<Int32, ListItem> m_periodListItems = new SafeDictionary<int, ListItem>();
    SafeDictionary<ListItem, TimeConfig> m_aggregationListItems = new SafeDictionary<ListItem, TimeConfig>();
    bool m_editing = false;
    FBIFunctionViewController m_controller;
    bool m_eventSuscribed = false;

    public FBIFunctionView()
    {
      InitializeComponent();
      m_axisElemTV = new SafeDictionary<AxisType, vTreeViewBox>();
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as FBIFunctionViewController;
    }

    public void LoadView()
    {
      MultilangueSetup();
      InitTV();
      LoadAggregations();
      LoadPeriods();
      LoadCurrencies();
      if (!m_eventSuscribed)
      {
        SuscribeEvents();
        m_eventSuscribed = true;
      }
      RefreshFormula();
    }

    public void RefreshFormula()
    {
      FBIFunctionExcelController.LastExecutedFunction = null;
      AddinModule.CurrentInstance.ExcelApp.ActiveCell.Formula = AddinModule.CurrentInstance.ExcelApp.ActiveCell.Formula;
      if (FBIFunctionExcelController.LastExecutedFunction != null)
        LoadFromFormula(FBIFunctionExcelController.LastExecutedFunction);
    }

    public void LoadFromFormula(FBIFunction p_formula)
    {
      m_axisElemTV[AxisType.Entities].TreeView.SelectedNode =
        FbiTreeView<AxisElem>.FindNode(m_axisElemTV[AxisType.Entities].TreeView, p_formula.EntityId);
      m_accountTree.TreeView.SelectedNode = FbiTreeView<Account>.FindNode(m_accountTree.TreeView, p_formula.AccountId);
      m_currencyCB.SelectedItem = m_currencyListItems[p_formula.CurrencyId];
      m_versionTree.TreeView.SelectedNode = FbiTreeView<Version>.FindNode(m_versionTree.TreeView, p_formula.VersionId);
      foreach (KeyValuePair<ListItem, TimeConfig> l_pair in m_aggregationListItems)
        if (l_pair.Value == p_formula.Aggregation)
        {
          m_aggregationCB.SelectedItem = l_pair.Key;
          break;
        }
      m_periodCB.SelectedItem = m_periodListItems[(Int32)p_formula.Period.ToOADate()];

      foreach (KeyValuePair<AxisType, List<string>> l_axis in p_formula.AxisElems)
        foreach (string l_axisElem in l_axis.Value)
          FbiTreeView<AxisElem>.CheckNode(m_axisElemTV[l_axis.Key].TreeView, AxisElemModel.Instance.GetValueId(l_axisElem));
      foreach (string l_filterValue in p_formula.Filters)
      {
        foreach (vTreeNode l_axisNode in m_categoriesFilterTree.TreeView.Nodes)
          foreach (vTreeNode l_filterNode in l_axisNode.Nodes)
            CheckNode(l_filterNode, FilterValueModel.Instance.GetValueId(l_filterValue));
      }
    }

    bool CheckNode(vTreeNode p_parentNode, UInt32 p_value)
    {
      foreach (vTreeNode l_node in p_parentNode.Nodes)
      {
        if (l_node.Value != null && (UInt32)l_node.Value == p_value)
        {
          l_node.Checked = CheckState.Checked;
          return (true);
        }
        if (CheckNode(l_node, p_value))
          return (true);
      }
      return (false);
    }

    #region Initialize

    private void MultilangueSetup()
    {
      this.m_validateButton.Text = Local.GetValue("ppsbi.insert_formula");
      this.m_categoryFilterLabel.Text = Local.GetValue("ppsbi.categories_filter");
      this.m_productFilterLabel.Text = Local.GetValue("ppsbi.products_filter");
      this.m_clientFilterLabel.Text = Local.GetValue("ppsbi.clients_filter");
      this.m_aggregationLabel.Text = Local.GetValue("ppsbi.aggregation");
      this.m_versionLabel.Text = Local.GetValue("general.version");
      this.m_currencyLabel.Text = Local.GetValue("general.currency");
      this.m_entityLabel.Text = Local.GetValue("general.entity");
      this.m_accountLabel.Text = Local.GetValue("general.account");
      this.m_periodLabel.Text = Local.GetValue("general.period");
      this.m_adjustmentFilterLabel.Text = Local.GetValue("ppsbi.adjustments_filter");
      this.Text = Local.GetValue("ppsbi.title");
    }

    void SuscribeEvents()
    {
      m_validateButton.Click += ValidateFormula;
      m_categoriesFilterTree.TextChanged += SetCategoriesText;
      m_categoriesFilterTree.TreeView.NodeChecked += SetCategoriesText;
      m_axisElemTV[AxisType.Client].TextChanged += SetClientText;
      m_axisElemTV[AxisType.Client].TreeView.NodeChecked += SetClientText;
      m_axisElemTV[AxisType.Product].TextChanged += SetProductText;
      m_axisElemTV[AxisType.Product].TreeView.NodeChecked += SetProductText;
      m_axisElemTV[AxisType.Adjustment].TextChanged += SetAdjustmentText;
      m_axisElemTV[AxisType.Adjustment].TreeView.NodeChecked += SetAdjustmentText;
      m_versionTree.TreeView.AfterSelect += OnVersionSelectedChanged;
      m_aggregationCB.TextChanged += OnAggregationSelectedChanged;
    }

    public void CloseDrop()
    {
      foreach (vTreeViewBox l_tv in m_axisElemTV.Values)
        l_tv.CloseDropDown();
      m_accountTree.CloseDropDown();
      m_versionTree.CloseDropDown();
      m_categoriesFilterTree.CloseDropDown();
      m_periodCB.CloseDropDown();
      m_currencyCB.CloseDropDown();
    }

    public void LoadPeriods()
    {
      m_periodListItems.Clear();
      m_periodCB.Items.Clear();
      Version l_version = VersionModel.Instance.GetValue(SelectedVersion);

      m_periodCB.Enabled = (l_version != null && l_version.IsFolder == false && Enum.IsDefined(typeof(TimeConfig), SelectedAggregation));
      if (m_periodCB.Enabled == false)
        return;
      List<Int32> l_periodList;

      if (TimeUtils.IsParentConfig(SelectedAggregation, l_version.TimeConfiguration))
        l_periodList = PeriodModel.GetPeriodList((Int32)l_version.StartPeriod,
          TimeUtils.GetParentConfigNbPeriods(l_version.TimeConfiguration, (Int32)l_version.NbPeriod), SelectedAggregation);
      else
        l_periodList = PeriodModel.GetPeriodList((Int32)l_version.StartPeriod, (Int32)l_version.NbPeriod, SelectedAggregation);
      foreach (Int32 l_period in l_periodList)
        m_periodListItems[l_period] = m_periodCB.Items.Add(PeriodModel.GetFormatedDate(l_period, SelectedAggregation));
      m_periodCB.SelectedItem = m_periodListItems.FirstOrDefault().Value;
    }

    void LoadCurrencies()
    {
      m_currencyListItems.Clear();
      m_currencyCB.Items.Clear();
      foreach (UInt32 l_currencyId in CurrencyModel.Instance.GetUsedCurrencies())
      {
        ListItem l_item = m_currencyCB.Items.Add(CurrencyModel.Instance.GetValueName(l_currencyId));

        m_currencyListItems[l_currencyId] = l_item;
        if (l_currencyId == Properties.Settings.Default.currentCurrency)
          m_currencyCB.SelectedItem = l_item;
      }
    }

    void LoadAggregations()
    {
      Version l_version = VersionModel.Instance.GetValue(SelectedVersion);

      m_aggregationListItems.Clear();
      m_aggregationCB.Items.Clear();
      m_aggregationCB.Enabled = (l_version != null && l_version.IsFolder == false);
      if (!m_aggregationCB.Enabled)
        return;
      ListItem l_item = m_aggregationCB.Items.Add(TimeUtils.GetLocal(l_version.TimeConfiguration));

      m_aggregationListItems[l_item] = l_version.TimeConfiguration;
      m_aggregationCB.SelectedItem = l_item;

      TimeConfig l_parentConfig = TimeUtils.GetParentConfig(l_version.TimeConfiguration);
      if (l_version.TimeConfiguration != l_parentConfig)
      {
        l_item = m_aggregationCB.Items.Add(TimeUtils.GetLocal(l_parentConfig));

        m_aggregationListItems[l_item] = l_parentConfig;
      }
    }

    void InitTV()
    {
      m_accountTree.TreeView.SelectedNode = null;
      m_accountTree.TreeView.Nodes.Clear();
      FbiTreeView<Account>.Load(m_accountTree.TreeView.Nodes, AccountModel.Instance.GetDictionary());

      m_versionTree.TreeView.Nodes.Clear();
      FbiTreeView<Version>.Load(m_versionTree.TreeView.Nodes, VersionModel.Instance.GetDictionary());
      m_versionTree.TreeView.SelectedNode = FbiTreeView<Version>.FindNode(m_versionTree.TreeView, Properties.Settings.Default.version_id);

      AddAxisElem(m_entityTree, AxisType.Entities);
      AxisElem l_topEntity = AxisElemModel.Instance.GetTopEntity();
      if (l_topEntity != null)
        m_axisElemTV[AxisType.Entities].TreeView.SelectedNode = FbiTreeView<AxisElem>.FindNode(m_axisElemTV[AxisType.Entities].TreeView, l_topEntity.Id);
      AddAxisElem(m_clientTree, AxisType.Client);
      AddAxisElem(m_productTree, AxisType.Product);
      AddAxisElem(m_adjustmentTree, AxisType.Adjustment);

      m_clientTree.TreeView.TriStateMode = true;
      m_productTree.TreeView.TriStateMode = true;
      m_adjustmentTree.TreeView.TriStateMode = true;
      m_categoriesFilterTree.TreeView.TriStateMode = true;

      AFbiTreeView.InitTVFormat(m_categoriesFilterTree.TreeView);
      m_categoriesFilterTree.TreeView.Nodes.Clear();
      AddCategorieFilterTV(Local.GetValue("general.clients_filters"), AxisType.Client);
      AddCategorieFilterTV(Local.GetValue("general.products_filters"), AxisType.Product);
      AddCategorieFilterTV(Local.GetValue("general.adjustments_filters"), AxisType.Adjustment);
      AddCategorieFilterTV(Local.GetValue("general.entities_filters"), AxisType.Entities);

      m_clientTree.Text = "";
      m_productTree.Text = "";
      m_adjustmentTree.Text = "";
      m_categoriesFilterTree.Text = "";
    }

    void AddAxisElem(vTreeViewBox p_tvBox, AxisType p_axis)
    {
      p_tvBox.TreeView.Nodes.Clear();
      FbiTreeView<AxisElem>.Load(p_tvBox.TreeView.Nodes, AxisElemModel.Instance.GetDictionary(p_axis));
      m_axisElemTV[p_axis] = p_tvBox;
    }

    void AddCategorieFilterTV(string p_name, AxisType p_axis)
    {
      vTreeNode l_node = new vTreeNode(p_name);
      l_node.ShowCheckBox = false;

      m_categoriesFilterTree.TreeView.Nodes.Add(l_node);
      FbiFilterHierarchyTreeView.Load(l_node.Nodes, FilterModel.Instance.GetDictionary(p_axis));
      foreach (vTreeNode l_currentNode in l_node.Nodes)
        l_currentNode.ShowCheckBox = false;
    }

    #endregion

    #region User callbacks

    void OnVersionSelectedChanged(object sender, vTreeViewEventArgs e)
    {
      LoadPeriods();
      LoadAggregations();
    }

    void OnAggregationSelectedChanged(object sender, EventArgs e)
    {
      LoadPeriods();
    }

    void SetClientText(object p_sender, EventArgs e) { SetAxisElemText(AxisType.Client); }
    void SetProductText(object p_sender, EventArgs e) { SetAxisElemText(AxisType.Product); }
    void SetAdjustmentText(object p_sender, EventArgs e) { SetAxisElemText(AxisType.Adjustment); }

    void SetCategoriesText(object p_sender, EventArgs e)
    {
      if (m_editing)
        return;
      m_editing = true;
      List<string> l_list = SelectedFilterValues;

      m_categoriesFilterTree.Text = "";
      foreach (string l_fv in l_list)
        m_categoriesFilterTree.Text += l_fv + "; ";
      m_editing = false;
    }

    void SetAxisElemText(AxisType p_axis)
    {
      if (m_editing)
        return;
      m_editing = true;
      vTreeViewBox l_tvBox = m_axisElemTV[p_axis];

      if (l_tvBox == null)
        return;
      List<string> l_list = GetSelectedAxisElem(p_axis);

      l_tvBox.Text = "";
      foreach (string l_elem in l_list)
        l_tvBox.Text += l_elem + "; ";
      m_editing = false;
    }

    void ValidateFormula(object p_sender, EventArgs e)
    {
      if (m_controller.SaveFunction() == false)
        MsgBox.Show(m_controller.Error);
    }

    #endregion

    #region Accessors

    public string SelectedPeriod
    {
      get
      {
        foreach (KeyValuePair<Int32, ListItem> l_pair in m_periodListItems)
          if (l_pair.Value == m_periodCB.SelectedItem)
            return (DateTime.FromOADate(l_pair.Key).ToString("MM/dd/yyyy"));
        return (null);
      }
    }

    public List<string> GetSelectedAxisElem(AxisType p_axis)
    {
      vTreeView l_tv = m_axisElemTV[p_axis].TreeView;
      List<string> l_list = new List<string>();

      if (l_tv != null)
        foreach (vTreeNode l_node in l_tv.GetNodes())
          if (l_node.Checked == CheckState.Checked && l_node.ShowCheckBox)
            l_list.Add(l_node.Text);
      return (l_list);
    }

    public string SelectedVersion
    {
      get
      {
        vTreeNode l_node = m_versionTree.TreeView.SelectedNode;

        if (l_node == null)
          return ("");
        return (l_node.Text);
      }
    }

    public string SelectedAccount
    {
      get
      {
        vTreeNode l_node = m_accountTree.TreeView.SelectedNode;

        if (l_node == null)
          return ("");
        return (l_node.Text);
      }
    }

    public string SelectedEntity
    {
      get
      {
        vTreeNode l_node = m_entityTree.TreeView.SelectedNode;

        if (l_node == null)
          return ("");
        return (l_node.Text);
      }
    }

    public List<string> SelectedFilterValues
    {
      get
      {
        List<string> l_list = new List<string>();

        foreach (vTreeNode l_node in m_categoriesFilterTree.TreeView.GetNodes())
          if (l_node.Checked == CheckState.Checked && l_node.ShowCheckBox)
            l_list.Add(l_node.Text);
        return (l_list);
      }
    }

    public string SelectedCurrency
    {
      get
      {
        return (m_currencyCB.Text);
      }
    }

    public TimeConfig SelectedAggregation
    {
      get
      {
        if (m_aggregationCB.SelectedItem != null && m_aggregationListItems.ContainsKey(m_aggregationCB.SelectedItem))
          return (m_aggregationListItems[m_aggregationCB.SelectedItem]);
        return (TimeConfig)0;
      }
    }

    #endregion
  }
}
