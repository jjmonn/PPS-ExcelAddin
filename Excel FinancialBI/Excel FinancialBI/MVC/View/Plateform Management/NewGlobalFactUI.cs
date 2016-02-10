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

  public partial class NewGlobalFactUI : Form, IView
  {
    public NewGlobalFactUI()
    {
      InitializeComponent();
      MultilangueSetup();
    }

    private void MultilangueSetup()
    {
      this.m_nameLabel.Text = Local.GetValue("general.name");
      this.CancelBT.Text = Local.GetValue("general.cancel");
      this.ValidateBT.Text = Local.GetValue("general.create");
      this.Text = Local.GetValue("global_facts.new_global_fact");
    }

    public void SetController(IController p_controller)
    {

    }
  }
}
