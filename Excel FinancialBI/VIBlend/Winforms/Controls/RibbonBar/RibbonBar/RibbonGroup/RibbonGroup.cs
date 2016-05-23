// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vRibbonGroup
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
  /// <summary>Represents a vRibbonGroup control</summary>
  /// <remarks>
  /// A ribbon group control is used to group and host multiple other controls within a ribbon bar.
  /// </remarks>
  [ToolboxBitmap(typeof (vRibbonGroup), "ControlIcons.vRibbonGroup.ico")]
  [Designer("VIBlend.WinForms.Controls.Design.RibbonGroupDesigner, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxItem(true)]
  [Description("Displays a ribbon group control that is used to group and host multiple other controls within a ribbon bar.")]
  public class vRibbonGroup : Control, IScrollableControlBase
  {
    private Timer indicatorsTimer1 = new Timer();
    private ToolStripDropDown dropDown = new ToolStripDropDown();
    private bool autoCollapse = true;
    internal Timer boundsTimer = new Timer();
    private int imageIndex = -1;
    private bool allowAnimations = true;
    private string styleKey = "RibbonGroup";
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private bool canClose = true;
    private bool useThemeTextColor = true;
    private bool collapse;
    private vRibbonGroupContent groupContent;
    internal BackgroundElement backFill;
    private Size savedSize;
    private bool clicked;
    private Image image;
    private RibbonStyle buttonStyle;
    private ImageList imageList;
    private AnimationManager manager;
    private ControlTheme theme;

    /// <summary>Gets the drop down.</summary>
    /// <value>The drop down.</value>
    [Browsable(false)]
    public ToolStripDropDown DropDown
    {
      get
      {
        return this.dropDown;
      }
    }

    /// <summary>Gets or sets the auto close interval.</summary>
    /// <value>The auto close interval.</value>
    [DefaultValue(2500)]
    [Browsable(false)]
    public int AutoCloseInterval
    {
      get
      {
        return this.boundsTimer.Interval;
      }
      set
      {
        this.boundsTimer.Interval = value;
      }
    }

    /// <summary>Gets or sets the group style.</summary>
    /// <value>The group style.</value>
    [Category("Appearance")]
    [DefaultValue(typeof (RibbonStyle), "RibbonStyle.Office2007")]
    [Browsable(false)]
    public RibbonStyle GroupStyle
    {
      get
      {
        for (Control parent = this.Parent; parent != null; parent = parent.Parent)
        {
          if (parent is vRibbonBar)
          {
            this.buttonStyle = (parent as vRibbonBar).RibbonStyle;
            break;
          }
        }
        return this.buttonStyle;
      }
      set
      {
        this.buttonStyle = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the image of the Ribbon Group control</summary>
    [Category("Behavior")]
    [DefaultValue(null)]
    public Image Image
    {
      get
      {
        return this.image;
      }
      set
      {
        this.image = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the index of the image.</summary>
    /// <value>The index of the image.</value>
    [DefaultValue(-1)]
    [Category("Behavior")]
    [Description("Gets or sets the index of the image.")]
    public int ImageIndex
    {
      get
      {
        return this.imageIndex;
      }
      set
      {
        this.imageIndex = value;
        if (this.ImageList == null || value < 0 || value >= this.ImageList.Images.Count)
          return;
        this.Image = this.ImageList.Images[value];
      }
    }

    /// <summary>Gets or sets the image list.</summary>
    /// <value>The image list.</value>
    [DefaultValue(null)]
    [Category("Behavior")]
    public ImageList ImageList
    {
      get
      {
        return this.imageList;
      }
      set
      {
        if (this.imageList == value)
          return;
        if (value != null)
          this.image = (Image) null;
        this.imageList = value;
        this.Invalidate();
      }
    }

    [Browsable(false)]
    internal Size SavedSize
    {
      get
      {
        return this.savedSize;
      }
      set
      {
        this.savedSize = value;
      }
    }

    /// <summary>Gets or sets the text associated with this control.</summary>
    /// <value></value>
    /// <returns>The text associated with this control.</returns>
    [Category("Behavior")]
    [Description("Gets or sets the text associated with this control.")]
    public new string Text
    {
      get
      {
        return this.Content.Footer.Text;
      }
      set
      {
        this.Content.Footer.Text = value;
        this.Invalidate();
      }
    }

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

    [DefaultValue(true)]
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

    /// <summary>Gets or sets the style key.</summary>
    /// <value>The style key.</value>
    protected virtual string StyleKey
    {
      get
      {
        return this.styleKey;
      }
      set
      {
        this.styleKey = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets button's theme")]
    [Category("Appearance")]
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
        this.theme = value;
        this.backFill.LoadTheme(this.theme);
        ControlTheme theme = ThemeCache.GetTheme(this.StyleKey, this.VIBlendTheme);
        FillStyle fillStyle = this.theme.QueryFillStyleSetter("RibbonTabBackGround");
        if (fillStyle != null)
          theme.StyleNormal.FillStyle = fillStyle;
        theme.StyleHighlight.FillStyle.Colors[0] = ControlPaint.LightLight(Color.FromArgb(200, theme.StyleNormal.FillStyle.Colors[0]));
        theme.StyleHighlight.FillStyle.Colors[1] = ControlPaint.LightLight(Color.FromArgb(200, theme.StyleNormal.FillStyle.Colors[1]));
        if (theme.StyleNormal.FillStyle.ColorsNumber > 2)
        {
          theme.StyleHighlight.FillStyle.Colors[2] = ControlPaint.LightLight(Color.FromArgb(200, theme.StyleNormal.FillStyle.Colors[2]));
          theme.StyleHighlight.FillStyle.Colors[3] = ControlPaint.LightLight(Color.FromArgb(200, theme.StyleNormal.FillStyle.Colors[3]));
        }
        theme.StyleNormal.FillStyle.Colors[0] = Color.Transparent;
        theme.StyleNormal.FillStyle.Colors[1] = Color.Transparent;
        if (theme.StyleNormal.FillStyle.ColorsNumber > 2)
        {
          theme.StyleNormal.FillStyle.Colors[2] = Color.Transparent;
          theme.StyleNormal.FillStyle.Colors[3] = Color.Transparent;
        }
        this.backFill.LoadTheme(theme);
        this.backFill.IsAnimated = false;
        this.Invalidate();
        this.Content.Invalidate();
      }
    }

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
        this.Content.VIBlendTheme = value;
      }
    }

    /// <summary>Gets the content.</summary>
    /// <value>The content.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    public vRibbonGroupContent Content
    {
      get
      {
        return this.groupContent;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:VIBlend.WinForms.Controls.vRibbonGroup" /> is collapsed.
    /// </summary>
    /// <value><c>true</c> if collapse; otherwise, <c>false</c>.</value>
    [Category("Layout")]
    [Description("Gets or sets a value indicating whether this group is collapsed.")]
    [DefaultValue(false)]
    internal bool Collapsed
    {
      get
      {
        return this.collapse;
      }
      set
      {
        if (this.Collapsed == value)
          return;
        this.collapse = value;
        if (this.dropDown != null)
          this.dropDown.Close();
        this.CalculateLayout();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the group is auto collapsed
    /// </summary>
    [DefaultValue(true)]
    [Category("Layout")]
    [Description("Gets or sets a value indicating whether the group is auto collapsed")]
    internal bool AutoCollapse
    {
      get
      {
        return this.autoCollapse;
      }
      set
      {
        this.autoCollapse = value;
      }
    }

    /// <summary>
    /// Gets or sets the height of the header area of the item
    /// </summary>
    [Description("Gets or sets the height of the header area of the item")]
    [Category("Appearance")]
    [DefaultValue(16)]
    public int FooterHeight
    {
      get
      {
        return this.Content.FooterHeight;
      }
      set
      {
        this.Content.FooterHeight = value;
      }
    }

    /// <summary>Gets or sets the radius.</summary>
    /// <value>The radius.</value>
    [DefaultValue(3)]
    [Category("Appearance")]
    [Description("Gets or sets the radius.")]
    public int Radius
    {
      get
      {
        return this.Content.Radius;
      }
      set
      {
        this.Content.Radius = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the text alignment.</summary>
    /// <value>The text alignment.</value>
    [Description("Gets or sets the text alignment.")]
    [Category("Behavior")]
    public ContentAlignment TextAlignment
    {
      get
      {
        return this.Content.TextAlignment;
      }
      set
      {
        this.Content.TextAlignment = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the footer button is enabled
    /// </summary>
    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether to show the footer button.")]
    public bool EnableFooterButton
    {
      get
      {
        return this.Content.EnableFooterButton;
      }
      set
      {
        this.Content.EnableFooterButton = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show the footer button.
    /// </summary>
    [Description("Gets or sets a value indicating whether to show the footer button.")]
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool ShowFooterButton
    {
      get
      {
        return this.Content.ShowFooterButton;
      }
      set
      {
        this.Content.ShowFooterButton = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use theme text color.
    /// </summary>
    [DefaultValue(true)]
    [Category("Appearance")]
    [Description("Gets or sets a value indicating whether to use theme text color.")]
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
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vRibbonGroup" /> class.
    /// </summary>
    public vRibbonGroup()
    {
      this.groupContent = new vRibbonGroupContent(this);
      this.Controls.Add((Control) this.Content);
      this.backFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.SetStyle(ControlStyles.Opaque, false);
      this.BackColor = Color.Transparent;
      this.dropDown.BackColor = Color.Transparent;
      this.indicatorsTimer1.Tick += new EventHandler(this.indicatorsTimer1_Tick);
      this.indicatorsTimer1.Interval = 10;
      this.dropDown.Closing += new ToolStripDropDownClosingEventHandler(this.dropDown_Closing);
      this.Content.ContentPanel.AutoScroll = false;
      this.Content.MouseEnter += new EventHandler(this.Content_MouseEnter);
      this.Content.ContentPanel.MouseEnter += new EventHandler(this.ContentPanel_MouseEnter);
      this.Content.Footer.MouseEnter += new EventHandler(this.Footer_MouseEnter);
      this.Content.Footer.MouseLeave += new EventHandler(this.Footer_MouseLeave);
      this.Content.MouseLeave += new EventHandler(this.Content_MouseLeave);
      this.Content.ContentPanel.MouseLeave += new EventHandler(this.ContentPanel_MouseLeave);
      this.Content.Paint += new PaintEventHandler(this.Content_Paint);
      this.boundsTimer.Interval = 2500;
      this.boundsTimer.Tick += new EventHandler(this.boundsTimer_Tick);
    }

    private Size GetCollapsedSize()
    {
      using (Graphics graphics = this.CreateGraphics())
      {
        Size size = Size.Ceiling(graphics.MeasureString(this.Text, this.Font));
        return new Size(size.Width + this.Padding.Horizontal + 10, size.Height + this.Padding.Vertical);
      }
    }

    private void ResetTextAlignment()
    {
      this.TextAlignment = ContentAlignment.MiddleCenter;
    }

    private bool ShouldSerializeTextAlignment()
    {
      return this.TextAlignment != ContentAlignment.MiddleCenter;
    }

    private void CalculateLayout()
    {
      if (this.Collapsed)
      {
        this.dropDown.Items.Clear();
        this.savedSize = this.Size;
        this.Size = new Size(this.GetCollapsedSize().Width, this.Height);
        ToolStripControlHost stripControlHost = new ToolStripControlHost((Control) this.Content);
        this.Content.Parent = (Control) null;
        this.dropDown.Items.Add((ToolStripItem) stripControlHost);
      }
      else
      {
        this.Size = this.savedSize;
        this.Content.Parent = (Control) this;
        this.Content.Visible = true;
      }
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.
    /// </summary>
    /// <param name="levent">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      this.Content.Bounds = new Rectangle(0, 0, this.Width, this.Height);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      this.Content.Bounds = new Rectangle(0, 0, this.Width, this.Height);
    }

    /// <summary>Clean up any resources being used.</summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.indicatorsTimer1.Dispose();
        this.boundsTimer.Dispose();
      }
      base.Dispose(disposing);
    }

    private void indicatorsTimer1_Tick(object sender, EventArgs e)
    {
      if (this.DesignMode)
        return;
      this.groupContent.Visible = false;
      if (this.dropDown.Opacity < 0.05)
        this.dropDown.Opacity = 0.0;
      if (this.dropDown.Opacity > 0.0)
      {
        this.dropDown.Opacity -= 0.05;
      }
      else
      {
        this.indicatorsTimer1.Stop();
        this.groupContent.Visible = true;
        this.dropDown.Close();
      }
      this.Invalidate();
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
      this.boundsTimer.Stop();
      base.OnMouseClick(e);
      this.clicked = this.dropDown.Visible;
      if (!this.clicked)
      {
        this.indicatorsTimer1.Stop();
        this.dropDown.Opacity = 1.0;
        Point position = new Point(0, this.Height);
        this.dropDown.MinimumSize = this.savedSize;
        this.dropDown.Items[0].Size = this.savedSize;
        this.dropDown.Padding = Padding.Empty;
        this.dropDown.Margin = Padding.Empty;
        this.Content.Radius = 0;
        this.dropDown.Show((Control) this, position);
        this.dropDown.Items[0].Margin = Padding.Empty;
        this.clicked = true;
        this.dropDown.Items[0].Visible = true;
      }
      else
      {
        this.indicatorsTimer1.Start();
        this.clicked = false;
      }
    }

    private void Content_Paint(object sender, PaintEventArgs e)
    {
      if (this.Content.drawContentFill || !this.Collapsed || !this.dropDown.Visible)
        return;
      this.boundsTimer.Stop();
      this.boundsTimer.Start();
    }

    private void boundsTimer_Tick(object sender, EventArgs e)
    {
      if (this.Collapsed)
      {
        this.boundsTimer.Stop();
        this.canClose = true;
      }
      if (!this.Content.drawContentFill)
      {
        this.canClose = true;
        this.dropDown.Close();
        this.boundsTimer.Stop();
      }
      else if (this.Content.RectangleToScreen(this.Content.ClientRectangle).Contains(Cursor.Position))
        this.boundsTimer.Stop();
      else if (this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position))
      {
        this.boundsTimer.Stop();
      }
      else
      {
        if (!this.canClose)
          return;
        this.dropDown.Close();
        this.boundsTimer.Stop();
      }
    }

    private void ContentPanel_MouseLeave(object sender, EventArgs e)
    {
      if (this.Content.RectangleToScreen(this.Content.ClientRectangle).Contains(Cursor.Position) || this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position))
        return;
      this.canClose = true;
      this.boundsTimer.Stop();
      this.boundsTimer.Start();
    }

    private void Content_MouseLeave(object sender, EventArgs e)
    {
      if (this.Content.RectangleToScreen(this.Content.ClientRectangle).Contains(Cursor.Position) || this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position))
        return;
      this.canClose = true;
      this.boundsTimer.Stop();
      this.boundsTimer.Start();
    }

    private void Footer_MouseLeave(object sender, EventArgs e)
    {
      if (this.Content.RectangleToScreen(this.Content.ClientRectangle).Contains(Cursor.Position) || this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position))
        return;
      this.canClose = true;
      this.boundsTimer.Stop();
      this.boundsTimer.Start();
    }

    private void Footer_MouseEnter(object sender, EventArgs e)
    {
      this.boundsTimer.Stop();
      this.canClose = false;
    }

    private void ContentPanel_MouseEnter(object sender, EventArgs e)
    {
      this.boundsTimer.Stop();
      this.canClose = false;
    }

    private void Content_MouseEnter(object sender, EventArgs e)
    {
      this.boundsTimer.Stop();
      this.canClose = false;
    }

    private void dropDown_Closing(object sender, ToolStripDropDownClosingEventArgs e)
    {
      if (!this.canClose)
      {
        e.Cancel = true;
      }
      else
      {
        if (this.dropDown.Opacity != 1.0 || this.indicatorsTimer1.Enabled)
          return;
        e.Cancel = true;
        this.indicatorsTimer1.Start();
        if (this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position))
          return;
        this.clicked = false;
      }
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      this.Invalidate();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.Invalidate();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      if (!this.Collapsed)
        return;
      Rectangle screen = this.RectangleToScreen(this.ClientRectangle);
      this.backFill.Bounds = this.ClientRectangle;
      if (screen.Contains(Cursor.Position))
        this.backFill.DrawElementFill(e.Graphics, ControlState.Hover);
      if (this.Image == null && this.ImageList != null && (this.ImageIndex >= 0 && this.ImageIndex < this.ImageList.Images.Count))
        this.Image = this.ImageList.Images[this.ImageIndex];
      if (this.Image != null)
      {
        Rectangle imageRectangle = ImageAndTextHelper.GetImageRectangle(this.Image, new Rectangle(0, 15, this.Width, this.ClientRectangle.Height - 30), ContentAlignment.TopCenter);
        e.Graphics.DrawImage(this.Image, imageRectangle);
        this.backFill.Bounds = new Rectangle(imageRectangle.X - 1, imageRectangle.Y - 1, imageRectangle.Width + 2, imageRectangle.Height + 2);
        this.backFill.DrawElementBorder(e.Graphics, ControlState.Normal);
      }
      this.backFill.Radius = 3;
      this.backFill.Bounds = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
      this.backFill.DrawElementBorder(e.Graphics, ControlState.Normal);
      StringFormat stringFormat = ImageAndTextHelper.GetStringFormat((Control) this, this.TextAlignment, true, false);
      using (SolidBrush solidBrush = new SolidBrush(this.backFill.ForeColor))
      {
        if (!this.UseThemeTextColor)
          solidBrush.Color = this.ForeColor;
        e.Graphics.DrawString(this.Text, this.Font, (Brush) solidBrush, (RectangleF) new Rectangle(0, this.Height / 2 - 5 - this.GetCollapsedSize().Height / 2, this.Width, this.Height / 2), stringFormat);
        new PaintHelper().DrawArrowFigure(e.Graphics, solidBrush.Color, new Rectangle(this.Width / 2 - 3, this.Height / 2 + this.Height / 4, 5, 3), ArrowDirection.Down);
      }
    }
  }
}
