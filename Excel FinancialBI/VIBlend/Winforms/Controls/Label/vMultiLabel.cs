// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vMultiLabel
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vMultiLabel control.</summary>
  /// <remarks>
  /// A vMultiLabel control provides visual grouping of multiple label controls.
  /// </remarks>
  [ToolboxBitmap(typeof (vMultiLabel), "ControlIcons.MultiLabel.ico")]
  [Description("Provides visual grouping of multiple label controls.")]
  [ToolboxItem(true)]
  public class vMultiLabel : ScrollableControlMiniBase
  {
    private vLabelCollection collection = new vLabelCollection();

    /// <summary>
    /// Gets a reference to the Labels collection of the vMultiLabel control
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public vLabelCollection Labels
    {
      get
      {
        return this.collection;
      }
    }

    static vMultiLabel()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vLabel" /> class.
    /// </summary>
    public vMultiLabel()
    {
      this.collection.CollectionChanged += new EventHandler(this.collection_CollectionChanged);
    }

    /// <summary>
    /// Handles the PropertyChanged event of the item control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.ComponentModel.PropertyChangedEventArgs" /> instance containing the event data.</param>
    private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "Height"))
        return;
      this.LayoutLabels();
    }

    /// <summary>
    /// Handles the CollectionChanged event of the collection control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    private void collection_CollectionChanged(object sender, EventArgs e)
    {
      this.RefreshLabels();
    }

    /// <summary>Refreshes the visuals.</summary>
    private void RefreshLabels()
    {
      this.Controls.Clear();
      foreach (Control label in this.Labels)
        this.Controls.Add(label);
      this.LayoutLabels();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.
    /// </summary>
    /// <param name="levent">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      this.LayoutLabels();
    }

    /// <summary>Layouts the labels.</summary>
    private void LayoutLabels()
    {
      int val1 = 1;
      foreach (vLabelItem label in this.Labels)
        val1 = Math.Max(val1, label.Row + 1);
      for (int row1 = 0; row1 < val1; ++row1)
      {
        List<vLabelItem> labelsByRow1 = this.GetLabelsByRow(row1);
        int y = 0;
        int num = 0;
        for (int row2 = 0; row2 < row1; ++row2)
        {
          List<vLabelItem> labelsByRow2 = this.GetLabelsByRow(row2);
          int val2 = 0;
          foreach (Control control in labelsByRow2)
            val2 = Math.Max(control.Height, val2);
          y += val2;
        }
        foreach (vLabelItem vLabelItem in labelsByRow1)
        {
          vLabelItem.Location = new Point(num + vLabelItem.HorizontalOffset, y);
          num += vLabelItem.Width + vLabelItem.HorizontalOffset;
        }
      }
    }

    /// <summary>
    /// Gets a list of the Labels on a specific row within the vMultiLabel control
    /// </summary>
    private List<vLabelItem> GetLabelsByRow(int row)
    {
      List<vLabelItem> vLabelItemList = new List<vLabelItem>();
      foreach (vLabelItem label in this.Labels)
      {
        if (label.Row == row)
          vLabelItemList.Add(label);
      }
      return vLabelItemList;
    }
  }
}
