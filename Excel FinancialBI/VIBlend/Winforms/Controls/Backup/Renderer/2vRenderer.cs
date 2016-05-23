// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vToolStripProfessionalRenderer
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <exclude />
  public class vToolStripProfessionalRenderer : ToolStripProfessionalRenderer
  {
    private BackgroundElement backEl = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) new vProgressBar());
    private vColorTable table = new vColorTable();
    private int gripOffset = 1;
    private int sizingGripSquare = 2;
    private int sizingGripSize = 3;
    private int grip = 4;
    private int rows = 3;
    private int checkInset = 1;
    private int marginInset = 2;
    private int separatorInset = 31;
    private float cutToolItemMenu = 1f;
    private float cutMenuItemBack = 1.2f;
    private float checkTickWidth = 1.6f;
    private float cutContextMenu;
    private Blend statusStripBlend;
    private ControlTheme theme;
    private Color disabledText;
    private Color menuItemText;
    private Color statusItemText;
    private Color contextMenuItemText;
    private Color disabledArrow;
    private Color arrowColor1;
    private Color arrowColor2;
    private Color separatorColor1;
    private Color separatorColor2;
    private Color contextMenuColor;
    private Color checkBoxBorder;
    private Color checkBoxTick;
    private Color statusStripBorderColor1;
    private Color statusStripBorderColor2;
    private Color sizeGripColor1;
    private Color sizeGripColor2;
    private GradientColors contextMenuItemColors;
    private GradientColors disabledMenuItemColors;
    private GradientColors toolStripItemSelectedColors;
    private GradientColors toolStripItemPressedColors;
    private GradientColors checkedColors;
    private GradientColors checkedPressedColors;
    private VIBLEND_THEME defaultTheme;

    /// <summary>Gets or sets the VIblend theme.</summary>
    /// <value>The VIblend theme.</value>
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
        if (defaultTheme == null)
          return;
        this.Theme = defaultTheme;
      }
    }

    /// <summary>Gets or sets the theme.</summary>
    /// <value>The theme.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Appearance")]
    [Browsable(false)]
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
        this.backEl = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) new vProgressBar());
        this.backEl.LoadTheme(value);
        FillStyle fillStyle1 = this.theme.QueryFillStyleSetter("MenuNormal");
        if (fillStyle1 != null)
          this.theme.StyleNormal.FillStyle = fillStyle1;
        FillStyle fillStyle2 = this.theme.QueryFillStyleSetter("MenuHighlight");
        if (fillStyle2 != null)
          this.theme.StyleHighlight.FillStyle = fillStyle2;
        FillStyle fillStyle3 = this.theme.QueryFillStyleSetter("MenuPressed");
        if (fillStyle3 != null)
          this.theme.StylePressed.FillStyle = fillStyle3;
        Color color1 = Color.Empty;
        Color color2 = this.theme.QueryColorSetter("MenuBorder");
        Color color3 = Color.Empty;
        this.theme.QueryColorSetter("MainMenuBorder");
        Color color4 = Color.Empty;
        Color color5 = this.theme.QueryColorSetter("MenuItemTextColor");
        Color color6 = Color.Empty;
        Color color7 = this.theme.QueryColorSetter("ContextMenuBackColor");
        if (color7 != Color.Empty)
          this.contextMenuColor = color7;
        this.statusItemText = this.backEl.ForeColor;
        if (color5 != Color.Empty)
          this.statusItemText = color5;
        this.arrowColor2 = this.backEl.ForeColor;
        this.disabledArrow = this.backEl.DisabledBorderColor;
        this.menuItemText = this.backEl.ForeColor;
        if (color5 != Color.Empty)
          this.menuItemText = color5;
        this.contextMenuItemText = this.backEl.ForeColor;
        if (color5 != Color.Empty)
          this.contextMenuItemText = color5;
        Color baseColor = this.theme.QueryColorSetter("MenuSeparatorColor");
        this.statusStripBorderColor2 = this.backEl.BorderColor;
        this.statusStripBorderColor1 = this.backEl.BorderColor;
        this.separatorColor1 = this.backEl.BorderColor;
        if (baseColor != Color.Empty)
        {
          this.separatorColor2 = baseColor;
          this.separatorColor1 = ControlPaint.LightLight(baseColor);
        }
        this.statusStripBorderColor1 = this.backEl.BorderColor;
        this.checkBoxBorder = this.backEl.BorderColor;
        this.checkBoxTick = this.backEl.BorderColor;
        this.checkedColors = new GradientColors();
        this.checkedPressedColors = new GradientColors();
        this.toolStripItemPressedColors = new GradientColors();
        this.toolStripItemSelectedColors = new GradientColors();
        this.contextMenuItemColors = new GradientColors();
        this.disabledMenuItemColors = new GradientColors();
        this.toolStripItemPressedColors.Color1 = this.theme.StylePressed.FillStyle.Colors[0];
        this.toolStripItemPressedColors.Color2 = this.theme.StylePressed.FillStyle.Colors[1];
        this.checkedColors.Color1 = this.theme.StyleNormal.BorderColor;
        this.checkedColors.Color2 = this.theme.StyleNormal.BorderColor;
        this.checkedPressedColors.Color1 = this.theme.StyleNormal.BorderColor;
        this.checkedPressedColors.Color2 = this.theme.StyleNormal.BorderColor;
        if (this.theme.StylePressed.FillStyle.ColorsNumber > 2)
        {
          this.toolStripItemPressedColors.Color3 = this.theme.StylePressed.FillStyle.Colors[2];
          this.toolStripItemPressedColors.Color4 = this.theme.StylePressed.FillStyle.Colors[3];
          this.checkedColors.Color3 = this.theme.StyleNormal.BorderColor;
          this.checkedColors.Color4 = this.theme.StyleNormal.BorderColor;
          this.checkedPressedColors.Color3 = this.theme.StyleNormal.BorderColor;
          this.checkedPressedColors.Color4 = this.theme.StyleNormal.BorderColor;
        }
        this.toolStripItemPressedColors.BorderColor1 = this.backEl.PressedBorderColor;
        this.toolStripItemPressedColors.BorderColor1 = ControlPaint.Dark(this.backEl.PressedBorderColor);
        this.toolStripItemPressedColors.FillColor1 = this.theme.StylePressed.FillStyle.Colors[0];
        this.toolStripItemPressedColors.FillColor2 = this.theme.StylePressed.FillStyle.Colors[1];
        if (this.theme.StylePressed.FillStyle.ColorsNumber > 2)
        {
          this.toolStripItemPressedColors.FillColor3 = this.theme.StylePressed.FillStyle.Colors[2];
          this.toolStripItemPressedColors.FillColor4 = this.theme.StylePressed.FillStyle.Colors[3];
        }
        this.toolStripItemSelectedColors.Color1 = this.theme.StyleHighlight.FillStyle.Colors[1];
        this.toolStripItemSelectedColors.Color2 = this.theme.StyleHighlight.FillStyle.Colors[1];
        if (this.theme.StyleHighlight.FillStyle.ColorsNumber > 2)
        {
          this.toolStripItemSelectedColors.Color3 = this.theme.StyleHighlight.FillStyle.Colors[2];
          this.toolStripItemSelectedColors.Color4 = this.theme.StyleHighlight.FillStyle.Colors[3];
        }
        this.toolStripItemSelectedColors.BorderColor1 = this.backEl.PressedBorderColor;
        this.toolStripItemSelectedColors.BorderColor1 = this.backEl.PressedBorderColor;
        if (color2 != Color.Empty)
        {
          this.toolStripItemSelectedColors.BorderColor1 = color2;
          this.toolStripItemSelectedColors.BorderColor1 = color2;
        }
        this.toolStripItemSelectedColors.FillColor1 = this.theme.StyleHighlight.FillStyle.Colors[0];
        this.toolStripItemSelectedColors.FillColor2 = this.theme.StyleHighlight.FillStyle.Colors[1];
        if (this.theme.StyleHighlight.FillStyle.ColorsNumber > 2)
        {
          this.toolStripItemSelectedColors.FillColor3 = this.theme.StyleHighlight.FillStyle.Colors[2];
          this.toolStripItemSelectedColors.FillColor4 = this.theme.StyleHighlight.FillStyle.Colors[3];
        }
        this.contextMenuItemColors.Color1 = this.theme.StylePressed.FillStyle.Colors[0];
        this.contextMenuItemColors.Color2 = this.theme.StylePressed.FillStyle.Colors[1];
        if (this.theme.StylePressed.FillStyle.ColorsNumber > 2)
        {
          this.contextMenuItemColors.Color3 = this.theme.StylePressed.FillStyle.Colors[2];
          this.contextMenuItemColors.Color4 = this.theme.StylePressed.FillStyle.Colors[3];
        }
        this.contextMenuItemColors.BorderColor1 = this.backEl.PressedBorderColor;
        this.contextMenuItemColors.BorderColor2 = this.backEl.PressedBorderColor;
        if (color2 != Color.Empty)
        {
          this.contextMenuItemColors.BorderColor1 = color2;
          this.contextMenuItemColors.BorderColor2 = color2;
        }
        this.contextMenuItemColors.FillColor1 = this.theme.StylePressed.FillStyle.Colors[0];
        this.contextMenuItemColors.FillColor2 = this.theme.StylePressed.FillStyle.Colors[1];
        if (this.theme.StylePressed.FillStyle.ColorsNumber > 2)
        {
          this.contextMenuItemColors.FillColor3 = this.theme.StylePressed.FillStyle.Colors[2];
          this.contextMenuItemColors.FillColor4 = this.theme.StylePressed.FillStyle.Colors[3];
        }
        this.disabledMenuItemColors.Color1 = this.theme.StyleDisabled.FillStyle.Colors[0];
        this.disabledMenuItemColors.Color2 = this.theme.StyleDisabled.FillStyle.Colors[1];
        if (this.theme.StyleDisabled.FillStyle.ColorsNumber > 2)
        {
          this.disabledMenuItemColors.Color3 = this.theme.StyleDisabled.FillStyle.Colors[1];
          this.disabledMenuItemColors.Color4 = this.theme.StyleDisabled.FillStyle.Colors[1];
        }
        this.disabledMenuItemColors.BorderColor1 = this.backEl.DisabledBorderColor;
        this.disabledMenuItemColors.BorderColor1 = this.backEl.DisabledBorderColor;
        this.disabledMenuItemColors.FillColor1 = this.theme.StyleDisabled.FillStyle.Colors[0];
        this.disabledMenuItemColors.FillColor2 = this.theme.StyleDisabled.FillStyle.Colors[1];
        if (this.theme.StyleDisabled.FillStyle.ColorsNumber <= 2)
          return;
        this.disabledMenuItemColors.FillColor3 = this.theme.StyleDisabled.FillStyle.Colors[1];
        this.disabledMenuItemColors.FillColor4 = this.theme.StyleDisabled.FillStyle.Colors[1];
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:vRenderer" /> class.
    /// </summary>
    /// <param name="table">The table.</param>
    public vToolStripProfessionalRenderer(vColorTable table)
      : base((ProfessionalColorTable) table)
    {
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:vRenderer" /> class.
    /// </summary>
    public vToolStripProfessionalRenderer()
      : base((ProfessionalColorTable) new vColorTable())
    {
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
      this.statusStripBlend = new Blend();
      this.statusStripBlend.Positions = new float[6]
      {
        0.0f,
        0.25f,
        0.25f,
        0.57f,
        0.86f,
        1f
      };
      this.statusStripBlend.Factors = new float[6]
      {
        0.1f,
        0.6f,
        1f,
        0.4f,
        0.0f,
        0.95f
      };
    }

    protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
    {
      if (e.ArrowRectangle.Width <= 0 || e.ArrowRectangle.Height <= 0)
        return;
      using (GraphicsPath arrow = this.GetArrow(e.Item, e.ArrowRectangle, e.Direction))
      {
        RectangleF bounds = arrow.GetBounds();
        bounds.Inflate(1f, 1f);
        Color color1 = e.Item.Enabled ? this.arrowColor1 : this.disabledArrow;
        Color color2 = e.Item.Enabled ? this.arrowColor2 : this.disabledArrow;
        float angle = 0.0f;
        switch (e.Direction)
        {
          case ArrowDirection.Left:
            angle = 180f;
            break;
          case ArrowDirection.Up:
            angle = 270f;
            break;
          case ArrowDirection.Right:
            angle = 0.0f;
            break;
          case ArrowDirection.Down:
            angle = 90f;
            break;
        }
        using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(bounds, color1, color2, angle))
          e.Graphics.FillPath((Brush) linearGradientBrush, arrow);
      }
    }

    protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
    {
      ToolStripButton toolStripButton = (ToolStripButton) e.Item;
      if (!toolStripButton.Selected && !toolStripButton.Pressed && !toolStripButton.Checked)
        return;
      this.DrawToolButtonBackground(e.Graphics, toolStripButton, e.ToolStrip);
    }

    protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
    {
      if (!e.Item.Selected && !e.Item.Pressed)
        return;
      this.DrawToolDropButtonBackground(e.Graphics, e.Item, e.ToolStrip);
    }

    protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
    {
      Rectangle imageRectangle = e.ImageRectangle;
      imageRectangle.Inflate(1, 1);
      if (imageRectangle.Top > this.checkInset)
      {
        int num = imageRectangle.Top - this.checkInset;
        imageRectangle.Y -= num;
        imageRectangle.Height += num;
      }
      if (imageRectangle.Height <= e.Item.Bounds.Height - this.checkInset * 2)
      {
        int num = e.Item.Bounds.Height - this.checkInset * 2 - imageRectangle.Height;
        imageRectangle.Height += num;
      }
      Graphics graphics = e.Graphics;
      SmoothingMode smoothingMode = graphics.SmoothingMode;
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      using (GraphicsPath border = this.GetBorder(imageRectangle, this.cutMenuItemBack))
      {
        using (SolidBrush solidBrush = new SolidBrush(this.ColorTable.CheckBackground))
          e.Graphics.FillPath((Brush) solidBrush, border);
        using (Pen pen = new Pen(this.checkBoxBorder))
          e.Graphics.DrawPath(pen, border);
        if (e.Image != null)
        {
          CheckState checkState = CheckState.Unchecked;
          if (e.Item is ToolStripMenuItem)
            checkState = ((ToolStripMenuItem) e.Item).CheckState;
          this.DrawCheckTick(e, imageRectangle, checkState);
        }
      }
      graphics.SmoothingMode = smoothingMode;
    }

    private Rectangle DrawCheckTick(ToolStripItemImageRenderEventArgs e, Rectangle checkBox, CheckState checkState)
    {
      switch (checkState)
      {
        case CheckState.Checked:
          using (GraphicsPath checkBoxTick = this.GetCheckBoxTick(checkBox))
          {
            using (Pen pen = new Pen(this.checkBoxTick, this.checkTickWidth))
            {
              e.Graphics.DrawPath(pen, checkBoxTick);
              break;
            }
          }
        case CheckState.Indeterminate:
          using (GraphicsPath boxIndeterminate = this.GetCheckBoxIndeterminate(checkBox))
          {
            using (SolidBrush solidBrush = new SolidBrush(this.checkBoxTick))
            {
              e.Graphics.FillPath((Brush) solidBrush, boxIndeterminate);
              break;
            }
          }
      }
      return checkBox;
    }

    protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
    {
      if (e.ToolStrip is MenuStrip || e.ToolStrip != null || (e.ToolStrip is ContextMenuStrip || e.ToolStrip is ToolStripDropDownMenu))
      {
        e.TextColor = e.Item.Enabled ? (!(e.ToolStrip is MenuStrip) || e.Item.Pressed || e.Item.Selected ? (!(e.ToolStrip is StatusStrip) || e.Item.Pressed || e.Item.Selected ? this.contextMenuItemText : this.statusItemText) : this.menuItemText) : this.disabledText;
        base.OnRenderItemText(e);
      }
      else
        base.OnRenderItemText(e);
    }

    protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
    {
      if (e.ToolStrip is ContextMenuStrip || e.ToolStrip is ToolStripDropDownMenu)
      {
        if (e.Image == null)
          return;
        if (e.Item.Enabled)
          e.Graphics.DrawImage(e.Image, e.ImageRectangle);
        else
          ControlPaint.DrawImageDisabled(e.Graphics, e.Image, e.ImageRectangle.X, e.ImageRectangle.Y, Color.Transparent);
      }
      else
        base.OnRenderItemImage(e);
    }

    protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
    {
      if (e.ToolStrip is MenuStrip || e.ToolStrip is ContextMenuStrip || e.ToolStrip is ToolStripDropDownMenu)
      {
        if (e.Item.Pressed && e.ToolStrip is MenuStrip)
        {
          this.DrawContextMenuCaption(e.Graphics, e.Item);
        }
        else
        {
          if (!e.Item.Selected)
            return;
          if (e.Item.Enabled)
          {
            if (e.ToolStrip is MenuStrip)
              this.DrawRootItem(e.Graphics, e.Item, this.toolStripItemSelectedColors);
            else
              this.DrawContextMenuItem(e.Graphics, e.Item, this.contextMenuItemColors);
          }
          else
          {
            Point client = e.ToolStrip.PointToClient(Control.MousePosition);
            if (e.Item.Bounds.Contains(client))
              return;
            if (e.ToolStrip is MenuStrip)
              this.DrawRootItem(e.Graphics, e.Item, this.disabledMenuItemColors);
            else
              this.DrawContextMenuItem(e.Graphics, e.Item, this.disabledMenuItemColors);
          }
        }
      }
      else
        base.OnRenderMenuItemBackground(e);
    }

    protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e)
    {
      if (e.Item.Selected || e.Item.Pressed)
      {
        ToolStripSplitButton splitButton = (ToolStripSplitButton) e.Item;
        this.DrawSplitButtonBackground(e.Graphics, splitButton, e.ToolStrip);
        Rectangle downButtonBounds = splitButton.DropDownButtonBounds;
        this.OnRenderArrow(new ToolStripArrowRenderEventArgs(e.Graphics, (ToolStripItem) splitButton, downButtonBounds, SystemColors.ControlText, ArrowDirection.Down));
      }
      else
        base.OnRenderSplitButtonBackground(e);
    }

    protected override void OnRenderStatusStripSizingGrip(ToolStripRenderEventArgs e)
    {
      using (SolidBrush darkBrush = new SolidBrush(this.sizeGripColor1))
      {
        using (SolidBrush lightBrush = new SolidBrush(this.sizeGripColor2))
        {
          bool rightToLeft = e.ToolStrip.RightToLeft == RightToLeft.Yes;
          int y = e.AffectedBounds.Bottom - this.sizingGripSize * 2 + 1;
          this.DrawGripRows(e, darkBrush, lightBrush, rightToLeft, y);
        }
      }
    }

    private int DrawGripRows(ToolStripRenderEventArgs e, SolidBrush darkBrush, SolidBrush lightBrush, bool rightToLeft, int y)
    {
      for (int i = this.rows; i >= 1; --i)
      {
        int x = rightToLeft ? e.AffectedBounds.Left + 1 : e.AffectedBounds.Right - this.sizingGripSize * 2 + 1;
        this.DrawHorizontalGrips(e, darkBrush, lightBrush, rightToLeft, y, i, x);
        y -= this.grip;
      }
      return y;
    }

    private int DrawHorizontalGrips(ToolStripRenderEventArgs e, SolidBrush darkBrush, SolidBrush lightBrush, bool rtl, int y, int i, int x)
    {
      for (int index = 0; index < i; ++index)
      {
        this.DrawGrip(e.Graphics, x, y, (Brush) darkBrush, (Brush) lightBrush);
        x -= rtl ? -this.grip : this.grip;
      }
      return x;
    }

    protected override void OnRenderToolStripContentPanelBackground(ToolStripContentPanelRenderEventArgs e)
    {
      base.OnRenderToolStripContentPanelBackground(e);
      if (e.ToolStripContentPanel.Width <= 0 || e.ToolStripContentPanel.Height <= 0)
        return;
      using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(e.ToolStripContentPanel.ClientRectangle, this.ColorTable.ToolStripContentPanelGradientEnd, this.ColorTable.ToolStripContentPanelGradientBegin, 90f))
        e.Graphics.FillRectangle((Brush) linearGradientBrush, e.ToolStripContentPanel.ClientRectangle);
    }

    protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
    {
      if (e.ToolStrip is ContextMenuStrip || e.ToolStrip is ToolStripDropDownMenu)
      {
        using (Pen lightPen = new Pen(this.separatorColor1))
        {
          using (Pen darkPen = new Pen(this.separatorColor2))
            this.DrawSeparator(e.Graphics, e.Vertical, e.Item.Bounds, lightPen, darkPen, this.separatorInset, e.ToolStrip.RightToLeft == RightToLeft.Yes);
        }
      }
      else if (e.ToolStrip is StatusStrip)
      {
        using (Pen lightPen = new Pen(this.ColorTable.SeparatorLight))
        {
          using (Pen darkPen = new Pen(this.ColorTable.SeparatorDark))
            this.DrawSeparator(e.Graphics, e.Vertical, e.Item.Bounds, lightPen, darkPen, 0, false);
        }
      }
      else
        base.OnRenderSeparator(e);
    }

    protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
    {
      if (e.ToolStrip is ContextMenuStrip || e.ToolStrip is ToolStripDropDownMenu)
      {
        using (GraphicsPath border = this.GetBorder(e.AffectedBounds, this.cutContextMenu))
        {
          using (GraphicsPath clippedBorder = this.GetClippedBorder(e.AffectedBounds, this.cutContextMenu))
          {
            Region region = e.Graphics.Clip.Clone();
            region.Intersect(clippedBorder);
            e.Graphics.Clip = region;
            using (SolidBrush solidBrush = new SolidBrush(this.contextMenuColor))
              e.Graphics.FillPath((Brush) solidBrush, border);
          }
        }
      }
      else if (e.ToolStrip is StatusStrip)
      {
        RectangleF rect = new RectangleF(0.0f, 1.5f, (float) e.ToolStrip.Width, (float) (e.ToolStrip.Height - 2));
        if ((double) rect.Width <= 0.0 || (double) rect.Height <= 0.0)
          return;
        using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, this.ColorTable.StatusStripGradientBegin, this.ColorTable.StatusStripGradientEnd, 90f))
          e.Graphics.FillRectangle((Brush) linearGradientBrush, rect);
      }
      else
        base.OnRenderToolStripBackground(e);
    }

    protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
    {
      if (e.ToolStrip is ContextMenuStrip || e.ToolStrip is ToolStripDropDownMenu)
      {
        Rectangle affectedBounds = e.AffectedBounds;
        bool flag = e.ToolStrip.RightToLeft == RightToLeft.Yes;
        affectedBounds.Y += this.marginInset;
        affectedBounds.Height -= this.marginInset * 2;
        if (!flag)
          affectedBounds.X += this.marginInset;
        else
          affectedBounds.X += this.marginInset / 2;
        using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(affectedBounds, this.ColorTable.ImageMarginGradientBegin, this.ColorTable.ImageMarginGradientEnd, LinearGradientMode.Vertical))
          e.Graphics.FillRectangle((Brush) linearGradientBrush, affectedBounds);
        using (Pen pen = new Pen(this.separatorColor1))
        {
          using (new Pen(this.separatorColor2))
          {
            if (!flag)
              e.Graphics.DrawLine(pen, affectedBounds.Right, affectedBounds.Top, affectedBounds.Right, affectedBounds.Bottom);
            else
              e.Graphics.DrawLine(pen, affectedBounds.Left - 1, affectedBounds.Top, affectedBounds.Left - 1, affectedBounds.Bottom);
          }
        }
      }
      else
        base.OnRenderImageMargin(e);
    }

    protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
    {
      if (e.ToolStrip is ContextMenuStrip || e.ToolStrip is ToolStripDropDownMenu)
      {
        using (GraphicsPath roundedBorder = this.CreateRoundedBorder(e.AffectedBounds, e.ConnectedArea, this.cutContextMenu))
        {
          using (GraphicsPath clippedBorder = this.GetClippedBorder(e.AffectedBounds, e.ConnectedArea, this.cutContextMenu))
          {
            Region region = e.Graphics.Clip.Clone();
            region.Intersect(clippedBorder);
            e.Graphics.Clip = region;
            SmoothingMode smoothingMode = e.Graphics.SmoothingMode;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (Pen pen = new Pen(this.ColorTable.MenuBorder))
              e.Graphics.DrawPath(pen, roundedBorder);
            e.Graphics.SmoothingMode = smoothingMode;
          }
        }
      }
      else if (e.ToolStrip is StatusStrip)
      {
        using (Pen pen1 = new Pen(this.statusStripBorderColor1))
        {
          using (Pen pen2 = new Pen(this.statusStripBorderColor2))
          {
            e.Graphics.DrawLine(pen1, 0, 0, e.ToolStrip.Width, 0);
            e.Graphics.DrawLine(pen2, 0, 1, e.ToolStrip.Width, 1);
          }
        }
      }
      else
        base.OnRenderToolStripBorder(e);
    }

    private void DrawToolButtonBackground(Graphics g, ToolStripButton toolStripButton, ToolStrip toolstrip)
    {
      if (toolStripButton.Enabled)
      {
        if (toolStripButton.Checked)
        {
          if (toolStripButton.Pressed)
            this.DrawRootItem(g, (ToolStripItem) toolStripButton, this.toolStripItemPressedColors);
          else if (toolStripButton.Selected)
            this.DrawRootItem(g, (ToolStripItem) toolStripButton, this.checkedPressedColors);
          else
            this.DrawRootItem(g, (ToolStripItem) toolStripButton, this.checkedColors);
        }
        else if (toolStripButton.Pressed)
        {
          this.DrawRootItem(g, (ToolStripItem) toolStripButton, this.toolStripItemPressedColors);
        }
        else
        {
          if (!toolStripButton.Selected)
            return;
          this.DrawRootItem(g, (ToolStripItem) toolStripButton, this.toolStripItemSelectedColors);
        }
      }
      else
      {
        if (!toolStripButton.Selected)
          return;
        Point client = toolstrip.PointToClient(Control.MousePosition);
        if (toolStripButton.Bounds.Contains(client))
          return;
        this.DrawRootItem(g, (ToolStripItem) toolStripButton, this.disabledMenuItemColors);
      }
    }

    private void DrawToolDropButtonBackground(Graphics g, ToolStripItem item, ToolStrip toolstrip)
    {
      if (!item.Selected && !item.Pressed)
        return;
      if (item.Enabled)
      {
        if (item.Pressed)
          this.DrawContextMenuCaption(g, item);
        else
          this.DrawRootItem(g, item, this.toolStripItemSelectedColors);
      }
      else
      {
        Point client = toolstrip.PointToClient(Control.MousePosition);
        if (item.Bounds.Contains(client))
          return;
        this.DrawRootItem(g, item, this.disabledMenuItemColors);
      }
    }

    private void DrawSplitButtonBackground(Graphics g, ToolStripSplitButton splitButton, ToolStrip toolstrip)
    {
      if (!splitButton.Selected && !splitButton.Pressed)
        return;
      if (splitButton.Enabled)
      {
        if (!splitButton.Pressed && splitButton.ButtonPressed)
          this.DrawSplitItem(g, splitButton, this.toolStripItemPressedColors, this.toolStripItemSelectedColors, this.contextMenuItemColors);
        else if (splitButton.Pressed && !splitButton.ButtonPressed)
          this.DrawContextMenuCaption(g, (ToolStripItem) splitButton);
        else
          this.DrawSplitItem(g, splitButton, this.toolStripItemSelectedColors, this.toolStripItemSelectedColors, this.contextMenuItemColors);
      }
      else
      {
        Point client = toolstrip.PointToClient(Control.MousePosition);
        if (splitButton.Bounds.Contains(client))
          return;
        this.DrawRootItem(g, (ToolStripItem) splitButton, this.disabledMenuItemColors);
      }
    }

    private void DrawRootItem(Graphics g, ToolStripItem item, GradientColors colors)
    {
      Rectangle backRect = new Rectangle(2, 0, item.Bounds.Width - 3, item.Bounds.Height);
      this.DrawItem(g, backRect, colors);
    }

    private void DrawSplitItem(Graphics g, ToolStripSplitButton splitButton, GradientColors colorsButton, GradientColors colorsDrop, GradientColors colorsSplit)
    {
      Rectangle rectangle = new Rectangle(Point.Empty, splitButton.Bounds.Size);
      Rectangle downButtonBounds = splitButton.DropDownButtonBounds;
      if (rectangle.Width <= 0 || downButtonBounds.Width <= 0 || (rectangle.Height <= 0 || downButtonBounds.Height <= 0))
        return;
      Rectangle backRect = rectangle;
      int num;
      if (downButtonBounds.X > 0)
      {
        backRect.Width = downButtonBounds.Left;
        --downButtonBounds.X;
        ++downButtonBounds.Width;
        num = downButtonBounds.X;
      }
      else
      {
        backRect.Width -= downButtonBounds.Width - 2;
        backRect.X = downButtonBounds.Right - 1;
        ++downButtonBounds.Width;
        num = downButtonBounds.Right - 1;
      }
      using (this.GetBorder(rectangle, this.cutMenuItemBack))
      {
        this.DrawBackGround(g, backRect, colorsButton);
        this.DrawBackGround(g, downButtonBounds, colorsDrop);
        using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Rectangle(rectangle.X + num, rectangle.Top, 1, rectangle.Height + 1), colorsSplit.BorderColor1, colorsSplit.BorderColor2, 90f))
        {
          linearGradientBrush.SetSigmaBellShape(0.5f);
          using (Pen pen = new Pen((Brush) linearGradientBrush))
            g.DrawLine(pen, rectangle.X + num, rectangle.Top + 1, rectangle.X + num, rectangle.Bottom - 1);
        }
        this.DrawBorder(g, rectangle, colorsButton);
      }
    }

    private void DrawContextMenuCaption(Graphics g, ToolStripItem item)
    {
      Rectangle rect = new Rectangle(Point.Empty, item.Bounds.Size);
      using (GraphicsPath border = this.GetBorder(rect, this.cutToolItemMenu))
      {
        using (this.GetBorderWithOffset(rect, this.cutToolItemMenu))
        {
          using (GraphicsPath clippedBorder = this.GetClippedBorder(rect, this.cutToolItemMenu))
          {
            Region region = g.Clip.Clone();
            region.Intersect(clippedBorder);
            g.Clip = region;
            using (SolidBrush solidBrush = new SolidBrush(this.contextMenuColor))
              g.FillPath((Brush) solidBrush, border);
            using (Pen pen = new Pen(this.ColorTable.MenuBorder))
              g.DrawPath(pen, border);
          }
        }
      }
    }

    private void DrawContextMenuItem(Graphics g, ToolStripItem item, GradientColors colors)
    {
      Rectangle backRect = new Rectangle(2, 0, item.Bounds.Width - 3, item.Bounds.Height);
      this.DrawItem(g, backRect, colors);
    }

    private void DrawItem(Graphics g, Rectangle backRect, GradientColors colors)
    {
      if (backRect.Width <= 0 || backRect.Height <= 0)
        return;
      this.DrawBackGround(g, backRect, colors);
      this.DrawBorder(g, backRect, colors);
    }

    private void DrawBackGround(Graphics g, Rectangle backRect, GradientColors colors)
    {
      backRect.Inflate(-1, -1);
      PaintHelper paintHelper = new PaintHelper();
      float[] colorOffsets = new float[4]{ 0.0f, 0.25f, 0.75f, 1f };
      Color[] colorStops = new Color[4]{ colors.Color1, colors.Color2, colors.Color3, colors.Color4 };
      paintHelper.DrawGradientRectFigure(g, backRect, colorStops, colorOffsets, GradientStyles.Linear, 90.0, 0.5, 0.5);
    }

    private void DrawBorder(Graphics g, Rectangle backRect, GradientColors colors)
    {
      SmoothingMode smoothingMode = g.SmoothingMode;
      g.SmoothingMode = SmoothingMode.AntiAlias;
      Rectangle rect = new Rectangle(3, 1, backRect.Width - 3, backRect.Height - 2);
      g.DrawRectangle(new Pen(colors.BorderColor1), rect);
      g.SmoothingMode = smoothingMode;
    }

    private void DrawGrip(Graphics g, int x, int y, Brush darkBrush, Brush lightBrush)
    {
      g.FillRectangle(lightBrush, x + this.gripOffset, y + this.gripOffset, this.sizingGripSquare, this.sizingGripSquare);
      g.FillRectangle(darkBrush, x, y, this.sizingGripSquare, this.sizingGripSquare);
    }

    private void DrawSeparator(Graphics g, bool vertical, Rectangle rect, Pen lightPen, Pen darkPen, int horizontalInset, bool rtl)
    {
      if (vertical)
        rect = vToolStripProfessionalRenderer.DrawVerticalSeparator(g, rect, lightPen, darkPen);
      else
        rect = vToolStripProfessionalRenderer.DrawHorizontalSeparator(g, rect, lightPen, darkPen, horizontalInset, rtl);
    }

    private static Rectangle DrawHorizontalSeparator(Graphics g, Rectangle rect, Pen lightPen, Pen darkPen, int hInset, bool rtl)
    {
      int num = rect.Height / 2;
      int x1 = rect.X + (rtl ? 0 : hInset);
      int x2 = rect.Right - (rtl ? hInset : 0);
      g.DrawLine(darkPen, x1, num, x2, num);
      g.DrawLine(lightPen, x1, num + 1, x2, num + 1);
      return rect;
    }

    private static Rectangle DrawVerticalSeparator(Graphics g, Rectangle rect, Pen lightPen, Pen darkPen)
    {
      int num = rect.Width / 2;
      int y = rect.Y;
      int bottom = rect.Bottom;
      g.DrawLine(darkPen, num, y, num, bottom);
      g.DrawLine(lightPen, num + 1, y, num + 1, bottom);
      return rect;
    }

    private GraphicsPath CreateRoundedBorder(Rectangle rect, Rectangle exclude, float cut)
    {
      return this.GetBorder(rect, cut);
    }

    private GraphicsPath GetBorder(Rectangle rect, float cut)
    {
      --rect.Width;
      --rect.Height;
      GraphicsPath graphicsPath = new GraphicsPath();
      graphicsPath.AddLine((float) rect.Left + cut, (float) rect.Top, (float) rect.Right - cut, (float) rect.Top);
      graphicsPath.AddLine((float) rect.Right - cut, (float) rect.Top, (float) rect.Right, (float) rect.Top + cut);
      graphicsPath.AddLine((float) rect.Right, (float) rect.Top + cut, (float) rect.Right, (float) rect.Bottom - cut);
      graphicsPath.AddLine((float) rect.Right, (float) rect.Bottom - cut, (float) rect.Right - cut, (float) rect.Bottom);
      graphicsPath.AddLine((float) rect.Right - cut, (float) rect.Bottom, (float) rect.Left + cut, (float) rect.Bottom);
      graphicsPath.AddLine((float) rect.Left + cut, (float) rect.Bottom, (float) rect.Left, (float) rect.Bottom - cut);
      graphicsPath.AddLine((float) rect.Left, (float) rect.Bottom - cut, (float) rect.Left, (float) rect.Top + cut);
      graphicsPath.AddLine((float) rect.Left, (float) rect.Top + cut, (float) rect.Left + cut, (float) rect.Top);
      return graphicsPath;
    }

    private GraphicsPath GetBorderWithOffset(Rectangle rect, float cut)
    {
      rect.Inflate(-1, -1);
      return this.GetBorder(rect, cut);
    }

    private GraphicsPath GetBorderWithOffset(Rectangle rect, Rectangle exclude, float cut)
    {
      rect.Inflate(-1, -1);
      return this.CreateRoundedBorder(rect, exclude, cut);
    }

    private GraphicsPath GetClippedBorder(Rectangle rect, float cut)
    {
      ++rect.Width;
      ++rect.Height;
      return this.GetBorder(rect, cut);
    }

    private GraphicsPath GetClippedBorder(Rectangle rect, Rectangle exclude, float cut)
    {
      ++rect.Width;
      ++rect.Height;
      return this.CreateRoundedBorder(rect, exclude, cut);
    }

    private GraphicsPath GetArrow(ToolStripItem item, Rectangle rect, ArrowDirection direction)
    {
      int num1;
      int num2;
      if (direction == ArrowDirection.Left || direction == ArrowDirection.Right)
      {
        num1 = rect.Right - (rect.Width - 4) / 2;
        num2 = rect.Y + rect.Height / 2;
      }
      else
      {
        num1 = rect.X + rect.Width / 2;
        num2 = rect.Bottom - (rect.Height - 3) / 2;
        if (item is ToolStripDropDownButton && item.RightToLeft == RightToLeft.Yes)
          ++num1;
      }
      GraphicsPath graphicsPath = new GraphicsPath();
      switch (direction)
      {
        case ArrowDirection.Left:
          graphicsPath.AddLine(num1 - 4, num2, num1, num2 - 4);
          graphicsPath.AddLine(num1, num2 - 4, num1, num2 + 4);
          graphicsPath.AddLine(num1, num2 + 4, num1 - 4, num2);
          break;
        case ArrowDirection.Up:
          graphicsPath.AddLine((float) num1 + 3f, (float) num2, (float) num1 - 3f, (float) num2);
          graphicsPath.AddLine((float) num1 - 3f, (float) num2, (float) num1, (float) num2 - 4f);
          graphicsPath.AddLine((float) num1, (float) num2 - 4f, (float) num1 + 3f, (float) num2);
          break;
        case ArrowDirection.Right:
          graphicsPath.AddLine(num1, num2, num1 - 4, num2 - 4);
          graphicsPath.AddLine(num1 - 4, num2 - 4, num1 - 4, num2 + 4);
          graphicsPath.AddLine(num1 - 4, num2 + 4, num1, num2);
          break;
        case ArrowDirection.Down:
          graphicsPath.AddLine((float) num1 + 3f, (float) num2 - 3f, (float) num1 - 2f, (float) num2 - 3f);
          graphicsPath.AddLine((float) num1 - 2f, (float) num2 - 3f, (float) num1, (float) num2);
          graphicsPath.AddLine((float) num1, (float) num2, (float) num1 + 3f, (float) num2 - 3f);
          break;
      }
      return graphicsPath;
    }

    private GraphicsPath GetCheckBoxTick(Rectangle rect)
    {
      int num = rect.X + rect.Width / 2;
      int y1 = rect.Y + rect.Height / 2;
      GraphicsPath graphicsPath = new GraphicsPath();
      graphicsPath.AddLine(num - 4, y1, num - 2, y1 + 4);
      graphicsPath.AddLine(num - 2, y1 + 4, num + 3, y1 - 5);
      return graphicsPath;
    }

    private GraphicsPath GetCheckBoxIndeterminate(Rectangle rect)
    {
      int num1 = rect.X + rect.Width / 2;
      int num2 = rect.Y + rect.Height / 2;
      GraphicsPath graphicsPath = new GraphicsPath();
      graphicsPath.AddLine(num1 - 3, num2, num1, num2 - 3);
      graphicsPath.AddLine(num1, num2 - 3, num1 + 3, num2);
      graphicsPath.AddLine(num1 + 3, num2, num1, num2 + 3);
      graphicsPath.AddLine(num1, num2 + 3, num1 - 3, num2);
      return graphicsPath;
    }
  }
}
