// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.ColorPickerEditor
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VIBlend.WinForms.Controls;

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>
  /// Represents a ColorPickerEditor used in the GridView control
  /// </summary>
  [ToolboxItem(false)]
  public class ColorPickerEditor : vColorPicker, IEditor
  {
    private bool autoOpenEditorOnActivation = true;
    private bool enableCloseOnSelectionChanged = true;
    private EditorActivationFlags activationFlags = EditorActivationFlags.PROGRAMMATIC | EditorActivationFlags.MOUSE_CLICK_SELECTED_CELL | EditorActivationFlags.KEY_PRESS_ENTER;
    private EditorActivationFlags deactivationFlags = EditorActivationFlags.MOUSE_CLICK | EditorActivationFlags.MOUSE_DBLCLICK | EditorActivationFlags.PROGRAMMATIC | EditorActivationFlags.KEY_PRESS_ESC;
    private GridCell editCell;
    private object editorValue;
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
        return CellEditorType.ColorPicker;
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

    /// <summary>Gets the edit cell.</summary>
    /// <value>The edit cell.</value>
    public GridCell EditCell
    {
      get
      {
        return this.editCell;
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
        Color selectedColor = this.SelectedColor;
        if (!(this.SelectedColor != Color.Empty))
          return (object) null;
        this.editorValue = (object) this.SelectedColor;
        return this.editorValue;
      }
      set
      {
        this.editorValue = value;
        this.LoadColorPickerWithEditorValue();
      }
    }

    /// <summary>Occurs when the editor is loaded.</summary>
    [Category("Action")]
    public event EventHandler Loaded;

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

    /// <summary>Loads the combo box with editor value.</summary>
    protected virtual void LoadColorPickerWithEditorValue()
    {
      this.editCell = (GridCell) null;
      this.isOpening = true;
      Color color1 = Color.Empty;
      Color color2;
      try
      {
        color2 = this.editorValue != null ? (Color) this.editorValue : Color.Empty;
      }
      catch (Exception ex)
      {
        this.SelectedColor = Color.Empty;
        return;
      }
      this.SelectedColor = color2;
    }

    private Form GetParentForm()
    {
      for (Control parent = this.Parent; parent != null && parent != null; parent = parent.Parent)
      {
        if (parent is Form)
          return parent as Form;
      }
      return (Form) null;
    }

    /// <summary>
    /// This method is called by the grid control when the editor is being activated
    /// </summary>
    public virtual void OnOpenEditor(GridCell cell)
    {
      if (cell == null)
        return;
      this.isOpening = true;
      this.currentCell = cell;
      this.LoadColorPickerWithEditorValue();
      if (this.AutoOpenEditorOnActivation)
      {
        Rectangle buttonRectangle = this.EditBase.ButtonRectangle;
        this.ShowDropDown(new Point(buttonRectangle.Left + 1, buttonRectangle.Bottom - 2));
      }
      this.isOpening = false;
      this.OnLoaded();
    }

    /// <summary>Called when the editor is loaded.</summary>
    protected virtual void OnLoaded()
    {
      if (this.Loaded == null)
        return;
      this.Loaded((object) this, EventArgs.Empty);
    }

    /// <summary>
    /// Raises the <see cref="E:KeyDown" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs" /> instance containing the event data.</param>
    protected override void OnKeyDown(KeyEventArgs e)
    {
      this.HandleEnter(e);
      base.OnKeyDown(e);
    }

    private void HandleEnter(KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return || (this.DeActivationFlags & EditorActivationFlags.KEY_PRESS_ENTER) == (EditorActivationFlags) 0 || this.EditCell == null)
        return;
      this.EditCell.RowItem.DataGridView.CloseEditor(false);
    }

    /// <summary>
    /// This methods is called by the grid control when the editor must update its layout
    /// </summary>
    /// <param name="cell">Reference to the GridCell being edited</param>
    public virtual void LayoutEditor(GridCell cell)
    {
      if (!this.DropDown.Visible)
        this.Bounds = new Rectangle(cell.ColumnItem.DrawBounds.X + 1, cell.RowItem.DrawBounds.Y + 1, cell.Bounds.Width - 1, cell.Bounds.Height - 1);
      this.editCell = cell;
    }

    /// <summary>
    /// This method is called by the grid control when at the end of a cell edit operation
    /// </summary>
    public virtual void OnCloseEditor()
    {
      if (this.SelectedColor != Color.Empty)
        this.editorValue = (object) this.Text;
      this.currentCell = (GridCell) null;
    }
  }
}
