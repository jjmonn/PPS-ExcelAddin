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
    string[] m_originalCellsAddress = new string[4096];
    object[] m_originalCellsColor = new object[4096];
    int m_nbRegistered = 0;
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

        case EditedFactStatus.OutputEqual :
          FillOutputBaseColor(p_cell);
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
        foreach (KeyValuePair<string, CRUDEntity> l_pair in p_areaController.Dimensions[l_dim].m_values)
        {
          Range l_cell = m_worksheet.Range[l_pair.Key];
          if (l_cell == null)
            return;
          RegisterCellOriginalFill(l_cell);
          l_cell.Interior.Color = Properties.Settings.Default.FactsEditionDimensionsFill;
        }
      }
    }

    public void FillOutputBaseColor(Range p_cell)
    {
      if (p_cell == null)
        return;
      RegisterCellOriginalFill(p_cell);
      p_cell.Interior.Color = Properties.Settings.Default.FactsEditionOutputsBackColor;
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
      catch
      {
      }

    }

    private void RegisterCellOriginalFill(Range p_cell)
    {
      if (m_originalCellsAddress.Contains(p_cell.Address))
        return;
      m_originalCellsColor[m_nbRegistered] = p_cell.Interior.Color;
      m_originalCellsAddress[m_nbRegistered] = p_cell.Address;
      m_nbRegistered++;
      if (m_nbRegistered >= m_originalCellsColor.Length)
      {
        object[] l_newColorTab = new object[m_originalCellsColor.Length * 2];
        string[] l_newAddressTab = new string[m_originalCellsAddress.Length * 2];

        Array.Copy(m_originalCellsColor, l_newColorTab, m_originalCellsColor.Length);
        Array.Copy(m_originalCellsAddress, l_newAddressTab, m_originalCellsAddress.Length);
        m_originalCellsColor = l_newColorTab;
        m_originalCellsAddress = l_newAddressTab;
      }
    }

    public void RevertToOriginalColors()
    {
      for (int i = 0; i < m_nbRegistered; ++i)
      {
        try
        {
          Range l_cell = m_worksheet.get_Range(m_originalCellsAddress[i]);

          if (l_cell != null)
            l_cell.Interior.Color = m_originalCellsColor[i];
        }
        catch (Exception e)
        {
          System.Diagnostics.Debug.WriteLine("revert to original color: " + e.Message);
        }
      }
      m_originalCellsColor = new object[4096];
      m_originalCellsAddress = new string[4096];
    }

  }
}
