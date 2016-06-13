// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vOptionsFieldSet
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>
  /// Represents an vOptionsFieldSet control that displays a drop-down list of single-choice items.
  /// </summary>
  [Designer("VIBlend.WinForms.Controls.Design.vOptionsFieldSetDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [Description("Displays a drop-down list of single-choice items.")]
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (vOptionsFieldSet), "ControlIcons.vFieldSet.ico")]
  public class vOptionsFieldSet : vCheckBox
  {
    private vPanel container = new vPanel();
    private List<vRadioButton> radiobuttons = new List<vRadioButton>();
    private Timer timer = new Timer();
    private int duration = 10;
    private Stack<int> animValues = new Stack<int>();
    private Timer boundsTimer = new Timer();
    private int hSpacing = 7;
    private int spacing = 2;
    private vDropDownBase dropDown;
    private int wantedHeight;
    private int step;

    /// <summary>
    /// Gets a reference to the DropDown window of the OptionsFieldSet control
    /// </summary>
    [Browsable(false)]
    public vDropDownBase DropDown
    {
      get
      {
        return this.dropDown;
      }
    }

    /// <summary>
    /// Gets or sets the horizontal spacing of the vOptionsFieldSet control.
    /// </summary>
    /// <value>The spacing.</value>
    [Category("Behavior")]
    public int HorizontalSpacing
    {
      get
      {
        return this.hSpacing;
      }
      set
      {
        this.hSpacing = value;
      }
    }

    /// <summary>
    /// Gets or sets the vertical spacing of the vOptionsFieldSet control.
    /// </summary>
    /// <value>The spacing.</value>
    [Category("Behavior")]
    [Description("Gets or sets the vertical spacing of the vOptionsFieldSet control.")]
    public int VerticalSpacing
    {
      get
      {
        return this.spacing;
      }
      set
      {
        this.spacing = value;
      }
    }

    /// <summary>
    /// Gets a reference to the radio buttons list of the vOptionsFieldSet control
    /// </summary>
    [Category("Behavior")]
    [Description("Gets the radio buttons.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public List<vRadioButton> RadioButtons
    {
      get
      {
        return this.radiobuttons;
      }
    }

    /// <summary>
    /// Gets or sets the container panel of the vOptionsFieldSet control
    /// </summary>
    /// <value>The container panel.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public vPanel Panel
    {
      get
      {
        return this.container;
      }
      set
      {
        this.container = value;
      }
    }

    static vOptionsFieldSet()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vOptionsFieldSet" /> class.
    /// </summary>
    public vOptionsFieldSet()
    {
      this.CheckedChanged += new EventHandler(this.vFieldSet_CheckedChanged);
      this.dropDown = new vDropDownBase();
      this.Checked = true;
      this.timer.Interval = 5;
      this.timer.Tick += new EventHandler(this.timer_Tick);
      this.dropDown.BackColor = Color.FromKnownColor(KnownColor.ControlLightLight);
      this.Panel.Opacity = 0.6f;
      this.Panel.AutoSize = false;
      this.Panel.Content.AutoScroll = false;
      this.Panel.Content.AutoSize = false;
      this.MouseMove += new MouseEventHandler(this.vFieldSet_MouseMove);
      this.Panel.MouseEnter += new EventHandler(this.Panel_MouseEnter);
      this.boundsTimer.Tick += new EventHandler(this.boundsTimer_Tick);
      this.PropertyChanged += new PropertyChangedEventHandler(this.vFieldSet_PropertyChanged);
    }

    private void vFieldSet_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "Theme"))
        return;
      foreach (vRadioButton radioButton in this.RadioButtons)
        radioButton.VIBlendTheme = this.VIBlendTheme;
      this.Panel.VIBlendTheme = this.VIBlendTheme;
    }

    private void Panel_MouseEnter(object sender, EventArgs e)
    {
      if (!this.Checked)
        return;
      this.Checked = false;
    }

    /// <summary>Clean up any resources being used.</summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.timer.Dispose();
        this.boundsTimer.Dispose();
      }
      base.Dispose(disposing);
    }

    private void boundsTimer_Tick(object sender, EventArgs e)
    {
      Point position = Cursor.Position;
      if (this.RectangleToScreen(this.ClientRectangle).Contains(position) || this.Panel.RectangleToScreen(this.Panel.ClientRectangle).Contains(position))
        return;
      this.boundsTimer.Stop();
      if (this.Checked)
        return;
      this.Checked = true;
    }

    private void vFieldSet_MouseMove(object sender, MouseEventArgs e)
    {
      if (this.ClientRectangle.Contains(e.Location))
      {
        if (!this.Checked)
          return;
        this.Checked = false;
      }
      else
        this.Checked = true;
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool ShowWindow(HandleRef hWnd, int nCmdShow);

    private void vFieldSet_CheckedChanged(object sender, EventArgs e)
    {
      this.HandleCheckedChanged();
    }

    private void HandleCheckedChanged()
    {
      this.CalculateBounds();
      this.step = 0;
      this.dropDown.Hide();
      this.timer.Stop();
      this.SetLocation();
      if (!this.Checked)
      {
        this.boundsTimer.Start();
        this.wantedHeight = this.dropDown.Size.Height;
        this.dropDown.Size = new Size(this.dropDown.Size.Width, 0);
        vOptionsFieldSet.ShowWindow(new HandleRef((object) this.dropDown, this.dropDown.Handle), 8);
      }
      else
        vOptionsFieldSet.ShowWindow(new HandleRef((object) this.dropDown, this.dropDown.Handle), 8);
      this.Animate();
    }

    private void SetLocation()
    {
      Rectangle screen = this.RectangleToScreen(this.ClientRectangle);
      this.dropDown.Location = new Point(screen.Left, screen.Top + this.Height + 1);
      if (this.dropDown.Bottom > Screen.PrimaryScreen.WorkingArea.Bottom)
        this.dropDown.Top = screen.Top - 1 - this.dropDown.Height;
      if (Screen.PrimaryScreen.Bounds.Right >= this.dropDown.Right)
        return;
      this.dropDown.Left = screen.Right - this.dropDown.Width;
    }

    private void Animate()
    {
      this.timer.Start();
    }

    private void timer_Tick(object sender, EventArgs e)
    {
      double num = (double) (this.step * this.step / this.duration);
      this.step += 2;
      if (!this.Checked)
      {
        if (num < (double) this.wantedHeight)
        {
          this.dropDown.Size = new Size(this.dropDown.Width, (int) num);
          this.animValues.Push((int) num);
        }
        else
        {
          this.dropDown.Size = new Size(this.dropDown.Width, this.wantedHeight);
          this.timer.Stop();
          this.step = 0;
        }
      }
      else if (this.animValues.Count > 0)
      {
        this.dropDown.Size = new Size(this.dropDown.Width, this.animValues.Pop());
      }
      else
      {
        this.dropDown.Size = new Size(this.dropDown.Width, 0);
        this.dropDown.Hide();
        this.timer.Stop();
        this.step = 0;
      }
    }

    private void CalculateBounds()
    {
      this.dropDown.Controls.Clear();
      this.Panel.Content.Controls.Clear();
      int verticalSpacing = this.VerticalSpacing;
      int val1 = 0;
      foreach (vRadioButton radioButton in this.RadioButtons)
      {
        this.Panel.Content.Controls.Add((Control) radioButton);
        radioButton.Location = new Point(this.hSpacing, verticalSpacing);
        verticalSpacing += radioButton.Height + this.VerticalSpacing;
        val1 = Math.Max(val1, radioButton.Size.Width);
        radioButton.BackColor = Color.Transparent;
      }
      this.Panel.Size = new Size(val1 + 2 * this.hSpacing, verticalSpacing + this.VerticalSpacing);
      this.dropDown.Size = this.Panel.Size;
      this.dropDown.Controls.Add((Control) this.Panel);
      this.Panel.Dock = DockStyle.Fill;
    }

    protected internal override void DrawCheck(Graphics graphics, int x, int y, ControlState controlState)
    {
      x -= 3;
      y -= 3;
      Point point = new Point(x, y);
      Color color = this.BorderColorFromState();
      if (!this.UseThemeCheckMarkColors)
        color = this.CheckMarkColor;
      using (Pen pen = new Pen(color))
      {
        pen.Width = 1f;
        graphics.DrawLine(pen, x, y + 3, x + 6, y + 3);
        if (!this.Checked)
          return;
        graphics.DrawLine(pen, x + 3, y, x + 3, y + 6);
      }
    }

    public override void DrawCheckBox(Graphics g, Rectangle rect, ControlState controlState)
    {
      base.DrawCheckBox(g, rect, controlState);
      Rectangle rectangle1 = Rectangle.Empty;
      Rectangle rectangle2 = Rectangle.Empty;
      rectangle2 = new Rectangle(rect.X + 4, rect.Y, rect.Width - 4, rect.Height);
      rectangle1 = this.RightToLeft != RightToLeft.No ? new Rectangle(rect.Right - 15, rect.Y + (rect.Height - 12) / 2, 12, 12) : new Rectangle(rect.X, rect.Y + (rect.Height - 12) / 2, 12, 12);
      if (this.RightToLeft == RightToLeft.No)
      {
        rectangle2.X += 10;
        rectangle2.Width -= 10;
      }
      else
        rectangle2.Width -= 16;
      if (this.Checked)
        return;
      this.DrawCheck(g, rectangle1.X + 6, rectangle1.Y + 6, controlState);
    }
  }
}
