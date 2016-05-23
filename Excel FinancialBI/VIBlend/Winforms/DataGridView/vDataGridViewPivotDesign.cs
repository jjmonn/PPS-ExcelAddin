// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.vDataGridViewPivotDesign
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using VIBlend.Utilities;
using VIBlend.WinForms.Controls;

namespace VIBlend.WinForms.DataGridView
{
  [Description("Represents a control that allows you to easily design the DataGrid Pivot Table.")]
  public class vDataGridViewPivotDesign : UserControl
  {
    private ContextMenuStrip menuStrip = new ContextMenuStrip();
    private PaintHelper helper = new PaintHelper();
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private vDataGridView associatedDataGrid;
    private Orientation areasOrientation;
    private bool isUpdating;
    private bool updateFromButton;
    private PivotDesignPanelLocalizationBase localization;
    private VIBlend.WinForms.Controls.ListItem activeItem;
    private vListBox activeListBox;
    private bool shouldUpdateTable;
    private Graphics dataAreaGraphics;
    private Graphics rowsAreaGraphics;
    private Graphics filterAreaGraphics;
    private Graphics columnsAreaGraphics;
    private Graphics boundFieldsGraphics;
    /// <summary>Required designer variable.</summary>
    private IContainer components;
    public SplitContainer splitContainer1;
    public vListBox listBoxBoundFields;
    public Label label1;
    public Label label2;
    public ImageList imageList1;
    private TableLayoutPanel tableLayoutPanel1;
    public vListBox listBoxDataArea;
    public vListBox listBoxColumnArea;
    public vListBox listBoxRowArea;
    public vCheckBox vCheckBox1;
    public vButton vButton1;
    public vButton vButton4;
    public vButton vButton3;
    public vButton vButton2;
    private TableLayoutPanel tableLayoutPanel2;
    private vStripsRenderer vStripsRenderer1;
    private vListBox listBoxFilterArea;
    private vButton vButton5;

    /// <summary>Gets or sets the areas orientation.</summary>
    /// <value>The areas orientation.</value>
    [Description("Gets or sets the areas orientation.")]
    [Category("Behavior")]
    public Orientation AreasOrientation
    {
      get
      {
        return this.areasOrientation;
      }
      set
      {
        this.areasOrientation = value;
        this.ApplyOrientation();
      }
    }

    /// <summary>Gets the localization.</summary>
    /// <value>The localization.</value>
    [Browsable(false)]
    public PivotDesignPanelLocalizationBase Localization
    {
      get
      {
        if (this.localization == null)
          this.localization = (PivotDesignPanelLocalizationBase) new PivotDesignPanelLocalization();
        return this.localization;
      }
      set
      {
        if (value == this.localization)
          return;
        this.localization = value;
      }
    }

