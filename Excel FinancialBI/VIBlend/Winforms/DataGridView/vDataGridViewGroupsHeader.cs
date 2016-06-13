// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.vDataGridViewGroupsHeader
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.DataGridView
{
  [Designer("VIBlend.WinForms.Controls.Design.vDataGridViewGroupingHeaderDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [Description("Displays DataGrid Grouping Headers")]
  public class vDataGridViewGroupsHeader : Control
  {
    private Padding headerItemsPadding = new Padding(3, 3, 3, 3);
    private bool renderRequired = true;
    private int itemIndent = 20;
    private bool showConnectingLines = true;
    private Color connectingLinesColor = Color.DarkGray;
    private bool allowDragDrop = true;
    private Color promptTextColor = Color.DarkGray;
    private Point initialPosition = Point.Empty;
    private Point initialItemsOffset = new Point(1, 1);
    private bool synchronizeDataGridViewTheme = true;
    private GridTheme theme = new GridTheme();
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private vDataGridView associatedDataGrid;
    private List<DataGridGroupHeaderItem> headerItems;
    private Form dragDropForm;
    private string promptText;
    private DataGridGroupHeaderItem dragItem;
    private bool isDragging;
    private HeaderGroupsAlignment groupsAlignment;

    /// <summary>Gets or sets the header items vertical layout.</summary>
    [Description("Gets or sets the header items vertical layout.")]
    [Category("Appearance")]
    public HeaderGroupsAlignment HeaderItemsAlignment
    {
      get
      {
        return this.groupsAlignment;
      }
      set
      {
        if (value == this.groupsAlignment)
          return;
        this.groupsAlignment = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the prompt text.</summary>
    /// <value>The prompt text.</value>
    [Description("Gets or sets the prompt text.")]
    [Category("Behavior")]
    public string PromptText
    {
      get
      {
        return this.promptText;
      }
      set
      {
        this.promptText = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the prompt text color.</summary>
    [Category("Appearance")]
    [DefaultValue(typeof (Color), "Color.DarkGray")]
    [Description("Gets or sets the prompt text color.")]
    public Color PromptTextColor
    {
      get
      {
        return this.promptTextColor;
      }
      set
      {
        this.promptTextColor = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to allow the items drag and drop.
    /// </summary>
    public bool AllowDragDrop
    {
      get
      {
        return this.allowDragDrop;
      }
      set
      {
        this.allowDragDrop = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show the header items connecting lines.
    /// </summary>
    [Description("Gets or sets a value indicating whether to show the header items connecting lines.")]
    [DefaultValue(true)]
    [Category("Appearance")]
    public bool ShowConnectingLines
    {
      get
      {
        return this.showConnectingLines;
      }
      set
      {
        if (value == this.showConnectingLines)
          return;
        this.showConnectingLines = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the color of the connecting lines.</summary>
    /// <value>The color of the connecting lines.</value>
    [Category("Appearance")]
    [DefaultValue(typeof (Color), "Color.DarkGray")]
    [Description("Gets or sets the color of the connecting lines.")]
    public Color ConnectingLinesColor
    {
      get
      {
        return this.connectingLinesColor;
      }
      set
      {
        if (!(value != this.connectingLinesColor))
          return;
        this.connectingLinesColor = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the header item indent.</summary>
    /// <value>The header item indent.</value>
    [Description("Gets or sets the header item indent.")]
    [Category("Appearance")]
    [DefaultValue(20)]
    public int HeaderItemIndent
    {
      get
      {
        return this.itemIndent;
      }
      set
      {
        if (value == this.itemIndent)
          return;
        this.itemIndent = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the header items padding.</summary>
    /// <value>The header items padding.</value>
    [DefaultValue(typeof (Padding), "3,3,3,3")]
    [Description("Gets or sets the header items padding.")]
    [Category("Appearance")]
    public Padding HeaderItemsPadding
    {
      get
      {
        return this.headerItemsPadding;
      }
      set
      {
        this.headerItemsPadding = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the associated data grid view.</summary>
    /// <value>The data grid view.</value>
    [Description("Gets or sets the associated data grid view.")]
    [Category("Behavior")]
    public vDataGridView DataGridView
    {
      get
      {
        return this.associatedDataGrid;
      }
      set
      {
        if (this.associatedDataGrid != null)
          this.UnwireEvents();
        this.associatedDataGrid = value;
        if (value == null)
          return;
        this.WireEvents();
        this.renderRequired = true;
        this.VIBlendTheme = value.VIBlendTheme;
        this.Theme = value.Theme;
        this.RenderGroups();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is theme synchronized.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is theme synchronized; otherwise, <c>false</c>.
    /// </value>
    [Browsable(false)]
    public bool IsThemeSynchronized
    {
      get
      {
        return this.synchronizeDataGridViewTheme;
      }
      set
      {
        this.synchronizeDataGridViewTheme = value;
      }
    }

    [Description("Gets or sets the grid theme")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Appearance")]
    public GridTheme Theme
    {
      get
      {
        return this.theme;
      }
      set
      {
        this.theme = value;
        this.Refresh();
      }
    }

    [Category("Appearance")]
    public VIBLEND_THEME VIBlendTheme
    {
      get
      {
        return this.defaultTheme;
      }
      set
      {
        try
        {
          this.Theme = GridTheme.GetDefaultTheme(value);
          this.defaultTheme = value;
          this.Invalidate();
        }
        catch (Exception ex)
        {
        }
      }
    }

    /// <summary>Gets or sets the initial items offset.</summary>
    /// <value>The initial items offset.</value>
    [DefaultValue(typeof (Point), "1,1")]
    [Description("Gets or sets the initial items offset.")]
    [Category("Appearance")]
    public Point InitialItemsOffset
    {
      get
      {
        return this.initialItemsOffset;
      }
      set
      {
        if (!(value != this.initialItemsOffset))
          return;
        this.initialItemsOffset = value;
        this.Invalidate();
      }
    }

    /// <summary>Occurs when the drag operation started.</summary>
    [Description("Occurs when the drag operation started.")]
    [Category("Action")]
    public event EventHandler<GroupHeaderDragEventArgs> DragStarted;

    /// <summary>Occurs when the drag operation is starting.</summary>
    [Category("Action")]
    [Description("Occurs when the drag operation is starting.")]
    public event EventHandler<GroupHeaderDragCancelEventArgs> DragStarting;

    /// <summary>Occurs when the Drag operation ended.</summary>
    [Category("Action")]
    [Description("Occurs when the Drag operation ended.")]
    public event EventHandler<GroupHeaderDragEventArgs> DragEnded;

    /// <summary>Occurs when the Drag operation is ending.</summary>
    [Category("Action")]
    [Description("Occurs when the Drag operation is ending.")]
    public event EventHandler<GroupHeaderDragCancelEventArgs> DragEnding;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.DataGridView.vDataGridViewGroupsHeader" /> class.
    /// </summary>
    public vDataGridViewGroupsHeader()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.EnableNotifyMessage | ControlStyles.OptimizedDoubleBuffer, true);
      this.headerItems = new List<DataGridGroupHeaderItem>();
      this.PromptText = "Drag a column here to group by this column.";
      this.BackColor = Color.White;
    }

    /// <summary>
    /// Raises the <see cref="E:DragStarted" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:VIBlend.WinForms.DataGridView.GroupHeaderDragEventArgs" /> instance containing the event data.</param>
    protected virtual void OnDragStarted(GroupHeaderDragEventArgs args)
    {
      if (this.DragStarted == null)
        return;
      this.DragStarted((object) this, args);
    }

    /// <summary>
    /// Raises the <see cref="E:DragStarting" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:VIBlend.WinForms.DataGridView.GroupHeaderDragCancelEventArgs" /> instance containing the event data.</param>
    protected virtual void OnDragStarting(GroupHeaderDragCancelEventArgs args)
    {
      if (this.DragStarting == null)
        return;
      this.DragStarting((object) this, args);
    }

    /// <summary>
    /// Raises the <see cref="E:DragEnded" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:VIBlend.WinForms.DataGridView.GroupHeaderDragEventArgs" /> instance containing the event data.</param>
    protected virtual void OnDragEnded(GroupHeaderDragEventArgs args)
    {
      if (this.DragEnded == null)
        return;
      this.DragEnded((object) this, args);
    }

    /// <summary>
    /// Raises the <see cref="E:DragEnding" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:VIBlend.WinForms.DataGridView.GroupHeaderDragCancelEventArgs" /> instance containing the event data.</param>
    protected virtual void OnDragEnding(GroupHeaderDragCancelEventArgs args)
    {
      if (this.DragEnding == null)
        return;
      this.DragEnding((object) this, args);
    }

    private Form GetDragDropForm(DataGridGroupHeaderItem headerItem)
    {
      if (headerItem == null)
        return (Form) null;
      if (this.dragDropForm == null)
      {
        this.dragDropForm = new Form();
        this.dragDropForm.ShowInTaskbar = false;
        this.dragDropForm.FormBorderStyle = FormBorderStyle.None;
        this.dragDropForm.Opacity = 0.01;
        this.dragDropForm.Paint += new PaintEventHandler(this.dragDropForm_Paint);
        this.dragDropForm.Size = headerItem.RenderBounds.Size;
        this.dragDropForm.MinimumSize = Size.Empty;
        this.dragDropForm.Shown += new EventHandler(this.dragDropForm_Shown);
      }
      return this.dragDropForm;
    }

    private void dragDropForm_Shown(object sender, EventArgs e)
    {
      this.dragDropForm.Opacity = 0.5;
      this.dragDropForm.Shown -= new EventHandler(this.dragDropForm_Shown);
    }

    private void dragDropForm_Paint(object sender, PaintEventArgs e)
    {
      int x = 0;
      int y = 0;
      HierarchyItemStyle hierarchyItemStyle = this.Theme.HierarchyItemStyleNormal;
      if (!this.Enabled)
        hierarchyItemStyle = this.Theme.HierarchyItemStyleDisabled;
      Rectangle rect = new Rectangle(x, y, this.dragItem.Bounds.Width, this.dragItem.Bounds.Height);
      Brush brush = hierarchyItemStyle.FillStyle.GetBrush(rect);
      e.Graphics.FillRectangle(brush, rect);
      using (Pen pen = new Pen(hierarchyItemStyle.BorderColor, 1f))
      {
        e.Graphics.DrawRectangle(pen, rect);
        StringFormat format = new StringFormat();
        format.Alignment = StringAlignment.Center;
        format.LineAlignment = StringAlignment.Center;
        using (SolidBrush solidBrush = new SolidBrush(this.ForeColor))
        {
          Rectangle rectangle = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
          e.Graphics.DrawString(this.dragItem.Text, this.Font, (Brush) solidBrush, (RectangleF) rectangle, format);
        }
      }
    }

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control" /> and its child controls and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      this.UnwireEvents();
    }

    /// <summary>Wires the events.</summary>
    public void WireEvents()
    {
      if (this.DataGridView == null)
        return;
      this.DataGridView.GroupingColumns.CollectionChanged += new EventHandler<CollectionChangedEventArgs>(this.GroupingColumns_CollectionChanged);
      this.DataGridView.PropertyChanged += new PropertyChangedEventHandler(this.DataGridView_PropertyChanged);
      this.DataGridView.HierarchyItemMouseClick += new vDataGridView.HierarchyItemMouseEventHandler(this.DataGridView_HierarchyItemMouseClick);
      this.DataGridView.HierarchyItemDrag += new vDataGridView.HierarchyItemDragEventHandler(this.DataGridView_HierarchyItemDrag);
    }

    private void DataGridView_HierarchyItemDrag(object sender, HierarchyItemDragEventArgs args)
    {
      this.Invalidate();
    }

    private void DataGridView_HierarchyItemMouseClick(object sender, HierarchyItemMouseEventArgs args)
    {
      this.Invalidate();
    }

    private void DataGridView_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!e.PropertyName.Equals("VIBlendTheme") || !this.IsThemeSynchronized)
        return;
      this.VIBlendTheme = this.DataGridView.VIBlendTheme;
    }

    /// <summary>Unwires the events.</summary>
    public void UnwireEvents()
    {
      if (this.DataGridView == null)
        return;
      this.DataGridView.GroupingColumns.CollectionChanged -= new EventHandler<CollectionChangedEventArgs>(this.GroupingColumns_CollectionChanged);
      this.DataGridView.PropertyChanged -= new PropertyChangedEventHandler(this.DataGridView_PropertyChanged);
      this.DataGridView.HierarchyItemMouseClick -= new vDataGridView.HierarchyItemMouseEventHandler(this.DataGridView_HierarchyItemMouseClick);
      this.DataGridView.HierarchyItemDrag -= new vDataGridView.HierarchyItemDragEventHandler(this.DataGridView_HierarchyItemDrag);
    }

    private void GroupingColumns_CollectionChanged(object sender, CollectionChangedEventArgs e)
    {
      if (this.DataGridView.suspendGroupingColumnsCollectionChanged)
        return;
      this.renderRequired = true;
      this.RenderGroups();
      this.DataGridView.Refresh();
      this.Invalidate();
    }

    /// <summary>Gets the column headers.</summary>
    /// <returns></returns>
    public List<DataGridGroupHeaderItem> GetColumnHeaders()
    {
      return this.headerItems;
    }

    /// <summary>Renders the groups.</summary>
    public virtual void RenderGroups()
    {
      if (this.headerItems == null || this.DataGridView == null || !this.renderRequired)
        return;
      this.headerItems.Clear();
      for (int index = 0; index < this.DataGridView.GroupingColumns.Count; ++index)
        this.headerItems.Add(new DataGridGroupHeaderItem(this.DataGridView.GroupingColumns[index], this));
      int val1 = 0;
      foreach (DataGridGroupHeaderItem headerItem in this.headerItems)
      {
        using (Graphics graphics = this.CreateGraphics())
        {
          SizeF sizeF = graphics.MeasureString(headerItem.Text, this.Font);
          sizeF.Width = sizeF.Width + (float) this.headerItemsPadding.Horizontal;
          sizeF.Height = sizeF.Height + (float) this.headerItemsPadding.Vertical;
          int num = headerItem.ShowCloseButton ? 18 : 0;
          headerItem.Bounds = new Rectangle(0, 0, (int) sizeF.Width + num, (int) sizeF.Height);
          switch (this.HeaderItemsAlignment)
          {
            case HeaderGroupsAlignment.Near:
              val1 = Math.Max(val1, headerItem.Bounds.Height);
              continue;
            case HeaderGroupsAlignment.Center:
              if (val1 == 0)
              {
                val1 = headerItem.Bounds.Height;
                continue;
              }
              val1 += headerItem.Bounds.Height / 2;
              continue;
            case HeaderGroupsAlignment.Far:
              val1 += headerItem.Bounds.Height;
              continue;
            default:
              continue;
          }
        }
      }
      if (val1 == 0)
        val1 = 23;
      this.Height = val1 + this.InitialItemsOffset.Y + 4;
      this.renderRequired = false;
    }

    private void DrawCloseButton(Graphics graphics, Rectangle bounds, Color color1, Color color2)
    {
      using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(bounds, color1, color1, LinearGradientMode.Vertical))
      {
        Rectangle rectangle = bounds;
        using (Pen pen = new Pen((Brush) linearGradientBrush))
        {
          int num1 = rectangle.X + rectangle.Width / 2 - 7;
          int num2 = num1 + 5;
          int y1 = rectangle.Y - 2 + rectangle.Height / 2;
          int y2 = y1 + 5;
          for (int index = 0; index < 2; ++index)
          {
            graphics.DrawLine(pen, num1 + 1 + index, y1, num2 + index, y2);
            graphics.DrawLine(pen, num2 + index, y1, num1 + 1 + index, y2);
          }
        }
      }
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      this.RenderContent(e);
      if (!this.ShowConnectingLines)
        return;
      this.RenderConnectingLines(e);
    }

    /// <summary>Renders the connecting lines.</summary>
    /// <param name="e">The <see cref="T:System.Windows.Forms.PaintEventArgs" /> instance containing the event data.</param>
    protected virtual void RenderConnectingLines(PaintEventArgs e)
    {
      if (this.DataGridView == null || this.headerItems.Count < 2)
        return;
      using (Pen pen = new Pen(this.ConnectingLinesColor))
      {
        for (int index = 0; index < this.headerItems.Count - 1; ++index)
        {
          DataGridGroupHeaderItem gridGroupHeaderItem1 = this.headerItems[index];
          DataGridGroupHeaderItem gridGroupHeaderItem2 = this.headerItems[index + 1];
          int num1 = 0;
          switch (this.HeaderItemsAlignment)
          {
            case HeaderGroupsAlignment.Near:
              int right1 = gridGroupHeaderItem1.RenderBounds.Right;
              int y1 = gridGroupHeaderItem1.RenderBounds.Bottom - gridGroupHeaderItem1.RenderBounds.Height / 2;
              num1 = right1;
              int num2 = y1;
              int left1 = gridGroupHeaderItem2.RenderBounds.Left;
              int y2_1 = num2;
              e.Graphics.DrawLine(pen, right1, y1, left1, y2_1);
              break;
            case HeaderGroupsAlignment.Center:
              int right2 = gridGroupHeaderItem1.RenderBounds.Right;
              int bottom1 = gridGroupHeaderItem1.RenderBounds.Bottom;
              int num3 = right2;
              int num4 = bottom1;
              int left2 = gridGroupHeaderItem2.RenderBounds.Left;
              int y2_2 = num4;
              e.Graphics.DrawLine(pen, right2, bottom1, num3, num4);
              e.Graphics.DrawLine(pen, num3, num4, left2, y2_2);
              break;
            case HeaderGroupsAlignment.Far:
              int x1 = gridGroupHeaderItem1.RenderBounds.Right - gridGroupHeaderItem1.RenderBounds.Width / 2;
              int bottom2 = gridGroupHeaderItem1.RenderBounds.Bottom;
              int num5 = x1;
              int num6 = bottom2 + gridGroupHeaderItem2.RenderBounds.Height / 2;
              int left3 = gridGroupHeaderItem2.RenderBounds.Left;
              int y2_3 = num6;
              e.Graphics.DrawLine(pen, x1, bottom2, num5, num6);
              e.Graphics.DrawLine(pen, num5, num6, left3, y2_3);
              break;
          }
        }
      }
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (this.DataGridView == null)
        return;
      foreach (DataGridGroupHeaderItem headerItem in this.headerItems)
      {
        if (headerItem.ShowCloseButton && headerItem.CloseButtonRenderBounds.Contains(e.Location))
        {
          this.DataGridView.GroupingColumns.Remove(headerItem.Column);
          return;
        }
      }
      if (!this.AllowDragDrop)
        return;
      foreach (DataGridGroupHeaderItem headerItem in this.headerItems)
      {
        if (headerItem.RenderBounds.Contains(e.Location))
        {
          this.dragItem = headerItem;
          this.initialPosition = e.Location;
          break;
        }
      }
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (this.AllowDragDrop)
      {
        if (this.initialPosition.X == 0 && this.initialPosition.Y == 0)
        {
          this.Invalidate();
          return;
        }
        if (!this.isDragging && (Math.Abs(this.initialPosition.X - e.Location.X) >= 3 || Math.Abs(this.initialPosition.Y - e.Location.Y) >= 3))
        {
          GroupHeaderDragCancelEventArgs args = new GroupHeaderDragCancelEventArgs(this.dragItem);
          this.OnDragStarting(args);
          if (args.Cancel)
          {
            this.Invalidate();
            return;
          }
          this.isDragging = true;
          this.OnDragStarted(new GroupHeaderDragEventArgs(this.dragItem));
        }
        if (this.isDragging)
        {
          Form dragDropForm = this.GetDragDropForm(this.dragItem);
          if (dragDropForm != null)
          {
            if (!dragDropForm.Visible)
            {
              dragDropForm.Show((IWin32Window) this);
              this.dragDropForm.Size = this.dragItem.Bounds.Size;
            }
            dragDropForm.Location = Cursor.Position;
          }
        }
      }
      this.Invalidate();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if (!this.isDragging)
      {
        this.isDragging = false;
        this.dragItem = (DataGridGroupHeaderItem) null;
        this.initialPosition = Point.Empty;
        foreach (DataGridGroupHeaderItem headerItem in this.headerItems)
        {
          if (headerItem.RenderBounds.Contains(e.Location) && headerItem.Column.SortMode != GridItemSortMode.NotSortable)
          {
            HierarchyItem associatedItem = this.GetAssociatedItem(headerItem);
            if (associatedItem != null)
            {
              this.DataGridView.ToggleSort(associatedItem);
              this.Invalidate();
              this.DataGridView.Invalidate();
            }
          }
        }
      }
      else
      {
        if (!this.AllowDragDrop || !this.isDragging || this.dragItem == null)
          return;
        GroupHeaderDragCancelEventArgs args = new GroupHeaderDragCancelEventArgs(this.dragItem);
        this.OnDragEnding(args);
        if (this.dragDropForm != null && this.dragDropForm.Visible)
        {
          this.dragDropForm.Hide();
          this.dragDropForm.Dispose();
          this.dragDropForm = (Form) null;
        }
        if (args.Cancel)
        {
          this.isDragging = false;
          this.dragItem = (DataGridGroupHeaderItem) null;
          this.initialPosition = Point.Empty;
        }
        else
        {
          int i = 0;
          if (this.DataGridView.ClientRectangle.Contains(this.DataGridView.PointToClient(Cursor.Position)))
          {
            this.DataGridView.GroupingColumns.Remove(this.dragItem.Column);
          }
          else
          {
            foreach (DataGridGroupHeaderItem headerItem in this.headerItems)
            {
              if (headerItem.RenderBounds.Contains(e.Location))
              {
                this.DataGridView.suspendGroupingColumnsCollectionChanged = true;
                if (this.DataGridView.GroupingColumns.IndexOf(this.dragItem.Column) < i)
                  ++i;
                this.DataGridView.GroupingColumns.Insert(this.dragItem.Column, i);
                for (int index = 0; index < this.DataGridView.GroupingColumns.Count; ++index)
                {
                  if (index != i && this.DataGridView.GroupingColumns[index].Equals((object) this.dragItem.Column))
                  {
                    this.DataGridView.suspendGroupingColumnsCollectionChanged = false;
                    this.DataGridView.GroupingColumns.RemoveAt(index);
                    break;
                  }
                }
                break;
              }
              ++i;
            }
          }
          this.OnDragEnded(new GroupHeaderDragEventArgs(this.dragItem));
          this.isDragging = false;
          this.dragItem = (DataGridGroupHeaderItem) null;
          this.initialPosition = Point.Empty;
          this.Refresh();
        }
      }
    }

    private void DrawSortingIndicator(Graphics g, Pen pen, SortingDirection sortingDirection, int left, int top)
    {
      if (sortingDirection == SortingDirection.Descending)
      {
        g.DrawLine(pen, left, top, left + 10, top);
        g.DrawLine(pen, left, top, left + 5, top + 5);
        g.DrawLine(pen, left + 5, top + 5, left + 10, top);
      }
      else
      {
        g.DrawLine(pen, left, top + 5, left + 10, top + 5);
        g.DrawLine(pen, left + 5, top, left + 10, top + 5);
        g.DrawLine(pen, left + 5, top, left, top + 5);
      }
    }

    /// <summary>Renders the content.</summary>
    /// <param name="e">The <see cref="T:System.Windows.Forms.PaintEventArgs" /> instance containing the event data.</param>
    protected virtual void RenderContent(PaintEventArgs e)
    {
      if (this.headerItems.Count == 0 && this.DataGridView != null && this.DataGridView.GroupingColumns.Count > 0)
      {
        this.renderRequired = true;
        this.RenderGroups();
      }
      else if (this.headerItems.Count == 0 && this.DataGridView != null && this.DataGridView.GroupingColumns.Count == 0)
      {
        using (SolidBrush solidBrush = new SolidBrush(this.PromptTextColor))
        {
          e.Graphics.DrawString(this.PromptText, this.Font, (Brush) solidBrush, (RectangleF) this.ClientRectangle, new StringFormat()
          {
            LineAlignment = StringAlignment.Center,
            Alignment = StringAlignment.Center
          });
          return;
        }
      }
      int x = this.initialItemsOffset.X;
      int y = this.initialItemsOffset.Y;
      foreach (DataGridGroupHeaderItem headerItem in this.headerItems)
      {
        ControlState controlState = ControlState.Normal;
        HierarchyItemStyle hierarchyItemStyle = this.Theme.HierarchyItemStyleNormal;
        if (!this.Enabled)
          hierarchyItemStyle = this.Theme.HierarchyItemStyleDisabled;
        bool drawSortIndicator = false;
        bool flag = this.InitializeDrawSortIndicator(headerItem, drawSortIndicator);
        int num1 = flag ? 14 : 0;
        Rectangle rect = new Rectangle(x, y, headerItem.Bounds.Width + num1, headerItem.Bounds.Height);
        x += rect.Width + this.itemIndent;
        switch (this.HeaderItemsAlignment)
        {
          case HeaderGroupsAlignment.Near:
            y = this.initialItemsOffset.Y;
            break;
          case HeaderGroupsAlignment.Center:
            y += headerItem.Bounds.Height / 2;
            break;
          case HeaderGroupsAlignment.Far:
            y += headerItem.Bounds.Height;
            break;
        }
        Point client1 = this.PointToClient(Cursor.Position);
        if (rect.Contains(client1))
          controlState = ControlState.Hover;
        Brush brush = (controlState == ControlState.Hover ? hierarchyItemStyle.FillStyleHighlight : hierarchyItemStyle.FillStyle).GetBrush(rect);
        e.Graphics.FillRectangle(brush, rect);
        Pen pen1 = new Pen(controlState == ControlState.Hover ? hierarchyItemStyle.BorderColorHighlighted : hierarchyItemStyle.BorderColor, 1f);
        e.Graphics.DrawRectangle(pen1, rect);
        StringFormat format = new StringFormat();
        format.Alignment = StringAlignment.Center;
        format.LineAlignment = StringAlignment.Center;
        using (SolidBrush solidBrush = new SolidBrush(this.ForeColor))
        {
          int num2 = headerItem.ShowCloseButton ? 18 : 0;
          Rectangle rectangle = new Rectangle(rect.X, rect.Y, rect.Width - num2 - this.Padding.Right - num1, rect.Height);
          if (num2 == 0)
            rectangle = new Rectangle(rect.X, rect.Y, rect.Width - num1, rect.Height);
          e.Graphics.DrawString(headerItem.Text, this.Font, (Brush) solidBrush, (RectangleF) rectangle, format);
        }
        if (headerItem.ShowCloseButton)
        {
          Rectangle bounds = new Rectangle(rect.Right - 18, rect.Y, 18, rect.Height);
          Point client2 = this.PointToClient(Cursor.Position);
          Color color = headerItem.CloseButtonColor;
          if (bounds.Contains(client2))
            color = headerItem.CloseButtonHighlightColor;
          this.DrawCloseButton(e.Graphics, bounds, color, color);
          headerItem.CloseButtonRenderBounds = bounds;
          if (flag)
          {
            using (Pen pen2 = new Pen(this.ForeColor))
            {
              int top = rect.Top + rect.Height / 2 - 3;
              this.DrawSortingIndicator(e.Graphics, pen2, this.DataGridView.RowsHierarchy.SortingDirection, bounds.Left - 14, top);
            }
          }
        }
        else if (flag)
        {
          using (Pen pen2 = new Pen(this.ForeColor))
          {
            int top = rect.Top + rect.Height / 2 - 3;
            this.DrawSortingIndicator(e.Graphics, pen2, this.DataGridView.ColumnsHierarchy.SortingDirection, rect.Right - 14, top);
          }
        }
        headerItem.RenderBounds = rect;
      }
    }

    private HierarchyItem GetAssociatedItem(DataGridGroupHeaderItem headerItem)
    {
      foreach (HierarchyItem hierarchyItem in this.DataGridView.ColumnsHierarchy.Items)
      {
        if (headerItem.Column.Text.Equals(hierarchyItem.Caption))
          return hierarchyItem;
      }
      return (HierarchyItem) null;
    }

    private bool InitializeDrawSortIndicator(DataGridGroupHeaderItem headerItem, bool drawSortIndicator)
    {
      if (headerItem.Column.SortMode != GridItemSortMode.NotSortable && this.DataGridView != null)
      {
        foreach (HierarchyItem hierarchyItem in this.DataGridView.ColumnsHierarchy.Items)
        {
          if (headerItem.Column.Text.Equals(hierarchyItem.Caption))
          {
            if (hierarchyItem.IsSortItem)
            {
              drawSortIndicator = true;
              break;
            }
            break;
          }
        }
      }
      return drawSortIndicator;
    }
  }
}
