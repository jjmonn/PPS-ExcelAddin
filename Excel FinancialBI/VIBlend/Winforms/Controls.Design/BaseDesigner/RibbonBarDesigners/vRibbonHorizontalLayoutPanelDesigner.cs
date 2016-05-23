// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.vRibbonHorizontalLayoutPanelDesigner
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.Layout;

namespace VIBlend.WinForms.Controls.Design
{
  public class vRibbonHorizontalLayoutPanelDesigner : ParentControlDesigner
  {
    public static int LoWord(int dwValue)
    {
      return dwValue & (int) ushort.MaxValue;
    }

    public static int HiWord(int dwValue)
    {
      return dwValue >> 16 & (int) ushort.MaxValue;
    }

    protected override void WndProc(ref Message m)
    {
      if (m.HWnd == this.Control.Handle)
      {
        switch (m.Msg)
        {
          case 514:
          case 517:
            this.HitTest(vRibbonHorizontalLayoutPanelDesigner.LoWord((int) m.LParam), vRibbonHorizontalLayoutPanelDesigner.HiWord((int) m.LParam));
            break;
        }
      }
      base.WndProc(ref m);
    }

    protected override void OnPaintAdornments(PaintEventArgs pe)
    {
      base.OnPaintAdornments(pe);
      int x = 0;
      foreach (Control control in (ArrangedElementCollection) this.Control.Controls)
      {
        if (control.Right > 0 && !(control is vArrowButton))
          x += control.Width + control.Margin.Horizontal;
      }
      Rectangle rect = new Rectangle(x, 1, 100, this.Control.Height - 2);
      using (Pen pen = new Pen(Color.Black))
      {
        pen.Color = Color.Black;
        pen.Width = 1f;
        pen.DashStyle = DashStyle.Dot;
        pe.Graphics.DrawRectangle(pen, rect);
        pe.Graphics.DrawString("Add RibbonGroup", this.Control.Font, Brushes.Black, (RectangleF) rect, ImageAndTextHelper.InitializeStringFormatAlignment(ContentAlignment.MiddleCenter));
      }
    }

    public override void Initialize(IComponent component)
    {
      base.Initialize(component);
      IComponentChangeService componentChangeService = this.GetService(typeof (IComponentChangeService)) as IComponentChangeService;
      this.GetService(typeof (IDesignerEventService));
      componentChangeService.ComponentRemoved += new ComponentEventHandler(this.changeService_ComponentRemoved);
    }

    public void changeService_ComponentRemoved(object sender, ComponentEventArgs e)
    {
    }

    private void OnAddControl(object sender, EventArgs e)
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add vRibbonGroup");
      vRibbonGroup vRibbonGroup = (vRibbonGroup) designerHost.CreateComponent(typeof (vRibbonGroup));
      this.Control.Controls.Add((Control) vRibbonGroup);
      vRibbonGroup.Size = new Size(120, 76);
      vRibbonGroup.Margin = new Padding(1, 1, 1, 1);
      transaction.Commit();
      this.Control.Invalidate();
    }

    public void HitTest(int x, int y)
    {
      if (this.Control == null)
        return;
      int x1 = 0;
      foreach (Control control in (ArrangedElementCollection) this.Control.Controls)
      {
        if (control.Right > 0 && !(control is vArrowButton))
          x1 += control.Width + control.Margin.Horizontal;
      }
      if (!new Rectangle(x1, 0, 100, this.Control.Height).Contains(x, y))
        return;
      this.OnAddControl((object) this, EventArgs.Empty);
    }
  }
}
