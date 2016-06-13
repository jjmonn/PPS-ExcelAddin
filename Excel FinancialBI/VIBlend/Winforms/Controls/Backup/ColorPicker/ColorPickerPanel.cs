// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.ColorPickerPanel
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
  /// <summary>Represents a ColorPicker panel control</summary>
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (ColorPickerPanel), "ControlIcons.ColorPickerPanel.ico")]
  [Description("Represents a ColorPicker panel control.")]
  public class ColorPickerPanel : Panel
  {
    private vListBox listBoxSystemColors = new vListBox();
    private vListBox listBoxWebColors = new vListBox();
    private vTabControl tabControl = new vTabControl();
    private vTabPage customColorsTabPage = new vTabPage("Custom");
    private vTabPage systemColorsTabPage = new vTabPage("System");
    private vTabPage webTabPage = new vTabPage("Web");
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private ColorBox[] panels;
    private Color selectedColor;
    private ControlTheme theme;

    public Color SelectedColor
    {
      get
      {
        return this.selectedColor;
      }
      set
      {
        this.selectedColor = value;
        this.FocusToSelectedColor();
      }
    }

    /// <summary>Gets or sets the theme of the control.</summary>
    [Browsable(false)]
    [Category("Appearance")]
    [Description("Gets or sets the theme of the control.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
        this.theme = value;
        this.tabControl.Theme = this.theme;
        this.listBoxWebColors.Theme = this.theme;
        this.listBoxSystemColors.Theme = this.theme;
        this.Refresh();
      }
    }

    /// <summary>
    /// Gets or sets the theme of the control using one of the built-in themes.
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
        if (this.tabControl != null)
          this.tabControl.VIBlendTheme = value;
        if (this.listBoxSystemColors != null)
          this.listBoxSystemColors.VIBlendTheme = value;
        if (this.listBoxWebColors != null)
          this.listBoxWebColors.VIBlendTheme = value;
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

    /// <summary>Occurs when a color is changed.</summary>
    [Category("Action")]
    [Description("Occurs when a color is changed.")]
    public event EventHandler SelectedColorChanged;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.ColorPickerPanel" /> class.
    /// </summary>
    public ColorPickerPanel()
    {
      this.Theme = ControlTheme.GetDefaultTheme(VIBLEND_THEME.VISTABLUE);
      this.InitializeColorBoxDropDown();
      this.Size = new Size(202, 250);
      this.AutoSize = false;
      this.tabControl.TitleHeight = 30;
      this.listBoxSystemColors.SelectedIndexChanged += new EventHandler(this.listBoxSystemColors_SelectedIndexChanged);
      this.listBoxWebColors.SelectedIndexChanged += new EventHandler(this.listBoxWebColors_SelectedIndexChanged);
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      if (this.Height == 250 && this.Width == 202)
        return;
      this.Size = new Size(202, 250);
      this.Refresh();
    }

    private void InitializeColorBoxDropDown()
    {
      try
      {
        this.SuspendLayout();
        this.VisibleChanged += new EventHandler(this.vPopUpForm_VisibleChanged);
        this.tabControl.SuspendLayout();
        this.tabControl.Dock = DockStyle.Fill;
        this.tabControl.EnableToolTips = false;
        this.tabControl.UseTabsAreaBackColor = false;
        this.tabControl.BackColor = Color.Transparent;
        this.tabControl.Paint += new PaintEventHandler(this.vTabControl_Paint);
        this.Controls.Add((Control) this.tabControl);
        this.customColorsTabPage.SuspendLayout();
        this.customColorsTabPage.Text = "Custom";
        this.customColorsTabPage.DockPadding.All = 4;
        this.tabControl.TabPages.Add(this.customColorsTabPage);
        this.webTabPage.SuspendLayout();
        this.webTabPage.Text = "Web";
        this.webTabPage.DockPadding.All = 0;
        this.tabControl.TabPages.Add(this.webTabPage);
        this.systemColorsTabPage.SuspendLayout();
        this.systemColorsTabPage.DockPadding.All = 0;
        this.systemColorsTabPage.Text = "System";
        this.tabControl.TabPages.Add(this.systemColorsTabPage);
        this.listBoxWebColors.Dock = DockStyle.Fill;
        this.listBoxWebColors.TabStop = false;
        this.listBoxWebColors.DrawListItem += new DrawItemEventHandler(this.webListBox_DrawItem);
        this.listBoxWebColors.UseThemeBackColor = false;
        this.listBoxWebColors.BackColor = Color.White;
        this.webTabPage.Controls.Add((Control) this.listBoxWebColors);
        this.listBoxSystemColors.Dock = DockStyle.Fill;
        this.listBoxSystemColors.TabStop = false;
        this.listBoxSystemColors.DrawListItem += new DrawItemEventHandler(this.systemListBox_DrawItem);
        this.listBoxSystemColors.UseThemeBackColor = false;
        this.listBoxSystemColors.BackColor = Color.White;
        this.systemColorsTabPage.Controls.Add((Control) this.listBoxSystemColors);
        this.tabControl.ResumeLayout(false);
        this.customColorsTabPage.ResumeLayout(false);
        this.webTabPage.ResumeLayout(false);
        this.systemColorsTabPage.ResumeLayout(false);
        this.ResumeLayout(false);
        this.InitializeColors();
      }
      catch
      {
      }
    }

    private void vPopUpForm_VisibleChanged(object sender, EventArgs e)
    {
    }

    private void DrawItem(bool web, object sender, DrawItemEventArgs e)
    {
      if (e.Index == -1)
        return;
      Color color;
      ColorPickerPanel.GetItemColor(web, e, out color);
      Rectangle bounds1 = e.Bounds;
      if ((e.State & DrawItemState.Selected) > DrawItemState.None)
        --bounds1.Width;
      Rectangle rect = new Rectangle(bounds1.X + 2, bounds1.Y + 2, 22, bounds1.Height - 5);
      Rectangle layoutRectangle = new Rectangle(rect.Right + 2, bounds1.Y, bounds1.Width - rect.X, bounds1.Height);
      Rectangle bounds2 = new Rectangle(bounds1.X, bounds1.Y, bounds1.Width, bounds1.Height);
      vListBox listBox = web ? this.listBoxWebColors : this.listBoxSystemColors;
      listBox.DrawListItemBackGroundAndBorder(e.Graphics, bounds2, listBox.Items[e.Index], e.State);
      if (e.Index < 0 || e.Index >= listBox.Items.Count)
        return;
      this.DrawItemColorAndText(color, listBox, listBox.Items[e.Index], e.Graphics, rect, layoutRectangle, e.State);
    }

    private void DrawItemColorAndText(Color color, vListBox listBox, ListItem item, Graphics graphics, Rectangle rect, Rectangle layoutRectangle, DrawItemState drawState)
    {
      using (Brush brush = (Brush) new SolidBrush(color))
        graphics.FillRectangle(brush, rect);
      graphics.DrawRectangle(Pens.Black, rect);
      listBox.DrawListItemTextAndDescription(graphics, layoutRectangle, item, drawState);
    }

    private static void GetItemColor(bool web, DrawItemEventArgs e, out Color color)
    {
      color = Color.Empty;
      if (web)
        color = vColors.Colors[e.Index];
      else
        color = vColors.SystemColors[e.Index];
    }

    private void DrawContainerPanel(ColorBox panel, bool isMouseOver)
    {
      using (Graphics graphics = Graphics.FromHwnd(panel.Handle))
      {
        Rectangle clientRectangle = panel.ClientRectangle;
        graphics.DrawRectangle(SystemPens.ControlDark, clientRectangle);
        if (!isMouseOver)
          return;
        graphics.DrawRectangle(SystemPens.Highlight, clientRectangle.X, clientRectangle.Y, clientRectangle.Width - 1, clientRectangle.Height - 1);
      }
    }

    private void DrawContainerPanel(object sender, bool isMouseOver)
    {
      ColorBox colorBox = sender as ColorBox;
      using (Graphics graphics = Graphics.FromHwnd(colorBox.Handle))
      {
        Rectangle clientRectangle = colorBox.ClientRectangle;
        graphics.DrawRectangle(SystemPens.ControlDark, clientRectangle);
        if (!isMouseOver)
          return;
        graphics.DrawRectangle(SystemPens.Highlight, clientRectangle.X, clientRectangle.Y, clientRectangle.Width - 1, clientRectangle.Height - 1);
      }
    }

    private void InitializeColors()
    {
      this.panels = new ColorBox[vColors.CustomColors.Length];
      this.listBoxWebColors.Items.Clear();
      foreach (Color color in vColors.Colors)
      {
        this.listBoxWebColors.Items.Add(new ListItem()
        {
          Value = (object) color,
          Text = color.Name
        });
        if (color.Equals((object) this.selectedColor))
          this.listBoxWebColors.SelectedItem = this.listBoxWebColors.Items[this.listBoxWebColors.Items.Count - 1];
      }
      this.listBoxSystemColors.Items.Clear();
      foreach (Color systemColor in vColors.SystemColors)
      {
        this.listBoxSystemColors.Items.Add(new ListItem()
        {
          Value = (object) systemColor,
          Text = systemColor.Name
        });
        if (systemColor.Equals((object) this.selectedColor))
          this.listBoxSystemColors.SelectedItem = this.listBoxSystemColors.Items[this.listBoxSystemColors.Items.Count - 1];
      }
      this.listBoxSystemColors.Refresh();
      this.listBoxWebColors.Refresh();
      int num1 = 6;
      int num2 = num1;
      int num3 = num1;
      for (int index = 0; index < vColors.CustomColors.Length; ++index)
      {
        this.panels[index] = new ColorBox();
        this.panels[index].Left = num2;
        this.panels[index].Top = num3;
        this.panels[index].Height = 24;
        this.panels[index].Width = 22;
        this.panels[index].BackColor = vColors.CustomColors[index];
        this.panels[index].MouseEnter += new EventHandler(this.OnMouseEnterPanel);
        this.panels[index].MouseLeave += new EventHandler(this.OnMouseLeavePanel);
        this.panels[index].MouseDown += new MouseEventHandler(this.OnMouseDownPanel);
        this.panels[index].MouseUp += new MouseEventHandler(this.OnMouseUpPanel);
        this.panels[index].MouseClick += new MouseEventHandler(this.ColorPickerPanel_MouseClick);
        this.panels[index].Paint += new PaintEventHandler(this.OnPanelPaint);
        if (this.selectedColor.Equals((object) vColors.CustomColors[index]))
          this.panels[index].BorderStyle = BorderStyle.Fixed3D;
        this.customColorsTabPage.Controls.Add((Control) this.panels[index]);
        num2 += 24;
        if (num2 > this.customColorsTabPage.Width - 5)
        {
          num2 = num1;
          num3 += 26;
        }
      }
    }

    private void InitializeComponent()
    {
      this.ClientSize = new Size(0, 0);
      this.Text = "";
      this.Paint += new PaintEventHandler(this.PopupForm_Paint);
    }

    private void systemListBox_DrawItem(object sender, DrawItemEventArgs e)
    {
      this.DrawItem(false, sender, e);
    }

    private void listBoxWebColors_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.listBoxWebColors.SelectedItem == null)
        return;
      Color color = (Color) this.listBoxWebColors.SelectedItem.Value;
      if (!(color != this.selectedColor))
        return;
      this.selectedColor = color;
      if (this.SelectedColorChanged == null)
        return;
      this.SelectedColorChanged((object) this, EventArgs.Empty);
    }

    internal void FocusToSelectedColor()
    {
      this.listBoxSystemColors.SelectedIndex = -1;
      this.listBoxWebColors.SelectedIndex = -1;
      if (this.selectedColor == Color.Empty)
        return;
      foreach (ListItem listItem in this.listBoxSystemColors.Items)
      {
        if ((Color) listItem.Value == this.selectedColor)
        {
          this.listBoxSystemColors.SelectedItem = listItem;
          this.listBoxSystemColors.EnsureVisible(this.listBoxSystemColors.Items.IndexOf(listItem));
          this.tabControl.SelectedTab = this.systemColorsTabPage;
          this.Refresh();
          return;
        }
      }
      foreach (ListItem listItem in this.listBoxWebColors.Items)
      {
        if ((Color) listItem.Value == this.selectedColor)
        {
          this.listBoxWebColors.SelectedItem = listItem;
          this.listBoxWebColors.EnsureVisible(this.listBoxWebColors.Items.IndexOf(listItem));
          this.tabControl.SelectedTab = this.webTabPage;
          this.Refresh();
          break;
        }
      }
    }

    private void listBoxSystemColors_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.listBoxSystemColors.SelectedItem == null)
        return;
      this.selectedColor = (Color) this.listBoxSystemColors.SelectedItem.Value;
      if (this.SelectedColorChanged == null)
        return;
      this.SelectedColorChanged((object) this, EventArgs.Empty);
    }

    private void webListBox_DrawItem(object sender, DrawItemEventArgs e)
    {
      this.DrawItem(true, sender, e);
    }

    private void SetLocalizableStrings()
    {
      this.webTabPage.Text = vColors.WebString;
      this.systemColorsTabPage.Text = vColors.SystemString;
      this.customColorsTabPage.Text = vColors.CustomString;
    }

    private void OnMouseDownPanel(object sender, MouseEventArgs e)
    {
      foreach (Label panel in this.panels)
        panel.BorderStyle = BorderStyle.FixedSingle;
      (sender as ColorBox).BorderStyle = BorderStyle.Fixed3D;
    }

    private void OnMouseEnterPanel(object sender, EventArgs e)
    {
      if ((sender as ColorBox).BorderStyle == BorderStyle.Fixed3D)
        return;
      (sender as ColorBox).BorderStyle = BorderStyle.None;
    }

    private void OnMouseLeavePanel(object sender, EventArgs e)
    {
      if ((sender as ColorBox).BorderStyle == BorderStyle.Fixed3D)
        return;
      (sender as ColorBox).BorderStyle = BorderStyle.FixedSingle;
    }

    private void OnMouseUpPanel(object sender, MouseEventArgs e)
    {
      if (e != null && (e.Button & MouseButtons.Left) <= MouseButtons.None)
        return;
      this.selectedColor = ((Control) sender).BackColor;
      foreach (Color color in vColors.Colors)
      {
        if (color.ToArgb() == this.selectedColor.ToArgb())
        {
          this.selectedColor = color;
          break;
        }
      }
      if (this.SelectedColorChanged == null)
        return;
      this.SelectedColorChanged((object) this, EventArgs.Empty);
    }

    private void ColorPickerPanel_MouseClick(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Right)
        return;
      ColorBox colorBox = (ColorBox) sender;
      int num = -1;
      for (int index = 0; index < this.panels.Length; ++index)
      {
        if (this.panels[index] == colorBox)
        {
          num = index;
          break;
        }
      }
      if (num >= 48)
      {
        ColorDialog colorDialog = new ColorDialog();
        colorDialog.Color = colorBox.BackColor;
        colorDialog.FullOpen = true;
        if (colorDialog.ShowDialog() == DialogResult.OK)
          colorBox.BackColor = colorDialog.Color;
      }
      this.selectedColor = colorBox.BackColor;
      if (this.SelectedColorChanged == null)
        return;
      this.SelectedColorChanged((object) this, EventArgs.Empty);
    }

    private void OnPanelPaint(object sender, PaintEventArgs e)
    {
      this.DrawContainerPanel(sender, false);
    }

    private void PopupForm_Paint(object sender, PaintEventArgs e)
    {
      e.Graphics.DrawRectangle(SystemPens.ControlDark, 0, 0, this.Width - 1, this.Height - 1);
    }

    private void vTabControl_Paint(object sender, PaintEventArgs e)
    {
      if (this.tabControl.SelectedIndex == 3 && this.listBoxWebColors.SelectedItem != null)
      {
        this.listBoxWebColors.Focus();
      }
      else
      {
        if (this.tabControl.SelectedIndex != 4 || this.listBoxSystemColors.SelectedItem == null)
          return;
        this.listBoxSystemColors.Focus();
      }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (!this.DesignMode)
        Licensing.LICheck((Control) this);
      base.OnPaint(e);
      if (this.tabControl.UseTabsAreaBackColor)
      {
        this.tabControl.UseTabsAreaBackColor = false;
        this.tabControl.BackColor = Color.Transparent;
        this.tabControl.Refresh();
      }
      Color color = this.Enabled ? this.theme.StyleNormal.FillStyle.Colors[0] : this.theme.StyleDisabled.FillStyle.Colors[0];
      FillStyle fillStyle = this.theme.QueryFillStyleSetter("TabControlBackGround");
      if (fillStyle != null)
        color = fillStyle.Colors[0];
      using (Brush brush = (Brush) new SolidBrush(color))
        e.Graphics.FillRectangle(brush, this.ClientRectangle);
      using (Pen pen = new Pen(this.Enabled ? this.theme.StyleNormal.BorderColor : this.theme.StyleDisabled.BorderColor))
        e.Graphics.DrawRectangle(pen, this.Left, this.Top, this.Width - 1, this.Height - 1);
    }
  }
}
