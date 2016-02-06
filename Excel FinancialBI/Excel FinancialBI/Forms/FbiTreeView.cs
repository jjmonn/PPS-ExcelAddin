using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using VIBlend;
using VIBlend.WinForms.Controls;

namespace FBI.Forms
{
  using Utils;
  using MVC.Model.CRUD;

  public class FbiTreeView<T> : vTreeView where T : NamedCRUDEntity
  {
    private MultiIndexDictionary<UInt32, String, T> m_items;
    private static readonly string ERR_GENERATE = "[FbiTreeView] Cannot generate vTreeView. Either the MultiIndexDictionary is null or incorrect";

    public FbiTreeView(MultiIndexDictionary<UInt32, String, T> p_items = null, MultiIndexDictionary<UInt32, String, T> p_icons = null)
    {
      m_items = p_items;
      if (p_items != null && !this.Load(p_items, p_icons))
      {
        throw new Exception(ERR_GENERATE);
      }
      InitTVFormat(this);
    }

    public bool Append(MultiIndexDictionary<UInt32, String, T> p_items, MultiIndexDictionary<UInt32, String, T> p_icons = null)
    {
      if (p_items == null)
        return (false);
      return (this.Load(p_items, p_icons));
    }

    public bool Add(vTreeNode p_node)
    {
      if (p_node == null)
        return (false);
      this.Nodes.Add(p_node);
      return (true);
    }

    public bool Remove(vTreeNode p_node)
    {
      if (p_node == null || this.GetNodes().Exists(x => x != p_node))
        return (false);
      if (p_node.Parent != null)
      {
        p_node.Parent.Nodes.Remove(p_node);
        return (true);
      }
      p_node.Remove();
      return (true);
    }

    public vTreeNode FindAtPosition(Point p_position)
    {
      if (p_position == null)
        return (null);
      p_position.Y -= this.ScrollPosition.Y;
      p_position.X -= this.ScrollPosition.X;
      return (this.HitTest(p_position));
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

    public static void InitTVFormat(vTreeView p_treeview)
    {
      //VIBlendScrollBarsTheme = VIBLEND_THEME.OFFICESILVER;

      // Change the Expand/Collapse arrow color.
      p_treeview.UseThemeIndicatorsColor = false;
      p_treeview.IndicatorsColor = Color.FromArgb(128, 128, 128);
      p_treeview.IndicatorsHighlightColor = Color.FromArgb(128, 128, 128);
      p_treeview.EnableIndicatorsAnimation = false;
      p_treeview.PaintNodesDefaultBorder = false;
      p_treeview.PaintNodesDefaultFill = false;
      p_treeview.UseThemeBackColor = false;
      p_treeview.BackColor = Color.White;
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
      vTreeNode l_node = p_nodeX;

      if (p_nodeX == null || p_nodeY == null)
        return (false);
      p_nodeX = p_nodeY;
      p_nodeY = l_node;
      return (true);
    }

    static bool Implements<TInterface>(Type type) where TInterface : class //TODO : use Benjamin thingy
    {
      var interfaceType = typeof(TInterface);

      if (!interfaceType.IsInterface)
        return (false);
      return (interfaceType.IsAssignableFrom(type));
    }

    private bool Load(MultiIndexDictionary<UInt32, String, T> p_items, MultiIndexDictionary<UInt32, String, T> p_icons = null)
    {
      if (Implements<NamedHierarchyCRUDEntity>(typeof(T)))
      {
        foreach (NamedHierarchyCRUDEntity l_item in p_items.SortedValues)
        {
          if (l_item.ParentId == 0)
          {
            vTreeNode l_node = new vTreeNode();
            if (!this.Generate(p_items, l_node, l_item.Id, p_icons))
              return (false);
            this.Nodes.Add(l_node);
          }
        }
      }
      else
      {
        /*foreach (NamedCRUDEntity l_item in m_items.SortedValues)
        {
          vTreeNode l_node = new vTreeNode();
          if (!this.Generate(l_node, l_item.Id))
            return (false);
          this.Nodes.Add(l_node);
        }*/
      }
      return (true);
    }

    private bool Generate(MultiIndexDictionary<UInt32, String, T> p_items, vTreeNode p_node, UInt32 p_itemId, MultiIndexDictionary<UInt32, String, T> p_icons = null)
    {
      if (Implements<NamedHierarchyCRUDEntity>(typeof(T)))
      {
        NamedHierarchyCRUDEntity l_currentItem = (NamedHierarchyCRUDEntity)p_items[p_itemId];

        if (l_currentItem == null)
          return (false);
        p_node.Value = l_currentItem.ToString();
        p_node.Text = l_currentItem.Name;
        foreach (NamedHierarchyCRUDEntity l_item in p_items.SortedValues)
        {
          if (l_item.ParentId == l_currentItem.Id)
          {
            vTreeNode l_node = new vTreeNode();
            if (!this.Generate(p_items, l_node, l_item.Id, p_icons))
              return (false);
            p_node.Nodes.Add(l_node);
            if (p_icons != null)
            {
              p_node.ImageIndex = (int)p_icons[l_item.Id].Image;
            }
          }
        }
      }
      else
      {
        /*NamedCRUDEntity l_currentItem = (NamedCRUDEntity)m_items[p_itemId];

        if (l_currentItem == null)
          return (false);
        p_node.Value = l_currentItem.ToString();
        p_node.Text = l_currentItem.Name;
        foreach (NamedCRUDEntity l_item in m_items.SortedValues)
        {
          vTreeNode l_node = new vTreeNode();
          if (!this.Generate(l_node, l_item.Id))
            return (false);
          p_node.Nodes.Add(l_node);
          if (m_icons != null)
          {
            p_node.ImageIndex = (int)m_icons[l_item.Id].Image;
          }
        }*/
      }
      return (true);
    }
  }
}
