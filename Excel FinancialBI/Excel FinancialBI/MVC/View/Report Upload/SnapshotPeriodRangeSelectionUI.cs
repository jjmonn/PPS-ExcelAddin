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
  using FBI;
  using Utils;

  public partial class SnapshotPeriodRangeSelectionUI : Form, IView
  {
    public bool m_showned { set; get; } 
    public SnapshotPeriodRangeSelectionUI()
    {
      InitializeComponent();
      MultilangueSelection();
    }

    private void MultilangueSelection()
    {
      this.Text = Local.GetValue("upload.periods_selection");
      m_validateButton.Text = Local.GetValue("general.validate");
      m_accountSelectionLabel.Text = Local.GetValue("upload.accounts_selection");
    }

    public void SetController(IController p_controller)
    {
      if (m_showned == false) { this.Visible = false; }
    }
  }
}
