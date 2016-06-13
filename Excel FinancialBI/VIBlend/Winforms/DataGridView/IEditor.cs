// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.IEditor
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System.Drawing;
using System.Windows.Forms;

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>Grid cell editor interface definition</summary>
  public interface IEditor
  {
    /// <summary>Gets the type of the grid cell editor</summary>
    CellEditorType EditorType { get; }

    /// <summary>Gets or sets the editor activation flags</summary>
    /// <remarks>
    /// A editor in GridView can be activated by mouse or keyboard events. For example a checkbox editor
    /// can be activated when the users moves the mouse over it. Use this property to specify the activation
    /// preferences for the editor.
    /// </remarks>
    EditorActivationFlags ActivationFlags { get; set; }

    /// <summary>Gets or sets the editor deactivation flags</summary>
    /// <remarks>
    /// A editor in GridView can be deactivated by mouse or keyboard events. For example a checkbox editor
    /// can be deactivated when the users moves the mouse out of the cell where the checkbox is located. Use this property to specify the deactivation
    /// preferences for the editor.
    /// </remarks>
    EditorActivationFlags DeActivationFlags { get; set; }

    /// <summary>
    /// Gets a reference to the underlying control of the editor
    /// </summary>
    Control Control { get; }

    /// <summary>Gets or sets the current value of the editor.</summary>
    /// <remarks>
    /// When the grid opens an editor it will set the EditorValue property with the value of the grid cell being edited.
    /// Once the edtior closes, the editor will use the EditorValue property to get the new value for the grid cell
    /// </remarks>
    object EditorValue { get; set; }

    /// <summary>Handles grid KeyDown events</summary>
    /// <remarks>
    /// This method is called by the grid to pass KeyDown events if the editor is currently open.
    /// Use this method to handle the KeyDown events and instruct the grid whether to keep the editor open
    /// </remarks>
    /// <param name="args">KeyDown event arguments</param>
    /// <param name="cell">The grid cell where the event occured. This may be null if the event did not occur within a grid cell.</param>
    /// <param name="closeEditor">Output flag indicating whether to keep the editor open or close it instantly</param>
    void OnGridKeyDown(KeyEventArgs args, GridCell cell, out bool closeEditor, out bool closeEditorAndSaveChanges);

    /// <summary>Handles the painting of the editor control</summary>
    /// <remarks>
    /// This method is called everytime the grid has to render a cell associated with the editor and the editor is not activated.
    /// Use the method to customize the visual appearance of grid cell which are currently not being edited. If you want to skip
    /// the custom painting set the isDrawHandled flag to false. This will instruct the grid to format the cell's value using
    /// the respective format provider and format expression and render it as text, image or both.
    /// </remarks>
    /// <param name="graphics">Reference to the GDI+ surface</param>
    /// <param name="gridCell">Reference to the grid cell being rendered</param>
    /// <param name="isDrawHandled">Output flag indicating whether the method handled the drawing or not</param>
    void DrawEditorControl(Graphics graphics, GridCell gridCell, out bool isDrawHandled);

    /// <summary>
    /// This method is called by the grid control when the editor is being activated
    /// </summary>
    /// <remarks>Use this method to perform your own initialization</remarks>
    /// <param name="cell">Reference to the GridCell being edited</param>
    void OnOpenEditor(GridCell cell);

    /// <summary>
    /// This methods is called by the grid control when the editor must update its layout
    /// </summary>
    /// <param name="cell">Reference to the GridCell being edited</param>
    void LayoutEditor(GridCell cell);

    /// <summary>
    /// This method is called by the grid control when at the end of a cell edit operation
    /// </summary>
    /// <remarks>
    /// Use this method to implement your own cleanup or post edit operation
    /// </remarks>
    void OnCloseEditor();
  }
}
