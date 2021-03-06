﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VIBlend.WinForms.Controls;

namespace FBI.Forms
{

  public delegate void DroppedEventHandler(object sender, DragEventArgs e);
  public delegate void NodeDroppedEventHandler(vTreeNode p_draggedNode, vTreeNode p_targetNode);

  public abstract class AFbiTreeView : vTreeView
  {
    public event NodeDroppedEventHandler NodeDropped;
    public event DroppedEventHandler Dropped;
    public virtual bool Loaded { get; protected set; }

    public AFbiTreeView(bool p_allowDragAndDrop)
    {
      AFbiTreeView.InitTVFormat(this);
      this.AllowDrop = p_allowDragAndDrop;
      if (p_allowDragAndDrop == true)
        SubscribeDragAndDropEvents();
    }

    public abstract bool Load();

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
      p_treeview.Dock = DockStyle.Fill;
    }


    public static void HideParentCheckBox(vTreeView p_tv)
    {
      foreach (vTreeNode l_node in p_tv.GetNodes())
        if (l_node.Nodes.Count != 0)
          l_node.ShowCheckBox = false;
    }

    public void HideParentCheckBox()
    {
      HideParentCheckBox(this);
    }

    public void CheckNode(UInt32 p_id)
    {
      CheckNode(this, p_id);
    }

    public static void CheckAllParentNodes(vTreeView p_tv)
    {
      foreach (vTreeNode l_node in p_tv.Nodes)
        l_node.Checked = CheckState.Checked;
    }

    public void CheckAllParentNodes()
    {
      CheckAllParentNodes(this);
    }

    public static void CheckNode(vTreeView p_tv, UInt32 p_id)
    {
      vTreeNode l_node = FindNode(p_tv, p_id);

      if (l_node != null)
        l_node.Checked = CheckState.Checked;
    }

    public vTreeNode FindAtPosition(Point p_position)
    {
      if (p_position == null)
        return (null);
      p_position.Y -= this.ScrollPosition.Y;
      p_position.X -= this.ScrollPosition.X;
      return (this.HitTest(p_position));
    }

    protected void SubscribeDragAndDropEvents()
    {
      // Careful: you must subscribe to the mouse down event into your view to launch the drag and drop
      this.DragEnter += FbiTreeview_DragEnter;
      this.DragOver += FbiTreeview_DragOver;
      this.DragDrop += FbiTreeview_DragDropNode;
      this.DragDrop += FbiTreeview_DragDrop;
      // You must subscribe to the NodeDropped event into your view in order to send the parent id update to your model
    }

    #region Drag and drop envents

    private void FbiTreeview_DragEnter(object sender, DragEventArgs e)
    {
      if (e.Data.GetData(typeof(vTreeNode)) as vTreeNode != null)
      {
        e.Effect = DragDropEffects.Move;
      }
      else
      {
        e.Effect = DragDropEffects.None;
      }
    }

    private void FbiTreeview_DragOver(object sender, DragEventArgs e)
    {
      vTreeNode l_treenode = this.FindAtPosition(new Point(e.X, e.Y));
      if (l_treenode != null && !object.ReferenceEquals(l_treenode, e.Data as vTreeNode))
      {
        e.Effect = DragDropEffects.Move;
      }
    }

    private void FbiTreeview_DragDropNode(object sender, DragEventArgs e)
    {
      vTreeNode l_draggedNode = e.Data.GetData(typeof(vTreeNode)) as vTreeNode;
      if (l_draggedNode != null)
      {
        Point location = PointToClient(new Point(e.X, e.Y - this.ScrollPosition.Y));
        vTreeNode l_targetNode = this.HitTest(location);

        if (NodeDropped != null)
          NodeDropped(l_draggedNode, l_targetNode);
      }
    }

    private void FbiTreeview_DragDrop(object sender, DragEventArgs e)
    {
      if (Dropped != null)
        Dropped(sender, e);
    }

    public static vTreeNode FindNode(vTreeView p_tv, UInt32 p_value)
    {
      foreach (vTreeNode l_node in p_tv.GetNodes())
        if (l_node.Value != null && (UInt32)l_node.Value == p_value)
          return (l_node);
      return (null);
    }

    public vTreeNode FindNode(UInt32 p_value)
    {
      return (FindNode(this, p_value));
    }

    #endregion

    public static void Copy(vTreeView p_copy, vTreeView p_toCopy)
    {
      vTreeNode l_newNode;

      foreach (vTreeNode l_node in p_toCopy.Nodes)
      {
        l_newNode = new vTreeNode(l_node.Text);
        l_newNode.Value = l_node.Value;
        l_newNode.Tag = l_node.Tag;
        CopyChildren(l_newNode, l_node);
        p_copy.Nodes.Add(l_newNode);
      }
    }

    public static void CopyChildren(vTreeNode p_parent, vTreeNode p_node)
    {
      vTreeNode l_newNode;

      foreach (vTreeNode l_node in p_node.Nodes)
      {
        l_newNode = new vTreeNode(l_node.Text);
        l_newNode.Value = l_node.Value;
        l_newNode.Tag = l_node.Tag;
        p_parent.Nodes.Add(l_newNode);
        CopyChildren(l_newNode, l_node);
      }
    }
  }
}
