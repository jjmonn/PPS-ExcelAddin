// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.EditorActivationFlags
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>Enumaration of the grid editor activation flags</summary>
  [Flags]
  public enum EditorActivationFlags
  {
    MOUSE_MOVE = 1,
    MOUSE_CLICK = 2,
    MOUSE_DBLCLICK = 4,
    KEY_PRESS = 8,
    PROGRAMMATIC = 16,
    MOUSE_CLICK_SELECTED_CELL = 32,
    KEY_PRESS_ENTER = 64,
    KEY_PRESS_ESC = 128,
  }
}
