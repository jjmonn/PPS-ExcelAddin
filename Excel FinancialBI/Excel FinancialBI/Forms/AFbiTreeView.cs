using System;
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
        Point location = this.PointToClient(Cursor.Position);
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
        if ((UInt32)l_node.Value == p_value)
          return (l_node);
      return (null);
    }

    public vTreeNode FindNode(UInt32 p_value)
    {
      return (FindNode(this, p_value));
    }

    #endregion

  }
}
