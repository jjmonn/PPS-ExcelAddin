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
    SafeDictionary<Tuple<AxisType, Type>, Tuple<bool, bool, UInt32>> m_TVFormatData = new SafeDictionary<Tuple<AxisType, Type>, Tuple<bool, bool, UInt32>>();

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
      InitPeriodRangeSelection();
      TreeViewInit();
      ComboBoxInit();

      MultilangueSetup();
      HideAllTV();
    }

    private void MultilangueSetup()
    {
      SelectionCB.Text = Local.GetValue("CUI.selection");
      m_entitySelectionLabel.Text = Local.GetValue("CUI.entities_selection");
      SelectAllToolStripMenuItem.Text = Local.GetValue("CUI.select_all");
      UnselectAllToolStripMenuItem.Text = Local.GetValue("CUI.unselect_all");
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
      InitTVAxisElem(AxisElemModel.Instance, AxisType.Entities, SplitContainer.Panel1.Controls, false, 0, true, true, true);
      InitFilterTV(AxisType.Entities);
      InitTVAxisElem(AxisElemModel.Instance, AxisType.Client);
      InitFilterTV(AxisType.Client);
      InitTVAxisElem(AxisElemModel.Instance, AxisType.Product);
      InitFilterTV(AxisType.Product);
      InitTVAxisElem(AxisElemModel.Instance, AxisType.Employee);
      InitFilterTV(AxisType.Employee);
      InitTVAxisElem(AxisElemModel.Instance, AxisType.Adjustment);
      InitFilterTV(AxisType.Adjustment);
      InitTV(VersionModel.Instance, m_selectionTableLayout.Controls, true, Properties.Settings.Default.version_id, false, true);
      InitTV(CurrencyModel.Instance, m_selectionTableLayout.Controls, false, Properties.Settings.Default.currentCurrency, false, true);
    }

    private void InitPeriodRangeSelection()
    {
      PeriodRangeSelectionControl l_view = m_controller.PeriodController.View as PeriodRangeSelectionControl;
      m_selectionTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 150));
      m_selectionTableLayout.RowCount++;
      m_selectionTableLayout.Controls.Add(l_view, 0, m_selectionTableLayout.RowCount - 1);
      l_view.Dock = DockStyle.Fill;
      l_view.BackColor = Color.White;
      l_view.BorderStyle = BorderStyle.FixedSingle;
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

    void InitFilterTV(AxisType p_axis)
    {
      Tuple<AxisType, Type> l_key = new Tuple<AxisType, Type>(p_axis, typeof(Filter));
      m_selectionTVList[l_key] = new FbiFilterHierarchyTreeView(p_axis);
      BaseInitTV<Filter>(l_key, m_selectionTVList[l_key], m_selectionTableLayout.Controls, false, false, 0, true, true);
      foreach (vTreeNode l_node in m_selectionTVList[l_key].Nodes)
        l_node.ShowCheckBox = false;
    }

    void InitTVAxisElem<T>(AxedCRUDModel<T> p_model, AxisType p_axis, bool p_hideParentCB = false, UInt32 p_checkedValue = 0)
            where T : class, AxedCRUDEntity, NamedCRUDEntity
    {
      FbiTreeView<T> l_tv = InitTVAxisElem(p_model, p_axis, null, p_hideParentCB, p_checkedValue, false, false, true);
      m_selectionTableLayout.Controls.Add(l_tv, 0, 1);
    }

    FbiTreeView<T> InitTVAxisElem<T>(AxedCRUDModel<T> p_model, AxisType p_axis, ControlCollection p_control, bool p_hideParentCB = false, 
      UInt32 p_checkedValue = 0, bool p_visible = true, bool p_load = true, bool p_checkAll = false) 
      where T : class, NamedCRUDEntity, AxedCRUDEntity
    {
      FbiTreeView<T> l_tv = new FbiTreeView<T>(p_model.GetDictionary(p_axis), null, false, p_load);
      Tuple<AxisType, Type> l_key = new Tuple<AxisType, Type>(p_axis, typeof(T));

      BaseInitTV<T>(l_key, l_tv, p_control, p_visible, p_hideParentCB, p_checkedValue, p_checkAll, p_load);
      return (l_tv);
    }

    void InitTV<T>(NamedCRUDModel<T> p_model, ControlCollection p_control, bool p_hideParentCB,
      UInt32 p_checkedValue, bool p_visible = false, bool p_load = false) 
      where T : class, NamedCRUDEntity
    {
      FbiTreeView<T> l_tv = new FbiTreeView<T>(p_model.GetDictionary(), null, false, p_load);
      Tuple<AxisType, Type> l_key = new Tuple<AxisType, Type>((AxisType)0, typeof(T));

      BaseInitTV<T>(l_key, l_tv, p_control, p_visible, p_hideParentCB, p_checkedValue, false, p_load);
    }

    void BaseInitTV<T>(Tuple<AxisType, Type> p_key, AFbiTreeView p_tv, ControlCollection p_control, bool p_visible, 
      bool p_hideParentCB, UInt32 p_checkedValue, bool p_checkAll, bool p_load)
    {
      m_selectionTVList[p_key] = p_tv;
      p_tv.TriStateMode = true;
      p_tv.Visible = p_visible;
      m_TVFormatData[p_key] = new Tuple<bool, bool, uint>(p_hideParentCB, p_checkAll, p_checkedValue);
      if (p_load)
        FormatTV(p_tv, m_TVFormatData[p_key]);
      if (p_control != null)
        p_control.Add(p_tv);
    }

    void FormatTV(AFbiTreeView p_tv, Tuple<bool, bool, UInt32> p_format)
    {
      if (p_format == null)
        return;
      if (p_format.Item1 == true)
        p_tv.HideParentCheckBox();
      if (p_format.Item2 == true)
        p_tv.CheckAllParentNodes();
      else if (p_format.Item3 != 0)
        p_tv.CheckNode(p_format.Item3);
    }

    #endregion

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

    public SafeDictionary<Type, List<UInt32>> GetCheckedElements()
    {
      SafeDictionary<Type, List<UInt32>> l_checkedElements = new SafeDictionary<Type, List<uint>>();

      foreach (KeyValuePair<Tuple<AxisType, Type>, AFbiTreeView> l_tv in m_selectionTVList)
      {
        bool l_allChecked = true;

        foreach (vTreeNode l_topNode in l_tv.Value.Nodes)
          if (l_topNode.Checked != CheckState.Checked)
            l_allChecked = false;
        if (l_allChecked) // Do not add values when all elements are selected
          continue;

        foreach (vTreeNode l_node in l_tv.Value.GetNodes())
          if (l_node.Checked == CheckState.Checked)
          {
            if (l_checkedElements[l_tv.Key.Item2] == null)
              l_checkedElements[l_tv.Key.Item2] = new List<uint>();
            l_checkedElements[l_tv.Key.Item2].Add((UInt32)l_node.Value);
          }
      }
      return (l_checkedElements);
    }

    public UInt32 GetSelectedEntity()
    {
      if (m_selectionTVList[new Tuple<AxisType, Type>(AxisType.Entities, typeof(AxisElem))].SelectedNode != null)
        return ((UInt32)m_selectionTVList[new Tuple<AxisType, Type>(AxisType.Entities, typeof(AxisElem))].SelectedNode.Value);
      return (0);
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
