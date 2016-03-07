using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using VIBlend;
using VIBlend.WinForms.Controls;
using System.Windows.Forms;

namespace FBI.Forms
{
  using Utils;
  using MVC.Model.CRUD;

  public class FbiTreeView<T> : AFbiTreeView where T : NamedCRUDEntity
  {
    private MultiIndexDictionary<UInt32, String, T> m_items;
    MultiIndexDictionary<UInt32, String, T> m_icons;
    public override bool Loaded { get; protected set; }
    private static readonly string ERR_GENERATE = "[FbiTreeView] Cannot generate vTreeView. Either the MultiIndexDictionary is null or incorrect";

    public FbiTreeView(MultiIndexDictionary<UInt32, String, T> p_items = null, MultiIndexDictionary<UInt32, String, T> p_icons = null,
                       bool p_allowDragAndDrop = false, bool p_load = true) : base(p_allowDragAndDrop)
    {
      Loaded = false;
      m_items = p_items;
      m_icons = p_icons;
      if (p_load && Load() == false)
        throw new Exception(ERR_GENERATE);
    }

    public static SafeDictionary<UInt32, Utils.TreeNode<UInt32>> LoadVirtualTree(MultiIndexDictionary<UInt32, String, T> p_items)
    {
      SafeDictionary<UInt32, Utils.TreeNode<UInt32>> l_dic = new SafeDictionary<uint, TreeNode<uint>>();
      if (Implements<HierarchyCRUDEntity>(typeof(T)) == false)
        return (null);
      foreach (T l_item in p_items.Values)
        l_dic[l_item.Id] = new TreeNode<UInt32>(l_item.Id);
      foreach (HierarchyCRUDEntity l_item in p_items.SortedValues)
      {
        if (l_dic.ContainsKey(l_item.Id) && l_dic.ContainsKey(l_item.ParentId))
          l_dic[l_item.ParentId].AddChild(l_dic[l_item.Id]);
      }
      return (l_dic);
    }

    public bool Add(vTreeNode p_node)
    {
      if (p_node == null)
        return (false);
      this.Nodes.Add(p_node);
      return (true);
    }

