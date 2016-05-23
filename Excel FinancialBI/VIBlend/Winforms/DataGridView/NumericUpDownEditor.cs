// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.NumericUpDownEditor
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using VIBlend.WinForms.Controls;

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>
  /// Represents a NumericUpDownEditor used in the GridView control
  /// </summary>
  [ToolboxItem(false)]
  public class NumericUpDownEditor : vNumericUpDown, IEditor
  {
    private EditorActivationFlags activationFlags = EditorActivationFlags.PROGRAMMATIC | EditorActivationFlags.MOUSE_CLICK_SELECTED_CELL | EditorActivationFlags.KEY_PRESS_ENTER;
    private EditorActivationFlags deactivationFlags = EditorActivationFlags.MOUSE_CLICK | EditorActivationFlags.MOUSE_DBLCLICK | EditorActivationFlags.PROGRAMMATIC | EditorActivationFlags.KEY_PRESS_ESC;
    private object editorValue;
    private GridCell currentCell;

    /// <summary>Gets the type of the grid cell editor</summary>
    public CellEditorType EditorType
    {
      get
      {
        return CellEditorType.NumericUpDown;
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
        return (Control) this;
      }
    }

    /// <summary>Get's or sets the current value of the editor</summary>
    public object EditorValue
    {
      get
      {
        this.editorValue = (object) this.Value;
        return this.editorValue;
      }
      set
      {
        this.editorValue = value;
        this.LoadDateTimePickerWithEditorValue();
      }
    }

    /// <summary>Handles grid KeyDown events</summary>
    /// <param name="args">KeyDown event arguments</param>
    /// <param name="closeEditor">The grid cell where the event occured. This may be null if the event did not occur within a grid cell.</param>
    /// <param name="closeEditor">Output flag indicating whether to keep the editor open or close it instantly</param>
    public void OnGridKeyDown(KeyEventArgs args, GridCell cell, out bool closeEditor, out bool closeEditorAndSaveChanges)
    {
      closeEditorAndSaveChanges = false;
      if (args.KeyData == Keys.Return)
      {
        closeEditor = true;
        closeEditorAndSaveChanges = true;
      }
      else if (args.KeyData == Keys.Escape)
        closeEditor = true;
      else
        closeEditor = false;
    }

    /// <summary>Handles the painting of the editor control</summary>
    /// <param name="graphics">Reference to the GDI+ surface</param>
    /// <param name="gridCell">Reference to the grid cell being rendered</param>
    /// <param name="isDrawHandled">Output flag indicating whether the method handled the drawing or not</param>
    public void DrawEditorControl(Graphics graphics, GridCell gridCell, out bool isDrawHandled)
    {
      isDrawHandled = false;
    }

    /// <summary>
    /// This methods is called by the grid control when the editor must update its layout
    /// </summary>
    /// <param name="cell">Reference to the GridCell being edited</param>
    public virtual void LayoutEditor(GridCell cell)
    {
      this.Bounds = new Rectangle(cell.ColumnItem.DrawBounds.X + 1, cell.RowItem.DrawBounds.Y + 1, cell.Bounds.Width - 1, cell.Bounds.Height - 1);
    }

    private void LoadDateTimePickerWithEditorValue()
    {
      int num;
      try
      {
        int result;
        num = !int.TryParse(this.editorValue.ToString(), out result) ? (int) this.editorValue : result;
      }
      catch (Exception ex)
      {
        Trace.WriteLine(ex.Message);
        num = 0;
      }
      this.Value = num;
    }

    /// <summary>
    /// This method is called by the grid control when the editor is being activated
    /// </summary>
    public void OnOpenEditor(GridCell cell)
    {
      if (cell == null)
        return;
      this.LoadDateTimePickerWithEditorValue();
      this.currentCell = cell;
    }

    /// <summary>
    /// This method is called by the grid control when at the end of a cell edit operation
    /// </summary>
    public void OnCloseEditor()
    {
      this.editorValue = (object) this.Value;
      this.currentCell = (GridCell) null;
    }
  }
}
