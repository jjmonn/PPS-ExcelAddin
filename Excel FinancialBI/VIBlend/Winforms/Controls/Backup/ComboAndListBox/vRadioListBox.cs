// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vRadioListBox
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
  [ToolboxItem(true)]
  [Designer("VIBlend.WinForms.Controls.Design.vListControlDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxBitmap(typeof (vListBox), "ControlIcons.vListBox.ico")]
  [Description("Displays a collection of items. The control allows you to use images, and detailed description for each item.")]
  public class vRadioListBox : vListBox
  {
    private vRadioButton radioButton = new vRadioButton();
    private bool checkOnClick;

    /// <summary>Gets or sets the selection mode.</summary>
    /// <value>The selection mode.</value>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
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

    /// <summary>
    /// Gets or sets a value indicating whether the item is checked when it is clicked.
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether the item is checked when it is clicked.")]
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

    /// <summary>Occurs when item is checked.</summary>
    [Description("Occurs when item is checked.")]
    [Category("Action")]
    public event ItemCheckChangedEventHandler ItemChecked;

    /// <summary>Occurs when item's check state is changing.</summary>
    [Description("Occurs when item's check state is changing.")]
    [Category("Action")]
    public event ItemCheckChangingEventHandler ItemChecking;

    static vRadioListBox()
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
      this.CheckItem(this.SelectedItem);
    }

    /// <summary>Checks the item.</summary>
    /// <param name="item">The item.</param>
    public virtual void CheckItem(ListItem item)
    {
      foreach (ListItem listItem in this.Items)
        listItem.IsChecked = new bool?(false);
      item.IsChecked = new bool?(true);
      this.Invalidate();
    }

    internal void DrawRadioButton(Graphics g, Rectangle rect, bool isEnabled, bool isMouseOver, bool isFocused, bool drawLine, bool Checked)
    {
      this.radioButton.UseThemeCheckMarkColors = true;
      this.radioButton.Bounds = new Rectangle(0, 0, 13, 13);
      if (this.radioButton.VIBlendTheme != this.VIBlendTheme)
        this.radioButton.VIBlendTheme = this.VIBlendTheme;
      this.radioButton.Text = "";
      this.radioButton.Checked = Checked;
      this.radioButton.Bounds = rect;
      this.radioButton.DrawRadioButton(g, rect, isMouseOver, this.radioButton.Checked);
    }

    protected override void OnMouseDown(MouseEventArgs mevent)
    {
      ListItem selectedItem = this.SelectedItem;
      base.OnMouseDown(mevent);
      ItemCheckChangingEventArgs args = new ItemCheckChangingEventArgs(this.SelectedItem);
      this.OnItemChecking(args);
      if (args.Cancel)
        return;
      if (this.CheckOnClick)
      {
        if (this.SelectedItem != null)
          this.CheckItem(this.SelectedItem);
      }
      else if (selectedItem == this.SelectedItem && this.SelectedItem != null)
        this.CheckItem(this.SelectedItem);
      this.OnItemChecked(new ItemCheckChangedEventArgs(this.SelectedItem));
      this.Invalidate();
    }

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
        bool Checked = true;
        if (listItem.IsChecked.HasValue && !listItem.IsChecked.Value)
          Checked = false;
        if (listItem != null && listItem.Font != null)
          font = listItem.Font;
        if (this.hotTrackIndex == index)
        {
          this.DrawRadioButton(e.Graphics, new Rectangle(x - 16, rect2.Y + this.ItemHeight / 2 - 6, 13, 13), this.Enabled, true, true, false, Checked);
          if (!this.OnDrawListItem(new DrawItemEventArgs(e.Graphics, font, rect2, index, DrawItemState.HotLight)))
            this.OnDrawItem(new DrawItemEventArgs(e.Graphics, font, rect2, index, DrawItemState.HotLight));
        }
        else if (this.SelectedIndex != index)
        {
          this.DrawRadioButton(e.Graphics, new Rectangle(x - 16, rect2.Y + this.ItemHeight / 2 - 6, 13, 13), this.Enabled, false, false, false, Checked);
          if (!this.OnDrawListItem(new DrawItemEventArgs(e.Graphics, font, rect2, index, DrawItemState.None)))
            this.OnDrawItem(new DrawItemEventArgs(e.Graphics, font, rect2, index, DrawItemState.None));
        }
        else
        {
          this.DrawRadioButton(e.Graphics, new Rectangle(x - 16, rect2.Y + this.ItemHeight / 2 - 6, 13, 13), this.Enabled, false, true, false, Checked);
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
