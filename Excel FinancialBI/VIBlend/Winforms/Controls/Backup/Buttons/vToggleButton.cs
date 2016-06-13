// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vToggleButton
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vToggleButton control</summary>
  [ToolboxItem(true)]
  [Designer("VIBlend.WinForms.Controls.Design.vDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxBitmap(typeof (vToggleButton), "ControlIcons.ToggleButton.ico")]
  public class vToggleButton : vButton
  {
    private CheckState toggle;
    private bool isTreeState;

    /// <summary>Gets or sets the toggle state of the control</summary>
    [Bindable(true)]
    [Category("Appearance")]
    [Browsable(true)]
    public CheckState Toggle
    {
      get
      {
        return this.toggle;
      }
      set
      {
        if (this.toggle == value)
          return;
        this.toggle = value;
        this.OnToggleStateChanged();
        if (this.isTreeState)
        {
          switch (value)
          {
            case CheckState.Unchecked:
              this.controlState = ControlState.Normal;
              return;
            case CheckState.Checked:
              this.controlState = ControlState.Pressed;
              return;
            case CheckState.Indeterminate:
              this.controlState = ControlState.Normal;
              return;
          }
        }
        else if (this.Toggle == CheckState.Checked)
          this.controlState = ControlState.Pressed;
        else
          this.controlState = ControlState.Normal;
        this.Refresh();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the toggle button is in 3-State mode
    /// </summary>
    [Bindable(true)]
    [Category("Behavior")]
    [Browsable(true)]
    [Description("Gets or sets a value indicating whether the toggle button is in 3-State mode")]
    [DefaultValue(false)]
    public bool IsThreeState
    {
      get
      {
        return this.isTreeState;
      }
      set
      {
        this.isTreeState = value;
        this.Invalidate();
      }
    }

    /// <summary>Occurs when the toggle state of the button changes</summary>
    public event EventHandler ToggleStateChanged;

    static vToggleButton()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vToggleButton" /> class.
    /// </summary>
    public vToggleButton()
    {
      this.StyleKey = "ToggleButton";
    }

    /// <exclude />
    protected override void OnMouseDown(MouseEventArgs mevent)
    {
      this.Capture = true;
      this.Invalidate();
      base.OnMouseDown(mevent);
    }

    /// <exclude />
    protected override void OnMouseLeave(EventArgs e)
    {
      if (this.Toggle == CheckState.Checked || this.Toggle == CheckState.Indeterminate)
        this.controlState = ControlState.Pressed;
      else
        this.controlState = ControlState.Normal;
      this.Invalidate();
    }

    /// <exclude />
    protected override void OnMouseUp(MouseEventArgs mevent)
    {
      this.Capture = false;
      if (this.ClientRectangle.Contains(mevent.Location))
        this.OnToggle();
      this.Invalidate();
    }

    /// <exclude />
    protected virtual void OnToggleStateChanged()
    {
      if (this.ToggleStateChanged == null)
        return;
      this.ToggleStateChanged((object) this, EventArgs.Empty);
    }

    /// <exclude />
    protected internal virtual void OnToggle()
    {
      if (this.isTreeState)
      {
        switch (this.Toggle)
        {
          case CheckState.Unchecked:
            this.Toggle = CheckState.Checked;
            this.controlState = ControlState.Pressed;
            break;
          case CheckState.Checked:
            this.Toggle = CheckState.Indeterminate;
            this.controlState = ControlState.Pressed;
            break;
          case CheckState.Indeterminate:
            this.Toggle = CheckState.Unchecked;
            this.controlState = ControlState.Normal;
            break;
        }
      }
      else if (this.Toggle == CheckState.Checked)
      {
        this.Toggle = CheckState.Unchecked;
        this.controlState = ControlState.Normal;
      }
      else
      {
        this.Toggle = CheckState.Checked;
        this.controlState = ControlState.Pressed;
      }
    }
  }
}
