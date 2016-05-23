// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vForm
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  [ToolboxItem(false)]
  public class vForm : Form, IScrollableControlBase
  {
    private vForm.MARGINS margins = new vForm.MARGINS();
    private int titleHeight = 29;
    private int borderWidth = 7;
    private int roundRadius = 5;
    private Color shadowColor = Color.FromArgb(38, 43, 48);
    private float titleOpacity = 1f;
    private ContentAlignment textAlignment = ContentAlignment.MiddleLeft;
    private bool closeBox = true;
    private ContextMenu titleMenu = new ContextMenu();
    private Rectangle topRect = Rectangle.Empty;
    private Rectangle botRect = Rectangle.Empty;
    private Rectangle lefRect = Rectangle.Empty;
    private Rectangle rigRect = Rectangle.Empty;
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private bool allowMaximizeOnDoubleClick = true;
    protected PaintHelper helper = new PaintHelper();
    private bool canResize = true;
    internal const int WM_NCHITTEST = 132;
    internal const int HTCLIENT = 1;
    internal const int HTCAPTION = 2;
    private Bitmap screenBitmap;
    protected BackgroundElement titleFill;
    private int shadowWidth;
    protected vControlBoundsHelper boundsHelper;
    protected internal vFormButton maximizeButton;
    protected internal vFormButton closeButton;
    protected internal vFormButton minimizeButton;
    private bool isActive;
    private bool enableGlassEffect;
    private bool resizing;
    private ControlTheme theme;
    private IContainer components;
    internal bool stopDefaultLayout;
    private bool lastActive;
    private bool canUpdate;
    private AnimationManager animManager;
    private Rectangle BorderBottomRect;
    private Rectangle BorderTopRect;
    private Rectangle BorderLeftRect;
    private Rectangle BorderRightRect;
    private Padding currentPadding;
    private bool canResizeSet;

    [Browsable(false)]
    public vControlBoundsHelper BoundsHelper
    {
      get
      {
        return this.boundsHelper;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether glass effect is enabled
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether glass effect is enabled")]
    internal virtual bool EnableGlassEffect
    {
      get
      {
        return this.enableGlassEffect;
      }
      set
      {
        this.enableGlassEffect = value;
        if (value)
        {
          this.DoubleBuffered = false;
          this.boundsHelper.UnWire();
          this.Region = (Region) null;
          this.SetPadding();
          this.Invalidate();
        }
        else
        {
          this.DoubleBuffered = true;
          this.boundsHelper.Wire();
          this.SetPadding();
          this.Invalidate();
        }
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

    /// <summary>Gets or sets the theme of the control.</summary>
    [Description("Gets or sets the theme of the control.")]
    [Browsable(false)]
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
    /// Gets a value indicating whether this instance is active window.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is active window; otherwise, <c>false</c>.
    /// </value>
    [Category("Behavior")]
    [Description("Gets a value indicating whether this instance is active window.")]
    public bool IsActiveWindow
    {
      get
      {
        return this.isActive;
      }
    }

    [Browsable(false)]
    public new FormBorderStyle FormBorderStyle
    {
      get
      {
        return FormBorderStyle.None;
      }
      set
      {
        base.FormBorderStyle = FormBorderStyle.None;
      }
    }

    /// <summary>Gets or sets the title opacity.</summary>
    /// <value>The title opacity.</value>
    [DefaultValue(0.9f)]
    [Category("Appearance")]
    [Description("Gets or sets the title opacity.")]
    public virtual float TitleOpacity
    {
      get
      {
        return this.titleOpacity;
      }
      set
      {
        this.titleOpacity = value;
      }
    }

    /// <summary>Gets or sets the color of the shadow.</summary>
    /// <value>The color of the shadow.</value>
    [Category("Appearance")]
    [Description("Gets or sets the color of the shadow")]
    [DefaultValue(typeof (Color), "Color.DarkGray")]
    public Color ShadowColor
    {
      get
      {
        return this.shadowColor;
      }
      set
      {
        this.shadowColor = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets the title rectangle.</summary>
    /// <value>The title rectangle.</value>
    [Description("Gets the title rectangle.")]
    [Category("Layout")]
    public Rectangle TitleRectangle
    {
      get
      {
        return new Rectangle(this.shadowWidth, this.shadowWidth, this.Width - 2 * this.shadowWidth, this.titleHeight);
      }
    }

    /// <summary>Gets or sets the width of the shadow.</summary>
    /// <value>The width of the shadow.</value>
    [Category("Layout")]
    [Description("Gets or sets the width of the shadow.")]
    [DefaultValue(6)]
    public int ShadowWidth
    {
      get
      {
        return this.shadowWidth;
      }
      set
      {
        this.shadowWidth = value;
        this.SetPadding();
      }
    }

    /// <summary>Gets or sets the round radius.</summary>
    /// <value>The round radius.</value>
    [Description("Gets or sets the round radius.")]
    [DefaultValue(5)]
    [Category("Appearance")]
    public int RoundRadius
    {
      get
      {
        return this.roundRadius;
      }
      set
      {
        this.roundRadius = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the width of the border.</summary>
    /// <value>The width of the border.</value>
    [Description("Gets or sets the width of the border.")]
    [Category("Layout")]
    [DefaultValue(6)]
    public int BorderWidth
    {
      get
      {
        return this.borderWidth;
      }
      set
      {
        this.borderWidth = value;
        this.SetPadding();
      }
    }

    /// <summary>Gets or sets the height of the title.</summary>
    /// <value>The height of the title.</value>
    [DefaultValue(29)]
    [Description("Gets or sets the height of the title.")]
    [Category("Layout")]
    public int TitleHeight
    {
      get
      {
        return this.titleHeight;
      }
      set
      {
        this.titleHeight = value;
        this.SetPadding();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the Minimize button is displayed in the caption bar of the form.
    /// </summary>
    /// <value></value>
    /// <returns>true to display a Minimize button for the form; otherwise, false. The default is true.
    /// </returns>
    /// <PermissionSet>
    /// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
    /// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [DefaultValue(true)]
    [Category("Appearance")]
    public new bool MinimizeBox
    {
      get
      {
        return base.MinimizeBox;
      }
      set
      {
        base.MinimizeBox = value;
        this.minimizeButton.Visible = false;
        this.PerformLayout();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the Maximize button is displayed in the caption bar of the form.
    /// </summary>
    /// <value></value>
    /// <returns>true to display a Maximize button for the form; otherwise, false. The default is true.
    /// </returns>
    /// <PermissionSet>
    /// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
    /// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [Category("Appearance")]
    [DefaultValue(true)]
    public new bool MaximizeBox
    {
      get
      {
        return base.MaximizeBox;
      }
      set
      {
        base.MaximizeBox = value;
        this.maximizeButton.Visible = false;
        this.PerformLayout();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show the close box.
    /// </summary>
    [DefaultValue(true)]
    [Category("Appearance")]
    [Description("Gets or sets a value indicating whether to show the close box.")]
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

    /// <summary>
    /// Gets or sets a value indicating whether form is maximized on double click
    /// </summary>
    [Description("Gets or sets a value indicating whether form is maximized on double click")]
    [Category("Behavior")]
    public bool AllowMaximize
    {
      get
      {
        return this.allowMaximizeOnDoubleClick;
      }
      set
      {
        this.allowMaximizeOnDoubleClick = value;
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

    /// <summary>Occurs when form is activating.</summary>
    [Category("Action")]
    public event EventHandler Activating;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vForm" /> class.
    /// </summary>
    public vForm()
    {
      this.InitializeComponent();
      this.Load += new EventHandler(this.Form1_Load);
      this.GotFocus += new EventHandler(this.Form1_GotFocus);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.FormBorderStyle = FormBorderStyle.None;
      this.ResizeRedraw = true;
      this.SetPadding();
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
      this.Size = new Size(800, 800);
      this.MinimumSize = new Size(200, 200);
      this.VIBlendTheme = VIBLEND_THEME.OFFICEGREEN;
      this.InitializeTitleMenu(this.titleMenu);
      this.boundsHelper = new vControlBoundsHelper((Control) this);
    }

    [DllImport("dwmapi.dll")]
    internal static extern void DwmIsCompositionEnabled(ref bool isEnabled);

    private bool IsPointOnGlass(int lParam)
    {
      if (!this.IsAeroGlassEnabled())
        return false;
      Point client = this.PointToClient(new Point(lParam << 16 >> 16, lParam >> 16));
      return this.topRect.Contains(client) || this.lefRect.Contains(client) || (this.rigRect.Contains(client) || this.botRect.Contains(client));
    }

    protected override void OnResizeBegin(EventArgs e)
    {
      base.OnResizeBegin(e);
      if (!(Cursor.Current != Cursors.Default) || !this.EnableGlassEffect)
        return;
      this.resizing = true;
      this.Refresh();
      this.DoubleBuffered = true;
    }

    protected override void OnResizeEnd(EventArgs e)
    {
      base.OnResizeEnd(e);
      if (!this.EnableGlassEffect || !this.resizing)
        return;
      this.DoubleBuffered = false;
      this.Invalidate();
      this.resizing = false;
    }

    public static void CallDwmBase(ref Message m)
    {
      IntPtr result = m.Result;
      vForm.DwmDefWindowProc(m.HWnd, m.Msg, m.WParam, m.LParam, out result);
      m.Result = result;
    }

    private bool IsAeroGlassEnabled()
    {
      if (Environment.OSVersion.Version.Major < 6)
        return false;
      bool isEnabled = false;
      vForm.DwmIsCompositionEnabled(ref isEnabled);
      return isEnabled;
    }

    protected virtual void OnActivating(EventArgs e)
    {
      if (this.Activating == null)
        return;
      this.Activating((object) this, e);
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool AdjustWindowRectEx(ref NativeFunctions.RECT lpRect, int dwStyle, bool bMenu, int dwExStyle);

    /// <summary>Clean up any resources being used.</summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (vForm));
      this.minimizeButton = new vFormButton();
      this.closeButton = new vFormButton();
      this.maximizeButton = new vFormButton();
      this.SuspendLayout();
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
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(495, 264);
      this.Controls.Add((Control) this.minimizeButton);
      this.Controls.Add((Control) this.closeButton);
      this.Controls.Add((Control) this.maximizeButton);
      this.Name = "vForm";
      this.Text = "Form1";
      this.ResumeLayout(false);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (e.Button != MouseButtons.Right || !this.TitleRectangle.Contains(e.Location))
        return;
      this.titleMenu.Show((Control) this, e.Location);
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    internal static extern IntPtr GetFocus();

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetActiveWindow();

    protected Control GetFocusControl()
    {
      Control control = (Control) null;
      IntPtr focus = vForm.GetFocus();
      vForm.GetActiveWindow();
      if (focus != IntPtr.Zero)
        control = Control.FromHandle(focus);
      return control;
    }

    private bool IsParentThis(Control control)
    {
      for (Control control1 = control; control1 != null; control1 = control1.Parent)
      {
        if (control1 == this)
          return true;
      }
      return false;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Form.Activated" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnActivated(EventArgs e)
    {
      this.isActive = true;
      base.OnActivated(e);
      this.MaximizedBounds = Screen.GetWorkingArea((Control) this);
      int num = this.canUpdate ? 1 : 0;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Form.Deactivate" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnDeactivate(EventArgs e)
    {
      base.OnDeactivate(e);
      this.isActive = false;
      if (this.EnableGlassEffect)
        return;
      this.InvalidateFrame();
    }

    /// <summary>Sets the padding.</summary>
    protected virtual void SetPadding()
    {
      this.Padding = new Padding(this.borderWidth + this.shadowWidth, this.titleHeight + this.shadowWidth, this.borderWidth + this.shadowWidth, this.borderWidth + this.shadowWidth);
      if (this.EnableGlassEffect)
      {
        this.margins.cxLeftWidth = this.Padding.Left;
        this.margins.cxRightWidth = this.Padding.Right;
        this.margins.cyTopHeight = this.Padding.Top;
        this.margins.cyBottomHeight = this.Padding.Bottom;
      }
      else
      {
        this.margins.cxLeftWidth = 0;
        this.margins.cxRightWidth = 0;
        this.margins.cyTopHeight = 0;
        this.margins.cyBottomHeight = 0;
      }
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (!this.Capture || new Rectangle(this.Padding.Left, this.Padding.Top, this.Width - this.Padding.Horizontal, this.Height - this.Padding.Vertical).Contains(e.Location))
        return;
      int num = this.EnableGlassEffect ? 1 : 0;
    }

    protected virtual void InvalidateFrame()
    {
      Rectangle rc1 = new Rectangle(0, 0, this.Width, this.Padding.Top);
      Rectangle rc2 = new Rectangle(0, this.Height - this.Padding.Bottom, this.Width, this.Padding.Bottom);
      Rectangle rc3 = new Rectangle(0, 0, this.Padding.Left, this.Height);
      Rectangle rc4 = new Rectangle(this.Width - this.Padding.Right, 0, this.Padding.Right, this.Height);
      this.Invalidate(rc1);
      this.Invalidate(rc2);
      this.Invalidate(rc3);
      this.Invalidate(rc4);
    }

    /// <summary>Handles the GotFocus event of the Form1 control.</summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    private void Form1_GotFocus(object sender, EventArgs e)
    {
    }

    /// <summary>Makes the screen copy without window.</summary>
    private void MakeScreenCopyWithoutWindow()
    {
      if ((double) this.TitleOpacity >= 1.0)
        return;
      this.Opacity = 0.0;
      this.MakeScreenCopy();
      this.Opacity = 1.0;
    }

    /// <summary>Handles the Load event of the Form1 control.</summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    private void Form1_Load(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      this.SetRegion(this.helper);
    }

    private void SetRegion(PaintHelper helper)
    {
      if (this.EnableGlassEffect)
        return;
      if (this.ShadowWidth > 0)
        this.Region = new Region(helper.GetRoundedPathRect(this.ClientRectangle, 2 * this.RoundRadius));
      else
        this.Region = new Region(helper.GetRoundedPathRect(this.ClientRectangle, this.RoundRadius));
      if (this.WindowState != FormWindowState.Maximized)
        return;
      this.Region = (Region) null;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnLostFocus(EventArgs e)
    {
      base.OnLostFocus(e);
    }

    protected override void OnShown(EventArgs e)
    {
      base.OnShown(e);
      if (this.WindowState != FormWindowState.Maximized)
        return;
      this.WindowState = FormWindowState.Normal;
      this.HandleMaximize();
      this.StartPosition = FormStartPosition.Manual;
      this.Location = Point.Empty;
    }

    /// <summary>
    /// Raises the <see cref="E:Layout" /> event.
    /// </summary>
    /// <param name="levent">The <see cref="T:System.Windows.Forms.LayoutEventArgs" /> instance containing the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      if (this.stopDefaultLayout)
        return;
      int num1 = 2;
      int num2 = this.ShadowWidth;
      if (this.ShadowWidth == 0)
        num2 = 2;
      if (this.WindowState == FormWindowState.Maximized)
        num2 = 2;
      if (this.MinimizeBox)
      {
        this.minimizeButton.Location = new Point(this.Width - 90 - num2, num2 + 2);
        if (this.RightToLeft == RightToLeft.Yes)
          this.minimizeButton.Location = new Point(90 + num2 - this.minimizeButton.Width, num2 + 2);
      }
      if (this.MaximizeBox)
      {
        this.maximizeButton.Location = new Point(this.Width - 90 - num2 + this.minimizeButton.Width + num1, num2 + 2);
        if (this.RightToLeft == RightToLeft.Yes)
          this.maximizeButton.Location = new Point(90 + num2 - this.minimizeButton.Width - this.maximizeButton.Width, num2 + 2);
      }
      if (this.CloseBox)
      {
        this.closeButton.Location = new Point(this.Width - 90 - num2 + this.minimizeButton.Width + this.maximizeButton.Width + 2 * num1, num2 + 2);
        if (this.RightToLeft == RightToLeft.Yes)
          this.closeButton.Location = new Point(90 + num2 - this.minimizeButton.Width - this.maximizeButton.Width - this.closeButton.Width, num2 + 2);
      }
      this.Invalidate();
    }

    private void SetTaskBarMenu()
    {
      if (base.FormBorderStyle != FormBorderStyle.None)
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

    [DllImport("dwmapi.dll")]
    public static extern int DwmDefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam, out IntPtr result);

    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 165)
        this.titleMenu.Show((Control) this, this.GetPointFromPtr(m.LParam));
      if (m.Msg == 787)
        this.SetTaskBarMenu();
      if (this.EnableGlassEffect)
      {
        base.WndProc(ref m);
        if (m.Msg == 132)
          this.NCHitTest(ref m);
        if (m.Msg != 674)
          return;
        vForm.CallDwmBase(ref m);
      }
      else
      {
        if (m.Msg == 6)
        {
          bool flag = vForm.Util.LOWORD(m.WParam) != 0;
          this.OnActivating(EventArgs.Empty);
          this.canUpdate = true;
          if (this.lastActive != flag && this.GetFocusControl() != null)
            this.canUpdate = false;
          this.lastActive = flag;
        }
        base.WndProc(ref m);
      }
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool GetWindowRect(HandleRef hWnd, [In, Out] ref NativeFunctions.RECT rect);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr FindWindow(string className, string windowName);

    /// <summary>
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
      if (!this.DesignMode)
        Licensing.LICheck((Control) this);
      base.OnPaint(e);
      if (this.EnableGlassEffect)
      {
        Graphics graphics = e.Graphics;
        this.topRect = new Rectangle(0, 0, this.ClientSize.Width, this.margins.cyTopHeight);
        this.lefRect = new Rectangle(0, 0, this.margins.cxLeftWidth, this.ClientSize.Height);
        this.rigRect = new Rectangle(this.ClientSize.Width - this.margins.cxRightWidth, 0, this.margins.cxRightWidth, this.ClientSize.Height);
        this.botRect = new Rectangle(0, this.ClientSize.Height - this.margins.cyBottomHeight, this.ClientSize.Width, this.margins.cyBottomHeight);
        if (!this.resizing)
        {
          graphics.FillRectangle(Brushes.Black, this.topRect);
          graphics.FillRectangle(Brushes.Black, this.lefRect);
          graphics.FillRectangle(Brushes.Black, this.rigRect);
          graphics.FillRectangle(Brushes.Black, this.botRect);
        }
      }
      e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
      e.Graphics.InterpolationMode = InterpolationMode.Low;
      Rectangle rect1 = new Rectangle(this.Padding.Left - 1, this.Padding.Top - 1, this.Width - this.Padding.Horizontal + 1, this.Height - this.Padding.Vertical + 1);
      int shadowOffset = this.shadowWidth;
      int borderOffset = this.borderWidth;
      if (this.WindowState != FormWindowState.Maximized)
      {
        Point screen = this.PointToScreen(Point.Empty);
        ImageAttributes imageAttributes = new ImageAttributes();
        RectangleF rectangleF = new RectangleF((PointF) screen, (SizeF) this.Size);
        int shadowWidth = this.ShadowWidth;
        this.titleFill.Opacity = this.TitleOpacity;
        Rectangle rect2 = new Rectangle(this.ShadowWidth - 1, this.ShadowWidth - 1, this.Width - 2 * this.ShadowWidth + 1, this.Height - 2 * this.ShadowWidth + 1);
        double num1 = (double) this.ShadowWidth;
        using (Pen pen = new Pen(this.ShadowColor))
        {
          SolidBrush solidBrush = new SolidBrush(this.ShadowColor);
          for (int index = 0; index < this.ShadowWidth; ++index)
          {
            Color color = Color.FromArgb((int) byte.MaxValue + (int) (-255.0 * ((double) index / num1)), this.ShadowColor);
            pen.Color = color;
            solidBrush.Color = color;
            e.Graphics.DrawRectangle(pen, rect2);
            rect2.Inflate(1, 1);
          }
        }
        Rectangle rectangle = new Rectangle(this.TitleRectangle.X - 1, this.TitleRectangle.Y - 1, this.Width - 2 * this.ShadowWidth + 1, this.Height - 2 * this.ShadowWidth + 1);
        if (this.EnableGlassEffect)
        {
          this.titleFill.Bounds = this.topRect;
          this.titleFill.DrawStandardFill(e.Graphics, ControlState.Normal, GradientStyles.Linear);
          this.titleFill.Bounds = this.botRect;
          this.titleFill.DrawStandardFill(e.Graphics, ControlState.Normal, GradientStyles.Linear);
          this.titleFill.Bounds = new Rectangle(0, this.margins.cyTopHeight - 2, this.margins.cxLeftWidth, this.Height - this.margins.cyTopHeight - this.margins.cyBottomHeight + 4);
          this.titleFill.DrawStandardFill(e.Graphics, ControlState.Normal, GradientStyles.Linear);
          this.titleFill.Bounds = new Rectangle(this.Width - this.margins.cxRightWidth, this.margins.cyTopHeight - 2, this.margins.cxLeftWidth, this.Height - this.margins.cyTopHeight - this.margins.cyBottomHeight + 4);
          this.titleFill.DrawStandardFill(e.Graphics, ControlState.Normal, GradientStyles.Linear);
        }
        else
        {
          this.titleFill.Bounds = rectangle;
          this.titleFill.DrawStandardFill(e.Graphics, ControlState.Normal, GradientStyles.Linear);
        }
        using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
          e.Graphics.FillRectangle((Brush) solidBrush, rect1);
        using (Pen pen = new Pen(this.titleFill.BorderColor))
        {
          e.Graphics.DrawRectangle(pen, rect1);
          int num2 = 1;
          if (this.shadowWidth > 0)
            num2 = 0;
          Rectangle bounds = new Rectangle(this.shadowWidth - 2, this.shadowWidth - 2, this.Width - 2 * this.shadowWidth - num2 + 2, this.Height - 2 * this.shadowWidth - num2 + 2);
          if (this.ShadowWidth < 2)
            bounds = new Rectangle(0, 0, this.Width - num2, this.Height - num2);
          GraphicsPath roundedPathRect = this.helper.GetRoundedPathRect(bounds, this.RoundRadius);
          e.Graphics.DrawPath(pen, roundedPathRect);
        }
      }
      else
      {
        this.titleFill.Bounds = new Rectangle(0, 0, this.Width, this.Height);
        this.titleFill.Opacity = 1f;
        this.titleFill.DrawStandardFill(e.Graphics, ControlState.Normal, GradientStyles.Linear);
        rect1 = new Rectangle(0, this.TitleHeight, this.Width, this.Height - this.TitleHeight);
        using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
          e.Graphics.FillRectangle((Brush) solidBrush, rect1);
        shadowOffset = 0;
        borderOffset = 0;
      }
      this.DrawText(e, shadowOffset, borderOffset);
    }

    protected virtual void DrawText(PaintEventArgs e, int shadowOffset, int borderOffset)
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
      this.helper.DrawText(e.Graphics, Bounds, false, this.ForeColor, this.Text, this.Font, textAlignment);
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

    /// <summary>Makes the screen copy.</summary>
    private void MakeScreenCopy()
    {
      this.screenBitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
      using (Graphics graphics = Graphics.FromImage((Image) this.screenBitmap))
        graphics.CopyFromScreen(Point.Empty, Point.Empty, this.screenBitmap.Size);
    }

    [DllImport("USER32.dll")]
    public static extern bool GetWindowRect(IntPtr hWnd, ref vForm.RECT lpRect);

    public Point GetPointFromPtr(IntPtr ptr)
    {
      Point point = new Point((int) ptr);
      vForm.RECT lpRect = new vForm.RECT();
      vForm.GetWindowRect(this.Handle, ref lpRect);
      point.X -= lpRect.Left;
      point.Y -= lpRect.Top;
      return point;
    }

    public int WMNCHitTest(Point p, int hitTest)
    {
      bool flag = true;
      this.BorderTopRect = new Rectangle(0, 0, this.Width, 5);
      this.BorderBottomRect = new Rectangle(0, this.Height - 5, this.Width, 5);
      this.BorderLeftRect = new Rectangle(0, 0, 5, this.Height);
      this.BorderRightRect = new Rectangle(this.Width - 5, 0, 5, this.Height);
      int num = hitTest;
      if (flag)
      {
        Rectangle rectangle1 = this.BorderBottomRect;
        Rectangle rectangle2 = this.BorderTopRect;
        if (rectangle2.Height < 2)
          rectangle2.Height = 3 - rectangle2.Height;
        if (rectangle1.Height < 2)
        {
          rectangle1.Height += 2;
          rectangle1.Y -= 2;
        }
        if (rectangle2.Contains(p))
          num = 12;
        if (this.BorderLeftRect.Contains(p))
          num = 10;
        if (this.BorderRightRect.Contains(p))
          num = 11;
        if (rectangle1.Contains(p))
          num = 15;
        if (new Rectangle(this.BorderLeftRect.X, rectangle1.Y, 16, rectangle1.Height).Contains(p))
          num = 16;
        if (new Rectangle(this.BorderRightRect.Right - 16, rectangle1.Y, 16, rectangle1.Height).Contains(p))
          num = 17;
        if (new Rectangle(0, 0, 16, rectangle2.Height).Contains(p))
          num = 13;
        if (new Rectangle(this.BorderRightRect.Right - 16, 0, 16, rectangle2.Height).Contains(p))
          num = 14;
      }
      return num;
    }

    protected virtual void NCHitTest(ref Message msg)
    {
      vForm.CallDwmBase(ref msg);
      switch (msg.Result.ToInt32())
      {
        case 1:
        case 2:
          Point pointFromPtr = this.GetPointFromPtr(msg.LParam);
          msg.Result = new IntPtr(this.WMNCHitTest(pointFromPtr, msg.Result.ToInt32()));
          if (msg.Result.ToInt32() != 1)
            break;
          if (pointFromPtr.Y < this.margins.cyTopHeight)
          {
            msg.Result = new IntPtr(2);
            break;
          }
          msg.Result = new IntPtr(5);
          break;
      }
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
      if (!this.AllowMaximize)
        return;
      this.canResizeSet = false;
      if (this.WindowState == FormWindowState.Maximized)
      {
        this.WindowState = FormWindowState.Normal;
        this.Padding = this.currentPadding;
        this.BoundsHelper.EnableResize = this.canResize;
      }
      else
      {
        this.WindowState = FormWindowState.Maximized;
        this.canResize = this.BoundsHelper.EnableResize;
        this.BoundsHelper.EnableResize = false;
        this.currentPadding = this.Padding;
        this.Padding = new Padding(0, this.Padding.Top, 0, 0);
      }
      this.canResizeSet = true;
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      if (this.canResizeSet && this.WindowState != FormWindowState.Maximized)
      {
        this.Padding = this.currentPadding;
        this.BoundsHelper.EnableResize = this.canResize;
        this.canResizeSet = false;
        this.Invalidate();
      }
      base.OnMouseEnter(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDoubleClick" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseDoubleClick(MouseEventArgs e)
    {
      if (new Rectangle(this.Padding.Left, 0, this.Width - this.Padding.Horizontal, this.Padding.Top).Contains(e.Location))
        this.HandleMaximize();
      base.OnMouseDoubleClick(e);
    }

    public struct MARGINS
    {
      public int cxLeftWidth;
      public int cxRightWidth;
      public int cyTopHeight;
      public int cyBottomHeight;
    }

    internal struct Margins
    {
      public int Left;
      public int Right;
      public int Top;
      public int Bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class MINMAXINFO
    {
      public vForm.POINT ptReserved;
      public vForm.POINT ptMaxSize;
      public vForm.POINT ptMaxPosition;
      public vForm.POINT ptMinTrackSize;
      public vForm.POINT ptMaxTrackSize;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class POINT
    {
      public int x;
      public int y;

      public POINT()
      {
      }

      public POINT(int x, int y)
      {
        this.x = x;
        this.y = y;
      }
    }

    public struct NCCALCSIZE_PARAMS
    {
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
      public NativeFunctions.RECT[] rgrc;
      public IntPtr lppos;
    }

    public static class Util
    {
      private static int GetEmbeddedNullStringLengthAnsi(string s)
      {
        int length = s.IndexOf(char.MinValue);
        if (length > -1)
          return vForm.Util.GetPInvokeStringLength(s.Substring(0, length)) + vForm.Util.GetEmbeddedNullStringLengthAnsi(s.Substring(length + 1)) + 1;
        return vForm.Util.GetPInvokeStringLength(s);
      }

      public static int GetPInvokeStringLength(string s)
      {
        if (s == null)
          return 0;
        if (Marshal.SystemDefaultCharSize == 2)
          return s.Length;
        if (s.Length == 0)
          return 0;
        if (s.IndexOf(char.MinValue) > -1)
          return vForm.Util.GetEmbeddedNullStringLengthAnsi(s);
        return vForm.Util.lstrlen(s);
      }

      public static int HIWORD(int n)
      {
        return n >> 16 & (int) ushort.MaxValue;
      }

      public static int HIWORD(IntPtr n)
      {
        return vForm.Util.HIWORD((int) (long) n);
      }

      public static int LOWORD(int n)
      {
        return n & (int) ushort.MaxValue;
      }

      public static int LOWORD(IntPtr n)
      {
        return vForm.Util.LOWORD((int) (long) n);
      }

      [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
      private static extern int lstrlen(string s);

      public static int MAKELONG(int low, int high)
      {
        return high << 16 | low & (int) ushort.MaxValue;
      }

      public static IntPtr MAKELPARAM(int low, int high)
      {
        return (IntPtr) (high << 16 | low & (int) ushort.MaxValue);
      }

      public static int SignedHIWORD(int n)
      {
        return (int) (short) (n >> 16 & (int) ushort.MaxValue);
      }

      public static int SignedHIWORD(IntPtr n)
      {
        return vForm.Util.SignedHIWORD((int) (long) n);
      }

      public static int SignedLOWORD(int n)
      {
        return (int) (short) (n & (int) ushort.MaxValue);
      }

      public static int SignedLOWORD(IntPtr n)
      {
        return vForm.Util.SignedLOWORD((int) (long) n);
      }
    }

    public struct RECT
    {
      public int Left;
      public int Top;
      public int Right;
      public int Bottom;

      public RECT(int l, int t, int r, int b)
      {
        this.Left = l;
        this.Top = t;
        this.Right = r;
        this.Bottom = b;
      }

      public RECT(Rectangle r)
      {
        this.Left = r.Left;
        this.Top = r.Top;
        this.Right = r.Right;
        this.Bottom = r.Bottom;
      }

      public Rectangle ToRectangle()
      {
        return Rectangle.FromLTRB(this.Left, this.Top, this.Right, this.Bottom);
      }
    }
  }
}
