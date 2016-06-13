// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vTabPage
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a tab page in the vTabControl</summary>
  [Designer("VIBlend.WinForms.Controls.Design.vTabPageDesigner, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxItem(false)]
  public class vTabPage : Panel, IScrollableControlBase, INotifyPropertyChanged
  {
    private bool enableCloseButton = true;
    private string styleKey = "TabPage";
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private bool useTabPageThemeSpecificSetters = true;
    private bool useTabControlTheme = true;
    private bool allowAnimations = true;
    private TextImageRelation relation = TextImageRelation.ImageBeforeText;
    private ContentAlignment textAlignment = ContentAlignment.MiddleCenter;
    private Padding padding = Padding.Empty;
    private int imageIndex = -1;
    private string tooltipText = "";
    private Color contentBorderColor = Color.Black;
    private Color textColor = Color.Black;
    private Color highlightTextColor = Color.Black;
    private Color pressedTextColor = Color.Black;
    private bool useDefaultTextFont = true;
    private bool useThemeTextColor = true;
    private Color contentBackColor = Color.White;
    private bool invisible;
    internal BackgroundElement backGround;
    internal BackgroundElement tabsFill;
    private TabContext tabContext;
    private int? headerHeight;
    private int? headerWidth;
    private Font defaultFont;
    private ControlTheme theme;
    internal ControlTheme themeTab;
    private ControlTheme themePagePanel;
    internal Color glossyColor;
    private bool multiLine;
    internal Rectangle pageRectangle;
    private bool useContentBorderColor;
    private Font textFont;
    private Font selectedTextFont;
    private bool useContentBackColor;

    /// <summary>Gets or sets whether the close buttons is enabled.</summary>
    [Description("Gets or sets whether the close button is enabled.")]
    [Browsable(false)]
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool EnableCloseButton
    {
      get
      {
        return this.enableCloseButton;
      }
      set
      {
        this.enableCloseButton = value;
        this.Invalidate();
        if (this.Parent == null)
          return;
        (this.Parent as vTabControl).initializedPagesWidth = false;
        (this.Parent as vTabControl).InitializeScrollBar();
        this.Parent.Invalidate();
      }
    }

    /// <summary>Gets or sets the height of the header.</summary>
    /// <value>The width of the header.</value>
    [DefaultValue(null)]
    [Category("Behavior")]
    [Description("Gets or sets the height of the header.")]
    public int? HeaderHeight
    {
      get
      {
        return this.headerHeight;
      }
      set
      {
        int? nullable1 = value;
        int? nullable2 = this.headerHeight;
        if ((nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : (nullable1.HasValue != nullable2.HasValue ? 1 : 0)) == 0)
          return;
        this.headerHeight = value;
        this.Invalidate();
        if (this.Parent == null)
          return;
        (this.Parent as vTabControl).initializedPagesWidth = false;
        (this.Parent as vTabControl).InitializeScrollBar();
        this.Parent.Invalidate();
      }
    }

    /// <summary>Gets or sets the width of the header.</summary>
    /// <value>The width of the header.</value>
    [DefaultValue(null)]
    [Description("Gets or sets the width of the header.")]
    [Category("Behavior")]
    public int? HeaderWidth
    {
      get
      {
        return this.headerWidth;
      }
      set
      {
        int? nullable1 = value;
        int? nullable2 = this.headerWidth;
        if ((nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : (nullable1.HasValue != nullable2.HasValue ? 1 : 0)) == 0)
          return;
        this.headerWidth = value;
        this.Invalidate();
        if (this.Parent == null)
          return;
        (this.Parent as vTabControl).initializedPagesWidth = false;
        (this.Parent as vTabControl).InitializeScrollBar();
        this.Parent.Invalidate();
      }
    }

    /// <summary>Gets or sets the layout panel.</summary>
    /// <value>The layout panel.</value>
    [Browsable(false)]
    public vRibbonHorizontalLayoutPanel LayoutPanel
    {
      get
      {
        if (this.Controls.Count > 0 && this.Controls[0] is vRibbonHorizontalLayoutPanel)
          return this.Controls[0] as vRibbonHorizontalLayoutPanel;
        return (vRibbonHorizontalLayoutPanel) null;
      }
    }

    /// <summary>Gets or sets the style key.</summary>
    /// <value>The style key.</value>
    protected internal virtual string StyleKey
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

    /// <summary>Gets or sets the theme of the tab page.</summary>
    [Description("Gets or sets the theme of the tab page.")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("TabPage.Appearance")]
    public ControlTheme Theme
    {
      get
      {
        return this.theme;
      }
      set
      {
        if (value == null || this.theme == value)
          return;
        this.theme = value;
        this.themeTab = value.CreateCopy();
        this.themePagePanel = value.CreateCopy();
        if (this.UseTabPageSpecificThemeSetters)
        {
          this.themePagePanel.StyleNormal.FillStyle = (FillStyle) new FillStyleSolid(this.theme.StyleNormal.FillStyle.Colors[0]);
          this.themePagePanel.Radius = 0.0f;
          FillStyle fillStyle1 = this.theme.QueryFillStyleSetter("TabControlBackGround");
          if (fillStyle1 != null)
            this.themePagePanel.StyleNormal.FillStyle = fillStyle1;
          FillStyle fillStyle2 = this.theme.QueryFillStyleSetter("TabControlContentBackGround");
          if (fillStyle2 != null)
            this.themePagePanel.StyleNormal.FillStyle = fillStyle2;
          FillStyle fillStyle3 = this.theme.QueryFillStyleSetter("RibbonTabBackGround");
          if (fillStyle3 != null && this.Parent is vRibbonBar)
            this.themePagePanel.StyleNormal.FillStyle = fillStyle3;
          FillStyle fillStyle4 = this.theme.QueryFillStyleSetter("TabControlPressed");
          if (fillStyle4 != null)
            this.themeTab.StylePressed.FillStyle = fillStyle4;
          this.glossyColor = this.theme.QueryColorSetter("TabControlPressedAndHighlightedBorder");
          Color color1 = Color.Empty;
          Color color2 = this.theme.QueryColorSetter("TabControlPressedBorder");
          if (color2 != Color.Empty)
            this.themeTab.StylePressed.BorderColor = color2;
          FillStyle fillStyle5 = this.theme.QueryFillStyleSetter("TabControlNormal");
          if (fillStyle5 != null)
            this.themeTab.StyleNormal.FillStyle = fillStyle5;
          Color color3 = Color.Empty;
          Color color4 = this.theme.QueryColorSetter("TabControlTextColorNormal");
          if (color4 != Color.Empty)
            this.themeTab.StyleNormal.TextColor = color4;
          Color color5 = Color.Empty;
          Color color6 = this.theme.QueryColorSetter("TabControlTextColorHighlight");
          if (color6 != Color.Empty)
            this.themeTab.StyleHighlight.TextColor = color6;
          Color color7 = Color.Empty;
          Color color8 = this.theme.QueryColorSetter("TabControlTextColorPressed");
          if (color8 != Color.Empty)
            this.themeTab.StylePressed.TextColor = color8;
          Color color9 = Color.Empty;
          Color color10 = this.theme.QueryColorSetter("TabControlNormalBorder");
          if (color10 != Color.Empty)
            this.themeTab.StyleNormal.BorderColor = color10;
          FillStyle fillStyle6 = this.theme.QueryFillStyleSetter("TabControlHighlight");
          if (fillStyle6 != null)
            this.themeTab.StyleHighlight.FillStyle = fillStyle6;
          Color color11 = Color.Empty;
          Color color12 = this.theme.QueryColorSetter("TabControlHighlightBorder");
          if (color12 != Color.Empty)
            this.themeTab.StyleHighlight.BorderColor = color12;
        }
        this.backGround.LoadTheme(this.themePagePanel);
        this.tabsFill.LoadTheme(this.themeTab);
        this.backGround.IsAnimated = false;
        this.tabsFill.IsAnimated = true;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the theme of the control using one of the built-in themes.
    /// </summary>
    [Category("TabPage.Appearance")]
    [Description("Gets or sets the theme of the TabPage.")]
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
    /// Gets or sets a value whether to use the tab page specific styles defined in the theme.
    /// </summary>
    [DefaultValue(true)]
    [Category("TabPage.Appearance")]
    [Description("Gets or sets a value whether to use the tab page specific styles defined in the theme.")]
    public bool UseTabPageSpecificThemeSetters
    {
      get
      {
        return this.useTabPageThemeSpecificSetters;
      }
      set
      {
        if (value == this.useTabPageThemeSpecificSetters)
          return;
        this.useTabPageThemeSpecificSetters = value;
        this.VIBlendTheme = this.VIBlendTheme;
      }
    }

    /// <summary>
    /// Determines whether the TabPage will use the TabControl's theme or the value from its own Theme properties
    /// </summary>
    [DefaultValue(true)]
    [Category("TabPage.Appearance")]
    public bool UseTabControlTheme
    {
      get
      {
        return this.useTabControlTheme;
      }
      set
      {
        this.useTabControlTheme = value;
      }
    }

    /// <summary>Gets or sets the tab context.</summary>
    /// <value>The tab context.</value>
    [Category("Behavior")]
    [Description("Gets or sets the tab context.")]
    [DefaultValue(null)]
    public TabContext TabContext
    {
      get
      {
        return this.tabContext;
      }
      set
      {
        this.tabContext = value;
        if (value == null)
          return;
        value.PropertyChanged += new PropertyChangedEventHandler(this.value_PropertyChanged);
      }
    }

    /// <exclude />
    [DefaultValue(typeof (Point), "0,0")]
    public new Point Location
    {
      get
      {
        return base.Location;
      }
      set
      {
        base.Location = value;
      }
    }

    /// <summary>Gets the animation manager.</summary>
    /// <value>The animation manager.</value>
    /// <exclude />
    [Browsable(false)]
    public AnimationManager AnimationManager
    {
      get
      {
        return (this.Parent as vTabControl).AnimationManager;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether [allow animations].
    /// </summary>
    /// <value><c>true</c> if [allow animations]; otherwise, <c>false</c>.</value>
    /// <exclude />
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
      }
    }

    /// <summary>Gets or sets the page's dock style</summary>
    [Browsable(false)]
    public new DockStyle Dock
    {
      get
      {
        return base.Dock;
      }
      set
      {
        base.Dock = value;
        this.OnPropertyChanged("Dock");
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:VIBlend.WinForms.Controls.vTabControl" /> is multiline.
    /// </summary>
    /// <value><c>true</c> if multiline; otherwise, <c>false</c>.</value>
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether the text is drawn on multiple lines")]
    [DefaultValue(false)]
    public bool MultilineText
    {
      get
      {
        return this.multiLine;
      }
      set
      {
        this.multiLine = value;
        if (this.Parent != null)
        {
          (this.Parent as vTabControl).initializedPagesWidth = false;
          this.Parent.Invalidate();
        }
        this.OnPropertyChanged("MultilineText");
      }
    }

    /// <summary>Gets or sets the text image relation.</summary>
    /// <value>The text image relation.</value>
    [Category("TabPage.Appearance")]
    [Description("Gets or sets the text image relation.")]
    public TextImageRelation TextImageRelation
    {
      get
      {
        return this.relation;
      }
      set
      {
        this.relation = value;
        if (this.Parent != null)
        {
          (this.Parent as vTabControl).initializedPagesWidth = false;
          this.Parent.Invalidate();
        }
        this.OnPropertyChanged("TextImageRelation");
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
        return this.textAlignment;
      }
      set
      {
        this.textAlignment = value;
        if (this.Parent != null)
        {
          (this.Parent as vTabControl).initializedPagesWidth = false;
          this.Parent.Invalidate();
        }
        this.OnPropertyChanged("TextAlignment");
      }
    }

    /// <summary>Gets the DockPadding of the tab page</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public new ScrollableControl.DockPaddingEdges DockPadding
    {
      get
      {
        return base.DockPadding;
      }
    }

    /// <summary>Gets or sets the Padding of the tab page.</summary>
    [DefaultValue(typeof (Padding), "Padding.Empty")]
    public new Padding Padding
    {
      get
      {
        return this.padding;
      }
      set
      {
        this.padding = value;
        if (this.Parent != null)
        {
          (this.Parent as vTabControl).initializedPagesWidth = false;
          this.Parent.Invalidate();
        }
        this.Invalidate();
        this.OnPropertyChanged("Padding");
      }
    }

    /// <summary>Gets the header bounds.</summary>
    /// <value>The header bounds.</value>
    [Browsable(false)]
    public Rectangle HeaderBounds
    {
      get
      {
        return this.pageRectangle;
      }
    }

    /// <summary>
    /// Gets or sets the index of the image displayed on the tab page.
    /// </summary>
    [Description("Gets or sets the index of the image displayed on the tab page.")]
    [DefaultValue(-1)]
    [Category("TabPage.Appearance")]
    public virtual int ImageIndex
    {
      get
      {
        return this.imageIndex;
      }
      set
      {
        this.imageIndex = value;
        this.Invalidate();
        if (this.Parent != null)
          this.Parent.Invalidate();
        this.UpdateTabControlLayout();
        this.OnPropertyChanged("ImageIndex");
      }
    }

    internal Image Image
    {
      get
      {
        return (Image) null;
      }
    }

    /// <summary>Gets or sets the tooltip text of the tab page.</summary>
    [Description("Gets or sets the tooltip text of the tab page.")]
    [Category("Behavior")]
    [DefaultValue("")]
    public string TooltipText
    {
      get
      {
        if (string.IsNullOrEmpty(this.tooltipText))
          this.tooltipText = this.Text;
        return this.tooltipText;
      }
      set
      {
        this.tooltipText = value != null ? value : "";
        this.OnPropertyChanged("TooltipText");
      }
    }

    /// <summary>Determines whether the tab page is visible.</summary>
    [Category("Appearance")]
    [Description("Determines whether the tab page is visible.")]
    [DefaultValue(false)]
    public bool Invisible
    {
      get
      {
        return this.invisible;
      }
      set
      {
        this.invisible = value;
        if (this.DesignMode)
          return;
        vTabControl vTabControl = this.Parent as vTabControl;
        if (vTabControl == null)
          return;
        if (vTabControl.SelectedTab == null && !this.invisible)
          vTabControl.SelectedTab = this;
        else if (vTabControl.SelectedTab == this)
        {
          foreach (vTabPage tabPage in vTabControl.TabPages)
          {
            if (!tabPage.Invisible)
            {
              vTabControl.SelectedTab = tabPage;
              break;
            }
          }
        }
        if (vTabControl.SelectedTab == this && this.invisible)
          vTabControl.SelectedTab = (vTabPage) null;
        vTabControl.Refresh();
      }
    }

    /// <summary>Gets or sets the Enabled property.</summary>
    [DefaultValue(true)]
    public new bool Enabled
    {
      get
      {
        return base.Enabled;
      }
      set
      {
        base.Enabled = value;
        vTabControl vTabControl = this.Parent as vTabControl;
        if (vTabControl == null)
          return;
        vTabControl.Refresh();
      }
    }

    /// <summary>Gets or sets the text of the tab page.</summary>
    [Description("Gets or sets the text of the tab page.")]
    [Browsable(true)]
    [Category("Appearance")]
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
        if (this.Parent == null)
          return;
        (this.Parent as vTabControl).initializedPagesWidth = false;
        (this.Parent as vTabControl).InitializeScrollBar();
        this.Parent.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use the content border color.
    /// </summary>
    [Description("Gets or sets a value indicating whether to use the content border color.")]
    [Category("TabPage.Appearance")]
    [DefaultValue(false)]
    public bool UseContentBorderColor
    {
      get
      {
        return this.useContentBorderColor;
      }
      set
      {
        if (value == this.useContentBorderColor)
          return;
        this.useContentBorderColor = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets a value of the ContentBorderColor.</summary>
    [DefaultValue(typeof (Color), "Black")]
    [Description("Gets or sets a value of the ContentBorderColor.")]
    [Category("TabPage.Appearance")]
    public Color ContentBorderColor
    {
      get
      {
        return this.contentBorderColor;
      }
      set
      {
        if (!(value != this.contentBorderColor))
          return;
        this.contentBorderColor = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the color of the text.</summary>
    /// <value>The color of the text.</value>
    [Description("Gets or sets the color of the text.")]
    [DefaultValue(typeof (Color), "Black")]
    [Category("TabPage.Appearance")]
    public Color TextColor
    {
      get
      {
        return this.textColor;
      }
      set
      {
        if (!(value != this.textColor))
          return;
        this.textColor = value;
        vTabControl vTabControl = this.Parent as vTabControl;
        if (vTabControl == null)
          return;
        vTabControl.Invalidate();
      }
    }

    /// <summary>Gets or sets the color of the text.</summary>
    /// <value>The color of the text.</value>
    [Category("TabPage.Appearance")]
    [DefaultValue(typeof (Color), "Black")]
    [Description("Gets or sets the color of the text.")]
    public Color HighlightTextColor
    {
      get
      {
        return this.highlightTextColor;
      }
      set
      {
        if (!(value != this.highlightTextColor))
          return;
        this.highlightTextColor = value;
        vTabControl vTabControl = this.Parent as vTabControl;
        if (vTabControl == null)
          return;
        vTabControl.Invalidate();
      }
    }

    /// <summary>Gets or sets the color of the text.</summary>
    /// <value>The color of the text.</value>
    [Category("TabPage.Appearance")]
    [Description("Gets or sets the color of the text.")]
    [DefaultValue(typeof (Color), "Black")]
    public Color PressedTextColor
    {
      get
      {
        return this.pressedTextColor;
      }
      set
      {
        if (!(value != this.pressedTextColor))
          return;
        this.pressedTextColor = value;
        vTabControl vTabControl = this.Parent as vTabControl;
        if (vTabControl == null)
          return;
        vTabControl.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use the theme text font.
    /// </summary>
    [Category("TabPage.Appearance")]
    [Description("Gets or sets a value indicating whether to use the theme text font.")]
    [DefaultValue(true)]
    public bool UseDefaultTextFont
    {
      get
      {
        return this.useDefaultTextFont;
      }
      set
      {
        if (value == this.useDefaultTextFont)
          return;
        this.useDefaultTextFont = value;
        vTabControl vTabControl = this.Parent as vTabControl;
        if (vTabControl != null)
          vTabControl.Invalidate();
        this.UpdateTabControlLayout();
      }
    }

    /// <summary>Gets or sets the font of the text.</summary>
    /// <value>The color of the text.</value>
    [Category("TabPage.Appearance")]
    [Description("Gets or sets the font of the text.")]
    public Font TextFont
    {
      get
      {
        if (this.textFont == null)
          return this.Font;
        return this.textFont;
      }
      set
      {
        if (value == this.textFont)
          return;
        this.textFont = value;
        vTabControl vTabControl = this.Parent as vTabControl;
        if (vTabControl != null)
          vTabControl.Invalidate();
        this.UpdateTabControlLayout();
      }
    }

    /// <summary>Gets the tab control.</summary>
    /// <value>The tab control.</value>
    [Browsable(false)]
    public vTabControl TabControl
    {
      get
      {
        return this.Parent as vTabControl;
      }
    }

    /// <summary>
    /// Gets a value indicating whether this instance is selected.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is selected; otherwise, <c>false</c>.
    /// </value>
    [Description("Gets a value indicating whether this instance is selected.")]
    [Browsable(false)]
    public bool IsSelected
    {
      get
      {
        if (this.TabControl != null)
          return this.TabControl.SelectedTab == this;
        return false;
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
    [Description("Gets or sets the control's Font.")]
    [Category("Appearance")]
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

    /// <summary>Gets or sets the font of the text.</summary>
    /// <value>The color of the text.</value>
    [Description("Gets or sets the font of the text.")]
    [Category("TabPage.Appearance")]
    public Font SelectedTextFont
    {
      get
      {
        if (this.selectedTextFont == null)
          return this.Font;
        return this.selectedTextFont;
      }
      set
      {
        if (value == this.selectedTextFont)
          return;
        this.selectedTextFont = value;
        vTabControl vTabControl = this.Parent as vTabControl;
        if (vTabControl != null)
          vTabControl.Invalidate();
        this.UpdateTabControlLayout();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use the theme text color.
    /// </summary>
    [Category("TabPage.Appearance")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether to use the theme text color.")]
    public bool UseThemeTextColor
    {
      get
      {
        return this.useThemeTextColor;
      }
      set
      {
        if (value == this.useThemeTextColor)
          return;
        this.useThemeTextColor = value;
        vTabControl vTabControl = this.Parent as vTabControl;
        if (vTabControl == null)
          return;
        vTabControl.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use the ContentBackColor property.
    /// </summary>
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether to use the ContentBackColor property.")]
    [Category("TabPage.Appearance")]
    public bool UseContentBackColor
    {
      get
      {
        return this.useContentBackColor;
      }
      set
      {
        if (value == this.useContentBackColor)
          return;
        this.useContentBackColor = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets a value of the ContentBackColor.</summary>
    [Category("TabPage.Appearance")]
    [Description("Gets or sets a value of the ContentBackColor.")]
    [DefaultValue(typeof (Color), "White")]
    public Color ContentBackColor
    {
      get
      {
        return this.contentBackColor;
      }
      set
      {
        if (!(value != this.contentBackColor))
          return;
        this.contentBackColor = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets whether the content of the tab page is visible.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new bool Visible
    {
      get
      {
        return base.Visible;
      }
      set
      {
        if (this.Parent != null && this == ((vTabControl) this.Parent).SelectedTab)
        {
          base.Visible = true;
          this.Dock = DockStyle.Fill;
        }
        else
          base.Visible = value;
      }
    }

    /// <summary>Occurs when a property value changes.</summary>
    public event PropertyChangedEventHandler PropertyChanged;

    static vTabPage()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>Constructor</summary>
    public vTabPage()
      : this("TabPage")
    {
      this.Scroll += new ScrollEventHandler(this.vTabPage_Scroll);
      this.defaultFont = new Font("Tahoma", this.Font.Size, this.Font.Style);
    }

    /// <summary>Constructor</summary>
    /// <param name="text">The text of the tab page</param>
    public vTabPage(string text)
    {
      this.backGround = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.tabsFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
      this.Text = text;
      this.DockPadding.Top = this.DockPadding.Left = this.DockPadding.Bottom = this.DockPadding.Right = 4;
      this.Visible = false;
      this.SetStyle(ControlStyles.ResizeRedraw, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.ParentChanged += new EventHandler(this.vTabPage_ParentChanged);
    }

    private void vTabPage_Scroll(object sender, ScrollEventArgs e)
    {
      this.Invalidate();
    }

    protected override void OnValidating(CancelEventArgs e)
    {
      base.OnValidating(e);
      if (!e.Cancel || !(this.Parent is vTabControl))
        return;
      (this.Parent as vTabControl).validationCanceled = true;
    }

    private void vTabPage_ParentChanged(object sender, EventArgs e)
    {
      if (this.Parent is vTabControl)
      {
        this.backGround.HostingControl = (IScrollableControlBase) (this.Parent as vTabControl);
        this.tabsFill.HostingControl = (IScrollableControlBase) (this.Parent as vTabControl);
      }
      if (!(this.Parent is vRibbonBar) || !this.DesignMode)
        return;
      foreach (Control control in (ArrangedElementCollection) this.Controls)
      {
        if (control is vRibbonHorizontalLayoutPanel)
          return;
      }
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add layoutPanel");
      vRibbonHorizontalLayoutPanel horizontalLayoutPanel = (vRibbonHorizontalLayoutPanel) designerHost.CreateComponent(typeof (vRibbonHorizontalLayoutPanel));
      transaction.Commit();
      horizontalLayoutPanel.Dock = DockStyle.Fill;
      this.Controls.Add((Control) horizontalLayoutPanel);
      horizontalLayoutPanel.ControlAdded += new ControlEventHandler(this.layoutPanel_ControlAdded);
      horizontalLayoutPanel.BackColor = Color.Transparent;
      if (this.tabsFill == null)
        return;
      this.tabsFill.IsAnimated = false;
    }

    private void layoutPanel_ControlAdded(object sender, ControlEventArgs e)
    {
      if (!(e.Control is vRibbonGroup))
        return;
      (e.Control as vRibbonGroup).VIBlendTheme = this.VIBlendTheme;
    }

    private void value_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "Visible"))
        return;
      if (this.TabContext.Visible)
        this.Invisible = false;
      else
        this.Invisible = true;
    }

    /// <exclude />
    protected override void OnSystemColorsChanged(EventArgs e)
    {
      base.OnSystemColorsChanged(e);
    }

    private void ResetTextImageRelation()
    {
      this.TextImageRelation = TextImageRelation.ImageBeforeText;
    }

    private bool ShouldSerializeTextImageRelation()
    {
      return this.TextImageRelation != TextImageRelation.ImageBeforeText;
    }

    private void ResetTextAlignment()
    {
      this.TextAlignment = ContentAlignment.MiddleCenter;
    }

    private bool ShouldSerializeTextAlignment()
    {
      return this.TextAlignment != ContentAlignment.MiddleCenter;
    }

    /// <summary>Gets the page rectange.</summary>
    /// <returns></returns>
    public Rectangle GetPageRectange()
    {
      return this.pageRectangle;
    }

    /// <summary>Updates the tab control layout.</summary>
    public virtual void UpdateTabControlLayout()
    {
      vTabControl vTabControl = this.Parent as vTabControl;
      if (vTabControl == null)
        return;
      using (Graphics g = Graphics.FromHwnd(this.Handle))
      {
        vTabControl.initializedPagesWidth = false;
        vTabControl.CalculatePagesWidth(g);
        vTabControl.InitializeScrollBar();
      }
    }

    /// <exclude />
    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
    }

    /// <exclude />
    protected override void OnPaint(PaintEventArgs e)
    {
      if (this.Parent == null || !(this.Parent is vTabControl))
        return;
      vTabControl vTabControl = this.Parent as vTabControl;
      if (this.useTabControlTheme && this.VIBlendTheme != vTabControl.VIBlendTheme)
        this.VIBlendTheme = vTabControl.VIBlendTheme;
      DrawTabPageEventArgs e1 = new DrawTabPageEventArgs(e.Graphics, this, this.ClientRectangle);
      vTabControl.OnDrawTabPageBackground(e1);
      if (e1.Handled)
        return;
      this.backGround.Bounds = this.ClientRectangle;
      if (!this.UseContentBackColor)
        this.backGround.DrawElementFill(e.Graphics, ControlState.Normal);
      else
        this.backGround.DrawStandardFill(e.Graphics, this.ContentBackColor);
      this.backGround.Bounds = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
      if (!this.UseContentBorderColor)
        this.backGround.DrawElementBorder(e.Graphics, ControlState.Normal);
      else
        this.backGround.DrawElementBorder(e.Graphics, ControlState.Normal, this.ContentBorderColor);
      base.OnPaint(e);
      if (vTabControl.SelectedTab != this || vTabControl.TabsShape != TabsShape.Office2007 && vTabControl.TabsShape != TabsShape.RoundedCorners)
        return;
      using (Pen pen = new Pen(this.tabsFill.Theme.StylePressed.FillStyle.Colors[this.tabsFill.Theme.StylePressed.FillStyle.Colors.Length - 1]))
      {
        int num1 = 0;
        switch (vTabControl.TabAlignment)
        {
          case vTabPageAlignment.Top:
            e.Graphics.DrawLine(pen, this.pageRectangle.Left, num1, this.pageRectangle.Right, num1);
            break;
          case vTabPageAlignment.Bottom:
            int num2 = this.Height - 1;
            e.Graphics.DrawLine(pen, this.pageRectangle.Left, num2, this.pageRectangle.Right, num2);
            break;
          case vTabPageAlignment.Left:
            int num3 = 0;
            e.Graphics.DrawLine(pen, num3, this.pageRectangle.Top, num3, this.pageRectangle.Bottom);
            break;
          case vTabPageAlignment.Right:
            int num4 = this.Width - 1;
            e.Graphics.DrawLine(pen, num4, this.pageRectangle.Top, num4, this.pageRectangle.Bottom);
            break;
        }
      }
    }

    private void ResetTextFont()
    {
      this.TextFont = Control.DefaultFont;
    }

    private bool ShouldSerializeTextFont()
    {
      bool flag1 = !this.TextFont.Equals((object) Control.DefaultFont);
      bool flag2 = !this.TextFont.Equals((object) this.defaultFont);
      if (flag1)
        return flag2;
      return false;
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

    private void ResetSelectedTextFont()
    {
      this.SelectedTextFont = Control.DefaultFont;
    }

    private bool ShouldSerializeSelectedTextFont()
    {
      bool flag1 = !this.SelectedTextFont.Equals((object) Control.DefaultFont);
      bool flag2 = !this.SelectedTextFont.Equals((object) this.defaultFont);
      if (flag1)
        return flag2;
      return false;
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
