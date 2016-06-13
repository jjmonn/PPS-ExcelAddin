// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.vDropDownForm
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
  public class vDropDownForm : Form
  {
    private Container components;
    public bool AllowHideForm;
    protected Control ParentControl;

    /// <summary>Occurs when form is to be hidden</summary>
    [Category("Behavior")]
    public event EventHandler HideForm;

    /// <summary>Occurs when form is shown</summary>
    [Category("Behavior")]
    public event EventHandler ShowForm;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.Utilities.vDropDownForm" /> class.
    /// </summary>
    public vDropDownForm()
      : this((Control) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.Utilities.vDropDownForm" /> class.
    /// </summary>
    /// <param name="parentControl">The parent control.</param>
    public vDropDownForm(Control parentControl)
    {
      this.components = (Container) null;
      this.InitializeComponent();
      this.SetStyle(ControlStyles.ResizeRedraw, true);
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.Selectable, false);
      this.TabStop = false;
      this.ParentControl = parentControl;
      this.ControlBox = false;
      this.ShowInTaskbar = false;
    }

    /// <summary>Hides the form and activate parent.</summary>
    public virtual void HideFormAndActivateParent()
    {
      if (this.AllowHideForm)
        return;
      if (this.ParentControl != null)
      {
        Form form = this.ParentControl.FindForm();
        if (form != null)
          form.Activate();
      }
      this.Close();
      this.CallHideForm(EventArgs.Empty);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.AutoScaleBaseSize = new Size(5, 13);
      this.FormBorderStyle = FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "VIBlendPopupForm";
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = "VIBlendPopupForm";
      this.TopMost = false;
      this.Deactivate += new EventHandler(this.vPopupForm_Close);
    }

    /// <summary>Calls the hide form.</summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    public void CallHideForm(EventArgs e)
    {
      this.OnHideForm(e);
    }

    /// <summary>Calls the show form.</summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    public void CallShowForm(EventArgs e)
    {
      this.OnShowForm(e);
    }

    /// <summary>
    /// Raises the <see cref="E:HideForm" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    protected virtual void OnHideForm(EventArgs e)
    {
      if (this.HideForm == null)
        return;
      this.HideForm((object) this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:ShowForm" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    protected virtual void OnShowForm(EventArgs e)
    {
      if (this.ShowForm == null)
        return;
      this.ShowForm((object) this, e);
    }

    /// <summary>Shows the popup form.</summary>
    public virtual void ShowPopupForm()
    {
      this.Show();
      this.CallShowForm(EventArgs.Empty);
    }

    private void vPopupForm_Close(object sender, EventArgs e)
    {
      this.HideFormAndActivateParent();
    }
  }
}
