// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vSeparator
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vSeparator control</summary>
  [ToolboxItem(true)]
  [Description("Displays a separator control.")]
  [Designer("VIBlend.WinForms.Controls.Design.vDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxBitmap(typeof (vSeparator), "ControlIcons.Separator.ico")]
  public class vSeparator : ScrollableControlMiniBase
  {
    private bool paintFill = true;
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    internal BackgroundElement backElement;
    private bool paintBorder;
    private ControlTheme theme;

    public bool PaintFill
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

    public bool PaintBorder
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

    /// <summary>Gets or sets the theme.</summary>
    /// <value>The theme.</value>
    [Category("Appearance")]
    [Browsable(false)]
    [Description("Gets or sets the vRadioButton Theme")]
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
        this.backElement.LoadTheme(value);
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the VI blend theme.</summary>
    /// <value>The VI blend theme.</value>
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

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vSeparator" /> class.
    /// </summary>
    public vSeparator()
    {
      this.backElement = new BackgroundElement(this.Bounds, (IScrollableControlBase) this);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      this.Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      this.backElement.Bounds = this.ClientRectangle;
      if (this.PaintFill)
      {
        using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(this.ClientRectangle, this.Theme.StyleNormal.FillStyle.Colors[0], ControlPaint.LightLight(this.Theme.StyleNormal.FillStyle.Colors[0]), LinearGradientMode.Vertical))
          e.Graphics.FillRectangle((Brush) linearGradientBrush, this.ClientRectangle);
      }
      else
      {
        using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
          e.Graphics.FillRectangle((Brush) solidBrush, this.ClientRectangle);
      }
      if (!this.PaintBorder)
        return;
      this.backElement.Bounds = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
      this.backElement.DrawElementBorder(e.Graphics, ControlState.Normal);
    }
  }
}
