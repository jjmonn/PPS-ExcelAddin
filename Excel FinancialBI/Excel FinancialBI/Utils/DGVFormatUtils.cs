using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIBlend.WinForms.DataGridView;
using System.Windows.Forms;
using VIBlend.Utilities;
using System.Drawing;

namespace FBI.Utils
{
  using MVC.Model.CRUD;
  using MVC.Model;
  using Forms;

  class DGVFormatUtils
  {
    static internal void FormatValue(BaseFbiDataGridView<ResultKey> p_dgv, ResultKey p_row, ResultKey p_column)
    {
      try
      {
        double l_value = (double)p_dgv.GetCellValue(p_row, p_column);

        GridCellStyle l_cellStyle = GridTheme.GetDefaultTheme(p_dgv.VIBlendTheme).GridCellStyle;

        if (l_value < 0)
          l_cellStyle.TextColor = Color.Red;
        else
          l_cellStyle.TextColor = Color.Green;
        p_dgv.CellsArea.SetCellDrawStyle(p_dgv.Rows[p_row], p_dgv.Columns[p_column], l_cellStyle);
      }
      catch (Exception e)
      {
        System.Diagnostics.Debug.WriteLine("DGVFormatUtils.FormatValue: " + e.Message);
      }
    }

    static private void FormatDGVHierachy(vDataGridView vDGV, HierarchyItemsCollection p_hierarchy, UInt32 currencyId)
    {
      string l_formatString = null;
      Int32 indent = default(Int32);
      foreach (HierarchyItem l_item in p_hierarchy)
      {
        Account l_account = AccountModel.Instance.GetValue(l_item.Caption);

        if (l_account == null)
          continue;

        VIBlend.Utilities.HierarchyItemStyle l_HANStyle = GridTheme.GetDefaultTheme(vDGV.VIBlendTheme).HierarchyItemStyleNormal;
        VIBlend.Utilities.HierarchyItemStyle l_HASStyle = GridTheme.GetDefaultTheme(vDGV.VIBlendTheme).HierarchyItemStyleSelected;
        VIBlend.Utilities.HierarchyItemStyle l_HENStyle = GridTheme.GetDefaultTheme(VIBLEND_THEME.OFFICE2010BLUE).HierarchyItemStyleNormal;
        VIBlend.Utilities.HierarchyItemStyle l_HESStyle = GridTheme.GetDefaultTheme(VIBLEND_THEME.OFFICE2010BLUE).HierarchyItemStyleSelected;
        GridCellStyle l_CAStyle = GridTheme.GetDefaultTheme(vDGV.VIBlendTheme).GridCellStyle;
        GridCellStyle l_CEStyle = GridTheme.GetDefaultTheme(VIBLEND_THEME.OFFICE2010BLUE).GridCellStyle;

        Font l_font = new Font(vDGV.Font.FontFamily, Properties.Settings.Default.dgvFontSize);
        l_HANStyle.Font = l_font;
        l_HASStyle.Font = l_font;
        l_HENStyle.Font = l_font;
        l_HESStyle.Font = l_font;
        l_CAStyle.Font = l_font;
        l_CEStyle.Font = l_font;

        Currency l_currency = CurrencyModel.Instance.GetValue(currencyId);

        if (l_currency == null)
          continue;

        l_formatString = FbiAccountFormat.Get(l_account.Type, l_currency);

        if (l_item.ParentItem == null)
        {
          FormatRow(l_item, l_formatString, l_CAStyle, l_HANStyle, l_HASStyle, indent, l_CAStyle, l_CEStyle, l_HANStyle,
          l_HASStyle, l_HENStyle, l_HESStyle);
        }
        else
        {
          FormatRow(l_item, l_formatString, l_CEStyle, l_HENStyle, l_HESStyle, indent, l_CAStyle, l_CEStyle, l_HANStyle,
          l_HASStyle, l_HENStyle, l_HESStyle);
        }
      }
    }

    static internal void FormatDGVs(vDataGridView vDGV, UInt32 currencyId)
    {
      FormatDGVHierachy(vDGV, vDGV.RowsHierarchy.Items, currencyId);
      FormatDGVHierachy(vDGV, vDGV.ColumnsHierarchy.Items, currencyId);
    }

    static private void FormatRow(HierarchyItem p_row, string p_formatString, GridCellStyle p_CStyle, HierarchyItemStyle p_HNStyle,
      HierarchyItemStyle p_HSStyle, Int32 p_indent, GridCellStyle p_CAStyle, GridCellStyle p_CEStyle, HierarchyItemStyle p_HANStyle,
      HierarchyItemStyle p_HASStyle, HierarchyItemStyle p_HENStyle, HierarchyItemStyle p_HESStyle)
    {
      switch (p_indent)
      {
        case 1:
          p_row.Caption = "   " + p_row.Caption;
          break;
        case 2:
          p_row.Caption = "      " + p_row.Caption;
          break;
        case 3:
          p_row.Caption = "            " + p_row.Caption;
          break;
      }

      p_row.HierarchyItemStyleNormal = p_HNStyle;
      p_row.HierarchyItemStyleSelected = p_HSStyle;
      p_row.CellsStyle = p_CStyle;
      p_row.CellsFormatString = p_formatString;
      p_row.CellsTextAlignment = ContentAlignment.MiddleRight;

      if (p_row.Items.Count > 0)
      {
        foreach (HierarchyItem l_item in p_row.Items)
        {
          l_item.HierarchyItemStyleNormal = p_HNStyle;
          l_item.HierarchyItemStyleSelected = p_HSStyle;
          l_item.CellsStyle = p_CStyle;
          l_item.CellsFormatString = p_formatString;
          l_item.CellsTextAlignment = ContentAlignment.MiddleRight;
        }
      }
      foreach (HierarchyItem l_subRow in p_row.Items)
      {
        FormatRow(l_subRow, p_formatString, p_CEStyle, p_HENStyle, p_HESStyle, p_indent, p_CAStyle, p_CEStyle, p_HANStyle,
        p_HASStyle, p_HENStyle, p_HESStyle);
      }
    }
  }
}
