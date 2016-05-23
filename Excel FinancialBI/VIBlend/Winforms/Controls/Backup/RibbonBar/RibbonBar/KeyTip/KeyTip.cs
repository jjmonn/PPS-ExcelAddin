// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.KeyTip
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a KeyTip component</summary>
  [Description("Represents a KeyTip component.")]
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (KeyTip), "ControlIcons.KeyTip.ico")]
  public class KeyTip : Component
  {
    private MouseAndKeyboardFilter filter = new MouseAndKeyboardFilter();
    private string tipText = "Tip";
    private ToolStripDropDown toolTip = new ToolStripDropDown();
    private Size keyTipSize = new Size(16, 16);
    private Control contentControl;
    private Keys activationKey;
    private bool checkedToolTip;
    private Point customLocation;

    /// <summary>Gets or sets the preferred location.</summary>
    /// <value>The preferred location.</value>
    [Category("Behavior")]
    [Description("Gets or sets the preferred location.")]
    public Point PreferredLocation
    {
      get
      {
        return this.customLocation;
      }
      set
      {
        this.customLocation = value;
      }
    }

    /// <summary>Gets or sets the size of the key tip.</summary>
    /// <value>The size of the key tip.</value>
    [Description("Gets or sets the size of the key tip.")]
    [Category("Appearance")]
    public Size KeyTipSize
    {
      get
      {
        return this.keyTipSize;
      }
      set
      {
        this.keyTipSize = value;
      }
    }

    /// <summary>Gets or sets the activation key.</summary>
    /// <value>The activation key.</value>
    [Category("Behavior")]
    [Description("Gets or sets the activation key.")]
    public Keys ActivationKey
    {
      get
      {
        return this.activationKey;
      }
      set
      {
        this.activationKey = value;
      }
    }

    /// <summary>Gets or sets the text.</summary>
    /// <value>The text.</value>
    [Category("Appearance")]
    [Description("Gets or sets the text.")]
    public string Text
    {
      get
      {
        return this.tipText;
      }
      set
      {
        this.tipText = value;
      }
    }

    /// <summary>Gets or sets the content control.</summary>
    /// <value>The content control.</value>
    [Category("Behavior")]
    [Description("Gets or sets the content control.")]
    public Control ContentControl
    {
      get
      {
        return this.contentControl;
      }
      set
      {
        if (this.contentControl == value)
          return;
        if (this.contentControl != null)
          this.toolTip.Hide();
        if (this.contentControl != null)
          this.UnWire();
        this.contentControl = value;
        if (this.contentControl != null)
        {
          Form form = this.contentControl.FindForm();
          if (form != null)
          {
            form.Deactivate += new EventHandler(this.form_Deactivate);
            form.Layout += new LayoutEventHandler(this.form_Layout);
          }
        }
        this.Wire();
      }
    }

    /// <summary>Occurs when keytip activation key is pressed</summary>
    [Category("Action")]
    public event KeyTipEventHandler KeyTipActivationKeyPressed;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.KeyTip" /> class.
    /// </summary>
    public KeyTip()
      : this((Control) null, "1")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.KeyTip" /> class.
    /// </summary>
    /// <param name="contentControl">The content control.</param>
    /// <param name="text">The text.</param>
    public KeyTip(Control contentControl, string text)
    {
      this.tipText = text;
      this.ContentControl = contentControl;
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      Application.RemoveMessageFilter((IMessageFilter) this.filter);
    }

    protected virtual void OnKeyTipActivationKeyPressed()
    {
      if (this.KeyTipActivationKeyPressed == null)
        return;
      this.KeyTipActivationKeyPressed((object) this, new KeyTipEventArgs(this.ContentControl));
    }

    private void form_Layout(object sender, LayoutEventArgs e)
    {
    }

    private void form_Deactivate(object sender, EventArgs e)
    {
      this.toolTip.Close();
    }

    private void Wire()
    {
      Application.AddMessageFilter((IMessageFilter) this.filter);
      this.filter.KeyDown += new KeyEventHandler(this.eventProvider_KeyDown);
      this.filter.KeyUp += new KeyEventHandler(this.eventProvider_KeyUp);
      this.filter.MouseEventFired += new EventHandler(this.filter_MouseEventFired);
    }

    private void UnWire()
    {
      Application.RemoveMessageFilter((IMessageFilter) this.filter);
      this.filter.KeyDown -= new KeyEventHandler(this.eventProvider_KeyDown);
      this.filter.KeyUp -= new KeyEventHandler(this.eventProvider_KeyUp);
      this.filter.MouseEventFired -= new EventHandler(this.filter_MouseEventFired);
    }

    private void eventProvider_KeyUp(object sender, KeyEventArgs e)
    {
      if (this.ContentControl == null || !this.ContentControl.Visible || e.KeyCode != Keys.F10 && e.KeyValue != 18)
        return;
      this.checkedToolTip = !this.checkedToolTip;
      if (this.checkedToolTip)
      {
        this.toolTip.AutoClose = false;
        this.toolTip.ShowItemToolTips = false;
        if (this.contentControl != null)
        {
          Form form = this.contentControl.FindForm();
          if (form != null)
          {
            form.Deactivate -= new EventHandler(this.form_Deactivate);
            form.Layout -= new LayoutEventHandler(this.form_Layout);
            form.Deactivate += new EventHandler(this.form_Deactivate);
            form.Layout += new LayoutEventHandler(this.form_Layout);
          }
        }
        if (this.customLocation.Equals((object) Point.Empty))
          this.toolTip.Show(this.ContentControl, new Point(this.ContentControl.Width / 2 - this.toolTip.Width / 2, this.ContentControl.Height - this.toolTip.Height / 2));
        else
          this.toolTip.Show(this.ContentControl, this.customLocation);
      }
      else
        this.toolTip.Close();
    }

    private void filter_MouseEventFired(object sender, EventArgs e)
    {
      this.toolTip.Close();
    }

    private void eventProvider_KeyDown(object sender, KeyEventArgs e)
    {
      int num = (int) e.KeyData;
      if ((Keys) e.KeyValue == this.ActivationKey && this.toolTip.Visible)
        this.OnKeyTipActivationKeyPressed();
      if (e.KeyCode == Keys.F10 || e.KeyValue == 18)
      {
        this.toolTip.Items.Clear();
        this.toolTip.Items.Add(this.Text);
        this.toolTip.Items[0].AutoSize = false;
        this.toolTip.MaximumSize = this.KeyTipSize;
        this.toolTip.MinimumSize = this.KeyTipSize;
        this.toolTip.Items[0].Size = this.KeyTipSize;
        this.toolTip.Margin = Padding.Empty;
        this.toolTip.Items[0].Margin = new Padding(1, 0, 0, 0);
        this.toolTip.Padding = Padding.Empty;
      }
      else
        this.toolTip.Close();
    }

    private void parentControl_KeyUp(object sender, KeyEventArgs e)
    {
    }

    private void parentControl_KeyDown(object sender, KeyEventArgs e)
    {
    }
  }
}
