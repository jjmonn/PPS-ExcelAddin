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
    private AxisType m_axisType;
    private MultiIndexDictionary<UInt32, string, Filter> m_filters;

    public FbiFilterHierarchyTreeView(AxisType p_axisType)
    {
      m_axisType = p_axisType;
      m_filters = FilterModel.Instance.GetDictionary(m_axisType);
      this.Load();
    }

    private bool Load()
    {
      SortedDictionary<string, vTreeNode> l_dic;

      l_dic = new SortedDictionary<string, vTreeNode>();
      if (m_filters == null)
        return (false);
      foreach (Filter l_filter in m_filters.SortedValues)
      {
        if (l_filter.ParentId == 0)
        {
          vTreeNode l_node = new vTreeNode();
          l_node.Text = l_filter.Name;
          l_node.Value = l_filter.Id;
          l_node.Tag = l_filter.GetType();
          if (!this.Generate(l_dic, l_node))
            return (false);
          this.Sort(l_dic, l_node);
          this.Nodes.Add(l_node);
        }
      }
      return (true);
    }

    private bool Generate(SortedDictionary<string, vTreeNode> p_dic, vTreeNode p_root)
    {
      foreach (Filter l_filter in m_filters.SortedValues)
      {
        foreach (FilterValue l_filterValue in FilterValueModel.Instance.GetDictionary(l_filter.Id).SortedValues)
        {
          vTreeNode l_node = new vTreeNode();
          l_node.Text = l_filterValue.Name;
          l_node.Value = l_filterValue.Id;
          l_node.Tag = l_filterValue.GetType();
          if (p_dic.ContainsKey(l_filterValue.Name.ToUpper()))
            p_dic[l_filterValue.Name.ToUpper()] = l_node;
          else
            p_dic.Add(l_filterValue.Name.ToUpper(), l_node);
          p_root.Nodes.Add(l_node);
        }
      }
      return (true);
    }

    private bool Sort(SortedDictionary<string, vTreeNode> p_dic, vTreeNode p_root)
    {
      vTreeNode l_node;
      FilterValue l_val;

      for (int i = 0; i < p_root.Nodes.Count; ++i)
      {
        if ((l_val = FilterValueModel.Instance.GetValue((UInt32)p_root.Nodes[i].Value)) == null)
        {
          p_root.Nodes[i].Remove();
          continue;
        }
        if (l_val.ParentId != 0)
        {
          FilterValue parent = FilterValueModel.Instance.GetValue(l_val.ParentId);

          if (parent != null && p_dic.ContainsKey(parent.Name.ToUpper()))
          {
            l_node = p_root.Nodes[i];
            p_root.Nodes.Remove(l_node);
            p_dic[parent.Name.ToUpper()].Nodes.Add(l_node);
            i--;
          }
        }
      }
      return (true);
    }


    public vTreeNode Get(UInt32 p_nodeId, Type p_type)
    {
      foreach (vTreeNode l_node in this.GetNodes())
      {
        if (p_type == (Type)l_node.Tag && p_nodeId == (UInt32)l_node.Value)
          return (l_node);
      }
      return (null);
    }
  }
}
