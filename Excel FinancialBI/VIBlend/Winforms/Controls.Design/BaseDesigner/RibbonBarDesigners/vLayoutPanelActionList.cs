// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.vLayoutPanelActionList
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls.Design
{
  public class vLayoutPanelActionList : DesignerActionList
  {
    public Control ControlBase
    {
      get
      {
        return this.Component as Control;
      }
    }

    public vLayoutPanelActionList(Control ctrl)
      : base((IComponent) ctrl)
    {
    }

    public override DesignerActionItemCollection GetSortedActionItems()
    {
      return new DesignerActionItemCollection() { (DesignerActionItem) new DesignerActionMethodItem((DesignerActionList) this, "AddButton", "Add vButton"), (DesignerActionItem) new DesignerActionMethodItem((DesignerActionList) this, "AddRadioButton", "Add vRadioButton"), (DesignerActionItem) new DesignerActionMethodItem((DesignerActionList) this, "AddCheckBox", "Add vCheckBox"), (DesignerActionItem) new DesignerActionMethodItem((DesignerActionList) this, "AddTextBox", "Add vTextBox"), (DesignerActionItem) new DesignerActionMethodItem((DesignerActionList) this, "AddComboBox", "Add vComboBox"), (DesignerActionItem) new DesignerActionMethodItem((DesignerActionList) this, "AddLabel", "Add vLabel"), (DesignerActionItem) new DesignerActionMethodItem((DesignerActionList) this, "AddSplitButton", "Add vSplitButton") };
    }

    protected virtual void AddButton()
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add Button");
      vButton vButton = (vButton) designerHost.CreateComponent(typeof (vButton));
      vButton.Text = "Button";
      Control control = this.ControlBase;
      if (control is vFlowLayoutPanel)
        control = (Control) (control as vFlowLayoutPanel).Content;
      control.Controls.Add((Control) vButton);
      transaction.Commit();
    }

    protected virtual void AddTextBox()
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add TextBox");
      vTextBox vTextBox = (vTextBox) designerHost.CreateComponent(typeof (vTextBox));
      Control control = this.ControlBase;
      if (control is vFlowLayoutPanel)
        control = (Control) (control as vFlowLayoutPanel).Content;
      control.Controls.Add((Control) vTextBox);
      vTextBox.MinimumSize = new Size(100, 20);
      transaction.Commit();
    }

    protected virtual void AddComboBox()
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add ComboBox");
      vComboBox vComboBox = (vComboBox) designerHost.CreateComponent(typeof (vComboBox));
      Control control = this.ControlBase;
      if (control is vFlowLayoutPanel)
        control = (Control) (control as vFlowLayoutPanel).Content;
      control.Controls.Add((Control) vComboBox);
      vComboBox.MinimumSize = new Size(100, 20);
      transaction.Commit();
    }

    protected virtual void AddLabel()
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add Label");
      vLabel vLabel = (vLabel) designerHost.CreateComponent(typeof (vLabel));
      Control control = this.ControlBase;
      if (control is vFlowLayoutPanel)
        control = (Control) (control as vFlowLayoutPanel).Content;
      control.Controls.Add((Control) vLabel);
      transaction.Commit();
    }

    protected virtual void AddRadioButton()
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add RadioButton");
      vRadioButton vRadioButton = (vRadioButton) designerHost.CreateComponent(typeof (vRadioButton));
      vRadioButton.Text = "Button";
      Control control = this.ControlBase;
      if (control is vFlowLayoutPanel)
        control = (Control) (control as vFlowLayoutPanel).Content;
      control.Controls.Add((Control) vRadioButton);
      transaction.Commit();
    }

    protected virtual void AddCheckBox()
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add CheckBox");
      vCheckBox vCheckBox = (vCheckBox) designerHost.CreateComponent(typeof (vCheckBox));
      vCheckBox.Text = "Button";
      Control control = this.ControlBase;
      if (control is vFlowLayoutPanel)
        control = (Control) (control as vFlowLayoutPanel).Content;
      control.Controls.Add((Control) vCheckBox);
      transaction.Commit();
    }

    protected virtual void AddSplitButton()
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add SplitButton");
      vSplitButton vSplitButton = (vSplitButton) designerHost.CreateComponent(typeof (vSplitButton));
      vSplitButton.Text = "Button";
      Control control = this.ControlBase;
      if (control is vFlowLayoutPanel)
        control = (Control) (control as vFlowLayoutPanel).Content;
      control.Controls.Add((Control) vSplitButton);
      transaction.Commit();
    }
  }
}
