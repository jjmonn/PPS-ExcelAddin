// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vTreeViewBox
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vTreeViewBox control.</summary>
  [Designer("VIBlend.WinForms.Controls.Design.vDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (vTreeViewBox), "ControlIcons.vTreeViewBox.ico")]
  [Description("Displays an editable text field, and a drop-down TreeView of selection choices.")]
  public class vTreeViewBox : vControlBox
  {
    private vTreeView treeViewCtrl;

    /// <summary>Gets a reference to the underlying vTreeView control</summary>
    [Browsable(false)]
    [Category("Content")]
    public vTreeView TreeView
    {
      get
      {
        if (this.treeViewCtrl == null)
          this.treeViewCtrl = new vTreeView();
        return this.treeViewCtrl;
      }
    }

    static vTreeViewBox()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>Constructor</summary>
    public vTreeViewBox()
    {
      this.ContentControl = (Control) this.TreeView;
      this.treeViewCtrl.VIBlendTheme = this.VIBlendTheme;
      this.treeViewCtrl.UseThemeBackColor = false;
      this.treeViewCtrl.UseThemeBorderColor = false;
      this.EditBase.ReadOnly = true;
      this.EditBase.TextBox.ReadOnly = true;
      this.treeViewCtrl.AfterSelect += new vTreeViewEventHandler(this.OnTreeViewAfterSelect);
    }

    private void OnTreeViewAfterSelect(object sender, vTreeViewEventArgs e)
    {
      if (e.Node == null)
        this.Text = "";
      else
        this.Text = e.Node.Text;
    }

    /// <summary>
    /// Raises the <see cref="E:Paint" /> event.
    /// </summary>
    /// <param name="p">The <see cref="T:System.Windows.Forms.PaintEventArgs" /> instance containing the event data.</param>
    protected override void OnPaint(PaintEventArgs p)
    {
      if (!this.DesignMode)
        Licensing.LICheck((Control) this);
      base.OnPaint(p);
      bool flag = false;
      if (this.VIBlendTheme != this.treeViewCtrl.VIBlendTheme)
      {
        this.treeViewCtrl.VIBlendTheme = this.VIBlendTheme;
        flag = true;
      }
      if (this.treeViewCtrl.BackColor != this.BackColorDropDown || this.treeViewCtrl.UseThemeBackColor)
      {
        this.treeViewCtrl.BackColor = this.BackColorDropDown;
        this.treeViewCtrl.UseThemeBackColor = false;
        flag = true;
      }
      Color color = this.UseThemeBorderColor ? this.Theme.StyleNormal.BorderColor : this.BackColorDropDown;
      if (this.treeViewCtrl.BorderColor != color || this.treeViewCtrl.UseThemeBorderColor)
      {
        this.treeViewCtrl.BorderColor = color;
        this.treeViewCtrl.UseThemeBorderColor = false;
        flag = true;
      }
      if (!flag)
        return;
      this.treeViewCtrl.Refresh();
    }
  }
}
