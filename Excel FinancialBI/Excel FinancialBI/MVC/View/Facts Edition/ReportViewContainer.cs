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
  using Utils;

  public partial class ReportViewContainer : Form
  {
    public ReportViewContainer()
    {
      InitializeComponent();
      this.Text = Local.GetValue("upload.title_report_view");
    }

    private void ReportViewContainer_FormClosing(object sender, FormClosingEventArgs e)
    {
      e.Cancel = true;
      Hide();
    }

    private void ReportViewContainer_Load(object sender, EventArgs e)
    {

    }
  }
}
