// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vStripsRenderer
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
  /// <summary>Represents a vStripsRenderer component</summary>
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (vStripsRenderer), "ControlIcons.vRendererComponent.ico")]
  [Description("Represents a vStripsRenderer component.")]
  public class vStripsRenderer : Component
  {
    private vColorTable table = new vColorTable();
    private BackgroundElement element = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) null);
    private ToolStrip renderedControl;
    private vToolStripProfessionalRenderer renderer;
    private MenuStrip renderedMenu;
    private StatusStrip renderedStrip;
    private ContextMenuStrip contextMenuStrip;
    private ControlTheme theme;
    private VIBLEND_THEME defaultTheme;

    /// <summary>Gets or sets the renderer.</summary>
    [Browsable(false)]
    public vToolStripProfessionalRenderer Renderer
    {
      get
      {
        return this.renderer;
      }
      set
      {
        this.renderer = value;
      }
    }

    /// <summary>Gets or sets the rendered context menu strip.</summary>
    [DefaultValue(null)]
    public ContextMenuStrip RenderedContextMenuStrip
    {
      get
      {
        return this.contextMenuStrip;
      }
      set
      {
        this.contextMenuStrip = value;
        if (value == null)
          return;
        this.contextMenuStrip.Renderer = (ToolStripRenderer) this.Renderer;
      }
    }

    /// <summary>Gets or sets the rendered tool strip.</summary>
    [DefaultValue(null)]
    public ToolStrip RenderedToolStrip
    {
      get
      {
        return this.renderedControl;
      }
      set
      {
        this.renderedControl = value;
        if (value == null)
          return;
        this.renderedControl.Renderer = (ToolStripRenderer) this.Renderer;
      }
    }

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
    [Description("Gets or sets the theme")]
    [Category("Appearance")]
    [Browsable(false)]
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
        this.Renderer = new vToolStripProfessionalRenderer(this.table);
        ControlTheme copy = value.CreateCopy();
        this.Renderer.Theme = copy;
        this.element.LoadTheme(copy);
        FillStyle fillStyle1 = this.theme.QueryFillStyleSetter("MenuNormal");
        if (fillStyle1 != null)
          this.element.Theme.StyleNormal.FillStyle = fillStyle1;
        FillStyle fillStyle2 = this.theme.QueryFillStyleSetter("MenuHighlight");
        if (fillStyle2 != null)
          this.element.Theme.StyleHighlight.FillStyle = fillStyle2;
        FillStyle fillStyle3 = this.theme.QueryFillStyleSetter("MenuPressed");
        if (fillStyle3 != null)
          this.element.Theme.StylePressed.FillStyle = fillStyle3;
        FillStyle fillStyle4 = this.theme.QueryFillStyleSetter("MenuImageArea");
        Color color1 = Color.Empty;
        Color color2 = this.theme.QueryColorSetter("MainMenuBorder");
        vColorTable.buttonBorder = this.element.BorderColor;
        vColorTable.buttonPressedColor1 = this.element.Theme.StylePressed.FillStyle.Colors[0];
        vColorTable.buttonPressedColor2 = this.element.Theme.StylePressed.FillStyle.Colors[1];
        if (this.element.Theme.StylePressed.FillStyle.ColorsNumber > 2)
          vColorTable.buttonPressedColor3 = this.element.Theme.StylePressed.FillStyle.Colors[2];
        vColorTable.buttonSelectedColor1 = this.element.Theme.StyleHighlight.FillStyle.Colors[0];
        vColorTable.buttonSelectedColor2 = this.element.Theme.StyleHighlight.FillStyle.Colors[1];
        if (this.element.Theme.StyleHighlight.FillStyle.ColorsNumber > 2)
          vColorTable.buttonSelectedColor3 = this.element.Theme.StyleHighlight.FillStyle.Colors[2];
        vColorTable.checkBoxBackGround = this.element.Theme.StyleNormal.FillStyle.Colors[0];
        vColorTable.gripColor1 = this.element.Theme.StyleNormal.FillStyle.Colors[0];
        vColorTable.gripColor2 = this.element.Theme.StyleNormal.FillStyle.Colors[1];
        vColorTable.imageAreaColor1 = this.element.Theme.StyleNormal.FillStyle.Colors[0];
        vColorTable.imageAreaColor2 = this.element.Theme.StyleNormal.FillStyle.Colors[1];
        if (this.element.Theme.StyleNormal.FillStyle.ColorsNumber > 2)
          vColorTable.imageAreaColor3 = this.element.Theme.StyleNormal.FillStyle.Colors[2];
        if (fillStyle4 != null)
        {
          vColorTable.imageAreaColor1 = fillStyle4.Colors[0];
          vColorTable.imageAreaColor2 = fillStyle4.Colors[1];
          if (fillStyle4.ColorsNumber > 2)
            vColorTable.imageAreaColor3 = fillStyle4.Colors[2];
        }
        vColorTable.menuBorder = this.element.Theme.StyleNormal.BorderColor;
        if (color2 != Color.Empty)
          vColorTable.menuBorder = color2;
        vColorTable.itemSelectedColor1 = this.element.Theme.StyleHighlight.FillStyle.Colors[0];
        vColorTable.itemSelectedColor2 = this.element.Theme.StyleHighlight.FillStyle.Colors[1];
        vColorTable.menuToolBackColor1 = this.element.Theme.StyleNormal.FillStyle.Colors[0];
        vColorTable.menuToolBackColor2 = this.element.Theme.StyleNormal.FillStyle.Colors[1];
        vColorTable.overflowColor1 = this.element.Theme.StyleNormal.FillStyle.Colors[0];
        vColorTable.overflowColor2 = this.element.Theme.StyleNormal.FillStyle.Colors[1];
        vColorTable.overflowColor3 = this.element.Theme.StyleNormal.FillStyle.Colors[1];
        vColorTable.separatorColor1 = this.element.Theme.StyleNormal.BorderColor;
        vColorTable.separatorColor2 = ControlPaint.Dark(this.element.Theme.StyleNormal.BorderColor);
        vColorTable.statusStripBackColor1 = this.element.Theme.StyleNormal.FillStyle.Colors[0];
        vColorTable.statusStripBackColor2 = this.element.Theme.StyleNormal.FillStyle.Colors[1];
        vColorTable.toolStripBackColor1 = this.element.Theme.StyleNormal.FillStyle.Colors[0];
        vColorTable.toolStripBackColor2 = this.element.Theme.StyleNormal.FillStyle.Colors[1];
        if (this.element.Theme.StyleNormal.FillStyle.ColorsNumber > 2)
          vColorTable.toolStripBackColor3 = this.element.Theme.StyleNormal.FillStyle.Colors[2];
        FillStyle fillStyle5 = this.theme.QueryFillStyleSetter("MainMenuBackground");
        if (fillStyle5 != null)
        {
          vColorTable.toolStripBackColor1 = fillStyle5.Colors[0];
          vColorTable.toolStripBackColor2 = fillStyle5.Colors[1];
          vColorTable.toolStripBackColor3 = fillStyle5.Colors[2];
          vColorTable.menuToolBackColor1 = fillStyle5.Colors[0];
          vColorTable.menuToolBackColor2 = fillStyle5.Colors[1];
        }
        vColorTable.toolStripBorderColor = this.element.Theme.StyleNormal.BorderColor;
        vColorTable.toolStripInnerColor = this.element.Theme.StyleNormal.FillStyle.Colors[0];
        vColorTable.contextMenuBackGround = this.element.Theme.StylePressed.FillStyle.Colors[0];
        if (this.RenderedMenuStrip != null)
          this.RenderedMenuStrip.Renderer = (ToolStripRenderer) this.Renderer;
        if (this.RenderedToolStrip != null)
          this.RenderedToolStrip.Renderer = (ToolStripRenderer) this.Renderer;
        if (this.RenderedStatusStrip != null)
          this.RenderedStatusStrip.Renderer = (ToolStripRenderer) this.Renderer;
        if (this.RenderedContextMenuStrip == null)
          return;
        this.RenderedContextMenuStrip.Renderer = (ToolStripRenderer) this.Renderer;
      }
    }

    /// <summary>Gets or sets the rendered menu strip.</summary>
    [DefaultValue(null)]
    public MenuStrip RenderedMenuStrip
    {
      get
      {
        return this.renderedMenu;
      }
      set
      {
        this.renderedMenu = value;
        if (value == null)
          return;
        this.renderedMenu.Renderer = (ToolStripRenderer) this.Renderer;
      }
    }

    /// <summary>Gets or sets the rendered status strip.</summary>
    [DefaultValue(null)]
    public StatusStrip RenderedStatusStrip
    {
      get
      {
        return this.renderedStrip;
      }
      set
      {
        this.renderedStrip = value;
        this.renderedStrip.Renderer = (ToolStripRenderer) this.Renderer;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:vRendererComponent" /> class.
    /// </summary>
    public vStripsRenderer()
    {
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
      this.renderer = new vToolStripProfessionalRenderer(this.table);
    }

    public vStripsRenderer(IContainer container)
      : this()
    {
      if (container == null)
        throw new ArgumentNullException("container");
      container.Add((IComponent) this);
    }
  }
}
