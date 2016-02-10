using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
  partial class AllocationKeysView : System.Windows.Forms.Form
  {

    //Form overrides dispose to clean up the component list.
    [System.Diagnostics.DebuggerNonUserCode()]
    protected override void Dispose(bool disposing)
    {
      try
      {
        if (disposing && components != null)
        {
          components.Dispose();
        }
      }
      finally
      {
        base.Dispose(disposing);
      }
    }

    //Required by the Windows Form Designer

    private System.ComponentModel.IContainer components = null;
    //NOTE: The following procedure is required by the Windows Form Designer
    //It can be modified using the Windows Form Designer.  
    //Do not modify it using the code editor.
    [System.Diagnostics.DebuggerStepThrough()]
    private void InitializeComponent()
    {
      VIBlend.WinForms.DataGridView.DataGridLocalization dataGridLocalization1 = new VIBlend.WinForms.DataGridView.DataGridLocalization();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AllocationKeysView));
      this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.m_allocationsKeysDGV = new VIBlend.WinForms.DataGridView.vDataGridView();
      this.Panel1 = new System.Windows.Forms.Panel();
      this.m_accountTextBox = new VIBlend.WinForms.Controls.vTextBox();
      this.m_accountLabel = new VIBlend.WinForms.Controls.vLabel();
      this.TableLayoutPanel1.SuspendLayout();
      this.Panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // TableLayoutPanel1
      // 
      this.TableLayoutPanel1.ColumnCount = 1;
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.TableLayoutPanel1.Controls.Add(this.m_allocationsKeysDGV, 0, 1);
      this.TableLayoutPanel1.Controls.Add(this.Panel1, 0, 0);
      this.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 2;
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.TableLayoutPanel1.Size = new System.Drawing.Size(594, 529);
      this.TableLayoutPanel1.TabIndex = 0;
      // 
      // m_allocationsKeysDGV
      // 
      this.m_allocationsKeysDGV.AllowAnimations = true;
      this.m_allocationsKeysDGV.AllowCellMerge = true;
      this.m_allocationsKeysDGV.AllowClipDrawing = true;
      this.m_allocationsKeysDGV.AllowContextMenuColumnChooser = true;
      this.m_allocationsKeysDGV.AllowContextMenuFiltering = true;
      this.m_allocationsKeysDGV.AllowContextMenuGrouping = true;
      this.m_allocationsKeysDGV.AllowContextMenuSorting = true;
      this.m_allocationsKeysDGV.AllowCopyPaste = false;
      this.m_allocationsKeysDGV.AllowDefaultContextMenu = true;
      this.m_allocationsKeysDGV.AllowDragDropIndication = true;
      this.m_allocationsKeysDGV.AllowHeaderItemHighlightOnCellSelection = true;
      this.m_allocationsKeysDGV.AutoUpdateOnListChanged = false;
      this.m_allocationsKeysDGV.BackColor = System.Drawing.SystemColors.Control;
      this.m_allocationsKeysDGV.BindingProgressEnabled = false;
      this.m_allocationsKeysDGV.BindingProgressSampleRate = 20000;
      this.m_allocationsKeysDGV.BorderColor = System.Drawing.Color.Empty;
      this.m_allocationsKeysDGV.CellsArea.AllowCellMerge = true;
      this.m_allocationsKeysDGV.CellsArea.ConditionalFormattingEnabled = false;
      this.m_allocationsKeysDGV.ColumnsHierarchy.AllowDragDrop = false;
      this.m_allocationsKeysDGV.ColumnsHierarchy.AllowResize = true;
      this.m_allocationsKeysDGV.ColumnsHierarchy.AutoStretchColumns = false;
      this.m_allocationsKeysDGV.ColumnsHierarchy.Fixed = false;
      this.m_allocationsKeysDGV.ColumnsHierarchy.ShowExpandCollapseButtons = true;
      this.m_allocationsKeysDGV.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_allocationsKeysDGV.EnableColumnChooser = false;
      this.m_allocationsKeysDGV.EnableResizeToolTip = true;
      this.m_allocationsKeysDGV.EnableToolTips = true;
      this.m_allocationsKeysDGV.FilterDisplayMode = VIBlend.WinForms.DataGridView.FilterDisplayMode.Default;
      this.m_allocationsKeysDGV.GridLinesDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
      this.m_allocationsKeysDGV.GridLinesDisplayMode = VIBlend.WinForms.DataGridView.GridLinesDisplayMode.DISPLAY_ALL;
      this.m_allocationsKeysDGV.GroupingEnabled = false;
      this.m_allocationsKeysDGV.HorizontalScroll = 0;
      this.m_allocationsKeysDGV.HorizontalScrollBarLargeChange = 20;
      this.m_allocationsKeysDGV.HorizontalScrollBarSmallChange = 5;
      this.m_allocationsKeysDGV.ImageList = null;
      this.m_allocationsKeysDGV.Localization = dataGridLocalization1;
      this.m_allocationsKeysDGV.Location = new System.Drawing.Point(3, 28);
      this.m_allocationsKeysDGV.MultipleSelectionEnabled = true;
      this.m_allocationsKeysDGV.Name = "m_allocationsKeysDGV";
      this.m_allocationsKeysDGV.PivotColumnsTotalsEnabled = false;
      this.m_allocationsKeysDGV.PivotColumnsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH;
      this.m_allocationsKeysDGV.PivotRowsTotalsEnabled = false;
      this.m_allocationsKeysDGV.PivotRowsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH;
      this.m_allocationsKeysDGV.RowsHierarchy.AllowDragDrop = false;
      this.m_allocationsKeysDGV.RowsHierarchy.AllowResize = true;
      this.m_allocationsKeysDGV.RowsHierarchy.CompactStyleRenderingEnabled = false;
      this.m_allocationsKeysDGV.RowsHierarchy.CompactStyleRenderingItemsIndent = 15;
      this.m_allocationsKeysDGV.RowsHierarchy.Fixed = false;
      this.m_allocationsKeysDGV.RowsHierarchy.ShowExpandCollapseButtons = true;
      this.m_allocationsKeysDGV.ScrollBarsEnabled = true;
      this.m_allocationsKeysDGV.SelectionBorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
      this.m_allocationsKeysDGV.SelectionBorderEnabled = true;
      this.m_allocationsKeysDGV.SelectionBorderWidth = 2;
      this.m_allocationsKeysDGV.SelectionMode = VIBlend.WinForms.DataGridView.vDataGridView.SELECTION_MODE.CELL_SELECT;
      this.m_allocationsKeysDGV.Size = new System.Drawing.Size(588, 498);
      this.m_allocationsKeysDGV.TabIndex = 0;
      this.m_allocationsKeysDGV.Text = "VDataGridView1";
      this.m_allocationsKeysDGV.ToolTipDuration = 5000;
      this.m_allocationsKeysDGV.ToolTipShowDelay = 1500;
      this.m_allocationsKeysDGV.VerticalScroll = 0;
      this.m_allocationsKeysDGV.VerticalScrollBarLargeChange = 20;
      this.m_allocationsKeysDGV.VerticalScrollBarSmallChange = 5;
      this.m_allocationsKeysDGV.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER;
      this.m_allocationsKeysDGV.VirtualModeCellDefault = false;
      // 
      // Panel1
      // 
      this.Panel1.Controls.Add(this.m_accountTextBox);
      this.Panel1.Controls.Add(this.m_accountLabel);
      this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.Panel1.Location = new System.Drawing.Point(0, 0);
      this.Panel1.Margin = new System.Windows.Forms.Padding(0);
      this.Panel1.Name = "Panel1";
      this.Panel1.Size = new System.Drawing.Size(594, 25);
      this.Panel1.TabIndex = 1;
      // 
      // m_accountTextBox
      // 
      this.m_accountTextBox.BackColor = System.Drawing.Color.White;
      this.m_accountTextBox.BoundsOffset = new System.Drawing.Size(1, 1);
      this.m_accountTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.m_accountTextBox.DefaultText = "Empty...";
      this.m_accountTextBox.Enabled = false;
      this.m_accountTextBox.Location = new System.Drawing.Point(77, 3);
      this.m_accountTextBox.MaxLength = 32767;
      this.m_accountTextBox.Name = "m_accountTextBox";
      this.m_accountTextBox.PasswordChar = '\0';
      this.m_accountTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.m_accountTextBox.SelectionLength = 0;
      this.m_accountTextBox.SelectionStart = 0;
      this.m_accountTextBox.Size = new System.Drawing.Size(514, 19);
      this.m_accountTextBox.TabIndex = 3;
      this.m_accountTextBox.Text = "Account";
      this.m_accountTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.m_accountTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER;
      // 
      // m_accountLabel
      // 
      this.m_accountLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_accountLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_accountLabel.Ellipsis = false;
      this.m_accountLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_accountLabel.Location = new System.Drawing.Point(3, 3);
      this.m_accountLabel.Multiline = true;
      this.m_accountLabel.Name = "m_accountLabel";
      this.m_accountLabel.Size = new System.Drawing.Size(68, 19);
      this.m_accountLabel.TabIndex = 2;
      this.m_accountLabel.Text = "Account";
      this.m_accountLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_accountLabel.UseMnemonics = true;
      this.m_accountLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // AllocationKeysView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(594, 529);
      this.Controls.Add(this.TableLayoutPanel1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "AllocationKeysView";
      this.Text = "Allocation Keys";
      this.TableLayoutPanel1.ResumeLayout(false);
      this.Panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    public System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
    public VIBlend.WinForms.DataGridView.vDataGridView m_allocationsKeysDGV;
    public System.Windows.Forms.Panel Panel1;
    public VIBlend.WinForms.Controls.vTextBox m_accountTextBox;
    public VIBlend.WinForms.Controls.vLabel m_accountLabel;
  }
}