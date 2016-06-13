// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.vApplicationMenuItemDesigner
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls.Design
{
  public class vApplicationMenuItemDesigner : vDesignerBase
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
            ISelectionService selectionService = this.GetService(typeof (ISelectionService)) as ISelectionService;
            if (selectionService != null)
            {
              selectionService.SelectionChanged -= new EventHandler(this.selector_SelectionChanged);
              selectionService.SelectionChanged += new EventHandler(this.selector_SelectionChanged);
            }
            this.HitOn(vApplicationMenuItemDesigner.LoWord((int) m.LParam), vApplicationMenuItemDesigner.HiWord((int) m.LParam));
            break;
        }
      }
      base.WndProc(ref m);
    }

    private void selector_SelectionChanged(object sender, EventArgs e)
    {
      ISelectionService selectionService = this.GetService(typeof (ISelectionService)) as ISelectionService;
      vApplicationMenuItem applicationMenuItem = this.Component as vApplicationMenuItem;
      if (applicationMenuItem == null)
        return;
      foreach (object selectedComponent in (IEnumerable) selectionService.GetSelectedComponents())
      {
        if (!(selectedComponent is vApplicationMenuItem))
          applicationMenuItem.CloseDropDown();
      }
    }

    protected override void OnPaintAdornments(PaintEventArgs pe)
    {
      base.OnPaintAdornments(pe);
      using (Pen pen = new Pen(Color.Black))
      {
        vApplicationMenuItem applicationMenuItem = this.Component as vApplicationMenuItem;
        if (applicationMenuItem == null)
          return;
        Rectangle rect = new Rectangle(0, 0, applicationMenuItem.Width - 1, applicationMenuItem.Height - 1);
        pen.Color = Color.Azure;
        pen.Width = 3f;
        pen.DashStyle = DashStyle.Dot;
        pe.Graphics.DrawRectangle(pen, rect);
      }
    }

    protected override bool GetHitTest(Point point)
    {
      return true;
    }

    public void HitOn(int x, int y)
    {
      vApplicationMenuItem applicationMenuItem1 = this.Component as vApplicationMenuItem;
      if (applicationMenuItem1 == null)
        return;
      ISelectionService selectionService = this.GetService(typeof (ISelectionService)) as ISelectionService;
      if (selectionService == null)
        return;
      if (applicationMenuItem1.ClientRectangle.Contains(x, y))
      {
        if (selectionService != null)
          selectionService.SetSelectedComponents((ICollection) new Component[1]
          {
            (Component) applicationMenuItem1
          }, SelectionTypes.Click);
        if (applicationMenuItem1.ParentMenuItem != null)
        {
          foreach (vApplicationMenuItem applicationMenuItem2 in applicationMenuItem1.ParentMenuItem.Items)
          {
            if (applicationMenuItem1 != applicationMenuItem2)
              applicationMenuItem2.CloseDropDown();
          }
        }
        if (!applicationMenuItem1.opened)
        {
          vApplicationMenuItem parentMenuItem = applicationMenuItem1.ParentMenuItem;
          applicationMenuItem1.ShowDropDown();
        }
        else
        {
          applicationMenuItem1.CloseDropDown();
          selectionService.SetSelectedComponents((ICollection) new Component[1]
          {
            (Component) applicationMenuItem1.RootMenuItem.Parent
          }, SelectionTypes.Click);
        }
      }
      else
      {
        applicationMenuItem1.RootMenuItem.CloseDropDown();
        selectionService.SetSelectedComponents((ICollection) new Component[1]
        {
          (Component) applicationMenuItem1.RootMenuItem.Parent
        }, SelectionTypes.Click);
      }
    }

    private void DropDown_Closed(object sender, ToolStripDropDownClosedEventArgs e)
    {
      ISelectionService selectionService = this.GetService(typeof (ISelectionService)) as ISelectionService;
      if (selectionService == null)
        return;
      selectionService.SetSelectedComponents((ICollection) new Component[1]
      {
        (Component) this.Control
      }, SelectionTypes.Click);
    }
  }
}
