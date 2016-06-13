// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.vRibbonBarDesigner
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
  public class vRibbonBarDesigner : vDesignerBase
  {
    private Component selectedElement;

    public vRibbonBar RibbonBar
    {
      get
      {
        return this.Control as vRibbonBar;
      }
    }

    public override DesignerActionListCollection ActionLists
    {
      get
      {
        DesignerActionListCollection actionListCollection = new DesignerActionListCollection();
        actionListCollection.AddRange(base.ActionLists);
        vRibbonBarDesignerActionList designerActionList = new vRibbonBarDesignerActionList((Control) this.RibbonBar);
        actionListCollection.Add((DesignerActionList) designerActionList);
        return actionListCollection;
      }
    }

    public Component SelectedElement
    {
      get
      {
        return this.selectedElement;
      }
      set
      {
        this.selectedElement = value;
        ISelectionService selectionService = this.GetService(typeof (ISelectionService)) as ISelectionService;
        if (selectionService != null && value != null)
          selectionService.SetSelectedComponents((ICollection) new Component[1]{ value }, SelectionTypes.Click);
        this.RibbonBar.Refresh();
      }
    }

    public static int LoWord(int dwValue)
    {
      return dwValue & (int) ushort.MaxValue;
    }

    public static int HiWord(int dwValue)
    {
      return dwValue >> 16 & (int) ushort.MaxValue;
    }

    private void SelectRibbon()
    {
      ISelectionService selectionService = this.GetService(typeof (ISelectionService)) as ISelectionService;
      if (selectionService == null)
        return;
      selectionService.SetSelectedComponents((ICollection) new Component[1]
      {
        (Component) this.RibbonBar
      }, SelectionTypes.Click);
    }

    protected override bool GetHitTest(Point point)
    {
      if (point.Y > 24)
        return true;
      return base.GetHitTest(point);
    }

    protected override void OnPaintAdornments(PaintEventArgs pe)
    {
      base.OnPaintAdornments(pe);
      if (this.RibbonBar == null)
        return;
      int val2 = this.RibbonBar.TabsInitialOffset;
      int num = 0;
      foreach (vTabPage tabPage in this.RibbonBar.TabPages)
      {
        Rectangle pageRectangle = this.RibbonBar.GetPageRectangle(pe.Graphics, tabPage);
        val2 = Math.Max(pageRectangle.Right, val2);
        num = Math.Max(num, pageRectangle.Height);
      }
      int x = val2 + this.RibbonBar.TabsSpacing;
      Rectangle rect = new Rectangle(x, this.RibbonBar.TitleHeight - num, 75, num);
      if (num == 0)
      {
        Size size = Size.Ceiling(pe.Graphics.MeasureString("Add New Tab", this.RibbonBar.Font));
        rect = new Rectangle(x + 2, this.RibbonBar.TitleHeight - size.Height - 8, 75, 8 + size.Height);
      }
      using (Pen pen = new Pen(Color.Black))
      {
        pen.Color = Color.Black;
        pen.Width = 1f;
        pen.DashStyle = DashStyle.Dot;
        pe.Graphics.DrawRectangle(pen, rect);
        pe.Graphics.DrawString("Add New Tab", this.RibbonBar.Font, Brushes.Black, (RectangleF) rect, ImageAndTextHelper.InitializeStringFormatAlignment(ContentAlignment.MiddleCenter));
      }
    }

    protected override void WndProc(ref Message m)
    {
      if (m.HWnd == this.Control.Handle)
      {
        switch (m.Msg)
        {
          case 514:
          case 517:
            this.HitTest(vRibbonBarDesigner.LoWord((int) m.LParam), vRibbonBarDesigner.HiWord((int) m.LParam));
            break;
        }
      }
      if (m.Msg == 513 && this.RibbonBar != null)
      {
        vTabPage vTabPage = this.RibbonBar.HitTest(this.RibbonBar.PointToClient(Cursor.Position));
        if (vTabPage != null)
        {
          this.RibbonBar.SelectedTab = vTabPage;
          ((ISelectionService) this.GetService(typeof (ISelectionService))).SetSelectedComponents((ICollection) new ArrayList()
          {
            (object) vTabPage
          });
        }
        if (this.RibbonBar.SelectedTab != null && this.RibbonBar.SelectedTab.RectangleToScreen(this.RibbonBar.SelectedTab.ClientRectangle).Contains(Cursor.Position))
          ((ISelectionService) this.GetService(typeof (ISelectionService))).SetSelectedComponents((ICollection) new ArrayList()
          {
            (object) this.RibbonBar.SelectedTab.LayoutPanel
          });
      }
      base.WndProc(ref m);
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
      DesignerTransaction transaction = designerHost.CreateTransaction("Add Page");
      vTabPage vTabPage = (vTabPage) designerHost.CreateComponent(typeof (vTabPage));
      this.RibbonBar.TabPages.Add(vTabPage);
      vTabPage.Dock = DockStyle.None;
      this.RibbonBar.Invalidate();
      transaction.Commit();
    }

    public void HitTest(int x, int y)
    {
      if (this.RibbonBar == null)
        return;
      if (y < 23)
      {
        this.SelectRibbon();
      }
      else
      {
        using (Graphics graphics = this.RibbonBar.CreateGraphics())
        {
          int val2 = this.RibbonBar.TabsInitialOffset;
          int num = 0;
          foreach (vTabPage tabPage in this.RibbonBar.TabPages)
          {
            Rectangle pageRectangle = this.RibbonBar.GetPageRectangle(graphics, tabPage);
            val2 = Math.Max(pageRectangle.Right, val2);
            num = Math.Max(num, pageRectangle.Height);
          }
          int x1 = val2 + this.RibbonBar.TabsSpacing;
          Rectangle rectangle = new Rectangle(x1, this.RibbonBar.TitleHeight - num, 75, num);
          if (num == 0)
          {
            Size size = Size.Ceiling(graphics.MeasureString("Add New Tab", this.RibbonBar.Font));
            rectangle = new Rectangle(x1, this.RibbonBar.TitleHeight - size.Height - 8, 75, 8 + size.Height);
          }
          if (!rectangle.Contains(x, y))
            return;
          this.OnAddControl((object) this, EventArgs.Empty);
        }
      }
    }
  }
}
