// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vHScrollBar
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a horizontal scroll bar control</summary>
  [Description("Represents a Horizontal ScrollBar control.")]
  [ToolboxBitmap(typeof (vHScrollBar), "ControlIcons.vHScrollBar.ico")]
  [ToolboxItem(true)]
  [Designer("VIBlend.WinForms.Controls.Design.vTrackControlsDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  public class vHScrollBar : vScrollBar
  {
    protected internal override string StyleKey
    {
      get
      {
        if (!base.StyleKey.Equals("HScrollBar"))
          return "HScrollBar";
        return base.StyleKey;
      }
      set
      {
        base.StyleKey = value;
      }
    }

    /// <exclude />
    protected internal override Rectangle SmallDecrementRectangle
    {
      get
      {
        return new Rectangle(0, 0, this.Height, this.ClientRectangle.Height);
      }
    }

    /// <exclude />
    protected internal override Rectangle SmallIncrementRectangle
    {
      get
      {
        return new Rectangle(this.ClientRectangle.Right - this.Height, 0, this.Height, this.ClientRectangle.Height);
      }
    }

    /// <exclude />
    protected override Rectangle LargeDecrementRectangle
    {
      get
      {
        return new Rectangle(this.SmallDecrementRectangle.Width, 2, this.thumbPos, this.Height - 4);
      }
    }

    /// <exclude />
    protected override Rectangle LargeIncrementRectangle
    {
      get
      {
        return new Rectangle(this.ThumbRectangle.X + this.ThumbRectangle.Width, 2, this.ClientRectangle.Right - this.ThumbRectangle.X - this.ThumbRectangle.Width, this.Height - 4);
      }
    }

    /// <exclude />
    protected override ArrowDirection SmallDecrementArrowDirection
    {
      get
      {
        return ArrowDirection.Left;
      }
    }

    /// <exclude />
    protected override ArrowDirection SmallIncrementArrowDirection
    {
      get
      {
        return ArrowDirection.Right;
      }
    }

    /// <exclude />
    protected override int ThumbArea
    {
      get
      {
        return this.Width - this.SmallDecrementRectangle.Width * 2 - this.ThumbRectangle.Width;
      }
    }

    static vHScrollBar()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>Constructor</summary>
    public vHScrollBar()
    {
      this.Height = 17;
      this.Width = 50;
      this.StyleKey = "HScrollBar";
    }

    /// <exclude />
    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      this.isThumbVisible = this.Width - this.SmallDecrementRectangle.Width * 2 >= 6;
    }

    /// <exclude />
    protected override void OnPaint(PaintEventArgs e)
    {
      this.buttonIncrement.RoundedCornersBitmask = (byte) 15;
      this.buttonDecrement.RoundedCornersBitmask = (byte) 15;
      if (this.ScrollButtonsSemiRounded)
      {
        this.buttonIncrement.RoundedCornersBitmask = (byte) 10;
        this.buttonDecrement.RoundedCornersBitmask = (byte) 5;
      }
      base.OnPaint(e);
    }
  }
}
