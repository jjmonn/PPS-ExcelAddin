// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.vLayoutPanelDesigner
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls.Design
{
  public class vLayoutPanelDesigner : vDesignerBase
  {
    private Control panel;

    public override DesignerActionListCollection ActionLists
    {
      get
      {
        DesignerActionListCollection actionListCollection = new DesignerActionListCollection();
        actionListCollection.AddRange(base.ActionLists);
        vLayoutPanelActionList layoutPanelActionList = new vLayoutPanelActionList(this.Control);
        actionListCollection.Add((DesignerActionList) layoutPanelActionList);
        return actionListCollection;
      }
    }

    public override DesignerVerbCollection Verbs
    {
      get
      {
        return new DesignerVerbCollection(new DesignerVerb[7]{ new DesignerVerb("Add vButton", new EventHandler(this.AddButton)), new DesignerVerb("Add vRadioButton", new EventHandler(this.AddRadioButton)), new DesignerVerb("Add vCheckBoxButton", new EventHandler(this.AddCheckBox)), new DesignerVerb("Add vTextBox", new EventHandler(this.AddTextBox)), new DesignerVerb("Add vComboBox", new EventHandler(this.AddComboBox)), new DesignerVerb("Add vLabel", new EventHandler(this.AddLabel)), new DesignerVerb("Add vSplitButton", new EventHandler(this.AddSplitButton)) });
      }
    }

    public bool SetChildAsContainer(Control control, string name)
    {
      try
      {
        INestedContainer nestedContainer = this.GetService(typeof (INestedContainer)) as INestedContainer;
        if (nestedContainer == null)
          return false;
        for (int index = 0; index < nestedContainer.Components.Count; ++index)
        {
          if (nestedContainer.Components[index].Equals((object) control))
            return true;
        }
        nestedContainer.Add((IComponent) control, name);
        return true;
      }
      catch (Exception ex)
      {
        Trace.WriteLine(ex.Message);
      }
      return false;
    }

    protected virtual void AddCheckBox(object sender, EventArgs e)
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add RadioButton");
      vCheckBox vCheckBox = (vCheckBox) designerHost.CreateComponent(typeof (vCheckBox));
      vCheckBox.Text = "Button";
      this.panel.Controls.Add((Control) vCheckBox);
      transaction.Commit();
    }

    protected virtual void AddRadioButton(object sender, EventArgs e)
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add RadioButton");
      vRadioButton vRadioButton = (vRadioButton) designerHost.CreateComponent(typeof (vRadioButton));
      vRadioButton.Text = "Button";
      this.panel.Controls.Add((Control) vRadioButton);
      transaction.Commit();
    }

    protected virtual void AddButton(object sender, EventArgs e)
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add Button");
      vButton vButton = (vButton) designerHost.CreateComponent(typeof (vButton));
      vButton.Text = "Button";
      this.panel.Controls.Add((Control) vButton);
      transaction.Commit();
    }

    protected virtual void AddTextBox(object sender, EventArgs e)
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add TextBox");
      vTextBox vTextBox = (vTextBox) designerHost.CreateComponent(typeof (vTextBox));
      this.panel.Controls.Add((Control) vTextBox);
      vTextBox.MinimumSize = new Size(100, 20);
      transaction.Commit();
    }

    protected virtual void AddComboBox(object sender, EventArgs e)
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add ComboBox");
      vComboBox vComboBox = (vComboBox) designerHost.CreateComponent(typeof (vComboBox));
      this.panel.Controls.Add((Control) vComboBox);
      vComboBox.MinimumSize = new Size(100, 20);
      transaction.Commit();
    }

    protected virtual void AddLabel(object sender, EventArgs e)
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add Label");
      this.panel.Controls.Add((Control) designerHost.CreateComponent(typeof (vLabel)));
      transaction.Commit();
    }

    protected virtual void AddSplitButton(object sender, EventArgs e)
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add SplitButton");
      vSplitButton vSplitButton = (vSplitButton) designerHost.CreateComponent(typeof (vSplitButton));
      vSplitButton.Text = "Button";
      this.panel.Controls.Add((Control) vSplitButton);
      transaction.Commit();
    }

    public override void Initialize(IComponent component)
    {
      base.Initialize(component);
      this.panel = (Control) component;
      if (!(this.panel is vFlowLayoutPanel))
        return;
      this.panel = (Control) (this.panel as vFlowLayoutPanel).Content;
      this.SetChildAsContainer(this.panel, "Content");
    }

    protected override bool GetHitTest(Point point)
    {
      if (this.panel == null)
        return base.GetHitTest(point);
      Rectangle screen = this.panel.RectangleToScreen(this.panel.ClientRectangle);
      if (this.panel is vHorizontalLayoutPanel)
      {
        if (((this.panel as vHorizontalLayoutPanel).CanScrollRight() || (this.panel as vHorizontalLayoutPanel).scrollOffset != 0) && (point.X > screen.Right - 8 || point.X < screen.X + 8))
          return true;
        return base.GetHitTest(point);
      }
      if (this.panel is vVerticalLayoutPanel && (!(this.panel as vVerticalLayoutPanel).CanScrollDown() && (this.panel as vVerticalLayoutPanel).scrollOffset == 0 || point.Y <= screen.Bottom - 8 && point.Y >= screen.Top + 8))
        return base.GetHitTest(point);
      return true;
    }
  }
}
