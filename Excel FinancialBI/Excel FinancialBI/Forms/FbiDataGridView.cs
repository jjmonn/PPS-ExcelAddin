using System;
using System.Collections.Generic;
using System.ComponentModel;
using VIBlend.WinForms.DataGridView;
using System.Windows.Forms;
using System.Drawing;

namespace FBI.Forms
{
  using Utils;
  using MVC.Model.CRUD;
  using MVC.Model;

  public class BaseFbiDataGridView<KeyType> : vDataGridView
  {
    public enum Dimension
    {
      COLUMN,
      ROW
    };

    protected SafeDictionary<KeyType, HierarchyItem> m_rowsDic = new SafeDictionary<KeyType, HierarchyItem>();
    protected SafeDictionary<KeyType, HierarchyItem> m_columnsDic = new SafeDictionary<KeyType, HierarchyItem>();
    protected SafeDictionary<HierarchyItem, KeyType> m_hierarchyItemDic = new SafeDictionary<HierarchyItem, KeyType>();
    protected const Int32 COLUMNS_WIDTH = 150;
    public GridCell HoveredCell { get; private set; }
    protected string m_cellValue = null;
    protected bool m_validated = false;
    public event CellEventHandler CellChangedAndValidated;
    protected bool m_allowDragAndDropSet = false;
    protected HierarchyItem RowDragged { get; set; }
    protected HierarchyItem RowDropTarget { get; set; }
    public delegate void DropedEventHandler(HierarchyItem p_origin, HierarchyItem p_dest, DragEventArgs p_args);
    public event DropedEventHandler Dropped;


    public SafeDictionary<KeyType, HierarchyItem> Rows
    {
      get { return (m_rowsDic); }
    }

    public SafeDictionary<KeyType, HierarchyItem> Columns
    {
      get { return (m_columnsDic); }
    }

    public SafeDictionary<HierarchyItem, KeyType> HierarchyItems
    {
      get { return (m_hierarchyItemDic); }
    }

    public HierarchyItem HoveredColumn
    {
      get
      {
        if (HoveredCell == null)
          return (null);
        return (HoveredCell.ColumnItem);
      }
    }

    public HierarchyItem HoveredRow
    {
      get
      {
        if (HoveredCell == null)
          return (null);
        return (HoveredCell.RowItem);
      }
    }

    public void ClearRows()
    {
      m_rowsDic.Clear();
      RowsHierarchy.Clear();
    }

    public void ClearColumns()
    {
      m_columnsDic.Clear();
      ColumnsHierarchy.Clear();
    }

    static protected bool Implements<TInterface>(Type type) where TInterface : class
    {
      var interfaceType = typeof(TInterface);

      if (!interfaceType.IsInterface)
        return (false);
      return (interfaceType.IsAssignableFrom(type));
    }

    public BaseFbiDataGridView()
    {
      HoveredCell = null;
      InitDGVDisplay();
      this.RowsHierarchy.Clear();
      m_rowsDic = new SafeDictionary<KeyType, HierarchyItem>();
      if (m_columnsDic == null)
        return;
      this.ColumnsHierarchy.Clear();
      m_columnsDic = new SafeDictionary<KeyType, HierarchyItem>();
      if (m_columnsDic == null)
        return;
      CellMouseEnter += OnMouseEnterCell;
      CellMouseLeave += OnMouseLeaveCell;
      CellValidating += OnCellValidating;
      CellValueChanged += OnCellChanged;
      Resize += OnResize;
    }

    void OnResize(object sender, EventArgs e)
    {
      ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL);
    }

