using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIBlend.WinForms.Controls;
using System.Diagnostics;

namespace FBI.Forms
{
  using Utils;
  using MVC.Model;
  using MVC.Model.CRUD;

  class FbiFilterHierarchyTreeView : vTreeView
  {
    AxisType m_axisType;
    MultiIndexDictionary<UInt32, string, Filter> m_filters;

    public FbiFilterHierarchyTreeView(AxisType p_axisType)
    {
      m_axisType = p_axisType;
      m_filters = FilterModel.Instance.GetDictionary(m_axisType);
      this.Load();
    }

    private bool Load()
    {
      if (m_filters == null)
        return (false);
      foreach (Filter l_filter in m_filters.SortedValues)
      {
        if (l_filter.ParentId == 0)
        {
          vTreeNode l_node = new vTreeNode();
          l_node.Text = l_filter.Name;
          l_node.Value = l_filter.Id;
          //
          Debug.WriteLine("Root: " + l_filter.Name + " " + l_filter.Id);
          //
          if (!this.Generate(l_node, 0, l_filter.Id, l_node))
            return (false);
          this.Nodes.Add(l_node);
        }
      }
      return (true);
    }

    private bool Generate(vTreeNode p_node, UInt32 p_parentId, UInt32 p_filterId, vTreeNode p_root)
    {
      foreach (FilterValue l_filterValue in FilterValueModel.Instance.GetDictionary(p_filterId).SortedValues)
      {
        //
        Debug.WriteLine("Value: " + l_filterValue.Name + " " + l_filterValue.Id + " " + l_filterValue.ParentId + " " + l_filterValue.FilterId);
        //
        if (l_filterValue.ParentId == 0)
        {
          vTreeNode l_node = new vTreeNode();
          l_node.Text = l_filterValue.Name;
          l_node.Value = l_filterValue.Id;
          //if (!this.Generate(l_node, l_filterValue.Id, l_filterValue.FilterId, p_root))
          //return (false);
          p_root.Nodes.Add(l_node);
        }
        if (l_filterValue.ParentId == p_parentId)
        {
          vTreeNode l_node = new vTreeNode();
          l_node.Text = l_filterValue.Name;
          l_node.Value = l_filterValue.Id;
          //if (!this.Generate(l_node, l_filterValue.Id, l_filterValue.Id, p_root))
          //return (false);
          p_node.Nodes.Add(l_node);
        }
      }
      return (true);
    }
  }
}
