// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.DataExport.ExcelXMLExportWriter
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;
using System.Drawing;
using System.IO;
using System.Text;

namespace VIBlend.WinForms.DataGridView.DataExport
{
  internal class ExcelXMLExportWriter : IExportWriter
  {
    private StreamWriter wr;
    private bool isRowStarted;
    private vDataGridView grid;

    public ExcelXMLExportWriter(vDataGridView grid)
    {
      this.grid = grid;
    }

    public bool CreateFile(string filePath)
    {
      try
      {
        if (this.wr != null)
          this.wr.Close();
        this.wr = new StreamWriter(filePath, false, Encoding.Unicode);
        this.wr.WriteLine("<?xml version=\"1.0\"?>");
        this.wr.WriteLine("<?mso-application progid=\"Excel.Sheet\"?>");
        this.wr.WriteLine("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"");
        this.wr.WriteLine("xmlns:o=\"urn:schemas-microsoft-com:office:office\"");
        this.wr.WriteLine("xmlns:x=\"urn:schemas-microsoft-com:office:excel\"");
        this.wr.WriteLine("xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\"");
        this.wr.WriteLine("xmlns:html=\"http://www.w3.org/TR/REC-html40\">");
        this.wr.WriteLine("<DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\">");
        this.wr.WriteLine("<Author></Author>");
        this.wr.WriteLine("<LastAuthor></LastAuthor>");
        this.wr.WriteLine("<Created></Created>");
        this.wr.WriteLine("<LastSaved></LastSaved>");
        this.wr.WriteLine("<Version>12.00</Version>");
        this.wr.WriteLine("</DocumentProperties>");
        this.wr.WriteLine("<ExcelWorkbook xmlns=\"urn:schemas-microsoft-com:office:excel\">");
        this.wr.WriteLine("<WindowHeight>8130</WindowHeight>");
        this.wr.WriteLine("<WindowWidth>15135</WindowWidth>");
        this.wr.WriteLine("<WindowTopX>120</WindowTopX>");
        this.wr.WriteLine("<WindowTopY>45</WindowTopY>");
        this.wr.WriteLine("<ProtectStructure>False</ProtectStructure>");
        this.wr.WriteLine("<ProtectWindows>False</ProtectWindows>");
        this.wr.WriteLine("</ExcelWorkbook>");
        this.wr.WriteLine("<Styles>");
        this.wr.WriteLine("<Style ss:ID=\"Default\" ss:Name=\"Normal\">");
        this.wr.WriteLine("<Alignment ss:Vertical=\"Bottom\"/>");
        this.wr.WriteLine("<Borders/>");
        this.wr.WriteLine("<Font ss:FontName=\"Calibri\" x:Family=\"Swiss\" ss:Size=\"11\" ss:Color=\"#000000\"/>");
        this.wr.WriteLine("<Interior/>");
        this.wr.WriteLine("<NumberFormat/>");
        this.wr.WriteLine("<Protection/>");
        this.wr.WriteLine("</Style>");
        this.wr.WriteLine("<Style ss:ID=\"s1\">");
        this.wr.WriteLine("<NumberFormat ss:Format=\"General Date\"/>");
        this.wr.WriteLine("</Style>");
        this.wr.WriteLine("<Style ss:ID=\"s2\">");
        this.wr.WriteLine("<Alignment ss:Vertical=\"Bottom\"/>");
        this.wr.WriteLine("<Borders/>");
        this.wr.WriteLine("<Font ss:FontName=\"Calibri\" x:Family=\"Swiss\" ss:Size=\"11\" ss:Color=\"" + ColorTranslator.ToHtml(this.grid.Theme.HierarchyItemStyleNormal.TextColor) + "\"/>");
        this.wr.WriteLine("<Interior ss:Color=\"" + ColorTranslator.ToHtml(this.grid.Theme.HierarchyItemStyleNormal.FillStyle.Colors[0]) + "\" ss:Pattern=\"Solid\"/>");
        this.wr.WriteLine("<Protection/>");
        this.wr.WriteLine("</Style>");
        this.wr.WriteLine("</Styles>");
        this.wr.WriteLine("<Worksheet ss:Name=\"Sheet1\">");
        this.wr.WriteLine("<Table x:FullColumns=\"1\" x:FullRows=\"1\" ss:DefaultRowHeight=\"15\">");
      }
      catch (Exception ex)
      {
      }
      return this.wr != null;
    }

    public void CloseFile()
    {
      if (this.wr == null)
        return;
      this.wr.WriteLine("</Table>");
      this.wr.WriteLine("<WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">");
      this.wr.WriteLine("<PageSetup>");
      this.wr.WriteLine("<Header x:Margin=\"0.3\"/>");
      this.wr.WriteLine("<Footer x:Margin=\"0.3\"/>");
      this.wr.WriteLine("<PageMargins x:Bottom=\"0.75\" x:Left=\"0.7\" x:Right=\"0.7\" x:Top=\"0.75\"/>");
      this.wr.WriteLine("</PageSetup>");
      this.wr.WriteLine("<Unsynced/>");
      this.wr.WriteLine("<Selected/>");
      this.wr.WriteLine("<Panes>");
      this.wr.WriteLine("</Panes>");
      this.wr.WriteLine("<ProtectObjects>False</ProtectObjects>");
      this.wr.WriteLine("<ProtectScenarios>False</ProtectScenarios>");
      this.wr.WriteLine("</WorksheetOptions>");
      this.wr.WriteLine("</Worksheet>");
      this.wr.WriteLine("</Workbook>");
      this.wr.Close();
      this.wr = (StreamWriter) null;
    }

    public void BeginRow()
    {
      if (this.wr == null)
        throw new Exception("File not open");
      if (this.isRowStarted)
        throw new Exception("Row is already started. Call EndRow first.");
      this.wr.WriteLine("<Row ss:AutoFitHeight=\"0\">");
      this.isRowStarted = true;
    }

    public void EndRow()
    {
      if (this.wr == null)
        throw new Exception("File not open");
      if (!this.isRowStarted)
        throw new Exception("There is no started row. Call BeginRow first.");
      this.wr.WriteLine("</Row>");
      this.isRowStarted = false;
    }

    public void AddColumn(string text)
    {
      if (text.Length > 0)
        this.wr.WriteLine("<Cell ss:StyleID=\"s2\"><Data ss:Type=\"String\">" + text + "</Data></Cell>");
      else
        this.AddColumn((object) text);
    }

    public void AddColumn(object value)
    {
      if (this.wr == null)
        throw new Exception("File not open");
      if (!this.isRowStarted)
        throw new Exception("There is no started row. Call BeginRow first.");
      if (value == null)
        this.wr.WriteLine("<Cell><Data ss:Type=\"String\"></Data></Cell>");
      else if (value is string)
      {
        this.wr.WriteLine("<Cell><Data ss:Type=\"String\">" + value.ToString() + "</Data></Cell>");
      }
      else
      {
        DateTime now = DateTime.Now;
        if (value is DateTime)
        {
          this.wr.WriteLine("<Cell ss:StyleID=\"s1\"><Data ss:Type=\"DateTime\">" + string.Format("{0:yyyy-MM-ddTHH:mm:ss}", (object) (DateTime) value) + "</Data></Cell>");
        }
        else
        {
          double result = 0.0;
          if (double.TryParse(value.ToString(), out result))
            this.wr.WriteLine("<Cell><Data ss:Type=\"Number\">" + value.ToString() + "</Data></Cell>");
          else
            this.wr.WriteLine("<Cell><Data ss:Type=\"String\">" + value.ToString() + "</Data></Cell>");
        }
      }
    }
  }
}
