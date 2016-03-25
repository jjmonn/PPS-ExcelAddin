using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.View
{
  using FBI.MVC.Controller;
  using FBI.MVC.Model;
  using FBI.MVC.Model.CRUD;
  using Microsoft.Office.Interop.Excel;
  using System.Drawing;
  using Utils;

  public class RangeHighlighter
  {
    Int32 DIMENSIONS_COLORS = Color.FromArgb(215, 239, 253).ToArgb();
    SafeDictionary<Range, Object> m_originalCellsColor = new SafeDictionary<Range, Object>();
    Worksheet m_worksheet;

    public RangeHighlighter(Worksheet p_worksheet)
    {
      m_worksheet = p_worksheet;
    }

    public void FillCellColor(Range p_cell, EditedFactStatus p_status)
    {
      if (p_cell == null)
        return;

      switch (p_status)
      {
        case EditedFactStatus.InputDifferent:
          FillInputCellRed(p_cell);
          break;

        case EditedFactStatus.OutputDifferent :
          FillOutputCellRed(p_cell);
          break;

        case EditedFactStatus.InputEqual :
          FillInputsBaseColor(p_cell);
          break;

        case EditedFactStatus.FactTagEqual :
          FillInputsBaseColor(p_cell);
          break;

        case EditedFactStatus.FactTagDifferent :
          FillInputCellRed(p_cell);
          break;

        case EditedFactStatus.LegalHolidayEqual:
          FillInputsBaseColor(p_cell);
          break;

        case EditedFactStatus.LegalHolidayDifferent:
          FillInputCellRed(p_cell);
          break;

        case EditedFactStatus.Committed:
          FillCellGreen(p_cell);
          break;

      }
    }

    public void FillDimensionColor(WorksheetAreaController p_areaController)
    {
      foreach (DimensionType l_dim in Enum.GetValues(typeof(DimensionType)))
      {
        foreach (string l_cellAdress in p_areaController.Dimensions[l_dim].m_values.Keys)
        {
          Range l_cell = m_worksheet.Range[l_cellAdress];
          if (l_cell == null)
            return;
          RegisterCellOriginalFill(l_cell);
          l_cell.Interior.Color = Properties.Settings.Default.FactsEditionDimensionsFill;
        }
      }
    }

    public void FillInputsBaseColor(Range p_cell)
    {
      if (p_cell == null)
        return;
      RegisterCellOriginalFill(p_cell);
      p_cell.Interior.Color = Properties.Settings.Default.FactsEditionInputsFillColor;
    }

    public void FillInputCellRed(Range p_cell)
    {
      if (p_cell == null)
        return;

      RegisterCellOriginalFill(p_cell);
      p_cell.Interior.Color = Properties.Settings.Default.FactsEditionInputsRedFill;
    }

    private void FillOutputCellRed(Range p_cell)
    {
      RegisterCellOriginalFill(p_cell);
      p_cell.Interior.Color = Properties.Settings.Default.FactsEditionOutputsRedFill;
    }

    // equal input vs equal output
    private void ClearFillColor(Range p_cell)
    {
      RegisterCellOriginalFill(p_cell);
      p_cell.Interior.Color = Color.Transparent;
    }

    public void FillCellGreen(Range p_cell)
    {
      if (p_cell == null)
        return;

      try
      {
        RegisterCellOriginalFill(p_cell);
        p_cell.Interior.Color = Properties.Settings.Default.FactsEditionInputCommitedFill;
      }
      catch (Exception e)
      {
      }

    }

    private void RegisterCellOriginalFill(Range p_cell)
    {
      if (!m_originalCellsColor.ContainsKey(p_cell)) // only add if color has not been yet changed
        m_originalCellsColor.Add(p_cell, p_cell.Interior.Color); 
    }

    public void RevertToOriginalColors()
    {
      foreach (KeyValuePair<Range, Object> l_keyPair in m_originalCellsColor)
      {
        try
        {
          l_keyPair.Key.Interior.Color = l_keyPair.Value;
        }
        catch (Exception e)
        {
          System.Diagnostics.Debug.WriteLine("revert to original color: " + e.Message);
        }
      }
      m_originalCellsColor.Clear();
    }

  }
}
