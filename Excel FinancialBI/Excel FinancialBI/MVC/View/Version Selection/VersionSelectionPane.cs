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
  using Controller;
  using Utils;

  public partial class VersionSelectionPane : AddinExpress.XL.ADXExcelTaskPane
  {
    public bool m_shown { set; get; } 
    public VersionSelectionPane()
    {
      InitializeComponent();
      this.MultilangueSetup();
      m_shown = false;
    }

    private void MultilangueSetup()
    {
      m_versionSelectionLabel.Text = Local.GetValue("general.select_version");
      m_validateButton.Text = Local.GetValue("general.validate");
    }

    private void VersionSelectionPane_ADXBeforeTaskPaneShow(object sender, AddinExpress.XL.ADXBeforeTaskPaneShowEventArgs e)
    {
      if (m_shown == false) { this.Visible = false; }
    }
  }
}
