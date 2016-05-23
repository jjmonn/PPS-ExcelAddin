// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vCheckedListBox
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vCheckedListBox control</summary>
  /// <remarks>
  /// A vCheckedListBox displays a collection of items. The control allows you to use images, check boxes and detailed description for each item.
  /// </remarks>
  [ToolboxBitmap(typeof (vListBox), "ControlIcons.CheckedListBox.ico")]
  [Description("Displays a collection of items. The control allows you to use images, and detailed description for each item.")]
  [Designer("VIBlend.WinForms.Controls.Design.vListControlDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxItem(true)]
  public class vCheckedListBox : vListBox
  {
    private vCheckBox checkBox = new vCheckBox();
    private bool checkOnClick;

    /// <summary>
    /// Gets or sets a value indicating whether the item is checked when it is clicked.
    /// </summary>
    [Description("Gets or sets a value indicating whether the item is checked when it is clicked.")]
    [Category("Behavior")]
    public bool CheckOnClick
    {
      get
      {
        return this.checkOnClick;
      }
      set
      {
        if (value == this.checkOnClick)
          return;
        this.checkOnClick = value;
      }
    }

    /// <summary>Gets or sets the selection mode.</summary>
    /// <value>The selection mode.</value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override SelectionMode SelectionMode
    {
      get
      {
        return base.SelectionMode;
      }
      set
      {
        base.SelectionMode = value;
      }
    }

    /// <summary>Occurs when item is checked.</summary>
    [Description("Occurs when item is checked.")]
    [Category("Action")]
    public event ItemCheckChangedEventHandler ItemChecked;

    /// <summary>Occurs when item's check state is changing.</summary>
    [Category("Action")]
    [Description("Occurs when item's check state is changing.")]
    public event ItemCheckChangingEventHandler ItemChecking;

    static vCheckedListBox()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>
    /// Raises the <see cref="E:ItemChecked" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:VIBlend.WinForms.Controls.ItemCheckChangedEventArgs" /> instance containing the event data.</param>
    protected virtual void OnItemChecked(ItemCheckChangedEventArgs args)
    {
      if (this.ItemChecked == null)
        return;
      this.ItemChecked((object) this, args);
    }

    /// <summary>
    /// Raises the <see cref="E:ItemChecking" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:VIBlend.WinForms.Controls.ItemCheckChangingEventArgs" /> instance containing the event data.</param>
    protected virtual void OnItemChecking(ItemCheckChangingEventArgs args)
    {
      if (this.ItemChecking == null)
        return;
      this.ItemChecking((object) this, args);
    }

    internal override void InitializeScrollBars()
    {
      base.InitializeScrollBars();
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
      if (e.KeyCode != Keys.Space || this.SelectedItem == null)
        return;
      ListItem selectedItem = this.SelectedItem;
      bool? isChecked = this.SelectedItem.IsChecked;
      bool? nullable = isChecked.HasValue ? new bool?(!isChecked.GetValueOrDefault()) : new bool?();
      selectedItem.IsChecked = nullable;
      this.Invalidate();
    }

    /// <summary>Draws the check box.</summary>
    /// <param name="g">The g.</param>
    /// <param name="rect">The rect.</param>
    /// <param name="isEnabled">if set to <c>true</c> [is enabled].</param>
    /// <param name="isMouseOver">if set to <c>true</c> [is mouse over].</param>
    /// <param name="isFocused">if set to <c>true</c> [is focused].</param>
    /// <param name="drawLine">if set to <c>true</c> [draw line].</param>
    /// <param name="checkState">State of the check.</param>
    protected internal virtual void DrawCheckBox(Graphics g, Rectangle rect, bool isEnabled, bool isMouseOver, bool isFocused, bool drawLine, CheckState checkState)
    {
      this.checkBox.UseThemeCheckMarkColors = true;
      this.checkBox.Bounds = new Rectangle(0, 0, 13, 13);
      if (this.checkBox.VIBlendTheme != this.VIBlendTheme)
        this.checkBox.VIBlendTheme = this.VIBlendTheme;
      this.checkBox.Text = "";
      this.checkBox.CheckState = checkState;
      this.checkBox.Bounds = rect;
      this.checkBox.DrawCheckBox(g, rect, isMouseOver ? ControlState.Hover : ControlState.Normal);
    }

    /// <summary>Raises the OnMouseDown event.</summary>
    /// <param name="mevent">A MouseEventArgs that contains the event data.</param>
    protected override void OnMouseDown(MouseEventArgs mevent)
    {
      ListItem selectedItem = this.SelectedItem;
      base.OnMouseDown(mevent);
      if (this.SelectedItem == null)
        return;
      if (this.CheckOnClick || selectedItem == this.SelectedItem)
      {
        Rectangle renderBounds = this.SelectedItem.RenderBounds;
        renderBounds.X -= 18;
        renderBounds.Width += 18;
        if (renderBounds.Contains(mevent.Location))
        {
          ItemCheckChangingEventArgs args = new ItemCheckChangingEventArgs(this.SelectedItem);
          this.OnItemChecking(args);
          if (args.Cancel)
            return;
          if (this.SelectedItem.IsChecked.HasValue && !this.SelectedItem.IsChecked.Value)
            this.SelectedItem.IsChecked = new bool?(true);
          else if (this.SelectedItem.IsChecked.HasValue && this.SelectedItem.IsChecked.Value)
          {
            if (!this.SelectedItem.IsThreeState)
              this.SelectedItem.IsChecked = new bool?(false);
            else
              this.SelectedItem.IsChecked = new bool?();
          }
          else
            this.SelectedItem.IsChecked = new bool?(false);
          this.OnItemChecked(new ItemCheckChangedEventArgs(this.SelectedItem));
        }
      }
      this.Invalidate();
    }

    /// <summary>check item.</summary>
    /// <param name="item"></param>
    public void CheckItem(ListItem item)
    {
      if (!this.Items.Contains(item))
        return;
      item.IsChecked = new bool?(true);
      this.Invalidate();
    }

    /// <summary>Checks all items.</summary>
    public void CheckAllItems()
    {
      foreach (ListItem listItem in this.Items)
        listItem.IsChecked = new bool?(true);
    }

    /// <summary>Uns the check all items.</summary>
    public void UnCheckAllItems()
    {
      foreach (ListItem listItem in this.Items)
        listItem.IsChecked = new bool?(false);
    }

    /// <summary>uncheck item.</summary>
    /// <param name="item"></param>
    public void UnCheckItem(ListItem item)
    {
      if (!this.Items.Contains(item))
        return;
      item.IsChecked = new bool?(false);
      this.Invalidate();
    }

    /// <summary>Renders the list box.</summary>
    /// <param name="e">The <see cref="T:System.Windows.Forms.PaintEventArgs" /> instance containing the event data.</param>
    protected override void RenderListBox(PaintEventArgs e)
    {
      if (this.isScrollBarUpdateRequired)
      {
        if (this.SelectedIndex != -1)
          this.EnsureVisible(this.SelectedIndex);
        else
          this.InitializeScrollBars();
      }
      if (this.backFill == null)
        return;
      Color backFillColor = this.GetBackFillColor();
      this.vscroll.BackColor = Color.Transparent;
      this.hscroll.BackColor = Color.Transparent;
      this.backFill.Bounds = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
      VisualStyle visualStyle = this.Enabled ? this.theme.StyleNormal : this.theme.StyleDisabled;
      FillStyle fillStyle = visualStyle.FillStyle;
      visualStyle.FillStyle = (FillStyle) new FillStyleSolid(backFillColor);
      this.backFill.DrawStandardFill(e.Graphics, this.Enabled ? ControlState.Normal : ControlState.Disabled, GradientStyles.Solid);
      visualStyle.FillStyle = fillStyle;
      int radius = this.backFill.Radius;
      int num1 = this.ItemHeight + this.ItemsSpacing;
      int num2 = this.displayBounds.Height - 4;
      if (this.hscroll.Visible)
        num2 -= this.hscroll.Height;
      int num3 = this.displayBounds.Width - 4 - 18;
      if (this.vscroll.Visible)
        num3 -= this.vscroll.Width;
      int num4 = this.vscroll.Value / num1;
      if (num4 > 0)
        --num4;
      int num5 = num2 / num1 + num4 + 2;
      if (num5 >= this.Items.Count)
        num5 = this.Items.Count - 1;
      int y = this.displayBounds.Y + 2 - this.vscroll.Value + num1 * num4;
      int x = this.displayBounds.X + 2 - (this.hscroll.Visible ? this.hscroll.Value : 0) + 18;
      if (this.RightToLeft == RightToLeft.Yes && this.vscroll.Visible)
        x += this.vscroll.Width - 1;
      Region clip = e.Graphics.Clip;
      Rectangle rect1 = this.displayBounds;
      ++rect1.Height;
      ++rect1.Width;
      if (this.vscroll.Visible)
      {
        rect1.Width -= this.vscroll.Width;
        if (this.RightToLeft == RightToLeft.Yes)
          rect1.X += this.vscroll.Width;
      }
      if (this.hscroll.Visible)
        rect1.Height -= this.hscroll.Height;
      e.Graphics.Clip = new Region(rect1);
      for (int index = num4; index <= num5; ++index)
      {
        int width = num3 + (this.hscroll.Visible ? this.hscroll.Maximum : 0);
        if (this.vscroll.Visible)
          ++width;
        Rectangle rect2 = new Rectangle(x, y, width, this.ItemHeight);
        y += num1;
        Font font = this.Font;
        ListItem listItem = this.Items[index];
        CheckState checkState = CheckState.Checked;
        if (listItem.IsChecked.HasValue && !listItem.IsChecked.Value)
          checkState = CheckState.Unchecked;
        else if (listItem.IsThreeState && !listItem.IsChecked.HasValue)
          checkState = CheckState.Indeterminate;
        if (listItem != null && listItem.Font != null)
          font = listItem.Font;
        if (this.hotTrackIndex == index)
        {
          this.DrawCheckBox(e.Graphics, new Rectangle(x - 16, rect2.Y + this.ItemHeight / 2 - 6, 13, 13), this.Enabled, true, true, false, checkState);
          if (!this.OnDrawListItem(new DrawItemEventArgs(e.Graphics, font, rect2, index, DrawItemState.HotLight)))
            this.OnDrawItem(new DrawItemEventArgs(e.Graphics, font, rect2, index, DrawItemState.HotLight));
        }
        else if (this.SelectedIndex != index)
        {
          this.DrawCheckBox(e.Graphics, new Rectangle(x - 16, rect2.Y + this.ItemHeight / 2 - 6, 13, 13), this.Enabled, false, false, false, checkState);
          if (!this.OnDrawListItem(new DrawItemEventArgs(e.Graphics, font, rect2, index, DrawItemState.None)))
            this.OnDrawItem(new DrawItemEventArgs(e.Graphics, font, rect2, index, DrawItemState.None));
        }
        else
        {
          this.DrawCheckBox(e.Graphics, new Rectangle(x - 16, rect2.Y + this.ItemHeight / 2 - 6, 13, 13), this.Enabled, false, true, false, checkState);
          if (!this.OnDrawListItem(new DrawItemEventArgs(e.Graphics, font, rect2, index, DrawItemState.Selected)))
            this.OnDrawItem(new DrawItemEventArgs(e.Graphics, this.Font, rect2, index, DrawItemState.Selected));
        }
      }
      e.Graphics.Clip = clip;
      this.backFill.Bounds = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
      this.backFill.Radius = radius;
      if (this.UseThemeBorderColor)
        this.backFill.DrawElementBorder(e.Graphics, this.Enabled ? ControlState.Normal : ControlState.Disabled);
      else
        this.backFill.DrawElementBorder(e.Graphics, this.Enabled ? ControlState.Normal : ControlState.Disabled, this.BorderColor);
    }
  }
}
