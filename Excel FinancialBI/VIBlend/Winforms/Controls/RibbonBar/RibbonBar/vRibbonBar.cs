// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vRibbonBar
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vRibbonBar control.</summary>
  [ToolboxItem(true)]
  [Description("Displays a Ribbon bar control where the user can add groups, galleries and other controls.")]
  [ToolboxBitmap(typeof (vRibbonBar), "ControlIcons.vRibbonBar.ico")]
  [Designer("VIBlend.WinForms.Controls.Design.vRibbonBarDesigner, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  public class vRibbonBar : vTabControl
  {
    private Stack<vRibbonGroup> groups = new Stack<vRibbonGroup>();
    private ContentAlignment textAlignment = ContentAlignment.MiddleCenter;
    private int contextXOffset = 9999;
    private bool drawQATBackground = true;
    private bool drawTitleFill = true;
    private bool allowRibbonRendering = true;
    private Hashtable groupsTable = new Hashtable();
    private vRibbonApplicationButton applicationButton;
    private QuickAccessToolbar qat;
    private BackgroundElement backElement;
    private bool allowMinimize;
    private Size savedMaximizedSize;
    private Size savedMinimizedSize;
    private int lastWidth;
    private string descriptionText;
    private Color descriptionTextColor;
    private Font descriptionTextFont;
    private int contextXRightOffset;
    private bool allowParentFormDrag;
    private RibbonStyle ribbonStyle;
    private Font defaultFont;
    private DateTime mouseUp;
    private Point initialPosition;

    /// <summary>Gets or sets the ribbon style.</summary>
    /// <value>The ribbon style.</value>
    [Description("Gets or sets the ribbon style.")]
    [Category("Appearance")]
    public RibbonStyle RibbonStyle
    {
      get
      {
        return this.ribbonStyle;
      }
      set
      {
        this.ribbonStyle = value;
        this.SetRibbonStyle();
      }
    }

    /// <summary>Gets or sets the initial offset for the tab pages.</summary>
    /// <value></value>
    [Browsable(false)]
    [DefaultValue(54)]
    public override int TabsInitialOffset
    {
      get
      {
        return base.TabsInitialOffset;
      }
      set
      {
        base.TabsInitialOffset = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether parent form could be moved and resized using ribbon's caption
    /// </summary>
    [DefaultValue(false)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether parent form could be moved and resized using ribbon's caption")]
    public bool AllowParentFormDrag
    {
      get
      {
        return this.allowParentFormDrag;
      }
      set
      {
        this.allowParentFormDrag = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether should draw qat background
    /// </summary>
    [DefaultValue(true)]
    [Category("Appearance")]
    [Description("Gets or sets a value indicating whether should draw qat background")]
    public bool DrawQuickAccessToolbarBackground
    {
      get
      {
        return this.drawQATBackground;
      }
      set
      {
        this.drawQATBackground = value;
      }
    }

    /// <summary>Gets or sets the  text.</summary>
    /// <value>The  text.</value>
    [Category("Behavior")]
    [Description("Gets or sets the  text.")]
    [Browsable(true)]
    public new string Text
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

    /// <summary>Gets or sets the description text.</summary>
    /// <value>The description text.</value>
    [Category("Behavior")]
    [Description("Gets or sets the description text.")]
    [DefaultValue(null)]
    public string DescriptionText
    {
      get
      {
        return this.descriptionText;
      }
      set
      {
        this.descriptionText = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the text alignment.</summary>
    /// <value>The text alignment.</value>
    [Category("Appearance")]
    [Description("Gets or sets the text alignment.")]
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

    /// <summary>Gets or sets the description text font.</summary>
    /// <value>The description text font.</value>
    [Category("Behavior")]
    [Description("Gets or sets the description text font.")]
    public Font DescriptionTextFont
    {
      get
      {
        if (this.descriptionTextFont == null)
          this.descriptionTextFont = this.Font;
        return this.descriptionTextFont;
      }
      set
      {
        this.descriptionTextFont = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the color of the description text.</summary>
    /// <value>The color of the description text.</value>
    [Description("Gets or sets the color of the description text.")]
    [Category("Appearance")]
    public Color DescriptionTextColor
    {
      get
      {
        return this.descriptionTextColor;
      }
      set
      {
        this.descriptionTextColor = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the font of the text displayed by the control.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property.
    /// </returns>
    /// <PermissionSet>
    /// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
    /// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [Category("Appearance")]
    [Description("Gets or sets the control's Font.")]
    public new Font Font
    {
      get
      {
        return base.Font;
      }
      set
      {
        base.Font = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether drop down style is enabled.
    /// </summary>
    /// <value></value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override bool EnableDropDownStyle
    {
      get
      {
        return base.EnableDropDownStyle;
      }
      set
      {
        base.EnableDropDownStyle = value;
      }
    }

    /// <summary>Gets or sets the height of all pages.</summary>
    /// <value>The height of all pages.</value>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override int AllPagesHeight
    {
      get
      {
        return -1;
      }
      set
      {
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override bool UseTabsAreaBackColor
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    [Browsable(false)]
    public override Color TabsAreaBackColor
    {
      get
      {
        return base.TabsAreaBackColor;
      }
      set
      {
        base.TabsAreaBackColor = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DefaultValue(true)]
    public override bool FitTabsToBounds
    {
      get
      {
        return true;
      }
      set
      {
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override vTabPageTextOrientation TextOrientation
    {
      get
      {
        return vTabPageTextOrientation.Horizontal;
      }
      set
      {
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override vTabPageAlignment TabAlignment
    {
      get
      {
        return vTabPageAlignment.Top;
      }
      set
      {
      }
    }

    /// <summary>
    /// Gets or sets the theme of the control using one of the built-in themes.
    /// </summary>
    /// <value></value>
    [Description("Gets or sets the theme of the control using one of the built-in themes.")]
    [Category("Appearance")]
    public override VIBLEND_THEME VIBlendTheme
    {
      get
      {
        return base.VIBlendTheme;
      }
      set
      {
        base.VIBlendTheme = value;
        if (this.qat != null)
          this.qat.VIBlendTheme = value;
        if (this.applicationButton != null)
          this.applicationButton.VIBlendTheme = value;
        foreach (vTabPage tabPage in this.TabPages)
        {
          tabPage.VIBlendTheme = value;
          tabPage.tabsFill.IsAnimated = false;
          tabPage.backGround.IsAnimated = false;
          if (tabPage.LayoutPanel != null)
          {
            tabPage.LayoutPanel.VIBlendTheme = value;
            for (int index = 0; index < tabPage.LayoutPanel.Controls.Count; ++index)
            {
              vRibbonGroup vRibbonGroup = tabPage.LayoutPanel.Controls[index] as vRibbonGroup;
              if (vRibbonGroup != null)
                vRibbonGroup.VIBlendTheme = value;
            }
          }
        }
      }
    }

    /// <summary>Gets or sets the tab contexts.</summary>
    /// <value>The tab context.</value>
    [Browsable(true)]
    [Category("Behavior")]
    [Description("Gets or sets the tab contexts. Define the RibbonBar contextual tabs.")]
    public override List<TabContext> TabContexts
    {
      get
      {
        return base.TabContexts;
      }
      set
      {
        base.TabContexts = value;
      }
    }

    [DefaultValue(true)]
    [Browsable(false)]
    public bool DrawTitleFill
    {
      get
      {
        return this.drawTitleFill;
      }
      set
      {
        this.drawTitleFill = value;
      }
    }

    /// <summary>
    /// Gets or sets whether the default rendering is enabled.
    /// </summary>
    [Category("Appearance")]
    [DefaultValue(true)]
    [Description("Gets or sets whether the default rendering is enabled.")]
    public bool AllowRibbonRendering
    {
      get
      {
        return this.allowRibbonRendering;
      }
      set
      {
        this.allowRibbonRendering = value;
        this.Invalidate();
      }
    }

    [Description("Gets or sets the theme of the control.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Category("Appearance")]
    public override ControlTheme Theme
    {
      get
      {
        return base.Theme;
      }
      set
      {
        if (this.backElement == null)
          this.backElement = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
        ControlTheme theme = ThemeCache.GetTheme(this.StyleKey, this.VIBlendTheme);
        ThemeCache.GetTheme(this.StyleKey + "1", this.VIBlendTheme);
        FillStyle fillStyle = theme.QueryFillStyleSetter("RibbonTitle");
        if (fillStyle != null)
          theme.StyleNormal.FillStyle = fillStyle;
        Color color = theme.QueryColorSetter("RibbonFormBorder");
        if (color != Color.Empty)
          theme.StyleNormal.BorderColor = color;
        this.backElement.LoadTheme(theme);
        this.backElement.IsAnimated = false;
        base.Theme = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control can be minimized.
    /// </summary>
    [DefaultValue(false)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether the control can be minimized.")]
    public bool AllowMinimizedState
    {
      get
      {
        return this.allowMinimize;
      }
      set
      {
        this.allowMinimize = value;
      }
    }

    /// <summary>
    /// Gets or sets the QuickAccessToolbar associated to the RibbonBar control.
    /// </summary>
    /// <value>The QuickAccessToolbar.</value>
    [DefaultValue(null)]
    [Description("Gets or sets the QuickAccessToolbar instance associated to the RibbonBar control.")]
    [Category("Behavior")]
    public QuickAccessToolbar QuickAccessToolbar
    {
      get
      {
        return this.qat;
      }
      set
      {
        if (value == null && this.qat != null)
        {
          this.Controls.Remove((Control) this.qat);
          this.qat.Parent = (Control) this.FindForm();
          this.qat.Location = new Point(100, 200);
        }
        this.qat = value;
        if (value == null)
          return;
        this.Controls.Add((Control) this.qat);
        this.qat.BackColor = Color.Transparent;
        this.qat.drawBackground = false;
        if (this.ApplicationButton != null)
          this.qat.Location = new Point(this.ApplicationButton.Right + 10, 3);
        else
          this.qat.Location = new Point(34, 3);
        value.VIBlendTheme = this.VIBlendTheme;
      }
    }

    /// <summary>Gets or sets the application button.</summary>
    /// <value>The application button.</value>
    [Category("Behavior")]
    [DefaultValue(null)]
    [Description("Gets or sets the application button.")]
    public vRibbonApplicationButton ApplicationButton
    {
      get
      {
        return this.applicationButton;
      }
      set
      {
        if (value == null && this.applicationButton != null)
        {
          --this.otherControls;
          this.Controls.Remove((Control) this.ApplicationButton);
          this.ApplicationButton.Parent = (Control) this.FindForm();
          this.ApplicationButton.Location = new Point(100, 200);
        }
        this.applicationButton = value;
        if (value == null)
          return;
        this.Controls.Add((Control) this.ApplicationButton);
        this.Controls.SetChildIndex((Control) this.ApplicationButton, 0);
        this.ApplicationButton.Dock = DockStyle.None;
        this.ApplicationButton.BackColor = Color.Transparent;
        this.ApplicationButton.Location = new Point(5, 7);
        ++this.otherControls;
        value.VIBlendTheme = this.VIBlendTheme;
      }
    }

    static vRibbonBar()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vRibbonBar" /> class.
    /// </summary>
    public vRibbonBar()
    {
      this.StyleKey = "RibbonBar";
      this.ResizeRedraw = true;
      this.TitleHeight = 54;
      this.defaultFont = new Font("Tahoma", this.Font.Size, this.Font.Style);
      this.VIBlendTheme = VIBLEND_THEME.OFFICEBLUE;
      this.TabsInitialOffset = 45;
      this.DockPadding.Right = 0;
      this.DockPadding.Left = 0;
      this.DockPadding.Top = 0;
      this.DockPadding.Bottom = 0;
      this.ControlAdded += new ControlEventHandler(this.vRibbonBar_ControlAdded);
      this.FitTabsToBounds = true;
      this.Dock = DockStyle.Top;
      this.Height = 138;
      this.MinimumSize = new Size(0, 138);
      this.MaximumSize = new Size(0, 138);
      this.TabPages.CollectionChanged += new EventHandler(this.TabPages_CollectionChanged);
      this.PopupPaint += new PaintEventHandler(this.vRibbonBar_PopupPaint);
      this.DescriptionTextFont = this.Font;
      this.DescriptionTextColor = Color.FromArgb(53, 110, 184);
      this.AllowDragDrop = false;
      this.Font = new Font("Tahoma", this.Font.Size, this.Font.Style);
      this.EnableDropDownStyle = false;
      this.allowAnimations = false;
      this.ShowFocusRectangle = false;
      this.EnableToolTips = false;
    }

    private void SetRibbonStyle()
    {
      if (this.ApplicationButton != null)
        this.ApplicationButton.RoundedButton.ButtonStyle = this.RibbonStyle;
      foreach (vTabPage tabPage in this.TabPages)
      {
        foreach (Control control in (ArrangedElementCollection) tabPage.LayoutPanel.Controls)
        {
          if (control is vRibbonGroup)
            (control as vRibbonGroup).GroupStyle = this.RibbonStyle;
        }
      }
      if (this.ApplicationButton != null)
      {
        if (this.RibbonStyle == RibbonStyle.Office2007)
        {
          this.ApplicationButton.Location = new Point(5, 6);
          this.ApplicationButton.RoundedButton.Size = new Size(38, 38);
          this.TabsInitialOffset = 45;
        }
        else
        {
          this.ApplicationButton.Bounds = new Rectangle(0, 30, 53, this.TitleHeight - 29);
          this.ApplicationButton.RoundedButton.Size = new Size(53, this.TitleHeight - 29);
          this.TabsInitialOffset = 55;
        }
      }
      if (this.qat == null)
        return;
      this.qat.ToolbarStyle = this.RibbonStyle;
      if (this.RibbonStyle == RibbonStyle.Office2007)
      {
        if (this.ApplicationButton != null)
          this.qat.Location = new Point(this.ApplicationButton.Right + 10, 3);
        else
          this.qat.Location = new Point(34, 3);
      }
      else
        this.qat.Location = new Point(0, 3);
    }

    private void ResetTextAlignment()
    {
      this.TextAlignment = ContentAlignment.MiddleCenter;
    }

    private bool ShouldSerializeTextAlignment()
    {
      return this.TextAlignment != ContentAlignment.MiddleCenter;
    }

    private void ResetDescriptionTextFont()
    {
      this.DescriptionTextFont = Control.DefaultFont;
    }

    private bool ShouldSerializeDescriptionTextFont()
    {
      return !this.DescriptionTextFont.Equals((object) Control.DefaultFont);
    }

    /// <summary>
    /// Resets the <see cref="P:System.Windows.Forms.Control.Font" /> property to its default value.
    /// </summary>
    /// <PermissionSet>
    /// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
    /// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public override void ResetFont()
    {
      this.Font = this.defaultFont;
    }

    private new bool ShouldSerializeFont()
    {
      return this.Font != null && !this.Font.Equals((object) this.defaultFont) && !this.Font.Equals((object) Control.DefaultFont);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.ParentChanged" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnParentChanged(EventArgs e)
    {
      base.OnParentChanged(e);
      vForm vForm = this.Parent as vForm;
    }

    private void vRibbonBar_PopupPaint(object sender, PaintEventArgs e)
    {
      if (this.TabPages.Count <= 0)
        return;
      Size size = this.dropDown.Size;
      this.TabPages[0].backGround.Bounds = new Rectangle(0, 0, size.Width, size.Height);
      this.TabPages[0].backGround.DrawElementFill(e.Graphics, ControlState.Normal);
    }

    private void TabPages_CollectionChanged(object sender, EventArgs e)
    {
    }

    private void vRibbonBar_ControlAdded(object sender, ControlEventArgs e)
    {
      vTabPage vTabPage = e.Control as vTabPage;
      if (vTabPage != null)
      {
        vTabPage.Dock = DockStyle.None;
        vTabPage.Bounds = new Rectangle(0, this.TitleHeight, this.Width, this.Height - this.TitleHeight);
      }
      if (this.DesignMode)
        return;
      foreach (vTabPage tabPage in this.TabPages)
        tabPage.VIBlendTheme = this.VIBlendTheme;
    }

    /// <summary>Raises the MouseUp event.</summary>
    /// <param name="e"></param>
    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      this.Capture = false;
      if (this.DesignMode)
        return;
      this.dropDownSize.Width = this.Width;
      if (!this.AllowMinimizedState)
        return;
      TimeSpan timeSpan = DateTime.Now - this.mouseUp;
      this.mouseUp = DateTime.Now;
      if (this.SelectedTab != null)
      {
        using (Graphics graphics = this.CreateGraphics())
        {
          if (!this.RectangleToScreen(this.GetPageRectangle(graphics, this.SelectedTab)).Contains(Cursor.Position))
            return;
        }
      }
      if (timeSpan.TotalSeconds < 0.1 || timeSpan.TotalSeconds >= 0.3)
        return;
      if (!this.EnableDropDownStyle)
      {
        this.savedMaximizedSize = this.MaximumSize;
        this.savedMinimizedSize = this.MinimumSize;
        this.MinimumSize = Size.Empty;
        this.MaximumSize = Size.Empty;
      }
      else
      {
        this.MaximumSize = this.savedMaximizedSize;
        this.MinimumSize = this.savedMinimizedSize;
      }
      this.EnableDropDownStyle = !this.EnableDropDownStyle;
      this.mouseUp = DateTime.MinValue;
    }

    /// <summary>Raises the MouseMove event.</summary>
    /// <param name="e"></param>
    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (!this.AllowParentFormDrag || this.DesignMode)
        return;
      Rectangle rectangle = new Rectangle(0, 0, this.Width, 29);
      if (!this.Capture || !(this.initialPosition != Point.Empty))
        return;
      Form form = this.FindForm();
      if (form == null)
        return;
      form.Location = new Point(form.Location.X + e.Location.X - this.initialPosition.X, form.Location.Y + e.Location.Y - this.initialPosition.Y);
    }

    /// <summary>Raises the MouseDown event.</summary>
    /// <param name="e"></param>
    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (!this.AllowParentFormDrag)
        return;
      if (new Rectangle(0, 0, this.Width, 29).Contains(e.Location))
      {
        this.initialPosition = e.Location;
        this.Capture = true;
      }
      else
        this.initialPosition = Point.Empty;
    }

    /// <exclude />
    protected override void OnLostFocus(EventArgs e)
    {
      base.OnLostFocus(e);
      this.Invalidate();
    }

    /// <exclude />
    protected override void OnMouseDoubleClick(MouseEventArgs e)
    {
      base.OnMouseDoubleClick(e);
    }

    /// <exclude />
    protected override void OnDoubleClick(EventArgs e)
    {
      base.OnDoubleClick(e);
    }

    /// <exclude />
    protected override void DrawContexts(Graphics g)
    {
      if (this.TabAlignment != vTabPageAlignment.Top)
        return;
      this.contextXOffset = 9999;
      this.contextXRightOffset = 0;
      foreach (TabContext tabContext in this.TabContexts)
      {
        if (tabContext.Visible)
        {
          int num = -1;
          int width = 0;
          int val1 = 0;
          List<vTabPage> pagesByContext = this.GetPagesByContext(tabContext);
          foreach (vTabPage page in pagesByContext)
          {
            Rectangle pageRectangle = this.GetPageRectangle(g, page);
            if (num == -1)
              num = pageRectangle.X;
            if (num > pageRectangle.X)
              num = pageRectangle.X;
            width += pageRectangle.Width + this.TabsSpacing;
            val1 = Math.Max(val1, pageRectangle.Height);
          }
          if (num > -1)
          {
            this.contextXOffset = Math.Min(num, this.contextXOffset);
            this.contextXRightOffset = Math.Max(this.contextXRightOffset, num + width);
          }
          if (pagesByContext.Count > 0)
          {
            Rectangle rectangle = new Rectangle(num, 0, width, this.TitleHeight - val1 - 2);
            if (rectangle.Width > 0 && rectangle.Height > 0)
            {
              ControlTheme copy = this.Theme.CreateCopy();
              copy.StyleNormal.FillStyle.Colors[0] = Color.FromArgb(0, tabContext.TabColor);
              copy.StyleNormal.FillStyle.Colors[1] = Color.FromArgb(35, tabContext.TabColor);
              if (copy.StyleNormal.FillStyle.ColorsNumber > 2)
                copy.StyleNormal.FillStyle.Colors[2] = Color.FromArgb(55, tabContext.TabColor);
              else
                copy.StyleNormal.FillStyle.Colors[1] = Color.FromArgb(205, tabContext.TabColor);
              if (copy.StyleNormal.FillStyle.ColorsNumber > 3)
                copy.StyleNormal.FillStyle.Colors[3] = Color.FromArgb(205, tabContext.TabColor);
              BackgroundElement backgroundElement = new BackgroundElement(rectangle, (IScrollableControlBase) this);
              backgroundElement.LoadTheme(copy);
              backgroundElement.DrawStandardFillWithCustomGradientOffsets(g, ControlState.Normal, GradientStyles.Linear, 90.0, 0.7, 0.9);
              using (Pen pen = new Pen(Color.FromArgb(15, tabContext.TabColor)))
              {
                g.DrawLine(pen, rectangle.Right, rectangle.Top, rectangle.Right, this.TitleHeight);
                g.DrawLine(pen, rectangle.Left, rectangle.Top, rectangle.Left, this.TitleHeight);
              }
              StringFormat format = new StringFormat();
              format.LineAlignment = StringAlignment.Center;
              format.Alignment = StringAlignment.Center;
              format.FormatFlags |= StringFormatFlags.NoWrap;
              format.Trimming = StringTrimming.EllipsisCharacter;
              using (SolidBrush solidBrush1 = new SolidBrush(tabContext.ForeColor))
              {
                if (WindowsAPI.IsGlassEnabled)
                {
                  SmoothingMode smoothingMode = g.SmoothingMode;
                  g.SmoothingMode = SmoothingMode.HighQuality;
                  using (GraphicsPath path = new GraphicsPath())
                  {
                    if (rectangle.Width >= 0)
                    {
                      path.AddString(this.Text, this.Font.FontFamily, (int) this.Font.Style, this.Font.SizeInPoints + 3f, rectangle, format);
                      using (SolidBrush solidBrush2 = new SolidBrush(tabContext.ForeColor))
                        g.FillPath((Brush) solidBrush2, path);
                    }
                  }
                  g.SmoothingMode = smoothingMode;
                }
                else
                  g.DrawString(tabContext.Text, this.Font, (Brush) solidBrush1, (RectangleF) rectangle, format);
              }
            }
          }
        }
      }
    }

    /// <exclude />
    protected internal virtual void DrawQatBackground(Graphics graphics, Rectangle bounds, Point roundPoint)
    {
      SmoothingMode smoothingMode = graphics.SmoothingMode;
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      int int32 = Convert.ToInt32(Math.Sqrt(Math.Pow((double) (bounds.X - roundPoint.X), 2.0) + Math.Pow((double) (bounds.Y - roundPoint.Y), 2.0)));
      using (GraphicsPath path = new GraphicsPath())
      {
        Rectangle rect = new Rectangle(roundPoint.X - int32, roundPoint.Y - int32, int32 * 2, int32 * 2);
        float num1 = (float) (Math.Acos((double) (roundPoint.Y - bounds.Bottom + 1) / (double) int32) * 180.0 / Math.PI);
        float num2 = (float) (Math.Acos((double) (roundPoint.Y - bounds.Y) / (double) int32) * 180.0 / Math.PI);
        path.AddArc(bounds.Right - bounds.Height, bounds.Y, bounds.Height, bounds.Height - 1, 270f, 180f);
        path.AddArc(rect, num1 - 90f, num2 - num1);
        path.CloseAllFigures();
        using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(this.ClientRectangle, ControlPaint.Light(this.backElement.Theme.StyleNormal.FillStyle.Colors[0]), ControlPaint.Dark(this.backElement.Theme.StyleNormal.FillStyle.Colors[1]), 90f))
        {
          using (Pen pen = new Pen(Color.FromArgb(200, ControlPaint.LightLight(this.backElement.BorderColor))))
          {
            graphics.FillPath((Brush) linearGradientBrush, path);
            graphics.DrawPath(pen, path);
          }
        }
      }
      graphics.SmoothingMode = smoothingMode;
    }

    /// <summary>
    /// </summary>
    /// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
    protected override void WndProc(ref Message m)
    {
      bool flag1 = false;
      if (WindowsAPI.IsWindows && this.Parent is OfficeRibbonForm && (!this.DesignMode && m.Msg == 132))
      {
        Form form = this.FindForm();
        int left = this.QuickAccessToolbar != null ? this.QuickAccessToolbar.Bounds.Right : 0;
        if (this.QuickAccessToolbar != null)
          left = this.QuickAccessToolbar.Bounds.Right;
        Rectangle r = Rectangle.FromLTRB(left, 0, this.Width, 29);
        Point point = new Point(WindowsAPI.LoWord((int) m.LParam), WindowsAPI.HiWord((int) m.LParam));
        this.PointToClient(point);
        bool flag2 = false;
        if (this.RectangleToScreen(r).Contains(point) && !flag2)
        {
          Point screen = this.PointToScreen(point);
          WindowsAPI.SendMessage(form.Handle, 132, m.WParam, WindowsAPI.MakeLParam(screen.X, screen.Y));
          m.Result = new IntPtr(-1);
          flag1 = true;
        }
      }
      if (flag1)
        return;
      base.WndProc(ref m);
    }

    /// <summary>Raises the Paint event.</summary>
    /// <param name="p"></param>
    protected override void OnPaint(PaintEventArgs p)
    {
      if (!this.AllowRibbonRendering)
      {
        base.OnPaint(p);
      }
      else
      {
        OfficeRibbonForm officeRibbonForm = this.Parent as OfficeRibbonForm;
        if (officeRibbonForm != null && !this.DesignMode)
          this.DrawTitleBackground(p.Graphics, 29);
        base.OnPaint(p);
        Rectangle rectangle1 = new Rectangle(1, 0, this.Width - 2, 29);
        if (this.FindForm() is RibbonForm)
          rectangle1 = new Rectangle(-1, 0, this.Width + 2, 29);
        if (this.drawTitleFill && this.DesignMode && officeRibbonForm != null)
        {
          this.backElement.Bounds = rectangle1;
          this.backElement.DrawStandardFill(p.Graphics, ControlState.Normal, GradientStyles.Linear);
        }
        if (this.drawTitleFill && officeRibbonForm == null)
        {
          this.backElement.Bounds = rectangle1;
          this.backElement.DrawStandardFill(p.Graphics, ControlState.Normal, GradientStyles.Linear);
        }
        else if (this.drawTitleFill && officeRibbonForm != null && !WindowsAPI.IsGlassEnabled)
        {
          rectangle1 = new Rectangle(0, 0, this.Width, 29);
          int radius = this.backElement.Radius;
          this.backElement.Radius = 0;
          this.backElement.Bounds = rectangle1;
          this.backElement.DrawStandardFill(p.Graphics, ControlState.Normal, GradientStyles.Linear);
          this.backElement.Radius = radius;
        }
        Color color = this.Theme.QueryColorSetter("RibbonFormBorder2");
        if (color != Color.Empty)
        {
          using (Pen pen = new Pen(color))
          {
            if (!(this.Parent is OfficeRibbonForm))
              p.Graphics.DrawLine(pen, 0, 0, this.Width, 0);
          }
        }
        if (this.qat != null)
        {
          int num = 0;
          if (this.ApplicationButton != null)
            num = this.ApplicationButton.Right - 30;
          if (this.drawQATBackground && this.RibbonStyle == RibbonStyle.Office2007)
          {
            this.DrawQatBackground(p.Graphics, new Rectangle(1 + num + 5, 3, Math.Max(15, this.qat.Width + 15), 22), new Point(15 + num, 22));
            int width = this.qat.Width;
          }
        }
        this.DrawContexts(p.Graphics);
        StringFormat stringFormat = ImageAndTextHelper.GetStringFormat((Control) this, this.TextAlignment, true, false);
        stringFormat.FormatFlags |= StringFormatFlags.NoWrap;
        this.contextXOffset = this.contextXOffset != 9999 ? this.Width - this.contextXOffset : 0;
        using (SolidBrush solidBrush1 = new SolidBrush(this.ForeColor))
        {
          Size size1 = Size.Ceiling(p.Graphics.MeasureString(this.DescriptionText, this.DescriptionTextFont));
          Size size2 = Size.Ceiling(p.Graphics.MeasureString(this.Text, this.Font));
          int num1 = 0;
          int num2 = 0;
          if (this.qat != null)
            num2 = this.qat.Right;
          int width1 = size1.Width;
          int width2 = size2.Width;
          if (this.TextAlignment != ContentAlignment.MiddleCenter)
            num1 = num2;
          Rectangle layoutRect = new Rectangle(rectangle1.X + num1, rectangle1.Y, rectangle1.Width - size1.Width - this.contextXOffset - num1, rectangle1.Height);
          Rectangle rectangle2 = new Rectangle(layoutRect.X + size2.Width, rectangle1.Y, rectangle1.Width - size2.Width - this.contextXOffset - num1, rectangle1.Height);
          if (this.contextXOffset > rectangle1.Width / 2)
          {
            layoutRect = new Rectangle(this.contextXRightOffset, rectangle1.Y, rectangle1.Width - this.contextXRightOffset - 100, rectangle1.Height);
            rectangle2 = new Rectangle(layoutRect.X + size2.Width, rectangle1.Y, rectangle1.Width - size2.Width, rectangle1.Height);
          }
          if (this.TextAlignment == ContentAlignment.MiddleCenter && num2 > layoutRect.Width / 2 - size1.Width / 2)
          {
            int num3 = num2;
            layoutRect = new Rectangle(rectangle1.X + num3, rectangle1.Y, rectangle1.Width - size1.Width - this.contextXOffset - num3, rectangle1.Height);
            rectangle2 = new Rectangle(layoutRect.X + size2.Width, rectangle1.Y, rectangle1.Width - size2.Width - this.contextXOffset - num3, rectangle1.Height);
          }
          if (officeRibbonForm != null && WindowsAPI.IsGlassEnabled && !this.DesignMode)
          {
            SmoothingMode smoothingMode = p.Graphics.SmoothingMode;
            p.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            using (GraphicsPath path = new GraphicsPath())
            {
              if (this.RibbonStyle == RibbonStyle.Office2007 && this.ApplicationButton != null)
              {
                layoutRect.X = this.ApplicationButton.Right;
                layoutRect.Width -= this.ApplicationButton.Right;
              }
              if (this.QuickAccessToolbar != null)
              {
                layoutRect.X = this.QuickAccessToolbar.Right;
                layoutRect.Width -= this.QuickAccessToolbar.Right;
              }
              if (layoutRect.Width >= 0)
              {
                path.AddString(this.Text + this.DescriptionText, this.Font.FontFamily, (int) this.Font.Style, this.Font.SizeInPoints + 3f, layoutRect, stringFormat);
                using (SolidBrush solidBrush2 = new SolidBrush(this.ForeColor))
                  p.Graphics.FillPath((Brush) solidBrush2, path);
              }
            }
            p.Graphics.SmoothingMode = smoothingMode;
          }
          else
          {
            p.Graphics.DrawString(this.Text, this.Font, (Brush) solidBrush1, (RectangleF) layoutRect, stringFormat);
            using (SolidBrush solidBrush2 = new SolidBrush(this.DescriptionTextColor))
              p.Graphics.DrawString(this.DescriptionText, this.DescriptionTextFont, (Brush) solidBrush2, (RectangleF) rectangle2, stringFormat);
          }
        }
      }
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      foreach (vTabPage tabPage in this.TabPages)
      {
        if (tabPage.Dock != DockStyle.None)
          tabPage.Dock = DockStyle.None;
        tabPage.Bounds = new Rectangle(0, this.TitleHeight, this.Width, this.Height - this.TitleHeight);
      }
    }

    /// <summary>
    /// </summary>
    /// <param name="e"></param>
    /// <exclude />
    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      if (this.ApplicationButton != null)
      {
        if (this.RibbonStyle == RibbonStyle.Office2007)
        {
          this.ApplicationButton.Location = new Point(5, 6);
          this.ApplicationButton.RoundedButton.Size = new Size(38, 38);
        }
        else
        {
          this.ApplicationButton.Bounds = new Rectangle(0, 30, this.ApplicationButton.Width, this.TitleHeight - 29);
          this.ApplicationButton.RoundedButton.Size = new Size(this.ApplicationButton.RoundedButton.Size.Width, this.TitleHeight - 29);
        }
      }
      if (this.qat != null)
      {
        if (this.RibbonStyle == RibbonStyle.Office2007)
        {
          if (this.ApplicationButton != null)
            this.qat.Location = new Point(this.ApplicationButton.Right + 10, 3);
          else
            this.qat.Location = new Point(34, 3);
        }
        else
          this.qat.Location = new Point(0, 3);
      }
      this.PerformChildrenLayout();
    }

    /// <summary>
    /// </summary>
    /// <param name="e"></param>
    /// <exclude />
    protected override void OnSelectedIndexChanged(EventArgs e)
    {
      base.OnSelectedIndexChanged(e);
      if (this.DesignMode)
        return;
      this.PerformChildrenLayout();
    }

    private void DrawTitleBackground(Graphics g, int vOffset)
    {
      Rectangle rect = Rectangle.Empty;
      int width = this.ClientRectangle.Width;
      int height = this.ClientRectangle.Height;
      if (this.TitleHeight < 0)
        ;
      rect = new Rectangle(0, vOffset, width, this.TitleHeight - vOffset);
      this.backGround.Bounds = rect;
      if (this.UseTabsAreaBackColor)
      {
        using (SolidBrush solidBrush = new SolidBrush(this.TabsAreaBackColor))
          g.FillRectangle((Brush) solidBrush, rect);
      }
      else
      {
        Brush brush = (this.Enabled ? this.backGround.Theme.StyleNormal : this.backGround.Theme.StyleDisabled).FillStyle.GetBrush(this.backGround.Bounds);
        g.FillRectangle(brush, rect);
        this.backGround.Bounds = rect;
        this.backGround.DrawElementFill(g, ControlState.None);
      }
    }

    protected override void DrawTitleBackGround(Graphics g)
    {
      Rectangle rect = Rectangle.Empty;
      int width = this.ClientRectangle.Width;
      int height = this.ClientRectangle.Height;
      rect = new Rectangle(0, 0, width, this.TitleHeight);
      this.backGround.Bounds = rect;
      if (this.UseTabsAreaBackColor)
      {
        using (SolidBrush solidBrush = new SolidBrush(this.TabsAreaBackColor))
          g.FillRectangle((Brush) solidBrush, rect);
      }
      else
      {
        Brush brush = (this.Enabled ? this.backGround.Theme.StyleNormal : this.backGround.Theme.StyleDisabled).FillStyle.GetBrush(this.backGround.Bounds);
        g.FillRectangle(brush, rect);
        this.backGround.Bounds = rect;
        this.backGround.DrawElementFill(g, ControlState.None);
      }
    }

    private void PerformChildrenLayout()
    {
      if (this.DesignMode)
        return;
      if (this.lastWidth == 0)
        this.lastWidth = this.Width;
      if (this.SelectedTab == null || this.SelectedTab.LayoutPanel == null)
        return;
      for (int index = this.SelectedTab.LayoutPanel.Controls.Count - 1; index >= 0; --index)
      {
        vRibbonGroup vRibbonGroup = this.SelectedTab.LayoutPanel.Controls[index] as vRibbonGroup;
        if (vRibbonGroup != null && this.SelectedTab.LayoutPanel.Controls[0].Visible && !vRibbonGroup.Collapsed)
        {
          if (!this.groupsTable.Contains((object) vRibbonGroup))
            this.groupsTable.Add((object) vRibbonGroup, (object) vRibbonGroup.Width);
          vRibbonGroup.Collapsed = true;
        }
      }
      int childrenWidth = this.SelectedTab.LayoutPanel.GetChildrenWidth();
      int num1 = this.SelectedTab.LayoutPanel.scrollOffset;
      int startOffset = this.SelectedTab.LayoutPanel.StartOffset;
      for (int index = 0; index < this.SelectedTab.LayoutPanel.Controls.Count; ++index)
      {
        vRibbonGroup vRibbonGroup = this.SelectedTab.LayoutPanel.Controls[index] as vRibbonGroup;
        if (vRibbonGroup != null && vRibbonGroup.Collapsed)
        {
          int num2 = (int) this.groupsTable[(object) vRibbonGroup];
          int width = vRibbonGroup.Width;
          int num3 = num2 - width;
          if (num3 < 0)
            num3 = width - num2;
          if (childrenWidth + num1 + startOffset + num3 < this.Width - 16)
          {
            vRibbonGroup.Collapsed = false;
            childrenWidth += num3;
          }
        }
      }
      this.lastWidth = this.Width;
    }
  }
}
