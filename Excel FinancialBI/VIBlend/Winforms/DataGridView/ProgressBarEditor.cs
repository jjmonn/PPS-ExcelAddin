// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.ProgressBarEditor
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VIBlend.Utilities;
using VIBlend.WinForms.Controls;

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>
  /// Represents a ProgressBarEditor used in the GridView control
  /// </summary>
  [ToolboxItem(false)]
  public class ProgressBarEditor : IEditor
  {
    private vProgressBar progressBar = new vProgressBar();
    private vProgressBar tmpProgressBar = new vProgressBar();
    private EditorActivationFlags activationFlags = EditorActivationFlags.MOUSE_MOVE | EditorActivationFlags.PROGRAMMATIC;
    private EditorActivationFlags deactivationFlags = EditorActivationFlags.MOUSE_MOVE | EditorActivationFlags.KEY_PRESS | EditorActivationFlags.PROGRAMMATIC;
    private VIBLEND_THEME? viblendTheme = new VIBLEND_THEME?();
    private GridCell editCell;
    private object editorValue;
    private GridCell currentCell;

    /// <summary>Gets or sets the maximum.</summary>
    /// <value>The maximum.</value>
    public int Maximum
    {
      get
      {
        return this.progressBar.Maximum;
      }
      set
      {
        this.progressBar.Maximum = value;
      }
    }

    /// <summary>Gets or sets the minimum.</summary>
    /// <value>The minimum.</value>
    public int Minimum
    {
      get
      {
        return this.progressBar.Minimum;
      }
      set
      {
        this.progressBar.Minimum = value;
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
        return CellEditorType.ProgressBar;
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
    /// Gets a reference to the underlying control of the editor
    /// </summary>
    public Control Control
    {
      get
      {
        return (Control) this.progressBar;
      }
    }

    /// <summary>Get's or sets the current value of the editor</summary>
    public object EditorValue
    {
      get
      {
        if (this.currentCell != null)
          this.editorValue = (object) this.progressBar.Value;
        return this.editorValue;
      }
      set
      {
        if (value is bool && (this.editorValue != null && (bool) this.editorValue == (bool) value))
          return;
        this.editorValue = value;
        this.LoadProgressWithValue(this.progressBar, this.editorValue);
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
        this.tmpProgressBar.VIBlendTheme = value.Value;
        this.progressBar.VIBlendTheme = value.Value;
      }
    }

    internal void DrawControl(Graphics graphics, GridCell cell)
    {
      Rectangle bounds = this.Layout(cell);
      this.tmpProgressBar.UseThemeBackground = true;
      if (this.tmpProgressBar.VIBlendTheme != cell.CellsArea.GridControl.VIBlendTheme)
        this.tmpProgressBar.VIBlendTheme = cell.CellsArea.GridControl.VIBlendTheme;
      if (!this.LoadProgressWithValue(this.tmpProgressBar, cell.Value) || cell.RowItem.HeightWithChildren <= bounds.Height || cell.ColumnItem.WidthWithChildren < bounds.Width)
        return;
      if (this.VIBlendTheme.HasValue)
        this.tmpProgressBar.VIBlendTheme = this.VIBlendTheme.Value;
      this.tmpProgressBar.RenderProgressBar(graphics, bounds);
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
      this.progressBar.Bounds = rectangle;
      this.LoadProgressWithValue(this.tmpProgressBar, cell.Value);
    }

    private bool LoadProgressWithValue(vProgressBar control, object value)
    {
      if (value == null)
        value = (object) 0;
      control.Value = (int) value;
      return true;
    }

    private Rectangle Layout(GridCell cell)
    {
      return new Rectangle(cell.ColumnItem.DrawBounds.X + 1, cell.RowItem.DrawBounds.Y + 1, cell.Bounds.Width - 1, cell.Bounds.Height - 1);
    }

    /// <summary>Handles grid KeyDown events</summary>
    /// <param name="args">KeyDown event arguments</param>
    /// <param name="closeEditor">The grid cell where the event occured. This may be null if the event did not occur within a grid cell.</param>
    /// <param name="closeEditor">Output flag indicating whether to keep the editor open or close it instantly</param>
    public void OnGridKeyDown(KeyEventArgs args, GridCell cell, out bool closeEditor, out bool closeEditorAndSaveChanges)
    {
      closeEditorAndSaveChanges = false;
      closeEditor = args.KeyData == Keys.Escape;
      if (args.KeyData != Keys.Return)
        return;
      closeEditor = true;
      closeEditorAndSaveChanges = true;
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
      this.Layout(cell);
      this.progressBar.Visible = false;
      this.currentCell = cell;
      this.LoadProgressWithValue(this.progressBar, this.editorValue);
      this.progressBar.Width = 0;
      this.progressBar.Height = 0;
    }

    /// <summary>
    /// This method is called by the grid control when at the end of a cell edit operation
    /// </summary>
    public void OnCloseEditor()
    {
      if (this.currentCell != null)
        this.editorValue = (object) this.progressBar.Value;
      this.currentCell = (GridCell) null;
    }
  }
}
