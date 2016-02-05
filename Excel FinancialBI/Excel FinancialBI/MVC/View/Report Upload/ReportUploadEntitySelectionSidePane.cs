using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using AddinExpress.XL;
  
namespace FBI.MVC.View
{  
    public partial class ReportUploadEntitySelectionSidePane : AddinExpress.XL.ADXExcelTaskPane
    {
      private bool m_visible = false;

        public ReportUploadEntitySelectionSidePane()
        {
            InitializeComponent();
        }

        private void ReportUploadEntitySelectionSidePane_ADXBeforeTaskPaneShow(object sender, ADXBeforeTaskPaneShowEventArgs e)
        {
          this.Visible = m_visible;
        }

     

    }
}
