﻿// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.DataExport.CSVExportWriter
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;
using System.IO;
using System.Text;

namespace VIBlend.WinForms.DataGridView.DataExport
{
  internal class CSVExportWriter : IExportWriter
  {
    private StreamWriter wr;
    private bool isRowStarted;
    private int columnsWritten;

    public bool CreateFile(string filePath)
    {
      try
      {
        if (this.wr != null)
          this.wr.Close();
        this.wr = new StreamWriter(filePath, false, Encoding.Unicode);
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
      this.wr.Close();
      this.wr = (StreamWriter) null;
    }

    public void BeginRow()
    {
      if (this.wr == null)
        throw new Exception("File not open");
      if (this.isRowStarted)
        throw new Exception("Row is already started. Call EndRow first.");
      this.isRowStarted = true;
      this.columnsWritten = 0;
    }

    public void EndRow()
    {
      if (this.wr == null)
        throw new Exception("File not open");
      if (!this.isRowStarted)
        throw new Exception("There is no started row. Call BeginRow first.");
      this.wr.Write(this.wr.NewLine);
      this.isRowStarted = false;
    }

    public void AddColumn(string text)
    {
      if (this.wr == null)
        throw new Exception("File not open");
      if (!this.isRowStarted)
        throw new Exception("There is no started row. Call BeginRow first.");
      if (this.columnsWritten > 0)
        this.wr.Write(',');
      if (text.Contains(","))
        text = string.Format("\"{0}\"", (object) text);
      this.wr.Write(text);
      ++this.columnsWritten;
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
