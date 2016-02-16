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
      Entities = 1,
      EntitiesCategories,
      Clients,
      ClientsCategories,
      Products,
      ProductsCategories,
      Employees,
      EmployeesCategories,
      Adjustments,
      AdjustmentsCategories,
      Periods,
      Versions
    }

    private CUILeftPaneController m_controller = null;

    SafeDictionary<ComboBoxType, AFbiTreeView> m_selectionTVList = new SafeDictionary<ComboBoxType, AFbiTreeView>();
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
      m_controller = p_controller as CUILeftPaneController;
    }

    public void InitView()
    {
      TreeViewInit();
      ComboBoxInit();

      MultilangueSetup();
      HideAllTV();
    }

    private void ComboBoxInit()
    {
      InitCBListItem(Local.GetValue("CUI.adjustment"), ComboBoxType.Adjustments);
      InitCBListItem(Local.GetValue("CUI.adjustment_cat"), ComboBoxType.AdjustmentsCategories);
      InitCBListItem(Local.GetValue("CUI.product"), ComboBoxType.Products);
      InitCBListItem(Local.GetValue("CUI.product_cat"), ComboBoxType.ProductsCategories);
      InitCBListItem(Local.GetValue("CUI.client"), ComboBoxType.Clients);
      InitCBListItem(Local.GetValue("CUI.client_cat"), ComboBoxType.ClientsCategories);
      InitCBListItem(Local.GetValue("CUI.employee_cat"), ComboBoxType.EmployeesCategories);
      InitCBListItem(Local.GetValue("CUI.entity_cat"), ComboBoxType.EntitiesCategories);
      InitCBListItem(Local.GetValue("CUI.periods"), ComboBoxType.Periods);
      InitCBListItem(Local.GetValue("CUI.versions"), ComboBoxType.Versions);

      SelectionCB.DropDownList = true;
      SelectionCB.SelectedItemChanged += OnSelectionCBSelectedItemChanged;
    }

    void InitCBListItem(string p_text, ComboBoxType p_value)
    {
      ListItem l_listItem = new ListItem();
      l_listItem.Text = p_text;
      l_listItem.Value = p_value;
      SelectionCB.Items.Add(l_listItem);
    }

    void InitTV(ComboBoxType p_category, AFbiTreeView p_tv)
    {
      InitTV(p_category, p_tv, m_selectionTableLayout.Controls, false);
    }

    void InitTV(ComboBoxType p_category, AFbiTreeView p_tv, ControlCollection p_control, bool p_visible = true)
    {
      m_selectionTVList[p_category] = p_tv;
      p_tv.CheckBoxes = true;
      p_tv.TriStateMode = true;
      p_tv.Dock = DockStyle.Fill;
      p_tv.Visible = p_visible;
      p_control.Add(p_tv);
    }

    private void TreeViewInit()
    {
      InitTV(ComboBoxType.Entities, new FbiTreeView<AxisElem>(AxisElemModel.Instance.GetDictionary(AxisType.Entities)), SplitContainer.Panel1.Controls);
      InitTV(ComboBoxType.EntitiesCategories, new FbiTreeView<Filter>(FilterModel.Instance.GetDictionary(AxisType.Entities), null, false, false));
      InitTV(ComboBoxType.Clients, new FbiTreeView<AxisElem>(AxisElemModel.Instance.GetDictionary(AxisType.Client), null, false, false));
      InitTV(ComboBoxType.ClientsCategories, new FbiTreeView<Filter>(FilterModel.Instance.GetDictionary(AxisType.Client), null, false, false));
      InitTV(ComboBoxType.Products, new FbiTreeView<AxisElem>(AxisElemModel.Instance.GetDictionary(AxisType.Product), null, false, false));
      InitTV(ComboBoxType.ProductsCategories, new FbiTreeView<Filter>(FilterModel.Instance.GetDictionary(AxisType.Product), null, false, false));
      InitTV(ComboBoxType.EmployeesCategories, new FbiTreeView<Filter>(FilterModel.Instance.GetDictionary(AxisType.Employee), null, false, false));
      InitTV(ComboBoxType.Adjustments, new FbiTreeView<AxisElem>(AxisElemModel.Instance.GetDictionary(AxisType.Adjustment), null, false, false));
      InitTV(ComboBoxType.AdjustmentsCategories, new FbiTreeView<Filter>(FilterModel.Instance.GetDictionary(AxisType.Adjustment), null, false, false));
    }

    private void MultilangueSetup()
    {
      SelectionCB.Text = Local.GetValue("CUI.selection");
      m_entitySelectionLabel.Text = Local.GetValue("CUI.entities_selection");
      SelectAllToolStripMenuItem.Text = Local.GetValue("CUI.select_all");
      UnselectAllToolStripMenuItem.Text = Local.GetValue("CUI.unselect_all");
    }

    #endregion

    #region Event

    private void OnSelectionCBSelectedItemChanged(object p_sender, EventArgs p_e)
    {
      ListItem l_listItem;

      HideAllTV();
      if ((l_listItem = ((vComboBox)p_sender).SelectedItem) != null)
      {
        if (m_selectionTVList[(ComboBoxType)l_listItem.Value] != null)
        {
          AFbiTreeView l_tv = m_selectionTVList[(ComboBoxType)l_listItem.Value];
          if (l_tv.Loaded == false)
            l_tv.Load();
          l_tv.Visible = true;
        }
      }
    }

    #endregion

    #region Utils

    private void HideAllTV()
    {
      foreach (KeyValuePair<ComboBoxType, AFbiTreeView> l_tv in m_selectionTVList)
        if (l_tv.Key != ComboBoxType.Entities)
          l_tv.Value.Visible = false;
    }

    #endregion

  }
}
