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

  public partial class VersionSelectionPane : AddinExpress.XL.ADXExcelTaskPane, IView
  {
    internal bool m_shown { set; get; } 
    public VersionSelectionPane()
    {
      InitializeComponent();
      m_shown = false;
    }

    public void SetController(IController p_controller)
    {

    }

    private void VersionSelectionPane_ADXBeforeTaskPaneShow(object sender, AddinExpress.XL.ADXBeforeTaskPaneShowEventArgs e)
    {
      if (m_shown == false) { this.Visible = false; }
    }
  }
}
