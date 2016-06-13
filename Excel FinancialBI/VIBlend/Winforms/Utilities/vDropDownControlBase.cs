// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.vDropDownControlBase
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VIBlend.Utilities
{
  [ToolboxItem(false)]
  public class vDropDownControlBase : vControlBase
  {
    private int vButtonWidth = 16;
    private ControlTheme theme = ControlTheme.GetDefaultTheme(VIBLEND_THEME.VISTABLUE);
    private bool useThemeBorderColor = true;
    private bool useThemeForeColor = true;
    private bool useThemeFont = true;
    private Color overrideBorderColor = Color.Gray;
    private Color overrideForeColor = Color.Black;
    private Color overrideBackColor = Color.White;
    private Font overrideFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular);
    private Control contentControl = new Control();
    private const int vBorderWidth = 1;
    private const int borderWidth = 1;
    private bool useThemeBackColor;

    public ControlTheme Theme
    {
      get
      {
        return this.theme;
      }
      set
      {
        if (this.theme == value)
          return;
        this.theme = value;
        this.Refresh();
      }
    }

    public bool UseThemeBorderColor
    {
      get
      {
        return this.useThemeBorderColor;
      }
      set
      {
        this.useThemeBorderColor = value;
      }
    }

    public bool UseThemeBackColor
    {
      get
      {
        return this.useThemeBackColor;
      }
      set
      {
        this.useThemeBackColor = value;
      }
    }

    public bool UseThemeForeColor
    {
      get
      {
        return this.useThemeForeColor;
      }
      set
      {
        this.useThemeForeColor = value;
      }
    }

    public bool UseThemeFont
    {
      get
      {
        return this.useThemeFont;
      }
      set
      {
        this.useThemeFont = value;
      }
    }

    public Color OverrideBorderColor
    {
      get
      {
        return this.overrideBorderColor;
      }
      set
      {
        this.overrideBorderColor = value;
      }
    }

    public Color OverrideForeColor
    {
      get
      {
        return this.overrideForeColor;
      }
      set
      {
        this.overrideForeColor = value;
      }
    }

    public Color OverrideBackColor
    {
      get
      {
        return this.overrideBackColor;
      }
      set
      {
        this.overrideBackColor = value;
      }
    }

    public Font OverrideFont
    {
      get
      {
        return this.overrideFont;
      }
      set
      {
        this.overrideFont = value;
      }
    }

    /// <summary>Gets or sets the content control.</summary>
    /// <value>The content control.</value>
    public Control ContentControl
    {
      get
      {
        return this.contentControl;
      }
      set
      {
        Control contentControl = this.ContentControl;
        if (value == null)
          return;
        this.contentControl = value;
        if (contentControl == null)
          return;
        this.Controls.Clear();
        this.Controls.Add(value);
        this.PerformEditLayout();
      }
    }

    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [DefaultValue(16)]
    public virtual int VButtonWidth
    {
      get
      {
        return this.vButtonWidth;
      }
      set
      {
        if (value < 8)
          value = 8;
        else if (value > this.Width - 16)
          value = this.Width - 16;
        this.vButtonWidth = value;
        this.PerformEditLayout();
        this.Invalidate();
      }
    }

    protected bool Hover
    {
      get
      {
        return this.PerformBoundsCalculations(this.ClientRectangle).Contains(this.PointToClient(Cursor.Position));
      }
    }

    public event EventHandler vButtonClick;

    public event EventHandler DrawvButton;

    public event PaintEventHandler PaintEditBox;

    public vDropDownControlBase()
    {
      this.PerformEditLayout();
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      this.PerformEditLayout();
    }

    /// <summary>Performs the bounds calculations.</summary>
    /// <param name="bounds">The bounds.</param>
    /// <returns></returns>
    public Rectangle PerformBoundsCalculations(Rectangle bounds)
    {
      Rectangle rectangle = this.PerformBoundsCalculations(bounds, this.vButtonWidth);
      --rectangle.X;
      --rectangle.Height;
      if (this.RightToLeft != RightToLeft.No)
        rectangle.X = 1;
      return rectangle;
    }

    private Rectangle PerformBoundsCalculations(Rectangle bounds, int buttonWidth)
    {
      return new Rectangle(bounds.Right - buttonWidth - 1, bounds.Top + 1, buttonWidth, bounds.Height - 2);
    }

    protected virtual void OnDrawvButton()
    {
      if (this.DrawvButton == null)
        return;
      this.DrawvButton((object) this, EventArgs.Empty);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (!this.DesignMode)
        Licensing.LICheck((Control) this);
      Graphics graphics = e.Graphics;
      Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
      VisualStyle visualStyle = this.theme.StyleNormal;
      if (this.HoverState)
        visualStyle = this.theme.StyleHighlight;
      if (!this.Enabled)
        visualStyle = this.theme.StyleDisabled;
      Color color1 = this.useThemeBackColor ? visualStyle.FillStyle.Colors[0] : this.overrideBackColor;
      Color color2 = this.useThemeBorderColor ? visualStyle.BorderColor : this.overrideBorderColor;
      if (this.contentControl != null)
      {
        this.contentControl.Font = this.useThemeFont ? visualStyle.Font : this.overrideFont;
        this.contentControl.ForeColor = this.useThemeForeColor ? visualStyle.TextColor : this.overrideForeColor;
        this.contentControl.BackColor = color1;
      }
      using (Brush brush = (Brush) new SolidBrush(color1))
        graphics.FillRectangle(brush, this.ClientRectangle);
      using (Pen pen = new Pen(color2))
        graphics.DrawRectangle(pen, rect);
      if (this.PaintEditBox != null)
        this.PaintEditBox((object) this, e);
      this.OnDrawvButton();
    }

    protected Rectangle InnerRect(Rectangle bounds)
    {
      Rectangle rectangle = this.PerformBoundsCalculations(bounds);
      return new Rectangle(bounds.Left + 1, bounds.Top + 1, bounds.Width - 2 - rectangle.Width, bounds.Height - 2);
    }

    protected Rectangle InnerRect()
    {
      Rectangle rectangle1 = this.InnerRect(this.ClientRectangle);
      Rectangle rectangle2 = this.PerformBoundsCalculations(this.ClientRectangle);
      rectangle2.Width += 2;
      rectangle1.Width -= rectangle2.Width - SystemInformation.HorizontalScrollBarArrowWidth + 1;
      if (this.RightToLeft == RightToLeft.No)
      {
        rectangle1.X += 2;
        return rectangle1;
      }
      rectangle1.X += rectangle2.Width;
      return rectangle1;
    }

    protected virtual void OnButtonClick(EventArgs e)
    {
      if (this.vButtonClick == null)
        return;
      this.vButtonClick((object) this, e);
    }

    protected override void OnClick(EventArgs e)
    {
      base.OnClick(e);
      if (!this.Hover)
        return;
      this.OnButtonClick(e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if ((e.Button & MouseButtons.Left) != MouseButtons.Left)
        return;
      this.Focus();
      this.Invalidate();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      this.Invalidate();
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if ((e.Button & MouseButtons.Left) != MouseButtons.Left)
        return;
      this.Invalidate();
    }

    protected virtual void PerformEditLayout()
    {
      try
      {
        Rectangle rectangle = this.InnerRect();
        this.contentControl.Left = rectangle.Left + 1;
        this.contentControl.Width = rectangle.Width - 2;
        if (this.contentControl.AutoSize)
        {
          this.contentControl.Height = rectangle.Height;
          this.contentControl.Top = (this.Height - this.contentControl.Height) / 2;
        }
        else
          this.contentControl.Top = rectangle.Top + (rectangle.Height - this.contentControl.Height) / 2;
      }
      catch
      {
      }
    }
  }
}
