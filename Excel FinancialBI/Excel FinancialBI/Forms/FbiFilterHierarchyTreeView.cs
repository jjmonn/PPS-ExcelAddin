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

  class FbiFilterHierarchyTreeView : AFbiTreeView
  {
    private AxisType m_axisType;
    public override bool Loaded { get; protected set; }
    private MultiIndexDictionary<UInt32, string, Filter> m_filters;

    public FbiFilterHierarchyTreeView(AxisType p_axisType, bool p_allowDragDrop = false) : base(p_allowDragDrop)
    {
      Loaded = false;
      m_axisType = p_axisType;
      m_filters = FilterModel.Instance.GetDictionary(m_axisType);
      this.Load();
    }

    public static bool Load(vTreeNodeCollection p_nodes, MultiIndexDictionary<UInt32, string, Filter> p_dic)
    {
      SafeDictionary<UInt32, vTreeNode> l_dic;

      l_dic = new SafeDictionary<UInt32, vTreeNode>();
      if (p_dic == null)
        return (false);
      foreach (Filter l_filter in p_dic.SortedValues)
      {
        if (l_filter.ParentId == 0)
        {
          vTreeNode l_node = new vTreeNode();
          l_node.Text = l_filter.Name;
          l_node.Value = l_filter.Id;
          l_node.Tag = l_filter.GetType();
          if (!Generate(l_dic, l_filter.Id, l_node))
            return (false);
          Sort(l_dic, l_node);
          p_nodes.Add(l_node);
        }
      }
      return (true);
    }

    public override bool Load()
    {
      Loaded = Load(Nodes, m_filters);
      return (Loaded);
    }

    public void GetAndAdd(NamedHierarchyCRUDEntity p_value, Type p_type)
    {
      vTreeNode l_newNode = this.Get(p_value.Id, p_type);
      vTreeNode l_parentNode = this.Get(p_value.ParentId, p_type);

      if (l_newNode == null)
      {
        l_newNode = new vTreeNode();
        l_newNode.Value = p_value.Id;
        if (l_parentNode != null)
          l_parentNode.Nodes.Add(l_newNode);
        else
          Nodes.Add(l_newNode);
      }
      l_newNode.Text = p_value.Name;
      l_newNode.ImageIndex = (int)p_value.Image;
    }

    private static bool Generate(SafeDictionary<UInt32, vTreeNode> p_dic, UInt32 p_filterRoot, vTreeNode p_root)
    {
      MultiIndexDictionary<UInt32, string, Filter> l_childrenDic = new MultiIndexDictionary<UInt32, string, Filter>();
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

    private static bool Sort(SafeDictionary<UInt32, vTreeNode> p_dic, vTreeNode p_root)
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
