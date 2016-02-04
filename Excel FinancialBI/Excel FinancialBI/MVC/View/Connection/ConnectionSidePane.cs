using System;
using System.Runtime.InteropServices;
using AddinExpress.XL;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

  
namespace FBI.MVC.View.Connection
{
    using Controller;

    public partial class ConnectionSidePane : AddinExpress.XL.ADXExcelTaskPane
    {
        public ConnectionSidePane()
        {
            InitializeComponent();
        }

        private void ADXExcelTaskPane_ADXBeforeTaskPaneShow(object sender, ADXBeforeTaskPaneShowEventArgs e)
        {
           
        }
    }
}
