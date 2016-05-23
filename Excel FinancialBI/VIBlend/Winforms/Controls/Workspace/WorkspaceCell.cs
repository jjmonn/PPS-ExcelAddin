// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.WorkspaceCell
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System.Drawing;

namespace VIBlend.WinForms.Controls
{
  internal class WorkspaceCell
  {
    /// <summary>Gets or sets the row.</summary>
    /// <value>The row.</value>
    public int Row { get; internal set; }

    /// <summary>Gets or sets the column.</summary>
    /// <value>The column.</value>
    public int Column { get; internal set; }

    /// <summary>Gets or sets the bounds.</summary>
    /// <value>The bounds.</value>
    public Rectangle Bounds { get; internal set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.WorkspaceCell" /> class.
    /// </summary>
    /// <param name="row">The row.</param>
    /// <param name="column">The column.</param>
    /// <param name="bounds">The bounds.</param>
    public WorkspaceCell(int row, int column, Rectangle bounds)
    {
      this.Row = row;
      this.Column = column;
      this.Bounds = bounds;
    }

    /// <summary>
    /// Determines whether the specified <see cref="T:System.Object" /> is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="T:System.Object" /> to compare with this instance.</param>
    /// <returns>
    /// 	<c>true</c> if the specified <see cref="T:System.Object" /> is equal to this instance; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">
    /// The <paramref name="obj" /> parameter is null.
    /// </exception>
    public override bool Equals(object obj)
    {
      WorkspaceCell workspaceCell = obj as WorkspaceCell;
      return workspaceCell.Bounds.Equals((object) this.Bounds) && workspaceCell.Row == this.Row && workspaceCell.Column == this.Column;
    }
  }
}
