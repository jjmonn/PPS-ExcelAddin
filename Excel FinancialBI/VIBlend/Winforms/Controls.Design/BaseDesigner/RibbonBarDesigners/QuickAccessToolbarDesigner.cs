// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.QuickAccessToolbarDesigner
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls.Design
{
  public class QuickAccessToolbarDesigner : vDesignerBase
  {
    private QuickAccessToolbar control;

    public override DesignerActionListCollection ActionLists
    {
      get
      {
        DesignerActionListCollection actionListCollection = new DesignerActionListCollection();
        actionListCollection.AddRange(base.ActionLists);
        QuickAccessToolbarActionList toolbarActionList = new QuickAccessToolbarActionList(this.Control);
        actionListCollection.Add((DesignerActionList) toolbarActionList);
        return actionListCollection;
      }
    }

    public override DesignerVerbCollection Verbs
    {
      get
      {
        return new DesignerVerbCollection(new DesignerVerb[8]{ new DesignerVerb("Add vButton", new EventHandler(this.AddButton)), new DesignerVerb("Add vRadioButton", new EventHandler(this.AddRadioButton)), new DesignerVerb("Add vCheckBox", new EventHandler(this.AddCheckBox)), new DesignerVerb("Add vTextBox", new EventHandler(this.AddTextBox)), new DesignerVerb("Add vComboBox", new EventHandler(this.AddComboBox)), new DesignerVerb("Add vLabel", new EventHandler(this.AddLabel)), new DesignerVerb("Add vSplitButton", new EventHandler(this.AddSplitButton)), new DesignerVerb("Add vSeparator", new EventHandler(this.AddSeparator)) });
      }
    }

    public override void Initialize(IComponent component)
    {
      try
      {
        base.Initialize(component);
        this.control = component as QuickAccessToolbar;
        if (this.control == null)
          return;
        this.SetChildAsContainer((Control) this.control.DropDown, "DropDown");
      }
      catch (Exception ex)
      {
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

    protected override void OnPaintAdornments(PaintEventArgs pe)
    {
      base.OnPaintAdornments(pe);
      if (this.control == null)
        return;
      using (Pen pen = new Pen(Color.Black))
      {
        Rectangle rect = new Rectangle(0, 0, this.control.Width - 1, this.control.Height - 1);
        pen.Color = Color.Azure;
        pen.Width = 3f;
        pen.DashStyle = DashStyle.Dot;
        pe.Graphics.DrawRectangle(pen, rect);
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

    protected virtual void AddSeparator(object sender, EventArgs e)
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add CheckBox");
      vSeparator vSeparator = (vSeparator) designerHost.CreateComponent(typeof (vSeparator));
      vSeparator.Text = "vSeparator";
      vSeparator.Size = new Size(2, 16);
      this.control.Content.Controls.Add((Control) vSeparator);
      transaction.Commit();
    }

    protected virtual void AddCheckBox(object sender, EventArgs e)
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add CheckBox");
      vCheckBox vCheckBox = (vCheckBox) designerHost.CreateComponent(typeof (vCheckBox));
      vCheckBox.Text = "CheckBox";
      vCheckBox.Size = new Size(16, 16);
      this.control.Content.Controls.Add((Control) vCheckBox);
      transaction.Commit();
    }

    protected virtual void AddRadioButton(object sender, EventArgs e)
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add RadioButton");
      vRadioButton vRadioButton = (vRadioButton) designerHost.CreateComponent(typeof (vRadioButton));
      vRadioButton.Text = "RadioButton";
      vRadioButton.Size = new Size(16, 16);
      this.control.Content.Controls.Add((Control) vRadioButton);
      transaction.Commit();
    }

    protected virtual void AddButton(object sender, EventArgs e)
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add Button");
      vButton vButton = (vButton) designerHost.CreateComponent(typeof (vButton));
      vButton.Size = new Size(16, 16);
      vButton.Text = "Button";
      this.control.Content.Controls.Add((Control) vButton);
      transaction.Commit();
    }

    protected virtual void AddTextBox(object sender, EventArgs e)
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add TextBox");
      vTextBox vTextBox = (vTextBox) designerHost.CreateComponent(typeof (vTextBox));
      this.control.Content.Controls.Add((Control) vTextBox);
      vTextBox.MinimumSize = new Size(100, 16);
      vTextBox.Size = new Size(16, 16);
      transaction.Commit();
    }

    protected virtual void AddComboBox(object sender, EventArgs e)
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add ComboBox");
      vComboBox vComboBox = (vComboBox) designerHost.CreateComponent(typeof (vComboBox));
      this.control.Content.Controls.Add((Control) vComboBox);
      vComboBox.MinimumSize = new Size(100, 16);
      vComboBox.Size = new Size(16, 16);
      transaction.Commit();
    }

    protected virtual void AddLabel(object sender, EventArgs e)
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add Label");
      vLabel vLabel = (vLabel) designerHost.CreateComponent(typeof (vLabel));
      vLabel.Size = new Size(16, 16);
      this.control.Content.Controls.Add((Control) vLabel);
      transaction.Commit();
    }

    protected virtual void AddSplitButton(object sender, EventArgs e)
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add SplitButton");
      vSplitButton vSplitButton = (vSplitButton) designerHost.CreateComponent(typeof (vSplitButton));
      vSplitButton.Text = "Button";
      vSplitButton.Size = new Size(16, 16);
      this.control.Content.Controls.Add((Control) vSplitButton);
      transaction.Commit();
    }
  }
}
