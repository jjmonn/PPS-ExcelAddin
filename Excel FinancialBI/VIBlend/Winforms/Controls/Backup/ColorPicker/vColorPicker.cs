// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vColorPicker
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
  /// <summary>Represents a vColorPicker control.</summary>
  [Description("Represents a drop-down control which allows the user to select a color.")]
  [DefaultEvent("SelectedColorChanged")]
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (vColorPicker), "ControlIcons.ColorPicker.ico")]
  [Designer("VIBlend.WinForms.Controls.Design.vDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  public class vColorPicker : vControlBox
  {
    private ColorPickerPanel colorsPanel = new ColorPickerPanel();
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private bool supresTextChangedHandling;
    private bool suppressCloseOnColorChanged;

    [Browsable(false)]
    public override SizingDirection DropDownResizeDirection
    {
      get
      {
        return base.DropDownResizeDirection;
      }
      set
      {
        base.DropDownResizeDirection = value;
      }
    }

    /// <summary>Gets or sets the selected color.</summary>
    [Description("Gets or sets the selected color.")]
    [Category("Behavior")]
    public Color SelectedColor
    {
      get
      {
        return this.colorsPanel.SelectedColor;
      }
      set
      {
        this.colorsPanel.SelectedColor = value;
        this.UpdateText();
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the theme of the control.</summary>
    [Category("Appearance")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets the theme of the control.")]
    public new ControlTheme Theme
    {
      get
      {
        return base.Theme;
      }
      set
      {
        if (value == null)
          return;
        base.Theme = value;
        this.colorsPanel.Theme = value;
        this.Refresh();
      }
    }

    /// <summary>
    /// Gets or sets the theme of the control using one of the built-in themes.
    /// </summary>
    [Category("Appearance")]
    public new VIBLEND_THEME VIBlendTheme
    {
      get
      {
        return this.defaultTheme;
      }
      set
      {
        this.defaultTheme = value;
        if (this.colorsPanel != null)
          this.colorsPanel.VIBlendTheme = value;
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

    /// <summary>Occurs when a color is changed.</summary>
    [Description("Occurs when a color is changed.")]
    [Category("Action")]
    public event EventHandler<ColorEventArgs> SelectedColorChanged;

    /// <summary>
    /// Occurs when the selected color has changed and vColorPicker updates text in the textbox portion of the control
    /// </summary>
    /// <remarks>
    /// Use this event to specify custom formatting for the selected color
    /// </remarks>
    public event vColorPicker.ColorStringFormatEventHandler ColorTextFormat;

    static vColorPicker()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    public vColorPicker()
    {
      this.SelectedColor = Color.White;
      this.Theme = ControlTheme.GetDefaultTheme(VIBLEND_THEME.VISTABLUE);
      this.DropDownHeight = this.colorsPanel.Height;
      this.DropDownWidth = this.colorsPanel.Width;
      this.Width = this.colorsPanel.Width;
      this.EditBase.ReadOnly = true;
      this.ContentControl = (Control) this.colorsPanel;
      this.DropDownResizeDirection = SizingDirection.None;
      this.ShowGrip = false;
      this.colorsPanel.SelectedColorChanged += new EventHandler(this.colorsPanel_SelectedColorChanged);
      this.DropDown.DropDownOpen += new EventHandler(this.DropDown_DropDownOpen);
      this.EditBase.PaintEditBox += new PaintEventHandler(this.EditBase_PaintEditBox);
      this.EditBase.TextChanged += new EventHandler(this.EditBase_TextChanged);
    }

    protected virtual void OnSelectedColorChanged(ColorEventArgs args)
    {
      if (this.SelectedColorChanged == null)
        return;
      this.SelectedColorChanged((object) this, args);
    }

    private void EditBase_TextChanged(object sender, EventArgs e)
    {
      if (this.supresTextChangedHandling)
        return;
      this.supresTextChangedHandling = true;
      this.UpdateText();
      this.supresTextChangedHandling = false;
    }

    private void EditBase_PaintEditBox(object sender, PaintEventArgs e)
    {
      Graphics graphics = e.Graphics;
      Rectangle clientRectangle = this.ClientRectangle;
      int num = 3;
      clientRectangle.Height = 13;
      clientRectangle.Width = 23;
      clientRectangle.X += num;
      clientRectangle.Y = (this.Height - clientRectangle.Height) / 2;
      this.EditBase.TextBoxAutoLayout = false;
      this.EditBase.TextBox.Left = clientRectangle.Right + num;
      this.EditBase.TextBox.Width = this.EditBase.Width - clientRectangle.Right - this.EditBase.VButtonWidth - 2 * num;
      this.DrawColorBox(graphics, clientRectangle);
      this.OnPaint(e);
    }

    private void DrawColorBox(Graphics graphics, Rectangle empty)
    {
      using (Brush brush = (Brush) new SolidBrush(this.SelectedColor))
        graphics.FillRectangle(brush, empty);
      if (this.Enabled)
        graphics.DrawRectangle(Pens.Black, empty);
      else
        graphics.DrawRectangle(Pens.Gray, empty);
    }

    private void DropDown_DropDownOpen(object sender, EventArgs e)
    {
      this.suppressCloseOnColorChanged = true;
      this.colorsPanel.FocusToSelectedColor();
      this.suppressCloseOnColorChanged = false;
    }

    private void UpdateText()
    {
      string FormattedText = string.Format("{0}, {1}, {2}, {3}", (object) this.colorsPanel.SelectedColor.A, (object) this.colorsPanel.SelectedColor.R, (object) this.colorsPanel.SelectedColor.G, (object) this.colorsPanel.SelectedColor.B);
      if (this.ColorTextFormat != null)
      {
        ColorStringFormatEventArgs e = new ColorStringFormatEventArgs(this.colorsPanel.SelectedColor, FormattedText);
        this.ColorTextFormat((object) this, e);
        FormattedText = e.FormattedText;
      }
      this.Text = FormattedText;
    }

    private void colorsPanel_SelectedColorChanged(object sender, EventArgs e)
    {
      this.UpdateText();
      this.OnSelectedColorChanged(new ColorEventArgs(this.SelectedColor));
      if (this.suppressCloseOnColorChanged)
        return;
      this.DropDown.CloseControl();
    }

    public delegate void ColorStringFormatEventHandler(object sender, ColorStringFormatEventArgs e);
  }
}
