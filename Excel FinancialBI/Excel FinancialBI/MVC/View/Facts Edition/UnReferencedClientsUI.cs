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

  public partial class UnReferencedClientsUI : Form, IView
  {
    public UnReferencedClientsUI()
    {
      InitializeComponent();
      MultilangueSetup();
    }

    private void MultilangueSetup()
    {
      this.Text = Local.GetValue("clientAutoCreation.unerefenced_clients");
      m_createAllButton.Text = Local.GetValue("general.validate");
    }

    public void SetController(IController p_controller)
    {
    }
  }
}