    /// <summary>Gets or sets the associated data grid view.</summary>
    /// <value>The data grid view.</value>
    [Category("Behavior")]
    [Description("Gets or sets the associated data grid view.")]
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
        if (value != null)
        {
          this.VIBlendTheme = value.VIBlendTheme;
          this.WireEvents();
        }
        this.UpdateFields();
      }
    }

    /// <summary>Gets or sets the VIBlend Theme.</summary>
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
          this.defaultTheme = value;
          this.Invalidate();
          this.SetTheme(value);
        }
        catch (Exception ex)
        {
        }
      }
    }

    /// <summary>Occurs when the pivot table is updated.</summary>
    [Category("Action")]
    public event EventHandler PivotTableUpdated;

    /// <summary>Occurs when text for the pivot value field is needed.</summary>
    [Category("Action")]
    public event PivotValueFieldTextNeededEventHandler PivotValueFieldTextNeeded;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.DataGridView.vDataGridViewPivotDesign" /> class.
    /// </summary>
    public vDataGridViewPivotDesign()
    {
      this.InitializeComponent();
      this.listBoxDataArea.AllowDragDrop = true;
      this.listBoxRowArea.AllowDragDrop = true;
      this.listBoxColumnArea.AllowDragDrop = true;
      this.listBoxBoundFields.AllowDragDrop = true;
      this.listBoxFilterArea.AllowDragDrop = true;
      this.listBoxDataArea.ItemDragEnded += new EventHandler<ListItemDragEventArgs>(this.listBoxDataArea_ItemDragEnded);
      this.listBoxDataArea.ItemDragEnding += new EventHandler<ListItemDragCancelEventArgs>(this.listBoxDataArea_ItemDragEnding);
      this.listBoxDataArea.ItemDragging += new EventHandler<ListItemDragEventArgs>(this.listBoxDataArea_ItemDragging);
      this.listBoxDataArea.ItemDragStarting += new EventHandler<ListItemDragCancelEventArgs>(this.listBoxDataArea_ItemDragStarting);
      this.listBoxDataArea.MouseLeave += new EventHandler(this.listBoxDataArea_MouseLeave);
      this.listBoxDataArea.MouseDown += new MouseEventHandler(this.listBoxDataArea_MouseDown);
      this.listBoxRowArea.ItemDragEnded += new EventHandler<ListItemDragEventArgs>(this.listBoxRowArea_ItemDragEnded);
      this.listBoxRowArea.ItemDragEnding += new EventHandler<ListItemDragCancelEventArgs>(this.listBoxRowArea_ItemDragEnding);
      this.listBoxRowArea.ItemDragStarting += new EventHandler<ListItemDragCancelEventArgs>(this.listBoxRowArea_ItemDragStarting);
      this.listBoxRowArea.ItemDragging += new EventHandler<ListItemDragEventArgs>(this.listBoxRowArea_ItemDragging);
      this.listBoxRowArea.MouseDown += new MouseEventHandler(this.listBoxRowArea_MouseDown);
      this.listBoxRowArea.MouseLeave += new EventHandler(this.listBoxRowArea_MouseLeave);
      this.listBoxColumnArea.ItemDragEnded += new EventHandler<ListItemDragEventArgs>(this.listBoxColumnArea_ItemDragEnded);
      this.listBoxColumnArea.ItemDragEnding += new EventHandler<ListItemDragCancelEventArgs>(this.listBoxColumnArea_ItemDragEnding);
      this.listBoxColumnArea.ItemDragging += new EventHandler<ListItemDragEventArgs>(this.listBoxColumnArea_ItemDragging);
      this.listBoxColumnArea.ItemDragStarting += new EventHandler<ListItemDragCancelEventArgs>(this.listBoxColumnArea_ItemDragStarting);
      this.listBoxColumnArea.MouseDown += new MouseEventHandler(this.listBoxColumnArea_MouseDown);
      this.listBoxColumnArea.MouseLeave += new EventHandler(this.listBoxColumnArea_MouseLeave);
      this.listBoxFilterArea.ItemDragEnded += new EventHandler<ListItemDragEventArgs>(this.listBoxFilterArea_ItemDragEnded);
      this.listBoxFilterArea.ItemDragEnding += new EventHandler<ListItemDragCancelEventArgs>(this.listBoxFilterArea_ItemDragEnding);
      this.listBoxFilterArea.ItemDragging += new EventHandler<ListItemDragEventArgs>(this.listBoxFilterArea_ItemDragging);
      this.listBoxFilterArea.ItemDragStarting += new EventHandler<ListItemDragCancelEventArgs>(this.listBoxFilterArea_ItemDragStarting);
      this.listBoxFilterArea.MouseDown += new MouseEventHandler(this.listBoxFilterArea_MouseDown);
      this.listBoxFilterArea.MouseLeave += new EventHandler(this.listBoxFilterArea_MouseLeave);
      this.listBoxBoundFields.ItemDragStarting += new EventHandler<ListItemDragCancelEventArgs>(this.listBoxBoundFields_ItemDragStarting);
      this.listBoxBoundFields.ItemDragging += new EventHandler<ListItemDragEventArgs>(this.listBoxBoundFields_ItemDragging);
      this.listBoxBoundFields.ItemDragEnding += new EventHandler<ListItemDragCancelEventArgs>(this.listBoxBoundFields_ItemDragEnding);
      this.listBoxBoundFields.ItemDragEnded += new EventHandler<ListItemDragEventArgs>(this.listBoxBoundFields_ItemDragEnded);
      this.listBoxBoundFields.MouseLeave += new EventHandler(this.listBoxBoundFields_MouseLeave);
      this.listBoxColumnArea.DrawListItem += new DrawItemEventHandler(this.listBoxColumnArea_DrawListItem);
      this.listBoxRowArea.DrawListItem += new DrawItemEventHandler(this.listBoxColumnArea_DrawListItem);
      this.listBoxDataArea.DrawListItem += new DrawItemEventHandler(this.listBoxColumnArea_DrawListItem);
      this.listBoxFilterArea.DrawListItem += new DrawItemEventHandler(this.listBoxColumnArea_DrawListItem);
      this.menuStrip.Items.Add(this.Localization.GetString(PivotDesignLocalizationNames.MoveUpItem));
      this.menuStrip.Items.Add(this.Localization.GetString(PivotDesignLocalizationNames.MoveDownItem));
      this.menuStrip.Items.Add(this.Localization.GetString(PivotDesignLocalizationNames.MoveToRowAreaItem));
      this.menuStrip.Items.Add(this.Localization.GetString(PivotDesignLocalizationNames.MoveToColumnAreaItem));
      this.menuStrip.Items.Add(this.Localization.GetString(PivotDesignLocalizationNames.MoveToDataAreaItem));
      this.menuStrip.Items.Add(this.Localization.GetString(PivotDesignLocalizationNames.RemoveFieldItem));
      this.menuStrip.Items.Add(this.Localization.GetString(PivotDesignLocalizationNames.SelectItem));
      this.menuStrip.Items[0].Image = (Image) this.GetIconFromResource("icon_arrowup.ico").ToBitmap();
      this.menuStrip.Items[1].Image = (Image) this.GetIconFromResource("icon_arrowdown.ico").ToBitmap();
      this.menuStrip.Items[6].Image = (Image) this.GetIconFromResource("icon_filter.ico").ToBitmap();
      this.menuStrip.Items[5].Image = (Image) this.GetImageFromResource("icon_remove.png");
      ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(this.Localization.GetString(PivotDesignLocalizationNames.FunctionItem));
      this.menuStrip.Items.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) toolStripMenuItem
      });
      toolStripMenuItem.Enabled = true;
      ToolStripItem toolStripItem1 = toolStripMenuItem.DropDownItems.Add(this.Localization.GetString(PivotDesignLocalizationNames.AverageItem));
      ToolStripItem toolStripItem2 = toolStripMenuItem.DropDownItems.Add(this.Localization.GetString(PivotDesignLocalizationNames.CountItem));
      ToolStripItem toolStripItem3 = toolStripMenuItem.DropDownItems.Add(this.Localization.GetString(PivotDesignLocalizationNames.MaxItem));
      ToolStripItem toolStripItem4 = toolStripMenuItem.DropDownItems.Add(this.Localization.GetString(PivotDesignLocalizationNames.MinItem));
      ToolStripItem toolStripItem5 = toolStripMenuItem.DropDownItems.Add(this.Localization.GetString(PivotDesignLocalizationNames.ProductItem));
      ToolStripItem toolStripItem6 = toolStripMenuItem.DropDownItems.Add(this.Localization.GetString(PivotDesignLocalizationNames.SumItem));
      toolStripItem1.Click += new EventHandler(this.averageItem_Click);
      toolStripItem2.Click += new EventHandler(this.countItem_Click);
      toolStripItem3.Click += new EventHandler(this.maxItem_Click);
      toolStripItem4.Click += new EventHandler(this.minItem_Click);
      toolStripItem6.Click += new EventHandler(this.sumItem_Click);
      toolStripItem5.Click += new EventHandler(this.productItem_Click);
      this.menuStrip.ItemClicked += new ToolStripItemClickedEventHandler(this.menuStrip_ItemClicked);
    }

    /// <summary>Called when the pivot table is updated.</summary>
    protected virtual void OnPivotTableUpdated()
    {
      if (this.PivotTableUpdated == null)
        return;
      this.PivotTableUpdated((object) this, EventArgs.Empty);
    }

    /// <summary>Called when the pivot table is updated.</summary>
    protected virtual void OnPivotValueFieldTextNeeded(BoundValueField field)
    {
      if (this.PivotValueFieldTextNeeded != null)
      {
        PivotValueFieldTextNeededEventArgs e = new PivotValueFieldTextNeededEventArgs();
        e.Field = field;
        e.Handled = false;
        this.PivotValueFieldTextNeeded((object) this, e);
        if (e.Handled)
          return;
      }
      field.Text = string.Format("{0} of {1}", (object) field.Function.ToString(), (object) field.Text);
    }

    /// <summary>Applies the orientation.</summary>
    protected virtual void ApplyOrientation()
    {
      if (this.AreasOrientation == Orientation.Horizontal)
      {
        this.tableLayoutPanel2.RowCount = 2;
        this.tableLayoutPanel2.ColumnCount = 4;
        this.tableLayoutPanel2.RowStyles.Clear();
        this.tableLayoutPanel2.ColumnStyles.Clear();
        ColumnStyle columnStyle1 = new ColumnStyle(SizeType.Percent, 25f);
        ColumnStyle columnStyle2 = new ColumnStyle(SizeType.Percent, 25f);
        ColumnStyle columnStyle3 = new ColumnStyle(SizeType.Percent, 25f);
        ColumnStyle columnStyle4 = new ColumnStyle(SizeType.Percent, 25f);
        this.tableLayoutPanel2.ColumnStyles.Add(columnStyle1);
        this.tableLayoutPanel2.ColumnStyles.Add(columnStyle2);
        this.tableLayoutPanel2.ColumnStyles.Add(columnStyle3);
        this.tableLayoutPanel2.ColumnStyles.Add(columnStyle4);
        RowStyle rowStyle1 = new RowStyle(SizeType.Absolute, 24f);
        RowStyle rowStyle2 = new RowStyle(SizeType.Percent, 100f);
        this.tableLayoutPanel2.RowStyles.Add(rowStyle1);
        this.tableLayoutPanel2.RowStyles.Add(rowStyle2);
        this.tableLayoutPanel2.SetRow((Control) this.vButton2, 0);
        this.tableLayoutPanel2.SetRow((Control) this.vButton3, 0);
        this.tableLayoutPanel2.SetRow((Control) this.vButton4, 0);
        this.tableLayoutPanel2.SetRow((Control) this.vButton5, 0);
        this.tableLayoutPanel2.SetColumn((Control) this.vButton5, 0);
        this.tableLayoutPanel2.SetColumn((Control) this.vButton2, 1);
        this.tableLayoutPanel2.SetColumn((Control) this.vButton3, 2);
        this.tableLayoutPanel2.SetColumn((Control) this.vButton4, 3);
        this.tableLayoutPanel2.SetColumn((Control) this.listBoxFilterArea, 0);
        this.tableLayoutPanel2.SetColumn((Control) this.listBoxRowArea, 1);
        this.tableLayoutPanel2.SetColumn((Control) this.listBoxColumnArea, 2);
        this.tableLayoutPanel2.SetColumn((Control) this.listBoxDataArea, 3);
        this.tableLayoutPanel2.SetRow((Control) this.listBoxFilterArea, 1);
        this.tableLayoutPanel2.SetRow((Control) this.listBoxRowArea, 1);
        this.tableLayoutPanel2.SetRow((Control) this.listBoxColumnArea, 1);
        this.tableLayoutPanel2.SetRow((Control) this.listBoxDataArea, 1);
      }
      else
      {
        this.tableLayoutPanel2.RowCount = 8;
        this.tableLayoutPanel2.ColumnCount = 1;
        this.tableLayoutPanel2.RowStyles.Clear();
        this.tableLayoutPanel2.ColumnStyles.Clear();
        this.tableLayoutPanel2.SetColumn((Control) this.vButton2, 0);
        this.tableLayoutPanel2.SetColumn((Control) this.vButton3, 0);
        this.tableLayoutPanel2.SetColumn((Control) this.vButton4, 0);
        this.tableLayoutPanel2.SetColumn((Control) this.vButton5, 0);
        this.tableLayoutPanel2.SetColumn((Control) this.listBoxFilterArea, 0);
        this.tableLayoutPanel2.SetColumn((Control) this.listBoxRowArea, 0);
        this.tableLayoutPanel2.SetColumn((Control) this.listBoxColumnArea, 0);
        this.tableLayoutPanel2.SetColumn((Control) this.listBoxDataArea, 0);
        RowStyle rowStyle1 = new RowStyle(SizeType.Absolute, 24f);
        RowStyle rowStyle2 = new RowStyle(SizeType.Percent, 25f);
        RowStyle rowStyle3 = new RowStyle(SizeType.Absolute, 24f);
        RowStyle rowStyle4 = new RowStyle(SizeType.Percent, 25f);
        RowStyle rowStyle5 = new RowStyle(SizeType.Absolute, 24f);
        RowStyle rowStyle6 = new RowStyle(SizeType.Percent, 25f);
        RowStyle rowStyle7 = new RowStyle(SizeType.Absolute, 24f);
        RowStyle rowStyle8 = new RowStyle(SizeType.Percent, 25f);
        this.tableLayoutPanel2.RowStyles.Add(rowStyle1);
        this.tableLayoutPanel2.RowStyles.Add(rowStyle2);
        this.tableLayoutPanel2.RowStyles.Add(rowStyle3);
        this.tableLayoutPanel2.RowStyles.Add(rowStyle4);
        this.tableLayoutPanel2.RowStyles.Add(rowStyle5);
        this.tableLayoutPanel2.RowStyles.Add(rowStyle6);
        this.tableLayoutPanel2.RowStyles.Add(rowStyle7);
        this.tableLayoutPanel2.RowStyles.Add(rowStyle8);
        this.tableLayoutPanel2.SetRow((Control) this.vButton2, 0);
        this.tableLayoutPanel2.SetRow((Control) this.vButton3, 2);
        this.tableLayoutPanel2.SetRow((Control) this.vButton4, 4);
        this.tableLayoutPanel2.SetRow((Control) this.vButton5, 6);
        this.tableLayoutPanel2.SetRow((Control) this.listBoxRowArea, 1);
        this.tableLayoutPanel2.SetRow((Control) this.listBoxColumnArea, 3);
        this.tableLayoutPanel2.SetRow((Control) this.listBoxDataArea, 5);
        this.tableLayoutPanel2.SetRow((Control) this.listBoxFilterArea, 7);
      }
    }

    private Bitmap GetImageFromResource(string imageName)
    {
      Stream stream = (Stream) null;
      try
      {
        stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("VIBlend.WinForms.DataGridView.Resources." + imageName);
        if (stream != null)
        {
          Bitmap bitmap = new Bitmap(stream);
          if (bitmap != null)
            return bitmap;
        }
      }
      catch (Exception ex)
      {
      }
      finally
      {
        if (stream != null)
          stream.Close();
      }
      return (Bitmap) null;
    }

    private Icon GetIconFromResource(string iconName)
    {
      Stream stream = (Stream) null;
      try
      {
        stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("VIBlend.WinForms.DataGridView.Resources." + iconName);
        if (stream != null)
        {
          Icon icon = new Icon(stream);
          if (icon != null)
            return icon;
        }
      }
      catch (Exception ex)
      {
      }
      finally
      {
        if (stream != null)
          stream.Close();
      }
      return (Icon) null;
    }

    private void listBoxRowArea_MouseLeave(object sender, EventArgs e)
    {
      this.InvalidateListBoxes();
    }

    private void listBoxColumnArea_MouseLeave(object sender, EventArgs e)
    {
      this.InvalidateListBoxes();
    }

    private void listBoxBoundFields_MouseLeave(object sender, EventArgs e)
    {
      this.InvalidateListBoxes();
    }

    private void listBoxFilterArea_MouseLeave(object sender, EventArgs e)
    {
      this.InvalidateListBoxes();
    }

    private void listBoxBoundFields_ItemDragEnded(object sender, ListItemDragEventArgs e)
    {
      if (!this.shouldUpdateTable)
        return;
      this.UpdatePivotTable();
    }

    private void listBoxDataArea_ItemDragEnded(object sender, ListItemDragEventArgs e)
    {
      if (!this.shouldUpdateTable)
        return;
      this.UpdatePivotTable();
    }

    private void listBoxRowArea_ItemDragEnded(object sender, ListItemDragEventArgs e)
    {
      if (!this.shouldUpdateTable)
        return;
      this.UpdatePivotTable();
    }

    private void listBoxFilterArea_ItemDragEnded(object sender, ListItemDragEventArgs e)
    {
      if (!this.shouldUpdateTable)
        return;
      this.UpdatePivotTable();
    }

    private void listBoxColumnArea_ItemDragEnded(object sender, ListItemDragEventArgs e)
    {
      if (!this.shouldUpdateTable)
        return;
      this.UpdatePivotTable();
    }

    private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {
      int num1 = this.activeListBox.Items.IndexOf(this.activeItem);
      if (e.ClickedItem.Text.Equals(this.Localization.GetString(PivotDesignLocalizationNames.MoveUpItem)) && num1 > 0 && this.activeListBox.Items.Count > 1)
      {
        this.activeListBox.Items.Remove(this.activeItem);
        this.activeListBox.Items.Insert(this.activeItem, --num1);
        this.activeListBox.SelectedItems.Clear();
        this.activeListBox.SelectedItem = this.activeItem;
        this.UpdatePivotTable();
      }
      if (e.ClickedItem.Text.Equals(this.Localization.GetString(PivotDesignLocalizationNames.SelectItem)))
      {
        if (this.activeItem == null)
          return;
        BoundField boundField = this.activeItem.Value as BoundField;
        if (boundField == null)
          return;
        this.DataGridView.ShowFilterForm(boundField);
      }
      else if (e.ClickedItem.Text.Equals(this.Localization.GetString(PivotDesignLocalizationNames.MoveDownItem)))
      {
        if (num1 < 0 || this.activeListBox.Items.Count <= 1 || num1 > this.activeListBox.Items.Count - 2)
          return;
        this.activeListBox.Items.Remove(this.activeItem);
        int num2;
        this.activeListBox.Items.Insert(this.activeItem, num2 = num1 + 1);
        this.activeListBox.SelectedItems.Clear();
        this.activeListBox.SelectedItem = this.activeItem;
        this.UpdatePivotTable();
      }
      else if (e.ClickedItem.Text.Equals("Move to Beginning"))
      {
        if (num1 <= 0)
          return;
        this.activeListBox.Items.Remove(this.activeItem);
        this.activeListBox.Items.Insert(this.activeItem, 0);
        this.activeListBox.SelectedItems.Clear();
        this.activeListBox.SelectedItem = this.activeItem;
        this.UpdatePivotTable();
      }
      else if (e.ClickedItem.Text.Equals("Move to End"))
      {
        if (num1 < 0)
          return;
        this.activeListBox.Items.Remove(this.activeItem);
        this.activeListBox.Items.Add(this.activeItem);
        this.activeListBox.SelectedItems.Clear();
        this.activeListBox.SelectedItem = this.activeItem;
        this.UpdatePivotTable();
      }
      else if (e.ClickedItem.Text.Equals(this.Localization.GetString(PivotDesignLocalizationNames.MoveToRowAreaItem)))
      {
        if (this.activeListBox == this.listBoxRowArea || this.ContainsDataField(this.listBoxRowArea, ((BoundField) this.activeItem.Value).DataField))
          return;
        if (this.activeListBox != this.listBoxBoundFields)
          this.activeListBox.Items.Remove(this.activeItem);
        this.listBoxRowArea.Items.Add(this.activeItem);
        this.listBoxRowArea.SelectedItems.Clear();
        this.listBoxRowArea.SelectedItem = this.activeItem;
        this.UpdatePivotTable();
      }
      else if (e.ClickedItem.Text.Equals(this.Localization.GetString(PivotDesignLocalizationNames.MoveToColumnAreaItem)))
      {
        if (this.activeListBox == this.listBoxColumnArea || this.ContainsDataField(this.listBoxColumnArea, ((BoundField) this.activeItem.Value).DataField))
          return;
        if (this.activeListBox != this.listBoxBoundFields)
          this.activeListBox.Items.Remove(this.activeItem);
        this.listBoxColumnArea.Items.Add(this.activeItem);
        this.listBoxColumnArea.SelectedItems.Clear();
        this.listBoxColumnArea.SelectedItem = this.activeItem;
        this.UpdatePivotTable();
      }
      else if (e.ClickedItem.Text.Equals(this.Localization.GetString(PivotDesignLocalizationNames.MoveToDataAreaItem)))
      {
        if (this.activeListBox == this.listBoxDataArea)
          return;
        if (this.activeListBox != this.listBoxBoundFields)
          this.activeListBox.Items.Remove(this.activeItem);
        this.listBoxDataArea.Items.Add(this.activeItem);
        this.listBoxDataArea.SelectedItems.Clear();
        this.listBoxDataArea.SelectedItem = this.activeItem;
        this.UpdatePivotTable();
      }
      else
      {
        if (!e.ClickedItem.Text.Equals(this.Localization.GetString(PivotDesignLocalizationNames.RemoveFieldItem)) || this.activeListBox == this.listBoxBoundFields)
          return;
        this.activeListBox.Items.Remove(this.activeItem);
        if (!this.ContainsDataField(this.listBoxBoundFields, ((BoundField) this.activeItem.Value).DataField))
        {
          this.listBoxBoundFields.Items.Add(this.activeItem);
          this.listBoxBoundFields.SelectedItems.Clear();
          this.listBoxBoundFields.SelectedItem = this.activeItem;
        }
        this.UpdatePivotTable();
      }
    }

    private void productItem_Click(object sender, EventArgs e)
    {
      ((BoundValueField) this.activeItem.Value).Function = PivotFieldFunction.Product;
      this.UpdatePivotTable();
    }

    private void sumItem_Click(object sender, EventArgs e)
    {
      ((BoundValueField) this.activeItem.Value).Function = PivotFieldFunction.Sum;
      this.UpdatePivotTable();
    }

    private void maxItem_Click(object sender, EventArgs e)
    {
      ((BoundValueField) this.activeItem.Value).Function = PivotFieldFunction.Max;
      this.UpdatePivotTable();
    }

    private void minItem_Click(object sender, EventArgs e)
    {
      ((BoundValueField) this.activeItem.Value).Function = PivotFieldFunction.Min;
      this.UpdatePivotTable();
    }

    private void countItem_Click(object sender, EventArgs e)
    {
      ((BoundValueField) this.activeItem.Value).Function = PivotFieldFunction.Count;
      this.UpdatePivotTable();
    }

    private void averageItem_Click(object sender, EventArgs e)
    {
      ((BoundValueField) this.activeItem.Value).Function = PivotFieldFunction.Average;
      this.UpdatePivotTable();
    }

    private void listBoxColumnArea_MouseDown(object sender, MouseEventArgs e)
    {
      VIBlend.WinForms.Controls.ListItem listItem = this.listBoxColumnArea.FindItem(e.Location);
      if (listItem == null || !new Rectangle(listItem.RenderBounds.Right - 10, listItem.RenderBounds.Y, 10, listItem.RenderBounds.Height).Contains(e.Location))
        return;
      this.activeItem = listItem;
      this.activeListBox = this.listBoxColumnArea;
      this.menuStrip.Items[this.menuStrip.Items.Count - 1].Visible = false;
      this.menuStrip.Show(Cursor.Position);
    }

    private void listBoxFilterArea_MouseDown(object sender, MouseEventArgs e)
    {
      VIBlend.WinForms.Controls.ListItem listItem = this.listBoxFilterArea.FindItem(e.Location);
      if (listItem == null || !new Rectangle(listItem.RenderBounds.Right - 10, listItem.RenderBounds.Y, 10, listItem.RenderBounds.Height).Contains(e.Location))
        return;
      this.activeItem = listItem;
      this.activeListBox = this.listBoxFilterArea;
      this.menuStrip.Items[this.menuStrip.Items.Count - 1].Visible = false;
      this.menuStrip.Show(Cursor.Position);
    }

    private void listBoxRowArea_MouseDown(object sender, MouseEventArgs e)
    {
      VIBlend.WinForms.Controls.ListItem listItem = this.listBoxRowArea.FindItem(e.Location);
      if (listItem == null || !new Rectangle(listItem.RenderBounds.Right - 10, listItem.RenderBounds.Y, 10, listItem.RenderBounds.Height).Contains(e.Location))
        return;
      this.activeItem = listItem;
      this.activeListBox = this.listBoxRowArea;
      this.menuStrip.Items[this.menuStrip.Items.Count - 1].Visible = false;
      this.menuStrip.Show(Cursor.Position);
    }

    private void listBoxDataArea_MouseDown(object sender, MouseEventArgs e)
    {
      VIBlend.WinForms.Controls.ListItem listItem = this.listBoxDataArea.FindItem(e.Location);
      if (listItem == null || !new Rectangle(listItem.RenderBounds.Right - 10, listItem.RenderBounds.Y, 10, listItem.RenderBounds.Height).Contains(e.Location))
        return;
      this.activeItem = listItem;
      this.activeListBox = this.listBoxDataArea;
      ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem) this.menuStrip.Items[this.menuStrip.Items.Count - 1];
      foreach (ToolStripMenuItem dropDownItem in (ArrangedElementCollection) toolStripMenuItem.DropDownItems)
        dropDownItem.Checked = false;
      switch (((BoundValueField) this.activeItem.Value).Function)
      {
        case PivotFieldFunction.Sum:
          (toolStripMenuItem.DropDownItems[5] as ToolStripMenuItem).Checked = true;
          break;
        case PivotFieldFunction.Count:
          (toolStripMenuItem.DropDownItems[1] as ToolStripMenuItem).Checked = true;
          break;
        case PivotFieldFunction.Average:
          (toolStripMenuItem.DropDownItems[0] as ToolStripMenuItem).Checked = true;
          break;
        case PivotFieldFunction.Max:
          (toolStripMenuItem.DropDownItems[2] as ToolStripMenuItem).Checked = true;
          break;
        case PivotFieldFunction.Min:
          (toolStripMenuItem.DropDownItems[3] as ToolStripMenuItem).Checked = true;
          break;
        case PivotFieldFunction.Product:
          (toolStripMenuItem.DropDownItems[4] as ToolStripMenuItem).Checked = true;
          break;
      }
      this.menuStrip.Items[this.menuStrip.Items.Count - 1].Visible = true;
      this.menuStrip.Show(Cursor.Position);
    }

    private void listBoxColumnArea_DrawListItem(object sender, DrawItemEventArgs e)
    {
      vListBox vListBox = sender as vListBox;
      vListBox.DrawItem(e.Graphics, e.Bounds, e.State, vListBox.Items[e.Index]);
      Rectangle bounds = new Rectangle(e.Bounds.Right - 7, e.Bounds.Top + e.Bounds.Height / 2 - 1, 5, 3);
      this.helper.DrawArrowFigure(e.Graphics, Color.Black, bounds, ArrowDirection.Down);
    }

    private void listBoxBoundFields_ItemDragEnding(object sender, ListItemDragCancelEventArgs e)
    {
      this.shouldUpdateTable = false;
      this.StopListBoxTimers();
      vListBox listBoxAtPosition = this.GetListBoxAtPosition();
      if (listBoxAtPosition == null || listBoxAtPosition == this.listBoxBoundFields)
        return;
      this.shouldUpdateTable = true;
      if ((listBoxAtPosition == this.listBoxFilterArea || listBoxAtPosition == this.listBoxRowArea || listBoxAtPosition == this.listBoxColumnArea) && (this.ContainsDataField(this.listBoxRowArea, ((BoundField) e.SourceItem.Value).DataField) || this.ContainsDataField(this.listBoxColumnArea, ((BoundField) e.SourceItem.Value).DataField) || (this.ContainsDataField(this.listBoxDataArea, ((BoundField) e.SourceItem.Value).DataField) || this.ContainsDataField(this.listBoxFilterArea, ((BoundField) e.SourceItem.Value).DataField))))
        return;
      vListBox.DropItem(this.listBoxBoundFields, listBoxAtPosition, e.SourceItem, ListBoxDropType.Clone);
    }

    private void listBoxBoundFields_ItemDragging(object sender, ListItemDragEventArgs e)
    {
      bool flag1 = this.DoDragging(this.listBoxDataArea, this.dataAreaGraphics);
      bool flag2 = this.DoDragging(this.listBoxColumnArea, this.columnsAreaGraphics);
      bool flag3 = this.DoDragging(this.listBoxRowArea, this.rowsAreaGraphics);
      this.DoDragging(this.listBoxRowArea, this.filterAreaGraphics);
      if (flag3 || flag2 || flag1)
        return;
      this.InvalidateListBoxes();
    }

    private void listBoxBoundFields_ItemDragStarting(object sender, ListItemDragCancelEventArgs e)
    {
      this.StartListBoxTimers();
    }

    private void listBoxFilterArea_ItemDragStarting(object sender, ListItemDragCancelEventArgs e)
    {
      this.StartListBoxTimers();
    }

    private void StartListBoxTimers()
    {
      this.listBoxBoundFields.StartDraggingTimer();
      this.listBoxDataArea.StartDraggingTimer();
      this.listBoxRowArea.StartDraggingTimer();
      this.listBoxColumnArea.StartDraggingTimer();
      this.listBoxFilterArea.StartDraggingTimer();
    }

    private void listBoxFilterArea_ItemDragging(object sender, ListItemDragEventArgs e)
    {
      bool flag1 = this.DoDragging(this.listBoxDataArea, this.dataAreaGraphics);
      bool flag2 = this.DoDragging(this.listBoxRowArea, this.rowsAreaGraphics);
      bool flag3 = this.DoDragging(this.listBoxBoundFields, this.boundFieldsGraphics);
      bool flag4 = this.DoDragging(this.listBoxColumnArea, this.columnsAreaGraphics);
      if (flag2 || flag3 || (flag1 || flag4))
        return;
      this.InvalidateListBoxes();
    }

    private void InvalidateListBoxes()
    {
      this.listBoxRowArea.Invalidate();
      this.listBoxColumnArea.Invalidate();
      this.listBoxDataArea.Invalidate();
      this.listBoxBoundFields.Invalidate();
      this.listBoxFilterArea.Invalidate();
    }

    private void listBoxFilterArea_ItemDragEnding(object sender, ListItemDragCancelEventArgs e)
    {
      this.shouldUpdateTable = false;
      this.StopListBoxTimers();
      vListBox listBoxAtPosition = this.GetListBoxAtPosition();
      if (listBoxAtPosition != null && listBoxAtPosition != this.listBoxFilterArea)
      {
        this.shouldUpdateTable = true;
        ListBoxDropType dropType = ListBoxDropType.Default;
        if (this.ContainsDataField(listBoxAtPosition, ((BoundField) e.SourceItem.Value).DataField) && listBoxAtPosition != this.listBoxDataArea)
          dropType = ListBoxDropType.Remove;
        vListBox.DropItem(this.listBoxFilterArea, listBoxAtPosition, e.SourceItem, dropType);
      }
      else
      {
        if (listBoxAtPosition != this.listBoxFilterArea)
          return;
        this.shouldUpdateTable = true;
      }
    }

    private void StopListBoxTimers()
    {
      this.listBoxBoundFields.StopDraggingTimer();
      this.listBoxDataArea.StopDraggingTimer();
      this.listBoxRowArea.StopDraggingTimer();
      this.listBoxColumnArea.StopDraggingTimer();
      this.listBoxFilterArea.StopDraggingTimer();
    }

    private void listBoxColumnArea_ItemDragStarting(object sender, ListItemDragCancelEventArgs e)
    {
      this.StartListBoxTimers();
    }

    private void listBoxColumnArea_ItemDragging(object sender, ListItemDragEventArgs e)
    {
      bool flag1 = this.DoDragging(this.listBoxDataArea, this.dataAreaGraphics);
      bool flag2 = this.DoDragging(this.listBoxRowArea, this.rowsAreaGraphics);
      bool flag3 = this.DoDragging(this.listBoxBoundFields, this.boundFieldsGraphics);
      bool flag4 = this.DoDragging(this.listBoxFilterArea, this.filterAreaGraphics);
      if (flag2 || flag3 || (flag1 || flag4))
        return;
      this.InvalidateListBoxes();
    }

    private void listBoxColumnArea_ItemDragEnding(object sender, ListItemDragCancelEventArgs e)
    {
      this.shouldUpdateTable = false;
      this.StopListBoxTimers();
      vListBox listBoxAtPosition = this.GetListBoxAtPosition();
      if (listBoxAtPosition != null && listBoxAtPosition != this.listBoxColumnArea)
      {
        this.shouldUpdateTable = true;
        ListBoxDropType dropType = ListBoxDropType.Default;
        if (this.ContainsDataField(listBoxAtPosition, ((BoundField) e.SourceItem.Value).DataField) && listBoxAtPosition != this.listBoxDataArea)
          dropType = ListBoxDropType.Remove;
        vListBox.DropItem(this.listBoxColumnArea, listBoxAtPosition, e.SourceItem, dropType);
      }
      else
      {
        if (listBoxAtPosition != this.listBoxColumnArea)
          return;
        this.shouldUpdateTable = true;
      }
    }

    private void listBoxRowArea_ItemDragging(object sender, ListItemDragEventArgs e)
    {
      bool flag1 = this.DoDragging(this.listBoxDataArea, this.dataAreaGraphics);
      bool flag2 = this.DoDragging(this.listBoxColumnArea, this.columnsAreaGraphics);
      bool flag3 = this.DoDragging(this.listBoxBoundFields, this.boundFieldsGraphics);
      bool flag4 = this.DoDragging(this.listBoxFilterArea, this.boundFieldsGraphics);
      if (flag2 || flag3 || (flag1 || flag4))
        return;
      this.InvalidateListBoxes();
    }

    private void listBoxRowArea_ItemDragStarting(object sender, ListItemDragCancelEventArgs e)
    {
      this.listBoxBoundFields.StartDraggingTimer();
      this.listBoxDataArea.StartDraggingTimer();
      this.listBoxRowArea.StartDraggingTimer();
      this.listBoxColumnArea.StartDraggingTimer();
      this.listBoxFilterArea.StartDraggingTimer();
    }

    private void listBoxRowArea_ItemDragEnding(object sender, ListItemDragCancelEventArgs e)
    {
      this.shouldUpdateTable = false;
      this.StopListBoxTimers();
      vListBox listBoxAtPosition = this.GetListBoxAtPosition();
      if (listBoxAtPosition != null && listBoxAtPosition != this.listBoxRowArea)
      {
        this.shouldUpdateTable = true;
        ListBoxDropType dropType = ListBoxDropType.Default;
        if (this.ContainsDataField(listBoxAtPosition, ((BoundField) e.SourceItem.Value).DataField) && listBoxAtPosition != this.listBoxDataArea)
          dropType = ListBoxDropType.Remove;
        vListBox.DropItem(this.listBoxRowArea, listBoxAtPosition, e.SourceItem, dropType);
      }
      else
      {
        if (listBoxAtPosition != this.listBoxRowArea)
          return;
        this.shouldUpdateTable = true;
      }
    }

    private void listBoxDataArea_MouseLeave(object sender, EventArgs e)
    {
      this.InvalidateListBoxes();
    }

    private void listBoxDataArea_ItemDragStarting(object sender, ListItemDragCancelEventArgs e)
    {
      this.StartListBoxTimers();
    }

    private bool DoDragging(vListBox listBox, Graphics graphics)
    {
      if (!listBox.RectangleToScreen(listBox.ClientRectangle).Contains(Cursor.Position))
        return false;
      if (graphics == null)
        graphics = listBox.CreateGraphics();
      listBox.PointToClient(Cursor.Position);
      vListBox.DrawDragFeedback(graphics, listBox);
      return true;
    }

    protected virtual vListBox GetListBoxAtPosition()
    {
      Rectangle screen1 = this.listBoxDataArea.RectangleToScreen(this.listBoxDataArea.ClientRectangle);
      Rectangle screen2 = this.listBoxColumnArea.RectangleToScreen(this.listBoxColumnArea.ClientRectangle);
      Rectangle screen3 = this.listBoxRowArea.RectangleToScreen(this.listBoxRowArea.ClientRectangle);
      Rectangle screen4 = this.listBoxBoundFields.RectangleToScreen(this.listBoxBoundFields.ClientRectangle);
      Rectangle screen5 = this.listBoxFilterArea.RectangleToScreen(this.listBoxFilterArea.ClientRectangle);
      if (screen1.Contains(Cursor.Position))
        return this.listBoxDataArea;
      if (screen2.Contains(Cursor.Position))
        return this.listBoxColumnArea;
      if (screen3.Contains(Cursor.Position))
        return this.listBoxRowArea;
      if (screen4.Contains(Cursor.Position))
        return this.listBoxBoundFields;
      if (screen5.Contains(Cursor.Position))
        return this.listBoxFilterArea;
      return (vListBox) null;
    }

    /// <summary>Updates the pivot table.</summary>
    public virtual void UpdatePivotTable()
    {
      if (this.vCheckBox1.Checked)
      {
        if (!this.updateFromButton)
          return;
      }
      try
      {
        this.BeginUpdate();
        this.DataGridView.BoundPivotColumns.Clear();
        this.DataGridView.BoundPivotRows.Clear();
        this.DataGridView.BoundPivotValues.Clear();
        this.DataGridView.BoundFieldsFilters.Clear();
        foreach (VIBlend.WinForms.Controls.ListItem listItem in this.listBoxColumnArea.Items)
        {
          if (listItem.Value != null)
          {
            if (listItem.Value is BoundValueField)
            {
              BoundValueField boundValueField = (BoundValueField) listItem.Value;
              listItem.Value = (object) new BoundField(listItem.Tag == null ? boundValueField.Text : listItem.Tag.ToString(), boundValueField.DataField);
              listItem.Text = (string) listItem.Tag;
            }
            this.DataGridView.BoundPivotColumns.Add((BoundField) listItem.Value);
          }
        }
        foreach (VIBlend.WinForms.Controls.ListItem listItem in this.listBoxRowArea.Items)
        {
          if (listItem.Value != null)
          {
            if (listItem.Value is BoundValueField)
            {
              BoundValueField boundValueField = (BoundValueField) listItem.Value;
              listItem.Value = (object) new BoundField(listItem.Tag == null ? boundValueField.Text : listItem.Tag.ToString(), boundValueField.DataField);
              listItem.Text = (string) listItem.Tag;
            }
            this.DataGridView.BoundPivotRows.Add((BoundField) listItem.Value);
          }
        }
        foreach (VIBlend.WinForms.Controls.ListItem listItem in this.listBoxFilterArea.Items)
        {
          if (listItem.Value != null)
          {
            if (listItem.Value is BoundValueField)
            {
              BoundValueField boundValueField = (BoundValueField) listItem.Value;
              listItem.Value = (object) new BoundField(listItem.Tag == null ? boundValueField.Text : listItem.Tag.ToString(), boundValueField.DataField);
              listItem.Text = (string) listItem.Tag;
            }
            this.DataGridView.BoundFieldsFilters.Add(new BoundFieldFilter()
            {
              DataField = ((BoundField) listItem.Value).DataField
            });
          }
        }
        foreach (VIBlend.WinForms.Controls.ListItem listItem in this.listBoxDataArea.Items)
        {
          if (listItem.Value != null)
          {
            BoundField boundField = (BoundField) listItem.Value;
            PivotFieldFunction function = PivotFieldFunction.Sum;
            if (listItem.Value is BoundValueField)
              function = ((BoundValueField) listItem.Value).Function;
            BoundValueField field = new BoundValueField((string) listItem.Tag, boundField.DataField, function);
            this.OnPivotValueFieldTextNeeded(field);
            listItem.Value = (object) field;
            listItem.Text = field.Text;
            this.DataGridView.BoundPivotValues.Add(field);
          }
        }
        this.listBoxBoundFields.Refresh();
        this.listBoxColumnArea.Refresh();
        this.listBoxRowArea.Refresh();
        this.listBoxDataArea.Refresh();
        this.listBoxFilterArea.Refresh();
        this.DataGridView.DataBind();
        this.DataGridView.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL);
        this.DataGridView.RowsHierarchy.AutoResize(AutoResizeMode.FIT_ITEM_CONTENT);
        this.DataGridView.Refresh();
        this.EndUpdate();
      }
      catch (Exception ex)
      {
      }
      this.OnPivotTableUpdated();
    }

    private void listBoxDataArea_ItemDragging(object sender, ListItemDragEventArgs e)
    {
      if (this.DoDragging(this.listBoxRowArea, this.rowsAreaGraphics) || this.DoDragging(this.listBoxColumnArea, this.columnsAreaGraphics) || this.DoDragging(this.listBoxBoundFields, this.boundFieldsGraphics))
        return;
      this.listBoxRowArea.Invalidate();
      this.listBoxColumnArea.Invalidate();
      this.listBoxDataArea.Invalidate();
      this.listBoxBoundFields.Invalidate();
    }

    private void listBoxDataArea_ItemDragEnding(object sender, ListItemDragCancelEventArgs e)
    {
      this.shouldUpdateTable = false;
      this.listBoxBoundFields.StopDraggingTimer();
      this.listBoxDataArea.StopDraggingTimer();
      this.listBoxRowArea.StopDraggingTimer();
      this.listBoxColumnArea.StopDraggingTimer();
      vListBox listBoxAtPosition = this.GetListBoxAtPosition();
      if (listBoxAtPosition != null && listBoxAtPosition != this.listBoxDataArea)
      {
        this.shouldUpdateTable = true;
        ListBoxDropType dropType = ListBoxDropType.Default;
        if (this.ContainsDataField(listBoxAtPosition, ((BoundField) e.SourceItem.Value).DataField))
          dropType = ListBoxDropType.Remove;
        vListBox.DropItem(this.listBoxDataArea, listBoxAtPosition, e.SourceItem, dropType);
      }
      else
      {
        if (listBoxAtPosition != this.listBoxDataArea)
          return;
        this.shouldUpdateTable = true;
      }
    }

    private void WireEvents()
    {
      if (this.DataGridView == null)
        return;
      this.DataGridView.BoundFields.CollectionChanged += new EventHandler<CollectionChangedEventArgs>(this.BoundFields_CollectionChanged);
      this.DataGridView.BoundPivotColumns.CollectionChanged += new EventHandler<CollectionChangedEventArgs>(this.BoundPivotColumns_CollectionChanged);
      this.DataGridView.BoundPivotRows.CollectionChanged += new EventHandler<CollectionChangedEventArgs>(this.BoundPivotRows_CollectionChanged);
      this.DataGridView.BoundPivotValues.CollectionChanged += new EventHandler<CollectionChangedEventArgs>(this.BoundPivotValues_CollectionChanged);
    }

    private void BoundPivotValues_CollectionChanged(object sender, CollectionChangedEventArgs e)
    {
      this.UpdateFields();
    }

    private void BoundPivotRows_CollectionChanged(object sender, CollectionChangedEventArgs e)
    {
      this.UpdateFields();
    }

    private void BoundPivotColumns_CollectionChanged(object sender, CollectionChangedEventArgs e)
    {
      this.UpdateFields();
    }

    private void BoundFields_CollectionChanged(object sender, CollectionChangedEventArgs e)
    {
      this.UpdateFields();
    }

    /// <summary>Begins the update.</summary>
    public void BeginUpdate()
    {
      this.isUpdating = true;
    }

    /// <summary>Ends the update.</summary>
    public void EndUpdate()
    {
      this.isUpdating = false;
    }

    private bool ContainsDataField(vListBox listBox, string dataField)
    {
      foreach (VIBlend.WinForms.Controls.ListItem listItem in listBox.Items)
      {
        if (listItem.Value != null && listItem.Value.GetType() == typeof (BoundField) && ((BoundField) listItem.Value).DataField == dataField)
          return true;
      }
      return false;
    }

    /// <summary>Updates the fields.</summary>
    public void UpdateFields()
    {
      if (this.isUpdating || this.DataGridView == null)
        return;
      this.listBoxBoundFields.Items.Clear();
      this.listBoxColumnArea.Items.Clear();
      this.listBoxDataArea.Items.Clear();
      this.listBoxRowArea.Items.Clear();
      foreach (BoundValueField boundPivotValue in this.DataGridView.BoundPivotValues)
      {
        VIBlend.WinForms.Controls.ListItem listItem = new VIBlend.WinForms.Controls.ListItem();
        listItem.Text = boundPivotValue.Text;
        listItem.Value = (object) boundPivotValue;
        listItem.Tag = (object) boundPivotValue.Text;
        foreach (BoundField boundField in this.DataGridView.BoundFields)
        {
          if (boundField.DataField == boundPivotValue.DataField)
          {
            listItem.Tag = (object) boundField.Text;
            break;
          }
        }
        this.listBoxDataArea.Items.Add(listItem);
      }
      foreach (BoundField boundPivotColumn in this.DataGridView.BoundPivotColumns)
      {
        if (!this.ContainsDataField(this.listBoxDataArea, boundPivotColumn.DataField) && !this.ContainsDataField(this.listBoxColumnArea, boundPivotColumn.DataField))
          this.listBoxColumnArea.Items.Add(new VIBlend.WinForms.Controls.ListItem()
          {
            Text = boundPivotColumn.Text,
            Tag = (object) boundPivotColumn.Text,
            Value = (object) boundPivotColumn
          });
      }
      foreach (BoundField boundPivotRow in this.DataGridView.BoundPivotRows)
      {
        if (!this.ContainsDataField(this.listBoxDataArea, boundPivotRow.DataField) && !this.ContainsDataField(this.listBoxColumnArea, boundPivotRow.DataField) && !this.ContainsDataField(this.listBoxRowArea, boundPivotRow.DataField))
          this.listBoxRowArea.Items.Add(new VIBlend.WinForms.Controls.ListItem()
          {
            Text = boundPivotRow.Text,
            Tag = (object) boundPivotRow.Text,
            Value = (object) boundPivotRow
          });
      }
      foreach (BoundField boundField in this.DataGridView.BoundFields)
      {
        if (!this.ContainsDataField(this.listBoxBoundFields, boundField.DataField))
          this.listBoxBoundFields.Items.Add(new VIBlend.WinForms.Controls.ListItem()
          {
            Text = boundField.Text,
            Tag = (object) boundField.Text,
            Value = (object) boundField
          });
      }
    }

    private void UnwireEvents()
    {
      if (this.DataGridView == null)
        return;
      this.DataGridView.BoundFields.CollectionChanged -= new EventHandler<CollectionChangedEventArgs>(this.BoundFields_CollectionChanged);
      this.DataGridView.BoundPivotColumns.CollectionChanged -= new EventHandler<CollectionChangedEventArgs>(this.BoundPivotColumns_CollectionChanged);
      this.DataGridView.BoundPivotRows.CollectionChanged -= new EventHandler<CollectionChangedEventArgs>(this.BoundPivotRows_CollectionChanged);
      this.DataGridView.BoundPivotValues.CollectionChanged -= new EventHandler<CollectionChangedEventArgs>(this.BoundPivotValues_CollectionChanged);
    }

    private void SetTheme(VIBLEND_THEME value)
    {
      this.vStripsRenderer1.VIBlendTheme = value;
      this.vStripsRenderer1.RenderedContextMenuStrip = this.menuStrip;
      this.vButton1.VIBlendTheme = value;
      this.vButton2.VIBlendTheme = value;
      this.vButton3.VIBlendTheme = value;
      this.vButton4.VIBlendTheme = value;
      this.listBoxFilterArea.VIBlendTheme = value;
      this.listBoxColumnArea.VIBlendTheme = value;
      this.listBoxDataArea.VIBlendTheme = value;
      this.listBoxRowArea.VIBlendTheme = value;
      this.vCheckBox1.VIBlendTheme = value;
      this.listBoxBoundFields.VIBlendTheme = value;
    }

    private void vButton1_Click(object sender, EventArgs e)
    {
      this.updateFromButton = true;
      this.UpdatePivotTable();
      this.updateFromButton = false;
    }

    /// <summary>Clean up any resources being used.</summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (vDataGridViewPivotDesign));
      vToolStripProfessionalRenderer professionalRenderer = new vToolStripProfessionalRenderer();
      this.splitContainer1 = new SplitContainer();
      this.listBoxBoundFields = new vListBox();
      this.label1 = new Label();
      this.tableLayoutPanel1 = new TableLayoutPanel();
      this.vCheckBox1 = new vCheckBox();
      this.vButton1 = new vButton();
      this.label2 = new Label();
      this.tableLayoutPanel2 = new TableLayoutPanel();
      this.vButton2 = new vButton();
      this.imageList1 = new ImageList(this.components);
      this.listBoxRowArea = new vListBox();
      this.listBoxColumnArea = new vListBox();
      this.listBoxDataArea = new vListBox();
      this.vButton4 = new vButton();
      this.vButton3 = new vButton();
      this.listBoxFilterArea = new vListBox();
      this.vButton5 = new vButton();
      this.vStripsRenderer1 = new vStripsRenderer(this.components);
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      this.tableLayoutPanel2.SuspendLayout();
      this.SuspendLayout();
      this.splitContainer1.Dock = DockStyle.Fill;
      this.splitContainer1.Location = new Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = Orientation.Horizontal;
      this.splitContainer1.Panel1.Controls.Add((Control) this.listBoxBoundFields);
      this.splitContainer1.Panel1.Controls.Add((Control) this.label1);
      this.splitContainer1.Panel1.Margin = new Padding(5);
      this.splitContainer1.Panel1.Padding = new Padding(5);
      this.splitContainer1.Panel2.Controls.Add((Control) this.tableLayoutPanel1);
      this.splitContainer1.Size = new Size(319, 293);
      this.splitContainer1.SplitterDistance = 101;
      this.splitContainer1.TabIndex = 0;
      this.listBoxBoundFields.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.listBoxBoundFields.Location = new Point(3, 24);
      this.listBoxBoundFields.Name = "listBoxBoundFields";
      this.listBoxBoundFields.RoundedCornersMaskListItem = (byte) 15;
      this.listBoxBoundFields.Size = new Size(312, 75);
      this.listBoxBoundFields.TabIndex = 1;
      this.listBoxBoundFields.Text = "vListBox1";
      this.listBoxBoundFields.VIBlendScrollBarsTheme = VIBLEND_THEME.VISTABLUE;
      this.listBoxBoundFields.VIBlendTheme = VIBLEND_THEME.VISTABLUE;
      this.label1.AutoSize = true;
      this.label1.Dock = DockStyle.Top;
      this.label1.Location = new Point(5, 5);
      this.label1.Name = "label1";
      this.label1.Size = new Size(150, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Drag fields to the areas below:";
      this.tableLayoutPanel1.ColumnCount = 3;
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333f));
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20f));
      this.tableLayoutPanel1.Controls.Add((Control) this.vCheckBox1, 0, 2);
      this.tableLayoutPanel1.Controls.Add((Control) this.vButton1, 2, 2);
      this.tableLayoutPanel1.Controls.Add((Control) this.label2, 0, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.tableLayoutPanel2, 0, 1);
      this.tableLayoutPanel1.Dock = DockStyle.Fill;
      this.tableLayoutPanel1.Location = new Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 3;
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
      this.tableLayoutPanel1.Size = new Size(319, 188);
      this.tableLayoutPanel1.TabIndex = 5;
      this.vCheckBox1.BackColor = Color.Transparent;
      this.tableLayoutPanel1.SetColumnSpan((Control) this.vCheckBox1, 2);
      this.vCheckBox1.Dock = DockStyle.Fill;
      this.vCheckBox1.Location = new Point(3, 166);
      this.vCheckBox1.Name = "vCheckBox1";
      this.vCheckBox1.Size = new Size(206, 19);
      this.vCheckBox1.TabIndex = 8;
      this.vCheckBox1.Text = "Defer Layout Update";
      this.vCheckBox1.UseThemeTextColor = false;
      this.vCheckBox1.UseVisualStyleBackColor = false;
      this.vCheckBox1.VIBlendTheme = VIBLEND_THEME.VISTABLUE;
      this.vButton1.AllowAnimations = true;
      this.vButton1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.vButton1.BackColor = Color.Transparent;
      this.vButton1.Location = new Point(215, 166);
      this.vButton1.Name = "vButton1";
      this.vButton1.RoundedCornersMask = (byte) 15;
      this.vButton1.Size = new Size(101, 19);
      this.vButton1.TabIndex = 9;
      this.vButton1.Text = "Update";
      this.vButton1.UseVisualStyleBackColor = false;
      this.vButton1.VIBlendTheme = VIBLEND_THEME.VISTABLUE;
      this.vButton1.Click += new EventHandler(this.vButton1_Click);
      this.label2.AutoSize = true;
      this.tableLayoutPanel1.SetColumnSpan((Control) this.label2, 3);
      this.label2.Dock = DockStyle.Fill;
      this.label2.Location = new Point(3, 0);
      this.label2.Name = "label2";
      this.label2.Size = new Size(313, 20);
      this.label2.TabIndex = 1;
      this.label2.Text = "Drag fields between areas below:";
      this.label2.TextAlign = ContentAlignment.MiddleLeft;
      this.tableLayoutPanel2.ColumnCount = 4;
      this.tableLayoutPanel1.SetColumnSpan((Control) this.tableLayoutPanel2, 3);
      this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
      this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
      this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
      this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
      this.tableLayoutPanel2.Controls.Add((Control) this.vButton2, 0, 0);
      this.tableLayoutPanel2.Controls.Add((Control) this.listBoxRowArea, 0, 1);
      this.tableLayoutPanel2.Controls.Add((Control) this.listBoxColumnArea, 1, 1);
      this.tableLayoutPanel2.Controls.Add((Control) this.listBoxDataArea, 2, 1);
      this.tableLayoutPanel2.Controls.Add((Control) this.vButton4, 2, 0);
      this.tableLayoutPanel2.Controls.Add((Control) this.vButton3, 1, 0);
      this.tableLayoutPanel2.Controls.Add((Control) this.listBoxFilterArea, 3, 1);
      this.tableLayoutPanel2.Controls.Add((Control) this.vButton5, 3, 0);
      this.tableLayoutPanel2.Dock = DockStyle.Fill;
      this.tableLayoutPanel2.Location = new Point(3, 23);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 2;
      this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 24f));
      this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.tableLayoutPanel2.Size = new Size(313, 137);
      this.tableLayoutPanel2.TabIndex = 10;
      this.vButton2.AllowAnimations = true;
      this.vButton2.BackColor = Color.Transparent;
      this.vButton2.Dock = DockStyle.Fill;
      this.vButton2.ImageKey = "rowArea.png";
      this.vButton2.ImageList = this.imageList1;
      this.vButton2.Location = new Point(3, 3);
      this.vButton2.Name = "vButton2";
      this.vButton2.PaintBorder = false;
      this.vButton2.PaintFill = false;
      this.vButton2.RoundedCornersMask = (byte) 15;
      this.vButton2.Size = new Size(72, 18);
      this.vButton2.TabIndex = 10;
      this.vButton2.Text = "Row Area";
      this.vButton2.TextAlign = ContentAlignment.MiddleLeft;
      this.vButton2.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.vButton2.TextWrap = true;
      this.vButton2.UseThemeTextColor = false;
      this.vButton2.UseVisualStyleBackColor = false;
      this.vButton2.VIBlendTheme = VIBLEND_THEME.VISTABLUE;
      this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
      this.imageList1.TransparentColor = Color.Transparent;
      this.imageList1.Images.SetKeyName(0, "columnArea.png");
      this.imageList1.Images.SetKeyName(1, "fieldsArea.png");
      this.imageList1.Images.SetKeyName(2, "rowArea.png");
      this.imageList1.Images.SetKeyName(3, "icon_filter.png");
      this.listBoxRowArea.Dock = DockStyle.Fill;
      this.listBoxRowArea.Location = new Point(3, 27);
      this.listBoxRowArea.Name = "listBoxRowArea";
      this.listBoxRowArea.RoundedCornersMaskListItem = (byte) 15;
      this.listBoxRowArea.Size = new Size(72, 107);
      this.listBoxRowArea.TabIndex = 5;
      this.listBoxRowArea.Text = "vListBox2";
      this.listBoxRowArea.VIBlendScrollBarsTheme = VIBLEND_THEME.VISTABLUE;
      this.listBoxRowArea.VIBlendTheme = VIBLEND_THEME.VISTABLUE;
      this.listBoxColumnArea.Dock = DockStyle.Fill;
      this.listBoxColumnArea.Location = new Point(81, 27);
      this.listBoxColumnArea.Name = "listBoxColumnArea";
      this.listBoxColumnArea.RoundedCornersMaskListItem = (byte) 15;
      this.listBoxColumnArea.Size = new Size(72, 107);
      this.listBoxColumnArea.TabIndex = 6;
      this.listBoxColumnArea.Text = "vListBox2";
      this.listBoxColumnArea.VIBlendScrollBarsTheme = VIBLEND_THEME.VISTABLUE;
      this.listBoxColumnArea.VIBlendTheme = VIBLEND_THEME.VISTABLUE;
      this.listBoxDataArea.Dock = DockStyle.Fill;
      this.listBoxDataArea.Location = new Point(159, 27);
      this.listBoxDataArea.Name = "listBoxDataArea";
      this.listBoxDataArea.RoundedCornersMaskListItem = (byte) 15;
      this.listBoxDataArea.Size = new Size(72, 107);
      this.listBoxDataArea.TabIndex = 7;
      this.listBoxDataArea.Text = "vListBox2";
      this.listBoxDataArea.VIBlendScrollBarsTheme = VIBLEND_THEME.VISTABLUE;
      this.listBoxDataArea.VIBlendTheme = VIBLEND_THEME.VISTABLUE;
      this.vButton4.AllowAnimations = true;
      this.vButton4.BackColor = Color.Transparent;
      this.vButton4.Dock = DockStyle.Fill;
      this.vButton4.ImageKey = "fieldsArea.png";
      this.vButton4.ImageList = this.imageList1;
      this.vButton4.Location = new Point(159, 3);
      this.vButton4.Name = "vButton4";
      this.vButton4.PaintBorder = false;
      this.vButton4.PaintFill = false;
      this.vButton4.RoundedCornersMask = (byte) 15;
      this.vButton4.Size = new Size(72, 18);
      this.vButton4.TabIndex = 12;
      this.vButton4.Text = "Data Area";
      this.vButton4.TextAlign = ContentAlignment.MiddleLeft;
      this.vButton4.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.vButton4.TextWrap = true;
      this.vButton4.UseThemeTextColor = false;
      this.vButton4.UseVisualStyleBackColor = false;
      this.vButton4.VIBlendTheme = VIBLEND_THEME.VISTABLUE;
      this.vButton3.AllowAnimations = true;
      this.vButton3.BackColor = Color.Transparent;
      this.vButton3.Dock = DockStyle.Fill;
      this.vButton3.ImageKey = "columnArea.png";
      this.vButton3.ImageList = this.imageList1;
      this.vButton3.Location = new Point(81, 3);
      this.vButton3.Name = "vButton3";
      this.vButton3.PaintBorder = false;
      this.vButton3.PaintFill = false;
      this.vButton3.RoundedCornersMask = (byte) 15;
      this.vButton3.Size = new Size(72, 18);
      this.vButton3.TabIndex = 11;
      this.vButton3.Text = "Column Area";
      this.vButton3.TextAlign = ContentAlignment.MiddleLeft;
      this.vButton3.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.vButton3.TextWrap = true;
      this.vButton3.UseThemeTextColor = false;
      this.vButton3.UseVisualStyleBackColor = false;
      this.vButton3.VIBlendTheme = VIBLEND_THEME.VISTABLUE;
      this.listBoxFilterArea.Dock = DockStyle.Fill;
      this.listBoxFilterArea.Location = new Point(237, 27);
      this.listBoxFilterArea.Name = "listBoxFilterArea";
      this.listBoxFilterArea.RoundedCornersMaskListItem = (byte) 15;
      this.listBoxFilterArea.Size = new Size(73, 107);
      this.listBoxFilterArea.TabIndex = 13;
      this.listBoxFilterArea.Text = "vListBox1";
      this.listBoxFilterArea.VIBlendScrollBarsTheme = VIBLEND_THEME.VISTABLUE;
      this.listBoxFilterArea.VIBlendTheme = VIBLEND_THEME.VISTABLUE;
      this.vButton5.AllowAnimations = true;
      this.vButton5.BackColor = Color.Transparent;
      this.vButton5.Dock = DockStyle.Fill;
      this.vButton5.ImageIndex = 3;
      this.vButton5.ImageList = this.imageList1;
      this.vButton5.Location = new Point(237, 3);
      this.vButton5.Name = "vButton5";
      this.vButton5.PaintBorder = false;
      this.vButton5.PaintDefaultBorder = false;
      this.vButton5.PaintDefaultFill = false;
      this.vButton5.PaintFill = false;
      this.vButton5.RoundedCornersMask = (byte) 15;
      this.vButton5.Size = new Size(73, 18);
      this.vButton5.TabIndex = 14;
      this.vButton5.Text = "Filter Area";
      this.vButton5.TextAlign = ContentAlignment.MiddleLeft;
      this.vButton5.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.vButton5.UseVisualStyleBackColor = false;
      this.vButton5.VIBlendTheme = VIBLEND_THEME.VISTABLUE;
      professionalRenderer.RoundedEdges = true;
      professionalRenderer.VIBlendTheme = VIBLEND_THEME.OFFICEBLACK;
      this.vStripsRenderer1.Renderer = professionalRenderer;
      this.vStripsRenderer1.VIBlendTheme = VIBLEND_THEME.OFFICEBLACK;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.splitContainer1);
      this.Name = "vDataGridViewPivotDesign";
      this.Size = new Size(319, 293);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.tableLayoutPanel2.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
