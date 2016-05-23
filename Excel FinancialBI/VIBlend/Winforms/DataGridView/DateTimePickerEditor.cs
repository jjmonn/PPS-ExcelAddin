// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.DateTimePickerEditor
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
  /// Represents a DateTimePickerEditor used in the GridView control
  /// </summary>
  [ToolboxItem(false)]
  public class DateTimePickerEditor : vDateTimePicker, IEditor
  {
    private bool autoOpenEditorOnActivation = true;
    private bool enableCloseOnSelectionChanged = true;
    private EditorActivationFlags activationFlags = EditorActivationFlags.PROGRAMMATIC | EditorActivationFlags.MOUSE_CLICK_SELECTED_CELL | EditorActivationFlags.KEY_PRESS_ENTER;
    private EditorActivationFlags deactivationFlags = EditorActivationFlags.MOUSE_CLICK | EditorActivationFlags.MOUSE_DBLCLICK | EditorActivationFlags.PROGRAMMATIC | EditorActivationFlags.KEY_PRESS_ESC;
    private object editorValue;
    private GridCell editCell;
    private GridCell currentCell;
    private bool isOpening;

    /// <summary>
    /// Gets or sets a value indicating whether to close the editor when the selection is changed.
    /// </summary>
    public bool EnableCloseOnSelectionChanged
    {
      get
      {
        return this.enableCloseOnSelectionChanged;
      }
      set
      {
        if (value == this.enableCloseOnSelectionChanged)
          return;
        this.enableCloseOnSelectionChanged = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to open the editor when activated.
    /// </summary>
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool AutoOpenEditorOnActivation
    {
      get
      {
        return this.autoOpenEditorOnActivation;
      }
      set
      {
        if (value == this.autoOpenEditorOnActivation)
          return;
        this.autoOpenEditorOnActivation = value;
      }
    }

    /// <summary>Gets the type of the grid cell editor</summary>
    public CellEditorType EditorType
    {
      get
      {
        return CellEditorType.TimePicker;
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
        try
        {
          this.editCell = (GridCell) null;
          this.isOpening = true;
          this.Value = new DateTime?((DateTime) this.editorValue);
        }
        catch (Exception ex)
        {
          Trace.WriteLine(ex.Message);
          this.Value = new DateTime?(DateTime.Now);
        }
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

    /// <summary>Called when the value has been changed</summary>
    protected override void OnValueChanged()
    {
      base.OnValueChanged();
      if (this.isOpening || !this.Calendar.Visible || (!this.EnableCloseOnSelectionChanged || this.EditCell == null))
        return;
      this.EditCell.RowItem.DataGridView.CloseEditor(false);
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
      else if (args.KeyData == Keys.Escape)
        closeEditor = true;
      else
        closeEditor = false;
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
      this.Focus();
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
    /// This method is called by the grid control when the editor is being activated
    /// </summary>
    public virtual void OnOpenEditor(GridCell cell)
    {
      if (cell == null || cell.CellsArea == null || this.currentCell == cell)
        return;
      this.isOpening = true;
      this.currentCell = cell;
      this.VIBlendTheme = cell.CellsArea.GridControl.VIBlendTheme;
      if (this.AutoOpenEditorOnActivation)
      {
        Rectangle buttonRectangle = this.EditBase.ButtonRectangle;
        this.ShowDropDown(new Point(buttonRectangle.Left + 1, buttonRectangle.Bottom - 2));
        this.EditBase.Focus();
      }
      this.isOpening = false;
    }

    /// <summary>
    /// This methods is called by the grid control when the editor must update its layout
    /// </summary>
    /// <param name="cell">Reference to the GridCell being edited</param>
    public virtual void LayoutEditor(GridCell cell)
    {
      this.Bounds = new Rectangle(cell.ColumnItem.DrawBounds.X + 1, cell.RowItem.DrawBounds.Y + 1, cell.Bounds.Width - 1, cell.Bounds.Height - 1);
      this.editCell = cell;
    }

    /// <summary>
    /// This method is called by the grid control when at the end of a cell edit operation
    /// </summary>
    public virtual void OnCloseEditor()
    {
      this.editorValue = (object) this.Value;
      this.currentCell = (GridCell) null;
      this.editCell = (GridCell) null;
    }
  }
}
