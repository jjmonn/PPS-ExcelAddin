// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vControlBoundsHelper
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Drawing;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls
{
  public class vControlBoundsHelper
  {
    private int resizeOffset = 5;
    private int edgeOffset = 5;
    private bool enableResize = true;
    private bool enableMove = true;
    private Control contentControl;
    private Rectangle leftResizeRect;
    private Rectangle topResizeRect;
    private Rectangle rightResizeRect;
    private Rectangle bottomResizeRect;
    private Rectangle topLeftEdge;
    private Rectangle topRightEdge;
    private Rectangle bottomLeftEdge;
    private Rectangle bottomRightEdge;
    private bool leftResize;
    private bool rightResize;
    private bool topResize;
    private bool bottomResize;
    private bool topleftResize;
    private bool topRightResize;
    private bool bottomLeftResize;
    private bool bottomRightResize;
    private Point initialPosition;
    private Size initialSize;
    private Rectangle mouseMoveArea;

    /// <summary>
    /// Gets or sets a value indicating whether the resizing is enabled.
    /// </summary>
    /// <value><c>true</c> if [enable resize]; otherwise, <c>false</c>.</value>
    public bool EnableResize
    {
      get
      {
        return this.enableResize;
      }
      set
      {
        this.enableResize = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the movement is enabled.
    /// </summary>
    public bool EnableMove
    {
      get
      {
        return this.enableMove;
      }
      set
      {
        this.enableMove = value;
      }
    }

    /// <summary>Gets the content control.</summary>
    /// <value>The content control.</value>
    public Control ContentControl
    {
      get
      {
        return this.ContentControl;
      }
    }

    /// <summary>Gets or sets the mouse move area.</summary>
    /// <value>The mouse move area.</value>
    public Rectangle MouseMoveArea
    {
      get
      {
        return this.mouseMoveArea;
      }
      set
      {
        this.mouseMoveArea = value;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vControlBoundsHelper" /> class.
    /// </summary>
    /// <param name="content">The content.</param>
    public vControlBoundsHelper(Control content)
    {
      this.contentControl = content;
      this.Wire();
    }

    /// <summary>Unwire events.</summary>
    public void UnWire()
    {
      this.contentControl.SizeChanged -= new EventHandler(this.contentControl_SizeChanged);
      this.contentControl.MouseUp -= new MouseEventHandler(this.contentControl_MouseUp);
      this.contentControl.MouseDown -= new MouseEventHandler(this.contentControl_MouseDown);
      this.contentControl.MouseMove -= new MouseEventHandler(this.contentControl_MouseMove);
      this.contentControl.MouseLeave -= new EventHandler(this.contentControl_MouseLeave);
    }

    /// <summary>Wires to the content control events.</summary>
    public void Wire()
    {
      this.contentControl.SizeChanged += new EventHandler(this.contentControl_SizeChanged);
      this.contentControl.MouseUp += new MouseEventHandler(this.contentControl_MouseUp);
      this.contentControl.MouseDown += new MouseEventHandler(this.contentControl_MouseDown);
      this.contentControl.MouseMove += new MouseEventHandler(this.contentControl_MouseMove);
      this.contentControl.MouseLeave += new EventHandler(this.contentControl_MouseLeave);
    }

    private void contentControl_MouseLeave(object sender, EventArgs e)
    {
      if (this.contentControl.Capture)
        return;
      Point client = this.contentControl.PointToClient(Cursor.Position);
      this.SetResizingCursor(new MouseEventArgs(MouseButtons.None, 0, client.X, client.Y, 0));
    }

    private void contentControl_MouseMove(object sender, MouseEventArgs e)
    {
      if (!this.contentControl.Capture)
      {
        if (this.rightResizeRect.Equals((object) Rectangle.Empty))
          this.InitializeResizeRectangles();
        if (!this.EnableResize)
          return;
        this.SetResizingCursor(e);
      }
      else
      {
        if (this.enableResize)
        {
          if (this.leftResize)
          {
            this.DoLeftResize(e);
            return;
          }
          if (this.topResize)
          {
            this.DoTopResize(e);
            return;
          }
          if (this.rightResize)
          {
            this.DoRightResize(e);
            return;
          }
          if (this.bottomResize)
          {
            this.DoBottomResize(e);
            return;
          }
          if (this.topleftResize)
          {
            this.DoTopLeftResize(e);
            return;
          }
          if (this.topRightResize)
          {
            this.DoTopRightResize(e);
            return;
          }
          if (this.bottomLeftResize)
          {
            this.DoBottomLeftResize(e);
            return;
          }
          if (this.bottomRightResize)
          {
            this.DoBottomRightResize(e);
            return;
          }
        }
        if (!this.contentControl.Capture || !this.enableMove)
          return;
        if (this.mouseMoveArea == Rectangle.Empty)
        {
          this.contentControl.Location = new Point(this.contentControl.Location.X + e.Location.X - this.initialPosition.X, this.contentControl.Location.Y + e.Location.Y - this.initialPosition.Y);
        }
        else
        {
          if (!this.mouseMoveArea.Contains(this.initialPosition))
            return;
          this.contentControl.Location = new Point(this.contentControl.Location.X + e.Location.X - this.initialPosition.X, this.contentControl.Location.Y + e.Location.Y - this.initialPosition.Y);
        }
      }
    }

    private void DoLeftResize(MouseEventArgs e)
    {
      Cursor.Current = Cursors.SizeWE;
      Size size = this.contentControl.Size;
      Point newLocation;
      Size newSize;
      this.GetLeftResizeBounds(e, out newLocation, out newSize);
      if (!(newSize != size))
        return;
      this.contentControl.SetBounds(newLocation.X, newLocation.Y, newSize.Width, newSize.Height);
    }

    private void DoBottomLeftResize(MouseEventArgs e)
    {
      Cursor.Current = Cursors.SizeNESW;
      this.bottomLeftResize = true;
      Size size = this.contentControl.Size;
      Point newLocation1;
      Size newSize1;
      this.GetBottomResizeBounds(e, out newLocation1, out newSize1);
      Point newLocation2;
      Size newSize2;
      this.GetLeftResizeBounds(e, out newLocation2, out newSize2);
      if (!(newSize1 != size))
        return;
      this.contentControl.SetBounds(newLocation2.X, newLocation1.Y, newSize2.Width, newSize1.Height);
    }

    private void GetLeftResizeBounds(MouseEventArgs e, out Point newLocation, out Size newSize)
    {
      Point point = new Point(this.initialPosition.X - e.Location.X, this.contentControl.Location.Y);
      newSize = new Size(this.contentControl.Width + point.X, this.contentControl.Height);
      if (newSize.Width >= this.contentControl.MinimumSize.Width)
        newLocation = new Point(this.contentControl.Location.X - point.X, this.contentControl.Location.Y);
      else
        newLocation = this.contentControl.Location;
    }

    private void DoTopResize(MouseEventArgs e)
    {
      Cursor.Current = Cursors.SizeNS;
      Size size = this.contentControl.Size;
      Point newLocation;
      Size newSize;
      this.GetTopResizeBounds(e, out newLocation, out newSize);
      if (!(newSize != size))
        return;
      this.contentControl.Bounds = new Rectangle(newLocation.X, newLocation.Y, newSize.Width, newSize.Height);
    }

    private void DoRightResize(MouseEventArgs e)
    {
      Cursor.Current = Cursors.SizeWE;
      Size size1 = this.contentControl.Size;
      Point point = new Point(this.initialPosition.X - e.Location.X, this.contentControl.Location.Y);
      Point location = this.contentControl.Location;
      Size size2 = new Size(this.initialSize.Width - point.X, this.contentControl.Height);
      if (!(size2 != size1))
        return;
      this.contentControl.Bounds = new Rectangle(location.X, location.Y, size2.Width, size2.Height);
    }

    private void DoBottomResize(MouseEventArgs e)
    {
      Cursor.Current = Cursors.SizeNS;
      Size size = this.contentControl.Size;
      Point newLocation;
      Size newSize;
      this.GetBottomResizeBounds(e, out newLocation, out newSize);
      if (!(newSize != size))
        return;
      this.contentControl.Bounds = new Rectangle(newLocation.X, newLocation.Y, newSize.Width, newSize.Height);
    }

    private void DoTopLeftResize(MouseEventArgs e)
    {
      Cursor.Current = Cursors.SizeNWSE;
      Size size = this.contentControl.Size;
      Point newLocation1;
      Size newSize1;
      this.GetTopResizeBounds(e, out newLocation1, out newSize1);
      Point newLocation2;
      Size newSize2;
      this.GetLeftResizeBounds(e, out newLocation2, out newSize2);
      this.contentControl.Bounds = new Rectangle(newLocation2.X, newLocation1.Y, newSize2.Width, newSize1.Height);
    }

    private void DoTopRightResize(MouseEventArgs e)
    {
      Cursor.Current = Cursors.SizeNESW;
      Size size1 = this.contentControl.Size;
      Point point = new Point(this.initialPosition.X - e.Location.X, this.contentControl.Location.Y);
      Point location = this.contentControl.Location;
      Size size2 = new Size(this.initialSize.Width - point.X, this.contentControl.Height);
      if (!(size2 != size1))
        return;
      Point newLocation;
      Size newSize;
      this.GetTopResizeBounds(e, out newLocation, out newSize);
      this.contentControl.Bounds = new Rectangle(location.X, newLocation.Y, size2.Width, newSize.Height);
    }

    private void DoBottomRightResize(MouseEventArgs e)
    {
      Cursor.Current = Cursors.SizeNWSE;
      Size size1 = this.contentControl.Size;
      Point point = new Point(this.initialPosition.X - e.Location.X, this.contentControl.Location.Y);
      Point location = this.contentControl.Location;
      Size size2 = new Size(this.initialSize.Width - point.X, this.contentControl.Height);
      if (!(size2 != size1))
        return;
      Point newLocation;
      Size newSize;
      this.GetBottomResizeBounds(e, out newLocation, out newSize);
      this.contentControl.Bounds = new Rectangle(location.X, newLocation.Y, size2.Width, newSize.Height);
    }

    private void InitializeFlags()
    {
      this.leftResize = false;
      this.rightResize = false;
      this.topResize = false;
      this.bottomResize = false;
      this.topleftResize = false;
      this.topRightResize = false;
      this.bottomLeftResize = false;
      this.bottomRightResize = false;
    }

    private void contentControl_MouseDown(object sender, MouseEventArgs e)
    {
      this.contentControl.Capture = true;
      this.InitializeFlags();
      this.InitializeResizeRectangles();
      this.SetResizingCursor(e);
      this.initialPosition = e.Location;
      this.initialSize = this.contentControl.Size;
    }

    private void contentControl_MouseUp(object sender, MouseEventArgs e)
    {
      this.contentControl.Capture = false;
      this.InitializeFlags();
    }

    private void contentControl_SizeChanged(object sender, EventArgs e)
    {
      this.InitializeResizeRectangles();
    }

    private void InitializeResizeRectangles()
    {
      this.leftResizeRect = new Rectangle(0, this.edgeOffset, this.resizeOffset, this.contentControl.Height - 2 * this.edgeOffset);
      this.rightResizeRect = new Rectangle(this.contentControl.Width - this.resizeOffset, this.edgeOffset, this.resizeOffset, this.contentControl.Height - 2 * this.edgeOffset);
      this.topResizeRect = new Rectangle(this.edgeOffset, 0, this.contentControl.Width - 2 * this.edgeOffset, this.resizeOffset);
      this.bottomResizeRect = new Rectangle(this.edgeOffset, this.contentControl.Height - this.edgeOffset, this.contentControl.Width - 2 * this.edgeOffset, this.edgeOffset);
      this.topLeftEdge = new Rectangle(0, 0, this.edgeOffset, this.edgeOffset);
      this.topRightEdge = new Rectangle(this.contentControl.Width - this.edgeOffset, 0, this.edgeOffset, this.edgeOffset);
      this.bottomLeftEdge = new Rectangle(0, this.contentControl.Height - this.edgeOffset, this.edgeOffset, this.edgeOffset);
      this.bottomRightEdge = new Rectangle(this.contentControl.Width - this.edgeOffset, this.contentControl.Height - this.edgeOffset, this.edgeOffset, this.edgeOffset);
    }

    private void GetBottomResizeBounds(MouseEventArgs e, out Point newLocation, out Size newSize)
    {
      Point point = new Point(this.contentControl.Location.X, this.initialPosition.Y - e.Location.Y);
      newLocation = this.contentControl.Location;
      newSize = new Size(this.contentControl.Width, this.initialSize.Height - point.Y);
    }

    private void GetTopResizeBounds(MouseEventArgs e, out Point newLocation, out Size newSize)
    {
      Point location = this.contentControl.Location;
      Size size = this.contentControl.Size;
      Point point = new Point(this.contentControl.Location.X, this.initialPosition.Y - e.Location.Y);
      newSize = new Size(this.contentControl.Width, this.contentControl.Height + point.Y);
      if (newSize.Height > this.contentControl.MinimumSize.Height)
        newLocation = new Point(this.contentControl.Location.X, this.contentControl.Location.Y - point.Y);
      else
        newLocation = this.contentControl.Location;
    }

    private void SetResizingCursor(MouseEventArgs e)
    {
      if (!this.EnableResize)
        return;
      if (this.leftResizeRect.Contains(e.Location))
      {
        Cursor.Current = Cursors.SizeWE;
        this.leftResize = true;
      }
      else if (this.topResizeRect.Contains(e.Location))
      {
        Cursor.Current = Cursors.SizeNS;
        this.topResize = true;
      }
      else if (this.rightResizeRect.Contains(e.Location))
      {
        Cursor.Current = Cursors.SizeWE;
        this.rightResize = true;
      }
      else if (this.bottomResizeRect.Contains(e.Location))
      {
        if (Cursor.Current != Cursors.SizeNS)
          Cursor.Current = Cursors.SizeNS;
        this.bottomResize = true;
      }
      else if (this.topLeftEdge.Contains(e.Location))
      {
        Cursor.Current = Cursors.SizeNWSE;
        this.topleftResize = true;
      }
      else if (this.topRightEdge.Contains(e.Location))
      {
        Cursor.Current = Cursors.SizeNESW;
        this.topRightResize = true;
      }
      else if (this.bottomLeftEdge.Contains(e.Location))
      {
        Cursor.Current = Cursors.SizeNESW;
        this.bottomLeftResize = true;
      }
      else
      {
        if (!this.bottomRightEdge.Contains(e.Location))
          return;
        Cursor.Current = Cursors.SizeNWSE;
        this.bottomRightResize = true;
      }
    }
  }
}
