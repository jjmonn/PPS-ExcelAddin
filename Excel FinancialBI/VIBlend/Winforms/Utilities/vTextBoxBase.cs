// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.vTextBoxBase
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VIBlend.Utilities
{
  /// <exclude />
  [ToolboxItem(false)]
  public class vTextBoxBase : TextBox
  {
    private bool drawDefaultText = true;
    private bool showDefaultText = true;
    private string defaultText = "Empty...";
    private Color defaultTextColor = Color.Gray;
    private List<string> autoCompleteValues = new List<string>();
    private const int WM_SETFOCUS = 7;
    private const int WM_KILLFOCUS = 8;
    private const int WM_ERASEBKGND = 14;
    private const int WM_PAINT = 15;
    private Font defaultTextFont;
    private bool update;
    private bool autoCompleteEnabled;

    /// <summary>
    /// Gets or sets a value indicating whether auto complete is enabled.
    /// </summary>
    [Description("Gets or sets a value indicating whether auto complete is enabled.")]
    [Category("Behavior")]
    [DefaultValue(false)]
    public bool AutoCompleteEnabled
    {
      get
      {
        return this.autoCompleteEnabled;
      }
      set
      {
        this.autoCompleteEnabled = value;
      }
    }

    /// <summary>Gets or sets the values.</summary>
    /// <value>The values.</value>
    [Category("Behavior")]
    [Description("Sets the list of values")]
    public List<string> AutoCompleteValues
    {
      get
      {
        return this.autoCompleteValues;
      }
      set
      {
        this.autoCompleteValues = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to draw default text
    /// </summary>
    [Category("Appearance")]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DefaultValue(true)]
    [Browsable(true)]
    [Description("Gets or sets a value indicating whether to draw default text")]
    public bool ShowDefaultText
    {
      get
      {
        return this.showDefaultText;
      }
      set
      {
        this.drawDefaultText = value;
        this.showDefaultText = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the default text.</summary>
    /// <value>The default text.</value>
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Description("Gets or sets the default text.")]
    [Browsable(true)]
    [Category("Appearance")]
    public string DefaultText
    {
      get
      {
        return this.defaultText;
      }
      set
      {
        this.defaultText = value.Trim();
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the default color of the text.</summary>
    /// <value>The default color of the text.</value>
    [Browsable(true)]
    [Description("Gets or sets the default color of the text.")]
    [DefaultValue(typeof (Color), "Gray")]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Category("Appearance")]
    public Color DefaultTextColor
    {
      get
      {
        return this.defaultTextColor;
      }
      set
      {
        this.defaultTextColor = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the default text font.</summary>
    /// <value>The default text font.</value>
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(true)]
    [Category("Appearance")]
    [Description("Gets or sets the default text font.")]
    public Font DefaultTextFont
    {
      get
      {
        return this.defaultTextFont;
      }
      set
      {
        this.defaultTextFont = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.Utilities.vTextBoxBase" /> class.
    /// </summary>
    public vTextBoxBase()
    {
      this.DefaultTextFont = this.Font;
      this.TextChanged += new EventHandler(this.vTextBoxBase_TextChanged);
    }

    /// <summary>Gets the index in auto complete values.</summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public int GetIndexInAutoCompleteValues(string value)
    {
      IEnumerator enumerator = (IEnumerator) this.autoCompleteValues.GetEnumerator();
      string upper = value.ToUpper();
      int num = -1;
      while (enumerator.MoveNext())
      {
        ++num;
        if (((string) enumerator.Current).ToUpper().Equals(upper.ToUpper()))
          return num;
      }
      return -1;
    }

    /// <summary>
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnTextChanged(EventArgs e)
    {
      if (!this.AutoCompleteEnabled)
      {
        base.OnTextChanged(e);
      }
      else
      {
        string upper = this.Text.ToUpper();
        if (upper == "" || this.update)
        {
          base.OnTextChanged(e);
        }
        else
        {
          IEnumerator enumerator = (IEnumerator) this.AutoCompleteValues.GetEnumerator();
          bool flag = false;
          int num = -1;
          while (enumerator.MoveNext() && !flag)
          {
            ++num;
            string str = (string) enumerator.Current;
            if (str != null && str.ToUpper().IndexOf(upper) == 0)
            {
              this.update = true;
              this.Text = str;
              this.SelectionStart = upper.Length;
              this.SelectionLength = str.Length - upper.Length;
              flag = true;
              this.update = false;
            }
          }
          base.OnTextChanged(e);
        }
      }
    }

    private void ResetDefaultTextFont()
    {
      this.DefaultTextFont = Control.DefaultFont;
    }

    private bool ShouldSerializeDefaultTextFont()
    {
      return this.DefaultTextFont != Control.DefaultFont;
    }

    /// <summary>Redraw the control when the text alignment changes</summary>
    /// <param name="e"></param>
    protected override void OnTextAlignChanged(EventArgs e)
    {
      base.OnTextAlignChanged(e);
      this.Invalidate();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      if (!this.drawDefaultText || this.Text.Length != 0)
        return;
      this.DrawDefaultText(e.Graphics);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
      if (!this.AutoCompleteEnabled || e.KeyValue != 8 && e.KeyValue != 46)
        return;
      this.update = true;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
    protected override void OnKeyUp(KeyEventArgs e)
    {
      base.OnKeyUp(e);
      if (!this.AutoCompleteEnabled)
        return;
      if (e.KeyValue == 8 || e.KeyValue == 46)
        this.update = false;
      if (e.KeyValue != 13)
        return;
      this.SelectionLength = 0;
    }

    /// <summary>
    /// </summary>
    /// <param name="m">A Windows Message object.</param>
    protected override void WndProc(ref Message m)
    {
      switch (m.Msg)
      {
        case 7:
          if (!this.ReadOnly)
          {
            this.drawDefaultText = false;
            break;
          }
          break;
        case 8:
          this.drawDefaultText = true;
          this.Invalidate();
          break;
      }
      base.WndProc(ref m);
      if (m.Msg != 15 || !this.drawDefaultText || (this.Text.Length != 0 || this.GetStyle(ControlStyles.UserPaint)))
        return;
      this.DrawDefaultText();
    }

    private void vTextBoxBase_TextChanged(object sender, EventArgs e)
    {
      if (!this.ShowDefaultText)
        return;
      if (this.Text.Length == 0)
        this.drawDefaultText = true;
      if (!this.drawDefaultText || this.Text.Length != 0)
        return;
      this.DrawDefaultText();
    }

    /// <summary>Draws the default text.</summary>
    protected virtual void DrawDefaultText()
    {
      if (!this.ShowDefaultText)
        return;
      using (Graphics graphics = this.CreateGraphics())
        this.DrawDefaultText(graphics);
    }

    /// <summary>Draws the default text.</summary>
    /// <param name="g">The g.</param>
    protected virtual void DrawDefaultText(Graphics g)
    {
      if (!this.ShowDefaultText)
        return;
      TextFormatFlags flags = TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding;
      Rectangle clientRectangle = this.ClientRectangle;
      switch (this.TextAlign)
      {
        case HorizontalAlignment.Left:
          flags = flags;
          clientRectangle.Offset(1, 1);
          break;
        case HorizontalAlignment.Right:
          flags |= TextFormatFlags.Right;
          clientRectangle.Offset(0, 1);
          break;
        case HorizontalAlignment.Center:
          flags |= TextFormatFlags.HorizontalCenter;
          clientRectangle.Offset(0, 1);
          break;
      }
      TextRenderer.DrawText((IDeviceContext) g, this.defaultText, this.defaultTextFont, clientRectangle, this.defaultTextColor, this.BackColor, flags);
    }
  }
}
