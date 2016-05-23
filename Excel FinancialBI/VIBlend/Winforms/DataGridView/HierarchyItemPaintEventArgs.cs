// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.HierarchyItemPaintEventArgs
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;
using System.Drawing;

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>Represents a HierarchyItem paint event argument</summary>
  public class HierarchyItemPaintEventArgs : EventArgs
  {
    private bool handled;
    private Rectangle bounds;
    private Graphics graphics;
    private HierarchyItem item;

    /// <summary>
    /// Determines whether the painting was performed by the event handler and the grid should not perform further painting for the item
    /// </summary>
    public bool Handled
    {
      get
      {
        return this.handled;
      }
      set
      {
        this.handled = value;
      }
    }

    /// <summary>The bounds of the paint area</summary>
    public Rectangle Bounds
    {
      get
      {
        return this.bounds;
      }
    }

    /// <summary>Refrence to GDI+ surface</summary>
    public Graphics Graphics
    {
      get
      {
        return this.graphics;
      }
    }

    /// <summary>The HierarachyItem associated with the paint event.</summary>
    public HierarchyItem HierarchyItem
    {
      get
      {
        return this.item;
      }
      set
      {
        this.item = value;
      }
    }

    /// <summary>Constructor</summary>
    /// <param name="item">A HierarchyItem associated with the event.</param>
    /// <param name="bounds">The bounds of the the item's paint area.</param>
    /// <param name="graphics">GDI+ surface</param>
    public HierarchyItemPaintEventArgs(HierarchyItem item, Rectangle bounds, Graphics graphics)
    {
      this.item = item;
      this.bounds = bounds;
      this.graphics = graphics;
    }
  }
}
