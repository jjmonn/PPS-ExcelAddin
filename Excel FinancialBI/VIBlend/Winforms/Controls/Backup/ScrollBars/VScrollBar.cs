// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vVScrollBar
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vertical scroll bar control</summary>
  [ToolboxItem(true)]
  [Designer("VIBlend.WinForms.Controls.Design.vTrackControlsDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxBitmap(typeof (vVScrollBar), "ControlIcons.vVScrollBar.ico")]
  [Description("Represents a Vertical ScrollBar control.")]
  public class vVScrollBar : vScrollBar
  {
    protected internal override string StyleKey
    {
      get
      {
        if (!base.StyleKey.Equals("VScrollBar"))
          return "VScrollBar";
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
        return new Rectangle(0, 0, this.ClientRectangle.Width, this.Width);
      }
    }

    /// <exclude />
    protected internal override Rectangle SmallIncrementRectangle
    {
      get
      {
        return new Rectangle(0, this.ClientRectangle.Bottom - this.Width, this.ClientRectangle.Width, this.Width);
      }
    }

    /// <exclude />
    protected override Rectangle LargeDecrementRectangle
    {
      get
      {
        return new Rectangle(1, this.ClientRectangle.Width - 1, this.ClientRectangle.Width, this.thumbPos);
      }
    }

    /// <exclude />
    protected override Rectangle LargeIncrementRectangle
    {
      get
      {
        return new Rectangle(0, this.ThumbRectangle.Y + this.ThumbRectangle.Height, this.ClientRectangle.Width, this.ClientRectangle.Bottom - this.SmallIncrementRectangle.Height);
      }
    }

    /// <exclude />
    protected override ArrowDirection SmallDecrementArrowDirection
    {
      get
      {
        return ArrowDirection.Up;
      }
    }

    /// <exclude />
    protected override ArrowDirection SmallIncrementArrowDirection
    {
      get
      {
        return ArrowDirection.Down;
      }
    }

    /// <exclude />
    protected override int ThumbArea
    {
      get
      {
        return this.Height - this.SmallDecrementRectangle.Height * 2 - this.ThumbRectangle.Height;
      }
    }

    static vVScrollBar()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>Constructor</summary>
    public vVScrollBar()
    {
      this.Width = 17;
      this.Height = 50;
      this.StyleKey = "VScrollBar";
    }

    /// <exclude />
    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      this.isThumbVisible = this.Height - this.SmallDecrementRectangle.Height * 2 >= 6;
    }

    /// <exclude />
    protected override void OnPaint(PaintEventArgs e)
    {
      this.buttonIncrement.RoundedCornersBitmask = (byte) 15;
      this.buttonDecrement.RoundedCornersBitmask = (byte) 15;
      if (this.ScrollButtonsSemiRounded)
      {
        this.buttonIncrement.RoundedCornersBitmask = (byte) 12;
        this.buttonDecrement.RoundedCornersBitmask = (byte) 3;
      }
      base.OnPaint(e);
    }
  }
}