    void InitDGVDisplay()
    {
      Dock = System.Windows.Forms.DockStyle.Fill;
      BackColor = System.Drawing.SystemColors.Control;
      VIBlendTheme = Addin.VIBLEND_THEME;
      ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL);
      ColumnsHierarchy.AutoStretchColumns = true;
      ColumnsHierarchy.AllowResize = true;
      RowsHierarchy.CompactStyleRenderingEnabled = true;
      AllowCopyPaste = true;
      FilterDisplayMode = FilterDisplayMode.Custom;
      //RowsHierarchy.AllowDragDrop = true;
      AllowDragDropIndication = true;
    }

    public HierarchyItem HitTestRow(Point p_point)
    {
      HierarchyItem l_row = RowsHierarchy.HitTest(p_point);

      if (l_row == null)
        if (HoveredCell != null)
          return (HoveredCell.RowItem);
      return (l_row);
    }

    public HierarchyItem HitTestColumn(Point p_point)
    {
      HierarchyItem l_column = ColumnsHierarchy.HitTest(p_point);

      if (l_column == null)
        if (HoveredCell != null)
          return (HoveredCell.ColumnItem);
      return (l_column);
    }

    public bool AllowDragAndDrop
    {
      set
      {
        if (value == false)
        {
          DragOver -= OnDGVDragOver;
          DragDrop -= OnDGVDropItem;
          MouseDown -= OnDGVMouseDown;
          m_allowDragAndDropSet = false;
        }
        else if (m_allowDragAndDropSet == false)
        {
          DragOver += OnDGVDragOver;
          DragDrop += OnDGVDropItem;
          MouseDown += OnDGVMouseDown;
          m_allowDragAndDropSet = true;
        }
      }
    }

    void OnDGVDragOver(object p_sender, DragEventArgs p_args)
    {
      p_args.Effect = DragDropEffects.Move;
    }

    void OnDGVDropItem(object p_sender, DragEventArgs p_args)
    {
      RowDropTarget = GetRowAtPosition(PointToClient(new Point(p_args.X, p_args.Y)));
      if (Dropped != null)
        Dropped(RowDragged, RowDropTarget, p_args);
    }

    void OnDGVMouseDown(object p_sender, MouseEventArgs p_args)
    {
      if (p_args.Button == MouseButtons.Left)
        if (ModifierKeys.HasFlag(Keys.Control))
        {
          RowDragged = GetRowAtPosition(new Point(p_args.X, p_args.Y));
          DoDragDrop(RowDragged, DragDropEffects.All);
        }
    }

    HierarchyItem GetRowAtPosition(Point p_point)
    {
      HierarchyItem l_row = RowsHierarchy.HitTest(p_point);
      if (l_row == null)
      {
        GridCell cell = CellsArea.HitTest(p_point);

        if (cell != null)
          l_row = cell.RowItem;
      }
      return (l_row);
    }


    public void FillField<V, Q>(KeyType p_row, KeyType p_column, V p_value, Q p_editor) where Q : IEditor
    {
      HierarchyItem row = m_rowsDic[p_row];
      HierarchyItem column = m_columnsDic[p_column];

      if (row == null)
        return;
      if (column == null)
        return;
      this.CellsArea.SetCellValue(row, column, (V)p_value);
      this.CellsArea.SetCellEditor(row, column, p_editor);
    }

    void OnMouseEnterCell(object p_sender, CellEventArgs p_args)
    {
      HoveredCell = p_args.Cell;
    }

    void OnMouseLeaveCell(object p_sender, CellEventArgs p_args)
    {
      HoveredCell = null;
    }

    public void DeleteRow(KeyType p_value)
    {
      HierarchyItem l_item = m_rowsDic[p_value];

      if (l_item == null)
        return;
      if (l_item.ParentItem != null)
        l_item.ParentItem.Items.Remove(l_item);
      else
        RowsHierarchy.Items.Remove(l_item);
    }

    public void DeleteColumn(KeyType p_value)
    {
      HierarchyItem l_item = m_columnsDic[p_value];

      if (l_item == null)
        return;
      if (l_item.ParentItem != null)
        l_item.ParentItem.Items.Remove(l_item);
      else
        ColumnsHierarchy.Items.Remove(l_item);
    }

    public void FillField<V>(KeyType p_row, KeyType p_column, V p_value)
    {
      HierarchyItem column = m_columnsDic[p_column];
      HierarchyItem row = m_rowsDic[p_row];

      if (row == null || column == null)
        return;
      this.CellsArea.SetCellValue(row, column, p_value);
    }

    public object GetCellValue(KeyType p_row, KeyType p_column)
    {
      HierarchyItem column = m_columnsDic[p_column];
      HierarchyItem row = m_rowsDic[p_row];

      if (row == null || column == null)
        return (null);
      return (this.CellsArea.GetCellValue(row, column));
    }

    public object GetCellEditor(KeyType p_row, KeyType p_column)
    {
      HierarchyItem column = m_columnsDic[p_column];
      HierarchyItem row = m_rowsDic[p_row];

      if (row == null || column == null)
        return (null);
      return (this.CellsArea.GetCellEditor(row, column));
    }

    public bool HasChild(Dimension p_dim, KeyType p_key)
    {
      HierarchyItem l_item = (p_dim == Dimension.ROW) ? m_rowsDic[p_key] : m_columnsDic[p_key];

      return (l_item != null && l_item.Items.Count > 0);
    }

    void OnCellValidating(object p_sender, CellEventArgs p_args)
    {
      m_cellValue = p_args.Cell.FormattedText;
      m_validated = true;
    }

    void OnCellChanged(object p_sender, CellEventArgs p_args)
    {
      if (CellChangedAndValidated != null && m_cellValue != p_args.Cell.FormattedText && m_validated)
      {
        m_validated = false;
        CellChangedAndValidated(p_sender, p_args);
      }
      m_validated = false;
      m_cellValue = null;
    }

    public HierarchyItem SetDimension(Dimension p_dimension, HierarchyItemsCollection p_parent, KeyType p_key, string p_name)
    {
      HierarchyItem l_item;
      SafeDictionary<KeyType, HierarchyItem> l_dimensionDic = null;

      if (p_dimension == Dimension.COLUMN)
        l_dimensionDic = m_columnsDic;
      else if (p_dimension == Dimension.ROW)
        l_dimensionDic = m_rowsDic;
      if (l_dimensionDic == null)
        return (null);
      if (l_dimensionDic[p_key] != null)
        l_item = l_dimensionDic[p_key];
      else
      {
        l_item = new HierarchyItem();
        l_dimensionDic[p_key] = l_item;
      }
      m_hierarchyItemDic[l_item] = p_key;
      l_item.ItemValue = p_key;
      l_item.Caption = p_name;
      l_item.Width = COLUMNS_WIDTH;
      p_parent.Add(l_item);
      return (l_item);
    }
  }
  public class FbiDataGridView : BaseFbiDataGridView<UInt32>
  {
    public FbiDataGridView()
    {

    }

    public void InitializeRows<T>(ICRUDModel<T> p_model, MultiIndexDictionary<UInt32, string, T> p_dic) where T : class, NamedCRUDEntity
    {
      if (Implements<NamedHierarchyCRUDEntity>(typeof(T)))
        foreach (T l_elem in p_dic.Values)
        {
          NamedHierarchyCRUDEntity l_hierarchyElem = l_elem as NamedHierarchyCRUDEntity;
          SetDimension(RowsHierarchy.Items, m_rowsDic, l_elem.Id, l_elem.Name, p_model, true, l_hierarchyElem.ParentId);
        }
      else
        foreach (T l_elem in p_dic.Values)
          SetDimension(RowsHierarchy.Items, m_rowsDic, l_elem.Id, l_elem.Name, p_model);
    }

    public void InitializeColumns<U>(ICRUDModel<U> p_model, MultiIndexDictionary<UInt32, string, U> p_dic) where U : class, NamedCRUDEntity
    {
      foreach (U l_elem in p_dic.Values)
        SetDimension(ColumnsHierarchy.Items, m_columnsDic, l_elem.Id, l_elem.Name, p_model);
    }

    public HierarchyItem SetDimension(Dimension p_dimension, UInt32 p_id, string p_name)
    {
      return SetDimension<NamedCRUDEntity>(p_dimension, p_id, p_name);
    }

    public HierarchyItem SetDimension<J>(Dimension p_dimension, UInt32 p_id, string p_name, UInt32 p_parentId = 0, ICRUDModel<J> p_model = null, int p_width = COLUMNS_WIDTH) where J : class, NamedCRUDEntity
    {
      List<Tuple<UInt32, UInt32, object>> cellValues = new List<Tuple<UInt32, UInt32, object>>();
      HierarchyItem l_item = null;

      if (p_dimension == Dimension.COLUMN)
      {
        if (m_columnsDic[p_id] != null)
          foreach (UInt32 l_row in Rows.Keys)
            cellValues.Add(new Tuple<UInt32, UInt32, object>(l_row, p_id, GetCellValue(l_row, p_id)));
        l_item = SetDimension<J>(ColumnsHierarchy.Items, m_columnsDic, p_id, p_name, p_model, p_parentId != 0, p_parentId, p_width);
      }
      else if (p_dimension == Dimension.ROW)
      {
        if (m_rowsDic[p_id] != null)
          foreach (UInt32 l_column in Columns.Keys)
            cellValues.Add(new Tuple<UInt32, UInt32, object>(p_id, l_column, GetCellValue(p_id, l_column)));
        l_item = SetDimension<J>(RowsHierarchy.Items, m_rowsDic, p_id, p_name, p_model, p_parentId != 0, p_parentId, p_width);
      }
      foreach (Tuple<UInt32, UInt32, object> l_cell in cellValues)
        FillField(l_cell.Item1, l_cell.Item2, l_cell.Item3);
      return (l_item);
    }

    HierarchyItem SetDimension<J>(HierarchyItemsCollection p_dimension, SafeDictionary<UInt32, HierarchyItem> p_saveDic, UInt32 p_id,
      string p_name, ICRUDModel<J> p_model = null, bool p_hasParent = false, UInt32 p_parentId = 0, int p_width = COLUMNS_WIDTH) where J : class, NamedCRUDEntity
    {
      HierarchyItem l_dim;

      if (p_saveDic.ContainsKey(p_id) == false)
      {
        l_dim = new HierarchyItem();
        if (l_dim == null)
          return (null);
        p_saveDic[p_id] = l_dim;
      }
      else
      {
        l_dim = p_saveDic[p_id];
        if (l_dim.ParentItem != null)
          l_dim.ParentItem.Items.Remove(l_dim);
        else
          p_dimension.Remove(l_dim);
      }
      if (p_hasParent == true && p_parentId != 0 && Implements<NamedHierarchyCRUDEntity>(typeof(J)) && p_model != null)
      {
        HierarchyItem l_parent = null;
        NamedHierarchyCRUDEntity parentEntity = p_model.GetValue(p_parentId) as NamedHierarchyCRUDEntity;

        if (parentEntity == null)
          return (l_dim);
        if (p_saveDic.ContainsKey(p_parentId) == true)
          l_parent = p_saveDic[p_parentId];
        else
          l_parent = SetDimension(p_dimension, p_saveDic, parentEntity.Id, p_name, p_model, p_hasParent, parentEntity.ParentId);
        if (l_parent != null)
          l_parent.Items.Add(l_dim);
      }
      else
        p_dimension.Add(l_dim);
      l_dim.ItemValue = p_id;
      l_dim.Caption = p_name;
      l_dim.Width = p_width;

      m_hierarchyItemDic.Remove(l_dim);
      m_hierarchyItemDic[l_dim] = p_id;
      return (l_dim);
    }
  }
}
