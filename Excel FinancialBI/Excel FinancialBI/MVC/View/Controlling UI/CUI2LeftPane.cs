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

  public partial class CUI2LeftPane : UserControl, IView
  {

    #region Variables

    private enum ComboBoxType
    {
      EntitiesCategories = 1,
      Clients,
      ClientsCategories,
      Products,
      ProductsCategories,
      EmployeesCategories,
      Adjustments,
      AdjustmentsCategories,
      Periods,
      Versions
    }

    private CUILeftPaneController m_controller = null;

    private FbiTreeView<AxisElem> m_entitiesTV = null;
    private FbiTreeView<Filter> m_entitiesCat = null;
    private FbiTreeView<AxisElem> m_clients = null;
    private FbiTreeView<Filter> m_clientsCat = null;
    private FbiTreeView<AxisElem> m_products = null;
    private FbiTreeView<Filter> m_productsCat = null;
    private FbiTreeView<Filter> m_employeesCat = null;
    private FbiTreeView<AxisElem> m_adjustments = null;
    private FbiTreeView<Filter> m_adjustmentsCat = null;
    //Periods
    //Versions
    private PeriodRangeSelectionControl m_periodControl = null;

    #endregion

    #region Initialize

    public CUI2LeftPane()
    {
      InitializeComponent();
    }

    public void SetController(IController p_controller)
    {
      this.m_controller = p_controller as CUILeftPaneController;
    }

    public void InitView()
    {
      this.TreeViewInit();
      this.ComboBoxInit();

      this.MultilangueSetup();
      this.HideAllTV();
    }

    private void ComboBoxInit()
    {
      ListItem l_listItem = new ListItem();
      l_listItem.Text = Local.GetValue("CUI.combobox_entities_cat");
      l_listItem.Value = ComboBoxType.EntitiesCategories;
      this.SelectionCB.Items.Add(l_listItem);

      l_listItem = new ListItem();
      l_listItem.Text = Local.GetValue("CUI.combobox_clients");
      l_listItem.Value = ComboBoxType.Clients;
      this.SelectionCB.Items.Add(l_listItem);

      l_listItem = new ListItem();
      l_listItem.Text = Local.GetValue("CUI.combobox_clients_cat");
      l_listItem.Value = ComboBoxType.ClientsCategories;
      this.SelectionCB.Items.Add(l_listItem);

      l_listItem = new ListItem();
      l_listItem.Text = Local.GetValue("CUI.combobox_products");
      l_listItem.Value = ComboBoxType.Products;
      this.SelectionCB.Items.Add(l_listItem);

      l_listItem = new ListItem();
      l_listItem.Text = Local.GetValue("CUI.combobox_products_cat");
      l_listItem.Value = ComboBoxType.ProductsCategories;
      this.SelectionCB.Items.Add(l_listItem);

      l_listItem = new ListItem();
      l_listItem.Text = Local.GetValue("CUI.combobox_employees_cat");
      l_listItem.Value = ComboBoxType.EmployeesCategories;
      this.SelectionCB.Items.Add(l_listItem);

      l_listItem = new ListItem();
      l_listItem.Text = Local.GetValue("CUI.combobox_adjustments");
      l_listItem.Value = ComboBoxType.Adjustments;
      this.SelectionCB.Items.Add(l_listItem);

      l_listItem = new ListItem();
      l_listItem.Text = Local.GetValue("CUI.combobox_adjustments_cat");
      l_listItem.Value = ComboBoxType.AdjustmentsCategories;
      this.SelectionCB.Items.Add(l_listItem);

      l_listItem = new ListItem();
      l_listItem.Text = Local.GetValue("CUI.combobox_periods");
      l_listItem.Value = ComboBoxType.Periods;
      this.SelectionCB.Items.Add(l_listItem);

      l_listItem = new ListItem();
      l_listItem.Text = Local.GetValue("CUI.combobox_versions");
      l_listItem.Value = ComboBoxType.Versions;
      this.SelectionCB.Items.Add(l_listItem);

      this.SelectionCB.DropDownList = true;
      this.SelectionCB.SelectedItemChanged += OnSelectionCBSelectedItemChanged;
    }

    private void TreeViewInit()
    {
      this.m_entitiesTV = new FbiTreeView<AxisElem>(AxisElemModel.Instance.GetDictionary(AxisType.Entities));
      this.m_entitiesTV.CheckBoxes = true;
      this.m_entitiesTV.TriStateMode = true;
      this.m_entitiesTV.Dock = DockStyle.Fill;
      this.SplitContainer.Panel1.Controls.Add(this.m_entitiesTV);

      this.m_entitiesCat = new FbiTreeView<Filter>(FilterModel.Instance.GetDictionary(AxisType.Entities));
      this.m_entitiesCat.CheckBoxes = true;
      this.m_entitiesCat.TriStateMode = true;
      this.m_entitiesCat.Dock = DockStyle.Fill;
      this.m_selectionTableLayout.Controls.Add(this.m_entitiesCat);

      this.m_clients = new FbiTreeView<AxisElem>(AxisElemModel.Instance.GetDictionary(AxisType.Client));
      this.m_clients.CheckBoxes = true;
      this.m_clients.TriStateMode = true;
      this.m_clients.Dock = DockStyle.Fill;
      this.m_selectionTableLayout.Controls.Add(this.m_clients);

      this.m_clientsCat = new FbiTreeView<Filter>(FilterModel.Instance.GetDictionary(AxisType.Client));
      this.m_clientsCat.CheckBoxes = true;
      this.m_clientsCat.TriStateMode = true;
      this.m_clientsCat.Dock = DockStyle.Fill;
      this.m_selectionTableLayout.Controls.Add(this.m_clientsCat);

      this.m_products = new FbiTreeView<AxisElem>(AxisElemModel.Instance.GetDictionary(AxisType.Product));
      this.m_products.CheckBoxes = true;
      this.m_products.TriStateMode = true;
      this.m_products.Dock = DockStyle.Fill;
      this.m_selectionTableLayout.Controls.Add(this.m_products);

      this.m_productsCat = new FbiTreeView<Filter>(FilterModel.Instance.GetDictionary(AxisType.Product));
      this.m_productsCat.CheckBoxes = true;
      this.m_productsCat.TriStateMode = true;
      this.m_productsCat.Dock = DockStyle.Fill;
      this.m_selectionTableLayout.Controls.Add(this.m_productsCat);

      this.m_employeesCat = new FbiTreeView<Filter>(FilterModel.Instance.GetDictionary(AxisType.Employee));
      this.m_employeesCat.CheckBoxes = true;
      this.m_employeesCat.TriStateMode = true;
      this.m_employeesCat.Dock = DockStyle.Fill;
      this.m_selectionTableLayout.Controls.Add(this.m_employeesCat);

      this.m_adjustments = new FbiTreeView<AxisElem>(AxisElemModel.Instance.GetDictionary(AxisType.Adjustment));
      this.m_adjustments.CheckBoxes = true;
      this.m_adjustments.TriStateMode = true;
      this.m_adjustments.Dock = DockStyle.Fill;
      this.m_selectionTableLayout.Controls.Add(this.m_adjustments);

      this.m_adjustmentsCat = new FbiTreeView<Filter>(FilterModel.Instance.GetDictionary(AxisType.Adjustment));
      this.m_adjustmentsCat.CheckBoxes = true;
      this.m_adjustmentsCat.TriStateMode = true;
      this.m_adjustmentsCat.Dock = DockStyle.Fill;
      this.m_selectionTableLayout.Controls.Add(this.m_adjustmentsCat);
    }

    private void MultilangueSetup()
    {
      this.SelectionCB.Text = Local.GetValue("CUI.selection");
      this.m_entitySelectionLabel.Text = Local.GetValue("CUI.entities_selection");
      this.SelectAllToolStripMenuItem.Text = Local.GetValue("CUI.select_all");
      this.UnselectAllToolStripMenuItem.Text = Local.GetValue("CUI.unselect_all");
    }

    #endregion

    #region Event

    private void OnSelectionCBSelectedItemChanged(object p_sender, EventArgs p_e)
    {
      ListItem l_listItem;

      this.HideAllTV();
      if ((l_listItem = ((vComboBox)p_sender).SelectedItem) != null)
      {
        switch ((ComboBoxType)l_listItem.Value)
        {
          case ComboBoxType.EntitiesCategories:
            this.m_entitiesCat.Visible = true;
            break;
          case ComboBoxType.Clients:
            this.m_clients.Visible = true;
            break;
          case ComboBoxType.ClientsCategories:
            this.m_clientsCat.Visible = true;
            break;
          case ComboBoxType.Products:
            this.m_products.Visible = true;
            break;
          case ComboBoxType.ProductsCategories:
            this.m_productsCat.Visible = true;
            break;
          case ComboBoxType.EmployeesCategories:
            this.m_employeesCat.Visible = true;
            break;
          case ComboBoxType.Adjustments:
            this.m_adjustments.Visible = true;
            break;
          case ComboBoxType.AdjustmentsCategories:
            this.m_adjustmentsCat.Visible = true;
            break;
          case ComboBoxType.Periods:
            break;
          case ComboBoxType.Versions:
            break;
        }
      }
    }

    #endregion

    #region Utils

    private void HideAllTV()
    {
      this.m_entitiesCat.Visible = false;
      this.m_clients.Visible = false;
      this.m_clientsCat.Visible = false;
      this.m_products.Visible = false;
      this.m_productsCat.Visible = false;
      this.m_employeesCat.Visible = false;
      this.m_adjustments.Visible = false;
      this.m_adjustmentsCat.Visible = false;
      //
      //
    }

    #endregion

  }
}
