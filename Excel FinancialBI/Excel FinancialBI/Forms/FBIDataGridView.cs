using System;
using System.Collections.Generic;
using System.ComponentModel;
using VIBlend.WinForms.DataGridView;

namespace FBI.Forms
{
  using Utils;
  using MVC.Model.CRUD;
  using MVC.Model;

  class FBIDataGridView<T, U, V> : vDataGridView 
    where T : class, NamedCRUDEntity 
    where U : class, NamedCRUDEntity
  {
    SafeDictionary<UInt32, HierarchyItem> m_rowsDic = new SafeDictionary<UInt32, HierarchyItem>();
    SafeDictionary<UInt32, HierarchyItem> m_columnsDic = new SafeDictionary<UInt32, HierarchyItem>();

    static bool Implements<TInterface>(Type type) where TInterface : class
    {
      var interfaceType = typeof(TInterface);

      if (!interfaceType.IsInterface)
        return (false);
      return (interfaceType.IsAssignableFrom(type));
    }

    public FBIDataGridView()
    {
      this.RowsHierarchy.Clear();
      m_rowsDic = new SafeDictionary<uint, HierarchyItem>();
      this.ColumnsHierarchy.Clear();
      m_columnsDic = new SafeDictionary<uint, HierarchyItem>();
    }

    public void InitializeRows(ICRUDModel<T> p_model, MultiIndexDictionary<uint, string, T> p_dic)
    {
      if (Implements<NamedHierarchyCRUDEntity>(typeof(T)))
        foreach (T l_elem in p_dic.Values)
        {
          NamedHierarchyCRUDEntity l_hierarchyElem = l_elem as NamedHierarchyCRUDEntity;
          SetDimension(m_rowsDic, p_model, l_elem.Id, l_elem.Name, true, l_hierarchyElem.ParentId);
        }
      else
        foreach (T l_elem in p_dic.Values)
          SetDimension(m_rowsDic, p_model, l_elem.Id, l_elem.Name);
    }

    public void InitializeColumns(ICRUDModel<U> p_model, MultiIndexDictionary<uint, string, U> p_dic)
    {
      foreach (U l_elem in p_dic.Values)
        SetDimension(m_columnsDic, p_model, l_elem.Id, l_elem.Name);
    }

    HierarchyItem SetDimension<J>(SafeDictionary<UInt32, HierarchyItem> p_saveDic, ICRUDModel<J> p_model, UInt32 p_id, string p_name, bool p_hasParent = false, UInt32 p_parentId = 0) where J : class, NamedCRUDEntity
    {
      HierarchyItem l_row;

      if (p_saveDic.ContainsKey(p_id) == false)
      {
        l_row = new HierarchyItem();
        p_saveDic[p_id] = l_row;
        this.ColumnsHierarchy.Items.Add(l_row);
      }
      else
        l_row = p_saveDic[p_id];
      l_row.ItemValue = p_id;
      l_row.Caption = p_name;
      if (p_hasParent == true && p_parentId != 0 && Implements<NamedHierarchyCRUDEntity>(typeof(J)))

      {
        HierarchyItem parent = null;
        NamedHierarchyCRUDEntity parentEntity = p_model.GetValue(p_parentId) as NamedHierarchyCRUDEntity;

        if (parentEntity == null)
          return (l_row);
        if (p_saveDic.ContainsKey(p_parentId) == true)
          parent = p_saveDic[p_parentId];
        else
          parent = SetDimension(p_saveDic, p_model, parentEntity.Id, p_name, p_hasParent, parentEntity.ParentId);
        if (parent != null)
          parent.Items.Add(l_row);
      }
      return (l_row);
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