    public void FindAndAdd(NamedHierarchyCRUDEntity p_value)
    {
      vTreeNode l_newNode = FindNode(p_value.Id);
      vTreeNode l_parentNode = FindNode(p_value.ParentId);

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

    public void FindAndRemove(UInt32 p_value)
    {
      vTreeNode l_node = FindNode(p_value);

      if (l_node != null)
        Remove(l_node);
    }

    public bool Remove(vTreeNode p_node)
    {
      if (p_node == null)
        return (false);
      if (p_node.Parent != null)
      {
        p_node.Parent.Nodes.Remove(p_node);
        return (true);
      }
      p_node.Remove();
      return (true);
    }

    public void MoveNodeUp(vTreeNode p_node)
    {
      if (p_node == null)
        return;
      Swap(p_node, p_node.PrevSiblingNode);
      this.Refresh();
    }

    public void MoveNodeDown(vTreeNode p_node)
    {
      if (p_node == null)
        return;
      Swap(p_node, p_node.NextSiblingNode);
      this.Refresh();
    }

    public static List<UInt32> GetNodesIdsUint(vTreeNode p_node)
    {
      List<UInt32> l_tmpList = new List<UInt32>();

      foreach (vTreeNode l_node in GetAllChildrenNodesList(p_node))
      {
        if (!l_tmpList.Contains((UInt32)l_node.Value))
          l_tmpList.Add((UInt32)l_node.Value);
      }
      return (l_tmpList);
    }

    public static List<vTreeNode> GetAllChildrenNodesList(vTreeNode p_node)
    {
      List<vTreeNode> l_tmpList = new List<vTreeNode>();

      foreach (vTreeNode l_node in p_node.Nodes)
      {
        FillChildrenNodesList(l_node, l_tmpList);
      }
      return (l_tmpList);
    }

    /*
     * Insert node at the node p_nodeInsertAt.
     * If p_fixedParent = true, the p_nodeInsertAt MUST have childrens to insert a node.
     * Otherwise, a child will be created for the node p_nodeInsertAt.
    */
    public static bool Insert(vTreeNode p_nodeToInsert, vTreeNode p_nodeInsertAt, bool p_fixedParent = true)
    {
      if (p_nodeInsertAt == p_nodeToInsert || p_nodeToInsert.Parent == p_nodeInsertAt)
      {
        return (true);
      }
      if (p_fixedParent && p_nodeInsertAt.Nodes.Count == 0)
      {
        return (false);
      }
      p_nodeInsertAt.Nodes.Add(p_nodeToInsert);
      return (true);
    }

    public static vTreeNode GetRoot(vTreeNode p_node)
    {
      vTreeNode l_node = p_node;

      while (l_node.Parent != null)
        l_node = l_node.Parent;
      return (l_node);
    }

    public static bool Swap(vTreeNode p_nodeX, vTreeNode p_nodeY)
    {
      if (p_nodeY == null || p_nodeX == null)
        return (false);

      if (p_nodeX.Parent == null)
      {
        vTreeView l_parentNode = p_nodeX.TreeView;
        Int32 indexOrigin = l_parentNode.Nodes.IndexOf(p_nodeX);
        Int32 indexNext = l_parentNode.Nodes.IndexOf(p_nodeY);

        l_parentNode.Nodes.RemoveAt(indexOrigin);
        l_parentNode.Nodes.Insert(indexOrigin, p_nodeY);
        l_parentNode.Nodes.RemoveAt(indexNext);
        l_parentNode.Nodes.Insert(indexNext, p_nodeX);
      }
      else
      {
        vTreeNode l_parentNode = p_nodeX.Parent;
        Int32 indexOrigin = l_parentNode.Nodes.IndexOf(p_nodeX);
        Int32 indexNext = l_parentNode.Nodes.IndexOf(p_nodeY);

        l_parentNode.Nodes.RemoveAt(indexOrigin);
        l_parentNode.Nodes.Insert(indexOrigin, p_nodeY);
        l_parentNode.Nodes.RemoveAt(indexNext);
        l_parentNode.Nodes.Insert(indexNext, p_nodeX);
      }
      return (true);
    }

    private static void FillChildrenNodesList(vTreeNode p_node, List<vTreeNode> p_list)
    {
      p_list.Add(p_node);
      foreach (vTreeNode l_subNode in p_node.Nodes)
      {
        p_list.Add(l_subNode);
        FillChildrenNodesList(l_subNode, p_list);
      }
    }

    static bool Implements<TInterface>(Type type) where TInterface : class //TODO : use Benjamin thingy
    {
      var interfaceType = typeof(TInterface);

      if (!interfaceType.IsInterface)
        return (false);
      return (interfaceType.IsAssignableFrom(type));
    }

    bool Load(MultiIndexDictionary<UInt32, String, T> p_items, MultiIndexDictionary<UInt32, String, T> p_icons = null)
    {
      return (Load(Nodes, p_items, p_icons));
    }

    public static bool Load(vTreeNodeCollection p_nodes, MultiIndexDictionary<UInt32, String, T> p_items,
      MultiIndexDictionary<UInt32, String, T> p_icons = null)
    {
      SafeDictionary<UInt32, Utils.TreeNode<UInt32>> l_virtualHierarchy = LoadVirtualTree(p_items);

      if (Implements<NamedHierarchyCRUDEntity>(typeof(T)))
      {
        foreach (NamedHierarchyCRUDEntity l_item in p_items.SortedValues)
        {
          if (l_item.ParentId == 0)
          {
            vTreeNode l_node = new vTreeNode();
            if (!Generate(l_virtualHierarchy, p_items, l_node, l_item.Id, p_icons))
              return (false);
            p_nodes.Add(l_node);
          }
        }
      }
      else
      {
        foreach (NamedCRUDEntity l_item in p_items.SortedValues)
        {
          vTreeNode l_node = new vTreeNode();
          if (!Generate(l_virtualHierarchy, p_items, l_node, l_item.Id, p_icons))
            return (false);
          p_nodes.Add(l_node);
        }
      }
      return (true);
    }

    public override bool Load()
    {
      if (m_items != null && !this.Load(m_items, m_icons))
        return (false);
      Loaded = true;
      return (true);
    }

    private static bool Generate(SafeDictionary<UInt32, Utils.TreeNode<UInt32>> p_virtualHierarchy, MultiIndexDictionary<UInt32, String, T> p_items,
      vTreeNode p_node, UInt32 p_itemId, MultiIndexDictionary<UInt32, String, T> p_icons = null)
    {
      if (Implements<NamedHierarchyCRUDEntity>(typeof(T)) && p_virtualHierarchy != null)
      {
        NamedHierarchyCRUDEntity l_currentItem = (NamedHierarchyCRUDEntity)p_items[p_itemId];

        if (l_currentItem == null || p_virtualHierarchy.ContainsKey(l_currentItem.Id) == false)
          return (false);
        p_node.Value = l_currentItem.Id;
        p_node.Text = l_currentItem.Name;
        p_node.ImageIndex = (Int32)l_currentItem.Image;
        foreach (TreeNode<UInt32> l_childNode in p_virtualHierarchy[l_currentItem.Id].Children)
        {
          if (p_items.ContainsKey(l_childNode.Value) == false)
            continue;
          vTreeNode l_node = new vTreeNode();
          if (!Generate(p_virtualHierarchy, p_items, l_node, l_childNode.Value, p_icons))
            return (false);
          p_node.Nodes.Add(l_node);
          if (p_icons != null)
            p_node.ImageIndex = (int)p_icons[l_childNode.Value].Image;
        }
      }
      else
      {
        NamedCRUDEntity l_currentItem = (NamedCRUDEntity)p_items[p_itemId];

        if (l_currentItem == null)
          return (false);
        p_node.Value = l_currentItem.Id;
        p_node.Text = l_currentItem.Name;
        p_node.ImageIndex = (Int32)l_currentItem.Image;
      }
      return (true);
    }

  }
}
