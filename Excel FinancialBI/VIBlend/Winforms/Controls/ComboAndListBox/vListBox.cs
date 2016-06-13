// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vListBox
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
using System.Globalization;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vListBox control</summary>
  /// <remarks>
  /// A vListBox displays a collection of items. The control allows you to use images, and detailed description for each item.
  /// </remarks>
  [Description("Displays a collection of items. The control allows you to use images, and detailed description for each item.")]
  [ToolboxBitmap(typeof (vListBox), "ControlIcons.vListBox.ico")]
  [Designer("VIBlend.WinForms.Controls.Design.vListControlDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxItem(true)]
  public class vListBox : Control, IScrollableControlBase
  {
    private Timer draggingTimer = new Timer();
    private int itemHeight = 17;
    internal vVScrollBar vscroll = new vVScrollBar();
    internal vHScrollBar hscroll = new vHScrollBar();
    protected Rectangle displayBounds = new Rectangle(0, 0, 0, 0);
    private bool hotTrack = true;
    private Color backColor = Color.White;
    private bool useBorderColor = true;
    private Color controlBorderColor = Color.Black;
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private VIBLEND_THEME defaultScrollBarsTheme = VIBLEND_THEME.VISTABLUE;
    private int itemsSpacing = 2;
    private bool allowSelection = true;
    private SelectionMode selectionMode = SelectionMode.One;
    private byte roundedCornersMaskListItem = 15;
    private int roundedCornersRadiusListItem = 1;
    private int lastSelectedIndexWithKeyboard = -1;
    protected bool isScrollBarUpdateRequired = true;
    private int lastSelectedIndex = -1;
    internal int hotTrackIndex = -1;
    private PaintHelper paintHelper = new PaintHelper();
    private Color dropFeedbackColor = Color.Blue;
    private static System.Type stringType = typeof (string);
    protected const int margin = 2;
    private ListItemsCollection selectedItems;
    private ListItemsCollection items;
    private List<int> selectedIndices;
    private bool inSetDataConnection;
    private bool isDataSourceInitEventHooked;
    private bool isDataSourceInitialized;
    private bool allowDragDrop;
    private ListItem dragSourceItem;
    private Form dragDropForm;
    private ImageList imageList;
    private BindingMemberInfo valueMember;
    private bool useThemeBackColor;
    private bool smartScrollEnabled;
    protected BackgroundElement backFill;
    protected ControlTheme theme;
    private object dataSource;
    private BindingMemberInfo displayMember;
    private bool allowMultipleSelection;
    private CurrencyManager currencyManager;
    private bool stopEnsureVisible;
    private bool isItemDropping;
    private Graphics grfx;
    private bool canDrag;
    private Point mouseDownPoint;
    private Point mouseUpPoint;
    private bool isDragging;
    private int lastHotTrackIndex;
    private bool shouldDropUp;
    private bool isUpdating;
    private static int cachedDropFeedbackY;
    private AnimationManager animationManager;
    private bool allowAnimations;

    /// <summary>
    /// Gets or sets whether the Drag drop operation is enabled
    /// </summary>
    [DefaultValue(false)]
    [Description("Gets or sets whether the Drag drop operation is enabled.")]
    [Category("Behavior")]
    public bool AllowDragDrop
    {
      get
      {
        return this.allowDragDrop;
      }
      set
      {
        if (value == this.allowDragDrop)
          return;
        this.allowDragDrop = value;
      }
    }

    /// <summary>
    /// Gets or sets whether the items change in appearance when the mouse passes over them
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets whether the items change in appearance when the mouse passes over them.")]
    [DefaultValue(true)]
    public bool HotTrack
    {
      get
      {
        return this.hotTrack;
      }
      set
      {
        if (value == this.hotTrack)
          return;
        this.hotTrack = value;
      }
    }

    /// <summary>
    /// Gets or sets whether to use the Theme BackColor or the control's BackColor property.
    /// </summary>
    [DefaultValue(false)]
    [Description("Gets or sets whether to use the Theme BackColor or the control's BackColor property")]
    [Category("Appearance")]
    [Browsable(true)]
    public bool UseThemeBackColor
    {
      get
      {
        return this.useThemeBackColor;
      }
      set
      {
        this.useThemeBackColor = value;
        this.backFill.LoadTheme(this.theme);
        this.Invalidate();
      }
    }

    /// <summary>Determines whether to use Smart scrolling</summary>
    [DefaultValue(false)]
    [Browsable(true)]
    [Category("Behavior")]
    [Description("Determines whether to use Smart scrolling")]
    public bool SmartScrollEnabled
    {
      get
      {
        return this.smartScrollEnabled;
      }
      set
      {
        this.smartScrollEnabled = value;
        this.hscroll.SmartScrollEnabled = this.vscroll.SmartScrollEnabled = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the BackColor property for the control</summary>
    [Browsable(true)]
    [Category("Appearance")]
    [Description("Gets or sets the BackColor property for the control")]
    [DefaultValue(typeof (Color), "White")]
    public override Color BackColor
    {
      get
      {
        return this.backColor;
      }
      set
      {
        this.backColor = value;
        base.BackColor = Color.Transparent;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the BackgroundImage for the control</summary>
    [Category("Appearance")]
    [Browsable(false)]
    public override Image BackgroundImage
    {
      get
      {
        return base.BackgroundImage;
      }
      set
      {
        base.BackgroundImage = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use the BorderColor
    /// </summary>
    [Description("Gets or sets a value indicating whether to use the BorderColor")]
    [Category("Behavior")]
    [DefaultValue(true)]
    public virtual bool UseThemeBorderColor
    {
      get
      {
        return this.useBorderColor;
      }
      set
      {
        this.useBorderColor = value;
        this.Refresh();
      }
    }

    /// <summary>Gets or sets the BorderColor of the ListBox.</summary>
    [Description("Gets or sets the BorderColor of the ListBox.")]
    [DefaultValue(typeof (Color), "Black")]
    [Category("Appearance")]
    public virtual Color BorderColor
    {
      get
      {
        return this.controlBorderColor;
      }
      set
      {
        if (!(value != this.BorderColor))
          return;
        this.controlBorderColor = value;
        this.Refresh();
      }
    }

    /// <summary>Gets or sets the Theme of the control.</summary>
    [Description("Gets or sets the Theme of the control.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Appearance")]
    [Browsable(false)]
    public ControlTheme Theme
    {
      get
      {
        return this.theme;
      }
      set
      {
        if (value == null || value == this.theme)
          return;
        this.theme = value;
        if (this.backFill != null)
          this.backFill.LoadTheme(this.theme);
        if (this.hscroll != null)
        {
          this.hscroll.Theme = this.theme;
          this.hscroll.ScrollButtonsRoundedCornersRadius = (int) this.theme.Radius;
        }
        if (this.vscroll != null)
        {
          this.vscroll.Theme = this.theme;
          this.vscroll.ScrollButtonsRoundedCornersRadius = (int) this.theme.Radius;
        }
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the theme of the control to one of the built-in themes.
    /// </summary>
    [Category("Appearance")]
    [Description("Gets or sets the theme of the control to one of the built-in themes.")]
    public VIBLEND_THEME VIBlendTheme
    {
      get
      {
        return this.defaultTheme;
      }
      set
      {
        this.defaultTheme = value;
        this.VIBlendScrollBarsTheme = value;
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
    /// Gets or sets the theme of the treeview scrollbars using one of the built-in themes.
    /// </summary>
    [Browsable(false)]
    [Category("Appearance")]
    public VIBLEND_THEME VIBlendScrollBarsTheme
    {
      get
      {
        return this.defaultScrollBarsTheme;
      }
      set
      {
        this.defaultScrollBarsTheme = value;
        if (this.hscroll == null || this.vscroll == null)
          return;
        this.hscroll.VIBlendTheme = value;
        this.vscroll.VIBlendTheme = value;
      }
    }

    /// <summary>Gets or sets the height of the vListBox items.</summary>
    [DefaultValue(17)]
    [Category("Behavior")]
    [Description("Gets or sets the height of the vListBox items.")]
    [Localizable(true)]
    [RefreshProperties(RefreshProperties.Repaint)]
    public int ItemHeight
    {
      get
      {
        return this.itemHeight;
      }
      set
      {
        this.itemHeight = value;
        this.isScrollBarUpdateRequired = true;
        this.Invalidate();
      }
    }

    [Category("Behavior")]
    [DefaultValue(2)]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Localizable(true)]
    [Description("Gets or sets spacing in pixels between the vListBox items.")]
    public int ItemsSpacing
    {
      get
      {
        return this.itemsSpacing;
      }
      set
      {
        this.itemsSpacing = value >= 0 ? value : 0;
        this.Refresh();
      }
    }

    /// <summary>Gets the vListBox items collection.</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Data")]
    [Description("Gets the vListBox items collection.")]
    public ListItemsCollection Items
    {
      get
      {
        return this.items;
      }
    }

    /// <summary>Gets the collection of selected indices.</summary>
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets the collection of selected indices.")]
    [Browsable(false)]
    public List<int> SelectedIndices
    {
      get
      {
        return this.selectedIndices;
      }
    }

    /// <summary>Gets the collection of selected items.</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Behavior")]
    [Description("Gets the collection of selected items.")]
    [Browsable(false)]
    public ListItemsCollection SelectedItems
    {
      get
      {
        return this.selectedItems;
      }
    }

    /// <summary>Gets or sets the selected item.</summary>
    [Category("Behavior")]
    [Bindable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets the selected item.")]
    [Browsable(false)]
    public ListItem SelectedItem
    {
      get
      {
        if (this.SelectedItems.Count == 0)
          return (ListItem) null;
        return this.SelectedItems[0];
      }
      set
      {
        if (!this.allowSelection)
          return;
        int num = this.Items.IndexOf(value);
        if (num == -1)
          return;
        if (value == null && this.SelectedItems.Count > 0)
        {
          ListItemChangingEventArgs args = new ListItemChangingEventArgs(value, this.SelectedItem);
          this.OnSelectedItemChanging(args);
          if (args.Cancel)
          {
            this.Invalidate();
          }
          else
          {
            this.SelectedItems.Clear();
            this.SelectedIndices.Clear();
            this.Invalidate();
            this.OnSelectedIndexChanged(EventArgs.Empty);
            this.OnSelectedItemChanged();
          }
        }
        else
        {
          if (this.SelectedItems.Count > 0 && this.SelectedItems.Contains(value))
            return;
          ListItemChangingEventArgs args = new ListItemChangingEventArgs(value, this.SelectedItem);
          this.OnSelectedItemChanging(args);
          if (args.Cancel)
          {
            this.Invalidate();
          }
          else
          {
            if (this.CanSelectMultipleItems())
            {
              this.SelectedItems.Add(value);
              this.SelectedIndices.Add(num);
            }
            else
            {
              this.SelectedItems.Clear();
              this.SelectedIndices.Clear();
              this.SelectedItems.Add(value);
              this.SelectedIndices.Add(num);
            }
            this.OnSelectedIndexChanged(EventArgs.Empty);
            this.OnSelectedItemChanged();
          }
        }
      }
    }

    /// <summary>Gets or sets the selected index.</summary>
    [Category("Behavior")]
    [DefaultValue(-1)]
    [Browsable(false)]
    [Description("Gets or sets the selected index.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Bindable(true)]
    public int SelectedIndex
    {
      get
      {
        if (this.SelectedIndices.Count > 0)
          return this.SelectedIndices[0];
        return -1;
      }
      set
      {
        if (!this.allowSelection || this.SelectionMode == SelectionMode.None)
          return;
        if (value == -1 && this.SelectedIndices.Count > 0)
        {
          ListItemChangingEventArgs args = new ListItemChangingEventArgs((ListItem) null, this.SelectedItem);
          this.OnSelectedItemChanging(args);
          if (args.Cancel)
          {
            this.Invalidate();
          }
          else
          {
            this.SelectedItems.Clear();
            this.SelectedIndices.Clear();
            this.OnSelectedIndexChanged(EventArgs.Empty);
            this.OnSelectedValueChanged(EventArgs.Empty);
            this.OnSelectedItemChanged();
            this.Invalidate();
          }
        }
        else
        {
          if (value <= -1 || value >= this.items.Count)
            return;
          bool flag1 = this.SelectedIndices.Count > 0 && this.SelectedIndices.Contains(value);
          this.lastSelectedIndexWithKeyboard = value;
          if (!flag1)
          {
            if (value < this.Items.Count)
            {
              ListItemChangingEventArgs args = new ListItemChangingEventArgs(this.Items[value], this.SelectedItem);
              this.OnSelectedItemChanging(args);
              if (args.Cancel)
              {
                this.Invalidate();
                return;
              }
            }
            bool flag2 = this.CanSelectMultipleItems();
            ListItem listItem = this.Items[value];
            if (flag2)
            {
              this.SelectedIndices.Add(value);
              this.SelectedItems.Add(listItem);
            }
            else
            {
              this.SelectedIndices.Clear();
              this.SelectedItems.Clear();
              this.SelectedIndices.Add(value);
              this.SelectedItems.Add(listItem);
            }
            this.EnsureVisible(this.SelectedIndex);
            this.Invalidate();
            this.OnSelectedIndexChanged(EventArgs.Empty);
            this.OnSelectedValueChanged(EventArgs.Empty);
            this.OnSelectedItemChanged();
          }
          else
          {
            ListItem newItem = this.Items[value];
            if (this.CanSelectMultipleItems())
            {
              ListItemChangingEventArgs args = new ListItemChangingEventArgs(newItem, this.SelectedItem);
              this.OnSelectedItemChanging(args);
              if (args.Cancel)
              {
                this.Invalidate();
                return;
              }
              this.SelectedIndices.Remove(value);
              this.SelectedItems.Remove(newItem);
              this.OnSelectedIndexChanged(EventArgs.Empty);
              this.OnSelectedValueChanged(EventArgs.Empty);
              this.OnSelectedItemChanged();
            }
            else if (this.SelectedIndices.Count > 1)
            {
              ListItemChangingEventArgs args = new ListItemChangingEventArgs(newItem, this.SelectedItem);
              this.OnSelectedItemChanging(args);
              if (args.Cancel)
              {
                this.Invalidate();
                return;
              }
              this.SelectedIndices.Clear();
              this.SelectedItems.Clear();
              this.SelectedIndices.Add(value);
              this.SelectedItems.Add(newItem);
              this.OnSelectedIndexChanged(EventArgs.Empty);
              this.OnSelectedValueChanged(EventArgs.Empty);
              this.OnSelectedItemChanged();
            }
            this.Invalidate();
          }
        }
      }
    }

    /// <summary>
    /// Gets or sets the display member associated to the vListBox.
    /// </summary>
    [Description("Gets or sets the display member associated to the vListBox.")]
    [DefaultValue("")]
    [Category("Behavior")]
    public string DisplayMember
    {
      get
      {
        return this.displayMember.BindingMember;
      }
      set
      {
        BindingMemberInfo bindingMemberInfo = this.displayMember;
        try
        {
          this.SetDataConnection(this.dataSource, new BindingMemberInfo(value), false);
        }
        catch
        {
          this.displayMember = bindingMemberInfo;
        }
      }
    }

    /// <summary>
    /// Gets a value indicating whether the vListBox currently enables selection of list items.
    /// </summary>
    [Browsable(false)]
    [DefaultValue(true)]
    [Category("Behavior")]
    [Obsolete("Use the SelectionMode property instead.")]
    public bool AllowSelection
    {
      get
      {
        return this.allowSelection;
      }
      set
      {
        if (value == this.allowSelection)
          return;
        this.allowSelection = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the multiple selection is enabled.
    /// </summary>
    [Obsolete("Use the SelectionMode property instead.")]
    [Description("Gets or sets a value indicating whether the multiple selection is enabled.")]
    [Category("Behavior")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DefaultValue(false)]
    public bool AllowMultipleSelection
    {
      get
      {
        return this.allowMultipleSelection;
      }
      set
      {
        if (!this.AllowSelection || value == this.allowMultipleSelection)
          return;
        this.allowMultipleSelection = value;
      }
    }

    /// <summary>Gets or sets the selection mode.</summary>
    /// <value>The selection mode.</value>
    [Category("Behavior")]
    [Description("Gets or sets the selection mode.")]
    public virtual SelectionMode SelectionMode
    {
      get
      {
        return this.selectionMode;
      }
      set
      {
        if (this.selectionMode == value)
          return;
        this.selectionMode = value;
      }
    }

    [DefaultValue(null)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Bindable(true)]
    [Category("Behavior")]
    [Description("Gets or sets the selected value")]
    public object SelectedValue
    {
      get
      {
        if (this.SelectedIndex != -1 && this.currencyManager != null)
          return this.FilterItemOnProperty(this.currencyManager.List[this.SelectedIndex], this.valueMember.BindingField);
        return (object) null;
      }
      set
      {
        if (this.currencyManager == null)
          return;
        string bindingField = this.valueMember.BindingField;
        if (string.IsNullOrEmpty(bindingField))
          throw new InvalidOperationException("Empty value member");
        this.SelectedIndex = this.Find(this.currencyManager.GetItemProperties().Find(bindingField, true), value, true);
      }
    }

    /// <summary>
    /// Gets or sets a string that specifies the property of the data source from which to draw the value.
    /// </summary>
    [Description("Gets or sets a string that specifies the property of the data source from which to draw the value.")]
    [DefaultValue("")]
    [Category("Behavior")]
    public string ValueMember
    {
      get
      {
        return this.valueMember.BindingMember;
      }
      set
      {
        if (value == null)
          value = "";
        BindingMemberInfo bindingMemberInfo = new BindingMemberInfo(value);
        if (bindingMemberInfo.Equals((object) this.valueMember))
          return;
        if (this.DisplayMember.Length == 0)
          this.SetDataConnection(this.DataSource, bindingMemberInfo, false);
        if (this.currencyManager != null && value != null && (value.Length != 0 && !this.BindingMemberInfoInDataManager(bindingMemberInfo)))
          throw new Exception("The Value member is wrong!");
        this.valueMember = bindingMemberInfo;
        this.OnSelectedValueChanged(EventArgs.Empty);
      }
    }

    /// <summary>
    /// Gets or sets the mask that determines which corners of a ListItem are rounded
    /// </summary>
    [Browsable(false)]
    public byte RoundedCornersMaskListItem
    {
      get
      {
        return this.roundedCornersMaskListItem;
      }
      set
      {
        this.roundedCornersMaskListItem = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the rounded corners radius of the ListItems.
    /// </summary>
    [DefaultValue(1)]
    [Description("Gets or sets the rounded corners radius of the ListItems.")]
    [Category("Behavior")]
    public int RoundedCornersRadiusListItem
    {
      get
      {
        return this.roundedCornersRadiusListItem;
      }
      set
      {
        if (value < 0)
          return;
        this.roundedCornersRadiusListItem = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the data source for the vListBox control</summary>
    [DefaultValue(null)]
    [AttributeProvider(typeof (IListSource))]
    [RefreshProperties(RefreshProperties.Repaint)]
    public object DataSource
    {
      get
      {
        return this.dataSource;
      }
      set
      {
        if (value != null && !(value is IList) && !(value is IListSource))
          throw new Exception("Invalid DataSource value is set.");
        if (this.dataSource == value)
          return;
        try
        {
          this.SetDataConnection(value, this.displayMember, false);
        }
        catch
        {
          this.DisplayMember = "";
        }
        if (value != null)
          return;
        this.DisplayMember = "";
      }
    }

    /// <summary>
    /// Gets or sets the images to display on the vListBox items.
    /// </summary>
    [Description("Gets or sets the images to display on the vListBox items")]
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
        this.imageList = value;
        this.Invalidate();
      }
    }

    public bool IsUpdating
    {
      get
      {
        return this.isUpdating;
      }
    }

    /// <summary>Gets or sets the color of the drop feedback.</summary>
    /// <value>The color of the drop feedback.</value>
    [DefaultValue(typeof (Color), "Blue")]
    [Category("Appearance")]
    [Description("Gets or sets the color of the drop feedback.")]
    public Color DropFeedbackColor
    {
      get
      {
        return this.dropFeedbackColor;
      }
      set
      {
        this.dropFeedbackColor = value;
      }
    }

    /// <summary>
    /// Determines whether animations are enabled for the control
    /// </summary>
    [Browsable(false)]
    [DefaultValue(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
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

    /// <exclude />
    [Browsable(false)]
    [Description("Gets an instance of Animation Manager")]
    public AnimationManager AnimationManager
    {
      get
      {
        if (this.animationManager == null)
          this.animationManager = new AnimationManager((Control) this);
        return this.animationManager;
      }
    }

    /// <summary>
    /// Occurs when the control's DataSource property is set and before the item is added to the Items collection. You can use this event to format the item's string.
    /// </summary>
    [Category("Action")]
    [Description("Occurs when the control's DataSource property is set and before the item is added to the Items collection. You can use this event to format the item's string.")]
    public event EventHandler<ListItemFormatEventArgs> ItemFormat;

    /// <summary>Occurs when the mouse is clicked over a ListItem.</summary>
    [Description("Occurs when the mouse is clicked over a ListItem.")]
    [Category("Action")]
    public event EventHandler<ListItemMouseEventArgs> ItemMouseDown;

    /// <summary>Occurs when the mouse is clicked over a ListItem.</summary>
    [Category("Action")]
    [Description("Occurs when the mouse is clicked over a ListItem.")]
    public event EventHandler<ListItemMouseEventArgs> ItemMouseUp;

    /// <summary>Occurs when item's drag operation has started.</summary>
    [Category("Action")]
    public event EventHandler<ListItemDragEventArgs> ItemDragStarted;

    /// <summary>Occurs when the drag item changes its location.</summary>
    [Category("Action")]
    public event EventHandler<ListItemDragEventArgs> ItemDragging;

    /// <summary>Occurs when item's drag operation is starting.</summary>
    [Category("Action")]
    public event EventHandler<ListItemDragCancelEventArgs> ItemDragStarting;

    /// <summary>Occurs when item's drag operation has ended.</summary>
    [Category("Action")]
    public event EventHandler<ListItemDragEventArgs> ItemDragEnded;

    /// <summary>Occurs when item's drag operation is ending.</summary>
    [Category("Action")]
    public event EventHandler<ListItemDragCancelEventArgs> ItemDragEnding;

    /// <summary>Occurs when the selected index changes</summary>
    public event EventHandler SelectedIndexChanged;

    /// <summary>occurs when the selection is changed.</summary>
    [Description("Occurs when the selection is changed.")]
    [Category("Action")]
    public event EventHandler SelectedItemChanged;

    /// <summary>Occurs when the selection is changing.</summary>
    [Category("Action")]
    [Description("Occurs when the selection is changing.")]
    public event EventHandler<ListItemChangingEventArgs> SelectedItemChanging;

    /// <summary>Occurs Data Source is changed.</summary>
    [Description("Occurs Data Source is changed.")]
    [Category("Action")]
    public event EventHandler DataSourceChanged;

    /// <summary>Occurs when the SelectedValue has changed.</summary>
    [Category("Action")]
    [Description("Occurs when the SelectedValue has changed.")]
    public event EventHandler SelectedValueChanged;

    /// <summary>Occurs when an item is being painted.</summary>
    [Category("Action")]
    [Description("Occurs when an item is being painted.")]
    public event DrawItemEventHandler DrawListItem;

    /// <summary>Occurs when the DisplayMember has been changed.</summary>
    [Category("Action")]
    [Description("Occurs when the DisplayMember has been changed")]
    public event EventHandler DisplayMemberChanged;

    static vListBox()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>Constructor</summary>
    public vListBox()
    {
      this.isScrollBarUpdateRequired = false;
      this.SetStyle(ControlStyles.ResizeRedraw, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.SetStyle(ControlStyles.Opaque, false);
      base.BackColor = Color.Transparent;
      this.BackColor = Color.White;
      this.Controls.Add((Control) this.vscroll);
      this.Controls.Add((Control) this.hscroll);
      this.vscroll.ValueChanged += new EventHandler(this.vscroll_ValueChanged);
      this.hscroll.ValueChanged += new EventHandler(this.hscroll_ValueChanged);
      this.backFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.selectedItems = new ListItemsCollection();
      this.items = new ListItemsCollection();
      this.items.CollectionChanged += new EventHandler(this.items_CollectionChanged);
      this.selectedIndices = new List<int>();
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
      this.SizeChanged += new EventHandler(this.ListBoxControl_SizeChanged);
      this.Layout += new LayoutEventHandler(this.vListBox_Layout);
      this.SmartScrollEnabled = false;
      this.isScrollBarUpdateRequired = true;
      this.draggingTimer.Interval = 300;
      this.draggingTimer.Tick += new EventHandler(this.draggingTimer_Tick);
    }

    /// <summary>
    /// Raises the <see cref="E:ItemFormat" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:VIBlend.WinForms.Controls.ListItemFormatEventArgs" /> instance containing the event data.</param>
    protected virtual void OnItemFormat(ListItemFormatEventArgs args)
    {
      if (this.ItemFormat == null)
        return;
      this.ItemFormat((object) this, args);
    }

    /// <summary>
    /// Raises the <see cref="E:ItemMouseDown" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:VIBlend.WinForms.Controls.ListItemMouseEventArgs" /> instance containing the event data.</param>
    protected virtual void OnItemMouseDown(ListItemMouseEventArgs args)
    {
      if (this.ItemMouseDown == null)
        return;
      this.ItemMouseDown((object) this, args);
    }

    /// <summary>
    /// Raises the <see cref="E:ItemMouseUp" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:VIBlend.WinForms.Controls.ListItemMouseEventArgs" /> instance containing the event data.</param>
    protected virtual void OnItemMouseUp(ListItemMouseEventArgs args)
    {
      if (this.ItemMouseUp == null)
        return;
      this.ItemMouseUp((object) this, args);
    }

    private void dragDropForm_Paint(object sender, PaintEventArgs e)
    {
      if (this.dragSourceItem == null)
        return;
      int x = 0;
      int y = 0;
      ControlTheme theme = this.Theme;
      Size size = new Size(this.Width, this.ItemHeight);
      Rectangle bounds = new Rectangle(x, y, size.Width, size.Height);
      this.DrawItem(e.Graphics, bounds, DrawItemState.Default, this.dragSourceItem);
      Rectangle rectangle = new Rectangle(size.Width - 10, size.Height - 7, 5, 3);
    }

    private Form GetDragDropForm()
    {
      if (this.dragDropForm == null)
      {
        this.dragDropForm = new Form();
        this.dragDropForm.ShowInTaskbar = false;
        this.dragDropForm.FormBorderStyle = FormBorderStyle.None;
        this.dragDropForm.Opacity = 0.01;
        this.dragDropForm.TopMost = true;
        this.dragDropForm.Paint += new PaintEventHandler(this.dragDropForm_Paint);
        this.dragDropForm.MinimumSize = new Size(1, 1);
        this.dragDropForm.Size = new Size(this.Width, this.ItemHeight);
        this.dragDropForm.Shown += new EventHandler(this.dragDropForm_Shown);
      }
      return this.dragDropForm;
    }

    private void dragDropForm_Shown(object sender, EventArgs e)
    {
      if (this.dragDropForm == null)
        return;
      this.dragDropForm.Opacity = 0.7;
      this.dragDropForm.Shown -= new EventHandler(this.dragDropForm_Shown);
    }

    protected virtual void OnItemDragStarted(ListItemDragEventArgs args)
    {
      if (this.ItemDragStarted == null)
        return;
      this.ItemDragStarted((object) this, args);
    }

    protected virtual void OnItemDragging(ListItemDragEventArgs args)
    {
      if (this.ItemDragging == null)
        return;
      this.ItemDragging((object) this, args);
    }

    protected virtual void OnItemDragStarting(ListItemDragCancelEventArgs args)
    {
      if (this.ItemDragStarting == null)
        return;
      this.ItemDragStarting((object) this, args);
    }

    protected internal virtual void OnItemDragEnded(ListItemDragEventArgs args)
    {
      if (this.ItemDragEnded == null)
        return;
      this.ItemDragEnded((object) this, args);
    }

    protected virtual void OnItemDragEnding(ListItemDragCancelEventArgs args)
    {
      if (this.ItemDragEnding == null)
        return;
      this.ItemDragEnding((object) this, args);
    }

    /// <summary>Starts the dragging timer.</summary>
    public void StartDraggingTimer()
    {
      this.draggingTimer.Start();
    }

    /// <summary>Stops the dragging timer.</summary>
    public void StopDraggingTimer()
    {
      this.draggingTimer.Stop();
    }

    /// <summary>Begins the drag.</summary>
    /// <param name="drag">The drag.</param>
    protected virtual void BeginDrag(ListItem drag)
    {
      this.draggingTimer.Start();
      ListItemDragCancelEventArgs args = new ListItemDragCancelEventArgs(this.dragSourceItem, (ListItem) null);
      this.OnItemDragStarting(args);
      if (args.Cancel)
        return;
      Point client = this.PointToClient(Cursor.Position);
      this.isDragging = true;
      this.dragSourceItem = this.FindItem(client);
      this.dragDropForm = this.GetDragDropForm();
      if (this.dragDropForm != null)
      {
        this.dragDropForm.Show();
        this.dragDropForm.Location = Cursor.Position;
      }
      this.OnItemDragStarted(new ListItemDragEventArgs(this.dragSourceItem, (ListItem) null));
    }

    /// <summary>Ends the drag.</summary>
    /// <param name="dragItem">The drag item.</param>
    /// <param name="targetItem">The target item.</param>
    protected internal virtual void EndDrag(ListItem dragItem, ListItem targetItem)
    {
      this.draggingTimer.Stop();
      this.isDragging = false;
      this.isItemDropping = true;
      this.mouseDownPoint = Point.Empty;
      if (dragItem == targetItem)
      {
        this.OnItemDragEnded(new ListItemDragEventArgs(this.dragSourceItem, targetItem));
        if (this.dragDropForm != null)
        {
          this.dragDropForm.Hide();
          this.dragDropForm = (Form) null;
        }
        this.isItemDropping = false;
      }
      else
      {
        if (this.dragDropForm != null)
        {
          this.dragDropForm.Hide();
          this.dragDropForm = (Form) null;
        }
        ListItemDragCancelEventArgs args = new ListItemDragCancelEventArgs(this.dragSourceItem, targetItem);
        this.OnItemDragEnding(args);
        if (args.Cancel)
          return;
        if (targetItem == null)
        {
          this.isItemDropping = false;
        }
        else
        {
          Point client = this.PointToClient(Cursor.Position);
          ListItem listItem = this.FindItem(client);
          bool flag = false;
          if (listItem != null)
          {
            if (listItem == this.dragSourceItem)
            {
              this.isItemDropping = false;
              return;
            }
            flag = new Rectangle(listItem.RenderBounds.X, listItem.RenderBounds.Y + this.ItemHeight / 2, listItem.RenderBounds.Width, this.ItemHeight / 2).Contains(client);
          }
          int index = this.Items.IndexOf(targetItem);
          int num = this.Items.IndexOf(this.dragSourceItem);
          this.stopEnsureVisible = true;
          if (this.dragSourceItem != null)
          {
            this.Items.Remove(dragItem);
            if (num < index)
              --index;
            if (!flag)
              this.Items.Insert(this.dragSourceItem, index);
            else if (index + 1 < this.Items.Count)
              this.Items.Insert(this.dragSourceItem, index + 1);
            else
              this.Items.Add(this.dragSourceItem);
          }
          this.isScrollBarUpdateRequired = false;
          this.SelectedItems.Clear();
          this.SelectedItem = this.dragSourceItem;
          this.dragSourceItem = (ListItem) null;
          this.Invalidate();
          this.stopEnsureVisible = false;
          this.isItemDropping = false;
          this.OnItemDragEnded(new ListItemDragEventArgs(this.dragSourceItem, targetItem));
        }
      }
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
    protected override void OnKeyDown(KeyEventArgs e)
    {
      if (this.isDragging && e.KeyCode == Keys.Escape)
        this.CancelDrag();
      base.OnKeyDown(e);
    }

    private void CancelDrag()
    {
      this.Capture = false;
      this.canDrag = false;
      this.EndDrag((ListItem) null, (ListItem) null);
      this.dragSourceItem = (ListItem) null;
    }

    /// <summary>Drops the item.</summary>
    /// <param name="sourceListBox">The source list box.</param>
    /// <param name="targetListBox">The target list box.</param>
    /// <param name="sourceItem">The source item.</param>
    /// <param name="dropType">Type of the drop.</param>
    public static void DropItem(vListBox sourceListBox, vListBox targetListBox, ListItem sourceItem, ListBoxDropType dropType)
    {
      if (sourceItem == null || targetListBox == null || (sourceListBox == null || sourceListBox == targetListBox))
        return;
      Point client = targetListBox.PointToClient(Cursor.Position);
      ListItem listItem1 = targetListBox.FindItem(client);
      bool flag = false;
      if (listItem1 != null)
      {
        if (listItem1 == sourceItem)
        {
          targetListBox.isItemDropping = false;
          return;
        }
        flag = new Rectangle(listItem1.RenderBounds.X, listItem1.RenderBounds.Y + targetListBox.ItemHeight / 2, listItem1.RenderBounds.Width, targetListBox.ItemHeight / 2).Contains(client);
      }
      int index = targetListBox.Items.IndexOf(listItem1);
      targetListBox.Items.IndexOf(sourceItem);
      targetListBox.stopEnsureVisible = true;
      int num = targetListBox.vscroll.Value;
      if (targetListBox.Items.Count == 0 || listItem1 == null && targetListBox.Items.Count > 0 && (targetListBox.Items[targetListBox.Items.Count - 1].RenderBounds.Bottom < client.Y && client.Y < targetListBox.Height))
      {
        Rectangle screen = targetListBox.RectangleToScreen(targetListBox.ClientRectangle);
        switch (dropType)
        {
          case ListBoxDropType.Default:
            sourceListBox.Items.Remove(sourceItem);
            if (screen.Contains(Cursor.Position))
            {
              targetListBox.Items.Add(sourceItem);
              break;
            }
            break;
          case ListBoxDropType.Clone:
            if (screen.Contains(Cursor.Position))
            {
              ListItem listItem2 = sourceItem.Clone();
              targetListBox.Items.Add(listItem2);
              break;
            }
            break;
          case ListBoxDropType.Remove:
            sourceListBox.Items.Remove(sourceItem);
            break;
        }
      }
      else if (listItem1 != null)
      {
        switch (dropType)
        {
          case ListBoxDropType.Default:
            sourceListBox.Items.Remove(sourceItem);
            if (!flag)
            {
              targetListBox.Items.Insert(sourceItem, index);
              break;
            }
            if (index + 1 < targetListBox.Items.Count)
            {
              targetListBox.Items.Insert(sourceItem, index + 1);
              break;
            }
            targetListBox.Items.Add(sourceItem);
            break;
          case ListBoxDropType.Clone:
            sourceItem = sourceItem.Clone();
            if (!flag)
            {
              targetListBox.Items.Insert(sourceItem, index);
              break;
            }
            if (index + 1 < targetListBox.Items.Count)
            {
              targetListBox.Items.Insert(sourceItem, index + 1);
              break;
            }
            targetListBox.Items.Add(sourceItem);
            break;
          case ListBoxDropType.Remove:
            sourceListBox.Items.Remove(sourceItem);
            break;
        }
      }
      targetListBox.isScrollBarUpdateRequired = false;
      targetListBox.SelectedItems.Clear();
      targetListBox.SelectedItem = sourceItem;
      targetListBox.dragSourceItem = (ListItem) null;
      targetListBox.Invalidate();
      targetListBox.stopEnsureVisible = false;
      targetListBox.isItemDropping = false;
      targetListBox.isScrollBarUpdateRequired = true;
      sourceListBox.isScrollBarUpdateRequired = true;
      targetListBox.InitializeScrollBars();
      sourceListBox.InitializeScrollBars();
      if (targetListBox.vscroll.Maximum > num && num >= targetListBox.vscroll.Minimum)
        targetListBox.vscroll.Value = num;
      sourceListBox.OnItemDragEnded(new ListItemDragEventArgs((ListItem) null, (ListItem) null));
    }

    /// <summary>Drops the item.</summary>
    /// <param name="sourceListBox">The source list box.</param>
    /// <param name="targetListBox">The target list box.</param>
    /// <param name="sourceItem">The source item.</param>
    public static void DropItem(vListBox sourceListBox, vListBox targetListBox, ListItem sourceItem)
    {
      vListBox.DropItem(sourceListBox, targetListBox, sourceItem, ListBoxDropType.Default);
    }

    protected internal virtual void OnSelectedItemChanged()
    {
      if (this.SelectedItemChanged == null)
        return;
      this.SelectedItemChanged((object) this, EventArgs.Empty);
    }

    protected internal virtual void OnSelectedItemChanging(ListItemChangingEventArgs args)
    {
      if (this.SelectedItemChanging == null)
        return;
      this.SelectedItemChanging((object) this, args);
    }

    /// <summary>Raises the SelectedValueChanged event.</summary>
    /// <param name="e">An EventArgs that contains the event data. </param>
    protected virtual void OnSelectedValueChanged(EventArgs e)
    {
      if (this.SelectedValueChanged == null)
        return;
      this.SelectedValueChanged((object) this, e);
    }

    private void UpdateDisplayBounds()
    {
      this.displayBounds = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
      this.displayBounds.Inflate(0, 0);
      this.vscroll.Visible = false;
      this.hscroll.Visible = false;
      this.isScrollBarUpdateRequired = true;
    }

    private void ListBoxControl_SizeChanged(object sender, EventArgs e)
    {
      this.UpdateDisplayBounds();
    }

    private void vListBox_Layout(object sender, LayoutEventArgs e)
    {
      this.UpdateDisplayBounds();
    }

    private void items_CollectionChanged(object sender, EventArgs e)
    {
      this.isScrollBarUpdateRequired = true;
      foreach (ListItem listItem in this.Items)
      {
        listItem.PropertyChanged -= new PropertyChangedEventHandler(this.item_PropertyChanged);
        listItem.PropertyChanged += new PropertyChangedEventHandler(this.item_PropertyChanged);
      }
      this.Invalidate();
    }

    private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.Invalidate();
    }

    private void hscroll_ValueChanged(object sender, EventArgs e)
    {
      this.Invalidate();
    }

    private void vscroll_ValueChanged(object sender, EventArgs e)
    {
      this.Invalidate();
    }

    /// <summary>
    /// Determines whether this instance can select multiple items.
    /// </summary>
    public bool CanSelectMultipleItems()
    {
      return this.SelectionMode == SelectionMode.MultiSimple || (this.IsShiftPressed() || Control.ModifierKeys == Keys.Control) && (this.SelectionMode == SelectionMode.MultiExtended || this.AllowMultipleSelection);
    }

    private bool IsShiftPressed()
    {
      return Control.ModifierKeys == Keys.Shift;
    }

    /// <summary>Raises the SelectedIndexChanged event</summary>
    /// <param name="e">An EventArgs that contains the event data. </param>
    protected virtual void OnSelectedIndexChanged(EventArgs e)
    {
      if (this.SelectedIndexChanged == null)
        return;
      this.SelectedIndexChanged((object) this, e);
    }

    /// <summary>Draws the list item back ground and border.</summary>
    /// <param name="graphics">The graphics.</param>
    /// <param name="bounds">The bounds.</param>
    /// <param name="drawState">State of the draw.</param>
    public virtual void DrawListItemBackGroundAndBorder(Graphics graphics, Rectangle bounds, ListItem listItem, DrawItemState drawState)
    {
      this.backFill.Radius = this.roundedCornersRadiusListItem;
      this.backFill.RoundedCornersBitmask = this.roundedCornersMaskListItem;
      bounds = new Rectangle(bounds.X + listItem.ItemLeftAndRightOffset, bounds.Y, bounds.Width - listItem.ItemLeftAndRightOffset * 2, bounds.Height);
      ControlState stateType = this.Enabled ? ControlState.Normal : ControlState.Disabled;
      if (!listItem.Enabled)
        stateType = ControlState.Disabled;
      else if (drawState == DrawItemState.HotLight || drawState == DrawItemState.Focus)
        stateType = this.Enabled ? ControlState.Hover : ControlState.Disabled;
      else if (drawState == DrawItemState.Selected)
        stateType = this.Enabled ? ControlState.Pressed : ControlState.DisabledPressed;
      else if (drawState == DrawItemState.Disabled)
        stateType = ControlState.Disabled;
      if (!listItem.UseThemeBackground)
      {
        Brush brush = listItem.BackgroundBrush;
        Color backgroundBorder = listItem.BackgroundBorder;
        switch (stateType)
        {
          case ControlState.Hover:
            brush = listItem.HighlightBackgroundBrush;
            backgroundBorder = listItem.HighlightBackgroundBorder;
            break;
          case ControlState.Pressed:
            brush = listItem.SelectedBackgroundBrush;
            backgroundBorder = listItem.SelectedBackgroundBorder;
            break;
          case ControlState.Disabled:
            brush = listItem.DisabledBackgroundBrush;
            backgroundBorder = listItem.DisabledBackgroundBorder;
            break;
        }
        if (brush == null)
          brush = (Brush) new SolidBrush(this.backFill.Theme.StyleNormal.FillStyle.Colors[0]);
        GraphicsPath partiallyRoundedPath = this.paintHelper.CreatePartiallyRoundedPath(bounds, listItem.RoundedCornersRadius, listItem.RoundedCornersMask);
        graphics.FillPath(brush, partiallyRoundedPath);
        this.backFill.Bounds = new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height - 1);
        this.backFill.DrawElementBorder(graphics, partiallyRoundedPath, backgroundBorder);
      }
      else
      {
        if (stateType != ControlState.Focused && stateType != ControlState.DisabledPressed && (stateType != ControlState.Pressed && stateType != ControlState.Hover))
          return;
        this.backFill.Bounds = new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height);
        this.backFill.DrawElementFill(graphics, stateType);
        this.backFill.Bounds = new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height - 1);
        this.backFill.DrawElementBorder(graphics, stateType);
        Rectangle bounds1 = this.backFill.Bounds;
        bounds1.Inflate(-1, -1);
        Color color = Color.Empty;
        if (stateType == ControlState.Normal)
          color = this.Theme.QueryColorSetter("ButtonNormalInnerBorderColor");
        else if (stateType == ControlState.Hover)
          color = this.Theme.QueryColorSetter("ButtonHighlightInnerBorderColor");
        else if (stateType == ControlState.Pressed)
          color = this.Theme.QueryColorSetter("ButtonPressedInnerBorderColor");
        if ((int) color.A <= 0)
          return;
        this.backFill.Bounds = bounds1;
        this.backFill.DrawElementBorder(graphics, stateType, color);
      }
    }

    protected virtual bool OnDrawListItem(DrawItemEventArgs args)
    {
      if (this.DrawListItem == null)
        return false;
      this.DrawListItem((object) this, args);
      return true;
    }

    /// <summary>Raises the DrawItem event.</summary>
    /// <param name="e">A DrawItemEventArgs that contains the event data.</param>
    protected internal void OnDrawItem(DrawItemEventArgs e)
    {
      Graphics graphics = e.Graphics;
      Rectangle bounds = e.Bounds;
      ListItem listItem = e.Index < 0 || e.Index >= this.items.Count ? (ListItem) null : this.items[e.Index];
      if (listItem == null)
        return;
      int num = (int) e.State;
      this.DrawItem(e.Graphics, bounds, e.State, listItem);
    }

    public virtual void DrawItem(Graphics graphics, Rectangle bounds, DrawItemState state, ListItem listItem)
    {
      this.DrawListItemBackGroundAndBorder(graphics, bounds, listItem, state);
      if (listItem != null)
        listItem.RenderBounds = bounds;
      int num = this.DrawItemImage(graphics, bounds, listItem, state);
      bool flag = listItem.ImageBeforeText && this.RightToLeft != RightToLeft.Yes || !listItem.ImageBeforeText && this.RightToLeft == RightToLeft.Yes;
      if (num > 0)
      {
        if (flag)
        {
          bounds.X += num + 2;
          bounds.Width -= num + 2;
        }
        else
          bounds.Width -= num + 2;
      }
      bounds.Y += 2;
      bounds.Height -= 4;
      this.DrawListItemTextAndDescription(graphics, bounds, listItem, state);
    }

    /// <summary>Gets the item image.</summary>
    /// <param name="listItem">The list item.</param>
    /// <param name="state">The state.</param>
    /// <returns></returns>
    public Image GetItemImage(ListItem listItem, DrawItemState state)
    {
      int index;
      switch (state)
      {
        case DrawItemState.Selected:
          index = listItem.ImageIndexSelected != -1 ? listItem.ImageIndexSelected : listItem.ImageIndex;
          break;
        case DrawItemState.HotLight:
          index = listItem.ImageIndexHover != -1 ? listItem.ImageIndexHover : listItem.ImageIndex;
          break;
        default:
          index = listItem.ImageIndex;
          break;
      }
      if (this.imageList != null && index >= 0 && index < this.imageList.Images.Count)
        return this.imageList.Images[index];
      return (Image) null;
    }

    /// <summary>Draws the item image.</summary>
    /// <param name="graphics">The graphics.</param>
    /// <param name="bounds">The bounds.</param>
    /// <param name="listItem">The list item.</param>
    /// <param name="state">The state.</param>
    /// <returns></returns>
    public int DrawItemImage(Graphics graphics, Rectangle bounds, ListItem listItem, DrawItemState state)
    {
      int num = 0;
      Image itemImage = this.GetItemImage(listItem, state);
      if (itemImage != null)
      {
        num = this.imageList.ImageSize.Width;
        bool flag = listItem.ImageBeforeText && this.RightToLeft != RightToLeft.Yes || !listItem.ImageBeforeText && this.RightToLeft == RightToLeft.Yes;
        Rectangle rectangle = new Rectangle(bounds.X + 1, bounds.Y + (bounds.Height - this.imageList.ImageSize.Height) / 2, this.imageList.ImageSize.Width, this.imageList.ImageSize.Height);
        if (!flag)
          rectangle.X = bounds.Right - num - 1;
        graphics.DrawImage(itemImage, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
      }
      return num;
    }

    /// <summary>Gets the item font.</summary>
    /// <param name="item">The item.</param>
    /// <param name="state">The state.</param>
    /// <returns></returns>
    public virtual Font GetItemFont(ListItem item, DrawItemState state)
    {
      if (item.Font != null)
        return item.Font;
      Font font = this.Font;
      return state != DrawItemState.Selected ? (state == DrawItemState.HotLight || state == DrawItemState.Focus ? (this.Enabled ? this.Theme.StyleHighlight.Font : this.theme.StyleDisabled.Font) : (state != DrawItemState.Disabled ? (this.Enabled ? this.theme.StyleNormal.Font : this.theme.StyleDisabled.Font) : this.theme.StyleDisabled.Font)) : (this.Enabled ? this.theme.StylePressed.Font : this.theme.StyleDisabledPressed.Font);
    }

    /// <summary>Gets the color of the item fore.</summary>
    /// <param name="item">The item.</param>
    /// <param name="state">The state.</param>
    /// <returns></returns>
    public virtual Color GetItemForeColor(ListItem item, DrawItemState state)
    {
      Color color1 = !this.Enabled || !item.Enabled ? this.theme.StyleDisabled.TextColor : this.theme.StyleNormal.TextColor;
      if (!item.Enabled || !item.Enabled)
      {
        if (!item.UseThemeTextColor)
          color1 = item.DisabledTextColor;
        return color1;
      }
      if (this.Enabled)
      {
        Color color2 = this.theme.QueryColorSetter("ListItemColor");
        if (!color2.IsEmpty)
          color1 = color2;
      }
      if (this.Enabled && !item.UseThemeTextColor)
        color1 = item.TextColor;
      if (state == DrawItemState.Selected)
      {
        color1 = this.Enabled ? this.theme.StylePressed.TextColor : this.theme.StyleDisabledPressed.TextColor;
        if (this.Enabled)
        {
          Color color2 = this.theme.QueryColorSetter("ListItemSelectedColor");
          if (!color2.IsEmpty)
            color1 = color2;
        }
        if (!item.UseThemeTextColor)
          color1 = item.SelectedTextColor;
      }
      else if (state == DrawItemState.HotLight || state == DrawItemState.Focus)
      {
        color1 = this.Enabled ? this.theme.StyleHighlight.TextColor : this.theme.StyleDisabled.TextColor;
        if (this.Enabled)
        {
          Color color2 = this.theme.QueryColorSetter("ListItemHighlightColor");
          if (!color2.IsEmpty)
            color1 = color2;
        }
        if (!item.UseThemeTextColor)
          color1 = item.HighlightTextColor;
      }
      return color1;
    }

    /// <summary>Draws the list item text and description.</summary>
    /// <param name="graphics">The graphics.</param>
    /// <param name="bounds">The bounds.</param>
    /// <param name="listItem">The list item.</param>
    /// <param name="state">The state.</param>
    public virtual void DrawListItemTextAndDescription(Graphics graphics, Rectangle bounds, ListItem listItem, DrawItemState state)
    {
      bounds = new Rectangle(bounds.X + listItem.ItemLeftAndRightOffset, bounds.Y, bounds.Width - listItem.ItemLeftAndRightOffset * 2, bounds.Height);
      using (StringFormat format1 = new StringFormat())
      {
        format1.Alignment = listItem.StringAlignment;
        format1.LineAlignment = listItem.StringLineAlignment;
        StringFormat format2 = new StringFormat();
        format2.Alignment = listItem.DescriptionStringAlignment;
        format2.LineAlignment = listItem.DescriptionStringLineAlignment;
        if (this.RightToLeft == RightToLeft.Yes)
          format1.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
        Font itemFont = this.GetItemFont(listItem, state);
        Font font = new Font(itemFont.FontFamily, (double) itemFont.Size < 9.0 ? 9f : itemFont.Size, FontStyle.Bold);
        int num1 = listItem.Text != string.Empty ? 1 : 0;
        string[] strArray = listItem.Description.Split('\n');
        foreach (string str in strArray)
        {
          if (str.Length > 0)
            ++num1;
        }
        if (num1 == 0)
          num1 = 1;
        double num2 = (double) (bounds.Height / num1);
        Color itemForeColor = this.GetItemForeColor(listItem, state);
        SolidBrush solidBrush1 = new SolidBrush(itemForeColor);
        SolidBrush solidBrush2 = new SolidBrush(itemForeColor);
        if (!listItem.UseThemeTextColor && listItem.Enabled && this.Enabled)
          solidBrush2 = new SolidBrush(listItem.DescriptionTextColor);
        for (int index = 0; index < num1; ++index)
        {
          Rectangle rectangle = new Rectangle(bounds.X, bounds.Y + (int) ((double) index * num2) + 1, bounds.Width, (int) num2);
          --rectangle.Height;
          --rectangle.Width;
          if (rectangle.Width != 0 && rectangle.Height != 0)
          {
            if (index == 0 && listItem.Text.Length != 0)
            {
              rectangle = new Rectangle(rectangle.X + listItem.AbsoluteTopLeftEdgeTextOffset.X, rectangle.Y + listItem.AbsoluteTopLeftEdgeTextOffset.Y, rectangle.Width, rectangle.Height);
              graphics.DrawString(listItem.Text, num1 == 1 ? itemFont : font, (Brush) solidBrush1, (RectangleF) rectangle, format1);
            }
            else
            {
              string s = listItem.Text.Length != 0 ? strArray[index - 1] : strArray[index];
              graphics.DrawString(s, itemFont, (Brush) solidBrush2, (RectangleF) rectangle, format2);
            }
          }
        }
        solidBrush1.Dispose();
        font.Dispose();
        solidBrush2.Dispose();
      }
    }

    /// <summary>Raises the MouseWheel event.</summary>
    /// <param name="e">A MouseEventArgs that contains the event data.</param>
    protected override void OnMouseWheel(MouseEventArgs e)
    {
      if (!this.vscroll.Visible)
        return;
      if (e.Delta < 0)
      {
        if (this.vscroll.Value + this.ItemHeight <= this.vscroll.Maximum)
          this.vscroll.Value += this.ItemHeight;
        else
          this.vscroll.Value = this.vscroll.Maximum;
      }
      else if (e.Delta > 0)
      {
        if (this.vscroll.Value - this.ItemHeight >= this.vscroll.Minimum)
          this.vscroll.Value -= this.ItemHeight;
        else
          this.vscroll.Value = this.vscroll.Minimum;
      }
      this.vscroll.SyncThumbPositionWithLogicalValue();
      this.Invalidate();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.BindingContextChanged" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnBindingContextChanged(EventArgs e)
    {
      base.OnBindingContextChanged(e);
      if (this.DataSource == null)
        return;
      this.ReBind();
    }

    /// <summary>Rebinds the ListBox control.</summary>
    public void ReBind()
    {
      if (this.DataSource == null || this.Items.Count != 0)
        return;
      this.SetDataConnection(this.DataSource, new BindingMemberInfo(this.DisplayMember), true);
    }

    private void DataBind()
    {
      if (this.dataSource is IComponent)
        ((IComponent) this.dataSource).Disposed += new EventHandler(this.DataSourceDisposed);
      ISupportInitializeNotification initializeNotification = this.dataSource as ISupportInitializeNotification;
      if (initializeNotification != null && !initializeNotification.IsInitialized)
      {
        initializeNotification.Initialized += new EventHandler(this.DataSourceInitialized);
        this.isDataSourceInitEventHooked = true;
        this.isDataSourceInitialized = false;
      }
      else
        this.isDataSourceInitialized = true;
    }

    private void ResetSelectionMode()
    {
      this.SelectionMode = SelectionMode.One;
    }

    private bool ShouldSerializeSelectionMode()
    {
      return this.SelectionMode != SelectionMode.One;
    }

    private void DataSourceInitialized(object sender, EventArgs e)
    {
      this.SetDataConnection(this.dataSource, this.displayMember, true);
    }

    private void DataSourceDisposed(object sender, EventArgs e)
    {
      this.SetDataConnection((object) null, new BindingMemberInfo(""), true);
    }

    private void UnwireDataSource()
    {
      if (this.dataSource is IComponent)
        ((IComponent) this.dataSource).Disposed -= new EventHandler(this.DataSourceDisposed);
      ISupportInitializeNotification initializeNotification = this.dataSource as ISupportInitializeNotification;
      if (initializeNotification == null || !this.isDataSourceInitEventHooked)
        return;
      initializeNotification.Initialized -= new EventHandler(this.DataSourceInitialized);
      this.isDataSourceInitEventHooked = false;
    }

    /// <summary>
    /// Sets the object with the specified index in the derived class.
    /// </summary>
    /// <param name="index">The array index of the object.</param>
    /// <param name="value">The object.</param>
    protected virtual void SetItemCore(int index, object value)
    {
    }

    /// <summary>Removes the sort.</summary>
    public void RemoveSort()
    {
      this.Items.InnerList.Clear();
      this.Items.InnerList.AddRange((IEnumerable<ListItem>) this.Items.List);
      this.Refresh();
    }

    /// <summary>Sorts the specified comparer.</summary>
    /// <param name="comparer">The comparer.</param>
    public void Sort(IComparer<ListItem> comparer)
    {
      this.Items.InnerList.Sort(comparer);
      this.Refresh();
    }

    /// <summary>Sorts the specified ascending.</summary>
    /// <param name="ascending">if set to <c>true</c> ascending.</param>
    public void Sort(bool ascending)
    {
      this.Items.InnerList.Sort((IComparer<ListItem>) new ListItemComparer(ascending));
      this.Refresh();
    }

    /// <summary>Reverses this instance.</summary>
    public void Reverse()
    {
      this.Items.InnerList.Reverse();
      this.Refresh();
    }

    /// <summary>Sorts the specified comparison.</summary>
    /// <param name="comparison">The comparison.</param>
    public void Sort(Comparison<ListItem> comparison)
    {
      this.Items.InnerList.Sort(comparison);
      this.Refresh();
    }

    /// <summary>
    /// Sets the object with the specified index in the derived class.
    /// </summary>
    protected virtual void SetItemsCore(IList list)
    {
      this.Items.Clear();
      foreach (object obj1 in (IEnumerable) list)
      {
        try
        {
          if (obj1 == null)
            this.Items.Add("");
          else if (obj1 is ListItem)
            this.Items.Add((ListItem) obj1);
          else if (obj1 is string)
            this.Items.Add((string) obj1);
          else if (this.DataSource != null)
          {
            object obj2 = this.FilterItemOnProperty(obj1, this.displayMember.BindingField);
            if (obj2 == null)
              this.Items.Add("");
            string @string = Convert.ToString(obj2, (IFormatProvider) CultureInfo.CurrentCulture);
            ListItem listItem = new ListItem();
            ListItemFormatEventArgs args = new ListItemFormatEventArgs(listItem, @string);
            this.OnItemFormat(args);
            listItem.Text = args.ItemString;
            listItem.Value = this.FilterItemOnProperty(obj1, this.valueMember.BindingField);
            this.Items.Add(listItem);
          }
          else
            this.Items.Add(obj1.ToString());
        }
        catch (Exception ex)
        {
        }
      }
      this.Invalidate();
    }

    private void DataManager_ItemChanged(object sender, ItemChangedEventArgs e)
    {
      if (this.currencyManager == null)
        return;
      this.SetItemsCore(this.currencyManager.List);
      if (!this.AllowSelection)
        return;
      this.SelectedIndex = this.currencyManager.Position;
    }

    private void DataManager_PositionChanged(object sender, EventArgs e)
    {
      if (this.currencyManager == null || !this.AllowSelection)
        return;
      this.SelectedIndex = this.currencyManager.Position;
    }

    private static System.Type NullableUnwrap(System.Type type)
    {
      if (type == vListBox.stringType)
        return vListBox.stringType;
      return Nullable.GetUnderlyingType(type) ?? type;
    }

    private static bool IsNullData(object value, object dataSourceNullValue)
    {
      if (value != null && value != DBNull.Value)
        return object.Equals(value, vListBox.NullData(value.GetType(), dataSourceNullValue));
      return true;
    }

    private static object NullData(System.Type type, object dataSourceNullValue)
    {
      if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof (Nullable<>) || dataSourceNullValue != null && dataSourceNullValue != DBNull.Value)
        return dataSourceNullValue;
      return (object) null;
    }

    private static TypeConverter NullableUnwrap(TypeConverter typeConverter)
    {
      NullableConverter nullableConverter = typeConverter as NullableConverter;
      if (nullableConverter == null)
        return typeConverter;
      return nullableConverter.UnderlyingTypeConverter;
    }

    private static object FormatObject(object value, System.Type targetType, TypeConverter sourceConverter, TypeConverter targetConverter, string formatString, IFormatProvider formatInfo, object formattedNullValue, object dataSourceNullValue)
    {
      if (vListBox.IsNullData(value, dataSourceNullValue))
        value = (object) DBNull.Value;
      System.Type type = targetType;
      targetType = vListBox.NullableUnwrap(targetType);
      sourceConverter = vListBox.NullableUnwrap(sourceConverter);
      targetConverter = vListBox.NullableUnwrap(targetConverter);
      bool flag = targetType != type;
      object obj = vListBox.FormatObjectInternal(value, targetType, sourceConverter, targetConverter, formatString, formatInfo, formattedNullValue);
      if (type.IsValueType && obj == null && !flag)
        throw new Exception();
      return obj;
    }

    private static object FormatObjectInternal(object value, System.Type targetType, TypeConverter sourceConverter, TypeConverter targetConverter, string formatString, IFormatProvider formatInfo, object formattedNullValue)
    {
      if (value == DBNull.Value || value == null)
      {
        if (formattedNullValue != null)
          return formattedNullValue;
        if (targetType == vListBox.stringType)
          return (object) string.Empty;
        return (object) null;
      }
      if (targetType == vListBox.stringType && value is IFormattable && !string.IsNullOrEmpty(formatString))
        return (object) (value as IFormattable).ToString(formatString, formatInfo);
      System.Type type = value.GetType();
      TypeConverter converter1 = TypeDescriptor.GetConverter(type);
      if (sourceConverter != null && sourceConverter != converter1 && sourceConverter.CanConvertTo(targetType))
        return sourceConverter.ConvertTo((ITypeDescriptorContext) null, vListBox.GetFormatterCulture(formatInfo), value, targetType);
      TypeConverter converter2 = TypeDescriptor.GetConverter(targetType);
      if (targetConverter != null && targetConverter != converter2 && targetConverter.CanConvertFrom(type))
        return targetConverter.ConvertFrom((ITypeDescriptorContext) null, vListBox.GetFormatterCulture(formatInfo), value);
      if (targetType.IsAssignableFrom(type))
        return value;
      if (sourceConverter == null)
        sourceConverter = converter1;
      if (targetConverter == null)
        targetConverter = converter2;
      if (sourceConverter != null && sourceConverter.CanConvertTo(targetType))
        return sourceConverter.ConvertTo((ITypeDescriptorContext) null, vListBox.GetFormatterCulture(formatInfo), value, targetType);
      if (targetConverter != null && targetConverter.CanConvertFrom(type))
        return targetConverter.ConvertFrom((ITypeDescriptorContext) null, vListBox.GetFormatterCulture(formatInfo), value);
      if (!(value is IConvertible))
        throw new Exception();
      return vListBox.ChangeType(value, targetType, formatInfo);
    }

    private static CultureInfo GetFormatterCulture(IFormatProvider formatInfo)
    {
      if (formatInfo is CultureInfo)
        return formatInfo as CultureInfo;
      return CultureInfo.CurrentCulture;
    }

    private static object ChangeType(object value, System.Type type, IFormatProvider formatInfo)
    {
      try
      {
        if (formatInfo == null)
          formatInfo = (IFormatProvider) CultureInfo.CurrentCulture;
        return Convert.ChangeType(value, type, formatInfo);
      }
      catch (InvalidCastException ex)
      {
        throw new FormatException(ex.Message, (Exception) ex);
      }
    }

    /// <summary>Raises the DataSourceChanged event</summary>
    /// <param name="e">A EventArgs that contains the event data.</param>
    protected virtual void OnDataSourceChanged(EventArgs e)
    {
      if (this.DataSourceChanged == null)
        return;
      this.DataSourceChanged((object) this, EventArgs.Empty);
    }

    /// <summary>Raises the OnDisplayMemberChanged event</summary>
    /// <param name="e">A EventArgs that contains the event data.</param>
    protected virtual void OnDisplayMemberChanged(EventArgs e)
    {
      if (this.DisplayMemberChanged == null)
        return;
      this.DisplayMemberChanged((object) this, EventArgs.Empty);
    }

    internal int Find(PropertyDescriptor property, object key, bool keepIndex)
    {
      if (key == null)
        throw new ArgumentNullException("key");
      if (property != null && this.currencyManager.List is IBindingList && ((IBindingList) this.currencyManager.List).SupportsSearching)
        return ((IBindingList) this.currencyManager.List).Find(property, key);
      for (int index = 0; index < this.currencyManager.List.Count; ++index)
      {
        object obj = property.GetValue(this.currencyManager.List[index]);
        if (key.Equals(obj))
          return index;
      }
      return -1;
    }

    protected object FilterItemOnProperty(object item, string field)
    {
      if (item != null)
      {
        if (field.Length > 0)
        {
          try
          {
            PropertyDescriptor propertyDescriptor = this.currencyManager == null ? TypeDescriptor.GetProperties(item).Find(field, true) : this.currencyManager.GetItemProperties().Find(field, true);
            if (propertyDescriptor != null)
              item = propertyDescriptor.GetValue(item);
          }
          catch
          {
          }
        }
      }
      return item;
    }

    private void SetDataConnection(object newDataSource, BindingMemberInfo newDisplayMember, bool force)
    {
      bool flag1 = this.dataSource != newDataSource;
      bool flag2 = !this.displayMember.Equals((object) newDisplayMember);
      if (this.inSetDataConnection)
        return;
      try
      {
        if (force || flag1 || flag2)
        {
          this.inSetDataConnection = true;
          IList list = this.currencyManager != null ? this.currencyManager.List : (IList) null;
          this.UnwireDataSource();
          this.dataSource = newDataSource;
          this.displayMember = newDisplayMember;
          this.DataBind();
          if (this.isDataSourceInitialized)
          {
            CurrencyManager currencyManager = (CurrencyManager) null;
            if (newDataSource != null && this.BindingContext != null && newDataSource != Convert.DBNull)
              currencyManager = (CurrencyManager) this.BindingContext[newDataSource, newDisplayMember.BindingPath];
            if (this.currencyManager != currencyManager)
            {
              if (this.currencyManager != null)
              {
                this.currencyManager.ItemChanged -= new ItemChangedEventHandler(this.DataManager_ItemChanged);
                this.currencyManager.PositionChanged -= new EventHandler(this.DataManager_PositionChanged);
              }
              this.currencyManager = currencyManager;
              if (this.currencyManager != null)
              {
                this.currencyManager.ItemChanged += new ItemChangedEventHandler(this.DataManager_ItemChanged);
                this.currencyManager.PositionChanged += new EventHandler(this.DataManager_PositionChanged);
              }
            }
            if (this.currencyManager != null && (flag2 || flag1) && (this.displayMember.BindingMember != null && this.displayMember.BindingMember.Length != 0 && !this.BindingMemberInfoInDataManager(this.displayMember)))
              return;
            if (this.currencyManager != null || this.currencyManager != null && list != this.currencyManager.List)
              this.DataManager_ItemChanged((object) this.currencyManager, (ItemChangedEventArgs) null);
          }
        }
        if (flag1)
          this.OnDataSourceChanged(EventArgs.Empty);
        if (!flag2)
          return;
        this.OnDisplayMemberChanged(EventArgs.Empty);
      }
      finally
      {
        this.inSetDataConnection = false;
      }
    }

    private bool BindingMemberInfoInDataManager(BindingMemberInfo bindingMemberInfo)
    {
      if (this.currencyManager != null)
      {
        PropertyDescriptorCollection itemProperties = this.currencyManager.GetItemProperties();
        int count = itemProperties.Count;
        for (int index = 0; index < count; ++index)
        {
          if (!typeof (IList).IsAssignableFrom(itemProperties[index].PropertyType) && itemProperties[index].Name.Equals(bindingMemberInfo.BindingField))
            return true;
        }
        for (int index = 0; index < count; ++index)
        {
          if (!typeof (IList).IsAssignableFrom(itemProperties[index].PropertyType) && string.Compare(itemProperties[index].Name, bindingMemberInfo.BindingField, true, CultureInfo.CurrentCulture) == 0)
            return true;
        }
      }
      return false;
    }

    /// <summary>Raises the Layout event.</summary>
    /// <param name="levent">A LayoutEventArgs that contains the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      this.InitializeScrollBars();
    }

    private void HandleKey(int step, int startIndex)
    {
      int index = startIndex;
      ListItem listItem = this.Items[index];
      while (index > 0 && step < 0 || step > 0 && index < this.Items.Count - 1)
      {
        index += step;
        if (index < 0)
        {
          index = 0;
          break;
        }
        if (index >= this.Items.Count)
        {
          index = this.Items.Count - 1;
          break;
        }
        if (this.Items[index].Enabled)
          break;
      }
      if (index < 0)
        index = 0;
      if (index >= this.Items.Count)
        index = this.Items.Count - 1;
      this.SelectedIndex = index;
      this.EnsureVisible(this.SelectedIndex);
    }

    private void HandleKey(int step)
    {
      int index = this.SelectedIndex;
      while (index > 0 && step < 0 || step > 0 && index < this.Items.Count - 1)
      {
        index += step;
        if (index < 0)
        {
          index = 0;
          break;
        }
        if (index >= this.Items.Count)
        {
          index = this.Items.Count - 1;
          break;
        }
        if (this.Items[index].Enabled)
          break;
      }
      if (index < 0)
        index = 0;
      if (index >= this.Items.Count)
        index = this.Items.Count - 1;
      this.SelectedIndex = index;
      this.EnsureVisible(this.SelectedIndex);
    }

    /// <summary>Ensures that a list view item is visible.</summary>
    /// <param name="itemIndex">Index of the list view item that is to be visible.</param>
    public void EnsureVisible(int itemIndex)
    {
      if (itemIndex < 0 || itemIndex >= this.Items.Count || this.stopEnsureVisible)
        return;
      if (this.isScrollBarUpdateRequired)
        this.InitializeScrollBars();
      int num1;
      vVScrollBar vVscrollBar1;
      int num2;
      for (num1 = this.itemHeight + this.itemsSpacing; this.vscroll.Value <= itemIndex * num1 && this.vscroll.Value < this.vscroll.Maximum && itemIndex * num1 > this.vscroll.Value - num1 + this.Size.Height; vVscrollBar1.Value = num2)
      {
        vVscrollBar1 = this.vscroll;
        num2 = vVscrollBar1.Value + num1;
      }
      vVScrollBar vVscrollBar2;
      int num3;
      for (; this.vscroll.Value > itemIndex * num1 && this.vscroll.Value > this.vscroll.Minimum; vVscrollBar2.Value = num3)
      {
        this.vscroll.Value -= num1;
        if (itemIndex * num1 < this.vscroll.Value + this.Size.Height)
        {
          vVscrollBar2 = this.vscroll;
          num3 = vVscrollBar2.Value - num1;
        }
        else
          break;
      }
      this.vscroll.SyncThumbPositionWithLogicalValue();
      this.Invalidate();
    }

    /// <summary>Raises the OnLostFocus event.</summary>
    /// <param name="e">An EventArgs that contains the event data. </param>
    protected override void OnLostFocus(EventArgs e)
    {
      if (this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position))
        this.Focus();
      base.OnLostFocus(e);
    }

    internal bool CallProcessCmdKey(ref Message msg, Keys keyData)
    {
      return this.ProcessCmdKey(ref msg, keyData);
    }

    /// <summary>This member overrides Control.ProcessCmdKey.</summary>
    /// <param name="msg">A Message, passed by reference, that represents the window message to process. </param>
    /// <param name="keyData">One of the Keys values that represents the key to process. </param>
    /// <returns>true if the character was processed by the control; otherwise, false. </returns>
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      switch (keyData)
      {
        case Keys.Prior:
          this.HandleKey(-this.Size.Height / this.ItemHeight);
          return true;
        case Keys.Next:
          this.HandleKey(this.Size.Height / this.ItemHeight);
          return true;
        case Keys.End:
          this.SelectedIndex = this.Items.Count - 1;
          this.EnsureVisible(this.SelectedIndex);
          return true;
        case Keys.Home:
          this.SelectedIndex = 0;
          this.EnsureVisible(this.SelectedIndex);
          return true;
        case Keys.Left:
          this.HandleKey(-1);
          return true;
        case Keys.Up:
          this.HandleKey(-1);
          return true;
        case Keys.Right:
          this.HandleKey(1);
          return true;
        case Keys.Down:
          this.HandleKey(1);
          return true;
        default:
          return base.ProcessCmdKey(ref msg, keyData);
      }
    }

    private Graphics GetGraphics()
    {
      if (this.grfx == null)
        this.grfx = this.CreateGraphics();
      return this.grfx;
    }

    internal virtual void InitializeScrollBars()
    {
      if (!this.isScrollBarUpdateRequired || this.items == null)
        return;
      this.isScrollBarUpdateRequired = false;
      int val2 = 0;
      Graphics graphics = this.GetGraphics();
      foreach (object obj in this.Items)
      {
        ListItem listItem = new ListItem();
        if (obj is ListItem)
          listItem = (ListItem) obj;
        else if (obj is string)
          listItem.Text = (string) obj;
        Font itemFont = this.GetItemFont(listItem, DrawItemState.Default);
        SizeF sizeF = graphics.MeasureString(listItem.Text, itemFont);
        if (listItem.Description != "")
        {
          Font font = new Font(itemFont.FontFamily, (double) itemFont.Size < 9.0 ? 9f : itemFont.Size, itemFont.Style | FontStyle.Bold);
          sizeF = graphics.MeasureString(listItem.Text, font);
          font.Dispose();
        }
        val2 = Math.Max((int) sizeF.Width, val2);
        if (listItem.Description != "")
        {
          string description = listItem.Description;
          char[] chArray = new char[1]{ '\n' };
          foreach (string text in description.Split(chArray))
            val2 = Math.Max((int) graphics.MeasureString(text, itemFont).Width, val2);
        }
      }
      if (this.ImageList != null)
        val2 += this.ImageList.ImageSize.Width;
      int num1 = this.displayBounds.Height - 4;
      int num2 = this.displayBounds.Width - 4;
      if (val2 > num2)
        num1 -= this.hscroll.Height;
      int num3 = this.itemHeight + this.itemsSpacing;
      int num4;
      if (this.Items.Count * num3 - this.itemsSpacing > num1)
      {
        this.vscroll.Visible = true;
        this.vscroll.Maximum = this.displayBounds.Y + this.Items.Count * num3 - num1 - this.itemsSpacing;
        this.vscroll.LargeChange = num1 / num3 + 1;
        this.vscroll.SmallChange = num3;
        num4 = 15;
      }
      else
      {
        this.vscroll.Visible = false;
        num4 = 0;
      }
      if (this.vscroll.Visible)
        num2 -= this.vscroll.Width;
      int num5;
      if (val2 > num2)
      {
        this.hscroll.Visible = true;
        num5 = 15;
        this.hscroll.Maximum = val2 - num2 + 4;
        this.vscroll.Maximum += this.hscroll.Height;
      }
      else
      {
        this.hscroll.Visible = false;
        num5 = 0;
      }
      this.vscroll.Size = new Size(15, this.displayBounds.Height - num5 + 1);
      if (this.RightToLeft == RightToLeft.Yes)
        this.vscroll.Location = new Point(this.displayBounds.X, this.displayBounds.Y);
      else
        this.vscroll.Location = new Point(this.displayBounds.Right - this.vscroll.Width + 1, this.displayBounds.Y);
      this.hscroll.Size = new Size(this.displayBounds.Width - num4 + 1, 15);
      if (this.RightToLeft == RightToLeft.Yes)
        this.hscroll.Location = new Point(this.displayBounds.X + num4, this.displayBounds.Bottom - this.hscroll.Height + 1);
      else
        this.hscroll.Location = new Point(this.displayBounds.X, this.displayBounds.Bottom - this.hscroll.Height + 1);
      this.vscroll.Value = 0;
      this.hscroll.Value = this.RightToLeft == RightToLeft.Yes ? this.hscroll.Maximum : 0;
    }

    /// <summary>Raises the OnMouseDown event.</summary>
    /// <param name="mevent">A MouseEventArgs that contains the event data.</param>
    protected override void OnMouseDown(MouseEventArgs mevent)
    {
      base.OnMouseDown(mevent);
      if (!this.Visible)
        return;
      this.Focus();
      int val2 = this.HitTest(this.PointToClient(Cursor.Position));
      if (val2 == -1)
        return;
      if (this.AllowDragDrop)
      {
        this.canDrag = true;
        this.mouseDownPoint = mevent.Location;
      }
      if (val2 >= 0 && val2 < this.Items.Count && !this.Items[val2].Enabled)
        return;
      if (this.lastSelectedIndex != -1 && this.SelectionMode == SelectionMode.MultiExtended && this.IsShiftPressed())
      {
        int num1 = Math.Min(this.lastSelectedIndex, val2);
        int num2 = Math.Max(this.lastSelectedIndex, val2);
        this.lastSelectedIndex = this.lastSelectedIndex >= val2 ? num2 : num1;
        ListItemChangingEventArgs args = new ListItemChangingEventArgs((ListItem) null, this.SelectedItem);
        this.OnSelectedItemChanging(args);
        if (args.Cancel)
        {
          this.Invalidate();
        }
        else
        {
          this.SelectedItems.Clear();
          this.SelectedIndices.Clear();
          for (int index = num1; index <= num2; ++index)
          {
            ListItem listItem = this.Items[index];
            this.SelectedIndices.Add(index);
            this.SelectedItems.Add(listItem);
          }
          this.OnSelectedIndexChanged(EventArgs.Empty);
          this.OnSelectedValueChanged(EventArgs.Empty);
          this.OnSelectedItemChanged();
          this.Invalidate();
          this.Invalidate();
          this.OnItemMouseDown(new ListItemMouseEventArgs(this.FindItem(mevent.Location), mevent));
        }
      }
      else
      {
        this.SelectedIndex = val2;
        this.lastSelectedIndex = val2;
        this.Invalidate();
        this.OnItemMouseDown(new ListItemMouseEventArgs(this.FindItem(mevent.Location), mevent));
      }
    }

    /// <summary>Raises the OnMouseUp event.</summary>
    /// <param name="mevent">A MouseEventArgs that contains the event data.</param>
    protected override void OnMouseUp(MouseEventArgs mevent)
    {
      base.OnMouseUp(mevent);
      this.canDrag = false;
      if (this.isDragging)
      {
        this.mouseUpPoint = mevent.Location;
        this.EndDrag(this.dragSourceItem, this.FindItem(this.mouseUpPoint));
      }
      this.OnItemMouseUp(new ListItemMouseEventArgs(this.FindItem(mevent.Location), mevent));
    }

    /// <summary>Raises the OnMouseLeave event.</summary>
    /// <param name="e">A EventArgs that contains the event data.</param>
    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.hotTrackIndex = -1;
      this.Invalidate();
    }

    /// <summary>Raises the OnMouseMove event.</summary>
    /// <param name="mevent">A MouseEventArgs that contains the event data.</param>
    protected override void OnMouseMove(MouseEventArgs mevent)
    {
      base.OnMouseMove(mevent);
      if (this.hotTrack)
      {
        int num = this.HitTest(this.PointToClient(Cursor.Position));
        this.hotTrackIndex = num;
        if (this.lastHotTrackIndex != this.hotTrackIndex)
        {
          this.lastHotTrackIndex = num;
          this.Invalidate();
        }
      }
      if (mevent.Button == MouseButtons.Left && this.canDrag)
      {
        if (this.AllowDragDrop && !this.isDragging)
        {
          Point point = new Point(this.mouseDownPoint.X - mevent.Location.X, this.mouseDownPoint.Y - mevent.Location.Y);
          if (Math.Abs(point.X) >= SystemInformation.DragSize.Width || Math.Abs(point.Y) >= SystemInformation.DragSize.Height)
          {
            ListItem drag = this.FindItem(mevent.Location);
            if (drag != null)
              this.BeginDrag(drag);
          }
        }
        else if (this.dragDropForm != null)
        {
          this.dragDropForm.Location = Cursor.Position;
          this.OnItemDragging(new ListItemDragEventArgs(this.dragSourceItem, (ListItem) null));
        }
      }
      if (this.isDragging && this.AllowDragDrop && mevent.Button == MouseButtons.None)
        this.EndDrag(this.dragSourceItem, (ListItem) null);
      if (!this.isDragging || !this.AllowDragDrop)
        return;
      this.Invalidate();
    }

    private void draggingTimer_Tick(object sender, EventArgs e)
    {
      Point client = this.PointToClient(Cursor.Position);
      if (!this.vscroll.Visible)
      {
        this.draggingTimer.Stop();
      }
      else
      {
        if (client.X < 0 || client.X > this.Width || !this.vscroll.Visible)
          return;
        if (client.Y <= 10 && client.Y >= 0)
        {
          if (this.vscroll.Value - this.ItemHeight >= 0)
            this.vscroll.Value -= this.ItemHeight;
          else
            this.vscroll.Value = 0;
        }
        else
        {
          if (client.Y < this.Height - 10 || client.Y > this.Height || !this.vscroll.Visible)
            return;
          if (this.vscroll.Value + this.ItemHeight < this.vscroll.Maximum)
            this.vscroll.Value += this.ItemHeight;
          else
            this.vscroll.Value = this.vscroll.Maximum;
        }
      }
    }

    /// <summary>Performs a HitTest at a specific point</summary>
    /// <param name="point">The point where to perform the HitTest</param>
    /// <returns>The zero-based index of the item located at the HitTest point. If there's no item at this point, the method returns -1.</returns>
    public int HitTest(Point point)
    {
      int num1 = this.itemHeight + this.itemsSpacing;
      int num2 = this.displayBounds.Height - 4;
      if (this.hscroll.Visible)
        num2 -= this.hscroll.Height;
      int num3 = this.displayBounds.Width - 4;
      if (this.vscroll.Visible)
        num3 -= this.vscroll.Width;
      int num4 = this.vscroll.Value / num1;
      if (num4 > 0)
        --num4;
      int num5 = num2 / num1 + num4 + 2;
      if (num5 >= this.items.Count)
        num5 = this.items.Count - 1;
      int y = 2 - this.vscroll.Value + num1 * num4;
      int x = 2 - this.hscroll.Value;
      for (int index = num4; index <= num5; ++index)
      {
        Rectangle rectangle = new Rectangle(x, y, num3 - x, this.ItemHeight);
        if (this.Items[index].Visible)
        {
          if (rectangle.Contains(new Point(point.X, point.Y)))
            return index;
          y += num1;
        }
      }
      return -1;
    }

    /// <summary>Begins a batch of changes on the items collection</summary>
    public void BeginUpdate()
    {
      this.isUpdating = true;
    }

    /// <summary>
    /// Begins a batch of changes on the items collection previously started by BeginUpdate()
    /// </summary>
    public void EndUpdate()
    {
      this.InitializeScrollBars();
      this.isUpdating = false;
    }

    /// <summary>Paints the background of the control.</summary>
    /// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains information about the control to paint.</param>
    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      using (Brush brush = (Brush) new SolidBrush(Color.Transparent))
        pevent.Graphics.FillRectangle(brush, this.ClientRectangle);
    }

    /// <summary>Gets the color of the back fill.</summary>
    /// <returns></returns>
    protected Color GetBackFillColor()
    {
      if (!this.UseThemeBackColor)
        return this.backColor;
      if (!this.Enabled)
        return this.theme.StyleDisabled.FillStyle.Colors[0];
      Color color = this.Theme.QueryColorSetter("ListBoxBackColor");
      if (color == Color.Empty)
        return this.theme.StyleNormal.FillStyle.Colors[0];
      return color;
    }

    /// <summary>Raises the OnPaint event.</summary>
    /// <param name="pevent">A PaintEventArgs that contains the event data. </param>
    protected override void OnPaint(PaintEventArgs e)
    {
      if (!this.DesignMode)
        Licensing.LICheck((Control) this);
      this.RenderListBox(e);
    }

    /// <summary>Get ListItem at specific point.</summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public ListItem FindItem(Point point)
    {
      int num1 = this.itemHeight + this.itemsSpacing;
      int num2 = this.displayBounds.Height - 4;
      if (this.hscroll.Visible)
        num2 -= this.hscroll.Height;
      int num3 = this.displayBounds.Width - 4;
      if (this.vscroll.Visible)
      {
        int num4 = num3 - this.vscroll.Width;
      }
      int num5 = this.vscroll.Value / num1;
      if (num5 > 0)
        --num5;
      int num6 = num2 / num1 + num5 + 2;
      if (num6 >= this.items.Count)
        num6 = this.items.Count - 1;
      int y = this.displayBounds.Y;
      int num7 = this.vscroll.Value;
      int num8 = this.displayBounds.X + 2 - (this.hscroll.Visible ? this.hscroll.Value : 0);
      if (this.RightToLeft == RightToLeft.Yes && this.vscroll.Visible)
      {
        int num9 = num8 + (this.vscroll.Width - 1);
      }
      Rectangle rectangle = this.displayBounds;
      ++rectangle.Height;
      ++rectangle.Width;
      if (this.vscroll.Visible)
      {
        rectangle.Width -= this.vscroll.Width;
        if (this.RightToLeft == RightToLeft.Yes)
          rectangle.X += this.vscroll.Width;
      }
      if (this.hscroll.Visible)
        rectangle.Height -= this.hscroll.Height;
      for (int index = num5; index <= num6; ++index)
      {
        ListItem listItem = this.Items[index];
        if (listItem.RenderBounds.Contains(point))
          return listItem;
      }
      return (ListItem) null;
    }

    public static ListItem FindItem(Point point, vListBox listBox)
    {
      int num1 = listBox.ItemHeight + listBox.ItemsSpacing;
      int num2 = listBox.displayBounds.Height - 4;
      if (listBox.hscroll.Visible)
        num2 -= listBox.hscroll.Height;
      int num3 = listBox.displayBounds.Width - 4;
      if (listBox.vscroll.Visible)
      {
        int num4 = num3 - listBox.vscroll.Width;
      }
      int num5 = listBox.vscroll.Value / num1;
      if (num5 > 0)
        --num5;
      int num6 = num2 / num1 + num5 + 2;
      if (num6 >= listBox.Items.Count)
        num6 = listBox.Items.Count - 1;
      int y = listBox.displayBounds.Y;
      int num7 = listBox.vscroll.Value;
      int num8 = listBox.displayBounds.X + 2 - (listBox.hscroll.Visible ? listBox.hscroll.Value : 0);
      if (listBox.RightToLeft == RightToLeft.Yes && listBox.vscroll.Visible)
      {
        int num9 = num8 + (listBox.vscroll.Width - 1);
      }
      Rectangle rectangle = listBox.displayBounds;
      ++rectangle.Height;
      ++rectangle.Width;
      if (listBox.vscroll.Visible)
      {
        rectangle.Width -= listBox.vscroll.Width;
        if (listBox.RightToLeft == RightToLeft.Yes)
          rectangle.X += listBox.vscroll.Width;
      }
      if (listBox.hscroll.Visible)
        rectangle.Height -= listBox.hscroll.Height;
      for (int index = num5; index <= num6; ++index)
      {
        ListItem listItem = listBox.Items[index];
        if (listItem.RenderBounds.Contains(point))
          return listItem;
      }
      return (ListItem) null;
    }

    /// <summary>Renders the list box.</summary>
    /// <param name="e">The <see cref="T:System.Windows.Forms.PaintEventArgs" /> instance containing the event data.</param>
    protected virtual void RenderListBox(PaintEventArgs e)
    {
      if (this.isScrollBarUpdateRequired && !this.isItemDropping)
      {
        if (this.SelectedIndex != -1)
          this.EnsureVisible(this.SelectedIndex);
        else
          this.InitializeScrollBars();
      }
      if (this.backFill == null)
        return;
      Color backFillColor = this.GetBackFillColor();
      this.vscroll.BackColor = Color.Transparent;
      this.hscroll.BackColor = Color.Transparent;
      this.backFill.Bounds = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
      VisualStyle visualStyle = this.Enabled ? this.theme.StyleNormal : this.theme.StyleDisabled;
      FillStyle fillStyle = visualStyle.FillStyle;
      visualStyle.FillStyle = (FillStyle) new FillStyleSolid(backFillColor);
      this.backFill.DrawStandardFill(e.Graphics, this.Enabled ? ControlState.Normal : ControlState.Disabled, GradientStyles.Solid);
      visualStyle.FillStyle = fillStyle;
      int radius = this.backFill.Radius;
      int num1 = this.itemHeight + this.itemsSpacing;
      int num2 = this.displayBounds.Height - 4;
      if (this.hscroll.Visible)
        num2 -= this.hscroll.Height;
      int num3 = this.displayBounds.Width - 4;
      if (this.vscroll.Visible)
        num3 -= this.vscroll.Width;
      int num4 = this.vscroll.Value / num1;
      if (num4 > 0)
        --num4;
      int num5 = num2 / num1 + num4 + 2;
      if (num5 >= this.items.Count)
        num5 = this.items.Count - 1;
      int y = this.displayBounds.Y + 2 - this.vscroll.Value + num1 * num4;
      int x = this.displayBounds.X + 2 - (this.hscroll.Visible ? this.hscroll.Value : 0);
      if (this.RightToLeft == RightToLeft.Yes && this.vscroll.Visible)
        x += this.vscroll.Width - 1;
      Region clip = e.Graphics.Clip;
      Rectangle rect1 = this.displayBounds;
      ++rect1.Height;
      ++rect1.Width;
      if (this.vscroll.Visible)
      {
        rect1.Width -= this.vscroll.Width;
        if (this.RightToLeft == RightToLeft.Yes)
          rect1.X += this.vscroll.Width;
      }
      if (this.hscroll.Visible)
        rect1.Height -= this.hscroll.Height;
      e.Graphics.Clip = new Region(rect1);
      for (int index = num4; index <= num5; ++index)
      {
        ListItem listItem = this.Items[index];
        if (listItem.Visible)
        {
          int width = num3 + (this.hscroll.Visible ? this.hscroll.Maximum : 0);
          if (this.vscroll.Visible)
            ++width;
          Rectangle rect2 = new Rectangle(x, y, width, this.ItemHeight);
          y += num1;
          Font font = this.Font;
          if (listItem != null && listItem.Font != null)
            font = listItem.Font;
          if (this.hotTrackIndex == index)
          {
            if (this.DrawListItem != null)
              this.DrawListItem((object) this, new DrawItemEventArgs(e.Graphics, font, rect2, index, DrawItemState.HotLight));
            else
              this.OnDrawItem(new DrawItemEventArgs(e.Graphics, font, rect2, index, DrawItemState.HotLight));
          }
          else if (!this.SelectedIndices.Contains(index))
          {
            if (this.DrawListItem != null)
              this.DrawListItem((object) this, new DrawItemEventArgs(e.Graphics, font, rect2, index, DrawItemState.None));
            else
              this.OnDrawItem(new DrawItemEventArgs(e.Graphics, font, rect2, index, DrawItemState.None));
          }
          else if (this.DrawListItem != null)
            this.DrawListItem((object) this, new DrawItemEventArgs(e.Graphics, font, rect2, index, DrawItemState.Selected));
          else
            this.OnDrawItem(new DrawItemEventArgs(e.Graphics, this.Font, rect2, index, DrawItemState.Selected));
        }
      }
      e.Graphics.Clip = clip;
      if (this.isDragging && this.AllowDragDrop)
        this.DrawDragFeedback(e.Graphics);
      this.backFill.Bounds = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
      this.backFill.Radius = radius;
      if (this.UseThemeBorderColor)
        this.backFill.DrawElementBorder(e.Graphics, this.Enabled ? ControlState.Normal : ControlState.Disabled);
      else
        this.backFill.DrawElementBorder(e.Graphics, this.Enabled ? ControlState.Normal : ControlState.Disabled, this.BorderColor);
    }

    protected internal virtual void DrawDragFeedback(Graphics g)
    {
      Point client = this.PointToClient(Cursor.Position);
      ListItem listItem = this.FindItem(client);
      if (listItem == null || listItem == this.dragSourceItem)
        return;
      using (Pen pen = new Pen(Color.Blue, 2f))
      {
        pen.DashStyle = DashStyle.Dash;
        if (!new Rectangle(listItem.RenderBounds.X, listItem.RenderBounds.Y + this.ItemHeight / 2, listItem.RenderBounds.Width, this.ItemHeight / 2 + this.ItemsSpacing + 1).Contains(client))
          g.DrawLine(pen, 2, listItem.RenderBounds.Y, this.Width - 4, listItem.RenderBounds.Y);
        else
          g.DrawLine(pen, 2, listItem.RenderBounds.Y + this.ItemHeight, this.Width - 4, listItem.RenderBounds.Y + this.ItemHeight);
      }
    }

    /// <summary>Draw</summary>
    /// <param name="g"></param>
    /// <param name="listBox"></param>
    public static void DrawDragFeedback(Graphics g, vListBox listBox)
    {
      if (listBox == null || g == null)
        return;
      Point client = listBox.PointToClient(Cursor.Position);
      ListItem listItem = listBox.FindItem(client);
      if (listItem != null)
      {
        using (Pen pen = new Pen(Color.Blue, 2f))
        {
          pen.DashStyle = DashStyle.Dash;
          if (!new Rectangle(listItem.RenderBounds.X, listItem.RenderBounds.Y + listBox.ItemHeight / 2, listItem.RenderBounds.Width, listBox.ItemHeight / 2 + listBox.ItemsSpacing + 1).Contains(client))
          {
            if (listItem.RenderBounds.Y != vListBox.cachedDropFeedbackY)
            {
              listBox.Refresh();
              g.DrawLine(pen, 2, listItem.RenderBounds.Y, listBox.Width - 4, listItem.RenderBounds.Y);
            }
            vListBox.cachedDropFeedbackY = listItem.RenderBounds.Y;
          }
          else
          {
            if (listItem.RenderBounds.Y + listBox.ItemHeight != vListBox.cachedDropFeedbackY)
            {
              listBox.Refresh();
              g.DrawLine(pen, 2, listItem.RenderBounds.Y + listBox.ItemHeight, listBox.Width - 4, listItem.RenderBounds.Y + listBox.ItemHeight);
            }
            vListBox.cachedDropFeedbackY = listItem.RenderBounds.Y + listBox.ItemHeight;
          }
        }
      }
      else
        listBox.Invalidate();
    }
  }
}
