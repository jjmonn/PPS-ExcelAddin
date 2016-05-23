// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.TabContext
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System.ComponentModel;
using System.Drawing;

namespace VIBlend.WinForms.Controls
{
  [ToolboxItem(false)]
  [DesignTimeVisible(false)]
  public class TabContext : Component, INotifyPropertyChanged
  {
    private Color tabColor = Color.Blue;
    private Color foreColor = Color.WhiteSmoke;
    private string text = "TabContext";
    private bool visible = true;

    /// <summary>Gets or sets the foreColor of the tab.</summary>
    /// <value>The foreColor of the tab.</value>
    [Description("Gets or sets the foreColor.")]
    [Category("Appearance")]
    public Color ForeColor
    {
      get
      {
        return this.foreColor;
      }
      set
      {
        if (!(this.foreColor != value))
          return;
        this.foreColor = value;
        this.OnPropertyChanged("ForeColor");
      }
    }

    /// <summary>Gets or sets the color of the tab.</summary>
    /// <value>The color of the tab.</value>
    [Category("Appearance")]
    [Description("Gets or sets the tab color.")]
    public Color TabColor
    {
      get
      {
        return this.tabColor;
      }
      set
      {
        if (!(this.tabColor != value))
          return;
        this.tabColor = value;
        this.OnPropertyChanged("TabColor");
      }
    }

    /// <summary>Gets or sets the name.</summary>
    /// <value>The name.</value>
    [Description("Gets or sets the name.")]
    [DefaultValue("TabContext")]
    [Category("Appearance")]
    public string Text
    {
      get
      {
        return this.text;
      }
      set
      {
        if (!(this.text != value))
          return;
        this.text = value;
        this.OnPropertyChanged("Text");
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:VIBlend.WinForms.Controls.TabContext" /> is visible.
    /// </summary>
    /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
    [Description("Gets or sets a value indicating whether the context is visible")]
    [Category("Appearance")]
    [DefaultValue(true)]
    public bool Visible
    {
      get
      {
        return this.visible;
      }
      set
      {
        if (value == this.visible)
          return;
        this.visible = value;
        this.OnPropertyChanged("Visible");
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.TabContext" /> class.
    /// </summary>
    public TabContext()
    {
    }

    public TabContext(Color color, string name, bool visible)
    {
      this.tabColor = color;
      this.text = name;
      this.visible = visible;
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
