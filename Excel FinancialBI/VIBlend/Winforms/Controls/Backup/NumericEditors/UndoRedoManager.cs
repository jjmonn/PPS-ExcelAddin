// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.UndoRedoManager
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System.Collections.Generic;

namespace VIBlend.WinForms.Controls
{
  public class UndoRedoManager
  {
    private List<EditorState> states = new List<EditorState>();
    private int counter;

    /// <summary>
    /// Saves the current state and adds it to the states collection.
    /// </summary>
    /// <param name="state"></param>
    public void SaveChanges(EditorState state)
    {
      this.states.Add(state);
      this.counter = this.states.Count - 1;
    }

    /// <summary>Undo current state.</summary>
    /// <returns></returns>
    public EditorState Undo()
    {
      if (this.counter > 0)
        --this.counter;
      return this.GetState();
    }

    /// <summary>Redo current state.</summary>
    /// <returns></returns>
    public EditorState Redo()
    {
      if (this.counter + 1 < this.states.Count)
        ++this.counter;
      return this.GetState();
    }

    /// <summary>Gets the current state.</summary>
    /// <returns></returns>
    public EditorState GetState()
    {
      if (this.counter >= 0 && this.counter < this.states.Count)
        return this.states[this.counter];
      return (EditorState) null;
    }
  }
}
