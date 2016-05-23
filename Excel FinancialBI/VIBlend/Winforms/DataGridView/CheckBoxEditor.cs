// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.CheckBoxEditor
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using VIBlend.Utilities;
using VIBlend.WinForms.Controls;

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>
  /// Represents a CheckBoxEditor used in the GridView control
  /// </summary>
  [ToolboxItem(false)]
  public class CheckBoxEditor : IEditor
  {
    private vCheckBox checkBox = new vCheckBox();
    private vCheckBox tempChkbox = new vCheckBox();
    private EditorActivationFlags activationFlags = EditorActivationFlags.MOUSE_MOVE | EditorActivationFlags.PROGRAMMATIC;
    private EditorActivationFlags deactivationFlags = EditorActivationFlags.MOUSE_MOVE | EditorActivationFlags.KEY_PRESS | EditorActivationFlags.PROGRAMMATIC;
    private bool handleSpaceKey = true;
    private VIBLEND_THEME? viblendTheme = new VIBLEND_THEME?();
    private bool currentCheckedValue;
    private GridCell editCell;
    private object editorValue;
    private GridCell currentCell;

    /// <summary>
    /// Gets or sets a value indicating whether the checkBox supports indeterminate state.
    /// </summary>
    public bool ThreeState
    {
      get
      {
        return this.checkBox.ThreeState;
      }
      set
      {
        this.checkBox.ThreeState = value;
      }
    }

    /// <summary>Gets the edit cell.</summary>
    /// <value>The edit cell.</value>
    public GridCell EditCell
    {
      get
      {
        return this.editCell;
      }
    }

    /// <summary>Gets the type of the grid cell editor</summary>
    public CellEditorType EditorType
    {
      get
      {
        return CellEditorType.CheckBox;
      }
    }

    /// <summary>Gets or sets the editor activation flags</summary>
    public EditorActivationFlags ActivationFlags
    {
      get
      {
        return this.activationFlags;
      }
      set
      {
        this.activationFlags = value;
        this.activationFlags |= EditorActivationFlags.PROGRAMMATIC;
      }
    }

    /// <summary>Gets or sets the editor activation flags</summary>
    public EditorActivationFlags DeActivationFlags
    {
      get
      {
        return this.deactivationFlags;
      }
      set
      {
        this.deactivationFlags = value;
        this.deactivationFlags |= EditorActivationFlags.PROGRAMMATIC;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to handle the space key.
    /// </summary>
    public bool HandleGridSpaceKey
    {
      get
      {
        return this.handleSpaceKey;
      }
      set
      {
        this.handleSpaceKey = value;
      }
    }

    /// <summary>
    /// Gets a reference to the underlying control of the editor
    /// </summary>
    public Control Control
    {
      get
      {
        return (Control) this.checkBox;
      }
    }

    /// <summary>Get's or sets the current value of the editor</summary>
    public object EditorValue
    {
      get
      {
        if (this.currentCell != null)
          this.editorValue = this.checkBox.ThreeState ? (object) this.checkBox.CheckState : (object) this.checkBox.Checked;
        return this.editorValue;
      }
      set
      {
        if (value is bool && (this.editorValue != null && (bool) this.editorValue == (bool) value))
          return;
        this.editorValue = value;
        this.LoadCheckBoxWithValue(this.checkBox, this.editorValue);
      }
    }

    /// <summary>Gets or sets the VIblend theme.</summary>
    /// <value>The VIblend theme.</value>
    public VIBLEND_THEME? VIBlendTheme
    {
      get
      {
        return this.viblendTheme;
      }
      set
      {
        this.viblendTheme = value;
        if (!value.HasValue)
          return;
        this.tempChkbox.VIBlendTheme = value.Value;
        this.checkBox.VIBlendTheme = value.Value;
      }
    }

    /// <summary>Occurs when the editor's value is changed.</summary>
    [Description("Occurs when the editor's value is changed.")]
    public event EventHandler CheckedChanged;

    /// <summary>Constructor</summary>
    public CheckBoxEditor()
    {
      this.checkBox.MouseDown += new MouseEventHandler(this.checkBox_MouseDown);
      this.checkBox.MouseUp += new MouseEventHandler(this.checkBox_MouseUp);
    }

    /// <summary>Called when the check-state is changed.</summary>
    protected virtual void OnCheckedChanged()
    {
      if (this.CheckedChanged == null)
        return;
      this.CheckedChanged((object) this, EventArgs.Empty);
    }

    private void checkBox_MouseUp(object sender, MouseEventArgs e)
    {
      if (this.checkBox.Checked == this.currentCheckedValue)
        return;
      this.OnCheckedChanged();
    }

    private void checkBox_MouseDown(object sender, MouseEventArgs e)
    {
      this.currentCheckedValue = this.checkBox.Checked;
    }

    internal void DrawControl(Graphics graphics, GridCell cell)
    {
      Rectangle rectangle = this.Layout(cell);
      this.tempChkbox.UseThemeCheckMarkColors = true;
      if (this.tempChkbox.VIBlendTheme != cell.CellsArea.GridControl.VIBlendTheme)
        this.tempChkbox.VIBlendTheme = cell.CellsArea.GridControl.VIBlendTheme;
      this.tempChkbox.Text = "";
      if (!this.LoadCheckBoxWithValue(this.tempChkbox, cell.Value) || cell.RowItem.HeightWithChildren <= rectangle.Height || cell.ColumnItem.WidthWithChildren < rectangle.Width)
        return;
      if (this.VIBlendTheme.HasValue)
        this.tempChkbox.VIBlendTheme = this.VIBlendTheme.Value;
      this.tempChkbox.Bounds = rectangle;
      Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height);
      this.tempChkbox.DrawToBitmap(bitmap, new Rectangle(0, 0, rectangle.Width, rectangle.Height));
      graphics.DrawImage((Image) bitmap, rectangle.X, rectangle.Y);
    }

    /// <summary>
    /// This methods is called by the grid control when the editor must update its layout
    /// </summary>
    /// <param name="cell">Reference to the GridCell being edited</param>
    public virtual void LayoutEditor(GridCell cell)
    {
      Rectangle rectangle = this.Layout(cell);
      this.editCell = cell;
      if (cell.RowItem.HeightWithChildren <= rectangle.Height || cell.ColumnItem.WidthWithChildren < rectangle.Width)
        return;
      this.checkBox.Bounds = rectangle;
      this.LoadCheckBoxWithValue(this.tempChkbox, cell.Value);
    }

    private bool LoadCheckBoxWithValue(vCheckBox control, object value)
    {
      if (value == null)
        value = (object) false;
      if (value is bool)
        control.Checked = (bool) value;
      else if (this.editorValue is CheckState)
        control.CheckState = (CheckState) value;
      else if (value is string)
        control.Checked = ((string) value).ToLower() == true.ToString().ToLower();
      else
        control.Checked = false;
      return true;
    }

    private Rectangle Layout(GridCell cell)
    {
      Rectangle rectangle1 = new Rectangle(cell.ColumnItem.DrawBounds.X + 1, cell.RowItem.DrawBounds.Y + 1, cell.Bounds.Width - 1, cell.Bounds.Height - 1);
      Rectangle rectangle2 = new Rectangle(0, 0, 13, 13);
      CellsArea cellsArea = cell.CellsArea;
      if (cell.TextAlignment == ContentAlignment.BottomLeft || cell.TextAlignment == ContentAlignment.MiddleLeft || cell.TextAlignment == ContentAlignment.TopLeft)
        rectangle2.X = rectangle1.X + 2;
      if (cell.TextAlignment == ContentAlignment.BottomRight || cell.TextAlignment == ContentAlignment.MiddleRight || cell.TextAlignment == ContentAlignment.TopRight)
        rectangle2.X = rectangle1.Right - rectangle2.Width - 2;
      if (cell.TextAlignment == ContentAlignment.BottomCenter || cell.TextAlignment == ContentAlignment.MiddleCenter || cell.TextAlignment == ContentAlignment.TopCenter)
        rectangle2.X = rectangle1.X + (rectangle1.Width - rectangle2.Width) / 2;
      if (cell.TextAlignment == ContentAlignment.TopLeft || cell.TextAlignment == ContentAlignment.TopRight || cell.TextAlignment == ContentAlignment.TopCenter)
        rectangle2.Y = rectangle1.Y + 2;
      if (cell.TextAlignment == ContentAlignment.BottomLeft || cell.TextAlignment == ContentAlignment.BottomRight || cell.TextAlignment == ContentAlignment.BottomCenter)
        rectangle2.Y = rectangle1.Bottom - rectangle2.Height - 2;
      if (cell.TextAlignment == ContentAlignment.MiddleLeft || cell.TextAlignment == ContentAlignment.MiddleRight || cell.TextAlignment == ContentAlignment.MiddleCenter)
        rectangle2.Y = rectangle1.Y + (rectangle1.Height - rectangle2.Height) / 2;
      return rectangle2;
    }

    /// <summary>Handles grid KeyDown events</summary>
    /// <param name="args">KeyDown event arguments</param>
    /// <param name="closeEditor">The grid cell where the event occured. This may be null if the event did not occur within a grid cell.</param>
    /// <param name="closeEditor">Output flag indicating whether to keep the editor open or close it instantly</param>
    public void OnGridKeyDown(KeyEventArgs args, GridCell cell, out bool closeEditor, out bool closeEditorAndSaveChanges)
    {
      closeEditorAndSaveChanges = false;
      closeEditor = args.KeyData == Keys.Escape;
      if (args.KeyData == Keys.Return)
      {
        closeEditor = true;
        closeEditorAndSaveChanges = true;
      }
      if (!this.HandleGridSpaceKey || args.KeyData != Keys.Space || this.checkBox.Focused)
        return;
      closeEditorAndSaveChanges = true;
      this.LoadCheckBoxWithValue(this.checkBox, cell.Value);
      this.checkBox.Checked = !this.checkBox.Checked;
      this.EditorValue = !this.ThreeState ? (object) this.checkBox.Checked : (object) this.checkBox.CheckState;
      try
      {
        cell.Value = this.EditorValue;
        if (cell.RowItem == null || cell.RowItem.DataGridView == null)
          return;
        cell.RowItem.DataGridView.Invalidate();
      }
      catch (Exception ex)
      {
        Trace.WriteLine(ex.Message);
      }
    }

    /// <summary>Handles the painting of the editor control</summary>
    /// <param name="graphics">Reference to the GDI+ surface</param>
    /// <param name="gridCell">Reference to the grid cell being rendered</param>
    /// <param name="isDrawHandled">Output flag indicating whether the method handled the drawing or not</param>
    public void DrawEditorControl(Graphics graphics, GridCell gridCell, out bool isDrawHandled)
    {
      this.Layout(gridCell);
      this.DrawControl(graphics, gridCell);
      isDrawHandled = true;
    }

    /// <summary>
    /// This method is called by the grid control when the editor is being activated
    /// </summary>
    public void OnOpenEditor(GridCell cell)
    {
      this.checkBox.Bounds = this.Layout(cell);
      this.currentCell = cell;
      this.LoadCheckBoxWithValue(this.checkBox, this.editorValue);
      this.checkBox.UseThemeCheckMarkColors = true;
      this.checkBox.VIBlendTheme = !this.VIBlendTheme.HasValue ? cell.CellsArea.GridControl.VIBlendTheme : this.VIBlendTheme.Value;
      this.checkBox.Refresh();
    }

    /// <summary>
    /// This method is called by the grid control when at the end of a cell edit operation
    /// </summary>
    public void OnCloseEditor()
    {
      if (this.currentCell != null)
        this.editorValue = this.checkBox.ThreeState ? (object) this.checkBox.CheckState : (object) this.checkBox.Checked;
      this.currentCell = (GridCell) null;
    }
  }
}
