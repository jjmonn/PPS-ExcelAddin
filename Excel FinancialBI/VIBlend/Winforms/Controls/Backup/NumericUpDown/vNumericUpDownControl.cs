// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vNumericUpDown
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vNumericUpDown control</summary>
  /// <remarks>
  /// A vNumericUpDown displays a numeric value that the user can increment or decrement by clicking on the up/down arrows.
  /// </remarks>
  [DefaultProperty("Value")]
  [Designer("VIBlend.WinForms.Controls.Design.vDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (vNumericUpDown), "ControlIcons.vNumericUpDown.ico")]
  [Description("Displays a numeric value that the user can increment or decrement by clicking on the up/down arrows.")]
  [DefaultEvent("ValueChanged")]
  public class vNumericUpDown : vNumericUpDownBase, IScrollableControlBase
  {
    private int increment = 1;
    private int maximum = 100;
    private bool allowAnimations = true;
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private bool dropDownArrowBackgroundEnabled = true;
    private PaintHelper paintHelper = new PaintHelper();
    private int minimum;
    private BackgroundElement upButtonFill;
    private BackgroundElement downButtonFill;
    private BackgroundElement contentFill;
    private AnimationManager manager;
    private ControlTheme theme;
    private int value;

    /// <exclude />
    [Browsable(false)]
    public new AnimationManager AnimationManager
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

    /// <summary>Determines whether the control is animated</summary>
    [DefaultValue(true)]
    [Browsable(false)]
    public new bool AllowAnimations
    {
      get
      {
        return this.allowAnimations;
      }
      set
      {
        this.allowAnimations = value;
        if (this.downButtonFill == null)
          return;
        this.downButtonFill.IsAnimated = value;
        this.upButtonFill.IsAnimated = value;
      }
    }

    /// <summary>Gets or sets the increment step.</summary>
    [Category("Behavior")]
    [Description("Gets or sets the increment step")]
    [DefaultValue(1)]
    public int Increment
    {
      get
      {
        return this.increment;
      }
      set
      {
        this.increment = value;
      }
    }

    /// <summary>Gets or sets the maximum value.</summary>
    [Description("Gets or sets the maximum value.")]
    [DefaultValue(100)]
    [Category("Behavior")]
    public int Maximum
    {
      get
      {
        return this.maximum;
      }
      set
      {
        this.maximum = value;
      }
    }

    /// <summary>Gets or sets the minimum value.</summary>
    [DefaultValue(0)]
    [Description("Gets or sets the minimum value.")]
    [Category("Behavior")]
    public int Minimum
    {
      get
      {
        return this.minimum;
      }
      set
      {
        this.minimum = value;
      }
    }

    /// <summary>
    /// Gets or sets the text of the edit field of the spin control.
    /// </summary>
    [Description("Gets or sets the text of the edit field of the spin control.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override string Text
    {
      get
      {
        return this.txtBox.Text;
      }
      set
      {
        this.txtBox.Text = value;
      }
    }

    /// <summary>Gets or sets the theme of the control.</summary>
    [Category("Appearance")]
    [Description("Gets or sets the theme of the control.")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new ControlTheme Theme
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
        this.upButtonFill.LoadTheme(value);
        this.downButtonFill.LoadTheme(value);
        this.upButtonFill.Radius = 0;
        this.downButtonFill.Radius = 0;
        this.contentFill.LoadTheme(value.CreateCopy());
        this.contentFill.Theme.StyleNormal.FillStyle = (FillStyle) new FillStyleSolid(this.BackColor);
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the theme of the control using one of the built-in themes.
    /// </summary>
    [Description("Gets or sets the theme of the control using one of the built-in themes.")]
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

    /// <summary>Gets or sets the value of the control.</summary>
    [DefaultValue(0)]
    [Category("Behavior")]
    [Description("Gets or sets the value of the control.")]
    public int Value
    {
      get
      {
        try
        {
          int num = int.Parse(this.txtBox.Text, (IFormatProvider) CultureInfo.InvariantCulture);
          if (num > this.Maximum)
            return this.Maximum;
          if (num < this.Minimum)
            return this.Minimum;
          this.value = num;
          return num;
        }
        catch
        {
        }
        return this.Minimum;
      }
      set
      {
        if (value == this.Value)
          return;
        int num = value;
        if (num > this.Maximum)
          num = this.Maximum;
        if (num < this.Minimum)
          num = this.Minimum;
        if (this.txtBox.Text != num.ToString())
          this.txtBox.Text = num.ToString();
        this.value = num;
      }
    }

    /// <summary>
    /// Determines whether to the drop-down arrow's background is painted
    /// </summary>
    [Category("EditBox")]
    [Browsable(true)]
    [DefaultValue(false)]
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

    /// <summary>Occurs when the Value property changes.</summary>
    [Category("Action")]
    public event EventHandler ValueChanged;

    static vNumericUpDown()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>Constructor</summary>
    public vNumericUpDown()
    {
      this.txtBox.TextChanged += new EventHandler(this.OnTextChanged);
      this.upButtonFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.downButtonFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.contentFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.Value = 0;
      this.TextBox.ShowDefaultText = false;
      this.VButtonWidth = 16;
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
      this.Layout += new LayoutEventHandler(this.vNumericUpDown_Layout);
    }

    private void vNumericUpDown_Layout(object sender, LayoutEventArgs e)
    {
      if (!(this.txtBox.Text != this.Value.ToString()))
        return;
      this.txtBox.Text = this.Value.ToString();
    }

    private void txtBox_TextChanged(object sender, EventArgs e)
    {
    }

    /// <summary>Overriden from vSpinControlBase</summary>
    protected override void HandleDownClick()
    {
      int num = this.Value - this.increment;
      if (num < this.Minimum)
        num = this.Minimum;
      this.Value = num;
    }

    /// <summary>Spins the value.</summary>
    /// <param name="up">if set to <c>true</c> [up].</param>
    public void SpinValue(bool up)
    {
      if (up)
        this.HandleUpClick();
      else
        this.HandleDownClick();
    }

    /// <summary>Overriden from vSpinControlBase</summary>
    protected override void HandleUpClick()
    {
      int num = this.Value + this.increment;
      if (num > this.Maximum)
        num = this.Maximum;
      this.Value = num;
    }

    private void OnTextChanged(object sender, EventArgs e)
    {
      if (this.value != this.Value)
        this.OnValueChanged(e);
      this.OnTextChanged(e);
    }

    /// <summary>Raises the ValueChanged event.</summary>
    /// <param name="e">An EventArgs that contains the event data.</param>
    protected virtual void OnValueChanged(EventArgs e)
    {
      if (this.ValueChanged == null)
        return;
      this.ValueChanged((object) this, e);
    }

    /// <summary>Raises the OnPaint event</summary>
    /// <param name="pevent">A PaintEventArgs that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
      Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
      this.DrawSpin(e.Graphics, rect, this.Text, this.Font, this.ForeColor, this.BackColor, this.RightToLeft, this.Enabled, this.ReadOnly, this.HoverState, this.IsUpRepeatButtonMouseOver, this.IsDownRepeatButtonMouseOver, this.isUpPressed, this.isDownPressed, this.Focused);
    }

    /// <summary>Draws the spin.</summary>
    /// <param name="g">The g.</param>
    /// <param name="rect">The rect.</param>
    /// <param name="text">The text.</param>
    /// <param name="font">The font.</param>
    /// <param name="foreColor">Color of the fore.</param>
    /// <param name="backColor">Color of the back.</param>
    /// <param name="rightToLeft">The right to left.</param>
    /// <param name="isEnabled">if set to <c>true</c> [is enabled].</param>
    /// <param name="readOnly">if set to <c>true</c> [read only].</param>
    /// <param name="isMouseOver">if set to <c>true</c> [is mouse over].</param>
    /// <param name="isUpMouseOver">if set to <c>true</c> [is up mouse over].</param>
    /// <param name="isDownMouseOver">if set to <c>true</c> [is down mouse over].</param>
    /// <param name="isUpPressed">if set to <c>true</c> [is up pressed].</param>
    /// <param name="isDownPressed">if set to <c>true</c> [is down pressed].</param>
    /// <param name="isFocused">if set to <c>true</c> [is focused].</param>
    protected virtual void DrawSpin(Graphics g, Rectangle rect, string text, Font font, Color foreColor, Color backColor, RightToLeft rightToLeft, bool isEnabled, bool readOnly, bool isMouseOver, bool isUpMouseOver, bool isDownMouseOver, bool isUpPressed, bool isDownPressed, bool isFocused)
    {
      if (isEnabled)
        text = string.Empty;
      this.contentFill.Bounds = rect;
      this.contentFill.DrawElementFill(g, isEnabled ? ControlState.Normal : ControlState.Disabled);
      this.contentFill.DrawElementBorder(g, isEnabled ? ControlState.Normal : ControlState.Disabled);
      Rectangle repeatButtonBounds1 = this.GetUpRepeatButtonBounds(this.ClientRectangle);
      Rectangle repeatButtonBounds2 = this.GetDownRepeatButtonBounds(this.ClientRectangle);
      this.upButtonFill.Bounds = repeatButtonBounds1;
      this.downButtonFill.Bounds = repeatButtonBounds2;
      ControlState stateType1 = ControlState.Normal;
      ControlState stateType2 = ControlState.Normal;
      if (readOnly)
        isMouseOver = false;
      if (isUpMouseOver)
        stateType1 = ControlState.Hover;
      if (isUpPressed)
        stateType1 = ControlState.Pressed;
      if (isDownMouseOver)
        stateType2 = ControlState.Hover;
      if (isDownPressed)
        stateType2 = ControlState.Pressed;
      if (isMouseOver && !isUpMouseOver && !isDownMouseOver)
      {
        stateType2 = ControlState.Normal;
        stateType1 = ControlState.Normal;
      }
      if (this.DropDownArrowBackgroundEnabled)
      {
        this.downButtonFill.DrawElementFill(g, stateType2);
        this.upButtonFill.DrawElementFill(g, stateType1);
      }
      if (isMouseOver || stateType2 == ControlState.Hover || stateType2 == ControlState.Pressed)
      {
        this.downButtonFill.DrawElementFill(g, stateType2);
        this.downButtonFill.DrawElementBorder(g, stateType2);
      }
      if (isMouseOver || stateType1 == ControlState.Hover || stateType1 == ControlState.Pressed)
      {
        this.upButtonFill.DrawElementFill(g, stateType1);
        this.upButtonFill.DrawElementBorder(g, stateType1);
      }
      ControlState controlState1 = isMouseOver ? ControlState.Hover : stateType1;
      ControlState controlState2 = isMouseOver ? ControlState.Hover : stateType2;
      if (stateType1 == ControlState.Pressed)
        controlState1 = ControlState.Pressed;
      if (stateType2 == ControlState.Pressed)
        controlState2 = ControlState.Pressed;
      this.paintHelper.DrawArrowFigure(g, this.GetArrowColor(controlState2), PaintHelper.OfficeArrowRectFromBounds(repeatButtonBounds2), ArrowDirection.Down);
      this.paintHelper.DrawArrowFigure(g, this.GetArrowColor(controlState1), PaintHelper.OfficeArrowRectFromBounds(repeatButtonBounds1), ArrowDirection.Up);
      if (text == null)
        return;
      Color color = foreColor;
      if (!isEnabled)
        color = this.theme.StyleDisabled.TextColor;
      using (StringFormat format = new StringFormat())
      {
        using (SolidBrush solidBrush = new SolidBrush(color))
        {
          format.FormatFlags = StringFormatFlags.NoWrap;
          format.LineAlignment = StringAlignment.Center;
          rect.X += 2;
          rect.Width -= 2;
          g.DrawString(text, font, (Brush) solidBrush, (RectangleF) rect, format);
        }
      }
    }

    /// <summary>Gets the color of the arrow.</summary>
    /// <param name="controlState">State of the control.</param>
    /// <returns></returns>
    protected virtual Color GetArrowColor(ControlState controlState)
    {
      Color color = this.theme.QueryColorSetter("DropDownArrowColor");
      if (color == Color.Empty)
        color = this.theme.StyleNormal.BorderColor;
      if (controlState == ControlState.Hover)
      {
        color = this.theme.QueryColorSetter("DropDownArrowColorHighlight");
        if (color == Color.Empty)
          color = this.theme.StyleHighlight.BorderColor;
      }
      if (controlState == ControlState.Pressed)
      {
        color = this.theme.QueryColorSetter("DropDownArrowColorSelected");
        if (color == Color.Empty)
          color = this.theme.QueryColorSetter("DropDownArrowColorHighlight");
        if (color == Color.Empty)
          color = this.theme.StylePressed.BorderColor;
      }
      return color;
    }
  }
}
