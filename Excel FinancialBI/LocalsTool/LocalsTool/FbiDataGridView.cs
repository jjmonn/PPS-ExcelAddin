using System;
using System.Collections.Generic;
using System.ComponentModel;
using VIBlend.WinForms.DataGridView;
using System.Windows.Forms;
using System.Drawing;

namespace FBI.Forms
{
  public class BaseFbiDataGridView<KeyType> : vDataGridView
  {
    public enum Dimension
    {
      COLUMN,
      ROW
    };

    protected SafeDictionary<KeyType, HierarchyItem> m_rowsDic = new SafeDictionary<KeyType, HierarchyItem>();
    protected SafeDictionary<KeyType, HierarchyItem> m_columnsDic = new SafeDictionary<KeyType, HierarchyItem>();
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
    }


    void InitDGVDisplay()
    {
      Dock = System.Windows.Forms.DockStyle.Fill;
      BackColor = System.Drawing.SystemColors.Control;
      ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL);
      ColumnsHierarchy.AutoStretchColumns = true;
      ColumnsHierarchy.AllowResize = true;
      RowsHierarchy.CompactStyleRenderingEnabled = true;
      AllowCopyPaste = true;
      FilterDisplayMode = FilterDisplayMode.Custom;
      AllowDragDropIndication = true;
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

    void OnCellValidating(object p_sender, CellEventArgs p_args)
    {
      m_cellValue = p_args.Cell.FormattedText;
      m_validated = true;
    }

    void OnCellChanged(object p_sender, CellEventArgs p_args)
    {
      if (CellChangedAndValidated != null && m_cellValue != p_args.Cell.FormattedText && m_validated)
        CellChangedAndValidated(p_sender, p_args);
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
      l_item.ItemValue = p_key;
      l_item.Caption = p_name;
      l_item.Width = COLUMNS_WIDTH;
      p_parent.Add(l_item);
      return (l_item);
    }
  }
}
