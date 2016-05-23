// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vSplitterPanel
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace VIBlend.WinForms.Controls
{
  [ToolboxItem(false)]
  [Browsable(false)]
  [Designer("VIBlend.WinForms.Controls.Design.vSplitPanelDesigner, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  public class vSplitterPanel : Panel
  {
    private Color borderColor = Color.Silver;

    /// <summary>Gets or sets the color of the border.</summary>
    /// <value>The color of the border.</value>
    [Description("Gets or sets the color of the border.")]
    public Color BorderColor
    {
      get
      {
        return this.borderColor;
      }
      set
      {
        this.borderColor = value;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vSplitterPanel" /> class.
    /// </summary>
    public vSplitterPanel()
    {
      this.SetStyle(ControlStyles.ContainerControl | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.SizeChanged += new EventHandler(this.vSplitterPanel_SizeChanged);
      this.BackColor = Color.White;
    }

    private void vSplitterPanel_SizeChanged(object sender, EventArgs e)
    {
      foreach (Control control in (ArrangedElementCollection) this.Controls)
        control.Invalidate();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
        e.Graphics.FillRectangle((Brush) solidBrush, this.ClientRectangle);
      using (Pen pen = new Pen(this.BorderColor))
      {
        Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
        e.Graphics.DrawRectangle(pen, rect);
      }
    }
  }
}
