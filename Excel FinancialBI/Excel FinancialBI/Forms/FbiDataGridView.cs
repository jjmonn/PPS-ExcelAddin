using System;
using System.Collections.Generic;
using System.ComponentModel;
using VIBlend.WinForms.DataGridView;

namespace FBI.Forms
{
  using Utils;
  using MVC.Model.CRUD;
  using MVC.Model;

  class FbiDataGridView<T, U, V> : vDataGridView 
    where T : class, NamedCRUDEntity 
    where U : class, NamedCRUDEntity
  {
    SafeDictionary<UInt32, HierarchyItem> m_rowsDic = new SafeDictionary<UInt32, HierarchyItem>();
    SafeDictionary<UInt32, HierarchyItem> m_columnsDic = new SafeDictionary<UInt32, HierarchyItem>();
    const Int32 COLUMNS_WIDTH = 150;

    static bool Implements<TInterface>(Type type) where TInterface : class
    {
      var interfaceType = typeof(TInterface);

      if (!interfaceType.IsInterface)
        return (false);
      return (interfaceType.IsAssignableFrom(type));
    }

    public FbiDataGridView()
    {
      InitDGVDisplay();
      this.RowsHierarchy.Clear();
      m_rowsDic = new SafeDictionary<uint, HierarchyItem>();
      if (m_columnsDic == null)
        return;
      this.ColumnsHierarchy.Clear();
      m_columnsDic = new SafeDictionary<uint, HierarchyItem>();
      if (m_columnsDic == null)
        return;
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
      RowsHierarchy.AllowDragDrop = true;
      AllowDragDropIndication = true;
    }

    public void InitializeRows(ICRUDModel<T> p_model, MultiIndexDictionary<uint, string, T> p_dic)
    {
      if (Implements<NamedHierarchyCRUDEntity>(typeof(T)))
        foreach (T l_elem in p_dic.Values)
        {
          NamedHierarchyCRUDEntity l_hierarchyElem = l_elem as NamedHierarchyCRUDEntity;
          SetDimension(RowsHierarchy.Items, m_rowsDic, p_model, l_elem.Id, l_elem.Name, true, l_hierarchyElem.ParentId);
        }
      else
        foreach (T l_elem in p_dic.Values)
          SetDimension(RowsHierarchy.Items, m_rowsDic, p_model, l_elem.Id, l_elem.Name);
    }

    public void InitializeColumns(ICRUDModel<U> p_model, MultiIndexDictionary<uint, string, U> p_dic)
    {
      foreach (U l_elem in p_dic.Values)
        SetDimension(ColumnsHierarchy.Items, m_columnsDic, p_model, l_elem.Id, l_elem.Name);
    }

    HierarchyItem SetDimension<J>(HierarchyItemsCollection p_dimension, SafeDictionary<UInt32, HierarchyItem> p_saveDic, ICRUDModel<J> p_model, UInt32 p_id,
      string p_name, bool p_hasParent = false, UInt32 p_parentId = 0) where J : class, NamedCRUDEntity
    {
      HierarchyItem l_dim;

      if (p_saveDic.ContainsKey(p_id) == false)
      {
        l_dim = new HierarchyItem();
        if (l_dim == null)
          return (null);
        p_saveDic[p_id] = l_dim;
        if (p_parentId == 0)
          p_dimension.Add(l_dim);
      }
      else
        l_dim = p_saveDic[p_id];
      l_dim.ItemValue = p_id;
      l_dim.Caption = p_name;
      l_dim.Width = COLUMNS_WIDTH;
      if (p_hasParent == true && p_parentId != 0 && Implements<NamedHierarchyCRUDEntity>(typeof(J)))
      {
        HierarchyItem parent = null;
        NamedHierarchyCRUDEntity parentEntity = p_model.GetValue(p_parentId) as NamedHierarchyCRUDEntity;

        if (parentEntity == null)
          return (l_dim);
        if (p_saveDic.ContainsKey(p_parentId) == true)
          parent = p_saveDic[p_parentId];
        else
          parent = SetDimension(p_dimension, p_saveDic, p_model, parentEntity.Id, p_name, p_hasParent, parentEntity.ParentId);
        if (parent != null)
          parent.Items.Add(l_dim);
      }
      return (l_dim);
    }

    public void FillField<Q>(UInt32 p_row, UInt32 p_column, V p_value, Q p_editor) where Q : IEditor
    {
      HierarchyItem row = m_rowsDic[p_row];
      HierarchyItem column = m_columnsDic[p_column];

      if (row == null)
        return;
      if (column == null)
        return;
      this.CellsArea.SetCellValue(row, column, p_value);
      this.CellsArea.SetCellEditor(row, column, p_editor);
    }

    public void FillField(HierarchyItem p_row, UInt32 p_column, V p_value)
    {
      HierarchyItem column = m_columnsDic[p_column];

      if (p_row == null)
        return;
      if (column == null)
        return;
      this.CellsArea.SetCellValue(p_row, column, p_value);
    }
  }
}
