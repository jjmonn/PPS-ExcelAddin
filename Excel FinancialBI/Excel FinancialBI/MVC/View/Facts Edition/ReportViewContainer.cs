using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FBI.MVC.View
{
  public partial class ReportViewContainer : Form
  {
    public ReportViewContainer()
    {
      InitializeComponent();
    }

    private void ReportViewContainer_FormClosing(object sender, FormClosingEventArgs e)
    {
      e.Cancel = true;
      Hide();
    }
  }
}
