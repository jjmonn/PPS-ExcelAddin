// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.MaskEditor
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VIBlend.WinForms.Controls;

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>Represents a Mask Editor.</summary>
  [ToolboxItem(false)]
  public class MaskEditor : vMaskedTextBox, IEditor
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
        return CellEditorType.Custom;
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
        string @string = this.TextBox.MaskedTextProvider.ToString(false, false, 0, this.Text.Length);
        if (@string.Length == 0)
        {
          this.editorValue = (object) @string;
          return this.editorValue;
        }
        this.editorValue = (object) this.Text;
        return this.editorValue;
      }
      set
      {
        this.editorValue = value == null ? (object) "" : value;
        this.Text = this.editorValue.ToString();
        this.Refresh();
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

    /// <summary>
    /// This method is called by the grid control when the editor is being activated
    /// </summary>
    public void OnOpenEditor(GridCell cell)
    {
      if (cell == null)
        return;
      this.currentCell = cell;
      this.Focus();
    }

    /// <summary>
    /// This method is called by the grid control when at the end of a cell edit operation
    /// </summary>
    public void OnCloseEditor()
    {
      this.EditorValue = (object) this.Text;
      this.currentCell = (GridCell) null;
    }
  }
}
