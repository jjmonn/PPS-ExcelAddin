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

    private CUILeftPaneController m_controller = null;

    SafeDictionary<Tuple<AxisType, Type>, AFbiTreeView> m_selectionTVList = new SafeDictionary<Tuple<AxisType, Type>, AFbiTreeView>();
    SafeDictionary<Tuple<AxisType, Type>, Tuple<bool, UInt32>> m_TVFormatData = new SafeDictionary<Tuple<AxisType, Type>, Tuple<bool, UInt32>>();
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
      InitCBListItem<AxisElem>(Local.GetValue("CUI.adjustment"), AxisType.Entities);
      InitCBListItem<Filter>(Local.GetValue("CUI.adjustment_cat"), AxisType.Adjustment);
      InitCBListItem<AxisElem>(Local.GetValue("CUI.product"), AxisType.Product);
      InitCBListItem<Filter>(Local.GetValue("CUI.product_cat"), AxisType.Product);
      InitCBListItem<AxisElem>(Local.GetValue("CUI.client"), AxisType.Client);
      InitCBListItem<Filter>(Local.GetValue("CUI.client_cat"), AxisType.Client);
      InitCBListItem<Filter>(Local.GetValue("CUI.employee_cat"), AxisType.Employee);
      InitCBListItem<Filter>(Local.GetValue("CUI.entity_cat"), AxisType.Entities);
      InitCBListItem<Version>(Local.GetValue("CUI.versions"));
      InitCBListItem<Currency>(Local.GetValue("CUI.currencies"));

      SelectionCB.DropDownList = true;
      SelectionCB.SelectedItemChanged += OnSelectionCBSelectedItemChanged;
    }

    private void TreeViewInit()
    {
      InitTV(AxisElemModel.Instance, AxisType.Entities, SplitContainer.Panel1.Controls);
      InitTV(FilterModel.Instance, AxisType.Entities);
      InitTV(AxisElemModel.Instance, AxisType.Client);
      InitTV(FilterModel.Instance, AxisType.Client);
      InitTV(AxisElemModel.Instance, AxisType.Product);
      InitTV(FilterModel.Instance, AxisType.Product);
      InitTV(AxisElemModel.Instance, AxisType.Employee);
      InitTV(FilterModel.Instance, AxisType.Employee);
      InitTV(AxisElemModel.Instance, AxisType.Adjustment);
      InitTV(FilterModel.Instance, AxisType.Adjustment);
      InitTV(VersionModel.Instance, m_selectionTableLayout.Controls, true, Properties.Settings.Default.version_id);
      InitTV(CurrencyModel.Instance, m_selectionTableLayout.Controls, false, Properties.Settings.Default.currentCurrency);
    }

    void InitCBListItem<T>(string p_text, AxisType p_axis = (AxisType)0)
      where T : class, NamedCRUDEntity
    {
      ListItem l_listItem = new ListItem();
      l_listItem.Text = p_text;
      l_listItem.Value = new Tuple<AxisType, Type>(p_axis, typeof(T));
      SelectionCB.Items.Add(l_listItem);
    }

    #region Initialize TV

    void InitTV<T>(AxedCRUDModel<T> p_model, AxisType p_axis, bool p_hideParentCB = false, UInt32 p_checkedValue = 0)
            where T : class, AxedCRUDEntity, NamedCRUDEntity
    {
      InitTV(p_model, p_axis, m_selectionTableLayout.Controls, p_hideParentCB, p_checkedValue, false, false);
    }

    void InitTV<T>(AxedCRUDModel<T> p_model, AxisType p_axis, ControlCollection p_control, bool p_hideParentCB = false, 
      UInt32 p_checkedValue = 0, bool p_visible = true, bool p_load = true) 
      where T : class, NamedCRUDEntity, AxedCRUDEntity
    {
      FbiTreeView<T> l_tv = new FbiTreeView<T>(p_model.GetDictionary(p_axis), null, false, p_load);

      m_selectionTVList[new Tuple<AxisType, Type>(p_axis, typeof(T))] = l_tv;
      BaseInitTV<T>(l_tv, p_control, p_visible, p_hideParentCB, p_checkedValue);
    }

    void InitTV<T>(NamedCRUDModel<T> p_model, ControlCollection p_control, bool p_hideParentCB,
      UInt32 p_checkedValue, bool p_visible = false, bool p_load = false) 
      where T : class, NamedCRUDEntity
    {
      FbiTreeView<T> l_tv = new FbiTreeView<T>(p_model.GetDictionary(), null, false, p_load);

      BaseInitTV<T>(l_tv, p_control, p_visible, p_hideParentCB, p_checkedValue);
    }

    void BaseInitTV<T>(AFbiTreeView p_tv, ControlCollection p_control, bool p_visible, bool p_hideParentCB, UInt32 p_checkedValue)
    {
      Tuple<AxisType, Type> l_key = new Tuple<AxisType, Type>((AxisType)0, typeof(T));

      m_selectionTVList[l_key] = p_tv;
      p_tv.CheckBoxes = true;
      p_tv.TriStateMode = true;
      p_tv.Dock = DockStyle.Fill;
      p_tv.Visible = p_visible;
      m_TVFormatData[l_key] = new Tuple<bool, uint>(p_hideParentCB, p_checkedValue);

      p_control.Add(p_tv);
    }

    void FormatTV(AFbiTreeView p_tv, Tuple<bool, UInt32> p_format)
    {
      if (p_format == null)
        return;
      if (p_format.Item1 == true)
        HideParentCheckBox(p_tv);
      if (p_format.Item2 != 0)
      {
        vTreeNode l_node = p_tv.FindNode(p_format.Item2);

        if (l_node != null)
          l_node.Checked = CheckState.Checked;
      }
    }

    #endregion

    void HideParentCheckBox(AFbiTreeView p_tv)
    {
      foreach (vTreeNode l_node in p_tv.GetNodes())
        if (l_node.Nodes.Count != 0)
          l_node.ShowCheckBox = false;
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
        if (m_selectionTVList[(Tuple<AxisType, Type>)l_listItem.Value] != null)
        {
          Tuple<AxisType, Type> l_key = (Tuple<AxisType, Type>)l_listItem.Value;
          bool l_test = l_key.GetHashCode() == new Tuple<AxisType, Type>( AxisType.Entities, typeof(AxisElem)).GetHashCode();

          AFbiTreeView l_tv = m_selectionTVList[l_key];
          if (l_tv.Loaded == false)
          {
            l_tv.Load();
            FormatTV(l_tv, m_TVFormatData[l_key]);
          }
          l_tv.Visible = true;
        }
      }
    }

    List<Tuple<Type, UInt32>> GetCheckedElements()
    {
      List<Tuple<Type, UInt32>> l_checkedElements = new List<Tuple<Type, UInt32>>();

      foreach (KeyValuePair<Tuple<AxisType, Type>, AFbiTreeView> l_tv in m_selectionTVList)
        foreach (vTreeNode l_node in l_tv.Value.GetNodes())
          if (l_node.Checked == CheckState.Checked)
            l_checkedElements.Add(new Tuple<Type, UInt32>(l_tv.Key.Item2, (UInt32)l_node.Value));
      return (l_checkedElements);
    }

    #endregion

    #region Utils

    private void HideAllTV()
    {
      foreach (KeyValuePair<Tuple<AxisType, Type>, AFbiTreeView> l_tv in m_selectionTVList)
        if (l_tv.Key.Item1 != AxisType.Entities || l_tv.Key.Item2 != typeof(AxisElem))
          l_tv.Value.Visible = false;
    }

    #endregion

  }
}
