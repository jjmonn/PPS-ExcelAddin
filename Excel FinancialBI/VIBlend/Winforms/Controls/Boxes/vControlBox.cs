// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vControlBox
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
  /// <summary>Represents a vControlBox control</summary>
  /// <remarks>
  /// A vControlBox displays an editable text box, and a drop-down container that can host other controls and components.
  /// </remarks>
  [Designer("VIBlend.WinForms.Controls.Design.vDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxBitmap(typeof (vControlBox), "ControlIcons.ControlBox.ico")]
  [ToolboxItem(true)]
  [Description("Displays an editable text box, and a drop-down container that can host other controls and components.")]
  public class vControlBox : Control, INotifyPropertyChanged
  {
    private PaintHelper paintHelper = new PaintHelper();
    private Timer boundsTimer = new Timer();
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private bool useThemeBackColor = true;
    private bool useThemeBorderColor = true;
    private bool useThemeFont = true;
    private bool showGrip = true;
    private SizingDirection dropDownResizeDirection = SizingDirection.Both;
    private Brush disabledBackgroundBrush = (Brush) new SolidBrush(Color.Silver);
    private Brush backgroundBrush = (Brush) new SolidBrush(Color.WhiteSmoke);
    private Brush selectedBackgroundBrush = (Brush) new SolidBrush(Color.WhiteSmoke);
    private Brush highlightBackgroundBrush = (Brush) new SolidBrush(Color.WhiteSmoke);
    private bool useThemeBackground = true;
    private Color borderColor = Color.Black;
    private bool useThemeDropDownArrowColor = true;
    private Color arrowColor = Color.Black;
    private Color backColorDropDown = Color.White;
    private BackgroundElement backFill;
    private ControlState controlState;
    private Control contentControl;
    private vButtonEditBase buttonEdit;
    private vDropDownBase dropDownBase;
    private vSizingControl sizingControl;
    private ControlTheme theme;
    private bool useThemeForeColor;
    private bool dropDownArrowBackgroundEnabled;

    /// <summary>Gets or sets the theme of the control.</summary>
    [Description("Gets or sets the theme of the control.")]
    [Category("Appearance")]
    [Browsable(false)]
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
        this.backFill.LoadTheme(value);
        this.sizingControl.backFill.LoadTheme(value);
        this.Refresh();
        this.OnPropertyChanged("Theme");
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
        if (defaultTheme != null)
          this.Theme = defaultTheme;
        this.OnPropertyChanged("VIBlendTheme");
      }
    }

    /// <summary>
    /// Gets or sets whether to use the Theme BackColor or the control's BackColor property.
    /// </summary>
    [Category("EditBox")]
    [Description("Gets or sets whether to use the Theme BackColor or the control's BackColor property")]
    [Browsable(true)]
    [DefaultValue(true)]
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
    /// Gets or sets whether to use the Theme BorderColor or the control's BackColor property.
    /// </summary>
    [DefaultValue(true)]
    [Description("Gets or sets whether to use the Theme BorderColor or the control's BackColor property")]
    [Category("EditBox")]
    [Browsable(true)]
    public bool UseThemeBorderColor
    {
      get
      {
        return this.useThemeBorderColor;
      }
      set
      {
        this.useThemeBorderColor = value;
        this.SyncEditBoxAndDropDownColors();
        this.Refresh();
      }
    }

    /// <summary>
    /// Gets or sets whether to use the Theme BorderColor or the control's BackColor property.
    /// </summary>
    [Category("EditBox")]
    [Description("Gets or sets whether to use the Theme ForeColor or the control's ForeColor property")]
    [DefaultValue(false)]
    [Browsable(true)]
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
    [Browsable(true)]
    [DefaultValue(true)]
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

    /// <summary>Gets a reference to the DropDown of the ControlBox</summary>
    /// <value>The drop down.</value>
    [Browsable(false)]
    public vDropDownBase DropDown
    {
      get
      {
        return this.dropDownBase;
      }
    }

    /// <summary>
    /// Gets or sets the height in pixels of the drop-down portion of the ControlBox
    /// </summary>
    [Category("DropDown")]
    [DefaultValue(200)]
    [Description("Gets or sets the height in pixels of the drop-down portion of the ControlBox")]
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
    /// Gets or sets the width in pixels of the drop-down portion of the ControlBox
    /// </summary>
    [DefaultValue(200)]
    [Category("DropDown")]
    [Description("Gets or sets the width in pixels of the drop-down portion of the ControlBox")]
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
    /// Gets or sets the maximum size of the drop-down portion of the ControlBox
    /// </summary>
    [Description("Gets or sets the width in pixels of the drop-down portion of the ControlBox")]
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
    /// Gets or sets the maximum size of the drop-down portion of the ControlBox
    /// </summary>
    [Category("DropDown")]
    [Description("Gets or sets the width in pixels of the drop-down portion of the ControlBox")]
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

    /// <summary>Gets or sets a value indicating whether grip is shown</summary>
    [DefaultValue(true)]
    [Browsable(false)]
    [Description("Gets or sets a value indicating whether grip is shown")]
    [Category("DropDown")]
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
          if (this.dropDownResizeDirection == SizingDirection.None)
            this.dropDownResizeDirection = SizingDirection.Both;
        }
        this.OnPropertyChanged("ShowGrip");
      }
    }

    /// <summary>
    /// Gets or sets a value indicating the possible drop-down resize directions
    /// </summary>
    [Description("Gets or sets a value indicating the drop-down resize directions")]
    [Category("DropDown")]
    [DefaultValue(true)]
    public virtual SizingDirection DropDownResizeDirection
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
        this.OnPropertyChanged("DropDownResizeDirection");
      }
    }

    /// <summary>
    /// Gets a reference to the EditBox of the vControlBox control
    /// </summary>
    [Browsable(false)]
    public vButtonEditBase EditBase
    {
      get
      {
        return this.buttonEdit;
      }
    }

    /// <summary>
    /// Gets or sets the content control which is displayed when the dropdown is opened
    /// </summary>
    [Browsable(false)]
    public Control ContentControl
    {
      get
      {
        return this.contentControl;
      }
      set
      {
        this.contentControl = value;
        this.dropDownBase.Controls.Clear();
        if (value != null)
        {
          this.dropDownBase.Controls.Add(value);
          this.contentControl.Dock = DockStyle.Fill;
        }
        this.ShowGrip = !this.ShowGrip;
        this.ShowGrip = !this.ShowGrip;
        this.OnPropertyChanged("ContentControl");
      }
    }

    /// <summary>Gets or sets the disabled background brush.</summary>
    /// <value>The disabled background brush.</value>
    [Category("DropDownButton.Appearance")]
    [Browsable(false)]
    [Description("Gets or sets the disabled background brush.")]
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
    [Description("Gets or sets the background brush.")]
    [Browsable(false)]
    [Category("DropDownButton.Appearance")]
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
    [Description("Gets or sets the pressed background brush.")]
    [Browsable(false)]
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
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether to use theme's background")]
    [DefaultValue(true)]
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
    /// Gets or sets the border color which is used when the UseThemeBorderColor property is false.
    /// </summary>
    [Category("EditBox")]
    [Description("Gets or sets the border color which is used when the UseThemeBorderColor property is false.")]
    public Color BorderColor
    {
      get
      {
        return this.borderColor;
      }
      set
      {
        this.borderColor = value;
        this.SyncEditBoxAndDropDownColors();
        this.Invalidate();
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
    [DefaultValue(typeof (Color), "Black")]
    [Category("EditBox")]
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
        this.Refresh();
      }
    }

    /// <summary>
    /// Gets or sets the foreground color which is used to display the text
    /// </summary>
    [Browsable(true)]
    [Category("EditBox")]
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
    [Category("EditBox")]
    [Browsable(true)]
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
    [Browsable(true)]
    [Category("EditBox")]
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

    /// <summary>
    /// Gets or sets the background color for the dropdown control.
    /// </summary>
    [DefaultValue(typeof (Color), "White")]
    [Browsable(true)]
    [Category("DropDown")]
    public Color BackColorDropDown
    {
      get
      {
        return this.backColorDropDown;
      }
      set
      {
        this.backColorDropDown = value;
        this.Refresh();
      }
    }

    /// <summary>Occurs when a property value changes.</summary>
    [Category("Action")]
    public event PropertyChangedEventHandler PropertyChanged;

    static vControlBox()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vControlBox" /> class.
    /// </summary>
    public vControlBox()
    {
      this.SetStyle(ControlStyles.Selectable, false);
      this.dropDownBase = new vDropDownBase();
      this.sizingControl = new vSizingControl();
      this.dropDownBase.Controls.Add((Control) this.sizingControl);
      this.DropDownHeight = this.DropDownWidth = 200;
      this.DropDownMinimumSize = new Size(10, 10);
      this.DropDownMaximumSize = new Size(1000, 1000);
      this.sizingControl.Size = new Size(0, 8);
      this.sizingControl.Dock = DockStyle.Bottom;
      this.buttonEdit = new vButtonEditBase();
      this.buttonEdit.Dock = DockStyle.Fill;
      this.dropDownBase.BackColor = Color.White;
      this.BackColor = Color.White;
      this.useThemeBackColor = false;
      this.buttonEdit.ReadOnly = true;
      this.buttonEdit.TextBox.ReadOnly = true;
      this.backFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this.buttonEdit);
      this.Theme = ControlTheme.GetDefaultTheme(VIBLEND_THEME.VISTABLUE);
      this.Controls.Add((Control) this.buttonEdit);
      this.buttonEdit.PaintEditBox += new PaintEventHandler(this.OnButtonEditPaint);
      this.buttonEdit.MouseDown += new MouseEventHandler(this.OnButtonEditMouseDown);
      this.buttonEdit.MouseMove += new MouseEventHandler(this.OnButtonEditMouseMove);
      this.buttonEdit.MouseLeave += new EventHandler(this.OnButtonEditMouseLeave);
      this.dropDownBase.DropDownOpen += new EventHandler(this.OnDropDownOpenPopup);
      this.dropDownBase.DropDownClose += new EventHandler(this.dropDown_DropDownClose);
      this.SizeChanged += new EventHandler(this.OnControlBoxSizeChanged);
      this.TextChanged += new EventHandler(this.OnControlBoxTextChanged);
      this.VisibleChanged += new EventHandler(this.OnControlBoxVisibleChanged);
      this.LostFocus += new EventHandler(this.vControlBox_LostFocus);
      this.buttonEdit.TextBox.KeyDown += new KeyEventHandler(this.TextBox_KeyDown);
      this.buttonEdit.TextBox.KeyPress += new KeyPressEventHandler(this.TextBox_KeyPress);
      this.buttonEdit.TextBox.KeyUp += new KeyEventHandler(this.TextBox_KeyUp);
    }

    private void TextBox_TextChanged(object sender, EventArgs e)
    {
      this.OnTextChanged(e);
    }

    private void TextBox_KeyUp(object sender, KeyEventArgs e)
    {
      this.OnKeyUp(e);
    }

    private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
    {
      this.OnKeyPress(e);
    }

    private void TextBox_KeyDown(object sender, KeyEventArgs e)
    {
      this.OnKeyDown(e);
      if (e.Alt && e.KeyCode == Keys.Up || e.KeyCode == Keys.F4)
      {
        this.DropDown.CloseControl();
      }
      else
      {
        if (!e.Alt || e.KeyCode != Keys.Down || this.buttonEdit == null)
          return;
        Point location = this.buttonEdit.PerformBoundsCalculations(this.ClientRectangle).Location;
        this.OnButtonEditMouseDown((object) this, new MouseEventArgs(MouseButtons.Left, 1, location.X, location.Y, 0));
      }
    }

    private void vControlBox_LostFocus(object sender, EventArgs e)
    {
      int num = this.buttonEdit.Focused ? 1 : 0;
    }

    private void dropDown_DropDownClose(object sender, EventArgs e)
    {
      this.Refresh();
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

    private void OnControlBoxVisibleChanged(object sender, EventArgs e)
    {
      if (this.Visible)
        return;
      this.dropDownBase.CloseControl();
    }

    private void OnControlBoxTextChanged(object sender, EventArgs e)
    {
      if (!(this.buttonEdit.Text != this.Text))
        return;
      this.buttonEdit.Text = this.Text;
    }

    private void OnControlBoxSizeChanged(object sender, EventArgs e)
    {
      this.dropDownBase.PreferredSize = new Size(this.dropDownBase.Size.Width, this.dropDownBase.Size.Height);
      if (this.contentControl == null)
        return;
      this.contentControl.Refresh();
    }

    /// <summary>Clean up any resources being used.</summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
        this.boundsTimer.Dispose();
      base.Dispose(disposing);
    }

    private void OnButtonEditTextChanged(object sender, EventArgs e)
    {
      this.Text = this.buttonEdit.Text;
    }

    private void OnDropDownOpenPopup(object sender, EventArgs e)
    {
      if (this.contentControl == null)
        return;
      this.contentControl.Capture = false;
      this.contentControl.PerformLayout();
    }

    private void boundsTimer_Tick(object sender, EventArgs e)
    {
    }

    private void OnButtonEditMouseDown(object sender, MouseEventArgs e)
    {
      this.ShowDropDown(e.Location);
    }

    public virtual void ShowDropDown(Point location)
    {
      if (!this.buttonEdit.PerformBoundsCalculations(this.ClientRectangle).Contains(location))
        return;
      this.buttonEdit.Focus();
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
      this.controlState = ControlState.Pressed;
      if (this.contentControl != null)
        this.DropDown.Capture = false;
      if (this.FindForm() != null)
      {
        this.FindForm().LocationChanged -= new EventHandler(this.vComboBox_LocationChanged);
        this.FindForm().LocationChanged += new EventHandler(this.vComboBox_LocationChanged);
        if (this.FindForm().ParentForm == null)
          return;
        this.FindForm().ParentForm.LocationChanged -= new EventHandler(this.vComboBox_LocationChanged);
        this.FindForm().ParentForm.LocationChanged += new EventHandler(this.vComboBox_LocationChanged);
      }
      else
      {
        if (this.Parent == null)
          return;
        this.Parent.LocationChanged -= new EventHandler(this.Parent_LocationChanged);
        this.Parent.LocationChanged += new EventHandler(this.Parent_LocationChanged);
      }
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
      this.backFill.Radius = 0;
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
      if (this.dropDownBase == null)
        return;
      this.dropDownBase.BackColor = this.backColorDropDown;
    }

    /// <summary>Closes the drop down.</summary>
    public void CloseDropDown()
    {
      this.DropDown.CloseControl();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
      if (!this.DesignMode)
        Licensing.LICheck((Control) this);
      this.SyncEditBoxAndDropDownColors();
      base.OnPaint(e);
    }

    /// <summary>Called when property is changed.</summary>
    /// <param name="propertyName">Name of the property.</param>
    protected virtual void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
