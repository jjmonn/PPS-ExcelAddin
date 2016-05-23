// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.ListItem
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System.ComponentModel;
using System.Drawing;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vListBox item</summary>
  public class ListItem : INotifyPropertyChanged
  {
    private int imageIndex = -1;
    private int imageIndexHover = -1;
    private int imageIndexSelected = -1;
    private Color textColor = Color.Black;
    private Color highlightTextColor = Color.Black;
    private Color selectedTextColor = Color.Black;
    private Color descriptionTextColor = Color.Black;
    private bool useThemeTextColor = true;
    private bool? isChecked = new bool?(false);
    private StringAlignment stringLineAlignment = StringAlignment.Center;
    private StringAlignment descriptionStringLineAlignment = StringAlignment.Center;
    private Point absolutetopLeftEdgeTextOffset = Point.Empty;
    private string text = "Item Text";
    private string description = "";
    private bool imageBeforeText = true;
    private Color disabledTextColor = Color.Silver;
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
    private bool useThemeBackground = true;
    private bool enabled = true;
    private bool visible = true;
    private bool isThreeState;
    private int itemLeftAndRightSpacing;
    private StringAlignment stringAlignment;
    private StringAlignment descriptionStringAlignment;
    private Font font;
    private object value;
    private object tag;
    private Rectangle renderBounds;

    /// <summary>Gets or sets the description string line alignment.</summary>
    /// <value>The description string line alignment.</value>
    [System.ComponentModel.Description("Gets or sets the description string line alignment.")]
    [Category("Appearance")]
    public StringAlignment DescriptionStringLineAlignment
    {
      get
      {
        return this.descriptionStringLineAlignment;
      }
      set
      {
        this.descriptionStringLineAlignment = value;
        this.OnPropertyChanged("DescriptionStringLineAlignment");
      }
    }

    /// <summary>Gets or sets the string line alignment.</summary>
    /// <value>The string line alignment.</value>
    [Category("Appearance")]
    [System.ComponentModel.Description("Gets or sets the string line alignment.")]
    public StringAlignment StringLineAlignment
    {
      get
      {
        return this.stringLineAlignment;
      }
      set
      {
        this.stringLineAlignment = value;
        this.OnPropertyChanged("StringLineAlignment");
      }
    }

    /// <summary>Gets or sets the string alignment.</summary>
    /// <value>The string alignment.</value>
    [Category("Appearance")]
    [System.ComponentModel.Description("Gets or sets the string alignment.")]
    public StringAlignment StringAlignment
    {
      get
      {
        return this.stringAlignment;
      }
      set
      {
        this.stringAlignment = value;
        this.OnPropertyChanged("StringAlignment");
      }
    }

    /// <summary>Gets or sets the description string alignment.</summary>
    /// <value>The description string alignment.</value>
    [Category("Appearance")]
    [System.ComponentModel.Description("Gets or sets the description string alignment.")]
    public StringAlignment DescriptionStringAlignment
    {
      get
      {
        return this.descriptionStringAlignment;
      }
      set
      {
        this.descriptionStringAlignment = value;
        this.OnPropertyChanged("DescriptionStringAlignment");
      }
    }

    /// <summary>Gets or sets the item's left and right border offset.</summary>
    /// <value>The item left and right spacing.</value>
    [System.ComponentModel.Description("Gets or sets the item's left and right border offset.")]
    [Category("Appearance")]
    [DefaultValue(0)]
    public int ItemLeftAndRightOffset
    {
      get
      {
        return this.itemLeftAndRightSpacing;
      }
      set
      {
        if (value == this.itemLeftAndRightSpacing)
          return;
        this.itemLeftAndRightSpacing = value;
        this.OnPropertyChanged("ItemLeftAndRightSpacing");
      }
    }

    /// <summary>Gets or sets the absolute top left edge text offset.</summary>
    /// <value>The absolute top left edge text offset.</value>
    [System.ComponentModel.Description("Gets or sets the absolute top left edge text offset.")]
    [Category("Appearance")]
    public Point AbsoluteTopLeftEdgeTextOffset
    {
      get
      {
        return this.absolutetopLeftEdgeTextOffset;
      }
      set
      {
        if (!(value != this.absolutetopLeftEdgeTextOffset))
          return;
        this.absolutetopLeftEdgeTextOffset = value;
        this.OnPropertyChanged("AbsoluteTopLeftEdgeTextOffset");
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the ListItem supports 3 check states.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is three state; otherwise, <c>false</c>.
    /// </value>
    [System.ComponentModel.Description("Gets or sets a value indicating whether the ListItem supports 3 check states.")]
    [Category("Behavior")]
    [DefaultValue(false)]
    public bool IsThreeState
    {
      get
      {
        return this.isThreeState;
      }
      set
      {
        if (value == this.isThreeState)
          return;
        this.isThreeState = value;
        this.OnPropertyChanged("IsThreeState");
      }
    }

    /// <summary>Gets the render bounds.</summary>
    /// <value>The render bounds.</value>
    [Browsable(false)]
    public Rectangle RenderBounds
    {
      get
      {
        return this.renderBounds;
      }
      internal set
      {
        this.renderBounds = value;
      }
    }

    /// <summary>Gets or sets the tag.</summary>
    /// <value>The tag.</value>
    [Category("Behavior")]
    [DefaultValue(null)]
    [System.ComponentModel.Description("Gets or sets the Tag object associated to the ListItem.")]
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

    /// <summary>Gets or sets the color of the disabled text.</summary>
    /// <value>The color of the disabled text.</value>
    [DefaultValue(typeof (Color), "Silver")]
    [System.ComponentModel.Description("Gets or sets the color of the disabled text.")]
    [Category("Appearance")]
    public Color DisabledTextColor
    {
      get
      {
        return this.disabledTextColor;
      }
      set
      {
        if (!(value != this.disabledTextColor))
          return;
        this.disabledTextColor = value;
        this.OnPropertyChanged("DisabledTextColor");
      }
    }

    /// <summary>Gets or sets the color of the text.</summary>
    /// <value>The color of the text.</value>
    [System.ComponentModel.Description("Gets or sets the color of the text.")]
    [DefaultValue(typeof (Color), "Black")]
    [Category("Appearance")]
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
        this.OnPropertyChanged("TextColor");
      }
    }

    /// <summary>Gets or sets the color of the highlight text.</summary>
    /// <value>The color of the highlight text.</value>
    [System.ComponentModel.Description("Gets or sets the color of the highlight text.")]
    [DefaultValue(typeof (Color), "Black")]
    [Category("Appearance")]
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
        this.OnPropertyChanged("HighlightTextColor");
      }
    }

    /// <summary>Gets or sets the color of the selected text.</summary>
    /// <value>The color of the selected text.</value>
    [DefaultValue(typeof (Color), "Black")]
    [Category("Appearance")]
    [System.ComponentModel.Description("Gets or sets the color of the selected text.")]
    public Color SelectedTextColor
    {
      get
      {
        return this.selectedTextColor;
      }
      set
      {
        if (!(value != this.selectedTextColor))
          return;
        this.selectedTextColor = value;
        this.OnPropertyChanged("SelectedTextColor");
      }
    }

    /// <summary>Gets or sets the color of the description text.</summary>
    /// <value>The color of the description text.</value>
    [Category("Appearance")]
    [System.ComponentModel.Description("Gets or sets the color of the description text.")]
    [DefaultValue(typeof (Color), "Black")]
    public Color DescriptionTextColor
    {
      get
      {
        return this.descriptionTextColor;
      }
      set
      {
        if (!(value != this.descriptionTextColor))
          return;
        this.descriptionTextColor = value;
        this.OnPropertyChanged("DescriptionTextColor");
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
    /// Gets or sets the rounded corners radius of the ListItem.
    /// </summary>
    [System.ComponentModel.Description("Gets or sets the rounded corners radius of the ListItem.")]
    [DefaultValue(3)]
    [Category("Behavior")]
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
    [System.ComponentModel.Description("Gets or sets the background border.")]
    [DefaultValue(typeof (Color), "Black")]
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
    [Category("Appearance")]
    [System.ComponentModel.Description("Gets or sets the DisabledBackgroundBorder border.")]
    [DefaultValue(typeof (Color), "Silver")]
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
    [System.ComponentModel.Description("Gets or sets the disabled background brush.")]
    [Category("Appearance")]
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
    [System.ComponentModel.Description("Gets or sets the highlightBackground border.")]
    [Category("Appearance")]
    [DefaultValue(typeof (Color), "Black")]
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
    [DefaultValue(typeof (Color), "Black")]
    [System.ComponentModel.Description("Gets or sets the selectedBackground border.")]
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
    [System.ComponentModel.Description("Gets or sets the background brush.")]
    [Browsable(false)]
    [Category("Appearance")]
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
    [Category("Appearance")]
    [System.ComponentModel.Description("Gets or sets the selected background brush.")]
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
    [System.ComponentModel.Description("Gets or sets the HighlightBackground brush.")]
    [Browsable(false)]
    [Category("Appearance")]
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

    /// <summary>
    /// Gets or sets a value indicating whether to use theme's background
    /// </summary>
    [DefaultValue(true)]
    [Category("Appearance")]
    [System.ComponentModel.Description("Gets or sets a value indicating whether to use theme's background")]
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
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use the default text color of the theme.
    /// </summary>
    [DefaultValue(true)]
    [System.ComponentModel.Description("Gets or sets a value indicating whether to use the default text color of the theme.")]
    [Category("Appearance")]
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
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the item is checked.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the item is checked; otherwise, <c>false</c>.
    /// </value>
    [System.ComponentModel.Description("Gets or sets a value indicating whether the item is checked/unchecked.")]
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool? IsChecked
    {
      get
      {
        return this.isChecked;
      }
      set
      {
        bool? nullable1 = value;
        bool? nullable2 = this.isChecked;
        if ((nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : (nullable1.HasValue != nullable2.HasValue ? 1 : 0)) == 0)
          return;
        this.isChecked = value;
        this.OnPropertyChanged("IsChecked");
      }
    }

    /// <summary>
    /// Gets or sets whether the item's image is displayed before or after the item's text and description
    /// </summary>
    [System.ComponentModel.Description("Gets or sets whether the item's image is displayed before or after the item's text and description.")]
    [Category("Appearance")]
    [DefaultValue(true)]
    public bool ImageBeforeText
    {
      get
      {
        return this.imageBeforeText;
      }
      set
      {
        if (value == this.imageBeforeText)
          return;
        this.imageBeforeText = value;
        this.OnPropertyChanged("IsChecked");
      }
    }

    /// <summary>Gets or sets a user defined value</summary>
    /// <remarks>
    /// Use this property to associate the ListItem with a specific object.
    /// </remarks>
    [System.ComponentModel.Description("Gets or sets a user defined value")]
    [Category("Behavior")]
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

    /// <summary>Gets or sets the font.</summary>
    /// <value>The font.</value>
    [System.ComponentModel.Description("Gets or sets the font.")]
    [DefaultValue(null)]
    [Category("Appearance")]
    public Font Font
    {
      get
      {
        return this.font;
      }
      set
      {
        if (value == this.font)
          return;
        this.font = value;
        this.OnPropertyChanged("Font");
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:VIBlend.WinForms.Controls.ListItem" /> is enabled.
    /// </summary>
    /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
    [Category("Behavior")]
    [System.ComponentModel.Description("Gets or sets a value whether the item is enabled.")]
    [DefaultValue(true)]
    public bool Enabled
    {
      get
      {
        return this.enabled;
      }
      set
      {
        if (value == this.enabled)
          return;
        this.enabled = value;
        this.OnPropertyChanged("Enabled");
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:VIBlend.WinForms.Controls.ListItem" /> is visible.
    /// </summary>
    /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
    [Category("Behavior")]
    [DefaultValue(true)]
    [System.ComponentModel.Description("Gets or sets a value whether the item is visible.")]
    public bool Visible
    {
      get
      {
        return this.visible;
      }
      set
      {
        if (value == this.visible)
          return;
        this.visible = value;
        this.OnPropertyChanged("Enabled");
      }
    }

    /// <summary>
    /// Gets or sets the image list index value of the item's image in the normal state.
    /// </summary>
    [System.ComponentModel.Description("Gets or sets the image list index value of the item's image in the normal state.")]
    [DefaultValue(-1)]
    [Category("Appearance")]
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
      }
    }

    /// <summary>
    /// Gets or sets the image list index value of the item's image in the hover state
    /// </summary>
    [DefaultValue(-1)]
    [Category("Appearance")]
    [System.ComponentModel.Description("Gets or sets the image list index value of the item's image in the hover state.")]
    public int ImageIndexHover
    {
      get
      {
        return this.imageIndexHover;
      }
      set
      {
        if (value == this.imageIndexHover)
          return;
        this.imageIndexHover = value;
        this.OnPropertyChanged("ImageIndexHover");
      }
    }

    /// <summary>
    /// Gets or sets the image list index value of the item's image in the selected state.
    /// </summary>
    [DefaultValue(-1)]
    [Category("Appearance")]
    [System.ComponentModel.Description("Gets or sets the image list index value of the item's image in the selected state.")]
    public int ImageIndexSelected
    {
      get
      {
        return this.imageIndexSelected;
      }
      set
      {
        if (value == this.imageIndexSelected)
          return;
        this.imageIndexSelected = value;
        this.OnPropertyChanged("ImageIndexSelected");
      }
    }

    /// <summary>Gets or sets the text of the item.</summary>
    [Category("Appearance")]
    [DefaultValue("Item Text")]
    [Browsable(true)]
    [System.ComponentModel.Description("Gets or sets the ListItem's Text.")]
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
        this.OnPropertyChanged("Text");
      }
    }

    /// <summary>
    /// Gets or sets the description of the item. In a ListItem the description appears as a detailed text under the main item's text.
    /// </summary>
    [Category("Appearance")]
    [Browsable(true)]
    [DefaultValue("")]
    [System.ComponentModel.Description("Gets or sets the item's description text.")]
    public string Description
    {
      get
      {
        return this.description;
      }
      set
      {
        if (!(value != this.description))
          return;
        this.description = value;
        this.OnPropertyChanged("Description");
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void ResetDescriptionStringLineAlignment()
    {
      this.DescriptionStringLineAlignment = StringAlignment.Center;
    }

    private bool ShouldSerializeDescriptionStringLineAlignment()
    {
      return this.DescriptionStringLineAlignment != StringAlignment.Center;
    }

    private void ResetStringLineAlignment()
    {
      this.StringLineAlignment = StringAlignment.Center;
    }

    private bool ShouldSerializeStringLineAlignment()
    {
      return this.StringLineAlignment != StringAlignment.Center;
    }

    private void ResetStringAlignment()
    {
      this.StringAlignment = StringAlignment.Near;
    }

    private bool ShouldSerializeStringAlignment()
    {
      return this.StringAlignment != StringAlignment.Near;
    }

    private void ResetDescriptionStringAlignment()
    {
      this.DescriptionStringAlignment = StringAlignment.Near;
    }

    private bool ShouldSerializeDescriptionStringAlignment()
    {
      return this.DescriptionStringAlignment != StringAlignment.Near;
    }

    private void ResetAbsoluteTopLeftEdgeTextOffset()
    {
      this.AbsoluteTopLeftEdgeTextOffset = Point.Empty;
    }

    private bool ShouldSerializeAbsoluteTopLeftEdgeTextOffset()
    {
      return this.AbsoluteTopLeftEdgeTextOffset != Point.Empty;
    }

    /// <summary>
    /// Determines whether the specified <see cref="T:System.Object" /> is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="T:System.Object" /> to compare with this instance.</param>
    /// <returns>
    /// 	<c>true</c> if the specified <see cref="T:System.Object" /> is equal to this instance; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">
    /// The <paramref name="obj" /> parameter is null.
    /// </exception>
    public override bool Equals(object obj)
    {
      ListItem listItem = obj as ListItem;
      if (listItem != null && !string.IsNullOrEmpty(listItem.Text) && !string.IsNullOrEmpty(this.Text))
        return listItem.Text.Equals(this.Text);
      return base.Equals(obj);
    }

    /// <summary>
    /// Returns a <see cref="T:System.String" /> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
      if (!string.IsNullOrEmpty(this.text))
        return this.Text;
      return base.ToString();
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>Clones this instance.</summary>
    /// <returns></returns>
    public ListItem Clone()
    {
      return new ListItem() { Text = this.Text, Value = this.Value, Font = this.Font, HighlightTextColor = this.HighlightTextColor, ImageBeforeText = this.ImageBeforeText, ImageIndex = this.ImageIndex, ImageIndexSelected = this.ImageIndexSelected, IsChecked = this.IsChecked, RenderBounds = this.RenderBounds, SelectedTextColor = this.SelectedTextColor, Tag = this.Tag, TextColor = this.TextColor, UseThemeTextColor = this.UseThemeTextColor, Description = this.Description };
    }
  }
}
