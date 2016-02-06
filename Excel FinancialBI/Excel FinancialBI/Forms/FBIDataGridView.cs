using System;
using System.Collections.Generic;
using System.ComponentModel;
using VIBlend.WinForms.DataGridView;

namespace FBI.Forms
{
  using Utils;
  using MVC.Model.CRUD;
  using MVC.Model;

  public class FbiDataGridView<V> : vDataGridView 
  {
    public enum Dimension
    {
      COLUMN,
      ROW
    };
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
      ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL);
      ColumnsHierarchy.AutoStretchColumns = true;
      ColumnsHierarchy.AllowResize = true;
      RowsHierarchy.CompactStyleRenderingEnabled = true;
      AllowCopyPaste = true;
      FilterDisplayMode = FilterDisplayMode.Custom;
      RowsHierarchy.AllowDragDrop = true;
      AllowDragDropIndication = true;
    }

    public void InitializeRows<T>(ICRUDModel<T> p_model, MultiIndexDictionary<uint, string, T> p_dic) where T : class, NamedCRUDEntity 
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

    public void InitializeColumns<U>(ICRUDModel<U> p_model, MultiIndexDictionary<uint, string, U> p_dic) where U : class, NamedCRUDEntity
    {
      foreach (U l_elem in p_dic.Values)
        SetDimension(ColumnsHierarchy.Items, m_columnsDic, l_elem.Id, l_elem.Name, p_model);
    }

    public void SetDimension(Dimension p_dimension, UInt32 p_id, string p_name)
    {
      if (p_dimension == Dimension.COLUMN)
        SetDimension<NamedCRUDEntity>(ColumnsHierarchy.Items, m_columnsDic, p_id, p_name);
      else if (p_dimension == Dimension.ROW)
        SetDimension<NamedCRUDEntity>(RowsHierarchy.Items, m_rowsDic, p_id, p_name);
    }

    HierarchyItem SetDimension<J>(HierarchyItemsCollection p_dimension, SafeDictionary<UInt32, HierarchyItem> p_saveDic, UInt32 p_id,
      string p_name, ICRUDModel<J> p_model = null, bool p_hasParent = false, UInt32 p_parentId = 0) where J : class, NamedCRUDEntity
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
  }
}
