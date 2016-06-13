// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vTreeNode
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a tree node in a vTreeView control.</summary>
  [ComVisible(false)]
  [DefaultProperty("Text")]
  [ToolboxBitmap(typeof (vTreeNode), "ControlIcons.vTreeNode.ico")]
  [ToolboxItem(false)]
  public class vTreeNode : IComponent, IDisposable, INotifyPropertyChanged
  {
    private string descriptionText = "";
    private ContentAlignment descriptionTextAlignment = ContentAlignment.TopLeft;
    private Color descriptionTextColor = Color.Black;
    private Color? disabledForeColor = new Color?(Color.Silver);
    private ContentAlignment textAlignment = ContentAlignment.MiddleCenter;
    private bool useThemeTextColor = true;
    private bool useThemeBackground = true;
    private int extraWidth = 2;
    private byte roundedCornersMask = 15;
    private int roundedCornersRadius = 3;
    private Color backgroundBorder = Color.Black;
    private Color disabledBackgroundBorder = Color.Silver;
    private Brush disabledBackgroundBrush = (Brush) new SolidBrush(Color.Silver);
    private Color highlightBackgroundBorder = Color.Black;
    private Color selectedBackgroundBorder = Color.Black;
    private Brush backgroundBrush = (Brush) new SolidBrush(Color.WhiteSmoke);
    private Brush selectedBackgroundBrush = (Brush) new SolidBrush(Color.WhiteSmoke);
    private Brush highlightBackgroundBrush = (Brush) new SolidBrush(Color.WhiteSmoke);
    private bool enabled = true;
    /// <exclude />
    protected bool showCheckBox = true;
    private PaintHelper helper = new PaintHelper();
    private int imageIndex = -1;
    /// <exclude />
    protected int stateImageIndex = -1;
    /// <exclude />
    protected int selectedImageIndex = -1;
    internal int index = -1;
    private bool isLabelEditable = true;
    /// <exclude />
    protected string text = "Node";
    private string tooltipText = "";
    private Font descriptionTextFont;
    internal int bottom;
    /// <exclude />
    protected internal bool expanded;
    /// <exclude />
    protected Rectangle labelBounds;
    /// <exclude />
    protected vTreeNode parent;
    /// <exclude />
    protected internal bool selected;
    /// <exclude />
    protected vTreeView vTreeView;
    /// <exclude />
    protected bool visible;
    private Color? foreColor;
    private Color? checkMarkColor;
    private Color? hforeColor;
    private Color? sforeColor;
    private Font font;
    private int? itemHeight;
    internal bool highLighted;
    internal CheckState checkedNode;
    private Rectangle checkBoxBounds;
    private object tag;
    private object value;
    private vTreeNodeCollection nodes;
    internal bool imageHovered;
    private ISite site;

    /// <summary>Gets or sets the height of the tree node.</summary>
    [Category("Behavior")]
    [DefaultValue(null)]
    [Description("Gets or sets the height of the tree node.")]
    public int? ItemHeight
    {
      get
      {
        if (this.itemHeight.HasValue)
          return new int?(this.itemHeight.Value);
        return new int?();
      }
      set
      {
        int? nullable1 = value;
        int? nullable2 = this.itemHeight;
        if ((nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : (nullable1.HasValue != nullable2.HasValue ? 1 : 0)) == 0)
          return;
        this.itemHeight = value;
        this.OnPropertyChanged("ItemHeight");
        if (this.TreeView == null)
          return;
        this.TreeView.InvalidateLayout();
      }
    }

    /// <summary>Gets or sets node's extra width</summary>
    [DefaultValue(2)]
    [Category("Behavior")]
    [Description("Gets or sets node's extra width")]
    public int ExtraWidth
    {
      get
      {
        return this.extraWidth;
      }
      set
      {
        this.extraWidth = value;
        this.OnPropertyChanged("ExtraWidth");
        if (this.vTreeView == null)
          return;
        this.vTreeView.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use theme's background
    /// </summary>
    [Description("Gets or sets a value indicating whether to use theme's background")]
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool UseThemeBackground
    {
      get
      {
        return this.useThemeBackground;
      }
      set
      {
        if (value == this.useThemeBackground)
          return;
        this.useThemeBackground = value;
        this.OnPropertyChanged("UseThemeBackground");
        if (this.vTreeView == null)
          return;
        this.vTreeView.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use theme's textcolor
    /// </summary>
    [Description("Gets or sets a value indicating whether to use theme's textcolor")]
    [DefaultValue(true)]
    [Category("Behavior")]
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
        this.OnPropertyChanged("UseThemeTextColor");
        if (this.vTreeView == null)
          return;
        this.vTreeView.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the mask that determines which corners are rounded
    /// </summary>
    [Browsable(false)]
    public byte RoundedCornersMask
    {
      get
      {
        return this.roundedCornersMask;
      }
      set
      {
        this.roundedCornersMask = value;
      }
    }

    /// <summary>
    /// Gets or sets the rounded corners radius of the vTreeNode control.
    /// </summary>
    [Description("Gets or sets the rounded corners radius of the vTreeNode.")]
    [Category("Behavior")]
    [DefaultValue(3)]
    public int RoundedCornersRadius
    {
      get
      {
        return this.roundedCornersRadius;
      }
      set
      {
        if (value < 0)
          return;
        this.roundedCornersRadius = value;
      }
    }

    /// <summary>Gets or sets the background border.</summary>
    /// <value>The background border.</value>
    [DefaultValue(typeof (Color), "Black")]
    [Description("Gets or sets the background border.")]
    [Category("Appearance")]
    public Color BackgroundBorder
    {
      get
      {
        return this.backgroundBorder;
      }
      set
      {
        this.backgroundBorder = value;
      }
    }

    /// <summary>Gets or sets the disabledBackgroundBorder border.</summary>
    /// <value>The disabledBackgroundBorder border.</value>
    [DefaultValue(typeof (Color), "Silver")]
    [Category("Appearance")]
    [Description("Gets or sets the DisabledBackgroundBorder border.")]
    public Color DisabledBackgroundBorder
    {
      get
      {
        return this.disabledBackgroundBorder;
      }
      set
      {
        this.disabledBackgroundBorder = value;
      }
    }

    /// <summary>Gets or sets the disabled background brush.</summary>
    /// <value>The disabled background brush.</value>
    [Browsable(false)]
    [Category("Appearance")]
    [Description("Gets or sets the disabled background brush.")]
    public Brush DisabledBackgroundBrush
    {
      get
      {
        return this.disabledBackgroundBrush;
      }
      set
      {
        this.disabledBackgroundBrush = value;
      }
    }

    /// <summary>Gets or sets the highlightBackground border.</summary>
    /// <value>The highlightBackground border.</value>
    [Description("Gets or sets the highlightBackground border.")]
    [DefaultValue(typeof (Color), "Black")]
    [Category("Appearance")]
    public Color HighlightBackgroundBorder
    {
      get
      {
        return this.highlightBackgroundBorder;
      }
      set
      {
        this.highlightBackgroundBorder = value;
      }
    }

    /// <summary>Gets or sets the selectedBackground border.</summary>
    /// <value>The selectedBackground border.</value>
    [Description("Gets or sets the selectedBackground border.")]
    [DefaultValue(typeof (Color), "Black")]
    [Category("Appearance")]
    public Color SelectedBackgroundBorder
    {
      get
      {
        return this.selectedBackgroundBorder;
      }
      set
      {
        this.selectedBackgroundBorder = value;
      }
    }

    /// <summary>Gets or sets the background brush.</summary>
    /// <value>The background brush.</value>
    [Category("Appearance")]
    [Browsable(false)]
    [Description("Gets or sets the background brush.")]
    public Brush BackgroundBrush
    {
      get
      {
        return this.backgroundBrush;
      }
      set
      {
        this.backgroundBrush = value;
      }
    }

    /// <summary>Gets or sets the background brush.</summary>
    /// <value>The background brush.</value>
    [Browsable(false)]
    [Description("Gets or sets the selected background brush.")]
    [Category("Appearance")]
    public Brush SelectedBackgroundBrush
    {
      get
      {
        return this.selectedBackgroundBrush;
      }
      set
      {
        this.selectedBackgroundBrush = value;
      }
    }

    /// <summary>Gets or sets the HighlightBackground brush.</summary>
    /// <value>The HighlightBackground brush.</value>
    [Category("Appearance")]
    [Description("Gets or sets the HighlightBackground brush.")]
    [Browsable(false)]
    public Brush HighlightBackgroundBrush
    {
      get
      {
        return this.highlightBackgroundBrush;
      }
      set
      {
        this.highlightBackgroundBrush = value;
      }
    }

    /// <summary>Gets or sets the font.</summary>
    /// <value>The font.</value>
    [Description("Gets or sets the font.")]
    [Category("Appearance")]
    [DefaultValue(null)]
    public Font Font
    {
      get
      {
        return this.font;
      }
      set
      {
        if (this.font == value)
          return;
        this.font = value;
        if (this.TreeView != null && value != null)
          this.TreeView.PerformLayout();
        this.OnPropertyChanged("Font");
      }
    }

    /// <summary>Gets or sets the description text alignment.</summary>
    /// <value>The description text alignment.</value>
    [Category("Appearance")]
    [Description("Gets or sets the description text alignment.")]
    public ContentAlignment DescriptionTextAlignment
    {
      get
      {
        return this.descriptionTextAlignment;
      }
      set
      {
        this.descriptionTextAlignment = value;
        this.OnPropertyChanged("DescriptionTextAlignment");
      }
    }

    /// <summary>Gets or sets the description text font.</summary>
    /// <value>The description text font.</value>
    [Description("Gets or sets the description text font.")]
    [Category("Appearance")]
    public Font DescriptionTextFont
    {
      get
      {
        return this.descriptionTextFont;
      }
      set
      {
        this.descriptionTextFont = value;
        this.OnPropertyChanged("DescriptionTextFont");
      }
    }

    /// <summary>Gets or sets the color of the description text.</summary>
    /// <value>The color of the description text.</value>
    [Description("Gets or sets the color of the description text.")]
    [DefaultValue(typeof (Color), "Black")]
    [Category("Behavior")]
    public Color DescriptionTextColor
    {
      get
      {
        return this.descriptionTextColor;
      }
      set
      {
        this.descriptionTextColor = value;
        this.OnPropertyChanged("DescriptionTextColor");
      }
    }

    /// <summary>Gets or sets the description text.</summary>
    /// <value>The description text.</value>
    [Description("Gets or sets the description text.")]
    [DefaultValue("")]
    [Category("Behavior")]
    public string DescriptionText
    {
      get
      {
        return this.descriptionText;
      }
      set
      {
        this.descriptionText = value;
        this.OnPropertyChanged("DescriptionText");
      }
    }

    /// <summary>Gets or sets the CheckMarkColor</summary>
    [Description("Gets or sets the CheckMarkColor")]
    [DefaultValue(typeof (Color), "Black")]
    [Category("Appearance")]
    public Color CheckMarkColor
    {
      get
      {
        if (this.checkMarkColor.HasValue)
          return this.checkMarkColor.Value;
        return Color.Black;
      }
      set
      {
        Color color = value;
        Color? nullable = this.checkMarkColor;
        if ((!nullable.HasValue ? 1 : (color != nullable.GetValueOrDefault() ? 1 : 0)) == 0)
          return;
        this.checkMarkColor = new Color?(value);
        this.OnPropertyChanged("CheckMarkColor");
      }
    }

    /// <summary>Gets or sets the HighlightForeColor</summary>
    [Category("Appearance")]
    [DefaultValue(typeof (Color), "Black")]
    [Description("Gets or sets the HighlightForeColor")]
    public Color HighlightForeColor
    {
      get
      {
        if (this.hforeColor.HasValue)
          return this.hforeColor.Value;
        if (this.vTreeView != null)
          return this.vTreeView.ForeColor;
        return Color.Black;
      }
      set
      {
        Color color = value;
        Color? nullable = this.hforeColor;
        if ((!nullable.HasValue ? 1 : (color != nullable.GetValueOrDefault() ? 1 : 0)) == 0)
          return;
        this.hforeColor = new Color?(value);
        this.OnPropertyChanged("HighlightForeColor");
      }
    }

    /// <summary>Gets or sets the SelectedForeColor</summary>
    [DefaultValue(typeof (Color), "Black")]
    [Category("Appearance")]
    [Description("Gets or sets the SelectedForeColor")]
    public Color SelectedForeColor
    {
      get
      {
        if (this.sforeColor.HasValue)
          return this.sforeColor.Value;
        if (this.vTreeView != null)
          return this.vTreeView.ForeColor;
        return Color.Black;
      }
      set
      {
        Color color = value;
        Color? nullable = this.sforeColor;
        if ((!nullable.HasValue ? 1 : (color != nullable.GetValueOrDefault() ? 1 : 0)) == 0)
          return;
        this.sforeColor = new Color?(value);
        this.OnPropertyChanged("SelectedForeColor");
      }
    }

    /// <summary>Gets or sets the DisabledForeColor</summary>
    [Category("Appearance")]
    [DefaultValue(typeof (Color), "Silver")]
    [Description("Gets or sets the DisabledForeColor")]
    public Color? DisabledForeColor
    {
      get
      {
        return this.disabledForeColor;
      }
      set
      {
        Color? nullable1 = value;
        Color? nullable2 = this.disabledForeColor;
        if ((nullable1.HasValue != nullable2.HasValue ? 1 : (!nullable1.HasValue ? 0 : (nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0))) == 0)
          return;
        this.disabledForeColor = value;
        this.OnPropertyChanged("DisabledForeColor");
      }
    }

    /// <summary>Gets or sets the ForeColor</summary>
    [DefaultValue(typeof (Color), "Black")]
    [Description("Gets or sets the ForeColor")]
    [Category("Appearance")]
    public Color ForeColor
    {
      get
      {
        if (this.foreColor.HasValue)
          return this.foreColor.Value;
        if (this.vTreeView != null)
          return this.vTreeView.ForeColor;
        return Color.Black;
      }
      set
      {
        Color color = value;
        Color? nullable = this.foreColor;
        if ((!nullable.HasValue ? 1 : (color != nullable.GetValueOrDefault() ? 1 : 0)) == 0)
          return;
        this.foreColor = new Color?(value);
        this.OnPropertyChanged("ForeColor");
      }
    }

    /// <summary>Gets or sets the text alignment</summary>
    [Description("Gets or sets the text alignment")]
    [Category("Appearance")]
    public ContentAlignment TextAlignment
    {
      get
      {
        return this.textAlignment;
      }
      set
      {
        if (value == this.textAlignment)
          return;
        this.textAlignment = value;
        this.OnPropertyChanged("TextAlignment");
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:VIBlend.WinForms.Controls.vTreeNode" /> is enabled.
    /// </summary>
    /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
    [Description("Gets or sets a value indicating whether the vTreeNode is enabled.")]
    [Category("Behavior")]
    [DefaultValue(true)]
    public bool Enabled
    {
      get
      {
        return this.enabled;
      }
      set
      {
        this.enabled = value;
      }
    }

    /// <summary>Gets or sets whether the node is checked</summary>
    [Category("Behavior")]
    [Description("Gets or sets whether the node is checked.")]
    public CheckState Checked
    {
      get
      {
        return this.checkedNode;
      }
      set
      {
        this.checkedNode = value;
        if (this.TreeView == null)
          return;
        this.TreeView.OnNodeChecked(this);
      }
    }

    /// <summary>Gets or sets whether to show checkboxes</summary>
    [Description("Gets or sets whether to show checkboxes.")]
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool ShowCheckBox
    {
      get
      {
        return this.showCheckBox;
      }
      set
      {
        this.showCheckBox = value;
      }
    }

    /// <summary>
    /// Gets the depth of the node within the vTreeView control.
    /// </summary>
    [Browsable(false)]
    public int Depth
    {
      get
      {
        int num = 0;
        for (vTreeNode vTreeNode = this.parent; vTreeNode != null; vTreeNode = vTreeNode.parent)
          ++num;
        return num;
      }
    }

    /// <summary>
    /// Gets or sets the image index of the image associated with the tree node.
    /// </summary>
    [DefaultValue(-1)]
    [Category("Behavior")]
    public int ImageIndex
    {
      get
      {
        return this.imageIndex;
      }
      set
      {
        if (value == this.imageIndex)
          return;
        this.imageIndex = value;
        this.OnPropertyChanged("ImageIndex");
        if (this.vTreeView == null)
          return;
        this.vTreeView.InvalidateLayout();
        this.vTreeView.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the index of the image used to indicate the state of the TreeNode when the parent TreeView has its CheckBoxes property set to false.
    /// </summary>
    [DefaultValue(-1)]
    [Category("Behavior")]
    public int StateImageIndex
    {
      get
      {
        return this.stateImageIndex;
      }
      set
      {
        if (value == this.stateImageIndex)
          return;
        this.stateImageIndex = value;
        this.OnPropertyChanged("StateImageIndex");
      }
    }

    /// <summary>
    /// Gets or sets the image list index value of the image that is displayed when the tree node is in the selected state.
    /// </summary>
    [Category("Behavior")]
    [DefaultValue(-1)]
    public int SelectedImageIndex
    {
      get
      {
        return this.selectedImageIndex;
      }
      set
      {
        if (value == this.selectedImageIndex)
          return;
        this.selectedImageIndex = value;
        this.OnPropertyChanged("SelectedImageIndex");
      }
    }

    internal int Index
    {
      get
      {
        return this.index;
      }
      set
      {
        this.index = value;
      }
    }

    /// <summary>Checks if the node is expanded</summary>
    [Category("Behavior")]
    [Browsable(false)]
    public bool IsExpanded
    {
      get
      {
        return this.expanded;
      }
    }

    /// <summary>Gets or sets whether the node is editable.</summary>
    [DefaultValue(true)]
    [Category("Behavior")]
    [Description("Gets or sets whether the node is editable.")]
    public bool IsLabelEditable
    {
      get
      {
        return this.isLabelEditable;
      }
      set
      {
        if (value == this.isLabelEditable)
          return;
        this.isLabelEditable = value;
        this.OnPropertyChanged("IsLabelEditable");
      }
    }

    /// <summary>Gets or sets whether the node is currently visible.</summary>
    [DefaultValue(true)]
    [Browsable(false)]
    public bool IsVisible
    {
      get
      {
        return this.visible;
      }
      set
      {
        if (this.visible == value)
          return;
        this.visible = value;
        if (this.vTreeView != null)
          this.vTreeView.InvalidateLayout();
        this.OnPropertyChanged("Visible");
      }
    }

    internal Rectangle CheckBoxBounds
    {
      get
      {
        return this.checkBoxBounds;
      }
      set
      {
        this.checkBoxBounds = value;
      }
    }

    /// <summary>Gets or sets the label bounds.</summary>
    /// <value>The label bounds.</value>
    public Rectangle LabelBounds
    {
      get
      {
        return this.labelBounds;
      }
      internal set
      {
        this.labelBounds = value;
      }
    }

    /// <summary>
    /// Gets the next node. Returns null if there is no next node.
    /// </summary>
    [Browsable(false)]
    public vTreeNode NextNode
    {
      get
      {
        vTreeNode firstVisibleChild = vTreeNode.GetFirstVisibleChild(this);
        if (firstVisibleChild != this)
          return firstVisibleChild;
        vTreeNode vTreeNode1 = this.parent;
        vTreeNode vTreeNode2 = this;
        do
        {
          vTreeNodeCollection treeNodeCollection = vTreeNode1 == null ? (this.vTreeView != null ? this.vTreeView.Nodes : (vTreeNodeCollection) null) : vTreeNode1.Nodes;
          int index = treeNodeCollection.IndexOf((object) vTreeNode2) + 1;
          int count = treeNodeCollection.Count;
          if (index < count)
            return treeNodeCollection[index];
          vTreeNode2 = vTreeNode1;
          if (vTreeNode1 != null)
            vTreeNode1 = vTreeNode1.Parent;
        }
        while (vTreeNode2 != null);
        return (vTreeNode) null;
      }
    }

    /// <summary>
    /// Gets the next sibling node. Returns null if no such node exists.
    /// </summary>
    [Browsable(false)]
    public vTreeNode NextSiblingNode
    {
      get
      {
        if (this.ParentCollection != null && this.ParentCollection.Count > this.Index + 1)
          return this.ParentCollection[this.Index + 1];
        return (vTreeNode) null;
      }
    }

    /// <summary>
    /// Gets the prev sibling node. Returns null if no such node exists.
    /// </summary>
    [Browsable(false)]
    public vTreeNode PrevSiblingNode
    {
      get
      {
        if (this.ParentCollection != null && 0 <= this.Index - 1 && this.ParentCollection.Count > 0)
          return this.ParentCollection[this.Index - 1];
        return (vTreeNode) null;
      }
    }

    /// <summary>Gets or sets the Tag</summary>
    [Description("Gets or sets the Tag")]
    [DefaultValue(null)]
    [Category("Behavior")]
    public object Tag
    {
      get
      {
        return this.tag;
      }
      set
      {
        if (value == this.tag)
          return;
        this.tag = value;
        this.OnPropertyChanged("Tag");
      }
    }

    /// <summary>Gets or sets the Value</summary>
    [Category("Behavior")]
    [Description("Gets or sets the Value")]
    [DefaultValue(null)]
    public object Value
    {
      get
      {
        return this.value;
      }
      set
      {
        if (value == this.value)
          return;
        this.value = value;
        this.OnPropertyChanged("Value");
      }
    }

    /// <summary>
    /// Gets the next visible node. Returns null if no such node exists.
    /// </summary>
    [Browsable(false)]
    public vTreeNode NextVisibleNode
    {
      get
      {
        for (vTreeNode nextNode = this.NextNode; nextNode != null; nextNode = nextNode.NextNode)
        {
          if (nextNode.IsVisible)
            return nextNode;
        }
        return (vTreeNode) null;
      }
    }

    /// <summary>Gets the child nodes of the vTreeNode.</summary>
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public vTreeNodeCollection Nodes
    {
      get
      {
        if (this.nodes == null)
          this.nodes = new vTreeNodeCollection(this.TreeView, this);
        return this.nodes;
      }
    }

    /// <summary>Gets the parent node of the vTreeNode.</summary>
    [Browsable(false)]
    [DefaultValue(null)]
    public virtual vTreeNode Parent
    {
      get
      {
        return this.parent;
      }
      set
      {
        this.parent = value;
      }
    }

    /// <summary>Gets the parent nodes collection.</summary>
    /// <remarks>
    /// The returned collection can be either the nodes collection of the parent node or the collection of tree nodes at root level.
    /// </remarks>
    internal vTreeNodeCollection ParentCollection
    {
      get
      {
        vTreeNode vTreeNode = this.parent;
        if (vTreeNode != null)
          return vTreeNode.Nodes;
        if (this.vTreeView == null)
          return (vTreeNodeCollection) null;
        return this.vTreeView.Nodes;
      }
    }

    /// <summary>
    /// Get the previous node. Returns null if no such node exists.
    /// </summary>
    [Browsable(false)]
    public vTreeNode PrevNode
    {
      get
      {
        vTreeNodeCollection parentCollection = this.ParentCollection;
        if (parentCollection == null)
          return (vTreeNode) null;
        int index = parentCollection.IndexOf((object) this) - 1;
        if (index >= 0)
          return parentCollection[index];
        return this.parent;
      }
    }

    /// <summary>
    /// Get the previous visible node. Returns null if no such node exists.
    /// </summary>
    [Browsable(false)]
    public vTreeNode PrevVisibleNode
    {
      get
      {
        for (vTreeNode prevNode = this.PrevNode; prevNode != null; prevNode = prevNode.PrevNode)
        {
          if (prevNode.IsVisible)
          {
            if (prevNode == this.parent)
              return prevNode;
            return this.GetLastVisibleChild(prevNode);
          }
        }
        return (vTreeNode) null;
      }
    }

    /// <summary>Gets or sets whether the node is selected.</summary>
    [DefaultValue(false)]
    public bool Selected
    {
      get
      {
        return this.selected;
      }
      set
      {
        if (this.selected == value)
          return;
        this.selected = value;
        if (this.TreeView != null && value)
          this.TreeView.SelectedNode = this;
        this.OnPropertyChanged("Selected");
      }
    }

    /// <summary>Gets or sets the text of the node.</summary>
    [Category("Behavior")]
    [Description("Gets or sets the text of the node.")]
    public string Text
    {
      get
      {
        return this.text;
      }
      set
      {
        if (!(value != this.text))
          return;
        this.text = value;
        this.tooltipText = value;
        if (this.TreeView != null)
          this.TreeView.Invalidate();
        this.OnPropertyChanged("Text");
      }
    }

    /// <summary>Gets or sets the tooltip text of the tree node</summary>
    [Description("Gets or sets the tooltip text of the tree node")]
    [Category("Appearance")]
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
        if (!(value != this.tooltipText))
          return;
        this.tooltipText = value != null ? value : "";
        this.OnPropertyChanged("TooltipText");
      }
    }

    /// <summary>Gets the tree view which hosts the tree node.</summary>
    [Browsable(false)]
    public vTreeView TreeView
    {
      get
      {
        if (this.vTreeView == null && this.Parent != null)
          this.vTreeView = this.Parent.TreeView;
        return this.vTreeView;
      }
      internal set
      {
        this.vTreeView = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public ISite Site
    {
      get
      {
        return this.site;
      }
      set
      {
        this.site = value;
      }
    }

    /// <summary>Occurs when the LabelBounds property is calculated.</summary>
    [Category("Action")]
    public event EventHandler<vTreeViewLayoutEventArgs> LayoutUpdated;

    public event EventHandler Disposed;

    /// <summary>Occurs when a property is changed.</summary>
    [Description("Occurs when a property is changed.")]
    [Category("Action")]
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>Constructor</summary>
    public vTreeNode()
    {
      this.visible = true;
    }

    /// <summary>Constructor</summary>
    /// <param name="text">The text of the node.</param>
    public vTreeNode(string text)
      : this()
    {
      this.Text = text;
    }

    /// <summary>Constructor</summary>
    /// <param name="parent">The parent node under which this node will be added.</param>
    public vTreeNode(vTreeNode parent)
    {
      this.visible = true;
      this.parent = parent;
    }

    ~vTreeNode()
    {
      this.Dispose(false);
    }

    private void ResetDescriptionTextAlignment()
    {
      this.DescriptionTextAlignment = ContentAlignment.TopLeft;
    }

    private bool ShouldSerializeDescriptionTextAlignment()
    {
      return this.DescriptionTextAlignment != ContentAlignment.TopLeft;
    }

    private void ResetDescriptionTextFont()
    {
      this.DescriptionTextFont = Control.DefaultFont;
    }

    private bool ShouldSerializeDescriptionTextFont()
    {
      return !this.DescriptionTextFont.Equals((object) Control.DefaultFont);
    }

    private void ResetTextAlignment()
    {
      this.TextAlignment = ContentAlignment.MiddleCenter;
    }

    private bool ShouldSerializeTextAlignment()
    {
      return this.TextAlignment != ContentAlignment.MiddleCenter;
    }

    /// <summary>Opens a textbox editor to edit the node.</summary>
    public void BeginEdit()
    {
      if (this.vTreeView == null || !this.vTreeView.LabelEdit || !this.isLabelEditable)
        return;
      this.vTreeView.SelectedNode = this;
      this.vTreeView.BeginEdit((string) null);
    }

    /// <summary>Collapses the TreeNode.</summary>
    public virtual void Collapse()
    {
      if (!this.expanded)
        return;
      if (this.vTreeView != null)
      {
        vTreeViewCancelEventArgs args = new vTreeViewCancelEventArgs(this, false, vTreeViewAction.Collapse);
        this.vTreeView.OnBeforeCollapse(args);
        if (args.Cancel)
          return;
      }
      if (this.vTreeView.enableToggleAnimation && this.vTreeView.opacityAnimationFromMouseDown)
        return;
      this.expanded = false;
      this.vTreeView.InvalidateLayout();
      if (this.vTreeView == null)
        return;
      this.vTreeView.OnAfterCollapse(this);
    }

    public virtual void Collapse(bool withoutAnimations)
    {
      if (!withoutAnimations)
      {
        this.Collapse();
      }
      else
      {
        if (!this.expanded)
          return;
        if (this.vTreeView != null)
        {
          vTreeViewCancelEventArgs args = new vTreeViewCancelEventArgs(this, false, vTreeViewAction.Collapse);
          this.vTreeView.OnBeforeCollapse(args);
          if (args.Cancel)
            return;
        }
        if (!withoutAnimations)
          return;
        this.expanded = false;
        this.vTreeView.InvalidateLayout();
        if (this.vTreeView == null)
          return;
        this.vTreeView.OnAfterCollapse(this);
      }
    }

    /// <summary>
    /// Checks if the node contains a specific child node under it.
    /// </summary>
    /// <param name="node">The child node to look for.</param>
    /// <returns>true if the node contains the child node; otherwise false;</returns>
    public bool Contains(vTreeNode node)
    {
      if (this.Nodes.Contains((object) node))
        return true;
      foreach (vTreeNode node1 in this.nodes)
      {
        if (node1.Contains(node))
          return true;
      }
      return false;
    }

    /// <summary>Draws the check box.</summary>
    /// <param name="g">The g.</param>
    /// <param name="rect">The rect.</param>
    /// <param name="isEnabled">if set to <c>true</c> [is enabled].</param>
    /// <param name="isMouseOver">if set to <c>true</c> [is mouse over].</param>
    /// <param name="isFocused">if set to <c>true</c> [is focused].</param>
    /// <param name="checkState">State of the check.</param>
    /// <param name="state">The state.</param>
    public void DrawCheckBox(Graphics g, Rectangle rect, bool isEnabled, bool isMouseOver, bool isFocused, CheckState checkState, ControlState state)
    {
      this.DrawCheckBox(g, rect, isEnabled, isMouseOver, isFocused, checkState, state, this.TreeView.VIBlendTheme);
    }

    /// <summary>Draws the check box.</summary>
    /// <param name="g">The g.</param>
    /// <param name="rect">The rect.</param>
    /// <param name="isEnabled">if set to <c>true</c> [is enabled].</param>
    /// <param name="isMouseOver">if set to <c>true</c> [is mouse over].</param>
    /// <param name="isFocused">if set to <c>true</c> [is focused].</param>
    /// <param name="checkState">State of the check.</param>
    /// <param name="state">The state.</param>
    /// <param name="theme">The theme.</param>
    public void DrawCheckBox(Graphics g, Rectangle rect, bool isEnabled, bool isMouseOver, bool isFocused, CheckState checkState, ControlState state, VIBLEND_THEME theme)
    {
      this.TreeView.checkBox.UseThemeCheckMarkColors = true;
      this.TreeView.checkBox.Bounds = new Rectangle(0, 0, 13, 13);
      if (this.TreeView.checkBox.VIBlendTheme != theme)
        this.TreeView.checkBox.VIBlendTheme = theme;
      this.TreeView.checkBox.Text = "";
      this.TreeView.checkBox.CheckState = checkState;
      this.TreeView.checkBox.Enabled = isEnabled;
      this.TreeView.checkBox.Bounds = rect;
      bool flag = false;
      if (this.checkMarkColor.HasValue)
      {
        this.TreeView.checkBox.UseThemeCheckMarkColors = false;
        this.TreeView.checkBox.CheckMarkColor = this.CheckMarkColor;
      }
      this.TreeView.checkBox.DrawCheckBox(g, rect, flag ? ControlState.Hover : ControlState.Normal);
    }

    private void ResetChecked()
    {
      this.Checked = CheckState.Unchecked;
    }

    private bool ShouldSerializeChecked()
    {
      return this.Checked != CheckState.Unchecked;
    }

    internal Bitmap GetNodeBitmap()
    {
      Bitmap bitmap = new Bitmap(this.labelBounds.Width, this.labelBounds.Height);
      BackgroundElement backgroundElement = new BackgroundElement(new Rectangle(0, 0, this.labelBounds.Width, this.labelBounds.Height), (IScrollableControlBase) this.vTreeView);
      ControlTheme copy = this.TreeView.Theme.CreateCopy();
      copy.StyleNormal.FillStyle = (FillStyle) new FillStyleSolid(this.TreeView.BackColor);
      copy.StyleNormal.BorderColor = this.TreeView.BorderColor;
      backgroundElement.LoadTheme(copy);
      using (Graphics g = Graphics.FromImage((Image) bitmap))
      {
        string text = this.Text;
        if (text != null)
        {
          if (this.vTreeView != null)
          {
            SolidBrush solidBrush;
            if (this.selected && this.vTreeView.Focused)
            {
              solidBrush = new SolidBrush(this.vTreeView.Theme.StylePressed.TextColor);
              backgroundElement.DrawElementFill(g, ControlState.Pressed);
              backgroundElement.Bounds = new Rectangle(backgroundElement.Bounds.X, backgroundElement.Bounds.Y, backgroundElement.Bounds.Width - 1, backgroundElement.Bounds.Height - 1);
              backgroundElement.DrawElementBorder(g, ControlState.Pressed);
            }
            else if (this.highLighted)
            {
              backgroundElement.DrawElementFill(g, ControlState.Hover);
              backgroundElement.Bounds = new Rectangle(backgroundElement.Bounds.X, backgroundElement.Bounds.Y, backgroundElement.Bounds.Width - 1, backgroundElement.Bounds.Height - 1);
              backgroundElement.DrawElementBorder(g, ControlState.Hover);
              solidBrush = new SolidBrush(this.vTreeView.Theme.StyleHighlight.TextColor);
            }
            else
            {
              backgroundElement.DrawElementFill(g, ControlState.Normal);
              backgroundElement.Bounds = new Rectangle(backgroundElement.Bounds.X, backgroundElement.Bounds.Y, backgroundElement.Bounds.Width - 1, backgroundElement.Bounds.Height - 1);
              backgroundElement.DrawElementBorder(g, ControlState.Normal);
              solidBrush = new SolidBrush(this.vTreeView.Theme.StyleNormal.TextColor);
            }
            StringFormat stringFormat = ImageAndTextHelper.GetStringFormat((Control) this.vTreeView, this.TextAlignment, true, true);
            if (!this.UseThemeTextColor)
              solidBrush.Color = this.ForeColor;
            Font font = this.vTreeView.Font;
            if (this.Font != null)
              font = this.Font;
            g.DrawString(text, font, (Brush) solidBrush, (RectangleF) new Rectangle(0, 0, this.labelBounds.Width, this.labelBounds.Height), stringFormat);
            solidBrush.Dispose();
          }
        }
      }
      return bitmap;
    }

    internal List<vTreeNode> GetChildren(List<vTreeNode> foundChildren, vTreeNodeCollection nodes)
    {
      foreach (vTreeNode node in nodes)
      {
        foundChildren.Add(node);
        if (node.Nodes.Count > 0)
          node.GetChildren(foundChildren, node.Nodes);
      }
      return foundChildren;
    }

    /// <summary>Draws the specified g.</summary>
    /// <param name="g">The g.</param>
    /// <param name="backGround">The background.</param>
    /// <param name="f">The f.</param>
    /// <param name="pen">The pen.</param>
    /// <param name="state">The state.</param>
    /// <param name="lineHeight">Height of the line.</param>
    /// <param name="indent">The indent.</param>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="imgSize">Size of the img.</param>
    /// <param name="img">The img.</param>
    /// <param name="selected">if set to <c>true</c> [selected].</param>
    protected internal virtual void Draw(Graphics g, TreeBackGroundElement backGround, Font f, Pen pen, LineStates state, int lineHeight, int indent, int x, int y, ref Size imgSize, Image img, bool selected)
    {
      int x1 = x;
      for (int index = 0; index < state.Depth; ++index)
      {
        LineState lineState = state[index];
        int num1 = x + imgSize.Width / 2;
        int x2 = x + indent;
        int num2 = y + (lineHeight + this.TreeView.NodesSpacing) / 2;
        int y2 = y + lineHeight + this.TreeView.NodesSpacing;
        bool flag = index + 1 == state.Depth;
        int num3 = -1;
        if (this.Parent == null && this.TreeView.Nodes.IndexOf((object) this) == 0)
          num3 = 1;
        if (this.TreeView.ShowRootLines)
        {
          if (flag && !this.TreeView.FullRowSelect)
            g.DrawLine(pen, num1, num2, x2, num2);
          if ((lineState & LineState.Last) == LineState.Last)
          {
            if (((lineState & LineState.HasParent) == LineState.HasParent || (lineState & LineState.First) == LineState.None && (lineState & LineState.Last) == LineState.Last) && (flag && num3 == -1 && !this.TreeView.FullRowSelect))
              g.DrawLine(pen, num1, y, num1, num2);
          }
          else if ((lineState & LineState.HasParent) == LineState.HasParent || (lineState & LineState.First) == LineState.None)
          {
            if (num3 == -1)
            {
              if (!this.TreeView.FullRowSelect)
                g.DrawLine(pen, num1, y, num1, y2);
            }
            else if (!this.TreeView.FullRowSelect)
              g.DrawLine(pen, num1, num2, num1, y2);
          }
          else if ((lineState & LineState.First) == LineState.First && !this.TreeView.FullRowSelect)
            g.DrawLine(pen, num1, num2, num1, y2);
        }
        x += indent;
      }
      string text = this.Text;
      if (text == null || this.vTreeView == null)
        return;
      backGround.RoundedCornersBitmask = this.roundedCornersMask;
      backGround.Radius = this.RoundedCornersRadius;
      this.CalculateBounds(g, f, lineHeight, x1, indent, state.Depth, y, imgSize);
      backGround.Bounds = this.labelBounds;
      this.checkBoxBounds = new Rectangle(x, y + this.LabelBounds.Height / 2 - 6, 13, 13);
      DrawNodeEventArgs args = new DrawNodeEventArgs(g, this, this.highLighted, selected, backGround);
      this.vTreeView.OnDrawTreeNode(args);
      if (args.Handled)
        return;
      if (this.TreeView.FullRowSelect && selected)
        backGround.Bounds = new Rectangle(this.TreeView.hScroll.Value, backGround.Bounds.Y, this.TreeView.Width, backGround.Bounds.Height);
      Color color1 = Color.FromArgb((int) ((double) byte.MaxValue * (double) backGround.Opacity), this.TreeView.Theme.StyleNormal.TextColor);
      Color baseColor1 = this.TreeView.Theme.QueryColorSetter("TreeForeColor");
      if (baseColor1 != Color.Empty)
        color1 = Color.FromArgb((int) ((double) byte.MaxValue * (double) backGround.Opacity), baseColor1);
      ControlState state1 = ControlState.Normal;
      SolidBrush brush;
      if (!this.Enabled)
      {
        brush = new SolidBrush(color1);
        if (!this.UseThemeBackground)
        {
          GraphicsPath partiallyRoundedPath = this.helper.CreatePartiallyRoundedPath(backGround.Bounds, this.RoundedCornersRadius, this.RoundedCornersMask);
          g.FillPath(this.DisabledBackgroundBrush, partiallyRoundedPath);
          backGround.Bounds = new Rectangle(backGround.Bounds.X, backGround.Bounds.Y, backGround.Bounds.Width - 1, backGround.Bounds.Height - 1);
          Color backgroundBorder = this.DisabledBackgroundBorder;
          backGround.DrawElementBorder(g, partiallyRoundedPath, this.DisabledBackgroundBorder);
        }
        else
        {
          backGround.DrawElementFill(g, ControlState.Disabled);
          backGround.Bounds = new Rectangle(backGround.Bounds.X, backGround.Bounds.Y, backGround.Bounds.Width - 1, backGround.Bounds.Height - 1);
          backGround.DrawElementBorder(g, ControlState.Disabled);
        }
      }
      else if (selected)
      {
        Color color2 = Color.FromArgb((int) ((double) byte.MaxValue * (double) backGround.Opacity), this.TreeView.Theme.StylePressed.TextColor);
        if (this.TreeView != null)
        {
          Color baseColor2 = this.TreeView.Theme.QueryColorSetter("TreeSelectedForeColor");
          if (!baseColor2.IsEmpty)
            color2 = Color.FromArgb((int) ((double) byte.MaxValue * (double) backGround.Opacity), baseColor2);
        }
        brush = new SolidBrush(color2);
        if (!this.UseThemeBackground)
        {
          GraphicsPath partiallyRoundedPath = this.helper.CreatePartiallyRoundedPath(backGround.Bounds, this.RoundedCornersRadius, this.RoundedCornersMask);
          g.FillPath(this.SelectedBackgroundBrush, partiallyRoundedPath);
          backGround.Bounds = new Rectangle(backGround.Bounds.X, backGround.Bounds.Y, backGround.Bounds.Width - 1, backGround.Bounds.Height - 1);
          Color backgroundBorder = this.SelectedBackgroundBorder;
          backGround.DrawElementBorder(g, partiallyRoundedPath, this.SelectedBackgroundBorder);
        }
        else
        {
          backGround.DrawElementFill(g, ControlState.Pressed);
          backGround.Bounds = new Rectangle(backGround.Bounds.X, backGround.Bounds.Y, backGround.Bounds.Width - 1, backGround.Bounds.Height - 1);
          backGround.DrawElementBorder(g, ControlState.Pressed);
        }
      }
      else if (this.highLighted)
      {
        if (!this.UseThemeBackground)
        {
          GraphicsPath partiallyRoundedPath = this.helper.CreatePartiallyRoundedPath(backGround.Bounds, this.RoundedCornersRadius, this.RoundedCornersMask);
          g.FillPath(this.HighlightBackgroundBrush, partiallyRoundedPath);
          backGround.Bounds = new Rectangle(backGround.Bounds.X, backGround.Bounds.Y, backGround.Bounds.Width - 1, backGround.Bounds.Height - 1);
          Color backgroundBorder = this.HighlightBackgroundBorder;
          backGround.DrawElementBorder(g, partiallyRoundedPath, this.HighlightBackgroundBorder);
        }
        else
        {
          backGround.DrawElementFill(g, ControlState.Hover);
          backGround.Bounds = new Rectangle(backGround.Bounds.X, backGround.Bounds.Y, backGround.Bounds.Width - 1, backGround.Bounds.Height - 1);
          backGround.DrawElementBorder(g, ControlState.Hover);
          state1 = ControlState.Hover;
          backGround.Bounds = new Rectangle(backGround.Bounds.X + 1, backGround.Bounds.Y + 1, backGround.Bounds.Width - 2, backGround.Bounds.Height - 2);
          Color color2 = this.TreeView.Theme.QueryColorSetter("ButtonHighlightInnerBorderColor");
          if (color2 != Color.Empty)
            backGround.DrawElementBorder(g, ControlState.Hover, color2);
          color1 = Color.FromArgb((int) ((double) byte.MaxValue * (double) backGround.Opacity), this.TreeView.Theme.StyleHighlight.TextColor);
          if (this.TreeView != null)
          {
            Color baseColor2 = this.TreeView.Theme.QueryColorSetter("TreeHighlightForeColor");
            if (!baseColor2.IsEmpty)
              color1 = Color.FromArgb((int) ((double) byte.MaxValue * (double) backGround.Opacity), baseColor2);
          }
        }
        brush = new SolidBrush(color1);
      }
      else
      {
        if (!this.UseThemeBackground)
        {
          GraphicsPath partiallyRoundedPath = this.helper.CreatePartiallyRoundedPath(backGround.Bounds, this.RoundedCornersRadius, this.RoundedCornersMask);
          if (this.TreeView.PaintNodesDefaultFill)
            g.FillPath(this.BackgroundBrush, partiallyRoundedPath);
          backGround.Bounds = new Rectangle(backGround.Bounds.X, backGround.Bounds.Y, backGround.Bounds.Width - 1, backGround.Bounds.Height - 1);
          if (this.TreeView.PaintNodesDefaultBorder)
          {
            Color backgroundBorder = this.BackgroundBorder;
            backGround.DrawElementBorder(g, partiallyRoundedPath, this.BackgroundBorder);
          }
        }
        else
        {
          if (this.TreeView.PaintNodesDefaultFill)
            backGround.DrawElementFill(g, ControlState.Normal);
          if (this.TreeView.PaintNodesDefaultBorder)
          {
            backGround.Bounds = new Rectangle(backGround.Bounds.X, backGround.Bounds.Y, backGround.Bounds.Width - 1, backGround.Bounds.Height - 1);
            backGround.DrawElementBorder(g, ControlState.Normal);
          }
        }
        brush = new SolidBrush(color1);
      }
      this.DrawImage(g, lineHeight, x, y, imgSize, img, backGround.Opacity);
      if (this.vTreeView.CheckBoxes && this.ShowCheckBox)
      {
        this.checkBoxBounds = new Rectangle(x, y + this.LabelBounds.Height / 2 - 6, 13, 13);
        this.DrawCheckBox(g, this.checkBoxBounds, this.vTreeView.Enabled, this.highLighted, false, this.Checked, state1);
      }
      if (!this.Enabled)
      {
        if (!this.UseThemeTextColor)
        {
          Color? disabledForeColor = this.DisabledForeColor;
          if (disabledForeColor.HasValue)
          {
            disabledForeColor = this.DisabledForeColor;
            brush = new SolidBrush(disabledForeColor.Value);
          }
        }
        else
          brush = new SolidBrush(backGround.DisabledForeColor);
      }
      this.DrawText(g, backGround, f, text, brush);
      if (this.Selected && this.TreeView.Focused && this.TreeView.ShowFocusRectangle)
      {
        Rectangle bounds = backGround.Bounds;
        bounds.Inflate(-2, -2);
        ControlPaint.DrawFocusRectangle(g, bounds);
      }
      brush.Dispose();
    }

    public void DrawImage(Graphics g, int lineHeight, int x, int y, Size imgSize, Image img, float opacity)
    {
      int width = imgSize.Width;
      int height = imgSize.Height;
      if (img == null)
        return;
      int y1 = y;
      if (height < lineHeight)
        y1 += (lineHeight - height) / 2;
      int x1 = x;
      if (this.vTreeView.CheckBoxes && this.ShowCheckBox)
        x1 += 15;
      Rectangle rectangle = new Rectangle(x1, y1, width, height);
      this.helper.DrawBitmap(g, img, x1, y1, width, height, (double) opacity);
    }

    /// <summary>Draws the text.</summary>
    /// <param name="g">The g.</param>
    /// <param name="backGround">The back ground.</param>
    /// <param name="f">The f.</param>
    /// <param name="label">The label.</param>
    /// <param name="brush">The brush.</param>
    public virtual void DrawText(Graphics g, TreeBackGroundElement backGround, Font f, string label, SolidBrush brush)
    {
      ImageAndTextHelper.GetStringFormat((Control) this.vTreeView, this.TextAlignment, true, true);
      if (!this.UseThemeTextColor)
      {
        brush.Color = Color.FromArgb((int) ((double) backGround.Opacity * (double) byte.MaxValue), this.ForeColor);
        if (this.highLighted)
          brush.Color = Color.FromArgb((int) ((double) backGround.Opacity * (double) byte.MaxValue), this.HighlightForeColor);
        else if (this.Selected)
          brush.Color = Color.FromArgb((int) ((double) backGround.Opacity * (double) byte.MaxValue), this.SelectedForeColor);
      }
      Font font = f;
      if (this.Font != null)
        font = this.Font;
      if (this.DescriptionTextFont == null)
        this.DescriptionTextFont = font;
      SolidBrush solidBrush = new SolidBrush(this.DescriptionTextColor);
      solidBrush.Color = Color.FromArgb((int) ((double) byte.MaxValue * (double) backGround.Opacity), solidBrush.Color);
      this.DrawNodeTextAndDescription(g, this.labelBounds, this, (Brush) brush, (Brush) solidBrush, font);
    }

    /// <summary>Draws the node text and description.</summary>
    /// <param name="graphics">The graphics.</param>
    /// <param name="bounds">The bounds.</param>
    /// <param name="node">The node.</param>
    /// <param name="state">The state.</param>
    /// <param name="brush">The brush.</param>
    /// <param name="descriptionBrush">The description brush.</param>
    public virtual void DrawNodeTextAndDescription(Graphics graphics, Rectangle bounds, vTreeNode node, Brush brush, Brush descriptionBrush, Font font)
    {
      StringFormat stringFormat1 = ImageAndTextHelper.GetStringFormat((Control) this.vTreeView, this.TextAlignment, true, true);
      StringFormat stringFormat2 = ImageAndTextHelper.GetStringFormat((Control) this.vTreeView, this.DescriptionTextAlignment, true, true);
      if (this.TreeView.RightToLeft == RightToLeft.Yes)
        stringFormat1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
      int num1 = node.Text != string.Empty ? 1 : 0;
      string[] strArray = node.DescriptionText.Split('\n');
      foreach (string str in strArray)
      {
        if (str.Length > 0)
          ++num1;
      }
      if (num1 == 0)
        num1 = 1;
      double num2 = (double) (bounds.Height / num1);
      for (int index = 0; index < num1; ++index)
      {
        Rectangle rectangle = new Rectangle(bounds.X, bounds.Y + (int) ((double) index * num2) + 1, bounds.Width, (int) num2);
        --rectangle.Height;
        if (rectangle.Width != 0 && rectangle.Height != 0)
        {
          if (index == 0 && node.Text.Length != 0)
          {
            rectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            graphics.DrawString(node.Text, font, brush, (RectangleF) rectangle, stringFormat1);
          }
          else
          {
            string s = node.Text.Length != 0 ? strArray[index - 1] : strArray[index];
            graphics.DrawString(s, this.DescriptionTextFont, descriptionBrush, (RectangleF) rectangle, stringFormat2);
          }
        }
      }
    }

    /// <summary>Ends the node edit</summary>
    /// <param name="cancel">Determines whether to apply the changes or not</param>
    public void EndEdit(bool cancel)
    {
      if (this.vTreeView == null)
        return;
      this.vTreeView.EndEdit(cancel);
    }

    /// <summary>Expands the node.</summary>
    public virtual void Expand()
    {
      if (this.vTreeView != null && this.vTreeView.IsUpdating)
      {
        this.expanded = true;
      }
      else
      {
        if (this.Nodes.Count <= 0)
          return;
        if (this.vTreeView != null)
        {
          vTreeViewCancelEventArgs args = new vTreeViewCancelEventArgs(this, false, vTreeViewAction.Expand);
          this.vTreeView.OnBeforeExpand(args);
          if (args.Cancel)
            return;
        }
        this.expanded = true;
        if (this.vTreeView == null)
          return;
        this.vTreeView.OnAfterExpand(this);
        this.vTreeView.InvalidateLayout();
      }
    }

    internal static vTreeNode GetFirstVisibleChild(vTreeNode n)
    {
      if (n.IsExpanded && n.Nodes.Count > 0)
      {
        foreach (vTreeNode node in n.Nodes)
        {
          if (node.IsVisible)
            return node;
        }
      }
      return n;
    }

    internal static int GetGap(int indent)
    {
      return 0;
    }

    internal vTreeNode GetLastVisibleChild(vTreeNode n)
    {
      vTreeNode vTreeNode = n;
      if (n.IsExpanded && n.Nodes.Count > 0)
      {
        vTreeNode lastVisibleNode = vTreeNode.GetLastVisibleNode(n.Nodes);
        if (lastVisibleNode != null)
          vTreeNode = lastVisibleNode;
      }
      return vTreeNode;
    }

    internal static vTreeNode GetLastVisibleNode(vTreeNodeCollection nodes)
    {
      if (nodes != null)
      {
        for (int index = nodes.Count - 1; index >= 0; --index)
        {
          vTreeNode vTreeNode = nodes[index];
          if (vTreeNode.IsVisible)
          {
            if (!vTreeNode.IsExpanded)
              return vTreeNode;
            return vTreeNode.GetLastVisibleNode(vTreeNode.Nodes);
          }
        }
      }
      return (vTreeNode) null;
    }

    internal Rectangle LabelAndImageBounds(Size imgSize, int indent)
    {
      int gap = vTreeNode.GetGap(indent);
      return new Rectangle(this.LabelBounds.Left - imgSize.Width - gap, this.LabelBounds.Top, this.LabelBounds.Width + imgSize.Width, this.LabelBounds.Height);
    }

    /// <summary>Calculates the bounds.</summary>
    /// <param name="g">The g.</param>
    /// <param name="f">The f.</param>
    /// <param name="lineHeight">Height of the line.</param>
    /// <param name="x">The x.</param>
    /// <param name="indent">The indent.</param>
    /// <param name="depth">The depth.</param>
    /// <param name="y">The y.</param>
    /// <param name="imgSize">Size of the img.</param>
    protected internal virtual void CalculateBounds(Graphics g, Font f, int lineHeight, int x, int indent, int depth, int y, Size imgSize)
    {
      int val1 = 10;
      if (this.Text != null)
      {
        SizeF sizeF = g.MeasureString(this.Text, f);
        if (this.Font != null)
          sizeF = g.MeasureString(this.Text, this.Font);
        val1 = Math.Max(val1, (int) sizeF.Width);
      }
      Image treeNodeImage = this.TreeView.GetTreeNodeImage(this, this.selected, this.IsExpanded);
      int num1 = imgSize.Width + vTreeNode.GetGap(indent) + 1;
      if (treeNodeImage == null)
        num1 = vTreeNode.GetGap(indent) + 1;
      int num2 = 0;
      if (this.vTreeView.CheckBoxes && this.ShowCheckBox)
        num2 = 15;
      if (this.ItemHeight.HasValue)
        lineHeight = this.ItemHeight.Value;
      this.LabelBounds = new Rectangle(x + indent * depth + num1 + num2, y, val1 + 6 + this.ExtraWidth, lineHeight);
      vTreeViewLayoutEventArgs args = new vTreeViewLayoutEventArgs(this, this.LabelBounds);
      this.OnLayoutUpdated(args);
      this.LabelBounds = args.Bounds;
    }

    /// <summary>
    /// Raises the <see cref="E:LayoutUpdated" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:VIBlend.WinForms.Controls.vTreeViewLayoutEventArgs" /> instance containing the event data.</param>
    protected virtual void OnLayoutUpdated(vTreeViewLayoutEventArgs args)
    {
      if (this.LayoutUpdated == null)
        return;
      this.LayoutUpdated((object) this, args);
    }

    /// <summary>Removes the node from the collection.</summary>
    public virtual void Remove()
    {
      vTreeNode parent = this.Parent;
      vTreeNodeCollection parentCollection = this.ParentCollection;
      vTreeView vTreeView = this.vTreeView;
      if (vTreeView != null)
        vTreeView.BeginUpdate();
      try
      {
        if (vTreeView != null)
        {
          vTreeView.OnRemoveNode(this);
          this.vTreeView = (vTreeView) null;
        }
        parentCollection.Remove(this);
        if (vTreeView.SelectedNode != this)
          return;
        vTreeView.SelectedNode = this.Parent != null ? this.Parent : (vTreeNode) null;
      }
      finally
      {
        if (vTreeView != null)
          vTreeView.EndUpdate();
      }
    }

    /// <summary>Removes all child nodes of the node.</summary>
    public virtual void RemoveChildren()
    {
      vTreeView vTreeView = this.vTreeView;
      if (vTreeView != null)
        vTreeView.BeginUpdate();
      try
      {
        foreach (vTreeNode vTreeNode in new List<vTreeNode>((IEnumerable<vTreeNode>) this.Nodes))
          vTreeNode.Remove();
      }
      finally
      {
        if (vTreeView != null)
          vTreeView.EndUpdate();
      }
    }

    /// <summary>Toggles the expanded/collapsed state of the node.</summary>
    public void Toggle()
    {
      if (this.IsExpanded)
        this.Collapse();
      else
        this.Expand();
      if (this.vTreeView == null)
        return;
      this.vTreeView.BringIntoView(this);
    }

    private vTreeNode CloneInternal(vTreeNode node)
    {
      vTreeNode vTreeNode = new vTreeNode();
      vTreeNode.text = node.text;
      vTreeNode.showCheckBox = node.showCheckBox;
      vTreeNode.selected = node.selected;
      vTreeNode.selectedImageIndex = node.selectedImageIndex;
      vTreeNode.stateImageIndex = node.stateImageIndex;
      vTreeNode.visible = node.visible;
      vTreeNode.Tag = node.Tag;
      vTreeNode.ImageIndex = node.ImageIndex;
      vTreeNode.TooltipText = node.TooltipText;
      vTreeNode.Font = node.Font;
      vTreeNode.ForeColor = node.ForeColor;
      vTreeNode.DescriptionText = node.DescriptionText;
      vTreeNode.DescriptionTextAlignment = node.DescriptionTextAlignment;
      vTreeNode.DescriptionTextColor = node.DescriptionTextColor;
      vTreeNode.DescriptionTextFont = node.DescriptionTextFont;
      vTreeNode.Checked = node.Checked;
      vTreeNode.TextAlignment = node.TextAlignment;
      foreach (vTreeNode node1 in node.Nodes)
      {
        vTreeNode node2 = this.CloneInternal(node1);
        vTreeNode.Nodes.Add(node2);
      }
      return vTreeNode;
    }

    /// <exclude />
    public object Clone()
    {
      return (object) this.CloneInternal(this);
    }

    public void Dispose()
    {
      this.Dispose(true);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing || this.Disposed == null)
        return;
      this.Disposed((object) this, EventArgs.Empty);
    }

    /// <summary>Called when a property is changed.</summary>
    /// <param name="propertyName">Name of the property.</param>
    protected virtual void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
