// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.QuickAccessToolbarActionList
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls.Design
{
  public class QuickAccessToolbarActionList : DesignerActionList
  {
    public Control ControlBase
    {
      get
      {
        return this.Component as Control;
      }
    }

    public QuickAccessToolbarActionList(Control ctrl)
      : base((IComponent) ctrl)
    {
    }

    public override DesignerActionItemCollection GetSortedActionItems()
    {
      return new DesignerActionItemCollection() { (DesignerActionItem) new DesignerActionMethodItem((DesignerActionList) this, "AddButton", "Add vButton"), (DesignerActionItem) new DesignerActionMethodItem((DesignerActionList) this, "AddRadioButton", "Add vRadioButton"), (DesignerActionItem) new DesignerActionMethodItem((DesignerActionList) this, "AddCheckBox", "Add vCheckBox"), (DesignerActionItem) new DesignerActionMethodItem((DesignerActionList) this, "AddTextBox", "Add vTextBox"), (DesignerActionItem) new DesignerActionMethodItem((DesignerActionList) this, "AddComboBox", "Add vComboBox"), (DesignerActionItem) new DesignerActionMethodItem((DesignerActionList) this, "AddLabel", "Add vLabel"), (DesignerActionItem) new DesignerActionMethodItem((DesignerActionList) this, "AddSplitButton", "Add vSplitButton"), (DesignerActionItem) new DesignerActionMethodItem((DesignerActionList) this, "AddSeparator", "Add Separator") };
    }

    protected virtual void AddSeparator()
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add Button");
      vSeparator vSeparator = (vSeparator) designerHost.CreateComponent(typeof (vSeparator));
      vSeparator.Text = "separator";
      (this.ControlBase as QuickAccessToolbar).Content.Controls.Add((Control) vSeparator);
      vSeparator.Size = new Size(2, 16);
      transaction.Commit();
    }

    protected virtual void AddButton()
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add Button");
      vButton vButton = (vButton) designerHost.CreateComponent(typeof (vButton));
      vButton.Text = "Button";
      (this.ControlBase as QuickAccessToolbar).Content.Controls.Add((Control) vButton);
      vButton.Size = new Size(16, 16);
      transaction.Commit();
    }

    protected virtual void AddTextBox()
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add TextBox");
      vTextBox vTextBox = (vTextBox) designerHost.CreateComponent(typeof (vTextBox));
      (this.ControlBase as QuickAccessToolbar).Content.Controls.Add((Control) vTextBox);
      vTextBox.MinimumSize = new Size(100, 16);
      vTextBox.Size = new Size(16, 16);
      transaction.Commit();
    }

    protected virtual void AddComboBox()
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add ComboBox");
      vComboBox vComboBox = (vComboBox) designerHost.CreateComponent(typeof (vComboBox));
      (this.ControlBase as QuickAccessToolbar).Content.Controls.Add((Control) vComboBox);
      vComboBox.MinimumSize = new Size(100, 16);
      vComboBox.Size = new Size(16, 16);
      transaction.Commit();
    }

    protected virtual void AddLabel()
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add Label");
      vLabel vLabel = (vLabel) designerHost.CreateComponent(typeof (vLabel));
      (this.ControlBase as QuickAccessToolbar).Content.Controls.Add((Control) vLabel);
      vLabel.Size = new Size(16, 16);
      transaction.Commit();
    }

    protected virtual void AddRadioButton()
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add RadioButton");
      vRadioButton vRadioButton = (vRadioButton) designerHost.CreateComponent(typeof (vRadioButton));
      vRadioButton.Text = "Button";
      (this.ControlBase as QuickAccessToolbar).Content.Controls.Add((Control) vRadioButton);
      vRadioButton.Size = new Size(16, 16);
      transaction.Commit();
    }

    protected virtual void AddCheckBox()
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add CheckBox");
      vCheckBox vCheckBox = (vCheckBox) designerHost.CreateComponent(typeof (vCheckBox));
      vCheckBox.Text = "Button";
      (this.ControlBase as QuickAccessToolbar).Content.Controls.Add((Control) vCheckBox);
      vCheckBox.Size = new Size(16, 16);
      transaction.Commit();
    }

    protected virtual void AddSplitButton()
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add SplitButton");
      vSplitButton vSplitButton = (vSplitButton) designerHost.CreateComponent(typeof (vSplitButton));
      vSplitButton.Text = "Button";
      (this.ControlBase as QuickAccessToolbar).Content.Controls.Add((Control) vSplitButton);
      vSplitButton.Size = new Size(16, 16);
      transaction.Commit();
    }
  }
}
