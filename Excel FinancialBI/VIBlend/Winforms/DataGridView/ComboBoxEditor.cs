// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.ComboBoxEditor
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
  /// Represents a ComboBoxEditor used in the GridView control
  /// </summary>
  [ToolboxItem(false)]
  public class ComboBoxEditor : vComboBox, IEditor
  {
    private bool autoOpenEditorOnActivation = true;
    private bool enableCloseOnSelectionChanged = true;
    private EditorActivationFlags activationFlags = EditorActivationFlags.PROGRAMMATIC | EditorActivationFlags.MOUSE_CLICK_SELECTED_CELL | EditorActivationFlags.KEY_PRESS_ENTER;
    private EditorActivationFlags deactivationFlags = EditorActivationFlags.MOUSE_CLICK | EditorActivationFlags.MOUSE_DBLCLICK | EditorActivationFlags.PROGRAMMATIC | EditorActivationFlags.KEY_PRESS_ESC;
    private bool selectionFromMouseDown;
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
        return CellEditorType.ComboBox;
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
        if (this.SelectedIndex == -1 || this.SelectedItem == null)
          return (object) null;
        this.editorValue = (object) this.SelectedItem.Text;
        return this.editorValue;
      }
      set
      {
        this.editorValue = value;
        this.LoadComboBoxWithEditorValue();
      }
    }

    /// <summary>Occurs when the editor is loaded.</summary>
    [Category("Action")]
    public event EventHandler Loaded;

    /// <summary>Constructor</summary>
    public ComboBoxEditor()
    {
      this.ListBox.MouseDown += new MouseEventHandler(this.ListBox_MouseDown);
      this.ListBox.KeyDown += new KeyEventHandler(this.ListBox_KeyDown);
    }

    private void ListBox_KeyDown(object sender, KeyEventArgs e)
    {
      this.HandleEnter(e);
    }

    private void ListBox_MouseDown(object sender, MouseEventArgs e)
    {
      this.selectionFromMouseDown = true;
    }

    protected override void OnSelectedIndexChanged(EventArgs e)
    {
      base.OnSelectedIndexChanged(e);
      if (this.selectionFromMouseDown && !this.isOpening && (this.EnableCloseOnSelectionChanged && this.EditCell != null))
        this.EditCell.RowItem.DataGridView.CloseEditor(false);
      this.selectionFromMouseDown = false;
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

    /// <summary>Loads the combo box with editor value.</summary>
    protected virtual void LoadComboBoxWithEditorValue()
    {
      this.editCell = (GridCell) null;
      this.isOpening = true;
      string str = this.editorValue != null ? this.editorValue.ToString() : "";
      int index;
      for (index = 0; index < this.Items.Count; ++index)
      {
        if (this.Items[index].Text == str && str.Length > 0)
        {
          this.SelectedIndex = index;
          break;
        }
      }
      if (this.SelectedItem != null && index >= this.Items.Count && (this.SelectedItem.ImageIndex != -1 && this.SelectedItem.Text.Length == 0) || index < this.Items.Count)
        return;
      this.SelectedIndex = -1;
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
      if (this.DataSource != null && this.Items.Count == 0)
      {
        Form parentForm = this.GetParentForm();
        if (parentForm != null)
        {
          this.BindingContext = parentForm.BindingContext;
          this.ListBox.BindingContext = parentForm.BindingContext;
        }
      }
      this.LoadComboBoxWithEditorValue();
      if (this.AutoOpenEditorOnActivation)
      {
        Rectangle buttonRectangle = this.EditBase.ButtonRectangle;
        this.ShowDropDown(new Point(buttonRectangle.Left + 1, buttonRectangle.Bottom - 2));
      }
      this.isOpening = false;
      this.OnLoaded();
    }

    /// <summary>Called when the drop down is opened.</summary>
    protected override void OnDropDownOpened()
    {
      base.OnDropDownOpened();
      this.EditBase.Focus();
    }

    /// <summary>Called when the editor is loaded.</summary>
    protected virtual void OnLoaded()
    {
      if (this.Loaded == null)
        return;
      this.Loaded((object) this, EventArgs.Empty);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
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
      {
        this.Bounds = new Rectangle(cell.ColumnItem.DrawBounds.X + 1, cell.RowItem.DrawBounds.Y + 1, cell.Bounds.Width - 1, cell.Bounds.Height - 1);
        this.DropDownWidth = this.Bounds.Width;
      }
      this.editCell = cell;
    }

    /// <summary>
    /// This method is called by the grid control when at the end of a cell edit operation
    /// </summary>
    public virtual void OnCloseEditor()
    {
      if (this.SelectedIndex != -1 && this.SelectedItem != null && !string.IsNullOrEmpty(this.SelectedItem.Text))
        this.editorValue = (object) this.SelectedItem.Text;
      this.currentCell = (GridCell) null;
    }
  }
}
