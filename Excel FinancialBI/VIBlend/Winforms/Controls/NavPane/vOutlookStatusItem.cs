// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vOutlookStatusItem
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  [ToolboxItem(false)]
  public class vOutlookStatusItem : Control, IScrollableControlBase
  {
    private ImageList imageList = new ImageList();
    private bool allowAnimations = true;
    private bool useThemeBackground = true;
    private Color backgroundBorder = Color.Black;
    private Color disabledBackgroundBorder = Color.Silver;
    private Brush disabledBackgroundBrush = (Brush) new SolidBrush(Color.Silver);
    private Brush backgroundBrush = (Brush) new SolidBrush(Color.WhiteSmoke);
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private vOutlookNavPane pane;
    private vSplitButton splitButton;
    internal BackgroundElement backFill;
    private vStripsRenderer extender;
    private ContextMenuStrip contextMenu;
    private AnimationManager manager;
    private ControlTheme theme;

    /// <summary>Gets or sets the associated menu.</summary>
    [Browsable(false)]
    public ContextMenuStrip Menu
    {
      get
      {
        return this.contextMenu;
      }
    }

    /// <summary>Gets the split button instance.</summary>
    [Description("Gets the split button instance")]
    [Browsable(false)]
    public vSplitButton SplitButton
    {
      get
      {
        return this.splitButton;
      }
    }

    [Browsable(false)]
    public AnimationManager AnimationManager
    {
      get
      {
        if (this.manager == null)
          this.manager = new AnimationManager((Control) this);
        return this.manager;
      }
      set
      {
        this.manager = value;
      }
    }

    [Browsable(false)]
    public bool AllowAnimations
    {
      get
      {
        return this.allowAnimations;
      }
      set
      {
        this.allowAnimations = value;
        this.backFill.IsAnimated = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Description("Gets or sets button's theme")]
    [Category("Appearance")]
    public ControlTheme Theme
    {
      get
      {
        return this.theme;
      }
      set
      {
        if (value == null)
          return;
        this.theme = value.CreateCopy();
        Color color1 = this.theme.QueryColorSetter("NavTextColorNormal");
        if (color1 != Color.Empty)
          this.theme.StyleNormal.TextColor = color1;
        Color color2 = this.theme.QueryColorSetter("NavTextColorPressed");
        if (color2 != Color.Empty)
          this.theme.StylePressed.TextColor = color2;
        Color color3 = this.theme.QueryColorSetter("NavTextColorHighlight");
        if (color3 != Color.Empty)
          this.theme.StyleHighlight.TextColor = color3;
        Color color4 = this.theme.QueryColorSetter("NavBorderNormal");
        if (color4 != Color.Empty)
          this.theme.StyleNormal.BorderColor = color4;
        FillStyle fillStyle = this.theme.QueryFillStyleSetter("NavNormal");
        if (fillStyle != null)
          this.theme.StyleNormal.FillStyle = fillStyle;
        this.backFill.LoadTheme(this.theme);
        this.backFill.IsAnimated = this.AllowAnimations;
        this.Invalidate();
        this.RefreshStatus();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use theme's background
    /// </summary>
    [Description("Gets or sets a value indicating whether to use theme's background")]
    [Category("Behavior")]
    public bool UseThemeBackground
    {
      get
      {
        return this.useThemeBackground;
      }
      set
      {
        if (value == this.useThemeBackground)
          return;
        this.useThemeBackground = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the background border.</summary>
    /// <value>The background border.</value>
    [Description("Gets or sets the background border.")]
    [Category("Appearance")]
    public Color BackgroundBorder
    {
      get
      {
        return this.backgroundBorder;
      }
      set
      {
        this.backgroundBorder = value;
      }
    }

    /// <summary>Gets or sets the disabledBackgroundBorder border.</summary>
    /// <value>The disabledBackgroundBorder border.</value>
    [Category("Appearance")]
    [Description("Gets or sets the DisabledBackgroundBorder border.")]
    public Color DisabledBackgroundBorder
    {
      get
      {
        return this.disabledBackgroundBorder;
      }
      set
      {
        this.disabledBackgroundBorder = value;
      }
    }

    /// <summary>Gets or sets the disabled background brush.</summary>
    /// <value>The disabled background brush.</value>
    [Category("Appearance")]
    [Description("Gets or sets the disabled background brush.")]
    public Brush DisabledBackgroundBrush
    {
      get
      {
        return this.disabledBackgroundBrush;
      }
      set
      {
        this.disabledBackgroundBrush = value;
      }
    }

    /// <summary>Gets or sets the background brush.</summary>
    /// <value>The background brush.</value>
    [Category("Appearance")]
    [Description("Gets or sets the background brush.")]
    public Brush BackgroundBrush
    {
      get
      {
        return this.backgroundBrush;
      }
      set
      {
        this.backgroundBrush = value;
      }
    }

    [Category("Appearance")]
    public VIBLEND_THEME VIBlendTheme
    {
      get
      {
        return this.defaultTheme;
      }
      set
      {
        this.defaultTheme = value;
        ControlTheme defaultTheme;
        try
        {
          defaultTheme = ControlTheme.GetDefaultTheme(this.defaultTheme);
        }
        catch (Exception ex)
        {
          Trace.WriteLine(ex.Message);
          return;
        }
        if (this.extender == null)
          this.extender = new vStripsRenderer();
        this.extender.VIBlendTheme = this.VIBlendTheme;
        this.splitButton.VIBlendTheme = this.VIBlendTheme;
        if (defaultTheme == null)
          return;
        this.Theme = defaultTheme;
      }
    }

    /// <summary>Gets the nav pane.</summary>
    /// <value>The nav pane.</value>
    public vOutlookNavPane NavPane
    {
      get
      {
        return this.pane;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vOutlookStatusItem" /> class.
    /// </summary>
    /// <param name="pane">The pane.</param>
    public vOutlookStatusItem(vOutlookNavPane pane)
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.backFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.splitButton = new vSplitButton();
      this.splitButton.BorderStyle = ButtonBorderStyle.NONE;
      this.splitButton.DrawArrowOnly = true;
      this.splitButton.DropDownButtonWidth = 22;
      this.SplitButton.PaintDefaultStateFill = false;
      this.SplitButton.PaintDefaultFill = false;
      this.SplitButton.PaintDefaultStateBorder = false;
      this.SplitButton.PaintDefaultBorder = false;
      this.splitButton.Size = new Size(18, 22);
      this.splitButton.RoundedCornersRadius = 0;
      this.pane = pane;
      this.Size = new Size(0, 30);
      this.Controls.Add((Control) this.splitButton);
      this.contextMenu = new ContextMenuStrip();
      this.splitButton.DropDownMenu = this.contextMenu;
      this.extender = new vStripsRenderer();
      this.extender.RenderedContextMenuStrip = this.contextMenu;
      this.contextMenu.ImageList = this.NavPane.LargeImageList;
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
      this.InitializeMenu();
      this.NavPane.Items.CollectionChanged += new EventHandler(this.Items_CollectionChanged);
      this.imageList.Images.Add((Image) this.GetUpImage());
      this.imageList.Images.Add((Image) this.GetDownImage());
    }

    private void Items_CollectionChanged(object sender, EventArgs e)
    {
      this.InitializeMenu();
    }

    /// <summary>Initializes the menu.</summary>
    protected internal virtual void InitializeMenu()
    {
      if (this.NavPane == null || this.Width == 0)
        return;
      this.contextMenu.Items.Clear();
      ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem(this.NavPane.ShowMoreButtonsItemString);
      ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem(this.NavPane.ShowFewerButtonsItemString);
      ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem(this.NavPane.AddOrRemoveButtonsString);
      this.contextMenu.Items.Add((ToolStripItem) toolStripMenuItem1);
      this.contextMenu.Items.Add((ToolStripItem) toolStripMenuItem2);
      this.contextMenu.Items.Add((ToolStripItem) toolStripMenuItem3);
      toolStripMenuItem1.Click += new EventHandler(this.item1_Click);
      toolStripMenuItem2.Click += new EventHandler(this.item2_Click);
      foreach (vOutlookItem vOutlookItem in this.NavPane.Items)
      {
        if (vOutlookItem.MenuItemVisible)
        {
          ToolStripMenuItem toolStripMenuItem4 = new ToolStripMenuItem();
          toolStripMenuItem4.Checked = true;
          toolStripMenuItem4.Text = vOutlookItem.HeaderText;
          toolStripMenuItem4.Click += new EventHandler(this.newItem_Click);
          toolStripMenuItem4.Tag = (object) vOutlookItem;
          toolStripMenuItem3.DropDownItems.Add((ToolStripItem) toolStripMenuItem4);
          vOutlookItem.PropertyChanged -= new PropertyChangedEventHandler(this.item_PropertyChanged);
          vOutlookItem.PropertyChanged += new PropertyChangedEventHandler(this.item_PropertyChanged);
        }
      }
      this.contextMenu.ImageList = this.imageList;
      if (this.imageList.Images.Count > 0)
        toolStripMenuItem1.ImageIndex = 0;
      else
        toolStripMenuItem1.ImageIndex = -1;
      if (this.imageList.Images.Count > 1)
        toolStripMenuItem2.ImageIndex = 1;
      else
        toolStripMenuItem2.ImageIndex = -1;
      toolStripMenuItem3.ImageIndex = -1;
      foreach (ToolStripItem dropDownItem in (ArrangedElementCollection) toolStripMenuItem3.DropDownItems)
        dropDownItem.ImageIndex = -1;
    }

    /// <summary>Gets up image.</summary>
    /// <returns></returns>
    protected virtual Bitmap GetUpImage()
    {
      if (this.NavPane != null && this.NavPane.UpArrowMenuImage != null)
        return new Bitmap(this.NavPane.UpArrowMenuImage);
      Stream stream = (Stream) null;
      try
      {
        stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("VIBlend.WinForms.Controls.NavPane.arrow_top.png");
        if (stream != null)
        {
          Bitmap bitmap = new Bitmap(stream);
          if (bitmap != null)
            return bitmap;
        }
      }
      catch (Exception ex)
      {
      }
      finally
      {
        if (stream != null)
          stream.Close();
      }
      return (Bitmap) null;
    }

    /// <summary>Gets down image.</summary>
    /// <returns></returns>
    protected virtual Bitmap GetDownImage()
    {
      if (this.NavPane != null && this.NavPane.DownArrowMenuImage != null)
        return new Bitmap(this.NavPane.DownArrowMenuImage);
      Stream stream = (Stream) null;
      try
      {
        stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("VIBlend.WinForms.Controls.NavPane.arrow_down.png");
        if (stream != null)
        {
          Bitmap bitmap = new Bitmap(stream);
          if (bitmap != null)
            return bitmap;
        }
      }
      catch (Exception ex)
      {
      }
      finally
      {
        if (stream != null)
          stream.Close();
      }
      return (Bitmap) null;
    }

    private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.InitializeMenu();
    }

    private void newItem_Click(object sender, EventArgs e)
    {
      ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
      toolStripMenuItem.Checked = !toolStripMenuItem.Checked;
      vOutlookItem vOutlookItem = toolStripMenuItem.Tag as vOutlookItem;
      this.NavPane.SuspendLayout();
      if (!toolStripMenuItem.Checked)
      {
        this.NavPane.collapsedItem.Add(vOutlookItem);
        vOutlookItem.Visible = false;
      }
      else
      {
        this.NavPane.collapsedItem.Remove(vOutlookItem);
        vOutlookItem.Visible = true;
      }
      this.NavPane.ResumeLayout();
      this.RefreshStatus();
    }

    private void item2_Click(object sender, EventArgs e)
    {
      this.NavPane.HideFirstGroup();
    }

    private void item1_Click(object sender, EventArgs e)
    {
      this.NavPane.ShowFirstGroup();
    }

    /// <summary>Refreshes the status.</summary>
    public void RefreshStatus()
    {
      if (!this.Visible)
        return;
      this.Controls.Clear();
      this.Controls.Add((Control) this.splitButton);
      this.SplitButton.VIBlendTheme = this.VIBlendTheme;
      this.SplitButton.Theme = this.Theme;
      this.SplitButton.PaintDefaultStateFill = false;
      this.SplitButton.PaintDefaultFill = false;
      this.SplitButton.PaintDefaultStateBorder = false;
      this.SplitButton.PaintDefaultBorder = false;
      foreach (vOutlookItem vOutlookItem in this.pane.Items)
      {
        if (!this.NavPane.collapsedItem.Contains(vOutlookItem))
        {
          if (!vOutlookItem.Visible)
          {
            if (this.Width - (this.Controls.Count + 1) * 28 < 5)
              break;
            vToggleButton vToggleButton = new vToggleButton();
            vToggleButton.PaintDefaultBorder = false;
            vToggleButton.PaintDefaultFill = false;
            vToggleButton.StyleKey = "OutlookToggleButton";
            this.Controls.Add((Control) vToggleButton);
            vToggleButton.ShowFocusRectangle = false;
            vToggleButton.BorderStyle = ButtonBorderStyle.NONE;
            vToggleButton.VIBlendTheme = this.VIBlendTheme;
            vToggleButton.RoundedCornersRadius = 0;
            vToggleButton.Size = new Size(24, 24);
            Point point = new Point(this.Width - this.Controls.Count * 28, 3);
            if (!this.SplitButton.Visible)
              point = new Point(this.Width - (this.Controls.Count - 1) * 28, 3);
            vToggleButton.Location = point;
            vToggleButton.Image = vOutlookItem.SmallImage;
            vToggleButton.UseAbsoluteImagePositioning = true;
            vToggleButton.ImageAbsolutePosition = new Point(4, 4);
            if (vOutlookItem.IsExpanded)
              vToggleButton.Toggle = CheckState.Checked;
            vToggleButton.ToggleStateChanged += new EventHandler(this.button_ToggleStateChanged);
            vToggleButton.Tag = (object) vOutlookItem;
          }
          vOutlookItem.HeaderControl.Click -= new EventHandler(this.HeaderControl_Click);
          vOutlookItem.HeaderControl.Click += new EventHandler(this.HeaderControl_Click);
        }
      }
    }

    private void HeaderControl_Click(object sender, EventArgs e)
    {
      vOutlookHeader vOutlookHeader = sender as vOutlookHeader;
      for (int index = 0; index < this.Controls.Count; ++index)
      {
        vToggleButton vToggleButton = this.Controls[index] as vToggleButton;
        if (vToggleButton != null && vToggleButton.Tag != vOutlookHeader.NavPaneItem)
          vToggleButton.Toggle = CheckState.Unchecked;
      }
    }

    private void button_ToggleStateChanged(object sender, EventArgs e)
    {
      vToggleButton vToggleButton1 = sender as vToggleButton;
      if (vToggleButton1.Tag as vOutlookItem == this.NavPane.SelectedItem && vToggleButton1.Toggle != CheckState.Checked)
      {
        vToggleButton1.Toggle = CheckState.Checked;
      }
      else
      {
        if (vToggleButton1.Toggle != CheckState.Checked)
          return;
        this.NavPane.CallHeaderClick((vToggleButton1.Tag as vOutlookItem).HeaderControl);
        for (int index = 0; index < this.Controls.Count; ++index)
        {
          vToggleButton vToggleButton2 = this.Controls[index] as vToggleButton;
          if (vToggleButton2 != null && vToggleButton2 != vToggleButton1)
          {
            vToggleButton2.Toggle = CheckState.Unchecked;
            this.NavPane.Refresh();
            this.Refresh();
          }
        }
      }
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.
    /// </summary>
    /// <param name="levent">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      this.splitButton.Location = new Point(this.Width - this.splitButton.Width - 5, 3);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
      this.RenderStatusItem(e);
    }

    /// <summary>Renders the status item.</summary>
    /// <param name="e">The <see cref="T:System.Windows.Forms.PaintEventArgs" /> instance containing the event data.</param>
    protected virtual void RenderStatusItem(PaintEventArgs e)
    {
      this.backFill.Bounds = this.ClientRectangle;
      this.backFill.Radius = 0;
      ControlState controlState = ControlState.Normal;
      if (!this.Enabled)
        controlState = ControlState.Disabled;
      if (this.NavPane != null)
      {
        DrawOutlookNavPaneStatusEventArgs args = new DrawOutlookNavPaneStatusEventArgs(e.Graphics, controlState, this);
        this.NavPane.OnDrawItemStatusPart(args);
        if (args.Handled)
          return;
      }
      if (!this.UseThemeBackground)
      {
        this.backFill.Bounds = this.ClientRectangle;
        this.backFill.Radius = 0;
        Rectangle rectangle = new Rectangle(0, 0, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
        using (Pen pen = new Pen(this.BackgroundBorder))
        {
          switch (controlState)
          {
            case ControlState.Normal:
            case ControlState.Default:
              e.Graphics.FillRectangle(this.BackgroundBrush, this.backFill.Bounds);
              this.backFill.Bounds = rectangle;
              e.Graphics.DrawRectangle(pen, this.backFill.Bounds);
              break;
            case ControlState.Disabled:
              e.Graphics.FillRectangle(this.DisabledBackgroundBrush, this.backFill.Bounds);
              pen.Color = this.DisabledBackgroundBorder;
              this.backFill.Bounds = rectangle;
              e.Graphics.DrawRectangle(pen, this.backFill.Bounds);
              break;
            default:
              e.Graphics.FillRectangle(this.BackgroundBrush, this.backFill.Bounds);
              this.backFill.Bounds = rectangle;
              e.Graphics.DrawRectangle(pen, this.backFill.Bounds);
              break;
          }
        }
      }
      else
      {
        this.backFill.DrawElementFill(e.Graphics, controlState);
        this.backFill.Bounds = new Rectangle(0, 0, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
        this.backFill.DrawElementBorder(e.Graphics, controlState);
      }
      RectangleF layoutRectangle = new RectangleF((float) (this.backFill.Bounds.X + 5), (float) this.backFill.Bounds.Y, (float) (this.backFill.Bounds.Width - 10), (float) this.backFill.Bounds.Height);
      e.Graphics.DrawString(this.Text, this.Font, (Brush) new SolidBrush(this.backFill.ForeColor), layoutRectangle);
    }
  }
}
