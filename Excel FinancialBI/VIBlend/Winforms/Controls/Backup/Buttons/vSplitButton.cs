// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vSplitButton
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vSplitButton control</summary>
  /// <remarks>
  /// A vSplitButton is similar to a normal button, and in addition it contains a built-in drop-down menu.
  /// </remarks>
  [Description("Displays a button with a built-in drop-down menu.")]
  [Designer("VIBlend.WinForms.Controls.Design.vDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxBitmap(typeof (vSplitButton), "ControlIcons.SplitButton.ico")]
  [ToolboxItem(true)]
  public class vSplitButton : vButton, IScrollableControlBase
  {
    private ContextMenuStrip contextMenu = new ContextMenuStrip();
    private int dropDownBtnWidth = 16;
    private bool paintDefaultFill = true;
    private bool paintDefaultBorder = true;
    private bool autoSyncDropDownMenu = true;
    private BackgroundElement dropDownBtnElement;
    private ControlState dropDownBtnState;
    private ControlState contentAreaState;
    private bool arrowPositionDirection;
    private bool drawArrowOnly;

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override bool PaintDefaultBorder
    {
      get
      {
        return base.PaintDefaultBorder;
      }
      set
      {
        base.PaintDefaultBorder = value;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override bool PaintDefaultFill
    {
      get
      {
        return base.PaintDefaultFill;
      }
      set
      {
        base.PaintDefaultFill = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to paint a background
    /// </summary>
    /// <value><c>true</c> if [paint fill]; otherwise, <c>false</c>.</value>
    [DefaultValue(true)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether to paint a background")]
    public bool PaintDefaultStateFill
    {
      get
      {
        return this.paintDefaultFill;
      }
      set
      {
        this.paintDefaultFill = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to paint a border
    /// </summary>
    /// <value><c>true</c> if [paint border]; otherwise, <c>false</c>.</value>
    [Description("Gets or sets a value indicating whether to paint a border")]
    [Category("Behavior")]
    [DefaultValue(true)]
    public bool PaintDefaultStateBorder
    {
      get
      {
        return this.paintDefaultBorder;
      }
      set
      {
        this.paintDefaultBorder = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the arrow position.</summary>
    /// <value>The arrow position.</value>
    [Category("Behavior")]
    [Description("Gets or sets the arrow position.")]
    [DefaultValue(false)]
    public bool ArrowUnderContent
    {
      get
      {
        return this.arrowPositionDirection;
      }
      set
      {
        this.arrowPositionDirection = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the theme of the control</summary>
    /// <value></value>
    [Category("Appearance")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets the theme of the control.")]
    public override ControlTheme Theme
    {
      get
      {
        return base.Theme;
      }
      set
      {
        if (this.dropDownBtnElement == null)
          this.dropDownBtnElement = new BackgroundElement(this.ClientRectangle, (IScrollableControlBase) this);
        if (value != null)
          this.dropDownBtnElement.LoadTheme(value.CreateCopy());
        base.Theme = value;
      }
    }

    /// <summary>Gets or sets  button's text alignment</summary>
    [Description("Gets or sets the border style of the button")]
    [DefaultValue(false)]
    [Category("Appearance")]
    public bool DrawArrowOnly
    {
      get
      {
        return this.drawArrowOnly;
      }
      set
      {
        this.drawArrowOnly = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the width of the drop down button.</summary>
    /// <value>The width of the drop down button.</value>
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets the width of the drop down button.")]
    [DefaultValue(16)]
    [Category("Behavior")]
    public virtual int DropDownButtonWidth
    {
      get
      {
        return this.dropDownBtnWidth;
      }
      set
      {
        if (value < 4)
          value = 4;
        else if (value > this.Width - 10)
          value = this.Width - 10;
        this.dropDownBtnWidth = value;
        this.Invalidate();
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new ContextMenuStrip ContextMenuStrip
    {
      get
      {
        return base.ContextMenuStrip;
      }
      set
      {
        base.ContextMenuStrip = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public new ContextMenu ContextMenu
    {
      get
      {
        return base.ContextMenu;
      }
      set
      {
        base.ContextMenu = value;
      }
    }

    /// <summary>
    /// Gets or sets the drop down menu associated with the control
    /// </summary>
    [Browsable(false)]
    [Description("Gets or sets the drop down menu associated with the control")]
    [Category("Behavior")]
    public ContextMenuStrip DropDownMenu
    {
      get
      {
        return this.contextMenu;
      }
      set
      {
        this.contextMenu = value;
        vStripsRenderer vStripsRenderer = new vStripsRenderer() { Theme = this.Theme, RenderedContextMenuStrip = this.contextMenu };
        if (this.contextMenu == null || !this.AutoSyncDropDownMenu)
          return;
        foreach (ToolStripItem toolStripItem in (ArrangedElementCollection) this.contextMenu.Items)
          toolStripItem.Click += new EventHandler(this.contextMenuItem_Click);
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to auto-sync the DropDownMenu. When the property is set to true, the clicked item's text is in sync with the button's text.
    /// </summary>
    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether to auto-sync the DropDownMenu. When the property is set to true, the clicked item's text is in sync with the button's text.")]
    public bool AutoSyncDropDownMenu
    {
      get
      {
        return this.autoSyncDropDownMenu;
      }
      set
      {
        this.autoSyncDropDownMenu = value;
      }
    }

    /// <summary>Occurs when the action button is clicked.</summary>
    [Description("Occurs when the action button is clicked.")]
    [Category("Action")]
    public event EventHandler ActionButtonClick;

    /// <summary>Occurs when the arrow button is clicked.</summary>
    [Description("Occurs when the arrow button is clicked.")]
    [Category("Action")]
    public event EventHandler ArrowButtonClick;

    static vSplitButton()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vSplitButton" /> class.
    /// </summary>
    public vSplitButton()
    {
      this.StyleKey = "SplitButton";
      this.dropDownBtnElement = new BackgroundElement(this.ClientRectangle, (IScrollableControlBase) this);
      this.Size = new Size(100, 30);
      this.SetStyle(ControlStyles.ResizeRedraw, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.SetStyle(ControlStyles.Opaque, false);
      this.BackColor = Color.Transparent;
      this.dropDownBtnElement.LoadTheme(this.Theme.CreateCopy());
    }

    /// <summary>Called when the button is clicked.</summary>
    protected virtual void OnActionButtonClick()
    {
      if (this.ActionButtonClick == null)
        return;
      this.ActionButtonClick((object) this, EventArgs.Empty);
    }

    /// <summary>Called when the arrow button is clicked.</summary>
    protected virtual void OnArrowButtonClick()
    {
      if (this.ArrowButtonClick == null)
        return;
      this.ArrowButtonClick((object) this, EventArgs.Empty);
    }

    /// <summary>Raises the MouseMove event</summary>
    /// <param name="args">A MouseEventArgs that contains the event data.</param>
    protected override void OnMouseMove(MouseEventArgs args)
    {
      if (this.contentAreaState != ControlState.Pressed && this.contentAreaState != ControlState.Disabled)
        this.contentAreaState = ControlState.Normal;
      if (this.dropDownBtnState != ControlState.Pressed && this.dropDownBtnState != ControlState.Disabled)
        this.dropDownBtnState = ControlState.Normal;
      if (this.GetDropDownRect().Contains(args.Location) || this.drawArrowOnly && this.ClientRectangle.Contains(args.Location))
      {
        if (this.dropDownBtnState != ControlState.Pressed && this.dropDownBtnState != ControlState.Disabled)
          this.dropDownBtnState = ControlState.Hover;
      }
      else if (this.GetContentRect().Contains(args.Location) && this.contentAreaState != ControlState.Pressed && this.contentAreaState != ControlState.Disabled)
        this.contentAreaState = ControlState.Hover;
      this.Invalidate();
    }

    /// <summary>Raises the MouseLeave event.</summary>
    /// <param name="e">A EventArgs that contains the event data.</param>
    protected override void OnMouseLeave(EventArgs e)
    {
      this.contentAreaState = ControlState.Normal;
      this.dropDownBtnState = ControlState.Normal;
      this.Invalidate();
      base.OnMouseLeave(e);
    }

    /// <summary>Raises the MouseDown event.</summary>
    /// <param name="e">A MouseEventArgs that contains the event data.</param>
    protected override void OnMouseDown(MouseEventArgs args)
    {
      this.Capture = true;
      if (this.GetDropDownRect().Contains(args.Location) || this.drawArrowOnly && this.ClientRectangle.Contains(args.Location))
      {
        this.dropDownBtnState = ControlState.Pressed;
        vStripsRenderer vStripsRenderer = new vStripsRenderer() { VIBlendTheme = this.VIBlendTheme, RenderedContextMenuStrip = this.DropDownMenu };
        if (this.DropDownMenu != null)
          this.DropDownMenu.Show((Control) this, new Point(this.ClientRectangle.X, this.ClientRectangle.Y + this.ClientRectangle.Height));
        this.Invalidate();
        this.OnArrowButtonClick();
      }
      else if (this.GetContentRect().Contains(args.Location))
      {
        this.contentAreaState = ControlState.Pressed;
        this.Invalidate();
        this.OnActionButtonClick();
      }
      base.OnMouseDown(args);
    }

    /// <summary>Raises the MouseUp event.</summary>
    /// <param name="e">A MouseEventArgs that contains the event data.</param>
    protected override void OnMouseUp(MouseEventArgs e)
    {
      this.Capture = false;
      this.contentAreaState = !this.GetContentRect().Contains(e.Location) ? ControlState.Normal : ControlState.Hover;
      this.dropDownBtnState = !this.GetDropDownRect().Contains(e.Location) ? ControlState.Normal : ControlState.Hover;
      this.Invalidate();
      base.OnMouseUp(e);
    }

    private void contextMenuItem_Click(object sender, EventArgs e)
    {
      this.Text = (sender as ToolStripItem).Text;
    }

    /// <summary>Draws the border.</summary>
    /// <param name="graphics">The graphics.</param>
    /// <param name="controlState">State of the control.</param>
    /// <param name="drawRect">The draw rect.</param>
    /// <returns></returns>
    protected override Rectangle DrawBorder(Graphics graphics, ControlState controlState, Rectangle drawRect)
    {
      if (this.BorderStyle == ButtonBorderStyle.SOLID && this.PaintBorder)
      {
        if (controlState != ControlState.Normal)
          this.backFill.DrawElementBorder(graphics, controlState);
        else if (controlState == ControlState.Normal && this.PaintDefaultStateBorder)
          this.backFill.DrawElementBorder(graphics, controlState);
      }
      return drawRect;
    }

    /// <summary>Draws the control.</summary>
    /// <param name="graphics">The graphics.</param>
    /// <param name="bounds">The bounds.</param>
    /// <param name="controlState">State of the control.</param>
    /// <param name="isFocused">if set to <c>true</c> [is focused].</param>
    /// <param name="roundedCornersMask">The rounded corners mask.</param>
    protected override void DrawControl(Graphics graphics, Rectangle bounds, ControlState controlState, bool isFocused, byte roundedCornersMask)
    {
      Graphics graphics1 = graphics;
      if (bounds.Width <= 0 || bounds.Height <= 0)
      {
        base.DrawControl(graphics, bounds, controlState, isFocused, roundedCornersMask);
      }
      else
      {
        Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
        if ((double) this.opacity != 1.0)
          graphics1 = Graphics.FromImage((Image) bitmap);
        SmoothingMode smoothingMode = graphics1.SmoothingMode;
        graphics1.SmoothingMode = SmoothingMode.HighQuality;
        this.backFill.Opacity = 1f;
        Rectangle drawRect1 = bounds;
        --drawRect1.Height;
        --drawRect1.Width;
        this.backFill.Bounds = drawRect1;
        this.backFill.RoundedCornersBitmask = roundedCornersMask;
        this.backFill.Radius = this.RoundedCornersRadius;
        if (this.PaintFill)
        {
          if (controlState != ControlState.Normal)
            this.backFill.DrawElementFill(graphics1, controlState);
          else if (controlState == ControlState.Normal && this.PaintDefaultStateFill)
            this.backFill.DrawElementFill(graphics1, controlState);
        }
        Rectangle imageAndTextRect = this.GetImageAndTextRect(ref bounds, controlState);
        this.DrawImageAndText(graphics1, controlState, imageAndTextRect);
        Rectangle drawRect2 = this.DrawBorder(graphics1, controlState, drawRect1);
        this.DrawFocusRectangle(graphics1, isFocused, drawRect2, roundedCornersMask);
        graphics1.SmoothingMode = smoothingMode;
        if ((double) this.opacity == 1.0 || bitmap == null)
          return;
        PaintHelper.SetOpacityToImage(bitmap, (byte) ((double) byte.MaxValue * (double) this.opacity));
        graphics.DrawImage((Image) bitmap, 0, 0);
        graphics1.Dispose();
        bitmap.Dispose();
      }
    }

    /// <summary>Override</summary>
    /// <param name="e">PaintEventArgs</param>
    protected override void OnPaint(PaintEventArgs e)
    {
      Graphics graphics = e.Graphics;
      Bitmap bitmap = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
      if ((double) this.opacity != 1.0)
        graphics = Graphics.FromImage((Image) bitmap);
      float opacity = this.Opacity;
      this.opacity = 1f;
      this.AllowAnimations = false;
      FillStyle fillStyle1 = this.Theme.StyleHighlight.FillStyle.Clone();
      for (int index = 0; index < fillStyle1.ColorsNumber; ++index)
      {
        Color baseColor = fillStyle1.Colors[index];
        fillStyle1.Colors[index] = Color.FromArgb(200, ControlPaint.LightLight(baseColor));
      }
      bool flag1 = (this.contentAreaState == ControlState.Hover || this.contentAreaState == ControlState.Pressed) && this.dropDownBtnState == ControlState.Normal;
      bool flag2 = (this.dropDownBtnState == ControlState.Hover || this.dropDownBtnState == ControlState.Pressed) && this.contentAreaState == ControlState.Normal;
      using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
        graphics.FillRectangle((Brush) solidBrush, this.ClientRectangle);
      ButtonBorderStyle buttonBorderStyle = this.buttonBorderStyle;
      this.buttonBorderStyle = ButtonBorderStyle.NONE;
      if (!this.drawArrowOnly)
      {
        byte roundedCornersMask1 = this.RoundedCornersMask;
        byte roundedCornersMask2 = this.RightToLeft != RightToLeft.Yes ? (byte) ((uint) roundedCornersMask1 ^ 10U) : (byte) ((uint) roundedCornersMask1 ^ 5U);
        FillStyle fillStyle2 = this.Theme.StyleHighlight.FillStyle;
        ControlState controlState = this.contentAreaState;
        if (flag2)
        {
          this.Theme.StyleHighlight.FillStyle = fillStyle1;
          controlState = ControlState.Hover;
        }
        this.DrawControl(graphics, this.GetContentRect(), controlState, this.Focused, roundedCornersMask2);
        this.Theme.StyleHighlight.FillStyle = fillStyle2;
      }
      if (!this.DesignMode)
      {
        if (this.dropDownBtnElement != null)
        {
          this.dropDownBtnElement.Radius = this.RoundedCornersRadius;
          this.dropDownBtnElement.RoundedCornersBitmask = this.RoundedCornersMask;
          if (this.drawArrowOnly)
          {
            this.dropDownBtnElement.Bounds = new Rectangle(0, 0, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
          }
          else
          {
            this.dropDownBtnElement.Bounds = this.GetDropDownRect();
            if (this.RightToLeft != RightToLeft.Yes)
              this.dropDownBtnElement.RoundedCornersBitmask ^= (byte) 5;
            else
              this.dropDownBtnElement.RoundedCornersBitmask ^= (byte) 10;
          }
          FillStyle fillStyle2 = (FillStyle) null;
          ControlState controlState = this.dropDownBtnState;
          if (!this.DesignMode)
          {
            fillStyle2 = this.dropDownBtnElement.Theme.StyleHighlight.FillStyle;
            if (flag1)
            {
              this.dropDownBtnElement.Theme.StyleHighlight.FillStyle = fillStyle1;
              controlState = ControlState.Hover;
            }
          }
          if (controlState != ControlState.Normal)
            this.dropDownBtnElement.DrawElementFill(graphics, this.Enabled ? controlState : ControlState.Disabled);
          else if (this.PaintDefaultStateFill)
            this.dropDownBtnElement.DrawElementFill(graphics, this.Enabled ? controlState : ControlState.Disabled);
          if (!this.DesignMode)
            this.dropDownBtnElement.Theme.StyleHighlight.FillStyle = fillStyle2;
        }
      }
      else
      {
        this.dropDownBtnElement.Bounds = this.GetDropDownRect();
        if (this.dropDownBtnState != ControlState.Normal)
          this.dropDownBtnElement.DrawElementFill(graphics, this.Enabled ? this.dropDownBtnState : ControlState.Disabled);
        else if (this.PaintDefaultStateFill)
          this.dropDownBtnElement.DrawElementFill(graphics, this.Enabled ? this.dropDownBtnState : ControlState.Disabled);
      }
      Rectangle dropDownRect = this.GetDropDownRect();
      this.DrawArrow(graphics, dropDownRect);
      this.buttonBorderStyle = buttonBorderStyle;
      if (this.BorderStyle == ButtonBorderStyle.SOLID)
      {
        GraphicsPath partiallyRoundedPath = this.paintHelper.CreatePartiallyRoundedPath(new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1), this.RoundedCornersRadius, this.RoundedCornersMask);
        ControlState controlState = this.contentAreaState;
        if (this.dropDownBtnState == ControlState.Hover)
          controlState = ControlState.Hover;
        else if (this.dropDownBtnState == ControlState.Pressed)
          controlState = ControlState.Pressed;
        if (controlState != ControlState.Normal)
        {
          Color borderColor = this.Theme.StyleNormal.BorderColor;
          if (controlState == ControlState.Pressed)
            borderColor = this.Theme.StylePressed.BorderColor;
          else if (controlState == ControlState.Hover)
            borderColor = this.Theme.StyleHighlight.BorderColor;
          else if (!this.Enabled)
            borderColor = this.Theme.StyleDisabled.BorderColor;
          this.paintHelper.DrawPath(graphics, partiallyRoundedPath, borderColor, PenAlignment.Center, 1f);
        }
        else if (this.PaintDefaultStateBorder)
          this.paintHelper.DrawPath(graphics, partiallyRoundedPath, this.Enabled ? this.Theme.StyleNormal.BorderColor : this.Theme.StyleDisabled.BorderColor, PenAlignment.Center, 1f);
      }
      this.opacity = opacity;
      if ((double) this.opacity == 1.0 || bitmap == null)
        return;
      PaintHelper.SetOpacityToImage(bitmap, (byte) ((double) byte.MaxValue * (double) this.opacity));
      e.Graphics.DrawImage((Image) bitmap, 0, 0);
      graphics.Dispose();
      bitmap.Dispose();
    }

    public virtual void DrawArrow(Graphics g, Rectangle bounds)
    {
      Color color = this.Theme.QueryColorSetter("DropDownArrowColor");
      if (color == Color.Empty)
        color = this.backFill.BorderColor;
      if (this.contentAreaState == ControlState.Hover)
      {
        color = this.Theme.QueryColorSetter("DropDownArrowColorHighlight");
        if (color == Color.Empty)
          color = this.backFill.HighlightBorderColor;
      }
      if (this.contentAreaState == ControlState.Pressed)
      {
        color = this.theme.QueryColorSetter("DropDownArrowColorSelected");
        if (color == Color.Empty)
          color = this.theme.QueryColorSetter("DropDownArrowColorHighlight");
        if (color == Color.Empty)
          color = this.backFill.PressedBorderColor;
      }
      if (!this.Enabled)
      {
        color = this.Theme.QueryColorSetter("DropDownArrowColorDisabled");
        if (color == Color.Empty)
          color = this.backFill.DisabledBorderColor;
      }
      Rectangle bounds1 = PaintHelper.OfficeArrowRectFromBounds(bounds);
      this.paintHelper.DrawArrowFigure(g, color, bounds1, ArrowDirection.Down);
    }

    private Rectangle GetDropDownRect()
    {
      return this.GetDropDownRect(new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1), this.dropDownBtnWidth);
    }

    private Rectangle GetDropDownRect(Rectangle bounds, int buttonWidth)
    {
      Rectangle rectangle = new Rectangle();
      rectangle = !this.drawArrowOnly ? new Rectangle(bounds.Right - buttonWidth - 1, bounds.Top, buttonWidth + 1, bounds.Height) : new Rectangle((bounds.Right - buttonWidth) / 2, bounds.Top, buttonWidth, bounds.Height);
      if (this.RightToLeft != RightToLeft.No && !this.drawArrowOnly)
        rectangle.X = 0;
      if (this.ArrowUnderContent)
        rectangle = new Rectangle(bounds.X, bounds.Height - this.dropDownBtnWidth, bounds.Width, this.dropDownBtnWidth);
      return rectangle;
    }

    /// <summary>Gets the content rect.</summary>
    /// <returns></returns>
    public Rectangle GetContentRect()
    {
      return this.GetContentRect(new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width, this.ClientRectangle.Height));
    }

    private Rectangle GetContentRect(Rectangle bounds)
    {
      if (this.drawArrowOnly)
        return Rectangle.Empty;
      Rectangle rectangle = Rectangle.Empty;
      rectangle.Width = bounds.Width - this.dropDownBtnWidth;
      rectangle.Y = bounds.Y;
      rectangle.Height = bounds.Height;
      rectangle.X = this.RightToLeft != RightToLeft.Yes ? bounds.X : bounds.X + this.dropDownBtnWidth;
      if (this.ArrowUnderContent)
      {
        rectangle.Width = bounds.Width;
        rectangle.Y = bounds.Y;
        rectangle.Height = bounds.Height - this.dropDownBtnWidth;
        rectangle.X = bounds.X;
      }
      return rectangle;
    }

    /// <summary>Gets the content rectangle.</summary>
    /// <param name="button">The button.</param>
    /// <returns></returns>
    public static Rectangle GetContentRectangle(vSplitButton button)
    {
      if (button.DrawArrowOnly)
        return Rectangle.Empty;
      Rectangle rectangle = Rectangle.Empty;
      rectangle.Width = button.Width - button.DropDownButtonWidth;
      rectangle.Y = 0;
      rectangle.Height = button.Height;
      rectangle.X = button.RightToLeft != RightToLeft.Yes ? 0 : button.DropDownButtonWidth;
      if (button.ArrowUnderContent)
      {
        rectangle.Width = button.Width;
        rectangle.Y = 0;
        rectangle.Height = button.Height - button.DropDownButtonWidth;
        rectangle.X = 0;
      }
      return rectangle;
    }
  }
}
