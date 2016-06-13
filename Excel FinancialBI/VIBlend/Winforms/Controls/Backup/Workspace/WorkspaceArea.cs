// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.WorkspaceArea
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls
{
  [ProvideProperty("RowSpan", typeof (Control))]
  [ProvideProperty("Column", typeof (Control))]
  [ProvideProperty("Row", typeof (Control))]
  [ProvideProperty("ColumnSpan", typeof (Control))]
  internal class WorkspaceArea : Panel
  {
    private TableLayoutPanel tableLayout = new TableLayoutPanel();
    private List<WorkspaceCell> cells = new List<WorkspaceCell>();
    private bool allowResize = true;
    private Color[] colors = new Color[12]{ Color.AliceBlue, Color.AntiqueWhite, Color.Aqua, Color.Aquamarine, Color.Azure, Color.Beige, Color.Bisque, Color.Black, Color.BlanchedAlmond, Color.Blue, Color.BlueViolet, Color.Brown };
    private Random random = new Random();
    private Dictionary<Rectangle, WorkspaceCell> cellsDictionary = new Dictionary<Rectangle, WorkspaceCell>();
    private Brush splitterBrush = (Brush) new SolidBrush(Color.Navy);
    private int splitterSize = 4;

    /// <summary>
    /// Gets or sets a value indicating whether resizing is enabled.
    /// </summary>
    /// <value><c>true</c> if [allow resize]; otherwise, <c>false</c>.</value>
    public bool AllowResize
    {
      get
      {
        return this.allowResize;
      }
      set
      {
        this.allowResize = value;
      }
    }

    /// <summary>Gets the cells.</summary>
    /// <value>The cells.</value>
    public WorkspaceCell[] Cells
    {
      get
      {
        return this.cells.ToArray();
      }
    }

    /// <summary>Gets or sets the column count.</summary>
    /// <value>The column count.</value>
    [Localizable(true)]
    [DefaultValue(0)]
    public int ColumnCount
    {
      get
      {
        return this.tableLayout.ColumnCount;
      }
      set
      {
        this.tableLayout.ColumnCount = value;
      }
    }

    /// <summary>Gets or sets the row count.</summary>
    /// <value>The row count.</value>
    [Localizable(true)]
    [DefaultValue(0)]
    public int RowCount
    {
      get
      {
        return this.tableLayout.RowCount;
      }
      set
      {
        this.tableLayout.RowCount = value;
      }
    }

    /// <summary>Gets the row styles.</summary>
    /// <value>The row styles.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [MergableProperty(false)]
    [Browsable(false)]
    [DisplayName("Rows")]
    public TableLayoutRowStyleCollection RowStyles
    {
      get
      {
        return this.tableLayout.RowStyles;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [MergableProperty(false)]
    [Browsable(false)]
    [DisplayName("Columns")]
    public TableLayoutColumnStyleCollection ColumnStyles
    {
      get
      {
        return this.tableLayout.ColumnStyles;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.WorkspaceArea" /> class.
    /// </summary>
    public WorkspaceArea()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.tableLayout.Dock = DockStyle.Fill;
      this.Controls.Add((Control) this.tableLayout);
      this.tableLayout.CellPaint += new TableLayoutCellPaintEventHandler(this.tableLayout_CellPaint);
      this.tableLayout.MouseDown += new MouseEventHandler(this.tableLayout_MouseDown);
      this.tableLayout.MouseMove += new MouseEventHandler(this.tableLayout_MouseMove);
    }

    private void tableLayout_MouseMove(object sender, MouseEventArgs e)
    {
      WorkspaceCell workspaceCell = this.HitTest(e.Location);
      if (workspaceCell == null)
        return;
      Rectangle rectangle1 = new Rectangle(workspaceCell.Bounds.Right - this.splitterSize, workspaceCell.Bounds.Y, this.splitterSize, workspaceCell.Bounds.Height);
      Rectangle rectangle2 = new Rectangle(workspaceCell.Bounds.X, workspaceCell.Bounds.Bottom - this.splitterSize, workspaceCell.Bounds.Width, this.splitterSize);
    }

    private void tableLayout_MouseDown(object sender, MouseEventArgs e)
    {
      this.HitTest(e.Location);
    }

    /// <summary>Tests for a cell under the mouse cursor.</summary>
    /// <param name="point">The point.</param>
    /// <returns></returns>
    public WorkspaceCell HitTest(Point point)
    {
      foreach (WorkspaceCell cell in this.cells)
      {
        if (cell.Bounds.Contains(point))
          return cell;
      }
      return (WorkspaceCell) null;
    }

    private void tableLayout_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
    {
      if (!this.cellsDictionary.ContainsKey(e.CellBounds))
      {
        WorkspaceCell workspaceCell = new WorkspaceCell(e.Row, e.Column, e.CellBounds);
        this.cells.Add(workspaceCell);
        this.cellsDictionary.Add(workspaceCell.Bounds, workspaceCell);
      }
      int index = this.random.Next(0, this.colors.Length);
      e.Graphics.FillRectangle((Brush) new SolidBrush(this.colors[index]), e.CellBounds);
      if (!this.AllowResize)
        return;
      Rectangle rect1 = new Rectangle(e.CellBounds.Right - this.splitterSize, e.CellBounds.Y, this.splitterSize, e.CellBounds.Height);
      Rectangle rect2 = new Rectangle(e.CellBounds.X, e.CellBounds.Bottom - this.splitterSize, e.CellBounds.Width, this.splitterSize);
      if (this.splitterBrush == null)
        return;
      if (e.Row < this.RowCount - 1)
        e.Graphics.FillRectangle(this.splitterBrush, rect2);
      if (e.Column >= this.ColumnCount - 1)
        return;
      e.Graphics.FillRectangle(this.splitterBrush, rect1);
    }

    /// <summary>
    /// </summary>
    /// <param name="levent">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      this.RefreshLayout();
    }

    /// <summary>Refreshes the layout.</summary>
    public virtual void RefreshLayout()
    {
    }

    /// <summary>Gets the column.</summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    [DefaultValue(-1)]
    [DisplayName("Column")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int GetColumn(WorkspaceItem item)
    {
      return this.tableLayout.GetColumn((Control) item);
    }

    /// <summary>Gets the column span.</summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    [DefaultValue(1)]
    [DisplayName("ColumnSpan")]
    public int GetColumnSpan(WorkspaceItem item)
    {
      return this.tableLayout.GetColumnSpan((Control) item);
    }

    /// <summary>Gets the column widths.</summary>
    /// <returns></returns>
    [Browsable(false)]
    public int[] GetColumnWidths()
    {
      return this.tableLayout.GetColumnWidths();
    }

    /// <summary>Gets the row.</summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(-1)]
    [DisplayName("Row")]
    public int GetRow(WorkspaceItem item)
    {
      return this.tableLayout.GetRow((Control) item);
    }

    /// <summary>Gets the row heights.</summary>
    /// <returns></returns>
    [Browsable(false)]
    public int[] GetRowHeights()
    {
      return this.tableLayout.GetRowHeights();
    }

    /// <summary>Gets the row span.</summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    [DisplayName("RowSpan")]
    [DefaultValue(1)]
    public int GetRowSpan(WorkspaceItem item)
    {
      return this.tableLayout.GetRowSpan((Control) item);
    }

    /// <summary>Sets the column.</summary>
    /// <param name="item">The item.</param>
    /// <param name="column">The column.</param>
    public void SetColumn(WorkspaceItem item, int column)
    {
      if (!this.tableLayout.Controls.Contains((Control) item))
        this.tableLayout.Controls.Add((Control) item);
      this.tableLayout.SetColumn((Control) item, column);
    }

    /// <summary>Sets the column span.</summary>
    /// <param name="item">The item.</param>
    /// <param name="value">The value.</param>
    public void SetColumnSpan(WorkspaceItem item, int value)
    {
      if (!this.tableLayout.Controls.Contains((Control) item))
        this.tableLayout.Controls.Add((Control) item);
      this.tableLayout.SetColumnSpan((Control) item, value);
    }

    /// <summary>Sets the row.</summary>
    /// <param name="item">The item.</param>
    /// <param name="row">The row.</param>
    public void SetRow(WorkspaceItem item, int row)
    {
      if (!this.tableLayout.Controls.Contains((Control) item))
        this.tableLayout.Controls.Add((Control) item);
      this.tableLayout.SetRow((Control) item, row);
    }

    /// <summary>Sets the row span.</summary>
    /// <param name="item">The item.</param>
    /// <param name="value">The value.</param>
    public void SetRowSpan(WorkspaceItem item, int value)
    {
      if (!this.tableLayout.Controls.Contains((Control) item))
        this.tableLayout.Controls.Add((Control) item);
      this.tableLayout.SetRowSpan((Control) item, value);
    }
  }
}
