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

  public partial class PDCClientSelectionUI : Form, IView
  {
    public PDCClientSelectionUI()
    {
      InitializeComponent();
      this.MultilangueSetup();
    }

    private void MultilangueSetup()
    {
      this.Text = Local.GetValue("upload.title_client_selection");
      m_validateButton.Text = Local.GetValue("general.validate");
    }

    public void SetController(IController p_controller)
    {
    }
  }
}