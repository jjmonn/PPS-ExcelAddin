// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vGroupBox
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>
  /// Represents a vGroupBox control which displays a frame around other controls, and allows the user to expand/collapse the entire group.
  /// </summary>
  [Description("Reperesent a vGroupBox control which displays a frame around other controls. The group box supports expand/collapse states.")]
  [Designer("VIBlend.WinForms.Controls.Design.vDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxBitmap(typeof (vGroupBox), "ControlIcons.GroupBox.ico")]
  [ToolboxItem(true)]
  public class vGroupBox : GroupBox, IScrollableControlBase
  {
    private int collapseHeight = 35;
    private Timer timer = new Timer();
    private int duration = 10;
    private Stack<int> animValues = new Stack<int>();
    private int borderRadius = 3;
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private bool useTitleBackColor = true;
    private bool useThemeBorderColor = true;
    private Color paintBorderColor = Color.Transparent;
    private bool enableCollapse;
    private bool collapsed;
    private bool isOnCollapseIndicator;
    private int wantedHeight;
    private int step;
    private Size initialSize;
    private bool enableToggleAnimation;
    private AnimationManager animManager;
    private ControlTheme theme;
    private BackgroundElement backGround;
    private Color titleBackColor;
    private bool useThemeTextColor;

    /// <summary>
    /// Gets or sets a value indicating whether toggle animation is enabled
    /// </summary>
    /// <value>
    /// 	<c>true</c> if true the animation is enabled; otherwise, <c>false</c>.
    /// </value>
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether  toggle animation is enabled")]
    [Category("Behavior")]
    public bool EnableToggleAnimation
    {
      get
      {
        return this.enableToggleAnimation;
      }
      set
      {
        this.enableToggleAnimation = value;
        this.Invalidate();
      }
    }

    /// <exclude />
    [Browsable(false)]
    public AnimationManager AnimationManager
    {
      get
      {
        if (this.animManager == null)
          this.animManager = new AnimationManager((Control) this);
        return this.animManager;
      }
    }

    /// <summary>Gets or sets whether the control is animated</summary>
    [DefaultValue(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public bool AllowAnimations
    {
      get
      {
        if (this.backGround != null)
          return this.backGround.IsAnimated;
        return true;
      }
      set
      {
        if (this.backGround == null)
          return;
        this.backGround.IsAnimated = value;
      }
    }

    /// <summary>Gets or sets the theme of the control.</summary>
    [Description("Gets or sets the theme of the control.")]
    [Category("Appearance")]
    [Browsable(false)]
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
        this.theme.Radius = (float) this.BorderRadius;
        this.backGround = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
        this.backGround.LoadTheme(this.theme);
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the border radius.</summary>
    /// <value>The border radius.</value>
    [Description("Gets or sets the BorderRadius of the GroupBox.  The Maximum value is 10.")]
    [DefaultValue(3)]
    [Category("Appearance")]
    public int BorderRadius
    {
      get
      {
        return this.borderRadius;
      }
      set
      {
        if (value == this.borderRadius || value < 0 || value > 10)
          return;
        this.borderRadius = value;
        if (this.backGround != null && this.theme != null)
        {
          this.theme.Radius = (float) this.borderRadius;
          this.backGround.Radius = this.borderRadius;
        }
        this.Refresh();
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
    /// Gets or sets a value indicating whether group box is collapsible
    /// </summary>
    [DefaultValue(false)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether group box is collapsible")]
    public bool EnableGroupBoxCollapse
    {
      get
      {
        return this.enableCollapse;
      }
      set
      {
        this.enableCollapse = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether group box should be collapsed
    /// </summary>
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether group box should be collapsed")]
    [Category("Behavior")]
    public bool CollapseGroupBox
    {
      get
      {
        return this.collapsed;
      }
      set
      {
        if (!this.EnableGroupBoxCollapse)
          return;
        if (this.collapsed != value)
        {
          this.collapsed = value;
          if (value)
            this.Collapse();
          else
            this.Expand();
          this.Invalidate();
        }
        if (this.CollapseGroupBox)
        {
          this.wantedHeight = this.CollapsedHeight;
          if (this.Height != this.CollapsedHeight)
            this.initialSize = this.Size;
          if (!this.EnableToggleAnimation)
            this.Height = this.wantedHeight;
        }
        else
        {
          this.wantedHeight = this.initialSize.Height;
          if (!this.EnableToggleAnimation)
            this.Height = this.wantedHeight;
        }
        if (!this.EnableToggleAnimation)
          return;
        this.Animate();
      }
    }

    /// <summary>Gets or sets the background color for the control.</summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.
    /// </returns>
    /// <PermissionSet>
    /// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public override Color BackColor
    {
      get
      {
        return base.BackColor;
      }
      set
      {
        base.BackColor = value;
        this.TitleBackColor = value;
      }
    }

    /// <summary>Gets or sets the title backcolor</summary>
    [Description("Gets or sets the title backcolor")]
    [Category("Appearance")]
    [DefaultValue(typeof (Color), "Transparent")]
    public Color TitleBackColor
    {
      get
      {
        return this.titleBackColor;
      }
      set
      {
        this.titleBackColor = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Determines whether to use the title back color to draw the text's fill
    /// </summary>
    [DefaultValue(true)]
    [Category("Appearance")]
    [Description("Determines whether to use the title back color to draw the text's fill")]
    public bool UseTitleBackColor
    {
      get
      {
        return this.useTitleBackColor;
      }
      set
      {
        this.useTitleBackColor = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Determines whether to use the text color from the Theme or a custom value
    /// </summary>
    [DefaultValue(false)]
    [Category("Appearance")]
    [Description("Determines whether to use the text color from the Theme or a custom value")]
    public bool UseThemeTextColor
    {
      get
      {
        return this.useThemeTextColor;
      }
      set
      {
        this.useThemeTextColor = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use the BorderColor
    /// </summary>
    [Description("Gets or sets a value indicating whether to use the BorderColor")]
    [DefaultValue(false)]
    [Category("Behavior")]
    public virtual bool UseThemeBorderColor
    {
      get
      {
        return this.useThemeBorderColor;
      }
      set
      {
        this.useThemeBorderColor = value;
        this.Refresh();
      }
    }

    /// <summary>Gets or sets the BorderColor of the GroupBox.</summary>
    [Category("Appearance")]
    [DefaultValue(typeof (Color), "Transparent")]
    [Description("Gets or sets the BorderColor of the GroupBox.")]
    public virtual Color BorderColor
    {
      get
      {
        return this.paintBorderColor;
      }
      set
      {
        this.paintBorderColor = value;
        this.Refresh();
      }
    }

    /// <summary>
    /// Gets or sets the Height of the vGroupBox control when the control is in a collapsed state.
    /// </summary>
    [Description("Gets or sets the Height of the vGroupBox control when the control is in a collapsed state.")]
    [DefaultValue(35)]
    [Category("Behavior")]
    [Browsable(false)]
    public int CollapsedHeight
    {
      get
      {
        return this.collapseHeight;
      }
      set
      {
        this.collapseHeight = value;
      }
    }

    /// <summary>Gets or sets the header text of the vGroupBox control</summary>
    [Browsable(true)]
    public override string Text
    {
      get
      {
        return base.Text;
      }
      set
      {
        base.Text = value;
        this.Invalidate();
      }
    }

    /// <summary>Occurs when group box is expanded/collapsed</summary>
    [Category("Action")]
    public event EventHandler CollapsedChanged;

    static vGroupBox()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vGroupBox" /> class.
    /// </summary>
    public vGroupBox()
    {
      this.SetStyle(ControlStyles.ResizeRedraw, true);
      this.SetStyle(ControlStyles.DoubleBuffer, true);
      this.SetStyle(ControlStyles.Selectable, false);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.SetStyle(ControlStyles.Opaque, false);
      this.BackColor = Color.Transparent;
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
      this.timer.Interval = 15;
      this.timer.Tick += new EventHandler(this.timer_Tick);
    }

    private void IsMouseOver()
    {
      bool flag = this.isOnCollapseIndicator;
      this.isOnCollapseIndicator = this.ShouldCollapse();
      if (flag == this.isOnCollapseIndicator || !this.EnableGroupBoxCollapse)
        return;
      this.DrawCollapse();
    }

    private void DrawCollapse()
    {
      using (Graphics graphics = this.CreateGraphics())
        this.DrawCollapse(graphics);
    }

    private void DrawCollapse(Graphics g)
    {
      Color borderColor = this.theme.StyleNormal.BorderColor;
      Pen pen1 = new Pen(borderColor);
      Rectangle indicatorBounds = this.GetIndicatorBounds();
      this.backGround.Bounds = indicatorBounds;
      Pen pen2;
      if (!this.isOnCollapseIndicator)
      {
        this.backGround.DrawStandardFill(g, ControlState.Normal, GradientStyles.Linear);
        this.backGround.DrawElementBorder(g, ControlState.Normal);
        pen2 = new Pen(borderColor);
      }
      else
      {
        this.backGround.DrawStandardFill(g, ControlState.Hover, GradientStyles.Linear);
        this.backGround.DrawElementBorder(g, ControlState.Hover);
        pen2 = new Pen(this.theme.StyleHighlight.BorderColor);
      }
      if (this.Theme != null)
      {
        Color color = this.Theme.QueryColorSetter("CheckMarkColor");
        if (!color.IsEmpty)
          pen2 = new Pen(color);
      }
      g.DrawLine(pen2, indicatorBounds.X + 2, indicatorBounds.Y + indicatorBounds.Height / 2, indicatorBounds.Right - 2, indicatorBounds.Y + indicatorBounds.Height / 2);
      if (!this.CollapseGroupBox)
        return;
      g.DrawLine(pen2, indicatorBounds.X + indicatorBounds.Width / 2, indicatorBounds.Y + 2, indicatorBounds.X + indicatorBounds.Width / 2, indicatorBounds.Bottom - 2);
    }

    private Rectangle GetIndicatorBounds()
    {
      if (this.RightToLeft == RightToLeft.Yes)
        return new Rectangle(this.Width - 17, 1, 10, 10);
      return new Rectangle(7, 1, 10, 10);
    }

    private bool ShouldCollapse()
    {
      if (this.DesignMode)
        return false;
      return this.GetIndicatorBounds().Contains(this.PointToClient(Cursor.Position));
    }

    private void CallCollapsedChanged(EventArgs e)
    {
      this.OnCollapsedChanged(e);
    }

    /// <summary>Raises the CollapsedChanged event</summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnCollapsedChanged(EventArgs e)
    {
      if (this.CollapsedChanged == null)
        return;
      this.CollapsedChanged((object) this, e);
    }

    /// <summary>Raises the ControlAdded event</summary>
    /// <param name="e">A ControlEventArgs that contains the event data.</param>
    protected override void OnControlAdded(ControlEventArgs e)
    {
      base.OnControlAdded(e);
      e.Control.Visible = !this.CollapseGroupBox;
    }

    /// <summary>Raises the OnMouseDown event</summary>
    /// <param name="e">An MouseEventArgs that contains the event data.</param>
    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      Point client = this.PointToClient(Cursor.Position);
      if (client.Y < 0 || client.Y > 12 || !this.EnableGroupBoxCollapse)
        return;
      this.CollapseGroupBox = !this.CollapseGroupBox;
    }

    /// <summary>Raises the OnMouseEnter event</summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      this.IsMouseOver();
    }

    /// <summary>Raises the OnMouseLeave event</summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.IsMouseOver();
    }

    /// <summary>Raises the OnMouseMove event</summary>
    /// <param name="e">An MouseEventArgs that contains the event data.</param>
    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      this.IsMouseOver();
    }

    private void Animate()
    {
      this.timer.Start();
    }

    /// <summary>Clean up any resources being used.</summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
        this.timer.Dispose();
      base.Dispose(disposing);
    }

    private void timer_Tick(object sender, EventArgs e)
    {
      double num = (double) (this.step * this.step / this.duration);
      this.step += 2;
      if (!this.CollapseGroupBox)
      {
        if (this.animValues.Count > 0)
        {
          this.Size = new Size(this.Width, this.animValues.Pop());
        }
        else
        {
          this.timer.Stop();
          this.Size = new Size(this.Width, this.initialSize.Height);
          this.step = 0;
        }
      }
      else if (num < (double) (this.initialSize.Height - this.wantedHeight))
      {
        int height = this.initialSize.Height - (int) num;
        this.Size = new Size(this.Width, height);
        this.animValues.Push(height);
      }
      else
      {
        this.Size = new Size(this.Width, this.wantedHeight);
        this.timer.Stop();
        this.step = 0;
      }
    }

    /// <summary>
    /// Raises the <see cref="E:Paint" /> event.
    /// </summary>
    /// <param name="p">The <see cref="T:System.Windows.Forms.PaintEventArgs" /> instance containing the event data.</param>
    protected override void OnPaint(PaintEventArgs p)
    {
      if (!this.DesignMode)
        Licensing.LICheck((Control) this);
      Graphics graphics = p.Graphics;
      int num = 8;
      if (this.EnableGroupBoxCollapse)
        num = 24;
      Rectangle clientRectangle = this.ClientRectangle;
      Rectangle rect1 = clientRectangle;
      SizeF sizeF = graphics.MeasureString(this.Text, this.Font);
      rect1.Width = (int) sizeF.Width + 4;
      rect1.Height = (int) sizeF.Height;
      if (this.RightToLeft == RightToLeft.Yes)
        rect1.X = clientRectangle.Right - rect1.Width - num;
      else
        rect1.X += num;
      if (this.Text.Length == 0)
        rect1.Width = 0;
      Color textColor = this.Theme.StyleDisabled.TextColor;
      int y = clientRectangle.Top + this.Font.Height / 2;
      if (this.Text.Length == 0)
        y = 0;
      using (new Pen(ControlPaint.Light(this.Theme.StyleNormal.BorderColor, 1f)))
      {
        using (new Pen(ControlPaint.Dark(this.Theme.StyleNormal.BorderColor, 0.0f)))
        {
          Rectangle bounds = new Rectangle(clientRectangle.Left + 1, y, clientRectangle.Width - 3, clientRectangle.Height - 16);
          new PaintHelper().GetRoundedPathRect(bounds, (int) this.Theme.Radius);
          this.backGround.Bounds = bounds;
          p.Graphics.Clip = new Region(this.ClientRectangle);
          p.Graphics.ExcludeClip(rect1);
          if (this.UseThemeBorderColor)
          {
            Color color = this.Theme.QueryColorSetter("GroupBoxBorder");
            if (!color.IsEmpty)
              this.backGround.DrawElementBorder(p.Graphics, ControlState.Normal, color);
            else
              this.backGround.DrawElementBorder(p.Graphics, ControlState.Normal);
          }
          else
            this.backGround.DrawElementBorder(p.Graphics, ControlState.Normal, this.BorderColor);
          p.Graphics.ResetClip();
          Rectangle rect2 = new Rectangle(rect1.X + 3, 0, rect1.Width - 2, 16);
          using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
            p.Graphics.FillRectangle((Brush) solidBrush, rect2);
          if (this.UseTitleBackColor)
          {
            using (SolidBrush solidBrush = new SolidBrush(this.TitleBackColor))
              p.Graphics.FillRectangle((Brush) solidBrush, rect2);
          }
        }
      }
      this.DrawText(graphics, ref rect1, ref textColor);
      if (!this.EnableGroupBoxCollapse)
        return;
      this.DrawCollapse(graphics);
    }

    /// <summary>Draws the text.</summary>
    /// <param name="g">The g.</param>
    /// <param name="rect">The rect.</param>
    /// <param name="disabledColor">Color of the disabled.</param>
    protected virtual void DrawText(Graphics g, ref Rectangle rect, ref Color disabledColor)
    {
      using (StringFormat format = new StringFormat())
      {
        format.FormatFlags = StringFormatFlags.NoWrap;
        format.Trimming = StringTrimming.EllipsisCharacter;
        format.Alignment = StringAlignment.Center;
        format.HotkeyPrefix = HotkeyPrefix.Show;
        if (this.RightToLeft == RightToLeft.Yes)
          format.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
        g.MeasureString(this.Text, this.Font);
        if (this.Enabled)
        {
          using (SolidBrush solidBrush = new SolidBrush(this.ForeColor))
          {
            if (this.UseThemeTextColor)
            {
              solidBrush.Color = this.Theme.StyleNormal.TextColor;
              Color color = this.Theme.QueryColorSetter("GroupBoxForeColor");
              if (!color.IsEmpty)
                solidBrush.Color = color;
            }
            Rectangle rectangle = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
            if (rect.Right > this.Width)
              rect.Width = this.Width - rect.X;
            g.DrawString(this.Text, this.Font, (Brush) solidBrush, (RectangleF) rect, format);
          }
        }
        else
          ControlPaint.DrawStringDisabled(g, this.Text, this.Font, disabledColor, (RectangleF) rect, format);
      }
    }

    private void Collapse()
    {
      foreach (Control control in (ArrangedElementCollection) this.Controls)
        control.Visible = false;
      this.CallCollapsedChanged(EventArgs.Empty);
    }

    private void Expand()
    {
      foreach (Control control in (ArrangedElementCollection) this.Controls)
        control.Visible = true;
      this.CallCollapsedChanged(EventArgs.Empty);
    }
  }
}
