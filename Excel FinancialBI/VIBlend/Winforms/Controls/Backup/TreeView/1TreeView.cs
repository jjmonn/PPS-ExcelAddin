// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vTreeView
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vTreeView control</summary>
  /// <remarks>
  /// A vTreeView displays a hierarchical collection of items with rich customization of the functionality, and the visual appearance."
  /// </remarks>
  [Designer("VIBlend.WinForms.Controls.Design.vTreeViewDesigner, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (vTreeView), "ControlIcons.vTreeView.ico")]
  [Description("Displays a hierarchical collection of items with rich customization of the functionality, and the visual appearance.")]
  [DefaultEvent("AfterSelect")]
  [DefaultProperty("Nodes")]
  public class vTreeView : Control, IScrollableControlBase
  {
    internal vCheckBox checkBox = new vCheckBox();
    private bool paintFill = true;
    private bool paintBorder = true;
    private Color lineColor = Color.Black;
    internal ArrayList selection = new ArrayList();
    private int treeIndent = 20;
    private Color borderColor = Color.Black;
    private Timer indicatorsTimer1 = new Timer();
    private Timer indicatorsTimer2 = new Timer();
    private Timer opacityTimer = new Timer();
    internal ToolTip treeViewToolTip = new ToolTip();
    private Timer timerToolTip = new Timer();
    private bool enableToolTips = true;
    private int toolTipShowDelay = 1500;
    private int toolTipHideDelay = 5000;
    private ToolTipEventArgs toolTipEventArgs = new ToolTipEventArgs();
    internal bool enableToggleAnimation = true;
    private int nodesSpacing = 5;
    private bool useThemeBackColor = true;
    private bool useThemeBorderColor = true;
    private Hashtable cachedImages = new Hashtable();
    private PaintHelper helper = new PaintHelper();
    private bool useThemeIndicatorsColor = true;
    private Color indicatorsColor = Color.Black;
    private Color indicatorsHighlightColor = Color.Black;
    private List<vTreeNode> collapsingNodes = new List<vTreeNode>();
    private List<vTreeNode> expandingNodes = new List<vTreeNode>();
    private double ImageOpacity = 1.0;
    private vTextBox vTextBox = new vTextBox();
    private bool allowanim = true;
    private const int PlusHeight = 4;
    private const int PlusWidth = 4;
    private bool fullRowSelect;
    internal bool opacityAnimationFinished;
    private vTreeNode collapsingNode;
    private vTreeNode expandingNode;
    internal bool opacityAnimationFromMouseDown;
    private bool enableIndicatorsAnimation;
    private bool enableMultipleSelection;
    private IContainer components;
    private vTreeNode downNode;
    private vTreeNode downSel;
    private bool editable;
    private vTreeNode focus;
    private ImageList imageList;
    private bool layoutInvalid;
    private Pen linePen;
    private Brush backBrush;
    private vTreeNodeCollection nodes;
    private Pen plusPen;
    private Point scrollPosition;
    private int updateDepth;
    private int virtualHeight;
    private int virtualWidth;
    internal vHScrollBar hScroll;
    internal vVScrollBar vScroll;
    internal TreeBackGroundElement[] backGrounds;
    internal BackgroundElement backGround;
    private bool checkBoxes;
    private Point showPoint;
    private vTreeNode hoveredNode;
    private int? itemHeight;
    private bool showFocusRectangle;
    private bool allowDragAndDrop;
    private bool triStateMode;
    private VIBLEND_THEME defaultTheme;
    private VIBLEND_THEME defaultScrollBarsTheme;
    internal vTreeNode editNode;
    private bool showRootLines;
    private ControlTheme theme;
    private bool defaultExpandCollapseIndicators;
    private Point initialMousePosition;
    private Form dragForm;
    private Timer scrollingTimer;
    private vTreeNode dragNode;
    private vTreeNode lastHoveredNode;
    private int nodesBackElement;
    private AnimationManager animationManager;

    /// <summary>
    /// Gets or sets a value indicating whether multiple selection is enabled.
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether multiple selection is enabled.")]
    [DefaultValue(false)]
    public bool EnableMultipleSelection
    {
      get
      {
        return this.enableMultipleSelection;
      }
      set
      {
        if (value == this.enableMultipleSelection)
          return;
        this.enableMultipleSelection = value;
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
    public new Font Font
    {
      get
      {
        return base.Font;
      }
      set
      {
        base.Font = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether fullRowSelect is enabled.
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether fullRowSelect is enabled.")]
    [DefaultValue(false)]
    public bool FullRowSelect
    {
      get
      {
        return this.fullRowSelect;
      }
      set
      {
        this.fullRowSelect = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether tool tips are enabled.
    /// </summary>
    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether tool tips are enabled.")]
    public virtual bool EnableToolTips
    {
      get
      {
        return this.enableToolTips;
      }
      set
      {
        this.enableToolTips = value;
      }
    }

    /// <summary>Gets or sets the tool tip show delay.</summary>
    /// <value>The tool tip show delay.</value>
    [DefaultValue(1500)]
    [Description("Time delay in milliseconds before the tooltip is shown")]
    [Category("Behavior")]
    public int ToolTipShowDelay
    {
      get
      {
        return this.toolTipShowDelay;
      }
      set
      {
        this.toolTipShowDelay = value;
        this.timerToolTip.Interval = value;
      }
    }

    /// <summary>Gets or sets the duration of the tool tip.</summary>
    /// <value>The duration of the tool tip.</value>
    [Description("Tooltip duration in milliseconds ")]
    [DefaultValue(5000)]
    [Category("Behavior")]
    public int ToolTipDuration
    {
      get
      {
        return this.toolTipHideDelay;
      }
      set
      {
        this.toolTipHideDelay = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether  toggle animation is enabled
    /// </summary>
    /// <value>
    /// 	<c>true</c> if true the animation is enabled; otherwise, <c>false</c>.
    /// </value>
    [Description("Gets or sets a value indicating whether  toggle animation is enabled")]
    [Category("Behavior")]
    [DefaultValue(true)]
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

    /// <summary>
    /// Determines whether to show the focus rectangle when the tab is Focused
    /// </summary>
    [DefaultValue(false)]
    [Description("Determines whether to show the focus rectangle when the tab is Focused")]
    [Category("Appearance")]
    public bool ShowFocusRectangle
    {
      get
      {
        return this.showFocusRectangle;
      }
      set
      {
        this.showFocusRectangle = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether  indigators animation is enabled
    /// </summary>
    /// <value>
    /// 	<c>true</c> if true the animation is enabled; otherwise, <c>false</c>.
    /// </value>
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether  toggle animation is enabled")]
    public bool EnableIndicatorsAnimation
    {
      get
      {
        return this.enableIndicatorsAnimation;
      }
      set
      {
        this.enableIndicatorsAnimation = value;
        if (!this.enableIndicatorsAnimation)
          this.ImageOpacity = 1.0;
        this.Invalidate();
        if (!this.DesignMode)
          return;
        this.ImageOpacity = 1.0;
      }
    }

    /// <summary>
    /// Gets or sets the border color of the vTreeView control
    /// </summary>
    [DefaultValue(typeof (Color), "Black")]
    [Category("Behavior")]
    [Description("Gets or sets the border color of the vTreeView control. This property is taken into account when the UseThemeBorderColor property is false.")]
    public Color BorderColor
    {
      get
      {
        return this.borderColor;
      }
      set
      {
        this.borderColor = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use the Theme BackColor
    /// </summary>
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether to use the Theme BackColor")]
    [Category("Appearance")]
    public bool UseThemeBackColor
    {
      get
      {
        return this.useThemeBackColor;
      }
      set
      {
        this.useThemeBackColor = value;
        this.Refresh();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use the Theme border color
    /// </summary>
    [Description("Gets or sets a value indicating whether to use the Theme border color")]
    [DefaultValue(true)]
    [Category("Appearance")]
    public bool UseThemeBorderColor
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

    /// <summary>Gets or sets the color of tree lines.</summary>
    [DefaultValue(typeof (Color), "Black")]
    [Category("Behavior")]
    [Description("Gets or sets the color of tree lines.")]
    public Color LineColor
    {
      get
      {
        return this.lineColor;
      }
      set
      {
        this.lineColor = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets whether drag and drop is enabled.</summary>
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets whether drag and drop is enabled.")]
    public bool AllowDragAndDrop
    {
      get
      {
        return this.allowDragAndDrop;
      }
      set
      {
        this.allowDragAndDrop = value;
      }
    }

    /// <summary>
    /// Gets or sets whether the tree view should display checkboxes next to the nodes.
    /// </summary>
    [DefaultValue(false)]
    [Category("Behavior")]
    [Description("Gets or sets whether the tree view should display checkboxes next to the nodes.")]
    public bool CheckBoxes
    {
      get
      {
        return this.checkBoxes;
      }
      set
      {
        this.checkBoxes = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets whether the tree view should display checkboxes next to the nodes and have the tri state mode enabled.
    /// </summary>
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets whether the tree view should display checkboxes next to the nodes and have the tri state mode enabled.")]
    public bool TriStateMode
    {
      get
      {
        return this.triStateMode;
      }
      set
      {
        if (value)
          this.CheckBoxes = true;
        this.triStateMode = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the theme of the control using one of the built-in themes.
    /// </summary>
    [Category("Appearance")]
    [Description("Gets or sets the theme of the control using one of the built-in themes.")]
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
        if (defaultTheme != null)
          this.Theme = defaultTheme;
        this.VIBlendScrollBarsTheme = value;
        this.vTextBox.VIBlendTheme = value;
      }
    }

    /// <summary>
    /// Gets or sets the theme of the treeview scrollbars using one of the built-in themes.
    /// </summary>
    [Browsable(false)]
    [Category("Appearance")]
    [Description("Gets or sets the theme of the treeview scrollbars using one of the built-in themes.")]
    public VIBLEND_THEME VIBlendScrollBarsTheme
    {
      get
      {
        return this.defaultScrollBarsTheme;
      }
      set
      {
        this.defaultScrollBarsTheme = value;
        this.vScroll.VIBlendTheme = value;
        this.hScroll.VIBlendTheme = value;
      }
    }

    /// <summary>Gets or sets the spacing between the tree nodes.</summary>
    [DefaultValue(5)]
    [Category("Behavior")]
    [Description("Gets or sets the spacing between the tree nodes.")]
    public int NodesSpacing
    {
      get
      {
        return this.nodesSpacing;
      }
      set
      {
        this.nodesSpacing = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to paint a background
    /// </summary>
    /// <value><c>true</c> if [paint fill]; otherwise, <c>false</c>.</value>
    [Description("Gets or sets a value indicating whether to paint a background")]
    [Category("Behavior")]
    [DefaultValue(true)]
    public bool PaintNodesDefaultFill
    {
      get
      {
        return this.paintFill;
      }
      set
      {
        this.paintFill = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to paint a border
    /// </summary>
    /// <value><c>true</c> if [paint border]; otherwise, <c>false</c>.</value>
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether to paint a border")]
    [Category("Behavior")]
    public bool PaintNodesDefaultBorder
    {
      get
      {
        return this.paintBorder;
      }
      set
      {
        this.paintBorder = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets whether to show root lines.</summary>
    [DefaultValue(false)]
    [Category("Appearance")]
    [Description("Gets or sets whether to show root lines.")]
    public bool ShowRootLines
    {
      get
      {
        return this.showRootLines;
      }
      set
      {
        this.showRootLines = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the theme of the control.</summary>
    [Category("Appearance")]
    [Description("Gets or sets the theme of the control.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
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
        FillStyle fillStyle1 = this.theme.QueryFillStyleSetter("TreeViewNormal");
        if (fillStyle1 != null)
          this.theme.StyleNormal.FillStyle = fillStyle1;
        FillStyle fillStyle2 = this.theme.QueryFillStyleSetter("TreeViewHighlight");
        if (fillStyle2 != null)
          this.theme.StyleHighlight.FillStyle = fillStyle2;
        FillStyle fillStyle3 = this.theme.QueryFillStyleSetter("TreeViewPressed");
        if (fillStyle2 != null)
          this.theme.StylePressed.FillStyle = fillStyle3;
        Color color1 = Color.Empty;
        Color color2 = this.theme.QueryColorSetter("TreeViewBorder");
        if (color2 != Color.Empty)
          this.theme.StyleNormal.BorderColor = color2;
        Color color3 = Color.Empty;
        Color color4 = this.theme.QueryColorSetter("TreeViewBorderHighlight");
        if (color4 != Color.Empty)
          this.theme.StyleHighlight.BorderColor = color4;
        Color color5 = Color.Empty;
        Color color6 = this.theme.QueryColorSetter("TreeViewBorderPressed");
        if (color6 != Color.Empty)
          this.theme.StylePressed.BorderColor = color6;
        if (this.backGrounds != null)
        {
          foreach (BackgroundElement backGround in this.backGrounds)
            backGround.LoadTheme(this.theme);
        }
        ControlTheme copy = this.theme.CreateCopy();
        copy.StyleNormal.FillStyle = (FillStyle) new FillStyleSolid(this.theme.StyleNormal.FillStyle.Colors[0]);
        this.backGround.LoadTheme(copy);
        this.backGround.Radius = 0;
        this.vScroll.Theme = value;
        this.hScroll.Theme = value;
        this.vTextBox.Theme = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets whether to use default expand/collapse indicators
    /// </summary>
    [DefaultValue(false)]
    [Category("Appearance")]
    [Description("Gets or sets whether to use default expand/collapse indicators")]
    public bool DefaultExpandCollapseIndicators
    {
      get
      {
        return this.defaultExpandCollapseIndicators;
      }
      set
      {
        this.defaultExpandCollapseIndicators = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use the default indicators color.
    /// </summary>
    [Description("Gets or sets a value indicating whether to use the theme indicators color.")]
    [DefaultValue(true)]
    [Category("Appearance")]
    public bool UseThemeIndicatorsColor
    {
      get
      {
        return this.useThemeIndicatorsColor;
      }
      set
      {
        this.useThemeIndicatorsColor = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the color of the indicators.</summary>
    /// <value>The color of the indicators.</value>
    [Description("Gets or sets the color of the indicators. In order to use this property, set the UseThemeIndicatorsColor property to false.")]
    [DefaultValue(typeof (Color), "Black")]
    [Category("Appearance")]
    public Color IndicatorsColor
    {
      get
      {
        return this.indicatorsColor;
      }
      set
      {
        this.indicatorsColor = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the highlight color of the indicators.</summary>
    /// <value>The color of the indicators.</value>
    [DefaultValue(typeof (Color), "Black")]
    [Category("Appearance")]
    [Description("Gets or sets the highlight color of the indicators. In order to use this property, set the UseThemeIndicatorsColor property to false.")]
    public Color IndicatorsHighlightColor
    {
      get
      {
        return this.indicatorsHighlightColor;
      }
      set
      {
        this.indicatorsHighlightColor = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets the currently selected nodes</summary>
    public vTreeNode[] SelectedNodes
    {
      get
      {
        return (vTreeNode[]) this.selection.ToArray(typeof (vTreeNode));
      }
    }

    /// <summary>
    /// Gets a value indicating whether this instance is updating.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is updating; otherwise, <c>false</c>.
    /// </value>
    public bool IsUpdating
    {
      get
      {
        return this.updateDepth > 0;
      }
    }

    /// <summary>Gets the first visible node.</summary>
    [Browsable(false)]
    public vTreeNode FirstVisibleNode
    {
      get
      {
        if (this.nodes != null)
        {
          foreach (vTreeNode node in this.nodes)
          {
            if (node.IsVisible)
              return node;
          }
        }
        return (vTreeNode) null;
      }
    }

    /// <summary>
    /// Gets or sets the ImageList that contains the Image objects used by the tree nodes.
    /// </summary>
    [Category("Behavior")]
    [DefaultValue(null)]
    [Description("Gets or sets the ImageList that contains the Image objects used by the tree nodes.")]
    public ImageList ImageList
    {
      get
      {
        return this.imageList;
      }
      set
      {
        this.imageList = value;
      }
    }

    /// <summary>
    /// Checks if the vTreeView is currently in a node text editing mode.
    /// </summary>
    [Browsable(false)]
    public bool IsEditing
    {
      get
      {
        return this.vTextBox.Visible;
      }
    }

    /// <summary>Gets the height of the tree nodes</summary>
    [Category("Behavior")]
    [DefaultValue(16)]
    [Description("Gets the height of tree nodes.")]
    public int ItemHeight
    {
      get
      {
        if (this.itemHeight.HasValue)
          return this.itemHeight.Value;
        return this.Font.Height + 3;
      }
      set
      {
        this.itemHeight = new int?(value);
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the label text of the tree nodes can be edited.
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether the label text of the tree nodes can be edited.")]
    [DefaultValue(false)]
    public bool LabelEdit
    {
      get
      {
        return this.editable;
      }
      set
      {
        this.editable = value;
      }
    }

    /// <summary>Gets the last visible node.</summary>
    [Browsable(false)]
    public vTreeNode LastVisibleNode
    {
      get
      {
        return vTreeNode.GetLastVisibleNode(this.Nodes);
      }
    }

    /// <summary>Gets the collection of the tree view nodes.</summary>
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public vTreeNodeCollection Nodes
    {
      get
      {
        return this.nodes;
      }
    }

    /// <summary>
    /// Gets the current scroll position of the vTreeView control
    /// </summary>
    [Browsable(false)]
    public Point ScrollPosition
    {
      get
      {
        return this.scrollPosition;
      }
      set
      {
        this.scrollPosition = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets the first selected node in the vTreeView.</summary>
    [Browsable(false)]
    public vTreeNode SelectedNode
    {
      get
      {
        if (this.selection.Count <= 0)
          return (vTreeNode) null;
        return (vTreeNode) this.selection[0];
      }
      set
      {
        if (value == null && this.SelectedNode != null)
        {
          if (this.OnSelectionChanging(value))
            return;
          this.SelectedNode.selected = false;
          this.ClearSelection();
          this.OnSelectionChanged();
          this.Invalidate();
        }
        else
        {
          if ((this.SelectedNode == value || this.EnableMultipleSelection) && !this.EnableMultipleSelection || this.OnSelectionChanging(value))
            return;
          if (Control.ModifierKeys != Keys.Control || !this.EnableMultipleSelection)
            this.ClearSelection();
          else if (Control.ModifierKeys == Keys.Control && this.EnableMultipleSelection && this.selection.Contains((object) value))
          {
            this.selection.Remove((object) value);
            if (value != null)
              value.selected = false;
            this.OnSelectionChanged();
            this.Invalidate();
            return;
          }
          if (value != null && !this.selection.Contains((object) value))
            this.selection.Add((object) value);
          if (value != null)
          {
            value.selected = true;
            this.SetFocus(value);
          }
          this.OnSelectionChanged();
          this.BringIntoView(value);
          this.Invalidate();
        }
      }
    }

    /// <summary>Gets or sets the indent of the tree nodes.</summary>
    [Description("Gets or sets the indent of the tree nodes.")]
    [DefaultValue(20)]
    [Category("Behavior")]
    public int TreeIndent
    {
      get
      {
        return this.treeIndent;
      }
      set
      {
        this.treeIndent = value;
        this.Invalidate();
      }
    }

    internal int VirtualHeight
    {
      get
      {
        return this.virtualHeight;
      }
      set
      {
        this.virtualHeight = value;
      }
    }

    internal int VirtualWidth
    {
      get
      {
        return this.virtualWidth;
      }
      set
      {
        this.virtualWidth = value;
      }
    }

    private int VisibleRows
    {
      get
      {
        return this.ClientRectangle.Height / this.ItemHeight;
      }
    }

    /// <exclude />
    [Browsable(false)]
    public AnimationManager AnimationManager
    {
      get
      {
        if (this.animationManager == null)
          this.animationManager = new AnimationManager((Control) this);
        return this.animationManager;
      }
    }

    /// <summary>Gets or sets whether the control is animated</summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DefaultValue(true)]
    public bool AllowAnimations
    {
      get
      {
        return this.allowanim;
      }
      set
      {
        if (this.backGrounds != null)
        {
          foreach (BackgroundElement backGround in this.backGrounds)
            backGround.IsAnimated = value;
        }
        this.allowanim = value;
      }
    }

    /// <summary>Occurs when a node is rendered.</summary>
    [Category("Action")]
    public event EventHandler<DrawNodeEventArgs> DrawTreeNode;

    /// <summary>Occurs after the tree node is collapsed.</summary>
    [Category("Action")]
    [Description("Occurs after the tree node is collapsed.")]
    public event vTreeViewEventHandler AfterCollapse;

    /// <summary>Occurs after the tree node is checked</summary>
    [Description("Occurs after the tree node is checked.")]
    [Category("Action")]
    public event vTreeViewEventHandler NodeChecked;

    /// <summary>
    /// Occurs when the user clicks the mouse button over a node
    /// </summary>
    [Description("Occurs when the user clicks the mouse button over a node.")]
    [Category("Action")]
    public event vTreeViewMouseEventHandler NodeMouseDown;

    /// <summary>
    /// Occurs when the user clicks the mouse button over a node
    /// </summary>
    [Description("Occurs when the user clicks the mouse button over a node.")]
    [Category("Action")]
    public event vTreeViewMouseEventHandler NodeMouseUp;

    /// <summary>Occurs after the tree node is expanded.</summary>
    [Description("Occurs after the tree node is expanded..")]
    [Category("Action")]
    public event vTreeViewEventHandler AfterExpand;

    /// <summary>Occurs before a drag and drop starts</summary>
    [Description("Occurs before a drag and drop starts.")]
    [Category("Action")]
    public event vTreeViewDragCancelEventHandler BeforeDragStart;

    /// <summary>Occurs before a drag and drop ends</summary>
    [Description("Occurs before a drag and drop starts.")]
    [Category("Action")]
    public event vTreeViewDragCancelEventHandler BeforeDragEnd;

    /// <summary>Occurs after a drag and drop starts</summary>
    [Description("Occurs before a drag and drop starts.")]
    [Category("Action")]
    public event vTreeViewDragEventHandler AfterDragStart;

    /// <summary>Occurs after a drag and drop ends</summary>
    [Description("Occurs after a drag and drop ends.")]
    [Category("Action")]
    public event vTreeViewDragEventHandler AfterDragEnd;

    /// <summary>Occurs before a node is selected</summary>
    [Description("Occurs before a node is selected.")]
    [Category("Action")]
    public event vTreeViewCancelEventHandler BeforeSelect;

    /// <summary>Occurs after a node is selected</summary>
    [Description("Occurs after a node is selected")]
    [Category("Action")]
    public event vTreeViewEventHandler AfterSelect;

    /// <summary>Occurs before a node collapses</summary>
    [Description("Occurs before a node collapses")]
    [Category("Action")]
    public event vTreeViewCancelEventHandler BeforeCollapse;

    /// <summary>Occurs before a node expands.</summary>
    [Description("Occurs before a node expands.")]
    [Category("Action")]
    public event vTreeViewCancelEventHandler BeforeExpand;

    /// <summary>Occurs before a node label edit.</summary>
    [Category("Action")]
    [Description("Occurs before a node label edit.")]
    public event vTreeViewLabelEditEventHandler BeforeLabelEdit;

    /// <summary>Occurs after a node label is edited.</summary>
    [Category("Action")]
    [Description("Occurs after a node label is edited.")]
    public event vTreeViewLabelEditEventHandler AfterLabelEdit;

    /// <summary>Occurs when tooltip shows.</summary>
    [Category("Action")]
    public event ToolTipShownHandler TooltipShow;

    static vTreeView()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>Constructor</summary>
    public vTreeView()
    {
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.ResizeRedraw, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.Selectable | ControlStyles.SupportsTransparentBackColor, true);
      this.InitializeComponent();
      this.SuspendLayout();
      this.ResumeLayout();
      this.hScroll = new vHScrollBar();
      this.vScroll = new vVScrollBar();
      this.vTextBox = new vTextBox();
      this.vTextBox.Visible = false;
      this.Controls.Add((Control) this.hScroll);
      this.Controls.Add((Control) this.vScroll);
      this.Controls.Add((Control) this.vTextBox);
      this.vScroll.Bounds = new Rectangle(0, 0, 15, 15);
      this.hScroll.Bounds = new Rectangle(0, 0, 15, 15);
      this.hScroll.Visible = false;
      this.vScroll.Visible = false;
      this.vScroll.Scroll += new ScrollEventHandler(this.vScroll_Scroll);
      this.hScroll.Scroll += new ScrollEventHandler(this.hScroll_Scroll);
      this.nodes = new vTreeNodeCollection(this, (vTreeNode) null);
      this.backGround = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.Theme = ControlTheme.GetDefaultTheme(VIBLEND_THEME.OFFICEBLACK);
      this.scrollingTimer = new Timer();
      this.scrollingTimer.Tick += new EventHandler(this.scrollingTimer_Tick);
      this.indicatorsTimer1.Tick += new EventHandler(this.indicatorsTimer1_Tick);
      this.indicatorsTimer2.Tick += new EventHandler(this.indicatorsTimer2_Tick);
      this.indicatorsTimer2.Interval = 10;
      this.indicatorsTimer1.Interval = 10;
      this.opacityTimer.Interval = 5;
      this.opacityTimer.Tick += new EventHandler(this.opacityTimer_Tick);
      this.timerToolTip.Tick += new EventHandler(this.timerToolTip_Tick);
      this.timerToolTip.Interval = 1500;
      if (this.backGrounds != null)
        return;
      this.PerformLayout();
    }

    protected internal virtual void OnDrawTreeNode(DrawNodeEventArgs args)
    {
      if (this.DrawTreeNode == null)
        return;
      this.DrawTreeNode((object) this, args);
    }

    private void timerToolTip_Tick(object sender, EventArgs e)
    {
      this.timerToolTip.Stop();
      if (this.toolTipEventArgs == null)
        return;
      this.OnToolTipShown(this.toolTipEventArgs);
    }

    protected internal virtual void OnToolTipShown(ToolTipEventArgs args)
    {
      if (!this.enableToolTips)
        return;
      if (this.TooltipShow != null)
        this.TooltipShow((object) this, args);
      if (!string.IsNullOrEmpty(""))
      {
        string toolTipText = args.ToolTipText;
      }
      if (this.hoveredNode == null)
        return;
      string tooltipText = this.hoveredNode.TooltipText;
      if (string.IsNullOrEmpty(tooltipText))
        return;
      Rectangle labelBounds = this.hoveredNode.LabelBounds;
      this.showPoint = this.PointToClient(Cursor.Position);
      vTreeNode vTreeNode = this.HitTest(this.showPoint);
      if (vTreeNode == null || !vTreeNode.Text.Equals(this.hoveredNode.Text))
        return;
      this.treeViewToolTip.Show(tooltipText, (IWin32Window) this, new Point(this.showPoint.X, labelBounds.Bottom), this.toolTipHideDelay);
    }

    private void ResetOpacity()
    {
      for (int index = 0; index < this.backGrounds.Length; ++index)
        this.backGrounds[index].Opacity = 1f;
      this.Invalidate();
    }

    private void opacityTimer_Tick(object sender, EventArgs e)
    {
      for (int index = 0; index < this.backGrounds.Length; ++index)
      {
        if (this.collapsingNodes.Count > 0)
        {
          if (this.collapsingNodes.Contains(this.backGrounds[index].BindedNode))
          {
            if ((double) this.backGrounds[index].Opacity > 0.0)
            {
              this.backGrounds[index].Opacity -= 0.1f;
              if ((double) this.backGrounds[index].Opacity <= 0.0)
              {
                this.backGrounds[index].Opacity = 0.0f;
                this.opacityAnimationFinished = true;
              }
            }
            else
              this.opacityAnimationFinished = true;
          }
        }
        else if (this.expandingNodes.Count > 0 && this.expandingNodes.Contains(this.backGrounds[index].BindedNode) && (double) this.backGrounds[index].Opacity < 1.0)
        {
          this.backGrounds[index].Opacity += 0.1f;
          if ((double) this.backGrounds[index].Opacity >= 1.0)
          {
            this.backGrounds[index].Opacity = 1f;
            this.opacityAnimationFinished = true;
          }
        }
      }
      this.ResetOpacityAnimation();
      this.Invalidate();
    }

    private void ResetOpacityAnimation()
    {
      if (!this.opacityAnimationFinished)
        return;
      this.opacityAnimationFromMouseDown = false;
      this.opacityTimer.Stop();
      this.opacityAnimationFinished = false;
      this.expandingNodes.Clear();
      this.collapsingNodes.Clear();
      if (this.collapsingNode != null)
      {
        this.collapsingNode.expanded = false;
        this.OnAfterCollapse(this.collapsingNode);
        this.ResetOpacity();
        this.InvalidateLayout();
        this.collapsingNode = (vTreeNode) null;
      }
      else
      {
        if (this.expandingNode == null)
          return;
        this.ResetOpacity();
        this.InvalidateLayout();
        this.expandingNode = (vTreeNode) null;
      }
    }

    private void scrollingTimer_Tick(object sender, EventArgs e)
    {
      if (this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position))
      {
        Rectangle r1 = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width, 20);
        Rectangle r2 = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Bottom - 20, this.ClientRectangle.Width, 20);
        if (this.RectangleToScreen(r1).Contains(Cursor.Position) && this.vScroll.Value > 0)
        {
          --this.vScroll.Value;
          this.vScroll.SyncThumbPositionWithLogicalValue();
        }
        if (this.RectangleToScreen(r2).Contains(Cursor.Position) && this.vScroll.Value < this.vScroll.Maximum)
        {
          ++this.vScroll.Value;
          this.vScroll.SyncThumbPositionWithLogicalValue();
        }
      }
      if (!this.vScroll.RectangleToScreen(this.vScroll.ClientRectangle).Contains(Cursor.Position))
        return;
      if (this.vScroll.RectangleToScreen(this.vScroll.SmallDecrementRectangle).Contains(Cursor.Position))
      {
        if (this.vScroll.Value <= 0)
          return;
        --this.vScroll.Value;
        this.vScroll.SyncThumbPositionWithLogicalValue();
      }
      else
      {
        if (!this.vScroll.RectangleToScreen(this.vScroll.SmallIncrementRectangle).Contains(Cursor.Position) || this.vScroll.Value >= this.vScroll.Maximum)
          return;
        ++this.vScroll.Value;
        this.vScroll.SyncThumbPositionWithLogicalValue();
      }
    }

    private void hScroll_Scroll(object sender, ScrollEventArgs e)
    {
      if (this.IsEditing)
        this.EndEdit(true);
      this.ScrollPosition = new Point(-e.NewValue, this.ScrollPosition.Y);
    }

    private void vScroll_Scroll(object sender, ScrollEventArgs e)
    {
      for (int index = 0; index <= this.VisibleRows; ++index)
      {
        if (this.backGrounds.Length > index && this.backGrounds[index].BindedNode != null && this.backGrounds[index].BindedNode.Equals((object) this.SelectedNode))
        {
          this.backGrounds[index] = new TreeBackGroundElement(Rectangle.Empty, (IScrollableControlBase) this);
          this.backGrounds[index].LoadTheme(this.Theme);
          this.Invalidate();
          break;
        }
      }
      if (this.IsEditing)
        this.EndEdit(true);
      this.ScrollPosition = new Point(this.ScrollPosition.X, -e.NewValue * this.ItemHeight);
      this.vScroll.Refresh();
    }

    private Point ApplyScrollOffset(Point p)
    {
      return new Point(p.X - this.scrollPosition.X, p.Y - this.scrollPosition.Y);
    }

    private Point ApplyScrollOffset(int x, int y)
    {
      return new Point(x - this.scrollPosition.X, y - this.scrollPosition.Y);
    }

    /// <summary>
    /// Opens a textbox editor to edit the currently selected node.
    /// </summary>
    /// <param name="value">Text to insert in the text box. If you pass a null (VB Nothing), the node's text will be used.</param>
    /// <returns></returns>
    public bool BeginEdit(string value)
    {
      vTreeNode selectedNode = this.SelectedNode;
      if (!this.editable || selectedNode == null || !selectedNode.IsLabelEditable)
        return false;
      string label = value != null ? value : selectedNode.Text;
      if (this.BeforeLabelEdit != null)
      {
        vTreeNodeLabelEditEventArgs e = new vTreeNodeLabelEditEventArgs(selectedNode, label);
        this.BeforeLabelEdit((object) this, e);
        if (e.CancelEdit)
          return false;
      }
      Rectangle rectangle = new Rectangle(selectedNode.LabelBounds.Left - -this.scrollPosition.X, selectedNode.LabelBounds.Top - -this.scrollPosition.Y, selectedNode.LabelBounds.Width, selectedNode.LabelBounds.Height);
      if (rectangle.Width < 1)
        rectangle.Width = 1;
      if (rectangle.Height < 1)
        rectangle.Height = 1;
      this.vTextBox.Bounds = rectangle;
      this.vTextBox.Text = selectedNode.Text;
      this.vTextBox.Visible = true;
      this.vTextBox.Focus();
      this.editNode = selectedNode;
      return true;
    }

    public void BeginUpdate()
    {
      ++this.updateDepth;
    }

    private void ClearSelection()
    {
      this.EndEdit(true);
      this.EndEdit(false);
      this.focus = (vTreeNode) null;
      foreach (vTreeNode node in this.selection)
      {
        node.Selected = false;
        this.InvalidateNode(node);
      }
      this.selection.Clear();
    }

    /// <summary>Collapses all tree nodes.</summary>
    public void CollapseAll()
    {
      this.CollapseAll(this, this.Nodes);
    }

    private void CollapseAll(vTreeView view, vTreeNodeCollection nodes)
    {
      if (nodes == null)
        return;
      if (view != null)
        view.BeginUpdate();
      try
      {
        foreach (vTreeNode node in nodes)
        {
          if (node.IsExpanded)
          {
            this.CollapseAll(view, node.Nodes);
            node.Collapse();
          }
        }
      }
      finally
      {
        if (view != null)
          view.EndUpdate();
      }
    }

    /// <exclude />
    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
      {
        this.timerToolTip.Dispose();
        this.indicatorsTimer1.Dispose();
        this.indicatorsTimer2.Dispose();
        this.opacityTimer.Dispose();
        this.scrollingTimer.Dispose();
        this.components.Dispose();
      }
      base.Dispose(disposing);
    }

    /// <summary>Clears the cached images.</summary>
    public void ClearCachedImages()
    {
      this.cachedImages = new Hashtable();
    }

    internal Image GetTreeNodeImage(vTreeNode node, bool selected, bool state)
    {
      if (this.ImageList == null)
        return (Image) null;
      if (node == null)
        return (Image) null;
      if (selected && node.SelectedImageIndex >= 0)
      {
        if (this.imageList.Images.Count <= node.SelectedImageIndex)
          return (Image) null;
        if (!this.cachedImages.Contains((object) (node.ToString() + "Selected")))
          this.cachedImages.Add((object) (node.ToString() + "Selected"), (object) this.ImageList.Images[node.SelectedImageIndex]);
        return (Image) this.cachedImages[(object) (node.ToString() + "Selected")];
      }
      if (state && node.StateImageIndex >= 0)
      {
        if (this.imageList.Images.Count <= node.StateImageIndex)
          return (Image) null;
        if (!this.cachedImages.Contains((object) (node.ToString() + "State")))
          this.cachedImages.Add((object) (node.ToString() + "State"), (object) this.ImageList.Images[node.StateImageIndex]);
        return (Image) this.cachedImages[(object) (node.ToString() + "State")];
      }
      if (node.ImageIndex < 0)
        return (Image) null;
      if (this.imageList.Images.Count <= node.ImageIndex)
        return (Image) null;
      if (!this.cachedImages.Contains((object) node))
        this.cachedImages.Add((object) node, (object) this.ImageList.Images[node.ImageIndex]);
      return (Image) this.cachedImages[(object) node];
    }

    private int DrawNodes(Graphics g, ref int backNum, ref Rectangle clip, LineStates state, int indent, int y, vTreeNodeCollection nodes)
    {
      if (nodes != null)
      {
        Font font = this.Font;
        Size imageSize = this.GetImageSize();
        int count = nodes.Count;
        int num = 0;
        int itemHeight1 = this.ItemHeight;
        int x = this.Margin.Left + 4;
        foreach (vTreeNode node in nodes)
        {
          if (node.IsVisible)
          {
            LineState state1 = LineState.None;
            if (num == 0)
              state1 &= LineState.First;
            if (num + 1 == count)
              state1 |= LineState.Last;
            int itemHeight2 = this.ItemHeight;
            if (node.ItemHeight.HasValue)
              itemHeight2 = node.ItemHeight.Value;
            state.Push(state1);
            bool flag = y + itemHeight2 + this.NodesSpacing > clip.Top && y <= clip.Bottom;
            if (flag)
            {
              int imageIndex = node.ImageIndex;
              if (!this.selection.Contains((object) node))
                node.Selected = false;
              bool selected = node.Selected;
              Image treeNodeImage = this.GetTreeNodeImage(node, selected, node.IsExpanded);
              ++backNum;
              if (backNum >= this.backGrounds.Length)
                return y;
              TreeBackGroundElement backGround = this.backGrounds[backNum];
              backGround.BindedNode = node;
              if ((double) backGround.Opacity == 1.0 && this.enableToggleAnimation && this.expandingNodes.Contains(node))
                backGround.Opacity = 0.0f;
              node.Draw(g, backGround, font, this.linePen, state, itemHeight2, indent, x, y, ref imageSize, treeNodeImage, selected);
            }
            int y1 = y;
            y += itemHeight2 + this.NodesSpacing;
            if (node.Nodes.Count > 0)
            {
              int depth = state.Depth - 1;
              if (node.IsExpanded)
              {
                y = this.DrawNodes(g, ref backNum, ref clip, state, indent, y, node.Nodes);
                if (flag)
                  this.PaintExpandCollapse(g, node, x + imageSize.Width / 2, y1, itemHeight2, depth, indent, false);
              }
              else if (flag)
                this.PaintExpandCollapse(g, node, x + imageSize.Width / 2, y1, itemHeight2, depth, indent, true);
            }
            state.Pop();
            if (y > clip.Bottom)
              return y;
          }
          ++num;
        }
      }
      return y;
    }

    private Image GetImageByPattern(string searchPattern)
    {
      Assembly executingAssembly = Assembly.GetExecutingAssembly();
      string[] manifestResourceNames = executingAssembly.GetManifestResourceNames();
      string resourceUri = "";
      foreach (string str in manifestResourceNames)
      {
        if (str.EndsWith(searchPattern))
          resourceUri = str;
      }
      if (resourceUri != "")
        return Image.FromStream(this.GetStreamFromResource(executingAssembly, resourceUri));
      return (Image) null;
    }

    private Stream GetStreamFromResource(Assembly assembly, string resourceUri)
    {
      return assembly.GetManifestResourceStream(resourceUri) ?? (Stream) null;
    }

    private Color GetBackColor()
    {
      Color color = this.BackColor;
      if (this.useThemeBackColor)
        color = this.Enabled ? this.theme.StyleNormal.FillStyle.Colors[0] : this.theme.StyleDisabled.FillStyle.Colors[0];
      return color;
    }

    private Color GetArrowColor(bool isHover)
    {
      if (!this.Enabled)
        return this.theme.StyleDisabled.BorderColor;
      Color color = Color.Empty;
      Color baseColor;
      if (isHover)
      {
        baseColor = this.theme.QueryColorSetter("TreeViewArrowBorderColorHover");
        if (baseColor == Color.Empty)
          baseColor = this.theme.StyleHighlight.BorderColor;
      }
      else
      {
        baseColor = this.theme.QueryColorSetter("TreeViewArrowBorderColor");
        if (baseColor == Color.Empty)
          baseColor = this.theme.StyleNormal.BorderColor;
      }
      return Color.FromArgb((int) ((double) byte.MaxValue * this.ImageOpacity), baseColor);
    }

    /// <summary>Gets all vTreeNodes recursively as list.</summary>
    /// <returns></returns>
    public List<vTreeNode> GetNodes()
    {
      List<vTreeNode> collection = new List<vTreeNode>();
      this.GetNodes(collection, this.Nodes);
      return collection;
    }

    private List<vTreeNode> GetNodes(List<vTreeNode> collection, vTreeNodeCollection nodes)
    {
      foreach (vTreeNode node in nodes)
      {
        collection.Add(node);
        if (node.Nodes.Count > 0)
          this.GetNodes(collection, node.Nodes);
      }
      return collection;
    }

    private FillStyle GetArrowFillStyle(bool isHover)
    {
      if (!this.Enabled)
        return this.theme.StyleDisabled.FillStyle;
      FillStyle fillStyle = (!isHover ? this.theme.QueryFillStyleSetter("TreeViewArrowFill") ?? this.theme.StyleNormal.FillStyle : this.theme.QueryFillStyleSetter("TreeViewArrowFillHover") ?? this.theme.StyleHighlight.FillStyle).Clone();
      for (int index = 0; index < fillStyle.ColorsNumber; ++index)
        fillStyle.Colors[index] = Color.FromArgb((int) ((double) byte.MaxValue * this.ImageOpacity), fillStyle.Colors[index]);
      return fillStyle;
    }

    /// <summary>Draws the arrow indicator.</summary>
    /// <param name="g">The g.</param>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="isExpanded">if set to <c>true</c> [is expanded].</param>
    /// <param name="isHover">if set to <c>true</c> [is hover].</param>
    protected virtual void DrawArrowIndicator(Graphics g, int x, int y, bool isExpanded, bool isHover)
    {
      if (this.ImageOpacity > 1.0)
        this.ImageOpacity = 1.0;
      GraphicsPath path = new GraphicsPath();
      int num1 = 6;
      if (isExpanded)
      {
        path.AddLine(x + num1, y + num1, x + num1, y);
        path.AddLine(x + num1, y, x, y + num1);
        path.AddLine(x, y + num1, x + num1, y + num1);
      }
      else
      {
        num1 = 8;
        int num2 = num1 / 2;
        path.AddLine(x, y, x, y + num1);
        path.AddLine(x, y + num1, x + num2, y + num2);
        path.AddLine(x + num2, y + num2, x, y);
      }
      using ((Brush) new SolidBrush(Color.FromArgb((int) ((double) byte.MaxValue * this.ImageOpacity), this.GetBackColor())))
        ;
      if (this.UseThemeIndicatorsColor)
      {
        using (Brush brush = this.GetArrowFillStyle(isHover).GetBrush(new Rectangle(x, y, num1, num1)))
          g.FillPath(brush, path);
        using (Pen pen = new Pen(this.GetArrowColor(isHover)))
          g.DrawPath(pen, path);
      }
      else
      {
        Color color = Color.FromArgb((int) ((double) byte.MaxValue * this.ImageOpacity), this.IndicatorsColor);
        if (isHover)
          color = Color.FromArgb((int) ((double) byte.MaxValue * this.ImageOpacity), this.IndicatorsHighlightColor);
        using (Brush brush = (Brush) new SolidBrush(color))
          g.FillPath(brush, path);
        using (Pen pen = new Pen(color))
          g.DrawPath(pen, path);
      }
    }

    private void PaintExpandCollapse(Graphics g, vTreeNode node, int x, int y, int height, int depth, int indent, bool isCollapsed)
    {
      if (this.DesignMode)
        this.ImageOpacity = 1.0;
      Rectangle boxBounds = this.GetBoxBounds(x, y, height, depth, indent);
      x = boxBounds.Left;
      y = boxBounds.Top;
      int num1 = 2;
      int num2 = 2;
      int num3 = y + 4;
      int num4 = x + 4;
      bool isHover = node.imageHovered;
      if (!this.defaultExpandCollapseIndicators)
        this.DrawArrowIndicator(g, x, y, !isCollapsed, isHover);
      else if (this.UseThemeIndicatorsColor)
      {
        BackgroundElement backgroundElement = new BackgroundElement(boxBounds, (IScrollableControlBase) this);
        backgroundElement.Bounds = boxBounds;
        backgroundElement.LoadTheme(this.Theme);
        backgroundElement.Radius = 0;
        backgroundElement.Opacity = (float) this.ImageOpacity;
        if (!isHover)
        {
          backgroundElement.DrawElementFill(g, ControlState.Normal);
          backgroundElement.DrawElementBorder(g, ControlState.Normal);
        }
        else
        {
          backgroundElement.DrawElementFill(g, ControlState.Hover);
          backgroundElement.DrawElementBorder(g, ControlState.Hover);
        }
        this.plusPen.Color = Color.FromArgb((int) ((double) byte.MaxValue * (double) backgroundElement.Opacity), this.plusPen.Color);
        g.DrawLine(this.plusPen, x + num1, num3, x + num1 + 4, num3);
        if (!isCollapsed)
          return;
        g.DrawLine(this.plusPen, num4, y + num2, num4, y + num2 + 4);
      }
      else
      {
        BackgroundElement backgroundElement = new BackgroundElement(boxBounds, (IScrollableControlBase) this);
        backgroundElement.Bounds = boxBounds;
        backgroundElement.Opacity = (float) this.ImageOpacity;
        backgroundElement.Radius = 0;
        if (!isHover)
          backgroundElement.DrawElementBorder(g, ControlState.Normal, this.IndicatorsColor);
        else
          backgroundElement.DrawElementBorder(g, ControlState.Normal, this.IndicatorsHighlightColor);
        using (Pen pen = new Pen(this.IndicatorsColor))
        {
          if (isHover)
            pen.Color = this.IndicatorsHighlightColor;
          g.DrawLine(pen, x + num1, num3, x + num1 + 4, num3);
          if (!isCollapsed)
            return;
          g.DrawLine(pen, num4, y + num2, num4, y + num2 + 4);
        }
      }
    }

    /// <summary>Ends the editing of a tree node.</summary>
    /// <param name="cancel">Determines whether the changes should be applied or not.</param>
    public void EndEdit(bool cancel)
    {
      if (this.editNode == null)
        return;
      if (!cancel)
        this.editNode.Text = this.vTextBox.Text;
      if (this.AfterLabelEdit != null)
        this.AfterLabelEdit((object) this, new vTreeNodeLabelEditEventArgs(this.editNode, this.editNode.Text));
      this.editNode = (vTreeNode) null;
      this.vTextBox.Text = string.Empty;
      this.vTextBox.Visible = false;
    }

    /// <summary>Ends the update.</summary>
    public void EndUpdate()
    {
      --this.updateDepth;
      if (this.updateDepth == 0 && this.layoutInvalid)
      {
        this.PerformLayout();
        this.Invalidate();
      }
      this.UpdateLayout();
    }

    /// <summary>Updates the layout.</summary>
    public void UpdateLayout()
    {
      this.InvalidateLayout();
    }

    /// <summary>Ensures that a specific tree node is visible</summary>
    /// <param name="node"></param>
    public static void EnsureVisible(vTreeNode node)
    {
      for (vTreeNode parent = node.Parent; parent != null; parent = parent.Parent)
      {
        if (!parent.IsExpanded)
          parent.Expand();
      }
    }

    /// <summary>Expands all nodes</summary>
    public void ExpandAll()
    {
      this.ExpandAll(this, this.Nodes);
    }

    private void ExpandAll(vTreeView view, vTreeNodeCollection nodes)
    {
      if (nodes == null || nodes.Count == 0)
        return;
      if (view != null)
        view.BeginUpdate();
      try
      {
        foreach (vTreeNode node in nodes)
        {
          if (node.Nodes.Count > 0)
          {
            if (!node.IsExpanded)
              node.Expand();
            this.ExpandAll(view, node.Nodes);
          }
        }
      }
      finally
      {
        if (view != null)
          view.EndUpdate();
      }
    }

    /// <summary>
    /// Performs a HitTest at a specific point and returns the tree node located at this point
    /// </summary>
    /// <param name="point">Point to test</param>
    /// <returns>A vTreeNode located at the hit test point. If there is no node at this location, the method returns null (VB Nothing).</returns>
    public vTreeNode HitTest(Point point)
    {
      return this.FindNodeAt(this.Nodes, point.X, point.Y);
    }

    /// <summary>
    /// Gets an instance of a node from collection in a given position.
    /// </summary>
    /// <param name="nodes"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public vTreeNode FindNodeAt(vTreeNodeCollection nodes, int x, int y)
    {
      if (nodes != null)
      {
        foreach (vTreeNode node in nodes)
        {
          if (node.IsVisible)
          {
            int itemHeight = this.ItemHeight;
            if (node.ItemHeight.HasValue)
              itemHeight = node.ItemHeight.Value;
            if (new Rectangle(0, node.LabelBounds.Top, this.Width, itemHeight).Contains(x, y))
              return node;
            if (node.IsExpanded && (node.LabelBounds.Top <= y && node.bottom >= y))
            {
              vTreeNode nodeAt = this.FindNodeAt(node.Nodes, x, y);
              if (nodeAt != null)
                return nodeAt;
            }
          }
        }
      }
      return (vTreeNode) null;
    }

    private Rectangle GetBoxBounds(int margin, int y, int height, int depth, int indent)
    {
      int x = margin + depth * indent - 4;
      y += (height - 8) / 2;
      return new Rectangle(x, y, 8, 8);
    }

    private Size GetImageSize()
    {
      Size size = new Size();
      if (this.imageList != null)
      {
        size = this.imageList.ImageSize;
        int height = size.Height;
        int width = size.Width;
        int itemHeight = this.ItemHeight;
        if (height > itemHeight)
          size = new Size(width * itemHeight / height, itemHeight);
      }
      return size;
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      KeyEventArgs e = new KeyEventArgs(keyData);
      this.DoKeyDown(e);
      return e.Handled;
    }

    internal void DoKeyDown(KeyEventArgs e)
    {
      vTreeNode selectedNode1 = this.SelectedNode;
      bool flag = (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z || e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) && (e.Modifiers == Keys.Shift || e.Modifiers == Keys.None);
      if ((e.Modifiers != Keys.Control || e.KeyCode != Keys.Home && e.KeyCode != Keys.End) && (e.Modifiers != Keys.None && !flag))
        return;
      Keys keyCode = e.KeyCode;
      vTreeNode selectedNode2 = this.SelectedNode;
      switch (keyCode)
      {
        case Keys.Return:
          this.EndEdit(false);
          break;
        case Keys.Escape:
          this.EndEdit(true);
          break;
        case Keys.Space:
          if (this.SelectedNode != null && !this.IsEditing)
          {
            this.SelectedNode.Checked = this.SelectedNode.Checked == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;
            this.Invalidate();
            e.Handled = true;
            break;
          }
          break;
        case Keys.Prior:
          if (!this.IsEditing)
          {
            this.DoPageUp();
            e.Handled = true;
            break;
          }
          break;
        case Keys.Next:
          if (!this.IsEditing)
          {
            this.DoPageDown();
            e.Handled = true;
            break;
          }
          break;
        case Keys.End:
          if (!this.IsEditing)
          {
            this.SelectedNode = this.LastVisibleNode;
            e.Handled = true;
            break;
          }
          break;
        case Keys.Home:
          if (!this.IsEditing)
          {
            this.SelectedNode = this.FirstVisibleNode;
            e.Handled = true;
            break;
          }
          break;
        case Keys.Left:
          if (!this.IsEditing && selectedNode1 != null)
          {
            if (!selectedNode1.IsExpanded)
            {
              if (selectedNode1.Parent != null)
              {
                this.SelectedNode = selectedNode1.Parent;
                break;
              }
              break;
            }
            selectedNode1.Collapse();
            break;
          }
          break;
        case Keys.Up:
          if (!this.IsEditing)
          {
            this.DoUpKey(selectedNode1);
            e.Handled = true;
            break;
          }
          break;
        case Keys.Right:
          if (!this.IsEditing)
          {
            if (selectedNode1 != null && !selectedNode1.IsExpanded)
              selectedNode1.Expand();
            e.Handled = true;
            break;
          }
          break;
        case Keys.Down:
          if (!this.IsEditing)
          {
            this.DoDownKey(selectedNode1);
            e.Handled = true;
            break;
          }
          break;
        case Keys.Multiply:
          if (selectedNode2 != null)
          {
            this.ExpandAll();
            e.Handled = true;
            break;
          }
          break;
        case Keys.Add:
          if (selectedNode2 != null)
          {
            selectedNode2.Expand();
            e.Handled = true;
            break;
          }
          break;
        case Keys.Subtract:
          if (!this.IsEditing && selectedNode2 != null)
          {
            selectedNode2.Collapse();
            e.Handled = true;
            break;
          }
          break;
        case Keys.F2:
          if (selectedNode2 != null && this.BeginEdit(selectedNode2.Text))
          {
            e.Handled = true;
            break;
          }
          break;
        default:
          if (!flag || e.Handled || (!this.ContainsFocus || this.IsEditing))
            return;
          char c = Convert.ToChar(e.KeyValue);
          if (!e.Shift)
            c = char.ToLower(c);
          if (!this.BeginEdit(c.ToString()))
            return;
          e.Handled = true;
          return;
      }
      if (this.IsEditing)
        return;
      e.Handled = true;
    }

    private void DoDownKey(vTreeNode selectedNode)
    {
      if (selectedNode == null)
      {
        this.SelectedNode = this.FirstVisibleNode;
      }
      else
      {
        vTreeNode nextVisibleNode = selectedNode.NextVisibleNode;
        if (nextVisibleNode == null)
          return;
        if (nextVisibleNode.Enabled)
        {
          this.SelectedNode = nextVisibleNode;
        }
        else
        {
          while (nextVisibleNode != null)
          {
            nextVisibleNode = nextVisibleNode.NextVisibleNode;
            if (nextVisibleNode != null && nextVisibleNode.Enabled)
            {
              this.SelectedNode = nextVisibleNode;
              break;
            }
          }
        }
      }
    }

    private void DoUpKey(vTreeNode selectedNode)
    {
      if (selectedNode == null)
      {
        this.SelectedNode = this.LastVisibleNode;
      }
      else
      {
        vTreeNode prevVisibleNode = selectedNode.PrevVisibleNode;
        if (prevVisibleNode == null)
          return;
        if (prevVisibleNode.Enabled)
        {
          this.SelectedNode = prevVisibleNode;
        }
        else
        {
          while (prevVisibleNode != null)
          {
            prevVisibleNode = prevVisibleNode.PrevVisibleNode;
            if (prevVisibleNode != null && prevVisibleNode.Enabled)
            {
              this.SelectedNode = prevVisibleNode;
              break;
            }
          }
        }
      }
    }

    internal void DoPageDown()
    {
      int visibleRows = this.VisibleRows;
      vTreeNode vTreeNode = this.SelectedNode ?? this.FirstVisibleNode;
      if (vTreeNode == null)
        return;
      vTreeNode nextVisibleNode;
      for (nextVisibleNode = vTreeNode.NextVisibleNode; nextVisibleNode != null && visibleRows > 0 && nextVisibleNode.NextVisibleNode != null; --visibleRows)
        nextVisibleNode = nextVisibleNode.NextVisibleNode;
      if (nextVisibleNode == null)
        return;
      if (nextVisibleNode.Enabled)
        this.SelectedNode = nextVisibleNode;
      else
        this.DoUpKey(nextVisibleNode);
    }

    internal void DoPageUp()
    {
      int visibleRows = this.VisibleRows;
      vTreeNode vTreeNode = this.SelectedNode ?? this.LastVisibleNode;
      if (vTreeNode == null)
        return;
      vTreeNode selectedNode;
      for (selectedNode = vTreeNode; selectedNode != null && visibleRows > 0 && selectedNode.PrevVisibleNode != null; --visibleRows)
        selectedNode = selectedNode.PrevVisibleNode;
      if (selectedNode == null)
        return;
      if (!selectedNode.Enabled)
        this.DoDownKey(selectedNode);
      else
        this.SelectedNode = selectedNode;
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.SuspendLayout();
      this.AccessibleRole = AccessibleRole.List;
      this.Name = this.AccessibleName = "TreeView";
      this.ResumeLayout(false);
    }

    internal void InvalidateLayout()
    {
      this.layoutInvalid = true;
      if (this.updateDepth != 0)
        return;
      this.PerformLayout();
      this.Refresh();
    }

    internal void InvalidateNode(vTreeNode node)
    {
      if (node == null)
        return;
      int itemHeight = this.ItemHeight;
      if (node.ItemHeight.HasValue)
        itemHeight = node.ItemHeight.Value;
      Rectangle rc = new Rectangle(0, node.LabelBounds.Top, this.Width, itemHeight);
      rc.Offset(this.scrollPosition);
      this.Invalidate(rc);
    }

    private int CalculateNodesBounds(Graphics g, int indent, int depth, int y, vTreeNodeCollection nodes)
    {
      Size imgSize = new Size();
      if (this.imageList != null)
        imgSize = this.imageList.ImageSize;
      int left = this.Margin.Left;
      int itemHeight1 = this.ItemHeight;
      Font font = this.Font;
      foreach (vTreeNode node in nodes)
      {
        if (node.IsVisible)
        {
          int itemHeight2 = this.ItemHeight;
          if (node.ItemHeight.HasValue)
            itemHeight2 = node.ItemHeight.Value;
          node.CalculateBounds(g, font, itemHeight2, left, indent, depth, y, imgSize);
          y += itemHeight2 + this.NodesSpacing;
          this.virtualWidth = Math.Max(this.virtualWidth, node.LabelBounds.Right);
          if (node.IsExpanded && node.Nodes.Count > 0)
            y = this.CalculateNodesBounds(g, indent, depth + 1, y, node.Nodes);
          node.bottom = y;
        }
      }
      return y;
    }

    /// <exclude />
    protected internal virtual void OnAfterCollapse(vTreeNode node)
    {
      if (this.AfterCollapse != null)
        this.AfterCollapse((object) this, new vTreeViewEventArgs(node, vTreeViewAction.Collapse));
      vTreeNode selectedNode = this.SelectedNode;
      if (selectedNode == null || !node.Contains(selectedNode))
        return;
      this.SelectedNode = node;
    }

    /// <exclude />
    protected internal virtual void OnNodeChecked(vTreeNode node)
    {
      if (this.NodeChecked != null)
        this.NodeChecked((object) this, new vTreeViewEventArgs(node, vTreeViewAction.Unknown));
      if (!this.CheckBoxes || !this.TriStateMode)
        return;
      this.CheckNodes(node, node);
    }

    /// <exclude />
    protected internal virtual void OnNodeMouseDown(vTreeNode node, MouseEventArgs args)
    {
      if (this.NodeMouseDown == null)
        return;
      this.NodeMouseDown((object) this, new vTreeViewMouseEventArgs(node, args, vTreeViewAction.Unknown));
    }

    /// <exclude />
    protected internal virtual void OnNodeMouseUp(vTreeNode node, MouseEventArgs args)
    {
      if (this.NodeMouseUp == null)
        return;
      this.NodeMouseUp((object) this, new vTreeViewMouseEventArgs(node, args, vTreeViewAction.Unknown));
    }

    private void CheckNodes(vTreeNode node, vTreeNode baseNode)
    {
      if (node != null)
      {
        int num = 0;
        bool flag = false;
        foreach (vTreeNode node1 in node.Nodes)
        {
          if (node1.Checked != CheckState.Unchecked)
          {
            if (node1.Checked == CheckState.Indeterminate)
              flag = true;
            ++num;
          }
        }
        if (node != baseNode)
        {
          if (num == node.Nodes.Count)
          {
            node.checkedNode = CheckState.Checked;
            if (flag)
              node.checkedNode = CheckState.Indeterminate;
          }
          else if (num > 0)
            node.checkedNode = CheckState.Indeterminate;
          else
            node.Checked = CheckState.Unchecked;
        }
        this.CheckNodes(node.Parent, baseNode);
      }
      else
      {
        if (baseNode.Nodes.Count == 0)
          return;
        Stack stack = new Stack();
        CheckState @checked = baseNode.Checked;
        baseNode = baseNode.Nodes[0];
        while (baseNode != null)
        {
          baseNode.checkedNode = @checked;
          if (baseNode.Nodes.Count > 0)
          {
            if (baseNode.NextSiblingNode != null)
              stack.Push((object) baseNode.NextSiblingNode);
            baseNode = baseNode.Nodes[0];
          }
          else
          {
            baseNode = baseNode.NextSiblingNode;
            if (baseNode == null && stack.Count > 0)
              baseNode = (vTreeNode) stack.Pop();
          }
        }
      }
      this.Invalidate();
    }

    /// <exclude />
    protected internal virtual void OnAfterExpand(vTreeNode node)
    {
      if (this.AfterExpand != null)
        this.AfterExpand((object) this, new vTreeViewEventArgs(node, vTreeViewAction.Expand));
      if (!this.enableToggleAnimation || !this.opacityAnimationFromMouseDown)
        return;
      this.expandingNode = node;
      this.expandingNodes = node.GetChildren(new List<vTreeNode>(), node.Nodes);
      if (this.opacityTimer.Enabled)
        this.ResetOpacityAnimation();
      this.opacityTimer.Start();
    }

    /// <exclude />
    protected internal virtual void OnBeforeDragStart(vTreeNode dragNode)
    {
      if (this.BeforeDragStart == null)
        return;
      this.BeforeDragStart((object) this, new vTreeViewDragCancelEventArgs(dragNode, (vTreeNode) null));
    }

    /// <exclude />
    protected internal virtual void OnAfterDragEnd(vTreeNode dragNode, vTreeNode targetNode)
    {
      if (this.AfterDragEnd == null)
        return;
      this.AfterDragEnd((object) this, new vTreeViewDragEventArgs(dragNode, targetNode));
    }

    /// <exclude />
    protected internal virtual void OnAfterDragStart(vTreeNode dragNode, vTreeNode targetNode)
    {
      if (this.AfterDragStart == null)
        return;
      this.AfterDragStart((object) this, new vTreeViewDragEventArgs(dragNode, targetNode));
    }

    /// <exclude />
    protected internal virtual void OnBeforeDragEnd(vTreeNode dragNode, vTreeNode targetNode)
    {
      if (this.BeforeDragEnd == null)
        return;
      this.BeforeDragEnd((object) this, new vTreeViewDragCancelEventArgs(dragNode, targetNode));
    }

    /// <exclude />
    protected internal virtual void OnBeforeCollapse(vTreeViewCancelEventArgs args)
    {
      if (this.BeforeCollapse != null)
      {
        this.BeforeCollapse((object) this, args);
        if (args.Cancel)
          return;
      }
      if (!this.enableToggleAnimation || !this.opacityAnimationFromMouseDown)
        return;
      this.collapsingNode = args.Node;
      this.collapsingNodes = args.Node.GetChildren(new List<vTreeNode>(), args.Node.Nodes);
      if (this.opacityTimer.Enabled)
        this.ResetOpacityAnimation();
      this.opacityTimer.Start();
    }

    /// <exclude />
    protected internal virtual void OnBeforeExpand(vTreeViewCancelEventArgs args)
    {
      if (this.BeforeExpand == null)
        return;
      this.BeforeExpand((object) this, args);
    }

    /// <exclude />
    protected override void OnGotFocus(EventArgs e)
    {
      this.Invalidate();
    }

    /// <exclude />
    protected override void OnKeyDown(KeyEventArgs e)
    {
      this.DoKeyDown(e);
      base.OnKeyDown(e);
    }

    private void SetScrollBarsBounds(bool hScrollVisible, bool vScrollVisible)
    {
      if (this.hScroll == null || this.vScroll == null)
        return;
      int num1 = 0;
      int num2 = 0;
      if (hScrollVisible)
        num1 = this.hScroll.Height;
      if (vScrollVisible)
        num2 = this.vScroll.Width;
      this.vScroll.Bounds = new Rectangle(this.ClientRectangle.Right - this.vScroll.Width, this.ClientRectangle.Y, this.vScroll.Width, this.ClientRectangle.Height - num1);
      this.hScroll.Bounds = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Bottom - this.hScroll.Height, this.ClientRectangle.Right - num2, this.hScroll.Height);
      this.hScroll.Visible = hScrollVisible;
      this.vScroll.Visible = vScrollVisible;
    }

    /// <exclude />
    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      this.Invalidate();
      this.PerformLayout();
    }

    /// <exclude />
    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      this.virtualHeight = 0;
      if (this.IsUpdating || this.nodes == null)
        return;
      Graphics graphics = this.CreateGraphics();
      using (graphics)
      {
        this.virtualWidth = 0;
        this.virtualHeight = this.CalculateNodesBounds(graphics, this.TreeIndent, 1, 1, this.Nodes);
        this.virtualHeight += this.ItemHeight;
        this.vScroll.SmallChange = 1;
        this.vScroll.LargeChange = this.VisibleRows;
        int num = this.virtualHeight / this.ItemHeight;
        this.vScroll.Maximum = Math.Max(1, num - this.vScroll.LargeChange);
        this.hScroll.Maximum = Math.Max(1, this.virtualWidth + this.TreeIndent - this.Width);
        bool vScrollVisible = false;
        bool hScrollVisible = false;
        if (num > this.VisibleRows)
        {
          this.vScroll.Visible = true;
          vScrollVisible = true;
        }
        else
        {
          this.vScroll.Visible = false;
          this.vScroll.Value = 0;
        }
        if (this.backGrounds == null || this.backGrounds.Length < this.VisibleRows)
        {
          this.backGrounds = new TreeBackGroundElement[this.VisibleRows + 5];
          for (int index = 0; index <= this.VisibleRows + 4; ++index)
          {
            this.backGrounds[index] = new TreeBackGroundElement(Rectangle.Empty, (IScrollableControlBase) this);
            this.backGrounds[index].LoadTheme(this.theme);
          }
        }
        int num3 = this.VirtualWidth + 2 * this.treeIndent + (this.vScroll.Visible ? this.vScroll.Width : 0);
        this.SetHScroll(ref hScrollVisible, this.vScroll.Maximum, num3);
        this.SetScrollBarsBounds(hScrollVisible, vScrollVisible);
      }
    }

    private void SetHScroll(ref bool hScrollVisible, int num7, int num3)
    {
      if (this.VirtualWidth + this.vScroll.Width > this.Size.Width && this.VirtualWidth > 0)
      {
        int val2 = num3 - this.Size.Width - this.TreeIndent;
        if (val2 < 0)
        {
          this.hScroll.Visible = false;
          this.hScroll.Value = 0;
          this.scrollPosition = new Point(0, this.scrollPosition.Y);
        }
        else
        {
          this.hScroll.Minimum = 0;
          if (this.vScroll.Visible && val2 >= this.vScroll.Width)
            this.hScroll.Maximum = val2 - this.vScroll.Width;
          else
            this.hScroll.Maximum = val2;
          this.hScroll.Value = Math.Min(this.hScroll.Value, val2);
          this.hScroll.Visible = true;
          this.vScroll.Maximum = num7 + 1;
          hScrollVisible = true;
        }
      }
      else
      {
        this.hScroll.Visible = false;
        this.hScroll.Value = 0;
        this.scrollPosition = new Point(0, this.scrollPosition.Y);
      }
    }

    /// <exclude />
    protected override void OnLostFocus(EventArgs e)
    {
      this.treeViewToolTip.Hide((IWin32Window) this);
      this.Invalidate();
    }

    /// <exclude />
    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      this.opacityAnimationFromMouseDown = false;
      this.EndEdit(false);
      this.initialMousePosition = e.Location;
      Point pt = this.ApplyScrollOffset(e.X, e.Y);
      int x = pt.X;
      int y = pt.Y;
      bool focused = this.Focused;
      this.Focus();
      vTreeNode vTreeNode = this.HitTest(new Point(x, y));
      vTreeNode selectedNode = this.SelectedNode;
      vTreeNode node = this.NodeAt(e.Location);
      if (node != null && node.Enabled)
      {
        bool flag = false;
        if (node.Parent != null && node.Parent.IsExpanded)
          flag = true;
        else if (node.Parent == null)
          flag = true;
        if (flag)
        {
          Rectangle boxBounds = this.GetBoxBounds(this.Margin.Left + this.GetImageSize().Width / 2, node.LabelBounds.Top, this.ItemHeight, node.Depth, this.TreeIndent);
          int num = (this.ItemHeight - boxBounds.Height) / 2;
          boxBounds.Inflate(num, num);
          if (!boxBounds.Contains(x, y))
          {
            this.SelectedNode = node;
            this.OnNodeMouseDown(node, e);
          }
        }
      }
      this.downSel = this.downNode = (vTreeNode) null;
      if (e.Button != MouseButtons.Left || vTreeNode == null)
        return;
      Size imageSize = this.GetImageSize();
      if (this.CheckBoxes && vTreeNode.ShowCheckBox)
      {
        if (vTreeNode.CheckBoxBounds.Contains(pt))
          vTreeNode.Checked = vTreeNode.Checked == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;
        this.Invalidate();
      }
      if (e.Clicks > 1)
      {
        if (node != null && vTreeNode != null)
        {
          this.opacityAnimationFromMouseDown = true;
          vTreeNode.Toggle();
          return;
        }
      }
      else if (focused && vTreeNode.LabelAndImageBounds(imageSize, this.TreeIndent).Contains(x, y) && vTreeNode.LabelBounds.Contains(x, y))
      {
        this.downSel = selectedNode;
        this.downNode = vTreeNode;
      }
      int itemHeight = this.ItemHeight;
      if (vTreeNode.ItemHeight.HasValue)
        itemHeight = vTreeNode.ItemHeight.Value;
      Rectangle boxBounds1 = this.GetBoxBounds(this.Margin.Left + imageSize.Width / 2, vTreeNode.LabelBounds.Top, itemHeight, vTreeNode.Depth, this.TreeIndent);
      int num1 = (itemHeight - boxBounds1.Height) / 2;
      boxBounds1.Inflate(num1, num1);
      if (!boxBounds1.Contains(x, y))
        return;
      this.opacityAnimationFromMouseDown = true;
      vTreeNode.Toggle();
    }

    internal virtual bool ShouldDrag(Point mousePosition)
    {
      return Math.Abs(mousePosition.X - this.initialMousePosition.X) >= SystemInformation.DragSize.Width || Math.Abs(mousePosition.Y - this.initialMousePosition.Y) >= SystemInformation.DragSize.Height;
    }

    internal Form CreateDragDropForm(Bitmap image)
    {
      Form form = new Form();
      try
      {
        form.Size = new Size(0, 0);
      }
      catch
      {
        form = new Form();
      }
      form.MinimizeBox = false;
      form.MaximizeBox = false;
      form.FormBorderStyle = FormBorderStyle.None;
      form.ShowInTaskbar = false;
      form.Text = "";
      form.Visible = false;
      return form;
    }

    /// <exclude />
    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      if (!this.enableIndicatorsAnimation)
        return;
      this.StartBackWardsPlusMinusAnimation();
    }

    /// <exclude />
    protected override void OnMouseLeave(EventArgs e)
    {
      if (this.backGrounds == null)
        return;
      for (int index = 0; index < this.backGrounds.Length; ++index)
      {
        if (this.backGrounds[index].BindedNode != null)
          this.backGrounds[index].BindedNode.highLighted = false;
      }
      if (this.lastHoveredNode != null)
      {
        this.lastHoveredNode.imageHovered = false;
        this.Invalidate();
      }
      if (this.enableIndicatorsAnimation)
        this.StartForwardsPlusMinusAnimation();
      base.OnMouseLeave(e);
    }

    internal void StartForwardsPlusMinusAnimation()
    {
      if (this.DesignMode)
        return;
      if (this.indicatorsTimer1 != null)
        this.indicatorsTimer1.Start();
      if (this.indicatorsTimer2 == null)
        return;
      this.indicatorsTimer2.Stop();
    }

    internal void StartBackWardsPlusMinusAnimation()
    {
      if (this.DesignMode)
        return;
      if (this.indicatorsTimer1 != null)
        this.indicatorsTimer1.Stop();
      if (this.indicatorsTimer2 == null)
        return;
      this.indicatorsTimer2.Start();
    }

    private void indicatorsTimer2_Tick(object sender, EventArgs e)
    {
      if (this.DesignMode)
        return;
      if (this.ImageOpacity < 1.0)
      {
        this.ImageOpacity += 0.05;
        if (this.ImageOpacity > 1.0)
          this.ImageOpacity = 1.0;
      }
      else
      {
        if (this.ImageOpacity > 1.0)
          this.ImageOpacity = 1.0;
        this.indicatorsTimer2.Stop();
      }
      this.Invalidate();
    }

    private void indicatorsTimer1_Tick(object sender, EventArgs e)
    {
      if (this.DesignMode)
        return;
      if (this.ImageOpacity < 0.05)
        this.ImageOpacity = 0.0;
      if (this.ImageOpacity > 0.0)
        this.ImageOpacity -= 0.025;
      else
        this.indicatorsTimer1.Stop();
      this.Invalidate();
    }

    /// <exclude />
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool ShowWindow(HandleRef hWnd, int nCmdShow);

    /// <exclude />
    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (this.backGrounds == null)
        return;
      if (this.AllowDragAndDrop && e.Button == MouseButtons.Left)
        this.Capture = true;
      if (this.AllowDragAndDrop && this.dragForm != null && this.dragForm.Visible)
      {
        if (!this.ClientRectangle.Contains(e.Location))
        {
          if (Cursor.Current != Cursors.No)
          {
            Cursor.Current = Cursors.No;
            this.Invalidate();
          }
        }
        else
          Cursor.Current = Cursors.Default;
      }
      Point pt = this.ApplyScrollOffset(e.X, e.Y);
      vTreeNode vTreeNode1 = this.NodeAt(e.Location);
      if (this.AllowDragAndDrop && this.Capture && (this.dragForm != null && this.dragForm.Visible))
        this.dragForm.Location = Cursor.Position;
      if (this.AllowDragAndDrop && e.Button == MouseButtons.Left && (this.ShouldDrag(e.Location) && vTreeNode1 != null))
      {
        Bitmap nodeBitmap = vTreeNode1.GetNodeBitmap();
        if (this.dragForm == null)
          this.dragForm = this.CreateDragDropForm(nodeBitmap);
        if (!this.dragForm.Visible)
        {
          this.dragNode = vTreeNode1;
          vTreeViewDragCancelEventArgs e1 = new vTreeViewDragCancelEventArgs(this.dragNode, (vTreeNode) null);
          if (this.BeforeDragStart != null)
            this.BeforeDragStart((object) this, e1);
          if (e1.Cancel)
          {
            this.dragForm.Hide();
            return;
          }
          this.dragForm.Location = Cursor.Position;
          this.dragForm.Size = nodeBitmap.Size;
          this.dragForm.BackgroundImage = (Image) nodeBitmap;
          this.dragForm.MaximumSize = nodeBitmap.Size;
          vTreeView.ShowWindow(new HandleRef((object) this.dragForm, this.dragForm.Handle), 8);
          this.dragForm.Size = nodeBitmap.Size;
          this.dragForm.Location = Cursor.Position;
          vTreeViewDragEventArgs e2 = new vTreeViewDragEventArgs(this.dragNode, (vTreeNode) null);
          if (this.AfterDragStart != null)
            this.AfterDragStart((object) this, e2);
          this.scrollingTimer.Start();
        }
      }
      bool flag = true;
      for (int index = 0; index < this.backGrounds.Length; ++index)
      {
        if (this.backGrounds[index].BindedNode != null)
        {
          if (this.backGrounds[index].BindedNode != vTreeNode1)
          {
            if (this.backGrounds[index].BindedNode.highLighted)
              flag = false;
            this.backGrounds[index].BindedNode.highLighted = false;
          }
          else if (vTreeNode1.LabelBounds.Contains(pt))
          {
            if (!vTreeNode1.highLighted)
            {
              vTreeNode1.highLighted = true;
              this.Refresh();
            }
            flag = true;
          }
          else
          {
            vTreeNode1.highLighted = false;
            flag = true;
            this.Refresh();
          }
        }
      }
      if (!flag)
        this.Refresh();
      int x = pt.X;
      int y = pt.Y;
      if (this.lastHoveredNode != null)
      {
        this.lastHoveredNode.imageHovered = false;
        this.Invalidate();
        this.lastHoveredNode = (vTreeNode) null;
      }
      vTreeNode vTreeNode2 = this.HitTest(new Point(x, y));
      if (vTreeNode2 == null)
        return;
      if (vTreeNode2.CheckBoxBounds.Contains(pt))
        this.Invalidate();
      Size imageSize = this.GetImageSize();
      int itemHeight = this.ItemHeight;
      if (vTreeNode2.ItemHeight.HasValue)
        itemHeight = vTreeNode2.ItemHeight.Value;
      Rectangle boxBounds = this.GetBoxBounds(this.Margin.Left + imageSize.Width / 2, vTreeNode2.LabelBounds.Top, itemHeight, vTreeNode2.Depth, this.TreeIndent);
      int num = (itemHeight - boxBounds.Height) / 2;
      boxBounds.Inflate(num, num);
      if (boxBounds.Contains(x, y))
      {
        vTreeNode2.imageHovered = true;
        this.lastHoveredNode = vTreeNode2;
      }
      if (vTreeNode2 == null || !this.enableToolTips || this.hoveredNode == vTreeNode2)
        return;
      this.hoveredNode = vTreeNode2;
      this.timerToolTip.Stop();
      this.timerToolTip.Start();
      this.treeViewToolTip.RemoveAll();
      this.showPoint = this.PointToClient(Cursor.Position);
    }

    /// <summary>Fires the MouseMove.</summary>
    /// <param name="args"></param>
    public void InvalidateNodes(vTreeNodeCollection nodes)
    {
      foreach (vTreeNode node in nodes)
        node.highLighted = false;
      this.Refresh();
    }

    private vTreeNode NodeAt(Point pt)
    {
      if (this.backGrounds == null)
        return (vTreeNode) null;
      for (int index = 0; index < this.backGrounds.Length; ++index)
      {
        if (this.backGrounds[index].BindedNode != null && this.backGrounds[index].BindedNode.LabelBounds.Contains(new Point(pt.X - this.scrollPosition.X, pt.Y - this.scrollPosition.Y)))
          return this.backGrounds[index].BindedNode;
      }
      return (vTreeNode) null;
    }

    /// <exclude />
    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      this.scrollingTimer.Stop();
      this.Capture = false;
      vTreeNode node = this.NodeAt(e.Location);
      if (node != null)
        this.OnNodeMouseUp(node, e);
      if (this.AllowDragAndDrop && this.dragForm != null && this.dragForm.Visible)
      {
        vTreeNode vTreeNode = this.NodeAt(e.Location);
        this.dragForm.Hide();
        if (vTreeNode == null || vTreeNode == this.dragNode)
          return;
        vTreeViewDragCancelEventArgs e1 = new vTreeViewDragCancelEventArgs(this.dragNode, vTreeNode);
        if (this.BeforeDragEnd != null)
          this.BeforeDragEnd((object) this, e1);
        if (e1.Cancel)
          return;
        for (vTreeNode parent = vTreeNode.Parent; parent != null; parent = parent.Parent)
        {
          if (parent == this.dragNode)
            return;
        }
        if (this.dragNode.Parent != null)
          this.dragNode.Parent.Nodes.Remove(this.dragNode);
        else
          this.Nodes.Remove(this.dragNode);
        vTreeNode.Nodes.Add(this.dragNode);
        this.vScroll.Value = 0;
        this.InvalidateLayout();
        vTreeNode.Expand();
        vTreeViewDragEventArgs e2 = new vTreeViewDragEventArgs(this.dragNode, vTreeNode);
        if (this.AfterDragEnd != null)
          this.AfterDragEnd((object) this, e2);
        this.Capture = false;
        this.BringIntoView(vTreeNode);
      }
      if (e.Button == MouseButtons.Left && this.downSel != null)
      {
        Point point = this.ApplyScrollOffset(e.X, e.Y);
        this.HitTest(new Point(point.X, point.Y));
        this.downSel = (vTreeNode) null;
      }
      this.downNode = (vTreeNode) null;
    }

    /// <exclude />
    protected override void OnPaint(PaintEventArgs e)
    {
      if (!this.DesignMode)
        Licensing.LICheck((Control) this);
      base.OnPaint(e);
      if (this.nodes == null)
        return;
      Graphics graphics = e.Graphics;
      RectangleF clipBounds = graphics.ClipBounds;
      Matrix transform = graphics.Transform;
      transform.Translate((float) this.scrollPosition.X, (float) this.scrollPosition.Y);
      graphics.Transform = transform;
      this.linePen = new Pen(this.LineColor, 1f);
      this.linePen.DashStyle = DashStyle.Dot;
      this.linePen.LineJoin = LineJoin.Round;
      this.plusPen = new Pen(this.ForeColor);
      this.plusPen.Alignment = PenAlignment.Center;
      this.backBrush = (Brush) new SolidBrush(Color.White);
      Rectangle clip = new Rectangle((int) clipBounds.X - this.scrollPosition.X, (int) clipBounds.Y - this.scrollPosition.Y, (int) clipBounds.Width, (int) clipBounds.Height);
      this.nodesBackElement = 0;
      this.backGround.Bounds = new Rectangle(-this.scrollPosition.X, -this.scrollPosition.Y, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
      using (SolidBrush solidBrush = new SolidBrush(this.GetBackColor()))
        graphics.FillRectangle((Brush) solidBrush, this.backGround.Bounds);
      this.DrawNodes(graphics, ref this.nodesBackElement, ref clip, new LineStates(), this.TreeIndent, 3, this.nodes);
      if (this.vScroll.Visible && this.hScroll.Visible)
      {
        using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
          graphics.FillRectangle((Brush) solidBrush, this.ClientRectangle.Width - this.vScroll.Width - 1 - this.scrollPosition.X, this.ClientRectangle.Height - this.hScroll.Height - this.scrollPosition.Y - 1, this.vScroll.Width + 2, this.hScroll.Height + 2);
      }
      if (this.UseThemeBorderColor)
      {
        this.backGround.DrawElementBorder(graphics, ControlState.Normal);
      }
      else
      {
        using (Pen pen = new Pen(this.BorderColor))
        {
          Rectangle rect = new Rectangle(-this.scrollPosition.X, -this.scrollPosition.Y, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
          graphics.DrawRectangle(pen, rect);
        }
      }
      if (this.linePen != null)
      {
        this.linePen.Dispose();
        this.linePen = (Pen) null;
      }
      if (this.plusPen != null)
      {
        this.plusPen.Dispose();
        this.plusPen = (Pen) null;
      }
      if (this.backBrush == null)
        return;
      this.backBrush.Dispose();
      this.backBrush = (Brush) null;
    }

    internal void OnRemoveNode(vTreeNode node)
    {
      if (node != null && this.SelectedNode != null && (node == this.SelectedNode || node.Contains(this.SelectedNode)))
      {
        vTreeNodeCollection treeNodeCollection = node.Parent == null ? this.Nodes : node.Parent.Nodes;
        if (treeNodeCollection != null)
        {
          int count = treeNodeCollection.Count;
          vTreeNode vTreeNode1;
          if (node.Index == count - 1)
          {
            vTreeNode1 = node.PrevVisibleNode;
          }
          else
          {
            vTreeNode vTreeNode2 = treeNodeCollection[node.Index + 1];
            vTreeNode1 = !vTreeNode2.IsVisible ? vTreeNode2.NextVisibleNode : vTreeNode2;
          }
          this.SelectedNode = vTreeNode1;
        }
      }
      this.InvalidateLayout();
    }

    private bool OnSelectionChanging(vTreeNode node)
    {
      if (this.BeforeSelect == null)
        return false;
      vTreeViewCancelEventArgs e = new vTreeViewCancelEventArgs(node, false, vTreeViewAction.Unknown);
      this.BeforeSelect((object) this, e);
      return e.Cancel;
    }

    private void OnSelectionChanged()
    {
      if (this.AfterSelect == null)
        return;
      this.AfterSelect((object) this, new vTreeViewEventArgs(this.SelectedNode, vTreeViewAction.Unknown));
    }

    private void SetFocus(vTreeNode node)
    {
      if (this.focus == node)
        return;
      this.InvalidateNode(this.focus);
      this.focus = node;
      this.InvalidateNode(node);
      vTreeView.EnsureVisible(node);
    }

    /// <exclude />
    protected override void OnMouseWheel(MouseEventArgs e)
    {
      base.OnMouseWheel(e);
      if (this.vScroll.Visible)
      {
        if (e.Delta < 0)
        {
          if (this.vScroll.Value >= this.vScroll.Maximum)
            return;
          ++this.vScroll.Value;
          this.vScroll.SyncThumbPositionWithLogicalValue();
        }
        else
        {
          if (this.vScroll.Value <= this.vScroll.Minimum)
            return;
          --this.vScroll.Value;
          this.vScroll.SyncThumbPositionWithLogicalValue();
        }
      }
      else
      {
        if (!this.hScroll.Visible)
          return;
        if (e.Delta < 0)
        {
          if (this.hScroll.Value >= this.hScroll.Maximum)
            return;
          ++this.hScroll.Value;
          this.hScroll.SyncThumbPositionWithLogicalValue();
        }
        else
        {
          if (this.hScroll.Value <= this.hScroll.Minimum)
            return;
          --this.hScroll.Value;
          this.hScroll.SyncThumbPositionWithLogicalValue();
        }
      }
    }

    /// <summary>Scrolls to a given tree node, if needeed.</summary>
    /// <param name="treeNode"></param>
    public void BringIntoView(vTreeNode treeNode)
    {
      if (treeNode == null || this.backGrounds == null)
        return;
      for (int index = 0; index < this.backGrounds.Length; ++index)
      {
        if (this.backGrounds[index].BindedNode != null && this.backGrounds[index].BindedNode.Equals((object) treeNode))
          return;
      }
      this.ScrollPosition = -this.ScrollPosition.Y > treeNode.LabelBounds.Y ? new Point(this.ScrollPosition.X, this.ScrollPosition.Y - (treeNode.LabelBounds.Y + this.scrollPosition.Y)) : new Point(this.ScrollPosition.X, this.ScrollPosition.Y - (treeNode.LabelBounds.Y + this.scrollPosition.Y));
      this.vScroll.Value = -this.scrollPosition.Y / this.ItemHeight;
      this.vScroll.SyncThumbPositionWithLogicalValue();
    }
  }
}
