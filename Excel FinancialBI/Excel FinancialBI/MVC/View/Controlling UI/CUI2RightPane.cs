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
  using Network;
  using Model;
  using Properties;

  public partial class CUI2RightPane : UserControl, IView
  {
    #region Variables

    private CUIRightPaneController m_controller = null;
    SafeDictionary<UInt32, DimensionElem> m_dimensions;
    SafeDictionary<UInt32, ListItem> m_listItemDimensions;
    UInt32 m_dimensionId = 1;
    FbiTreeView<DimensionElem> m_dimensionTV;
    UInt32 m_draggingItem = 0;

    #endregion

    #region Initialize

    public CUIDimensionConf GetRowConf()
    {
      return (GetDisplayListConf(m_rowsDisplayList));
    }

    public CUIDimensionConf GetColumnConf()
    {
      return (GetDisplayListConf(m_columnsDisplayList));
    }

    CUIDimensionConf GetDisplayListConf(vListBox p_displayList)
    {
      CUIDimensionConf l_topConf = null;
      CUIDimensionConf l_currentConf = null;

      foreach (ListItem l_item in p_displayList.Items)
      {
        UInt32 l_dimensionId = (UInt32)l_item.Value;
        DimensionElem l_dimension = m_dimensions[l_dimensionId];

        if (l_dimension == null)
          continue;
        if (l_topConf == null)
          l_topConf = l_dimension.Conf;
        else
          l_currentConf.Child = l_dimension.Conf;
        l_currentConf = l_dimension.Conf;
      }
      return (l_topConf);
    }

    public CUI2RightPane()
    {
      InitializeComponent();
      m_dimensions = new SafeDictionary<uint, DimensionElem>();
      m_listItemDimensions = new SafeDictionary<uint, ListItem>();
      m_dimensionTV = new FbiTreeView<DimensionElem>(null, null, true);
      MultilangueSetup();
    }

    public void LoadView()
    {
      UInt32 l_adjustment = SetToDimensionTV(GenerateDimension(Local.GetValue("CUI.dimension.adjustment"), new AxisElemConf(AxisType.Adjustment)));
      LoadFilters(AxisType.Adjustment, l_adjustment);
      UInt32 l_client = SetToDimensionTV(GenerateDimension(Local.GetValue("CUI.dimension.client"), new AxisElemConf(AxisType.Client)));
      LoadFilters(AxisType.Client, l_client);
      UInt32 l_entity = SetToDimensionTV(GenerateDimension(Local.GetValue("CUI.dimension.entity"), new AxisElemConf(AxisType.Entities)));
      LoadFilters(AxisType.Entities, l_entity);
      SetToDimensionTV(GenerateDimension(Local.GetValue("CUI.dimension.version"), new VersionConf(Convert.ToUInt32(Settings.Default.version_id))));
      SetToRowList(GenerateDimension(Local.GetValue("CUI.dimension.account"), new CUIDimensionConf(typeof(Account)), false));

      if ((Account.AccountProcess)Settings.Default.processId == Account.AccountProcess.FINANCIAL)
      {
        SetToColumnList(GenerateDimension(Local.GetValue("CUI.dimension.year"), new PeriodConf(TimeConfig.YEARS)));
        SetToDimensionTV(GenerateDimension(Local.GetValue("CUI.dimension.year"), new PeriodConf(TimeConfig.YEARS)));
        SetToDimensionTV(GenerateDimension(Local.GetValue("CUI.dimension.month"), new PeriodConf(TimeConfig.MONTHS)));
        UInt32 l_product = SetToDimensionTV(GenerateDimension(Local.GetValue("CUI.dimension.product"), new AxisElemConf(AxisType.Product)));
        LoadFilters(AxisType.Product, l_product);
      }
      else
      {
        SetToColumnList(GenerateDimension(Local.GetValue("CUI.dimension.day"), new PeriodConf(TimeConfig.DAYS)));
        SetToDimensionTV(GenerateDimension(Local.GetValue("CUI.dimension.day"), new PeriodConf(TimeConfig.DAYS)));
        SetToDimensionTV(GenerateDimension(Local.GetValue("CUI.dimension.week"), new PeriodConf(TimeConfig.WEEK)));
        UInt32 l_employee = SetToDimensionTV(GenerateDimension(Local.GetValue("CUI.dimension.employee"), new AxisElemConf(AxisType.Employee), true, false));
        LoadFilters(AxisType.Employee, l_employee);
      }

      m_dimensionsTVPanel.Controls.Add(m_dimensionTV);
      m_dimensionTV.Dock = DockStyle.Fill;
      m_rowsDisplayList.AllowDrop = true;
      m_rowsDisplayList.AllowDragDrop = true;
      m_columnsDisplayList.AllowDrop = true;
      m_columnsDisplayList.AllowDragDrop = true;
      SuscribeEvents();
    }

    void SuscribeEvents()
    {
      m_dimensionTV.NodeMouseDown += OnDimensionTVNodeMouseDown;
      m_rowsDisplayList.ItemMouseDown += OnDisplayListItemMouseDown;
      m_columnsDisplayList.ItemMouseDown += OnDisplayListItemMouseDown;
      m_rowsDisplayList.DragOver += OnDisplayListDragOver;
      m_columnsDisplayList.DragOver += OnDisplayListDragOver;
      m_rowsDisplayList.DragDrop += OnDisplayListDropItem;
      m_columnsDisplayList.DragDrop += OnDisplayListDropItem;
      m_rowsDisplayList.KeyDown += OnDisplayListKeyDown;
      m_columnsDisplayList.KeyDown += OnDisplayListKeyDown;
    }

    void OnDisplayListKeyDown(object p_sender, KeyEventArgs p_args)
    {
      vListBox l_displayList = (vListBox)p_sender;

      if (l_displayList != null)
      {
        if (p_args.KeyCode == Keys.Delete)
          if (l_displayList.SelectedItem != null)
          {
            UInt32 l_dimensionId = (UInt32)l_displayList.SelectedItem.Value;
            DimensionElem l_dimension = (DimensionElem)m_dimensions[l_dimensionId];

            if (l_dimension != null && l_dimension.Deletable)
              l_displayList.Items.Remove(l_displayList.SelectedItem);
            else
              MessageBox.Show(Local.GetValue("CUI.not_deletable_dimension"));
          }
      }
    }

    void OnDisplayListItemMouseDown(object p_sender, ListItemMouseEventArgs p_args)
    {
      vListBox l_displayList = (vListBox)p_sender;

      if (p_args.Item != null && l_displayList != null)
      {
        DimensionElem l_dimension = (DimensionElem)m_dimensions[(UInt32)p_args.Item.Value];

        if (l_dimension != null && l_dimension.Draggable)
        {
          l_displayList.DoDragDrop(p_args.Item, DragDropEffects.Move);
          m_draggingItem = l_dimension.Id;
        }
      }
    }

    void OnDimensionTVNodeMouseDown(object p_sender, vTreeViewMouseEventArgs p_e)
    {
      if (p_e.Node != null)
      {
        DimensionElem l_dimension = (DimensionElem)m_dimensions[(UInt32)p_e.Node.Value];

        if (l_dimension != null && l_dimension.Draggable)
        {
          m_dimensionTV.DoDragDrop(p_e.Node, DragDropEffects.Move);
          m_draggingItem = l_dimension.Id;
        }
      }
    }

    void OnDisplayListDragOver(object p_sender, DragEventArgs p_e)
    {
      p_e.Effect = DragDropEffects.Move;
    }

    void OnDisplayListDropItem(object p_sender, DragEventArgs p_e)
    {
      vListBox l_displayList = (vListBox)p_sender;

      if (l_displayList != null)
        SetToDisplayList(m_draggingItem, l_displayList);
      m_draggingItem = 0;
    }

    void LoadFilters(AxisType p_type, UInt32 p_parentId)
    {
      MultiIndexDictionary<UInt32, string, Filter> l_filterDic = FilterModel.Instance.GetDictionary(p_type);

      foreach (Filter l_filter in l_filterDic.Values)
        SetToDimensionTV(GenerateDimension(l_filter.Name, new FilterConf(l_filter.Id), true, true, p_parentId));
    }

    void RemoveFromPreviousPlace(UInt32 p_dimensionId)
    {
      m_rowsDisplayList.Items.Remove(m_listItemDimensions[p_dimensionId]);
      m_columnsDisplayList.Items.Remove(m_listItemDimensions[p_dimensionId]);
      m_listItemDimensions.Remove(p_dimensionId);
    }

    UInt32 SetToDimensionTV(UInt32 p_dimensionId)
    {
      DimensionElem l_dimension = m_dimensions[p_dimensionId];

      if (l_dimension == null)
        return (p_dimensionId);
      m_dimensionTV.FindAndAdd(l_dimension);
      return (p_dimensionId);
    }

    UInt32 SetToDisplayList(UInt32 p_dimensionId, vListBox p_list)
    {
      RemoveFromPreviousPlace(m_draggingItem);
      DimensionElem l_dimension = m_dimensions[p_dimensionId];
      ListItem l_item = new ListItem();

      if (l_dimension == null)
        return (p_dimensionId);
      l_item.Text = l_dimension.Name;
      l_item.Value = l_dimension.Id;
      m_listItemDimensions[l_dimension.Id] = l_item;
      p_list.Items.Add(l_item);
      return (p_dimensionId);
    }

    UInt32 SetToRowList(UInt32 p_dimensionId)
    {
      return (SetToDisplayList(p_dimensionId, m_rowsDisplayList));
    }

    UInt32 SetToColumnList(UInt32 p_dimensionId)
    {
      return (SetToDisplayList(p_dimensionId, m_columnsDisplayList));
    }

    UInt32 GenerateDimension(string p_name, CUIDimensionConf p_conf, bool p_deletable = true, bool p_draggable = true, UInt32 p_parentId = 0)
    {
      UInt32 l_id = m_dimensionId++;

      m_dimensions[l_id] = new DimensionElem(l_id, p_name, p_conf, p_deletable, p_draggable, p_parentId);
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
