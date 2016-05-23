// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.EditorActivationCancelEventArgs
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System.Windows.Forms;

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>
  /// Represents Grid cell editor activation event arguments that supports event canceling.
  /// </summary>
  public class EditorActivationCancelEventArgs : CellCancelEventArgs
  {
    private EditorActivationFlags activationFlags = EditorActivationFlags.MOUSE_DBLCLICK;
    private KeyEventArgs keyArgs = new KeyEventArgs(Keys.None);

    /// <summary>
    /// Gets the bitmaks that represents the cell editor's activation flags.
    /// </summary>
    public EditorActivationFlags ActivationFlags
    {
      get
      {
        return this.activationFlags;
      }
    }

    /// <summary>Gets the KeyEventArgs.</summary>
    public KeyEventArgs KeyEventArgs
    {
      get
      {
        return this.keyArgs;
      }
    }

    /// <summary>EditorActivationCancelEventArgs constructor.</summary>
    /// <param name="cell">A refrence to the grid cell.</param>
    /// <param name="editorActivationFlag">Bitmask that represents the editor activation flags.</param>
    /// <param name="keyArgs">Reference to the key event arguments.</param>
    public EditorActivationCancelEventArgs(GridCell cell, EditorActivationFlags editorActivationFlag, KeyEventArgs keyArgs)
      : base(cell)
    {
      this.activationFlags = editorActivationFlag;
      this.keyArgs = keyArgs;
    }
  }
}
