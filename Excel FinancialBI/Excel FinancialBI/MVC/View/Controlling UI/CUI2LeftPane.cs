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

  public partial class CUI2LeftPane : UserControl, IView
  {
    public CUI2LeftPane()
    {
      InitializeComponent();
      MultilangueSetup();
    }

    private void MultilangueSetup()
    {
      this.SelectionCB.Text = Local.GetValue("CUI.selection");
      this.m_entitySelectionLabel.Text = Local.GetValue("CUI.entities_selection");
      this.SelectAllToolStripMenuItem.Text = Local.GetValue("CUI.select_all");
      this.UnselectAllToolStripMenuItem.Text = Local.GetValue("CUI.unselect_all");
    }

    public void SetController(IController p_controller)
    {

    }
  }
}
