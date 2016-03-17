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

  public partial class FBIFunctionView : Form, IView
  {
    FBIFunctionController m_controller;
    SafeDictionary<AxisType, vTreeViewBox> m_axisElemTV;
    bool m_editing = false;

    public FBIFunctionView()
    {
      InitializeComponent();
      m_axisElemTV = new SafeDictionary<AxisType, vTreeViewBox>();
    }

    private void MultilangueSetup()
    {
      this.validate_cmd.Text = Local.GetValue("ppsbi.insert_formula");
      this.m_categoryFilterLabel.Text = Local.GetValue("ppsbi.categories_filter");
      this.m_productFilterLabel.Text = Local.GetValue("ppsbi.products_filter");
      this.m_clientFilterLabel.Text = Local.GetValue("ppsbi.clients_filter");
      this.m_versionLabel.Text = Local.GetValue("general.version");
      this.m_currencyLabel.Text = Local.GetValue("general.currency");
      this.m_entityLabel.Text = Local.GetValue("general.entity");
      this.m_accountLabel.Text = Local.GetValue("general.account");
      this.m_periodLabel.Text = Local.GetValue("general.period");
      this.m_adjustmentFilterLabel.Text = Local.GetValue("ppsbi.adjustments_filter");
      this.Text = Local.GetValue("ppsbi.title");
    }

    public void SetController(IController p_controller)
    {
      m_controller = p_controller as FBIFunctionController;
    }

    public void LoadView()
    {
      MultilangueSetup();
      InitTV();
      m_periodPicker.FormatValue = "MM/yyyy";
      SuscribeEvents();
    }

    void SuscribeEvents()
    {
      m_categoriesFilterTree.TextChanged += SetCategoriesText;
      m_categoriesFilterTree.TreeView.NodeChecked += SetCategoriesText;
      m_axisElemTV[AxisType.Client].TextChanged += SetClientText;
      m_axisElemTV[AxisType.Client].TreeView.NodeChecked += SetClientText;
      m_axisElemTV[AxisType.Product].TextChanged += SetProductText;
      m_axisElemTV[AxisType.Product].TreeView.NodeChecked += SetProductText;
      m_axisElemTV[AxisType.Adjustment].TextChanged += SetAdjustmentText;
      m_axisElemTV[AxisType.Adjustment].TreeView.NodeChecked += SetAdjustmentText;
    }

    void SetClientText(object sender, EventArgs e) { SetAxisElemText(AxisType.Client); }
    void SetProductText(object sender, EventArgs e) { SetAxisElemText(AxisType.Product); }
    void SetAdjustmentText(object sender, EventArgs e) { SetAxisElemText(AxisType.Adjustment); }

    void SetCategoriesText(object sender, EventArgs e)
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

    void InitTV()
    {
      FbiTreeView<Account>.Load(m_accountTree.TreeView.Nodes, AccountModel.Instance.GetDictionary());
      foreach (UInt32 l_currencyId in CurrencyModel.Instance.GetUsedCurrencies())
        m_currencyCB.Items.Add(CurrencyModel.Instance.GetValueName(l_currencyId));
      FbiTreeView<Version>.Load(m_versionTree.TreeView.Nodes, VersionModel.Instance.GetDictionary());

      AddAxisElem(m_entityTree, AxisType.Entities);
      AddAxisElem(m_clientTree, AxisType.Client);
      AddAxisElem(m_productTree, AxisType.Product);
      AddAxisElem(m_adjustmentTree, AxisType.Adjustment);

      m_clientTree.TreeView.TriStateMode = true;
      m_productTree.TreeView.TriStateMode = true;
      m_adjustmentTree.TreeView.TriStateMode = true;
      m_categoriesFilterTree.TreeView.TriStateMode = true;
      AddCategorieFilterTV(Local.GetValue("general.clients_filters"), AxisType.Client);
      AddCategorieFilterTV(Local.GetValue("general.products_filters"), AxisType.Product);
      AddCategorieFilterTV(Local.GetValue("general.adjustments_filters"), AxisType.Adjustment);
      AddCategorieFilterTV(Local.GetValue("general.entities_filters"), AxisType.Entities);
    }

    void AddAxisElem(vTreeViewBox p_tvBox, AxisType p_axis)
    {
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

    #region Accessors

    public string SelectedPeriod
    {
      get
      {
        return m_periodPicker.Value.Value.ToString("0:dd/MM/yyyy");
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

    #endregion
  }
}
