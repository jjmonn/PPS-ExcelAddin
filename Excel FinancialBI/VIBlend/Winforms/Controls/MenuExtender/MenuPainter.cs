// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.MenuPaintHelper
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <exclude />
  internal class MenuPaintHelper
  {
    protected StringFormat stringFormat = new StringFormat(StringFormat.GenericTypographic);
    protected const int CHECK_SIZE = 16;
    protected const int MENU_SEPARATOR_HEIGHT = 2;
    protected const int MENU_BORDER_SIZE = 6;
    protected const int IMAGE_BUFFER = 8;
    protected const int SHORTCUT_BUFFER = 20;
    protected const int SHORTCUT_RIGHT_BUFFER = 15;
    protected const int TOPLEVEL_MENU_BUFFER = 11;
    protected const int TEXT_LEFT_BUFFER = 8;
    protected const int SMALL_IMAGE_SIZE = 16;
    protected const int MEDIUM_IMAGE_SIZE = 24;
    protected const int LARGE_IMAGE_SIZE = 32;
    protected const int EXTRA_LARGE_IMAGE_SIZE = 48;
    private Container components;
    protected Image checkImage;
    protected Image radioCheckImage;
    private BackgroundElement backFill;
    private ControlTheme theme;

    [Category("Appearance")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets the vMenuPainter Theme")]
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
        this.backFill.LoadTheme(value);
      }
    }

    public MenuPaintHelper(IContainer container)
    {
      this.InitializeComponent();
      this.Initialize();
      this.backFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) new vProgressBar());
      this.Theme = ControlTheme.GetDefaultTheme(VIBLEND_THEME.VISTABLUE);
      this.backFill.IsAnimated = false;
    }

    /// <summary>
    /// Initializes a new instance of the BaseMenuPainter class.
    /// </summary>
    public MenuPaintHelper(IDraw treme)
    {
      this.InitializeComponent();
      this.Initialize();
      this.backFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) new vProgressBar());
      this.Theme = ControlTheme.GetDefaultTheme(VIBLEND_THEME.ORANGEFRESH);
      this.backFill.IsAnimated = false;
    }

    public MenuPaintHelper(ControlTheme theme)
    {
      this.InitializeComponent();
      this.Initialize();
      this.backFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) new vProgressBar());
      this.backFill.LoadTheme(theme);
      this.backFill.IsAnimated = false;
      this.Theme = theme;
    }

    public MenuPaintHelper()
    {
      this.InitializeComponent();
      this.Initialize();
      this.backFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) new vProgressBar());
      this.Theme = ControlTheme.GetDefaultTheme(VIBLEND_THEME.ORANGEFRESH);
      this.backFill.IsAnimated = false;
    }

    private void Initialize()
    {
      this.stringFormat.HotkeyPrefix = HotkeyPrefix.Show;
      Stream resourceStream1 = this.GetResourceStream("VIBlend.WinForms.Controls.MenuExtender.MenuCheckMark.ico");
      if (resourceStream1 != null)
        this.checkImage = Image.FromStream(resourceStream1);
      Stream resourceStream2 = this.GetResourceStream("VIBlend.WinForms.Controls.MenuExtender.MenuRadioMark.ico");
      if (resourceStream2 == null)
        return;
      this.radioCheckImage = Image.FromStream(resourceStream2);
    }

    private Stream GetResourceStream(string resourceName)
    {
      return this.GetType().Assembly.GetManifestResourceStream(resourceName);
    }

    private int GetImageHeight(MenuItemImageSize imageSize)
    {
      int num;
      switch (imageSize)
      {
        case MenuItemImageSize.SIZE_SMALL:
          num = 16;
          break;
        case MenuItemImageSize.SIZE_MEDIUM:
          num = 24;
          break;
        case MenuItemImageSize.SIZE_LARGE:
          num = 32;
          break;
        case MenuItemImageSize.SIZE_VERYLARGE:
          num = 48;
          break;
        default:
          num = 16;
          break;
      }
      return num;
    }

    private void DrawMenuItem(MenuItem item, DrawItemEventArgs e, Image image, MenuItemImageSize imageSize)
    {
      int imageHeight = this.GetImageHeight(imageSize);
      this.PaintBackground(item, e, item.Parent is MainMenu, imageHeight);
      if (item.Text == "-")
      {
        this.PaintSeparator(e, imageHeight);
      }
      else
      {
        if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
          this.PaintHighlight(item, e, item.Parent is MainMenu);
        if ((e.State & DrawItemState.HotLight) == DrawItemState.HotLight)
        {
          this.PaintHotLight(item, e, item.Parent is MainMenu);
          this.PaintHighlight(item, e, item.Parent is MainMenu);
        }
        if (!(item.Parent is MainMenu))
        {
          int imageSize1 = imageHeight < 16 ? 16 : imageHeight;
          if (item.Checked)
            this.DrawMenuItemCheckMark(item, e, false, imageSize1);
          else if (item.RadioCheck)
            this.DrawMenuItemCheckMark(item, e, true, imageSize1);
          else if (image != null)
            this.DrawMenuItemImage(item, e, image, imageHeight);
        }
        this.DrawMenuItemText(item, e, imageHeight);
        if (item.Parent is MainMenu || this.GetShortcutText(item) == null)
          return;
        this.PaintShortcut(item, e);
      }
    }

    protected virtual int CalculateItemHeight(MenuItem item, MenuItemImageSize imageSize)
    {
      if (item.Text == "-")
        return 2;
      return this.GetImageHeight(imageSize) + 6;
    }

    protected virtual Color GetTextColor(DrawItemState state)
    {
      if ((state & DrawItemState.Disabled) == DrawItemState.Disabled)
        return this.Theme.StyleDisabled.TextColor;
      if ((state & DrawItemState.Selected) == DrawItemState.Selected)
        return this.Theme.StylePressed.TextColor;
      if ((state & DrawItemState.HotLight) == DrawItemState.HotLight)
        return this.Theme.StyleHighlight.TextColor;
      return this.Theme.StyleNormal.TextColor;
    }

    protected virtual void PaintMenuBar(Graphics g, Rectangle r)
    {
      FillStyleGradient fillStyleGradient = (FillStyleGradient) this.backFill.Theme.StyleNormal.FillStyle;
      using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(r, fillStyleGradient.Color1, fillStyleGradient.Color2, LinearGradientMode.Horizontal))
        g.FillRectangle((Brush) linearGradientBrush, r);
    }

    protected string GetShortcutText(MenuItem item)
    {
      if (!item.ShowShortcut || item.Shortcut == Shortcut.None)
        return (string) null;
      Keys keys = (Keys) item.Shortcut;
      return ((int) Convert.ToChar((object) Keys.Tab)).ToString() + TypeDescriptor.GetConverter(keys.GetType()).ConvertToString((object) keys);
    }

    protected virtual void MeasureMenuItem(MenuItem item, MeasureItemEventArgs e, MenuItemImageSize imageSize)
    {
      e.ItemHeight = this.CalculateItemHeight(item, imageSize);
      e.ItemWidth = this.CalculateItemWidth(item, e.Graphics, true, item.Parent is MainMenu, this.GetImageHeight(imageSize));
    }

    protected virtual int CalculateItemWidth(MenuItem item, Graphics g, bool showImages, bool topLevel, int imageWidth)
    {
      int num1 = (int) g.MeasureString(item.Text, SystemInformation.MenuFont, 1000, this.stringFormat).Width;
      int num2 = 0;
      string shortcutText = this.GetShortcutText(item);
      if (shortcutText != null)
        num2 = (int) g.MeasureString(shortcutText, SystemInformation.MenuFont, 1000, this.stringFormat).Width;
      if (topLevel)
        return num1 + 11;
      if (showImages)
        return num1 + 20 + num2 + 8 + imageWidth + 8;
      return num1 + 20 + num2 + 8;
    }

    protected virtual void PaintBackground(MenuItem item, DrawItemEventArgs e, bool TopLevel, int imageSize)
    {
      FillStyle fillStyle = this.backFill.Theme.StyleNormal.FillStyle;
      Graphics graphics = e.Graphics;
      if (TopLevel)
      {
        Rectangle rectangle = new Rectangle(e.Bounds.Location, new Size(e.Bounds.Width + 1, e.Bounds.Height));
        this.backFill.Bounds = e.Bounds;
        this.backFill.DrawElementFill(e.Graphics, ControlState.Normal);
        e.Graphics.DrawRectangle(new Pen(this.backFill.BorderColor), new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1));
      }
      else
      {
        using (SolidBrush solidBrush = new SolidBrush(fillStyle.Colors[0]))
          graphics.FillRectangle((Brush) solidBrush, e.Bounds);
        if (imageSize == 0)
          return;
        Color color2 = fillStyle.Colors[0];
        Color color1 = fillStyle.Colors[0];
        if (fillStyle.ColorsNumber > 1)
        {
          color2 = fillStyle.Colors[0];
          color1 = fillStyle.Colors[1];
        }
        if (fillStyle.ColorsNumber > 2)
        {
          color2 = fillStyle.Colors[2];
          color1 = fillStyle.Colors[3];
        }
        Rectangle rect = new Rectangle(e.Bounds.Location, new Size(imageSize + 8, e.Bounds.Size.Height));
        using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, color1, color2, 0.0f, false))
          graphics.FillRectangle((Brush) linearGradientBrush, rect);
      }
    }

    protected virtual void PaintHighlight(MenuItem item, DrawItemEventArgs e, bool TopLevel)
    {
      if (TopLevel)
      {
        Rectangle rect = new Rectangle(e.Bounds.Location, new Size(e.Bounds.Width, e.Bounds.Height - 1));
        this.backFill.Bounds = rect;
        this.backFill.DrawElementFill(e.Graphics, ControlState.Hover);
        e.Graphics.DrawRectangle(new Pen(this.backFill.HighlightBorderColor), rect);
      }
      else
      {
        Rectangle rect = new Rectangle(e.Bounds.Location, new Size(e.Bounds.Width - 2, e.Bounds.Height - 1));
        using (new SolidBrush(this.backFill.Theme.StyleHighlight.FillStyle.Colors[0]))
        {
          this.backFill.Bounds = rect;
          this.backFill.DrawElementFill(e.Graphics, ControlState.Hover);
        }
        e.Graphics.DrawRectangle(new Pen(this.backFill.HighlightBorderColor), rect);
      }
    }

    protected virtual void PaintHotLight(MenuItem item, DrawItemEventArgs e, bool TopLevel)
    {
      if (TopLevel)
      {
        Rectangle rect = new Rectangle(e.Bounds.Location, new Size(e.Bounds.Width, e.Bounds.Height - 1));
        this.backFill.Bounds = e.Bounds;
        this.backFill.DrawElementFill(e.Graphics, ControlState.Pressed);
        e.Graphics.DrawRectangle(new Pen(this.backFill.PressedBorderColor), rect);
      }
      else
      {
        Rectangle rect = new Rectangle(e.Bounds.Location, new Size(e.Bounds.Width - 2, e.Bounds.Height - 1));
        using (new SolidBrush(this.backFill.Theme.StylePressed.FillStyle.Colors[0]))
        {
          this.backFill.Bounds = e.Bounds;
          this.backFill.DrawElementFill(e.Graphics, ControlState.Pressed);
        }
        e.Graphics.DrawRectangle(new Pen(this.backFill.PressedBorderColor), rect);
      }
    }

    protected virtual void DrawMenuItemImage(MenuItem item, DrawItemEventArgs e, Image image, int imageSize)
    {
      if (image == null)
        return;
      int y = e.Bounds.Top + (e.Bounds.Height - imageSize) / 2;
      e.Graphics.DrawImage(image, new Rectangle(new Point(4, y), new Size(imageSize, imageSize)), 0, 0, imageSize, imageSize, GraphicsUnit.Pixel);
    }

    protected virtual void DrawMenuItemText(MenuItem item, DrawItemEventArgs e, int imageSize)
    {
      Font menuFont = SystemInformation.MenuFont;
      using (Brush brush = (Brush) new SolidBrush(this.GetTextColor(e.State)))
      {
        if (item.Parent is MainMenu)
          e.Graphics.DrawString(item.Text, menuFont, brush, (float) (e.Bounds.Left + 8), (float) (e.Bounds.Top + (e.Bounds.Height - menuFont.Height) / 2), this.stringFormat);
        else
          e.Graphics.DrawString(item.Text, menuFont, brush, (float) (e.Bounds.Left + 8 + imageSize + 8), (float) (e.Bounds.Top + (e.Bounds.Height - menuFont.Height) / 2), this.stringFormat);
      }
    }

    protected virtual void PaintSeparator(DrawItemEventArgs e, int imageSize)
    {
      int y = e.Bounds.Top + e.Bounds.Height / 2;
      e.Graphics.DrawLine(Pens.DarkGray, new Point(e.Bounds.Left + 8, y), new Point(e.Bounds.Left + e.Bounds.Width - 3, y));
    }

    protected virtual void PaintShortcut(MenuItem item, DrawItemEventArgs e)
    {
      Font menuFont = SystemInformation.MenuFont;
      using (Brush brush = (Brush) new SolidBrush(this.GetTextColor(e.State)))
      {
        string shortcutText = this.GetShortcutText(item);
        if (shortcutText == null)
          return;
        int num = (int) e.Graphics.MeasureString(shortcutText, menuFont, 1000, this.stringFormat).Width;
        e.Graphics.DrawString(shortcutText, menuFont, brush, (float) (e.Bounds.Width - 15 - num), (float) (e.Bounds.Top + (e.Bounds.Height - menuFont.Height) / 2), this.stringFormat);
      }
    }

    protected virtual void DrawMenuItemCheckMark(MenuItem item, DrawItemEventArgs e, bool Radio, int imageSize)
    {
      Rectangle rectangle = new Rectangle(new Point(4, e.Bounds.Top + (e.Bounds.Height - imageSize) / 2), new Size(imageSize, imageSize));
      e.Graphics.FillRectangle(SystemBrushes.Control, rectangle);
      if (this.radioCheckImage == null || this.checkImage == null)
        return;
      e.Graphics.DrawImage(Radio ? this.radioCheckImage : this.checkImage, rectangle, 0, 0, imageSize, imageSize, GraphicsUnit.Pixel);
    }

    public override string ToString()
    {
      return this.GetType().Name;
    }

    public void PaintBar(Graphics g, Rectangle r)
    {
      this.PaintMenuBar(g, r);
    }

    public void PaintItem(MenuItem item, DrawItemEventArgs e, Image image, MenuItemImageSize imageSize)
    {
      this.DrawMenuItem(item, e, image, imageSize);
    }

    public void MeasureItem(MenuItem item, MeasureItemEventArgs e, MenuItemImageSize imageSize)
    {
      this.MeasureMenuItem(item, e, imageSize);
    }

    private void InitializeComponent()
    {
      this.components = new Container();
    }
  }
}
