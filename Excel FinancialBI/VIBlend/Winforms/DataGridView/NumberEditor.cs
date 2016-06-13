// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.NumberEditor
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
  [ToolboxItem(false)]
  public class NumberEditor : vNumberEditor, IEditor
  {
    private EditorActivationFlags activationFlags = EditorActivationFlags.PROGRAMMATIC | EditorActivationFlags.MOUSE_CLICK_SELECTED_CELL | EditorActivationFlags.KEY_PRESS_ENTER;
    private EditorActivationFlags deactivationFlags = EditorActivationFlags.MOUSE_CLICK | EditorActivationFlags.MOUSE_DBLCLICK | EditorActivationFlags.PROGRAMMATIC | EditorActivationFlags.KEY_PRESS_ESC;
    private bool handleEnterAndEscKeys = true;
    private GridCell editCell;
    private object editorValue;
    private GridCell currentCell;

    /// <summary>Gets the type of the grid cell editor</summary>
    public CellEditorType EditorType
    {
      get
      {
        return CellEditorType.Number;
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

    /// <summary>Gets the edit cell.</summary>
    /// <value>The edit cell.</value>
    public GridCell EditCell
    {
      get
      {
        return this.editCell;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to handle the enter and esc keys.
    /// </summary>
    [Description("Gets or sets a value indicating whether to handle the enter and esc keys.")]
    [Category("Behavior")]
    public bool HandleEnterKey
    {
      get
      {
        return this.handleEnterAndEscKeys;
      }
      set
      {
        this.handleEnterAndEscKeys = value;
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
        this.editorValue = value == null ? (object) 0 : value;
        this.LoadEditorValue();
        this.Refresh();
      }
    }

    /// <summary>
    /// Raises the <see cref="E:KeyDown" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs" /> instance containing the event data.</param>
    protected override void OnKeyDown(KeyEventArgs e)
    {
      this.HandleDefaultKeys(e);
      base.OnKeyDown(e);
    }

    /// <summary>Handles the default keys.</summary>
    /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs" /> instance containing the event data.</param>
    protected virtual void HandleDefaultKeys(KeyEventArgs e)
    {
      if (!this.HandleEnterKey)
        return;
      if (e.KeyCode == Keys.Return)
      {
        if (this.EditCell == null)
          return;
        this.EditCell.RowItem.DataGridView.CloseEditor(false);
      }
      else
      {
        if (e.KeyCode != Keys.Escape || this.EditCell == null)
          return;
        this.EditCell.RowItem.DataGridView.CloseEditor(true);
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
      this.editCell = cell;
      this.Bounds = new Rectangle(cell.ColumnItem.DrawBounds.X + 1, cell.RowItem.DrawBounds.Y + 1, cell.Bounds.Width - 1, cell.Bounds.Height - 1);
    }

    /// <summary>Loads the editor value.</summary>
    protected virtual void LoadEditorValue()
    {
      Decimal num1 = new Decimal(0);
      Decimal num2;
      try
      {
        Decimal result;
        num2 = !Decimal.TryParse(this.editorValue.ToString(), out result) ? (Decimal) this.editorValue : result;
      }
      catch (Exception ex)
      {
        Trace.WriteLine(ex.Message);
        num2 = new Decimal(0);
      }
      this.Value = num2;
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
      this.LoadEditorValue();
    }

    /// <summary>
    /// This method is called by the grid control when at the end of a cell edit operation
    /// </summary>
    public void OnCloseEditor()
    {
      this.EditorValue = (object) this.Value;
      this.currentCell = (GridCell) null;
    }
  }
}
