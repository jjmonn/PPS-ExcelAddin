using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIBlend.WinForms.Controls;

namespace FBI.Utils
{
  class TreeviewUtils
  {

    #region Find node

    public static vTreeNode FindNode(vTreeView p_treeview, UInt32 p_id)
    {
      List<vTreeNode> childrenNodesList = new List<vTreeNode>();
      foreach (vTreeNode subNode in p_treeview.Nodes)
      {
        FillChildrenNodesList(subNode, childrenNodesList);
      }
      foreach (vTreeNode subNode in childrenNodesList)
      {
        if ((UInt32)subNode.Value == p_id)
          return subNode;
      }
      return null;
    }

    private static void FillChildrenNodesList(vTreeNode p_node, List<vTreeNode> p_nodesList)
    {
      p_nodesList.Add(p_node);
      foreach (vTreeNode subNode in p_node.Nodes)
      {
        FillChildrenNodesList(subNode, p_nodesList);
      }
    }

    #endregion

    public static void TreeviewboxNodeSelection(vTreeViewBox p_treeviewbox, vTreeNode p_treenode)
    {
      if (p_treenode != null)
      {
        p_treeviewbox.TreeView.SelectedNode = p_treenode;
        p_treeviewbox.Text = p_treeviewbox.TreeView.SelectedNode.Text;
      }
    }


  }
}
