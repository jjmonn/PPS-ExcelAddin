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
    int m_SPDistance = 250;
    bool m_filterPaneOpen = true;

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

    public void LoadView()
    {
      if ((Account.AccountProcess)Properties.Settings.Default.processId == Account.AccountProcess.RH)
        InitPeriodRangeSelection();
      MultilangueSetup();
      TreeViewInit();
      ComboBoxInit();

      SuscribeEvents();
    }

    void SuscribeEvents()
    {
      m_selectionTVList[new Tuple<AxisType, Type>((AxisType)0, typeof(Version))].NodeChecked += OnVersionSelect;
      SelectionCB.SelectedItemChanged += OnSelectionCBSelectedItemChanged;
      UnselectAllToolStripMenuItem.Click += OnUnSelectAllButtonClick;
      SelectAllToolStripMenuItem.Click += OnSelectAllButtonClick;
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
      InitCBListItem<AxisElem>(Local.GetValue("CUI.dimension.adjustment"), AxisType.Entities);
      InitCBListItem<Filter>(Local.GetValue("CUI.dimension.adjustment_cat"), AxisType.Adjustment);
      InitCBListItem<AxisElem>(Local.GetValue("CUI.dimension.product"), AxisType.Product);
      InitCBListItem<Filter>(Local.GetValue("CUI.dimension.product_cat"), AxisType.Product);
      InitCBListItem<AxisElem>(Local.GetValue("CUI.dimension.client"), AxisType.Client);
      InitCBListItem<Filter>(Local.GetValue("CUI.dimension.client_cat"), AxisType.Client);
      InitCBListItem<Filter>(Local.GetValue("CUI.dimension.employee_cat"), AxisType.Employee);
      InitCBListItem<Filter>(Local.GetValue("CUI.dimension.entity_cat"), AxisType.Entities);
      SelectionCB.SelectedItem = InitCBListItem<Version>(Local.GetValue("CUI.dimension.versions"));
      InitCBListItem<Currency>(Local.GetValue("CUI.dimension.currencies"));
       
      SelectionCB.DropDownList = true;
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
      InitTV(VersionModel.Instance, m_selectionTableLayout.Controls, true, Properties.Settings.Default.version_id, true, true);
      FbiTreeView<Currency> l_currencyTV = new FbiTreeView<Currency>(CurrencyModel.Instance.GetUsedCurrenciesDic(), null, false, true);
 
      Tuple<AxisType, Type> l_key = new Tuple<AxisType, Type>(AxisType.Entities, typeof(AxisElem));
      m_selectionTVList[l_key].ImageList = EntitiesTVImageList;

      BaseInitTV<Currency>(l_key, l_currencyTV, m_selectionTableLayout.Controls, false, false, Properties.Settings.Default.currentCurrency, false, true);
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

    ListItem InitCBListItem<T>(string p_text, AxisType p_axis = (AxisType)0)
      where T : class, NamedCRUDEntity
    {
      ListItem l_listItem = new ListItem();
      l_listItem.Text = p_text;
      l_listItem.Value = new Tuple<AxisType, Type>(p_axis, typeof(T));
      SelectionCB.Items.Add(l_listItem);
      return (l_listItem);
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
      p_tv.ContextMenuStrip = m_rightClickMenu;
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

    AFbiTreeView GetTVFromToolStripMenuItem(object p_sender)
    {
      if (p_sender.GetType() != typeof(ToolStripMenuItem))
        return null;
      ToolStripMenuItem l_item = p_sender as ToolStripMenuItem;
      ContextMenuStrip l_menu = l_item.Owner as ContextMenuStrip;
      if (l_menu.SourceControl.GetType().IsSubclassOf(typeof(AFbiTreeView)) == false)
        return null;
      AFbiTreeView l_tv = l_menu.SourceControl as AFbiTreeView;
      return (l_tv);
    }

    private void OnFilterPanelCollapseClick(object p_sender, EventArgs p_e)
    {
      if (p_sender.GetType() == typeof(vButton))
      {
        vButton l_button = p_sender as vButton;
        l_button.ImageIndex = (m_filterPaneOpen) ? 0 : 1;
      }
      if (m_filterPaneOpen)
      {
        m_SPDistance = SplitContainer.SplitterDistance;
        SplitContainer.SplitterDistance = SplitContainer.Height;
      }
      else
        SplitContainer.SplitterDistance = m_SPDistance;
      m_filterPaneOpen = !m_filterPaneOpen;
    }

    void OnSelectAllButtonClick(object p_sender, EventArgs p_e)
    {
      AFbiTreeView l_tv = GetTVFromToolStripMenuItem(p_sender);

      if (l_tv != null)
        foreach (vTreeNode l_node in l_tv.GetNodes())
          l_node.Checked = CheckState.Checked;
    }

    void OnUnSelectAllButtonClick(object p_sender, EventArgs p_e)
    {
      AFbiTreeView l_tv = GetTVFromToolStripMenuItem(p_sender);

      if (l_tv != null)
        foreach (vTreeNode l_node in l_tv.GetNodes())
          l_node.Checked = CheckState.Unchecked;
    }

    void OnVersionSelect(object p_sender, vTreeViewEventArgs p_e)
    {
      Int32 l_minStartPeriod = -1;
      Int32 l_maxLastPeriod = 0;
      SafeDictionary<Type, List<UInt32>> l_dic = GetCheckedElements();

      if (l_dic[typeof(Version)] == null)
        return;
      foreach (UInt32 l_versionId in l_dic[typeof(Version)])
      {
        Version l_version = VersionModel.Instance.GetValue(l_versionId);
        Int32 l_lastPeriod;
        if (l_version == null)
          continue;

        l_lastPeriod = PeriodModel.GetLastPeriod((Int32)l_version.StartPeriod, (Int32)l_version.NbPeriod, l_version.TimeConfiguration);
        if (l_version.StartPeriod < l_minStartPeriod || l_minStartPeriod == -1)
          l_minStartPeriod = (Int32)l_version.StartPeriod;
        if (l_maxLastPeriod < l_lastPeriod)
          l_maxLastPeriod = l_lastPeriod;
      }
      m_controller.PeriodController.MinDate = DateTime.FromOADate(l_minStartPeriod);
      m_controller.PeriodController.MaxDate = DateTime.FromOADate(l_maxLastPeriod);
    }

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
