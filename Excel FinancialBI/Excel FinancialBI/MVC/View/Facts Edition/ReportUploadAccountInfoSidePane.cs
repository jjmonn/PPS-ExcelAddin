using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using AddinExpress.XL;
  
namespace FBI.MVC.View
{
  using FBI;
  using Utils;

  public partial class ReportUploadAccountInfoSidePane : AddinExpress.XL.ADXExcelTaskPane
  {
    public  bool m_shown { set; get; }
    public ReportUploadAccountInfoSidePane()
    {
      InitializeComponent();
      MultilangueSetup();
    }

    private void MultilangueSetup()
    {
      VLabel1.Text = Local.GetValue("general.account");
      VLabel2.Text = Local.GetValue("general.formula");
      VLabel3.Text = Local.GetValue("general.description");
      VLabel4.Text = Local.GetValue("accounts_edition.account_type");
    }

    private void ReportUploadAccountInfoSidePane_ADXBeforeTaskPaneShow(object sender, ADXBeforeTaskPaneShowEventArgs e)
    {
      if (m_shown == false) { this.Visible = false; }
    }
  }
}
