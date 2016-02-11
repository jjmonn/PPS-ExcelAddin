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

  public class FbiDataGridView : vDataGridView 
  {
    public enum Dimension
    {
      COLUMN,
      ROW
    };

    SafeDictionary<UInt32, HierarchyItem> m_rowsDic = new SafeDictionary<UInt32, HierarchyItem>();
    SafeDictionary<UInt32, HierarchyItem> m_columnsDic = new SafeDictionary<UInt32, HierarchyItem>();
    const Int32 COLUMNS_WIDTH = 150;
    public GridCell HoveredCell { get; private set; }
    string m_cellValue = null;
    bool m_validated = false;
    public event CellEventHandler CellChangedAndValidated;
    bool m_allowDragAndDropSet = false;
    HierarchyItem RowDragged { get; set; }
    HierarchyItem RowDropTarget { get; set; }
    public delegate void DropedEventHandler(HierarchyItem p_origin, HierarchyItem p_dest, DragEventArgs p_args);
    public event DropedEventHandler Dropped;

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

    static bool Implements<TInterface>(Type type) where TInterface : class
    {
      var interfaceType = typeof(TInterface);

      if (!interfaceType.IsInterface)
        return (false);
      return (interfaceType.IsAssignableFrom(type));
    }

    public FbiDataGridView()
    {
      HoveredCell = null;
      InitDGVDisplay();
      this.RowsHierarchy.Clear();
      m_rowsDic = new SafeDictionary<UInt32, HierarchyItem>();
      if (m_columnsDic == null)
        return;
      this.ColumnsHierarchy.Clear();
      m_columnsDic = new SafeDictionary<UInt32, HierarchyItem>();
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

    public void SetDimension(Dimension p_dimension, UInt32 p_id, string p_name)
    {
      SetDimension<NamedCRUDEntity>(p_dimension, p_id, p_name);
    }

    public void SetDimension<J>(Dimension p_dimension, UInt32 p_id, string p_name, UInt32 p_parentId = 0, ICRUDModel<J> p_model = null, int p_width = COLUMNS_WIDTH) where J : class, NamedCRUDEntity
    {
      if (p_dimension == Dimension.COLUMN)
        SetDimension<J>(ColumnsHierarchy.Items, m_columnsDic, p_id, p_name, p_model, p_parentId != 0, p_parentId, p_width);
      else if (p_dimension == Dimension.ROW)
        SetDimension<J>(RowsHierarchy.Items, m_rowsDic, p_id, p_name, p_model, p_parentId != 0, p_parentId, p_width);
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
        HierarchyItem parent = null;
        NamedHierarchyCRUDEntity parentEntity = p_model.GetValue(p_parentId) as NamedHierarchyCRUDEntity;

        if (parentEntity == null)
          return (l_dim);
        if (p_saveDic.ContainsKey(p_parentId) == true)
          parent = p_saveDic[p_parentId];
        else
          parent = SetDimension(p_dimension, p_saveDic, parentEntity.Id, p_name, p_model, p_hasParent, parentEntity.ParentId);
        if (parent != null)
          parent.Items.Add(l_dim);
      }
      else
        p_dimension.Add(l_dim);
      l_dim.ItemValue = p_id;
      l_dim.Caption = p_name;
      l_dim.Width = p_width;
      return (l_dim);
    }

    public void FillField<V, Q>(UInt32 p_row, UInt32 p_column, V p_value, Q p_editor) where Q : IEditor
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

    public void DeleteRow(UInt32 p_value)
    {
      HierarchyItem l_item = m_rowsDic[p_value];

      if (l_item == null)
        return;
      if (l_item.ParentItem != null)
        l_item.ParentItem.Items.Remove(l_item);
      else
        RowsHierarchy.Items.Remove(l_item);
    }

    public void DeleteColumn(UInt32 p_value)
    {
      HierarchyItem l_item = m_columnsDic[p_value];

      if (l_item == null)
        return;
      if (l_item.ParentItem != null)
        l_item.ParentItem.Items.Remove(l_item);
      else
        ColumnsHierarchy.Items.Remove(l_item);
    }

    public void FillField<V>(UInt32 p_row, UInt32 p_column, V p_value)
    {
      HierarchyItem column = m_columnsDic[p_column];
      HierarchyItem row = m_rowsDic[p_row];

      if (row == null)
        return;
      if (column == null)
        return;
      this.CellsArea.SetCellValue(row, column, p_value);
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
  }
}
