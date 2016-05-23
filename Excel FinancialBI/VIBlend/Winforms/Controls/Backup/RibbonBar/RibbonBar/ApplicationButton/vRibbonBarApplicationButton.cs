// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vRibbonApplicationButton
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vRibbonApplicationButton control.</summary>
  [ToolboxBitmap(typeof (vRibbonApplicationButton), "ControlIcons.vRibbonApplicationButton.ico")]
  [ToolboxItem(true)]
  [Description("Displays a ribbon application button with an attached  menu.")]
  [Designer("VIBlend.WinForms.Controls.Design.vRibbonApplicationButtonDesigner, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  public class vRibbonApplicationButton : Control, IScrollableControlBase
  {
    private Padding contentPadding = new Padding(4, 18, 4, 26);
    private bool allowAnimations = true;
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private int dropDownWidth = 200;
    private int dropDownHeight = 200;
    private RibbonGroupPanel content;
    private vToggleCircularButton roundButton;
    private vToolStripDropDown dropDown;
    private BackgroundElement backFill;
    private AnimationManager manager;
    private ControlTheme theme;
    private bool fromSetControlSize;
    private Size contentSize;

    /// <exclude />
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

    /// <summary>Determines whether to use animations</summary>
    [DefaultValue(true)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool AllowAnimations
    {
      get
      {
        return this.allowAnimations;
      }
      set
      {
        this.allowAnimations = value;
      }
    }

    /// <summary>Gets or sets the theme of the control</summary>
    [Browsable(false)]
    [Description("Gets or sets the theme of the control")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
        this.RoundedButton.Theme = this.theme;
        this.backFill.LoadTheme(this.theme);
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the theme of the control using one of the built-in themes
    /// </summary>
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
        if (this.roundButton != null)
          this.roundButton.VIBlendTheme = value;
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
        if (defaultTheme == null)
          return;
        this.Theme = defaultTheme;
      }
    }

    /// <summary>Gets the rounded button.</summary>
    /// <value>The rounded button.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public vToggleCircularButton RoundedButton
    {
      get
      {
        return this.roundButton;
      }
    }

    /// <summary>Gets or sets the width of the drop down.</summary>
    /// <value>The width of the drop down.</value>
    [Description("Gets or sets the width of the drop down.")]
    [DefaultValue(200)]
    [Category("Behavior")]
    public int DropDownWidth
    {
      get
      {
        return this.dropDownWidth;
      }
      set
      {
        this.dropDownWidth = value;
        this.Width = value;
      }
    }

    /// <summary>Gets or sets the height of the drop down.</summary>
    /// <value>The height of the drop down.</value>
    [Description("Gets or sets the height of the drop down.")]
    [DefaultValue(200)]
    [Category("Behavior")]
    public int DropDownHeight
    {
      get
      {
        return this.dropDownHeight;
      }
      set
      {
        this.dropDownHeight = value;
        this.Height = value + this.roundButton.Height;
      }
    }

    /// <summary>Gets the content.</summary>
    /// <value>The content.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public Panel Content
    {
      get
      {
        return (Panel) this.content;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vRibbonApplicationButton" /> class.
    /// </summary>
    public vRibbonApplicationButton()
    {
      this.backFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.roundButton = new vToggleCircularButton();
      this.content = new RibbonGroupPanel();
      this.content.MinimumSize = new Size(50, 50);
      this.roundButton.Size = new Size(38, 38);
      this.roundButton.Toggle = CheckState.Unchecked;
      this.content.BackColor = Color.FromKnownColor(KnownColor.ControlLightLight);
      this.Controls.Add((Control) this.roundButton);
      this.Controls.Add((Control) this.content);
      this.roundButton.ToggleStateChanged += new EventHandler(this.roundButton_ToggleStateChanged);
      this.roundButton.MouseDown += new MouseEventHandler(this.roundButton_MouseDown);
      this.content.SizeChanged += new EventHandler(this.myPanel_SizeChanged);
      this.SizeChanged += new EventHandler(this.MyControl_SizeChanged);
      this.content.Paint += new PaintEventHandler(this.content_Paint);
      this.content.Click += new EventHandler(this.content_Click);
      this.Theme = ControlTheme.GetDefaultTheme(this.defaultTheme);
      this.SetStyle(ControlStyles.ResizeRedraw, false);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.SetStyle(ControlStyles.Opaque, false);
    }

    private void content_Click(object sender, EventArgs e)
    {
      if (!this.roundButton.ClientRectangle.Contains(this.PointToClient(Cursor.Position)))
        return;
      this.roundButton.OnToggle();
    }

    private void content_Paint(object sender, PaintEventArgs e)
    {
      if (this.roundButton.Toggle != CheckState.Checked)
        return;
      Rectangle rectangle = this.content.ClientRectangle;
      this.backFill.LoadTheme(this.Theme.CreateCopy());
      if (this.backFill.Theme.StyleNormal.FillStyle.Colors.Length == 4)
      {
        Color color1 = this.backFill.Theme.StyleNormal.FillStyle.Colors[0];
        Color color2 = this.backFill.Theme.StyleNormal.FillStyle.Colors[1];
        Color color3 = this.backFill.Theme.StyleNormal.FillStyle.Colors[2];
        Color color4 = this.backFill.Theme.StyleNormal.FillStyle.Colors[3];
        this.backFill.Theme.StyleNormal.FillStyle.Colors[1] = color3;
        this.backFill.Theme.StyleNormal.FillStyle.Colors[3] = color1;
      }
      this.backFill.Bounds = rectangle;
      this.backFill.DrawStandardFill(e.Graphics, ControlState.Normal, GradientStyles.Linear);
      rectangle = new Rectangle(0, 0, this.content.Width, this.contentPadding.Top);
      this.backFill.Bounds = rectangle;
      this.backFill.DrawStandardFill(e.Graphics, ControlState.Normal, GradientStyles.Linear);
      rectangle = new Rectangle(0, this.content.Height - this.contentPadding.Bottom, this.content.Width, this.contentPadding.Bottom);
      this.backFill.Bounds = rectangle;
      this.backFill.DrawStandardFill(e.Graphics, ControlState.Normal, GradientStyles.Linear);
      Rectangle rect = new Rectangle(this.contentPadding.Left, this.contentPadding.Top, this.content.Width - this.contentPadding.Horizontal, this.content.Height - this.contentPadding.Vertical);
      Color color = ControlPaint.LightLight(this.backFill.BorderColor);
      using (SolidBrush solidBrush = new SolidBrush(this.content.BackColor))
      {
        e.Graphics.FillRectangle((Brush) solidBrush, rect);
        this.backFill.Radius = 0;
        this.backFill.Bounds = rect;
        this.backFill.DrawElementBorder(e.Graphics, ControlState.Normal, color);
        this.backFill.Bounds = new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2);
        if (this.Content != null && this.Content.BackgroundImage != null && this.backFill.Bounds.Width > 0 && this.backFill.Bounds.Height > 0)
          e.Graphics.DrawImage(this.Content.BackgroundImage, this.backFill.Bounds);
        this.backFill.DrawElementBorder(e.Graphics, ControlState.Normal);
      }
      rectangle = new Rectangle(0, 0, this.content.Width - 1, this.content.Height - 1);
      this.backFill.Bounds = rectangle;
      this.backFill.DrawElementBorder(e.Graphics, ControlState.Normal);
      this.backFill.Bounds = new Rectangle(1, 1, this.content.Width - 3, this.content.Height - 3);
      this.backFill.DrawElementBorder(e.Graphics, ControlState.Normal, color);
      if (this.DesignMode)
        return;
      Rectangle bounds = new Rectangle(5, -this.roundButton.Height / 2, this.roundButton.Width, this.roundButton.Height);
      if (this.roundButton.RectangleToScreen(this.roundButton.ClientRectangle).X == 0)
        bounds = new Rectangle(0, -this.roundButton.Height / 2, this.roundButton.Width, this.roundButton.Height);
      if (this.PointToScreen(Point.Empty).Y > this.dropDown.Top)
        return;
      if (this.roundButton.controlState == ControlState.Hover)
        this.roundButton.DrawButton(e.Graphics, bounds, ControlState.Hover);
      else
        this.roundButton.DrawButton(e.Graphics, bounds, ControlState.Pressed);
    }

    private void roundButton_MouseDown(object sender, MouseEventArgs e)
    {
      if (this.roundButton.Toggle != CheckState.Checked)
        return;
      vToolStripDropDown toolStripDropDown = this.dropDown;
    }

    private void MyControl_SizeChanged(object sender, EventArgs e)
    {
      if (!this.fromSetControlSize)
        this.contentSize = new Size(this.DropDownWidth, this.DropDownHeight - this.roundButton.Height);
      this.fromSetControlSize = false;
      this.PerformLayout();
      if (this.DesignMode)
        return;
      this.Size = this.RoundedButton.Size;
    }

    private void myPanel_SizeChanged(object sender, EventArgs e)
    {
    }

    private void roundButton_ToggleStateChanged(object sender, EventArgs e)
    {
      if (this.DesignMode)
      {
        this.SetControlSize();
      }
      else
      {
        vToolStripDropDown toolStripDropDown = this.dropDown;
        if (this.roundButton.Toggle == CheckState.Checked)
        {
          this.content.Parent = (Control) this.dropDown.Panel;
          this.dropDown.AutoClose = true;
          this.dropDown.Opacity = 1.0;
          this.content.MinimumSize = new Size(this.DropDownWidth, this.DropDownHeight);
          this.content.MaximumSize = new Size(this.DropDownWidth, this.DropDownHeight);
          this.dropDown.Panel.MinimumSize = this.content.Size;
          this.dropDown.Show((Control) this.roundButton, new Point(this.roundButton.Left - 5, this.roundButton.Bottom - this.roundButton.Height / 2));
          this.dropDown.MinimumSize = new Size(this.DropDownWidth, this.DropDownHeight);
          this.dropDown.BackColor = Color.White;
        }
        else
          this.dropDown.StartForwardsPlusMinusAnimation();
      }
    }

    /// <summary>Opens the Popup.</summary>
    public void Open()
    {
      if (this.roundButton.Toggle != CheckState.Checked)
      {
        this.roundButton.Toggle = CheckState.Checked;
      }
      else
      {
        this.content.Parent = (Control) this.dropDown.Panel;
        this.dropDown.AutoClose = true;
        this.dropDown.Opacity = 1.0;
        this.content.MinimumSize = new Size(this.DropDownWidth, this.DropDownHeight);
        this.dropDown.Panel.MinimumSize = this.content.Size;
        this.dropDown.Show((Control) this.roundButton, new Point(this.roundButton.Left - 5, this.roundButton.Bottom - this.roundButton.Height / 2));
        this.dropDown.MinimumSize = new Size(this.DropDownWidth, this.DropDownHeight);
        this.dropDown.BackColor = Color.White;
      }
    }

    /// <summary>Closes the Popup.</summary>
    public void Close()
    {
      if (this.roundButton.Toggle != CheckState.Unchecked)
      {
        this.roundButton.Toggle = CheckState.Unchecked;
      }
      else
      {
        this.dropDown.Opacity = 0.0;
        this.dropDown.Hide();
      }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
    }

    private void dropDown_Closing(object sender, ToolStripDropDownClosingEventArgs e)
    {
      if (this.dropDown.Opacity <= 0.0 || this.roundButton.IsDisposed)
        return;
      Point client = this.roundButton.PointToClient(Cursor.Position);
      e.Cancel = true;
      if (this.roundButton.ClientRectangle.Contains(client))
      {
        if (!this.dropDown.Visible)
          return;
        this.dropDown.StartForwardsPlusMinusAnimation();
      }
      else
        this.roundButton.Toggle = CheckState.Unchecked;
    }

    private void dropDown_Closed(object sender, ToolStripDropDownClosedEventArgs e)
    {
      int num = (int) this.roundButton.Toggle;
    }

    private void SetControlSize()
    {
      this.fromSetControlSize = true;
      if (this.roundButton.Toggle == CheckState.Checked)
        this.Size = new Size(this.DropDownWidth, this.DropDownHeight + this.roundButton.Height);
      else
        this.Size = this.RoundedButton.Size;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.
    /// </summary>
    /// <param name="levent">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      if (this.dropDown == null && !this.DesignMode)
      {
        this.dropDown = new vToolStripDropDown((Control) this.content);
        this.dropDown.Visible = false;
        this.dropDown.Closed += new ToolStripDropDownClosedEventHandler(this.dropDown_Closed);
        this.dropDown.Closing += new ToolStripDropDownClosingEventHandler(this.dropDown_Closing);
        this.dropDown.DropShadowEnabled = true;
        this.dropDown.AutoClose = true;
        this.dropDown.CanResize = false;
      }
      this.roundButton.Bounds = new Rectangle(0, 0, this.roundButton.Width, this.roundButton.Height);
      if (this.DesignMode)
      {
        if (this.roundButton.Toggle == CheckState.Checked)
          this.content.Bounds = new Rectangle(0, this.roundButton.Height, this.DropDownWidth, this.DropDownHeight);
        else
          this.content.Bounds = Rectangle.Empty;
      }
      else
        this.Size = this.RoundedButton.Size;
    }
  }
}
