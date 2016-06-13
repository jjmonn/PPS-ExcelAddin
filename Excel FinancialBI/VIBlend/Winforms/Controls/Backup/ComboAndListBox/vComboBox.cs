// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vComboBox
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents vComboBox control</summary>
  [ToolboxBitmap(typeof (vComboBox), "ControlIcons.vComboBox.ico")]
  [DefaultEvent("SelectedIndexChanged")]
  [Designer("VIBlend.WinForms.Controls.Design.vListControlDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxItem(true)]
  [Description("Displays an editable text box, and a drop-down list of values.")]
  public class vComboBox : Control, IScrollableControlBase
  {
    private vDropDownBase dropDownBase = new vDropDownBase();
    private vSizingControl sizingControl = new vSizingControl();
    private int itemHeight = 17;
    private bool useDefaultDropDownWidth = true;
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private VIBLEND_THEME defaultScrollBarsTheme = VIBLEND_THEME.VISTABLUE;
    private bool useThemeDropDownArrowColor = true;
    private Color arrowColor = Color.Black;
    private bool useThemeBackColor = true;
    private Color borderColor = Color.Black;
    private bool useThemeBorderColor = true;
    private bool useThemeFont = true;
    private bool showGrip = true;
    private SizingDirection dropDownResizeDirection = SizingDirection.Both;
    private bool allowCloseDropDown = true;
    private PaintHelper paintHelper = new PaintHelper();
    private Brush disabledBackgroundBrush = (Brush) new SolidBrush(Color.Silver);
    private Brush backgroundBrush = (Brush) new SolidBrush(Color.WhiteSmoke);
    private Brush selectedBackgroundBrush = (Brush) new SolidBrush(Color.WhiteSmoke);
    private Brush highlightBackgroundBrush = (Brush) new SolidBrush(Color.WhiteSmoke);
    private bool useThemeBackground = true;
    private bool allowAnimations = true;
    private BackgroundElement backFill;
    private ControlState controlState;
    private vButtonEditBase buttonEdit;
    private int defaultWidth;
    private bool dropDownStyle;
    private vListBox listBox;
    private bool autoCompleteEnabled;
    private ControlTheme theme;
    private bool useThemeForeColor;
    private bool dropDownArrowBackgroundEnabled;
    private ImageList imageList;
    private AnimationManager manager;

    /// <summary>
    /// Gets or sets a value indicating whether to use the default drop down width.
    /// </summary>
    [DefaultValue(true)]
    [Description("Behavior")]
    [Category("Behavior")]
    public bool UseDefaultDropDownWidth
    {
      get
      {
        return this.useDefaultDropDownWidth;
      }
      set
      {
        this.useDefaultDropDownWidth = value;
        this.PerformLayout();
      }
    }

    [Category("Behavior")]
    public new bool TabStop
    {
      get
      {
        if (this.buttonEdit == null)
          return false;
        return this.buttonEdit.TextBox.TabStop;
      }
      set
      {
        if (this.buttonEdit == null)
          return;
        base.TabStop = value;
        this.buttonEdit.TabStop = value;
        this.buttonEdit.TextBox.TabStop = value;
        this.DropDown.TabStop = value;
        this.sizingControl.TabStop = value;
        this.ListBox.TabStop = value;
      }
    }

    /// <summary>
    /// Gets or sets the tab order of the control within its container.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The index value of the control within the set of controls within its container. The controls in the container are included in the tab order.
    /// </returns>
    [Category("Behavior")]
    public new int TabIndex
    {
      get
      {
        if (this.buttonEdit == null)
          return -1;
        return base.TabIndex;
      }
      set
      {
        if (this.buttonEdit == null)
          return;
        base.TabIndex = value;
        this.buttonEdit.TextBox.TabIndex = value;
      }
    }

    /// <summary>
    /// Gets or sets whether the combo box is a drop down list.
    /// </summary>
    [Description("Gets or sets whether the combo box is a drop down list.")]
    [Category("Behavior")]
    [DefaultValue(false)]
    public bool DropDownList
    {
      get
      {
        return this.dropDownStyle;
      }
      set
      {
        if (this.dropDownStyle == value)
          return;
        this.dropDownStyle = value;
        if (value)
          this.EditBase.ReadOnly = true;
        else
          this.EditBase.ReadOnly = false;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether auto complete is enabled.
    /// </summary>
    [Description("Gets or sets a value indicating whether auto complete is enabled.")]
    [Category("Behavior")]
    [DefaultValue(false)]
    public bool AutoCompleteEnabled
    {
      get
      {
        return this.autoCompleteEnabled;
      }
      set
      {
        this.autoCompleteEnabled = value;
        if (value)
          this.InitializeAutoCompleteValues();
        this.EditBase.TextBox.AutoCompleteEnabled = value;
      }
    }

    /// <summary>
    /// Indicates whether the control should draw right-to-left for RTL langauges
    /// </summary>
    [Category("Appearance")]
    [Browsable(true)]
    public override RightToLeft RightToLeft
    {
      get
      {
        return base.RightToLeft;
      }
      set
      {
        base.RightToLeft = value;
        this.listBox.RightToLeft = value;
        this.EditBase.RightToLeft = value;
        this.Refresh();
      }
    }

    /// <summary>Gets or sets the Theme of the control.</summary>
    [Browsable(false)]
    [Category("Appearance")]
    [Description("Gets or sets the Theme of the control.")]
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
        this.theme = value;
        this.listBox.Theme = this.theme;
        this.theme.Radius = 0.0f;
        this.backFill.LoadTheme(this.theme);
        this.SyncEditBoxAndDropDownColors();
        this.sizingControl.backFill.LoadTheme(this.theme);
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the theme of the control to one of the built-in themes.
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
        this.VIBlendScrollBarsTheme = value;
        if (this.listBox != null)
        {
          this.listBox.VIBlendTheme = value;
          this.listBox.VIBlendScrollBarsTheme = this.VIBlendScrollBarsTheme;
        }
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
    [Category("Appearance")]
    [Browsable(false)]
    public VIBLEND_THEME VIBlendScrollBarsTheme
    {
      get
      {
        return this.defaultScrollBarsTheme;
      }
      set
      {
        this.defaultScrollBarsTheme = value;
        if (this.listBox == null)
          return;
        this.listBox.VIBlendScrollBarsTheme = value;
      }
    }

    /// <summary>
    /// Gets or sets whether to use the Theme default arrow color.
    /// </summary>
    [Description("Gets or sets whether to use the Theme default arrow color")]
    [Browsable(true)]
    [Category("EditBox")]
    public bool UseThemeDropDownArrowColor
    {
      get
      {
        return this.useThemeDropDownArrowColor;
      }
      set
      {
        this.useThemeDropDownArrowColor = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the border color which is used when the UseThemeDropDownArrowColor property is false.
    /// </summary>
    [Category("EditBox")]
    [DefaultValue(typeof (Color), "Black")]
    [Description("Gets or sets the arrow color which is used when the UseThemeDropDownArrowColor property is false.")]
    public Color DropDownArrowColor
    {
      get
      {
        return this.arrowColor;
      }
      set
      {
        this.arrowColor = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets whether to use the Theme BackColor or the control's BackColor property.
    /// </summary>
    [Browsable(true)]
    [Description("Gets or sets whether to use the Theme BackColor or the control's BackColor property")]
    [DefaultValue(true)]
    [Category("EditBox")]
    public bool UseThemeBackColor
    {
      get
      {
        return this.useThemeBackColor;
      }
      set
      {
        this.useThemeBackColor = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the border color which is used when the UseThemeBorderColor property is false.
    /// </summary>
    [Category("EditBox")]
    [Description("Gets or sets the border color which is used when the UseThemeBorderColor property is false.")]
    [DefaultValue(typeof (Color), "Black")]
    public Color BorderColor
    {
      get
      {
        return this.borderColor;
      }
      set
      {
        this.borderColor = value;
        this.buttonEdit.OverrideBorderColor = this.BorderColor;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets whether to use the Theme BorderColor or the control's BackColor property.
    /// </summary>
    [Browsable(true)]
    [Description("Gets or sets whether to use the Theme BorderColor or the control's BackColor property")]
    [DefaultValue(true)]
    [Category("EditBox")]
    public bool UseThemeBorderColor
    {
      get
      {
        return this.useThemeBorderColor;
      }
      set
      {
        this.useThemeBorderColor = value;
        this.buttonEdit.UseThemeBorderColor = value;
        this.Refresh();
      }
    }

    /// <summary>
    /// Gets or sets whether to use the Theme BorderColor or the control's BackColor property.
    /// </summary>
    [Description("Gets or sets whether to use the Theme ForeColor or the control's ForeColor property")]
    [Browsable(true)]
    [DefaultValue(false)]
    [Category("EditBox")]
    public bool UseThemeForeColor
    {
      get
      {
        return this.useThemeForeColor;
      }
      set
      {
        this.useThemeForeColor = value;
        this.Refresh();
      }
    }

    /// <summary>
    /// Gets or sets whether to use the Theme Font or the control's Font property.
    /// </summary>
    [Description("Gets or sets whether to use the Theme Font or the control's Font property")]
    [Category("EditBox")]
    [DefaultValue(true)]
    [Browsable(true)]
    public bool UseThemeFont
    {
      get
      {
        return this.useThemeFont;
      }
      set
      {
        this.useThemeFont = value;
        this.Refresh();
      }
    }

    /// <summary>
    /// Determines whether to the drop-down arrow's background is painted
    /// </summary>
    [DefaultValue(false)]
    [Category("EditBox")]
    [Browsable(true)]
    public bool DropDownArrowBackgroundEnabled
    {
      get
      {
        return this.dropDownArrowBackgroundEnabled;
      }
      set
      {
        this.dropDownArrowBackgroundEnabled = value;
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
        return this.listBox.RoundedCornersMaskListItem;
      }
      set
      {
        this.listBox.RoundedCornersMaskListItem = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the rounded corners radius of the ListItems.
    /// </summary>
    [Description("Gets or sets the rounded corners radius of the ListItems.")]
    [DefaultValue(1)]
    [Category("Behavior")]
    public int RoundedCornersRadiusListItem
    {
      get
      {
        return this.listBox.RoundedCornersRadiusListItem;
      }
      set
      {
        if (value < 0)
          return;
        this.listBox.RoundedCornersRadiusListItem = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets a value indicating whether grip is shown</summary>
    [Browsable(false)]
    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether grip is shown")]
    public bool ShowGrip
    {
      get
      {
        return this.showGrip;
      }
      set
      {
        if (this.showGrip == value)
          return;
        this.showGrip = value;
        if (!value)
        {
          this.dropDownBase.Controls.Remove((Control) this.sizingControl);
          this.dropDownResizeDirection = SizingDirection.None;
        }
        else
        {
          this.dropDownBase.Controls.Add((Control) this.sizingControl);
          this.sizingControl.Size = new Size(0, 8);
          this.sizingControl.Dock = DockStyle.Bottom;
          if (this.dropDownResizeDirection != SizingDirection.None)
            return;
          this.dropDownResizeDirection = SizingDirection.Both;
        }
      }
    }

    /// <summary>
    /// Gets or sets a value indicating the possible drop-down resize directions
    /// </summary>
    [Description("Gets or sets a value indicating the drop-down resize directions")]
    [DefaultValue(true)]
    [Category("DropDown")]
    public SizingDirection DropDownResizeDirection
    {
      get
      {
        return this.dropDownResizeDirection;
      }
      set
      {
        if (this.dropDownResizeDirection == value)
          return;
        this.dropDownResizeDirection = value;
        if (value == SizingDirection.None && this.showGrip)
          this.ShowGrip = false;
        if (this.dropDownResizeDirection != SizingDirection.None && !this.showGrip)
          this.ShowGrip = true;
        this.sizingControl.SizingDirection = this.dropDownResizeDirection;
        this.sizingControl.Refresh();
      }
    }

    /// <summary>Gets or sets the background color for the control.</summary>
    [Browsable(true)]
    [Category("EditBox")]
    public override Color BackColor
    {
      get
      {
        return base.BackColor;
      }
      set
      {
        base.BackColor = value;
        this.SyncEditBoxAndDropDownColors();
      }
    }

    /// <summary>
    /// Gets or sets the foreground color which is used to display the text
    /// </summary>
    [Category("EditBox")]
    [Browsable(true)]
    public override Color ForeColor
    {
      get
      {
        return base.ForeColor;
      }
      set
      {
        base.ForeColor = value;
        this.Refresh();
      }
    }

    /// <summary>
    /// Gets or sets the font that is used to display the text in the editbox of the control
    /// </summary>
    [Browsable(true)]
    [Category("EditBox")]
    public override Font Font
    {
      get
      {
        return base.Font;
      }
      set
      {
        base.Font = value;
        this.Refresh();
      }
    }

    /// <summary>
    /// Gets or sets the text displayed in the editbox of the control
    /// </summary>
    [Category("EditBox")]
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
        this.Refresh();
      }
    }

    /// <summary>Gets or sets the background image of the control</summary>
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
      }
    }

    /// <summary>Gets or sets the images list of the vComboBox</summary>
    [Description("Gets or sets the images list of the vComboBox")]
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
        this.listBox.ImageList = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets the drop down.</summary>
    /// <value>The drop down.</value>
    [Browsable(false)]
    public vDropDownBase DropDown
    {
      get
      {
        return this.dropDownBase;
      }
    }

    /// <summary>Gets the edit base.</summary>
    /// <value>The edit base.</value>
    [Browsable(false)]
    public vButtonEditBase EditBase
    {
      get
      {
        return this.buttonEdit;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to draw default text
    /// </summary>
    [Browsable(true)]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether to draw default text")]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Category("Appearance")]
    public bool ShowDefaultText
    {
      get
      {
        return this.EditBase.TextBox.ShowDefaultText;
      }
      set
      {
        this.EditBase.TextBox.ShowDefaultText = value;
      }
    }

    /// <summary>Gets or sets the default text.</summary>
    /// <value>The default text.</value>
    [Category("Appearance")]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(true)]
    [Description("Gets or sets the default text.")]
    [DefaultValue("Select Item...")]
    public string DefaultText
    {
      get
      {
        return this.EditBase.TextBox.DefaultText;
      }
      set
      {
        this.EditBase.TextBox.DefaultText = value;
      }
    }

    /// <summary>Gets or sets the default color of the text.</summary>
    /// <value>The default color of the text.</value>
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Category("Appearance")]
    [Description("Gets or sets the default color of the text.")]
    [DefaultValue(typeof (Color), "Gray")]
    public Color DefaultTextColor
    {
      get
      {
        return this.EditBase.TextBox.DefaultTextColor;
      }
      set
      {
        this.EditBase.TextBox.DefaultTextColor = value;
      }
    }

    /// <summary>Gets or sets the selected value.</summary>
    /// <value>The selected value.</value>
    [Description("Gets or sets the selected value")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Behavior")]
    [Browsable(false)]
    [DefaultValue(null)]
    [Bindable(true)]
    public object SelectedValue
    {
      get
      {
        return this.listBox.SelectedValue;
      }
      set
      {
        this.listBox.SelectedValue = value;
      }
    }

    /// <summary>Gets or sets the DataSource of the vComboBox</summary>
    [Description("Gets or sets the DataSource of the vComboBox")]
    [AttributeProvider(typeof (IListSource))]
    [DefaultValue(null)]
    [Category("Behavior")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public object DataSource
    {
      get
      {
        return this.listBox.DataSource;
      }
      set
      {
        this.listBox.BindingContext = this.BindingContext;
        this.listBox.DataSource = value;
      }
    }

    /// <summary>Gets the vListBox inside the vComboBox.</summary>
    [Description("Gets the vListBox inside the vComboBox.")]
    [Browsable(false)]
    public vListBox ListBox
    {
      get
      {
        return this.listBox;
      }
      set
      {
        if (value == null || this.listBox == null)
          return;
        this.dropDownBase.Controls.Clear();
        this.listBox.MouseDown -= new MouseEventHandler(this.OnListBoxMouseDown);
        this.listBox.MouseEnter -= new EventHandler(this.OnListBoxMouseEnter);
        this.listBox.MouseLeave -= new EventHandler(this.OnListBoxMouseLeave);
        this.listBox.SelectedIndexChanged -= new EventHandler(this.OnListBoxSelectedIndexChanged);
        this.listBox.SelectedValueChanged -= new EventHandler(this.OnListBoxSelectedValueChanged);
        this.listBox.SelectedItemChanged -= new EventHandler(this.listBox_SelectedItemChanged);
        this.listBox.SelectedItemChanging -= new EventHandler<ListItemChangingEventArgs>(this.listBox_SelectedItemChanging);
        this.listBox = value;
        this.listBox.Dock = DockStyle.Fill;
        this.dropDownBase.Controls.Add((Control) this.listBox);
        this.dropDownBase.Controls.Add((Control) this.sizingControl);
        this.listBox.MouseDown += new MouseEventHandler(this.OnListBoxMouseDown);
        this.listBox.MouseEnter += new EventHandler(this.OnListBoxMouseEnter);
        this.listBox.MouseLeave += new EventHandler(this.OnListBoxMouseLeave);
        this.listBox.SelectedIndexChanged += new EventHandler(this.OnListBoxSelectedIndexChanged);
        this.listBox.SelectedValueChanged += new EventHandler(this.OnListBoxSelectedValueChanged);
        this.listBox.SelectedItemChanged += new EventHandler(this.listBox_SelectedItemChanged);
        this.listBox.SelectedItemChanging += new EventHandler<ListItemChangingEventArgs>(this.listBox_SelectedItemChanging);
      }
    }

    /// <summary>
    /// Gets or sets the display member associated to the vComboBox.
    /// </summary>
    [Category("Behavior")]
    [Browsable(false)]
    [Description("Gets or sets the display member associated to the vComboBox.")]
    public string DisplayMember
    {
      get
      {
        return this.listBox.DisplayMember;
      }
      set
      {
        this.listBox.DisplayMember = value;
      }
    }

    /// <summary>
    /// Gets or sets a string that specifies the property of the data source from which to draw the value.
    /// </summary>
    [Browsable(false)]
    [Description("Gets or sets a string that specifies the property of the data source from which to draw the value.")]
    [Category("Behavior")]
    public string ValueMember
    {
      get
      {
        return this.listBox.ValueMember;
      }
      set
      {
        this.listBox.ValueMember = value;
      }
    }

    /// <summary>Gets or sets the disabled background brush.</summary>
    /// <value>The disabled background brush.</value>
    [Description("Gets or sets the disabled background brush.")]
    [Browsable(false)]
    [Category("DropDownButton.Appearance")]
    public Brush DropDownButtonDisabledBackgroundBrush
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

    /// <summary>Gets or sets the background brush.</summary>
    /// <value>The background brush.</value>
    [Category("DropDownButton.Appearance")]
    [Browsable(false)]
    [Description("Gets or sets the background brush.")]
    public Brush DropDownButtonBackgroundBrush
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
    [Category("DropDownButton.Appearance")]
    [Browsable(false)]
    [Description("Gets or sets the pressed background brush.")]
    public Brush DropDownButtonPressedBackgroundBrush
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
    [Browsable(false)]
    [Category("DropDownButton.Appearance")]
    [Description("Gets or sets the HighlightBackground brush.")]
    public Brush DropDownButtonHighlightBackgroundBrush
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
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether to use theme's background")]
    public bool UseDropDownButtonThemeBackground
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
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the height in pixels of the drop-down portion of the ComboBox
    /// </summary>
    [DefaultValue(150)]
    [Description("Gets or sets the height in pixels of the drop-down portion of the ComboBox")]
    [Category("DropDown")]
    public int DropDownHeight
    {
      get
      {
        return this.dropDownBase.PreferredSize.Height;
      }
      set
      {
        if (value < 0)
          return;
        this.dropDownBase.PreferredSize = new Size(this.dropDownBase.PreferredSize.Width, value);
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the width in pixels of the drop-down portion of the ComboBox
    /// </summary>
    [DefaultValue(100)]
    [Category("DropDown")]
    [Description("Gets or sets the width in pixels of the drop-down portion of the ComboBox")]
    public int DropDownWidth
    {
      get
      {
        return this.dropDownBase.PreferredSize.Width;
      }
      set
      {
        if (value < 0)
          return;
        this.dropDownBase.PreferredSize = new Size(value, this.dropDownBase.PreferredSize.Height);
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the maximum size of the drop-down portion of the ComboBox
    /// </summary>
    [Description("Gets or sets the width in pixels of the drop-down portion of the ComboBox")]
    [Category("DropDown")]
    public Size DropDownMaximumSize
    {
      get
      {
        return this.dropDownBase.MaximumSize;
      }
      set
      {
        this.dropDownBase.MaximumSize = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the maximum size of the drop-down portion of the ComboBox
    /// </summary>
    [Description("Gets or sets the width in pixels of the drop-down portion of the ComboBox")]
    [Category("DropDown")]
    public Size DropDownMinimumSize
    {
      get
      {
        return this.dropDownBase.MinimumSize;
      }
      set
      {
        this.dropDownBase.MinimumSize = value;
        this.Invalidate();
      }
    }

    [Localizable(true)]
    [DefaultValue(2)]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("Behavior")]
    [Description("Gets or sets spacing in pixels between the vComboBox items.")]
    public int ItemsSpacing
    {
      get
      {
        return this.listBox.ItemsSpacing;
      }
      set
      {
        this.listBox.ItemsSpacing = value;
      }
    }

    /// <summary>
    /// Gets or sets the height of the items inside the dropdown list of the vComboBox
    /// </summary>
    [Localizable(true)]
    [Category("Behavior")]
    [Description("Gets or sets the height of the items inside the dropdown list of the vComboBox")]
    [DefaultValue(17)]
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
        this.listBox.ItemHeight = value;
      }
    }

    /// <summary>Gets the ComboBox Items collection.</summary>
    [Description("Gets the ComboBox Items collection.")]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Localizable(true)]
    public ListItemsCollection Items
    {
      get
      {
        return this.listBox.Items;
      }
    }

    /// <summary>
    /// Gets or sets the currently selected item of the drop down list of the combo box control.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Bindable(true)]
    public ListItem SelectedItem
    {
      get
      {
        return this.listBox.SelectedItem;
      }
      set
      {
        this.listBox.SelectedItem = value;
        this.OnSelectedIndexChanged(EventArgs.Empty);
      }
    }

    /// <summary>
    /// Gets or sets the index of the currently selected item. Returns -1 if there is no item selected
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets the index of the currently selected item. Returns -1 if there is no item selected")]
    [DefaultValue(-1)]
    public int SelectedIndex
    {
      get
      {
        return this.listBox.SelectedIndex;
      }
      set
      {
        if (value < -1 || value >= this.listBox.Items.Count)
          return;
        this.listBox.SelectedIndex = value;
        if (value == -1)
          this.Text = "";
        this.Invalidate();
        this.OnSelectedIndexChanged(EventArgs.Empty);
      }
    }

    /// <exclude />
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

    /// <summary>Determines whether to use animations</summary>
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

    /// <summary>Occurs when the mouse is clicked over a ListItem.</summary>
    [Description("Occurs when the mouse is clicked over a ListItem.")]
    [Category("Action")]
    public event EventHandler<ListItemMouseEventArgs> ItemMouseDown;

    /// <summary>Occurs when the mouse is clicked over a ListItem.</summary>
    [Category("Action")]
    [Description("Occurs when the mouse is clicked over a ListItem.")]
    public event EventHandler<ListItemMouseEventArgs> ItemMouseUp;

    /// <summary>occurs when the selection is changed.</summary>
    [Category("Action")]
    [Description("Occurs when the selection is changed.")]
    public event EventHandler SelectedItemChanged;

    /// <summary>Occurs when the selection is changing.</summary>
    [Description("Occurs when the selection is changing.")]
    [Category("Action")]
    public event EventHandler<ListItemChangingEventArgs> SelectedItemChanging;

    /// <summary>Occurs when the selected index has changed</summary>
    [Category("Action")]
    public event EventHandler SelectedIndexChanged;

    /// <summary>Occurs when the SelectedValue has changed.</summary>
    [Category("Action")]
    [Description("Occurs when the SelectedValue has changed.")]
    public event EventHandler SelectedValueChanged;

    [Category("Action")]
    [Description("Occurs when the DropDown is closed.")]
    public event EventHandler DropDownClose;

    /// <summary>Occurs when the DropDown is opened.</summary>
    [Category("Action")]
    [Description("Occurs when the DropDown is opened.")]
    public event EventHandler DropDownOpen;

    static vComboBox()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>Constructor</summary>
    public vComboBox()
    {
      this.listBox = new vListBox();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.Selectable, false);
      this.listBox.Dock = DockStyle.Fill;
      this.dropDownBase.Controls.Add((Control) this.listBox);
      this.dropDownBase.Controls.Add((Control) this.sizingControl);
      this.DropDownHeight = this.DropDownWidth = 150;
      this.DropDownMinimumSize = new Size(10, 10);
      this.DropDownMaximumSize = new Size(1000, 1000);
      this.sizingControl.Size = new Size(0, 8);
      this.sizingControl.Dock = DockStyle.Bottom;
      this.BackColor = Color.White;
      this.UseThemeBackColor = false;
      this.buttonEdit = new vButtonEditBase();
      this.buttonEdit.VButtonWidth = 13;
      this.buttonEdit.Dock = DockStyle.Fill;
      this.buttonEdit.TextBox.Click += new EventHandler(this.TextBox_Click);
      this.buttonEdit.TextBox.DoubleClick += new EventHandler(this.TextBox_DoubleClick);
      this.buttonEdit.TextBox.MouseHover += new EventHandler(this.TextBox_MouseHover);
      this.buttonEdit.TextBox.MouseMove += new MouseEventHandler(this.TextBox_MouseMove);
      this.buttonEdit.TextBox.MouseEnter += new EventHandler(this.TextBox_MouseEnter);
      this.buttonEdit.TextBox.MouseLeave += new EventHandler(this.TextBox_MouseLeave);
      this.buttonEdit.TextBox.SizeChanged += new EventHandler(this.TextBox_SizeChanged);
      this.buttonEdit.TextBox.TextChanged += new EventHandler(this.TextBox_TextChanged);
      this.buttonEdit.TextBox.KeyDown += new KeyEventHandler(this.TextBox_KeyDown);
      this.buttonEdit.TextBox.KeyPress += new KeyPressEventHandler(this.TextBox_KeyPress);
      this.buttonEdit.TextBox.KeyUp += new KeyEventHandler(this.TextBox_KeyUp);
      this.backFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this.buttonEdit);
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
      this.Controls.Add((Control) this.buttonEdit);
      this.buttonEdit.PaintEditBox += new PaintEventHandler(this.OnButtonEditPaint);
      this.buttonEdit.TextChanged += new EventHandler(this.OnEditBoxTextChanged);
      this.buttonEdit.MouseDown += new MouseEventHandler(this.OnButtonEditMouseDown);
      this.buttonEdit.MouseUp += new MouseEventHandler(this.buttonEdit_MouseUp);
      this.buttonEdit.MouseMove += new MouseEventHandler(this.OnButtonEditMouseMove);
      this.buttonEdit.MouseWheel += new MouseEventHandler(this.OnButtonEditMouseWheel);
      this.buttonEdit.MouseEnter += new EventHandler(this.OnButtonEditMouseEnter);
      this.buttonEdit.MouseLeave += new EventHandler(this.OnButtonEditMouseLeave);
      this.buttonEdit.MouseClick += new MouseEventHandler(this.buttonEdit_MouseClick);
      this.buttonEdit.KeyUp += new KeyEventHandler(this.buttonEdit_KeyUp);
      this.listBox.ItemMouseUp += new EventHandler<ListItemMouseEventArgs>(this.listBox_ItemMouseUp);
      this.listBox.ItemMouseDown += new EventHandler<ListItemMouseEventArgs>(this.listBox_ItemMouseDown);
      this.listBox.MouseDown += new MouseEventHandler(this.OnListBoxMouseDown);
      this.listBox.MouseUp += new MouseEventHandler(this.listBox_MouseUp);
      this.listBox.MouseEnter += new EventHandler(this.OnListBoxMouseEnter);
      this.listBox.MouseLeave += new EventHandler(this.OnListBoxMouseLeave);
      this.listBox.SelectedIndexChanged += new EventHandler(this.OnListBoxSelectedIndexChanged);
      this.listBox.SelectedValueChanged += new EventHandler(this.OnListBoxSelectedValueChanged);
      this.listBox.SelectedItemChanged += new EventHandler(this.listBox_SelectedItemChanged);
      this.listBox.SelectedItemChanging += new EventHandler<ListItemChangingEventArgs>(this.listBox_SelectedItemChanging);
      this.dropDownBase.DropDownOpen += new EventHandler(this.OnDropDownOpenPopup);
      this.dropDownBase.DropDownClose += new EventHandler(this.OnDropDownClosePopup);
      this.SizeChanged += new EventHandler(this.OnSizeChanged);
      this.TextChanged += new EventHandler(this.OnTextChanged);
      this.VisibleChanged += new EventHandler(this.OnVisibleChanged);
      this.Items.CollectionChanged += new EventHandler(this.Items_CollectionChanged);
      this.LostFocus += new EventHandler(this.vComboBox_LostFocus);
      this.dropDownBase.DropDownClose += new EventHandler(this.dropDownBase_DropDownClose);
      this.buttonEdit.TextBox.DefaultText = "Select Item...";
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

    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      this.defaultWidth = this.Width;
      if (!this.UseDefaultDropDownWidth)
        return;
      this.DropDownWidth = this.defaultWidth;
    }

    /// <summary>Closes the drop down.</summary>
    public void CloseDropDown()
    {
      this.DropDown.CloseControl();
    }

    private void listBox_ItemMouseUp(object sender, ListItemMouseEventArgs e)
    {
      this.OnItemMouseUp(e);
    }

    private void listBox_ItemMouseDown(object sender, ListItemMouseEventArgs e)
    {
      this.OnItemMouseDown(e);
    }

    private void listBox_MouseUp(object sender, MouseEventArgs e)
    {
      if (!this.allowCloseDropDown)
        return;
      this.dropDownBase.CloseControl();
      this.Refresh();
    }

    private void buttonEdit_MouseUp(object sender, MouseEventArgs e)
    {
      this.OnMouseUp(e);
    }

    private void buttonEdit_MouseClick(object sender, MouseEventArgs e)
    {
      this.OnMouseClick(e);
    }

    private void listBox_SelectedItemChanging(object sender, ListItemChangingEventArgs e)
    {
      this.OnSelectedItemChanging(e);
    }

    private void listBox_SelectedItemChanged(object sender, EventArgs e)
    {
      this.OnSelectedItemChanged();
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

    private void TextBox_GotFocus(object sender, EventArgs e)
    {
      this.OnGotFocus(e);
    }

    private void TextBox_LostFocus(object sender, EventArgs e)
    {
      this.OnLostFocus(e);
    }

    private void TextBox_MouseEnter(object sender, EventArgs e)
    {
      this.OnMouseEnter(e);
    }

    private void TextBox_MouseLeave(object sender, EventArgs e)
    {
      this.OnMouseLeave(e);
    }

    private void TextBox_SizeChanged(object sender, EventArgs e)
    {
      this.OnSizeChanged(e);
    }

    private void TextBox_TextChanged(object sender, EventArgs e)
    {
    }

    private void TextBox_Click(object sender, EventArgs e)
    {
      this.OnClick(e);
    }

    private void TextBox_DoubleClick(object sender, EventArgs e)
    {
      this.OnDoubleClick(e);
    }

    private void TextBox_KeyDown(object sender, KeyEventArgs e)
    {
      this.OnKeyDown(e);
    }

    private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
    {
      this.OnKeyPress(e);
    }

    private void TextBox_KeyUp(object sender, KeyEventArgs e)
    {
      this.OnKeyUp(e);
    }

    private void TextBox_MouseDown(object sender, MouseEventArgs e)
    {
      this.OnMouseDown(e);
    }

    private void TextBox_MouseHover(object sender, EventArgs e)
    {
      this.OnMouseHover(e);
    }

    private void TextBox_MouseMove(object sender, MouseEventArgs e)
    {
      this.OnMouseMove(e);
    }

    private void TextBox_MouseUp(object sender, MouseEventArgs e)
    {
      this.OnMouseUp(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
      if (e.Alt && e.KeyCode == Keys.Up || e.KeyCode == Keys.Escape)
      {
        this.DropDown.CloseControl();
        this.listBox.Capture = false;
      }
      else
      {
        if (e.KeyCode != Keys.F4 && (!e.Alt || e.KeyCode != Keys.Down) || this.buttonEdit == null)
          return;
        Point location = this.buttonEdit.PerformBoundsCalculations(this.ClientRectangle).Location;
        this.OnButtonEditMouseDown((object) this, new MouseEventArgs(MouseButtons.Left, 1, location.X, location.Y, 0));
      }
    }

    private void RefreshAutoCompleteItems()
    {
      if (!this.AutoCompleteEnabled)
        return;
      this.InitializeAutoCompleteValues();
    }

    private void InitializeAutoCompleteValues()
    {
      List<string> stringList = new List<string>();
      for (int index = 0; index < this.Items.Count; ++index)
        stringList.Add(this.Items[index].Text);
      this.EditBase.TextBox.AutoCompleteValues = stringList;
    }

    private void dropDownBase_DropDownClose(object sender, EventArgs e)
    {
      this.Refresh();
    }

    private void vComboBox_LostFocus(object sender, EventArgs e)
    {
      this.listBox.Capture = false;
    }

    private void SyncEditBoxAndDropDownColors()
    {
      if (this.buttonEdit != null)
      {
        if (this.buttonEdit.Theme != this.theme)
          this.buttonEdit.Theme = this.theme;
        this.buttonEdit.OverrideBackColor = this.BackColor;
        this.buttonEdit.OverrideForeColor = this.ForeColor;
        this.buttonEdit.OverrideBorderColor = this.BorderColor;
        this.buttonEdit.OverrideFont = this.Font;
        this.buttonEdit.UseThemeBackColor = this.useThemeBackColor;
        this.buttonEdit.UseThemeBorderColor = this.useThemeBorderColor;
        this.buttonEdit.UseThemeFont = this.useThemeFont;
        this.buttonEdit.UseThemeForeColor = this.useThemeForeColor;
      }
      if (this.listBox == null)
        return;
      if (this.listBox.Theme != this.theme)
        this.listBox.Theme = this.theme;
      this.listBox.Theme = this.theme;
      this.listBox.BackColor = this.BackColor;
      this.listBox.UseThemeBackColor = this.useThemeBackColor;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (!this.DesignMode)
        Licensing.LICheck((Control) this);
      this.SyncEditBoxAndDropDownColors();
      if (this.buttonEdit != null)
        this.buttonEdit.Enabled = this.Enabled;
      if (this.dropDownBase != null)
        this.dropDownBase.Enabled = this.Enabled;
      base.OnPaint(e);
    }

    private void buttonEdit_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyValue == 13)
        this.AutoSelectIndex();
      if (e.KeyCode != Keys.Escape || !this.AutoCompleteEnabled)
        return;
      this.listBox.hotTrackIndex = -1;
      this.listBox.EnsureVisible(this.SelectedIndex);
      this.listBox.Invalidate();
      this.Text = this.Items[this.SelectedIndex].Text;
    }

    private void AutoSelectIndex()
    {
      if (!this.AutoCompleteEnabled)
        return;
      int autoCompleteValues = this.buttonEdit.TextBox.GetIndexInAutoCompleteValues(this.buttonEdit.Text);
      if (autoCompleteValues <= -1)
        return;
      this.SelectedIndex = autoCompleteValues;
    }

    private void AutoTrackIndex()
    {
      if (!this.AutoCompleteEnabled)
        return;
      int autoCompleteValues = this.buttonEdit.TextBox.GetIndexInAutoCompleteValues(this.buttonEdit.Text);
      if (autoCompleteValues <= -1)
        return;
      this.listBox.hotTrackIndex = autoCompleteValues;
      this.listBox.EnsureVisible(autoCompleteValues);
      this.listBox.Invalidate();
    }

    private void OnButtonEditMouseWheel(object sender, MouseEventArgs e)
    {
      if (!this.listBox.vscroll.Visible)
        return;
      if (e.Delta > 0)
        this.listBox.vscroll.Value -= this.listBox.ItemHeight;
      else
        this.listBox.vscroll.Value += this.listBox.ItemHeight;
    }

    private void OnListBoxMouseDown(object sender, MouseEventArgs e)
    {
      this.allowCloseDropDown = true;
    }

    /// <summary>This member overrides Control.ProcessCmdKey.</summary>
    /// <param name="msg">A Message, passed by reference, that represents the window message to process. </param>
    /// <param name="keyData">One of the Keys values that represents the key to process. </param>
    /// <returns>true if the character was processed by the control; otherwise, false. </returns>
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (keyData == Keys.Escape || keyData == Keys.Return)
        return base.ProcessCmdKey(ref msg, keyData);
      if (keyData == Keys.Tab)
        this.AutoSelectIndex();
      if (this.listBox == null)
        return true;
      this.allowCloseDropDown = false;
      return this.listBox.CallProcessCmdKey(ref msg, keyData);
    }

    private void Items_CollectionChanged(object sender, EventArgs e)
    {
      this.Invalidate();
      this.RefreshAutoCompleteItems();
    }

    /// <summary>Raises the SelectedValueChanged event.</summary>
    /// <param name="e">An EventArgs that contains the event data. </param>
    protected virtual void OnSelectedValueChanged(EventArgs e)
    {
      if (this.SelectedValueChanged == null)
        return;
      this.SelectedValueChanged((object) this, e);
    }

    private void OnListBoxSelectedValueChanged(object sender, EventArgs e)
    {
      this.OnSelectedValueChanged(e);
    }

    private void OnButtonEditMouseLeave(object sender, EventArgs e)
    {
      this.controlState = ControlState.Normal;
      this.Invalidate();
    }

    private void OnButtonEditMouseMove(object sender, MouseEventArgs e)
    {
      if (!this.buttonEdit.PerformBoundsCalculations(this.ClientRectangle).Contains(e.Location))
        return;
      this.controlState = ControlState.Hover;
      this.Invalidate();
    }

    private void OnButtonEditMouseEnter(object sender, EventArgs e)
    {
    }

    private void OnVisibleChanged(object sender, EventArgs e)
    {
      if (this.Visible)
        return;
      this.dropDownBase.CloseControl();
      this.listBox.Capture = false;
    }

    private void OnTextChanged(object sender, EventArgs e)
    {
      if (!(this.buttonEdit.Text != this.Text))
        return;
      this.buttonEdit.Text = this.Text;
    }

    private void OnSizeChanged(object sender, EventArgs e)
    {
      this.dropDownBase.PreferredSize = new Size(this.dropDownBase.Size.Width, this.dropDownBase.Size.Height);
    }

    private void OnEditBoxTextChanged(object sender, EventArgs e)
    {
      this.AutoTrackIndex();
      this.Text = this.buttonEdit.Text;
    }

    private void OnListBoxSelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.listBox.SelectedItem != null)
        this.buttonEdit.Text = this.listBox.SelectedItem.Text;
      this.OnSelectedIndexChanged(e);
    }

    private void OnListBoxMouseEnter(object sender, EventArgs e)
    {
    }

    private void OnListBoxMouseLeave(object sender, EventArgs e)
    {
    }

    private void OnDropDownOpenPopup(object sender, EventArgs e)
    {
      this.listBox.InitializeScrollBars();
      this.listBox.EnsureVisible(this.listBox.SelectedIndex);
      this.listBox.Invalidate();
      this.OnDropDownOpened();
    }

    private void OnDropDownClosePopup(object sender, EventArgs e)
    {
      this.listBox.Capture = false;
      this.OnDropDownClosed();
    }

    private void OnButtonEditMouseDown(object sender, MouseEventArgs e)
    {
      if (!this.ClientRectangle.Contains(e.Location))
        return;
      this.ShowDropDown(e.Location);
      this.OnMouseDown(e);
    }

    /// <summary>Shows the drop down.</summary>
    /// <param name="location">The location.</param>
    public virtual void ShowDropDown(Point location)
    {
      if (this.buttonEdit.PerformBoundsCalculations(this.ClientRectangle).Contains(location))
      {
        this.buttonEdit.Focus();
        this.controlState = ControlState.Pressed;
        if (this.dropDownBase.GetShowDirection(ArrowDirection.Down, new Point(this.PointToScreen(Point.Empty).X, this.PointToScreen(Point.Empty).Y + this.Size.Height)) == ArrowDirection.Down)
        {
          this.dropDownBase.OpenControl(ArrowDirection.Down, new Point(this.PointToScreen(Point.Empty).X, this.PointToScreen(Point.Empty).Y + this.Size.Height));
          if (this.ShowGrip && this.sizingControl != null)
          {
            this.sizingControl.Dock = DockStyle.Bottom;
            this.sizingControl.SizingDirection = SizingDirection.Both;
          }
        }
        else
        {
          Rectangle screenRect;
          Rectangle popupRect;
          this.dropDownBase.OpenControl(ArrowDirection.Up, this.dropDownBase.CalculateShowLocation(ArrowDirection.Up, new Point(this.PointToScreen(Point.Empty).X, this.PointToScreen(Point.Empty).Y), out screenRect, out popupRect));
          if (this.ShowGrip && this.sizingControl != null)
          {
            this.sizingControl.Dock = DockStyle.Top;
            this.sizingControl.SizingDirection = SizingDirection.Horizontal;
          }
        }
      }
      if (this.FindForm() != null)
      {
        this.FindForm().LocationChanged -= new EventHandler(this.vComboBox_LocationChanged);
        this.FindForm().LocationChanged += new EventHandler(this.vComboBox_LocationChanged);
        if (this.FindForm().ParentForm != null)
        {
          this.FindForm().ParentForm.LocationChanged -= new EventHandler(this.vComboBox_LocationChanged);
          this.FindForm().ParentForm.LocationChanged += new EventHandler(this.vComboBox_LocationChanged);
        }
      }
      else if (this.Parent != null)
      {
        this.Parent.LocationChanged -= new EventHandler(this.Parent_LocationChanged);
        this.Parent.LocationChanged += new EventHandler(this.Parent_LocationChanged);
      }
      this.buttonEdit.Invalidate();
      this.Invalidate();
    }

    /// <summary>Called when the drop down is closed.</summary>
    protected virtual void OnDropDownClosed()
    {
      if (this.DropDownClose == null)
        return;
      this.DropDownClose((object) this, EventArgs.Empty);
    }

    /// <summary>Called when the drop down is opened.</summary>
    protected virtual void OnDropDownOpened()
    {
      if (this.DropDownOpen == null)
        return;
      this.DropDownOpen((object) this, EventArgs.Empty);
    }

    private void Parent_LocationChanged(object sender, EventArgs e)
    {
      this.DropDown.CloseControl();
      this.Refresh();
    }

    private void vComboBox_LocationChanged(object sender, EventArgs e)
    {
      this.DropDown.CloseControl();
      this.Refresh();
    }

    private void OnButtonEditPaint(object sender, PaintEventArgs e)
    {
      this.DrawArrow(e);
    }

    /// <summary>Draws the arrow.</summary>
    /// <param name="e">The <see cref="T:System.Windows.Forms.PaintEventArgs" /> instance containing the event data.</param>
    protected virtual void DrawArrow(PaintEventArgs e)
    {
      Rectangle clientRectangle = this.ClientRectangle;
      --clientRectangle.Width;
      --clientRectangle.Height;
      Rectangle rectangle = this.buttonEdit.PerformBoundsCalculations(clientRectangle);
      this.backFill.Bounds = new Rectangle(this.Width - rectangle.Width - 1, 1, rectangle.Width, this.Height - 2);
      if (this.RightToLeft == RightToLeft.Yes)
        this.backFill.Bounds = new Rectangle(0, 0, rectangle.Width, this.Height);
      bool flag1 = false;
      bool flag2 = false;
      this.backFill.EnableSmoothingMode = false;
      if (this.dropDownArrowBackgroundEnabled || !this.dropDownArrowBackgroundEnabled && (this.controlState == ControlState.Hover || this.dropDownBase.Visible))
      {
        if (this.UseDropDownButtonThemeBackground)
        {
          if (this.dropDownBase.Visible)
            this.backFill.DrawElementFill(e.Graphics, ControlState.Pressed);
          else
            this.backFill.DrawElementFill(e.Graphics, this.controlState);
        }
        else if (this.DropDownButtonBackgroundBrush != null && !this.dropDownBase.Visible)
        {
          if (!this.Enabled)
            e.Graphics.FillRectangle(this.DropDownButtonDisabledBackgroundBrush, this.backFill.Bounds);
          else if (this.controlState != ControlState.Hover)
            e.Graphics.FillRectangle(this.DropDownButtonBackgroundBrush, this.backFill.Bounds);
          else
            e.Graphics.FillRectangle(this.DropDownButtonHighlightBackgroundBrush, this.backFill.Bounds);
        }
        else if (this.dropDownBase.Visible && this.DropDownButtonPressedBackgroundBrush != null)
          e.Graphics.FillRectangle(this.DropDownButtonPressedBackgroundBrush, this.backFill.Bounds);
        flag1 = false;
        flag2 = true;
      }
      if (!flag1 && (this.ClientRectangle.Contains(this.PointToClient(Cursor.Position)) && this.Enabled))
      {
        if (this.UseDropDownButtonThemeBackground)
        {
          if (this.dropDownBase.Visible)
            this.backFill.DrawElementFill(e.Graphics, ControlState.Pressed);
          else
            this.backFill.DrawElementFill(e.Graphics, ControlState.Normal);
        }
        else if (this.RectangleToScreen(this.backFill.Bounds).Contains(Cursor.Position))
          e.Graphics.FillRectangle(this.DropDownButtonHighlightBackgroundBrush, this.backFill.Bounds);
        else if (this.DropDownButtonBackgroundBrush != null)
          e.Graphics.FillRectangle(this.DropDownButtonBackgroundBrush, this.backFill.Bounds);
        flag2 = true;
      }
      this.backFill.EnableSmoothingMode = true;
      Color color1 = this.theme.QueryColorSetter("DropDownArrowColor");
      if (color1 == Color.Empty)
        color1 = this.backFill.BorderColor;
      if (!flag2)
      {
        Color color2 = this.theme.QueryColorSetter("DropDownArrowWithoutFillColor");
        if (!color2.IsEmpty)
          color1 = color2;
      }
      if (this.controlState == ControlState.Hover)
      {
        color1 = this.theme.QueryColorSetter("DropDownArrowColorHighlight");
        if (color1 == Color.Empty)
          color1 = this.backFill.HighlightBorderColor;
      }
      if (this.controlState == ControlState.Pressed || this.dropDownBase.Visible)
      {
        color1 = this.theme.QueryColorSetter("DropDownArrowColorSelected");
        if (color1 == Color.Empty)
          color1 = this.theme.QueryColorSetter("DropDownArrowColorHighlight");
        if (color1 == Color.Empty)
          color1 = this.backFill.PressedBorderColor;
      }
      if (!this.Enabled)
      {
        color1 = this.theme.QueryColorSetter("DropDownArrowColorDisabled");
        if (color1 == Color.Empty)
          color1 = this.backFill.DisabledBorderColor;
      }
      if (!this.UseThemeDropDownArrowColor)
        color1 = this.DropDownArrowColor;
      Rectangle bounds = PaintHelper.OfficeArrowRectFromBounds(this.backFill.Bounds);
      this.paintHelper.DrawArrowFigure(e.Graphics, color1, bounds, ArrowDirection.Down);
    }

    /// <summary>Raises the SelectedIndexChanged event</summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnSelectedIndexChanged(EventArgs e)
    {
      if (this.SelectedIndexChanged == null)
        return;
      this.SelectedIndexChanged((object) this, e);
    }
  }
}
