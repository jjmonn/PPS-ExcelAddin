// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.ButtonEditor
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System.Drawing;
using System.Windows.Forms;
using VIBlend.WinForms.Controls;

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>
  /// Represents a ButtonEditor used in the GridView control
  /// </summary>
  public class ButtonEditor : IEditor
  {
    private vButton button = new vButton();
    private EditorActivationFlags activationFlags = EditorActivationFlags.MOUSE_MOVE | EditorActivationFlags.PROGRAMMATIC;
    private EditorActivationFlags deactivationFlags = EditorActivationFlags.MOUSE_MOVE | EditorActivationFlags.PROGRAMMATIC;
    private vButton tmpButton = new vButton();
    private Rectangle bounds;
    private object editorValue;
    private GridCell currentCell;

    /// <summary>Gets the type of the grid cell editor</summary>
    public CellEditorType EditorType
    {
      get
      {
        return CellEditorType.Button;
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
        return (Control) this.button;
      }
    }

    /// <summary>Get's or sets the current value of the editor</summary>
    public object EditorValue
    {
      get
      {
        return this.editorValue;
      }
      set
      {
        this.editorValue = value != null ? value : (object) "";
        this.button.Text = this.editorValue.ToString();
        this.button.Refresh();
      }
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

    /// <summary>Handles the painting of the editor control</summary>
    /// <param name="graphics">Reference to the GDI+ surface</param>
    /// <param name="gridCell">Reference to the grid cell being rendered</param>
    /// <param name="isDrawHandled">Output flag indicating whether the method handled the drawing or not</param>
    public void DrawEditorControl(Graphics graphics, GridCell gridCell, out bool isDrawHandled)
    {
      this.Layout(gridCell);
      this.tmpButton.Bounds = this.bounds;
      this.tmpButton.Text = gridCell.FormattedText;
      this.tmpButton.Bounds = this.bounds;
      this.tmpButton.VIBlendTheme = gridCell.CellsArea.GridControl.VIBlendTheme;
      Bitmap bitmap = new Bitmap(this.bounds.Width, this.bounds.Height);
      this.tmpButton.DrawToBitmap(bitmap, new Rectangle(0, 0, this.bounds.Width, this.bounds.Height));
      graphics.DrawImage((Image) bitmap, this.bounds.X, this.bounds.Y);
      isDrawHandled = true;
      isDrawHandled = true;
    }

    /// <summary>
    /// This methods is called by the grid control when the editor must update its layout
    /// </summary>
    /// <param name="cell">Reference to the GridCell being edited</param>
    public virtual void LayoutEditor(GridCell cell)
    {
      this.Layout(cell);
    }

    /// <exclude />
    protected void Layout(GridCell cell)
    {
      this.bounds = new Rectangle(1, 1, cell.Bounds.Width - 9, cell.Bounds.Height - 9);
      CellsArea cellsArea = cell.CellsArea;
      if (cell.TextAlignment == ContentAlignment.BottomLeft || cell.TextAlignment == ContentAlignment.MiddleLeft || cell.TextAlignment == ContentAlignment.TopLeft)
        this.bounds.X = cell.ColumnItem.X + cellsArea.GridControl.ColumnsHierarchy.X + 2;
      if (cell.TextAlignment == ContentAlignment.BottomRight || cell.TextAlignment == ContentAlignment.MiddleRight || cell.TextAlignment == ContentAlignment.TopRight)
        this.bounds.X = cell.ColumnItem.X + cellsArea.GridControl.ColumnsHierarchy.X + cell.ColumnItem.WidthWithChildren - this.bounds.Width - 2;
      if (cell.TextAlignment == ContentAlignment.BottomCenter || cell.TextAlignment == ContentAlignment.MiddleCenter || cell.TextAlignment == ContentAlignment.TopCenter)
        this.bounds.X = cell.ColumnItem.X + cellsArea.GridControl.ColumnsHierarchy.X + (cell.ColumnItem.WidthWithChildren - this.bounds.Width) / 2;
      if (cell.TextAlignment == ContentAlignment.TopLeft || cell.TextAlignment == ContentAlignment.TopRight || cell.TextAlignment == ContentAlignment.TopCenter)
        this.bounds.Y = cell.RowItem.Y + cellsArea.GridControl.RowsHierarchy.Y + 2;
      if (cell.TextAlignment == ContentAlignment.BottomLeft || cell.TextAlignment == ContentAlignment.BottomRight || cell.TextAlignment == ContentAlignment.BottomCenter)
        this.bounds.Y = cell.RowItem.Y + cell.RowItem.HeightWithChildren + cellsArea.GridControl.RowsHierarchy.Y - this.bounds.Height - 2;
      if (cell.TextAlignment != ContentAlignment.MiddleLeft && cell.TextAlignment != ContentAlignment.MiddleRight && cell.TextAlignment != ContentAlignment.MiddleCenter)
        return;
      this.bounds.Y = cell.RowItem.Y + cell.RowItem.Hierarchy.Y + (cell.RowItem.HeightWithChildren - this.bounds.Height) / 2;
    }

    /// <summary>
    /// This method is called by the grid control when the editor is being activated
    /// </summary>
    public virtual void OnOpenEditor(GridCell cell)
    {
      if (cell == null || cell.CellsArea == null || this.currentCell == cell)
        return;
      this.currentCell = cell;
      this.Layout(cell);
      this.button.Show();
      this.button.Text = this.editorValue.ToString();
      this.button.SetBounds(this.bounds.X, this.bounds.Y, this.bounds.Width, this.bounds.Height);
      this.button.VIBlendTheme = cell.CellsArea.GridControl.VIBlendTheme;
      this.button.Refresh();
    }

    /// <summary>
    /// This method is called by the grid control when at the end of a cell edit operation
    /// </summary>
    public virtual void OnCloseEditor()
    {
      this.currentCell = (GridCell) null;
    }
  }
}
