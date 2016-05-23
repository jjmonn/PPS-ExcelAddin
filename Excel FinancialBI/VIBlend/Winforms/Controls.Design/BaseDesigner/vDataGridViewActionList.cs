// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.vDataGridViewActionList
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using VIBlend.WinForms.DataGridView;

namespace VIBlend.WinForms.Controls.Design
{
  public class vDataGridViewActionList : DesignerActionList
  {
    private DesignerActionUIService uiService;
    private vDataGridViewDesignerBase _owner;

    public vDataGridView ControlBase
    {
      get
      {
        return this.Component as vDataGridView;
      }
    }

    public bool EnableColumnChooser
    {
      get
      {
        return (bool) TypeDescriptor.GetProperties((object) this.ControlBase)["EnableColumnChooser"].GetValue((object) this.ControlBase);
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.ControlBase)["EnableColumnChooser"].SetValue((object) this.ControlBase, (object) value);
      }
    }

    [AttributeProvider(typeof (IListSource))]
    public object DataSource
    {
      get
      {
        return ((vDataGridView) this.Component).DataSource;
      }
      set
      {
        vDataGridView vDataGridView = (vDataGridView) this.Component;
        IDesignerHost designerHost = this.GetService(typeof (IDesignerHost)) as IDesignerHost;
        IComponentChangeService componentChangeService = this.GetService(typeof (IComponentChangeService)) as IComponentChangeService;
        PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties((object) vDataGridView)["DataSource"];
        if (designerHost == null || componentChangeService == null)
          return;
        using (DesignerTransaction transaction = designerHost.CreateTransaction("Set DataGridView DataSource"))
        {
          componentChangeService.OnComponentChanging((object) this.Component, (MemberDescriptor) propertyDescriptor);
          if (value is BindingSource)
          {
            vDataGridView.DataSource = (value as BindingSource).DataSource;
            vDataGridView.DataMember = (value as BindingSource).DataMember;
          }
          else
            vDataGridView.DataSource = value;
          if (vDataGridView.Parent != null && vDataGridView.BindingContext == null)
            vDataGridView.BindingContext = vDataGridView.Parent.BindingContext;
          componentChangeService.OnComponentChanged((object) this.Component, (MemberDescriptor) propertyDescriptor, (object) null, (object) null);
          transaction.Commit();
          vDataGridView.DataBind();
          vDataGridView.ColumnsHierarchy.AutoResize();
          this.RefreshPanelContent();
        }
      }
    }

    public ImageList ImageList
    {
      get
      {
        return (ImageList) TypeDescriptor.GetProperties((object) this.ControlBase)["ImageList"].GetValue((object) this.ControlBase);
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.ControlBase)["ImageList"].SetValue((object) this.ControlBase, (object) value);
      }
    }

    public bool EnableResizeToolTip
    {
      get
      {
        return (bool) TypeDescriptor.GetProperties((object) this.ControlBase)["EnableResizeToolTip"].GetValue((object) this.ControlBase);
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.ControlBase)["EnableResizeToolTip"].SetValue((object) this.ControlBase, (object) value);
      }
    }

    public GridLinesDisplayMode GridLinesDisplayMode
    {
      get
      {
        return (GridLinesDisplayMode) TypeDescriptor.GetProperties((object) this.ControlBase)["GridLinesDisplayMode"].GetValue((object) this.ControlBase);
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.ControlBase)["GridLinesDisplayMode"].SetValue((object) this.ControlBase, (object) value);
      }
    }

    public DashStyle GridLinesDashStyle
    {
      get
      {
        return (DashStyle) TypeDescriptor.GetProperties((object) this.ControlBase)["GridLinesDashStyle"].GetValue((object) this.ControlBase);
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.ControlBase)["GridLinesDashStyle"].SetValue((object) this.ControlBase, (object) value);
      }
    }

    public vDataGridView.SELECTION_MODE SelectionMode
    {
      get
      {
        return (vDataGridView.SELECTION_MODE) TypeDescriptor.GetProperties((object) this.ControlBase)["SelectionMode"].GetValue((object) this.ControlBase);
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.ControlBase)["SelectionMode"].SetValue((object) this.ControlBase, (object) value);
      }
    }

    public bool EnableToolTips
    {
      get
      {
        return (bool) TypeDescriptor.GetProperties((object) this.ControlBase)["EnableToolTips"].GetValue((object) this.ControlBase);
      }
      set
      {
        TypeDescriptor.GetProperties((object) this.ControlBase)["EnableToolTips"].SetValue((object) this.ControlBase, (object) value);
      }
    }

    public vDataGridViewActionList(vDataGridViewDesignerBase owner)
      : base(owner.Component)
    {
      this._owner = owner;
      this.uiService = this.GetService(typeof (DesignerActionUIService)) as DesignerActionUIService;
    }

    public override DesignerActionItemCollection GetSortedActionItems()
    {
      return new DesignerActionItemCollection() { (DesignerActionItem) new DesignerActionPropertyItem("EnableColumnChooser", "Enable Column Chooser"), (DesignerActionItem) new DesignerActionPropertyItem("EnableToolTips", "Enable ToolTips"), (DesignerActionItem) new DesignerActionPropertyItem("EnableResizeToolTip", "Enable Resize ToolTip"), (DesignerActionItem) new DesignerActionPropertyItem("ImageList", "ImageList"), (DesignerActionItem) new DesignerActionPropertyItem("GridLinesDisplayMode", "Lines Display Mode"), (DesignerActionItem) new DesignerActionPropertyItem("GridLinesDashStyle", "Lines Dash Style"), (DesignerActionItem) new DesignerActionPropertyItem("SelectionMode", "Selection Mode") };
    }

    private void RefreshPanelContent()
    {
      if (this.uiService == null)
        return;
      this.uiService.Refresh(this._owner.Component);
    }
  }
}
