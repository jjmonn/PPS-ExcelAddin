using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FBI.Utils;

namespace FBI.MVC.View
{
  using Controller;

  public partial class AllocationKeysView : Form, IView
  {


    public AllocationKeysView()
    {
      InitializeComponent();
    }

    public void SetController(IController p_controller)
    {

    }

    public void LoadView()
    {
      this.MultilangueSetup();
    }

    private void MultilangueSetup()
    {
      m_accountLabel.Text = Local.GetValue("accounts_edition.account_name");

    }
  }
}
