// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.DesignerActionListBase
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls.Design
{
  public class DesignerActionListBase : DesignerActionList
  {
    public virtual Control ControlBase
    {
      get
      {
        return this.Component as Control;
      }
    }

    public VIBLEND_THEME Theme
    {
      get
      {
        try
        {
          return (VIBLEND_THEME) TypeDescriptor.GetProperties((object) this.ControlBase)["VIBlendTheme"].GetValue((object) this.ControlBase);
        }
        catch
        {
          return VIBLEND_THEME.VISTABLUE;
        }
      }
      set
      {
        try
        {
          TypeDescriptor.GetProperties((object) this.ControlBase)["VIBlendTheme"].SetValue((object) this.ControlBase, (object) value);
        }
        catch (Exception ex)
        {
          int num = (int) MessageBox.Show(ex.Message);
        }
      }
    }

    public DockStyle Dock
    {
      get
      {
        return (DockStyle) TypeDescriptor.GetProperties((object) this.ControlBase)["Dock"].GetValue((object) this.ControlBase);
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.ControlBase)["Dock"].SetValue((object) this.ControlBase, (object) value);
      }
    }

    public DesignerActionListBase(Control ctrl)
      : base((IComponent) ctrl)
    {
    }

    public override DesignerActionItemCollection GetSortedActionItems()
    {
      return new DesignerActionItemCollection() { (DesignerActionItem) new DesignerActionPropertyItem("Dock", "Dock"), (DesignerActionItem) new DesignerActionPropertyItem("Theme", "Theme"), (DesignerActionItem) new DesignerActionMethodItem((DesignerActionList) this, "SupportPage", "Get Technical Support", "Properties", "Get professional help, technical assistence and advice."), (DesignerActionItem) new DesignerActionMethodItem((DesignerActionList) this, "BugReport", "Report a problem", "Properties", "Report a problem"), (DesignerActionItem) new DesignerActionMethodItem((DesignerActionList) this, "FeatureRequest", "Request a feature", "Properties", "Tell us what you need. We'll be happy to hear your feedback and suggestions."), (DesignerActionItem) new DesignerActionMethodItem((DesignerActionList) this, "Web", "Visit our website", "Properties", "Visit our website") };
    }

    protected virtual void SupportPage()
    {
      Cursor current = Cursor.Current;
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        Process.Start(string.Format("http://www.viblend.com/support.aspx"));
      }
      finally
      {
        Cursor.Current = current;
      }
    }

    protected virtual void FeatureRequest()
    {
      Cursor current = Cursor.Current;
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        Process.Start(string.Format("http://www.viblend.com/customerfeedback.aspx"));
      }
      finally
      {
        Cursor.Current = current;
      }
    }

    protected virtual void Web()
    {
      Cursor current = Cursor.Current;
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        Process.Start(string.Format("http://www.viblend.com/"));
      }
      finally
      {
        Cursor.Current = current;
      }
    }

    protected virtual void Documentation()
    {
      Cursor current = Cursor.Current;
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        Process.Start(string.Format("http://www.viblend.com/products/winforms/controls/api/help/help.html"));
      }
      finally
      {
        Cursor.Current = current;
      }
    }

    protected virtual void BugReport()
    {
      Cursor current = Cursor.Current;
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        Process.Start(string.Format("http://www.viblend.com/customerfeedback.aspx"));
      }
      finally
      {
        Cursor.Current = current;
      }
    }
  }
}
