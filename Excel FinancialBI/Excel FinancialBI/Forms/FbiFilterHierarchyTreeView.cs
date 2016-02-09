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
      FbiTreeView<Filter>.InitTVFormat(this);
    }

    public bool Load()
    {
      SafeDictionary<UInt32, vTreeNode> l_dic;

      l_dic = new SafeDictionary<UInt32, vTreeNode>();
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
          if (!this.Generate(l_dic, l_filter.Id, l_node))
            return (false);
          this.Sort(l_dic, l_node);
          this.Nodes.Add(l_node);
        }
      }
      return (true);
    }

    private bool Generate(SafeDictionary<UInt32, vTreeNode> p_dic, UInt32 p_filterRoot, vTreeNode p_root)
    {
      MultiIndexDictionary<UInt32, string, Filter> l_childrenDic = new MultiIndexDictionary<uint, string, Filter>();
      FilterModel.Instance.GetChildrenDictionary(p_filterRoot, l_childrenDic);
      Filter l_topLevel = FilterModel.Instance.GetValue(p_filterRoot);

      if (l_topLevel != null)
      {
        l_childrenDic.Set(l_topLevel.Id, l_topLevel.Name, l_topLevel);
      }
      foreach (Filter l_filter in l_childrenDic.SortedValues)
      {
        foreach (FilterValue l_filterValue in FilterValueModel.Instance.GetDictionary(l_filter.Id).SortedValues)
        {

          System.Diagnostics.Debug.WriteLine(">> FilterValue added: " + l_filterValue.Name);

          vTreeNode l_node = new vTreeNode();
          l_node.Text = l_filterValue.Name;
          l_node.Value = l_filterValue.Id;
          l_node.Tag = l_filterValue.GetType();
          p_dic[l_filterValue.Id] = l_node;
          p_root.Nodes.Add(l_node);
        }
      }
      return (true);
    }

    private bool Sort(SafeDictionary<UInt32, vTreeNode> p_dic, vTreeNode p_root)
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
          if (p_dic.ContainsKey(l_val.ParentId))
          {
            l_node = p_root.Nodes[i];
            p_root.Nodes.Remove(l_node);
            p_dic[l_val.ParentId].Nodes.Add(l_node);
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
