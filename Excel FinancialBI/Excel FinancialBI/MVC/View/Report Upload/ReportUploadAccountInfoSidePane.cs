using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using AddinExpress.XL;
  
namespace FBI.MVC.View
{  
    public partial class ReportUploadAccountInfoSidePane : AddinExpress.XL.ADXExcelTaskPane
    {

      private bool m_visible = false;

      public ReportUploadAccountInfoSidePane()
      {
          InitializeComponent();
      }

      private void ReportUploadAccountInfoSidePane_ADXBeforeTaskPaneShow(object sender, ADXBeforeTaskPaneShowEventArgs e)
      {
        this.Visible = m_visible;
      }
    }
}
