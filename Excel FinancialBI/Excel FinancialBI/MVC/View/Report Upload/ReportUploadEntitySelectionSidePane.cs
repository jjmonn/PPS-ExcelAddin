using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using AddinExpress.XL;

namespace FBI.MVC.View
{
  public partial class ReportUploadEntitySelectionSidePane : AddinExpress.XL.ADXExcelTaskPane
  {
    internal bool m_shown { set; get; }
    public ReportUploadEntitySelectionSidePane()
    {
      InitializeComponent();
    }

    private void ReportUploadEntitySelectionSidePane_ADXBeforeTaskPaneShow(object sender, ADXBeforeTaskPaneShowEventArgs e)
    {
      if (m_shown == false) { this.Visible = false; }
    }

  }
}

