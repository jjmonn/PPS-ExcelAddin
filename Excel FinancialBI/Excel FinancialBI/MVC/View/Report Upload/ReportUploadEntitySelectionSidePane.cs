using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using AddinExpress.XL;

namespace FBI.MVC.View
{
  using FBI;
  using Utils;

  public partial class ReportUploadEntitySelectionSidePane : AddinExpress.XL.ADXExcelTaskPane
  {
    internal bool m_shown { set; get; }
    public ReportUploadEntitySelectionSidePane()
    {
      InitializeComponent();
      MultilangueSetup();
    }

    private void MultilangueSetup()
    {
      m_entitySelectionLabel.Text = Local.GetValue("upload.entities_selection");
      m_periodsSelectionLabel.Text = Local.GetValue("upload.periods_selection");
      m_accountSelectionLabel.Text = Local.GetValue("upload.accounts_selection");
      m_validateButton.Text = Local.GetValue("general.validate");
    }

    private void ReportUploadEntitySelectionSidePane_ADXBeforeTaskPaneShow(object sender, ADXBeforeTaskPaneShowEventArgs e)
    {
      if (m_shown == false) { this.Visible = false; }
    }

  }
}

