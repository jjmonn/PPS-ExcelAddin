using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using AddinExpress.XL;

namespace FBI.MVC.View
{
  using Controller;
  using Utils;

  public partial class FBIFunctionSidePane : AddinExpress.XL.ADXExcelTaskPane
  {
    FBIFunctionViewController m_controller;
    FBIFunctionView m_view;
    ExcelWorksheetEvents m_worksheetEvents;
    bool m_windowed = false;

    public FBIFunctionSidePane()
    {
      InitializeComponent();
      m_worksheetEvents = new ExcelWorksheetEvents(Addin.AddinModule);
      ADXAfterTaskPaneHide += OnSidePaneHide;
      ADXAfterTaskPaneShow += OnSidePaneShow;
      this.Text = Local.GetValue("ppsbi.title");
    }

    protected override void WndProc(ref Message m)
    {
 	    base.WndProc(ref m);
    }

    void OnSidePaneShow(object sender, ADXAfterTaskPaneShowEventArgs e)
    {
      if (!m_windowed)
        m_worksheetEvents.ConnectTo(Addin.AddinModule.ExcelApp.ActiveSheet, true);
    }

    void OnSidePaneHide(object sender, EventArgs e)
    {
      if (!m_windowed)
        m_worksheetEvents.RemoveConnection();
    }

    public void LoadController()
    {
      if (m_controller == null)
      {
        m_controller = new FBIFunctionViewController(this);
        m_view = m_controller.View as FBIFunctionView;
        m_view.m_extractBT.Click += OnExtractClick;
        m_worksheetEvents.SelectionChanged += WorksheetEvents_SelectionChanged;
      }
      m_controller.LoadView();
    }

    void WorksheetEvents_SelectionChanged(Microsoft.Office.Interop.Excel.Range p_range)
    {
      if (m_view != null)
        m_view.LoadView();
    }

    void OnExtractClick(object sender, EventArgs e)
    {
      m_view.m_extractBT.Visible = false;
      m_windowed = true;
      ViewToWindow l_window = new ViewToWindow(m_view, this, () => 
      {
        m_view.m_extractBT.Visible = true;
        Visible = true;
        m_windowed = false;
      });
      Visible = false;
      l_window.Size = new System.Drawing.Size(l_window.Size.Width, 500);
      l_window.Text = Text;
      l_window.ShowInTaskbar = true;
      l_window.ShowIcon = true;
      l_window.TopMost = true;
    }
  }
}
