// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.DataExport.HTMLExportWriter
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;
using System.IO;
using System.Text;

namespace VIBlend.WinForms.DataGridView.DataExport
{
  internal class HTMLExportWriter : IExportWriter
  {
    private StreamWriter wr;
    private bool isRowStarted;
    private int currentRow;
    private int currentColumn;

    public bool CreateFile(string filePath)
    {
      try
      {
        if (this.wr != null)
          this.wr.Close();
        this.wr = new StreamWriter(filePath, false, Encoding.Unicode);
        this.wr.WriteLine("<html>");
        this.wr.WriteLine("<head>");
        this.wr.WriteLine("<title></title>");
        this.wr.WriteLine("<META http-equiv=Content-type content=\"text/html; charset=UTF-8\">");
        this.wr.WriteLine("<style type=\"text/css\">");
        this.wr.WriteLine(".styleItem { font-family: \"Veranda\"; font-size: 9pt; border-style: solid; border-width: 1px; border-color: #7392BD; background-color: #BBD4EF;}");
        this.wr.WriteLine(".styleCell { font-family: \"Veranda\"; font-size: 9pt; border-style: solid; border-width: 1px; border-color: #7392BD; background-color: #FFFFFF;}");
        this.wr.WriteLine("</style>");
        this.wr.WriteLine("</head>");
        this.wr.WriteLine("<body>");
        this.wr.WriteLine("<table cellspacing=\"0\" cellpadding=\"0\" style=\"border-width:1px;\">");
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
      this.wr.WriteLine("</table>");
      this.wr.WriteLine("</body>");
      this.wr.WriteLine("</html>");
      this.wr.Close();
      this.wr = (StreamWriter) null;
    }

    public void BeginRow()
    {
      if (this.wr == null)
        throw new Exception("File not open");
      if (this.isRowStarted)
        throw new Exception("Row is already started. Call EndRow first.");
      this.wr.WriteLine("<tr>");
      this.isRowStarted = true;
    }

    public void EndRow()
    {
      if (this.wr == null)
        throw new Exception("File not open");
      if (!this.isRowStarted)
        throw new Exception("There is no started row. Call BeginRow first.");
      this.wr.WriteLine("</tr>");
      this.isRowStarted = false;
      ++this.currentRow;
      this.currentColumn = 0;
    }

    public void AddColumn(string text)
    {
      if (this.wr == null)
        throw new Exception("File not open");
      if (!this.isRowStarted)
        throw new Exception("There is no started row. Call BeginRow first.");
      if (string.IsNullOrEmpty(text))
        text = "&nbsp;";
      if (this.currentRow == 0 || this.currentColumn == 0)
        this.wr.WriteLine("<td class=\"styleItem\">" + text + "</td>");
      else
        this.wr.WriteLine("<td class=\"styleCell\">" + text + "</td>");
      ++this.currentColumn;
    }

    public void AddColumn(object value)
    {
      if (value == null)
        this.AddColumn("");
      else
        this.AddColumn(value.ToString());
    }
  }
}
