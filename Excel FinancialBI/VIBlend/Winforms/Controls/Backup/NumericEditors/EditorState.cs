// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.EditorState
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;

namespace VIBlend.WinForms.Controls
{
  public class EditorState
  {
    private int selectionStart;
    private int selectionLength;
    private string text;
    private Decimal value;
    private EditorStateType stateType;

    /// <summary>Gets or sets editor's state type.</summary>
    public EditorStateType StateType
    {
      get
      {
        return this.stateType;
      }
      set
      {
        this.stateType = value;
      }
    }

    /// <summary>Gets or sets editor's text</summary>
    public string Text
    {
      get
      {
        return this.text;
      }
      set
      {
        this.text = value;
      }
    }

    /// <summary>Gets or sets editor's value.</summary>
    public Decimal Value
    {
      get
      {
        return this.value;
      }
      set
      {
        this.value = value;
      }
    }

    /// <summary>Gets or sets selection start.</summary>
    public int SelectionStart
    {
      get
      {
        return this.selectionStart;
      }
      set
      {
        this.selectionStart = value;
      }
    }

    /// <summary>Gets or sets selection length</summary>
    public int SelectionLength
    {
      get
      {
        return this.selectionLength;
      }
      set
      {
        this.selectionLength = value;
      }
    }
  }
}
