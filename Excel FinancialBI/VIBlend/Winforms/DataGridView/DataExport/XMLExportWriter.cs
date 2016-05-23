// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.DataExport.XMLExportWriter
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;
using System.IO;
using System.Text;

namespace VIBlend.WinForms.DataGridView.DataExport
{
  internal class XMLExportWriter : IExportWriter
  {
    private StreamWriter wr;
    private bool isRowStarted;

    public bool CreateFile(string filePath)
    {
      try
      {
        if (this.wr != null)
          this.wr.Close();
        this.wr = new StreamWriter(filePath, false, Encoding.Unicode);
        this.wr.WriteLine("<?xml version=\"1.0\"?>");
        this.wr.WriteLine("<Table>");
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
      this.wr.Close();
      this.wr = (StreamWriter) null;
    }

    public void BeginRow()
    {
      if (this.wr == null)
        throw new Exception("File not open");
      if (this.isRowStarted)
        throw new Exception("Row is already started. Call EndRow first.");
      this.wr.WriteLine("<Row>");
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
      if (this.wr == null)
        throw new Exception("File not open");
      if (!this.isRowStarted)
        throw new Exception("There is no started row. Call BeginRow first.");
      this.wr.WriteLine("<Cell>" + text + "</Cell>");
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
