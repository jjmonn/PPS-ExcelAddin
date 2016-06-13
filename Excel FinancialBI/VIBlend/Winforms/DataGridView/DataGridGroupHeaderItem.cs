// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.DataGridGroupHeaderItem
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System.Drawing;

namespace VIBlend.WinForms.DataGridView
{
  public class DataGridGroupHeaderItem
  {
    private bool showCloseButton = true;
    private Color closeButtonColor = Color.Black;
    private Color closeButtonHighlightColor = Color.Red;
    private Rectangle bounds;
    private Rectangle renderBounds;
    private Rectangle closeButtonRenderBounds;
    private BoundField associatedItem;
    private string text;
    private vDataGridViewGroupsHeader parentHeader;

    /// <summary>Gets the parent header.</summary>
    /// <value>The parent header.</value>
    public vDataGridViewGroupsHeader ParentHeader
    {
      get
      {
        return this.parentHeader;
      }
    }

    /// <summary>Gets the column.</summary>
    /// <value>The column.</value>
    public BoundField Column
    {
      get
      {
        return this.associatedItem;
      }
    }

    /// <summary>Gets or sets the text.</summary>
    /// <value>The text.</value>
    public string Text
    {
      get
      {
        return this.text;
      }
      set
      {
        this.text = value;
        if (this.ParentHeader == null)
          return;
        this.ParentHeader.Invalidate();
      }
    }

    /// <summary>Gets or sets the bounds.</summary>
    /// <value>The bounds.</value>
    public Rectangle Bounds
    {
      get
      {
        return this.bounds;
      }
      internal set
      {
        this.bounds = value;
      }
    }

    /// <summary>Gets or sets the bounds.</summary>
    /// <value>The bounds.</value>
    public Rectangle RenderBounds
    {
      get
      {
        return this.renderBounds;
      }
      internal set
      {
        this.renderBounds = value;
      }
    }

    /// <summary>Gets or sets the bounds.</summary>
    /// <value>The bounds.</value>
    public Rectangle CloseButtonRenderBounds
    {
      get
      {
        return this.closeButtonRenderBounds;
      }
      internal set
      {
        this.closeButtonRenderBounds = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show a close button.
    /// </summary>
    public bool ShowCloseButton
    {
      get
      {
        return this.showCloseButton;
      }
      set
      {
        this.showCloseButton = value;
        if (this.ParentHeader == null)
          return;
        this.ParentHeader.Invalidate();
      }
    }

    /// <summary>Gets or sets the color of the close button.</summary>
    /// <value>The color of the close button.</value>
    public Color CloseButtonColor
    {
      get
      {
        return this.closeButtonColor;
      }
      set
      {
        this.closeButtonColor = value;
        if (this.ParentHeader == null)
          return;
        this.ParentHeader.Invalidate();
      }
    }

    /// <summary>Gets or sets the color of the close button highlight.</summary>
    /// <value>The color of the close button highlight.</value>
    public Color CloseButtonHighlightColor
    {
      get
      {
        return this.closeButtonHighlightColor;
      }
      set
      {
        this.closeButtonHighlightColor = value;
        if (this.ParentHeader == null)
          return;
        this.ParentHeader.Invalidate();
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.DataGridView.DataGridGroupHeaderItem" /> class.
    /// </summary>
    /// <param name="item">The item.</param>
    public DataGridGroupHeaderItem(BoundField boundField, vDataGridViewGroupsHeader parent)
    {
      this.associatedItem = boundField;
      this.parentHeader = parent;
      if (boundField == null)
        return;
      this.text = boundField.Text;
    }
  }
}
