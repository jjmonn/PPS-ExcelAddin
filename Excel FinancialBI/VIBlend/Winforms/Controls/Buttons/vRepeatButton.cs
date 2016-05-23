// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vRepeatButton
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vRepeatButton control</summary>
  /// <remarks>
  /// A vRepeatButton is a button with a built-in timer that raises events when the user holds the button pressed.
  /// </remarks>
  [Description("Represents a button with a built-in timer that raises events when the user holds the button pressed.")]
  [Designer("VIBlend.WinForms.Controls.Design.vDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (vRepeatButton), "ControlIcons.RepeatButton.ico")]
  public class vRepeatButton : vButton
  {
    private int delayInterval = 100;
    private Timer timer;

    /// <summary>Gets or sets the repeat delay interval</summary>
    [DefaultValue(100)]
    [Category("Behavior")]
    [Description("Gets or sets the repeat delay")]
    public int DelayInterval
    {
      get
      {
        return this.delayInterval;
      }
      set
      {
        if (value == 0)
          value = 1;
        this.delayInterval = value;
        this.timer.Interval = value;
      }
    }

    /// <summary>Occurs when the vRepeatButton delay elapses</summary>
    public event EventHandler RepeatButtonClick;

    static vRepeatButton()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>Constructor</summary>
    public vRepeatButton()
    {
      this.timer = new Timer();
      this.timer.Tick += new EventHandler(this.timer_Tick);
    }

    private void timer_Tick(object sender, EventArgs e)
    {
      this.OnRepeatButtonClick();
    }

    /// <summary>Clean up any resources being used.</summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
        this.timer.Dispose();
      base.Dispose(disposing);
    }

    /// <exclude />
    protected virtual void OnRepeatButtonClick()
    {
      if (this.RepeatButtonClick == null)
        return;
      this.RepeatButtonClick((object) this, EventArgs.Empty);
    }

    /// <exclude />
    protected override void OnMouseDown(MouseEventArgs mevent)
    {
      base.OnMouseDown(mevent);
      this.timer.Start();
    }

    /// <exclude />
    protected override void OnMouseUp(MouseEventArgs mevent)
    {
      base.OnMouseUp(mevent);
      this.timer.Stop();
    }
  }
}
