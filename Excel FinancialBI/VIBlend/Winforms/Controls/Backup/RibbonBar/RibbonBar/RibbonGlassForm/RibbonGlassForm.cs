// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.OfficeRibbonForm
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
  public class OfficeRibbonForm : Form, IScrollableControlBase
  {
    private bool closeBox = true;
    internal ContextMenu titleMenu = new ContextMenu();
    private ContentAlignment textAlignment = ContentAlignment.MiddleLeft;
    private PaintHelper paintHelper = new PaintHelper();
    private int titleHeight = 29;
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private GlassFormUtility helper;
    private vRibbonBar ribbon;
    private vFormButton maximizeButton;
    private vFormButton closeButton;
    private vFormButton minimizeButton;
    internal BackgroundElement titleFill;
    public QuickAccessToolbar quickAccessToolbar1;
    public vRibbonApplicationButton vRibbonApplicationButton1;
    private ControlTheme theme;
    private AnimationManager animManager;

    /// <summary>Gets or sets the height of the title.</summary>
    /// <value>The height of the title.</value>
    [Description("Gets or sets the height of the title.")]
    [DefaultValue(29)]
    [Category("Layout")]
    [Browsable(false)]
    public int TitleHeight
    {
      get
      {
        return this.titleHeight;
      }
      internal set
      {
        this.titleHeight = value;
      }
    }

    /// <summary>Gets or sets the text alignment.</summary>
    /// <value>The text alignment.</value>
    [Category("Appearance")]
    [Description("Gets or sets the text alignment.")]
    [DefaultValue(typeof (ContentAlignment), "ContentAlignment.MiddleLeft")]
    public ContentAlignment TextAlignment
    {
      get
      {
        return this.textAlignment;
      }
      set
      {
        this.textAlignment = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets the title rectangle.</summary>
    /// <value>The title rectangle.</value>
    [Category("Layout")]
    [Description("Gets the title rectangle.")]
    public Rectangle TitleRectangle
    {
      get
      {
        return new Rectangle(0, 0, this.Width, this.titleHeight);
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show the close box.
    /// </summary>
    [Category("Appearance")]
    [Description("Gets or sets a value indicating whether to show the close box.")]
    [DefaultValue(true)]
    public bool CloseBox
    {
      get
      {
        return this.closeBox;
      }
      set
      {
        this.closeBox = value;
        this.closeButton.Visible = false;
        this.PerformLayout();
      }
    }

    /// <summary>Gets or sets the theme of the control.</summary>
    [Description("Gets or sets the theme of the control.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Appearance")]
    [Browsable(false)]
    public virtual ControlTheme Theme
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
        ControlTheme copy = value.CreateCopy();
        this.titleFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
        this.titleFill.LoadTheme(copy);
        this.maximizeButton.Theme = value;
        this.maximizeButton.ForeColor = ControlPaint.LightLight(this.BackColor);
        this.minimizeButton.ForeColor = ControlPaint.LightLight(this.BackColor);
        this.closeButton.ForeColor = ControlPaint.LightLight(this.BackColor);
        this.minimizeButton.Theme = value;
        this.closeButton.Theme = value;
        FillStyle fillStyle = copy.QueryFillStyleSetter("RibbonTitle");
        if (fillStyle != null)
          copy.StyleNormal.FillStyle = fillStyle;
        Color color = copy.QueryColorSetter("RibbonFormBorder");
        if (color != Color.Empty)
          copy.StyleNormal.BorderColor = color;
        this.titleFill.IsAnimated = false;
        this.titleFill.LoadTheme(copy);
        this.minimizeButton.VIBlendTheme = this.VIBlendTheme;
        this.maximizeButton.VIBlendTheme = this.VIBlendTheme;
        this.closeButton.VIBlendTheme = this.VIBlendTheme;
        this.maximizeButton.MinimumSize = new Size(17, 16);
        this.minimizeButton.MinimumSize = new Size(17, 16);
        this.closeButton.MinimumSize = new Size(17, 16);
        this.maximizeButton.MaximumSize = new Size(17, 16);
        this.minimizeButton.MaximumSize = new Size(17, 16);
        this.closeButton.MaximumSize = new Size(17, 16);
        if (this.IsOffice2010Style())
        {
          Size size = new Size(34, 19);
          this.maximizeButton.MinimumSize = size;
          this.minimizeButton.MinimumSize = size;
          this.closeButton.MinimumSize = size;
          this.maximizeButton.MaximumSize = size;
          this.minimizeButton.MaximumSize = size;
          this.closeButton.MaximumSize = size;
        }
        else
        {
          this.maximizeButton.RibbonStyle = true;
          this.closeButton.RibbonStyle = true;
          this.minimizeButton.RibbonStyle = true;
        }
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

    /// <summary>Gets or set the RibbonBar's instance.</summary>
    public vRibbonBar Ribbon
    {
      get
      {
        return this.ribbon;
      }
      set
      {
        this.ribbon = value;
        if (value == null)
          return;
        this.helper = new GlassFormUtility(this);
        this.helper.Ribbon = this.ribbon;
      }
    }

    public GlassFormUtility Helper
    {
      get
      {
        return this.helper;
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
    [Browsable(false)]
    public bool AllowAnimations
    {
      get
      {
        if (this.titleFill != null)
          return this.titleFill.IsAnimated;
        return true;
      }
      set
      {
        if (this.titleFill == null)
          return;
        this.titleFill.IsAnimated = value;
      }
    }

    public OfficeRibbonForm()
    {
      this.minimizeButton = new vFormButton();
      this.closeButton = new vFormButton();
      this.maximizeButton = new vFormButton();
      this.InitializeTitleMenu(this.titleMenu);
      this.Text = "VIBlend Ribbon Form";
      if (WindowsAPI.IsWindows && !WindowsAPI.IsGlassEnabled)
      {
        this.SetStyle(ControlStyles.ResizeRedraw, true);
        this.SetStyle(ControlStyles.Opaque, WindowsAPI.IsGlassEnabled);
        this.SetStyle(ControlStyles.UserPaint, true);
        this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        this.FormBorderStyle = FormBorderStyle.None;
        this.MinimumSize = new Size(250, 250);
        this.MaximumSize = SystemInformation.WorkingArea.Size;
        this.minimizeButton.AllowAnimations = true;
        this.minimizeButton.BackColor = Color.Transparent;
        this.minimizeButton.BorderStyle = ButtonBorderStyle.SOLID;
        this.minimizeButton.ButtonType = vFormButtonType.MinimizeButton;
        this.minimizeButton.ForeColor = Color.White;
        this.minimizeButton.ImageAbsolutePosition = new Point(5, 5);
        this.minimizeButton.Location = new Point(167, 12);
        this.minimizeButton.Name = "minimizeButton";
        this.minimizeButton.Opacity = 1f;
        this.minimizeButton.Padding = new Padding(3);
        this.minimizeButton.PaintBorder = true;
        this.minimizeButton.PaintFill = true;
        this.minimizeButton.RoundedCornersRadius = -1;
        this.minimizeButton.ShowFocusRectangle = false;
        this.minimizeButton.Size = new Size(24, 18);
        this.minimizeButton.StretchImage = false;
        this.minimizeButton.TabIndex = 0;
        this.minimizeButton.TextWrap = false;
        this.minimizeButton.UseAbsoluteImagePositioning = false;
        this.minimizeButton.UseVisualStyleBackColor = false;
        this.minimizeButton.VIBlendTheme = VIBLEND_THEME.OFFICEGREEN;
        this.minimizeButton.Click += new EventHandler(this.minimizeButton_Click);
        this.closeButton.AllowAnimations = true;
        this.closeButton.BackColor = Color.Transparent;
        this.closeButton.BorderStyle = ButtonBorderStyle.SOLID;
        this.closeButton.ButtonType = vFormButtonType.CloseButton;
        this.closeButton.ForeColor = Color.White;
        this.closeButton.ImageAbsolutePosition = new Point(5, 5);
        this.closeButton.Location = new Point(439, 31);
        this.closeButton.Margin = new Padding(5);
        this.closeButton.Name = "closeButton";
        this.closeButton.Opacity = 1f;
        this.closeButton.PaintBorder = true;
        this.closeButton.PaintFill = true;
        this.closeButton.RoundedCornersRadius = -1;
        this.closeButton.ShowFocusRectangle = false;
        this.closeButton.Size = new Size(30, 18);
        this.closeButton.StretchImage = false;
        this.closeButton.TabIndex = 3;
        this.closeButton.TextWrap = false;
        this.closeButton.UseAbsoluteImagePositioning = false;
        this.closeButton.UseVisualStyleBackColor = false;
        this.closeButton.VIBlendTheme = VIBLEND_THEME.OFFICEGREEN;
        this.closeButton.Click += new EventHandler(this.closeButton_Click);
        this.maximizeButton.AllowAnimations = true;
        this.maximizeButton.BackColor = Color.Transparent;
        this.maximizeButton.BorderStyle = ButtonBorderStyle.SOLID;
        this.maximizeButton.ButtonType = vFormButtonType.MaximizeButton;
        this.maximizeButton.ForeColor = Color.White;
        this.maximizeButton.ImageAbsolutePosition = new Point(5, 5);
        this.maximizeButton.Location = new Point(371, 31);
        this.maximizeButton.Name = "maximizeButton";
        this.maximizeButton.Opacity = 1f;
        this.maximizeButton.PaintBorder = true;
        this.maximizeButton.PaintFill = true;
        this.maximizeButton.RoundedCornersRadius = -1;
        this.maximizeButton.ShowFocusRectangle = false;
        this.maximizeButton.Size = new Size(25, 18);
        this.maximizeButton.StretchImage = false;
        this.maximizeButton.TabIndex = 1;
        this.maximizeButton.TextWrap = false;
        this.maximizeButton.UseAbsoluteImagePositioning = false;
        this.maximizeButton.UseVisualStyleBackColor = false;
        this.maximizeButton.VIBlendTheme = VIBLEND_THEME.OFFICEGREEN;
        this.maximizeButton.Click += new EventHandler(this.maximizeButton_Click);
        this.Controls.Add((Control) this.minimizeButton);
        this.Controls.Add((Control) this.closeButton);
        this.Controls.Add((Control) this.maximizeButton);
        this.minimizeButton.BringToFront();
        this.closeButton.BringToFront();
        this.maximizeButton.BringToFront();
      }
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
      this.VIBlendTheme = VIBLEND_THEME.OFFICEBLUE;
    }

    /// <summary>Gets the text alignment.</summary>
    /// <returns></returns>
    private ContentAlignment GetTextAlignment()
    {
      if (this.RightToLeft == RightToLeft.Yes)
      {
        switch (this.TextAlignment)
        {
          case ContentAlignment.BottomCenter:
          case ContentAlignment.TopCenter:
          case ContentAlignment.MiddleCenter:
            return this.textAlignment;
          case ContentAlignment.BottomRight:
            return ContentAlignment.BottomLeft;
          case ContentAlignment.MiddleRight:
            return ContentAlignment.MiddleLeft;
          case ContentAlignment.BottomLeft:
            return ContentAlignment.BottomRight;
          case ContentAlignment.TopLeft:
            return ContentAlignment.TopRight;
          case ContentAlignment.TopRight:
            return ContentAlignment.TopLeft;
          case ContentAlignment.MiddleLeft:
            return ContentAlignment.MiddleRight;
        }
      }
      return this.TextAlignment;
    }

    protected internal virtual void DrawText(PaintEventArgs e, int shadowOffset, int borderOffset)
    {
      Rectangle targetRect = new Rectangle(shadowOffset + borderOffset, shadowOffset + this.TitleHeight / 4, this.TitleHeight / 2, this.TitleHeight / 2);
      Rectangle Bounds = new Rectangle(shadowOffset + borderOffset + 5 + this.TitleHeight / 2, shadowOffset, this.Width - shadowOffset - borderOffset - 95, this.TitleHeight);
      if (!this.ShowIcon)
        Bounds = new Rectangle(shadowOffset + borderOffset + 5, shadowOffset, this.Width - shadowOffset - borderOffset - 95, this.TitleHeight);
      Size size = Size.Ceiling(e.Graphics.MeasureString(this.Text, this.Font));
      if (this.RightToLeft == RightToLeft.Yes)
      {
        targetRect = new Rectangle(this.Width - shadowOffset - borderOffset - this.TitleHeight / 2, shadowOffset + this.TitleHeight / 4, this.TitleHeight / 2, this.TitleHeight / 2);
        Bounds = new Rectangle(shadowOffset + borderOffset + 90, shadowOffset, this.Width - shadowOffset - borderOffset - 85 - size.Width, this.TitleHeight);
      }
      if (this.ShowIcon)
        e.Graphics.DrawIcon(this.Icon, targetRect);
      ContentAlignment textAlignment = this.GetTextAlignment();
      this.paintHelper.DrawText(e.Graphics, Bounds, false, this.ForeColor, this.Text, this.Font, textAlignment);
    }

    protected override void OnTextChanged(EventArgs e)
    {
      base.OnTextChanged(e);
      if (this.Ribbon == null)
        return;
      this.Ribbon.Text = this.Text;
    }

    internal void SetTaskBarMenu()
    {
      if (this.FormBorderStyle != FormBorderStyle.None)
        return;
      this.InitializeTitleMenu().Show((Control) this, this.PointToClient(Control.MousePosition));
    }

    private ContextMenu InitializeTitleMenu()
    {
      ContextMenu contextMenu = new ContextMenu();
      MenuItem menuItem1 = new MenuItem("Restore");
      menuItem1.Click += new EventHandler(this.menuitem_RestoreClick);
      contextMenu.MenuItems.Add(menuItem1);
      MenuItem menuItem2 = new MenuItem("Minimize");
      menuItem2.Click += new EventHandler(this.menuitem_MinimizeClick);
      menuItem2.Enabled = this.WindowState != FormWindowState.Minimized;
      contextMenu.MenuItems.Add(menuItem2);
      MenuItem menuItem3 = new MenuItem("Maximize");
      menuItem3.Click += new EventHandler(this.menuitem_MaximizeClick);
      menuItem3.Enabled = this.WindowState != FormWindowState.Maximized;
      contextMenu.MenuItems.Add(menuItem3);
      contextMenu.MenuItems.Add(new MenuItem("-"));
      MenuItem menuItem4 = new MenuItem("Close");
      menuItem4.Click += new EventHandler(this.menuitem_CloseClick);
      contextMenu.MenuItems.Add(menuItem4);
      return contextMenu;
    }

    private ContextMenu InitializeTitleMenu(ContextMenu menu)
    {
      MenuItem menuItem1 = new MenuItem("Restore");
      menuItem1.Click += new EventHandler(this.menuitem_RestoreClick);
      menu.MenuItems.Add(menuItem1);
      MenuItem menuItem2 = new MenuItem("Minimize");
      menuItem2.Click += new EventHandler(this.menuitem_MinimizeClick);
      menuItem2.Enabled = this.WindowState != FormWindowState.Minimized;
      menu.MenuItems.Add(menuItem2);
      MenuItem menuItem3 = new MenuItem("Maximize");
      menuItem3.Click += new EventHandler(this.menuitem_MaximizeClick);
      menuItem3.Enabled = this.WindowState != FormWindowState.Maximized;
      menu.MenuItems.Add(menuItem3);
      menu.MenuItems.Add(new MenuItem("-"));
      MenuItem menuItem4 = new MenuItem("Close");
      menuItem4.Click += new EventHandler(this.menuitem_CloseClick);
      menu.MenuItems.Add(menuItem4);
      return menu;
    }

    private void menuitem_CloseClick(object sender, EventArgs e)
    {
      this.Close();
    }

    private void menuitem_MinimizeClick(object sender, EventArgs e)
    {
      this.WindowState = FormWindowState.Minimized;
    }

    private void menuitem_RestoreClick(object sender, EventArgs e)
    {
      this.WindowState = FormWindowState.Normal;
    }

    private void menuitem_MaximizeClick(object sender, EventArgs e)
    {
      this.HandleMaximize();
    }

    /// <summary>
    /// Raises the <see cref="E:Layout" /> event.
    /// </summary>
    /// <param name="levent">The <see cref="T:System.Windows.Forms.LayoutEventArgs" /> instance containing the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      int num1 = 2;
      int num2 = 2;
      int num3 = 62;
      if (this.IsOffice2010Style())
        num3 = 111;
      int num4 = num3;
      if (!this.MaximizeBox)
        num4 = num3 - this.maximizeButton.Width - num1 - num2;
      if (this.MinimizeBox)
      {
        this.minimizeButton.Location = new Point(this.Width - num4 - num2, num2 + 2);
        if (this.RightToLeft == RightToLeft.Yes)
          this.minimizeButton.Location = new Point(num4 + num2 - this.minimizeButton.Width, num2 + 2);
      }
      if (this.MaximizeBox)
      {
        this.maximizeButton.Location = new Point(this.Width - num4 - num2 + this.minimizeButton.Width + num1, num2 + 2);
        if (this.RightToLeft == RightToLeft.Yes)
          this.maximizeButton.Location = new Point(num4 + num2 - this.minimizeButton.Width - this.maximizeButton.Width, num2 + 2);
      }
      if (!this.CloseBox)
        return;
      if (!this.MaximizeBox)
        this.closeButton.Location = new Point(this.Width - num4 - num2 + this.minimizeButton.Width + 2 * num1, num2 + 2);
      else
        this.closeButton.Location = new Point(this.Width - num4 - num2 + this.minimizeButton.Width + this.maximizeButton.Width + 2 * num1, num2 + 2);
      if (this.RightToLeft != RightToLeft.Yes)
        return;
      this.closeButton.Location = new Point(num4 + num2 - this.minimizeButton.Width - this.maximizeButton.Width - this.closeButton.Width, num2 + 2);
    }

    /// <summary>Handles the Click event of the closeButton control.</summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    private void closeButton_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    /// <summary>
    /// Handles the Click event of the minimizeButton control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    private void minimizeButton_Click(object sender, EventArgs e)
    {
      this.WindowState = FormWindowState.Minimized;
    }

    /// <summary>
    /// Handles the Click event of the maximizeButton control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    private void maximizeButton_Click(object sender, EventArgs e)
    {
      this.HandleMaximize();
    }

    /// <summary>Handles the maximize.</summary>
    public void HandleMaximize()
    {
      if (this.WindowState == FormWindowState.Maximized)
        this.WindowState = FormWindowState.Normal;
      else
        this.WindowState = FormWindowState.Maximized;
    }

    private bool IsOffice2010Style()
    {
      if (this.VIBlendTheme != VIBLEND_THEME.OFFICE2010BLACK && this.VIBlendTheme != VIBLEND_THEME.OFFICE2010SILVER)
        return this.VIBlendTheme == VIBLEND_THEME.OFFICE2010BLUE;
      return true;
    }

    protected override void OnLoad(EventArgs e)
    {
      if (!this.DesignMode && this.Ribbon != null && WindowsAPI.IsGlassEnabled)
        this.Ribbon.BackColor = Color.Black;
      if (!WindowsAPI.IsGlassEnabled)
      {
        this.minimizeButton.BringToFront();
        this.maximizeButton.BringToFront();
        this.closeButton.BringToFront();
      }
      base.OnLoad(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      if (WindowsAPI.IsGlassEnabled)
        return;
      this.SetRegion(this.paintHelper);
    }

    private void SetRegion(PaintHelper helper)
    {
      if (WindowsAPI.IsGlassEnabled)
        return;
      if (this.WindowState != FormWindowState.Maximized)
      {
        Rectangle bounds = new Rectangle(0, 0, this.Width, this.Height);
        this.Region = new Region(helper.GetRoundedPathRect(bounds, 5));
      }
      if (this.WindowState != FormWindowState.Maximized)
        return;
      this.Region = (Region) null;
    }

    /// <summary>Overrides the WndProc funciton</summary>
    /// <param name="m"></param>
    protected override void WndProc(ref Message m)
    {
      if (this.Helper == null)
      {
        base.WndProc(ref m);
      }
      else
      {
        if (this.Helper == null || this.Helper.WndProc(ref m))
          return;
        base.WndProc(ref m);
      }
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.ClientSize = new Size(713, 425);
      this.Name = "RibbonGlassForm";
      this.ResumeLayout(false);
    }
  }
}
