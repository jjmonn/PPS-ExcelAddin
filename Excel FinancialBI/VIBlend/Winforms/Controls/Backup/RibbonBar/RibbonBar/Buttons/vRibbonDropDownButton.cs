// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vRibbonDropDownButton
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <exclude />
  [ToolboxItem(false)]
  [Designer("VIBlend.WinForms.Controls.Design.vDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  public class vRibbonDropDownButton : ScrollableControlMiniBase
  {
    private bool drawScrollButtonsBorder = true;
    private PaintHelper helper = new PaintHelper();
    private RibbonPaintHelper ribbonPaintHelper = new RibbonPaintHelper();
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    protected BackgroundElement scrollElement;
    protected BackgroundElement button;
    private int scrollButtonsRoundedCornersRadius;
    private ControlTheme theme;
    private ControlTheme themeButton;
    private ControlTheme themeScrollElement;
    private ControlState buttonState;

    /// <summary>
    /// Gets a value indicating whether the mouse cursor is over.
    /// </summary>
    /// <value><c>true</c> if [mouse hover]; otherwise, <c>false</c>.</value>
    protected bool MouseHover
    {
      get
      {
        return this.ClientRectangle.Contains(this.PointToClient(Cursor.Position));
      }
    }

    /// <summary>Gets or sets the theme of the control.</summary>
    [Browsable(false)]
    [Category("Appearance")]
    [Description("Gets or sets the theme of the control.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ControlTheme Theme
    {
      get
      {
        return this.theme;
      }
      set
      {
        if (value == null)
          return;
        this.theme = value.CreateCopy();
        this.themeButton = value.CreateCopy();
        this.themeScrollElement = value.CreateCopy();
        this.button.LoadTheme(this.themeButton);
        this.AllowAnimations = true;
        this.button.IsAnimated = true;
        this.button.Radius = this.scrollButtonsRoundedCornersRadius;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the theme of the control using one of the built-in themes.
    /// </summary>
    [Category("Appearance")]
    public VIBLEND_THEME VIBlendTheme
    {
      get
      {
        return this.defaultTheme;
      }
      set
      {
        this.defaultTheme = value;
        ControlTheme defaultTheme;
        try
        {
          defaultTheme = ControlTheme.GetDefaultTheme(this.defaultTheme);
        }
        catch (Exception ex)
        {
          Trace.WriteLine(ex.Message);
          return;
        }
        if (defaultTheme == null)
          return;
        this.Theme = defaultTheme;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vRibbonDropDownButton" /> class.
    /// </summary>
    public vRibbonDropDownButton()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.CreateScrollElements();
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
      this.AllowAnimations = true;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (this.ClientRectangle.Contains(e.Location))
      {
        if (e.Button == MouseButtons.None)
          this.buttonState = ControlState.Hover;
      }
      else
        this.buttonState = ControlState.Normal;
      this.Invalidate();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.buttonState = ControlState.Normal;
      this.Invalidate();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseUp(MouseEventArgs e)
    {
      this.Capture = false;
      base.OnMouseUp(e);
      this.buttonState = !this.ClientRectangle.Contains(e.Location) ? ControlState.Normal : ControlState.Hover;
      this.Invalidate();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnLostFocus(EventArgs e)
    {
      base.OnLostFocus(e);
      this.Invalidate();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      this.Capture = true;
      if (this.buttonState == ControlState.Pressed)
        return;
      this.buttonState = ControlState.Pressed;
      this.Invalidate();
    }

    /// <summary>Creates the scroll elements.</summary>
    protected void CreateScrollElements()
    {
      if (this.button == null)
      {
        this.button = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
        this.button.Owner = "ScrollButton";
      }
      this.button.HostingControl = (IScrollableControlBase) this;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
      if (!this.DesignMode)
        Licensing.LICheck((Control) this);
      base.OnPaint(e);
      if (this.ClientRectangle.Width == 0 || (this.ClientRectangle.Height == 0 || this.button == null))
        return;
      if (!this.Enabled)
        this.buttonState = ControlState.Disabled;
      Rectangle rectangle = new Rectangle(0, 0, this.Width, this.Height);
      --rectangle.Width;
      --rectangle.Height;
      this.button.Bounds = rectangle;
      this.button.DrawElementFill(e.Graphics, this.buttonState);
      if (this.drawScrollButtonsBorder || this.buttonState != ControlState.Normal)
        this.button.DrawElementBorder(e.Graphics, this.buttonState);
      Color baseColor = this.button.BorderColor;
      if (this.buttonState == ControlState.Pressed)
        baseColor = this.button.PressedBorderColor;
      if (this.buttonState == ControlState.Hover)
        baseColor = this.button.HighlightBorderColor;
      this.ribbonPaintHelper.GetArrowSize();
      Color arrowColor = ControlPaint.Dark(baseColor);
      this.ribbonPaintHelper.DrawRibbonDropDownButtonArrow(e.Graphics, this.ClientRectangle, arrowColor);
    }
  }
}
