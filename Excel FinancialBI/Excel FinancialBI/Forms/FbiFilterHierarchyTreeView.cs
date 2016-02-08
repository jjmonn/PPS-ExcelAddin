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
      SafeDictionary<UInt32, vTreeNode> l_dic;

      l_dic = new SafeDictionary<uint, vTreeNode>();
      if (m_filters == null)
        return (false);
      foreach (Filter l_filter in m_filters.SortedValues)
      {
        if (l_filter.ParentId == 0)
        {
          vTreeNode l_node = new vTreeNode();
          l_node.Text = l_filter.Name;
          l_node.Value = l_filter.Id;
          if (!this.Generate(l_dic, l_node))
            return (false);
          this.Sort(l_dic, l_node);
          this.Nodes.Add(l_node);
        }
      }
      return (true);
    }

    private bool Generate(SafeDictionary<UInt32, vTreeNode> p_dic, vTreeNode p_root)
    {
      foreach (Filter l_filter in m_filters.SortedValues)
      {
        foreach (FilterValue l_filterValue in FilterValueModel.Instance.GetDictionary(l_filter.Id).SortedValues)
        {
          vTreeNode l_node = new vTreeNode();
          l_node.Text = l_filterValue.Name;
          l_node.Value = l_filterValue.Id;
          Debug.WriteLine(l_filterValue.Name + " " + l_filterValue.Id);
          p_dic[l_filterValue.Id] = l_node;
          p_root.Nodes.Add(l_node);
        }
      }
      return (true);
    }

    private bool Sort(SafeDictionary<UInt32, vTreeNode> p_dic, vTreeNode p_root)
    {
      FilterValue l_val;
      vTreeNode l_node, l_tmpNode;

      for (int i = 0; i < p_root.Nodes.Count; ++i)
      {
        if ((l_val = FilterValueModel.Instance.GetValue((UInt32)p_root.Nodes[i].Value)) == null)
        {
          p_root.Nodes[i].Remove();
          continue;
        }
        if (l_val.ParentId != 0 && (l_tmpNode = p_dic[l_val.ParentId]) != null)
        {
          l_node = p_root.Nodes[i];
          p_root.Nodes.Remove(l_node);
          l_tmpNode.Nodes.Add(l_node);
          i--;
        }
      }
      return (true);
    }
  }
}
