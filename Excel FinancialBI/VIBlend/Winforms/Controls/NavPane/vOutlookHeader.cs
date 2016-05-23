// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vOutlookHeader
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <exclude />
  [Designer("VIBlend.WinForms.Controls.Design.OutlookHeaderDesigner, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxItem(false)]
  public class vOutlookHeader : Control, IScrollableControlBase
  {
    private bool allowAnimations = true;
    internal bool enableStates = true;
    private PaintHelper helper = new PaintHelper();
    private vOutlookItem item;
    internal BackgroundElement backFill;
    private AnimationManager manager;
    internal ControlState state;

    /// <summary>Gets or sets the animation manager.</summary>
    /// <value>The animation manager.</value>
    [Browsable(false)]
    public AnimationManager AnimationManager
    {
      get
      {
        if (this.manager == null)
          this.manager = new AnimationManager((Control) this);
        return this.manager;
      }
      set
      {
        this.manager = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to allow animations
    /// </summary>
    [Browsable(false)]
    public bool AllowAnimations
    {
      get
      {
        return this.allowAnimations;
      }
      set
      {
        this.allowAnimations = value;
        this.backFill.IsAnimated = value;
      }
    }

    /// <summary>
    /// Gets or sets the font of the text displayed by the control.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property.
    /// </returns>
    public new Font Font
    {
      get
      {
        return base.Font;
      }
      set
      {
        base.Font = value;
        this.backFill.Font = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets the nav pane item.</summary>
    /// <value>The nav pane item.</value>
    public vOutlookItem NavPaneItem
    {
      get
      {
        return this.item;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vOutlookHeader" /> class.
    /// </summary>
    /// <param name="item">The item.</param>
    public vOutlookHeader(vOutlookItem item)
    {
      this.Text = "Header";
      this.item = item;
      this.Size = new Size(0, 30);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.backFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
    }

    /// <summary>
    /// Raises the <see cref="E:MouseMove" /> event.
    /// </summary>
    /// <param name="mevent">The <see cref="T:System.Windows.Forms.MouseEventArgs" /> instance containing the event data.</param>
    protected override void OnMouseMove(MouseEventArgs mevent)
    {
      if (this.ClientRectangle.Contains(mevent.Location) && !this.Capture)
      {
        this.state = ControlState.Hover;
        this.Refresh();
        if (this.NavPaneItem != null && this.NavPaneItem.NavPane != null)
          this.NavPaneItem.NavPane.StartTimer();
      }
      base.OnMouseMove(mevent);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnMouseLeave(EventArgs e)
    {
      if (this.NavPaneItem != null && this.NavPaneItem.NavPane != null)
      {
        this.NavPaneItem.NavPane.paneToolTip.RemoveAll();
        this.NavPaneItem.NavPane.timerToolTip.Stop();
      }
      this.state = ControlState.Normal;
      this.Refresh();
      base.OnMouseLeave(e);
    }

    /// <summary>
    /// Raises the <see cref="E:MouseDown" /> event.
    /// </summary>
    /// <param name="mevent">The <see cref="T:System.Windows.Forms.MouseEventArgs" /> instance containing the event data.</param>
    protected override void OnMouseDown(MouseEventArgs mevent)
    {
      this.Capture = true;
      this.Refresh();
      base.OnMouseDown(mevent);
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      this.Invalidate();
    }

    /// <summary>
    /// Raises the <see cref="E:MouseUp" /> event.
    /// </summary>
    /// <param name="mevent">The <see cref="T:System.Windows.Forms.MouseEventArgs" /> instance containing the event data.</param>
    protected override void OnMouseUp(MouseEventArgs mevent)
    {
      this.Capture = false;
      this.state = !this.ClientRectangle.Contains(mevent.Location) ? ControlState.Normal : ControlState.Hover;
      this.Refresh();
      base.OnMouseUp(mevent);
    }

    private bool IsOffice2010()
    {
      return this.NavPaneItem != null && this.NavPaneItem.NavPane != null && (this.NavPaneItem.NavPane.VIBlendTheme == VIBLEND_THEME.OFFICE2010BLACK || this.NavPaneItem.NavPane.VIBlendTheme == VIBLEND_THEME.OFFICE2010BLUE || this.NavPaneItem.NavPane.VIBlendTheme == VIBLEND_THEME.OFFICE2010SILVER);
    }

    /// <summary>Paints the background of the control.</summary>
    /// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains information about the control to paint.</param>
    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
      if (this.state != ControlState.Hover)
        this.state = ControlState.Normal;
      if (this.NavPaneItem != null && this.NavPaneItem.IsExpanded && this.NavPaneItem.NavPane != null)
        this.state = ControlState.Pressed;
      if (!this.enableStates)
        this.state = ControlState.Normal;
      if (!this.Enabled)
        this.state = ControlState.Disabled;
      DrawOutlookNavPaneEventArgs args = new DrawOutlookNavPaneEventArgs(e.Graphics, this.state, this);
      if (this.NavPaneItem != null && this.NavPaneItem.NavPane != null)
      {
        this.NavPaneItem.NavPane.OnDrawItemHeader(args);
        if (args.Handled)
          return;
      }
      else if (this.Parent is vOutlookNavPane)
      {
        (this.Parent as vOutlookNavPane).OnDrawItemHeader(args);
        if (args.Handled)
          return;
      }
      this.RenderHeader(e);
    }

    /// <summary>Renders the header.</summary>
    /// <param name="e">The <see cref="T:System.Windows.Forms.PaintEventArgs" /> instance containing the event data.</param>
    protected virtual void RenderHeader(PaintEventArgs e)
    {
      if (this.backFill.Theme == null)
        this.backFill.LoadTheme(this.NavPaneItem.NavPane.ItemsTheme);
      if (this.NavPaneItem != null && this.NavPaneItem.HeaderFont != null && this.Font != this.NavPaneItem.HeaderFont)
      {
        this.Font = this.NavPaneItem.HeaderFont;
        this.backFill.Font = this.NavPaneItem.HeaderFont;
        this.backFill.HighLightFont = this.NavPaneItem.HeaderFont;
        this.backFill.PressedFont = this.NavPaneItem.HeaderFont;
      }
      if (!this.enableStates && this.NavPaneItem != null && (this.NavPaneItem.NavPane != null && !this.NavPaneItem.NavPane.UseHeaderThemeBackground))
      {
        this.backFill.Bounds = this.ClientRectangle;
        this.backFill.Radius = 0;
        Rectangle rectangle = new Rectangle(0, 0, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
        using (Pen pen = new Pen(this.NavPaneItem.NavPane.HeaderBackgroundBorder))
        {
          e.Graphics.FillRectangle(this.NavPaneItem.NavPane.HeaderBackgroundBrush, this.backFill.Bounds);
          this.backFill.Bounds = rectangle;
          e.Graphics.DrawRectangle(pen, this.backFill.Bounds);
        }
      }
      else if (this.NavPaneItem != null && !this.NavPaneItem.UseThemeBackground)
      {
        this.backFill.Bounds = this.ClientRectangle;
        this.backFill.Radius = 0;
        Rectangle rectangle = new Rectangle(0, 0, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
        using (Pen pen = new Pen(this.NavPaneItem.BackgroundBorder))
        {
          switch (this.state)
          {
            case ControlState.Normal:
            case ControlState.Default:
              e.Graphics.FillRectangle(this.NavPaneItem.BackgroundBrush, this.backFill.Bounds);
              this.backFill.Bounds = rectangle;
              e.Graphics.DrawRectangle(pen, this.backFill.Bounds);
              break;
            case ControlState.Hover:
              e.Graphics.FillRectangle(this.NavPaneItem.HighlightBackgroundBrush, this.backFill.Bounds);
              pen.Color = this.NavPaneItem.HighlightBackgroundBorder;
              this.backFill.Bounds = rectangle;
              e.Graphics.DrawRectangle(pen, this.backFill.Bounds);
              break;
            case ControlState.Pressed:
              e.Graphics.FillRectangle(this.NavPaneItem.SelectedBackgroundBrush, this.backFill.Bounds);
              pen.Color = this.NavPaneItem.SelectedBackgroundBorder;
              this.backFill.Bounds = rectangle;
              e.Graphics.DrawRectangle(pen, this.backFill.Bounds);
              break;
            case ControlState.Disabled:
              e.Graphics.FillRectangle(this.NavPaneItem.DisabledBackgroundBrush, this.backFill.Bounds);
              pen.Color = this.NavPaneItem.DisabledBackgroundBorder;
              this.backFill.Bounds = rectangle;
              e.Graphics.DrawRectangle(pen, this.backFill.Bounds);
              break;
            default:
              e.Graphics.FillRectangle(this.NavPaneItem.BackgroundBrush, this.backFill.Bounds);
              this.backFill.Bounds = rectangle;
              e.Graphics.DrawRectangle(pen, this.backFill.Bounds);
              break;
          }
        }
      }
      else if (this.IsOffice2010() && this.Enabled)
      {
        this.backFill.Bounds = this.ClientRectangle;
        this.backFill.DrawElementFill(e.Graphics, this.state);
        this.backFill.Radius = 0;
        Rectangle rectangle1 = new Rectangle(0, 0, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
        this.backFill.Bounds = rectangle1;
        this.backFill.DrawElementBorder(e.Graphics, ControlState.Normal);
        Color color1 = this.NavPaneItem.NavPane.ItemsTheme.QueryColorSetter("NavInnerHoverBorder");
        Color color2 = this.NavPaneItem.NavPane.ItemsTheme.QueryColorSetter("NavOuterHoverBorder");
        if (color1 != Color.Empty && this.state == ControlState.Hover)
        {
          rectangle1.Inflate(-1, -1);
          this.backFill.Bounds = rectangle1;
          this.backFill.DrawElementBorder(e.Graphics, ControlState.Hover, color2);
          rectangle1.Inflate(-1, -1);
          this.backFill.Bounds = rectangle1;
          this.backFill.DrawElementBorder(e.Graphics, ControlState.Hover, color1);
          rectangle1.Inflate(2, 2);
          this.backFill.Bounds = rectangle1;
        }
        Color color3 = this.NavPaneItem.NavPane.ItemsTheme.QueryColorSetter("NavInnerPressedBorder");
        Color color4 = this.NavPaneItem.NavPane.ItemsTheme.QueryColorSetter("NavOuterPressedBorder");
        if (color3 != Color.Empty && this.state == ControlState.Pressed)
        {
          rectangle1.Inflate(-1, -1);
          this.backFill.Bounds = rectangle1;
          this.backFill.DrawElementBorder(e.Graphics, ControlState.Hover, color4);
          rectangle1.Inflate(-1, -1);
          this.backFill.Bounds = rectangle1;
          this.backFill.DrawElementBorder(e.Graphics, ControlState.Pressed, color3);
          rectangle1.Inflate(2, 2);
          this.backFill.Bounds = rectangle1;
        }
        this.backFill.SetLastState(this.state);
        if (this.state == ControlState.Hover || this.state == ControlState.Pressed)
        {
          Rectangle rectangle2 = new Rectangle(0, this.Bounds.Height / 2, this.Width / 2, this.Bounds.Height / 2);
          int offsetY = this.Bounds.Height / 4;
          int offsetX = this.Bounds.Width / 4;
          Color[] surroundColors = new Color[1]{ Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue) };
          this.helper.DrawWin7Glow(e.Graphics, rectangle2, offsetX, offsetY, Color.FromArgb(50, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), surroundColors);
        }
      }
      else
        this.DrawDefaultBackground(e);
      Rectangle textBounds = new Rectangle(this.backFill.Bounds.X + 5, this.backFill.Bounds.Y, this.backFill.Bounds.Width - 10, this.backFill.Bounds.Height);
      if (this.backFill.Font != this.Font)
      {
        this.backFill.Font = this.Font;
        this.backFill.PressedFont = this.Font;
        this.backFill.HighLightFont = this.Font;
      }
      if (!this.Enabled)
        this.state = ControlState.Disabled;
      this.backFill.TextAlignment = this.NavPaneItem.HeaderTextAlignment;
      this.backFill.ImageAlignment = this.NavPaneItem.HeaderImageAlignment;
      if (!this.enableStates && this.NavPaneItem != null && (this.NavPaneItem.NavPane != null && !this.NavPaneItem.NavPane.UseHeaderThemeTextColor))
      {
        this.backFill.TextAlignment = this.NavPaneItem.NavPane.HeaderTextAlignment;
        Color headerTextColor = this.NavPaneItem.NavPane.HeaderTextColor;
        this.backFill.DrawElementTextAndImage(e.Graphics, this.state, this.Text, this.item.LargeImage, textBounds, this.NavPaneItem.HeaderTextImageRelation, headerTextColor);
      }
      else if (!this.NavPaneItem.UseThemeTextColor)
      {
        Color color = this.ForeColor;
        switch (this.state)
        {
          case ControlState.Normal:
          case ControlState.Default:
            color = this.NavPaneItem.TextColor;
            break;
          case ControlState.Hover:
            color = this.NavPaneItem.HighlightTextColor;
            break;
          case ControlState.Pressed:
            color = this.NavPaneItem.SelectedTextColor;
            break;
          case ControlState.Disabled:
            color = this.NavPaneItem.DisabledTextColor;
            break;
        }
        this.backFill.DrawElementTextAndImage(e.Graphics, this.state, this.Text, this.item.LargeImage, textBounds, this.NavPaneItem.HeaderTextImageRelation, color);
      }
      else
        this.backFill.DrawElementTextAndImage(e.Graphics, this.state, this.Text, this.item.LargeImage, textBounds, this.NavPaneItem.HeaderTextImageRelation);
    }

    private void DrawDefaultBackground(PaintEventArgs e)
    {
      this.backFill.Bounds = this.ClientRectangle;
      this.backFill.DrawElementFill(e.Graphics, this.state);
      this.backFill.Radius = 0;
      this.backFill.Bounds = new Rectangle(0, 0, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
      this.backFill.DrawElementBorder(e.Graphics, ControlState.Normal);
      this.backFill.SetLastState(this.state);
    }
  }
}
