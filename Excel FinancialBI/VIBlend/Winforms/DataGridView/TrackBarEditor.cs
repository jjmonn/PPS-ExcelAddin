// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.TrackBarEditor
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
  /// Represents a vTrackBarEditor used in the GridView control
  /// </summary>
  [ToolboxItem(false)]
  public class TrackBarEditor : IEditor
  {
    private EditorActivationFlags activationFlags = EditorActivationFlags.MOUSE_MOVE | EditorActivationFlags.PROGRAMMATIC;
    private EditorActivationFlags deactivationFlags = EditorActivationFlags.MOUSE_MOVE | EditorActivationFlags.PROGRAMMATIC;
    private vTrackBar trackBar = new vTrackBar();
    private vTrackBar tempTrackBar = new vTrackBar();
    private Rectangle bounds;
    private object editorValue;
    private GridCell currentCell;

    /// <summary>Gets the type of the grid cell editor</summary>
    public CellEditorType EditorType
    {
      get
      {
        return CellEditorType.TrackBar;
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
        return (Control) this.trackBar;
      }
    }

    /// <summary>Get's or sets the current value of the editor</summary>
    public object EditorValue
    {
      get
      {
        this.editorValue = (object) this.trackBar.Value;
        return this.editorValue;
      }
      set
      {
        this.editorValue = value;
        this.LoadSliderWithValue(this.trackBar, this.editorValue);
      }
    }

    private Rectangle Layout(GridCell cell)
    {
      Rectangle rectangle1 = new Rectangle(cell.ColumnItem.DrawBounds.X + 1, cell.RowItem.DrawBounds.Y + 1, cell.Bounds.Width - 1, cell.Bounds.Height - 1);
      Rectangle rectangle2 = new Rectangle(0, 0, cell.Bounds.Width - 4, cell.Bounds.Height - 4);
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
    /// <param name="cell">The grid cell where the event occured. This may be null if the event did not occur within a grid cell.</param>
    /// <param name="closeEditor">Output flag indicating whether to keep the editor open or close it instantly</param>
    public void OnGridKeyDown(KeyEventArgs args, GridCell cell, out bool closeEditor, out bool closeEditorAndSaveChanges)
    {
      closeEditorAndSaveChanges = false;
      if (args.KeyData == Keys.Return)
      {
        closeEditor = true;
        closeEditorAndSaveChanges = true;
      }
      if (args.KeyData == Keys.Escape)
        closeEditor = true;
      else
        closeEditor = false;
    }

    /// <summary>
    /// This methods is called by the grid control when the editor must update its layout
    /// </summary>
    /// <param name="cell">Reference to the GridCell being edited</param>
    public virtual void LayoutEditor(GridCell cell)
    {
      this.bounds = this.trackBar.Bounds = this.Layout(cell);
    }

    /// <summary>Handles the painting of the editor control</summary>
    /// <param name="graphics">Reference to the GDI+ surface</param>
    /// <param name="gridCell">Reference to the grid cell being rendered</param>
    /// <param name="isDrawHandled">Output flag indicating whether the method handled the drawing or not</param>
    public void DrawEditorControl(Graphics graphics, GridCell gridCell, out bool isDrawHandled)
    {
      Rectangle rectangle = this.Layout(gridCell);
      this.tempTrackBar.Minimum = this.trackBar.Minimum;
      this.tempTrackBar.Maximum = this.trackBar.Maximum;
      this.tempTrackBar.Size = this.trackBar.Size;
      this.tempTrackBar.ThumbSize = this.trackBar.ThumbSize;
      this.tempTrackBar.RoundedCornersRadius = this.trackBar.RoundedCornersRadius;
      this.tempTrackBar.RoundedCornersMask = this.trackBar.RoundedCornersMask;
      this.tempTrackBar.RoundedCornersRadiusThumb = this.trackBar.RoundedCornersRadiusThumb;
      this.tempTrackBar.RoundedCornersMaskThumb = this.trackBar.RoundedCornersMaskThumb;
      this.LoadSliderWithValue(this.tempTrackBar, gridCell.Value);
      this.tempTrackBar.Bounds = rectangle;
      Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height);
      if (this.tempTrackBar.VIBlendTheme != gridCell.CellsArea.GridControl.VIBlendTheme)
        this.tempTrackBar.VIBlendTheme = gridCell.CellsArea.GridControl.VIBlendTheme;
      GridCellStyle gridCellStyle = gridCell.RowItem.Hierarchy.GridControl.Theme.GridCellStyle;
      if (gridCell.DrawStyle != null)
        gridCellStyle = gridCell.DrawStyle;
      this.tempTrackBar.BackColor = gridCellStyle.FillStyle.Colors[0];
      this.tempTrackBar.AllowAnimations = false;
      this.tempTrackBar.DrawToBitmap(bitmap, new Rectangle(0, 0, rectangle.Width, rectangle.Height));
      graphics.DrawImage((Image) bitmap, rectangle.X, rectangle.Y);
      isDrawHandled = true;
    }

    private void LoadSliderWithValue(vTrackBar ctrl, object value)
    {
      try
      {
        if (value is string)
        {
          int result = 0;
          if (int.TryParse((string) value, out result))
            ctrl.Value = result;
        }
        else if (value is int)
          ctrl.Value = (int) value;
      }
      catch (Exception ex)
      {
        Trace.WriteLine(ex.Message);
        ctrl.Value = 0;
      }
      ctrl.Refresh();
    }

    /// <summary>
    /// This method is called by the grid control when the editor is being activated
    /// </summary>
    public virtual void OnOpenEditor(GridCell cell)
    {
      if (cell == null)
        return;
      this.trackBar.AllowAnimations = false;
      this.trackBar.Bounds = this.bounds = this.Layout(cell);
      this.currentCell = cell;
      this.LoadSliderWithValue(this.trackBar, cell.Value);
    }

    /// <summary>
    /// This method is called by the grid control when at the end of a cell edit operation
    /// </summary>
    public virtual void OnCloseEditor()
    {
      this.editorValue = (object) this.trackBar.Value;
      this.currentCell = (GridCell) null;
    }
  }
}
