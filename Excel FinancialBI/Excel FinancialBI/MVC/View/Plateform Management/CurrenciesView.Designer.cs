using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
  partial class CurrenciesView : System.Windows.Forms.UserControl
  {

    //UserControl overrides dispose to clean up the component list.
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

    private System.ComponentModel.IContainer components;
    //NOTE: The following procedure is required by the Windows Form Designer
    //It can be modified using the Windows Form Designer.  
    //Do not modify it using the code editor.
    [System.Diagnostics.DebuggerStepThrough()]
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CurrenciesView));
      VIBlend.WinForms.DataGridView.DataGridLocalization DataGridLocalization1 = new VIBlend.WinForms.DataGridView.DataGridLocalization();
      this.VContextMenu1 = new VIBlend.WinForms.Controls.vContextMenu();
      this.SetMainCurrencyCallBack = new System.Windows.Forms.MenuItem();
      this.ValidateButton = new VIBlend.WinForms.Controls.vButton();
      this.EditButtonsImagelist = new System.Windows.Forms.ImageList(this.components);
      this.m_currenciesDataGridView = new VIBlend.WinForms.DataGridView.vDataGridView();
      this.SuspendLayout();
      //
      //VContextMenu1
      //
      this.VContextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { this.SetMainCurrencyCallBack });
      //
      //SetMainCurrencyCallBack
      //
      this.SetMainCurrencyCallBack.Index = 0;
      this.SetMainCurrencyCallBack.Text = "Set_main_currency";
      //
      //ValidateButton
      //
      this.ValidateButton.AllowAnimations = true;
      this.ValidateButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
      this.ValidateButton.BackColor = System.Drawing.Color.Transparent;
      this.ValidateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.ValidateButton.ImageKey = "1420498403_340208.ico";
      this.ValidateButton.ImageList = this.EditButtonsImagelist;
      this.ValidateButton.Location = new System.Drawing.Point(686, 457);
      this.ValidateButton.Name = "ValidateButton";
      this.ValidateButton.RoundedCornersMask = Convert.ToByte(15);
      this.ValidateButton.Size = new System.Drawing.Size(76, 30);
      this.ValidateButton.TabIndex = 1;
      this.ValidateButton.Text = "Save";
      this.ValidateButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.ValidateButton.UseVisualStyleBackColor = false;
      this.ValidateButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //EditButtonsImagelist
      //
      this.EditButtonsImagelist.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("EditButtonsImagelist.ImageStream");
      this.EditButtonsImagelist.TransparentColor = System.Drawing.Color.Transparent;
      this.EditButtonsImagelist.Images.SetKeyName(0, "1420498403_340208.ico");
      //
      //m_currenciesDataGridView
      //
      this.m_currenciesDataGridView.AllowAnimations = true;
      this.m_currenciesDataGridView.AllowCellMerge = true;
      this.m_currenciesDataGridView.AllowClipDrawing = true;
      this.m_currenciesDataGridView.AllowContextMenuColumnChooser = true;
      this.m_currenciesDataGridView.AllowContextMenuFiltering = true;
      this.m_currenciesDataGridView.AllowContextMenuGrouping = true;
      this.m_currenciesDataGridView.AllowContextMenuSorting = true;
      this.m_currenciesDataGridView.AllowCopyPaste = false;
      this.m_currenciesDataGridView.AllowDefaultContextMenu = true;
      this.m_currenciesDataGridView.AllowDragDropIndication = true;
      this.m_currenciesDataGridView.AllowHeaderItemHighlightOnCellSelection = true;
      this.m_currenciesDataGridView.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
      this.m_currenciesDataGridView.AutoUpdateOnListChanged = false;
      this.m_currenciesDataGridView.BackColor = System.Drawing.Color.White;
      this.m_currenciesDataGridView.BindingProgressEnabled = false;
      this.m_currenciesDataGridView.BindingProgressSampleRate = 20000;
      this.m_currenciesDataGridView.BorderColor = System.Drawing.Color.Empty;
      this.m_currenciesDataGridView.CellsArea.AllowCellMerge = true;
      this.m_currenciesDataGridView.CellsArea.ConditionalFormattingEnabled = false;
      this.m_currenciesDataGridView.ColumnsHierarchy.AllowDragDrop = false;
      this.m_currenciesDataGridView.ColumnsHierarchy.AllowResize = true;
      this.m_currenciesDataGridView.ColumnsHierarchy.AutoStretchColumns = false;
      this.m_currenciesDataGridView.ColumnsHierarchy.Fixed = false;
      this.m_currenciesDataGridView.ColumnsHierarchy.ShowExpandCollapseButtons = true;
      this.m_currenciesDataGridView.EnableColumnChooser = false;
      this.m_currenciesDataGridView.EnableResizeToolTip = true;
      this.m_currenciesDataGridView.EnableToolTips = true;
      this.m_currenciesDataGridView.FilterDisplayMode = VIBlend.WinForms.DataGridView.FilterDisplayMode.Default;
      this.m_currenciesDataGridView.GridLinesDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
      this.m_currenciesDataGridView.GridLinesDisplayMode = VIBlend.WinForms.DataGridView.GridLinesDisplayMode.DISPLAY_ALL;
      this.m_currenciesDataGridView.GroupingEnabled = false;
      this.m_currenciesDataGridView.HorizontalScroll = 0;
      this.m_currenciesDataGridView.HorizontalScrollBarLargeChange = 20;
      this.m_currenciesDataGridView.HorizontalScrollBarSmallChange = 5;
      this.m_currenciesDataGridView.ImageList = null;
      this.m_currenciesDataGridView.Localization = DataGridLocalization1;
      this.m_currenciesDataGridView.Location = new System.Drawing.Point(17, 38);
      this.m_currenciesDataGridView.MultipleSelectionEnabled = true;
      this.m_currenciesDataGridView.Name = "m_currenciesDataGridView";
      this.m_currenciesDataGridView.PivotColumnsTotalsEnabled = false;
      this.m_currenciesDataGridView.PivotColumnsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH;
      this.m_currenciesDataGridView.PivotRowsTotalsEnabled = false;
      this.m_currenciesDataGridView.PivotRowsTotalsMode = VIBlend.WinForms.DataGridView.PivotTotalsMode.DISPLAY_BOTH;
      this.m_currenciesDataGridView.RowsHierarchy.AllowDragDrop = false;
      this.m_currenciesDataGridView.RowsHierarchy.AllowResize = true;
      this.m_currenciesDataGridView.RowsHierarchy.CompactStyleRenderingEnabled = false;
      this.m_currenciesDataGridView.RowsHierarchy.CompactStyleRenderingItemsIndent = 15;
      this.m_currenciesDataGridView.RowsHierarchy.Fixed = false;
      this.m_currenciesDataGridView.RowsHierarchy.ShowExpandCollapseButtons = true;
      this.m_currenciesDataGridView.ScrollBarsEnabled = true;
      this.m_currenciesDataGridView.SelectionBorderDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
      this.m_currenciesDataGridView.SelectionBorderEnabled = true;
      this.m_currenciesDataGridView.SelectionBorderWidth = 2;
      this.m_currenciesDataGridView.SelectionMode = VIBlend.WinForms.DataGridView.vDataGridView.SELECTION_MODE.CELL_SELECT;
      this.m_currenciesDataGridView.Size = new System.Drawing.Size(745, 403);
      this.m_currenciesDataGridView.TabIndex = 2;
      this.m_currenciesDataGridView.Text = "VDataGridView1";
      this.m_currenciesDataGridView.ToolTipDuration = 5000;
      this.m_currenciesDataGridView.ToolTipShowDelay = 1500;
      this.m_currenciesDataGridView.VerticalScroll = 0;
      this.m_currenciesDataGridView.VerticalScrollBarLargeChange = 20;
      this.m_currenciesDataGridView.VerticalScrollBarSmallChange = 5;
      this.m_currenciesDataGridView.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER;
      this.m_currenciesDataGridView.VirtualModeCellDefault = false;
      //
      //CurrenciesView
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.m_currenciesDataGridView);
      this.Controls.Add(this.ValidateButton);
      this.Name = "CurrenciesView";
      this.Size = new System.Drawing.Size(776, 511);
      this.ResumeLayout(false);

    }
    internal VIBlend.WinForms.Controls.vContextMenu VContextMenu1;
    internal VIBlend.WinForms.Controls.vButton ValidateButton;
    internal VIBlend.WinForms.DataGridView.vDataGridView m_currenciesDataGridView;
    internal System.Windows.Forms.MenuItem SetMainCurrencyCallBack;

    internal System.Windows.Forms.ImageList EditButtonsImagelist;
  }
}