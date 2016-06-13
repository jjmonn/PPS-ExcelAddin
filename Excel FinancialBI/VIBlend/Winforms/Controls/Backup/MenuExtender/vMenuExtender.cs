// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vMenuExtender
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  [ToolboxItem(false)]
  [DefaultProperty("ImageList")]
  [ProvideProperty("ImageIndex", typeof (Component))]
  public class vMenuExtender : Component, IExtenderProvider
  {
    private Hashtable hashTable = new Hashtable();
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private const int MIM_BACKGROUND = 2;
    private const int WM_NCPAINT = 133;
    private Container components;
    private ImageList imageList;
    private Form form;
    private Menu mainMenu;
    private IntPtr BarBackgroundBrush;
    private Bitmap bitmapBackMenu;
    private MenuPaintHelper menuPaintHelper;
    private ControlTheme theme;

    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets the Theme")]
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
        this.menuPaintHelper = new MenuPaintHelper(value);
        this.DrawToBitmap();
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
        if (defaultTheme == null)
          return;
        this.Theme = defaultTheme;
        this.menuPaintHelper.Theme = defaultTheme;
      }
    }

    [Category("Appearance")]
    [Description("The ImageList used to draw menu images.")]
    [DefaultValue(null)]
    public ImageList ImageList
    {
      get
      {
        return this.imageList;
      }
      set
      {
        this.imageList = value;
      }
    }

    /// <summary>vMenuExtender constructor</summary>
    public vMenuExtender(IContainer container)
    {
      container.Add((IComponent) this);
      this.InitializeComponent();
      this.menuPaintHelper = new MenuPaintHelper(container);
    }

    /// <summary>vMenuExtender constructor</summary>
    public vMenuExtender(Menu mainMenu)
    {
      this.InitializeComponent();
      this.menuPaintHelper = new MenuPaintHelper();
      this.mainMenu = mainMenu;
    }

    private void CheckForm(MenuItem item)
    {
      if (item == null || this.mainMenu != null)
        return;
      this.mainMenu = (Menu) item.GetMainMenu();
      if (this.mainMenu == null)
        return;
      if (this.mainMenu is vMenuControl)
        this.form = (this.mainMenu as vMenuControl).GetForm();
      if (this.form == null)
        return;
      this.DrawToBitmap();
      this.form.Resize += new EventHandler(this.form_Resize);
    }

    private void CreatevMenuBrush()
    {
      this.BarBackgroundBrush = MenuWin32APIs.CreatePatternBrush(this.bitmapBackMenu.GetHbitmap());
      MenuWin32APIs.SetMenuInfo(this.mainMenu.Handle, ref new MENUINFO((Control) this.form)
      {
        fMask = 2,
        hbrBack = this.BarBackgroundBrush
      });
      MenuWin32APIs.SendMessage(this.form.Handle, 133U, 0U, 0);
    }

    public void DrawToBitmap()
    {
      if (this.form == null)
        return;
      if (this.bitmapBackMenu != null)
      {
        this.bitmapBackMenu.Dispose();
        this.bitmapBackMenu = (Bitmap) null;
      }
      if (this.BarBackgroundBrush != IntPtr.Zero)
      {
        MenuWin32APIs.DeleteObject(this.BarBackgroundBrush);
        this.BarBackgroundBrush = IntPtr.Zero;
      }
      this.bitmapBackMenu = new Bitmap(this.form.Width, SystemInformation.MenuHeight);
      using (Graphics g = Graphics.FromImage((Image) this.bitmapBackMenu))
        this.DrawBackBar(g, new Rectangle(0, 0, this.form.Width, SystemInformation.MenuHeight));
      this.CreatevMenuBrush();
    }

    private void DrawBackBar(Graphics g, Rectangle r)
    {
      this.menuPaintHelper.PaintBar(g, r);
    }

    private void DrawToolItem(MenuItem item, DrawItemEventArgs e, Image image, MenuItemImageSize imageSize)
    {
      this.menuPaintHelper.PaintItem(item, e, image, imageSize);
    }

    private void MeasureItem(MenuItem item, MeasureItemEventArgs e, MenuItemImageSize imageSize)
    {
      this.menuPaintHelper.MeasureItem(item, e, imageSize);
    }

    private void form_Resize(object sender, EventArgs e)
    {
      this.DrawToBitmap();
    }

    private void MeasureMenuItem(object sender, MeasureItemEventArgs e)
    {
      MenuItem menuItem = (MenuItem) sender;
      this.CheckForm(menuItem);
      this.MeasureItem(menuItem, e, this.GetImageSize());
    }

    private void DrawMenuItem(object sender, DrawItemEventArgs e)
    {
      if (sender == null)
        return;
      MenuItem menuItem = (MenuItem) sender;
      Image menuItemImage = this.GetMenuItemImage(menuItem);
      this.DrawToolItem(menuItem, e, menuItemImage, this.GetImageSize());
    }

    private Image GetMenuItemImage(MenuItem item)
    {
      if (this.imageList != null)
      {
        int menuItemImageIndex = this.GetMenuItemImageIndex((Component) item);
        if (menuItemImageIndex >= 0 && menuItemImageIndex < this.imageList.Images.Count)
          return this.imageList.Images[menuItemImageIndex];
      }
      return (Image) null;
    }

    private MenuItemImageSize GetImageSize()
    {
      if (this.imageList == null)
        return MenuItemImageSize.SIZE_NA;
      if (this.imageList.ImageSize.Height <= 16)
        return MenuItemImageSize.SIZE_SMALL;
      if (this.imageList.ImageSize.Height <= 24)
        return MenuItemImageSize.SIZE_MEDIUM;
      return this.imageList.ImageSize.Height <= 32 ? MenuItemImageSize.SIZE_LARGE : MenuItemImageSize.SIZE_VERYLARGE;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        MenuWin32APIs.DeleteObject(this.BarBackgroundBrush);
        if (this.components != null)
          this.components.Dispose();
      }
      base.Dispose(disposing);
    }

    /// <exclude />
    public void Refresh()
    {
      this.DrawToBitmap();
    }

    /// <exclude />
    public void Attach(MenuItem[] items)
    {
      foreach (MenuItem menuItem in items)
      {
        if (menuItem != null)
        {
          menuItem.OwnerDraw = true;
          menuItem.MeasureItem += new MeasureItemEventHandler(this.MeasureMenuItem);
          menuItem.DrawItem += new DrawItemEventHandler(this.DrawMenuItem);
        }
      }
    }

    /// <exclude />
    public void Attach(Menu.MenuItemCollection items)
    {
      foreach (MenuItem menuItem in items)
      {
        if (menuItem != null)
        {
          menuItem.OwnerDraw = true;
          menuItem.MeasureItem += new MeasureItemEventHandler(this.MeasureMenuItem);
          menuItem.DrawItem += new DrawItemEventHandler(this.DrawMenuItem);
        }
      }
    }

    private void InitializeComponent()
    {
      this.components = new Container();
    }

    /// <exclude />
    public bool CanExtend(object extendee)
    {
      return extendee is MenuItem;
    }

    /// <exclude />
    [Category("Appearance")]
    public int GetMenuItemImageIndex(Component component)
    {
      if (component != null && this.hashTable != null && this.hashTable.ContainsKey((object) component))
        return (int) this.hashTable[(object) component];
      return -1;
    }

    /// <exclude />
    public void SetImageIndex(Component component, int value)
    {
      if (component == null || this.hashTable == null)
        return;
      this.hashTable[(object) component] = (object) value;
      MenuItem menuItem = component as MenuItem;
      if (value == -2)
      {
        menuItem.OwnerDraw = false;
      }
      else
      {
        menuItem.OwnerDraw = true;
        menuItem.MeasureItem += new MeasureItemEventHandler(this.MeasureMenuItem);
        menuItem.DrawItem += new DrawItemEventHandler(this.DrawMenuItem);
      }
    }
  }
}
